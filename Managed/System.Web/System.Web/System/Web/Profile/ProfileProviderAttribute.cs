using System;

namespace System.Web.Profile
{
	/// <summary>Identifies the profile provider for a user-profile property.</summary>
	// Token: 0x02000510 RID: 1296
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class ProfileProviderAttribute : Attribute
	{
		/// <summary>Creates a new instance of the <see cref="T:System.Web.Profile.ProfileProviderAttribute" /> class with the specified profile provider name.</summary>
		/// <param name="providerName">The name of the profile provider for the property.</param>
		// Token: 0x06003998 RID: 14744 RVA: 0x0009AE86 File Offset: 0x00099086
		public ProfileProviderAttribute(string providerName)
		{
			this.providerName = providerName;
		}

		/// <summary>Gets the name of the profile provider for the user-profile property.</summary>
		/// <returns>The name of the profile provider for the user-profile property.</returns>
		// Token: 0x170011DC RID: 4572
		// (get) Token: 0x06003999 RID: 14745 RVA: 0x0009AE95 File Offset: 0x00099095
		public string ProviderName
		{
			get
			{
				return this.providerName;
			}
		}

		// Token: 0x04001F36 RID: 7990
		private string providerName;
	}
}
