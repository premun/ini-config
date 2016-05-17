using System.Collections.Generic;
using System.Linq;

namespace Config.Options
{
	public sealed class ListOption<T> : Option<T> where T : Option
	{
		private IList<T> _values;

		public IEnumerable<T> Values
		{
			get { return _values; }
			set { _values = value.ToList(); }
		}

		#region Overrides of Option

		public override object Data
		{
			get
			{
				return Values;
			}
			protected set
			{
				Values = (IEnumerable<T>) value;
			}
		}

		#endregion

		public ListOption(IEnumerable<T> values)
		{
			Values = values;
		}

		public ListOption(T[] values)
		{
			Values = values;
		}

		public IEnumerable<T> Get()
		{
			return _values;
		}

		public override string Serialize()
		{
			return string.Join(", ", _values.Select(x => x.Serialize()));
		}

		public static implicit operator ListOption<T>(T[] t)
		{
			return new ListOption<T>(t);
		}
	}
}