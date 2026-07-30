using System;
using Unity;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Enables developers to override the rendering for only the selected sections of <see cref="T:System.Web.UI.WebControls.WebParts.EditorPart" /> controls in an <see cref="T:System.Web.UI.WebControls.WebParts.EditorZoneBase" /> zone.</summary>
	// Token: 0x020006DF RID: 1759
	public class EditorPartChrome
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.EditorPartChrome" /> class. </summary>
		/// <param name="zone">The associated <see cref="T:System.Web.UI.WebControls.WebParts.EditorZoneBase" />.</param>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Web.UI.WebControls.WebParts.EditorZoneBase" /> that <paramref name="zone" /> refers to is null.</exception>
		// Token: 0x06004AA8 RID: 19112 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public EditorPartChrome(EditorZoneBase zone)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets a reference to the associated <see cref="T:System.Web.UI.WebControls.WebParts.EditorZoneBase" /> zone.</summary>
		/// <returns>A reference to an <see cref="T:System.Web.UI.WebControls.WebParts.EditorZoneBase" /> that is associated with the <see cref="T:System.Web.UI.WebControls.WebParts.EditorPartChrome" />.</returns>
		// Token: 0x17001711 RID: 5905
		// (get) Token: 0x06004AA9 RID: 19113 RVA: 0x0000E80B File Offset: 0x0000CA0B
		protected EditorZoneBase Zone
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Creates the style object that supplies style attributes for each <see cref="T:System.Web.UI.WebControls.WebParts.EditorPart" /> control rendered by the <see cref="T:System.Web.UI.WebControls.WebParts.EditorPartChrome" /> object.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.Style" /> that contains style attributes for <paramref name="editorPart" />.</returns>
		/// <param name="editorPart">The control that is currently being rendered.</param>
		/// <param name="chromeType">The type of chrome for a particular control; one of the <see cref="T:System.Web.UI.WebControls.WebParts.PartChromeType" /> enumeration values.</param>
		/// <exception cref="T:System.ArgumentNullException">The control that <paramref name="editorPart" /> refers to is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="chromeType" /> is not a <see cref="T:System.Web.UI.WebControls.WebParts.PartChromeType" />.</exception>
		// Token: 0x06004AAA RID: 19114 RVA: 0x0000E80B File Offset: 0x0000CA0B
		protected virtual Style CreateEditorPartChromeStyle(EditorPart editorPart, PartChromeType chromeType)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Performs tasks that must be done prior to rendering <see cref="T:System.Web.UI.WebControls.WebParts.EditorPart" /> controls.</summary>
		// Token: 0x06004AAB RID: 19115 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public virtual void PerformPreRender()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Renders a complete <see cref="T:System.Web.UI.WebControls.WebParts.EditorPart" /> control with all its sections.</summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> that receives the <paramref name="editorPart" /> content.</param>
		/// <param name="editorPart">The control currently being rendered.</param>
		/// <exception cref="T:System.ArgumentNullException">The control that <paramref name="editorPart" /> refers to is null.</exception>
		// Token: 0x06004AAC RID: 19116 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public virtual void RenderEditorPart(HtmlTextWriter writer, EditorPart editorPart)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Renders the main content area of an <see cref="T:System.Web.UI.WebControls.WebParts.EditorPart" /> control, excluding the header and footer.</summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> that receives the <paramref name="editorPart" /> content.</param>
		/// <param name="editorPart">The control currently being rendered.</param>
		// Token: 0x06004AAD RID: 19117 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void RenderPartContents(HtmlTextWriter writer, EditorPart editorPart)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
