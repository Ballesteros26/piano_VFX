using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Security.Policy
{
	/// <summary>Provides the strong name of a code assembly as evidence for policy evaluation. This class cannot be inherited.</summary>
	// Token: 0x0200057C RID: 1404
	[ComVisible(true)]
	[Serializable]
	public sealed class StrongName : EvidenceBase, IIdentityPermissionFactory, IBuiltInEvidence
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Policy.StrongName" /> class with the strong name public key blob, name, and version.</summary>
		/// <param name="blob">The <see cref="T:System.Security.Permissions.StrongNamePublicKeyBlob" /> of the software publisher. </param>
		/// <param name="name">The simple name section of the strong name. </param>
		/// <param name="version">The <see cref="T:System.Version" /> of the strong name. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="blob" /> parameter is null.-or- The <paramref name="name" /> parameter is null.-or- The <paramref name="version" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="name" /> parameter is an empty string (""). </exception>
		// Token: 0x06003EE5 RID: 16101 RVA: 0x000E162C File Offset: 0x000DF82C
		public StrongName(StrongNamePublicKeyBlob blob, string name, Version version)
		{
			if (blob == null)
			{
				throw new ArgumentNullException("blob");
			}
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (version == null)
			{
				throw new ArgumentNullException("version");
			}
			if (name.Length == 0)
			{
				throw new ArgumentException(Locale.GetText("Empty"), "name");
			}
			this.publickey = blob;
			this.name = name;
			this.version = version;
		}

		/// <summary>Gets the simple name of the current <see cref="T:System.Security.Policy.StrongName" />.</summary>
		/// <returns>The simple name part of the <see cref="T:System.Security.Policy.StrongName" />.</returns>
		// Token: 0x17000A53 RID: 2643
		// (get) Token: 0x06003EE6 RID: 16102 RVA: 0x000E16A1 File Offset: 0x000DF8A1
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		/// <summary>Gets the <see cref="T:System.Security.Permissions.StrongNamePublicKeyBlob" /> of the current <see cref="T:System.Security.Policy.StrongName" />.</summary>
		/// <returns>The <see cref="T:System.Security.Permissions.StrongNamePublicKeyBlob" /> of the current <see cref="T:System.Security.Policy.StrongName" />.</returns>
		// Token: 0x17000A54 RID: 2644
		// (get) Token: 0x06003EE7 RID: 16103 RVA: 0x000E16A9 File Offset: 0x000DF8A9
		public StrongNamePublicKeyBlob PublicKey
		{
			get
			{
				return this.publickey;
			}
		}

		/// <summary>Gets the <see cref="T:System.Version" /> of the current <see cref="T:System.Security.Policy.StrongName" />.</summary>
		/// <returns>The <see cref="T:System.Version" /> of the current <see cref="T:System.Security.Policy.StrongName" />.</returns>
		// Token: 0x17000A55 RID: 2645
		// (get) Token: 0x06003EE8 RID: 16104 RVA: 0x000E16B1 File Offset: 0x000DF8B1
		public Version Version
		{
			get
			{
				return this.version;
			}
		}

		/// <summary>Creates an equivalent copy of the current <see cref="T:System.Security.Policy.StrongName" />.</summary>
		/// <returns>A new, identical copy of the current <see cref="T:System.Security.Policy.StrongName" />.</returns>
		// Token: 0x06003EE9 RID: 16105 RVA: 0x000E16B9 File Offset: 0x000DF8B9
		public object Copy()
		{
			return new StrongName(this.publickey, this.name, this.version);
		}

		/// <summary>Creates a <see cref="T:System.Security.Permissions.StrongNameIdentityPermission" /> that corresponds to the current <see cref="T:System.Security.Policy.StrongName" />.</summary>
		/// <returns>A <see cref="T:System.Security.Permissions.StrongNameIdentityPermission" /> for the specified <see cref="T:System.Security.Policy.StrongName" />.</returns>
		/// <param name="evidence">The <see cref="T:System.Security.Policy.Evidence" /> from which to construct the <see cref="T:System.Security.Permissions.StrongNameIdentityPermission" />. </param>
		// Token: 0x06003EEA RID: 16106 RVA: 0x000E16D2 File Offset: 0x000DF8D2
		public IPermission CreateIdentityPermission(Evidence evidence)
		{
			return new StrongNameIdentityPermission(this.publickey, this.name, this.version);
		}

		/// <summary>Determines whether the specified strong name is equal to the current strong name.</summary>
		/// <returns>true if the specified strong name is equal to the current strong name; otherwise, false.</returns>
		/// <param name="o">The strong name to compare against the current strong name. </param>
		// Token: 0x06003EEB RID: 16107 RVA: 0x000E16EC File Offset: 0x000DF8EC
		public override bool Equals(object o)
		{
			StrongName strongName = o as StrongName;
			return strongName != null && !(this.name != strongName.Name) && this.Version.Equals(strongName.Version) && this.PublicKey.Equals(strongName.PublicKey);
		}

		/// <summary>Gets the hash code of the current <see cref="T:System.Security.Policy.StrongName" />.</summary>
		/// <returns>The hash code of the current <see cref="T:System.Security.Policy.StrongName" />.</returns>
		// Token: 0x06003EEC RID: 16108 RVA: 0x000E1740 File Offset: 0x000DF940
		public override int GetHashCode()
		{
			return this.publickey.GetHashCode();
		}

		/// <summary>Creates a string representation of the current <see cref="T:System.Security.Policy.StrongName" />.</summary>
		/// <returns>A representation of the current <see cref="T:System.Security.Policy.StrongName" />.</returns>
		// Token: 0x06003EED RID: 16109 RVA: 0x000E1750 File Offset: 0x000DF950
		public override string ToString()
		{
			SecurityElement securityElement = new SecurityElement(typeof(StrongName).Name);
			securityElement.AddAttribute("version", "1");
			securityElement.AddAttribute("Key", this.publickey.ToString());
			securityElement.AddAttribute("Name", this.name);
			securityElement.AddAttribute("Version", this.version.ToString());
			return securityElement.ToString();
		}

		// Token: 0x06003EEE RID: 16110 RVA: 0x000E17C3 File Offset: 0x000DF9C3
		int IBuiltInEvidence.GetRequiredSize(bool verbose)
		{
			return (verbose ? 5 : 1) + this.name.Length;
		}

		// Token: 0x06003EEF RID: 16111 RVA: 0x00015ED5 File Offset: 0x000140D5
		[MonoTODO("IBuiltInEvidence")]
		int IBuiltInEvidence.InitFromBuffer(char[] buffer, int position)
		{
			return 0;
		}

		// Token: 0x06003EF0 RID: 16112 RVA: 0x00015ED5 File Offset: 0x000140D5
		[MonoTODO("IBuiltInEvidence")]
		int IBuiltInEvidence.OutputToBuffer(char[] buffer, int position, bool verbose)
		{
			return 0;
		}

		// Token: 0x04001FFF RID: 8191
		private StrongNamePublicKeyBlob publickey;

		// Token: 0x04002000 RID: 8192
		private string name;

		// Token: 0x04002001 RID: 8193
		private Version version;
	}
}
