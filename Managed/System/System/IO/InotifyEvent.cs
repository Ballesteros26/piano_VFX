using System;

namespace System.IO
{
	// Token: 0x020003D4 RID: 980
	internal struct InotifyEvent
	{
		// Token: 0x06001E07 RID: 7687 RVA: 0x0007705D File Offset: 0x0007525D
		public override string ToString()
		{
			return string.Format("[Descriptor: {0} Mask: {1} Name: {2}]", this.WatchDescriptor, this.Mask, this.Name);
		}

		// Token: 0x04001A42 RID: 6722
		public static readonly InotifyEvent Default;

		// Token: 0x04001A43 RID: 6723
		public int WatchDescriptor;

		// Token: 0x04001A44 RID: 6724
		public InotifyMask Mask;

		// Token: 0x04001A45 RID: 6725
		public string Name;
	}
}
