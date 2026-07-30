using System;

namespace System.Web.Services.Protocols
{
	/// <summary>Represents an event handler that accepts the results of asynchronously invoked Web methods. This class cannot be inherited.</summary>
	/// <param name="sender">A reference to the Web service proxy.</param>
	/// <param name="e">An <see cref="T:System.Web.Services.Protocols.InvokeCompletedEventArgs" /> containing the results of the method invocation.</param>
	// Token: 0x0200001E RID: 30
	// (Invoke) Token: 0x060000A9 RID: 169
	public delegate void InvokeCompletedEventHandler(object sender, InvokeCompletedEventArgs e);
}
