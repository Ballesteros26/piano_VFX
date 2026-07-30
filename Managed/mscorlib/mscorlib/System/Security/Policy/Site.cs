using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Mono.Security;

namespace System.Security.Policy
{
	/// <summary>Provides the Web site from which a code assembly originates as evidence for policy evaluation. This class cannot be inherited.</summary>
	// Token: 0x0200057A RID: 1402
	[ComVisible(true)]
	[Serializable]
	public sealed class Site : EvidenceBase, IIdentityPermissionFactory, IBuiltInEvidence
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Policy.Site" /> class with the website from which a code assembly originates.</summary>
		/// <param name="name">The website of origin for the associated code assembly. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is null. </exception>
		// Token: 0x06003ECB RID: 16075 RVA: 0x000E11D1 File Offset: 0x000DF3D1
		public Site(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("url");
			}
			if (!Site.IsValid(name))
			{
				throw new ArgumentException(Locale.GetText("name is not valid"));
			}
			this.origin_site = name;
		}

		/// <summary>Creates a new <see cref="T:System.Security.Policy.Site" /> object from the specified URL.</summary>
		/// <returns>A new site object.</returns>
		/// <param name="url">The URL from which to create the new <see cref="T:System.Security.Policy.Site" /> object. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="url" /> parameter is not a valid URL. -or-The <paramref name="url" /> parameter is a file name.</exception>
		// Token: 0x06003ECC RID: 16076 RVA: 0x000E1208 File Offset: 0x000DF408
		public static Site CreateFromUrl(string url)
		{
			if (url == null)
			{
				throw new ArgumentNullException("url");
			}
			if (url.Length == 0)
			{
				throw new FormatException(Locale.GetText("Empty URL."));
			}
			string text = Site.UrlToSite(url);
			if (text == null)
			{
				throw new ArgumentException(string.Format(Locale.GetText("Invalid URL '{0}'."), url), "url");
			}
			return new Site(text);
		}

		/// <summary>Creates an equivalent copy of the <see cref="T:System.Security.Policy.Site" /> object.</summary>
		/// <returns>A new object that is identical to the current <see cref="T:System.Security.Policy.Site" /> object.</returns>
		// Token: 0x06003ECD RID: 16077 RVA: 0x000E1264 File Offset: 0x000DF464
		public object Copy()
		{
			return new Site(this.origin_site);
		}

		/// <summary>Creates an identity permission that corresponds to the current <see cref="T:System.Security.Policy.Site" /> object.</summary>
		/// <returns>A site identity permission for the current <see cref="T:System.Security.Policy.Site" /> object.</returns>
		/// <param name="evidence">The evidence from which to construct the identity permission. </param>
		// Token: 0x06003ECE RID: 16078 RVA: 0x000E1271 File Offset: 0x000DF471
		public IPermission CreateIdentityPermission(Evidence evidence)
		{
			return new SiteIdentityPermission(this.origin_site);
		}

		/// <summary>Compares the current <see cref="T:System.Security.Policy.Site" /> to the specified object for equivalence.</summary>
		/// <returns>true if the two instances of the <see cref="T:System.Security.Policy.Site" /> class are equal; otherwise, false.</returns>
		/// <param name="o">The object to test for equivalence with the current object. </param>
		// Token: 0x06003ECF RID: 16079 RVA: 0x000E1280 File Offset: 0x000DF480
		public override bool Equals(object o)
		{
			Site site = o as Site;
			return site != null && string.Compare(site.Name, this.origin_site, true, CultureInfo.InvariantCulture) == 0;
		}

		/// <summary>Returns the hash code of the current website name.</summary>
		/// <returns>The hash code of the current website name.</returns>
		// Token: 0x06003ED0 RID: 16080 RVA: 0x000E12B3 File Offset: 0x000DF4B3
		public override int GetHashCode()
		{
			return this.origin_site.GetHashCode();
		}

		/// <summary>Returns a string representation of the current <see cref="T:System.Security.Policy.Site" /> object.</summary>
		/// <returns>A representation of the current site.</returns>
		// Token: 0x06003ED1 RID: 16081 RVA: 0x000E12C0 File Offset: 0x000DF4C0
		public override string ToString()
		{
			SecurityElement securityElement = new SecurityElement("System.Security.Policy.Site");
			securityElement.AddAttribute("version", "1");
			securityElement.AddChild(new SecurityElement("Name", this.origin_site));
			return securityElement.ToString();
		}

		/// <summary>Gets the website from which the code assembly originates.</summary>
		/// <returns>The name of the website from which the code assembly originates.</returns>
		// Token: 0x17000A51 RID: 2641
		// (get) Token: 0x06003ED2 RID: 16082 RVA: 0x000E12F7 File Offset: 0x000DF4F7
		public string Name
		{
			get
			{
				return this.origin_site;
			}
		}

		// Token: 0x06003ED3 RID: 16083 RVA: 0x000E12FF File Offset: 0x000DF4FF
		int IBuiltInEvidence.GetRequiredSize(bool verbose)
		{
			return (verbose ? 3 : 1) + this.origin_site.Length;
		}

		// Token: 0x06003ED4 RID: 16084 RVA: 0x00015ED5 File Offset: 0x000140D5
		[MonoTODO("IBuiltInEvidence")]
		int IBuiltInEvidence.InitFromBuffer(char[] buffer, int position)
		{
			return 0;
		}

		// Token: 0x06003ED5 RID: 16085 RVA: 0x00015ED5 File Offset: 0x000140D5
		[MonoTODO("IBuiltInEvidence")]
		int IBuiltInEvidence.OutputToBuffer(char[] buffer, int position, bool verbose)
		{
			return 0;
		}

		// Token: 0x06003ED6 RID: 16086 RVA: 0x000E1314 File Offset: 0x000DF514
		internal static bool IsValid(string name)
		{
			if (name == string.Empty)
			{
				return false;
			}
			if (name.Length == 1 && name == ".")
			{
				return false;
			}
			string[] array = name.Split(new char[] { '.' });
			for (int i = 0; i < array.Length; i++)
			{
				string text = array[i];
				if (i != 0 || !(text == "*"))
				{
					string text2 = text;
					for (int j = 0; j < text2.Length; j++)
					{
						int num = Convert.ToInt32(text2[j]);
						if (num != 33 && num != 45 && (num < 35 || num > 41) && (num < 48 || num > 57) && (num < 64 || num > 90) && (num < 94 || num > 95) && (num < 97 || num > 123) && (num < 125 || num > 126))
						{
							return false;
						}
					}
				}
			}
			return true;
		}

		// Token: 0x06003ED7 RID: 16087 RVA: 0x000E1410 File Offset: 0x000DF610
		internal static string UrlToSite(string url)
		{
			if (url == null)
			{
				return null;
			}
			Uri uri = new Uri(url);
			if (uri.Scheme == Uri.UriSchemeFile)
			{
				return null;
			}
			string host = uri.Host;
			if (!Site.IsValid(host))
			{
				return null;
			}
			return host;
		}

		// Token: 0x04001FFC RID: 8188
		internal string origin_site;
	}
}
