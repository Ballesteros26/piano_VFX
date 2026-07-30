using System;
using System.Collections;

namespace System.Xml.Schema
{
	// Token: 0x02000384 RID: 900
	internal class AxisStack
	{
		// Token: 0x17000736 RID: 1846
		// (get) Token: 0x06002470 RID: 9328 RVA: 0x000DDBF5 File Offset: 0x000DBDF5
		internal ForwardAxis Subtree
		{
			get
			{
				return this.subtree;
			}
		}

		// Token: 0x17000737 RID: 1847
		// (get) Token: 0x06002471 RID: 9329 RVA: 0x000DDBFD File Offset: 0x000DBDFD
		internal int Length
		{
			get
			{
				return this.stack.Count;
			}
		}

		// Token: 0x06002472 RID: 9330 RVA: 0x000DDC0A File Offset: 0x000DBE0A
		public AxisStack(ForwardAxis faxis, ActiveAxis parent)
		{
			this.subtree = faxis;
			this.stack = new ArrayList();
			this.parent = parent;
			if (!faxis.IsDss)
			{
				this.Push(1);
			}
		}

		// Token: 0x06002473 RID: 9331 RVA: 0x000DDC3C File Offset: 0x000DBE3C
		internal void Push(int depth)
		{
			AxisElement axisElement = new AxisElement(this.subtree.RootNode, depth);
			this.stack.Add(axisElement);
		}

		// Token: 0x06002474 RID: 9332 RVA: 0x000DDC68 File Offset: 0x000DBE68
		internal void Pop()
		{
			this.stack.RemoveAt(this.Length - 1);
		}

		// Token: 0x06002475 RID: 9333 RVA: 0x000DDC7D File Offset: 0x000DBE7D
		internal static bool Equal(string thisname, string thisURN, string name, string URN)
		{
			if (thisURN == null)
			{
				if (URN != null && URN.Length != 0)
				{
					return false;
				}
			}
			else if (thisURN.Length != 0 && thisURN != URN)
			{
				return false;
			}
			return thisname.Length == 0 || !(thisname != name);
		}

		// Token: 0x06002476 RID: 9334 RVA: 0x000DDCB8 File Offset: 0x000DBEB8
		internal void MoveToParent(string name, string URN, int depth)
		{
			if (this.subtree.IsSelfAxis)
			{
				return;
			}
			for (int i = 0; i < this.stack.Count; i++)
			{
				((AxisElement)this.stack[i]).MoveToParent(depth, this.subtree);
			}
			if (this.subtree.IsDss && AxisStack.Equal(this.subtree.RootNode.Name, this.subtree.RootNode.Urn, name, URN))
			{
				this.Pop();
			}
		}

		// Token: 0x06002477 RID: 9335 RVA: 0x000DDD44 File Offset: 0x000DBF44
		internal bool MoveToChild(string name, string URN, int depth)
		{
			bool flag = false;
			if (this.subtree.IsDss && AxisStack.Equal(this.subtree.RootNode.Name, this.subtree.RootNode.Urn, name, URN))
			{
				this.Push(-1);
			}
			for (int i = 0; i < this.stack.Count; i++)
			{
				if (((AxisElement)this.stack[i]).MoveToChild(name, URN, depth, this.subtree))
				{
					flag = true;
				}
			}
			return flag;
		}

		// Token: 0x06002478 RID: 9336 RVA: 0x000DDDCC File Offset: 0x000DBFCC
		internal bool MoveToAttribute(string name, string URN, int depth)
		{
			if (!this.subtree.IsAttribute)
			{
				return false;
			}
			if (!AxisStack.Equal(this.subtree.TopNode.Name, this.subtree.TopNode.Urn, name, URN))
			{
				return false;
			}
			bool flag = false;
			if (this.subtree.TopNode.Input == null)
			{
				return this.subtree.IsDss || depth == 1;
			}
			for (int i = 0; i < this.stack.Count; i++)
			{
				AxisElement axisElement = (AxisElement)this.stack[i];
				if (axisElement.isMatch && axisElement.CurNode == this.subtree.TopNode.Input)
				{
					flag = true;
				}
			}
			return flag;
		}

		// Token: 0x040018CB RID: 6347
		private ArrayList stack;

		// Token: 0x040018CC RID: 6348
		private ForwardAxis subtree;

		// Token: 0x040018CD RID: 6349
		private ActiveAxis parent;
	}
}
