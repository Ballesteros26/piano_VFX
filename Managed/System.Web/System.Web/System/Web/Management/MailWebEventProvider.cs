using System;
using Unity;

namespace System.Web.Management
{
	/// <summary>Provides the base functionality for creating event providers that send e-mail.</summary>
	// Token: 0x02000748 RID: 1864
	public abstract class MailWebEventProvider : BufferedWebEventProvider
	{
		// Token: 0x06004CAA RID: 19626 RVA: 0x0000B3E4 File Offset: 0x000095E4
		internal MailWebEventProvider()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Processes the event passed to the provider.</summary>
		/// <param name="eventRaised">The <see cref="T:System.Web.Management.WebBaseEvent" /> object to process.</param>
		// Token: 0x06004CAB RID: 19627 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public override void ProcessEvent(WebBaseEvent eventRaised)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Removes all events from the provider's buffer.</summary>
		/// <param name="flushInfo">The <see cref="T:System.Web.Management.WebEventBufferFlushInfo" /> that contains the count of events waiting to send their notification.</param>
		// Token: 0x06004CAC RID: 19628 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public override void ProcessEventFlush(WebEventBufferFlushInfo flushInfo)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Performs tasks associated with shutting down the provider.</summary>
		// Token: 0x06004CAD RID: 19629 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public override void Shutdown()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
