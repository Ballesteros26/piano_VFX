using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents a radio button control.</summary>
	// Token: 0x020003F8 RID: 1016
	[Designer("System.Web.UI.Design.WebControls.CheckBoxDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[SupportsEventValidation]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class RadioButton : CheckBox, IPostBackDataHandler
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.RadioButton" /> class.</summary>
		// Token: 0x06002CD3 RID: 11475 RVA: 0x00076FBF File Offset: 0x000751BF
		public RadioButton()
			: base("radio")
		{
		}

		/// <summary>Gets or sets the name of the group that the radio button belongs to.</summary>
		/// <returns>The name of the group that the radio button belongs to. The default is an empty string ("").</returns>
		// Token: 0x17000E47 RID: 3655
		// (get) Token: 0x06002CD4 RID: 11476 RVA: 0x00076FCC File Offset: 0x000751CC
		// (set) Token: 0x06002CD5 RID: 11477 RVA: 0x00076FE3 File Offset: 0x000751E3
		[WebCategory("Behavior")]
		[WebSysDescription("")]
		[Themeable(false)]
		[DefaultValue("")]
		public virtual string GroupName
		{
			get
			{
				return this.ViewState.GetString("GroupName", string.Empty);
			}
			set
			{
				this.ViewState["GroupName"] = value;
			}
		}

		// Token: 0x17000E48 RID: 3656
		// (get) Token: 0x06002CD6 RID: 11478 RVA: 0x00076FF8 File Offset: 0x000751F8
		internal override string NameAttribute
		{
			get
			{
				string uniqueID = this.UniqueID;
				string groupName = this.GroupName;
				if (groupName.Length == 0)
				{
					return uniqueID;
				}
				int num = -1;
				if (uniqueID != null)
				{
					num = uniqueID.LastIndexOf(base.IdSeparator);
				}
				if (num == -1)
				{
					return groupName;
				}
				return uniqueID.Substring(0, num + 1) + groupName;
			}
		}

		// Token: 0x17000E49 RID: 3657
		// (get) Token: 0x06002CD7 RID: 11479 RVA: 0x00077048 File Offset: 0x00075248
		// (set) Token: 0x06002CD8 RID: 11480 RVA: 0x000648CC File Offset: 0x00062ACC
		internal string ValueAttribute
		{
			get
			{
				string text = (string)this.ViewState["Value"];
				if (text != null)
				{
					return text;
				}
				string id = this.ID;
				if (!string.IsNullOrEmpty(id))
				{
					return id;
				}
				return this.UniqueID;
			}
			set
			{
				this.ViewState["Value"] = value;
			}
		}

		// Token: 0x06002CD9 RID: 11481 RVA: 0x00077088 File Offset: 0x00075288
		internal override void InternalAddAttributesToRender(HtmlTextWriter w, bool enabled)
		{
			Page page = this.Page;
			if (page != null)
			{
				page.ClientScript.RegisterForEventValidation(this.NameAttribute, this.ValueAttribute);
			}
			base.InternalAddAttributesToRender(w, enabled);
			w.AddAttribute(HtmlTextWriterAttribute.Value, this.ValueAttribute);
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06002CDA RID: 11482 RVA: 0x000770CC File Offset: 0x000752CC
		protected internal override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
		}

		/// <summary>Processes postback data for the <see cref="T:System.Web.UI.WebControls.RadioButton" /> control.</summary>
		/// <returns>true if the data for the <see cref="T:System.Web.UI.WebControls.RadioButton" /> has changed; otherwise, false.</returns>
		/// <param name="postDataKey">The key identifier for the control.</param>
		/// <param name="postCollection">The collection of all incoming name values.</param>
		// Token: 0x06002CDB RID: 11483 RVA: 0x000770D8 File Offset: 0x000752D8
		protected override bool LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			string text = postCollection[this.NameAttribute];
			bool flag = text == this.ValueAttribute;
			base.ValidateEvent(this.NameAttribute, text);
			if (this.Checked == flag)
			{
				return false;
			}
			this.Checked = flag;
			return flag;
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.RadioButton.CheckedChanged" /> event, if the <see cref="P:System.Windows.Forms.RadioButton.Checked" /> property has changed on postback.</summary>
		// Token: 0x06002CDC RID: 11484 RVA: 0x0007711F File Offset: 0x0007531F
		protected override void RaisePostDataChangedEvent()
		{
			if (this.CausesValidation)
			{
				this.Page.Validate(this.ValidationGroup);
			}
			this.OnCheckedChanged(EventArgs.Empty);
		}

		/// <summary>For a description of this member, see <see cref="M:System.Web.UI.IPostBackDataHandler.LoadPostData(System.String,System.Collections.Specialized.NameValueCollection)" />.</summary>
		/// <returns>true if <see cref="T:System.Web.UI.WebControls.RadioButton" /> is checked; otherwise, false. The default is false.</returns>
		/// <param name="postDataKey">A string.</param>
		/// <param name="postCollection">A name value collection that represents the posted collection of data. </param>
		// Token: 0x06002CDD RID: 11485 RVA: 0x0004EFEB File Offset: 0x0004D1EB
		bool IPostBackDataHandler.LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			return this.LoadPostData(postDataKey, postCollection);
		}
	}
}
