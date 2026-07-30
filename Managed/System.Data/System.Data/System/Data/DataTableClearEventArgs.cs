using System;

namespace System.Data
{
	/// <summary>Provides data for the <see cref="M:System.Data.DataTable.Clear" /> method.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200008F RID: 143
	public sealed class DataTableClearEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DataTableClearEventArgs" /> class.</summary>
		/// <param name="dataTable">The <see cref="T:System.Data.DataTable" /> whose rows are being cleared.</param>
		// Token: 0x0600088A RID: 2186 RVA: 0x00027B1B File Offset: 0x00025D1B
		public DataTableClearEventArgs(DataTable dataTable)
		{
			this.Table = dataTable;
		}

		/// <summary>Gets the table whose rows are being cleared.</summary>
		/// <returns>The <see cref="T:System.Data.DataTable" /> whose rows are being cleared.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000181 RID: 385
		// (get) Token: 0x0600088B RID: 2187 RVA: 0x00027B2A File Offset: 0x00025D2A
		public DataTable Table { get; }

		/// <summary>Gets the table name whose rows are being cleared.</summary>
		/// <returns>A <see cref="T:System.String" /> indicating the table name.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000182 RID: 386
		// (get) Token: 0x0600088C RID: 2188 RVA: 0x00027B32 File Offset: 0x00025D32
		public string TableName
		{
			get
			{
				return this.Table.TableName;
			}
		}

		/// <summary>Gets the namespace of the table whose rows are being cleared.</summary>
		/// <returns>A <see cref="T:System.String" /> indicating the namespace name.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000183 RID: 387
		// (get) Token: 0x0600088D RID: 2189 RVA: 0x00027B3F File Offset: 0x00025D3F
		public string TableNamespace
		{
			get
			{
				return this.Table.Namespace;
			}
		}
	}
}
