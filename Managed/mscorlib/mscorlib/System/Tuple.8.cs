using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace System
{
	/// <summary>Represents a 7-tuple, or septuple. </summary>
	/// <typeparam name="T1">The type of the tuple's first component.</typeparam>
	/// <typeparam name="T2">The type of the tuple's second component.</typeparam>
	/// <typeparam name="T3">The type of the tuple's third component.</typeparam>
	/// <typeparam name="T4">The type of the tuple's fourth component.</typeparam>
	/// <typeparam name="T5">The type of the tuple's fifth component.</typeparam>
	/// <typeparam name="T6">The type of the tuple's sixth component.</typeparam>
	/// <typeparam name="T7">The type of the tuple's seventh component.</typeparam>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000F0 RID: 240
	[Serializable]
	public class Tuple<T1, T2, T3, T4, T5, T6, T7> : IStructuralEquatable, IStructuralComparable, IComparable, ITupleInternal, ITuple
	{
		/// <summary>Gets the value of the current <see cref="T:System.Tuple`7" /> object's first component.</summary>
		/// <returns>The value of the current <see cref="T:System.Tuple`7" /> object's first component.</returns>
		// Token: 0x1700016E RID: 366
		// (get) Token: 0x06000916 RID: 2326 RVA: 0x0002FDFE File Offset: 0x0002DFFE
		public T1 Item1
		{
			get
			{
				return this.m_Item1;
			}
		}

		/// <summary>Gets the value of the current <see cref="T:System.Tuple`7" /> object's second component.</summary>
		/// <returns>The value of the current <see cref="T:System.Tuple`7" /> object's second component.</returns>
		// Token: 0x1700016F RID: 367
		// (get) Token: 0x06000917 RID: 2327 RVA: 0x0002FE06 File Offset: 0x0002E006
		public T2 Item2
		{
			get
			{
				return this.m_Item2;
			}
		}

		/// <summary>Gets the value of the current <see cref="T:System.Tuple`7" /> object's third component.</summary>
		/// <returns>The value of the current <see cref="T:System.Tuple`7" /> object's third component.</returns>
		// Token: 0x17000170 RID: 368
		// (get) Token: 0x06000918 RID: 2328 RVA: 0x0002FE0E File Offset: 0x0002E00E
		public T3 Item3
		{
			get
			{
				return this.m_Item3;
			}
		}

		/// <summary>Gets the value of the current <see cref="T:System.Tuple`7" /> object's fourth component.</summary>
		/// <returns>The value of the current <see cref="T:System.Tuple`7" /> object's fourth component.</returns>
		// Token: 0x17000171 RID: 369
		// (get) Token: 0x06000919 RID: 2329 RVA: 0x0002FE16 File Offset: 0x0002E016
		public T4 Item4
		{
			get
			{
				return this.m_Item4;
			}
		}

		/// <summary>Gets the value of the current <see cref="T:System.Tuple`7" /> object's fifth component.</summary>
		/// <returns>The value of the current <see cref="T:System.Tuple`7" /> object's fifth component.</returns>
		// Token: 0x17000172 RID: 370
		// (get) Token: 0x0600091A RID: 2330 RVA: 0x0002FE1E File Offset: 0x0002E01E
		public T5 Item5
		{
			get
			{
				return this.m_Item5;
			}
		}

		/// <summary>Gets the value of the current <see cref="T:System.Tuple`7" /> object's sixth component.</summary>
		/// <returns>The value of the current <see cref="T:System.Tuple`7" /> object's sixth component.</returns>
		// Token: 0x17000173 RID: 371
		// (get) Token: 0x0600091B RID: 2331 RVA: 0x0002FE26 File Offset: 0x0002E026
		public T6 Item6
		{
			get
			{
				return this.m_Item6;
			}
		}

		/// <summary>Gets the value of the current <see cref="T:System.Tuple`7" /> object's seventh component.</summary>
		/// <returns>The value of the current <see cref="T:System.Tuple`7" /> object's seventh component.</returns>
		// Token: 0x17000174 RID: 372
		// (get) Token: 0x0600091C RID: 2332 RVA: 0x0002FE2E File Offset: 0x0002E02E
		public T7 Item7
		{
			get
			{
				return this.m_Item7;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Tuple`7" /> class.</summary>
		/// <param name="item1">The value of the tuple's first component.</param>
		/// <param name="item2">The value of the tuple's second component.</param>
		/// <param name="item3">The value of the tuple's third component.</param>
		/// <param name="item4">The value of the tuple's fourth component</param>
		/// <param name="item5">The value of the tuple's fifth component.</param>
		/// <param name="item6">The value of the tuple's sixth component.</param>
		/// <param name="item7">The value of the tuple's seventh component.</param>
		// Token: 0x0600091D RID: 2333 RVA: 0x0002FE36 File Offset: 0x0002E036
		public Tuple(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7)
		{
			this.m_Item1 = item1;
			this.m_Item2 = item2;
			this.m_Item3 = item3;
			this.m_Item4 = item4;
			this.m_Item5 = item5;
			this.m_Item6 = item6;
			this.m_Item7 = item7;
		}

		/// <summary>Returns a value that indicates whether the current <see cref="T:System.Tuple`7" /> object is equal to a specified object.</summary>
		/// <returns>true if the current instance is equal to the specified object; otherwise, false.</returns>
		/// <param name="obj">The object to compare with this instance.</param>
		// Token: 0x0600091E RID: 2334 RVA: 0x0002EDF2 File Offset: 0x0002CFF2
		public override bool Equals(object obj)
		{
			return ((IStructuralEquatable)this).Equals(obj, ObjectEqualityComparer.Default);
		}

		/// <summary>Returns a value that indicates whether the current <see cref="T:System.Tuple`7" /> object is equal to a specified object based on a specified comparison method.</summary>
		/// <returns>true if the current instance is equal to the specified object; otherwise, false.</returns>
		/// <param name="other">The object to compare with this instance.</param>
		/// <param name="comparer">An object that defines the method to use to evaluate whether the two objects are equal.</param>
		// Token: 0x0600091F RID: 2335 RVA: 0x0002FE74 File Offset: 0x0002E074
		bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer)
		{
			if (other == null)
			{
				return false;
			}
			Tuple<T1, T2, T3, T4, T5, T6, T7> tuple = other as Tuple<T1, T2, T3, T4, T5, T6, T7>;
			return tuple != null && (comparer.Equals(this.m_Item1, tuple.m_Item1) && comparer.Equals(this.m_Item2, tuple.m_Item2) && comparer.Equals(this.m_Item3, tuple.m_Item3) && comparer.Equals(this.m_Item4, tuple.m_Item4) && comparer.Equals(this.m_Item5, tuple.m_Item5) && comparer.Equals(this.m_Item6, tuple.m_Item6)) && comparer.Equals(this.m_Item7, tuple.m_Item7);
		}

		/// <summary>Compares the current <see cref="T:System.Tuple`7" /> object to a specified object and returns an integer that indicates whether the current object is before, after, or in the same position as the specified object in the sort order.</summary>
		/// <returns>A signed integer that indicates the relative position of this instance and <paramref name="obj" /> in the sort order, as shown in the following table.ValueDescriptionA negative integerThis instance precedes <paramref name="obj" />.ZeroThis instance and <paramref name="obj" /> have the same position in the sort order.A positive integerThis instance follows <paramref name="obj" />.</returns>
		/// <param name="obj">An object to compare with the current instance.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="obj" /> is not a <see cref="T:System.Tuple`7" /> object.</exception>
		// Token: 0x06000920 RID: 2336 RVA: 0x0002EE3A File Offset: 0x0002D03A
		int IComparable.CompareTo(object obj)
		{
			return ((IStructuralComparable)this).CompareTo(obj, LowLevelComparer.Default);
		}

		/// <summary>Compares the current <see cref="T:System.Tuple`7" /> object to a specified object by using a specified comparer, and returns an integer that indicates whether the current object is before, after, or in the same position as the specified object in the sort order.</summary>
		/// <returns>A signed integer that indicates the relative position of this instance and <paramref name="other" /> in the sort order, as shown in the following table.ValueDescriptionA negative integerThis instance precedes <paramref name="other" />.ZeroThis instance and <paramref name="other" /> have the same position in the sort order.A positive integerThis instance follows <paramref name="other" />.</returns>
		/// <param name="other">An object to compare with the current instance.</param>
		/// <param name="comparer">An object that provides custom rules for comparison.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="other" /> is not a <see cref="T:System.Tuple`7" /> object.</exception>
		// Token: 0x06000921 RID: 2337 RVA: 0x0002FF6C File Offset: 0x0002E16C
		int IStructuralComparable.CompareTo(object other, IComparer comparer)
		{
			if (other == null)
			{
				return 1;
			}
			Tuple<T1, T2, T3, T4, T5, T6, T7> tuple = other as Tuple<T1, T2, T3, T4, T5, T6, T7>;
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
			return comparer.Compare(this.m_Item7, tuple.m_Item7);
		}

		/// <summary>Returns the hash code for the current <see cref="T:System.Tuple`7" /> object.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06000922 RID: 2338 RVA: 0x0002EEA0 File Offset: 0x0002D0A0
		public override int GetHashCode()
		{
			return ((IStructuralEquatable)this).GetHashCode(ObjectEqualityComparer.Default);
		}

		/// <summary>Calculates the hash code for the current <see cref="T:System.Tuple`7" /> object by using a specified computation method.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		/// <param name="comparer">An object whose <see cref="M:System.Collections.IEqualityComparer.GetHashCode(System.Object)" /> method calculates the hash code of the current <see cref="T:System.Tuple`7" /> object.</param>
		// Token: 0x06000923 RID: 2339 RVA: 0x00030094 File Offset: 0x0002E294
		int IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
		{
			return Tuple.CombineHashCodes(comparer.GetHashCode(this.m_Item1), comparer.GetHashCode(this.m_Item2), comparer.GetHashCode(this.m_Item3), comparer.GetHashCode(this.m_Item4), comparer.GetHashCode(this.m_Item5), comparer.GetHashCode(this.m_Item6), comparer.GetHashCode(this.m_Item7));
		}

		// Token: 0x06000924 RID: 2340 RVA: 0x0002EEC0 File Offset: 0x0002D0C0
		int ITupleInternal.GetHashCode(IEqualityComparer comparer)
		{
			return ((IStructuralEquatable)this).GetHashCode(comparer);
		}

		/// <summary>Returns a string that represents the value of this <see cref="T:System.Tuple`7" /> instance.</summary>
		/// <returns>The string representation of this <see cref="T:System.Tuple`7" /> object.</returns>
		// Token: 0x06000925 RID: 2341 RVA: 0x00030120 File Offset: 0x0002E320
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("(");
			return ((ITupleInternal)this).ToString(stringBuilder);
		}

		// Token: 0x06000926 RID: 2342 RVA: 0x00030148 File Offset: 0x0002E348
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
			sb.Append(')');
			return sb.ToString();
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06000927 RID: 2343 RVA: 0x0002A81F File Offset: 0x00028A1F
		int ITuple.Length
		{
			get
			{
				return 7;
			}
		}

		// Token: 0x17000176 RID: 374
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
					throw new IndexOutOfRangeException();
				}
			}
		}

		// Token: 0x040006EE RID: 1774
		private readonly T1 m_Item1;

		// Token: 0x040006EF RID: 1775
		private readonly T2 m_Item2;

		// Token: 0x040006F0 RID: 1776
		private readonly T3 m_Item3;

		// Token: 0x040006F1 RID: 1777
		private readonly T4 m_Item4;

		// Token: 0x040006F2 RID: 1778
		private readonly T5 m_Item5;

		// Token: 0x040006F3 RID: 1779
		private readonly T6 m_Item6;

		// Token: 0x040006F4 RID: 1780
		private readonly T7 m_Item7;
	}
}
