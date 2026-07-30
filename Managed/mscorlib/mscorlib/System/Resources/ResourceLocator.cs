using System;

namespace System.Resources
{
	// Token: 0x020002A9 RID: 681
	internal struct ResourceLocator
	{
		// Token: 0x06001F4A RID: 8010 RVA: 0x00079B5E File Offset: 0x00077D5E
		internal ResourceLocator(int dataPos, object value)
		{
			this._dataPos = dataPos;
			this._value = value;
		}

		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x06001F4B RID: 8011 RVA: 0x00079B6E File Offset: 0x00077D6E
		internal int DataPosition
		{
			get
			{
				return this._dataPos;
			}
		}

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x06001F4C RID: 8012 RVA: 0x00079B76 File Offset: 0x00077D76
		// (set) Token: 0x06001F4D RID: 8013 RVA: 0x00079B7E File Offset: 0x00077D7E
		internal object Value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = value;
			}
		}

		// Token: 0x06001F4E RID: 8014 RVA: 0x00079B87 File Offset: 0x00077D87
		internal static bool CanCache(ResourceTypeCode value)
		{
			return value <= ResourceTypeCode.TimeSpan;
		}

		// Token: 0x040010E4 RID: 4324
		internal object _value;

		// Token: 0x040010E5 RID: 4325
		internal int _dataPos;
	}
}
