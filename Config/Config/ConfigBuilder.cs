using System.Collections.Generic;
using Config.Format;
using Config.IniFiles.Errors;

namespace Config
{
	public abstract class ConfigBuilder : IConfigBuilder
	{
	    private readonly IConfig _empty = new Config();

		public IConfig Build(ConfigFormatSpecifier formatSpecifier = null, BuildMode buildMode = BuildMode.Relaxed)
		{
			var config = new Config();
			Build(config, formatSpecifier, buildMode);
			return config;
		}

		public abstract void Build(IConfig config, ConfigFormatSpecifier formatSpecifier = null, BuildMode buildMode = BuildMode.Relaxed);

		public IConfig Empty
	    {
	        get { return _empty; }
	    }

		public abstract bool Ok { get; }

		public abstract IEnumerable<FormatError> Errors { get; }
	}
}