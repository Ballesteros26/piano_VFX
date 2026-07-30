using System;
using System.Collections;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Security.Policy
{
	/// <summary>Determines whether an assembly belongs to a code group by testing the site from which it originated. This class cannot be inherited.</summary>
	// Token: 0x0200057B RID: 1403
	[ComVisible(true)]
	[Serializable]
	public sealed class SiteMembershipCondition : IMembershipCondition, ISecurityEncodable, ISecurityPolicyEncodable, IConstantMembershipCondition
	{
		// Token: 0x06003ED8 RID: 16088 RVA: 0x000E144F File Offset: 0x000DF64F
		internal SiteMembershipCondition()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Policy.SiteMembershipCondition" /> class with name of the site that determines membership.</summary>
		/// <param name="site">The site name or wildcard expression. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="site" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="site" /> parameter is not a valid <see cref="T:System.Security.Policy.Site" />. </exception>
		// Token: 0x06003ED9 RID: 16089 RVA: 0x000E145E File Offset: 0x000DF65E
		public SiteMembershipCondition(string site)
		{
			this.Site = site;
		}

		/// <summary>Gets or sets the site for which the membership condition tests.</summary>
		/// <returns>The site for which the membership condition tests.</returns>
		/// <exception cref="T:System.ArgumentNullException">An attempt is made to set <see cref="P:System.Security.Policy.SiteMembershipCondition.Site" /> to null. </exception>
		/// <exception cref="T:System.ArgumentException">An attempt is made to set <see cref="P:System.Security.Policy.SiteMembershipCondition.Site" /> to an invalid <see cref="T:System.Security.Policy.Site" />. </exception>
		// Token: 0x17000A52 RID: 2642
		// (get) Token: 0x06003EDA RID: 16090 RVA: 0x000E1474 File Offset: 0x000DF674
		// (set) Token: 0x06003EDB RID: 16091 RVA: 0x000E147C File Offset: 0x000DF67C
		public string Site
		{
			get
			{
				return this._site;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("site");
				}
				if (!global::System.Security.Policy.Site.IsValid(value))
				{
					throw new ArgumentException("invalid site");
				}
				this._site = value;
			}
		}

		/// <summary>Determines whether the specified evidence satisfies the membership condition.</summary>
		/// <returns>true if the specified evidence satisfies the membership condition; otherwise, false.</returns>
		/// <param name="evidence">The <see cref="T:System.Security.Policy.Evidence" /> against which to make the test. </param>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Security.Policy.SiteMembershipCondition.Site" /> property is null. </exception>
		// Token: 0x06003EDC RID: 16092 RVA: 0x000E14A8 File Offset: 0x000DF6A8
		public bool Check(Evidence evidence)
		{
			if (evidence == null)
			{
				return false;
			}
			IEnumerator hostEnumerator = evidence.GetHostEnumerator();
			while (hostEnumerator.MoveNext())
			{
				if (hostEnumerator.Current is Site)
				{
					string[] array = this._site.Split(new char[] { '.' });
					string[] array2 = (hostEnumerator.Current as Site).origin_site.Split(new char[] { '.' });
					int i = array.Length - 1;
					int num = array2.Length - 1;
					while (i >= 0)
					{
						if (i == 0)
						{
							return string.Compare(array[0], "*", true, CultureInfo.InvariantCulture) == 0;
						}
						if (string.Compare(array[i], array2[num], true, CultureInfo.InvariantCulture) != 0)
						{
							return false;
						}
						i--;
						num--;
					}
					return true;
				}
			}
			return false;
		}

		/// <summary>Creates an equivalent copy of the membership condition.</summary>
		/// <returns>A new, identical copy of the current membership condition.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Security.Policy.SiteMembershipCondition.Site" /> property is null. </exception>
		// Token: 0x06003EDD RID: 16093 RVA: 0x000E1569 File Offset: 0x000DF769
		public IMembershipCondition Copy()
		{
			return new SiteMembershipCondition(this._site);
		}

		/// <summary>Determines whether the site from the specified <see cref="T:System.Security.Policy.SiteMembershipCondition" /> object is equivalent to the site contained in the current <see cref="T:System.Security.Policy.SiteMembershipCondition" />.</summary>
		/// <returns>true if the site from the specified <see cref="T:System.Security.Policy.SiteMembershipCondition" /> object is equivalent to the site contained in the current <see cref="T:System.Security.Policy.SiteMembershipCondition" />; otherwise, false.</returns>
		/// <param name="o">The <see cref="T:System.Security.Policy.SiteMembershipCondition" /> object to compare to the current <see cref="T:System.Security.Policy.SiteMembershipCondition" />. </param>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Security.Policy.SiteMembershipCondition.Site" /> property for the current object or the specified object is null. </exception>
		// Token: 0x06003EDE RID: 16094 RVA: 0x000E1576 File Offset: 0x000DF776
		public override bool Equals(object o)
		{
			return o != null && o is SiteMembershipCondition && new Site((o as SiteMembershipCondition)._site).Equals(new Site(this._site));
		}

		/// <summary>Reconstructs a security object with a specified state from an XML encoding.</summary>
		/// <param name="e">The XML encoding to use to reconstruct the security object. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="e" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="e" /> parameter is not a valid membership condition element. </exception>
		// Token: 0x06003EDF RID: 16095 RVA: 0x000E15A7 File Offset: 0x000DF7A7
		public void FromXml(SecurityElement e)
		{
			this.FromXml(e, null);
		}

		/// <summary>Reconstructs a security object with a specified state from an XML encoding.</summary>
		/// <param name="e">The XML encoding to use to reconstruct the security object. </param>
		/// <param name="level">The <see cref="T:System.Security.Policy.PolicyLevel" /> context, used to resolve <see cref="T:System.Security.NamedPermissionSet" /> references. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="e" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="e" /> parameter is not a valid membership condition element. </exception>
		// Token: 0x06003EE0 RID: 16096 RVA: 0x000E15B1 File Offset: 0x000DF7B1
		public void FromXml(SecurityElement e, PolicyLevel level)
		{
			MembershipConditionHelper.CheckSecurityElement(e, "e", this.version, this.version);
			this._site = e.Attribute("Site");
		}

		/// <summary>Gets the hash code for the current membership condition.</summary>
		/// <returns>The hash code for the current membership condition.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Security.Policy.SiteMembershipCondition.Site" /> property is null. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06003EE1 RID: 16097 RVA: 0x000E15DC File Offset: 0x000DF7DC
		public override int GetHashCode()
		{
			return this._site.GetHashCode();
		}

		/// <summary>Creates and returns a string representation of the membership condition.</summary>
		/// <returns>A string representation of the membership condition.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Security.Policy.SiteMembershipCondition.Site" /> property is null. </exception>
		// Token: 0x06003EE2 RID: 16098 RVA: 0x000E15E9 File Offset: 0x000DF7E9
		public override string ToString()
		{
			return "Site - " + this._site;
		}

		/// <summary>Creates an XML encoding of the security object and its current state.</summary>
		/// <returns>An XML encoding of the security object, including any state information.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Security.Policy.SiteMembershipCondition.Site" /> property is null. </exception>
		// Token: 0x06003EE3 RID: 16099 RVA: 0x000E15FB File Offset: 0x000DF7FB
		public SecurityElement ToXml()
		{
			return this.ToXml(null);
		}

		/// <summary>Creates an XML encoding of the security object and its current state with the specified <see cref="T:System.Security.Policy.PolicyLevel" />.</summary>
		/// <returns>An XML encoding of the security object, including any state information.</returns>
		/// <param name="level">The <see cref="T:System.Security.Policy.PolicyLevel" /> context, used to resolve <see cref="T:System.Security.NamedPermissionSet" /> references. </param>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Security.Policy.SiteMembershipCondition.Site" /> property is null. </exception>
		// Token: 0x06003EE4 RID: 16100 RVA: 0x000E1604 File Offset: 0x000DF804
		public SecurityElement ToXml(PolicyLevel level)
		{
			SecurityElement securityElement = MembershipConditionHelper.Element(typeof(SiteMembershipCondition), this.version);
			securityElement.AddAttribute("Site", this._site);
			return securityElement;
		}

		// Token: 0x04001FFD RID: 8189
		private readonly int version = 1;

		// Token: 0x04001FFE RID: 8190
		private string _site;
	}
}
