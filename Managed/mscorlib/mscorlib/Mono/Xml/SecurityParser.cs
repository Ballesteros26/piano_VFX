using System;
using System.Collections;
using System.IO;
using System.Security;

namespace Mono.Xml
{
	// Token: 0x02000025 RID: 37
	internal class SecurityParser : SmallXmlParser, SmallXmlParser.IContentHandler
	{
		// Token: 0x060000AB RID: 171 RVA: 0x000040B0 File Offset: 0x000022B0
		public SecurityParser()
		{
			this.stack = new Stack();
		}

		// Token: 0x060000AC RID: 172 RVA: 0x000040C3 File Offset: 0x000022C3
		public void LoadXml(string xml)
		{
			this.root = null;
			this.stack.Clear();
			base.Parse(new StringReader(xml), this);
		}

		// Token: 0x060000AD RID: 173 RVA: 0x000040E4 File Offset: 0x000022E4
		public SecurityElement ToXml()
		{
			return this.root;
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00002194 File Offset: 0x00000394
		public void OnStartParsing(SmallXmlParser parser)
		{
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00002194 File Offset: 0x00000394
		public void OnProcessingInstruction(string name, string text)
		{
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00002194 File Offset: 0x00000394
		public void OnIgnorableWhitespace(string s)
		{
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x000040EC File Offset: 0x000022EC
		public void OnStartElement(string name, SmallXmlParser.IAttrList attrs)
		{
			SecurityElement securityElement = new SecurityElement(name);
			if (this.root == null)
			{
				this.root = securityElement;
				this.current = securityElement;
			}
			else
			{
				((SecurityElement)this.stack.Peek()).AddChild(securityElement);
			}
			this.stack.Push(securityElement);
			this.current = securityElement;
			int length = attrs.Length;
			for (int i = 0; i < length; i++)
			{
				this.current.AddAttribute(attrs.GetName(i), SecurityElement.Escape(attrs.GetValue(i)));
			}
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00004172 File Offset: 0x00002372
		public void OnEndElement(string name)
		{
			this.current = (SecurityElement)this.stack.Pop();
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x0000418A File Offset: 0x0000238A
		public void OnChars(string ch)
		{
			this.current.Text = SecurityElement.Escape(ch);
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00002194 File Offset: 0x00000394
		public void OnEndParsing(SmallXmlParser parser)
		{
		}

		// Token: 0x040003B7 RID: 951
		private SecurityElement root;

		// Token: 0x040003B8 RID: 952
		private SecurityElement current;

		// Token: 0x040003B9 RID: 953
		private Stack stack;
	}
}
