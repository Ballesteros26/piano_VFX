using System;

namespace System.Xml.Schema
{
	// Token: 0x02000393 RID: 915
	internal class KSStruct
	{
		// Token: 0x060024F4 RID: 9460 RVA: 0x000DF9B3 File Offset: 0x000DDBB3
		public KSStruct(KeySequence ks, int dim)
		{
			this.ks = ks;
			this.fields = new LocatedActiveAxis[dim];
		}

		// Token: 0x04001910 RID: 6416
		public int depth;

		// Token: 0x04001911 RID: 6417
		public KeySequence ks;

		// Token: 0x04001912 RID: 6418
		public LocatedActiveAxis[] fields;
	}
}
