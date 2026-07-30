using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.Security.Permissions;
using System.Web.Util;

namespace System.Web.UI.WebControls
{
	/// <summary>Displays a check box that allows the user to select a true or false condition.</summary>
	// Token: 0x0200034C RID: 844
	[Designer("System.Web.UI.Design.WebControls.CheckBoxDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[DataBindingHandler("System.Web.UI.Design.TextDataBindingHandler, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[DefaultEvent("CheckedChanged")]
	[DefaultProperty("Text")]
	[ControlValueProperty("Checked", null)]
	[SupportsEventValidation]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class CheckBox : WebControl, IPostBackDataHandler, ICheckBoxControl
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.CheckBox" /> class.</summary>
		// Token: 0x06001EFE RID: 7934 RVA: 0x0004E4EE File Offset: 0x0004C6EE
		public CheckBox()
			: base(HtmlTextWriterTag.Input)
		{
			this.render_type = "checkbox";
		}

		// Token: 0x06001EFF RID: 7935 RVA: 0x0004E503 File Offset: 0x0004C703
		internal CheckBox(string render_type)
			: base(HtmlTextWriterTag.Input)
		{
			this.render_type = render_type;
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Web.UI.WebControls.CheckBox" /> state automatically posts back to the server when clicked.</summary>
		/// <returns>true to automatically post the state of the <see cref="T:System.Web.UI.WebControls.CheckBox" /> control to the server when it is clicked; otherwise, false. The default is false.</returns>
		// Token: 0x170009AA RID: 2474
		// (get) Token: 0x06001F00 RID: 7936 RVA: 0x0004E514 File Offset: 0x0004C714
		// (set) Token: 0x06001F01 RID: 7937 RVA: 0x0004E527 File Offset: 0x0004C727
		[Themeable(false)]
		[DefaultValue(false)]
		[WebSysDescription("")]
		[WebCategory("Behavior")]
		public virtual bool AutoPostBack
		{
			get
			{
				return this.ViewState.GetBool("AutoPostBack", false);
			}
			set
			{
				this.ViewState["AutoPostBack"] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether validation is performed when the <see cref="T:System.Web.UI.WebControls.CheckBox" /> control is selected.</summary>
		/// <returns>true if validation is performed when the <see cref="T:System.Web.UI.WebControls.CheckBox" /> is clicked; otherwise, false. The default is false.</returns>
		// Token: 0x170009AB RID: 2475
		// (get) Token: 0x06001F02 RID: 7938 RVA: 0x0004E53F File Offset: 0x0004C73F
		// (set) Token: 0x06001F03 RID: 7939 RVA: 0x000419B2 File Offset: 0x0003FBB2
		[DefaultValue(false)]
		[Themeable(false)]
		[WebSysDescription("")]
		[WebCategory("Behavior")]
		public virtual bool CausesValidation
		{
			get
			{
				return this.ViewState.GetBool("CausesValidation", false);
			}
			set
			{
				this.ViewState["CausesValidation"] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Web.UI.WebControls.CheckBox" /> control is checked.</summary>
		/// <returns>true to indicate a checked state; otherwise, false. The default is false.</returns>
		// Token: 0x170009AC RID: 2476
		// (get) Token: 0x06001F04 RID: 7940 RVA: 0x0004E552 File Offset: 0x0004C752
		// (set) Token: 0x06001F05 RID: 7941 RVA: 0x0004E565 File Offset: 0x0004C765
		[DefaultValue(false)]
		[Bindable(true, BindingDirection.TwoWay)]
		[Themeable(false)]
		[WebSysDescription("")]
		[WebCategory("Behavior")]
		public virtual bool Checked
		{
			get
			{
				return this.ViewState.GetBool("Checked", false);
			}
			set
			{
				this.ViewState["Checked"] = value;
			}
		}

		/// <summary>Gets a reference to the collection of attributes for the rendered input element of the <see cref="T:System.Web.UI.WebControls.CheckBox" /> control.</summary>
		/// <returns>The collection of attribute names and values that are added to the rendered INPUT element for the <see cref="T:System.Web.UI.WebControls.CheckBox" /> control. The default is an empty <see cref="T:System.Web.UI.AttributeCollection" />.</returns>
		// Token: 0x170009AD RID: 2477
		// (get) Token: 0x06001F06 RID: 7942 RVA: 0x0004E580 File Offset: 0x0004C780
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public AttributeCollection InputAttributes
		{
			get
			{
				if (this.inputAttributes == null)
				{
					if (this.inputAttributesState == null)
					{
						this.inputAttributesState = new StateBag(true);
						if (base.IsTrackingViewState)
						{
							this.inputAttributesState.TrackViewState();
						}
					}
					this.inputAttributes = new AttributeCollection(this.inputAttributesState);
				}
				return this.inputAttributes;
			}
		}

		/// <summary>Gets a reference to the collection of attributes for the rendered LABEL element of the <see cref="T:System.Web.UI.WebControls.CheckBox" /> control.</summary>
		/// <returns>The collection of attribute names and values that are added to the rendered LABEL element for the <see cref="T:System.Web.UI.WebControls.CheckBox" />. The default is an empty <see cref="T:System.Web.UI.AttributeCollection" />.</returns>
		// Token: 0x170009AE RID: 2478
		// (get) Token: 0x06001F07 RID: 7943 RVA: 0x0004E5D4 File Offset: 0x0004C7D4
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public AttributeCollection LabelAttributes
		{
			get
			{
				if (this.labelAttributes == null)
				{
					if (this.labelAttributesState == null)
					{
						this.labelAttributesState = new StateBag(true);
						if (base.IsTrackingViewState)
						{
							this.labelAttributesState.TrackViewState();
						}
					}
					this.labelAttributes = new AttributeCollection(this.labelAttributesState);
				}
				return this.labelAttributes;
			}
		}

		/// <summary>Gets or sets the text label associated with the <see cref="T:System.Web.UI.WebControls.CheckBox" />.</summary>
		/// <returns>The text label associated with the CheckBox. The default value is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x170009AF RID: 2479
		// (get) Token: 0x06001F08 RID: 7944 RVA: 0x0004A013 File Offset: 0x00048213
		// (set) Token: 0x06001F09 RID: 7945 RVA: 0x0004A02A File Offset: 0x0004822A
		[WebCategory("Appearance")]
		[WebSysDescription("")]
		[Localizable(true)]
		[Bindable(true)]
		[DefaultValue("")]
		public virtual string Text
		{
			get
			{
				return this.ViewState.GetString("Text", string.Empty);
			}
			set
			{
				this.ViewState["Text"] = value;
			}
		}

		/// <summary>Gets or sets the alignment of the text label associated with the <see cref="T:System.Web.UI.WebControls.CheckBox" /> control.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.TextAlign" /> values. The default value is Right.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified value is not one of the <see cref="T:System.Web.UI.WebControls.TextAlign" /> values. </exception>
		// Token: 0x170009B0 RID: 2480
		// (get) Token: 0x06001F0A RID: 7946 RVA: 0x0004E627 File Offset: 0x0004C827
		// (set) Token: 0x06001F0B RID: 7947 RVA: 0x0004E63A File Offset: 0x0004C83A
		[WebSysDescription("")]
		[DefaultValue(TextAlign.Right)]
		[WebCategory("Appearance")]
		public virtual TextAlign TextAlign
		{
			get
			{
				return (TextAlign)this.ViewState.GetInt("TextAlign", 2);
			}
			set
			{
				if (value != TextAlign.Left && value != TextAlign.Right)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.ViewState["TextAlign"] = value;
			}
		}

		/// <summary>Gets or sets the group of controls for which the <see cref="T:System.Web.UI.WebControls.CheckBox" /> control causes validation when it posts back to the server. </summary>
		/// <returns>The group of controls for which the <see cref="T:System.Web.UI.WebControls.CheckBox" /> causes validation when it posts back to the server. The default is an empty string ("").</returns>
		// Token: 0x170009B1 RID: 2481
		// (get) Token: 0x06001F0C RID: 7948 RVA: 0x000419CA File Offset: 0x0003FBCA
		// (set) Token: 0x06001F0D RID: 7949 RVA: 0x000419E1 File Offset: 0x0003FBE1
		[Themeable(false)]
		[DefaultValue("")]
		[WebSysDescription("")]
		[WebCategory("Behavior")]
		public virtual string ValidationGroup
		{
			get
			{
				return this.ViewState.GetString("ValidationGroup", string.Empty);
			}
			set
			{
				this.ViewState["ValidationGroup"] = value;
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Web.UI.WebControls.CheckBox.Checked" /> property changes between posts to the server.</summary>
		// Token: 0x14000055 RID: 85
		// (add) Token: 0x06001F0E RID: 7950 RVA: 0x0004E665 File Offset: 0x0004C865
		// (remove) Token: 0x06001F0F RID: 7951 RVA: 0x0004E678 File Offset: 0x0004C878
		[WebSysDescription("")]
		[WebCategory("Action")]
		public event EventHandler CheckedChanged
		{
			add
			{
				base.Events.AddHandler(CheckBox.EventCheckedChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(CheckBox.EventCheckedChanged, value);
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.CheckBox.CheckedChanged" /> event of the <see cref="T:System.Web.UI.WebControls.CheckBox" /> control. This allows you to handle the event directly.</summary>
		/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06001F10 RID: 7952 RVA: 0x0004E68C File Offset: 0x0004C88C
		protected virtual void OnCheckedChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[CheckBox.EventCheckedChanged];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x170009B2 RID: 2482
		// (get) Token: 0x06001F11 RID: 7953 RVA: 0x00042187 File Offset: 0x00040387
		internal virtual string NameAttribute
		{
			get
			{
				return this.UniqueID;
			}
		}

		/// <summary>Loads the previously saved view state of the <see cref="T:System.Web.UI.WebControls.CheckBox" /> control.</summary>
		/// <param name="savedState">An object that contains the saved view state values for the control. </param>
		// Token: 0x06001F12 RID: 7954 RVA: 0x0004E6BC File Offset: 0x0004C8BC
		protected override void LoadViewState(object savedState)
		{
			if (savedState == null)
			{
				base.LoadViewState(null);
				return;
			}
			Triplet triplet = (Triplet)savedState;
			base.LoadViewState(triplet.First);
			if (triplet.Second != null)
			{
				if (this.inputAttributesState == null)
				{
					this.inputAttributesState = new StateBag(true);
					this.inputAttributesState.TrackViewState();
				}
				this.inputAttributesState.LoadViewState(triplet.Second);
			}
			if (triplet.Third != null)
			{
				if (this.labelAttributesState == null)
				{
					this.labelAttributesState = new StateBag(true);
					this.labelAttributesState.TrackViewState();
				}
				this.labelAttributesState.LoadViewState(triplet.Third);
			}
		}

		/// <summary>Saves the changes to the <see cref="T:System.Web.UI.WebControls.CheckBox" /> view state since the time the page was posted back to the server.</summary>
		/// <returns>The object that contains the changes to the <see cref="T:System.Web.UI.WebControls.CheckBox" /> view state; otherwise, if no view state is associated with the object, null.</returns>
		// Token: 0x06001F13 RID: 7955 RVA: 0x0004E758 File Offset: 0x0004C958
		protected override object SaveViewState()
		{
			object obj = base.SaveViewState();
			object obj2 = null;
			object obj3 = null;
			if (this.inputAttributesState != null)
			{
				obj2 = this.inputAttributesState.SaveViewState();
			}
			if (this.labelAttributesState != null)
			{
				obj3 = this.labelAttributesState.SaveViewState();
			}
			if (obj == null && obj2 == null && obj3 == null)
			{
				return null;
			}
			return new Triplet(obj, obj2, obj3);
		}

		/// <summary>Tracks view-state changes to the <see cref="T:System.Web.UI.WebControls.CheckBox" /> control so that they can be stored in the control's <see cref="T:System.Web.UI.StateBag" /> object. This object is accessible through the <see cref="P:System.Web.UI.Control.ViewState" /> property. </summary>
		// Token: 0x06001F14 RID: 7956 RVA: 0x0004E7AB File Offset: 0x0004C9AB
		protected override void TrackViewState()
		{
			base.TrackViewState();
			if (this.inputAttributesState != null)
			{
				this.inputAttributesState.TrackViewState();
			}
			if (this.labelAttributesState != null)
			{
				this.labelAttributesState.TrackViewState();
			}
		}

		/// <summary>Registers client script for generating postback prior to rendering on the client if <see cref="P:System.Web.UI.WebControls.CheckBox.AutoPostBack" /> is true.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06001F15 RID: 7957 RVA: 0x0004E7DC File Offset: 0x0004C9DC
		protected internal override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			Page page = this.Page;
			if (page != null && base.IsEnabled)
			{
				page.RegisterRequiresPostBack(this);
				page.RegisterEnabledControl(this);
			}
		}

		// Token: 0x06001F16 RID: 7958 RVA: 0x0004E810 File Offset: 0x0004CA10
		private static bool IsInputOrCommonAttr(string attname)
		{
			attname = attname.ToUpper(Helpers.InvariantCulture);
			uint num = global::<PrivateImplementationDetails>.ComputeStringHash(attname);
			if (num <= 1828175992U)
			{
				if (num <= 622060074U)
				{
					if (num <= 370604265U)
					{
						if (num != 223059961U)
						{
							if (num != 304993157U)
							{
								if (num != 370604265U)
								{
									return false;
								}
								if (!(attname == "SRC"))
								{
									return false;
								}
							}
							else if (!(attname == "DISABLED"))
							{
								return false;
							}
						}
						else if (!(attname == "ONMOUSEOVER"))
						{
							return false;
						}
					}
					else if (num != 527474642U)
					{
						if (num != 547231721U)
						{
							if (num != 622060074U)
							{
								return false;
							}
							if (!(attname == "VALUE"))
							{
								return false;
							}
						}
						else if (!(attname == "ACCEPT"))
						{
							return false;
						}
					}
					else if (!(attname == "ACCESSKEY"))
					{
						return false;
					}
				}
				else if (num <= 1394915399U)
				{
					if (num != 904560638U)
					{
						if (num != 990604873U)
						{
							if (num != 1394915399U)
							{
								return false;
							}
							if (!(attname == "ONMOUSEDOWN"))
							{
								return false;
							}
						}
						else if (!(attname == "ONBLUR"))
						{
							return false;
						}
					}
					else if (!(attname == "ONMOUSEUP"))
					{
						return false;
					}
				}
				else if (num <= 1636987420U)
				{
					if (num != 1566678716U)
					{
						if (num != 1636987420U)
						{
							return false;
						}
						if (!(attname == "SIZE"))
						{
							return false;
						}
					}
					else if (!(attname == "ALT"))
					{
						return false;
					}
				}
				else if (num != 1656862888U)
				{
					if (num != 1828175992U)
					{
						return false;
					}
					if (!(attname == "ONSELECT"))
					{
						return false;
					}
				}
				else if (!(attname == "TABINDEX"))
				{
					return false;
				}
			}
			else if (num <= 2741377153U)
			{
				if (num <= 1994583590U)
				{
					if (num != 1883566248U)
					{
						if (num != 1925226952U)
						{
							if (num != 1994583590U)
							{
								return false;
							}
							if (!(attname == "CHECKED"))
							{
								return false;
							}
						}
						else if (!(attname == "ONFOCUS"))
						{
							return false;
						}
					}
					else if (!(attname == "ONCHANGE"))
					{
						return false;
					}
				}
				else if (num != 2097196413U)
				{
					if (num != 2140897288U)
					{
						if (num != 2741377153U)
						{
							return false;
						}
						if (!(attname == "MAXLENGTH"))
						{
							return false;
						}
					}
					else if (!(attname == "ONCLICK"))
					{
						return false;
					}
				}
				else if (!(attname == "ONMOUSEOUT"))
				{
					return false;
				}
			}
			else if (num <= 3145768986U)
			{
				if (num != 2937075063U)
				{
					if (num != 3044347291U)
					{
						if (num != 3145768986U)
						{
							return false;
						}
						if (!(attname == "ONKEYUP"))
						{
							return false;
						}
					}
					else if (!(attname == "ONKEYDOWN"))
					{
						return false;
					}
				}
				else if (!(attname == "READONLY"))
				{
					return false;
				}
			}
			else if (num <= 3644745434U)
			{
				if (num != 3538573158U)
				{
					if (num != 3644745434U)
					{
						return false;
					}
					if (!(attname == "USEMAP"))
					{
						return false;
					}
				}
				else if (!(attname == "ONKEYPRESS"))
				{
					return false;
				}
			}
			else if (num != 3798112898U)
			{
				if (num != 4239779666U)
				{
					return false;
				}
				if (!(attname == "ONMOUSEMOVE"))
				{
					return false;
				}
			}
			else if (!(attname == "ONDBLCLICK"))
			{
				return false;
			}
			return true;
		}

		// Token: 0x06001F17 RID: 7959 RVA: 0x0004EBBC File Offset: 0x0004CDBC
		private bool AddAttributesForSpan(HtmlTextWriter writer)
		{
			if (base.HasAttributes)
			{
				AttributeCollection attributes = base.Attributes;
				ICollection keys = attributes.Keys;
				string[] array = new string[keys.Count];
				keys.CopyTo(array, 0);
				foreach (string text in array)
				{
					if (CheckBox.IsInputOrCommonAttr(text))
					{
						if (this.common_attrs == null)
						{
							this.common_attrs = new AttributeCollection(new StateBag());
						}
						this.common_attrs[text] = base.Attributes[text];
						attributes.Remove(text);
					}
				}
				if (attributes.Count > 0)
				{
					attributes.AddAttributes(writer);
					return true;
				}
			}
			return false;
		}

		/// <summary>Displays the <see cref="T:System.Web.UI.WebControls.CheckBox" /> on the client.</summary>
		/// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> that contains the output stream to render on the client. </param>
		// Token: 0x06001F18 RID: 7960 RVA: 0x0004EC60 File Offset: 0x0004CE60
		protected internal override void Render(HtmlTextWriter writer)
		{
			Page page = this.Page;
			if (page != null)
			{
				page.VerifyRenderingInServerForm(this);
				page.ClientScript.RegisterForEventValidation(this.UniqueID);
			}
			bool flag = base.ControlStyleCreated && !base.ControlStyle.IsEmpty;
			bool isEnabled = base.IsEnabled;
			if (!isEnabled)
			{
				if (!base.RenderingCompatibilityLessThan40)
				{
					base.ControlStyle.PrependCssClass(WebControl.DisabledCssClass);
				}
				else
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Disabled, "disabled", false);
				}
				flag = true;
			}
			if (flag)
			{
				base.AddDisplayStyleAttribute(writer);
				base.ControlStyle.AddAttributesToRender(writer, this);
			}
			string toolTip = this.ToolTip;
			if (toolTip != null && toolTip.Length > 0)
			{
				writer.AddAttribute("title", toolTip);
				flag = true;
			}
			if (base.HasAttributes && this.AddAttributesForSpan(writer))
			{
				flag = true;
			}
			if (flag)
			{
				writer.RenderBeginTag(HtmlTextWriterTag.Span);
			}
			if (this.TextAlign == TextAlign.Right)
			{
				this.RenderInput(writer, isEnabled);
				this.RenderLabel(writer);
			}
			else
			{
				this.RenderLabel(writer);
				this.RenderInput(writer, isEnabled);
			}
			if (flag)
			{
				writer.RenderEndTag();
			}
		}

		// Token: 0x06001F19 RID: 7961 RVA: 0x0004ED68 File Offset: 0x0004CF68
		private void RenderInput(HtmlTextWriter w, bool enabled)
		{
			if (this.ClientID != null && this.ClientID.Length > 0)
			{
				w.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID);
			}
			w.AddAttribute(HtmlTextWriterAttribute.Type, this.render_type);
			string nameAttribute = this.NameAttribute;
			if (nameAttribute != null && nameAttribute.Length > 0)
			{
				w.AddAttribute(HtmlTextWriterAttribute.Name, nameAttribute);
			}
			this.InternalAddAttributesToRender(w, enabled);
			this.AddAttributesToRender(w);
			if (this.Checked)
			{
				w.AddAttribute(HtmlTextWriterAttribute.Checked, "checked", false);
			}
			if (this.AutoPostBack)
			{
				Page page = this.Page;
				string text = ((page != null) ? page.ClientScript.GetPostBackEventReference(this.GetPostBackOptions(), true) : string.Empty);
				text = "setTimeout('" + text.Replace("\\", "\\\\").Replace("'", "\\'") + "', 0)";
				if (this.common_attrs != null && this.common_attrs["onclick"] != null)
				{
					text = ClientScriptManager.EnsureEndsWithSemicolon(this.common_attrs["onclick"]) + text;
					this.common_attrs.Remove("onclick");
				}
				w.AddAttribute(HtmlTextWriterAttribute.Onclick, text);
			}
			if (this.AccessKey.Length > 0)
			{
				w.AddAttribute(HtmlTextWriterAttribute.Accesskey, this.AccessKey);
			}
			if (this.TabIndex != 0)
			{
				w.AddAttribute(HtmlTextWriterAttribute.Tabindex, this.TabIndex.ToString(NumberFormatInfo.InvariantInfo));
			}
			if (this.common_attrs != null)
			{
				this.common_attrs.AddAttributes(w);
			}
			if (this.inputAttributes != null)
			{
				this.inputAttributes.AddAttributes(w);
			}
			w.RenderBeginTag(HtmlTextWriterTag.Input);
			w.RenderEndTag();
		}

		// Token: 0x06001F1A RID: 7962 RVA: 0x0004EF0C File Offset: 0x0004D10C
		private void RenderLabel(HtmlTextWriter w)
		{
			string text = this.Text;
			if (text.Length > 0)
			{
				if (this.labelAttributes != null)
				{
					this.labelAttributes.AddAttributes(w);
				}
				w.AddAttribute(HtmlTextWriterAttribute.For, this.ClientID);
				w.RenderBeginTag(HtmlTextWriterTag.Label);
				w.Write(text);
				w.RenderEndTag();
			}
		}

		/// <summary>Processes the postback data for the <see cref="T:System.Web.UI.WebControls.CheckBox" /> control.</summary>
		/// <returns>true if the posted content is different from the last posting; otherwise, false.</returns>
		/// <param name="postDataKey">The index within the posted collection that references the content to load. </param>
		/// <param name="postCollection">The collection posted to the server.</param>
		// Token: 0x06001F1B RID: 7963 RVA: 0x0004EF60 File Offset: 0x0004D160
		protected virtual bool LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			if (!base.IsEnabled)
			{
				return false;
			}
			string text = postCollection[postDataKey];
			bool flag = text != null && text.Length > 0;
			if (this.Checked != flag)
			{
				this.Checked = flag;
				return true;
			}
			return false;
		}

		/// <summary>Invokes the <see cref="M:System.Web.UI.WebControls.CheckBox.OnCheckedChanged(System.EventArgs)" /> method when the posted data for the <see cref="T:System.Web.UI.WebControls.CheckBox" /> control has changed.</summary>
		// Token: 0x06001F1C RID: 7964 RVA: 0x0004EFA4 File Offset: 0x0004D1A4
		protected virtual void RaisePostDataChangedEvent()
		{
			base.ValidateEvent(this.UniqueID, string.Empty);
			if (this.CausesValidation)
			{
				Page page = this.Page;
				if (page != null)
				{
					page.Validate(this.ValidationGroup);
				}
			}
			this.OnCheckedChanged(EventArgs.Empty);
		}

		/// <summary>Processes posted data for the <see cref="T:System.Web.UI.WebControls.CheckBox" /> control.</summary>
		/// <returns>true if the state of the <see cref="T:System.Web.UI.WebControls.CheckBox" /> has changed; otherwise false.</returns>
		/// <param name="postDataKey">The key value used to index an entry in the collection. </param>
		/// <param name="postCollection">A <see cref="T:System.Collections.Specialized.NameValueCollection" /> that contains post information. </param>
		// Token: 0x06001F1D RID: 7965 RVA: 0x0004EFEB File Offset: 0x0004D1EB
		bool IPostBackDataHandler.LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			return this.LoadPostData(postDataKey, postCollection);
		}

		/// <summary>Raises when posted data for a control has changed.</summary>
		// Token: 0x06001F1E RID: 7966 RVA: 0x0004EFF5 File Offset: 0x0004D1F5
		void IPostBackDataHandler.RaisePostDataChangedEvent()
		{
			this.RaisePostDataChangedEvent();
		}

		// Token: 0x06001F1F RID: 7967 RVA: 0x0004F000 File Offset: 0x0004D200
		private PostBackOptions GetPostBackOptions()
		{
			PostBackOptions postBackOptions = new PostBackOptions(this);
			postBackOptions.ActionUrl = null;
			postBackOptions.ValidationGroup = null;
			postBackOptions.Argument = string.Empty;
			postBackOptions.RequiresJavaScriptProtocol = false;
			postBackOptions.ClientSubmit = true;
			Page page = this.Page;
			postBackOptions.PerformValidation = this.CausesValidation && page != null && page.AreValidatorsUplevel(this.ValidationGroup);
			if (postBackOptions.PerformValidation)
			{
				postBackOptions.ValidationGroup = this.ValidationGroup;
			}
			return postBackOptions;
		}

		/// <summary>Adds the HTML attributes and styles of a <see cref="T:System.Web.UI.WebControls.CheckBox" /> control to be rendered to the specified output stream.</summary>
		/// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
		// Token: 0x06001F20 RID: 7968 RVA: 0x0000393A File Offset: 0x00001B3A
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
		}

		// Token: 0x06001F21 RID: 7969 RVA: 0x0004F077 File Offset: 0x0004D277
		internal virtual void InternalAddAttributesToRender(HtmlTextWriter w, bool enabled)
		{
			if (!enabled)
			{
				w.AddAttribute(HtmlTextWriterAttribute.Disabled, "disabled", false);
			}
		}

		// Token: 0x0400187F RID: 6271
		private string render_type;

		// Token: 0x04001880 RID: 6272
		private AttributeCollection common_attrs;

		// Token: 0x04001881 RID: 6273
		private AttributeCollection inputAttributes;

		// Token: 0x04001882 RID: 6274
		private StateBag inputAttributesState;

		// Token: 0x04001883 RID: 6275
		private AttributeCollection labelAttributes;

		// Token: 0x04001884 RID: 6276
		private StateBag labelAttributesState;

		// Token: 0x04001885 RID: 6277
		private static readonly object EventCheckedChanged = new object();
	}
}
