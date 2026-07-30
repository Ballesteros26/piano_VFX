using System;
using System.ComponentModel.Design;
using System.Web.UI.WebControls;

namespace System.Web.UI.Design
{
	/// <summary>Provides services for editing control templates at design time. This class cannot be inherited.</summary>
	// Token: 0x020000A3 RID: 163
	[Obsolete("Template editing is supported in ControlDesigner.TemplateGroups with SetViewFlags(ViewFlags.TemplateEditing, true) in 2.0.")]
	public sealed class TemplateEditingService : ITemplateEditingService, IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.TemplateEditingService" /> class with the specified designer host. </summary>
		/// <param name="designerHost">An <see cref="T:System.ComponentModel.Design.IDesignerHost" />  implementation, used to access components at design time.</param>
		// Token: 0x060004D3 RID: 1235 RVA: 0x0000934E File Offset: 0x0000754E
		public TemplateEditingService(IDesignerHost designerHost)
		{
			if (designerHost == null)
			{
				throw new ArgumentNullException("designerHost");
			}
			this._designerHost = designerHost;
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x0000936C File Offset: 0x0000756C
		~TemplateEditingService()
		{
			this.Dispose(false);
		}

		/// <summary>Creates a new template editing frame for the specified templated control designer, using the specified name and templates.</summary>
		/// <returns>The new <see cref="T:System.Web.UI.Design.ITemplateEditingFrame" />.</returns>
		/// <param name="designer">The <see cref="T:System.Web.UI.Design.TemplatedControlDesigner" /> that will use the template editing frame.</param>
		/// <param name="frameName">The name of the editing frame that will be displayed on the frame. Typically, this is the same as the <see cref="P:System.ComponentModel.Design.DesignerVerb.Text" /> used as the menu text for the <see cref="T:System.Web.UI.Design.TemplateEditingVerb" /> that is invoked to create the frame.</param>
		/// <param name="templateNames">An array of names for the templates that the template editing frame will contain.</param>
		// Token: 0x060004D5 RID: 1237 RVA: 0x0000939C File Offset: 0x0000759C
		[MonoTODO]
		public ITemplateEditingFrame CreateFrame(TemplatedControlDesigner designer, string frameName, string[] templateNames)
		{
			return this.CreateFrame(designer, frameName, templateNames, null, null);
		}

		/// <summary>Creates a new template editing frame for the specified <see cref="T:System.Web.UI.Design.TemplatedControlDesigner" /> object, using the specified name, template names, control style, and template styles.</summary>
		/// <returns>The new <see cref="T:System.Web.UI.Design.ITemplateEditingFrame" />.</returns>
		/// <param name="designer">The <see cref="T:System.Web.UI.Design.TemplatedControlDesigner" /> that will use the template editing frame. </param>
		/// <param name="frameName">The name of the editing frame that will be displayed on the frame. Typically, this is the same as the <see cref="P:System.ComponentModel.Design.DesignerVerb.Text" /> used as the menu text for the <see cref="T:System.Web.UI.Design.TemplateEditingVerb" /> that is invoked to create the frame. </param>
		/// <param name="templateNames">An array of names for the templates that the template editing frame will contain. </param>
		/// <param name="controlStyle">The control <see cref="T:System.Web.UI.WebControls.Style" /> for the editing frame. </param>
		/// <param name="templateStyles">An array of type <see cref="T:System.Web.UI.WebControls.Style" /> that represents the template styles for the editing frame. </param>
		// Token: 0x060004D6 RID: 1238 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public ITemplateEditingFrame CreateFrame(TemplatedControlDesigner designer, string frameName, string[] templateNames, Style controlStyle, Style[] templateStyles)
		{
			throw new NotImplementedException();
		}

		/// <summary>Releases all resources that are used by the <see cref="T:System.Web.UI.Design.TemplateEditingService" /> object. </summary>
		// Token: 0x060004D7 RID: 1239 RVA: 0x000093A9 File Offset: 0x000075A9
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x000093B8 File Offset: 0x000075B8
		private void Dispose(bool disposing)
		{
			if (disposing)
			{
				this._designerHost = null;
			}
		}

		/// <summary>Gets the name of the parent template.</summary>
		/// <returns>The name of the parent template.</returns>
		/// <param name="control">The <see cref="T:System.Web.UI.Control" /> for which to get the name of the parent template. </param>
		// Token: 0x060004D9 RID: 1241 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public string GetContainingTemplateName(Control control)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets a value that indicates whether the service supports nested template editing.</summary>
		/// <returns>true if the service supports nested template editing; otherwise, false.</returns>
		// Token: 0x17000130 RID: 304
		// (get) Token: 0x060004DA RID: 1242 RVA: 0x0000241E File Offset: 0x0000061E
		public bool SupportsNestedTemplateEditing
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04000134 RID: 308
		private IDesignerHost _designerHost;
	}
}
