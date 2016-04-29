namespace Config.Format.Errors
{
	/// <summary>
	/// Thrown when section is found multiple times in one config file.
	/// </summary>
	public class NoSectionException : ConfigException
	{
		public NoSectionException()
			: base("Config option found but no parent section found before")
		{
		}
	}
}