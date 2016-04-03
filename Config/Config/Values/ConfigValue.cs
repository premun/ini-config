namespace Config.Values
{
    public abstract class ConfigValue
    {
		public object Value { get; protected set; }

		public string Description { get; set; }

	    public T Get<T>()
	    {
		    return (T) Value;
	    }

	    public virtual string Serialize()
	    {
		    return Value.ToString();
	    }
    }

	public abstract class ConfigValue<T> : ConfigValue
	{
		public void Set(T value)
		{
			Value = value;
		}
	}
}