using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x0200001A RID: 26
	internal sealed class DescendantOverDescendantQuery : DescendantBaseQuery
	{
		// Token: 0x0600009A RID: 154 RVA: 0x000034BC File Offset: 0x000016BC
		public DescendantOverDescendantQuery(Query qyParent, bool matchSelf, string name, string prefix, XPathNodeType typeTest, bool abbrAxis)
			: base(qyParent, name, prefix, typeTest, matchSelf, abbrAxis)
		{
		}

		// Token: 0x0600009B RID: 155 RVA: 0x000034CD File Offset: 0x000016CD
		private DescendantOverDescendantQuery(DescendantOverDescendantQuery other)
			: base(other)
		{
			this.level = other.level;
		}

		// Token: 0x0600009C RID: 156 RVA: 0x000034E2 File Offset: 0x000016E2
		public override void Reset()
		{
			this.level = 0;
			base.Reset();
		}

		// Token: 0x0600009D RID: 157 RVA: 0x000034F4 File Offset: 0x000016F4
		public override XPathNavigator Advance()
		{
			for (;;)
			{
				IL_0000:
				if (this.level == 0)
				{
					this.currentNode = this.qyInput.Advance();
					this.position = 0;
					if (this.currentNode == null)
					{
						break;
					}
					if (this.matchSelf && this.matches(this.currentNode))
					{
						goto Block_3;
					}
					this.currentNode = this.currentNode.Clone();
					if (!this.MoveToFirstChild())
					{
						continue;
					}
				}
				else if (!this.MoveUpUntillNext())
				{
					continue;
				}
				while (!this.matches(this.currentNode))
				{
					if (!this.MoveToFirstChild())
					{
						goto IL_0000;
					}
				}
				goto Block_5;
			}
			return null;
			Block_3:
			this.position = 1;
			return this.currentNode;
			Block_5:
			this.position++;
			return this.currentNode;
		}

		// Token: 0x0600009E RID: 158 RVA: 0x000035A1 File Offset: 0x000017A1
		private bool MoveToFirstChild()
		{
			if (this.currentNode.MoveToFirstChild())
			{
				this.level++;
				return true;
			}
			return false;
		}

		// Token: 0x0600009F RID: 159 RVA: 0x000035C1 File Offset: 0x000017C1
		private bool MoveUpUntillNext()
		{
			while (!this.currentNode.MoveToNext())
			{
				this.level--;
				if (this.level == 0)
				{
					return false;
				}
				this.currentNode.MoveToParent();
			}
			return true;
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x000035F7 File Offset: 0x000017F7
		public override XPathNodeIterator Clone()
		{
			return new DescendantOverDescendantQuery(this);
		}

		// Token: 0x0400007B RID: 123
		private int level;
	}
}
