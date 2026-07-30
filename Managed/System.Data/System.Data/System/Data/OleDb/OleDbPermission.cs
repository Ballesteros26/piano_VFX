using System;
using System.ComponentModel;
using System.Data.Common;
using System.Security;
using System.Security.Permissions;

namespace System.Data.OleDb
{
	/// <summary>Enables the .NET Framework Data Provider for OLE DB to help make sure that a user has a security level sufficient to access an OLE DB data source.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200011F RID: 287
	[Serializable]
	public sealed class OleDbPermission : DBDataPermission
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.OleDb.OleDbPermission" /> class.</summary>
		// Token: 0x06000EB9 RID: 3769 RVA: 0x00050C12 File Offset: 0x0004EE12
		[Obsolete("OleDbPermission() has been deprecated.  Use the OleDbPermission(PermissionState.None) constructor.  http://go.microsoft.com/fwlink/?linkid=14202", true)]
		public OleDbPermission()
			: this(PermissionState.None)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.OleDb.OleDbPermission" /> class.</summary>
		/// <param name="state">One of the <see cref="T:System.Security.Permissions.PermissionState" /> values. </param>
		// Token: 0x06000EBA RID: 3770 RVA: 0x00050C1B File Offset: 0x0004EE1B
		public OleDbPermission(PermissionState state)
			: base(state)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.OleDb.OleDbPermission" /> class.</summary>
		/// <param name="state">One of the <see cref="T:System.Security.Permissions.PermissionState" /> values. </param>
		/// <param name="allowBlankPassword">Indicates whether a blank password is allowed. </param>
		// Token: 0x06000EBB RID: 3771 RVA: 0x00050C24 File Offset: 0x0004EE24
		[Obsolete("OleDbPermission(PermissionState state, Boolean allowBlankPassword) has been deprecated.  Use the OleDbPermission(PermissionState.None) constructor.  http://go.microsoft.com/fwlink/?linkid=14202", true)]
		public OleDbPermission(PermissionState state, bool allowBlankPassword)
			: this(state)
		{
			base.AllowBlankPassword = allowBlankPassword;
		}

		// Token: 0x06000EBC RID: 3772 RVA: 0x00050C34 File Offset: 0x0004EE34
		private OleDbPermission(OleDbPermission permission)
			: base(permission)
		{
		}

		// Token: 0x06000EBD RID: 3773 RVA: 0x00050C3D File Offset: 0x0004EE3D
		internal OleDbPermission(OleDbPermissionAttribute permissionAttribute)
			: base(permissionAttribute)
		{
		}

		// Token: 0x06000EBE RID: 3774 RVA: 0x00050C46 File Offset: 0x0004EE46
		internal OleDbPermission(OleDbConnectionString constr)
			: base(constr)
		{
			if (constr == null || constr.IsEmpty)
			{
				base.Add(ADP.StrEmpty, ADP.StrEmpty, KeyRestrictionBehavior.AllowOnly);
			}
		}

		/// <summary>This property has been marked as obsolete. Setting this property will have no effect.</summary>
		/// <returns>This property has been marked as obsolete. Setting this property will have no effect.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000270 RID: 624
		// (get) Token: 0x06000EBF RID: 3775 RVA: 0x00050C6C File Offset: 0x0004EE6C
		// (set) Token: 0x06000EC0 RID: 3776 RVA: 0x00050CBC File Offset: 0x0004EEBC
		[Obsolete("Provider property has been deprecated.  Use the Add method.  http://go.microsoft.com/fwlink/?linkid=14202")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		public string Provider
		{
			get
			{
				string text = this._providers;
				if (text == null)
				{
					string[] providerRestriction = this._providerRestriction;
					if (providerRestriction != null && providerRestriction.Length != 0)
					{
						text = providerRestriction[0];
						for (int i = 1; i < providerRestriction.Length; i++)
						{
							text = text + ";" + providerRestriction[i];
						}
					}
				}
				if (text == null)
				{
					return ADP.StrEmpty;
				}
				return text;
			}
			set
			{
				string[] array = null;
				if (!ADP.IsEmpty(value))
				{
					array = value.Split(new char[] { ';' });
					array = DBConnectionString.RemoveDuplicates(array);
				}
				this._providerRestriction = array;
				this._providers = value;
			}
		}

		/// <summary>Returns the <see cref="T:System.Data.OleDb.OleDbPermission" /> as an <see cref="T:System.Security.IPermission" />.</summary>
		/// <returns>A copy of the current permission object.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06000EC1 RID: 3777 RVA: 0x00050CFA File Offset: 0x0004EEFA
		public override IPermission Copy()
		{
			return new OleDbPermission(this);
		}

		// Token: 0x04000A00 RID: 2560
		private string[] _providerRestriction;

		// Token: 0x04000A01 RID: 2561
		private string _providers;
	}
}
