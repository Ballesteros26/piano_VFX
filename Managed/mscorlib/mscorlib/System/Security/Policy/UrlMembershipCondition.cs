using System;
using System.Collections;
using System.Globalization;
using System.Runtime.InteropServices;
using Mono.Security;

namespace System.Security.Policy
{
	/// <summary>Determines whether an assembly belongs to a code group by testing its URL. This class cannot be inherited.</summary>
	// Token: 0x02000582 RID: 1410
	[ComVisible(true)]
	[Serializable]
	public sealed class UrlMembershipCondition : IMembershipCondition, ISecurityEncodable, ISecurityPolicyEncodable, IConstantMembershipCondition
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Policy.UrlMembershipCondition" /> class with the URL that determines membership.</summary>
		/// <param name="url">The URL for which to test. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="url" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="url" /> must be an absolute URL.</exception>
		// Token: 0x06003F24 RID: 16164 RVA: 0x000E1F74 File Offset: 0x000E0174
		public UrlMembershipCondition(string url)
		{
			if (url == null)
			{
				throw new ArgumentNullException("url");
			}
			this.CheckUrl(url);
			this.userUrl = url;
			this.url = new Url(url);
		}

		// Token: 0x06003F25 RID: 16165 RVA: 0x000E1FAB File Offset: 0x000E01AB
		internal UrlMembershipCondition(Url url, string userUrl)
		{
			this.url = (Url)url.Copy();
			this.userUrl = userUrl;
		}

		/// <summary>Gets or sets the URL for which the membership condition tests.</summary>
		/// <returns>The URL for which the membership condition tests.</returns>
		/// <exception cref="T:System.ArgumentNullException">An attempt is made to set <see cref="P:System.Security.Policy.UrlMembershipCondition.Url" /> to null. </exception>
		/// <exception cref="T:System.ArgumentException">Value is not an absolute URL.</exception>
		// Token: 0x17000A61 RID: 2657
		// (get) Token: 0x06003F26 RID: 16166 RVA: 0x000E1FD2 File Offset: 0x000E01D2
		// (set) Token: 0x06003F27 RID: 16167 RVA: 0x000E1FF3 File Offset: 0x000E01F3
		public string Url
		{
			get
			{
				if (this.userUrl == null)
				{
					this.userUrl = this.url.Value;
				}
				return this.userUrl;
			}
			set
			{
				this.url = new Url(value);
			}
		}

		/// <summary>Determines whether the specified evidence satisfies the membership condition.</summary>
		/// <returns>true if the specified evidence satisfies the membership condition; otherwise, false.</returns>
		/// <param name="evidence">The evidence set against which to make the test. </param>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="P:System.Security.Policy.UrlMembershipCondition.Url" /> property is null. </exception>
		// Token: 0x06003F28 RID: 16168 RVA: 0x000E2004 File Offset: 0x000E0204
		public bool Check(Evidence evidence)
		{
			if (evidence == null)
			{
				return false;
			}
			string value = this.url.Value;
			int num = value.LastIndexOf("*");
			if (num == -1)
			{
				num = value.Length;
			}
			IEnumerator hostEnumerator = evidence.GetHostEnumerator();
			while (hostEnumerator.MoveNext())
			{
				if (hostEnumerator.Current is Url && string.Compare(value, 0, (hostEnumerator.Current as Url).Value, 0, num, true, CultureInfo.InvariantCulture) == 0)
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Creates an equivalent copy of the membership condition.</summary>
		/// <returns>A new, identical copy of the current membership condition.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="P:System.Security.Policy.UrlMembershipCondition.Url" /> property is null. </exception>
		// Token: 0x06003F29 RID: 16169 RVA: 0x000E207B File Offset: 0x000E027B
		public IMembershipCondition Copy()
		{
			return new UrlMembershipCondition(this.url, this.userUrl);
		}

		/// <summary>Determines whether the URL from the specified object is equivalent to the URL contained in the current <see cref="T:System.Security.Policy.UrlMembershipCondition" />.</summary>
		/// <returns>true if the URL from the specified object is equivalent to the URL contained in the current <see cref="T:System.Security.Policy.UrlMembershipCondition" />; otherwise, false.</returns>
		/// <param name="o">The object to compare to the current <see cref="T:System.Security.Policy.UrlMembershipCondition" />. </param>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="P:System.Security.Policy.UrlMembershipCondition.Url" /> property of the current object or the specified object is null. </exception>
		// Token: 0x06003F2A RID: 16170 RVA: 0x000E2090 File Offset: 0x000E0290
		public override bool Equals(object o)
		{
			UrlMembershipCondition urlMembershipCondition = o as UrlMembershipCondition;
			if (o == null)
			{
				return false;
			}
			string value = this.url.Value;
			int num = value.Length;
			if (value[num - 1] == '*')
			{
				num--;
				if (value[num - 1] == '/')
				{
					num--;
				}
			}
			return string.Compare(value, 0, urlMembershipCondition.Url, 0, num, true, CultureInfo.InvariantCulture) == 0;
		}

		/// <summary>Reconstructs a security object with a specified state from an XML encoding.</summary>
		/// <param name="e">The XML encoding to use to reconstruct the security object. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="e" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="e" /> parameter is not a valid membership condition element. </exception>
		// Token: 0x06003F2B RID: 16171 RVA: 0x000E20F6 File Offset: 0x000E02F6
		public void FromXml(SecurityElement e)
		{
			this.FromXml(e, null);
		}

		/// <summary>Reconstructs a security object with a specified state from an XML encoding.</summary>
		/// <param name="e">The XML encoding to use to reconstruct the security object. </param>
		/// <param name="level">The policy level context, used to resolve named permission set references. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="e" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="e" /> parameter is not a valid membership condition element. </exception>
		// Token: 0x06003F2C RID: 16172 RVA: 0x000E2100 File Offset: 0x000E0300
		public void FromXml(SecurityElement e, PolicyLevel level)
		{
			MembershipConditionHelper.CheckSecurityElement(e, "e", this.version, this.version);
			string text = e.Attribute("Url");
			if (text != null)
			{
				this.CheckUrl(text);
				this.url = new Url(text);
			}
			else
			{
				this.url = null;
			}
			this.userUrl = text;
		}

		/// <summary>Gets the hash code for the current membership condition.</summary>
		/// <returns>The hash code for the current membership condition.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="P:System.Security.Policy.UrlMembershipCondition.Url" /> property is null. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06003F2D RID: 16173 RVA: 0x000E2157 File Offset: 0x000E0357
		public override int GetHashCode()
		{
			return this.url.GetHashCode();
		}

		/// <summary>Creates and returns a string representation of the membership condition.</summary>
		/// <returns>A string representation of the state of the membership condition.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="P:System.Security.Policy.UrlMembershipCondition.Url" /> property is null. </exception>
		// Token: 0x06003F2E RID: 16174 RVA: 0x000E2164 File Offset: 0x000E0364
		public override string ToString()
		{
			return "Url - " + this.Url;
		}

		/// <summary>Creates an XML encoding of the security object and its current state.</summary>
		/// <returns>An XML encoding of the security object, including any state information.</returns>
		// Token: 0x06003F2F RID: 16175 RVA: 0x000E2176 File Offset: 0x000E0376
		public SecurityElement ToXml()
		{
			return this.ToXml(null);
		}

		/// <summary>Creates an XML encoding of the security object and its current state with the specified <see cref="T:System.Security.Policy.PolicyLevel" />.</summary>
		/// <returns>An XML encoding of the security object, including any state information.</returns>
		/// <param name="level">The policy level context for resolving named permission set references. </param>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="P:System.Security.Policy.UrlMembershipCondition.Url" /> property is null. </exception>
		// Token: 0x06003F30 RID: 16176 RVA: 0x000E217F File Offset: 0x000E037F
		public SecurityElement ToXml(PolicyLevel level)
		{
			SecurityElement securityElement = MembershipConditionHelper.Element(typeof(UrlMembershipCondition), this.version);
			securityElement.AddAttribute("Url", this.userUrl);
			return securityElement;
		}

		// Token: 0x06003F31 RID: 16177 RVA: 0x000E21A8 File Offset: 0x000E03A8
		internal void CheckUrl(string url)
		{
			if (new Uri((url.IndexOf(Uri.SchemeDelimiter) < 0) ? ("file://" + url) : url, false, false).Host.IndexOf('*') >= 1)
			{
				throw new ArgumentException(Locale.GetText("Invalid * character in url"), "name");
			}
		}

		// Token: 0x04002011 RID: 8209
		private readonly int version = 1;

		// Token: 0x04002012 RID: 8210
		private Url url;

		// Token: 0x04002013 RID: 8211
		private string userUrl;
	}
}
