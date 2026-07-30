using System;

namespace System.Data
{
	/// <summary>Used by the Visual Basic .NET Data Designers to represent a parameter to a Command object, and optionally, its mapping to <see cref="T:System.Data.DataSet" /> columns.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000CC RID: 204
	public interface IDbDataParameter : IDataParameter
	{
		/// <summary>Indicates the precision of numeric parameters.</summary>
		/// <returns>The maximum number of digits used to represent the Value property of a data provider Parameter object. The default value is 0, which indicates that a data provider sets the precision for Value.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000217 RID: 535
		// (get) Token: 0x06000BB6 RID: 2998
		// (set) Token: 0x06000BB7 RID: 2999
		byte Precision { get; set; }

		/// <summary>Indicates the scale of numeric parameters.</summary>
		/// <returns>The number of decimal places to which <see cref="T:System.Data.OleDb.OleDbParameter.Value" /> is resolved. The default is 0.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000218 RID: 536
		// (get) Token: 0x06000BB8 RID: 3000
		// (set) Token: 0x06000BB9 RID: 3001
		byte Scale { get; set; }

		/// <summary>The size of the parameter.</summary>
		/// <returns>The maximum size, in bytes, of the data within the column. The default value is inferred from the the parameter value.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000219 RID: 537
		// (get) Token: 0x06000BBA RID: 3002
		// (set) Token: 0x06000BBB RID: 3003
		int Size { get; set; }
	}
}
