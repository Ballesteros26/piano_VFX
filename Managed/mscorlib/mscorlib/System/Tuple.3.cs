using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace System
{
	/// <summary>Represents a 2-tuple, or pair. </summary>
	/// <typeparam name="T1">The type of the tuple's first component.</typeparam>
	/// <typeparam name="T2">The type of the tuple's second component.</typeparam>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000EB RID: 235
	[Serializable]
	public class Tuple<T1, T2> : IStructuralEquatable, IStructuralComparable, IComparable, ITupleInternal, ITuple
	{
		/// <summary>Gets the value of the current <see cref="T:System.Tuple`2" /> object's first component.</summary>
		/// <returns>The value of the current <see cref="T:System.Tuple`2" /> object's first component.</returns>
		// Token: 0x17000150 RID: 336
		// (get) Token: 0x060008C6 RID: 2246 RVA: 0x0002EF2B File Offset: 0x0002D12B
		public T1 Item1
		{
			get
			{
				return this.m_Item1;
			}
		}

		/// <summary>Gets the value of the current <see cref="T:System.Tuple`2" /> object's second component.</summary>
		/// <returns>The value of the current <see cref="T:System.Tuple`2" /> object's second component.</returns>
		// Token: 0x17000151 RID: 337
		// (get) Token: 0x060008C7 RID: 2247 RVA: 0x0002EF33 File Offset: 0x0002D133
		public T2 Item2
		{
			get
			{
				return this.m_Item2;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Tuple`2" /> class.</summary>
		/// <param name="item1">The value of the tuple's first component.</param>
		/// <param name="item2">The value of the tuple's second component.</param>
		// Token: 0x060008C8 RID: 2248 RVA: 0x0002EF3B File Offset: 0x0002D13B
		public Tuple(T1 item1, T2 item2)
		{
			this.m_Item1 = item1;
			this.m_Item2 = item2;
		}

		/// <summary>Returns a value that indicates whether the current <see cref="T:System.Tuple`2" /> object is equal to a specified object.</summary>
		/// <returns>true if the current instance is equal to the specified object; otherwise, false.</returns>
		/// <param name="obj">The object to compare with this instance.</param>
		// Token: 0x060008C9 RID: 2249 RVA: 0x0002EDF2 File Offset: 0x0002CFF2
		public override bool Equals(object obj)
		{
			return ((IStructuralEquatable)this).Equals(obj, ObjectEqualityComparer.Default);
		}

		/// <summary>Returns a value that indicates whether the current <see cref="T:System.Tuple`2" /> object is equal to a specified object based on a specified comparison method.</summary>
		/// <returns>true if the current instance is equal to the specified object; otherwise, false.</returns>
		/// <param name="other">The object to compare with this instance.</param>
		/// <param name="comparer">An object that defines the method to use to evaluate whether the two objects are equal.</param>
		// Token: 0x060008CA RID: 2250 RVA: 0x0002EF54 File Offset: 0x0002D154
		bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer)
		{
			if (other == null)
			{
				return false;
			}
			Tuple<T1, T2> tuple = other as Tuple<T1, T2>;
			return tuple != null && comparer.Equals(this.m_Item1, tuple.m_Item1) && comparer.Equals(this.m_Item2, tuple.m_Item2);
		}

		/// <summary>Compares the current <see cref="T:System.Tuple`2" /> object to a specified object and returns an integer that indicates whether the current object is before, after, or in the same position as the specified object in the sort order.</summary>
		/// <returns>A signed integer that indicates the relative position of this instance and <paramref name="obj" /> in the sort order, as shown in the following table.ValueDescriptionA negative integerThis instance precedes <paramref name="obj" />.ZeroThis instance and <paramref name="obj" /> have the same position in the sort order.A positive integerThis instance follows <paramref name="obj" />.</returns>
		/// <param name="obj">An object to compare with the current instance.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="obj" /> is not a <see cref="T:System.Tuple`2" /> object.</exception>
		// Token: 0x060008CB RID: 2251 RVA: 0x0002EE3A File Offset: 0x0002D03A
		int IComparable.CompareTo(object obj)
		{
			return ((IStructuralComparable)this).CompareTo(obj, LowLevelComparer.Default);
		}

		/// <summary>Compares the current <see cref="T:System.Tuple`2" /> object to a specified object by using a specified comparer, and returns an integer that indicates whether the current object is before, after, or in the same position as the specified object in the sort order.</summary>
		/// <returns>A signed integer that indicates the relative position of this instance and <paramref name="other" /> in the sort order, as shown in the following table.ValueDescriptionA negative integerThis instance precedes <paramref name="other" />.ZeroThis instance and <paramref name="other" /> have the same position in the sort order.A positive integerThis instance follows <paramref name="other" />.</returns>
		/// <param name="other">An object to compare with the current instance.</param>
		/// <param name="comparer">An object that provides custom rules for comparison.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="other" /> is not a <see cref="T:System.Tuple`2" /> object.</exception>
		// Token: 0x060008CC RID: 2252 RVA: 0x0002EFB0 File Offset: 0x0002D1B0
		int IStructuralComparable.CompareTo(object other, IComparer comparer)
		{
			if (other == null)
			{
				return 1;
			}
			Tuple<T1, T2> tuple = other as Tuple<T1, T2>;
			if (tuple == null)
			{
				throw new ArgumentException(SR.Format("Argument must be of type {0}.", base.GetType().ToString()), "other");
			}
			int num = comparer.Compare(this.m_Item1, tuple.m_Item1);
			if (num != 0)
			{
				return num;
			}
			return comparer.Compare(this.m_Item2, tuple.m_Item2);
		}

		/// <summary>Returns the hash code for the current <see cref="T:System.Tuple`2" /> object.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x060008CD RID: 2253 RVA: 0x0002EEA0 File Offset: 0x0002D0A0
		public override int GetHashCode()
		{
			return ((IStructuralEquatable)this).GetHashCode(ObjectEqualityComparer.Default);
		}

		/// <summary>Calculates the hash code for the current <see cref="T:System.Tuple`2" /> object by using a specified computation method.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		/// <param name="comparer">An object whose <see cref="M:System.Collections.IEqualityComparer.GetHashCode(System.Object)" />  method calculates the hash code of the current <see cref="T:System.Tuple`2" /> object.</param>
		// Token: 0x060008CE RID: 2254 RVA: 0x0002F02C File Offset: 0x0002D22C
		int IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
		{
			return Tuple.CombineHashCodes(comparer.GetHashCode(this.m_Item1), comparer.GetHashCode(this.m_Item2));
		}

		// Token: 0x060008CF RID: 2255 RVA: 0x0002EEC0 File Offset: 0x0002D0C0
		int ITupleInternal.GetHashCode(IEqualityComparer comparer)
		{
			return ((IStructuralEquatable)this).GetHashCode(comparer);
		}

		/// <summary>Returns a string that represents the value of this <see cref="T:System.Tuple`2" /> instance.</summary>
		/// <returns>The string representation of this <see cref="T:System.Tuple`2" /> object.</returns>
		// Token: 0x060008D0 RID: 2256 RVA: 0x0002F058 File Offset: 0x0002D258
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("(");
			return ((ITupleInternal)this).ToString(stringBuilder);
		}

		// Token: 0x060008D1 RID: 2257 RVA: 0x0002F080 File Offset: 0x0002D280
		string ITupleInternal.ToString(StringBuilder sb)
		{
			sb.Append(this.m_Item1);
			sb.Append(", ");
			sb.Append(this.m_Item2);
			sb.Append(')');
			return sb.ToString();
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x060008D2 RID: 2258 RVA: 0x0001EFC9 File Offset: 0x0001D1C9
		int ITuple.Length
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x17000153 RID: 339
		object ITuple.this[int index]
		{
			get
			{
				if (index == 0)
				{
					return this.Item1;
				}
				if (index != 1)
				{
					throw new IndexOutOfRangeException();
				}
				return this.Item2;
			}
		}

		// Token: 0x040006DA RID: 1754
		private readonly T1 m_Item1;

		// Token: 0x040006DB RID: 1755
		private readonly T2 m_Item2;
	}
}
