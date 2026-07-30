using System;
using System.Text;

namespace Mono.Security.X509
{
	// Token: 0x02000067 RID: 103
	internal class X520
	{
		// Token: 0x02000068 RID: 104
		public abstract class AttributeTypeAndValue
		{
			// Token: 0x06000389 RID: 905 RVA: 0x00015439 File Offset: 0x00013639
			protected AttributeTypeAndValue(string oid, int upperBound)
			{
				this.oid = oid;
				this.upperBound = upperBound;
				this.encoding = byte.MaxValue;
			}

			// Token: 0x0600038A RID: 906 RVA: 0x0001545A File Offset: 0x0001365A
			protected AttributeTypeAndValue(string oid, int upperBound, byte encoding)
			{
				this.oid = oid;
				this.upperBound = upperBound;
				this.encoding = encoding;
			}

			// Token: 0x170000BD RID: 189
			// (get) Token: 0x0600038B RID: 907 RVA: 0x00015477 File Offset: 0x00013677
			// (set) Token: 0x0600038C RID: 908 RVA: 0x00015480 File Offset: 0x00013680
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

			// Token: 0x170000BE RID: 190
			// (get) Token: 0x0600038D RID: 909 RVA: 0x000154CF File Offset: 0x000136CF
			public ASN1 ASN1
			{
				get
				{
					return this.GetASN1();
				}
			}

			// Token: 0x0600038E RID: 910 RVA: 0x000154D8 File Offset: 0x000136D8
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

			// Token: 0x0600038F RID: 911 RVA: 0x00015580 File Offset: 0x00013780
			internal ASN1 GetASN1()
			{
				return this.GetASN1(this.encoding);
			}

			// Token: 0x06000390 RID: 912 RVA: 0x0001558E File Offset: 0x0001378E
			public byte[] GetBytes(byte encoding)
			{
				return this.GetASN1(encoding).GetBytes();
			}

			// Token: 0x06000391 RID: 913 RVA: 0x0001559C File Offset: 0x0001379C
			public byte[] GetBytes()
			{
				return this.GetASN1().GetBytes();
			}

			// Token: 0x06000392 RID: 914 RVA: 0x000155AC File Offset: 0x000137AC
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

			// Token: 0x04000537 RID: 1335
			private string oid;

			// Token: 0x04000538 RID: 1336
			private string attrValue;

			// Token: 0x04000539 RID: 1337
			private int upperBound;

			// Token: 0x0400053A RID: 1338
			private byte encoding;
		}

		// Token: 0x02000069 RID: 105
		public class Name : X520.AttributeTypeAndValue
		{
			// Token: 0x06000393 RID: 915 RVA: 0x000155F0 File Offset: 0x000137F0
			public Name()
				: base("2.5.4.41", 32768)
			{
			}
		}

		// Token: 0x0200006A RID: 106
		public class CommonName : X520.AttributeTypeAndValue
		{
			// Token: 0x06000394 RID: 916 RVA: 0x00015602 File Offset: 0x00013802
			public CommonName()
				: base("2.5.4.3", 64)
			{
			}
		}

		// Token: 0x0200006B RID: 107
		public class SerialNumber : X520.AttributeTypeAndValue
		{
			// Token: 0x06000395 RID: 917 RVA: 0x00015611 File Offset: 0x00013811
			public SerialNumber()
				: base("2.5.4.5", 64, 19)
			{
			}
		}

		// Token: 0x0200006C RID: 108
		public class LocalityName : X520.AttributeTypeAndValue
		{
			// Token: 0x06000396 RID: 918 RVA: 0x00015622 File Offset: 0x00013822
			public LocalityName()
				: base("2.5.4.7", 128)
			{
			}
		}

		// Token: 0x0200006D RID: 109
		public class StateOrProvinceName : X520.AttributeTypeAndValue
		{
			// Token: 0x06000397 RID: 919 RVA: 0x00015634 File Offset: 0x00013834
			public StateOrProvinceName()
				: base("2.5.4.8", 128)
			{
			}
		}

		// Token: 0x0200006E RID: 110
		public class OrganizationName : X520.AttributeTypeAndValue
		{
			// Token: 0x06000398 RID: 920 RVA: 0x00015646 File Offset: 0x00013846
			public OrganizationName()
				: base("2.5.4.10", 64)
			{
			}
		}

		// Token: 0x0200006F RID: 111
		public class OrganizationalUnitName : X520.AttributeTypeAndValue
		{
			// Token: 0x06000399 RID: 921 RVA: 0x00015655 File Offset: 0x00013855
			public OrganizationalUnitName()
				: base("2.5.4.11", 64)
			{
			}
		}

		// Token: 0x02000070 RID: 112
		public class EmailAddress : X520.AttributeTypeAndValue
		{
			// Token: 0x0600039A RID: 922 RVA: 0x00015664 File Offset: 0x00013864
			public EmailAddress()
				: base("1.2.840.113549.1.9.1", 128, 22)
			{
			}
		}

		// Token: 0x02000071 RID: 113
		public class DomainComponent : X520.AttributeTypeAndValue
		{
			// Token: 0x0600039B RID: 923 RVA: 0x00015678 File Offset: 0x00013878
			public DomainComponent()
				: base("0.9.2342.19200300.100.1.25", int.MaxValue, 22)
			{
			}
		}

		// Token: 0x02000072 RID: 114
		public class UserId : X520.AttributeTypeAndValue
		{
			// Token: 0x0600039C RID: 924 RVA: 0x0001568C File Offset: 0x0001388C
			public UserId()
				: base("0.9.2342.19200300.100.1.1", 256)
			{
			}
		}

		// Token: 0x02000073 RID: 115
		public class Oid : X520.AttributeTypeAndValue
		{
			// Token: 0x0600039D RID: 925 RVA: 0x0001569E File Offset: 0x0001389E
			public Oid(string oid)
				: base(oid, int.MaxValue)
			{
			}
		}

		// Token: 0x02000074 RID: 116
		public class Title : X520.AttributeTypeAndValue
		{
			// Token: 0x0600039E RID: 926 RVA: 0x000156AC File Offset: 0x000138AC
			public Title()
				: base("2.5.4.12", 64)
			{
			}
		}

		// Token: 0x02000075 RID: 117
		public class CountryName : X520.AttributeTypeAndValue
		{
			// Token: 0x0600039F RID: 927 RVA: 0x000156BB File Offset: 0x000138BB
			public CountryName()
				: base("2.5.4.6", 2, 19)
			{
			}
		}

		// Token: 0x02000076 RID: 118
		public class DnQualifier : X520.AttributeTypeAndValue
		{
			// Token: 0x060003A0 RID: 928 RVA: 0x000156CB File Offset: 0x000138CB
			public DnQualifier()
				: base("2.5.4.46", 2, 19)
			{
			}
		}

		// Token: 0x02000077 RID: 119
		public class Surname : X520.AttributeTypeAndValue
		{
			// Token: 0x060003A1 RID: 929 RVA: 0x000156DB File Offset: 0x000138DB
			public Surname()
				: base("2.5.4.4", 32768)
			{
			}
		}

		// Token: 0x02000078 RID: 120
		public class GivenName : X520.AttributeTypeAndValue
		{
			// Token: 0x060003A2 RID: 930 RVA: 0x000156ED File Offset: 0x000138ED
			public GivenName()
				: base("2.5.4.42", 16)
			{
			}
		}

		// Token: 0x02000079 RID: 121
		public class Initial : X520.AttributeTypeAndValue
		{
			// Token: 0x060003A3 RID: 931 RVA: 0x000156FC File Offset: 0x000138FC
			public Initial()
				: base("2.5.4.43", 5)
			{
			}
		}
	}
}
