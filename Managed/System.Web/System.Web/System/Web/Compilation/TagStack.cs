using System;
using System.Collections;
using System.Web.Util;

namespace System.Web.Compilation
{
	// Token: 0x02000623 RID: 1571
	internal class TagStack
	{
		// Token: 0x06004352 RID: 17234 RVA: 0x000B3CD8 File Offset: 0x000B1ED8
		public TagStack()
		{
			this.tags = new Stack();
		}

		// Token: 0x06004353 RID: 17235 RVA: 0x000B3CEB File Offset: 0x000B1EEB
		public void Push(string tagid)
		{
			this.tags.Push(tagid);
		}

		// Token: 0x06004354 RID: 17236 RVA: 0x000B3CF9 File Offset: 0x000B1EF9
		public string Pop()
		{
			if (this.tags.Count == 0)
			{
				return null;
			}
			return (string)this.tags.Pop();
		}

		// Token: 0x06004355 RID: 17237 RVA: 0x000B3D1A File Offset: 0x000B1F1A
		public bool CompareTo(string tagid)
		{
			return this.tags.Count != 0 && string.Compare(tagid, (string)this.tags.Peek(), true, Helpers.InvariantCulture) == 0;
		}

		// Token: 0x1700153B RID: 5435
		// (get) Token: 0x06004356 RID: 17238 RVA: 0x000B3D4A File Offset: 0x000B1F4A
		public int Count
		{
			get
			{
				return this.tags.Count;
			}
		}

		// Token: 0x1700153C RID: 5436
		// (get) Token: 0x06004357 RID: 17239 RVA: 0x000B3D57 File Offset: 0x000B1F57
		public string Current
		{
			get
			{
				return (string)this.tags.Peek();
			}
		}

		// Token: 0x04002401 RID: 9217
		private Stack tags;
	}
}
