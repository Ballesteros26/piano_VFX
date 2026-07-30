using System;
using System.Drawing;

namespace System.Windows.Forms
{
	/// <summary>Provides methods used to paint common Windows controls and their elements. This class cannot be inherited.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000AE RID: 174
	public sealed class ControlPaint
	{
		// Token: 0x06000ABC RID: 2748 RVA: 0x0002C534 File Offset: 0x0002A734
		private ControlPaint()
		{
		}

		// Token: 0x06000ABE RID: 2750 RVA: 0x0002C554 File Offset: 0x0002A754
		internal static void Color2HBS(Color color, out int h, out int l, out int s)
		{
			int r = (int)color.R;
			int g = (int)color.G;
			int b = (int)color.B;
			int num = Math.Max(Math.Max(r, g), b);
			int num2 = Math.Min(Math.Min(r, g), b);
			l = ((num + num2) * ControlPaint.HLSMax + ControlPaint.RGBMax) / (2 * ControlPaint.RGBMax);
			if (num == num2)
			{
				h = 0;
				s = 0;
				return;
			}
			if (l <= ControlPaint.HLSMax / 2)
			{
				s = ((num - num2) * ControlPaint.HLSMax + (num + num2) / 2) / (num + num2);
			}
			else
			{
				s = ((num - num2) * ControlPaint.HLSMax + (2 * ControlPaint.RGBMax - num - num2) / 2) / (2 * ControlPaint.RGBMax - num - num2);
			}
			int num3 = ((num - r) * (ControlPaint.HLSMax / 6) + (num - num2) / 2) / (num - num2);
			int num4 = ((num - g) * (ControlPaint.HLSMax / 6) + (num - num2) / 2) / (num - num2);
			int num5 = ((num - b) * (ControlPaint.HLSMax / 6) + (num - num2) / 2) / (num - num2);
			if (r == num)
			{
				h = num5 - num4;
			}
			else if (g == num)
			{
				h = ControlPaint.HLSMax / 3 + num3 - num5;
			}
			else
			{
				h = 2 * ControlPaint.HLSMax / 3 + num4 - num3;
			}
			if (h < 0)
			{
				h += ControlPaint.HLSMax;
			}
			if (h > ControlPaint.HLSMax)
			{
				h -= ControlPaint.HLSMax;
			}
		}

		// Token: 0x06000ABF RID: 2751 RVA: 0x0002C6C4 File Offset: 0x0002A8C4
		private static int HueToRGB(int n1, int n2, int hue)
		{
			if (hue < 0)
			{
				hue += ControlPaint.HLSMax;
			}
			if (hue > ControlPaint.HLSMax)
			{
				hue -= ControlPaint.HLSMax;
			}
			if (hue < ControlPaint.HLSMax / 6)
			{
				return n1 + ((n2 - n1) * hue + ControlPaint.HLSMax / 12) / (ControlPaint.HLSMax / 6);
			}
			if (hue < ControlPaint.HLSMax / 2)
			{
				return n2;
			}
			if (hue < ControlPaint.HLSMax * 2 / 3)
			{
				return n1 + ((n2 - n1) * (ControlPaint.HLSMax * 2 / 3 - hue) + ControlPaint.HLSMax / 12) / (ControlPaint.HLSMax / 6);
			}
			return n1;
		}

		// Token: 0x06000AC0 RID: 2752 RVA: 0x0002C760 File Offset: 0x0002A960
		internal static Color HBS2Color(int hue, int lum, int sat)
		{
			int num3;
			int num2;
			int num;
			if (sat == 0)
			{
				num = (num2 = (num3 = lum * ControlPaint.RGBMax / ControlPaint.HLSMax));
			}
			else
			{
				int num4;
				if (lum <= ControlPaint.HLSMax / 2)
				{
					num4 = (lum * (ControlPaint.HLSMax + sat) + ControlPaint.HLSMax / 2) / ControlPaint.HLSMax;
				}
				else
				{
					num4 = sat + lum - (sat * lum + ControlPaint.HLSMax / 2) / ControlPaint.HLSMax;
				}
				int num5 = 2 * lum - num4;
				num2 = Math.Min(255, (ControlPaint.HueToRGB(num5, num4, hue + ControlPaint.HLSMax / 3) * ControlPaint.RGBMax + ControlPaint.HLSMax / 2) / ControlPaint.HLSMax);
				num = Math.Min(255, (ControlPaint.HueToRGB(num5, num4, hue) * ControlPaint.RGBMax + ControlPaint.HLSMax / 2) / ControlPaint.HLSMax);
				num3 = Math.Min(255, (ControlPaint.HueToRGB(num5, num4, hue - ControlPaint.HLSMax / 3) * ControlPaint.RGBMax + ControlPaint.HLSMax / 2) / ControlPaint.HLSMax);
			}
			return Color.FromArgb(num2, num, num3);
		}

		/// <summary>Gets the color to use as the <see cref="P:System.Drawing.SystemColors.ControlDark" /> color.</summary>
		/// <returns>The <see cref="T:System.Drawing.Color" /> to use as the <see cref="P:System.Drawing.SystemColors.ControlDark" /> color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000257 RID: 599
		// (get) Token: 0x06000AC1 RID: 2753 RVA: 0x0002C864 File Offset: 0x0002AA64
		public static Color ContrastControlDark
		{
			get
			{
				return SystemColors.ControlDark;
			}
		}

		/// <summary>Creates a 16-bit color bitmap.</summary>
		/// <returns>An <see cref="T:System.IntPtr" /> representing the handle to the bitmap.</returns>
		/// <param name="bitmap">The <see cref="T:System.Drawing.Bitmap" /> to create.</param>
		/// <param name="background">The <see cref="T:System.Drawing.Color" /> of the background.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06000AC2 RID: 2754 RVA: 0x0002C86C File Offset: 0x0002AA6C
		[MonoTODO("Not implemented, will throw NotImplementedException")]
		public static IntPtr CreateHBitmap16Bit(Bitmap bitmap, Color background)
		{
			throw new NotImplementedException();
		}

		/// <summary>Creates a Win32 HBITMAP out of the image. </summary>
		/// <returns>An <see cref="T:System.IntPtr" /> representing the handle to the bitmap.</returns>
		/// <param name="bitmap">The <see cref="T:System.Drawing.Bitmap" /> to create.</param>
		/// <param name="monochromeMask">A pointer to the monochrome mask.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06000AC3 RID: 2755 RVA: 0x0002C874 File Offset: 0x0002AA74
		[MonoTODO("Not implemented, will throw NotImplementedException")]
		public static IntPtr CreateHBitmapColorMask(Bitmap bitmap, IntPtr monochromeMask)
		{
			throw new NotImplementedException();
		}

		/// <summary>Creates a color mask for the specified bitmap that indicates which color should be displayed as transparent.</summary>
		/// <returns>The handle to the <see cref="T:System.Drawing.Bitmap" /> mask.</returns>
		/// <param name="bitmap">The <see cref="T:System.Drawing.Bitmap" /> to create the transparency mask for. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06000AC4 RID: 2756 RVA: 0x0002C87C File Offset: 0x0002AA7C
		[MonoTODO("Not implemented, will throw NotImplementedException")]
		public static IntPtr CreateHBitmapTransparencyMask(Bitmap bitmap)
		{
			throw new NotImplementedException();
		}

		/// <summary>Creates a new light color object for the control from the specified color.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the light color on the control.</returns>
		/// <param name="baseColor">The <see cref="T:System.Drawing.Color" /> to be lightened. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000AC5 RID: 2757 RVA: 0x0002C884 File Offset: 0x0002AA84
		public static Color Light(Color baseColor)
		{
			return ControlPaint.Light(baseColor, 0.5f);
		}

		/// <summary>Creates a new light color object for the control from the specified color and lightens it by the specified percentage.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the light color on the control.</returns>
		/// <param name="baseColor">The <see cref="T:System.Drawing.Color" /> to be lightened. </param>
		/// <param name="percOfLightLight">The percentage to lighten the specified <see cref="T:System.Drawing.Color" />. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000AC6 RID: 2758 RVA: 0x0002C894 File Offset: 0x0002AA94
		public static Color Light(Color baseColor, float percOfLightLight)
		{
			if (baseColor.ToArgb() != ThemeEngine.Current.ColorControl.ToArgb())
			{
				int num;
				int num2;
				int num3;
				ControlPaint.Color2HBS(baseColor, out num, out num2, out num3);
				int num4 = Math.Min(255, num2 + (int)((float)(255 - num2) * 0.5f * percOfLightLight));
				return ControlPaint.HBS2Color(num, num4, num3);
			}
			if (percOfLightLight <= 0f)
			{
				return ThemeEngine.Current.ColorControlLight;
			}
			if (percOfLightLight == 1f)
			{
				return ThemeEngine.Current.ColorControlLightLight;
			}
			int num5 = (int)(ThemeEngine.Current.ColorControlLightLight.R - ThemeEngine.Current.ColorControlLight.R);
			int num6 = (int)(ThemeEngine.Current.ColorControlLightLight.G - ThemeEngine.Current.ColorControlLight.G);
			int num7 = (int)(ThemeEngine.Current.ColorControlLightLight.B - ThemeEngine.Current.ColorControlLight.B);
			return Color.FromArgb((int)ThemeEngine.Current.ColorControlLight.A, (int)((float)ThemeEngine.Current.ColorControlLight.R + (float)num5 * percOfLightLight), (int)((float)ThemeEngine.Current.ColorControlLight.G + (float)num6 * percOfLightLight), (int)((float)ThemeEngine.Current.ColorControlLight.B + (float)num7 * percOfLightLight));
		}

		/// <summary>Creates a new light color object for the control from the specified color.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the light color on the control.</returns>
		/// <param name="baseColor">The <see cref="T:System.Drawing.Color" /> to be lightened. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000AC7 RID: 2759 RVA: 0x0002CA08 File Offset: 0x0002AC08
		public static Color LightLight(Color baseColor)
		{
			return ControlPaint.Light(baseColor, 1f);
		}

		/// <summary>Creates a new dark color object for the control from the specified color.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the dark color on the control.</returns>
		/// <param name="baseColor">The <see cref="T:System.Drawing.Color" /> to be darkened. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000AC8 RID: 2760 RVA: 0x0002CA18 File Offset: 0x0002AC18
		public static Color Dark(Color baseColor)
		{
			return ControlPaint.Dark(baseColor, 0.5f);
		}

		/// <summary>Creates a new dark color object for the control from the specified color and darkens it by the specified percentage.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represent the dark color on the control.</returns>
		/// <param name="baseColor">The <see cref="T:System.Drawing.Color" /> to be darkened. </param>
		/// <param name="percOfDarkDark">The percentage to darken the specified <see cref="T:System.Drawing.Color" />. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000AC9 RID: 2761 RVA: 0x0002CA28 File Offset: 0x0002AC28
		public static Color Dark(Color baseColor, float percOfDarkDark)
		{
			if (baseColor.ToArgb() != ThemeEngine.Current.ColorControl.ToArgb())
			{
				int num;
				int num2;
				int num3;
				ControlPaint.Color2HBS(baseColor, out num, out num2, out num3);
				int num4 = Math.Max(0, num2 - (int)((float)num2 * 0.333f));
				int num5 = Math.Max(0, num4 - (int)((float)num4 * percOfDarkDark));
				return ControlPaint.HBS2Color(num, num5, num3);
			}
			if (percOfDarkDark <= 0f)
			{
				return ThemeEngine.Current.ColorControlDark;
			}
			if (percOfDarkDark == 1f)
			{
				return ThemeEngine.Current.ColorControlDarkDark;
			}
			int num6 = (int)(ThemeEngine.Current.ColorControlDarkDark.R - ThemeEngine.Current.ColorControlDark.R);
			int num7 = (int)(ThemeEngine.Current.ColorControlDarkDark.G - ThemeEngine.Current.ColorControlDark.G);
			int num8 = (int)(ThemeEngine.Current.ColorControlDarkDark.B - ThemeEngine.Current.ColorControlDark.B);
			return Color.FromArgb((int)ThemeEngine.Current.ColorControlDark.A, (int)((float)ThemeEngine.Current.ColorControlDark.R + (float)num6 * percOfDarkDark), (int)((float)ThemeEngine.Current.ColorControlDark.G + (float)num7 * percOfDarkDark), (int)((float)ThemeEngine.Current.ColorControlDark.B + (float)num8 * percOfDarkDark));
		}

		/// <summary>Creates a new dark color object for the control from the specified color.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the dark color on the control.</returns>
		/// <param name="baseColor">The <see cref="T:System.Drawing.Color" /> to be darkened. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000ACA RID: 2762 RVA: 0x0002CBA0 File Offset: 0x0002ADA0
		public static Color DarkDark(Color baseColor)
		{
			return ControlPaint.Dark(baseColor, 1f);
		}

		/// <summary>Draws a border with the specified style and color, on the specified graphics surface, and within the specified bounds on a button-style control.</summary>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> to draw on. </param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that represents the dimensions of the border. </param>
		/// <param name="color">The <see cref="T:System.Drawing.Color" /> of the border. </param>
		/// <param name="style">One of the <see cref="T:System.Windows.Forms.ButtonBorderStyle" /> values that specifies the style of the border. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000ACB RID: 2763 RVA: 0x0002CBB0 File Offset: 0x0002ADB0
		public static void DrawBorder(Graphics graphics, Rectangle bounds, Color color, ButtonBorderStyle style)
		{
			int num = 1;
			int num2 = 1;
			if (style == ButtonBorderStyle.Inset)
			{
				num = 2;
			}
			if (style == ButtonBorderStyle.Outset)
			{
				num2 = 2;
				num = 2;
			}
			ControlPaint.DrawBorder(graphics, bounds, color, num, style, color, num, style, color, num2, style, color, num2, style);
		}

		// Token: 0x06000ACC RID: 2764 RVA: 0x0002CBE8 File Offset: 0x0002ADE8
		internal static void DrawBorder(Graphics graphics, RectangleF bounds, Color color, ButtonBorderStyle style)
		{
			int num = 1;
			int num2 = 1;
			if (style == ButtonBorderStyle.Inset)
			{
				num = 2;
			}
			if (style == ButtonBorderStyle.Outset)
			{
				num2 = 2;
				num = 2;
			}
			ThemeEngine.Current.CPDrawBorder(graphics, bounds, color, num, style, color, num, style, color, num2, style, color, num2, style);
		}

		/// <summary>Draws a border on a button-style control with the specified styles, colors, and border widths; on the specified graphics surface; and within the specified bounds.</summary>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> to draw on. </param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that represents the dimensions of the border. </param>
		/// <param name="leftColor">The <see cref="T:System.Drawing.Color" /> of the left of the border. </param>
		/// <param name="leftWidth">The width of the left border. </param>
		/// <param name="leftStyle">One of the <see cref="T:System.Windows.Forms.ButtonBorderStyle" /> values that specifies the style of the left border. </param>
		/// <param name="topColor">The <see cref="T:System.Drawing.Color" /> of the top of the border. </param>
		/// <param name="topWidth">The width of the top border. </param>
		/// <param name="topStyle">One of the <see cref="T:System.Windows.Forms.ButtonBorderStyle" /> values that specifies the style of the top border. </param>
		/// <param name="rightColor">The <see cref="T:System.Drawing.Color" /> of the right of the border. </param>
		/// <param name="rightWidth">The width of the right border. </param>
		/// <param name="rightStyle">One of the <see cref="T:System.Windows.Forms.ButtonBorderStyle" /> values that specifies the style of the right border. </param>
		/// <param name="bottomColor">The <see cref="T:System.Drawing.Color" /> of the bottom of the border. </param>
		/// <param name="bottomWidth">The width of the bottom border. </param>
		/// <param name="bottomStyle">One of the <see cref="T:System.Windows.Forms.ButtonBorderStyle" /> values that specifies the style of the bottom border. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000ACD RID: 2765 RVA: 0x0002CC28 File Offset: 0x0002AE28
		public static void DrawBorder(Graphics graphics, Rectangle bounds, Color leftColor, int leftWidth, ButtonBorderStyle leftStyle, Color topColor, int topWidth, ButtonBorderStyle topStyle, Color rightColor, int rightWidth, ButtonBorderStyle rightStyle, Color bottomColor, int bottomWidth, ButtonBorderStyle bottomStyle)
		{
			ThemeEngine.Current.CPDrawBorder(graphics, bounds, leftColor, leftWidth, leftStyle, topColor, topWidth, topStyle, rightColor, rightWidth, rightStyle, bottomColor, bottomWidth, bottomStyle);
		}

		/// <summary>Draws a three-dimensional style border on the specified graphics surface and within the specified bounds on a control.</summary>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> to draw on. </param>
		/// <param name="rectangle">The <see cref="T:System.Drawing.Rectangle" /> that represents the dimensions of the border. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000ACE RID: 2766 RVA: 0x0002CC58 File Offset: 0x0002AE58
		public static void DrawBorder3D(Graphics graphics, Rectangle rectangle)
		{
			ControlPaint.DrawBorder3D(graphics, rectangle, Border3DStyle.Etched, Border3DSide.Left | Border3DSide.Top | Border3DSide.Right | Border3DSide.Bottom);
		}

		/// <summary>Draws a three-dimensional style border with the specified style, on the specified graphics surface, and within the specified bounds on a control.</summary>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> to draw on. </param>
		/// <param name="rectangle">The <see cref="T:System.Drawing.Rectangle" /> that represents the dimensions of the border. </param>
		/// <param name="style">One of the <see cref="T:System.Windows.Forms.Border3DStyle" /> values that specifies the style of the border. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000ACF RID: 2767 RVA: 0x0002CC64 File Offset: 0x0002AE64
		public static void DrawBorder3D(Graphics graphics, Rectangle rectangle, Border3DStyle style)
		{
			ControlPaint.DrawBorder3D(graphics, rectangle, style, Border3DSide.Left | Border3DSide.Top | Border3DSide.Right | Border3DSide.Bottom);
		}

		/// <summary>Draws a three-dimensional style border on the specified graphics surface and within the specified bounds on a control.</summary>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> to draw on. </param>
		/// <param name="x">The x-coordinate of the top left of the border rectangle. </param>
		/// <param name="y">The y-coordinate of the top left of the border rectangle. </param>
		/// <param name="width">The width of the border rectangle. </param>
		/// <param name="height">The height of the border rectangle. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000AD0 RID: 2768 RVA: 0x0002CC70 File Offset: 0x0002AE70
		public static void DrawBorder3D(Graphics graphics, int x, int y, int width, int height)
		{
			ControlPaint.DrawBorder3D(graphics, new Rectangle(x, y, width, height), Border3DStyle.Etched, Border3DSide.Left | Border3DSide.Top | Border3DSide.Right | Border3DSide.Bottom);
		}

		/// <summary>Draws a three-dimensional style border with the specified style, on the specified graphics surface, and within the specified bounds on a control.</summary>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> to draw on. </param>
		/// <param name="x">The x-coordinate of the top left of the border rectangle. </param>
		/// <param name="y">The y-coordinate of the top left of the border rectangle. </param>
		/// <param name="width">The width of the border rectangle. </param>
		/// <param name="height">The height of the border rectangle. </param>
		/// <param name="style">One of the <see cref="T:System.Windows.Forms.Border3DStyle" /> values that specifies the style of the border. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000AD1 RID: 2769 RVA: 0x0002CC88 File Offset: 0x0002AE88
		public static void DrawBorder3D(Graphics graphics, int x, int y, int width, int height, Border3DStyle style)
		{
			ControlPaint.DrawBorder3D(graphics, new Rectangle(x, y, width, height), style, Border3DSide.Left | Border3DSide.Top | Border3DSide.Right | Border3DSide.Bottom);
		}

		/// <summary>Draws a three-dimensional style border with the specified style, on the specified graphics surface and side, and within the specified bounds on a control.</summary>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> to draw on. </param>
		/// <param name="x">The x-coordinate of the top left of the border rectangle. </param>
		/// <param name="y">The y-coordinate of the top left of the border rectangle. </param>
		/// <param name="width">The width of the border rectangle. </param>
		/// <param name="height">The height of the border rectangle. </param>
		/// <param name="style">One of the <see cref="T:System.Windows.Forms.Border3DStyle" /> values that specifies the style of the border. </param>
		/// <param name="sides">The <see cref="T:System.Windows.Forms.Border3DSide" /> of the rectangle to draw the border on. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000AD2 RID: 2770 RVA: 0x0002CCA0 File Offset: 0x0002AEA0
		public static void DrawBorder3D(Graphics graphics, int x, int y, int width, int height, Border3DStyle style, Border3DSide sides)
		{
			ControlPaint.DrawBorder3D(graphics, new Rectangle(x, y, width, height), style, sides);
		}

		/// <summary>Draws a three-dimensional style border with the specified style, on the specified graphics surface and sides, and within the specified bounds on a control.</summary>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> to draw on. </param>
		/// <param name="rectangle">The <see cref="T:System.Drawing.Rectangle" /> that represents the dimensions of the border. </param>
		/// <param name="style">One of the <see cref="T:System.Windows.Forms.Border3DStyle" /> values that specifies the style of the border. </param>
		/// <param name="sides">One of the <see cref="T:System.Windows.Forms.Border3DSide" /> values that specifies the side of the rectangle to draw the border on. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000AD3 RID: 2771 RVA: 0x0002CCB8 File Offset: 0x0002AEB8
		public static void DrawBorder3D(Graphics graphics, Rectangle rectangle, Border3DStyle style, Border3DSide sides)
		{
			ThemeEngine.Current.CPDrawBorder3D(graphics, rectangle, style, sides);
		}

		/// <summary>Draws a button control in the specified state, on the specified graphics surface, and within the specified bounds.</summary>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> to draw on. </param>
		/// <param name="x">The x-coordinate of the upper left corner of the drawing rectangle. </param>
		/// <param name="y">The y-coordinate of the upper left corner of the drawing rectangle. </param>
		/// <param name="width">The width of the button. </param>
		/// <param name="height">The height of the button. </param>
		/// <param name="state">A bitwise combination of the <see cref="T:System.Windows.Forms.ButtonState" /> values that specifies the state to draw the button in. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000AD4 RID: 2772 RVA: 0x0002CCC8 File Offset: 0x0002AEC8
		public static void DrawButton(Graphics graphics, int x, int y, int width, int height, ButtonState state)
		{
			ControlPaint.DrawButton(graphics, new Rectangle(x, y, width, height), state);
		}

		/// <summary>Draws a button control in the specified state, on the specified graphics surface, and within the specified bounds.</summary>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> to draw on. </param>
		/// <param name="rectangle">The <see cref="T:System.Drawing.Rectangle" /> that represents the dimensions of the button. </param>
		/// <param name="state">A bitwise combination of the <see cref="T:System.Windows.Forms.ButtonState" /> values that specifies the state to draw the button in. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000AD5 RID: 2773 RVA: 0x0002CCDC File Offset: 0x0002AEDC
		public static void DrawButton(Graphics graphics, Rectangle rectangle, ButtonState state)
		{
			ThemeEngine.Current.CPDrawButton(graphics, rectangle, state);
		}

		/// <summary>Draws the specified caption button control in the specified state, on the specified graphics surface, and within the specified bounds.</summary>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> to draw on. </param>
		/// <param name="x">The x-coordinate of the top left of the drawing rectangle. </param>
		/// <param name="y">The y-coordinate of the top left of the drawing rectangle. </param>
		/// <param name="width">The width of the drawing rectangle. </param>
		/// <param name="height">The height of the drawing rectangle. </param>
		/// <param name="button">One of the <see cref="T:System.Windows.Forms.CaptionButton" /> values that specifies the type of caption button to draw. </param>
		/// <param name="state">A bitwise combination of the <see cref="T:System.Windows.Forms.ButtonState" /> values that specifies the state to draw the button in. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000AD6 RID: 2774 RVA: 0x0002CCEC File Offset: 0x0002AEEC
		public static void DrawCaptionButton(Graphics graphics, int x, int y, int width, int height, CaptionButton button, ButtonState state)
		{
			ControlPaint.DrawCaptionButton(graphics, new Rectangle(x, y, width, height), button, state);
		}

		/// <summary>Draws the specified caption button control in the specified state, on the specified graphics surface, and within the specified bounds.</summary>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> to draw on. </param>
		/// <param name="rectangle">The <see cref="T:System.Drawing.Rectangle" /> that represents the dimensions of the caption button. </param>
		/// <param name="button">One of the <see cref="T:System.Windows.Forms.CaptionButton" /> values that specifies the type of caption button to draw. </param>
		/// <param name="state">A bitwise combination of the <see cref="T:System.Windows.Forms.ButtonState" /> values that specifies the state to draw the button in. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000AD7 RID: 2775 RVA: 0x0002CD04 File Offset: 0x0002AF04
		public static void DrawCaptionButton(Graphics graphics, Rectangle rectangle, CaptionButton button, ButtonState state)
		{
			ThemeEngine.Current.CPDrawCaptionButton(graphics, rectangle, button, state);
		}

		/// <summary>Draws a check box control in the specified state, on the specified graphics surface, and within the specified bounds.</summary>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> to draw on. </param>
		/// <param name="x">The x-coordinate of the upper left corner of the drawing rectangle. </param>
		/// <param name="y">The y-coordinate of the upper left corner of the drawing rectangle. </param>
		/// <param name="width">The width of the check box. </param>
		/// <param name="height">The height of the check box. </param>
		/// <param name="state">A bitwise combination of the <see cref="T:System.Windows.Forms.ButtonState" /> values that specifies the state to draw the check box in. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000AD8 RID: 2776 RVA: 0x0002CD14 File Offset: 0x0002AF14
		public static void DrawCheckBox(Graphics graphics, int x, int y, int width, int height, ButtonState state)
		{
			ControlPaint.DrawCheckBox(graphics, new Rectangle(x, y, width, height), state);
		}

		/// <summary>Draws a check box control in the specified state, on the specified graphics surface, and within the specified bounds.</summary>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> to draw on. </param>
		/// <param name="rectangle">The <see cref="T:System.Drawing.Rectangle" /> that represents the dimensions of the check box. </param>
		/// <param name="state">A bitwise combination of the <see cref="T:System.Windows.Forms.ButtonState" /> values that specifies the state to draw the check box in. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000AD9 RID: 2777 RVA: 0x0002CD28 File Offset: 0x0002AF28
		public static void DrawCheckBox(Graphics graphics, Rectangle rectangle, ButtonState state)
		{
			ThemeEngine.Current.CPDrawCheckBox(graphics, rectangle, state);
		}

		/// <summary>Draws a drop-down button on a combo box control in the specified state, on the specified graphics surface, and within the specified bounds.</summary>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> to draw on. </param>
		/// <param name="rectangle">The <see cref="T:System.Drawing.Rectangle" /> that represents the dimensions of the combo box. </param>
		/// <param name="state">A bitwise combination of the <see cref="T:System.Windows.Forms.ButtonState" /> values that specifies the state to draw the combo box in. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000ADA RID: 2778 RVA: 0x0002CD38 File Offset: 0x0002AF38
		public static void DrawComboButton(Graphics graphics, Rectangle rectangle, ButtonState state)
		{
			ThemeEngine.Current.CPDrawComboButton(graphics, rectangle, state);
		}

		/// <summary>Draws a drop-down button on a combo box control in the specified state, on the specified graphics surface, and within the specified bounds.</summary>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> to draw on. </param>
		/// <param name="x">The x-coordinate of the top left of the border rectangle. </param>
		/// <param name="y">The y-coordinate of the top left of the border rectangle. </param>
		/// <param name="width">The width of the combo box. </param>
		/// <param name="height">The height of the combo box. </param>
		/// <param name="state">A bitwise combination of the <see cref="T:System.Windows.Forms.ButtonState" /> values that specifies the state to draw the combo box in. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000ADB RID: 2779 RVA: 0x0002CD48 File Offset: 0x0002AF48
		public static void DrawComboButton(Graphics graphics, int x, int y, int width, int height, ButtonState state)
		{
			ControlPaint.DrawComboButton(graphics, new Rectangle(x, y, width, height), state);
		}

		/// <summary>Draws a container control grab handle glyph on the specified graphics surface and within the specified bounds.</summary>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> to draw on. </param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that represents the dimensions of the grab handle glyph. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000ADC RID: 2780 RVA: 0x0002CD5C File Offset: 0x0002AF5C
		public static void DrawContainerGrabHandle(Graphics graphics, Rectangle bounds)
		{
			ThemeEngine.Current.CPDrawContainerGrabHandle(graphics, bounds);
		}

		/// <summary>Draws a focus rectangle on the specified graphics surface and within the specified bounds.</summary>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> to draw on. </param>
		/// <param name="rectangle">The <see cref="T:System.Drawing.Rectangle" /> that represents the dimensions of the grab handle glyph. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000ADD RID: 2781 RVA: 0x0002CD6C File Offset: 0x0002AF6C
		public static void DrawFocusRectangle(Graphics graphics, Rectangle rectangle)
		{
			ControlPaint.DrawFocusRectangle(graphics, rectangle, SystemColors.Control, SystemColors.ControlText);
		}

		/// <summary>Draws a focus rectangle on the specified graphics surface and within the specified bounds.</summary>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> to draw on. </param>
		/// <param name="rectangle">The <see cref="T:System.Drawing.Rectangle" /> that represents the dimensions of the grab handle glyph. </param>
		/// <param name="foreColor">The <see cref="T:System.Drawing.Color" /> that is the foreground color of the object to draw the focus rectangle on. </param>
		/// <param name="backColor">The <see cref="T:System.Drawing.Color" /> that is the background color of the object to draw the focus rectangle on. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000ADE RID: 2782 RVA: 0x0002CD80 File Offset: 0x0002AF80
		public static void DrawFocusRectangle(Graphics graphics, Rectangle rectangle, Color foreColor, Color backColor)
		{
			ThemeEngine.Current.CPDrawFocusRectangle(graphics, rectangle, foreColor, backColor);
		}

		/// <summary>Draws a standard selection grab handle glyph on the specified graphics surface, within the specified bounds, and in the specified state and style.</summary>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> to draw on. </param>
		/// <param name="rectangle">The <see cref="T:System.Drawing.Rectangle" /> that represents the dimensions of the grab handle glyph. </param>
		/// <param name="primary">true to draw the handle as a primary grab handle; otherwise, false. </param>
		/// <param name="enabled">true to draw the handle in an enabled state; otherwise, false. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000ADF RID: 2783 RVA: 0x0002CD90 File Offset: 0x0002AF90
		public static void DrawGrabHandle(Graphics graphics, Rectangle rectangle, bool primary, bool enabled)
		{
			ThemeEngine.Current.CPDrawGrabHandle(graphics, rectangle, primary, enabled);
		}

		/// <summary>Draws a grid of one-pixel dots with the specified spacing, within the specified bounds, on the specified graphics surface, and in the specified color.</summary>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> to draw on. </param>
		/// <param name="area">The <see cref="T:System.Drawing.Rectangle" /> that represents the dimensions of the grid. </param>
		/// <param name="pixelsBetweenDots">The <see cref="T:System.Drawing.Size" /> that specified the height and width between the dots of the grid. </param>
		/// <param name="backColor">The <see cref="T:System.Drawing.Color" /> of the background behind the grid. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000AE0 RID: 2784 RVA: 0x0002CDA0 File Offset: 0x0002AFA0
		public static void DrawGrid(Graphics graphics, Rectangle area, Size pixelsBetweenDots, Color backColor)
		{
			ThemeEngine.Current.CPDrawGrid(graphics, area, pixelsBetweenDots, backColor);
		}

		/// <summary>Draws the specified image in a disabled state.</summary>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> to draw on. </param>
		/// <param name="image">The <see cref="T:System.Drawing.Image" /> to draw. </param>
		/// <param name="x">The x-coordinate of the top left of the border image. </param>
		/// <param name="y">The y-coordinate of the top left of the border image. </param>
		/// <param name="background">The <see cref="T:System.Drawing.Color" /> of the background behind the image. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000AE1 RID: 2785 RVA: 0x0002CDB0 File Offset: 0x0002AFB0
		public static void DrawImageDisabled(Graphics graphics, Image image, int x, int y, Color background)
		{
			ThemeEngine.Current.CPDrawImageDisabled(graphics, image, x, y, background);
		}

		/// <summary>Draws a locked selection frame on the screen within the specified bounds and on the specified graphics surface. Specifies whether to draw the frame with the primary selected colors.</summary>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> to draw on. </param>
		/// <param name="rectangle">The <see cref="T:System.Drawing.Rectangle" /> that represents the dimensions of the frame. </param>
		/// <param name="primary">true to draw this frame with the colors used for the primary selection; otherwise, false. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000AE2 RID: 2786 RVA: 0x0002CDC4 File Offset: 0x0002AFC4
		public static void DrawLockedFrame(Graphics graphics, Rectangle rectangle, bool primary)
		{
			ThemeEngine.Current.CPDrawLockedFrame(graphics, rectangle, primary);
		}

		/// <summary>Draws the specified menu glyph on a menu item control within the specified bounds and on the specified surface.</summary>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> to draw on. </param>
		/// <param name="rectangle">The <see cref="T:System.Drawing.Rectangle" /> that represents the dimensions of the glyph. </param>
		/// <param name="glyph">One of the <see cref="T:System.Windows.Forms.MenuGlyph" /> values that specifies the image to draw. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000AE3 RID: 2787 RVA: 0x0002CDD4 File Offset: 0x0002AFD4
		public static void DrawMenuGlyph(Graphics graphics, Rectangle rectangle, MenuGlyph glyph)
		{
			ThemeEngine.Current.CPDrawMenuGlyph(graphics, rectangle, glyph, ThemeEngine.Current.ColorMenuText, Color.Empty);
		}

		/// <summary>Draws the specified menu glyph on a menu item control within the specified bounds and on the specified surface, replacing <see cref="P:System.Drawing.Color.White" /> with the color specified in the <paramref name="backColor" /> parameter and replacing <see cref="P:System.Drawing.Color.Black" /> with the color specified in the <paramref name="foreColor" /> parameter.</summary>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> to draw on.</param>
		/// <param name="rectangle">The <see cref="T:System.Drawing.Rectangle" /> that represents the dimensions of the glyph. </param>
		/// <param name="glyph">One of the <see cref="T:System.Windows.Forms.MenuGlyph" /> values that specifies the image to draw. </param>
		/// <param name="foreColor">The color that replaces <see cref="P:System.Drawing.Color.White" /> as the foreground color.</param>
		/// <param name="backColor">The color that replaces <see cref="P:System.Drawing.Color.Black" /> as the background color.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000AE4 RID: 2788 RVA: 0x0002CE00 File Offset: 0x0002B000
		public static void DrawMenuGlyph(Graphics graphics, Rectangle rectangle, MenuGlyph glyph, Color foreColor, Color backColor)
		{
			ThemeEngine.Current.CPDrawMenuGlyph(graphics, rectangle, glyph, foreColor, backColor);
		}

		/// <summary>Draws the specified menu glyph on a menu item control with the specified bounds and on the specified surface.</summary>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> to draw on. </param>
		/// <param name="x">The x-coordinate of the upper left corner of the drawing rectangle. </param>
		/// <param name="y">The y-coordinate of the upper left corner of the drawing rectangle. </param>
		/// <param name="width">The width of the menu glyph. </param>
		/// <param name="height">The height of the menu glyph. </param>
		/// <param name="glyph">One of the <see cref="T:System.Windows.Forms.MenuGlyph" /> values that specifies the image to draw. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000AE5 RID: 2789 RVA: 0x0002CE14 File Offset: 0x0002B014
		public static void DrawMenuGlyph(Graphics graphics, int x, int y, int width, int height, MenuGlyph glyph)
		{
			ControlPaint.DrawMenuGlyph(graphics, new Rectangle(x, y, width, height), glyph);
		}

		/// <summary>Draws the specified menu glyph on a menu item control within the specified coordinates, height, and width on the specified surface, replacing <see cref="P:System.Drawing.Color.White" /> with the color specified in the <paramref name="backColor" /> parameter and replacing <see cref="P:System.Drawing.Color.Black" /> with the color specified in the <paramref name="foreColor" /> parameter.</summary>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> to draw on. </param>
		/// <param name="x">The x-coordinate of the upper left corner of the drawing rectangle.</param>
		/// <param name="y">The y-coordinate of the upper left corner of the drawing rectangle. </param>
		/// <param name="width">The width of the menu glyph.</param>
		/// <param name="height">The height of the menu glyph.</param>
		/// <param name="glyph">One of the <see cref="T:System.Windows.Forms.MenuGlyph" /> values that specifies the image to draw.</param>
		/// <param name="foreColor">The color that replaces <see cref="P:System.Drawing.Color.White" /> as the foreground color.</param>
		/// <param name="backColor">The color that replaces <see cref="P:System.Drawing.Color.Black" /> as the background color.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000AE6 RID: 2790 RVA: 0x0002CE28 File Offset: 0x0002B028
		public static void DrawMenuGlyph(Graphics graphics, int x, int y, int width, int height, MenuGlyph glyph, Color foreColor, Color backColor)
		{
			ControlPaint.DrawMenuGlyph(graphics, new Rectangle(x, y, width, height), glyph, foreColor, backColor);
		}

		/// <summary>Draws a three-state check box control in the specified state, on the specified graphics surface, and within the specified bounds.</summary>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> to draw on. </param>
		/// <param name="rectangle">The <see cref="T:System.Drawing.Rectangle" /> that represents the dimensions of the check box. </param>
		/// <param name="state">A bitwise combination of the <see cref="T:System.Windows.Forms.ButtonState" /> values that specifies the state to draw the check box in. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000AE7 RID: 2791 RVA: 0x0002CE40 File Offset: 0x0002B040
		public static void DrawMixedCheckBox(Graphics graphics, Rectangle rectangle, ButtonState state)
		{
			ThemeEngine.Current.CPDrawMixedCheckBox(graphics, rectangle, state);
		}

		/// <summary>Draws a three-state check box control in the specified state, on the specified graphics surface, and within the specified bounds.</summary>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> to draw on. </param>
		/// <param name="x">The x-coordinate of the upper left corner of the drawing rectangle. </param>
		/// <param name="y">The y-coordinate of the upper left corner of the drawing rectangle. </param>
		/// <param name="width">The width of the check box. </param>
		/// <param name="height">The height of the check box. </param>
		/// <param name="state">A bitwise combination of the <see cref="T:System.Windows.Forms.ButtonState" /> values that specifies the state to draw the check box in. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000AE8 RID: 2792 RVA: 0x0002CE50 File Offset: 0x0002B050
		public static void DrawMixedCheckBox(Graphics graphics, int x, int y, int width, int height, ButtonState state)
		{
			ControlPaint.DrawMixedCheckBox(graphics, new Rectangle(x, y, width, height), state);
		}

		/// <summary>Draws a radio button control in the specified state, on the specified graphics surface, and within the specified bounds.</summary>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> to draw on. </param>
		/// <param name="x">The x-coordinate of the upper left corner of the drawing rectangle. </param>
		/// <param name="y">The y-coordinate of the upper left corner of the drawing rectangle. </param>
		/// <param name="width">The width of the radio button. </param>
		/// <param name="height">The height of the radio button. </param>
		/// <param name="state">A bitwise combination of the <see cref="T:System.Windows.Forms.ButtonState" /> values that specifies the state to draw the radio button in. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000AE9 RID: 2793 RVA: 0x0002CE64 File Offset: 0x0002B064
		public static void DrawRadioButton(Graphics graphics, int x, int y, int width, int height, ButtonState state)
		{
			ControlPaint.DrawRadioButton(graphics, new Rectangle(x, y, width, height), state);
		}

		/// <summary>Draws a radio button control in the specified state, on the specified graphics surface, and within the specified bounds.</summary>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> to draw on. </param>
		/// <param name="rectangle">The <see cref="T:System.Drawing.Rectangle" /> that represents the dimensions of the radio button. </param>
		/// <param name="state">A bitwise combination of the <see cref="T:System.Windows.Forms.ButtonState" /> values that specifies the state to draw the radio button in. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000AEA RID: 2794 RVA: 0x0002CE78 File Offset: 0x0002B078
		public static void DrawRadioButton(Graphics graphics, Rectangle rectangle, ButtonState state)
		{
			ThemeEngine.Current.CPDrawRadioButton(graphics, rectangle, state);
		}

		/// <summary>Draws a reversible frame on the screen within the specified bounds, with the specified background color, and in the specified state.</summary>
		/// <param name="rectangle">The <see cref="T:System.Drawing.Rectangle" /> that represents the dimensions of the rectangle to draw, in screen coordinates. </param>
		/// <param name="backColor">The <see cref="T:System.Drawing.Color" /> of the background behind the frame. </param>
		/// <param name="style">One of the <see cref="T:System.Windows.Forms.FrameStyle" /> values that specifies the style of the frame. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Window="AllWindows" />
		/// </PermissionSet>
		// Token: 0x06000AEB RID: 2795 RVA: 0x0002CE88 File Offset: 0x0002B088
		public static void DrawReversibleFrame(Rectangle rectangle, Color backColor, FrameStyle style)
		{
			XplatUI.DrawReversibleFrame(rectangle, backColor, style);
		}

		/// <summary>Draws a reversible line on the screen within the specified starting and ending points and with the specified background color.</summary>
		/// <param name="start">The starting <see cref="T:System.Drawing.Point" /> of the line, in screen coordinates. </param>
		/// <param name="end">The ending <see cref="T:System.Drawing.Point" /> of the line, in screen coordinates. </param>
		/// <param name="backColor">The <see cref="T:System.Drawing.Color" /> of the background behind the line. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Window="AllWindows" />
		/// </PermissionSet>
		// Token: 0x06000AEC RID: 2796 RVA: 0x0002CE94 File Offset: 0x0002B094
		public static void DrawReversibleLine(Point start, Point end, Color backColor)
		{
			XplatUI.DrawReversibleLine(start, end, backColor);
		}

		/// <summary>Draws a filled, reversible rectangle on the screen.</summary>
		/// <param name="rectangle">The <see cref="T:System.Drawing.Rectangle" /> that represents the dimensions of the rectangle to fill, in screen coordinates. </param>
		/// <param name="backColor">The <see cref="T:System.Drawing.Color" /> of the background behind the fill. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Window="AllWindows" />
		/// </PermissionSet>
		// Token: 0x06000AED RID: 2797 RVA: 0x0002CEA0 File Offset: 0x0002B0A0
		public static void FillReversibleRectangle(Rectangle rectangle, Color backColor)
		{
			XplatUI.FillReversibleRectangle(rectangle, backColor);
		}

		/// <summary>Draws the specified scroll button on a scroll bar control in the specified state, on the specified graphics surface, and within the specified bounds.</summary>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> to draw on. </param>
		/// <param name="x">The x-coordinate of the upper left corner of the drawing rectangle. </param>
		/// <param name="y">The y-coordinate of the upper left corner of the drawing rectangle. </param>
		/// <param name="width">The width of the scroll button. </param>
		/// <param name="height">The height of the scroll button. </param>
		/// <param name="button">One of the <see cref="T:System.Windows.Forms.ScrollButton" /> values that specifies the type of scroll arrow to draw. </param>
		/// <param name="state">A bitwise combination of the <see cref="T:System.Windows.Forms.ButtonState" /> values that specifies the state to draw the scroll button in. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000AEE RID: 2798 RVA: 0x0002CEAC File Offset: 0x0002B0AC
		public static void DrawScrollButton(Graphics graphics, int x, int y, int width, int height, ScrollButton button, ButtonState state)
		{
			ThemeEngine.Current.CPDrawScrollButton(graphics, new Rectangle(x, y, width, height), button, state);
		}

		/// <summary>Draws the specified scroll button on a scroll bar control in the specified state, on the specified graphics surface, and within the specified bounds.</summary>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> to draw on. </param>
		/// <param name="rectangle">The <see cref="T:System.Drawing.Rectangle" /> that represents the dimensions of the glyph. </param>
		/// <param name="button">One of the <see cref="T:System.Windows.Forms.ScrollButton" /> values that specifies the type of scroll arrow to draw. </param>
		/// <param name="state">A bitwise combination of the <see cref="T:System.Windows.Forms.ButtonState" /> values that specifies the state to draw the scroll button in. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000AEF RID: 2799 RVA: 0x0002CEC8 File Offset: 0x0002B0C8
		public static void DrawScrollButton(Graphics graphics, Rectangle rectangle, ScrollButton button, ButtonState state)
		{
			ThemeEngine.Current.CPDrawScrollButton(graphics, rectangle, button, state);
		}

		/// <summary>Draws a standard selection frame in the specified state, on the specified graphics surface, with the specified inner and outer dimensions, and with the specified background color.</summary>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> to draw on. </param>
		/// <param name="active">true to draw the selection frame in an active state; otherwise, false. </param>
		/// <param name="outsideRect">The <see cref="T:System.Drawing.Rectangle" /> that represents the outer boundary of the selection frame. </param>
		/// <param name="insideRect">The <see cref="T:System.Drawing.Rectangle" /> that represents the inner boundary of the selection frame.</param>
		/// <param name="backColor">The <see cref="T:System.Drawing.Color" /> of the background behind the frame. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000AF0 RID: 2800 RVA: 0x0002CED8 File Offset: 0x0002B0D8
		public static void DrawSelectionFrame(Graphics graphics, bool active, Rectangle outsideRect, Rectangle insideRect, Color backColor)
		{
			if (!ControlPaint.DSFNotImpl)
			{
				ControlPaint.DSFNotImpl = true;
				Console.WriteLine("NOT IMPLEMENTED: DrawSelectionFrame(Graphics graphics, bool active, Rectangle outsideRect, Rectangle insideRect, Color backColor)");
			}
		}

		/// <summary>Draws a size grip on a form with the specified bounds and background color and on the specified graphics surface.</summary>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> to draw on. </param>
		/// <param name="backColor">The <see cref="T:System.Drawing.Color" /> of the background used to determine the colors of the size grip.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that represents the dimensions of the size grip.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000AF1 RID: 2801 RVA: 0x0002CEF4 File Offset: 0x0002B0F4
		public static void DrawSizeGrip(Graphics graphics, Color backColor, Rectangle bounds)
		{
			ThemeEngine.Current.CPDrawSizeGrip(graphics, backColor, bounds);
		}

		/// <summary>Draws a size grip on a form with the specified bounds and background color and on the specified graphics surface.</summary>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> to draw on. </param>
		/// <param name="backColor">The <see cref="T:System.Drawing.Color" /> of the background used to determine the colors of the size grip. </param>
		/// <param name="x">The x-coordinate of the upper left corner of the size grip. </param>
		/// <param name="y">The y-coordinate of the upper left corner of the size grip. </param>
		/// <param name="width">The width of the size grip. </param>
		/// <param name="height">The height of the size grip. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000AF2 RID: 2802 RVA: 0x0002CF04 File Offset: 0x0002B104
		public static void DrawSizeGrip(Graphics graphics, Color backColor, int x, int y, int width, int height)
		{
			ControlPaint.DrawSizeGrip(graphics, backColor, new Rectangle(x, y, width, height));
		}

		/// <summary>Draws the specified string in a disabled state on the specified graphics surface; within the specified bounds; and in the specified font, color, and format.</summary>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> to draw on. </param>
		/// <param name="s">The string to draw. </param>
		/// <param name="font">The <see cref="T:System.Drawing.Font" /> to draw the string with. </param>
		/// <param name="color">The <see cref="T:System.Drawing.Color" /> of the background behind the string. </param>
		/// <param name="layoutRectangle">The <see cref="T:System.Drawing.RectangleF" /> that represents the dimensions of the string. </param>
		/// <param name="format">The <see cref="T:System.Drawing.StringFormat" /> to apply to the string. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000AF3 RID: 2803 RVA: 0x0002CF18 File Offset: 0x0002B118
		public static void DrawStringDisabled(Graphics graphics, string s, Font font, Color color, RectangleF layoutRectangle, StringFormat format)
		{
			ThemeEngine.Current.CPDrawStringDisabled(graphics, s, font, color, layoutRectangle, format);
		}

		/// <summary>Draws the specified string in a disabled state on the specified graphics surface, within the specified bounds, and in the specified font, color, and format, using the specified GDI-based <see cref="T:System.Windows.Forms.TextRenderer" />.</summary>
		/// <param name="dc">The GDI-based <see cref="T:System.Windows.Forms.TextRenderer" />.</param>
		/// <param name="s">The string to draw. </param>
		/// <param name="font">The <see cref="T:System.Drawing.Font" /> to draw the string with.</param>
		/// <param name="color">The <see cref="T:System.Drawing.Color" /> of the background behind the string.</param>
		/// <param name="layoutRectangle">The <see cref="T:System.Drawing.RectangleF" /> that represents the dimensions of the string.</param>
		/// <param name="format">The <see cref="T:System.Drawing.StringFormat" /> to apply to the string.</param>
		// Token: 0x06000AF4 RID: 2804 RVA: 0x0002CF38 File Offset: 0x0002B138
		public static void DrawStringDisabled(IDeviceContext dc, string s, Font font, Color color, Rectangle layoutRectangle, TextFormatFlags format)
		{
			ThemeEngine.Current.CPDrawStringDisabled(dc, s, font, color, layoutRectangle, format);
		}

		/// <summary>Draws a border in the style appropriate for disabled items.</summary>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> to draw on.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that represents the dimensions of the border.</param>
		// Token: 0x06000AF5 RID: 2805 RVA: 0x0002CF58 File Offset: 0x0002B158
		public static void DrawVisualStyleBorder(Graphics graphics, Rectangle bounds)
		{
			ThemeEngine.Current.CPDrawVisualStyleBorder(graphics, bounds);
		}

		// Token: 0x0400083C RID: 2108
		private static int RGBMax = 255;

		// Token: 0x0400083D RID: 2109
		private static int HLSMax = 255;

		// Token: 0x0400083E RID: 2110
		[MonoTODO("Stub, does nothing")]
		private static bool DSFNotImpl;
	}
}
