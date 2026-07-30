using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	/// <summary>Allows security actions for <see cref="T:System.Security.Permissions.ReflectionPermission" /> to be applied to code using declarative security. </summary>
	// Token: 0x020005AA RID: 1450
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[ComVisible(true)]
	[Serializable]
	public sealed class ReflectionPermissionAttribute : CodeAccessSecurityAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.ReflectionPermissionAttribute" /> class with the specified <see cref="T:System.Security.Permissions.SecurityAction" />.</summary>
		/// <param name="action">One of the <see cref="T:System.Security.Permissions.SecurityAction" /> values. </param>
		// Token: 0x0600408D RID: 16525 RVA: 0x000E2D08 File Offset: 0x000E0F08
		public ReflectionPermissionAttribute(SecurityAction action)
			: base(action)
		{
		}

		/// <summary>Gets or sets the current allowed uses of reflection.</summary>
		/// <returns>One or more of the <see cref="T:System.Security.Permissions.ReflectionPermissionFlag" /> values combined using a bitwise OR.</returns>
		/// <exception cref="T:System.ArgumentException">An attempt is made to set this property to an invalid value. See <see cref="T:System.Security.Permissions.ReflectionPermissionFlag" /> for the valid values. </exception>
		// Token: 0x17000AA9 RID: 2729
		// (get) Token: 0x0600408E RID: 16526 RVA: 0x000E5FB0 File Offset: 0x000E41B0
		// (set) Token: 0x0600408F RID: 16527 RVA: 0x000E5FB8 File Offset: 0x000E41B8
		public ReflectionPermissionFlag Flags
		{
			get
			{
				return this.flags;
			}
			set
			{
				this.flags = value;
				this.memberAccess = (this.flags & ReflectionPermissionFlag.MemberAccess) == ReflectionPermissionFlag.MemberAccess;
				this.reflectionEmit = (this.flags & ReflectionPermissionFlag.ReflectionEmit) == ReflectionPermissionFlag.ReflectionEmit;
				this.typeInfo = (this.flags & ReflectionPermissionFlag.TypeInformation) == ReflectionPermissionFlag.TypeInformation;
			}
		}

		/// <summary>Gets or sets a value that indicates whether invocation of operations on non-public members is allowed.</summary>
		/// <returns>true if invocation of operations on non-public members is allowed; otherwise, false.</returns>
		// Token: 0x17000AAA RID: 2730
		// (get) Token: 0x06004090 RID: 16528 RVA: 0x000E5FF4 File Offset: 0x000E41F4
		// (set) Token: 0x06004091 RID: 16529 RVA: 0x000E5FFC File Offset: 0x000E41FC
		public bool MemberAccess
		{
			get
			{
				return this.memberAccess;
			}
			set
			{
				if (value)
				{
					this.flags |= ReflectionPermissionFlag.MemberAccess;
				}
				else
				{
					this.flags -= 2;
				}
				this.memberAccess = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether use of certain features in <see cref="N:System.Reflection.Emit" />, such as emitting debug symbols, is allowed.</summary>
		/// <returns>true if use of the affected features is allowed; otherwise, false.</returns>
		// Token: 0x17000AAB RID: 2731
		// (get) Token: 0x06004092 RID: 16530 RVA: 0x000E6026 File Offset: 0x000E4226
		// (set) Token: 0x06004093 RID: 16531 RVA: 0x000E602E File Offset: 0x000E422E
		[Obsolete]
		public bool ReflectionEmit
		{
			get
			{
				return this.reflectionEmit;
			}
			set
			{
				if (value)
				{
					this.flags |= ReflectionPermissionFlag.ReflectionEmit;
				}
				else
				{
					this.flags -= 4;
				}
				this.reflectionEmit = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether restricted invocation of non-public members is allowed. Restricted invocation means that the grant set of the assembly that contains the non-public member that is being invoked must be equal to, or a subset of, the grant set of the invoking assembly. </summary>
		/// <returns>true if restricted invocation of non-public members is allowed; otherwise, false.</returns>
		// Token: 0x17000AAC RID: 2732
		// (get) Token: 0x06004094 RID: 16532 RVA: 0x000E6058 File Offset: 0x000E4258
		// (set) Token: 0x06004095 RID: 16533 RVA: 0x000E6065 File Offset: 0x000E4265
		public bool RestrictedMemberAccess
		{
			get
			{
				return (this.flags & ReflectionPermissionFlag.RestrictedMemberAccess) == ReflectionPermissionFlag.RestrictedMemberAccess;
			}
			set
			{
				if (value)
				{
					this.flags |= ReflectionPermissionFlag.RestrictedMemberAccess;
					return;
				}
				this.flags -= 8;
			}
		}

		/// <summary>Gets or sets a value that indicates whether reflection on members that are not visible is allowed.</summary>
		/// <returns>true if reflection on members that are not visible is allowed; otherwise, false.</returns>
		// Token: 0x17000AAD RID: 2733
		// (get) Token: 0x06004096 RID: 16534 RVA: 0x000E6087 File Offset: 0x000E4287
		// (set) Token: 0x06004097 RID: 16535 RVA: 0x000E608F File Offset: 0x000E428F
		[Obsolete("not enforced in 2.0+")]
		public bool TypeInformation
		{
			get
			{
				return this.typeInfo;
			}
			set
			{
				if (value)
				{
					this.flags |= ReflectionPermissionFlag.TypeInformation;
				}
				else
				{
					this.flags--;
				}
				this.typeInfo = value;
			}
		}

		/// <summary>Creates and returns a new <see cref="T:System.Security.Permissions.ReflectionPermission" />.</summary>
		/// <returns>A <see cref="T:System.Security.Permissions.ReflectionPermission" /> that corresponds to this attribute.</returns>
		// Token: 0x06004098 RID: 16536 RVA: 0x000E60BC File Offset: 0x000E42BC
		public override IPermission CreatePermission()
		{
			ReflectionPermission reflectionPermission;
			if (base.Unrestricted)
			{
				reflectionPermission = new ReflectionPermission(PermissionState.Unrestricted);
			}
			else
			{
				reflectionPermission = new ReflectionPermission(this.flags);
			}
			return reflectionPermission;
		}

		// Token: 0x040020AF RID: 8367
		private ReflectionPermissionFlag flags;

		// Token: 0x040020B0 RID: 8368
		private bool memberAccess;

		// Token: 0x040020B1 RID: 8369
		private bool reflectionEmit;

		// Token: 0x040020B2 RID: 8370
		private bool typeInfo;
	}
}
