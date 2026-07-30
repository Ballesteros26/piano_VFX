using System;
using System.Collections;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides navigation and a user interface (UI) to collect related data across multiple steps.</summary>
	// Token: 0x0200043F RID: 1087
	[Bindable(false)]
	[DefaultEvent("FinishButtonClick")]
	[Designer("System.Web.UI.Design.WebControls.WizardDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[ToolboxData("<{0}:Wizard runat=\"server\"> <WizardSteps> <asp:WizardStep title=\"Step 1\" runat=\"server\"></asp:WizardStep> <asp:WizardStep title=\"Step 2\" runat=\"server\"></asp:WizardStep> </WizardSteps> </{0}:Wizard>")]
	public class Wizard : CompositeControl
	{
		/// <summary>Occurs when the user switches to a new step in the control.</summary>
		// Token: 0x140000F6 RID: 246
		// (add) Token: 0x06003246 RID: 12870 RVA: 0x00086568 File Offset: 0x00084768
		// (remove) Token: 0x06003247 RID: 12871 RVA: 0x0008657B File Offset: 0x0008477B
		public event EventHandler ActiveStepChanged
		{
			add
			{
				base.Events.AddHandler(Wizard.ActiveStepChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Wizard.ActiveStepChangedEvent, value);
			}
		}

		/// <summary>Occurs when the Cancel button is clicked.</summary>
		// Token: 0x140000F7 RID: 247
		// (add) Token: 0x06003248 RID: 12872 RVA: 0x0008658E File Offset: 0x0008478E
		// (remove) Token: 0x06003249 RID: 12873 RVA: 0x000865A1 File Offset: 0x000847A1
		public event EventHandler CancelButtonClick
		{
			add
			{
				base.Events.AddHandler(Wizard.CancelButtonClickEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Wizard.CancelButtonClickEvent, value);
			}
		}

		/// <summary>Occurs when the Finish button is clicked.</summary>
		// Token: 0x140000F8 RID: 248
		// (add) Token: 0x0600324A RID: 12874 RVA: 0x000865B4 File Offset: 0x000847B4
		// (remove) Token: 0x0600324B RID: 12875 RVA: 0x000865C7 File Offset: 0x000847C7
		public event WizardNavigationEventHandler FinishButtonClick
		{
			add
			{
				base.Events.AddHandler(Wizard.FinishButtonClickEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Wizard.FinishButtonClickEvent, value);
			}
		}

		/// <summary>Occurs when the Next button is clicked.</summary>
		// Token: 0x140000F9 RID: 249
		// (add) Token: 0x0600324C RID: 12876 RVA: 0x000865DA File Offset: 0x000847DA
		// (remove) Token: 0x0600324D RID: 12877 RVA: 0x000865ED File Offset: 0x000847ED
		public event WizardNavigationEventHandler NextButtonClick
		{
			add
			{
				base.Events.AddHandler(Wizard.NextButtonClickEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Wizard.NextButtonClickEvent, value);
			}
		}

		/// <summary>Occurs when the Previous button is clicked.</summary>
		// Token: 0x140000FA RID: 250
		// (add) Token: 0x0600324E RID: 12878 RVA: 0x00086600 File Offset: 0x00084800
		// (remove) Token: 0x0600324F RID: 12879 RVA: 0x00086613 File Offset: 0x00084813
		public event WizardNavigationEventHandler PreviousButtonClick
		{
			add
			{
				base.Events.AddHandler(Wizard.PreviousButtonClickEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Wizard.PreviousButtonClickEvent, value);
			}
		}

		/// <summary>Occurs when a button in the sidebar area is clicked.</summary>
		// Token: 0x140000FB RID: 251
		// (add) Token: 0x06003250 RID: 12880 RVA: 0x00086626 File Offset: 0x00084826
		// (remove) Token: 0x06003251 RID: 12881 RVA: 0x00086639 File Offset: 0x00084839
		public event WizardNavigationEventHandler SideBarButtonClick
		{
			add
			{
				base.Events.AddHandler(Wizard.SideBarButtonClickEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Wizard.SideBarButtonClickEvent, value);
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.Wizard.ActiveStepChanged" /> event.</summary>
		/// <param name="source">The source of the event.</param>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06003252 RID: 12882 RVA: 0x0008664C File Offset: 0x0008484C
		protected virtual void OnActiveStepChanged(object source, EventArgs e)
		{
			if (base.Events != null)
			{
				EventHandler eventHandler = (EventHandler)base.Events[Wizard.ActiveStepChangedEvent];
				if (eventHandler != null)
				{
					eventHandler(source, e);
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.Wizard.CancelButtonClick" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> containing the event data.</param>
		// Token: 0x06003253 RID: 12883 RVA: 0x00086684 File Offset: 0x00084884
		protected virtual void OnCancelButtonClick(EventArgs e)
		{
			if (base.Events != null)
			{
				EventHandler eventHandler = (EventHandler)base.Events[Wizard.CancelButtonClickEvent];
				if (eventHandler != null)
				{
					eventHandler(this, e);
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.Wizard.FinishButtonClick" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Web.UI.WebControls.WizardNavigationEventArgs" /> containing the event data.</param>
		// Token: 0x06003254 RID: 12884 RVA: 0x000866BC File Offset: 0x000848BC
		protected virtual void OnFinishButtonClick(WizardNavigationEventArgs e)
		{
			if (base.Events != null)
			{
				WizardNavigationEventHandler wizardNavigationEventHandler = (WizardNavigationEventHandler)base.Events[Wizard.FinishButtonClickEvent];
				if (wizardNavigationEventHandler != null)
				{
					wizardNavigationEventHandler(this, e);
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.Wizard.NextButtonClick" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Web.UI.WebControls.WizardNavigationEventArgs" /> containing the event data.</param>
		// Token: 0x06003255 RID: 12885 RVA: 0x000866F4 File Offset: 0x000848F4
		protected virtual void OnNextButtonClick(WizardNavigationEventArgs e)
		{
			if (base.Events != null)
			{
				WizardNavigationEventHandler wizardNavigationEventHandler = (WizardNavigationEventHandler)base.Events[Wizard.NextButtonClickEvent];
				if (wizardNavigationEventHandler != null)
				{
					wizardNavigationEventHandler(this, e);
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.Wizard.PreviousButtonClick" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Web.UI.WebControls.WizardNavigationEventArgs" /> containing event data.</param>
		// Token: 0x06003256 RID: 12886 RVA: 0x0008672C File Offset: 0x0008492C
		protected virtual void OnPreviousButtonClick(WizardNavigationEventArgs e)
		{
			if (base.Events != null)
			{
				WizardNavigationEventHandler wizardNavigationEventHandler = (WizardNavigationEventHandler)base.Events[Wizard.PreviousButtonClickEvent];
				if (wizardNavigationEventHandler != null)
				{
					wizardNavigationEventHandler(this, e);
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.Wizard.SideBarButtonClick" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Web.UI.WebControls.WizardNavigationEventArgs" /> containing event data.</param>
		// Token: 0x06003257 RID: 12887 RVA: 0x00086764 File Offset: 0x00084964
		protected virtual void OnSideBarButtonClick(WizardNavigationEventArgs e)
		{
			if (base.Events != null)
			{
				WizardNavigationEventHandler wizardNavigationEventHandler = (WizardNavigationEventHandler)base.Events[Wizard.SideBarButtonClickEvent];
				if (wizardNavigationEventHandler != null)
				{
					wizardNavigationEventHandler(this, e);
				}
			}
		}

		/// <summary>Gets the step in the <see cref="P:System.Web.UI.WebControls.Wizard.WizardSteps" /> collection that is currently displayed to the user.</summary>
		/// <returns>The <see cref="T:System.Web.UI.WebControls.WizardStepBase" /> that is currently displayed to the user.</returns>
		/// <exception cref="T:System.InvalidOperationException">The corresponding <see cref="P:System.Web.UI.WebControls.Wizard.ActiveStepIndex" /> is less than -1 or greater than the number of <see cref="T:System.Web.UI.WebControls.WizardStepBase" /> objects in the <see cref="T:System.Web.UI.WebControls.Wizard" />.</exception>
		// Token: 0x17000FE5 RID: 4069
		// (get) Token: 0x06003258 RID: 12888 RVA: 0x0008679C File Offset: 0x0008499C
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public WizardStepBase ActiveStep
		{
			get
			{
				int num = this.ActiveStepIndex;
				if (num < -1 || num >= this.WizardSteps.Count)
				{
					throw new InvalidOperationException("ActiveStepIndex has an invalid value.");
				}
				if (num == -1)
				{
					return null;
				}
				return this.WizardSteps[num];
			}
		}

		/// <summary>Gets or sets the index of the current <see cref="T:System.Web.UI.WebControls.WizardStepBase" /> object.</summary>
		/// <returns>The index of the <see cref="T:System.Web.UI.WebControls.WizardStepBase" /> that is currently displayed in the <see cref="T:System.Web.UI.WebControls.Wizard" /> control.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The selected value is higher than the number of wizard steps defined in the <see cref="P:System.Web.UI.WebControls.Wizard.WizardSteps" /> collection.</exception>
		// Token: 0x17000FE6 RID: 4070
		// (get) Token: 0x06003259 RID: 12889 RVA: 0x000867DF File Offset: 0x000849DF
		// (set) Token: 0x0600325A RID: 12890 RVA: 0x000867E8 File Offset: 0x000849E8
		[DefaultValue(-1)]
		[Themeable(false)]
		public virtual int ActiveStepIndex
		{
			get
			{
				return this.activeStepIndex;
			}
			set
			{
				if (value < -1 || (value > this.WizardSteps.Count && (this.inited || this.WizardSteps.Count > 0)))
				{
					throw new ArgumentOutOfRangeException("The ActiveStepIndex must be less than WizardSteps.Count and at least -1");
				}
				if (this.inited && !this.AllowNavigationToStep(value))
				{
					return;
				}
				if (this.activeStepIndex != value)
				{
					this.activeStepIndex = value;
					if (this.inited)
					{
						this.multiView.ActiveViewIndex = value;
						if (this.stepDatalist != null)
						{
							this.stepDatalist.SelectedIndex = value;
							this.stepDatalist.DataBind();
						}
						this.OnActiveStepChanged(this, EventArgs.Empty);
					}
				}
			}
		}

		/// <summary>Gets or sets the URL of the image displayed for the Cancel button.</summary>
		/// <returns>The URL of the image displayed for the Cancel button on the <see cref="T:System.Web.UI.WebControls.Wizard" /> control. The default value is an empty string ("").</returns>
		// Token: 0x17000FE7 RID: 4071
		// (get) Token: 0x0600325B RID: 12891 RVA: 0x0008688C File Offset: 0x00084A8C
		// (set) Token: 0x0600325C RID: 12892 RVA: 0x0004C21D File Offset: 0x0004A41D
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue("")]
		[UrlProperty]
		public virtual string CancelButtonImageUrl
		{
			get
			{
				object obj = this.ViewState["CancelButtonImageUrl"];
				if (obj == null)
				{
					return string.Empty;
				}
				return (string)obj;
			}
			set
			{
				this.ViewState["CancelButtonImageUrl"] = value;
			}
		}

		/// <summary>Gets a reference to a collection of style properties that define the appearance of the Cancel button.</summary>
		/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.Style" /> that defines the style settings for Cancel on the <see cref="T:System.Web.UI.WebControls.Wizard" />.</returns>
		// Token: 0x17000FE8 RID: 4072
		// (get) Token: 0x0600325D RID: 12893 RVA: 0x000868B9 File Offset: 0x00084AB9
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[DefaultValue(null)]
		public Style CancelButtonStyle
		{
			get
			{
				if (this.cancelButtonStyle == null)
				{
					this.cancelButtonStyle = new Style();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this.cancelButtonStyle).TrackViewState();
					}
				}
				return this.cancelButtonStyle;
			}
		}

		/// <summary>Gets or sets the text caption that is displayed for the Cancel button.</summary>
		/// <returns>The text caption displayed for Cancel on the <see cref="T:System.Web.UI.WebControls.Wizard" />. The default is "Cancel". The default text for the control is localized based on the current locale for the server.</returns>
		// Token: 0x17000FE9 RID: 4073
		// (get) Token: 0x0600325E RID: 12894 RVA: 0x000868E8 File Offset: 0x00084AE8
		// (set) Token: 0x0600325F RID: 12895 RVA: 0x0004C275 File Offset: 0x0004A475
		[Localizable(true)]
		public virtual string CancelButtonText
		{
			get
			{
				object obj = this.ViewState["CancelButtonText"];
				if (obj == null)
				{
					return "Cancel";
				}
				return (string)obj;
			}
			set
			{
				this.ViewState["CancelButtonText"] = value;
			}
		}

		/// <summary>Gets or sets the type of button that is rendered as the Cancel button.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.ButtonType" /> values. The default is <see cref="F:System.Web.UI.WebControls.ButtonType.Button" />.</returns>
		// Token: 0x17000FEA RID: 4074
		// (get) Token: 0x06003260 RID: 12896 RVA: 0x00086918 File Offset: 0x00084B18
		// (set) Token: 0x06003261 RID: 12897 RVA: 0x0004C2B3 File Offset: 0x0004A4B3
		[DefaultValue(ButtonType.Button)]
		public virtual ButtonType CancelButtonType
		{
			get
			{
				object obj = this.ViewState["CancelButtonType"];
				if (obj == null)
				{
					return ButtonType.Button;
				}
				return (ButtonType)obj;
			}
			set
			{
				this.ViewState["CancelButtonType"] = value;
			}
		}

		/// <summary>Gets or sets the URL that the user is directed to when they click the Cancel button.</summary>
		/// <returns>The URL that the user is redirected to when they click Cancel on the <see cref="T:System.Web.UI.WebControls.Wizard" />. The default is an empty string ("").</returns>
		// Token: 0x17000FEB RID: 4075
		// (get) Token: 0x06003262 RID: 12898 RVA: 0x00086944 File Offset: 0x00084B44
		// (set) Token: 0x06003263 RID: 12899 RVA: 0x0004C2E2 File Offset: 0x0004A4E2
		[DefaultValue("")]
		[Themeable(false)]
		[Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[UrlProperty]
		public virtual string CancelDestinationPageUrl
		{
			get
			{
				object obj = this.ViewState["CancelDestinationPageUrl"];
				if (obj == null)
				{
					return string.Empty;
				}
				return (string)obj;
			}
			set
			{
				this.ViewState["CancelDestinationPageUrl"] = value;
			}
		}

		/// <summary>Gets or sets the amount of space between the contents of the cell and the cell border.</summary>
		/// <returns>The amount of space, in pixels, between the contents of a cell and the cell border. The default is 0.</returns>
		// Token: 0x17000FEC RID: 4076
		// (get) Token: 0x06003264 RID: 12900 RVA: 0x00086971 File Offset: 0x00084B71
		// (set) Token: 0x06003265 RID: 12901 RVA: 0x0005A61F File Offset: 0x0005881F
		[DefaultValue(0)]
		public virtual int CellPadding
		{
			get
			{
				if (base.ControlStyleCreated)
				{
					return ((TableStyle)base.ControlStyle).CellPadding;
				}
				return 0;
			}
			set
			{
				((TableStyle)base.ControlStyle).CellPadding = value;
			}
		}

		/// <summary>Gets or sets the amount of space between cells.</summary>
		/// <returns>The amount of space, in pixels, between cells. The default is 0.</returns>
		// Token: 0x17000FED RID: 4077
		// (get) Token: 0x06003266 RID: 12902 RVA: 0x0005A632 File Offset: 0x00058832
		// (set) Token: 0x06003267 RID: 12903 RVA: 0x0005A64E File Offset: 0x0005884E
		[DefaultValue(0)]
		public virtual int CellSpacing
		{
			get
			{
				if (base.ControlStyleCreated)
				{
					return ((TableStyle)base.ControlStyle).CellSpacing;
				}
				return 0;
			}
			set
			{
				((TableStyle)base.ControlStyle).CellSpacing = value;
			}
		}

		/// <summary>Gets or sets a Boolean value indicating whether to display a Cancel button.</summary>
		/// <returns>true to display Cancel on the <see cref="T:System.Web.UI.WebControls.Wizard" />; otherwise, false. The default is false.This property cannot be set by themes or style sheet themes. For more information, see <see cref="T:System.Web.UI.ThemeableAttribute" /> and ASP.NET Themes and Skins.</returns>
		// Token: 0x17000FEE RID: 4078
		// (get) Token: 0x06003268 RID: 12904 RVA: 0x00086990 File Offset: 0x00084B90
		// (set) Token: 0x06003269 RID: 12905 RVA: 0x000869B9 File Offset: 0x00084BB9
		[Themeable(false)]
		[DefaultValue(false)]
		public virtual bool DisplayCancelButton
		{
			get
			{
				object obj = this.ViewState["DisplayCancelButton"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["DisplayCancelButton"] = value;
			}
		}

		/// <summary>Gets or sets a Boolean value indicating whether to display the sidebar area on the <see cref="T:System.Web.UI.WebControls.Wizard" /> control.</summary>
		/// <returns>true to display the sidebar area on the <see cref="T:System.Web.UI.WebControls.Wizard" />; otherwise, false. The default is true.This property cannot be set by themes or style sheet themes. For more information, see <see cref="T:System.Web.UI.ThemeableAttribute" /> and ASP.NET Themes and Skins.</returns>
		// Token: 0x17000FEF RID: 4079
		// (get) Token: 0x0600326A RID: 12906 RVA: 0x000869D4 File Offset: 0x00084BD4
		// (set) Token: 0x0600326B RID: 12907 RVA: 0x000869FD File Offset: 0x00084BFD
		[Themeable(false)]
		[DefaultValue(true)]
		public virtual bool DisplaySideBar
		{
			get
			{
				object obj = this.ViewState["DisplaySideBar"];
				return obj == null || (bool)obj;
			}
			set
			{
				this.ViewState["DisplaySideBar"] = value;
				this.UpdateViews();
			}
		}

		/// <summary>Gets or sets the URL of the image that is displayed for the Finish button.</summary>
		/// <returns>The URL of the image displayed for Finish on the <see cref="T:System.Web.UI.WebControls.Wizard" />. The default is an empty string ("").</returns>
		// Token: 0x17000FF0 RID: 4080
		// (get) Token: 0x0600326C RID: 12908 RVA: 0x00086A1C File Offset: 0x00084C1C
		// (set) Token: 0x0600326D RID: 12909 RVA: 0x00086A49 File Offset: 0x00084C49
		[DefaultValue("")]
		[UrlProperty]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public virtual string FinishCompleteButtonImageUrl
		{
			get
			{
				object obj = this.ViewState["FinishCompleteButtonImageUrl"];
				if (obj == null)
				{
					return string.Empty;
				}
				return (string)obj;
			}
			set
			{
				this.ViewState["FinishCompleteButtonImageUrl"] = value;
			}
		}

		/// <summary>Gets a reference to a <see cref="T:System.Web.UI.WebControls.Style" /> object that defines the settings for the Finish button.</summary>
		/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.Style" /> that defines the style settings for Finish on the <see cref="T:System.Web.UI.WebControls.Wizard" />.</returns>
		// Token: 0x17000FF1 RID: 4081
		// (get) Token: 0x0600326E RID: 12910 RVA: 0x00086A5C File Offset: 0x00084C5C
		[NotifyParentProperty(true)]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public Style FinishCompleteButtonStyle
		{
			get
			{
				if (this.finishCompleteButtonStyle == null)
				{
					this.finishCompleteButtonStyle = new Style();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this.finishCompleteButtonStyle).TrackViewState();
					}
				}
				return this.finishCompleteButtonStyle;
			}
		}

		/// <summary>Gets or sets the text caption that is displayed for the Finish button.</summary>
		/// <returns>The text caption displayed for Finish on the <see cref="T:System.Web.UI.WebControls.Wizard" />. The default is "Finish". The default text for the control is localized based on the current locale for the server.</returns>
		// Token: 0x17000FF2 RID: 4082
		// (get) Token: 0x0600326F RID: 12911 RVA: 0x00086A8C File Offset: 0x00084C8C
		// (set) Token: 0x06003270 RID: 12912 RVA: 0x00086AB9 File Offset: 0x00084CB9
		[Localizable(true)]
		public virtual string FinishCompleteButtonText
		{
			get
			{
				object obj = this.ViewState["FinishCompleteButtonText"];
				if (obj == null)
				{
					return "Finish";
				}
				return (string)obj;
			}
			set
			{
				this.ViewState["FinishCompleteButtonText"] = value;
			}
		}

		/// <summary>Gets or sets the type of button that is rendered as the Finish button.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.ButtonType" /> values. The default is <see cref="F:System.Web.UI.WebControls.ButtonType.Button" />.</returns>
		// Token: 0x17000FF3 RID: 4083
		// (get) Token: 0x06003271 RID: 12913 RVA: 0x00086ACC File Offset: 0x00084CCC
		// (set) Token: 0x06003272 RID: 12914 RVA: 0x00086AF5 File Offset: 0x00084CF5
		[DefaultValue(ButtonType.Button)]
		public virtual ButtonType FinishCompleteButtonType
		{
			get
			{
				object obj = this.ViewState["FinishCompleteButtonType"];
				if (obj == null)
				{
					return ButtonType.Button;
				}
				return (ButtonType)obj;
			}
			set
			{
				this.ViewState["FinishCompleteButtonType"] = value;
			}
		}

		/// <summary>Gets or sets the URL that the user is redirected to when they click the Finish button.</summary>
		/// <returns>The URL that the user is redirected to when they click Finish on the <see cref="T:System.Web.UI.WebControls.Wizard" />. The default is an empty string ("").</returns>
		// Token: 0x17000FF4 RID: 4084
		// (get) Token: 0x06003273 RID: 12915 RVA: 0x00086B10 File Offset: 0x00084D10
		// (set) Token: 0x06003274 RID: 12916 RVA: 0x00086B3D File Offset: 0x00084D3D
		[Themeable(false)]
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[UrlProperty]
		public virtual string FinishDestinationPageUrl
		{
			get
			{
				object obj = this.ViewState["FinishDestinationPageUrl"];
				if (obj == null)
				{
					return string.Empty;
				}
				return (string)obj;
			}
			set
			{
				this.ViewState["FinishDestinationPageUrl"] = value;
			}
		}

		/// <summary>Gets or sets the template that is used to display the navigation area on the <see cref="F:System.Web.UI.WebControls.WizardStepType.Finish" /> step.</summary>
		/// <returns>The <see cref="T:System.Web.UI.ITemplate" /> that defines the content for the navigation area for the <see cref="F:System.Web.UI.WebControls.WizardStepType.Finish" /> on the <see cref="T:System.Web.UI.WebControls.Wizard" />. The default is null.</returns>
		// Token: 0x17000FF5 RID: 4085
		// (get) Token: 0x06003275 RID: 12917 RVA: 0x00086B50 File Offset: 0x00084D50
		// (set) Token: 0x06003276 RID: 12918 RVA: 0x00086B58 File Offset: 0x00084D58
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(Wizard), BindingDirection.OneWay)]
		[DefaultValue(null)]
		[Browsable(false)]
		public virtual ITemplate FinishNavigationTemplate
		{
			get
			{
				return this.finishNavigationTemplate;
			}
			set
			{
				this.finishNavigationTemplate = value;
				this.UpdateViews();
			}
		}

		/// <summary>Gets or sets the URL of the image that is displayed for the Previous button on the <see cref="F:System.Web.UI.WebControls.WizardStepType.Finish" /> step.</summary>
		/// <returns>The URL of the image displayed for Previous on the <see cref="F:System.Web.UI.WebControls.WizardStepType.Finish" /> of the <see cref="T:System.Web.UI.WebControls.Wizard" />. The default is an empty string ("").</returns>
		// Token: 0x17000FF6 RID: 4086
		// (get) Token: 0x06003277 RID: 12919 RVA: 0x00086B68 File Offset: 0x00084D68
		// (set) Token: 0x06003278 RID: 12920 RVA: 0x00086B95 File Offset: 0x00084D95
		[UrlProperty]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue("")]
		public virtual string FinishPreviousButtonImageUrl
		{
			get
			{
				object obj = this.ViewState["FinishPreviousButtonImageUrl"];
				if (obj == null)
				{
					return string.Empty;
				}
				return (string)obj;
			}
			set
			{
				this.ViewState["FinishPreviousButtonImageUrl"] = value;
			}
		}

		/// <summary>Gets a reference to a <see cref="T:System.Web.UI.WebControls.Style" /> object that defines the settings for the Previous button on the <see cref="F:System.Web.UI.WebControls.WizardStepType.Finish" /> step.</summary>
		/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.Style" /> that defines the style settings for Previous on the <see cref="F:System.Web.UI.WebControls.WizardStepType.Finish" /> of the <see cref="T:System.Web.UI.WebControls.Wizard" />.</returns>
		// Token: 0x17000FF7 RID: 4087
		// (get) Token: 0x06003279 RID: 12921 RVA: 0x00086BA8 File Offset: 0x00084DA8
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public Style FinishPreviousButtonStyle
		{
			get
			{
				if (this.finishPreviousButtonStyle == null)
				{
					this.finishPreviousButtonStyle = new Style();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this.finishPreviousButtonStyle).TrackViewState();
					}
				}
				return this.finishPreviousButtonStyle;
			}
		}

		/// <summary>Gets or sets the text caption that is displayed for the Previous button on the <see cref="F:System.Web.UI.WebControls.WizardStepType.Finish" /> step.</summary>
		/// <returns>The text caption displayed for Previous on the <see cref="F:System.Web.UI.WebControls.WizardStepType.Finish" /> of the <see cref="T:System.Web.UI.WebControls.Wizard" />. The default is "Previous". The default text for the control is localized based on the current locale for the server.</returns>
		// Token: 0x17000FF8 RID: 4088
		// (get) Token: 0x0600327A RID: 12922 RVA: 0x00086BD8 File Offset: 0x00084DD8
		// (set) Token: 0x0600327B RID: 12923 RVA: 0x00086C05 File Offset: 0x00084E05
		[Localizable(true)]
		public virtual string FinishPreviousButtonText
		{
			get
			{
				object obj = this.ViewState["FinishPreviousButtonText"];
				if (obj == null)
				{
					return "Previous";
				}
				return (string)obj;
			}
			set
			{
				this.ViewState["FinishPreviousButtonText"] = value;
			}
		}

		/// <summary>Gets or sets the type of button that is rendered as the Previous button on the <see cref="F:System.Web.UI.WebControls.WizardStepType.Finish" /> step.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.ButtonType" /> values. The default is <see cref="F:System.Web.UI.WebControls.ButtonType.Button" />.</returns>
		// Token: 0x17000FF9 RID: 4089
		// (get) Token: 0x0600327C RID: 12924 RVA: 0x00086C18 File Offset: 0x00084E18
		// (set) Token: 0x0600327D RID: 12925 RVA: 0x00086C41 File Offset: 0x00084E41
		[DefaultValue(ButtonType.Button)]
		public virtual ButtonType FinishPreviousButtonType
		{
			get
			{
				object obj = this.ViewState["FinishPreviousButtonType"];
				if (obj == null)
				{
					return ButtonType.Button;
				}
				return (ButtonType)obj;
			}
			set
			{
				this.ViewState["FinishPreviousButtonType"] = value;
			}
		}

		/// <summary>Gets a reference to a <see cref="T:System.Web.UI.WebControls.Style" /> object that defines the settings for the header area on the control.</summary>
		/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.Style" /> that defines the style settings for the header area on the <see cref="T:System.Web.UI.WebControls.Wizard" />.</returns>
		// Token: 0x17000FFA RID: 4090
		// (get) Token: 0x0600327E RID: 12926 RVA: 0x00086C59 File Offset: 0x00084E59
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[DefaultValue(null)]
		public TableItemStyle HeaderStyle
		{
			get
			{
				if (this.headerStyle == null)
				{
					this.headerStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this.headerStyle).TrackViewState();
					}
				}
				return this.headerStyle;
			}
		}

		/// <summary>Gets or sets the template that is used to display the header area on the control.</summary>
		/// <returns>An <see cref="T:System.Web.UI.ITemplate" /> that contains the template for displaying the header area on the <see cref="T:System.Web.UI.WebControls.Wizard" />. The default is null.</returns>
		// Token: 0x17000FFB RID: 4091
		// (get) Token: 0x0600327F RID: 12927 RVA: 0x00086C87 File Offset: 0x00084E87
		// (set) Token: 0x06003280 RID: 12928 RVA: 0x00086C8F File Offset: 0x00084E8F
		[Browsable(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(Wizard), BindingDirection.OneWay)]
		[DefaultValue(null)]
		public virtual ITemplate HeaderTemplate
		{
			get
			{
				return this.headerTemplate;
			}
			set
			{
				this.headerTemplate = value;
				this.UpdateViews();
			}
		}

		/// <summary>Gets or sets the text caption that is displayed for the header area on the control.</summary>
		/// <returns>The text caption displayed for the header area on the <see cref="T:System.Web.UI.WebControls.Wizard" />. The default is an empty string ("").</returns>
		// Token: 0x17000FFC RID: 4092
		// (get) Token: 0x06003281 RID: 12929 RVA: 0x00086CA0 File Offset: 0x00084EA0
		// (set) Token: 0x06003282 RID: 12930 RVA: 0x00085451 File Offset: 0x00083651
		[Localizable(true)]
		[DefaultValue("")]
		public virtual string HeaderText
		{
			get
			{
				object obj = this.ViewState["HeaderText"];
				if (obj == null)
				{
					return string.Empty;
				}
				return (string)obj;
			}
			set
			{
				this.ViewState["HeaderText"] = value;
			}
		}

		/// <summary>Gets or sets the custom content of the root container in a <see cref="T:System.Web.UI.WebControls.Wizard" /> control.</summary>
		/// <returns>An object that contains the custom content for the root container in a <see cref="T:System.Web.UI.WebControls.Wizard" /> control. The default is null, which indicates that this property is not set.</returns>
		// Token: 0x17000FFD RID: 4093
		// (get) Token: 0x06003283 RID: 12931 RVA: 0x00086CCD File Offset: 0x00084ECD
		// (set) Token: 0x06003284 RID: 12932 RVA: 0x00086CD5 File Offset: 0x00084ED5
		[Browsable(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(Wizard))]
		[DefaultValue(null)]
		public virtual ITemplate LayoutTemplate { get; set; }

		/// <summary>Gets a reference to a <see cref="T:System.Web.UI.WebControls.Style" /> object that defines the settings for the buttons in the navigation area on the control.</summary>
		/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.Style" /> that defines the style settings for the buttons in the navigation area on the <see cref="T:System.Web.UI.WebControls.Wizard" />.</returns>
		// Token: 0x17000FFE RID: 4094
		// (get) Token: 0x06003285 RID: 12933 RVA: 0x00086CDE File Offset: 0x00084EDE
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[DefaultValue(null)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public Style NavigationButtonStyle
		{
			get
			{
				if (this.navigationButtonStyle == null)
				{
					this.navigationButtonStyle = new Style();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this.navigationButtonStyle).TrackViewState();
					}
				}
				return this.navigationButtonStyle;
			}
		}

		/// <summary>Gets a reference to a <see cref="T:System.Web.UI.WebControls.Style" /> object that defines the settings for the navigation area on the control.</summary>
		/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.Style" /> that defines the style settings for the navigation area on the <see cref="T:System.Web.UI.WebControls.Wizard" />.</returns>
		// Token: 0x17000FFF RID: 4095
		// (get) Token: 0x06003286 RID: 12934 RVA: 0x00086D0C File Offset: 0x00084F0C
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public TableItemStyle NavigationStyle
		{
			get
			{
				if (this.navigationStyle == null)
				{
					this.navigationStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this.navigationStyle).TrackViewState();
					}
				}
				return this.navigationStyle;
			}
		}

		/// <summary>Gets a reference to a <see cref="T:System.Web.UI.WebControls.Style" /> object that defines the settings for the sidebar area on the control.</summary>
		/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.Style" /> that defines the style settings for the sidebar area on the <see cref="T:System.Web.UI.WebControls.Wizard" />.</returns>
		// Token: 0x17001000 RID: 4096
		// (get) Token: 0x06003287 RID: 12935 RVA: 0x00086D3A File Offset: 0x00084F3A
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[DefaultValue(null)]
		[NotifyParentProperty(true)]
		public TableItemStyle SideBarStyle
		{
			get
			{
				if (this.sideBarStyle == null)
				{
					this.sideBarStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this.sideBarStyle).TrackViewState();
					}
				}
				return this.sideBarStyle;
			}
		}

		/// <summary>Gets a reference to a <see cref="T:System.Web.UI.WebControls.Style" /> object that defines the settings for the buttons on the sidebar.</summary>
		/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.Style" /> that defines the style settings for the buttons on the sidebar of the <see cref="T:System.Web.UI.WebControls.Wizard" />.</returns>
		// Token: 0x17001001 RID: 4097
		// (get) Token: 0x06003288 RID: 12936 RVA: 0x00086D68 File Offset: 0x00084F68
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue(null)]
		[NotifyParentProperty(true)]
		public Style SideBarButtonStyle
		{
			get
			{
				if (this.sideBarButtonStyle == null)
				{
					this.sideBarButtonStyle = new Style();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this.sideBarButtonStyle).TrackViewState();
					}
				}
				return this.sideBarButtonStyle;
			}
		}

		/// <summary>Gets or sets the template that is used to display the sidebar area on the control.</summary>
		/// <returns>An <see cref="T:System.Web.UI.ITemplate" /> that contains the template for displaying the sidebar area on the <see cref="T:System.Web.UI.WebControls.Wizard" />. The default is null.</returns>
		// Token: 0x17001002 RID: 4098
		// (get) Token: 0x06003289 RID: 12937 RVA: 0x00086D96 File Offset: 0x00084F96
		// (set) Token: 0x0600328A RID: 12938 RVA: 0x00086D9E File Offset: 0x00084F9E
		[Browsable(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(Wizard), BindingDirection.OneWay)]
		[DefaultValue(null)]
		public virtual ITemplate SideBarTemplate
		{
			get
			{
				return this.sideBarTemplate;
			}
			set
			{
				this.sideBarTemplate = value;
				this.UpdateViews();
			}
		}

		/// <summary>Gets or sets a value that is used to render alternate text that notifies screen readers to skip the content in the sidebar area.</summary>
		/// <returns>A string that the <see cref="T:System.Web.UI.WebControls.Wizard" /> renders as alternate text with an invisible image, as a hint to screen readers. The default is "Skip Navigation Links". The default text for the control is localized based on the current locale for the server.</returns>
		// Token: 0x17001003 RID: 4099
		// (get) Token: 0x0600328B RID: 12939 RVA: 0x00086DB0 File Offset: 0x00084FB0
		// (set) Token: 0x0600328C RID: 12940 RVA: 0x0006BD85 File Offset: 0x00069F85
		[Localizable(true)]
		public virtual string SkipLinkText
		{
			get
			{
				object obj = this.ViewState["SkipLinkText"];
				if (obj == null)
				{
					return "Skip Navigation Links.";
				}
				return (string)obj;
			}
			set
			{
				this.ViewState["SkipLinkText"] = value;
			}
		}

		/// <summary>Gets or sets the template that is used to display the navigation area on the <see cref="F:System.Web.UI.WebControls.WizardStepType.Start" /> step of the <see cref="T:System.Web.UI.WebControls.Wizard" /> control.</summary>
		/// <returns>An <see cref="T:System.Web.UI.ITemplate" /> that contains the template for displaying the navigation area on the <see cref="F:System.Web.UI.WebControls.WizardStepType.Start" /> for the <see cref="T:System.Web.UI.WebControls.Wizard" />. The default is null.</returns>
		// Token: 0x17001004 RID: 4100
		// (get) Token: 0x0600328D RID: 12941 RVA: 0x00086DDD File Offset: 0x00084FDD
		// (set) Token: 0x0600328E RID: 12942 RVA: 0x00086DE5 File Offset: 0x00084FE5
		[Browsable(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(Wizard), BindingDirection.OneWay)]
		[DefaultValue(null)]
		public virtual ITemplate StartNavigationTemplate
		{
			get
			{
				return this.startNavigationTemplate;
			}
			set
			{
				this.startNavigationTemplate = value;
				this.UpdateViews();
			}
		}

		/// <summary>Gets or sets the URL of the image that is displayed for the Next button on the <see cref="F:System.Web.UI.WebControls.WizardStepType.Start" /> step.</summary>
		/// <returns>The URL of the image displayed for Next on the <see cref="F:System.Web.UI.WebControls.WizardStepType.Start" /> of the <see cref="T:System.Web.UI.WebControls.Wizard" />. The default is an empty string ("").</returns>
		// Token: 0x17001005 RID: 4101
		// (get) Token: 0x0600328F RID: 12943 RVA: 0x00086DF4 File Offset: 0x00084FF4
		// (set) Token: 0x06003290 RID: 12944 RVA: 0x00086E21 File Offset: 0x00085021
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[UrlProperty]
		[DefaultValue("")]
		public virtual string StartNextButtonImageUrl
		{
			get
			{
				object obj = this.ViewState["StartNextButtonImageUrl"];
				if (obj == null)
				{
					return string.Empty;
				}
				return (string)obj;
			}
			set
			{
				this.ViewState["StartNextButtonImageUrl"] = value;
			}
		}

		/// <summary>Gets a reference to a <see cref="T:System.Web.UI.WebControls.Style" /> object that defines the settings for the Next button on the <see cref="F:System.Web.UI.WebControls.WizardStepType.Start" /> step.</summary>
		/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.Style" /> that defines the style settings for Next on the <see cref="F:System.Web.UI.WebControls.WizardStepType.Start" /> of the <see cref="T:System.Web.UI.WebControls.Wizard" />.</returns>
		// Token: 0x17001006 RID: 4102
		// (get) Token: 0x06003291 RID: 12945 RVA: 0x00086E34 File Offset: 0x00085034
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[DefaultValue(null)]
		public Style StartNextButtonStyle
		{
			get
			{
				if (this.startNextButtonStyle == null)
				{
					this.startNextButtonStyle = new Style();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this.startNextButtonStyle).TrackViewState();
					}
				}
				return this.startNextButtonStyle;
			}
		}

		/// <summary>Gets or sets the text caption that is displayed for the Next button on the <see cref="F:System.Web.UI.WebControls.WizardStepType.Start" /> step.</summary>
		/// <returns>The text caption displayed for Next on the <see cref="F:System.Web.UI.WebControls.WizardStepType.Start" /> of the <see cref="T:System.Web.UI.WebControls.Wizard" />. The default is "Next". The default text for the control is localized based on the current locale for the server.</returns>
		// Token: 0x17001007 RID: 4103
		// (get) Token: 0x06003292 RID: 12946 RVA: 0x00086E64 File Offset: 0x00085064
		// (set) Token: 0x06003293 RID: 12947 RVA: 0x00086E91 File Offset: 0x00085091
		[Localizable(true)]
		public virtual string StartNextButtonText
		{
			get
			{
				object obj = this.ViewState["StartNextButtonText"];
				if (obj == null)
				{
					return "Next";
				}
				return (string)obj;
			}
			set
			{
				this.ViewState["StartNextButtonText"] = value;
			}
		}

		/// <summary>Gets or sets the type of button that is rendered as the Next button on the <see cref="F:System.Web.UI.WebControls.WizardStepType.Start" /> step.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.ButtonType" /> values. The default is <see cref="F:System.Web.UI.WebControls.ButtonType.Button" />.</returns>
		// Token: 0x17001008 RID: 4104
		// (get) Token: 0x06003294 RID: 12948 RVA: 0x00086EA4 File Offset: 0x000850A4
		// (set) Token: 0x06003295 RID: 12949 RVA: 0x00086ECD File Offset: 0x000850CD
		[DefaultValue(ButtonType.Button)]
		public virtual ButtonType StartNextButtonType
		{
			get
			{
				object obj = this.ViewState["StartNextButtonType"];
				if (obj == null)
				{
					return ButtonType.Button;
				}
				return (ButtonType)obj;
			}
			set
			{
				this.ViewState["StartNextButtonType"] = value;
			}
		}

		/// <summary>Gets or sets the template that is used to display the navigation area on any <see cref="T:System.Web.UI.WebControls.WizardStepBase" />-derived objects other than the <see cref="F:System.Web.UI.WebControls.WizardStepType.Start" />, the <see cref="F:System.Web.UI.WebControls.WizardStepType.Finish" />, or <see cref="F:System.Web.UI.WebControls.WizardStepType.Complete" /> step.</summary>
		/// <returns>An <see cref="T:System.Web.UI.ITemplate" /> that contains the template for displaying the navigation area on any <see cref="T:System.Web.UI.WebControls.WizardStepBase" />-derived objects of the <see cref="T:System.Web.UI.WebControls.Wizard" /> control other than the <see cref="F:System.Web.UI.WebControls.WizardStepType.Start" />, <see cref="F:System.Web.UI.WebControls.WizardStepType.Finish" />, or <see cref="F:System.Web.UI.WebControls.WizardStepType.Complete" />. The default is null.</returns>
		// Token: 0x17001009 RID: 4105
		// (get) Token: 0x06003296 RID: 12950 RVA: 0x00086EE5 File Offset: 0x000850E5
		// (set) Token: 0x06003297 RID: 12951 RVA: 0x00086EED File Offset: 0x000850ED
		[Browsable(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(Wizard), BindingDirection.OneWay)]
		[DefaultValue(null)]
		public virtual ITemplate StepNavigationTemplate
		{
			get
			{
				return this.stepNavigationTemplate;
			}
			set
			{
				this.stepNavigationTemplate = value;
				this.UpdateViews();
			}
		}

		/// <summary>Gets or sets the URL of the image that is displayed for the Next button.</summary>
		/// <returns>The URL of the image displayed for Next on the <see cref="T:System.Web.UI.WebControls.Wizard" />.</returns>
		// Token: 0x1700100A RID: 4106
		// (get) Token: 0x06003298 RID: 12952 RVA: 0x00086EFC File Offset: 0x000850FC
		// (set) Token: 0x06003299 RID: 12953 RVA: 0x00086F29 File Offset: 0x00085129
		[DefaultValue("")]
		[UrlProperty]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public virtual string StepNextButtonImageUrl
		{
			get
			{
				object obj = this.ViewState["StepNextButtonImageUrl"];
				if (obj == null)
				{
					return string.Empty;
				}
				return (string)obj;
			}
			set
			{
				this.ViewState["StepNextButtonImageUrl"] = value;
			}
		}

		/// <summary>Gets a reference to the <see cref="T:System.Web.UI.WebControls.Style" /> object that defines the settings for the Next button.</summary>
		/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.Style" /> that defines the style settings for Next on the <see cref="T:System.Web.UI.WebControls.Wizard" />.</returns>
		// Token: 0x1700100B RID: 4107
		// (get) Token: 0x0600329A RID: 12954 RVA: 0x00086F3C File Offset: 0x0008513C
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public Style StepNextButtonStyle
		{
			get
			{
				if (this.stepNextButtonStyle == null)
				{
					this.stepNextButtonStyle = new Style();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this.stepNextButtonStyle).TrackViewState();
					}
				}
				return this.stepNextButtonStyle;
			}
		}

		/// <summary>Gets or sets the text caption that is displayed for the Next button.</summary>
		/// <returns>The text caption displayed for Next on the <see cref="T:System.Web.UI.WebControls.Wizard" />. The default is "Next". The default text for the control is localized based on the current locale for the server.</returns>
		// Token: 0x1700100C RID: 4108
		// (get) Token: 0x0600329B RID: 12955 RVA: 0x00086F6C File Offset: 0x0008516C
		// (set) Token: 0x0600329C RID: 12956 RVA: 0x00086F99 File Offset: 0x00085199
		[Localizable(true)]
		public virtual string StepNextButtonText
		{
			get
			{
				object obj = this.ViewState["StepNextButtonText"];
				if (obj == null)
				{
					return "Next";
				}
				return (string)obj;
			}
			set
			{
				this.ViewState["StepNextButtonText"] = value;
			}
		}

		/// <summary>Gets or sets the type of button that is rendered as the Next button.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.ButtonType" /> values. The default is <see cref="F:System.Web.UI.WebControls.ButtonType.Button" />.</returns>
		// Token: 0x1700100D RID: 4109
		// (get) Token: 0x0600329D RID: 12957 RVA: 0x00086FAC File Offset: 0x000851AC
		// (set) Token: 0x0600329E RID: 12958 RVA: 0x00086FD5 File Offset: 0x000851D5
		[DefaultValue(ButtonType.Button)]
		public virtual ButtonType StepNextButtonType
		{
			get
			{
				object obj = this.ViewState["StepNextButtonType"];
				if (obj == null)
				{
					return ButtonType.Button;
				}
				return (ButtonType)obj;
			}
			set
			{
				this.ViewState["StepNextButtonType"] = value;
			}
		}

		/// <summary>Gets or sets the URL of the image that is displayed for the Previous button.</summary>
		/// <returns>The URL of the image displayed for Previous on the <see cref="T:System.Web.UI.WebControls.Wizard" />.</returns>
		// Token: 0x1700100E RID: 4110
		// (get) Token: 0x0600329F RID: 12959 RVA: 0x00086FF0 File Offset: 0x000851F0
		// (set) Token: 0x060032A0 RID: 12960 RVA: 0x0008701D File Offset: 0x0008521D
		[UrlProperty]
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public virtual string StepPreviousButtonImageUrl
		{
			get
			{
				object obj = this.ViewState["StepPreviousButtonImageUrl"];
				if (obj == null)
				{
					return string.Empty;
				}
				return (string)obj;
			}
			set
			{
				this.ViewState["StepPreviousButtonImageUrl"] = value;
			}
		}

		/// <summary>Gets a reference to a <see cref="T:System.Web.UI.WebControls.Style" /> object that defines the settings for the Previous button.</summary>
		/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.Style" /> that defines the style settings for Previous on a <see cref="F:System.Web.UI.WebControls.WizardStepType.Step" /> for the <see cref="T:System.Web.UI.WebControls.Wizard" />.</returns>
		// Token: 0x1700100F RID: 4111
		// (get) Token: 0x060032A1 RID: 12961 RVA: 0x00087030 File Offset: 0x00085230
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public Style StepPreviousButtonStyle
		{
			get
			{
				if (this.stepPreviousButtonStyle == null)
				{
					this.stepPreviousButtonStyle = new Style();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this.stepPreviousButtonStyle).TrackViewState();
					}
				}
				return this.stepPreviousButtonStyle;
			}
		}

		/// <summary>Gets or sets the text caption that is displayed for the Previous button.</summary>
		/// <returns>The text caption displayed for Previous on the <see cref="T:System.Web.UI.WebControls.Wizard" />. The default is "Previous". The default text for the control is localized based on the current locale for the server.</returns>
		// Token: 0x17001010 RID: 4112
		// (get) Token: 0x060032A2 RID: 12962 RVA: 0x00087060 File Offset: 0x00085260
		// (set) Token: 0x060032A3 RID: 12963 RVA: 0x0008708D File Offset: 0x0008528D
		[Localizable(true)]
		public virtual string StepPreviousButtonText
		{
			get
			{
				object obj = this.ViewState["StepPreviousButtonText"];
				if (obj == null)
				{
					return "Previous";
				}
				return (string)obj;
			}
			set
			{
				this.ViewState["StepPreviousButtonText"] = value;
			}
		}

		/// <summary>Gets or sets the type of button that is rendered as the Previous button.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.ButtonType" /> values. The default is <see cref="F:System.Web.UI.WebControls.ButtonType.Button" />.</returns>
		// Token: 0x17001011 RID: 4113
		// (get) Token: 0x060032A4 RID: 12964 RVA: 0x000870A0 File Offset: 0x000852A0
		// (set) Token: 0x060032A5 RID: 12965 RVA: 0x000870C9 File Offset: 0x000852C9
		[DefaultValue(ButtonType.Button)]
		public virtual ButtonType StepPreviousButtonType
		{
			get
			{
				object obj = this.ViewState["StepPreviousButtonType"];
				if (obj == null)
				{
					return ButtonType.Button;
				}
				return (ButtonType)obj;
			}
			set
			{
				this.ViewState["StepPreviousButtonType"] = value;
			}
		}

		/// <summary>Gets a reference to a <see cref="T:System.Web.UI.WebControls.Style" /> object that defines the settings for the <see cref="T:System.Web.UI.WebControls.WizardStep" /> objects.</summary>
		/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.Style" /> that defines the style settings for the <see cref="T:System.Web.UI.WebControls.WizardStep" /> objects on the <see cref="T:System.Web.UI.WebControls.Wizard" />.</returns>
		// Token: 0x17001012 RID: 4114
		// (get) Token: 0x060032A6 RID: 12966 RVA: 0x000870E1 File Offset: 0x000852E1
		[NotifyParentProperty(true)]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public TableItemStyle StepStyle
		{
			get
			{
				if (this.stepStyle == null)
				{
					this.stepStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this.stepStyle).TrackViewState();
					}
				}
				return this.stepStyle;
			}
		}

		/// <summary>Gets a collection containing all the <see cref="T:System.Web.UI.WebControls.WizardStepBase" /> objects that are defined for the control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WizardStepCollection" /> representing all the <see cref="T:System.Web.UI.WebControls.WizardStepBase" /> objects defined for the <see cref="T:System.Web.UI.WebControls.Wizard" />.</returns>
		// Token: 0x17001013 RID: 4115
		// (get) Token: 0x060032A7 RID: 12967 RVA: 0x0008710F File Offset: 0x0008530F
		[Themeable(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Editor("System.Web.UI.Design.WebControls.WizardStepCollectionEditor,System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public virtual WizardStepCollection WizardSteps
		{
			get
			{
				if (this.steps == null)
				{
					this.steps = new WizardStepCollection(this);
				}
				return this.steps;
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that corresponds to the <see cref="T:System.Web.UI.WebControls.Wizard" /> control.</summary>
		/// <returns>The <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that corresponds to the <see cref="T:System.Web.UI.WebControls.Wizard" /> control.</returns>
		// Token: 0x17001014 RID: 4116
		// (get) Token: 0x060032A8 RID: 12968 RVA: 0x0004D090 File Offset: 0x0004B290
		protected new virtual HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Table;
			}
		}

		// Token: 0x17001015 RID: 4117
		// (get) Token: 0x060032A9 RID: 12969 RVA: 0x0008712B File Offset: 0x0008532B
		internal virtual ITemplate SideBarItemTemplate
		{
			get
			{
				return new Wizard.SideBarButtonTemplate(this);
			}
		}

		/// <summary>Returns a collection of <see cref="T:System.Web.UI.WebControls.WizardStepBase" /> objects that have been accessed.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> containing the <see cref="T:System.Web.UI.WebControls.WizardStepBase" /> objects that have been accessed.</returns>
		// Token: 0x060032AA RID: 12970 RVA: 0x00087133 File Offset: 0x00085333
		public ICollection GetHistory()
		{
			if (this.history == null)
			{
				this.history = new ArrayList();
			}
			return this.history;
		}

		/// <summary>Sets the specified <see cref="T:System.Web.UI.WebControls.WizardStepBase" />-derived object as the value for the <see cref="P:System.Web.UI.WebControls.Wizard.ActiveStep" /> property of the <see cref="T:System.Web.UI.WebControls.Wizard" /> control.</summary>
		/// <param name="wizardStep">The <see cref="T:System.Web.UI.WebControls.WizardStepBase" />-derived object to set as the <see cref="P:System.Web.UI.WebControls.Wizard.ActiveStep" />.</param>
		/// <exception cref="T:System.ArgumentNullException">The value of the <see cref="T:System.Web.UI.WebControls.WizardStepBase" />-derived object passed in is null.</exception>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Web.UI.WebControls.Wizard.ActiveStepIndex" /> of the associated <see cref="T:System.Web.UI.WebControls.WizardStepBase" />-derived object passed in is equal to -1.</exception>
		// Token: 0x060032AB RID: 12971 RVA: 0x00087150 File Offset: 0x00085350
		public void MoveTo(WizardStepBase wizardStep)
		{
			if (wizardStep == null)
			{
				throw new ArgumentNullException("wizardStep");
			}
			int num = this.WizardSteps.IndexOf(wizardStep);
			if (num == -1)
			{
				throw new ArgumentException("The provided wizard step does not belong to this wizard.");
			}
			this.ActiveStepIndex = num;
		}

		/// <summary>Returns the <see cref="T:System.Web.UI.WebControls.WizardStepType" /> value for the specified <see cref="T:System.Web.UI.WebControls.WizardStepBase" /> object.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.WizardStepType" /> values.</returns>
		/// <param name="wizardStep">The <see cref="T:System.Web.UI.WebControls.WizardStepBase" /> for which the associated <see cref="T:System.Web.UI.WebControls.WizardStepType" />  is returned.</param>
		/// <param name="index">The index of the <see cref="T:System.Web.UI.WebControls.WizardStepBase" /> for which the associated <see cref="T:System.Web.UI.WebControls.WizardStepType" />  is returned.</param>
		// Token: 0x060032AC RID: 12972 RVA: 0x00087190 File Offset: 0x00085390
		public WizardStepType GetStepType(WizardStepBase wizardStep, int index)
		{
			if (wizardStep.StepType != WizardStepType.Auto)
			{
				return wizardStep.StepType;
			}
			if (index == this.WizardSteps.Count - 1 || (this.WizardSteps.Count > 1 && this.WizardSteps[this.WizardSteps.Count - 1].StepType == WizardStepType.Complete && index == this.WizardSteps.Count - 2))
			{
				return WizardStepType.Finish;
			}
			if (index == 0)
			{
				return WizardStepType.Start;
			}
			return WizardStepType.Step;
		}

		/// <summary>Uses a Boolean value to determine whether the <see cref="P:System.Web.UI.WebControls.Wizard.ActiveStep" /> property can be set to the <see cref="T:System.Web.UI.WebControls.WizardStepBase" /> object that corresponds to the index that is passed in.</summary>
		/// <returns>false if the index passed in refers to a <see cref="T:System.Web.UI.WebControls.WizardStepBase" /> that has already been accessed and its <see cref="P:System.Web.UI.WebControls.WizardStepBase.AllowReturn" /> property is set to false; otherwise, true.</returns>
		/// <param name="index">The index of the <see cref="T:System.Web.UI.WebControls.WizardStepBase" /> object being checked.</param>
		// Token: 0x060032AD RID: 12973 RVA: 0x00087204 File Offset: 0x00085404
		protected virtual bool AllowNavigationToStep(int index)
		{
			return index < 0 || index >= this.WizardSteps.Count || this.history == null || !this.history.Contains(index) || this.WizardSteps[index].AllowReturn;
		}

		/// <summary>Raises the Init event.</summary>
		/// <param name="e">The raised event.</param>
		// Token: 0x060032AE RID: 12974 RVA: 0x00087251 File Offset: 0x00085451
		protected internal override void OnInit(EventArgs e)
		{
			this.Page.RegisterRequiresControlState(this);
			base.OnInit(e);
			if (this.ActiveStepIndex == -1)
			{
				this.ActiveStepIndex = 0;
			}
			this.EnsureChildControls();
			this.inited = true;
		}

		/// <summary>Creates control collection.</summary>
		// Token: 0x060032AF RID: 12975 RVA: 0x00087283 File Offset: 0x00085483
		protected override ControlCollection CreateControlCollection()
		{
			ControlCollection controlCollection = new ControlCollection(this);
			controlCollection.SetReadonly(true);
			return controlCollection;
		}

		/// <summary>Creates child controls.</summary>
		// Token: 0x060032B0 RID: 12976 RVA: 0x00087292 File Offset: 0x00085492
		protected internal override void CreateChildControls()
		{
			this.CreateControlHierarchy();
		}

		// Token: 0x060032B1 RID: 12977 RVA: 0x0008729A File Offset: 0x0008549A
		private InvalidOperationException MakeLayoutException(string phName, string phID, string condition = null)
		{
			return new InvalidOperationException(string.Format("A {0} placeholder must be specified on Wizard '{1}'{2}. Specify a placeholder by setting a control's ID property to \"{3}\". The placeholder control must also specify runat=\"server\"", new object[] { phName, this.ID, condition, phID }));
		}

		// Token: 0x060032B2 RID: 12978 RVA: 0x000872C8 File Offset: 0x000854C8
		private void CreateControlHierarchy_LayoutTemplate(ITemplate layoutTemplate)
		{
			WizardLayoutContainer wizardLayoutContainer = new WizardLayoutContainer();
			ControlCollection controls = this.Controls;
			controls.SetReadonly(false);
			controls.Add(wizardLayoutContainer);
			controls.SetReadonly(true);
			layoutTemplate.InstantiateIn(wizardLayoutContainer);
			WizardStepCollection wizardSteps = this.WizardSteps;
			bool flag = wizardSteps != null && wizardSteps.Count > 0;
			Control control;
			Control control2;
			if (this.DisplaySideBar)
			{
				control = wizardLayoutContainer.FindControl(Wizard.SideBarPlaceholderId);
				if (control == null)
				{
					throw this.MakeLayoutException("sidebar", Wizard.SideBarPlaceholderId, " when DisplaySideBar is set to true");
				}
				control2 = new Control();
				this.CreateSideBar(control2);
				this.ReplacePlaceHolder(wizardLayoutContainer, control, control2);
			}
			ITemplate template = this.HeaderTemplate;
			if (template != null)
			{
				control = wizardLayoutContainer.FindControl(Wizard.HeaderPlaceholderId);
				if (control == null)
				{
					throw this.MakeLayoutException("header", Wizard.HeaderPlaceholderId, " when HeaderTemplate is set");
				}
				control2 = new Control();
				template.InstantiateIn(control2);
				this.ReplacePlaceHolder(wizardLayoutContainer, control, control2);
			}
			control = wizardLayoutContainer.FindControl(Wizard.WizardStepPlaceholderId);
			if (control == null)
			{
				throw this.MakeLayoutException("step", Wizard.WizardStepPlaceholderId, null);
			}
			this.customNavigation = null;
			this.multiView = new MultiView();
			foreach (object obj in wizardSteps)
			{
				View view = (View)obj;
				if (view is TemplatedWizardStep)
				{
					this.InstantiateTemplateStep((TemplatedWizardStep)view);
				}
				this.multiView.Views.Add(view);
			}
			this.multiView.ActiveViewIndex = this.ActiveStepIndex;
			this.ReplacePlaceHolder(wizardLayoutContainer, control, this.multiView);
			control = wizardLayoutContainer.FindControl(Wizard.NavigationPlaceholderId);
			if (control == null)
			{
				throw this.MakeLayoutException("navigation", Wizard.NavigationPlaceholderId, null);
			}
			Table table = new Table();
			table.CellSpacing = 5;
			table.CellPadding = 5;
			TableRow tableRow = new TableRow();
			TableCell tableCell = new TableCell();
			tableCell.HorizontalAlign = HorizontalAlign.Right;
			control2 = new Control();
			this.CreateButtonBar(control2);
			tableRow.Cells.Add(tableCell);
			table.Rows.Add(tableRow);
			this.ReplacePlaceHolder(wizardLayoutContainer, control, control2);
			wizardLayoutContainer.Visible = flag;
		}

		// Token: 0x060032B3 RID: 12979 RVA: 0x000874F0 File Offset: 0x000856F0
		private void ReplacePlaceHolder(WebControl container, Control placeHolder, Control replacement)
		{
			ControlCollection controls = container.Controls;
			int num = controls.IndexOf(placeHolder);
			controls.Remove(placeHolder);
			controls.AddAt(num, replacement);
		}

		/// <summary>Creates the hierarchy of child controls that make up the control.</summary>
		/// <exception cref="T:System.InvalidOperationException">The sidebar template does not contain a <see cref="T:System.Web.UI.WebControls.DataList" /> control.</exception>
		// Token: 0x060032B4 RID: 12980 RVA: 0x0008751C File Offset: 0x0008571C
		protected virtual void CreateControlHierarchy()
		{
			ITemplate layoutTemplate = this.LayoutTemplate;
			if (layoutTemplate != null)
			{
				this.CreateControlHierarchy_LayoutTemplate(layoutTemplate);
				return;
			}
			this.styles.Clear();
			this.wizardTable = new ContainedTable(this);
			Table table = this.wizardTable;
			if (this.DisplaySideBar)
			{
				table = new Table();
				table.CellPadding = 0;
				table.CellSpacing = 0;
				table.Height = new Unit("100%");
				table.Width = new Unit("100%");
				TableRow tableRow = new TableRow();
				Wizard.TableCellNamingContainer tableCellNamingContainer = new Wizard.TableCellNamingContainer(this.SkipLinkText, this.ClientID);
				tableCellNamingContainer.ID = "SideBarContainer";
				tableCellNamingContainer.ControlStyle.Height = Unit.Percentage(100.0);
				this.CreateSideBar(tableCellNamingContainer);
				tableRow.Cells.Add(tableCellNamingContainer);
				TableCell tableCell = new TableCell();
				tableCell.Controls.Add(table);
				tableCell.Height = new Unit("100%");
				tableRow.Cells.Add(tableCell);
				this.wizardTable.Rows.Add(tableRow);
			}
			this.AddHeaderRow(table);
			TableRow tableRow2 = new TableRow();
			TableCell tableCell2 = new TableCell();
			this.customNavigation = null;
			this.multiView = new MultiView();
			foreach (object obj in this.WizardSteps)
			{
				View view = (View)obj;
				if (view is TemplatedWizardStep)
				{
					this.InstantiateTemplateStep((TemplatedWizardStep)view);
				}
				this.multiView.Views.Add(view);
			}
			this.multiView.ActiveViewIndex = this.ActiveStepIndex;
			this.RegisterApplyStyle(tableCell2, this.StepStyle);
			tableCell2.Controls.Add(this.multiView);
			tableRow2.Cells.Add(tableCell2);
			tableRow2.Height = new Unit("100%");
			table.Rows.Add(tableRow2);
			TableRow tableRow3 = new TableRow();
			this._navigationCell = new TableCell();
			this._navigationCell.HorizontalAlign = HorizontalAlign.Right;
			this.RegisterApplyStyle(this._navigationCell, this.NavigationStyle);
			this.CreateButtonBar(this._navigationCell);
			tableRow3.Cells.Add(this._navigationCell);
			table.Rows.Add(tableRow3);
			this.Controls.SetReadonly(false);
			this.Controls.Add(this.wizardTable);
			this.Controls.SetReadonly(true);
		}

		// Token: 0x060032B5 RID: 12981 RVA: 0x000877B0 File Offset: 0x000859B0
		internal virtual void InstantiateTemplateStep(TemplatedWizardStep step)
		{
			Wizard.BaseWizardContainer baseWizardContainer = new Wizard.BaseWizardContainer();
			if (step.ContentTemplate != null)
			{
				step.ContentTemplate.InstantiateIn(baseWizardContainer.InnerCell);
			}
			step.ContentTemplateContainer = baseWizardContainer;
			step.Controls.Clear();
			step.Controls.Add(baseWizardContainer);
			Wizard.BaseWizardNavigationContainer baseWizardNavigationContainer = new Wizard.BaseWizardNavigationContainer();
			if (step.CustomNavigationTemplate != null)
			{
				step.CustomNavigationTemplate.InstantiateIn(baseWizardNavigationContainer);
				this.RegisterCustomNavigation(step, baseWizardNavigationContainer);
			}
			step.CustomNavigationTemplateContainer = baseWizardNavigationContainer;
		}

		// Token: 0x060032B6 RID: 12982 RVA: 0x00087823 File Offset: 0x00085A23
		internal void RegisterCustomNavigation(TemplatedWizardStep step, Wizard.BaseWizardNavigationContainer customNavigationTemplateContainer)
		{
			if (this.customNavigation == null)
			{
				this.customNavigation = new Hashtable();
			}
			this.customNavigation[step] = customNavigationTemplateContainer;
		}

		// Token: 0x060032B7 RID: 12983 RVA: 0x00087848 File Offset: 0x00085A48
		private void CreateButtonBar(Control container)
		{
			if (this.customNavigation != null && this.customNavigation.Values.Count > 0)
			{
				int num = 0;
				foreach (object obj in this.customNavigation.Values)
				{
					Control control = (Control)obj;
					control.ID = "CustomNavigationTemplateContainerID" + num++;
					container.Controls.Add(control);
				}
			}
			this._startNavContainer = new Wizard.StartNavigationContainer(this);
			this._startNavContainer.ID = "StartNavigationTemplateContainerID";
			if (this.startNavigationTemplate != null)
			{
				this.startNavigationTemplate.InstantiateIn(this._startNavContainer);
			}
			else
			{
				TableRow tableRow;
				Wizard.AddNavButtonsTable(this._startNavContainer, out tableRow);
				this.AddButtonCell(tableRow, this.CreateButtonSet(Wizard.StartNextButtonIDShort, Wizard.MoveNextCommandName));
				this.AddButtonCell(tableRow, this.CreateButtonSet(Wizard.CancelButtonIDShort, Wizard.CancelCommandName, false));
				this._startNavContainer.ConfirmDefaultTemplate();
			}
			container.Controls.Add(this._startNavContainer);
			this._stepNavContainer = new Wizard.StepNavigationContainer(this);
			this._stepNavContainer.ID = "StepNavigationTemplateContainerID";
			if (this.stepNavigationTemplate != null)
			{
				this.stepNavigationTemplate.InstantiateIn(this._stepNavContainer);
			}
			else
			{
				TableRow tableRow2;
				Wizard.AddNavButtonsTable(this._stepNavContainer, out tableRow2);
				this.AddButtonCell(tableRow2, this.CreateButtonSet(Wizard.StepPreviousButtonIDShort, Wizard.MovePreviousCommandName, false));
				this.AddButtonCell(tableRow2, this.CreateButtonSet(Wizard.StepNextButtonIDShort, Wizard.MoveNextCommandName));
				this.AddButtonCell(tableRow2, this.CreateButtonSet(Wizard.CancelButtonIDShort, Wizard.CancelCommandName, false));
				this._stepNavContainer.ConfirmDefaultTemplate();
			}
			container.Controls.Add(this._stepNavContainer);
			this._finishNavContainer = new Wizard.FinishNavigationContainer(this);
			this._finishNavContainer.ID = "FinishNavigationTemplateContainerID";
			if (this.finishNavigationTemplate != null)
			{
				this.finishNavigationTemplate.InstantiateIn(this._finishNavContainer);
			}
			else
			{
				TableRow tableRow3;
				Wizard.AddNavButtonsTable(this._finishNavContainer, out tableRow3);
				this.AddButtonCell(tableRow3, this.CreateButtonSet(Wizard.FinishPreviousButtonIDShort, Wizard.MovePreviousCommandName, false));
				this.AddButtonCell(tableRow3, this.CreateButtonSet(Wizard.FinishButtonIDShort, Wizard.MoveCompleteCommandName));
				this.AddButtonCell(tableRow3, this.CreateButtonSet(Wizard.CancelButtonIDShort, Wizard.CancelCommandName, false));
				this._finishNavContainer.ConfirmDefaultTemplate();
			}
			container.Controls.Add(this._finishNavContainer);
		}

		// Token: 0x060032B8 RID: 12984 RVA: 0x00087AC8 File Offset: 0x00085CC8
		private static void AddNavButtonsTable(Wizard.BaseWizardNavigationContainer container, out TableRow row)
		{
			Table table = new Table();
			table.CellPadding = 5;
			table.CellSpacing = 5;
			row = new TableRow();
			table.Rows.Add(row);
			container.Controls.Add(table);
		}

		// Token: 0x060032B9 RID: 12985 RVA: 0x00087B0A File Offset: 0x00085D0A
		private Control[] CreateButtonSet(string id, string command)
		{
			return this.CreateButtonSet(id, command, true, null);
		}

		// Token: 0x060032BA RID: 12986 RVA: 0x00087B16 File Offset: 0x00085D16
		private Control[] CreateButtonSet(string id, string command, bool causesValidation)
		{
			return this.CreateButtonSet(id, command, causesValidation, null);
		}

		// Token: 0x060032BB RID: 12987 RVA: 0x00087B24 File Offset: 0x00085D24
		internal Control[] CreateButtonSet(string id, string command, bool causesValidation, string validationGroup)
		{
			return new Control[]
			{
				this.CreateButton(id + ButtonType.Button, command, ButtonType.Button, causesValidation, validationGroup),
				this.CreateButton(id + ButtonType.Image, command, ButtonType.Image, causesValidation, validationGroup),
				this.CreateButton(id + ButtonType.Link, command, ButtonType.Link, causesValidation, validationGroup)
			};
		}

		// Token: 0x060032BC RID: 12988 RVA: 0x00087B88 File Offset: 0x00085D88
		private Control CreateButton(string id, string command, ButtonType type, bool causesValidation, string validationGroup)
		{
			WebControl webControl;
			switch (type)
			{
			case ButtonType.Button:
				webControl = this.CreateStandardButton();
				break;
			case ButtonType.Image:
				webControl = this.CreateImageButton(null);
				break;
			case ButtonType.Link:
				webControl = this.CreateLinkButton();
				break;
			default:
				throw new ArgumentOutOfRangeException("type");
			}
			webControl.ID = id;
			webControl.EnableTheming = false;
			((IButtonControl)webControl).CommandName = command;
			((IButtonControl)webControl).CausesValidation = causesValidation;
			if (!string.IsNullOrEmpty(validationGroup))
			{
				((IButtonControl)webControl).ValidationGroup = validationGroup;
			}
			this.RegisterApplyStyle(webControl, this.NavigationButtonStyle);
			return webControl;
		}

		// Token: 0x060032BD RID: 12989 RVA: 0x00087C1B File Offset: 0x00085E1B
		private WebControl CreateStandardButton()
		{
			return new Button();
		}

		// Token: 0x060032BE RID: 12990 RVA: 0x00087C22 File Offset: 0x00085E22
		private WebControl CreateImageButton(string imageUrl)
		{
			return new ImageButton
			{
				ImageUrl = imageUrl
			};
		}

		// Token: 0x060032BF RID: 12991 RVA: 0x00087C30 File Offset: 0x00085E30
		private WebControl CreateLinkButton()
		{
			return new LinkButton();
		}

		// Token: 0x060032C0 RID: 12992 RVA: 0x00087C38 File Offset: 0x00085E38
		private void AddButtonCell(TableRow row, params Control[] controls)
		{
			TableCell tableCell = new TableCell();
			tableCell.HorizontalAlign = HorizontalAlign.Right;
			for (int i = 0; i < controls.Length; i++)
			{
				tableCell.Controls.Add(controls[i]);
			}
			row.Cells.Add(tableCell);
		}

		// Token: 0x060032C1 RID: 12993 RVA: 0x00087C7C File Offset: 0x00085E7C
		private void CreateSideBar(Control container)
		{
			WebControl webControl = container as WebControl;
			if (webControl != null)
			{
				this.RegisterApplyStyle(webControl, this.SideBarStyle);
			}
			if (this.sideBarTemplate != null)
			{
				this.sideBarTemplate.InstantiateIn(container);
				this.stepDatalist = container.FindControl(Wizard.DataListID) as DataList;
				if (this.stepDatalist == null)
				{
					throw new InvalidOperationException("The side bar template must contain a DataList control with id '" + Wizard.DataListID + "'.");
				}
				this.stepDatalist.ItemDataBound += this.StepDatalistItemDataBound;
			}
			else
			{
				this.stepDatalist = new DataList();
				this.stepDatalist.ID = Wizard.DataListID;
				this.stepDatalist.SelectedItemStyle.Font.Bold = true;
				this.stepDatalist.ItemTemplate = this.SideBarItemTemplate;
				container.Controls.Add(this.stepDatalist);
			}
			this.stepDatalist.ItemCommand += this.StepDatalistItemCommand;
			this.stepDatalist.CellSpacing = 0;
			this.stepDatalist.DataSource = this.WizardSteps;
			this.stepDatalist.SelectedIndex = this.ActiveStepIndex;
			this.stepDatalist.DataBind();
		}

		// Token: 0x060032C2 RID: 12994 RVA: 0x00087DA8 File Offset: 0x00085FA8
		private void StepDatalistItemCommand(object sender, DataListCommandEventArgs e)
		{
			WizardNavigationEventArgs wizardNavigationEventArgs = new WizardNavigationEventArgs(this.ActiveStepIndex, Convert.ToInt32(e.CommandArgument));
			this.OnSideBarButtonClick(wizardNavigationEventArgs);
			if (!wizardNavigationEventArgs.Cancel)
			{
				this.ActiveStepIndex = wizardNavigationEventArgs.NextStepIndex;
			}
		}

		// Token: 0x060032C3 RID: 12995 RVA: 0x00087DE8 File Offset: 0x00085FE8
		private void StepDatalistItemDataBound(object sender, DataListItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.SelectedItem)
			{
				IButtonControl buttonControl = (IButtonControl)e.Item.FindControl(Wizard.SideBarButtonID);
				if (buttonControl == null)
				{
					throw new InvalidOperationException("SideBarList control must contain an IButtonControl with ID " + Wizard.SideBarButtonID + " in every item template, this maybe include ItemTemplate, EditItemTemplate, SelectedItemTemplate or AlternatingItemTemplate if they exist.");
				}
				WizardStepBase wizardStepBase = (WizardStepBase)e.Item.DataItem;
				if (buttonControl is Button)
				{
					((Button)buttonControl).UseSubmitBehavior = false;
				}
				buttonControl.CommandName = Wizard.MoveToCommandName;
				buttonControl.CommandArgument = this.WizardSteps.IndexOf(wizardStepBase).ToString();
				buttonControl.Text = wizardStepBase.Name;
				if (wizardStepBase.StepType == WizardStepType.Complete && buttonControl is WebControl)
				{
					((WebControl)buttonControl).Enabled = false;
				}
			}
		}

		// Token: 0x060032C4 RID: 12996 RVA: 0x00087EC8 File Offset: 0x000860C8
		private void AddHeaderRow(Table table)
		{
			TableRow tableRow = new TableRow();
			this._headerCell = new Wizard.WizardHeaderCell();
			this._headerCell.ID = "HeaderContainer";
			this.RegisterApplyStyle(this._headerCell, this.HeaderStyle);
			if (this.headerTemplate != null)
			{
				this.headerTemplate.InstantiateIn(this._headerCell);
				this._headerCell.ConfirmInitState();
			}
			tableRow.Cells.Add(this._headerCell);
			table.Rows.Add(tableRow);
		}

		// Token: 0x060032C5 RID: 12997 RVA: 0x00087F4B File Offset: 0x0008614B
		internal void RegisterApplyStyle(WebControl control, Style style)
		{
			this.styles.Add(new object[] { control, style });
		}

		/// <summary>Creates control style.</summary>
		// Token: 0x060032C6 RID: 12998 RVA: 0x00087F67 File Offset: 0x00086167
		protected override Style CreateControlStyle()
		{
			return new TableStyle
			{
				CellPadding = 0,
				CellSpacing = 0
			};
		}

		/// <summary>Gets the design mode state.</summary>
		// Token: 0x060032C7 RID: 12999 RVA: 0x00003A1F File Offset: 0x00001C1F
		[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
		protected override IDictionary GetDesignModeState()
		{
			throw new NotImplementedException();
		}

		/// <summary>Restores control state information.</summary>
		/// <param name="state">The control state to be restored.</param>
		// Token: 0x060032C8 RID: 13000 RVA: 0x00087F7C File Offset: 0x0008617C
		protected internal override void LoadControlState(object state)
		{
			if (state == null)
			{
				return;
			}
			object[] array = (object[])state;
			base.LoadControlState(array[0]);
			this.activeStepIndex = (int)array[1];
			this.history = (ArrayList)array[2];
		}

		/// <summary>Save the control state.</summary>
		/// <returns>The control state.</returns>
		// Token: 0x060032C9 RID: 13001 RVA: 0x00087FBC File Offset: 0x000861BC
		protected internal override object SaveControlState()
		{
			if (this.GetHistory().Count == 0 || (int)this.history[0] != this.ActiveStepIndex)
			{
				this.history.Insert(0, this.ActiveStepIndex);
			}
			object obj = base.SaveControlState();
			return new object[] { obj, this.activeStepIndex, this.history };
		}

		/// <summary>Loads view-state information.</summary>
		/// <param name="savedState">The control state to be restored.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="savedState" /> is not a valid <see cref="P:System.Web.UI.PageStatePersister.ViewState" /> value.</exception>
		// Token: 0x060032CA RID: 13002 RVA: 0x00088030 File Offset: 0x00086230
		protected override void LoadViewState(object savedState)
		{
			if (savedState == null)
			{
				base.LoadViewState(null);
				return;
			}
			object[] array = (object[])savedState;
			base.LoadViewState(array[0]);
			if (array[1] != null)
			{
				((IStateManager)this.StepStyle).LoadViewState(array[1]);
			}
			if (array[2] != null)
			{
				((IStateManager)this.SideBarStyle).LoadViewState(array[2]);
			}
			if (array[3] != null)
			{
				((IStateManager)this.HeaderStyle).LoadViewState(array[3]);
			}
			if (array[4] != null)
			{
				((IStateManager)this.NavigationStyle).LoadViewState(array[4]);
			}
			if (array[5] != null)
			{
				((IStateManager)this.SideBarButtonStyle).LoadViewState(array[5]);
			}
			if (array[6] != null)
			{
				((IStateManager)this.CancelButtonStyle).LoadViewState(array[6]);
			}
			if (array[7] != null)
			{
				((IStateManager)this.FinishCompleteButtonStyle).LoadViewState(array[7]);
			}
			if (array[8] != null)
			{
				((IStateManager)this.FinishPreviousButtonStyle).LoadViewState(array[8]);
			}
			if (array[9] != null)
			{
				((IStateManager)this.StartNextButtonStyle).LoadViewState(array[9]);
			}
			if (array[10] != null)
			{
				((IStateManager)this.StepNextButtonStyle).LoadViewState(array[10]);
			}
			if (array[11] != null)
			{
				((IStateManager)this.StepPreviousButtonStyle).LoadViewState(array[11]);
			}
			if (array[12] != null)
			{
				((IStateManager)this.NavigationButtonStyle).LoadViewState(array[12]);
			}
			if (array[13] != null)
			{
				base.ControlStyle.LoadViewState(array[13]);
			}
		}

		/// <summary>Saves the view state.</summary>
		/// <returns>The view state.</returns>
		// Token: 0x060032CB RID: 13003 RVA: 0x0008815C File Offset: 0x0008635C
		protected override object SaveViewState()
		{
			object[] array = new object[14];
			array[0] = base.SaveViewState();
			if (this.stepStyle != null)
			{
				array[1] = ((IStateManager)this.stepStyle).SaveViewState();
			}
			if (this.sideBarStyle != null)
			{
				array[2] = ((IStateManager)this.sideBarStyle).SaveViewState();
			}
			if (this.headerStyle != null)
			{
				array[3] = ((IStateManager)this.headerStyle).SaveViewState();
			}
			if (this.navigationStyle != null)
			{
				array[4] = ((IStateManager)this.navigationStyle).SaveViewState();
			}
			if (this.sideBarButtonStyle != null)
			{
				array[5] = ((IStateManager)this.sideBarButtonStyle).SaveViewState();
			}
			if (this.cancelButtonStyle != null)
			{
				array[6] = ((IStateManager)this.cancelButtonStyle).SaveViewState();
			}
			if (this.finishCompleteButtonStyle != null)
			{
				array[7] = ((IStateManager)this.finishCompleteButtonStyle).SaveViewState();
			}
			if (this.finishPreviousButtonStyle != null)
			{
				array[8] = ((IStateManager)this.finishPreviousButtonStyle).SaveViewState();
			}
			if (this.startNextButtonStyle != null)
			{
				array[9] = ((IStateManager)this.startNextButtonStyle).SaveViewState();
			}
			if (this.stepNextButtonStyle != null)
			{
				array[10] = ((IStateManager)this.stepNextButtonStyle).SaveViewState();
			}
			if (this.stepPreviousButtonStyle != null)
			{
				array[11] = ((IStateManager)this.stepPreviousButtonStyle).SaveViewState();
			}
			if (this.navigationButtonStyle != null)
			{
				array[12] = ((IStateManager)this.navigationButtonStyle).SaveViewState();
			}
			if (base.ControlStyleCreated)
			{
				array[13] = base.ControlStyle.SaveViewState();
			}
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] != null)
				{
					return array;
				}
			}
			return null;
		}

		/// <summary>Tracks view state.</summary>
		// Token: 0x060032CC RID: 13004 RVA: 0x000882B4 File Offset: 0x000864B4
		protected override void TrackViewState()
		{
			base.TrackViewState();
			if (this.stepStyle != null)
			{
				((IStateManager)this.stepStyle).TrackViewState();
			}
			if (this.sideBarStyle != null)
			{
				((IStateManager)this.sideBarStyle).TrackViewState();
			}
			if (this.headerStyle != null)
			{
				((IStateManager)this.headerStyle).TrackViewState();
			}
			if (this.navigationStyle != null)
			{
				((IStateManager)this.navigationStyle).TrackViewState();
			}
			if (this.sideBarButtonStyle != null)
			{
				((IStateManager)this.sideBarButtonStyle).TrackViewState();
			}
			if (this.cancelButtonStyle != null)
			{
				((IStateManager)this.cancelButtonStyle).TrackViewState();
			}
			if (this.finishCompleteButtonStyle != null)
			{
				((IStateManager)this.finishCompleteButtonStyle).TrackViewState();
			}
			if (this.finishPreviousButtonStyle != null)
			{
				((IStateManager)this.finishPreviousButtonStyle).TrackViewState();
			}
			if (this.startNextButtonStyle != null)
			{
				((IStateManager)this.startNextButtonStyle).TrackViewState();
			}
			if (this.stepNextButtonStyle != null)
			{
				((IStateManager)this.stepNextButtonStyle).TrackViewState();
			}
			if (this.stepPreviousButtonStyle != null)
			{
				((IStateManager)this.stepPreviousButtonStyle).TrackViewState();
			}
			if (this.navigationButtonStyle != null)
			{
				((IStateManager)this.navigationButtonStyle).TrackViewState();
			}
			if (base.ControlStyleCreated)
			{
				base.ControlStyle.TrackViewState();
			}
		}

		/// <summary>Registers a new instance of the <see cref="T:System.Web.UI.WebControls.CommandEventHandler" /> class for the specified <see cref="T:System.Web.UI.WebControls.IButtonControl" /> object.</summary>
		/// <param name="button">The <see cref="T:System.Web.UI.WebControls.IButtonControl" /> for which the new instance of <see cref="T:System.Web.UI.WebControls.CommandEventHandler" /> is registered.</param>
		// Token: 0x060032CD RID: 13005 RVA: 0x000883BE File Offset: 0x000865BE
		protected internal void RegisterCommandEvents(IButtonControl button)
		{
			button.Command += this.ProcessCommand;
		}

		// Token: 0x060032CE RID: 13006 RVA: 0x000883D4 File Offset: 0x000865D4
		private void ProcessCommand(object sender, CommandEventArgs args)
		{
			Control control = sender as Control;
			if (control != null)
			{
				string id = control.ID;
				if (id == "CancelButton")
				{
					this.ProcessEvent("Cancel", null);
					return;
				}
				if (id == "FinishButton")
				{
					this.ProcessEvent("MoveComplete", null);
					return;
				}
				if (id == "StepPreviousButton" || id == "FinishPreviousButton")
				{
					this.ProcessEvent("MovePrevious", null);
					return;
				}
				if (id == "StartNextButton" || id == "StepNextButton")
				{
					this.ProcessEvent("MoveNext", null);
					return;
				}
			}
			this.ProcessEvent(args.CommandName, args.CommandArgument as string);
		}

		/// <summary>Determines whether the event for the server control is passed up the page’s user interface server control hierarchy.</summary>
		/// <returns>true if the event for the server control is passed up the page’s user interface server control hierarchy; otherwise, false.</returns>
		/// <param name="source">The source of the event.</param>
		/// <param name="e">Contains event data.</param>
		// Token: 0x060032CF RID: 13007 RVA: 0x00088490 File Offset: 0x00086690
		protected override bool OnBubbleEvent(object source, EventArgs e)
		{
			CommandEventArgs commandEventArgs = e as CommandEventArgs;
			if (commandEventArgs != null)
			{
				this.ProcessEvent(commandEventArgs.CommandName, commandEventArgs.CommandArgument as string);
				return true;
			}
			return base.OnBubbleEvent(source, e);
		}

		// Token: 0x060032D0 RID: 13008 RVA: 0x000884C8 File Offset: 0x000866C8
		private void ProcessEvent(string commandName, string commandArg)
		{
			if (!(commandName == "Cancel"))
			{
				if (!(commandName == "MoveComplete"))
				{
					if (!(commandName == "MoveNext"))
					{
						if (!(commandName == "MovePrevious"))
						{
							if (!(commandName == "Move"))
							{
								return;
							}
							int num = int.Parse(commandArg);
							this.ActiveStepIndex = num;
						}
						else if (this.ActiveStepIndex > 0)
						{
							WizardNavigationEventArgs wizardNavigationEventArgs = new WizardNavigationEventArgs(this.ActiveStepIndex, this.ActiveStepIndex - 1);
							int num2 = this.ActiveStepIndex;
							this.OnPreviousButtonClick(wizardNavigationEventArgs);
							if (!wizardNavigationEventArgs.Cancel)
							{
								if (num2 == this.activeStepIndex)
								{
									int num3 = this.ActiveStepIndex;
									this.ActiveStepIndex = num3 - 1;
								}
								if (this.history != null && this.activeStepIndex < num2)
								{
									this.history.Remove(num2);
									return;
								}
							}
						}
					}
					else if (this.ActiveStepIndex < this.WizardSteps.Count - 1)
					{
						WizardNavigationEventArgs wizardNavigationEventArgs2 = new WizardNavigationEventArgs(this.ActiveStepIndex, this.ActiveStepIndex + 1);
						int num4 = this.ActiveStepIndex;
						this.OnNextButtonClick(wizardNavigationEventArgs2);
						if (!wizardNavigationEventArgs2.Cancel && num4 == this.activeStepIndex)
						{
							int num3 = this.ActiveStepIndex;
							this.ActiveStepIndex = num3 + 1;
							return;
						}
					}
				}
				else
				{
					int num5 = -1;
					for (int i = 0; i < this.WizardSteps.Count; i++)
					{
						if (this.WizardSteps[i].StepType == WizardStepType.Complete)
						{
							num5 = i;
							break;
						}
					}
					if (num5 == -1 && this.ActiveStepIndex == this.WizardSteps.Count - 1)
					{
						num5 = this.ActiveStepIndex;
					}
					WizardNavigationEventArgs wizardNavigationEventArgs3 = new WizardNavigationEventArgs(this.ActiveStepIndex, num5);
					this.OnFinishButtonClick(wizardNavigationEventArgs3);
					if (this.FinishDestinationPageUrl.Length > 0)
					{
						this.Context.Response.Redirect(this.FinishDestinationPageUrl);
						return;
					}
					if (num5 != -1 && !wizardNavigationEventArgs3.Cancel)
					{
						this.ActiveStepIndex = num5;
						return;
					}
				}
				return;
			}
			if (this.CancelDestinationPageUrl.Length > 0)
			{
				this.Context.Response.Redirect(this.CancelDestinationPageUrl);
				return;
			}
			this.OnCancelButtonClick(EventArgs.Empty);
		}

		// Token: 0x060032D1 RID: 13009 RVA: 0x000798F1 File Offset: 0x00077AF1
		internal void UpdateViews()
		{
			base.ChildControlsCreated = false;
		}

		/// <summary>Renders the control to the specified writer.</summary>
		/// <param name="writer">The HTML writer.</param>
		// Token: 0x060032D2 RID: 13010 RVA: 0x000886E3 File Offset: 0x000868E3
		protected internal override void Render(HtmlTextWriter writer)
		{
			this.PrepareControlHierarchy();
			if (this.LayoutTemplate == null)
			{
				this.wizardTable.Render(writer);
				return;
			}
			this.RenderChildren(writer);
		}

		// Token: 0x060032D3 RID: 13011 RVA: 0x00088708 File Offset: 0x00086908
		private void PrepareControlHierarchy()
		{
			if (this.LayoutTemplate == null)
			{
				if (!this._headerCell.Initialized)
				{
					if (string.IsNullOrEmpty(this.HeaderText))
					{
						this._headerCell.Parent.Visible = false;
					}
					else
					{
						this._headerCell.Text = this.HeaderText;
					}
				}
				if (this.ActiveStep.StepType == WizardStepType.Complete)
				{
					this._headerCell.Parent.Visible = false;
				}
			}
			else
			{
				WizardStepCollection wizardSteps = this.WizardSteps;
				if (wizardSteps == null || wizardSteps.Count == 0)
				{
					return;
				}
			}
			if (this.stepDatalist != null)
			{
				this.stepDatalist.SelectedIndex = this.ActiveStepIndex;
				this.stepDatalist.DataBind();
				if (this.ActiveStep.StepType == WizardStepType.Complete)
				{
					this.stepDatalist.NamingContainer.Visible = false;
				}
			}
			TemplatedWizardStep templatedWizardStep = this.ActiveStep as TemplatedWizardStep;
			if (templatedWizardStep != null)
			{
				Wizard.BaseWizardContainer baseWizardContainer = templatedWizardStep.ContentTemplateContainer as Wizard.BaseWizardContainer;
				if (baseWizardContainer != null)
				{
					baseWizardContainer.PrepareControlHierarchy();
				}
			}
			if (this.customNavigation != null)
			{
				foreach (object obj in this.customNavigation.Values)
				{
					((Control)obj).Visible = false;
				}
			}
			this._startNavContainer.Visible = false;
			this._stepNavContainer.Visible = false;
			this._finishNavContainer.Visible = false;
			Wizard.BaseWizardNavigationContainer currentNavContainer = this.GetCurrentNavContainer();
			if (currentNavContainer == null)
			{
				if (this._navigationCell != null)
				{
					this._navigationCell.Parent.Visible = false;
				}
			}
			else
			{
				currentNavContainer.Visible = true;
				currentNavContainer.PrepareControlHierarchy();
				if (this._navigationCell != null && !currentNavContainer.Visible)
				{
					this._navigationCell.Parent.Visible = false;
				}
			}
			foreach (object obj2 in this.styles)
			{
				object[] array = (object[])obj2;
				((WebControl)array[0]).ApplyStyle((Style)array[1]);
			}
		}

		// Token: 0x060032D4 RID: 13012 RVA: 0x0008892C File Offset: 0x00086B2C
		private Wizard.BaseWizardNavigationContainer GetCurrentNavContainer()
		{
			if (this.customNavigation != null && this.customNavigation[this.ActiveStep] != null)
			{
				return (Wizard.BaseWizardNavigationContainer)this.customNavigation[this.ActiveStep];
			}
			switch (this.GetStepType(this.ActiveStep, this.ActiveStepIndex))
			{
			case WizardStepType.Finish:
				return this._finishNavContainer;
			case WizardStepType.Start:
				return this._startNavContainer;
			case WizardStepType.Step:
				return this._stepNavContainer;
			default:
				return null;
			}
		}

		// Token: 0x060032D6 RID: 13014 RVA: 0x000889C4 File Offset: 0x00086BC4
		// Note: this type is marked as 'beforefieldinit'.
		static Wizard()
		{
			Wizard.ActiveStepChangedEvent = new object();
			Wizard.CancelButtonClickEvent = new object();
			Wizard.FinishButtonClickEvent = new object();
			Wizard.NextButtonClickEvent = new object();
			Wizard.PreviousButtonClickEvent = new object();
			Wizard.SideBarButtonClickEvent = new object();
		}

		/// <summary>Retrieves the command name for the Cancel button. This field is static and read-only.</summary>
		// Token: 0x04001C6F RID: 7279
		public static readonly string CancelCommandName = "Cancel";

		/// <summary>Retrieves the command name that is associated with the Finish button. This field is static and read-only.</summary>
		// Token: 0x04001C70 RID: 7280
		public static readonly string MoveCompleteCommandName = "MoveComplete";

		/// <summary>Retrieves the command name that is associated with the Next button. This field is static and read-only.</summary>
		// Token: 0x04001C71 RID: 7281
		public static readonly string MoveNextCommandName = "MoveNext";

		/// <summary>Retrieves the command name that is associated with the Previous button. This field is static and read-only. </summary>
		// Token: 0x04001C72 RID: 7282
		public static readonly string MovePreviousCommandName = "MovePrevious";

		/// <summary>Retrieves the command name that is associated with each of the sidebar buttons. This field is static and read-only. </summary>
		// Token: 0x04001C73 RID: 7283
		public static readonly string MoveToCommandName = "Move";

		/// <summary>Gets the ID of the <see cref="P:System.Web.UI.WebControls.Wizard.HeaderTemplate" /> placeholder in a <see cref="T:System.Web.UI.WebControls.Wizard" /> control.</summary>
		// Token: 0x04001C74 RID: 7284
		public static readonly string HeaderPlaceholderId = "headerPlaceholder";

		/// <summary>Gets the ID of the <see cref="P:System.Web.UI.WebControls.Wizard.StartNavigationTemplate" /> placeholder in a <see cref="T:System.Web.UI.WebControls.Wizard" /> control.</summary>
		// Token: 0x04001C75 RID: 7285
		public static readonly string NavigationPlaceholderId = "navigationPlaceholder";

		/// <summary>Gets the ID of the <see cref="P:System.Web.UI.WebControls.Wizard.SideBarTemplate" /> placeholder in a <see cref="T:System.Web.UI.WebControls.Wizard" /> control.</summary>
		// Token: 0x04001C76 RID: 7286
		public static readonly string SideBarPlaceholderId = "sideBarPlaceholder";

		/// <summary>Gets the ID of the <see cref="T:System.Web.UI.WebControls.WizardStep" /> placeholder in a <see cref="T:System.Web.UI.WebControls.Wizard" /> control.</summary>
		// Token: 0x04001C77 RID: 7287
		public static readonly string WizardStepPlaceholderId = "wizardStepPlaceholder";

		/// <summary>Retrieves the identifier for the sidebar <see cref="T:System.Web.UI.WebControls.DataList" /> collection. This field is static and read-only.</summary>
		// Token: 0x04001C78 RID: 7288
		protected static readonly string DataListID = "SideBarList";

		// Token: 0x04001C79 RID: 7289
		private static readonly string CancelButtonIDShort = "Cancel";

		/// <summary>Specifies the identifier for the Cancel button. This field is static and read-only.</summary>
		// Token: 0x04001C7A RID: 7290
		protected static readonly string CancelButtonID = Wizard.CancelButtonIDShort + "Button";

		// Token: 0x04001C7B RID: 7291
		private static readonly string CustomFinishButtonIDShort = "CustomFinish";

		/// <summary>Retrieves the identifier for a custom Finish button. This field is static and read-only.</summary>
		// Token: 0x04001C7C RID: 7292
		protected static readonly string CustomFinishButtonID = Wizard.CustomFinishButtonIDShort + "Button";

		// Token: 0x04001C7D RID: 7293
		private static readonly string CustomNextButtonIDShort = "CustomNext";

		/// <summary>Retrieves the identifier for a custom Next button. This field is static and read-only.</summary>
		// Token: 0x04001C7E RID: 7294
		protected static readonly string CustomNextButtonID = Wizard.CustomNextButtonIDShort + "Button";

		// Token: 0x04001C7F RID: 7295
		private static readonly string CustomPreviousButtonIDShort = "CustomPrevious";

		/// <summary>Retrieves the identifier for a custom Previous button. This field is static and read-only.</summary>
		// Token: 0x04001C80 RID: 7296
		protected static readonly string CustomPreviousButtonID = Wizard.CustomPreviousButtonIDShort + "Button";

		// Token: 0x04001C81 RID: 7297
		private static readonly string FinishButtonIDShort = "Finish";

		/// <summary>Retrieves the identifier for the Finish button. This field is static and read-only.</summary>
		// Token: 0x04001C82 RID: 7298
		protected static readonly string FinishButtonID = Wizard.FinishButtonIDShort + "Button";

		// Token: 0x04001C83 RID: 7299
		private static readonly string FinishPreviousButtonIDShort = "FinishPrevious";

		/// <summary>Retrieves the identifier for the Previous button on the <see cref="F:System.Web.UI.WebControls.WizardStepType.Finish" /> step. This field is static and read-only.</summary>
		// Token: 0x04001C84 RID: 7300
		protected static readonly string FinishPreviousButtonID = Wizard.FinishPreviousButtonIDShort + "Button";

		// Token: 0x04001C85 RID: 7301
		private static readonly string SideBarButtonIDShort = "SideBar";

		/// <summary>Retrieves the identifier that is associated with each of the sidebar buttons. This field is static and read-only. </summary>
		// Token: 0x04001C86 RID: 7302
		protected static readonly string SideBarButtonID = Wizard.SideBarButtonIDShort + "Button";

		// Token: 0x04001C87 RID: 7303
		private static readonly string StartNextButtonIDShort = "StartNext";

		/// <summary>Retrieves the identifier that is associated with the Next button on the <see cref="F:System.Web.UI.WebControls.WizardStepType.Start" /> step. This field is static and read-only. </summary>
		// Token: 0x04001C88 RID: 7304
		protected static readonly string StartNextButtonID = Wizard.StartNextButtonIDShort + "Button";

		// Token: 0x04001C89 RID: 7305
		private static readonly string StepNextButtonIDShort = "StepNext";

		/// <summary>Retrieves the identifier that is associated with the Next button. This field is static and read-only. </summary>
		// Token: 0x04001C8A RID: 7306
		protected static readonly string StepNextButtonID = Wizard.StepNextButtonIDShort + "Button";

		// Token: 0x04001C8B RID: 7307
		private static readonly string StepPreviousButtonIDShort = "StepPrevious";

		/// <summary>Retrieves the identifier that is associated with the Previous button. This field is static and read-only. </summary>
		// Token: 0x04001C8C RID: 7308
		protected static readonly string StepPreviousButtonID = Wizard.StepPreviousButtonIDShort + "Button";

		// Token: 0x04001C8D RID: 7309
		private WizardStepCollection steps;

		// Token: 0x04001C8E RID: 7310
		private TableItemStyle stepStyle;

		// Token: 0x04001C8F RID: 7311
		private TableItemStyle sideBarStyle;

		// Token: 0x04001C90 RID: 7312
		private TableItemStyle headerStyle;

		// Token: 0x04001C91 RID: 7313
		private TableItemStyle navigationStyle;

		// Token: 0x04001C92 RID: 7314
		private Style sideBarButtonStyle;

		// Token: 0x04001C93 RID: 7315
		private Style cancelButtonStyle;

		// Token: 0x04001C94 RID: 7316
		private Style finishCompleteButtonStyle;

		// Token: 0x04001C95 RID: 7317
		private Style finishPreviousButtonStyle;

		// Token: 0x04001C96 RID: 7318
		private Style startNextButtonStyle;

		// Token: 0x04001C97 RID: 7319
		private Style stepNextButtonStyle;

		// Token: 0x04001C98 RID: 7320
		private Style stepPreviousButtonStyle;

		// Token: 0x04001C99 RID: 7321
		private Style navigationButtonStyle;

		// Token: 0x04001C9A RID: 7322
		private ITemplate finishNavigationTemplate;

		// Token: 0x04001C9B RID: 7323
		private ITemplate startNavigationTemplate;

		// Token: 0x04001C9C RID: 7324
		private ITemplate stepNavigationTemplate;

		// Token: 0x04001C9D RID: 7325
		private ITemplate headerTemplate;

		// Token: 0x04001C9E RID: 7326
		private ITemplate sideBarTemplate;

		// Token: 0x04001C9F RID: 7327
		private int activeStepIndex = -1;

		// Token: 0x04001CA0 RID: 7328
		private bool inited;

		// Token: 0x04001CA1 RID: 7329
		private ArrayList history;

		// Token: 0x04001CA2 RID: 7330
		private Table wizardTable;

		// Token: 0x04001CA3 RID: 7331
		private Wizard.WizardHeaderCell _headerCell;

		// Token: 0x04001CA4 RID: 7332
		private TableCell _navigationCell;

		// Token: 0x04001CA5 RID: 7333
		private Wizard.StartNavigationContainer _startNavContainer;

		// Token: 0x04001CA6 RID: 7334
		private Wizard.StepNavigationContainer _stepNavContainer;

		// Token: 0x04001CA7 RID: 7335
		private Wizard.FinishNavigationContainer _finishNavContainer;

		// Token: 0x04001CA8 RID: 7336
		private MultiView multiView;

		// Token: 0x04001CA9 RID: 7337
		private DataList stepDatalist;

		// Token: 0x04001CAA RID: 7338
		private ArrayList styles = new ArrayList();

		// Token: 0x04001CAB RID: 7339
		private Hashtable customNavigation;

		// Token: 0x02000440 RID: 1088
		private sealed class TableCellNamingContainer : TableCell, INamingContainer, INonBindingContainer
		{
			// Token: 0x060032D7 RID: 13015 RVA: 0x00088BA0 File Offset: 0x00086DA0
			protected internal override void RenderChildren(HtmlTextWriter writer)
			{
				if (this.haveSkipLink)
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Href, "#" + this.clientId + "_SkipLink");
					writer.RenderBeginTag(HtmlTextWriterTag.A);
					writer.AddAttribute(HtmlTextWriterAttribute.Alt, this.skipLinkText);
					writer.AddAttribute(HtmlTextWriterAttribute.Height, "0");
					writer.AddAttribute(HtmlTextWriterAttribute.Width, "0");
					Page page = this.Page;
					ClientScriptManager clientScriptManager;
					if (page != null)
					{
						clientScriptManager = page.ClientScript;
					}
					else
					{
						clientScriptManager = new ClientScriptManager(null);
					}
					writer.AddAttribute(HtmlTextWriterAttribute.Src, clientScriptManager.GetWebResourceUrl(typeof(SiteMapPath), "transparent.gif"));
					writer.AddStyleAttribute(HtmlTextWriterStyle.BorderWidth, "0px");
					writer.RenderBeginTag(HtmlTextWriterTag.Img);
					writer.RenderEndTag();
					writer.RenderEndTag();
				}
				base.RenderChildren(writer);
				if (this.haveSkipLink)
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Id, "SkipLink");
					writer.RenderBeginTag(HtmlTextWriterTag.A);
					writer.RenderEndTag();
				}
			}

			// Token: 0x060032D8 RID: 13016 RVA: 0x00088C83 File Offset: 0x00086E83
			public TableCellNamingContainer(string skipLinkText, string clientId)
			{
				this.skipLinkText = skipLinkText;
				this.clientId = clientId;
				this.haveSkipLink = !string.IsNullOrEmpty(skipLinkText);
			}

			// Token: 0x04001CB3 RID: 7347
			private string skipLinkText;

			// Token: 0x04001CB4 RID: 7348
			private string clientId;

			// Token: 0x04001CB5 RID: 7349
			private bool haveSkipLink;
		}

		// Token: 0x02000441 RID: 1089
		private sealed class SideBarButtonTemplate : ITemplate
		{
			// Token: 0x060032D9 RID: 13017 RVA: 0x00088CA8 File Offset: 0x00086EA8
			public SideBarButtonTemplate(Wizard wizard)
			{
				this.wizard = wizard;
			}

			// Token: 0x060032DA RID: 13018 RVA: 0x00088CB8 File Offset: 0x00086EB8
			public void InstantiateIn(Control control)
			{
				LinkButton linkButton = new LinkButton();
				this.wizard.RegisterApplyStyle(linkButton, this.wizard.SideBarButtonStyle);
				control.Controls.Add(linkButton);
				control.DataBinding += this.Bound;
			}

			// Token: 0x060032DB RID: 13019 RVA: 0x00088D00 File Offset: 0x00086F00
			private void Bound(object s, EventArgs args)
			{
				WizardStepBase wizardStepBase = DataBinder.GetDataItem(s) as WizardStepBase;
				if (wizardStepBase != null)
				{
					LinkButton linkButton = (LinkButton)((DataListItem)s).Controls[0];
					linkButton.ID = Wizard.SideBarButtonID;
					linkButton.CommandName = Wizard.MoveToCommandName;
					linkButton.CommandArgument = this.wizard.WizardSteps.IndexOf(wizardStepBase).ToString();
					linkButton.Text = wizardStepBase.Name;
					if (wizardStepBase.StepType == WizardStepType.Complete)
					{
						linkButton.Enabled = false;
					}
				}
			}

			// Token: 0x04001CB6 RID: 7350
			private Wizard wizard;
		}

		// Token: 0x02000442 RID: 1090
		private class WizardHeaderCell : TableCell, INamingContainer, INonBindingContainer
		{
			// Token: 0x17001016 RID: 4118
			// (get) Token: 0x060032DC RID: 13020 RVA: 0x00088D84 File Offset: 0x00086F84
			public bool Initialized
			{
				get
				{
					return this._initialized;
				}
			}

			// Token: 0x060032DE RID: 13022 RVA: 0x00088D94 File Offset: 0x00086F94
			public void ConfirmInitState()
			{
				this._initialized = true;
			}

			// Token: 0x04001CB7 RID: 7351
			private bool _initialized;
		}

		// Token: 0x02000443 RID: 1091
		internal abstract class DefaultNavigationContainer : Wizard.BaseWizardNavigationContainer
		{
			// Token: 0x17001017 RID: 4119
			// (get) Token: 0x060032DF RID: 13023 RVA: 0x00088D9D File Offset: 0x00086F9D
			protected Wizard Wizard
			{
				get
				{
					return this._wizard;
				}
			}

			// Token: 0x060032E0 RID: 13024 RVA: 0x00088DA5 File Offset: 0x00086FA5
			protected DefaultNavigationContainer(Wizard wizard)
			{
				this._wizard = wizard;
			}

			// Token: 0x060032E1 RID: 13025 RVA: 0x00088DB4 File Offset: 0x00086FB4
			public sealed override void PrepareControlHierarchy()
			{
				if (this._isDefault)
				{
					this.UpdateState();
				}
			}

			// Token: 0x060032E2 RID: 13026
			protected abstract void UpdateState();

			// Token: 0x060032E3 RID: 13027 RVA: 0x00088DC4 File Offset: 0x00086FC4
			public void ConfirmDefaultTemplate()
			{
				this._isDefault = true;
			}

			// Token: 0x060032E4 RID: 13028 RVA: 0x00088DD0 File Offset: 0x00086FD0
			protected void UpdateNavButtonState(string id, string text, string image, Style style)
			{
				WebControl webControl = (WebControl)this.FindControl(id);
				foreach (object obj in webControl.Parent.Controls)
				{
					Control control = (Control)obj;
					control.Visible = webControl == control;
				}
				((IButtonControl)webControl).Text = text;
				ImageButton imageButton = webControl as ImageButton;
				if (imageButton != null)
				{
					imageButton.ImageUrl = image;
				}
				webControl.ApplyStyle(style);
			}

			// Token: 0x04001CB8 RID: 7352
			private bool _isDefault;

			// Token: 0x04001CB9 RID: 7353
			private Wizard _wizard;
		}

		// Token: 0x02000444 RID: 1092
		private sealed class StartNavigationContainer : Wizard.DefaultNavigationContainer
		{
			// Token: 0x060032E5 RID: 13029 RVA: 0x00088E68 File Offset: 0x00087068
			public StartNavigationContainer(Wizard wizard)
				: base(wizard)
			{
			}

			// Token: 0x060032E6 RID: 13030 RVA: 0x00088E74 File Offset: 0x00087074
			protected override void UpdateState()
			{
				bool flag = false;
				if (base.Wizard.AllowNavigationToStep(base.Wizard.ActiveStepIndex + 1))
				{
					flag = true;
					base.UpdateNavButtonState(Wizard.StartNextButtonIDShort + base.Wizard.StartNextButtonType, base.Wizard.StartNextButtonText, base.Wizard.StartNextButtonImageUrl, base.Wizard.StartNextButtonStyle);
				}
				else
				{
					((Table)this.Controls[0]).Rows[0].Cells[0].Visible = false;
				}
				if (base.Wizard.DisplayCancelButton)
				{
					flag = true;
					base.UpdateNavButtonState(Wizard.CancelButtonIDShort + base.Wizard.CancelButtonType, base.Wizard.CancelButtonText, base.Wizard.CancelButtonImageUrl, base.Wizard.CancelButtonStyle);
				}
				else
				{
					((Table)this.Controls[0]).Rows[0].Cells[1].Visible = false;
				}
				this.Visible = flag;
			}
		}

		// Token: 0x02000445 RID: 1093
		private sealed class StepNavigationContainer : Wizard.DefaultNavigationContainer
		{
			// Token: 0x060032E7 RID: 13031 RVA: 0x00088E68 File Offset: 0x00087068
			public StepNavigationContainer(Wizard wizard)
				: base(wizard)
			{
			}

			// Token: 0x060032E8 RID: 13032 RVA: 0x00088F98 File Offset: 0x00087198
			protected override void UpdateState()
			{
				bool flag = false;
				if (base.Wizard.AllowNavigationToStep(base.Wizard.ActiveStepIndex - 1))
				{
					flag = true;
					base.UpdateNavButtonState(Wizard.StepPreviousButtonIDShort + base.Wizard.StepPreviousButtonType, base.Wizard.StepPreviousButtonText, base.Wizard.StepPreviousButtonImageUrl, base.Wizard.StepPreviousButtonStyle);
				}
				else
				{
					((Table)this.Controls[0]).Rows[0].Cells[0].Visible = false;
				}
				if (base.Wizard.AllowNavigationToStep(base.Wizard.ActiveStepIndex + 1))
				{
					flag = true;
					base.UpdateNavButtonState(Wizard.StepNextButtonIDShort + base.Wizard.StepNextButtonType, base.Wizard.StepNextButtonText, base.Wizard.StepNextButtonImageUrl, base.Wizard.StepNextButtonStyle);
				}
				else
				{
					((Table)this.Controls[0]).Rows[0].Cells[1].Visible = false;
				}
				if (base.Wizard.DisplayCancelButton)
				{
					flag = true;
					base.UpdateNavButtonState(Wizard.CancelButtonIDShort + base.Wizard.CancelButtonType, base.Wizard.CancelButtonText, base.Wizard.CancelButtonImageUrl, base.Wizard.CancelButtonStyle);
				}
				else
				{
					((Table)this.Controls[0]).Rows[0].Cells[2].Visible = false;
				}
				this.Visible = flag;
			}
		}

		// Token: 0x02000446 RID: 1094
		private sealed class FinishNavigationContainer : Wizard.DefaultNavigationContainer
		{
			// Token: 0x060032E9 RID: 13033 RVA: 0x00088E68 File Offset: 0x00087068
			public FinishNavigationContainer(Wizard wizard)
				: base(wizard)
			{
			}

			// Token: 0x060032EA RID: 13034 RVA: 0x00089148 File Offset: 0x00087348
			protected override void UpdateState()
			{
				int num = base.Wizard.ActiveStepIndex - 1;
				if (num >= 0 && base.Wizard.AllowNavigationToStep(num))
				{
					base.UpdateNavButtonState(Wizard.FinishPreviousButtonIDShort + base.Wizard.FinishPreviousButtonType, base.Wizard.FinishPreviousButtonText, base.Wizard.FinishPreviousButtonImageUrl, base.Wizard.FinishPreviousButtonStyle);
				}
				else
				{
					((Table)this.Controls[0]).Rows[0].Cells[0].Visible = false;
				}
				base.UpdateNavButtonState(Wizard.FinishButtonIDShort + base.Wizard.FinishCompleteButtonType, base.Wizard.FinishCompleteButtonText, base.Wizard.FinishCompleteButtonImageUrl, base.Wizard.FinishCompleteButtonStyle);
				if (base.Wizard.DisplayCancelButton)
				{
					base.UpdateNavButtonState(Wizard.CancelButtonIDShort + base.Wizard.CancelButtonType, base.Wizard.CancelButtonText, base.Wizard.CancelButtonImageUrl, base.Wizard.CancelButtonStyle);
					return;
				}
				((Table)this.Controls[0]).Rows[0].Cells[2].Visible = false;
			}
		}

		// Token: 0x02000447 RID: 1095
		internal class BaseWizardContainer : Table, INamingContainer, INonBindingContainer
		{
			// Token: 0x17001018 RID: 4120
			// (get) Token: 0x060032EB RID: 13035 RVA: 0x000892A2 File Offset: 0x000874A2
			public TableCell InnerCell
			{
				get
				{
					return this.Rows[0].Cells[0];
				}
			}

			// Token: 0x060032EC RID: 13036 RVA: 0x000892BB File Offset: 0x000874BB
			internal BaseWizardContainer()
			{
				this.InitTable();
			}

			// Token: 0x060032ED RID: 13037 RVA: 0x000892CC File Offset: 0x000874CC
			private void InitTable()
			{
				TableRow tableRow = new TableRow();
				TableCell tableCell = new TableCell();
				tableCell.ControlStyle.Width = Unit.Percentage(100.0);
				tableCell.ControlStyle.Height = Unit.Percentage(100.0);
				tableRow.Cells.Add(tableCell);
				base.ControlStyle.Width = Unit.Percentage(100.0);
				base.ControlStyle.Height = Unit.Percentage(100.0);
				this.CellPadding = 0;
				this.CellSpacing = 0;
				this.Rows.Add(tableRow);
			}

			// Token: 0x060032EE RID: 13038 RVA: 0x0000393A File Offset: 0x00001B3A
			public virtual void PrepareControlHierarchy()
			{
			}
		}

		// Token: 0x02000448 RID: 1096
		internal class BaseWizardNavigationContainer : Control, INamingContainer, INonBindingContainer
		{
			// Token: 0x060032EF RID: 13039 RVA: 0x0002C3D8 File Offset: 0x0002A5D8
			internal BaseWizardNavigationContainer()
			{
			}

			// Token: 0x060032F0 RID: 13040 RVA: 0x0000393A File Offset: 0x00001B3A
			public virtual void PrepareControlHierarchy()
			{
			}
		}

		// Token: 0x02000449 RID: 1097
		internal abstract class DefaultContentContainer : Wizard.BaseWizardContainer
		{
			// Token: 0x17001019 RID: 4121
			// (get) Token: 0x060032F1 RID: 13041 RVA: 0x00089371 File Offset: 0x00087571
			protected bool IsDefaultTemplate
			{
				get
				{
					return this._isDefault;
				}
			}

			// Token: 0x1700101A RID: 4122
			// (get) Token: 0x060032F2 RID: 13042 RVA: 0x00089379 File Offset: 0x00087579
			protected Wizard Wizard
			{
				get
				{
					return this._wizard;
				}
			}

			// Token: 0x060032F3 RID: 13043 RVA: 0x00089381 File Offset: 0x00087581
			protected DefaultContentContainer(Wizard wizard)
			{
				this._wizard = wizard;
			}

			// Token: 0x060032F4 RID: 13044 RVA: 0x00089390 File Offset: 0x00087590
			public sealed override void PrepareControlHierarchy()
			{
				if (this._isDefault)
				{
					this.UpdateState();
				}
			}

			// Token: 0x060032F5 RID: 13045
			protected abstract void UpdateState();

			// Token: 0x060032F6 RID: 13046 RVA: 0x000893A0 File Offset: 0x000875A0
			public void ConfirmDefaultTemplate()
			{
				this._isDefault = true;
			}

			// Token: 0x04001CBA RID: 7354
			private bool _isDefault;

			// Token: 0x04001CBB RID: 7355
			private Wizard _wizard;
		}
	}
}
