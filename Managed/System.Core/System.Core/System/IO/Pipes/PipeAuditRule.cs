using System;
using System.Security.AccessControl;
using System.Security.Permissions;
using System.Security.Principal;

namespace System.IO.Pipes
{
	/// <summary>Represents an abstraction of an access control entry (ACE) that defines an audit rule for a pipe.</summary>
	// Token: 0x02000030 RID: 48
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class PipeAuditRule : AuditRule
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Pipes.PipeAuditRule" /> class for a user account specified in a <see cref="T:System.Security.Principal.IdentityReference" /> object.</summary>
		/// <param name="identity">An <see cref="T:System.Security.Principal.IdentityReference" /> object that encapsulates a reference to a user account.</param>
		/// <param name="rights">One of the <see cref="T:System.IO.Pipes.PipeAccessRights" /> values that specifies the type of operation associated with the access rule.</param>
		/// <param name="flags">One of the <see cref="T:System.Security.AccessControl.AuditFlags" /> values that specifies when to perform auditing.</param>
		// Token: 0x060000E8 RID: 232 RVA: 0x00003957 File Offset: 0x00001B57
		public PipeAuditRule(IdentityReference identity, PipeAccessRights rights, AuditFlags flags)
			: base(identity, (int)rights, false, InheritanceFlags.None, PropagationFlags.None, flags)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Pipes.PipeAuditRule" /> class for a named user account.</summary>
		/// <param name="identity">The name of the user account.</param>
		/// <param name="rights">One of the <see cref="T:System.IO.Pipes.PipeAccessRights" /> values that specifies the type of operation associated with the access rule.</param>
		/// <param name="flags">One of the <see cref="T:System.Security.AccessControl.AuditFlags" /> values that specifies when to perform auditing.</param>
		// Token: 0x060000E9 RID: 233 RVA: 0x00003965 File Offset: 0x00001B65
		public PipeAuditRule(string identity, PipeAccessRights rights, AuditFlags flags)
			: this(new NTAccount(identity), rights, flags)
		{
		}

		/// <summary>Gets the <see cref="T:System.IO.Pipes.PipeAccessRights" /> flags that are associated with the current <see cref="T:System.IO.Pipes.PipeAuditRule" /> object.</summary>
		/// <returns>A bitwise combination of the <see cref="T:System.IO.Pipes.PipeAccessRights" /> values. </returns>
		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000EA RID: 234 RVA: 0x0000394F File Offset: 0x00001B4F
		public PipeAccessRights PipeAccessRights
		{
			get
			{
				return (PipeAccessRights)base.AccessMask;
			}
		}
	}
}
