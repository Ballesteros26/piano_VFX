using System;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.WebParts.WebPartManager.AuthorizeWebPart" /> event. </summary>
	// Token: 0x0200048C RID: 1164
	public class WebPartAuthorizationEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartAuthorizationEventArgs" /> class. </summary>
		/// <param name="type">The <see cref="T:System.Type" /> of the control being checked for authorization. </param>
		/// <param name="path">The relative application path to the source file for the control being authorized, if the control is a user control. </param>
		/// <param name="authorizationFilter">An arbitrary string value assigned to the <see cref="P:System.Web.UI.WebControls.WebParts.WebPart.AuthorizationFilter" /> property of a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control, used for authorizing whether a control can be added to a page. </param>
		/// <param name="isShared">Indicates whether the control being checked for authorization is a shared control, meaning that it is visible to many or all users of the application, and its <see cref="P:System.Web.UI.WebControls.WebParts.WebPart.IsShared" /> property value is set to true. </param>
		// Token: 0x060034DB RID: 13531 RVA: 0x0008B28C File Offset: 0x0008948C
		public WebPartAuthorizationEventArgs(Type type, string path, string authorizationFilter, bool isShared)
		{
			this.type = type;
			this.path = path;
			this.authorizationFilter = authorizationFilter;
			this.isShared = isShared;
		}

		/// <summary>Gets the <see cref="T:System.Type" /> of the Web Parts control being checked for authorization.</summary>
		/// <returns>The <see cref="T:System.Type" /> of the control being checked for authorization.</returns>
		// Token: 0x170010A6 RID: 4262
		// (get) Token: 0x060034DC RID: 13532 RVA: 0x0008B2B1 File Offset: 0x000894B1
		public Type Type
		{
			get
			{
				return this.type;
			}
		}

		/// <summary>Gets the relative application path to the source file for the control being authorized, if the control is a user control.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the relative application path.</returns>
		// Token: 0x170010A7 RID: 4263
		// (get) Token: 0x060034DD RID: 13533 RVA: 0x0008B2B9 File Offset: 0x000894B9
		public string Path
		{
			get
			{
				return this.path;
			}
		}

		/// <summary>Gets the string value assigned to the <see cref="P:System.Web.UI.WebControls.WebParts.WebPart.AuthorizationFilter" /> property of a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control, used for authorizing whether a control can be added to a page.</summary>
		/// <returns>A <see cref="T:System.String" /> used in determining whether a control is authorized to be added to a page.</returns>
		// Token: 0x170010A8 RID: 4264
		// (get) Token: 0x060034DE RID: 13534 RVA: 0x0008B2C1 File Offset: 0x000894C1
		public string AuthorizationFilter
		{
			get
			{
				return this.authorizationFilter;
			}
		}

		/// <summary>Gets a value that indicates whether a Web Parts control is visible to all users of a Web Parts page.</summary>
		/// <returns>true if the Web Parts control is visible to all users of the page; otherwise, false.</returns>
		// Token: 0x170010A9 RID: 4265
		// (get) Token: 0x060034DF RID: 13535 RVA: 0x0008B2C9 File Offset: 0x000894C9
		public bool IsShared
		{
			get
			{
				return this.isShared;
			}
		}

		/// <summary>Gets or sets the value indicating whether a Web Parts control can be added to a page.</summary>
		/// <returns>true if the Web Parts control can be added to the page; otherwise, false.</returns>
		// Token: 0x170010AA RID: 4266
		// (get) Token: 0x060034E0 RID: 13536 RVA: 0x0008B2D1 File Offset: 0x000894D1
		// (set) Token: 0x060034E1 RID: 13537 RVA: 0x0008B2D9 File Offset: 0x000894D9
		public bool IsAuthorized
		{
			get
			{
				return this.authorized;
			}
			set
			{
				this.authorized = value;
			}
		}

		// Token: 0x04001D33 RID: 7475
		private bool authorized;

		// Token: 0x04001D34 RID: 7476
		private Type type;

		// Token: 0x04001D35 RID: 7477
		private string path;

		// Token: 0x04001D36 RID: 7478
		private string authorizationFilter;

		// Token: 0x04001D37 RID: 7479
		private bool isShared;
	}
}
