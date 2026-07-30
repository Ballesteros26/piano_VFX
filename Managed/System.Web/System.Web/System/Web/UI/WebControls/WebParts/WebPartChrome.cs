using System;
using Unity;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Enables developers to override the rendering for only the selected sections of <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> or server controls in a <see cref="T:System.Web.UI.WebControls.WebParts.WebPartZoneBase" /> zone.</summary>
	// Token: 0x020006C8 RID: 1736
	public class WebPartChrome
	{
		/// <summary>Initializes a new instance of the control.</summary>
		/// <param name="zone">The associated <see cref="T:System.Web.UI.WebControls.WebParts.WebPartZoneBase" /> control.</param>
		/// <param name="manager">The <see cref="T:System.Web.UI.WebControls.WebParts.WebPartManager" /> control on the current page. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="zone" /> is null.</exception>
		// Token: 0x060049F6 RID: 18934 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public WebPartChrome(WebPartZoneBase zone, WebPartManager manager)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets a value that indicates whether controls can be dragged into and out of the zone.</summary>
		/// <returns>A Boolean value that indicates whether controls can be dragged.</returns>
		// Token: 0x170016D5 RID: 5845
		// (get) Token: 0x060049F7 RID: 18935 RVA: 0x000CA690 File Offset: 0x000C8890
		protected bool DragDropEnabled
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
		}

		/// <summary>Gets a reference to the current <see cref="T:System.Web.UI.WebControls.WebParts.WebPartManager" /> instance.</summary>
		/// <returns>A reference to the current <see cref="T:System.Web.UI.WebControls.WebParts.WebPartManager" /> on the Web page.</returns>
		// Token: 0x170016D6 RID: 5846
		// (get) Token: 0x060049F8 RID: 18936 RVA: 0x0000E80B File Offset: 0x0000CA0B
		protected WebPartManager WebPartManager
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets a reference to the associated <see cref="T:System.Web.UI.WebControls.WebParts.WebPartZoneBase" /> zone.</summary>
		/// <returns>A reference to a <see cref="T:System.Web.UI.WebControls.WebParts.WebPartZoneBase" /> that is associated with the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartChrome" />.</returns>
		// Token: 0x170016D7 RID: 5847
		// (get) Token: 0x060049F9 RID: 18937 RVA: 0x0000E80B File Offset: 0x0000CA0B
		protected WebPartZoneBase Zone
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Creates the style object that supplies style attributes for each <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control rendered by the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartChrome" /> object.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.Style" /> that contains style attributes for the <paramref name="webPart" />.</returns>
		/// <param name="webPart">The control that is currently being rendered. </param>
		/// <param name="chromeType">The type of chrome for a particular control; one of the <see cref="T:System.Web.UI.WebControls.WebParts.PartChromeType" />  enumeration values.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="webPart" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="chromeType" /> is not one of the <see cref="T:System.Web.UI.WebControls.WebParts.PartChromeType" />  enumeration values.</exception>
		// Token: 0x060049FA RID: 18938 RVA: 0x0000E80B File Offset: 0x0000CA0B
		protected virtual Style CreateWebPartChromeStyle(WebPart webPart, PartChromeType chromeType)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Excludes specific verbs from being rendered, based on criteria provided by a developer.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartVerbCollection" /> containing all verbs associated with the <paramref name="webPart" />.</returns>
		/// <param name="verbs">The collection of all verbs associated with the control referenced in the <paramref name="webPart" /> parameter. </param>
		/// <param name="webPart">The control currently being rendered. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="verbs" /> collection is null.- or - <paramref name="webPart" /> is null.</exception>
		// Token: 0x060049FB RID: 18939 RVA: 0x0000E80B File Offset: 0x0000CA0B
		protected virtual WebPartVerbCollection FilterWebPartVerbs(WebPartVerbCollection verbs, WebPart webPart)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Gets the client ID for the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartChrome" /> object as rendered in a Web page.</summary>
		/// <returns>A string that contains the client ID for the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartChrome" /> object.</returns>
		/// <param name="webPart">The control currently being rendered. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="webPart" /> is null.</exception>
		// Token: 0x060049FC RID: 18940 RVA: 0x0000E80B File Offset: 0x0000CA0B
		protected string GetWebPartChromeClientID(WebPart webPart)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Gets the client ID for the table cell that contains the title for a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control.</summary>
		/// <returns>A string that contains the client ID for the title of the <paramref name="webPart" />.</returns>
		/// <param name="webPart">The control currently being rendered. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="webPart" /> is null.</exception>
		// Token: 0x060049FD RID: 18941 RVA: 0x0000E80B File Offset: 0x0000CA0B
		protected string GetWebPartTitleClientID(WebPart webPart)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Gets a collection of verbs that should be rendered with a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartVerbCollection" /> containing all the verbs that should be rendered with <paramref name="webPart" />.</returns>
		/// <param name="webPart">The control currently being rendered. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="webPart" /> is null.</exception>
		// Token: 0x060049FE RID: 18942 RVA: 0x0000E80B File Offset: 0x0000CA0B
		protected virtual WebPartVerbCollection GetWebPartVerbs(WebPart webPart)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Performs tasks that must be done prior to rendering <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> controls.</summary>
		// Token: 0x060049FF RID: 18943 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public virtual void PerformPreRender()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Renders the main content area of a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control, excluding the header and footer.</summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> that receives the <paramref name="webPart" /> content. </param>
		/// <param name="webPart">The control currently being rendered. </param>
		// Token: 0x06004A00 RID: 18944 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void RenderPartContents(HtmlTextWriter writer, WebPart webPart)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Renders a complete <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control with all its sections.</summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> that receives the <paramref name="webPart" /> content. </param>
		/// <param name="webPart">The control currently being rendered. </param>
		/// <exception cref="T:System.ArgumentNullException">The control that <paramref name="webPart" /> refers to is null. </exception>
		// Token: 0x06004A01 RID: 18945 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public virtual void RenderWebPart(HtmlTextWriter writer, WebPart webPart)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
