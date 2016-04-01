using System.Collections.Generic;

namespace Config.Format
{
    public class FormatSpecifier : IFormatSpecifier
    {
        public FormatSpecifier()
        {
            RequiredSections = new List<FormatSectionSpecifier>();
            OptionalSections = new List<FormatSectionSpecifier>();
        }

        #region Implementation of IFormatSpecifier

        public List<FormatSectionSpecifier> RequiredSections { get; set; }
        public List<FormatSectionSpecifier> OptionalSections { get; set; }

        #endregion
    }
}