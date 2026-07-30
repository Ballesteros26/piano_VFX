using System;
using System.Collections;

namespace System.IO
{
	// Token: 0x020003D5 RID: 981
	internal class ParentInotifyData
	{
		// Token: 0x04001A46 RID: 6726
		public bool IncludeSubdirs;

		// Token: 0x04001A47 RID: 6727
		public bool Enabled;

		// Token: 0x04001A48 RID: 6728
		public ArrayList children;

		// Token: 0x04001A49 RID: 6729
		public InotifyData data;
	}
}
