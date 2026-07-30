using System;
using System.Xml;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000010 RID: 16
	internal sealed class CacheChildrenQuery : ChildrenQuery
	{
		// Token: 0x06000051 RID: 81 RVA: 0x00002A0E File Offset: 0x00000C0E
		public CacheChildrenQuery(Query qyInput, string name, string prefix, XPathNodeType type)
			: base(qyInput, name, prefix, type)
		{
			this.elementStk = new ClonableStack<XPathNavigator>();
			this.positionStk = new ClonableStack<int>();
			this.needInput = true;
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002A38 File Offset: 0x00000C38
		private CacheChildrenQuery(CacheChildrenQuery other)
			: base(other)
		{
			this.nextInput = Query.Clone(other.nextInput);
			this.elementStk = other.elementStk.Clone();
			this.positionStk = other.positionStk.Clone();
			this.needInput = other.needInput;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002A8B File Offset: 0x00000C8B
		public override void Reset()
		{
			this.nextInput = null;
			this.elementStk.Clear();
			this.positionStk.Clear();
			this.needInput = true;
			base.Reset();
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002AB8 File Offset: 0x00000CB8
		public override XPathNavigator Advance()
		{
			for (;;)
			{
				if (this.needInput)
				{
					if (this.elementStk.Count == 0)
					{
						this.currentNode = this.GetNextInput();
						if (this.currentNode == null)
						{
							break;
						}
						if (!this.currentNode.MoveToFirstChild())
						{
							continue;
						}
						this.position = 0;
					}
					else
					{
						this.currentNode = this.elementStk.Pop();
						this.position = this.positionStk.Pop();
						if (!this.DecideNextNode())
						{
							continue;
						}
					}
					this.needInput = false;
				}
				else if (!this.currentNode.MoveToNext() || !this.DecideNextNode())
				{
					this.needInput = true;
					continue;
				}
				if (this.matches(this.currentNode))
				{
					goto Block_5;
				}
			}
			return null;
			Block_5:
			this.position++;
			return this.currentNode;
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002B80 File Offset: 0x00000D80
		private bool DecideNextNode()
		{
			this.nextInput = this.GetNextInput();
			if (this.nextInput != null && Query.CompareNodes(this.currentNode, this.nextInput) == XmlNodeOrder.After)
			{
				this.elementStk.Push(this.currentNode);
				this.positionStk.Push(this.position);
				this.currentNode = this.nextInput;
				this.nextInput = null;
				if (!this.currentNode.MoveToFirstChild())
				{
					return false;
				}
				this.position = 0;
			}
			return true;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002C04 File Offset: 0x00000E04
		private XPathNavigator GetNextInput()
		{
			XPathNavigator xpathNavigator;
			if (this.nextInput != null)
			{
				xpathNavigator = this.nextInput;
				this.nextInput = null;
			}
			else
			{
				xpathNavigator = this.qyInput.Advance();
				if (xpathNavigator != null)
				{
					xpathNavigator = xpathNavigator.Clone();
				}
			}
			return xpathNavigator;
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002C40 File Offset: 0x00000E40
		public override XPathNodeIterator Clone()
		{
			return new CacheChildrenQuery(this);
		}

		// Token: 0x04000068 RID: 104
		private XPathNavigator nextInput;

		// Token: 0x04000069 RID: 105
		private ClonableStack<XPathNavigator> elementStk;

		// Token: 0x0400006A RID: 106
		private ClonableStack<int> positionStk;

		// Token: 0x0400006B RID: 107
		private bool needInput;
	}
}
