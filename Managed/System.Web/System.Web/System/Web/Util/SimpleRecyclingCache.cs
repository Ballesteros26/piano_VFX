using System;
using System.Collections;

namespace System.Web.Util
{
	// Token: 0x02000132 RID: 306
	internal class SimpleRecyclingCache
	{
		// Token: 0x06000E51 RID: 3665 RVA: 0x00026DB4 File Offset: 0x00024FB4
		internal SimpleRecyclingCache()
		{
			this.CreateHashtable();
		}

		// Token: 0x06000E52 RID: 3666 RVA: 0x00026DC2 File Offset: 0x00024FC2
		private void CreateHashtable()
		{
			SimpleRecyclingCache._hashtable = new Hashtable(100, StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x170004E2 RID: 1250
		internal object this[object key]
		{
			get
			{
				return SimpleRecyclingCache._hashtable[key];
			}
			set
			{
				lock (this)
				{
					if (SimpleRecyclingCache._hashtable.Count >= 100)
					{
						SimpleRecyclingCache._hashtable.Clear();
					}
					SimpleRecyclingCache._hashtable[key] = value;
				}
			}
		}

		// Token: 0x040011D9 RID: 4569
		private const int MAX_SIZE = 100;

		// Token: 0x040011DA RID: 4570
		private static Hashtable _hashtable;
	}
}
