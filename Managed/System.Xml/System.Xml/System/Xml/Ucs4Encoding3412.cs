using System;

namespace System.Xml
{
	// Token: 0x02000296 RID: 662
	internal class Ucs4Encoding3412 : Ucs4Encoding
	{
		// Token: 0x060018A2 RID: 6306 RVA: 0x0008EB02 File Offset: 0x0008CD02
		public Ucs4Encoding3412()
		{
			this.ucs4Decoder = new Ucs4Decoder3412();
		}

		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x060018A3 RID: 6307 RVA: 0x0008EB15 File Offset: 0x0008CD15
		public override string EncodingName
		{
			get
			{
				return "ucs-4 (order 3412)";
			}
		}

		// Token: 0x060018A4 RID: 6308 RVA: 0x0008EB1C File Offset: 0x0008CD1C
		public override byte[] GetPreamble()
		{
			byte[] array = new byte[4];
			array[0] = 254;
			array[1] = byte.MaxValue;
			return array;
		}
	}
}
