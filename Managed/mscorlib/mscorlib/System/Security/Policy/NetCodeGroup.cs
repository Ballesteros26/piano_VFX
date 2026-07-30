using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Security.Policy
{
	/// <summary>Grants Web permission to the site from which the assembly was downloaded. This class cannot be inherited.</summary>
	// Token: 0x02000572 RID: 1394
	[ComVisible(true)]
	[Serializable]
	public sealed class NetCodeGroup : CodeGroup
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Policy.NetCodeGroup" /> class.</summary>
		/// <param name="membershipCondition">A membership condition that tests evidence to determine whether this code group applies code access security policy. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="membershipCondition" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The type of the <paramref name="membershipCondition" /> parameter is not valid. </exception>
		// Token: 0x06003E65 RID: 15973 RVA: 0x000DF55E File Offset: 0x000DD75E
		public NetCodeGroup(IMembershipCondition membershipCondition)
			: base(membershipCondition, null)
		{
		}

		// Token: 0x06003E66 RID: 15974 RVA: 0x000DF573 File Offset: 0x000DD773
		internal NetCodeGroup(SecurityElement e, PolicyLevel level)
			: base(e, level)
		{
		}

		/// <summary>Gets a string representation of the attributes of the policy statement for the code group.</summary>
		/// <returns>Always null.</returns>
		// Token: 0x17000A40 RID: 2624
		// (get) Token: 0x06003E67 RID: 15975 RVA: 0x0000A42E File Offset: 0x0000862E
		public override string AttributeString
		{
			get
			{
				return null;
			}
		}

		/// <summary>Gets the logic to use for merging groups.</summary>
		/// <returns>The string "Union".</returns>
		// Token: 0x17000A41 RID: 2625
		// (get) Token: 0x06003E68 RID: 15976 RVA: 0x000DE988 File Offset: 0x000DCB88
		public override string MergeLogic
		{
			get
			{
				return "Union";
			}
		}

		/// <summary>Gets the name of the <see cref="T:System.Security.NamedPermissionSet" /> for the code group.</summary>
		/// <returns>Always the string "Same site Web."</returns>
		// Token: 0x17000A42 RID: 2626
		// (get) Token: 0x06003E69 RID: 15977 RVA: 0x000DF588 File Offset: 0x000DD788
		public override string PermissionSetName
		{
			get
			{
				return "Same site Web";
			}
		}

		/// <summary>Adds the specified connection access to the current code group.</summary>
		/// <param name="originScheme">A <see cref="T:System.String" /> containing the scheme to match against the code's scheme.</param>
		/// <param name="connectAccess">A <see cref="T:System.Security.Policy.CodeConnectAccess" /> that specifies the scheme and port code can use to connect back to its origin server.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="originScheme" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="originScheme" /> contains characters that are not permitted in schemes.-or-<paramref name="originScheme" /> = <see cref="F:System.Security.Policy.NetCodeGroup.AbsentOriginScheme" /> and <paramref name="connectAccess" /> specifies <see cref="F:System.Security.Policy.CodeConnectAccess.OriginScheme" /> as its scheme.</exception>
		// Token: 0x06003E6A RID: 15978 RVA: 0x000DF590 File Offset: 0x000DD790
		[MonoTODO("(2.0) missing validations")]
		public void AddConnectAccess(string originScheme, CodeConnectAccess connectAccess)
		{
			if (originScheme == null)
			{
				throw new ArgumentException("originScheme");
			}
			if (originScheme == NetCodeGroup.AbsentOriginScheme && connectAccess.Scheme == CodeConnectAccess.OriginScheme)
			{
				throw new ArgumentOutOfRangeException("connectAccess", Locale.GetText("Schema == CodeConnectAccess.OriginScheme"));
			}
			if (this._rules.ContainsKey(originScheme))
			{
				if (connectAccess != null)
				{
					CodeConnectAccess[] array = (CodeConnectAccess[])this._rules[originScheme];
					CodeConnectAccess[] array2 = new CodeConnectAccess[array.Length + 1];
					Array.Copy(array, 0, array2, 0, array.Length);
					array2[array.Length] = connectAccess;
					this._rules[originScheme] = array2;
					return;
				}
			}
			else
			{
				CodeConnectAccess[] array3 = new CodeConnectAccess[] { connectAccess };
				this._rules.Add(originScheme, array3);
			}
		}

		/// <summary>Makes a deep copy of the current code group.</summary>
		/// <returns>An equivalent copy of the current code group, including its membership conditions and child code groups.</returns>
		// Token: 0x06003E6B RID: 15979 RVA: 0x000DF648 File Offset: 0x000DD848
		public override CodeGroup Copy()
		{
			NetCodeGroup netCodeGroup = new NetCodeGroup(base.MembershipCondition);
			netCodeGroup.Name = base.Name;
			netCodeGroup.Description = base.Description;
			netCodeGroup.PolicyStatement = base.PolicyStatement;
			foreach (object obj in base.Children)
			{
				CodeGroup codeGroup = (CodeGroup)obj;
				netCodeGroup.AddChild(codeGroup.Copy());
			}
			return netCodeGroup;
		}

		// Token: 0x06003E6C RID: 15980 RVA: 0x000DF6D8 File Offset: 0x000DD8D8
		private bool Equals(CodeConnectAccess[] rules1, CodeConnectAccess[] rules2)
		{
			for (int i = 0; i < rules1.Length; i++)
			{
				bool flag = false;
				for (int j = 0; j < rules2.Length; j++)
				{
					if (rules1[i].Equals(rules2[j]))
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Determines whether the specified code group is equivalent to the current code group.</summary>
		/// <returns>true if the specified code group is equivalent to the current code group; otherwise, false.</returns>
		/// <param name="o">The <see cref="T:System.Security.Policy.NetCodeGroup" /> object to compare with the current code group.</param>
		// Token: 0x06003E6D RID: 15981 RVA: 0x000DF71C File Offset: 0x000DD91C
		public override bool Equals(object o)
		{
			if (!base.Equals(o))
			{
				return false;
			}
			NetCodeGroup netCodeGroup = o as NetCodeGroup;
			if (netCodeGroup == null)
			{
				return false;
			}
			foreach (object obj in this._rules)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				CodeConnectAccess[] array = (CodeConnectAccess[])netCodeGroup._rules[dictionaryEntry.Key];
				bool flag;
				if (array != null)
				{
					flag = this.Equals((CodeConnectAccess[])dictionaryEntry.Value, array);
				}
				else
				{
					flag = dictionaryEntry.Value == null;
				}
				if (!flag)
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Gets the connection access information for the current code group.</summary>
		/// <returns>A <see cref="T:System.Collections.DictionaryEntry" /> array containing connection access information.</returns>
		// Token: 0x06003E6E RID: 15982 RVA: 0x000DF7D8 File Offset: 0x000DD9D8
		public DictionaryEntry[] GetConnectAccessRules()
		{
			DictionaryEntry[] array = new DictionaryEntry[this._rules.Count];
			this._rules.CopyTo(array, 0);
			return array;
		}

		/// <returns>The hash code of the current code group.</returns>
		// Token: 0x06003E6F RID: 15983 RVA: 0x000DF804 File Offset: 0x000DDA04
		public override int GetHashCode()
		{
			if (this._hashcode == 0)
			{
				this._hashcode = base.GetHashCode();
				foreach (object obj in this._rules)
				{
					CodeConnectAccess[] array = (CodeConnectAccess[])((DictionaryEntry)obj).Value;
					if (array != null)
					{
						foreach (CodeConnectAccess codeConnectAccess in array)
						{
							this._hashcode ^= codeConnectAccess.GetHashCode();
						}
					}
				}
			}
			return this._hashcode;
		}

		/// <summary>Resolves policy for the code group and its descendants for a set of evidence.</summary>
		/// <returns>A <see cref="T:System.Security.Policy.PolicyStatement" /> that consists of the permissions granted by the code group with optional attributes, or null if the code group does not apply (the membership condition does not match the specified evidence).</returns>
		/// <param name="evidence">The <see cref="T:System.Security.Policy.Evidence" /> for the assembly. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="evidence" /> parameter is null. </exception>
		/// <exception cref="T:System.Security.Policy.PolicyException">More than one code group (including the parent code group and any child code groups) is marked <see cref="F:System.Security.Policy.PolicyStatementAttribute.Exclusive" />. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06003E70 RID: 15984 RVA: 0x000DF8B4 File Offset: 0x000DDAB4
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
			PermissionSet permissionSet = null;
			if (base.PolicyStatement == null)
			{
				permissionSet = new PermissionSet(PermissionState.None);
			}
			else
			{
				permissionSet = base.PolicyStatement.PermissionSet.Copy();
			}
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

		/// <summary>Removes all connection access information for the current code group.</summary>
		// Token: 0x06003E71 RID: 15985 RVA: 0x000DF980 File Offset: 0x000DDB80
		public void ResetConnectAccess()
		{
			this._rules.Clear();
		}

		/// <summary>Resolves matching code groups.</summary>
		/// <returns>The complete set of code groups that were matched by the evidence.</returns>
		/// <param name="evidence">The evidence for the assembly. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="evidence" /> parameter is null. </exception>
		// Token: 0x06003E72 RID: 15986 RVA: 0x000DF990 File Offset: 0x000DDB90
		public override CodeGroup ResolveMatchingCodeGroups(Evidence evidence)
		{
			if (evidence == null)
			{
				throw new ArgumentNullException("evidence");
			}
			CodeGroup codeGroup = null;
			if (base.MembershipCondition.Check(evidence))
			{
				codeGroup = this.Copy();
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

		// Token: 0x06003E73 RID: 15987 RVA: 0x000DFA18 File Offset: 0x000DDC18
		[MonoTODO("(2.0) Add new stuff (CodeConnectAccess) into XML")]
		protected override void CreateXml(SecurityElement element, PolicyLevel level)
		{
			base.CreateXml(element, level);
		}

		// Token: 0x06003E74 RID: 15988 RVA: 0x000DFA22 File Offset: 0x000DDC22
		[MonoTODO("(2.0) Parse new stuff (CodeConnectAccess) from XML")]
		protected override void ParseXml(SecurityElement e, PolicyLevel level)
		{
			base.ParseXml(e, level);
		}

		/// <summary>Contains a value used to specify connection access for code with an unknown or unrecognized origin scheme.</summary>
		// Token: 0x04001FE3 RID: 8163
		public static readonly string AbsentOriginScheme = string.Empty;

		/// <summary>Contains a value used to specify any other unspecified origin scheme.</summary>
		// Token: 0x04001FE4 RID: 8164
		public static readonly string AnyOtherOriginScheme = "*";

		// Token: 0x04001FE5 RID: 8165
		private Hashtable _rules = new Hashtable();

		// Token: 0x04001FE6 RID: 8166
		private int _hashcode;
	}
}
