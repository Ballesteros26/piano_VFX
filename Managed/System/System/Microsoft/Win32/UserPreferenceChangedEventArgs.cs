using System;
using System.Security.Permissions;

namespace Microsoft.Win32
{
	/// <summary>Provides data for the <see cref="E:Microsoft.Win32.SystemEvents.UserPreferenceChanged" /> event.</summary>
	// Token: 0x020000DB RID: 219
	[PermissionSet(SecurityAction.LinkDemand, Unrestricted = true)]
	[PermissionSet(SecurityAction.InheritanceDemand, Unrestricted = true)]
	public class UserPreferenceChangedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:Microsoft.Win32.UserPreferenceChangedEventArgs" /> class using the specified user preference category identifier.</summary>
		/// <param name="category">One of the <see cref="T:Microsoft.Win32.UserPreferenceCategory" /> values that indicates the user preference category that has changed. </param>
		// Token: 0x060004E3 RID: 1251 RVA: 0x0000ED62 File Offset: 0x0000CF62
		public UserPreferenceChangedEventArgs(UserPreferenceCategory category)
		{
			this.mycategory = category;
		}

		/// <summary>Gets the category of user preferences that has changed.</summary>
		/// <returns>One of the <see cref="T:Microsoft.Win32.UserPreferenceCategory" /> values that indicates the category of user preferences that has changed.</returns>
		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x060004E4 RID: 1252 RVA: 0x0000ED71 File Offset: 0x0000CF71
		public UserPreferenceCategory Category
		{
			get
			{
				return this.mycategory;
			}
		}

		// Token: 0x04000BAD RID: 2989
		private UserPreferenceCategory mycategory;
	}
}
