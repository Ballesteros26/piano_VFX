using System;

namespace System.IO.Pipes
{
	/// <summary>Provides options for creating a <see cref="T:System.IO.Pipes.PipeStream" /> object. This enumeration has a <see cref="T:System.FlagsAttribute" /> attribute that allows a bitwise combination of its member values.</summary>
	// Token: 0x02000037 RID: 55
	[Flags]
	public enum PipeOptions
	{
		/// <summary>Indicates that there are no additional parameters.</summary>
		// Token: 0x04000213 RID: 531
		None = 0,
		/// <summary>Indicates that the system should write through any intermediate cache and go directly to the pipe.</summary>
		// Token: 0x04000214 RID: 532
		WriteThrough = -2147483648,
		/// <summary>Indicates that the pipe can be used for asynchronous reading and writing.</summary>
		// Token: 0x04000215 RID: 533
		Asynchronous = 1073741824
	}
}
