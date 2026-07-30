using System;

namespace System.Web.Profile
{
	/// <summary>Provides information about a user profile.</summary>
	// Token: 0x02000504 RID: 1284
	[Serializable]
	public class ProfileInfo
	{
		/// <summary>Creates an instance of the <see cref="T:System.Web.Profile.ProfileInfo" /> class with the specified property values.</summary>
		/// <param name="username">The user name for the profile.</param>
		/// <param name="isAnonymous">true to indicate the profile is for an anonymous user; false to indicate the profile is for an authenticated user.</param>
		/// <param name="lastActivityDate">The last date and time when the profile was read or updated.</param>
		/// <param name="lastUpdatedDate">The last date and time when the profile was updated.</param>
		/// <param name="size">The size of the profile information and values stored in the data source.</param>
		// Token: 0x0600392C RID: 14636 RVA: 0x00099D38 File Offset: 0x00097F38
		public ProfileInfo(string username, bool isAnonymous, DateTime lastActivityDate, DateTime lastUpdatedDate, int size)
		{
			if (username != null)
			{
				username = username.Trim();
			}
			this._UserName = username;
			if (lastActivityDate.Kind == DateTimeKind.Local)
			{
				lastActivityDate = lastActivityDate.ToUniversalTime();
			}
			this._LastActivityDate = lastActivityDate;
			if (lastUpdatedDate.Kind == DateTimeKind.Local)
			{
				lastUpdatedDate = lastUpdatedDate.ToUniversalTime();
			}
			this._LastUpdatedDate = lastUpdatedDate;
			this._IsAnonymous = isAnonymous;
			this._Size = size;
		}

		/// <summary>Creates an instance of the <see cref="T:System.Web.Profile.ProfileInfo" /> object for a class that inherits the <see cref="T:System.Web.Profile.ProfileInfo" /> class.</summary>
		// Token: 0x0600392D RID: 14637 RVA: 0x00002050 File Offset: 0x00000250
		protected ProfileInfo()
		{
		}

		/// <summary>Gets the user name for the profile.</summary>
		/// <returns>The user name for the profile.</returns>
		// Token: 0x170011C2 RID: 4546
		// (get) Token: 0x0600392E RID: 14638 RVA: 0x00099DA1 File Offset: 0x00097FA1
		public virtual string UserName
		{
			get
			{
				return this._UserName;
			}
		}

		/// <summary>Gets the last date and time when the profile was read or updated.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> that represents the last date and time when the profile was read or updated.</returns>
		// Token: 0x170011C3 RID: 4547
		// (get) Token: 0x0600392F RID: 14639 RVA: 0x00099DA9 File Offset: 0x00097FA9
		public virtual DateTime LastActivityDate
		{
			get
			{
				return this._LastActivityDate.ToLocalTime();
			}
		}

		/// <summary>Gets the last date and time when the profile was updated.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> that represents the last date and time when the profile was updated.</returns>
		// Token: 0x170011C4 RID: 4548
		// (get) Token: 0x06003930 RID: 14640 RVA: 0x00099DB6 File Offset: 0x00097FB6
		public virtual DateTime LastUpdatedDate
		{
			get
			{
				return this._LastUpdatedDate.ToLocalTime();
			}
		}

		/// <summary>Gets whether the user name for the profile is anonymous.</summary>
		/// <returns>true if the user name for the profile is anonymous; otherwise, false.</returns>
		// Token: 0x170011C5 RID: 4549
		// (get) Token: 0x06003931 RID: 14641 RVA: 0x00099DC3 File Offset: 0x00097FC3
		public virtual bool IsAnonymous
		{
			get
			{
				return this._IsAnonymous;
			}
		}

		/// <summary>Gets the size of the profile property names and values stored in the data source.</summary>
		/// <returns>The size of the profile property names and values stored in the data source.</returns>
		// Token: 0x170011C6 RID: 4550
		// (get) Token: 0x06003932 RID: 14642 RVA: 0x00099DCB File Offset: 0x00097FCB
		public virtual int Size
		{
			get
			{
				return this._Size;
			}
		}

		// Token: 0x04001F16 RID: 7958
		private string _UserName;

		// Token: 0x04001F17 RID: 7959
		private DateTime _LastActivityDate;

		// Token: 0x04001F18 RID: 7960
		private DateTime _LastUpdatedDate;

		// Token: 0x04001F19 RID: 7961
		private bool _IsAnonymous;

		// Token: 0x04001F1A RID: 7962
		private int _Size;
	}
}
