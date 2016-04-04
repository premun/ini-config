using System;
using Config.Format;

namespace Config
{
	/// <summary>
	/// Class that builds a config out of an ini file.
	/// </summary>
	public class IniFileConfigBuilder : ConfigBuilder, IDisposable
	{
		public IniFileConfigBuilder(string path)
		{
			throw new NotImplementedException();
		}

		public override IConfig Build(IFormatSpecifier formatSpecifier = null, BuildMode buildMode = BuildMode.Relaxed)
		{
			throw new NotImplementedException();
		}

		public void Dispose()
		{
			throw new NotImplementedException();
		}
	}
}