using System;

// Token: 0x0200001D RID: 29
internal class DiagnosticListener
{
	// Token: 0x060000B5 RID: 181 RVA: 0x00005C14 File Offset: 0x00003E14
	internal DiagnosticListener(string s)
	{
	}

	// Token: 0x060000B6 RID: 182 RVA: 0x00005DE9 File Offset: 0x00003FE9
	internal bool IsEnabled(string s)
	{
		return DiagnosticListener.DiagnosticListenerEnabled;
	}

	// Token: 0x060000B7 RID: 183 RVA: 0x00005DF0 File Offset: 0x00003FF0
	internal void Write(string s1, object s2)
	{
		Console.WriteLine(string.Format("|| {0},  {1}", s1, s2));
	}

	// Token: 0x040003DD RID: 989
	internal static bool DiagnosticListenerEnabled;
}
