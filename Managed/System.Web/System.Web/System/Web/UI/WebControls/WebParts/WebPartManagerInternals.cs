using System;
using Unity;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Isolates into a separate class methods that are used by the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartManager" /> control and can be overridden by developers who extend the control, but are rarely needed by page developers.</summary>
	// Token: 0x020006BF RID: 1727
	public sealed class WebPartManagerInternals
	{
		// Token: 0x06004940 RID: 18752 RVA: 0x0000B3E4 File Offset: 0x000095E4
		internal WebPartManagerInternals()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Adds a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> or server control to the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartManager" /> control's collection of <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> controls on a Web page.</summary>
		/// <param name="webPart">The <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> being added to the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartManager" /> control's collection of controls. </param>
		// Token: 0x06004941 RID: 18753 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public void AddWebPart(WebPart webPart)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Calls the <see cref="M:System.Web.UI.WebControls.WebParts.WebPart.OnClosing(System.EventArgs)" /> method of the specified control.</summary>
		/// <param name="webPart">A <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" />  that has been selected for closing. </param>
		// Token: 0x06004942 RID: 18754 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public void CallOnClosing(WebPart webPart)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Calls the <see cref="M:System.Web.UI.WebControls.WebParts.WebPart.OnConnectModeChanged(System.EventArgs)" /> method of the specified control.</summary>
		/// <param name="webPart">A <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" />  that has entered or exited the connect display mode. </param>
		// Token: 0x06004943 RID: 18755 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public void CallOnConnectModeChanged(WebPart webPart)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Calls the <see cref="M:System.Web.UI.WebControls.WebParts.WebPart.OnDeleting(System.EventArgs)" /> method of the specified control.</summary>
		/// <param name="webPart">A <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" />  that has been selected for deletion. </param>
		// Token: 0x06004944 RID: 18756 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public void CallOnDeleting(WebPart webPart)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Calls the <see cref="M:System.Web.UI.WebControls.WebParts.WebPart.OnEditModeChanged(System.EventArgs)" /> method of the specified control.</summary>
		/// <param name="webPart">A <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" />  that has entered or exited edit display mode. </param>
		// Token: 0x06004945 RID: 18757 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public void CallOnEditModeChanged(WebPart webPart)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Indicates whether the specified <see cref="T:System.Web.UI.WebControls.WebParts.WebPartConnection" /> object has been deleted.</summary>
		/// <returns>A Boolean value that indicates whether the connection has been deleted.</returns>
		/// <param name="connection">A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartConnection" /> that has been selected for deletion.</param>
		// Token: 0x06004946 RID: 18758 RVA: 0x000CA2BC File Offset: 0x000C84BC
		public bool ConnectionDeleted(WebPartConnection connection)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		/// <summary>Creates an object based on the parameter passed to the method.</summary>
		/// <returns>An <see cref="T:System.Object" /> of the same type as <paramref name="type" />.</returns>
		/// <param name="type">The <see cref="T:System.Type" /> of the object to create. </param>
		// Token: 0x06004947 RID: 18759 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public object CreateObjectFromType(Type type)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Sets a property to indicate that the specified connection object has been deleted.</summary>
		/// <param name="connection">A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartConnection" /> that has been selected for deletion.</param>
		// Token: 0x06004948 RID: 18760 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public void DeleteConnection(WebPartConnection connection)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the ID of a zone that contains the specified <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> or server control.</summary>
		/// <returns>A string that represents the ID of the zone that contains <paramref name="webPart" />.</returns>
		/// <param name="webPart">A <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" />  that resides in a zone. </param>
		// Token: 0x06004949 RID: 18761 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public string GetZoneID(WebPart webPart)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Loads previously saved state data for a <see cref="T:System.Web.UI.WebControls.WebParts.WebPartTransformer" /> object that participates in a connection between controls.</summary>
		/// <param name="transformer">A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartTransformer" /> that is used to connect controls.</param>
		/// <param name="savedState">An <see cref="T:System.Object" /> that contains the previously saved state data.</param>
		// Token: 0x0600494A RID: 18762 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public void LoadConfigurationState(WebPartTransformer transformer, object savedState)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Removes a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> or other server control from the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartManager" /> control's collection of controls.</summary>
		/// <param name="webPart">A <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> or other server control to be removed. </param>
		// Token: 0x0600494B RID: 18763 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public void RemoveWebPart(WebPart webPart)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Saves state data for a <see cref="T:System.Web.UI.WebControls.WebParts.WebPartTransformer" /> object that participates in a connection between controls.</summary>
		/// <returns>An <see cref="T:System.Object" /> that contains the saved state data.</returns>
		/// <param name="transformer">A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartTransformer" /> that is used to connect controls.</param>
		// Token: 0x0600494C RID: 18764 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public object SaveConfigurationState(WebPartTransformer transformer)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Establishes an errors message, and causes the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartChrome" /> object that is responsible for rendering a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control to render that message rather than the contents of the control.</summary>
		/// <param name="webPart">A <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> that participates in a connection with another control. </param>
		/// <param name="connectErrorMessage">A string that contains the text of the error message. </param>
		// Token: 0x0600494D RID: 18765 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public void SetConnectErrorMessage(WebPart webPart, string connectErrorMessage)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Sets a property on a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> or other server control indicating whether the control has shared personalization data.</summary>
		/// <param name="webPart">A <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> or other server control that can have shared personalization data. </param>
		/// <param name="hasSharedData">A Boolean value that indicates whether <paramref name="webPart" /> has shared data. </param>
		// Token: 0x0600494E RID: 18766 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public void SetHasSharedData(WebPart webPart, bool hasSharedData)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Sets a property on a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> or other server control indicating whether the control has user personalization data.</summary>
		/// <param name="webPart">A <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> or other server control that can have user personalization data. </param>
		/// <param name="hasUserData">A Boolean value that indicates whether <paramref name="webPart" /> has shared data. </param>
		// Token: 0x0600494F RID: 18767 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public void SetHasUserData(WebPart webPart, bool hasUserData)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Sets the <see cref="P:System.Web.UI.WebControls.WebParts.WebPart.IsClosed" /> property of a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control.</summary>
		/// <param name="webPart">A <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> for which this method sets the <see cref="P:System.Web.UI.WebControls.WebParts.WebPart.IsClosed" /> property. </param>
		/// <param name="isClosed">A Boolean value that indicates whether a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> is closed on a page. </param>
		// Token: 0x06004950 RID: 18768 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public void SetIsClosed(WebPart webPart, bool isClosed)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Sets the <see cref="P:System.Web.UI.WebControls.WebParts.WebPart.IsShared" /> property of a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control.</summary>
		/// <param name="webPart">A <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> for which this method sets the <see cref="P:System.Web.UI.WebControls.WebParts.WebPart.IsShared" /> property. </param>
		/// <param name="isShared">A Boolean value that indicates whether <paramref name="webPart" /> is shared. </param>
		// Token: 0x06004951 RID: 18769 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public void SetIsShared(WebPart webPart, bool isShared)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Sets the <see cref="P:System.Web.UI.WebControls.WebParts.WebPartConnection.IsShared" /> property of a <see cref="T:System.Web.UI.WebControls.WebParts.WebPartConnection" /> object. </summary>
		/// <param name="connection">A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartConnection" /> between two <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> controls.  </param>
		/// <param name="isShared">A Boolean value that indicates whether <paramref name="connection" /> is shared. </param>
		// Token: 0x06004952 RID: 18770 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public void SetIsShared(WebPartConnection connection, bool isShared)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Sets the <see cref="P:System.Web.UI.WebControls.WebParts.WebPart.IsStandalone" /> property of a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control.</summary>
		/// <param name="webPart">A <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> for which this method sets the <see cref="P:System.Web.UI.WebControls.WebParts.WebPart.IsStandalone" /> property.  </param>
		/// <param name="isStandalone">A Boolean value that indicates whether <paramref name="webPart" /> is a standalone control. </param>
		// Token: 0x06004953 RID: 18771 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public void SetIsStandalone(WebPart webPart, bool isStandalone)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Sets the <see cref="P:System.Web.UI.WebControls.WebParts.WebPart.IsStatic" /> property of a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control.</summary>
		/// <param name="webPart">The control for which the property value is being set. </param>
		/// <param name="isStatic">A Boolean value that indicates whether <paramref name="webPart" /> is static. </param>
		// Token: 0x06004954 RID: 18772 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public void SetIsStatic(WebPart webPart, bool isStatic)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Sets the <see cref="P:System.Web.UI.WebControls.WebParts.WebPartConnection.IsStatic" /> property of a <see cref="T:System.Web.UI.WebControls.WebParts.WebPartConnection" /> object.</summary>
		/// <param name="connection">The connection for which the property value is being set. </param>
		/// <param name="isStatic">A Boolean value that indicates whether <paramref name="connection" /> is static.  </param>
		// Token: 0x06004955 RID: 18773 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public void SetIsStatic(WebPartConnection connection, bool isStatic)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Assigns a <see cref="T:System.Web.UI.WebControls.WebParts.WebPartTransformer" /> object to be used in a connection between two server controls.</summary>
		/// <param name="connection">A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartConnection" /> that creates a connection between server controls. </param>
		/// <param name="transformer">A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartTransformer" /> to be used with <paramref name="connection" />.</param>
		// Token: 0x06004956 RID: 18774 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public void SetTransformer(WebPartConnection connection, WebPartTransformer transformer)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Sets a property that enables a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> or server control to keep the ID of the containing zone.</summary>
		/// <param name="webPart">The <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> whose <paramref name="zoneID" /> property value is being set. </param>
		/// <param name="zoneID">A string that contains the ID of the zone that <paramref name="webPart" /> belongs to. </param>
		// Token: 0x06004957 RID: 18775 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public void SetZoneID(WebPart webPart, string zoneID)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Sets the index of the specified controlwithin its zone relative to the other <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> controls within the zone.</summary>
		/// <param name="webPart">A <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control for which the method sets a zone index.</param>
		/// <param name="zoneIndex">The index of <paramref name="webPart" /> within its zone relative to other controls in the zone.</param>
		// Token: 0x06004958 RID: 18776 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public void SetZoneIndex(WebPart webPart, int zoneIndex)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
