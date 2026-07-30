using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000160 RID: 352
	[NativeHeader("Runtime/Export/Input/Cursor.bindings.h")]
	public class Cursor
	{
		// Token: 0x06000FF4 RID: 4084 RVA: 0x0001668F File Offset: 0x0001488F
		private static void SetCursor(Texture2D texture, CursorMode cursorMode)
		{
			Cursor.SetCursor(texture, Vector2.zero, cursorMode);
		}

		// Token: 0x06000FF5 RID: 4085 RVA: 0x0001669F File Offset: 0x0001489F
		public static void SetCursor(Texture2D texture, Vector2 hotspot, CursorMode cursorMode)
		{
			Cursor.SetCursor_Injected(texture, ref hotspot, cursorMode);
		}

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x06000FF6 RID: 4086
		// (set) Token: 0x06000FF7 RID: 4087
		public static extern bool visible
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x06000FF8 RID: 4088
		// (set) Token: 0x06000FF9 RID: 4089
		public static extern CursorLockMode lockState
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x06000FFB RID: 4091
		[MethodImpl(4096)]
		private static extern void SetCursor_Injected(Texture2D texture, ref Vector2 hotspot, CursorMode cursorMode);
	}
}
