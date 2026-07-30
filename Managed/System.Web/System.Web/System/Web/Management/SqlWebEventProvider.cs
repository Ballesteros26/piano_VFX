using System;
using System.Security.Permissions;
using Unity;

namespace System.Web.Management
{
	/// <summary>Implements an event provider that saves event notifications to an SQL database.</summary>
	// Token: 0x0200074D RID: 1869
	[PermissionSet(SecurityAction.InheritanceDemand, Unrestricted = true)]
	public class SqlWebEventProvider : BufferedWebEventProvider
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Management.SqlWebEventProvider" /> class.</summary>
		// Token: 0x06004CCB RID: 19659 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected internal SqlWebEventProvider()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Called when event processing is complete.</summary>
		/// <param name="raisedEvents">A <see cref="T:System.Web.Management.WebBaseEventCollection" /> object of events raised.</param>
		// Token: 0x06004CCC RID: 19660 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void EventProcessingComplete(WebBaseEventCollection raisedEvents)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Processes the event passed to the provider.</summary>
		/// <param name="eventRaised">The <see cref="T:System.Web.Management.WebBaseEvent" /> object to process.</param>
		// Token: 0x06004CCD RID: 19661 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public override void ProcessEvent(WebBaseEvent eventRaised)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Removes all events from the provider's buffer.</summary>
		/// <param name="flushInfo">The <see cref="T:System.Web.Management.WebEventBufferFlushInfo" /> object that contains the buffer information to be flushed.</param>
		// Token: 0x06004CCE RID: 19662 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public override void ProcessEventFlush(WebEventBufferFlushInfo flushInfo)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Performs tasks associated with shutting down the provider.</summary>
		// Token: 0x06004CCF RID: 19663 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public override void Shutdown()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
