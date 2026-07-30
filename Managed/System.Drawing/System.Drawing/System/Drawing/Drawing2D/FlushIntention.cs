using System;

namespace System.Drawing.Drawing2D
{
	/// <summary>Specifies whether commands in the graphics stack are terminated (flushed) immediately or executed as soon as possible.</summary>
	// Token: 0x0200013A RID: 314
	public enum FlushIntention
	{
		/// <summary>Specifies that the stack of all graphics operations is flushed immediately.</summary>
		// Token: 0x04000AC1 RID: 2753
		Flush,
		/// <summary>Specifies that all graphics operations on the stack are executed as soon as possible. This synchronizes the graphics state.</summary>
		// Token: 0x04000AC2 RID: 2754
		Sync
	}
}
