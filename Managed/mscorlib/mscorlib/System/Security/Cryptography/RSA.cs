using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Util;
using System.Text;

namespace System.Security.Cryptography
{
	/// <summary>Represents the base class from which all implementations of the <see cref="T:System.Security.Cryptography.RSA" /> algorithm inherit.</summary>
	// Token: 0x0200067C RID: 1660
	[ComVisible(true)]
	public abstract class RSA : AsymmetricAlgorithm
	{
		/// <summary>Creates an instance of the default implementation of the <see cref="T:System.Security.Cryptography.RSA" /> algorithm.</summary>
		/// <returns>A new instance of the default implementation of <see cref="T:System.Security.Cryptography.RSA" />.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06004727 RID: 18215 RVA: 0x000FD9D8 File Offset: 0x000FBBD8
		public new static RSA Create()
		{
			return RSA.Create("System.Security.Cryptography.RSA");
		}

		/// <summary>Creates an instance of the specified implementation of <see cref="T:System.Security.Cryptography.RSA" />.</summary>
		/// <returns>A new instance of the specified implementation of <see cref="T:System.Security.Cryptography.RSA" />.</returns>
		/// <param name="algName">The name of the implementation of <see cref="T:System.Security.Cryptography.RSA" /> to use. </param>
		// Token: 0x06004728 RID: 18216 RVA: 0x000FD9E4 File Offset: 0x000FBBE4
		public new static RSA Create(string algName)
		{
			return (RSA)CryptoConfig.CreateFromName(algName);
		}

		// Token: 0x06004729 RID: 18217 RVA: 0x000FD9F1 File Offset: 0x000FBBF1
		public virtual byte[] Encrypt(byte[] data, RSAEncryptionPadding padding)
		{
			throw RSA.DerivedClassMustOverride();
		}

		// Token: 0x0600472A RID: 18218 RVA: 0x000FD9F1 File Offset: 0x000FBBF1
		public virtual byte[] Decrypt(byte[] data, RSAEncryptionPadding padding)
		{
			throw RSA.DerivedClassMustOverride();
		}

		// Token: 0x0600472B RID: 18219 RVA: 0x000FD9F1 File Offset: 0x000FBBF1
		public virtual byte[] SignHash(byte[] hash, HashAlgorithmName hashAlgorithm, RSASignaturePadding padding)
		{
			throw RSA.DerivedClassMustOverride();
		}

		// Token: 0x0600472C RID: 18220 RVA: 0x000FD9F1 File Offset: 0x000FBBF1
		public virtual bool VerifyHash(byte[] hash, byte[] signature, HashAlgorithmName hashAlgorithm, RSASignaturePadding padding)
		{
			throw RSA.DerivedClassMustOverride();
		}

		// Token: 0x0600472D RID: 18221 RVA: 0x000FD9F1 File Offset: 0x000FBBF1
		protected virtual byte[] HashData(byte[] data, int offset, int count, HashAlgorithmName hashAlgorithm)
		{
			throw RSA.DerivedClassMustOverride();
		}

		// Token: 0x0600472E RID: 18222 RVA: 0x000FD9F1 File Offset: 0x000FBBF1
		protected virtual byte[] HashData(Stream data, HashAlgorithmName hashAlgorithm)
		{
			throw RSA.DerivedClassMustOverride();
		}

		// Token: 0x0600472F RID: 18223 RVA: 0x000FD9F8 File Offset: 0x000FBBF8
		public byte[] SignData(byte[] data, HashAlgorithmName hashAlgorithm, RSASignaturePadding padding)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			return this.SignData(data, 0, data.Length, hashAlgorithm, padding);
		}

		// Token: 0x06004730 RID: 18224 RVA: 0x000FDA18 File Offset: 0x000FBC18
		public virtual byte[] SignData(byte[] data, int offset, int count, HashAlgorithmName hashAlgorithm, RSASignaturePadding padding)
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
				throw RSA.HashAlgorithmNameNullOrEmpty();
			}
			if (padding == null)
			{
				throw new ArgumentNullException("padding");
			}
			byte[] array = this.HashData(data, offset, count, hashAlgorithm);
			return this.SignHash(array, hashAlgorithm, padding);
		}

		// Token: 0x06004731 RID: 18225 RVA: 0x000FDAA0 File Offset: 0x000FBCA0
		public virtual byte[] SignData(Stream data, HashAlgorithmName hashAlgorithm, RSASignaturePadding padding)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			if (string.IsNullOrEmpty(hashAlgorithm.Name))
			{
				throw RSA.HashAlgorithmNameNullOrEmpty();
			}
			if (padding == null)
			{
				throw new ArgumentNullException("padding");
			}
			byte[] array = this.HashData(data, hashAlgorithm);
			return this.SignHash(array, hashAlgorithm, padding);
		}

		// Token: 0x06004732 RID: 18226 RVA: 0x000FDAF5 File Offset: 0x000FBCF5
		public bool VerifyData(byte[] data, byte[] signature, HashAlgorithmName hashAlgorithm, RSASignaturePadding padding)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			return this.VerifyData(data, 0, data.Length, signature, hashAlgorithm, padding);
		}

		// Token: 0x06004733 RID: 18227 RVA: 0x000FDB14 File Offset: 0x000FBD14
		public virtual bool VerifyData(byte[] data, int offset, int count, byte[] signature, HashAlgorithmName hashAlgorithm, RSASignaturePadding padding)
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
				throw RSA.HashAlgorithmNameNullOrEmpty();
			}
			if (padding == null)
			{
				throw new ArgumentNullException("padding");
			}
			byte[] array = this.HashData(data, offset, count, hashAlgorithm);
			return this.VerifyHash(array, signature, hashAlgorithm, padding);
		}

		// Token: 0x06004734 RID: 18228 RVA: 0x000FDBAC File Offset: 0x000FBDAC
		public bool VerifyData(Stream data, byte[] signature, HashAlgorithmName hashAlgorithm, RSASignaturePadding padding)
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
				throw RSA.HashAlgorithmNameNullOrEmpty();
			}
			if (padding == null)
			{
				throw new ArgumentNullException("padding");
			}
			byte[] array = this.HashData(data, hashAlgorithm);
			return this.VerifyHash(array, signature, hashAlgorithm, padding);
		}

		// Token: 0x06004735 RID: 18229 RVA: 0x000F76AD File Offset: 0x000F58AD
		private static Exception DerivedClassMustOverride()
		{
			return new NotImplementedException(Environment.GetResourceString("Derived classes must provide an implementation."));
		}

		// Token: 0x06004736 RID: 18230 RVA: 0x000F76BE File Offset: 0x000F58BE
		internal static Exception HashAlgorithmNameNullOrEmpty()
		{
			return new ArgumentException(Environment.GetResourceString("The hash algorithm name cannot be null or empty."), "hashAlgorithm");
		}

		/// <summary>When overridden in a derived class, decrypts the input data using the private key.</summary>
		/// <returns>The resulting decryption of the <paramref name="rgb" /> parameter in plain text.</returns>
		/// <param name="rgb">The cipher text to be decrypted. </param>
		// Token: 0x06004737 RID: 18231 RVA: 0x0007E376 File Offset: 0x0007C576
		public virtual byte[] DecryptValue(byte[] rgb)
		{
			throw new NotSupportedException(Environment.GetResourceString("Method is not supported."));
		}

		/// <summary>When overridden in a derived class, encrypts the input data using the public key.</summary>
		/// <returns>The resulting encryption of the <paramref name="rgb" /> parameter as cipher text.</returns>
		/// <param name="rgb">The plain text to be encrypted. </param>
		// Token: 0x06004738 RID: 18232 RVA: 0x0007E376 File Offset: 0x0007C576
		public virtual byte[] EncryptValue(byte[] rgb)
		{
			throw new NotSupportedException(Environment.GetResourceString("Method is not supported."));
		}

		// Token: 0x17000C03 RID: 3075
		// (get) Token: 0x06004739 RID: 18233 RVA: 0x000FDC12 File Offset: 0x000FBE12
		public override string KeyExchangeAlgorithm
		{
			get
			{
				return "RSA";
			}
		}

		// Token: 0x17000C04 RID: 3076
		// (get) Token: 0x0600473A RID: 18234 RVA: 0x000FDC12 File Offset: 0x000FBE12
		public override string SignatureAlgorithm
		{
			get
			{
				return "RSA";
			}
		}

		/// <summary>Initializes an <see cref="T:System.Security.Cryptography.RSA" /> object from the key information from an XML string.</summary>
		/// <param name="xmlString">The XML string containing <see cref="T:System.Security.Cryptography.RSA" /> key information. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="xmlString" /> parameter is null. </exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The format of the <paramref name="xmlString" /> parameter is not valid. </exception>
		// Token: 0x0600473B RID: 18235 RVA: 0x000FDC1C File Offset: 0x000FBE1C
		public override void FromXmlString(string xmlString)
		{
			if (xmlString == null)
			{
				throw new ArgumentNullException("xmlString");
			}
			RSAParameters rsaparameters = default(RSAParameters);
			SecurityElement topElement = new Parser(xmlString).GetTopElement();
			string text = topElement.SearchForTextOfLocalName("Modulus");
			if (text == null)
			{
				throw new CryptographicException(Environment.GetResourceString("Input string does not contain a valid encoding of the '{0}' '{1}' parameter.", new object[] { "RSA", "Modulus" }));
			}
			rsaparameters.Modulus = Convert.FromBase64String(Utils.DiscardWhiteSpaces(text));
			string text2 = topElement.SearchForTextOfLocalName("Exponent");
			if (text2 == null)
			{
				throw new CryptographicException(Environment.GetResourceString("Input string does not contain a valid encoding of the '{0}' '{1}' parameter.", new object[] { "RSA", "Exponent" }));
			}
			rsaparameters.Exponent = Convert.FromBase64String(Utils.DiscardWhiteSpaces(text2));
			string text3 = topElement.SearchForTextOfLocalName("P");
			if (text3 != null)
			{
				rsaparameters.P = Convert.FromBase64String(Utils.DiscardWhiteSpaces(text3));
			}
			string text4 = topElement.SearchForTextOfLocalName("Q");
			if (text4 != null)
			{
				rsaparameters.Q = Convert.FromBase64String(Utils.DiscardWhiteSpaces(text4));
			}
			string text5 = topElement.SearchForTextOfLocalName("DP");
			if (text5 != null)
			{
				rsaparameters.DP = Convert.FromBase64String(Utils.DiscardWhiteSpaces(text5));
			}
			string text6 = topElement.SearchForTextOfLocalName("DQ");
			if (text6 != null)
			{
				rsaparameters.DQ = Convert.FromBase64String(Utils.DiscardWhiteSpaces(text6));
			}
			string text7 = topElement.SearchForTextOfLocalName("InverseQ");
			if (text7 != null)
			{
				rsaparameters.InverseQ = Convert.FromBase64String(Utils.DiscardWhiteSpaces(text7));
			}
			string text8 = topElement.SearchForTextOfLocalName("D");
			if (text8 != null)
			{
				rsaparameters.D = Convert.FromBase64String(Utils.DiscardWhiteSpaces(text8));
			}
			this.ImportParameters(rsaparameters);
		}

		/// <summary>Creates and returns an XML string containing the key of the current <see cref="T:System.Security.Cryptography.RSA" /> object.</summary>
		/// <returns>An XML string containing the key of the current <see cref="T:System.Security.Cryptography.RSA" /> object.</returns>
		/// <param name="includePrivateParameters">true to include a public and private RSA key; false to include only the public key. </param>
		// Token: 0x0600473C RID: 18236 RVA: 0x000FDDB8 File Offset: 0x000FBFB8
		public override string ToXmlString(bool includePrivateParameters)
		{
			RSAParameters rsaparameters = this.ExportParameters(includePrivateParameters);
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("<RSAKeyValue>");
			stringBuilder.Append("<Modulus>" + Convert.ToBase64String(rsaparameters.Modulus) + "</Modulus>");
			stringBuilder.Append("<Exponent>" + Convert.ToBase64String(rsaparameters.Exponent) + "</Exponent>");
			if (includePrivateParameters)
			{
				stringBuilder.Append("<P>" + Convert.ToBase64String(rsaparameters.P) + "</P>");
				stringBuilder.Append("<Q>" + Convert.ToBase64String(rsaparameters.Q) + "</Q>");
				stringBuilder.Append("<DP>" + Convert.ToBase64String(rsaparameters.DP) + "</DP>");
				stringBuilder.Append("<DQ>" + Convert.ToBase64String(rsaparameters.DQ) + "</DQ>");
				stringBuilder.Append("<InverseQ>" + Convert.ToBase64String(rsaparameters.InverseQ) + "</InverseQ>");
				stringBuilder.Append("<D>" + Convert.ToBase64String(rsaparameters.D) + "</D>");
			}
			stringBuilder.Append("</RSAKeyValue>");
			return stringBuilder.ToString();
		}

		/// <summary>When overridden in a derived class, exports the <see cref="T:System.Security.Cryptography.RSAParameters" />.</summary>
		/// <returns>The parameters for <see cref="T:System.Security.Cryptography.DSA" />.</returns>
		/// <param name="includePrivateParameters">true to include private parameters; otherwise, false. </param>
		// Token: 0x0600473D RID: 18237
		public abstract RSAParameters ExportParameters(bool includePrivateParameters);

		/// <summary>When overridden in a derived class, imports the specified <see cref="T:System.Security.Cryptography.RSAParameters" />.</summary>
		/// <param name="parameters">The parameters for <see cref="T:System.Security.Cryptography.RSA" />. </param>
		// Token: 0x0600473E RID: 18238
		public abstract void ImportParameters(RSAParameters parameters);
	}
}
