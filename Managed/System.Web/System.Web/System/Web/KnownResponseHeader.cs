using System;

namespace System.Web
{
	// Token: 0x020000A9 RID: 169
	internal sealed class KnownResponseHeader : BaseResponseHeader
	{
		// Token: 0x060008F7 RID: 2295 RVA: 0x0001600B File Offset: 0x0001420B
		internal KnownResponseHeader(int ID, string val)
			: base(val)
		{
			this.ID = ID;
		}

		// Token: 0x060008F8 RID: 2296 RVA: 0x0001601B File Offset: 0x0001421B
		internal override void SendContent(HttpWorkerRequest wr)
		{
			wr.SendKnownResponseHeader(this.ID, base.Value);
		}

		// Token: 0x04000FEC RID: 4076
		public int ID;
	}
}
