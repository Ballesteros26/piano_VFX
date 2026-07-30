using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x020000D9 RID: 217
	[Serializable]
	public struct ValueTuple<T1> : IEquatable<ValueTuple<T1>>, IStructuralEquatable, IStructuralComparable, IComparable, IComparable<ValueTuple<T1>>, IValueTupleInternal, ITuple
	{
		// Token: 0x0600076F RID: 1903 RVA: 0x000279F3 File Offset: 0x00025BF3
		public ValueTuple(T1 item1)
		{
			this.Item1 = item1;
		}

		// Token: 0x06000770 RID: 1904 RVA: 0x000279FC File Offset: 0x00025BFC
		public override bool Equals(object obj)
		{
			return obj is ValueTuple<T1> && this.Equals((ValueTuple<T1>)obj);
		}

		// Token: 0x06000771 RID: 1905 RVA: 0x00027A14 File Offset: 0x00025C14
		public bool Equals(ValueTuple<T1> other)
		{
			return EqualityComparer<T1>.Default.Equals(this.Item1, other.Item1);
		}

		// Token: 0x06000772 RID: 1906 RVA: 0x00027A2C File Offset: 0x00025C2C
		bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer)
		{
			if (other == null || !(other is ValueTuple<T1>))
			{
				return false;
			}
			ValueTuple<T1> valueTuple = (ValueTuple<T1>)other;
			return comparer.Equals(this.Item1, valueTuple.Item1);
		}

		// Token: 0x06000773 RID: 1907 RVA: 0x00027A6C File Offset: 0x00025C6C
		int IComparable.CompareTo(object other)
		{
			if (other == null)
			{
				return 1;
			}
			if (!(other is ValueTuple<T1>))
			{
				throw new ArgumentException(SR.Format("Argument must be of type {0}.", base.GetType().ToString()), "other");
			}
			ValueTuple<T1> valueTuple = (ValueTuple<T1>)other;
			return Comparer<T1>.Default.Compare(this.Item1, valueTuple.Item1);
		}

		// Token: 0x06000774 RID: 1908 RVA: 0x00027ACD File Offset: 0x00025CCD
		public int CompareTo(ValueTuple<T1> other)
		{
			return Comparer<T1>.Default.Compare(this.Item1, other.Item1);
		}

		// Token: 0x06000775 RID: 1909 RVA: 0x00027AE8 File Offset: 0x00025CE8
		int IStructuralComparable.CompareTo(object other, IComparer comparer)
		{
			if (other == null)
			{
				return 1;
			}
			if (!(other is ValueTuple<T1>))
			{
				throw new ArgumentException(SR.Format("Argument must be of type {0}.", base.GetType().ToString()), "other");
			}
			ValueTuple<T1> valueTuple = (ValueTuple<T1>)other;
			return comparer.Compare(this.Item1, valueTuple.Item1);
		}

		// Token: 0x06000776 RID: 1910 RVA: 0x00027B50 File Offset: 0x00025D50
		public override int GetHashCode()
		{
			ref T1 ptr = ref this.Item1;
			T1 t = default(T1);
			if (t == null)
			{
				t = this.Item1;
				ptr = ref t;
				if (t == null)
				{
					return 0;
				}
			}
			return ptr.GetHashCode();
		}

		// Token: 0x06000777 RID: 1911 RVA: 0x00027B91 File Offset: 0x00025D91
		int IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
		{
			return comparer.GetHashCode(this.Item1);
		}

		// Token: 0x06000778 RID: 1912 RVA: 0x00027B91 File Offset: 0x00025D91
		int IValueTupleInternal.GetHashCode(IEqualityComparer comparer)
		{
			return comparer.GetHashCode(this.Item1);
		}

		// Token: 0x06000779 RID: 1913 RVA: 0x00027BA4 File Offset: 0x00025DA4
		public override string ToString()
		{
			string text = "(";
			ref T1 ptr = ref this.Item1;
			T1 t = default(T1);
			string text2;
			if (t == null)
			{
				t = this.Item1;
				ptr = ref t;
				if (t == null)
				{
					text2 = null;
					goto IL_003A;
				}
			}
			text2 = ptr.ToString();
			IL_003A:
			return text + text2 + ")";
		}

		// Token: 0x0600077A RID: 1914 RVA: 0x00027BF8 File Offset: 0x00025DF8
		string IValueTupleInternal.ToStringEnd()
		{
			ref T1 ptr = ref this.Item1;
			T1 t = default(T1);
			string text;
			if (t == null)
			{
				t = this.Item1;
				ptr = ref t;
				if (t == null)
				{
					text = null;
					goto IL_0035;
				}
			}
			text = ptr.ToString();
			IL_0035:
			return text + ")";
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x0600077B RID: 1915 RVA: 0x00003B29 File Offset: 0x00001D29
		int ITuple.Length
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x1700012F RID: 303
		object ITuple.this[int index]
		{
			get
			{
				if (index != 0)
				{
					throw new IndexOutOfRangeException();
				}
				return this.Item1;
			}
		}

		// Token: 0x040006A6 RID: 1702
		public T1 Item1;
	}
}
