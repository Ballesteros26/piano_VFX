using System;

namespace System.Data
{
	/// <summary>Determines the serialization format for a <see cref="T:System.Data.DataSet" />.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000087 RID: 135
	public enum SerializationFormat
	{
		/// <summary>Serialize as XML content. The default.</summary>
		// Token: 0x040005AB RID: 1451
		Xml,
		/// <summary>Serialize as binary content. Available in ADO.NET 2.0 only.</summary>
		// Token: 0x040005AC RID: 1452
		Binary
	}
}
