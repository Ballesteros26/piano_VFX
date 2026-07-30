using System;

namespace System.IO.Pipes
{
	/// <summary>Specifies the transmission mode of the pipe.</summary>
	// Token: 0x0200003B RID: 59
	[Serializable]
	public enum PipeTransmissionMode
	{
		/// <summary>Indicates that data in the pipe is transmitted and read as a stream of bytes.</summary>
		// Token: 0x04000224 RID: 548
		Byte,
		/// <summary>Indicates that data in the pipe is transmitted and read as a stream of messages.</summary>
		// Token: 0x04000225 RID: 549
		Message
	}
}
