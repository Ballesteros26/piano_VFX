using System;

namespace System.Diagnostics
{
	/// <summary>Specifies the file structure that will be used for the <see cref="T:System.Diagnostics.EventSchemaTraceListener" /> log.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200037B RID: 891
	public enum TraceLogRetentionOption
	{
		/// <summary>A finite number of sequential files, each with a maximum file size. When the <see cref="P:System.Diagnostics.EventSchemaTraceListener.MaximumFileSize" /> property value is reached, writing starts in a new file with an incremented integer suffix. When the <see cref="P:System.Diagnostics.EventSchemaTraceListener.MaximumNumberOfFiles" /> property value is reached, the first file is cleared and overwritten. Files are then incrementally overwritten in a circular manner.</summary>
		// Token: 0x04000BCF RID: 3023
		LimitedCircularFiles = 1,
		/// <summary>A finite number of sequential files, each with a maximum file size. When the <see cref="P:System.Diagnostics.EventSchemaTraceListener.MaximumFileSize" /> property value is reached, writing starts in a new file with an incremented integer suffix.</summary>
		// Token: 0x04000BD0 RID: 3024
		LimitedSequentialFiles = 3,
		/// <summary>One file with a maximum file size that is determined by the <see cref="P:System.Diagnostics.EventSchemaTraceListener.MaximumFileSize" /> property.</summary>
		// Token: 0x04000BD1 RID: 3025
		SingleFileBoundedSize,
		/// <summary>One file with no maximum file size restriction.</summary>
		// Token: 0x04000BD2 RID: 3026
		SingleFileUnboundedSize = 2,
		/// <summary>An unlimited number of sequential files, each with a maximum file size that is determined by the <see cref="P:System.Diagnostics.EventSchemaTraceListener.MaximumFileSize" /> property. There is no logical bound to the number or size of the files, but it is limited by the physical constraints imposed by the computer.</summary>
		// Token: 0x04000BD3 RID: 3027
		UnlimitedSequentialFiles = 0
	}
}
