using System;
using System.Collections.Generic;

namespace System.Xml
{
	// Token: 0x020000A8 RID: 168
	internal class QueryOutputWriter : XmlRawWriter
	{
		// Token: 0x06000587 RID: 1415 RVA: 0x000183A0 File Offset: 0x000165A0
		public QueryOutputWriter(XmlRawWriter writer, XmlWriterSettings settings)
		{
			this.wrapped = writer;
			this.systemId = settings.DocTypeSystem;
			this.publicId = settings.DocTypePublic;
			if (settings.OutputMethod == XmlOutputMethod.Xml)
			{
				if (this.systemId != null)
				{
					this.outputDocType = true;
					this.checkWellFormedDoc = true;
				}
				if (settings.AutoXmlDeclaration && settings.Standalone == XmlStandalone.Yes)
				{
					this.checkWellFormedDoc = true;
				}
				if (settings.CDataSectionElements.Count > 0)
				{
					this.bitsCData = new BitStack();
					this.lookupCDataElems = new Dictionary<XmlQualifiedName, int>();
					this.qnameCData = new XmlQualifiedName();
					foreach (XmlQualifiedName xmlQualifiedName in settings.CDataSectionElements)
					{
						this.lookupCDataElems[xmlQualifiedName] = 0;
					}
					this.bitsCData.PushBit(false);
					return;
				}
			}
			else if (settings.OutputMethod == XmlOutputMethod.Html && (this.systemId != null || this.publicId != null))
			{
				this.outputDocType = true;
			}
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x06000588 RID: 1416 RVA: 0x000184B8 File Offset: 0x000166B8
		// (set) Token: 0x06000589 RID: 1417 RVA: 0x000184C0 File Offset: 0x000166C0
		internal override IXmlNamespaceResolver NamespaceResolver
		{
			get
			{
				return this.resolver;
			}
			set
			{
				this.resolver = value;
				this.wrapped.NamespaceResolver = value;
			}
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x000184D5 File Offset: 0x000166D5
		internal override void WriteXmlDeclaration(XmlStandalone standalone)
		{
			this.wrapped.WriteXmlDeclaration(standalone);
		}

		// Token: 0x0600058B RID: 1419 RVA: 0x000184E3 File Offset: 0x000166E3
		internal override void WriteXmlDeclaration(string xmldecl)
		{
			this.wrapped.WriteXmlDeclaration(xmldecl);
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x0600058C RID: 1420 RVA: 0x000184F1 File Offset: 0x000166F1
		public override XmlWriterSettings Settings
		{
			get
			{
				XmlWriterSettings settings = this.wrapped.Settings;
				settings.ReadOnly = false;
				settings.DocTypeSystem = this.systemId;
				settings.DocTypePublic = this.publicId;
				settings.ReadOnly = true;
				return settings;
			}
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x00018524 File Offset: 0x00016724
		public override void WriteDocType(string name, string pubid, string sysid, string subset)
		{
			if (this.publicId == null && this.systemId == null)
			{
				this.wrapped.WriteDocType(name, pubid, sysid, subset);
			}
		}

		// Token: 0x0600058E RID: 1422 RVA: 0x00018548 File Offset: 0x00016748
		public override void WriteStartElement(string prefix, string localName, string ns)
		{
			this.EndCDataSection();
			if (this.checkWellFormedDoc)
			{
				if (this.depth == 0 && this.hasDocElem)
				{
					throw new XmlException("Document cannot have multiple document elements.", string.Empty);
				}
				this.depth++;
				this.hasDocElem = true;
			}
			if (this.outputDocType)
			{
				this.wrapped.WriteDocType((prefix.Length != 0) ? (prefix + ":" + localName) : localName, this.publicId, this.systemId, null);
				this.outputDocType = false;
			}
			this.wrapped.WriteStartElement(prefix, localName, ns);
			if (this.lookupCDataElems != null)
			{
				this.qnameCData.Init(localName, ns);
				this.bitsCData.PushBit(this.lookupCDataElems.ContainsKey(this.qnameCData));
			}
		}

		// Token: 0x0600058F RID: 1423 RVA: 0x00018615 File Offset: 0x00016815
		internal override void WriteEndElement(string prefix, string localName, string ns)
		{
			this.EndCDataSection();
			this.wrapped.WriteEndElement(prefix, localName, ns);
			if (this.checkWellFormedDoc)
			{
				this.depth--;
			}
			if (this.lookupCDataElems != null)
			{
				this.bitsCData.PopBit();
			}
		}

		// Token: 0x06000590 RID: 1424 RVA: 0x00018655 File Offset: 0x00016855
		internal override void WriteFullEndElement(string prefix, string localName, string ns)
		{
			this.EndCDataSection();
			this.wrapped.WriteFullEndElement(prefix, localName, ns);
			if (this.checkWellFormedDoc)
			{
				this.depth--;
			}
			if (this.lookupCDataElems != null)
			{
				this.bitsCData.PopBit();
			}
		}

		// Token: 0x06000591 RID: 1425 RVA: 0x00018695 File Offset: 0x00016895
		internal override void StartElementContent()
		{
			this.wrapped.StartElementContent();
		}

		// Token: 0x06000592 RID: 1426 RVA: 0x000186A2 File Offset: 0x000168A2
		public override void WriteStartAttribute(string prefix, string localName, string ns)
		{
			this.inAttr = true;
			this.wrapped.WriteStartAttribute(prefix, localName, ns);
		}

		// Token: 0x06000593 RID: 1427 RVA: 0x000186B9 File Offset: 0x000168B9
		public override void WriteEndAttribute()
		{
			this.inAttr = false;
			this.wrapped.WriteEndAttribute();
		}

		// Token: 0x06000594 RID: 1428 RVA: 0x000186CD File Offset: 0x000168CD
		internal override void WriteNamespaceDeclaration(string prefix, string ns)
		{
			this.wrapped.WriteNamespaceDeclaration(prefix, ns);
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06000595 RID: 1429 RVA: 0x000186DC File Offset: 0x000168DC
		internal override bool SupportsNamespaceDeclarationInChunks
		{
			get
			{
				return this.wrapped.SupportsNamespaceDeclarationInChunks;
			}
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x000186E9 File Offset: 0x000168E9
		internal override void WriteStartNamespaceDeclaration(string prefix)
		{
			this.wrapped.WriteStartNamespaceDeclaration(prefix);
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x000186F7 File Offset: 0x000168F7
		internal override void WriteEndNamespaceDeclaration()
		{
			this.wrapped.WriteEndNamespaceDeclaration();
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x00018704 File Offset: 0x00016904
		public override void WriteCData(string text)
		{
			this.wrapped.WriteCData(text);
		}

		// Token: 0x06000599 RID: 1433 RVA: 0x00018712 File Offset: 0x00016912
		public override void WriteComment(string text)
		{
			this.EndCDataSection();
			this.wrapped.WriteComment(text);
		}

		// Token: 0x0600059A RID: 1434 RVA: 0x00018726 File Offset: 0x00016926
		public override void WriteProcessingInstruction(string name, string text)
		{
			this.EndCDataSection();
			this.wrapped.WriteProcessingInstruction(name, text);
		}

		// Token: 0x0600059B RID: 1435 RVA: 0x0001873B File Offset: 0x0001693B
		public override void WriteWhitespace(string ws)
		{
			if (!this.inAttr && (this.inCDataSection || this.StartCDataSection()))
			{
				this.wrapped.WriteCData(ws);
				return;
			}
			this.wrapped.WriteWhitespace(ws);
		}

		// Token: 0x0600059C RID: 1436 RVA: 0x0001876E File Offset: 0x0001696E
		public override void WriteString(string text)
		{
			if (!this.inAttr && (this.inCDataSection || this.StartCDataSection()))
			{
				this.wrapped.WriteCData(text);
				return;
			}
			this.wrapped.WriteString(text);
		}

		// Token: 0x0600059D RID: 1437 RVA: 0x000187A1 File Offset: 0x000169A1
		public override void WriteChars(char[] buffer, int index, int count)
		{
			if (!this.inAttr && (this.inCDataSection || this.StartCDataSection()))
			{
				this.wrapped.WriteCData(new string(buffer, index, count));
				return;
			}
			this.wrapped.WriteChars(buffer, index, count);
		}

		// Token: 0x0600059E RID: 1438 RVA: 0x000187DD File Offset: 0x000169DD
		public override void WriteEntityRef(string name)
		{
			this.EndCDataSection();
			this.wrapped.WriteEntityRef(name);
		}

		// Token: 0x0600059F RID: 1439 RVA: 0x000187F1 File Offset: 0x000169F1
		public override void WriteCharEntity(char ch)
		{
			this.EndCDataSection();
			this.wrapped.WriteCharEntity(ch);
		}

		// Token: 0x060005A0 RID: 1440 RVA: 0x00018805 File Offset: 0x00016A05
		public override void WriteSurrogateCharEntity(char lowChar, char highChar)
		{
			this.EndCDataSection();
			this.wrapped.WriteSurrogateCharEntity(lowChar, highChar);
		}

		// Token: 0x060005A1 RID: 1441 RVA: 0x0001881A File Offset: 0x00016A1A
		public override void WriteRaw(char[] buffer, int index, int count)
		{
			if (!this.inAttr && (this.inCDataSection || this.StartCDataSection()))
			{
				this.wrapped.WriteCData(new string(buffer, index, count));
				return;
			}
			this.wrapped.WriteRaw(buffer, index, count);
		}

		// Token: 0x060005A2 RID: 1442 RVA: 0x00018856 File Offset: 0x00016A56
		public override void WriteRaw(string data)
		{
			if (!this.inAttr && (this.inCDataSection || this.StartCDataSection()))
			{
				this.wrapped.WriteCData(data);
				return;
			}
			this.wrapped.WriteRaw(data);
		}

		// Token: 0x060005A3 RID: 1443 RVA: 0x00018889 File Offset: 0x00016A89
		public override void Close()
		{
			this.wrapped.Close();
			if (this.checkWellFormedDoc && !this.hasDocElem)
			{
				throw new XmlException("Document does not have a root element.", string.Empty);
			}
		}

		// Token: 0x060005A4 RID: 1444 RVA: 0x000188B6 File Offset: 0x00016AB6
		public override void Flush()
		{
			this.wrapped.Flush();
		}

		// Token: 0x060005A5 RID: 1445 RVA: 0x000188C3 File Offset: 0x00016AC3
		private bool StartCDataSection()
		{
			if (this.lookupCDataElems != null && this.bitsCData.PeekBit())
			{
				this.inCDataSection = true;
				return true;
			}
			return false;
		}

		// Token: 0x060005A6 RID: 1446 RVA: 0x000188E4 File Offset: 0x00016AE4
		private void EndCDataSection()
		{
			this.inCDataSection = false;
		}

		// Token: 0x04000341 RID: 833
		private XmlRawWriter wrapped;

		// Token: 0x04000342 RID: 834
		private bool inCDataSection;

		// Token: 0x04000343 RID: 835
		private Dictionary<XmlQualifiedName, int> lookupCDataElems;

		// Token: 0x04000344 RID: 836
		private BitStack bitsCData;

		// Token: 0x04000345 RID: 837
		private XmlQualifiedName qnameCData;

		// Token: 0x04000346 RID: 838
		private bool outputDocType;

		// Token: 0x04000347 RID: 839
		private bool checkWellFormedDoc;

		// Token: 0x04000348 RID: 840
		private bool hasDocElem;

		// Token: 0x04000349 RID: 841
		private bool inAttr;

		// Token: 0x0400034A RID: 842
		private string systemId;

		// Token: 0x0400034B RID: 843
		private string publicId;

		// Token: 0x0400034C RID: 844
		private int depth;
	}
}
