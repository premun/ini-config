namespace Config.Options
{
	public class IntOption : Option<int>
	{
		public IntOption(int rawValue)
		{
			RawValue = rawValue;
		}

		public IntOption(string value)
		{
			RawValue = int.Parse(value);
		}

		public static implicit operator IntOption(int i)
		{
			return new IntOption(i);
		}
	}
}
