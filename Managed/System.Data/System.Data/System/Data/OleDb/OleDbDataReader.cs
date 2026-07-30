using System;
using System.Collections;
using System.ComponentModel;
using System.Data.Common;

namespace System.Data.OleDb
{
	/// <summary>Provides a way of reading a forward-only stream of data rows from a data source. This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000127 RID: 295
	[MonoTODO("OleDb is not implemented.")]
	public sealed class OleDbDataReader : DbDataReader
	{
		// Token: 0x06000F53 RID: 3923 RVA: 0x00050EE1 File Offset: 0x0004F0E1
		internal OleDbDataReader()
		{
		}

		/// <summary>Gets a value that indicates the depth of nesting for the current row.</summary>
		/// <returns>The depth of nesting for the current row.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06000F54 RID: 3924 RVA: 0x00050D50 File Offset: 0x0004EF50
		public override int Depth
		{
			get
			{
				throw ADP.OleDb();
			}
		}

		/// <summary>Gets the number of columns in the current row.</summary>
		/// <returns>When not positioned in a valid recordset, 0; otherwise the number of columns in the current record. The default is -1.</returns>
		/// <exception cref="T:System.NotSupportedException">There is no current connection to a data source. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000295 RID: 661
		// (get) Token: 0x06000F55 RID: 3925 RVA: 0x00050D50 File Offset: 0x0004EF50
		public override int FieldCount
		{
			get
			{
				throw ADP.OleDb();
			}
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Data.OleDb.OleDbDataReader" /> contains one or more rows.</summary>
		/// <returns>true if the <see cref="T:System.Data.OleDb.OleDbDataReader" /> contains one or more rows; otherwise false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000296 RID: 662
		// (get) Token: 0x06000F56 RID: 3926 RVA: 0x00050D50 File Offset: 0x0004EF50
		public override bool HasRows
		{
			get
			{
				throw ADP.OleDb();
			}
		}

		/// <summary>Indicates whether the data reader is closed.</summary>
		/// <returns>true if the <see cref="T:System.Data.OleDb.OleDbDataReader" /> is closed; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000297 RID: 663
		// (get) Token: 0x06000F57 RID: 3927 RVA: 0x00050D50 File Offset: 0x0004EF50
		public override bool IsClosed
		{
			get
			{
				throw ADP.OleDb();
			}
		}

		/// <summary>Gets the number of rows changed, inserted, or deleted by execution of the SQL statement.</summary>
		/// <returns>The number of rows changed, inserted, or deleted; 0 if no rows were affected or the statement failed; and -1 for SELECT statements.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000298 RID: 664
		// (get) Token: 0x06000F58 RID: 3928 RVA: 0x00050D50 File Offset: 0x0004EF50
		public override int RecordsAffected
		{
			get
			{
				throw ADP.OleDb();
			}
		}

		/// <summary>Gets the number of fields in the <see cref="T:System.Data.OleDb.OleDbDataReader" /> that are not hidden.</summary>
		/// <returns>The number of fields that are not hidden.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06000F59 RID: 3929 RVA: 0x00050D50 File Offset: 0x0004EF50
		public override int VisibleFieldCount
		{
			get
			{
				throw ADP.OleDb();
			}
		}

		/// <summary>Closes the <see cref="T:System.Data.OleDb.OleDbDataReader" /> object.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000F5A RID: 3930 RVA: 0x00050D50 File Offset: 0x0004EF50
		public override void Close()
		{
			throw ADP.OleDb();
		}

		/// <summary>Gets the value of the specified column as a Boolean.</summary>
		/// <returns>The value of the column.</returns>
		/// <param name="ordinal">The zero-based column ordinal. </param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000F5B RID: 3931 RVA: 0x00050D50 File Offset: 0x0004EF50
		public override bool GetBoolean(int ordinal)
		{
			throw ADP.OleDb();
		}

		/// <summary>Gets the value of the specified column as a byte.</summary>
		/// <returns>The value of the specified column as a byte.</returns>
		/// <param name="ordinal">The zero-based column ordinal. </param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000F5C RID: 3932 RVA: 0x00050D50 File Offset: 0x0004EF50
		public override byte GetByte(int ordinal)
		{
			throw ADP.OleDb();
		}

		/// <summary>Reads a stream of bytes from the specified column offset into the buffer as an array starting at the given buffer offset.</summary>
		/// <returns>The actual number of bytes read.</returns>
		/// <param name="ordinal">The zero-based column ordinal. </param>
		/// <param name="dataIndex">The index within the field from which to start the read operation. </param>
		/// <param name="buffer">The buffer into which to read the stream of bytes. </param>
		/// <param name="bufferIndex">The index within the <paramref name="buffer" /> where the write operation is to start. </param>
		/// <param name="length">The maximum length to copy into the buffer. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000F5D RID: 3933 RVA: 0x00050D50 File Offset: 0x0004EF50
		public override long GetBytes(int ordinal, long dataIndex, byte[] buffer, int bufferIndex, int length)
		{
			throw ADP.OleDb();
		}

		/// <summary>Gets the value of the specified column as a character.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="ordinal">The zero-based column ordinal. </param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000F5E RID: 3934 RVA: 0x00050D50 File Offset: 0x0004EF50
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override char GetChar(int ordinal)
		{
			throw ADP.OleDb();
		}

		/// <summary>Reads a stream of characters from the specified column offset into the buffer as an array starting at the given buffer offset.</summary>
		/// <returns>The actual number of characters read.</returns>
		/// <param name="ordinal">The zero-based column ordinal. </param>
		/// <param name="dataIndex">The index within the row from which to start the read operation. </param>
		/// <param name="buffer">The buffer into which to copy data. </param>
		/// <param name="bufferIndex">The index within the <paramref name="buffer" /> where the write operation is to start. </param>
		/// <param name="length">The number of characters to read. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000F5F RID: 3935 RVA: 0x00050D50 File Offset: 0x0004EF50
		public override long GetChars(int ordinal, long dataIndex, char[] buffer, int bufferIndex, int length)
		{
			throw ADP.OleDb();
		}

		/// <summary>Returns an <see cref="T:System.Data.OleDb.OleDbDataReader" /> object for the requested column ordinal.</summary>
		/// <returns>An <see cref="T:System.Data.OleDb.OleDbDataReader" /> object.</returns>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000F60 RID: 3936 RVA: 0x00050D50 File Offset: 0x0004EF50
		public new OleDbDataReader GetData(int ordinal)
		{
			throw ADP.OleDb();
		}

		// Token: 0x06000F61 RID: 3937 RVA: 0x00050D50 File Offset: 0x0004EF50
		protected override DbDataReader GetDbDataReader(int ordinal)
		{
			throw ADP.OleDb();
		}

		/// <summary>Gets the name of the source data type.</summary>
		/// <returns>The name of the back-end data type. For more information, see SQL Server data types or Access data types.</returns>
		/// <param name="index">The zero-based column ordinal. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000F62 RID: 3938 RVA: 0x00050D50 File Offset: 0x0004EF50
		public override string GetDataTypeName(int index)
		{
			throw ADP.OleDb();
		}

		/// <summary>Gets the value of the specified column as a <see cref="T:System.DateTime" /> object.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="ordinal">The zero-based column ordinal. </param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000F63 RID: 3939 RVA: 0x00050EEC File Offset: 0x0004F0EC
		public override DateTime GetDateTime(int ordinal)
		{
			throw ADP.OleDb();
		}

		/// <summary>Gets the value of the specified column as a <see cref="T:System.Decimal" /> object.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="ordinal">The zero-based column ordinal. </param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000F64 RID: 3940 RVA: 0x00050F00 File Offset: 0x0004F100
		public override decimal GetDecimal(int ordinal)
		{
			throw ADP.OleDb();
		}

		/// <summary>Gets the value of the specified column as a double-precision floating-point number.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="ordinal">The zero-based column ordinal. </param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000F65 RID: 3941 RVA: 0x00050D50 File Offset: 0x0004EF50
		public override double GetDouble(int ordinal)
		{
			throw ADP.OleDb();
		}

		/// <summary>Returns an <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the rows in the data reader.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the rows in the data reader.</returns>
		// Token: 0x06000F66 RID: 3942 RVA: 0x00050D50 File Offset: 0x0004EF50
		public override IEnumerator GetEnumerator()
		{
			throw ADP.OleDb();
		}

		/// <summary>Gets the <see cref="T:System.Type" /> that is the data type of the object.</summary>
		/// <returns>The <see cref="T:System.Type" /> that is the data type of the object.</returns>
		/// <param name="index">The zero-based column ordinal. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000F67 RID: 3943 RVA: 0x00050D50 File Offset: 0x0004EF50
		public override Type GetFieldType(int index)
		{
			throw ADP.OleDb();
		}

		/// <summary>Gets the value of the specified column as a single-precision floating-point number.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="ordinal">The zero-based column ordinal. </param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000F68 RID: 3944 RVA: 0x00050D50 File Offset: 0x0004EF50
		public override float GetFloat(int ordinal)
		{
			throw ADP.OleDb();
		}

		/// <summary>Gets the value of the specified column as a globally unique identifier (GUID).</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="ordinal">The zero-based column ordinal. </param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000F69 RID: 3945 RVA: 0x00050F14 File Offset: 0x0004F114
		public override Guid GetGuid(int ordinal)
		{
			throw ADP.OleDb();
		}

		/// <summary>Gets the value of the specified column as a 16-bit signed integer.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="ordinal">The zero-based column ordinal. </param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000F6A RID: 3946 RVA: 0x00050D50 File Offset: 0x0004EF50
		public override short GetInt16(int ordinal)
		{
			throw ADP.OleDb();
		}

		/// <summary>Gets the value of the specified column as a 32-bit signed integer.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="ordinal">The zero-based column ordinal. </param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000F6B RID: 3947 RVA: 0x00050D50 File Offset: 0x0004EF50
		public override int GetInt32(int ordinal)
		{
			throw ADP.OleDb();
		}

		/// <summary>Gets the value of the specified column as a 64-bit signed integer.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="ordinal">The zero-based column ordinal. </param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000F6C RID: 3948 RVA: 0x00050D50 File Offset: 0x0004EF50
		public override long GetInt64(int ordinal)
		{
			throw ADP.OleDb();
		}

		/// <summary>Gets the name of the specified column.</summary>
		/// <returns>The name of the specified column.</returns>
		/// <param name="index">The zero-based column ordinal. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000F6D RID: 3949 RVA: 0x00050D50 File Offset: 0x0004EF50
		public override string GetName(int index)
		{
			throw ADP.OleDb();
		}

		/// <summary>Gets the column ordinal, given the name of the column.</summary>
		/// <returns>The zero-based column ordinal.</returns>
		/// <param name="name">The name of the column. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">The name specified is not a valid column name. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000F6E RID: 3950 RVA: 0x00050D50 File Offset: 0x0004EF50
		public override int GetOrdinal(string name)
		{
			throw ADP.OleDb();
		}

		/// <summary>Returns a <see cref="T:System.Data.DataTable" /> that describes the column metadata of the <see cref="T:System.Data.OleDb.OleDbDataReader" />.</summary>
		/// <returns>A <see cref="T:System.Data.DataTable" /> that describes the column metadata.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Data.OleDb.OleDbDataReader" /> is closed. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000F6F RID: 3951 RVA: 0x00050D50 File Offset: 0x0004EF50
		public override DataTable GetSchemaTable()
		{
			throw ADP.OleDb();
		}

		/// <summary>Gets the value of the specified column as a string.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="ordinal">The zero-based column ordinal. </param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000F70 RID: 3952 RVA: 0x00050D50 File Offset: 0x0004EF50
		public override string GetString(int ordinal)
		{
			throw ADP.OleDb();
		}

		/// <summary>Gets the value of the specified column as a <see cref="T:System.TimeSpan" /> object.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="ordinal">The zero-based column ordinal. </param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000F71 RID: 3953 RVA: 0x00050F28 File Offset: 0x0004F128
		public TimeSpan GetTimeSpan(int ordinal)
		{
			throw ADP.OleDb();
		}

		/// <summary>Gets the value of the column at the specified ordinal in its native format.</summary>
		/// <returns>The value to return.</returns>
		/// <param name="ordinal">The zero-based column ordinal. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000F72 RID: 3954 RVA: 0x00050D50 File Offset: 0x0004EF50
		public override object GetValue(int ordinal)
		{
			throw ADP.OleDb();
		}

		/// <summary>Populates an array of objects with the column values of the current row.</summary>
		/// <returns>The number of instances of <see cref="T:System.Object" /> in the array.</returns>
		/// <param name="values">An array of <see cref="T:System.Object" /> into which to copy the attribute columns. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000F73 RID: 3955 RVA: 0x00050D50 File Offset: 0x0004EF50
		public override int GetValues(object[] values)
		{
			throw ADP.OleDb();
		}

		/// <summary>Gets a value that indicates whether the column contains nonexistent or missing values.</summary>
		/// <returns>true if the specified column value is equivalent to <see cref="T:System.DBNull" />; otherwise, false.</returns>
		/// <param name="ordinal">The zero-based column ordinal. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000F74 RID: 3956 RVA: 0x00050D50 File Offset: 0x0004EF50
		public override bool IsDBNull(int ordinal)
		{
			throw ADP.OleDb();
		}

		/// <summary>Advances the data reader to the next result, when reading the results of batch SQL statements.</summary>
		/// <returns>true if there are more result sets; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000F75 RID: 3957 RVA: 0x00050D50 File Offset: 0x0004EF50
		public override bool NextResult()
		{
			throw ADP.OleDb();
		}

		/// <summary>Advances the <see cref="T:System.Data.OleDb.OleDbDataReader" /> to the next record.</summary>
		/// <returns>true if there are more rows; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000F76 RID: 3958 RVA: 0x00050D50 File Offset: 0x0004EF50
		public override bool Read()
		{
			throw ADP.OleDb();
		}

		/// <summary>Gets the value of the specified column in its native format given the column ordinal.</summary>
		/// <returns>The value of the specified column in its native format.</returns>
		/// <param name="index">The column ordinal. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount" />. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700029A RID: 666
		public override object this[int index]
		{
			get
			{
				throw ADP.OleDb();
			}
		}

		/// <summary>Gets the value of the specified column in its native format given the column name.</summary>
		/// <returns>The value of the specified column in its native format.</returns>
		/// <param name="name">The column name. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">No column with the specified name was found. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700029B RID: 667
		public override object this[string name]
		{
			get
			{
				throw ADP.OleDb();
			}
		}
	}
}
