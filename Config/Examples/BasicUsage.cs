using System;
using Config;
using Config.Format;

namespace Examples
{
	public class BasicUsage
	{
		public void ConfigBuilder()
		{
			IConfig config;
			using (IniFileConfigBuilder builder = new IniFileConfigBuilder("/www/mywebsite/config.ini"))
			{
				try
				{
					config = builder.Build();
				}
				catch (ConfigFormatException e)
				{
					Console.WriteLine("Cannot build config:");

					foreach (var error in e.ErrorList)
					{
						Console.WriteLine("  - " + error);
					}

					throw;
				}
			}

			// Use config
			var hostname = config["MySQL"]["hostname"].As<string>();
			var port = config["MySQL"]["port"].As<int>();
			
			// ...
		} 
	}
}