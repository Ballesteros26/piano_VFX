using System;

namespace System.Data
{
	/// <summary>Represents the method that will handle the <see cref="E:System.Data.DataSet.MergeFailed" /> event.</summary>
	/// <param name="sender">The source of the event.</param>
	/// <param name="e">The data for the event.</param>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000D5 RID: 213
	// (Invoke) Token: 0x06000BD0 RID: 3024
	public delegate void MergeFailedEventHandler(object sender, MergeFailedEventArgs e);
}
