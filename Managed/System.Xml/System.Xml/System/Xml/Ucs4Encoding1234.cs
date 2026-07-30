using System;

namespace System.Xml
{
	// Token: 0x02000293 RID: 659
	internal class Ucs4Encoding1234 : Ucs4Encoding
	{
		// Token: 0x06001899 RID: 6297 RVA: 0x0008EA6C File Offset: 0x0008CC6C
		public Ucs4Encoding1234()
		{
			this.ucs4Decoder = new Ucs4Decoder1234();
		}

		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x0600189A RID: 6298 RVA: 0x0008EA7F File Offset: 0x0008CC7F
		public override string EncodingName
		{
			get
			{
				return "ucs-4 (Bigendian)";
			}
		}

		// Token: 0x0600189B RID: 6299 RVA: 0x0008EA86 File Offset: 0x0008CC86
		public override byte[] GetPreamble()
		{
			return new byte[] { 0, 0, 254, byte.MaxValue };
		}
	}
}
