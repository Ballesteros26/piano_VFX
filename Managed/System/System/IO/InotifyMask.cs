using System;

namespace System.IO
{
	// Token: 0x020003D3 RID: 979
	[Flags]
	internal enum InotifyMask : uint
	{
		// Token: 0x04001A2D RID: 6701
		Access = 1U,
		// Token: 0x04001A2E RID: 6702
		Modify = 2U,
		// Token: 0x04001A2F RID: 6703
		Attrib = 4U,
		// Token: 0x04001A30 RID: 6704
		CloseWrite = 8U,
		// Token: 0x04001A31 RID: 6705
		CloseNoWrite = 16U,
		// Token: 0x04001A32 RID: 6706
		Open = 32U,
		// Token: 0x04001A33 RID: 6707
		MovedFrom = 64U,
		// Token: 0x04001A34 RID: 6708
		MovedTo = 128U,
		// Token: 0x04001A35 RID: 6709
		Create = 256U,
		// Token: 0x04001A36 RID: 6710
		Delete = 512U,
		// Token: 0x04001A37 RID: 6711
		DeleteSelf = 1024U,
		// Token: 0x04001A38 RID: 6712
		MoveSelf = 2048U,
		// Token: 0x04001A39 RID: 6713
		BaseEvents = 4095U,
		// Token: 0x04001A3A RID: 6714
		Umount = 8192U,
		// Token: 0x04001A3B RID: 6715
		Overflow = 16384U,
		// Token: 0x04001A3C RID: 6716
		Ignored = 32768U,
		// Token: 0x04001A3D RID: 6717
		OnlyDir = 16777216U,
		// Token: 0x04001A3E RID: 6718
		DontFollow = 33554432U,
		// Token: 0x04001A3F RID: 6719
		AddMask = 536870912U,
		// Token: 0x04001A40 RID: 6720
		Directory = 1073741824U,
		// Token: 0x04001A41 RID: 6721
		OneShot = 2147483648U
	}
}
