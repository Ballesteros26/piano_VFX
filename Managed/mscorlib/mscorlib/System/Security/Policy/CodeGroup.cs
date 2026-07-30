using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Security.Policy
{
	/// <summary>Represents the abstract base class from which all implementations of code groups must derive.</summary>
	// Token: 0x0200055E RID: 1374
	[ComVisible(true)]
	[Serializable]
	public abstract class CodeGroup
	{
		/// <summary>Initializes a new instance of <see cref="T:System.Security.Policy.CodeGroup" />.</summary>
		/// <param name="membershipCondition">A membership condition that tests evidence to determine whether this code group applies policy. </param>
		/// <param name="policy">The policy statement for the code group in the form of a permission set and attributes to grant code that matches the membership condition. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="membershipCondition" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The type of the <paramref name="membershipCondition" /> parameter is not valid.-or- The type of the <paramref name="policy" /> parameter is not valid. </exception>
		// Token: 0x06003DB9 RID: 15801 RVA: 0x000DD772 File Offset: 0x000DB972
		protected CodeGroup(IMembershipCondition membershipCondition, PolicyStatement policy)
		{
			if (membershipCondition == null)
			{
				throw new ArgumentNullException("membershipCondition");
			}
			if (policy != null)
			{
				this.m_policy = policy.Copy();
			}
			this.m_membershipCondition = membershipCondition.Copy();
		}

		// Token: 0x06003DBA RID: 15802 RVA: 0x000DD7AE File Offset: 0x000DB9AE
		internal CodeGroup(SecurityElement e, PolicyLevel level)
		{
			this.FromXml(e, level);
		}

		/// <summary>When overridden in a derived class, makes a deep copy of the current code group.</summary>
		/// <returns>An equivalent copy of the current code group, including its membership conditions and child code groups.</returns>
		// Token: 0x06003DBB RID: 15803
		public abstract CodeGroup Copy();

		/// <summary>When overridden in a derived class, gets the merge logic for the code group.</summary>
		/// <returns>A description of the merge logic for the code group.</returns>
		// Token: 0x17000A20 RID: 2592
		// (get) Token: 0x06003DBC RID: 15804
		public abstract string MergeLogic { get; }

		/// <summary>When overridden in a derived class, resolves policy for the code group and its descendants for a set of evidence.</summary>
		/// <returns>A policy statement that consists of the permissions granted by the code group with optional attributes, or null if the code group does not apply (the membership condition does not match the specified evidence).</returns>
		/// <param name="evidence">The evidence for the assembly. </param>
		// Token: 0x06003DBD RID: 15805
		public abstract PolicyStatement Resolve(Evidence evidence);

		/// <summary>When overridden in a derived class, resolves matching code groups.</summary>
		/// <returns>A <see cref="T:System.Security.Policy.CodeGroup" /> that is the root of the tree of matching code groups.</returns>
		/// <param name="evidence">The evidence for the assembly. </param>
		// Token: 0x06003DBE RID: 15806
		public abstract CodeGroup ResolveMatchingCodeGroups(Evidence evidence);

		/// <summary>Gets or sets the policy statement associated with the code group.</summary>
		/// <returns>The policy statement for the code group.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x17000A21 RID: 2593
		// (get) Token: 0x06003DBF RID: 15807 RVA: 0x000DD7C9 File Offset: 0x000DB9C9
		// (set) Token: 0x06003DC0 RID: 15808 RVA: 0x000DD7D1 File Offset: 0x000DB9D1
		public PolicyStatement PolicyStatement
		{
			get
			{
				return this.m_policy;
			}
			set
			{
				this.m_policy = value;
			}
		}

		/// <summary>Gets or sets the description of the code group.</summary>
		/// <returns>The description of the code group.</returns>
		// Token: 0x17000A22 RID: 2594
		// (get) Token: 0x06003DC1 RID: 15809 RVA: 0x000DD7DA File Offset: 0x000DB9DA
		// (set) Token: 0x06003DC2 RID: 15810 RVA: 0x000DD7E2 File Offset: 0x000DB9E2
		public string Description
		{
			get
			{
				return this.m_description;
			}
			set
			{
				this.m_description = value;
			}
		}

		/// <summary>Gets or sets the code group's membership condition.</summary>
		/// <returns>The membership condition that determines to which evidence the code group is applicable.</returns>
		/// <exception cref="T:System.ArgumentNullException">An attempt is made to set this parameter to null. </exception>
		// Token: 0x17000A23 RID: 2595
		// (get) Token: 0x06003DC3 RID: 15811 RVA: 0x000DD7EB File Offset: 0x000DB9EB
		// (set) Token: 0x06003DC4 RID: 15812 RVA: 0x000DD7F3 File Offset: 0x000DB9F3
		public IMembershipCondition MembershipCondition
		{
			get
			{
				return this.m_membershipCondition;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentException("value");
				}
				this.m_membershipCondition = value;
			}
		}

		/// <summary>Gets or sets the name of the code group.</summary>
		/// <returns>The name of the code group.</returns>
		// Token: 0x17000A24 RID: 2596
		// (get) Token: 0x06003DC5 RID: 15813 RVA: 0x000DD80A File Offset: 0x000DBA0A
		// (set) Token: 0x06003DC6 RID: 15814 RVA: 0x000DD812 File Offset: 0x000DBA12
		public string Name
		{
			get
			{
				return this.m_name;
			}
			set
			{
				this.m_name = value;
			}
		}

		/// <summary>Gets or sets an ordered list of the child code groups of a code group.</summary>
		/// <returns>A list of child code groups.</returns>
		/// <exception cref="T:System.ArgumentNullException">An attempt is made to set this property to null. </exception>
		/// <exception cref="T:System.ArgumentException">An attempt is made to set this property with a list of children that are not <see cref="T:System.Security.Policy.CodeGroup" /> objects.</exception>
		// Token: 0x17000A25 RID: 2597
		// (get) Token: 0x06003DC7 RID: 15815 RVA: 0x000DD81B File Offset: 0x000DBA1B
		// (set) Token: 0x06003DC8 RID: 15816 RVA: 0x000DD823 File Offset: 0x000DBA23
		public IList Children
		{
			get
			{
				return this.m_children;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.m_children = new ArrayList(value);
			}
		}

		/// <summary>Gets a string representation of the attributes of the policy statement for the code group.</summary>
		/// <returns>A string representation of the attributes of the policy statement for the code group.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x17000A26 RID: 2598
		// (get) Token: 0x06003DC9 RID: 15817 RVA: 0x000DD83F File Offset: 0x000DBA3F
		public virtual string AttributeString
		{
			get
			{
				if (this.m_policy != null)
				{
					return this.m_policy.AttributeString;
				}
				return null;
			}
		}

		/// <summary>Gets the name of the named permission set for the code group.</summary>
		/// <returns>The name of a named permission set of the policy level.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x17000A27 RID: 2599
		// (get) Token: 0x06003DCA RID: 15818 RVA: 0x000DD856 File Offset: 0x000DBA56
		public virtual string PermissionSetName
		{
			get
			{
				if (this.m_policy == null)
				{
					return null;
				}
				if (this.m_policy.PermissionSet is NamedPermissionSet)
				{
					return ((NamedPermissionSet)this.m_policy.PermissionSet).Name;
				}
				return null;
			}
		}

		/// <summary>Adds a child code group to the current code group.</summary>
		/// <param name="group">The code group to be added as a child. This new child code group is added to the end of the list. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="group" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="group" /> parameter is not a valid code group. </exception>
		// Token: 0x06003DCB RID: 15819 RVA: 0x000DD88B File Offset: 0x000DBA8B
		public void AddChild(CodeGroup group)
		{
			if (group == null)
			{
				throw new ArgumentNullException("group");
			}
			this.m_children.Add(group.Copy());
		}

		/// <summary>Determines whether the specified code group is equivalent to the current code group.</summary>
		/// <returns>true if the specified code group is equivalent to the current code group; otherwise, false.</returns>
		/// <param name="o">The code group to compare with the current code group. </param>
		// Token: 0x06003DCC RID: 15820 RVA: 0x000DD8B0 File Offset: 0x000DBAB0
		public override bool Equals(object o)
		{
			CodeGroup codeGroup = o as CodeGroup;
			return codeGroup != null && this.Equals(codeGroup, false);
		}

		/// <summary>Determines whether the specified code group is equivalent to the current code group, checking the child code groups as well, if specified.</summary>
		/// <returns>true if the specified code group is equivalent to the current code group; otherwise, false.</returns>
		/// <param name="cg">The code group to compare with the current code group. </param>
		/// <param name="compareChildren">true to compare child code groups, as well; otherwise, false. </param>
		// Token: 0x06003DCD RID: 15821 RVA: 0x000DD8D4 File Offset: 0x000DBAD4
		public bool Equals(CodeGroup cg, bool compareChildren)
		{
			if (cg.Name != this.Name)
			{
				return false;
			}
			if (cg.Description != this.Description)
			{
				return false;
			}
			if (!cg.MembershipCondition.Equals(this.m_membershipCondition))
			{
				return false;
			}
			if (compareChildren)
			{
				int count = cg.Children.Count;
				if (this.Children.Count != count)
				{
					return false;
				}
				for (int i = 0; i < count; i++)
				{
					if (!((CodeGroup)this.Children[i]).Equals((CodeGroup)cg.Children[i], false))
					{
						return false;
					}
				}
			}
			return true;
		}

		/// <summary>Removes the specified child code group.</summary>
		/// <param name="group">The code group to be removed as a child. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="group" /> parameter is not an immediate child code group of the current code group. </exception>
		// Token: 0x06003DCE RID: 15822 RVA: 0x000DD978 File Offset: 0x000DBB78
		public void RemoveChild(CodeGroup group)
		{
			if (group != null)
			{
				this.m_children.Remove(group);
			}
		}

		/// <summary>Gets the hash code of the current code group.</summary>
		/// <returns>The hash code of the current code group.</returns>
		// Token: 0x06003DCF RID: 15823 RVA: 0x000DD98C File Offset: 0x000DBB8C
		public override int GetHashCode()
		{
			int num = this.m_membershipCondition.GetHashCode();
			if (this.m_policy != null)
			{
				num += this.m_policy.GetHashCode();
			}
			return num;
		}

		/// <summary>Reconstructs a security object with a given state from an XML encoding.</summary>
		/// <param name="e">The XML encoding to use to reconstruct the security object. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="e" /> parameter is null. </exception>
		// Token: 0x06003DD0 RID: 15824 RVA: 0x000DD9BC File Offset: 0x000DBBBC
		public void FromXml(SecurityElement e)
		{
			this.FromXml(e, null);
		}

		/// <summary>Reconstructs a security object with a given state and policy level from an XML encoding.</summary>
		/// <param name="e">The XML encoding to use to reconstruct the security object. </param>
		/// <param name="level">The policy level within which the code group exists. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="e" /> parameter is null. </exception>
		// Token: 0x06003DD1 RID: 15825 RVA: 0x000DD9C8 File Offset: 0x000DBBC8
		public void FromXml(SecurityElement e, PolicyLevel level)
		{
			if (e == null)
			{
				throw new ArgumentNullException("e");
			}
			string text = e.Attribute("PermissionSetName");
			PermissionSet permissionSet;
			if (text != null && level != null)
			{
				permissionSet = level.GetNamedPermissionSet(text);
			}
			else
			{
				SecurityElement securityElement = e.SearchForChildByTag("PermissionSet");
				if (securityElement != null)
				{
					permissionSet = (PermissionSet)Activator.CreateInstance(Type.GetType(securityElement.Attribute("class")), true);
					permissionSet.FromXml(securityElement);
				}
				else
				{
					permissionSet = new PermissionSet(new PermissionSet(PermissionState.None));
				}
			}
			this.m_policy = new PolicyStatement(permissionSet);
			this.m_children.Clear();
			if (e.Children != null && e.Children.Count > 0)
			{
				foreach (object obj in e.Children)
				{
					SecurityElement securityElement2 = (SecurityElement)obj;
					if (securityElement2.Tag == "CodeGroup")
					{
						this.AddChild(CodeGroup.CreateFromXml(securityElement2, level));
					}
				}
			}
			this.m_membershipCondition = null;
			SecurityElement securityElement3 = e.SearchForChildByTag("IMembershipCondition");
			if (securityElement3 != null)
			{
				string text2 = securityElement3.Attribute("class");
				Type type = Type.GetType(text2);
				if (type == null)
				{
					type = Type.GetType("System.Security.Policy." + text2);
				}
				this.m_membershipCondition = (IMembershipCondition)Activator.CreateInstance(type, true);
				this.m_membershipCondition.FromXml(securityElement3, level);
			}
			this.m_name = e.Attribute("Name");
			this.m_description = e.Attribute("Description");
			this.ParseXml(e, level);
		}

		/// <summary>When overridden in a derived class, reconstructs properties and internal state specific to a derived code group from the specified <see cref="T:System.Security.SecurityElement" />.</summary>
		/// <param name="e">The XML encoding to use to reconstruct the security object. </param>
		/// <param name="level">The policy level within which the code group exists. </param>
		// Token: 0x06003DD2 RID: 15826 RVA: 0x00002194 File Offset: 0x00000394
		protected virtual void ParseXml(SecurityElement e, PolicyLevel level)
		{
		}

		/// <summary>Creates an XML encoding of the security object and its current state.</summary>
		/// <returns>An XML encoding of the security object, including any state information.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06003DD3 RID: 15827 RVA: 0x000DDB70 File Offset: 0x000DBD70
		public SecurityElement ToXml()
		{
			return this.ToXml(null);
		}

		/// <summary>Creates an XML encoding of the security object, its current state, and the policy level within which the code exists.</summary>
		/// <returns>An XML encoding of the security object, including any state information.</returns>
		/// <param name="level">The policy level within which the code group exists. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06003DD4 RID: 15828 RVA: 0x000DDB7C File Offset: 0x000DBD7C
		public SecurityElement ToXml(PolicyLevel level)
		{
			SecurityElement securityElement = new SecurityElement("CodeGroup");
			securityElement.AddAttribute("class", base.GetType().AssemblyQualifiedName);
			securityElement.AddAttribute("version", "1");
			if (this.Name != null)
			{
				securityElement.AddAttribute("Name", this.Name);
			}
			if (this.Description != null)
			{
				securityElement.AddAttribute("Description", this.Description);
			}
			if (this.MembershipCondition != null)
			{
				securityElement.AddChild(this.MembershipCondition.ToXml());
			}
			if (this.PolicyStatement != null && this.PolicyStatement.PermissionSet != null)
			{
				securityElement.AddChild(this.PolicyStatement.PermissionSet.ToXml());
			}
			foreach (object obj in this.Children)
			{
				CodeGroup codeGroup = (CodeGroup)obj;
				securityElement.AddChild(codeGroup.ToXml());
			}
			this.CreateXml(securityElement, level);
			return securityElement;
		}

		/// <summary>When overridden in a derived class, serializes properties and internal state specific to a derived code group and adds the serialization to the specified <see cref="T:System.Security.SecurityElement" />.</summary>
		/// <param name="element">The XML encoding to which to add the serialization. </param>
		/// <param name="level">The policy level within which the code group exists. </param>
		// Token: 0x06003DD5 RID: 15829 RVA: 0x00002194 File Offset: 0x00000394
		protected virtual void CreateXml(SecurityElement element, PolicyLevel level)
		{
		}

		// Token: 0x06003DD6 RID: 15830 RVA: 0x000DDC8C File Offset: 0x000DBE8C
		internal static CodeGroup CreateFromXml(SecurityElement se, PolicyLevel level)
		{
			string text = se.Attribute("class");
			string text2 = text;
			int num = text2.IndexOf(",");
			if (num > 0)
			{
				text2 = text2.Substring(0, num);
			}
			num = text2.LastIndexOf(".");
			if (num > 0)
			{
				text2 = text2.Substring(num + 1);
			}
			if (text2 == "FileCodeGroup")
			{
				return new FileCodeGroup(se, level);
			}
			if (text2 == "FirstMatchCodeGroup")
			{
				return new FirstMatchCodeGroup(se, level);
			}
			if (text2 == "NetCodeGroup")
			{
				return new NetCodeGroup(se, level);
			}
			if (!(text2 == "UnionCodeGroup"))
			{
				CodeGroup codeGroup = (CodeGroup)Activator.CreateInstance(Type.GetType(text), true);
				codeGroup.FromXml(se, level);
				return codeGroup;
			}
			return new UnionCodeGroup(se, level);
		}

		// Token: 0x04001FAC RID: 8108
		private PolicyStatement m_policy;

		// Token: 0x04001FAD RID: 8109
		private IMembershipCondition m_membershipCondition;

		// Token: 0x04001FAE RID: 8110
		private string m_description;

		// Token: 0x04001FAF RID: 8111
		private string m_name;

		// Token: 0x04001FB0 RID: 8112
		private ArrayList m_children = new ArrayList();
	}
}
