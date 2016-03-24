namespace Config
{
    /// <summary>
    /// Represents functionality for storing IConfig objects.
    /// </summary>
	public interface IConfigSaver
	{
        /// <summary>
        /// Saves given IConfig data. Target is dependent on implementation (ini file, database...).
        /// </summary>
        /// <param name="config">Config data object</param>
		void Save(IConfig config);
	}
}