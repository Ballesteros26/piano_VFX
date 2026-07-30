using System;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Security.Permissions;
using System.Web.Util;

namespace System.Web.UI.HtmlControls
{
	/// <summary>Allows programmatic access to the HTML &lt;input type= button&gt;, &lt;input type= submit&gt;, and &lt;input type= reset&gt; elements on the server.</summary>
	// Token: 0x02000261 RID: 609
	[DefaultEvent("ServerClick")]
	[SupportsEventValidation]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class HtmlInputButton : HtmlInputControl, IPostBackEventHandler
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlInputButton" /> class using default values.</summary>
		// Token: 0x060018CA RID: 6346 RVA: 0x00042CEB File Offset: 0x00040EEB
		public HtmlInputButton()
			: this("button")
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlInputButton" /> class using the specified button type.</summary>
		/// <param name="type">The input button type. </param>
		// Token: 0x060018CB RID: 6347 RVA: 0x00042CF8 File Offset: 0x00040EF8
		public HtmlInputButton(string type)
			: base(type)
		{
		}

		/// <summary>Gets or sets a value indicating whether validation is performed when the <see cref="T:System.Web.UI.HtmlControls.HtmlInputButton" /> control is clicked.</summary>
		/// <returns>true if validation is performed when the <see cref="T:System.Web.UI.HtmlControls.HtmlInputButton" /> control is clicked; otherwise, false. The default value is true.</returns>
		// Token: 0x170007E2 RID: 2018
		// (get) Token: 0x060018CC RID: 6348 RVA: 0x00042D04 File Offset: 0x00040F04
		// (set) Token: 0x060018CD RID: 6349 RVA: 0x00042D2D File Offset: 0x00040F2D
		[WebSysDescription("")]
		[DefaultValue(true)]
		[WebCategory("Behavior")]
		public virtual bool CausesValidation
		{
			get
			{
				string text = base.Attributes["CausesValidation"];
				return text == null || bool.Parse(text);
			}
			set
			{
				base.Attributes["CausesValidation"] = value.ToString();
			}
		}

		/// <summary>Gets or sets the group of controls for which the <see cref="T:System.Web.UI.HtmlControls.HtmlInputButton" /> causes validation when it posts back to the server.</summary>
		/// <returns>The group of controls for which the <see cref="T:System.Web.UI.HtmlControls.HtmlInputButton" /> control causes validation when it posts back to the server. The default value is an empty string (""), indicating that this property is not set. </returns>
		// Token: 0x170007E3 RID: 2019
		// (get) Token: 0x060018CE RID: 6350 RVA: 0x00042D48 File Offset: 0x00040F48
		// (set) Token: 0x060018CF RID: 6351 RVA: 0x00042D70 File Offset: 0x00040F70
		[DefaultValue("")]
		public virtual string ValidationGroup
		{
			get
			{
				string text = base.Attributes["ValidationGroup"];
				if (text == null)
				{
					return "";
				}
				return text;
			}
			set
			{
				if (value == null)
				{
					base.Attributes.Remove("ValidationGroup");
					return;
				}
				base.Attributes["ValidationGroup"] = value;
			}
		}

		// Token: 0x060018D0 RID: 6352 RVA: 0x00042D98 File Offset: 0x00040F98
		private void RaisePostBackEventInternal(string eventArgument)
		{
			base.ValidateEvent(this.UniqueID, eventArgument);
			if (this.CausesValidation)
			{
				this.Page.Validate(this.ValidationGroup);
			}
			if (string.Compare(base.Type, "reset", true, Helpers.InvariantCulture) != 0)
			{
				this.OnServerClick(EventArgs.Empty);
				return;
			}
			this.ResetForm(this.FindForm());
		}

		// Token: 0x060018D1 RID: 6353 RVA: 0x00042DFC File Offset: 0x00040FFC
		private HtmlForm FindForm()
		{
			Page page = this.Page;
			if (page != null)
			{
				return page.Form;
			}
			return null;
		}

		// Token: 0x060018D2 RID: 6354 RVA: 0x00042E1B File Offset: 0x0004101B
		private void ResetForm(HtmlForm form)
		{
			if (form == null || !form.HasControls())
			{
				return;
			}
			this.ResetChildrenValues(form.Controls);
		}

		// Token: 0x060018D3 RID: 6355 RVA: 0x00042E38 File Offset: 0x00041038
		private void ResetChildrenValues(ControlCollection children)
		{
			foreach (object obj in children)
			{
				Control control = (Control)obj;
				if (control != null)
				{
					if (control.HasControls())
					{
						this.ResetChildrenValues(control.Controls);
					}
					this.ResetChildValue(control);
				}
			}
		}

		// Token: 0x060018D4 RID: 6356 RVA: 0x00042EA4 File Offset: 0x000410A4
		private void ResetChildValue(Control child)
		{
			Type type = child.GetType();
			object[] array = type.GetCustomAttributes(false);
			if (array == null || array.Length == 0)
			{
				return;
			}
			string text = null;
			object[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				DefaultPropertyAttribute defaultPropertyAttribute = array2[i] as DefaultPropertyAttribute;
				if (defaultPropertyAttribute != null)
				{
					text = defaultPropertyAttribute.Name;
					break;
				}
			}
			if (text == null || text.Length == 0)
			{
				return;
			}
			PropertyInfo propertyInfo = null;
			try
			{
				propertyInfo = type.GetProperty(text, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
			}
			catch (Exception)
			{
			}
			if (propertyInfo == null || !propertyInfo.CanWrite)
			{
				return;
			}
			array = propertyInfo.GetCustomAttributes(false);
			if (array == null || array.Length == 0)
			{
				return;
			}
			object obj = null;
			array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				DefaultValueAttribute defaultValueAttribute = array2[i] as DefaultValueAttribute;
				if (defaultValueAttribute != null)
				{
					obj = defaultValueAttribute.Value;
					break;
				}
			}
			if (obj == null || propertyInfo.PropertyType != obj.GetType())
			{
				return;
			}
			try
			{
				propertyInfo.SetValue(child, obj, null);
			}
			catch (Exception)
			{
			}
		}

		/// <summary>Raises events for the <see cref="T:System.Web.UI.HtmlControls.HtmlInputButton" /> control when it posts back to the server.</summary>
		/// <param name="eventArgument">The argument for the event.</param>
		// Token: 0x060018D5 RID: 6357 RVA: 0x00042FBC File Offset: 0x000411BC
		protected virtual void RaisePostBackEvent(string eventArgument)
		{
			this.RaisePostBackEventInternal(eventArgument);
		}

		/// <summary>Implements the <see cref="M:System.Web.UI.IPostBackEventHandler.RaisePostBackEvent(System.String)" /> method by calling the <see cref="M:System.Web.UI.HtmlControls.HtmlInputButton.RaisePostBackEvent(System.String)" /> method.</summary>
		/// <param name="eventArgument">A <see cref="T:System.String" /> that represents an optional event argument to be passed to the event handler.</param>
		// Token: 0x060018D6 RID: 6358 RVA: 0x00042FC5 File Offset: 0x000411C5
		void IPostBackEventHandler.RaisePostBackEvent(string eventArgument)
		{
			this.RaisePostBackEvent(eventArgument);
		}

		/// <summary>Raises the <see cref="M:System.Web.UI.Control.OnPreRender(System.EventArgs)" /> event and registers client script for generating postback.</summary>
		/// <param name="e">An <see cref="P:System.Web.UI.Design.ViewEventArgs.EventArgs" /> that contains the event data. </param>
		// Token: 0x060018D7 RID: 6359 RVA: 0x00042FCE File Offset: 0x000411CE
		protected internal override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			if (base.Events[HtmlInputButton.ServerClickEvent] != null)
			{
				this.Page.RequiresPostBackScript();
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.HtmlControls.HtmlInputButton.ServerClick" /> event. This allows you to handle the event directly.</summary>
		/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060018D8 RID: 6360 RVA: 0x00042FF4 File Offset: 0x000411F4
		protected virtual void OnServerClick(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[HtmlInputButton.ServerClickEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Renders the attributes into the specified writer and then calls the <see cref="M:System.Web.UI.HtmlControls.HtmlControl.RenderAttributes(System.Web.UI.HtmlTextWriter)" /> method.</summary>
		/// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter" /> that receives the rendered content.</param>
		// Token: 0x060018D9 RID: 6361 RVA: 0x00043024 File Offset: 0x00041224
		protected override void RenderAttributes(HtmlTextWriter writer)
		{
			CultureInfo invariantCulture = Helpers.InvariantCulture;
			string type = base.Type;
			if (string.Compare(type, "reset", true, invariantCulture) != 0 && (string.Compare(type, "submit", true, invariantCulture) == 0 || (string.Compare(type, "button", true, invariantCulture) == 0 && base.Events[HtmlInputButton.ServerClickEvent] != null)))
			{
				string text = string.Empty;
				if (base.Attributes["onclick"] != null)
				{
					text = ClientScriptManager.EnsureEndsWithSemicolon(base.Attributes["onclick"] + text);
					base.Attributes.Remove("onclick");
				}
				Page page = this.Page;
				if (page != null)
				{
					PostBackOptions postBackOptions = this.GetPostBackOptions();
					text += page.ClientScript.GetPostBackEventReference(postBackOptions, true);
				}
				if (text.Length > 0)
				{
					bool flag = true;
					if (base.Events[HtmlInputButton.ServerClickEvent] != null)
					{
						flag = false;
					}
					writer.WriteAttribute("onclick", text, flag);
					writer.WriteAttribute("language", "javascript");
				}
			}
			base.Attributes.Remove("CausesValidation");
			base.RenderAttributes(writer);
		}

		// Token: 0x060018DA RID: 6362 RVA: 0x00043148 File Offset: 0x00041348
		private PostBackOptions GetPostBackOptions()
		{
			Page page = this.Page;
			PostBackOptions postBackOptions = new PostBackOptions(this);
			postBackOptions.ValidationGroup = null;
			postBackOptions.ActionUrl = null;
			postBackOptions.Argument = string.Empty;
			postBackOptions.RequiresJavaScriptProtocol = false;
			postBackOptions.ClientSubmit = string.Compare(base.Type, "submit", true, Helpers.InvariantCulture) != 0;
			postBackOptions.PerformValidation = this.CausesValidation && page != null && page.Validators.Count > 0;
			if (postBackOptions.PerformValidation)
			{
				postBackOptions.ValidationGroup = this.ValidationGroup;
			}
			return postBackOptions;
		}

		/// <summary>Occurs when an <see cref="T:System.Web.UI.HtmlControls.HtmlInputButton" /> control is clicked on the Web page.</summary>
		// Token: 0x14000038 RID: 56
		// (add) Token: 0x060018DB RID: 6363 RVA: 0x000431D9 File Offset: 0x000413D9
		// (remove) Token: 0x060018DC RID: 6364 RVA: 0x000431EC File Offset: 0x000413EC
		[WebSysDescription("")]
		[WebCategory("Action")]
		public event EventHandler ServerClick
		{
			add
			{
				base.Events.AddHandler(HtmlInputButton.ServerClickEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(HtmlInputButton.ServerClickEvent, value);
			}
		}

		// Token: 0x060018DD RID: 6365 RVA: 0x000431FF File Offset: 0x000413FF
		// Note: this type is marked as 'beforefieldinit'.
		static HtmlInputButton()
		{
			HtmlInputButton.ServerClickEvent = new object();
		}
	}
}
