using System;

namespace System.Web
{
	/// <summary>Represents the method that handles asynchronous events such as application events. </summary>
	/// <param name="ar">The <see cref="T:System.IAsyncResult" /> that is the result of the <see cref="T:System.Web.BeginEventHandler" /> operation. </param>
	// Token: 0x0200006C RID: 108
	// (Invoke) Token: 0x06000448 RID: 1096
	public delegate void EndEventHandler(IAsyncResult ar);
}
