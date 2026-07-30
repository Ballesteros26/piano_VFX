using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Xml.XPath;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x0200060E RID: 1550
	[EditorBrowsable(EditorBrowsableState.Never)]
	public sealed class XmlQueryOutput : XmlWriter
	{
		// Token: 0x06003C39 RID: 15417 RVA: 0x0015059E File Offset: 0x0014E79E
		internal XmlQueryOutput(XmlQueryRuntime runtime, XmlSequenceWriter seqwrt)
		{
			this.runtime = runtime;
			this.seqwrt = seqwrt;
			this.xstate = XmlState.WithinSequence;
		}

		// Token: 0x06003C3A RID: 15418 RVA: 0x001505C6 File Offset: 0x0014E7C6
		internal XmlQueryOutput(XmlQueryRuntime runtime, XmlEventCache xwrt)
		{
			this.runtime = runtime;
			this.xwrt = xwrt;
			this.xstate = XmlState.WithinContent;
			this.depth = 1;
			this.rootType = XPathNodeType.Root;
		}

		// Token: 0x17000C49 RID: 3145
		// (get) Token: 0x06003C3B RID: 15419 RVA: 0x001505FC File Offset: 0x0014E7FC
		internal XmlSequenceWriter SequenceWriter
		{
			get
			{
				return this.seqwrt;
			}
		}

		// Token: 0x17000C4A RID: 3146
		// (get) Token: 0x06003C3C RID: 15420 RVA: 0x00150604 File Offset: 0x0014E804
		// (set) Token: 0x06003C3D RID: 15421 RVA: 0x0015060C File Offset: 0x0014E80C
		internal XmlRawWriter Writer
		{
			get
			{
				return this.xwrt;
			}
			set
			{
				IRemovableWriter removableWriter = value as IRemovableWriter;
				if (removableWriter != null)
				{
					removableWriter.OnRemoveWriterEvent = new OnRemoveWriter(this.SetWrappedWriter);
				}
				this.xwrt = value;
			}
		}

		// Token: 0x06003C3E RID: 15422 RVA: 0x0015063C File Offset: 0x0014E83C
		private void SetWrappedWriter(XmlRawWriter writer)
		{
			if (this.Writer is XmlAttributeCache)
			{
				this.attrCache = (XmlAttributeCache)this.Writer;
			}
			this.Writer = writer;
		}

		// Token: 0x06003C3F RID: 15423 RVA: 0x00010C4A File Offset: 0x0000EE4A
		public override void WriteStartDocument()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06003C40 RID: 15424 RVA: 0x00010C4A File Offset: 0x0000EE4A
		public override void WriteStartDocument(bool standalone)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06003C41 RID: 15425 RVA: 0x00010C4A File Offset: 0x0000EE4A
		public override void WriteEndDocument()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06003C42 RID: 15426 RVA: 0x00010C4A File Offset: 0x0000EE4A
		public override void WriteDocType(string name, string pubid, string sysid, string subset)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06003C43 RID: 15427 RVA: 0x00150664 File Offset: 0x0014E864
		public override void WriteStartElement(string prefix, string localName, string ns)
		{
			this.ConstructWithinContent(XPathNodeType.Element);
			this.WriteStartElementUnchecked(prefix, localName, ns);
			this.WriteNamespaceDeclarationUnchecked(prefix, ns);
			if (this.attrCache == null)
			{
				this.attrCache = new XmlAttributeCache();
			}
			this.attrCache.Init(this.Writer);
			this.Writer = this.attrCache;
			this.attrCache = null;
			this.PushElementNames(prefix, localName, ns);
		}

		// Token: 0x06003C44 RID: 15428 RVA: 0x001506CC File Offset: 0x0014E8CC
		public override void WriteEndElement()
		{
			if (this.xstate == XmlState.EnumAttrs)
			{
				this.StartElementContentUnchecked();
			}
			string text;
			string text2;
			string text3;
			this.PopElementNames(out text, out text2, out text3);
			this.WriteEndElementUnchecked(text, text2, text3);
			if (this.depth == 0)
			{
				this.EndTree();
			}
		}

		// Token: 0x06003C45 RID: 15429 RVA: 0x00070FD3 File Offset: 0x0006F1D3
		public override void WriteFullEndElement()
		{
			this.WriteEndElement();
		}

		// Token: 0x06003C46 RID: 15430 RVA: 0x0015070C File Offset: 0x0014E90C
		public override void WriteStartAttribute(string prefix, string localName, string ns)
		{
			if (prefix.Length == 5 && prefix == "xmlns")
			{
				this.WriteStartNamespace(localName);
				return;
			}
			this.ConstructInEnumAttrs(XPathNodeType.Attribute);
			if (ns.Length != 0 && this.depth != 0)
			{
				prefix = this.CheckAttributePrefix(prefix, ns);
			}
			this.WriteStartAttributeUnchecked(prefix, localName, ns);
		}

		// Token: 0x06003C47 RID: 15431 RVA: 0x00150761 File Offset: 0x0014E961
		public override void WriteEndAttribute()
		{
			if (this.xstate == XmlState.WithinNmsp)
			{
				this.WriteEndNamespace();
				return;
			}
			this.WriteEndAttributeUnchecked();
			if (this.depth == 0)
			{
				this.EndTree();
			}
		}

		// Token: 0x06003C48 RID: 15432 RVA: 0x00150787 File Offset: 0x0014E987
		public override void WriteComment(string text)
		{
			this.WriteStartComment();
			this.WriteCommentString(text);
			this.WriteEndComment();
		}

		// Token: 0x06003C49 RID: 15433 RVA: 0x0015079C File Offset: 0x0014E99C
		public override void WriteProcessingInstruction(string target, string text)
		{
			this.WriteStartProcessingInstruction(target);
			this.WriteProcessingInstructionString(text);
			this.WriteEndProcessingInstruction();
		}

		// Token: 0x06003C4A RID: 15434 RVA: 0x00010C4A File Offset: 0x0000EE4A
		public override void WriteEntityRef(string name)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06003C4B RID: 15435 RVA: 0x00010C4A File Offset: 0x0000EE4A
		public override void WriteCharEntity(char ch)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06003C4C RID: 15436 RVA: 0x00010C4A File Offset: 0x0000EE4A
		public override void WriteSurrogateCharEntity(char lowChar, char highChar)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06003C4D RID: 15437 RVA: 0x00010C4A File Offset: 0x0000EE4A
		public override void WriteWhitespace(string ws)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06003C4E RID: 15438 RVA: 0x001507B2 File Offset: 0x0014E9B2
		public override void WriteString(string text)
		{
			this.WriteString(text, false);
		}

		// Token: 0x06003C4F RID: 15439 RVA: 0x00010C4A File Offset: 0x0000EE4A
		public override void WriteChars(char[] buffer, int index, int count)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06003C50 RID: 15440 RVA: 0x00010C4A File Offset: 0x0000EE4A
		public override void WriteRaw(char[] buffer, int index, int count)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06003C51 RID: 15441 RVA: 0x001507BC File Offset: 0x0014E9BC
		public override void WriteRaw(string data)
		{
			this.WriteString(data, true);
		}

		// Token: 0x06003C52 RID: 15442 RVA: 0x001507B2 File Offset: 0x0014E9B2
		public override void WriteCData(string text)
		{
			this.WriteString(text, false);
		}

		// Token: 0x06003C53 RID: 15443 RVA: 0x00010C4A File Offset: 0x0000EE4A
		public override void WriteBase64(byte[] buffer, int index, int count)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000C4B RID: 3147
		// (get) Token: 0x06003C54 RID: 15444 RVA: 0x00010C4A File Offset: 0x0000EE4A
		public override WriteState WriteState
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06003C55 RID: 15445 RVA: 0x00002F50 File Offset: 0x00001150
		public override void Close()
		{
		}

		// Token: 0x06003C56 RID: 15446 RVA: 0x00002F50 File Offset: 0x00001150
		public override void Flush()
		{
		}

		// Token: 0x06003C57 RID: 15447 RVA: 0x00010C4A File Offset: 0x0000EE4A
		public override string LookupPrefix(string ns)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000C4C RID: 3148
		// (get) Token: 0x06003C58 RID: 15448 RVA: 0x00010C4A File Offset: 0x0000EE4A
		public override XmlSpace XmlSpace
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000C4D RID: 3149
		// (get) Token: 0x06003C59 RID: 15449 RVA: 0x00010C4A File Offset: 0x0000EE4A
		public override string XmlLang
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06003C5A RID: 15450 RVA: 0x001507C6 File Offset: 0x0014E9C6
		public void StartTree(XPathNodeType rootType)
		{
			this.Writer = this.seqwrt.StartTree(rootType, this.nsmgr, this.runtime.NameTable);
			this.rootType = rootType;
			this.xstate = ((rootType == XPathNodeType.Attribute || rootType == XPathNodeType.Namespace) ? XmlState.EnumAttrs : XmlState.WithinContent);
		}

		// Token: 0x06003C5B RID: 15451 RVA: 0x00150804 File Offset: 0x0014EA04
		public void EndTree()
		{
			this.seqwrt.EndTree();
			this.xstate = XmlState.WithinSequence;
			this.Writer = null;
		}

		// Token: 0x06003C5C RID: 15452 RVA: 0x00150820 File Offset: 0x0014EA20
		public void WriteStartElementUnchecked(string prefix, string localName, string ns)
		{
			if (this.nsmgr != null)
			{
				this.nsmgr.PushScope();
			}
			this.Writer.WriteStartElement(prefix, localName, ns);
			this.usedPrefixes.Clear();
			this.usedPrefixes[prefix] = ns;
			this.xstate = XmlState.EnumAttrs;
			this.depth++;
		}

		// Token: 0x06003C5D RID: 15453 RVA: 0x0015087B File Offset: 0x0014EA7B
		public void WriteStartElementUnchecked(string localName)
		{
			this.WriteStartElementUnchecked(string.Empty, localName, string.Empty);
		}

		// Token: 0x06003C5E RID: 15454 RVA: 0x0015088E File Offset: 0x0014EA8E
		public void StartElementContentUnchecked()
		{
			if (this.cntNmsp != 0)
			{
				this.WriteCachedNamespaces();
			}
			this.Writer.StartElementContent();
			this.xstate = XmlState.WithinContent;
		}

		// Token: 0x06003C5F RID: 15455 RVA: 0x001508B0 File Offset: 0x0014EAB0
		public void WriteEndElementUnchecked(string prefix, string localName, string ns)
		{
			this.Writer.WriteEndElement(prefix, localName, ns);
			this.xstate = XmlState.WithinContent;
			this.depth--;
			if (this.nsmgr != null)
			{
				this.nsmgr.PopScope();
			}
		}

		// Token: 0x06003C60 RID: 15456 RVA: 0x001508E9 File Offset: 0x0014EAE9
		public void WriteEndElementUnchecked(string localName)
		{
			this.WriteEndElementUnchecked(string.Empty, localName, string.Empty);
		}

		// Token: 0x06003C61 RID: 15457 RVA: 0x001508FC File Offset: 0x0014EAFC
		public void WriteStartAttributeUnchecked(string prefix, string localName, string ns)
		{
			this.Writer.WriteStartAttribute(prefix, localName, ns);
			this.xstate = XmlState.WithinAttr;
			this.depth++;
		}

		// Token: 0x06003C62 RID: 15458 RVA: 0x00150921 File Offset: 0x0014EB21
		public void WriteStartAttributeUnchecked(string localName)
		{
			this.WriteStartAttributeUnchecked(string.Empty, localName, string.Empty);
		}

		// Token: 0x06003C63 RID: 15459 RVA: 0x00150934 File Offset: 0x0014EB34
		public void WriteEndAttributeUnchecked()
		{
			this.Writer.WriteEndAttribute();
			this.xstate = XmlState.EnumAttrs;
			this.depth--;
		}

		// Token: 0x06003C64 RID: 15460 RVA: 0x00150958 File Offset: 0x0014EB58
		public void WriteNamespaceDeclarationUnchecked(string prefix, string ns)
		{
			if (this.depth == 0)
			{
				this.Writer.WriteNamespaceDeclaration(prefix, ns);
				return;
			}
			if (this.nsmgr == null)
			{
				if (ns.Length == 0 && prefix.Length == 0)
				{
					return;
				}
				this.nsmgr = new XmlNamespaceManager(this.runtime.NameTable);
				this.nsmgr.PushScope();
			}
			if (this.nsmgr.LookupNamespace(prefix) != ns)
			{
				this.AddNamespace(prefix, ns);
			}
			this.usedPrefixes[prefix] = ns;
		}

		// Token: 0x06003C65 RID: 15461 RVA: 0x001509DE File Offset: 0x0014EBDE
		public void WriteStringUnchecked(string text)
		{
			this.Writer.WriteString(text);
		}

		// Token: 0x06003C66 RID: 15462 RVA: 0x001509EC File Offset: 0x0014EBEC
		public void WriteRawUnchecked(string text)
		{
			this.Writer.WriteRaw(text);
		}

		// Token: 0x06003C67 RID: 15463 RVA: 0x001509FA File Offset: 0x0014EBFA
		public void WriteStartRoot()
		{
			if (this.xstate != XmlState.WithinSequence)
			{
				this.ThrowInvalidStateError(XPathNodeType.Root);
			}
			this.StartTree(XPathNodeType.Root);
			this.depth++;
		}

		// Token: 0x06003C68 RID: 15464 RVA: 0x00150A20 File Offset: 0x0014EC20
		public void WriteEndRoot()
		{
			this.depth--;
			this.EndTree();
		}

		// Token: 0x06003C69 RID: 15465 RVA: 0x00150A36 File Offset: 0x0014EC36
		public void WriteStartElementLocalName(string localName)
		{
			this.WriteStartElement(string.Empty, localName, string.Empty);
		}

		// Token: 0x06003C6A RID: 15466 RVA: 0x00150A49 File Offset: 0x0014EC49
		public void WriteStartAttributeLocalName(string localName)
		{
			this.WriteStartAttribute(string.Empty, localName, string.Empty);
		}

		// Token: 0x06003C6B RID: 15467 RVA: 0x00150A5C File Offset: 0x0014EC5C
		public void WriteStartElementComputed(string tagName, int prefixMappingsIndex)
		{
			this.WriteStartComputed(XPathNodeType.Element, tagName, prefixMappingsIndex);
		}

		// Token: 0x06003C6C RID: 15468 RVA: 0x00150A67 File Offset: 0x0014EC67
		public void WriteStartElementComputed(string tagName, string ns)
		{
			this.WriteStartComputed(XPathNodeType.Element, tagName, ns);
		}

		// Token: 0x06003C6D RID: 15469 RVA: 0x00150A72 File Offset: 0x0014EC72
		public void WriteStartElementComputed(XPathNavigator navigator)
		{
			this.WriteStartComputed(XPathNodeType.Element, navigator);
		}

		// Token: 0x06003C6E RID: 15470 RVA: 0x00150A7C File Offset: 0x0014EC7C
		public void WriteStartElementComputed(XmlQualifiedName name)
		{
			this.WriteStartComputed(XPathNodeType.Element, name);
		}

		// Token: 0x06003C6F RID: 15471 RVA: 0x00150A86 File Offset: 0x0014EC86
		public void WriteStartAttributeComputed(string tagName, int prefixMappingsIndex)
		{
			this.WriteStartComputed(XPathNodeType.Attribute, tagName, prefixMappingsIndex);
		}

		// Token: 0x06003C70 RID: 15472 RVA: 0x00150A91 File Offset: 0x0014EC91
		public void WriteStartAttributeComputed(string tagName, string ns)
		{
			this.WriteStartComputed(XPathNodeType.Attribute, tagName, ns);
		}

		// Token: 0x06003C71 RID: 15473 RVA: 0x00150A9C File Offset: 0x0014EC9C
		public void WriteStartAttributeComputed(XPathNavigator navigator)
		{
			this.WriteStartComputed(XPathNodeType.Attribute, navigator);
		}

		// Token: 0x06003C72 RID: 15474 RVA: 0x00150AA6 File Offset: 0x0014ECA6
		public void WriteStartAttributeComputed(XmlQualifiedName name)
		{
			this.WriteStartComputed(XPathNodeType.Attribute, name);
		}

		// Token: 0x06003C73 RID: 15475 RVA: 0x00150AB0 File Offset: 0x0014ECB0
		public void WriteNamespaceDeclaration(string prefix, string ns)
		{
			this.ConstructInEnumAttrs(XPathNodeType.Namespace);
			if (this.nsmgr == null)
			{
				this.WriteNamespaceDeclarationUnchecked(prefix, ns);
			}
			else
			{
				string text = this.nsmgr.LookupNamespace(prefix);
				if (ns != text)
				{
					if (text != null && this.usedPrefixes.ContainsKey(prefix))
					{
						throw new XslTransformException("Cannot construct namespace declaration xmlns{0}{1}='{2}'. Prefix '{1}' is already mapped to namespace '{3}'.", new string[]
						{
							(prefix.Length == 0) ? "" : ":",
							prefix,
							ns,
							text
						});
					}
					this.AddNamespace(prefix, ns);
				}
			}
			if (this.depth == 0)
			{
				this.EndTree();
			}
			this.usedPrefixes[prefix] = ns;
		}

		// Token: 0x06003C74 RID: 15476 RVA: 0x00150B54 File Offset: 0x0014ED54
		public void WriteStartNamespace(string prefix)
		{
			this.ConstructInEnumAttrs(XPathNodeType.Namespace);
			this.piTarget = prefix;
			this.nodeText.Clear();
			this.xstate = XmlState.WithinNmsp;
			this.depth++;
		}

		// Token: 0x06003C75 RID: 15477 RVA: 0x00150B84 File Offset: 0x0014ED84
		public void WriteNamespaceString(string text)
		{
			this.nodeText.ConcatNoDelimiter(text);
		}

		// Token: 0x06003C76 RID: 15478 RVA: 0x00150B92 File Offset: 0x0014ED92
		public void WriteEndNamespace()
		{
			this.xstate = XmlState.EnumAttrs;
			this.depth--;
			this.WriteNamespaceDeclaration(this.piTarget, this.nodeText.GetResult());
			if (this.depth == 0)
			{
				this.EndTree();
			}
		}

		// Token: 0x06003C77 RID: 15479 RVA: 0x00150BCE File Offset: 0x0014EDCE
		public void WriteStartComment()
		{
			this.ConstructWithinContent(XPathNodeType.Comment);
			this.nodeText.Clear();
			this.xstate = XmlState.WithinComment;
			this.depth++;
		}

		// Token: 0x06003C78 RID: 15480 RVA: 0x00150B84 File Offset: 0x0014ED84
		public void WriteCommentString(string text)
		{
			this.nodeText.ConcatNoDelimiter(text);
		}

		// Token: 0x06003C79 RID: 15481 RVA: 0x00150BF7 File Offset: 0x0014EDF7
		public void WriteEndComment()
		{
			this.Writer.WriteComment(this.nodeText.GetResult());
			this.xstate = XmlState.WithinContent;
			this.depth--;
			if (this.depth == 0)
			{
				this.EndTree();
			}
		}

		// Token: 0x06003C7A RID: 15482 RVA: 0x00150C34 File Offset: 0x0014EE34
		public void WriteStartProcessingInstruction(string target)
		{
			this.ConstructWithinContent(XPathNodeType.ProcessingInstruction);
			ValidateNames.ValidateNameThrow("", target, "", XPathNodeType.ProcessingInstruction, ValidateNames.Flags.AllExceptPrefixMapping);
			this.piTarget = target;
			this.nodeText.Clear();
			this.xstate = XmlState.WithinPI;
			this.depth++;
		}

		// Token: 0x06003C7B RID: 15483 RVA: 0x00150B84 File Offset: 0x0014ED84
		public void WriteProcessingInstructionString(string text)
		{
			this.nodeText.ConcatNoDelimiter(text);
		}

		// Token: 0x06003C7C RID: 15484 RVA: 0x00150C84 File Offset: 0x0014EE84
		public void WriteEndProcessingInstruction()
		{
			this.Writer.WriteProcessingInstruction(this.piTarget, this.nodeText.GetResult());
			this.xstate = XmlState.WithinContent;
			this.depth--;
			if (this.depth == 0)
			{
				this.EndTree();
			}
		}

		// Token: 0x06003C7D RID: 15485 RVA: 0x00150CD0 File Offset: 0x0014EED0
		public void WriteItem(XPathItem item)
		{
			if (!item.IsNode)
			{
				this.seqwrt.WriteItem(item);
				return;
			}
			XPathNavigator xpathNavigator = (XPathNavigator)item;
			if (this.xstate == XmlState.WithinSequence)
			{
				this.seqwrt.WriteItem(xpathNavigator);
				return;
			}
			this.CopyNode(xpathNavigator);
		}

		// Token: 0x06003C7E RID: 15486 RVA: 0x00150D18 File Offset: 0x0014EF18
		public void XsltCopyOf(XPathNavigator navigator)
		{
			RtfNavigator rtfNavigator = navigator as RtfNavigator;
			if (rtfNavigator != null)
			{
				rtfNavigator.CopyToWriter(this);
				return;
			}
			if (navigator.NodeType == XPathNodeType.Root)
			{
				if (navigator.MoveToFirstChild())
				{
					do
					{
						this.CopyNode(navigator);
					}
					while (navigator.MoveToNext());
					navigator.MoveToParent();
					return;
				}
			}
			else
			{
				this.CopyNode(navigator);
			}
		}

		// Token: 0x06003C7F RID: 15487 RVA: 0x00150D65 File Offset: 0x0014EF65
		public bool StartCopy(XPathNavigator navigator)
		{
			if (navigator.NodeType == XPathNodeType.Root)
			{
				return true;
			}
			if (this.StartCopy(navigator, true))
			{
				this.CopyNamespaces(navigator, XPathNamespaceScope.ExcludeXml);
				return true;
			}
			return false;
		}

		// Token: 0x06003C80 RID: 15488 RVA: 0x00150D86 File Offset: 0x0014EF86
		public void EndCopy(XPathNavigator navigator)
		{
			if (navigator.NodeType == XPathNodeType.Element)
			{
				this.WriteEndElement();
			}
		}

		// Token: 0x06003C81 RID: 15489 RVA: 0x00150D97 File Offset: 0x0014EF97
		private void AddNamespace(string prefix, string ns)
		{
			this.nsmgr.AddNamespace(prefix, ns);
			this.cntNmsp++;
			this.usedPrefixes[prefix] = ns;
		}

		// Token: 0x06003C82 RID: 15490 RVA: 0x00150DC4 File Offset: 0x0014EFC4
		private void WriteString(string text, bool disableOutputEscaping)
		{
			switch (this.xstate)
			{
			case XmlState.WithinSequence:
				this.StartTree(XPathNodeType.Text);
				break;
			case XmlState.EnumAttrs:
				this.StartElementContentUnchecked();
				break;
			case XmlState.WithinContent:
				break;
			case XmlState.WithinAttr:
				this.WriteStringUnchecked(text);
				goto IL_0071;
			case XmlState.WithinNmsp:
				this.WriteNamespaceString(text);
				goto IL_0071;
			case XmlState.WithinComment:
				this.WriteCommentString(text);
				goto IL_0071;
			case XmlState.WithinPI:
				this.WriteProcessingInstructionString(text);
				goto IL_0071;
			default:
				goto IL_0071;
			}
			if (disableOutputEscaping)
			{
				this.WriteRawUnchecked(text);
			}
			else
			{
				this.WriteStringUnchecked(text);
			}
			IL_0071:
			if (this.depth == 0)
			{
				this.EndTree();
			}
		}

		// Token: 0x06003C83 RID: 15491 RVA: 0x00150E50 File Offset: 0x0014F050
		private void CopyNode(XPathNavigator navigator)
		{
			int num = this.depth;
			for (;;)
			{
				IL_0007:
				if (this.StartCopy(navigator, this.depth == num))
				{
					XPathNodeType nodeType = navigator.NodeType;
					if (navigator.MoveToFirstAttribute())
					{
						do
						{
							this.StartCopy(navigator, false);
						}
						while (navigator.MoveToNextAttribute());
						navigator.MoveToParent();
					}
					this.CopyNamespaces(navigator, (this.depth - 1 == num) ? XPathNamespaceScope.ExcludeXml : XPathNamespaceScope.Local);
					this.StartElementContentUnchecked();
					if (navigator.MoveToFirstChild())
					{
						continue;
					}
					this.EndCopy(navigator, this.depth - 1 == num);
				}
				while (this.depth != num)
				{
					if (navigator.MoveToNext())
					{
						goto IL_0007;
					}
					navigator.MoveToParent();
					this.EndCopy(navigator, this.depth - 1 == num);
				}
				break;
			}
		}

		// Token: 0x06003C84 RID: 15492 RVA: 0x00150F04 File Offset: 0x0014F104
		private bool StartCopy(XPathNavigator navigator, bool callChk)
		{
			bool flag = false;
			switch (navigator.NodeType)
			{
			case XPathNodeType.Root:
				this.ThrowInvalidStateError(XPathNodeType.Root);
				break;
			case XPathNodeType.Element:
				if (callChk)
				{
					this.WriteStartElement(navigator.Prefix, navigator.LocalName, navigator.NamespaceURI);
				}
				else
				{
					this.WriteStartElementUnchecked(navigator.Prefix, navigator.LocalName, navigator.NamespaceURI);
				}
				flag = true;
				break;
			case XPathNodeType.Attribute:
				if (callChk)
				{
					this.WriteStartAttribute(navigator.Prefix, navigator.LocalName, navigator.NamespaceURI);
				}
				else
				{
					this.WriteStartAttributeUnchecked(navigator.Prefix, navigator.LocalName, navigator.NamespaceURI);
				}
				this.WriteString(navigator.Value);
				if (callChk)
				{
					this.WriteEndAttribute();
				}
				else
				{
					this.WriteEndAttributeUnchecked();
				}
				break;
			case XPathNodeType.Namespace:
				if (callChk)
				{
					XmlAttributeCache xmlAttributeCache = this.Writer as XmlAttributeCache;
					if (xmlAttributeCache != null && xmlAttributeCache.Count != 0)
					{
						throw new XslTransformException("Namespace nodes cannot be added to the parent element after an attribute node has already been added.", new string[] { string.Empty });
					}
					this.WriteNamespaceDeclaration(navigator.LocalName, navigator.Value);
				}
				else
				{
					this.WriteNamespaceDeclarationUnchecked(navigator.LocalName, navigator.Value);
				}
				break;
			case XPathNodeType.Text:
			case XPathNodeType.SignificantWhitespace:
			case XPathNodeType.Whitespace:
				if (callChk)
				{
					this.WriteString(navigator.Value, false);
				}
				else
				{
					this.WriteStringUnchecked(navigator.Value);
				}
				break;
			case XPathNodeType.ProcessingInstruction:
				this.WriteStartProcessingInstruction(navigator.LocalName);
				this.WriteProcessingInstructionString(navigator.Value);
				this.WriteEndProcessingInstruction();
				break;
			case XPathNodeType.Comment:
				this.WriteStartComment();
				this.WriteCommentString(navigator.Value);
				this.WriteEndComment();
				break;
			}
			return flag;
		}

		// Token: 0x06003C85 RID: 15493 RVA: 0x0015109C File Offset: 0x0014F29C
		private void EndCopy(XPathNavigator navigator, bool callChk)
		{
			if (callChk)
			{
				this.WriteEndElement();
				return;
			}
			this.WriteEndElementUnchecked(navigator.Prefix, navigator.LocalName, navigator.NamespaceURI);
		}

		// Token: 0x06003C86 RID: 15494 RVA: 0x001510C0 File Offset: 0x0014F2C0
		private void CopyNamespaces(XPathNavigator navigator, XPathNamespaceScope nsScope)
		{
			if (navigator.NamespaceURI.Length == 0)
			{
				this.WriteNamespaceDeclarationUnchecked(string.Empty, string.Empty);
			}
			if (navigator.MoveToFirstNamespace(nsScope))
			{
				this.CopyNamespacesHelper(navigator, nsScope);
				navigator.MoveToParent();
			}
		}

		// Token: 0x06003C87 RID: 15495 RVA: 0x001510F8 File Offset: 0x0014F2F8
		private void CopyNamespacesHelper(XPathNavigator navigator, XPathNamespaceScope nsScope)
		{
			string localName = navigator.LocalName;
			string value = navigator.Value;
			if (navigator.MoveToNextNamespace(nsScope))
			{
				this.CopyNamespacesHelper(navigator, nsScope);
			}
			this.WriteNamespaceDeclarationUnchecked(localName, value);
		}

		// Token: 0x06003C88 RID: 15496 RVA: 0x0015112C File Offset: 0x0014F32C
		private void ConstructWithinContent(XPathNodeType rootType)
		{
			switch (this.xstate)
			{
			case XmlState.WithinSequence:
				this.StartTree(rootType);
				this.xstate = XmlState.WithinContent;
				return;
			case XmlState.EnumAttrs:
				this.StartElementContentUnchecked();
				return;
			case XmlState.WithinContent:
				break;
			default:
				this.ThrowInvalidStateError(rootType);
				break;
			}
		}

		// Token: 0x06003C89 RID: 15497 RVA: 0x00151174 File Offset: 0x0014F374
		private void ConstructInEnumAttrs(XPathNodeType rootType)
		{
			XmlState xmlState = this.xstate;
			if (xmlState != XmlState.WithinSequence)
			{
				if (xmlState != XmlState.EnumAttrs)
				{
					this.ThrowInvalidStateError(rootType);
				}
				return;
			}
			this.StartTree(rootType);
			this.xstate = XmlState.EnumAttrs;
		}

		// Token: 0x06003C8A RID: 15498 RVA: 0x001511A8 File Offset: 0x0014F3A8
		private void WriteCachedNamespaces()
		{
			while (this.cntNmsp != 0)
			{
				this.cntNmsp--;
				string text;
				string text2;
				this.nsmgr.GetNamespaceDeclaration(this.cntNmsp, out text, out text2);
				this.Writer.WriteNamespaceDeclaration(text, text2);
			}
		}

		// Token: 0x06003C8B RID: 15499 RVA: 0x001511F0 File Offset: 0x0014F3F0
		private XPathNodeType XmlStateToNodeType(XmlState xstate)
		{
			switch (xstate)
			{
			case XmlState.EnumAttrs:
				return XPathNodeType.Element;
			case XmlState.WithinContent:
				return XPathNodeType.Element;
			case XmlState.WithinAttr:
				return XPathNodeType.Attribute;
			case XmlState.WithinComment:
				return XPathNodeType.Comment;
			case XmlState.WithinPI:
				return XPathNodeType.ProcessingInstruction;
			}
			return XPathNodeType.Element;
		}

		// Token: 0x06003C8C RID: 15500 RVA: 0x00151220 File Offset: 0x0014F420
		private string CheckAttributePrefix(string prefix, string ns)
		{
			if (this.nsmgr == null)
			{
				this.WriteNamespaceDeclarationUnchecked(prefix, ns);
			}
			else
			{
				for (;;)
				{
					string text = this.nsmgr.LookupNamespace(prefix);
					if (!(text != ns))
					{
						return prefix;
					}
					if (text == null)
					{
						break;
					}
					prefix = this.RemapPrefix(prefix, ns, false);
				}
				this.AddNamespace(prefix, ns);
			}
			return prefix;
		}

		// Token: 0x06003C8D RID: 15501 RVA: 0x00151270 File Offset: 0x0014F470
		private string RemapPrefix(string prefix, string ns, bool isElemPrefix)
		{
			if (this.conflictPrefixes == null)
			{
				this.conflictPrefixes = new Dictionary<string, string>(16);
			}
			if (this.nsmgr == null)
			{
				this.nsmgr = new XmlNamespaceManager(this.runtime.NameTable);
				this.nsmgr.PushScope();
			}
			string text = this.nsmgr.LookupPrefix(ns);
			if ((text == null || (!isElemPrefix && text.Length == 0)) && (!this.conflictPrefixes.TryGetValue(ns, out text) || !(text != prefix) || (!isElemPrefix && text.Length == 0)))
			{
				string text2 = "xp_";
				int num = this.prefixIndex;
				this.prefixIndex = num + 1;
				text = text2 + num.ToString(CultureInfo.InvariantCulture);
			}
			this.conflictPrefixes[ns] = text;
			return text;
		}

		// Token: 0x06003C8E RID: 15502 RVA: 0x00151330 File Offset: 0x0014F530
		private void WriteStartComputed(XPathNodeType nodeType, string tagName, int prefixMappingsIndex)
		{
			string text;
			string text2;
			string text3;
			this.runtime.ParseTagName(tagName, prefixMappingsIndex, out text, out text2, out text3);
			text = this.EnsureValidName(text, text2, text3, nodeType);
			if (nodeType == XPathNodeType.Element)
			{
				this.WriteStartElement(text, text2, text3);
				return;
			}
			this.WriteStartAttribute(text, text2, text3);
		}

		// Token: 0x06003C8F RID: 15503 RVA: 0x00151374 File Offset: 0x0014F574
		private void WriteStartComputed(XPathNodeType nodeType, string tagName, string ns)
		{
			string text;
			string text2;
			ValidateNames.ParseQNameThrow(tagName, out text, out text2);
			text = this.EnsureValidName(text, text2, ns, nodeType);
			if (nodeType == XPathNodeType.Element)
			{
				this.WriteStartElement(text, text2, ns);
				return;
			}
			this.WriteStartAttribute(text, text2, ns);
		}

		// Token: 0x06003C90 RID: 15504 RVA: 0x001513B0 File Offset: 0x0014F5B0
		private void WriteStartComputed(XPathNodeType nodeType, XPathNavigator navigator)
		{
			string text = navigator.Prefix;
			string localName = navigator.LocalName;
			string namespaceURI = navigator.NamespaceURI;
			if (navigator.NodeType != nodeType)
			{
				text = this.EnsureValidName(text, localName, namespaceURI, nodeType);
			}
			if (nodeType == XPathNodeType.Element)
			{
				this.WriteStartElement(text, localName, namespaceURI);
				return;
			}
			this.WriteStartAttribute(text, localName, namespaceURI);
		}

		// Token: 0x06003C91 RID: 15505 RVA: 0x00151400 File Offset: 0x0014F600
		private void WriteStartComputed(XPathNodeType nodeType, XmlQualifiedName name)
		{
			string text = ((name.Namespace.Length != 0) ? this.RemapPrefix(string.Empty, name.Namespace, nodeType == XPathNodeType.Element) : string.Empty);
			text = this.EnsureValidName(text, name.Name, name.Namespace, nodeType);
			if (nodeType == XPathNodeType.Element)
			{
				this.WriteStartElement(text, name.Name, name.Namespace);
				return;
			}
			this.WriteStartAttribute(text, name.Name, name.Namespace);
		}

		// Token: 0x06003C92 RID: 15506 RVA: 0x00151477 File Offset: 0x0014F677
		private string EnsureValidName(string prefix, string localName, string ns, XPathNodeType nodeType)
		{
			if (!ValidateNames.ValidateName(prefix, localName, ns, nodeType, ValidateNames.Flags.AllExceptNCNames))
			{
				prefix = ((ns.Length != 0) ? this.RemapPrefix(string.Empty, ns, nodeType == XPathNodeType.Element) : string.Empty);
				ValidateNames.ValidateNameThrow(prefix, localName, ns, nodeType, ValidateNames.Flags.AllExceptNCNames);
			}
			return prefix;
		}

		// Token: 0x06003C93 RID: 15507 RVA: 0x001514B4 File Offset: 0x0014F6B4
		private void PushElementNames(string prefix, string localName, string ns)
		{
			if (this.stkNames == null)
			{
				this.stkNames = new Stack<string>(15);
			}
			this.stkNames.Push(prefix);
			this.stkNames.Push(localName);
			this.stkNames.Push(ns);
		}

		// Token: 0x06003C94 RID: 15508 RVA: 0x001514EF File Offset: 0x0014F6EF
		private void PopElementNames(out string prefix, out string localName, out string ns)
		{
			ns = this.stkNames.Pop();
			localName = this.stkNames.Pop();
			prefix = this.stkNames.Pop();
		}

		// Token: 0x06003C95 RID: 15509 RVA: 0x00151518 File Offset: 0x0014F718
		private void ThrowInvalidStateError(XPathNodeType constructorType)
		{
			switch (constructorType)
			{
			case XPathNodeType.Root:
			case XPathNodeType.Element:
			case XPathNodeType.Text:
			case XPathNodeType.ProcessingInstruction:
			case XPathNodeType.Comment:
				break;
			case XPathNodeType.Attribute:
			case XPathNodeType.Namespace:
				if (this.depth == 1)
				{
					throw new XslTransformException("An item of type '{0}' cannot be constructed within a node of type '{1}'.", new string[]
					{
						constructorType.ToString(),
						this.rootType.ToString()
					});
				}
				if (this.xstate == XmlState.WithinContent)
				{
					throw new XslTransformException("Attribute and namespace nodes cannot be added to the parent element after a text, comment, pi, or sub-element node has already been added.", new string[] { string.Empty });
				}
				break;
			case XPathNodeType.SignificantWhitespace:
			case XPathNodeType.Whitespace:
				goto IL_00D0;
			default:
				goto IL_00D0;
			}
			throw new XslTransformException("An item of type '{0}' cannot be constructed within a node of type '{1}'.", new string[]
			{
				constructorType.ToString(),
				this.XmlStateToNodeType(this.xstate).ToString()
			});
			IL_00D0:
			throw new XslTransformException("An item of type '{0}' cannot be constructed within a node of type '{1}'.", new string[]
			{
				"Unknown",
				this.XmlStateToNodeType(this.xstate).ToString()
			});
		}

		// Token: 0x04002790 RID: 10128
		private XmlRawWriter xwrt;

		// Token: 0x04002791 RID: 10129
		private XmlQueryRuntime runtime;

		// Token: 0x04002792 RID: 10130
		private XmlAttributeCache attrCache;

		// Token: 0x04002793 RID: 10131
		private int depth;

		// Token: 0x04002794 RID: 10132
		private XmlState xstate;

		// Token: 0x04002795 RID: 10133
		private XmlSequenceWriter seqwrt;

		// Token: 0x04002796 RID: 10134
		private XmlNamespaceManager nsmgr;

		// Token: 0x04002797 RID: 10135
		private int cntNmsp;

		// Token: 0x04002798 RID: 10136
		private Dictionary<string, string> conflictPrefixes;

		// Token: 0x04002799 RID: 10137
		private int prefixIndex;

		// Token: 0x0400279A RID: 10138
		private string piTarget;

		// Token: 0x0400279B RID: 10139
		private StringConcat nodeText;

		// Token: 0x0400279C RID: 10140
		private Stack<string> stkNames;

		// Token: 0x0400279D RID: 10141
		private XPathNodeType rootType;

		// Token: 0x0400279E RID: 10142
		private Dictionary<string, string> usedPrefixes = new Dictionary<string, string>();
	}
}
