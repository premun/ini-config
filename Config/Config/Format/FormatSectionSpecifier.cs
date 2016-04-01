using System.Collections.Generic;

namespace Config.Format
{
    public class FormatSectionSpecifier : IFormatSectionSpecifier
    {
        public FormatSectionSpecifier(string name)
        {
            Name = name;
            RequiredOptions = new List<IFormatOption>();
            OptionalOption = new List<IFormatOption>();
        }

        #region Implementation of IFormatSectionSpecifier

        public string Name { get; set; }
        public List<IFormatOption> RequiredOptions { get; set; }
        public List<IFormatOption> OptionalOption { get; set; }

        #endregion
    }
}