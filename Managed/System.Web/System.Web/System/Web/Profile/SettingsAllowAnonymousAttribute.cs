using System;

namespace System.Web.Profile
{
	/// <summary>Identifies whether a profile property can be set or accessed for an anonymous user.</summary>
	// Token: 0x02000512 RID: 1298
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class SettingsAllowAnonymousAttribute : Attribute
	{
		/// <summary>Creates a new instance of the <see cref="T:System.Web.Profile.SettingsAllowAnonymousAttribute" /> class and specifies whether to allow anonymous access to the associated profile property.</summary>
		/// <param name="allow">true if anonymous users can access the associated profile property; otherwise false.</param>
		// Token: 0x0600399D RID: 14749 RVA: 0x0009AEBC File Offset: 0x000990BC
		public SettingsAllowAnonymousAttribute(bool allow)
		{
			this.allow = allow;
		}

		/// <summary>Gets a value indicating whether the <see cref="P:System.Web.Profile.SettingsAllowAnonymousAttribute.Allow" /> property is set to the default value.</summary>
		/// <returns>true if the <see cref="P:System.Web.Profile.SettingsAllowAnonymousAttribute.Allow" /> property is set to the default value; otherwise false.</returns>
		// Token: 0x0600399E RID: 14750 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoTODO("Not implemented")]
		public override bool IsDefaultAttribute()
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets a value indicating whether the associated property of a custom profile implementation can be accessed if the user is an anonymous user.</summary>
		/// <returns>true if anonymous users can access the associated profile property; otherwise, false.</returns>
		// Token: 0x170011DE RID: 4574
		// (get) Token: 0x0600399F RID: 14751 RVA: 0x0009AECB File Offset: 0x000990CB
		public bool Allow
		{
			get
			{
				return this.allow;
			}
		}

		// Token: 0x04001F37 RID: 7991
		private bool allow;
	}
}
