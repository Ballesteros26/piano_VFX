using System;
using System.Xml.XPath;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x020004F9 RID: 1273
	internal sealed class CopyAttributesAction : Action
	{
		// Token: 0x06003406 RID: 13318 RVA: 0x00128E2E File Offset: 0x0012702E
		internal static CopyAttributesAction GetAction()
		{
			return CopyAttributesAction.s_Action;
		}

		// Token: 0x06003407 RID: 13319 RVA: 0x00128E38 File Offset: 0x00127038
		internal override void Execute(Processor processor, ActionFrame frame)
		{
			while (processor.CanContinue)
			{
				switch (frame.State)
				{
				case 0:
					if (!frame.Node.HasAttributes || !frame.Node.MoveToFirstAttribute())
					{
						frame.Finished();
						return;
					}
					frame.State = 2;
					break;
				case 1:
					return;
				case 2:
					break;
				case 3:
					if (CopyAttributesAction.SendTextEvent(processor, frame.Node))
					{
						frame.State = 4;
						continue;
					}
					return;
				case 4:
					if (CopyAttributesAction.SendEndEvent(processor, frame.Node))
					{
						frame.State = 5;
						continue;
					}
					return;
				case 5:
					if (frame.Node.MoveToNextAttribute())
					{
						frame.State = 2;
						continue;
					}
					frame.Node.MoveToParent();
					frame.Finished();
					return;
				default:
					return;
				}
				if (!CopyAttributesAction.SendBeginEvent(processor, frame.Node))
				{
					break;
				}
				frame.State = 3;
			}
		}

		// Token: 0x06003408 RID: 13320 RVA: 0x00128F11 File Offset: 0x00127111
		private static bool SendBeginEvent(Processor processor, XPathNavigator node)
		{
			return processor.BeginEvent(XPathNodeType.Attribute, node.Prefix, node.LocalName, node.NamespaceURI, false);
		}

		// Token: 0x06003409 RID: 13321 RVA: 0x00128F2D File Offset: 0x0012712D
		private static bool SendTextEvent(Processor processor, XPathNavigator node)
		{
			return processor.TextEvent(node.Value);
		}

		// Token: 0x0600340A RID: 13322 RVA: 0x00128F3B File Offset: 0x0012713B
		private static bool SendEndEvent(Processor processor, XPathNavigator node)
		{
			return processor.EndEvent(XPathNodeType.Attribute);
		}

		// Token: 0x0400216F RID: 8559
		private const int BeginEvent = 2;

		// Token: 0x04002170 RID: 8560
		private const int TextEvent = 3;

		// Token: 0x04002171 RID: 8561
		private const int EndEvent = 4;

		// Token: 0x04002172 RID: 8562
		private const int Advance = 5;

		// Token: 0x04002173 RID: 8563
		private static CopyAttributesAction s_Action = new CopyAttributesAction();
	}
}
