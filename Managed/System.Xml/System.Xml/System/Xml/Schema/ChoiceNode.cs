using System;

namespace System.Xml.Schema
{
	// Token: 0x020003A1 RID: 929
	internal sealed class ChoiceNode : InteriorNode
	{
		// Token: 0x06002546 RID: 9542 RVA: 0x000E0964 File Offset: 0x000DEB64
		private static void ConstructChildPos(SyntaxTreeNode child, BitSet firstpos, BitSet lastpos, BitSet[] followpos)
		{
			BitSet bitSet = new BitSet(firstpos.Count);
			BitSet bitSet2 = new BitSet(lastpos.Count);
			child.ConstructPos(bitSet, bitSet2, followpos);
			firstpos.Or(bitSet);
			lastpos.Or(bitSet2);
		}

		// Token: 0x06002547 RID: 9543 RVA: 0x000E09A0 File Offset: 0x000DEBA0
		public override void ConstructPos(BitSet firstpos, BitSet lastpos, BitSet[] followpos)
		{
			BitSet bitSet = new BitSet(firstpos.Count);
			BitSet bitSet2 = new BitSet(lastpos.Count);
			ChoiceNode choiceNode = this;
			SyntaxTreeNode leftChild;
			do
			{
				ChoiceNode.ConstructChildPos(choiceNode.RightChild, bitSet, bitSet2, followpos);
				leftChild = choiceNode.LeftChild;
				choiceNode = leftChild as ChoiceNode;
			}
			while (choiceNode != null);
			leftChild.ConstructPos(firstpos, lastpos, followpos);
			firstpos.Or(bitSet);
			lastpos.Or(bitSet2);
		}

		// Token: 0x17000776 RID: 1910
		// (get) Token: 0x06002548 RID: 9544 RVA: 0x000E0A00 File Offset: 0x000DEC00
		public override bool IsNullable
		{
			get
			{
				ChoiceNode choiceNode = this;
				while (!choiceNode.RightChild.IsNullable)
				{
					SyntaxTreeNode leftChild = choiceNode.LeftChild;
					choiceNode = leftChild as ChoiceNode;
					if (choiceNode == null)
					{
						return leftChild.IsNullable;
					}
				}
				return true;
			}
		}

		// Token: 0x06002549 RID: 9545 RVA: 0x000E0929 File Offset: 0x000DEB29
		public override void ExpandTree(InteriorNode parent, SymbolsDictionary symbols, Positions positions)
		{
			base.ExpandTreeNoRecursive(parent, symbols, positions);
		}
	}
}
