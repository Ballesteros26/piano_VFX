using System;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x020005F6 RID: 1526
	internal class WhitespaceRuleReader : XmlWrappingReader
	{
		// Token: 0x06003B6C RID: 15212 RVA: 0x0014E38C File Offset: 0x0014C58C
		public static XmlReader CreateReader(XmlReader baseReader, WhitespaceRuleLookup wsRules)
		{
			if (wsRules == null)
			{
				return baseReader;
			}
			XmlReaderSettings settings = baseReader.Settings;
			if (settings != null)
			{
				if (settings.IgnoreWhitespace)
				{
					return baseReader;
				}
			}
			else
			{
				XmlTextReader xmlTextReader = baseReader as XmlTextReader;
				if (xmlTextReader != null && xmlTextReader.WhitespaceHandling == WhitespaceHandling.None)
				{
					return baseReader;
				}
				XmlTextReaderImpl xmlTextReaderImpl = baseReader as XmlTextReaderImpl;
				if (xmlTextReaderImpl != null && xmlTextReaderImpl.WhitespaceHandling == WhitespaceHandling.None)
				{
					return baseReader;
				}
			}
			return new WhitespaceRuleReader(baseReader, wsRules);
		}

		// Token: 0x06003B6D RID: 15213 RVA: 0x0014E3E4 File Offset: 0x0014C5E4
		private WhitespaceRuleReader(XmlReader baseReader, WhitespaceRuleLookup wsRules)
			: base(baseReader)
		{
			this.val = null;
			this.stkStrip = new BitStack();
			this.shouldStrip = false;
			this.preserveAdjacent = false;
			this.wsRules = wsRules;
			this.wsRules.Atomize(baseReader.NameTable);
		}

		// Token: 0x17000C18 RID: 3096
		// (get) Token: 0x06003B6E RID: 15214 RVA: 0x0014E43B File Offset: 0x0014C63B
		public override string Value
		{
			get
			{
				if (this.val != null)
				{
					return this.val;
				}
				return base.Value;
			}
		}

		// Token: 0x06003B6F RID: 15215 RVA: 0x0014E454 File Offset: 0x0014C654
		public override bool Read()
		{
			XmlCharType instance = XmlCharType.Instance;
			string text = null;
			this.val = null;
			while (base.Read())
			{
				XmlNodeType nodeType = base.NodeType;
				if (nodeType != XmlNodeType.Element)
				{
					if (nodeType - XmlNodeType.Text > 1)
					{
						switch (nodeType)
						{
						case XmlNodeType.Whitespace:
						case XmlNodeType.SignificantWhitespace:
							break;
						case XmlNodeType.EndElement:
							this.shouldStrip = this.stkStrip.PopBit();
							goto IL_010E;
						case XmlNodeType.EndEntity:
							continue;
						default:
							goto IL_010E;
						}
					}
					else
					{
						if (this.preserveAdjacent)
						{
							return true;
						}
						if (!this.shouldStrip)
						{
							goto IL_010E;
						}
						if (!instance.IsOnlyWhitespace(base.Value))
						{
							if (text != null)
							{
								this.val = text + base.Value;
							}
							this.preserveAdjacent = true;
							return true;
						}
					}
					if (this.preserveAdjacent)
					{
						return true;
					}
					if (this.shouldStrip)
					{
						if (text == null)
						{
							text = base.Value;
							continue;
						}
						text += base.Value;
						continue;
					}
				}
				else if (!base.IsEmptyElement)
				{
					this.stkStrip.PushBit(this.shouldStrip);
					this.shouldStrip = this.wsRules.ShouldStripSpace(base.LocalName, base.NamespaceURI) && base.XmlSpace != XmlSpace.Preserve;
				}
				IL_010E:
				this.preserveAdjacent = false;
				return true;
			}
			return false;
		}

		// Token: 0x0400272C RID: 10028
		private WhitespaceRuleLookup wsRules;

		// Token: 0x0400272D RID: 10029
		private BitStack stkStrip;

		// Token: 0x0400272E RID: 10030
		private bool shouldStrip;

		// Token: 0x0400272F RID: 10031
		private bool preserveAdjacent;

		// Token: 0x04002730 RID: 10032
		private string val;

		// Token: 0x04002731 RID: 10033
		private XmlCharType xmlCharType = XmlCharType.Instance;
	}
}
