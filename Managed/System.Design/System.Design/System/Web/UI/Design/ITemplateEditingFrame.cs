using System;
using System.Web.UI.WebControls;

namespace System.Web.UI.Design
{
	/// <summary>Provides an interface to manage a template editing area.</summary>
	// Token: 0x02000093 RID: 147
	[Obsolete("Template editing is supported in ControlDesigner.TemplateGroups with SetViewFlags(ViewFlags.TemplateEditing, true) in 2.0.")]
	public interface ITemplateEditingFrame : IDisposable
	{
		/// <summary>Closes the control and optionally saves any changes.</summary>
		/// <param name="saveChanges">true if changes to the document should be saved; otherwise, false. </param>
		// Token: 0x0600047B RID: 1147
		void Close(bool saveChanges);

		/// <summary>Opens and displays the control.</summary>
		// Token: 0x0600047C RID: 1148
		void Open();

		/// <summary>Resizes the control to the specified width and height.</summary>
		/// <param name="width">The new width for the control. </param>
		/// <param name="height">The new height for the control. </param>
		// Token: 0x0600047D RID: 1149
		void Resize(int width, int height);

		/// <summary>Saves any changes to the document.</summary>
		// Token: 0x0600047E RID: 1150
		void Save();

		/// <summary>Changes the name of the control to the specified name.</summary>
		/// <param name="newName">The new name for the control. </param>
		// Token: 0x0600047F RID: 1151
		void UpdateControlName(string newName);

		/// <summary>Gets the style for the editing frame.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.Style" /> that represents the Web server control style attributes for the editing frame.</returns>
		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000480 RID: 1152
		Style ControlStyle { get; }

		/// <summary>Gets or sets the initial height of the control.</summary>
		/// <returns>The initial height of the control.</returns>
		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06000481 RID: 1153
		// (set) Token: 0x06000482 RID: 1154
		int InitialHeight { get; set; }

		/// <summary>Gets or sets the initial width of the control.</summary>
		/// <returns>The initial width of the control.</returns>
		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000483 RID: 1155
		// (set) Token: 0x06000484 RID: 1156
		int InitialWidth { get; set; }

		/// <summary>Gets the name of the editing frame.</summary>
		/// <returns>The name of the editing frame.</returns>
		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000485 RID: 1157
		string Name { get; }

		/// <summary>Gets a set of names of templates to use.</summary>
		/// <returns>An array of template names.</returns>
		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000486 RID: 1158
		string[] TemplateNames { get; }

		/// <summary>Gets the template styles for the control.</summary>
		/// <returns>An array of <see cref="T:System.Web.UI.WebControls.Style" /> objects that represent the template styles for the control.</returns>
		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000487 RID: 1159
		Style[] TemplateStyles { get; }

		/// <summary>Gets or sets the verb that invokes the template.</summary>
		/// <returns>A <see cref="T:System.Web.UI.Design.TemplateEditingVerb" /> that invokes the template.</returns>
		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000488 RID: 1160
		// (set) Token: 0x06000489 RID: 1161
		TemplateEditingVerb Verb { get; set; }
	}
}
