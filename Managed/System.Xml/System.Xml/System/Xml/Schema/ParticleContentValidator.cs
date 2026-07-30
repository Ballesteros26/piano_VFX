using System;
using System.Collections;

namespace System.Xml.Schema
{
	// Token: 0x020003A7 RID: 935
	internal sealed class ParticleContentValidator : ContentValidator
	{
		// Token: 0x0600256C RID: 9580 RVA: 0x000E0CB8 File Offset: 0x000DEEB8
		public ParticleContentValidator(XmlSchemaContentType contentType)
			: this(contentType, true)
		{
		}

		// Token: 0x0600256D RID: 9581 RVA: 0x000E0CC2 File Offset: 0x000DEEC2
		public ParticleContentValidator(XmlSchemaContentType contentType, bool enableUpaCheck)
			: base(contentType)
		{
			this.enableUpaCheck = enableUpaCheck;
		}

		// Token: 0x0600256E RID: 9582 RVA: 0x00007944 File Offset: 0x00005B44
		public override void InitValidation(ValidationState context)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x0600256F RID: 9583 RVA: 0x00007944 File Offset: 0x00005B44
		public override object ValidateElement(XmlQualifiedName name, ValidationState context, out int errorCode)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x06002570 RID: 9584 RVA: 0x00007944 File Offset: 0x00005B44
		public override bool CompleteValidation(ValidationState context)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x06002571 RID: 9585 RVA: 0x000E0CD2 File Offset: 0x000DEED2
		public void Start()
		{
			this.symbols = new SymbolsDictionary();
			this.positions = new Positions();
			this.stack = new Stack();
		}

		// Token: 0x06002572 RID: 9586 RVA: 0x000E0CF5 File Offset: 0x000DEEF5
		public void OpenGroup()
		{
			this.stack.Push(null);
		}

		// Token: 0x06002573 RID: 9587 RVA: 0x000E0D04 File Offset: 0x000DEF04
		public void CloseGroup()
		{
			SyntaxTreeNode syntaxTreeNode = (SyntaxTreeNode)this.stack.Pop();
			if (syntaxTreeNode == null)
			{
				return;
			}
			if (this.stack.Count == 0)
			{
				this.contentNode = syntaxTreeNode;
				this.isPartial = false;
				return;
			}
			InteriorNode interiorNode = (InteriorNode)this.stack.Pop();
			if (interiorNode != null)
			{
				interiorNode.RightChild = syntaxTreeNode;
				syntaxTreeNode = interiorNode;
				this.isPartial = true;
			}
			else
			{
				this.isPartial = false;
			}
			this.stack.Push(syntaxTreeNode);
		}

		// Token: 0x06002574 RID: 9588 RVA: 0x000E0D7B File Offset: 0x000DEF7B
		public bool Exists(XmlQualifiedName name)
		{
			return this.symbols.Exists(name);
		}

		// Token: 0x06002575 RID: 9589 RVA: 0x000E0D8E File Offset: 0x000DEF8E
		public void AddName(XmlQualifiedName name, object particle)
		{
			this.AddLeafNode(new LeafNode(this.positions.Add(this.symbols.AddName(name, particle), particle)));
		}

		// Token: 0x06002576 RID: 9590 RVA: 0x000E0DB4 File Offset: 0x000DEFB4
		public void AddNamespaceList(NamespaceList namespaceList, object particle)
		{
			this.symbols.AddNamespaceList(namespaceList, particle, false);
			this.AddLeafNode(new NamespaceListNode(namespaceList, particle));
		}

		// Token: 0x06002577 RID: 9591 RVA: 0x000E0DD4 File Offset: 0x000DEFD4
		private void AddLeafNode(SyntaxTreeNode node)
		{
			if (this.stack.Count > 0)
			{
				InteriorNode interiorNode = (InteriorNode)this.stack.Pop();
				if (interiorNode != null)
				{
					interiorNode.RightChild = node;
					node = interiorNode;
				}
			}
			this.stack.Push(node);
			this.isPartial = true;
		}

		// Token: 0x06002578 RID: 9592 RVA: 0x000E0E20 File Offset: 0x000DF020
		public void AddChoice()
		{
			SyntaxTreeNode syntaxTreeNode = (SyntaxTreeNode)this.stack.Pop();
			InteriorNode interiorNode = new ChoiceNode();
			interiorNode.LeftChild = syntaxTreeNode;
			this.stack.Push(interiorNode);
		}

		// Token: 0x06002579 RID: 9593 RVA: 0x000E0E58 File Offset: 0x000DF058
		public void AddSequence()
		{
			SyntaxTreeNode syntaxTreeNode = (SyntaxTreeNode)this.stack.Pop();
			InteriorNode interiorNode = new SequenceNode();
			interiorNode.LeftChild = syntaxTreeNode;
			this.stack.Push(interiorNode);
		}

		// Token: 0x0600257A RID: 9594 RVA: 0x000E0E8F File Offset: 0x000DF08F
		public void AddStar()
		{
			this.Closure(new StarNode());
		}

		// Token: 0x0600257B RID: 9595 RVA: 0x000E0E9C File Offset: 0x000DF09C
		public void AddPlus()
		{
			this.Closure(new PlusNode());
		}

		// Token: 0x0600257C RID: 9596 RVA: 0x000E0EA9 File Offset: 0x000DF0A9
		public void AddQMark()
		{
			this.Closure(new QmarkNode());
		}

		// Token: 0x0600257D RID: 9597 RVA: 0x000E0EB8 File Offset: 0x000DF0B8
		public void AddLeafRange(decimal min, decimal max)
		{
			LeafRangeNode leafRangeNode = new LeafRangeNode(min, max);
			int num = this.positions.Add(-2, leafRangeNode);
			leafRangeNode.Pos = num;
			this.Closure(new SequenceNode
			{
				RightChild = leafRangeNode
			});
			this.minMaxNodesCount++;
		}

		// Token: 0x0600257E RID: 9598 RVA: 0x000E0F08 File Offset: 0x000DF108
		private void Closure(InteriorNode node)
		{
			if (this.stack.Count > 0)
			{
				SyntaxTreeNode syntaxTreeNode = (SyntaxTreeNode)this.stack.Pop();
				InteriorNode interiorNode = syntaxTreeNode as InteriorNode;
				if (this.isPartial && interiorNode != null)
				{
					node.LeftChild = interiorNode.RightChild;
					interiorNode.RightChild = node;
				}
				else
				{
					node.LeftChild = syntaxTreeNode;
					syntaxTreeNode = node;
				}
				this.stack.Push(syntaxTreeNode);
				return;
			}
			if (this.contentNode != null)
			{
				node.LeftChild = this.contentNode;
				this.contentNode = node;
			}
		}

		// Token: 0x0600257F RID: 9599 RVA: 0x000E0F8C File Offset: 0x000DF18C
		public ContentValidator Finish()
		{
			return this.Finish(true);
		}

		// Token: 0x06002580 RID: 9600 RVA: 0x000E0F98 File Offset: 0x000DF198
		public ContentValidator Finish(bool useDFA)
		{
			if (this.contentNode == null)
			{
				if (base.ContentType != XmlSchemaContentType.Mixed)
				{
					return ContentValidator.Empty;
				}
				bool isOpen = base.IsOpen;
				if (!base.IsOpen)
				{
					return ContentValidator.TextOnly;
				}
				return ContentValidator.Any;
			}
			else
			{
				InteriorNode interiorNode = new SequenceNode();
				interiorNode.LeftChild = this.contentNode;
				LeafNode leafNode = new LeafNode(this.positions.Add(this.symbols.AddName(XmlQualifiedName.Empty, null), null));
				interiorNode.RightChild = leafNode;
				this.contentNode.ExpandTree(interiorNode, this.symbols, this.positions);
				int count = this.symbols.Count;
				int count2 = this.positions.Count;
				BitSet bitSet = new BitSet(count2);
				BitSet bitSet2 = new BitSet(count2);
				BitSet[] array = new BitSet[count2];
				for (int i = 0; i < count2; i++)
				{
					array[i] = new BitSet(count2);
				}
				interiorNode.ConstructPos(bitSet, bitSet2, array);
				if (this.minMaxNodesCount > 0)
				{
					BitSet bitSet3;
					BitSet[] array2 = this.CalculateTotalFollowposForRangeNodes(bitSet, array, out bitSet3);
					if (this.enableUpaCheck)
					{
						this.CheckCMUPAWithLeafRangeNodes(this.GetApplicableMinMaxFollowPos(bitSet, bitSet3, array2));
						for (int j = 0; j < count2; j++)
						{
							this.CheckCMUPAWithLeafRangeNodes(this.GetApplicableMinMaxFollowPos(array[j], bitSet3, array2));
						}
					}
					return new RangeContentValidator(bitSet, array, this.symbols, this.positions, leafNode.Pos, base.ContentType, interiorNode.LeftChild.IsNullable, bitSet3, this.minMaxNodesCount);
				}
				int[][] array3 = null;
				if (!this.symbols.IsUpaEnforced)
				{
					if (this.enableUpaCheck)
					{
						this.CheckUniqueParticleAttribution(bitSet, array);
					}
				}
				else if (useDFA)
				{
					array3 = this.BuildTransitionTable(bitSet, array, leafNode.Pos);
				}
				if (array3 != null)
				{
					return new DfaContentValidator(array3, this.symbols, base.ContentType, base.IsOpen, interiorNode.LeftChild.IsNullable);
				}
				return new NfaContentValidator(bitSet, array, this.symbols, this.positions, leafNode.Pos, base.ContentType, base.IsOpen, interiorNode.LeftChild.IsNullable);
			}
		}

		// Token: 0x06002581 RID: 9601 RVA: 0x000E119C File Offset: 0x000DF39C
		private BitSet[] CalculateTotalFollowposForRangeNodes(BitSet firstpos, BitSet[] followpos, out BitSet posWithRangeTerminals)
		{
			int count = this.positions.Count;
			posWithRangeTerminals = new BitSet(count);
			BitSet[] array = new BitSet[this.minMaxNodesCount];
			int num = 0;
			for (int i = count - 1; i >= 0; i--)
			{
				Position position = this.positions[i];
				if (position.symbol == -2)
				{
					LeafRangeNode leafRangeNode = position.particle as LeafRangeNode;
					BitSet bitSet = new BitSet(count);
					bitSet.Clear();
					bitSet.Or(followpos[i]);
					if (leafRangeNode.Min != leafRangeNode.Max)
					{
						bitSet.Or(leafRangeNode.NextIteration);
					}
					for (int num2 = bitSet.NextSet(-1); num2 != -1; num2 = bitSet.NextSet(num2))
					{
						if (num2 > i)
						{
							Position position2 = this.positions[num2];
							if (position2.symbol == -2)
							{
								LeafRangeNode leafRangeNode2 = position2.particle as LeafRangeNode;
								bitSet.Or(array[leafRangeNode2.Pos]);
							}
						}
					}
					array[num] = bitSet;
					leafRangeNode.Pos = num++;
					posWithRangeTerminals.Set(i);
				}
			}
			return array;
		}

		// Token: 0x06002582 RID: 9602 RVA: 0x000E12B8 File Offset: 0x000DF4B8
		private void CheckCMUPAWithLeafRangeNodes(BitSet curpos)
		{
			object[] array = new object[this.symbols.Count];
			for (int num = curpos.NextSet(-1); num != -1; num = curpos.NextSet(num))
			{
				Position position = this.positions[num];
				int symbol = position.symbol;
				if (symbol >= 0)
				{
					if (array[symbol] != null)
					{
						throw new UpaException(array[symbol], position.particle);
					}
					array[symbol] = position.particle;
				}
			}
		}

		// Token: 0x06002583 RID: 9603 RVA: 0x000E1324 File Offset: 0x000DF524
		private BitSet GetApplicableMinMaxFollowPos(BitSet curpos, BitSet posWithRangeTerminals, BitSet[] minmaxFollowPos)
		{
			if (curpos.Intersects(posWithRangeTerminals))
			{
				BitSet bitSet = new BitSet(this.positions.Count);
				bitSet.Or(curpos);
				bitSet.And(posWithRangeTerminals);
				curpos = curpos.Clone();
				for (int num = bitSet.NextSet(-1); num != -1; num = bitSet.NextSet(num))
				{
					LeafRangeNode leafRangeNode = this.positions[num].particle as LeafRangeNode;
					curpos.Or(minmaxFollowPos[leafRangeNode.Pos]);
				}
			}
			return curpos;
		}

		// Token: 0x06002584 RID: 9604 RVA: 0x000E13A0 File Offset: 0x000DF5A0
		private void CheckUniqueParticleAttribution(BitSet firstpos, BitSet[] followpos)
		{
			this.CheckUniqueParticleAttribution(firstpos);
			for (int i = 0; i < this.positions.Count; i++)
			{
				this.CheckUniqueParticleAttribution(followpos[i]);
			}
		}

		// Token: 0x06002585 RID: 9605 RVA: 0x000E13D4 File Offset: 0x000DF5D4
		private void CheckUniqueParticleAttribution(BitSet curpos)
		{
			object[] array = new object[this.symbols.Count];
			for (int num = curpos.NextSet(-1); num != -1; num = curpos.NextSet(num))
			{
				int symbol = this.positions[num].symbol;
				if (array[symbol] == null)
				{
					array[symbol] = this.positions[num].particle;
				}
				else if (array[symbol] != this.positions[num].particle)
				{
					throw new UpaException(array[symbol], this.positions[num].particle);
				}
			}
		}

		// Token: 0x06002586 RID: 9606 RVA: 0x000E1468 File Offset: 0x000DF668
		private int[][] BuildTransitionTable(BitSet firstpos, BitSet[] followpos, int endMarkerPos)
		{
			int count = this.positions.Count;
			int num = 8192 / count;
			int count2 = this.symbols.Count;
			ArrayList arrayList = new ArrayList();
			Hashtable hashtable = new Hashtable();
			hashtable.Add(new BitSet(count), -1);
			Queue queue = new Queue();
			int num2 = 0;
			queue.Enqueue(firstpos);
			hashtable.Add(firstpos, 0);
			arrayList.Add(new int[count2 + 1]);
			while (queue.Count > 0)
			{
				BitSet bitSet = (BitSet)queue.Dequeue();
				int[] array = (int[])arrayList[num2];
				if (bitSet[endMarkerPos])
				{
					array[count2] = 1;
				}
				for (int i = 0; i < count2; i++)
				{
					BitSet bitSet2 = new BitSet(count);
					for (int num3 = bitSet.NextSet(-1); num3 != -1; num3 = bitSet.NextSet(num3))
					{
						if (i == this.positions[num3].symbol)
						{
							bitSet2.Or(followpos[num3]);
						}
					}
					object obj = hashtable[bitSet2];
					if (obj != null)
					{
						array[i] = (int)obj;
					}
					else
					{
						int num4 = hashtable.Count - 1;
						if (num4 >= num)
						{
							return null;
						}
						queue.Enqueue(bitSet2);
						hashtable.Add(bitSet2, num4);
						arrayList.Add(new int[count2 + 1]);
						array[i] = num4;
					}
				}
				num2++;
			}
			return (int[][])arrayList.ToArray(typeof(int[]));
		}

		// Token: 0x0400193F RID: 6463
		private SymbolsDictionary symbols;

		// Token: 0x04001940 RID: 6464
		private Positions positions;

		// Token: 0x04001941 RID: 6465
		private Stack stack;

		// Token: 0x04001942 RID: 6466
		private SyntaxTreeNode contentNode;

		// Token: 0x04001943 RID: 6467
		private bool isPartial;

		// Token: 0x04001944 RID: 6468
		private int minMaxNodesCount;

		// Token: 0x04001945 RID: 6469
		private bool enableUpaCheck;
	}
}
