using System;
using System.Security.Permissions;
using Unity;

namespace System.Diagnostics
{
	/// <summary>Directs tracing or debugging output of end-to-end events to an XML-encoded, schema-compliant log file.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200037A RID: 890
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public class EventSchemaTraceListener : TextWriterTraceListener
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.EventSchemaTraceListener" /> class, using the specified file as the recipient of debugging and tracing output.</summary>
		/// <param name="fileName">The path for the log file.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001A8C RID: 6796 RVA: 0x0000220F File Offset: 0x0000040F
		public EventSchemaTraceListener(string fileName)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.EventSchemaTraceListener" /> class with the specified name, using the specified file as the recipient of debugging and tracing output.</summary>
		/// <param name="fileName">The path for the log file.</param>
		/// <param name="name">The name of the listener.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001A8D RID: 6797 RVA: 0x0000220F File Offset: 0x0000040F
		public EventSchemaTraceListener(string fileName, string name)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.EventSchemaTraceListener" /> class with the specified name and specified buffer size, using the specified file as the recipient of debugging and tracing output.</summary>
		/// <param name="fileName">The path for the log file.</param>
		/// <param name="name">The name of the listener.</param>
		/// <param name="bufferSize">The size of the output buffer, in bytes.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001A8E RID: 6798 RVA: 0x0000220F File Offset: 0x0000040F
		public EventSchemaTraceListener(string fileName, string name, int bufferSize)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.EventSchemaTraceListener" /> class with the specified name and specified buffer size, using the specified file with the specified log retention policy as the recipient of the debugging and tracing output.</summary>
		/// <param name="fileName">The path for the log file.</param>
		/// <param name="name">The name of the listener.</param>
		/// <param name="bufferSize">The size of the output buffer, in bytes.</param>
		/// <param name="logRetentionOption">One of the <see cref="T:System.Diagnostics.TraceLogRetentionOption" /> values. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001A8F RID: 6799 RVA: 0x0000220F File Offset: 0x0000040F
		public EventSchemaTraceListener(string fileName, string name, int bufferSize, TraceLogRetentionOption logRetentionOption)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.EventSchemaTraceListener" /> class with the specified name and specified buffer size, using the specified file with the specified log retention policy and maximum size as the recipient of the debugging and tracing output.</summary>
		/// <param name="fileName">The path for the log file.</param>
		/// <param name="name">The name of the listener.</param>
		/// <param name="bufferSize">The size of the output buffer, in bytes.</param>
		/// <param name="logRetentionOption">One of the <see cref="T:System.Diagnostics.TraceLogRetentionOption" /> values.</param>
		/// <param name="maximumFileSize">The maximum file size, in bytes.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="maximumFileSize" /> is less than <paramref name="bufferSize" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="maximumFileSize" /> is a negative number.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001A90 RID: 6800 RVA: 0x0000220F File Offset: 0x0000040F
		public EventSchemaTraceListener(string fileName, string name, int bufferSize, TraceLogRetentionOption logRetentionOption, long maximumFileSize)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.EventSchemaTraceListener" /> class with the specified name and specified buffer size, using the specified file with the specified log retention policy, maximum size, and file count as the recipient of the debugging and tracing output.</summary>
		/// <param name="fileName">The path for the log file.</param>
		/// <param name="name">The name of the listener.</param>
		/// <param name="bufferSize">The size of the output buffer, in bytes.</param>
		/// <param name="logRetentionOption">One of the <see cref="T:System.Diagnostics.TraceLogRetentionOption" /> values.</param>
		/// <param name="maximumFileSize">The maximum file size, in bytes.</param>
		/// <param name="maximumNumberOfFiles">The maximum number of output log files.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="maximumFileSize" /> is less than <paramref name="bufferSize" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="maximumFileSize" /> is a negative number.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="maximumNumberOfFiles" /> is less than 1, and <paramref name="logRetentionOption" /> is <see cref="F:System.Diagnostics.TraceLogRetentionOption.LimitedSequentialFiles" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="maximumNumberOfFiles" /> is less than 2, and <paramref name="logRetentionOption" /> is <see cref="F:System.Diagnostics.TraceLogRetentionOption.LimitedCircularFiles" />.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001A91 RID: 6801 RVA: 0x0000220F File Offset: 0x0000040F
		public EventSchemaTraceListener(string fileName, string name, int bufferSize, TraceLogRetentionOption logRetentionOption, long maximumFileSize, int maximumNumberOfFiles)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the size of the output buffer.</summary>
		/// <returns>The size of the output buffer, in bytes. </returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170004CB RID: 1227
		// (get) Token: 0x06001A92 RID: 6802 RVA: 0x000562EC File Offset: 0x000544EC
		public int BufferSize
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}

		/// <summary>Gets the maximum size of the log file.</summary>
		/// <returns>The maximum file size, in bytes.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170004CC RID: 1228
		// (get) Token: 0x06001A93 RID: 6803 RVA: 0x00056308 File Offset: 0x00054508
		public long MaximumFileSize
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0L;
			}
		}

		/// <summary>Gets the maximum number of log files.</summary>
		/// <returns>The maximum number of log files, determined by the value of the <see cref="P:System.Diagnostics.EventSchemaTraceListener.TraceLogRetentionOption" /> property for the file.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170004CD RID: 1229
		// (get) Token: 0x06001A94 RID: 6804 RVA: 0x00056324 File Offset: 0x00054524
		public int MaximumNumberOfFiles
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}

		/// <summary>Gets the trace log retention option for the file.</summary>
		/// <returns>One of the <see cref="T:System.Diagnostics.TraceLogRetentionOption" /> values. The default is <see cref="F:System.Diagnostics.TraceLogRetentionOption.SingleFileUnboundedSize" />. </returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170004CE RID: 1230
		// (get) Token: 0x06001A95 RID: 6805 RVA: 0x00056340 File Offset: 0x00054540
		public TraceLogRetentionOption TraceLogRetentionOption
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return TraceLogRetentionOption.UnlimitedSequentialFiles;
			}
		}
	}
}
