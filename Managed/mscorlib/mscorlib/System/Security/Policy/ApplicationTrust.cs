using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Permissions;
using Mono.Security.Cryptography;

namespace System.Security.Policy
{
	/// <summary>Encapsulates security decisions about an application. This class cannot be inherited.</summary>
	// Token: 0x02000559 RID: 1369
	[ComVisible(true)]
	[Serializable]
	public sealed class ApplicationTrust : EvidenceBase, ISecurityEncodable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Policy.ApplicationTrust" /> class.</summary>
		// Token: 0x06003D86 RID: 15750 RVA: 0x000DCEF6 File Offset: 0x000DB0F6
		public ApplicationTrust()
		{
			this.fullTrustAssemblies = new List<StrongName>(0);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Policy.ApplicationTrust" /> class with an <see cref="T:System.ApplicationIdentity" />. </summary>
		/// <param name="applicationIdentity">An <see cref="T:System.ApplicationIdentity" /> that uniquely identifies an application.</param>
		// Token: 0x06003D87 RID: 15751 RVA: 0x000DCF0A File Offset: 0x000DB10A
		public ApplicationTrust(ApplicationIdentity applicationIdentity)
			: this()
		{
			if (applicationIdentity == null)
			{
				throw new ArgumentNullException("applicationIdentity");
			}
			this._appid = applicationIdentity;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Policy.ApplicationTrust" /> class using the provided grant set and collection of full-trust assemblies.</summary>
		/// <param name="defaultGrantSet">A default permission set that is granted to all assemblies that do not have specific grants.</param>
		/// <param name="fullTrustAssemblies">An array of strong names that represent assemblies that should be considered fully trusted in an application domain.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="defaultGrantSet" /> is null.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="fullTrustAssemblies" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="fullTrustAssemblies" /> contains an assembly that does not have a <see cref="T:System.Security.Policy.StrongName" />.</exception>
		// Token: 0x06003D88 RID: 15752 RVA: 0x000DCF28 File Offset: 0x000DB128
		public ApplicationTrust(PermissionSet defaultGrantSet, IEnumerable<StrongName> fullTrustAssemblies)
		{
			if (defaultGrantSet == null)
			{
				throw new ArgumentNullException("defaultGrantSet");
			}
			this._defaultPolicy = new PolicyStatement(defaultGrantSet);
			if (fullTrustAssemblies == null)
			{
				throw new ArgumentNullException("fullTrustAssemblies");
			}
			this.fullTrustAssemblies = new List<StrongName>();
			foreach (StrongName strongName in fullTrustAssemblies)
			{
				if (strongName == null)
				{
					throw new ArgumentException("fullTrustAssemblies contains an assembly that does not have a StrongName");
				}
				this.fullTrustAssemblies.Add((StrongName)strongName.Copy());
			}
		}

		/// <summary>Gets or sets the application identity for the application trust object.</summary>
		/// <returns>An <see cref="T:System.ApplicationIdentity" /> for the application trust object.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <see cref="T:System.ApplicationIdentity" /> cannot be set because it has a value of null.</exception>
		// Token: 0x17000A11 RID: 2577
		// (get) Token: 0x06003D89 RID: 15753 RVA: 0x000DCFC8 File Offset: 0x000DB1C8
		// (set) Token: 0x06003D8A RID: 15754 RVA: 0x000DCFD0 File Offset: 0x000DB1D0
		public ApplicationIdentity ApplicationIdentity
		{
			get
			{
				return this._appid;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("ApplicationIdentity");
				}
				this._appid = value;
			}
		}

		/// <summary>Gets or sets the policy statement defining the default grant set.</summary>
		/// <returns>A <see cref="T:System.Security.Policy.PolicyStatement" /> describing the default grants.</returns>
		// Token: 0x17000A12 RID: 2578
		// (get) Token: 0x06003D8B RID: 15755 RVA: 0x000DCFE7 File Offset: 0x000DB1E7
		// (set) Token: 0x06003D8C RID: 15756 RVA: 0x000DD003 File Offset: 0x000DB203
		public PolicyStatement DefaultGrantSet
		{
			get
			{
				if (this._defaultPolicy == null)
				{
					this._defaultPolicy = this.GetDefaultGrantSet();
				}
				return this._defaultPolicy;
			}
			set
			{
				this._defaultPolicy = value;
			}
		}

		/// <summary>Gets or sets extra security information about the application.</summary>
		/// <returns>An object containing additional security information about the application.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000A13 RID: 2579
		// (get) Token: 0x06003D8D RID: 15757 RVA: 0x000DD00C File Offset: 0x000DB20C
		// (set) Token: 0x06003D8E RID: 15758 RVA: 0x000DD014 File Offset: 0x000DB214
		public object ExtraInfo
		{
			get
			{
				return this._xtranfo;
			}
			set
			{
				this._xtranfo = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the application has the required permission grants and is trusted to run.</summary>
		/// <returns>true if the application is trusted to run; otherwise, false. The default is false.</returns>
		// Token: 0x17000A14 RID: 2580
		// (get) Token: 0x06003D8F RID: 15759 RVA: 0x000DD01D File Offset: 0x000DB21D
		// (set) Token: 0x06003D90 RID: 15760 RVA: 0x000DD025 File Offset: 0x000DB225
		public bool IsApplicationTrustedToRun
		{
			get
			{
				return this._trustrun;
			}
			set
			{
				this._trustrun = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether application trust information is persisted.</summary>
		/// <returns>true if application trust information is persisted; otherwise, false. The default is false.</returns>
		// Token: 0x17000A15 RID: 2581
		// (get) Token: 0x06003D91 RID: 15761 RVA: 0x000DD02E File Offset: 0x000DB22E
		// (set) Token: 0x06003D92 RID: 15762 RVA: 0x000DD036 File Offset: 0x000DB236
		public bool Persist
		{
			get
			{
				return this._persist;
			}
			set
			{
				this._persist = value;
			}
		}

		/// <summary>Reconstructs an <see cref="T:System.Security.Policy.ApplicationTrust" /> object with a given state from an XML encoding.</summary>
		/// <param name="element">The XML encoding to use to reconstruct the <see cref="T:System.Security.Policy.ApplicationTrust" /> object. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="element" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">The XML encoding used for <paramref name="element" /> is invalid.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06003D93 RID: 15763 RVA: 0x000DD040 File Offset: 0x000DB240
		public void FromXml(SecurityElement element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			if (element.Tag != "ApplicationTrust")
			{
				throw new ArgumentException("element");
			}
			string text = element.Attribute("FullName");
			if (text != null)
			{
				this._appid = new ApplicationIdentity(text);
			}
			else
			{
				this._appid = null;
			}
			this._defaultPolicy = null;
			SecurityElement securityElement = element.SearchForChildByTag("DefaultGrant");
			if (securityElement != null)
			{
				for (int i = 0; i < securityElement.Children.Count; i++)
				{
					SecurityElement securityElement2 = securityElement.Children[i] as SecurityElement;
					if (securityElement2.Tag == "PolicyStatement")
					{
						this.DefaultGrantSet.FromXml(securityElement2, null);
						break;
					}
				}
			}
			if (!bool.TryParse(element.Attribute("TrustedToRun"), out this._trustrun))
			{
				this._trustrun = false;
			}
			if (!bool.TryParse(element.Attribute("Persist"), out this._persist))
			{
				this._persist = false;
			}
			this._xtranfo = null;
			SecurityElement securityElement3 = element.SearchForChildByTag("ExtraInfo");
			if (securityElement3 != null)
			{
				text = securityElement3.Attribute("Data");
				if (text != null)
				{
					using (MemoryStream memoryStream = new MemoryStream(CryptoConvert.FromHex(text)))
					{
						BinaryFormatter binaryFormatter = new BinaryFormatter();
						this._xtranfo = binaryFormatter.Deserialize(memoryStream);
					}
				}
			}
		}

		/// <summary>Creates an XML encoding of the <see cref="T:System.Security.Policy.ApplicationTrust" /> object and its current state.</summary>
		/// <returns>An XML encoding of the security object, including any state information.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06003D94 RID: 15764 RVA: 0x000DD1A4 File Offset: 0x000DB3A4
		public SecurityElement ToXml()
		{
			SecurityElement securityElement = new SecurityElement("ApplicationTrust");
			securityElement.AddAttribute("version", "1");
			if (this._appid != null)
			{
				securityElement.AddAttribute("FullName", this._appid.FullName);
			}
			if (this._trustrun)
			{
				securityElement.AddAttribute("TrustedToRun", "true");
			}
			if (this._persist)
			{
				securityElement.AddAttribute("Persist", "true");
			}
			SecurityElement securityElement2 = new SecurityElement("DefaultGrant");
			securityElement2.AddChild(this.DefaultGrantSet.ToXml());
			securityElement.AddChild(securityElement2);
			if (this._xtranfo != null)
			{
				byte[] array = null;
				using (MemoryStream memoryStream = new MemoryStream())
				{
					new BinaryFormatter().Serialize(memoryStream, this._xtranfo);
					array = memoryStream.ToArray();
				}
				SecurityElement securityElement3 = new SecurityElement("ExtraInfo");
				securityElement3.AddAttribute("Data", CryptoConvert.ToHex(array));
				securityElement.AddChild(securityElement3);
			}
			return securityElement;
		}

		/// <summary>Gets the list of full-trust assemblies for this application trust.</summary>
		/// <returns>A list of full-trust assemblies.</returns>
		// Token: 0x17000A16 RID: 2582
		// (get) Token: 0x06003D95 RID: 15765 RVA: 0x000DD2AC File Offset: 0x000DB4AC
		public IList<StrongName> FullTrustAssemblies
		{
			get
			{
				return this.fullTrustAssemblies;
			}
		}

		// Token: 0x06003D96 RID: 15766 RVA: 0x000DD2B4 File Offset: 0x000DB4B4
		private PolicyStatement GetDefaultGrantSet()
		{
			return new PolicyStatement(new PermissionSet(PermissionState.None));
		}

		// Token: 0x04001F9B RID: 8091
		private ApplicationIdentity _appid;

		// Token: 0x04001F9C RID: 8092
		private PolicyStatement _defaultPolicy;

		// Token: 0x04001F9D RID: 8093
		private object _xtranfo;

		// Token: 0x04001F9E RID: 8094
		private bool _trustrun;

		// Token: 0x04001F9F RID: 8095
		private bool _persist;

		// Token: 0x04001FA0 RID: 8096
		private IList<StrongName> fullTrustAssemblies;
	}
}
