using System;
using System.Data.Common;
using System.Security;
using System.Security.Permissions;

namespace System.Data.SqlClient
{
	/// <summary>Associates a security action with a custom security attribute.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000238 RID: 568
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public sealed class SqlClientPermissionAttribute : DBDataPermissionAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlClient.SqlClientPermissionAttribute" /> class. </summary>
		/// <param name="action">One of the <see cref="T:System.Security.Permissions.SecurityAction" /> values representing an action that can be performed by using declarative security. </param>
		// Token: 0x060019B7 RID: 6583 RVA: 0x00050D02 File Offset: 0x0004EF02
		public SqlClientPermissionAttribute(SecurityAction action)
			: base(action)
		{
		}

		/// <summary>Returns a <see cref="T:System.Data.SqlClient.SqlClientPermission" /> object that is configured according to the attribute properties.</summary>
		/// <returns>A <see cref="T:System.Data.SqlClient.SqlClientPermission" /> object.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060019B8 RID: 6584 RVA: 0x00082CBB File Offset: 0x00080EBB
		public override IPermission CreatePermission()
		{
			return new SqlClientPermission(this);
		}
	}
}
