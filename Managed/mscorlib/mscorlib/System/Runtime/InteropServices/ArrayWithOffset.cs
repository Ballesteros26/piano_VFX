using System;
using System.Security;

namespace System.Runtime.InteropServices
{
	/// <summary>Encapsulates an array and an offset within the specified array.</summary>
	// Token: 0x020008A2 RID: 2210
	[ComVisible(true)]
	[Serializable]
	public struct ArrayWithOffset
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> structure.</summary>
		/// <param name="array">A managed array. </param>
		/// <param name="offset">The offset in bytes, of the element to be passed through platform invoke. </param>
		/// <exception cref="T:System.ArgumentException">The array is larger than 2 gigabytes (GB).</exception>
		// Token: 0x060054CB RID: 21707 RVA: 0x00128242 File Offset: 0x00126442
		[SecuritySafeCritical]
		public ArrayWithOffset(object array, int offset)
		{
			this.m_array = array;
			this.m_offset = offset;
			this.m_count = 0;
			this.m_count = this.CalculateCount();
		}

		/// <summary>Returns the managed array referenced by this <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" />.</summary>
		/// <returns>The managed array this instance references.</returns>
		// Token: 0x060054CC RID: 21708 RVA: 0x00128265 File Offset: 0x00126465
		public object GetArray()
		{
			return this.m_array;
		}

		/// <summary>Returns the offset provided when this <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> was constructed.</summary>
		/// <returns>The offset for this instance.</returns>
		// Token: 0x060054CD RID: 21709 RVA: 0x0012826D File Offset: 0x0012646D
		public int GetOffset()
		{
			return this.m_offset;
		}

		/// <summary>Returns a hash code for this value type.</summary>
		/// <returns>The hash code for this instance.</returns>
		// Token: 0x060054CE RID: 21710 RVA: 0x00128275 File Offset: 0x00126475
		public override int GetHashCode()
		{
			return this.m_count + this.m_offset;
		}

		/// <summary>Indicates whether the specified object matches the current <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> object.</summary>
		/// <returns>true if the object matches this <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" />; otherwise, false.</returns>
		/// <param name="obj">Object to compare with this instance. </param>
		// Token: 0x060054CF RID: 21711 RVA: 0x00128284 File Offset: 0x00126484
		public override bool Equals(object obj)
		{
			return obj is ArrayWithOffset && this.Equals((ArrayWithOffset)obj);
		}

		/// <summary>Indicates whether the specified <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> object matches the current instance.</summary>
		/// <returns>true if the specified <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> object matches the current instance; otherwise, false.</returns>
		/// <param name="obj">An <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> object to compare with this instance.</param>
		// Token: 0x060054D0 RID: 21712 RVA: 0x0012829C File Offset: 0x0012649C
		public bool Equals(ArrayWithOffset obj)
		{
			return obj.m_array == this.m_array && obj.m_offset == this.m_offset && obj.m_count == this.m_count;
		}

		/// <summary>Determines whether two specified <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> objects have the same value.</summary>
		/// <returns>true if the value of <paramref name="a" /> is the same as the value of <paramref name="b" />; otherwise, false.</returns>
		/// <param name="a">An <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> object to compare with the <paramref name="b" /> parameter. </param>
		/// <param name="b">An <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> object to compare with the <paramref name="a" /> parameter.</param>
		// Token: 0x060054D1 RID: 21713 RVA: 0x001282CA File Offset: 0x001264CA
		public static bool operator ==(ArrayWithOffset a, ArrayWithOffset b)
		{
			return a.Equals(b);
		}

		/// <summary>Determines whether two specified <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> objects no not have the same value.</summary>
		/// <returns>true if the value of <paramref name="a" /> is not the same as the value of <paramref name="b" />; otherwise, false.</returns>
		/// <param name="a">An <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> object to compare with the <paramref name="b" /> parameter. </param>
		/// <param name="b">An <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> object to compare with the <paramref name="a" /> parameter.</param>
		// Token: 0x060054D2 RID: 21714 RVA: 0x001282D4 File Offset: 0x001264D4
		public static bool operator !=(ArrayWithOffset a, ArrayWithOffset b)
		{
			return !(a == b);
		}

		// Token: 0x060054D3 RID: 21715 RVA: 0x001282E0 File Offset: 0x001264E0
		private int CalculateCount()
		{
			Array array = this.m_array as Array;
			if (array == null)
			{
				throw new ArgumentException();
			}
			return array.Rank * array.Length - this.m_offset;
		}

		// Token: 0x04002BEB RID: 11243
		private object m_array;

		// Token: 0x04002BEC RID: 11244
		private int m_offset;

		// Token: 0x04002BED RID: 11245
		private int m_count;
	}
}
