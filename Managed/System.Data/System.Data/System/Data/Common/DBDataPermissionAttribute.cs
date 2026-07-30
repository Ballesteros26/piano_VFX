using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Data.Common
{
	/// <summary>Associates a security action with a custom security attribute. </summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000388 RID: 904
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public abstract class DBDataPermissionAttribute : CodeAccessSecurityAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Common.DBDataPermissionAttribute" />.</summary>
		/// <param name="action">One of the security action values representing an action that can be performed by declarative security.</param>
		// Token: 0x06002AC2 RID: 10946 RVA: 0x000BDC24 File Offset: 0x000BBE24
		protected DBDataPermissionAttribute(SecurityAction action)
			: base(action)
		{
		}

		/// <summary>Gets or sets a value indicating whether a blank password is allowed.</summary>
		/// <returns>true if a blank password is allowed; otherwise false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000724 RID: 1828
		// (get) Token: 0x06002AC3 RID: 10947 RVA: 0x000BDC2D File Offset: 0x000BBE2D
		// (set) Token: 0x06002AC4 RID: 10948 RVA: 0x000BDC35 File Offset: 0x000BBE35
		public bool AllowBlankPassword
		{
			get
			{
				return this._allowBlankPassword;
			}
			set
			{
				this._allowBlankPassword = value;
			}
		}

		/// <summary>Gets or sets a permitted connection string.</summary>
		/// <returns>A permitted connection string.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000725 RID: 1829
		// (get) Token: 0x06002AC5 RID: 10949 RVA: 0x000BDC40 File Offset: 0x000BBE40
		// (set) Token: 0x06002AC6 RID: 10950 RVA: 0x000BDC5E File Offset: 0x000BBE5E
		public string ConnectionString
		{
			get
			{
				string connectionString = this._connectionString;
				if (connectionString == null)
				{
					return string.Empty;
				}
				return connectionString;
			}
			set
			{
				this._connectionString = value;
			}
		}

		/// <summary>Identifies whether the list of connection string parameters identified by the <see cref="P:System.Data.Common.DBDataPermissionAttribute.KeyRestrictions" /> property are the only connection string parameters allowed.</summary>
		/// <returns>One of the <see cref="T:System.Data.KeyRestrictionBehavior" /> values.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000726 RID: 1830
		// (get) Token: 0x06002AC7 RID: 10951 RVA: 0x000BDC67 File Offset: 0x000BBE67
		// (set) Token: 0x06002AC8 RID: 10952 RVA: 0x000BDC6F File Offset: 0x000BBE6F
		public KeyRestrictionBehavior KeyRestrictionBehavior
		{
			get
			{
				return this._behavior;
			}
			set
			{
				if (value <= KeyRestrictionBehavior.PreventUsage)
				{
					this._behavior = value;
					return;
				}
				throw ADP.InvalidKeyRestrictionBehavior(value);
			}
		}

		/// <summary>Gets or sets connection string parameters that are allowed or disallowed.</summary>
		/// <returns>One or more connection string parameters that are allowed or disallowed.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000727 RID: 1831
		// (get) Token: 0x06002AC9 RID: 10953 RVA: 0x000BDC84 File Offset: 0x000BBE84
		// (set) Token: 0x06002ACA RID: 10954 RVA: 0x000BDCA2 File Offset: 0x000BBEA2
		public string KeyRestrictions
		{
			get
			{
				string restrictions = this._restrictions;
				if (restrictions == null)
				{
					return ADP.StrEmpty;
				}
				return restrictions;
			}
			set
			{
				this._restrictions = value;
			}
		}

		/// <summary>Identifies whether the attribute should serialize the connection string.</summary>
		/// <returns>true if the attribute should serialize the connection string; otherwise false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06002ACB RID: 10955 RVA: 0x000BDCAB File Offset: 0x000BBEAB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeConnectionString()
		{
			return this._connectionString != null;
		}

		/// <summary>Identifies whether the attribute should serialize the set of key restrictions.</summary>
		/// <returns>true if the attribute should serialize the set of key restrictions; otherwise false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06002ACC RID: 10956 RVA: 0x000BDCB6 File Offset: 0x000BBEB6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeKeyRestrictions()
		{
			return this._restrictions != null;
		}

		// Token: 0x040019F4 RID: 6644
		private bool _allowBlankPassword;

		// Token: 0x040019F5 RID: 6645
		private string _connectionString;

		// Token: 0x040019F6 RID: 6646
		private string _restrictions;

		// Token: 0x040019F7 RID: 6647
		private KeyRestrictionBehavior _behavior;
	}
}
