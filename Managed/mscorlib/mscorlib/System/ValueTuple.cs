using System;
using System.Collections;
using System.Numerics.Hashing;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x020000D8 RID: 216
	[Serializable]
	public struct ValueTuple : IEquatable<ValueTuple>, IStructuralEquatable, IStructuralComparable, IComparable, IComparable<ValueTuple>, IValueTupleInternal, ITuple
	{
		// Token: 0x06000752 RID: 1874 RVA: 0x00027891 File Offset: 0x00025A91
		public override bool Equals(object obj)
		{
			return obj is ValueTuple;
		}

		// Token: 0x06000753 RID: 1875 RVA: 0x00003B29 File Offset: 0x00001D29
		public bool Equals(ValueTuple other)
		{
			return true;
		}

		// Token: 0x06000754 RID: 1876 RVA: 0x00027891 File Offset: 0x00025A91
		bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer)
		{
			return other is ValueTuple;
		}

		// Token: 0x06000755 RID: 1877 RVA: 0x0002789C File Offset: 0x00025A9C
		int IComparable.CompareTo(object other)
		{
			if (other == null)
			{
				return 1;
			}
			if (!(other is ValueTuple))
			{
				throw new ArgumentException(SR.Format("Argument must be of type {0}.", base.GetType().ToString()), "other");
			}
			return 0;
		}

		// Token: 0x06000756 RID: 1878 RVA: 0x00015ED5 File Offset: 0x000140D5
		public int CompareTo(ValueTuple other)
		{
			return 0;
		}

		// Token: 0x06000757 RID: 1879 RVA: 0x0002789C File Offset: 0x00025A9C
		int IStructuralComparable.CompareTo(object other, IComparer comparer)
		{
			if (other == null)
			{
				return 1;
			}
			if (!(other is ValueTuple))
			{
				throw new ArgumentException(SR.Format("Argument must be of type {0}.", base.GetType().ToString()), "other");
			}
			return 0;
		}

		// Token: 0x06000758 RID: 1880 RVA: 0x00015ED5 File Offset: 0x000140D5
		public override int GetHashCode()
		{
			return 0;
		}

		// Token: 0x06000759 RID: 1881 RVA: 0x00015ED5 File Offset: 0x000140D5
		int IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
		{
			return 0;
		}

		// Token: 0x0600075A RID: 1882 RVA: 0x00015ED5 File Offset: 0x000140D5
		int IValueTupleInternal.GetHashCode(IEqualityComparer comparer)
		{
			return 0;
		}

		// Token: 0x0600075B RID: 1883 RVA: 0x000278D6 File Offset: 0x00025AD6
		public override string ToString()
		{
			return "()";
		}

		// Token: 0x0600075C RID: 1884 RVA: 0x000278DD File Offset: 0x00025ADD
		string IValueTupleInternal.ToStringEnd()
		{
			return ")";
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x0600075D RID: 1885 RVA: 0x00015ED5 File Offset: 0x000140D5
		int ITuple.Length
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x1700012D RID: 301
		object ITuple.this[int index]
		{
			get
			{
				throw new IndexOutOfRangeException();
			}
		}

		// Token: 0x0600075F RID: 1887 RVA: 0x000278EC File Offset: 0x00025AEC
		public static ValueTuple Create()
		{
			return default(ValueTuple);
		}

		// Token: 0x06000760 RID: 1888 RVA: 0x00027902 File Offset: 0x00025B02
		public static ValueTuple<T1> Create<T1>(T1 item1)
		{
			return new ValueTuple<T1>(item1);
		}

		// Token: 0x06000761 RID: 1889 RVA: 0x0002790A File Offset: 0x00025B0A
		public static ValueTuple<T1, T2> Create<T1, T2>(T1 item1, T2 item2)
		{
			return new ValueTuple<T1, T2>(item1, item2);
		}

		// Token: 0x06000762 RID: 1890 RVA: 0x00027913 File Offset: 0x00025B13
		public static ValueTuple<T1, T2, T3> Create<T1, T2, T3>(T1 item1, T2 item2, T3 item3)
		{
			return new ValueTuple<T1, T2, T3>(item1, item2, item3);
		}

		// Token: 0x06000763 RID: 1891 RVA: 0x0002791D File Offset: 0x00025B1D
		public static ValueTuple<T1, T2, T3, T4> Create<T1, T2, T3, T4>(T1 item1, T2 item2, T3 item3, T4 item4)
		{
			return new ValueTuple<T1, T2, T3, T4>(item1, item2, item3, item4);
		}

		// Token: 0x06000764 RID: 1892 RVA: 0x00027928 File Offset: 0x00025B28
		public static ValueTuple<T1, T2, T3, T4, T5> Create<T1, T2, T3, T4, T5>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5)
		{
			return new ValueTuple<T1, T2, T3, T4, T5>(item1, item2, item3, item4, item5);
		}

		// Token: 0x06000765 RID: 1893 RVA: 0x00027935 File Offset: 0x00025B35
		public static ValueTuple<T1, T2, T3, T4, T5, T6> Create<T1, T2, T3, T4, T5, T6>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6)
		{
			return new ValueTuple<T1, T2, T3, T4, T5, T6>(item1, item2, item3, item4, item5, item6);
		}

		// Token: 0x06000766 RID: 1894 RVA: 0x00027944 File Offset: 0x00025B44
		public static ValueTuple<T1, T2, T3, T4, T5, T6, T7> Create<T1, T2, T3, T4, T5, T6, T7>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7)
		{
			return new ValueTuple<T1, T2, T3, T4, T5, T6, T7>(item1, item2, item3, item4, item5, item6, item7);
		}

		// Token: 0x06000767 RID: 1895 RVA: 0x00027955 File Offset: 0x00025B55
		public static ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8>> Create<T1, T2, T3, T4, T5, T6, T7, T8>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8)
		{
			return new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8>>(item1, item2, item3, item4, item5, item6, item7, ValueTuple.Create<T8>(item8));
		}

		// Token: 0x06000768 RID: 1896 RVA: 0x0002796D File Offset: 0x00025B6D
		internal static int CombineHashCodes(int h1, int h2)
		{
			return global::System.Numerics.Hashing.HashHelpers.Combine(global::System.Numerics.Hashing.HashHelpers.Combine(global::System.Numerics.Hashing.HashHelpers.RandomSeed, h1), h2);
		}

		// Token: 0x06000769 RID: 1897 RVA: 0x00027980 File Offset: 0x00025B80
		internal static int CombineHashCodes(int h1, int h2, int h3)
		{
			return global::System.Numerics.Hashing.HashHelpers.Combine(ValueTuple.CombineHashCodes(h1, h2), h3);
		}

		// Token: 0x0600076A RID: 1898 RVA: 0x0002798F File Offset: 0x00025B8F
		internal static int CombineHashCodes(int h1, int h2, int h3, int h4)
		{
			return global::System.Numerics.Hashing.HashHelpers.Combine(ValueTuple.CombineHashCodes(h1, h2, h3), h4);
		}

		// Token: 0x0600076B RID: 1899 RVA: 0x0002799F File Offset: 0x00025B9F
		internal static int CombineHashCodes(int h1, int h2, int h3, int h4, int h5)
		{
			return global::System.Numerics.Hashing.HashHelpers.Combine(ValueTuple.CombineHashCodes(h1, h2, h3, h4), h5);
		}

		// Token: 0x0600076C RID: 1900 RVA: 0x000279B1 File Offset: 0x00025BB1
		internal static int CombineHashCodes(int h1, int h2, int h3, int h4, int h5, int h6)
		{
			return global::System.Numerics.Hashing.HashHelpers.Combine(ValueTuple.CombineHashCodes(h1, h2, h3, h4, h5), h6);
		}

		// Token: 0x0600076D RID: 1901 RVA: 0x000279C5 File Offset: 0x00025BC5
		internal static int CombineHashCodes(int h1, int h2, int h3, int h4, int h5, int h6, int h7)
		{
			return global::System.Numerics.Hashing.HashHelpers.Combine(ValueTuple.CombineHashCodes(h1, h2, h3, h4, h5, h6), h7);
		}

		// Token: 0x0600076E RID: 1902 RVA: 0x000279DB File Offset: 0x00025BDB
		internal static int CombineHashCodes(int h1, int h2, int h3, int h4, int h5, int h6, int h7, int h8)
		{
			return global::System.Numerics.Hashing.HashHelpers.Combine(ValueTuple.CombineHashCodes(h1, h2, h3, h4, h5, h6, h7), h8);
		}
	}
}
