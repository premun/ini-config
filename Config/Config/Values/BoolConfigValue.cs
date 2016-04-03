using System;

namespace Config.Values
{
	public class BoolConfigValue : ConfigValue<bool>
	{
		private string _textValue;

		public BoolConfigValue(bool value)
		{
			Value = value;
			_textValue = "false";
		}

		public BoolConfigValue(string value)
		{
			Value = FromString(value);
			_textValue = value;
		}

		private bool FromString(string value)
		{
			switch (value)
			{
				case "0":
				case "f":
				case "n":
				case "off":
				case "no":
				case "disabled":
					return false;

				case "1":
				case "t":
				case "y":
				case "on":
				case "yes":
				case "enabled":
					return true;

				default:
					throw new ArgumentOutOfRangeException(string.Format("Unknown boolean value '{0}'", value));
			}
		}

		public override string Serialize()
		{
			return _textValue;
		}

		public void Set(string value)
		{
			Value = FromString(value);
			_textValue = value;
		}
	}
}
