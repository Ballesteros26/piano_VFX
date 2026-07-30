using System;

namespace System.Windows.Forms
{
	/// <summary>Provides options that specify the relationship between the control and preprocessing messages.</summary>
	// Token: 0x0200028E RID: 654
	public enum PreProcessControlState
	{
		/// <summary>Specifies that the message has been processed and no further processing is required.</summary>
		// Token: 0x04001512 RID: 5394
		MessageProcessed,
		/// <summary>Specifies that the control requires the message and that processing should continue.</summary>
		// Token: 0x04001513 RID: 5395
		MessageNeeded,
		/// <summary>Specifies that the control does not require the message.</summary>
		// Token: 0x04001514 RID: 5396
		MessageNotNeeded
	}
}
