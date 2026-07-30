using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200000E RID: 14
	internal class CursorManager : ICursorManager
	{
		// Token: 0x0600004D RID: 77 RVA: 0x00003044 File Offset: 0x00001244
		public void SetCursor(Cursor cursor)
		{
			bool flag = cursor.texture != null;
			if (flag)
			{
				Cursor.SetCursor(cursor.texture, cursor.hotspot, CursorMode.Auto);
			}
			else
			{
				bool flag2 = cursor.defaultCursorId != 0;
				if (flag2)
				{
					Debug.LogWarning("Runtime cursors other than the default cursor need to be defined using a texture.");
				}
				this.ResetCursor();
			}
		}

		// Token: 0x0600004E RID: 78 RVA: 0x000030A0 File Offset: 0x000012A0
		public void ResetCursor()
		{
			Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
		}
	}
}
