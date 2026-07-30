using System;

namespace System.Data
{
	/// <summary>Represents the method that will handle the <see cref="E:System.Data.Common.DataAdapter.FillError" /> event.</summary>
	/// <param name="sender">The source of the event. </param>
	/// <param name="e">The <see cref="T:System.Data.FillErrorEventArgs" /> that contains the event data. </param>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000A5 RID: 165
	// (Invoke) Token: 0x06000A12 RID: 2578
	public delegate void FillErrorEventHandler(object sender, FillErrorEventArgs e);
}
