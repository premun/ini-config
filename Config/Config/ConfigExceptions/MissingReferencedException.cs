namespace Config.ConfigExceptions
{
	public class MissingReferencedException : ConfigException
	{
		public MissingReferencedException(string section, string option)
			: base("Reference to non existent option '" + section + "@" + option + "'.")
		{}
	}
}