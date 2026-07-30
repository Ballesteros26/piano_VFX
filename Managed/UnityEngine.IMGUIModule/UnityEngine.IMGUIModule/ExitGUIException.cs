using System;

namespace UnityEngine
{
	// Token: 0x0200002F RID: 47
	public sealed class ExitGUIException : Exception
	{
		// Token: 0x060003C0 RID: 960 RVA: 0x0000C3A0 File Offset: 0x0000A5A0
		public ExitGUIException()
		{
			GUIUtility.guiIsExiting = true;
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x0000C3B1 File Offset: 0x0000A5B1
		internal ExitGUIException(string message)
			: base(message)
		{
			GUIUtility.guiIsExiting = true;
		}
	}
}
