using System;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x0200054E RID: 1358
	internal class UseAttributeSetsAction : CompiledAction
	{
		// Token: 0x17000B90 RID: 2960
		// (get) Token: 0x060036C0 RID: 14016 RVA: 0x0013234A File Offset: 0x0013054A
		internal XmlQualifiedName[] UsedSets
		{
			get
			{
				return this.useAttributeSets;
			}
		}

		// Token: 0x060036C1 RID: 14017 RVA: 0x00132354 File Offset: 0x00130554
		internal override void Compile(Compiler compiler)
		{
			this.useString = compiler.Input.Value;
			if (this.useString.Length == 0)
			{
				this.useAttributeSets = new XmlQualifiedName[0];
				return;
			}
			string[] array = XmlConvert.SplitString(this.useString);
			try
			{
				this.useAttributeSets = new XmlQualifiedName[array.Length];
				for (int i = 0; i < array.Length; i++)
				{
					this.useAttributeSets[i] = compiler.CreateXPathQName(array[i]);
				}
			}
			catch (XsltException)
			{
				if (!compiler.ForwardCompatibility)
				{
					throw;
				}
				this.useAttributeSets = new XmlQualifiedName[0];
			}
		}

		// Token: 0x060036C2 RID: 14018 RVA: 0x001323F0 File Offset: 0x001305F0
		internal override void Execute(Processor processor, ActionFrame frame)
		{
			int state = frame.State;
			if (state != 0)
			{
				if (state != 2)
				{
					return;
				}
			}
			else
			{
				frame.Counter = 0;
				frame.State = 2;
			}
			if (frame.Counter < this.useAttributeSets.Length)
			{
				AttributeSetAction attributeSet = processor.RootAction.GetAttributeSet(this.useAttributeSets[frame.Counter]);
				frame.IncrementCounter();
				processor.PushActionFrame(attributeSet, frame.NodeSet);
				return;
			}
			frame.Finished();
		}

		// Token: 0x04002315 RID: 8981
		private XmlQualifiedName[] useAttributeSets;

		// Token: 0x04002316 RID: 8982
		private string useString;

		// Token: 0x04002317 RID: 8983
		private const int ProcessingSets = 2;
	}
}
