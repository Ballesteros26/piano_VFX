using System;
using System.Collections;

namespace System.Web.Services.Description
{
	// Token: 0x020000D8 RID: 216
	internal class MimeParameterCollection : CollectionBase
	{
		// Token: 0x1700016B RID: 363
		// (get) Token: 0x0600057C RID: 1404 RVA: 0x00018D2A File Offset: 0x00016F2A
		// (set) Token: 0x0600057D RID: 1405 RVA: 0x00018D32 File Offset: 0x00016F32
		internal Type WriterType
		{
			get
			{
				return this.writerType;
			}
			set
			{
				this.writerType = value;
			}
		}

		// Token: 0x1700016C RID: 364
		internal MimeParameter this[int index]
		{
			get
			{
				return (MimeParameter)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x0000CD4B File Offset: 0x0000AF4B
		internal int Add(MimeParameter parameter)
		{
			return base.List.Add(parameter);
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x0000CD59 File Offset: 0x0000AF59
		internal void Insert(int index, MimeParameter parameter)
		{
			base.List.Insert(index, parameter);
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x0000CD68 File Offset: 0x0000AF68
		internal int IndexOf(MimeParameter parameter)
		{
			return base.List.IndexOf(parameter);
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x0000CD76 File Offset: 0x0000AF76
		internal bool Contains(MimeParameter parameter)
		{
			return base.List.Contains(parameter);
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x0000CD84 File Offset: 0x0000AF84
		internal void Remove(MimeParameter parameter)
		{
			base.List.Remove(parameter);
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x0000CD92 File Offset: 0x0000AF92
		internal void CopyTo(MimeParameter[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		// Token: 0x04000398 RID: 920
		private Type writerType;
	}
}
