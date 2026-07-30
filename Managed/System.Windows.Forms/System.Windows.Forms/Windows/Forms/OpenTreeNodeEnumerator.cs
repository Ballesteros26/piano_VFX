using System;
using System.Collections;

namespace System.Windows.Forms
{
	// Token: 0x0200027E RID: 638
	internal class OpenTreeNodeEnumerator : IEnumerator
	{
		// Token: 0x0600298E RID: 10638 RVA: 0x000A0498 File Offset: 0x0009E698
		public OpenTreeNodeEnumerator(TreeNode start)
		{
			this.start = start;
		}

		// Token: 0x17000A29 RID: 2601
		// (get) Token: 0x0600298F RID: 10639 RVA: 0x000A04A8 File Offset: 0x0009E6A8
		public object Current
		{
			get
			{
				return this.current;
			}
		}

		// Token: 0x17000A2A RID: 2602
		// (get) Token: 0x06002990 RID: 10640 RVA: 0x000A04B0 File Offset: 0x0009E6B0
		public TreeNode CurrentNode
		{
			get
			{
				return this.current;
			}
		}

		// Token: 0x06002991 RID: 10641 RVA: 0x000A04B8 File Offset: 0x0009E6B8
		public bool MoveNext()
		{
			if (!this.started)
			{
				this.started = true;
				this.current = this.start;
				return this.current != null;
			}
			if (this.current.is_expanded && this.current.Nodes.Count > 0)
			{
				this.current = this.current.Nodes[0];
				return true;
			}
			TreeNode parent = this.current;
			TreeNode treeNode = this.current.NextNode;
			while (treeNode == null)
			{
				if (parent.parent == null)
				{
					return false;
				}
				parent = parent.parent;
				if (parent.parent != null)
				{
					treeNode = parent.NextNode;
				}
			}
			this.current = treeNode;
			return true;
		}

		// Token: 0x06002992 RID: 10642 RVA: 0x000A057C File Offset: 0x0009E77C
		public bool MovePrevious()
		{
			if (!this.started)
			{
				this.started = true;
				this.current = this.start;
				return this.current != null;
			}
			if (this.current.PrevNode != null)
			{
				TreeNode treeNode = this.current.PrevNode;
				for (TreeNode treeNode2 = treeNode; treeNode2 != null; treeNode2 = treeNode2.LastNode)
				{
					treeNode = treeNode2;
					if (!treeNode2.is_expanded)
					{
						break;
					}
				}
				this.current = treeNode;
				return true;
			}
			if (this.current.Parent == null)
			{
				return false;
			}
			this.current = this.current.Parent;
			return true;
		}

		// Token: 0x06002993 RID: 10643 RVA: 0x000A0624 File Offset: 0x0009E824
		public void Reset()
		{
			this.started = false;
		}

		// Token: 0x0400149E RID: 5278
		private TreeNode start;

		// Token: 0x0400149F RID: 5279
		private TreeNode current;

		// Token: 0x040014A0 RID: 5280
		private bool started;
	}
}
