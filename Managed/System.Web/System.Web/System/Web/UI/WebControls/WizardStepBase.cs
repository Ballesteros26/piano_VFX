using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	/// <summary>Implements the basic functionality required by a step in a <see cref="T:System.Web.UI.WebControls.Wizard" /> control.</summary>
	// Token: 0x0200044E RID: 1102
	[ToolboxItem("")]
	[Bindable(false)]
	[ControlBuilder(typeof(WizardStepControlBuilder))]
	public abstract class WizardStepBase : View
	{
		/// <summary>Gets or sets a value indicating whether the user is allowed to return to the current step from a subsequent step in a <see cref="T:System.Web.UI.WebControls.WizardStepCollection" /> collection. </summary>
		/// <returns>true if the user is allowed to return to the current step; otherwise, false. The default value is true.</returns>
		// Token: 0x1700101E RID: 4126
		// (get) Token: 0x06003304 RID: 13060 RVA: 0x000893E8 File Offset: 0x000875E8
		// (set) Token: 0x06003305 RID: 13061 RVA: 0x00054450 File Offset: 0x00052650
		[Themeable(false)]
		[Filterable(false)]
		[DefaultValue(true)]
		public virtual bool AllowReturn
		{
			get
			{
				object obj = this.ViewState["AllowReturn"];
				return obj == null || (bool)obj;
			}
			set
			{
				this.ViewState["AllowReturn"] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether themes apply to this control.</summary>
		/// <returns>true to use themes; otherwise, false. The default is false.</returns>
		// Token: 0x1700101F RID: 4127
		// (get) Token: 0x06003306 RID: 13062 RVA: 0x00089411 File Offset: 0x00087611
		// (set) Token: 0x06003307 RID: 13063 RVA: 0x00089419 File Offset: 0x00087619
		[Browsable(true)]
		public override bool EnableTheming
		{
			get
			{
				return base.EnableTheming;
			}
			set
			{
				base.EnableTheming = value;
			}
		}

		/// <summary>Gets or sets the programmatic identifier assigned to the server control.</summary>
		/// <returns>The programmatic identifier assigned to the control.</returns>
		/// <exception cref="T:System.ArgumentException">The property was set to an invalid identifier string at design time.-or-The property was set to the same identifier as the containing <see cref="P:System.Web.UI.WebControls.WizardStepBase.Wizard" /> control at design time.-or- The property was set to the same identifier as another step in the containing <see cref="P:System.Web.UI.WebControls.WizardStepBase.Wizard" /> control at design time.</exception>
		// Token: 0x17001020 RID: 4128
		// (get) Token: 0x06003308 RID: 13064 RVA: 0x00037227 File Offset: 0x00035427
		// (set) Token: 0x06003309 RID: 13065 RVA: 0x0003722F File Offset: 0x0003542F
		public override string ID
		{
			get
			{
				return base.ID;
			}
			set
			{
				base.ID = value;
			}
		}

		/// <summary>Gets the name associated with a step in a control that acts as a wizard.</summary>
		/// <returns>The name associated with a step in a control that acts as a wizard.</returns>
		// Token: 0x17001021 RID: 4129
		// (get) Token: 0x0600330A RID: 13066 RVA: 0x00089422 File Offset: 0x00087622
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public virtual string Name
		{
			get
			{
				if (this.Title != null && this.Title.Length > 0)
				{
					return this.Title;
				}
				if (this.ID != null && this.ID.Length > 0)
				{
					return this.ID;
				}
				return null;
			}
		}

		/// <summary>Gets or sets the type of navigation user interface (UI) to display for a step in a <see cref="T:System.Web.UI.WebControls.Wizard" /> control.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.WizardStepType" /> enumeration values. The default value is WizardStepType.Auto.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The selected value is not one of the <see cref="T:System.Web.UI.WebControls.WizardStepType" /> enumeration values.</exception>
		// Token: 0x17001022 RID: 4130
		// (get) Token: 0x0600330B RID: 13067 RVA: 0x00089460 File Offset: 0x00087660
		// (set) Token: 0x0600330C RID: 13068 RVA: 0x00089489 File Offset: 0x00087689
		[DefaultValue(WizardStepType.Auto)]
		public virtual WizardStepType StepType
		{
			get
			{
				object obj = this.ViewState["StepType"];
				if (obj == null)
				{
					return WizardStepType.Auto;
				}
				return (WizardStepType)obj;
			}
			set
			{
				this.ViewState["StepType"] = value;
			}
		}

		/// <summary>Gets or sets the title to use for a step in a <see cref="T:System.Web.UI.WebControls.Wizard" /> control when the sidebar feature is enabled.</summary>
		/// <returns>The title to use for a step in a <see cref="T:System.Web.UI.WebControls.Wizard" /> control when the sidebar feature is enabled. The default value is an empty string ("").</returns>
		// Token: 0x17001023 RID: 4131
		// (get) Token: 0x0600330D RID: 13069 RVA: 0x000894A4 File Offset: 0x000876A4
		// (set) Token: 0x0600330E RID: 13070 RVA: 0x000894D1 File Offset: 0x000876D1
		[Localizable(true)]
		[DefaultValue("")]
		public virtual string Title
		{
			get
			{
				object obj = this.ViewState["Title"];
				if (obj == null)
				{
					return string.Empty;
				}
				return (string)obj;
			}
			set
			{
				this.ViewState["Title"] = value;
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.UI.WebControls.Wizard" /> control that is the parent of the object derived from <see cref="T:System.Web.UI.WebControls.WizardStepBase" />.</summary>
		/// <returns>The <see cref="T:System.Web.UI.WebControls.Wizard" /> control that is the parent of the object derived from <see cref="T:System.Web.UI.WebControls.WizardStepBase" />.</returns>
		// Token: 0x17001024 RID: 4132
		// (get) Token: 0x0600330F RID: 13071 RVA: 0x000894E4 File Offset: 0x000876E4
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[Browsable(false)]
		public Wizard Wizard
		{
			get
			{
				return this.wizard;
			}
		}

		/// <param name="savedState">An <see cref="T:System.Object" /> that represents the <see cref="T:System.Web.UI.WebControls.WizardStepBase" /> control to restore.</param>
		// Token: 0x06003310 RID: 13072 RVA: 0x000894EC File Offset: 0x000876EC
		protected override void LoadViewState(object savedState)
		{
			base.LoadViewState(savedState);
		}

		/// <summary>Raises the <see cref="M:System.Web.UI.Control.OnLoad(System.EventArgs)" /> event.</summary>
		/// <param name="e">The <see cref="T:System.EventArgs" /> object that contains the event data.</param>
		// Token: 0x06003311 RID: 13073 RVA: 0x000894F5 File Offset: 0x000876F5
		protected internal override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
		}

		/// <summary>Outputs the content of the <see cref="T:System.Web.UI.WebControls.WizardStepBase" /> control's child controls to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object, which writes the content to be rendered on the client.</summary>
		/// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
		// Token: 0x06003312 RID: 13074 RVA: 0x000894FE File Offset: 0x000876FE
		protected internal override void RenderChildren(HtmlTextWriter writer)
		{
			base.RenderChildren(writer);
		}

		// Token: 0x06003313 RID: 13075 RVA: 0x00089507 File Offset: 0x00087707
		internal void SetWizard(Wizard w)
		{
			this.wizard = w;
		}

		// Token: 0x04001CBF RID: 7359
		private Wizard wizard;
	}
}
