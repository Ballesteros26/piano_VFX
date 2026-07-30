using System;
using System.Collections.Generic;

namespace System.Xml.Schema
{
	// Token: 0x0200039F RID: 927
	internal sealed class SequenceNode : InteriorNode
	{
		// Token: 0x06002541 RID: 9537 RVA: 0x000E0754 File Offset: 0x000DE954
		public override void ConstructPos(BitSet firstpos, BitSet lastpos, BitSet[] followpos)
		{
			Stack<SequenceNode.SequenceConstructPosContext> stack = new Stack<SequenceNode.SequenceConstructPosContext>();
			SequenceNode.SequenceConstructPosContext sequenceConstructPosContext = new SequenceNode.SequenceConstructPosContext(this, firstpos, lastpos);
			SequenceNode sequenceNode;
			for (;;)
			{
				sequenceNode = sequenceConstructPosContext.this_;
				sequenceConstructPosContext.lastposLeft = new BitSet(lastpos.Count);
				if (!(sequenceNode.LeftChild is SequenceNode))
				{
					break;
				}
				stack.Push(sequenceConstructPosContext);
				sequenceConstructPosContext = new SequenceNode.SequenceConstructPosContext((SequenceNode)sequenceNode.LeftChild, sequenceConstructPosContext.firstpos, sequenceConstructPosContext.lastposLeft);
			}
			sequenceNode.LeftChild.ConstructPos(sequenceConstructPosContext.firstpos, sequenceConstructPosContext.lastposLeft, followpos);
			for (;;)
			{
				sequenceConstructPosContext.firstposRight = new BitSet(firstpos.Count);
				sequenceNode.RightChild.ConstructPos(sequenceConstructPosContext.firstposRight, sequenceConstructPosContext.lastpos, followpos);
				if (sequenceNode.LeftChild.IsNullable && !sequenceNode.RightChild.IsRangeNode)
				{
					sequenceConstructPosContext.firstpos.Or(sequenceConstructPosContext.firstposRight);
				}
				if (sequenceNode.RightChild.IsNullable)
				{
					sequenceConstructPosContext.lastpos.Or(sequenceConstructPosContext.lastposLeft);
				}
				for (int num = sequenceConstructPosContext.lastposLeft.NextSet(-1); num != -1; num = sequenceConstructPosContext.lastposLeft.NextSet(num))
				{
					followpos[num].Or(sequenceConstructPosContext.firstposRight);
				}
				if (sequenceNode.RightChild.IsRangeNode)
				{
					((LeafRangeNode)sequenceNode.RightChild).NextIteration = sequenceConstructPosContext.firstpos.Clone();
				}
				if (stack.Count == 0)
				{
					break;
				}
				sequenceConstructPosContext = stack.Pop();
				sequenceNode = sequenceConstructPosContext.this_;
			}
		}

		// Token: 0x17000775 RID: 1909
		// (get) Token: 0x06002542 RID: 9538 RVA: 0x000E08BC File Offset: 0x000DEABC
		public override bool IsNullable
		{
			get
			{
				SequenceNode sequenceNode = this;
				while (!sequenceNode.RightChild.IsRangeNode || !(((LeafRangeNode)sequenceNode.RightChild).Min == 0m))
				{
					if (!sequenceNode.RightChild.IsNullable && !sequenceNode.RightChild.IsRangeNode)
					{
						return false;
					}
					SyntaxTreeNode leftChild = sequenceNode.LeftChild;
					sequenceNode = leftChild as SequenceNode;
					if (sequenceNode == null)
					{
						return leftChild.IsNullable;
					}
				}
				return true;
			}
		}

		// Token: 0x06002543 RID: 9539 RVA: 0x000E0929 File Offset: 0x000DEB29
		public override void ExpandTree(InteriorNode parent, SymbolsDictionary symbols, Positions positions)
		{
			base.ExpandTreeNoRecursive(parent, symbols, positions);
		}

		// Token: 0x020003A0 RID: 928
		private struct SequenceConstructPosContext
		{
			// Token: 0x06002545 RID: 9541 RVA: 0x000E093C File Offset: 0x000DEB3C
			public SequenceConstructPosContext(SequenceNode node, BitSet firstpos, BitSet lastpos)
			{
				this.this_ = node;
				this.firstpos = firstpos;
				this.lastpos = lastpos;
				this.lastposLeft = null;
				this.firstposRight = null;
			}

			// Token: 0x04001930 RID: 6448
			public SequenceNode this_;

			// Token: 0x04001931 RID: 6449
			public BitSet firstpos;

			// Token: 0x04001932 RID: 6450
			public BitSet lastpos;

			// Token: 0x04001933 RID: 6451
			public BitSet lastposLeft;

			// Token: 0x04001934 RID: 6452
			public BitSet firstposRight;
		}
	}
}
