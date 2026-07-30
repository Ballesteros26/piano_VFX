using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Represents the abstract base class from which all implementations of asymmetric algorithms must inherit.</summary>
	// Token: 0x02000643 RID: 1603
	[ComVisible(true)]
	public abstract class AsymmetricAlgorithm : IDisposable
	{
		/// <summary>Releases all resources used by the current instance of the <see cref="T:System.Security.Cryptography.AsymmetricAlgorithm" /> class.</summary>
		// Token: 0x06004596 RID: 17814 RVA: 0x000F4B47 File Offset: 0x000F2D47
		public void Dispose()
		{
			this.Clear();
		}

		/// <summary>Releases all resources used by the <see cref="T:System.Security.Cryptography.AsymmetricAlgorithm" /> class.</summary>
		// Token: 0x06004597 RID: 17815 RVA: 0x000F4B4F File Offset: 0x000F2D4F
		public void Clear()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Security.Cryptography.AsymmetricAlgorithm" /> class and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x06004598 RID: 17816 RVA: 0x00002194 File Offset: 0x00000394
		protected virtual void Dispose(bool disposing)
		{
		}

		/// <summary>Gets or sets the size, in bits, of the key modulus used by the asymmetric algorithm.</summary>
		/// <returns>The size, in bits, of the key modulus used by the asymmetric algorithm.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The key modulus size is invalid. </exception>
		// Token: 0x17000BBE RID: 3006
		// (get) Token: 0x06004599 RID: 17817 RVA: 0x000F4B5E File Offset: 0x000F2D5E
		// (set) Token: 0x0600459A RID: 17818 RVA: 0x000F4B68 File Offset: 0x000F2D68
		public virtual int KeySize
		{
			get
			{
				return this.KeySizeValue;
			}
			set
			{
				for (int i = 0; i < this.LegalKeySizesValue.Length; i++)
				{
					if (this.LegalKeySizesValue[i].SkipSize == 0)
					{
						if (this.LegalKeySizesValue[i].MinSize == value)
						{
							this.KeySizeValue = value;
							return;
						}
					}
					else
					{
						for (int j = this.LegalKeySizesValue[i].MinSize; j <= this.LegalKeySizesValue[i].MaxSize; j += this.LegalKeySizesValue[i].SkipSize)
						{
							if (j == value)
							{
								this.KeySizeValue = value;
								return;
							}
						}
					}
				}
				throw new CryptographicException(Environment.GetResourceString("Specified key is not a valid size for this algorithm."));
			}
		}

		/// <summary>Gets the key sizes that are supported by the asymmetric algorithm.</summary>
		/// <returns>An array that contains the key sizes supported by the asymmetric algorithm.</returns>
		// Token: 0x17000BBF RID: 3007
		// (get) Token: 0x0600459B RID: 17819 RVA: 0x000F4BFA File Offset: 0x000F2DFA
		public virtual KeySizes[] LegalKeySizes
		{
			get
			{
				return (KeySizes[])this.LegalKeySizesValue.Clone();
			}
		}

		/// <summary>Gets the name of the signature algorithm.</summary>
		/// <returns>The name of the signature algorithm.</returns>
		// Token: 0x17000BC0 RID: 3008
		// (get) Token: 0x0600459C RID: 17820 RVA: 0x0002126B File Offset: 0x0001F46B
		public virtual string SignatureAlgorithm
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets the name of the key exchange algorithm.</summary>
		/// <returns>The name of the key exchange algorithm.</returns>
		// Token: 0x17000BC1 RID: 3009
		// (get) Token: 0x0600459D RID: 17821 RVA: 0x0002126B File Offset: 0x0001F46B
		public virtual string KeyExchangeAlgorithm
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Creates a default cryptographic object used to perform the asymmetric algorithm.</summary>
		/// <returns>A new <see cref="T:System.Security.Cryptography.RSACryptoServiceProvider" /> instance, unless the default settings have been changed with the &lt;cryptoClass&gt; element.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x0600459E RID: 17822 RVA: 0x000F4C0C File Offset: 0x000F2E0C
		public static AsymmetricAlgorithm Create()
		{
			return AsymmetricAlgorithm.Create("System.Security.Cryptography.AsymmetricAlgorithm");
		}

		/// <summary>Creates an instance of the specified implementation of an asymmetric algorithm.</summary>
		/// <returns>A new instance of the specified asymmetric algorithm implementation.</returns>
		/// <param name="algName">The asymmetric algorithm implementation to use. The following table shows the valid values for the <paramref name="algName" /> parameter and the algorithms they map to.Parameter valueImplements System.Security.Cryptography.AsymmetricAlgorithm<see cref="T:System.Security.Cryptography.AsymmetricAlgorithm" />RSA<see cref="T:System.Security.Cryptography.RSA" />System.Security.Cryptography.RSA<see cref="T:System.Security.Cryptography.RSA" />DSA<see cref="T:System.Security.Cryptography.DSA" />System.Security.Cryptography.DSA<see cref="T:System.Security.Cryptography.DSA" />ECDsa<see cref="T:System.Security.Cryptography.ECDsa" />ECDsaCng<see cref="T:System.Security.Cryptography.ECDsaCng" />System.Security.Cryptography.ECDsaCng<see cref="T:System.Security.Cryptography.ECDsaCng" />ECDH<see cref="T:System.Security.Cryptography.ECDiffieHellman" />ECDiffieHellman<see cref="T:System.Security.Cryptography.ECDiffieHellman" />ECDiffieHellmanCng<see cref="T:System.Security.Cryptography.ECDiffieHellmanCng" />System.Security.Cryptography.ECDiffieHellmanCng<see cref="T:System.Security.Cryptography.ECDiffieHellmanCng" /></param>
		// Token: 0x0600459F RID: 17823 RVA: 0x000F4C18 File Offset: 0x000F2E18
		public static AsymmetricAlgorithm Create(string algName)
		{
			return (AsymmetricAlgorithm)CryptoConfig.CreateFromName(algName);
		}

		/// <summary>When overridden in a derived class, reconstructs an <see cref="T:System.Security.Cryptography.AsymmetricAlgorithm" /> object from an XML string.</summary>
		/// <param name="xmlString">The XML string to use to reconstruct the <see cref="T:System.Security.Cryptography.AsymmetricAlgorithm" /> object. </param>
		// Token: 0x060045A0 RID: 17824 RVA: 0x0002126B File Offset: 0x0001F46B
		public virtual void FromXmlString(string xmlString)
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, creates and returns an XML string representation of the current <see cref="T:System.Security.Cryptography.AsymmetricAlgorithm" /> object.</summary>
		/// <returns>An XML string encoding of the current <see cref="T:System.Security.Cryptography.AsymmetricAlgorithm" /> object.</returns>
		/// <param name="includePrivateParameters">true to include private parameters; otherwise, false. </param>
		// Token: 0x060045A1 RID: 17825 RVA: 0x0002126B File Offset: 0x0001F46B
		public virtual string ToXmlString(bool includePrivateParameters)
		{
			throw new NotImplementedException();
		}

		/// <summary>Represents the size, in bits, of the key modulus used by the asymmetric algorithm.</summary>
		// Token: 0x040023CE RID: 9166
		protected int KeySizeValue;

		/// <summary>Specifies the key sizes that are supported by the asymmetric algorithm.</summary>
		// Token: 0x040023CF RID: 9167
		protected KeySizes[] LegalKeySizesValue;
	}
}
