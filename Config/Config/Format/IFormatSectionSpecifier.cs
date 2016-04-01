using System.Collections;
using System.Collections.Generic;

namespace Config.Format
{
    public interface IFormatSectionSpecifier
    {
        string Name { get; set; }

        List<IFormatOption> RequiredOptions { get; }

        // TODO lepsi nazev
        List<IFormatOption> OptionalOption { get; }
    }
}