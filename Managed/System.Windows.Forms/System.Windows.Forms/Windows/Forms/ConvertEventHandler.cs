using System;

namespace System.Windows.Forms
{
	/// <summary>Represents the method that will handle the <see cref="E:System.Windows.Forms.Binding.Parse" /> and <see cref="E:System.Windows.Forms.Binding.Format" /> events of a <see cref="T:System.Windows.Forms.Binding" />.</summary>
	/// <param name="sender">The source of the event. </param>
	/// <param name="e">A <see cref="T:System.Windows.Forms.ConvertEventArgs" /> that contains the event data. </param>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200065E RID: 1630
	// (Invoke) Token: 0x0600512A RID: 20778
	public delegate void ConvertEventHandler(object sender, ConvertEventArgs e);
}
