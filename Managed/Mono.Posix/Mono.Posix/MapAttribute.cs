using System;

// Token: 0x02000004 RID: 4
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Field | AttributeTargets.Delegate)]
internal class MapAttribute : Attribute
{
	// Token: 0x06000004 RID: 4 RVA: 0x00002064 File Offset: 0x00000264
	public MapAttribute()
	{
	}

	// Token: 0x06000005 RID: 5 RVA: 0x0000206C File Offset: 0x0000026C
	public MapAttribute(string nativeType)
	{
		this.nativeType = nativeType;
	}

	// Token: 0x17000001 RID: 1
	// (get) Token: 0x06000006 RID: 6 RVA: 0x0000207B File Offset: 0x0000027B
	public string NativeType
	{
		get
		{
			return this.nativeType;
		}
	}

	// Token: 0x17000002 RID: 2
	// (get) Token: 0x06000007 RID: 7 RVA: 0x00002083 File Offset: 0x00000283
	// (set) Token: 0x06000008 RID: 8 RVA: 0x0000208B File Offset: 0x0000028B
	public string SuppressFlags
	{
		get
		{
			return this.suppressFlags;
		}
		set
		{
			this.suppressFlags = value;
		}
	}

	// Token: 0x0400002A RID: 42
	private string nativeType;

	// Token: 0x0400002B RID: 43
	private string suppressFlags;
}
