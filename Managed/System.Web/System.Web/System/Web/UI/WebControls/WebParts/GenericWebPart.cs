using System;
using System.ComponentModel;
using Unity;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Wraps server controls that are not <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> controls so that they can appear and behave as true <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> controls at run time.</summary>
	// Token: 0x020006D8 RID: 1752
	[ToolboxItem(false)]
	public class GenericWebPart : WebPart
	{
		/// <summary>Initializes a new instance of a <see cref="T:System.Web.UI.WebControls.WebParts.GenericWebPart" /> control by passing in a reference to a control that becomes the child control.</summary>
		/// <param name="control">A server control that becomes the child control of the <see cref="T:System.Web.UI.WebControls.WebParts.GenericWebPart" /> control instance. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="control" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="control" /> is of type <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" />.- or -<paramref name="control" /> has a null or empty <see cref="P:System.Web.UI.Control.ID" /> property.- or -<paramref name="control" /> has been output-cached.</exception>
		// Token: 0x06004A47 RID: 19015 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected internal GenericWebPart(Control control)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets a reference to the child control that is wrapped by a <see cref="T:System.Web.UI.WebControls.WebParts.GenericWebPart" /> control at run time.</summary>
		/// <returns>A <see cref="T:System.Web.UI.Control" /> that refers to the wrapped child control.</returns>
		// Token: 0x170016ED RID: 5869
		// (get) Token: 0x06004A48 RID: 19016 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public Control ChildControl
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets a reference to the child control, to enable the control to be edited by custom <see cref="T:System.Web.UI.WebControls.WebParts.EditorPart" /> controls.</summary>
		/// <returns>An <see cref="T:System.Object" /> that consists of the child control of a <see cref="T:System.Web.UI.WebControls.WebParts.GenericWebPart" /> control. </returns>
		// Token: 0x170016EE RID: 5870
		// (get) Token: 0x06004A49 RID: 19017 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public override object WebBrowsableObject
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Returns a collection of custom <see cref="T:System.Web.UI.WebControls.WebParts.EditorPart" /> controls that can be used to edit the child control of a <see cref="T:System.Web.UI.WebControls.WebParts.GenericWebPart" /> control when it is in edit mode.</summary>
		/// <returns>An <see cref="T:System.Web.UI.WebControls.WebParts.EditorPartCollection" /> that contains custom <see cref="T:System.Web.UI.WebControls.WebParts.EditorPart" /> controls associated with a server control.</returns>
		// Token: 0x06004A4A RID: 19018 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public override EditorPartCollection CreateEditorParts()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}
	}
}
