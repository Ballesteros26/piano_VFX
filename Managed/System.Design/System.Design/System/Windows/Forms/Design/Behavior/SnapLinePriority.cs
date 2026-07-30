using System;

namespace System.Windows.Forms.Design.Behavior
{
	/// <summary>Specifies the relative importance of a snapline.</summary>
	// Token: 0x0200004E RID: 78
	public enum SnapLinePriority
	{
		/// <summary>The lowest priority category.</summary>
		// Token: 0x0400010A RID: 266
		Low = 1,
		/// <summary>The middle priority category.</summary>
		// Token: 0x0400010B RID: 267
		Medium,
		/// <summary>The highest priority category.</summary>
		// Token: 0x0400010C RID: 268
		High,
		/// <summary>The priority category that is equivalent to the highest priority of all the current snaplines. Indicates that this category of snapline should always be active.</summary>
		// Token: 0x0400010D RID: 269
		Always
	}
}
