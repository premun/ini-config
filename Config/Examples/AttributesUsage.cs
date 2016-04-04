using Config;
using Config.Attribute;

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
}