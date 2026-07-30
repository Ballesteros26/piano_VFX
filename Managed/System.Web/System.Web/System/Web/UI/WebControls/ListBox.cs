using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Security.Permissions;
using System.Web.Util;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents a list box control that allows single or multiple item selection.</summary>
	// Token: 0x020003BF RID: 959
	[ValidationProperty("SelectedItem")]
	[SupportsEventValidation]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class ListBox : ListControl, IPostBackDataHandler
	{
		/// <summary>Gets or sets the border color of the control.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> object that represents the border color of the control.</returns>
		// Token: 0x17000C97 RID: 3223
		// (get) Token: 0x06002792 RID: 10130 RVA: 0x0005CFEE File Offset: 0x0005B1EE
		// (set) Token: 0x06002793 RID: 10131 RVA: 0x0005CFF6 File Offset: 0x0005B1F6
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
		// Token: 0x17000C98 RID: 3224
		// (get) Token: 0x06002794 RID: 10132 RVA: 0x0005CFFF File Offset: 0x0005B1FF
		// (set) Token: 0x06002795 RID: 10133 RVA: 0x0005D007 File Offset: 0x0005B207
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
		/// <returns>A <see cref="T:System.Web.UI.WebControls.Unit" /> object that represents the border width of the control.</returns>
		// Token: 0x17000C99 RID: 3225
		// (get) Token: 0x06002796 RID: 10134 RVA: 0x0005D010 File Offset: 0x0005B210
		// (set) Token: 0x06002797 RID: 10135 RVA: 0x0005D018 File Offset: 0x0005B218
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

		/// <summary>Gets or sets the number of rows displayed in the <see cref="T:System.Web.UI.WebControls.ListBox" /> control.</summary>
		/// <returns>The number of rows displayed in the <see cref="T:System.Web.UI.WebControls.ListBox" /> control. The default value is 4.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified number of rows is less than one or greater than 2000. </exception>
		// Token: 0x17000C9A RID: 3226
		// (get) Token: 0x06002798 RID: 10136 RVA: 0x00066EEE File Offset: 0x000650EE
		// (set) Token: 0x06002799 RID: 10137 RVA: 0x00066F01 File Offset: 0x00065101
		[WebCategory("Appearance")]
		[DefaultValue(4)]
		[WebSysDescription("")]
		public virtual int Rows
		{
			get
			{
				return this.ViewState.GetInt("Rows", 4);
			}
			set
			{
				if (value < 1 || value > 2000)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.ViewState["Rows"] = value;
			}
		}

		/// <summary>Gets or sets the selection mode of the <see cref="T:System.Web.UI.WebControls.ListBox" /> control.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.ListSelectionMode" /> values. The default value is Single.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified selection mode is not one of the <see cref="T:System.Web.UI.WebControls.ListSelectionMode" /> values. </exception>
		// Token: 0x17000C9B RID: 3227
		// (get) Token: 0x0600279A RID: 10138 RVA: 0x00066F30 File Offset: 0x00065130
		// (set) Token: 0x0600279B RID: 10139 RVA: 0x00066F43 File Offset: 0x00065143
		[DefaultValue(ListSelectionMode.Single)]
		[WebSysDescription("")]
		[WebCategory("Behavior")]
		public virtual ListSelectionMode SelectionMode
		{
			get
			{
				return (ListSelectionMode)this.ViewState.GetInt("SelectionMode", 0);
			}
			set
			{
				if (!Enum.IsDefined(typeof(ListSelectionMode), value))
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.ViewState["SelectionMode"] = value;
			}
		}

		/// <summary>Gets the array of index values for currently selected items in the <see cref="T:System.Web.UI.WebControls.ListBox" /> control.</summary>
		/// <returns>An array of integers, each representing the index of a selected item in the list box.</returns>
		// Token: 0x0600279C RID: 10140 RVA: 0x00066F7D File Offset: 0x0006517D
		public virtual int[] GetSelectedIndices()
		{
			return (int[])base.GetSelectedIndicesInternal().ToArray(typeof(int));
		}

		/// <summary>Adds name, size, multiple, and onchange to the list of attributes to render.</summary>
		/// <param name="writer">The output stream that renders HTML content to the client. </param>
		// Token: 0x0600279D RID: 10141 RVA: 0x00066F9C File Offset: 0x0006519C
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			if (this.Page != null)
			{
				this.Page.VerifyRenderingInServerForm(this);
			}
			if (this.ID != null)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Name, this.UniqueID);
			}
			if (this.AutoPostBack)
			{
				string text = this.Page.ClientScript.GetPostBackEventReference(this.GetPostBackOptions(), true);
				text = "setTimeout('" + text.Replace("\\", "\\\\").Replace("'", "\\'") + "', 0)";
				writer.AddAttribute(HtmlTextWriterAttribute.Onchange, base.BuildScriptAttribute("onchange", text));
			}
			if (this.SelectionMode == ListSelectionMode.Multiple)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Multiple, "multiple", false);
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Size, this.Rows.ToString(Helpers.InvariantCulture));
			base.AddAttributesToRender(writer);
		}

		// Token: 0x0600279E RID: 10142 RVA: 0x00067070 File Offset: 0x00065270
		private PostBackOptions GetPostBackOptions()
		{
			PostBackOptions postBackOptions = new PostBackOptions(this);
			postBackOptions.ActionUrl = null;
			postBackOptions.ValidationGroup = null;
			postBackOptions.Argument = string.Empty;
			postBackOptions.RequiresJavaScriptProtocol = false;
			postBackOptions.ClientSubmit = true;
			postBackOptions.PerformValidation = this.CausesValidation && this.Page != null && this.Page.AreValidatorsUplevel(this.ValidationGroup);
			if (postBackOptions.PerformValidation)
			{
				postBackOptions.ValidationGroup = this.ValidationGroup;
			}
			return postBackOptions;
		}

		/// <summary>Configures the <see cref="T:System.Web.UI.WebControls.ListBox" /> control prior to rendering on the client.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
		// Token: 0x0600279F RID: 10143 RVA: 0x000670EC File Offset: 0x000652EC
		protected internal override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			Page page = this.Page;
			if (page != null && base.IsEnabled)
			{
				page.RegisterRequiresPostBack(this);
			}
		}

		/// <summary>Loads the posted content of the list control, if it is different from the last posting.</summary>
		/// <returns>true if the posted content is different from the last posting; otherwise, false.</returns>
		/// <param name="postDataKey">The key identifier for the control, used to index the <paramref name="postCollection" />.</param>
		/// <param name="postCollection">A <see cref="T:System.Collections.Specialized.NameValueCollection" /> that contains value information indexed by control identifiers. </param>
		// Token: 0x060027A0 RID: 10144 RVA: 0x0006711C File Offset: 0x0006531C
		protected virtual bool LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			this.EnsureDataBound();
			string[] values = postCollection.GetValues(postDataKey);
			if (values == null || values.Length == 0)
			{
				int selectedIndex = this.SelectedIndex;
				this.SelectedIndex = -1;
				return selectedIndex != -1;
			}
			base.ValidateEvent(this.UniqueID, values[0]);
			if (this.SelectionMode == ListSelectionMode.Single)
			{
				return this.SelectSingle(values);
			}
			return this.SelectMultiple(values);
		}

		// Token: 0x060027A1 RID: 10145 RVA: 0x00067178 File Offset: 0x00065378
		private bool SelectSingle(string[] values)
		{
			string text = values[0];
			int num = this.Items.IndexOf(text);
			int selectedIndex = this.SelectedIndex;
			if (num != selectedIndex)
			{
				this.SelectedIndex = num;
				return true;
			}
			return false;
		}

		// Token: 0x060027A2 RID: 10146 RVA: 0x000671AC File Offset: 0x000653AC
		private bool SelectMultiple(string[] values)
		{
			ArrayList selectedIndicesInternal = base.GetSelectedIndicesInternal();
			this.ClearSelection();
			foreach (string text in values)
			{
				ListItem listItem = this.Items.FindByValue(text);
				if (listItem != null)
				{
					listItem.Selected = true;
				}
			}
			ArrayList selectedIndicesInternal2 = base.GetSelectedIndicesInternal();
			int num = selectedIndicesInternal.Count;
			if (selectedIndicesInternal2.Count != num)
			{
				return true;
			}
			while (--num >= 0)
			{
				if ((int)selectedIndicesInternal[num] != (int)selectedIndicesInternal2[num])
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Invokes the <see cref="M:System.Web.UI.WebControls.ListControl.OnSelectedIndexChanged(System.EventArgs)" /> method whenever posted data for the <see cref="T:System.Web.UI.WebControls.ListBox" /> control has changed.</summary>
		// Token: 0x060027A3 RID: 10147 RVA: 0x00067239 File Offset: 0x00065439
		protected virtual void RaisePostDataChangedEvent()
		{
			if (this.CausesValidation)
			{
				this.Page.Validate(this.ValidationGroup);
			}
			this.OnSelectedIndexChanged(EventArgs.Empty);
		}

		/// <summary>Loads the posted content of the list control, if it is different from the last posting.</summary>
		/// <returns>true if the posted content is different from the last posting; otherwise, false.</returns>
		/// <param name="postDataKey">The index within the posted collection that references the content to load. </param>
		/// <param name="postCollection">The collection posted to the server. </param>
		// Token: 0x060027A4 RID: 10148 RVA: 0x0006725F File Offset: 0x0006545F
		bool IPostBackDataHandler.LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			return this.LoadPostData(postDataKey, postCollection);
		}

		/// <summary>Invokes the <see cref="M:System.Web.UI.WebControls.ListControl.OnSelectedIndexChanged(System.EventArgs)" /> method whenever posted data for the <see cref="T:System.Web.UI.WebControls.ListBox" /> control has changed.</summary>
		// Token: 0x060027A5 RID: 10149 RVA: 0x00067269 File Offset: 0x00065469
		void IPostBackDataHandler.RaisePostDataChangedEvent()
		{
			this.RaisePostDataChangedEvent();
		}

		// Token: 0x060027A6 RID: 10150 RVA: 0x00067271 File Offset: 0x00065471
		internal override bool MultiSelectOk()
		{
			return this.SelectionMode == ListSelectionMode.Multiple;
		}
	}
}
