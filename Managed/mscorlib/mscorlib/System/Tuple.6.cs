using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace System
{
	/// <summary>Represents a 5-tuple, or quintuple. </summary>
	/// <typeparam name="T1">The type of the tuple's first component.</typeparam>
	/// <typeparam name="T2">The type of the tuple's second component.</typeparam>
	/// <typeparam name="T3">The type of the tuple's third component.</typeparam>
	/// <typeparam name="T4">The type of the tuple's fourth component.</typeparam>
	/// <typeparam name="T5">The type of the tuple's fifth component.</typeparam>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000EE RID: 238
	[Serializable]
	public class Tuple<T1, T2, T3, T4, T5> : IStructuralEquatable, IStructuralComparable, IComparable, ITupleInternal, ITuple
	{
		/// <summary>Gets the value of the current <see cref="T:System.Tuple`5" /> object's first component.</summary>
		/// <returns>The value of the current <see cref="T:System.Tuple`5" /> object's first component.</returns>
		// Token: 0x1700015F RID: 351
		// (get) Token: 0x060008F3 RID: 2291 RVA: 0x0002F64E File Offset: 0x0002D84E
		public T1 Item1
		{
			get
			{
				return this.m_Item1;
			}
		}

		/// <summary>Gets the value of the current <see cref="T:System.Tuple`5" /> object's second component.</summary>
		/// <returns>The value of the current <see cref="T:System.Tuple`5" /> object's second component.</returns>
		// Token: 0x17000160 RID: 352
		// (get) Token: 0x060008F4 RID: 2292 RVA: 0x0002F656 File Offset: 0x0002D856
		public T2 Item2
		{
			get
			{
				return this.m_Item2;
			}
		}

		/// <summary>Gets the value of the current <see cref="T:System.Tuple`5" /> object's third component.</summary>
		/// <returns>The value of the current <see cref="T:System.Tuple`5" /> object's third component.</returns>
		// Token: 0x17000161 RID: 353
		// (get) Token: 0x060008F5 RID: 2293 RVA: 0x0002F65E File Offset: 0x0002D85E
		public T3 Item3
		{
			get
			{
				return this.m_Item3;
			}
		}

		/// <summary>Gets the value of the current <see cref="T:System.Tuple`5" /> object's fourth component.</summary>
		/// <returns>The value of the current <see cref="T:System.Tuple`5" /> object's fourth component.</returns>
		// Token: 0x17000162 RID: 354
		// (get) Token: 0x060008F6 RID: 2294 RVA: 0x0002F666 File Offset: 0x0002D866
		public T4 Item4
		{
			get
			{
				return this.m_Item4;
			}
		}

		/// <summary>Gets the value of the current <see cref="T:System.Tuple`5" /> object's fifth component.</summary>
		/// <returns>The value of the current <see cref="T:System.Tuple`5" /> object's fifth component.</returns>
		// Token: 0x17000163 RID: 355
		// (get) Token: 0x060008F7 RID: 2295 RVA: 0x0002F66E File Offset: 0x0002D86E
		public T5 Item5
		{
			get
			{
				return this.m_Item5;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Tuple`5" /> class.</summary>
		/// <param name="item1">The value of the tuple's first component.</param>
		/// <param name="item2">The value of the tuple's second component.</param>
		/// <param name="item3">The value of the tuple's third component.</param>
		/// <param name="item4">The value of the tuple's fourth component</param>
		/// <param name="item5">The value of the tuple's fifth component.</param>
		// Token: 0x060008F8 RID: 2296 RVA: 0x0002F676 File Offset: 0x0002D876
		public Tuple(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5)
		{
			this.m_Item1 = item1;
			this.m_Item2 = item2;
			this.m_Item3 = item3;
			this.m_Item4 = item4;
			this.m_Item5 = item5;
		}

		/// <summary>Returns a value that indicates whether the current <see cref="T:System.Tuple`5" /> object is equal to a specified object.</summary>
		/// <returns>true if the current instance is equal to the specified object; otherwise, false.</returns>
		/// <param name="obj">The object to compare with this instance.</param>
		// Token: 0x060008F9 RID: 2297 RVA: 0x0002EDF2 File Offset: 0x0002CFF2
		public override bool Equals(object obj)
		{
			return ((IStructuralEquatable)this).Equals(obj, ObjectEqualityComparer.Default);
		}

		/// <summary>Returns a value that indicates whether the current <see cref="T:System.Tuple`5" /> object is equal to a specified object based on a specified comparison method.</summary>
		/// <returns>true if the current instance is equal to the specified object; otherwise, false.</returns>
		/// <param name="other">The object to compare with this instance.</param>
		/// <param name="comparer">An object that defines the method to use to evaluate whether the two objects are equal.</param>
		// Token: 0x060008FA RID: 2298 RVA: 0x0002F6A4 File Offset: 0x0002D8A4
		bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer)
		{
			if (other == null)
			{
				return false;
			}
			Tuple<T1, T2, T3, T4, T5> tuple = other as Tuple<T1, T2, T3, T4, T5>;
			return tuple != null && (comparer.Equals(this.m_Item1, tuple.m_Item1) && comparer.Equals(this.m_Item2, tuple.m_Item2) && comparer.Equals(this.m_Item3, tuple.m_Item3) && comparer.Equals(this.m_Item4, tuple.m_Item4)) && comparer.Equals(this.m_Item5, tuple.m_Item5);
		}

		/// <summary>Compares the current <see cref="T:System.Tuple`5" /> object to a specified object and returns an integer that indicates whether the current object is before, after, or in the same position as the specified object in the sort order.</summary>
		/// <returns>A signed integer that indicates the relative position of this instance and <paramref name="obj" /> in the sort order, as shown in the following table.ValueDescriptionA negative integerThis instance precedes <paramref name="obj" />.ZeroThis instance and <paramref name="obj" /> have the same position in the sort order.A positive integerThis instance follows <paramref name="obj" />.</returns>
		/// <param name="obj">An object to compare with the current instance.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="obj" /> is not a <see cref="T:System.Tuple`5" /> object.</exception>
		// Token: 0x060008FB RID: 2299 RVA: 0x0002EE3A File Offset: 0x0002D03A
		int IComparable.CompareTo(object obj)
		{
			return ((IStructuralComparable)this).CompareTo(obj, LowLevelComparer.Default);
		}

		/// <summary>Compares the current <see cref="T:System.Tuple`5" /> object to a specified object by using a specified comparer and returns an integer that indicates whether the current object is before, after, or in the same position as the specified object in the sort order.</summary>
		/// <returns>A signed integer that indicates the relative position of this instance and <paramref name="other" /> in the sort order, as shown in the following table.ValueDescriptionA negative integerThis instance precedes <paramref name="other" />.ZeroThis instance and <paramref name="other" /> have the same position in the sort order.A positive integerThis instance follows <paramref name="other" />.</returns>
		/// <param name="other">An object to compare with the current instance.</param>
		/// <param name="comparer">An object that provides custom rules for comparison.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="other" /> is not a <see cref="T:System.Tuple`5" /> object.</exception>
		// Token: 0x060008FC RID: 2300 RVA: 0x0002F758 File Offset: 0x0002D958
		int IStructuralComparable.CompareTo(object other, IComparer comparer)
		{
			if (other == null)
			{
				return 1;
			}
			Tuple<T1, T2, T3, T4, T5> tuple = other as Tuple<T1, T2, T3, T4, T5>;
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
			return comparer.Compare(this.m_Item5, tuple.m_Item5);
		}

		/// <summary>Returns the hash code for the current <see cref="T:System.Tuple`5" /> object.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x060008FD RID: 2301 RVA: 0x0002EEA0 File Offset: 0x0002D0A0
		public override int GetHashCode()
		{
			return ((IStructuralEquatable)this).GetHashCode(ObjectEqualityComparer.Default);
		}

		/// <summary>Calculates the hash code for the current <see cref="T:System.Tuple`5" /> object by using a specified computation method.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		/// <param name="comparer">An object whose <see cref="M:System.Collections.IEqualityComparer.GetHashCode(System.Object)" />  method calculates the hash code of the current <see cref="T:System.Tuple`5" /> object.</param>
		// Token: 0x060008FE RID: 2302 RVA: 0x0002F83C File Offset: 0x0002DA3C
		int IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
		{
			return Tuple.CombineHashCodes(comparer.GetHashCode(this.m_Item1), comparer.GetHashCode(this.m_Item2), comparer.GetHashCode(this.m_Item3), comparer.GetHashCode(this.m_Item4), comparer.GetHashCode(this.m_Item5));
		}

		// Token: 0x060008FF RID: 2303 RVA: 0x0002EEC0 File Offset: 0x0002D0C0
		int ITupleInternal.GetHashCode(IEqualityComparer comparer)
		{
			return ((IStructuralEquatable)this).GetHashCode(comparer);
		}

		/// <summary>Returns a string that represents the value of this <see cref="T:System.Tuple`5" /> instance.</summary>
		/// <returns>The string representation of this <see cref="T:System.Tuple`5" /> object.</returns>
		// Token: 0x06000900 RID: 2304 RVA: 0x0002F8A4 File Offset: 0x0002DAA4
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("(");
			return ((ITupleInternal)this).ToString(stringBuilder);
		}

		// Token: 0x06000901 RID: 2305 RVA: 0x0002F8CC File Offset: 0x0002DACC
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
			sb.Append(')');
			return sb.ToString();
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x06000902 RID: 2306 RVA: 0x000293E5 File Offset: 0x000275E5
		int ITuple.Length
		{
			get
			{
				return 5;
			}
		}

		// Token: 0x17000165 RID: 357
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
				default:
					throw new IndexOutOfRangeException();
				}
			}
		}

		// Token: 0x040006E3 RID: 1763
		private readonly T1 m_Item1;

		// Token: 0x040006E4 RID: 1764
		private readonly T2 m_Item2;

		// Token: 0x040006E5 RID: 1765
		private readonly T3 m_Item3;

		// Token: 0x040006E6 RID: 1766
		private readonly T4 m_Item4;

		// Token: 0x040006E7 RID: 1767
		private readonly T5 m_Item5;
	}
}
