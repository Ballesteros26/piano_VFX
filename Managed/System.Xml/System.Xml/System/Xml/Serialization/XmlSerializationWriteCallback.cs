using System;

namespace System.Xml.Serialization
{
	/// <summary>Delegate that is used by the <see cref="T:System.Xml.Serialization.XmlSerializer" /> class for serialization of types from SOAP-encoded, non-root XML data. </summary>
	/// <param name="o">The object being serialized.</param>
	// Token: 0x0200035A RID: 858
	// (Invoke) Token: 0x0600229E RID: 8862
	public delegate void XmlSerializationWriteCallback(object o);
}
