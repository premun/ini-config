using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Config.ConfigExceptions;
using Config.Format;
using Config.Format.OptionSpecifiers;
using Config.IniFiles.Errors;
using Config.IniFiles.Parser;
using Config.IniFiles.Parser.Tokens;
using Config.IniFiles.Validation;
using Config.Options;


[assembly: InternalsVisibleTo("ConfigTests")]

namespace Config.IniFiles
{
	/// <summary>
	/// Class that builds a config out of an ini file.
	/// </summary>
	internal class IniFileConfigBuilder : IConfigBuilder, IDisposable
	{
		private readonly string _path;

		private const string IdentifierPattern = 
			@"[a-zA-Z\.\$:][a-zA-Z0-9_ \-\.:\$]*";

		private const string ReferencePattern =
			@"^\$\{(" + IdentifierPattern + ")#(" + IdentifierPattern + @")\}";

		#region Parsing context related fields

		private List<FormatError> _errors;

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
		    _config.FormatSpecifier = formatSpecifier;
			_errors = new List<FormatError>();
			_formatSpecifier = formatSpecifier;
			_buildMode = buildMode;

			ParseConfig();
		    var strategy = ValidationFactory.GetValidationStrategy(buildMode);
			ValidateConfig(strategy);
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
					// Casts token to dynamic so right method will be founded at runtime.
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

			// Ini options have restricted identifier format
			var identifierRegex = new Regex("^" + IdentifierPattern + "$");
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
			// We will remember the last comment in the section as the only one
			// Might not be the best solution, but not sure what is the right behaviour
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

        /// <summary>
        /// Validates the configuration.
        /// </summary>
        private void ValidateConfig(IValidation validation)
        {
            var errors = validation.ValidateConfig(_config);
            if (errors.Any())
            {
                ReportValidationErrors(errors);
            }
        }

		/// <summary>
		/// Depending on BuildMode, either throws an exception (Strict) or adds error to the list of errors.
		/// </summary>
		/// <param name="error">Error to be reported</param>
		private void ReportError(FormatError error)
		{
			_errors.Add(error);

			if (_buildMode == BuildMode.Strict)
			{
				throw new IniConfigException(error);
			}
		}

        private void ReportValidationErrors(IList<ConfigException> errors)
        {
            var exception = new ConfigFormatException
            {
                ErrorList = errors
            };
            _errors.Add(new MissingSectionOrOptionError(exception));
            if (_buildMode == BuildMode.Strict)
            {
                throw exception;
            }
        }

        public void Dispose()
		{
			_parser.Dispose();
		}
	}
}