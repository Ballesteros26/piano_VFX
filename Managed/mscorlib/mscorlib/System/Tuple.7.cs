using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace System
{
	/// <summary>Represents a 6-tuple, or sextuple. </summary>
	/// <typeparam name="T1">The type of the tuple's first component.</typeparam>
	/// <typeparam name="T2">The type of the tuple's second component.</typeparam>
	/// <typeparam name="T3">The type of the tuple's third component.</typeparam>
	/// <typeparam name="T4">The type of the tuple's fourth component.</typeparam>
	/// <typeparam name="T5">The type of the tuple's fifth component.</typeparam>
	/// <typeparam name="T6">The type of the tuple's sixth component.</typeparam>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000EF RID: 239
	[Serializable]
	public class Tuple<T1, T2, T3, T4, T5, T6> : IStructuralEquatable, IStructuralComparable, IComparable, ITupleInternal, ITuple
	{
		/// <summary>Gets the value of the current <see cref="T:System.Tuple`6" /> object's first component.</summary>
		/// <returns>The value of the current <see cref="T:System.Tuple`6" /> object's first component.</returns>
		// Token: 0x17000166 RID: 358
		// (get) Token: 0x06000904 RID: 2308 RVA: 0x0002F9DE File Offset: 0x0002DBDE
		public T1 Item1
		{
			get
			{
				return this.m_Item1;
			}
		}

		/// <summary>Gets the value of the current <see cref="T:System.Tuple`6" /> object's second component.</summary>
		/// <returns>The value of the current <see cref="T:System.Tuple`6" /> object's second component.</returns>
		// Token: 0x17000167 RID: 359
		// (get) Token: 0x06000905 RID: 2309 RVA: 0x0002F9E6 File Offset: 0x0002DBE6
		public T2 Item2
		{
			get
			{
				return this.m_Item2;
			}
		}

		/// <summary>Gets the value of the current <see cref="T:System.Tuple`6" /> object's third component.</summary>
		/// <returns>The value of the current <see cref="T:System.Tuple`6" /> object's third component.</returns>
		// Token: 0x17000168 RID: 360
		// (get) Token: 0x06000906 RID: 2310 RVA: 0x0002F9EE File Offset: 0x0002DBEE
		public T3 Item3
		{
			get
			{
				return this.m_Item3;
			}
		}

		/// <summary>Gets the value of the current <see cref="T:System.Tuple`6" /> object's fourth component.</summary>
		/// <returns>The value of the current <see cref="T:System.Tuple`6" /> object's fourth component.</returns>
		// Token: 0x17000169 RID: 361
		// (get) Token: 0x06000907 RID: 2311 RVA: 0x0002F9F6 File Offset: 0x0002DBF6
		public T4 Item4
		{
			get
			{
				return this.m_Item4;
			}
		}

		/// <summary>Gets the value of the current <see cref="T:System.Tuple`6" /> object's fifth component.</summary>
		/// <returns>The value of the current <see cref="T:System.Tuple`6" /> object's fifth  component.</returns>
		// Token: 0x1700016A RID: 362
		// (get) Token: 0x06000908 RID: 2312 RVA: 0x0002F9FE File Offset: 0x0002DBFE
		public T5 Item5
		{
			get
			{
				return this.m_Item5;
			}
		}

		/// <summary>Gets the value of the current <see cref="T:System.Tuple`6" /> object's sixth component.</summary>
		/// <returns>The value of the current <see cref="T:System.Tuple`6" /> object's sixth component.</returns>
		// Token: 0x1700016B RID: 363
		// (get) Token: 0x06000909 RID: 2313 RVA: 0x0002FA06 File Offset: 0x0002DC06
		public T6 Item6
		{
			get
			{
				return this.m_Item6;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Tuple`6" /> class.</summary>
		/// <param name="item1">The value of the tuple's first component.</param>
		/// <param name="item2">The value of the tuple's second component.</param>
		/// <param name="item3">The value of the tuple's third component.</param>
		/// <param name="item4">The value of the tuple's fourth component</param>
		/// <param name="item5">The value of the tuple's fifth component.</param>
		/// <param name="item6">The value of the tuple's sixth component.</param>
		// Token: 0x0600090A RID: 2314 RVA: 0x0002FA0E File Offset: 0x0002DC0E
		public Tuple(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6)
		{
			this.m_Item1 = item1;
			this.m_Item2 = item2;
			this.m_Item3 = item3;
			this.m_Item4 = item4;
			this.m_Item5 = item5;
			this.m_Item6 = item6;
		}

		/// <summary>Returns a value that indicates whether the current <see cref="T:System.Tuple`6" /> object is equal to a specified object.</summary>
		/// <returns>true if the current instance is equal to the specified object; otherwise, false.</returns>
		/// <param name="obj">The object to compare with this instance.</param>
		// Token: 0x0600090B RID: 2315 RVA: 0x0002EDF2 File Offset: 0x0002CFF2
		public override bool Equals(object obj)
		{
			return ((IStructuralEquatable)this).Equals(obj, ObjectEqualityComparer.Default);
		}

		/// <summary>Returns a value that indicates whether the current <see cref="T:System.Tuple`6" /> object is equal to a specified object based on a specified comparison method.</summary>
		/// <returns>true if the current instance is equal to the specified object; otherwise, false.</returns>
		/// <param name="other">The object to compare with this instance.</param>
		/// <param name="comparer">An object that defines the method to use to evaluate whether the two objects are equal.</param>
		// Token: 0x0600090C RID: 2316 RVA: 0x0002FA44 File Offset: 0x0002DC44
		bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer)
		{
			if (other == null)
			{
				return false;
			}
			Tuple<T1, T2, T3, T4, T5, T6> tuple = other as Tuple<T1, T2, T3, T4, T5, T6>;
			return tuple != null && (comparer.Equals(this.m_Item1, tuple.m_Item1) && comparer.Equals(this.m_Item2, tuple.m_Item2) && comparer.Equals(this.m_Item3, tuple.m_Item3) && comparer.Equals(this.m_Item4, tuple.m_Item4) && comparer.Equals(this.m_Item5, tuple.m_Item5)) && comparer.Equals(this.m_Item6, tuple.m_Item6);
		}

		/// <summary>Compares the current <see cref="T:System.Tuple`6" /> object to a specified object and returns an integer that indicates whether the current object is before, after, or in the same position as the specified object in the sort order.</summary>
		/// <returns>A signed integer that indicates the relative position of this instance and <paramref name="obj" /> in the sort order, as shown in the following table.ValueDescriptionA negative integerThis instance precedes <paramref name="obj" />.ZeroThis instance and <paramref name="obj" /> have the same position in the sort order.A positive integerThis instance follows <paramref name="obj" />.</returns>
		/// <param name="obj">An object to compare with the current instance.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="obj" /> is not a <see cref="T:System.Tuple`6" /> object.</exception>
		// Token: 0x0600090D RID: 2317 RVA: 0x0002EE3A File Offset: 0x0002D03A
		int IComparable.CompareTo(object obj)
		{
			return ((IStructuralComparable)this).CompareTo(obj, LowLevelComparer.Default);
		}

		/// <summary>Compares the current <see cref="T:System.Tuple`6" /> object to a specified object by using a specified comparer and returns an integer that indicates whether the current object is before, after, or in the same position as the specified object in the sort order.</summary>
		/// <returns>A signed integer that indicates the relative position of this instance and <paramref name="other" /> in the sort order, as shown in the following table.ValueDescriptionA negative integerThis instance precedes <paramref name="other" />.ZeroThis instance and <paramref name="other" /> have the same position in the sort order.A positive integerThis instance follows <paramref name="other" />.</returns>
		/// <param name="other">An object to compare with the current instance.</param>
		/// <param name="comparer">An object that provides custom rules for comparison.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="other" /> is not a <see cref="T:System.Tuple`6" /> object.</exception>
		// Token: 0x0600090E RID: 2318 RVA: 0x0002FB1C File Offset: 0x0002DD1C
		int IStructuralComparable.CompareTo(object other, IComparer comparer)
		{
			if (other == null)
			{
				return 1;
			}
			Tuple<T1, T2, T3, T4, T5, T6> tuple = other as Tuple<T1, T2, T3, T4, T5, T6>;
			if (tuple == null)
			{
				throw new ArgumentException(SR.Format("Argument must be of type {0}.", base.GetType().ToString()), "other");
			}
			int num = comparer.Compare(this.m_Item1, tuple.m_Item1);
			if (num != 0)
			{
				return num;
			}
			num = comparer.Compare(this.m_Item2, tuple.m_Item2);
			if (num != 0)
			{
				return num;
			}
			num = comparer.Compare(this.m_Item3, tuple.m_Item3);
			if (num != 0)
			{
				return num;
			}
			num = comparer.Compare(this.m_Item4, tuple.m_Item4);
			if (num != 0)
			{
				return num;
			}
			num = comparer.Compare(this.m_Item5, tuple.m_Item5);
			if (num != 0)
			{
				return num;
			}
			return comparer.Compare(this.m_Item6, tuple.m_Item6);
		}

		/// <summary>Returns the hash code for the current <see cref="T:System.Tuple`6" /> object.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x0600090F RID: 2319 RVA: 0x0002EEA0 File Offset: 0x0002D0A0
		public override int GetHashCode()
		{
			return ((IStructuralEquatable)this).GetHashCode(ObjectEqualityComparer.Default);
		}

		/// <summary>Calculates the hash code for the current <see cref="T:System.Tuple`6" /> object by using a specified computation method.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		/// <param name="comparer">An object whose <see cref="M:System.Collections.IEqualityComparer.GetHashCode(System.Object)" />  method calculates the hash code of the current <see cref="T:System.Tuple`6" /> object.</param>
		// Token: 0x06000910 RID: 2320 RVA: 0x0002FC20 File Offset: 0x0002DE20
		int IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
		{
			return Tuple.CombineHashCodes(comparer.GetHashCode(this.m_Item1), comparer.GetHashCode(this.m_Item2), comparer.GetHashCode(this.m_Item3), comparer.GetHashCode(this.m_Item4), comparer.GetHashCode(this.m_Item5), comparer.GetHashCode(this.m_Item6));
		}

		// Token: 0x06000911 RID: 2321 RVA: 0x0002EEC0 File Offset: 0x0002D0C0
		int ITupleInternal.GetHashCode(IEqualityComparer comparer)
		{
			return ((IStructuralEquatable)this).GetHashCode(comparer);
		}

		/// <summary>Returns a string that represents the value of this <see cref="T:System.Tuple`6" /> instance.</summary>
		/// <returns>The string representation of this <see cref="T:System.Tuple`6" /> object.</returns>
		// Token: 0x06000912 RID: 2322 RVA: 0x0002FC98 File Offset: 0x0002DE98
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("(");
			return ((ITupleInternal)this).ToString(stringBuilder);
		}

		// Token: 0x06000913 RID: 2323 RVA: 0x0002FCC0 File Offset: 0x0002DEC0
		string ITupleInternal.ToString(StringBuilder sb)
		{
			sb.Append(this.m_Item1);
			sb.Append(", ");
			sb.Append(this.m_Item2);
			sb.Append(", ");
			sb.Append(this.m_Item3);
			sb.Append(", ");
			sb.Append(this.m_Item4);
			sb.Append(", ");
			sb.Append(this.m_Item5);
			sb.Append(", ");
			sb.Append(this.m_Item6);
			sb.Append(')');
			return sb.ToString();
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x06000914 RID: 2324 RVA: 0x00029D4E File Offset: 0x00027F4E
		int ITuple.Length
		{
			get
			{
				return 6;
			}
		}

		// Token: 0x1700016D RID: 365
		object ITuple.this[int index]
		{
			get
			{
				switch (index)
				{
				case 0:
					return this.Item1;
				case 1:
					return this.Item2;
				case 2:
					return this.Item3;
				case 3:
					return this.Item4;
				case 4:
					return this.Item5;
				case 5:
					return this.Item6;
				default:
					throw new IndexOutOfRangeException();
				}
			}
		}

		// Token: 0x040006E8 RID: 1768
		private readonly T1 m_Item1;

		// Token: 0x040006E9 RID: 1769
		private readonly T2 m_Item2;

		// Token: 0x040006EA RID: 1770
		private readonly T3 m_Item3;

		// Token: 0x040006EB RID: 1771
		private readonly T4 m_Item4;

		// Token: 0x040006EC RID: 1772
		private readonly T5 m_Item5;

		// Token: 0x040006ED RID: 1773
		private readonly T6 m_Item6;
	}
}
