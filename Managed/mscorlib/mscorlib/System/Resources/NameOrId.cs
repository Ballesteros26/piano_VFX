using System;

namespace System.Resources
{
	// Token: 0x020002B5 RID: 693
	internal class NameOrId
	{
		// Token: 0x06001FAD RID: 8109 RVA: 0x0007CAD5 File Offset: 0x0007ACD5
		public NameOrId(string name)
		{
			this.name = name;
		}

		// Token: 0x06001FAE RID: 8110 RVA: 0x0007CAE4 File Offset: 0x0007ACE4
		public NameOrId(int id)
		{
			this.id = id;
		}

		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x06001FAF RID: 8111 RVA: 0x0007CAF3 File Offset: 0x0007ACF3
		public bool IsName
		{
			get
			{
				return this.name != null;
			}
		}

		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x06001FB0 RID: 8112 RVA: 0x0007CAFE File Offset: 0x0007ACFE
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x06001FB1 RID: 8113 RVA: 0x0007CB06 File Offset: 0x0007AD06
		public int Id
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x06001FB2 RID: 8114 RVA: 0x0007CB0E File Offset: 0x0007AD0E
		public override string ToString()
		{
			if (this.name != null)
			{
				return "Name(" + this.name + ")";
			}
			return "Id(" + this.id + ")";
		}

		// Token: 0x0400113F RID: 4415
		private string name;

		// Token: 0x04001140 RID: 4416
		private int id;
	}
}
