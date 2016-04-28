﻿namespace Config.Options
{
	public abstract class Option
	{
		public object RawValue { get; protected set; }

		public string Comment { get; set; }

		public virtual string Serialize()
		{
			return RawValue.ToString();
		}

		#region Auto-boxing

		/**
		 * Auto boxing occurs when we want to add values into sections:
		 * config["Section"]["foo"] = "bar";
		 *
		 * Then "bar" needs to be boxed into StringOption automatically.
		 */

		public static implicit operator Option(bool b)
		{
			return new BoolOption(b);
		}

		public static implicit operator Option(float f)
		{
			return new FloatOption(f);
		}

		public static implicit operator Option(int i)
		{
			return new IntOption(i);
		}

		public static implicit operator Option(long l)
		{
			return new SignedOption(l);
		}

		public static implicit operator Option(string s)
		{
			return new StringOption(s);
		}

		public static implicit operator Option(ulong l)
		{
			return new UnsignedOption(l);
		}

		#endregion
	}

	public abstract class Option<T> : Option
	{
		public void Set(T value)
		{
			RawValue = value;
		}
	}
}