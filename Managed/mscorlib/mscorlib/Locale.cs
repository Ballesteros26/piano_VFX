using System;

// Token: 0x02000007 RID: 7
internal sealed class Locale
{
	// Token: 0x06000006 RID: 6 RVA: 0x00002111 File Offset: 0x00000311
	private Locale()
	{
	}

	// Token: 0x06000007 RID: 7 RVA: 0x00002119 File Offset: 0x00000319
	public static string GetText(string msg)
	{
		return msg;
	}

	// Token: 0x06000008 RID: 8 RVA: 0x0000211C File Offset: 0x0000031C
	public static string GetText(string fmt, params object[] args)
	{
		return string.Format(fmt, args);
	}
}
