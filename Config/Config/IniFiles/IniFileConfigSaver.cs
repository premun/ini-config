using System;

namespace Config.IniFiles
{
	/// <summary>
	/// Example stub of an object that will store config into an ini file.
	/// </summary>
	/// TODO: Turn into internal?
	public class IniFileConfigSaver : IConfigSaver, IDisposable
	{
		private readonly string _path;

		public IniFileConfigSaver(string path)
		{
			_path = path;
		}

		public void SaveConfig(IConfig config, Verbosity verbosity)
		{
			throw new NotImplementedException();
		}

		public void Dispose()
		{
			throw new NotImplementedException();
		}
	}
}