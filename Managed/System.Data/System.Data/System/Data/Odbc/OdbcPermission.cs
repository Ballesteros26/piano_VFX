using System;
using System.Data.Common;
using System.Security;
using System.Security.Permissions;

namespace System.Data.Odbc
{
	/// <summary>Enables the .NET Framework Data Provider for ODBC to help make sure that a user has a security level sufficient to access an ODBC data source. This class cannot be inherited.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020002B7 RID: 695
	[Serializable]
	public sealed class OdbcPermission : DBDataPermission
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Odbc.OdbcPermission" /> class.</summary>
		// Token: 0x06001DA4 RID: 7588 RVA: 0x00092285 File Offset: 0x00090485
		[Obsolete("OdbcPermission() has been deprecated.  Use the OdbcPermission(PermissionState.None) constructor.  http://go.microsoft.com/fwlink/?linkid=14202", true)]
		public OdbcPermission()
			: this(PermissionState.None)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Odbc.OdbcPermission" /> class with one of the <see cref="T:System.Security.Permissions.PermissionState" /> values.</summary>
		/// <param name="state">One of the <see cref="T:System.Security.Permissions.PermissionState" /> values. </param>
		// Token: 0x06001DA5 RID: 7589 RVA: 0x00050C1B File Offset: 0x0004EE1B
		public OdbcPermission(PermissionState state)
			: base(state)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Odbc.OdbcPermission" /> class.</summary>
		/// <param name="state">One of the System.Security.Permissions.PermissionState values. </param>
		/// <param name="allowBlankPassword">Indicates whether a blank password is allowed. </param>
		// Token: 0x06001DA6 RID: 7590 RVA: 0x0009228E File Offset: 0x0009048E
		[Obsolete("OdbcPermission(PermissionState state, Boolean allowBlankPassword) has been deprecated.  Use the OdbcPermission(PermissionState.None) constructor.  http://go.microsoft.com/fwlink/?linkid=14202", true)]
		public OdbcPermission(PermissionState state, bool allowBlankPassword)
			: this(state)
		{
			base.AllowBlankPassword = allowBlankPassword;
		}

		// Token: 0x06001DA7 RID: 7591 RVA: 0x00050C34 File Offset: 0x0004EE34
		private OdbcPermission(OdbcPermission permission)
			: base(permission)
		{
		}

		// Token: 0x06001DA8 RID: 7592 RVA: 0x00050C3D File Offset: 0x0004EE3D
		internal OdbcPermission(OdbcPermissionAttribute permissionAttribute)
			: base(permissionAttribute)
		{
		}

		// Token: 0x06001DA9 RID: 7593 RVA: 0x00050C46 File Offset: 0x0004EE46
		internal OdbcPermission(OdbcConnectionString constr)
			: base(constr)
		{
			if (constr == null || constr.IsEmpty)
			{
				base.Add(ADP.StrEmpty, ADP.StrEmpty, KeyRestrictionBehavior.AllowOnly);
			}
		}

		/// <summary>Adds access for the specified connection string to the existing state of the permission.</summary>
		/// <param name="connectionString">A permitted connection string. </param>
		/// <param name="restrictions">String that identifies connection string parameters that are allowed or disallowed. </param>
		/// <param name="behavior">One of the <see cref="T:System.Data.KeyRestrictionBehavior" /> values. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001DAA RID: 7594 RVA: 0x000922A0 File Offset: 0x000904A0
		public override void Add(string connectionString, string restrictions, KeyRestrictionBehavior behavior)
		{
			DBConnectionString dbconnectionString = new DBConnectionString(connectionString, restrictions, behavior, null, true);
			base.AddPermissionEntry(dbconnectionString);
		}

		/// <summary>Returns the <see cref="T:System.Data.Odbc.OdbcPermission" /> as an <see cref="T:System.Security.IPermission" />.</summary>
		/// <returns>A copy of the current permission object.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06001DAB RID: 7595 RVA: 0x000922BF File Offset: 0x000904BF
		public override IPermission Copy()
		{
			return new OdbcPermission(this);
		}
	}
}
