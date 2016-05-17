
namespace Config.Format
{
    /// <summary>
    /// Represents implementation of config specification.
    /// </summary>
	public interface IConfigFormatSpecifier
	{
        /// <summary>
        /// Gets the format specifier.
        /// </summary>
        /// <returns></returns>
		ConfigFormatSpecifier GetFormatSpecifier();
	}
}
