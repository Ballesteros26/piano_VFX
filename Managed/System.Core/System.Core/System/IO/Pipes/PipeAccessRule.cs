using System;
using System.Security.AccessControl;
using System.Security.Permissions;
using System.Security.Principal;

namespace System.IO.Pipes
{
	/// <summary>Represents an abstraction of an access control entry (ACE) that defines an access rule for a pipe.</summary>
	// Token: 0x0200002F RID: 47
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class PipeAccessRule : AccessRule
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Pipes.PipeAccessRule" /> class with the specified identity, pipe access rights, and access control type.</summary>
		/// <param name="identity">An <see cref="T:System.Security.Principal.IdentityReference" /> object that encapsulates a reference to a user account.</param>
		/// <param name="rights">One of the <see cref="T:System.IO.Pipes.PipeAccessRights" /> values that specifies the type of operation associated with the access rule.</param>
		/// <param name="type">One of the <see cref="T:System.Security.AccessControl.AccessControlType" /> values that specifies whether to allow or deny the operation.</param>
		// Token: 0x060000E5 RID: 229 RVA: 0x00003931 File Offset: 0x00001B31
		public PipeAccessRule(IdentityReference identity, PipeAccessRights rights, AccessControlType type)
			: base(identity, (int)rights, false, InheritanceFlags.None, PropagationFlags.None, type)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Pipes.PipeAccessRule" /> class with the specified identity, pipe access rights, and access control type.</summary>
		/// <param name="identity">The name of the user account.</param>
		/// <param name="rights">One of the <see cref="T:System.IO.Pipes.PipeAccessRights" /> values that specifies the type of operation associated with the access rule.</param>
		/// <param name="type">One of the <see cref="T:System.Security.AccessControl.AccessControlType" /> values that specifies whether to allow or deny the operation.</param>
		// Token: 0x060000E6 RID: 230 RVA: 0x0000393F File Offset: 0x00001B3F
		public PipeAccessRule(string identity, PipeAccessRights rights, AccessControlType type)
			: this(new NTAccount(identity), rights, type)
		{
		}

		/// <summary>Gets the <see cref="T:System.IO.Pipes.PipeAccessRights" /> flags that are associated with the current <see cref="T:System.IO.Pipes.PipeAccessRule" /> object.</summary>
		/// <returns>A bitwise combination of the <see cref="T:System.IO.Pipes.PipeAccessRights" /> values.</returns>
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x0000394F File Offset: 0x00001B4F
		public PipeAccessRights PipeAccessRights
		{
			get
			{
				return (PipeAccessRights)base.AccessMask;
			}
		}
	}
}
