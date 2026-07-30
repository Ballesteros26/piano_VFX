using System;

namespace System.Windows.Forms
{
	/// <summary>Provides a collection of <see cref="T:System.Windows.Forms.Cursor" /> objects for use by a Windows Forms application.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000BA RID: 186
	public sealed class Cursors
	{
		// Token: 0x06000B6D RID: 2925 RVA: 0x0002EECC File Offset: 0x0002D0CC
		private Cursors()
		{
		}

		/// <summary>Gets the cursor that appears when an application starts.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Cursor" /> that represents the cursor that appears when an application starts.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06000B6E RID: 2926 RVA: 0x0002EED4 File Offset: 0x0002D0D4
		public static Cursor AppStarting
		{
			get
			{
				if (Cursors.app_starting == null)
				{
					Cursors.app_starting = new Cursor(StdCursor.AppStarting);
					Cursors.app_starting.name = "AppStarting";
				}
				return Cursors.app_starting;
			}
		}

		/// <summary>Gets the arrow cursor.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Cursor" /> that represents the arrow cursor.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000279 RID: 633
		// (get) Token: 0x06000B6F RID: 2927 RVA: 0x0002EF08 File Offset: 0x0002D108
		public static Cursor Arrow
		{
			get
			{
				if (Cursors.arrow == null)
				{
					Cursors.arrow = new Cursor(StdCursor.Arrow);
					Cursors.arrow.name = "Arrow";
				}
				return Cursors.arrow;
			}
		}

		/// <summary>Gets the crosshair cursor.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Cursor" /> that represents the crosshair cursor.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700027A RID: 634
		// (get) Token: 0x06000B70 RID: 2928 RVA: 0x0002EF3C File Offset: 0x0002D13C
		public static Cursor Cross
		{
			get
			{
				if (Cursors.cross == null)
				{
					Cursors.cross = new Cursor(StdCursor.Cross);
					Cursors.cross.name = "Cross";
				}
				return Cursors.cross;
			}
		}

		/// <summary>Gets the default cursor, which is usually an arrow cursor.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Cursor" /> that represents the default cursor.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700027B RID: 635
		// (get) Token: 0x06000B71 RID: 2929 RVA: 0x0002EF70 File Offset: 0x0002D170
		public static Cursor Default
		{
			get
			{
				if (Cursors.def == null)
				{
					Cursors.def = new Cursor(StdCursor.Default);
					Cursors.def.name = "Default";
				}
				return Cursors.def;
			}
		}

		/// <summary>Gets the hand cursor, typically used when hovering over a Web link.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Cursor" /> that represents the hand cursor.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700027C RID: 636
		// (get) Token: 0x06000B72 RID: 2930 RVA: 0x0002EFA4 File Offset: 0x0002D1A4
		public static Cursor Hand
		{
			get
			{
				if (Cursors.hand == null)
				{
					Cursors.hand = new Cursor(StdCursor.Hand);
					Cursors.hand.name = "Hand";
				}
				return Cursors.hand;
			}
		}

		/// <summary>Gets the Help cursor, which is a combination of an arrow and a question mark.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Cursor" /> that represents the Help cursor.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700027D RID: 637
		// (get) Token: 0x06000B73 RID: 2931 RVA: 0x0002EFD8 File Offset: 0x0002D1D8
		public static Cursor Help
		{
			get
			{
				if (Cursors.help == null)
				{
					Cursors.help = new Cursor(StdCursor.Help);
					Cursors.help.name = "Help";
				}
				return Cursors.help;
			}
		}

		/// <summary>Gets the cursor that appears when the mouse is positioned over a horizontal splitter bar.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Cursor" /> that represents the cursor that appears when the mouse is positioned over a horizontal splitter bar.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700027E RID: 638
		// (get) Token: 0x06000B74 RID: 2932 RVA: 0x0002F00C File Offset: 0x0002D20C
		public static Cursor HSplit
		{
			get
			{
				if (Cursors.hsplit == null)
				{
					Cursors.hsplit = new Cursor(typeof(Splitter), "SplitterNS.cur");
					Cursors.hsplit.name = "HSplit";
				}
				return Cursors.hsplit;
			}
		}

		/// <summary>Gets the I-beam cursor, which is used to show where the text cursor appears when the mouse is clicked.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Cursor" /> that represents the I-beam cursor.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700027F RID: 639
		// (get) Token: 0x06000B75 RID: 2933 RVA: 0x0002F04C File Offset: 0x0002D24C
		public static Cursor IBeam
		{
			get
			{
				if (Cursors.ibeam == null)
				{
					Cursors.ibeam = new Cursor(StdCursor.IBeam);
					Cursors.ibeam.name = "IBeam";
				}
				return Cursors.ibeam;
			}
		}

		/// <summary>Gets the cursor that indicates that a particular region is invalid for the current operation.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Cursor" /> that represents the cursor that indicates that a particular region is invalid for the current operation.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000280 RID: 640
		// (get) Token: 0x06000B76 RID: 2934 RVA: 0x0002F080 File Offset: 0x0002D280
		public static Cursor No
		{
			get
			{
				if (Cursors.no == null)
				{
					Cursors.no = new Cursor(StdCursor.No);
					Cursors.no.name = "No";
				}
				return Cursors.no;
			}
		}

		/// <summary>Gets the cursor that appears during wheel operations when the mouse is not moving, but the window can be scrolled in both a horizontal and vertical direction.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Cursor" /> that represents the cursor that appears during wheel operations when the mouse is not moving.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000281 RID: 641
		// (get) Token: 0x06000B77 RID: 2935 RVA: 0x0002F0B4 File Offset: 0x0002D2B4
		public static Cursor NoMove2D
		{
			get
			{
				if (Cursors.no_move_2d == null)
				{
					Cursors.no_move_2d = new Cursor(StdCursor.NoMove2D);
					Cursors.no_move_2d.name = "NoMove2D";
				}
				return Cursors.no_move_2d;
			}
		}

		/// <summary>Gets the cursor that appears during wheel operations when the mouse is not moving, but the window can be scrolled in a horizontal direction.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Cursor" /> that represents the cursor that appears during wheel operations when the mouse is not moving.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000282 RID: 642
		// (get) Token: 0x06000B78 RID: 2936 RVA: 0x0002F0F4 File Offset: 0x0002D2F4
		public static Cursor NoMoveHoriz
		{
			get
			{
				if (Cursors.no_move_horiz == null)
				{
					Cursors.no_move_horiz = new Cursor(StdCursor.NoMoveHoriz);
					Cursors.no_move_horiz.name = "NoMoveHoriz";
				}
				return Cursors.no_move_horiz;
			}
		}

		/// <summary>Gets the cursor that appears during wheel operations when the mouse is not moving, but the window can be scrolled in a vertical direction.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Cursor" /> that represents the cursor that appears during wheel operations when the mouse is not moving.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000283 RID: 643
		// (get) Token: 0x06000B79 RID: 2937 RVA: 0x0002F134 File Offset: 0x0002D334
		public static Cursor NoMoveVert
		{
			get
			{
				if (Cursors.no_move_vert == null)
				{
					Cursors.no_move_vert = new Cursor(StdCursor.NoMoveVert);
					Cursors.no_move_vert.name = "NoMoveVert";
				}
				return Cursors.no_move_vert;
			}
		}

		/// <summary>Gets the cursor that appears during wheel operations when the mouse is moving and the window is scrolling horizontally to the right.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Cursor" /> that represents the cursor that appears during wheel operations when the mouse is moving and the window is scrolling horizontally to the right.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000284 RID: 644
		// (get) Token: 0x06000B7A RID: 2938 RVA: 0x0002F174 File Offset: 0x0002D374
		public static Cursor PanEast
		{
			get
			{
				if (Cursors.pan_east == null)
				{
					Cursors.pan_east = new Cursor(StdCursor.PanEast);
					Cursors.pan_east.name = "PanEast";
				}
				return Cursors.pan_east;
			}
		}

		/// <summary>Gets the cursor that appears during wheel operations when the mouse is moving and the window is scrolling horizontally and vertically upward and to the right.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Cursor" /> that represents the cursor that appears during wheel operations when the mouse is moving and the window is scrolling horizontally and vertically upward and to the right.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000285 RID: 645
		// (get) Token: 0x06000B7B RID: 2939 RVA: 0x0002F1B4 File Offset: 0x0002D3B4
		public static Cursor PanNE
		{
			get
			{
				if (Cursors.pan_ne == null)
				{
					Cursors.pan_ne = new Cursor(StdCursor.PanNE);
					Cursors.pan_ne.name = "PanNE";
				}
				return Cursors.pan_ne;
			}
		}

		/// <summary>Gets the cursor that appears during wheel operations when the mouse is moving and the window is scrolling vertically in an upward direction.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Cursor" /> that represents the cursor that appears during wheel operations when the mouse is moving and the window is scrolling vertically in an upward direction.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000286 RID: 646
		// (get) Token: 0x06000B7C RID: 2940 RVA: 0x0002F1F4 File Offset: 0x0002D3F4
		public static Cursor PanNorth
		{
			get
			{
				if (Cursors.pan_north == null)
				{
					Cursors.pan_north = new Cursor(StdCursor.PanNorth);
					Cursors.pan_north.name = "PanNorth";
				}
				return Cursors.pan_north;
			}
		}

		/// <summary>Gets the cursor that appears during wheel operations when the mouse is moving and the window is scrolling horizontally and vertically upward and to the left.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Cursor" /> that represents the cursor that appears during wheel operations when the mouse is moving and the window is scrolling horizontally and vertically upward and to the left.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000287 RID: 647
		// (get) Token: 0x06000B7D RID: 2941 RVA: 0x0002F234 File Offset: 0x0002D434
		public static Cursor PanNW
		{
			get
			{
				if (Cursors.pan_nw == null)
				{
					Cursors.pan_nw = new Cursor(StdCursor.PanNW);
					Cursors.pan_nw.name = "PanNW";
				}
				return Cursors.pan_nw;
			}
		}

		/// <summary>Gets the cursor that appears during wheel operations when the mouse is moving and the window is scrolling horizontally and vertically downward and to the right.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Cursor" /> that represents the cursor that appears during wheel operations when the mouse is moving and the window is scrolling horizontally and vertically downward and to the right.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000288 RID: 648
		// (get) Token: 0x06000B7E RID: 2942 RVA: 0x0002F274 File Offset: 0x0002D474
		public static Cursor PanSE
		{
			get
			{
				if (Cursors.pan_se == null)
				{
					Cursors.pan_se = new Cursor(StdCursor.PanSE);
					Cursors.pan_se.name = "PanSE";
				}
				return Cursors.pan_se;
			}
		}

		/// <summary>Gets the cursor that appears during wheel operations when the mouse is moving and the window is scrolling vertically in a downward direction.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Cursor" /> that represents the cursor that appears during wheel operations when the mouse is moving and the window is scrolling vertically in a downward direction.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000289 RID: 649
		// (get) Token: 0x06000B7F RID: 2943 RVA: 0x0002F2B4 File Offset: 0x0002D4B4
		public static Cursor PanSouth
		{
			get
			{
				if (Cursors.pan_south == null)
				{
					Cursors.pan_south = new Cursor(StdCursor.PanSouth);
					Cursors.pan_south.name = "PanSouth";
				}
				return Cursors.pan_south;
			}
		}

		/// <summary>Gets the cursor that appears during wheel operations when the mouse is moving and the window is scrolling horizontally and vertically downward and to the left.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Cursor" /> that represents the cursor that appears during wheel operations when the mouse is moving and the window is scrolling horizontally and vertically downward and to the left.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700028A RID: 650
		// (get) Token: 0x06000B80 RID: 2944 RVA: 0x0002F2F4 File Offset: 0x0002D4F4
		public static Cursor PanSW
		{
			get
			{
				if (Cursors.pan_sw == null)
				{
					Cursors.pan_sw = new Cursor(StdCursor.PanSW);
					Cursors.pan_sw.name = "PanSW";
				}
				return Cursors.pan_sw;
			}
		}

		/// <summary>Gets the cursor that appears during wheel operations when the mouse is moving and the window is scrolling horizontally to the left.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Cursor" /> that represents the cursor that appears during wheel operations when the mouse is moving and the window is scrolling horizontally to the left.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700028B RID: 651
		// (get) Token: 0x06000B81 RID: 2945 RVA: 0x0002F334 File Offset: 0x0002D534
		public static Cursor PanWest
		{
			get
			{
				if (Cursors.pan_west == null)
				{
					Cursors.pan_west = new Cursor(StdCursor.PanWest);
					Cursors.pan_west.name = "PanWest";
				}
				return Cursors.pan_west;
			}
		}

		/// <summary>Gets the four-headed sizing cursor, which consists of four joined arrows that point north, south, east, and west.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Cursor" /> that represents the four-headed sizing cursor.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700028C RID: 652
		// (get) Token: 0x06000B82 RID: 2946 RVA: 0x0002F374 File Offset: 0x0002D574
		public static Cursor SizeAll
		{
			get
			{
				if (Cursors.size_all == null)
				{
					Cursors.size_all = new Cursor(StdCursor.SizeAll);
					Cursors.size_all.name = "SizeAll";
				}
				return Cursors.size_all;
			}
		}

		/// <summary>Gets the two-headed diagonal (northeast/southwest) sizing cursor.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Cursor" /> that represents two-headed diagonal (northeast/southwest) sizing cursor.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700028D RID: 653
		// (get) Token: 0x06000B83 RID: 2947 RVA: 0x0002F3B4 File Offset: 0x0002D5B4
		public static Cursor SizeNESW
		{
			get
			{
				if (Cursors.size_nesw == null)
				{
					if (XplatUI.RunningOnUnix)
					{
						Cursors.size_nesw = new Cursor(typeof(Cursor), "NESW.cur");
						Cursors.size_nesw.name = "SizeNESW";
					}
					else
					{
						Cursors.size_nesw = new Cursor(StdCursor.SizeNWSE);
						Cursors.size_nesw.name = "SizeNESW";
					}
				}
				return Cursors.size_nesw;
			}
		}

		/// <summary>Gets the two-headed vertical (north/south) sizing cursor.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Cursor" /> that represents the two-headed vertical (north/south) sizing cursor.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700028E RID: 654
		// (get) Token: 0x06000B84 RID: 2948 RVA: 0x0002F428 File Offset: 0x0002D628
		public static Cursor SizeNS
		{
			get
			{
				if (Cursors.size_ns == null)
				{
					Cursors.size_ns = new Cursor(StdCursor.SizeNS);
					Cursors.size_ns.name = "SizeNS";
				}
				return Cursors.size_ns;
			}
		}

		/// <summary>Gets the two-headed diagonal (northwest/southeast) sizing cursor.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Cursor" /> that represents the two-headed diagonal (northwest/southeast) sizing cursor.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700028F RID: 655
		// (get) Token: 0x06000B85 RID: 2949 RVA: 0x0002F468 File Offset: 0x0002D668
		public static Cursor SizeNWSE
		{
			get
			{
				if (Cursors.size_nwse == null)
				{
					if (XplatUI.RunningOnUnix)
					{
						Cursors.size_nwse = new Cursor(typeof(Cursor), "NWSE.cur");
						Cursors.size_nwse.name = "SizeNWSE";
					}
					else
					{
						Cursors.size_nwse = new Cursor(StdCursor.SizeNWSE);
						Cursors.size_nwse.name = "SizeNWSE";
					}
				}
				return Cursors.size_nwse;
			}
		}

		/// <summary>Gets the two-headed horizontal (west/east) sizing cursor.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Cursor" /> that represents the two-headed horizontal (west/east) sizing cursor.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000290 RID: 656
		// (get) Token: 0x06000B86 RID: 2950 RVA: 0x0002F4DC File Offset: 0x0002D6DC
		public static Cursor SizeWE
		{
			get
			{
				if (Cursors.size_we == null)
				{
					Cursors.size_we = new Cursor(StdCursor.SizeWE);
					Cursors.size_we.name = "SizeWE";
				}
				return Cursors.size_we;
			}
		}

		/// <summary>Gets the up arrow cursor, typically used to identify an insertion point.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Cursor" /> that represents the up arrow cursor.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000291 RID: 657
		// (get) Token: 0x06000B87 RID: 2951 RVA: 0x0002F51C File Offset: 0x0002D71C
		public static Cursor UpArrow
		{
			get
			{
				if (Cursors.up_arrow == null)
				{
					Cursors.up_arrow = new Cursor(StdCursor.UpArrow);
					Cursors.up_arrow.name = "UpArrow";
				}
				return Cursors.up_arrow;
			}
		}

		/// <summary>Gets the cursor that appears when the mouse is positioned over a vertical splitter bar.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Cursor" /> that represents the cursor that appears when the mouse is positioned over a vertical splitter bar.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000292 RID: 658
		// (get) Token: 0x06000B88 RID: 2952 RVA: 0x0002F55C File Offset: 0x0002D75C
		public static Cursor VSplit
		{
			get
			{
				if (Cursors.vsplit == null)
				{
					Cursors.vsplit = new Cursor(typeof(Cursor), "SplitterWE.cur");
					Cursors.vsplit.name = "VSplit";
				}
				return Cursors.vsplit;
			}
		}

		/// <summary>Gets the wait cursor, typically an hourglass shape.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Cursor" /> that represents the wait cursor.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000293 RID: 659
		// (get) Token: 0x06000B89 RID: 2953 RVA: 0x0002F59C File Offset: 0x0002D79C
		public static Cursor WaitCursor
		{
			get
			{
				if (Cursors.wait_cursor == null)
				{
					Cursors.wait_cursor = new Cursor(StdCursor.WaitCursor);
					Cursors.wait_cursor.name = "WaitCursor";
				}
				return Cursors.wait_cursor;
			}
		}

		// Token: 0x04000894 RID: 2196
		internal static Cursor app_starting;

		// Token: 0x04000895 RID: 2197
		internal static Cursor arrow;

		// Token: 0x04000896 RID: 2198
		internal static Cursor cross;

		// Token: 0x04000897 RID: 2199
		internal static Cursor def;

		// Token: 0x04000898 RID: 2200
		internal static Cursor hand;

		// Token: 0x04000899 RID: 2201
		internal static Cursor help;

		// Token: 0x0400089A RID: 2202
		internal static Cursor hsplit;

		// Token: 0x0400089B RID: 2203
		internal static Cursor ibeam;

		// Token: 0x0400089C RID: 2204
		internal static Cursor no;

		// Token: 0x0400089D RID: 2205
		internal static Cursor no_move_2d;

		// Token: 0x0400089E RID: 2206
		internal static Cursor no_move_horiz;

		// Token: 0x0400089F RID: 2207
		internal static Cursor no_move_vert;

		// Token: 0x040008A0 RID: 2208
		internal static Cursor pan_east;

		// Token: 0x040008A1 RID: 2209
		internal static Cursor pan_ne;

		// Token: 0x040008A2 RID: 2210
		internal static Cursor pan_north;

		// Token: 0x040008A3 RID: 2211
		internal static Cursor pan_nw;

		// Token: 0x040008A4 RID: 2212
		internal static Cursor pan_se;

		// Token: 0x040008A5 RID: 2213
		internal static Cursor pan_south;

		// Token: 0x040008A6 RID: 2214
		internal static Cursor pan_sw;

		// Token: 0x040008A7 RID: 2215
		internal static Cursor pan_west;

		// Token: 0x040008A8 RID: 2216
		internal static Cursor size_all;

		// Token: 0x040008A9 RID: 2217
		internal static Cursor size_nesw;

		// Token: 0x040008AA RID: 2218
		internal static Cursor size_ns;

		// Token: 0x040008AB RID: 2219
		internal static Cursor size_nwse;

		// Token: 0x040008AC RID: 2220
		internal static Cursor size_we;

		// Token: 0x040008AD RID: 2221
		internal static Cursor up_arrow;

		// Token: 0x040008AE RID: 2222
		internal static Cursor vsplit;

		// Token: 0x040008AF RID: 2223
		internal static Cursor wait_cursor;
	}
}
