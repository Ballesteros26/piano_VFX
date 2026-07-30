using System;
using System.Xml.XPath;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x020004FC RID: 1276
	internal sealed class CopyNodeSetAction : Action
	{
		// Token: 0x06003417 RID: 13335 RVA: 0x00129121 File Offset: 0x00127321
		internal static CopyNodeSetAction GetAction()
		{
			return CopyNodeSetAction.s_Action;
		}

		// Token: 0x06003418 RID: 13336 RVA: 0x00129128 File Offset: 0x00127328
		internal override void Execute(Processor processor, ActionFrame frame)
		{
			while (processor.CanContinue)
			{
				switch (frame.State)
				{
				case 0:
					if (!frame.NextNode(processor))
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
				{
					XPathNodeType nodeType = frame.Node.NodeType;
					if (nodeType == XPathNodeType.Element || nodeType == XPathNodeType.Root)
					{
						processor.PushActionFrame(CopyNamespacesAction.GetAction(), frame.NodeSet);
						frame.State = 4;
						return;
					}
					if (CopyNodeSetAction.SendTextEvent(processor, frame.Node))
					{
						frame.State = 7;
						continue;
					}
					return;
				}
				case 4:
					processor.PushActionFrame(CopyAttributesAction.GetAction(), frame.NodeSet);
					frame.State = 5;
					return;
				case 5:
					if (frame.Node.HasChildren)
					{
						processor.PushActionFrame(CopyNodeSetAction.GetAction(), frame.Node.SelectChildren(XPathNodeType.All));
						frame.State = 6;
						return;
					}
					frame.State = 7;
					goto IL_0107;
				case 6:
					frame.State = 7;
					continue;
				case 7:
					goto IL_0107;
				default:
					return;
				}
				if (CopyNodeSetAction.SendBeginEvent(processor, frame.Node))
				{
					frame.State = 3;
					continue;
				}
				break;
				IL_0107:
				if (!CopyNodeSetAction.SendEndEvent(processor, frame.Node))
				{
					break;
				}
				frame.State = 0;
			}
		}

		// Token: 0x06003419 RID: 13337 RVA: 0x0012925C File Offset: 0x0012745C
		private static bool SendBeginEvent(Processor processor, XPathNavigator node)
		{
			return processor.CopyBeginEvent(node, node.IsEmptyElement);
		}

		// Token: 0x0600341A RID: 13338 RVA: 0x0012926B File Offset: 0x0012746B
		private static bool SendTextEvent(Processor processor, XPathNavigator node)
		{
			return processor.CopyTextEvent(node);
		}

		// Token: 0x0600341B RID: 13339 RVA: 0x00129274 File Offset: 0x00127474
		private static bool SendEndEvent(Processor processor, XPathNavigator node)
		{
			return processor.CopyEndEvent(node);
		}

		// Token: 0x0400217B RID: 8571
		private const int BeginEvent = 2;

		// Token: 0x0400217C RID: 8572
		private const int Contents = 3;

		// Token: 0x0400217D RID: 8573
		private const int Namespaces = 4;

		// Token: 0x0400217E RID: 8574
		private const int Attributes = 5;

		// Token: 0x0400217F RID: 8575
		private const int Subtree = 6;

		// Token: 0x04002180 RID: 8576
		private const int EndEvent = 7;

		// Token: 0x04002181 RID: 8577
		private static CopyNodeSetAction s_Action = new CopyNodeSetAction();
	}
}
