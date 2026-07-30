using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x0200002E RID: 46
	internal sealed class NamespaceQuery : BaseAxisQuery
	{
		// Token: 0x0600013B RID: 315 RVA: 0x00002105 File Offset: 0x00000305
		public NamespaceQuery(Query qyParent, string Name, string Prefix, XPathNodeType Type)
			: base(qyParent, Name, Prefix, Type)
		{
		}

		// Token: 0x0600013C RID: 316 RVA: 0x000051D4 File Offset: 0x000033D4
		private NamespaceQuery(NamespaceQuery other)
			: base(other)
		{
			this.onNamespace = other.onNamespace;
		}

		// Token: 0x0600013D RID: 317 RVA: 0x000051E9 File Offset: 0x000033E9
		public override void Reset()
		{
			this.onNamespace = false;
			base.Reset();
		}

		// Token: 0x0600013E RID: 318 RVA: 0x000051F8 File Offset: 0x000033F8
		public override XPathNavigator Advance()
		{
			for (;;)
			{
				if (!this.onNamespace)
				{
					this.currentNode = this.qyInput.Advance();
					if (this.currentNode == null)
					{
						break;
					}
					this.position = 0;
					this.currentNode = this.currentNode.Clone();
					this.onNamespace = this.currentNode.MoveToFirstNamespace();
				}
				else
				{
					this.onNamespace = this.currentNode.MoveToNextNamespace();
				}
				if (this.onNamespace && this.matches(this.currentNode))
				{
					goto Block_3;
				}
			}
			return null;
			Block_3:
			this.position++;
			return this.currentNode;
		}

		// Token: 0x0600013F RID: 319 RVA: 0x0000528E File Offset: 0x0000348E
		public override bool matches(XPathNavigator e)
		{
			return e.Value.Length != 0 && (!base.NameTest || base.Name.Equals(e.LocalName));
		}

		// Token: 0x06000140 RID: 320 RVA: 0x000052BA File Offset: 0x000034BA
		public override XPathNodeIterator Clone()
		{
			return new NamespaceQuery(this);
		}

		// Token: 0x040000B9 RID: 185
		private bool onNamespace;
	}
}
