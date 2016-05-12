using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Config.Format;
using Config.Format.OptionSpecifiers;
using Config.IniFiles.Errors;
using Config.IniFiles.Parser;
using Config.IniFiles.Parser.Tokens;
using Config.Options;

[assembly: InternalsVisibleTo("ConfigTests")]

namespace Config.IniFiles
{
	/// <summary>
	/// Class that builds a config out of an ini file.
	/// </summary>
	/// TODO: Turn into internal?
	public class IniFileConfigBuilder : IConfigBuilder, IDisposable
	{
		private readonly string _path;

	    private const string ReferencePattern =
	        @"^\$\{([a-zA-Z\.\$:][a-zA-Z0-9_ \-\.:\$]*)#([a-zA-Z\.\$:][a-zA-Z0-9_ \-\.:\$]*)\}";


		#region Parsing context related fields

		private IList<FormatError> _errors;

		private ConfigFormatSpecifier _formatSpecifier;

		private BuildMode _buildMode = BuildMode.Relaxed;

		private readonly ITokenParser _parser;

		private IConfig _config;

		private IConfigSection _currentSection;

		#endregion

		public IniFileConfigBuilder(string path)
		{
			_path = path;
			_parser = new TokenParser();
		}

		internal IniFileConfigBuilder(ITokenParser tokenParser)
		{
			_parser = tokenParser;
		}

		public IConfig Build(ConfigFormatSpecifier formatSpecifier = null, BuildMode buildMode = BuildMode.Relaxed)
		{
			var config = new Config();
			Build(config, formatSpecifier, buildMode);
			return config;
		}

		public void Build(IConfig config, ConfigFormatSpecifier formatSpecifier = null, BuildMode buildMode = BuildMode.Relaxed)
		{
			_config = config;
			_errors = new List<FormatError>();
			_formatSpecifier = formatSpecifier;
			_buildMode = buildMode;

			ParseConfig();
			ValidateConfig();
		}

		public IEnumerable<FormatError> Errors
		{
			get
			{
				if (_errors == null)
				{
					throw new InvalidOperationException("Getter for Errors called, but Build() not called before");
				}

				return _errors;
			}
		}

		/// <summary>
		/// Gets whether there are parser errors.
		/// </summary>
		public bool Ok
		{
			get
			{
				return _errors == null || _errors.Count == 0;
			}
		}

	    /// <summary>
		/// Reads the ini file and creates sections and options (with string values).
		/// </summary>
		private void ParseConfig()
		{
			using (_parser)
			{
				_parser.Open(_path);

				Token token;
				while ((token = _parser.GetNextToken()) != null)
				{
					// TODO: Okomentovat
					ParseToken((dynamic) token);
				}
			}
		}

		private void ParseToken(SectionHeaderToken token)
		{
			var sectionName = token.Name;
			if (_config[sectionName] != null)
			{
				ReportError(new DuplicateSectionError(sectionName, _parser.GetLine()));
				return;
			}

			_currentSection = _config.AddSection(sectionName);
		}

		private void ParseToken(OptionToken token)
		{
			if (_currentSection == null)
			{
				ReportError(new NoSectionError(_parser.GetLine()));
				return;
			}

			// TODO: Tahle kontrola by mozna mela bejt nekde niz (treba v OptionToken.Parse()), 
			// TODO: vyhodit nejakou special vyjjimku a tady ji odchytit a pridat cislo radku atd.
			// TODO: Pokud se to prenda jinam, tak treba zmenit InvalidIdentifierShouldRaiseError test
			var identifierRegex = new Regex(@"^[a-zA-Z\.\$:][a-zA-Z0-9_ \-\.:\$]*$");
			if (!identifierRegex.IsMatch(token.Name))
			{
				ReportError(new InvalidIdentifierError(token.Name, _parser.GetLine()));
				return;
			}

			var option = ParseOptionValue(token, _currentSection.Name);
			_currentSection[token.Name] = option;
		}

		private void ParseToken(CommentToken token)
		{
			// TODO: Opravdu tohle chovani?
			if (_currentSection != null)
			{
				_currentSection.Comment = token.Content;
			}
		}

		/// <summary>
		/// Tries to parse an option value according to the format specification.
		/// </summary>
		private Option ParseOptionValue(OptionToken token, string sectionName)
		{
			if (_formatSpecifier == null)
			{
				return new StringOption(token);
			}

			var formatSection = _formatSpecifier[sectionName];
			if (formatSection == null)
			{
				return new StringOption(token);
			}

            // Checks if string is reference
            var regex = Regex.Match(token.Value, ReferencePattern);
            if (regex.Success)
            {
                return new ReferenceOption(regex.Groups[1].Value, regex.Groups[2].Value, _config);
            }

			var formatOption = formatSection[token.Name];
			if (formatOption == null)
			{
				return new StringOption(token);
			}

			try
			{
                var option = ParseToken(formatOption, token);
				option.Comment = token.Comment;
				return option;
			}
			catch
			{
				ReportError(new InvalidValueError(token.Name, token.Value, formatOption, _parser.GetLine()));
				return new StringOption(token);
			}
		}

	    private Option ParseToken(OptionSpecifier specifier, OptionToken token)
	    {
	        return specifier.Parse(token.Value);
	    }

		private void ValidateConfig()
		{
			// TODO: Zvalidovat chybejici options/sections a reportovat errory
		}

		/// <summary>
		/// Depending on BuildMode, either throws an exception (Strict) or adds error to the list of errors.
		/// </summary>
		/// <param name="error"></param>
		// TODO overit, ze chceme tohle chovani vzhledem k build modum
		private void ReportError(FormatError error)
		{
			_errors.Add(error);

			if (_buildMode == BuildMode.Strict)
			{
				throw new IniConfigException(error);
			}
		}

		public void Dispose()
		{
			_parser.Dispose();
		}
	}
}