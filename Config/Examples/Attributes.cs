using Config;
using Config.Attribute;
using Config.Format;

namespace Examples
{
	/// <summary>
	/// Example usage of config attributes.
	/// Class will load its values from specified config file.
	/// </summary>
	[Config("/www/mywebsite/config.ini", typeof(ConfigFormatSpecifier),  BuildMode.Strict)]
	public class MySqlConnector
	{
		/// <summary>
		/// First attribute parameter is section, second is option name, third says, if option is required.
		/// </summary>
		[ConfigOption("MySQL", "hostname", true)]
		public string Hostname;

		[ConfigOption("MySQL", "username", true)]
		public string Username;

		[ConfigOption("MySQL", "password")]
		public string Password;

		[ConfigOption("MySQL", "schema")]
		public string Schema;
	}

	public class InitializationExample
	{
		public void InitializeMySqlConnector()
		{
			ConfigFactory.Create<MySqlConnector>();
		}
	}

	public class MySqlConfigSpecifier : IConfigFormatSpecifier {
		public ConfigFormatSpecifier GetFormatSpecifier()
		{
			return new ConfigFormatSpecifier()
				.AddSection("MySQL", true)
					.AddOption("hostname", required: true)
					.AddOption("username", required: true)
					.AddOption("password")
					.AddOption("schema", defaultValue: "db1")
				.FinishDefinition();
		}
	}
}