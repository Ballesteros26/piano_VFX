using System;
using System.Security.Permissions;

namespace Microsoft.Win32
{
	/// <summary>Provides data for the <see cref="E:Microsoft.Win32.SystemEvents.UserPreferenceChanging" /> event.</summary>
	// Token: 0x020000DD RID: 221
	[PermissionSet(SecurityAction.LinkDemand, Unrestricted = true)]
	[PermissionSet(SecurityAction.InheritanceDemand, Unrestricted = true)]
	public class UserPreferenceChangingEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:Microsoft.Win32.UserPreferenceChangingEventArgs" /> class using the specified user preference category identifier.</summary>
		/// <param name="category">One of the <see cref="T:Microsoft.Win32.UserPreferenceCategory" /> values that indicate the user preference category that is changing. </param>
		// Token: 0x060004E9 RID: 1257 RVA: 0x0000ED79 File Offset: 0x0000CF79
		public UserPreferenceChangingEventArgs(UserPreferenceCategory category)
		{
			this.mycategory = category;
		}

		/// <summary>Gets the category of user preferences that is changing.</summary>
		/// <returns>One of the <see cref="T:Microsoft.Win32.UserPreferenceCategory" /> values that indicates the category of user preferences that is changing.</returns>
		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x060004EA RID: 1258 RVA: 0x0000ED88 File Offset: 0x0000CF88
		public UserPreferenceCategory Category
		{
			get
			{
				return this.mycategory;
			}
		}

		// Token: 0x04000BAE RID: 2990
		private UserPreferenceCategory mycategory;
	}
}
