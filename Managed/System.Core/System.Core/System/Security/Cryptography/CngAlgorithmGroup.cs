using System;
using System.Security.Permissions;

namespace System.Security.Cryptography
{
	/// <summary>Encapsulates the name of an encryption algorithm group. </summary>
	// Token: 0x02000061 RID: 97
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	[Serializable]
	public sealed class CngAlgorithmGroup : IEquatable<CngAlgorithmGroup>
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.CngAlgorithmGroup" /> class.</summary>
		/// <param name="algorithmGroup">The name of the algorithm group to initialize.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="algorithmGroup" /> parameter is null.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="algorithmGroup" /> parameter length is 0 (zero).</exception>
		// Token: 0x060001FF RID: 511 RVA: 0x000056C0 File Offset: 0x000038C0
		public CngAlgorithmGroup(string algorithmGroup)
		{
			if (algorithmGroup == null)
			{
				throw new ArgumentNullException("algorithmGroup");
			}
			if (algorithmGroup.Length == 0)
			{
				throw new ArgumentException(global::SR.GetString("The algorithm group '{0}' is invalid.", new object[] { algorithmGroup }), "algorithmGroup");
			}
			this.m_algorithmGroup = algorithmGroup;
		}

		/// <summary>Gets the name of the algorithm group that the current <see cref="T:System.Security.Cryptography.CngAlgorithm" /> object specifies.</summary>
		/// <returns>The embedded algorithm group name.</returns>
		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000200 RID: 512 RVA: 0x0000570F File Offset: 0x0000390F
		public string AlgorithmGroup
		{
			get
			{
				return this.m_algorithmGroup;
			}
		}

		/// <summary>Determines whether two <see cref="T:System.Security.Cryptography.CngAlgorithmGroup" /> objects specify the same algorithm group.</summary>
		/// <returns>true if the two objects specify the same algorithm group; otherwise, false.</returns>
		/// <param name="left">An object that specifies an algorithm group.</param>
		/// <param name="right">A second object, to be compared to the object that is identified by the <paramref name="left" /> parameter.</param>
		// Token: 0x06000201 RID: 513 RVA: 0x00005717 File Offset: 0x00003917
		public static bool operator ==(CngAlgorithmGroup left, CngAlgorithmGroup right)
		{
			if (left == null)
			{
				return right == null;
			}
			return left.Equals(right);
		}

		/// <summary>Determines whether two <see cref="T:System.Security.Cryptography.CngAlgorithmGroup" /> objects do not specify the same algorithm group.</summary>
		/// <returns>true if the two objects do not specify the same algorithm group; otherwise, false. </returns>
		/// <param name="left">An object that specifies an algorithm group.</param>
		/// <param name="right">A second object, to be compared to the object that is identified by the <paramref name="left" /> parameter.</param>
		// Token: 0x06000202 RID: 514 RVA: 0x00005728 File Offset: 0x00003928
		public static bool operator !=(CngAlgorithmGroup left, CngAlgorithmGroup right)
		{
			if (left == null)
			{
				return right != null;
			}
			return !left.Equals(right);
		}

		/// <summary>Compares the specified object to the current <see cref="T:System.Security.Cryptography.CngAlgorithmGroup" /> object.</summary>
		/// <returns>true if the <paramref name="obj" /> parameter is a <see cref="T:System.Security.Cryptography.CngAlgorithmGroup" /> that specifies the same algorithm group as the current object; otherwise, false.</returns>
		/// <param name="obj">An object to be compared to the current <see cref="T:System.Security.Cryptography.CngAlgorithmGroup" /> object.</param>
		// Token: 0x06000203 RID: 515 RVA: 0x0000573C File Offset: 0x0000393C
		public override bool Equals(object obj)
		{
			return this.Equals(obj as CngAlgorithmGroup);
		}

		/// <summary>Compares the specified <see cref="T:System.Security.Cryptography.CngAlgorithmGroup" /> object to the current <see cref="T:System.Security.Cryptography.CngAlgorithmGroup" /> object.</summary>
		/// <returns>true if the <paramref name="other" /> parameter specifies the same algorithm group as the current object; otherwise, false.</returns>
		/// <param name="other">An object to be compared to the current <see cref="T:System.Security.Cryptography.CngAlgorithmGroup" /> object.</param>
		// Token: 0x06000204 RID: 516 RVA: 0x0000574A File Offset: 0x0000394A
		public bool Equals(CngAlgorithmGroup other)
		{
			return other != null && this.m_algorithmGroup.Equals(other.AlgorithmGroup);
		}

		/// <summary>Generates a hash value for the algorithm group name that is embedded in the current <see cref="T:System.Security.Cryptography.CngAlgorithmGroup" /> object.</summary>
		/// <returns>The hash value of the embedded algorithm group name.</returns>
		// Token: 0x06000205 RID: 517 RVA: 0x00005762 File Offset: 0x00003962
		public override int GetHashCode()
		{
			return this.m_algorithmGroup.GetHashCode();
		}

		/// <summary>Gets the name of the algorithm group that the current <see cref="T:System.Security.Cryptography.CngAlgorithm" /> object specifies.</summary>
		/// <returns>The embedded algorithm group name.</returns>
		// Token: 0x06000206 RID: 518 RVA: 0x0000570F File Offset: 0x0000390F
		public override string ToString()
		{
			return this.m_algorithmGroup;
		}

		/// <summary>Gets a <see cref="T:System.Security.Cryptography.CngAlgorithmGroup" /> object that specifies the Diffie-Hellman family of algorithms.</summary>
		/// <returns>An object that specifies the Diffie-Hellman family of algorithms.</returns>
		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000207 RID: 519 RVA: 0x0000576F File Offset: 0x0000396F
		public static CngAlgorithmGroup DiffieHellman
		{
			get
			{
				if (CngAlgorithmGroup.s_dh == null)
				{
					CngAlgorithmGroup.s_dh = new CngAlgorithmGroup("DH");
				}
				return CngAlgorithmGroup.s_dh;
			}
		}

		/// <summary>Gets a <see cref="T:System.Security.Cryptography.CngAlgorithmGroup" /> object that specifies the Digital Signature Algorithm (DSA) family of algorithms.</summary>
		/// <returns>An object that specifies the DSA family of algorithms.</returns>
		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000208 RID: 520 RVA: 0x00005798 File Offset: 0x00003998
		public static CngAlgorithmGroup Dsa
		{
			get
			{
				if (CngAlgorithmGroup.s_dsa == null)
				{
					CngAlgorithmGroup.s_dsa = new CngAlgorithmGroup("DSA");
				}
				return CngAlgorithmGroup.s_dsa;
			}
		}

		/// <summary>Gets a <see cref="T:System.Security.Cryptography.CngAlgorithmGroup" /> object that specifies the Elliptic Curve Diffie-Hellman (ECDH) family of algorithms.</summary>
		/// <returns>An object that specifies the ECDH family of algorithms.</returns>
		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000209 RID: 521 RVA: 0x000057C1 File Offset: 0x000039C1
		public static CngAlgorithmGroup ECDiffieHellman
		{
			get
			{
				if (CngAlgorithmGroup.s_ecdh == null)
				{
					CngAlgorithmGroup.s_ecdh = new CngAlgorithmGroup("ECDH");
				}
				return CngAlgorithmGroup.s_ecdh;
			}
		}

		/// <summary>Gets a <see cref="T:System.Security.Cryptography.CngAlgorithmGroup" /> object that specifies the Elliptic Curve Digital Signature Algorithm (ECDSA) family of algorithms.</summary>
		/// <returns>An object that specifies the ECDSA family of algorithms.</returns>
		// Token: 0x1700006A RID: 106
		// (get) Token: 0x0600020A RID: 522 RVA: 0x000057EA File Offset: 0x000039EA
		public static CngAlgorithmGroup ECDsa
		{
			get
			{
				if (CngAlgorithmGroup.s_ecdsa == null)
				{
					CngAlgorithmGroup.s_ecdsa = new CngAlgorithmGroup("ECDSA");
				}
				return CngAlgorithmGroup.s_ecdsa;
			}
		}

		/// <summary>Gets a <see cref="T:System.Security.Cryptography.CngAlgorithmGroup" /> object that specifies the Rivest-Shamir-Adleman (RSA) family of algorithms.</summary>
		/// <returns>An object that specifies the RSA family of algorithms.</returns>
		// Token: 0x1700006B RID: 107
		// (get) Token: 0x0600020B RID: 523 RVA: 0x00005813 File Offset: 0x00003A13
		public static CngAlgorithmGroup Rsa
		{
			get
			{
				if (CngAlgorithmGroup.s_rsa == null)
				{
					CngAlgorithmGroup.s_rsa = new CngAlgorithmGroup("RSA");
				}
				return CngAlgorithmGroup.s_rsa;
			}
		}

		// Token: 0x04000296 RID: 662
		private static volatile CngAlgorithmGroup s_dh;

		// Token: 0x04000297 RID: 663
		private static volatile CngAlgorithmGroup s_dsa;

		// Token: 0x04000298 RID: 664
		private static volatile CngAlgorithmGroup s_ecdh;

		// Token: 0x04000299 RID: 665
		private static volatile CngAlgorithmGroup s_ecdsa;

		// Token: 0x0400029A RID: 666
		private static volatile CngAlgorithmGroup s_rsa;

		// Token: 0x0400029B RID: 667
		private string m_algorithmGroup;
	}
}
