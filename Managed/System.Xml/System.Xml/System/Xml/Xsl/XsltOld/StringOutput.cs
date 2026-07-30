using System;
using System.Text;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x02000540 RID: 1344
	internal class StringOutput : SequentialOutput
	{
		// Token: 0x17000B7F RID: 2943
		// (get) Token: 0x0600366E RID: 13934 RVA: 0x001311E3 File Offset: 0x0012F3E3
		internal string Result
		{
			get
			{
				return this.result;
			}
		}

		// Token: 0x0600366F RID: 13935 RVA: 0x001311EB File Offset: 0x0012F3EB
		internal StringOutput(Processor processor)
			: base(processor)
		{
			this.builder = new StringBuilder();
		}

		// Token: 0x06003670 RID: 13936 RVA: 0x001311FF File Offset: 0x0012F3FF
		internal override void Write(char outputChar)
		{
			this.builder.Append(outputChar);
		}

		// Token: 0x06003671 RID: 13937 RVA: 0x0013120E File Offset: 0x0012F40E
		internal override void Write(string outputText)
		{
			this.builder.Append(outputText);
		}

		// Token: 0x06003672 RID: 13938 RVA: 0x0013121D File Offset: 0x0012F41D
		internal override void Close()
		{
			this.result = this.builder.ToString();
		}

		// Token: 0x040022F0 RID: 8944
		private StringBuilder builder;

		// Token: 0x040022F1 RID: 8945
		private string result;
	}
}
