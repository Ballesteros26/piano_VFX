using System;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Mono.Xml;
using Unity;

namespace System.Security.Policy
{
	/// <summary>Represents the security policy levels for the common language runtime. This class cannot be inherited.</summary>
	// Token: 0x02000575 RID: 1397
	[ComVisible(true)]
	[Serializable]
	public sealed class PolicyLevel
	{
		// Token: 0x06003E83 RID: 16003 RVA: 0x000DFBFE File Offset: 0x000DDDFE
		internal PolicyLevel(string label, PolicyLevelType type)
		{
			this.label = label;
			this._type = type;
			this.full_trust_assemblies = new ArrayList();
			this.named_permission_sets = new ArrayList();
		}

		// Token: 0x06003E84 RID: 16004 RVA: 0x000DFC2C File Offset: 0x000DDE2C
		internal void LoadFromFile(string filename)
		{
			try
			{
				if (!File.Exists(filename))
				{
					string text = filename + ".default";
					if (File.Exists(text))
					{
						File.Copy(text, filename);
					}
				}
				if (File.Exists(filename))
				{
					using (StreamReader streamReader = File.OpenText(filename))
					{
						this.xml = this.FromString(streamReader.ReadToEnd());
					}
					try
					{
						SecurityManager.ResolvingPolicyLevel = this;
						this.FromXml(this.xml);
						goto IL_008A;
					}
					finally
					{
						SecurityManager.ResolvingPolicyLevel = this;
					}
				}
				this.CreateDefaultFullTrustAssemblies();
				this.CreateDefaultNamedPermissionSets();
				this.CreateDefaultLevel(this._type);
				this.Save();
				IL_008A:;
			}
			catch
			{
			}
			finally
			{
				this._location = filename;
			}
		}

		// Token: 0x06003E85 RID: 16005 RVA: 0x000DFD04 File Offset: 0x000DDF04
		internal void LoadFromString(string xml)
		{
			this.FromXml(this.FromString(xml));
		}

		// Token: 0x06003E86 RID: 16006 RVA: 0x000DFD14 File Offset: 0x000DDF14
		private SecurityElement FromString(string xml)
		{
			SecurityParser securityParser = new SecurityParser();
			securityParser.LoadXml(xml);
			SecurityElement securityElement = securityParser.ToXml();
			if (securityElement.Tag != "configuration")
			{
				throw new ArgumentException(Locale.GetText("missing <configuration> root element"));
			}
			SecurityElement securityElement2 = (SecurityElement)securityElement.Children[0];
			if (securityElement2.Tag != "mscorlib")
			{
				throw new ArgumentException(Locale.GetText("missing <mscorlib> tag"));
			}
			SecurityElement securityElement3 = (SecurityElement)securityElement2.Children[0];
			if (securityElement3.Tag != "security")
			{
				throw new ArgumentException(Locale.GetText("missing <security> tag"));
			}
			SecurityElement securityElement4 = (SecurityElement)securityElement3.Children[0];
			if (securityElement4.Tag != "policy")
			{
				throw new ArgumentException(Locale.GetText("missing <policy> tag"));
			}
			return (SecurityElement)securityElement4.Children[0];
		}

		/// <summary>Gets a list of <see cref="T:System.Security.Policy.StrongNameMembershipCondition" /> objects used to determine whether an assembly is a member of the group of assemblies used to evaluate security policy.</summary>
		/// <returns>A list of <see cref="T:System.Security.Policy.StrongNameMembershipCondition" /> objects used to determine whether an assembly is a member of the group of assemblies used to evaluate security policy. These assemblies are granted full trust during security policy evaluation of assemblies not in the list.</returns>
		// Token: 0x17000A46 RID: 2630
		// (get) Token: 0x06003E87 RID: 16007 RVA: 0x000DFDFA File Offset: 0x000DDFFA
		[Obsolete("All GACed assemblies are now fully trusted and all permissions now succeed on fully trusted code.")]
		public IList FullTrustAssemblies
		{
			get
			{
				return this.full_trust_assemblies;
			}
		}

		/// <summary>Gets a descriptive label for the policy level.</summary>
		/// <returns>The label associated with the policy level.</returns>
		// Token: 0x17000A47 RID: 2631
		// (get) Token: 0x06003E88 RID: 16008 RVA: 0x000DFE02 File Offset: 0x000DE002
		public string Label
		{
			get
			{
				return this.label;
			}
		}

		/// <summary>Gets a list of named permission sets defined for the policy level.</summary>
		/// <returns>A list of named permission sets defined for the policy level.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x17000A48 RID: 2632
		// (get) Token: 0x06003E89 RID: 16009 RVA: 0x000DFE0A File Offset: 0x000DE00A
		public IList NamedPermissionSets
		{
			get
			{
				return this.named_permission_sets;
			}
		}

		/// <summary>Gets or sets the root code group for the policy level.</summary>
		/// <returns>The <see cref="T:System.Security.Policy.CodeGroup" /> that is the root of the tree of policy level code groups.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value for <see cref="P:System.Security.Policy.PolicyLevel.RootCodeGroup" /> is null.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlPolicy" />
		/// </PermissionSet>
		// Token: 0x17000A49 RID: 2633
		// (get) Token: 0x06003E8A RID: 16010 RVA: 0x000DFE12 File Offset: 0x000DE012
		// (set) Token: 0x06003E8B RID: 16011 RVA: 0x000DFE1A File Offset: 0x000DE01A
		public CodeGroup RootCodeGroup
		{
			get
			{
				return this.root_code_group;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.root_code_group = value;
			}
		}

		/// <summary>Gets the path where the policy file is stored.</summary>
		/// <returns>The path where the policy file is stored, or null if the <see cref="T:System.Security.Policy.PolicyLevel" /> does not have a storage location.</returns>
		// Token: 0x17000A4A RID: 2634
		// (get) Token: 0x06003E8C RID: 16012 RVA: 0x000DFE31 File Offset: 0x000DE031
		public string StoreLocation
		{
			get
			{
				return this._location;
			}
		}

		/// <summary>Gets the type of the policy level.</summary>
		/// <returns>One of the <see cref="T:System.Security.PolicyLevelType" /> values.</returns>
		// Token: 0x17000A4B RID: 2635
		// (get) Token: 0x06003E8D RID: 16013 RVA: 0x000DFE39 File Offset: 0x000DE039
		[ComVisible(false)]
		public PolicyLevelType Type
		{
			get
			{
				return this._type;
			}
		}

		/// <summary>Adds a <see cref="T:System.Security.Policy.StrongNameMembershipCondition" /> corresponding to the specified <see cref="T:System.Security.Policy.StrongName" /> to the list of <see cref="T:System.Security.Policy.StrongNameMembershipCondition" /> objects used to determine whether an assembly is a member of the group of assemblies that should not be evaluated.</summary>
		/// <param name="sn">The <see cref="T:System.Security.Policy.StrongName" /> used to create the <see cref="T:System.Security.Policy.StrongNameMembershipCondition" /> to add to the list of <see cref="T:System.Security.Policy.StrongNameMembershipCondition" /> objects used to determine whether an assembly is a member of the group of assemblies that should not be evaluated. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="sn" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <see cref="T:System.Security.Policy.StrongName" /> specified by the <paramref name="sn" /> parameter already has full trust. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06003E8E RID: 16014 RVA: 0x000DFE44 File Offset: 0x000DE044
		[Obsolete("All GACed assemblies are now fully trusted and all permissions now succeed on fully trusted code.")]
		public void AddFullTrustAssembly(StrongName sn)
		{
			if (sn == null)
			{
				throw new ArgumentNullException("sn");
			}
			StrongNameMembershipCondition strongNameMembershipCondition = new StrongNameMembershipCondition(sn.PublicKey, sn.Name, sn.Version);
			this.AddFullTrustAssembly(strongNameMembershipCondition);
		}

		/// <summary>Adds the specified <see cref="T:System.Security.Policy.StrongNameMembershipCondition" /> to the list of <see cref="T:System.Security.Policy.StrongNameMembershipCondition" /> objects used to determine whether an assembly is a member of the group of assemblies that should not be evaluated.</summary>
		/// <param name="snMC">The <see cref="T:System.Security.Policy.StrongNameMembershipCondition" /> to add to the list of <see cref="T:System.Security.Policy.StrongNameMembershipCondition" /> objects used to determine whether an assembly is a member of the group of assemblies that should not be evaluated. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="snMC" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <see cref="T:System.Security.Policy.StrongNameMembershipCondition" /> specified by the <paramref name="snMC" /> parameter already has full trust. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06003E8F RID: 16015 RVA: 0x000DFE80 File Offset: 0x000DE080
		[Obsolete("All GACed assemblies are now fully trusted and all permissions now succeed on fully trusted code.")]
		public void AddFullTrustAssembly(StrongNameMembershipCondition snMC)
		{
			if (snMC == null)
			{
				throw new ArgumentNullException("snMC");
			}
			using (IEnumerator enumerator = this.full_trust_assemblies.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (((StrongNameMembershipCondition)enumerator.Current).Equals(snMC))
					{
						throw new ArgumentException(Locale.GetText("sn already has full trust."));
					}
				}
			}
			this.full_trust_assemblies.Add(snMC);
		}

		/// <summary>Adds a <see cref="T:System.Security.NamedPermissionSet" /> to the current policy level.</summary>
		/// <param name="permSet">The <see cref="T:System.Security.NamedPermissionSet" /> to add to the current policy level. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="permSet" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="permSet" /> parameter has the same name as an existing <see cref="T:System.Security.NamedPermissionSet" /> in the <see cref="T:System.Security.Policy.PolicyLevel" />. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06003E90 RID: 16016 RVA: 0x000DFF04 File Offset: 0x000DE104
		public void AddNamedPermissionSet(NamedPermissionSet permSet)
		{
			if (permSet == null)
			{
				throw new ArgumentNullException("permSet");
			}
			foreach (object obj in this.named_permission_sets)
			{
				NamedPermissionSet namedPermissionSet = (NamedPermissionSet)obj;
				if (permSet.Name == namedPermissionSet.Name)
				{
					throw new ArgumentException(Locale.GetText("This NamedPermissionSet is the same an existing NamedPermissionSet."));
				}
			}
			this.named_permission_sets.Add(permSet.Copy());
		}

		/// <summary>Replaces a <see cref="T:System.Security.NamedPermissionSet" /> in the current policy level with the specified <see cref="T:System.Security.PermissionSet" />.</summary>
		/// <returns>A copy of the <see cref="T:System.Security.NamedPermissionSet" /> that was replaced.</returns>
		/// <param name="name">The name of the <see cref="T:System.Security.NamedPermissionSet" /> to replace. </param>
		/// <param name="pSet">The <see cref="T:System.Security.PermissionSet" /> that replaces the <see cref="T:System.Security.NamedPermissionSet" /> specified by the <paramref name="name" /> parameter. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is null.-or- The <paramref name="pSet" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="name" /> parameter is equal to the name of a reserved permission set.-or- The <see cref="T:System.Security.PermissionSet" /> specified by the <paramref name="pSet" /> parameter cannot be found. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06003E91 RID: 16017 RVA: 0x000DFF9C File Offset: 0x000DE19C
		public NamedPermissionSet ChangeNamedPermissionSet(string name, PermissionSet pSet)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (pSet == null)
			{
				throw new ArgumentNullException("pSet");
			}
			if (DefaultPolicies.ReservedNames.IsReserved(name))
			{
				throw new ArgumentException(Locale.GetText("Reserved name"));
			}
			foreach (object obj in this.named_permission_sets)
			{
				NamedPermissionSet namedPermissionSet = (NamedPermissionSet)obj;
				if (name == namedPermissionSet.Name)
				{
					this.named_permission_sets.Remove(namedPermissionSet);
					this.AddNamedPermissionSet(new NamedPermissionSet(name, pSet));
					return namedPermissionSet;
				}
			}
			throw new ArgumentException(Locale.GetText("PermissionSet not found"));
		}

		/// <summary>Creates a new policy level for use at the application domain policy level.</summary>
		/// <returns>The newly created <see cref="T:System.Security.Policy.PolicyLevel" />.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06003E92 RID: 16018 RVA: 0x000E0060 File Offset: 0x000DE260
		public static PolicyLevel CreateAppDomainLevel()
		{
			UnionCodeGroup unionCodeGroup = new UnionCodeGroup(new AllMembershipCondition(), new PolicyStatement(DefaultPolicies.FullTrust));
			unionCodeGroup.Name = "All_Code";
			PolicyLevel policyLevel = new PolicyLevel("AppDomain", PolicyLevelType.AppDomain);
			policyLevel.RootCodeGroup = unionCodeGroup;
			policyLevel.Reset();
			return policyLevel;
		}

		/// <summary>Reconstructs a security object with a given state from an XML encoding.</summary>
		/// <param name="e">The XML encoding to use to reconstruct the security object. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="e" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <see cref="T:System.Security.SecurityElement" /> specified by the <paramref name="e" /> parameter is invalid. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06003E93 RID: 16019 RVA: 0x000E00A8 File Offset: 0x000DE2A8
		public void FromXml(SecurityElement e)
		{
			if (e == null)
			{
				throw new ArgumentNullException("e");
			}
			SecurityElement securityElement = e.SearchForChildByTag("SecurityClasses");
			if (securityElement != null && securityElement.Children != null && securityElement.Children.Count > 0)
			{
				this.fullNames = new Hashtable(securityElement.Children.Count);
				foreach (object obj in securityElement.Children)
				{
					SecurityElement securityElement2 = (SecurityElement)obj;
					this.fullNames.Add(securityElement2.Attributes["Name"], securityElement2.Attributes["Description"]);
				}
			}
			SecurityElement securityElement3 = e.SearchForChildByTag("FullTrustAssemblies");
			if (securityElement3 != null && securityElement3.Children != null && securityElement3.Children.Count > 0)
			{
				this.full_trust_assemblies.Clear();
				foreach (object obj2 in securityElement3.Children)
				{
					SecurityElement securityElement4 = (SecurityElement)obj2;
					if (securityElement4.Tag != "IMembershipCondition")
					{
						throw new ArgumentException(Locale.GetText("Invalid XML"));
					}
					if (securityElement4.Attribute("class").IndexOf("StrongNameMembershipCondition") < 0)
					{
						throw new ArgumentException(Locale.GetText("Invalid XML - must be StrongNameMembershipCondition"));
					}
					this.full_trust_assemblies.Add(new StrongNameMembershipCondition(securityElement4));
				}
			}
			SecurityElement securityElement5 = e.SearchForChildByTag("CodeGroup");
			if (securityElement5 != null && securityElement5.Children != null && securityElement5.Children.Count > 0)
			{
				this.root_code_group = CodeGroup.CreateFromXml(securityElement5, this);
				SecurityElement securityElement6 = e.SearchForChildByTag("NamedPermissionSets");
				if (securityElement6 != null && securityElement6.Children != null && securityElement6.Children.Count > 0)
				{
					this.named_permission_sets.Clear();
					foreach (object obj3 in securityElement6.Children)
					{
						SecurityElement securityElement7 = (SecurityElement)obj3;
						NamedPermissionSet namedPermissionSet = new NamedPermissionSet();
						namedPermissionSet.Resolver = this;
						namedPermissionSet.FromXml(securityElement7);
						this.named_permission_sets.Add(namedPermissionSet);
					}
				}
				return;
			}
			throw new ArgumentException(Locale.GetText("Missing Root CodeGroup"));
		}

		/// <summary>Returns the <see cref="T:System.Security.NamedPermissionSet" /> in the current policy level with the specified name.</summary>
		/// <returns>The <see cref="T:System.Security.NamedPermissionSet" /> in the current policy level with the specified name, if found; otherwise, null.</returns>
		/// <param name="name">The name of the <see cref="T:System.Security.NamedPermissionSet" /> to find. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is null. </exception>
		// Token: 0x06003E94 RID: 16020 RVA: 0x000E0348 File Offset: 0x000DE548
		public NamedPermissionSet GetNamedPermissionSet(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			foreach (object obj in this.named_permission_sets)
			{
				NamedPermissionSet namedPermissionSet = (NamedPermissionSet)obj;
				if (namedPermissionSet.Name == name)
				{
					return (NamedPermissionSet)namedPermissionSet.Copy();
				}
			}
			return null;
		}

		/// <summary>Replaces the configuration file for this <see cref="T:System.Security.Policy.PolicyLevel" /> with the last backup (reflecting the state of policy prior to the last time it was saved) and returns it to the state of the last save.</summary>
		/// <exception cref="T:System.Security.Policy.PolicyException">The policy level does not have a valid configuration file. </exception>
		// Token: 0x06003E95 RID: 16021 RVA: 0x000E03C8 File Offset: 0x000DE5C8
		public void Recover()
		{
			if (this._location == null)
			{
				throw new PolicyException(Locale.GetText("Only file based policies may be recovered."));
			}
			string text = this._location + ".backup";
			if (!File.Exists(text))
			{
				throw new PolicyException(Locale.GetText("No policy backup exists."));
			}
			try
			{
				File.Copy(text, this._location, true);
			}
			catch (Exception ex)
			{
				throw new PolicyException(Locale.GetText("Couldn't replace the policy file with it's backup."), ex);
			}
		}

		/// <summary>Removes an assembly with the specified <see cref="T:System.Security.Policy.StrongName" /> from the list of assemblies the policy level uses to evaluate policy.</summary>
		/// <param name="sn">The <see cref="T:System.Security.Policy.StrongName" /> of the assembly to remove from the list of assemblies used to evaluate policy. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="sn" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The assembly with the <see cref="T:System.Security.Policy.StrongName" /> specified by the <paramref name="sn" /> parameter does not have full trust. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06003E96 RID: 16022 RVA: 0x000E0448 File Offset: 0x000DE648
		[Obsolete("All GACed assemblies are now fully trusted and all permissions now succeed on fully trusted code.")]
		public void RemoveFullTrustAssembly(StrongName sn)
		{
			if (sn == null)
			{
				throw new ArgumentNullException("sn");
			}
			StrongNameMembershipCondition strongNameMembershipCondition = new StrongNameMembershipCondition(sn.PublicKey, sn.Name, sn.Version);
			this.RemoveFullTrustAssembly(strongNameMembershipCondition);
		}

		/// <summary>Removes an assembly with the specified <see cref="T:System.Security.Policy.StrongNameMembershipCondition" /> from the list of assemblies the policy level uses to evaluate policy.</summary>
		/// <param name="snMC">The <see cref="T:System.Security.Policy.StrongNameMembershipCondition" /> of the assembly to remove from the list of assemblies used to evaluate policy. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="snMC" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <see cref="T:System.Security.Policy.StrongNameMembershipCondition" /> specified by the <paramref name="snMC" /> parameter does not have full trust. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06003E97 RID: 16023 RVA: 0x000E0482 File Offset: 0x000DE682
		[Obsolete("All GACed assemblies are now fully trusted and all permissions now succeed on fully trusted code.")]
		public void RemoveFullTrustAssembly(StrongNameMembershipCondition snMC)
		{
			if (snMC == null)
			{
				throw new ArgumentNullException("snMC");
			}
			if (((IList)this.full_trust_assemblies).Contains(snMC))
			{
				((IList)this.full_trust_assemblies).Remove(snMC);
				return;
			}
			throw new ArgumentException(Locale.GetText("sn does not have full trust."));
		}

		/// <summary>Removes the specified <see cref="T:System.Security.NamedPermissionSet" /> from the current policy level.</summary>
		/// <returns>The <see cref="T:System.Security.NamedPermissionSet" /> that was removed.</returns>
		/// <param name="permSet">The <see cref="T:System.Security.NamedPermissionSet" /> to remove from the current policy level. </param>
		/// <exception cref="T:System.ArgumentException">The <see cref="T:System.Security.NamedPermissionSet" /> specified by the <paramref name="permSet" /> parameter was not found. </exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="permSet" /> parameter is null. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06003E98 RID: 16024 RVA: 0x000E04BC File Offset: 0x000DE6BC
		public NamedPermissionSet RemoveNamedPermissionSet(NamedPermissionSet permSet)
		{
			if (permSet == null)
			{
				throw new ArgumentNullException("permSet");
			}
			return this.RemoveNamedPermissionSet(permSet.Name);
		}

		/// <summary>Removes the <see cref="T:System.Security.NamedPermissionSet" /> with the specified name from the current policy level.</summary>
		/// <returns>The <see cref="T:System.Security.NamedPermissionSet" /> that was removed.</returns>
		/// <param name="name">The name of the <see cref="T:System.Security.NamedPermissionSet" /> to remove. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="name" /> parameter is equal to the name of a reserved permission set.-or- A <see cref="T:System.Security.NamedPermissionSet" /> with the specified name cannot be found. </exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is null. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06003E99 RID: 16025 RVA: 0x000E04D8 File Offset: 0x000DE6D8
		public NamedPermissionSet RemoveNamedPermissionSet(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (DefaultPolicies.ReservedNames.IsReserved(name))
			{
				throw new ArgumentException(Locale.GetText("Reserved name"));
			}
			foreach (object obj in this.named_permission_sets)
			{
				NamedPermissionSet namedPermissionSet = (NamedPermissionSet)obj;
				if (name == namedPermissionSet.Name)
				{
					this.named_permission_sets.Remove(namedPermissionSet);
					return namedPermissionSet;
				}
			}
			throw new ArgumentException(string.Format(Locale.GetText("Name '{0}' cannot be found."), name), "name");
		}

		/// <summary>Returns the current policy level to the default state.</summary>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence, ControlPolicy" />
		/// </PermissionSet>
		// Token: 0x06003E9A RID: 16026 RVA: 0x000E058C File Offset: 0x000DE78C
		public void Reset()
		{
			if (this.fullNames != null)
			{
				this.fullNames.Clear();
			}
			if (this._type != PolicyLevelType.AppDomain)
			{
				this.full_trust_assemblies.Clear();
				this.named_permission_sets.Clear();
				if (this._location != null && File.Exists(this._location))
				{
					try
					{
						File.Delete(this._location);
					}
					catch
					{
					}
				}
				this.LoadFromFile(this._location);
				return;
			}
			this.CreateDefaultFullTrustAssemblies();
			this.CreateDefaultNamedPermissionSets();
		}

		/// <summary>Resolves policy based on evidence for the policy level, and returns the resulting <see cref="T:System.Security.Policy.PolicyStatement" />.</summary>
		/// <returns>The resulting <see cref="T:System.Security.Policy.PolicyStatement" />.</returns>
		/// <param name="evidence">The <see cref="T:System.Security.Policy.Evidence" /> used to resolve the <see cref="T:System.Security.Policy.PolicyLevel" />. </param>
		/// <exception cref="T:System.Security.Policy.PolicyException">The policy level contains multiple matching code groups marked as exclusive. </exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="evidence" /> parameter is null. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06003E9B RID: 16027 RVA: 0x000E061C File Offset: 0x000DE81C
		public PolicyStatement Resolve(Evidence evidence)
		{
			if (evidence == null)
			{
				throw new ArgumentNullException("evidence");
			}
			PolicyStatement policyStatement = this.root_code_group.Resolve(evidence);
			if (policyStatement == null)
			{
				return PolicyStatement.Empty();
			}
			return policyStatement;
		}

		/// <summary>Resolves policy at the policy level and returns the root of a code group tree that matches the evidence.</summary>
		/// <returns>A <see cref="T:System.Security.Policy.CodeGroup" /> representing the root of a tree of code groups matching the specified evidence.</returns>
		/// <param name="evidence">The <see cref="T:System.Security.Policy.Evidence" /> used to resolve policy. </param>
		/// <exception cref="T:System.Security.Policy.PolicyException">The policy level contains multiple matching code groups marked as exclusive. </exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="evidence" /> parameter is null. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06003E9C RID: 16028 RVA: 0x000E0650 File Offset: 0x000DE850
		public CodeGroup ResolveMatchingCodeGroups(Evidence evidence)
		{
			if (evidence == null)
			{
				throw new ArgumentNullException("evidence");
			}
			CodeGroup codeGroup = this.root_code_group.ResolveMatchingCodeGroups(evidence);
			if (codeGroup == null)
			{
				return null;
			}
			return codeGroup;
		}

		/// <summary>Creates an XML encoding of the security object and its current state.</summary>
		/// <returns>An XML encoding of the security object, including any state information.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06003E9D RID: 16029 RVA: 0x000E0680 File Offset: 0x000DE880
		public SecurityElement ToXml()
		{
			Hashtable hashtable = new Hashtable();
			if (this.full_trust_assemblies.Count > 0 && !hashtable.Contains("StrongNameMembershipCondition"))
			{
				hashtable.Add("StrongNameMembershipCondition", typeof(StrongNameMembershipCondition).FullName);
			}
			SecurityElement securityElement = new SecurityElement("NamedPermissionSets");
			foreach (object obj in this.named_permission_sets)
			{
				NamedPermissionSet namedPermissionSet = (NamedPermissionSet)obj;
				SecurityElement securityElement2 = namedPermissionSet.ToXml();
				object obj2 = securityElement2.Attributes["class"];
				if (!hashtable.Contains(obj2))
				{
					hashtable.Add(obj2, namedPermissionSet.GetType().FullName);
				}
				securityElement.AddChild(securityElement2);
			}
			SecurityElement securityElement3 = new SecurityElement("FullTrustAssemblies");
			foreach (object obj3 in this.full_trust_assemblies)
			{
				StrongNameMembershipCondition strongNameMembershipCondition = (StrongNameMembershipCondition)obj3;
				securityElement3.AddChild(strongNameMembershipCondition.ToXml(this));
			}
			SecurityElement securityElement4 = new SecurityElement("SecurityClasses");
			if (hashtable.Count > 0)
			{
				foreach (object obj4 in hashtable)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj4;
					SecurityElement securityElement5 = new SecurityElement("SecurityClass");
					securityElement5.AddAttribute("Name", (string)dictionaryEntry.Key);
					securityElement5.AddAttribute("Description", (string)dictionaryEntry.Value);
					securityElement4.AddChild(securityElement5);
				}
			}
			SecurityElement securityElement6 = new SecurityElement(typeof(PolicyLevel).Name);
			securityElement6.AddAttribute("version", "1");
			securityElement6.AddChild(securityElement4);
			securityElement6.AddChild(securityElement);
			if (this.root_code_group != null)
			{
				securityElement6.AddChild(this.root_code_group.ToXml(this));
			}
			securityElement6.AddChild(securityElement3);
			return securityElement6;
		}

		// Token: 0x06003E9E RID: 16030 RVA: 0x000E08BC File Offset: 0x000DEABC
		internal void Save()
		{
			if (this._type == PolicyLevelType.AppDomain)
			{
				throw new PolicyException(Locale.GetText("Can't save AppDomain PolicyLevel"));
			}
			if (this._location != null)
			{
				try
				{
					if (File.Exists(this._location))
					{
						File.Copy(this._location, this._location + ".backup", true);
					}
				}
				catch (Exception)
				{
				}
				finally
				{
					using (StreamWriter streamWriter = new StreamWriter(this._location))
					{
						streamWriter.Write(this.ToXml().ToString());
						streamWriter.Close();
					}
				}
			}
		}

		// Token: 0x06003E9F RID: 16031 RVA: 0x000E0970 File Offset: 0x000DEB70
		internal void CreateDefaultLevel(PolicyLevelType type)
		{
			PolicyStatement policyStatement = new PolicyStatement(DefaultPolicies.FullTrust);
			switch (type)
			{
			case PolicyLevelType.User:
			case PolicyLevelType.Enterprise:
			case PolicyLevelType.AppDomain:
				this.root_code_group = new UnionCodeGroup(new AllMembershipCondition(), policyStatement);
				this.root_code_group.Name = "All_Code";
				return;
			case PolicyLevelType.Machine:
			{
				PolicyStatement policyStatement2 = new PolicyStatement(DefaultPolicies.Nothing);
				this.root_code_group = new UnionCodeGroup(new AllMembershipCondition(), policyStatement2);
				this.root_code_group.Name = "All_Code";
				UnionCodeGroup unionCodeGroup = new UnionCodeGroup(new ZoneMembershipCondition(SecurityZone.MyComputer), policyStatement);
				unionCodeGroup.Name = "My_Computer_Zone";
				this.root_code_group.AddChild(unionCodeGroup);
				UnionCodeGroup unionCodeGroup2 = new UnionCodeGroup(new ZoneMembershipCondition(SecurityZone.Intranet), new PolicyStatement(DefaultPolicies.LocalIntranet));
				unionCodeGroup2.Name = "LocalIntranet_Zone";
				this.root_code_group.AddChild(unionCodeGroup2);
				PolicyStatement policyStatement3 = new PolicyStatement(DefaultPolicies.Internet);
				UnionCodeGroup unionCodeGroup3 = new UnionCodeGroup(new ZoneMembershipCondition(SecurityZone.Internet), policyStatement3);
				unionCodeGroup3.Name = "Internet_Zone";
				this.root_code_group.AddChild(unionCodeGroup3);
				UnionCodeGroup unionCodeGroup4 = new UnionCodeGroup(new ZoneMembershipCondition(SecurityZone.Untrusted), policyStatement2);
				unionCodeGroup4.Name = "Restricted_Zone";
				this.root_code_group.AddChild(unionCodeGroup4);
				UnionCodeGroup unionCodeGroup5 = new UnionCodeGroup(new ZoneMembershipCondition(SecurityZone.Trusted), policyStatement3);
				unionCodeGroup5.Name = "Trusted_Zone";
				this.root_code_group.AddChild(unionCodeGroup5);
				return;
			}
			default:
				return;
			}
		}

		// Token: 0x06003EA0 RID: 16032 RVA: 0x000E0AC4 File Offset: 0x000DECC4
		internal void CreateDefaultFullTrustAssemblies()
		{
			this.full_trust_assemblies.Clear();
			this.full_trust_assemblies.Add(DefaultPolicies.FullTrustMembership("mscorlib", DefaultPolicies.Key.Ecma));
			this.full_trust_assemblies.Add(DefaultPolicies.FullTrustMembership("System", DefaultPolicies.Key.Ecma));
			this.full_trust_assemblies.Add(DefaultPolicies.FullTrustMembership("System.Data", DefaultPolicies.Key.Ecma));
			this.full_trust_assemblies.Add(DefaultPolicies.FullTrustMembership("System.DirectoryServices", DefaultPolicies.Key.MsFinal));
			this.full_trust_assemblies.Add(DefaultPolicies.FullTrustMembership("System.Drawing", DefaultPolicies.Key.MsFinal));
			this.full_trust_assemblies.Add(DefaultPolicies.FullTrustMembership("System.Messaging", DefaultPolicies.Key.MsFinal));
			this.full_trust_assemblies.Add(DefaultPolicies.FullTrustMembership("System.ServiceProcess", DefaultPolicies.Key.MsFinal));
		}

		// Token: 0x06003EA1 RID: 16033 RVA: 0x000E0B80 File Offset: 0x000DED80
		internal void CreateDefaultNamedPermissionSets()
		{
			this.named_permission_sets.Clear();
			try
			{
				SecurityManager.ResolvingPolicyLevel = this;
				this.named_permission_sets.Add(DefaultPolicies.LocalIntranet);
				this.named_permission_sets.Add(DefaultPolicies.Internet);
				this.named_permission_sets.Add(DefaultPolicies.SkipVerification);
				this.named_permission_sets.Add(DefaultPolicies.Execution);
				this.named_permission_sets.Add(DefaultPolicies.Nothing);
				this.named_permission_sets.Add(DefaultPolicies.Everything);
				this.named_permission_sets.Add(DefaultPolicies.FullTrust);
			}
			finally
			{
				SecurityManager.ResolvingPolicyLevel = null;
			}
		}

		// Token: 0x06003EA2 RID: 16034 RVA: 0x000E0C30 File Offset: 0x000DEE30
		internal string ResolveClassName(string className)
		{
			if (this.fullNames != null)
			{
				object obj = this.fullNames[className];
				if (obj != null)
				{
					return (string)obj;
				}
			}
			return className;
		}

		// Token: 0x06003EA3 RID: 16035 RVA: 0x000E0C60 File Offset: 0x000DEE60
		internal bool IsFullTrustAssembly(Assembly a)
		{
			AssemblyName name = a.GetName();
			StrongNameMembershipCondition strongNameMembershipCondition = new StrongNameMembershipCondition(new StrongNamePublicKeyBlob(name.GetPublicKey()), name.Name, name.Version);
			using (IEnumerator enumerator = this.full_trust_assemblies.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (((StrongNameMembershipCondition)enumerator.Current).Equals(strongNameMembershipCondition))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06003EA4 RID: 16036 RVA: 0x0001FB35 File Offset: 0x0001DD35
		internal PolicyLevel()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04001FEA RID: 8170
		private string label;

		// Token: 0x04001FEB RID: 8171
		private CodeGroup root_code_group;

		// Token: 0x04001FEC RID: 8172
		private ArrayList full_trust_assemblies;

		// Token: 0x04001FED RID: 8173
		private ArrayList named_permission_sets;

		// Token: 0x04001FEE RID: 8174
		private string _location;

		// Token: 0x04001FEF RID: 8175
		private PolicyLevelType _type;

		// Token: 0x04001FF0 RID: 8176
		private Hashtable fullNames;

		// Token: 0x04001FF1 RID: 8177
		private SecurityElement xml;
	}
}
