using System;
using System.Runtime.InteropServices;

namespace System
{
	/// <summary>Represents the method that will handle an event that has no event data.</summary>
	/// <param name="sender">The source of the event. </param>
	/// <param name="e">An object that contains no event data. </param>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200015B RID: 347
	// (Invoke) Token: 0x06000EFB RID: 3835
	[ComVisible(true)]
	[Serializable]
	public delegate void EventHandler(object sender, EventArgs e);
}
