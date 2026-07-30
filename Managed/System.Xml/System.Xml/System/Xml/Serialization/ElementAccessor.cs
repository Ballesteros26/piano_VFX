using System;

namespace System.Xml.Serialization
{
	// Token: 0x020002E1 RID: 737
	internal class ElementAccessor : Accessor
	{
		// Token: 0x1700054D RID: 1357
		// (get) Token: 0x06001BAA RID: 7082 RVA: 0x00099C72 File Offset: 0x00097E72
		// (set) Token: 0x06001BAB RID: 7083 RVA: 0x00099C7A File Offset: 0x00097E7A
		internal bool IsSoap
		{
			get
			{
				return this.isSoap;
			}
			set
			{
				this.isSoap = value;
			}
		}

		// Token: 0x1700054E RID: 1358
		// (get) Token: 0x06001BAC RID: 7084 RVA: 0x00099C83 File Offset: 0x00097E83
		// (set) Token: 0x06001BAD RID: 7085 RVA: 0x00099C8B File Offset: 0x00097E8B
		internal bool IsNullable
		{
			get
			{
				return this.nullable;
			}
			set
			{
				this.nullable = value;
			}
		}

		// Token: 0x1700054F RID: 1359
		// (get) Token: 0x06001BAE RID: 7086 RVA: 0x00099C94 File Offset: 0x00097E94
		// (set) Token: 0x06001BAF RID: 7087 RVA: 0x00099C9C File Offset: 0x00097E9C
		internal bool IsUnbounded
		{
			get
			{
				return this.unbounded;
			}
			set
			{
				this.unbounded = value;
			}
		}

		// Token: 0x06001BB0 RID: 7088 RVA: 0x00099CA8 File Offset: 0x00097EA8
		internal ElementAccessor Clone()
		{
			return new ElementAccessor
			{
				nullable = this.nullable,
				IsTopLevelInSchema = base.IsTopLevelInSchema,
				Form = base.Form,
				isSoap = this.isSoap,
				Name = this.Name,
				Default = base.Default,
				Namespace = base.Namespace,
				Mapping = base.Mapping,
				Any = base.Any
			};
		}

		// Token: 0x04001601 RID: 5633
		private bool nullable;

		// Token: 0x04001602 RID: 5634
		private bool isSoap;

		// Token: 0x04001603 RID: 5635
		private bool unbounded;
	}
}
