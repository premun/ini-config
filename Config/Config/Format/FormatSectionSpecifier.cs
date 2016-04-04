using System.Collections.Generic;

namespace Config.Format
{
    public class FormatSectionSpecifier : IFormatSectionSpecifier
	{
		#region Implementation of IFormatSectionSpecifier

		public string Name { get; set; }

		public List<IFormatOption> RequiredOptions { get; set; }

		public List<IFormatOption> OptionalOptions { get; set; }

		#endregion

		public FormatSectionSpecifier(string name)
        {
            Name = name;
            RequiredOptions = new List<IFormatOption>();
            OptionalOptions = new List<IFormatOption>();
        }
    }
}