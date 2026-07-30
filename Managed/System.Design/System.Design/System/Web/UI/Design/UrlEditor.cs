using System;
using System.ComponentModel;
using System.Drawing.Design;

namespace System.Web.UI.Design
{
	/// <summary>Provides a user interface for selecting a URL.</summary>
	// Token: 0x020000B0 RID: 176
	public class UrlEditor : UITypeEditor
	{
		/// <summary>Gets the caption to display on the selection dialog box.</summary>
		/// <returns>The caption to display on the selection dialog box.</returns>
		// Token: 0x17000148 RID: 328
		// (get) Token: 0x06000536 RID: 1334 RVA: 0x000094AE File Offset: 0x000076AE
		protected virtual string Caption
		{
			get
			{
				return "Select URL";
			}
		}

		/// <summary>Gets the file name filter string for the editor. This is used to determine the items that appear in the file list of the dialog box.</summary>
		/// <returns>A string that contains information about the file filtering options available in the dialog box.</returns>
		// Token: 0x17000149 RID: 329
		// (get) Token: 0x06000537 RID: 1335 RVA: 0x000094B5 File Offset: 0x000076B5
		protected virtual string Filter
		{
			get
			{
				return "All Files(*.*)|*.*|";
			}
		}

		/// <summary>Gets the options for the URL builder to use.</summary>
		/// <returns>A <see cref="T:System.Web.UI.Design.UrlBuilderOptions" /> that indicates the options for the URL builder to use.</returns>
		// Token: 0x1700014A RID: 330
		// (get) Token: 0x06000538 RID: 1336 RVA: 0x0000241E File Offset: 0x0000061E
		protected virtual UrlBuilderOptions Options
		{
			get
			{
				return UrlBuilderOptions.None;
			}
		}

		/// <summary>Edits the value of the specified object using the editor style provided by the <see cref="M:System.Web.UI.Design.UrlEditor.GetEditStyle(System.ComponentModel.ITypeDescriptorContext)" /> method.</summary>
		/// <returns>The new value of the object. If the value of the object hasn't changed, this method should return the same object it was passed.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> object that can be used to gain additional context information. </param>
		/// <param name="provider">A service provider object through which editing services may be obtained. </param>
		/// <param name="value">An instance of the value being edited. </param>
		// Token: 0x06000539 RID: 1337 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the editing style of the <see cref="M:System.Web.UI.Design.UrlEditor.EditValue(System.ComponentModel.ITypeDescriptorContext,System.IServiceProvider,System.Object)" /> method.</summary>
		/// <returns>One of the <see cref="T:System.Drawing.Design.UITypeEditorEditStyle" /> values indicating the provided editing style. If the method is not supported, this method will return <see cref="F:System.Drawing.Design.UITypeEditorEditStyle.None" />.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> object that can be used to gain additional context information. </param>
		// Token: 0x0600053A RID: 1338 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			throw new NotImplementedException();
		}
	}
}
