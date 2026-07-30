using System;
using System.Security.Permissions;
using Unity;

namespace System.Diagnostics.Eventing.Reader
{
	/// <summary>When the <see cref="E:System.Diagnostics.Eventing.Reader.EventLogWatcher.EventRecordWritten" /> event is raised, an instance of this object is passed to the delegate method that handles the event. This object contains the event that was published to the event log or the exception that occurred when the event subscription failed. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020003A0 RID: 928
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class EventRecordWrittenEventArgs : EventArgs
	{
		// Token: 0x06001B8F RID: 7055 RVA: 0x0000220F File Offset: 0x0000040F
		internal EventRecordWrittenEventArgs()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the exception that occurred when the event subscription failed. The exception has a description of why the subscription failed.</summary>
		/// <returns>Returns an <see cref="T:System.Exception" /> object.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700053B RID: 1339
		// (get) Token: 0x06001B90 RID: 7056 RVA: 0x000560B4 File Offset: 0x000542B4
		public Exception EventException
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the event record that is published to the event log. This event matches the criteria from the query specified in the event subscription.</summary>
		/// <returns>Returns a <see cref="T:System.Diagnostics.Eventing.Reader.EventRecord" /> object.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700053C RID: 1340
		// (get) Token: 0x06001B91 RID: 7057 RVA: 0x000560B4 File Offset: 0x000542B4
		public EventRecord EventRecord
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}
	}
}
