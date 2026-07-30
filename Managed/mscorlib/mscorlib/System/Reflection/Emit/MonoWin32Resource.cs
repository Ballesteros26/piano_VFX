using System;

namespace System.Reflection.Emit
{
	// Token: 0x02000346 RID: 838
	internal struct MonoWin32Resource
	{
		// Token: 0x060024F0 RID: 9456 RVA: 0x0008498A File Offset: 0x00082B8A
		public MonoWin32Resource(int res_type, int res_id, int lang_id, byte[] data)
		{
			this.res_type = res_type;
			this.res_id = res_id;
			this.lang_id = lang_id;
			this.data = data;
		}

		// Token: 0x04001390 RID: 5008
		public int res_type;

		// Token: 0x04001391 RID: 5009
		public int res_id;

		// Token: 0x04001392 RID: 5010
		public int lang_id;

		// Token: 0x04001393 RID: 5011
		public byte[] data;
	}
}
