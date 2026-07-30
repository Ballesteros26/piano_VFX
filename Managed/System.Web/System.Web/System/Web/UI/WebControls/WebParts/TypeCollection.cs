using System;
using System.Collections;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x02000489 RID: 1161
	public class TypeCollection : ReadOnlyCollectionBase
	{
		// Token: 0x0600348F RID: 13455 RVA: 0x0008A99C File Offset: 0x00088B9C
		public TypeCollection()
		{
		}

		// Token: 0x06003490 RID: 13456 RVA: 0x0008A9A4 File Offset: 0x00088BA4
		public TypeCollection(ICollection types)
		{
			base.InnerList.AddRange(types);
		}

		// Token: 0x06003491 RID: 13457 RVA: 0x0008AED7 File Offset: 0x000890D7
		public TypeCollection(TypeCollection existingTypes, ICollection types)
		{
			base.InnerList.AddRange(existingTypes.InnerList);
			base.InnerList.AddRange(types);
		}

		// Token: 0x06003492 RID: 13458 RVA: 0x0008A9D8 File Offset: 0x00088BD8
		public bool Contains(Type value)
		{
			return base.InnerList.Contains(value);
		}

		// Token: 0x06003493 RID: 13459 RVA: 0x0008AEFC File Offset: 0x000890FC
		public void CopyTo(Type[] array, int index)
		{
			base.InnerList.CopyTo(0, array, index, this.Count);
		}

		// Token: 0x06003494 RID: 13460 RVA: 0x0008A9F5 File Offset: 0x00088BF5
		public int IndexOf(Type value)
		{
			return base.InnerList.IndexOf(value);
		}

		// Token: 0x17001081 RID: 4225
		public Type this[int index]
		{
			get
			{
				return (Type)base.InnerList[index];
			}
		}

		// Token: 0x04001D1C RID: 7452
		public static readonly TypeCollection Empty = new TypeCollection();
	}
}
