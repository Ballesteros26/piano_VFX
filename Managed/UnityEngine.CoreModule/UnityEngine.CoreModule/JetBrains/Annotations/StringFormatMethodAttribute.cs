using System;

namespace JetBrains.Annotations
{
	// Token: 0x0200007D RID: 125
	[AttributeUsage(96, AllowMultiple = false, Inherited = true)]
	public sealed class StringFormatMethodAttribute : Attribute
	{
		// Token: 0x06000199 RID: 409 RVA: 0x00003FA6 File Offset: 0x000021A6
		public StringFormatMethodAttribute(string formatParameterName)
		{
			this.FormatParameterName = formatParameterName;
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600019A RID: 410 RVA: 0x00003FB8 File Offset: 0x000021B8
		// (set) Token: 0x0600019B RID: 411 RVA: 0x00003FC0 File Offset: 0x000021C0
		public string FormatParameterName { get; private set; }
	}
}
