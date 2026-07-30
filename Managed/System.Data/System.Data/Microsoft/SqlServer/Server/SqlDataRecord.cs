using System;
using System.Data;
using System.Data.Common;
using System.Data.ProviderBase;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace Microsoft.SqlServer.Server
{
	/// <summary>Represents a single row of data and its metadata. This class cannot be inherited.</summary>
	// Token: 0x020003AD RID: 941
	public class SqlDataRecord : IDataRecord
	{
		/// <summary>Gets the number of columns in the data row. This property is read-only.</summary>
		/// <returns>The number of columns in the data row as an integer.</returns>
		// Token: 0x17000751 RID: 1873
		// (get) Token: 0x06002C61 RID: 11361 RVA: 0x000C0AF4 File Offset: 0x000BECF4
		public virtual int FieldCount
		{
			get
			{
				this.EnsureSubclassOverride();
				return this._columnMetaData.Length;
			}
		}

		/// <summary>Returns the name of the column specified by the ordinal argument.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the column name.</returns>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <exception cref="T:System.IndexOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />). </exception>
		/// <exception cref="T:System.InvalidCastException">There is a type mismatch.</exception>
		// Token: 0x06002C62 RID: 11362 RVA: 0x000C0B04 File Offset: 0x000BED04
		public virtual string GetName(int ordinal)
		{
			this.EnsureSubclassOverride();
			return this.GetSqlMetaData(ordinal).Name;
		}

		/// <summary>Returns the name of the data type for the column specified by the ordinal argument.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the data type of the column.</returns>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />). </exception>
		/// <exception cref="T:System.InvalidCastException">There is a type mismatch.</exception>
		// Token: 0x06002C63 RID: 11363 RVA: 0x000C0B18 File Offset: 0x000BED18
		public virtual string GetDataTypeName(int ordinal)
		{
			this.EnsureSubclassOverride();
			SqlMetaData sqlMetaData = this.GetSqlMetaData(ordinal);
			if (SqlDbType.Udt == sqlMetaData.SqlDbType)
			{
				return null;
			}
			return MetaType.GetMetaTypeFromSqlDbType(sqlMetaData.SqlDbType, false).TypeName;
		}

		/// <summary>Returns a <see cref="T:System.Type" /> object representing the common language runtime (CLR) type that maps to the SQL Server type of the column specified by the <paramref name="ordinal" /> argument.</summary>
		/// <returns>The column type as a <see cref="T:System.Type" /> object.</returns>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <exception cref="T:System.IndexOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />). </exception>
		/// <exception cref="T:System.TypeLoadException">The column is of a user-defined type that is not available to the calling application domain.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The column is of a user-defined type that is not available to the calling application domain.</exception>
		/// <exception cref="T:System.InvalidCastException">There is a type mismatch.</exception>
		// Token: 0x06002C64 RID: 11364 RVA: 0x000C0B50 File Offset: 0x000BED50
		public virtual Type GetFieldType(int ordinal)
		{
			this.EnsureSubclassOverride();
			return MetaType.GetMetaTypeFromSqlDbType(this.GetSqlMetaData(ordinal).SqlDbType, false).ClassType;
		}

		/// <summary>Returns the common language runtime (CLR) type value for the column specified by the ordinal argument.</summary>
		/// <returns>The CLR type value of the column specified by the ordinal.</returns>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <exception cref="T:System.IndexOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />).</exception>
		/// <exception cref="T:System.InvalidCastException">There is a type mismatch.</exception>
		// Token: 0x06002C65 RID: 11365 RVA: 0x000C0B70 File Offset: 0x000BED70
		public virtual object GetValue(int ordinal)
		{
			this.EnsureSubclassOverride();
			SmiMetaData smiMetaData = this.GetSmiMetaData(ordinal);
			return ValueUtilsSmi.GetValue200(this._eventSink, this._recordBuffer, ordinal, smiMetaData);
		}

		/// <summary>Returns the values for all the columns in the record, expressed as common language runtime (CLR) types, in an array.</summary>
		/// <returns>An <see cref="T:System.Int32" /> that indicates the number of columns copied.</returns>
		/// <param name="values">The array into which to copy the values column values.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="values" /> is null.</exception>
		/// <exception cref="T:System.InvalidCastException">There is a type mismatch.</exception>
		// Token: 0x06002C66 RID: 11366 RVA: 0x000C0BA0 File Offset: 0x000BEDA0
		public virtual int GetValues(object[] values)
		{
			this.EnsureSubclassOverride();
			if (values == null)
			{
				throw ADP.ArgumentNull("values");
			}
			int num = ((values.Length < this.FieldCount) ? values.Length : this.FieldCount);
			for (int i = 0; i < num; i++)
			{
				values[i] = this.GetValue(i);
			}
			return num;
		}

		/// <summary>Returns the column ordinal specified by the column name.</summary>
		/// <returns>The zero-based ordinal of the column as an integer.</returns>
		/// <param name="name">The name of the column to look up.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null.</exception>
		/// <exception cref="T:System.IndexOutOfRangeException">The column name could not be found.</exception>
		/// <exception cref="T:System.InvalidCastException">There is a type mismatch.</exception>
		// Token: 0x06002C67 RID: 11367 RVA: 0x000C0BF0 File Offset: 0x000BEDF0
		public virtual int GetOrdinal(string name)
		{
			this.EnsureSubclassOverride();
			if (this._fieldNameLookup == null)
			{
				string[] array = new string[this.FieldCount];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = this.GetSqlMetaData(i).Name;
				}
				this._fieldNameLookup = new FieldNameLookup(array, -1);
			}
			return this._fieldNameLookup.GetOrdinal(name);
		}

		/// <summary>Gets the common language runtime (CLR) type value for the column specified by the column <paramref name="ordinal" /> argument.</summary>
		/// <returns>The CLR type value of the column specified by the <paramref name="ordinal" />.</returns>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <exception cref="T:System.IndexOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />).</exception>
		// Token: 0x17000752 RID: 1874
		public virtual object this[int ordinal]
		{
			get
			{
				this.EnsureSubclassOverride();
				return this.GetValue(ordinal);
			}
		}

		/// <summary>Gets the common language runtime (CLR) type value for the column specified by the column <paramref name="name" /> argument.</summary>
		/// <returns>The CLR type value of the column specified by the <paramref name="name" />.</returns>
		/// <param name="name">The name of the column.</param>
		// Token: 0x17000753 RID: 1875
		public virtual object this[string name]
		{
			get
			{
				this.EnsureSubclassOverride();
				return this.GetValue(this.GetOrdinal(name));
			}
		}

		/// <summary>Gets the value for the column specified by the ordinal as a <see cref="T:System.Boolean" />.</summary>
		/// <returns>The column value as a <see cref="T:System.Boolean" />.</returns>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <exception cref="T:System.IndexOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />).</exception>
		/// <exception cref="T:System.Data.SqlTypes.SqlNullValueException">The column specified by <paramref name="ordinal" /> is null.</exception>
		/// <exception cref="T:System.InvalidCastException">There is a type mismatch.</exception>
		// Token: 0x06002C6A RID: 11370 RVA: 0x000C0C71 File Offset: 0x000BEE71
		public virtual bool GetBoolean(int ordinal)
		{
			this.EnsureSubclassOverride();
			return ValueUtilsSmi.GetBoolean(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal));
		}

		/// <summary>Gets the value for the column specified by the ordinal as a <see cref="T:System.Byte" />.</summary>
		/// <returns>The column value as a <see cref="T:System.Byte" />.</returns>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <exception cref="T:System.IndexOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />).</exception>
		/// <exception cref="T:System.Data.SqlTypes.SqlNullValueException">The column specified by <paramref name="ordinal" /> is null.</exception>
		/// <exception cref="T:System.InvalidCastException">There is a type mismatch.</exception>
		// Token: 0x06002C6B RID: 11371 RVA: 0x000C0C92 File Offset: 0x000BEE92
		public virtual byte GetByte(int ordinal)
		{
			this.EnsureSubclassOverride();
			return ValueUtilsSmi.GetByte(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal));
		}

		/// <summary>Gets the value for the column specified by the ordinal as an array of <see cref="T:System.Byte" /> objects.</summary>
		/// <returns>The number of bytes copied.</returns>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <param name="fieldOffset">The offset into the field value to start retrieving bytes.</param>
		/// <param name="buffer">The target buffer to which to copy bytes.</param>
		/// <param name="bufferOffset">The offset into the buffer to which to start copying bytes.</param>
		/// <param name="length">The number of bytes to copy to the buffer.</param>
		/// <exception cref="T:System.IndexOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />).</exception>
		/// <exception cref="T:System.Data.SqlTypes.SqlNullValueException">The column specified by <paramref name="ordinal" /> is null.</exception>
		/// <exception cref="T:System.InvalidCastException">There is a type mismatch.</exception>
		// Token: 0x06002C6C RID: 11372 RVA: 0x000C0CB4 File Offset: 0x000BEEB4
		public virtual long GetBytes(int ordinal, long fieldOffset, byte[] buffer, int bufferOffset, int length)
		{
			this.EnsureSubclassOverride();
			return ValueUtilsSmi.GetBytes(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), fieldOffset, buffer, bufferOffset, length, true);
		}

		/// <summary>Gets the value for the column specified by the ordinal as a <see cref="T:System.Char" />.</summary>
		/// <returns>The column value as a <see cref="T:System.Char" />.</returns>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <exception cref="T:System.IndexOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />).</exception>
		/// <exception cref="T:System.Data.SqlTypes.SqlNullValueException">The column specified by <paramref name="ordinal" /> is null.</exception>
		/// <exception cref="T:System.InvalidCastException">There is a type mismatch.</exception>
		// Token: 0x06002C6D RID: 11373 RVA: 0x000C0CE7 File Offset: 0x000BEEE7
		public virtual char GetChar(int ordinal)
		{
			this.EnsureSubclassOverride();
			throw ADP.NotSupported();
		}

		/// <summary>Gets the value for the column specified by the ordinal as an array of <see cref="T:System.Char" /> objects.</summary>
		/// <returns>The number of characters copied.</returns>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <param name="fieldOffset">The offset into the field value to start retrieving characters.</param>
		/// <param name="buffer">The target buffer to copy chars to.</param>
		/// <param name="bufferOffset">The offset into the buffer to start copying chars to.</param>
		/// <param name="length">The number of chars to copy to the buffer.</param>
		/// <exception cref="T:System.IndexOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />).</exception>
		/// <exception cref="T:System.Data.SqlTypes.SqlNullValueException">The column specified by <paramref name="ordinal" /> is null.</exception>
		/// <exception cref="T:System.InvalidCastException">There is a type mismatch.</exception>
		// Token: 0x06002C6E RID: 11374 RVA: 0x000C0CF4 File Offset: 0x000BEEF4
		public virtual long GetChars(int ordinal, long fieldOffset, char[] buffer, int bufferOffset, int length)
		{
			this.EnsureSubclassOverride();
			return ValueUtilsSmi.GetChars(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), fieldOffset, buffer, bufferOffset, length);
		}

		/// <summary>Gets the value for the column specified by the ordinal as a <see cref="T:System.Guid" />.</summary>
		/// <returns>The column value as a <see cref="T:System.Guid" />.</returns>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <exception cref="T:System.IndexOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />).</exception>
		/// <exception cref="T:System.Data.SqlTypes.SqlNullValueException">The column specified by <paramref name="ordinal" /> is null.</exception>
		/// <exception cref="T:System.InvalidCastException">There is a type mismatch.</exception>
		// Token: 0x06002C6F RID: 11375 RVA: 0x000C0D1B File Offset: 0x000BEF1B
		public virtual Guid GetGuid(int ordinal)
		{
			this.EnsureSubclassOverride();
			return ValueUtilsSmi.GetGuid(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal));
		}

		/// <summary>Gets the value for the column specified by the ordinal as a <see cref="T:System.Int16" />.</summary>
		/// <returns>The column value as a <see cref="T:System.Int16" />.</returns>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <exception cref="T:System.IndexOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />).</exception>
		/// <exception cref="T:System.Data.SqlTypes.SqlNullValueException">The column specified by <paramref name="ordinal" /> is null.</exception>
		/// <exception cref="T:System.InvalidCastException">There is a type mismatch.</exception>
		// Token: 0x06002C70 RID: 11376 RVA: 0x000C0D3C File Offset: 0x000BEF3C
		public virtual short GetInt16(int ordinal)
		{
			this.EnsureSubclassOverride();
			return ValueUtilsSmi.GetInt16(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal));
		}

		/// <summary>Gets the value for the column specified by the ordinal as a <see cref="T:System.Int32" />.</summary>
		/// <returns>The column value as a <see cref="T:System.Int32" />.</returns>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <exception cref="T:System.IndexOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />).</exception>
		/// <exception cref="T:System.Data.SqlTypes.SqlNullValueException">The column specified by <paramref name="ordinal" /> is null.</exception>
		/// <exception cref="T:System.InvalidCastException">There is a type mismatch.</exception>
		// Token: 0x06002C71 RID: 11377 RVA: 0x000C0D5D File Offset: 0x000BEF5D
		public virtual int GetInt32(int ordinal)
		{
			this.EnsureSubclassOverride();
			return ValueUtilsSmi.GetInt32(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal));
		}

		/// <summary>Gets the value for the column specified by the ordinal as a <see cref="T:System.Int64" />.</summary>
		/// <returns>The column value as a <see cref="T:System.Int64" />.</returns>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <exception cref="T:System.IndexOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />).</exception>
		/// <exception cref="T:System.Data.SqlTypes.SqlNullValueException">The column specified by <paramref name="ordinal" /> is null.</exception>
		/// <exception cref="T:System.InvalidCastException">There is a type mismatch.</exception>
		// Token: 0x06002C72 RID: 11378 RVA: 0x000C0D7E File Offset: 0x000BEF7E
		public virtual long GetInt64(int ordinal)
		{
			this.EnsureSubclassOverride();
			return ValueUtilsSmi.GetInt64(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal));
		}

		/// <summary>Gets the value for the column specified by the ordinal as a float.</summary>
		/// <returns>The column value as a float.</returns>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <exception cref="T:System.IndexOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />).</exception>
		/// <exception cref="T:System.Data.SqlTypes.SqlNullValueException">The column specified by <paramref name="ordinal" /> is null.</exception>
		/// <exception cref="T:System.InvalidCastException">There is a type mismatch.</exception>
		// Token: 0x06002C73 RID: 11379 RVA: 0x000C0D9F File Offset: 0x000BEF9F
		public virtual float GetFloat(int ordinal)
		{
			this.EnsureSubclassOverride();
			return ValueUtilsSmi.GetSingle(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal));
		}

		/// <summary>Gets the value for the column specified by the ordinal as a <see cref="T:System.Double" />.</summary>
		/// <returns>The column value as a <see cref="T:System.Double" />.</returns>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <exception cref="T:System.IndexOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />).</exception>
		/// <exception cref="T:System.Data.SqlTypes.SqlNullValueException">The column specified by <paramref name="ordinal" /> is null.</exception>
		/// <exception cref="T:System.InvalidCastException">There is a type mismatch.</exception>
		// Token: 0x06002C74 RID: 11380 RVA: 0x000C0DC0 File Offset: 0x000BEFC0
		public virtual double GetDouble(int ordinal)
		{
			this.EnsureSubclassOverride();
			return ValueUtilsSmi.GetDouble(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal));
		}

		/// <summary>Gets the value for the column specified by the ordinal as a <see cref="T:System.String" />.</summary>
		/// <returns>The column value as a <see cref="T:System.String" />.</returns>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />).</exception>
		/// <exception cref="T:System.InvalidCastException">There is a type mismatch.</exception>
		// Token: 0x06002C75 RID: 11381 RVA: 0x000C0DE4 File Offset: 0x000BEFE4
		public virtual string GetString(int ordinal)
		{
			this.EnsureSubclassOverride();
			SmiMetaData smiMetaData = this.GetSmiMetaData(ordinal);
			if (this._usesStringStorageForXml && SqlDbType.Xml == smiMetaData.SqlDbType)
			{
				return ValueUtilsSmi.GetString(this._eventSink, this._recordBuffer, ordinal, SqlDataRecord.s_maxNVarCharForXml);
			}
			return ValueUtilsSmi.GetString(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal));
		}

		/// <summary>Gets the value for the column specified by the ordinal as a <see cref="T:System.Decimal" />.</summary>
		/// <returns>The column value as a <see cref="T:System.Decimal" />.</returns>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <exception cref="T:System.IndexOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />).</exception>
		/// <exception cref="T:System.Data.SqlTypes.SqlNullValueException">The column specified by <paramref name="ordinal" /> is null.</exception>
		/// <exception cref="T:System.InvalidCastException">There is a type mismatch.</exception>
		// Token: 0x06002C76 RID: 11382 RVA: 0x000C0E42 File Offset: 0x000BF042
		public virtual decimal GetDecimal(int ordinal)
		{
			this.EnsureSubclassOverride();
			return ValueUtilsSmi.GetDecimal(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal));
		}

		/// <summary>Gets the value for the column specified by the ordinal as a <see cref="T:System.DateTime" />.</summary>
		/// <returns>The column value as a <see cref="T:System.DateTime" />.</returns>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <exception cref="T:System.IndexOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />).</exception>
		/// <exception cref="T:System.Data.SqlTypes.SqlNullValueException">The column specified by <paramref name="ordinal" /> is null.</exception>
		/// <exception cref="T:System.InvalidCastException">There is a type mismatch.</exception>
		// Token: 0x06002C77 RID: 11383 RVA: 0x000C0E63 File Offset: 0x000BF063
		public virtual DateTime GetDateTime(int ordinal)
		{
			this.EnsureSubclassOverride();
			return ValueUtilsSmi.GetDateTime(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal));
		}

		/// <summary>Returns the specified column’s data as a <see cref="T:System.DateTimeOffset" />.</summary>
		/// <returns>The value of the specified column as a <see cref="T:System.DateTimeOffset" />.</returns>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		// Token: 0x06002C78 RID: 11384 RVA: 0x000C0E84 File Offset: 0x000BF084
		public virtual DateTimeOffset GetDateTimeOffset(int ordinal)
		{
			this.EnsureSubclassOverride();
			return ValueUtilsSmi.GetDateTimeOffset(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal));
		}

		/// <summary>Returns the specified column’s data as a <see cref="T:System.TimeSpan" />.</summary>
		/// <returns>The value of the specified column as a <see cref="T:System.TimeSpan" />.</returns>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		// Token: 0x06002C79 RID: 11385 RVA: 0x000C0EA5 File Offset: 0x000BF0A5
		public virtual TimeSpan GetTimeSpan(int ordinal)
		{
			this.EnsureSubclassOverride();
			return ValueUtilsSmi.GetTimeSpan(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal));
		}

		/// <summary>Returns true if the column specified by the column ordinal parameter is null.</summary>
		/// <returns>true if the column is null; false otherwise.</returns>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <exception cref="T:System.IndexOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />).</exception>
		// Token: 0x06002C7A RID: 11386 RVA: 0x000C0EC6 File Offset: 0x000BF0C6
		public virtual bool IsDBNull(int ordinal)
		{
			this.EnsureSubclassOverride();
			this.ThrowIfInvalidOrdinal(ordinal);
			return ValueUtilsSmi.IsDBNull(this._eventSink, this._recordBuffer, ordinal);
		}

		/// <summary>Returns a <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> object, describing the metadata of the column specified by the column ordinal.</summary>
		/// <returns>The column metadata as a <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> object.</returns>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <exception cref="T:System.IndexOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />).</exception>
		/// <exception cref="T:System.InvalidCastException">There is a type mismatch.</exception>
		// Token: 0x06002C7B RID: 11387 RVA: 0x000C0EE7 File Offset: 0x000BF0E7
		public virtual SqlMetaData GetSqlMetaData(int ordinal)
		{
			this.EnsureSubclassOverride();
			return this._columnMetaData[ordinal];
		}

		/// <summary>Returns a <see cref="T:System.Type" /> object that represents the type (as a SQL Server type, defined in <see cref="N:System.Data.SqlTypes" />) that maps to the SQL Server type of the column.</summary>
		/// <returns>The column type as a <see cref="T:System.Type" /> object.</returns>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <exception cref="T:System.IndexOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />). </exception>
		/// <exception cref="T:System.TypeLoadException">The column is of a user-defined type that is not available to the calling application domain.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The column is of a user-defined type that is not available to the calling application domain.</exception>
		/// <exception cref="T:System.InvalidCastException">There is a type mismatch.</exception>
		// Token: 0x06002C7C RID: 11388 RVA: 0x000C0EF7 File Offset: 0x000BF0F7
		public virtual Type GetSqlFieldType(int ordinal)
		{
			this.EnsureSubclassOverride();
			return MetaType.GetMetaTypeFromSqlDbType(this.GetSqlMetaData(ordinal).SqlDbType, false).SqlType;
		}

		/// <summary>Returns the data value stored in the column, expressed as a SQL Server type, specified by the column ordinal.</summary>
		/// <returns>The value of the column, expressed as a SQL Server type, as a <see cref="T:System.Object" />.</returns>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <exception cref="T:System.IndexOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />).</exception>
		/// <exception cref="T:System.InvalidCastException">There is a type mismatch.</exception>
		// Token: 0x06002C7D RID: 11389 RVA: 0x000C0F18 File Offset: 0x000BF118
		public virtual object GetSqlValue(int ordinal)
		{
			this.EnsureSubclassOverride();
			SmiMetaData smiMetaData = this.GetSmiMetaData(ordinal);
			return ValueUtilsSmi.GetSqlValue200(this._eventSink, this._recordBuffer, ordinal, smiMetaData);
		}

		/// <summary>Returns the values for all the columns in the record, expressed as SQL Server types, in an array.</summary>
		/// <returns>An <see cref="T:System.Int32" /> that indicates the number of columns copied.</returns>
		/// <param name="values">The array into which to copy the values column values. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="values" /> is null.</exception>
		/// <exception cref="T:System.InvalidCastException">There is a type mismatch.</exception>
		// Token: 0x06002C7E RID: 11390 RVA: 0x000C0F48 File Offset: 0x000BF148
		public virtual int GetSqlValues(object[] values)
		{
			this.EnsureSubclassOverride();
			if (values == null)
			{
				throw ADP.ArgumentNull("values");
			}
			int num = ((values.Length < this.FieldCount) ? values.Length : this.FieldCount);
			for (int i = 0; i < num; i++)
			{
				values[i] = this.GetSqlValue(i);
			}
			return num;
		}

		/// <summary>Gets the value for the column specified by the ordinal as a <see cref="T:System.Data.SqlTypes.SqlBinary" />.</summary>
		/// <returns>The column value as a <see cref="T:System.Data.SqlTypes.SqlBinary" />.</returns>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <exception cref="T:System.IndexOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />).</exception>
		/// <exception cref="T:System.InvalidCastException">There is a type mismatch.</exception>
		// Token: 0x06002C7F RID: 11391 RVA: 0x000C0F97 File Offset: 0x000BF197
		public virtual SqlBinary GetSqlBinary(int ordinal)
		{
			this.EnsureSubclassOverride();
			return ValueUtilsSmi.GetSqlBinary(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal));
		}

		/// <summary>Gets the value for the column specified by the ordinal as a <see cref="T:System.Data.SqlTypes.SqlBytes" />.</summary>
		/// <returns>The column value as a <see cref="T:System.Data.SqlTypes.SqlBytes" />.</returns>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <exception cref="T:System.IndexOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />).</exception>
		/// <exception cref="T:System.InvalidCastException">There is a type mismatch.</exception>
		// Token: 0x06002C80 RID: 11392 RVA: 0x000C0FB8 File Offset: 0x000BF1B8
		public virtual SqlBytes GetSqlBytes(int ordinal)
		{
			this.EnsureSubclassOverride();
			return ValueUtilsSmi.GetSqlBytes(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal));
		}

		/// <summary>Gets the value for the column specified by the ordinal as a <see cref="T:System.Data.SqlTypes.SqlXml" />.</summary>
		/// <returns>The column value as a <see cref="T:System.Data.SqlTypes.SqlXml" />.</returns>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />).</exception>
		/// <exception cref="T:System.InvalidCastException">There is a type mismatch.</exception>
		// Token: 0x06002C81 RID: 11393 RVA: 0x000C0FD9 File Offset: 0x000BF1D9
		public virtual SqlXml GetSqlXml(int ordinal)
		{
			this.EnsureSubclassOverride();
			return ValueUtilsSmi.GetSqlXml(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal));
		}

		/// <summary>Gets the value for the column specified by the ordinal as a <see cref="T:System.Data.SqlTypes.SqlBoolean" />.</summary>
		/// <returns>The column value as a <see cref="T:System.Data.SqlTypes.SqlBoolean" />.</returns>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <exception cref="T:System.IndexOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />).</exception>
		/// <exception cref="T:System.InvalidCastException">There is a type mismatch.</exception>
		// Token: 0x06002C82 RID: 11394 RVA: 0x000C0FFA File Offset: 0x000BF1FA
		public virtual SqlBoolean GetSqlBoolean(int ordinal)
		{
			this.EnsureSubclassOverride();
			return ValueUtilsSmi.GetSqlBoolean(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal));
		}

		/// <summary>Gets the value for the column specified by the ordinal as a <see cref="T:System.Data.SqlTypes.SqlByte" />.</summary>
		/// <returns>The column value as a <see cref="T:System.Data.SqlTypes.SqlByte" />.</returns>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <exception cref="T:System.IndexOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />).</exception>
		/// <exception cref="T:System.InvalidCastException">There is a type mismatch.</exception>
		// Token: 0x06002C83 RID: 11395 RVA: 0x000C101B File Offset: 0x000BF21B
		public virtual SqlByte GetSqlByte(int ordinal)
		{
			this.EnsureSubclassOverride();
			return ValueUtilsSmi.GetSqlByte(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal));
		}

		/// <summary>Gets the value for the column specified by the ordinal as a <see cref="T:System.Data.SqlTypes.SqlChars" />.</summary>
		/// <returns>The column value as a <see cref="T:System.Data.SqlTypes.SqlChars" />.</returns>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <exception cref="T:System.IndexOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />).</exception>
		/// <exception cref="T:System.InvalidCastException">There is a type mismatch.</exception>
		// Token: 0x06002C84 RID: 11396 RVA: 0x000C103C File Offset: 0x000BF23C
		public virtual SqlChars GetSqlChars(int ordinal)
		{
			this.EnsureSubclassOverride();
			return ValueUtilsSmi.GetSqlChars(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal));
		}

		/// <summary>Gets the value for the column specified by the ordinal as a <see cref="T:System.Data.SqlTypes.SqlInt16" />.</summary>
		/// <returns>The column value as a <see cref="T:System.Data.SqlTypes.SqlInt16" />.</returns>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <exception cref="T:System.IndexOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />).</exception>
		/// <exception cref="T:System.InvalidCastException">There is a type mismatch.</exception>
		// Token: 0x06002C85 RID: 11397 RVA: 0x000C105D File Offset: 0x000BF25D
		public virtual SqlInt16 GetSqlInt16(int ordinal)
		{
			this.EnsureSubclassOverride();
			return ValueUtilsSmi.GetSqlInt16(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal));
		}

		/// <summary>Gets the value for the column specified by the ordinal as a <see cref="T:System.Data.SqlTypes.SqlInt32" />.</summary>
		/// <returns>The column value as a <see cref="T:System.Data.SqlTypes.SqlInt32" />.</returns>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <exception cref="T:System.IndexOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />).</exception>
		/// <exception cref="T:System.InvalidCastException">There is a type mismatch.</exception>
		// Token: 0x06002C86 RID: 11398 RVA: 0x000C107E File Offset: 0x000BF27E
		public virtual SqlInt32 GetSqlInt32(int ordinal)
		{
			this.EnsureSubclassOverride();
			return ValueUtilsSmi.GetSqlInt32(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal));
		}

		/// <summary>Gets the value for the column specified by the ordinal as a <see cref="T:System.Data.SqlTypes.SqlInt64" />.</summary>
		/// <returns>The column value as a <see cref="T:System.Data.SqlTypes.SqlInt64" />.</returns>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <exception cref="T:System.IndexOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />).</exception>
		/// <exception cref="T:System.InvalidCastException">There is a type mismatch.</exception>
		// Token: 0x06002C87 RID: 11399 RVA: 0x000C109F File Offset: 0x000BF29F
		public virtual SqlInt64 GetSqlInt64(int ordinal)
		{
			this.EnsureSubclassOverride();
			return ValueUtilsSmi.GetSqlInt64(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal));
		}

		/// <summary>Gets the value for the column specified by the ordinal as a <see cref="T:System.Data.SqlTypes.SqlSingle" />.</summary>
		/// <returns>The column value as a <see cref="T:System.Data.SqlTypes.SqlSingle" />.</returns>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <exception cref="T:System.IndexOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />).</exception>
		/// <exception cref="T:System.InvalidCastException">There is a type mismatch.</exception>
		// Token: 0x06002C88 RID: 11400 RVA: 0x000C10C0 File Offset: 0x000BF2C0
		public virtual SqlSingle GetSqlSingle(int ordinal)
		{
			this.EnsureSubclassOverride();
			return ValueUtilsSmi.GetSqlSingle(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal));
		}

		/// <summary>Gets the value for the column specified by the ordinal as a <see cref="T:System.Data.SqlTypes.SqlDouble" />.</summary>
		/// <returns>The column value as a <see cref="T:System.Data.SqlTypes.SqlDouble" />.</returns>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <exception cref="T:System.IndexOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />).</exception>
		/// <exception cref="T:System.InvalidCastException">There is a type mismatch.</exception>
		// Token: 0x06002C89 RID: 11401 RVA: 0x000C10E1 File Offset: 0x000BF2E1
		public virtual SqlDouble GetSqlDouble(int ordinal)
		{
			this.EnsureSubclassOverride();
			return ValueUtilsSmi.GetSqlDouble(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal));
		}

		/// <summary>Gets the value for the column specified by the ordinal as a <see cref="T:System.Data.SqlTypes.SqlMoney" />.</summary>
		/// <returns>The column value as a <see cref="T:System.Data.SqlTypes.SqlMoney" />.</returns>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <exception cref="T:System.IndexOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />).</exception>
		/// <exception cref="T:System.InvalidCastException">There is a type mismatch.</exception>
		// Token: 0x06002C8A RID: 11402 RVA: 0x000C1102 File Offset: 0x000BF302
		public virtual SqlMoney GetSqlMoney(int ordinal)
		{
			this.EnsureSubclassOverride();
			return ValueUtilsSmi.GetSqlMoney(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal));
		}

		/// <summary>Gets the value for the column specified by the ordinal as a <see cref="T:System.Data.SqlTypes.SqlDateTime" />.</summary>
		/// <returns>The column value as a <see cref="T:System.Data.SqlTypes.SqlDateTime" />.</returns>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <exception cref="T:System.IndexOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />).</exception>
		/// <exception cref="T:System.InvalidCastException">There is a type mismatch.</exception>
		// Token: 0x06002C8B RID: 11403 RVA: 0x000C1123 File Offset: 0x000BF323
		public virtual SqlDateTime GetSqlDateTime(int ordinal)
		{
			this.EnsureSubclassOverride();
			return ValueUtilsSmi.GetSqlDateTime(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal));
		}

		/// <summary>Gets the value for the column specified by the ordinal as a <see cref="T:System.Data.SqlTypes.SqlDecimal" />.</summary>
		/// <returns>The column value as a <see cref="T:System.Data.SqlTypes.SqlDecimal" />.</returns>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <exception cref="T:System.IndexOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />).</exception>
		/// <exception cref="T:System.InvalidCastException">There is a type mismatch.</exception>
		// Token: 0x06002C8C RID: 11404 RVA: 0x000C1144 File Offset: 0x000BF344
		public virtual SqlDecimal GetSqlDecimal(int ordinal)
		{
			this.EnsureSubclassOverride();
			return ValueUtilsSmi.GetSqlDecimal(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal));
		}

		/// <summary>Gets the value for the column specified by the ordinal as a <see cref="T:System.Data.SqlTypes.SqlString" />.</summary>
		/// <returns>The column value as a <see cref="T:System.Data.SqlTypes.SqlString" />.</returns>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <exception cref="T:System.IndexOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />).</exception>
		/// <exception cref="T:System.InvalidCastException">There is a type mismatch.</exception>
		// Token: 0x06002C8D RID: 11405 RVA: 0x000C1165 File Offset: 0x000BF365
		public virtual SqlString GetSqlString(int ordinal)
		{
			this.EnsureSubclassOverride();
			return ValueUtilsSmi.GetSqlString(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal));
		}

		/// <summary>Gets the value for the column specified by the ordinal as a <see cref="T:System.Data.SqlTypes.SqlGuid" />.</summary>
		/// <returns>The column value as a <see cref="T:System.Data.SqlTypes.SqlGuid" />.</returns>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <exception cref="T:System.IndexOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />).</exception>
		/// <exception cref="T:System.InvalidCastException">There is a type mismatch.</exception>
		// Token: 0x06002C8E RID: 11406 RVA: 0x000C1186 File Offset: 0x000BF386
		public virtual SqlGuid GetSqlGuid(int ordinal)
		{
			this.EnsureSubclassOverride();
			return ValueUtilsSmi.GetSqlGuid(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal));
		}

		/// <summary>Sets new values for all of the columns in the <see cref="T:Microsoft.SqlServer.Server.SqlDataRecord" />. These values are expressed as common language runtime (CLR) types.</summary>
		/// <returns>The number of column values set as an integer.</returns>
		/// <param name="values">The array of new values, expressed as CLR types boxed as <see cref="T:System.Object" /> references, for the <see cref="T:Microsoft.SqlServer.Server.SqlDataRecord" /> instance.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="values" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">The size of values does not match the number of columns in the <see cref="T:Microsoft.SqlServer.Server.SqlDataRecord" /> instance.</exception>
		// Token: 0x06002C8F RID: 11407 RVA: 0x000C11A8 File Offset: 0x000BF3A8
		public virtual int SetValues(params object[] values)
		{
			this.EnsureSubclassOverride();
			if (values == null)
			{
				throw ADP.ArgumentNull("values");
			}
			int num = ((values.Length > this.FieldCount) ? this.FieldCount : values.Length);
			ExtendedClrTypeCode[] array = new ExtendedClrTypeCode[num];
			for (int i = 0; i < num; i++)
			{
				SqlMetaData sqlMetaData = this.GetSqlMetaData(i);
				array[i] = MetaDataUtilsSmi.DetermineExtendedTypeCodeForUseWithSqlDbType(sqlMetaData.SqlDbType, false, values[i]);
				if (ExtendedClrTypeCode.Invalid == array[i])
				{
					throw ADP.InvalidCast();
				}
			}
			for (int j = 0; j < num; j++)
			{
				ValueUtilsSmi.SetCompatibleValueV200(this._eventSink, this._recordBuffer, j, this.GetSmiMetaData(j), values[j], array[j], 0, 0, null);
			}
			return num;
		}

		/// <summary>Sets a new value, expressed as a common language runtime (CLR) type, for the column specified by the column ordinal.</summary>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <param name="value">The new value for the specified column.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />).</exception>
		// Token: 0x06002C90 RID: 11408 RVA: 0x000C1250 File Offset: 0x000BF450
		public virtual void SetValue(int ordinal, object value)
		{
			this.EnsureSubclassOverride();
			ExtendedClrTypeCode extendedClrTypeCode = MetaDataUtilsSmi.DetermineExtendedTypeCodeForUseWithSqlDbType(this.GetSqlMetaData(ordinal).SqlDbType, false, value);
			if (ExtendedClrTypeCode.Invalid == extendedClrTypeCode)
			{
				throw ADP.InvalidCast();
			}
			ValueUtilsSmi.SetCompatibleValueV200(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), value, extendedClrTypeCode, 0, 0, null);
		}

		/// <summary>Sets the data stored in the column to the specified <see cref="T:System.Boolean" /> value.</summary>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <param name="value">The new value of the column.</param>
		// Token: 0x06002C91 RID: 11409 RVA: 0x000C129F File Offset: 0x000BF49F
		public virtual void SetBoolean(int ordinal, bool value)
		{
			this.EnsureSubclassOverride();
			ValueUtilsSmi.SetBoolean(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), value);
		}

		/// <summary>Sets the data stored in the column to the specified <see cref="T:System.Byte" /> value.</summary>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <param name="value">The new value of the column.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />).</exception>
		// Token: 0x06002C92 RID: 11410 RVA: 0x000C12C1 File Offset: 0x000BF4C1
		public virtual void SetByte(int ordinal, byte value)
		{
			this.EnsureSubclassOverride();
			ValueUtilsSmi.SetByte(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), value);
		}

		/// <summary>Sets the data stored in the column to the specified array of <see cref="T:System.Byte" /> values.</summary>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <param name="fieldOffset">The offset into the field value to start copying bytes.</param>
		/// <param name="buffer">The target buffer from which to copy bytes.</param>
		/// <param name="bufferOffset">The offset into the buffer from which to start copying bytes.</param>
		/// <param name="length">The number of bytes to copy from the buffer.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />).</exception>
		// Token: 0x06002C93 RID: 11411 RVA: 0x000C12E3 File Offset: 0x000BF4E3
		public virtual void SetBytes(int ordinal, long fieldOffset, byte[] buffer, int bufferOffset, int length)
		{
			this.EnsureSubclassOverride();
			ValueUtilsSmi.SetBytes(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), fieldOffset, buffer, bufferOffset, length);
		}

		/// <summary>Sets the data stored in the column to the specified <see cref="T:System.Char" /> value.</summary>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <param name="value">The new value of the column.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />).</exception>
		// Token: 0x06002C94 RID: 11412 RVA: 0x000C0CE7 File Offset: 0x000BEEE7
		public virtual void SetChar(int ordinal, char value)
		{
			this.EnsureSubclassOverride();
			throw ADP.NotSupported();
		}

		/// <summary>Sets the data stored in the column to the specified array of <see cref="T:System.Char" /> values.</summary>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <param name="fieldOffset">The offset into the field value to start copying characters.</param>
		/// <param name="buffer">The target buffer from which to copy chars.</param>
		/// <param name="bufferOffset">The offset into the buffer from which to start copying chars.</param>
		/// <param name="length">The number of chars to copy from the buffer.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />).</exception>
		// Token: 0x06002C95 RID: 11413 RVA: 0x000C130B File Offset: 0x000BF50B
		public virtual void SetChars(int ordinal, long fieldOffset, char[] buffer, int bufferOffset, int length)
		{
			this.EnsureSubclassOverride();
			ValueUtilsSmi.SetChars(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), fieldOffset, buffer, bufferOffset, length);
		}

		/// <summary>Sets the data stored in the column to the specified <see cref="T:System.Int16" /> value.</summary>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <param name="value">The new value of the column.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />).</exception>
		// Token: 0x06002C96 RID: 11414 RVA: 0x000C1333 File Offset: 0x000BF533
		public virtual void SetInt16(int ordinal, short value)
		{
			this.EnsureSubclassOverride();
			ValueUtilsSmi.SetInt16(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), value);
		}

		/// <summary>Sets the data stored in the column to the specified <see cref="T:System.Int32" /> value.</summary>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <param name="value">The new value of the column.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />).</exception>
		// Token: 0x06002C97 RID: 11415 RVA: 0x000C1355 File Offset: 0x000BF555
		public virtual void SetInt32(int ordinal, int value)
		{
			this.EnsureSubclassOverride();
			ValueUtilsSmi.SetInt32(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), value);
		}

		/// <summary>Sets the data stored in the column to the specified <see cref="T:System.Int64" /> value.</summary>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <param name="value">The new value of the column.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />).</exception>
		// Token: 0x06002C98 RID: 11416 RVA: 0x000C1377 File Offset: 0x000BF577
		public virtual void SetInt64(int ordinal, long value)
		{
			this.EnsureSubclassOverride();
			ValueUtilsSmi.SetInt64(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), value);
		}

		/// <summary>Sets the data stored in the column to the specified float value.</summary>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <param name="value">The new value of the column.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />).</exception>
		// Token: 0x06002C99 RID: 11417 RVA: 0x000C1399 File Offset: 0x000BF599
		public virtual void SetFloat(int ordinal, float value)
		{
			this.EnsureSubclassOverride();
			ValueUtilsSmi.SetSingle(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), value);
		}

		/// <summary>Sets the data stored in the column to the specified <see cref="T:System.Double" /> value.</summary>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <param name="value">The new value of the column.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />).</exception>
		// Token: 0x06002C9A RID: 11418 RVA: 0x000C13BB File Offset: 0x000BF5BB
		public virtual void SetDouble(int ordinal, double value)
		{
			this.EnsureSubclassOverride();
			ValueUtilsSmi.SetDouble(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), value);
		}

		/// <summary>Sets the data stored in the column to the specified <see cref="T:System.String" /> value.</summary>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <param name="value">The new value of the column.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />).</exception>
		// Token: 0x06002C9B RID: 11419 RVA: 0x000C13DD File Offset: 0x000BF5DD
		public virtual void SetString(int ordinal, string value)
		{
			this.EnsureSubclassOverride();
			ValueUtilsSmi.SetString(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), value);
		}

		/// <summary>Sets the data stored in the column to the specified <see cref="T:System.Decimal" /> value.</summary>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <param name="value">The new value of the column.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />).</exception>
		// Token: 0x06002C9C RID: 11420 RVA: 0x000C13FF File Offset: 0x000BF5FF
		public virtual void SetDecimal(int ordinal, decimal value)
		{
			this.EnsureSubclassOverride();
			ValueUtilsSmi.SetDecimal(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), value);
		}

		/// <summary>Sets the data stored in the column to the specified <see cref="T:System.DateTime" /> value.</summary>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <param name="value">The new value of the column.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />).</exception>
		// Token: 0x06002C9D RID: 11421 RVA: 0x000C1421 File Offset: 0x000BF621
		public virtual void SetDateTime(int ordinal, DateTime value)
		{
			this.EnsureSubclassOverride();
			ValueUtilsSmi.SetDateTime(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), value);
		}

		/// <summary>Sets the value of the column specified to the <see cref="T:System.TimeSpan" />.</summary>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <param name="value">The new value of the column.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="ordinal" /> passed in is a negative number.</exception>
		/// <exception cref="T:System.ArgumentException">The <see cref="T:System.TimeSpan" /> value passed in is greater than 24 hours in length.</exception>
		// Token: 0x06002C9E RID: 11422 RVA: 0x000C1443 File Offset: 0x000BF643
		public virtual void SetTimeSpan(int ordinal, TimeSpan value)
		{
			this.EnsureSubclassOverride();
			ValueUtilsSmi.SetTimeSpan(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), value);
		}

		/// <summary>Sets the value of the column specified to the <see cref="T:System.DateTimeOffset" /> value.</summary>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <param name="value">The new value of the column.</param>
		// Token: 0x06002C9F RID: 11423 RVA: 0x000C1465 File Offset: 0x000BF665
		public virtual void SetDateTimeOffset(int ordinal, DateTimeOffset value)
		{
			this.EnsureSubclassOverride();
			ValueUtilsSmi.SetDateTimeOffset(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), value);
		}

		/// <summary>Sets the value in the specified column to <see cref="T:System.DBNull" />.</summary>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		// Token: 0x06002CA0 RID: 11424 RVA: 0x000C1487 File Offset: 0x000BF687
		public virtual void SetDBNull(int ordinal)
		{
			this.EnsureSubclassOverride();
			ValueUtilsSmi.SetDBNull(this._eventSink, this._recordBuffer, ordinal, true);
		}

		/// <summary>Sets the data stored in the column to the specified <see cref="T:System.Guid" /> value.</summary>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <param name="value">The new value of the column.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />).</exception>
		// Token: 0x06002CA1 RID: 11425 RVA: 0x000C14A2 File Offset: 0x000BF6A2
		public virtual void SetGuid(int ordinal, Guid value)
		{
			this.EnsureSubclassOverride();
			ValueUtilsSmi.SetGuid(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), value);
		}

		/// <summary>Sets the data stored in the column to the specified <see cref="T:System.Data.SqlTypes.SqlBoolean" /> value.</summary>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <param name="value">The new value of the column.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />).</exception>
		// Token: 0x06002CA2 RID: 11426 RVA: 0x000C14C4 File Offset: 0x000BF6C4
		public virtual void SetSqlBoolean(int ordinal, SqlBoolean value)
		{
			this.EnsureSubclassOverride();
			ValueUtilsSmi.SetSqlBoolean(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), value);
		}

		/// <summary>Sets the data stored in the column to the specified <see cref="T:System.Data.SqlTypes.SqlByte" /> value.</summary>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <param name="value">The new value of the column.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />).</exception>
		// Token: 0x06002CA3 RID: 11427 RVA: 0x000C14E6 File Offset: 0x000BF6E6
		public virtual void SetSqlByte(int ordinal, SqlByte value)
		{
			this.EnsureSubclassOverride();
			ValueUtilsSmi.SetSqlByte(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), value);
		}

		/// <summary>Sets the data stored in the column to the specified <see cref="T:System.Data.SqlTypes.SqlInt16" /> value.</summary>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <param name="value">The new value of the column.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />).</exception>
		// Token: 0x06002CA4 RID: 11428 RVA: 0x000C1508 File Offset: 0x000BF708
		public virtual void SetSqlInt16(int ordinal, SqlInt16 value)
		{
			this.EnsureSubclassOverride();
			ValueUtilsSmi.SetSqlInt16(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), value);
		}

		/// <summary>Sets the data stored in the column to the specified <see cref="T:System.Data.SqlTypes.SqlInt32" /> value.</summary>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <param name="value">The new value of the column.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />).</exception>
		// Token: 0x06002CA5 RID: 11429 RVA: 0x000C152A File Offset: 0x000BF72A
		public virtual void SetSqlInt32(int ordinal, SqlInt32 value)
		{
			this.EnsureSubclassOverride();
			ValueUtilsSmi.SetSqlInt32(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), value);
		}

		/// <summary>Sets the data stored in the column to the specified <see cref="T:System.Data.SqlTypes.SqlInt64" /> value.</summary>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <param name="value">The new value of the column.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />).</exception>
		// Token: 0x06002CA6 RID: 11430 RVA: 0x000C154C File Offset: 0x000BF74C
		public virtual void SetSqlInt64(int ordinal, SqlInt64 value)
		{
			this.EnsureSubclassOverride();
			ValueUtilsSmi.SetSqlInt64(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), value);
		}

		/// <summary>Sets the data stored in the column to the specified <see cref="T:System.Data.SqlTypes.SqlSingle" /> value.</summary>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <param name="value">The new value of the column.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />).</exception>
		// Token: 0x06002CA7 RID: 11431 RVA: 0x000C156E File Offset: 0x000BF76E
		public virtual void SetSqlSingle(int ordinal, SqlSingle value)
		{
			this.EnsureSubclassOverride();
			ValueUtilsSmi.SetSqlSingle(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), value);
		}

		/// <summary>Sets the data stored in the column to the specified <see cref="T:System.Data.SqlTypes.SqlDouble" /> value.</summary>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <param name="value">The new value of the column.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />).</exception>
		// Token: 0x06002CA8 RID: 11432 RVA: 0x000C1590 File Offset: 0x000BF790
		public virtual void SetSqlDouble(int ordinal, SqlDouble value)
		{
			this.EnsureSubclassOverride();
			ValueUtilsSmi.SetSqlDouble(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), value);
		}

		/// <summary>Sets the data stored in the column to the specified <see cref="T:System.Data.SqlTypes.SqlMoney" /> value.</summary>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <param name="value">The new value of the column.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />).</exception>
		// Token: 0x06002CA9 RID: 11433 RVA: 0x000C15B2 File Offset: 0x000BF7B2
		public virtual void SetSqlMoney(int ordinal, SqlMoney value)
		{
			this.EnsureSubclassOverride();
			ValueUtilsSmi.SetSqlMoney(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), value);
		}

		/// <summary>Sets the data stored in the column to the specified <see cref="T:System.Data.SqlTypes.SqlDateTime" /> value.</summary>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <param name="value">The new value of the column.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />).</exception>
		// Token: 0x06002CAA RID: 11434 RVA: 0x000C15D4 File Offset: 0x000BF7D4
		public virtual void SetSqlDateTime(int ordinal, SqlDateTime value)
		{
			this.EnsureSubclassOverride();
			ValueUtilsSmi.SetSqlDateTime(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), value);
		}

		/// <summary>Sets the data stored in the column to the specified <see cref="T:System.Data.SqlTypes.SqlXml" /> value.</summary>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <param name="value">The new value of the column.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />).</exception>
		// Token: 0x06002CAB RID: 11435 RVA: 0x000C15F6 File Offset: 0x000BF7F6
		public virtual void SetSqlXml(int ordinal, SqlXml value)
		{
			this.EnsureSubclassOverride();
			ValueUtilsSmi.SetSqlXml(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), value);
		}

		/// <summary>Sets the data stored in the column to the specified <see cref="T:System.Data.SqlTypes.SqlDecimal" /> value.</summary>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <param name="value">The new value of the column.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />).</exception>
		// Token: 0x06002CAC RID: 11436 RVA: 0x000C1618 File Offset: 0x000BF818
		public virtual void SetSqlDecimal(int ordinal, SqlDecimal value)
		{
			this.EnsureSubclassOverride();
			ValueUtilsSmi.SetSqlDecimal(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), value);
		}

		/// <summary>Sets the data stored in the column to the specified <see cref="T:System.Data.SqlTypes.SqlString" /> value.</summary>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <param name="value">The new value of the column.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />).</exception>
		// Token: 0x06002CAD RID: 11437 RVA: 0x000C163A File Offset: 0x000BF83A
		public virtual void SetSqlString(int ordinal, SqlString value)
		{
			this.EnsureSubclassOverride();
			ValueUtilsSmi.SetSqlString(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), value);
		}

		/// <summary>Sets the data stored in the column to the specified <see cref="T:System.Data.SqlTypes.SqlBinary" /> value.</summary>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <param name="value">The new value of the column.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />).</exception>
		// Token: 0x06002CAE RID: 11438 RVA: 0x000C165C File Offset: 0x000BF85C
		public virtual void SetSqlBinary(int ordinal, SqlBinary value)
		{
			this.EnsureSubclassOverride();
			ValueUtilsSmi.SetSqlBinary(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), value);
		}

		/// <summary>Sets the data stored in the column to the specified <see cref="T:System.Data.SqlTypes.SqlGuid" /> value.</summary>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <param name="value">The new value of the column.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />).</exception>
		// Token: 0x06002CAF RID: 11439 RVA: 0x000C167E File Offset: 0x000BF87E
		public virtual void SetSqlGuid(int ordinal, SqlGuid value)
		{
			this.EnsureSubclassOverride();
			ValueUtilsSmi.SetSqlGuid(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), value);
		}

		/// <summary>Sets the data stored in the column to the specified <see cref="T:System.Data.SqlTypes.SqlChars" /> value.</summary>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <param name="value">The new value of the column.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />).</exception>
		// Token: 0x06002CB0 RID: 11440 RVA: 0x000C16A0 File Offset: 0x000BF8A0
		public virtual void SetSqlChars(int ordinal, SqlChars value)
		{
			this.EnsureSubclassOverride();
			ValueUtilsSmi.SetSqlChars(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), value);
		}

		/// <summary>Sets the data stored in the column to the specified <see cref="T:System.Data.SqlTypes.SqlBytes" /> value.</summary>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <param name="value">The new value of the column.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="ordinal" /> is less than 0 or greater than the number of columns (that is, <see cref="P:Microsoft.SqlServer.Server.SqlDataRecord.FieldCount" />).</exception>
		// Token: 0x06002CB1 RID: 11441 RVA: 0x000C16C2 File Offset: 0x000BF8C2
		public virtual void SetSqlBytes(int ordinal, SqlBytes value)
		{
			this.EnsureSubclassOverride();
			ValueUtilsSmi.SetSqlBytes(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), value);
		}

		/// <summary>Inititializes a new <see cref="T:Microsoft.SqlServer.Server.SqlDataRecord" /> instance with the schema based on the array of <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> objects passed as an argument.</summary>
		/// <param name="metaData">An array of <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> objects that describe each column in the <see cref="T:Microsoft.SqlServer.Server.SqlDataRecord" />.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="metaData" /> is null. </exception>
		// Token: 0x06002CB2 RID: 11442 RVA: 0x000C16E4 File Offset: 0x000BF8E4
		public SqlDataRecord(params SqlMetaData[] metaData)
		{
			if (metaData == null)
			{
				throw ADP.ArgumentNull("metaData");
			}
			this._columnMetaData = new SqlMetaData[metaData.Length];
			this._columnSmiMetaData = new SmiExtendedMetaData[metaData.Length];
			for (int i = 0; i < this._columnSmiMetaData.Length; i++)
			{
				if (metaData[i] == null)
				{
					throw ADP.ArgumentNull(string.Format("{0}[{1}]", "metaData", i));
				}
				this._columnMetaData[i] = metaData[i];
				this._columnSmiMetaData[i] = MetaDataUtilsSmi.SqlMetaDataToSmiExtendedMetaData(this._columnMetaData[i]);
			}
			this._eventSink = new SmiEventSink_Default();
			this._recordBuffer = new MemoryRecordBuffer(this._columnSmiMetaData);
			this._usesStringStorageForXml = true;
			this._eventSink.ProcessMessagesAndThrow();
		}

		// Token: 0x06002CB3 RID: 11443 RVA: 0x000C17A4 File Offset: 0x000BF9A4
		internal SqlDataRecord(SmiRecordBuffer recordBuffer, params SmiExtendedMetaData[] metaData)
		{
			this._columnMetaData = new SqlMetaData[metaData.Length];
			this._columnSmiMetaData = new SmiExtendedMetaData[metaData.Length];
			for (int i = 0; i < this._columnSmiMetaData.Length; i++)
			{
				this._columnSmiMetaData[i] = metaData[i];
				this._columnMetaData[i] = MetaDataUtilsSmi.SmiExtendedMetaDataToSqlMetaData(this._columnSmiMetaData[i]);
			}
			this._eventSink = new SmiEventSink_Default();
			this._recordBuffer = recordBuffer;
			this._eventSink.ProcessMessagesAndThrow();
		}

		// Token: 0x17000754 RID: 1876
		// (get) Token: 0x06002CB4 RID: 11444 RVA: 0x000C1823 File Offset: 0x000BFA23
		internal SmiRecordBuffer RecordBuffer
		{
			get
			{
				return this._recordBuffer;
			}
		}

		// Token: 0x06002CB5 RID: 11445 RVA: 0x000C182B File Offset: 0x000BFA2B
		internal SqlMetaData[] InternalGetMetaData()
		{
			return this._columnMetaData;
		}

		// Token: 0x06002CB6 RID: 11446 RVA: 0x000C1833 File Offset: 0x000BFA33
		internal SmiExtendedMetaData[] InternalGetSmiMetaData()
		{
			return this._columnSmiMetaData;
		}

		// Token: 0x06002CB7 RID: 11447 RVA: 0x000C183B File Offset: 0x000BFA3B
		internal SmiExtendedMetaData GetSmiMetaData(int ordinal)
		{
			return this._columnSmiMetaData[ordinal];
		}

		// Token: 0x06002CB8 RID: 11448 RVA: 0x000C1845 File Offset: 0x000BFA45
		internal void ThrowIfInvalidOrdinal(int ordinal)
		{
			if (0 > ordinal || this.FieldCount <= ordinal)
			{
				throw ADP.IndexOutOfRange(ordinal);
			}
		}

		// Token: 0x06002CB9 RID: 11449 RVA: 0x000C185B File Offset: 0x000BFA5B
		private void EnsureSubclassOverride()
		{
			if (this._recordBuffer == null)
			{
				throw SQL.SubclassMustOverride();
			}
		}

		// Token: 0x06002CBA RID: 11450 RVA: 0x000621D6 File Offset: 0x000603D6
		public IDataReader GetData(int i)
		{
			throw ADP.NotSupported();
		}

		// Token: 0x04001AD5 RID: 6869
		private SmiRecordBuffer _recordBuffer;

		// Token: 0x04001AD6 RID: 6870
		private SmiExtendedMetaData[] _columnSmiMetaData;

		// Token: 0x04001AD7 RID: 6871
		private SmiEventSink_Default _eventSink;

		// Token: 0x04001AD8 RID: 6872
		private SqlMetaData[] _columnMetaData;

		// Token: 0x04001AD9 RID: 6873
		private FieldNameLookup _fieldNameLookup;

		// Token: 0x04001ADA RID: 6874
		private bool _usesStringStorageForXml;

		// Token: 0x04001ADB RID: 6875
		private static readonly SmiMetaData s_maxNVarCharForXml = new SmiMetaData(SqlDbType.NVarChar, -1L, SmiMetaData.DefaultNVarChar_NoCollation.Precision, SmiMetaData.DefaultNVarChar_NoCollation.Scale, SmiMetaData.DefaultNVarChar.LocaleId, SmiMetaData.DefaultNVarChar.CompareOptions);
	}
}
