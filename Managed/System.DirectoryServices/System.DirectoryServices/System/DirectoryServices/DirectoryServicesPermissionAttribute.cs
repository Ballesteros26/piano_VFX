using System;
using System.Security;
using System.Security.Permissions;

namespace System.DirectoryServices
{
	/// <summary>Allows declarative <see cref="N:System.DirectoryServices" /> permission checks.          </summary>
	// Token: 0x02000019 RID: 25
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Event, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public class DirectoryServicesPermissionAttribute : CodeAccessSecurityAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="M:System.DirectoryServices.DirectoryServicesPermissionAttribute.#ctor(System.Security.Permissions.SecurityAction)" /> class.          </summary>
		/// <param name="action">One of the <see cref="T:System.Security.Permissions.SecurityAction" /> values.</param>
		// Token: 0x060000E0 RID: 224 RVA: 0x00003DC8 File Offset: 0x00001FC8
		public DirectoryServicesPermissionAttribute(SecurityAction action)
			: base(action)
		{
			this.path = "*";
			this.access = DirectoryServicesPermissionAccess.Browse;
		}

		/// <summary>Gets or sets a path to an Active Directory Domain Services node to which the permissions apply.          </summary>
		/// <returns>The path to an Active Directory Domain Services node. The default is "*".</returns>
		/// <exception cref="T:System.ArgumentNullException">The property value is a null reference (Nothing in Visual Basic).</exception>
		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000E1 RID: 225 RVA: 0x00003DE3 File Offset: 0x00001FE3
		// (set) Token: 0x060000E2 RID: 226 RVA: 0x00003DEB File Offset: 0x00001FEB
		public string Path
		{
			get
			{
				return this.path;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("Path");
				}
				this.path = value;
			}
		}

		/// <summary>Gets or sets the access levels that are used in creating permissions.          </summary>
		/// <returns>One of the <see cref="T:System.DirectoryServices.DirectoryServicesPermissionAccess" /> values. The default is Browse.</returns>
		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000E3 RID: 227 RVA: 0x00003E02 File Offset: 0x00002002
		// (set) Token: 0x060000E4 RID: 228 RVA: 0x00003E0A File Offset: 0x0000200A
		public DirectoryServicesPermissionAccess PermissionAccess
		{
			get
			{
				return this.access;
			}
			set
			{
				this.access = value;
			}
		}

		/// <summary>Creates permissions based on the attribute's specifications.          </summary>
		/// <returns>An <see cref="T:System.Security.IPermission" /> that represents the created permission.</returns>
		// Token: 0x060000E5 RID: 229 RVA: 0x00003E13 File Offset: 0x00002013
		public override IPermission CreatePermission()
		{
			if (base.Unrestricted)
			{
				return new DirectoryServicesPermission(PermissionState.Unrestricted);
			}
			return new DirectoryServicesPermission(this.access, this.path);
		}

		// Token: 0x04000083 RID: 131
		private string path;

		// Token: 0x04000084 RID: 132
		private DirectoryServicesPermissionAccess access;
	}
}
