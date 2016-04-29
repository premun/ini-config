using System;
using System.Collections.Generic;
using System.IO;
using Config.Format;
using Config.IniFiles.Errors;
using Config.IniFiles.Parser;
using Config.IniFiles.Parser.Tokens;
using Config.Options;

namespace Config.IniFiles
{
	/// <summary>
	/// Class that builds a config out of an ini file.
	/// </summary>
	/// TODO: Turn into internal?
	public class IniFileConfigBuilder : ConfigBuilder, IDisposable
	{
		private readonly string _path;

		#region Parsing context related fields

		private IList<FormatError> _errors;

		private ConfigFormatSpecifier _formatSpecifier;

		private BuildMode _buildMode = BuildMode.Relaxed;

		private StreamReader _reader;

		private ITokenParser _parser;

		private IConfig _config;

		private IConfigSection _currentSection;

		#endregion

		public IniFileConfigBuilder(string path)
		{
			_path = path;
		}

		public override IConfig Build(ConfigFormatSpecifier formatSpecifier = null, BuildMode buildMode = BuildMode.Relaxed)
		{
			_config = new Config();
			_errors = new List<FormatError>();
			_formatSpecifier = formatSpecifier;
			_buildMode = buildMode;

			ParseConfig();
			ValidateConfig();

			return _config;
		}

		public override IEnumerable<FormatError> GetErrors()
		{
			if (_errors == null)
			{
				throw new InvalidOperationException("GetErrors() called, but Build() not called before");
			}

			return _errors;
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
			using (_reader = new StreamReader(_path))
			{
				_parser = new TokenParser(_reader);
				
				Token token;

				while ((token = _parser.GetNextToken()) != null)
				{
					if (token is SectionHeaderToken)
					{
						ParseToken((SectionHeaderToken) token);
					}
					else if (token is OptionToken)
					{
						ParseToken((OptionToken) token);
					}
					else if (token is CommentToken)
					{
						ParseToken((CommentToken) token);
					}
				}
			}
		}

		private void ParseToken(SectionHeaderToken token)
		{
			var sectionName = token.Name;
			if (_config.GetSection(sectionName) != null)
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
				ReportError(new NoSectionException(_parser.GetLine()));
				return;
			}
			
			_currentSection[token.Name] = ParseOptionValue(token, _currentSection.Name);
		}

		private void ParseToken(CommentToken token)
		{
			// TODO: Opravdu tohle chovani?
			if (_currentSection != null)
			{
				_currentSection.Comment = ((CommentToken) token).Content;
			}
		}

		/// <summary>
		/// Tries to parse an option value according to the format specification.
		/// </summary>
		private Option ParseOptionValue(OptionToken token, string sectionName)
		{
			var formatSection = _formatSpecifier[sectionName];
			if (formatSection == null)
			{
				return new StringOption(token);
			}

			var formatOption = formatSection[token.Name];
			if (formatOption == null)
			{
				return new StringOption(token);
			}

			try
			{
				var option = formatOption.Parse(token.Value);
				option.Comment = token.Comment;
				return option;
			}
			catch
			{
				ReportError(new InvalidValueError(token.Name, token.Value, formatOption, _parser.GetLine()));
				return new StringOption(token);
			}
		}

		/// <summary>
		/// Depending on BuildMode, either throws an exception (Strict) or adds error to the list of errors.
		/// </summary>
		/// <param name="error"></param>
		private void ReportError(FormatError error)
		{
			if (_buildMode == BuildMode.Strict)
			{
				throw new IniConfigException(error);
			}

			_errors.Add(error);
		}

		private void ValidateConfig()
		{
			throw new NotImplementedException();
		}

		public void Dispose()
		{
			_reader.Close();
			_reader.Dispose();
		}
	}
}