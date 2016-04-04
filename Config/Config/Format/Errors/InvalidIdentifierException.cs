namespace Config.Format.Errors
{
	/// <summary>
	/// Thrown when identifier contains invalid characters.
	/// </summary>
	public class InvalidIdentifierException : ConfigException
	{
		public InvalidIdentifierException(string identifier) 
			: base("Identifier '" + identifier + "' is invalid.")
		{
		}
	}
}