using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Collections
{
	/// <summary>Compares two objects for equivalence, ignoring the case of strings.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020009BB RID: 2491
	[ComVisible(true)]
	[Serializable]
	public class CaseInsensitiveComparer : IComparer
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.CaseInsensitiveComparer" /> class using the <see cref="P:System.Threading.Thread.CurrentCulture" /> of the current thread.</summary>
		// Token: 0x06005C0D RID: 23565 RVA: 0x00130C10 File Offset: 0x0012EE10
		public CaseInsensitiveComparer()
		{
			this.m_compareInfo = CultureInfo.CurrentCulture.CompareInfo;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.CaseInsensitiveComparer" /> class using the specified <see cref="T:System.Globalization.CultureInfo" />.</summary>
		/// <param name="culture">The <see cref="T:System.Globalization.CultureInfo" /> to use for the new <see cref="T:System.Collections.CaseInsensitiveComparer" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="culture" /> is null. </exception>
		// Token: 0x06005C0E RID: 23566 RVA: 0x00130C28 File Offset: 0x0012EE28
		public CaseInsensitiveComparer(CultureInfo culture)
		{
			if (culture == null)
			{
				throw new ArgumentNullException("culture");
			}
			this.m_compareInfo = culture.CompareInfo;
		}

		/// <summary>Gets an instance of <see cref="T:System.Collections.CaseInsensitiveComparer" /> that is associated with the <see cref="P:System.Threading.Thread.CurrentCulture" /> of the current thread and that is always available.</summary>
		/// <returns>An instance of <see cref="T:System.Collections.CaseInsensitiveComparer" /> that is associated with the <see cref="P:System.Threading.Thread.CurrentCulture" /> of the current thread.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x1700100C RID: 4108
		// (get) Token: 0x06005C0F RID: 23567 RVA: 0x00130C4A File Offset: 0x0012EE4A
		public static CaseInsensitiveComparer Default
		{
			get
			{
				return new CaseInsensitiveComparer(CultureInfo.CurrentCulture);
			}
		}

		/// <summary>Gets an instance of <see cref="T:System.Collections.CaseInsensitiveComparer" /> that is associated with <see cref="P:System.Globalization.CultureInfo.InvariantCulture" /> and that is always available.</summary>
		/// <returns>An instance of <see cref="T:System.Collections.CaseInsensitiveComparer" /> that is associated with <see cref="P:System.Globalization.CultureInfo.InvariantCulture" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x1700100D RID: 4109
		// (get) Token: 0x06005C10 RID: 23568 RVA: 0x00130C56 File Offset: 0x0012EE56
		public static CaseInsensitiveComparer DefaultInvariant
		{
			get
			{
				if (CaseInsensitiveComparer.m_InvariantCaseInsensitiveComparer == null)
				{
					CaseInsensitiveComparer.m_InvariantCaseInsensitiveComparer = new CaseInsensitiveComparer(CultureInfo.InvariantCulture);
				}
				return CaseInsensitiveComparer.m_InvariantCaseInsensitiveComparer;
			}
		}

		/// <summary>Performs a case-insensitive comparison of two objects of the same type and returns a value indicating whether one is less than, equal to, or greater than the other.</summary>
		/// <returns>A signed integer that indicates the relative values of <paramref name="a" /> and <paramref name="b" />, as shown in the following table.Value Meaning Less than zero <paramref name="a" /> is less than <paramref name="b" />, with casing ignored. Zero <paramref name="a" /> equals <paramref name="b" />, with casing ignored. Greater than zero <paramref name="a" /> is greater than <paramref name="b" />, with casing ignored. </returns>
		/// <param name="a">The first object to compare. </param>
		/// <param name="b">The second object to compare. </param>
		/// <exception cref="T:System.ArgumentException">Neither <paramref name="a" /> nor <paramref name="b" /> implements the <see cref="T:System.IComparable" /> interface.-or- <paramref name="a" /> and <paramref name="b" /> are of different types. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06005C11 RID: 23569 RVA: 0x00130C7C File Offset: 0x0012EE7C
		public int Compare(object a, object b)
		{
			string text = a as string;
			string text2 = b as string;
			if (text != null && text2 != null)
			{
				return this.m_compareInfo.Compare(text, text2, CompareOptions.IgnoreCase);
			}
			return Comparer.Default.Compare(a, b);
		}

		// Token: 0x04002F30 RID: 12080
		private CompareInfo m_compareInfo;

		// Token: 0x04002F31 RID: 12081
		private static volatile CaseInsensitiveComparer m_InvariantCaseInsensitiveComparer;
	}
}
