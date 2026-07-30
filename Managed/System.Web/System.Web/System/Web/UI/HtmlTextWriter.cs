using System;
using System.Collections;
using System.IO;
using System.Security.Permissions;
using System.Text;
using System.Web.UI.WebControls;

namespace System.Web.UI
{
	/// <summary>Writes markup characters and text to an ASP.NET server control output stream. This class provides formatting capabilities that ASP.NET server controls use when rendering markup to clients.</summary>
	// Token: 0x020001D7 RID: 471
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class HtmlTextWriter : TextWriter
	{
		// Token: 0x06001312 RID: 4882 RVA: 0x0003375C File Offset: 0x0003195C
		static HtmlTextWriter()
		{
			foreach (HtmlTextWriter.HtmlTag htmlTag in HtmlTextWriter.tags)
			{
				HtmlTextWriter._tagTable.Add(htmlTag.name, htmlTag);
			}
			foreach (HtmlTextWriter.HtmlAttribute htmlAttribute in HtmlTextWriter.htmlattrs)
			{
				HtmlTextWriter._attributeTable.Add(htmlAttribute.name, htmlAttribute);
			}
			foreach (HtmlTextWriter.HtmlStyle htmlStyle in HtmlTextWriter.htmlstyles)
			{
				HtmlTextWriter._styleTable.Add(htmlStyle.name, htmlStyle);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.HtmlTextWriter" /> class that uses a default tab string.</summary>
		/// <param name="writer">The <see cref="T:System.IO.TextWriter" /> instance that renders the markup content. </param>
		// Token: 0x06001313 RID: 4883 RVA: 0x0003449E File Offset: 0x0003269E
		public HtmlTextWriter(TextWriter writer)
			: this(writer, "\t")
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.HtmlTextWriter" /> class with a specified tab string character.</summary>
		/// <param name="writer">The <see cref="T:System.IO.TextWriter" /> that renders the markup content. </param>
		/// <param name="tabString">The string to use to render a line indentation. </param>
		// Token: 0x06001314 RID: 4884 RVA: 0x000344AC File Offset: 0x000326AC
		public HtmlTextWriter(TextWriter writer, string tabString)
		{
			this.b = writer;
			this.tab_string = tabString;
		}

		// Token: 0x06001315 RID: 4885 RVA: 0x000344D7 File Offset: 0x000326D7
		internal static string StaticGetStyleName(HtmlTextWriterStyle styleKey)
		{
			if (styleKey < (HtmlTextWriterStyle)HtmlTextWriter.htmlstyles.Length)
			{
				return HtmlTextWriter.htmlstyles[(int)styleKey].name;
			}
			return null;
		}

		/// <summary>Registers markup attributes, whether literals or dynamically generated, from the source file so that they can be properly rendered to the requesting client.</summary>
		/// <param name="name">A string containing the name of the markup attribute to register. </param>
		/// <param name="key">An <see cref="T:System.Web.UI.HtmlTextWriterAttribute" /> that corresponds with the attribute name. </param>
		// Token: 0x06001316 RID: 4886 RVA: 0x0000393A File Offset: 0x00001B3A
		[global::System.MonoTODO("Does nothing")]
		protected static void RegisterAttribute(string name, HtmlTextWriterAttribute key)
		{
		}

		/// <summary>Registers markup style properties, whether literals or dynamically generated, from the source file so that they can be properly rendered to the requesting client.</summary>
		/// <param name="name">The string passed from the source file specifying the style name. </param>
		/// <param name="key">The <see cref="T:System.Web.UI.HtmlTextWriterStyle" /> that corresponds with the specified style. </param>
		// Token: 0x06001317 RID: 4887 RVA: 0x0000393A File Offset: 0x00001B3A
		[global::System.MonoTODO("Does nothing")]
		protected static void RegisterStyle(string name, HtmlTextWriterStyle key)
		{
		}

		/// <summary>Registers markup tags, whether literals or dynamically generated, from the source file so that they can be properly rendered to the requesting client.</summary>
		/// <param name="name">A string that contains the HTML tag. </param>
		/// <param name="key">An <see cref="T:System.Web.UI.HtmlTextWriterTag" /> that specifies which element to render. </param>
		// Token: 0x06001318 RID: 4888 RVA: 0x0000393A File Offset: 0x00001B3A
		[global::System.MonoTODO("Does nothing")]
		protected static void RegisterTag(string name, HtmlTextWriterTag key)
		{
		}

		/// <summary>Adds the markup attribute and the attribute value to the opening tag of the element that the <see cref="T:System.Web.UI.HtmlTextWriter" /> object creates with a subsequent call to the <see cref="Overload:System.Web.UI.HtmlTextWriter.RenderBeginTag" /> method, with optional encoding.</summary>
		/// <param name="key">An <see cref="T:System.Web.UI.HtmlTextWriterAttribute" /> that represents the markup attribute to add to the output stream. </param>
		/// <param name="value">A string containing the value to assign to the attribute. </param>
		/// <param name="fEncode">true to encode the attribute and its value; otherwise, false. </param>
		// Token: 0x06001319 RID: 4889 RVA: 0x000344F1 File Offset: 0x000326F1
		public virtual void AddAttribute(HtmlTextWriterAttribute key, string value, bool fEncode)
		{
			if (fEncode)
			{
				value = HttpUtility.HtmlAttributeEncode(value);
			}
			this.AddAttribute(this.GetAttributeName(key), value, key);
		}

		/// <summary>Adds the markup attribute and the attribute value to the opening tag of the element that the <see cref="T:System.Web.UI.HtmlTextWriter" /> object creates with a subsequent call to the <see cref="Overload:System.Web.UI.HtmlTextWriter.RenderBeginTag" /> method.</summary>
		/// <param name="key">An <see cref="T:System.Web.UI.HtmlTextWriterAttribute" /> that represents the markup attribute to add to the output stream. </param>
		/// <param name="value">A string containing the value to assign to the attribute. </param>
		// Token: 0x0600131A RID: 4890 RVA: 0x0003450D File Offset: 0x0003270D
		public virtual void AddAttribute(HtmlTextWriterAttribute key, string value)
		{
			if (key != HtmlTextWriterAttribute.Name && key != HtmlTextWriterAttribute.Id)
			{
				value = HttpUtility.HtmlAttributeEncode(value);
			}
			this.AddAttribute(this.GetAttributeName(key), value, key);
		}

		/// <summary>Adds the specified markup attribute and value to the opening tag of the element that the <see cref="T:System.Web.UI.HtmlTextWriter" /> object creates with a subsequent call to the <see cref="Overload:System.Web.UI.HtmlTextWriter.RenderBeginTag" /> method, with optional encoding.</summary>
		/// <param name="name">A string containing the name of the attribute to add. </param>
		/// <param name="value">A string containing the value to assign to the attribute. </param>
		/// <param name="fEndode">true to encode the attribute and its value; otherwise, false. </param>
		// Token: 0x0600131B RID: 4891 RVA: 0x00034530 File Offset: 0x00032730
		public virtual void AddAttribute(string name, string value, bool fEndode)
		{
			if (fEndode)
			{
				value = HttpUtility.HtmlAttributeEncode(value);
			}
			this.AddAttribute(name, value, this.GetAttributeKey(name));
		}

		/// <summary>Adds the specified markup attribute and value to the opening tag of the element that the <see cref="T:System.Web.UI.HtmlTextWriter" /> object creates with a subsequent call to the <see cref="Overload:System.Web.UI.HtmlTextWriter.RenderBeginTag" /> method.</summary>
		/// <param name="name">A string containing the name of the attribute to add. </param>
		/// <param name="value">A string containing the value to assign to the attribute. </param>
		// Token: 0x0600131C RID: 4892 RVA: 0x0003454C File Offset: 0x0003274C
		public virtual void AddAttribute(string name, string value)
		{
			HtmlTextWriterAttribute attributeKey = this.GetAttributeKey(name);
			if (attributeKey != HtmlTextWriterAttribute.Name && attributeKey != HtmlTextWriterAttribute.Id)
			{
				value = HttpUtility.HtmlAttributeEncode(value);
			}
			this.AddAttribute(name, value, attributeKey);
		}

		/// <summary>Adds the specified markup attribute and value, along with an <see cref="T:System.Web.UI.HtmlTextWriterAttribute" /> enumeration value, to the opening tag of the element that the <see cref="T:System.Web.UI.HtmlTextWriter" /> object creates with a subsequent call to the <see cref="Overload:System.Web.UI.HtmlTextWriter.RenderBeginTag" /> method.</summary>
		/// <param name="name">A string containing the name of the attribute to add. </param>
		/// <param name="value">A string containing the value to assign to the attribute. </param>
		/// <param name="key">An <see cref="T:System.Web.UI.HtmlTextWriterAttribute" /> that represents the attribute. </param>
		// Token: 0x0600131D RID: 4893 RVA: 0x0003457C File Offset: 0x0003277C
		protected virtual void AddAttribute(string name, string value, HtmlTextWriterAttribute key)
		{
			this.NextAttrStack();
			this.attrs[this.attrs_pos].name = name;
			this.attrs[this.attrs_pos].value = value;
			this.attrs[this.attrs_pos].key = key;
		}

		/// <summary>Adds the specified markup style attribute and the attribute value, along with an <see cref="T:System.Web.UI.HtmlTextWriterStyle" /> enumeration value, to the opening markup tag created by a subsequent call to the <see cref="Overload:System.Web.UI.HtmlTextWriter.RenderBeginTag" /> method.</summary>
		/// <param name="name">A string that contains the style attribute to be added. </param>
		/// <param name="value">A string that contains the value to assign to the attribute. </param>
		/// <param name="key">An <see cref="T:System.Web.UI.HtmlTextWriterStyle" /> that represents the style attribute to add. </param>
		// Token: 0x0600131E RID: 4894 RVA: 0x000345D4 File Offset: 0x000327D4
		protected virtual void AddStyleAttribute(string name, string value, HtmlTextWriterStyle key)
		{
			this.NextStyleStack();
			this.styles[this.styles_pos].name = name;
			value = HttpUtility.HtmlAttributeEncode(value);
			this.styles[this.styles_pos].value = value;
			this.styles[this.styles_pos].key = key;
		}

		/// <summary>Adds the specified markup style attribute and the attribute value to the opening markup tag created by a subsequent call to the <see cref="Overload:System.Web.UI.HtmlTextWriter.RenderBeginTag" /> method.</summary>
		/// <param name="name">A string that contains the style attribute to add. </param>
		/// <param name="value">A string that contains the value to assign to the attribute. </param>
		// Token: 0x0600131F RID: 4895 RVA: 0x00034634 File Offset: 0x00032834
		public virtual void AddStyleAttribute(string name, string value)
		{
			this.AddStyleAttribute(name, value, this.GetStyleKey(name));
		}

		/// <summary>Adds the markup style attribute associated with the specified <see cref="T:System.Web.UI.HtmlTextWriterStyle" /> value and the attribute value to the opening markup tag created by a subsequent call to the <see cref="Overload:System.Web.UI.HtmlTextWriter.RenderBeginTag" /> method.</summary>
		/// <param name="key">An <see cref="T:System.Web.UI.HtmlTextWriterStyle" /> that represents the style attribute to add to the output stream. </param>
		/// <param name="value">A string that contains the value to assign to the attribute. </param>
		// Token: 0x06001320 RID: 4896 RVA: 0x00034645 File Offset: 0x00032845
		public virtual void AddStyleAttribute(HtmlTextWriterStyle key, string value)
		{
			this.AddStyleAttribute(this.GetStyleName(key), value, key);
		}

		/// <summary>Closes the <see cref="T:System.Web.UI.HtmlTextWriter" /> object and releases any system resources associated with it.</summary>
		// Token: 0x06001321 RID: 4897 RVA: 0x00034656 File Offset: 0x00032856
		public override void Close()
		{
			this.b.Close();
		}

		/// <summary>Encodes the value of the specified markup attribute based on the requirements of the <see cref="T:System.Web.HttpRequest" /> object of the current context.</summary>
		/// <returns>A string containing the encoded attribute value.</returns>
		/// <param name="attrKey">An <see cref="T:System.Web.UI.HtmlTextWriterAttribute" /> representing the markup attribute. </param>
		/// <param name="value">A string containing the attribute value to encode. </param>
		// Token: 0x06001322 RID: 4898 RVA: 0x00034663 File Offset: 0x00032863
		protected virtual string EncodeAttributeValue(HtmlTextWriterAttribute attrKey, string value)
		{
			return HttpUtility.HtmlAttributeEncode(value);
		}

		/// <summary>Encodes the value of the specified markup attribute based on the requirements of the <see cref="T:System.Web.HttpRequest" /> object of the current context.</summary>
		/// <returns>A string containing the encoded attribute value, null if <paramref name="value" /> is empty, or the unencoded attribute value if <paramref name="fEncode" /> is false.</returns>
		/// <param name="value">A string containing the attribute value to encode. </param>
		/// <param name="fEncode">true to encode the attribute value; otherwise, false. </param>
		// Token: 0x06001323 RID: 4899 RVA: 0x0003466B File Offset: 0x0003286B
		protected string EncodeAttributeValue(string value, bool fEncode)
		{
			if (fEncode)
			{
				return HttpUtility.HtmlAttributeEncode(value);
			}
			return value;
		}

		/// <summary>Performs minimal URL encoding by converting spaces in the specified URL to the string "%20".</summary>
		/// <returns>A string containing the encoded URL.</returns>
		/// <param name="url">A string containing the URL to encode. </param>
		// Token: 0x06001324 RID: 4900 RVA: 0x00034678 File Offset: 0x00032878
		protected string EncodeUrl(string url)
		{
			return HttpUtility.UrlPathEncode(url);
		}

		/// <summary>Removes all the markup and style attributes on all properties of the page or Web server control.</summary>
		// Token: 0x06001325 RID: 4901 RVA: 0x00034680 File Offset: 0x00032880
		protected virtual void FilterAttributes()
		{
			HtmlTextWriter.AddedAttr addedAttr = default(HtmlTextWriter.AddedAttr);
			for (int i = 0; i <= this.attrs_pos; i++)
			{
				HtmlTextWriter.AddedAttr addedAttr2 = this.attrs[i];
				if (this.OnAttributeRender(addedAttr2.name, addedAttr2.value, addedAttr2.key))
				{
					if (addedAttr2.key == HtmlTextWriterAttribute.Style)
					{
						addedAttr = addedAttr2;
					}
					else
					{
						this.WriteAttribute(addedAttr2.name, addedAttr2.value, false);
					}
				}
			}
			if (this.styles_pos != -1 || addedAttr.value != null)
			{
				this.Write(' ');
				this.Write("style");
				this.Write("=\"");
				for (int j = 0; j <= this.styles_pos; j++)
				{
					HtmlTextWriter.AddedStyle addedStyle = this.styles[j];
					if (this.OnStyleAttributeRender(addedStyle.name, addedStyle.value, addedStyle.key))
					{
						if (addedStyle.key == HtmlTextWriterStyle.BackgroundImage)
						{
							addedStyle.value = "url(" + HttpUtility.UrlPathEncode(addedStyle.value) + ")";
						}
						this.WriteStyleAttribute(addedStyle.name, addedStyle.value, false);
					}
				}
				this.Write(addedAttr.value);
				this.Write('"');
			}
			this.styles_pos = (this.attrs_pos = -1);
		}

		/// <summary>Clears all buffers for the current <see cref="T:System.Web.UI.HtmlTextWriter" /> object and causes any buffered data to be written to the output stream.</summary>
		// Token: 0x06001326 RID: 4902 RVA: 0x000347C3 File Offset: 0x000329C3
		public override void Flush()
		{
			this.b.Flush();
		}

		/// <summary>Obtains the corresponding <see cref="T:System.Web.UI.HtmlTextWriterAttribute" /> enumeration value for the specified attribute.</summary>
		/// <returns>The <see cref="T:System.Web.UI.HtmlTextWriterAttribute" /> enumeration value for the specified attribute; otherwise, an invalid <see cref="T:System.Web.UI.HtmlTextWriterAttribute" /> value if the attribute is not a member of the enumeration.</returns>
		/// <param name="attrName">A string that contains the attribute for which to obtain the <see cref="T:System.Web.UI.HtmlTextWriterAttribute" />. </param>
		// Token: 0x06001327 RID: 4903 RVA: 0x000347D0 File Offset: 0x000329D0
		protected HtmlTextWriterAttribute GetAttributeKey(string attrName)
		{
			object obj = HtmlTextWriter._attributeTable[attrName];
			if (obj == null)
			{
				return (HtmlTextWriterAttribute)(-1);
			}
			return ((HtmlTextWriter.HtmlAttribute)obj).key;
		}

		/// <summary>Obtains the name of the markup attribute associated with the specified <see cref="T:System.Web.UI.HtmlTextWriterAttribute" /> value.</summary>
		/// <returns>A string containing the name of the markup attribute.</returns>
		/// <param name="attrKey">The <see cref="T:System.Web.UI.HtmlTextWriterAttribute" /> to obtain the markup attribute name for. </param>
		// Token: 0x06001328 RID: 4904 RVA: 0x000347F9 File Offset: 0x000329F9
		protected string GetAttributeName(HtmlTextWriterAttribute attrKey)
		{
			if (attrKey < (HtmlTextWriterAttribute)HtmlTextWriter.htmlattrs.Length)
			{
				return HtmlTextWriter.htmlattrs[(int)attrKey].name;
			}
			return null;
		}

		/// <summary>Obtains the <see cref="T:System.Web.UI.HtmlTextWriterStyle" /> enumeration value for the specified style.</summary>
		/// <returns>The <see cref="T:System.Web.UI.HtmlTextWriterStyle" /> enumeration value corresponding to <paramref name="styleName" />.</returns>
		/// <param name="styleName">The style attribute for which to obtain the <see cref="T:System.Web.UI.HtmlTextWriterStyle" />. </param>
		// Token: 0x06001329 RID: 4905 RVA: 0x00034814 File Offset: 0x00032A14
		protected HtmlTextWriterStyle GetStyleKey(string styleName)
		{
			object obj = HtmlTextWriter._styleTable[styleName];
			if (obj == null)
			{
				return (HtmlTextWriterStyle)(-1);
			}
			return ((HtmlTextWriter.HtmlStyle)obj).key;
		}

		/// <summary>Obtains the markup style attribute name associated with the specified <see cref="T:System.Web.UI.HtmlTextWriterStyle" /> enumeration value.</summary>
		/// <returns>The style attribute name associated with the <see cref="T:System.Web.UI.HtmlTextWriterStyle" /> enumeration value specified in <paramref name="styleKey" />.</returns>
		/// <param name="styleKey">The <see cref="T:System.Web.UI.HtmlTextWriterStyle" /> to obtain the style attribute name for. </param>
		// Token: 0x0600132A RID: 4906 RVA: 0x0003483D File Offset: 0x00032A3D
		protected string GetStyleName(HtmlTextWriterStyle styleKey)
		{
			return HtmlTextWriter.StaticGetStyleName(styleKey);
		}

		/// <summary>Obtains the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration value associated with the specified markup element.</summary>
		/// <returns>The <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration value; otherwise, if <paramref name="tagName" /> is not associated with a specific <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value, <see cref="F:System.Web.UI.HtmlTextWriterTag.Unknown" />.</returns>
		/// <param name="tagName">The markup element for which to obtain the <see cref="T:System.Web.UI.HtmlTextWriterTag" />. </param>
		// Token: 0x0600132B RID: 4907 RVA: 0x00034848 File Offset: 0x00032A48
		protected virtual HtmlTextWriterTag GetTagKey(string tagName)
		{
			object obj = HtmlTextWriter._tagTable[tagName];
			if (obj == null)
			{
				return HtmlTextWriterTag.Unknown;
			}
			return ((HtmlTextWriter.HtmlTag)obj).key;
		}

		// Token: 0x0600132C RID: 4908 RVA: 0x00034871 File Offset: 0x00032A71
		internal static string StaticGetTagName(HtmlTextWriterTag tagKey)
		{
			if (tagKey < (HtmlTextWriterTag)HtmlTextWriter.tags.Length)
			{
				return HtmlTextWriter.tags[(int)tagKey].name;
			}
			return null;
		}

		/// <summary>Obtains the markup element associated with the specified <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration value.</summary>
		/// <returns>A string representing the markup element.</returns>
		/// <param name="tagKey">The <see cref="T:System.Web.UI.HtmlTextWriterTag" /> to obtain the markup element for. </param>
		// Token: 0x0600132D RID: 4909 RVA: 0x0003488B File Offset: 0x00032A8B
		protected virtual string GetTagName(HtmlTextWriterTag tagKey)
		{
			if (tagKey < (HtmlTextWriterTag)HtmlTextWriter.tags.Length)
			{
				return HtmlTextWriter.tags[(int)tagKey].name;
			}
			return null;
		}

		/// <summary>Determines whether the specified markup attribute and its value are rendered during the next call to the <see cref="Overload:System.Web.UI.HtmlTextWriter.RenderBeginTag" /> method.</summary>
		/// <returns>true if the attribute is rendered during the next call to the <see cref="Overload:System.Web.UI.HtmlTextWriter.RenderBeginTag" /> method; otherwise, false.</returns>
		/// <param name="key">The <see cref="T:System.Web.UI.HtmlTextWriterAttribute" /> associated with the markup attribute. </param>
		// Token: 0x0600132E RID: 4910 RVA: 0x000348A8 File Offset: 0x00032AA8
		protected bool IsAttributeDefined(HtmlTextWriterAttribute key)
		{
			string text;
			return this.IsAttributeDefined(key, out text);
		}

		/// <summary>Determines whether the specified markup attribute and its value are rendered during the next call to the <see cref="Overload:System.Web.UI.HtmlTextWriter.RenderBeginTag" /> method.</summary>
		/// <returns>true if the attribute is rendered during the next call to the <see cref="Overload:System.Web.UI.HtmlTextWriter.RenderBeginTag" /> method; otherwise, false.</returns>
		/// <param name="key">The <see cref="T:System.Web.UI.HtmlTextWriterAttribute" /> associated with the markup attribute. </param>
		/// <param name="value">The value assigned to the attribute. </param>
		// Token: 0x0600132F RID: 4911 RVA: 0x000348C0 File Offset: 0x00032AC0
		protected bool IsAttributeDefined(HtmlTextWriterAttribute key, out string value)
		{
			for (int i = 0; i <= this.attrs_pos; i++)
			{
				if (this.attrs[i].key == key)
				{
					value = this.attrs[i].value;
					return true;
				}
			}
			value = null;
			return false;
		}

		/// <summary>Determines whether the specified markup style attribute is rendered during the next call to the <see cref="Overload:System.Web.UI.HtmlTextWriter.RenderBeginTag" /> method.</summary>
		/// <returns>true if the attribute will be rendered during the next call to the <see cref="Overload:System.Web.UI.HtmlTextWriter.RenderBeginTag" /> method; otherwise, false.</returns>
		/// <param name="key">The <see cref="T:System.Web.UI.HtmlTextWriterStyle" /> associated with the attribute. </param>
		// Token: 0x06001330 RID: 4912 RVA: 0x0003490C File Offset: 0x00032B0C
		protected bool IsStyleAttributeDefined(HtmlTextWriterStyle key)
		{
			string text;
			return this.IsStyleAttributeDefined(key, out text);
		}

		/// <summary>Determines whether the specified markup style attribute and its value are rendered during the next call to the <see cref="Overload:System.Web.UI.HtmlTextWriter.RenderBeginTag" /> method.</summary>
		/// <returns>true if the attribute and its value will be rendered during the next call to the <see cref="Overload:System.Web.UI.HtmlTextWriter.RenderBeginTag" /> method; otherwise, false.</returns>
		/// <param name="key">The <see cref="T:System.Web.UI.HtmlTextWriterStyle" /> associated with the attribute. </param>
		/// <param name="value">The value assigned to the style attribute. </param>
		// Token: 0x06001331 RID: 4913 RVA: 0x00034924 File Offset: 0x00032B24
		protected bool IsStyleAttributeDefined(HtmlTextWriterStyle key, out string value)
		{
			for (int i = 0; i <= this.styles_pos; i++)
			{
				if (this.styles[i].key == key)
				{
					value = this.styles[i].value;
					return true;
				}
			}
			value = null;
			return false;
		}

		/// <summary>Determines whether the specified markup attribute and its value can be rendered to the current markup element.</summary>
		/// <returns>Always true.</returns>
		/// <param name="name">A string containing the name of the attribute to render. </param>
		/// <param name="value">A string containing the value that is assigned to the attribute. </param>
		/// <param name="key">The <see cref="T:System.Web.UI.HtmlTextWriterAttribute" /> associated with the markup attribute. </param>
		// Token: 0x06001332 RID: 4914 RVA: 0x00008B66 File Offset: 0x00006D66
		protected virtual bool OnAttributeRender(string name, string value, HtmlTextWriterAttribute key)
		{
			return true;
		}

		/// <summary>Determines whether the specified markup style attribute and its value can be rendered to the current markup element.</summary>
		/// <returns>Always true.</returns>
		/// <param name="name">A string containing the name of the style attribute to render. </param>
		/// <param name="value">A string containing the value that is assigned to the style attribute. </param>
		/// <param name="key">The <see cref="T:System.Web.UI.HtmlTextWriterStyle" /> associated with the style attribute. </param>
		// Token: 0x06001333 RID: 4915 RVA: 0x00008B66 File Offset: 0x00006D66
		protected virtual bool OnStyleAttributeRender(string name, string value, HtmlTextWriterStyle key)
		{
			return true;
		}

		/// <summary>Determines whether the specified markup element will be rendered to the requesting page.</summary>
		/// <returns>Always true.</returns>
		/// <param name="name">A string containing the name of the element to render. </param>
		/// <param name="key">The <see cref="T:System.Web.UI.HtmlTextWriterTag" /> associated with the element. </param>
		// Token: 0x06001334 RID: 4916 RVA: 0x00008B66 File Offset: 0x00006D66
		protected virtual bool OnTagRender(string name, HtmlTextWriterTag key)
		{
			return true;
		}

		/// <summary>Writes a series of tab strings that represent the indentation level for a line of markup characters.</summary>
		// Token: 0x06001335 RID: 4917 RVA: 0x00034970 File Offset: 0x00032B70
		protected virtual void OutputTabs()
		{
			if (!this.newline)
			{
				return;
			}
			this.newline = false;
			for (int i = 0; i < this.Indent; i++)
			{
				this.b.Write(this.tab_string);
			}
		}

		/// <summary>Removes the most recently saved markup element from the list of rendered elements.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the most recently rendered markup element.</returns>
		/// <exception cref="T:System.InvalidOperationException">The list of rendered elements is empty. </exception>
		// Token: 0x06001336 RID: 4918 RVA: 0x000349AF File Offset: 0x00032BAF
		protected string PopEndTag()
		{
			if (this.tagstack_pos == -1)
			{
				throw new InvalidOperationException();
			}
			string tagName = this.TagName;
			this.tagstack_pos--;
			return tagName;
		}

		/// <summary>Saves the specified markup element for later use when generating the end tag for a markup element.</summary>
		/// <param name="endTag">The closing tag of the markup element. </param>
		// Token: 0x06001337 RID: 4919 RVA: 0x000349D4 File Offset: 0x00032BD4
		protected void PushEndTag(string endTag)
		{
			this.NextTagStack();
			this.TagName = endTag;
		}

		// Token: 0x06001338 RID: 4920 RVA: 0x000349E3 File Offset: 0x00032BE3
		private void PushEndTag(HtmlTextWriterTag t)
		{
			this.NextTagStack();
			this.TagKey = t;
		}

		/// <summary>Writes any text or spacing that occurs after the content and before the closing tag of the markup element to the markup output stream.</summary>
		/// <returns>A string that contains the spacing or text to write after the content of the element. </returns>
		// Token: 0x06001339 RID: 4921 RVA: 0x00003BEA File Offset: 0x00001DEA
		protected virtual string RenderAfterContent()
		{
			return null;
		}

		/// <summary>Writes any spacing or text that occurs after the closing tag for a markup element.</summary>
		/// <returns>The spacing or text to write after the closing tag of the element. </returns>
		// Token: 0x0600133A RID: 4922 RVA: 0x00003BEA File Offset: 0x00001DEA
		protected virtual string RenderAfterTag()
		{
			return null;
		}

		/// <summary>Writes any text or spacing before the content and after the opening tag of a markup element.</summary>
		/// <returns>The text or spacing to write prior to the content of the element. If not overridden, <see cref="M:System.Web.UI.HtmlTextWriter.RenderBeforeContent" /> returns null.</returns>
		// Token: 0x0600133B RID: 4923 RVA: 0x00003BEA File Offset: 0x00001DEA
		protected virtual string RenderBeforeContent()
		{
			return null;
		}

		/// <summary>Writes any text or spacing that occurs before the opening tag of a markup element.</summary>
		/// <returns>The text or spacing to write before the markup element opening tag. If not overridden, null.</returns>
		// Token: 0x0600133C RID: 4924 RVA: 0x00003BEA File Offset: 0x00001DEA
		protected virtual string RenderBeforeTag()
		{
			return null;
		}

		/// <summary>Writes the opening tag of the specified markup element to the output stream.</summary>
		/// <param name="tagName">A string containing the name of the markup element for which to render the opening tag.</param>
		// Token: 0x0600133D RID: 4925 RVA: 0x000349F4 File Offset: 0x00032BF4
		public virtual void RenderBeginTag(string tagName)
		{
			bool flag = !this.OnTagRender(tagName, this.GetTagKey(tagName));
			this.PushEndTag(tagName);
			this.TagIgnore = flag;
			this.DoBeginTag();
		}

		/// <summary>Writes the opening tag of the markup element associated with the specified <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration value to the output stream.</summary>
		/// <param name="tagKey">One of the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> values that defines the opening tag of the markup element to render. </param>
		// Token: 0x0600133E RID: 4926 RVA: 0x00034A28 File Offset: 0x00032C28
		public virtual void RenderBeginTag(HtmlTextWriterTag tagKey)
		{
			bool flag = !this.OnTagRender(this.GetTagName(tagKey), tagKey);
			this.PushEndTag(tagKey);
			this.DoBeginTag();
			this.TagIgnore = flag;
		}

		// Token: 0x0600133F RID: 4927 RVA: 0x00034A5B File Offset: 0x00032C5B
		private void WriteIfNotNull(string s)
		{
			if (s != null)
			{
				this.Write(s);
			}
		}

		// Token: 0x06001340 RID: 4928 RVA: 0x00034A68 File Offset: 0x00032C68
		private void DoBeginTag()
		{
			this.WriteIfNotNull(this.RenderBeforeTag());
			if (!this.TagIgnore)
			{
				this.WriteBeginTag(this.TagName);
				this.FilterAttributes();
				HtmlTextWriterTag htmlTextWriterTag = ((this.TagKey < (HtmlTextWriterTag)HtmlTextWriter.tags.Length) ? this.TagKey : HtmlTextWriterTag.Unknown);
				switch (HtmlTextWriter.tags[(int)htmlTextWriterTag].tag_type)
				{
				case HtmlTextWriter.TagType.Block:
				{
					this.Write('>');
					this.WriteLine();
					int num = this.Indent;
					this.Indent = num + 1;
					break;
				}
				case HtmlTextWriter.TagType.Inline:
					this.Write('>');
					break;
				case HtmlTextWriter.TagType.SelfClosing:
					this.Write(" />");
					break;
				}
			}
			this.WriteIfNotNull(this.RenderBeforeContent());
		}

		/// <summary>Writes the end tag of a markup element to the output stream.</summary>
		// Token: 0x06001341 RID: 4929 RVA: 0x00034B1C File Offset: 0x00032D1C
		public virtual void RenderEndTag()
		{
			this.WriteIfNotNull(this.RenderAfterContent());
			if (!this.TagIgnore)
			{
				HtmlTextWriterTag htmlTextWriterTag = ((this.TagKey < (HtmlTextWriterTag)HtmlTextWriter.tags.Length) ? this.TagKey : HtmlTextWriterTag.Unknown);
				switch (HtmlTextWriter.tags[(int)htmlTextWriterTag].tag_type)
				{
				case HtmlTextWriter.TagType.Block:
				{
					int num = this.Indent;
					this.Indent = num - 1;
					this.WriteLineNoTabs(string.Empty);
					this.WriteEndTag(this.TagName);
					break;
				}
				case HtmlTextWriter.TagType.Inline:
					this.WriteEndTag(this.TagName);
					break;
				}
			}
			this.WriteIfNotNull(this.RenderAfterTag());
			this.PopEndTag();
		}

		/// <summary>Writes the specified markup attribute and value to the output stream, and, if specified, writes the value encoded.</summary>
		/// <param name="name">The markup attribute to write to the output stream. </param>
		/// <param name="value">The value assigned to the attribute. </param>
		/// <param name="fEncode">true to encode the attribute and its assigned value; otherwise, false. </param>
		// Token: 0x06001342 RID: 4930 RVA: 0x00034BBF File Offset: 0x00032DBF
		public virtual void WriteAttribute(string name, string value, bool fEncode)
		{
			this.Write(' ');
			this.Write(name);
			if (value != null)
			{
				this.Write("=\"");
				value = this.EncodeAttributeValue(value, fEncode);
				this.Write(value);
				this.Write('"');
			}
		}

		/// <summary>Writes any tab spacing and the opening tag of the specified markup element to the output stream.</summary>
		/// <param name="tagName">The markup element of which to write the opening tag. </param>
		// Token: 0x06001343 RID: 4931 RVA: 0x00034BF7 File Offset: 0x00032DF7
		public virtual void WriteBeginTag(string tagName)
		{
			this.Write('<');
			this.Write(tagName);
		}

		/// <summary>Writes any tab spacing and the closing tag of the specified markup element.</summary>
		/// <param name="tagName">The element to write the closing tag for. </param>
		// Token: 0x06001344 RID: 4932 RVA: 0x00034C08 File Offset: 0x00032E08
		public virtual void WriteEndTag(string tagName)
		{
			this.Write("</");
			this.Write(tagName);
			this.Write('>');
		}

		/// <summary>Writes any tab spacing and the opening tag of the specified markup element to the output stream.</summary>
		/// <param name="tagName">The element to write to the output stream. </param>
		// Token: 0x06001345 RID: 4933 RVA: 0x00034C24 File Offset: 0x00032E24
		public virtual void WriteFullBeginTag(string tagName)
		{
			this.Write('<');
			this.Write(tagName);
			this.Write('>');
		}

		/// <summary>Writes the specified style attribute to the output stream.</summary>
		/// <param name="name">The style attribute to write to the output stream. </param>
		/// <param name="value">The value assigned to the style attribute. </param>
		// Token: 0x06001346 RID: 4934 RVA: 0x00034C3D File Offset: 0x00032E3D
		public virtual void WriteStyleAttribute(string name, string value)
		{
			this.WriteStyleAttribute(name, value, false);
		}

		/// <summary>Writes the specified style attribute and value to the output stream, and encodes the value, if specified.</summary>
		/// <param name="name">The style attribute to write to the output stream. </param>
		/// <param name="value">The value assigned to the style attribute. </param>
		/// <param name="fEncode">true to encode the style attribute and its assigned value; otherwise, false. </param>
		// Token: 0x06001347 RID: 4935 RVA: 0x00034C48 File Offset: 0x00032E48
		public virtual void WriteStyleAttribute(string name, string value, bool fEncode)
		{
			this.Write(name);
			this.Write(':');
			this.Write(this.EncodeAttributeValue(value, fEncode));
			this.Write(';');
		}

		/// <summary>Writes the text representation of a subarray of Unicode characters to the output stream, along with any pending tab spacing.</summary>
		/// <param name="buffer">The array of characters from which to write text to the output stream. </param>
		/// <param name="index">The index location in the array where writing begins. </param>
		/// <param name="count">The number of characters to write to the output stream. </param>
		// Token: 0x06001348 RID: 4936 RVA: 0x00034C6F File Offset: 0x00032E6F
		public override void Write(char[] buffer, int index, int count)
		{
			this.OutputTabs();
			this.b.Write(buffer, index, count);
		}

		/// <summary>Writes the text representation of a double-precision floating-point number to the output stream, along with any pending tab spacing.</summary>
		/// <param name="value">The double-precision floating-point number to write to the output stream. </param>
		// Token: 0x06001349 RID: 4937 RVA: 0x00034C85 File Offset: 0x00032E85
		public override void Write(double value)
		{
			this.OutputTabs();
			this.b.Write(value);
		}

		/// <summary>Writes the text representation of a Unicode character to the output stream, along with any pending tab spacing.</summary>
		/// <param name="value">The Unicode character to write to the output stream. </param>
		// Token: 0x0600134A RID: 4938 RVA: 0x00034C99 File Offset: 0x00032E99
		public override void Write(char value)
		{
			this.OutputTabs();
			this.b.Write(value);
		}

		/// <summary>Writes the text representation of an array of Unicode characters to the output stream, along with any pending tab spacing.</summary>
		/// <param name="buffer">The array of Unicode characters to write to the output stream. </param>
		// Token: 0x0600134B RID: 4939 RVA: 0x00034CAD File Offset: 0x00032EAD
		public override void Write(char[] buffer)
		{
			this.OutputTabs();
			this.b.Write(buffer);
		}

		/// <summary>Writes the text representation of a 32-byte signed integer to the output stream, along with any pending tab spacing.</summary>
		/// <param name="value">The 32-byte signed integer to write to the output stream. </param>
		// Token: 0x0600134C RID: 4940 RVA: 0x00034CC1 File Offset: 0x00032EC1
		public override void Write(int value)
		{
			this.OutputTabs();
			this.b.Write(value);
		}

		/// <summary>Writes a tab string and a formatted string to the output stream, using the same semantics as the <see cref="M:System.String.Format(System.String,System.Object)" /> method, along with any pending tab spacing.</summary>
		/// <param name="format">A string that contains zero or more format items. </param>
		/// <param name="arg0">An object to format.</param>
		// Token: 0x0600134D RID: 4941 RVA: 0x00034CD5 File Offset: 0x00032ED5
		public override void Write(string format, object arg0)
		{
			this.OutputTabs();
			this.b.Write(format, arg0);
		}

		/// <summary>Writes a formatted string that contains the text representation of two objects to the output stream, along with any pending tab spacing. This method uses the same semantics as the <see cref="M:System.String.Format(System.String,System.Object,System.Object)" /> method.</summary>
		/// <param name="format">A string that contains zero or more format items. </param>
		/// <param name="arg0">An object to format. </param>
		/// <param name="arg1">An object to format.</param>
		// Token: 0x0600134E RID: 4942 RVA: 0x00034CEA File Offset: 0x00032EEA
		public override void Write(string format, object arg0, object arg1)
		{
			this.OutputTabs();
			this.b.Write(format, arg0, arg1);
		}

		/// <summary>Writes a formatted string that contains the text representation of an object array to the output stream, along with any pending tab spacing. This method uses the same semantics as the <see cref="M:System.String.Format(System.String,System.Object[])" /> method.</summary>
		/// <param name="format">A string that contains zero or more format items. </param>
		/// <param name="arg">An object array to format. </param>
		// Token: 0x0600134F RID: 4943 RVA: 0x00034D00 File Offset: 0x00032F00
		public override void Write(string format, params object[] arg)
		{
			this.OutputTabs();
			this.b.Write(format, arg);
		}

		/// <summary>Writes the specified string to the output stream, along with any pending tab spacing.</summary>
		/// <param name="s">The string to write to the output stream. </param>
		// Token: 0x06001350 RID: 4944 RVA: 0x00034D15 File Offset: 0x00032F15
		public override void Write(string s)
		{
			this.OutputTabs();
			this.b.Write(s);
		}

		/// <summary>Writes the text representation of a 64-byte signed integer to the output stream, along with any pending tab spacing.</summary>
		/// <param name="value">The 64-byte signed integer to write to the output stream. </param>
		// Token: 0x06001351 RID: 4945 RVA: 0x00034D29 File Offset: 0x00032F29
		public override void Write(long value)
		{
			this.OutputTabs();
			this.b.Write(value);
		}

		/// <summary>Writes the text representation of an object to the output stream, along with any pending tab spacing.</summary>
		/// <param name="value">The object to write to the output stream. </param>
		// Token: 0x06001352 RID: 4946 RVA: 0x00034D3D File Offset: 0x00032F3D
		public override void Write(object value)
		{
			this.OutputTabs();
			this.b.Write(value);
		}

		/// <summary>Writes the text representation of a single-precision floating-point number to the output stream, along with any pending tab spacing.</summary>
		/// <param name="value">The single-precision floating-point number to write to the output stream. </param>
		// Token: 0x06001353 RID: 4947 RVA: 0x00034D51 File Offset: 0x00032F51
		public override void Write(float value)
		{
			this.OutputTabs();
			this.b.Write(value);
		}

		/// <summary>Writes the text representation of a Boolean value to the output stream, along with any pending tab spacing.</summary>
		/// <param name="value">The <see cref="T:System.Boolean" /> to write to the output stream. </param>
		// Token: 0x06001354 RID: 4948 RVA: 0x00034D65 File Offset: 0x00032F65
		public override void Write(bool value)
		{
			this.OutputTabs();
			this.b.Write(value);
		}

		/// <summary>Writes the specified markup attribute and value to the output stream.</summary>
		/// <param name="name">The attribute to write to the output stream. </param>
		/// <param name="value">The value assigned to the attribute. </param>
		// Token: 0x06001355 RID: 4949 RVA: 0x00034D79 File Offset: 0x00032F79
		public virtual void WriteAttribute(string name, string value)
		{
			this.WriteAttribute(name, value, false);
		}

		/// <summary>Writes any pending tab spacing and a Unicode character, followed by a line terminator string, to the output stream.</summary>
		/// <param name="value">The character to write to the output stream. </param>
		// Token: 0x06001356 RID: 4950 RVA: 0x00034D84 File Offset: 0x00032F84
		public override void WriteLine(char value)
		{
			this.OutputTabs();
			this.b.WriteLine(value);
			this.newline = true;
		}

		/// <summary>Writes any pending tab spacing and the text representation of a 64-byte signed integer, followed by a line terminator string, to the output stream.</summary>
		/// <param name="value">The 64-byte signed integer to write to the output stream. </param>
		// Token: 0x06001357 RID: 4951 RVA: 0x00034D9F File Offset: 0x00032F9F
		public override void WriteLine(long value)
		{
			this.OutputTabs();
			this.b.WriteLine(value);
			this.newline = true;
		}

		/// <summary>Writes any pending tab spacing and the text representation of an object, followed by a line terminator string, to the output stream.</summary>
		/// <param name="value">The object to write to the output stream. </param>
		// Token: 0x06001358 RID: 4952 RVA: 0x00034DBA File Offset: 0x00032FBA
		public override void WriteLine(object value)
		{
			this.OutputTabs();
			this.b.WriteLine(value);
			this.newline = true;
		}

		/// <summary>Writes any pending tab spacing and the text representation of a double-precision floating-point number, followed by a line terminator string, to the output stream.</summary>
		/// <param name="value">The double-precision floating-point number to write to the output stream. </param>
		// Token: 0x06001359 RID: 4953 RVA: 0x00034DD5 File Offset: 0x00032FD5
		public override void WriteLine(double value)
		{
			this.OutputTabs();
			this.b.WriteLine(value);
			this.newline = true;
		}

		/// <summary>Writes any pending tab spacing and a subarray of Unicode characters, followed by a line terminator string, to the output stream.</summary>
		/// <param name="buffer">The character array from which to write text to the output stream. </param>
		/// <param name="index">The location in the character array where writing begins. </param>
		/// <param name="count">The number of characters in the array to write to the output stream. </param>
		// Token: 0x0600135A RID: 4954 RVA: 0x00034DF0 File Offset: 0x00032FF0
		public override void WriteLine(char[] buffer, int index, int count)
		{
			this.OutputTabs();
			this.b.WriteLine(buffer, index, count);
			this.newline = true;
		}

		/// <summary>Writes any pending tab spacing and an array of Unicode characters, followed by a line terminator string, to the output stream.</summary>
		/// <param name="buffer">The character array to write to the output stream. </param>
		// Token: 0x0600135B RID: 4955 RVA: 0x00034E0D File Offset: 0x0003300D
		public override void WriteLine(char[] buffer)
		{
			this.OutputTabs();
			this.b.WriteLine(buffer);
			this.newline = true;
		}

		/// <summary>Writes any pending tab spacing and the text representation of a Boolean value, followed by a line terminator string, to the output stream.</summary>
		/// <param name="value">The Boolean to write to the output stream. </param>
		// Token: 0x0600135C RID: 4956 RVA: 0x00034E28 File Offset: 0x00033028
		public override void WriteLine(bool value)
		{
			this.OutputTabs();
			this.b.WriteLine(value);
			this.newline = true;
		}

		/// <summary>Writes a line terminator string to the output stream.</summary>
		// Token: 0x0600135D RID: 4957 RVA: 0x00034E43 File Offset: 0x00033043
		public override void WriteLine()
		{
			this.OutputTabs();
			this.b.WriteLine();
			this.newline = true;
		}

		/// <summary>Writes any pending tab spacing and the text representation of a 32-byte signed integer, followed by a line terminator string, to the output stream.</summary>
		/// <param name="value">The 32-byte signed integer to write to the output stream. </param>
		// Token: 0x0600135E RID: 4958 RVA: 0x00034E5D File Offset: 0x0003305D
		public override void WriteLine(int value)
		{
			this.OutputTabs();
			this.b.WriteLine(value);
			this.newline = true;
		}

		/// <summary>Writes any pending tab spacing and a formatted string that contains the text representation of two objects, followed by a line terminator string, to the output stream.</summary>
		/// <param name="format">A string containing zero or more format items.</param>
		/// <param name="arg0">An object to format.</param>
		/// <param name="arg1">An object to format.</param>
		// Token: 0x0600135F RID: 4959 RVA: 0x00034E78 File Offset: 0x00033078
		public override void WriteLine(string format, object arg0, object arg1)
		{
			this.OutputTabs();
			this.b.WriteLine(format, arg0, arg1);
			this.newline = true;
		}

		/// <summary>Writes any pending tab spacing and a formatted string containing the text representation of an object, followed by a line terminator string, to the output stream. </summary>
		/// <param name="format">A string containing zero or more format items. </param>
		/// <param name="arg0">An object to format.</param>
		// Token: 0x06001360 RID: 4960 RVA: 0x00034E95 File Offset: 0x00033095
		public override void WriteLine(string format, object arg0)
		{
			this.OutputTabs();
			this.b.WriteLine(format, arg0);
			this.newline = true;
		}

		/// <summary>Writes any pending tab spacing and a formatted string that contains the text representation of an object array, followed by a line terminator string, to the output stream.</summary>
		/// <param name="format">A string containing zero or more format items.</param>
		/// <param name="arg">An object array to format. </param>
		// Token: 0x06001361 RID: 4961 RVA: 0x00034EB1 File Offset: 0x000330B1
		public override void WriteLine(string format, params object[] arg)
		{
			this.OutputTabs();
			this.b.WriteLine(format, arg);
			this.newline = true;
		}

		/// <summary>Writes any pending tab spacing and the text representation of a 4-byte unsigned integer, followed by a line terminator string, to the output stream.</summary>
		/// <param name="value">The 4-byte unsigned integer to write to the output stream. </param>
		// Token: 0x06001362 RID: 4962 RVA: 0x00034ECD File Offset: 0x000330CD
		[CLSCompliant(false)]
		public override void WriteLine(uint value)
		{
			this.OutputTabs();
			this.b.WriteLine(value);
			this.newline = true;
		}

		/// <summary>Writes any pending tab spacing and a text string, followed by a line terminator string, to the output stream.</summary>
		/// <param name="s">The string to write to the output stream. </param>
		// Token: 0x06001363 RID: 4963 RVA: 0x00034EE8 File Offset: 0x000330E8
		public override void WriteLine(string s)
		{
			this.OutputTabs();
			this.b.WriteLine(s);
			this.newline = true;
		}

		/// <summary>Writes any pending tab spacing and the text representation of a single-precision floating-point number, followed by a line terminator string, to the output stream.</summary>
		/// <param name="value">The single-precision floating point number to write to the output stream. </param>
		// Token: 0x06001364 RID: 4964 RVA: 0x00034F03 File Offset: 0x00033103
		public override void WriteLine(float value)
		{
			this.OutputTabs();
			this.b.WriteLine(value);
			this.newline = true;
		}

		/// <summary>Writes a string, followed by a line terminator string, to the output stream. This method ignores any specified tab spacing.</summary>
		/// <param name="s">The string to write to the output stream. </param>
		// Token: 0x06001365 RID: 4965 RVA: 0x00034F1E File Offset: 0x0003311E
		public void WriteLineNoTabs(string s)
		{
			this.b.WriteLine(s);
			this.newline = true;
		}

		/// <summary>Gets the encoding that the <see cref="T:System.Web.UI.HtmlTextWriter" /> object uses to write content to the page.</summary>
		/// <returns>The <see cref="T:System.Text.Encoding" /> in which the markup is written to the page.</returns>
		// Token: 0x17000615 RID: 1557
		// (get) Token: 0x06001366 RID: 4966 RVA: 0x00034F33 File Offset: 0x00033133
		public override Encoding Encoding
		{
			get
			{
				return this.b.Encoding;
			}
		}

		/// <summary>Gets or sets the number of tab positions to indent the beginning of each line of markup.</summary>
		/// <returns>The number of tab positions to indent each line.</returns>
		// Token: 0x17000616 RID: 1558
		// (get) Token: 0x06001367 RID: 4967 RVA: 0x00034F40 File Offset: 0x00033140
		// (set) Token: 0x06001368 RID: 4968 RVA: 0x00034F48 File Offset: 0x00033148
		public int Indent
		{
			get
			{
				return this.indent;
			}
			set
			{
				this.indent = value;
			}
		}

		/// <summary>Gets or sets the text writer that writes the inner content of the markup element.</summary>
		/// <returns>A <see cref="T:System.IO.TextWriter" /> that writes the inner markup content.</returns>
		// Token: 0x17000617 RID: 1559
		// (get) Token: 0x06001369 RID: 4969 RVA: 0x00034F51 File Offset: 0x00033151
		// (set) Token: 0x0600136A RID: 4970 RVA: 0x00034F59 File Offset: 0x00033159
		public TextWriter InnerWriter
		{
			get
			{
				return this.b;
			}
			set
			{
				this.b = value;
			}
		}

		/// <summary>Gets or sets the line terminator string used by the <see cref="T:System.Web.UI.HtmlTextWriter" /> object.</summary>
		/// <returns>The line terminator string used by the current <see cref="T:System.Web.UI.HtmlTextWriter" />.</returns>
		// Token: 0x17000618 RID: 1560
		// (get) Token: 0x0600136B RID: 4971 RVA: 0x00034F62 File Offset: 0x00033162
		// (set) Token: 0x0600136C RID: 4972 RVA: 0x00034F6F File Offset: 0x0003316F
		public override string NewLine
		{
			get
			{
				return this.b.NewLine;
			}
			set
			{
				this.b.NewLine = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value for the specified markup element.</summary>
		/// <returns>The markup element that is having its opening tag rendered.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The property value cannot be set. </exception>
		// Token: 0x17000619 RID: 1561
		// (get) Token: 0x0600136D RID: 4973 RVA: 0x00034F7D File Offset: 0x0003317D
		// (set) Token: 0x0600136E RID: 4974 RVA: 0x00034FA4 File Offset: 0x000331A4
		protected HtmlTextWriterTag TagKey
		{
			get
			{
				if (this.tagstack_pos == -1)
				{
					throw new InvalidOperationException();
				}
				return this.tagstack[this.tagstack_pos].key;
			}
			set
			{
				this.tagstack[this.tagstack_pos].key = value;
				this.tagstack[this.tagstack_pos].name = this.GetTagName(value);
			}
		}

		/// <summary>Gets or sets the tag name of the markup element being rendered.</summary>
		/// <returns>The tag name of the markup element being rendered.</returns>
		// Token: 0x1700061A RID: 1562
		// (get) Token: 0x0600136F RID: 4975 RVA: 0x00034FDA File Offset: 0x000331DA
		// (set) Token: 0x06001370 RID: 4976 RVA: 0x00035004 File Offset: 0x00033204
		protected string TagName
		{
			get
			{
				if (this.tagstack_pos == -1)
				{
					throw new InvalidOperationException();
				}
				return this.tagstack[this.tagstack_pos].name;
			}
			set
			{
				this.tagstack[this.tagstack_pos].name = value;
				this.tagstack[this.tagstack_pos].key = this.GetTagKey(value);
				if (this.tagstack[this.tagstack_pos].key != HtmlTextWriterTag.Unknown)
				{
					this.tagstack[this.tagstack_pos].name = this.GetTagName(this.tagstack[this.tagstack_pos].key);
				}
			}
		}

		// Token: 0x1700061B RID: 1563
		// (get) Token: 0x06001371 RID: 4977 RVA: 0x0003508F File Offset: 0x0003328F
		// (set) Token: 0x06001372 RID: 4978 RVA: 0x000350B6 File Offset: 0x000332B6
		private bool TagIgnore
		{
			get
			{
				if (this.tagstack_pos == -1)
				{
					throw new InvalidOperationException();
				}
				return this.tagstack[this.tagstack_pos].ignore;
			}
			set
			{
				if (this.tagstack_pos == -1)
				{
					throw new InvalidOperationException();
				}
				this.tagstack[this.tagstack_pos].ignore = value;
			}
		}

		// Token: 0x06001373 RID: 4979 RVA: 0x000350DE File Offset: 0x000332DE
		internal HttpWriter GetHttpWriter()
		{
			return this.b as HttpWriter;
		}

		// Token: 0x06001374 RID: 4980 RVA: 0x000350EC File Offset: 0x000332EC
		private void NextStyleStack()
		{
			if (this.styles == null)
			{
				this.styles = new HtmlTextWriter.AddedStyle[16];
			}
			int num = this.styles_pos + 1;
			this.styles_pos = num;
			if (num < this.styles.Length)
			{
				return;
			}
			HtmlTextWriter.AddedStyle[] array = new HtmlTextWriter.AddedStyle[this.styles.Length * 2];
			Array.Copy(this.styles, array, this.styles.Length);
			this.styles = array;
		}

		// Token: 0x06001375 RID: 4981 RVA: 0x00035158 File Offset: 0x00033358
		private void NextAttrStack()
		{
			if (this.attrs == null)
			{
				this.attrs = new HtmlTextWriter.AddedAttr[16];
			}
			int num = this.attrs_pos + 1;
			this.attrs_pos = num;
			if (num < this.attrs.Length)
			{
				return;
			}
			HtmlTextWriter.AddedAttr[] array = new HtmlTextWriter.AddedAttr[this.attrs.Length * 2];
			Array.Copy(this.attrs, array, this.attrs.Length);
			this.attrs = array;
		}

		// Token: 0x06001376 RID: 4982 RVA: 0x000351C4 File Offset: 0x000333C4
		private void NextTagStack()
		{
			if (this.tagstack == null)
			{
				this.tagstack = new HtmlTextWriter.AddedTag[16];
			}
			int num = this.tagstack_pos + 1;
			this.tagstack_pos = num;
			if (num < this.tagstack.Length)
			{
				return;
			}
			HtmlTextWriter.AddedTag[] array = new HtmlTextWriter.AddedTag[this.tagstack.Length * 2];
			Array.Copy(this.tagstack, array, this.tagstack.Length);
			this.tagstack = array;
		}

		/// <summary>Checks an attribute to ensure that it can be rendered in the opening tag of a &lt;form&gt; markup element. </summary>
		/// <returns>Always true.</returns>
		/// <param name="attribute">A string that contains the name of the attribute to check. </param>
		// Token: 0x06001377 RID: 4983 RVA: 0x00008B66 File Offset: 0x00006D66
		public virtual bool IsValidFormAttribute(string attribute)
		{
			return true;
		}

		/// <summary>Writes a &lt;br /&gt; markup element to the output stream. </summary>
		// Token: 0x06001378 RID: 4984 RVA: 0x00035230 File Offset: 0x00033430
		public virtual void WriteBreak()
		{
			string tagName = this.GetTagName(HtmlTextWriterTag.Br);
			this.WriteBeginTag(tagName);
			this.Write(" />");
		}

		/// <summary>Encodes the specified text for the requesting device, and then writes it to the output stream. </summary>
		/// <param name="text">The text string to encode and write to the output stream. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="text" /> is null.</exception>
		// Token: 0x06001379 RID: 4985 RVA: 0x00035258 File Offset: 0x00033458
		public virtual void WriteEncodedText(string text)
		{
			this.Write(HttpUtility.HtmlEncode(text));
		}

		/// <summary>Encodes the specified URL, and then writes it to the output stream. The URL might include parameters.</summary>
		/// <param name="url">The URL string to encode and write to the output stream. </param>
		// Token: 0x0600137A RID: 4986 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoNotSupported("")]
		public virtual void WriteEncodedUrl(string url)
		{
			throw new NotImplementedException();
		}

		/// <summary>Encodes the specified URL parameter for the requesting device, and then writes it to the output stream.</summary>
		/// <param name="urlText">The URL parameter string to encode and write to the output stream. </param>
		// Token: 0x0600137B RID: 4987 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoNotSupported("")]
		public virtual void WriteEncodedUrlParameter(string urlText)
		{
			throw new NotImplementedException();
		}

		/// <summary>Writes the specified string, encoding it according to URL requirements.</summary>
		/// <param name="text">The string to encode and write to the output stream. </param>
		/// <param name="argument">true to encode the string as a part of the parameter section of the URL; false to encode the string as part of the path section of the URL. </param>
		// Token: 0x0600137C RID: 4988 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoNotSupported("")]
		protected void WriteUrlEncodedString(string text, bool argument)
		{
			throw new NotImplementedException();
		}

		/// <summary>Writes the opening tag of a &lt;span&gt; element that contains attributes that implement the layout and character formatting of the specified style. </summary>
		/// <param name="style">A <see cref="T:System.Web.UI.WebControls.Style" /> that specifies the layout and formatting to begin applying to the block of markup. </param>
		// Token: 0x0600137D RID: 4989 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoNotSupported("")]
		public virtual void EnterStyle(Style style)
		{
			throw new NotImplementedException();
		}

		/// <summary>Writes the opening tag of a markup element that contains attributes that implement the layout and character formatting of the specified style. </summary>
		/// <param name="style">A <see cref="T:System.Web.UI.WebControls.Style" /> that specifies the layout and formatting to begin applying to the block of markup.</param>
		/// <param name="tag">An <see cref="T:System.Web.UI.HtmlTextWriterTag" /> that specifies the opening tag of the markup element that will contain the style object specified in <paramref name="style" />. </param>
		// Token: 0x0600137E RID: 4990 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoNotSupported("")]
		public virtual void EnterStyle(Style style, HtmlTextWriterTag tag)
		{
			throw new NotImplementedException();
		}

		/// <summary>Writes the closing tag of a &lt;span&gt; element to end the specified layout and character formatting. </summary>
		/// <param name="style">A <see cref="T:System.Web.UI.WebControls.Style" /> that specifies the layout and formatting to close. </param>
		// Token: 0x0600137F RID: 4991 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoNotSupported("")]
		public virtual void ExitStyle(Style style)
		{
			throw new NotImplementedException();
		}

		/// <summary>Writes the closing tag of the specified markup element to end the specified layout and character formatting. </summary>
		/// <param name="style">A <see cref="T:System.Web.UI.WebControls.Style" /> that specifies the layout and formatting to stop applying to the output text.</param>
		/// <param name="tag">An <see cref="T:System.Web.UI.HtmlTextWriterTag" /> that specifies the closing tag of the markup element that contained the attributes that applied the specified style. This must match the key passed in the corresponding <see cref="M:System.Web.UI.HtmlTextWriter.EnterStyle" /> call. </param>
		// Token: 0x06001380 RID: 4992 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoNotSupported("")]
		public virtual void ExitStyle(Style style, HtmlTextWriterTag tag)
		{
			throw new NotImplementedException();
		}

		/// <summary>Notifies an <see cref="T:System.Web.UI.HtmlTextWriter" /> object, or an object of a derived class, that a control is about to be rendered. </summary>
		// Token: 0x06001381 RID: 4993 RVA: 0x0000393A File Offset: 0x00001B3A
		public virtual void BeginRender()
		{
		}

		/// <summary>Notifies an <see cref="T:System.Web.UI.HtmlTextWriter" /> object, or an object of a derived class, that a control has finished rendering. You can use this method to close any markup elements opened in the <see cref="M:System.Web.UI.HtmlTextWriter.BeginRender" /> method.</summary>
		// Token: 0x06001382 RID: 4994 RVA: 0x0000393A File Offset: 0x00001B3A
		public virtual void EndRender()
		{
		}

		// Token: 0x04001442 RID: 5186
		private static readonly Hashtable _tagTable = new Hashtable(HtmlTextWriter.tags.Length, StringComparer.OrdinalIgnoreCase);

		// Token: 0x04001443 RID: 5187
		private static readonly Hashtable _attributeTable = new Hashtable(HtmlTextWriter.htmlattrs.Length, StringComparer.OrdinalIgnoreCase);

		// Token: 0x04001444 RID: 5188
		private static readonly Hashtable _styleTable = new Hashtable(HtmlTextWriter.htmlstyles.Length, StringComparer.OrdinalIgnoreCase);

		// Token: 0x04001445 RID: 5189
		private int indent;

		// Token: 0x04001446 RID: 5190
		private TextWriter b;

		// Token: 0x04001447 RID: 5191
		private string tab_string;

		// Token: 0x04001448 RID: 5192
		private bool newline;

		// Token: 0x04001449 RID: 5193
		private HtmlTextWriter.AddedStyle[] styles;

		// Token: 0x0400144A RID: 5194
		private HtmlTextWriter.AddedAttr[] attrs;

		// Token: 0x0400144B RID: 5195
		private HtmlTextWriter.AddedTag[] tagstack;

		// Token: 0x0400144C RID: 5196
		private int styles_pos = -1;

		// Token: 0x0400144D RID: 5197
		private int attrs_pos = -1;

		// Token: 0x0400144E RID: 5198
		private int tagstack_pos = -1;

		/// <summary>Represents a single tab character.</summary>
		// Token: 0x0400144F RID: 5199
		public const string DefaultTabString = "\t";

		/// <summary>Represents the quotation mark (") character.</summary>
		// Token: 0x04001450 RID: 5200
		public const char DoubleQuoteChar = '"';

		/// <summary>Represents the left angle bracket and slash mark (&lt;/) of the closing tag of a markup element.</summary>
		// Token: 0x04001451 RID: 5201
		public const string EndTagLeftChars = "</";

		/// <summary>Represents the equal sign (=).</summary>
		// Token: 0x04001452 RID: 5202
		public const char EqualsChar = '=';

		/// <summary>Represents an equal sign (=) and a double quotation mark (") together in a string (="). </summary>
		// Token: 0x04001453 RID: 5203
		public const string EqualsDoubleQuoteString = "=\"";

		/// <summary>Represents a space and the self-closing slash mark (/) of a markup tag.</summary>
		// Token: 0x04001454 RID: 5204
		public const string SelfClosingChars = " /";

		/// <summary>Represents the closing slash mark and right angle bracket (/&gt;) of a self-closing markup element.</summary>
		// Token: 0x04001455 RID: 5205
		public const string SelfClosingTagEnd = " />";

		/// <summary>Represents the semicolon (;).</summary>
		// Token: 0x04001456 RID: 5206
		public const char SemicolonChar = ';';

		/// <summary>Represents an apostrophe (').</summary>
		// Token: 0x04001457 RID: 5207
		public const char SingleQuoteChar = '\'';

		/// <summary>Represents the slash mark (/).</summary>
		// Token: 0x04001458 RID: 5208
		public const char SlashChar = '/';

		/// <summary>Represents a space ( ) character.</summary>
		// Token: 0x04001459 RID: 5209
		public const char SpaceChar = ' ';

		/// <summary>Represents the style equals (:) character used to set style attributes equal to values.</summary>
		// Token: 0x0400145A RID: 5210
		public const char StyleEqualsChar = ':';

		/// <summary>Represents the opening angle bracket (&lt;) of a markup tag.</summary>
		// Token: 0x0400145B RID: 5211
		public const char TagLeftChar = '<';

		/// <summary>Represents the closing angle bracket (&gt;) of a markup tag.</summary>
		// Token: 0x0400145C RID: 5212
		public const char TagRightChar = '>';

		// Token: 0x0400145D RID: 5213
		private static HtmlTextWriter.HtmlTag[] tags = new HtmlTextWriter.HtmlTag[]
		{
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Unknown, string.Empty, HtmlTextWriter.TagType.Block),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.A, "a", HtmlTextWriter.TagType.Inline),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Acronym, "acronym", HtmlTextWriter.TagType.Inline),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Address, "address", HtmlTextWriter.TagType.Block),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Area, "area", HtmlTextWriter.TagType.Block),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.B, "b", HtmlTextWriter.TagType.Inline),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Base, "base", HtmlTextWriter.TagType.SelfClosing),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Basefont, "basefont", HtmlTextWriter.TagType.SelfClosing),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Bdo, "bdo", HtmlTextWriter.TagType.Inline),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Bgsound, "bgsound", HtmlTextWriter.TagType.SelfClosing),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Big, "big", HtmlTextWriter.TagType.Inline),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Blockquote, "blockquote", HtmlTextWriter.TagType.Block),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Body, "body", HtmlTextWriter.TagType.Block),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Br, "br", HtmlTextWriter.TagType.Block),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Button, "button", HtmlTextWriter.TagType.Inline),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Caption, "caption", HtmlTextWriter.TagType.Block),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Center, "center", HtmlTextWriter.TagType.Block),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Cite, "cite", HtmlTextWriter.TagType.Inline),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Code, "code", HtmlTextWriter.TagType.Inline),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Col, "col", HtmlTextWriter.TagType.SelfClosing),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Colgroup, "colgroup", HtmlTextWriter.TagType.Block),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Dd, "dd", HtmlTextWriter.TagType.Inline),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Del, "del", HtmlTextWriter.TagType.Inline),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Dfn, "dfn", HtmlTextWriter.TagType.Inline),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Dir, "dir", HtmlTextWriter.TagType.Block),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Div, "div", HtmlTextWriter.TagType.Block),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Dl, "dl", HtmlTextWriter.TagType.Block),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Dt, "dt", HtmlTextWriter.TagType.Inline),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Em, "em", HtmlTextWriter.TagType.Inline),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Embed, "embed", HtmlTextWriter.TagType.SelfClosing),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Fieldset, "fieldset", HtmlTextWriter.TagType.Block),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Font, "font", HtmlTextWriter.TagType.Inline),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Form, "form", HtmlTextWriter.TagType.Block),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Frame, "frame", HtmlTextWriter.TagType.SelfClosing),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Frameset, "frameset", HtmlTextWriter.TagType.Block),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.H1, "h1", HtmlTextWriter.TagType.Block),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.H2, "h2", HtmlTextWriter.TagType.Block),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.H3, "h3", HtmlTextWriter.TagType.Block),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.H4, "h4", HtmlTextWriter.TagType.Block),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.H5, "h5", HtmlTextWriter.TagType.Block),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.H6, "h6", HtmlTextWriter.TagType.Block),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Head, "head", HtmlTextWriter.TagType.Block),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Hr, "hr", HtmlTextWriter.TagType.SelfClosing),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Html, "html", HtmlTextWriter.TagType.Block),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.I, "i", HtmlTextWriter.TagType.Inline),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Iframe, "iframe", HtmlTextWriter.TagType.Block),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Img, "img", HtmlTextWriter.TagType.SelfClosing),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Input, "input", HtmlTextWriter.TagType.SelfClosing),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Ins, "ins", HtmlTextWriter.TagType.Inline),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Isindex, "isindex", HtmlTextWriter.TagType.SelfClosing),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Kbd, "kbd", HtmlTextWriter.TagType.Inline),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Label, "label", HtmlTextWriter.TagType.Inline),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Legend, "legend", HtmlTextWriter.TagType.Block),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Li, "li", HtmlTextWriter.TagType.Inline),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Link, "link", HtmlTextWriter.TagType.SelfClosing),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Map, "map", HtmlTextWriter.TagType.Block),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Marquee, "marquee", HtmlTextWriter.TagType.Block),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Menu, "menu", HtmlTextWriter.TagType.Block),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Meta, "meta", HtmlTextWriter.TagType.SelfClosing),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Nobr, "nobr", HtmlTextWriter.TagType.Inline),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Noframes, "noframes", HtmlTextWriter.TagType.Block),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Noscript, "noscript", HtmlTextWriter.TagType.Block),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Object, "object", HtmlTextWriter.TagType.Block),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Ol, "ol", HtmlTextWriter.TagType.Block),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Option, "option", HtmlTextWriter.TagType.Block),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.P, "p", HtmlTextWriter.TagType.Inline),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Param, "param", HtmlTextWriter.TagType.Block),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Pre, "pre", HtmlTextWriter.TagType.Block),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Q, "q", HtmlTextWriter.TagType.Inline),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Rt, "rt", HtmlTextWriter.TagType.Block),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Ruby, "ruby", HtmlTextWriter.TagType.Block),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.S, "s", HtmlTextWriter.TagType.Inline),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Samp, "samp", HtmlTextWriter.TagType.Inline),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Script, "script", HtmlTextWriter.TagType.Block),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Select, "select", HtmlTextWriter.TagType.Block),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Small, "small", HtmlTextWriter.TagType.Block),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Span, "span", HtmlTextWriter.TagType.Inline),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Strike, "strike", HtmlTextWriter.TagType.Inline),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Strong, "strong", HtmlTextWriter.TagType.Inline),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Style, "style", HtmlTextWriter.TagType.Block),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Sub, "sub", HtmlTextWriter.TagType.Inline),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Sup, "sup", HtmlTextWriter.TagType.Inline),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Table, "table", HtmlTextWriter.TagType.Block),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Tbody, "tbody", HtmlTextWriter.TagType.Block),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Td, "td", HtmlTextWriter.TagType.Inline),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Textarea, "textarea", HtmlTextWriter.TagType.Inline),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Tfoot, "tfoot", HtmlTextWriter.TagType.Block),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Th, "th", HtmlTextWriter.TagType.Inline),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Thead, "thead", HtmlTextWriter.TagType.Block),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Title, "title", HtmlTextWriter.TagType.Block),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Tr, "tr", HtmlTextWriter.TagType.Block),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Tt, "tt", HtmlTextWriter.TagType.Inline),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.U, "u", HtmlTextWriter.TagType.Inline),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Ul, "ul", HtmlTextWriter.TagType.Block),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Var, "var", HtmlTextWriter.TagType.Inline),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Wbr, "wbr", HtmlTextWriter.TagType.SelfClosing),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Xml, "xml", HtmlTextWriter.TagType.Block)
		};

		// Token: 0x0400145E RID: 5214
		private static HtmlTextWriter.HtmlAttribute[] htmlattrs = new HtmlTextWriter.HtmlAttribute[]
		{
			new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Accesskey, "accesskey"),
			new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Align, "align"),
			new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Alt, "alt"),
			new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Background, "background"),
			new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Bgcolor, "bgcolor"),
			new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Border, "border"),
			new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Bordercolor, "bordercolor"),
			new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Cellpadding, "cellpadding"),
			new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Cellspacing, "cellspacing"),
			new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Checked, "checked"),
			new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Class, "class"),
			new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Cols, "cols"),
			new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Colspan, "colspan"),
			new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Disabled, "disabled"),
			new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.For, "for"),
			new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Height, "height"),
			new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Href, "href"),
			new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Id, "id"),
			new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Maxlength, "maxlength"),
			new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Multiple, "multiple"),
			new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Name, "name"),
			new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Nowrap, "nowrap"),
			new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Onchange, "onchange"),
			new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Onclick, "onclick"),
			new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.ReadOnly, "readonly"),
			new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Rows, "rows"),
			new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Rowspan, "rowspan"),
			new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Rules, "rules"),
			new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Selected, "selected"),
			new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Size, "size"),
			new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Src, "src"),
			new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Style, "style"),
			new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Tabindex, "tabindex"),
			new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Target, "target"),
			new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Title, "title"),
			new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Type, "type"),
			new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Valign, "valign"),
			new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Value, "value"),
			new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Width, "width"),
			new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Wrap, "wrap"),
			new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Abbr, "abbr"),
			new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.AutoComplete, "autocomplete"),
			new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Axis, "axis"),
			new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Content, "content"),
			new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Coords, "coords"),
			new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.DesignerRegion, "_designerregion"),
			new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Dir, "dir"),
			new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Headers, "headers"),
			new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Longdesc, "longdesc"),
			new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Rel, "rel"),
			new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Scope, "scope"),
			new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Shape, "shape"),
			new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Usemap, "usemap"),
			new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.VCardName, "vcard_name")
		};

		// Token: 0x0400145F RID: 5215
		private static HtmlTextWriter.HtmlStyle[] htmlstyles = new HtmlTextWriter.HtmlStyle[]
		{
			new HtmlTextWriter.HtmlStyle(HtmlTextWriterStyle.BackgroundColor, "background-color"),
			new HtmlTextWriter.HtmlStyle(HtmlTextWriterStyle.BackgroundImage, "background-image"),
			new HtmlTextWriter.HtmlStyle(HtmlTextWriterStyle.BorderCollapse, "border-collapse"),
			new HtmlTextWriter.HtmlStyle(HtmlTextWriterStyle.BorderColor, "border-color"),
			new HtmlTextWriter.HtmlStyle(HtmlTextWriterStyle.BorderStyle, "border-style"),
			new HtmlTextWriter.HtmlStyle(HtmlTextWriterStyle.BorderWidth, "border-width"),
			new HtmlTextWriter.HtmlStyle(HtmlTextWriterStyle.Color, "color"),
			new HtmlTextWriter.HtmlStyle(HtmlTextWriterStyle.FontFamily, "font-family"),
			new HtmlTextWriter.HtmlStyle(HtmlTextWriterStyle.FontSize, "font-size"),
			new HtmlTextWriter.HtmlStyle(HtmlTextWriterStyle.FontStyle, "font-style"),
			new HtmlTextWriter.HtmlStyle(HtmlTextWriterStyle.FontWeight, "font-weight"),
			new HtmlTextWriter.HtmlStyle(HtmlTextWriterStyle.Height, "height"),
			new HtmlTextWriter.HtmlStyle(HtmlTextWriterStyle.TextDecoration, "text-decoration"),
			new HtmlTextWriter.HtmlStyle(HtmlTextWriterStyle.Width, "width"),
			new HtmlTextWriter.HtmlStyle(HtmlTextWriterStyle.ListStyleImage, "list-style-image"),
			new HtmlTextWriter.HtmlStyle(HtmlTextWriterStyle.ListStyleType, "list-style-type"),
			new HtmlTextWriter.HtmlStyle(HtmlTextWriterStyle.Cursor, "cursor"),
			new HtmlTextWriter.HtmlStyle(HtmlTextWriterStyle.Direction, "direction"),
			new HtmlTextWriter.HtmlStyle(HtmlTextWriterStyle.Display, "display"),
			new HtmlTextWriter.HtmlStyle(HtmlTextWriterStyle.Filter, "filter"),
			new HtmlTextWriter.HtmlStyle(HtmlTextWriterStyle.FontVariant, "font-variant"),
			new HtmlTextWriter.HtmlStyle(HtmlTextWriterStyle.Left, "left"),
			new HtmlTextWriter.HtmlStyle(HtmlTextWriterStyle.Margin, "margin"),
			new HtmlTextWriter.HtmlStyle(HtmlTextWriterStyle.MarginBottom, "margin-bottom"),
			new HtmlTextWriter.HtmlStyle(HtmlTextWriterStyle.MarginLeft, "margin-left"),
			new HtmlTextWriter.HtmlStyle(HtmlTextWriterStyle.MarginRight, "margin-right"),
			new HtmlTextWriter.HtmlStyle(HtmlTextWriterStyle.MarginTop, "margin-top"),
			new HtmlTextWriter.HtmlStyle(HtmlTextWriterStyle.Overflow, "overflow"),
			new HtmlTextWriter.HtmlStyle(HtmlTextWriterStyle.OverflowX, "overflow-x"),
			new HtmlTextWriter.HtmlStyle(HtmlTextWriterStyle.OverflowY, "overflow-y"),
			new HtmlTextWriter.HtmlStyle(HtmlTextWriterStyle.Padding, "padding"),
			new HtmlTextWriter.HtmlStyle(HtmlTextWriterStyle.PaddingBottom, "padding-bottom"),
			new HtmlTextWriter.HtmlStyle(HtmlTextWriterStyle.PaddingLeft, "padding-left"),
			new HtmlTextWriter.HtmlStyle(HtmlTextWriterStyle.PaddingRight, "padding-right"),
			new HtmlTextWriter.HtmlStyle(HtmlTextWriterStyle.PaddingTop, "padding-top"),
			new HtmlTextWriter.HtmlStyle(HtmlTextWriterStyle.Position, "position"),
			new HtmlTextWriter.HtmlStyle(HtmlTextWriterStyle.TextAlign, "text-align"),
			new HtmlTextWriter.HtmlStyle(HtmlTextWriterStyle.VerticalAlign, "vertical-align"),
			new HtmlTextWriter.HtmlStyle(HtmlTextWriterStyle.TextOverflow, "text-overflow"),
			new HtmlTextWriter.HtmlStyle(HtmlTextWriterStyle.Top, "top"),
			new HtmlTextWriter.HtmlStyle(HtmlTextWriterStyle.Visibility, "visibility"),
			new HtmlTextWriter.HtmlStyle(HtmlTextWriterStyle.WhiteSpace, "white-space"),
			new HtmlTextWriter.HtmlStyle(HtmlTextWriterStyle.ZIndex, "z-index")
		};

		// Token: 0x020001D8 RID: 472
		private struct AddedTag
		{
			// Token: 0x04001460 RID: 5216
			public string name;

			// Token: 0x04001461 RID: 5217
			public HtmlTextWriterTag key;

			// Token: 0x04001462 RID: 5218
			public bool ignore;
		}

		// Token: 0x020001D9 RID: 473
		private struct AddedStyle
		{
			// Token: 0x04001463 RID: 5219
			public string name;

			// Token: 0x04001464 RID: 5220
			public HtmlTextWriterStyle key;

			// Token: 0x04001465 RID: 5221
			public string value;
		}

		// Token: 0x020001DA RID: 474
		private struct AddedAttr
		{
			// Token: 0x04001466 RID: 5222
			public string name;

			// Token: 0x04001467 RID: 5223
			public HtmlTextWriterAttribute key;

			// Token: 0x04001468 RID: 5224
			public string value;
		}

		// Token: 0x020001DB RID: 475
		private enum TagType
		{
			// Token: 0x0400146A RID: 5226
			Block,
			// Token: 0x0400146B RID: 5227
			Inline,
			// Token: 0x0400146C RID: 5228
			SelfClosing
		}

		// Token: 0x020001DC RID: 476
		private sealed class HtmlTag
		{
			// Token: 0x06001383 RID: 4995 RVA: 0x00035266 File Offset: 0x00033466
			public HtmlTag(HtmlTextWriterTag k, string n, HtmlTextWriter.TagType tt)
			{
				this.key = k;
				this.name = n;
				this.tag_type = tt;
			}

			// Token: 0x0400146D RID: 5229
			public readonly HtmlTextWriterTag key;

			// Token: 0x0400146E RID: 5230
			public readonly string name;

			// Token: 0x0400146F RID: 5231
			public readonly HtmlTextWriter.TagType tag_type;
		}

		// Token: 0x020001DD RID: 477
		private sealed class HtmlStyle
		{
			// Token: 0x06001384 RID: 4996 RVA: 0x00035283 File Offset: 0x00033483
			public HtmlStyle(HtmlTextWriterStyle k, string n)
			{
				this.key = k;
				this.name = n;
			}

			// Token: 0x04001470 RID: 5232
			public readonly HtmlTextWriterStyle key;

			// Token: 0x04001471 RID: 5233
			public readonly string name;
		}

		// Token: 0x020001DE RID: 478
		private sealed class HtmlAttribute
		{
			// Token: 0x06001385 RID: 4997 RVA: 0x00035299 File Offset: 0x00033499
			public HtmlAttribute(HtmlTextWriterAttribute k, string n)
			{
				this.key = k;
				this.name = n;
			}

			// Token: 0x04001472 RID: 5234
			public readonly HtmlTextWriterAttribute key;

			// Token: 0x04001473 RID: 5235
			public readonly string name;
		}
	}
}
