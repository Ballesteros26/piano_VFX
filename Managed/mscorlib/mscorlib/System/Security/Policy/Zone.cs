using System;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Mono.Security;

namespace System.Security.Policy
{
	/// <summary>Provides the security zone of a code assembly as evidence for policy evaluation. This class cannot be inherited.</summary>
	// Token: 0x02000583 RID: 1411
	[ComVisible(true)]
	[Serializable]
	public sealed class Zone : EvidenceBase, IIdentityPermissionFactory, IBuiltInEvidence
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Policy.Zone" /> class with the zone from which a code assembly originates.</summary>
		/// <param name="zone">The zone of origin for the associated code assembly. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="zone" /> parameter is not a valid <see cref="T:System.Security.SecurityZone" />. </exception>
		// Token: 0x06003F32 RID: 16178 RVA: 0x000E21FC File Offset: 0x000E03FC
		public Zone(SecurityZone zone)
		{
			if (!Enum.IsDefined(typeof(SecurityZone), zone))
			{
				throw new ArgumentException(string.Format(Locale.GetText("Invalid zone {0}."), zone), "zone");
			}
			this.zone = zone;
		}

		/// <summary>Gets the zone from which the code assembly originates.</summary>
		/// <returns>The zone from which the code assembly originates.</returns>
		// Token: 0x17000A62 RID: 2658
		// (get) Token: 0x06003F33 RID: 16179 RVA: 0x000E224D File Offset: 0x000E044D
		public SecurityZone SecurityZone
		{
			get
			{
				return this.zone;
			}
		}

		/// <summary>Creates an equivalent copy of the evidence object.</summary>
		/// <returns>A new, identical copy of the evidence object.</returns>
		// Token: 0x06003F34 RID: 16180 RVA: 0x000E2255 File Offset: 0x000E0455
		public object Copy()
		{
			return new Zone(this.zone);
		}

		/// <summary>Creates an identity permission that corresponds to the current instance of the <see cref="T:System.Security.Policy.Zone" /> evidence class.</summary>
		/// <returns>A <see cref="T:System.Security.Permissions.ZoneIdentityPermission" /> for the specified <see cref="T:System.Security.Policy.Zone" /> evidence.</returns>
		/// <param name="evidence">The evidence set from which to construct the identity permission. </param>
		// Token: 0x06003F35 RID: 16181 RVA: 0x000E2262 File Offset: 0x000E0462
		public IPermission CreateIdentityPermission(Evidence evidence)
		{
			return new ZoneIdentityPermission(this.zone);
		}

		/// <summary>Creates a new zone with the specified URL.</summary>
		/// <returns>A new zone with the specified URL.</returns>
		/// <param name="url">The URL from which to create the zone. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="url" /> parameter is null. </exception>
		// Token: 0x06003F36 RID: 16182 RVA: 0x000E2270 File Offset: 0x000E0470
		[MonoTODO("Not user configurable yet")]
		public static Zone CreateFromUrl(string url)
		{
			if (url == null)
			{
				throw new ArgumentNullException("url");
			}
			SecurityZone securityZone = SecurityZone.NoZone;
			if (url.Length == 0)
			{
				return new Zone(securityZone);
			}
			Uri uri = null;
			try
			{
				uri = new Uri(url);
			}
			catch
			{
				return new Zone(securityZone);
			}
			if (securityZone == SecurityZone.NoZone)
			{
				if (uri.IsFile)
				{
					if (File.Exists(uri.LocalPath))
					{
						securityZone = SecurityZone.MyComputer;
					}
					else if (string.Compare("FILE://", 0, url, 0, 7, true, CultureInfo.InvariantCulture) == 0)
					{
						securityZone = SecurityZone.Intranet;
					}
					else
					{
						securityZone = SecurityZone.Internet;
					}
				}
				else if (uri.IsLoopback)
				{
					securityZone = SecurityZone.Intranet;
				}
				else
				{
					securityZone = SecurityZone.Internet;
				}
			}
			return new Zone(securityZone);
		}

		/// <summary>Compares the current <see cref="T:System.Security.Policy.Zone" /> evidence object to the specified object for equivalence.</summary>
		/// <returns>true if the two <see cref="T:System.Security.Policy.Zone" /> objects are equal; otherwise, false.</returns>
		/// <param name="o">The <see cref="T:System.Security.Policy.Zone" /> evidence object to test for equivalence with the current object. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="o" /> parameter is not a <see cref="T:System.Security.Policy.Zone" /> object. </exception>
		// Token: 0x06003F37 RID: 16183 RVA: 0x000E2314 File Offset: 0x000E0514
		public override bool Equals(object o)
		{
			Zone zone = o as Zone;
			return zone != null && zone.zone == this.zone;
		}

		/// <summary>Gets the hash code of the current zone.</summary>
		/// <returns>The hash code of the current zone.</returns>
		// Token: 0x06003F38 RID: 16184 RVA: 0x000E224D File Offset: 0x000E044D
		public override int GetHashCode()
		{
			return (int)this.zone;
		}

		/// <summary>Returns a string representation of the current <see cref="T:System.Security.Policy.Zone" />.</summary>
		/// <returns>A representation of the current <see cref="T:System.Security.Policy.Zone" />.</returns>
		// Token: 0x06003F39 RID: 16185 RVA: 0x000E233C File Offset: 0x000E053C
		public override string ToString()
		{
			SecurityElement securityElement = new SecurityElement("System.Security.Policy.Zone");
			securityElement.AddAttribute("version", "1");
			securityElement.AddChild(new SecurityElement("Zone", this.zone.ToString()));
			return securityElement.ToString();
		}

		// Token: 0x06003F3A RID: 16186 RVA: 0x00028538 File Offset: 0x00026738
		int IBuiltInEvidence.GetRequiredSize(bool verbose)
		{
			return 3;
		}

		// Token: 0x06003F3B RID: 16187 RVA: 0x000E2389 File Offset: 0x000E0589
		int IBuiltInEvidence.InitFromBuffer(char[] buffer, int position)
		{
			char c = buffer[position++];
			char c2 = buffer[position++];
			return position;
		}

		// Token: 0x06003F3C RID: 16188 RVA: 0x000E239E File Offset: 0x000E059E
		int IBuiltInEvidence.OutputToBuffer(char[] buffer, int position, bool verbose)
		{
			buffer[position++] = '\u0003';
			buffer[position++] = (char)(this.zone >> 16);
			buffer[position++] = (char)(this.zone & (SecurityZone)65535);
			return position;
		}

		// Token: 0x04002014 RID: 8212
		private SecurityZone zone;
	}
}
