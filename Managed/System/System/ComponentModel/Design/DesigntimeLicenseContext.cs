using System;
using System.Collections;
using System.Reflection;
using System.Security.Permissions;

namespace System.ComponentModel.Design
{
	/// <summary>Represents a design-time license context that can support a license provider at design time.</summary>
	// Token: 0x0200031B RID: 795
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public class DesigntimeLicenseContext : LicenseContext
	{
		/// <summary>Gets the license usage mode.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.LicenseUsageMode" /> indicating the licensing mode for the context.</returns>
		// Token: 0x1700052A RID: 1322
		// (get) Token: 0x0600195C RID: 6492 RVA: 0x000027E2 File Offset: 0x000009E2
		public override LicenseUsageMode UsageMode
		{
			get
			{
				return LicenseUsageMode.Designtime;
			}
		}

		/// <summary>Gets a saved license key.</summary>
		/// <returns>The saved license key that matches the specified type.</returns>
		/// <param name="type">The type of the license key. </param>
		/// <param name="resourceAssembly">The assembly to get the key from. </param>
		// Token: 0x0600195D RID: 6493 RVA: 0x00009E57 File Offset: 0x00008057
		public override string GetSavedLicenseKey(Type type, Assembly resourceAssembly)
		{
			return null;
		}

		/// <summary>Sets a saved license key.</summary>
		/// <param name="type">The type of the license key. </param>
		/// <param name="key">The license key. </param>
		// Token: 0x0600195E RID: 6494 RVA: 0x00069E44 File Offset: 0x00068044
		public override void SetSavedLicenseKey(Type type, string key)
		{
			this.savedLicenseKeys[type.AssemblyQualifiedName] = key;
		}

		// Token: 0x0400146C RID: 5228
		internal Hashtable savedLicenseKeys = new Hashtable();
	}
}
