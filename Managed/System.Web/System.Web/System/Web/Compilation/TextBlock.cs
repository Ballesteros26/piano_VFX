using System;

namespace System.Web.Compilation
{
	// Token: 0x02000625 RID: 1573
	internal sealed class TextBlock
	{
		// Token: 0x06004358 RID: 17240 RVA: 0x000B3D69 File Offset: 0x000B1F69
		public TextBlock(TextBlockType type, string content)
		{
			this.Content = content;
			this.Type = type;
			this.Length = content.Length;
		}

		// Token: 0x06004359 RID: 17241 RVA: 0x000B3D8B File Offset: 0x000B1F8B
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				base.GetType().FullName,
				" [",
				this.Type,
				"]"
			});
		}

		// Token: 0x04002407 RID: 9223
		public string Content;

		// Token: 0x04002408 RID: 9224
		public readonly TextBlockType Type;

		// Token: 0x04002409 RID: 9225
		public readonly int Length;
	}
}
