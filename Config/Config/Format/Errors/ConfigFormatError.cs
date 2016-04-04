namespace Config.Format.Errors
{
    // TODO: udelat asi nejaky potomky a vymyslet, co za chyby muze bejt...
    public abstract class ConfigFormatError 
    {
	    protected ConfigFormatError(string message)
	    {
		    Message = message;
	    }

	    public string Message { get; }
    }
}
