namespace Config.IniFiles.Errors
{
	public class FormatError
	{
		public string Message { get; set; }

		public int Line { get; set; }

		public FormatError(string message, int line)
		{
			Line = line;
			Message = message;
		}
	}
}
