namespace Config.IniFiles.Errors
{
	/// <summary>
	/// Raised when value could not be parsed correctly.
	/// </summary>
	public class NoSectionError : FormatError
	{
		public NoSectionError(int line)
			: base("Config option found but no parent section found before", line)
		{
		}
	}
}