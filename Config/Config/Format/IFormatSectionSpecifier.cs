using System.Collections;

namespace Config.Format
{
    public interface IFormatSectionSpecifier
    {
        string Name { get; set; }

        void SetOption(string name, bool required);
    }
}