using System;
using Unity;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Defines the basic functionality that represents the personalization data for a page.</summary>
	// Token: 0x020007B1 RID: 1969
	public abstract class PersonalizationState
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationState" /> class. </summary>
		/// <param name="webPartManager">A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartManager" /> that manages Web Parts controls that have personalization data.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="webPartManager" /> parameter is null.</exception>
		// Token: 0x06004F88 RID: 20360 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected PersonalizationState(WebPartManager webPartManager)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets a value that indicates whether any personalization information has changed.</summary>
		/// <returns>true if any personalization state instance has changed (is "dirty"); otherwise, false.</returns>
		// Token: 0x1700182F RID: 6191
		// (get) Token: 0x06004F89 RID: 20361 RVA: 0x000CB848 File Offset: 0x000C9A48
		public bool IsDirty
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
		}

		/// <summary>When overridden, gets a value that indicates whether any personalization state was extracted by a state instance.</summary>
		/// <returns>true if any personalization state was extracted by a state instance; otherwise, false.</returns>
		// Token: 0x17001830 RID: 6192
		// (get) Token: 0x06004F8A RID: 20362
		public abstract bool IsEmpty { get; }

		/// <summary>Gets a reference to the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartManager" /> control currently associated with the personalization state instance.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartManager" /> associated with the <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationState" />.</returns>
		// Token: 0x17001831 RID: 6193
		// (get) Token: 0x06004F8B RID: 20363 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public WebPartManager WebPartManager
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>When overridden, applies personalization data to the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartManager" /> control associated with the personalization state instance.</summary>
		// Token: 0x06004F8C RID: 20364
		public abstract void ApplyWebPartManagerPersonalization();

		/// <summary>When overridden, applies personalization data to the specified Web Parts control.</summary>
		/// <param name="webPart">The Web Parts control to which personalization data is applied.</param>
		// Token: 0x06004F8D RID: 20365
		public abstract void ApplyWebPartPersonalization(WebPart webPart);

		/// <summary>When overridden, extracts personalization information from the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartManager" /> control associated with the current personalization state.</summary>
		// Token: 0x06004F8E RID: 20366
		public abstract void ExtractWebPartManagerPersonalization();

		/// <summary>When overridden, extracts personalization data from the specified Web Parts control.</summary>
		/// <param name="webPart">The Web Parts control from which personalization data is extracted.</param>
		// Token: 0x06004F8F RID: 20367
		public abstract void ExtractWebPartPersonalization(WebPart webPart);

		/// <summary>Retrieves the authorization filter for the specified Web Parts control.</summary>
		/// <returns>The authorization filter string for a Web Parts control.</returns>
		/// <param name="webPartID">The ID of the Web Parts control from which the <see cref="P:System.Web.UI.WebControls.WebParts.WebPart.AuthorizationFilter" /> is retrieved.</param>
		// Token: 0x06004F90 RID: 20368
		public abstract string GetAuthorizationFilter(string webPartID);

		/// <summary>Marks the current personalization state as having changed.</summary>
		// Token: 0x06004F91 RID: 20369 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected void SetDirty()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>When overridden, marks a Web Parts control as having changed.</summary>
		/// <param name="webPart">The Web Parts control to be marked as having changed.</param>
		// Token: 0x06004F92 RID: 20370
		public abstract void SetWebPartDirty(WebPart webPart);

		/// <summary>When overridden, marks the associated <see cref="T:System.Web.UI.WebControls.WebParts.WebPartManager" /> control as having changed.</summary>
		// Token: 0x06004F93 RID: 20371
		public abstract void SetWebPartManagerDirty();

		/// <summary>Verifies that the specified Web Parts control is valid.</summary>
		/// <param name="webPart">The Web Parts control to be validated.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="webPart" /> is not currently managed by the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartManager" /> associated with the <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationState" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="webPart " />is null.</exception>
		// Token: 0x06004F94 RID: 20372 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected void ValidateWebPart(WebPart webPart)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
