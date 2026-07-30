using System;
using System.IO;

namespace Novell.Directory.Ldap.Asn1
{
	// Token: 0x020000D3 RID: 211
	public class Asn1Identifier : ICloneable
	{
		// Token: 0x17000171 RID: 369
		// (get) Token: 0x0600052D RID: 1325 RVA: 0x00016FFE File Offset: 0x000151FE
		public virtual int Asn1Class
		{
			get
			{
				return this.tagClass;
			}
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x0600052E RID: 1326 RVA: 0x00017006 File Offset: 0x00015206
		public virtual bool Constructed
		{
			get
			{
				return this.constructed;
			}
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x0600052F RID: 1327 RVA: 0x0001700E File Offset: 0x0001520E
		public virtual int Tag
		{
			get
			{
				return this.tag;
			}
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x06000530 RID: 1328 RVA: 0x00017016 File Offset: 0x00015216
		public virtual int EncodedLength
		{
			get
			{
				return this.encodedLength;
			}
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06000531 RID: 1329 RVA: 0x0001701E File Offset: 0x0001521E
		[CLSCompliant(false)]
		public virtual bool Universal
		{
			get
			{
				return this.tagClass == 0;
			}
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06000532 RID: 1330 RVA: 0x00017029 File Offset: 0x00015229
		[CLSCompliant(false)]
		public virtual bool Application
		{
			get
			{
				return this.tagClass == 1;
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x06000533 RID: 1331 RVA: 0x00017034 File Offset: 0x00015234
		[CLSCompliant(false)]
		public virtual bool Context
		{
			get
			{
				return this.tagClass == 2;
			}
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06000534 RID: 1332 RVA: 0x0001703F File Offset: 0x0001523F
		[CLSCompliant(false)]
		public virtual bool Private
		{
			get
			{
				return this.tagClass == 3;
			}
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x0001704A File Offset: 0x0001524A
		public Asn1Identifier(int tagClass, bool constructed, int tag)
		{
			this.tagClass = tagClass;
			this.constructed = constructed;
			this.tag = tag;
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x00017068 File Offset: 0x00015268
		public Asn1Identifier(Stream in_Renamed)
		{
			int num = in_Renamed.ReadByte();
			this.encodedLength++;
			if (num < 0)
			{
				throw new EndOfStreamException("BERDecoder: decode: EOF in Identifier");
			}
			this.tagClass = num >> 6;
			this.constructed = (num & 32) != 0;
			this.tag = num & 31;
			if (this.tag == 31)
			{
				this.tag = this.decodeTagNumber(in_Renamed);
			}
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x000170D6 File Offset: 0x000152D6
		public Asn1Identifier()
		{
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x000170E0 File Offset: 0x000152E0
		public void reset(Stream in_Renamed)
		{
			this.encodedLength = 0;
			int num = in_Renamed.ReadByte();
			this.encodedLength++;
			if (num < 0)
			{
				throw new EndOfStreamException("BERDecoder: decode: EOF in Identifier");
			}
			this.tagClass = num >> 6;
			this.constructed = (num & 32) != 0;
			this.tag = num & 31;
			if (this.tag == 31)
			{
				this.tag = this.decodeTagNumber(in_Renamed);
			}
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x00017150 File Offset: 0x00015350
		private int decodeTagNumber(Stream in_Renamed)
		{
			int num = 0;
			for (;;)
			{
				int num2 = in_Renamed.ReadByte();
				this.encodedLength++;
				if (num2 < 0)
				{
					break;
				}
				num = (num << 7) + (num2 & 127);
				if ((num2 & 128) == 0)
				{
					return num;
				}
			}
			throw new EndOfStreamException("BERDecoder: decode: EOF in tag number");
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x00017198 File Offset: 0x00015398
		public object Clone()
		{
			object obj;
			try
			{
				obj = base.MemberwiseClone();
			}
			catch (Exception)
			{
				throw new SystemException("Internal error, cannot create clone");
			}
			return obj;
		}

		// Token: 0x0400049F RID: 1183
		public const int UNIVERSAL = 0;

		// Token: 0x040004A0 RID: 1184
		public const int APPLICATION = 1;

		// Token: 0x040004A1 RID: 1185
		public const int CONTEXT = 2;

		// Token: 0x040004A2 RID: 1186
		public const int PRIVATE = 3;

		// Token: 0x040004A3 RID: 1187
		private int tagClass;

		// Token: 0x040004A4 RID: 1188
		private bool constructed;

		// Token: 0x040004A5 RID: 1189
		private int tag;

		// Token: 0x040004A6 RID: 1190
		private int encodedLength;
	}
}
