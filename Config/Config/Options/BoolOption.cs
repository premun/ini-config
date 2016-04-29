using System;

namespace Config.Options
{
	public class BoolOption : Option<bool>
	{
		private readonly string _textValue;

		public BoolOption(bool rawValue)
		{
			RawValue = rawValue;
			_textValue = "false";
		}

		public BoolOption(string value)
		{
			_textValue = value;

			switch (value)
			{
				case "0":
				case "f":
				case "n":
				case "off":
				case "no":
				case "disabled":
					RawValue = false;
					break;

				case "1":
				case "t":
				case "y":
				case "on":
				case "yes":
				case "enabled":
					RawValue = true;
					break;

				default:
					throw new ArgumentOutOfRangeException(string.Format("Unknown boolean value '{0}'", value));
			}
		}

		public override string Serialize()
		{
			return _textValue;
		}

		public static implicit operator BoolOption(bool b)
		{
			return new BoolOption(b);
		}
	}
}
