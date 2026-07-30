using System;
using Unity;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Defines a common set of properties for the several display modes that a Web Parts page can enter.</summary>
	// Token: 0x020006BD RID: 1725
	public abstract class WebPartDisplayMode
	{
		/// <summary>Initializes a value for the name of the display mode.</summary>
		/// <param name="name">The name of the display mode.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is null.</exception>
		// Token: 0x06004930 RID: 18736 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected WebPartDisplayMode(string name)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets a value that determines whether users can change the layout of a Web Parts page when the page is in a certain display mode.</summary>
		/// <returns>true if users can change the page layout; otherwise, false. The default is false.</returns>
		// Token: 0x1700168A RID: 5770
		// (get) Token: 0x06004931 RID: 18737 RVA: 0x000CA1C0 File Offset: 0x000C83C0
		public virtual bool AllowPageDesign
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
		}

		/// <summary>Gets a value that indicates whether a certain display mode is associated with a class that derives from the <see cref="T:System.Web.UI.WebControls.WebParts.ToolZone" /> class.</summary>
		/// <returns>true if the display mode is associated with a <see cref="T:System.Web.UI.WebControls.WebParts.ToolZone" /> class; otherwise, false. The default is false.</returns>
		// Token: 0x1700168B RID: 5771
		// (get) Token: 0x06004932 RID: 18738 RVA: 0x000CA1DC File Offset: 0x000C83DC
		public virtual bool AssociatedWithToolZone
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
		}

		/// <summary>Gets the name of a display mode.</summary>
		/// <returns>A string that contains the name of a display mode. </returns>
		// Token: 0x1700168C RID: 5772
		// (get) Token: 0x06004933 RID: 18739 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public string Name
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets a value that indicates whether a particular display mode requires personalization to be enabled.</summary>
		/// <returns>A Boolean value that indicates whether personalization is required. The default is false.</returns>
		// Token: 0x1700168D RID: 5773
		// (get) Token: 0x06004934 RID: 18740 RVA: 0x000CA1F8 File Offset: 0x000C83F8
		public virtual bool RequiresPersonalization
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
		}

		/// <summary>Gets a value that indicates whether controls that have their <see cref="P:System.Web.UI.WebControls.WebParts.WebPart.Hidden" /> property set to true should be displayed.</summary>
		/// <returns>A Boolean value that indicates whether hidden controls should be displayed. The default is false.</returns>
		// Token: 0x1700168E RID: 5774
		// (get) Token: 0x06004935 RID: 18741 RVA: 0x000CA214 File Offset: 0x000C8414
		public virtual bool ShowHiddenWebParts
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
		}

		/// <summary>Gets a value that indicates whether users can personalize a page while the page is in a certain display mode.</summary>
		/// <returns>true if users can personalize a page; otherwise, false. The default is true. However, when <see cref="P:System.Web.UI.WebControls.WebParts.WebPartDisplayMode.RequiresPersonalization" /> is set to true, and personalization is disabled on the page, the default will be false.</returns>
		/// <param name="webPartManager">The <see cref="T:System.Web.UI.WebControls.WebParts.WebPartManager" />  control instance on the current page.</param>
		// Token: 0x06004936 RID: 18742 RVA: 0x000CA230 File Offset: 0x000C8430
		public virtual bool IsEnabled(WebPartManager webPartManager)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}
	}
}
