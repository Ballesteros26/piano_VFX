using System;
using System.Security.Permissions;

namespace System.Security.Cryptography
{
	/// <summary>Encapsulates the name of an encryption algorithm. </summary>
	// Token: 0x02000060 RID: 96
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	[Serializable]
	public sealed class CngAlgorithm : IEquatable<CngAlgorithm>
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.CngAlgorithm" /> class.</summary>
		/// <param name="algorithm">The name of the algorithm to initialize.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="algorithm" /> parameter is null.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="algorithm" /> parameter length is 0 (zero).</exception>
		// Token: 0x060001E9 RID: 489 RVA: 0x000053D0 File Offset: 0x000035D0
		public CngAlgorithm(string algorithm)
		{
			if (algorithm == null)
			{
				throw new ArgumentNullException("algorithm");
			}
			if (algorithm.Length == 0)
			{
				throw new ArgumentException(global::SR.GetString("The algorithm name '{0}' is invalid.", new object[] { algorithm }), "algorithm");
			}
			this.m_algorithm = algorithm;
		}

		/// <summary>Gets the algorithm name that the current <see cref="T:System.Security.Cryptography.CngAlgorithm" /> object specifies.</summary>
		/// <returns>The embedded algorithm name.</returns>
		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060001EA RID: 490 RVA: 0x0000541F File Offset: 0x0000361F
		public string Algorithm
		{
			get
			{
				return this.m_algorithm;
			}
		}

		/// <summary>Determines whether two <see cref="T:System.Security.Cryptography.CngAlgorithm" /> objects specify the same algorithm name.</summary>
		/// <returns>true if the two objects specify the same algorithm name; otherwise, false.</returns>
		/// <param name="left">An object that specifies an algorithm name.</param>
		/// <param name="right">A second object, to be compared to the object that is identified by the <paramref name="left" /> parameter.</param>
		// Token: 0x060001EB RID: 491 RVA: 0x00005427 File Offset: 0x00003627
		public static bool operator ==(CngAlgorithm left, CngAlgorithm right)
		{
			if (left == null)
			{
				return right == null;
			}
			return left.Equals(right);
		}

		/// <summary>Determines whether two <see cref="T:System.Security.Cryptography.CngAlgorithm" /> objects do not specify the same algorithm.</summary>
		/// <returns>true if the two objects do not specify the same algorithm name; otherwise, false.</returns>
		/// <param name="left">An object that specifies an algorithm name.</param>
		/// <param name="right">A second object, to be compared to the object that is identified by the <paramref name="left" /> parameter.</param>
		// Token: 0x060001EC RID: 492 RVA: 0x00005438 File Offset: 0x00003638
		public static bool operator !=(CngAlgorithm left, CngAlgorithm right)
		{
			if (left == null)
			{
				return right != null;
			}
			return !left.Equals(right);
		}

		/// <summary>Compares the specified object to the current <see cref="T:System.Security.Cryptography.CngAlgorithm" /> object.</summary>
		/// <returns>true if the <paramref name="obj" /> parameter is a <see cref="T:System.Security.Cryptography.CngAlgorithm" /> that specifies the same algorithm as the current object; otherwise, false.</returns>
		/// <param name="obj">An object to be compared to the current <see cref="T:System.Security.Cryptography.CngAlgorithm" /> object.</param>
		// Token: 0x060001ED RID: 493 RVA: 0x0000544C File Offset: 0x0000364C
		public override bool Equals(object obj)
		{
			return this.Equals(obj as CngAlgorithm);
		}

		/// <summary>Compares the specified <see cref="T:System.Security.Cryptography.CngAlgorithm" /> object to the current <see cref="T:System.Security.Cryptography.CngAlgorithm" /> object. </summary>
		/// <returns>true if the <paramref name="other" /> parameter specifies the same algorithm as the current object; otherwise, false.</returns>
		/// <param name="other">An object to be compared to the current <see cref="T:System.Security.Cryptography.CngAlgorithm" /> object.</param>
		// Token: 0x060001EE RID: 494 RVA: 0x0000545A File Offset: 0x0000365A
		public bool Equals(CngAlgorithm other)
		{
			return other != null && this.m_algorithm.Equals(other.Algorithm);
		}

		/// <summary>Generates a hash value for the algorithm name that is embedded in the current <see cref="T:System.Security.Cryptography.CngAlgorithm" /> object.</summary>
		/// <returns>The hash value of the embedded algorithm name.</returns>
		// Token: 0x060001EF RID: 495 RVA: 0x00005472 File Offset: 0x00003672
		public override int GetHashCode()
		{
			return this.m_algorithm.GetHashCode();
		}

		/// <summary>Gets the name of the algorithm that the current <see cref="T:System.Security.Cryptography.CngAlgorithm" /> object specifies.</summary>
		/// <returns>The embedded algorithm name.</returns>
		// Token: 0x060001F0 RID: 496 RVA: 0x0000541F File Offset: 0x0000361F
		public override string ToString()
		{
			return this.m_algorithm;
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060001F1 RID: 497 RVA: 0x0000547F File Offset: 0x0000367F
		public static CngAlgorithm Rsa
		{
			get
			{
				if (CngAlgorithm.s_rsa == null)
				{
					CngAlgorithm.s_rsa = new CngAlgorithm("RSA");
				}
				return CngAlgorithm.s_rsa;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060001F2 RID: 498 RVA: 0x000054A8 File Offset: 0x000036A8
		public static CngAlgorithm ECDiffieHellman
		{
			get
			{
				if (CngAlgorithm.s_ecdh == null)
				{
					CngAlgorithm.s_ecdh = new CngAlgorithm("ECDH");
				}
				return CngAlgorithm.s_ecdh;
			}
		}

		/// <summary>Gets a <see cref="T:System.Security.Cryptography.CngAlgorithm" /> object that specifies an Elliptic Curve Diffie-Hellman (ECDH) key exchange algorithm that uses the P-256 curve.</summary>
		/// <returns>An object that specifies an ECDH algorithm that uses the P-256 curve.</returns>
		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060001F3 RID: 499 RVA: 0x000054D1 File Offset: 0x000036D1
		public static CngAlgorithm ECDiffieHellmanP256
		{
			get
			{
				if (CngAlgorithm.s_ecdhp256 == null)
				{
					CngAlgorithm.s_ecdhp256 = new CngAlgorithm("ECDH_P256");
				}
				return CngAlgorithm.s_ecdhp256;
			}
		}

		/// <summary>Gets a <see cref="T:System.Security.Cryptography.CngAlgorithm" /> object that specifies an Elliptic Curve Diffie-Hellman (ECDH) key exchange algorithm that uses the P-384 curve.</summary>
		/// <returns>An object that specifies an ECDH algorithm that uses the P-384 curve.</returns>
		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060001F4 RID: 500 RVA: 0x000054FA File Offset: 0x000036FA
		public static CngAlgorithm ECDiffieHellmanP384
		{
			get
			{
				if (CngAlgorithm.s_ecdhp384 == null)
				{
					CngAlgorithm.s_ecdhp384 = new CngAlgorithm("ECDH_P384");
				}
				return CngAlgorithm.s_ecdhp384;
			}
		}

		/// <summary>Gets a <see cref="T:System.Security.Cryptography.CngAlgorithm" /> object that specifies an Elliptic Curve Diffie-Hellman (ECDH) key exchange algorithm that uses the P-521 curve.</summary>
		/// <returns>An object that specifies an ECDH algorithm that uses the P-521 curve.</returns>
		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060001F5 RID: 501 RVA: 0x00005523 File Offset: 0x00003723
		public static CngAlgorithm ECDiffieHellmanP521
		{
			get
			{
				if (CngAlgorithm.s_ecdhp521 == null)
				{
					CngAlgorithm.s_ecdhp521 = new CngAlgorithm("ECDH_P521");
				}
				return CngAlgorithm.s_ecdhp521;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060001F6 RID: 502 RVA: 0x0000554C File Offset: 0x0000374C
		public static CngAlgorithm ECDsa
		{
			get
			{
				if (CngAlgorithm.s_ecdsa == null)
				{
					CngAlgorithm.s_ecdsa = new CngAlgorithm("ECDSA");
				}
				return CngAlgorithm.s_ecdsa;
			}
		}

		/// <summary>Gets a <see cref="T:System.Security.Cryptography.CngAlgorithm" /> object that specifies an Elliptic Curve Digital Signature Algorithm (ECDSA) that uses the P-256 curve.</summary>
		/// <returns>An object that specifies an ECDSA algorithm that uses the P-256 curve.</returns>
		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060001F7 RID: 503 RVA: 0x00005575 File Offset: 0x00003775
		public static CngAlgorithm ECDsaP256
		{
			get
			{
				if (CngAlgorithm.s_ecdsap256 == null)
				{
					CngAlgorithm.s_ecdsap256 = new CngAlgorithm("ECDSA_P256");
				}
				return CngAlgorithm.s_ecdsap256;
			}
		}

		/// <summary>Gets a <see cref="T:System.Security.Cryptography.CngAlgorithm" /> object that specifies an Elliptic Curve Digital Signature Algorithm (ECDSA) that uses the P-384 curve.</summary>
		/// <returns>An object that specifies an ECDSA algorithm that uses the P-384 curve.</returns>
		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060001F8 RID: 504 RVA: 0x0000559E File Offset: 0x0000379E
		public static CngAlgorithm ECDsaP384
		{
			get
			{
				if (CngAlgorithm.s_ecdsap384 == null)
				{
					CngAlgorithm.s_ecdsap384 = new CngAlgorithm("ECDSA_P384");
				}
				return CngAlgorithm.s_ecdsap384;
			}
		}

		/// <summary>Gets a new <see cref="T:System.Security.Cryptography.CngAlgorithm" /> object that specifies an Elliptic Curve Digital Signature Algorithm (ECDSA) that uses the P-521 curve.</summary>
		/// <returns>An object that specifies an ECDSA algorithm that uses the P-521 curve.</returns>
		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060001F9 RID: 505 RVA: 0x000055C7 File Offset: 0x000037C7
		public static CngAlgorithm ECDsaP521
		{
			get
			{
				if (CngAlgorithm.s_ecdsap521 == null)
				{
					CngAlgorithm.s_ecdsap521 = new CngAlgorithm("ECDSA_P521");
				}
				return CngAlgorithm.s_ecdsap521;
			}
		}

		/// <summary>Gets a new <see cref="T:System.Security.Cryptography.CngAlgorithm" /> object that specifies the Message Digest 5 (MD5) hash algorithm.</summary>
		/// <returns>An object that specifies the MD5 algorithm.</returns>
		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060001FA RID: 506 RVA: 0x000055F0 File Offset: 0x000037F0
		public static CngAlgorithm MD5
		{
			get
			{
				if (CngAlgorithm.s_md5 == null)
				{
					CngAlgorithm.s_md5 = new CngAlgorithm("MD5");
				}
				return CngAlgorithm.s_md5;
			}
		}

		/// <summary>Gets a new <see cref="T:System.Security.Cryptography.CngAlgorithm" /> object that specifies the Secure Hash Algorithm 1 (SHA-1) algorithm.</summary>
		/// <returns>An object that specifies the SHA-1 algorithm.</returns>
		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060001FB RID: 507 RVA: 0x00005619 File Offset: 0x00003819
		public static CngAlgorithm Sha1
		{
			get
			{
				if (CngAlgorithm.s_sha1 == null)
				{
					CngAlgorithm.s_sha1 = new CngAlgorithm("SHA1");
				}
				return CngAlgorithm.s_sha1;
			}
		}

		/// <summary>Gets a new <see cref="T:System.Security.Cryptography.CngAlgorithm" /> object that specifies the Secure Hash Algorithm 256 (SHA-256) algorithm.</summary>
		/// <returns>An object that specifies the SHA-256 algorithm.</returns>
		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060001FC RID: 508 RVA: 0x00005642 File Offset: 0x00003842
		public static CngAlgorithm Sha256
		{
			get
			{
				if (CngAlgorithm.s_sha256 == null)
				{
					CngAlgorithm.s_sha256 = new CngAlgorithm("SHA256");
				}
				return CngAlgorithm.s_sha256;
			}
		}

		/// <summary>Gets a new <see cref="T:System.Security.Cryptography.CngAlgorithm" /> object that specifies the Secure Hash Algorithm 384 (SHA-384) algorithm.</summary>
		/// <returns>An object that specifies the SHA-384 algorithm.</returns>
		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060001FD RID: 509 RVA: 0x0000566B File Offset: 0x0000386B
		public static CngAlgorithm Sha384
		{
			get
			{
				if (CngAlgorithm.s_sha384 == null)
				{
					CngAlgorithm.s_sha384 = new CngAlgorithm("SHA384");
				}
				return CngAlgorithm.s_sha384;
			}
		}

		/// <summary>Gets a new <see cref="T:System.Security.Cryptography.CngAlgorithm" /> object that specifies the Secure Hash Algorithm 512 (SHA-512) algorithm.</summary>
		/// <returns>An object that specifies the SHA-512 algorithm.</returns>
		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060001FE RID: 510 RVA: 0x00005694 File Offset: 0x00003894
		public static CngAlgorithm Sha512
		{
			get
			{
				if (CngAlgorithm.s_sha512 == null)
				{
					CngAlgorithm.s_sha512 = new CngAlgorithm("SHA512");
				}
				return CngAlgorithm.s_sha512;
			}
		}

		// Token: 0x04000287 RID: 647
		private static volatile CngAlgorithm s_ecdh;

		// Token: 0x04000288 RID: 648
		private static volatile CngAlgorithm s_ecdhp256;

		// Token: 0x04000289 RID: 649
		private static volatile CngAlgorithm s_ecdhp384;

		// Token: 0x0400028A RID: 650
		private static volatile CngAlgorithm s_ecdhp521;

		// Token: 0x0400028B RID: 651
		private static volatile CngAlgorithm s_ecdsa;

		// Token: 0x0400028C RID: 652
		private static volatile CngAlgorithm s_ecdsap256;

		// Token: 0x0400028D RID: 653
		private static volatile CngAlgorithm s_ecdsap384;

		// Token: 0x0400028E RID: 654
		private static volatile CngAlgorithm s_ecdsap521;

		// Token: 0x0400028F RID: 655
		private static volatile CngAlgorithm s_md5;

		// Token: 0x04000290 RID: 656
		private static volatile CngAlgorithm s_sha1;

		// Token: 0x04000291 RID: 657
		private static volatile CngAlgorithm s_sha256;

		// Token: 0x04000292 RID: 658
		private static volatile CngAlgorithm s_sha384;

		// Token: 0x04000293 RID: 659
		private static volatile CngAlgorithm s_sha512;

		// Token: 0x04000294 RID: 660
		private static volatile CngAlgorithm s_rsa;

		// Token: 0x04000295 RID: 661
		private string m_algorithm;
	}
}
