using System;
using System.Runtime.InteropServices;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	/// <summary>Provides the ability to control access to objects without direct manipulation of Access Control Lists (ACLs); also grants the ability to type-cast access rights. </summary>
	/// <typeparam name="T">The access rights for the object.</typeparam>
	// Token: 0x02000605 RID: 1541
	public abstract class ObjectSecurity<T> : NativeObjectSecurity where T : struct
	{
		/// <summary>Initializes a new instance of the ObjectSecurity`1 class.</summary>
		/// <param name="isContainer">true if the new <see cref="T:System.Security.AccessControl.ObjectSecurity`1" /> object is a container object.</param>
		/// <param name="resourceType">The type of resource.</param>
		// Token: 0x06004378 RID: 17272 RVA: 0x000ED28D File Offset: 0x000EB48D
		protected ObjectSecurity(bool isContainer, ResourceType resourceType)
			: base(isContainer, resourceType)
		{
		}

		/// <summary>Initializes a new instance of the ObjectSecurity`1 class.</summary>
		/// <param name="isContainer">true if the new <see cref="T:System.Security.AccessControl.ObjectSecurity`1" /> object is a container object.</param>
		/// <param name="resourceType">The type of resource.</param>
		/// <param name="safeHandle">A handle.</param>
		/// <param name="includeSections">The sections to include.</param>
		// Token: 0x06004379 RID: 17273 RVA: 0x000ED297 File Offset: 0x000EB497
		protected ObjectSecurity(bool isContainer, ResourceType resourceType, SafeHandle safeHandle, AccessControlSections includeSections)
			: base(isContainer, resourceType, safeHandle, includeSections)
		{
		}

		/// <summary>Initializes a new instance of the ObjectSecurity`1 class.</summary>
		/// <param name="isContainer">true if the new <see cref="T:System.Security.AccessControl.ObjectSecurity`1" /> object is a container object.</param>
		/// <param name="resourceType">The type of resource.</param>
		/// <param name="name">The name of the securable object with which the new <see cref="T:System.Security.AccessControl.ObjectSecurity`1" /> object is associated.</param>
		/// <param name="includeSections">The sections to include.</param>
		// Token: 0x0600437A RID: 17274 RVA: 0x000ED2A4 File Offset: 0x000EB4A4
		protected ObjectSecurity(bool isContainer, ResourceType resourceType, string name, AccessControlSections includeSections)
			: base(isContainer, resourceType, name, includeSections)
		{
		}

		/// <summary>Initializes a new instance of the ObjectSecurity`1 class.</summary>
		/// <param name="isContainer">true if the new <see cref="T:System.Security.AccessControl.ObjectSecurity`1" /> object is a container object.</param>
		/// <param name="resourceType">The type of resource.</param>
		/// <param name="safeHandle">A handle.</param>
		/// <param name="includeSections">The sections to include.</param>
		/// <param name="exceptionFromErrorCode">A delegate implemented by integrators that provides custom exceptions.</param>
		/// <param name="exceptionContext">An object that contains contextual information about the source or destination of the exception.</param>
		// Token: 0x0600437B RID: 17275 RVA: 0x000ED2B1 File Offset: 0x000EB4B1
		protected ObjectSecurity(bool isContainer, ResourceType resourceType, SafeHandle safeHandle, AccessControlSections includeSections, NativeObjectSecurity.ExceptionFromErrorCode exceptionFromErrorCode, object exceptionContext)
			: base(isContainer, resourceType, safeHandle, includeSections, exceptionFromErrorCode, exceptionContext)
		{
		}

		/// <summary>Initializes a new instance of the ObjectSecurity`1 class.</summary>
		/// <param name="isContainer">true if the new <see cref="T:System.Security.AccessControl.ObjectSecurity`1" /> object is a container object.</param>
		/// <param name="resourceType">The type of resource.</param>
		/// <param name="name">The name of the securable object with which the new <see cref="T:System.Security.AccessControl.ObjectSecurity`1" /> object is associated.</param>
		/// <param name="includeSections">The sections to include. </param>
		/// <param name="exceptionFromErrorCode">A delegate implemented by integrators that provides custom exceptions.</param>
		/// <param name="exceptionContext">An object that contains contextual information about the source or destination of the exception.</param>
		// Token: 0x0600437C RID: 17276 RVA: 0x000ED2C2 File Offset: 0x000EB4C2
		protected ObjectSecurity(bool isContainer, ResourceType resourceType, string name, AccessControlSections includeSections, NativeObjectSecurity.ExceptionFromErrorCode exceptionFromErrorCode, object exceptionContext)
			: base(isContainer, resourceType, name, includeSections, exceptionFromErrorCode, exceptionContext)
		{
		}

		/// <summary>Gets the Type of the securable object associated with this ObjectSecurity`1 object.</summary>
		/// <returns>The type of the securable object associated with the current instance.</returns>
		// Token: 0x17000B45 RID: 2885
		// (get) Token: 0x0600437D RID: 17277 RVA: 0x000ED2D3 File Offset: 0x000EB4D3
		public override Type AccessRightType
		{
			get
			{
				return typeof(T);
			}
		}

		/// <summary>Gets the Type of the object associated with the access rules of this ObjectSecurity`1 object. </summary>
		/// <returns>The Type of the object associated with the access rules of the current instance.</returns>
		// Token: 0x17000B46 RID: 2886
		// (get) Token: 0x0600437E RID: 17278 RVA: 0x000ED2DF File Offset: 0x000EB4DF
		public override Type AccessRuleType
		{
			get
			{
				return typeof(AccessRule<T>);
			}
		}

		/// <summary>Gets the Type object associated with the audit rules of this ObjectSecurity`1 object.</summary>
		/// <returns>The Type object associated with the audit rules of the current instance.</returns>
		// Token: 0x17000B47 RID: 2887
		// (get) Token: 0x0600437F RID: 17279 RVA: 0x000ED2EB File Offset: 0x000EB4EB
		public override Type AuditRuleType
		{
			get
			{
				return typeof(AuditRule<T>);
			}
		}

		/// <summary>Initializes a new instance of the ObjectAccessRule class that represents a new access control rule for the associated security object.</summary>
		/// <returns>Represents a new access control rule for the specified user, with the specified access rights, access control, and flags.</returns>
		/// <param name="identityReference">Represents a user account.</param>
		/// <param name="accessMask">The access type.</param>
		/// <param name="isInherited">true if the access rule is inherited; otherwise, false.</param>
		/// <param name="inheritanceFlags">Specifies how to propagate access masks to child objects.</param>
		/// <param name="propagationFlags">Specifies how to propagate Access Control Entries (ACEs) to child objects.</param>
		/// <param name="type">Specifies whether access is allowed or denied.</param>
		// Token: 0x06004380 RID: 17280 RVA: 0x000ED2F7 File Offset: 0x000EB4F7
		public override AccessRule AccessRuleFactory(IdentityReference identityReference, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type)
		{
			return new AccessRule<T>(identityReference, accessMask, isInherited, inheritanceFlags, propagationFlags, type);
		}

		/// <summary>Adds the specified access rule to the Discretionary Access Control List (DACL) associated with this ObjectSecurity`1 object.</summary>
		/// <param name="rule">The rule to add.</param>
		// Token: 0x06004381 RID: 17281 RVA: 0x000EA4DF File Offset: 0x000E86DF
		public virtual void AddAccessRule(AccessRule<T> rule)
		{
			base.AddAccessRule(rule);
		}

		/// <summary>Removes access rules that contain the same security identifier and access mask as the specified access rule from the Discretionary Access Control List (DACL) associated with this ObjectSecurity`1 object.</summary>
		/// <returns>Returns true if the access rule was successfully removed; otherwise, false.</returns>
		/// <param name="rule">The rule to remove.</param>
		// Token: 0x06004382 RID: 17282 RVA: 0x000EA4E8 File Offset: 0x000E86E8
		public virtual bool RemoveAccessRule(AccessRule<T> rule)
		{
			return base.RemoveAccessRule(rule);
		}

		/// <summary>Removes all access rules that have the same security identifier as the specified access rule from the Discretionary Access Control List (DACL) associated with this ObjectSecurity`1 object.</summary>
		/// <param name="rule">The access rule to remove.</param>
		// Token: 0x06004383 RID: 17283 RVA: 0x000EA4F1 File Offset: 0x000E86F1
		public virtual void RemoveAccessRuleAll(AccessRule<T> rule)
		{
			base.RemoveAccessRuleAll(rule);
		}

		/// <summary>Removes all access rules that exactly match the specified access rule from the Discretionary Access Control List (DACL) associated with this ObjectSecurity`1 object</summary>
		/// <param name="rule">The access rule to remove.</param>
		// Token: 0x06004384 RID: 17284 RVA: 0x000EA4FA File Offset: 0x000E86FA
		public virtual void RemoveAccessRuleSpecific(AccessRule<T> rule)
		{
			base.RemoveAccessRuleSpecific(rule);
		}

		/// <summary>Removes all access rules in the Discretionary Access Control List (DACL) associated with this ObjectSecurity`1 object and then adds the specified access rule.</summary>
		/// <param name="rule">The access rule to reset.</param>
		// Token: 0x06004385 RID: 17285 RVA: 0x000EA503 File Offset: 0x000E8703
		public virtual void ResetAccessRule(AccessRule<T> rule)
		{
			base.ResetAccessRule(rule);
		}

		/// <summary>Removes all access rules that contain the same security identifier and qualifier as the specified access rule in the Discretionary Access Control List (DACL) associated with this ObjectSecurity`1 object and then adds the specified access rule.</summary>
		/// <param name="rule">The access rule to set.</param>
		// Token: 0x06004386 RID: 17286 RVA: 0x000EA50C File Offset: 0x000E870C
		public virtual void SetAccessRule(AccessRule<T> rule)
		{
			base.SetAccessRule(rule);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.AuditRule" /> class representing the specified audit rule for the specified user.</summary>
		/// <returns>Returns the specified audit rule for the specified user.</returns>
		/// <param name="identityReference">Represents a user account. </param>
		/// <param name="accessMask">An integer that specifies an access type.</param>
		/// <param name="isInherited">true if the access rule is inherited; otherwise, false.</param>
		/// <param name="inheritanceFlags">Specifies how to propagate access masks to child objects.</param>
		/// <param name="propagationFlags">Specifies how to propagate Access Control Entries (ACEs) to child objects.</param>
		/// <param name="flags">Describes the type of auditing to perform.</param>
		// Token: 0x06004387 RID: 17287 RVA: 0x000ED307 File Offset: 0x000EB507
		public override AuditRule AuditRuleFactory(IdentityReference identityReference, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags)
		{
			return new AuditRule<T>(identityReference, accessMask, isInherited, inheritanceFlags, propagationFlags, flags);
		}

		/// <summary>Adds the specified audit rule to the System Access Control List (SACL) associated with this ObjectSecurity`1 object.</summary>
		/// <param name="rule">The audit rule to add.</param>
		// Token: 0x06004388 RID: 17288 RVA: 0x000EA520 File Offset: 0x000E8720
		public virtual void AddAuditRule(AuditRule<T> rule)
		{
			base.AddAuditRule(rule);
		}

		/// <summary>Removes audit rules that contain the same security identifier and access mask as the specified audit rule from the System Access Control List (SACL) associated with this ObjectSecurity`1 object.</summary>
		/// <returns>Returns true if the object was removed; otherwise, false.</returns>
		/// <param name="rule">The audit rule to remove</param>
		// Token: 0x06004389 RID: 17289 RVA: 0x000EA529 File Offset: 0x000E8729
		public virtual bool RemoveAuditRule(AuditRule<T> rule)
		{
			return base.RemoveAuditRule(rule);
		}

		/// <summary>Removes all audit rules that have the same security identifier as the specified audit rule from the System Access Control List (SACL) associated with this ObjectSecurity`1 object.</summary>
		/// <param name="rule">The audit rule to remove.</param>
		// Token: 0x0600438A RID: 17290 RVA: 0x000EA532 File Offset: 0x000E8732
		public virtual void RemoveAuditRuleAll(AuditRule<T> rule)
		{
			base.RemoveAuditRuleAll(rule);
		}

		/// <summary>Removes all audit rules that exactly match the specified audit rule from the System Access Control List (SACL) associated with this ObjectSecurity`1 object</summary>
		/// <param name="rule">The audit rule to remove.</param>
		// Token: 0x0600438B RID: 17291 RVA: 0x000EA53B File Offset: 0x000E873B
		public virtual void RemoveAuditRuleSpecific(AuditRule<T> rule)
		{
			base.RemoveAuditRuleSpecific(rule);
		}

		/// <summary>Removes all audit rules that contain the same security identifier and qualifier as the specified audit rule in the System Access Control List (SACL) associated with this ObjectSecurity`1 object and then adds the specified audit rule.</summary>
		/// <param name="rule">The audit rule to set.</param>
		// Token: 0x0600438C RID: 17292 RVA: 0x000EA544 File Offset: 0x000E8744
		public virtual void SetAuditRule(AuditRule<T> rule)
		{
			base.SetAuditRule(rule);
		}

		/// <summary>Saves the security descriptor associated with this ObjectSecurity`1 object to permanent storage, using the specified handle.</summary>
		/// <param name="handle">The handle of the securable object with which this ObjectSecurity`1 object is associated.</param>
		// Token: 0x0600438D RID: 17293 RVA: 0x000ED318 File Offset: 0x000EB518
		protected void Persist(SafeHandle handle)
		{
			base.WriteLock();
			try
			{
				this.Persist(handle, base.AccessControlSectionsModified);
			}
			finally
			{
				base.WriteUnlock();
			}
		}

		/// <summary>Saves the security descriptor associated with this ObjectSecurity`1 object to permanent storage, using the specified name.</summary>
		/// <param name="name">The name of the securable object with which this ObjectSecurity`1 object is associated.</param>
		// Token: 0x0600438E RID: 17294 RVA: 0x000ED354 File Offset: 0x000EB554
		protected void Persist(string name)
		{
			base.WriteLock();
			try
			{
				this.Persist(name, base.AccessControlSectionsModified);
			}
			finally
			{
				base.WriteUnlock();
			}
		}
	}
}
