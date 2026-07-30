using System;

namespace System.Xml
{
	// Token: 0x02000294 RID: 660
	internal class Ucs4Encoding4321 : Ucs4Encoding
	{
		// Token: 0x0600189C RID: 6300 RVA: 0x0008EA9E File Offset: 0x0008CC9E
		public Ucs4Encoding4321()
		{
			this.ucs4Decoder = new Ucs4Decoder4321();
		}

		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x0600189D RID: 6301 RVA: 0x0008EAB1 File Offset: 0x0008CCB1
		public override string EncodingName
		{
			get
			{
				return "ucs-4";
			}
		}

		// Token: 0x0600189E RID: 6302 RVA: 0x0008EAB8 File Offset: 0x0008CCB8
		public override byte[] GetPreamble()
		{
			byte[] array = new byte[4];
			array[0] = byte.MaxValue;
			array[1] = 254;
			return array;
		}
	}
}
