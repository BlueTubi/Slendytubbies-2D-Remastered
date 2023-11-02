using System.Collections;
using System.Collections.Generic;

namespace ExitGames.Client.Photon
{
	public class Hashtable : Dictionary<object, object>
	{
		private DictionaryEntryEnumerator enumerator;

		public new object this[object key]
		{
			get
			{
				object value = null;
				TryGetValue(key, out value);
				return value;
			}
			set
			{
				base[key] = value;
			}
		}

		public Hashtable()
		{
		}

		public Hashtable(int x)
			: base(x)
		{
		}

		public new IEnumerator<DictionaryEntry> GetEnumerator()
		{
			return new DictionaryEntryEnumerator(((IDictionary)this).GetEnumerator());
		}

		public override string ToString()
		{
			List<string> list = new List<string>();
			foreach (object key in base.Keys)
			{
				list.Add(string.Concat("(", key.GetType(), ")", key, "=(", key.GetType(), ")", this[key]));
			}
			return string.Join(", ", list.ToArray());
		}

		public object Clone()
		{
			return new Dictionary<object, object>(this);
		}
	}
}
