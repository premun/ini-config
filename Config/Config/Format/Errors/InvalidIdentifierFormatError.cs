namespace Config.Format.Errors
{
	public class InvalidIdentifierFormatError : ConfigFormatError
	{
		public InvalidIdentifierFormatError(string identifier) 
			: base("Identifier '" + identifier + "' is invalid.")
		{
		}
	}
}