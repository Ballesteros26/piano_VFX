using System;
using System.Text;
using Mono.Security;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Defines the usage of a key contained within an X.509 certificate.  This class cannot be inherited.</summary>
	// Token: 0x020003BF RID: 959
	public sealed class X509KeyUsageExtension : X509Extension
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509KeyUsageExtension" /> class.</summary>
		// Token: 0x06001D69 RID: 7529 RVA: 0x0007447A File Offset: 0x0007267A
		public X509KeyUsageExtension()
		{
			this._oid = new Oid("2.5.29.15", "Key Usage");
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509KeyUsageExtension" /> class using an <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object and a value that identifies whether the extension is critical. </summary>
		/// <param name="encodedKeyUsage">The encoded data to use to create the extension.</param>
		/// <param name="critical">true if the extension is critical; otherwise, false.</param>
		// Token: 0x06001D6A RID: 7530 RVA: 0x00074498 File Offset: 0x00072698
		public X509KeyUsageExtension(AsnEncodedData encodedKeyUsage, bool critical)
		{
			this._oid = new Oid("2.5.29.15", "Key Usage");
			this._raw = encodedKeyUsage.RawData;
			base.Critical = critical;
			this._status = this.Decode(base.RawData);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509KeyUsageExtension" /> class using the specified <see cref="T:System.Security.Cryptography.X509Certificates.X509KeyUsageFlags" /> value and a value that identifies whether the extension is critical. </summary>
		/// <param name="keyUsages">One of the <see cref="T:System.Security.Cryptography.X509Certificates.X509KeyUsageFlags" /> values that describes how to use the key.</param>
		/// <param name="critical">true if the extension is critical; otherwise, false.</param>
		// Token: 0x06001D6B RID: 7531 RVA: 0x000744E5 File Offset: 0x000726E5
		public X509KeyUsageExtension(X509KeyUsageFlags keyUsages, bool critical)
		{
			this._oid = new Oid("2.5.29.15", "Key Usage");
			base.Critical = critical;
			this._keyUsages = this.GetValidFlags(keyUsages);
			base.RawData = this.Encode();
		}

		/// <summary>Gets the key usage flag associated with the certificate.</summary>
		/// <returns>One of the <see cref="P:System.Security.Cryptography.X509Certificates.X509KeyUsageExtension.KeyUsages" /> values.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The extension cannot be decoded. </exception>
		// Token: 0x17000615 RID: 1557
		// (get) Token: 0x06001D6C RID: 7532 RVA: 0x00074524 File Offset: 0x00072724
		public X509KeyUsageFlags KeyUsages
		{
			get
			{
				AsnDecodeStatus status = this._status;
				if (status == AsnDecodeStatus.Ok || status == AsnDecodeStatus.InformationNotAvailable)
				{
					return this._keyUsages;
				}
				throw new CryptographicException("Badly encoded extension.");
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509KeyUsageExtension" /> class using an <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object. </summary>
		/// <param name="asnEncodedData">The encoded data to use to create the extension.</param>
		// Token: 0x06001D6D RID: 7533 RVA: 0x00074550 File Offset: 0x00072750
		public override void CopyFrom(AsnEncodedData asnEncodedData)
		{
			if (asnEncodedData == null)
			{
				throw new ArgumentNullException("asnEncodedData");
			}
			X509Extension x509Extension = asnEncodedData as X509Extension;
			if (x509Extension == null)
			{
				throw new ArgumentException(global::Locale.GetText("Wrong type."), "asnEncodedData");
			}
			if (x509Extension._oid == null)
			{
				this._oid = new Oid("2.5.29.15", "Key Usage");
			}
			else
			{
				this._oid = new Oid(x509Extension._oid);
			}
			base.RawData = x509Extension.RawData;
			base.Critical = x509Extension.Critical;
			this._status = this.Decode(base.RawData);
		}

		// Token: 0x06001D6E RID: 7534 RVA: 0x000745E4 File Offset: 0x000727E4
		internal X509KeyUsageFlags GetValidFlags(X509KeyUsageFlags flags)
		{
			if ((flags & (X509KeyUsageFlags.EncipherOnly | X509KeyUsageFlags.CrlSign | X509KeyUsageFlags.KeyCertSign | X509KeyUsageFlags.KeyAgreement | X509KeyUsageFlags.DataEncipherment | X509KeyUsageFlags.KeyEncipherment | X509KeyUsageFlags.NonRepudiation | X509KeyUsageFlags.DigitalSignature | X509KeyUsageFlags.DecipherOnly)) != flags)
			{
				return X509KeyUsageFlags.None;
			}
			return flags;
		}

		// Token: 0x06001D6F RID: 7535 RVA: 0x000745F4 File Offset: 0x000727F4
		internal AsnDecodeStatus Decode(byte[] extension)
		{
			if (extension == null || extension.Length == 0)
			{
				return AsnDecodeStatus.BadAsn;
			}
			if (extension[0] != 3)
			{
				return AsnDecodeStatus.BadTag;
			}
			if (extension.Length < 3)
			{
				return AsnDecodeStatus.BadLength;
			}
			if (extension.Length < 4)
			{
				return AsnDecodeStatus.InformationNotAvailable;
			}
			try
			{
				Mono.Security.ASN1 asn = new Mono.Security.ASN1(extension);
				int num = 0;
				int i = 1;
				while (i < asn.Value.Length)
				{
					num = (num << 8) + (int)asn.Value[i++];
				}
				this._keyUsages = this.GetValidFlags((X509KeyUsageFlags)num);
			}
			catch
			{
				return AsnDecodeStatus.BadAsn;
			}
			return AsnDecodeStatus.Ok;
		}

		// Token: 0x06001D70 RID: 7536 RVA: 0x00074674 File Offset: 0x00072874
		internal byte[] Encode()
		{
			int keyUsages = (int)this._keyUsages;
			byte b = 0;
			Mono.Security.ASN1 asn;
			if (keyUsages == 0)
			{
				asn = new Mono.Security.ASN1(3, new byte[] { b });
			}
			else
			{
				int num = ((keyUsages < 255) ? keyUsages : (keyUsages >> 8));
				while ((num & 1) == 0 && b < 8)
				{
					b += 1;
					num >>= 1;
				}
				if (keyUsages <= 255)
				{
					asn = new Mono.Security.ASN1(3, new byte[]
					{
						b,
						(byte)keyUsages
					});
				}
				else
				{
					asn = new Mono.Security.ASN1(3, new byte[]
					{
						b,
						(byte)keyUsages,
						(byte)(keyUsages >> 8)
					});
				}
			}
			return asn.GetBytes();
		}

		// Token: 0x06001D71 RID: 7537 RVA: 0x00074708 File Offset: 0x00072908
		internal override string ToString(bool multiLine)
		{
			switch (this._status)
			{
			case AsnDecodeStatus.BadAsn:
				return string.Empty;
			case AsnDecodeStatus.BadTag:
			case AsnDecodeStatus.BadLength:
				return base.FormatUnkownData(this._raw);
			case AsnDecodeStatus.InformationNotAvailable:
				return "Information Not Available";
			default:
			{
				if (this._oid.Value != "2.5.29.15")
				{
					return string.Format("Unknown Key Usage ({0})", this._oid.Value);
				}
				if (this._keyUsages == X509KeyUsageFlags.None)
				{
					return "Information Not Available";
				}
				StringBuilder stringBuilder = new StringBuilder();
				if ((this._keyUsages & X509KeyUsageFlags.DigitalSignature) != X509KeyUsageFlags.None)
				{
					stringBuilder.Append("Digital Signature");
				}
				if ((this._keyUsages & X509KeyUsageFlags.NonRepudiation) != X509KeyUsageFlags.None)
				{
					if (stringBuilder.Length > 0)
					{
						stringBuilder.Append(", ");
					}
					stringBuilder.Append("Non-Repudiation");
				}
				if ((this._keyUsages & X509KeyUsageFlags.KeyEncipherment) != X509KeyUsageFlags.None)
				{
					if (stringBuilder.Length > 0)
					{
						stringBuilder.Append(", ");
					}
					stringBuilder.Append("Key Encipherment");
				}
				if ((this._keyUsages & X509KeyUsageFlags.DataEncipherment) != X509KeyUsageFlags.None)
				{
					if (stringBuilder.Length > 0)
					{
						stringBuilder.Append(", ");
					}
					stringBuilder.Append("Data Encipherment");
				}
				if ((this._keyUsages & X509KeyUsageFlags.KeyAgreement) != X509KeyUsageFlags.None)
				{
					if (stringBuilder.Length > 0)
					{
						stringBuilder.Append(", ");
					}
					stringBuilder.Append("Key Agreement");
				}
				if ((this._keyUsages & X509KeyUsageFlags.KeyCertSign) != X509KeyUsageFlags.None)
				{
					if (stringBuilder.Length > 0)
					{
						stringBuilder.Append(", ");
					}
					stringBuilder.Append("Certificate Signing");
				}
				if ((this._keyUsages & X509KeyUsageFlags.CrlSign) != X509KeyUsageFlags.None)
				{
					if (stringBuilder.Length > 0)
					{
						stringBuilder.Append(", ");
					}
					stringBuilder.Append("Off-line CRL Signing, CRL Signing");
				}
				if ((this._keyUsages & X509KeyUsageFlags.EncipherOnly) != X509KeyUsageFlags.None)
				{
					if (stringBuilder.Length > 0)
					{
						stringBuilder.Append(", ");
					}
					stringBuilder.Append("Encipher Only");
				}
				if ((this._keyUsages & X509KeyUsageFlags.DecipherOnly) != X509KeyUsageFlags.None)
				{
					if (stringBuilder.Length > 0)
					{
						stringBuilder.Append(", ");
					}
					stringBuilder.Append("Decipher Only");
				}
				int keyUsages = (int)this._keyUsages;
				stringBuilder.Append(" (");
				stringBuilder.Append(((byte)keyUsages).ToString("x2"));
				if (keyUsages > 255)
				{
					stringBuilder.Append(" ");
					stringBuilder.Append(((byte)(keyUsages >> 8)).ToString("x2"));
				}
				stringBuilder.Append(")");
				if (multiLine)
				{
					stringBuilder.Append(Environment.NewLine);
				}
				return stringBuilder.ToString();
			}
			}
		}

		// Token: 0x040019CC RID: 6604
		internal const string oid = "2.5.29.15";

		// Token: 0x040019CD RID: 6605
		internal const string friendlyName = "Key Usage";

		// Token: 0x040019CE RID: 6606
		internal const X509KeyUsageFlags all = X509KeyUsageFlags.EncipherOnly | X509KeyUsageFlags.CrlSign | X509KeyUsageFlags.KeyCertSign | X509KeyUsageFlags.KeyAgreement | X509KeyUsageFlags.DataEncipherment | X509KeyUsageFlags.KeyEncipherment | X509KeyUsageFlags.NonRepudiation | X509KeyUsageFlags.DigitalSignature | X509KeyUsageFlags.DecipherOnly;

		// Token: 0x040019CF RID: 6607
		private X509KeyUsageFlags _keyUsages;

		// Token: 0x040019D0 RID: 6608
		private AsnDecodeStatus _status;
	}
}
