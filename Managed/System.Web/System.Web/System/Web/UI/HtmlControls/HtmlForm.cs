using System;
using System.ComponentModel;
using System.Security.Permissions;
using System.Web.Configuration;
using System.Web.UI.WebControls;
using System.Web.Util;

namespace System.Web.UI.HtmlControls
{
	/// <summary>Provides programmatic access to the HTML &lt;form&gt; element on the server.</summary>
	// Token: 0x0200025A RID: 602
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class HtmlForm : HtmlContainerControl
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlForm" /> class.</summary>
		// Token: 0x06001889 RID: 6281 RVA: 0x00042040 File Offset: 0x00040240
		public HtmlForm()
			: base("form")
		{
		}

		/// <summary>Gets or sets the action attribute of the HTML form.</summary>
		/// <returns>The action attribute of the HTML form. The default value is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x170007CE RID: 1998
		// (get) Token: 0x0600188A RID: 6282 RVA: 0x00042050 File Offset: 0x00040250
		// (set) Token: 0x0600188B RID: 6283 RVA: 0x0004207D File Offset: 0x0004027D
		public string Action
		{
			get
			{
				string text = base.Attributes["action"];
				if (string.IsNullOrEmpty(text))
				{
					return string.Empty;
				}
				return text;
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					base.Attributes["action"] = null;
					return;
				}
				base.Attributes["action"] = value;
			}
		}

		/// <summary>Gets or sets the child control of the <see cref="T:System.Web.UI.HtmlControls.HtmlForm" /> control that causes postback when the ENTER key is pressed.</summary>
		/// <returns>The <see cref="P:System.Web.UI.Control.ID" /> of the button control to display as the default button when the <see cref="T:System.Web.UI.HtmlControls.HtmlForm" /> is loaded. The default value is an empty string ("").</returns>
		/// <exception cref="T:System.InvalidOperationException">The control referenced as the default button is not of the type <see cref="T:System.Web.UI.WebControls.IButtonControl" />.</exception>
		// Token: 0x170007CF RID: 1999
		// (get) Token: 0x0600188C RID: 6284 RVA: 0x000420AA File Offset: 0x000402AA
		// (set) Token: 0x0600188D RID: 6285 RVA: 0x000420BB File Offset: 0x000402BB
		[DefaultValue("")]
		public string DefaultButton
		{
			get
			{
				return this._defaultbutton ?? string.Empty;
			}
			set
			{
				this._defaultbutton = value;
			}
		}

		/// <summary>Gets or sets the control on the form to display as the control with input focus when the <see cref="T:System.Web.UI.HtmlControls.HtmlForm" /> control is loaded.</summary>
		/// <returns>The <see cref="P:System.Web.UI.Control.ClientID" /> of the control on the form to display as the control with input focus when the <see cref="T:System.Web.UI.HtmlControls.HtmlForm" /> is loaded. The default value is an empty string ("").</returns>
		// Token: 0x170007D0 RID: 2000
		// (get) Token: 0x0600188E RID: 6286 RVA: 0x000420C4 File Offset: 0x000402C4
		// (set) Token: 0x0600188F RID: 6287 RVA: 0x000420D5 File Offset: 0x000402D5
		[DefaultValue("")]
		public string DefaultFocus
		{
			get
			{
				return this._defaultfocus ?? string.Empty;
			}
			set
			{
				this._defaultfocus = value;
			}
		}

		/// <summary>Gets or sets the encoding type a browser uses when posting the form's data to the server.</summary>
		/// <returns>A string that contains the encoding type. The default value is an empty string (""), indicating that the browser's default content type is used.</returns>
		// Token: 0x170007D1 RID: 2001
		// (get) Token: 0x06001890 RID: 6288 RVA: 0x000420E0 File Offset: 0x000402E0
		// (set) Token: 0x06001891 RID: 6289 RVA: 0x00042108 File Offset: 0x00040308
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string Enctype
		{
			get
			{
				string text = base.Attributes["enctype"];
				if (text == null)
				{
					return string.Empty;
				}
				return text;
			}
			set
			{
				if (value == null)
				{
					base.Attributes.Remove("enctype");
					return;
				}
				base.Attributes["enctype"] = value;
			}
		}

		/// <summary>Gets or sets a value that indicates how a browser posts form data to the server for processing.</summary>
		/// <returns>A string that indicates how a browser posts form data to the server. The default value is POST.</returns>
		// Token: 0x170007D2 RID: 2002
		// (get) Token: 0x06001892 RID: 6290 RVA: 0x00042130 File Offset: 0x00040330
		// (set) Token: 0x06001893 RID: 6291 RVA: 0x00042160 File Offset: 0x00040360
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string Method
		{
			get
			{
				string text = base.Attributes["method"];
				if (text == null || text.Length == 0)
				{
					return "post";
				}
				return text;
			}
			set
			{
				if (value == null)
				{
					base.Attributes.Remove("method");
					return;
				}
				base.Attributes["method"] = value;
			}
		}

		/// <summary>Gets the identifier name for the <see cref="T:System.Web.UI.HtmlControls.HtmlForm" /> control.</summary>
		/// <returns>A string that contains the identifier name for the <see cref="T:System.Web.UI.HtmlControls.HtmlForm" />.</returns>
		// Token: 0x170007D3 RID: 2003
		// (get) Token: 0x06001894 RID: 6292 RVA: 0x00042187 File Offset: 0x00040387
		// (set) Token: 0x06001895 RID: 6293 RVA: 0x0000393A File Offset: 0x00001B3A
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual string Name
		{
			get
			{
				return this.UniqueID;
			}
			set
			{
			}
		}

		/// <summary>Gets or sets a Boolean value indicating whether to force controls disabled on the client to submit their values, allowing them to preserve their values after the page posts back to the server. </summary>
		/// <returns>true if controls disabled on the client are forced to submit their values; otherwise, false. The default value is false.</returns>
		// Token: 0x170007D4 RID: 2004
		// (get) Token: 0x06001896 RID: 6294 RVA: 0x0004218F File Offset: 0x0004038F
		// (set) Token: 0x06001897 RID: 6295 RVA: 0x00042197 File Offset: 0x00040397
		[DefaultValue(false)]
		public virtual bool SubmitDisabledControls
		{
			get
			{
				return this.submitdisabledcontrols;
			}
			set
			{
				this.submitdisabledcontrols = value;
			}
		}

		/// <summary>Gets or sets the frame or window in which to render the results of information that is posted to the server.</summary>
		/// <returns>The browser window or frame that displays the results of the information posted to the server. The default is an empty string (""), which refreshes the window or frame with focus. </returns>
		// Token: 0x170007D5 RID: 2005
		// (get) Token: 0x06001898 RID: 6296 RVA: 0x000421A0 File Offset: 0x000403A0
		// (set) Token: 0x06001899 RID: 6297 RVA: 0x000421C8 File Offset: 0x000403C8
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DefaultValue("")]
		public string Target
		{
			get
			{
				string text = base.Attributes["target"];
				if (text == null)
				{
					return string.Empty;
				}
				return text;
			}
			set
			{
				if (value == null)
				{
					base.Attributes.Remove("target");
					return;
				}
				base.Attributes["target"] = value;
			}
		}

		/// <summary>Gets the unique programmatic identifier assigned to the <see cref="T:System.Web.UI.HtmlControls.HtmlForm" /> control.</summary>
		/// <returns>The unique programmatic identifier assigned to the <see cref="T:System.Web.UI.HtmlControls.HtmlForm" /> control.</returns>
		// Token: 0x170007D6 RID: 2006
		// (get) Token: 0x0600189A RID: 6298 RVA: 0x000421EF File Offset: 0x000403EF
		public override string UniqueID
		{
			get
			{
				if (this.NamingContainer == this.Page)
				{
					return this.ID;
				}
				return "aspnetForm";
			}
		}

		/// <summary>Creates a new <see cref="T:System.Web.UI.ControlCollection" /> collection for the <see cref="T:System.Web.UI.HtmlControls.HtmlForm" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.ControlCollection" /> that contains the <see cref="T:System.Web.UI.HtmlControls.HtmlForm" /> control's child server controls.</returns>
		// Token: 0x0600189B RID: 6299 RVA: 0x0004220B File Offset: 0x0004040B
		[global::System.MonoTODO("why override?")]
		protected override ControlCollection CreateControlCollection()
		{
			return base.CreateControlCollection();
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.Control.Init" /> event for the <see cref="T:System.Web.UI.HtmlControls.HtmlForm" /> control.</summary>
		/// <param name="e">A <see cref="T:System.EventArgs" /> that contains event data.</param>
		// Token: 0x0600189C RID: 6300 RVA: 0x00042214 File Offset: 0x00040414
		protected internal override void OnInit(EventArgs e)
		{
			this.inited = true;
			Page page = this.Page;
			if (page != null)
			{
				page.RegisterViewStateHandler();
				page.RegisterForm(this);
			}
			base.OnInit(e);
		}

		// Token: 0x0600189D RID: 6301 RVA: 0x00042248 File Offset: 0x00040448
		internal bool DetermineRenderUplevel()
		{
			if (this.isUplevel != null)
			{
				return this.isUplevel.Value;
			}
			this.isUplevel = new bool?(UplevelHelper.IsUplevel(HttpCapabilitiesBase.GetUserAgentForDetection(HttpContext.Current.Request)));
			return this.isUplevel.Value;
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event for the <see cref="T:System.Web.UI.HtmlControls.HtmlForm" /> control.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x0600189E RID: 6302 RVA: 0x000419F4 File Offset: 0x0003FBF4
		protected internal override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
		}

		/// <summary>Renders the <see cref="T:System.Web.UI.HtmlControls.HtmlForm" /> control's attributes to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object.</summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> that receives the rendered content. </param>
		/// <exception cref="T:System.InvalidOperationException">The control ID set in the <see cref="P:System.Web.UI.HtmlControls.HtmlForm.DefaultButton" /> property is not of the type <see cref="T:System.Web.UI.WebControls.IButtonControl" />.</exception>
		// Token: 0x0600189F RID: 6303 RVA: 0x00042298 File Offset: 0x00040498
		protected override void RenderAttributes(HtmlTextWriter writer)
		{
			string text = base.Attributes["action"];
			Page page = this.Page;
			HttpRequest httpRequest = ((page != null) ? page.RequestInternal : null);
			string text4;
			if (string.IsNullOrEmpty(text))
			{
				string text2 = ((httpRequest != null) ? httpRequest.ClientFilePath : null);
				string text3 = ((httpRequest != null) ? httpRequest.CurrentExecutionFilePath : null);
				if (text2 == null)
				{
					text4 = this.Action;
				}
				else if (text2 == text3)
				{
					text4 = UrlUtils.GetFile(text2);
				}
				else
				{
					SessionStateSection sessionStateSection = WebConfigurationManager.GetSection("system.web/sessionState") as SessionStateSection;
					bool flag = sessionStateSection != null && sessionStateSection.Cookieless == HttpCookieMode.UseUri;
					string appDomainAppVirtualPath = HttpRuntime.AppDomainAppVirtualPath;
					int length = appDomainAppVirtualPath.Length;
					if (length > 1)
					{
						if (flag)
						{
							if (StrUtils.StartsWith(text2, appDomainAppVirtualPath, true))
							{
								text2 = text2.Substring(length + 1);
							}
						}
						else if (StrUtils.StartsWith(text3, appDomainAppVirtualPath, true))
						{
							text3 = text3.Substring(length + 1);
						}
					}
					if (flag)
					{
						Uri uri = new Uri("http://host" + text3);
						text4 = new Uri("http://host" + text2).MakeRelative(uri);
					}
					else
					{
						text4 = text3;
					}
				}
			}
			else
			{
				text4 = text;
			}
			if (httpRequest != null)
			{
				text4 += httpRequest.QueryStringRaw;
			}
			if (httpRequest != null)
			{
				XhtmlConformanceSection xhtmlConformanceSection = WebConfigurationManager.GetSection("system.web/xhtmlConformance") as XhtmlConformanceSection;
				if ((xhtmlConformanceSection == null || xhtmlConformanceSection.Mode != XhtmlConformanceMode.Strict) && base.RenderingCompatibilityLessThan40)
				{
					writer.WriteAttribute("name", this.Name);
				}
			}
			writer.WriteAttribute("method", this.Method);
			if (string.IsNullOrEmpty(text))
			{
				writer.WriteAttribute("action", text4, true);
			}
			if (this.ID == null)
			{
				string clientID = this.ClientID;
			}
			string text5 = ((page != null) ? page.GetSubmitStatements() : null);
			if (!string.IsNullOrEmpty(text5))
			{
				base.Attributes.Remove("onsubmit");
				writer.WriteAttribute("onsubmit", text5);
			}
			string enctype = this.Enctype;
			if (!string.IsNullOrEmpty(enctype))
			{
				writer.WriteAttribute("enctype", enctype);
			}
			string target = this.Target;
			if (!string.IsNullOrEmpty(target))
			{
				writer.WriteAttribute("target", target);
			}
			string defaultButton = this.DefaultButton;
			if (!string.IsNullOrEmpty(defaultButton))
			{
				Control control = this.FindControl(defaultButton);
				if (control == null || !(control is IButtonControl))
				{
					throw new InvalidOperationException(string.Format("The DefaultButton of '{0}' must be the ID of a control of type IButtonControl.", this.ID));
				}
				if (page != null && this.DetermineRenderUplevel())
				{
					writer.WriteAttribute("onkeypress", string.Concat(new string[] { "javascript:return ", page.WebFormScriptReference, ".WebForm_FireDefaultButton(event, '", control.ClientID, "')" }));
				}
			}
			base.Attributes.Remove("method");
			base.Attributes.Remove("enctype");
			base.Attributes.Remove("target");
			base.RenderAttributes(writer);
		}

		/// <summary>Renders the child controls of the <see cref="T:System.Web.UI.HtmlControls.HtmlForm" /> control.</summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> that receives the rendered content.</param>
		/// <exception cref="T:System.Web.HttpException">The Web page has more than one server-side &lt;form&gt; tag.</exception>
		// Token: 0x060018A0 RID: 6304 RVA: 0x00042574 File Offset: 0x00040774
		protected internal override void RenderChildren(HtmlTextWriter writer)
		{
			Page page = this.Page;
			if (!this.inited && page != null)
			{
				page.RegisterViewStateHandler();
				page.RegisterForm(this);
			}
			if (page != null)
			{
				page.OnFormRender(writer, this.ClientID);
			}
			base.RenderChildren(writer);
			if (page != null)
			{
				page.OnFormPostRender(writer, this.ClientID);
			}
		}

		/// <summary>Renders the <see cref="T:System.Web.UI.HtmlControls.HtmlForm" /> control to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object.</summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> that receives the form control content.</param>
		// Token: 0x060018A1 RID: 6305 RVA: 0x00032AB6 File Offset: 0x00030CB6
		[global::System.MonoTODO("why override?")]
		public override void RenderControl(HtmlTextWriter writer)
		{
			base.RenderControl(writer);
		}

		/// <summary>Renders the <see cref="T:System.Web.UI.HtmlControls.HtmlForm" /> control to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object.</summary>
		/// <param name="output">The <see cref="T:System.Web.UI.HtmlTextWriter" /> that receives the rendered content.</param>
		/// <exception cref="T:System.Web.HttpException">The <see cref="T:System.Web.UI.HtmlControls.HtmlForm" /> control is not rendered without a reference to the <see cref="T:System.Web.UI.Page" /> instance.</exception>
		// Token: 0x060018A2 RID: 6306 RVA: 0x000425C7 File Offset: 0x000407C7
		protected internal override void Render(HtmlTextWriter output)
		{
			base.Render(output);
		}

		// Token: 0x04001626 RID: 5670
		private bool inited;

		// Token: 0x04001627 RID: 5671
		private string _defaultfocus;

		// Token: 0x04001628 RID: 5672
		private string _defaultbutton;

		// Token: 0x04001629 RID: 5673
		private bool submitdisabledcontrols;

		// Token: 0x0400162A RID: 5674
		private bool? isUplevel;
	}
}
