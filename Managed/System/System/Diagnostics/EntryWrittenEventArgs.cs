using System;

namespace System.Diagnostics
{
	/// <summary>Provides data for the <see cref="E:System.Diagnostics.EventLog.EntryWritten" /> event.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001ED RID: 493
	public class EntryWrittenEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.EntryWrittenEventArgs" /> class.</summary>
		// Token: 0x06000F95 RID: 3989 RVA: 0x000482E4 File Offset: 0x000464E4
		public EntryWrittenEventArgs()
			: this(null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.EntryWrittenEventArgs" /> class with the specified event log entry.</summary>
		/// <param name="entry">An <see cref="T:System.Diagnostics.EventLogEntry" /> that represents the entry that was written. </param>
		// Token: 0x06000F96 RID: 3990 RVA: 0x000482ED File Offset: 0x000464ED
		public EntryWrittenEventArgs(EventLogEntry entry)
		{
			this.entry = entry;
		}

		/// <summary>Gets the event log entry that was written to the log.</summary>
		/// <returns>An <see cref="T:System.Diagnostics.EventLogEntry" /> that represents the entry that was written to the event log.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170002FE RID: 766
		// (get) Token: 0x06000F97 RID: 3991 RVA: 0x000482FC File Offset: 0x000464FC
		public EventLogEntry Entry
		{
			get
			{
				return this.entry;
			}
		}

		// Token: 0x04001121 RID: 4385
		private EventLogEntry entry;
	}
}
