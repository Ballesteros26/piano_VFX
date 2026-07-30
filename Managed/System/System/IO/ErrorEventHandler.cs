using System;

namespace System.IO
{
	/// <summary>Represents the method that will handle the <see cref="E:System.IO.FileSystemWatcher.Error" /> event of a <see cref="T:System.IO.FileSystemWatcher" /> object.</summary>
	/// <param name="sender">The source of the event. </param>
	/// <param name="e">An <see cref="T:System.IO.ErrorEventArgs" /> object that contains the event data. </param>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020003C6 RID: 966
	// (Invoke) Token: 0x06001DA7 RID: 7591
	public delegate void ErrorEventHandler(object sender, ErrorEventArgs e);
}
