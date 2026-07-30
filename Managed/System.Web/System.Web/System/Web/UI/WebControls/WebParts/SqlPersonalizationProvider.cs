using System;
using Unity;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Implements a personalization provider that uses Microsoft SQL Server.</summary>
	// Token: 0x020007BD RID: 1981
	public class SqlPersonalizationProvider : PersonalizationProvider
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.SqlPersonalizationProvider" /> class. </summary>
		// Token: 0x06004FDE RID: 20446 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public SqlPersonalizationProvider()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets or sets the name of the application to store and retrieve personalization information for.</summary>
		/// <returns>The name of the application to store and retrieve personalization information for. The default is the <see cref="P:System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath" /> property value for the current <see cref="P:System.Web.HttpContext.Request" />.</returns>
		/// <exception cref="T:System.Configuration.Provider.ProviderException">The property was set to a string with a length greater than 256 characters.</exception>
		// Token: 0x17001847 RID: 6215
		// (get) Token: 0x06004FDF RID: 20447 RVA: 0x0000E80B File Offset: 0x0000CA0B
		// (set) Token: 0x06004FE0 RID: 20448 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public override string ApplicationName
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

		/// <summary>Returns a collection containing zero or more <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationStateInfo" />-derived objects, based on the specified scope and parameters.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationStateInfoCollection" /> containing zero or more <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationStateInfo" />-derived objects.</returns>
		/// <param name="scope">A <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationScope" /> indicating the personalization information to be queried. This value cannot be null.</param>
		/// <param name="query">A <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationStateQuery" /> containing a query. This value can be null.</param>
		/// <param name="pageIndex">The location where the query starts.</param>
		/// <param name="pageSize">The number of records to return.</param>
		/// <param name="totalRecords">The total number of records available.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="pageIndex" /> is less than zero.- or -<paramref name="pageSize" /> is less than or equal to zero.- or -The combination of <paramref name="pageIndex" /> and <paramref name="pageSize" /> results in a value greater than <see cref="F:System.Int32.MaxValue" />.- or -<see cref="P:System.Web.UI.WebControls.WebParts.PersonalizationStateQuery.PathToMatch" /> is non-null and is an empty string ("") after trimming.- or -The length of <see cref="P:System.Web.UI.WebControls.WebParts.PersonalizationStateQuery.PathToMatch" /> is greater than 256 characters when the value is non-null.- or -<see cref="P:System.Web.UI.WebControls.WebParts.PersonalizationStateQuery.UsernameToMatch" /> is non-null and is an empty string after trimming.- or -The length of <see cref="P:System.Web.UI.WebControls.WebParts.PersonalizationStateQuery.UsernameToMatch" /> is greater than 256 characters when the value is non-null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The<paramref name=" scope" /> specified is not a valid value from the <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationScope" /> enumeration.</exception>
		// Token: 0x06004FE1 RID: 20449 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public override PersonalizationStateInfoCollection FindState(PersonalizationScope scope, PersonalizationStateQuery query, int pageIndex, int pageSize, out int totalRecords)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Returns a count of the number of rows in the underlying data store that exist for the specified <paramref name="scope" /> parameter.</summary>
		/// <returns>The number of rows in the underlying data store that exist for the specified <paramref name="scope" /> parameter.</returns>
		/// <param name="scope">A <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationScope" /> indicating the personalization information to be queried. This value cannot be null.</param>
		/// <param name="query">A <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationStateQuery" /> containing a query. This value can be null.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <see cref="P:System.Web.UI.WebControls.WebParts.PersonalizationStateQuery.PathToMatch" /> is non-null and is an empty string ("") after trimming.- or -The length of <see cref="P:System.Web.UI.WebControls.WebParts.PersonalizationStateQuery.PathToMatch" /> is greater than 256 characters when the value is non-null.- or -<see cref="P:System.Web.UI.WebControls.WebParts.PersonalizationStateQuery.UsernameToMatch" /> is non-null and is an empty string after trimming.- or -The length of <see cref="P:System.Web.UI.WebControls.WebParts.PersonalizationStateQuery.UsernameToMatch" /> is greater than 256 characters when the value is non-null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The<paramref name=" scope" /> specified is not a valid value from the <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationScope" /> enumeration.</exception>
		// Token: 0x06004FE2 RID: 20450 RVA: 0x000CB9B4 File Offset: 0x000C9BB4
		public override int GetCountOfState(PersonalizationScope scope, PersonalizationStateQuery query)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return 0;
		}

		/// <summary>Loads personalization data from the underlying data store, based on the specified parameters.</summary>
		/// <param name="webPartManager">The <see cref="T:System.Web.UI.WebControls.WebParts.WebPartManager" /> managing the personalization data.</param>
		/// <param name="path">The path for personalization information in the <see cref="F:System.Web.UI.WebControls.WebParts.PersonalizationScope.Shared" /> scope to be used as the retrieval key.</param>
		/// <param name="userName">The user name for personalization information in the <see cref="F:System.Web.UI.WebControls.WebParts.PersonalizationScope.User" /> scope to be used as the retrieval key.</param>
		/// <param name="sharedDataBlob">The returned data for the <see cref="F:System.Web.UI.WebControls.WebParts.PersonalizationScope.Shared" /> scope.</param>
		/// <param name="userDataBlob">The returned data for the <see cref="F:System.Web.UI.WebControls.WebParts.PersonalizationScope.User" /> scope.</param>
		// Token: 0x06004FE3 RID: 20451 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected override void LoadPersonalizationBlobs(WebPartManager webPartManager, string path, string userName, ref byte[] sharedDataBlob, ref byte[] userDataBlob)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Deletes personalization data from the underlying data store. </summary>
		/// <param name="webPartManager">The <see cref="T:System.Web.UI.WebControls.WebParts.WebPartManager" /> managing the personalization data.</param>
		/// <param name="path">The path for personalization information in the <see cref="F:System.Web.UI.WebControls.WebParts.PersonalizationScope.Shared" /> scope to be used as the retrieval key.</param>
		/// <param name="userName">The user name for personalization information in the <see cref="F:System.Web.UI.WebControls.WebParts.PersonalizationScope.User" /> scope to be used as the retrieval key.</param>
		// Token: 0x06004FE4 RID: 20452 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected override void ResetPersonalizationBlob(WebPartManager webPartManager, string path, string userName)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Deletes personalization state information from the underlying data store, based on the specified parameters.</summary>
		/// <returns>The number of rows deleted.</returns>
		/// <param name="scope">A <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationScope" /> indicating the personalization information to be queried. This value cannot be null.</param>
		/// <param name="paths">The paths for personalization information in the <see cref="F:System.Web.UI.WebControls.WebParts.PersonalizationScope.Shared" /> scope to be deleted.</param>
		/// <param name="usernames">The user names for personalization information in the <see cref="F:System.Web.UI.WebControls.WebParts.PersonalizationScope.User" /> scope to be deleted.</param>
		/// <exception cref="T:System.ArgumentException">Either the <paramref name="paths" /> or the <paramref name="usernames" /> parameter is an empty array.- or - The <paramref name="paths" /> and <paramref name="usernames" /> parameters contained within the respective arrays do not meet the validation rules.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The<paramref name=" scope" /> specified is not a member of the <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationScope" /> enumeration.</exception>
		// Token: 0x06004FE5 RID: 20453 RVA: 0x000CB9D0 File Offset: 0x000C9BD0
		public override int ResetState(PersonalizationScope scope, string[] paths, string[] usernames)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return 0;
		}

		/// <summary>Deletes user personalization data from the underlying data store, based on the specified parameters.</summary>
		/// <returns>The count of rows deleted from the underlying data store.</returns>
		/// <param name="path">The path of the personalization data to be deleted. This value can be null but cannot be an empty string ("").</param>
		/// <param name="userInactiveSinceDate">The date indicating the last activity.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is an empty string.- or -The path is greater than 256 characters in length.</exception>
		// Token: 0x06004FE6 RID: 20454 RVA: 0x000CB9EC File Offset: 0x000C9BEC
		public override int ResetUserState(string path, DateTime userInactiveSinceDate)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return 0;
		}

		/// <summary>Saves raw personalization data to the underlying Microsoft SQL Server database.</summary>
		/// <param name="webPartManager">The <see cref="T:System.Web.UI.WebControls.WebParts.WebPartManager" /> managing the personalization data.</param>
		/// <param name="path">A <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationScope" /> indicating the personalization information to be saved. This value cannot be null.</param>
		/// <param name="userName">The user name for personalization information in the <see cref="F:System.Web.UI.WebControls.WebParts.PersonalizationScope.User" /> scope to be used as the key.</param>
		/// <param name="dataBlob">The byte array of data to be saved.</param>
		// Token: 0x06004FE7 RID: 20455 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected override void SavePersonalizationBlob(WebPartManager webPartManager, string path, string userName, byte[] dataBlob)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
