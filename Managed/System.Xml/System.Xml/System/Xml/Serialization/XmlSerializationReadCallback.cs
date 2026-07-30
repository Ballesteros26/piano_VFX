using System;

namespace System.Xml.Serialization
{
	/// <summary>Delegate used by the <see cref="T:System.Xml.Serialization.XmlSerializer" /> class for deserialization of types from SOAP-encoded, non-root XML data. </summary>
	/// <returns>The object returned by the callback.</returns>
	// Token: 0x02000351 RID: 849
	// (Invoke) Token: 0x06002163 RID: 8547
	public delegate object XmlSerializationReadCallback();
}
