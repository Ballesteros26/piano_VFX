using System;
using System.Collections;
using System.Configuration.Provider;
using Unity;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Implements the basic functionality for a personalization provider.</summary>
	// Token: 0x020007B0 RID: 1968
	public abstract class PersonalizationProvider : ProviderBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationProvider" /> class. </summary>
		// Token: 0x06004F78 RID: 20344 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected PersonalizationProvider()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>When overridden in a derived class, gets or sets the name of the application configured for the provider.</summary>
		/// <returns>The application configured for the personalization provider.</returns>
		// Token: 0x1700182E RID: 6190
		// (get) Token: 0x06004F79 RID: 20345
		// (set) Token: 0x06004F7A RID: 20346
		public abstract string ApplicationName { get; set; }

		/// <summary>Returns a list of <see cref="T:System.Web.UI.WebControls.WebParts.WebPartUserCapability" /> objects that represent the set of known capabilities used by the Web Parts control set.</summary>
		/// <returns>An <see cref="T:System.Collections.IList" /> that represents the set of known capabilities used by the Web Parts control set.</returns>
		// Token: 0x06004F7B RID: 20347 RVA: 0x0000E80B File Offset: 0x0000CA0B
		protected virtual IList CreateSupportedUserCapabilities()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Determines whether the initial personalization scope should be <see cref="F:System.Web.UI.WebControls.WebParts.PersonalizationScope.Shared" /> or <see cref="F:System.Web.UI.WebControls.WebParts.PersonalizationScope.User" /> scope.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationScope" /> indicating whether the current personalization scope is <see cref="F:System.Web.UI.WebControls.WebParts.PersonalizationScope.User" /> or <see cref="F:System.Web.UI.WebControls.WebParts.PersonalizationScope.Shared" />.</returns>
		/// <param name="webPartManager">The <see cref="T:System.Web.UI.WebControls.WebParts.WebPartManager" /> that manages the personalization information.</param>
		/// <param name="loadedState">The personalization state information.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="webPartManager" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">The page associated with <paramref name="webPartManager" /> is null.</exception>
		// Token: 0x06004F7C RID: 20348 RVA: 0x000CB82C File Offset: 0x000C9A2C
		public virtual PersonalizationScope DetermineInitialScope(WebPartManager webPartManager, PersonalizationState loadedState)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return PersonalizationScope.User;
		}

		/// <summary>Returns a dictionary containing <see cref="T:System.Web.UI.WebControls.WebParts.WebPartUserCapability" /> instances that represent the personalization-related capabilities of the currently executing user account.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionary" /> containing zero or more <see cref="T:System.Web.UI.WebControls.WebParts.WebPartUserCapability" /> instances if the user account is authenticated, or null if the executing user account is not authenticated.</returns>
		/// <param name="webPartManager">The <see cref="T:System.Web.UI.WebControls.WebParts.WebPartManager" /> managing the personalization information.</param>
		/// <exception cref="T:System.ArgumentException">The page associated with <paramref name="webPartManager" /> is null.- or -The request associated with the page is null.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="webPartManager" /> is null.</exception>
		// Token: 0x06004F7D RID: 20349 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public virtual IDictionary DetermineUserCapabilities(WebPartManager webPartManager)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>When overridden in a derived class, returns a collection containing zero or more <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationStateInfo" />-derived objects based on scope and specific query parameters. </summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationStateInfoCollection" /> containing zero or more <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationStateInfo" />-derived objects.</returns>
		/// <param name="scope">A <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationScope" /> with the personalization information to be queried. This value cannot be null.</param>
		/// <param name="query">A <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationStateQuery" /> containing a query. This value can be null.</param>
		/// <param name="pageIndex">The location where the query starts.</param>
		/// <param name="pageSize">The number of records to return.</param>
		/// <param name="totalRecords">The total number of records available.</param>
		// Token: 0x06004F7E RID: 20350
		public abstract PersonalizationStateInfoCollection FindState(PersonalizationScope scope, PersonalizationStateQuery query, int pageIndex, int pageSize, out int totalRecords);

		/// <summary>When overridden in a derived class, returns the number of rows in the underlying data store that exist within the specified scope.</summary>
		/// <returns>The number of rows in the underlying data store that exist for the specified <paramref name="scope" /> parameter.</returns>
		/// <param name="scope">A <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationScope" /> of the personalization information to be queried. This value cannot be null.</param>
		/// <param name="query">A <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationStateQuery" /> containing a query. This value can be null.</param>
		// Token: 0x06004F7F RID: 20351
		public abstract int GetCountOfState(PersonalizationScope scope, PersonalizationStateQuery query);

		/// <summary>When overridden in a derived class, loads raw personalization data from the underlying data store.</summary>
		/// <param name="webPartManager">The <see cref="T:System.Web.UI.WebControls.WebParts.WebPartManager" /> managing the personalization data.</param>
		/// <param name="path">The path for personalization information to be used as the retrieval key.</param>
		/// <param name="userName">The user name for personalization information to be used as the retrieval key.</param>
		/// <param name="sharedDataBlob">The returned data for the <see cref="F:System.Web.UI.WebControls.WebParts.PersonalizationScope.Shared" /> scope.</param>
		/// <param name="userDataBlob">The returned data for the <see cref="F:System.Web.UI.WebControls.WebParts.PersonalizationScope.User" /> scope.</param>
		// Token: 0x06004F80 RID: 20352
		protected abstract void LoadPersonalizationBlobs(WebPartManager webPartManager, string path, string userName, ref byte[] sharedDataBlob, ref byte[] userDataBlob);

		/// <summary>Loads the raw data from the underlying data store and converts that data into a <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationState" /> object.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationState" /> containing personalization data.</returns>
		/// <param name="webPartManager">The <see cref="T:System.Web.UI.WebControls.WebParts.WebPartManager" /> managing the personalization data.</param>
		/// <param name="ignoreCurrentUser">A <see cref="T:System.Boolean" /> indicating whether the user name should be passed to the personalization provider.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="webPartManager" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">The page associated with <paramref name="webPartManager" /> is null.- or -The request associated with the page is null.</exception>
		// Token: 0x06004F81 RID: 20353 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public virtual PersonalizationState LoadPersonalizationState(WebPartManager webPartManager, bool ignoreCurrentUser)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>When overridden in a derived class, deletes raw personalization data from the underlying data store. </summary>
		/// <param name="webPartManager">The <see cref="T:System.Web.UI.WebControls.WebParts.WebPartManager" /> managing the personalization data.</param>
		/// <param name="path">The path for personalization information to be used as the data store key.</param>
		/// <param name="userName">The user name for personalization information to be used as the data store key.</param>
		// Token: 0x06004F82 RID: 20354
		protected abstract void ResetPersonalizationBlob(WebPartManager webPartManager, string path, string userName);

		/// <summary>Resets personalization data to the underlying data store.</summary>
		/// <param name="webPartManager">The <see cref="T:System.Web.UI.WebControls.WebParts.WebPartManager" /> managing the personalization data.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="webPartManager" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="webPartManager" /> is not associated with a page.- or -The page is not associated with an in-progress <see cref="T:System.Web.HttpRequest" />.</exception>
		// Token: 0x06004F83 RID: 20355 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public virtual void ResetPersonalizationState(WebPartManager webPartManager)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>When overridden in a derived class, deletes personalization state from the underlying data store based on the specified parameters. </summary>
		/// <returns>The number of rows deleted.</returns>
		/// <param name="scope">A <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationScope" /> of the personalization information to be reset. This value cannot be null.</param>
		/// <param name="paths">The paths for personalization information to be deleted.</param>
		/// <param name="usernames">The user names for personalization information to be deleted.</param>
		// Token: 0x06004F84 RID: 20356
		public abstract int ResetState(PersonalizationScope scope, string[] paths, string[] usernames);

		/// <summary>When overridden in a derived class, deletes Web Parts personalization data from the underlying data store based on the specified parameters. </summary>
		/// <returns>The number of rows deleted from the underlying data store.</returns>
		/// <param name="path">The path of the personalization data to be deleted. This value can be null but cannot be an empty string ("").</param>
		/// <param name="userInactiveSinceDate">The date indicating the last time a Web site user changed personalization data.</param>
		// Token: 0x06004F85 RID: 20357
		public abstract int ResetUserState(string path, DateTime userInactiveSinceDate);

		/// <summary>When overridden in a derived class, saves raw personalization data to the underlying data store. </summary>
		/// <param name="webPartManager">The <see cref="T:System.Web.UI.WebControls.WebParts.WebPartManager" /> managing the personalization data.</param>
		/// <param name="path">The path for personalization information to be used as the data store key.</param>
		/// <param name="userName">The user name for personalization information to be used as the key.</param>
		/// <param name="dataBlob">The byte array of data to be saved.</param>
		// Token: 0x06004F86 RID: 20358
		protected abstract void SavePersonalizationBlob(WebPartManager webPartManager, string path, string userName, byte[] dataBlob);

		/// <summary>Saves personalization data to a data store.</summary>
		/// <param name="state">A <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationState" /> containing personalization data to be saved.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="state" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="state" /> object being saved has a different type from the <paramref name="state" /> object that was returned by the <see cref="M:System.Web.UI.WebControls.WebParts.PersonalizationProvider.LoadPersonalizationState(System.Web.UI.WebControls.WebParts.WebPartManager,System.Boolean)" /> method. </exception>
		// Token: 0x06004F87 RID: 20359 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public virtual void SavePersonalizationState(PersonalizationState state)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
