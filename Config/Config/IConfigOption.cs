namespace Config
{
    public interface IConfigOption
    {
        /// <summary>
        /// Unique name of the config option, used for identification.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        string Name { get; }

        object Value { get; set; }

        T Get<T>();

        void Set(object value);
    }
}