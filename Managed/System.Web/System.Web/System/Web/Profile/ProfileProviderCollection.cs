using System;
using System.Configuration;
using System.Configuration.Provider;

namespace System.Web.Profile
{
	/// <summary>A collection of objects that inherit the <see cref="T:System.Web.Profile.ProfileProvider" /> abstract class.</summary>
	// Token: 0x02000511 RID: 1297
	public sealed class ProfileProviderCollection : SettingsProviderCollection
	{
		/// <summary>Adds a profile provider to the collection.</summary>
		/// <param name="provider">The profile provider to add to the collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="provider" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="provider" /> is not of a type that inherits the <see cref="T:System.Web.Profile.ProfileProvider" /> abstract class.</exception>
		// Token: 0x0600399B RID: 14747 RVA: 0x0009AEA5 File Offset: 0x000990A5
		public override void Add(ProviderBase provider)
		{
			base.Add(provider);
		}

		/// <summary>Returns the profile provider referenced by the specified provider name.</summary>
		/// <returns>An object that inherits the <see cref="T:System.Web.Profile.ProfileProvider" /> abstract class.</returns>
		/// <param name="name">The name of the profile provider.</param>
		// Token: 0x170011DD RID: 4573
		public ProfileProvider this[string name]
		{
			get
			{
				return (ProfileProvider)base[name];
			}
		}
	}
}
