using System;

namespace System.Web
{
	/// <summary>Represents the method that handles asynchronous events such as application events. This delegate is called at the start of an asynchronous operation.</summary>
	/// <returns>The <see cref="T:System.IAsyncResult" /> that represents the result of the <see cref="T:System.Web.BeginEventHandler" /> operation.</returns>
	/// <param name="sender">The source of the event. </param>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	/// <param name="cb">The delegate to call when the asynchronous method call is complete. If <paramref name="cb" /> is null, the delegate is not called. </param>
	/// <param name="extraData">Any additional data needed to process the request. </param>
	// Token: 0x02000064 RID: 100
	// (Invoke) Token: 0x0600041E RID: 1054
	public delegate IAsyncResult BeginEventHandler(object sender, EventArgs e, AsyncCallback cb, object extraData);
}
