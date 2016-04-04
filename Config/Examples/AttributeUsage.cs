using Config;
using Config.Attribute;

namespace Examples
{
	/// <summary>
	/// Example usage of config attributes.
	/// Class will load its values from specified config file.
	/// </summary>
	[Config("/www/mywebsite/config.ini", typeof (MySqlFormater), BuildMode.Strict)]
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

		public MySqlConnector()
		{
			// Connect to MySQL using fetched config values...
		}
	}

	public class InitializationExample
	{
		public void InitializeMySqlConnector()
		{
			ConfigFactory.Create<MySqlConnector>();
		}
	}
}