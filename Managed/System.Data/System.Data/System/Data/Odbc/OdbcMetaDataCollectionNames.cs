using System;

namespace System.Data.Odbc
{
	/// <summary>Provides a list of constants for use with the GetSchema method to retrieve metadata collections.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020002A5 RID: 677
	public static class OdbcMetaDataCollectionNames
	{
		/// <summary>A constant for use with the GetSchema method that represents the Columns collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04001546 RID: 5446
		public static readonly string Columns = "Columns";

		/// <summary>A constant for use with the GetSchema method that represents the Indexes collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04001547 RID: 5447
		public static readonly string Indexes = "Indexes";

		/// <summary>A constant for use with the GetSchema method that represents the Procedures collection. </summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04001548 RID: 5448
		public static readonly string Procedures = "Procedures";

		/// <summary>A constant for use with the GetSchema method that represents the ProcedureColumns collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04001549 RID: 5449
		public static readonly string ProcedureColumns = "ProcedureColumns";

		/// <summary>A constant for use with the GetSchema method that represents the ProcedureParameters collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0400154A RID: 5450
		public static readonly string ProcedureParameters = "ProcedureParameters";

		/// <summary>A constant for use with the GetSchema method that represents the Tables collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0400154B RID: 5451
		public static readonly string Tables = "Tables";

		/// <summary>A constant for use with the GetSchema method that represents the Views collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0400154C RID: 5452
		public static readonly string Views = "Views";
	}
}
