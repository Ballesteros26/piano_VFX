using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace System
{
	/// <summary>Represents an n-tuple, where n is 8 or greater.</summary>
	/// <typeparam name="T1">The type of the tuple's first component.</typeparam>
	/// <typeparam name="T2">The type of the tuple's second component.</typeparam>
	/// <typeparam name="T3">The type of the tuple's third component.</typeparam>
	/// <typeparam name="T4">The type of the tuple's fourth component.</typeparam>
	/// <typeparam name="T5">The type of the tuple's fifth component.</typeparam>
	/// <typeparam name="T6">The type of the tuple's sixth component.</typeparam>
	/// <typeparam name="T7">The type of the tuple's seventh component.</typeparam>
	/// <typeparam name="TRest">Any generic Tuple object that defines the types of the tuple's remaining components.</typeparam>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000F1 RID: 241
	[Serializable]
	public class Tuple<T1, T2, T3, T4, T5, T6, T7, TRest> : IStructuralEquatable, IStructuralComparable, IComparable, ITupleInternal, ITuple
	{
		/// <summary>Gets the value of the current <see cref="T:System.Tuple`8" /> object's first component.</summary>
		/// <returns>The value of the current <see cref="T:System.Tuple`8" /> object's first component.</returns>
		// Token: 0x17000177 RID: 375
		// (get) Token: 0x06000929 RID: 2345 RVA: 0x000302B6 File Offset: 0x0002E4B6
		public T1 Item1
		{
			get
			{
				return this.m_Item1;
			}
		}

		/// <summary>Gets the value of the current <see cref="T:System.Tuple`8" /> object's second component.</summary>
		/// <returns>The value of the current <see cref="T:System.Tuple`8" /> object's second component.</returns>
		// Token: 0x17000178 RID: 376
		// (get) Token: 0x0600092A RID: 2346 RVA: 0x000302BE File Offset: 0x0002E4BE
		public T2 Item2
		{
			get
			{
				return this.m_Item2;
			}
		}

		/// <summary>Gets the value of the current <see cref="T:System.Tuple`8" /> object's third component.</summary>
		/// <returns>The value of the current <see cref="T:System.Tuple`8" /> object's third component.</returns>
		// Token: 0x17000179 RID: 377
		// (get) Token: 0x0600092B RID: 2347 RVA: 0x000302C6 File Offset: 0x0002E4C6
		public T3 Item3
		{
			get
			{
				return this.m_Item3;
			}
		}

		/// <summary>Gets the value of the current <see cref="T:System.Tuple`8" /> object's fourth component.</summary>
		/// <returns>The value of the current <see cref="T:System.Tuple`8" /> object's fourth component.</returns>
		// Token: 0x1700017A RID: 378
		// (get) Token: 0x0600092C RID: 2348 RVA: 0x000302CE File Offset: 0x0002E4CE
		public T4 Item4
		{
			get
			{
				return this.m_Item4;
			}
		}

		/// <summary>Gets the value of the current <see cref="T:System.Tuple`8" /> object's fifth component.</summary>
		/// <returns>The value of the current <see cref="T:System.Tuple`8" /> object's fifth component.</returns>
		// Token: 0x1700017B RID: 379
		// (get) Token: 0x0600092D RID: 2349 RVA: 0x000302D6 File Offset: 0x0002E4D6
		public T5 Item5
		{
			get
			{
				return this.m_Item5;
			}
		}

		/// <summary>Gets the value of the current <see cref="T:System.Tuple`8" /> object's sixth component.</summary>
		/// <returns>The value of the current <see cref="T:System.Tuple`8" /> object's sixth component.</returns>
		// Token: 0x1700017C RID: 380
		// (get) Token: 0x0600092E RID: 2350 RVA: 0x000302DE File Offset: 0x0002E4DE
		public T6 Item6
		{
			get
			{
				return this.m_Item6;
			}
		}

		/// <summary>Gets the value of the current <see cref="T:System.Tuple`8" /> object's seventh component.</summary>
		/// <returns>The value of the current <see cref="T:System.Tuple`8" /> object's seventh component.</returns>
		// Token: 0x1700017D RID: 381
		// (get) Token: 0x0600092F RID: 2351 RVA: 0x000302E6 File Offset: 0x0002E4E6
		public T7 Item7
		{
			get
			{
				return this.m_Item7;
			}
		}

		/// <summary>Gets the current <see cref="T:System.Tuple`8" /> object's remaining components.</summary>
		/// <returns>The value of the current <see cref="T:System.Tuple`8" /> object's remaining components.</returns>
		// Token: 0x1700017E RID: 382
		// (get) Token: 0x06000930 RID: 2352 RVA: 0x000302EE File Offset: 0x0002E4EE
		public TRest Rest
		{
			get
			{
				return this.m_Rest;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Tuple`8" /> class.</summary>
		/// <param name="item1">The value of the tuple's first component.</param>
		/// <param name="item2">The value of the tuple's second component.</param>
		/// <param name="item3">The value of the tuple's third component.</param>
		/// <param name="item4">The value of the tuple's fourth component</param>
		/// <param name="item5">The value of the tuple's fifth component.</param>
		/// <param name="item6">The value of the tuple's sixth component.</param>
		/// <param name="item7">The value of the tuple's seventh component.</param>
		/// <param name="rest">Any generic Tuple object that contains the values of the tuple's remaining components.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="rest" /> is not a generic Tuple object.</exception>
		// Token: 0x06000931 RID: 2353 RVA: 0x000302F8 File Offset: 0x0002E4F8
		public Tuple(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, TRest rest)
		{
			if (!(rest is ITupleInternal))
			{
				throw new ArgumentException("The last element of an eight element tuple must be a Tuple.");
			}
			this.m_Item1 = item1;
			this.m_Item2 = item2;
			this.m_Item3 = item3;
			this.m_Item4 = item4;
			this.m_Item5 = item5;
			this.m_Item6 = item6;
			this.m_Item7 = item7;
			this.m_Rest = rest;
		}

		/// <summary>Returns a value that indicates whether the current <see cref="T:System.Tuple`8" /> object is equal to a specified object.</summary>
		/// <returns>true if the current instance is equal to the specified object; otherwise, false.</returns>
		/// <param name="obj">The object to compare with this instance.</param>
		// Token: 0x06000932 RID: 2354 RVA: 0x0002EDF2 File Offset: 0x0002CFF2
		public override bool Equals(object obj)
		{
			return ((IStructuralEquatable)this).Equals(obj, ObjectEqualityComparer.Default);
		}

		/// <summary>Returns a value that indicates whether the current <see cref="T:System.Tuple`8" /> object is equal to a specified object based on a specified comparison method.</summary>
		/// <returns>true if the current instance is equal to the specified object; otherwise, false.</returns>
		/// <param name="other">The object to compare with this instance.</param>
		/// <param name="comparer">An object that defines the method to use to evaluate whether the two objects are equal.</param>
		// Token: 0x06000933 RID: 2355 RVA: 0x00030364 File Offset: 0x0002E564
		bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer)
		{
			if (other == null)
			{
				return false;
			}
			Tuple<T1, T2, T3, T4, T5, T6, T7, TRest> tuple = other as Tuple<T1, T2, T3, T4, T5, T6, T7, TRest>;
			return tuple != null && (comparer.Equals(this.m_Item1, tuple.m_Item1) && comparer.Equals(this.m_Item2, tuple.m_Item2) && comparer.Equals(this.m_Item3, tuple.m_Item3) && comparer.Equals(this.m_Item4, tuple.m_Item4) && comparer.Equals(this.m_Item5, tuple.m_Item5) && comparer.Equals(this.m_Item6, tuple.m_Item6) && comparer.Equals(this.m_Item7, tuple.m_Item7)) && comparer.Equals(this.m_Rest, tuple.m_Rest);
		}

		/// <summary>Compares the current <see cref="T:System.Tuple`8" /> object to a specified object and returns an integer that indicates whether the current object is before, after, or in the same position as the specified object in the sort order.</summary>
		/// <returns>A signed integer that indicates the relative position of this instance and <paramref name="obj" /> in the sort order, as shown in the following table.ValueDescriptionA negative integerThis instance precedes <paramref name="obj" />.ZeroThis instance and <paramref name="obj" /> have the same position in the sort order.A positive integerThis instance follows <paramref name="obj" />.</returns>
		/// <param name="obj">An object to compare with the current instance.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="obj" /> is not a <see cref="T:System.Tuple`8" /> object.</exception>
		// Token: 0x06000934 RID: 2356 RVA: 0x0002EE3A File Offset: 0x0002D03A
		int IComparable.CompareTo(object obj)
		{
			return ((IStructuralComparable)this).CompareTo(obj, LowLevelComparer.Default);
		}

		/// <summary>Compares the current <see cref="T:System.Tuple`8" /> object to a specified object by using a specified comparer and returns an integer that indicates whether the current object is before, after, or in the same position as the specified object in the sort order.</summary>
		/// <returns>A signed integer that indicates the relative position of this instance and <paramref name="other" /> in the sort order, as shown in the following table.ValueDescriptionA negative integerThis instance precedes <paramref name="other" />.ZeroThis instance and <paramref name="other" /> have the same position in the sort order.A positive integerThis instance follows <paramref name="other" />.</returns>
		/// <param name="other">An object to compare with the current instance.</param>
		/// <param name="comparer">An object that provides custom rules for comparison.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="other" /> is not a <see cref="T:System.Tuple`8" /> object.</exception>
		// Token: 0x06000935 RID: 2357 RVA: 0x0003047C File Offset: 0x0002E67C
		int IStructuralComparable.CompareTo(object other, IComparer comparer)
		{
			if (other == null)
			{
				return 1;
			}
			Tuple<T1, T2, T3, T4, T5, T6, T7, TRest> tuple = other as Tuple<T1, T2, T3, T4, T5, T6, T7, TRest>;
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
			num = comparer.Compare(this.m_Item6, tuple.m_Item6);
			if (num != 0)
			{
				return num;
			}
			num = comparer.Compare(this.m_Item7, tuple.m_Item7);
			if (num != 0)
			{
				return num;
			}
			return comparer.Compare(this.m_Rest, tuple.m_Rest);
		}

		/// <summary>Calculates the hash code for the current <see cref="T:System.Tuple`8" /> object.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06000936 RID: 2358 RVA: 0x0002EEA0 File Offset: 0x0002D0A0
		public override int GetHashCode()
		{
			return ((IStructuralEquatable)this).GetHashCode(ObjectEqualityComparer.Default);
		}

		/// <summary>Calculates the hash code for the current <see cref="T:System.Tuple`8" /> object by using a specified computation method.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		/// <param name="comparer">An object whose <see cref="M:System.Collections.IEqualityComparer.GetHashCode(System.Object)" />  method calculates the hash code of the current <see cref="T:System.Tuple`8" /> object.</param>
		// Token: 0x06000937 RID: 2359 RVA: 0x000305C4 File Offset: 0x0002E7C4
		int IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
		{
			ITupleInternal tupleInternal = (ITupleInternal)((object)this.m_Rest);
			if (tupleInternal.Length >= 8)
			{
				return tupleInternal.GetHashCode(comparer);
			}
			switch (8 - tupleInternal.Length)
			{
			case 1:
				return Tuple.CombineHashCodes(comparer.GetHashCode(this.m_Item7), tupleInternal.GetHashCode(comparer));
			case 2:
				return Tuple.CombineHashCodes(comparer.GetHashCode(this.m_Item6), comparer.GetHashCode(this.m_Item7), tupleInternal.GetHashCode(comparer));
			case 3:
				return Tuple.CombineHashCodes(comparer.GetHashCode(this.m_Item5), comparer.GetHashCode(this.m_Item6), comparer.GetHashCode(this.m_Item7), tupleInternal.GetHashCode(comparer));
			case 4:
				return Tuple.CombineHashCodes(comparer.GetHashCode(this.m_Item4), comparer.GetHashCode(this.m_Item5), comparer.GetHashCode(this.m_Item6), comparer.GetHashCode(this.m_Item7), tupleInternal.GetHashCode(comparer));
			case 5:
				return Tuple.CombineHashCodes(comparer.GetHashCode(this.m_Item3), comparer.GetHashCode(this.m_Item4), comparer.GetHashCode(this.m_Item5), comparer.GetHashCode(this.m_Item6), comparer.GetHashCode(this.m_Item7), tupleInternal.GetHashCode(comparer));
			case 6:
				return Tuple.CombineHashCodes(comparer.GetHashCode(this.m_Item2), comparer.GetHashCode(this.m_Item3), comparer.GetHashCode(this.m_Item4), comparer.GetHashCode(this.m_Item5), comparer.GetHashCode(this.m_Item6), comparer.GetHashCode(this.m_Item7), tupleInternal.GetHashCode(comparer));
			case 7:
				return Tuple.CombineHashCodes(comparer.GetHashCode(this.m_Item1), comparer.GetHashCode(this.m_Item2), comparer.GetHashCode(this.m_Item3), comparer.GetHashCode(this.m_Item4), comparer.GetHashCode(this.m_Item5), comparer.GetHashCode(this.m_Item6), comparer.GetHashCode(this.m_Item7), tupleInternal.GetHashCode(comparer));
			default:
				return -1;
			}
		}

		// Token: 0x06000938 RID: 2360 RVA: 0x0002EEC0 File Offset: 0x0002D0C0
		int ITupleInternal.GetHashCode(IEqualityComparer comparer)
		{
			return ((IStructuralEquatable)this).GetHashCode(comparer);
		}

		/// <summary>Returns a string that represents the value of this <see cref="T:System.Tuple`8" /> instance.</summary>
		/// <returns>The string representation of this <see cref="T:System.Tuple`8" /> object.</returns>
		// Token: 0x06000939 RID: 2361 RVA: 0x00030860 File Offset: 0x0002EA60
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("(");
			return ((ITupleInternal)this).ToString(stringBuilder);
		}

		// Token: 0x0600093A RID: 2362 RVA: 0x00030888 File Offset: 0x0002EA88
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
			sb.Append(", ");
			sb.Append(this.m_Item7);
			sb.Append(", ");
			return ((ITupleInternal)((object)this.m_Rest)).ToString(sb);
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x0600093B RID: 2363 RVA: 0x0003097D File Offset: 0x0002EB7D
		int ITuple.Length
		{
			get
			{
				return 7 + ((ITupleInternal)((object)this.Rest)).Length;
			}
		}

		// Token: 0x17000180 RID: 384
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
				case 6:
					return this.Item7;
				default:
					return ((ITupleInternal)((object)this.Rest))[index - 7];
				}
			}
		}

		// Token: 0x040006F5 RID: 1781
		private readonly T1 m_Item1;

		// Token: 0x040006F6 RID: 1782
		private readonly T2 m_Item2;

		// Token: 0x040006F7 RID: 1783
		private readonly T3 m_Item3;

		// Token: 0x040006F8 RID: 1784
		private readonly T4 m_Item4;

		// Token: 0x040006F9 RID: 1785
		private readonly T5 m_Item5;

		// Token: 0x040006FA RID: 1786
		private readonly T6 m_Item6;

		// Token: 0x040006FB RID: 1787
		private readonly T7 m_Item7;

		// Token: 0x040006FC RID: 1788
		private readonly TRest m_Rest;
	}
}
