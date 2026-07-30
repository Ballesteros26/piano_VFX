using System;
using Unity;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Represents a set of query parameters that can be passed to a personalization provider using the various get and find methods. This class cannot be inherited.</summary>
	// Token: 0x020007B2 RID: 1970
	[Serializable]
	public sealed class PersonalizationStateQuery
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationStateQuery" /> class. </summary>
		// Token: 0x06004F95 RID: 20373 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public PersonalizationStateQuery()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets or sets the value of the query parameter.</summary>
		/// <returns>The value of the query parameter indicated by the <paramref name="queryKey" /> parameter.</returns>
		/// <param name="queryKey">A case-insensitive query string. The value should be one of the following: "PathToMatch", "UserInactiveSinceDate", or "UsernameToMatch".</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="queryKey" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="queryKey" /> is an empty string ("").- or -When trimmed, <paramref name="queryKey" /> results in an empty string. - or -A value provided for one of the three properties on <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationStateQuery" /> was of the wrong type.</exception>
		// Token: 0x17001832 RID: 6194
		public object this[string queryKey]
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets the path to be used for a query.</summary>
		/// <returns>The path to be used for a query.</returns>
		// Token: 0x17001833 RID: 6195
		// (get) Token: 0x06004F98 RID: 20376 RVA: 0x0000E80B File Offset: 0x0000CA0B
		// (set) Token: 0x06004F99 RID: 20377 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public string PathToMatch
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets the last active date used in a query.</summary>
		/// <returns>The last active date to be used in a query.</returns>
		// Token: 0x17001834 RID: 6196
		// (get) Token: 0x06004F9A RID: 20378 RVA: 0x000CB864 File Offset: 0x000C9A64
		// (set) Token: 0x06004F9B RID: 20379 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public DateTime UserInactiveSinceDate
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(DateTime);
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets the user name in a query.</summary>
		/// <returns>The user name to be used in a query.</returns>
		// Token: 0x17001835 RID: 6197
		// (get) Token: 0x06004F9C RID: 20380 RVA: 0x0000E80B File Offset: 0x0000CA0B
		// (set) Token: 0x06004F9D RID: 20381 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public string UsernameToMatch
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}
	}
}
