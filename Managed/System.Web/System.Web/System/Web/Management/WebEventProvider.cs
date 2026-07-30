using System;
using System.Configuration.Provider;
using Unity;

namespace System.Web.Management
{
	/// <summary>Provides the base class for non buffered event providers.</summary>
	// Token: 0x02000742 RID: 1858
	public abstract class WebEventProvider : ProviderBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Management.WebEventProvider" /> class.</summary>
		// Token: 0x06004C86 RID: 19590 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected WebEventProvider()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Moves the events from the provider's buffer into the event log. </summary>
		// Token: 0x06004C87 RID: 19591
		public abstract void Flush();

		/// <summary>Processes the event passed to the provider.</summary>
		/// <param name="raisedEvent">The <see cref="T:System.Web.Management.WebBaseEvent" /> object to process.</param>
		// Token: 0x06004C88 RID: 19592
		public abstract void ProcessEvent(WebBaseEvent raisedEvent);

		/// <summary>Performs tasks associated with shutting down the provider.</summary>
		// Token: 0x06004C89 RID: 19593
		public abstract void Shutdown();
	}
}
