using System;
using System.Linq;
using Config;
using Config.Format;
using Config.Values;

namespace Examples
{
	/// <summary>
	/// Example of how a config will be read from an ini file and then used.
	/// </summary>
	public class BasicUsage
	{
		public void ConfigBuilder()
		{
			IConfig config;
			using (var builder = new IniFileConfigBuilder("/www/mywebsite/config.ini"))
			{
				try
				{
					config = builder.Build();
				}
				catch (ConfigFormatException e)
				{
					Console.WriteLine("Cannot build config. Encountered following errors:");

					e.ErrorList
						.ToList()
						.ForEach(error => Console.WriteLine("  - " + error));

					throw;
				}
			}

			// Get config values
			// Either directly
			string hostname = config["MySQL"]["hostname"].As<string>();

			// Or through section
			var mysqlSection = config["MySQL"];
			int port = mysqlSection["port"].As<int>();

			// Set new values (notice method chaining)
			config["MySQL"]
				.Set("foo", 3.14f)
				.Set("bar", 0x45);

			// Set new values using indexer
			config["MySQL"]["persistent"] = true;

			// Get specific section that was not required
			var httpSection = config["HTTP"];
			if (httpSection != null)
			{
				bool secure = httpSection["use_https"].As<bool>();
				int timeout = httpSection["timeout"].As<int>();
			}

			// Add new section
			var newSection = config.AddSection("New section");

			// Add new values
			newSection
				.Set("foo", 123)
				.Set("bar", false);

			newSection["another"] = 42.69f;

			// Save changed config into a new file
			using (var configSaver = new IniFileConfigSaver("/www/mywebsite/config.local.ini"))
			{
				configSaver.SaveConfig(config);
			}
		} 
	}
}