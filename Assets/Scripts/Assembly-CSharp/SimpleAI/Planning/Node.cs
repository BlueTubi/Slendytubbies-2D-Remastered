using System;

namespace SimpleAI.Planning
{
	public class Node : IComparable<Node>
	{
		public enum eState
		{
			kUnvisited = 0,
			kOpen = 1,
			kClosed = 2,
			kBlocked = 3
		}

		public const int kInvalidIndex = -1;

		private float m_f;

		private float m_g;

		private float m_h;

		private eState m_state;

		private Node m_parent;

		private int m_index;

		public eState State
		{
			get
			{
				return m_state;
			}
			set
			{
				m_state = value;
			}
		}

		public Node Parent
		{
			get
			{
				return m_parent;
			}
			set
			{
				m_parent = value;
			}
		}

		public int Index
		{
			get
			{
				return m_index;
			}
			set
			{
				m_index = value;
			}
		}

		public float F
		{
			get
			{
				return m_f;
			}
			set
			{
				m_f = value;
			}
		}

		public float G
		{
			get
			{
				return m_g;
			}
			set
			{
				m_g = value;
			}
		}

		public float H
		{
			get
			{
				return m_h;
			}
			set
			{
				m_h = value;
			}
		}

		public Node()
		{
			Awake(-1, eState.kUnvisited);
		}

		public int CompareTo(Node other)
		{
			if (m_f < other.m_f)
			{
				return -1;
			}
			if (m_f > other.m_f)
			{
				return 1;
			}
			return 0;
		}

		public override string ToString()
		{
			return "Node:" + m_f;
		}

		public void Awake(int nodeIndex, eState state)
		{
			m_index = nodeIndex;
			m_f = float.MaxValue;
			m_state = state;
			m_parent = null;
			m_g = float.MaxValue;
			m_h = float.MaxValue;
		}
	}
}
