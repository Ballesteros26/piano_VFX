using System;

namespace System.Data
{
	/// <summary>The delegate type for the event handlers of the <see cref="E:System.Data.SqlClient.SqlCommand.StatementCompleted" /> event.</summary>
	/// <param name="sender">The source of the event.</param>
	/// <param name="e">The data for the event.</param>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000FA RID: 250
	// (Invoke) Token: 0x06000CF6 RID: 3318
	public delegate void StatementCompletedEventHandler(object sender, StatementCompletedEventArgs e);
}
