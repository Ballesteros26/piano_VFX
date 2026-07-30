using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.HtmlControls
{
	/// <summary>Defines the methods, properties, and events for all HTML server control elements not represented by a specific .NET Framework class.</summary>
	// Token: 0x0200025B RID: 603
	[ConstructorNeedsTag(true)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class HtmlGenericControl : HtmlContainerControl
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlGenericControl" /> class with default values.</summary>
		// Token: 0x060018A3 RID: 6307 RVA: 0x000425D0 File Offset: 0x000407D0
		public HtmlGenericControl()
			: this("span")
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlGenericControl" /> class with the specified tag.</summary>
		/// <param name="tag">The name of the element for which this instance of the class is created. </param>
		// Token: 0x060018A4 RID: 6308 RVA: 0x000425DD File Offset: 0x000407DD
		public HtmlGenericControl(string tag)
		{
			if (tag == null)
			{
				tag = "";
			}
			this._tagName = tag;
		}

		/// <summary>Gets or sets the name of the HTML element represented by the <see cref="T:System.Web.UI.HtmlControls.HtmlGenericControl" /> control.</summary>
		/// <returns>The tag name of an element.</returns>
		// Token: 0x170007D7 RID: 2007
		// (get) Token: 0x060018A5 RID: 6309 RVA: 0x00042038 File Offset: 0x00040238
		// (set) Token: 0x060018A6 RID: 6310 RVA: 0x000425F6 File Offset: 0x000407F6
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebSysDescription("")]
		[WebCategory("Appearance")]
		public new string TagName
		{
			get
			{
				return this._tagName;
			}
			set
			{
				this._tagName = value;
			}
		}
	}
}
