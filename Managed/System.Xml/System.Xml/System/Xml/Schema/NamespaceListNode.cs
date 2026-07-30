using System;
using System.Collections;

namespace System.Xml.Schema
{
	// Token: 0x0200039D RID: 925
	internal class NamespaceListNode : SyntaxTreeNode
	{
		// Token: 0x06002533 RID: 9523 RVA: 0x000E0565 File Offset: 0x000DE765
		public NamespaceListNode(NamespaceList namespaceList, object particle)
		{
			this.namespaceList = namespaceList;
			this.particle = particle;
		}

		// Token: 0x06002534 RID: 9524 RVA: 0x00007944 File Offset: 0x00005B44
		public override SyntaxTreeNode Clone(Positions positions)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x06002535 RID: 9525 RVA: 0x000E057B File Offset: 0x000DE77B
		public virtual ICollection GetResolvedSymbols(SymbolsDictionary symbols)
		{
			return symbols.GetNamespaceListSymbols(this.namespaceList);
		}

		// Token: 0x06002536 RID: 9526 RVA: 0x000E058C File Offset: 0x000DE78C
		public override void ExpandTree(InteriorNode parent, SymbolsDictionary symbols, Positions positions)
		{
			SyntaxTreeNode syntaxTreeNode = null;
			foreach (object obj in this.GetResolvedSymbols(symbols))
			{
				int num = (int)obj;
				if (symbols.GetParticle(num) != this.particle)
				{
					symbols.IsUpaEnforced = false;
				}
				LeafNode leafNode = new LeafNode(positions.Add(num, this.particle));
				if (syntaxTreeNode == null)
				{
					syntaxTreeNode = leafNode;
				}
				else
				{
					syntaxTreeNode = new ChoiceNode
					{
						LeftChild = syntaxTreeNode,
						RightChild = leafNode
					};
				}
			}
			if (parent.LeftChild == this)
			{
				parent.LeftChild = syntaxTreeNode;
				return;
			}
			parent.RightChild = syntaxTreeNode;
		}

		// Token: 0x06002537 RID: 9527 RVA: 0x00007944 File Offset: 0x00005B44
		public override void ConstructPos(BitSet firstpos, BitSet lastpos, BitSet[] followpos)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x17000772 RID: 1906
		// (get) Token: 0x06002538 RID: 9528 RVA: 0x00007944 File Offset: 0x00005B44
		public override bool IsNullable
		{
			get
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x0400192C RID: 6444
		protected NamespaceList namespaceList;

		// Token: 0x0400192D RID: 6445
		protected object particle;
	}
}
