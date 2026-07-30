using System;
using System.Security;
using System.Security.Permissions;
using Unity;

namespace System.Diagnostics.Eventing.Reader
{
	/// <summary>Allows you to subscribe to incoming events. Each time a desired event is published to an event log, the <see cref="E:System.Diagnostics.Eventing.Reader.EventLogWatcher.EventRecordWritten" /> event is raised, and the method that handles this event will be executed. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200039F RID: 927
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public class EventLogWatcher : IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.Eventing.Reader.EventLogWatcher" /> class by specifying an event query.</summary>
		/// <param name="eventQuery">Specifies a query for the event subscription. When an event is logged that matches the criteria expressed in the query, then the <see cref="E:System.Diagnostics.Eventing.Reader.EventLogWatcher.EventRecordWritten" /> event is raised. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001B85 RID: 7045 RVA: 0x0000220F File Offset: 0x0000040F
		public EventLogWatcher(EventLogQuery eventQuery)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.Eventing.Reader.EventLogWatcher" /> class by specifying an event query and a bookmark that is used as starting position for the query.</summary>
		/// <param name="eventQuery">Specifies a query for the event subscription. When an event is logged that matches the criteria expressed in the query, then the <see cref="E:System.Diagnostics.Eventing.Reader.EventLogWatcher.EventRecordWritten" /> event is raised.</param>
		/// <param name="bookmark">The bookmark (placeholder) used as a starting position in the event log or stream of events. Only events that have been logged after the bookmark event will be returned by the query.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001B86 RID: 7046 RVA: 0x0000220F File Offset: 0x0000040F
		public EventLogWatcher(EventLogQuery eventQuery, EventBookmark bookmark)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.Eventing.Reader.EventLogWatcher" /> class by specifying an event query, a bookmark that is used as starting position for the query, and a Boolean value that determines whether to read the events that already exist in the event log.</summary>
		/// <param name="eventQuery">Specifies a query for the event subscription. When an event is logged that matches the criteria expressed in the query, then the <see cref="E:System.Diagnostics.Eventing.Reader.EventLogWatcher.EventRecordWritten" /> event is raised.</param>
		/// <param name="bookmark">The bookmark (placeholder) used as a starting position in the event log or stream of events. Only events that have been logged after the bookmark event will be returned by the query.</param>
		/// <param name="readExistingEvents">A Boolean value that determines whether to read the events that already exist in the event log. If this value is true, then the existing events are read and if this value is false, then the existing events are not read.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001B87 RID: 7047 RVA: 0x0000220F File Offset: 0x0000040F
		public EventLogWatcher(EventLogQuery eventQuery, EventBookmark bookmark, bool readExistingEvents)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.Eventing.Reader.EventLogWatcher" /> class by specifying the name or path to an event log.</summary>
		/// <param name="path">The path or name of the event log monitor for events. If any event is logged in this event log, then the <see cref="E:System.Diagnostics.Eventing.Reader.EventLogWatcher.EventRecordWritten" /> event is raised.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001B88 RID: 7048 RVA: 0x0000220F File Offset: 0x0000040F
		public EventLogWatcher(string path)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Determines whether this object starts delivering events to the event delegate.</summary>
		/// <returns>Returns true when this object can deliver events to the event delegate, and returns false when this object has stopped delivery.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700053A RID: 1338
		// (get) Token: 0x06001B89 RID: 7049 RVA: 0x00056A2C File Offset: 0x00054C2C
		// (set) Token: 0x06001B8A RID: 7050 RVA: 0x0000220F File Offset: 0x0000040F
		public bool Enabled
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Allows setting a delegate (event handler method) that gets called every time an event is published that matches the criteria specified in the event query for this object. </summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06001B8B RID: 7051 RVA: 0x0000220F File Offset: 0x0000040F
		// (remove) Token: 0x06001B8C RID: 7052 RVA: 0x0000220F File Offset: 0x0000040F
		public event EventHandler<EventRecordWrittenEventArgs> EventRecordWritten
		{
			add
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
			remove
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Releases all the resources used by this object.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001B8D RID: 7053 RVA: 0x0000220F File Offset: 0x0000040F
		public void Dispose()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Releases the unmanaged resources used by this object, and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
		// Token: 0x06001B8E RID: 7054 RVA: 0x0000220F File Offset: 0x0000040F
		[SecuritySafeCritical]
		protected virtual void Dispose(bool disposing)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
