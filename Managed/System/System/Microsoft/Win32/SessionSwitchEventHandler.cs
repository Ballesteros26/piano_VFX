using System;

namespace Microsoft.Win32
{
	/// <summary>Represents the method that will handle the <see cref="E:Microsoft.Win32.SystemEvents.SessionSwitch" /> event.</summary>
	/// <param name="sender">The source of the event. </param>
	/// <param name="e">A <see cref="T:Microsoft.Win32.SessionSwitchEventArgs" /> indicating the type of the session change event. </param>
	// Token: 0x020000D5 RID: 213
	// (Invoke) Token: 0x060004B8 RID: 1208
	public delegate void SessionSwitchEventHandler(object sender, SessionSwitchEventArgs e);
}
