using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System.Collections
{
	/// <summary>Compares two objects for equivalence, where string comparisons are case-sensitive.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020009BE RID: 2494
	[ComVisible(true)]
	[Serializable]
	public sealed class Comparer : IComparer, ISerializable
	{
		// Token: 0x06005C37 RID: 23607 RVA: 0x001310D0 File Offset: 0x0012F2D0
		private Comparer()
		{
			this.m_compareInfo = null;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Comparer" /> class using the specified <see cref="T:System.Globalization.CultureInfo" />.</summary>
		/// <param name="culture">The <see cref="T:System.Globalization.CultureInfo" /> to use for the new <see cref="T:System.Collections.Comparer" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="culture" /> is null. </exception>
		// Token: 0x06005C38 RID: 23608 RVA: 0x001310DF File Offset: 0x0012F2DF
		public Comparer(CultureInfo culture)
		{
			if (culture == null)
			{
				throw new ArgumentNullException("culture");
			}
			this.m_compareInfo = culture.CompareInfo;
		}

		// Token: 0x06005C39 RID: 23609 RVA: 0x00131104 File Offset: 0x0012F304
		private Comparer(SerializationInfo info, StreamingContext context)
		{
			this.m_compareInfo = null;
			SerializationInfoEnumerator enumerator = info.GetEnumerator();
			while (enumerator.MoveNext())
			{
				string name = enumerator.Name;
				if (name == "CompareInfo")
				{
					this.m_compareInfo = (CompareInfo)info.GetValue("CompareInfo", typeof(CompareInfo));
				}
			}
		}

		/// <summary>Performs a case-sensitive comparison of two objects of the same type and returns a value indicating whether one is less than, equal to, or greater than the other.</summary>
		/// <returns>A signed integer that indicates the relative values of <paramref name="a" /> and <paramref name="b" />, as shown in the following table.Value Meaning Less than zero <paramref name="a" /> is less than <paramref name="b" />. Zero <paramref name="a" /> equals <paramref name="b" />. Greater than zero <paramref name="a" /> is greater than <paramref name="b" />. </returns>
		/// <param name="a">The first object to compare. </param>
		/// <param name="b">The second object to compare. </param>
		/// <exception cref="T:System.ArgumentException">Neither <paramref name="a" /> nor <paramref name="b" /> implements the <see cref="T:System.IComparable" /> interface.-or- <paramref name="a" /> and <paramref name="b" /> are of different types and neither one can handle comparisons with the other. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06005C3A RID: 23610 RVA: 0x00131164 File Offset: 0x0012F364
		public int Compare(object a, object b)
		{
			if (a == b)
			{
				return 0;
			}
			if (a == null)
			{
				return -1;
			}
			if (b == null)
			{
				return 1;
			}
			if (this.m_compareInfo != null)
			{
				string text = a as string;
				string text2 = b as string;
				if (text != null && text2 != null)
				{
					return this.m_compareInfo.Compare(text, text2);
				}
			}
			IComparable comparable = a as IComparable;
			if (comparable != null)
			{
				return comparable.CompareTo(b);
			}
			IComparable comparable2 = b as IComparable;
			if (comparable2 != null)
			{
				return -comparable2.CompareTo(a);
			}
			throw new ArgumentException(Environment.GetResourceString("At least one object must implement IComparable."));
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the data required for serialization.  </summary>
		/// <param name="info">The object to populate with data.</param>
		/// <param name="context">The context information about the source or destination of the serialization.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is null.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06005C3B RID: 23611 RVA: 0x001311DF File Offset: 0x0012F3DF
		[SecurityCritical]
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			if (this.m_compareInfo != null)
			{
				info.AddValue("CompareInfo", this.m_compareInfo);
			}
		}

		// Token: 0x04002F35 RID: 12085
		private CompareInfo m_compareInfo;

		/// <summary>Represents an instance of <see cref="T:System.Collections.Comparer" /> that is associated with the <see cref="P:System.Threading.Thread.CurrentCulture" /> of the current thread. This field is read-only.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x04002F36 RID: 12086
		public static readonly Comparer Default = new Comparer(CultureInfo.CurrentCulture);

		/// <summary>Represents an instance of <see cref="T:System.Collections.Comparer" /> that is associated with <see cref="P:System.Globalization.CultureInfo.InvariantCulture" />. This field is read-only.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x04002F37 RID: 12087
		public static readonly Comparer DefaultInvariant = new Comparer(CultureInfo.InvariantCulture);

		// Token: 0x04002F38 RID: 12088
		private const string CompareInfoName = "CompareInfo";
	}
}
