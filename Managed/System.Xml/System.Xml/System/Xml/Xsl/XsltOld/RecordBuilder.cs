using System;
using System.Collections;
using System.Text;
using System.Xml.XPath;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x02000538 RID: 1336
	internal sealed class RecordBuilder
	{
		// Token: 0x060035F8 RID: 13816 RVA: 0x0012E8B8 File Offset: 0x0012CAB8
		internal RecordBuilder(RecordOutput output, XmlNameTable nameTable)
		{
			this.output = output;
			this.nameTable = ((nameTable != null) ? nameTable : new NameTable());
			this.atoms = new OutKeywords(this.nameTable);
			this.scopeManager = new OutputScopeManager(this.nameTable, this.atoms);
		}

		// Token: 0x17000B70 RID: 2928
		// (get) Token: 0x060035F9 RID: 13817 RVA: 0x0012E937 File Offset: 0x0012CB37
		// (set) Token: 0x060035FA RID: 13818 RVA: 0x0012E93F File Offset: 0x0012CB3F
		internal int OutputState
		{
			get
			{
				return this.outputState;
			}
			set
			{
				this.outputState = value;
			}
		}

		// Token: 0x17000B71 RID: 2929
		// (get) Token: 0x060035FB RID: 13819 RVA: 0x0012E948 File Offset: 0x0012CB48
		// (set) Token: 0x060035FC RID: 13820 RVA: 0x0012E950 File Offset: 0x0012CB50
		internal RecordBuilder Next
		{
			get
			{
				return this.next;
			}
			set
			{
				this.next = value;
			}
		}

		// Token: 0x17000B72 RID: 2930
		// (get) Token: 0x060035FD RID: 13821 RVA: 0x0012E959 File Offset: 0x0012CB59
		internal RecordOutput Output
		{
			get
			{
				return this.output;
			}
		}

		// Token: 0x17000B73 RID: 2931
		// (get) Token: 0x060035FE RID: 13822 RVA: 0x0012E961 File Offset: 0x0012CB61
		internal BuilderInfo MainNode
		{
			get
			{
				return this.mainNode;
			}
		}

		// Token: 0x17000B74 RID: 2932
		// (get) Token: 0x060035FF RID: 13823 RVA: 0x0012E969 File Offset: 0x0012CB69
		internal ArrayList AttributeList
		{
			get
			{
				return this.attributeList;
			}
		}

		// Token: 0x17000B75 RID: 2933
		// (get) Token: 0x06003600 RID: 13824 RVA: 0x0012E971 File Offset: 0x0012CB71
		internal int AttributeCount
		{
			get
			{
				return this.attributeCount;
			}
		}

		// Token: 0x17000B76 RID: 2934
		// (get) Token: 0x06003601 RID: 13825 RVA: 0x0012E979 File Offset: 0x0012CB79
		internal OutputScopeManager Manager
		{
			get
			{
				return this.scopeManager;
			}
		}

		// Token: 0x06003602 RID: 13826 RVA: 0x0012E981 File Offset: 0x0012CB81
		private void ValueAppend(string s, bool disableOutputEscaping)
		{
			this.currentInfo.ValueAppend(s, disableOutputEscaping);
		}

		// Token: 0x06003603 RID: 13827 RVA: 0x0012E990 File Offset: 0x0012CB90
		private bool CanOutput(int state)
		{
			if (this.recordState == 0 || (state & 8192) == 0)
			{
				return true;
			}
			this.recordState = 2;
			this.FinalizeRecord();
			this.SetEmptyFlag(state);
			return this.output.RecordDone(this) == Processor.OutputResult.Continue;
		}

		// Token: 0x06003604 RID: 13828 RVA: 0x0012E9C8 File Offset: 0x0012CBC8
		internal Processor.OutputResult BeginEvent(int state, XPathNodeType nodeType, string prefix, string name, string nspace, bool empty, object htmlProps, bool search)
		{
			if (!this.CanOutput(state))
			{
				return Processor.OutputResult.Overflow;
			}
			this.AdjustDepth(state);
			this.ResetRecord(state);
			this.PopElementScope();
			prefix = ((prefix != null) ? this.nameTable.Add(prefix) : this.atoms.Empty);
			name = ((name != null) ? this.nameTable.Add(name) : this.atoms.Empty);
			nspace = ((nspace != null) ? this.nameTable.Add(nspace) : this.atoms.Empty);
			switch (nodeType)
			{
			case XPathNodeType.Element:
				this.mainNode.htmlProps = htmlProps as HtmlElementProps;
				this.mainNode.search = search;
				this.BeginElement(prefix, name, nspace, empty);
				break;
			case XPathNodeType.Attribute:
				this.BeginAttribute(prefix, name, nspace, htmlProps, search);
				break;
			case XPathNodeType.Namespace:
				this.BeginNamespace(name, nspace);
				break;
			case XPathNodeType.ProcessingInstruction:
				if (!this.BeginProcessingInstruction(prefix, name, nspace))
				{
					return Processor.OutputResult.Error;
				}
				break;
			case XPathNodeType.Comment:
				this.BeginComment();
				break;
			}
			return this.CheckRecordBegin(state);
		}

		// Token: 0x06003605 RID: 13829 RVA: 0x0012EAEC File Offset: 0x0012CCEC
		internal Processor.OutputResult TextEvent(int state, string text, bool disableOutputEscaping)
		{
			if (!this.CanOutput(state))
			{
				return Processor.OutputResult.Overflow;
			}
			this.AdjustDepth(state);
			this.ResetRecord(state);
			this.PopElementScope();
			if ((state & 8192) != 0)
			{
				this.currentInfo.Depth = this.recordDepth;
				this.currentInfo.NodeType = XmlNodeType.Text;
			}
			this.ValueAppend(text, disableOutputEscaping);
			return this.CheckRecordBegin(state);
		}

		// Token: 0x06003606 RID: 13830 RVA: 0x0012EB50 File Offset: 0x0012CD50
		internal Processor.OutputResult EndEvent(int state, XPathNodeType nodeType)
		{
			if (!this.CanOutput(state))
			{
				return Processor.OutputResult.Overflow;
			}
			this.AdjustDepth(state);
			this.PopElementScope();
			this.popScope = (state & 65536) != 0;
			if ((state & 4096) != 0 && this.mainNode.IsEmptyTag)
			{
				return Processor.OutputResult.Continue;
			}
			this.ResetRecord(state);
			if ((state & 8192) != 0 && nodeType == XPathNodeType.Element)
			{
				this.EndElement();
			}
			return this.CheckRecordEnd(state);
		}

		// Token: 0x06003607 RID: 13831 RVA: 0x0012EBBE File Offset: 0x0012CDBE
		internal void Reset()
		{
			if (this.recordState == 2)
			{
				this.recordState = 0;
			}
		}

		// Token: 0x06003608 RID: 13832 RVA: 0x0012EBD0 File Offset: 0x0012CDD0
		internal void TheEnd()
		{
			if (this.recordState == 1)
			{
				this.recordState = 2;
				this.FinalizeRecord();
				this.output.RecordDone(this);
			}
			this.output.TheEnd();
		}

		// Token: 0x06003609 RID: 13833 RVA: 0x0012EC00 File Offset: 0x0012CE00
		private int FindAttribute(string name, string nspace, ref string prefix)
		{
			for (int i = 0; i < this.attributeCount; i++)
			{
				BuilderInfo builderInfo = (BuilderInfo)this.attributeList[i];
				if (Ref.Equal(builderInfo.LocalName, name))
				{
					if (Ref.Equal(builderInfo.NamespaceURI, nspace))
					{
						return i;
					}
					if (Ref.Equal(builderInfo.Prefix, prefix))
					{
						prefix = string.Empty;
					}
				}
			}
			return -1;
		}

		// Token: 0x0600360A RID: 13834 RVA: 0x0012EC68 File Offset: 0x0012CE68
		private void BeginElement(string prefix, string name, string nspace, bool empty)
		{
			this.currentInfo.NodeType = XmlNodeType.Element;
			this.currentInfo.Prefix = prefix;
			this.currentInfo.LocalName = name;
			this.currentInfo.NamespaceURI = nspace;
			this.currentInfo.Depth = this.recordDepth;
			this.currentInfo.IsEmptyTag = empty;
			this.scopeManager.PushScope(name, nspace, prefix);
		}

		// Token: 0x0600360B RID: 13835 RVA: 0x0012ECD4 File Offset: 0x0012CED4
		private void EndElement()
		{
			OutputScope currentElementScope = this.scopeManager.CurrentElementScope;
			this.currentInfo.NodeType = XmlNodeType.EndElement;
			this.currentInfo.Prefix = currentElementScope.Prefix;
			this.currentInfo.LocalName = currentElementScope.Name;
			this.currentInfo.NamespaceURI = currentElementScope.Namespace;
			this.currentInfo.Depth = this.recordDepth;
		}

		// Token: 0x0600360C RID: 13836 RVA: 0x0012ED40 File Offset: 0x0012CF40
		private int NewAttribute()
		{
			if (this.attributeCount >= this.attributeList.Count)
			{
				this.attributeList.Add(new BuilderInfo());
			}
			int num = this.attributeCount;
			this.attributeCount = num + 1;
			return num;
		}

		// Token: 0x0600360D RID: 13837 RVA: 0x0012ED84 File Offset: 0x0012CF84
		private void BeginAttribute(string prefix, string name, string nspace, object htmlAttrProps, bool search)
		{
			int num = this.FindAttribute(name, nspace, ref prefix);
			if (num == -1)
			{
				num = this.NewAttribute();
			}
			BuilderInfo builderInfo = (BuilderInfo)this.attributeList[num];
			builderInfo.Initialize(prefix, name, nspace);
			builderInfo.Depth = this.recordDepth;
			builderInfo.NodeType = XmlNodeType.Attribute;
			builderInfo.htmlAttrProps = htmlAttrProps as HtmlAttributeProps;
			builderInfo.search = search;
			this.currentInfo = builderInfo;
		}

		// Token: 0x0600360E RID: 13838 RVA: 0x0012EDF4 File Offset: 0x0012CFF4
		private void BeginNamespace(string name, string nspace)
		{
			bool flag = false;
			if (Ref.Equal(name, this.atoms.Empty))
			{
				if (!Ref.Equal(nspace, this.scopeManager.DefaultNamespace) && !Ref.Equal(this.mainNode.NamespaceURI, this.atoms.Empty))
				{
					this.DeclareNamespace(nspace, name);
				}
			}
			else
			{
				string text = this.scopeManager.ResolveNamespace(name, out flag);
				if (text != null)
				{
					if (!Ref.Equal(nspace, text) && !flag)
					{
						this.DeclareNamespace(nspace, name);
					}
				}
				else
				{
					this.DeclareNamespace(nspace, name);
				}
			}
			this.currentInfo = this.dummy;
			this.currentInfo.NodeType = XmlNodeType.Attribute;
		}

		// Token: 0x0600360F RID: 13839 RVA: 0x0012EE98 File Offset: 0x0012D098
		private bool BeginProcessingInstruction(string prefix, string name, string nspace)
		{
			this.currentInfo.NodeType = XmlNodeType.ProcessingInstruction;
			this.currentInfo.Prefix = prefix;
			this.currentInfo.LocalName = name;
			this.currentInfo.NamespaceURI = nspace;
			this.currentInfo.Depth = this.recordDepth;
			return true;
		}

		// Token: 0x06003610 RID: 13840 RVA: 0x0012EEE7 File Offset: 0x0012D0E7
		private void BeginComment()
		{
			this.currentInfo.NodeType = XmlNodeType.Comment;
			this.currentInfo.Depth = this.recordDepth;
		}

		// Token: 0x06003611 RID: 13841 RVA: 0x0012EF08 File Offset: 0x0012D108
		private void AdjustDepth(int state)
		{
			int num = state & 768;
			if (num == 256)
			{
				this.recordDepth++;
				return;
			}
			if (num != 512)
			{
				return;
			}
			this.recordDepth--;
		}

		// Token: 0x06003612 RID: 13842 RVA: 0x0012EF4C File Offset: 0x0012D14C
		private void ResetRecord(int state)
		{
			if ((state & 8192) != 0)
			{
				this.attributeCount = 0;
				this.namespaceCount = 0;
				this.currentInfo = this.mainNode;
				this.currentInfo.Initialize(this.atoms.Empty, this.atoms.Empty, this.atoms.Empty);
				this.currentInfo.NodeType = XmlNodeType.None;
				this.currentInfo.IsEmptyTag = false;
				this.currentInfo.htmlProps = null;
				this.currentInfo.htmlAttrProps = null;
			}
		}

		// Token: 0x06003613 RID: 13843 RVA: 0x0012EFD8 File Offset: 0x0012D1D8
		private void PopElementScope()
		{
			if (this.popScope)
			{
				this.scopeManager.PopScope();
				this.popScope = false;
			}
		}

		// Token: 0x06003614 RID: 13844 RVA: 0x0012EFF4 File Offset: 0x0012D1F4
		private Processor.OutputResult CheckRecordBegin(int state)
		{
			if ((state & 16384) != 0)
			{
				this.recordState = 2;
				this.FinalizeRecord();
				this.SetEmptyFlag(state);
				return this.output.RecordDone(this);
			}
			this.recordState = 1;
			return Processor.OutputResult.Continue;
		}

		// Token: 0x06003615 RID: 13845 RVA: 0x0012F028 File Offset: 0x0012D228
		private Processor.OutputResult CheckRecordEnd(int state)
		{
			if ((state & 16384) != 0)
			{
				this.recordState = 2;
				this.FinalizeRecord();
				this.SetEmptyFlag(state);
				return this.output.RecordDone(this);
			}
			return Processor.OutputResult.Continue;
		}

		// Token: 0x06003616 RID: 13846 RVA: 0x0012F055 File Offset: 0x0012D255
		private void SetEmptyFlag(int state)
		{
			if ((state & 1024) != 0)
			{
				this.mainNode.IsEmptyTag = false;
			}
		}

		// Token: 0x06003617 RID: 13847 RVA: 0x0012F06C File Offset: 0x0012D26C
		private void AnalyzeSpaceLang()
		{
			for (int i = 0; i < this.attributeCount; i++)
			{
				BuilderInfo builderInfo = (BuilderInfo)this.attributeList[i];
				if (Ref.Equal(builderInfo.Prefix, this.atoms.Xml))
				{
					OutputScope currentElementScope = this.scopeManager.CurrentElementScope;
					if (Ref.Equal(builderInfo.LocalName, this.atoms.Lang))
					{
						currentElementScope.Lang = builderInfo.Value;
					}
					else if (Ref.Equal(builderInfo.LocalName, this.atoms.Space))
					{
						currentElementScope.Space = RecordBuilder.TranslateXmlSpace(builderInfo.Value);
					}
				}
			}
		}

		// Token: 0x06003618 RID: 13848 RVA: 0x0012F118 File Offset: 0x0012D318
		private void FixupElement()
		{
			if (Ref.Equal(this.mainNode.NamespaceURI, this.atoms.Empty))
			{
				this.mainNode.Prefix = this.atoms.Empty;
			}
			if (Ref.Equal(this.mainNode.Prefix, this.atoms.Empty))
			{
				if (!Ref.Equal(this.mainNode.NamespaceURI, this.scopeManager.DefaultNamespace))
				{
					this.DeclareNamespace(this.mainNode.NamespaceURI, this.mainNode.Prefix);
				}
			}
			else
			{
				bool flag = false;
				string text = this.scopeManager.ResolveNamespace(this.mainNode.Prefix, out flag);
				if (text != null)
				{
					if (!Ref.Equal(this.mainNode.NamespaceURI, text))
					{
						if (flag)
						{
							this.mainNode.Prefix = this.GetPrefixForNamespace(this.mainNode.NamespaceURI);
						}
						else
						{
							this.DeclareNamespace(this.mainNode.NamespaceURI, this.mainNode.Prefix);
						}
					}
				}
				else
				{
					this.DeclareNamespace(this.mainNode.NamespaceURI, this.mainNode.Prefix);
				}
			}
			this.scopeManager.CurrentElementScope.Prefix = this.mainNode.Prefix;
		}

		// Token: 0x06003619 RID: 13849 RVA: 0x0012F260 File Offset: 0x0012D460
		private void FixupAttributes(int attributeCount)
		{
			for (int i = 0; i < attributeCount; i++)
			{
				BuilderInfo builderInfo = (BuilderInfo)this.attributeList[i];
				if (Ref.Equal(builderInfo.NamespaceURI, this.atoms.Empty))
				{
					builderInfo.Prefix = this.atoms.Empty;
				}
				else if (Ref.Equal(builderInfo.Prefix, this.atoms.Empty))
				{
					builderInfo.Prefix = this.GetPrefixForNamespace(builderInfo.NamespaceURI);
				}
				else
				{
					bool flag = false;
					string text = this.scopeManager.ResolveNamespace(builderInfo.Prefix, out flag);
					if (text != null)
					{
						if (!Ref.Equal(builderInfo.NamespaceURI, text))
						{
							if (flag)
							{
								builderInfo.Prefix = this.GetPrefixForNamespace(builderInfo.NamespaceURI);
							}
							else
							{
								this.DeclareNamespace(builderInfo.NamespaceURI, builderInfo.Prefix);
							}
						}
					}
					else
					{
						this.DeclareNamespace(builderInfo.NamespaceURI, builderInfo.Prefix);
					}
				}
			}
		}

		// Token: 0x0600361A RID: 13850 RVA: 0x0012F350 File Offset: 0x0012D550
		private void AppendNamespaces()
		{
			for (int i = this.namespaceCount - 1; i >= 0; i--)
			{
				((BuilderInfo)this.attributeList[this.NewAttribute()]).Initialize((BuilderInfo)this.namespaceList[i]);
			}
		}

		// Token: 0x0600361B RID: 13851 RVA: 0x0012F39C File Offset: 0x0012D59C
		private void AnalyzeComment()
		{
			StringBuilder stringBuilder = null;
			string value = this.mainNode.Value;
			bool flag = false;
			int i = 0;
			int num = 0;
			while (i < value.Length)
			{
				char c = value[i];
				if (c == '-')
				{
					if (flag)
					{
						if (stringBuilder == null)
						{
							stringBuilder = new StringBuilder(value, num, i, 2 * value.Length);
						}
						else
						{
							stringBuilder.Append(value, num, i - num);
						}
						stringBuilder.Append(" -");
						num = i + 1;
					}
					flag = true;
				}
				else
				{
					flag = false;
				}
				i++;
			}
			if (stringBuilder != null)
			{
				if (num < value.Length)
				{
					stringBuilder.Append(value, num, value.Length - num);
				}
				if (flag)
				{
					stringBuilder.Append(" ");
				}
				this.mainNode.Value = stringBuilder.ToString();
				return;
			}
			if (flag)
			{
				this.mainNode.ValueAppend(" ", false);
			}
		}

		// Token: 0x0600361C RID: 13852 RVA: 0x0012F470 File Offset: 0x0012D670
		private void AnalyzeProcessingInstruction()
		{
			StringBuilder stringBuilder = null;
			string value = this.mainNode.Value;
			bool flag = false;
			int i = 0;
			int num = 0;
			while (i < value.Length)
			{
				char c = value[i];
				if (c != '>')
				{
					flag = c == '?';
				}
				else
				{
					if (flag)
					{
						if (stringBuilder == null)
						{
							stringBuilder = new StringBuilder(value, num, i, 2 * value.Length);
						}
						else
						{
							stringBuilder.Append(value, num, i - num);
						}
						stringBuilder.Append(" >");
						num = i + 1;
					}
					flag = false;
				}
				i++;
			}
			if (stringBuilder != null)
			{
				if (num < value.Length)
				{
					stringBuilder.Append(value, num, value.Length - num);
				}
				this.mainNode.Value = stringBuilder.ToString();
			}
		}

		// Token: 0x0600361D RID: 13853 RVA: 0x0012F52C File Offset: 0x0012D72C
		private void FinalizeRecord()
		{
			XmlNodeType nodeType = this.mainNode.NodeType;
			if (nodeType == XmlNodeType.Element)
			{
				int num = this.attributeCount;
				this.FixupElement();
				this.FixupAttributes(num);
				this.AnalyzeSpaceLang();
				this.AppendNamespaces();
				return;
			}
			if (nodeType == XmlNodeType.ProcessingInstruction)
			{
				this.AnalyzeProcessingInstruction();
				return;
			}
			if (nodeType != XmlNodeType.Comment)
			{
				return;
			}
			this.AnalyzeComment();
		}

		// Token: 0x0600361E RID: 13854 RVA: 0x0012F580 File Offset: 0x0012D780
		private int NewNamespace()
		{
			if (this.namespaceCount >= this.namespaceList.Count)
			{
				this.namespaceList.Add(new BuilderInfo());
			}
			int num = this.namespaceCount;
			this.namespaceCount = num + 1;
			return num;
		}

		// Token: 0x0600361F RID: 13855 RVA: 0x0012F5C4 File Offset: 0x0012D7C4
		private void DeclareNamespace(string nspace, string prefix)
		{
			int num = this.NewNamespace();
			BuilderInfo builderInfo = (BuilderInfo)this.namespaceList[num];
			if (prefix == this.atoms.Empty)
			{
				builderInfo.Initialize(this.atoms.Empty, this.atoms.Xmlns, this.atoms.XmlnsNamespace);
			}
			else
			{
				builderInfo.Initialize(this.atoms.Xmlns, prefix, this.atoms.XmlnsNamespace);
			}
			builderInfo.Depth = this.recordDepth;
			builderInfo.NodeType = XmlNodeType.Attribute;
			builderInfo.Value = nspace;
			this.scopeManager.PushNamespace(prefix, nspace);
		}

		// Token: 0x06003620 RID: 13856 RVA: 0x0012F66C File Offset: 0x0012D86C
		private string DeclareNewNamespace(string nspace)
		{
			string text = this.scopeManager.GeneratePrefix("xp_{0}");
			this.DeclareNamespace(nspace, text);
			return text;
		}

		// Token: 0x06003621 RID: 13857 RVA: 0x0012F694 File Offset: 0x0012D894
		internal string GetPrefixForNamespace(string nspace)
		{
			string text = null;
			if (this.scopeManager.FindPrefix(nspace, out text))
			{
				return text;
			}
			return this.DeclareNewNamespace(nspace);
		}

		// Token: 0x06003622 RID: 13858 RVA: 0x0012F6BC File Offset: 0x0012D8BC
		private static XmlSpace TranslateXmlSpace(string space)
		{
			if (space == "default")
			{
				return XmlSpace.Default;
			}
			if (space == "preserve")
			{
				return XmlSpace.Preserve;
			}
			return XmlSpace.None;
		}

		// Token: 0x04002260 RID: 8800
		private int outputState;

		// Token: 0x04002261 RID: 8801
		private RecordBuilder next;

		// Token: 0x04002262 RID: 8802
		private RecordOutput output;

		// Token: 0x04002263 RID: 8803
		private XmlNameTable nameTable;

		// Token: 0x04002264 RID: 8804
		private OutKeywords atoms;

		// Token: 0x04002265 RID: 8805
		private OutputScopeManager scopeManager;

		// Token: 0x04002266 RID: 8806
		private BuilderInfo mainNode = new BuilderInfo();

		// Token: 0x04002267 RID: 8807
		private ArrayList attributeList = new ArrayList();

		// Token: 0x04002268 RID: 8808
		private int attributeCount;

		// Token: 0x04002269 RID: 8809
		private ArrayList namespaceList = new ArrayList();

		// Token: 0x0400226A RID: 8810
		private int namespaceCount;

		// Token: 0x0400226B RID: 8811
		private BuilderInfo dummy = new BuilderInfo();

		// Token: 0x0400226C RID: 8812
		private BuilderInfo currentInfo;

		// Token: 0x0400226D RID: 8813
		private bool popScope;

		// Token: 0x0400226E RID: 8814
		private int recordState;

		// Token: 0x0400226F RID: 8815
		private int recordDepth;

		// Token: 0x04002270 RID: 8816
		private const int NoRecord = 0;

		// Token: 0x04002271 RID: 8817
		private const int SomeRecord = 1;

		// Token: 0x04002272 RID: 8818
		private const int HaveRecord = 2;

		// Token: 0x04002273 RID: 8819
		private const char s_Minus = '-';

		// Token: 0x04002274 RID: 8820
		private const string s_Space = " ";

		// Token: 0x04002275 RID: 8821
		private const string s_SpaceMinus = " -";

		// Token: 0x04002276 RID: 8822
		private const char s_Question = '?';

		// Token: 0x04002277 RID: 8823
		private const char s_Greater = '>';

		// Token: 0x04002278 RID: 8824
		private const string s_SpaceGreater = " >";

		// Token: 0x04002279 RID: 8825
		private const string PrefixFormat = "xp_{0}";
	}
}
