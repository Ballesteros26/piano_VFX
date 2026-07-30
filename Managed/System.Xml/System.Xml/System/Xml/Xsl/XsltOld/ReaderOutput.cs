using System;
using System.Collections;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml.Utils;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x02000536 RID: 1334
	internal class ReaderOutput : XmlReader, RecordOutput
	{
		// Token: 0x060035C2 RID: 13762 RVA: 0x0012DFC0 File Offset: 0x0012C1C0
		internal ReaderOutput(Processor processor)
		{
			this.processor = processor;
			this.nameTable = processor.NameTable;
			this.Reset();
		}

		// Token: 0x17000B5A RID: 2906
		// (get) Token: 0x060035C3 RID: 13763 RVA: 0x0012DFF7 File Offset: 0x0012C1F7
		public override XmlNodeType NodeType
		{
			get
			{
				return this.currentInfo.NodeType;
			}
		}

		// Token: 0x17000B5B RID: 2907
		// (get) Token: 0x060035C4 RID: 13764 RVA: 0x0012E004 File Offset: 0x0012C204
		public override string Name
		{
			get
			{
				string prefix = this.Prefix;
				string localName = this.LocalName;
				if (prefix == null || prefix.Length <= 0)
				{
					return localName;
				}
				if (localName.Length > 0)
				{
					return this.nameTable.Add(prefix + ":" + localName);
				}
				return prefix;
			}
		}

		// Token: 0x17000B5C RID: 2908
		// (get) Token: 0x060035C5 RID: 13765 RVA: 0x0012E04F File Offset: 0x0012C24F
		public override string LocalName
		{
			get
			{
				return this.currentInfo.LocalName;
			}
		}

		// Token: 0x17000B5D RID: 2909
		// (get) Token: 0x060035C6 RID: 13766 RVA: 0x0012E05C File Offset: 0x0012C25C
		public override string NamespaceURI
		{
			get
			{
				return this.currentInfo.NamespaceURI;
			}
		}

		// Token: 0x17000B5E RID: 2910
		// (get) Token: 0x060035C7 RID: 13767 RVA: 0x0012E069 File Offset: 0x0012C269
		public override string Prefix
		{
			get
			{
				return this.currentInfo.Prefix;
			}
		}

		// Token: 0x17000B5F RID: 2911
		// (get) Token: 0x060035C8 RID: 13768 RVA: 0x000296AD File Offset: 0x000278AD
		public override bool HasValue
		{
			get
			{
				return XmlReader.HasValueInternal(this.NodeType);
			}
		}

		// Token: 0x17000B60 RID: 2912
		// (get) Token: 0x060035C9 RID: 13769 RVA: 0x0012E076 File Offset: 0x0012C276
		public override string Value
		{
			get
			{
				return this.currentInfo.Value;
			}
		}

		// Token: 0x17000B61 RID: 2913
		// (get) Token: 0x060035CA RID: 13770 RVA: 0x0012E083 File Offset: 0x0012C283
		public override int Depth
		{
			get
			{
				return this.currentInfo.Depth;
			}
		}

		// Token: 0x17000B62 RID: 2914
		// (get) Token: 0x060035CB RID: 13771 RVA: 0x00003065 File Offset: 0x00001265
		public override string BaseURI
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x17000B63 RID: 2915
		// (get) Token: 0x060035CC RID: 13772 RVA: 0x0012E090 File Offset: 0x0012C290
		public override bool IsEmptyElement
		{
			get
			{
				return this.currentInfo.IsEmptyTag;
			}
		}

		// Token: 0x17000B64 RID: 2916
		// (get) Token: 0x060035CD RID: 13773 RVA: 0x0012E09D File Offset: 0x0012C29D
		public override char QuoteChar
		{
			get
			{
				return this.encoder.QuoteChar;
			}
		}

		// Token: 0x17000B65 RID: 2917
		// (get) Token: 0x060035CE RID: 13774 RVA: 0x0000226C File Offset: 0x0000046C
		public override bool IsDefault
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000B66 RID: 2918
		// (get) Token: 0x060035CF RID: 13775 RVA: 0x0012E0AA File Offset: 0x0012C2AA
		public override XmlSpace XmlSpace
		{
			get
			{
				if (this.manager == null)
				{
					return XmlSpace.None;
				}
				return this.manager.XmlSpace;
			}
		}

		// Token: 0x17000B67 RID: 2919
		// (get) Token: 0x060035D0 RID: 13776 RVA: 0x0012E0C1 File Offset: 0x0012C2C1
		public override string XmlLang
		{
			get
			{
				if (this.manager == null)
				{
					return string.Empty;
				}
				return this.manager.XmlLang;
			}
		}

		// Token: 0x17000B68 RID: 2920
		// (get) Token: 0x060035D1 RID: 13777 RVA: 0x0012E0DC File Offset: 0x0012C2DC
		public override int AttributeCount
		{
			get
			{
				return this.attributeCount;
			}
		}

		// Token: 0x060035D2 RID: 13778 RVA: 0x0012E0E4 File Offset: 0x0012C2E4
		public override string GetAttribute(string name)
		{
			int num;
			if (this.FindAttribute(name, out num))
			{
				return ((BuilderInfo)this.attributeList[num]).Value;
			}
			return null;
		}

		// Token: 0x060035D3 RID: 13779 RVA: 0x0012E114 File Offset: 0x0012C314
		public override string GetAttribute(string localName, string namespaceURI)
		{
			int num;
			if (this.FindAttribute(localName, namespaceURI, out num))
			{
				return ((BuilderInfo)this.attributeList[num]).Value;
			}
			return null;
		}

		// Token: 0x060035D4 RID: 13780 RVA: 0x0012E145 File Offset: 0x0012C345
		public override string GetAttribute(int i)
		{
			return this.GetBuilderInfo(i).Value;
		}

		// Token: 0x17000B69 RID: 2921
		public override string this[int i]
		{
			get
			{
				return this.GetAttribute(i);
			}
		}

		// Token: 0x17000B6A RID: 2922
		public override string this[string name]
		{
			get
			{
				return this.GetAttribute(name);
			}
		}

		// Token: 0x17000B6B RID: 2923
		public override string this[string name, string namespaceURI]
		{
			get
			{
				return this.GetAttribute(name, namespaceURI);
			}
		}

		// Token: 0x060035D8 RID: 13784 RVA: 0x0012E154 File Offset: 0x0012C354
		public override bool MoveToAttribute(string name)
		{
			int num;
			if (this.FindAttribute(name, out num))
			{
				this.SetAttribute(num);
				return true;
			}
			return false;
		}

		// Token: 0x060035D9 RID: 13785 RVA: 0x0012E178 File Offset: 0x0012C378
		public override bool MoveToAttribute(string localName, string namespaceURI)
		{
			int num;
			if (this.FindAttribute(localName, namespaceURI, out num))
			{
				this.SetAttribute(num);
				return true;
			}
			return false;
		}

		// Token: 0x060035DA RID: 13786 RVA: 0x0012E19B File Offset: 0x0012C39B
		public override void MoveToAttribute(int i)
		{
			if (i < 0 || this.attributeCount <= i)
			{
				throw new ArgumentOutOfRangeException("i");
			}
			this.SetAttribute(i);
		}

		// Token: 0x060035DB RID: 13787 RVA: 0x0012E1BC File Offset: 0x0012C3BC
		public override bool MoveToFirstAttribute()
		{
			if (this.attributeCount <= 0)
			{
				return false;
			}
			this.SetAttribute(0);
			return true;
		}

		// Token: 0x060035DC RID: 13788 RVA: 0x0012E1D1 File Offset: 0x0012C3D1
		public override bool MoveToNextAttribute()
		{
			if (this.currentIndex + 1 < this.attributeCount)
			{
				this.SetAttribute(this.currentIndex + 1);
				return true;
			}
			return false;
		}

		// Token: 0x060035DD RID: 13789 RVA: 0x0012E1F4 File Offset: 0x0012C3F4
		public override bool MoveToElement()
		{
			if (this.NodeType == XmlNodeType.Attribute || this.currentInfo == this.attributeValue)
			{
				this.SetMainNode();
				return true;
			}
			return false;
		}

		// Token: 0x060035DE RID: 13790 RVA: 0x0012E218 File Offset: 0x0012C418
		public override bool Read()
		{
			if (this.state != ReadState.Interactive)
			{
				if (this.state != ReadState.Initial)
				{
					return false;
				}
				this.state = ReadState.Interactive;
			}
			for (;;)
			{
				if (this.haveRecord)
				{
					this.processor.ResetOutput();
					this.haveRecord = false;
				}
				this.processor.Execute();
				if (!this.haveRecord)
				{
					goto IL_00A0;
				}
				XmlNodeType nodeType = this.NodeType;
				if (nodeType != XmlNodeType.Text)
				{
					if (nodeType != XmlNodeType.Whitespace)
					{
						break;
					}
				}
				else
				{
					if (!this.xmlCharType.IsOnlyWhitespace(this.Value))
					{
						break;
					}
					this.currentInfo.NodeType = XmlNodeType.Whitespace;
				}
				if (this.Value.Length != 0)
				{
					goto Block_8;
				}
			}
			goto IL_00AD;
			Block_8:
			if (this.XmlSpace == XmlSpace.Preserve)
			{
				this.currentInfo.NodeType = XmlNodeType.SignificantWhitespace;
				goto IL_00AD;
			}
			goto IL_00AD;
			IL_00A0:
			this.state = ReadState.EndOfFile;
			this.Reset();
			IL_00AD:
			return this.haveRecord;
		}

		// Token: 0x17000B6C RID: 2924
		// (get) Token: 0x060035DF RID: 13791 RVA: 0x0012E2D8 File Offset: 0x0012C4D8
		public override bool EOF
		{
			get
			{
				return this.state == ReadState.EndOfFile;
			}
		}

		// Token: 0x060035E0 RID: 13792 RVA: 0x0012E2E3 File Offset: 0x0012C4E3
		public override void Close()
		{
			this.processor = null;
			this.state = ReadState.Closed;
			this.Reset();
		}

		// Token: 0x17000B6D RID: 2925
		// (get) Token: 0x060035E1 RID: 13793 RVA: 0x0012E2F9 File Offset: 0x0012C4F9
		public override ReadState ReadState
		{
			get
			{
				return this.state;
			}
		}

		// Token: 0x060035E2 RID: 13794 RVA: 0x0012E304 File Offset: 0x0012C504
		public override string ReadString()
		{
			string text = string.Empty;
			if (this.NodeType == XmlNodeType.Element || this.NodeType == XmlNodeType.Attribute || this.currentInfo == this.attributeValue)
			{
				if (this.mainNode.IsEmptyTag)
				{
					return text;
				}
				if (!this.Read())
				{
					throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current state of the object."));
				}
			}
			StringBuilder stringBuilder = null;
			bool flag = true;
			do
			{
				XmlNodeType nodeType = this.NodeType;
				if (nodeType != XmlNodeType.Text && nodeType - XmlNodeType.Whitespace > 1)
				{
					goto IL_00A0;
				}
				if (flag)
				{
					text = this.Value;
					flag = false;
				}
				else
				{
					if (stringBuilder == null)
					{
						stringBuilder = new StringBuilder(text);
					}
					stringBuilder.Append(this.Value);
				}
			}
			while (this.Read());
			throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current state of the object."));
			IL_00A0:
			if (stringBuilder != null)
			{
				return stringBuilder.ToString();
			}
			return text;
		}

		// Token: 0x060035E3 RID: 13795 RVA: 0x0012E3BC File Offset: 0x0012C5BC
		public override string ReadInnerXml()
		{
			if (this.ReadState == ReadState.Interactive)
			{
				if (this.NodeType == XmlNodeType.Element && !this.IsEmptyElement)
				{
					StringOutput stringOutput = new StringOutput(this.processor);
					stringOutput.OmitXmlDecl();
					int i = this.Depth;
					this.Read();
					while (i < this.Depth)
					{
						stringOutput.RecordDone(this.builder);
						this.Read();
					}
					this.Read();
					stringOutput.TheEnd();
					return stringOutput.Result;
				}
				if (this.NodeType == XmlNodeType.Attribute)
				{
					return this.encoder.AtributeInnerXml(this.Value);
				}
				this.Read();
			}
			return string.Empty;
		}

		// Token: 0x060035E4 RID: 13796 RVA: 0x0012E460 File Offset: 0x0012C660
		public override string ReadOuterXml()
		{
			if (this.ReadState == ReadState.Interactive)
			{
				if (this.NodeType == XmlNodeType.Element)
				{
					StringOutput stringOutput = new StringOutput(this.processor);
					stringOutput.OmitXmlDecl();
					bool isEmptyElement = this.IsEmptyElement;
					int i = this.Depth;
					stringOutput.RecordDone(this.builder);
					this.Read();
					while (i < this.Depth)
					{
						stringOutput.RecordDone(this.builder);
						this.Read();
					}
					if (!isEmptyElement)
					{
						stringOutput.RecordDone(this.builder);
						this.Read();
					}
					stringOutput.TheEnd();
					return stringOutput.Result;
				}
				if (this.NodeType == XmlNodeType.Attribute)
				{
					return this.encoder.AtributeOuterXml(this.Name, this.Value);
				}
				this.Read();
			}
			return string.Empty;
		}

		// Token: 0x17000B6E RID: 2926
		// (get) Token: 0x060035E5 RID: 13797 RVA: 0x0012E526 File Offset: 0x0012C726
		public override XmlNameTable NameTable
		{
			get
			{
				return this.nameTable;
			}
		}

		// Token: 0x060035E6 RID: 13798 RVA: 0x0012E52E File Offset: 0x0012C72E
		public override string LookupNamespace(string prefix)
		{
			prefix = this.nameTable.Get(prefix);
			if (this.manager != null && prefix != null)
			{
				return this.manager.ResolveNamespace(prefix);
			}
			return null;
		}

		// Token: 0x060035E7 RID: 13799 RVA: 0x0012E557 File Offset: 0x0012C757
		public override void ResolveEntity()
		{
			if (this.NodeType != XmlNodeType.EntityReference)
			{
				throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current state of the object."));
			}
		}

		// Token: 0x060035E8 RID: 13800 RVA: 0x0012E574 File Offset: 0x0012C774
		public override bool ReadAttributeValue()
		{
			if (this.ReadState != ReadState.Interactive || this.NodeType != XmlNodeType.Attribute)
			{
				return false;
			}
			if (this.attributeValue == null)
			{
				this.attributeValue = new BuilderInfo();
				this.attributeValue.NodeType = XmlNodeType.Text;
			}
			if (this.currentInfo == this.attributeValue)
			{
				return false;
			}
			this.attributeValue.Value = this.currentInfo.Value;
			this.attributeValue.Depth = this.currentInfo.Depth + 1;
			this.currentInfo = this.attributeValue;
			return true;
		}

		// Token: 0x060035E9 RID: 13801 RVA: 0x0012E600 File Offset: 0x0012C800
		public Processor.OutputResult RecordDone(RecordBuilder record)
		{
			this.builder = record;
			this.mainNode = record.MainNode;
			this.attributeList = record.AttributeList;
			this.attributeCount = record.AttributeCount;
			this.manager = record.Manager;
			this.haveRecord = true;
			this.SetMainNode();
			return Processor.OutputResult.Interrupt;
		}

		// Token: 0x060035EA RID: 13802 RVA: 0x00002F50 File Offset: 0x00001150
		public void TheEnd()
		{
		}

		// Token: 0x060035EB RID: 13803 RVA: 0x0012E652 File Offset: 0x0012C852
		private void SetMainNode()
		{
			this.currentIndex = -1;
			this.currentInfo = this.mainNode;
		}

		// Token: 0x060035EC RID: 13804 RVA: 0x0012E667 File Offset: 0x0012C867
		private void SetAttribute(int attrib)
		{
			this.currentIndex = attrib;
			this.currentInfo = (BuilderInfo)this.attributeList[attrib];
		}

		// Token: 0x060035ED RID: 13805 RVA: 0x0012E687 File Offset: 0x0012C887
		private BuilderInfo GetBuilderInfo(int attrib)
		{
			if (attrib < 0 || this.attributeCount <= attrib)
			{
				throw new ArgumentOutOfRangeException("attrib");
			}
			return (BuilderInfo)this.attributeList[attrib];
		}

		// Token: 0x060035EE RID: 13806 RVA: 0x0012E6B4 File Offset: 0x0012C8B4
		private bool FindAttribute(string localName, string namespaceURI, out int attrIndex)
		{
			if (namespaceURI == null)
			{
				namespaceURI = string.Empty;
			}
			if (localName == null)
			{
				localName = string.Empty;
			}
			for (int i = 0; i < this.attributeCount; i++)
			{
				BuilderInfo builderInfo = (BuilderInfo)this.attributeList[i];
				if (builderInfo.NamespaceURI == namespaceURI && builderInfo.LocalName == localName)
				{
					attrIndex = i;
					return true;
				}
			}
			attrIndex = -1;
			return false;
		}

		// Token: 0x060035EF RID: 13807 RVA: 0x0012E720 File Offset: 0x0012C920
		private bool FindAttribute(string name, out int attrIndex)
		{
			if (name == null)
			{
				name = string.Empty;
			}
			for (int i = 0; i < this.attributeCount; i++)
			{
				if (((BuilderInfo)this.attributeList[i]).Name == name)
				{
					attrIndex = i;
					return true;
				}
			}
			attrIndex = -1;
			return false;
		}

		// Token: 0x060035F0 RID: 13808 RVA: 0x0012E76F File Offset: 0x0012C96F
		private void Reset()
		{
			this.currentIndex = -1;
			this.currentInfo = ReaderOutput.s_DefaultInfo;
			this.mainNode = ReaderOutput.s_DefaultInfo;
			this.manager = null;
		}

		// Token: 0x060035F1 RID: 13809 RVA: 0x00002F50 File Offset: 0x00001150
		[Conditional("DEBUG")]
		private void CheckCurrentInfo()
		{
		}

		// Token: 0x0400224F RID: 8783
		private Processor processor;

		// Token: 0x04002250 RID: 8784
		private XmlNameTable nameTable;

		// Token: 0x04002251 RID: 8785
		private RecordBuilder builder;

		// Token: 0x04002252 RID: 8786
		private BuilderInfo mainNode;

		// Token: 0x04002253 RID: 8787
		private ArrayList attributeList;

		// Token: 0x04002254 RID: 8788
		private int attributeCount;

		// Token: 0x04002255 RID: 8789
		private BuilderInfo attributeValue;

		// Token: 0x04002256 RID: 8790
		private OutputScopeManager manager;

		// Token: 0x04002257 RID: 8791
		private int currentIndex;

		// Token: 0x04002258 RID: 8792
		private BuilderInfo currentInfo;

		// Token: 0x04002259 RID: 8793
		private ReadState state;

		// Token: 0x0400225A RID: 8794
		private bool haveRecord;

		// Token: 0x0400225B RID: 8795
		private static BuilderInfo s_DefaultInfo = new BuilderInfo();

		// Token: 0x0400225C RID: 8796
		private ReaderOutput.XmlEncoder encoder = new ReaderOutput.XmlEncoder();

		// Token: 0x0400225D RID: 8797
		private XmlCharType xmlCharType = XmlCharType.Instance;

		// Token: 0x02000537 RID: 1335
		private class XmlEncoder
		{
			// Token: 0x060035F3 RID: 13811 RVA: 0x0012E7A1 File Offset: 0x0012C9A1
			private void Init()
			{
				this.buffer = new StringBuilder();
				this.encoder = new XmlTextEncoder(new StringWriter(this.buffer, CultureInfo.InvariantCulture));
			}

			// Token: 0x060035F4 RID: 13812 RVA: 0x0012E7CC File Offset: 0x0012C9CC
			public string AtributeInnerXml(string value)
			{
				if (this.encoder == null)
				{
					this.Init();
				}
				this.buffer.Length = 0;
				this.encoder.StartAttribute(false);
				this.encoder.Write(value);
				this.encoder.EndAttribute();
				return this.buffer.ToString();
			}

			// Token: 0x060035F5 RID: 13813 RVA: 0x0012E824 File Offset: 0x0012CA24
			public string AtributeOuterXml(string name, string value)
			{
				if (this.encoder == null)
				{
					this.Init();
				}
				this.buffer.Length = 0;
				this.buffer.Append(name);
				this.buffer.Append('=');
				this.buffer.Append(this.QuoteChar);
				this.encoder.StartAttribute(false);
				this.encoder.Write(value);
				this.encoder.EndAttribute();
				this.buffer.Append(this.QuoteChar);
				return this.buffer.ToString();
			}

			// Token: 0x17000B6F RID: 2927
			// (get) Token: 0x060035F6 RID: 13814 RVA: 0x000296BA File Offset: 0x000278BA
			public char QuoteChar
			{
				get
				{
					return '"';
				}
			}

			// Token: 0x0400225E RID: 8798
			private StringBuilder buffer;

			// Token: 0x0400225F RID: 8799
			private XmlTextEncoder encoder;
		}
	}
}
