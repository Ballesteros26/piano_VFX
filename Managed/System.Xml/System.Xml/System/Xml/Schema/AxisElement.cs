using System;

namespace System.Xml.Schema
{
	// Token: 0x02000383 RID: 899
	internal class AxisElement
	{
		// Token: 0x17000735 RID: 1845
		// (get) Token: 0x0600246B RID: 9323 RVA: 0x000DDA44 File Offset: 0x000DBC44
		internal DoubleLinkAxis CurNode
		{
			get
			{
				return this.curNode;
			}
		}

		// Token: 0x0600246C RID: 9324 RVA: 0x000DDA4C File Offset: 0x000DBC4C
		internal AxisElement(DoubleLinkAxis node, int depth)
		{
			this.curNode = node;
			this.curDepth = depth;
			this.rootDepth = depth;
			this.isMatch = false;
		}

		// Token: 0x0600246D RID: 9325 RVA: 0x000DDA80 File Offset: 0x000DBC80
		internal void SetDepth(int depth)
		{
			this.curDepth = depth;
			this.rootDepth = depth;
		}

		// Token: 0x0600246E RID: 9326 RVA: 0x000DDAA0 File Offset: 0x000DBCA0
		internal void MoveToParent(int depth, ForwardAxis parent)
		{
			if (depth != this.curDepth - 1)
			{
				if (depth == this.curDepth && this.isMatch)
				{
					this.isMatch = false;
				}
				return;
			}
			if (this.curNode.Input == parent.RootNode && parent.IsDss)
			{
				this.curNode = parent.RootNode;
				this.rootDepth = (this.curDepth = -1);
				return;
			}
			if (this.curNode.Input != null)
			{
				this.curNode = (DoubleLinkAxis)this.curNode.Input;
				this.curDepth--;
				return;
			}
		}

		// Token: 0x0600246F RID: 9327 RVA: 0x000DDB3C File Offset: 0x000DBD3C
		internal bool MoveToChild(string name, string URN, int depth, ForwardAxis parent)
		{
			if (Asttree.IsAttribute(this.curNode))
			{
				return false;
			}
			if (this.isMatch)
			{
				this.isMatch = false;
			}
			if (!AxisStack.Equal(this.curNode.Name, this.curNode.Urn, name, URN))
			{
				return false;
			}
			if (this.curDepth == -1)
			{
				this.SetDepth(depth);
			}
			else if (depth > this.curDepth)
			{
				return false;
			}
			if (this.curNode == parent.TopNode)
			{
				this.isMatch = true;
				return true;
			}
			DoubleLinkAxis doubleLinkAxis = (DoubleLinkAxis)this.curNode.Next;
			if (Asttree.IsAttribute(doubleLinkAxis))
			{
				this.isMatch = true;
				return false;
			}
			this.curNode = doubleLinkAxis;
			this.curDepth++;
			return false;
		}

		// Token: 0x040018C7 RID: 6343
		internal DoubleLinkAxis curNode;

		// Token: 0x040018C8 RID: 6344
		internal int rootDepth;

		// Token: 0x040018C9 RID: 6345
		internal int curDepth;

		// Token: 0x040018CA RID: 6346
		internal bool isMatch;
	}
}
