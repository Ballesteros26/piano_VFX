using System;

namespace System.Windows.Forms
{
	// Token: 0x020003F0 RID: 1008
	internal struct XSelectionClearEvent
	{
		// Token: 0x04001EDD RID: 7901
		internal XEventName type;

		// Token: 0x04001EDE RID: 7902
		internal IntPtr serial;

		// Token: 0x04001EDF RID: 7903
		internal bool send_event;

		// Token: 0x04001EE0 RID: 7904
		internal IntPtr display;

		// Token: 0x04001EE1 RID: 7905
		internal IntPtr window;

		// Token: 0x04001EE2 RID: 7906
		internal IntPtr selection;

		// Token: 0x04001EE3 RID: 7907
		internal IntPtr time;
	}
}
