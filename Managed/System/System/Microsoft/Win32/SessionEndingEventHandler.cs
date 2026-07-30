using System;

namespace Microsoft.Win32
{
	/// <summary>Represents the method that will handle the <see cref="E:Microsoft.Win32.SystemEvents.SessionEnding" /> event from the operating system.</summary>
	/// <param name="sender">The source of the event. When this event is raised by the <see cref="T:Microsoft.Win32.SystemEvents" /> class, this object is always null. </param>
	/// <param name="e">A <see cref="T:Microsoft.Win32.SessionEndingEventArgs" /> that contains the event data. </param>
	// Token: 0x020000D3 RID: 211
	// (Invoke) Token: 0x060004B2 RID: 1202
	public delegate void SessionEndingEventHandler(object sender, SessionEndingEventArgs e);
}
