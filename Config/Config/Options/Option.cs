using System.Collections.Generic;
using System.Linq;

namespace Config.Options
{
	public abstract class Option
	{
		public object Data { get; protected set; }

		public string Comment { get; set; }

		public virtual string Serialize()
		{
			return Data.ToString();
		}

		#region Typed getters

		public bool Bool
		{
			get { return (bool) Data; }
		}

		public IEnumerable<bool> BoolList
		{
			get
			{
				var list = ((ListOption<BoolOption>) Data).Get();
				return list.Select(o => o.Bool);
			}
		}

		public float Float
		{
			get { return (float) Data; }
		}

		public IEnumerable<float> FloatList
		{
			get
			{
				var list = ((ListOption<FloatOption>) Data).Get();
				return list.Select(o => o.Float);
			}
		}

		public int Int
		{
			get { return (int) Data; }
		}

		public IEnumerable<int> IntList
		{
			get
			{
				var list = ((ListOption<IntOption>) Data).Get();
				return list.Select(o => o.Int);
			}
		}

		public long Signed
		{
			get { return (long) Data; }
		}

		public IEnumerable<long> SignedList
		{
			get
			{
				var list = ((ListOption<SignedOption>) Data).Get();
				return list.Select(o => o.Signed);
			}
		}

		public string String
		{
			get { return (string) Data; }
		}

		public IEnumerable<string> StringList
		{
			get
			{
				var list = ((ListOption<StringOption>) Data).Get();
				return list.Select(o => o.String);
			}
		}

		public ulong Unsigned
		{
			get { return (ulong) Data; }
		}

		public IEnumerable<ulong> UnsignedList
		{
			get
			{
				var list = ((ListOption<UnsignedOption>) Data).Get();
				return list.Select(o => o.Unsigned);
			}
		}

		#endregion

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

		public static implicit operator Option(bool[] bl)
		{
			return new ListOption<BoolOption>(bl.Select(b => new BoolOption(b)));
		}

		public static implicit operator Option(float f)
		{
			return new FloatOption(f);
		}

		public static implicit operator Option(float[] fl)
		{
			return new ListOption<FloatOption>(fl.Select(f => new FloatOption(f)));
		}

		public static implicit operator Option(int i)
		{
			return new IntOption(i);
		}

		public static implicit operator Option(int[] il)
		{
			return new ListOption<IntOption>(il.Select(i => new IntOption(i)));
		}

		public static implicit operator Option(long l)
		{
			return new SignedOption(l);
		}

		public static implicit operator Option(long[] sl)
		{
			return new ListOption<SignedOption>(sl.Select(s => new SignedOption(s)));
		}

		public static implicit operator Option(string s)
		{
			return new StringOption(s);
		}

		public static implicit operator Option(string[] sl)
		{
			return new ListOption<StringOption>(sl.Select(s => new StringOption(s)));
		}

		public static implicit operator Option(ulong l)
		{
			return new UnsignedOption(l);
		}

		public static implicit operator Option(ulong[] ul)
		{
			return new ListOption<UnsignedOption>(ul.Select(u => new UnsignedOption(u)));
		}

		#endregion
	}

	public abstract class Option<T> : Option
	{
	}
}