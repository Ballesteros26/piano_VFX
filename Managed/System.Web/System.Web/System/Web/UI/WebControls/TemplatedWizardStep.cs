using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents a step in a wizard control that can be customized through the use of templates.</summary>
	// Token: 0x02000429 RID: 1065
	[Themeable(true)]
	[PersistChildren(false)]
	[ParseChildren(true)]
	[ToolboxItem(false)]
	[ControlBuilder(typeof(WizardStepControlBuilder))]
	[Bindable(false)]
	public class TemplatedWizardStep : WizardStepBase
	{
		/// <summary>Gets or sets the template for displaying the content of a step in a <see cref="T:System.Web.UI.WebControls.Wizard" /> control.</summary>
		/// <returns>An <see cref="T:System.Web.UI.ITemplate" /> object that contains the template for displaying the content of a step in a <see cref="T:System.Web.UI.WebControls.Wizard" /> control.</returns>
		// Token: 0x17000F3E RID: 3902
		// (get) Token: 0x0600300C RID: 12300 RVA: 0x0007E9DF File Offset: 0x0007CBDF
		// (set) Token: 0x0600300D RID: 12301 RVA: 0x0007E9E7 File Offset: 0x0007CBE7
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[TemplateContainer(typeof(Wizard))]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Browsable(false)]
		public virtual ITemplate ContentTemplate
		{
			get
			{
				return this._contentTemplate;
			}
			set
			{
				this._contentTemplate = value;
			}
		}

		/// <summary>Gets the container that a <see cref="T:System.Web.UI.WebControls.Wizard" /> control uses to create a <see cref="P:System.Web.UI.WebControls.TemplatedWizardStep.ContentTemplate" /> template for a step.</summary>
		/// <returns>A <see cref="T:System.Web.UI.Control" /> that contains the <see cref="P:System.Web.UI.WebControls.TemplatedWizardStep.ContentTemplate" /> template for a step.</returns>
		// Token: 0x17000F3F RID: 3903
		// (get) Token: 0x0600300E RID: 12302 RVA: 0x0007E9F0 File Offset: 0x0007CBF0
		// (set) Token: 0x0600300F RID: 12303 RVA: 0x0007E9F8 File Offset: 0x0007CBF8
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Control ContentTemplateContainer
		{
			get
			{
				return this._contentTemplateContainer;
			}
			internal set
			{
				this._contentTemplateContainer = value;
			}
		}

		/// <summary>Gets or sets the template for displaying the navigation user interface (UI) of a step in a <see cref="T:System.Web.UI.WebControls.Wizard" /> control.</summary>
		/// <returns>An <see cref="T:System.Web.UI.ITemplate" /> object that contains the template for displaying the navigation UI of a step in a <see cref="T:System.Web.UI.WebControls.Wizard" /> control.</returns>
		// Token: 0x17000F40 RID: 3904
		// (get) Token: 0x06003010 RID: 12304 RVA: 0x0007EA01 File Offset: 0x0007CC01
		// (set) Token: 0x06003011 RID: 12305 RVA: 0x0007EA09 File Offset: 0x0007CC09
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(Wizard))]
		[Browsable(false)]
		public virtual ITemplate CustomNavigationTemplate
		{
			get
			{
				return this._customNavigationTemplate;
			}
			set
			{
				this._customNavigationTemplate = value;
			}
		}

		/// <summary>Gets the container that a <see cref="T:System.Web.UI.WebControls.Wizard" /> control uses to create a <see cref="P:System.Web.UI.WebControls.TemplatedWizardStep.CustomNavigationTemplate" /> template for a step.</summary>
		/// <returns>A <see cref="T:System.Web.UI.Control" /> that contains the <see cref="P:System.Web.UI.WebControls.TemplatedWizardStep.CustomNavigationTemplate" /> template for a step.</returns>
		/// <exception cref="T:System.NullReferenceException">If <see cref="P:System.Web.UI.WebControls.TemplatedWizardStep.CustomNavigationTemplate" /> has no content.</exception>
		// Token: 0x17000F41 RID: 3905
		// (get) Token: 0x06003012 RID: 12306 RVA: 0x0007EA12 File Offset: 0x0007CC12
		// (set) Token: 0x06003013 RID: 12307 RVA: 0x0007EA1A File Offset: 0x0007CC1A
		[Browsable(false)]
		[Bindable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Control CustomNavigationTemplateContainer
		{
			get
			{
				return this._customNavigationTemplateContainer;
			}
			internal set
			{
				this._customNavigationTemplateContainer = value;
			}
		}

		/// <summary>Gets the skin to apply to the <see cref="T:System.Web.UI.WebControls.TemplatedWizardStep" />.</summary>
		// Token: 0x17000F42 RID: 3906
		// (get) Token: 0x06003014 RID: 12308 RVA: 0x00032ACF File Offset: 0x00030CCF
		// (set) Token: 0x06003015 RID: 12309 RVA: 0x00032AD7 File Offset: 0x00030CD7
		[Browsable(true)]
		[global::System.MonoTODO("Why override?")]
		public override string SkinID
		{
			get
			{
				return base.SkinID;
			}
			set
			{
				base.SkinID = value;
			}
		}

		// Token: 0x04001C04 RID: 7172
		private ITemplate _contentTemplate;

		// Token: 0x04001C05 RID: 7173
		private Control _contentTemplateContainer;

		// Token: 0x04001C06 RID: 7174
		private ITemplate _customNavigationTemplate;

		// Token: 0x04001C07 RID: 7175
		private Control _customNavigationTemplateContainer;
	}
}
