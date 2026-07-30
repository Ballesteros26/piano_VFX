using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing.Design;
using System.Security.Permissions;
using System.Web.Util;
using System.Xml;

namespace System.Web.UI.WebControls
{
	/// <summary>Displays an advertisement banner on a Web page.</summary>
	// Token: 0x02000331 RID: 817
	[DefaultProperty("AdvertisementFile")]
	[Designer("System.Web.UI.Design.WebControls.AdRotatorDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[DefaultEvent("AdCreated")]
	[ToolboxData("<{0}:AdRotator runat=\"server\"></{0}:AdRotator>")]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class AdRotator : DataBoundControl
	{
		/// <summary>Raises the <see cref="E:System.Web.UI.Control.Init" /> event.</summary>
		/// <param name="e">The event arguments.</param>
		// Token: 0x06001C46 RID: 7238 RVA: 0x00046AAA File Offset: 0x00044CAA
		protected internal override void OnInit(EventArgs e)
		{
			base.OnInit(e);
		}

		/// <summary>Gets the advertisement information for rendering by looking up the file data or calling the user event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> object that contains event data.</param>
		// Token: 0x06001C47 RID: 7239 RVA: 0x00046AB4 File Offset: 0x00044CB4
		protected internal override void OnPreRender(EventArgs e)
		{
			Hashtable hashtable = null;
			if (!string.IsNullOrEmpty(this.ad_file))
			{
				this.ReadAdsFromFile(base.GetPhysicalFilePath(this.ad_file));
				hashtable = this.ChooseAd();
			}
			AdCreatedEventArgs adCreatedEventArgs = new AdCreatedEventArgs(hashtable);
			this.OnAdCreated(adCreatedEventArgs);
			this.createdargs = adCreatedEventArgs;
		}

		/// <summary>Binds the specified data source to the <see cref="T:System.Web.UI.WebControls.AdRotator" /> control.</summary>
		/// <param name="data">An object that represents the data source; the object must implement the <see cref="T:System.Collections.IEnumerable" /> interface.</param>
		// Token: 0x06001C48 RID: 7240 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoTODO("Not implemented")]
		protected internal override void PerformDataBinding(IEnumerable data)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the advertisement data from the associated data source.</summary>
		// Token: 0x06001C49 RID: 7241 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoTODO("Not implemented")]
		protected override void PerformSelect()
		{
			throw new NotImplementedException();
		}

		/// <summary>Displays the <see cref="T:System.Web.UI.WebControls.AdRotator" /> control on the client.</summary>
		/// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> that contains the output stream to render on the client. </param>
		// Token: 0x06001C4A RID: 7242 RVA: 0x00046B00 File Offset: 0x00044D00
		protected internal override void Render(HtmlTextWriter writer)
		{
			AdCreatedEventArgs adCreatedEventArgs = this.createdargs;
			base.AddAttributesToRender(writer);
			if (adCreatedEventArgs.NavigateUrl != null && adCreatedEventArgs.NavigateUrl.Length > 0)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Href, this.ResolveAdUrl(adCreatedEventArgs.NavigateUrl));
			}
			if (this.Target != null && this.Target.Length > 0)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Target, this.Target);
			}
			writer.RenderBeginTag(HtmlTextWriterTag.A);
			if (adCreatedEventArgs.ImageUrl != null && adCreatedEventArgs.ImageUrl.Length > 0)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Src, this.ResolveAdUrl(adCreatedEventArgs.ImageUrl));
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Alt, (adCreatedEventArgs.AlternateText == null) ? string.Empty : adCreatedEventArgs.AlternateText);
			writer.AddAttribute(HtmlTextWriterAttribute.Border, "0", false);
			writer.RenderBeginTag(HtmlTextWriterTag.Img);
			writer.RenderEndTag();
			writer.RenderEndTag();
		}

		// Token: 0x06001C4B RID: 7243 RVA: 0x00046BD8 File Offset: 0x00044DD8
		private string ResolveAdUrl(string url)
		{
			if (this.AdvertisementFile != null && this.AdvertisementFile.Length > 0 && url[0] != '/' && url[0] != '~')
			{
				try
				{
					new Uri(url);
				}
				catch
				{
					return UrlUtils.Combine(UrlUtils.GetDirectory(base.ResolveUrl(this.AdvertisementFile)), url);
				}
			}
			return base.ResolveUrl(url);
		}

		// Token: 0x06001C4C RID: 7244 RVA: 0x00046C50 File Offset: 0x00044E50
		private Hashtable ChooseAd()
		{
			string keywordFilter = this.KeywordFilter;
			int num = 0;
			int num2 = 0;
			bool flag = keywordFilter.Length == 0;
			foreach (object obj in this.ads)
			{
				Hashtable hashtable = (Hashtable)obj;
				if (flag || keywordFilter == (string)hashtable["Keyword"])
				{
					num += ((hashtable["Impressions"] != null) ? int.Parse((string)hashtable["Impressions"]) : 1);
				}
			}
			int num3 = new Random().Next(num);
			foreach (object obj2 in this.ads)
			{
				Hashtable hashtable2 = (Hashtable)obj2;
				if (flag || !(keywordFilter != (string)hashtable2["Keyword"]))
				{
					num2 += ((hashtable2["Impressions"] != null) ? int.Parse((string)hashtable2["Impressions"]) : 1);
					if (num2 > num3)
					{
						return hashtable2;
					}
				}
			}
			if (num != 0)
			{
				throw new Exception("I should only get here if no ads matched");
			}
			return null;
		}

		// Token: 0x06001C4D RID: 7245 RVA: 0x00046DC0 File Offset: 0x00044FC0
		private void ReadAdsFromFile(string s)
		{
			XmlDocument xmlDocument = new XmlDocument();
			try
			{
				xmlDocument.Load(s);
			}
			catch (Exception ex)
			{
				throw new HttpException("AdRotator could not parse the xml file", ex);
			}
			this.ads.Clear();
			foreach (object obj in xmlDocument.DocumentElement.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				Hashtable hashtable = new Hashtable();
				foreach (object obj2 in xmlNode.ChildNodes)
				{
					XmlNode xmlNode2 = (XmlNode)obj2;
					hashtable.Add(xmlNode2.Name, xmlNode2.InnerText);
				}
				this.ads.Add(hashtable);
			}
		}

		/// <summary>Gets or sets the path to an XML file that contains advertisement information.</summary>
		/// <returns>The location of an XML file containing advertisement information. The default value is an empty string ("").</returns>
		// Token: 0x170008AF RID: 2223
		// (get) Token: 0x06001C4E RID: 7246 RVA: 0x00046EBC File Offset: 0x000450BC
		// (set) Token: 0x06001C4F RID: 7247 RVA: 0x00046EC4 File Offset: 0x000450C4
		[WebCategory("Behavior")]
		[Editor("System.Web.UI.Design.XmlUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[DefaultValue("")]
		[WebSysDescription("")]
		[UrlProperty]
		[Bindable(true)]
		public string AdvertisementFile
		{
			get
			{
				return this.ad_file;
			}
			set
			{
				this.ad_file = value;
			}
		}

		/// <summary>Gets or sets a custom data field to use in place of the AlternateText attribute for an advertisement.</summary>
		/// <returns>The name that identifies the field where the alternate text for an advertisement is stored. The default value is "AlternateText."</returns>
		// Token: 0x170008B0 RID: 2224
		// (get) Token: 0x06001C50 RID: 7248 RVA: 0x00003A1F File Offset: 0x00001C1F
		// (set) Token: 0x06001C51 RID: 7249 RVA: 0x00003A1F File Offset: 0x00001C1F
		[DefaultValue("AlternateText")]
		[WebSysDescription("")]
		[WebCategory("Behavior")]
		[global::System.MonoTODO("Not implemented")]
		public string AlternateTextField
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the font properties associated with the advertisement banner control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.FontInfo" /> object that represents the font properties of the control.</returns>
		// Token: 0x170008B1 RID: 2225
		// (get) Token: 0x06001C52 RID: 7250 RVA: 0x00046ECD File Offset: 0x000450CD
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override FontInfo Font
		{
			get
			{
				return base.Font;
			}
		}

		/// <summary>Gets or sets a custom data field to use in place of the ImageUrl attribute for an advertisement.</summary>
		/// <returns>The name that identifies the field where the URL for the image displayed for an advertisement is stored. The default value is "ImageUrl."</returns>
		// Token: 0x170008B2 RID: 2226
		// (get) Token: 0x06001C53 RID: 7251 RVA: 0x00003A1F File Offset: 0x00001C1F
		// (set) Token: 0x06001C54 RID: 7252 RVA: 0x00003A1F File Offset: 0x00001C1F
		[WebSysDescription("")]
		[WebCategory("Behavior")]
		[DefaultValue("ImageUrl")]
		[global::System.MonoTODO("Not implemented")]
		public string ImageUrlField
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets a category keyword to filter for specific types of advertisements in the XML advertisement file.</summary>
		/// <returns>The keyword to filter for specific types of advertisements in the XML advertisement file. The default value is an empty string ("").</returns>
		// Token: 0x170008B3 RID: 2227
		// (get) Token: 0x06001C55 RID: 7253 RVA: 0x00046ED5 File Offset: 0x000450D5
		// (set) Token: 0x06001C56 RID: 7254 RVA: 0x00046EEC File Offset: 0x000450EC
		[Bindable(true)]
		[WebSysDescription("")]
		[WebCategory("Behavior")]
		[DefaultValue("")]
		public string KeywordFilter
		{
			get
			{
				return this.ViewState.GetString("KeywordFilter", string.Empty);
			}
			set
			{
				this.ViewState["KeywordFilter"] = value;
			}
		}

		/// <summary>Gets or sets a custom data field to use in place of the NavigateUrl attribute for an advertisement.</summary>
		/// <returns>The name that identifies the field containing the URL for the page to navigate to when the <see cref="T:System.Web.UI.WebControls.AdRotator" /> control is clicked. The default value is "NavigateUrl."</returns>
		// Token: 0x170008B4 RID: 2228
		// (get) Token: 0x06001C57 RID: 7255 RVA: 0x00003A1F File Offset: 0x00001C1F
		// (set) Token: 0x06001C58 RID: 7256 RVA: 0x00003A1F File Offset: 0x00001C1F
		[DefaultValue("NavigateUrl")]
		[global::System.MonoTODO("Not implemented")]
		[WebSysDescription("")]
		[WebCategory("Behavior")]
		public string NavigateUrlField
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets the name of the browser window or frame that displays the contents of the Web page linked to when the <see cref="T:System.Web.UI.WebControls.AdRotator" /> control is clicked.</summary>
		/// <returns>The browser window or frame that displays the contents of the Web page linked to when the <see cref="T:System.Web.UI.WebControls.AdRotator" /> control is clicked. The default value is an empty string (""), which refreshes the window or frame with focus.</returns>
		// Token: 0x170008B5 RID: 2229
		// (get) Token: 0x06001C59 RID: 7257 RVA: 0x00046EFF File Offset: 0x000450FF
		// (set) Token: 0x06001C5A RID: 7258 RVA: 0x00046F16 File Offset: 0x00045116
		[WebCategory("Behavior")]
		[Bindable(true)]
		[DefaultValue("_top")]
		[TypeConverter(typeof(TargetConverter))]
		[WebSysDescription("")]
		public string Target
		{
			get
			{
				return this.ViewState.GetString("Target", "_top");
			}
			set
			{
				this.ViewState["Target"] = value;
			}
		}

		/// <summary>Gets the unique, hierarchically qualified identifier for the <see cref="T:System.Web.UI.WebControls.AdRotator" /> control.</summary>
		/// <returns>The fully qualified identifier for the server control.</returns>
		// Token: 0x170008B6 RID: 2230
		// (get) Token: 0x06001C5B RID: 7259 RVA: 0x00046F29 File Offset: 0x00045129
		public override string UniqueID
		{
			get
			{
				return base.UniqueID;
			}
		}

		/// <summary>Gets the HTML tag for the <see cref="T:System.Web.UI.WebControls.AdRotator" /> control. </summary>
		/// <returns>An <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration value representing the HTML tag for an <see cref="T:System.Web.UI.WebControls.AdRotator" /> control. </returns>
		// Token: 0x170008B7 RID: 2231
		// (get) Token: 0x06001C5C RID: 7260 RVA: 0x00046F31 File Offset: 0x00045131
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return base.TagKey;
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.AdRotator.AdCreated" /> event for the <see cref="T:System.Web.UI.WebControls.AdRotator" /> control.</summary>
		/// <param name="e">An <see cref="T:System.Web.UI.WebControls.AdCreatedEventArgs" /> that contains event data. </param>
		// Token: 0x06001C5D RID: 7261 RVA: 0x00046F3C File Offset: 0x0004513C
		protected virtual void OnAdCreated(AdCreatedEventArgs e)
		{
			AdCreatedEventHandler adCreatedEventHandler = (AdCreatedEventHandler)base.Events[AdRotator.AdCreatedEvent];
			if (adCreatedEventHandler != null)
			{
				adCreatedEventHandler(this, e);
			}
		}

		/// <summary>Occurs once per round trip to the server after the creation of the control, but before the page is rendered.</summary>
		// Token: 0x14000045 RID: 69
		// (add) Token: 0x06001C5E RID: 7262 RVA: 0x00046F6A File Offset: 0x0004516A
		// (remove) Token: 0x06001C5F RID: 7263 RVA: 0x00046F7D File Offset: 0x0004517D
		[WebSysDescription("")]
		[WebCategory("Action")]
		public event AdCreatedEventHandler AdCreated
		{
			add
			{
				base.Events.AddHandler(AdRotator.AdCreatedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(AdRotator.AdCreatedEvent, value);
			}
		}

		// Token: 0x06001C61 RID: 7265 RVA: 0x00046FAE File Offset: 0x000451AE
		// Note: this type is marked as 'beforefieldinit'.
		static AdRotator()
		{
			AdRotator.AdCreatedEvent = new object();
		}

		// Token: 0x040017EE RID: 6126
		private AdCreatedEventArgs createdargs;

		// Token: 0x040017EF RID: 6127
		private ArrayList ads = new ArrayList();

		// Token: 0x040017F0 RID: 6128
		private string ad_file = string.Empty;
	}
}
