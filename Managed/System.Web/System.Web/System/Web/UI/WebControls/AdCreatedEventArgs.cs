using System;
using System.Collections;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.AdRotator.AdCreated" /> event of the <see cref="T:System.Web.UI.WebControls.AdRotator" /> control. This class cannot be inherited.</summary>
	// Token: 0x02000330 RID: 816
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class AdCreatedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.AdCreatedEventArgs" /> class.</summary>
		/// <param name="adProperties">A <see cref="T:System.Collections.IDictionary" /> containing the advertisement properties from the XML file. </param>
		// Token: 0x06001C3E RID: 7230 RVA: 0x000469FC File Offset: 0x00044BFC
		public AdCreatedEventArgs(IDictionary adProperties)
		{
			this.properties = adProperties;
			if (this.properties != null)
			{
				this.alt_text = (string)this.properties["AlternateText"];
				this.img_url = (string)this.properties["ImageUrl"];
				this.nav_url = (string)this.properties["NavigateUrl"];
			}
		}

		/// <summary>Gets a <see cref="T:System.Collections.IDictionary" /> object that contains all the advertisement properties for the currently displayed advertisement.</summary>
		/// <returns>A <see cref="T:System.Collections.IDictionary" /> that contains a list of advertisement properties for the currently displayed advertisement. The default value is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x170008AB RID: 2219
		// (get) Token: 0x06001C3F RID: 7231 RVA: 0x00046A6F File Offset: 0x00044C6F
		public IDictionary AdProperties
		{
			get
			{
				return this.properties;
			}
		}

		/// <summary>Gets or sets the alternate text displayed in the <see cref="T:System.Web.UI.WebControls.AdRotator" /> control when the advertisement image is unavailable. Browsers that support the ToolTips feature display this text as a ToolTip for the advertisement.</summary>
		/// <returns>The text displayed in place of the advertisement image if the image is unavailable. The default value is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x170008AC RID: 2220
		// (get) Token: 0x06001C40 RID: 7232 RVA: 0x00046A77 File Offset: 0x00044C77
		// (set) Token: 0x06001C41 RID: 7233 RVA: 0x00046A7F File Offset: 0x00044C7F
		public string AlternateText
		{
			get
			{
				return this.alt_text;
			}
			set
			{
				this.alt_text = value;
			}
		}

		/// <summary>Gets or sets the URL of an image to display in the <see cref="T:System.Web.UI.WebControls.AdRotator" /> control.</summary>
		/// <returns>The URL of an image to display in the <see cref="T:System.Web.UI.WebControls.AdRotator" /> control. The default value is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x170008AD RID: 2221
		// (get) Token: 0x06001C42 RID: 7234 RVA: 0x00046A88 File Offset: 0x00044C88
		// (set) Token: 0x06001C43 RID: 7235 RVA: 0x00046A90 File Offset: 0x00044C90
		public string ImageUrl
		{
			get
			{
				return this.img_url;
			}
			set
			{
				this.img_url = value;
			}
		}

		/// <summary>Gets or sets the Web page to display when the <see cref="T:System.Web.UI.WebControls.AdRotator" /> control is clicked.</summary>
		/// <returns>The Web page to display when the <see cref="T:System.Web.UI.WebControls.AdRotator" /> control is clicked. The default value is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x170008AE RID: 2222
		// (get) Token: 0x06001C44 RID: 7236 RVA: 0x00046A99 File Offset: 0x00044C99
		// (set) Token: 0x06001C45 RID: 7237 RVA: 0x00046AA1 File Offset: 0x00044CA1
		public string NavigateUrl
		{
			get
			{
				return this.nav_url;
			}
			set
			{
				this.nav_url = value;
			}
		}

		// Token: 0x040017EA RID: 6122
		private IDictionary properties;

		// Token: 0x040017EB RID: 6123
		private string alt_text;

		// Token: 0x040017EC RID: 6124
		private string img_url;

		// Token: 0x040017ED RID: 6125
		private string nav_url;
	}
}
