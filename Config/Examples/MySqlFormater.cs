using System.Collections.Generic;
using Config.Format;
using Config.Values;

namespace Examples
{
	public class MySqlFormater : IFormatSpecifier
	{
		#region Implementation of IFormatSpecifier

		public List<IFormatSectionSpecifier> RequiredSections { get; } = new List<IFormatSectionSpecifier>
		{
			new FormatSectionSpecifier("MySQL")
			{
				RequiredOptions = new List<IFormatOption>
				{
					new FormatOption<StringConfigValue>("hostname"),
					new FormatOption<StringConfigValue>("username"),
					new FormatOption<StringConfigValue>("password"),
					new FormatOption<StringConfigValue>("schema")
				},
				OptionalOptions = new List<IFormatOption>
				{
					new FormatOption<IntConfigValue>("port")
				}
			}
		};

		public List<IFormatSectionSpecifier> OptionalSections => new List<IFormatSectionSpecifier>();

		#endregion
	}
}