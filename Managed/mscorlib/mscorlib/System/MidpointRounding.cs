using System;
using System.Runtime.InteropServices;

namespace System
{
	/// <summary>Specifies how mathematical rounding methods should process a number that is midway between two numbers.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200019D RID: 413
	[ComVisible(true)]
	public enum MidpointRounding
	{
		/// <summary>When a number is halfway between two others, it is rounded toward the nearest even number.</summary>
		// Token: 0x04000A37 RID: 2615
		ToEven,
		/// <summary>When a number is halfway between two others, it is rounded toward the nearest number that is away from zero.</summary>
		// Token: 0x04000A38 RID: 2616
		AwayFromZero
	}
}
