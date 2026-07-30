using System;
using System.Data.Common;
using System.Security;
using System.Security.Permissions;

namespace System.Data.SqlClient
{
	/// <summary>Enables the .NET Framework Data Provider for SQL Server to help make sure that a user has a security level sufficient to access a data source. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000237 RID: 567
	[Serializable]
	public sealed class SqlClientPermission : DBDataPermission
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlClient.SqlClientPermission" /> class.</summary>
		// Token: 0x060019AF RID: 6575 RVA: 0x00082C75 File Offset: 0x00080E75
		[Obsolete("SqlClientPermission() has been deprecated.  Use the SqlClientPermission(PermissionState.None) constructor.  http://go.microsoft.com/fwlink/?linkid=14202", true)]
		public SqlClientPermission()
			: this(PermissionState.None)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlClient.SqlClientPermission" /> class.</summary>
		/// <param name="state">One of the <see cref="T:System.Security.Permissions.PermissionState" /> values. </param>
		// Token: 0x060019B0 RID: 6576 RVA: 0x00050C1B File Offset: 0x0004EE1B
		public SqlClientPermission(PermissionState state)
			: base(state)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlClient.SqlClientPermission" /> class.</summary>
		/// <param name="state">One of the <see cref="T:System.Security.Permissions.PermissionState" /> values. </param>
		/// <param name="allowBlankPassword">Indicates whether a blank password is allowed. </param>
		// Token: 0x060019B1 RID: 6577 RVA: 0x00082C7E File Offset: 0x00080E7E
		[Obsolete("SqlClientPermission(PermissionState state, Boolean allowBlankPassword) has been deprecated.  Use the SqlClientPermission(PermissionState.None) constructor.  http://go.microsoft.com/fwlink/?linkid=14202", true)]
		public SqlClientPermission(PermissionState state, bool allowBlankPassword)
			: this(state)
		{
			base.AllowBlankPassword = allowBlankPassword;
		}

		// Token: 0x060019B2 RID: 6578 RVA: 0x00050C34 File Offset: 0x0004EE34
		private SqlClientPermission(SqlClientPermission permission)
			: base(permission)
		{
		}

		// Token: 0x060019B3 RID: 6579 RVA: 0x00050C3D File Offset: 0x0004EE3D
		internal SqlClientPermission(SqlClientPermissionAttribute permissionAttribute)
			: base(permissionAttribute)
		{
		}

		// Token: 0x060019B4 RID: 6580 RVA: 0x00050C46 File Offset: 0x0004EE46
		internal SqlClientPermission(SqlConnectionString constr)
			: base(constr)
		{
			if (constr == null || constr.IsEmpty)
			{
				base.Add(ADP.StrEmpty, ADP.StrEmpty, KeyRestrictionBehavior.AllowOnly);
			}
		}

		/// <summary>Adds a new connection string and a set of restricted keywords to the <see cref="T:System.Data.SqlClient.SqlClientPermission" /> object.</summary>
		/// <param name="connectionString">The connection string.</param>
		/// <param name="restrictions">The key restrictions.</param>
		/// <param name="behavior">One of the <see cref="T:System.Data.KeyRestrictionBehavior" /> enumerations.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060019B5 RID: 6581 RVA: 0x00082C90 File Offset: 0x00080E90
		public override void Add(string connectionString, string restrictions, KeyRestrictionBehavior behavior)
		{
			DBConnectionString dbconnectionString = new DBConnectionString(connectionString, restrictions, behavior, SqlConnectionString.GetParseSynonyms(), false);
			base.AddPermissionEntry(dbconnectionString);
		}

		/// <summary>Returns the <see cref="T:System.Data.SqlClient.SqlClientPermission" /> as an <see cref="T:System.Security.IPermission" />.</summary>
		/// <returns>A copy of the current permission object.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060019B6 RID: 6582 RVA: 0x00082CB3 File Offset: 0x00080EB3
		public override IPermission Copy()
		{
			return new SqlClientPermission(this);
		}
	}
}
