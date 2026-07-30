using System;

namespace System.Web.UI.Design
{
	/// <summary>Provides basic functionality for describing the structure of a data field at design time.</summary>
	// Token: 0x02000088 RID: 136
	public interface IDataSourceFieldSchema
	{
		/// <summary>Gets the type of data stored in the field.</summary>
		/// <returns>A <see cref="T:System.Type" /> object.</returns>
		// Token: 0x17000100 RID: 256
		// (get) Token: 0x0600044C RID: 1100
		Type DataType { get; }

		/// <summary>Gets a value indicating whether the value of the field automatically increments for each new row.</summary>
		/// <returns>true if the field's <see cref="P:System.Web.UI.Design.IDataSourceFieldSchema.DataType" /> is numeric and the underlying field increments automatically as new rows are added; otherwise, false.</returns>
		// Token: 0x17000101 RID: 257
		// (get) Token: 0x0600044D RID: 1101
		bool Identity { get; }

		/// <summary>Gets a value indicating whether the field is editable.</summary>
		/// <returns>true if the field is read-only; otherwise, false.</returns>
		// Token: 0x17000102 RID: 258
		// (get) Token: 0x0600044E RID: 1102
		bool IsReadOnly { get; }

		/// <summary>Gets a value indicating whether values in the field are required to be unique.</summary>
		/// <returns>true if data in the field must be unique; otherwise, false.</returns>
		// Token: 0x17000103 RID: 259
		// (get) Token: 0x0600044F RID: 1103
		bool IsUnique { get; }

		/// <summary>Gets a value indicting the size of data that can be stored in the field.</summary>
		/// <returns>The number of bytes the field can store.</returns>
		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000450 RID: 1104
		int Length { get; }

		/// <summary>Gets the name of the field.</summary>
		/// <returns>The name of the field.</returns>
		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000451 RID: 1105
		string Name { get; }

		/// <summary>Gets a value indicating whether the field can accept null values.</summary>
		/// <returns>true if the field can accept null values; otherwise, false.</returns>
		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000452 RID: 1106
		bool Nullable { get; }

		/// <summary>Gets the maximum number of digits used to represent a numerical value in the field.</summary>
		/// <returns>The maximum number of digits used to represent the values of the field if the <see cref="P:System.Web.UI.Design.IDataSourceFieldSchema.DataType" /> property of the field represents a numeric type. If this property is not implemented, it should return -1.</returns>
		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000453 RID: 1107
		int Precision { get; }

		/// <summary>Gets a value indicating whether the field is in the primary key.</summary>
		/// <returns>true if the field is in the primary key; otherwise, false.</returns>
		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000454 RID: 1108
		bool PrimaryKey { get; }

		/// <summary>Gets the number of decimal places to which numerical values in the field are resolved.</summary>
		/// <returns>If the <see cref="P:System.Web.UI.Design.IDataSourceFieldSchema.DataType" /> property of the field represents a numeric type, returns the number of decimal places to which values are resolved, otherwise -1.</returns>
		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06000455 RID: 1109
		int Scale { get; }
	}
}
