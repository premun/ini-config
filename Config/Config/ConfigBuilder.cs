using System.Collections.Generic;
using Config.Format;
using Config.IniFiles.Errors;

namespace Config
{
	public abstract class ConfigBuilder : IConfigBuilder
	{
	    private readonly IConfig _empty = new Config();

	    public abstract IConfig Build(ConfigFormatSpecifier formatSpecifier = null, BuildMode buildMode = BuildMode.Relaxed);

		public abstract IEnumerable<FormatError> GetErrors();

		public IConfig Empty
	    {
	        get { return _empty; }
	    }
	}
}