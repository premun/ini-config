using Config.Format;

namespace Config
{
	public interface IConfigBuilder
	{
		IConfig Build(IFormatSpecifier formatSpecifier = null, Mode mode = Mode.Relaxed);

		IConfig Empty { get; }
	}
}