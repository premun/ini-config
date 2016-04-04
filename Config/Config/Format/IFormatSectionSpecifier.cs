using System.Collections.Generic;

namespace Config.Format
{
	/// <summary>
	/// Class that helps with defining of config file structure.
	/// Represents a set of options that can occur inside a config section.
	/// </summary>
	public interface IFormatSectionSpecifier
    {
        string Name { get; set; }

        List<IFormatOption> RequiredOptions { get; }
		
        List<IFormatOption> OptionalOptions { get; }
    }
}