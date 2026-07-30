using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200002A RID: 42
	internal class ImmediateModeException : Exception
	{
		// Token: 0x060000F1 RID: 241 RVA: 0x0000591C File Offset: 0x00003B1C
		public ImmediateModeException(Exception inner)
			: base("", inner)
		{
		}
	}
}
