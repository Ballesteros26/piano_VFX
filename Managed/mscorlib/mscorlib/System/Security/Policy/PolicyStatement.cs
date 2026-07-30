using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Security.Policy
{
	/// <summary>Represents the statement of a <see cref="T:System.Security.Policy.CodeGroup" /> describing the permissions and other information that apply to code with a particular set of evidence. This class cannot be inherited.</summary>
	// Token: 0x02000576 RID: 1398
	[ComVisible(true)]
	[Serializable]
	public sealed class PolicyStatement : ISecurityEncodable, ISecurityPolicyEncodable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Policy.PolicyStatement" /> class with the specified <see cref="T:System.Security.PermissionSet" />.</summary>
		/// <param name="permSet">The <see cref="T:System.Security.PermissionSet" /> with which to initialize the new instance. </param>
		// Token: 0x06003EA5 RID: 16037 RVA: 0x000E0CE8 File Offset: 0x000DEEE8
		public PolicyStatement(PermissionSet permSet)
			: this(permSet, PolicyStatementAttribute.Nothing)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Policy.PolicyStatement" /> class with the specified <see cref="T:System.Security.PermissionSet" /> and attributes.</summary>
		/// <param name="permSet">The <see cref="T:System.Security.PermissionSet" /> with which to initialize the new instance. </param>
		/// <param name="attributes">A bitwise combination of the <see cref="T:System.Security.Policy.PolicyStatementAttribute" /> values. </param>
		// Token: 0x06003EA6 RID: 16038 RVA: 0x000E0CF2 File Offset: 0x000DEEF2
		public PolicyStatement(PermissionSet permSet, PolicyStatementAttribute attributes)
		{
			if (permSet != null)
			{
				this.perms = permSet.Copy();
				this.perms.SetReadOnly(true);
			}
			this.attrs = attributes;
		}

		/// <summary>Gets or sets the <see cref="T:System.Security.PermissionSet" /> of the policy statement.</summary>
		/// <returns>The <see cref="T:System.Security.PermissionSet" /> of the policy statement.</returns>
		// Token: 0x17000A4C RID: 2636
		// (get) Token: 0x06003EA7 RID: 16039 RVA: 0x000E0D1C File Offset: 0x000DEF1C
		// (set) Token: 0x06003EA8 RID: 16040 RVA: 0x000E0D44 File Offset: 0x000DEF44
		public PermissionSet PermissionSet
		{
			get
			{
				if (this.perms == null)
				{
					this.perms = new PermissionSet(PermissionState.None);
					this.perms.SetReadOnly(true);
				}
				return this.perms;
			}
			set
			{
				this.perms = value;
			}
		}

		/// <summary>Gets or sets the attributes of the policy statement.</summary>
		/// <returns>The attributes of the policy statement.</returns>
		// Token: 0x17000A4D RID: 2637
		// (get) Token: 0x06003EA9 RID: 16041 RVA: 0x000E0D4D File Offset: 0x000DEF4D
		// (set) Token: 0x06003EAA RID: 16042 RVA: 0x000E0D55 File Offset: 0x000DEF55
		public PolicyStatementAttribute Attributes
		{
			get
			{
				return this.attrs;
			}
			set
			{
				if (value <= PolicyStatementAttribute.All)
				{
					this.attrs = value;
					return;
				}
				throw new ArgumentException(string.Format(Locale.GetText("Invalid value for {0}."), "PolicyStatementAttribute"));
			}
		}

		/// <summary>Gets a string representation of the attributes of the policy statement.</summary>
		/// <returns>A text string representing the attributes of the policy statement.</returns>
		// Token: 0x17000A4E RID: 2638
		// (get) Token: 0x06003EAB RID: 16043 RVA: 0x000E0D7C File Offset: 0x000DEF7C
		public string AttributeString
		{
			get
			{
				switch (this.attrs)
				{
				case PolicyStatementAttribute.Exclusive:
					return "Exclusive";
				case PolicyStatementAttribute.LevelFinal:
					return "LevelFinal";
				case PolicyStatementAttribute.All:
					return "Exclusive LevelFinal";
				default:
					return string.Empty;
				}
			}
		}

		/// <summary>Creates an equivalent copy of the current policy statement.</summary>
		/// <returns>A new copy of the <see cref="T:System.Security.Policy.PolicyStatement" /> with <see cref="P:System.Security.Policy.PolicyStatement.PermissionSet" /> and <see cref="P:System.Security.Policy.PolicyStatement.Attributes" /> identical to those of the current <see cref="T:System.Security.Policy.PolicyStatement" />.</returns>
		// Token: 0x06003EAC RID: 16044 RVA: 0x000E0DBD File Offset: 0x000DEFBD
		public PolicyStatement Copy()
		{
			return new PolicyStatement(this.perms, this.attrs);
		}

		/// <summary>Reconstructs a security object with a given state from an XML encoding.</summary>
		/// <param name="et">The XML encoding to use to reconstruct the security object. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="et" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="et" /> parameter is not a valid <see cref="T:System.Security.Policy.PolicyStatement" /> encoding. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06003EAD RID: 16045 RVA: 0x000E0DD0 File Offset: 0x000DEFD0
		public void FromXml(SecurityElement et)
		{
			this.FromXml(et, null);
		}

		/// <summary>Reconstructs a security object with a given state from an XML encoding.</summary>
		/// <param name="et">The XML encoding to use to reconstruct the security object. </param>
		/// <param name="level">The <see cref="T:System.Security.Policy.PolicyLevel" /> context for lookup of <see cref="T:System.Security.NamedPermissionSet" /> values. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="et" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="et" /> parameter is not a valid <see cref="T:System.Security.Policy.PolicyStatement" /> encoding. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06003EAE RID: 16046 RVA: 0x000E0DDC File Offset: 0x000DEFDC
		[SecuritySafeCritical]
		public void FromXml(SecurityElement et, PolicyLevel level)
		{
			if (et == null)
			{
				throw new ArgumentNullException("et");
			}
			if (et.Tag != "PolicyStatement")
			{
				throw new ArgumentException(Locale.GetText("Invalid tag."));
			}
			string text = et.Attribute("Attributes");
			if (text != null)
			{
				this.attrs = (PolicyStatementAttribute)Enum.Parse(typeof(PolicyStatementAttribute), text);
			}
			SecurityElement securityElement = et.SearchForChildByTag("PermissionSet");
			this.PermissionSet.FromXml(securityElement);
		}

		/// <summary>Creates an XML encoding of the security object and its current state.</summary>
		/// <returns>An XML encoding of the security object, including any state information.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06003EAF RID: 16047 RVA: 0x000E0E5B File Offset: 0x000DF05B
		public SecurityElement ToXml()
		{
			return this.ToXml(null);
		}

		/// <summary>Creates an XML encoding of the security object and its current state.</summary>
		/// <returns>An XML encoding of the security object, including any state information.</returns>
		/// <param name="level">The <see cref="T:System.Security.Policy.PolicyLevel" /> context for lookup of <see cref="T:System.Security.NamedPermissionSet" /> values. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06003EB0 RID: 16048 RVA: 0x000E0E64 File Offset: 0x000DF064
		public SecurityElement ToXml(PolicyLevel level)
		{
			SecurityElement securityElement = new SecurityElement("PolicyStatement");
			securityElement.AddAttribute("version", "1");
			if (this.attrs != PolicyStatementAttribute.Nothing)
			{
				securityElement.AddAttribute("Attributes", this.attrs.ToString());
			}
			securityElement.AddChild(this.PermissionSet.ToXml());
			return securityElement;
		}

		/// <summary>Determines whether the specified <see cref="T:System.Security.Policy.PolicyStatement" /> object is equal to the current <see cref="T:System.Security.Policy.PolicyStatement" />.</summary>
		/// <returns>true if the specified <see cref="T:System.Security.Policy.PolicyStatement" /> is equal to the current <see cref="T:System.Security.Policy.PolicyStatement" /> object; otherwise, false.</returns>
		/// <param name="obj">The <see cref="T:System.Security.Policy.PolicyStatement" /> object to compare with the current <see cref="T:System.Security.Policy.PolicyStatement" />. </param>
		// Token: 0x06003EB1 RID: 16049 RVA: 0x000E0EC4 File Offset: 0x000DF0C4
		[ComVisible(false)]
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			PolicyStatement policyStatement = obj as PolicyStatement;
			return policyStatement != null && this.PermissionSet.Equals(obj) && this.attrs == policyStatement.attrs;
		}

		/// <summary>Gets a hash code for the <see cref="T:System.Security.Policy.PolicyStatement" /> object that is suitable for use in hashing algorithms and data structures such as a hash table.</summary>
		/// <returns>A hash code for the current <see cref="T:System.Security.Policy.PolicyStatement" /> object.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06003EB2 RID: 16050 RVA: 0x000E0F00 File Offset: 0x000DF100
		[ComVisible(false)]
		public override int GetHashCode()
		{
			return this.PermissionSet.GetHashCode() ^ (int)this.attrs;
		}

		// Token: 0x06003EB3 RID: 16051 RVA: 0x000DD2B4 File Offset: 0x000DB4B4
		internal static PolicyStatement Empty()
		{
			return new PolicyStatement(new PermissionSet(PermissionState.None));
		}

		// Token: 0x04001FF2 RID: 8178
		private PermissionSet perms;

		// Token: 0x04001FF3 RID: 8179
		private PolicyStatementAttribute attrs;
	}
}
