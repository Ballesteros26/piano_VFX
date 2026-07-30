using System;

namespace System.Data.Common
{
	/// <summary>Indicates the position of the catalog name in a qualified table name in a text command. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000325 RID: 805
	public enum CatalogLocation
	{
		/// <summary>Indicates that the position of the catalog name occurs before the schema portion of a fully qualified table name in a text command.</summary>
		// Token: 0x040017DD RID: 6109
		Start = 1,
		/// <summary>Indicates that the position of the catalog name occurs after the schema portion of a fully qualified table name in a text command.</summary>
		// Token: 0x040017DE RID: 6110
		End
	}
}
