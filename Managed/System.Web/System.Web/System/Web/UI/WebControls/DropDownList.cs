using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents a control that allows the user to select a single item from a drop-down list. </summary>
	// Token: 0x02000391 RID: 913
	[ValidationProperty("SelectedItem")]
	[SupportsEventValidation]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class DropDownList : ListControl, IPostBackDataHandler
	{
		/// <summary>Gets or sets the border color of the control.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the border color of the control.</returns>
		// Token: 0x17000B64 RID: 2916
		// (get) Token: 0x060023D3 RID: 9171 RVA: 0x0005CFEE File Offset: 0x0005B1EE
		// (set) Token: 0x060023D4 RID: 9172 RVA: 0x0005CFF6 File Offset: 0x0005B1F6
		[Browsable(false)]
		public override Color BorderColor
		{
			get
			{
				return base.BorderColor;
			}
			set
			{
				base.BorderColor = value;
			}
		}

		/// <summary>Gets or sets the border style of the control.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.BorderStyle" /> values.</returns>
		// Token: 0x17000B65 RID: 2917
		// (get) Token: 0x060023D5 RID: 9173 RVA: 0x0005CFFF File Offset: 0x0005B1FF
		// (set) Token: 0x060023D6 RID: 9174 RVA: 0x0005D007 File Offset: 0x0005B207
		[Browsable(false)]
		public override BorderStyle BorderStyle
		{
			get
			{
				return base.BorderStyle;
			}
			set
			{
				base.BorderStyle = value;
			}
		}

		/// <summary>Gets or sets the border width for the control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.Unit" /> that represents the border width for the control.</returns>
		// Token: 0x17000B66 RID: 2918
		// (get) Token: 0x060023D7 RID: 9175 RVA: 0x0005D010 File Offset: 0x0005B210
		// (set) Token: 0x060023D8 RID: 9176 RVA: 0x0005D018 File Offset: 0x0005B218
		[Browsable(false)]
		public override Unit BorderWidth
		{
			get
			{
				return base.BorderWidth;
			}
			set
			{
				base.BorderWidth = value;
			}
		}

		/// <summary>Gets or sets the index of the selected item in the <see cref="T:System.Web.UI.WebControls.DropDownList" /> control.</summary>
		/// <returns>The index of the selected item in the <see cref="T:System.Web.UI.WebControls.DropDownList" /> control. The default value is 0, which selects the first item in the list.</returns>
		// Token: 0x17000B67 RID: 2919
		// (get) Token: 0x060023D9 RID: 9177 RVA: 0x0005D024 File Offset: 0x0005B224
		// (set) Token: 0x060023DA RID: 9178 RVA: 0x0005D05E File Offset: 0x0005B25E
		[DefaultValue(0)]
		[WebSysDescription("")]
		[WebCategory("Misc")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override int SelectedIndex
		{
			get
			{
				int selectedIndex = base.SelectedIndex;
				if (selectedIndex != -1 || this.Items.Count == 0)
				{
					return selectedIndex;
				}
				this.Items[0].Selected = true;
				return 0;
			}
			set
			{
				base.SelectedIndex = value;
			}
		}

		/// <summary>Gets a value that indicates whether the control should set the disabled attribute of the rendered HTML element to "disabled" when the control's <see cref="P:System.Web.UI.WebControls.WebControl.IsEnabled" /> property is false.</summary>
		/// <returns>true if the <see cref="P:System.Web.UI.Control.RenderingCompatibility" /> property indicates an ASP.NET version lower than 4.0; otherwise, false.</returns>
		// Token: 0x17000B68 RID: 2920
		// (get) Token: 0x060023DB RID: 9179 RVA: 0x0004789D File Offset: 0x00045A9D
		public override bool SupportsDisabledAttribute
		{
			get
			{
				return base.RenderingCompatibilityLessThan40;
			}
		}

		/// <summary>Adds HTML attributes and styles that need to be rendered to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object.</summary>
		/// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream that renders HTML contents to the client.</param>
		// Token: 0x060023DC RID: 9180 RVA: 0x0005D068 File Offset: 0x0005B268
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			Page page = this.Page;
			if (page != null)
			{
				page.VerifyRenderingInServerForm(this);
			}
			if (writer == null)
			{
				return;
			}
			if (!string.IsNullOrEmpty(this.UniqueID))
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Name, this.UniqueID, true);
			}
			if (!base.IsEnabled && this.SelectedIndex == -1)
			{
				this.SelectedIndex = 1;
			}
			if (this.AutoPostBack)
			{
				string text = ((page != null) ? page.ClientScript.GetPostBackEventReference(this.GetPostBackOptions(), true) : string.Empty);
				text = "setTimeout('" + text.Replace("\\", "\\\\").Replace("'", "\\'") + "', 0)";
				writer.AddAttribute(HtmlTextWriterAttribute.Onchange, base.BuildScriptAttribute("onchange", text));
			}
			base.AddAttributesToRender(writer);
		}

		// Token: 0x060023DD RID: 9181 RVA: 0x0005D130 File Offset: 0x0005B330
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

		/// <summary>Creates a collection to store child controls.</summary>
		/// <returns>Always returns an <see cref="T:System.Web.UI.EmptyControlCollection" />.</returns>
		// Token: 0x060023DE RID: 9182 RVA: 0x0004B0A2 File Offset: 0x000492A2
		protected override ControlCollection CreateControlCollection()
		{
			return base.CreateControlCollection();
		}

		/// <summary>Always throws an <see cref="T:System.Web.HttpException" /> exception because multiple selection is not supported for the <see cref="T:System.Web.UI.WebControls.DropDownList" /> control.</summary>
		/// <exception cref="T:System.Web.HttpException">In all cases.</exception>
		// Token: 0x060023DF RID: 9183 RVA: 0x0005D1A7 File Offset: 0x0005B3A7
		protected internal override void VerifyMultiSelect()
		{
			throw new HttpException("DropDownList only may have a single selected item");
		}

		/// <summary>Processes postback data for the <see cref="T:System.Web.UI.WebControls.DropDownList" /> control.</summary>
		/// <returns>true if the <see cref="T:System.Web.UI.WebControls.DropDownList" /> control's state changes as a result of the postback event; otherwise, false.</returns>
		/// <param name="postDataKey">The index within the posted collection that references the content to load.</param>
		/// <param name="postCollection">The collection of all incoming name values posted to the server.</param>
		// Token: 0x060023E0 RID: 9184 RVA: 0x0005D1B4 File Offset: 0x0005B3B4
		protected virtual bool LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			this.EnsureDataBound();
			int num = this.Items.IndexOf(postCollection[postDataKey]);
			base.ValidateEvent(postDataKey, postCollection[postDataKey]);
			if (num != this.SelectedIndex)
			{
				this.SelectedIndex = num;
				return true;
			}
			return false;
		}

		/// <summary>Raises events for the <see cref="T:System.Web.UI.WebControls.DropDownList" /> control when postback occurs.</summary>
		// Token: 0x060023E1 RID: 9185 RVA: 0x0005D1FC File Offset: 0x0005B3FC
		protected virtual void RaisePostDataChangedEvent()
		{
			if (this.CausesValidation)
			{
				Page page = this.Page;
				if (page != null)
				{
					page.Validate(this.ValidationGroup);
				}
			}
			this.OnSelectedIndexChanged(EventArgs.Empty);
		}

		/// <summary>Processes posted data for the <see cref="T:System.Web.UI.WebControls.DropDownList" /> control.</summary>
		/// <returns>true if the posted content is different from the last posting; otherwise, false.</returns>
		/// <param name="postDataKey">The key value used to index an entry in the collection. </param>
		/// <param name="postCollection">A <see cref="T:System.Collections.Specialized.NameValueCollection" /> that contains post information.  </param>
		// Token: 0x060023E2 RID: 9186 RVA: 0x0005D232 File Offset: 0x0005B432
		bool IPostBackDataHandler.LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			return this.LoadPostData(postDataKey, postCollection);
		}

		/// <summary>Raises events for the <see cref="T:System.Web.UI.WebControls.DropDownList" /> control on postback.</summary>
		// Token: 0x060023E3 RID: 9187 RVA: 0x0005D23C File Offset: 0x0005B43C
		void IPostBackDataHandler.RaisePostDataChangedEvent()
		{
			this.RaisePostDataChangedEvent();
		}
	}
}
