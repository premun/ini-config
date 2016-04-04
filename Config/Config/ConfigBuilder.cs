using Config.Format;

namespace Config
{
	public abstract class ConfigBuilder : IConfigBuilder
	{
	    private readonly IConfig _empty = new Config();

	    public abstract IConfig Build(IFormatSpecifier formatSpecifier = null, BuildMode buildMode = BuildMode.Relaxed);

	    public IConfig Empty
	    {
	        get { return _empty; }
	    }
	}
}