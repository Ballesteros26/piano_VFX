using System;
using Unity;

namespace System.Security.Permissions
{
	/// <summary>Determines the permission flags that apply to a <see cref="T:System.ComponentModel.TypeDescriptor" />.</summary>
	// Token: 0x020007D5 RID: 2005
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public sealed class TypeDescriptorPermissionAttribute : CodeAccessSecurityAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.TypeDescriptorPermissionAttribute" /> class with the specified <see cref="T:System.Security.Permissions.SecurityAction" />. </summary>
		/// <param name="action">One of the <see cref="T:System.Security.Permissions.SecurityAction" /> values. </param>
		// Token: 0x06004021 RID: 16417 RVA: 0x000027E8 File Offset: 0x000009E8
		public TypeDescriptorPermissionAttribute(SecurityAction action)
		{
		}

		/// <summary>Gets or sets the <see cref="T:System.Security.Permissions.TypeDescriptorPermissionFlags" /> for the <see cref="T:System.ComponentModel.TypeDescriptor" />. </summary>
		/// <returns>The <see cref="T:System.Security.Permissions.TypeDescriptorPermissionFlags" /> for the <see cref="T:System.ComponentModel.TypeDescriptor" />.</returns>
		// Token: 0x17000F7F RID: 3967
		// (get) Token: 0x06004022 RID: 16418 RVA: 0x000E0DD4 File Offset: 0x000DEFD4
		// (set) Token: 0x06004023 RID: 16419 RVA: 0x0000F0CE File Offset: 0x0000D2CE
		public TypeDescriptorPermissionFlags Flags
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return TypeDescriptorPermissionFlags.NoFlags;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets a value that indicates whether the type descriptor can be accessed from partial trust. </summary>
		/// <returns>true if the type descriptor can be accessed from partial trust; otherwise, false. </returns>
		// Token: 0x17000F80 RID: 3968
		// (get) Token: 0x06004024 RID: 16420 RVA: 0x000E0DF0 File Offset: 0x000DEFF0
		// (set) Token: 0x06004025 RID: 16421 RVA: 0x0000F0CE File Offset: 0x0000D2CE
		public bool RestrictedRegistrationAccess
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <returns>A serializable permission object.</returns>
		// Token: 0x06004026 RID: 16422 RVA: 0x0003D2D0 File Offset: 0x0003B4D0
		public override IPermission CreatePermission()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}
	}
}
