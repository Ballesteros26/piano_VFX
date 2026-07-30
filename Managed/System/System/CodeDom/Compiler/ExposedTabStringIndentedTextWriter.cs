using System;
using System.IO;

namespace System.CodeDom.Compiler
{
	// Token: 0x020007AF RID: 1967
	internal sealed class ExposedTabStringIndentedTextWriter : IndentedTextWriter
	{
		// Token: 0x06003F79 RID: 16249 RVA: 0x000DFC5E File Offset: 0x000DDE5E
		public ExposedTabStringIndentedTextWriter(TextWriter writer, string tabString)
			: base(writer, tabString)
		{
			this.TabString = tabString ?? "    ";
		}

		// Token: 0x06003F7A RID: 16250 RVA: 0x000DFC78 File Offset: 0x000DDE78
		internal void InternalOutputTabs()
		{
			TextWriter innerWriter = base.InnerWriter;
			for (int i = 0; i < base.Indent; i++)
			{
				innerWriter.Write(this.TabString);
			}
		}

		// Token: 0x17000F4E RID: 3918
		// (get) Token: 0x06003F7B RID: 16251 RVA: 0x000DFCA9 File Offset: 0x000DDEA9
		internal string TabString { get; }
	}
}
