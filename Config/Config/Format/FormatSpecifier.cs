using System.Collections.Generic;

namespace Config.Format
{
    public class FormatSpecifier : IFormatSpecifier
    {
        public FormatSpecifier()
        {
            RequiredSections = new List<IFormatSectionSpecifier>();
            OptionalSections = new List<IFormatSectionSpecifier>();
        }

        #region Implementation of IFormatSpecifier

        public List<IFormatSectionSpecifier> RequiredSections { get; set; }

        public List<IFormatSectionSpecifier> OptionalSections { get; set; }

        #endregion
    }
}