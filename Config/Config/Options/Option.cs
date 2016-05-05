using System.Collections.Generic;
using System.Linq;

namespace Config.Options
{
	public abstract class Option
	{
		public virtual object Data { get; protected set; }

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

		public IList<bool> BoolList
		{
			get
			{
				var list = (List<BoolOption>) Data;
				return list.Select(o => o.Bool).ToList();
			}
		}

		public float Float
		{
			get { return (float) Data; }
		}

        public IList<float> FloatList
		{
			get
			{
				var list = (List<FloatOption>) Data;
				return list.Select(o => o.Float).ToList();
			}
		}

		public int Int
		{
			get { return (int) Data; }
		}

        public IList<int> IntList
		{
			get
			{
			    var list = (List<IntOption>) Data;
				return list.Select(o => o.Int).ToList();
			}
		}

		public long Signed
		{
			get { return (long) Data; }
		}

        public IList<long> SignedList
		{
			get
			{
				var list = (List<SignedOption>) Data;
			    return list.Select(o => o.Signed).ToList();
			}
		}

		public string String
		{
			get { return (string) Data; }
		}

		public IList<string> StringList
		{
			get
			{
				var list = (List<StringOption>) Data;
				return list.Select(o => o.String).ToList();
			}
		}

		public ulong Unsigned
		{
			get { return (ulong) Data; }
		}

		public IList<ulong> UnsignedList
		{
			get
			{
				var list = (List<UnsignedOption>) Data;
				return list.Select(o => o.Unsigned).ToList();
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