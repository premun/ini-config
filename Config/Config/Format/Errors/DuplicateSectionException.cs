namespace Config.Format.Errors
{
	/// <summary>
	/// Thrown when section is found multiple times in one config file.
	/// </summary>
	public class DuplicateSectionException : ConfigException
	{
		public DuplicateSectionException(string sectionName)
			: base("Section '" + sectionName + "' found multiple times.")
		{
		}
	}
}