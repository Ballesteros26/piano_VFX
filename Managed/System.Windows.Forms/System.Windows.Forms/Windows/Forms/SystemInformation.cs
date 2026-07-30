using System;
using System.ComponentModel;
using System.Drawing;

namespace System.Windows.Forms
{
	/// <summary>Provides information about the current system environment.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020002F2 RID: 754
	public class SystemInformation
	{
		// Token: 0x060031DA RID: 12762 RVA: 0x000BEF80 File Offset: 0x000BD180
		private SystemInformation()
		{
		}

		/// <summary>Gets the active window tracking delay.</summary>
		/// <returns>The active window tracking delay, in milliseconds.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000CB7 RID: 3255
		// (get) Token: 0x060031DB RID: 12763 RVA: 0x000BEF88 File Offset: 0x000BD188
		[MonoInternalNote("Determine if we need an X11 implementation or if defaults are good.")]
		public static int ActiveWindowTrackingDelay
		{
			get
			{
				return XplatUI.ActiveWindowTrackingDelay;
			}
		}

		/// <summary>Gets a value that indicates the direction in which the operating system arranges minimized windows.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ArrangeDirection" /> values that indicates the direction in which the operating system arranges minimized windows.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000CB8 RID: 3256
		// (get) Token: 0x060031DC RID: 12764 RVA: 0x000BEF90 File Offset: 0x000BD190
		public static ArrangeDirection ArrangeDirection
		{
			get
			{
				return ThemeEngine.Current.ArrangeDirection;
			}
		}

		/// <summary>Gets an <see cref="T:System.Windows.Forms.ArrangeStartingPosition" /> value that indicates the starting position from which the operating system arranges minimized windows.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ArrangeStartingPosition" /> values that indicates the starting position from which the operating system arranges minimized windows.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000CB9 RID: 3257
		// (get) Token: 0x060031DD RID: 12765 RVA: 0x000BEF9C File Offset: 0x000BD19C
		public static ArrangeStartingPosition ArrangeStartingPosition
		{
			get
			{
				return ThemeEngine.Current.ArrangeStartingPosition;
			}
		}

		/// <summary>Gets a <see cref="T:System.Windows.Forms.BootMode" /> value that indicates the boot mode the system was started in.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.BootMode" /> values that indicates the boot mode the system was started in.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000CBA RID: 3258
		// (get) Token: 0x060031DE RID: 12766 RVA: 0x000BEFA8 File Offset: 0x000BD1A8
		public static BootMode BootMode
		{
			get
			{
				return BootMode.Normal;
			}
		}

		/// <summary>Gets the thickness, in pixels, of a three-dimensional (3-D) style window or system control border.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that indicates the width, in pixels, of a 3-D style vertical border, and the height, in pixels, of a 3-D style horizontal border.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000CBB RID: 3259
		// (get) Token: 0x060031DF RID: 12767 RVA: 0x000BEFAC File Offset: 0x000BD1AC
		public static Size Border3DSize
		{
			get
			{
				return ThemeEngine.Current.Border3DSize;
			}
		}

		/// <summary>Gets the border multiplier factor that is used when determining the thickness of a window's sizing border.</summary>
		/// <returns>The multiplier used to determine the thickness of a window's sizing border.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000CBC RID: 3260
		// (get) Token: 0x060031E0 RID: 12768 RVA: 0x000BEFB8 File Offset: 0x000BD1B8
		[MonoInternalNote("Determine if we need an X11 implementation or if defaults are good.")]
		public static int BorderMultiplierFactor
		{
			get
			{
				return ThemeEngine.Current.BorderMultiplierFactor;
			}
		}

		/// <summary>Gets the thickness, in pixels, of a flat-style window or system control border.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that indicates the width, in pixels, of a vertical border, and the height, in pixels, of a horizontal border.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000CBD RID: 3261
		// (get) Token: 0x060031E1 RID: 12769 RVA: 0x000BEFC4 File Offset: 0x000BD1C4
		public static Size BorderSize
		{
			get
			{
				return ThemeEngine.Current.BorderSize;
			}
		}

		/// <summary>Gets the standard size, in pixels, of a button in a window's title bar.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that indicates the standard dimensions, in pixels, of a button in a window's title bar.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000CBE RID: 3262
		// (get) Token: 0x060031E2 RID: 12770 RVA: 0x000BEFD0 File Offset: 0x000BD1D0
		public static Size CaptionButtonSize
		{
			get
			{
				return ThemeEngine.Current.CaptionButtonSize;
			}
		}

		/// <summary>Gets the height, in pixels, of the standard title bar area of a window.</summary>
		/// <returns>The height, in pixels, of the standard title bar area of a window.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000CBF RID: 3263
		// (get) Token: 0x060031E3 RID: 12771 RVA: 0x000BEFDC File Offset: 0x000BD1DC
		public static int CaptionHeight
		{
			get
			{
				return ThemeEngine.Current.CaptionHeight;
			}
		}

		/// <summary>Gets the caret blink time.</summary>
		/// <returns>The caret blink time.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000CC0 RID: 3264
		// (get) Token: 0x060031E4 RID: 12772 RVA: 0x000BEFE8 File Offset: 0x000BD1E8
		[MonoInternalNote("Determine if we need an X11 implementation or if defaults are good.")]
		public static int CaretBlinkTime
		{
			get
			{
				return XplatUI.CaretBlinkTime;
			}
		}

		/// <summary>Gets the width, in pixels, of the caret in edit controls.</summary>
		/// <returns>The width, in pixels, of the caret in edit controls.</returns>
		/// <exception cref="T:System.NotSupportedException">The operating system does not support this feature.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000CC1 RID: 3265
		// (get) Token: 0x060031E5 RID: 12773 RVA: 0x000BEFF0 File Offset: 0x000BD1F0
		[MonoInternalNote("Determine if we need an X11 implementation or if defaults are good.")]
		public static int CaretWidth
		{
			get
			{
				return XplatUI.CaretWidth;
			}
		}

		/// <summary>Gets the NetBIOS computer name of the local computer.</summary>
		/// <returns>The name of this computer.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000CC2 RID: 3266
		// (get) Token: 0x060031E6 RID: 12774 RVA: 0x000BEFF8 File Offset: 0x000BD1F8
		public static string ComputerName
		{
			get
			{
				return Environment.MachineName;
			}
		}

		/// <summary>Gets the maximum size, in pixels, that a cursor can occupy.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that indicates the maximum dimensions of a cursor in pixels.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000CC3 RID: 3267
		// (get) Token: 0x060031E7 RID: 12775 RVA: 0x000BF000 File Offset: 0x000BD200
		public static Size CursorSize
		{
			get
			{
				return XplatUI.CursorSize;
			}
		}

		/// <summary>Gets a value indicating whether the operating system is capable of handling double-byte character set (DBCS) characters.</summary>
		/// <returns>true if the operating system supports DBCS; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000CC4 RID: 3268
		// (get) Token: 0x060031E8 RID: 12776 RVA: 0x000BF008 File Offset: 0x000BD208
		public static bool DbcsEnabled
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether the debug version of USER.EXE is installed.</summary>
		/// <returns>true if the debugging version of USER.EXE is installed; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000CC5 RID: 3269
		// (get) Token: 0x060031E9 RID: 12777 RVA: 0x000BF00C File Offset: 0x000BD20C
		public static bool DebugOS
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets the dimensions, in pixels, of the area within which the user must click twice for the operating system to consider the two clicks a double-click.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that indicates the dimensions, in pixels, of the area within which the user must click twice for the operating system to consider the two clicks a double-click.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000CC6 RID: 3270
		// (get) Token: 0x060031EA RID: 12778 RVA: 0x000BF010 File Offset: 0x000BD210
		public static Size DoubleClickSize
		{
			get
			{
				return ThemeEngine.Current.DoubleClickSize;
			}
		}

		/// <summary>Gets the maximum number of milliseconds that can elapse between a first click and a second click for the OS to consider the mouse action a double-click.</summary>
		/// <returns>The maximum amount of time, in milliseconds, that can elapse between a first click and a second click for the OS to consider the mouse action a double-click.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000CC7 RID: 3271
		// (get) Token: 0x060031EB RID: 12779 RVA: 0x000BF01C File Offset: 0x000BD21C
		public static int DoubleClickTime
		{
			get
			{
				return ThemeEngine.Current.DoubleClickTime;
			}
		}

		/// <summary>Gets a value indicating whether the user has enabled full window drag.</summary>
		/// <returns>true if the user has enabled full window drag; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000CC8 RID: 3272
		// (get) Token: 0x060031EC RID: 12780 RVA: 0x000BF028 File Offset: 0x000BD228
		public static bool DragFullWindows
		{
			get
			{
				return XplatUI.DragFullWindows;
			}
		}

		/// <summary>Gets the width and height of a rectangle centered on the point the mouse button was pressed, within which a drag operation will not begin.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that specifies the area of a rectangle, in pixels, centered on the point the mouse button was pressed, within which a drag operation will not begin.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000CC9 RID: 3273
		// (get) Token: 0x060031ED RID: 12781 RVA: 0x000BF030 File Offset: 0x000BD230
		public static Size DragSize
		{
			get
			{
				return XplatUI.DragSize;
			}
		}

		/// <summary>Gets the thickness, in pixels, of the frame border of a window that has a caption and is not resizable.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that specifies the thickness, in pixels, of a fixed sized window border.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000CCA RID: 3274
		// (get) Token: 0x060031EE RID: 12782 RVA: 0x000BF038 File Offset: 0x000BD238
		public static Size FixedFrameBorderSize
		{
			get
			{
				return ThemeEngine.Current.FixedFrameBorderSize;
			}
		}

		/// <summary>Gets the font smoothing contrast value used in ClearType smoothing.</summary>
		/// <returns>The ClearType font smoothing contrast value.</returns>
		/// <exception cref="T:System.NotSupportedException">The operating system does not support this feature.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000CCB RID: 3275
		// (get) Token: 0x060031EF RID: 12783 RVA: 0x000BF044 File Offset: 0x000BD244
		[MonoInternalNote("Determine if we need an X11 implementation or if defaults are good.")]
		public static int FontSmoothingContrast
		{
			get
			{
				return XplatUI.FontSmoothingContrast;
			}
		}

		/// <summary>Gets the current type of font smoothing.</summary>
		/// <returns>A value that indicates the current type of font smoothing.</returns>
		/// <exception cref="T:System.NotSupportedException">The operating system does not support this feature.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000CCC RID: 3276
		// (get) Token: 0x060031F0 RID: 12784 RVA: 0x000BF04C File Offset: 0x000BD24C
		[MonoInternalNote("Determine if we need an X11 implementation or if defaults are good.")]
		public static int FontSmoothingType
		{
			get
			{
				return XplatUI.FontSmoothingType;
			}
		}

		/// <summary>Gets the thickness, in pixels, of the resizing border that is drawn around the perimeter of a window that is being drag resized.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that indicates the thickness, in pixels, of the width of a vertical resizing border and the height of a horizontal resizing border.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000CCD RID: 3277
		// (get) Token: 0x060031F1 RID: 12785 RVA: 0x000BF054 File Offset: 0x000BD254
		public static Size FrameBorderSize
		{
			get
			{
				return ThemeEngine.Current.FrameBorderSize;
			}
		}

		/// <summary>Gets a value indicating whether the user has enabled the high-contrast mode accessibility feature.</summary>
		/// <returns>true if the user has enabled high-contrast mode; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000CCE RID: 3278
		// (get) Token: 0x060031F2 RID: 12786 RVA: 0x000BF060 File Offset: 0x000BD260
		public static bool HighContrast
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets the thickness of the left and right edges of the system focus rectangle, in pixels.</summary>
		/// <returns>The thickness of the left and right edges of the system focus rectangle, in pixels.</returns>
		/// <exception cref="T:System.NotSupportedException">The operating system does not support this feature.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000CCF RID: 3279
		// (get) Token: 0x060031F3 RID: 12787 RVA: 0x000BF064 File Offset: 0x000BD264
		[MonoInternalNote("Determine if we need an X11 implementation or if defaults are good.")]
		public static int HorizontalFocusThickness
		{
			get
			{
				return ThemeEngine.Current.HorizontalFocusThickness;
			}
		}

		/// <summary>Gets the thickness of the left and right edges of the sizing border around the perimeter of a window being resized, in pixels.</summary>
		/// <returns>The width of the left and right edges of the sizing border around the perimeter of a window being resized, in pixels.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000CD0 RID: 3280
		// (get) Token: 0x060031F4 RID: 12788 RVA: 0x000BF070 File Offset: 0x000BD270
		[MonoInternalNote("Determine if we need an X11 implementation or if defaults are good.")]
		public static int HorizontalResizeBorderThickness
		{
			get
			{
				return XplatUI.HorizontalResizeBorderThickness;
			}
		}

		/// <summary>Gets the width, in pixels, of the arrow bitmap on the horizontal scroll bar.</summary>
		/// <returns>The width, in pixels, of the arrow bitmap on the horizontal scroll bar.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000CD1 RID: 3281
		// (get) Token: 0x060031F5 RID: 12789 RVA: 0x000BF078 File Offset: 0x000BD278
		public static int HorizontalScrollBarArrowWidth
		{
			get
			{
				return ThemeEngine.Current.HorizontalScrollBarArrowWidth;
			}
		}

		/// <summary>Gets the default height, in pixels, of the horizontal scroll bar.</summary>
		/// <returns>The default height, in pixels, of the horizontal scroll bar.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000CD2 RID: 3282
		// (get) Token: 0x060031F6 RID: 12790 RVA: 0x000BF084 File Offset: 0x000BD284
		public static int HorizontalScrollBarHeight
		{
			get
			{
				return ThemeEngine.Current.HorizontalScrollBarHeight;
			}
		}

		/// <summary>Gets the width, in pixels, of the scroll box in a horizontal scroll bar.</summary>
		/// <returns>The width, in pixels, of the scroll box in a horizontal scroll bar.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000CD3 RID: 3283
		// (get) Token: 0x060031F7 RID: 12791 RVA: 0x000BF090 File Offset: 0x000BD290
		public static int HorizontalScrollBarThumbWidth
		{
			get
			{
				return ThemeEngine.Current.HorizontalScrollBarThumbWidth;
			}
		}

		/// <summary>Gets the dimensions, in pixels, of the Windows default program icon size.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that indicates the default dimensions, in pixels, for a program icon.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000CD4 RID: 3284
		// (get) Token: 0x060031F8 RID: 12792 RVA: 0x000BF09C File Offset: 0x000BD29C
		public static Size IconSize
		{
			get
			{
				return XplatUI.IconSize;
			}
		}

		/// <summary>Gets the width, in pixels, of an icon arrangement cell in large icon view.</summary>
		/// <returns>The width, in pixels, of an icon arrangement cell in large icon view.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000CD5 RID: 3285
		// (get) Token: 0x060031F9 RID: 12793 RVA: 0x000BF0A4 File Offset: 0x000BD2A4
		public static int IconHorizontalSpacing
		{
			get
			{
				return SystemInformation.IconSpacingSize.Width;
			}
		}

		/// <summary>Gets the height, in pixels, of an icon arrangement cell in large icon view.</summary>
		/// <returns>The height, in pixels, of an icon arrangement cell in large icon view.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000CD6 RID: 3286
		// (get) Token: 0x060031FA RID: 12794 RVA: 0x000BF0C0 File Offset: 0x000BD2C0
		public static int IconVerticalSpacing
		{
			get
			{
				return SystemInformation.IconSpacingSize.Height;
			}
		}

		/// <summary>Gets the size, in pixels, of the grid square used to arrange icons in a large-icon view.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that specifies the dimensions, in pixels, of the grid square used to arrange icons in a large-icon view.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000CD7 RID: 3287
		// (get) Token: 0x060031FB RID: 12795 RVA: 0x000BF0DC File Offset: 0x000BD2DC
		public static Size IconSpacingSize
		{
			get
			{
				return ThemeEngine.Current.IconSpacingSize;
			}
		}

		/// <summary>Gets a value indicating whether active window tracking is enabled.</summary>
		/// <returns>true if active window tracking is enabled; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000CD8 RID: 3288
		// (get) Token: 0x060031FC RID: 12796 RVA: 0x000BF0E8 File Offset: 0x000BD2E8
		[MonoInternalNote("Determine if we need an X11 implementation or if defaults are good.")]
		public static bool IsActiveWindowTrackingEnabled
		{
			get
			{
				return XplatUI.IsActiveWindowTrackingEnabled;
			}
		}

		/// <summary>Gets a value indicating whether the slide-open effect for combo boxes is enabled.</summary>
		/// <returns>true if the slide-open effect for combo boxes is enabled; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000CD9 RID: 3289
		// (get) Token: 0x060031FD RID: 12797 RVA: 0x000BF0F0 File Offset: 0x000BD2F0
		[MonoInternalNote("Determine if we need an X11 implementation or if defaults are good.")]
		public static bool IsComboBoxAnimationEnabled
		{
			get
			{
				return XplatUI.IsComboBoxAnimationEnabled;
			}
		}

		/// <summary>Gets a value indicating whether the drop shadow effect is enabled.</summary>
		/// <returns>true if the drop shadow effect is enabled; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000CDA RID: 3290
		// (get) Token: 0x060031FE RID: 12798 RVA: 0x000BF0F8 File Offset: 0x000BD2F8
		[MonoInternalNote("Determine if we need an X11 implementation or if defaults are good.")]
		public static bool IsDropShadowEnabled
		{
			get
			{
				return XplatUI.IsDropShadowEnabled;
			}
		}

		/// <summary>Gets a value indicating whether native user menus have a flat menu appearance. </summary>
		/// <returns>This property is not used and always returns false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000CDB RID: 3291
		// (get) Token: 0x060031FF RID: 12799 RVA: 0x000BF100 File Offset: 0x000BD300
		public static bool IsFlatMenuEnabled
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether font smoothing is enabled.</summary>
		/// <returns>true if the font smoothing feature is enabled; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000CDC RID: 3292
		// (get) Token: 0x06003200 RID: 12800 RVA: 0x000BF104 File Offset: 0x000BD304
		[MonoInternalNote("Determine if we need an X11 implementation or if defaults are good.")]
		public static bool IsFontSmoothingEnabled
		{
			get
			{
				return XplatUI.IsFontSmoothingEnabled;
			}
		}

		/// <summary>Gets a value indicating whether hot tracking of user-interface elements, such as menu names on menu bars, is enabled.</summary>
		/// <returns>true if hot tracking of user-interface elements is enabled; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000CDD RID: 3293
		// (get) Token: 0x06003201 RID: 12801 RVA: 0x000BF10C File Offset: 0x000BD30C
		[MonoInternalNote("Determine if we need an X11 implementation or if defaults are good.")]
		public static bool IsHotTrackingEnabled
		{
			get
			{
				return XplatUI.IsHotTrackingEnabled;
			}
		}

		/// <summary>Gets a value indicating whether icon-title wrapping is enabled.</summary>
		/// <returns>true if the icon-title wrapping feature is enabled; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000CDE RID: 3294
		// (get) Token: 0x06003202 RID: 12802 RVA: 0x000BF114 File Offset: 0x000BD314
		[MonoInternalNote("Determine if we need an X11 implementation or if defaults are good.")]
		public static bool IsIconTitleWrappingEnabled
		{
			get
			{
				return XplatUI.IsIconTitleWrappingEnabled;
			}
		}

		/// <summary>Gets a value indicating whether the user relies on the keyboard instead of the mouse, and prefers applications to display keyboard interfaces that would otherwise be hidden.</summary>
		/// <returns>true if keyboard preferred mode is enabled; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000CDF RID: 3295
		// (get) Token: 0x06003203 RID: 12803 RVA: 0x000BF11C File Offset: 0x000BD31C
		[MonoInternalNote("Determine if we need an X11 implementation or if defaults are good.")]
		public static bool IsKeyboardPreferred
		{
			get
			{
				return XplatUI.IsKeyboardPreferred;
			}
		}

		/// <summary>Gets a value indicating whether the smooth-scrolling effect for list boxes is enabled.</summary>
		/// <returns>true if smooth-scrolling is enabled; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000CE0 RID: 3296
		// (get) Token: 0x06003204 RID: 12804 RVA: 0x000BF124 File Offset: 0x000BD324
		[MonoInternalNote("Determine if we need an X11 implementation or if defaults are good.")]
		public static bool IsListBoxSmoothScrollingEnabled
		{
			get
			{
				return XplatUI.IsListBoxSmoothScrollingEnabled;
			}
		}

		/// <summary>Gets a value indicating whether menu fade or slide animation features are enabled.</summary>
		/// <returns>true if menu fade or slide animation is enabled; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000CE1 RID: 3297
		// (get) Token: 0x06003205 RID: 12805 RVA: 0x000BF12C File Offset: 0x000BD32C
		[MonoInternalNote("Determine if we need an X11 implementation or if defaults are good.")]
		public static bool IsMenuAnimationEnabled
		{
			get
			{
				return XplatUI.IsMenuAnimationEnabled;
			}
		}

		/// <summary>Gets a value indicating whether menu fade animation is enabled.</summary>
		/// <returns>true if fade animation is enabled; false if it is disabled.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000CE2 RID: 3298
		// (get) Token: 0x06003206 RID: 12806 RVA: 0x000BF134 File Offset: 0x000BD334
		[MonoInternalNote("Determine if we need an X11 implementation or if defaults are good.")]
		public static bool IsMenuFadeEnabled
		{
			get
			{
				return XplatUI.IsMenuFadeEnabled;
			}
		}

		/// <summary>Gets a value indicating whether window minimize and restore animation is enabled.</summary>
		/// <returns>true if window minimize and restore animation is enabled; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000CE3 RID: 3299
		// (get) Token: 0x06003207 RID: 12807 RVA: 0x000BF13C File Offset: 0x000BD33C
		[MonoInternalNote("Determine if we need an X11 implementation or if defaults are good.")]
		public static bool IsMinimizeRestoreAnimationEnabled
		{
			get
			{
				return XplatUI.IsMinimizeRestoreAnimationEnabled;
			}
		}

		/// <summary>Gets a value indicating whether the selection fade effect is enabled.</summary>
		/// <returns>true if the selection fade effect is enabled; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000CE4 RID: 3300
		// (get) Token: 0x06003208 RID: 12808 RVA: 0x000BF144 File Offset: 0x000BD344
		[MonoInternalNote("Determine if we need an X11 implementation or if defaults are good.")]
		public static bool IsSelectionFadeEnabled
		{
			get
			{
				return XplatUI.IsSelectionFadeEnabled;
			}
		}

		/// <summary>Gets a value indicating whether the snap-to-default-button feature is enabled.</summary>
		/// <returns>true if the snap-to-default-button feature is enabled; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000CE5 RID: 3301
		// (get) Token: 0x06003209 RID: 12809 RVA: 0x000BF14C File Offset: 0x000BD34C
		[MonoInternalNote("Determine if we need an X11 implementation or if defaults are good.")]
		public static bool IsSnapToDefaultEnabled
		{
			get
			{
				return XplatUI.IsSnapToDefaultEnabled;
			}
		}

		/// <summary>Gets a value indicating whether the gradient effect for window title bars is enabled.</summary>
		/// <returns>true if the gradient effect for window title bars is enabled; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000CE6 RID: 3302
		// (get) Token: 0x0600320A RID: 12810 RVA: 0x000BF154 File Offset: 0x000BD354
		[MonoInternalNote("Determine if we need an X11 implementation or if defaults are good.")]
		public static bool IsTitleBarGradientEnabled
		{
			get
			{
				return XplatUI.IsTitleBarGradientEnabled;
			}
		}

		/// <summary>Gets a value indicating whether <see cref="T:System.Windows.Forms.ToolTip" /> animation is enabled.</summary>
		/// <returns>true if <see cref="T:System.Windows.Forms.ToolTip" /> animation is enabled; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000CE7 RID: 3303
		// (get) Token: 0x0600320B RID: 12811 RVA: 0x000BF15C File Offset: 0x000BD35C
		[MonoInternalNote("Determine if we need an X11 implementation or if defaults are good.")]
		public static bool IsToolTipAnimationEnabled
		{
			get
			{
				return XplatUI.IsToolTipAnimationEnabled;
			}
		}

		/// <summary>Gets the height, in pixels, of the Kanji window at the bottom of the screen for double-byte character set (DBCS) versions of Windows.</summary>
		/// <returns>The height, in pixels, of the Kanji window.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000CE8 RID: 3304
		// (get) Token: 0x0600320C RID: 12812 RVA: 0x000BF164 File Offset: 0x000BD364
		public static int KanjiWindowHeight
		{
			get
			{
				return 0;
			}
		}

		/// <summary>Gets the keyboard repeat-delay setting.</summary>
		/// <returns>The keyboard repeat-delay setting, from 0 (approximately 250 millisecond delay) through 3 (approximately 1 second delay).</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000CE9 RID: 3305
		// (get) Token: 0x0600320D RID: 12813 RVA: 0x000BF168 File Offset: 0x000BD368
		public static int KeyboardDelay
		{
			get
			{
				return XplatUI.KeyboardDelay;
			}
		}

		/// <summary>Gets the keyboard repeat-speed setting.</summary>
		/// <returns>The keyboard repeat-speed setting, from 0 (approximately 2.5 repetitions per second) through 31 (approximately 30 repetitions per second).</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000CEA RID: 3306
		// (get) Token: 0x0600320E RID: 12814 RVA: 0x000BF170 File Offset: 0x000BD370
		public static int KeyboardSpeed
		{
			get
			{
				return XplatUI.KeyboardSpeed;
			}
		}

		/// <summary>Gets the default maximum dimensions, in pixels, of a window that has a caption and sizing borders.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that specifies the maximum dimensions, in pixels, to which a window can be sized.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000CEB RID: 3307
		// (get) Token: 0x0600320F RID: 12815 RVA: 0x000BF178 File Offset: 0x000BD378
		public static Size MaxWindowTrackSize
		{
			get
			{
				return XplatUI.MaxWindowTrackSize;
			}
		}

		/// <summary>Gets a value indicating whether menu access keys are always underlined.</summary>
		/// <returns>true if menu access keys are always underlined; false if they are underlined only when the menu is activated or receives focus.</returns>
		// Token: 0x17000CEC RID: 3308
		// (get) Token: 0x06003210 RID: 12816 RVA: 0x000BF180 File Offset: 0x000BD380
		public static bool MenuAccessKeysUnderlined
		{
			get
			{
				return ThemeEngine.Current.MenuAccessKeysUnderlined;
			}
		}

		/// <summary>Gets the default width, in pixels, for menu-bar buttons and the height, in pixels, of a menu bar.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that indicates the default width for menu-bar buttons, in pixels, and the height of a menu bar, in pixels.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000CED RID: 3309
		// (get) Token: 0x06003211 RID: 12817 RVA: 0x000BF18C File Offset: 0x000BD38C
		[MonoInternalNote("Determine if we need an X11 implementation or if defaults are good.")]
		public static Size MenuBarButtonSize
		{
			get
			{
				return ThemeEngine.Current.MenuBarButtonSize;
			}
		}

		/// <summary>Gets the default dimensions, in pixels, of menu-bar buttons.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that indicates the default dimensions, in pixels, of menu-bar buttons.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000CEE RID: 3310
		// (get) Token: 0x06003212 RID: 12818 RVA: 0x000BF198 File Offset: 0x000BD398
		public static Size MenuButtonSize
		{
			get
			{
				return ThemeEngine.Current.MenuButtonSize;
			}
		}

		/// <summary>Gets the dimensions, in pixels, of the default size of a menu check mark area.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that indicates the default size, in pixels, of a menu check mark area.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000CEF RID: 3311
		// (get) Token: 0x06003213 RID: 12819 RVA: 0x000BF1A4 File Offset: 0x000BD3A4
		public static Size MenuCheckSize
		{
			get
			{
				return ThemeEngine.Current.MenuCheckSize;
			}
		}

		/// <summary>Gets the font used to display text on menus.</summary>
		/// <returns>The <see cref="T:System.Drawing.Font" /> used to display text on menus.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000CF0 RID: 3312
		// (get) Token: 0x06003214 RID: 12820 RVA: 0x000BF1B0 File Offset: 0x000BD3B0
		public static Font MenuFont
		{
			get
			{
				return (Font)ThemeEngine.Current.MenuFont.Clone();
			}
		}

		/// <summary>Gets the height, in pixels, of one line of a menu.</summary>
		/// <returns>The height, in pixels, of one line of a menu.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000CF1 RID: 3313
		// (get) Token: 0x06003215 RID: 12821 RVA: 0x000BF1C8 File Offset: 0x000BD3C8
		public static int MenuHeight
		{
			get
			{
				return ThemeEngine.Current.MenuHeight;
			}
		}

		/// <summary>Gets the time, in milliseconds, that the system waits before displaying a cascaded shortcut menu when the mouse cursor is over a submenu item.</summary>
		/// <returns>The time, in milliseconds, that the system waits before displaying a cascaded shortcut menu when the mouse cursor is over a submenu item.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000CF2 RID: 3314
		// (get) Token: 0x06003216 RID: 12822 RVA: 0x000BF1D4 File Offset: 0x000BD3D4
		[MonoInternalNote("Determine if we need an X11 implementation or if defaults are good.")]
		public static int MenuShowDelay
		{
			get
			{
				return XplatUI.MenuShowDelay;
			}
		}

		/// <summary>Gets a value indicating whether the operating system is enabled for the Hebrew and Arabic languages.</summary>
		/// <returns>true if the operating system is enabled for Hebrew or Arabic; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000CF3 RID: 3315
		// (get) Token: 0x06003217 RID: 12823 RVA: 0x000BF1DC File Offset: 0x000BD3DC
		public static bool MidEastEnabled
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets the dimensions, in pixels, of a normal minimized window.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that indicates the dimensions, in pixels, of a normal minimized window.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000CF4 RID: 3316
		// (get) Token: 0x06003218 RID: 12824 RVA: 0x000BF1E0 File Offset: 0x000BD3E0
		public static Size MinimizedWindowSize
		{
			get
			{
				return XplatUI.MinimizedWindowSize;
			}
		}

		/// <summary>Gets the dimensions, in pixels, of the area each minimized window is allocated when arranged.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that indicates the area each minimized window is allocated when arranged.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000CF5 RID: 3317
		// (get) Token: 0x06003219 RID: 12825 RVA: 0x000BF1E8 File Offset: 0x000BD3E8
		public static Size MinimizedWindowSpacingSize
		{
			get
			{
				return XplatUI.MinimizedWindowSpacingSize;
			}
		}

		/// <summary>Gets the minimum width and height for a window, in pixels.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that specifies the minimum allowable dimensions of a window, in pixels.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000CF6 RID: 3318
		// (get) Token: 0x0600321A RID: 12826 RVA: 0x000BF1F0 File Offset: 0x000BD3F0
		public static Size MinimumWindowSize
		{
			get
			{
				return XplatUI.MinimumWindowSize;
			}
		}

		/// <summary>Gets the default minimum dimensions, in pixels, that a window may occupy during a drag resize.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that indicates the default minimum width and height of a window during resize, in pixels.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000CF7 RID: 3319
		// (get) Token: 0x0600321B RID: 12827 RVA: 0x000BF1F8 File Offset: 0x000BD3F8
		public static Size MinWindowTrackSize
		{
			get
			{
				return XplatUI.MinWindowTrackSize;
			}
		}

		/// <summary>Gets the number of display monitors on the desktop.</summary>
		/// <returns>The number of monitors that make up the desktop.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000CF8 RID: 3320
		// (get) Token: 0x0600321C RID: 12828 RVA: 0x000BF200 File Offset: 0x000BD400
		public static int MonitorCount
		{
			get
			{
				return 1;
			}
		}

		/// <summary>Gets a value indicating whether all the display monitors are using the same pixel color format.</summary>
		/// <returns>true if all monitors are using the same pixel color format; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000CF9 RID: 3321
		// (get) Token: 0x0600321D RID: 12829 RVA: 0x000BF204 File Offset: 0x000BD404
		public static bool MonitorsSameDisplayFormat
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets the number of buttons on the mouse.</summary>
		/// <returns>The number of buttons on the mouse, or zero if no mouse is installed.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000CFA RID: 3322
		// (get) Token: 0x0600321E RID: 12830 RVA: 0x000BF208 File Offset: 0x000BD408
		public static int MouseButtons
		{
			get
			{
				return XplatUI.MouseButtonCount;
			}
		}

		/// <summary>Gets a value indicating whether the functions of the left and right mouse buttons have been swapped.</summary>
		/// <returns>true if the functions of the left and right mouse buttons are swapped; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000CFB RID: 3323
		// (get) Token: 0x0600321F RID: 12831 RVA: 0x000BF210 File Offset: 0x000BD410
		public static bool MouseButtonsSwapped
		{
			get
			{
				return XplatUI.MouseButtonsSwapped;
			}
		}

		/// <summary>Gets the dimensions, in pixels, of the rectangle within which the mouse pointer has to stay for the mouse hover time before a mouse hover message is generated.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that indicates the dimensions, in pixels, of the rectangle within which the mouse pointer has to stay for the mouse hover time before a mouse hover message is generated.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000CFC RID: 3324
		// (get) Token: 0x06003220 RID: 12832 RVA: 0x000BF218 File Offset: 0x000BD418
		public static Size MouseHoverSize
		{
			get
			{
				return XplatUI.MouseHoverSize;
			}
		}

		/// <summary>Gets the time, in milliseconds, that the mouse pointer has to stay in the hover rectangle before a mouse hover message is generated.</summary>
		/// <returns>The time, in milliseconds, that the mouse pointer has to stay in the hover rectangle before a mouse hover message is generated.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000CFD RID: 3325
		// (get) Token: 0x06003221 RID: 12833 RVA: 0x000BF220 File Offset: 0x000BD420
		public static int MouseHoverTime
		{
			get
			{
				return XplatUI.MouseHoverTime;
			}
		}

		/// <summary>Gets the current mouse speed.</summary>
		/// <returns>A mouse speed value between 1 (slowest) and 20 (fastest).</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000CFE RID: 3326
		// (get) Token: 0x06003222 RID: 12834 RVA: 0x000BF228 File Offset: 0x000BD428
		[MonoInternalNote("Determine if we need an X11 implementation or if defaults are good.")]
		public static int MouseSpeed
		{
			get
			{
				return XplatUI.MouseSpeed;
			}
		}

		/// <summary>Gets the amount of the delta value of a single mouse wheel rotation increment.</summary>
		/// <returns>The amount of the delta value of a single mouse wheel rotation increment.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000CFF RID: 3327
		// (get) Token: 0x06003223 RID: 12835 RVA: 0x000BF230 File Offset: 0x000BD430
		public static int MouseWheelScrollDelta
		{
			get
			{
				return XplatUI.MouseWheelScrollDelta;
			}
		}

		/// <summary>Gets a value indicating whether a pointing device is installed.</summary>
		/// <returns>true if a mouse is installed; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000D00 RID: 3328
		// (get) Token: 0x06003224 RID: 12836 RVA: 0x000BF238 File Offset: 0x000BD438
		[EditorBrowsable(1)]
		public static bool MousePresent
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets a value indicating whether a mouse with a mouse wheel is installed.</summary>
		/// <returns>true if a mouse with a mouse wheel is installed; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000D01 RID: 3329
		// (get) Token: 0x06003225 RID: 12837 RVA: 0x000BF23C File Offset: 0x000BD43C
		public static bool MouseWheelPresent
		{
			get
			{
				return XplatUI.MouseWheelPresent;
			}
		}

		/// <summary>Gets the number of lines to scroll when the mouse wheel is rotated.</summary>
		/// <returns>The number of lines to scroll on a mouse wheel rotation, or -1 if the "One screen at a time" mouse option is selected.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000D02 RID: 3330
		// (get) Token: 0x06003226 RID: 12838 RVA: 0x000BF244 File Offset: 0x000BD444
		public static int MouseWheelScrollLines
		{
			get
			{
				return ThemeEngine.Current.MouseWheelScrollLines;
			}
		}

		/// <summary>Gets a value indicating whether the operating system natively supports a mouse wheel.</summary>
		/// <returns>true if the operating system natively supports a mouse wheel; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000D03 RID: 3331
		// (get) Token: 0x06003227 RID: 12839 RVA: 0x000BF250 File Offset: 0x000BD450
		public static bool NativeMouseWheelSupport
		{
			get
			{
				return SystemInformation.MouseWheelPresent;
			}
		}

		/// <summary>Gets a value indicating whether a network connection is present.</summary>
		/// <returns>true if a network connection is present; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000D04 RID: 3332
		// (get) Token: 0x06003228 RID: 12840 RVA: 0x000BF258 File Offset: 0x000BD458
		public static bool Network
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets a value indicating whether the Microsoft Windows for Pen Computing extensions are installed.</summary>
		/// <returns>true if the Windows for Pen Computing extensions are installed; false if Windows for Pen Computing extensions are not installed.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000D05 RID: 3333
		// (get) Token: 0x06003229 RID: 12841 RVA: 0x000BF25C File Offset: 0x000BD45C
		public static bool PenWindows
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets the side of pop-up menus that are aligned to the corresponding menu-bar item.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.LeftRightAlignment" /> that indicates whether pop-up menus are left-aligned or right-aligned, relative to the corresponding menu-bar item.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000D06 RID: 3334
		// (get) Token: 0x0600322A RID: 12842 RVA: 0x000BF260 File Offset: 0x000BD460
		[MonoInternalNote("Determine if we need an X11 implementation or if defaults are good.")]
		public static LeftRightAlignment PopupMenuAlignment
		{
			get
			{
				return XplatUI.PopupMenuAlignment;
			}
		}

		/// <summary>Gets the current system power status.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.PowerStatus" /> that indicates the current system power status.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000D07 RID: 3335
		// (get) Token: 0x0600322B RID: 12843 RVA: 0x000BF268 File Offset: 0x000BD468
		[MonoTODO("Only implemented for Win32.")]
		public static PowerStatus PowerStatus
		{
			get
			{
				return XplatUI.PowerStatus;
			}
		}

		/// <summary>Gets the default dimensions, in pixels, of a maximized window on the primary display.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that specifies the dimensions, in pixels, of a maximized window on the primary display.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000D08 RID: 3336
		// (get) Token: 0x0600322C RID: 12844 RVA: 0x000BF270 File Offset: 0x000BD470
		public static Size PrimaryMonitorMaximizedWindowSize
		{
			get
			{
				return new Size(SystemInformation.WorkingArea.Width, SystemInformation.WorkingArea.Height);
			}
		}

		/// <summary>Gets the dimensions, in pixels, of the current video mode of the primary display.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that indicates the dimensions, in pixels, of the current video mode of the primary display.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000D09 RID: 3337
		// (get) Token: 0x0600322D RID: 12845 RVA: 0x000BF29C File Offset: 0x000BD49C
		public static Size PrimaryMonitorSize
		{
			get
			{
				return new Size(SystemInformation.WorkingArea.Width, SystemInformation.WorkingArea.Height);
			}
		}

		/// <summary>Gets a value indicating whether drop-down menus are right-aligned with the corresponding menu-bar item.</summary>
		/// <returns>true if drop-down menus are right-aligned with the corresponding menu-bar item; false if the menus are left-aligned.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000D0A RID: 3338
		// (get) Token: 0x0600322E RID: 12846 RVA: 0x000BF2C8 File Offset: 0x000BD4C8
		public static bool RightAlignedMenus
		{
			get
			{
				return ThemeEngine.Current.RightAlignedMenus;
			}
		}

		/// <summary>Gets the orientation of the screen.</summary>
		/// <returns>The orientation of the screen, in degrees.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000D0B RID: 3339
		// (get) Token: 0x0600322F RID: 12847 RVA: 0x000BF2D4 File Offset: 0x000BD4D4
		public static ScreenOrientation ScreenOrientation
		{
			get
			{
				return ScreenOrientation.Angle0;
			}
		}

		/// <summary>Gets a value indicating whether a Security Manager is present on this operating system.</summary>
		/// <returns>true if a Security Manager is present; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000D0C RID: 3340
		// (get) Token: 0x06003230 RID: 12848 RVA: 0x000BF2D8 File Offset: 0x000BD4D8
		public static bool Secure
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets a value indicating whether the user prefers that an application present information in visual form in situations when it would present the information in audible form.</summary>
		/// <returns>true if the application should visually show information about audible output; false if the application does not need to provide extra visual cues for audio events.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000D0D RID: 3341
		// (get) Token: 0x06003231 RID: 12849 RVA: 0x000BF2DC File Offset: 0x000BD4DC
		public static bool ShowSounds
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets the width, in pixels, of the sizing border drawn around the perimeter of a window being resized.</summary>
		/// <returns>The width, in pixels, of the window sizing border drawn around the perimeter of a window being resized.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000D0E RID: 3342
		// (get) Token: 0x06003232 RID: 12850 RVA: 0x000BF2E0 File Offset: 0x000BD4E0
		[MonoInternalNote("Determine if we need an X11 implementation or if defaults are good.")]
		public static int SizingBorderWidth
		{
			get
			{
				return XplatUI.SizingBorderWidth;
			}
		}

		/// <summary>Gets the width, in pixels, of small caption buttons, and the height, in pixels, of small captions.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that indicates the width, in pixels, of small caption buttons, and the height, in pixels, of small captions.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000D0F RID: 3343
		// (get) Token: 0x06003233 RID: 12851 RVA: 0x000BF2E8 File Offset: 0x000BD4E8
		[MonoInternalNote("Determine if we need an X11 implementation or if defaults are good.")]
		public static Size SmallCaptionButtonSize
		{
			get
			{
				return XplatUI.SmallCaptionButtonSize;
			}
		}

		/// <summary>Gets the dimensions, in pixels, of a small icon.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that specifies the dimensions, in pixels, of a small icon.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000D10 RID: 3344
		// (get) Token: 0x06003234 RID: 12852 RVA: 0x000BF2F0 File Offset: 0x000BD4F0
		public static Size SmallIconSize
		{
			get
			{
				return XplatUI.SmallIconSize;
			}
		}

		/// <summary>Gets a value indicating whether the calling process is associated with a Terminal Services client session.</summary>
		/// <returns>true if the calling process is associated with a Terminal Services client session; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000D11 RID: 3345
		// (get) Token: 0x06003235 RID: 12853 RVA: 0x000BF2F8 File Offset: 0x000BD4F8
		public static bool TerminalServerSession
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets the dimensions, in pixels, of small caption buttons.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that indicates the dimensions, in pixels, of small caption buttons.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000D12 RID: 3346
		// (get) Token: 0x06003236 RID: 12854 RVA: 0x000BF2FC File Offset: 0x000BD4FC
		public static Size ToolWindowCaptionButtonSize
		{
			get
			{
				return ThemeEngine.Current.ToolWindowCaptionButtonSize;
			}
		}

		/// <summary>Gets the height, in pixels, of a tool window caption.</summary>
		/// <returns>The height, in pixels, of a tool window caption in pixels.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000D13 RID: 3347
		// (get) Token: 0x06003237 RID: 12855 RVA: 0x000BF308 File Offset: 0x000BD508
		public static int ToolWindowCaptionHeight
		{
			get
			{
				return ThemeEngine.Current.ToolWindowCaptionHeight;
			}
		}

		/// <summary>Gets a value indicating whether user interface (UI) effects are enabled or disabled.</summary>
		/// <returns>true if UI effects are enabled; otherwise, false.</returns>
		// Token: 0x17000D14 RID: 3348
		// (get) Token: 0x06003238 RID: 12856 RVA: 0x000BF314 File Offset: 0x000BD514
		[MonoInternalNote("Determine if we need an X11 implementation or if defaults are good.")]
		public static bool UIEffectsEnabled
		{
			get
			{
				return XplatUI.UIEffectsEnabled;
			}
		}

		/// <summary>Gets the name of the domain the user belongs to.</summary>
		/// <returns>The name of the user domain. If a local user account exists with the same name as the user name, this property gets the computer name.</returns>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The operating system does not support this feature.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000D15 RID: 3349
		// (get) Token: 0x06003239 RID: 12857 RVA: 0x000BF31C File Offset: 0x000BD51C
		public static string UserDomainName
		{
			get
			{
				return Environment.UserDomainName;
			}
		}

		/// <summary>Gets a value indicating whether the current process is running in user-interactive mode.</summary>
		/// <returns>true if the current process is running in user-interactive mode; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000D16 RID: 3350
		// (get) Token: 0x0600323A RID: 12858 RVA: 0x000BF324 File Offset: 0x000BD524
		public static bool UserInteractive
		{
			get
			{
				return Environment.UserInteractive;
			}
		}

		/// <summary>Gets the user name associated with the current thread.</summary>
		/// <returns>The user name of the user associated with the current thread.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000D17 RID: 3351
		// (get) Token: 0x0600323B RID: 12859 RVA: 0x000BF32C File Offset: 0x000BD52C
		public static string UserName
		{
			get
			{
				return Environment.UserName;
			}
		}

		/// <summary>Gets the thickness, in pixels, of the top and bottom edges of the system focus rectangle.</summary>
		/// <returns>The thickness, in pixels, of the top and bottom edges of the system focus rectangle.</returns>
		/// <exception cref="T:System.NotSupportedException">The operating system does not support this feature.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000D18 RID: 3352
		// (get) Token: 0x0600323C RID: 12860 RVA: 0x000BF334 File Offset: 0x000BD534
		[MonoInternalNote("Determine if we need an X11 implementation or if defaults are good.")]
		public static int VerticalFocusThickness
		{
			get
			{
				return ThemeEngine.Current.VerticalFocusThickness;
			}
		}

		/// <summary>Gets the thickness, in pixels, of the top and bottom edges of the sizing border around the perimeter of a window being resized.</summary>
		/// <returns>The height, in pixels, of the top and bottom edges of the sizing border around the perimeter of a window being resized, in pixels.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000D19 RID: 3353
		// (get) Token: 0x0600323D RID: 12861 RVA: 0x000BF340 File Offset: 0x000BD540
		[MonoInternalNote("Determine if we need an X11 implementation or if defaults are good.")]
		public static int VerticalResizeBorderThickness
		{
			get
			{
				return XplatUI.VerticalResizeBorderThickness;
			}
		}

		/// <summary>Gets the height, in pixels, of the arrow bitmap on the vertical scroll bar.</summary>
		/// <returns>The height, in pixels, of the arrow bitmap on the vertical scroll bar.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000D1A RID: 3354
		// (get) Token: 0x0600323E RID: 12862 RVA: 0x000BF348 File Offset: 0x000BD548
		public static int VerticalScrollBarArrowHeight
		{
			get
			{
				return ThemeEngine.Current.VerticalScrollBarArrowHeight;
			}
		}

		/// <summary>Gets the height, in pixels, of the scroll box in a vertical scroll bar.</summary>
		/// <returns>The height, in pixels, of the scroll box in a vertical scroll bar.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000D1B RID: 3355
		// (get) Token: 0x0600323F RID: 12863 RVA: 0x000BF354 File Offset: 0x000BD554
		public static int VerticalScrollBarThumbHeight
		{
			get
			{
				return ThemeEngine.Current.VerticalScrollBarThumbHeight;
			}
		}

		/// <summary>Gets the default width, in pixels, of the vertical scroll bar.</summary>
		/// <returns>The default width, in pixels, of the vertical scroll bar.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000D1C RID: 3356
		// (get) Token: 0x06003240 RID: 12864 RVA: 0x000BF360 File Offset: 0x000BD560
		public static int VerticalScrollBarWidth
		{
			get
			{
				return ThemeEngine.Current.VerticalScrollBarWidth;
			}
		}

		/// <summary>Gets the bounds of the virtual screen.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that specifies the bounding rectangle of the entire virtual screen.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000D1D RID: 3357
		// (get) Token: 0x06003241 RID: 12865 RVA: 0x000BF36C File Offset: 0x000BD56C
		public static Rectangle VirtualScreen
		{
			get
			{
				return XplatUI.VirtualScreen;
			}
		}

		/// <summary>Gets the size, in pixels, of the working area of the screen.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the size, in pixels, of the working area of the screen.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000D1E RID: 3358
		// (get) Token: 0x06003242 RID: 12866 RVA: 0x000BF374 File Offset: 0x000BD574
		public static Rectangle WorkingArea
		{
			get
			{
				return XplatUI.WorkingArea;
			}
		}
	}
}
