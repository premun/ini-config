﻿using Config.Format.Errors;

namespace Config.Values
{
	public class BoolConfigValue : ConfigValue<bool>
	{
		private readonly string _textValue;

		public BoolConfigValue(bool value)
		{
			Value = value;
			_textValue = "false";
		}

		public BoolConfigValue(string value)
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
					Value = false;
					break;

				case "1":
				case "t":
				case "y":
				case "on":
				case "yes":
				case "enabled":
					Value = true;
					break;

				default:
					throw new InvalidValueException(string.Format("Unknown boolean value '{0}'", value));
			}
		}

		public override string Serialize()
		{
			return _textValue;
		}

		public static implicit operator BoolConfigValue(bool b)
		{
			return new BoolConfigValue(b);
		}
	}
}
