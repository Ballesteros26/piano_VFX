using System;

namespace System.Data
{
	/// <summary>Indicates the schema serialization mode for a typed <see cref="T:System.Data.DataSet" />.</summary>
	// Token: 0x020000E9 RID: 233
	public enum SchemaSerializationMode
	{
		/// <summary>Includes schema serialization for a typed <see cref="T:System.Data.DataSet" />. The default.</summary>
		// Token: 0x0400083F RID: 2111
		IncludeSchema = 1,
		/// <summary>Skips schema serialization for a typed <see cref="T:System.Data.DataSet" />.</summary>
		// Token: 0x04000840 RID: 2112
		ExcludeSchema
	}
}
