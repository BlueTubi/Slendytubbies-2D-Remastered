using System;
using System.Collections;
using System.Collections.Generic;

namespace SimpleAI
{
	public class BinaryHeap<T> : IEnumerable, ICollection<T>, IEnumerable<T> where T : IComparable<T>
	{
		private const int DEFAULT_SIZE = 4;

		private T[] _data = new T[4];

		private int _count;

		private int _capacity = 4;

		private bool _sorted;

		public int Count
		{
			get
			{
				return _count;
			}
		}

		public int Capacity
		{
			get
			{
				return _capacity;
			}
			set
			{
				int capacity = _capacity;
				_capacity = Math.Max(value, _count);
				if (_capacity != capacity)
				{
					T[] array = new T[_capacity];
					Array.Copy(_data, array, _count);
					_data = array;
				}
			}
		}

		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		public BinaryHeap()
		{
		}

		private BinaryHeap(T[] data, int count)
		{
			Capacity = count;
			_count = count;
			Array.Copy(data, _data, count);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public T Peek()
		{
			return _data[0];
		}

		public void Clear()
		{
			_count = 0;
			_data = new T[_capacity];
		}

		public void Add(T item)
		{
			if (_count < Capacity)
			{
				_data[_count] = item;
				UpHeap();
				_count++;
			}
		}

		public T Remove()
		{
			if (_count == 0)
			{
				throw new InvalidOperationException("Cannot remove item, heap is empty.");
			}
			T result = _data[0];
			_count--;
			_data[0] = _data[_count];
			_data[_count] = default(T);
			DownHeap();
			return result;
		}

		private void UpHeap()
		{
			_sorted = false;
			int num = _count;
			T val = _data[num];
			int num2 = Parent(num);
			while (num2 > -1 && val.CompareTo(_data[num2]) < 0)
			{
				_data[num] = _data[num2];
				num = num2;
				num2 = Parent(num);
			}
			_data[num] = val;
		}

		private void DownHeap()
		{
			_sorted = false;
			int num = 0;
			T val = _data[num];
			while (true)
			{
				int num2 = Child1(num);
				if (num2 >= _count)
				{
					break;
				}
				int num3 = Child2(num);
				int num4 = ((num3 < _count) ? ((_data[num2].CompareTo(_data[num3]) >= 0) ? num3 : num2) : num2);
				if (val.CompareTo(_data[num4]) > 0)
				{
					_data[num] = _data[num4];
					num = num4;
					continue;
				}
				break;
			}
			_data[num] = val;
		}

		private void EnsureSort()
		{
			if (!_sorted)
			{
				Array.Sort(_data, 0, _count);
				_sorted = true;
			}
		}

		private static int Parent(int index)
		{
			return index - 1 >> 1;
		}

		private static int Child1(int index)
		{
			return (index << 1) + 1;
		}

		private static int Child2(int index)
		{
			return (index << 1) + 2;
		}

		public BinaryHeap<T> Copy()
		{
			return new BinaryHeap<T>(_data, _count);
		}

		public IEnumerator<T> GetEnumerator()
		{
			EnsureSort();
			for (int i = 0; i < _count; i++)
			{
				yield return _data[i];
			}
		}

		public bool Contains(T item)
		{
			EnsureSort();
			return Array.BinarySearch(_data, 0, _count, item) >= 0;
		}

		public void CopyTo(T[] array, int arrayIndex)
		{
			EnsureSort();
			Array.Copy(_data, array, _count);
		}

		public bool Remove(T item)
		{
			EnsureSort();
			int num = Array.BinarySearch(_data, 0, _count, item);
			if (num < 0)
			{
				return false;
			}
			Array.Copy(_data, num + 1, _data, num, _count - num);
			_data[_count] = default(T);
			_count--;
			return true;
		}
	}
}
