using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies the system power status.</summary>
	// Token: 0x0200028B RID: 651
	public enum PowerLineStatus
	{
		/// <summary>The system is offline.</summary>
		// Token: 0x04001506 RID: 5382
		Offline,
		/// <summary>The system is online.</summary>
		// Token: 0x04001507 RID: 5383
		Online,
		/// <summary>The power status of the system is unknown.</summary>
		// Token: 0x04001508 RID: 5384
		Unknown = 255
	}
}
