using System;

namespace System.Data
{
	/// <summary>Represents a parameter to a Command object, and optionally, its mapping to <see cref="T:System.Data.DataSet" /> columns; and is implemented by .NET Framework data providers that access data sources.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000C5 RID: 197
	public interface IDataParameter
	{
		/// <summary>Gets or sets the <see cref="T:System.Data.DbType" /> of the parameter.</summary>
		/// <returns>One of the <see cref="T:System.Data.DbType" /> values. The default is <see cref="F:System.Data.DbType.String" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The property was not set to a valid <see cref="T:System.Data.DbType" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001FA RID: 506
		// (get) Token: 0x06000B5D RID: 2909
		// (set) Token: 0x06000B5E RID: 2910
		DbType DbType { get; set; }

		/// <summary>Gets or sets a value indicating whether the parameter is input-only, output-only, bidirectional, or a stored procedure return value parameter.</summary>
		/// <returns>One of the <see cref="T:System.Data.ParameterDirection" /> values. The default is Input.</returns>
		/// <exception cref="T:System.ArgumentException">The property was not set to one of the valid <see cref="T:System.Data.ParameterDirection" /> values. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001FB RID: 507
		// (get) Token: 0x06000B5F RID: 2911
		// (set) Token: 0x06000B60 RID: 2912
		ParameterDirection Direction { get; set; }

		/// <summary>Gets a value indicating whether the parameter accepts null values.</summary>
		/// <returns>true if null values are accepted; otherwise, false. The default is false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001FC RID: 508
		// (get) Token: 0x06000B61 RID: 2913
		bool IsNullable { get; }

		/// <summary>Gets or sets the name of the <see cref="T:System.Data.IDataParameter" />.</summary>
		/// <returns>The name of the <see cref="T:System.Data.IDataParameter" />. The default is an empty string.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001FD RID: 509
		// (get) Token: 0x06000B62 RID: 2914
		// (set) Token: 0x06000B63 RID: 2915
		string ParameterName { get; set; }

		/// <summary>Gets or sets the name of the source column that is mapped to the <see cref="T:System.Data.DataSet" /> and used for loading or returning the <see cref="P:System.Data.IDataParameter.Value" />.</summary>
		/// <returns>The name of the source column that is mapped to the <see cref="T:System.Data.DataSet" />. The default is an empty string.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001FE RID: 510
		// (get) Token: 0x06000B64 RID: 2916
		// (set) Token: 0x06000B65 RID: 2917
		string SourceColumn { get; set; }

		/// <summary>Gets or sets the <see cref="T:System.Data.DataRowVersion" /> to use when loading <see cref="P:System.Data.IDataParameter.Value" />.</summary>
		/// <returns>One of the <see cref="T:System.Data.DataRowVersion" /> values. The default is Current.</returns>
		/// <exception cref="T:System.ArgumentException">The property was not set one of the <see cref="T:System.Data.DataRowVersion" /> values. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001FF RID: 511
		// (get) Token: 0x06000B66 RID: 2918
		// (set) Token: 0x06000B67 RID: 2919
		DataRowVersion SourceVersion { get; set; }

		/// <summary>Gets or sets the value of the parameter.</summary>
		/// <returns>An <see cref="T:System.Object" /> that is the value of the parameter. The default value is null.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000200 RID: 512
		// (get) Token: 0x06000B68 RID: 2920
		// (set) Token: 0x06000B69 RID: 2921
		object Value { get; set; }
	}
}
