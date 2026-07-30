using System;

namespace System.Xml.Xsl
{
	// Token: 0x020004BE RID: 1214
	internal struct StringPair
	{
		// Token: 0x06003148 RID: 12616 RVA: 0x0011C9DD File Offset: 0x0011ABDD
		public StringPair(string left, string right)
		{
			this.left = left;
			this.right = right;
		}

		// Token: 0x17000A66 RID: 2662
		// (get) Token: 0x06003149 RID: 12617 RVA: 0x0011C9ED File Offset: 0x0011ABED
		public string Left
		{
			get
			{
				return this.left;
			}
		}

		// Token: 0x17000A67 RID: 2663
		// (get) Token: 0x0600314A RID: 12618 RVA: 0x0011C9F5 File Offset: 0x0011ABF5
		public string Right
		{
			get
			{
				return this.right;
			}
		}

		// Token: 0x04002032 RID: 8242
		private string left;

		// Token: 0x04002033 RID: 8243
		private string right;
	}
}
