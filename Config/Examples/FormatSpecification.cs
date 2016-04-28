using System.Collections.Generic;
using Config.Format;
using Config.Options;

namespace Examples
{
	/// <summary>
	/// Example of how a config file format can be specified.
	/// There are two required sections (Server, HTTP) and one optional (Paths).
	/// </summary>
	public class FormatSpecification
    {
	    public readonly IFormatSpecifier FormatSpecifier = new FormatSpecifier
		{
			RequiredSections = new List<IFormatSectionSpecifier>
				{
					new FormatSectionSpecifier("Server")
					{
						RequiredOptions = new List<IFormatOption>
						{
							new FormatOption<StringOption>("hostname"),
							// Example of restricted values (range)
							new ConstraintFormatOption<IntOption>("port", x => (int) x > 0 && (int) x < 65536),
							// Example of enum
							new EnumFormatOption<StringOption>("domain", new [] {"com", "eu", "fr"})
						}
					},
					new FormatSectionSpecifier("HTTP")
					{
						RequiredOptions = new List<IFormatOption>
						{
							// Example of default value
							new FormatOption<IntOption>("timeout", 5000)
						},
						OptionalOptions = new List<IFormatOption>
						{
							// Example of default value
							new FormatOption<BoolOption>("use_https", false)
						}
					}
				},
			OptionalSections = new List<IFormatSectionSpecifier>
				{
					new FormatSectionSpecifier("Paths")
					{
						RequiredOptions = new List<IFormatOption>
						{
							new FormatOption<StringOption>("jquery_js")
						},
						OptionalOptions = new List<IFormatOption>
						{
							new FormatOption<StringOption>("main_css"),
							new FormatOption<StringOption>("main_js")
						}
					}
				}
		};
    }
}
