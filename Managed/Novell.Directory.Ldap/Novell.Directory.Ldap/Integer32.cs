using System;

// Token: 0x02000004 RID: 4
public class Integer32
{
	// Token: 0x06000002 RID: 2 RVA: 0x00002050 File Offset: 0x00000250
	public Integer32(int ival)
	{
		this._wintv = ival;
	}

	// Token: 0x17000001 RID: 1
	// (get) Token: 0x06000003 RID: 3 RVA: 0x0000205F File Offset: 0x0000025F
	// (set) Token: 0x06000004 RID: 4 RVA: 0x00002067 File Offset: 0x00000267
	public int intValue
	{
		get
		{
			return this._wintv;
		}
		set
		{
			this._wintv = value;
		}
	}

	// Token: 0x0400002A RID: 42
	private int _wintv;
}
