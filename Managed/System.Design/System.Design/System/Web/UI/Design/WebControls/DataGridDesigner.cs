using System;
using System.Security.Permissions;
using Unity;

namespace System.Web.UI.Design.WebControls
{
	/// <summary>Extends design-time behavior for the <see cref="T:System.Web.UI.WebControls.DataGrid" /> Web server control.</summary>
	// Token: 0x0200018D RID: 397
	[SupportsPreviewControl(true)]
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class DataGridDesigner : BaseDataListDesigner
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.WebControls.DataGridDesigner" /> class.</summary>
		// Token: 0x06000B4B RID: 2891 RVA: 0x00009519 File Offset: 0x00007719
		public DataGridDesigner()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Creates a template editing frame using the specified verb.</summary>
		/// <returns>A template editing frame.</returns>
		/// <param name="verb">The <see cref="T:System.Web.UI.Design.TemplateEditingVerb" /> for which to create the template editing frame.</param>
		// Token: 0x06000B4C RID: 2892 RVA: 0x0000970B File Offset: 0x0000790B
		[Obsolete("Use of this method is not recommended because template editing is handled in ControlDesigner. To support template editing expose template data in the TemplateGroups property and call SetViewFlags(ViewFlags.TemplateEditing, true). http://go.microsoft.com/fwlink/?linkid=14202")]
		protected override ITemplateEditingFrame CreateTemplateEditingFrame(TemplateEditingVerb verb)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Gets the cached template editing verbs available to the designer.</summary>
		/// <returns>An array of <see cref="T:System.Web.UI.Design.TemplateEditingVerb" /> objects consisting of the cached template editing verbs that are available to the designer.</returns>
		// Token: 0x06000B4D RID: 2893 RVA: 0x0000970B File Offset: 0x0000790B
		[Obsolete("Use of this method is not recommended because template editing is handled in ControlDesigner. To support template editing expose template data in the TemplateGroups property and call SetViewFlags(ViewFlags.TemplateEditing, true). http://go.microsoft.com/fwlink/?linkid=14202")]
		protected override TemplateEditingVerb[] GetCachedTemplateEditingVerbs()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Gets the content of the template.</summary>
		/// <returns>The content of the template.</returns>
		/// <param name="editingFrame">The <see cref="T:System.Web.UI.Design.ITemplateEditingFrame" /> from which to get the content. </param>
		/// <param name="templateName">The name of the template. </param>
		/// <param name="allowEditing">true if the template's content can be edited; false if the content is read-only. </param>
		// Token: 0x06000B4E RID: 2894 RVA: 0x0000970B File Offset: 0x0000790B
		[Obsolete("Use of this method is not recommended because template editing is handled in ControlDesigner. To support template editing expose template data in the TemplateGroups property and call SetViewFlags(ViewFlags.TemplateEditing, true). http://go.microsoft.com/fwlink/?linkid=14202")]
		public override string GetTemplateContent(ITemplateEditingFrame editingFrame, string templateName, out bool allowEditing)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Called when the columns of the template change.</summary>
		// Token: 0x06000B4F RID: 2895 RVA: 0x00009519 File Offset: 0x00007719
		public virtual void OnColumnsChanged()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Called when the template editing verbs change.</summary>
		// Token: 0x06000B50 RID: 2896 RVA: 0x00009519 File Offset: 0x00007719
		protected override void OnTemplateEditingVerbsChanged()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Sets the content for the specified template and frame.</summary>
		/// <param name="editingFrame">The <see cref="T:System.Web.UI.Design.ITemplateEditingFrame" /> to set the content for. </param>
		/// <param name="templateName">The name of the template. </param>
		/// <param name="templateContent">The new content for the template.</param>
		// Token: 0x06000B51 RID: 2897 RVA: 0x00009519 File Offset: 0x00007719
		[Obsolete("Use of this method is not recommended because template editing is handled in ControlDesigner. To support template editing expose template data in the TemplateGroups property and call SetViewFlags(ViewFlags.TemplateEditing, true). http://go.microsoft.com/fwlink/?linkid=14202")]
		public override void SetTemplateContent(ITemplateEditingFrame editingFrame, string templateName, string templateContent)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
