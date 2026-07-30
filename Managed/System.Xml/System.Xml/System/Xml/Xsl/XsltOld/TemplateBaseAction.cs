using System;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x02000544 RID: 1348
	internal abstract class TemplateBaseAction : ContainerAction
	{
		// Token: 0x06003698 RID: 13976 RVA: 0x00131D31 File Offset: 0x0012FF31
		public int AllocateVariableSlot()
		{
			int num = this.variableFreeSlot;
			this.variableFreeSlot++;
			if (this.variableCount < this.variableFreeSlot)
			{
				this.variableCount = this.variableFreeSlot;
			}
			return num;
		}

		// Token: 0x06003699 RID: 13977 RVA: 0x00002F50 File Offset: 0x00001150
		public void ReleaseVariableSlots(int n)
		{
		}

		// Token: 0x04002305 RID: 8965
		protected int variableCount;

		// Token: 0x04002306 RID: 8966
		private int variableFreeSlot;
	}
}
