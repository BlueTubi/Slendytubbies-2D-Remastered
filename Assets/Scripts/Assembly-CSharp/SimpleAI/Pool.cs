using System;
using System.Collections;
using System.Collections.Generic;

namespace SimpleAI
{
	public class Pool<T> : IEnumerable, IEnumerable<T> where T : new()
	{
		public struct Node
		{
			internal int NodeIndex;

			public T Item;
		}

		private Node[] pool;

		private bool[] active;

		private Queue<int> available;

		public int AvailableCount
		{
			get
			{
				return available.Count;
			}
		}

		public int ActiveCount
		{
			get
			{
				return pool.Length - available.Count;
			}
		}

		public int Capacity
		{
			get
			{
				return pool.Length;
			}
		}

		public IEnumerable<Node> ActiveNodes
		{
			get
			{
				Node[] array = pool;
				for (int i = 0; i < array.Length; i++)
				{
					Node item = array[i];
					if (active[item.NodeIndex])
					{
						yield return item;
					}
				}
			}
		}

		public IEnumerable<Node> AllNodes
		{
			get
			{
				Node[] array = pool;
				for (int i = 0; i < array.Length; i++)
				{
					yield return array[i];
				}
			}
		}

		public Pool(int capacity)
		{
			if (capacity <= 0)
			{
				throw new ArgumentOutOfRangeException("Pool must contain at least one item.");
			}
			pool = new Node[capacity];
			active = new bool[capacity];
			available = new Queue<int>(capacity);
			for (int i = 0; i < capacity; i++)
			{
				pool[i] = default(Node);
				pool[i].NodeIndex = i;
				pool[i].Item = new T();
				active[i] = false;
				available.Enqueue(i);
			}
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public void Clear()
		{
			available.Clear();
			for (int i = 0; i < pool.Length; i++)
			{
				active[i] = false;
				available.Enqueue(i);
			}
		}

		public Node Get()
		{
			int num = available.Dequeue();
			active[num] = true;
			return pool[num];
		}

		public void Return(Node item)
		{
			if (item.NodeIndex < 0 || item.NodeIndex > pool.Length)
			{
				throw new ArgumentException("Invalid item node.");
			}
			if (!active[item.NodeIndex])
			{
				throw new InvalidOperationException("Attempt to return an inactive node.");
			}
			active[item.NodeIndex] = false;
			available.Enqueue(item.NodeIndex);
		}

		public void SetItemValue(Node item)
		{
			if (item.NodeIndex < 0 || item.NodeIndex > pool.Length)
			{
				throw new ArgumentException("Invalid item node.");
			}
			pool[item.NodeIndex].Item = item.Item;
		}

		public int CopyTo(T[] array, int arrayIndex)
		{
			int num = arrayIndex;
			Node[] array2 = pool;
			for (int i = 0; i < array2.Length; i++)
			{
				Node node = array2[i];
				if (active[node.NodeIndex])
				{
					array[num++] = node.Item;
					if (num == array.Length)
					{
						return num - arrayIndex;
					}
				}
			}
			return num - arrayIndex;
		}

		public IEnumerator<T> GetEnumerator()
		{
			Node[] array = pool;
			for (int i = 0; i < array.Length; i++)
			{
				Node item = array[i];
				if (active[item.NodeIndex])
				{
					yield return item.Item;
				}
			}
		}
	}
}
