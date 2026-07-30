using System;

namespace System.Data.SqlClient
{
	/// <summary>Represents the method that will handle the <see cref="E:System.Data.SqlClient.SqlConnection.InfoMessage" /> event of a <see cref="T:System.Data.SqlClient.SqlConnection" />.</summary>
	/// <param name="sender">The source of the event. </param>
	/// <param name="e">A <see cref="T:System.Data.SqlClient.SqlInfoMessageEventArgs" /> object that contains the event data. </param>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001C3 RID: 451
	// (Invoke) Token: 0x060014F3 RID: 5363
	public delegate void SqlInfoMessageEventHandler(object sender, SqlInfoMessageEventArgs e);
}
