namespace Config.Options
{
	public class StringOption : Option<int>
	{
		public StringOption(string rawValue)
		{
			RawValue = rawValue;
		}

		public static implicit operator StringOption(string s)
		{
			return new StringOption(s);
		}
	}
}
