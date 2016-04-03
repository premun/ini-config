namespace Config.Values
{
	public class SignedConfigValue : ConfigValue<long>
	{
		public SignedConfigValue(long value)
		{
			Value = value;
		}
	}
}