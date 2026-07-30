using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.HtmlControls
{
	/// <summary>Serves as the abstract base class that defines the methods, properties, and events common to all HTML input controls, such as the &lt;input type=text&gt;, &lt;input type=submit&gt;, and &lt;input type= file&gt; elements.</summary>
	// Token: 0x02000263 RID: 611
	[ControlBuilder(typeof(HtmlEmptyTagControlBuilder))]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public abstract class HtmlInputControl : HtmlControl
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlInputControl" /> class.</summary>
		/// <param name="type">The type of input control. </param>
		// Token: 0x060018ED RID: 6381 RVA: 0x0004339A File Offset: 0x0004159A
		protected HtmlInputControl(string type)
			: base("input")
		{
			if (type == null)
			{
				type = string.Empty;
			}
			base.Attributes["type"] = type;
		}

		/// <summary>Gets or sets the unique identifier name for the <see cref="T:System.Web.UI.HtmlControls.HtmlInputControl" /> control.</summary>
		/// <returns>A string that represents the value of the <see cref="P:System.Web.UI.Control.UniqueID" /> property.</returns>
		// Token: 0x170007E5 RID: 2021
		// (get) Token: 0x060018EE RID: 6382 RVA: 0x00042187 File Offset: 0x00040387
		// (set) Token: 0x060018EF RID: 6383 RVA: 0x0000393A File Offset: 0x00001B3A
		[DefaultValue("")]
		[WebCategory("Behavior")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebSysDescription("")]
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

		/// <summary>Gets the type of an <see cref="T:System.Web.UI.HtmlControls.HtmlInputControl" />.</summary>
		/// <returns>A string that contains the type of an <see cref="T:System.Web.UI.HtmlControls.HtmlInputControl" />.</returns>
		// Token: 0x170007E6 RID: 2022
		// (get) Token: 0x060018F0 RID: 6384 RVA: 0x000433C2 File Offset: 0x000415C2
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebSysDescription("")]
		[WebCategory("Behavior")]
		[DefaultValue("")]
		public string Type
		{
			get
			{
				return base.Attributes["type"];
			}
		}

		/// <summary>Gets or sets the value associated with the <see cref="T:System.Web.UI.HtmlControls.HtmlInputControl" /> control.</summary>
		/// <returns>The value associated with the <see cref="T:System.Web.UI.HtmlControls.HtmlInputControl" />.</returns>
		// Token: 0x170007E7 RID: 2023
		// (get) Token: 0x060018F1 RID: 6385 RVA: 0x000433D4 File Offset: 0x000415D4
		// (set) Token: 0x060018F2 RID: 6386 RVA: 0x000433FC File Offset: 0x000415FC
		[DefaultValue("")]
		[WebSysDescription("")]
		[WebCategory("Appearance")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual string Value
		{
			get
			{
				string text = base.Attributes["value"];
				if (text != null)
				{
					return text;
				}
				return string.Empty;
			}
			set
			{
				if (value == null)
				{
					base.Attributes.Remove("value");
					return;
				}
				base.Attributes["value"] = value;
			}
		}

		/// <summary>Renders the attributes of the <see cref="T:System.Web.UI.HtmlControls.HtmlInputControl" /> control to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object.</summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> that contains the output stream to render to the client.</param>
		// Token: 0x060018F3 RID: 6387 RVA: 0x00043423 File Offset: 0x00041623
		protected override void RenderAttributes(HtmlTextWriter writer)
		{
			if (base.Attributes["name"] == null)
			{
				writer.WriteAttribute("name", this.Name);
			}
			base.RenderAttributes(writer);
			writer.Write(" /");
		}
	}
}
