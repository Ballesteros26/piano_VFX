using System;
using System.ComponentModel;
using System.Data.Common;
using System.Security;
using System.Security.Permissions;

namespace System.Data.OleDb
{
	/// <summary>Associates a security action with a custom security attribute.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000120 RID: 288
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public sealed class OleDbPermissionAttribute : DBDataPermissionAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.OleDb.OleDbPermissionAttribute" /> class.</summary>
		/// <param name="action">One of the <see cref="T:System.Security.Permissions.SecurityAction" /> values representing an action that can be performed by using declarative security. </param>
		// Token: 0x06000EC2 RID: 3778 RVA: 0x00050D02 File Offset: 0x0004EF02
		public OleDbPermissionAttribute(SecurityAction action)
			: base(action)
		{
		}

		/// <summary>Gets or sets a comma-delimited string that contains a list of supported providers.</summary>
		/// <returns>A comma-delimited list of providers allowed by the security policy.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000271 RID: 625
		// (get) Token: 0x06000EC3 RID: 3779 RVA: 0x00050D0C File Offset: 0x0004EF0C
		// (set) Token: 0x06000EC4 RID: 3780 RVA: 0x00050D2A File Offset: 0x0004EF2A
		[Obsolete("Provider property has been deprecated.  Use the Add method.  http://go.microsoft.com/fwlink/?linkid=14202")]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public string Provider
		{
			get
			{
				string providers = this._providers;
				if (providers == null)
				{
					return ADP.StrEmpty;
				}
				return providers;
			}
			set
			{
				this._providers = value;
			}
		}

		/// <summary>Returns an <see cref="T:System.Data.OleDb.OleDbPermission" /> object that is configured according to the attribute properties.</summary>
		/// <returns>An <see cref="T:System.Data.OleDb.OleDbPermission" /> object.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000EC5 RID: 3781 RVA: 0x00050D33 File Offset: 0x0004EF33
		public override IPermission CreatePermission()
		{
			return new OleDbPermission(this);
		}

		// Token: 0x04000A02 RID: 2562
		private string _providers;
	}
}
