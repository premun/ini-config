using Config.Format;

namespace Config
{
	public abstract class ConfigBuilder : IConfigBuilder
	{
		public abstract IConfig Build(IFormatSpecifier formatSpecifier = null, BuildMode buildMode = BuildMode.Relaxed);

		public IConfig Empty { get; } = new Config();
	}
}