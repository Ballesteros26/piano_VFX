using System;
using System.Security.Permissions;

namespace System.Security.Cryptography
{
	/// <summary>Encapsulates the name of a key storage provider (KSP) for use with Cryptography Next Generation (CNG) objects.</summary>
	// Token: 0x02000068 RID: 104
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	[Serializable]
	public sealed class CngProvider : IEquatable<CngProvider>
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.CngProvider" /> class.</summary>
		/// <param name="provider">The name of the key storage provider (KSP) to initialize.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="provider" /> parameter is null.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="provider" /> parameter length is 0 (zero).</exception>
		// Token: 0x06000259 RID: 601 RVA: 0x00005CB8 File Offset: 0x00003EB8
		public CngProvider(string provider)
		{
			if (provider == null)
			{
				throw new ArgumentNullException("provider");
			}
			if (provider.Length == 0)
			{
				throw new ArgumentException(global::SR.GetString("The provider name '{0}' is invalid.", new object[] { provider }), "provider");
			}
			this.m_provider = provider;
		}

		/// <summary>Gets the name of the key storage provider (KSP) that the current <see cref="T:System.Security.Cryptography.CngProvider" /> object specifies.</summary>
		/// <returns>The embedded KSP name.</returns>
		// Token: 0x1700008E RID: 142
		// (get) Token: 0x0600025A RID: 602 RVA: 0x00005D07 File Offset: 0x00003F07
		public string Provider
		{
			get
			{
				return this.m_provider;
			}
		}

		/// <summary>Determines whether two <see cref="T:System.Security.Cryptography.CngProvider" /> objects specify the same key storage provider (KSP).</summary>
		/// <returns>true if the two objects represent the same KSP; otherwise, false.</returns>
		/// <param name="left">An object that specifies a KSP.</param>
		/// <param name="right">A second object, to be compared to the object that is identified by the <paramref name="left" /> parameter.</param>
		// Token: 0x0600025B RID: 603 RVA: 0x00005D0F File Offset: 0x00003F0F
		public static bool operator ==(CngProvider left, CngProvider right)
		{
			if (left == null)
			{
				return right == null;
			}
			return left.Equals(right);
		}

		/// <summary>Determines whether two <see cref="T:System.Security.Cryptography.CngProvider" /> objects do not represent the same key storage provider (KSP).</summary>
		/// <returns>true if the two objects do not represent the same KSP; otherwise, false.</returns>
		/// <param name="left">An object that specifies a KSP.</param>
		/// <param name="right">A second object, to be compared to the object that is identified by the <paramref name="left" /> parameter.</param>
		// Token: 0x0600025C RID: 604 RVA: 0x00005D20 File Offset: 0x00003F20
		public static bool operator !=(CngProvider left, CngProvider right)
		{
			if (left == null)
			{
				return right != null;
			}
			return !left.Equals(right);
		}

		/// <summary>Compares the specified object to the current <see cref="T:System.Security.Cryptography.CngProvider" /> object.</summary>
		/// <returns>true if the <paramref name="obj" /> parameter is a <see cref="T:System.Security.Cryptography.CngProvider" /> that specifies the same key storage provider(KSP) as the current object; otherwise, false.</returns>
		/// <param name="obj">An object to be compared to the current <see cref="T:System.Security.Cryptography.CngProvider" /> object.</param>
		// Token: 0x0600025D RID: 605 RVA: 0x00005D34 File Offset: 0x00003F34
		public override bool Equals(object obj)
		{
			return this.Equals(obj as CngProvider);
		}

		/// <summary>Compares the specified <see cref="T:System.Security.Cryptography.CngProvider" /> object to the current <see cref="T:System.Security.Cryptography.CngProvider" /> object.</summary>
		/// <returns>true if the <paramref name="other" /> parameter specifies the same key storage provider (KSP) as the current object; otherwise, false.</returns>
		/// <param name="other">An object to be compared to the current <see cref="T:System.Security.Cryptography.CngProvider" /> object.</param>
		// Token: 0x0600025E RID: 606 RVA: 0x00005D42 File Offset: 0x00003F42
		public bool Equals(CngProvider other)
		{
			return other != null && this.m_provider.Equals(other.Provider);
		}

		/// <summary>Generates a hash value for the name of the key storage provider (KSP) that is embedded in the current <see cref="T:System.Security.Cryptography.CngProvider" /> object.</summary>
		/// <returns>The hash value of the embedded KSP name.</returns>
		// Token: 0x0600025F RID: 607 RVA: 0x00005D5A File Offset: 0x00003F5A
		public override int GetHashCode()
		{
			return this.m_provider.GetHashCode();
		}

		/// <summary>Gets the name of the key storage provider (KSP) that the current <see cref="T:System.Security.Cryptography.CngProvider" /> object specifies.</summary>
		/// <returns>The embedded KSP name.</returns>
		// Token: 0x06000260 RID: 608 RVA: 0x00005D67 File Offset: 0x00003F67
		public override string ToString()
		{
			return this.m_provider.ToString();
		}

		/// <summary>Gets a <see cref="T:System.Security.Cryptography.CngProvider" /> object that specifies the Microsoft Smart Card Key Storage Provider. </summary>
		/// <returns>An object that specifies the Microsoft Smart Card Key Storage Provider.</returns>
		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000261 RID: 609 RVA: 0x00005D74 File Offset: 0x00003F74
		public static CngProvider MicrosoftSmartCardKeyStorageProvider
		{
			get
			{
				if (CngProvider.s_msSmartCardKsp == null)
				{
					CngProvider.s_msSmartCardKsp = new CngProvider("Microsoft Smart Card Key Storage Provider");
				}
				return CngProvider.s_msSmartCardKsp;
			}
		}

		/// <summary>Gets a <see cref="T:System.Security.Cryptography.CngProvider" /> object that specifies the Microsoft Software Key Storage Provider.</summary>
		/// <returns>An object that specifies the Microsoft Software Key Storage Provider.</returns>
		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000262 RID: 610 RVA: 0x00005D9D File Offset: 0x00003F9D
		public static CngProvider MicrosoftSoftwareKeyStorageProvider
		{
			get
			{
				if (CngProvider.s_msSoftwareKsp == null)
				{
					CngProvider.s_msSoftwareKsp = new CngProvider("Microsoft Software Key Storage Provider");
				}
				return CngProvider.s_msSoftwareKsp;
			}
		}

		// Token: 0x040002B3 RID: 691
		private static volatile CngProvider s_msSmartCardKsp;

		// Token: 0x040002B4 RID: 692
		private static volatile CngProvider s_msSoftwareKsp;

		// Token: 0x040002B5 RID: 693
		private string m_provider;
	}
}
