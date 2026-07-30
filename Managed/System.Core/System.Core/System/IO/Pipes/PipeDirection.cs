using System;

namespace System.IO.Pipes
{
	/// <summary>Specifies the direction of the pipe.</summary>
	// Token: 0x02000031 RID: 49
	[Serializable]
	public enum PipeDirection
	{
		/// <summary>Specifies that the pipe direction is in.</summary>
		// Token: 0x0400020F RID: 527
		In = 1,
		/// <summary>Specifies that the pipe direction is out.</summary>
		// Token: 0x04000210 RID: 528
		Out,
		/// <summary>Specifies that the pipe direction is two-way.</summary>
		// Token: 0x04000211 RID: 529
		InOut
	}
}
