using System.Collections.Generic;

namespace Config.Format
{
	/// <summary>
	/// Enables user to specify the format of a config section.
	/// </summary>
	public interface IFormatSectionSpecifier
    {
        string Name { get; set; }

        List<IFormatOption> RequiredOptions { get; }
		
        List<IFormatOption> OptionalOptions { get; }
    }
}