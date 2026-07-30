using System;
using System.Text;

namespace Mono.Security.X509
{
	// Token: 0x0200001D RID: 29
	public class X520
	{
		// Token: 0x020000C8 RID: 200
		public abstract class AttributeTypeAndValue
		{
			// Token: 0x06000757 RID: 1879 RVA: 0x000218A8 File Offset: 0x0001FAA8
			protected AttributeTypeAndValue(string oid, int upperBound)
			{
				this.oid = oid;
				this.upperBound = upperBound;
				this.encoding = byte.MaxValue;
			}

			// Token: 0x06000758 RID: 1880 RVA: 0x000218C9 File Offset: 0x0001FAC9
			protected AttributeTypeAndValue(string oid, int upperBound, byte encoding)
			{
				this.oid = oid;
				this.upperBound = upperBound;
				this.encoding = encoding;
			}

			// Token: 0x170001D3 RID: 467
			// (get) Token: 0x06000759 RID: 1881 RVA: 0x000218E6 File Offset: 0x0001FAE6
			// (set) Token: 0x0600075A RID: 1882 RVA: 0x000218F0 File Offset: 0x0001FAF0
			public string Value
			{
				get
				{
					return this.attrValue;
				}
				set
				{
					if (this.attrValue != null && this.attrValue.Length > this.upperBound)
					{
						throw new FormatException(string.Format(Locale.GetText("Value length bigger than upperbound ({0})."), this.upperBound));
					}
					this.attrValue = value;
				}
			}

			// Token: 0x170001D4 RID: 468
			// (get) Token: 0x0600075B RID: 1883 RVA: 0x0002193F File Offset: 0x0001FB3F
			public ASN1 ASN1
			{
				get
				{
					return this.GetASN1();
				}
			}

			// Token: 0x0600075C RID: 1884 RVA: 0x00021948 File Offset: 0x0001FB48
			internal ASN1 GetASN1(byte encoding)
			{
				byte b = encoding;
				if (b == 255)
				{
					b = this.SelectBestEncoding();
				}
				ASN1 asn = new ASN1(48);
				asn.Add(ASN1Convert.FromOid(this.oid));
				if (b != 19)
				{
					if (b != 22)
					{
						if (b == 30)
						{
							asn.Add(new ASN1(30, Encoding.BigEndianUnicode.GetBytes(this.attrValue)));
						}
					}
					else
					{
						asn.Add(new ASN1(22, Encoding.ASCII.GetBytes(this.attrValue)));
					}
				}
				else
				{
					asn.Add(new ASN1(19, Encoding.ASCII.GetBytes(this.attrValue)));
				}
				return asn;
			}

			// Token: 0x0600075D RID: 1885 RVA: 0x000219F0 File Offset: 0x0001FBF0
			internal ASN1 GetASN1()
			{
				return this.GetASN1(this.encoding);
			}

			// Token: 0x0600075E RID: 1886 RVA: 0x000219FE File Offset: 0x0001FBFE
			public byte[] GetBytes(byte encoding)
			{
				return this.GetASN1(encoding).GetBytes();
			}

			// Token: 0x0600075F RID: 1887 RVA: 0x00021A0C File Offset: 0x0001FC0C
			public byte[] GetBytes()
			{
				return this.GetASN1().GetBytes();
			}

			// Token: 0x06000760 RID: 1888 RVA: 0x00021A1C File Offset: 0x0001FC1C
			private byte SelectBestEncoding()
			{
				foreach (char c in this.attrValue)
				{
					if (c == '@' || c == '_')
					{
						return 30;
					}
					if (c > '\u007f')
					{
						return 30;
					}
				}
				return 19;
			}

			// Token: 0x040004CE RID: 1230
			private string oid;

			// Token: 0x040004CF RID: 1231
			private string attrValue;

			// Token: 0x040004D0 RID: 1232
			private int upperBound;

			// Token: 0x040004D1 RID: 1233
			private byte encoding;
		}

		// Token: 0x020000C9 RID: 201
		public class Name : X520.AttributeTypeAndValue
		{
			// Token: 0x06000761 RID: 1889 RVA: 0x00021A60 File Offset: 0x0001FC60
			public Name()
				: base("2.5.4.41", 32768)
			{
			}
		}

		// Token: 0x020000CA RID: 202
		public class CommonName : X520.AttributeTypeAndValue
		{
			// Token: 0x06000762 RID: 1890 RVA: 0x00021A72 File Offset: 0x0001FC72
			public CommonName()
				: base("2.5.4.3", 64)
			{
			}
		}

		// Token: 0x020000CB RID: 203
		public class SerialNumber : X520.AttributeTypeAndValue
		{
			// Token: 0x06000763 RID: 1891 RVA: 0x00021A81 File Offset: 0x0001FC81
			public SerialNumber()
				: base("2.5.4.5", 64, 19)
			{
			}
		}

		// Token: 0x020000CC RID: 204
		public class LocalityName : X520.AttributeTypeAndValue
		{
			// Token: 0x06000764 RID: 1892 RVA: 0x00021A92 File Offset: 0x0001FC92
			public LocalityName()
				: base("2.5.4.7", 128)
			{
			}
		}

		// Token: 0x020000CD RID: 205
		public class StateOrProvinceName : X520.AttributeTypeAndValue
		{
			// Token: 0x06000765 RID: 1893 RVA: 0x00021AA4 File Offset: 0x0001FCA4
			public StateOrProvinceName()
				: base("2.5.4.8", 128)
			{
			}
		}

		// Token: 0x020000CE RID: 206
		public class OrganizationName : X520.AttributeTypeAndValue
		{
			// Token: 0x06000766 RID: 1894 RVA: 0x00021AB6 File Offset: 0x0001FCB6
			public OrganizationName()
				: base("2.5.4.10", 64)
			{
			}
		}

		// Token: 0x020000CF RID: 207
		public class OrganizationalUnitName : X520.AttributeTypeAndValue
		{
			// Token: 0x06000767 RID: 1895 RVA: 0x00021AC5 File Offset: 0x0001FCC5
			public OrganizationalUnitName()
				: base("2.5.4.11", 64)
			{
			}
		}

		// Token: 0x020000D0 RID: 208
		public class EmailAddress : X520.AttributeTypeAndValue
		{
			// Token: 0x06000768 RID: 1896 RVA: 0x00021AD4 File Offset: 0x0001FCD4
			public EmailAddress()
				: base("1.2.840.113549.1.9.1", 128, 22)
			{
			}
		}

		// Token: 0x020000D1 RID: 209
		public class DomainComponent : X520.AttributeTypeAndValue
		{
			// Token: 0x06000769 RID: 1897 RVA: 0x00021AE8 File Offset: 0x0001FCE8
			public DomainComponent()
				: base("0.9.2342.19200300.100.1.25", int.MaxValue, 22)
			{
			}
		}

		// Token: 0x020000D2 RID: 210
		public class UserId : X520.AttributeTypeAndValue
		{
			// Token: 0x0600076A RID: 1898 RVA: 0x00021AFC File Offset: 0x0001FCFC
			public UserId()
				: base("0.9.2342.19200300.100.1.1", 256)
			{
			}
		}

		// Token: 0x020000D3 RID: 211
		public class Oid : X520.AttributeTypeAndValue
		{
			// Token: 0x0600076B RID: 1899 RVA: 0x00021B0E File Offset: 0x0001FD0E
			public Oid(string oid)
				: base(oid, int.MaxValue)
			{
			}
		}

		// Token: 0x020000D4 RID: 212
		public class Title : X520.AttributeTypeAndValue
		{
			// Token: 0x0600076C RID: 1900 RVA: 0x00021B1C File Offset: 0x0001FD1C
			public Title()
				: base("2.5.4.12", 64)
			{
			}
		}

		// Token: 0x020000D5 RID: 213
		public class CountryName : X520.AttributeTypeAndValue
		{
			// Token: 0x0600076D RID: 1901 RVA: 0x00021B2B File Offset: 0x0001FD2B
			public CountryName()
				: base("2.5.4.6", 2, 19)
			{
			}
		}

		// Token: 0x020000D6 RID: 214
		public class DnQualifier : X520.AttributeTypeAndValue
		{
			// Token: 0x0600076E RID: 1902 RVA: 0x00021B3B File Offset: 0x0001FD3B
			public DnQualifier()
				: base("2.5.4.46", 2, 19)
			{
			}
		}

		// Token: 0x020000D7 RID: 215
		public class Surname : X520.AttributeTypeAndValue
		{
			// Token: 0x0600076F RID: 1903 RVA: 0x00021B4B File Offset: 0x0001FD4B
			public Surname()
				: base("2.5.4.4", 32768)
			{
			}
		}

		// Token: 0x020000D8 RID: 216
		public class GivenName : X520.AttributeTypeAndValue
		{
			// Token: 0x06000770 RID: 1904 RVA: 0x00021B5D File Offset: 0x0001FD5D
			public GivenName()
				: base("2.5.4.42", 16)
			{
			}
		}

		// Token: 0x020000D9 RID: 217
		public class Initial : X520.AttributeTypeAndValue
		{
			// Token: 0x06000771 RID: 1905 RVA: 0x00021B6C File Offset: 0x0001FD6C
			public Initial()
				: base("2.5.4.43", 5)
			{
			}
		}
	}
}
