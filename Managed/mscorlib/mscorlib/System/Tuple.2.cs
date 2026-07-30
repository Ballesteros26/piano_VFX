using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace System
{
	/// <summary>Represents a 1-tuple, or singleton. </summary>
	/// <typeparam name="T1">The type of the tuple's only component.</typeparam>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020000EA RID: 234
	[Serializable]
	public class Tuple<T1> : IStructuralEquatable, IStructuralComparable, IComparable, ITupleInternal, ITuple
	{
		/// <summary>Gets the value of the <see cref="T:System.Tuple`1" /> object's single component. </summary>
		/// <returns>The value of the current <see cref="T:System.Tuple`1" /> object's single component.</returns>
		// Token: 0x1700014D RID: 333
		// (get) Token: 0x060008B9 RID: 2233 RVA: 0x0002EDDB File Offset: 0x0002CFDB
		public T1 Item1
		{
			get
			{
				return this.m_Item1;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Tuple`1" /> class.</summary>
		/// <param name="item1">The value of the tuple's only component.</param>
		// Token: 0x060008BA RID: 2234 RVA: 0x0002EDE3 File Offset: 0x0002CFE3
		public Tuple(T1 item1)
		{
			this.m_Item1 = item1;
		}

		/// <summary>Returns a value that indicates whether the current <see cref="T:System.Tuple`1" /> object is equal to a specified object.</summary>
		/// <returns>true if the current instance is equal to the specified object; otherwise, false.</returns>
		/// <param name="obj">The object to compare with this instance.</param>
		// Token: 0x060008BB RID: 2235 RVA: 0x0002EDF2 File Offset: 0x0002CFF2
		public override bool Equals(object obj)
		{
			return ((IStructuralEquatable)this).Equals(obj, ObjectEqualityComparer.Default);
		}

		/// <summary>Returns a value that indicates whether the current <see cref="T:System.Tuple`1" /> object is equal to a specified object based on a specified comparison method.</summary>
		/// <returns>true if the current instance is equal to the specified object; otherwise, false.</returns>
		/// <param name="other">The object to compare with this instance.</param>
		/// <param name="comparer">An object that defines the method to use to evaluate whether the two objects are equal.</param>
		// Token: 0x060008BC RID: 2236 RVA: 0x0002EE00 File Offset: 0x0002D000
		bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer)
		{
			if (other == null)
			{
				return false;
			}
			Tuple<T1> tuple = other as Tuple<T1>;
			return tuple != null && comparer.Equals(this.m_Item1, tuple.m_Item1);
		}

		/// <summary>Compares the current <see cref="T:System.Tuple`1" /> object to a specified object, and returns an integer that indicates whether the current object is before, after, or in the same position as the specified object in the sort order.</summary>
		/// <returns>A signed integer that indicates the relative position of this instance and <paramref name="obj" /> in the sort order, as shown in the following table.ValueDescriptionA negative integerThis instance precedes <paramref name="obj" />.ZeroThis instance and <paramref name="obj" /> have the same position in the sort order.A positive integerThis instance follows <paramref name="obj" />.</returns>
		/// <param name="obj">An object to compare with the current instance.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="obj" /> is not a <see cref="T:System.Tuple`1" /> object.</exception>
		// Token: 0x060008BD RID: 2237 RVA: 0x0002EE3A File Offset: 0x0002D03A
		int IComparable.CompareTo(object obj)
		{
			return ((IStructuralComparable)this).CompareTo(obj, LowLevelComparer.Default);
		}

		/// <summary>Compares the current <see cref="T:System.Tuple`1" /> object to a specified object by using a specified comparer, and returns an integer that indicates whether the current object is before, after, or in the same position as the specified object in the sort order.</summary>
		/// <returns>A signed integer that indicates the relative position of this instance and <paramref name="other" /> in the sort order, as shown in the following table.ValueDescriptionA negative integerThis instance precedes <paramref name="other" />.ZeroThis instance and <paramref name="other" /> have the same position in the sort order.A positive integerThis instance follows <paramref name="other" />.</returns>
		/// <param name="other">An object to compare with the current instance.</param>
		/// <param name="comparer">An object that provides custom rules for comparison.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="other" /> is not a <see cref="T:System.Tuple`1" /> object.</exception>
		// Token: 0x060008BE RID: 2238 RVA: 0x0002EE48 File Offset: 0x0002D048
		int IStructuralComparable.CompareTo(object other, IComparer comparer)
		{
			if (other == null)
			{
				return 1;
			}
			Tuple<T1> tuple = other as Tuple<T1>;
			if (tuple == null)
			{
				throw new ArgumentException(SR.Format("Argument must be of type {0}.", base.GetType().ToString()), "other");
			}
			return comparer.Compare(this.m_Item1, tuple.m_Item1);
		}

		/// <summary>Returns the hash code for the current <see cref="T:System.Tuple`1" /> object.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x060008BF RID: 2239 RVA: 0x0002EEA0 File Offset: 0x0002D0A0
		public override int GetHashCode()
		{
			return ((IStructuralEquatable)this).GetHashCode(ObjectEqualityComparer.Default);
		}

		/// <summary>Calculates the hash code for the current <see cref="T:System.Tuple`1" /> object by using a specified computation method.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		/// <param name="comparer">An object whose <see cref="M:System.Collections.IEqualityComparer.GetHashCode(System.Object)" />  method calculates the hash code of the current <see cref="T:System.Tuple`1" /> object.</param>
		// Token: 0x060008C0 RID: 2240 RVA: 0x0002EEAD File Offset: 0x0002D0AD
		int IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
		{
			return comparer.GetHashCode(this.m_Item1);
		}

		// Token: 0x060008C1 RID: 2241 RVA: 0x0002EEC0 File Offset: 0x0002D0C0
		int ITupleInternal.GetHashCode(IEqualityComparer comparer)
		{
			return ((IStructuralEquatable)this).GetHashCode(comparer);
		}

		/// <summary>Returns a string that represents the value of this <see cref="T:System.Tuple`1" /> instance.</summary>
		/// <returns>The string representation of this <see cref="T:System.Tuple`1" /> object.</returns>
		// Token: 0x060008C2 RID: 2242 RVA: 0x0002EECC File Offset: 0x0002D0CC
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("(");
			return ((ITupleInternal)this).ToString(stringBuilder);
		}

		// Token: 0x060008C3 RID: 2243 RVA: 0x0002EEF2 File Offset: 0x0002D0F2
		string ITupleInternal.ToString(StringBuilder sb)
		{
			sb.Append(this.m_Item1);
			sb.Append(')');
			return sb.ToString();
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x060008C4 RID: 2244 RVA: 0x00003B29 File Offset: 0x00001D29
		int ITuple.Length
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x1700014F RID: 335
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

		// Token: 0x040006D9 RID: 1753
		private readonly T1 m_Item1;
	}
}
