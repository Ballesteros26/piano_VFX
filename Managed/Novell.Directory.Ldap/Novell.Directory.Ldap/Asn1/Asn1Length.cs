using System;
using System.IO;

namespace Novell.Directory.Ldap.Asn1
{
	// Token: 0x020000D5 RID: 213
	public class Asn1Length
	{
		// Token: 0x17000179 RID: 377
		// (get) Token: 0x06000541 RID: 1345 RVA: 0x00017238 File Offset: 0x00015438
		public virtual int Length
		{
			get
			{
				return this.length;
			}
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x06000542 RID: 1346 RVA: 0x00017240 File Offset: 0x00015440
		public virtual int EncodedLength
		{
			get
			{
				return this.encodedLength;
			}
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x00017248 File Offset: 0x00015448
		public Asn1Length()
		{
		}

		// Token: 0x06000544 RID: 1348 RVA: 0x00017250 File Offset: 0x00015450
		public Asn1Length(int length)
		{
			this.length = length;
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x00017260 File Offset: 0x00015460
		public Asn1Length(Stream in_Renamed)
		{
			int i = in_Renamed.ReadByte();
			this.encodedLength++;
			if (i == 128)
			{
				this.length = -1;
				return;
			}
			if (i < 128)
			{
				this.length = i;
				return;
			}
			this.length = 0;
			for (i &= 127; i > 0; i--)
			{
				int num = in_Renamed.ReadByte();
				this.encodedLength++;
				if (num < 0)
				{
					throw new EndOfStreamException("BERDecoder: decode: EOF in Asn1Length");
				}
				this.length = (this.length << 8) + num;
			}
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x000172F4 File Offset: 0x000154F4
		public void reset(Stream in_Renamed)
		{
			this.encodedLength = 0;
			int i = in_Renamed.ReadByte();
			this.encodedLength++;
			if (i == 128)
			{
				this.length = -1;
				return;
			}
			if (i < 128)
			{
				this.length = i;
				return;
			}
			this.length = 0;
			for (i &= 127; i > 0; i--)
			{
				int num = in_Renamed.ReadByte();
				this.encodedLength++;
				if (num < 0)
				{
					throw new EndOfStreamException("BERDecoder: decode: EOF in Asn1Length");
				}
				this.length = (this.length << 8) + num;
			}
		}

		// Token: 0x040004A9 RID: 1193
		private int length;

		// Token: 0x040004AA RID: 1194
		private int encodedLength;
	}
}
