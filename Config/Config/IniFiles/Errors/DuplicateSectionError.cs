namespace Config.IniFiles.Errors
{
	/// <summary>
	/// Raised when section is found multiple times in one config file.
	/// </summary>
	public class DuplicateSectionError : FormatError
	{
		public DuplicateSectionError(string sectionName, int line)
			: base("Section '" + sectionName + "' found multiple times.", line)
		{
		}
	}
}