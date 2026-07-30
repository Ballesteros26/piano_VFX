using System;

namespace System.Web.UI.Design
{
	/// <summary>Provides data for a <see cref="E:System.Web.UI.Design.IControlDesignerView.ViewEvent" /> event that is raised when the template mode changes for a control on the design surface.</summary>
	// Token: 0x020000A7 RID: 167
	public class TemplateModeChangedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.TemplateModeChangedEventArgs" /> class with the specified template group.</summary>
		/// <param name="newTemplateGroup">A new template group that is used to initialize the <see cref="P:System.Web.UI.Design.TemplateModeChangedEventArgs.NewTemplateGroup" />.</param>
		// Token: 0x06000505 RID: 1285 RVA: 0x00009437 File Offset: 0x00007637
		public TemplateModeChangedEventArgs(TemplateGroup newTemplateGroup)
		{
			this.group = newTemplateGroup;
		}

		/// <summary>Gets the template group that was created when you entered template editing mode.</summary>
		/// <returns>A <see cref="T:System.Web.UI.Design.TemplateGroup" /> if you entered template editing mode with a new template; otherwise, null.</returns>
		// Token: 0x1700013E RID: 318
		// (get) Token: 0x06000506 RID: 1286 RVA: 0x00009446 File Offset: 0x00007646
		public TemplateGroup NewTemplateGroup
		{
			get
			{
				return this.group;
			}
		}

		// Token: 0x04000136 RID: 310
		private TemplateGroup group;
	}
}
