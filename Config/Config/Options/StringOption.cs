namespace Config.Options
{
	public class StringOption : Option<string>
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
