using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms.CarbonInternal
{
	// Token: 0x020004A3 RID: 1187
	internal class Cursor
	{
		// Token: 0x06004BA4 RID: 19364 RVA: 0x0012CD3C File Offset: 0x0012AF3C
		internal static Bitmap DefineStdCursorBitmap(StdCursor id)
		{
			return new Bitmap(16, 16);
		}

		// Token: 0x06004BA5 RID: 19365 RVA: 0x0012CD48 File Offset: 0x0012AF48
		internal static IntPtr DefineCursor(Bitmap bitmap, Bitmap mask, Color cursor_pixel, Color mask_pixel, int xHotSpot, int yHotSpot)
		{
			CarbonCursor carbonCursor = new CarbonCursor(bitmap, mask, cursor_pixel, mask_pixel, xHotSpot, yHotSpot);
			return (IntPtr)GCHandle.Alloc(carbonCursor);
		}

		// Token: 0x06004BA6 RID: 19366 RVA: 0x0012CD74 File Offset: 0x0012AF74
		internal static IntPtr DefineStdCursor(StdCursor id)
		{
			CarbonCursor carbonCursor = new CarbonCursor(id);
			return (IntPtr)GCHandle.Alloc(carbonCursor);
		}

		// Token: 0x06004BA7 RID: 19367 RVA: 0x0012CD9C File Offset: 0x0012AF9C
		internal static void SetCursor(IntPtr cursor)
		{
			if (cursor == IntPtr.Zero)
			{
				Cursor.defcur.SetCursor();
				return;
			}
			((CarbonCursor)((GCHandle)cursor).Target).SetCursor();
		}

		// Token: 0x040028AE RID: 10414
		internal static CarbonCursor defcur = new CarbonCursor(StdCursor.Default);
	}
}
