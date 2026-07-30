using System;
using System.Security.Permissions;

namespace System.Security.Cryptography
{
	/// <summary>Provides an abstract base class that Elliptic Curve Diffie-Hellman (ECDH) algorithm implementations can derive from. This class provides the basic set of operations that all ECDH implementations must support.</summary>
	// Token: 0x0200006A RID: 106
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public abstract class ECDiffieHellman : AsymmetricAlgorithm
	{
		/// <summary>Gets the name of the key exchange algorithm.</summary>
		/// <returns>The name of the key exchange algorithm. </returns>
		// Token: 0x17000096 RID: 150
		// (get) Token: 0x0600026D RID: 621 RVA: 0x00005E4A File Offset: 0x0000404A
		public override string KeyExchangeAlgorithm
		{
			get
			{
				return "ECDiffieHellman";
			}
		}

		/// <summary>Gets the name of the signature algorithm.</summary>
		/// <returns>Always null.</returns>
		// Token: 0x17000097 RID: 151
		// (get) Token: 0x0600026E RID: 622 RVA: 0x00005E51 File Offset: 0x00004051
		public override string SignatureAlgorithm
		{
			get
			{
				return null;
			}
		}

		/// <summary>Creates a new instance of the default implementation of the Elliptic Curve Diffie-Hellman (ECDH) algorithm.</summary>
		/// <returns>A new instance of the default implementation of this class.</returns>
		// Token: 0x0600026F RID: 623 RVA: 0x0000227E File Offset: 0x0000047E
		public new static ECDiffieHellman Create()
		{
			throw new NotImplementedException();
		}

		/// <summary>Creates a new instance of the specified implementation of the Elliptic Curve Diffie-Hellman (ECDH) algorithm.</summary>
		/// <returns>A new instance of the specified implementation of this class. If the specified algorithm name does not map to an ECDH implementation, this method returns null.</returns>
		/// <param name="algorithm">The name of an implementation of the ECDH algorithm. The following strings all refer to the same implementation, which is the only implementation currently supported in the .NET Framework:- "ECDH"- "ECDiffieHellman"- "ECDiffieHellmanCng"- "System.Security.Cryptography.ECDiffieHellmanCng"You can also provide the name of a custom ECDH implementation.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="algorithm" /> parameter is null. </exception>
		// Token: 0x06000270 RID: 624 RVA: 0x00005E54 File Offset: 0x00004054
		public new static ECDiffieHellman Create(string algorithm)
		{
			if (algorithm == null)
			{
				throw new ArgumentNullException("algorithm");
			}
			return CryptoConfig.CreateFromName(algorithm) as ECDiffieHellman;
		}

		// Token: 0x06000271 RID: 625 RVA: 0x00005E70 File Offset: 0x00004070
		public static ECDiffieHellman Create(ECCurve curve)
		{
			ECDiffieHellman ecdiffieHellman = ECDiffieHellman.Create();
			if (ecdiffieHellman != null)
			{
				try
				{
					ecdiffieHellman.GenerateKey(curve);
				}
				catch
				{
					ecdiffieHellman.Dispose();
					throw;
				}
			}
			return ecdiffieHellman;
		}

		// Token: 0x06000272 RID: 626 RVA: 0x00005EAC File Offset: 0x000040AC
		public static ECDiffieHellman Create(ECParameters parameters)
		{
			ECDiffieHellman ecdiffieHellman = ECDiffieHellman.Create();
			if (ecdiffieHellman != null)
			{
				try
				{
					ecdiffieHellman.ImportParameters(parameters);
				}
				catch
				{
					ecdiffieHellman.Dispose();
					throw;
				}
			}
			return ecdiffieHellman;
		}

		/// <summary>Gets the public key that is being used by the current Elliptic Curve Diffie-Hellman (ECDH) instance.</summary>
		/// <returns>The public part of the ECDH key pair that is being used by this <see cref="T:System.Security.Cryptography.ECDiffieHellman" /> instance.</returns>
		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000273 RID: 627
		public abstract ECDiffieHellmanPublicKey PublicKey { get; }

		/// <summary>Derives bytes that can be used as a key, given another party's public key.</summary>
		/// <returns>The key material from the key exchange with the other party’s public key.</returns>
		/// <param name="otherPartyPublicKey">The other party's public key.</param>
		// Token: 0x06000274 RID: 628 RVA: 0x00005EE8 File Offset: 0x000040E8
		public virtual byte[] DeriveKeyMaterial(ECDiffieHellmanPublicKey otherPartyPublicKey)
		{
			throw ECDiffieHellman.DerivedClassMustOverride();
		}

		// Token: 0x06000275 RID: 629 RVA: 0x00005EEF File Offset: 0x000040EF
		public byte[] DeriveKeyFromHash(ECDiffieHellmanPublicKey otherPartyPublicKey, HashAlgorithmName hashAlgorithm)
		{
			return this.DeriveKeyFromHash(otherPartyPublicKey, hashAlgorithm, null, null);
		}

		// Token: 0x06000276 RID: 630 RVA: 0x00005EE8 File Offset: 0x000040E8
		public virtual byte[] DeriveKeyFromHash(ECDiffieHellmanPublicKey otherPartyPublicKey, HashAlgorithmName hashAlgorithm, byte[] secretPrepend, byte[] secretAppend)
		{
			throw ECDiffieHellman.DerivedClassMustOverride();
		}

		// Token: 0x06000277 RID: 631 RVA: 0x00005EFB File Offset: 0x000040FB
		public byte[] DeriveKeyFromHmac(ECDiffieHellmanPublicKey otherPartyPublicKey, HashAlgorithmName hashAlgorithm, byte[] hmacKey)
		{
			return this.DeriveKeyFromHmac(otherPartyPublicKey, hashAlgorithm, hmacKey, null, null);
		}

		// Token: 0x06000278 RID: 632 RVA: 0x00005EE8 File Offset: 0x000040E8
		public virtual byte[] DeriveKeyFromHmac(ECDiffieHellmanPublicKey otherPartyPublicKey, HashAlgorithmName hashAlgorithm, byte[] hmacKey, byte[] secretPrepend, byte[] secretAppend)
		{
			throw ECDiffieHellman.DerivedClassMustOverride();
		}

		// Token: 0x06000279 RID: 633 RVA: 0x00005EE8 File Offset: 0x000040E8
		public virtual byte[] DeriveKeyTls(ECDiffieHellmanPublicKey otherPartyPublicKey, byte[] prfLabel, byte[] prfSeed)
		{
			throw ECDiffieHellman.DerivedClassMustOverride();
		}

		// Token: 0x0600027A RID: 634 RVA: 0x00005F08 File Offset: 0x00004108
		private static Exception DerivedClassMustOverride()
		{
			return new NotImplementedException(global::SR.GetString("Method not supported. Derived class must override."));
		}

		// Token: 0x0600027B RID: 635 RVA: 0x00005EE8 File Offset: 0x000040E8
		public virtual ECParameters ExportParameters(bool includePrivateParameters)
		{
			throw ECDiffieHellman.DerivedClassMustOverride();
		}

		// Token: 0x0600027C RID: 636 RVA: 0x00005EE8 File Offset: 0x000040E8
		public virtual ECParameters ExportExplicitParameters(bool includePrivateParameters)
		{
			throw ECDiffieHellman.DerivedClassMustOverride();
		}

		// Token: 0x0600027D RID: 637 RVA: 0x00005EE8 File Offset: 0x000040E8
		public virtual void ImportParameters(ECParameters parameters)
		{
			throw ECDiffieHellman.DerivedClassMustOverride();
		}

		// Token: 0x0600027E RID: 638 RVA: 0x00005F19 File Offset: 0x00004119
		public virtual void GenerateKey(ECCurve curve)
		{
			throw new NotSupportedException(global::SR.GetString("Method not supported. Derived class must override."));
		}
	}
}
