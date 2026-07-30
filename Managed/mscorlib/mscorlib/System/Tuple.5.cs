using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace System
{
	/// <summary>Represents a 4-tuple, or quadruple. </summary>
	/// <typeparam name="T1">The type of the tuple's first component.</typeparam>
	/// <typeparam name="T2">The type of the tuple's second component.</typeparam>
	/// <typeparam name="T3">The type of the tuple's third component.</typeparam>
	/// <typeparam name="T4">The type of the tuple's fourth component.</typeparam>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000ED RID: 237
	[Serializable]
	public class Tuple<T1, T2, T3, T4> : IStructuralEquatable, IStructuralComparable, IComparable, ITupleInternal, ITuple
	{
		/// <summary>Gets the value of the current <see cref="T:System.Tuple`4" /> object's first component.</summary>
		/// <returns>The value of the current <see cref="T:System.Tuple`4" /> object's first component.</returns>
		// Token: 0x17000159 RID: 345
		// (get) Token: 0x060008E3 RID: 2275 RVA: 0x0002F34D File Offset: 0x0002D54D
		public T1 Item1
		{
			get
			{
				return this.m_Item1;
			}
		}

		/// <summary>Gets the value of the current <see cref="T:System.Tuple`4" /> object's second component.</summary>
		/// <returns>The value of the current <see cref="T:System.Tuple`4" /> object's second component.</returns>
		// Token: 0x1700015A RID: 346
		// (get) Token: 0x060008E4 RID: 2276 RVA: 0x0002F355 File Offset: 0x0002D555
		public T2 Item2
		{
			get
			{
				return this.m_Item2;
			}
		}

		/// <summary>Gets the value of the current <see cref="T:System.Tuple`4" /> object's third component.</summary>
		/// <returns>The value of the current <see cref="T:System.Tuple`4" /> object's third component.</returns>
		// Token: 0x1700015B RID: 347
		// (get) Token: 0x060008E5 RID: 2277 RVA: 0x0002F35D File Offset: 0x0002D55D
		public T3 Item3
		{
			get
			{
				return this.m_Item3;
			}
		}

		/// <summary>Gets the value of the current <see cref="T:System.Tuple`4" /> object's fourth component.</summary>
		/// <returns>The value of the current <see cref="T:System.Tuple`4" /> object's fourth component.</returns>
		// Token: 0x1700015C RID: 348
		// (get) Token: 0x060008E6 RID: 2278 RVA: 0x0002F365 File Offset: 0x0002D565
		public T4 Item4
		{
			get
			{
				return this.m_Item4;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Tuple`4" /> class.</summary>
		/// <param name="item1">The value of the tuple's first component.</param>
		/// <param name="item2">The value of the tuple's second component.</param>
		/// <param name="item3">The value of the tuple's third component.</param>
		/// <param name="item4">The value of the tuple's fourth component</param>
		// Token: 0x060008E7 RID: 2279 RVA: 0x0002F36D File Offset: 0x0002D56D
		public Tuple(T1 item1, T2 item2, T3 item3, T4 item4)
		{
			this.m_Item1 = item1;
			this.m_Item2 = item2;
			this.m_Item3 = item3;
			this.m_Item4 = item4;
		}

		/// <summary>Returns a value that indicates whether the current <see cref="T:System.Tuple`4" /> object is equal to a specified object.</summary>
		/// <returns>true if the current instance is equal to the specified object; otherwise, false.</returns>
		/// <param name="obj">The object to compare with this instance.</param>
		// Token: 0x060008E8 RID: 2280 RVA: 0x0002EDF2 File Offset: 0x0002CFF2
		public override bool Equals(object obj)
		{
			return ((IStructuralEquatable)this).Equals(obj, ObjectEqualityComparer.Default);
		}

		/// <summary>Returns a value that indicates whether the current <see cref="T:System.Tuple`4" /> object is equal to a specified object based on a specified comparison method.</summary>
		/// <returns>true if the current instance is equal to the specified object; otherwise, false. </returns>
		/// <param name="other">The object to compare with this instance.</param>
		/// <param name="comparer">An object that defines the method to use to evaluate whether the two objects are equal.</param>
		// Token: 0x060008E9 RID: 2281 RVA: 0x0002F394 File Offset: 0x0002D594
		bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer)
		{
			if (other == null)
			{
				return false;
			}
			Tuple<T1, T2, T3, T4> tuple = other as Tuple<T1, T2, T3, T4>;
			return tuple != null && (comparer.Equals(this.m_Item1, tuple.m_Item1) && comparer.Equals(this.m_Item2, tuple.m_Item2) && comparer.Equals(this.m_Item3, tuple.m_Item3)) && comparer.Equals(this.m_Item4, tuple.m_Item4);
		}

		/// <summary>Compares the current <see cref="T:System.Tuple`4" /> object to a specified object and returns an integer that indicates whether the current object is before, after, or in the same position as the specified object in the sort order.</summary>
		/// <returns>A signed integer that indicates the relative position of this instance and <paramref name="obj" /> in the sort order, as shown in the following table.ValueDescriptionA negative integerThis instance precedes <paramref name="obj" />.ZeroThis instance and <paramref name="obj" /> have the same position in the sort order.A positive integerThis instance follows <paramref name="obj" />.</returns>
		/// <param name="obj">An object to compare with the current instance.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="obj" /> is not a <see cref="T:System.Tuple`4" /> object.</exception>
		// Token: 0x060008EA RID: 2282 RVA: 0x0002EE3A File Offset: 0x0002D03A
		int IComparable.CompareTo(object obj)
		{
			return ((IStructuralComparable)this).CompareTo(obj, LowLevelComparer.Default);
		}

		/// <summary>Compares the current <see cref="T:System.Tuple`4" /> object to a specified object by using a specified comparer and returns an integer that indicates whether the current object is before, after, or in the same position as the specified object in the sort order.</summary>
		/// <returns>A signed integer that indicates the relative position of this instance and <paramref name="other" /> in the sort order, as shown in the following table.ValueDescriptionA negative integerThis instance precedes <paramref name="other" />.ZeroThis instance and <paramref name="other" /> have the same position in the sort order.A positive integerThis instance follows <paramref name="other" />.</returns>
		/// <param name="other">An object to compare with the current instance.</param>
		/// <param name="comparer">An object that provides custom rules for comparison.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="other" /> is not a <see cref="T:System.Tuple`4" /> object.</exception>
		// Token: 0x060008EB RID: 2283 RVA: 0x0002F42C File Offset: 0x0002D62C
		int IStructuralComparable.CompareTo(object other, IComparer comparer)
		{
			if (other == null)
			{
				return 1;
			}
			Tuple<T1, T2, T3, T4> tuple = other as Tuple<T1, T2, T3, T4>;
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
			return comparer.Compare(this.m_Item4, tuple.m_Item4);
		}

		/// <summary>Returns the hash code for the current <see cref="T:System.Tuple`4" /> object.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x060008EC RID: 2284 RVA: 0x0002EEA0 File Offset: 0x0002D0A0
		public override int GetHashCode()
		{
			return ((IStructuralEquatable)this).GetHashCode(ObjectEqualityComparer.Default);
		}

		/// <summary>Calculates the hash code for the current <see cref="T:System.Tuple`4" /> object by using a specified computation method.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		/// <param name="comparer">An object whose <see cref="M:System.Collections.IEqualityComparer.GetHashCode(System.Object)" />  method calculates the hash code of the current <see cref="T:System.Tuple`4" /> object.</param>
		// Token: 0x060008ED RID: 2285 RVA: 0x0002F4EC File Offset: 0x0002D6EC
		int IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
		{
			return Tuple.CombineHashCodes(comparer.GetHashCode(this.m_Item1), comparer.GetHashCode(this.m_Item2), comparer.GetHashCode(this.m_Item3), comparer.GetHashCode(this.m_Item4));
		}

		// Token: 0x060008EE RID: 2286 RVA: 0x0002EEC0 File Offset: 0x0002D0C0
		int ITupleInternal.GetHashCode(IEqualityComparer comparer)
		{
			return ((IStructuralEquatable)this).GetHashCode(comparer);
		}

		/// <summary>Returns a string that represents the value of this <see cref="T:System.Tuple`4" /> instance.</summary>
		/// <returns>The string representation of this <see cref="T:System.Tuple`4" /> object.</returns>
		// Token: 0x060008EF RID: 2287 RVA: 0x0002F544 File Offset: 0x0002D744
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("(");
			return ((ITupleInternal)this).ToString(stringBuilder);
		}

		// Token: 0x060008F0 RID: 2288 RVA: 0x0002F56C File Offset: 0x0002D76C
		string ITupleInternal.ToString(StringBuilder sb)
		{
			sb.Append(this.m_Item1);
			sb.Append(", ");
			sb.Append(this.m_Item2);
			sb.Append(", ");
			sb.Append(this.m_Item3);
			sb.Append(", ");
			sb.Append(this.m_Item4);
			sb.Append(')');
			return sb.ToString();
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x060008F1 RID: 2289 RVA: 0x00028BDC File Offset: 0x00026DDC
		int ITuple.Length
		{
			get
			{
				return 4;
			}
		}

		// Token: 0x1700015E RID: 350
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
				default:
					throw new IndexOutOfRangeException();
				}
			}
		}

		// Token: 0x040006DF RID: 1759
		private readonly T1 m_Item1;

		// Token: 0x040006E0 RID: 1760
		private readonly T2 m_Item2;

		// Token: 0x040006E1 RID: 1761
		private readonly T3 m_Item3;

		// Token: 0x040006E2 RID: 1762
		private readonly T4 m_Item4;
	}
}
