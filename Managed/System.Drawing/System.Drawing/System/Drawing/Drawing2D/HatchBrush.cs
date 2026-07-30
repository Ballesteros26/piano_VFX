using System;
using System.Runtime.InteropServices;

namespace System.Drawing.Drawing2D
{
	/// <summary>Defines a rectangular brush with a hatch style, a foreground color, and a background color. This class cannot be inherited.</summary>
	// Token: 0x0200013D RID: 317
	public sealed class HatchBrush : Brush
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Drawing2D.HatchBrush" /> class with the specified <see cref="T:System.Drawing.Drawing2D.HatchStyle" /> enumeration and foreground color.</summary>
		/// <param name="hatchstyle">One of the <see cref="T:System.Drawing.Drawing2D.HatchStyle" /> values that represents the pattern drawn by this <see cref="T:System.Drawing.Drawing2D.HatchBrush" />. </param>
		/// <param name="foreColor">The <see cref="T:System.Drawing.Color" /> structure that represents the color of lines drawn by this <see cref="T:System.Drawing.Drawing2D.HatchBrush" />. </param>
		// Token: 0x06000E0C RID: 3596 RVA: 0x0001EE9F File Offset: 0x0001D09F
		public HatchBrush(HatchStyle hatchstyle, Color foreColor)
			: this(hatchstyle, foreColor, Color.FromArgb(-16777216))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Drawing2D.HatchBrush" /> class with the specified <see cref="T:System.Drawing.Drawing2D.HatchStyle" /> enumeration, foreground color, and background color.</summary>
		/// <param name="hatchstyle">One of the <see cref="T:System.Drawing.Drawing2D.HatchStyle" /> values that represents the pattern drawn by this <see cref="T:System.Drawing.Drawing2D.HatchBrush" />. </param>
		/// <param name="foreColor">The <see cref="T:System.Drawing.Color" /> structure that represents the color of lines drawn by this <see cref="T:System.Drawing.Drawing2D.HatchBrush" />. </param>
		/// <param name="backColor">The <see cref="T:System.Drawing.Color" /> structure that represents the color of spaces between the lines drawn by this <see cref="T:System.Drawing.Drawing2D.HatchBrush" />. </param>
		// Token: 0x06000E0D RID: 3597 RVA: 0x0001EEB4 File Offset: 0x0001D0B4
		public HatchBrush(HatchStyle hatchstyle, Color foreColor, Color backColor)
		{
			if (hatchstyle < HatchStyle.Horizontal || hatchstyle > HatchStyle.SolidDiamond)
			{
				throw new ArgumentException(SR.Format("The value of argument '{0}' ({1}) is invalid for Enum type '{2}'.", new object[] { "hatchstyle", hatchstyle, "HatchStyle" }), "hatchstyle");
			}
			IntPtr intPtr;
			SafeNativeMethods.Gdip.CheckStatus(GDIPlus.GdipCreateHatchBrush((int)hatchstyle, foreColor.ToArgb(), backColor.ToArgb(), out intPtr));
			base.SetNativeBrushInternal(intPtr);
		}

		// Token: 0x06000E0E RID: 3598 RVA: 0x00003E14 File Offset: 0x00002014
		internal HatchBrush(IntPtr nativeBrush)
		{
			base.SetNativeBrushInternal(nativeBrush);
		}

		/// <summary>Creates an exact copy of this <see cref="T:System.Drawing.Drawing2D.HatchBrush" /> object.</summary>
		/// <returns>The <see cref="T:System.Drawing.Drawing2D.HatchBrush" /> this method creates, cast as an object.</returns>
		// Token: 0x06000E0F RID: 3599 RVA: 0x0001EF28 File Offset: 0x0001D128
		public override object Clone()
		{
			IntPtr zero = IntPtr.Zero;
			SafeNativeMethods.Gdip.CheckStatus(GDIPlus.GdipCloneBrush(new HandleRef(this, base.NativeBrush), out zero));
			return new HatchBrush(zero);
		}

		/// <summary>Gets the hatch style of this <see cref="T:System.Drawing.Drawing2D.HatchBrush" /> object.</summary>
		/// <returns>One of the <see cref="T:System.Drawing.Drawing2D.HatchStyle" /> values that represents the pattern of this <see cref="T:System.Drawing.Drawing2D.HatchBrush" />.</returns>
		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x06000E10 RID: 3600 RVA: 0x0001EF5C File Offset: 0x0001D15C
		public HatchStyle HatchStyle
		{
			get
			{
				int num;
				SafeNativeMethods.Gdip.CheckStatus(GDIPlus.GdipGetHatchStyle(new HandleRef(this, base.NativeBrush), out num));
				return (HatchStyle)num;
			}
		}

		/// <summary>Gets the color of hatch lines drawn by this <see cref="T:System.Drawing.Drawing2D.HatchBrush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> structure that represents the foreground color for this <see cref="T:System.Drawing.Drawing2D.HatchBrush" />.</returns>
		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x06000E11 RID: 3601 RVA: 0x0001EF84 File Offset: 0x0001D184
		public Color ForegroundColor
		{
			get
			{
				int num;
				SafeNativeMethods.Gdip.CheckStatus(GDIPlus.GdipGetHatchForegroundColor(new HandleRef(this, base.NativeBrush), out num));
				return Color.FromArgb(num);
			}
		}

		/// <summary>Gets the color of spaces between the hatch lines drawn by this <see cref="T:System.Drawing.Drawing2D.HatchBrush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> structure that represents the background color for this <see cref="T:System.Drawing.Drawing2D.HatchBrush" />.</returns>
		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x06000E12 RID: 3602 RVA: 0x0001EFB0 File Offset: 0x0001D1B0
		public Color BackgroundColor
		{
			get
			{
				int num;
				SafeNativeMethods.Gdip.CheckStatus(GDIPlus.GdipGetHatchBackgroundColor(new HandleRef(this, base.NativeBrush), out num));
				return Color.FromArgb(num);
			}
		}
	}
}
