using System;

namespace System.Xml
{
	// Token: 0x02000295 RID: 661
	internal class Ucs4Encoding2143 : Ucs4Encoding
	{
		// Token: 0x0600189F RID: 6303 RVA: 0x0008EAD0 File Offset: 0x0008CCD0
		public Ucs4Encoding2143()
		{
			this.ucs4Decoder = new Ucs4Decoder2143();
		}

		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x060018A0 RID: 6304 RVA: 0x0008EAE3 File Offset: 0x0008CCE3
		public override string EncodingName
		{
			get
			{
				return "ucs-4 (order 2143)";
			}
		}

		// Token: 0x060018A1 RID: 6305 RVA: 0x0008EAEA File Offset: 0x0008CCEA
		public override byte[] GetPreamble()
		{
			return new byte[] { 0, 0, byte.MaxValue, 254 };
		}
	}
}
