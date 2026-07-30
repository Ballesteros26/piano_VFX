using System;
using System.Xml.XPath;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x020004FB RID: 1275
	internal sealed class CopyNamespacesAction : Action
	{
		// Token: 0x06003413 RID: 13331 RVA: 0x00129046 File Offset: 0x00127246
		internal static CopyNamespacesAction GetAction()
		{
			return CopyNamespacesAction.s_Action;
		}

		// Token: 0x06003414 RID: 13332 RVA: 0x00129050 File Offset: 0x00127250
		internal override void Execute(Processor processor, ActionFrame frame)
		{
			while (processor.CanContinue)
			{
				switch (frame.State)
				{
				case 0:
					if (!frame.Node.MoveToFirstNamespace(XPathNamespaceScope.ExcludeXml))
					{
						frame.Finished();
						return;
					}
					frame.State = 2;
					break;
				case 1:
				case 3:
					return;
				case 2:
					break;
				case 4:
					if (processor.EndEvent(XPathNodeType.Namespace))
					{
						frame.State = 5;
						continue;
					}
					return;
				case 5:
					if (frame.Node.MoveToNextNamespace(XPathNamespaceScope.ExcludeXml))
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
				if (!processor.BeginEvent(XPathNodeType.Namespace, null, frame.Node.LocalName, frame.Node.Value, false))
				{
					break;
				}
				frame.State = 4;
			}
		}

		// Token: 0x04002176 RID: 8566
		private const int BeginEvent = 2;

		// Token: 0x04002177 RID: 8567
		private const int TextEvent = 3;

		// Token: 0x04002178 RID: 8568
		private const int EndEvent = 4;

		// Token: 0x04002179 RID: 8569
		private const int Advance = 5;

		// Token: 0x0400217A RID: 8570
		private static CopyNamespacesAction s_Action = new CopyNamespacesAction();
	}
}
