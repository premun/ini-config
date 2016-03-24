namespace Config
{
    public interface IConfig
    { 
        ConfigSection this[string key] { get; set; }
    }
}