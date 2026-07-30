using System;
using System.Security.Permissions;
using Unity;

namespace System.Diagnostics.Eventing.Reader
{
	/// <summary>Allows you to access the run-time properties of active event logs and event log files. These properties include the number of events in the log, the size of the log, a value that determines whether the log is full, and the last time the log was written to or accessed.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200038E RID: 910
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class EventLogInformation
	{
		// Token: 0x06001B06 RID: 6918 RVA: 0x0000220F File Offset: 0x0000040F
		internal EventLogInformation()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the file attributes of the log file associated with the log.</summary>
		/// <returns>Returns an integer value. This value can be null.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170004F5 RID: 1269
		// (get) Token: 0x06001B07 RID: 6919 RVA: 0x00056738 File Offset: 0x00054938
		public int? Attributes
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the time that the log file associated with the event log was created.</summary>
		/// <returns>Returns a <see cref="T:System.DateTime" /> object. This value can be null.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170004F6 RID: 1270
		// (get) Token: 0x06001B08 RID: 6920 RVA: 0x00056754 File Offset: 0x00054954
		public DateTime? CreationTime
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the size of the file, in bytes, associated with the event log.</summary>
		/// <returns>Returns a long value.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170004F7 RID: 1271
		// (get) Token: 0x06001B09 RID: 6921 RVA: 0x00056770 File Offset: 0x00054970
		public long? FileSize
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets a Boolean value that determines whether the log file has reached its maximum size (the log is full).</summary>
		/// <returns>Returns true if the log is full, and returns false if the log is not full.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x06001B0A RID: 6922 RVA: 0x0005678C File Offset: 0x0005498C
		public bool? IsLogFull
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the last time the log file associated with the event log was accessed.</summary>
		/// <returns>Returns a <see cref="T:System.DateTime" /> object. This value can be null.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x06001B0B RID: 6923 RVA: 0x000567A8 File Offset: 0x000549A8
		public DateTime? LastAccessTime
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the last time data was written to the log file associated with the event log.</summary>
		/// <returns>Returns a <see cref="T:System.DateTime" /> object. This value can be null.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170004FA RID: 1274
		// (get) Token: 0x06001B0C RID: 6924 RVA: 0x000567C4 File Offset: 0x000549C4
		public DateTime? LastWriteTime
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the number of the oldest event record in the event log.</summary>
		/// <returns>Returns a long value that represents the number of the oldest event record in the event log. This value can be null.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170004FB RID: 1275
		// (get) Token: 0x06001B0D RID: 6925 RVA: 0x000567E0 File Offset: 0x000549E0
		public long? OldestRecordNumber
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the number of event records in the event log.</summary>
		/// <returns>Returns a long value that represents the number of event records in the event log. This value can be null.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170004FC RID: 1276
		// (get) Token: 0x06001B0E RID: 6926 RVA: 0x000567FC File Offset: 0x000549FC
		public long? RecordCount
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}
	}
}
