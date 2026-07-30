using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;

namespace System.Xml
{
	// Token: 0x020001AD RID: 429
	internal class XmlWellFormedWriter : XmlWriter
	{
		// Token: 0x06000F6C RID: 3948 RVA: 0x0005A46C File Offset: 0x0005866C
		internal XmlWellFormedWriter(XmlWriter writer, XmlWriterSettings settings)
		{
			this.writer = writer;
			this.rawWriter = writer as XmlRawWriter;
			this.predefinedNamespaces = writer as IXmlNamespaceResolver;
			if (this.rawWriter != null)
			{
				this.rawWriter.NamespaceResolver = new XmlWellFormedWriter.NamespaceResolverProxy(this);
			}
			this.checkCharacters = settings.CheckCharacters;
			this.omitDuplNamespaces = (settings.NamespaceHandling & NamespaceHandling.OmitDuplicates) > NamespaceHandling.Default;
			this.writeEndDocumentOnClose = settings.WriteEndDocumentOnClose;
			this.conformanceLevel = settings.ConformanceLevel;
			this.stateTable = ((this.conformanceLevel == ConformanceLevel.Document) ? XmlWellFormedWriter.StateTableDocument : XmlWellFormedWriter.StateTableAuto);
			this.currentState = XmlWellFormedWriter.State.Start;
			this.nsStack = new XmlWellFormedWriter.Namespace[8];
			this.nsStack[0].Set("xmlns", "http://www.w3.org/2000/xmlns/", XmlWellFormedWriter.NamespaceKind.Special);
			this.nsStack[1].Set("xml", "http://www.w3.org/XML/1998/namespace", XmlWellFormedWriter.NamespaceKind.Special);
			if (this.predefinedNamespaces == null)
			{
				this.nsStack[2].Set(string.Empty, string.Empty, XmlWellFormedWriter.NamespaceKind.Implied);
			}
			else
			{
				string text = this.predefinedNamespaces.LookupNamespace(string.Empty);
				this.nsStack[2].Set(string.Empty, (text == null) ? string.Empty : text, XmlWellFormedWriter.NamespaceKind.Implied);
			}
			this.nsTop = 2;
			this.elemScopeStack = new XmlWellFormedWriter.ElementScope[8];
			this.elemScopeStack[0].Set(string.Empty, string.Empty, string.Empty, this.nsTop);
			this.elemScopeStack[0].xmlSpace = XmlSpace.None;
			this.elemScopeStack[0].xmlLang = null;
			this.elemTop = 0;
			this.attrStack = new XmlWellFormedWriter.AttrName[8];
			this.hasher = new SecureStringHasher();
		}

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x06000F6D RID: 3949 RVA: 0x0005A631 File Offset: 0x00058831
		public override WriteState WriteState
		{
			get
			{
				if (this.currentState <= XmlWellFormedWriter.State.Error)
				{
					return XmlWellFormedWriter.state2WriteState[(int)this.currentState];
				}
				return WriteState.Error;
			}
		}

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x06000F6E RID: 3950 RVA: 0x0005A64C File Offset: 0x0005884C
		public override XmlWriterSettings Settings
		{
			get
			{
				XmlWriterSettings settings = this.writer.Settings;
				settings.ReadOnly = false;
				settings.ConformanceLevel = this.conformanceLevel;
				if (this.omitDuplNamespaces)
				{
					settings.NamespaceHandling |= NamespaceHandling.OmitDuplicates;
				}
				settings.WriteEndDocumentOnClose = this.writeEndDocumentOnClose;
				settings.ReadOnly = true;
				return settings;
			}
		}

		// Token: 0x06000F6F RID: 3951 RVA: 0x0005A6A2 File Offset: 0x000588A2
		public override void WriteStartDocument()
		{
			this.WriteStartDocumentImpl(XmlStandalone.Omit);
		}

		// Token: 0x06000F70 RID: 3952 RVA: 0x0005A6AB File Offset: 0x000588AB
		public override void WriteStartDocument(bool standalone)
		{
			this.WriteStartDocumentImpl(standalone ? XmlStandalone.Yes : XmlStandalone.No);
		}

		// Token: 0x06000F71 RID: 3953 RVA: 0x0005A6BC File Offset: 0x000588BC
		public override void WriteEndDocument()
		{
			try
			{
				while (this.elemTop > 0)
				{
					this.WriteEndElement();
				}
				int num = (int)this.currentState;
				this.AdvanceState(XmlWellFormedWriter.Token.EndDocument);
				if (num != 7)
				{
					throw new ArgumentException(Res.GetString("Document does not have a root element."));
				}
				if (this.rawWriter == null)
				{
					this.writer.WriteEndDocument();
				}
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000F72 RID: 3954 RVA: 0x0005A72C File Offset: 0x0005892C
		public override void WriteDocType(string name, string pubid, string sysid, string subset)
		{
			try
			{
				if (name == null || name.Length == 0)
				{
					throw new ArgumentException(Res.GetString("The empty string '' is not a valid name."));
				}
				XmlConvert.VerifyQName(name, ExceptionType.XmlException);
				if (this.conformanceLevel == ConformanceLevel.Fragment)
				{
					throw new InvalidOperationException(Res.GetString("DTD is not allowed in XML fragments."));
				}
				this.AdvanceState(XmlWellFormedWriter.Token.Dtd);
				if (this.dtdWritten)
				{
					this.currentState = XmlWellFormedWriter.State.Error;
					throw new InvalidOperationException(Res.GetString("The DTD has already been written out."));
				}
				if (this.conformanceLevel == ConformanceLevel.Auto)
				{
					this.conformanceLevel = ConformanceLevel.Document;
					this.stateTable = XmlWellFormedWriter.StateTableDocument;
				}
				if (this.checkCharacters)
				{
					int num;
					if (pubid != null && (num = this.xmlCharType.IsPublicId(pubid)) >= 0)
					{
						throw new ArgumentException(Res.GetString("'{0}', hexadecimal value {1}, is an invalid character.", XmlException.BuildCharExceptionArgs(pubid, num)), "pubid");
					}
					if (sysid != null && (num = this.xmlCharType.IsOnlyCharData(sysid)) >= 0)
					{
						throw new ArgumentException(Res.GetString("'{0}', hexadecimal value {1}, is an invalid character.", XmlException.BuildCharExceptionArgs(sysid, num)), "sysid");
					}
					if (subset != null && (num = this.xmlCharType.IsOnlyCharData(subset)) >= 0)
					{
						throw new ArgumentException(Res.GetString("'{0}', hexadecimal value {1}, is an invalid character.", XmlException.BuildCharExceptionArgs(subset, num)), "subset");
					}
				}
				this.writer.WriteDocType(name, pubid, sysid, subset);
				this.dtdWritten = true;
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000F73 RID: 3955 RVA: 0x0005A894 File Offset: 0x00058A94
		public override void WriteStartElement(string prefix, string localName, string ns)
		{
			try
			{
				if (localName == null || localName.Length == 0)
				{
					throw new ArgumentException(Res.GetString("The empty string '' is not a valid local name."));
				}
				this.CheckNCName(localName);
				this.AdvanceState(XmlWellFormedWriter.Token.StartElement);
				if (prefix == null)
				{
					if (ns != null)
					{
						prefix = this.LookupPrefix(ns);
					}
					if (prefix == null)
					{
						prefix = string.Empty;
					}
				}
				else if (prefix.Length > 0)
				{
					this.CheckNCName(prefix);
					if (ns == null)
					{
						ns = this.LookupNamespace(prefix);
					}
					if (ns == null || (ns != null && ns.Length == 0))
					{
						throw new ArgumentException(Res.GetString("Cannot use a prefix with an empty namespace."));
					}
				}
				if (ns == null)
				{
					ns = this.LookupNamespace(prefix);
					if (ns == null)
					{
						ns = string.Empty;
					}
				}
				if (this.elemTop == 0 && this.rawWriter != null)
				{
					this.rawWriter.OnRootElement(this.conformanceLevel);
				}
				this.writer.WriteStartElement(prefix, localName, ns);
				int num = this.elemTop + 1;
				this.elemTop = num;
				int num2 = num;
				if (num2 == this.elemScopeStack.Length)
				{
					XmlWellFormedWriter.ElementScope[] array = new XmlWellFormedWriter.ElementScope[num2 * 2];
					Array.Copy(this.elemScopeStack, array, num2);
					this.elemScopeStack = array;
				}
				this.elemScopeStack[num2].Set(prefix, localName, ns, this.nsTop);
				this.PushNamespaceImplicit(prefix, ns);
				if (this.attrCount >= 14)
				{
					this.attrHashTable.Clear();
				}
				this.attrCount = 0;
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000F74 RID: 3956 RVA: 0x0005AA08 File Offset: 0x00058C08
		public override void WriteEndElement()
		{
			try
			{
				this.AdvanceState(XmlWellFormedWriter.Token.EndElement);
				int num = this.elemTop;
				if (num == 0)
				{
					throw new XmlException("There was no XML start tag open.", string.Empty);
				}
				if (this.rawWriter != null)
				{
					this.elemScopeStack[num].WriteEndElement(this.rawWriter);
				}
				else
				{
					this.writer.WriteEndElement();
				}
				int prevNSTop = this.elemScopeStack[num].prevNSTop;
				if (this.useNsHashtable && prevNSTop < this.nsTop)
				{
					this.PopNamespaces(prevNSTop + 1, this.nsTop);
				}
				this.nsTop = prevNSTop;
				if ((this.elemTop = num - 1) == 0)
				{
					if (this.conformanceLevel == ConformanceLevel.Document)
					{
						this.currentState = XmlWellFormedWriter.State.AfterRootEle;
					}
					else
					{
						this.currentState = XmlWellFormedWriter.State.TopLevel;
					}
				}
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000F75 RID: 3957 RVA: 0x0005AAE0 File Offset: 0x00058CE0
		public override void WriteFullEndElement()
		{
			try
			{
				this.AdvanceState(XmlWellFormedWriter.Token.EndElement);
				int num = this.elemTop;
				if (num == 0)
				{
					throw new XmlException("There was no XML start tag open.", string.Empty);
				}
				if (this.rawWriter != null)
				{
					this.elemScopeStack[num].WriteFullEndElement(this.rawWriter);
				}
				else
				{
					this.writer.WriteFullEndElement();
				}
				int prevNSTop = this.elemScopeStack[num].prevNSTop;
				if (this.useNsHashtable && prevNSTop < this.nsTop)
				{
					this.PopNamespaces(prevNSTop + 1, this.nsTop);
				}
				this.nsTop = prevNSTop;
				if ((this.elemTop = num - 1) == 0)
				{
					if (this.conformanceLevel == ConformanceLevel.Document)
					{
						this.currentState = XmlWellFormedWriter.State.AfterRootEle;
					}
					else
					{
						this.currentState = XmlWellFormedWriter.State.TopLevel;
					}
				}
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000F76 RID: 3958 RVA: 0x0005ABB8 File Offset: 0x00058DB8
		public override void WriteStartAttribute(string prefix, string localName, string namespaceName)
		{
			try
			{
				if (localName == null || localName.Length == 0)
				{
					if (!(prefix == "xmlns"))
					{
						throw new ArgumentException(Res.GetString("The empty string '' is not a valid local name."));
					}
					localName = "xmlns";
					prefix = string.Empty;
				}
				this.CheckNCName(localName);
				this.AdvanceState(XmlWellFormedWriter.Token.StartAttribute);
				if (prefix == null)
				{
					if (namespaceName != null && (!(localName == "xmlns") || !(namespaceName == "http://www.w3.org/2000/xmlns/")))
					{
						prefix = this.LookupPrefix(namespaceName);
					}
					if (prefix == null)
					{
						prefix = string.Empty;
					}
				}
				if (namespaceName == null)
				{
					if (prefix != null && prefix.Length > 0)
					{
						namespaceName = this.LookupNamespace(prefix);
					}
					if (namespaceName == null)
					{
						namespaceName = string.Empty;
					}
				}
				if (prefix.Length == 0)
				{
					if (localName[0] == 'x' && localName == "xmlns")
					{
						if (namespaceName.Length > 0 && namespaceName != "http://www.w3.org/2000/xmlns/")
						{
							throw new ArgumentException(Res.GetString("Prefix \"xmlns\" is reserved for use by XML."));
						}
						this.curDeclPrefix = string.Empty;
						this.SetSpecialAttribute(XmlWellFormedWriter.SpecialAttribute.DefaultXmlns);
						goto IL_0224;
					}
					else if (namespaceName.Length > 0)
					{
						prefix = this.LookupPrefix(namespaceName);
						if (prefix == null || prefix.Length == 0)
						{
							prefix = this.GeneratePrefix();
						}
					}
				}
				else
				{
					if (prefix[0] == 'x')
					{
						if (prefix == "xmlns")
						{
							if (namespaceName.Length > 0 && namespaceName != "http://www.w3.org/2000/xmlns/")
							{
								throw new ArgumentException(Res.GetString("Prefix \"xmlns\" is reserved for use by XML."));
							}
							this.curDeclPrefix = localName;
							this.SetSpecialAttribute(XmlWellFormedWriter.SpecialAttribute.PrefixedXmlns);
							goto IL_0224;
						}
						else if (prefix == "xml")
						{
							if (namespaceName.Length > 0 && namespaceName != "http://www.w3.org/XML/1998/namespace")
							{
								throw new ArgumentException(Res.GetString("Prefix \"xml\" is reserved for use by XML and can be mapped only to namespace name \"http://www.w3.org/XML/1998/namespace\"."));
							}
							if (localName == "space")
							{
								this.SetSpecialAttribute(XmlWellFormedWriter.SpecialAttribute.XmlSpace);
								goto IL_0224;
							}
							if (localName == "lang")
							{
								this.SetSpecialAttribute(XmlWellFormedWriter.SpecialAttribute.XmlLang);
								goto IL_0224;
							}
						}
					}
					this.CheckNCName(prefix);
					if (namespaceName.Length == 0)
					{
						prefix = string.Empty;
					}
					else
					{
						string text = this.LookupLocalNamespace(prefix);
						if (text != null && text != namespaceName)
						{
							prefix = this.GeneratePrefix();
						}
					}
				}
				if (prefix.Length != 0)
				{
					this.PushNamespaceImplicit(prefix, namespaceName);
				}
				IL_0224:
				this.AddAttribute(prefix, localName, namespaceName);
				if (this.specAttr == XmlWellFormedWriter.SpecialAttribute.No)
				{
					this.writer.WriteStartAttribute(prefix, localName, namespaceName);
				}
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000F77 RID: 3959 RVA: 0x0005AE34 File Offset: 0x00059034
		public override void WriteEndAttribute()
		{
			try
			{
				this.AdvanceState(XmlWellFormedWriter.Token.EndAttribute);
				if (this.specAttr != XmlWellFormedWriter.SpecialAttribute.No)
				{
					switch (this.specAttr)
					{
					case XmlWellFormedWriter.SpecialAttribute.DefaultXmlns:
					{
						string text = this.attrValueCache.StringValue;
						if (this.PushNamespaceExplicit(string.Empty, text))
						{
							if (this.rawWriter != null)
							{
								if (this.rawWriter.SupportsNamespaceDeclarationInChunks)
								{
									this.rawWriter.WriteStartNamespaceDeclaration(string.Empty);
									this.attrValueCache.Replay(this.rawWriter);
									this.rawWriter.WriteEndNamespaceDeclaration();
								}
								else
								{
									this.rawWriter.WriteNamespaceDeclaration(string.Empty, text);
								}
							}
							else
							{
								this.writer.WriteStartAttribute(string.Empty, "xmlns", "http://www.w3.org/2000/xmlns/");
								this.attrValueCache.Replay(this.writer);
								this.writer.WriteEndAttribute();
							}
						}
						this.curDeclPrefix = null;
						break;
					}
					case XmlWellFormedWriter.SpecialAttribute.PrefixedXmlns:
					{
						string text = this.attrValueCache.StringValue;
						if (text.Length == 0)
						{
							throw new ArgumentException(Res.GetString("Cannot use a prefix with an empty namespace."));
						}
						if (text == "http://www.w3.org/2000/xmlns/" || (text == "http://www.w3.org/XML/1998/namespace" && this.curDeclPrefix != "xml"))
						{
							throw new ArgumentException(Res.GetString("Cannot bind to the reserved namespace."));
						}
						if (this.PushNamespaceExplicit(this.curDeclPrefix, text))
						{
							if (this.rawWriter != null)
							{
								if (this.rawWriter.SupportsNamespaceDeclarationInChunks)
								{
									this.rawWriter.WriteStartNamespaceDeclaration(this.curDeclPrefix);
									this.attrValueCache.Replay(this.rawWriter);
									this.rawWriter.WriteEndNamespaceDeclaration();
								}
								else
								{
									this.rawWriter.WriteNamespaceDeclaration(this.curDeclPrefix, text);
								}
							}
							else
							{
								this.writer.WriteStartAttribute("xmlns", this.curDeclPrefix, "http://www.w3.org/2000/xmlns/");
								this.attrValueCache.Replay(this.writer);
								this.writer.WriteEndAttribute();
							}
						}
						this.curDeclPrefix = null;
						break;
					}
					case XmlWellFormedWriter.SpecialAttribute.XmlSpace:
					{
						this.attrValueCache.Trim();
						string text = this.attrValueCache.StringValue;
						if (text == "default")
						{
							this.elemScopeStack[this.elemTop].xmlSpace = XmlSpace.Default;
						}
						else
						{
							if (!(text == "preserve"))
							{
								throw new ArgumentException(Res.GetString("'{0}' is an invalid xml:space value.", new object[] { text }));
							}
							this.elemScopeStack[this.elemTop].xmlSpace = XmlSpace.Preserve;
						}
						this.writer.WriteStartAttribute("xml", "space", "http://www.w3.org/XML/1998/namespace");
						this.attrValueCache.Replay(this.writer);
						this.writer.WriteEndAttribute();
						break;
					}
					case XmlWellFormedWriter.SpecialAttribute.XmlLang:
					{
						string text = this.attrValueCache.StringValue;
						this.elemScopeStack[this.elemTop].xmlLang = text;
						this.writer.WriteStartAttribute("xml", "lang", "http://www.w3.org/XML/1998/namespace");
						this.attrValueCache.Replay(this.writer);
						this.writer.WriteEndAttribute();
						break;
					}
					}
					this.specAttr = XmlWellFormedWriter.SpecialAttribute.No;
					this.attrValueCache.Clear();
				}
				else
				{
					this.writer.WriteEndAttribute();
				}
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000F78 RID: 3960 RVA: 0x0005B190 File Offset: 0x00059390
		public override void WriteCData(string text)
		{
			try
			{
				if (text == null)
				{
					text = string.Empty;
				}
				this.AdvanceState(XmlWellFormedWriter.Token.CData);
				this.writer.WriteCData(text);
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000F79 RID: 3961 RVA: 0x0005B1D8 File Offset: 0x000593D8
		public override void WriteComment(string text)
		{
			try
			{
				if (text == null)
				{
					text = string.Empty;
				}
				this.AdvanceState(XmlWellFormedWriter.Token.Comment);
				this.writer.WriteComment(text);
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000F7A RID: 3962 RVA: 0x0005B220 File Offset: 0x00059420
		public override void WriteProcessingInstruction(string name, string text)
		{
			try
			{
				if (name == null || name.Length == 0)
				{
					throw new ArgumentException(Res.GetString("The empty string '' is not a valid name."));
				}
				this.CheckNCName(name);
				if (text == null)
				{
					text = string.Empty;
				}
				if (name.Length == 3 && string.Compare(name, "xml", StringComparison.OrdinalIgnoreCase) == 0)
				{
					if (this.currentState != XmlWellFormedWriter.State.Start)
					{
						throw new ArgumentException(Res.GetString((this.conformanceLevel == ConformanceLevel.Document) ? "Cannot write XML declaration. WriteStartDocument method has already written it." : "Cannot write XML declaration. XML declaration can be only at the beginning of the document."));
					}
					this.xmlDeclFollows = true;
					this.AdvanceState(XmlWellFormedWriter.Token.PI);
					if (this.rawWriter != null)
					{
						this.rawWriter.WriteXmlDeclaration(text);
					}
					else
					{
						this.writer.WriteProcessingInstruction(name, text);
					}
				}
				else
				{
					this.AdvanceState(XmlWellFormedWriter.Token.PI);
					this.writer.WriteProcessingInstruction(name, text);
				}
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000F7B RID: 3963 RVA: 0x0005B2FC File Offset: 0x000594FC
		public override void WriteEntityRef(string name)
		{
			try
			{
				if (name == null || name.Length == 0)
				{
					throw new ArgumentException(Res.GetString("The empty string '' is not a valid name."));
				}
				this.CheckNCName(name);
				this.AdvanceState(XmlWellFormedWriter.Token.Text);
				if (this.SaveAttrValue)
				{
					this.attrValueCache.WriteEntityRef(name);
				}
				else
				{
					this.writer.WriteEntityRef(name);
				}
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000F7C RID: 3964 RVA: 0x0005B374 File Offset: 0x00059574
		public override void WriteCharEntity(char ch)
		{
			try
			{
				if (char.IsSurrogate(ch))
				{
					throw new ArgumentException(Res.GetString("The surrogate pair is invalid. Missing a low surrogate character."));
				}
				this.AdvanceState(XmlWellFormedWriter.Token.Text);
				if (this.SaveAttrValue)
				{
					this.attrValueCache.WriteCharEntity(ch);
				}
				else
				{
					this.writer.WriteCharEntity(ch);
				}
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000F7D RID: 3965 RVA: 0x0005B3E0 File Offset: 0x000595E0
		public override void WriteSurrogateCharEntity(char lowChar, char highChar)
		{
			try
			{
				if (!char.IsSurrogatePair(highChar, lowChar))
				{
					throw XmlConvert.CreateInvalidSurrogatePairException(lowChar, highChar);
				}
				this.AdvanceState(XmlWellFormedWriter.Token.Text);
				if (this.SaveAttrValue)
				{
					this.attrValueCache.WriteSurrogateCharEntity(lowChar, highChar);
				}
				else
				{
					this.writer.WriteSurrogateCharEntity(lowChar, highChar);
				}
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000F7E RID: 3966 RVA: 0x0005B448 File Offset: 0x00059648
		public override void WriteWhitespace(string ws)
		{
			try
			{
				if (ws == null)
				{
					ws = string.Empty;
				}
				if (!XmlCharType.Instance.IsOnlyWhitespace(ws))
				{
					throw new ArgumentException(Res.GetString("Only white space characters should be used."));
				}
				this.AdvanceState(XmlWellFormedWriter.Token.Whitespace);
				if (this.SaveAttrValue)
				{
					this.attrValueCache.WriteWhitespace(ws);
				}
				else
				{
					this.writer.WriteWhitespace(ws);
				}
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000F7F RID: 3967 RVA: 0x0005B4C8 File Offset: 0x000596C8
		public override void WriteString(string text)
		{
			try
			{
				if (text != null)
				{
					this.AdvanceState(XmlWellFormedWriter.Token.Text);
					if (this.SaveAttrValue)
					{
						this.attrValueCache.WriteString(text);
					}
					else
					{
						this.writer.WriteString(text);
					}
				}
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000F80 RID: 3968 RVA: 0x0005B524 File Offset: 0x00059724
		public override void WriteChars(char[] buffer, int index, int count)
		{
			try
			{
				if (buffer == null)
				{
					throw new ArgumentNullException("buffer");
				}
				if (index < 0)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				if (count < 0)
				{
					throw new ArgumentOutOfRangeException("count");
				}
				if (count > buffer.Length - index)
				{
					throw new ArgumentOutOfRangeException("count");
				}
				this.AdvanceState(XmlWellFormedWriter.Token.Text);
				if (this.SaveAttrValue)
				{
					this.attrValueCache.WriteChars(buffer, index, count);
				}
				else
				{
					this.writer.WriteChars(buffer, index, count);
				}
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000F81 RID: 3969 RVA: 0x0005B5BC File Offset: 0x000597BC
		public override void WriteRaw(char[] buffer, int index, int count)
		{
			try
			{
				if (buffer == null)
				{
					throw new ArgumentNullException("buffer");
				}
				if (index < 0)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				if (count < 0)
				{
					throw new ArgumentOutOfRangeException("count");
				}
				if (count > buffer.Length - index)
				{
					throw new ArgumentOutOfRangeException("count");
				}
				this.AdvanceState(XmlWellFormedWriter.Token.RawData);
				if (this.SaveAttrValue)
				{
					this.attrValueCache.WriteRaw(buffer, index, count);
				}
				else
				{
					this.writer.WriteRaw(buffer, index, count);
				}
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000F82 RID: 3970 RVA: 0x0005B654 File Offset: 0x00059854
		public override void WriteRaw(string data)
		{
			try
			{
				if (data != null)
				{
					this.AdvanceState(XmlWellFormedWriter.Token.RawData);
					if (this.SaveAttrValue)
					{
						this.attrValueCache.WriteRaw(data);
					}
					else
					{
						this.writer.WriteRaw(data);
					}
				}
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000F83 RID: 3971 RVA: 0x0005B6B0 File Offset: 0x000598B0
		public override void WriteBase64(byte[] buffer, int index, int count)
		{
			try
			{
				if (buffer == null)
				{
					throw new ArgumentNullException("buffer");
				}
				if (index < 0)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				if (count < 0)
				{
					throw new ArgumentOutOfRangeException("count");
				}
				if (count > buffer.Length - index)
				{
					throw new ArgumentOutOfRangeException("count");
				}
				this.AdvanceState(XmlWellFormedWriter.Token.Base64);
				this.writer.WriteBase64(buffer, index, count);
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000F84 RID: 3972 RVA: 0x0005B730 File Offset: 0x00059930
		public override void Close()
		{
			if (this.currentState != XmlWellFormedWriter.State.Closed)
			{
				try
				{
					if (this.writeEndDocumentOnClose)
					{
						while (this.currentState != XmlWellFormedWriter.State.Error)
						{
							if (this.elemTop <= 0)
							{
								break;
							}
							this.WriteEndElement();
						}
					}
					else if (this.currentState != XmlWellFormedWriter.State.Error && this.elemTop > 0)
					{
						try
						{
							this.AdvanceState(XmlWellFormedWriter.Token.EndElement);
						}
						catch
						{
							this.currentState = XmlWellFormedWriter.State.Error;
							throw;
						}
					}
					if (this.InBase64 && this.rawWriter != null)
					{
						this.rawWriter.WriteEndBase64();
					}
					this.writer.Flush();
				}
				finally
				{
					try
					{
						if (this.rawWriter != null)
						{
							this.rawWriter.Close(this.WriteState);
						}
						else
						{
							this.writer.Close();
						}
					}
					finally
					{
						this.currentState = XmlWellFormedWriter.State.Closed;
					}
				}
			}
		}

		// Token: 0x06000F85 RID: 3973 RVA: 0x0005B81C File Offset: 0x00059A1C
		public override void Flush()
		{
			try
			{
				this.writer.Flush();
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000F86 RID: 3974 RVA: 0x0005B854 File Offset: 0x00059A54
		public override string LookupPrefix(string ns)
		{
			string text;
			try
			{
				if (ns == null)
				{
					throw new ArgumentNullException("ns");
				}
				for (int i = this.nsTop; i >= 0; i--)
				{
					if (this.nsStack[i].namespaceUri == ns)
					{
						string prefix = this.nsStack[i].prefix;
						for (i++; i <= this.nsTop; i++)
						{
							if (this.nsStack[i].prefix == prefix)
							{
								return null;
							}
						}
						return prefix;
					}
				}
				text = ((this.predefinedNamespaces != null) ? this.predefinedNamespaces.LookupPrefix(ns) : null);
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
			return text;
		}

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x06000F87 RID: 3975 RVA: 0x0005B918 File Offset: 0x00059B18
		public override XmlSpace XmlSpace
		{
			get
			{
				int num = this.elemTop;
				while (num >= 0 && this.elemScopeStack[num].xmlSpace == (XmlSpace)(-1))
				{
					num--;
				}
				return this.elemScopeStack[num].xmlSpace;
			}
		}

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x06000F88 RID: 3976 RVA: 0x0005B95C File Offset: 0x00059B5C
		public override string XmlLang
		{
			get
			{
				int num = this.elemTop;
				while (num > 0 && this.elemScopeStack[num].xmlLang == null)
				{
					num--;
				}
				return this.elemScopeStack[num].xmlLang;
			}
		}

		// Token: 0x06000F89 RID: 3977 RVA: 0x0005B9A0 File Offset: 0x00059BA0
		public override void WriteQualifiedName(string localName, string ns)
		{
			try
			{
				if (localName == null || localName.Length == 0)
				{
					throw new ArgumentException(Res.GetString("The empty string '' is not a valid local name."));
				}
				this.CheckNCName(localName);
				this.AdvanceState(XmlWellFormedWriter.Token.Text);
				string text = string.Empty;
				if (ns != null && ns.Length != 0)
				{
					text = this.LookupPrefix(ns);
					if (text == null)
					{
						if (this.currentState != XmlWellFormedWriter.State.Attribute)
						{
							throw new ArgumentException(Res.GetString("The '{0}' namespace is not defined.", new object[] { ns }));
						}
						text = this.GeneratePrefix();
						this.PushNamespaceImplicit(text, ns);
					}
				}
				if (this.SaveAttrValue || this.rawWriter == null)
				{
					if (text.Length != 0)
					{
						this.WriteString(text);
						this.WriteString(":");
					}
					this.WriteString(localName);
				}
				else
				{
					this.rawWriter.WriteQualifiedName(text, localName, ns);
				}
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000F8A RID: 3978 RVA: 0x0005BA84 File Offset: 0x00059C84
		public override void WriteValue(bool value)
		{
			try
			{
				this.AdvanceState(XmlWellFormedWriter.Token.AtomicValue);
				this.writer.WriteValue(value);
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000F8B RID: 3979 RVA: 0x0005BAC4 File Offset: 0x00059CC4
		public override void WriteValue(DateTime value)
		{
			try
			{
				this.AdvanceState(XmlWellFormedWriter.Token.AtomicValue);
				this.writer.WriteValue(value);
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000F8C RID: 3980 RVA: 0x0005BB04 File Offset: 0x00059D04
		public override void WriteValue(DateTimeOffset value)
		{
			try
			{
				this.AdvanceState(XmlWellFormedWriter.Token.AtomicValue);
				this.writer.WriteValue(value);
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000F8D RID: 3981 RVA: 0x0005BB44 File Offset: 0x00059D44
		public override void WriteValue(double value)
		{
			try
			{
				this.AdvanceState(XmlWellFormedWriter.Token.AtomicValue);
				this.writer.WriteValue(value);
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000F8E RID: 3982 RVA: 0x0005BB84 File Offset: 0x00059D84
		public override void WriteValue(float value)
		{
			try
			{
				this.AdvanceState(XmlWellFormedWriter.Token.AtomicValue);
				this.writer.WriteValue(value);
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000F8F RID: 3983 RVA: 0x0005BBC4 File Offset: 0x00059DC4
		public override void WriteValue(decimal value)
		{
			try
			{
				this.AdvanceState(XmlWellFormedWriter.Token.AtomicValue);
				this.writer.WriteValue(value);
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000F90 RID: 3984 RVA: 0x0005BC04 File Offset: 0x00059E04
		public override void WriteValue(int value)
		{
			try
			{
				this.AdvanceState(XmlWellFormedWriter.Token.AtomicValue);
				this.writer.WriteValue(value);
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000F91 RID: 3985 RVA: 0x0005BC44 File Offset: 0x00059E44
		public override void WriteValue(long value)
		{
			try
			{
				this.AdvanceState(XmlWellFormedWriter.Token.AtomicValue);
				this.writer.WriteValue(value);
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000F92 RID: 3986 RVA: 0x0005BC84 File Offset: 0x00059E84
		public override void WriteValue(string value)
		{
			try
			{
				if (value != null)
				{
					if (this.SaveAttrValue)
					{
						this.AdvanceState(XmlWellFormedWriter.Token.Text);
						this.attrValueCache.WriteValue(value);
					}
					else
					{
						this.AdvanceState(XmlWellFormedWriter.Token.AtomicValue);
						this.writer.WriteValue(value);
					}
				}
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000F93 RID: 3987 RVA: 0x0005BCE8 File Offset: 0x00059EE8
		public override void WriteValue(object value)
		{
			try
			{
				if (this.SaveAttrValue && value is string)
				{
					this.AdvanceState(XmlWellFormedWriter.Token.Text);
					this.attrValueCache.WriteValue((string)value);
				}
				else
				{
					this.AdvanceState(XmlWellFormedWriter.Token.AtomicValue);
					this.writer.WriteValue(value);
				}
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000F94 RID: 3988 RVA: 0x0005BD54 File Offset: 0x00059F54
		public override void WriteBinHex(byte[] buffer, int index, int count)
		{
			if (this.IsClosedOrErrorState)
			{
				throw new InvalidOperationException(Res.GetString("The Writer is closed or in error state."));
			}
			try
			{
				this.AdvanceState(XmlWellFormedWriter.Token.Text);
				base.WriteBinHex(buffer, index, count);
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x06000F95 RID: 3989 RVA: 0x0005BDA8 File Offset: 0x00059FA8
		internal XmlWriter InnerWriter
		{
			get
			{
				return this.writer;
			}
		}

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x06000F96 RID: 3990 RVA: 0x0005BDB0 File Offset: 0x00059FB0
		internal XmlRawWriter RawWriter
		{
			get
			{
				return this.rawWriter;
			}
		}

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x06000F97 RID: 3991 RVA: 0x0005BDB8 File Offset: 0x00059FB8
		private bool SaveAttrValue
		{
			get
			{
				return this.specAttr > XmlWellFormedWriter.SpecialAttribute.No;
			}
		}

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x06000F98 RID: 3992 RVA: 0x0005BDC3 File Offset: 0x00059FC3
		private bool InBase64
		{
			get
			{
				return this.currentState == XmlWellFormedWriter.State.B64Content || this.currentState == XmlWellFormedWriter.State.B64Attribute || this.currentState == XmlWellFormedWriter.State.RootLevelB64Attr;
			}
		}

		// Token: 0x06000F99 RID: 3993 RVA: 0x0005BDE4 File Offset: 0x00059FE4
		private void SetSpecialAttribute(XmlWellFormedWriter.SpecialAttribute special)
		{
			this.specAttr = special;
			if (XmlWellFormedWriter.State.Attribute == this.currentState)
			{
				this.currentState = XmlWellFormedWriter.State.SpecialAttr;
			}
			else if (XmlWellFormedWriter.State.RootLevelAttr == this.currentState)
			{
				this.currentState = XmlWellFormedWriter.State.RootLevelSpecAttr;
			}
			if (this.attrValueCache == null)
			{
				this.attrValueCache = new XmlWellFormedWriter.AttributeValueCache();
			}
		}

		// Token: 0x06000F9A RID: 3994 RVA: 0x0005BE30 File Offset: 0x0005A030
		private void WriteStartDocumentImpl(XmlStandalone standalone)
		{
			try
			{
				this.AdvanceState(XmlWellFormedWriter.Token.StartDocument);
				if (this.conformanceLevel == ConformanceLevel.Auto)
				{
					this.conformanceLevel = ConformanceLevel.Document;
					this.stateTable = XmlWellFormedWriter.StateTableDocument;
				}
				else if (this.conformanceLevel == ConformanceLevel.Fragment)
				{
					throw new InvalidOperationException(Res.GetString("WriteStartDocument cannot be called on writers created with ConformanceLevel.Fragment."));
				}
				if (this.rawWriter != null)
				{
					if (!this.xmlDeclFollows)
					{
						this.rawWriter.WriteXmlDeclaration(standalone);
					}
				}
				else
				{
					this.writer.WriteStartDocument();
				}
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000F9B RID: 3995 RVA: 0x0005BEC0 File Offset: 0x0005A0C0
		private void StartFragment()
		{
			this.conformanceLevel = ConformanceLevel.Fragment;
		}

		// Token: 0x06000F9C RID: 3996 RVA: 0x0005BECC File Offset: 0x0005A0CC
		private void PushNamespaceImplicit(string prefix, string ns)
		{
			int num = this.LookupNamespaceIndex(prefix);
			XmlWellFormedWriter.NamespaceKind namespaceKind;
			if (num != -1)
			{
				if (num > this.elemScopeStack[this.elemTop].prevNSTop)
				{
					if (this.nsStack[num].namespaceUri != ns)
					{
						throw new XmlException("The prefix '{0}' cannot be redefined from '{1}' to '{2}' within the same start element tag.", new string[]
						{
							prefix,
							this.nsStack[num].namespaceUri,
							ns
						});
					}
					return;
				}
				else if (this.nsStack[num].kind == XmlWellFormedWriter.NamespaceKind.Special)
				{
					if (!(prefix == "xml"))
					{
						throw new ArgumentException(Res.GetString("Prefix \"xmlns\" is reserved for use by XML."));
					}
					if (ns != this.nsStack[num].namespaceUri)
					{
						throw new ArgumentException(Res.GetString("Prefix \"xml\" is reserved for use by XML and can be mapped only to namespace name \"http://www.w3.org/XML/1998/namespace\"."));
					}
					namespaceKind = XmlWellFormedWriter.NamespaceKind.Implied;
				}
				else
				{
					namespaceKind = ((this.nsStack[num].namespaceUri == ns) ? XmlWellFormedWriter.NamespaceKind.Implied : XmlWellFormedWriter.NamespaceKind.NeedToWrite);
				}
			}
			else
			{
				if ((ns == "http://www.w3.org/XML/1998/namespace" && prefix != "xml") || (ns == "http://www.w3.org/2000/xmlns/" && prefix != "xmlns"))
				{
					throw new ArgumentException(Res.GetString("Prefix '{0}' cannot be mapped to namespace name reserved for \"xml\" or \"xmlns\".", new object[] { prefix }));
				}
				if (this.predefinedNamespaces != null)
				{
					namespaceKind = ((this.predefinedNamespaces.LookupNamespace(prefix) == ns) ? XmlWellFormedWriter.NamespaceKind.Implied : XmlWellFormedWriter.NamespaceKind.NeedToWrite);
				}
				else
				{
					namespaceKind = XmlWellFormedWriter.NamespaceKind.NeedToWrite;
				}
			}
			this.AddNamespace(prefix, ns, namespaceKind);
		}

		// Token: 0x06000F9D RID: 3997 RVA: 0x0005C048 File Offset: 0x0005A248
		private bool PushNamespaceExplicit(string prefix, string ns)
		{
			bool flag = true;
			int num = this.LookupNamespaceIndex(prefix);
			if (num != -1)
			{
				if (num > this.elemScopeStack[this.elemTop].prevNSTop)
				{
					if (this.nsStack[num].namespaceUri != ns)
					{
						throw new XmlException("The prefix '{0}' cannot be redefined from '{1}' to '{2}' within the same start element tag.", new string[]
						{
							prefix,
							this.nsStack[num].namespaceUri,
							ns
						});
					}
					XmlWellFormedWriter.NamespaceKind kind = this.nsStack[num].kind;
					if (kind == XmlWellFormedWriter.NamespaceKind.Written)
					{
						throw XmlWellFormedWriter.DupAttrException((prefix.Length == 0) ? string.Empty : "xmlns", (prefix.Length == 0) ? "xmlns" : prefix);
					}
					if (this.omitDuplNamespaces && kind != XmlWellFormedWriter.NamespaceKind.NeedToWrite)
					{
						flag = false;
					}
					this.nsStack[num].kind = XmlWellFormedWriter.NamespaceKind.Written;
					return flag;
				}
				else if (this.nsStack[num].namespaceUri == ns && this.omitDuplNamespaces)
				{
					flag = false;
				}
			}
			else if (this.predefinedNamespaces != null && this.predefinedNamespaces.LookupNamespace(prefix) == ns && this.omitDuplNamespaces)
			{
				flag = false;
			}
			if ((ns == "http://www.w3.org/XML/1998/namespace" && prefix != "xml") || (ns == "http://www.w3.org/2000/xmlns/" && prefix != "xmlns"))
			{
				throw new ArgumentException(Res.GetString("Prefix '{0}' cannot be mapped to namespace name reserved for \"xml\" or \"xmlns\".", new object[] { prefix }));
			}
			if (prefix.Length > 0 && prefix[0] == 'x')
			{
				if (prefix == "xml")
				{
					if (ns != "http://www.w3.org/XML/1998/namespace")
					{
						throw new ArgumentException(Res.GetString("Prefix \"xml\" is reserved for use by XML and can be mapped only to namespace name \"http://www.w3.org/XML/1998/namespace\"."));
					}
				}
				else if (prefix == "xmlns")
				{
					throw new ArgumentException(Res.GetString("Prefix \"xmlns\" is reserved for use by XML."));
				}
			}
			this.AddNamespace(prefix, ns, XmlWellFormedWriter.NamespaceKind.Written);
			return flag;
		}

		// Token: 0x06000F9E RID: 3998 RVA: 0x0005C228 File Offset: 0x0005A428
		private void AddNamespace(string prefix, string ns, XmlWellFormedWriter.NamespaceKind kind)
		{
			int num = this.nsTop + 1;
			this.nsTop = num;
			int num2 = num;
			if (num2 == this.nsStack.Length)
			{
				XmlWellFormedWriter.Namespace[] array = new XmlWellFormedWriter.Namespace[num2 * 2];
				Array.Copy(this.nsStack, array, num2);
				this.nsStack = array;
			}
			this.nsStack[num2].Set(prefix, ns, kind);
			if (this.useNsHashtable)
			{
				this.AddToNamespaceHashtable(this.nsTop);
				return;
			}
			if (this.nsTop == 16)
			{
				this.nsHashtable = new Dictionary<string, int>(this.hasher);
				for (int i = 0; i <= this.nsTop; i++)
				{
					this.AddToNamespaceHashtable(i);
				}
				this.useNsHashtable = true;
			}
		}

		// Token: 0x06000F9F RID: 3999 RVA: 0x0005C2D4 File Offset: 0x0005A4D4
		private void AddToNamespaceHashtable(int namespaceIndex)
		{
			string prefix = this.nsStack[namespaceIndex].prefix;
			int num;
			if (this.nsHashtable.TryGetValue(prefix, out num))
			{
				this.nsStack[namespaceIndex].prevNsIndex = num;
			}
			this.nsHashtable[prefix] = namespaceIndex;
		}

		// Token: 0x06000FA0 RID: 4000 RVA: 0x0005C324 File Offset: 0x0005A524
		private int LookupNamespaceIndex(string prefix)
		{
			if (this.useNsHashtable)
			{
				int num;
				if (this.nsHashtable.TryGetValue(prefix, out num))
				{
					return num;
				}
			}
			else
			{
				for (int i = this.nsTop; i >= 0; i--)
				{
					if (this.nsStack[i].prefix == prefix)
					{
						return i;
					}
				}
			}
			return -1;
		}

		// Token: 0x06000FA1 RID: 4001 RVA: 0x0005C378 File Offset: 0x0005A578
		private void PopNamespaces(int indexFrom, int indexTo)
		{
			for (int i = indexTo; i >= indexFrom; i--)
			{
				if (this.nsStack[i].prevNsIndex == -1)
				{
					this.nsHashtable.Remove(this.nsStack[i].prefix);
				}
				else
				{
					this.nsHashtable[this.nsStack[i].prefix] = this.nsStack[i].prevNsIndex;
				}
			}
		}

		// Token: 0x06000FA2 RID: 4002 RVA: 0x0005C3F4 File Offset: 0x0005A5F4
		private static XmlException DupAttrException(string prefix, string localName)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (prefix.Length > 0)
			{
				stringBuilder.Append(prefix);
				stringBuilder.Append(':');
			}
			stringBuilder.Append(localName);
			return new XmlException("'{0}' is a duplicate attribute name.", stringBuilder.ToString());
		}

		// Token: 0x06000FA3 RID: 4003 RVA: 0x0005C43C File Offset: 0x0005A63C
		private void AdvanceState(XmlWellFormedWriter.Token token)
		{
			if (this.currentState < XmlWellFormedWriter.State.Closed)
			{
				XmlWellFormedWriter.State state;
				for (;;)
				{
					state = this.stateTable[(int)(((int)token << 4) + (int)this.currentState)];
					if (state < XmlWellFormedWriter.State.Error)
					{
						break;
					}
					if (state != XmlWellFormedWriter.State.Error)
					{
						switch (state)
						{
						case XmlWellFormedWriter.State.StartContent:
							goto IL_00E3;
						case XmlWellFormedWriter.State.StartContentEle:
							goto IL_00F0;
						case XmlWellFormedWriter.State.StartContentB64:
							goto IL_00FD;
						case XmlWellFormedWriter.State.StartDoc:
							goto IL_010A;
						case XmlWellFormedWriter.State.StartDocEle:
							goto IL_0117;
						case XmlWellFormedWriter.State.EndAttrSEle:
							goto IL_0124;
						case XmlWellFormedWriter.State.EndAttrEEle:
							goto IL_0137;
						case XmlWellFormedWriter.State.EndAttrSCont:
							goto IL_014A;
						case XmlWellFormedWriter.State.EndAttrSAttr:
							goto IL_015D;
						case XmlWellFormedWriter.State.PostB64Cont:
							if (this.rawWriter != null)
							{
								this.rawWriter.WriteEndBase64();
							}
							this.currentState = XmlWellFormedWriter.State.Content;
							continue;
						case XmlWellFormedWriter.State.PostB64Attr:
							if (this.rawWriter != null)
							{
								this.rawWriter.WriteEndBase64();
							}
							this.currentState = XmlWellFormedWriter.State.Attribute;
							continue;
						case XmlWellFormedWriter.State.PostB64RootAttr:
							if (this.rawWriter != null)
							{
								this.rawWriter.WriteEndBase64();
							}
							this.currentState = XmlWellFormedWriter.State.RootLevelAttr;
							continue;
						case XmlWellFormedWriter.State.StartFragEle:
							goto IL_01C8;
						case XmlWellFormedWriter.State.StartFragCont:
							goto IL_01D2;
						case XmlWellFormedWriter.State.StartFragB64:
							goto IL_01DC;
						case XmlWellFormedWriter.State.StartRootLevelAttr:
							goto IL_01E6;
						}
						break;
					}
					goto IL_00D1;
				}
				goto IL_01EF;
				IL_00D1:
				this.ThrowInvalidStateTransition(token, this.currentState);
				goto IL_01EF;
				IL_00E3:
				this.StartElementContent();
				state = XmlWellFormedWriter.State.Content;
				goto IL_01EF;
				IL_00F0:
				this.StartElementContent();
				state = XmlWellFormedWriter.State.Element;
				goto IL_01EF;
				IL_00FD:
				this.StartElementContent();
				state = XmlWellFormedWriter.State.B64Content;
				goto IL_01EF;
				IL_010A:
				this.WriteStartDocument();
				state = XmlWellFormedWriter.State.Document;
				goto IL_01EF;
				IL_0117:
				this.WriteStartDocument();
				state = XmlWellFormedWriter.State.Element;
				goto IL_01EF;
				IL_0124:
				this.WriteEndAttribute();
				this.StartElementContent();
				state = XmlWellFormedWriter.State.Element;
				goto IL_01EF;
				IL_0137:
				this.WriteEndAttribute();
				this.StartElementContent();
				state = XmlWellFormedWriter.State.Content;
				goto IL_01EF;
				IL_014A:
				this.WriteEndAttribute();
				this.StartElementContent();
				state = XmlWellFormedWriter.State.Content;
				goto IL_01EF;
				IL_015D:
				this.WriteEndAttribute();
				state = XmlWellFormedWriter.State.Attribute;
				goto IL_01EF;
				IL_01C8:
				this.StartFragment();
				state = XmlWellFormedWriter.State.Element;
				goto IL_01EF;
				IL_01D2:
				this.StartFragment();
				state = XmlWellFormedWriter.State.Content;
				goto IL_01EF;
				IL_01DC:
				this.StartFragment();
				state = XmlWellFormedWriter.State.B64Content;
				goto IL_01EF;
				IL_01E6:
				this.WriteEndAttribute();
				state = XmlWellFormedWriter.State.RootLevelAttr;
				IL_01EF:
				this.currentState = state;
				return;
			}
			if (this.currentState == XmlWellFormedWriter.State.Closed || this.currentState == XmlWellFormedWriter.State.Error)
			{
				throw new InvalidOperationException(Res.GetString("The Writer is closed or in error state."));
			}
			throw new InvalidOperationException(Res.GetString("Token {0} in state {1} would result in an invalid XML document.", new object[]
			{
				XmlWellFormedWriter.tokenName[(int)token],
				XmlWellFormedWriter.GetStateName(this.currentState)
			}));
		}

		// Token: 0x06000FA4 RID: 4004 RVA: 0x0005C640 File Offset: 0x0005A840
		private void StartElementContent()
		{
			int prevNSTop = this.elemScopeStack[this.elemTop].prevNSTop;
			for (int i = this.nsTop; i > prevNSTop; i--)
			{
				if (this.nsStack[i].kind == XmlWellFormedWriter.NamespaceKind.NeedToWrite)
				{
					this.nsStack[i].WriteDecl(this.writer, this.rawWriter);
				}
			}
			if (this.rawWriter != null)
			{
				this.rawWriter.StartElementContent();
			}
		}

		// Token: 0x06000FA5 RID: 4005 RVA: 0x0005C6B9 File Offset: 0x0005A8B9
		private static string GetStateName(XmlWellFormedWriter.State state)
		{
			if (state >= XmlWellFormedWriter.State.Error)
			{
				return "Error";
			}
			return XmlWellFormedWriter.stateName[(int)state];
		}

		// Token: 0x06000FA6 RID: 4006 RVA: 0x0005C6D0 File Offset: 0x0005A8D0
		internal string LookupNamespace(string prefix)
		{
			for (int i = this.nsTop; i >= 0; i--)
			{
				if (this.nsStack[i].prefix == prefix)
				{
					return this.nsStack[i].namespaceUri;
				}
			}
			if (this.predefinedNamespaces == null)
			{
				return null;
			}
			return this.predefinedNamespaces.LookupNamespace(prefix);
		}

		// Token: 0x06000FA7 RID: 4007 RVA: 0x0005C730 File Offset: 0x0005A930
		private string LookupLocalNamespace(string prefix)
		{
			for (int i = this.nsTop; i > this.elemScopeStack[this.elemTop].prevNSTop; i--)
			{
				if (this.nsStack[i].prefix == prefix)
				{
					return this.nsStack[i].namespaceUri;
				}
			}
			return null;
		}

		// Token: 0x06000FA8 RID: 4008 RVA: 0x0005C790 File Offset: 0x0005A990
		private string GeneratePrefix()
		{
			string text = "p" + (this.nsTop - 2).ToString("d", CultureInfo.InvariantCulture);
			if (this.LookupNamespace(text) == null)
			{
				return text;
			}
			int num = 0;
			string text2;
			do
			{
				text2 = text + num.ToString(CultureInfo.InvariantCulture);
				num++;
			}
			while (this.LookupNamespace(text2) != null);
			return text2;
		}

		// Token: 0x06000FA9 RID: 4009 RVA: 0x0005C7F0 File Offset: 0x0005A9F0
		private void CheckNCName(string ncname)
		{
			int length = ncname.Length;
			if ((this.xmlCharType.charProperties[(int)ncname[0]] & 4) != 0)
			{
				for (int i = 1; i < length; i++)
				{
					if ((this.xmlCharType.charProperties[(int)ncname[i]] & 8) == 0)
					{
						throw XmlWellFormedWriter.InvalidCharsException(ncname, i);
					}
				}
				return;
			}
			throw XmlWellFormedWriter.InvalidCharsException(ncname, 0);
		}

		// Token: 0x06000FAA RID: 4010 RVA: 0x0005C850 File Offset: 0x0005AA50
		private static Exception InvalidCharsException(string name, int badCharIndex)
		{
			string[] array = XmlException.BuildCharExceptionArgs(name, badCharIndex);
			return new ArgumentException(Res.GetString("Invalid name character in '{0}'. The '{1}' character, hexadecimal value {2}, cannot be included in a name.", new string[]
			{
				name,
				array[0],
				array[1]
			}));
		}

		// Token: 0x06000FAB RID: 4011 RVA: 0x0005C88C File Offset: 0x0005AA8C
		private void ThrowInvalidStateTransition(XmlWellFormedWriter.Token token, XmlWellFormedWriter.State currentState)
		{
			string @string = Res.GetString("Token {0} in state {1} would result in an invalid XML document.", new object[]
			{
				XmlWellFormedWriter.tokenName[(int)token],
				XmlWellFormedWriter.GetStateName(currentState)
			});
			if ((currentState == XmlWellFormedWriter.State.Start || currentState == XmlWellFormedWriter.State.AfterRootEle) && this.conformanceLevel == ConformanceLevel.Document)
			{
				throw new InvalidOperationException(@string + " " + Res.GetString("Make sure that the ConformanceLevel setting is set to ConformanceLevel.Fragment or ConformanceLevel.Auto if you want to write an XML fragment."));
			}
			throw new InvalidOperationException(@string);
		}

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x06000FAC RID: 4012 RVA: 0x0005C8EE File Offset: 0x0005AAEE
		private bool IsClosedOrErrorState
		{
			get
			{
				return this.currentState >= XmlWellFormedWriter.State.Closed;
			}
		}

		// Token: 0x06000FAD RID: 4013 RVA: 0x0005C900 File Offset: 0x0005AB00
		private void AddAttribute(string prefix, string localName, string namespaceName)
		{
			int num = this.attrCount;
			this.attrCount = num + 1;
			int num2 = num;
			if (num2 == this.attrStack.Length)
			{
				XmlWellFormedWriter.AttrName[] array = new XmlWellFormedWriter.AttrName[num2 * 2];
				Array.Copy(this.attrStack, array, num2);
				this.attrStack = array;
			}
			this.attrStack[num2].Set(prefix, localName, namespaceName);
			if (this.attrCount < 14)
			{
				for (int i = 0; i < num2; i++)
				{
					if (this.attrStack[i].IsDuplicate(prefix, localName, namespaceName))
					{
						throw XmlWellFormedWriter.DupAttrException(prefix, localName);
					}
				}
				return;
			}
			if (this.attrCount == 14)
			{
				if (this.attrHashTable == null)
				{
					this.attrHashTable = new Dictionary<string, int>(this.hasher);
				}
				for (int j = 0; j < num2; j++)
				{
					this.AddToAttrHashTable(j);
				}
			}
			this.AddToAttrHashTable(num2);
			for (int k = this.attrStack[num2].prev; k > 0; k = this.attrStack[k].prev)
			{
				k--;
				if (this.attrStack[k].IsDuplicate(prefix, localName, namespaceName))
				{
					throw XmlWellFormedWriter.DupAttrException(prefix, localName);
				}
			}
		}

		// Token: 0x06000FAE RID: 4014 RVA: 0x0005CA28 File Offset: 0x0005AC28
		private void AddToAttrHashTable(int attributeIndex)
		{
			string localName = this.attrStack[attributeIndex].localName;
			int count = this.attrHashTable.Count;
			this.attrHashTable[localName] = 0;
			if (count != this.attrHashTable.Count)
			{
				return;
			}
			int num = attributeIndex - 1;
			while (num >= 0 && !(this.attrStack[num].localName == localName))
			{
				num--;
			}
			this.attrStack[attributeIndex].prev = num + 1;
		}

		// Token: 0x06000FAF RID: 4015 RVA: 0x0005CAA8 File Offset: 0x0005ACA8
		public override Task WriteStartDocumentAsync()
		{
			return this.WriteStartDocumentImplAsync(XmlStandalone.Omit);
		}

		// Token: 0x06000FB0 RID: 4016 RVA: 0x0005CAB1 File Offset: 0x0005ACB1
		public override Task WriteStartDocumentAsync(bool standalone)
		{
			return this.WriteStartDocumentImplAsync(standalone ? XmlStandalone.Yes : XmlStandalone.No);
		}

		// Token: 0x06000FB1 RID: 4017 RVA: 0x0005CAC0 File Offset: 0x0005ACC0
		public override async Task WriteEndDocumentAsync()
		{
			try
			{
				while (this.elemTop > 0)
				{
					await this.WriteEndElementAsync().ConfigureAwait(false);
				}
				XmlWellFormedWriter.State prevState = this.currentState;
				await this.AdvanceStateAsync(XmlWellFormedWriter.Token.EndDocument).ConfigureAwait(false);
				if (prevState != XmlWellFormedWriter.State.AfterRootEle)
				{
					throw new ArgumentException(Res.GetString("Document does not have a root element."));
				}
				if (this.rawWriter == null)
				{
					await this.writer.WriteEndDocumentAsync().ConfigureAwait(false);
				}
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000FB2 RID: 4018 RVA: 0x0005CB08 File Offset: 0x0005AD08
		public override async Task WriteDocTypeAsync(string name, string pubid, string sysid, string subset)
		{
			try
			{
				if (name == null || name.Length == 0)
				{
					throw new ArgumentException(Res.GetString("The empty string '' is not a valid name."));
				}
				XmlConvert.VerifyQName(name, ExceptionType.XmlException);
				if (this.conformanceLevel == ConformanceLevel.Fragment)
				{
					throw new InvalidOperationException(Res.GetString("DTD is not allowed in XML fragments."));
				}
				await this.AdvanceStateAsync(XmlWellFormedWriter.Token.Dtd).ConfigureAwait(false);
				if (this.dtdWritten)
				{
					this.currentState = XmlWellFormedWriter.State.Error;
					throw new InvalidOperationException(Res.GetString("The DTD has already been written out."));
				}
				if (this.conformanceLevel == ConformanceLevel.Auto)
				{
					this.conformanceLevel = ConformanceLevel.Document;
					this.stateTable = XmlWellFormedWriter.StateTableDocument;
				}
				if (this.checkCharacters)
				{
					int num;
					if (pubid != null && (num = this.xmlCharType.IsPublicId(pubid)) >= 0)
					{
						throw new ArgumentException(Res.GetString("'{0}', hexadecimal value {1}, is an invalid character.", XmlException.BuildCharExceptionArgs(pubid, num)), "pubid");
					}
					if (sysid != null && (num = this.xmlCharType.IsOnlyCharData(sysid)) >= 0)
					{
						throw new ArgumentException(Res.GetString("'{0}', hexadecimal value {1}, is an invalid character.", XmlException.BuildCharExceptionArgs(sysid, num)), "sysid");
					}
					if (subset != null && (num = this.xmlCharType.IsOnlyCharData(subset)) >= 0)
					{
						throw new ArgumentException(Res.GetString("'{0}', hexadecimal value {1}, is an invalid character.", XmlException.BuildCharExceptionArgs(subset, num)), "subset");
					}
				}
				await this.writer.WriteDocTypeAsync(name, pubid, sysid, subset).ConfigureAwait(false);
				this.dtdWritten = true;
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000FB3 RID: 4019 RVA: 0x0005CB6E File Offset: 0x0005AD6E
		private Task TryReturnTask(Task task)
		{
			if (task.IsSuccess())
			{
				return AsyncHelper.DoneTask;
			}
			return this._TryReturnTask(task);
		}

		// Token: 0x06000FB4 RID: 4020 RVA: 0x0005CB88 File Offset: 0x0005AD88
		private async Task _TryReturnTask(Task task)
		{
			try
			{
				await task.ConfigureAwait(false);
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000FB5 RID: 4021 RVA: 0x0005CBD5 File Offset: 0x0005ADD5
		private Task SequenceRun(Task task, Func<Task> nextTaskFun)
		{
			if (task.IsSuccess())
			{
				return this.TryReturnTask(nextTaskFun());
			}
			return this._SequenceRun(task, nextTaskFun);
		}

		// Token: 0x06000FB6 RID: 4022 RVA: 0x0005CBF4 File Offset: 0x0005ADF4
		private async Task _SequenceRun(Task task, Func<Task> nextTaskFun)
		{
			try
			{
				await task.ConfigureAwait(false);
				await nextTaskFun().ConfigureAwait(false);
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000FB7 RID: 4023 RVA: 0x0005CC4C File Offset: 0x0005AE4C
		public override Task WriteStartElementAsync(string prefix, string localName, string ns)
		{
			Task task2;
			try
			{
				if (localName == null || localName.Length == 0)
				{
					throw new ArgumentException(Res.GetString("The empty string '' is not a valid local name."));
				}
				this.CheckNCName(localName);
				Task task = this.AdvanceStateAsync(XmlWellFormedWriter.Token.StartElement);
				if (task.IsSuccess())
				{
					task2 = this.WriteStartElementAsync_NoAdvanceState(prefix, localName, ns);
				}
				else
				{
					task2 = this.WriteStartElementAsync_NoAdvanceState(task, prefix, localName, ns);
				}
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
			return task2;
		}

		// Token: 0x06000FB8 RID: 4024 RVA: 0x0005CCC0 File Offset: 0x0005AEC0
		private Task WriteStartElementAsync_NoAdvanceState(string prefix, string localName, string ns)
		{
			Task task2;
			try
			{
				if (prefix == null)
				{
					if (ns != null)
					{
						prefix = this.LookupPrefix(ns);
					}
					if (prefix == null)
					{
						prefix = string.Empty;
					}
				}
				else if (prefix.Length > 0)
				{
					this.CheckNCName(prefix);
					if (ns == null)
					{
						ns = this.LookupNamespace(prefix);
					}
					if (ns == null || (ns != null && ns.Length == 0))
					{
						throw new ArgumentException(Res.GetString("Cannot use a prefix with an empty namespace."));
					}
				}
				if (ns == null)
				{
					ns = this.LookupNamespace(prefix);
					if (ns == null)
					{
						ns = string.Empty;
					}
				}
				if (this.elemTop == 0 && this.rawWriter != null)
				{
					this.rawWriter.OnRootElement(this.conformanceLevel);
				}
				Task task = this.writer.WriteStartElementAsync(prefix, localName, ns);
				if (task.IsSuccess())
				{
					this.WriteStartElementAsync_FinishWrite(prefix, localName, ns);
					task2 = AsyncHelper.DoneTask;
				}
				else
				{
					task2 = this.WriteStartElementAsync_FinishWrite(task, prefix, localName, ns);
				}
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
			return task2;
		}

		// Token: 0x06000FB9 RID: 4025 RVA: 0x0005CDAC File Offset: 0x0005AFAC
		private async Task WriteStartElementAsync_NoAdvanceState(Task task, string prefix, string localName, string ns)
		{
			try
			{
				await task.ConfigureAwait(false);
				await this.WriteStartElementAsync_NoAdvanceState(prefix, localName, ns).ConfigureAwait(false);
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000FBA RID: 4026 RVA: 0x0005CE14 File Offset: 0x0005B014
		private void WriteStartElementAsync_FinishWrite(string prefix, string localName, string ns)
		{
			try
			{
				int num = this.elemTop + 1;
				this.elemTop = num;
				int num2 = num;
				if (num2 == this.elemScopeStack.Length)
				{
					XmlWellFormedWriter.ElementScope[] array = new XmlWellFormedWriter.ElementScope[num2 * 2];
					Array.Copy(this.elemScopeStack, array, num2);
					this.elemScopeStack = array;
				}
				this.elemScopeStack[num2].Set(prefix, localName, ns, this.nsTop);
				this.PushNamespaceImplicit(prefix, ns);
				if (this.attrCount >= 14)
				{
					this.attrHashTable.Clear();
				}
				this.attrCount = 0;
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000FBB RID: 4027 RVA: 0x0005CEB8 File Offset: 0x0005B0B8
		private async Task WriteStartElementAsync_FinishWrite(Task t, string prefix, string localName, string ns)
		{
			try
			{
				await t.ConfigureAwait(false);
				this.WriteStartElementAsync_FinishWrite(prefix, localName, ns);
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000FBC RID: 4028 RVA: 0x0005CF20 File Offset: 0x0005B120
		public override Task WriteEndElementAsync()
		{
			Task task2;
			try
			{
				Task task = this.AdvanceStateAsync(XmlWellFormedWriter.Token.EndElement);
				task2 = this.SequenceRun(task, new Func<Task>(this.WriteEndElementAsync_NoAdvanceState));
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
			return task2;
		}

		// Token: 0x06000FBD RID: 4029 RVA: 0x0005CF68 File Offset: 0x0005B168
		private Task WriteEndElementAsync_NoAdvanceState()
		{
			Task task2;
			try
			{
				int num = this.elemTop;
				if (num == 0)
				{
					throw new XmlException("There was no XML start tag open.", string.Empty);
				}
				Task task;
				if (this.rawWriter != null)
				{
					task = this.elemScopeStack[num].WriteEndElementAsync(this.rawWriter);
				}
				else
				{
					task = this.writer.WriteEndElementAsync();
				}
				task2 = this.SequenceRun(task, new Func<Task>(this.WriteEndElementAsync_FinishWrite));
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
			return task2;
		}

		// Token: 0x06000FBE RID: 4030 RVA: 0x0005CFF0 File Offset: 0x0005B1F0
		private Task WriteEndElementAsync_FinishWrite()
		{
			try
			{
				int num = this.elemTop;
				int prevNSTop = this.elemScopeStack[num].prevNSTop;
				if (this.useNsHashtable && prevNSTop < this.nsTop)
				{
					this.PopNamespaces(prevNSTop + 1, this.nsTop);
				}
				this.nsTop = prevNSTop;
				if ((this.elemTop = num - 1) == 0)
				{
					if (this.conformanceLevel == ConformanceLevel.Document)
					{
						this.currentState = XmlWellFormedWriter.State.AfterRootEle;
					}
					else
					{
						this.currentState = XmlWellFormedWriter.State.TopLevel;
					}
				}
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
			return AsyncHelper.DoneTask;
		}

		// Token: 0x06000FBF RID: 4031 RVA: 0x0005D088 File Offset: 0x0005B288
		public override Task WriteFullEndElementAsync()
		{
			Task task2;
			try
			{
				Task task = this.AdvanceStateAsync(XmlWellFormedWriter.Token.EndElement);
				task2 = this.SequenceRun(task, new Func<Task>(this.WriteFullEndElementAsync_NoAdvanceState));
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
			return task2;
		}

		// Token: 0x06000FC0 RID: 4032 RVA: 0x0005D0D0 File Offset: 0x0005B2D0
		private Task WriteFullEndElementAsync_NoAdvanceState()
		{
			Task task2;
			try
			{
				int num = this.elemTop;
				if (num == 0)
				{
					throw new XmlException("There was no XML start tag open.", string.Empty);
				}
				Task task;
				if (this.rawWriter != null)
				{
					task = this.elemScopeStack[num].WriteFullEndElementAsync(this.rawWriter);
				}
				else
				{
					task = this.writer.WriteFullEndElementAsync();
				}
				task2 = this.SequenceRun(task, new Func<Task>(this.WriteEndElementAsync_FinishWrite));
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
			return task2;
		}

		// Token: 0x06000FC1 RID: 4033 RVA: 0x0005D158 File Offset: 0x0005B358
		protected internal override Task WriteStartAttributeAsync(string prefix, string localName, string namespaceName)
		{
			Task task2;
			try
			{
				if (localName == null || localName.Length == 0)
				{
					if (!(prefix == "xmlns"))
					{
						throw new ArgumentException(Res.GetString("The empty string '' is not a valid local name."));
					}
					localName = "xmlns";
					prefix = string.Empty;
				}
				this.CheckNCName(localName);
				Task task = this.AdvanceStateAsync(XmlWellFormedWriter.Token.StartAttribute);
				if (task.IsSuccess())
				{
					task2 = this.WriteStartAttributeAsync_NoAdvanceState(prefix, localName, namespaceName);
				}
				else
				{
					task2 = this.WriteStartAttributeAsync_NoAdvanceState(task, prefix, localName, namespaceName);
				}
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
			return task2;
		}

		// Token: 0x06000FC2 RID: 4034 RVA: 0x0005D1EC File Offset: 0x0005B3EC
		private Task WriteStartAttributeAsync_NoAdvanceState(string prefix, string localName, string namespaceName)
		{
			Task task;
			try
			{
				if (prefix == null)
				{
					if (namespaceName != null && (!(localName == "xmlns") || !(namespaceName == "http://www.w3.org/2000/xmlns/")))
					{
						prefix = this.LookupPrefix(namespaceName);
					}
					if (prefix == null)
					{
						prefix = string.Empty;
					}
				}
				if (namespaceName == null)
				{
					if (prefix != null && prefix.Length > 0)
					{
						namespaceName = this.LookupNamespace(prefix);
					}
					if (namespaceName == null)
					{
						namespaceName = string.Empty;
					}
				}
				if (prefix.Length == 0)
				{
					if (localName[0] == 'x' && localName == "xmlns")
					{
						if (namespaceName.Length > 0 && namespaceName != "http://www.w3.org/2000/xmlns/")
						{
							throw new ArgumentException(Res.GetString("Prefix \"xmlns\" is reserved for use by XML."));
						}
						this.curDeclPrefix = string.Empty;
						this.SetSpecialAttribute(XmlWellFormedWriter.SpecialAttribute.DefaultXmlns);
						goto IL_01DE;
					}
					else if (namespaceName.Length > 0)
					{
						prefix = this.LookupPrefix(namespaceName);
						if (prefix == null || prefix.Length == 0)
						{
							prefix = this.GeneratePrefix();
						}
					}
				}
				else
				{
					if (prefix[0] == 'x')
					{
						if (prefix == "xmlns")
						{
							if (namespaceName.Length > 0 && namespaceName != "http://www.w3.org/2000/xmlns/")
							{
								throw new ArgumentException(Res.GetString("Prefix \"xmlns\" is reserved for use by XML."));
							}
							this.curDeclPrefix = localName;
							this.SetSpecialAttribute(XmlWellFormedWriter.SpecialAttribute.PrefixedXmlns);
							goto IL_01DE;
						}
						else if (prefix == "xml")
						{
							if (namespaceName.Length > 0 && namespaceName != "http://www.w3.org/XML/1998/namespace")
							{
								throw new ArgumentException(Res.GetString("Prefix \"xml\" is reserved for use by XML and can be mapped only to namespace name \"http://www.w3.org/XML/1998/namespace\"."));
							}
							if (localName == "space")
							{
								this.SetSpecialAttribute(XmlWellFormedWriter.SpecialAttribute.XmlSpace);
								goto IL_01DE;
							}
							if (localName == "lang")
							{
								this.SetSpecialAttribute(XmlWellFormedWriter.SpecialAttribute.XmlLang);
								goto IL_01DE;
							}
						}
					}
					this.CheckNCName(prefix);
					if (namespaceName.Length == 0)
					{
						prefix = string.Empty;
					}
					else
					{
						string text = this.LookupLocalNamespace(prefix);
						if (text != null && text != namespaceName)
						{
							prefix = this.GeneratePrefix();
						}
					}
				}
				if (prefix.Length != 0)
				{
					this.PushNamespaceImplicit(prefix, namespaceName);
				}
				IL_01DE:
				this.AddAttribute(prefix, localName, namespaceName);
				if (this.specAttr == XmlWellFormedWriter.SpecialAttribute.No)
				{
					task = this.TryReturnTask(this.writer.WriteStartAttributeAsync(prefix, localName, namespaceName));
				}
				else
				{
					task = AsyncHelper.DoneTask;
				}
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
			return task;
		}

		// Token: 0x06000FC3 RID: 4035 RVA: 0x0005D430 File Offset: 0x0005B630
		private async Task WriteStartAttributeAsync_NoAdvanceState(Task task, string prefix, string localName, string namespaceName)
		{
			try
			{
				await task.ConfigureAwait(false);
				await this.WriteStartAttributeAsync_NoAdvanceState(prefix, localName, namespaceName).ConfigureAwait(false);
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000FC4 RID: 4036 RVA: 0x0005D498 File Offset: 0x0005B698
		protected internal override Task WriteEndAttributeAsync()
		{
			Task task2;
			try
			{
				Task task = this.AdvanceStateAsync(XmlWellFormedWriter.Token.EndAttribute);
				task2 = this.SequenceRun(task, new Func<Task>(this.WriteEndAttributeAsync_NoAdvance));
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
			return task2;
		}

		// Token: 0x06000FC5 RID: 4037 RVA: 0x0005D4E0 File Offset: 0x0005B6E0
		private Task WriteEndAttributeAsync_NoAdvance()
		{
			Task task;
			try
			{
				if (this.specAttr != XmlWellFormedWriter.SpecialAttribute.No)
				{
					task = this.WriteEndAttributeAsync_SepcialAtt();
				}
				else
				{
					task = this.TryReturnTask(this.writer.WriteEndAttributeAsync());
				}
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
			return task;
		}

		// Token: 0x06000FC6 RID: 4038 RVA: 0x0005D530 File Offset: 0x0005B730
		private async Task WriteEndAttributeAsync_SepcialAtt()
		{
			try
			{
				string value;
				switch (this.specAttr)
				{
				case XmlWellFormedWriter.SpecialAttribute.DefaultXmlns:
					value = this.attrValueCache.StringValue;
					if (this.PushNamespaceExplicit(string.Empty, value))
					{
						if (this.rawWriter != null)
						{
							if (this.rawWriter.SupportsNamespaceDeclarationInChunks)
							{
								await this.rawWriter.WriteStartNamespaceDeclarationAsync(string.Empty).ConfigureAwait(false);
								await this.attrValueCache.ReplayAsync(this.rawWriter).ConfigureAwait(false);
								await this.rawWriter.WriteEndNamespaceDeclarationAsync().ConfigureAwait(false);
							}
							else
							{
								await this.rawWriter.WriteNamespaceDeclarationAsync(string.Empty, value).ConfigureAwait(false);
							}
						}
						else
						{
							await this.writer.WriteStartAttributeAsync(string.Empty, "xmlns", "http://www.w3.org/2000/xmlns/").ConfigureAwait(false);
							await this.attrValueCache.ReplayAsync(this.writer).ConfigureAwait(false);
							await this.writer.WriteEndAttributeAsync().ConfigureAwait(false);
						}
					}
					this.curDeclPrefix = null;
					break;
				case XmlWellFormedWriter.SpecialAttribute.PrefixedXmlns:
					value = this.attrValueCache.StringValue;
					if (value.Length == 0)
					{
						throw new ArgumentException(Res.GetString("Cannot use a prefix with an empty namespace."));
					}
					if (value == "http://www.w3.org/2000/xmlns/" || (value == "http://www.w3.org/XML/1998/namespace" && this.curDeclPrefix != "xml"))
					{
						throw new ArgumentException(Res.GetString("Cannot bind to the reserved namespace."));
					}
					if (this.PushNamespaceExplicit(this.curDeclPrefix, value))
					{
						if (this.rawWriter != null)
						{
							if (this.rawWriter.SupportsNamespaceDeclarationInChunks)
							{
								await this.rawWriter.WriteStartNamespaceDeclarationAsync(this.curDeclPrefix).ConfigureAwait(false);
								await this.attrValueCache.ReplayAsync(this.rawWriter).ConfigureAwait(false);
								await this.rawWriter.WriteEndNamespaceDeclarationAsync().ConfigureAwait(false);
							}
							else
							{
								await this.rawWriter.WriteNamespaceDeclarationAsync(this.curDeclPrefix, value).ConfigureAwait(false);
							}
						}
						else
						{
							await this.writer.WriteStartAttributeAsync("xmlns", this.curDeclPrefix, "http://www.w3.org/2000/xmlns/").ConfigureAwait(false);
							await this.attrValueCache.ReplayAsync(this.writer).ConfigureAwait(false);
							await this.writer.WriteEndAttributeAsync().ConfigureAwait(false);
						}
					}
					this.curDeclPrefix = null;
					break;
				case XmlWellFormedWriter.SpecialAttribute.XmlSpace:
					this.attrValueCache.Trim();
					value = this.attrValueCache.StringValue;
					if (value == "default")
					{
						this.elemScopeStack[this.elemTop].xmlSpace = XmlSpace.Default;
					}
					else
					{
						if (!(value == "preserve"))
						{
							throw new ArgumentException(Res.GetString("'{0}' is an invalid xml:space value.", new object[] { value }));
						}
						this.elemScopeStack[this.elemTop].xmlSpace = XmlSpace.Preserve;
					}
					await this.writer.WriteStartAttributeAsync("xml", "space", "http://www.w3.org/XML/1998/namespace").ConfigureAwait(false);
					await this.attrValueCache.ReplayAsync(this.writer).ConfigureAwait(false);
					await this.writer.WriteEndAttributeAsync().ConfigureAwait(false);
					break;
				case XmlWellFormedWriter.SpecialAttribute.XmlLang:
					value = this.attrValueCache.StringValue;
					this.elemScopeStack[this.elemTop].xmlLang = value;
					await this.writer.WriteStartAttributeAsync("xml", "lang", "http://www.w3.org/XML/1998/namespace").ConfigureAwait(false);
					await this.attrValueCache.ReplayAsync(this.writer).ConfigureAwait(false);
					await this.writer.WriteEndAttributeAsync().ConfigureAwait(false);
					break;
				}
				this.specAttr = XmlWellFormedWriter.SpecialAttribute.No;
				this.attrValueCache.Clear();
				value = null;
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000FC7 RID: 4039 RVA: 0x0005D578 File Offset: 0x0005B778
		public override async Task WriteCDataAsync(string text)
		{
			try
			{
				if (text == null)
				{
					text = string.Empty;
				}
				await this.AdvanceStateAsync(XmlWellFormedWriter.Token.CData).ConfigureAwait(false);
				await this.writer.WriteCDataAsync(text).ConfigureAwait(false);
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000FC8 RID: 4040 RVA: 0x0005D5C8 File Offset: 0x0005B7C8
		public override async Task WriteCommentAsync(string text)
		{
			try
			{
				if (text == null)
				{
					text = string.Empty;
				}
				await this.AdvanceStateAsync(XmlWellFormedWriter.Token.Comment).ConfigureAwait(false);
				await this.writer.WriteCommentAsync(text).ConfigureAwait(false);
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000FC9 RID: 4041 RVA: 0x0005D618 File Offset: 0x0005B818
		public override async Task WriteProcessingInstructionAsync(string name, string text)
		{
			try
			{
				if (name == null || name.Length == 0)
				{
					throw new ArgumentException(Res.GetString("The empty string '' is not a valid name."));
				}
				this.CheckNCName(name);
				if (text == null)
				{
					text = string.Empty;
				}
				if (name.Length == 3 && string.Compare(name, "xml", StringComparison.OrdinalIgnoreCase) == 0)
				{
					if (this.currentState != XmlWellFormedWriter.State.Start)
					{
						throw new ArgumentException(Res.GetString((this.conformanceLevel == ConformanceLevel.Document) ? "Cannot write XML declaration. WriteStartDocument method has already written it." : "Cannot write XML declaration. XML declaration can be only at the beginning of the document."));
					}
					this.xmlDeclFollows = true;
					await this.AdvanceStateAsync(XmlWellFormedWriter.Token.PI).ConfigureAwait(false);
					if (this.rawWriter != null)
					{
						await this.rawWriter.WriteXmlDeclarationAsync(text).ConfigureAwait(false);
					}
					else
					{
						await this.writer.WriteProcessingInstructionAsync(name, text).ConfigureAwait(false);
					}
				}
				else
				{
					await this.AdvanceStateAsync(XmlWellFormedWriter.Token.PI).ConfigureAwait(false);
					await this.writer.WriteProcessingInstructionAsync(name, text).ConfigureAwait(false);
				}
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000FCA RID: 4042 RVA: 0x0005D670 File Offset: 0x0005B870
		public override async Task WriteEntityRefAsync(string name)
		{
			try
			{
				if (name == null || name.Length == 0)
				{
					throw new ArgumentException(Res.GetString("The empty string '' is not a valid name."));
				}
				this.CheckNCName(name);
				await this.AdvanceStateAsync(XmlWellFormedWriter.Token.Text).ConfigureAwait(false);
				if (this.SaveAttrValue)
				{
					this.attrValueCache.WriteEntityRef(name);
				}
				else
				{
					await this.writer.WriteEntityRefAsync(name).ConfigureAwait(false);
				}
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000FCB RID: 4043 RVA: 0x0005D6C0 File Offset: 0x0005B8C0
		public override async Task WriteCharEntityAsync(char ch)
		{
			try
			{
				if (char.IsSurrogate(ch))
				{
					throw new ArgumentException(Res.GetString("The surrogate pair is invalid. Missing a low surrogate character."));
				}
				await this.AdvanceStateAsync(XmlWellFormedWriter.Token.Text).ConfigureAwait(false);
				if (this.SaveAttrValue)
				{
					this.attrValueCache.WriteCharEntity(ch);
				}
				else
				{
					await this.writer.WriteCharEntityAsync(ch).ConfigureAwait(false);
				}
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000FCC RID: 4044 RVA: 0x0005D710 File Offset: 0x0005B910
		public override async Task WriteSurrogateCharEntityAsync(char lowChar, char highChar)
		{
			try
			{
				if (!char.IsSurrogatePair(highChar, lowChar))
				{
					throw XmlConvert.CreateInvalidSurrogatePairException(lowChar, highChar);
				}
				await this.AdvanceStateAsync(XmlWellFormedWriter.Token.Text).ConfigureAwait(false);
				if (this.SaveAttrValue)
				{
					this.attrValueCache.WriteSurrogateCharEntity(lowChar, highChar);
				}
				else
				{
					await this.writer.WriteSurrogateCharEntityAsync(lowChar, highChar).ConfigureAwait(false);
				}
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000FCD RID: 4045 RVA: 0x0005D768 File Offset: 0x0005B968
		public override async Task WriteWhitespaceAsync(string ws)
		{
			try
			{
				if (ws == null)
				{
					ws = string.Empty;
				}
				if (!XmlCharType.Instance.IsOnlyWhitespace(ws))
				{
					throw new ArgumentException(Res.GetString("Only white space characters should be used."));
				}
				await this.AdvanceStateAsync(XmlWellFormedWriter.Token.Whitespace).ConfigureAwait(false);
				if (this.SaveAttrValue)
				{
					this.attrValueCache.WriteWhitespace(ws);
				}
				else
				{
					await this.writer.WriteWhitespaceAsync(ws).ConfigureAwait(false);
				}
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000FCE RID: 4046 RVA: 0x0005D7B8 File Offset: 0x0005B9B8
		public override Task WriteStringAsync(string text)
		{
			Task task;
			try
			{
				if (text == null)
				{
					task = AsyncHelper.DoneTask;
				}
				else
				{
					Task task2 = this.AdvanceStateAsync(XmlWellFormedWriter.Token.Text);
					if (task2.IsSuccess())
					{
						task = this.WriteStringAsync_NoAdvanceState(text);
					}
					else
					{
						task = this.WriteStringAsync_NoAdvanceState(task2, text);
					}
				}
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
			return task;
		}

		// Token: 0x06000FCF RID: 4047 RVA: 0x0005D814 File Offset: 0x0005BA14
		private Task WriteStringAsync_NoAdvanceState(string text)
		{
			Task task;
			try
			{
				if (this.SaveAttrValue)
				{
					this.attrValueCache.WriteString(text);
					task = AsyncHelper.DoneTask;
				}
				else
				{
					task = this.TryReturnTask(this.writer.WriteStringAsync(text));
				}
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
			return task;
		}

		// Token: 0x06000FD0 RID: 4048 RVA: 0x0005D870 File Offset: 0x0005BA70
		private async Task WriteStringAsync_NoAdvanceState(Task task, string text)
		{
			try
			{
				await task.ConfigureAwait(false);
				await this.WriteStringAsync_NoAdvanceState(text).ConfigureAwait(false);
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000FD1 RID: 4049 RVA: 0x0005D8C8 File Offset: 0x0005BAC8
		public override async Task WriteCharsAsync(char[] buffer, int index, int count)
		{
			try
			{
				if (buffer == null)
				{
					throw new ArgumentNullException("buffer");
				}
				if (index < 0)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				if (count < 0)
				{
					throw new ArgumentOutOfRangeException("count");
				}
				if (count > buffer.Length - index)
				{
					throw new ArgumentOutOfRangeException("count");
				}
				await this.AdvanceStateAsync(XmlWellFormedWriter.Token.Text).ConfigureAwait(false);
				if (this.SaveAttrValue)
				{
					this.attrValueCache.WriteChars(buffer, index, count);
				}
				else
				{
					await this.writer.WriteCharsAsync(buffer, index, count).ConfigureAwait(false);
				}
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000FD2 RID: 4050 RVA: 0x0005D928 File Offset: 0x0005BB28
		public override async Task WriteRawAsync(char[] buffer, int index, int count)
		{
			try
			{
				if (buffer == null)
				{
					throw new ArgumentNullException("buffer");
				}
				if (index < 0)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				if (count < 0)
				{
					throw new ArgumentOutOfRangeException("count");
				}
				if (count > buffer.Length - index)
				{
					throw new ArgumentOutOfRangeException("count");
				}
				await this.AdvanceStateAsync(XmlWellFormedWriter.Token.RawData).ConfigureAwait(false);
				if (this.SaveAttrValue)
				{
					this.attrValueCache.WriteRaw(buffer, index, count);
				}
				else
				{
					await this.writer.WriteRawAsync(buffer, index, count).ConfigureAwait(false);
				}
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000FD3 RID: 4051 RVA: 0x0005D988 File Offset: 0x0005BB88
		public override async Task WriteRawAsync(string data)
		{
			try
			{
				if (data != null)
				{
					await this.AdvanceStateAsync(XmlWellFormedWriter.Token.RawData).ConfigureAwait(false);
					if (this.SaveAttrValue)
					{
						this.attrValueCache.WriteRaw(data);
					}
					else
					{
						await this.writer.WriteRawAsync(data).ConfigureAwait(false);
					}
				}
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000FD4 RID: 4052 RVA: 0x0005D9D8 File Offset: 0x0005BBD8
		public override Task WriteBase64Async(byte[] buffer, int index, int count)
		{
			Task task2;
			try
			{
				if (buffer == null)
				{
					throw new ArgumentNullException("buffer");
				}
				if (index < 0)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				if (count < 0)
				{
					throw new ArgumentOutOfRangeException("count");
				}
				if (count > buffer.Length - index)
				{
					throw new ArgumentOutOfRangeException("count");
				}
				Task task = this.AdvanceStateAsync(XmlWellFormedWriter.Token.Base64);
				if (task.IsSuccess())
				{
					task2 = this.TryReturnTask(this.writer.WriteBase64Async(buffer, index, count));
				}
				else
				{
					task2 = this.WriteBase64Async_NoAdvanceState(task, buffer, index, count);
				}
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
			return task2;
		}

		// Token: 0x06000FD5 RID: 4053 RVA: 0x0005DA78 File Offset: 0x0005BC78
		private async Task WriteBase64Async_NoAdvanceState(Task task, byte[] buffer, int index, int count)
		{
			try
			{
				await task.ConfigureAwait(false);
				await this.writer.WriteBase64Async(buffer, index, count).ConfigureAwait(false);
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000FD6 RID: 4054 RVA: 0x0005DAE0 File Offset: 0x0005BCE0
		public override async Task FlushAsync()
		{
			try
			{
				await this.writer.FlushAsync().ConfigureAwait(false);
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000FD7 RID: 4055 RVA: 0x0005DB28 File Offset: 0x0005BD28
		public override async Task WriteQualifiedNameAsync(string localName, string ns)
		{
			try
			{
				if (localName == null || localName.Length == 0)
				{
					throw new ArgumentException(Res.GetString("The empty string '' is not a valid local name."));
				}
				this.CheckNCName(localName);
				await this.AdvanceStateAsync(XmlWellFormedWriter.Token.Text).ConfigureAwait(false);
				string prefix = string.Empty;
				if (ns != null && ns.Length != 0)
				{
					prefix = this.LookupPrefix(ns);
					if (prefix == null)
					{
						if (this.currentState != XmlWellFormedWriter.State.Attribute)
						{
							throw new ArgumentException(Res.GetString("The '{0}' namespace is not defined.", new object[] { ns }));
						}
						prefix = this.GeneratePrefix();
						this.PushNamespaceImplicit(prefix, ns);
					}
				}
				if (this.SaveAttrValue || this.rawWriter == null)
				{
					if (prefix.Length != 0)
					{
						await this.WriteStringAsync(prefix).ConfigureAwait(false);
						await this.WriteStringAsync(":").ConfigureAwait(false);
					}
					await this.WriteStringAsync(localName).ConfigureAwait(false);
				}
				else
				{
					await this.rawWriter.WriteQualifiedNameAsync(prefix, localName, ns).ConfigureAwait(false);
				}
				prefix = null;
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000FD8 RID: 4056 RVA: 0x0005DB80 File Offset: 0x0005BD80
		public override async Task WriteBinHexAsync(byte[] buffer, int index, int count)
		{
			if (this.IsClosedOrErrorState)
			{
				throw new InvalidOperationException(Res.GetString("The Writer is closed or in error state."));
			}
			try
			{
				await this.AdvanceStateAsync(XmlWellFormedWriter.Token.Text).ConfigureAwait(false);
				await base.WriteBinHexAsync(buffer, index, count).ConfigureAwait(false);
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000FD9 RID: 4057 RVA: 0x0005DBE0 File Offset: 0x0005BDE0
		private async Task WriteStartDocumentImplAsync(XmlStandalone standalone)
		{
			try
			{
				await this.AdvanceStateAsync(XmlWellFormedWriter.Token.StartDocument).ConfigureAwait(false);
				if (this.conformanceLevel == ConformanceLevel.Auto)
				{
					this.conformanceLevel = ConformanceLevel.Document;
					this.stateTable = XmlWellFormedWriter.StateTableDocument;
				}
				else if (this.conformanceLevel == ConformanceLevel.Fragment)
				{
					throw new InvalidOperationException(Res.GetString("WriteStartDocument cannot be called on writers created with ConformanceLevel.Fragment."));
				}
				if (this.rawWriter != null)
				{
					if (!this.xmlDeclFollows)
					{
						await this.rawWriter.WriteXmlDeclarationAsync(standalone).ConfigureAwait(false);
					}
				}
				else
				{
					await this.writer.WriteStartDocumentAsync().ConfigureAwait(false);
				}
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000FDA RID: 4058 RVA: 0x0005DC2D File Offset: 0x0005BE2D
		private Task AdvanceStateAsync_ReturnWhenFinish(Task task, XmlWellFormedWriter.State newState)
		{
			if (task.IsSuccess())
			{
				this.currentState = newState;
				return AsyncHelper.DoneTask;
			}
			return this._AdvanceStateAsync_ReturnWhenFinish(task, newState);
		}

		// Token: 0x06000FDB RID: 4059 RVA: 0x0005DC4C File Offset: 0x0005BE4C
		private async Task _AdvanceStateAsync_ReturnWhenFinish(Task task, XmlWellFormedWriter.State newState)
		{
			await task.ConfigureAwait(false);
			this.currentState = newState;
		}

		// Token: 0x06000FDC RID: 4060 RVA: 0x0005DCA1 File Offset: 0x0005BEA1
		private Task AdvanceStateAsync_ContinueWhenFinish(Task task, XmlWellFormedWriter.State newState, XmlWellFormedWriter.Token token)
		{
			if (task.IsSuccess())
			{
				this.currentState = newState;
				return this.AdvanceStateAsync(token);
			}
			return this._AdvanceStateAsync_ContinueWhenFinish(task, newState, token);
		}

		// Token: 0x06000FDD RID: 4061 RVA: 0x0005DCC4 File Offset: 0x0005BEC4
		private async Task _AdvanceStateAsync_ContinueWhenFinish(Task task, XmlWellFormedWriter.State newState, XmlWellFormedWriter.Token token)
		{
			await task.ConfigureAwait(false);
			this.currentState = newState;
			await this.AdvanceStateAsync(token).ConfigureAwait(false);
		}

		// Token: 0x06000FDE RID: 4062 RVA: 0x0005DD24 File Offset: 0x0005BF24
		private Task AdvanceStateAsync(XmlWellFormedWriter.Token token)
		{
			if (this.currentState < XmlWellFormedWriter.State.Closed)
			{
				XmlWellFormedWriter.State state;
				for (;;)
				{
					state = this.stateTable[(int)(((int)token << 4) + (int)this.currentState)];
					if (state < XmlWellFormedWriter.State.Error)
					{
						break;
					}
					if (state != XmlWellFormedWriter.State.Error)
					{
						switch (state)
						{
						case XmlWellFormedWriter.State.StartContent:
							goto IL_00E3;
						case XmlWellFormedWriter.State.StartContentEle:
							goto IL_00F1;
						case XmlWellFormedWriter.State.StartContentB64:
							goto IL_00FF;
						case XmlWellFormedWriter.State.StartDoc:
							goto IL_010D;
						case XmlWellFormedWriter.State.StartDocEle:
							goto IL_011B;
						case XmlWellFormedWriter.State.EndAttrSEle:
							goto IL_0129;
						case XmlWellFormedWriter.State.EndAttrEEle:
							goto IL_014B;
						case XmlWellFormedWriter.State.EndAttrSCont:
							goto IL_016D;
						case XmlWellFormedWriter.State.EndAttrSAttr:
							goto IL_018F;
						case XmlWellFormedWriter.State.PostB64Cont:
							if (this.rawWriter != null)
							{
								goto Block_6;
							}
							this.currentState = XmlWellFormedWriter.State.Content;
							continue;
						case XmlWellFormedWriter.State.PostB64Attr:
							if (this.rawWriter != null)
							{
								goto Block_7;
							}
							this.currentState = XmlWellFormedWriter.State.Attribute;
							continue;
						case XmlWellFormedWriter.State.PostB64RootAttr:
							if (this.rawWriter != null)
							{
								goto Block_8;
							}
							this.currentState = XmlWellFormedWriter.State.RootLevelAttr;
							continue;
						case XmlWellFormedWriter.State.StartFragEle:
							goto IL_0217;
						case XmlWellFormedWriter.State.StartFragCont:
							goto IL_0221;
						case XmlWellFormedWriter.State.StartFragB64:
							goto IL_022B;
						case XmlWellFormedWriter.State.StartRootLevelAttr:
							goto IL_0235;
						}
						break;
					}
					goto IL_00D1;
				}
				goto IL_0244;
				IL_00D1:
				this.ThrowInvalidStateTransition(token, this.currentState);
				goto IL_0244;
				IL_00E3:
				return this.AdvanceStateAsync_ReturnWhenFinish(this.StartElementContentAsync(), XmlWellFormedWriter.State.Content);
				IL_00F1:
				return this.AdvanceStateAsync_ReturnWhenFinish(this.StartElementContentAsync(), XmlWellFormedWriter.State.Element);
				IL_00FF:
				return this.AdvanceStateAsync_ReturnWhenFinish(this.StartElementContentAsync(), XmlWellFormedWriter.State.B64Content);
				IL_010D:
				return this.AdvanceStateAsync_ReturnWhenFinish(this.WriteStartDocumentAsync(), XmlWellFormedWriter.State.Document);
				IL_011B:
				return this.AdvanceStateAsync_ReturnWhenFinish(this.WriteStartDocumentAsync(), XmlWellFormedWriter.State.Element);
				IL_0129:
				Task task = this.SequenceRun(this.WriteEndAttributeAsync(), new Func<Task>(this.StartElementContentAsync));
				return this.AdvanceStateAsync_ReturnWhenFinish(task, XmlWellFormedWriter.State.Element);
				IL_014B:
				task = this.SequenceRun(this.WriteEndAttributeAsync(), new Func<Task>(this.StartElementContentAsync));
				return this.AdvanceStateAsync_ReturnWhenFinish(task, XmlWellFormedWriter.State.Content);
				IL_016D:
				task = this.SequenceRun(this.WriteEndAttributeAsync(), new Func<Task>(this.StartElementContentAsync));
				return this.AdvanceStateAsync_ReturnWhenFinish(task, XmlWellFormedWriter.State.Content);
				IL_018F:
				return this.AdvanceStateAsync_ReturnWhenFinish(this.WriteEndAttributeAsync(), XmlWellFormedWriter.State.Attribute);
				Block_6:
				return this.AdvanceStateAsync_ContinueWhenFinish(this.rawWriter.WriteEndBase64Async(), XmlWellFormedWriter.State.Content, token);
				Block_7:
				return this.AdvanceStateAsync_ContinueWhenFinish(this.rawWriter.WriteEndBase64Async(), XmlWellFormedWriter.State.Attribute, token);
				Block_8:
				return this.AdvanceStateAsync_ContinueWhenFinish(this.rawWriter.WriteEndBase64Async(), XmlWellFormedWriter.State.RootLevelAttr, token);
				IL_0217:
				this.StartFragment();
				state = XmlWellFormedWriter.State.Element;
				goto IL_0244;
				IL_0221:
				this.StartFragment();
				state = XmlWellFormedWriter.State.Content;
				goto IL_0244;
				IL_022B:
				this.StartFragment();
				state = XmlWellFormedWriter.State.B64Content;
				goto IL_0244;
				IL_0235:
				return this.AdvanceStateAsync_ReturnWhenFinish(this.WriteEndAttributeAsync(), XmlWellFormedWriter.State.RootLevelAttr);
				IL_0244:
				this.currentState = state;
				return AsyncHelper.DoneTask;
			}
			if (this.currentState == XmlWellFormedWriter.State.Closed || this.currentState == XmlWellFormedWriter.State.Error)
			{
				throw new InvalidOperationException(Res.GetString("The Writer is closed or in error state."));
			}
			throw new InvalidOperationException(Res.GetString("Token {0} in state {1} would result in an invalid XML document.", new object[]
			{
				XmlWellFormedWriter.tokenName[(int)token],
				XmlWellFormedWriter.GetStateName(this.currentState)
			}));
		}

		// Token: 0x06000FDF RID: 4063 RVA: 0x0005DF84 File Offset: 0x0005C184
		private async Task StartElementContentAsync_WithNS()
		{
			int start = this.elemScopeStack[this.elemTop].prevNSTop;
			for (int i = this.nsTop; i > start; i--)
			{
				if (this.nsStack[i].kind == XmlWellFormedWriter.NamespaceKind.NeedToWrite)
				{
					await this.nsStack[i].WriteDeclAsync(this.writer, this.rawWriter).ConfigureAwait(false);
				}
			}
			if (this.rawWriter != null)
			{
				this.rawWriter.StartElementContent();
			}
		}

		// Token: 0x06000FE0 RID: 4064 RVA: 0x0005DFC9 File Offset: 0x0005C1C9
		private Task StartElementContentAsync()
		{
			if (this.nsTop > this.elemScopeStack[this.elemTop].prevNSTop)
			{
				return this.StartElementContentAsync_WithNS();
			}
			if (this.rawWriter != null)
			{
				this.rawWriter.StartElementContent();
			}
			return AsyncHelper.DoneTask;
		}

		// Token: 0x04000AAF RID: 2735
		private XmlWriter writer;

		// Token: 0x04000AB0 RID: 2736
		private XmlRawWriter rawWriter;

		// Token: 0x04000AB1 RID: 2737
		private IXmlNamespaceResolver predefinedNamespaces;

		// Token: 0x04000AB2 RID: 2738
		private XmlWellFormedWriter.Namespace[] nsStack;

		// Token: 0x04000AB3 RID: 2739
		private int nsTop;

		// Token: 0x04000AB4 RID: 2740
		private Dictionary<string, int> nsHashtable;

		// Token: 0x04000AB5 RID: 2741
		private bool useNsHashtable;

		// Token: 0x04000AB6 RID: 2742
		private XmlWellFormedWriter.ElementScope[] elemScopeStack;

		// Token: 0x04000AB7 RID: 2743
		private int elemTop;

		// Token: 0x04000AB8 RID: 2744
		private XmlWellFormedWriter.AttrName[] attrStack;

		// Token: 0x04000AB9 RID: 2745
		private int attrCount;

		// Token: 0x04000ABA RID: 2746
		private Dictionary<string, int> attrHashTable;

		// Token: 0x04000ABB RID: 2747
		private XmlWellFormedWriter.SpecialAttribute specAttr;

		// Token: 0x04000ABC RID: 2748
		private XmlWellFormedWriter.AttributeValueCache attrValueCache;

		// Token: 0x04000ABD RID: 2749
		private string curDeclPrefix;

		// Token: 0x04000ABE RID: 2750
		private XmlWellFormedWriter.State[] stateTable;

		// Token: 0x04000ABF RID: 2751
		private XmlWellFormedWriter.State currentState;

		// Token: 0x04000AC0 RID: 2752
		private bool checkCharacters;

		// Token: 0x04000AC1 RID: 2753
		private bool omitDuplNamespaces;

		// Token: 0x04000AC2 RID: 2754
		private bool writeEndDocumentOnClose;

		// Token: 0x04000AC3 RID: 2755
		private ConformanceLevel conformanceLevel;

		// Token: 0x04000AC4 RID: 2756
		private bool dtdWritten;

		// Token: 0x04000AC5 RID: 2757
		private bool xmlDeclFollows;

		// Token: 0x04000AC6 RID: 2758
		private XmlCharType xmlCharType = XmlCharType.Instance;

		// Token: 0x04000AC7 RID: 2759
		private SecureStringHasher hasher;

		// Token: 0x04000AC8 RID: 2760
		private const int ElementStackInitialSize = 8;

		// Token: 0x04000AC9 RID: 2761
		private const int NamespaceStackInitialSize = 8;

		// Token: 0x04000ACA RID: 2762
		private const int AttributeArrayInitialSize = 8;

		// Token: 0x04000ACB RID: 2763
		private const int MaxAttrDuplWalkCount = 14;

		// Token: 0x04000ACC RID: 2764
		private const int MaxNamespacesWalkCount = 16;

		// Token: 0x04000ACD RID: 2765
		internal static readonly string[] stateName = new string[]
		{
			"Start", "TopLevel", "Document", "Element Start Tag", "Element Content", "Element Content", "Attribute", "EndRootElement", "Attribute", "Special Attribute",
			"End Document", "Root Level Attribute Value", "Root Level Special Attribute Value", "Root Level Base64 Attribute Value", "After Root Level Attribute", "Closed", "Error"
		};

		// Token: 0x04000ACE RID: 2766
		internal static readonly string[] tokenName = new string[]
		{
			"StartDocument", "EndDocument", "PI", "Comment", "DTD", "StartElement", "EndElement", "StartAttribute", "EndAttribute", "Text",
			"CDATA", "Atomic value", "Base64", "RawData", "Whitespace"
		};

		// Token: 0x04000ACF RID: 2767
		private static WriteState[] state2WriteState = new WriteState[]
		{
			WriteState.Start,
			WriteState.Prolog,
			WriteState.Prolog,
			WriteState.Element,
			WriteState.Content,
			WriteState.Content,
			WriteState.Attribute,
			WriteState.Content,
			WriteState.Attribute,
			WriteState.Attribute,
			WriteState.Content,
			WriteState.Attribute,
			WriteState.Attribute,
			WriteState.Attribute,
			WriteState.Attribute,
			WriteState.Closed,
			WriteState.Error
		};

		// Token: 0x04000AD0 RID: 2768
		private static readonly XmlWellFormedWriter.State[] StateTableDocument = new XmlWellFormedWriter.State[]
		{
			XmlWellFormedWriter.State.Document,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.PostB64Cont,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.PostB64Cont,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.EndDocument,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.StartDoc,
			XmlWellFormedWriter.State.TopLevel,
			XmlWellFormedWriter.State.Document,
			XmlWellFormedWriter.State.StartContent,
			XmlWellFormedWriter.State.Content,
			XmlWellFormedWriter.State.PostB64Cont,
			XmlWellFormedWriter.State.PostB64Attr,
			XmlWellFormedWriter.State.AfterRootEle,
			XmlWellFormedWriter.State.EndAttrSCont,
			XmlWellFormedWriter.State.EndAttrSCont,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.StartDoc,
			XmlWellFormedWriter.State.TopLevel,
			XmlWellFormedWriter.State.Document,
			XmlWellFormedWriter.State.StartContent,
			XmlWellFormedWriter.State.Content,
			XmlWellFormedWriter.State.PostB64Cont,
			XmlWellFormedWriter.State.PostB64Attr,
			XmlWellFormedWriter.State.AfterRootEle,
			XmlWellFormedWriter.State.EndAttrSCont,
			XmlWellFormedWriter.State.EndAttrSCont,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.StartDoc,
			XmlWellFormedWriter.State.TopLevel,
			XmlWellFormedWriter.State.Document,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.PostB64Cont,
			XmlWellFormedWriter.State.PostB64Attr,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.StartDocEle,
			XmlWellFormedWriter.State.Element,
			XmlWellFormedWriter.State.Element,
			XmlWellFormedWriter.State.StartContentEle,
			XmlWellFormedWriter.State.Element,
			XmlWellFormedWriter.State.PostB64Cont,
			XmlWellFormedWriter.State.PostB64Attr,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.EndAttrSEle,
			XmlWellFormedWriter.State.EndAttrSEle,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.StartContent,
			XmlWellFormedWriter.State.Content,
			XmlWellFormedWriter.State.PostB64Cont,
			XmlWellFormedWriter.State.PostB64Attr,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.EndAttrEEle,
			XmlWellFormedWriter.State.EndAttrEEle,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Attribute,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.PostB64Cont,
			XmlWellFormedWriter.State.PostB64Attr,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.EndAttrSAttr,
			XmlWellFormedWriter.State.EndAttrSAttr,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.PostB64Cont,
			XmlWellFormedWriter.State.PostB64Attr,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Element,
			XmlWellFormedWriter.State.Element,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.StartContent,
			XmlWellFormedWriter.State.Content,
			XmlWellFormedWriter.State.PostB64Cont,
			XmlWellFormedWriter.State.PostB64Attr,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Attribute,
			XmlWellFormedWriter.State.SpecialAttr,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.StartContent,
			XmlWellFormedWriter.State.Content,
			XmlWellFormedWriter.State.PostB64Cont,
			XmlWellFormedWriter.State.PostB64Attr,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.EndAttrSCont,
			XmlWellFormedWriter.State.EndAttrSCont,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.StartContent,
			XmlWellFormedWriter.State.Content,
			XmlWellFormedWriter.State.PostB64Cont,
			XmlWellFormedWriter.State.PostB64Attr,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Attribute,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.StartContentB64,
			XmlWellFormedWriter.State.B64Content,
			XmlWellFormedWriter.State.B64Content,
			XmlWellFormedWriter.State.B64Attribute,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.B64Attribute,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.StartDoc,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Document,
			XmlWellFormedWriter.State.StartContent,
			XmlWellFormedWriter.State.Content,
			XmlWellFormedWriter.State.PostB64Cont,
			XmlWellFormedWriter.State.PostB64Attr,
			XmlWellFormedWriter.State.AfterRootEle,
			XmlWellFormedWriter.State.Attribute,
			XmlWellFormedWriter.State.SpecialAttr,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.StartDoc,
			XmlWellFormedWriter.State.TopLevel,
			XmlWellFormedWriter.State.Document,
			XmlWellFormedWriter.State.StartContent,
			XmlWellFormedWriter.State.Content,
			XmlWellFormedWriter.State.PostB64Cont,
			XmlWellFormedWriter.State.PostB64Attr,
			XmlWellFormedWriter.State.AfterRootEle,
			XmlWellFormedWriter.State.Attribute,
			XmlWellFormedWriter.State.SpecialAttr,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error
		};

		// Token: 0x04000AD1 RID: 2769
		private static readonly XmlWellFormedWriter.State[] StateTableAuto = new XmlWellFormedWriter.State[]
		{
			XmlWellFormedWriter.State.Document,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.PostB64Cont,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.PostB64Cont,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.EndDocument,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.TopLevel,
			XmlWellFormedWriter.State.TopLevel,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.StartContent,
			XmlWellFormedWriter.State.Content,
			XmlWellFormedWriter.State.PostB64Cont,
			XmlWellFormedWriter.State.PostB64Attr,
			XmlWellFormedWriter.State.AfterRootEle,
			XmlWellFormedWriter.State.EndAttrSCont,
			XmlWellFormedWriter.State.EndAttrSCont,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.TopLevel,
			XmlWellFormedWriter.State.TopLevel,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.StartContent,
			XmlWellFormedWriter.State.Content,
			XmlWellFormedWriter.State.PostB64Cont,
			XmlWellFormedWriter.State.PostB64Attr,
			XmlWellFormedWriter.State.AfterRootEle,
			XmlWellFormedWriter.State.EndAttrSCont,
			XmlWellFormedWriter.State.EndAttrSCont,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.StartDoc,
			XmlWellFormedWriter.State.TopLevel,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.PostB64Cont,
			XmlWellFormedWriter.State.PostB64Attr,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.StartFragEle,
			XmlWellFormedWriter.State.Element,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.StartContentEle,
			XmlWellFormedWriter.State.Element,
			XmlWellFormedWriter.State.PostB64Cont,
			XmlWellFormedWriter.State.PostB64Attr,
			XmlWellFormedWriter.State.Element,
			XmlWellFormedWriter.State.EndAttrSEle,
			XmlWellFormedWriter.State.EndAttrSEle,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.StartContent,
			XmlWellFormedWriter.State.Content,
			XmlWellFormedWriter.State.PostB64Cont,
			XmlWellFormedWriter.State.PostB64Attr,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.EndAttrEEle,
			XmlWellFormedWriter.State.EndAttrEEle,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.RootLevelAttr,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Attribute,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.PostB64Cont,
			XmlWellFormedWriter.State.PostB64Attr,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.EndAttrSAttr,
			XmlWellFormedWriter.State.EndAttrSAttr,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.StartRootLevelAttr,
			XmlWellFormedWriter.State.StartRootLevelAttr,
			XmlWellFormedWriter.State.PostB64RootAttr,
			XmlWellFormedWriter.State.RootLevelAttr,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.PostB64Cont,
			XmlWellFormedWriter.State.PostB64Attr,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Element,
			XmlWellFormedWriter.State.Element,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.AfterRootLevelAttr,
			XmlWellFormedWriter.State.AfterRootLevelAttr,
			XmlWellFormedWriter.State.PostB64RootAttr,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.StartFragCont,
			XmlWellFormedWriter.State.StartFragCont,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.StartContent,
			XmlWellFormedWriter.State.Content,
			XmlWellFormedWriter.State.PostB64Cont,
			XmlWellFormedWriter.State.PostB64Attr,
			XmlWellFormedWriter.State.Content,
			XmlWellFormedWriter.State.Attribute,
			XmlWellFormedWriter.State.SpecialAttr,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.RootLevelAttr,
			XmlWellFormedWriter.State.RootLevelSpecAttr,
			XmlWellFormedWriter.State.PostB64RootAttr,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.StartFragCont,
			XmlWellFormedWriter.State.StartFragCont,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.StartContent,
			XmlWellFormedWriter.State.Content,
			XmlWellFormedWriter.State.PostB64Cont,
			XmlWellFormedWriter.State.PostB64Attr,
			XmlWellFormedWriter.State.Content,
			XmlWellFormedWriter.State.EndAttrSCont,
			XmlWellFormedWriter.State.EndAttrSCont,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.StartFragCont,
			XmlWellFormedWriter.State.StartFragCont,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.StartContent,
			XmlWellFormedWriter.State.Content,
			XmlWellFormedWriter.State.PostB64Cont,
			XmlWellFormedWriter.State.PostB64Attr,
			XmlWellFormedWriter.State.Content,
			XmlWellFormedWriter.State.Attribute,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.RootLevelAttr,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.PostB64RootAttr,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.StartFragB64,
			XmlWellFormedWriter.State.StartFragB64,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.StartContentB64,
			XmlWellFormedWriter.State.B64Content,
			XmlWellFormedWriter.State.B64Content,
			XmlWellFormedWriter.State.B64Attribute,
			XmlWellFormedWriter.State.B64Content,
			XmlWellFormedWriter.State.B64Attribute,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.RootLevelB64Attr,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.RootLevelB64Attr,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.StartFragCont,
			XmlWellFormedWriter.State.TopLevel,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.StartContent,
			XmlWellFormedWriter.State.Content,
			XmlWellFormedWriter.State.PostB64Cont,
			XmlWellFormedWriter.State.PostB64Attr,
			XmlWellFormedWriter.State.Content,
			XmlWellFormedWriter.State.Attribute,
			XmlWellFormedWriter.State.SpecialAttr,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.RootLevelAttr,
			XmlWellFormedWriter.State.RootLevelSpecAttr,
			XmlWellFormedWriter.State.PostB64RootAttr,
			XmlWellFormedWriter.State.AfterRootLevelAttr,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.TopLevel,
			XmlWellFormedWriter.State.TopLevel,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.StartContent,
			XmlWellFormedWriter.State.Content,
			XmlWellFormedWriter.State.PostB64Cont,
			XmlWellFormedWriter.State.PostB64Attr,
			XmlWellFormedWriter.State.AfterRootEle,
			XmlWellFormedWriter.State.Attribute,
			XmlWellFormedWriter.State.SpecialAttr,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.RootLevelAttr,
			XmlWellFormedWriter.State.RootLevelSpecAttr,
			XmlWellFormedWriter.State.PostB64RootAttr,
			XmlWellFormedWriter.State.AfterRootLevelAttr,
			XmlWellFormedWriter.State.Error
		};

		// Token: 0x020001AE RID: 430
		private enum State
		{
			// Token: 0x04000AD3 RID: 2771
			Start,
			// Token: 0x04000AD4 RID: 2772
			TopLevel,
			// Token: 0x04000AD5 RID: 2773
			Document,
			// Token: 0x04000AD6 RID: 2774
			Element,
			// Token: 0x04000AD7 RID: 2775
			Content,
			// Token: 0x04000AD8 RID: 2776
			B64Content,
			// Token: 0x04000AD9 RID: 2777
			B64Attribute,
			// Token: 0x04000ADA RID: 2778
			AfterRootEle,
			// Token: 0x04000ADB RID: 2779
			Attribute,
			// Token: 0x04000ADC RID: 2780
			SpecialAttr,
			// Token: 0x04000ADD RID: 2781
			EndDocument,
			// Token: 0x04000ADE RID: 2782
			RootLevelAttr,
			// Token: 0x04000ADF RID: 2783
			RootLevelSpecAttr,
			// Token: 0x04000AE0 RID: 2784
			RootLevelB64Attr,
			// Token: 0x04000AE1 RID: 2785
			AfterRootLevelAttr,
			// Token: 0x04000AE2 RID: 2786
			Closed,
			// Token: 0x04000AE3 RID: 2787
			Error,
			// Token: 0x04000AE4 RID: 2788
			StartContent = 101,
			// Token: 0x04000AE5 RID: 2789
			StartContentEle,
			// Token: 0x04000AE6 RID: 2790
			StartContentB64,
			// Token: 0x04000AE7 RID: 2791
			StartDoc,
			// Token: 0x04000AE8 RID: 2792
			StartDocEle = 106,
			// Token: 0x04000AE9 RID: 2793
			EndAttrSEle,
			// Token: 0x04000AEA RID: 2794
			EndAttrEEle,
			// Token: 0x04000AEB RID: 2795
			EndAttrSCont,
			// Token: 0x04000AEC RID: 2796
			EndAttrSAttr = 111,
			// Token: 0x04000AED RID: 2797
			PostB64Cont,
			// Token: 0x04000AEE RID: 2798
			PostB64Attr,
			// Token: 0x04000AEF RID: 2799
			PostB64RootAttr,
			// Token: 0x04000AF0 RID: 2800
			StartFragEle,
			// Token: 0x04000AF1 RID: 2801
			StartFragCont,
			// Token: 0x04000AF2 RID: 2802
			StartFragB64,
			// Token: 0x04000AF3 RID: 2803
			StartRootLevelAttr
		}

		// Token: 0x020001AF RID: 431
		private enum Token
		{
			// Token: 0x04000AF5 RID: 2805
			StartDocument,
			// Token: 0x04000AF6 RID: 2806
			EndDocument,
			// Token: 0x04000AF7 RID: 2807
			PI,
			// Token: 0x04000AF8 RID: 2808
			Comment,
			// Token: 0x04000AF9 RID: 2809
			Dtd,
			// Token: 0x04000AFA RID: 2810
			StartElement,
			// Token: 0x04000AFB RID: 2811
			EndElement,
			// Token: 0x04000AFC RID: 2812
			StartAttribute,
			// Token: 0x04000AFD RID: 2813
			EndAttribute,
			// Token: 0x04000AFE RID: 2814
			Text,
			// Token: 0x04000AFF RID: 2815
			CData,
			// Token: 0x04000B00 RID: 2816
			AtomicValue,
			// Token: 0x04000B01 RID: 2817
			Base64,
			// Token: 0x04000B02 RID: 2818
			RawData,
			// Token: 0x04000B03 RID: 2819
			Whitespace
		}

		// Token: 0x020001B0 RID: 432
		private class NamespaceResolverProxy : IXmlNamespaceResolver
		{
			// Token: 0x06000FE3 RID: 4067 RVA: 0x0005E191 File Offset: 0x0005C391
			internal NamespaceResolverProxy(XmlWellFormedWriter wfWriter)
			{
				this.wfWriter = wfWriter;
			}

			// Token: 0x06000FE4 RID: 4068 RVA: 0x0000A533 File Offset: 0x00008733
			IDictionary<string, string> IXmlNamespaceResolver.GetNamespacesInScope(XmlNamespaceScope scope)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000FE5 RID: 4069 RVA: 0x0005E1A0 File Offset: 0x0005C3A0
			string IXmlNamespaceResolver.LookupNamespace(string prefix)
			{
				return this.wfWriter.LookupNamespace(prefix);
			}

			// Token: 0x06000FE6 RID: 4070 RVA: 0x0005E1AE File Offset: 0x0005C3AE
			string IXmlNamespaceResolver.LookupPrefix(string namespaceName)
			{
				return this.wfWriter.LookupPrefix(namespaceName);
			}

			// Token: 0x04000B04 RID: 2820
			private XmlWellFormedWriter wfWriter;
		}

		// Token: 0x020001B1 RID: 433
		private struct ElementScope
		{
			// Token: 0x06000FE7 RID: 4071 RVA: 0x0005E1BC File Offset: 0x0005C3BC
			internal void Set(string prefix, string localName, string namespaceUri, int prevNSTop)
			{
				this.prevNSTop = prevNSTop;
				this.prefix = prefix;
				this.namespaceUri = namespaceUri;
				this.localName = localName;
				this.xmlSpace = (XmlSpace)(-1);
				this.xmlLang = null;
			}

			// Token: 0x06000FE8 RID: 4072 RVA: 0x0005E1E9 File Offset: 0x0005C3E9
			internal void WriteEndElement(XmlRawWriter rawWriter)
			{
				rawWriter.WriteEndElement(this.prefix, this.localName, this.namespaceUri);
			}

			// Token: 0x06000FE9 RID: 4073 RVA: 0x0005E203 File Offset: 0x0005C403
			internal void WriteFullEndElement(XmlRawWriter rawWriter)
			{
				rawWriter.WriteFullEndElement(this.prefix, this.localName, this.namespaceUri);
			}

			// Token: 0x06000FEA RID: 4074 RVA: 0x0005E21D File Offset: 0x0005C41D
			internal Task WriteEndElementAsync(XmlRawWriter rawWriter)
			{
				return rawWriter.WriteEndElementAsync(this.prefix, this.localName, this.namespaceUri);
			}

			// Token: 0x06000FEB RID: 4075 RVA: 0x0005E237 File Offset: 0x0005C437
			internal Task WriteFullEndElementAsync(XmlRawWriter rawWriter)
			{
				return rawWriter.WriteFullEndElementAsync(this.prefix, this.localName, this.namespaceUri);
			}

			// Token: 0x04000B05 RID: 2821
			internal int prevNSTop;

			// Token: 0x04000B06 RID: 2822
			internal string prefix;

			// Token: 0x04000B07 RID: 2823
			internal string localName;

			// Token: 0x04000B08 RID: 2824
			internal string namespaceUri;

			// Token: 0x04000B09 RID: 2825
			internal XmlSpace xmlSpace;

			// Token: 0x04000B0A RID: 2826
			internal string xmlLang;
		}

		// Token: 0x020001B2 RID: 434
		private enum NamespaceKind
		{
			// Token: 0x04000B0C RID: 2828
			Written,
			// Token: 0x04000B0D RID: 2829
			NeedToWrite,
			// Token: 0x04000B0E RID: 2830
			Implied,
			// Token: 0x04000B0F RID: 2831
			Special
		}

		// Token: 0x020001B3 RID: 435
		private struct Namespace
		{
			// Token: 0x06000FEC RID: 4076 RVA: 0x0005E251 File Offset: 0x0005C451
			internal void Set(string prefix, string namespaceUri, XmlWellFormedWriter.NamespaceKind kind)
			{
				this.prefix = prefix;
				this.namespaceUri = namespaceUri;
				this.kind = kind;
				this.prevNsIndex = -1;
			}

			// Token: 0x06000FED RID: 4077 RVA: 0x0005E270 File Offset: 0x0005C470
			internal void WriteDecl(XmlWriter writer, XmlRawWriter rawWriter)
			{
				if (rawWriter != null)
				{
					rawWriter.WriteNamespaceDeclaration(this.prefix, this.namespaceUri);
					return;
				}
				if (this.prefix.Length == 0)
				{
					writer.WriteStartAttribute(string.Empty, "xmlns", "http://www.w3.org/2000/xmlns/");
				}
				else
				{
					writer.WriteStartAttribute("xmlns", this.prefix, "http://www.w3.org/2000/xmlns/");
				}
				writer.WriteString(this.namespaceUri);
				writer.WriteEndAttribute();
			}

			// Token: 0x06000FEE RID: 4078 RVA: 0x0005E2E0 File Offset: 0x0005C4E0
			internal async Task WriteDeclAsync(XmlWriter writer, XmlRawWriter rawWriter)
			{
				if (rawWriter != null)
				{
					await rawWriter.WriteNamespaceDeclarationAsync(this.prefix, this.namespaceUri).ConfigureAwait(false);
				}
				else
				{
					if (this.prefix.Length == 0)
					{
						await writer.WriteStartAttributeAsync(string.Empty, "xmlns", "http://www.w3.org/2000/xmlns/").ConfigureAwait(false);
					}
					else
					{
						await writer.WriteStartAttributeAsync("xmlns", this.prefix, "http://www.w3.org/2000/xmlns/").ConfigureAwait(false);
					}
					await writer.WriteStringAsync(this.namespaceUri).ConfigureAwait(false);
					await writer.WriteEndAttributeAsync().ConfigureAwait(false);
				}
			}

			// Token: 0x04000B10 RID: 2832
			internal string prefix;

			// Token: 0x04000B11 RID: 2833
			internal string namespaceUri;

			// Token: 0x04000B12 RID: 2834
			internal XmlWellFormedWriter.NamespaceKind kind;

			// Token: 0x04000B13 RID: 2835
			internal int prevNsIndex;
		}

		// Token: 0x020001B5 RID: 437
		private struct AttrName
		{
			// Token: 0x06000FF1 RID: 4081 RVA: 0x0005E636 File Offset: 0x0005C836
			internal void Set(string prefix, string localName, string namespaceUri)
			{
				this.prefix = prefix;
				this.namespaceUri = namespaceUri;
				this.localName = localName;
				this.prev = 0;
			}

			// Token: 0x06000FF2 RID: 4082 RVA: 0x0005E654 File Offset: 0x0005C854
			internal bool IsDuplicate(string prefix, string localName, string namespaceUri)
			{
				return this.localName == localName && (this.prefix == prefix || this.namespaceUri == namespaceUri);
			}

			// Token: 0x04000B1A RID: 2842
			internal string prefix;

			// Token: 0x04000B1B RID: 2843
			internal string namespaceUri;

			// Token: 0x04000B1C RID: 2844
			internal string localName;

			// Token: 0x04000B1D RID: 2845
			internal int prev;
		}

		// Token: 0x020001B6 RID: 438
		private enum SpecialAttribute
		{
			// Token: 0x04000B1F RID: 2847
			No,
			// Token: 0x04000B20 RID: 2848
			DefaultXmlns,
			// Token: 0x04000B21 RID: 2849
			PrefixedXmlns,
			// Token: 0x04000B22 RID: 2850
			XmlSpace,
			// Token: 0x04000B23 RID: 2851
			XmlLang
		}

		// Token: 0x020001B7 RID: 439
		private class AttributeValueCache
		{
			// Token: 0x170002B3 RID: 691
			// (get) Token: 0x06000FF3 RID: 4083 RVA: 0x0005E682 File Offset: 0x0005C882
			internal string StringValue
			{
				get
				{
					if (this.singleStringValue != null)
					{
						return this.singleStringValue;
					}
					return this.stringValue.ToString();
				}
			}

			// Token: 0x06000FF4 RID: 4084 RVA: 0x0005E6A0 File Offset: 0x0005C8A0
			internal void WriteEntityRef(string name)
			{
				if (this.singleStringValue != null)
				{
					this.StartComplexValue();
				}
				if (!(name == "lt"))
				{
					if (!(name == "gt"))
					{
						if (!(name == "quot"))
						{
							if (!(name == "apos"))
							{
								if (!(name == "amp"))
								{
									this.stringValue.Append('&');
									this.stringValue.Append(name);
									this.stringValue.Append(';');
								}
								else
								{
									this.stringValue.Append('&');
								}
							}
							else
							{
								this.stringValue.Append('\'');
							}
						}
						else
						{
							this.stringValue.Append('"');
						}
					}
					else
					{
						this.stringValue.Append('>');
					}
				}
				else
				{
					this.stringValue.Append('<');
				}
				this.AddItem(XmlWellFormedWriter.AttributeValueCache.ItemType.EntityRef, name);
			}

			// Token: 0x06000FF5 RID: 4085 RVA: 0x0005E77F File Offset: 0x0005C97F
			internal void WriteCharEntity(char ch)
			{
				if (this.singleStringValue != null)
				{
					this.StartComplexValue();
				}
				this.stringValue.Append(ch);
				this.AddItem(XmlWellFormedWriter.AttributeValueCache.ItemType.CharEntity, ch);
			}

			// Token: 0x06000FF6 RID: 4086 RVA: 0x0005E7A9 File Offset: 0x0005C9A9
			internal void WriteSurrogateCharEntity(char lowChar, char highChar)
			{
				if (this.singleStringValue != null)
				{
					this.StartComplexValue();
				}
				this.stringValue.Append(highChar);
				this.stringValue.Append(lowChar);
				this.AddItem(XmlWellFormedWriter.AttributeValueCache.ItemType.SurrogateCharEntity, new char[] { lowChar, highChar });
			}

			// Token: 0x06000FF7 RID: 4087 RVA: 0x0005E7E8 File Offset: 0x0005C9E8
			internal void WriteWhitespace(string ws)
			{
				if (this.singleStringValue != null)
				{
					this.StartComplexValue();
				}
				this.stringValue.Append(ws);
				this.AddItem(XmlWellFormedWriter.AttributeValueCache.ItemType.Whitespace, ws);
			}

			// Token: 0x06000FF8 RID: 4088 RVA: 0x0005E80D File Offset: 0x0005CA0D
			internal void WriteString(string text)
			{
				if (this.singleStringValue != null)
				{
					this.StartComplexValue();
				}
				else if (this.lastItem == -1)
				{
					this.singleStringValue = text;
					return;
				}
				this.stringValue.Append(text);
				this.AddItem(XmlWellFormedWriter.AttributeValueCache.ItemType.String, text);
			}

			// Token: 0x06000FF9 RID: 4089 RVA: 0x0005E845 File Offset: 0x0005CA45
			internal void WriteChars(char[] buffer, int index, int count)
			{
				if (this.singleStringValue != null)
				{
					this.StartComplexValue();
				}
				this.stringValue.Append(buffer, index, count);
				this.AddItem(XmlWellFormedWriter.AttributeValueCache.ItemType.StringChars, new XmlWellFormedWriter.AttributeValueCache.BufferChunk(buffer, index, count));
			}

			// Token: 0x06000FFA RID: 4090 RVA: 0x0005E873 File Offset: 0x0005CA73
			internal void WriteRaw(char[] buffer, int index, int count)
			{
				if (this.singleStringValue != null)
				{
					this.StartComplexValue();
				}
				this.stringValue.Append(buffer, index, count);
				this.AddItem(XmlWellFormedWriter.AttributeValueCache.ItemType.RawChars, new XmlWellFormedWriter.AttributeValueCache.BufferChunk(buffer, index, count));
			}

			// Token: 0x06000FFB RID: 4091 RVA: 0x0005E8A1 File Offset: 0x0005CAA1
			internal void WriteRaw(string data)
			{
				if (this.singleStringValue != null)
				{
					this.StartComplexValue();
				}
				this.stringValue.Append(data);
				this.AddItem(XmlWellFormedWriter.AttributeValueCache.ItemType.Raw, data);
			}

			// Token: 0x06000FFC RID: 4092 RVA: 0x0005E8C6 File Offset: 0x0005CAC6
			internal void WriteValue(string value)
			{
				if (this.singleStringValue != null)
				{
					this.StartComplexValue();
				}
				this.stringValue.Append(value);
				this.AddItem(XmlWellFormedWriter.AttributeValueCache.ItemType.ValueString, value);
			}

			// Token: 0x06000FFD RID: 4093 RVA: 0x0005E8EC File Offset: 0x0005CAEC
			internal void Replay(XmlWriter writer)
			{
				if (this.singleStringValue != null)
				{
					writer.WriteString(this.singleStringValue);
					return;
				}
				for (int i = this.firstItem; i <= this.lastItem; i++)
				{
					XmlWellFormedWriter.AttributeValueCache.Item item = this.items[i];
					switch (item.type)
					{
					case XmlWellFormedWriter.AttributeValueCache.ItemType.EntityRef:
						writer.WriteEntityRef((string)item.data);
						break;
					case XmlWellFormedWriter.AttributeValueCache.ItemType.CharEntity:
						writer.WriteCharEntity((char)item.data);
						break;
					case XmlWellFormedWriter.AttributeValueCache.ItemType.SurrogateCharEntity:
					{
						char[] array = (char[])item.data;
						writer.WriteSurrogateCharEntity(array[0], array[1]);
						break;
					}
					case XmlWellFormedWriter.AttributeValueCache.ItemType.Whitespace:
						writer.WriteWhitespace((string)item.data);
						break;
					case XmlWellFormedWriter.AttributeValueCache.ItemType.String:
						writer.WriteString((string)item.data);
						break;
					case XmlWellFormedWriter.AttributeValueCache.ItemType.StringChars:
					{
						XmlWellFormedWriter.AttributeValueCache.BufferChunk bufferChunk = (XmlWellFormedWriter.AttributeValueCache.BufferChunk)item.data;
						writer.WriteChars(bufferChunk.buffer, bufferChunk.index, bufferChunk.count);
						break;
					}
					case XmlWellFormedWriter.AttributeValueCache.ItemType.Raw:
						writer.WriteRaw((string)item.data);
						break;
					case XmlWellFormedWriter.AttributeValueCache.ItemType.RawChars:
					{
						XmlWellFormedWriter.AttributeValueCache.BufferChunk bufferChunk = (XmlWellFormedWriter.AttributeValueCache.BufferChunk)item.data;
						writer.WriteChars(bufferChunk.buffer, bufferChunk.index, bufferChunk.count);
						break;
					}
					case XmlWellFormedWriter.AttributeValueCache.ItemType.ValueString:
						writer.WriteValue((string)item.data);
						break;
					}
				}
			}

			// Token: 0x06000FFE RID: 4094 RVA: 0x0005EA50 File Offset: 0x0005CC50
			internal void Trim()
			{
				if (this.singleStringValue != null)
				{
					this.singleStringValue = XmlConvert.TrimString(this.singleStringValue);
					return;
				}
				string text = this.stringValue.ToString();
				string text2 = XmlConvert.TrimString(text);
				if (text != text2)
				{
					this.stringValue = new StringBuilder(text2);
				}
				XmlCharType instance = XmlCharType.Instance;
				int num = this.firstItem;
				while (num == this.firstItem && num <= this.lastItem)
				{
					XmlWellFormedWriter.AttributeValueCache.Item item = this.items[num];
					switch (item.type)
					{
					case XmlWellFormedWriter.AttributeValueCache.ItemType.Whitespace:
						this.firstItem++;
						break;
					case XmlWellFormedWriter.AttributeValueCache.ItemType.String:
					case XmlWellFormedWriter.AttributeValueCache.ItemType.Raw:
					case XmlWellFormedWriter.AttributeValueCache.ItemType.ValueString:
						item.data = XmlConvert.TrimStringStart((string)item.data);
						if (((string)item.data).Length == 0)
						{
							this.firstItem++;
						}
						break;
					case XmlWellFormedWriter.AttributeValueCache.ItemType.StringChars:
					case XmlWellFormedWriter.AttributeValueCache.ItemType.RawChars:
					{
						XmlWellFormedWriter.AttributeValueCache.BufferChunk bufferChunk = (XmlWellFormedWriter.AttributeValueCache.BufferChunk)item.data;
						int num2 = bufferChunk.index + bufferChunk.count;
						while (bufferChunk.index < num2 && instance.IsWhiteSpace(bufferChunk.buffer[bufferChunk.index]))
						{
							bufferChunk.index++;
							bufferChunk.count--;
						}
						if (bufferChunk.index == num2)
						{
							this.firstItem++;
						}
						break;
					}
					}
					num++;
				}
				num = this.lastItem;
				while (num == this.lastItem && num >= this.firstItem)
				{
					XmlWellFormedWriter.AttributeValueCache.Item item2 = this.items[num];
					switch (item2.type)
					{
					case XmlWellFormedWriter.AttributeValueCache.ItemType.Whitespace:
						this.lastItem--;
						break;
					case XmlWellFormedWriter.AttributeValueCache.ItemType.String:
					case XmlWellFormedWriter.AttributeValueCache.ItemType.Raw:
					case XmlWellFormedWriter.AttributeValueCache.ItemType.ValueString:
						item2.data = XmlConvert.TrimStringEnd((string)item2.data);
						if (((string)item2.data).Length == 0)
						{
							this.lastItem--;
						}
						break;
					case XmlWellFormedWriter.AttributeValueCache.ItemType.StringChars:
					case XmlWellFormedWriter.AttributeValueCache.ItemType.RawChars:
					{
						XmlWellFormedWriter.AttributeValueCache.BufferChunk bufferChunk2 = (XmlWellFormedWriter.AttributeValueCache.BufferChunk)item2.data;
						while (bufferChunk2.count > 0 && instance.IsWhiteSpace(bufferChunk2.buffer[bufferChunk2.index + bufferChunk2.count - 1]))
						{
							bufferChunk2.count--;
						}
						if (bufferChunk2.count == 0)
						{
							this.lastItem--;
						}
						break;
					}
					}
					num--;
				}
			}

			// Token: 0x06000FFF RID: 4095 RVA: 0x0005ECD5 File Offset: 0x0005CED5
			internal void Clear()
			{
				this.singleStringValue = null;
				this.lastItem = -1;
				this.firstItem = 0;
				this.stringValue.Length = 0;
			}

			// Token: 0x06001000 RID: 4096 RVA: 0x0005ECF8 File Offset: 0x0005CEF8
			private void StartComplexValue()
			{
				this.stringValue.Append(this.singleStringValue);
				this.AddItem(XmlWellFormedWriter.AttributeValueCache.ItemType.String, this.singleStringValue);
				this.singleStringValue = null;
			}

			// Token: 0x06001001 RID: 4097 RVA: 0x0005ED20 File Offset: 0x0005CF20
			private void AddItem(XmlWellFormedWriter.AttributeValueCache.ItemType type, object data)
			{
				int num = this.lastItem + 1;
				if (this.items == null)
				{
					this.items = new XmlWellFormedWriter.AttributeValueCache.Item[4];
				}
				else if (this.items.Length == num)
				{
					XmlWellFormedWriter.AttributeValueCache.Item[] array = new XmlWellFormedWriter.AttributeValueCache.Item[num * 2];
					Array.Copy(this.items, array, num);
					this.items = array;
				}
				if (this.items[num] == null)
				{
					this.items[num] = new XmlWellFormedWriter.AttributeValueCache.Item();
				}
				this.items[num].Set(type, data);
				this.lastItem = num;
			}

			// Token: 0x06001002 RID: 4098 RVA: 0x0005EDA4 File Offset: 0x0005CFA4
			internal async Task ReplayAsync(XmlWriter writer)
			{
				if (this.singleStringValue != null)
				{
					await writer.WriteStringAsync(this.singleStringValue).ConfigureAwait(false);
				}
				else
				{
					for (int i = this.firstItem; i <= this.lastItem; i++)
					{
						XmlWellFormedWriter.AttributeValueCache.Item item = this.items[i];
						switch (item.type)
						{
						case XmlWellFormedWriter.AttributeValueCache.ItemType.EntityRef:
							await writer.WriteEntityRefAsync((string)item.data).ConfigureAwait(false);
							break;
						case XmlWellFormedWriter.AttributeValueCache.ItemType.CharEntity:
							await writer.WriteCharEntityAsync((char)item.data).ConfigureAwait(false);
							break;
						case XmlWellFormedWriter.AttributeValueCache.ItemType.SurrogateCharEntity:
						{
							char[] array = (char[])item.data;
							await writer.WriteSurrogateCharEntityAsync(array[0], array[1]).ConfigureAwait(false);
							break;
						}
						case XmlWellFormedWriter.AttributeValueCache.ItemType.Whitespace:
							await writer.WriteWhitespaceAsync((string)item.data).ConfigureAwait(false);
							break;
						case XmlWellFormedWriter.AttributeValueCache.ItemType.String:
							await writer.WriteStringAsync((string)item.data).ConfigureAwait(false);
							break;
						case XmlWellFormedWriter.AttributeValueCache.ItemType.StringChars:
						{
							XmlWellFormedWriter.AttributeValueCache.BufferChunk bufferChunk = (XmlWellFormedWriter.AttributeValueCache.BufferChunk)item.data;
							await writer.WriteCharsAsync(bufferChunk.buffer, bufferChunk.index, bufferChunk.count).ConfigureAwait(false);
							break;
						}
						case XmlWellFormedWriter.AttributeValueCache.ItemType.Raw:
							await writer.WriteRawAsync((string)item.data).ConfigureAwait(false);
							break;
						case XmlWellFormedWriter.AttributeValueCache.ItemType.RawChars:
						{
							XmlWellFormedWriter.AttributeValueCache.BufferChunk bufferChunk = (XmlWellFormedWriter.AttributeValueCache.BufferChunk)item.data;
							await writer.WriteCharsAsync(bufferChunk.buffer, bufferChunk.index, bufferChunk.count).ConfigureAwait(false);
							break;
						}
						case XmlWellFormedWriter.AttributeValueCache.ItemType.ValueString:
							await writer.WriteStringAsync((string)item.data).ConfigureAwait(false);
							break;
						}
						item = null;
					}
				}
			}

			// Token: 0x04000B24 RID: 2852
			private StringBuilder stringValue = new StringBuilder();

			// Token: 0x04000B25 RID: 2853
			private string singleStringValue;

			// Token: 0x04000B26 RID: 2854
			private XmlWellFormedWriter.AttributeValueCache.Item[] items;

			// Token: 0x04000B27 RID: 2855
			private int firstItem;

			// Token: 0x04000B28 RID: 2856
			private int lastItem = -1;

			// Token: 0x020001B8 RID: 440
			private enum ItemType
			{
				// Token: 0x04000B2A RID: 2858
				EntityRef,
				// Token: 0x04000B2B RID: 2859
				CharEntity,
				// Token: 0x04000B2C RID: 2860
				SurrogateCharEntity,
				// Token: 0x04000B2D RID: 2861
				Whitespace,
				// Token: 0x04000B2E RID: 2862
				String,
				// Token: 0x04000B2F RID: 2863
				StringChars,
				// Token: 0x04000B30 RID: 2864
				Raw,
				// Token: 0x04000B31 RID: 2865
				RawChars,
				// Token: 0x04000B32 RID: 2866
				ValueString
			}

			// Token: 0x020001B9 RID: 441
			private class Item
			{
				// Token: 0x06001004 RID: 4100 RVA: 0x000020FD File Offset: 0x000002FD
				internal Item()
				{
				}

				// Token: 0x06001005 RID: 4101 RVA: 0x0005EE0B File Offset: 0x0005D00B
				internal void Set(XmlWellFormedWriter.AttributeValueCache.ItemType type, object data)
				{
					this.type = type;
					this.data = data;
				}

				// Token: 0x04000B33 RID: 2867
				internal XmlWellFormedWriter.AttributeValueCache.ItemType type;

				// Token: 0x04000B34 RID: 2868
				internal object data;
			}

			// Token: 0x020001BA RID: 442
			private class BufferChunk
			{
				// Token: 0x06001006 RID: 4102 RVA: 0x0005EE1B File Offset: 0x0005D01B
				internal BufferChunk(char[] buffer, int index, int count)
				{
					this.buffer = buffer;
					this.index = index;
					this.count = count;
				}

				// Token: 0x04000B35 RID: 2869
				internal char[] buffer;

				// Token: 0x04000B36 RID: 2870
				internal int index;

				// Token: 0x04000B37 RID: 2871
				internal int count;
			}
		}
	}
}
