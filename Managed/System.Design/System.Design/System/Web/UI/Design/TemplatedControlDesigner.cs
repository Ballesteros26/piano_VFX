using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;

namespace System.Web.UI.Design
{
	/// <summary>Extends design-time behavior for template-based server controls.</summary>
	// Token: 0x020000A8 RID: 168
	public abstract class TemplatedControlDesigner : ControlDesigner
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.TemplatedControlDesigner" /> class.</summary>
		// Token: 0x06000507 RID: 1287 RVA: 0x0000944E File Offset: 0x0000764E
		public TemplatedControlDesigner()
		{
		}

		/// <summary>Initializes the designer and loads the specified component.</summary>
		/// <param name="component">The control element being designed.</param>
		// Token: 0x06000508 RID: 1288 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public override void Initialize(IComponent component)
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, creates a template editing frame for the specified verb.</summary>
		/// <returns>The new template editing frame.</returns>
		/// <param name="verb">The template editing verb to create a template editing frame for. </param>
		// Token: 0x06000509 RID: 1289
		[Obsolete("Template editing is supported in ControlDesigner.TemplateGroups with SetViewFlags(ViewFlags.TemplateEditing, true) in 2.0.")]
		protected abstract ITemplateEditingFrame CreateTemplateEditingFrame(TemplateEditingVerb verb);

		/// <summary>Gets the cached template editing verbs.</summary>
		/// <returns>An array of <see cref="T:System.Web.UI.Design.TemplateEditingVerb" /> objects, if any.</returns>
		// Token: 0x0600050A RID: 1290
		[Obsolete("Template editing is supported in ControlDesigner.TemplateGroups with SetViewFlags(ViewFlags.TemplateEditing, true) in 2.0.")]
		protected abstract TemplateEditingVerb[] GetCachedTemplateEditingVerbs();

		/// <summary>When overridden in a derived class, gets the template's content.</summary>
		/// <returns>The content of the template.</returns>
		/// <param name="editingFrame">The template editing frame to retrieve the content of. </param>
		/// <param name="templateName">The name of the template. </param>
		/// <param name="allowEditing">true if the template's content can be edited; false if the content is read-only. </param>
		// Token: 0x0600050B RID: 1291
		[Obsolete("Template editing is supported in ControlDesigner.TemplateGroups with SetViewFlags(ViewFlags.TemplateEditing, true) in 2.0.")]
		public abstract string GetTemplateContent(ITemplateEditingFrame editingFrame, string templateName, out bool allowEditing);

		/// <summary>When overridden in a derived class, sets the specified template's content to the specified content.</summary>
		/// <param name="editingFrame">The template editing frame to provide content for. </param>
		/// <param name="templateName">The name of the template. </param>
		/// <param name="templateContent">The content to set for the template. </param>
		// Token: 0x0600050C RID: 1292
		[Obsolete("Template editing is supported in ControlDesigner.TemplateGroups with SetViewFlags(ViewFlags.TemplateEditing, true) in 2.0.")]
		public abstract void SetTemplateContent(ITemplateEditingFrame editingFrame, string templateName, string templateContent);

		/// <summary>Opens a particular template frame object for editing in the designer.</summary>
		/// <param name="newTemplateEditingFrame">The template editing frame object to open in the designer. </param>
		// Token: 0x0600050D RID: 1293 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		[Obsolete("Template editing is supported in ControlDesigner.TemplateGroups with SetViewFlags(ViewFlags.TemplateEditing, true) in 2.0.")]
		public void EnterTemplateMode(ITemplateEditingFrame newTemplateEditingFrame)
		{
			throw new NotImplementedException();
		}

		/// <summary>Closes the currently active template editing frame after saving any relevant changes.</summary>
		/// <param name="fSwitchingTemplates">true when switching from one template editing frame to another; otherwise false. </param>
		/// <param name="fNested">true if this designer is nested (one or more levels) within another control whose designer is also in template editing mode; otherwise false. </param>
		/// <param name="fSave">true if templates should be saved on exit; otherwise, false. </param>
		// Token: 0x0600050E RID: 1294 RVA: 0x0000234B File Offset: 0x0000054B
		[Obsolete("Template editing is supported in ControlDesigner.TemplateGroups with SetViewFlags(ViewFlags.TemplateEditing, true) in 2.0.")]
		[MonoTODO]
		public void ExitTemplateMode(bool fSwitchingTemplates, bool fNested, bool fSave)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the data item property of the template's container.</summary>
		/// <returns>A string representing the data.</returns>
		/// <param name="templateName">The name of the template. </param>
		// Token: 0x0600050F RID: 1295 RVA: 0x0000945D File Offset: 0x0000765D
		[Obsolete("Template editing is supported in ControlDesigner.TemplateGroups with SetViewFlags(ViewFlags.TemplateEditing, true) in 2.0.")]
		public virtual string GetTemplateContainerDataItemProperty(string templateName)
		{
			return string.Empty;
		}

		/// <summary>Gets the data source of the template's container.</summary>
		/// <returns>The data source of the container of the specified template.</returns>
		/// <param name="templateName">The name of the template. </param>
		// Token: 0x06000510 RID: 1296 RVA: 0x0000256A File Offset: 0x0000076A
		[Obsolete("Template editing is supported in ControlDesigner.TemplateGroups with SetViewFlags(ViewFlags.TemplateEditing, true) in 2.0.")]
		public virtual IEnumerable GetTemplateContainerDataSource(string templateName)
		{
			return null;
		}

		/// <summary>Gets the template editing verbs available to the designer.</summary>
		/// <returns>The template editing verbs available to the designer.</returns>
		// Token: 0x06000511 RID: 1297 RVA: 0x0000234B File Offset: 0x0000054B
		[Obsolete("Template editing is supported in ControlDesigner.TemplateGroups with SetViewFlags(ViewFlags.TemplateEditing, true) in 2.0.")]
		[MonoTODO]
		public TemplateEditingVerb[] GetTemplateEditingVerbs()
		{
			throw new NotImplementedException();
		}

		/// <summary>Creates a template from the specified text.</summary>
		/// <returns>An <see cref="T:System.Web.UI.ITemplate" /> from the specified text.</returns>
		/// <param name="text">The text to retrieve a template from. </param>
		// Token: 0x06000512 RID: 1298 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		protected ITemplate GetTemplateFromText(string text)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the type of the parent of the template property.</summary>
		/// <returns>The type of the object that has the template property.</returns>
		/// <param name="templateName">The name of the template to return the type of the parent for. </param>
		// Token: 0x06000513 RID: 1299 RVA: 0x00009464 File Offset: 0x00007664
		[Obsolete("Template editing is supported in ControlDesigner.TemplateGroups with SetViewFlags(ViewFlags.TemplateEditing, true) in 2.0.")]
		public virtual Type GetTemplatePropertyParentType(string templateName)
		{
			return base.Component.GetType();
		}

		/// <summary>Gets a string of text that represents the specified template.</summary>
		/// <returns>A string that represents the specified template.</returns>
		/// <param name="template">The <see cref="T:System.Web.UI.ITemplate" /> to convert to text. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="template" /> is null.</exception>
		// Token: 0x06000514 RID: 1300 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		protected string GetTextFromTemplate(ITemplate template)
		{
			throw new NotImplementedException();
		}

		/// <summary>Provides an opportunity to perform additional processing when a behavior is attached to the designer.</summary>
		// Token: 0x06000515 RID: 1301 RVA: 0x0000234B File Offset: 0x0000054B
		[Obsolete("Use ControlDesigner.Tag instead")]
		[MonoTODO]
		protected override void OnBehaviorAttached()
		{
			throw new NotImplementedException();
		}

		/// <summary>Delegate to handle the component changed event.</summary>
		/// <param name="sender">The object sending the event. </param>
		/// <param name="ce">A <see cref="T:System.ComponentModel.Design.ComponentChangedEventArgs" /> that provides data for the event. </param>
		// Token: 0x06000516 RID: 1302 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public override void OnComponentChanged(object sender, ComponentChangedEventArgs ce)
		{
			throw new NotImplementedException();
		}

		/// <summary>Provides an opportunity to perform additional processing when the parent of this designer is changed.</summary>
		// Token: 0x06000517 RID: 1303 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public override void OnSetParent()
		{
			throw new NotImplementedException();
		}

		/// <summary>Provides an opportunity to perform additional processing when the template mode is changed.</summary>
		// Token: 0x06000518 RID: 1304 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		protected virtual void OnTemplateModeChanged()
		{
			throw new NotImplementedException();
		}

		/// <summary>Saves the active template editing frame.</summary>
		// Token: 0x06000519 RID: 1305 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		protected void SaveActiveTemplateEditingFrame()
		{
			throw new NotImplementedException();
		}

		/// <summary>Updates the design-time HTML.</summary>
		// Token: 0x0600051A RID: 1306 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public override void UpdateDesignTimeHtml()
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets a value indicating whether the designer allows data binding. </summary>
		/// <returns>true, if the designer allows data binding; otherwise, false.</returns>
		// Token: 0x1700013F RID: 319
		// (get) Token: 0x0600051B RID: 1307 RVA: 0x0000234B File Offset: 0x0000054B
		protected override bool DataBindingsEnabled
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a collection of template groups, each containing a template definition.</summary>
		/// <returns>A collection of <see cref="T:System.Web.UI.Design.TemplateGroup" /> elements.</returns>
		// Token: 0x17000140 RID: 320
		// (get) Token: 0x0600051C RID: 1308 RVA: 0x0000234B File Offset: 0x0000054B
		public override TemplateGroupCollection TemplateGroups
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the active template editing frame.</summary>
		/// <returns>An <see cref="T:System.Web.UI.Design.ITemplateEditingFrame" /> that is the currently active template editing frame.</returns>
		// Token: 0x17000141 RID: 321
		// (get) Token: 0x0600051D RID: 1309 RVA: 0x00009471 File Offset: 0x00007671
		[Obsolete("Template editing is supported in ControlDesigner.TemplateGroups with SetViewFlags(ViewFlags.TemplateEditing, true) in 2.0.")]
		public ITemplateEditingFrame ActiveTemplateEditingFrame
		{
			get
			{
				return this._activeTemplateFrame;
			}
		}

		/// <summary>Gets a value indicating whether or not this designer will allow the viewing or editing of templates.</summary>
		/// <returns>true if the designer will allow the viewing or editing of templates; otherwise, false.</returns>
		// Token: 0x17000142 RID: 322
		// (get) Token: 0x0600051E RID: 1310 RVA: 0x00009479 File Offset: 0x00007679
		public bool CanEnterTemplateMode
		{
			get
			{
				return this._enableTemplateEditing;
			}
		}

		/// <summary>Gets a value indicating whether the designer document is in template mode.</summary>
		/// <returns>true if the designer document is in template mode; otherwise, false.</returns>
		// Token: 0x17000143 RID: 323
		// (get) Token: 0x0600051F RID: 1311 RVA: 0x00009481 File Offset: 0x00007681
		[Obsolete("Use ControlDesigner.InTemplateMode instead")]
		public new bool InTemplateMode
		{
			get
			{
				return this._templateMode;
			}
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x06000520 RID: 1312 RVA: 0x00009489 File Offset: 0x00007689
		internal EventHandler TemplateEditingVerbHandler
		{
			get
			{
				return this._templateVerbHandler;
			}
		}

		// Token: 0x04000137 RID: 311
		private ITemplateEditingFrame _activeTemplateFrame;

		// Token: 0x04000138 RID: 312
		private bool _enableTemplateEditing = true;

		// Token: 0x04000139 RID: 313
		private bool _templateMode;

		// Token: 0x0400013A RID: 314
		private EventHandler _templateVerbHandler;
	}
}
