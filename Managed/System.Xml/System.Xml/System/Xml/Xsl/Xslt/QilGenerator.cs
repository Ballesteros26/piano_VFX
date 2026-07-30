using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Text;
using System.Xml.Schema;
using System.Xml.XPath;
using System.Xml.Xsl.Qil;
using System.Xml.Xsl.Runtime;
using System.Xml.Xsl.XPath;

namespace System.Xml.Xsl.Xslt
{
	// Token: 0x02000584 RID: 1412
	internal class QilGenerator : IErrorHelper, IXPathEnvironment, IFocus
	{
		// Token: 0x060037D4 RID: 14292 RVA: 0x00136F86 File Offset: 0x00135186
		public static QilExpression CompileStylesheet(Compiler compiler)
		{
			return new QilGenerator(compiler.IsDebug).Compile(compiler);
		}

		// Token: 0x060037D5 RID: 14293 RVA: 0x00136F9C File Offset: 0x0013519C
		private QilGenerator(bool debug)
		{
			this.scope = new CompilerScopeManager<QilIterator>();
			this.outputScope = new OutputScopeManager();
			this.prefixesInUse = new HybridDictionary();
			this.f = new XsltQilFactory(new QilFactory(), debug);
			this.xpathBuilder = new XPathBuilder(this);
			this.xpathParser = new XPathParser<QilNode>();
			this.ptrnBuilder = new XPathPatternBuilder(this);
			this.ptrnParser = new XPathPatternParser();
			this.refReplacer = new ReferenceReplacer(this.f.BaseFactory);
			this.invkGen = new InvokeGenerator(this.f, debug);
			this.matcherBuilder = new MatcherBuilder(this.f, this.refReplacer, this.invkGen);
			this.singlFocus = new SingletonFocus(this.f);
			this.funcFocus = default(FunctionFocus);
			this.curLoop = new LoopFocus(this.f);
			this.strConcat = new QilStrConcatenator(this.f);
			this.varHelper = new QilGenerator.VariableHelper(this.f);
			this.elementOrDocumentType = XmlQueryTypeFactory.DocumentOrElement;
			this.textOrAttributeType = XmlQueryTypeFactory.NodeChoice(XmlNodeKindFlags.Attribute | XmlNodeKindFlags.Text);
			this.nameCurrent = this.f.QName("current", "urn:schemas-microsoft-com:xslt-debug");
			this.namePosition = this.f.QName("position", "urn:schemas-microsoft-com:xslt-debug");
			this.nameLast = this.f.QName("last", "urn:schemas-microsoft-com:xslt-debug");
			this.nameNamespaces = this.f.QName("namespaces", "urn:schemas-microsoft-com:xslt-debug");
			this.nameInit = this.f.QName("init", "urn:schemas-microsoft-com:xslt-debug");
			this.formatterCnt = 0;
		}

		// Token: 0x17000BBD RID: 3005
		// (get) Token: 0x060037D6 RID: 14294 RVA: 0x0013716A File Offset: 0x0013536A
		private bool IsDebug
		{
			get
			{
				return this.compiler.IsDebug;
			}
		}

		// Token: 0x17000BBE RID: 3006
		// (get) Token: 0x060037D7 RID: 14295 RVA: 0x00137177 File Offset: 0x00135377
		private bool EvaluateFuncCalls
		{
			get
			{
				return !this.IsDebug;
			}
		}

		// Token: 0x17000BBF RID: 3007
		// (get) Token: 0x060037D8 RID: 14296 RVA: 0x00137177 File Offset: 0x00135377
		private bool InferXPathTypes
		{
			get
			{
				return !this.IsDebug;
			}
		}

		// Token: 0x060037D9 RID: 14297 RVA: 0x00137184 File Offset: 0x00135384
		private QilExpression Compile(Compiler compiler)
		{
			this.compiler = compiler;
			this.functions = this.f.FunctionList();
			this.extPars = this.f.GlobalParameterList();
			this.gloVars = this.f.GlobalVariableList();
			this.nsVars = this.f.GlobalVariableList();
			compiler.Scripts.CompileScripts();
			new XslAstRewriter().Rewrite(compiler);
			if (!this.IsDebug)
			{
				new XslAstAnalyzer().Analyze(compiler);
			}
			this.CreateGlobalVarPars();
			try
			{
				this.CompileKeys();
				this.CompileAndSortMatches(compiler.Root.Imports[0]);
				this.PrecompileProtoTemplatesHeaders();
				this.CompileGlobalVariables();
				foreach (ProtoTemplate protoTemplate in compiler.AllTemplates)
				{
					this.CompileProtoTemplate(protoTemplate);
				}
			}
			catch (XslLoadException ex)
			{
				ex.SetSourceLineInfo(this.lastScope.SourceLine);
				throw;
			}
			catch (Exception ex2)
			{
				if (!XmlException.IsCatchableException(ex2))
				{
					throw;
				}
				throw new XslLoadException(ex2, this.lastScope.SourceLine);
			}
			this.CompileInitializationCode();
			QilNode qilNode = this.CompileRootExpression(compiler.StartApplyTemplates);
			foreach (ProtoTemplate protoTemplate2 in compiler.AllTemplates)
			{
				foreach (QilNode qilNode2 in protoTemplate2.Function.Arguments)
				{
					QilParameter qilParameter = (QilParameter)qilNode2;
					if (!this.IsDebug || qilParameter.Name.Equals(this.nameNamespaces))
					{
						qilParameter.DefaultValue = null;
					}
				}
			}
			Dictionary<string, Type> scriptClasses = compiler.Scripts.ScriptClasses;
			List<EarlyBoundInfo> list = new List<EarlyBoundInfo>(scriptClasses.Count);
			foreach (KeyValuePair<string, Type> keyValuePair in scriptClasses)
			{
				if (keyValuePair.Value != null)
				{
					list.Add(new EarlyBoundInfo(keyValuePair.Key, keyValuePair.Value));
				}
			}
			QilExpression qilExpression = this.f.QilExpression(qilNode, this.f.BaseFactory);
			qilExpression.EarlyBoundTypes = list;
			qilExpression.FunctionList = this.functions;
			qilExpression.GlobalParameterList = this.extPars;
			qilExpression.GlobalVariableList = this.gloVars;
			qilExpression.WhitespaceRules = compiler.WhitespaceRules;
			qilExpression.IsDebug = this.IsDebug;
			qilExpression.DefaultWriterSettings = compiler.Output.Settings;
			QilDepthChecker.Check(qilExpression);
			return qilExpression;
		}

		// Token: 0x060037DA RID: 14298 RVA: 0x0013745C File Offset: 0x0013565C
		private QilNode InvokeOnCurrentNodeChanged()
		{
			return this.f.Loop(this.f.Let(this.f.InvokeOnCurrentNodeChanged(this.curLoop.GetCurrent())), this.f.Sequence());
		}

		// Token: 0x060037DB RID: 14299 RVA: 0x00002F50 File Offset: 0x00001150
		[Conditional("DEBUG")]
		private void CheckSingletonFocus()
		{
		}

		// Token: 0x060037DC RID: 14300 RVA: 0x001374A4 File Offset: 0x001356A4
		private void CompileInitializationCode()
		{
			QilNode qilNode = this.f.Int32(0);
			if (this.formatNumberDynamicUsed || this.IsDebug)
			{
				bool flag = false;
				foreach (DecimalFormatDecl decimalFormatDecl in this.compiler.DecimalFormats)
				{
					qilNode = this.f.Add(qilNode, this.f.InvokeRegisterDecimalFormat(decimalFormatDecl));
					flag |= decimalFormatDecl.Name == DecimalFormatDecl.Default.Name;
				}
				if (!flag)
				{
					qilNode = this.f.Add(qilNode, this.f.InvokeRegisterDecimalFormat(DecimalFormatDecl.Default));
				}
			}
			foreach (string text in this.compiler.Scripts.ScriptClasses.Keys)
			{
				qilNode = this.f.Add(qilNode, this.f.InvokeCheckScriptNamespace(text));
			}
			if (qilNode.NodeType == QilNodeType.Add)
			{
				QilFunction qilFunction = this.f.Function(this.f.FormalParameterList(), qilNode, this.f.True());
				qilFunction.DebugName = "Init";
				this.functions.Add(qilFunction);
				QilNode qilNode2 = this.f.Invoke(qilFunction, this.f.ActualParameterList());
				if (this.IsDebug)
				{
					qilNode2 = this.f.TypeAssert(qilNode2, XmlQueryTypeFactory.ItemS);
				}
				QilIterator qilIterator = this.f.Let(qilNode2);
				qilIterator.DebugName = this.nameInit.ToString();
				this.gloVars.Insert(0, qilIterator);
			}
		}

		// Token: 0x060037DD RID: 14301 RVA: 0x00137678 File Offset: 0x00135878
		private QilNode CompileRootExpression(XslNode applyTmpls)
		{
			this.singlFocus.SetFocus(SingletonFocusType.InitialContextNode);
			QilNode qilNode = this.GenerateApply(this.compiler.Root, applyTmpls);
			this.singlFocus.SetFocus(null);
			return this.f.DocumentCtor(qilNode);
		}

		// Token: 0x060037DE RID: 14302 RVA: 0x001376BC File Offset: 0x001358BC
		private QilList EnterScope(XslNode node)
		{
			this.lastScope = node;
			this.xslVersion = node.XslVersion;
			if (this.scope.EnterScope(node.Namespaces))
			{
				return this.BuildDebuggerNamespaces();
			}
			return null;
		}

		// Token: 0x060037DF RID: 14303 RVA: 0x001376EC File Offset: 0x001358EC
		private void ExitScope()
		{
			this.scope.ExitScope();
		}

		// Token: 0x060037E0 RID: 14304 RVA: 0x001376FC File Offset: 0x001358FC
		private QilList BuildDebuggerNamespaces()
		{
			if (this.IsDebug)
			{
				QilList qilList = this.f.BaseFactory.Sequence();
				foreach (CompilerScopeManager<QilIterator>.ScopeRecord scopeRecord in this.scope)
				{
					qilList.Add(this.f.NamespaceDecl(this.f.String(scopeRecord.ncName), this.f.String(scopeRecord.nsUri)));
				}
				return qilList;
			}
			return null;
		}

		// Token: 0x060037E1 RID: 14305 RVA: 0x00137777 File Offset: 0x00135977
		private QilNode GetCurrentNode()
		{
			if (this.curLoop.IsFocusSet)
			{
				return this.curLoop.GetCurrent();
			}
			if (this.funcFocus.IsFocusSet)
			{
				return this.funcFocus.GetCurrent();
			}
			return this.singlFocus.GetCurrent();
		}

		// Token: 0x060037E2 RID: 14306 RVA: 0x001377B6 File Offset: 0x001359B6
		private QilNode GetCurrentPosition()
		{
			if (this.curLoop.IsFocusSet)
			{
				return this.curLoop.GetPosition();
			}
			if (this.funcFocus.IsFocusSet)
			{
				return this.funcFocus.GetPosition();
			}
			return this.singlFocus.GetPosition();
		}

		// Token: 0x060037E3 RID: 14307 RVA: 0x001377F5 File Offset: 0x001359F5
		private QilNode GetLastPosition()
		{
			if (this.curLoop.IsFocusSet)
			{
				return this.curLoop.GetLast();
			}
			if (this.funcFocus.IsFocusSet)
			{
				return this.funcFocus.GetLast();
			}
			return this.singlFocus.GetLast();
		}

		// Token: 0x060037E4 RID: 14308 RVA: 0x00137834 File Offset: 0x00135A34
		private XmlQueryType ChooseBestType(VarPar var)
		{
			if (this.IsDebug || !this.InferXPathTypes)
			{
				return XmlQueryTypeFactory.ItemS;
			}
			XslFlags xslFlags = var.Flags & XslFlags.TypeFilter;
			if (xslFlags <= (XslFlags.Node | XslFlags.Nodeset))
			{
				if (xslFlags <= XslFlags.Node)
				{
					switch (xslFlags)
					{
					case XslFlags.String:
						return XmlQueryTypeFactory.StringX;
					case XslFlags.Number:
						return XmlQueryTypeFactory.DoubleX;
					case XslFlags.String | XslFlags.Number:
						break;
					case XslFlags.Boolean:
						return XmlQueryTypeFactory.BooleanX;
					default:
						if (xslFlags == XslFlags.Node)
						{
							return XmlQueryTypeFactory.NodeNotRtf;
						}
						break;
					}
				}
				else
				{
					if (xslFlags == XslFlags.Nodeset)
					{
						return XmlQueryTypeFactory.NodeNotRtfS;
					}
					if (xslFlags == (XslFlags.Node | XslFlags.Nodeset))
					{
						return XmlQueryTypeFactory.NodeNotRtfS;
					}
				}
			}
			else if (xslFlags <= (XslFlags.Node | XslFlags.Rtf))
			{
				if (xslFlags == XslFlags.Rtf)
				{
					return XmlQueryTypeFactory.Node;
				}
				if (xslFlags == (XslFlags.Node | XslFlags.Rtf))
				{
					return XmlQueryTypeFactory.Node;
				}
			}
			else
			{
				if (xslFlags == (XslFlags.Nodeset | XslFlags.Rtf))
				{
					return XmlQueryTypeFactory.NodeS;
				}
				if (xslFlags == (XslFlags.Node | XslFlags.Nodeset | XslFlags.Rtf))
				{
					return XmlQueryTypeFactory.NodeS;
				}
			}
			return XmlQueryTypeFactory.ItemS;
		}

		// Token: 0x060037E5 RID: 14309 RVA: 0x001378F4 File Offset: 0x00135AF4
		private QilIterator GetNsVar(QilList nsList)
		{
			foreach (QilNode qilNode in this.nsVars)
			{
				QilIterator qilIterator = (QilIterator)qilNode;
				QilList qilList = (QilList)qilIterator.Binding;
				if (qilList.Count == nsList.Count)
				{
					bool flag = true;
					for (int i = 0; i < nsList.Count; i++)
					{
						if (((QilLiteral)((QilBinary)nsList[i]).Right).Value != ((QilLiteral)((QilBinary)qilList[i]).Right).Value || ((QilLiteral)((QilBinary)nsList[i]).Left).Value != ((QilLiteral)((QilBinary)qilList[i]).Left).Value)
						{
							flag = false;
							break;
						}
					}
					if (flag)
					{
						return qilIterator;
					}
				}
			}
			QilIterator qilIterator2 = this.f.Let(nsList);
			qilIterator2.DebugName = this.f.QName("ns" + this.nsVars.Count, "urn:schemas-microsoft-com:xslt-debug").ToString();
			this.gloVars.Add(qilIterator2);
			this.nsVars.Add(qilIterator2);
			return qilIterator2;
		}

		// Token: 0x060037E6 RID: 14310 RVA: 0x00137A60 File Offset: 0x00135C60
		private void PrecompileProtoTemplatesHeaders()
		{
			List<VarPar> list = null;
			Dictionary<VarPar, Template> dictionary = null;
			Dictionary<VarPar, QilFunction> dictionary2 = null;
			foreach (ProtoTemplate protoTemplate in this.compiler.AllTemplates)
			{
				QilList qilList = this.f.FormalParameterList();
				XslFlags xslFlags = ((!this.IsDebug) ? protoTemplate.Flags : XslFlags.FocusFilter);
				QilList qilList2 = this.EnterScope(protoTemplate);
				if ((xslFlags & XslFlags.Current) != XslFlags.None)
				{
					qilList.Add(this.CreateXslParam(this.CloneName(this.nameCurrent), XmlQueryTypeFactory.NodeNotRtf));
				}
				if ((xslFlags & XslFlags.Position) != XslFlags.None)
				{
					qilList.Add(this.CreateXslParam(this.CloneName(this.namePosition), XmlQueryTypeFactory.DoubleX));
				}
				if ((xslFlags & XslFlags.Last) != XslFlags.None)
				{
					qilList.Add(this.CreateXslParam(this.CloneName(this.nameLast), XmlQueryTypeFactory.DoubleX));
				}
				if (this.IsDebug && qilList2 != null)
				{
					QilParameter qilParameter = this.CreateXslParam(this.CloneName(this.nameNamespaces), XmlQueryTypeFactory.NamespaceS);
					qilParameter.DefaultValue = this.GetNsVar(qilList2);
					qilList.Add(qilParameter);
				}
				Template template = protoTemplate as Template;
				if (template != null)
				{
					this.funcFocus.StartFocus(qilList, xslFlags);
					for (int i = 0; i < protoTemplate.Content.Count; i++)
					{
						XslNode xslNode = protoTemplate.Content[i];
						if (xslNode.NodeType != XslNodeType.Text)
						{
							if (xslNode.NodeType != XslNodeType.Param)
							{
								break;
							}
							VarPar varPar = (VarPar)xslNode;
							this.EnterScope(varPar);
							if (this.scope.IsLocalVariable(varPar.Name.LocalName, varPar.Name.NamespaceUri))
							{
								this.ReportError("The variable or parameter '{0}' was duplicated within the same scope.", new string[] { varPar.Name.QualifiedName });
							}
							QilParameter qilParameter2 = this.CreateXslParam(varPar.Name, this.ChooseBestType(varPar));
							if (this.IsDebug)
							{
								qilParameter2.Annotation = varPar;
							}
							else if ((varPar.DefValueFlags & XslFlags.HasCalls) == XslFlags.None)
							{
								qilParameter2.DefaultValue = this.CompileVarParValue(varPar);
							}
							else
							{
								QilList qilList3 = this.f.FormalParameterList();
								QilList qilList4 = this.f.ActualParameterList();
								for (int j = 0; j < qilList.Count; j++)
								{
									QilParameter qilParameter3 = this.f.Parameter(qilList[j].XmlType);
									qilParameter3.DebugName = ((QilParameter)qilList[j]).DebugName;
									qilParameter3.Name = this.CloneName(((QilParameter)qilList[j]).Name);
									QilGenerator.SetLineInfo(qilParameter3, qilList[j].SourceLine);
									qilList3.Add(qilParameter3);
									qilList4.Add(qilList[j]);
								}
								varPar.Flags |= template.Flags & XslFlags.FocusFilter;
								QilFunction qilFunction = this.f.Function(qilList3, this.f.Boolean((varPar.DefValueFlags & XslFlags.SideEffects) > XslFlags.None), this.ChooseBestType(varPar));
								qilFunction.SourceLine = SourceLineInfo.NoSource;
								qilFunction.DebugName = "<xsl:param name=\"" + varPar.Name.QualifiedName + "\">";
								qilParameter2.DefaultValue = this.f.Invoke(qilFunction, qilList4);
								if (list == null)
								{
									list = new List<VarPar>();
									dictionary = new Dictionary<VarPar, Template>();
									dictionary2 = new Dictionary<VarPar, QilFunction>();
								}
								list.Add(varPar);
								dictionary.Add(varPar, template);
								dictionary2.Add(varPar, qilFunction);
							}
							QilGenerator.SetLineInfo(qilParameter2, varPar.SourceLine);
							this.ExitScope();
							this.scope.AddVariable(varPar.Name, qilParameter2);
							qilList.Add(qilParameter2);
						}
					}
					this.funcFocus.StopFocus();
				}
				this.ExitScope();
				protoTemplate.Function = this.f.Function(qilList, this.f.Boolean((protoTemplate.Flags & XslFlags.SideEffects) > XslFlags.None), (protoTemplate is AttributeSet) ? XmlQueryTypeFactory.AttributeS : XmlQueryTypeFactory.NodeNotRtfS);
				protoTemplate.Function.DebugName = protoTemplate.GetDebugName();
				QilGenerator.SetLineInfo(protoTemplate.Function, protoTemplate.SourceLine ?? SourceLineInfo.NoSource);
				this.functions.Add(protoTemplate.Function);
			}
			if (list != null)
			{
				foreach (VarPar varPar2 in list)
				{
					Template template2 = dictionary[varPar2];
					QilFunction qilFunction2 = dictionary2[varPar2];
					this.funcFocus.StartFocus(qilFunction2.Arguments, varPar2.Flags);
					this.EnterScope(template2);
					this.EnterScope(varPar2);
					foreach (QilNode qilNode in qilFunction2.Arguments)
					{
						QilParameter qilParameter4 = (QilParameter)qilNode;
						this.scope.AddVariable(qilParameter4.Name, qilParameter4);
					}
					qilFunction2.Definition = this.CompileVarParValue(varPar2);
					QilGenerator.SetLineInfo(qilFunction2.Definition, varPar2.SourceLine);
					this.ExitScope();
					this.ExitScope();
					this.funcFocus.StopFocus();
					this.functions.Add(qilFunction2);
				}
			}
		}

		// Token: 0x060037E7 RID: 14311 RVA: 0x00138040 File Offset: 0x00136240
		private QilParameter CreateXslParam(QilName name, XmlQueryType xt)
		{
			QilParameter qilParameter = this.f.Parameter(xt);
			qilParameter.DebugName = name.ToString();
			qilParameter.Name = name;
			return qilParameter;
		}

		// Token: 0x060037E8 RID: 14312 RVA: 0x00138064 File Offset: 0x00136264
		private void CompileProtoTemplate(ProtoTemplate tmpl)
		{
			this.EnterScope(tmpl);
			this.funcFocus.StartFocus(tmpl.Function.Arguments, (!this.IsDebug) ? tmpl.Flags : XslFlags.FocusFilter);
			foreach (QilNode qilNode in tmpl.Function.Arguments)
			{
				QilParameter qilParameter = (QilParameter)qilNode;
				if (qilParameter.Name.NamespaceUri != "urn:schemas-microsoft-com:xslt-debug")
				{
					if (this.IsDebug)
					{
						VarPar varPar = (VarPar)qilParameter.Annotation;
						QilList qilList = this.EnterScope(varPar);
						qilParameter.DefaultValue = this.CompileVarParValue(varPar);
						this.ExitScope();
						qilParameter.DefaultValue = this.SetDebugNs(qilParameter.DefaultValue, qilList);
					}
					this.scope.AddVariable(qilParameter.Name, qilParameter);
				}
			}
			tmpl.Function.Definition = this.CompileInstructions(tmpl.Content);
			this.funcFocus.StopFocus();
			this.ExitScope();
		}

		// Token: 0x060037E9 RID: 14313 RVA: 0x0013817C File Offset: 0x0013637C
		private QilList InstructionList()
		{
			return this.f.BaseFactory.Sequence();
		}

		// Token: 0x060037EA RID: 14314 RVA: 0x0013818E File Offset: 0x0013638E
		private QilNode CompileInstructions(IList<XslNode> instructions)
		{
			return this.CompileInstructions(instructions, 0, this.InstructionList());
		}

		// Token: 0x060037EB RID: 14315 RVA: 0x0013819E File Offset: 0x0013639E
		private QilNode CompileInstructions(IList<XslNode> instructions, int from)
		{
			return this.CompileInstructions(instructions, from, this.InstructionList());
		}

		// Token: 0x060037EC RID: 14316 RVA: 0x001381AE File Offset: 0x001363AE
		private QilNode CompileInstructions(IList<XslNode> instructions, QilList content)
		{
			return this.CompileInstructions(instructions, 0, content);
		}

		// Token: 0x060037ED RID: 14317 RVA: 0x001381BC File Offset: 0x001363BC
		private QilNode CompileInstructions(IList<XslNode> instructions, int from, QilList content)
		{
			for (int i = from; i < instructions.Count; i++)
			{
				XslNode xslNode = instructions[i];
				XslNodeType nodeType = xslNode.NodeType;
				if (nodeType != XslNodeType.Param)
				{
					QilList qilList = this.EnterScope(xslNode);
					QilNode qilNode;
					switch (nodeType)
					{
					case XslNodeType.ApplyImports:
						qilNode = this.CompileApplyImports(xslNode);
						break;
					case XslNodeType.ApplyTemplates:
						qilNode = this.CompileApplyTemplates((XslNodeEx)xslNode);
						break;
					case XslNodeType.Attribute:
						qilNode = this.CompileAttribute((NodeCtor)xslNode);
						break;
					case XslNodeType.AttributeSet:
					case XslNodeType.Key:
					case XslNodeType.Otherwise:
					case XslNodeType.Param:
					case XslNodeType.Sort:
					case XslNodeType.Template:
						goto IL_01FD;
					case XslNodeType.CallTemplate:
						qilNode = this.CompileCallTemplate((XslNodeEx)xslNode);
						break;
					case XslNodeType.Choose:
						qilNode = this.CompileChoose(xslNode);
						break;
					case XslNodeType.Comment:
						qilNode = this.CompileComment(xslNode);
						break;
					case XslNodeType.Copy:
						qilNode = this.CompileCopy(xslNode);
						break;
					case XslNodeType.CopyOf:
						qilNode = this.CompileCopyOf(xslNode);
						break;
					case XslNodeType.Element:
						qilNode = this.CompileElement((NodeCtor)xslNode);
						break;
					case XslNodeType.Error:
						qilNode = this.CompileError(xslNode);
						break;
					case XslNodeType.ForEach:
						qilNode = this.CompileForEach((XslNodeEx)xslNode);
						break;
					case XslNodeType.If:
						qilNode = this.CompileIf(xslNode);
						break;
					case XslNodeType.List:
						qilNode = this.CompileList(xslNode);
						break;
					case XslNodeType.LiteralAttribute:
						qilNode = this.CompileLiteralAttribute(xslNode);
						break;
					case XslNodeType.LiteralElement:
						qilNode = this.CompileLiteralElement(xslNode);
						break;
					case XslNodeType.Message:
						qilNode = this.CompileMessage(xslNode);
						break;
					case XslNodeType.Nop:
						qilNode = this.CompileNop(xslNode);
						break;
					case XslNodeType.Number:
						qilNode = this.CompileNumber((Number)xslNode);
						break;
					case XslNodeType.PI:
						qilNode = this.CompilePI(xslNode);
						break;
					case XslNodeType.Text:
						qilNode = this.CompileText((Text)xslNode);
						break;
					case XslNodeType.UseAttributeSet:
						qilNode = this.CompileUseAttributeSet(xslNode);
						break;
					case XslNodeType.ValueOf:
						qilNode = this.CompileValueOf(xslNode);
						break;
					case XslNodeType.ValueOfDoe:
						qilNode = this.CompileValueOfDoe(xslNode);
						break;
					case XslNodeType.Variable:
						qilNode = this.CompileVariable(xslNode);
						break;
					default:
						goto IL_01FD;
					}
					IL_0200:
					this.ExitScope();
					if (qilNode.NodeType != QilNodeType.Sequence || qilNode.Count != 0)
					{
						if (nodeType != XslNodeType.LiteralAttribute && nodeType != XslNodeType.UseAttributeSet)
						{
							this.SetLineInfoCheck(qilNode, xslNode.SourceLine);
						}
						qilNode = this.SetDebugNs(qilNode, qilList);
						if (nodeType == XslNodeType.Variable)
						{
							QilIterator qilIterator = this.f.Let(qilNode);
							qilIterator.DebugName = xslNode.Name.ToString();
							this.scope.AddVariable(xslNode.Name, qilIterator);
							qilNode = this.f.Loop(qilIterator, this.CompileInstructions(instructions, i + 1));
							i = instructions.Count;
						}
						content.Add(qilNode);
						goto IL_02A1;
					}
					goto IL_02A1;
					IL_01FD:
					qilNode = null;
					goto IL_0200;
				}
				IL_02A1:;
			}
			if (!this.IsDebug && content.Count == 1)
			{
				return content[0];
			}
			return content;
		}

		// Token: 0x060037EE RID: 14318 RVA: 0x00138494 File Offset: 0x00136694
		private QilNode CompileList(XslNode node)
		{
			return this.CompileInstructions(node.Content);
		}

		// Token: 0x060037EF RID: 14319 RVA: 0x001384A2 File Offset: 0x001366A2
		private QilNode CompileNop(XslNode node)
		{
			return this.f.Nop(this.f.Sequence());
		}

		// Token: 0x060037F0 RID: 14320 RVA: 0x001384BC File Offset: 0x001366BC
		private void AddNsDecl(QilList content, string prefix, string nsUri)
		{
			if (this.outputScope.LookupNamespace(prefix) == nsUri)
			{
				return;
			}
			this.outputScope.AddNamespace(prefix, nsUri);
			content.Add(this.f.NamespaceDecl(this.f.String(prefix), this.f.String(nsUri)));
		}

		// Token: 0x060037F1 RID: 14321 RVA: 0x00138514 File Offset: 0x00136714
		private QilNode CompileLiteralElement(XslNode node)
		{
			bool flag = true;
			QilName name;
			string text;
			string namespaceUri;
			QilList qilList;
			for (;;)
			{
				IL_0002:
				this.prefixesInUse.Clear();
				name = node.Name;
				text = name.Prefix;
				namespaceUri = name.NamespaceUri;
				this.compiler.ApplyNsAliases(ref text, ref namespaceUri);
				if (flag)
				{
					this.prefixesInUse.Add(text, namespaceUri);
				}
				else
				{
					text = name.Prefix;
				}
				this.outputScope.PushScope();
				qilList = this.InstructionList();
				foreach (CompilerScopeManager<QilIterator>.ScopeRecord scopeRecord in this.scope)
				{
					string text2 = scopeRecord.ncName;
					string nsUri = scopeRecord.nsUri;
					if (nsUri != "http://www.w3.org/1999/XSL/Transform" && !this.scope.IsExNamespace(nsUri))
					{
						this.compiler.ApplyNsAliases(ref text2, ref nsUri);
						if (flag)
						{
							if (this.prefixesInUse.Contains(text2))
							{
								if ((string)this.prefixesInUse[text2] != nsUri)
								{
									this.outputScope.PopScope();
									flag = false;
									goto IL_0002;
								}
							}
							else
							{
								this.prefixesInUse.Add(text2, nsUri);
							}
						}
						else
						{
							text2 = scopeRecord.ncName;
						}
						this.AddNsDecl(qilList, text2, nsUri);
					}
				}
				break;
			}
			QilNode qilNode = this.CompileInstructions(node.Content, qilList);
			this.outputScope.PopScope();
			name.Prefix = text;
			name.NamespaceUri = namespaceUri;
			return this.f.ElementCtor(name, qilNode);
		}

		// Token: 0x060037F2 RID: 14322 RVA: 0x00138684 File Offset: 0x00136884
		private QilNode CompileElement(NodeCtor node)
		{
			QilNode qilNode = this.CompileStringAvt(node.NsAvt);
			QilNode qilNode2 = this.CompileStringAvt(node.NameAvt);
			QilNode qilNode3;
			if (qilNode2.NodeType == QilNodeType.LiteralString && (qilNode == null || qilNode.NodeType == QilNodeType.LiteralString))
			{
				string text = (QilLiteral)qilNode2;
				string text2;
				string text3;
				bool flag = this.compiler.ParseQName(text, out text2, out text3, this);
				string text4;
				if (qilNode == null)
				{
					text4 = (flag ? this.ResolvePrefix(false, text2) : this.compiler.CreatePhantomNamespace());
				}
				else
				{
					text4 = (QilLiteral)qilNode;
				}
				qilNode3 = this.f.QName(text3, text4, text2);
			}
			else if (qilNode != null)
			{
				qilNode3 = this.f.StrParseQName(qilNode2, qilNode);
			}
			else
			{
				qilNode3 = this.ResolveQNameDynamic(false, qilNode2);
			}
			this.outputScope.PushScope();
			this.outputScope.InvalidateAllPrefixes();
			QilNode qilNode4 = this.CompileInstructions(node.Content);
			this.outputScope.PopScope();
			return this.f.ElementCtor(qilNode3, qilNode4);
		}

		// Token: 0x060037F3 RID: 14323 RVA: 0x0013877C File Offset: 0x0013697C
		private QilNode CompileLiteralAttribute(XslNode node)
		{
			QilName name = node.Name;
			string prefix = name.Prefix;
			string namespaceUri = name.NamespaceUri;
			if (prefix.Length != 0)
			{
				this.compiler.ApplyNsAliases(ref prefix, ref namespaceUri);
			}
			name.Prefix = prefix;
			name.NamespaceUri = namespaceUri;
			return this.f.AttributeCtor(name, this.CompileTextAvt(node.Select));
		}

		// Token: 0x060037F4 RID: 14324 RVA: 0x001387DC File Offset: 0x001369DC
		private QilNode CompileAttribute(NodeCtor node)
		{
			QilNode qilNode = this.CompileStringAvt(node.NsAvt);
			QilNode qilNode2 = this.CompileStringAvt(node.NameAvt);
			bool flag = false;
			QilNode qilNode3;
			if (qilNode2.NodeType == QilNodeType.LiteralString && (qilNode == null || qilNode.NodeType == QilNodeType.LiteralString))
			{
				string text = (QilLiteral)qilNode2;
				string text2;
				string text3;
				bool flag2 = this.compiler.ParseQName(text, out text2, out text3, this);
				string text4;
				if (qilNode == null)
				{
					text4 = (flag2 ? this.ResolvePrefix(true, text2) : this.compiler.CreatePhantomNamespace());
				}
				else
				{
					text4 = (QilLiteral)qilNode;
					flag = true;
				}
				if (text == "xmlns" || (text3 == "xmlns" && text4.Length == 0))
				{
					this.ReportError("An attribute with a local name 'xmlns' and a null namespace URI cannot be created.", new string[] { "name", text });
				}
				qilNode3 = this.f.QName(text3, text4, text2);
			}
			else if (qilNode != null)
			{
				qilNode3 = this.f.StrParseQName(qilNode2, qilNode);
			}
			else
			{
				qilNode3 = this.ResolveQNameDynamic(true, qilNode2);
			}
			if (flag)
			{
				this.outputScope.InvalidateNonDefaultPrefixes();
			}
			return this.f.AttributeCtor(qilNode3, this.CompileInstructions(node.Content));
		}

		// Token: 0x060037F5 RID: 14325 RVA: 0x0013890C File Offset: 0x00136B0C
		private QilNode ExtractText(string source, ref int pos)
		{
			int num = pos;
			this.unescapedText.Length = 0;
			int i;
			for (i = pos; i < source.Length; i++)
			{
				char c = source[i];
				if (c == '{' || c == '}')
				{
					if (i + 1 < source.Length && source[i + 1] == c)
					{
						i++;
						this.unescapedText.Append(source, num, i - num);
						num = i + 1;
					}
					else
					{
						if (c == '{')
						{
							break;
						}
						pos = source.Length;
						if (this.xslVersion != XslVersion.ForwardsCompatible)
						{
							this.ReportError("The right curly brace in an attribute value template '{0}' outside an expression must be doubled.", new string[] { source });
							return null;
						}
						return this.f.Error(this.lastScope.SourceLine, "The right curly brace in an attribute value template '{0}' outside an expression must be doubled.", new string[] { source });
					}
				}
			}
			pos = i;
			if (this.unescapedText.Length != 0)
			{
				this.unescapedText.Append(source, num, i - num);
				return this.f.String(this.unescapedText.ToString());
			}
			if (i <= num)
			{
				return null;
			}
			return this.f.String(source.Substring(num, i - num));
		}

		// Token: 0x060037F6 RID: 14326 RVA: 0x00138A30 File Offset: 0x00136C30
		private QilNode CompileAvt(string source)
		{
			QilList qilList = this.f.BaseFactory.Sequence();
			int i = 0;
			while (i < source.Length)
			{
				QilNode qilNode = this.ExtractText(source, ref i);
				if (qilNode != null)
				{
					qilList.Add(qilNode);
				}
				if (i < source.Length)
				{
					i++;
					QilNode qilNode2 = this.CompileXPathExpressionWithinAvt(source, ref i);
					qilList.Add(this.f.ConvertToString(qilNode2));
				}
			}
			if (qilList.Count == 1)
			{
				return qilList[0];
			}
			return qilList;
		}

		// Token: 0x060037F7 RID: 14327 RVA: 0x00138AAA File Offset: 0x00136CAA
		private QilNode CompileStringAvt(string avt)
		{
			if (avt == null)
			{
				return null;
			}
			if (avt.IndexOfAny(QilGenerator.curlyBraces) == -1)
			{
				return this.f.String(avt);
			}
			return this.f.StrConcat(this.CompileAvt(avt));
		}

		// Token: 0x060037F8 RID: 14328 RVA: 0x00138AE0 File Offset: 0x00136CE0
		private QilNode CompileTextAvt(string avt)
		{
			if (avt.IndexOfAny(QilGenerator.curlyBraces) == -1)
			{
				return this.f.TextCtor(this.f.String(avt));
			}
			QilNode qilNode = this.CompileAvt(avt);
			if (qilNode.NodeType == QilNodeType.Sequence)
			{
				QilList qilList = this.InstructionList();
				foreach (QilNode qilNode2 in qilNode)
				{
					qilList.Add(this.f.TextCtor(qilNode2));
				}
				return qilList;
			}
			return this.f.TextCtor(qilNode);
		}

		// Token: 0x060037F9 RID: 14329 RVA: 0x00138B80 File Offset: 0x00136D80
		private QilNode CompileText(Text node)
		{
			if (node.Hints == SerializationHints.None)
			{
				return this.f.TextCtor(this.f.String(node.Select));
			}
			return this.f.RawTextCtor(this.f.String(node.Select));
		}

		// Token: 0x060037FA RID: 14330 RVA: 0x00138BD0 File Offset: 0x00136DD0
		private QilNode CompilePI(XslNode node)
		{
			QilNode qilNode = this.CompileStringAvt(node.Select);
			if (qilNode.NodeType == QilNodeType.LiteralString)
			{
				string text = (QilLiteral)qilNode;
				this.compiler.ValidatePiName(text, this);
			}
			return this.f.PICtor(qilNode, this.CompileInstructions(node.Content));
		}

		// Token: 0x060037FB RID: 14331 RVA: 0x00138C25 File Offset: 0x00136E25
		private QilNode CompileComment(XslNode node)
		{
			return this.f.CommentCtor(this.CompileInstructions(node.Content));
		}

		// Token: 0x060037FC RID: 14332 RVA: 0x00138C3E File Offset: 0x00136E3E
		private QilNode CompileError(XslNode node)
		{
			return this.f.Error(this.f.String(node.Select));
		}

		// Token: 0x060037FD RID: 14333 RVA: 0x00138C5C File Offset: 0x00136E5C
		private QilNode WrapLoopBody(ISourceLineInfo before, QilNode expr, ISourceLineInfo after)
		{
			if (this.IsDebug)
			{
				return this.f.Sequence(new QilNode[]
				{
					QilGenerator.SetLineInfo(this.InvokeOnCurrentNodeChanged(), before),
					expr,
					QilGenerator.SetLineInfo(this.f.Nop(this.f.Sequence()), after)
				});
			}
			return expr;
		}

		// Token: 0x060037FE RID: 14334 RVA: 0x00138CB8 File Offset: 0x00136EB8
		private QilNode CompileForEach(XslNodeEx node)
		{
			IList<XslNode> content = node.Content;
			LoopFocus loopFocus = this.curLoop;
			QilIterator qilIterator = this.f.For(this.CompileNodeSetExpression(node.Select));
			this.curLoop.SetFocus(qilIterator);
			int num = this.varHelper.StartVariables();
			this.curLoop.Sort(this.CompileSorts(content, ref loopFocus));
			QilNode qilNode = this.CompileInstructions(content);
			qilNode = this.WrapLoopBody(node.ElemNameLi, qilNode, node.EndTagLi);
			qilNode = this.AddCurrentPositionLast(qilNode);
			qilNode = this.curLoop.ConstructLoop(qilNode);
			qilNode = this.varHelper.FinishVariables(qilNode, num);
			this.curLoop = loopFocus;
			return qilNode;
		}

		// Token: 0x060037FF RID: 14335 RVA: 0x00138D60 File Offset: 0x00136F60
		private QilNode CompileApplyTemplates(XslNodeEx node)
		{
			IList<XslNode> content = node.Content;
			int num = this.varHelper.StartVariables();
			QilIterator qilIterator = this.f.Let(this.CompileNodeSetExpression(node.Select));
			this.varHelper.AddVariable(qilIterator);
			for (int i = 0; i < content.Count; i++)
			{
				VarPar varPar = content[i] as VarPar;
				if (varPar != null)
				{
					this.CompileWithParam(varPar);
					QilNode value = varPar.Value;
					if (this.IsDebug || (!(value is QilIterator) && !(value is QilLiteral)))
					{
						QilIterator qilIterator2 = this.f.Let(value);
						qilIterator2.DebugName = this.f.QName("with-param " + varPar.Name.QualifiedName, "urn:schemas-microsoft-com:xslt-debug").ToString();
						this.varHelper.AddVariable(qilIterator2);
						varPar.Value = qilIterator2;
					}
				}
			}
			LoopFocus loopFocus = this.curLoop;
			QilIterator qilIterator3 = this.f.For(qilIterator);
			this.curLoop.SetFocus(qilIterator3);
			this.curLoop.Sort(this.CompileSorts(content, ref loopFocus));
			QilNode qilNode = this.GenerateApply(this.compiler.Root, node);
			qilNode = this.WrapLoopBody(node.ElemNameLi, qilNode, node.EndTagLi);
			qilNode = this.AddCurrentPositionLast(qilNode);
			qilNode = this.curLoop.ConstructLoop(qilNode);
			this.curLoop = loopFocus;
			return this.varHelper.FinishVariables(qilNode, num);
		}

		// Token: 0x06003800 RID: 14336 RVA: 0x00138EE1 File Offset: 0x001370E1
		private QilNode CompileApplyImports(XslNode node)
		{
			return this.GenerateApply((StylesheetLevel)node.Arg, node);
		}

		// Token: 0x06003801 RID: 14337 RVA: 0x00138EF8 File Offset: 0x001370F8
		private QilNode CompileCallTemplate(XslNodeEx node)
		{
			int num = this.varHelper.StartVariables();
			IList<XslNode> content = node.Content;
			foreach (XslNode xslNode in content)
			{
				VarPar varPar = (VarPar)xslNode;
				this.CompileWithParam(varPar);
				if (this.IsDebug)
				{
					QilNode value = varPar.Value;
					QilIterator qilIterator = this.f.Let(value);
					qilIterator.DebugName = this.f.QName("with-param " + varPar.Name.QualifiedName, "urn:schemas-microsoft-com:xslt-debug").ToString();
					this.varHelper.AddVariable(qilIterator);
					varPar.Value = qilIterator;
				}
			}
			Template template;
			QilNode qilNode;
			if (this.compiler.NamedTemplates.TryGetValue(node.Name, out template))
			{
				qilNode = this.invkGen.GenerateInvoke(template.Function, this.AddRemoveImplicitArgs(node.Content, template.Flags));
			}
			else
			{
				if (!this.compiler.IsPhantomName(node.Name))
				{
					this.compiler.ReportError(node.SourceLine, "The named template '{0}' does not exist.", new string[] { node.Name.QualifiedName });
				}
				qilNode = this.f.Sequence();
			}
			if (content.Count > 0)
			{
				qilNode = QilGenerator.SetLineInfo(qilNode, node.ElemNameLi);
			}
			qilNode = this.varHelper.FinishVariables(qilNode, num);
			if (this.IsDebug)
			{
				return this.f.Nop(qilNode);
			}
			return qilNode;
		}

		// Token: 0x06003802 RID: 14338 RVA: 0x0013908C File Offset: 0x0013728C
		private QilNode CompileUseAttributeSet(XslNode node)
		{
			this.outputScope.InvalidateAllPrefixes();
			AttributeSet attributeSet;
			if (this.compiler.AttributeSets.TryGetValue(node.Name, out attributeSet))
			{
				return this.invkGen.GenerateInvoke(attributeSet.Function, this.AddRemoveImplicitArgs(node.Content, attributeSet.Flags));
			}
			if (!this.compiler.IsPhantomName(node.Name))
			{
				this.compiler.ReportError(node.SourceLine, "A reference to attribute set '{0}' cannot be resolved. An 'xsl:attribute-set' of this name must be declared at the top level of the stylesheet.", new string[] { node.Name.QualifiedName });
			}
			return this.f.Sequence();
		}

		// Token: 0x06003803 RID: 14339 RVA: 0x0013912C File Offset: 0x0013732C
		private QilNode CompileCopy(XslNode copy)
		{
			QilNode currentNode = this.GetCurrentNode();
			if ((currentNode.XmlType.NodeKinds & (XmlNodeKindFlags.Attribute | XmlNodeKindFlags.Namespace)) != XmlNodeKindFlags.None)
			{
				this.outputScope.InvalidateAllPrefixes();
			}
			if (currentNode.XmlType.NodeKinds == XmlNodeKindFlags.Element)
			{
				QilList qilList = this.InstructionList();
				qilList.Add(this.f.XPathNamespace(currentNode));
				this.outputScope.PushScope();
				this.outputScope.InvalidateAllPrefixes();
				QilNode qilNode = this.CompileInstructions(copy.Content, qilList);
				this.outputScope.PopScope();
				return this.f.ElementCtor(this.f.NameOf(currentNode), qilNode);
			}
			if (currentNode.XmlType.NodeKinds == XmlNodeKindFlags.Document)
			{
				return this.CompileInstructions(copy.Content);
			}
			if ((currentNode.XmlType.NodeKinds & (XmlNodeKindFlags.Document | XmlNodeKindFlags.Element)) == XmlNodeKindFlags.None)
			{
				return currentNode;
			}
			return this.f.XsltCopy(currentNode, this.CompileInstructions(copy.Content));
		}

		// Token: 0x06003804 RID: 14340 RVA: 0x00139210 File Offset: 0x00137410
		private QilNode CompileCopyOf(XslNode node)
		{
			QilNode qilNode = this.CompileXPathExpression(node.Select);
			if (qilNode.XmlType.IsNode)
			{
				if ((qilNode.XmlType.NodeKinds & (XmlNodeKindFlags.Attribute | XmlNodeKindFlags.Namespace)) != XmlNodeKindFlags.None)
				{
					this.outputScope.InvalidateAllPrefixes();
				}
				if (qilNode.XmlType.IsNotRtf && (qilNode.XmlType.NodeKinds & XmlNodeKindFlags.Document) == XmlNodeKindFlags.None)
				{
					return qilNode;
				}
				if (qilNode.XmlType.IsSingleton)
				{
					return this.f.XsltCopyOf(qilNode);
				}
				QilIterator qilIterator;
				return this.f.Loop(qilIterator = this.f.For(qilNode), this.f.XsltCopyOf(qilIterator));
			}
			else
			{
				if (qilNode.XmlType.IsAtomicValue)
				{
					return this.f.TextCtor(this.f.ConvertToString(qilNode));
				}
				this.outputScope.InvalidateAllPrefixes();
				QilIterator qilIterator2;
				return this.f.Loop(qilIterator2 = this.f.For(qilNode), this.f.Conditional(this.f.IsType(qilIterator2, XmlQueryTypeFactory.Node), this.f.XsltCopyOf(this.f.TypeAssert(qilIterator2, XmlQueryTypeFactory.Node)), this.f.TextCtor(this.f.XsltConvert(qilIterator2, XmlQueryTypeFactory.StringX))));
			}
		}

		// Token: 0x06003805 RID: 14341 RVA: 0x0013934D File Offset: 0x0013754D
		private QilNode CompileValueOf(XslNode valueOf)
		{
			return this.f.TextCtor(this.f.ConvertToString(this.CompileXPathExpression(valueOf.Select)));
		}

		// Token: 0x06003806 RID: 14342 RVA: 0x00139371 File Offset: 0x00137571
		private QilNode CompileValueOfDoe(XslNode valueOf)
		{
			return this.f.RawTextCtor(this.f.ConvertToString(this.CompileXPathExpression(valueOf.Select)));
		}

		// Token: 0x06003807 RID: 14343 RVA: 0x00139395 File Offset: 0x00137595
		private QilNode CompileWhen(XslNode whenNode, QilNode otherwise)
		{
			return this.f.Conditional(this.f.ConvertToBoolean(this.CompileXPathExpression(whenNode.Select)), this.CompileInstructions(whenNode.Content), otherwise);
		}

		// Token: 0x06003808 RID: 14344 RVA: 0x001393C6 File Offset: 0x001375C6
		private QilNode CompileIf(XslNode ifNode)
		{
			return this.CompileWhen(ifNode, this.InstructionList());
		}

		// Token: 0x06003809 RID: 14345 RVA: 0x001393D8 File Offset: 0x001375D8
		private QilNode CompileChoose(XslNode node)
		{
			IList<XslNode> content = node.Content;
			QilNode qilNode = null;
			int num = content.Count - 1;
			while (0 <= num)
			{
				XslNode xslNode = content[num];
				QilList qilList = this.EnterScope(xslNode);
				if (xslNode.NodeType == XslNodeType.Otherwise)
				{
					qilNode = this.CompileInstructions(xslNode.Content);
				}
				else
				{
					qilNode = this.CompileWhen(xslNode, qilNode ?? this.InstructionList());
				}
				this.ExitScope();
				this.SetLineInfoCheck(qilNode, xslNode.SourceLine);
				qilNode = this.SetDebugNs(qilNode, qilList);
				num--;
			}
			if (qilNode == null)
			{
				return this.f.Sequence();
			}
			if (!this.IsDebug)
			{
				return qilNode;
			}
			return this.f.Sequence(qilNode);
		}

		// Token: 0x0600380A RID: 14346 RVA: 0x00139480 File Offset: 0x00137680
		private QilNode CompileMessage(XslNode node)
		{
			string uri = this.lastScope.SourceLine.Uri;
			QilNode qilNode = this.f.RtfCtor(this.CompileInstructions(node.Content), this.f.String(uri));
			qilNode = this.f.InvokeOuterXml(qilNode);
			if (!(bool)node.Arg)
			{
				return this.f.Warning(qilNode);
			}
			QilIterator qilIterator;
			return this.f.Loop(qilIterator = this.f.Let(qilNode), this.f.Sequence(this.f.Warning(qilIterator), this.f.Error(qilIterator)));
		}

		// Token: 0x0600380B RID: 14347 RVA: 0x00139528 File Offset: 0x00137728
		private QilNode CompileVariable(XslNode node)
		{
			if (this.scope.IsLocalVariable(node.Name.LocalName, node.Name.NamespaceUri))
			{
				this.ReportError("The variable or parameter '{0}' was duplicated within the same scope.", new string[] { node.Name.QualifiedName });
			}
			return this.CompileVarParValue(node);
		}

		// Token: 0x0600380C RID: 14348 RVA: 0x00139580 File Offset: 0x00137780
		private QilNode CompileVarParValue(XslNode node)
		{
			string uri = this.lastScope.SourceLine.Uri;
			IList<XslNode> content = node.Content;
			string select = node.Select;
			QilNode qilNode;
			if (select != null)
			{
				QilList qilList = this.InstructionList();
				qilList.Add(this.CompileXPathExpression(select));
				qilNode = this.CompileInstructions(content, qilList);
			}
			else if (content.Count != 0)
			{
				this.outputScope.PushScope();
				this.outputScope.InvalidateAllPrefixes();
				qilNode = this.f.RtfCtor(this.CompileInstructions(content), this.f.String(uri));
				this.outputScope.PopScope();
			}
			else
			{
				qilNode = this.f.String(string.Empty);
			}
			if (this.IsDebug)
			{
				qilNode = this.f.TypeAssert(qilNode, XmlQueryTypeFactory.ItemS);
			}
			return qilNode;
		}

		// Token: 0x0600380D RID: 14349 RVA: 0x00139648 File Offset: 0x00137848
		private void CompileWithParam(VarPar withParam)
		{
			QilList qilList = this.EnterScope(withParam);
			QilNode qilNode = this.CompileVarParValue(withParam);
			this.ExitScope();
			QilGenerator.SetLineInfo(qilNode, withParam.SourceLine);
			qilNode = this.SetDebugNs(qilNode, qilList);
			withParam.Value = qilNode;
		}

		// Token: 0x0600380E RID: 14350 RVA: 0x00139688 File Offset: 0x00137888
		private QilNode CompileSorts(IList<XslNode> content, ref LoopFocus parentLoop)
		{
			QilList qilList = this.f.BaseFactory.SortKeyList();
			int i = 0;
			while (i < content.Count)
			{
				Sort sort = content[i] as Sort;
				if (sort != null)
				{
					this.CompileSort(sort, qilList, ref parentLoop);
					content.RemoveAt(i);
				}
				else
				{
					i++;
				}
			}
			if (qilList.Count == 0)
			{
				return null;
			}
			return qilList;
		}

		// Token: 0x0600380F RID: 14351 RVA: 0x001396E4 File Offset: 0x001378E4
		private QilNode CompileLangAttribute(string attValue, bool fwdCompat)
		{
			QilNode qilNode = this.CompileStringAvt(attValue);
			if (qilNode != null)
			{
				if (qilNode.NodeType == QilNodeType.LiteralString)
				{
					if (XsltLibrary.LangToLcidInternal((QilLiteral)qilNode, fwdCompat, this) == 127)
					{
						qilNode = null;
					}
				}
				else
				{
					QilIterator qilIterator;
					qilNode = this.f.Loop(qilIterator = this.f.Let(qilNode), this.f.Conditional(this.f.Eq(this.f.InvokeLangToLcid(qilIterator, fwdCompat), this.f.Int32(127)), this.f.String(string.Empty), qilIterator));
				}
			}
			return qilNode;
		}

		// Token: 0x06003810 RID: 14352 RVA: 0x0013977C File Offset: 0x0013797C
		private QilNode CompileLangAttributeToLcid(string attValue, bool fwdCompat)
		{
			return this.CompileLangToLcid(this.CompileStringAvt(attValue), fwdCompat);
		}

		// Token: 0x06003811 RID: 14353 RVA: 0x0013978C File Offset: 0x0013798C
		private QilNode CompileLangToLcid(QilNode lang, bool fwdCompat)
		{
			if (lang == null)
			{
				return this.f.Double(127.0);
			}
			if (lang.NodeType == QilNodeType.LiteralString)
			{
				return this.f.Double((double)XsltLibrary.LangToLcidInternal((QilLiteral)lang, fwdCompat, this));
			}
			return this.f.XsltConvert(this.f.InvokeLangToLcid(lang, fwdCompat), XmlQueryTypeFactory.DoubleX);
		}

		// Token: 0x06003812 RID: 14354 RVA: 0x001397F8 File Offset: 0x001379F8
		private void CompileDataTypeAttribute(string attValue, bool fwdCompat, ref QilNode select, out QilNode select2)
		{
			QilNode qilNode = this.CompileStringAvt(attValue);
			if (qilNode != null)
			{
				if (qilNode.NodeType != QilNodeType.LiteralString)
				{
					QilIterator qilIterator;
					qilNode = this.f.Loop(qilIterator = this.f.Let(qilNode), this.f.Conditional(this.f.Eq(qilIterator, this.f.String("number")), this.f.False(), this.f.Conditional(this.f.Eq(qilIterator, this.f.String("text")), this.f.True(), fwdCompat ? this.f.True() : this.f.Loop(this.f.Let(this.ResolveQNameDynamic(true, qilIterator)), this.f.Error(this.lastScope.SourceLine, "The value of the '{0}' attribute must be '{1}' or '{2}'.", new string[] { "data-type", "text", "number" })))));
					QilIterator qilIterator2 = this.f.Let(qilNode);
					this.varHelper.AddVariable(qilIterator2);
					select2 = select.DeepClone(this.f.BaseFactory);
					select = this.f.Conditional(qilIterator2, this.f.ConvertToString(select), this.f.String(string.Empty));
					select2 = this.f.Conditional(qilIterator2, this.f.Double(0.0), this.f.ConvertToNumber(select2));
					return;
				}
				string text = (QilLiteral)qilNode;
				if (text == "number")
				{
					select = this.f.ConvertToNumber(select);
					select2 = null;
					return;
				}
				if (!(text == "text") && !fwdCompat)
				{
					string text2;
					string text3;
					int length = (this.compiler.ParseQName(text, out text2, out text3, this) ? this.ResolvePrefix(true, text2) : this.compiler.CreatePhantomNamespace()).Length;
					this.ReportError("The value of the '{0}' attribute must be '{1}' or '{2}'.", new string[] { "data-type", "text", "number" });
				}
			}
			select = this.f.ConvertToString(select);
			select2 = null;
		}

		// Token: 0x06003813 RID: 14355 RVA: 0x00139A50 File Offset: 0x00137C50
		private QilNode CompileOrderAttribute(string attName, string attValue, string value0, string value1, bool fwdCompat)
		{
			QilNode qilNode = this.CompileStringAvt(attValue);
			if (qilNode != null)
			{
				if (qilNode.NodeType == QilNodeType.LiteralString)
				{
					string text = (QilLiteral)qilNode;
					if (text == value1)
					{
						qilNode = this.f.String("1");
					}
					else
					{
						if (text != value0 && !fwdCompat)
						{
							this.ReportError("The value of the '{0}' attribute must be '{1}' or '{2}'.", new string[] { attName, value0, value1 });
						}
						qilNode = this.f.String("0");
					}
				}
				else
				{
					QilIterator qilIterator;
					qilNode = this.f.Loop(qilIterator = this.f.Let(qilNode), this.f.Conditional(this.f.Eq(qilIterator, this.f.String(value1)), this.f.String("1"), fwdCompat ? this.f.String("0") : this.f.Conditional(this.f.Eq(qilIterator, this.f.String(value0)), this.f.String("0"), this.f.Error(this.lastScope.SourceLine, "The value of the '{0}' attribute must be '{1}' or '{2}'.", new string[] { attName, value0, value1 }))));
				}
			}
			return qilNode;
		}

		// Token: 0x06003814 RID: 14356 RVA: 0x00139BA8 File Offset: 0x00137DA8
		private void CompileSort(Sort sort, QilList keyList, ref LoopFocus parentLoop)
		{
			this.EnterScope(sort);
			bool forwardsCompatible = sort.ForwardsCompatible;
			QilNode qilNode = this.CompileXPathExpression(sort.Select);
			QilNode qilNode2;
			QilNode qilNode3;
			QilNode qilNode4;
			QilNode qilNode5;
			if (sort.Lang != null || sort.DataType != null || sort.Order != null || sort.CaseOrder != null)
			{
				LoopFocus loopFocus = this.curLoop;
				this.curLoop = parentLoop;
				qilNode2 = this.CompileLangAttribute(sort.Lang, forwardsCompatible);
				this.CompileDataTypeAttribute(sort.DataType, forwardsCompatible, ref qilNode, out qilNode3);
				qilNode4 = this.CompileOrderAttribute("order", sort.Order, "ascending", "descending", forwardsCompatible);
				qilNode5 = this.CompileOrderAttribute("case-order", sort.CaseOrder, "lower-first", "upper-first", forwardsCompatible);
				this.curLoop = loopFocus;
			}
			else
			{
				qilNode = this.f.ConvertToString(qilNode);
				qilNode2 = (qilNode3 = (qilNode4 = (qilNode5 = null)));
			}
			this.strConcat.Reset();
			this.strConcat.Append("http://collations.microsoft.com");
			this.strConcat.Append('/');
			this.strConcat.Append(qilNode2);
			char c = '?';
			if (qilNode4 != null)
			{
				this.strConcat.Append(c);
				this.strConcat.Append("descendingOrder=");
				this.strConcat.Append(qilNode4);
				c = '&';
			}
			if (qilNode5 != null)
			{
				this.strConcat.Append(c);
				this.strConcat.Append("upperFirst=");
				this.strConcat.Append(qilNode5);
			}
			QilNode qilNode6 = this.strConcat.ToQil();
			QilSortKey qilSortKey = this.f.SortKey(qilNode, qilNode6);
			keyList.Add(qilSortKey);
			if (qilNode3 != null)
			{
				qilSortKey = this.f.SortKey(qilNode3, qilNode6.DeepClone(this.f.BaseFactory));
				keyList.Add(qilSortKey);
			}
			this.ExitScope();
		}

		// Token: 0x06003815 RID: 14357 RVA: 0x00139D74 File Offset: 0x00137F74
		private QilNode MatchPattern(QilNode pattern, QilIterator testNode)
		{
			if (pattern.NodeType == QilNodeType.Error)
			{
				return pattern;
			}
			QilList qilList;
			if (pattern.NodeType == QilNodeType.Sequence)
			{
				qilList = (QilList)pattern;
			}
			else
			{
				qilList = this.f.BaseFactory.Sequence();
				qilList.Add(pattern);
			}
			QilNode qilNode = this.f.False();
			int num = qilList.Count - 1;
			while (0 <= num)
			{
				QilLoop qilLoop = (QilLoop)qilList[num];
				qilNode = this.f.Or(this.refReplacer.Replace(qilLoop.Body, qilLoop.Variable, testNode), qilNode);
				num--;
			}
			return qilNode;
		}

		// Token: 0x06003816 RID: 14358 RVA: 0x00139E0C File Offset: 0x0013800C
		private QilNode MatchCountPattern(QilNode countPattern, QilIterator testNode)
		{
			if (countPattern != null)
			{
				return this.MatchPattern(countPattern, testNode);
			}
			QilNode currentNode = this.GetCurrentNode();
			XmlNodeKindFlags nodeKinds = currentNode.XmlType.NodeKinds;
			if ((nodeKinds & (nodeKinds - 1)) != XmlNodeKindFlags.None)
			{
				return this.f.InvokeIsSameNodeSort(testNode, currentNode);
			}
			if (nodeKinds <= XmlNodeKindFlags.Text)
			{
				QilNode qilNode;
				switch (nodeKinds)
				{
				case XmlNodeKindFlags.Document:
					return this.f.IsType(testNode, XmlQueryTypeFactory.Document);
				case XmlNodeKindFlags.Element:
					qilNode = this.f.IsType(testNode, XmlQueryTypeFactory.Element);
					break;
				case XmlNodeKindFlags.Document | XmlNodeKindFlags.Element:
					goto IL_0154;
				case XmlNodeKindFlags.Attribute:
					qilNode = this.f.IsType(testNode, XmlQueryTypeFactory.Attribute);
					break;
				default:
					if (nodeKinds != XmlNodeKindFlags.Text)
					{
						goto IL_0154;
					}
					return this.f.IsType(testNode, XmlQueryTypeFactory.Text);
				}
				return this.f.And(qilNode, this.f.And(this.f.Eq(this.f.LocalNameOf(testNode), this.f.LocalNameOf(currentNode)), this.f.Eq(this.f.NamespaceUriOf(testNode), this.f.NamespaceUriOf(this.GetCurrentNode()))));
			}
			if (nodeKinds == XmlNodeKindFlags.Comment)
			{
				return this.f.IsType(testNode, XmlQueryTypeFactory.Comment);
			}
			if (nodeKinds == XmlNodeKindFlags.PI)
			{
				return this.f.And(this.f.IsType(testNode, XmlQueryTypeFactory.PI), this.f.Eq(this.f.LocalNameOf(testNode), this.f.LocalNameOf(currentNode)));
			}
			if (nodeKinds == XmlNodeKindFlags.Namespace)
			{
				return this.f.And(this.f.IsType(testNode, XmlQueryTypeFactory.Namespace), this.f.Eq(this.f.LocalNameOf(testNode), this.f.LocalNameOf(currentNode)));
			}
			IL_0154:
			return this.f.False();
		}

		// Token: 0x06003817 RID: 14359 RVA: 0x00139FDC File Offset: 0x001381DC
		private QilNode PlaceMarker(QilNode countPattern, QilNode fromPattern, bool multiple)
		{
			QilNode qilNode = ((countPattern != null) ? countPattern.DeepClone(this.f.BaseFactory) : null);
			QilIterator qilIterator;
			QilNode qilNode2 = this.f.Filter(qilIterator = this.f.For(this.f.AncestorOrSelf(this.GetCurrentNode())), this.MatchCountPattern(countPattern, qilIterator));
			QilNode qilNode3;
			if (multiple)
			{
				qilNode3 = this.f.DocOrderDistinct(qilNode2);
			}
			else
			{
				qilNode3 = this.f.Filter(qilIterator = this.f.For(qilNode2), this.f.Eq(this.f.PositionOf(qilIterator), this.f.Int32(1)));
			}
			QilNode qilNode4;
			QilIterator qilIterator2;
			if (fromPattern == null)
			{
				qilNode4 = qilNode3;
			}
			else
			{
				QilNode qilNode5 = this.f.Filter(qilIterator = this.f.For(this.f.AncestorOrSelf(this.GetCurrentNode())), this.MatchPattern(fromPattern, qilIterator));
				QilNode qilNode6 = this.f.Filter(qilIterator = this.f.For(qilNode5), this.f.Eq(this.f.PositionOf(qilIterator), this.f.Int32(1)));
				qilNode4 = this.f.Loop(qilIterator = this.f.For(qilNode6), this.f.Filter(qilIterator2 = this.f.For(qilNode3), this.f.Before(qilIterator, qilIterator2)));
			}
			return this.f.Loop(qilIterator2 = this.f.For(qilNode4), this.f.Add(this.f.Int32(1), this.f.Length(this.f.Filter(qilIterator = this.f.For(this.f.PrecedingSibling(qilIterator2)), this.MatchCountPattern(qilNode, qilIterator)))));
		}

		// Token: 0x06003818 RID: 14360 RVA: 0x0013A1B8 File Offset: 0x001383B8
		private QilNode PlaceMarkerAny(QilNode countPattern, QilNode fromPattern)
		{
			QilNode qilNode2;
			QilIterator qilIterator3;
			if (fromPattern == null)
			{
				QilNode qilNode = this.f.NodeRange(this.f.Root(this.GetCurrentNode()), this.GetCurrentNode());
				QilIterator qilIterator;
				qilNode2 = this.f.Filter(qilIterator = this.f.For(qilNode), this.MatchCountPattern(countPattern, qilIterator));
			}
			else
			{
				QilIterator qilIterator;
				QilNode qilNode3 = this.f.Filter(qilIterator = this.f.For(this.f.Preceding(this.GetCurrentNode())), this.MatchPattern(fromPattern, qilIterator));
				QilNode qilNode4 = this.f.Filter(qilIterator = this.f.For(qilNode3), this.f.Eq(this.f.PositionOf(qilIterator), this.f.Int32(1)));
				QilIterator qilIterator2;
				qilNode2 = this.f.Loop(qilIterator = this.f.For(qilNode4), this.f.Filter(qilIterator2 = this.f.For(this.f.Filter(qilIterator3 = this.f.For(this.f.NodeRange(qilIterator, this.GetCurrentNode())), this.MatchCountPattern(countPattern, qilIterator3))), this.f.Not(this.f.Is(qilIterator, qilIterator2))));
			}
			return this.f.Loop(qilIterator3 = this.f.Let(this.f.Length(qilNode2)), this.f.Conditional(this.f.Eq(qilIterator3, this.f.Int32(0)), this.f.Sequence(), qilIterator3));
		}

		// Token: 0x06003819 RID: 14361 RVA: 0x0013A360 File Offset: 0x00138560
		private QilNode CompileLetterValueAttribute(string attValue, bool fwdCompat)
		{
			QilNode qilNode = this.CompileStringAvt(attValue);
			if (qilNode == null)
			{
				return this.f.String("default");
			}
			if (qilNode.NodeType == QilNodeType.LiteralString)
			{
				string text = (QilLiteral)qilNode;
				if (text != "alphabetic" && text != "traditional")
				{
					if (fwdCompat)
					{
						return this.f.String("default");
					}
					this.ReportError("The value of the '{0}' attribute must be '{1}' or '{2}'.", new string[] { "letter-value", "alphabetic", "traditional" });
				}
				return qilNode;
			}
			QilIterator qilIterator = this.f.Let(qilNode);
			return this.f.Loop(qilIterator, this.f.Conditional(this.f.Or(this.f.Eq(qilIterator, this.f.String("alphabetic")), this.f.Eq(qilIterator, this.f.String("traditional"))), qilIterator, fwdCompat ? this.f.String("default") : this.f.Error(this.lastScope.SourceLine, "The value of the '{0}' attribute must be '{1}' or '{2}'.", new string[] { "letter-value", "alphabetic", "traditional" })));
		}

		// Token: 0x0600381A RID: 14362 RVA: 0x0013A4B4 File Offset: 0x001386B4
		private QilNode CompileGroupingSeparatorAttribute(string attValue, bool fwdCompat)
		{
			QilNode qilNode = this.CompileStringAvt(attValue);
			if (qilNode == null)
			{
				qilNode = this.f.String(string.Empty);
			}
			else if (qilNode.NodeType == QilNodeType.LiteralString)
			{
				if (((QilLiteral)qilNode).Length != 1)
				{
					if (!fwdCompat)
					{
						this.ReportError("The value of the '{0}' attribute must be a single character.", new string[] { "grouping-separator" });
					}
					qilNode = this.f.String(string.Empty);
				}
			}
			else
			{
				QilIterator qilIterator = this.f.Let(qilNode);
				qilNode = this.f.Loop(qilIterator, this.f.Conditional(this.f.Eq(this.f.StrLength(qilIterator), this.f.Int32(1)), qilIterator, fwdCompat ? this.f.String(string.Empty) : this.f.Error(this.lastScope.SourceLine, "The value of the '{0}' attribute must be a single character.", new string[] { "grouping-separator" })));
			}
			return qilNode;
		}

		// Token: 0x0600381B RID: 14363 RVA: 0x0013A5BC File Offset: 0x001387BC
		private QilNode CompileGroupingSizeAttribute(string attValue, bool fwdCompat)
		{
			QilNode qilNode = this.CompileStringAvt(attValue);
			if (qilNode == null)
			{
				return this.f.Double(0.0);
			}
			if (qilNode.NodeType != QilNodeType.LiteralString)
			{
				QilIterator qilIterator = this.f.Let(this.f.ConvertToNumber(qilNode));
				return this.f.Loop(qilIterator, this.f.Conditional(this.f.And(this.f.Lt(this.f.Double(0.0), qilIterator), this.f.Lt(qilIterator, this.f.Double(2147483647.0))), qilIterator, this.f.Double(0.0)));
			}
			double num = XsltFunctions.Round(XPathConvert.StringToDouble((QilLiteral)qilNode));
			if (0.0 <= num && num <= 2147483647.0)
			{
				return this.f.Double(num);
			}
			return this.f.Double(0.0);
		}

		// Token: 0x0600381C RID: 14364 RVA: 0x0013A6D4 File Offset: 0x001388D4
		private QilNode CompileNumber(Number num)
		{
			QilNode qilNode;
			if (num.Value != null)
			{
				qilNode = this.f.ConvertToNumber(this.CompileXPathExpression(num.Value));
			}
			else
			{
				QilNode qilNode2 = ((num.Count != null) ? this.CompileNumberPattern(num.Count) : null);
				QilNode qilNode3 = ((num.From != null) ? this.CompileNumberPattern(num.From) : null);
				NumberLevel level = num.Level;
				if (level != NumberLevel.Single)
				{
					if (level != NumberLevel.Multiple)
					{
						qilNode = this.PlaceMarkerAny(qilNode2, qilNode3);
					}
					else
					{
						qilNode = this.PlaceMarker(qilNode2, qilNode3, true);
					}
				}
				else
				{
					qilNode = this.PlaceMarker(qilNode2, qilNode3, false);
				}
			}
			bool forwardsCompatible = num.ForwardsCompatible;
			return this.f.TextCtor(this.f.InvokeNumberFormat(qilNode, this.CompileStringAvt(num.Format), this.CompileLangAttributeToLcid(num.Lang, forwardsCompatible), this.CompileLetterValueAttribute(num.LetterValue, forwardsCompatible), this.CompileGroupingSeparatorAttribute(num.GroupingSeparator, forwardsCompatible), this.CompileGroupingSizeAttribute(num.GroupingSize, forwardsCompatible)));
		}

		// Token: 0x0600381D RID: 14365 RVA: 0x0013A7C8 File Offset: 0x001389C8
		private void CompileAndSortMatches(Stylesheet sheet)
		{
			foreach (Template template in sheet.Templates)
			{
				if (template.Match != null)
				{
					this.EnterScope(template);
					QilNode qilNode = this.CompileMatchPattern(template.Match);
					if (qilNode.NodeType == QilNodeType.Sequence)
					{
						QilList qilList = (QilList)qilNode;
						for (int i = 0; i < qilList.Count; i++)
						{
							sheet.AddTemplateMatch(template, (QilLoop)qilList[i]);
						}
					}
					else
					{
						sheet.AddTemplateMatch(template, (QilLoop)qilNode);
					}
					this.ExitScope();
				}
			}
			sheet.SortTemplateMatches();
			foreach (Stylesheet stylesheet in sheet.Imports)
			{
				this.CompileAndSortMatches(stylesheet);
			}
		}

		// Token: 0x0600381E RID: 14366 RVA: 0x0013A8B0 File Offset: 0x00138AB0
		private void CompileKeys()
		{
			for (int i = 0; i < this.compiler.Keys.Count; i++)
			{
				foreach (Key key in this.compiler.Keys[i])
				{
					this.EnterScope(key);
					QilParameter qilParameter = this.f.Parameter(XmlQueryTypeFactory.NodeNotRtf);
					this.singlFocus.SetFocus(qilParameter);
					QilIterator qilIterator = this.f.For(this.f.OptimizeBarrier(this.CompileKeyMatch(key.Match)));
					this.singlFocus.SetFocus(qilIterator);
					QilIterator qilIterator2 = this.f.For(this.CompileKeyUse(key));
					qilIterator2 = this.f.For(this.f.OptimizeBarrier(this.f.Loop(qilIterator2, this.f.ConvertToString(qilIterator2))));
					QilParameter qilParameter2 = this.f.Parameter(XmlQueryTypeFactory.StringX);
					QilFunction qilFunction = this.f.Function(this.f.FormalParameterList(qilParameter, qilParameter2), this.f.Filter(qilIterator, this.f.Not(this.f.IsEmpty(this.f.Filter(qilIterator2, this.f.Eq(qilIterator2, qilParameter2))))), this.f.False());
					qilFunction.DebugName = key.GetDebugName();
					QilGenerator.SetLineInfo(qilFunction, key.SourceLine);
					key.Function = qilFunction;
					this.functions.Add(qilFunction);
					this.ExitScope();
				}
			}
			this.singlFocus.SetFocus(null);
		}

		// Token: 0x0600381F RID: 14367 RVA: 0x0013AA90 File Offset: 0x00138C90
		private void CreateGlobalVarPars()
		{
			foreach (VarPar varPar in this.compiler.ExternalPars)
			{
				this.CreateGlobalVarPar(varPar);
			}
			foreach (VarPar varPar2 in this.compiler.GlobalVars)
			{
				this.CreateGlobalVarPar(varPar2);
			}
		}

		// Token: 0x06003820 RID: 14368 RVA: 0x0013AB30 File Offset: 0x00138D30
		private void CreateGlobalVarPar(VarPar varPar)
		{
			XmlQueryType xmlQueryType = this.ChooseBestType(varPar);
			QilIterator qilIterator;
			if (varPar.NodeType == XslNodeType.Variable)
			{
				qilIterator = this.f.Let(this.f.Unknown(xmlQueryType));
			}
			else
			{
				qilIterator = this.f.Parameter(null, varPar.Name, xmlQueryType);
			}
			qilIterator.DebugName = varPar.Name.ToString();
			varPar.Value = qilIterator;
			QilGenerator.SetLineInfo(qilIterator, varPar.SourceLine);
			this.scope.AddVariable(varPar.Name, qilIterator);
		}

		// Token: 0x06003821 RID: 14369 RVA: 0x0013ABB4 File Offset: 0x00138DB4
		private void CompileGlobalVariables()
		{
			this.singlFocus.SetFocus(SingletonFocusType.InitialDocumentNode);
			foreach (VarPar varPar in this.compiler.ExternalPars)
			{
				this.extPars.Add(this.CompileGlobalVarPar(varPar));
			}
			foreach (VarPar varPar2 in this.compiler.GlobalVars)
			{
				this.gloVars.Add(this.CompileGlobalVarPar(varPar2));
			}
			this.singlFocus.SetFocus(null);
		}

		// Token: 0x06003822 RID: 14370 RVA: 0x0013AC84 File Offset: 0x00138E84
		private QilIterator CompileGlobalVarPar(VarPar varPar)
		{
			QilIterator qilIterator = (QilIterator)varPar.Value;
			QilList qilList = this.EnterScope(varPar);
			QilNode qilNode = this.CompileVarParValue(varPar);
			QilGenerator.SetLineInfo(qilNode, qilIterator.SourceLine);
			qilNode = this.AddCurrentPositionLast(qilNode);
			qilNode = this.SetDebugNs(qilNode, qilList);
			qilIterator.SourceLine = SourceLineInfo.NoSource;
			qilIterator.Binding = qilNode;
			this.ExitScope();
			return qilIterator;
		}

		// Token: 0x06003823 RID: 14371 RVA: 0x0013ACE4 File Offset: 0x00138EE4
		private void ReportErrorInXPath(XslLoadException e)
		{
			XPathCompileException ex = e as XPathCompileException;
			string text = ((ex != null) ? ex.FormatDetailedMessage() : e.Message);
			this.compiler.ReportError(this.lastScope.SourceLine, "{0}", new string[] { text });
		}

		// Token: 0x06003824 RID: 14372 RVA: 0x0013AD2F File Offset: 0x00138F2F
		private QilNode PhantomXPathExpression()
		{
			return this.f.TypeAssert(this.f.Sequence(), XmlQueryTypeFactory.ItemS);
		}

		// Token: 0x06003825 RID: 14373 RVA: 0x0013AD4C File Offset: 0x00138F4C
		private QilNode PhantomKeyMatch()
		{
			return this.f.TypeAssert(this.f.Sequence(), XmlQueryTypeFactory.NodeNotRtfS);
		}

		// Token: 0x06003826 RID: 14374 RVA: 0x0013AD6C File Offset: 0x00138F6C
		private QilNode CompileXPathExpression(string expr)
		{
			this.SetEnvironmentFlags(true, true, true);
			QilNode qilNode;
			if (expr == null)
			{
				qilNode = this.PhantomXPathExpression();
			}
			else
			{
				try
				{
					XPathScanner xpathScanner = new XPathScanner(expr);
					qilNode = this.xpathParser.Parse(xpathScanner, this.xpathBuilder, LexKind.Eof);
				}
				catch (XslLoadException ex)
				{
					if (this.xslVersion != XslVersion.ForwardsCompatible)
					{
						this.ReportErrorInXPath(ex);
					}
					qilNode = this.f.Error(this.f.String(ex.Message));
				}
			}
			if (qilNode is QilIterator)
			{
				qilNode = this.f.Nop(qilNode);
			}
			return qilNode;
		}

		// Token: 0x06003827 RID: 14375 RVA: 0x0013AE04 File Offset: 0x00139004
		private QilNode CompileNodeSetExpression(string expr)
		{
			QilNode qilNode = this.f.TryEnsureNodeSet(this.CompileXPathExpression(expr));
			if (qilNode == null)
			{
				XPathCompileException ex = new XPathCompileException(expr, 0, expr.Length, "Expression must evaluate to a node-set.", null);
				if (this.xslVersion != XslVersion.ForwardsCompatible)
				{
					this.ReportErrorInXPath(ex);
				}
				qilNode = this.f.Error(this.f.String(ex.Message));
			}
			return qilNode;
		}

		// Token: 0x06003828 RID: 14376 RVA: 0x0013AE6C File Offset: 0x0013906C
		private QilNode CompileXPathExpressionWithinAvt(string expr, ref int pos)
		{
			this.SetEnvironmentFlags(true, true, true);
			QilNode qilNode;
			try
			{
				XPathScanner xpathScanner = new XPathScanner(expr, pos);
				qilNode = this.xpathParser.Parse(xpathScanner, this.xpathBuilder, LexKind.RBrace);
				pos = xpathScanner.LexStart + 1;
			}
			catch (XslLoadException ex)
			{
				if (this.xslVersion != XslVersion.ForwardsCompatible)
				{
					this.ReportErrorInXPath(ex);
				}
				qilNode = this.f.Error(this.f.String(ex.Message));
				pos = expr.Length;
			}
			if (qilNode is QilIterator)
			{
				qilNode = this.f.Nop(qilNode);
			}
			return qilNode;
		}

		// Token: 0x06003829 RID: 14377 RVA: 0x0013AF0C File Offset: 0x0013910C
		private QilNode CompileMatchPattern(string pttrn)
		{
			this.SetEnvironmentFlags(false, false, true);
			QilNode qilNode;
			try
			{
				XPathScanner xpathScanner = new XPathScanner(pttrn);
				qilNode = this.ptrnParser.Parse(xpathScanner, this.ptrnBuilder);
			}
			catch (XslLoadException ex)
			{
				if (this.xslVersion != XslVersion.ForwardsCompatible)
				{
					this.ReportErrorInXPath(ex);
				}
				qilNode = this.f.Loop(this.f.For(this.ptrnBuilder.FixupNode), this.f.Error(this.f.String(ex.Message)));
				XPathPatternBuilder.SetPriority(qilNode, 0.5);
			}
			return qilNode;
		}

		// Token: 0x0600382A RID: 14378 RVA: 0x0013AFB0 File Offset: 0x001391B0
		private QilNode CompileNumberPattern(string pttrn)
		{
			this.SetEnvironmentFlags(true, false, true);
			QilNode qilNode;
			try
			{
				XPathScanner xpathScanner = new XPathScanner(pttrn);
				qilNode = this.ptrnParser.Parse(xpathScanner, this.ptrnBuilder);
			}
			catch (XslLoadException ex)
			{
				if (this.xslVersion != XslVersion.ForwardsCompatible)
				{
					this.ReportErrorInXPath(ex);
				}
				qilNode = this.f.Error(this.f.String(ex.Message));
			}
			return qilNode;
		}

		// Token: 0x0600382B RID: 14379 RVA: 0x0013B024 File Offset: 0x00139224
		private QilNode CompileKeyMatch(string pttrn)
		{
			if (this.keyMatchBuilder == null)
			{
				this.keyMatchBuilder = new KeyMatchBuilder(this);
			}
			this.SetEnvironmentFlags(false, false, false);
			QilNode qilNode;
			if (pttrn == null)
			{
				qilNode = this.PhantomKeyMatch();
			}
			else
			{
				try
				{
					XPathScanner xpathScanner = new XPathScanner(pttrn);
					qilNode = this.ptrnParser.Parse(xpathScanner, this.keyMatchBuilder);
				}
				catch (XslLoadException ex)
				{
					if (this.xslVersion != XslVersion.ForwardsCompatible)
					{
						this.ReportErrorInXPath(ex);
					}
					qilNode = this.f.Error(this.f.String(ex.Message));
				}
			}
			return qilNode;
		}

		// Token: 0x0600382C RID: 14380 RVA: 0x0013B0B8 File Offset: 0x001392B8
		private QilNode CompileKeyUse(Key key)
		{
			string use = key.Use;
			this.SetEnvironmentFlags(false, true, false);
			QilNode qilNode;
			if (use == null)
			{
				qilNode = this.f.Error(this.f.String(XslLoadException.CreateMessage(key.SourceLine, "Missing mandatory attribute '{0}'.", new string[] { "use" })));
			}
			else
			{
				try
				{
					XPathScanner xpathScanner = new XPathScanner(use);
					qilNode = this.xpathParser.Parse(xpathScanner, this.xpathBuilder, LexKind.Eof);
				}
				catch (XslLoadException ex)
				{
					if (this.xslVersion != XslVersion.ForwardsCompatible)
					{
						this.ReportErrorInXPath(ex);
					}
					qilNode = this.f.Error(this.f.String(ex.Message));
				}
			}
			if (qilNode is QilIterator)
			{
				qilNode = this.f.Nop(qilNode);
			}
			return qilNode;
		}

		// Token: 0x0600382D RID: 14381 RVA: 0x0013B184 File Offset: 0x00139384
		private QilNode ResolveQNameDynamic(bool ignoreDefaultNs, QilNode qilName)
		{
			QilList qilList = this.f.BaseFactory.Sequence();
			if (ignoreDefaultNs)
			{
				qilList.Add(this.f.NamespaceDecl(this.f.String(string.Empty), this.f.String(string.Empty)));
			}
			foreach (CompilerScopeManager<QilIterator>.ScopeRecord scopeRecord in this.scope)
			{
				string ncName = scopeRecord.ncName;
				string nsUri = scopeRecord.nsUri;
				if (!ignoreDefaultNs || ncName.Length != 0)
				{
					qilList.Add(this.f.NamespaceDecl(this.f.String(ncName), this.f.String(nsUri)));
				}
			}
			return this.f.StrParseQName(qilName, qilList);
		}

		// Token: 0x0600382E RID: 14382 RVA: 0x0013B242 File Offset: 0x00139442
		private QilNode GenerateApply(StylesheetLevel sheet, XslNode node)
		{
			if (this.compiler.Settings.CheckOnly)
			{
				return this.f.Sequence();
			}
			return this.InvokeApplyFunction(sheet, node.Name, node.Content);
		}

		// Token: 0x0600382F RID: 14383 RVA: 0x0013B278 File Offset: 0x00139478
		private void SetArg(IList<XslNode> args, int pos, QilName name, QilNode value)
		{
			VarPar varPar;
			if (args.Count <= pos || args[pos].Name != name)
			{
				varPar = AstFactory.WithParam(name);
				args.Insert(pos, varPar);
			}
			else
			{
				varPar = (VarPar)args[pos];
			}
			varPar.Value = value;
		}

		// Token: 0x06003830 RID: 14384 RVA: 0x0013B2C8 File Offset: 0x001394C8
		private IList<XslNode> AddRemoveImplicitArgs(IList<XslNode> args, XslFlags flags)
		{
			if (this.IsDebug)
			{
				flags = XslFlags.FocusFilter;
			}
			if ((flags & XslFlags.FocusFilter) != XslFlags.None)
			{
				if (args == null || args.IsReadOnly)
				{
					args = new List<XslNode>(3);
				}
				int num = 0;
				if ((flags & XslFlags.Current) != XslFlags.None)
				{
					this.SetArg(args, num++, this.nameCurrent, this.GetCurrentNode());
				}
				if ((flags & XslFlags.Position) != XslFlags.None)
				{
					this.SetArg(args, num++, this.namePosition, this.GetCurrentPosition());
				}
				if ((flags & XslFlags.Last) != XslFlags.None)
				{
					this.SetArg(args, num++, this.nameLast, this.GetLastPosition());
				}
			}
			return args;
		}

		// Token: 0x06003831 RID: 14385 RVA: 0x0013B368 File Offset: 0x00139568
		private bool FillupInvokeArgs(IList<QilNode> formalArgs, IList<XslNode> actualArgs, QilList invokeArgs)
		{
			if (actualArgs.Count != formalArgs.Count)
			{
				return false;
			}
			invokeArgs.Clear();
			for (int i = 0; i < formalArgs.Count; i++)
			{
				QilName name = ((QilParameter)formalArgs[i]).Name;
				XmlQueryType xmlType = formalArgs[i].XmlType;
				QilNode qilNode = null;
				int j = 0;
				while (j < actualArgs.Count)
				{
					VarPar varPar = (VarPar)actualArgs[j];
					if (name.Equals(varPar.Name))
					{
						QilNode value = varPar.Value;
						XmlQueryType xmlType2 = value.XmlType;
						if (xmlType2 != xmlType && (!xmlType2.IsNode || !xmlType.IsNode || !xmlType2.IsSubtypeOf(xmlType)))
						{
							return false;
						}
						qilNode = value;
						break;
					}
					else
					{
						j++;
					}
				}
				if (qilNode == null)
				{
					return false;
				}
				invokeArgs.Add(qilNode);
			}
			return true;
		}

		// Token: 0x06003832 RID: 14386 RVA: 0x0013B444 File Offset: 0x00139644
		private QilNode InvokeApplyFunction(StylesheetLevel sheet, QilName mode, IList<XslNode> actualArgs)
		{
			XslFlags xslFlags;
			if (!sheet.ModeFlags.TryGetValue(mode, out xslFlags))
			{
				xslFlags = XslFlags.None;
			}
			xslFlags |= XslFlags.Current;
			actualArgs = this.AddRemoveImplicitArgs(actualArgs, xslFlags);
			QilList qilList = this.f.ActualParameterList();
			QilFunction qilFunction = null;
			List<QilFunction> list;
			if (!sheet.ApplyFunctions.TryGetValue(mode, out list))
			{
				list = (sheet.ApplyFunctions[mode] = new List<QilFunction>());
			}
			foreach (QilFunction qilFunction2 in list)
			{
				if (this.FillupInvokeArgs(qilFunction2.Arguments, actualArgs, qilList))
				{
					qilFunction = qilFunction2;
					break;
				}
			}
			if (qilFunction == null)
			{
				qilList.Clear();
				QilList qilList2 = this.f.FormalParameterList();
				for (int i = 0; i < actualArgs.Count; i++)
				{
					VarPar varPar = (VarPar)actualArgs[i];
					qilList.Add(varPar.Value);
					QilParameter qilParameter = this.f.Parameter((i == 0) ? XmlQueryTypeFactory.NodeNotRtf : varPar.Value.XmlType);
					qilParameter.Name = this.CloneName(varPar.Name);
					qilList2.Add(qilParameter);
					varPar.Value = qilParameter;
				}
				qilFunction = this.f.Function(qilList2, this.f.Boolean((xslFlags & XslFlags.SideEffects) > XslFlags.None), XmlQueryTypeFactory.NodeNotRtfS);
				string text = ((mode.LocalName.Length == 0) ? string.Empty : (" mode=\"" + mode.QualifiedName + "\""));
				qilFunction.DebugName = ((sheet is RootLevel) ? "<xsl:apply-templates" : "<xsl:apply-imports") + text + ">";
				list.Add(qilFunction);
				this.functions.Add(qilFunction);
				QilIterator qilIterator = (QilIterator)qilList2[0];
				QilIterator qilIterator2 = this.f.For(this.f.Content(qilIterator));
				QilNode qilNode = this.f.Filter(qilIterator2, this.f.IsType(qilIterator2, XmlQueryTypeFactory.Content));
				qilNode.XmlType = XmlQueryTypeFactory.ContentS;
				LoopFocus loopFocus = this.curLoop;
				this.curLoop.SetFocus(this.f.For(qilNode));
				QilNode qilNode2 = this.InvokeApplyFunction(this.compiler.Root, mode, null);
				if (this.IsDebug)
				{
					qilNode2 = this.f.Sequence(this.InvokeOnCurrentNodeChanged(), qilNode2);
				}
				QilLoop qilLoop = this.curLoop.ConstructLoop(qilNode2);
				this.curLoop = loopFocus;
				QilTernary qilTernary = this.f.BaseFactory.Conditional(this.f.IsType(qilIterator, this.elementOrDocumentType), qilLoop, this.f.Conditional(this.f.IsType(qilIterator, this.textOrAttributeType), this.f.TextCtor(this.f.XPathNodeValue(qilIterator)), this.f.Sequence()));
				this.matcherBuilder.CollectPatterns(sheet, mode);
				qilFunction.Definition = this.matcherBuilder.BuildMatcher(qilIterator, actualArgs, qilTernary);
			}
			return this.f.Invoke(qilFunction, qilList);
		}

		// Token: 0x06003833 RID: 14387 RVA: 0x0013B774 File Offset: 0x00139974
		public void ReportError(string res, params string[] args)
		{
			this.compiler.ReportError(this.lastScope.SourceLine, res, args);
		}

		// Token: 0x06003834 RID: 14388 RVA: 0x0013B78E File Offset: 0x0013998E
		public void ReportWarning(string res, params string[] args)
		{
			this.compiler.ReportWarning(this.lastScope.SourceLine, res, args);
		}

		// Token: 0x06003835 RID: 14389 RVA: 0x00002F50 File Offset: 0x00001150
		[Conditional("DEBUG")]
		private void VerifyXPathQName(QilName qname)
		{
		}

		// Token: 0x06003836 RID: 14390 RVA: 0x0013B7A8 File Offset: 0x001399A8
		private string ResolvePrefix(bool ignoreDefaultNs, string prefix)
		{
			if (ignoreDefaultNs && prefix.Length == 0)
			{
				return string.Empty;
			}
			string text = this.scope.LookupNamespace(prefix);
			if (text == null)
			{
				if (prefix.Length == 0)
				{
					text = string.Empty;
				}
				else
				{
					this.ReportError("Prefix '{0}' is not defined.", new string[] { prefix });
					text = this.compiler.CreatePhantomNamespace();
				}
			}
			return text;
		}

		// Token: 0x06003837 RID: 14391 RVA: 0x0013B808 File Offset: 0x00139A08
		private void SetLineInfoCheck(QilNode n, ISourceLineInfo lineInfo)
		{
			if (n.SourceLine == null)
			{
				QilGenerator.SetLineInfo(n, lineInfo);
			}
		}

		// Token: 0x06003838 RID: 14392 RVA: 0x0013B81C File Offset: 0x00139A1C
		private static QilNode SetLineInfo(QilNode n, ISourceLineInfo lineInfo)
		{
			if (lineInfo != null && 0 < lineInfo.Start.Line && lineInfo.Start.LessOrEqual(lineInfo.End))
			{
				n.SourceLine = lineInfo;
			}
			return n;
		}

		// Token: 0x06003839 RID: 14393 RVA: 0x0013B85C File Offset: 0x00139A5C
		private QilNode AddDebugVariable(QilName name, QilNode value, QilNode content)
		{
			QilIterator qilIterator = this.f.Let(value);
			qilIterator.DebugName = name.ToString();
			return this.f.Loop(qilIterator, content);
		}

		// Token: 0x0600383A RID: 14394 RVA: 0x0013B890 File Offset: 0x00139A90
		private QilNode SetDebugNs(QilNode n, QilList nsList)
		{
			if (n != null && nsList != null)
			{
				QilNode qilNode = this.GetNsVar(nsList);
				if (qilNode.XmlType.Cardinality == XmlQueryCardinality.One)
				{
					qilNode = this.f.TypeAssert(qilNode, XmlQueryTypeFactory.NamespaceS);
				}
				n = this.AddDebugVariable(this.CloneName(this.nameNamespaces), qilNode, n);
			}
			return n;
		}

		// Token: 0x0600383B RID: 14395 RVA: 0x0013B8EC File Offset: 0x00139AEC
		private QilNode AddCurrentPositionLast(QilNode content)
		{
			if (this.IsDebug)
			{
				content = this.AddDebugVariable(this.CloneName(this.nameLast), this.GetLastPosition(), content);
				content = this.AddDebugVariable(this.CloneName(this.namePosition), this.GetCurrentPosition(), content);
				content = this.AddDebugVariable(this.CloneName(this.nameCurrent), this.GetCurrentNode(), content);
			}
			return content;
		}

		// Token: 0x0600383C RID: 14396 RVA: 0x0013B953 File Offset: 0x00139B53
		private QilName CloneName(QilName name)
		{
			return (QilName)name.ShallowClone(this.f.BaseFactory);
		}

		// Token: 0x0600383D RID: 14397 RVA: 0x0013B96B File Offset: 0x00139B6B
		private void SetEnvironmentFlags(bool allowVariables, bool allowCurrent, bool allowKey)
		{
			this.allowVariables = allowVariables;
			this.allowCurrent = allowCurrent;
			this.allowKey = allowKey;
		}

		// Token: 0x17000BC0 RID: 3008
		// (get) Token: 0x0600383E RID: 14398 RVA: 0x0013B982 File Offset: 0x00139B82
		XPathQilFactory IXPathEnvironment.Factory
		{
			get
			{
				return this.f;
			}
		}

		// Token: 0x0600383F RID: 14399 RVA: 0x0013B98A File Offset: 0x00139B8A
		QilNode IFocus.GetCurrent()
		{
			return this.GetCurrentNode();
		}

		// Token: 0x06003840 RID: 14400 RVA: 0x0013B992 File Offset: 0x00139B92
		QilNode IFocus.GetPosition()
		{
			return this.GetCurrentPosition();
		}

		// Token: 0x06003841 RID: 14401 RVA: 0x0013B99A File Offset: 0x00139B9A
		QilNode IFocus.GetLast()
		{
			return this.GetLastPosition();
		}

		// Token: 0x06003842 RID: 14402 RVA: 0x0013B9A2 File Offset: 0x00139BA2
		string IXPathEnvironment.ResolvePrefix(string prefix)
		{
			return this.ResolvePrefixThrow(true, prefix);
		}

		// Token: 0x06003843 RID: 14403 RVA: 0x0013B9AC File Offset: 0x00139BAC
		QilNode IXPathEnvironment.ResolveVariable(string prefix, string name)
		{
			if (!this.allowVariables)
			{
				throw new XslLoadException("Variables cannot be used within this expression.", Array.Empty<string>());
			}
			string text = this.ResolvePrefixThrow(true, prefix);
			QilNode qilNode = this.scope.LookupVariable(name, text);
			if (qilNode == null)
			{
				throw new XslLoadException("The variable or parameter '{0}' is either not defined or it is out of scope.", new string[] { Compiler.ConstructQName(prefix, name) });
			}
			XmlQueryType xmlType = qilNode.XmlType;
			if (qilNode.NodeType == QilNodeType.Parameter && xmlType.IsNode && xmlType.IsNotRtf && xmlType.MaybeMany && !xmlType.IsDod)
			{
				qilNode = this.f.TypeAssert(qilNode, XmlQueryTypeFactory.NodeSDod);
			}
			return qilNode;
		}

		// Token: 0x06003844 RID: 14404 RVA: 0x0013BA4C File Offset: 0x00139C4C
		QilNode IXPathEnvironment.ResolveFunction(string prefix, string name, IList<QilNode> args, IFocus env)
		{
			if (prefix.Length != 0)
			{
				string text = this.ResolvePrefixThrow(true, prefix);
				if (text == "urn:schemas-microsoft-com:xslt")
				{
					if (name == "node-set")
					{
						XPathBuilder.FunctionInfo<QilGenerator.FuncId>.CheckArity(1, 1, name, args.Count);
						return this.CompileMsNodeSet(args[0]);
					}
					if (name == "string-compare")
					{
						XPathBuilder.FunctionInfo<QilGenerator.FuncId>.CheckArity(2, 4, name, args.Count);
						return this.f.InvokeMsStringCompare(this.f.ConvertToString(args[0]), this.f.ConvertToString(args[1]), (2 < args.Count) ? this.f.ConvertToString(args[2]) : this.f.String(string.Empty), (3 < args.Count) ? this.f.ConvertToString(args[3]) : this.f.String(string.Empty));
					}
					if (name == "utc")
					{
						XPathBuilder.FunctionInfo<QilGenerator.FuncId>.CheckArity(1, 1, name, args.Count);
						return this.f.InvokeMsUtc(this.f.ConvertToString(args[0]));
					}
					if (name == "format-date" || name == "format-time")
					{
						XPathBuilder.FunctionInfo<QilGenerator.FuncId>.CheckArity(1, 3, name, args.Count);
						XslVersion xslVersion = this.xslVersion;
						return this.f.InvokeMsFormatDateTime(this.f.ConvertToString(args[0]), (1 < args.Count) ? this.f.ConvertToString(args[1]) : this.f.String(string.Empty), (2 < args.Count) ? this.f.ConvertToString(args[2]) : this.f.String(string.Empty), this.f.Boolean(name == "format-date"));
					}
					if (name == "local-name")
					{
						XPathBuilder.FunctionInfo<QilGenerator.FuncId>.CheckArity(1, 1, name, args.Count);
						return this.f.InvokeMsLocalName(this.f.ConvertToString(args[0]));
					}
					if (name == "namespace-uri")
					{
						XPathBuilder.FunctionInfo<QilGenerator.FuncId>.CheckArity(1, 1, name, args.Count);
						return this.f.InvokeMsNamespaceUri(this.f.ConvertToString(args[0]), env.GetCurrent());
					}
					if (name == "number")
					{
						XPathBuilder.FunctionInfo<QilGenerator.FuncId>.CheckArity(1, 1, name, args.Count);
						return this.f.InvokeMsNumber(args[0]);
					}
				}
				if (text == "http://exslt.org/common")
				{
					if (name == "node-set")
					{
						XPathBuilder.FunctionInfo<QilGenerator.FuncId>.CheckArity(1, 1, name, args.Count);
						return this.CompileMsNodeSet(args[0]);
					}
					if (name == "object-type")
					{
						XPathBuilder.FunctionInfo<QilGenerator.FuncId>.CheckArity(1, 1, name, args.Count);
						return this.EXslObjectType(args[0]);
					}
				}
				for (int i = 0; i < args.Count; i++)
				{
					args[i] = this.f.SafeDocOrderDistinct(args[i]);
				}
				if (this.compiler.Settings.EnableScript)
				{
					XmlExtensionFunction xmlExtensionFunction = this.compiler.Scripts.ResolveFunction(name, text, args.Count, this);
					if (xmlExtensionFunction != null)
					{
						return this.GenerateScriptCall(this.f.QName(name, text, prefix), xmlExtensionFunction, args);
					}
				}
				else if (this.compiler.Scripts.ScriptClasses.ContainsKey(text))
				{
					this.ReportWarning("Execution of scripts was prohibited. Use the XsltSettings.EnableScript property to enable it.", Array.Empty<string>());
					return this.f.Error(this.lastScope.SourceLine, "Execution of scripts was prohibited. Use the XsltSettings.EnableScript property to enable it.", Array.Empty<string>());
				}
				return this.f.XsltInvokeLateBound(this.f.QName(name, text, prefix), args);
			}
			XPathBuilder.FunctionInfo<QilGenerator.FuncId> functionInfo;
			if (!QilGenerator.FunctionTable.TryGetValue(name, out functionInfo))
			{
				throw new XslLoadException("'{0}()' is an unknown XSLT function.", new string[] { Compiler.ConstructQName(prefix, name) });
			}
			functionInfo.CastArguments(args, name, this.f);
			switch (functionInfo.id)
			{
			case QilGenerator.FuncId.Current:
				if (!this.allowCurrent)
				{
					throw new XslLoadException("The 'current()' function cannot be used in a pattern.", Array.Empty<string>());
				}
				return ((IFocus)this).GetCurrent();
			case QilGenerator.FuncId.Document:
				return this.CompileFnDocument(args[0], (args.Count > 1) ? args[1] : null);
			case QilGenerator.FuncId.Key:
				if (!this.allowKey)
				{
					throw new XslLoadException("The 'key()' function cannot be used in 'use' and 'match' attributes of 'xsl:key' element.", Array.Empty<string>());
				}
				return this.CompileFnKey(args[0], args[1], env);
			case QilGenerator.FuncId.FormatNumber:
				return this.CompileFormatNumber(args[0], args[1], (args.Count > 2) ? args[2] : null);
			case QilGenerator.FuncId.UnparsedEntityUri:
				return this.CompileUnparsedEntityUri(args[0]);
			case QilGenerator.FuncId.GenerateId:
				return this.CompileGenerateId((args.Count > 0) ? args[0] : env.GetCurrent());
			case QilGenerator.FuncId.SystemProperty:
				return this.CompileSystemProperty(args[0]);
			case QilGenerator.FuncId.ElementAvailable:
				return this.CompileElementAvailable(args[0]);
			case QilGenerator.FuncId.FunctionAvailable:
				return this.CompileFunctionAvailable(args[0]);
			default:
				return null;
			}
		}

		// Token: 0x06003845 RID: 14405 RVA: 0x0013BF88 File Offset: 0x0013A188
		private QilNode GenerateScriptCall(QilName name, XmlExtensionFunction scrFunc, IList<QilNode> args)
		{
			for (int i = 0; i < args.Count; i++)
			{
				XmlQueryType xmlArgumentType = scrFunc.GetXmlArgumentType(i);
				XmlTypeCode typeCode = xmlArgumentType.TypeCode;
				if (typeCode != XmlTypeCode.Item)
				{
					if (typeCode != XmlTypeCode.Node)
					{
						switch (typeCode)
						{
						case XmlTypeCode.String:
							args[i] = this.f.ConvertToString(args[i]);
							break;
						case XmlTypeCode.Boolean:
							args[i] = this.f.ConvertToBoolean(args[i]);
							break;
						case XmlTypeCode.Double:
							args[i] = this.f.ConvertToNumber(args[i]);
							break;
						}
					}
					else
					{
						args[i] = (xmlArgumentType.IsSingleton ? this.f.ConvertToNode(args[i]) : this.f.ConvertToNodeSet(args[i]));
					}
				}
			}
			return this.f.XsltInvokeEarlyBound(name, scrFunc.Method, scrFunc.XmlReturnType, args);
		}

		// Token: 0x06003846 RID: 14406 RVA: 0x0013C088 File Offset: 0x0013A288
		private string ResolvePrefixThrow(bool ignoreDefaultNs, string prefix)
		{
			if (ignoreDefaultNs && prefix.Length == 0)
			{
				return string.Empty;
			}
			string text = this.scope.LookupNamespace(prefix);
			if (text == null)
			{
				if (prefix.Length != 0)
				{
					throw new XslLoadException("Prefix '{0}' is not defined.", new string[] { prefix });
				}
				text = string.Empty;
			}
			return text;
		}

		// Token: 0x06003847 RID: 14407 RVA: 0x0013C0DC File Offset: 0x0013A2DC
		private static Dictionary<string, XPathBuilder.FunctionInfo<QilGenerator.FuncId>> CreateFunctionTable()
		{
			return new Dictionary<string, XPathBuilder.FunctionInfo<QilGenerator.FuncId>>(16)
			{
				{
					"current",
					new XPathBuilder.FunctionInfo<QilGenerator.FuncId>(QilGenerator.FuncId.Current, 0, 0, null)
				},
				{
					"document",
					new XPathBuilder.FunctionInfo<QilGenerator.FuncId>(QilGenerator.FuncId.Document, 1, 2, QilGenerator.argFnDocument)
				},
				{
					"key",
					new XPathBuilder.FunctionInfo<QilGenerator.FuncId>(QilGenerator.FuncId.Key, 2, 2, QilGenerator.argFnKey)
				},
				{
					"format-number",
					new XPathBuilder.FunctionInfo<QilGenerator.FuncId>(QilGenerator.FuncId.FormatNumber, 2, 3, QilGenerator.argFnFormatNumber)
				},
				{
					"unparsed-entity-uri",
					new XPathBuilder.FunctionInfo<QilGenerator.FuncId>(QilGenerator.FuncId.UnparsedEntityUri, 1, 1, XPathBuilder.argString)
				},
				{
					"generate-id",
					new XPathBuilder.FunctionInfo<QilGenerator.FuncId>(QilGenerator.FuncId.GenerateId, 0, 1, XPathBuilder.argNodeSet)
				},
				{
					"system-property",
					new XPathBuilder.FunctionInfo<QilGenerator.FuncId>(QilGenerator.FuncId.SystemProperty, 1, 1, XPathBuilder.argString)
				},
				{
					"element-available",
					new XPathBuilder.FunctionInfo<QilGenerator.FuncId>(QilGenerator.FuncId.ElementAvailable, 1, 1, XPathBuilder.argString)
				},
				{
					"function-available",
					new XPathBuilder.FunctionInfo<QilGenerator.FuncId>(QilGenerator.FuncId.FunctionAvailable, 1, 1, XPathBuilder.argString)
				}
			};
		}

		// Token: 0x06003848 RID: 14408 RVA: 0x0013C1C4 File Offset: 0x0013A3C4
		public static bool IsFunctionAvailable(string localName, string nsUri)
		{
			if (XPathBuilder.IsFunctionAvailable(localName, nsUri))
			{
				return true;
			}
			if (nsUri.Length == 0)
			{
				return QilGenerator.FunctionTable.ContainsKey(localName) && localName != "unparsed-entity-uri";
			}
			if (nsUri == "urn:schemas-microsoft-com:xslt")
			{
				return localName == "node-set" || localName == "format-date" || localName == "format-time" || localName == "local-name" || localName == "namespace-uri" || localName == "number" || localName == "string-compare" || localName == "utc";
			}
			return nsUri == "http://exslt.org/common" && (localName == "node-set" || localName == "object-type");
		}

		// Token: 0x06003849 RID: 14409 RVA: 0x0013C2A0 File Offset: 0x0013A4A0
		public static bool IsElementAvailable(XmlQualifiedName name)
		{
			if (name.Namespace == "http://www.w3.org/1999/XSL/Transform")
			{
				string name2 = name.Name;
				return name2 == "apply-imports" || name2 == "apply-templates" || name2 == "attribute" || name2 == "call-template" || name2 == "choose" || name2 == "comment" || name2 == "copy" || name2 == "copy-of" || name2 == "element" || name2 == "fallback" || name2 == "for-each" || name2 == "if" || name2 == "message" || name2 == "number" || name2 == "processing-instruction" || name2 == "text" || name2 == "value-of" || name2 == "variable";
			}
			return false;
		}

		// Token: 0x0600384A RID: 14410 RVA: 0x0013C3D0 File Offset: 0x0013A5D0
		private QilNode CompileFnKey(QilNode name, QilNode keys, IFocus env)
		{
			QilNode qilNode;
			if (keys.XmlType.IsNode)
			{
				if (keys.XmlType.IsSingleton)
				{
					qilNode = this.CompileSingleKey(name, this.f.ConvertToString(keys), env);
				}
				else
				{
					QilIterator qilIterator;
					qilNode = this.f.Loop(qilIterator = this.f.For(keys), this.CompileSingleKey(name, this.f.ConvertToString(qilIterator), env));
				}
			}
			else if (keys.XmlType.IsAtomicValue)
			{
				qilNode = this.CompileSingleKey(name, this.f.ConvertToString(keys), env);
			}
			else
			{
				QilIterator qilIterator;
				QilIterator qilIterator2;
				QilIterator qilIterator3;
				qilNode = this.f.Loop(qilIterator2 = this.f.Let(name), this.f.Loop(qilIterator3 = this.f.Let(keys), this.f.Conditional(this.f.Not(this.f.IsType(qilIterator3, XmlQueryTypeFactory.AnyAtomicType)), this.f.Loop(qilIterator = this.f.For(this.f.TypeAssert(qilIterator3, XmlQueryTypeFactory.NodeS)), this.CompileSingleKey(qilIterator2, this.f.ConvertToString(qilIterator), env)), this.CompileSingleKey(qilIterator2, this.f.XsltConvert(qilIterator3, XmlQueryTypeFactory.StringX), env))));
			}
			return this.f.DocOrderDistinct(qilNode);
		}

		// Token: 0x0600384B RID: 14411 RVA: 0x0013C528 File Offset: 0x0013A728
		private QilNode CompileSingleKey(QilNode name, QilNode key, IFocus env)
		{
			QilNode qilNode;
			if (name.NodeType == QilNodeType.LiteralString)
			{
				string text = (QilLiteral)name;
				string text2;
				string text3;
				this.compiler.ParseQName(text, out text2, out text3, default(QilGenerator.ThrowErrorHelper));
				string text4 = this.ResolvePrefixThrow(true, text2);
				QilName qilName = this.f.QName(text3, text4, text2);
				if (!this.compiler.Keys.Contains(qilName))
				{
					throw new XslLoadException("A reference to key '{0}' cannot be resolved. An 'xsl:key' of this name must be declared at the top level of the stylesheet.", new string[] { text });
				}
				qilNode = this.CompileSingleKey(this.compiler.Keys[qilName], key, env);
			}
			else
			{
				if (this.generalKey == null)
				{
					this.generalKey = this.CreateGeneralKeyFunction();
				}
				QilIterator qilIterator = this.f.Let(name);
				QilNode qilNode2 = this.ResolveQNameDynamic(true, qilIterator);
				qilNode = this.f.Invoke(this.generalKey, this.f.ActualParameterList(new QilNode[]
				{
					qilIterator,
					qilNode2,
					key,
					env.GetCurrent()
				}));
				qilNode = this.f.Loop(qilIterator, qilNode);
			}
			return qilNode;
		}

		// Token: 0x0600384C RID: 14412 RVA: 0x0013C648 File Offset: 0x0013A848
		private QilNode CompileSingleKey(List<Key> defList, QilNode key, IFocus env)
		{
			if (defList.Count == 1)
			{
				return this.f.Invoke(defList[0].Function, this.f.ActualParameterList(env.GetCurrent(), key));
			}
			QilIterator qilIterator = this.f.Let(key);
			QilNode qilNode = this.f.Sequence();
			foreach (Key key2 in defList)
			{
				qilNode.Add(this.f.Invoke(key2.Function, this.f.ActualParameterList(env.GetCurrent(), qilIterator)));
			}
			return this.f.Loop(qilIterator, qilNode);
		}

		// Token: 0x0600384D RID: 14413 RVA: 0x0013C714 File Offset: 0x0013A914
		private QilNode CompileSingleKey(List<Key> defList, QilIterator key, QilIterator context)
		{
			QilList qilList = this.f.BaseFactory.Sequence();
			QilNode qilNode = null;
			foreach (Key key2 in defList)
			{
				qilNode = this.f.Invoke(key2.Function, this.f.ActualParameterList(context, key));
				qilList.Add(qilNode);
			}
			if (defList.Count != 1)
			{
				return qilList;
			}
			return qilNode;
		}

		// Token: 0x0600384E RID: 14414 RVA: 0x0013C7A0 File Offset: 0x0013A9A0
		private QilFunction CreateGeneralKeyFunction()
		{
			QilIterator qilIterator = this.f.Parameter(XmlQueryTypeFactory.StringX);
			QilIterator qilIterator2 = this.f.Parameter(XmlQueryTypeFactory.QNameX);
			QilIterator qilIterator3 = this.f.Parameter(XmlQueryTypeFactory.StringX);
			QilIterator qilIterator4 = this.f.Parameter(XmlQueryTypeFactory.NodeNotRtf);
			QilNode qilNode = this.f.Error("A reference to key '{0}' cannot be resolved. An 'xsl:key' of this name must be declared at the top level of the stylesheet.", qilIterator);
			for (int i = 0; i < this.compiler.Keys.Count; i++)
			{
				qilNode = this.f.Conditional(this.f.Eq(qilIterator2, this.compiler.Keys[i][0].Name.DeepClone(this.f.BaseFactory)), this.CompileSingleKey(this.compiler.Keys[i], qilIterator3, qilIterator4), qilNode);
			}
			QilFunction qilFunction = this.f.Function(this.f.FormalParameterList(new QilNode[] { qilIterator, qilIterator2, qilIterator3, qilIterator4 }), qilNode, this.f.False());
			qilFunction.DebugName = "key";
			this.functions.Add(qilFunction);
			return qilFunction;
		}

		// Token: 0x0600384F RID: 14415 RVA: 0x0013C8DC File Offset: 0x0013AADC
		private QilNode CompileFnDocument(QilNode uris, QilNode baseNode)
		{
			if (!this.compiler.Settings.EnableDocumentFunction)
			{
				this.ReportWarning("Execution of the 'document()' function was prohibited. Use the XsltSettings.EnableDocumentFunction property to enable it.", Array.Empty<string>());
				return this.f.Error(this.lastScope.SourceLine, "Execution of the 'document()' function was prohibited. Use the XsltSettings.EnableDocumentFunction property to enable it.", Array.Empty<string>());
			}
			QilNode qilNode;
			if (uris.XmlType.IsNode)
			{
				QilIterator qilIterator;
				qilNode = this.f.DocOrderDistinct(this.f.Loop(qilIterator = this.f.For(uris), this.CompileSingleDocument(this.f.ConvertToString(qilIterator), baseNode ?? qilIterator)));
			}
			else if (uris.XmlType.IsAtomicValue)
			{
				qilNode = this.CompileSingleDocument(this.f.ConvertToString(uris), baseNode);
			}
			else
			{
				QilIterator qilIterator2 = this.f.Let(uris);
				QilIterator qilIterator3 = ((baseNode != null) ? this.f.Let(baseNode) : null);
				QilIterator qilIterator;
				qilNode = this.f.Conditional(this.f.Not(this.f.IsType(qilIterator2, XmlQueryTypeFactory.AnyAtomicType)), this.f.DocOrderDistinct(this.f.Loop(qilIterator = this.f.For(this.f.TypeAssert(qilIterator2, XmlQueryTypeFactory.NodeS)), this.CompileSingleDocument(this.f.ConvertToString(qilIterator), qilIterator3 ?? qilIterator))), this.CompileSingleDocument(this.f.XsltConvert(qilIterator2, XmlQueryTypeFactory.StringX), qilIterator3));
				qilNode = ((baseNode != null) ? this.f.Loop(qilIterator3, qilNode) : qilNode);
				qilNode = this.f.Loop(qilIterator2, qilNode);
			}
			return qilNode;
		}

		// Token: 0x06003850 RID: 14416 RVA: 0x0013CA70 File Offset: 0x0013AC70
		private QilNode CompileSingleDocument(QilNode uri, QilNode baseNode)
		{
			QilNode qilNode;
			if (baseNode == null)
			{
				qilNode = this.f.String(this.lastScope.SourceLine.Uri);
			}
			else if (baseNode.XmlType.IsSingleton)
			{
				qilNode = this.f.InvokeBaseUri(baseNode);
			}
			else
			{
				QilIterator qilIterator;
				qilNode = this.f.StrConcat(this.f.Loop(qilIterator = this.f.FirstNode(baseNode), this.f.InvokeBaseUri(qilIterator)));
			}
			return this.f.DataSource(uri, qilNode);
		}

		// Token: 0x06003851 RID: 14417 RVA: 0x0013CAF8 File Offset: 0x0013ACF8
		private QilNode CompileFormatNumber(QilNode value, QilNode formatPicture, QilNode formatName)
		{
			XmlQualifiedName xmlQualifiedName;
			if (formatName == null)
			{
				xmlQualifiedName = new XmlQualifiedName();
				formatName = this.f.String(string.Empty);
			}
			else if (formatName.NodeType == QilNodeType.LiteralString)
			{
				xmlQualifiedName = this.ResolveQNameThrow(true, formatName);
			}
			else
			{
				xmlQualifiedName = null;
			}
			if (!(xmlQualifiedName != null))
			{
				this.formatNumberDynamicUsed = true;
				QilIterator qilIterator = this.f.Let(formatName);
				QilNode qilNode = this.ResolveQNameDynamic(true, qilIterator);
				return this.f.Loop(qilIterator, this.f.InvokeFormatNumberDynamic(value, formatPicture, qilNode, qilIterator));
			}
			DecimalFormatDecl decimalFormatDecl;
			if (this.compiler.DecimalFormats.Contains(xmlQualifiedName))
			{
				decimalFormatDecl = this.compiler.DecimalFormats[xmlQualifiedName];
			}
			else
			{
				if (xmlQualifiedName != DecimalFormatDecl.Default.Name)
				{
					throw new XslLoadException("Decimal format '{0}' is not defined.", new string[] { (QilLiteral)formatName });
				}
				decimalFormatDecl = DecimalFormatDecl.Default;
			}
			if (formatPicture.NodeType == QilNodeType.LiteralString)
			{
				QilIterator qilIterator2 = this.f.Let(this.f.InvokeRegisterDecimalFormatter(formatPicture, decimalFormatDecl));
				QilReference qilReference = qilIterator2;
				QilPatternFactory qilPatternFactory = this.f;
				object obj = "formatter";
				int num = this.formatterCnt;
				this.formatterCnt = num + 1;
				qilReference.DebugName = qilPatternFactory.QName(obj + num, "urn:schemas-microsoft-com:xslt-debug").ToString();
				this.gloVars.Add(qilIterator2);
				return this.f.InvokeFormatNumberStatic(value, qilIterator2);
			}
			this.formatNumberDynamicUsed = true;
			QilNode qilNode2 = this.f.QName(xmlQualifiedName.Name, xmlQualifiedName.Namespace);
			return this.f.InvokeFormatNumberDynamic(value, formatPicture, qilNode2, formatName);
		}

		// Token: 0x06003852 RID: 14418 RVA: 0x0013CC8B File Offset: 0x0013AE8B
		private QilNode CompileUnparsedEntityUri(QilNode n)
		{
			return this.f.Error(this.lastScope.SourceLine, "'{0}()' is an unsupported XSLT function.", new string[] { "unparsed-entity-uri" });
		}

		// Token: 0x06003853 RID: 14419 RVA: 0x0013CCB8 File Offset: 0x0013AEB8
		private QilNode CompileGenerateId(QilNode n)
		{
			if (n.XmlType.IsSingleton)
			{
				return this.f.XsltGenerateId(n);
			}
			QilIterator qilIterator;
			return this.f.StrConcat(this.f.Loop(qilIterator = this.f.FirstNode(n), this.f.XsltGenerateId(qilIterator)));
		}

		// Token: 0x06003854 RID: 14420 RVA: 0x0013CD10 File Offset: 0x0013AF10
		private XmlQualifiedName ResolveQNameThrow(bool ignoreDefaultNs, QilNode qilName)
		{
			string text = (QilLiteral)qilName;
			string text2;
			string text3;
			this.compiler.ParseQName(text, out text2, out text3, default(QilGenerator.ThrowErrorHelper));
			string text4 = this.ResolvePrefixThrow(ignoreDefaultNs, text2);
			return new XmlQualifiedName(text3, text4);
		}

		// Token: 0x06003855 RID: 14421 RVA: 0x0013CD5C File Offset: 0x0013AF5C
		private QilNode CompileSystemProperty(QilNode name)
		{
			if (name.NodeType == QilNodeType.LiteralString)
			{
				XmlQualifiedName xmlQualifiedName = this.ResolveQNameThrow(true, name);
				if (this.EvaluateFuncCalls)
				{
					XPathItem xpathItem = XsltFunctions.SystemProperty(xmlQualifiedName);
					if (xpathItem.ValueType == XsltConvert.StringType)
					{
						return this.f.String(xpathItem.Value);
					}
					return this.f.Double(xpathItem.ValueAsDouble);
				}
				else
				{
					name = this.f.QName(xmlQualifiedName.Name, xmlQualifiedName.Namespace);
				}
			}
			else
			{
				name = this.ResolveQNameDynamic(true, name);
			}
			return this.f.InvokeSystemProperty(name);
		}

		// Token: 0x06003856 RID: 14422 RVA: 0x0013CDF4 File Offset: 0x0013AFF4
		private QilNode CompileElementAvailable(QilNode name)
		{
			if (name.NodeType == QilNodeType.LiteralString)
			{
				XmlQualifiedName xmlQualifiedName = this.ResolveQNameThrow(false, name);
				if (this.EvaluateFuncCalls)
				{
					return this.f.Boolean(QilGenerator.IsElementAvailable(xmlQualifiedName));
				}
				name = this.f.QName(xmlQualifiedName.Name, xmlQualifiedName.Namespace);
			}
			else
			{
				name = this.ResolveQNameDynamic(false, name);
			}
			return this.f.InvokeElementAvailable(name);
		}

		// Token: 0x06003857 RID: 14423 RVA: 0x0013CE60 File Offset: 0x0013B060
		private QilNode CompileFunctionAvailable(QilNode name)
		{
			if (name.NodeType == QilNodeType.LiteralString)
			{
				XmlQualifiedName xmlQualifiedName = this.ResolveQNameThrow(true, name);
				if (this.EvaluateFuncCalls && (xmlQualifiedName.Namespace.Length == 0 || xmlQualifiedName.Namespace == "http://www.w3.org/1999/XSL/Transform"))
				{
					return this.f.Boolean(QilGenerator.IsFunctionAvailable(xmlQualifiedName.Name, xmlQualifiedName.Namespace));
				}
				name = this.f.QName(xmlQualifiedName.Name, xmlQualifiedName.Namespace);
			}
			else
			{
				name = this.ResolveQNameDynamic(true, name);
			}
			return this.f.InvokeFunctionAvailable(name);
		}

		// Token: 0x06003858 RID: 14424 RVA: 0x0013CEF5 File Offset: 0x0013B0F5
		private QilNode CompileMsNodeSet(QilNode n)
		{
			if (n.XmlType.IsNode && n.XmlType.IsNotRtf)
			{
				return n;
			}
			return this.f.XsltConvert(n, XmlQueryTypeFactory.NodeSDod);
		}

		// Token: 0x06003859 RID: 14425 RVA: 0x0013CF24 File Offset: 0x0013B124
		private QilNode EXslObjectType(QilNode n)
		{
			if (this.EvaluateFuncCalls)
			{
				switch (n.XmlType.TypeCode)
				{
				case XmlTypeCode.String:
					return this.f.String("string");
				case XmlTypeCode.Boolean:
					return this.f.String("boolean");
				case XmlTypeCode.Double:
					return this.f.String("number");
				}
				if (n.XmlType.IsNode && n.XmlType.IsNotRtf)
				{
					return this.f.String("node-set");
				}
			}
			return this.f.InvokeEXslObjectType(n);
		}

		// Token: 0x04002455 RID: 9301
		private CompilerScopeManager<QilIterator> scope;

		// Token: 0x04002456 RID: 9302
		private OutputScopeManager outputScope;

		// Token: 0x04002457 RID: 9303
		private HybridDictionary prefixesInUse;

		// Token: 0x04002458 RID: 9304
		private XsltQilFactory f;

		// Token: 0x04002459 RID: 9305
		private XPathBuilder xpathBuilder;

		// Token: 0x0400245A RID: 9306
		private XPathParser<QilNode> xpathParser;

		// Token: 0x0400245B RID: 9307
		private XPathPatternBuilder ptrnBuilder;

		// Token: 0x0400245C RID: 9308
		private XPathPatternParser ptrnParser;

		// Token: 0x0400245D RID: 9309
		private ReferenceReplacer refReplacer;

		// Token: 0x0400245E RID: 9310
		private KeyMatchBuilder keyMatchBuilder;

		// Token: 0x0400245F RID: 9311
		private InvokeGenerator invkGen;

		// Token: 0x04002460 RID: 9312
		private MatcherBuilder matcherBuilder;

		// Token: 0x04002461 RID: 9313
		private QilStrConcatenator strConcat;

		// Token: 0x04002462 RID: 9314
		private QilGenerator.VariableHelper varHelper;

		// Token: 0x04002463 RID: 9315
		private Compiler compiler;

		// Token: 0x04002464 RID: 9316
		private QilList functions;

		// Token: 0x04002465 RID: 9317
		private QilFunction generalKey;

		// Token: 0x04002466 RID: 9318
		private bool formatNumberDynamicUsed;

		// Token: 0x04002467 RID: 9319
		private QilList extPars;

		// Token: 0x04002468 RID: 9320
		private QilList gloVars;

		// Token: 0x04002469 RID: 9321
		private QilList nsVars;

		// Token: 0x0400246A RID: 9322
		private XmlQueryType elementOrDocumentType;

		// Token: 0x0400246B RID: 9323
		private XmlQueryType textOrAttributeType;

		// Token: 0x0400246C RID: 9324
		private XslNode lastScope;

		// Token: 0x0400246D RID: 9325
		private XslVersion xslVersion;

		// Token: 0x0400246E RID: 9326
		private QilName nameCurrent;

		// Token: 0x0400246F RID: 9327
		private QilName namePosition;

		// Token: 0x04002470 RID: 9328
		private QilName nameLast;

		// Token: 0x04002471 RID: 9329
		private QilName nameNamespaces;

		// Token: 0x04002472 RID: 9330
		private QilName nameInit;

		// Token: 0x04002473 RID: 9331
		private SingletonFocus singlFocus;

		// Token: 0x04002474 RID: 9332
		private FunctionFocus funcFocus;

		// Token: 0x04002475 RID: 9333
		private LoopFocus curLoop;

		// Token: 0x04002476 RID: 9334
		private int formatterCnt;

		// Token: 0x04002477 RID: 9335
		private readonly StringBuilder unescapedText = new StringBuilder();

		// Token: 0x04002478 RID: 9336
		private static readonly char[] curlyBraces = new char[] { '{', '}' };

		// Token: 0x04002479 RID: 9337
		private const XmlNodeKindFlags InvalidatingNodes = XmlNodeKindFlags.Attribute | XmlNodeKindFlags.Namespace;

		// Token: 0x0400247A RID: 9338
		private bool allowVariables = true;

		// Token: 0x0400247B RID: 9339
		private bool allowCurrent = true;

		// Token: 0x0400247C RID: 9340
		private bool allowKey = true;

		// Token: 0x0400247D RID: 9341
		private static readonly XmlTypeCode[] argFnDocument = new XmlTypeCode[]
		{
			XmlTypeCode.Item,
			XmlTypeCode.Node
		};

		// Token: 0x0400247E RID: 9342
		private static readonly XmlTypeCode[] argFnKey = new XmlTypeCode[]
		{
			XmlTypeCode.String,
			XmlTypeCode.Item
		};

		// Token: 0x0400247F RID: 9343
		private static readonly XmlTypeCode[] argFnFormatNumber = new XmlTypeCode[]
		{
			XmlTypeCode.Double,
			XmlTypeCode.String,
			XmlTypeCode.String
		};

		// Token: 0x04002480 RID: 9344
		public static Dictionary<string, XPathBuilder.FunctionInfo<QilGenerator.FuncId>> FunctionTable = QilGenerator.CreateFunctionTable();

		// Token: 0x02000585 RID: 1413
		private class VariableHelper
		{
			// Token: 0x0600385B RID: 14427 RVA: 0x0013D03D File Offset: 0x0013B23D
			public VariableHelper(XPathQilFactory f)
			{
				this.f = f;
			}

			// Token: 0x0600385C RID: 14428 RVA: 0x0013D057 File Offset: 0x0013B257
			public int StartVariables()
			{
				return this.vars.Count;
			}

			// Token: 0x0600385D RID: 14429 RVA: 0x0013D064 File Offset: 0x0013B264
			public void AddVariable(QilIterator let)
			{
				this.vars.Push(let);
			}

			// Token: 0x0600385E RID: 14430 RVA: 0x0013D074 File Offset: 0x0013B274
			public QilNode FinishVariables(QilNode node, int varScope)
			{
				int num = this.vars.Count - varScope;
				while (num-- != 0)
				{
					node = this.f.Loop(this.vars.Pop(), node);
				}
				return node;
			}

			// Token: 0x0600385F RID: 14431 RVA: 0x00002F50 File Offset: 0x00001150
			[Conditional("DEBUG")]
			public void CheckEmpty()
			{
			}

			// Token: 0x04002481 RID: 9345
			private Stack<QilIterator> vars = new Stack<QilIterator>();

			// Token: 0x04002482 RID: 9346
			private XPathQilFactory f;
		}

		// Token: 0x02000586 RID: 1414
		private struct ThrowErrorHelper : IErrorHelper
		{
			// Token: 0x06003860 RID: 14432 RVA: 0x0013D0B2 File Offset: 0x0013B2B2
			public void ReportError(string res, params string[] args)
			{
				throw new XslLoadException("{0}", new string[] { res });
			}

			// Token: 0x06003861 RID: 14433 RVA: 0x00002F50 File Offset: 0x00001150
			public void ReportWarning(string res, params string[] args)
			{
			}
		}

		// Token: 0x02000587 RID: 1415
		public enum FuncId
		{
			// Token: 0x04002484 RID: 9348
			Current,
			// Token: 0x04002485 RID: 9349
			Document,
			// Token: 0x04002486 RID: 9350
			Key,
			// Token: 0x04002487 RID: 9351
			FormatNumber,
			// Token: 0x04002488 RID: 9352
			UnparsedEntityUri,
			// Token: 0x04002489 RID: 9353
			GenerateId,
			// Token: 0x0400248A RID: 9354
			SystemProperty,
			// Token: 0x0400248B RID: 9355
			ElementAvailable,
			// Token: 0x0400248C RID: 9356
			FunctionAvailable
		}
	}
}
