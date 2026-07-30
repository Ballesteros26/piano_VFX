using System;
using System.Security.Permissions;
using Unity;

namespace System.Web.UI.Design.WebControls
{
	/// <summary>Extends design-time behavior for the <see cref="T:System.Web.UI.WebControls.DataList" /> Web server control.</summary>
	// Token: 0x0200018E RID: 398
	[SupportsPreviewControl(true)]
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class DataListDesigner : BaseDataListDesigner
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.WebControls.DataListDesigner" /> class.</summary>
		// Token: 0x06000B52 RID: 2898 RVA: 0x00009519 File Offset: 0x00007719
		public DataListDesigner()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets a value indicating whether there are templates defined for the associated control.</summary>
		/// <returns>true if the associated control has templates defined; otherwise false.</returns>
		// Token: 0x17000263 RID: 611
		// (get) Token: 0x06000B53 RID: 2899 RVA: 0x000166D0 File Offset: 0x000148D0
		protected bool TemplatesExist
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
		}

		/// <summary>Creates a template editing frame using the specified verb.</summary>
		/// <returns>An <see cref="T:System.Web.UI.Design.ITemplateEditingFrame" /> object.</returns>
		/// <param name="verb">The <see cref="T:System.Web.UI.Design.TemplateEditingVerb" /> for which to create the template editing frame.</param>
		// Token: 0x06000B54 RID: 2900 RVA: 0x0000970B File Offset: 0x0000790B
		[Obsolete("Use of this method is not recommended because template editing is handled in ControlDesigner. To support template editing expose template data in the TemplateGroups property and call SetViewFlags(ViewFlags.TemplateEditing, true). http://go.microsoft.com/fwlink/?linkid=14202")]
		protected override ITemplateEditingFrame CreateTemplateEditingFrame(TemplateEditingVerb verb)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Gets the cached template editing verbs available to the designer.</summary>
		/// <returns>An array of <see cref="T:System.Web.UI.Design.TemplateEditingVerb" /> objects consisting of the cached template editing verbs that are available to the designer.</returns>
		// Token: 0x06000B55 RID: 2901 RVA: 0x0000970B File Offset: 0x0000790B
		[Obsolete("Use of this method is not recommended because template editing is handled in ControlDesigner. To support template editing expose template data in the TemplateGroups property and call SetViewFlags(ViewFlags.TemplateEditing, true). http://go.microsoft.com/fwlink/?linkid=14202")]
		protected override TemplateEditingVerb[] GetCachedTemplateEditingVerbs()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Gets the content of the template.</summary>
		/// <returns>The content of the template.</returns>
		/// <param name="editingFrame">The <see cref="T:System.Web.UI.Design.ITemplateEditingFrame" /> instance from which to get the content.</param>
		/// <param name="templateName">The name of the template. </param>
		/// <param name="allowEditing">true if the template's content can be edited; false if the content is read-only. </param>
		// Token: 0x06000B56 RID: 2902 RVA: 0x0000970B File Offset: 0x0000790B
		[Obsolete("Use of this method is not recommended because template editing is handled in ControlDesigner. To support template editing expose template data in the TemplateGroups property and call SetViewFlags(ViewFlags.TemplateEditing, true). http://go.microsoft.com/fwlink/?linkid=14202")]
		public override string GetTemplateContent(ITemplateEditingFrame editingFrame, string templateName, out bool allowEditing)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Called when the data source to which the underlying control is bound loads a new schema.</summary>
		// Token: 0x06000B57 RID: 2903 RVA: 0x00009519 File Offset: 0x00007719
		protected override void OnSchemaRefreshed()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Called when the template editing verbs change.</summary>
		// Token: 0x06000B58 RID: 2904 RVA: 0x00009519 File Offset: 0x00007719
		protected override void OnTemplateEditingVerbsChanged()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Sets the content for the specified template and frame.</summary>
		/// <param name="editingFrame">The <see cref="T:System.Web.UI.Design.ITemplateEditingFrame" /> instance for which to set the content.</param>
		/// <param name="templateName">The name of the template. </param>
		/// <param name="templateContent">The new content for the template. </param>
		// Token: 0x06000B59 RID: 2905 RVA: 0x00009519 File Offset: 0x00007719
		[Obsolete("Use of this method is not recommended because template editing is handled in ControlDesigner. To support template editing expose template data in the TemplateGroups property and call SetViewFlags(ViewFlags.TemplateEditing, true). http://go.microsoft.com/fwlink/?linkid=14202")]
		public override void SetTemplateContent(ITemplateEditingFrame editingFrame, string templateName, string templateContent)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
