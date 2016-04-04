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

		public T As<T>()
		{
			return Get<T>();
		}

		public virtual string Serialize()
	    {
		    return Value.ToString();
		}

		#region Auto boxing

		public static implicit operator ConfigValue(bool b)
		{
			return new BoolConfigValue(b);
		}

		public static implicit operator ConfigValue(float f)
		{
			return new FloatConfigValue(f);
		}

		public static implicit operator ConfigValue(int i)
		{
			return new IntConfigValue(i);
		}

		public static implicit operator ConfigValue(long l)
		{
			return new SignedConfigValue(l);
		}

		public static implicit operator ConfigValue(string s)
		{
			return new StringConfigValue(s);
		}

		public static implicit operator ConfigValue(ulong l)
		{
			return new UnsignedConfigValue(l);
		}

		#endregion
	}

	public abstract class ConfigValue<T> : ConfigValue
	{
		public void Set(T value)
		{
			Value = value;
		}
	}
}