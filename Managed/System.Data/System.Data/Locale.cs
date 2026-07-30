using System;

// Token: 0x0200001B RID: 27
internal sealed class Locale
{
	// Token: 0x060000A9 RID: 169 RVA: 0x00005C14 File Offset: 0x00003E14
	private Locale()
	{
	}

	// Token: 0x060000AA RID: 170 RVA: 0x00005D82 File Offset: 0x00003F82
	public static string GetText(string msg)
	{
		return msg;
	}

	// Token: 0x060000AB RID: 171 RVA: 0x00005D85 File Offset: 0x00003F85
	public static string GetText(string fmt, params object[] args)
	{
		return string.Format(fmt, args);
	}
}
