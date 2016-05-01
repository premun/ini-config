namespace Config.IniFiles.Errors
{
	/// <summary>
	/// Raised when identifier contains invalid characters.
	/// </summary>
	/// TODO: zatim ji nikde nevyhazujeme, ale asi bychom meli (necet jsem ted uz znova specku totiz)
	public class InvalidIdentifierError : FormatError
	{
		public InvalidIdentifierError(string identifier, int line) 
			: base("Identifier '" + identifier + "' is invalid.", line)
		{
		}
	}
}