using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Text;
using Mono.Xml;

namespace System.Security
{
	/// <summary>Represents the XML object model for encoding security objects. This class cannot be inherited.</summary>
	// Token: 0x0200054A RID: 1354
	[ComVisible(true)]
	[Serializable]
	public sealed class SecurityElement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.SecurityElement" /> class with the specified tag.</summary>
		/// <param name="tag">The tag name of an XML element. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="tag" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="tag" /> parameter is invalid in XML. </exception>
		// Token: 0x06003CE4 RID: 15588 RVA: 0x000DB2E8 File Offset: 0x000D94E8
		public SecurityElement(string tag)
			: this(tag, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.SecurityElement" /> class with the specified tag and text.</summary>
		/// <param name="tag">The tag name of the XML element. </param>
		/// <param name="text">The text content within the element. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="tag" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="tag" /> parameter or <paramref name="text" /> parameter is invalid in XML. </exception>
		// Token: 0x06003CE5 RID: 15589 RVA: 0x000DB2F4 File Offset: 0x000D94F4
		public SecurityElement(string tag, string text)
		{
			if (tag == null)
			{
				throw new ArgumentNullException("tag");
			}
			if (!SecurityElement.IsValidTag(tag))
			{
				throw new ArgumentException(Locale.GetText("Invalid XML string") + ": " + tag);
			}
			this.tag = tag;
			this.Text = text;
		}

		// Token: 0x06003CE6 RID: 15590 RVA: 0x000DB348 File Offset: 0x000D9548
		internal SecurityElement(SecurityElement se)
		{
			this.Tag = se.Tag;
			this.Text = se.Text;
			if (se.attributes != null)
			{
				foreach (object obj in se.attributes)
				{
					SecurityElement.SecurityAttribute securityAttribute = (SecurityElement.SecurityAttribute)obj;
					this.AddAttribute(securityAttribute.Name, securityAttribute.Value);
				}
			}
			if (se.children != null)
			{
				foreach (object obj2 in se.children)
				{
					SecurityElement securityElement = (SecurityElement)obj2;
					this.AddChild(securityElement);
				}
			}
		}

		/// <summary>Gets or sets the attributes of an XML element as name/value pairs.</summary>
		/// <returns>The <see cref="T:System.Collections.Hashtable" /> object for the attribute values of the XML element.</returns>
		/// <exception cref="T:System.InvalidCastException">The name or value of the <see cref="T:System.Collections.Hashtable" /> object is invalid. </exception>
		/// <exception cref="T:System.ArgumentException">The name is not a valid XML attribute name.</exception>
		// Token: 0x170009EE RID: 2542
		// (get) Token: 0x06003CE7 RID: 15591 RVA: 0x000DB424 File Offset: 0x000D9624
		// (set) Token: 0x06003CE8 RID: 15592 RVA: 0x000DB4A4 File Offset: 0x000D96A4
		public Hashtable Attributes
		{
			get
			{
				if (this.attributes == null)
				{
					return null;
				}
				Hashtable hashtable = new Hashtable(this.attributes.Count);
				foreach (object obj in this.attributes)
				{
					SecurityElement.SecurityAttribute securityAttribute = (SecurityElement.SecurityAttribute)obj;
					hashtable.Add(securityAttribute.Name, securityAttribute.Value);
				}
				return hashtable;
			}
			set
			{
				if (value == null || value.Count == 0)
				{
					this.attributes.Clear();
					return;
				}
				if (this.attributes == null)
				{
					this.attributes = new ArrayList();
				}
				else
				{
					this.attributes.Clear();
				}
				IDictionaryEnumerator enumerator = value.GetEnumerator();
				while (enumerator.MoveNext())
				{
					this.attributes.Add(new SecurityElement.SecurityAttribute((string)enumerator.Key, (string)enumerator.Value));
				}
			}
		}

		/// <summary>Gets or sets the array of child elements of the XML element.</summary>
		/// <returns>The ordered child elements of the XML element as security elements.</returns>
		/// <exception cref="T:System.ArgumentException">A child of the XML parent node is null. </exception>
		// Token: 0x170009EF RID: 2543
		// (get) Token: 0x06003CE9 RID: 15593 RVA: 0x000DB520 File Offset: 0x000D9720
		// (set) Token: 0x06003CEA RID: 15594 RVA: 0x000DB528 File Offset: 0x000D9728
		public ArrayList Children
		{
			get
			{
				return this.children;
			}
			set
			{
				if (value != null)
				{
					using (IEnumerator enumerator = value.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							if (enumerator.Current == null)
							{
								throw new ArgumentNullException();
							}
						}
					}
				}
				this.children = value;
			}
		}

		/// <summary>Gets or sets the tag name of an XML element.</summary>
		/// <returns>The tag name of an XML element.</returns>
		/// <exception cref="T:System.ArgumentNullException">The tag is null. </exception>
		/// <exception cref="T:System.ArgumentException">The tag is not valid in XML. </exception>
		// Token: 0x170009F0 RID: 2544
		// (get) Token: 0x06003CEB RID: 15595 RVA: 0x000DB584 File Offset: 0x000D9784
		// (set) Token: 0x06003CEC RID: 15596 RVA: 0x000DB58C File Offset: 0x000D978C
		public string Tag
		{
			get
			{
				return this.tag;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("Tag");
				}
				if (!SecurityElement.IsValidTag(value))
				{
					throw new ArgumentException(Locale.GetText("Invalid XML string") + ": " + value);
				}
				this.tag = value;
			}
		}

		/// <summary>Gets or sets the text within an XML element.</summary>
		/// <returns>The value of the text within an XML element.</returns>
		/// <exception cref="T:System.ArgumentException">The text is not valid in XML. </exception>
		// Token: 0x170009F1 RID: 2545
		// (get) Token: 0x06003CED RID: 15597 RVA: 0x000DB5C6 File Offset: 0x000D97C6
		// (set) Token: 0x06003CEE RID: 15598 RVA: 0x000DB5CE File Offset: 0x000D97CE
		public string Text
		{
			get
			{
				return this.text;
			}
			set
			{
				if (value != null && !SecurityElement.IsValidText(value))
				{
					throw new ArgumentException(Locale.GetText("Invalid XML string") + ": " + value);
				}
				this.text = SecurityElement.Unescape(value);
			}
		}

		/// <summary>Adds a name/value attribute to an XML element.</summary>
		/// <param name="name">The name of the attribute. </param>
		/// <param name="value">The value of the attribute. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter or <paramref name="value" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="name" /> parameter or <paramref name="value" /> parameter is invalid in XML.-or- An attribute with the name specified by the <paramref name="name" /> parameter already exists. </exception>
		// Token: 0x06003CEF RID: 15599 RVA: 0x000DB604 File Offset: 0x000D9804
		public void AddAttribute(string name, string value)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (this.GetAttribute(name) != null)
			{
				throw new ArgumentException(Locale.GetText("Duplicate attribute : " + name));
			}
			if (this.attributes == null)
			{
				this.attributes = new ArrayList();
			}
			this.attributes.Add(new SecurityElement.SecurityAttribute(name, value));
		}

		/// <summary>Adds a child element to the XML element.</summary>
		/// <param name="child">The child element to add. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="child" /> parameter is null. </exception>
		// Token: 0x06003CF0 RID: 15600 RVA: 0x000DB672 File Offset: 0x000D9872
		public void AddChild(SecurityElement child)
		{
			if (child == null)
			{
				throw new ArgumentNullException("child");
			}
			if (this.children == null)
			{
				this.children = new ArrayList();
			}
			this.children.Add(child);
		}

		/// <summary>Finds an attribute by name in an XML element.</summary>
		/// <returns>The value associated with the named attribute, or null if no attribute with <paramref name="name" /> exists.</returns>
		/// <param name="name">The name of the attribute for which to search. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is null. </exception>
		// Token: 0x06003CF1 RID: 15601 RVA: 0x000DB6A4 File Offset: 0x000D98A4
		public string Attribute(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			SecurityElement.SecurityAttribute attribute = this.GetAttribute(name);
			if (attribute != null)
			{
				return attribute.Value;
			}
			return null;
		}

		/// <summary>Creates and returns an identical copy of the current <see cref="T:System.Security.SecurityElement" /> object.</summary>
		/// <returns>A copy of the current <see cref="T:System.Security.SecurityElement" /> object.</returns>
		// Token: 0x06003CF2 RID: 15602 RVA: 0x000DB6D2 File Offset: 0x000D98D2
		[ComVisible(false)]
		public SecurityElement Copy()
		{
			return new SecurityElement(this);
		}

		/// <summary>Compares two XML element objects for equality.</summary>
		/// <returns>true if the tag, attribute names and values, child elements, and text fields in the current XML element are identical to their counterparts in the <paramref name="other" /> parameter; otherwise, false.</returns>
		/// <param name="other">An XML element object to which to compare the current XML element object. </param>
		// Token: 0x06003CF3 RID: 15603 RVA: 0x000DB6DC File Offset: 0x000D98DC
		public bool Equal(SecurityElement other)
		{
			if (other == null)
			{
				return false;
			}
			if (this == other)
			{
				return true;
			}
			if (this.text != other.text)
			{
				return false;
			}
			if (this.tag != other.tag)
			{
				return false;
			}
			if (this.attributes == null && other.attributes != null && other.attributes.Count != 0)
			{
				return false;
			}
			if (other.attributes == null && this.attributes != null && this.attributes.Count != 0)
			{
				return false;
			}
			if (this.attributes != null && other.attributes != null)
			{
				if (this.attributes.Count != other.attributes.Count)
				{
					return false;
				}
				foreach (object obj in this.attributes)
				{
					SecurityElement.SecurityAttribute securityAttribute = (SecurityElement.SecurityAttribute)obj;
					SecurityElement.SecurityAttribute attribute = other.GetAttribute(securityAttribute.Name);
					if (attribute == null || securityAttribute.Value != attribute.Value)
					{
						return false;
					}
				}
			}
			if (this.children == null && other.children != null && other.children.Count != 0)
			{
				return false;
			}
			if (other.children == null && this.children != null && this.children.Count != 0)
			{
				return false;
			}
			if (this.children != null && other.children != null)
			{
				if (this.children.Count != other.children.Count)
				{
					return false;
				}
				for (int i = 0; i < this.children.Count; i++)
				{
					if (!((SecurityElement)this.children[i]).Equal((SecurityElement)other.children[i]))
					{
						return false;
					}
				}
			}
			return true;
		}

		/// <summary>Replaces invalid XML characters in a string with their valid XML equivalent.</summary>
		/// <returns>The input string with invalid characters replaced.</returns>
		/// <param name="str">The string within which to escape invalid characters. </param>
		// Token: 0x06003CF4 RID: 15604 RVA: 0x000DB8B0 File Offset: 0x000D9AB0
		public static string Escape(string str)
		{
			if (str == null)
			{
				return null;
			}
			if (str.IndexOfAny(SecurityElement.invalid_chars) == -1)
			{
				return str;
			}
			StringBuilder stringBuilder = new StringBuilder();
			int length = str.Length;
			int i = 0;
			while (i < length)
			{
				char c = str[i];
				if (c <= '&')
				{
					if (c != '"')
					{
						if (c != '&')
						{
							goto IL_0096;
						}
						stringBuilder.Append("&amp;");
					}
					else
					{
						stringBuilder.Append("&quot;");
					}
				}
				else if (c != '\'')
				{
					if (c != '<')
					{
						if (c != '>')
						{
							goto IL_0096;
						}
						stringBuilder.Append("&gt;");
					}
					else
					{
						stringBuilder.Append("&lt;");
					}
				}
				else
				{
					stringBuilder.Append("&apos;");
				}
				IL_009E:
				i++;
				continue;
				IL_0096:
				stringBuilder.Append(c);
				goto IL_009E;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06003CF5 RID: 15605 RVA: 0x000DB96C File Offset: 0x000D9B6C
		private static string Unescape(string str)
		{
			if (str == null)
			{
				return null;
			}
			StringBuilder stringBuilder = new StringBuilder(str);
			stringBuilder.Replace("&lt;", "<");
			stringBuilder.Replace("&gt;", ">");
			stringBuilder.Replace("&amp;", "&");
			stringBuilder.Replace("&quot;", "\"");
			stringBuilder.Replace("&apos;", "'");
			return stringBuilder.ToString();
		}

		/// <summary>Creates a security element from an XML-encoded string.</summary>
		/// <returns>A <see cref="T:System.Security.SecurityElement" /> created from the XML.</returns>
		/// <param name="xml">The XML-encoded string from which to create the security element.</param>
		/// <exception cref="T:System.Security.XmlSyntaxException">
		///   <paramref name="xml" /> contains one or more single quotation mark characters.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="xml" /> is null.</exception>
		// Token: 0x06003CF6 RID: 15606 RVA: 0x000DB9E0 File Offset: 0x000D9BE0
		public static SecurityElement FromString(string xml)
		{
			if (xml == null)
			{
				throw new ArgumentNullException("xml");
			}
			if (xml.Length == 0)
			{
				throw new XmlSyntaxException(Locale.GetText("Empty string."));
			}
			SecurityElement securityElement;
			try
			{
				SecurityParser securityParser = new SecurityParser();
				securityParser.LoadXml(xml);
				securityElement = securityParser.ToXml();
			}
			catch (Exception ex)
			{
				throw new XmlSyntaxException(Locale.GetText("Invalid XML."), ex);
			}
			return securityElement;
		}

		/// <summary>Determines whether a string is a valid attribute name.</summary>
		/// <returns>true if the <paramref name="name" /> parameter is a valid XML attribute name; otherwise, false.</returns>
		/// <param name="name">The attribute name to test for validity. </param>
		// Token: 0x06003CF7 RID: 15607 RVA: 0x000DBA4C File Offset: 0x000D9C4C
		public static bool IsValidAttributeName(string name)
		{
			return name != null && name.IndexOfAny(SecurityElement.invalid_attr_name_chars) == -1;
		}

		/// <summary>Determines whether a string is a valid attribute value.</summary>
		/// <returns>true if the <paramref name="value" /> parameter is a valid XML attribute value; otherwise, false.</returns>
		/// <param name="value">The attribute value to test for validity. </param>
		// Token: 0x06003CF8 RID: 15608 RVA: 0x000DBA61 File Offset: 0x000D9C61
		public static bool IsValidAttributeValue(string value)
		{
			return value != null && value.IndexOfAny(SecurityElement.invalid_attr_value_chars) == -1;
		}

		/// <summary>Determines whether a string is a valid tag.</summary>
		/// <returns>true if the <paramref name="tag" /> parameter is a valid XML tag; otherwise, false.</returns>
		/// <param name="tag">The tag to test for validity. </param>
		// Token: 0x06003CF9 RID: 15609 RVA: 0x000DBA76 File Offset: 0x000D9C76
		public static bool IsValidTag(string tag)
		{
			return tag != null && tag.IndexOfAny(SecurityElement.invalid_tag_chars) == -1;
		}

		/// <summary>Determines whether a string is valid as text within an XML element.</summary>
		/// <returns>true if the <paramref name="text" /> parameter is a valid XML text element; otherwise, false.</returns>
		/// <param name="text">The text to test for validity. </param>
		// Token: 0x06003CFA RID: 15610 RVA: 0x000DBA8B File Offset: 0x000D9C8B
		public static bool IsValidText(string text)
		{
			return text != null && text.IndexOfAny(SecurityElement.invalid_text_chars) == -1;
		}

		/// <summary>Finds a child by its tag name.</summary>
		/// <returns>The first child XML element with the specified tag value, or null if no child element with <paramref name="tag" /> exists.</returns>
		/// <param name="tag">The tag for which to search in child elements. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="tag" /> parameter is null. </exception>
		// Token: 0x06003CFB RID: 15611 RVA: 0x000DBAA0 File Offset: 0x000D9CA0
		public SecurityElement SearchForChildByTag(string tag)
		{
			if (tag == null)
			{
				throw new ArgumentNullException("tag");
			}
			if (this.children == null)
			{
				return null;
			}
			for (int i = 0; i < this.children.Count; i++)
			{
				SecurityElement securityElement = (SecurityElement)this.children[i];
				if (securityElement.tag == tag)
				{
					return securityElement;
				}
			}
			return null;
		}

		/// <summary>Finds a child by its tag name and returns the contained text.</summary>
		/// <returns>The text contents of the first child element with the specified tag value.</returns>
		/// <param name="tag">The tag for which to search in child elements. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="tag" /> is null. </exception>
		// Token: 0x06003CFC RID: 15612 RVA: 0x000DBB00 File Offset: 0x000D9D00
		public string SearchForTextOfTag(string tag)
		{
			if (tag == null)
			{
				throw new ArgumentNullException("tag");
			}
			if (this.tag == tag)
			{
				return this.text;
			}
			if (this.children == null)
			{
				return null;
			}
			for (int i = 0; i < this.children.Count; i++)
			{
				string text = ((SecurityElement)this.children[i]).SearchForTextOfTag(tag);
				if (text != null)
				{
					return text;
				}
			}
			return null;
		}

		/// <summary>Produces a string representation of an XML element and its constituent attributes, child elements, and text.</summary>
		/// <returns>The XML element and its contents.</returns>
		// Token: 0x06003CFD RID: 15613 RVA: 0x000DBB70 File Offset: 0x000D9D70
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			this.ToXml(ref stringBuilder, 0);
			return stringBuilder.ToString();
		}

		// Token: 0x06003CFE RID: 15614 RVA: 0x000DBB94 File Offset: 0x000D9D94
		private void ToXml(ref StringBuilder s, int level)
		{
			s.Append("<");
			s.Append(this.tag);
			if (this.attributes != null)
			{
				s.Append(" ");
				for (int i = 0; i < this.attributes.Count; i++)
				{
					SecurityElement.SecurityAttribute securityAttribute = (SecurityElement.SecurityAttribute)this.attributes[i];
					s.Append(securityAttribute.Name).Append("=\"").Append(SecurityElement.Escape(securityAttribute.Value))
						.Append("\"");
					if (i != this.attributes.Count - 1)
					{
						s.Append(Environment.NewLine);
					}
				}
			}
			if ((this.text == null || this.text == string.Empty) && (this.children == null || this.children.Count == 0))
			{
				s.Append("/>").Append(Environment.NewLine);
				return;
			}
			s.Append(">").Append(SecurityElement.Escape(this.text));
			if (this.children != null)
			{
				s.Append(Environment.NewLine);
				foreach (object obj in this.children)
				{
					((SecurityElement)obj).ToXml(ref s, level + 1);
				}
			}
			s.Append("</").Append(this.tag).Append(">")
				.Append(Environment.NewLine);
		}

		// Token: 0x06003CFF RID: 15615 RVA: 0x000DBD40 File Offset: 0x000D9F40
		internal SecurityElement.SecurityAttribute GetAttribute(string name)
		{
			if (this.attributes != null)
			{
				foreach (object obj in this.attributes)
				{
					SecurityElement.SecurityAttribute securityAttribute = (SecurityElement.SecurityAttribute)obj;
					if (securityAttribute.Name == name)
					{
						return securityAttribute;
					}
				}
			}
			return null;
		}

		// Token: 0x170009F2 RID: 2546
		// (get) Token: 0x06003D00 RID: 15616 RVA: 0x000DB584 File Offset: 0x000D9784
		internal string m_strTag
		{
			get
			{
				return this.tag;
			}
		}

		// Token: 0x170009F3 RID: 2547
		// (get) Token: 0x06003D01 RID: 15617 RVA: 0x000DB5C6 File Offset: 0x000D97C6
		// (set) Token: 0x06003D02 RID: 15618 RVA: 0x000DBDB0 File Offset: 0x000D9FB0
		internal string m_strText
		{
			get
			{
				return this.text;
			}
			set
			{
				this.text = value;
			}
		}

		// Token: 0x170009F4 RID: 2548
		// (get) Token: 0x06003D03 RID: 15619 RVA: 0x000DBDB9 File Offset: 0x000D9FB9
		internal ArrayList m_lAttributes
		{
			get
			{
				return this.attributes;
			}
		}

		// Token: 0x170009F5 RID: 2549
		// (get) Token: 0x06003D04 RID: 15620 RVA: 0x000DB520 File Offset: 0x000D9720
		internal ArrayList InternalChildren
		{
			get
			{
				return this.children;
			}
		}

		// Token: 0x06003D05 RID: 15621 RVA: 0x000DBDC4 File Offset: 0x000D9FC4
		internal string SearchForTextOfLocalName(string strLocalName)
		{
			if (strLocalName == null)
			{
				throw new ArgumentNullException("strLocalName");
			}
			if (this.tag == null)
			{
				return null;
			}
			if (this.tag.Equals(strLocalName) || this.tag.EndsWith(":" + strLocalName, StringComparison.Ordinal))
			{
				return SecurityElement.Unescape(this.text);
			}
			if (this.children == null)
			{
				return null;
			}
			foreach (object obj in this.children)
			{
				string text = ((SecurityElement)obj).SearchForTextOfLocalName(strLocalName);
				if (text != null)
				{
					return text;
				}
			}
			return null;
		}

		// Token: 0x04001F66 RID: 8038
		private string text;

		// Token: 0x04001F67 RID: 8039
		private string tag;

		// Token: 0x04001F68 RID: 8040
		private ArrayList attributes;

		// Token: 0x04001F69 RID: 8041
		private ArrayList children;

		// Token: 0x04001F6A RID: 8042
		private static readonly char[] invalid_tag_chars = new char[] { ' ', '<', '>' };

		// Token: 0x04001F6B RID: 8043
		private static readonly char[] invalid_text_chars = new char[] { '<', '>' };

		// Token: 0x04001F6C RID: 8044
		private static readonly char[] invalid_attr_name_chars = new char[] { ' ', '<', '>' };

		// Token: 0x04001F6D RID: 8045
		private static readonly char[] invalid_attr_value_chars = new char[] { '"', '<', '>' };

		// Token: 0x04001F6E RID: 8046
		private static readonly char[] invalid_chars = new char[] { '<', '>', '"', '\'', '&' };

		// Token: 0x0200054B RID: 1355
		internal class SecurityAttribute
		{
			// Token: 0x06003D07 RID: 15623 RVA: 0x000DBED0 File Offset: 0x000DA0D0
			public SecurityAttribute(string name, string value)
			{
				if (!SecurityElement.IsValidAttributeName(name))
				{
					throw new ArgumentException(Locale.GetText("Invalid XML attribute name") + ": " + name);
				}
				if (!SecurityElement.IsValidAttributeValue(value))
				{
					throw new ArgumentException(Locale.GetText("Invalid XML attribute value") + ": " + value);
				}
				this._name = name;
				this._value = SecurityElement.Unescape(value);
			}

			// Token: 0x170009F6 RID: 2550
			// (get) Token: 0x06003D08 RID: 15624 RVA: 0x000DBF3C File Offset: 0x000DA13C
			public string Name
			{
				get
				{
					return this._name;
				}
			}

			// Token: 0x170009F7 RID: 2551
			// (get) Token: 0x06003D09 RID: 15625 RVA: 0x000DBF44 File Offset: 0x000DA144
			public string Value
			{
				get
				{
					return this._value;
				}
			}

			// Token: 0x04001F6F RID: 8047
			private string _name;

			// Token: 0x04001F70 RID: 8048
			private string _value;
		}
	}
}
