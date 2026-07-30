using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Mono.Security;

namespace System.Security.Policy
{
	/// <summary>Provides the URL from which a code assembly originates as evidence for policy evaluation. This class cannot be inherited.</summary>
	// Token: 0x02000581 RID: 1409
	[ComVisible(true)]
	[Serializable]
	public sealed class Url : EvidenceBase, IIdentityPermissionFactory, IBuiltInEvidence
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Policy.Url" /> class with the URL from which a code assembly originates.</summary>
		/// <param name="name">The URL of origin for the associated code assembly. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is null. </exception>
		// Token: 0x06003F18 RID: 16152 RVA: 0x000E1DAC File Offset: 0x000DFFAC
		public Url(string name)
			: this(name, false)
		{
		}

		// Token: 0x06003F19 RID: 16153 RVA: 0x000E1DB6 File Offset: 0x000DFFB6
		internal Url(string name, bool validated)
		{
			this.origin_url = (validated ? name : this.Prepare(name));
		}

		/// <summary>Creates a new copy of the evidence object.</summary>
		/// <returns>A new, identical copy of the evidence object.</returns>
		// Token: 0x06003F1A RID: 16154 RVA: 0x000E1DD1 File Offset: 0x000DFFD1
		public object Copy()
		{
			return new Url(this.origin_url, true);
		}

		/// <summary>Creates an identity permission corresponding to the current instance of the <see cref="T:System.Security.Policy.Url" /> evidence class.</summary>
		/// <returns>A <see cref="T:System.Security.Permissions.UrlIdentityPermission" /> for the specified <see cref="T:System.Security.Policy.Url" /> evidence.</returns>
		/// <param name="evidence">The evidence set from which to construct the identity permission. </param>
		// Token: 0x06003F1B RID: 16155 RVA: 0x000E1DDF File Offset: 0x000DFFDF
		public IPermission CreateIdentityPermission(Evidence evidence)
		{
			return new UrlIdentityPermission(this.origin_url);
		}

		/// <summary>Compares the current <see cref="T:System.Security.Policy.Url" /> evidence object to the specified object for equivalence.</summary>
		/// <returns>true if the two <see cref="T:System.Security.Policy.Url" /> objects are equal; otherwise, false.</returns>
		/// <param name="o">The <see cref="T:System.Security.Policy.Url" /> evidence object to test for equivalence with the current object. </param>
		// Token: 0x06003F1C RID: 16156 RVA: 0x000E1DEC File Offset: 0x000DFFEC
		public override bool Equals(object o)
		{
			Url url = o as Url;
			if (url == null)
			{
				return false;
			}
			string text = url.Value;
			string text2 = this.origin_url;
			if (text.IndexOf(Uri.SchemeDelimiter) < 0)
			{
				text = "file://" + text;
			}
			if (text2.IndexOf(Uri.SchemeDelimiter) < 0)
			{
				text2 = "file://" + text2;
			}
			return string.Compare(text, text2, true, CultureInfo.InvariantCulture) == 0;
		}

		/// <summary>Gets the hash code of the current URL.</summary>
		/// <returns>The hash code of the current URL.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06003F1D RID: 16157 RVA: 0x000E1E58 File Offset: 0x000E0058
		public override int GetHashCode()
		{
			string text = this.origin_url;
			if (text.IndexOf(Uri.SchemeDelimiter) < 0)
			{
				text = "file://" + text;
			}
			return text.GetHashCode();
		}

		/// <summary>Returns a string representation of the current <see cref="T:System.Security.Policy.Url" />.</summary>
		/// <returns>A representation of the current <see cref="T:System.Security.Policy.Url" />.</returns>
		// Token: 0x06003F1E RID: 16158 RVA: 0x000E1E8C File Offset: 0x000E008C
		public override string ToString()
		{
			SecurityElement securityElement = new SecurityElement("System.Security.Policy.Url");
			securityElement.AddAttribute("version", "1");
			securityElement.AddChild(new SecurityElement("Url", this.origin_url));
			return securityElement.ToString();
		}

		/// <summary>Gets the URL from which the code assembly originates.</summary>
		/// <returns>The URL from which the code assembly originates.</returns>
		// Token: 0x17000A60 RID: 2656
		// (get) Token: 0x06003F1F RID: 16159 RVA: 0x000E1EC3 File Offset: 0x000E00C3
		public string Value
		{
			get
			{
				return this.origin_url;
			}
		}

		// Token: 0x06003F20 RID: 16160 RVA: 0x000E1ECB File Offset: 0x000E00CB
		int IBuiltInEvidence.GetRequiredSize(bool verbose)
		{
			return (verbose ? 3 : 1) + this.origin_url.Length;
		}

		// Token: 0x06003F21 RID: 16161 RVA: 0x00015ED5 File Offset: 0x000140D5
		[MonoTODO("IBuiltInEvidence")]
		int IBuiltInEvidence.InitFromBuffer(char[] buffer, int position)
		{
			return 0;
		}

		// Token: 0x06003F22 RID: 16162 RVA: 0x00015ED5 File Offset: 0x000140D5
		[MonoTODO("IBuiltInEvidence")]
		int IBuiltInEvidence.OutputToBuffer(char[] buffer, int position, bool verbose)
		{
			return 0;
		}

		// Token: 0x06003F23 RID: 16163 RVA: 0x000E1EE0 File Offset: 0x000E00E0
		private string Prepare(string url)
		{
			if (url == null)
			{
				throw new ArgumentNullException("Url");
			}
			if (url == string.Empty)
			{
				throw new FormatException(Locale.GetText("Invalid (empty) Url"));
			}
			if (url.IndexOf(Uri.SchemeDelimiter) > 0)
			{
				if (url.StartsWith("file://"))
				{
					url = "file://" + url.Substring(7);
				}
				url = new Uri(url, false, false).ToString();
			}
			int num = url.Length - 1;
			if (url[num] == '/')
			{
				url = url.Substring(0, num);
			}
			return url;
		}

		// Token: 0x04002010 RID: 8208
		private string origin_url;
	}
}
