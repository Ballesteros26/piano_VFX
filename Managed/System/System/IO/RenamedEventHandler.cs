using System;

namespace System.IO
{
	/// <summary>Represents the method that will handle the <see cref="E:System.IO.FileSystemWatcher.Renamed" /> event of a <see cref="T:System.IO.FileSystemWatcher" /> class.</summary>
	/// <param name="sender">The source of the event. </param>
	/// <param name="e">The <see cref="T:System.IO.RenamedEventArgs" /> that contains the event data. </param>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020003E7 RID: 999
	// (Invoke) Token: 0x06001E5B RID: 7771
	public delegate void RenamedEventHandler(object sender, RenamedEventArgs e);
}
