namespace Config.Format.Errors
{
	/// <summary>
	/// Thrown when value could not be parsed correctly.
	/// </summary>
	public class InvalidValueException : ConfigException
	{
		public InvalidValueException(string message) : base(message)
		{
		}
	}
}