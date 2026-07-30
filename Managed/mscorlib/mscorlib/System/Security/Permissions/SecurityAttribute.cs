using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	/// <summary>Specifies the base attribute class for declarative security from which <see cref="T:System.Security.Permissions.CodeAccessSecurityAttribute" /> is derived.</summary>
	// Token: 0x020005B0 RID: 1456
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[ComVisible(true)]
	[Serializable]
	public abstract class SecurityAttribute : Attribute
	{
		/// <summary>Initializes a new instance of <see cref="T:System.Security.Permissions.SecurityAttribute" /> with the specified <see cref="T:System.Security.Permissions.SecurityAction" />.</summary>
		/// <param name="action">One of the <see cref="T:System.Security.Permissions.SecurityAction" /> values. </param>
		// Token: 0x060040BF RID: 16575 RVA: 0x000E6B63 File Offset: 0x000E4D63
		protected SecurityAttribute(SecurityAction action)
		{
			this.Action = action;
		}

		/// <summary>When overridden in a derived class, creates a permission object that can then be serialized into binary form and persistently stored along with the <see cref="T:System.Security.Permissions.SecurityAction" /> in an assembly's metadata.</summary>
		/// <returns>A serializable permission object.</returns>
		// Token: 0x060040C0 RID: 16576
		public abstract IPermission CreatePermission();

		/// <summary>Gets or sets a value indicating whether full (unrestricted) permission to the resource protected by the attribute is declared.</summary>
		/// <returns>true if full permission to the protected resource is declared; otherwise, false.</returns>
		// Token: 0x17000AB5 RID: 2741
		// (get) Token: 0x060040C1 RID: 16577 RVA: 0x000E6B72 File Offset: 0x000E4D72
		// (set) Token: 0x060040C2 RID: 16578 RVA: 0x000E6B7A File Offset: 0x000E4D7A
		public bool Unrestricted
		{
			get
			{
				return this.m_Unrestricted;
			}
			set
			{
				this.m_Unrestricted = value;
			}
		}

		/// <summary>Gets or sets a security action.</summary>
		/// <returns>One of the <see cref="T:System.Security.Permissions.SecurityAction" /> values.</returns>
		// Token: 0x17000AB6 RID: 2742
		// (get) Token: 0x060040C3 RID: 16579 RVA: 0x000E6B83 File Offset: 0x000E4D83
		// (set) Token: 0x060040C4 RID: 16580 RVA: 0x000E6B8B File Offset: 0x000E4D8B
		public SecurityAction Action
		{
			get
			{
				return this.m_Action;
			}
			set
			{
				this.m_Action = value;
			}
		}

		// Token: 0x040020D4 RID: 8404
		private SecurityAction m_Action;

		// Token: 0x040020D5 RID: 8405
		private bool m_Unrestricted;
	}
}
