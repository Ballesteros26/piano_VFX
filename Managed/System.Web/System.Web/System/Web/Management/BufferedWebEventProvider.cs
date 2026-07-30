using System;
using Unity;

namespace System.Web.Management
{
	/// <summary>Provides the base functionality for creating event providers that require buffering.</summary>
	// Token: 0x02000741 RID: 1857
	public abstract class BufferedWebEventProvider : WebEventProvider
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Management.BufferedWebEventProvider" /> class. </summary>
		// Token: 0x06004C7F RID: 19583 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected BufferedWebEventProvider()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets a value indicating the buffering mode used by the provider.</summary>
		/// <returns>The name of the buffering mode. </returns>
		// Token: 0x17001787 RID: 6023
		// (get) Token: 0x06004C80 RID: 19584 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public string BufferMode
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets a value indicating whether the provider is in buffered mode.</summary>
		/// <returns>true if the provider is in buffered mode; otherwise, false. The default is true.</returns>
		// Token: 0x17001788 RID: 6024
		// (get) Token: 0x06004C81 RID: 19585 RVA: 0x000CAEE0 File Offset: 0x000C90E0
		public bool UseBuffering
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
		}

		/// <summary>Moves the events from the provider's buffer into the event log. </summary>
		// Token: 0x06004C82 RID: 19586 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public override void Flush()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Processes the event passed to the provider.</summary>
		/// <param name="eventRaised">The <see cref="T:System.Web.Management.WebBaseEvent" /> object to process.</param>
		// Token: 0x06004C83 RID: 19587 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public override void ProcessEvent(WebBaseEvent eventRaised)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Processes the buffered events.</summary>
		/// <param name="flushInfo">A <see cref="T:System.Web.Management.WebEventBufferFlushInfo" /> that contains buffering information.</param>
		// Token: 0x06004C84 RID: 19588
		public abstract void ProcessEventFlush(WebEventBufferFlushInfo flushInfo);

		/// <summary>Performs tasks associated with shutting down the provider.</summary>
		// Token: 0x06004C85 RID: 19589 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public override void Shutdown()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
