namespace Config.ConfigExceptions
{
	public class ReferenceCycleException : ConfigException
	{
		public ReferenceCycleException(string section, string option)
			: base("Cycle reference detected when referencing '${" + section + "#" + option + "}'.")
		{
		}
	}
}