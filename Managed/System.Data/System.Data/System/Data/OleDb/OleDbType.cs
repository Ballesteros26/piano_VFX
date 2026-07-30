using System;

namespace System.Data.OleDb
{
	/// <summary>Specifies the data type of a field, a property, for use in an <see cref="T:System.Data.OleDb.OleDbParameter" />.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000121 RID: 289
	public enum OleDbType
	{
		/// <summary>A 64-bit signed integer (DBTYPE_I8). This maps to <see cref="T:System.Int64" />.</summary>
		// Token: 0x04000A04 RID: 2564
		BigInt = 20,
		/// <summary>A stream of binary data (DBTYPE_BYTES). This maps to an <see cref="T:System.Array" /> of type <see cref="T:System.Byte" />.</summary>
		// Token: 0x04000A05 RID: 2565
		Binary = 128,
		/// <summary>A Boolean value (DBTYPE_BOOL). This maps to <see cref="T:System.Boolean" />.</summary>
		// Token: 0x04000A06 RID: 2566
		Boolean = 11,
		/// <summary>A null-terminated character string of Unicode characters (DBTYPE_BSTR). This maps to <see cref="T:System.String" />.</summary>
		// Token: 0x04000A07 RID: 2567
		BSTR = 8,
		/// <summary>A character string (DBTYPE_STR). This maps to <see cref="T:System.String" />.</summary>
		// Token: 0x04000A08 RID: 2568
		Char = 129,
		/// <summary>A currency value ranging from -2 63 (or -922,337,203,685,477.5808) to 2 63 -1 (or +922,337,203,685,477.5807) with an accuracy to a ten-thousandth of a currency unit (DBTYPE_CY). This maps to <see cref="T:System.Decimal" />.</summary>
		// Token: 0x04000A09 RID: 2569
		Currency = 6,
		/// <summary>Date data, stored as a double (DBTYPE_DATE). The whole portion is the number of days since December 30, 1899, and the fractional portion is a fraction of a day. This maps to <see cref="T:System.DateTime" />.</summary>
		// Token: 0x04000A0A RID: 2570
		Date,
		/// <summary>Date data in the format yyyymmdd (DBTYPE_DBDATE). This maps to <see cref="T:System.DateTime" />.</summary>
		// Token: 0x04000A0B RID: 2571
		DBDate = 133,
		/// <summary>Time data in the format hhmmss (DBTYPE_DBTIME). This maps to <see cref="T:System.TimeSpan" />.</summary>
		// Token: 0x04000A0C RID: 2572
		DBTime,
		/// <summary>Data and time data in the format yyyymmddhhmmss (DBTYPE_DBTIMESTAMP). This maps to <see cref="T:System.DateTime" />.</summary>
		// Token: 0x04000A0D RID: 2573
		DBTimeStamp,
		/// <summary>A fixed precision and scale numeric value between -10 38 -1 and 10 38 -1 (DBTYPE_DECIMAL). This maps to <see cref="T:System.Decimal" />.</summary>
		// Token: 0x04000A0E RID: 2574
		Decimal = 14,
		/// <summary>A floating-point number within the range of -1.79E +308 through 1.79E +308 (DBTYPE_R8). This maps to <see cref="T:System.Double" />.</summary>
		// Token: 0x04000A0F RID: 2575
		Double = 5,
		/// <summary>No value (DBTYPE_EMPTY).</summary>
		// Token: 0x04000A10 RID: 2576
		Empty = 0,
		/// <summary>A 32-bit error code (DBTYPE_ERROR). This maps to <see cref="T:System.Exception" />.</summary>
		// Token: 0x04000A11 RID: 2577
		Error = 10,
		/// <summary>A 64-bit unsigned integer representing the number of 100-nanosecond intervals since January 1, 1601 (DBTYPE_FILETIME). This maps to <see cref="T:System.DateTime" />.</summary>
		// Token: 0x04000A12 RID: 2578
		Filetime = 64,
		/// <summary>A globally unique identifier (or GUID) (DBTYPE_GUID). This maps to <see cref="T:System.Guid" />.</summary>
		// Token: 0x04000A13 RID: 2579
		Guid = 72,
		/// <summary>A pointer to an IDispatch interface (DBTYPE_IDISPATCH). This maps to <see cref="T:System.Object" />.</summary>
		// Token: 0x04000A14 RID: 2580
		IDispatch = 9,
		/// <summary>A 32-bit signed integer (DBTYPE_I4). This maps to <see cref="T:System.Int32" />.</summary>
		// Token: 0x04000A15 RID: 2581
		Integer = 3,
		/// <summary>A pointer to an IUnknown interface (DBTYPE_UNKNOWN). This maps to <see cref="T:System.Object" />.</summary>
		// Token: 0x04000A16 RID: 2582
		IUnknown = 13,
		/// <summary>A long binary value (<see cref="T:System.Data.OleDb.OleDbParameter" /> only). This maps to an <see cref="T:System.Array" /> of type <see cref="T:System.Byte" />.</summary>
		// Token: 0x04000A17 RID: 2583
		LongVarBinary = 205,
		/// <summary>A long string value (<see cref="T:System.Data.OleDb.OleDbParameter" /> only). This maps to <see cref="T:System.String" />.</summary>
		// Token: 0x04000A18 RID: 2584
		LongVarChar = 201,
		/// <summary>A long null-terminated Unicode string value (<see cref="T:System.Data.OleDb.OleDbParameter" /> only). This maps to <see cref="T:System.String" />.</summary>
		// Token: 0x04000A19 RID: 2585
		LongVarWChar = 203,
		/// <summary>An exact numeric value with a fixed precision and scale (DBTYPE_NUMERIC). This maps to <see cref="T:System.Decimal" />.</summary>
		// Token: 0x04000A1A RID: 2586
		Numeric = 131,
		/// <summary>An automation PROPVARIANT (DBTYPE_PROP_VARIANT). This maps to <see cref="T:System.Object" />.</summary>
		// Token: 0x04000A1B RID: 2587
		PropVariant = 138,
		/// <summary>A floating-point number within the range of -3.40E +38 through 3.40E +38 (DBTYPE_R4). This maps to <see cref="T:System.Single" />.</summary>
		// Token: 0x04000A1C RID: 2588
		Single = 4,
		/// <summary>A 16-bit signed integer (DBTYPE_I2). This maps to <see cref="T:System.Int16" />.</summary>
		// Token: 0x04000A1D RID: 2589
		SmallInt = 2,
		/// <summary>A 8-bit signed integer (DBTYPE_I1). This maps to <see cref="T:System.SByte" />.</summary>
		// Token: 0x04000A1E RID: 2590
		TinyInt = 16,
		/// <summary>A 64-bit unsigned integer (DBTYPE_UI8). This maps to <see cref="T:System.UInt64" />.</summary>
		// Token: 0x04000A1F RID: 2591
		UnsignedBigInt = 21,
		/// <summary>A 32-bit unsigned integer (DBTYPE_UI4). This maps to <see cref="T:System.UInt32" />.</summary>
		// Token: 0x04000A20 RID: 2592
		UnsignedInt = 19,
		/// <summary>A 16-bit unsigned integer (DBTYPE_UI2). This maps to <see cref="T:System.UInt16" />.</summary>
		// Token: 0x04000A21 RID: 2593
		UnsignedSmallInt = 18,
		/// <summary>A 8-bit unsigned integer (DBTYPE_UI1). This maps to <see cref="T:System.Byte" />.</summary>
		// Token: 0x04000A22 RID: 2594
		UnsignedTinyInt = 17,
		/// <summary>A variable-length stream of binary data (<see cref="T:System.Data.OleDb.OleDbParameter" /> only). This maps to an <see cref="T:System.Array" /> of type <see cref="T:System.Byte" />.</summary>
		// Token: 0x04000A23 RID: 2595
		VarBinary = 204,
		/// <summary>A variable-length stream of non-Unicode characters (<see cref="T:System.Data.OleDb.OleDbParameter" /> only). This maps to <see cref="T:System.String" />.</summary>
		// Token: 0x04000A24 RID: 2596
		VarChar = 200,
		/// <summary>A special data type that can contain numeric, string, binary, or date data, and also the special values Empty and Null (DBTYPE_VARIANT). This type is assumed if no other is specified. This maps to <see cref="T:System.Object" />.</summary>
		// Token: 0x04000A25 RID: 2597
		Variant = 12,
		/// <summary>A variable-length numeric value (<see cref="T:System.Data.OleDb.OleDbParameter" /> only). This maps to <see cref="T:System.Decimal" />.</summary>
		// Token: 0x04000A26 RID: 2598
		VarNumeric = 139,
		/// <summary>A variable-length, null-terminated stream of Unicode characters (<see cref="T:System.Data.OleDb.OleDbParameter" /> only). This maps to <see cref="T:System.String" />.</summary>
		// Token: 0x04000A27 RID: 2599
		VarWChar = 202,
		/// <summary>A null-terminated stream of Unicode characters (DBTYPE_WSTR). This maps to <see cref="T:System.String" />. </summary>
		// Token: 0x04000A28 RID: 2600
		WChar = 130
	}
}
