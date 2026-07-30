using System;
using System.Collections;
using System.Globalization;
using System.Text;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x0200053D RID: 1341
	internal abstract class SequentialOutput : RecordOutput
	{
		// Token: 0x0600363C RID: 13884 RVA: 0x0012FD68 File Offset: 0x0012DF68
		private void CacheOuptutProps(XsltOutput output)
		{
			this.output = output;
			this.isXmlOutput = this.output.Method == XsltOutput.OutputMethod.Xml;
			this.isHtmlOutput = this.output.Method == XsltOutput.OutputMethod.Html;
			this.cdataElements = this.output.CDataElements;
			this.indentOutput = this.output.Indent;
			this.outputDoctype = this.output.DoctypeSystem != null || (this.isHtmlOutput && this.output.DoctypePublic != null);
			this.outputXmlDecl = this.isXmlOutput && !this.output.OmitXmlDeclaration && !this.omitXmlDeclCalled;
		}

		// Token: 0x0600363D RID: 13885 RVA: 0x0012FE1C File Offset: 0x0012E01C
		internal SequentialOutput(Processor processor)
		{
			this.processor = processor;
			this.CacheOuptutProps(processor.Output);
		}

		// Token: 0x0600363E RID: 13886 RVA: 0x0012FE49 File Offset: 0x0012E049
		public void OmitXmlDecl()
		{
			this.omitXmlDeclCalled = true;
			this.outputXmlDecl = false;
		}

		// Token: 0x0600363F RID: 13887 RVA: 0x0012FE5C File Offset: 0x0012E05C
		private void WriteStartElement(RecordBuilder record)
		{
			BuilderInfo mainNode = record.MainNode;
			HtmlElementProps htmlElementProps = null;
			if (this.isHtmlOutput)
			{
				if (mainNode.Prefix.Length == 0)
				{
					htmlElementProps = mainNode.htmlProps;
					if (htmlElementProps == null && mainNode.search)
					{
						htmlElementProps = HtmlElementProps.GetProps(mainNode.LocalName);
					}
					record.Manager.CurrentElementScope.HtmlElementProps = htmlElementProps;
					mainNode.IsEmptyTag = false;
				}
			}
			else if (this.isXmlOutput && mainNode.Depth == 0)
			{
				if (this.secondRoot && (this.output.DoctypeSystem != null || this.output.Standalone))
				{
					throw XsltException.Create("There are multiple root elements in the output XML.", Array.Empty<string>());
				}
				this.secondRoot = true;
			}
			if (this.outputDoctype)
			{
				this.WriteDoctype(mainNode);
				this.outputDoctype = false;
			}
			if (this.cdataElements != null && this.cdataElements.Contains(new XmlQualifiedName(mainNode.LocalName, mainNode.NamespaceURI)) && this.isXmlOutput)
			{
				record.Manager.CurrentElementScope.ToCData = true;
			}
			this.Indent(record);
			this.Write('<');
			this.WriteName(mainNode.Prefix, mainNode.LocalName);
			this.WriteAttributes(record.AttributeList, record.AttributeCount, htmlElementProps);
			if (mainNode.IsEmptyTag)
			{
				this.Write(" />");
			}
			else
			{
				this.Write('>');
			}
			if (htmlElementProps != null && htmlElementProps.Head)
			{
				BuilderInfo builderInfo = mainNode;
				int num = builderInfo.Depth;
				builderInfo.Depth = num + 1;
				this.Indent(record);
				BuilderInfo builderInfo2 = mainNode;
				num = builderInfo2.Depth;
				builderInfo2.Depth = num - 1;
				this.Write("<META http-equiv=\"Content-Type\" content=\"");
				this.Write(this.output.MediaType);
				this.Write("; charset=");
				this.Write(this.encoding.WebName);
				this.Write("\">");
			}
		}

		// Token: 0x06003640 RID: 13888 RVA: 0x00130028 File Offset: 0x0012E228
		private void WriteTextNode(RecordBuilder record)
		{
			BuilderInfo mainNode = record.MainNode;
			OutputScope currentElementScope = record.Manager.CurrentElementScope;
			currentElementScope.Mixed = true;
			if (currentElementScope.HtmlElementProps != null && currentElementScope.HtmlElementProps.NoEntities)
			{
				this.Write(mainNode.Value);
				return;
			}
			if (currentElementScope.ToCData)
			{
				this.WriteCDataSection(mainNode.Value);
				return;
			}
			this.WriteTextNode(mainNode);
		}

		// Token: 0x06003641 RID: 13889 RVA: 0x00130090 File Offset: 0x0012E290
		private void WriteTextNode(BuilderInfo node)
		{
			for (int i = 0; i < node.TextInfoCount; i++)
			{
				string text = node.TextInfo[i];
				if (text == null)
				{
					i++;
					this.Write(node.TextInfo[i]);
				}
				else
				{
					this.WriteWithReplace(text, SequentialOutput.s_TextValueFind, SequentialOutput.s_TextValueReplace);
				}
			}
		}

		// Token: 0x06003642 RID: 13890 RVA: 0x001300DF File Offset: 0x0012E2DF
		private void WriteCDataSection(string value)
		{
			this.Write("<![CDATA[");
			this.WriteCData(value);
			this.Write("]]>");
		}

		// Token: 0x06003643 RID: 13891 RVA: 0x00130100 File Offset: 0x0012E300
		private void WriteDoctype(BuilderInfo mainNode)
		{
			this.Indent(0);
			this.Write("<!DOCTYPE ");
			if (this.isXmlOutput)
			{
				this.WriteName(mainNode.Prefix, mainNode.LocalName);
			}
			else
			{
				this.WriteName(string.Empty, "html");
			}
			this.Write(' ');
			if (this.output.DoctypePublic != null)
			{
				this.Write("PUBLIC ");
				this.Write('"');
				this.Write(this.output.DoctypePublic);
				this.Write("\" ");
			}
			else
			{
				this.Write("SYSTEM ");
			}
			if (this.output.DoctypeSystem != null)
			{
				this.Write('"');
				this.Write(this.output.DoctypeSystem);
				this.Write('"');
			}
			this.Write('>');
		}

		// Token: 0x06003644 RID: 13892 RVA: 0x001301D4 File Offset: 0x0012E3D4
		private void WriteXmlDeclaration()
		{
			this.outputXmlDecl = false;
			this.Indent(0);
			this.Write("<?");
			this.WriteName(string.Empty, "xml");
			this.Write(" version=\"1.0\"");
			if (this.encoding != null)
			{
				this.Write(" encoding=\"");
				this.Write(this.encoding.WebName);
				this.Write('"');
			}
			if (this.output.HasStandalone)
			{
				this.Write(" standalone=\"");
				this.Write(this.output.Standalone ? "yes" : "no");
				this.Write('"');
			}
			this.Write("?>");
		}

		// Token: 0x06003645 RID: 13893 RVA: 0x0013028B File Offset: 0x0012E48B
		private void WriteProcessingInstruction(RecordBuilder record)
		{
			this.Indent(record);
			this.WriteProcessingInstruction(record.MainNode);
		}

		// Token: 0x06003646 RID: 13894 RVA: 0x001302A0 File Offset: 0x0012E4A0
		private void WriteProcessingInstruction(BuilderInfo node)
		{
			this.Write("<?");
			this.WriteName(node.Prefix, node.LocalName);
			this.Write(' ');
			this.Write(node.Value);
			if (this.isHtmlOutput)
			{
				this.Write('>');
				return;
			}
			this.Write("?>");
		}

		// Token: 0x06003647 RID: 13895 RVA: 0x001302FC File Offset: 0x0012E4FC
		private void WriteEndElement(RecordBuilder record)
		{
			BuilderInfo mainNode = record.MainNode;
			HtmlElementProps htmlElementProps = record.Manager.CurrentElementScope.HtmlElementProps;
			if (htmlElementProps != null && htmlElementProps.Empty)
			{
				return;
			}
			this.Indent(record);
			this.Write("</");
			this.WriteName(record.MainNode.Prefix, record.MainNode.LocalName);
			this.Write('>');
		}

		// Token: 0x06003648 RID: 13896 RVA: 0x00130364 File Offset: 0x0012E564
		public Processor.OutputResult RecordDone(RecordBuilder record)
		{
			if (this.output.Method == XsltOutput.OutputMethod.Unknown)
			{
				if (!this.DecideDefaultOutput(record.MainNode))
				{
					this.CacheRecord(record);
				}
				else
				{
					this.OutputCachedRecords();
					this.OutputRecord(record);
				}
			}
			else
			{
				this.OutputRecord(record);
			}
			record.Reset();
			return Processor.OutputResult.Continue;
		}

		// Token: 0x06003649 RID: 13897 RVA: 0x001303B3 File Offset: 0x0012E5B3
		public void TheEnd()
		{
			this.OutputCachedRecords();
			this.Close();
		}

		// Token: 0x0600364A RID: 13898 RVA: 0x001303C4 File Offset: 0x0012E5C4
		private bool DecideDefaultOutput(BuilderInfo node)
		{
			XsltOutput.OutputMethod outputMethod = XsltOutput.OutputMethod.Xml;
			XmlNodeType nodeType = node.NodeType;
			if (nodeType != XmlNodeType.Element)
			{
				if (nodeType != XmlNodeType.Text && nodeType - XmlNodeType.Whitespace > 1)
				{
					return false;
				}
				if (this.xmlCharType.IsOnlyWhitespace(node.Value))
				{
					return false;
				}
				outputMethod = XsltOutput.OutputMethod.Xml;
			}
			else if (node.NamespaceURI.Length == 0 && string.Compare("html", node.LocalName, StringComparison.OrdinalIgnoreCase) == 0)
			{
				outputMethod = XsltOutput.OutputMethod.Html;
			}
			if (this.processor.SetDefaultOutput(outputMethod))
			{
				this.CacheOuptutProps(this.processor.Output);
			}
			return true;
		}

		// Token: 0x0600364B RID: 13899 RVA: 0x0013044A File Offset: 0x0012E64A
		private void CacheRecord(RecordBuilder record)
		{
			if (this.outputCache == null)
			{
				this.outputCache = new ArrayList();
			}
			this.outputCache.Add(record.MainNode.Clone());
		}

		// Token: 0x0600364C RID: 13900 RVA: 0x00130478 File Offset: 0x0012E678
		private void OutputCachedRecords()
		{
			if (this.outputCache == null)
			{
				return;
			}
			for (int i = 0; i < this.outputCache.Count; i++)
			{
				BuilderInfo builderInfo = (BuilderInfo)this.outputCache[i];
				this.OutputRecord(builderInfo);
			}
			this.outputCache = null;
		}

		// Token: 0x0600364D RID: 13901 RVA: 0x001304C4 File Offset: 0x0012E6C4
		private void OutputRecord(RecordBuilder record)
		{
			BuilderInfo mainNode = record.MainNode;
			if (this.outputXmlDecl)
			{
				this.WriteXmlDeclaration();
			}
			switch (mainNode.NodeType)
			{
			case XmlNodeType.Element:
				this.WriteStartElement(record);
				return;
			case XmlNodeType.Attribute:
			case XmlNodeType.CDATA:
			case XmlNodeType.Entity:
			case XmlNodeType.Document:
			case XmlNodeType.DocumentFragment:
			case XmlNodeType.Notation:
				break;
			case XmlNodeType.Text:
			case XmlNodeType.Whitespace:
			case XmlNodeType.SignificantWhitespace:
				this.WriteTextNode(record);
				return;
			case XmlNodeType.EntityReference:
				this.Write('&');
				this.WriteName(mainNode.Prefix, mainNode.LocalName);
				this.Write(';');
				return;
			case XmlNodeType.ProcessingInstruction:
				this.WriteProcessingInstruction(record);
				return;
			case XmlNodeType.Comment:
				this.Indent(record);
				this.Write("<!--");
				this.Write(mainNode.Value);
				this.Write("-->");
				return;
			case XmlNodeType.DocumentType:
				this.Write(mainNode.Value);
				return;
			case XmlNodeType.EndElement:
				this.WriteEndElement(record);
				break;
			default:
				return;
			}
		}

		// Token: 0x0600364E RID: 13902 RVA: 0x001305AC File Offset: 0x0012E7AC
		private void OutputRecord(BuilderInfo node)
		{
			if (this.outputXmlDecl)
			{
				this.WriteXmlDeclaration();
			}
			this.Indent(0);
			switch (node.NodeType)
			{
			case XmlNodeType.Element:
			case XmlNodeType.Attribute:
			case XmlNodeType.CDATA:
			case XmlNodeType.Entity:
			case XmlNodeType.Document:
			case XmlNodeType.DocumentFragment:
			case XmlNodeType.Notation:
			case XmlNodeType.EndElement:
				break;
			case XmlNodeType.Text:
			case XmlNodeType.Whitespace:
			case XmlNodeType.SignificantWhitespace:
				this.WriteTextNode(node);
				return;
			case XmlNodeType.EntityReference:
				this.Write('&');
				this.WriteName(node.Prefix, node.LocalName);
				this.Write(';');
				return;
			case XmlNodeType.ProcessingInstruction:
				this.WriteProcessingInstruction(node);
				return;
			case XmlNodeType.Comment:
				this.Write("<!--");
				this.Write(node.Value);
				this.Write("-->");
				return;
			case XmlNodeType.DocumentType:
				this.Write(node.Value);
				break;
			default:
				return;
			}
		}

		// Token: 0x0600364F RID: 13903 RVA: 0x0013067C File Offset: 0x0012E87C
		private void WriteName(string prefix, string name)
		{
			if (prefix != null && prefix.Length > 0)
			{
				this.Write(prefix);
				if (name == null || name.Length <= 0)
				{
					return;
				}
				this.Write(':');
			}
			this.Write(name);
		}

		// Token: 0x06003650 RID: 13904 RVA: 0x001306AF File Offset: 0x0012E8AF
		private void WriteXmlAttributeValue(string value)
		{
			this.WriteWithReplace(value, SequentialOutput.s_XmlAttributeValueFind, SequentialOutput.s_XmlAttributeValueReplace);
		}

		// Token: 0x06003651 RID: 13905 RVA: 0x001306C4 File Offset: 0x0012E8C4
		private void WriteHtmlAttributeValue(string value)
		{
			int length = value.Length;
			int i = 0;
			while (i < length)
			{
				char c = value[i];
				i++;
				if (c != '"')
				{
					if (c == '&')
					{
						if (i != length && value[i] == '{')
						{
							this.Write(c);
						}
						else
						{
							this.Write("&amp;");
						}
					}
					else
					{
						this.Write(c);
					}
				}
				else
				{
					this.Write("&quot;");
				}
			}
		}

		// Token: 0x06003652 RID: 13906 RVA: 0x00130730 File Offset: 0x0012E930
		private void WriteHtmlUri(string value)
		{
			int length = value.Length;
			int i = 0;
			while (i < length)
			{
				char c = value[i];
				i++;
				if (c <= '\r')
				{
					if (c == '\n')
					{
						this.Write("&#xA;");
						continue;
					}
					if (c == '\r')
					{
						this.Write("&#xD;");
						continue;
					}
				}
				else
				{
					if (c == '"')
					{
						this.Write("&quot;");
						continue;
					}
					if (c == '&')
					{
						if (i != length && value[i] == '{')
						{
							this.Write(c);
							continue;
						}
						this.Write("&amp;");
						continue;
					}
				}
				if ('\u007f' < c)
				{
					if (this.utf8Encoding == null)
					{
						this.utf8Encoding = Encoding.UTF8;
						this.byteBuffer = new byte[this.utf8Encoding.GetMaxByteCount(1)];
					}
					int bytes = this.utf8Encoding.GetBytes(value, i - 1, 1, this.byteBuffer, 0);
					for (int j = 0; j < bytes; j++)
					{
						this.Write("%");
						uint num = (uint)this.byteBuffer[j];
						this.Write(num.ToString("X2", CultureInfo.InvariantCulture));
					}
				}
				else
				{
					this.Write(c);
				}
			}
		}

		// Token: 0x06003653 RID: 13907 RVA: 0x00130864 File Offset: 0x0012EA64
		private void WriteWithReplace(string value, char[] find, string[] replace)
		{
			int length = value.Length;
			int i;
			for (i = 0; i < length; i++)
			{
				int num = value.IndexOfAny(find, i);
				if (num == -1)
				{
					break;
				}
				while (i < num)
				{
					this.Write(value[i]);
					i++;
				}
				char c = value[i];
				int num2 = find.Length - 1;
				while (0 <= num2)
				{
					if (find[num2] == c)
					{
						this.Write(replace[num2]);
						break;
					}
					num2--;
				}
			}
			if (i == 0)
			{
				this.Write(value);
				return;
			}
			while (i < length)
			{
				this.Write(value[i]);
				i++;
			}
		}

		// Token: 0x06003654 RID: 13908 RVA: 0x001308F7 File Offset: 0x0012EAF7
		private void WriteCData(string value)
		{
			this.Write(value.Replace("]]>", "]]]]><![CDATA[>"));
		}

		// Token: 0x06003655 RID: 13909 RVA: 0x00130910 File Offset: 0x0012EB10
		private void WriteAttributes(ArrayList list, int count, HtmlElementProps htmlElementsProps)
		{
			for (int i = 0; i < count; i++)
			{
				BuilderInfo builderInfo = (BuilderInfo)list[i];
				string value = builderInfo.Value;
				bool flag = false;
				bool flag2 = false;
				if (htmlElementsProps != null && builderInfo.Prefix.Length == 0)
				{
					HtmlAttributeProps htmlAttributeProps = builderInfo.htmlAttrProps;
					if (htmlAttributeProps == null && builderInfo.search)
					{
						htmlAttributeProps = HtmlAttributeProps.GetProps(builderInfo.LocalName);
					}
					if (htmlAttributeProps != null)
					{
						flag = htmlElementsProps.AbrParent && htmlAttributeProps.Abr;
						flag2 = htmlElementsProps.UriParent && (htmlAttributeProps.Uri || (htmlElementsProps.NameParent && htmlAttributeProps.Name));
					}
				}
				this.Write(' ');
				this.WriteName(builderInfo.Prefix, builderInfo.LocalName);
				if (!flag || string.Compare(builderInfo.LocalName, value, StringComparison.OrdinalIgnoreCase) != 0)
				{
					this.Write("=\"");
					if (flag2)
					{
						this.WriteHtmlUri(value);
					}
					else if (this.isHtmlOutput)
					{
						this.WriteHtmlAttributeValue(value);
					}
					else
					{
						this.WriteXmlAttributeValue(value);
					}
					this.Write('"');
				}
			}
		}

		// Token: 0x06003656 RID: 13910 RVA: 0x00130A1F File Offset: 0x0012EC1F
		private void Indent(RecordBuilder record)
		{
			if (!record.Manager.CurrentElementScope.Mixed)
			{
				this.Indent(record.MainNode.Depth);
			}
		}

		// Token: 0x06003657 RID: 13911 RVA: 0x00130A44 File Offset: 0x0012EC44
		private void Indent(int depth)
		{
			if (this.firstLine)
			{
				if (this.indentOutput)
				{
					this.firstLine = false;
				}
				return;
			}
			this.Write("\r\n");
			int num = 2 * depth;
			while (0 < num)
			{
				this.Write(" ");
				num--;
			}
		}

		// Token: 0x06003658 RID: 13912
		internal abstract void Write(char outputChar);

		// Token: 0x06003659 RID: 13913
		internal abstract void Write(string outputText);

		// Token: 0x0600365A RID: 13914
		internal abstract void Close();

		// Token: 0x04002288 RID: 8840
		private const char s_Colon = ':';

		// Token: 0x04002289 RID: 8841
		private const char s_GreaterThan = '>';

		// Token: 0x0400228A RID: 8842
		private const char s_LessThan = '<';

		// Token: 0x0400228B RID: 8843
		private const char s_Space = ' ';

		// Token: 0x0400228C RID: 8844
		private const char s_Quote = '"';

		// Token: 0x0400228D RID: 8845
		private const char s_Semicolon = ';';

		// Token: 0x0400228E RID: 8846
		private const char s_NewLine = '\n';

		// Token: 0x0400228F RID: 8847
		private const char s_Return = '\r';

		// Token: 0x04002290 RID: 8848
		private const char s_Ampersand = '&';

		// Token: 0x04002291 RID: 8849
		private const string s_LessThanQuestion = "<?";

		// Token: 0x04002292 RID: 8850
		private const string s_QuestionGreaterThan = "?>";

		// Token: 0x04002293 RID: 8851
		private const string s_LessThanSlash = "</";

		// Token: 0x04002294 RID: 8852
		private const string s_SlashGreaterThan = " />";

		// Token: 0x04002295 RID: 8853
		private const string s_EqualQuote = "=\"";

		// Token: 0x04002296 RID: 8854
		private const string s_DocType = "<!DOCTYPE ";

		// Token: 0x04002297 RID: 8855
		private const string s_CommentBegin = "<!--";

		// Token: 0x04002298 RID: 8856
		private const string s_CommentEnd = "-->";

		// Token: 0x04002299 RID: 8857
		private const string s_CDataBegin = "<![CDATA[";

		// Token: 0x0400229A RID: 8858
		private const string s_CDataEnd = "]]>";

		// Token: 0x0400229B RID: 8859
		private const string s_VersionAll = " version=\"1.0\"";

		// Token: 0x0400229C RID: 8860
		private const string s_Standalone = " standalone=\"";

		// Token: 0x0400229D RID: 8861
		private const string s_EncodingStart = " encoding=\"";

		// Token: 0x0400229E RID: 8862
		private const string s_Public = "PUBLIC ";

		// Token: 0x0400229F RID: 8863
		private const string s_System = "SYSTEM ";

		// Token: 0x040022A0 RID: 8864
		private const string s_Html = "html";

		// Token: 0x040022A1 RID: 8865
		private const string s_QuoteSpace = "\" ";

		// Token: 0x040022A2 RID: 8866
		private const string s_CDataSplit = "]]]]><![CDATA[>";

		// Token: 0x040022A3 RID: 8867
		private const string s_EnLessThan = "&lt;";

		// Token: 0x040022A4 RID: 8868
		private const string s_EnGreaterThan = "&gt;";

		// Token: 0x040022A5 RID: 8869
		private const string s_EnAmpersand = "&amp;";

		// Token: 0x040022A6 RID: 8870
		private const string s_EnQuote = "&quot;";

		// Token: 0x040022A7 RID: 8871
		private const string s_EnNewLine = "&#xA;";

		// Token: 0x040022A8 RID: 8872
		private const string s_EnReturn = "&#xD;";

		// Token: 0x040022A9 RID: 8873
		private const string s_EndOfLine = "\r\n";

		// Token: 0x040022AA RID: 8874
		private static char[] s_TextValueFind = new char[] { '&', '>', '<' };

		// Token: 0x040022AB RID: 8875
		private static string[] s_TextValueReplace = new string[] { "&amp;", "&gt;", "&lt;" };

		// Token: 0x040022AC RID: 8876
		private static char[] s_XmlAttributeValueFind = new char[] { '&', '>', '<', '"', '\n', '\r' };

		// Token: 0x040022AD RID: 8877
		private static string[] s_XmlAttributeValueReplace = new string[] { "&amp;", "&gt;", "&lt;", "&quot;", "&#xA;", "&#xD;" };

		// Token: 0x040022AE RID: 8878
		private Processor processor;

		// Token: 0x040022AF RID: 8879
		protected Encoding encoding;

		// Token: 0x040022B0 RID: 8880
		private ArrayList outputCache;

		// Token: 0x040022B1 RID: 8881
		private bool firstLine = true;

		// Token: 0x040022B2 RID: 8882
		private bool secondRoot;

		// Token: 0x040022B3 RID: 8883
		private XsltOutput output;

		// Token: 0x040022B4 RID: 8884
		private bool isHtmlOutput;

		// Token: 0x040022B5 RID: 8885
		private bool isXmlOutput;

		// Token: 0x040022B6 RID: 8886
		private Hashtable cdataElements;

		// Token: 0x040022B7 RID: 8887
		private bool indentOutput;

		// Token: 0x040022B8 RID: 8888
		private bool outputDoctype;

		// Token: 0x040022B9 RID: 8889
		private bool outputXmlDecl;

		// Token: 0x040022BA RID: 8890
		private bool omitXmlDeclCalled;

		// Token: 0x040022BB RID: 8891
		private byte[] byteBuffer;

		// Token: 0x040022BC RID: 8892
		private Encoding utf8Encoding;

		// Token: 0x040022BD RID: 8893
		private XmlCharType xmlCharType = XmlCharType.Instance;
	}
}
