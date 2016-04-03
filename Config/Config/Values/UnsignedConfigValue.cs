namespace Config.Values
{
	public class UnsignedConfigValue : ConfigValue<ulong>
	{
		public UnsignedConfigValue(ulong value)
		{
			Value = value;
		}
	}
}