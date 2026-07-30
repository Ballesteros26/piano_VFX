using System;
using System.Collections;

namespace System.Xml.Schema
{
	// Token: 0x02000385 RID: 901
	internal class ActiveAxis
	{
		// Token: 0x17000738 RID: 1848
		// (get) Token: 0x06002479 RID: 9337 RVA: 0x000DDE86 File Offset: 0x000DC086
		public int CurrentDepth
		{
			get
			{
				return this.currentDepth;
			}
		}

		// Token: 0x0600247A RID: 9338 RVA: 0x000DDE8E File Offset: 0x000DC08E
		internal void Reactivate()
		{
			this.isActive = true;
			this.currentDepth = -1;
		}

		// Token: 0x0600247B RID: 9339 RVA: 0x000DDEA0 File Offset: 0x000DC0A0
		internal ActiveAxis(Asttree axisTree)
		{
			this.axisTree = axisTree;
			this.currentDepth = -1;
			this.axisStack = new ArrayList(axisTree.SubtreeArray.Count);
			for (int i = 0; i < axisTree.SubtreeArray.Count; i++)
			{
				AxisStack axisStack = new AxisStack((ForwardAxis)axisTree.SubtreeArray[i], this);
				this.axisStack.Add(axisStack);
			}
			this.isActive = true;
		}

		// Token: 0x0600247C RID: 9340 RVA: 0x000DDF1C File Offset: 0x000DC11C
		public bool MoveToStartElement(string localname, string URN)
		{
			if (!this.isActive)
			{
				return false;
			}
			this.currentDepth++;
			bool flag = false;
			for (int i = 0; i < this.axisStack.Count; i++)
			{
				AxisStack axisStack = (AxisStack)this.axisStack[i];
				if (axisStack.Subtree.IsSelfAxis)
				{
					if (axisStack.Subtree.IsDss || this.CurrentDepth == 0)
					{
						flag = true;
					}
				}
				else if (this.CurrentDepth != 0 && axisStack.MoveToChild(localname, URN, this.currentDepth))
				{
					flag = true;
				}
			}
			return flag;
		}

		// Token: 0x0600247D RID: 9341 RVA: 0x000DDFAC File Offset: 0x000DC1AC
		public virtual bool EndElement(string localname, string URN)
		{
			if (this.currentDepth == 0)
			{
				this.isActive = false;
				this.currentDepth--;
			}
			if (!this.isActive)
			{
				return false;
			}
			for (int i = 0; i < this.axisStack.Count; i++)
			{
				((AxisStack)this.axisStack[i]).MoveToParent(localname, URN, this.currentDepth);
			}
			this.currentDepth--;
			return false;
		}

		// Token: 0x0600247E RID: 9342 RVA: 0x000DE024 File Offset: 0x000DC224
		public bool MoveToAttribute(string localname, string URN)
		{
			if (!this.isActive)
			{
				return false;
			}
			bool flag = false;
			for (int i = 0; i < this.axisStack.Count; i++)
			{
				if (((AxisStack)this.axisStack[i]).MoveToAttribute(localname, URN, this.currentDepth + 1))
				{
					flag = true;
				}
			}
			return flag;
		}

		// Token: 0x040018CE RID: 6350
		private int currentDepth;

		// Token: 0x040018CF RID: 6351
		private bool isActive;

		// Token: 0x040018D0 RID: 6352
		private Asttree axisTree;

		// Token: 0x040018D1 RID: 6353
		private ArrayList axisStack;
	}
}
