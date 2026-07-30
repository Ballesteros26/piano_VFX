using System;
using System.Runtime.InteropServices;

namespace System.Security.Policy
{
	/// <summary>Represents a code group whose policy statement is the union of the current code group's policy statement and the policy statement of all its matching child code groups. This class cannot be inherited.</summary>
	// Token: 0x02000580 RID: 1408
	[ComVisible(true)]
	[Serializable]
	public sealed class UnionCodeGroup : CodeGroup
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Policy.UnionCodeGroup" /> class.</summary>
		/// <param name="membershipCondition">A membership condition that tests evidence to determine whether this code group applies policy. </param>
		/// <param name="policy">The policy statement for the code group in the form of a permission set and attributes to grant code that matches the membership condition. </param>
		/// <exception cref="T:System.ArgumentException">The type of the <paramref name="membershipCondition" /> parameter is not valid.-or- The type of the <paramref name="policy" /> parameter is not valid. </exception>
		// Token: 0x06003F11 RID: 16145 RVA: 0x000DEBC6 File Offset: 0x000DCDC6
		public UnionCodeGroup(IMembershipCondition membershipCondition, PolicyStatement policy)
			: base(membershipCondition, policy)
		{
		}

		// Token: 0x06003F12 RID: 16146 RVA: 0x000DE8EF File Offset: 0x000DCAEF
		internal UnionCodeGroup(SecurityElement e, PolicyLevel level)
			: base(e, level)
		{
		}

		/// <summary>Makes a deep copy of the current code group.</summary>
		/// <returns>An equivalent copy of the current code group, including its membership conditions and child code groups.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06003F13 RID: 16147 RVA: 0x000E1BC4 File Offset: 0x000DFDC4
		public override CodeGroup Copy()
		{
			return this.Copy(true);
		}

		// Token: 0x06003F14 RID: 16148 RVA: 0x000E1BD0 File Offset: 0x000DFDD0
		internal CodeGroup Copy(bool childs)
		{
			UnionCodeGroup unionCodeGroup = new UnionCodeGroup(base.MembershipCondition, base.PolicyStatement);
			unionCodeGroup.Name = base.Name;
			unionCodeGroup.Description = base.Description;
			if (childs)
			{
				foreach (object obj in base.Children)
				{
					CodeGroup codeGroup = (CodeGroup)obj;
					unionCodeGroup.AddChild(codeGroup.Copy());
				}
			}
			return unionCodeGroup;
		}

		/// <summary>Resolves policy for the code group and its descendants for a set of evidence.</summary>
		/// <returns>A policy statement consisting of the permissions granted by the code group with optional attributes, or null if the code group does not apply (the membership condition does not match the specified evidence).</returns>
		/// <param name="evidence">The evidence for the assembly. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="evidence" /> parameter is null. </exception>
		/// <exception cref="T:System.Security.Policy.PolicyException">More than one code group (including the parent code group and any child code groups) is marked <see cref="F:System.Security.Policy.PolicyStatementAttribute.Exclusive" />. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06003F15 RID: 16149 RVA: 0x000E1C5C File Offset: 0x000DFE5C
		public override PolicyStatement Resolve(Evidence evidence)
		{
			if (evidence == null)
			{
				throw new ArgumentNullException("evidence");
			}
			if (!base.MembershipCondition.Check(evidence))
			{
				return null;
			}
			PermissionSet permissionSet = base.PolicyStatement.PermissionSet.Copy();
			if (base.Children.Count > 0)
			{
				foreach (object obj in base.Children)
				{
					PolicyStatement policyStatement = ((CodeGroup)obj).Resolve(evidence);
					if (policyStatement != null)
					{
						permissionSet = permissionSet.Union(policyStatement.PermissionSet);
					}
				}
			}
			PolicyStatement policyStatement2 = base.PolicyStatement.Copy();
			policyStatement2.PermissionSet = permissionSet;
			return policyStatement2;
		}

		/// <summary>Resolves matching code groups.</summary>
		/// <returns>The complete set of code groups that were matched by the evidence.</returns>
		/// <param name="evidence">The evidence for the assembly. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="evidence" /> parameter is null. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06003F16 RID: 16150 RVA: 0x000E1D14 File Offset: 0x000DFF14
		public override CodeGroup ResolveMatchingCodeGroups(Evidence evidence)
		{
			if (evidence == null)
			{
				throw new ArgumentNullException("evidence");
			}
			if (!base.MembershipCondition.Check(evidence))
			{
				return null;
			}
			CodeGroup codeGroup = this.Copy(false);
			if (base.Children.Count > 0)
			{
				foreach (object obj in base.Children)
				{
					CodeGroup codeGroup2 = ((CodeGroup)obj).ResolveMatchingCodeGroups(evidence);
					if (codeGroup2 != null)
					{
						codeGroup.AddChild(codeGroup2);
					}
				}
			}
			return codeGroup;
		}

		/// <summary>Gets the merge logic.</summary>
		/// <returns>Always the string "Union".</returns>
		// Token: 0x17000A5F RID: 2655
		// (get) Token: 0x06003F17 RID: 16151 RVA: 0x000DE988 File Offset: 0x000DCB88
		public override string MergeLogic
		{
			get
			{
				return "Union";
			}
		}
	}
}
