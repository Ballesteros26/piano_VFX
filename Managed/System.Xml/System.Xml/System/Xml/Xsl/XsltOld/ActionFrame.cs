using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.XPath;
using System.Xml.Xsl.XsltOld.Debugger;
using MS.Internal.Xml.XPath;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x020004E4 RID: 1252
	internal class ActionFrame : IStackFrame
	{
		// Token: 0x17000AE4 RID: 2788
		// (get) Token: 0x060032FF RID: 13055 RVA: 0x00124AA3 File Offset: 0x00122CA3
		// (set) Token: 0x06003300 RID: 13056 RVA: 0x00124AAB File Offset: 0x00122CAB
		internal PrefixQName CalulatedName
		{
			get
			{
				return this.calulatedName;
			}
			set
			{
				this.calulatedName = value;
			}
		}

		// Token: 0x17000AE5 RID: 2789
		// (get) Token: 0x06003301 RID: 13057 RVA: 0x00124AB4 File Offset: 0x00122CB4
		// (set) Token: 0x06003302 RID: 13058 RVA: 0x00124ABC File Offset: 0x00122CBC
		internal string StoredOutput
		{
			get
			{
				return this.storedOutput;
			}
			set
			{
				this.storedOutput = value;
			}
		}

		// Token: 0x17000AE6 RID: 2790
		// (get) Token: 0x06003303 RID: 13059 RVA: 0x00124AC5 File Offset: 0x00122CC5
		// (set) Token: 0x06003304 RID: 13060 RVA: 0x00124ACD File Offset: 0x00122CCD
		internal int State
		{
			get
			{
				return this.state;
			}
			set
			{
				this.state = value;
			}
		}

		// Token: 0x17000AE7 RID: 2791
		// (get) Token: 0x06003305 RID: 13061 RVA: 0x00124AD6 File Offset: 0x00122CD6
		// (set) Token: 0x06003306 RID: 13062 RVA: 0x00124ADE File Offset: 0x00122CDE
		internal int Counter
		{
			get
			{
				return this.counter;
			}
			set
			{
				this.counter = value;
			}
		}

		// Token: 0x17000AE8 RID: 2792
		// (get) Token: 0x06003307 RID: 13063 RVA: 0x00124AE7 File Offset: 0x00122CE7
		internal ActionFrame Container
		{
			get
			{
				return this.container;
			}
		}

		// Token: 0x17000AE9 RID: 2793
		// (get) Token: 0x06003308 RID: 13064 RVA: 0x00124AEF File Offset: 0x00122CEF
		internal XPathNavigator Node
		{
			get
			{
				if (this.nodeSet != null)
				{
					return this.nodeSet.Current;
				}
				return null;
			}
		}

		// Token: 0x17000AEA RID: 2794
		// (get) Token: 0x06003309 RID: 13065 RVA: 0x00124B06 File Offset: 0x00122D06
		internal XPathNodeIterator NodeSet
		{
			get
			{
				return this.nodeSet;
			}
		}

		// Token: 0x17000AEB RID: 2795
		// (get) Token: 0x0600330A RID: 13066 RVA: 0x00124B0E File Offset: 0x00122D0E
		internal XPathNodeIterator NewNodeSet
		{
			get
			{
				return this.newNodeSet;
			}
		}

		// Token: 0x0600330B RID: 13067 RVA: 0x00124B18 File Offset: 0x00122D18
		internal int IncrementCounter()
		{
			int num = this.counter + 1;
			this.counter = num;
			return num;
		}

		// Token: 0x0600330C RID: 13068 RVA: 0x00124B36 File Offset: 0x00122D36
		internal void AllocateVariables(int count)
		{
			if (0 < count)
			{
				this.variables = new object[count];
				return;
			}
			this.variables = null;
		}

		// Token: 0x0600330D RID: 13069 RVA: 0x00124B50 File Offset: 0x00122D50
		internal object GetVariable(int index)
		{
			return this.variables[index];
		}

		// Token: 0x0600330E RID: 13070 RVA: 0x00124B5A File Offset: 0x00122D5A
		internal void SetVariable(int index, object value)
		{
			this.variables[index] = value;
		}

		// Token: 0x0600330F RID: 13071 RVA: 0x00124B65 File Offset: 0x00122D65
		internal void SetParameter(XmlQualifiedName name, object value)
		{
			if (this.withParams == null)
			{
				this.withParams = new Hashtable();
			}
			this.withParams[name] = value;
		}

		// Token: 0x06003310 RID: 13072 RVA: 0x00124B87 File Offset: 0x00122D87
		internal void ResetParams()
		{
			if (this.withParams != null)
			{
				this.withParams.Clear();
			}
		}

		// Token: 0x06003311 RID: 13073 RVA: 0x00124B9C File Offset: 0x00122D9C
		internal object GetParameter(XmlQualifiedName name)
		{
			if (this.withParams != null)
			{
				return this.withParams[name];
			}
			return null;
		}

		// Token: 0x06003312 RID: 13074 RVA: 0x00124BB4 File Offset: 0x00122DB4
		internal void InitNodeSet(XPathNodeIterator nodeSet)
		{
			this.nodeSet = nodeSet;
		}

		// Token: 0x06003313 RID: 13075 RVA: 0x00124BBD File Offset: 0x00122DBD
		internal void InitNewNodeSet(XPathNodeIterator nodeSet)
		{
			this.newNodeSet = nodeSet;
		}

		// Token: 0x06003314 RID: 13076 RVA: 0x00124BC8 File Offset: 0x00122DC8
		internal void SortNewNodeSet(Processor proc, ArrayList sortarray)
		{
			int count = sortarray.Count;
			XPathSortComparer xpathSortComparer = new XPathSortComparer(count);
			for (int i = 0; i < count; i++)
			{
				Sort sort = (Sort)sortarray[i];
				Query compiledQuery = proc.GetCompiledQuery(sort.select);
				xpathSortComparer.AddSort(compiledQuery, new XPathComparerHelper(sort.order, sort.caseOrder, sort.lang, sort.dataType));
			}
			List<SortKey> list = new List<SortKey>();
			while (this.NewNextNode(proc))
			{
				XPathNodeIterator xpathNodeIterator = this.nodeSet;
				this.nodeSet = this.newNodeSet;
				SortKey sortKey = new SortKey(count, list.Count, this.newNodeSet.Current.Clone());
				for (int j = 0; j < count; j++)
				{
					sortKey[j] = xpathSortComparer.Expression(j).Evaluate(this.newNodeSet);
				}
				list.Add(sortKey);
				this.nodeSet = xpathNodeIterator;
			}
			list.Sort(xpathSortComparer);
			this.newNodeSet = new ActionFrame.XPathSortArrayIterator(list);
		}

		// Token: 0x06003315 RID: 13077 RVA: 0x00124CC7 File Offset: 0x00122EC7
		internal void Finished()
		{
			this.State = -1;
		}

		// Token: 0x06003316 RID: 13078 RVA: 0x00124CD0 File Offset: 0x00122ED0
		internal void Inherit(ActionFrame parent)
		{
			this.variables = parent.variables;
		}

		// Token: 0x06003317 RID: 13079 RVA: 0x00124CDE File Offset: 0x00122EDE
		private void Init(Action action, ActionFrame container, XPathNodeIterator nodeSet)
		{
			this.state = 0;
			this.action = action;
			this.container = container;
			this.currentAction = 0;
			this.nodeSet = nodeSet;
			this.newNodeSet = null;
		}

		// Token: 0x06003318 RID: 13080 RVA: 0x00124D0A File Offset: 0x00122F0A
		internal void Init(Action action, XPathNodeIterator nodeSet)
		{
			this.Init(action, null, nodeSet);
		}

		// Token: 0x06003319 RID: 13081 RVA: 0x00124D15 File Offset: 0x00122F15
		internal void Init(ActionFrame containerFrame, XPathNodeIterator nodeSet)
		{
			this.Init(containerFrame.GetAction(0), containerFrame, nodeSet);
		}

		// Token: 0x0600331A RID: 13082 RVA: 0x00124D26 File Offset: 0x00122F26
		internal void SetAction(Action action)
		{
			this.SetAction(action, 0);
		}

		// Token: 0x0600331B RID: 13083 RVA: 0x00124D30 File Offset: 0x00122F30
		internal void SetAction(Action action, int state)
		{
			this.action = action;
			this.state = state;
		}

		// Token: 0x0600331C RID: 13084 RVA: 0x00124D40 File Offset: 0x00122F40
		private Action GetAction(int actionIndex)
		{
			return ((ContainerAction)this.action).GetAction(actionIndex);
		}

		// Token: 0x0600331D RID: 13085 RVA: 0x00124D53 File Offset: 0x00122F53
		internal void Exit()
		{
			this.Finished();
			this.container = null;
		}

		// Token: 0x0600331E RID: 13086 RVA: 0x00124D64 File Offset: 0x00122F64
		internal bool Execute(Processor processor)
		{
			if (this.action == null)
			{
				return true;
			}
			this.action.Execute(processor, this);
			if (this.State == -1)
			{
				if (this.container != null)
				{
					this.currentAction++;
					this.action = this.container.GetAction(this.currentAction);
					this.State = 0;
				}
				else
				{
					this.action = null;
				}
				return this.action == null;
			}
			return false;
		}

		// Token: 0x0600331F RID: 13087 RVA: 0x00124DDC File Offset: 0x00122FDC
		internal bool NextNode(Processor proc)
		{
			bool flag = this.nodeSet.MoveNext();
			if (flag && proc.Stylesheet.Whitespace)
			{
				XPathNodeType xpathNodeType = this.nodeSet.Current.NodeType;
				if (xpathNodeType == XPathNodeType.Whitespace)
				{
					XPathNavigator xpathNavigator = this.nodeSet.Current.Clone();
					bool flag2;
					do
					{
						xpathNavigator.MoveTo(this.nodeSet.Current);
						xpathNavigator.MoveToParent();
						flag2 = !proc.Stylesheet.PreserveWhiteSpace(proc, xpathNavigator) && (flag = this.nodeSet.MoveNext());
						xpathNodeType = this.nodeSet.Current.NodeType;
					}
					while (flag2 && xpathNodeType == XPathNodeType.Whitespace);
				}
			}
			return flag;
		}

		// Token: 0x06003320 RID: 13088 RVA: 0x00124E80 File Offset: 0x00123080
		internal bool NewNextNode(Processor proc)
		{
			bool flag = this.newNodeSet.MoveNext();
			if (flag && proc.Stylesheet.Whitespace)
			{
				XPathNodeType xpathNodeType = this.newNodeSet.Current.NodeType;
				if (xpathNodeType == XPathNodeType.Whitespace)
				{
					XPathNavigator xpathNavigator = this.newNodeSet.Current.Clone();
					bool flag2;
					do
					{
						xpathNavigator.MoveTo(this.newNodeSet.Current);
						xpathNavigator.MoveToParent();
						flag2 = !proc.Stylesheet.PreserveWhiteSpace(proc, xpathNavigator) && (flag = this.newNodeSet.MoveNext());
						xpathNodeType = this.newNodeSet.Current.NodeType;
					}
					while (flag2 && xpathNodeType == XPathNodeType.Whitespace);
				}
			}
			return flag;
		}

		// Token: 0x17000AEC RID: 2796
		// (get) Token: 0x06003321 RID: 13089 RVA: 0x00124F24 File Offset: 0x00123124
		XPathNavigator IStackFrame.Instruction
		{
			get
			{
				if (this.action == null)
				{
					return null;
				}
				return this.action.GetDbgData(this).StyleSheet;
			}
		}

		// Token: 0x17000AED RID: 2797
		// (get) Token: 0x06003322 RID: 13090 RVA: 0x00124F41 File Offset: 0x00123141
		XPathNodeIterator IStackFrame.NodeSet
		{
			get
			{
				return this.nodeSet.Clone();
			}
		}

		// Token: 0x06003323 RID: 13091 RVA: 0x00124F4E File Offset: 0x0012314E
		int IStackFrame.GetVariablesCount()
		{
			if (this.action == null)
			{
				return 0;
			}
			return this.action.GetDbgData(this).Variables.Length;
		}

		// Token: 0x06003324 RID: 13092 RVA: 0x00124F6D File Offset: 0x0012316D
		XPathNavigator IStackFrame.GetVariable(int varIndex)
		{
			return this.action.GetDbgData(this).Variables[varIndex].GetDbgData(null).StyleSheet;
		}

		// Token: 0x06003325 RID: 13093 RVA: 0x00124F8D File Offset: 0x0012318D
		object IStackFrame.GetVariableValue(int varIndex)
		{
			return this.GetVariable(this.action.GetDbgData(this).Variables[varIndex].VarKey);
		}

		// Token: 0x04002106 RID: 8454
		private int state;

		// Token: 0x04002107 RID: 8455
		private int counter;

		// Token: 0x04002108 RID: 8456
		private object[] variables;

		// Token: 0x04002109 RID: 8457
		private Hashtable withParams;

		// Token: 0x0400210A RID: 8458
		private Action action;

		// Token: 0x0400210B RID: 8459
		private ActionFrame container;

		// Token: 0x0400210C RID: 8460
		private int currentAction;

		// Token: 0x0400210D RID: 8461
		private XPathNodeIterator nodeSet;

		// Token: 0x0400210E RID: 8462
		private XPathNodeIterator newNodeSet;

		// Token: 0x0400210F RID: 8463
		private PrefixQName calulatedName;

		// Token: 0x04002110 RID: 8464
		private string storedOutput;

		// Token: 0x020004E5 RID: 1253
		private class XPathSortArrayIterator : XPathArrayIterator
		{
			// Token: 0x06003327 RID: 13095 RVA: 0x00124FAD File Offset: 0x001231AD
			public XPathSortArrayIterator(List<SortKey> list)
				: base(list)
			{
			}

			// Token: 0x06003328 RID: 13096 RVA: 0x00124FB6 File Offset: 0x001231B6
			public XPathSortArrayIterator(ActionFrame.XPathSortArrayIterator it)
				: base(it)
			{
			}

			// Token: 0x06003329 RID: 13097 RVA: 0x00124FBF File Offset: 0x001231BF
			public override XPathNodeIterator Clone()
			{
				return new ActionFrame.XPathSortArrayIterator(this);
			}

			// Token: 0x17000AEE RID: 2798
			// (get) Token: 0x0600332A RID: 13098 RVA: 0x00124FC7 File Offset: 0x001231C7
			public override XPathNavigator Current
			{
				get
				{
					return ((SortKey)this.list[this.index - 1]).Node;
				}
			}
		}
	}
}
