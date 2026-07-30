using System;

namespace System.Xml.Serialization
{
	/// <summary>Represents the method that handles the <see cref="E:System.Xml.Serialization.XmlSerializer.UnknownElement" /> event of an <see cref="T:System.Xml.Serialization.XmlSerializer" />.</summary>
	/// <param name="sender">The source of the event. </param>
	/// <param name="e">A <see cref="T:System.Xml.Serialization.XmlElementEventArgs" />  that contains the event data. </param>
	// Token: 0x0200036E RID: 878
	// (Invoke) Token: 0x060023F0 RID: 9200
	public delegate void XmlElementEventHandler(object sender, XmlElementEventArgs e);
}
