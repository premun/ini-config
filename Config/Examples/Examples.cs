using System.Collections.Generic;
using Config;
using Config.Format;
using Config.Values;

namespace Examples
{
    public class Examples
    {
		private readonly FormatSpecifier _formatSpecifier = new FormatSpecifier
		{
			RequiredSections = new List<FormatSectionSpecifier>
			{
				new FormatSectionSpecifier("Section1")
				{
					RequiredOptions = new List<IFormatOption>
					{
						new FormatOption<string>("Option1"),
						new FormatOption<IntConfigValue>("Option2")
					}
				},
				new FormatSectionSpecifier("Section2")
				{
					RequiredOptions = new List<IFormatOption>
					{
						new FormatListOption<int>("Opt1")
					},
					OptionalOptions = new List<IFormatOption>
					{
						new FormatOption<bool>("Optional1")
					}
				}
			},
			OptionalSections = new List<FormatSectionSpecifier>
			{
				new FormatSectionSpecifier("OptSection")
				{
					RequiredOptions = new List<IFormatOption>
					{
						new FormatOption<double>("ReqInOpt1")
					}
				}
			}
		};

	    private void ExampleUsage(IConfigBuilder configBuilder)
		{
			var config = configBuilder.Build(_formatSpecifier, BuildMode.Strict);
			config["foo"]["bar"].Get<int>();
		}
	}
}
