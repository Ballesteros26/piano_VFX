using System;
using System.IO;
using System.Security.Permissions;

namespace System.Security.Cryptography
{
	/// <summary>Provides an abstract base class that encapsulates the Elliptic Curve Digital Signature Algorithm (ECDSA).</summary>
	// Token: 0x0200006C RID: 108
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public abstract class ECDsa : AsymmetricAlgorithm
	{
		/// <summary>Gets the name of the key exchange algorithm.</summary>
		/// <returns>Always null.</returns>
		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000288 RID: 648 RVA: 0x00005E51 File Offset: 0x00004051
		public override string KeyExchangeAlgorithm
		{
			get
			{
				return null;
			}
		}

		/// <summary>Gets the name of the signature algorithm.</summary>
		/// <returns>The string "ECDsa".</returns>
		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000289 RID: 649 RVA: 0x00005F99 File Offset: 0x00004199
		public override string SignatureAlgorithm
		{
			get
			{
				return "ECDsa";
			}
		}

		/// <summary>Creates a new instance of the default implementation of the Elliptic Curve Digital Signature Algorithm (ECDSA).</summary>
		/// <returns>A new instance of the default implementation (<see cref="T:System.Security.Cryptography.ECDsaCng" />) of this class.</returns>
		// Token: 0x0600028A RID: 650 RVA: 0x0000227E File Offset: 0x0000047E
		public new static ECDsa Create()
		{
			throw new NotImplementedException();
		}

		/// <summary>Creates a new instance of the specified implementation of the Elliptic Curve Digital Signature Algorithm (ECDSA).</summary>
		/// <returns>A new instance of the specified implementation of this class. If the specified algorithm name does not map to an ECDSA implementation, this method returns null. </returns>
		/// <param name="algorithm">The name of an ECDSA implementation. The following strings all refer to the same implementation, which is the only implementation currently supported in the .NET Framework:- "ECDsa"- "ECDsaCng"- "System.Security.Cryptography.ECDsaCng"You can also provide the name of a custom ECDSA implementation.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="algorithm" /> parameter is null.</exception>
		// Token: 0x0600028B RID: 651 RVA: 0x00005FA0 File Offset: 0x000041A0
		public new static ECDsa Create(string algorithm)
		{
			if (algorithm == null)
			{
				throw new ArgumentNullException("algorithm");
			}
			return CryptoConfig.CreateFromName(algorithm) as ECDsa;
		}

		// Token: 0x0600028C RID: 652 RVA: 0x00005FBC File Offset: 0x000041BC
		public static ECDsa Create(ECCurve curve)
		{
			ECDsa ecdsa = ECDsa.Create();
			if (ecdsa != null)
			{
				try
				{
					ecdsa.GenerateKey(curve);
				}
				catch
				{
					ecdsa.Dispose();
					throw;
				}
			}
			return ecdsa;
		}

		// Token: 0x0600028D RID: 653 RVA: 0x00005FF8 File Offset: 0x000041F8
		public static ECDsa Create(ECParameters parameters)
		{
			ECDsa ecdsa = ECDsa.Create();
			if (ecdsa != null)
			{
				try
				{
					ecdsa.ImportParameters(parameters);
				}
				catch
				{
					ecdsa.Dispose();
					throw;
				}
			}
			return ecdsa;
		}

		/// <summary>Generates a digital signature for the specified hash value. </summary>
		/// <returns>A digital signature that consists of the given hash value encrypted with the private key.</returns>
		/// <param name="hash">The hash value of the data that is being signed.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="hash" /> parameter is null.</exception>
		// Token: 0x0600028E RID: 654
		public abstract byte[] SignHash(byte[] hash);

		/// <summary>Verifies a digital signature against the specified hash value. </summary>
		/// <returns>true if the hash value equals the decrypted signature; otherwise, false.</returns>
		/// <param name="hash">The hash value of a block of data.</param>
		/// <param name="signature">The digital signature to be verified.</param>
		// Token: 0x0600028F RID: 655
		public abstract bool VerifyHash(byte[] hash, byte[] signature);

		// Token: 0x06000290 RID: 656 RVA: 0x00006034 File Offset: 0x00004234
		protected virtual byte[] HashData(byte[] data, int offset, int count, HashAlgorithmName hashAlgorithm)
		{
			throw ECDsa.DerivedClassMustOverride();
		}

		// Token: 0x06000291 RID: 657 RVA: 0x00006034 File Offset: 0x00004234
		protected virtual byte[] HashData(Stream data, HashAlgorithmName hashAlgorithm)
		{
			throw ECDsa.DerivedClassMustOverride();
		}

		// Token: 0x06000292 RID: 658 RVA: 0x0000603B File Offset: 0x0000423B
		public virtual byte[] SignData(byte[] data, HashAlgorithmName hashAlgorithm)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			return this.SignData(data, 0, data.Length, hashAlgorithm);
		}

		// Token: 0x06000293 RID: 659 RVA: 0x00006058 File Offset: 0x00004258
		public virtual byte[] SignData(byte[] data, int offset, int count, HashAlgorithmName hashAlgorithm)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			if (offset < 0 || offset > data.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (count < 0 || count > data.Length - offset)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (string.IsNullOrEmpty(hashAlgorithm.Name))
			{
				throw ECDsa.HashAlgorithmNameNullOrEmpty();
			}
			byte[] array = this.HashData(data, offset, count, hashAlgorithm);
			return this.SignHash(array);
		}

		// Token: 0x06000294 RID: 660 RVA: 0x000060C8 File Offset: 0x000042C8
		public virtual byte[] SignData(Stream data, HashAlgorithmName hashAlgorithm)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			if (string.IsNullOrEmpty(hashAlgorithm.Name))
			{
				throw ECDsa.HashAlgorithmNameNullOrEmpty();
			}
			byte[] array = this.HashData(data, hashAlgorithm);
			return this.SignHash(array);
		}

		// Token: 0x06000295 RID: 661 RVA: 0x00006107 File Offset: 0x00004307
		public bool VerifyData(byte[] data, byte[] signature, HashAlgorithmName hashAlgorithm)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			return this.VerifyData(data, 0, data.Length, signature, hashAlgorithm);
		}

		// Token: 0x06000296 RID: 662 RVA: 0x00006124 File Offset: 0x00004324
		public virtual bool VerifyData(byte[] data, int offset, int count, byte[] signature, HashAlgorithmName hashAlgorithm)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			if (offset < 0 || offset > data.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (count < 0 || count > data.Length - offset)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (signature == null)
			{
				throw new ArgumentNullException("signature");
			}
			if (string.IsNullOrEmpty(hashAlgorithm.Name))
			{
				throw ECDsa.HashAlgorithmNameNullOrEmpty();
			}
			byte[] array = this.HashData(data, offset, count, hashAlgorithm);
			return this.VerifyHash(array, signature);
		}

		// Token: 0x06000297 RID: 663 RVA: 0x000061A4 File Offset: 0x000043A4
		public bool VerifyData(Stream data, byte[] signature, HashAlgorithmName hashAlgorithm)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			if (signature == null)
			{
				throw new ArgumentNullException("signature");
			}
			if (string.IsNullOrEmpty(hashAlgorithm.Name))
			{
				throw ECDsa.HashAlgorithmNameNullOrEmpty();
			}
			byte[] array = this.HashData(data, hashAlgorithm);
			return this.VerifyHash(array, signature);
		}

		// Token: 0x06000298 RID: 664 RVA: 0x00005F19 File Offset: 0x00004119
		public virtual ECParameters ExportParameters(bool includePrivateParameters)
		{
			throw new NotSupportedException(global::SR.GetString("Method not supported. Derived class must override."));
		}

		// Token: 0x06000299 RID: 665 RVA: 0x00005F19 File Offset: 0x00004119
		public virtual ECParameters ExportExplicitParameters(bool includePrivateParameters)
		{
			throw new NotSupportedException(global::SR.GetString("Method not supported. Derived class must override."));
		}

		// Token: 0x0600029A RID: 666 RVA: 0x00005F19 File Offset: 0x00004119
		public virtual void ImportParameters(ECParameters parameters)
		{
			throw new NotSupportedException(global::SR.GetString("Method not supported. Derived class must override."));
		}

		// Token: 0x0600029B RID: 667 RVA: 0x00005F19 File Offset: 0x00004119
		public virtual void GenerateKey(ECCurve curve)
		{
			throw new NotSupportedException(global::SR.GetString("Method not supported. Derived class must override."));
		}

		// Token: 0x0600029C RID: 668 RVA: 0x00005F08 File Offset: 0x00004108
		private static Exception DerivedClassMustOverride()
		{
			return new NotImplementedException(global::SR.GetString("Method not supported. Derived class must override."));
		}

		// Token: 0x0600029D RID: 669 RVA: 0x000061F2 File Offset: 0x000043F2
		internal static Exception HashAlgorithmNameNullOrEmpty()
		{
			return new ArgumentException(global::SR.GetString("The hash algorithm name cannot be null or empty."), "hashAlgorithm");
		}
	}
}
