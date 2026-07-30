using System;

namespace System.Web
{
	/// <summary>Converts task-returning asynchronous methods into methods that use the asynchronous programming model used in previous versions of ASP.NET and that is based on begin and end events.</summary>
	// Token: 0x0200006D RID: 109
	public sealed class EventHandlerTaskAsyncHelper
	{
		/// <summary>Represents the <see cref="T:System.Web.BeginEventHandler" /> method for an asynchronous task.</summary>
		/// <returns>The method that handles the begin event for the asynchronous task.</returns>
		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x0600044B RID: 1099 RVA: 0x00008CAE File Offset: 0x00006EAE
		public BeginEventHandler BeginEventHandler
		{
			get
			{
				return this.beginEventHandler;
			}
		}

		/// <summary>Represents the <see cref="T:System.Web.EndEventHandler" /> method for an asynchronous task.</summary>
		/// <returns>The method that handles the end event for the asynchronous task.</returns>
		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x0600044C RID: 1100 RVA: 0x00008CB6 File Offset: 0x00006EB6
		public EndEventHandler EndEventHandler
		{
			get
			{
				return EventHandlerTaskAsyncHelper.endEventHandler;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.EventHandlerTaskAsyncHelper" /> class.</summary>
		/// <param name="handler">The asynchronous task.</param>
		// Token: 0x0600044D RID: 1101 RVA: 0x00008CBD File Offset: 0x00006EBD
		public EventHandlerTaskAsyncHelper(TaskEventHandler handler)
		{
			if (handler == null)
			{
				throw new ArgumentNullException("handler");
			}
			this.taskEventHandler = handler;
			this.beginEventHandler = new BeginEventHandler(this.GetAsyncResult);
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x00008CEC File Offset: 0x00006EEC
		private IAsyncResult GetAsyncResult(object sender, EventArgs e, AsyncCallback callback, object state)
		{
			return TaskAsyncResult.GetAsyncResult(this.taskEventHandler(sender, e), callback, state);
		}

		// Token: 0x04000E63 RID: 3683
		private readonly TaskEventHandler taskEventHandler;

		// Token: 0x04000E64 RID: 3684
		private readonly BeginEventHandler beginEventHandler;

		// Token: 0x04000E65 RID: 3685
		private static readonly EndEventHandler endEventHandler = new EndEventHandler(TaskAsyncResult.Wait);
	}
}
