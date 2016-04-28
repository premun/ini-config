using System;
using System.IO;
using Config.Format;
using Config.IniFileParser;
using Config.IniFileParser.Tokens;

namespace Config
{
	/// <summary>
	/// Class that builds a config out of an ini file.
	/// </summary>
	public class IniFileConfigBuilder : ConfigBuilder, IDisposable
	{
		private readonly string _path;

		public IniFileConfigBuilder(string path)
		{
			_path = path;
		}

		public override IConfig Build(ConfigFormatSpecifier formatSpecifier = null, BuildMode buildMode = BuildMode.Relaxed)
		{
			IConfig config = new Config();

			using (var reader = new StreamReader(_path))
			{
				var parser = new TokenParser(reader);

				try
				{
					IConfigSection currentSection = null;
					Token token;

					while ((token = parser.GetNextToken()) != null)
					{
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
							// TODO: Parse value
							// currentSection.Add(option.Name, option.Value);
						}
						else if (token is CommentToken)
						{
							// TODO
							// currentSection.Comment = ((CommentToken) token).Content;
						}
					}
				}
				catch (ConfigFormatException e)
				{
					// TODO
				}
			}

			// TODO: Validate format specification

			return config;
		}

		public void Dispose()
		{
			throw new NotImplementedException();
		}
	}
}