namespace Config.Format
{
	/// <summary>
	/// Enables user to specify name of a required or an optional config option.
	/// </summary>
	public interface IFormatOption
    {
        string Name { get; set; }
    }

    public interface IFormatOption<T> : IFormatOption
    {
    }
}