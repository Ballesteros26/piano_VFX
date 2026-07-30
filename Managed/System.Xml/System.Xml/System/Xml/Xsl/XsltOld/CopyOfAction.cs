using System;
using System.Xml.XPath;
using MS.Internal.Xml.XPath;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x020004FD RID: 1277
	internal class CopyOfAction : CompiledAction
	{
		// Token: 0x0600341E RID: 13342 RVA: 0x00129289 File Offset: 0x00127489
		internal override void Compile(Compiler compiler)
		{
			base.CompileAttributes(compiler);
			base.CheckRequiredAttribute(compiler, this.selectKey != -1, "select");
			base.CheckEmpty(compiler);
		}

		// Token: 0x0600341F RID: 13343 RVA: 0x001292B4 File Offset: 0x001274B4
		internal override bool CompileAttribute(Compiler compiler)
		{
			string localName = compiler.Input.LocalName;
			string value = compiler.Input.Value;
			if (Ref.Equal(localName, compiler.Atoms.Select))
			{
				this.selectKey = compiler.AddQuery(value);
				return true;
			}
			return false;
		}

		// Token: 0x06003420 RID: 13344 RVA: 0x001292FC File Offset: 0x001274FC
		internal override void Execute(Processor processor, ActionFrame frame)
		{
			switch (frame.State)
			{
			case 0:
			{
				Query valueQuery = processor.GetValueQuery(this.selectKey);
				object obj = valueQuery.Evaluate(frame.NodeSet);
				if (obj is XPathNodeIterator)
				{
					processor.PushActionFrame(CopyNodeSetAction.GetAction(), new XPathArrayIterator(valueQuery));
					frame.State = 3;
					return;
				}
				XPathNavigator xpathNavigator = obj as XPathNavigator;
				if (xpathNavigator != null)
				{
					processor.PushActionFrame(CopyNodeSetAction.GetAction(), new XPathSingletonIterator(xpathNavigator));
					frame.State = 3;
					return;
				}
				string text = XmlConvert.ToXPathString(obj);
				if (processor.TextEvent(text))
				{
					frame.Finished();
					return;
				}
				frame.StoredOutput = text;
				frame.State = 2;
				return;
			}
			case 1:
				break;
			case 2:
				processor.TextEvent(frame.StoredOutput);
				frame.Finished();
				return;
			case 3:
				frame.Finished();
				break;
			default:
				return;
			}
		}

		// Token: 0x04002182 RID: 8578
		private const int ResultStored = 2;

		// Token: 0x04002183 RID: 8579
		private const int NodeSetCopied = 3;

		// Token: 0x04002184 RID: 8580
		private int selectKey = -1;
	}
}
