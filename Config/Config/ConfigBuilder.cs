using Config.Format;

namespace Config
{
	// TODO: implement protected method that will compare format with loaded values and implementations can use it
	public abstract class ConfigBuilder : IConfigBuilder
	{
		public abstract IConfig Build(IFormatSpecifier formatSpecifier = null, BuildMode buildMode = BuildMode.Relaxed);

		public IConfig Empty { get; } = new Config();
	}
}