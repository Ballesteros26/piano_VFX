using System;
using System.Collections.Specialized;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents a hidden field used to store a non-displayed value.</summary>
	// Token: 0x020003AC RID: 940
	[Designer("System.Web.UI.Design.WebControls.HiddenFieldDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[ControlValueProperty("Value")]
	[NonVisualControl]
	[ParseChildren]
	[PersistChildren(false)]
	[SupportsEventValidation]
	[DefaultEvent("ValueChanged")]
	[DefaultProperty("Value")]
	public class HiddenField : Control, IPostBackDataHandler
	{
		/// <summary>Occurs when the value of the <see cref="T:System.Web.UI.WebControls.HiddenField" /> control changes between posts to the server.</summary>
		// Token: 0x1400009F RID: 159
		// (add) Token: 0x06002655 RID: 9813 RVA: 0x0006488F File Offset: 0x00062A8F
		// (remove) Token: 0x06002656 RID: 9814 RVA: 0x000648A2 File Offset: 0x00062AA2
		public event EventHandler ValueChanged
		{
			add
			{
				base.Events.AddHandler(HiddenField.ValueChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(HiddenField.ValueChangedEvent, value);
			}
		}

		/// <summary>Gets or sets the value of the hidden field.</summary>
		/// <returns>The value of the hidden field. The default is an empty string ("").</returns>
		// Token: 0x17000C42 RID: 3138
		// (get) Token: 0x06002657 RID: 9815 RVA: 0x000648B5 File Offset: 0x00062AB5
		// (set) Token: 0x06002658 RID: 9816 RVA: 0x000648CC File Offset: 0x00062ACC
		[Bindable(true)]
		[DefaultValue("")]
		public virtual string Value
		{
			get
			{
				return this.ViewState.GetString("Value", string.Empty);
			}
			set
			{
				this.ViewState["Value"] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether themes apply to this control.</summary>
		/// <returns>Always returns false to indicate that this control does not support themes.</returns>
		/// <exception cref="T:System.NotSupportedException">An attempt is made to set this property.</exception>
		// Token: 0x17000C43 RID: 3139
		// (get) Token: 0x06002659 RID: 9817 RVA: 0x00008A69 File Offset: 0x00006C69
		// (set) Token: 0x0600265A RID: 9818 RVA: 0x00003A01 File Offset: 0x00001C01
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DefaultValue(false)]
		public override bool EnableTheming
		{
			get
			{
				return false;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		/// <summary>Gets or sets the skin to apply to the control.</summary>
		/// <returns>Always returns an empty string ("") to indicate that themes are not supported.</returns>
		/// <exception cref="T:System.NotSupportedException">An attempt is made to set this property.</exception>
		// Token: 0x17000C44 RID: 3140
		// (get) Token: 0x0600265B RID: 9819 RVA: 0x0000EE9B File Offset: 0x0000D09B
		// (set) Token: 0x0600265C RID: 9820 RVA: 0x00003A01 File Offset: 0x00001C01
		[DefaultValue("")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string SkinID
		{
			get
			{
				return string.Empty;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		/// <summary>Sets input focus to this control.</summary>
		/// <exception cref="T:System.NotSupportedException">An attempt is made to call this method.</exception>
		// Token: 0x0600265D RID: 9821 RVA: 0x00003A01 File Offset: 0x00001C01
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override void Focus()
		{
			throw new NotSupportedException();
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.HiddenField.ValueChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data. </param>
		// Token: 0x0600265E RID: 9822 RVA: 0x000648E0 File Offset: 0x00062AE0
		protected virtual void OnValueChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[HiddenField.ValueChangedEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Processes postback data for a <see cref="T:System.Web.UI.WebControls.HiddenField" /> control.</summary>
		/// <returns>true if the <see cref="T:System.Web.UI.WebControls.HiddenField" /> control's state changes as a result of the postback; otherwise, false.</returns>
		/// <param name="postDataKey">The key identifier for the control.</param>
		/// <param name="postCollection">The collection of all incoming name values.</param>
		// Token: 0x0600265F RID: 9823 RVA: 0x0006490E File Offset: 0x00062B0E
		protected virtual bool LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			if (this.Value != postCollection[postDataKey])
			{
				this.Value = postCollection[postDataKey];
				return true;
			}
			return false;
		}

		/// <summary>Notifies the ASP.NET application that the state of the <see cref="T:System.Web.UI.WebControls.HiddenField" /> control has changed.</summary>
		// Token: 0x06002660 RID: 9824 RVA: 0x00064934 File Offset: 0x00062B34
		protected virtual void RaisePostDataChangedEvent()
		{
			base.ValidateEvent(this.UniqueID, string.Empty);
			this.OnValueChanged(EventArgs.Empty);
		}

		/// <summary>Creates an <see cref="T:System.Web.UI.EmptyControlCollection" /> object used to indicate that child controls are not allowed.</summary>
		/// <returns>Always returns an <see cref="T:System.Web.UI.EmptyControlCollection" /> object.</returns>
		// Token: 0x06002661 RID: 9825 RVA: 0x00032889 File Offset: 0x00030A89
		protected override ControlCollection CreateControlCollection()
		{
			return new EmptyControlCollection(this);
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
		// Token: 0x06002662 RID: 9826 RVA: 0x000419F4 File Offset: 0x0003FBF4
		protected internal override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
		}

		/// <summary>Renders the Web server control content to the client's browser using the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object.</summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> object used to render the server control content on the client's browser. </param>
		// Token: 0x06002663 RID: 9827 RVA: 0x00064954 File Offset: 0x00062B54
		protected internal override void Render(HtmlTextWriter writer)
		{
			Page page = this.Page;
			string uniqueID = this.UniqueID;
			if (page != null)
			{
				page.ClientScript.RegisterForEventValidation(uniqueID);
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Type, "hidden", false);
			if (!string.IsNullOrEmpty(this.ClientID))
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID);
			}
			if (!string.IsNullOrEmpty(uniqueID))
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Name, uniqueID);
			}
			if (!string.IsNullOrEmpty(this.Value))
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Value, this.Value);
			}
			writer.RenderBeginTag(HtmlTextWriterTag.Input);
			writer.RenderEndTag();
		}

		/// <summary>For a description of this member, see the <see cref="M:System.Web.UI.IPostBackDataHandler.LoadPostData(System.String,System.Collections.Specialized.NameValueCollection)" /> method.</summary>
		/// <returns>true if the server control's state changes as a result of the postback; otherwise, false.</returns>
		/// <param name="postDataKey">The key identifier for the control.</param>
		/// <param name="postCollection">The collection of all incoming name values.</param>
		// Token: 0x06002664 RID: 9828 RVA: 0x000649E1 File Offset: 0x00062BE1
		bool IPostBackDataHandler.LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			return this.LoadPostData(postDataKey, postCollection);
		}

		/// <summary>For a description of this member, see the <see cref="M:System.Web.UI.IPostBackDataHandler.RaisePostDataChangedEvent" /> method.</summary>
		// Token: 0x06002665 RID: 9829 RVA: 0x000649EB File Offset: 0x00062BEB
		void IPostBackDataHandler.RaisePostDataChangedEvent()
		{
			this.RaisePostDataChangedEvent();
		}

		// Token: 0x06002667 RID: 9831 RVA: 0x000649F3 File Offset: 0x00062BF3
		// Note: this type is marked as 'beforefieldinit'.
		static HiddenField()
		{
			HiddenField.ValueChangedEvent = new object();
		}
	}
}
