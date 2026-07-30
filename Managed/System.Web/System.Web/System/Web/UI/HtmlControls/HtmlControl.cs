using System;
using System.ComponentModel;
using System.Globalization;
using System.Security.Permissions;

namespace System.Web.UI.HtmlControls
{
	/// <summary>Defines the methods, properties, and events common to all HTML server controls in the ASP.NET page framework.</summary>
	// Token: 0x02000257 RID: 599
	[Designer("System.Web.UI.Design.HtmlIntrinsicControlDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[ToolboxItem(false)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public abstract class HtmlControl : Control, IAttributeAccessor
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlControl" /> class using default values.</summary>
		// Token: 0x06001872 RID: 6258 RVA: 0x00041EB7 File Offset: 0x000400B7
		protected HtmlControl()
			: this("span")
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlControl" /> class using the specified tag.</summary>
		/// <param name="tag">A string that specifies the tag name of the control. </param>
		// Token: 0x06001873 RID: 6259 RVA: 0x00041EC4 File Offset: 0x000400C4
		protected HtmlControl(string tag)
		{
			this._tagName = tag;
		}

		/// <returns>A <see cref="T:System.Web.UI.ControlCollection" /> that contains the <see cref="T:System.Web.UI.HtmlControls.HtmlControl" /> child server controls.</returns>
		// Token: 0x06001874 RID: 6260 RVA: 0x00032889 File Offset: 0x00030A89
		protected override ControlCollection CreateControlCollection()
		{
			return new EmptyControlCollection(this);
		}

		// Token: 0x06001875 RID: 6261 RVA: 0x00041ED3 File Offset: 0x000400D3
		internal static string AttributeToString(int n)
		{
			if (n != -1)
			{
				return n.ToString(NumberFormatInfo.InvariantInfo);
			}
			return null;
		}

		// Token: 0x06001876 RID: 6262 RVA: 0x00041EE7 File Offset: 0x000400E7
		internal static string AttributeToString(string s)
		{
			if (s != null && s.Length != 0)
			{
				return s;
			}
			return null;
		}

		// Token: 0x06001877 RID: 6263 RVA: 0x00041EF8 File Offset: 0x000400F8
		internal void PreProcessRelativeReference(HtmlTextWriter writer, string attribName)
		{
			string text = this.Attributes[attribName];
			if (text != null && text.Length != 0)
			{
				try
				{
					text = base.ResolveClientUrl(text);
				}
				catch (Exception)
				{
					throw new HttpException(attribName + " property had malformed url");
				}
				writer.WriteAttribute(attribName, text);
				this.Attributes.Remove(attribName);
			}
		}

		/// <summary>Gets the value of the named attribute on the <see cref="T:System.Web.UI.HtmlControls.HtmlControl" /> control.</summary>
		/// <returns>The value of this attribute on the element, as a <see cref="T:System.String" /> value. If the specified attribute does not exist on this element, returns an empty string ("").</returns>
		/// <param name="name">The name of the attribute. This argument is case-insensitive.</param>
		// Token: 0x06001878 RID: 6264 RVA: 0x00041F60 File Offset: 0x00040160
		protected virtual string GetAttribute(string name)
		{
			return this.Attributes[name];
		}

		/// <summary>Sets the value of the named attribute on the <see cref="T:System.Web.UI.HtmlControls.HtmlControl" /> control.</summary>
		/// <param name="name">The name of the attribute to set.</param>
		/// <param name="value">The value to set the attribute to.</param>
		// Token: 0x06001879 RID: 6265 RVA: 0x00041F6E File Offset: 0x0004016E
		protected virtual void SetAttribute(string name, string value)
		{
			this.Attributes[name] = value;
		}

		/// <summary>For a description of this member, see <see cref="M:System.Web.UI.IAttributeAccessor.GetAttribute(System.String)" />. </summary>
		/// <returns>The value of this attribute on the element, as a <see cref="T:System.String" /> value. If the specified attribute does not exist on this element, returns an empty string ("").</returns>
		/// <param name="name">The attribute name.</param>
		// Token: 0x0600187A RID: 6266 RVA: 0x00041F60 File Offset: 0x00040160
		string IAttributeAccessor.GetAttribute(string name)
		{
			return this.Attributes[name];
		}

		/// <summary>For a description of this member, see <see cref="M:System.Web.UI.IAttributeAccessor.SetAttribute(System.String,System.String)" />. </summary>
		/// <param name="name">The name of the attribute to set.</param>
		/// <param name="value">The value to set the attribute to.</param>
		// Token: 0x0600187B RID: 6267 RVA: 0x00041F6E File Offset: 0x0004016E
		void IAttributeAccessor.SetAttribute(string name, string value)
		{
			this.Attributes[name] = value;
		}

		/// <summary>Renders the opening HTML tag of the control into the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object.</summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> that receives the rendered content.</param>
		// Token: 0x0600187C RID: 6268 RVA: 0x00041F7D File Offset: 0x0004017D
		protected virtual void RenderBeginTag(HtmlTextWriter writer)
		{
			writer.WriteBeginTag(this.TagName);
			this.RenderAttributes(writer);
			writer.Write('>');
		}

		/// <summary>Writes content to render on a client to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object.</summary>
		/// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> that contains the output stream to render on the client.</param>
		// Token: 0x0600187D RID: 6269 RVA: 0x00041F9A File Offset: 0x0004019A
		protected internal override void Render(HtmlTextWriter writer)
		{
			this.RenderBeginTag(writer);
		}

		/// <summary>Renders the <see cref="T:System.Web.UI.HtmlControls.HtmlControl" /> control's attributes into the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object.</summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> that receives the rendered content.</param>
		// Token: 0x0600187E RID: 6270 RVA: 0x00041FA3 File Offset: 0x000401A3
		protected virtual void RenderAttributes(HtmlTextWriter writer)
		{
			if (this.ID != null)
			{
				writer.WriteAttribute("id", this.ClientID);
			}
			this.Attributes.Render(writer);
		}

		/// <summary>Gets a collection of all attribute name and value pairs expressed on a server control tag within the ASP.NET page.</summary>
		/// <returns>A <see cref="T:System.Web.UI.AttributeCollection" /> object that contains all attribute name and value pairs expressed on a server control tag within the Web page.</returns>
		// Token: 0x170007C9 RID: 1993
		// (get) Token: 0x0600187F RID: 6271 RVA: 0x00041FCA File Offset: 0x000401CA
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public AttributeCollection Attributes
		{
			get
			{
				if (this._attributes == null)
				{
					this._attributes = new AttributeCollection(this.ViewState);
				}
				return this._attributes;
			}
		}

		/// <summary>Gets or sets a value indicating whether the HTML server control is disabled.</summary>
		/// <returns>true if the control is disabled; otherwise, false. The default value is false.</returns>
		// Token: 0x170007CA RID: 1994
		// (get) Token: 0x06001880 RID: 6272 RVA: 0x00041FEB File Offset: 0x000401EB
		// (set) Token: 0x06001881 RID: 6273 RVA: 0x00042000 File Offset: 0x00040200
		[DefaultValue(false)]
		[TypeConverter(typeof(MinimizableAttributeTypeConverter))]
		[WebCategory("Behavior")]
		[WebSysDescription("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool Disabled
		{
			get
			{
				return this.Attributes["disabled"] != null;
			}
			set
			{
				if (!value)
				{
					this.Attributes.Remove("disabled");
					return;
				}
				this.Attributes["disabled"] = "disabled";
			}
		}

		/// <summary>Gets a collection of all cascading style sheet (CSS) properties applied to a specified HTML server control in the ASP.NET file.</summary>
		/// <returns>A <see cref="T:System.Web.UI.CssStyleCollection" /> object that contains the style properties for the HTML server control.</returns>
		// Token: 0x170007CB RID: 1995
		// (get) Token: 0x06001882 RID: 6274 RVA: 0x0004202B File Offset: 0x0004022B
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public CssStyleCollection Style
		{
			get
			{
				return this.Attributes.CssStyle;
			}
		}

		/// <summary>Gets the element name of a tag that contains a runat=server attribute and value pair.</summary>
		/// <returns>The element name of the specified tag.</returns>
		// Token: 0x170007CC RID: 1996
		// (get) Token: 0x06001883 RID: 6275 RVA: 0x00042038 File Offset: 0x00040238
		[WebSysDescription("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DefaultValue("")]
		[WebCategory("Appearance")]
		public virtual string TagName
		{
			get
			{
				return this._tagName;
			}
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Web.UI.HtmlControls.HtmlControl" /> view state is case-sensitive.</summary>
		/// <returns>true in all cases.</returns>
		// Token: 0x170007CD RID: 1997
		// (get) Token: 0x06001884 RID: 6276 RVA: 0x00008B66 File Offset: 0x00006D66
		protected override bool ViewStateIgnoresCase
		{
			get
			{
				return true;
			}
		}

		// Token: 0x04001624 RID: 5668
		internal string _tagName;

		// Token: 0x04001625 RID: 5669
		private AttributeCollection _attributes;
	}
}
