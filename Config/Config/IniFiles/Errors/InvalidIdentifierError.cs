namespace Config.IniFiles.Errors
{
	/// <summary>
	/// Raised when identifier contains invalid characters.
	/// </summary>
	public class InvalidIdentifierError : FormatError
	{
		public InvalidIdentifierError(string identifier, int line) 
			: base("Identifier '" + identifier + "' is invalid.", line)
		{
		}
	}
}