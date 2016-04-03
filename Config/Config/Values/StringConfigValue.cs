namespace Config.Values
{
	public class StringConfigValue : ConfigValue<int>
	{
		public StringConfigValue(string value)
		{
			Value = value;
		}
	}
}
