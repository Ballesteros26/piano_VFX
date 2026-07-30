using System;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Security.Permissions;
using System.Security.Principal;

namespace System.IO.Pipes
{
	/// <summary>Represents the access control and audit security for a pipe.</summary>
	// Token: 0x02000038 RID: 56
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public class PipeSecurity : NativeObjectSecurity
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Pipes.PipeSecurity" /> class.</summary>
		// Token: 0x060000F5 RID: 245 RVA: 0x00003975 File Offset: 0x00001B75
		public PipeSecurity()
			: base(false, ResourceType.FileObject)
		{
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x0000397F File Offset: 0x00001B7F
		internal PipeSecurity(SafeHandle handle, AccessControlSections includeSections)
			: base(false, ResourceType.FileObject, handle, includeSections)
		{
		}

		/// <summary>Gets the <see cref="T:System.Type" /> of the securable object that is associated with the current <see cref="T:System.IO.Pipes.PipeSecurity" /> object.</summary>
		/// <returns>The type of the securable object that is associated with the current <see cref="T:System.IO.Pipes.PipeSecurity" /> object.</returns>
		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000F7 RID: 247 RVA: 0x0000398B File Offset: 0x00001B8B
		public override Type AccessRightType
		{
			get
			{
				return typeof(PipeAccessRights);
			}
		}

		/// <summary>Gets the <see cref="T:System.Type" /> of the object that is associated with the access rules of the current <see cref="T:System.IO.Pipes.PipeSecurity" /> object.</summary>
		/// <returns>The type of the object that is associated with the access rules of the current <see cref="T:System.IO.Pipes.PipeSecurity" /> object.</returns>
		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x00003997 File Offset: 0x00001B97
		public override Type AccessRuleType
		{
			get
			{
				return typeof(PipeAccessRule);
			}
		}

		/// <summary>Gets the <see cref="T:System.Type" /> object associated with the audit rules of the current <see cref="T:System.IO.Pipes.PipeSecurity" /> object.</summary>
		/// <returns>The type of the object that is associated with the audit rules of the current <see cref="T:System.IO.Pipes.PipeSecurity" /> object.</returns>
		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x000039A3 File Offset: 0x00001BA3
		public override Type AuditRuleType
		{
			get
			{
				return typeof(PipeAuditRule);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.AccessRule" /> class with the specified values.</summary>
		/// <returns>The <see cref="T:System.Security.AccessControl.AccessRule" /> object that this method creates.</returns>
		/// <param name="identityReference">The identity that the access rule applies to. It must be an object that can be cast as a <see cref="T:System.Security.Principal.SecurityIdentifier" /> object.</param>
		/// <param name="accessMask">The access mask of this rule. The access mask is a 32-bit collection of anonymous bits, the meaning of which is defined by the individual integrators</param>
		/// <param name="isInherited">true if this rule is inherited from a parent container; otherwise false.</param>
		/// <param name="inheritanceFlags">One of the <see cref="T:System.Security.AccessControl.InheritanceFlags" /> values that specifies the inheritance properties of the access rule.</param>
		/// <param name="propagationFlags">One of the <see cref="T:System.Security.AccessControl.PropagationFlags" /> values that specifies whether inherited access rules are automatically propagated. The propagation flags are ignored if <paramref name="inheritanceFlags" /> is set to <see cref="F:System.Security.AccessControl.InheritanceFlags.None" />.</param>
		/// <param name="type">Specifies the valid access control type.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="accessMask" />, <paramref name="inheritanceFlags" />, <paramref name="propagationFlags" />, or <paramref name="type" /> specifies an invalid value.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="identityReference" /> is null. -or-<paramref name="accessMask" /> is zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="identityReference" /> is neither of type <see cref="T:System.Security.Principal.SecurityIdentifier" /> nor of a type, such as <see cref="T:System.Security.Principal.NTAccount" />, that can be converted to type <see cref="T:System.Security.Principal.SecurityIdentifier" />.</exception>
		// Token: 0x060000FA RID: 250 RVA: 0x000039AF File Offset: 0x00001BAF
		public override AccessRule AccessRuleFactory(IdentityReference identityReference, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type)
		{
			return new PipeAccessRule(identityReference, (PipeAccessRights)accessMask, type);
		}

		/// <summary>Adds an access rule to the Discretionary Access Control List (DACL) that is associated with the current <see cref="T:System.IO.Pipes.PipeSecurity" /> object.</summary>
		/// <param name="rule">The access rule to add.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="rule" /> parameter is null.</exception>
		// Token: 0x060000FB RID: 251 RVA: 0x000039BA File Offset: 0x00001BBA
		public void AddAccessRule(PipeAccessRule rule)
		{
			base.AddAccessRule(rule);
		}

		/// <summary>Adds an audit rule to the System Access Control List (SACL)that is associated with the current <see cref="T:System.IO.Pipes.PipeSecurity" /> object.</summary>
		/// <param name="rule">The audit rule to add.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="rule" /> parameter is null.</exception>
		// Token: 0x060000FC RID: 252 RVA: 0x000039C3 File Offset: 0x00001BC3
		public void AddAuditRule(PipeAuditRule rule)
		{
			base.AddAuditRule(rule);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.AuditRule" /> class with the specified values.</summary>
		/// <returns>The <see cref="T:System.Security.AccessControl.AuditRule" /> object that this method creates.</returns>
		/// <param name="identityReference">The identity that the access rule applies to. It must be an object that can be cast as a <see cref="T:System.Security.Principal.SecurityIdentifier" /> object.</param>
		/// <param name="accessMask">The access mask of this rule. The access mask is a 32-bit collection of anonymous bits, the meaning of which is defined by the individual integrators</param>
		/// <param name="isInherited">true if this rule is inherited from a parent container; otherwise, false..</param>
		/// <param name="inheritanceFlags">One of the <see cref="T:System.Security.AccessControl.InheritanceFlags" /> values that specifies the inheritance properties of the access rule.</param>
		/// <param name="propagationFlags">One of the <see cref="T:System.Security.AccessControl.PropagationFlags" /> values that specifies whether inherited access rules are automatically propagated. The propagation flags are ignored if <paramref name="inheritanceFlags" /> is set to <see cref="F:System.Security.AccessControl.InheritanceFlags.None" />.</param>
		/// <param name="flags">One of the <see cref="T:System.Security.AccessControl.AuditFlags" /> values that specifies the valid access control type.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="accessMask" />, <paramref name="inheritanceFlags" />, <paramref name="propagationFlags" />, or <paramref name="flags" /> properties specify an invalid value.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="identityReference" /> property is null. -or-The <paramref name="accessMask" /> property is zero.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="identityReference" /> property is neither of type <see cref="T:System.Security.Principal.SecurityIdentifier" /> nor of a type, such as <see cref="T:System.Security.Principal.NTAccount" />, that can be converted to type <see cref="T:System.Security.Principal.SecurityIdentifier" />.</exception>
		// Token: 0x060000FD RID: 253 RVA: 0x000039CC File Offset: 0x00001BCC
		public sealed override AuditRule AuditRuleFactory(IdentityReference identityReference, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags)
		{
			return new PipeAuditRule(identityReference, (PipeAccessRights)accessMask, flags);
		}

		/// <summary>Saves the specified sections of the security descriptor that is associated with the current <see cref="T:System.IO.Pipes.PipeSecurity" /> object to permanent storage.</summary>
		/// <param name="handle">The handle of the securable object that the current <see cref="T:System.IO.Pipes.PipeSecurity" /> object is associated with.</param>
		// Token: 0x060000FE RID: 254 RVA: 0x000039D8 File Offset: 0x00001BD8
		[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
		protected internal void Persist(SafeHandle handle)
		{
			base.WriteLock();
			try
			{
				base.Persist(handle, base.AccessControlSectionsModified, null);
			}
			finally
			{
				base.WriteUnlock();
			}
		}

		/// <summary>Saves the specified sections of the security descriptor that is associated with the current <see cref="T:System.IO.Pipes.PipeSecurity" /> object to permanent storage.</summary>
		/// <param name="name">The name of the securable object that the current <see cref="T:System.IO.Pipes.PipeSecurity" /> object is associated with.</param>
		// Token: 0x060000FF RID: 255 RVA: 0x00003A14 File Offset: 0x00001C14
		[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
		protected internal void Persist(string name)
		{
			base.WriteLock();
			try
			{
				base.Persist(name, base.AccessControlSectionsModified, null);
			}
			finally
			{
				base.WriteUnlock();
			}
		}

		/// <summary>Removes an access rule from the Discretionary Access Control List (DACL) that is associated with the current <see cref="T:System.IO.Pipes.PipeSecurity" /> object.</summary>
		/// <returns>true if the operation is successful; otherwise, false.</returns>
		/// <param name="rule">The access rule to remove.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="rule" /> parameter is null.</exception>
		// Token: 0x06000100 RID: 256 RVA: 0x00003A50 File Offset: 0x00001C50
		public bool RemoveAccessRule(PipeAccessRule rule)
		{
			return base.RemoveAccessRule(rule);
		}

		/// <summary>Removes the specified access rule from the Discretionary Access Control List (DACL) that is associated with the current <see cref="T:System.IO.Pipes.PipeSecurity" /> object.</summary>
		/// <param name="rule">The access rule to remove.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="rule" /> parameter is null.</exception>
		// Token: 0x06000101 RID: 257 RVA: 0x00003A59 File Offset: 0x00001C59
		public void RemoveAccessRuleSpecific(PipeAccessRule rule)
		{
			base.RemoveAccessRuleSpecific(rule);
		}

		/// <summary>Removes an audit rule from the System Access Control List (SACL) that is associated with the current <see cref="T:System.IO.Pipes.PipeSecurity" /> object.</summary>
		/// <returns>true if the audit rule was removed; otherwise, false</returns>
		/// <param name="rule">The audit rule to remove.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="rule" /> parameter is null.</exception>
		// Token: 0x06000102 RID: 258 RVA: 0x00003A62 File Offset: 0x00001C62
		public bool RemoveAuditRule(PipeAuditRule rule)
		{
			return base.RemoveAuditRule(rule);
		}

		/// <summary>Removes all audit rules that have the same security identifier as the specified audit rule from the System Access Control List (SACL) that is associated with the current <see cref="T:System.IO.Pipes.PipeSecurity" /> object.</summary>
		/// <param name="rule">The audit rule to remove.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="rule" /> parameter is null.</exception>
		// Token: 0x06000103 RID: 259 RVA: 0x00003A6B File Offset: 0x00001C6B
		public void RemoveAuditRuleAll(PipeAuditRule rule)
		{
			base.RemoveAuditRuleAll(rule);
		}

		/// <summary>Removes the specified audit rule from the System Access Control List (SACL) that is associated with the current <see cref="T:System.IO.Pipes.PipeSecurity" /> object.</summary>
		/// <param name="rule">The audit rule to remove.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="rule" /> parameter is null.</exception>
		// Token: 0x06000104 RID: 260 RVA: 0x00003A74 File Offset: 0x00001C74
		public void RemoveAuditRuleSpecific(PipeAuditRule rule)
		{
			base.RemoveAuditRuleSpecific(rule);
		}

		/// <summary>Removes all access rules in the Discretionary Access Control List (DACL) that is associated with the current <see cref="T:System.IO.Pipes.PipeSecurity" /> object and then adds the specified access rule.</summary>
		/// <param name="rule">The access rule to add.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="rule" /> parameter is null.</exception>
		// Token: 0x06000105 RID: 261 RVA: 0x00003A7D File Offset: 0x00001C7D
		public void ResetAccessRule(PipeAccessRule rule)
		{
			base.ResetAccessRule(rule);
		}

		/// <summary>Sets an access rule in the Discretionary Access Control List (DACL) that is associated with the current <see cref="T:System.IO.Pipes.PipeSecurity" /> object.</summary>
		/// <param name="rule">The rule to set.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="rule" /> parameter is null.</exception>
		// Token: 0x06000106 RID: 262 RVA: 0x00003A86 File Offset: 0x00001C86
		public void SetAccessRule(PipeAccessRule rule)
		{
			base.SetAccessRule(rule);
		}

		/// <summary>Sets an audit rule in the System Access Control List (SACL) that is associated with the current <see cref="T:System.IO.Pipes.PipeSecurity" /> object.</summary>
		/// <param name="rule">The rule to set.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="rule" /> parameter is null.</exception>
		// Token: 0x06000107 RID: 263 RVA: 0x00003A8F File Offset: 0x00001C8F
		public void SetAuditRule(PipeAuditRule rule)
		{
			base.SetAuditRule(rule);
		}
	}
}
