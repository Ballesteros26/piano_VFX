using System;

namespace System.Diagnostics.Eventing.Reader
{
	/// <summary>Specifies that a string contains a name of an event log or the file system path to an event log file.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200038D RID: 909
	public enum PathType
	{
		/// <summary>A path parameter contains the file system path to an event log file.</summary>
		// Token: 0x04000C0A RID: 3082
		FilePath = 2,
		/// <summary>A path parameter contains the name of the event log.</summary>
		// Token: 0x04000C0B RID: 3083
		LogName = 1
	}
}
