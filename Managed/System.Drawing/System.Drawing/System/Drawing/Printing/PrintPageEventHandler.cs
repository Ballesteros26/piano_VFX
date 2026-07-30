using System;

namespace System.Drawing.Printing
{
	/// <summary>Represents the method that will handle the <see cref="E:System.Drawing.Printing.PrintDocument.PrintPage" /> event of a <see cref="T:System.Drawing.Printing.PrintDocument" />.</summary>
	/// <param name="sender">The source of the event. </param>
	/// <param name="e">A <see cref="T:System.Drawing.Printing.PrintPageEventArgs" /> that contains the event data. </param>
	// Token: 0x020000BD RID: 189
	// (Invoke) Token: 0x06000A7F RID: 2687
	public delegate void PrintPageEventHandler(object sender, PrintPageEventArgs e);
}
