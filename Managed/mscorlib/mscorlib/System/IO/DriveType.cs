using System;
using System.Runtime.InteropServices;

namespace System.IO
{
	/// <summary>Defines constants for drive types, including CDRom, Fixed, Network, NoRootDirectory, Ram, Removable, and Unknown.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020003CF RID: 975
	[ComVisible(true)]
	[Serializable]
	public enum DriveType
	{
		/// <summary>The drive is an optical disc device, such as a CD or DVD-ROM.</summary>
		// Token: 0x040017B9 RID: 6073
		CDRom = 5,
		/// <summary>The drive is a fixed disk.</summary>
		// Token: 0x040017BA RID: 6074
		Fixed = 3,
		/// <summary>The drive is a network drive.</summary>
		// Token: 0x040017BB RID: 6075
		Network,
		/// <summary>The drive does not have a root directory.</summary>
		// Token: 0x040017BC RID: 6076
		NoRootDirectory = 1,
		/// <summary>The drive is a RAM disk.</summary>
		// Token: 0x040017BD RID: 6077
		Ram = 6,
		/// <summary>The drive is a removable storage device, such as a floppy disk drive or a USB flash drive.</summary>
		// Token: 0x040017BE RID: 6078
		Removable = 2,
		/// <summary>The type of drive is unknown.</summary>
		// Token: 0x040017BF RID: 6079
		Unknown = 0
	}
}
