using System;

namespace System.Data.Odbc
{
	/// <summary>Represents the method that will handle the <see cref="E:System.Data.Odbc.OdbcConnection.InfoMessage" /> event of an <see cref="T:System.Data.Odbc.OdbcConnection" />.</summary>
	/// <param name="sender">The source of the event. </param>
	/// <param name="e">An <see cref="T:System.Data.Odbc.OdbcInfoMessageEventArgs" /> object that contains the event data. </param>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020002A3 RID: 675
	// (Invoke) Token: 0x06001CAC RID: 7340
	public delegate void OdbcInfoMessageEventHandler(object sender, OdbcInfoMessageEventArgs e);
}
