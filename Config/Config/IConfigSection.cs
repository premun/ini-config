namespace Config
{
    public interface IConfigSection
	{
		T Get<T>(string key);
		T Set<T>(string key, T value);
    }
}
