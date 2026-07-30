using System;
using System.Collections.Generic;

namespace System.Xml.Schema
{
	// Token: 0x0200039E RID: 926
	internal abstract class InteriorNode : SyntaxTreeNode
	{
		// Token: 0x17000773 RID: 1907
		// (get) Token: 0x06002539 RID: 9529 RVA: 0x000E0640 File Offset: 0x000DE840
		// (set) Token: 0x0600253A RID: 9530 RVA: 0x000E0648 File Offset: 0x000DE848
		public SyntaxTreeNode LeftChild
		{
			get
			{
				return this.leftChild;
			}
			set
			{
				this.leftChild = value;
			}
		}

		// Token: 0x17000774 RID: 1908
		// (get) Token: 0x0600253B RID: 9531 RVA: 0x000E0651 File Offset: 0x000DE851
		// (set) Token: 0x0600253C RID: 9532 RVA: 0x000E0659 File Offset: 0x000DE859
		public SyntaxTreeNode RightChild
		{
			get
			{
				return this.rightChild;
			}
			set
			{
				this.rightChild = value;
			}
		}

		// Token: 0x0600253D RID: 9533 RVA: 0x000E0664 File Offset: 0x000DE864
		public override SyntaxTreeNode Clone(Positions positions)
		{
			InteriorNode interiorNode = (InteriorNode)base.MemberwiseClone();
			interiorNode.LeftChild = this.leftChild.Clone(positions);
			if (this.rightChild != null)
			{
				interiorNode.RightChild = this.rightChild.Clone(positions);
			}
			return interiorNode;
		}

		// Token: 0x0600253E RID: 9534 RVA: 0x000E06AC File Offset: 0x000DE8AC
		protected void ExpandTreeNoRecursive(InteriorNode parent, SymbolsDictionary symbols, Positions positions)
		{
			Stack<InteriorNode> stack = new Stack<InteriorNode>();
			InteriorNode interiorNode = this;
			while (interiorNode.leftChild is ChoiceNode || interiorNode.leftChild is SequenceNode)
			{
				stack.Push(interiorNode);
				interiorNode = (InteriorNode)interiorNode.leftChild;
			}
			interiorNode.leftChild.ExpandTree(interiorNode, symbols, positions);
			for (;;)
			{
				if (interiorNode.rightChild != null)
				{
					interiorNode.rightChild.ExpandTree(interiorNode, symbols, positions);
				}
				if (stack.Count == 0)
				{
					break;
				}
				interiorNode = stack.Pop();
			}
		}

		// Token: 0x0600253F RID: 9535 RVA: 0x000E0725 File Offset: 0x000DE925
		public override void ExpandTree(InteriorNode parent, SymbolsDictionary symbols, Positions positions)
		{
			this.leftChild.ExpandTree(this, symbols, positions);
			if (this.rightChild != null)
			{
				this.rightChild.ExpandTree(this, symbols, positions);
			}
		}

		// Token: 0x0400192E RID: 6446
		private SyntaxTreeNode leftChild;

		// Token: 0x0400192F RID: 6447
		private SyntaxTreeNode rightChild;
	}
}
