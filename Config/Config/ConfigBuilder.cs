using System.Collections.Generic;
using Config.Format;

namespace Config
{
	public abstract class ConfigBuilder : IConfigBuilder
	{
	    private readonly IConfig _empty = new Config();

	    public abstract IConfig Build(ConfigFormatSpecifier formatSpecifier = null, BuildMode buildMode = BuildMode.Relaxed);

		public abstract IEnumerable<ConfigException> GetErrors();

		public IConfig Empty
	    {
	        get { return _empty; }
	    }
	}
}