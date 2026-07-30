using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;

namespace System.Security.Policy
{
	/// <summary>Determines whether an assembly belongs to a code group by testing its strong name. This class cannot be inherited.</summary>
	// Token: 0x0200057D RID: 1405
	[ComVisible(true)]
	[Serializable]
	public sealed class StrongNameMembershipCondition : IMembershipCondition, ISecurityEncodable, ISecurityPolicyEncodable, IConstantMembershipCondition
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Policy.StrongNameMembershipCondition" /> class with the strong name public key blob, name, and version number that determine membership.</summary>
		/// <param name="blob">The strong name public key blob of the software publisher. </param>
		/// <param name="name">The simple name section of the strong name. </param>
		/// <param name="version">The version number of the strong name. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="blob" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="name" /> parameter is null.-or-The <paramref name="name" /> parameter is an empty string ("").</exception>
		// Token: 0x06003EF1 RID: 16113 RVA: 0x000E17D8 File Offset: 0x000DF9D8
		public StrongNameMembershipCondition(StrongNamePublicKeyBlob blob, string name, Version version)
		{
			if (blob == null)
			{
				throw new ArgumentNullException("blob");
			}
			this.blob = blob;
			this.name = name;
			if (version != null)
			{
				this.assemblyVersion = (Version)version.Clone();
			}
		}

		// Token: 0x06003EF2 RID: 16114 RVA: 0x000E1828 File Offset: 0x000DFA28
		internal StrongNameMembershipCondition(SecurityElement e)
		{
			this.FromXml(e);
		}

		// Token: 0x06003EF3 RID: 16115 RVA: 0x000E183E File Offset: 0x000DFA3E
		internal StrongNameMembershipCondition()
		{
		}

		/// <summary>Gets or sets the simple name of the <see cref="T:System.Security.Policy.StrongName" /> for which the membership condition tests.</summary>
		/// <returns>The simple name of the <see cref="T:System.Security.Policy.StrongName" /> for which the membership condition tests.</returns>
		/// <exception cref="T:System.ArgumentException">The value is null.-or-The value is an empty string ("").</exception>
		// Token: 0x17000A56 RID: 2646
		// (get) Token: 0x06003EF4 RID: 16116 RVA: 0x000E184D File Offset: 0x000DFA4D
		// (set) Token: 0x06003EF5 RID: 16117 RVA: 0x000E1855 File Offset: 0x000DFA55
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Version" /> of the <see cref="T:System.Security.Policy.StrongName" /> for which the membership condition tests.</summary>
		/// <returns>The <see cref="T:System.Version" /> of the <see cref="T:System.Security.Policy.StrongName" /> for which the membership condition tests.</returns>
		// Token: 0x17000A57 RID: 2647
		// (get) Token: 0x06003EF6 RID: 16118 RVA: 0x000E185E File Offset: 0x000DFA5E
		// (set) Token: 0x06003EF7 RID: 16119 RVA: 0x000E1866 File Offset: 0x000DFA66
		public Version Version
		{
			get
			{
				return this.assemblyVersion;
			}
			set
			{
				this.assemblyVersion = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Security.Permissions.StrongNamePublicKeyBlob" /> of the <see cref="T:System.Security.Policy.StrongName" /> for which the membership condition tests.</summary>
		/// <returns>The <see cref="T:System.Security.Permissions.StrongNamePublicKeyBlob" /> of the <see cref="T:System.Security.Policy.StrongName" /> for which the membership condition tests.</returns>
		/// <exception cref="T:System.ArgumentNullException">An attempt is made to set the <see cref="P:System.Security.Policy.StrongNameMembershipCondition.PublicKey" /> to null. </exception>
		// Token: 0x17000A58 RID: 2648
		// (get) Token: 0x06003EF8 RID: 16120 RVA: 0x000E186F File Offset: 0x000DFA6F
		// (set) Token: 0x06003EF9 RID: 16121 RVA: 0x000E1877 File Offset: 0x000DFA77
		public StrongNamePublicKeyBlob PublicKey
		{
			get
			{
				return this.blob;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("PublicKey");
				}
				this.blob = value;
			}
		}

		/// <summary>Determines whether the specified evidence satisfies the membership condition.</summary>
		/// <returns>true if the specified evidence satisfies the membership condition; otherwise, false.</returns>
		/// <param name="evidence">The <see cref="T:System.Security.Policy.Evidence" /> against which to make the test. </param>
		// Token: 0x06003EFA RID: 16122 RVA: 0x000E1890 File Offset: 0x000DFA90
		public bool Check(Evidence evidence)
		{
			if (evidence == null)
			{
				return false;
			}
			IEnumerator hostEnumerator = evidence.GetHostEnumerator();
			while (hostEnumerator.MoveNext())
			{
				object obj = hostEnumerator.Current;
				StrongName strongName = obj as StrongName;
				if (strongName != null)
				{
					return strongName.PublicKey.Equals(this.blob) && (this.name == null || !(this.name != strongName.Name)) && (!(this.assemblyVersion != null) || this.assemblyVersion.Equals(strongName.Version));
				}
			}
			return false;
		}

		/// <summary>Creates an equivalent copy of the current <see cref="T:System.Security.Policy.StrongNameMembershipCondition" />.</summary>
		/// <returns>A new, identical copy of the current <see cref="T:System.Security.Policy.StrongNameMembershipCondition" /></returns>
		// Token: 0x06003EFB RID: 16123 RVA: 0x000E191A File Offset: 0x000DFB1A
		public IMembershipCondition Copy()
		{
			return new StrongNameMembershipCondition(this.blob, this.name, this.assemblyVersion);
		}

		/// <summary>Determines whether the <see cref="T:System.Security.Policy.StrongName" /> from the specified object is equivalent to the <see cref="T:System.Security.Policy.StrongName" /> contained in the current <see cref="T:System.Security.Policy.StrongNameMembershipCondition" />.</summary>
		/// <returns>true if the <see cref="T:System.Security.Policy.StrongName" /> from the specified object is equivalent to the <see cref="T:System.Security.Policy.StrongName" /> contained in the current <see cref="T:System.Security.Policy.StrongNameMembershipCondition" />; otherwise, false.</returns>
		/// <param name="o">The object to compare to the current <see cref="T:System.Security.Policy.StrongNameMembershipCondition" />. </param>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Security.Policy.StrongNameMembershipCondition.PublicKey" /> property of the current object or the specified object is null. </exception>
		// Token: 0x06003EFC RID: 16124 RVA: 0x000E1934 File Offset: 0x000DFB34
		public override bool Equals(object o)
		{
			StrongNameMembershipCondition strongNameMembershipCondition = o as StrongNameMembershipCondition;
			if (strongNameMembershipCondition == null)
			{
				return false;
			}
			if (!strongNameMembershipCondition.PublicKey.Equals(this.PublicKey))
			{
				return false;
			}
			if (this.name != strongNameMembershipCondition.Name)
			{
				return false;
			}
			if (this.assemblyVersion != null)
			{
				return this.assemblyVersion.Equals(strongNameMembershipCondition.Version);
			}
			return strongNameMembershipCondition.Version == null;
		}

		/// <summary>Returns the hash code for the current <see cref="T:System.Security.Policy.StrongNameMembershipCondition" />.</summary>
		/// <returns>The hash code for the current <see cref="T:System.Security.Policy.StrongNameMembershipCondition" />.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Security.Policy.StrongNameMembershipCondition.PublicKey" /> property is null. </exception>
		// Token: 0x06003EFD RID: 16125 RVA: 0x000E19A3 File Offset: 0x000DFBA3
		public override int GetHashCode()
		{
			return this.blob.GetHashCode();
		}

		/// <summary>Reconstructs a security object with a specified state from an XML encoding.</summary>
		/// <param name="e">The XML encoding to use to reconstruct the security object. </param>
		// Token: 0x06003EFE RID: 16126 RVA: 0x000E19B0 File Offset: 0x000DFBB0
		public void FromXml(SecurityElement e)
		{
			this.FromXml(e, null);
		}

		/// <summary>Reconstructs a security object with a specified state from an XML encoding.</summary>
		/// <param name="e">The XML encoding to use to reconstruct the security object. </param>
		/// <param name="level">The <see cref="T:System.Security.Policy.PolicyLevel" /> context, used to resolve <see cref="T:System.Security.NamedPermissionSet" /> references. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="e" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="e" /> parameter is not a valid membership condition element. </exception>
		// Token: 0x06003EFF RID: 16127 RVA: 0x000E19BC File Offset: 0x000DFBBC
		public void FromXml(SecurityElement e, PolicyLevel level)
		{
			MembershipConditionHelper.CheckSecurityElement(e, "e", this.version, this.version);
			this.blob = StrongNamePublicKeyBlob.FromString(e.Attribute("PublicKeyBlob"));
			this.name = e.Attribute("Name");
			string text = e.Attribute("AssemblyVersion");
			if (text == null)
			{
				this.assemblyVersion = null;
				return;
			}
			this.assemblyVersion = new Version(text);
		}

		/// <summary>Creates and returns a string representation of the current <see cref="T:System.Security.Policy.StrongNameMembershipCondition" />.</summary>
		/// <returns>A representation of the current <see cref="T:System.Security.Policy.StrongNameMembershipCondition" />.</returns>
		// Token: 0x06003F00 RID: 16128 RVA: 0x000E1A2C File Offset: 0x000DFC2C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder("StrongName - ");
			stringBuilder.Append(this.blob);
			if (this.name != null)
			{
				stringBuilder.AppendFormat(" name = {0}", this.name);
			}
			if (this.assemblyVersion != null)
			{
				stringBuilder.AppendFormat(" version = {0}", this.assemblyVersion);
			}
			return stringBuilder.ToString();
		}

		/// <summary>Creates an XML encoding of the security object and its current state.</summary>
		/// <returns>An XML encoding of the security object, including any state information.</returns>
		// Token: 0x06003F01 RID: 16129 RVA: 0x000E1A91 File Offset: 0x000DFC91
		public SecurityElement ToXml()
		{
			return this.ToXml(null);
		}

		/// <summary>Creates an XML encoding of the security object and its current state with the specified <see cref="T:System.Security.Policy.PolicyLevel" />.</summary>
		/// <returns>An XML encoding of the security object, including any state information.</returns>
		/// <param name="level">The <see cref="T:System.Security.Policy.PolicyLevel" /> context, which is used to resolve <see cref="T:System.Security.NamedPermissionSet" /> references. </param>
		// Token: 0x06003F02 RID: 16130 RVA: 0x000E1A9C File Offset: 0x000DFC9C
		public SecurityElement ToXml(PolicyLevel level)
		{
			SecurityElement securityElement = MembershipConditionHelper.Element(typeof(StrongNameMembershipCondition), this.version);
			if (this.blob != null)
			{
				securityElement.AddAttribute("PublicKeyBlob", this.blob.ToString());
			}
			if (this.name != null)
			{
				securityElement.AddAttribute("Name", this.name);
			}
			if (this.assemblyVersion != null)
			{
				string text = this.assemblyVersion.ToString();
				if (text != "0.0")
				{
					securityElement.AddAttribute("AssemblyVersion", text);
				}
			}
			return securityElement;
		}

		// Token: 0x04002002 RID: 8194
		private readonly int version = 1;

		// Token: 0x04002003 RID: 8195
		private StrongNamePublicKeyBlob blob;

		// Token: 0x04002004 RID: 8196
		private string name;

		// Token: 0x04002005 RID: 8197
		private Version assemblyVersion;
	}
}
