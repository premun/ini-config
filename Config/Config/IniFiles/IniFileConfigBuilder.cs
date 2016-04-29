using System;
using System.IO;
using Config.Format;
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

		public IniFileConfigBuilder(string path)
		{
			_path = path;
		}

		public override IConfig Build(ConfigFormatSpecifier formatSpecifier = null, BuildMode buildMode = BuildMode.Relaxed)
		{
			var config = ReadConfig();
			ParseConfig(config, formatSpecifier);
			ValidateConfig(config);

			return config;
		}

		/// <summary>
		/// Tries to parse all values into the type dictated by the format specification.
		/// </summary>
		/// <param name="config">Config to be parsed</param>
		/// <param name="formatSpecifier">Format specification</param>
		private void ParseConfig(IConfig config, ConfigFormatSpecifier formatSpecifier)
		{
			foreach (var section in config.Sections)
			{
				var formatSection = formatSpecifier[section.Name];
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

					section[optionName] = formatOption.Parse(section[optionName].String);
				}
			}
		}

		private void ValidateConfig(IConfig config)
		{
			throw new NotImplementedException();
		}

		private IConfig ReadConfig()
		{
			var config = new Config();

			using (var reader = new StreamReader(_path))
			{
				var parser = new TokenParser(reader);

				IConfigSection currentSection = null;
				Token token;

				while ((token = parser.GetNextToken()) != null)
				{
					// TODO: wrap block in try/catch and collect errors
					// TODO: decide whether to continue parsing even with parser errors
					if (token is SectionHeaderToken)
					{
						var sectionName = ((SectionHeaderToken) token).Name;
						if (config.GetSection(sectionName) != null)
						{
							// TODO: DuplicateSectionToken
							continue;
						}

						currentSection = config.AddSection(sectionName);
					}
					else if (token is OptionToken)
					{
						if (currentSection == null)
						{
							// TODO: throw new ConfigFormatException
							continue;
						}

						var optionToken = (OptionToken) token;

						// We save values as strings and parse them in the second step
						currentSection.Add(optionToken.Name, new StringOption(optionToken.Value));
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

			return config;
		}

		public void Dispose()
		{
			throw new NotImplementedException();
		}
	}
}