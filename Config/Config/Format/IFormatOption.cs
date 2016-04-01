namespace Config.Format
{
    public interface IFormatOption
    {
        string Name { get; set; }
    }

    public interface IFormatOption<T> : IFormatOption
    {
    }
}