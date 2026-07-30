using System;

namespace System.Web.Services.Description
{
	// Token: 0x020000DB RID: 219
	internal class MimeTextReturn : MimeReturn
	{
		// Token: 0x17000171 RID: 369
		// (get) Token: 0x06000592 RID: 1426 RVA: 0x00018DAA File Offset: 0x00016FAA
		// (set) Token: 0x06000593 RID: 1427 RVA: 0x00018DB2 File Offset: 0x00016FB2
		internal MimeTextBinding TextBinding
		{
			get
			{
				return this.textBinding;
			}
			set
			{
				this.textBinding = value;
			}
		}

		// Token: 0x0400039D RID: 925
		private MimeTextBinding textBinding;
	}
}
