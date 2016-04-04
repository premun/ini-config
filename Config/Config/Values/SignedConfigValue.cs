namespace Config.Values
{
	public class SignedConfigValue : ConfigValue<long>
	{
		public SignedConfigValue(long value)
		{
			Value = value;
		}

		public static implicit operator SignedConfigValue(long l)
		{
			return new SignedConfigValue(l);
		}
	}
}