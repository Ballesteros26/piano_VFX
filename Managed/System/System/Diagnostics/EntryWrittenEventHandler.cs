using System;

namespace System.Diagnostics
{
	/// <summary>Represents the method that will handle the <see cref="E:System.Diagnostics.EventLog.EntryWritten" /> event of an <see cref="T:System.Diagnostics.EventLog" />.</summary>
	/// <param name="sender">The source of the event. </param>
	/// <param name="e">An <see cref="T:System.Diagnostics.EntryWrittenEventArgs" /> that contains the event data. </param>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001EE RID: 494
	// (Invoke) Token: 0x06000F99 RID: 3993
	public delegate void EntryWrittenEventHandler(object sender, EntryWrittenEventArgs e);
}
