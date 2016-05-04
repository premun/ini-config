namespace Config.IniFiles.Errors
{
	public class FormatError
	{
		public string Message { get; private set; }

		public int Line { get; private set; }

	    public FormatError(string message, int line)
		{
			Line = line;
			Message = message;
		}
	}
}
