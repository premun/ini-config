namespace Config.Values
{
	public class UnsignedConfigValue : ConfigValue<ulong>
	{
		public UnsignedConfigValue(ulong value)
		{
			Value = value;
		}

		public static implicit operator UnsignedConfigValue(ulong l)
		{
			return new UnsignedConfigValue(l);
		}
	}
}