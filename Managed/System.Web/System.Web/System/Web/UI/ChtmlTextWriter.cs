using System;
using System.Collections;
using System.IO;

namespace System.Web.UI
{
	/// <summary>Writes a series of cHTML-specific characters and text to the output stream of an ASP.NET server control. The <see cref="T:System.Web.UI.ChtmlTextWriter" /> class provides formatting capabilities that ASP.NET server controls use when rendering cHTML content to clients.</summary>
	// Token: 0x020001AA RID: 426
	public class ChtmlTextWriter : Html32TextWriter
	{
		// Token: 0x06001058 RID: 4184 RVA: 0x0002CA50 File Offset: 0x0002AC50
		static ChtmlTextWriter()
		{
			ChtmlTextWriter.SetupGlobalSuppressedAttrs(ChtmlTextWriter.global_suppressed_attributes);
		}

		// Token: 0x06001059 RID: 4185 RVA: 0x0002CADF File Offset: 0x0002ACDF
		private static void SetupGlobalSuppressedAttrs(string[] attrs)
		{
			ChtmlTextWriter.global_suppressed_attrs = new Hashtable();
			ChtmlTextWriter.PopulateHash(ChtmlTextWriter.global_suppressed_attrs, ChtmlTextWriter.global_suppressed_attributes);
		}

		// Token: 0x0600105A RID: 4186 RVA: 0x0002CAFC File Offset: 0x0002ACFC
		private static void PopulateHash(Hashtable hash, string[] keys)
		{
			foreach (string text in keys)
			{
				hash.Add(text, true);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.ChtmlTextWriter" /> class that uses the <see cref="F:System.Web.UI.HtmlTextWriter.DefaultTabString" /> constant to indent lines.</summary>
		/// <param name="writer">The <see cref="T:System.IO.TextWriter" /> that renders the markup content. </param>
		// Token: 0x0600105B RID: 4187 RVA: 0x0002CB2A File Offset: 0x0002AD2A
		public ChtmlTextWriter(TextWriter writer)
			: this(writer, "\t")
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.ChtmlTextWriter" /> class with the specified line indentation.</summary>
		/// <param name="writer">The <see cref="T:System.IO.TextWriter" /> that renders the markup content. </param>
		/// <param name="tabString">The number of spaces defined in the <see cref="P:System.Web.UI.HtmlTextWriter.Indent" />. </param>
		// Token: 0x0600105C RID: 4188 RVA: 0x0002CB38 File Offset: 0x0002AD38
		public ChtmlTextWriter(TextWriter writer, string tabString)
			: base(writer, tabString)
		{
			foreach (string text in ChtmlTextWriter.recognized_attributes)
			{
				this.recognized_attrs.Add(text, new Hashtable());
			}
			this.SetupSuppressedAttrs();
		}

		// Token: 0x0600105D RID: 4189 RVA: 0x0002CBAC File Offset: 0x0002ADAC
		private void SetupSuppressedAttrs()
		{
			string[] array = new string[] { "accesskey", "cellspacing", "cellpadding", "gridlines", "rules" };
			string[] array2 = new string[] { "cellspacing", "cellpadding", "gridlines", "rules" };
			ChtmlTextWriter.Init("div", array, this.suppressed_attrs);
			ChtmlTextWriter.Init("span", array2, this.suppressed_attrs);
		}

		// Token: 0x0600105E RID: 4190 RVA: 0x0002CC34 File Offset: 0x0002AE34
		private static void Init(string key, string[] attrs, Hashtable container)
		{
			Hashtable hashtable = new Hashtable(attrs.Length);
			ChtmlTextWriter.PopulateHash(hashtable, attrs);
			container.Add(key, hashtable);
		}

		/// <summary>Gets a <see cref="T:System.Collections.Hashtable" /> object of globally suppressed attributes that cannot be rendered on cHTML elements. </summary>
		/// <returns>A <see cref="T:System.Collections.Hashtable" /> of globally suppressed cHTML attributes.</returns>
		// Token: 0x17000557 RID: 1367
		// (get) Token: 0x0600105F RID: 4191 RVA: 0x0002CC59 File Offset: 0x0002AE59
		protected Hashtable GlobalSuppressedAttributes
		{
			get
			{
				return ChtmlTextWriter.global_suppressed_attrs;
			}
		}

		/// <summary>Gets a <see cref="T:System.Collections.Hashtable" /> object of recognized attributes that could be rendered on cHTML elements.</summary>
		/// <returns>A <see cref="T:System.Collections.Hashtable" /> of recognized cHTML attributes.</returns>
		// Token: 0x17000558 RID: 1368
		// (get) Token: 0x06001060 RID: 4192 RVA: 0x0002CC60 File Offset: 0x0002AE60
		protected Hashtable RecognizedAttributes
		{
			get
			{
				return this.recognized_attrs;
			}
		}

		/// <summary>Gets a <see cref="T:System.Collections.Hashtable" /> object of user-specified suppressed attributes that are not rendered on cHTML elements.</summary>
		/// <returns>A <see cref="T:System.Collections.Hashtable" /> of suppressed cHTML attributes.</returns>
		// Token: 0x17000559 RID: 1369
		// (get) Token: 0x06001061 RID: 4193 RVA: 0x0002CC68 File Offset: 0x0002AE68
		protected Hashtable SuppressedAttributes
		{
			get
			{
				return this.suppressed_attrs;
			}
		}

		/// <summary>Adds an attribute to a cHTML element of the <see cref="T:System.Web.UI.ChtmlTextWriter" /> object.</summary>
		/// <param name="elementName">The cHTML element to add the attribute to.</param>
		/// <param name="attributeName">The attribute to add to <paramref name="elementName" />.</param>
		// Token: 0x06001062 RID: 4194 RVA: 0x0002CC70 File Offset: 0x0002AE70
		public virtual void AddRecognizedAttribute(string elementName, string attributeName)
		{
			Hashtable hashtable = (Hashtable)this.recognized_attrs[elementName];
			if (hashtable == null)
			{
				hashtable = new Hashtable();
				hashtable.Add(attributeName, true);
				this.recognized_attrs.Add(elementName, hashtable);
				return;
			}
			hashtable.Add(attributeName, true);
		}

		/// <summary>Removes an attribute of a cHTML element of the <see cref="T:System.Web.UI.ChtmlTextWriter" /> object.</summary>
		/// <param name="elementName">The cHTML element to remove an attribute from.</param>
		/// <param name="attributeName">The attribute to remove from <paramref name="elementName" />.</param>
		// Token: 0x06001063 RID: 4195 RVA: 0x0002CCC0 File Offset: 0x0002AEC0
		public virtual void RemoveRecognizedAttribute(string elementName, string attributeName)
		{
			Hashtable hashtable = (Hashtable)this.recognized_attrs[elementName];
			if (hashtable != null)
			{
				hashtable.Remove(attributeName);
			}
		}

		/// <summary>Writes a br element to the cHTML output stream.</summary>
		// Token: 0x06001064 RID: 4196 RVA: 0x0002CCEC File Offset: 0x0002AEEC
		public override void WriteBreak()
		{
			string tagName = this.GetTagName(HtmlTextWriterTag.Br);
			this.WriteBeginTag(tagName);
			this.Write('>');
		}

		/// <summary>Encodes the specified text for the requesting device, and then writes it to the output stream. </summary>
		/// <param name="text">The text string to encode and write to the output stream. </param>
		// Token: 0x06001065 RID: 4197 RVA: 0x0002CD11 File Offset: 0x0002AF11
		public override void WriteEncodedText(string text)
		{
			base.WriteEncodedText(text);
		}

		/// <summary>Determines whether the specified cHTML attribute and its value are rendered to the requesting page. You can override the <see cref="M:System.Web.UI.ChtmlTextWriter.OnAttributeRender(System.String,System.String,System.Web.UI.HtmlTextWriterAttribute)" /> method in classes that derive from the <see cref="T:System.Web.UI.ChtmlTextWriter" /> class to filter out attributes that you do not want to render on devices that support cHTML.</summary>
		/// <returns>true to write the attribute and its value to the <see cref="T:System.Web.UI.ChtmlTextWriter" /> output stream; otherwise, false.</returns>
		/// <param name="name">The cHTML attribute to render. </param>
		/// <param name="value">The value assigned to <paramref name="name" />. </param>
		/// <param name="key">The <see cref="T:System.Web.UI.HtmlTextWriterAttribute" /> associated with <paramref name="name" />. </param>
		// Token: 0x06001066 RID: 4198 RVA: 0x0002CD1A File Offset: 0x0002AF1A
		protected override bool OnAttributeRender(string name, string value, HtmlTextWriterAttribute key)
		{
			return (bool)this.attr_render[null];
		}

		/// <summary>Determines whether the specified cHTML markup style attribute and its value can be rendered to the current markup element.</summary>
		/// <returns>true if the style can be rendered; otherwise, false.</returns>
		/// <param name="name">A string containing the name of the style attribute to render. </param>
		/// <param name="value">A string containing the value that is assigned to <paramref name="name" />. </param>
		/// <param name="key">The <see cref="T:System.Web.UI.HtmlTextWriterStyle" /> associated with <paramref name="name" />.</param>
		// Token: 0x06001067 RID: 4199 RVA: 0x0002CD2D File Offset: 0x0002AF2D
		protected override bool OnStyleAttributeRender(string name, string value, HtmlTextWriterStyle key)
		{
			return key == HtmlTextWriterStyle.Display;
		}

		/// <summary>Determines whether the specified cHTML markup element is rendered to the requesting page. </summary>
		/// <returns>true if the specified cHTML markup element can be rendered; otherwise, false.</returns>
		/// <param name="name">A string containing the name of the cHTML element to render.</param>
		/// <param name="key">The <see cref="T:System.Web.UI.HtmlTextWriterTag" /> associated with <paramref name="name" />.</param>
		// Token: 0x06001068 RID: 4200 RVA: 0x0002CD34 File Offset: 0x0002AF34
		protected override bool OnTagRender(string name, HtmlTextWriterTag key)
		{
			return key != HtmlTextWriterTag.Span;
		}

		// Token: 0x0400136C RID: 4972
		private static Hashtable global_suppressed_attrs;

		// Token: 0x0400136D RID: 4973
		private static string[] global_suppressed_attributes = new string[] { "onclick", "ondblclick", "onmousedown", "onmouseup", "onmouseover", "onmousemove", "onmouseout", "onkeypress", "onkeydown", "onkeyup" };

		// Token: 0x0400136E RID: 4974
		private static string[] recognized_attributes = new string[] { "div", "span" };

		// Token: 0x0400136F RID: 4975
		private Hashtable recognized_attrs = new Hashtable(ChtmlTextWriter.recognized_attributes.Length);

		// Token: 0x04001370 RID: 4976
		private Hashtable suppressed_attrs = new Hashtable(ChtmlTextWriter.recognized_attributes.Length);

		// Token: 0x04001371 RID: 4977
		private Hashtable attr_render = new Hashtable();
	}
}
