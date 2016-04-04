namespace Config.Values
{
	public class IntConfigValue : ConfigValue<int>
	{
		public IntConfigValue(int value)
		{
			Value = value;
		}

		public static implicit operator IntConfigValue(int i)
		{
			return new IntConfigValue(i);
		}
	}
}
