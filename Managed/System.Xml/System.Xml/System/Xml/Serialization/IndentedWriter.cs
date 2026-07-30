using System;
using System.IO;

namespace System.Xml.Serialization
{
	// Token: 0x02000374 RID: 884
	internal class IndentedWriter
	{
		// Token: 0x0600240F RID: 9231 RVA: 0x000DCC6E File Offset: 0x000DAE6E
		internal IndentedWriter(TextWriter writer, bool compact)
		{
			this.writer = writer;
			this.compact = compact;
		}

		// Token: 0x1700071A RID: 1818
		// (get) Token: 0x06002410 RID: 9232 RVA: 0x000DCC84 File Offset: 0x000DAE84
		// (set) Token: 0x06002411 RID: 9233 RVA: 0x000DCC8C File Offset: 0x000DAE8C
		internal int Indent
		{
			get
			{
				return this.indentLevel;
			}
			set
			{
				this.indentLevel = value;
			}
		}

		// Token: 0x06002412 RID: 9234 RVA: 0x000DCC95 File Offset: 0x000DAE95
		internal void Write(string s)
		{
			if (this.needIndent)
			{
				this.WriteIndent();
			}
			this.writer.Write(s);
		}

		// Token: 0x06002413 RID: 9235 RVA: 0x000DCCB1 File Offset: 0x000DAEB1
		internal void Write(char c)
		{
			if (this.needIndent)
			{
				this.WriteIndent();
			}
			this.writer.Write(c);
		}

		// Token: 0x06002414 RID: 9236 RVA: 0x000DCCCD File Offset: 0x000DAECD
		internal void WriteLine(string s)
		{
			if (this.needIndent)
			{
				this.WriteIndent();
			}
			this.writer.WriteLine(s);
			this.needIndent = true;
		}

		// Token: 0x06002415 RID: 9237 RVA: 0x000DCCF0 File Offset: 0x000DAEF0
		internal void WriteLine()
		{
			this.writer.WriteLine();
			this.needIndent = true;
		}

		// Token: 0x06002416 RID: 9238 RVA: 0x000DCD04 File Offset: 0x000DAF04
		internal void WriteIndent()
		{
			this.needIndent = false;
			if (!this.compact)
			{
				for (int i = 0; i < this.indentLevel; i++)
				{
					this.writer.Write("    ");
				}
			}
		}

		// Token: 0x04001887 RID: 6279
		private TextWriter writer;

		// Token: 0x04001888 RID: 6280
		private bool needIndent;

		// Token: 0x04001889 RID: 6281
		private int indentLevel;

		// Token: 0x0400188A RID: 6282
		private bool compact;
	}
}
