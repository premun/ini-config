using System;
using System.Collections.Generic;
using System.IO;
using Config.Format;
using Config.Format.Errors;
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

		private IList<ConfigException> _errors;

		private ConfigFormatSpecifier _formatSpecifier;

		private BuildMode _buildMode = BuildMode.Relaxed;

		private StreamReader _reader;

		private IConfig _config;

		public IniFileConfigBuilder(string path)
		{
			_path = path;
		}

		public override IConfig Build(ConfigFormatSpecifier formatSpecifier = null, BuildMode buildMode = BuildMode.Relaxed)
		{
			_config = new Config();
			_errors = new List<ConfigException>();
			_formatSpecifier = formatSpecifier;
			_buildMode = buildMode;

			ReadConfig();
			ParseConfig();
			ValidateConfig();

			return _config;
		}

		public override IEnumerable<ConfigException> GetErrors()
		{
			if (_errors == null)
			{
				throw new InvalidOperationException("GetErrors() called, but Build() not called before");
			}

			return _errors;
		}

		/// <summary>
		/// Reads the ini file and creates sections and options (with string values).
		/// </summary>
		private void ReadConfig()
		{
			var config = new Config();

			using (_reader = new StreamReader(_path))
			{
				var parser = new TokenParser(_reader);

				IConfigSection currentSection = null;
				Token token;

				while ((token = parser.GetNextToken()) != null)
				{
					if (token is SectionHeaderToken)
					{
						var sectionName = ((SectionHeaderToken) token).Name;
						if (config.GetSection(sectionName) != null)
						{
							ReportError(new DuplicateSectionException(sectionName));
							continue;
						}

						currentSection = config.AddSection(sectionName);
					}
					else if (token is OptionToken)
					{
						if (currentSection == null)
						{
							ReportError(new NoSectionException());
							continue;
						}

						var optionToken = (OptionToken) token;
						var option = new StringOption(optionToken.Value)
						{
							Comment = optionToken.Comment
						};

						// We save values as strings and parse them in the second step
						currentSection.Add(optionToken.Name, option);
					}
					else if (token is CommentToken)
					{
						// TODO: Opravdu tohle chovani?
						if (currentSection != null)
						{
							currentSection.Comment = ((CommentToken) token).Content;
						}
					}
				}
			}
		}

		/// <summary>
		/// Tries to parse all values into the type dictated by the format specification.
		/// </summary>
		private void ParseConfig()
		{
			foreach (var section in _config.Sections)
			{
				var formatSection = _formatSpecifier[section.Name];
				if (formatSection == null)
				{
					continue;
				}

				foreach (var optionName in section.Keys)
				{
					var formatOption = formatSection[optionName];
					if (formatOption == null)
					{
						continue;
					}

					try
					{
						section[optionName] = formatOption.Parse(section[optionName].String);
					}
					catch
					{
						ReportError(new ParseOptionException(optionName, formatOption));
					}
				}
			}
		}

		private void ReportError(ConfigException error)
		{
			if (_buildMode == BuildMode.Strict)
			{
				throw error;
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