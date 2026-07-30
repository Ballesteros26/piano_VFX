using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms.CarbonInternal
{
	// Token: 0x020004A4 RID: 1188
	internal struct CarbonCursor
	{
		// Token: 0x06004BA8 RID: 19368 RVA: 0x0012CDE0 File Offset: 0x0012AFE0
		public CarbonCursor(Bitmap bitmap, Bitmap mask, Color cursor_pixel, Color mask_pixel, int xHotSpot, int yHotSpot)
		{
			this.id = StdCursor.Default;
			this.bmp = bitmap;
			this.mask = mask;
			this.cursor_color = cursor_pixel;
			this.mask_color = mask_pixel;
			this.hot_x = xHotSpot;
			this.hot_y = yHotSpot;
			this.standard = true;
		}

		// Token: 0x06004BA9 RID: 19369 RVA: 0x0012CE20 File Offset: 0x0012B020
		public CarbonCursor(StdCursor id)
		{
			this.id = id;
			this.bmp = null;
			this.mask = null;
			this.cursor_color = Color.Black;
			this.mask_color = Color.Black;
			this.hot_x = 0;
			this.hot_y = 0;
			this.standard = true;
		}

		// Token: 0x17001311 RID: 4881
		// (get) Token: 0x06004BAA RID: 19370 RVA: 0x0012CE70 File Offset: 0x0012B070
		public StdCursor StdCursor
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x17001312 RID: 4882
		// (get) Token: 0x06004BAB RID: 19371 RVA: 0x0012CE78 File Offset: 0x0012B078
		public Bitmap Bitmap
		{
			get
			{
				return this.bmp;
			}
		}

		// Token: 0x17001313 RID: 4883
		// (get) Token: 0x06004BAC RID: 19372 RVA: 0x0012CE80 File Offset: 0x0012B080
		public Bitmap Mask
		{
			get
			{
				return this.mask;
			}
		}

		// Token: 0x17001314 RID: 4884
		// (get) Token: 0x06004BAD RID: 19373 RVA: 0x0012CE88 File Offset: 0x0012B088
		public Color CursorColor
		{
			get
			{
				return this.cursor_color;
			}
		}

		// Token: 0x17001315 RID: 4885
		// (get) Token: 0x06004BAE RID: 19374 RVA: 0x0012CE90 File Offset: 0x0012B090
		public Color MaskColor
		{
			get
			{
				return this.mask_color;
			}
		}

		// Token: 0x17001316 RID: 4886
		// (get) Token: 0x06004BAF RID: 19375 RVA: 0x0012CE98 File Offset: 0x0012B098
		public int HotSpotX
		{
			get
			{
				return this.hot_x;
			}
		}

		// Token: 0x17001317 RID: 4887
		// (get) Token: 0x06004BB0 RID: 19376 RVA: 0x0012CEA0 File Offset: 0x0012B0A0
		public int HotSpotY
		{
			get
			{
				return this.hot_y;
			}
		}

		// Token: 0x06004BB1 RID: 19377 RVA: 0x0012CEA8 File Offset: 0x0012B0A8
		public void SetCursor()
		{
			if (this.standard)
			{
				this.SetStandardCursor();
			}
			else
			{
				this.SetCustomCursor();
			}
		}

		// Token: 0x06004BB2 RID: 19378 RVA: 0x0012CEC8 File Offset: 0x0012B0C8
		public void SetCustomCursor()
		{
			throw new NotImplementedException("We dont support custom cursors yet");
		}

		// Token: 0x06004BB3 RID: 19379 RVA: 0x0012CED4 File Offset: 0x0012B0D4
		public void SetStandardCursor()
		{
			switch (this.id)
			{
			case StdCursor.Default:
				CarbonCursor.SetThemeCursor(ThemeCursor.kThemeArrowCursor);
				break;
			case StdCursor.AppStarting:
				CarbonCursor.SetThemeCursor(ThemeCursor.kThemeSpinningCursor);
				break;
			case StdCursor.Arrow:
				CarbonCursor.SetThemeCursor(ThemeCursor.kThemeArrowCursor);
				break;
			case StdCursor.Cross:
				CarbonCursor.SetThemeCursor(ThemeCursor.kThemeCrossCursor);
				break;
			case StdCursor.Hand:
				CarbonCursor.SetThemeCursor(ThemeCursor.kThemeOpenHandCursor);
				break;
			case StdCursor.Help:
				CarbonCursor.SetThemeCursor(ThemeCursor.kThemeArrowCursor);
				break;
			case StdCursor.HSplit:
				CarbonCursor.SetThemeCursor(ThemeCursor.kThemeResizeLeftRightCursor);
				break;
			case StdCursor.IBeam:
				CarbonCursor.SetThemeCursor(ThemeCursor.kThemeIBeamCursor);
				break;
			case StdCursor.No:
				CarbonCursor.SetThemeCursor(ThemeCursor.kThemeNotAllowedCursor);
				break;
			case StdCursor.NoMove2D:
				CarbonCursor.SetThemeCursor(ThemeCursor.kThemeNotAllowedCursor);
				break;
			case StdCursor.NoMoveHoriz:
				CarbonCursor.SetThemeCursor(ThemeCursor.kThemeNotAllowedCursor);
				break;
			case StdCursor.NoMoveVert:
				CarbonCursor.SetThemeCursor(ThemeCursor.kThemeNotAllowedCursor);
				break;
			case StdCursor.PanEast:
				CarbonCursor.SetThemeCursor(ThemeCursor.kThemeResizeRightCursor);
				break;
			case StdCursor.PanNE:
				CarbonCursor.SetThemeCursor(ThemeCursor.kThemeArrowCursor);
				break;
			case StdCursor.PanNorth:
				CarbonCursor.SetThemeCursor(ThemeCursor.kThemeArrowCursor);
				break;
			case StdCursor.PanNW:
				CarbonCursor.SetThemeCursor(ThemeCursor.kThemeArrowCursor);
				break;
			case StdCursor.PanSE:
				CarbonCursor.SetThemeCursor(ThemeCursor.kThemeArrowCursor);
				break;
			case StdCursor.PanSouth:
				CarbonCursor.SetThemeCursor(ThemeCursor.kThemeArrowCursor);
				break;
			case StdCursor.PanSW:
				CarbonCursor.SetThemeCursor(ThemeCursor.kThemeArrowCursor);
				break;
			case StdCursor.PanWest:
				CarbonCursor.SetThemeCursor(ThemeCursor.kThemeResizeLeftCursor);
				break;
			case StdCursor.SizeAll:
				CarbonCursor.SetThemeCursor(ThemeCursor.kThemeResizeLeftRightCursor);
				break;
			case StdCursor.SizeNESW:
				CarbonCursor.SetThemeCursor(ThemeCursor.kThemeArrowCursor);
				break;
			case StdCursor.SizeNS:
				CarbonCursor.SetThemeCursor(ThemeCursor.kThemeArrowCursor);
				break;
			case StdCursor.SizeNWSE:
				CarbonCursor.SetThemeCursor(ThemeCursor.kThemeArrowCursor);
				break;
			case StdCursor.SizeWE:
				CarbonCursor.SetThemeCursor(ThemeCursor.kThemeArrowCursor);
				break;
			case StdCursor.UpArrow:
				CarbonCursor.SetThemeCursor(ThemeCursor.kThemeArrowCursor);
				break;
			case StdCursor.VSplit:
				CarbonCursor.SetThemeCursor(ThemeCursor.kThemeArrowCursor);
				break;
			case StdCursor.WaitCursor:
				CarbonCursor.SetThemeCursor(ThemeCursor.kThemeSpinningCursor);
				break;
			default:
				CarbonCursor.SetThemeCursor(ThemeCursor.kThemeArrowCursor);
				break;
			}
		}

		// Token: 0x06004BB4 RID: 19380
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern int SetThemeCursor(ThemeCursor cursor);

		// Token: 0x040028AF RID: 10415
		private Bitmap bmp;

		// Token: 0x040028B0 RID: 10416
		private Bitmap mask;

		// Token: 0x040028B1 RID: 10417
		private Color cursor_color;

		// Token: 0x040028B2 RID: 10418
		private Color mask_color;

		// Token: 0x040028B3 RID: 10419
		private int hot_x;

		// Token: 0x040028B4 RID: 10420
		private int hot_y;

		// Token: 0x040028B5 RID: 10421
		private StdCursor id;

		// Token: 0x040028B6 RID: 10422
		private bool standard;
	}
}
