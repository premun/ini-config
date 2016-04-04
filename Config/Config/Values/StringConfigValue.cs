namespace Config.Values
{
	public class StringConfigValue : ConfigValue<int>
	{
		public StringConfigValue(string value)
		{
			Value = value;
		}

		public static implicit operator StringConfigValue(string s)
		{
			return new StringConfigValue(s);
		}
	}
}
