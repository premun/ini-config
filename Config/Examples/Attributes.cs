using System.Collections.Generic;
using Config;
using Config.Attribute;
using Config.Format;
using Config.Values;

namespace Examples
{
	/// <summary>
	/// Example usage of config attributes.
	/// </summary>
	[Config("/www/mywebsite/config.ini", typeof (MySqlFormater), BuildMode.Strict)]
	public class MySqlConnector
	{
		[ConfigOption("MySQL", "hostname")]
		public string Hostname;

		[ConfigOption("MySQL", "username")]
		public string Username;

		[ConfigOption("MySQL", "password")]
		public string Password;

		[ConfigOption("MySQL", "schema")]
		public string Schema;

		public MySqlConnector()
		{
			// Connect to MySQL using fetched config values...
		}
	}

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