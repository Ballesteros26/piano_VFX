using System;
using System.Collections;
using System.ComponentModel;
using System.Data.Common;

namespace System.Data.OleDb
{
	/// <summary>Represents a collection of parameters relevant to an <see cref="T:System.Data.OleDb.OleDbCommand" /> as well as their respective mappings to columns in a <see cref="T:System.Data.DataSet" />. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000133 RID: 307
	[MonoTODO("OleDb is not implemented.")]
	public class OleDbParameterCollection : DbParameterCollection
	{
		// Token: 0x06000FC6 RID: 4038 RVA: 0x00050F6C File Offset: 0x0004F16C
		internal OleDbParameterCollection()
		{
		}

		/// <summary>Adds the specified <see cref="T:System.Data.OleDb.OleDbParameter" /> to the <see cref="T:System.Data.OleDb.OleDbParameterCollection" />.</summary>
		/// <returns>The index of the new <see cref="T:System.Data.OleDb.OleDbParameter" /> object.</returns>
		/// <param name="value">The <see cref="T:System.Data.OleDb.OleDbParameter" /> to add to the collection.</param>
		/// <exception cref="T:System.ArgumentException">The <see cref="T:System.Data.OleDb.OleDbParameter" /> specified in the <paramref name="value" /> parameter is already added to this or another <see cref="T:System.Data.OleDb.OleDbParameterCollection" />. </exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is null. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000FC7 RID: 4039 RVA: 0x00050D50 File Offset: 0x0004EF50
		public OleDbParameter Add(OleDbParameter value)
		{
			throw ADP.OleDb();
		}

		/// <summary>Adds the specified <see cref="T:System.Data.OleDb.OleDbParameter" /> object to the <see cref="T:System.Data.OleDb.OleDbParameterCollection" />.</summary>
		/// <returns>The index of the new <see cref="T:System.Data.OleDb.OleDbParameter" /> object in the collection.</returns>
		/// <param name="value">A <see cref="T:System.Object" />.</param>
		// Token: 0x06000FC8 RID: 4040 RVA: 0x00050D50 File Offset: 0x0004EF50
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int Add(object value)
		{
			throw ADP.OleDb();
		}

		/// <summary>Adds an <see cref="T:System.Data.OleDb.OleDbParameter" /> to the <see cref="T:System.Data.OleDb.OleDbParameterCollection" />, given the parameter name and data type.</summary>
		/// <returns>The index of the new <see cref="T:System.Data.OleDb.OleDbParameter" /> object.</returns>
		/// <param name="parameterName">The name of the parameter. </param>
		/// <param name="oleDbType">One of the <see cref="T:System.Data.OleDb.OleDbType" /> values. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000FC9 RID: 4041 RVA: 0x00050D50 File Offset: 0x0004EF50
		public OleDbParameter Add(string parameterName, OleDbType oleDbType)
		{
			throw ADP.OleDb();
		}

		/// <summary>Adds an <see cref="T:System.Data.OleDb.OleDbParameter" /> to the <see cref="T:System.Data.OleDb.OleDbParameterCollection" /> given the parameter name, data type, and column length.</summary>
		/// <returns>The index of the new <see cref="T:System.Data.OleDb.OleDbParameter" /> object.</returns>
		/// <param name="parameterName">The name of the parameter. </param>
		/// <param name="oleDbType">One of the <see cref="T:System.Data.OleDb.OleDbType" /> values. </param>
		/// <param name="size">The length of the column. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000FCA RID: 4042 RVA: 0x00050D50 File Offset: 0x0004EF50
		public OleDbParameter Add(string parameterName, OleDbType oleDbType, int size)
		{
			throw ADP.OleDb();
		}

		/// <summary>Adds an <see cref="T:System.Data.OleDb.OleDbParameter" /> to the <see cref="T:System.Data.OleDb.OleDbParameterCollection" /> given the parameter name, data type, column length, and source column name.</summary>
		/// <returns>The index of the new <see cref="T:System.Data.OleDb.OleDbParameter" /> object.</returns>
		/// <param name="parameterName">The name of the parameter. </param>
		/// <param name="oleDbType">One of the <see cref="T:System.Data.OleDb.OleDbType" /> values. </param>
		/// <param name="size">The length of the column. </param>
		/// <param name="sourceColumn">The name of the source column. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000FCB RID: 4043 RVA: 0x00050D50 File Offset: 0x0004EF50
		public OleDbParameter Add(string parameterName, OleDbType oleDbType, int size, string sourceColumn)
		{
			throw ADP.OleDb();
		}

		/// <summary>Adds an <see cref="T:System.Data.OleDb.OleDbParameter" /> to the <see cref="T:System.Data.OleDb.OleDbParameterCollection" /> given the parameter name and value.</summary>
		/// <returns>The index of the new <see cref="T:System.Data.OleDb.OleDbParameter" /> object.</returns>
		/// <param name="parameterName">The name of the parameter. </param>
		/// <param name="value">The <see cref="P:System.Data.OleDb.OleDbParameter.Value" /> of the <see cref="T:System.Data.OleDb.OleDbParameter" /> to add to the collection. </param>
		/// <exception cref="T:System.InvalidCastException">The <paramref name="value" /> parameter is not an <see cref="T:System.Data.OleDb.OleDbParameter" />. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000FCC RID: 4044 RVA: 0x00050D50 File Offset: 0x0004EF50
		public OleDbParameter Add(string parameterName, object value)
		{
			throw ADP.OleDb();
		}

		/// <summary>Adds an array of values to the end of the <see cref="T:System.Data.OleDb.OleDbParameterCollection" />.</summary>
		/// <param name="values">The <see cref="T:System.Array" /> values to add.</param>
		// Token: 0x06000FCD RID: 4045 RVA: 0x00050D50 File Offset: 0x0004EF50
		public override void AddRange(Array values)
		{
			throw ADP.OleDb();
		}

		/// <summary>Adds an array of <see cref="T:System.Data.OleDb.OleDbParameter" /> values to the end of the <see cref="T:System.Data.OleDb.OleDbParameterCollection" />.</summary>
		/// <param name="values">The <see cref="T:System.Data.OleDbParameter" /> values to add.</param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000FCE RID: 4046 RVA: 0x00050D50 File Offset: 0x0004EF50
		public void AddRange(OleDbParameter[] values)
		{
			throw ADP.OleDb();
		}

		/// <summary>Adds a value to the end of the <see cref="T:System.Data.OleDb.OleDbParameterCollection" />.</summary>
		/// <returns>An <see cref="T:System.Data.OleDb.OleDbParameter" /> object.</returns>
		/// <param name="parameterName">The name of the parameter.</param>
		/// <param name="value">The value to be added.</param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000FCF RID: 4047 RVA: 0x00050D50 File Offset: 0x0004EF50
		public OleDbParameter AddWithValue(string parameterName, object value)
		{
			throw ADP.OleDb();
		}

		/// <summary>Removes all <see cref="T:System.Data.OleDb.OleDbParameter" /> objects from the <see cref="T:System.Data.OleDb.OleDbParameterCollection" />.</summary>
		// Token: 0x06000FD0 RID: 4048 RVA: 0x00050D50 File Offset: 0x0004EF50
		public override void Clear()
		{
			throw ADP.OleDb();
		}

		/// <summary>Determines whether the specified <see cref="T:System.Data.OleDb.OleDbParameter" /> is in this <see cref="T:System.Data.OleDb.OleDbParameterCollection" />.</summary>
		/// <returns>true if the <see cref="P:System.Data.OleDb.OleDbParameter" /> is in the collection; otherwise false.</returns>
		/// <param name="value">The <see cref="T:System.Data.OleDb.OleDbParameter" /> value.</param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000FD1 RID: 4049 RVA: 0x00050D50 File Offset: 0x0004EF50
		public bool Contains(OleDbParameter value)
		{
			throw ADP.OleDb();
		}

		/// <summary>Determines whether the specified <see cref="T:System.Object" /> is in this <see cref="T:System.Data.OleDb.OleDbParameterCollection" />.</summary>
		/// <returns>true if the <see cref="T:System.Data.OleDb.OleDbParameterCollection" /> contains <paramref name="value" />; otherwise false.</returns>
		/// <param name="value">The <see cref="T:System.Object" /> value.</param>
		// Token: 0x06000FD2 RID: 4050 RVA: 0x00050D50 File Offset: 0x0004EF50
		public override bool Contains(object value)
		{
			throw ADP.OleDb();
		}

		/// <summary>Determines whether the specified <see cref="T:System.String" /> is in this <see cref="T:System.Data.OleDb.OleDbParameterCollection" />.</summary>
		/// <returns>true if the <see cref="T:System.Data.OleDb.OleDbParameterCollection" /> contains the value; otherwise false.</returns>
		/// <param name="value">The <see cref="T:System.String" /> value.</param>
		// Token: 0x06000FD3 RID: 4051 RVA: 0x00050D50 File Offset: 0x0004EF50
		public override bool Contains(string value)
		{
			throw ADP.OleDb();
		}

		/// <summary>Copies all the elements of the current <see cref="T:System.Data.OleDb.OleDbParameterCollection" /> to the specified one-dimensional <see cref="T:System.Array" /> starting at the specified destination <see cref="T:System.Array" /> index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from the current <see cref="T:System.Data.OleDb.OleDbParameterCollection" />.</param>
		/// <param name="index">A 32-bit integer that represents the index in the <see cref="T:System.Array" /> at which copying starts.</param>
		// Token: 0x06000FD4 RID: 4052 RVA: 0x00050D50 File Offset: 0x0004EF50
		public override void CopyTo(Array array, int index)
		{
			throw ADP.OleDb();
		}

		/// <summary>Copies all the elements of the current <see cref="T:System.Data.OleDb.OleDbParameterCollection" /> to the specified <see cref="T:System.Data.OleDb.OleDbParameterCollection" /> starting at the specified destination index.</summary>
		/// <param name="array">The <see cref="T:System.Data.OleDb.OleDbParameterCollection" /> that is the destination of the elements copied from the current <see cref="T:System.Data.OleDb.OleDbParameterCollection" />.</param>
		/// <param name="index">A 32-bit integer that represents the index in the <see cref="T:System.Data.OleDb.OleDbParameterCollection" /> at which copying starts.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000FD5 RID: 4053 RVA: 0x00050D50 File Offset: 0x0004EF50
		public void CopyTo(OleDbParameter[] array, int index)
		{
			throw ADP.OleDb();
		}

		/// <summary>Returns an enumerator that iterates through the <see cref="T:System.Data.OleDb.OleDbParameterCollection" />.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerator" /> for the <see cref="T:System.Data.OleDb.OleDbParameterCollection" />.</returns>
		// Token: 0x06000FD6 RID: 4054 RVA: 0x00050D50 File Offset: 0x0004EF50
		public override IEnumerator GetEnumerator()
		{
			throw ADP.OleDb();
		}

		// Token: 0x06000FD7 RID: 4055 RVA: 0x00050D50 File Offset: 0x0004EF50
		protected override DbParameter GetParameter(int index)
		{
			throw ADP.OleDb();
		}

		// Token: 0x06000FD8 RID: 4056 RVA: 0x00050D50 File Offset: 0x0004EF50
		protected override DbParameter GetParameter(string parameterName)
		{
			throw ADP.OleDb();
		}

		/// <summary>Gets the location of the specified <see cref="T:System.Data.OleDb.OleDbParameter" /> within the collection.</summary>
		/// <returns>The zero-based location of the specified <see cref="T:System.Data.OleDb.OleDbParameter" /> that is a <see cref="T:System.Data.OleDb.OleDbParameter" /> within the collection.</returns>
		/// <param name="value">The <see cref="T:System.Data.OleDb.OleDbParameter" /> object in the collection to find.</param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000FD9 RID: 4057 RVA: 0x00050D50 File Offset: 0x0004EF50
		public int IndexOf(OleDbParameter value)
		{
			throw ADP.OleDb();
		}

		/// <summary>The location of the specified <see cref="T:System.Object" /> within the collection.</summary>
		/// <returns>The zero-based location of the specified <see cref="T:System.Object" /> that is a <see cref="T:System.Data.OleDb.OleDbParameterCollection" /> within the collection.</returns>
		/// <param name="value">The <see cref="T:System.Object" /> to find.</param>
		// Token: 0x06000FDA RID: 4058 RVA: 0x00050D50 File Offset: 0x0004EF50
		public override int IndexOf(object value)
		{
			throw ADP.OleDb();
		}

		/// <summary>Gets the location of the specified <see cref="T:System.Data.OleDb.OleDbParameter" /> with the specified name.</summary>
		/// <returns>The zero-based location of the specified <see cref="T:System.Data.OleDb.OleDbParameter" /> with the specified case-sensitive name.</returns>
		/// <param name="parameterName">The case-sensitive name of the <see cref="T:System.Data.OleDb.OleDbParameter" /> to find.</param>
		// Token: 0x06000FDB RID: 4059 RVA: 0x00050D50 File Offset: 0x0004EF50
		public override int IndexOf(string parameterName)
		{
			throw ADP.OleDb();
		}

		/// <summary>Inserts a <see cref="T:System.Data.OleDb.OleDbParameter" /> object into the <see cref="T:System.Data.OleDb.OleDbParameterCollection" /> at the specified index.</summary>
		/// <param name="index">The zero-based index at which value should be inserted.</param>
		/// <param name="value">An <see cref="T:System.Data.OleDb.OleDbParameter" /> object to be inserted in the <see cref="T:System.Data.OleDb.OleDbParameterCollection" />.</param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000FDC RID: 4060 RVA: 0x00050D50 File Offset: 0x0004EF50
		public void Insert(int index, OleDbParameter value)
		{
			throw ADP.OleDb();
		}

		/// <summary>Inserts a <see cref="T:System.Object" /> into the <see cref="T:System.Data.OleDb.OleDbParameterCollection" /> at the specified index.</summary>
		/// <param name="index">The zero-based index at which value should be inserted.</param>
		/// <param name="value">A <see cref="T:System.Object" /> to be inserted in the <see cref="T:System.Data.OleDb.OleDbParameterCollection" />.</param>
		// Token: 0x06000FDD RID: 4061 RVA: 0x00050D50 File Offset: 0x0004EF50
		public override void Insert(int index, object value)
		{
			throw ADP.OleDb();
		}

		/// <summary>Removes the <see cref="T:System.Data.OleDb.OleDbParameter" /> from the <see cref="T:System.Data.OleDb.OleDbParameterCollection" />.</summary>
		/// <param name="value">An <see cref="T:System.Data.OleDb.OleDbParameter" /> object to remove from the collection.</param>
		/// <exception cref="T:System.InvalidCastException">The parameter is not a <see cref="T:System.Data.OleDb.OleDbParameter" />. </exception>
		/// <exception cref="T:System.SystemException">The parameter does not exist in the collection. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000FDE RID: 4062 RVA: 0x00050D50 File Offset: 0x0004EF50
		public void Remove(OleDbParameter value)
		{
			throw ADP.OleDb();
		}

		/// <summary>Removes the <see cref="T:System.Object" /> object from the <see cref="T:System.Data.OleDb.OleDbParameterCollection" />.</summary>
		/// <param name="value">An <see cref="T:System.Object" /> to be removed from the <see cref="T:System.Data.OleDb.OleDbParameterCollection" />.</param>
		// Token: 0x06000FDF RID: 4063 RVA: 0x00050D50 File Offset: 0x0004EF50
		public override void Remove(object value)
		{
			throw ADP.OleDb();
		}

		/// <summary>Removes the <see cref="T:System.Data.OleDb.OleDbParameter" /> from the <see cref="T:System.Data.OleDb.OleDbParameterCollection" /> at the specified index.</summary>
		/// <param name="index">The zero-based index of the <see cref="T:System.Data.OleDb.OleDbParameter" /> object to remove.</param>
		// Token: 0x06000FE0 RID: 4064 RVA: 0x00050D50 File Offset: 0x0004EF50
		public override void RemoveAt(int index)
		{
			throw ADP.OleDb();
		}

		/// <summary>Removes the <see cref="T:System.Data.OleDb.OleDbParameter" /> from the <see cref="T:System.Data.OleDb.OleDbParameterCollection" /> at the specified parameter name.</summary>
		/// <param name="parameterName">The name of the <see cref="T:System.Data.OleDb.OleDbParameter" /> object to remove.</param>
		// Token: 0x06000FE1 RID: 4065 RVA: 0x00050D50 File Offset: 0x0004EF50
		public override void RemoveAt(string parameterName)
		{
			throw ADP.OleDb();
		}

		// Token: 0x06000FE2 RID: 4066 RVA: 0x00050D50 File Offset: 0x0004EF50
		protected override void SetParameter(int index, DbParameter value)
		{
			throw ADP.OleDb();
		}

		// Token: 0x06000FE3 RID: 4067 RVA: 0x00050D50 File Offset: 0x0004EF50
		protected override void SetParameter(string parameterName, DbParameter value)
		{
			throw ADP.OleDb();
		}

		/// <summary>Returns an integer that contains the number of elements in the <see cref="T:System.Data.OleDb.OleDbParameterCollection" />. Read-only. </summary>
		/// <returns>The number of elements in the <see cref="T:System.Data.OleDb.OleDbParameterCollection" /> as an integer.</returns>
		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x06000FE4 RID: 4068 RVA: 0x00050D50 File Offset: 0x0004EF50
		public override int Count
		{
			get
			{
				throw ADP.OleDb();
			}
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Data.OleDb.OleDbParameterCollection" /> has a fixed size. Read-only.</summary>
		/// <returns>true if the <see cref="T:System.Data.OleDb.OleDbParameterCollection" /> has a fixed size; otherwise false.</returns>
		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x06000FE5 RID: 4069 RVA: 0x00050D50 File Offset: 0x0004EF50
		public override bool IsFixedSize
		{
			get
			{
				throw ADP.OleDb();
			}
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Data.OleDb.OleDbParameterCollection" /> is read-only.</summary>
		/// <returns>true if the <see cref="T:System.Data.OleDb.OleDbParameterCollection" /> is read only; otherwise false.</returns>
		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x06000FE6 RID: 4070 RVA: 0x00050D50 File Offset: 0x0004EF50
		public override bool IsReadOnly
		{
			get
			{
				throw ADP.OleDb();
			}
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Data.OleDb.OleDbParameterCollection" /> is synchronized. Read-only.</summary>
		/// <returns>true if the <see cref="T:System.Data.OleDb.OleDbParameterCollection" /> is synchronized; otherwise false.</returns>
		// Token: 0x170002BA RID: 698
		// (get) Token: 0x06000FE7 RID: 4071 RVA: 0x00050D50 File Offset: 0x0004EF50
		public override bool IsSynchronized
		{
			get
			{
				throw ADP.OleDb();
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Data.OleDb.OleDbParameter" /> at the specified index.</summary>
		/// <returns>The <see cref="T:System.Data.OleDb.OleDbParameter" /> at the specified index.</returns>
		/// <param name="index">The zero-based index of the parameter to retrieve. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">The index specified does not exist. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x170002BB RID: 699
		public OleDbParameter this[int index]
		{
			get
			{
				throw ADP.OleDb();
			}
			set
			{
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Data.OleDb.OleDbParameter" /> with the specified name.</summary>
		/// <returns>The <see cref="T:System.Data.OleDb.OleDbParameter" /> with the specified name.</returns>
		/// <param name="parameterName">The name of the parameter to retrieve. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">The name specified does not exist. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x170002BC RID: 700
		public OleDbParameter this[string parameterName]
		{
			get
			{
				throw ADP.OleDb();
			}
			set
			{
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Data.OleDb.OleDbParameterCollection" />. Read-only.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Data.OleDb.OleDbParameterCollection" />.</returns>
		// Token: 0x170002BD RID: 701
		// (get) Token: 0x06000FEC RID: 4076 RVA: 0x00050D50 File Offset: 0x0004EF50
		public override object SyncRoot
		{
			get
			{
				throw ADP.OleDb();
			}
		}
	}
}
