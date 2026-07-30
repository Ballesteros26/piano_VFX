using System;
using Unity;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Represents the per-user personalization information for a page and a user. This class cannot be inherited.</summary>
	// Token: 0x020007BF RID: 1983
	[Serializable]
	public sealed class UserPersonalizationStateInfo : PersonalizationStateInfo
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.UserPersonalizationStateInfo" /> class. </summary>
		/// <param name="path">The page that the information applies to. <paramref name="path" /> is an application-relative (using tilde syntax) virtual path.</param>
		/// <param name="lastUpdatedDate">The last date and time that the user information for a page was updated.</param>
		/// <param name="size">The size, in bytes, of the per-user state information for the page.</param>
		/// <param name="username">The user to whom the personalization information for the page applies.</param>
		/// <param name="lastActivityDate">The last time the user was active in the ASP.NET application.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" />, after trimming, is an empty string ("").- or - <paramref name="username" />, after trimming, is an empty string ("").</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null.- or - <paramref name="username" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="size" /> is negative.</exception>
		// Token: 0x06004FEA RID: 20458 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public UserPersonalizationStateInfo(string path, DateTime lastUpdatedDate, int size, string username, DateTime lastActivityDate)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the last time the user was active in the ASP.NET application associated with a page.</summary>
		/// <returns>The last time the user was active in the ASP.NET application associated with a page.</returns>
		// Token: 0x17001848 RID: 6216
		// (get) Token: 0x06004FEB RID: 20459 RVA: 0x000CBA08 File Offset: 0x000C9C08
		public DateTime LastActivityDate
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(DateTime);
			}
		}

		/// <summary>Gets the user name to which the personalization information for a page applies.</summary>
		/// <returns>The user name to which the personalization information for a page applies.</returns>
		// Token: 0x17001849 RID: 6217
		// (get) Token: 0x06004FEC RID: 20460 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public string Username
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}
	}
}
