using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace System
{
	/// <summary>Represents a 3-tuple, or triple. </summary>
	/// <typeparam name="T1">The type of the tuple's first component.</typeparam>
	/// <typeparam name="T2">The type of the tuple's second component.</typeparam>
	/// <typeparam name="T3">The type of the tuple's third component.</typeparam>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000EC RID: 236
	[Serializable]
	public class Tuple<T1, T2, T3> : IStructuralEquatable, IStructuralComparable, IComparable, ITupleInternal, ITuple
	{
		/// <summary>Gets the value of the current <see cref="T:System.Tuple`3" /> object's first component.</summary>
		/// <returns>The value of the current <see cref="T:System.Tuple`3" /> object's first component.</returns>
		// Token: 0x17000154 RID: 340
		// (get) Token: 0x060008D4 RID: 2260 RVA: 0x0002F0F4 File Offset: 0x0002D2F4
		public T1 Item1
		{
			get
			{
				return this.m_Item1;
			}
		}

		/// <summary>Gets the value of the current <see cref="T:System.Tuple`3" /> object's second component.</summary>
		/// <returns>The value of the current <see cref="T:System.Tuple`3" /> object's second component.</returns>
		// Token: 0x17000155 RID: 341
		// (get) Token: 0x060008D5 RID: 2261 RVA: 0x0002F0FC File Offset: 0x0002D2FC
		public T2 Item2
		{
			get
			{
				return this.m_Item2;
			}
		}

		/// <summary>Gets the value of the current <see cref="T:System.Tuple`3" /> object's third component.</summary>
		/// <returns>The value of the current <see cref="T:System.Tuple`3" /> object's third component.</returns>
		// Token: 0x17000156 RID: 342
		// (get) Token: 0x060008D6 RID: 2262 RVA: 0x0002F104 File Offset: 0x0002D304
		public T3 Item3
		{
			get
			{
				return this.m_Item3;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Tuple`3" /> class.</summary>
		/// <param name="item1">The value of the tuple's first component.</param>
		/// <param name="item2">The value of the tuple's second component.</param>
		/// <param name="item3">The value of the tuple's third component.</param>
		// Token: 0x060008D7 RID: 2263 RVA: 0x0002F10C File Offset: 0x0002D30C
		public Tuple(T1 item1, T2 item2, T3 item3)
		{
			this.m_Item1 = item1;
			this.m_Item2 = item2;
			this.m_Item3 = item3;
		}

		/// <summary>Returns a value that indicates whether the current <see cref="T:System.Tuple`3" /> object is equal to a specified object.</summary>
		/// <returns>true if the current instance is equal to the specified object; otherwise, false.</returns>
		/// <param name="obj">The object to compare with this instance.</param>
		// Token: 0x060008D8 RID: 2264 RVA: 0x0002EDF2 File Offset: 0x0002CFF2
		public override bool Equals(object obj)
		{
			return ((IStructuralEquatable)this).Equals(obj, ObjectEqualityComparer.Default);
		}

		/// <summary>Returns a value that indicates whether the current <see cref="T:System.Tuple`3" /> object is equal to a specified object based on a specified comparison method.</summary>
		/// <returns>true if the current instance is equal to the specified object; otherwise, false.</returns>
		/// <param name="other">The object to compare with this instance.</param>
		/// <param name="comparer">An object that defines the method to use to evaluate whether the two objects are equal.</param>
		// Token: 0x060008D9 RID: 2265 RVA: 0x0002F12C File Offset: 0x0002D32C
		bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer)
		{
			if (other == null)
			{
				return false;
			}
			Tuple<T1, T2, T3> tuple = other as Tuple<T1, T2, T3>;
			return tuple != null && (comparer.Equals(this.m_Item1, tuple.m_Item1) && comparer.Equals(this.m_Item2, tuple.m_Item2)) && comparer.Equals(this.m_Item3, tuple.m_Item3);
		}

		/// <summary>Compares the current <see cref="T:System.Tuple`3" /> object to a specified object and returns an integer that indicates whether the current object is before, after, or in the same position as the specified object in the sort order.</summary>
		/// <returns>A signed integer that indicates the relative position of this instance and <paramref name="obj" /> in the sort order, as shown in the following table.ValueDescriptionA negative integerThis instance precedes <paramref name="obj" />.ZeroThis instance and <paramref name="obj" /> have the same position in the sort order.A positive integerThis instance follows <paramref name="obj" />.</returns>
		/// <param name="obj">An object to compare with the current instance.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="obj" /> is not a <see cref="T:System.Tuple`3" /> object.</exception>
		// Token: 0x060008DA RID: 2266 RVA: 0x0002EE3A File Offset: 0x0002D03A
		int IComparable.CompareTo(object obj)
		{
			return ((IStructuralComparable)this).CompareTo(obj, LowLevelComparer.Default);
		}

		/// <summary>Compares the current <see cref="T:System.Tuple`3" /> object to a specified object by using a specified comparer, and returns an integer that indicates whether the current object is before, after, or in the same position as the specified object in the sort order.</summary>
		/// <returns>A signed integer that indicates the relative position of this instance and <paramref name="other" /> in the sort order, as shown in the following table.ValueDescriptionA negative integerThis instance precedes <paramref name="other" />.ZeroThis instance and <paramref name="other" /> have the same position in the sort order.A positive integerThis instance follows <paramref name="other" />.</returns>
		/// <param name="other">An object to compare with the current instance.</param>
		/// <param name="comparer">An object that provides custom rules for comparison.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="other" /> is not a <see cref="T:System.Tuple`3" /> object.</exception>
		// Token: 0x060008DB RID: 2267 RVA: 0x0002F1A4 File Offset: 0x0002D3A4
		int IStructuralComparable.CompareTo(object other, IComparer comparer)
		{
			if (other == null)
			{
				return 1;
			}
			Tuple<T1, T2, T3> tuple = other as Tuple<T1, T2, T3>;
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
			return comparer.Compare(this.m_Item3, tuple.m_Item3);
		}

		/// <summary>Returns the hash code for the current <see cref="T:System.Tuple`3" /> object.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x060008DC RID: 2268 RVA: 0x0002EEA0 File Offset: 0x0002D0A0
		public override int GetHashCode()
		{
			return ((IStructuralEquatable)this).GetHashCode(ObjectEqualityComparer.Default);
		}

		/// <summary>Calculates the hash code for the current <see cref="T:System.Tuple`3" /> object by using a specified computation method.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		/// <param name="comparer">An object whose <see cref="M:System.Collections.IEqualityComparer.GetHashCode(System.Object)" />  method calculates the hash code of the current <see cref="T:System.Tuple`3" /> object.</param>
		// Token: 0x060008DD RID: 2269 RVA: 0x0002F242 File Offset: 0x0002D442
		int IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
		{
			return Tuple.CombineHashCodes(comparer.GetHashCode(this.m_Item1), comparer.GetHashCode(this.m_Item2), comparer.GetHashCode(this.m_Item3));
		}

		// Token: 0x060008DE RID: 2270 RVA: 0x0002EEC0 File Offset: 0x0002D0C0
		int ITupleInternal.GetHashCode(IEqualityComparer comparer)
		{
			return ((IStructuralEquatable)this).GetHashCode(comparer);
		}

		/// <summary>Returns a string that represents the value of this <see cref="T:System.Tuple`3" /> instance.</summary>
		/// <returns>The string representation of this <see cref="T:System.Tuple`3" /> object.</returns>
		// Token: 0x060008DF RID: 2271 RVA: 0x0002F27C File Offset: 0x0002D47C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("(");
			return ((ITupleInternal)this).ToString(stringBuilder);
		}

		// Token: 0x060008E0 RID: 2272 RVA: 0x0002F2A4 File Offset: 0x0002D4A4
		string ITupleInternal.ToString(StringBuilder sb)
		{
			sb.Append(this.m_Item1);
			sb.Append(", ");
			sb.Append(this.m_Item2);
			sb.Append(", ");
			sb.Append(this.m_Item3);
			sb.Append(')');
			return sb.ToString();
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x060008E1 RID: 2273 RVA: 0x00028538 File Offset: 0x00026738
		int ITuple.Length
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x17000158 RID: 344
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
				default:
					throw new IndexOutOfRangeException();
				}
			}
		}

		// Token: 0x040006DC RID: 1756
		private readonly T1 m_Item1;

		// Token: 0x040006DD RID: 1757
		private readonly T2 m_Item2;

		// Token: 0x040006DE RID: 1758
		private readonly T3 m_Item3;
	}
}
