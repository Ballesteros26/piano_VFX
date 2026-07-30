using System;
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.Common
{
	/// <summary>Reads a forward-only stream of rows from a data source.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000345 RID: 837
	public abstract class DbDataReader : MarshalByRefObject, IDataReader, IDisposable, IDataRecord, IEnumerable
	{
		/// <summary>Gets a value indicating the depth of nesting for the current row.</summary>
		/// <returns>The depth of nesting for the current row.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170006DF RID: 1759
		// (get) Token: 0x06002792 RID: 10130
		public abstract int Depth { get; }

		/// <summary>Gets the number of columns in the current row.</summary>
		/// <returns>The number of columns in the current row.</returns>
		/// <exception cref="T:System.NotSupportedException">There is no current connection to an instance of SQL Server. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170006E0 RID: 1760
		// (get) Token: 0x06002793 RID: 10131
		public abstract int FieldCount { get; }

		/// <summary>Gets a value that indicates whether this <see cref="T:System.Data.Common.DbDataReader" /> contains one or more rows.</summary>
		/// <returns>true if the <see cref="T:System.Data.Common.DbDataReader" /> contains one or more rows; otherwise false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170006E1 RID: 1761
		// (get) Token: 0x06002794 RID: 10132
		public abstract bool HasRows { get; }

		/// <summary>Gets a value indicating whether the <see cref="T:System.Data.Common.DbDataReader" /> is closed.</summary>
		/// <returns>true if the <see cref="T:System.Data.Common.DbDataReader" /> is closed; otherwise false.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Data.SqlClient.SqlDataReader" /> is closed. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170006E2 RID: 1762
		// (get) Token: 0x06002795 RID: 10133
		public abstract bool IsClosed { get; }

		/// <summary>Gets the number of rows changed, inserted, or deleted by execution of the SQL statement. </summary>
		/// <returns>The number of rows changed, inserted, or deleted. -1 for SELECT statements; 0 if no rows were affected or the statement failed.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170006E3 RID: 1763
		// (get) Token: 0x06002796 RID: 10134
		public abstract int RecordsAffected { get; }

		/// <summary>Gets the number of fields in the <see cref="T:System.Data.Common.DbDataReader" /> that are not hidden.</summary>
		/// <returns>The number of fields that are not hidden.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170006E4 RID: 1764
		// (get) Token: 0x06002797 RID: 10135 RVA: 0x000B0C88 File Offset: 0x000AEE88
		public virtual int VisibleFieldCount
		{
			get
			{
				return this.FieldCount;
			}
		}

		/// <summary>Gets the value of the specified column as an instance of <see cref="T:System.Object" />.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		/// <exception cref="T:System.IndexOutOfRangeException">The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170006E5 RID: 1765
		public abstract object this[int ordinal] { get; }

		/// <summary>Gets the value of the specified column as an instance of <see cref="T:System.Object" />.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="name">The name of the column.</param>
		/// <exception cref="T:System.IndexOutOfRangeException">No column with the specified name was found. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170006E6 RID: 1766
		public abstract object this[string name] { get; }

		/// <summary>Closes the <see cref="T:System.Data.Common.DbDataReader" /> object.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600279A RID: 10138 RVA: 0x00005E03 File Offset: 0x00004003
		public virtual void Close()
		{
		}

		/// <summary>Releases all resources used by the current instance of the <see cref="T:System.Data.Common.DbDataReader" /> class.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600279B RID: 10139 RVA: 0x000B0C90 File Offset: 0x000AEE90
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Dispose()
		{
			this.Dispose(true);
		}

		/// <summary>Releases the managed resources used by the <see cref="T:System.Data.Common.DbDataReader" /> and optionally releases the unmanaged resources.</summary>
		/// <param name="disposing">true to release managed and unmanaged resources; false to release only unmanaged resources.</param>
		// Token: 0x0600279C RID: 10140 RVA: 0x000B0C99 File Offset: 0x000AEE99
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.Close();
			}
		}

		/// <summary>Gets name of the data type of the specified column.</summary>
		/// <returns>A string representing the name of the data type.</returns>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600279D RID: 10141
		public abstract string GetDataTypeName(int ordinal);

		/// <summary>Returns an <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the rows in the data reader.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the rows in the data reader.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600279E RID: 10142
		[EditorBrowsable(EditorBrowsableState.Never)]
		public abstract IEnumerator GetEnumerator();

		/// <summary>Gets the data type of the specified column.</summary>
		/// <returns>The data type of the specified column.</returns>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600279F RID: 10143
		public abstract Type GetFieldType(int ordinal);

		/// <summary>Gets the name of the column, given the zero-based column ordinal.</summary>
		/// <returns>The name of the specified column.</returns>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060027A0 RID: 10144
		public abstract string GetName(int ordinal);

		/// <summary>Gets the column ordinal given the name of the column.</summary>
		/// <returns>The zero-based column ordinal.</returns>
		/// <param name="name">The name of the column.</param>
		/// <exception cref="T:System.IndexOutOfRangeException">The name specified is not a valid column name.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060027A1 RID: 10145
		public abstract int GetOrdinal(string name);

		/// <summary>Returns a <see cref="T:System.Data.DataTable" /> that describes the column metadata of the <see cref="T:System.Data.Common.DbDataReader" />.</summary>
		/// <returns>A <see cref="T:System.Data.DataTable" /> that describes the column metadata.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Data.SqlClient.SqlDataReader" /> is closed. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060027A2 RID: 10146 RVA: 0x0007BE7D File Offset: 0x0007A07D
		public virtual DataTable GetSchemaTable()
		{
			throw new NotSupportedException();
		}

		/// <summary>Gets the value of the specified column as a Boolean.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060027A3 RID: 10147
		public abstract bool GetBoolean(int ordinal);

		/// <summary>Gets the value of the specified column as a byte.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060027A4 RID: 10148
		public abstract byte GetByte(int ordinal);

		/// <summary>Reads a stream of bytes from the specified column, starting at location indicated by <paramref name="dataOffset" />, into the buffer, starting at the location indicated by <paramref name="bufferOffset" />.</summary>
		/// <returns>The actual number of bytes read.</returns>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		/// <param name="dataOffset">The index within the row from which to begin the read operation.</param>
		/// <param name="buffer">The buffer into which to copy the data.</param>
		/// <param name="bufferOffset">The index with the buffer to which the data will be copied.</param>
		/// <param name="length">The maximum number of characters to read.</param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060027A5 RID: 10149
		public abstract long GetBytes(int ordinal, long dataOffset, byte[] buffer, int bufferOffset, int length);

		/// <summary>Gets the value of the specified column as a single character.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060027A6 RID: 10150
		public abstract char GetChar(int ordinal);

		/// <summary>Reads a stream of characters from the specified column, starting at location indicated by <paramref name="dataOffset" />, into the buffer, starting at the location indicated by <paramref name="bufferOffset" />.</summary>
		/// <returns>The actual number of characters read.</returns>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		/// <param name="dataOffset">The index within the row from which to begin the read operation.</param>
		/// <param name="buffer">The buffer into which to copy the data.</param>
		/// <param name="bufferOffset">The index with the buffer to which the data will be copied.</param>
		/// <param name="length">The maximum number of characters to read.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060027A7 RID: 10151
		public abstract long GetChars(int ordinal, long dataOffset, char[] buffer, int bufferOffset, int length);

		/// <summary>Returns a <see cref="T:System.Data.Common.DbDataReader" /> object for the requested column ordinal.</summary>
		/// <returns>A <see cref="T:System.Data.Common.DbDataReader" /> object.</returns>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060027A8 RID: 10152 RVA: 0x000B0CA4 File Offset: 0x000AEEA4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public DbDataReader GetData(int ordinal)
		{
			return this.GetDbDataReader(ordinal);
		}

		/// <summary>For a description of this member, see <see cref="M:System.Data.IDataRecord.GetData(System.Int32)" />.</summary>
		/// <returns>An instance of <see cref="T:System.Data.IDataReader" /> to be used when the field points to more remote structured data.</returns>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		// Token: 0x060027A9 RID: 10153 RVA: 0x000B0CA4 File Offset: 0x000AEEA4
		IDataReader IDataRecord.GetData(int ordinal)
		{
			return this.GetDbDataReader(ordinal);
		}

		/// <summary>Returns a <see cref="T:System.Data.Common.DbDataReader" /> object for the requested column ordinal that can be overridden with a provider-specific implementation.</summary>
		/// <returns>A <see cref="T:System.Data.Common.DbDataReader" /> object.</returns>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		// Token: 0x060027AA RID: 10154 RVA: 0x000621D6 File Offset: 0x000603D6
		protected virtual DbDataReader GetDbDataReader(int ordinal)
		{
			throw ADP.NotSupported();
		}

		/// <summary>Gets the value of the specified column as a <see cref="T:System.DateTime" /> object.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060027AB RID: 10155
		public abstract DateTime GetDateTime(int ordinal);

		/// <summary>Gets the value of the specified column as a <see cref="T:System.Decimal" /> object.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060027AC RID: 10156
		public abstract decimal GetDecimal(int ordinal);

		/// <summary>Gets the value of the specified column as a double-precision floating point number.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060027AD RID: 10157
		public abstract double GetDouble(int ordinal);

		/// <summary>Gets the value of the specified column as a single-precision floating point number.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060027AE RID: 10158
		public abstract float GetFloat(int ordinal);

		/// <summary>Gets the value of the specified column as a globally-unique identifier (GUID).</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060027AF RID: 10159
		public abstract Guid GetGuid(int ordinal);

		/// <summary>Gets the value of the specified column as a 16-bit signed integer.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060027B0 RID: 10160
		public abstract short GetInt16(int ordinal);

		/// <summary>Gets the value of the specified column as a 32-bit signed integer.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060027B1 RID: 10161
		public abstract int GetInt32(int ordinal);

		/// <summary>Gets the value of the specified column as a 64-bit signed integer.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060027B2 RID: 10162
		public abstract long GetInt64(int ordinal);

		/// <summary>Returns the provider-specific field type of the specified column.</summary>
		/// <returns>The <see cref="T:System.Type" /> object that describes the data type of the specified column.</returns>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060027B3 RID: 10163 RVA: 0x000B0CAD File Offset: 0x000AEEAD
		[EditorBrowsable(EditorBrowsableState.Never)]
		public virtual Type GetProviderSpecificFieldType(int ordinal)
		{
			return this.GetFieldType(ordinal);
		}

		/// <summary>Gets the value of the specified column as an instance of <see cref="T:System.Object" />.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060027B4 RID: 10164 RVA: 0x000604E6 File Offset: 0x0005E6E6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public virtual object GetProviderSpecificValue(int ordinal)
		{
			return this.GetValue(ordinal);
		}

		/// <summary>Gets all provider-specific attribute columns in the collection for the current row.</summary>
		/// <returns>The number of instances of <see cref="T:System.Object" /> in the array.</returns>
		/// <param name="values">An array of <see cref="T:System.Object" /> into which to copy the attribute columns.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060027B5 RID: 10165 RVA: 0x000B0CB6 File Offset: 0x000AEEB6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public virtual int GetProviderSpecificValues(object[] values)
		{
			return this.GetValues(values);
		}

		/// <summary>Gets the value of the specified column as an instance of <see cref="T:System.String" />.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060027B6 RID: 10166
		public abstract string GetString(int ordinal);

		/// <summary>Retrieves data as a <see cref="T:System.IO.Stream" />.</summary>
		/// <returns>The returned object.</returns>
		/// <param name="ordinal">Retrieves data as a <see cref="T:System.IO.Stream" />.</param>
		/// <exception cref="T:System.InvalidOperationException">The connection drops or is closed during the data retrieval.The <see cref="T:System.Data.Common.DbDataReader" /> is closed during the data retrieval.There is no data ready to be read (for example, the first <see cref="M:System.Data.Common.DbDataReader.Read" /> hasn't been called, or returned false).Tried to read a previously-read column in sequential mode.There was an asynchronous operation in progress. This applies to all Get* methods when running in sequential mode, as they could be called while reading a stream.</exception>
		/// <exception cref="T:System.IndexOutOfRangeException">Trying to read a column that does not exist.</exception>
		/// <exception cref="T:System.InvalidCastException">The returned type was not one of the types below:binaryimagevarbinaryudt</exception>
		// Token: 0x060027B7 RID: 10167 RVA: 0x000B0CC0 File Offset: 0x000AEEC0
		public virtual Stream GetStream(int ordinal)
		{
			Stream stream;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				long num = 0L;
				byte[] array = new byte[4096];
				long bytes;
				do
				{
					bytes = this.GetBytes(ordinal, num, array, 0, array.Length);
					memoryStream.Write(array, 0, (int)bytes);
					num += bytes;
				}
				while (bytes > 0L);
				stream = new MemoryStream(memoryStream.ToArray(), false);
			}
			return stream;
		}

		/// <summary>Retrieves data as a <see cref="T:System.IO.TextReader" />.</summary>
		/// <returns>The returned object.</returns>
		/// <param name="ordinal">Retrieves data as a <see cref="T:System.IO.TextReader" />.</param>
		/// <exception cref="T:System.InvalidOperationException">The connection drops or is closed during the data retrieval.The <see cref="T:System.Data.Common.DbDataReader" /> is closed during the data retrieval.There is no data ready to be read (for example, the first <see cref="M:System.Data.Common.DbDataReader.Read" /> hasn't been called, or returned false).Tried to read a previously-read column in sequential mode.There was an asynchronous operation in progress. This applies to all Get* methods when running in sequential mode, as they could be called while reading a stream.</exception>
		/// <exception cref="T:System.IndexOutOfRangeException">Trying to read a column that does not exist.</exception>
		/// <exception cref="T:System.InvalidCastException">The returned type was not one of the types below:charncharntextnvarchartextvarchar</exception>
		// Token: 0x060027B8 RID: 10168 RVA: 0x000B0D34 File Offset: 0x000AEF34
		public virtual TextReader GetTextReader(int ordinal)
		{
			if (this.IsDBNull(ordinal))
			{
				return new StringReader(string.Empty);
			}
			return new StringReader(this.GetString(ordinal));
		}

		/// <summary>Gets the value of the specified column as an instance of <see cref="T:System.Object" />.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060027B9 RID: 10169
		public abstract object GetValue(int ordinal);

		/// <summary>Synchronously gets the value of the specified column as a type.</summary>
		/// <returns>The column to be retrieved.</returns>
		/// <param name="ordinal">The column to be retrieved.</param>
		/// <typeparam name="T">Synchronously gets the value of the specified column as a type.</typeparam>
		/// <exception cref="T:System.InvalidOperationException">The connection drops or is closed during the data retrieval.The <see cref="T:System.Data.SqlClient.SqlDataReader" /> is closed during the data retrieval.There is no data ready to be read (for example, the first <see cref="M:System.Data.SqlClient.SqlDataReader.Read" /> hasn't been called, or returned false).Tried to read a previously-read column in sequential mode.There was an asynchronous operation in progress. This applies to all Get* methods when running in sequential mode, as they could be called while reading a stream.</exception>
		/// <exception cref="T:System.IndexOutOfRangeException">Trying to read a column that does not exist.</exception>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="T" /> doesn’t match the type returned by SQL Server or cannot be cast.</exception>
		// Token: 0x060027BA RID: 10170 RVA: 0x000B0D56 File Offset: 0x000AEF56
		public virtual T GetFieldValue<T>(int ordinal)
		{
			return (T)((object)this.GetValue(ordinal));
		}

		/// <summary>Asynchronously gets the value of the specified column as a type.</summary>
		/// <returns>The type of the value to be returned.</returns>
		/// <param name="ordinal">The type of the value to be returned.</param>
		/// <typeparam name="T">The type of the value to be returned. See the remarks section for more information.</typeparam>
		/// <exception cref="T:System.InvalidOperationException">The connection drops or is closed during the data retrieval.The <see cref="T:System.Data.Common.DbDataReader" /> is closed during the data retrieval.There is no data ready to be read (for example, the first <see cref="M:System.Data.Common.DbDataReader.Read" /> hasn't been called, or returned false).Tried to read a previously-read column in sequential mode.There was an asynchronous operation in progress. This applies to all Get* methods when running in sequential mode, as they could be called while reading a stream.</exception>
		/// <exception cref="T:System.IndexOutOfRangeException">Trying to read a column that does not exist.</exception>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="T" /> doesn’t match the type returned by the data source  or cannot be cast.</exception>
		// Token: 0x060027BB RID: 10171 RVA: 0x000B0D64 File Offset: 0x000AEF64
		public Task<T> GetFieldValueAsync<T>(int ordinal)
		{
			return this.GetFieldValueAsync<T>(ordinal, CancellationToken.None);
		}

		/// <summary>Asynchronously gets the value of the specified column as a type.</summary>
		/// <returns>The type of the value to be returned.</returns>
		/// <param name="ordinal">The type of the value to be returned.</param>
		/// <param name="cancellationToken">The cancellation instruction, which propagates a notification that operations should be canceled. This does not guarantee the cancellation. A setting of CancellationToken.None makes this method equivalent to <see cref="M:System.Data.Common.DbDataReader.GetFieldValueAsync``1(System.Int32)" />. The returned task must be marked as cancelled.</param>
		/// <typeparam name="T">The type of the value to be returned. See the remarks section for more information.</typeparam>
		/// <exception cref="T:System.InvalidOperationException">The connection drops or is closed during the data retrieval.The <see cref="T:System.Data.Common.DbDataReader" /> is closed during the data retrieval.There is no data ready to be read (for example, the first <see cref="M:System.Data.Common.DbDataReader.Read" /> hasn't been called, or returned false).Tried to read a previously-read column in sequential mode.There was an asynchronous operation in progress. This applies to all Get* methods when running in sequential mode, as they could be called while reading a stream.</exception>
		/// <exception cref="T:System.IndexOutOfRangeException">Trying to read a column that does not exist.</exception>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="T" /> doesn’t match the type returned by the data source or cannot be cast.</exception>
		// Token: 0x060027BC RID: 10172 RVA: 0x000B0D74 File Offset: 0x000AEF74
		public virtual Task<T> GetFieldValueAsync<T>(int ordinal, CancellationToken cancellationToken)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return ADP.CreatedTaskWithCancellation<T>();
			}
			Task<T> task;
			try
			{
				task = Task.FromResult<T>(this.GetFieldValue<T>(ordinal));
			}
			catch (Exception ex)
			{
				task = Task.FromException<T>(ex);
			}
			return task;
		}

		/// <summary>Populates an array of objects with the column values of the current row.</summary>
		/// <returns>The number of instances of <see cref="T:System.Object" /> in the array.</returns>
		/// <param name="values">An array of <see cref="T:System.Object" /> into which to copy the attribute columns.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060027BD RID: 10173
		public abstract int GetValues(object[] values);

		/// <summary>Gets a value that indicates whether the column contains nonexistent or missing values.</summary>
		/// <returns>true if the specified column is equivalent to <see cref="T:System.DBNull" />; otherwise false.</returns>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060027BE RID: 10174
		public abstract bool IsDBNull(int ordinal);

		/// <summary>An asynchronous version of <see cref="M:System.Data.Common.DbDataReader.IsDBNull(System.Int32)" />, which gets a value that indicates whether the column contains non-existent or missing values.</summary>
		/// <returns>true if the specified column value is equivalent to DBNull otherwise false.</returns>
		/// <param name="ordinal">The zero-based column to be retrieved.</param>
		/// <exception cref="T:System.InvalidOperationException">The connection drops or is closed during the data retrieval.The <see cref="T:System.Data.Common.DbDataReader" /> is closed during the data retrieval.There is no data ready to be read (for example, the first <see cref="M:System.Data.Common.DbDataReader.Read" /> hasn't been called, or returned false).Trying to read a previously read column in sequential mode.There was an asynchronous operation in progress. This applies to all Get* methods when running in sequential mode, as they could be called while reading a stream.</exception>
		/// <exception cref="T:System.IndexOutOfRangeException">Trying to read a column that does not exist.</exception>
		// Token: 0x060027BF RID: 10175 RVA: 0x000B0DBC File Offset: 0x000AEFBC
		public Task<bool> IsDBNullAsync(int ordinal)
		{
			return this.IsDBNullAsync(ordinal, CancellationToken.None);
		}

		/// <summary>An asynchronous version of <see cref="M:System.Data.Common.DbDataReader.IsDBNull(System.Int32)" />, which gets a value that indicates whether the column contains non-existent or missing values. Optionally, sends a notification that operations should be cancelled.</summary>
		/// <returns>true if the specified column value is equivalent to DBNull otherwise false.</returns>
		/// <param name="ordinal">The zero-based column to be retrieved.</param>
		/// <param name="cancellationToken">The cancellation instruction, which propagates a notification that operations should be canceled. This does not guarantee the cancellation. A setting of CancellationToken.None makes this method equivalent to <see cref="M:System.Data.Common.DbDataReader.IsDBNullAsync(System.Int32)" />. The returned task must be marked as cancelled.</param>
		/// <exception cref="T:System.InvalidOperationException">The connection drops or is closed during the data retrieval.The <see cref="T:System.Data.Common.DbDataReader" /> is closed during the data retrieval.There is no data ready to be read (for example, the first <see cref="M:System.Data.Common.DbDataReader.Read" /> hasn't been called, or returned false).Trying to read a previously read column in sequential mode.There was an asynchronous operation in progress. This applies to all Get* methods when running in sequential mode, as they could be called while reading a stream.</exception>
		/// <exception cref="T:System.IndexOutOfRangeException">Trying to read a column that does not exist.</exception>
		// Token: 0x060027C0 RID: 10176 RVA: 0x000B0DCC File Offset: 0x000AEFCC
		public virtual Task<bool> IsDBNullAsync(int ordinal, CancellationToken cancellationToken)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return ADP.CreatedTaskWithCancellation<bool>();
			}
			Task<bool> task;
			try
			{
				task = (this.IsDBNull(ordinal) ? ADP.TrueTask : ADP.FalseTask);
			}
			catch (Exception ex)
			{
				task = Task.FromException<bool>(ex);
			}
			return task;
		}

		/// <summary>Advances the reader to the next result when reading the results of a batch of statements.</summary>
		/// <returns>true if there are more result sets; otherwise false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060027C1 RID: 10177
		public abstract bool NextResult();

		/// <summary>Advances the reader to the next record in a result set.</summary>
		/// <returns>true if there are more rows; otherwise false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060027C2 RID: 10178
		public abstract bool Read();

		/// <summary>An asynchronous version of <see cref="M:System.Data.Common.DbDataReader.Read" />, which advances the reader to the next record in a result set. This method invokes <see cref="M:System.Data.Common.DbDataReader.ReadAsync(System.Threading.CancellationToken)" /> with CancellationToken.None.</summary>
		/// <returns>A task representing the asynchronous operation.</returns>
		/// <exception cref="T:System.Data.Common.DbException">An error occurred while executing the command text.</exception>
		// Token: 0x060027C3 RID: 10179 RVA: 0x000B0E1C File Offset: 0x000AF01C
		public Task<bool> ReadAsync()
		{
			return this.ReadAsync(CancellationToken.None);
		}

		/// <summary>This is the asynchronous version of <see cref="M:System.Data.Common.DbDataReader.Read" />.  Providers should override with an appropriate implementation. The cancellationToken may optionally be ignored.The default implementation invokes the synchronous <see cref="M:System.Data.Common.DbDataReader.Read" /> method and returns a completed task, blocking the calling thread. The default implementation will return a cancelled task if passed an already cancelled cancellationToken.  Exceptions thrown by Read will be communicated via the returned Task Exception property.Do not invoke other methods and properties of the DbDataReader object until the returned Task is complete.</summary>
		/// <returns>A task representing the asynchronous operation.</returns>
		/// <param name="cancellationToken">The cancellation instruction.</param>
		/// <exception cref="T:System.Data.Common.DbException">An error occurred while executing the command text.</exception>
		// Token: 0x060027C4 RID: 10180 RVA: 0x000B0E2C File Offset: 0x000AF02C
		public virtual Task<bool> ReadAsync(CancellationToken cancellationToken)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return ADP.CreatedTaskWithCancellation<bool>();
			}
			Task<bool> task;
			try
			{
				task = (this.Read() ? ADP.TrueTask : ADP.FalseTask);
			}
			catch (Exception ex)
			{
				task = Task.FromException<bool>(ex);
			}
			return task;
		}

		/// <summary>An asynchronous version of <see cref="M:System.Data.Common.DbDataReader.NextResult" />, which advances the reader to the next result when reading the results of a batch of statements.Invokes <see cref="M:System.Data.Common.DbDataReader.NextResultAsync(System.Threading.CancellationToken)" /> with CancellationToken.None.</summary>
		/// <returns>A task representing the asynchronous operation.</returns>
		/// <exception cref="T:System.Data.Common.DbException">An error occurred while executing the command text.</exception>
		// Token: 0x060027C5 RID: 10181 RVA: 0x000B0E7C File Offset: 0x000AF07C
		public Task<bool> NextResultAsync()
		{
			return this.NextResultAsync(CancellationToken.None);
		}

		/// <summary>This is the asynchronous version of <see cref="M:System.Data.Common.DbDataReader.NextResult" />. Providers should override with an appropriate implementation. The <paramref name="cancellationToken" /> may optionally be ignored.The default implementation invokes the synchronous <see cref="M:System.Data.Common.DbDataReader.NextResult" /> method and returns a completed task, blocking the calling thread. The default implementation will return a cancelled task if passed an already cancelled <paramref name="cancellationToken" />. Exceptions thrown by <see cref="M:System.Data.Common.DbDataReader.NextResult" /> will be communicated via the returned Task Exception property.Other methods and properties of the DbDataReader object should not be invoked while the returned Task is not yet completed.</summary>
		/// <returns>A task representing the asynchronous operation.</returns>
		/// <param name="cancellationToken">The cancellation instruction.</param>
		/// <exception cref="T:System.Data.Common.DbException">An error occurred while executing the command text.</exception>
		// Token: 0x060027C6 RID: 10182 RVA: 0x000B0E8C File Offset: 0x000AF08C
		public virtual Task<bool> NextResultAsync(CancellationToken cancellationToken)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return ADP.CreatedTaskWithCancellation<bool>();
			}
			Task<bool> task;
			try
			{
				task = (this.NextResult() ? ADP.TrueTask : ADP.FalseTask);
			}
			catch (Exception ex)
			{
				task = Task.FromException<bool>(ex);
			}
			return task;
		}
	}
}
