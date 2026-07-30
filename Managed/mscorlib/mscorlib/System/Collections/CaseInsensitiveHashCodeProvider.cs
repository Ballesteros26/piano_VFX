using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Collections
{
	/// <summary>Supplies a hash code for an object, using a hashing algorithm that ignores the case of strings.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020009BC RID: 2492
	[ComVisible(true)]
	[Obsolete("Please use StringComparer instead.")]
	[Serializable]
	public class CaseInsensitiveHashCodeProvider : IHashCodeProvider
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.CaseInsensitiveHashCodeProvider" /> class using the <see cref="P:System.Threading.Thread.CurrentCulture" /> of the current thread.</summary>
		// Token: 0x06005C12 RID: 23570 RVA: 0x00130CB8 File Offset: 0x0012EEB8
		public CaseInsensitiveHashCodeProvider()
		{
			this.m_text = CultureInfo.CurrentCulture.TextInfo;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.CaseInsensitiveHashCodeProvider" /> class using the specified <see cref="T:System.Globalization.CultureInfo" />.</summary>
		/// <param name="culture">The <see cref="T:System.Globalization.CultureInfo" /> to use for the new <see cref="T:System.Collections.CaseInsensitiveHashCodeProvider" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="culture" /> is null. </exception>
		// Token: 0x06005C13 RID: 23571 RVA: 0x00130CD0 File Offset: 0x0012EED0
		public CaseInsensitiveHashCodeProvider(CultureInfo culture)
		{
			if (culture == null)
			{
				throw new ArgumentNullException("culture");
			}
			this.m_text = culture.TextInfo;
		}

		/// <summary>Gets an instance of <see cref="T:System.Collections.CaseInsensitiveHashCodeProvider" /> that is associated with the <see cref="P:System.Threading.Thread.CurrentCulture" /> of the current thread and that is always available.</summary>
		/// <returns>An instance of <see cref="T:System.Collections.CaseInsensitiveHashCodeProvider" /> that is associated with the <see cref="P:System.Threading.Thread.CurrentCulture" /> of the current thread.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x1700100E RID: 4110
		// (get) Token: 0x06005C14 RID: 23572 RVA: 0x00130CF2 File Offset: 0x0012EEF2
		public static CaseInsensitiveHashCodeProvider Default
		{
			get
			{
				return new CaseInsensitiveHashCodeProvider(CultureInfo.CurrentCulture);
			}
		}

		/// <summary>Gets an instance of <see cref="T:System.Collections.CaseInsensitiveHashCodeProvider" /> that is associated with <see cref="P:System.Globalization.CultureInfo.InvariantCulture" /> and that is always available.</summary>
		/// <returns>An instance of <see cref="T:System.Collections.CaseInsensitiveHashCodeProvider" /> that is associated with <see cref="P:System.Globalization.CultureInfo.InvariantCulture" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x1700100F RID: 4111
		// (get) Token: 0x06005C15 RID: 23573 RVA: 0x00130CFE File Offset: 0x0012EEFE
		public static CaseInsensitiveHashCodeProvider DefaultInvariant
		{
			get
			{
				if (CaseInsensitiveHashCodeProvider.m_InvariantCaseInsensitiveHashCodeProvider == null)
				{
					CaseInsensitiveHashCodeProvider.m_InvariantCaseInsensitiveHashCodeProvider = new CaseInsensitiveHashCodeProvider(CultureInfo.InvariantCulture);
				}
				return CaseInsensitiveHashCodeProvider.m_InvariantCaseInsensitiveHashCodeProvider;
			}
		}

		/// <summary>Returns a hash code for the given object, using a hashing algorithm that ignores the case of strings.</summary>
		/// <returns>A hash code for the given object, using a hashing algorithm that ignores the case of strings.</returns>
		/// <param name="obj">The <see cref="T:System.Object" /> for which a hash code is to be returned. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="obj" /> is null. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06005C16 RID: 23574 RVA: 0x00130D24 File Offset: 0x0012EF24
		public int GetHashCode(object obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			string text = obj as string;
			if (text == null)
			{
				return obj.GetHashCode();
			}
			return this.m_text.GetCaseInsensitiveHashCode(text);
		}

		// Token: 0x04002F32 RID: 12082
		private TextInfo m_text;

		// Token: 0x04002F33 RID: 12083
		private static volatile CaseInsensitiveHashCodeProvider m_InvariantCaseInsensitiveHashCodeProvider;
	}
}
