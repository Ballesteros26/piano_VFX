using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Security;
using System.Text;
using System.Xml.XPath;
using System.Xml.Xsl.XsltOld.Debugger;
using MS.Internal.Xml.XPath;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x02000532 RID: 1330
	internal sealed class Processor : IXsltProcessor
	{
		// Token: 0x17000B49 RID: 2889
		// (get) Token: 0x06003574 RID: 13684 RVA: 0x0012CF1C File Offset: 0x0012B11C
		internal XPathNavigator Current
		{
			get
			{
				ActionFrame actionFrame = (ActionFrame)this.actionStack.Peek();
				if (actionFrame == null)
				{
					return null;
				}
				return actionFrame.Node;
			}
		}

		// Token: 0x17000B4A RID: 2890
		// (get) Token: 0x06003575 RID: 13685 RVA: 0x0012CF45 File Offset: 0x0012B145
		// (set) Token: 0x06003576 RID: 13686 RVA: 0x0012CF4D File Offset: 0x0012B14D
		internal Processor.ExecResult ExecutionResult
		{
			get
			{
				return this.execResult;
			}
			set
			{
				this.execResult = value;
			}
		}

		// Token: 0x17000B4B RID: 2891
		// (get) Token: 0x06003577 RID: 13687 RVA: 0x0012CF56 File Offset: 0x0012B156
		internal Stylesheet Stylesheet
		{
			get
			{
				return this.stylesheet;
			}
		}

		// Token: 0x17000B4C RID: 2892
		// (get) Token: 0x06003578 RID: 13688 RVA: 0x0012CF5E File Offset: 0x0012B15E
		internal XmlResolver Resolver
		{
			get
			{
				return this.resolver;
			}
		}

		// Token: 0x17000B4D RID: 2893
		// (get) Token: 0x06003579 RID: 13689 RVA: 0x0012CF66 File Offset: 0x0012B166
		internal ArrayList SortArray
		{
			get
			{
				return this.sortArray;
			}
		}

		// Token: 0x17000B4E RID: 2894
		// (get) Token: 0x0600357A RID: 13690 RVA: 0x0012CF6E File Offset: 0x0012B16E
		internal Key[] KeyList
		{
			get
			{
				return this.keyList;
			}
		}

		// Token: 0x0600357B RID: 13691 RVA: 0x0012CF78 File Offset: 0x0012B178
		internal XPathNavigator GetNavigator(Uri ruri)
		{
			XPathNavigator xpathNavigator;
			if (this.documentCache != null)
			{
				xpathNavigator = this.documentCache[ruri] as XPathNavigator;
				if (xpathNavigator != null)
				{
					return xpathNavigator.Clone();
				}
			}
			else
			{
				this.documentCache = new Hashtable();
			}
			object entity = this.resolver.GetEntity(ruri, null, null);
			if (entity is Stream)
			{
				xpathNavigator = ((IXPathNavigable)Compiler.LoadDocument(new XmlTextReaderImpl(ruri.ToString(), (Stream)entity)
				{
					XmlResolver = this.resolver
				})).CreateNavigator();
			}
			else
			{
				if (!(entity is XPathNavigator))
				{
					throw XsltException.Create("Cannot resolve the referenced document '{0}'.", new string[] { ruri.ToString() });
				}
				xpathNavigator = (XPathNavigator)entity;
			}
			this.documentCache[ruri] = xpathNavigator.Clone();
			return xpathNavigator;
		}

		// Token: 0x0600357C RID: 13692 RVA: 0x0012D035 File Offset: 0x0012B235
		internal void AddSort(Sort sortinfo)
		{
			this.sortArray.Add(sortinfo);
		}

		// Token: 0x0600357D RID: 13693 RVA: 0x0012D044 File Offset: 0x0012B244
		internal void InitSortArray()
		{
			if (this.sortArray == null)
			{
				this.sortArray = new ArrayList();
				return;
			}
			this.sortArray.Clear();
		}

		// Token: 0x0600357E RID: 13694 RVA: 0x0012D068 File Offset: 0x0012B268
		internal object GetGlobalParameter(XmlQualifiedName qname)
		{
			object obj = this.args.GetParam(qname.Name, qname.Namespace);
			if (obj == null)
			{
				return null;
			}
			if (!(obj is XPathNodeIterator) && !(obj is XPathNavigator) && !(obj is bool) && !(obj is double) && !(obj is string))
			{
				if (obj is short || obj is ushort || obj is int || obj is uint || obj is long || obj is ulong || obj is float || obj is decimal)
				{
					obj = XmlConvert.ToXPathDouble(obj);
				}
				else
				{
					obj = obj.ToString();
				}
			}
			return obj;
		}

		// Token: 0x0600357F RID: 13695 RVA: 0x0012D110 File Offset: 0x0012B310
		internal object GetExtensionObject(string nsUri)
		{
			return this.args.GetExtensionObject(nsUri);
		}

		// Token: 0x06003580 RID: 13696 RVA: 0x0012D11E File Offset: 0x0012B31E
		internal object GetScriptObject(string nsUri)
		{
			return this.scriptExtensions[nsUri];
		}

		// Token: 0x17000B4F RID: 2895
		// (get) Token: 0x06003581 RID: 13697 RVA: 0x0012D12C File Offset: 0x0012B32C
		internal RootAction RootAction
		{
			get
			{
				return this.rootAction;
			}
		}

		// Token: 0x17000B50 RID: 2896
		// (get) Token: 0x06003582 RID: 13698 RVA: 0x0012D134 File Offset: 0x0012B334
		internal XPathNavigator Document
		{
			get
			{
				return this.document;
			}
		}

		// Token: 0x06003583 RID: 13699 RVA: 0x0012D13C File Offset: 0x0012B33C
		internal StringBuilder GetSharedStringBuilder()
		{
			if (this.sharedStringBuilder == null)
			{
				this.sharedStringBuilder = new StringBuilder();
			}
			else
			{
				this.sharedStringBuilder.Length = 0;
			}
			return this.sharedStringBuilder;
		}

		// Token: 0x06003584 RID: 13700 RVA: 0x00002F50 File Offset: 0x00001150
		internal void ReleaseSharedStringBuilder()
		{
		}

		// Token: 0x17000B51 RID: 2897
		// (get) Token: 0x06003585 RID: 13701 RVA: 0x0012D165 File Offset: 0x0012B365
		internal ArrayList NumberList
		{
			get
			{
				if (this.numberList == null)
				{
					this.numberList = new ArrayList();
				}
				return this.numberList;
			}
		}

		// Token: 0x17000B52 RID: 2898
		// (get) Token: 0x06003586 RID: 13702 RVA: 0x0012D180 File Offset: 0x0012B380
		internal IXsltDebugger Debugger
		{
			get
			{
				return this.debugger;
			}
		}

		// Token: 0x17000B53 RID: 2899
		// (get) Token: 0x06003587 RID: 13703 RVA: 0x0012D188 File Offset: 0x0012B388
		internal HWStack ActionStack
		{
			get
			{
				return this.actionStack;
			}
		}

		// Token: 0x17000B54 RID: 2900
		// (get) Token: 0x06003588 RID: 13704 RVA: 0x0012D190 File Offset: 0x0012B390
		internal RecordBuilder Builder
		{
			get
			{
				return this.builder;
			}
		}

		// Token: 0x17000B55 RID: 2901
		// (get) Token: 0x06003589 RID: 13705 RVA: 0x0012D198 File Offset: 0x0012B398
		internal XsltOutput Output
		{
			get
			{
				return this.output;
			}
		}

		// Token: 0x0600358A RID: 13706 RVA: 0x0012D1A0 File Offset: 0x0012B3A0
		public Processor(XPathNavigator doc, XsltArgumentList args, XmlResolver resolver, Stylesheet stylesheet, List<TheQuery> queryStore, RootAction rootAction, IXsltDebugger debugger)
		{
			this.stylesheet = stylesheet;
			this.queryStore = queryStore;
			this.rootAction = rootAction;
			this.queryList = new Query[queryStore.Count];
			for (int i = 0; i < queryStore.Count; i++)
			{
				this.queryList[i] = Query.Clone(queryStore[i].CompiledQuery.QueryTree);
			}
			this.xsm = new StateMachine();
			this.document = doc;
			this.builder = null;
			this.actionStack = new HWStack(10);
			this.output = this.rootAction.Output;
			this.permissions = this.rootAction.permissions;
			this.resolver = resolver ?? XmlNullResolver.Singleton;
			this.args = args ?? new XsltArgumentList();
			this.debugger = debugger;
			if (this.debugger != null)
			{
				this.debuggerStack = new HWStack(10, 1000);
				this.templateLookup = new TemplateLookupActionDbg();
			}
			if (this.rootAction.KeyList != null)
			{
				this.keyList = new Key[this.rootAction.KeyList.Count];
				for (int j = 0; j < this.keyList.Length; j++)
				{
					this.keyList[j] = this.rootAction.KeyList[j].Clone();
				}
			}
			this.scriptExtensions = new Hashtable(this.stylesheet.ScriptObjectTypes.Count);
			foreach (object obj in this.stylesheet.ScriptObjectTypes)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				string text = (string)dictionaryEntry.Key;
				if (this.GetExtensionObject(text) != null)
				{
					throw XsltException.Create("Namespace '{0}' has a duplicate implementation.", new string[] { text });
				}
				this.scriptExtensions.Add(text, Activator.CreateInstance((Type)dictionaryEntry.Value, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.CreateInstance, null, null, null));
			}
			this.PushActionFrame(this.rootAction, null);
		}

		// Token: 0x0600358B RID: 13707 RVA: 0x0012D3DC File Offset: 0x0012B5DC
		public ReaderOutput StartReader()
		{
			ReaderOutput readerOutput = new ReaderOutput(this);
			this.builder = new RecordBuilder(readerOutput, this.nameTable);
			return readerOutput;
		}

		// Token: 0x0600358C RID: 13708 RVA: 0x0012D404 File Offset: 0x0012B604
		public void Execute(Stream stream)
		{
			RecordOutput recordOutput = null;
			switch (this.output.Method)
			{
			case XsltOutput.OutputMethod.Xml:
			case XsltOutput.OutputMethod.Html:
			case XsltOutput.OutputMethod.Other:
			case XsltOutput.OutputMethod.Unknown:
				recordOutput = new TextOutput(this, stream);
				break;
			case XsltOutput.OutputMethod.Text:
				recordOutput = new TextOnlyOutput(this, stream);
				break;
			}
			this.builder = new RecordBuilder(recordOutput, this.nameTable);
			this.Execute();
		}

		// Token: 0x0600358D RID: 13709 RVA: 0x0012D468 File Offset: 0x0012B668
		public void Execute(TextWriter writer)
		{
			RecordOutput recordOutput = null;
			switch (this.output.Method)
			{
			case XsltOutput.OutputMethod.Xml:
			case XsltOutput.OutputMethod.Html:
			case XsltOutput.OutputMethod.Other:
			case XsltOutput.OutputMethod.Unknown:
				recordOutput = new TextOutput(this, writer);
				break;
			case XsltOutput.OutputMethod.Text:
				recordOutput = new TextOnlyOutput(this, writer);
				break;
			}
			this.builder = new RecordBuilder(recordOutput, this.nameTable);
			this.Execute();
		}

		// Token: 0x0600358E RID: 13710 RVA: 0x0012D4C9 File Offset: 0x0012B6C9
		public void Execute(XmlWriter writer)
		{
			this.builder = new RecordBuilder(new WriterOutput(this, writer), this.nameTable);
			this.Execute();
		}

		// Token: 0x0600358F RID: 13711 RVA: 0x0012D4EC File Offset: 0x0012B6EC
		internal void Execute()
		{
			while (this.execResult == Processor.ExecResult.Continue)
			{
				ActionFrame actionFrame = (ActionFrame)this.actionStack.Peek();
				if (actionFrame == null)
				{
					this.builder.TheEnd();
					this.ExecutionResult = Processor.ExecResult.Done;
					break;
				}
				if (actionFrame.Execute(this))
				{
					this.actionStack.Pop();
				}
			}
			if (this.execResult == Processor.ExecResult.Interrupt)
			{
				this.execResult = Processor.ExecResult.Continue;
			}
		}

		// Token: 0x06003590 RID: 13712 RVA: 0x0012D550 File Offset: 0x0012B750
		internal ActionFrame PushNewFrame()
		{
			ActionFrame actionFrame = (ActionFrame)this.actionStack.Peek();
			ActionFrame actionFrame2 = (ActionFrame)this.actionStack.Push();
			if (actionFrame2 == null)
			{
				actionFrame2 = new ActionFrame();
				this.actionStack.AddToTop(actionFrame2);
			}
			if (actionFrame != null)
			{
				actionFrame2.Inherit(actionFrame);
			}
			return actionFrame2;
		}

		// Token: 0x06003591 RID: 13713 RVA: 0x0012D59F File Offset: 0x0012B79F
		internal void PushActionFrame(Action action, XPathNodeIterator nodeSet)
		{
			this.PushNewFrame().Init(action, nodeSet);
		}

		// Token: 0x06003592 RID: 13714 RVA: 0x0012D5AE File Offset: 0x0012B7AE
		internal void PushActionFrame(ActionFrame container)
		{
			this.PushActionFrame(container, container.NodeSet);
		}

		// Token: 0x06003593 RID: 13715 RVA: 0x0012D5BD File Offset: 0x0012B7BD
		internal void PushActionFrame(ActionFrame container, XPathNodeIterator nodeSet)
		{
			this.PushNewFrame().Init(container, nodeSet);
		}

		// Token: 0x06003594 RID: 13716 RVA: 0x0012D5CC File Offset: 0x0012B7CC
		internal void PushTemplateLookup(XPathNodeIterator nodeSet, XmlQualifiedName mode, Stylesheet importsOf)
		{
			this.templateLookup.Initialize(mode, importsOf);
			this.PushActionFrame(this.templateLookup, nodeSet);
		}

		// Token: 0x06003595 RID: 13717 RVA: 0x0012D5E8 File Offset: 0x0012B7E8
		internal string GetQueryExpression(int key)
		{
			return this.queryStore[key].CompiledQuery.Expression;
		}

		// Token: 0x06003596 RID: 13718 RVA: 0x0012D600 File Offset: 0x0012B800
		internal Query GetCompiledQuery(int key)
		{
			TheQuery theQuery = this.queryStore[key];
			theQuery.CompiledQuery.CheckErrors();
			Query query = Query.Clone(this.queryList[key]);
			query.SetXsltContext(new XsltCompileContext(theQuery._ScopeManager, this));
			return query;
		}

		// Token: 0x06003597 RID: 13719 RVA: 0x0012D644 File Offset: 0x0012B844
		internal Query GetValueQuery(int key)
		{
			return this.GetValueQuery(key, null);
		}

		// Token: 0x06003598 RID: 13720 RVA: 0x0012D650 File Offset: 0x0012B850
		internal Query GetValueQuery(int key, XsltCompileContext context)
		{
			TheQuery theQuery = this.queryStore[key];
			theQuery.CompiledQuery.CheckErrors();
			Query query = this.queryList[key];
			if (context == null)
			{
				context = new XsltCompileContext(theQuery._ScopeManager, this);
			}
			else
			{
				context.Reinitialize(theQuery._ScopeManager, this);
			}
			query.SetXsltContext(context);
			return query;
		}

		// Token: 0x06003599 RID: 13721 RVA: 0x0012D6A4 File Offset: 0x0012B8A4
		private XsltCompileContext GetValueOfContext()
		{
			if (this.valueOfContext == null)
			{
				this.valueOfContext = new XsltCompileContext();
			}
			return this.valueOfContext;
		}

		// Token: 0x0600359A RID: 13722 RVA: 0x0012D6BF File Offset: 0x0012B8BF
		[Conditional("DEBUG")]
		private void RecycleValueOfContext()
		{
			if (this.valueOfContext != null)
			{
				this.valueOfContext.Recycle();
			}
		}

		// Token: 0x0600359B RID: 13723 RVA: 0x0012D6D4 File Offset: 0x0012B8D4
		private XsltCompileContext GetMatchesContext()
		{
			if (this.matchesContext == null)
			{
				this.matchesContext = new XsltCompileContext();
			}
			return this.matchesContext;
		}

		// Token: 0x0600359C RID: 13724 RVA: 0x0012D6EF File Offset: 0x0012B8EF
		[Conditional("DEBUG")]
		private void RecycleMatchesContext()
		{
			if (this.matchesContext != null)
			{
				this.matchesContext.Recycle();
			}
		}

		// Token: 0x0600359D RID: 13725 RVA: 0x0012D704 File Offset: 0x0012B904
		internal string ValueOf(ActionFrame context, int key)
		{
			Query valueQuery = this.GetValueQuery(key, this.GetValueOfContext());
			object obj = valueQuery.Evaluate(context.NodeSet);
			string text;
			if (obj is XPathNodeIterator)
			{
				XPathNavigator xpathNavigator = valueQuery.Advance();
				text = ((xpathNavigator != null) ? this.ValueOf(xpathNavigator) : string.Empty);
			}
			else
			{
				text = XmlConvert.ToXPathString(obj);
			}
			return text;
		}

		// Token: 0x0600359E RID: 13726 RVA: 0x0012D758 File Offset: 0x0012B958
		internal string ValueOf(XPathNavigator n)
		{
			if (this.stylesheet.Whitespace && n.NodeType == XPathNodeType.Element)
			{
				StringBuilder stringBuilder = this.GetSharedStringBuilder();
				this.ElementValueWithoutWS(n, stringBuilder);
				this.ReleaseSharedStringBuilder();
				return stringBuilder.ToString();
			}
			return n.Value;
		}

		// Token: 0x0600359F RID: 13727 RVA: 0x0012D7A0 File Offset: 0x0012B9A0
		private void ElementValueWithoutWS(XPathNavigator nav, StringBuilder builder)
		{
			bool flag = this.Stylesheet.PreserveWhiteSpace(this, nav);
			if (nav.MoveToFirstChild())
			{
				do
				{
					switch (nav.NodeType)
					{
					case XPathNodeType.Element:
						this.ElementValueWithoutWS(nav, builder);
						break;
					case XPathNodeType.Text:
					case XPathNodeType.SignificantWhitespace:
						builder.Append(nav.Value);
						break;
					case XPathNodeType.Whitespace:
						if (flag)
						{
							builder.Append(nav.Value);
						}
						break;
					}
				}
				while (nav.MoveToNext());
				nav.MoveToParent();
			}
		}

		// Token: 0x060035A0 RID: 13728 RVA: 0x0012D824 File Offset: 0x0012BA24
		internal XPathNodeIterator StartQuery(XPathNodeIterator context, int key)
		{
			Query compiledQuery = this.GetCompiledQuery(key);
			if (compiledQuery.Evaluate(context) is XPathNodeIterator)
			{
				return new XPathSelectionIterator(context.Current, compiledQuery);
			}
			throw XsltException.Create("Expression must evaluate to a node-set.", Array.Empty<string>());
		}

		// Token: 0x060035A1 RID: 13729 RVA: 0x0012D863 File Offset: 0x0012BA63
		internal object Evaluate(ActionFrame context, int key)
		{
			return this.GetValueQuery(key).Evaluate(context.NodeSet);
		}

		// Token: 0x060035A2 RID: 13730 RVA: 0x0012D878 File Offset: 0x0012BA78
		internal object RunQuery(ActionFrame context, int key)
		{
			object obj = this.GetCompiledQuery(key).Evaluate(context.NodeSet);
			XPathNodeIterator xpathNodeIterator = obj as XPathNodeIterator;
			if (xpathNodeIterator != null)
			{
				return new XPathArrayIterator(xpathNodeIterator);
			}
			return obj;
		}

		// Token: 0x060035A3 RID: 13731 RVA: 0x0012D8AC File Offset: 0x0012BAAC
		internal string EvaluateString(ActionFrame context, int key)
		{
			object obj = this.Evaluate(context, key);
			string text = null;
			if (obj != null)
			{
				text = XmlConvert.ToXPathString(obj);
			}
			if (text == null)
			{
				text = string.Empty;
			}
			return text;
		}

		// Token: 0x060035A4 RID: 13732 RVA: 0x0012D8D8 File Offset: 0x0012BAD8
		internal bool EvaluateBoolean(ActionFrame context, int key)
		{
			object obj = this.Evaluate(context, key);
			if (obj == null)
			{
				return false;
			}
			XPathNavigator xpathNavigator = obj as XPathNavigator;
			if (xpathNavigator == null)
			{
				return Convert.ToBoolean(obj, CultureInfo.InvariantCulture);
			}
			return Convert.ToBoolean(xpathNavigator.Value, CultureInfo.InvariantCulture);
		}

		// Token: 0x060035A5 RID: 13733 RVA: 0x0012D91C File Offset: 0x0012BB1C
		internal bool Matches(XPathNavigator context, int key)
		{
			Query valueQuery = this.GetValueQuery(key, this.GetMatchesContext());
			bool flag;
			try
			{
				flag = valueQuery.MatchNode(context) != null;
			}
			catch (XPathException)
			{
				throw XsltException.Create("'{0}' is an invalid XSLT pattern.", new string[] { this.GetQueryExpression(key) });
			}
			return flag;
		}

		// Token: 0x17000B56 RID: 2902
		// (get) Token: 0x060035A6 RID: 13734 RVA: 0x0012D974 File Offset: 0x0012BB74
		internal XmlNameTable NameTable
		{
			get
			{
				return this.nameTable;
			}
		}

		// Token: 0x17000B57 RID: 2903
		// (get) Token: 0x060035A7 RID: 13735 RVA: 0x0012D97C File Offset: 0x0012BB7C
		internal bool CanContinue
		{
			get
			{
				return this.execResult == Processor.ExecResult.Continue;
			}
		}

		// Token: 0x17000B58 RID: 2904
		// (get) Token: 0x060035A8 RID: 13736 RVA: 0x0012D987 File Offset: 0x0012BB87
		internal bool ExecutionDone
		{
			get
			{
				return this.execResult == Processor.ExecResult.Done;
			}
		}

		// Token: 0x060035A9 RID: 13737 RVA: 0x0012D992 File Offset: 0x0012BB92
		internal void ResetOutput()
		{
			this.builder.Reset();
		}

		// Token: 0x060035AA RID: 13738 RVA: 0x0012D99F File Offset: 0x0012BB9F
		internal bool BeginEvent(XPathNodeType nodeType, string prefix, string name, string nspace, bool empty)
		{
			return this.BeginEvent(nodeType, prefix, name, nspace, empty, null, true);
		}

		// Token: 0x060035AB RID: 13739 RVA: 0x0012D9B0 File Offset: 0x0012BBB0
		internal bool BeginEvent(XPathNodeType nodeType, string prefix, string name, string nspace, bool empty, object htmlProps, bool search)
		{
			int num = this.xsm.BeginOutlook(nodeType);
			if (this.ignoreLevel > 0 || num == 16)
			{
				this.ignoreLevel++;
				return true;
			}
			switch (this.builder.BeginEvent(num, nodeType, prefix, name, nspace, empty, htmlProps, search))
			{
			case Processor.OutputResult.Continue:
				this.xsm.Begin(nodeType);
				return true;
			case Processor.OutputResult.Interrupt:
				this.xsm.Begin(nodeType);
				this.ExecutionResult = Processor.ExecResult.Interrupt;
				return true;
			case Processor.OutputResult.Overflow:
				this.ExecutionResult = Processor.ExecResult.Interrupt;
				return false;
			case Processor.OutputResult.Error:
				this.ignoreLevel++;
				return true;
			case Processor.OutputResult.Ignore:
				return true;
			default:
				return true;
			}
		}

		// Token: 0x060035AC RID: 13740 RVA: 0x0012DA5D File Offset: 0x0012BC5D
		internal bool TextEvent(string text)
		{
			return this.TextEvent(text, false);
		}

		// Token: 0x060035AD RID: 13741 RVA: 0x0012DA68 File Offset: 0x0012BC68
		internal bool TextEvent(string text, bool disableOutputEscaping)
		{
			if (this.ignoreLevel > 0)
			{
				return true;
			}
			int num = this.xsm.BeginOutlook(XPathNodeType.Text);
			switch (this.builder.TextEvent(num, text, disableOutputEscaping))
			{
			case Processor.OutputResult.Continue:
				this.xsm.Begin(XPathNodeType.Text);
				return true;
			case Processor.OutputResult.Interrupt:
				this.xsm.Begin(XPathNodeType.Text);
				this.ExecutionResult = Processor.ExecResult.Interrupt;
				return true;
			case Processor.OutputResult.Overflow:
				this.ExecutionResult = Processor.ExecResult.Interrupt;
				return false;
			case Processor.OutputResult.Error:
			case Processor.OutputResult.Ignore:
				return true;
			default:
				return true;
			}
		}

		// Token: 0x060035AE RID: 13742 RVA: 0x0012DAEC File Offset: 0x0012BCEC
		internal bool EndEvent(XPathNodeType nodeType)
		{
			if (this.ignoreLevel > 0)
			{
				this.ignoreLevel--;
				return true;
			}
			int num = this.xsm.EndOutlook(nodeType);
			switch (this.builder.EndEvent(num, nodeType))
			{
			case Processor.OutputResult.Continue:
				this.xsm.End(nodeType);
				return true;
			case Processor.OutputResult.Interrupt:
				this.xsm.End(nodeType);
				this.ExecutionResult = Processor.ExecResult.Interrupt;
				return true;
			case Processor.OutputResult.Overflow:
				this.ExecutionResult = Processor.ExecResult.Interrupt;
				return false;
			}
			return true;
		}

		// Token: 0x060035AF RID: 13743 RVA: 0x0012DB78 File Offset: 0x0012BD78
		internal bool CopyBeginEvent(XPathNavigator node, bool emptyflag)
		{
			switch (node.NodeType)
			{
			case XPathNodeType.Element:
			case XPathNodeType.Attribute:
			case XPathNodeType.ProcessingInstruction:
			case XPathNodeType.Comment:
				return this.BeginEvent(node.NodeType, node.Prefix, node.LocalName, node.NamespaceURI, emptyflag);
			case XPathNodeType.Namespace:
				return this.BeginEvent(XPathNodeType.Namespace, null, node.LocalName, node.Value, false);
			}
			return true;
		}

		// Token: 0x060035B0 RID: 13744 RVA: 0x0012DBF4 File Offset: 0x0012BDF4
		internal bool CopyTextEvent(XPathNavigator node)
		{
			switch (node.NodeType)
			{
			case XPathNodeType.Attribute:
			case XPathNodeType.Text:
			case XPathNodeType.SignificantWhitespace:
			case XPathNodeType.Whitespace:
			case XPathNodeType.ProcessingInstruction:
			case XPathNodeType.Comment:
			{
				string value = node.Value;
				return this.TextEvent(value);
			}
			}
			return true;
		}

		// Token: 0x060035B1 RID: 13745 RVA: 0x0012DC48 File Offset: 0x0012BE48
		internal bool CopyEndEvent(XPathNavigator node)
		{
			switch (node.NodeType)
			{
			case XPathNodeType.Element:
			case XPathNodeType.Attribute:
			case XPathNodeType.Namespace:
			case XPathNodeType.ProcessingInstruction:
			case XPathNodeType.Comment:
				return this.EndEvent(node.NodeType);
			}
			return true;
		}

		// Token: 0x060035B2 RID: 13746 RVA: 0x0012DC9A File Offset: 0x0012BE9A
		internal static bool IsRoot(XPathNavigator navigator)
		{
			if (navigator.NodeType == XPathNodeType.Root)
			{
				return true;
			}
			if (navigator.NodeType == XPathNodeType.Element)
			{
				XPathNavigator xpathNavigator = navigator.Clone();
				xpathNavigator.MoveToRoot();
				return xpathNavigator.IsSamePosition(navigator);
			}
			return false;
		}

		// Token: 0x060035B3 RID: 13747 RVA: 0x0012DCC4 File Offset: 0x0012BEC4
		internal void PushOutput(RecordOutput output)
		{
			this.builder.OutputState = this.xsm.State;
			RecordBuilder recordBuilder = this.builder;
			this.builder = new RecordBuilder(output, this.nameTable);
			this.builder.Next = recordBuilder;
			this.xsm.Reset();
		}

		// Token: 0x060035B4 RID: 13748 RVA: 0x0012DD18 File Offset: 0x0012BF18
		internal RecordOutput PopOutput()
		{
			RecordBuilder recordBuilder = this.builder;
			this.builder = recordBuilder.Next;
			this.xsm.State = this.builder.OutputState;
			recordBuilder.TheEnd();
			return recordBuilder.Output;
		}

		// Token: 0x060035B5 RID: 13749 RVA: 0x0012DD5A File Offset: 0x0012BF5A
		internal bool SetDefaultOutput(XsltOutput.OutputMethod method)
		{
			if (this.Output.Method != method)
			{
				this.output = this.output.CreateDerivedOutput(method);
				return true;
			}
			return false;
		}

		// Token: 0x060035B6 RID: 13750 RVA: 0x0012DD80 File Offset: 0x0012BF80
		internal object GetVariableValue(VariableAction variable)
		{
			int varKey = variable.VarKey;
			if (!variable.IsGlobal)
			{
				return ((ActionFrame)this.actionStack.Peek()).GetVariable(varKey);
			}
			ActionFrame actionFrame = (ActionFrame)this.actionStack[0];
			object variable2 = actionFrame.GetVariable(varKey);
			if (variable2 == VariableAction.BeingComputedMark)
			{
				throw XsltException.Create("Circular reference in the definition of variable '{0}'.", new string[] { variable.NameStr });
			}
			if (variable2 != null)
			{
				return variable2;
			}
			int length = this.actionStack.Length;
			ActionFrame actionFrame2 = this.PushNewFrame();
			actionFrame2.Inherit(actionFrame);
			actionFrame2.Init(variable, actionFrame.NodeSet);
			do
			{
				if (((ActionFrame)this.actionStack.Peek()).Execute(this))
				{
					this.actionStack.Pop();
				}
			}
			while (length < this.actionStack.Length);
			return actionFrame.GetVariable(varKey);
		}

		// Token: 0x060035B7 RID: 13751 RVA: 0x0012DE57 File Offset: 0x0012C057
		internal void SetParameter(XmlQualifiedName name, object value)
		{
			((ActionFrame)this.actionStack[this.actionStack.Length - 2]).SetParameter(name, value);
		}

		// Token: 0x060035B8 RID: 13752 RVA: 0x0012DE7D File Offset: 0x0012C07D
		internal void ResetParams()
		{
			((ActionFrame)this.actionStack[this.actionStack.Length - 1]).ResetParams();
		}

		// Token: 0x060035B9 RID: 13753 RVA: 0x0012DEA1 File Offset: 0x0012C0A1
		internal object GetParameter(XmlQualifiedName name)
		{
			return ((ActionFrame)this.actionStack[this.actionStack.Length - 3]).GetParameter(name);
		}

		// Token: 0x060035BA RID: 13754 RVA: 0x0012DEC8 File Offset: 0x0012C0C8
		internal void PushDebuggerStack()
		{
			Processor.DebuggerFrame debuggerFrame = (Processor.DebuggerFrame)this.debuggerStack.Push();
			if (debuggerFrame == null)
			{
				debuggerFrame = new Processor.DebuggerFrame();
				this.debuggerStack.AddToTop(debuggerFrame);
			}
			debuggerFrame.actionFrame = (ActionFrame)this.actionStack.Peek();
		}

		// Token: 0x060035BB RID: 13755 RVA: 0x0012DF11 File Offset: 0x0012C111
		internal void PopDebuggerStack()
		{
			this.debuggerStack.Pop();
		}

		// Token: 0x060035BC RID: 13756 RVA: 0x0012DF1F File Offset: 0x0012C11F
		internal void OnInstructionExecute()
		{
			((Processor.DebuggerFrame)this.debuggerStack.Peek()).actionFrame = (ActionFrame)this.actionStack.Peek();
			this.Debugger.OnInstructionExecute(this);
		}

		// Token: 0x060035BD RID: 13757 RVA: 0x0012DF52 File Offset: 0x0012C152
		internal XmlQualifiedName GetPrevioseMode()
		{
			return ((Processor.DebuggerFrame)this.debuggerStack[this.debuggerStack.Length - 2]).currentMode;
		}

		// Token: 0x060035BE RID: 13758 RVA: 0x0012DF76 File Offset: 0x0012C176
		internal void SetCurrentMode(XmlQualifiedName mode)
		{
			((Processor.DebuggerFrame)this.debuggerStack[this.debuggerStack.Length - 1]).currentMode = mode;
		}

		// Token: 0x17000B59 RID: 2905
		// (get) Token: 0x060035BF RID: 13759 RVA: 0x0012DF9B File Offset: 0x0012C19B
		int IXsltProcessor.StackDepth
		{
			get
			{
				return this.debuggerStack.Length;
			}
		}

		// Token: 0x060035C0 RID: 13760 RVA: 0x0012DFA8 File Offset: 0x0012C1A8
		IStackFrame IXsltProcessor.GetStackFrame(int depth)
		{
			return ((Processor.DebuggerFrame)this.debuggerStack[depth]).actionFrame;
		}

		// Token: 0x04002228 RID: 8744
		private const int StackIncrement = 10;

		// Token: 0x04002229 RID: 8745
		private Processor.ExecResult execResult;

		// Token: 0x0400222A RID: 8746
		private Stylesheet stylesheet;

		// Token: 0x0400222B RID: 8747
		private RootAction rootAction;

		// Token: 0x0400222C RID: 8748
		private Key[] keyList;

		// Token: 0x0400222D RID: 8749
		private List<TheQuery> queryStore;

		// Token: 0x0400222E RID: 8750
		public PermissionSet permissions;

		// Token: 0x0400222F RID: 8751
		private XPathNavigator document;

		// Token: 0x04002230 RID: 8752
		private HWStack actionStack;

		// Token: 0x04002231 RID: 8753
		private HWStack debuggerStack;

		// Token: 0x04002232 RID: 8754
		private StringBuilder sharedStringBuilder;

		// Token: 0x04002233 RID: 8755
		private int ignoreLevel;

		// Token: 0x04002234 RID: 8756
		private StateMachine xsm;

		// Token: 0x04002235 RID: 8757
		private RecordBuilder builder;

		// Token: 0x04002236 RID: 8758
		private XsltOutput output;

		// Token: 0x04002237 RID: 8759
		private XmlNameTable nameTable = new NameTable();

		// Token: 0x04002238 RID: 8760
		private XmlResolver resolver;

		// Token: 0x04002239 RID: 8761
		private XsltArgumentList args;

		// Token: 0x0400223A RID: 8762
		private Hashtable scriptExtensions;

		// Token: 0x0400223B RID: 8763
		private ArrayList numberList;

		// Token: 0x0400223C RID: 8764
		private TemplateLookupAction templateLookup = new TemplateLookupAction();

		// Token: 0x0400223D RID: 8765
		private IXsltDebugger debugger;

		// Token: 0x0400223E RID: 8766
		private Query[] queryList;

		// Token: 0x0400223F RID: 8767
		private ArrayList sortArray;

		// Token: 0x04002240 RID: 8768
		private Hashtable documentCache;

		// Token: 0x04002241 RID: 8769
		private XsltCompileContext valueOfContext;

		// Token: 0x04002242 RID: 8770
		private XsltCompileContext matchesContext;

		// Token: 0x02000533 RID: 1331
		internal enum ExecResult
		{
			// Token: 0x04002244 RID: 8772
			Continue,
			// Token: 0x04002245 RID: 8773
			Interrupt,
			// Token: 0x04002246 RID: 8774
			Done
		}

		// Token: 0x02000534 RID: 1332
		internal enum OutputResult
		{
			// Token: 0x04002248 RID: 8776
			Continue,
			// Token: 0x04002249 RID: 8777
			Interrupt,
			// Token: 0x0400224A RID: 8778
			Overflow,
			// Token: 0x0400224B RID: 8779
			Error,
			// Token: 0x0400224C RID: 8780
			Ignore
		}

		// Token: 0x02000535 RID: 1333
		internal class DebuggerFrame
		{
			// Token: 0x0400224D RID: 8781
			internal ActionFrame actionFrame;

			// Token: 0x0400224E RID: 8782
			internal XmlQualifiedName currentMode;
		}
	}
}
