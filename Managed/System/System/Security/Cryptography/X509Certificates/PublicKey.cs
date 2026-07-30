using System;
using Mono.Security;
using Mono.Security.Cryptography;
using Mono.Security.X509;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Represents a certificate's public key information. This class cannot be inherited.</summary>
	// Token: 0x020003A6 RID: 934
	public sealed class PublicKey
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.PublicKey" /> class using an object identifier (OID) object of the public key, an ASN.1-encoded representation of the public key parameters, and an ASN.1-encoded representation of the public key value. </summary>
		/// <param name="oid">An object identifier (OID) object that represents the public key.</param>
		/// <param name="parameters">An ASN.1-encoded representation of the public key parameters.</param>
		/// <param name="keyValue">An ASN.1-encoded representation of the public key value.</param>
		// Token: 0x06001BE2 RID: 7138 RVA: 0x0006F114 File Offset: 0x0006D314
		public PublicKey(Oid oid, AsnEncodedData parameters, AsnEncodedData keyValue)
		{
			if (oid == null)
			{
				throw new ArgumentNullException("oid");
			}
			if (parameters == null)
			{
				throw new ArgumentNullException("parameters");
			}
			if (keyValue == null)
			{
				throw new ArgumentNullException("keyValue");
			}
			this._oid = new Oid(oid);
			this._params = new AsnEncodedData(parameters);
			this._keyValue = new AsnEncodedData(keyValue);
		}

		// Token: 0x06001BE3 RID: 7139 RVA: 0x0006F178 File Offset: 0x0006D378
		internal PublicKey(Mono.Security.X509.X509Certificate certificate)
		{
			bool flag = true;
			if (certificate.KeyAlgorithm == "1.2.840.113549.1.1.1")
			{
				RSACryptoServiceProvider rsacryptoServiceProvider = certificate.RSA as RSACryptoServiceProvider;
				if (rsacryptoServiceProvider != null && rsacryptoServiceProvider.PublicOnly)
				{
					this._key = certificate.RSA;
					flag = false;
				}
				else
				{
					Mono.Security.Cryptography.RSAManaged rsamanaged = certificate.RSA as Mono.Security.Cryptography.RSAManaged;
					if (rsamanaged != null && rsamanaged.PublicOnly)
					{
						this._key = certificate.RSA;
						flag = false;
					}
				}
				if (flag)
				{
					RSAParameters rsaparameters = certificate.RSA.ExportParameters(false);
					this._key = RSA.Create();
					(this._key as RSA).ImportParameters(rsaparameters);
				}
			}
			else
			{
				DSACryptoServiceProvider dsacryptoServiceProvider = certificate.DSA as DSACryptoServiceProvider;
				if (dsacryptoServiceProvider != null && dsacryptoServiceProvider.PublicOnly)
				{
					this._key = certificate.DSA;
					flag = false;
				}
				if (flag)
				{
					DSAParameters dsaparameters = certificate.DSA.ExportParameters(false);
					this._key = DSA.Create();
					(this._key as DSA).ImportParameters(dsaparameters);
				}
			}
			this._oid = new Oid(certificate.KeyAlgorithm);
			this._keyValue = new AsnEncodedData(this._oid, certificate.PublicKey);
			this._params = new AsnEncodedData(this._oid, certificate.KeyAlgorithmParameters ?? PublicKey.Empty);
		}

		/// <summary>Gets the ASN.1-encoded representation of the public key value.</summary>
		/// <returns>The ASN.1-encoded representation of the public key value.</returns>
		// Token: 0x170005A5 RID: 1445
		// (get) Token: 0x06001BE4 RID: 7140 RVA: 0x0006F2BA File Offset: 0x0006D4BA
		public AsnEncodedData EncodedKeyValue
		{
			get
			{
				return this._keyValue;
			}
		}

		/// <summary>Gets the ASN.1-encoded representation of the public key parameters.</summary>
		/// <returns>The ASN.1-encoded representation of the public key parameters.</returns>
		// Token: 0x170005A6 RID: 1446
		// (get) Token: 0x06001BE5 RID: 7141 RVA: 0x0006F2C2 File Offset: 0x0006D4C2
		public AsnEncodedData EncodedParameters
		{
			get
			{
				return this._params;
			}
		}

		/// <summary>Gets an <see cref="T:System.Security.Cryptography.RSACryptoServiceProvider" /> or <see cref="T:System.Security.Cryptography.DSACryptoServiceProvider" /> object representing the public key.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.AsymmetricAlgorithm" /> object representing the public key.</returns>
		/// <exception cref="T:System.NotSupportedException">The key algorithm is not supported.</exception>
		// Token: 0x170005A7 RID: 1447
		// (get) Token: 0x06001BE6 RID: 7142 RVA: 0x0006F2CC File Offset: 0x0006D4CC
		public AsymmetricAlgorithm Key
		{
			get
			{
				if (this._key == null)
				{
					string value = this._oid.Value;
					if (!(value == "1.2.840.113549.1.1.1"))
					{
						if (!(value == "1.2.840.10040.4.1"))
						{
							throw new NotSupportedException(global::Locale.GetText("Cannot decode public key from unknown OID '{0}'.", new object[] { this._oid.Value }));
						}
						this._key = PublicKey.DecodeDSA(this._keyValue.RawData, this._params.RawData);
					}
					else
					{
						this._key = PublicKey.DecodeRSA(this._keyValue.RawData);
					}
				}
				return this._key;
			}
		}

		/// <summary>Gets an object identifier (OID) object of the public key.</summary>
		/// <returns>An object identifier (OID) object of the public key.</returns>
		// Token: 0x170005A8 RID: 1448
		// (get) Token: 0x06001BE7 RID: 7143 RVA: 0x0006F371 File Offset: 0x0006D571
		public Oid Oid
		{
			get
			{
				return this._oid;
			}
		}

		// Token: 0x06001BE8 RID: 7144 RVA: 0x0006F37C File Offset: 0x0006D57C
		private static byte[] GetUnsignedBigInteger(byte[] integer)
		{
			if (integer[0] != 0)
			{
				return integer;
			}
			int num = integer.Length - 1;
			byte[] array = new byte[num];
			Buffer.BlockCopy(integer, 1, array, 0, num);
			return array;
		}

		// Token: 0x06001BE9 RID: 7145 RVA: 0x0006F3A8 File Offset: 0x0006D5A8
		internal static DSA DecodeDSA(byte[] rawPublicKey, byte[] rawParameters)
		{
			DSAParameters dsaparameters = default(DSAParameters);
			try
			{
				Mono.Security.ASN1 asn = new Mono.Security.ASN1(rawPublicKey);
				if (asn.Tag != 2)
				{
					throw new CryptographicException(global::Locale.GetText("Missing DSA Y integer."));
				}
				dsaparameters.Y = PublicKey.GetUnsignedBigInteger(asn.Value);
				Mono.Security.ASN1 asn2 = new Mono.Security.ASN1(rawParameters);
				if (asn2 == null || asn2.Tag != 48 || asn2.Count < 3)
				{
					throw new CryptographicException(global::Locale.GetText("Missing DSA parameters."));
				}
				if (asn2[0].Tag != 2 || asn2[1].Tag != 2 || asn2[2].Tag != 2)
				{
					throw new CryptographicException(global::Locale.GetText("Invalid DSA parameters."));
				}
				dsaparameters.P = PublicKey.GetUnsignedBigInteger(asn2[0].Value);
				dsaparameters.Q = PublicKey.GetUnsignedBigInteger(asn2[1].Value);
				dsaparameters.G = PublicKey.GetUnsignedBigInteger(asn2[2].Value);
			}
			catch (Exception ex)
			{
				throw new CryptographicException(global::Locale.GetText("Error decoding the ASN.1 structure."), ex);
			}
			DSACryptoServiceProvider dsacryptoServiceProvider = new DSACryptoServiceProvider(dsaparameters.Y.Length << 3);
			dsacryptoServiceProvider.ImportParameters(dsaparameters);
			return dsacryptoServiceProvider;
		}

		// Token: 0x06001BEA RID: 7146 RVA: 0x0006F4DC File Offset: 0x0006D6DC
		internal static RSA DecodeRSA(byte[] rawPublicKey)
		{
			RSAParameters rsaparameters = default(RSAParameters);
			try
			{
				Mono.Security.ASN1 asn = new Mono.Security.ASN1(rawPublicKey);
				if (asn.Count == 0)
				{
					throw new CryptographicException(global::Locale.GetText("Missing RSA modulus and exponent."));
				}
				Mono.Security.ASN1 asn2 = asn[0];
				if (asn2 == null || asn2.Tag != 2)
				{
					throw new CryptographicException(global::Locale.GetText("Missing RSA modulus."));
				}
				Mono.Security.ASN1 asn3 = asn[1];
				if (asn3.Tag != 2)
				{
					throw new CryptographicException(global::Locale.GetText("Missing RSA public exponent."));
				}
				rsaparameters.Modulus = PublicKey.GetUnsignedBigInteger(asn2.Value);
				rsaparameters.Exponent = asn3.Value;
			}
			catch (Exception ex)
			{
				throw new CryptographicException(global::Locale.GetText("Error decoding the ASN.1 structure."), ex);
			}
			RSACryptoServiceProvider rsacryptoServiceProvider = new RSACryptoServiceProvider(rsaparameters.Modulus.Length << 3);
			rsacryptoServiceProvider.ImportParameters(rsaparameters);
			return rsacryptoServiceProvider;
		}

		// Token: 0x04001980 RID: 6528
		private const string rsaOid = "1.2.840.113549.1.1.1";

		// Token: 0x04001981 RID: 6529
		private const string dsaOid = "1.2.840.10040.4.1";

		// Token: 0x04001982 RID: 6530
		private AsymmetricAlgorithm _key;

		// Token: 0x04001983 RID: 6531
		private AsnEncodedData _keyValue;

		// Token: 0x04001984 RID: 6532
		private AsnEncodedData _params;

		// Token: 0x04001985 RID: 6533
		private Oid _oid;

		// Token: 0x04001986 RID: 6534
		private static byte[] Empty = new byte[0];
	}
}
