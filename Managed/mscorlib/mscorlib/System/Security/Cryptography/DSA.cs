using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Util;
using System.Text;

namespace System.Security.Cryptography
{
	/// <summary>Represents the abstract base class from which all implementations of the Digital Signature Algorithm (<see cref="T:System.Security.Cryptography.DSA" />) must inherit.</summary>
	// Token: 0x0200065B RID: 1627
	[ComVisible(true)]
	public abstract class DSA : AsymmetricAlgorithm
	{
		/// <summary>Creates the default cryptographic object used to perform the asymmetric algorithm.</summary>
		/// <returns>A cryptographic object used to perform the asymmetric algorithm.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06004624 RID: 17956 RVA: 0x000F7133 File Offset: 0x000F5333
		public new static DSA Create()
		{
			return DSA.Create("System.Security.Cryptography.DSA");
		}

		/// <summary>Creates the specified cryptographic object used to perform the asymmetric algorithm.</summary>
		/// <returns>A cryptographic object used to perform the asymmetric algorithm.</returns>
		/// <param name="algName">The name of the specific implementation of <see cref="T:System.Security.Cryptography.DSA" /> to use. </param>
		// Token: 0x06004625 RID: 17957 RVA: 0x000F713F File Offset: 0x000F533F
		public new static DSA Create(string algName)
		{
			return (DSA)CryptoConfig.CreateFromName(algName);
		}

		/// <summary>When overridden in a derived class, creates the <see cref="T:System.Security.Cryptography.DSA" /> signature for the specified data.</summary>
		/// <returns>The digital signature for the specified data.</returns>
		/// <param name="rgbHash">The data to be signed. </param>
		// Token: 0x06004626 RID: 17958
		public abstract byte[] CreateSignature(byte[] rgbHash);

		/// <summary>When overridden in a derived class, verifies the <see cref="T:System.Security.Cryptography.DSA" /> signature for the specified data.</summary>
		/// <returns>true if <paramref name="rgbSignature" /> matches the signature computed using the specified hash algorithm and key on <paramref name="rgbHash" />; otherwise, false.</returns>
		/// <param name="rgbHash">The hash of the data signed with <paramref name="rgbSignature" />. </param>
		/// <param name="rgbSignature">The signature to be verified for <paramref name="rgbData" />. </param>
		// Token: 0x06004627 RID: 17959
		public abstract bool VerifySignature(byte[] rgbHash, byte[] rgbSignature);

		// Token: 0x06004628 RID: 17960 RVA: 0x000F714C File Offset: 0x000F534C
		protected virtual byte[] HashData(byte[] data, int offset, int count, HashAlgorithmName hashAlgorithm)
		{
			throw DSA.DerivedClassMustOverride();
		}

		// Token: 0x06004629 RID: 17961 RVA: 0x000F714C File Offset: 0x000F534C
		protected virtual byte[] HashData(Stream data, HashAlgorithmName hashAlgorithm)
		{
			throw DSA.DerivedClassMustOverride();
		}

		// Token: 0x0600462A RID: 17962 RVA: 0x000F7153 File Offset: 0x000F5353
		public byte[] SignData(byte[] data, HashAlgorithmName hashAlgorithm)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			return this.SignData(data, 0, data.Length, hashAlgorithm);
		}

		// Token: 0x0600462B RID: 17963 RVA: 0x000F7170 File Offset: 0x000F5370
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
				throw DSA.HashAlgorithmNameNullOrEmpty();
			}
			byte[] array = this.HashData(data, offset, count, hashAlgorithm);
			return this.CreateSignature(array);
		}

		// Token: 0x0600462C RID: 17964 RVA: 0x000F71E0 File Offset: 0x000F53E0
		public virtual byte[] SignData(Stream data, HashAlgorithmName hashAlgorithm)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			if (string.IsNullOrEmpty(hashAlgorithm.Name))
			{
				throw DSA.HashAlgorithmNameNullOrEmpty();
			}
			byte[] array = this.HashData(data, hashAlgorithm);
			return this.CreateSignature(array);
		}

		// Token: 0x0600462D RID: 17965 RVA: 0x000F721F File Offset: 0x000F541F
		public bool VerifyData(byte[] data, byte[] signature, HashAlgorithmName hashAlgorithm)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			return this.VerifyData(data, 0, data.Length, signature, hashAlgorithm);
		}

		// Token: 0x0600462E RID: 17966 RVA: 0x000F723C File Offset: 0x000F543C
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
				throw DSA.HashAlgorithmNameNullOrEmpty();
			}
			byte[] array = this.HashData(data, offset, count, hashAlgorithm);
			return this.VerifySignature(array, signature);
		}

		// Token: 0x0600462F RID: 17967 RVA: 0x000F72BC File Offset: 0x000F54BC
		public virtual bool VerifyData(Stream data, byte[] signature, HashAlgorithmName hashAlgorithm)
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
				throw DSA.HashAlgorithmNameNullOrEmpty();
			}
			byte[] array = this.HashData(data, hashAlgorithm);
			return this.VerifySignature(array, signature);
		}

		/// <summary>Reconstructs a <see cref="T:System.Security.Cryptography.DSA" /> object from an XML string.</summary>
		/// <param name="xmlString">The XML string to use to reconstruct the <see cref="T:System.Security.Cryptography.DSA" /> object. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="xmlString" /> parameter is null. </exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The format of the <paramref name="xmlString" /> parameter is not valid. </exception>
		// Token: 0x06004630 RID: 17968 RVA: 0x000F730C File Offset: 0x000F550C
		public override void FromXmlString(string xmlString)
		{
			if (xmlString == null)
			{
				throw new ArgumentNullException("xmlString");
			}
			DSAParameters dsaparameters = default(DSAParameters);
			SecurityElement topElement = new Parser(xmlString).GetTopElement();
			string text = topElement.SearchForTextOfLocalName("P");
			if (text == null)
			{
				throw new CryptographicException(Environment.GetResourceString("Input string does not contain a valid encoding of the '{0}' '{1}' parameter.", new object[] { "DSA", "P" }));
			}
			dsaparameters.P = Convert.FromBase64String(Utils.DiscardWhiteSpaces(text));
			string text2 = topElement.SearchForTextOfLocalName("Q");
			if (text2 == null)
			{
				throw new CryptographicException(Environment.GetResourceString("Input string does not contain a valid encoding of the '{0}' '{1}' parameter.", new object[] { "DSA", "Q" }));
			}
			dsaparameters.Q = Convert.FromBase64String(Utils.DiscardWhiteSpaces(text2));
			string text3 = topElement.SearchForTextOfLocalName("G");
			if (text3 == null)
			{
				throw new CryptographicException(Environment.GetResourceString("Input string does not contain a valid encoding of the '{0}' '{1}' parameter.", new object[] { "DSA", "G" }));
			}
			dsaparameters.G = Convert.FromBase64String(Utils.DiscardWhiteSpaces(text3));
			string text4 = topElement.SearchForTextOfLocalName("Y");
			if (text4 == null)
			{
				throw new CryptographicException(Environment.GetResourceString("Input string does not contain a valid encoding of the '{0}' '{1}' parameter.", new object[] { "DSA", "Y" }));
			}
			dsaparameters.Y = Convert.FromBase64String(Utils.DiscardWhiteSpaces(text4));
			string text5 = topElement.SearchForTextOfLocalName("J");
			if (text5 != null)
			{
				dsaparameters.J = Convert.FromBase64String(Utils.DiscardWhiteSpaces(text5));
			}
			string text6 = topElement.SearchForTextOfLocalName("X");
			if (text6 != null)
			{
				dsaparameters.X = Convert.FromBase64String(Utils.DiscardWhiteSpaces(text6));
			}
			string text7 = topElement.SearchForTextOfLocalName("Seed");
			string text8 = topElement.SearchForTextOfLocalName("PgenCounter");
			if (text7 != null && text8 != null)
			{
				dsaparameters.Seed = Convert.FromBase64String(Utils.DiscardWhiteSpaces(text7));
				dsaparameters.Counter = Utils.ConvertByteArrayToInt(Convert.FromBase64String(Utils.DiscardWhiteSpaces(text8)));
			}
			else if (text7 != null || text8 != null)
			{
				if (text7 == null)
				{
					throw new CryptographicException(Environment.GetResourceString("Input string does not contain a valid encoding of the '{0}' '{1}' parameter.", new object[] { "DSA", "Seed" }));
				}
				throw new CryptographicException(Environment.GetResourceString("Input string does not contain a valid encoding of the '{0}' '{1}' parameter.", new object[] { "DSA", "PgenCounter" }));
			}
			this.ImportParameters(dsaparameters);
		}

		/// <summary>Creates and returns an XML string representation of the current <see cref="T:System.Security.Cryptography.DSA" /> object.</summary>
		/// <returns>An XML string encoding of the current <see cref="T:System.Security.Cryptography.DSA" /> object.</returns>
		/// <param name="includePrivateParameters">true to include private parameters; otherwise, false. </param>
		// Token: 0x06004631 RID: 17969 RVA: 0x000F7554 File Offset: 0x000F5754
		public override string ToXmlString(bool includePrivateParameters)
		{
			DSAParameters dsaparameters = this.ExportParameters(includePrivateParameters);
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("<DSAKeyValue>");
			stringBuilder.Append("<P>" + Convert.ToBase64String(dsaparameters.P) + "</P>");
			stringBuilder.Append("<Q>" + Convert.ToBase64String(dsaparameters.Q) + "</Q>");
			stringBuilder.Append("<G>" + Convert.ToBase64String(dsaparameters.G) + "</G>");
			stringBuilder.Append("<Y>" + Convert.ToBase64String(dsaparameters.Y) + "</Y>");
			if (dsaparameters.J != null)
			{
				stringBuilder.Append("<J>" + Convert.ToBase64String(dsaparameters.J) + "</J>");
			}
			if (dsaparameters.Seed != null)
			{
				stringBuilder.Append("<Seed>" + Convert.ToBase64String(dsaparameters.Seed) + "</Seed>");
				stringBuilder.Append("<PgenCounter>" + Convert.ToBase64String(Utils.ConvertIntToByteArray(dsaparameters.Counter)) + "</PgenCounter>");
			}
			if (includePrivateParameters)
			{
				stringBuilder.Append("<X>" + Convert.ToBase64String(dsaparameters.X) + "</X>");
			}
			stringBuilder.Append("</DSAKeyValue>");
			return stringBuilder.ToString();
		}

		/// <summary>When overridden in a derived class, exports the <see cref="T:System.Security.Cryptography.DSAParameters" />.</summary>
		/// <returns>The parameters for <see cref="T:System.Security.Cryptography.DSA" />.</returns>
		/// <param name="includePrivateParameters">true to include private parameters; otherwise, false. </param>
		// Token: 0x06004632 RID: 17970
		public abstract DSAParameters ExportParameters(bool includePrivateParameters);

		/// <summary>When overridden in a derived class, imports the specified <see cref="T:System.Security.Cryptography.DSAParameters" />.</summary>
		/// <param name="parameters">The parameters for <see cref="T:System.Security.Cryptography.DSA" />. </param>
		// Token: 0x06004633 RID: 17971
		public abstract void ImportParameters(DSAParameters parameters);

		// Token: 0x06004634 RID: 17972 RVA: 0x000F76AD File Offset: 0x000F58AD
		private static Exception DerivedClassMustOverride()
		{
			return new NotImplementedException(Environment.GetResourceString("Derived classes must provide an implementation."));
		}

		// Token: 0x06004635 RID: 17973 RVA: 0x000F76BE File Offset: 0x000F58BE
		internal static Exception HashAlgorithmNameNullOrEmpty()
		{
			return new ArgumentException(Environment.GetResourceString("The hash algorithm name cannot be null or empty."), "hashAlgorithm");
		}
	}
}
