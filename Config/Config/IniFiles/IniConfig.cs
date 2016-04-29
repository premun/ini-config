using System;
using Config.Format;

namespace Config.IniFiles
{
	/// <summary>
	/// Facade class for ini file manipulation. Sugarcoats using IConfig together with ini files.
	/// </summary>
	public static class IniConfig
	{
		private static string _lastPath;

		public static IConfig FromFile(string path, ConfigFormatSpecifier formatSpecifier = null, BuildMode buildMode = BuildMode.Strict)
		{
			var config = new IniFileConfigBuilder(path).Build(formatSpecifier, buildMode);
			_lastPath = path;
			return config;
		}

		public static void SaveToFile(this IConfig config, string path, Verbosity verbosity = Verbosity.Defaults)
		{
			new IniFileConfigSaver(path).SaveConfig(config, verbosity);
		}

		public static void SaveToFile(this IConfig config, Verbosity verbosity = Verbosity.Defaults)
		{
			if (string.IsNullOrEmpty(_lastPath))
			{
				throw new InvalidOperationException("Please specify the file name");
			}

			new IniFileConfigSaver(_lastPath).SaveConfig(config, verbosity);
		}
	}
}
