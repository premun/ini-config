namespace Config.Format.Errors
{
	public class DuplicateSectionFormatError : ConfigFormatError
	{
		public DuplicateSectionFormatError(string sectionName)
			: base("Section '" + sectionName + "' found multiple times.")
		{
		}
	}
}