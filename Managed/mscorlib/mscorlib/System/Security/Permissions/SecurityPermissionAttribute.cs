using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	/// <summary>Allows security actions for <see cref="T:System.Security.Permissions.SecurityPermission" /> to be applied to code using declarative security. This class cannot be inherited.</summary>
	// Token: 0x020005B2 RID: 1458
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[ComVisible(true)]
	[Serializable]
	public sealed class SecurityPermissionAttribute : CodeAccessSecurityAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.SecurityPermissionAttribute" /> class with the specified <see cref="T:System.Security.Permissions.SecurityAction" />.</summary>
		/// <param name="action">One of the <see cref="T:System.Security.Permissions.SecurityAction" /> values. </param>
		// Token: 0x060040D3 RID: 16595 RVA: 0x000E6E03 File Offset: 0x000E5003
		public SecurityPermissionAttribute(SecurityAction action)
			: base(action)
		{
			this.m_Flags = SecurityPermissionFlag.NoFlags;
		}

		/// <summary>Gets or sets a value indicating whether permission to assert that all this code's callers have the requisite permission for the operation is declared.</summary>
		/// <returns>true if permission to assert is declared; otherwise, false.</returns>
		// Token: 0x17000AB8 RID: 2744
		// (get) Token: 0x060040D4 RID: 16596 RVA: 0x000E6E13 File Offset: 0x000E5013
		// (set) Token: 0x060040D5 RID: 16597 RVA: 0x000E6E20 File Offset: 0x000E5020
		public bool Assertion
		{
			get
			{
				return (this.m_Flags & SecurityPermissionFlag.Assertion) > SecurityPermissionFlag.NoFlags;
			}
			set
			{
				if (value)
				{
					this.m_Flags |= SecurityPermissionFlag.Assertion;
					return;
				}
				this.m_Flags &= ~SecurityPermissionFlag.Assertion;
			}
		}

		/// <summary>Gets or sets a value that indicates whether code has permission to perform binding redirection in the application configuration file.</summary>
		/// <returns>true if code can perform binding redirects; otherwise, false.</returns>
		// Token: 0x17000AB9 RID: 2745
		// (get) Token: 0x060040D6 RID: 16598 RVA: 0x000E6E43 File Offset: 0x000E5043
		// (set) Token: 0x060040D7 RID: 16599 RVA: 0x000E6E54 File Offset: 0x000E5054
		public bool BindingRedirects
		{
			get
			{
				return (this.m_Flags & SecurityPermissionFlag.BindingRedirects) > SecurityPermissionFlag.NoFlags;
			}
			set
			{
				if (value)
				{
					this.m_Flags |= SecurityPermissionFlag.BindingRedirects;
					return;
				}
				this.m_Flags &= ~SecurityPermissionFlag.BindingRedirects;
			}
		}

		/// <summary>Gets or sets a value indicating whether permission to manipulate <see cref="T:System.AppDomain" /> is declared.</summary>
		/// <returns>true if permission to manipulate <see cref="T:System.AppDomain" /> is declared; otherwise, false.</returns>
		// Token: 0x17000ABA RID: 2746
		// (get) Token: 0x060040D8 RID: 16600 RVA: 0x000E6E7E File Offset: 0x000E507E
		// (set) Token: 0x060040D9 RID: 16601 RVA: 0x000E6E8F File Offset: 0x000E508F
		public bool ControlAppDomain
		{
			get
			{
				return (this.m_Flags & SecurityPermissionFlag.ControlAppDomain) > SecurityPermissionFlag.NoFlags;
			}
			set
			{
				if (value)
				{
					this.m_Flags |= SecurityPermissionFlag.ControlAppDomain;
					return;
				}
				this.m_Flags &= ~SecurityPermissionFlag.ControlAppDomain;
			}
		}

		/// <summary>Gets or sets a value indicating whether permission to alter or manipulate domain security policy is declared.</summary>
		/// <returns>true if permission to alter or manipulate security policy in an application domain is declared; otherwise, false.</returns>
		// Token: 0x17000ABB RID: 2747
		// (get) Token: 0x060040DA RID: 16602 RVA: 0x000E6EB9 File Offset: 0x000E50B9
		// (set) Token: 0x060040DB RID: 16603 RVA: 0x000E6ECA File Offset: 0x000E50CA
		public bool ControlDomainPolicy
		{
			get
			{
				return (this.m_Flags & SecurityPermissionFlag.ControlDomainPolicy) > SecurityPermissionFlag.NoFlags;
			}
			set
			{
				if (value)
				{
					this.m_Flags |= SecurityPermissionFlag.ControlDomainPolicy;
					return;
				}
				this.m_Flags &= ~SecurityPermissionFlag.ControlDomainPolicy;
			}
		}

		/// <summary>Gets or sets a value indicating whether permission to alter or manipulate evidence is declared.</summary>
		/// <returns>true if the ability to alter or manipulate evidence is declared; otherwise, false.</returns>
		// Token: 0x17000ABC RID: 2748
		// (get) Token: 0x060040DC RID: 16604 RVA: 0x000E6EF4 File Offset: 0x000E50F4
		// (set) Token: 0x060040DD RID: 16605 RVA: 0x000E6F02 File Offset: 0x000E5102
		public bool ControlEvidence
		{
			get
			{
				return (this.m_Flags & SecurityPermissionFlag.ControlEvidence) > SecurityPermissionFlag.NoFlags;
			}
			set
			{
				if (value)
				{
					this.m_Flags |= SecurityPermissionFlag.ControlEvidence;
					return;
				}
				this.m_Flags &= ~SecurityPermissionFlag.ControlEvidence;
			}
		}

		/// <summary>Gets or sets a value indicating whether permission to view and manipulate security policy is declared.</summary>
		/// <returns>true if permission to manipulate security policy is declared; otherwise, false.</returns>
		// Token: 0x17000ABD RID: 2749
		// (get) Token: 0x060040DE RID: 16606 RVA: 0x000E6F26 File Offset: 0x000E5126
		// (set) Token: 0x060040DF RID: 16607 RVA: 0x000E6F34 File Offset: 0x000E5134
		public bool ControlPolicy
		{
			get
			{
				return (this.m_Flags & SecurityPermissionFlag.ControlPolicy) > SecurityPermissionFlag.NoFlags;
			}
			set
			{
				if (value)
				{
					this.m_Flags |= SecurityPermissionFlag.ControlPolicy;
					return;
				}
				this.m_Flags &= ~SecurityPermissionFlag.ControlPolicy;
			}
		}

		/// <summary>Gets or sets a value indicating whether permission to manipulate the current principal is declared.</summary>
		/// <returns>true if permission to manipulate the current principal is declared; otherwise, false.</returns>
		// Token: 0x17000ABE RID: 2750
		// (get) Token: 0x060040E0 RID: 16608 RVA: 0x000E6F58 File Offset: 0x000E5158
		// (set) Token: 0x060040E1 RID: 16609 RVA: 0x000E6F69 File Offset: 0x000E5169
		public bool ControlPrincipal
		{
			get
			{
				return (this.m_Flags & SecurityPermissionFlag.ControlPrincipal) > SecurityPermissionFlag.NoFlags;
			}
			set
			{
				if (value)
				{
					this.m_Flags |= SecurityPermissionFlag.ControlPrincipal;
					return;
				}
				this.m_Flags &= ~SecurityPermissionFlag.ControlPrincipal;
			}
		}

		/// <summary>Gets or sets a value indicating whether permission to manipulate threads is declared.</summary>
		/// <returns>true if permission to manipulate threads is declared; otherwise, false.</returns>
		// Token: 0x17000ABF RID: 2751
		// (get) Token: 0x060040E2 RID: 16610 RVA: 0x000E6F93 File Offset: 0x000E5193
		// (set) Token: 0x060040E3 RID: 16611 RVA: 0x000E6FA1 File Offset: 0x000E51A1
		public bool ControlThread
		{
			get
			{
				return (this.m_Flags & SecurityPermissionFlag.ControlThread) > SecurityPermissionFlag.NoFlags;
			}
			set
			{
				if (value)
				{
					this.m_Flags |= SecurityPermissionFlag.ControlThread;
					return;
				}
				this.m_Flags &= ~SecurityPermissionFlag.ControlThread;
			}
		}

		/// <summary>Gets or sets a value indicating whether permission to execute code is declared.</summary>
		/// <returns>true if permission to execute code is declared; otherwise, false.</returns>
		// Token: 0x17000AC0 RID: 2752
		// (get) Token: 0x060040E4 RID: 16612 RVA: 0x000E6FC5 File Offset: 0x000E51C5
		// (set) Token: 0x060040E5 RID: 16613 RVA: 0x000E6FD2 File Offset: 0x000E51D2
		public bool Execution
		{
			get
			{
				return (this.m_Flags & SecurityPermissionFlag.Execution) > SecurityPermissionFlag.NoFlags;
			}
			set
			{
				if (value)
				{
					this.m_Flags |= SecurityPermissionFlag.Execution;
					return;
				}
				this.m_Flags &= ~SecurityPermissionFlag.Execution;
			}
		}

		/// <summary>Gets or sets a value indicating whether code can plug into the common language runtime infrastructure, such as adding Remoting Context Sinks, Envoy Sinks and Dynamic Sinks.</summary>
		/// <returns>true if code can plug into the common language runtime infrastructure; otherwise, false.</returns>
		// Token: 0x17000AC1 RID: 2753
		// (get) Token: 0x060040E6 RID: 16614 RVA: 0x000E6FF5 File Offset: 0x000E51F5
		// (set) Token: 0x060040E7 RID: 16615 RVA: 0x000E7006 File Offset: 0x000E5206
		[ComVisible(true)]
		public bool Infrastructure
		{
			get
			{
				return (this.m_Flags & SecurityPermissionFlag.Infrastructure) > SecurityPermissionFlag.NoFlags;
			}
			set
			{
				if (value)
				{
					this.m_Flags |= SecurityPermissionFlag.Infrastructure;
					return;
				}
				this.m_Flags &= ~SecurityPermissionFlag.Infrastructure;
			}
		}

		/// <summary>Gets or sets a value indicating whether code can configure remoting types and channels.</summary>
		/// <returns>true if code can configure remoting types and channels; otherwise, false.</returns>
		// Token: 0x17000AC2 RID: 2754
		// (get) Token: 0x060040E8 RID: 16616 RVA: 0x000E7030 File Offset: 0x000E5230
		// (set) Token: 0x060040E9 RID: 16617 RVA: 0x000E7041 File Offset: 0x000E5241
		public bool RemotingConfiguration
		{
			get
			{
				return (this.m_Flags & SecurityPermissionFlag.RemotingConfiguration) > SecurityPermissionFlag.NoFlags;
			}
			set
			{
				if (value)
				{
					this.m_Flags |= SecurityPermissionFlag.RemotingConfiguration;
					return;
				}
				this.m_Flags &= ~SecurityPermissionFlag.RemotingConfiguration;
			}
		}

		/// <summary>Gets or sets a value indicating whether code can use a serialization formatter to serialize or deserialize an object.</summary>
		/// <returns>true if code can use a serialization formatter to serialize or deserialize an object; otherwise, false.</returns>
		// Token: 0x17000AC3 RID: 2755
		// (get) Token: 0x060040EA RID: 16618 RVA: 0x000E706B File Offset: 0x000E526B
		// (set) Token: 0x060040EB RID: 16619 RVA: 0x000E707C File Offset: 0x000E527C
		public bool SerializationFormatter
		{
			get
			{
				return (this.m_Flags & SecurityPermissionFlag.SerializationFormatter) > SecurityPermissionFlag.NoFlags;
			}
			set
			{
				if (value)
				{
					this.m_Flags |= SecurityPermissionFlag.SerializationFormatter;
					return;
				}
				this.m_Flags &= ~SecurityPermissionFlag.SerializationFormatter;
			}
		}

		/// <summary>Gets or sets a value indicating whether permission to bypass code verification is declared.</summary>
		/// <returns>true if permission to bypass code verification is declared; otherwise, false.</returns>
		// Token: 0x17000AC4 RID: 2756
		// (get) Token: 0x060040EC RID: 16620 RVA: 0x000E70A6 File Offset: 0x000E52A6
		// (set) Token: 0x060040ED RID: 16621 RVA: 0x000E70B3 File Offset: 0x000E52B3
		public bool SkipVerification
		{
			get
			{
				return (this.m_Flags & SecurityPermissionFlag.SkipVerification) > SecurityPermissionFlag.NoFlags;
			}
			set
			{
				if (value)
				{
					this.m_Flags |= SecurityPermissionFlag.SkipVerification;
					return;
				}
				this.m_Flags &= ~SecurityPermissionFlag.SkipVerification;
			}
		}

		/// <summary>Gets or sets a value indicating whether permission to call unmanaged code is declared.</summary>
		/// <returns>true if permission to call unmanaged code is declared; otherwise, false.</returns>
		// Token: 0x17000AC5 RID: 2757
		// (get) Token: 0x060040EE RID: 16622 RVA: 0x000E70D6 File Offset: 0x000E52D6
		// (set) Token: 0x060040EF RID: 16623 RVA: 0x000E70E3 File Offset: 0x000E52E3
		public bool UnmanagedCode
		{
			get
			{
				return (this.m_Flags & SecurityPermissionFlag.UnmanagedCode) > SecurityPermissionFlag.NoFlags;
			}
			set
			{
				if (value)
				{
					this.m_Flags |= SecurityPermissionFlag.UnmanagedCode;
					return;
				}
				this.m_Flags &= ~SecurityPermissionFlag.UnmanagedCode;
			}
		}

		/// <summary>Creates and returns a new <see cref="T:System.Security.Permissions.SecurityPermission" />.</summary>
		/// <returns>A <see cref="T:System.Security.Permissions.SecurityPermission" /> that corresponds to this attribute.</returns>
		// Token: 0x060040F0 RID: 16624 RVA: 0x000E7108 File Offset: 0x000E5308
		public override IPermission CreatePermission()
		{
			SecurityPermission securityPermission;
			if (base.Unrestricted)
			{
				securityPermission = new SecurityPermission(PermissionState.Unrestricted);
			}
			else
			{
				securityPermission = new SecurityPermission(this.m_Flags);
			}
			return securityPermission;
		}

		/// <summary>Gets or sets all permission flags comprising the <see cref="T:System.Security.Permissions.SecurityPermission" /> permissions.</summary>
		/// <returns>One or more of the <see cref="T:System.Security.Permissions.SecurityPermissionFlag" /> values combined using a bitwise OR.</returns>
		/// <exception cref="T:System.ArgumentException">An attempt is made to set this property to an invalid value. See <see cref="T:System.Security.Permissions.SecurityPermissionFlag" /> for the valid values. </exception>
		// Token: 0x17000AC6 RID: 2758
		// (get) Token: 0x060040F1 RID: 16625 RVA: 0x000E7135 File Offset: 0x000E5335
		// (set) Token: 0x060040F2 RID: 16626 RVA: 0x000E713D File Offset: 0x000E533D
		public SecurityPermissionFlag Flags
		{
			get
			{
				return this.m_Flags;
			}
			set
			{
				this.m_Flags = value;
			}
		}

		// Token: 0x040020D8 RID: 8408
		private SecurityPermissionFlag m_Flags;
	}
}
