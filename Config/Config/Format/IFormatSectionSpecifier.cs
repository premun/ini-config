using System.Collections;

namespace Config.Format
{
    public interface IFormatSectionSpecifier
    {
        string Name { get; set; }

        void SetOptional(IConfigOption option);

        void SetRequired(IConfigOption option);
    }
}