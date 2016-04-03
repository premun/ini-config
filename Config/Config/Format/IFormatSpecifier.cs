using System.Collections.Generic;

namespace Config.Format
{
    /// <summary>
    /// Enables user to specify the format of the config file.
    /// </summary>
    public interface IFormatSpecifier
    {
        List<FormatSectionSpecifier> RequiredSections { get; }

        List<FormatSectionSpecifier> OptionalSections { get; }
    }
}