using System;

namespace System.Xml.Serialization
{
	/// <summary>Delegate used by the <see cref="T:System.Xml.Serialization.XmlSerializer" /> class for deserialization of SOAP-encoded XML data. </summary>
	/// <param name="fixup">An instance of the <see cref="T:System.Xml.Serialization.XmlSerializationReader.Fixup" /> class that contains the object to be fixed and the array of string identifiers for the items to fill in.</param>
	// Token: 0x0200034F RID: 847
	// (Invoke) Token: 0x0600215B RID: 8539
	public delegate void XmlSerializationFixupCallback(object fixup);
}
