using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace System.Drawing.Drawing2D
{
	/// <summary>Encapsulates a <see cref="T:System.Drawing.Brush" /> with a linear gradient. This class cannot be inherited.</summary>
	// Token: 0x02000151 RID: 337
	public sealed class LinearGradientBrush : Brush
	{
		// Token: 0x06000E89 RID: 3721 RVA: 0x0001FFFD File Offset: 0x0001E1FD
		internal LinearGradientBrush(IntPtr native)
		{
			Status status = GDIPlus.GdipGetLineRect(native, out this.rectangle);
			base.SetNativeBrush(native);
			GDIPlus.CheckStatus(status);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Drawing2D.LinearGradientBrush" /> class with the specified points and colors.</summary>
		/// <param name="point1">A <see cref="T:System.Drawing.Point" /> structure that represents the starting point of the linear gradient. </param>
		/// <param name="point2">A <see cref="T:System.Drawing.Point" /> structure that represents the endpoint of the linear gradient. </param>
		/// <param name="color1">A <see cref="T:System.Drawing.Color" /> structure that represents the starting color of the linear gradient. </param>
		/// <param name="color2">A <see cref="T:System.Drawing.Color" /> structure that represents the ending color of the linear gradient. </param>
		// Token: 0x06000E8A RID: 3722 RVA: 0x00020020 File Offset: 0x0001E220
		public LinearGradientBrush(Point point1, Point point2, Color color1, Color color2)
		{
			IntPtr intPtr;
			GDIPlus.CheckStatus(GDIPlus.GdipCreateLineBrushI(ref point1, ref point2, color1.ToArgb(), color2.ToArgb(), WrapMode.Tile, out intPtr));
			base.SetNativeBrush(intPtr);
			GDIPlus.CheckStatus(GDIPlus.GdipGetLineRect(intPtr, out this.rectangle));
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Drawing2D.LinearGradientBrush" /> class with the specified points and colors.</summary>
		/// <param name="point1">A <see cref="T:System.Drawing.PointF" /> structure that represents the starting point of the linear gradient. </param>
		/// <param name="point2">A <see cref="T:System.Drawing.PointF" /> structure that represents the endpoint of the linear gradient. </param>
		/// <param name="color1">A <see cref="T:System.Drawing.Color" /> structure that represents the starting color of the linear gradient. </param>
		/// <param name="color2">A <see cref="T:System.Drawing.Color" /> structure that represents the ending color of the linear gradient. </param>
		// Token: 0x06000E8B RID: 3723 RVA: 0x0002006C File Offset: 0x0001E26C
		public LinearGradientBrush(PointF point1, PointF point2, Color color1, Color color2)
		{
			IntPtr intPtr;
			GDIPlus.CheckStatus(GDIPlus.GdipCreateLineBrush(ref point1, ref point2, color1.ToArgb(), color2.ToArgb(), WrapMode.Tile, out intPtr));
			base.SetNativeBrush(intPtr);
			GDIPlus.CheckStatus(GDIPlus.GdipGetLineRect(intPtr, out this.rectangle));
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Drawing.Drawing2D.LinearGradientBrush" /> class based on a rectangle, starting and ending colors, and orientation.</summary>
		/// <param name="rect">A <see cref="T:System.Drawing.Rectangle" /> structure that specifies the bounds of the linear gradient. </param>
		/// <param name="color1">A <see cref="T:System.Drawing.Color" /> structure that represents the starting color for the gradient. </param>
		/// <param name="color2">A <see cref="T:System.Drawing.Color" /> structure that represents the ending color for the gradient. </param>
		/// <param name="linearGradientMode">A <see cref="T:System.Drawing.Drawing2D.LinearGradientMode" /> enumeration element that specifies the orientation of the gradient. The orientation determines the starting and ending points of the gradient. For example, LinearGradientMode.ForwardDiagonal specifies that the starting point is the upper-left corner of the rectangle and the ending point is the lower-right corner of the rectangle. </param>
		// Token: 0x06000E8C RID: 3724 RVA: 0x000200B8 File Offset: 0x0001E2B8
		public LinearGradientBrush(Rectangle rect, Color color1, Color color2, LinearGradientMode linearGradientMode)
		{
			if (linearGradientMode < LinearGradientMode.Horizontal || linearGradientMode > LinearGradientMode.BackwardDiagonal)
			{
				throw new InvalidEnumArgumentException("linearGradientMode", (int)linearGradientMode, typeof(LinearGradientMode));
			}
			if (rect.Width == 0 || rect.Height == 0)
			{
				throw new ArgumentException(string.Format("Rectangle '{0}' cannot have a width or height equal to 0.", rect.ToString()));
			}
			IntPtr intPtr;
			GDIPlus.CheckStatus(GDIPlus.GdipCreateLineBrushFromRectI(ref rect, color1.ToArgb(), color2.ToArgb(), linearGradientMode, WrapMode.Tile, out intPtr));
			base.SetNativeBrush(intPtr);
			this.rectangle = rect;
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Drawing.Drawing2D.LinearGradientBrush" /> class based on a rectangle, starting and ending colors, and an orientation angle.</summary>
		/// <param name="rect">A <see cref="T:System.Drawing.Rectangle" /> structure that specifies the bounds of the linear gradient. </param>
		/// <param name="color1">A <see cref="T:System.Drawing.Color" /> structure that represents the starting color for the gradient. </param>
		/// <param name="color2">A <see cref="T:System.Drawing.Color" /> structure that represents the ending color for the gradient. </param>
		/// <param name="angle">The angle, measured in degrees clockwise from the x-axis, of the gradient's orientation line. </param>
		// Token: 0x06000E8D RID: 3725 RVA: 0x0002014D File Offset: 0x0001E34D
		public LinearGradientBrush(Rectangle rect, Color color1, Color color2, float angle)
			: this(rect, color1, color2, angle, false)
		{
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Drawing.Drawing2D.LinearGradientBrush" /> based on a rectangle, starting and ending colors, and an orientation mode.</summary>
		/// <param name="rect">A <see cref="T:System.Drawing.RectangleF" /> structure that specifies the bounds of the linear gradient. </param>
		/// <param name="color1">A <see cref="T:System.Drawing.Color" /> structure that represents the starting color for the gradient. </param>
		/// <param name="color2">A <see cref="T:System.Drawing.Color" /> structure that represents the ending color for the gradient. </param>
		/// <param name="linearGradientMode">A <see cref="T:System.Drawing.Drawing2D.LinearGradientMode" /> enumeration element that specifies the orientation of the gradient. The orientation determines the starting and ending points of the gradient. For example, LinearGradientMode.ForwardDiagonal specifies that the starting point is the upper-left corner of the rectangle and the ending point is the lower-right corner of the rectangle. </param>
		// Token: 0x06000E8E RID: 3726 RVA: 0x0002015C File Offset: 0x0001E35C
		public LinearGradientBrush(RectangleF rect, Color color1, Color color2, LinearGradientMode linearGradientMode)
		{
			if (linearGradientMode < LinearGradientMode.Horizontal || linearGradientMode > LinearGradientMode.BackwardDiagonal)
			{
				throw new InvalidEnumArgumentException("linearGradientMode", (int)linearGradientMode, typeof(LinearGradientMode));
			}
			if ((double)rect.Width == 0.0 || (double)rect.Height == 0.0)
			{
				throw new ArgumentException(string.Format("Rectangle '{0}' cannot have a width or height equal to 0.", rect.ToString()));
			}
			IntPtr intPtr;
			GDIPlus.CheckStatus(GDIPlus.GdipCreateLineBrushFromRect(ref rect, color1.ToArgb(), color2.ToArgb(), linearGradientMode, WrapMode.Tile, out intPtr));
			base.SetNativeBrush(intPtr);
			this.rectangle = rect;
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Drawing.Drawing2D.LinearGradientBrush" /> class based on a rectangle, starting and ending colors, and an orientation angle.</summary>
		/// <param name="rect">A <see cref="T:System.Drawing.RectangleF" /> structure that specifies the bounds of the linear gradient. </param>
		/// <param name="color1">A <see cref="T:System.Drawing.Color" /> structure that represents the starting color for the gradient. </param>
		/// <param name="color2">A <see cref="T:System.Drawing.Color" /> structure that represents the ending color for the gradient. </param>
		/// <param name="angle">The angle, measured in degrees clockwise from the x-axis, of the gradient's orientation line. </param>
		// Token: 0x06000E8F RID: 3727 RVA: 0x00020200 File Offset: 0x0001E400
		public LinearGradientBrush(RectangleF rect, Color color1, Color color2, float angle)
			: this(rect, color1, color2, angle, false)
		{
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Drawing.Drawing2D.LinearGradientBrush" /> class based on a rectangle, starting and ending colors, and an orientation angle.</summary>
		/// <param name="rect">A <see cref="T:System.Drawing.Rectangle" /> structure that specifies the bounds of the linear gradient. </param>
		/// <param name="color1">A <see cref="T:System.Drawing.Color" /> structure that represents the starting color for the gradient. </param>
		/// <param name="color2">A <see cref="T:System.Drawing.Color" /> structure that represents the ending color for the gradient. </param>
		/// <param name="angle">The angle, measured in degrees clockwise from the x-axis, of the gradient's orientation line. </param>
		/// <param name="isAngleScaleable">Set to true to specify that the angle is affected by the transform associated with this <see cref="T:System.Drawing.Drawing2D.LinearGradientBrush" />; otherwise, false. </param>
		// Token: 0x06000E90 RID: 3728 RVA: 0x00020210 File Offset: 0x0001E410
		public LinearGradientBrush(Rectangle rect, Color color1, Color color2, float angle, bool isAngleScaleable)
		{
			if (rect.Width == 0 || rect.Height == 0)
			{
				throw new ArgumentException(string.Format("Rectangle '{0}' cannot have a width or height equal to 0.", rect.ToString()));
			}
			IntPtr intPtr;
			GDIPlus.CheckStatus(GDIPlus.GdipCreateLineBrushFromRectWithAngleI(ref rect, color1.ToArgb(), color2.ToArgb(), angle, isAngleScaleable, WrapMode.Tile, out intPtr));
			base.SetNativeBrush(intPtr);
			this.rectangle = rect;
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Drawing.Drawing2D.LinearGradientBrush" /> class based on a rectangle, starting and ending colors, and an orientation angle.</summary>
		/// <param name="rect">A <see cref="T:System.Drawing.RectangleF" /> structure that specifies the bounds of the linear gradient. </param>
		/// <param name="color1">A <see cref="T:System.Drawing.Color" /> structure that represents the starting color for the gradient. </param>
		/// <param name="color2">A <see cref="T:System.Drawing.Color" /> structure that represents the ending color for the gradient. </param>
		/// <param name="angle">The angle, measured in degrees clockwise from the x-axis, of the gradient's orientation line. </param>
		/// <param name="isAngleScaleable">Set to true to specify that the angle is affected by the transform associated with this <see cref="T:System.Drawing.Drawing2D.LinearGradientBrush" />; otherwise, false. </param>
		// Token: 0x06000E91 RID: 3729 RVA: 0x00020288 File Offset: 0x0001E488
		public LinearGradientBrush(RectangleF rect, Color color1, Color color2, float angle, bool isAngleScaleable)
		{
			if (rect.Width == 0f || rect.Height == 0f)
			{
				throw new ArgumentException(string.Format("Rectangle '{0}' cannot have a width or height equal to 0.", rect.ToString()));
			}
			IntPtr intPtr;
			GDIPlus.CheckStatus(GDIPlus.GdipCreateLineBrushFromRectWithAngle(ref rect, color1.ToArgb(), color2.ToArgb(), angle, isAngleScaleable, WrapMode.Tile, out intPtr));
			base.SetNativeBrush(intPtr);
			this.rectangle = rect;
		}

		/// <summary>Gets or sets a <see cref="T:System.Drawing.Drawing2D.Blend" /> that specifies positions and factors that define a custom falloff for the gradient.</summary>
		/// <returns>A <see cref="T:System.Drawing.Drawing2D.Blend" /> that represents a custom falloff for the gradient.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x06000E92 RID: 3730 RVA: 0x00020304 File Offset: 0x0001E504
		// (set) Token: 0x06000E93 RID: 3731 RVA: 0x00020358 File Offset: 0x0001E558
		public Blend Blend
		{
			get
			{
				int num;
				GDIPlus.CheckStatus(GDIPlus.GdipGetLineBlendCount(base.NativeBrush, out num));
				float[] array = new float[num];
				float[] array2 = new float[num];
				GDIPlus.CheckStatus(GDIPlus.GdipGetLineBlend(base.NativeBrush, array, array2, num));
				return new Blend
				{
					Factors = array,
					Positions = array2
				};
			}
			set
			{
				float[] factors = value.Factors;
				float[] positions = value.Positions;
				int num = factors.Length;
				if (num == 0 || positions.Length == 0)
				{
					throw new ArgumentException("Invalid Blend object. It should have at least 2 elements in each of the factors and positions arrays.");
				}
				if (num != positions.Length)
				{
					throw new ArgumentException("Invalid Blend object. It should contain the same number of factors and positions values.");
				}
				if (positions[0] != 0f)
				{
					throw new ArgumentException("Invalid Blend object. The positions array must have 0.0 as its first element.");
				}
				if (positions[num - 1] != 1f)
				{
					throw new ArgumentException("Invalid Blend object. The positions array must have 1.0 as its last element.");
				}
				GDIPlus.CheckStatus(GDIPlus.GdipSetLineBlend(base.NativeBrush, factors, positions, num));
			}
		}

		/// <summary>Gets or sets a value indicating whether gamma correction is enabled for this <see cref="T:System.Drawing.Drawing2D.LinearGradientBrush" />.</summary>
		/// <returns>The value is true if gamma correction is enabled for this <see cref="T:System.Drawing.Drawing2D.LinearGradientBrush" />; otherwise, false.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x06000E94 RID: 3732 RVA: 0x000203DC File Offset: 0x0001E5DC
		// (set) Token: 0x06000E95 RID: 3733 RVA: 0x000203FC File Offset: 0x0001E5FC
		[MonoTODO("The GammaCorrection value is ignored when using libgdiplus.")]
		public bool GammaCorrection
		{
			get
			{
				bool flag;
				GDIPlus.CheckStatus(GDIPlus.GdipGetLineGammaCorrection(base.NativeBrush, out flag));
				return flag;
			}
			set
			{
				GDIPlus.CheckStatus(GDIPlus.GdipSetLineGammaCorrection(base.NativeBrush, value));
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Drawing.Drawing2D.ColorBlend" /> that defines a multicolor linear gradient.</summary>
		/// <returns>A <see cref="T:System.Drawing.Drawing2D.ColorBlend" /> that defines a multicolor linear gradient.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x06000E96 RID: 3734 RVA: 0x00020410 File Offset: 0x0001E610
		// (set) Token: 0x06000E97 RID: 3735 RVA: 0x00020490 File Offset: 0x0001E690
		public ColorBlend InterpolationColors
		{
			get
			{
				int num;
				GDIPlus.CheckStatus(GDIPlus.GdipGetLinePresetBlendCount(base.NativeBrush, out num));
				int[] array = new int[num];
				float[] array2 = new float[num];
				GDIPlus.CheckStatus(GDIPlus.GdipGetLinePresetBlend(base.NativeBrush, array, array2, num));
				ColorBlend colorBlend = new ColorBlend();
				Color[] array3 = new Color[num];
				for (int i = 0; i < num; i++)
				{
					array3[i] = Color.FromArgb(array[i]);
				}
				colorBlend.Colors = array3;
				colorBlend.Positions = array2;
				return colorBlend;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentException("InterpolationColors is null");
				}
				Color[] colors = value.Colors;
				float[] positions = value.Positions;
				int num = colors.Length;
				if (num == 0 || positions.Length == 0)
				{
					throw new ArgumentException("Invalid ColorBlend object. It should have at least 2 elements in each of the colors and positions arrays.");
				}
				if (num != positions.Length)
				{
					throw new ArgumentException("Invalid ColorBlend object. It should contain the same number of positions and color values.");
				}
				if (positions[0] != 0f)
				{
					throw new ArgumentException("Invalid ColorBlend object. The positions array must have 0.0 as its first element.");
				}
				if (positions[num - 1] != 1f)
				{
					throw new ArgumentException("Invalid ColorBlend object. The positions array must have 1.0 as its last element.");
				}
				int[] array = new int[colors.Length];
				for (int i = 0; i < colors.Length; i++)
				{
					array[i] = colors[i].ToArgb();
				}
				GDIPlus.CheckStatus(GDIPlus.GdipSetLinePresetBlend(base.NativeBrush, array, positions, num));
			}
		}

		/// <summary>Gets or sets the starting and ending colors of the gradient.</summary>
		/// <returns>An array of two <see cref="T:System.Drawing.Color" /> structures that represents the starting and ending colors of the gradient.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x06000E98 RID: 3736 RVA: 0x0002054C File Offset: 0x0001E74C
		// (set) Token: 0x06000E99 RID: 3737 RVA: 0x00020595 File Offset: 0x0001E795
		public Color[] LinearColors
		{
			get
			{
				int[] array = new int[2];
				GDIPlus.CheckStatus(GDIPlus.GdipGetLineColors(base.NativeBrush, array));
				return new Color[]
				{
					Color.FromArgb(array[0]),
					Color.FromArgb(array[1])
				};
			}
			set
			{
				GDIPlus.CheckStatus(GDIPlus.GdipSetLineColors(base.NativeBrush, value[0].ToArgb(), value[1].ToArgb()));
			}
		}

		/// <summary>Gets a rectangular region that defines the starting and ending points of the gradient.</summary>
		/// <returns>A <see cref="T:System.Drawing.RectangleF" /> structure that specifies the starting and ending points of the gradient.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x06000E9A RID: 3738 RVA: 0x000205BF File Offset: 0x0001E7BF
		public RectangleF Rectangle
		{
			get
			{
				return this.rectangle;
			}
		}

		/// <summary>Gets or sets a copy <see cref="T:System.Drawing.Drawing2D.Matrix" /> that defines a local geometric transform for this <see cref="T:System.Drawing.Drawing2D.LinearGradientBrush" />.</summary>
		/// <returns>A copy of the <see cref="T:System.Drawing.Drawing2D.Matrix" /> that defines a geometric transform that applies only to fills drawn with this <see cref="T:System.Drawing.Drawing2D.LinearGradientBrush" />.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x06000E9B RID: 3739 RVA: 0x000205C8 File Offset: 0x0001E7C8
		// (set) Token: 0x06000E9C RID: 3740 RVA: 0x000205F2 File Offset: 0x0001E7F2
		public Matrix Transform
		{
			get
			{
				Matrix matrix = new Matrix();
				GDIPlus.CheckStatus(GDIPlus.GdipGetLineTransform(base.NativeBrush, matrix.nativeMatrix));
				return matrix;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("Transform");
				}
				GDIPlus.CheckStatus(GDIPlus.GdipSetLineTransform(base.NativeBrush, value.nativeMatrix));
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Drawing.Drawing2D.WrapMode" /> enumeration that indicates the wrap mode for this <see cref="T:System.Drawing.Drawing2D.LinearGradientBrush" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Drawing2D.WrapMode" /> that specifies how fills drawn with this <see cref="T:System.Drawing.Drawing2D.LinearGradientBrush" /> are tiled.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x06000E9D RID: 3741 RVA: 0x00020618 File Offset: 0x0001E818
		// (set) Token: 0x06000E9E RID: 3742 RVA: 0x00020638 File Offset: 0x0001E838
		public WrapMode WrapMode
		{
			get
			{
				WrapMode wrapMode;
				GDIPlus.CheckStatus(GDIPlus.GdipGetLineWrapMode(base.NativeBrush, out wrapMode));
				return wrapMode;
			}
			set
			{
				if (value < WrapMode.Tile || value > WrapMode.Clamp)
				{
					throw new InvalidEnumArgumentException("WrapMode");
				}
				GDIPlus.CheckStatus(GDIPlus.GdipSetLineWrapMode(base.NativeBrush, value));
			}
		}

		/// <summary>Multiplies the <see cref="T:System.Drawing.Drawing2D.Matrix" /> that represents the local geometric transform of this <see cref="T:System.Drawing.Drawing2D.LinearGradientBrush" /> by the specified <see cref="T:System.Drawing.Drawing2D.Matrix" /> by prepending the specified <see cref="T:System.Drawing.Drawing2D.Matrix" />.</summary>
		/// <param name="matrix">The <see cref="T:System.Drawing.Drawing2D.Matrix" /> by which to multiply the geometric transform. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000E9F RID: 3743 RVA: 0x0002065E File Offset: 0x0001E85E
		public void MultiplyTransform(Matrix matrix)
		{
			this.MultiplyTransform(matrix, MatrixOrder.Prepend);
		}

		/// <summary>Multiplies the <see cref="T:System.Drawing.Drawing2D.Matrix" /> that represents the local geometric transform of this <see cref="T:System.Drawing.Drawing2D.LinearGradientBrush" /> by the specified <see cref="T:System.Drawing.Drawing2D.Matrix" /> in the specified order.</summary>
		/// <param name="matrix">The <see cref="T:System.Drawing.Drawing2D.Matrix" /> by which to multiply the geometric transform. </param>
		/// <param name="order">A <see cref="T:System.Drawing.Drawing2D.MatrixOrder" /> that specifies in which order to multiply the two matrices. </param>
		// Token: 0x06000EA0 RID: 3744 RVA: 0x00020668 File Offset: 0x0001E868
		public void MultiplyTransform(Matrix matrix, MatrixOrder order)
		{
			if (matrix == null)
			{
				throw new ArgumentNullException("matrix");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipMultiplyLineTransform(base.NativeBrush, matrix.nativeMatrix, order));
		}

		/// <summary>Resets the <see cref="P:System.Drawing.Drawing2D.LinearGradientBrush.Transform" /> property to identity.</summary>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000EA1 RID: 3745 RVA: 0x0002068F File Offset: 0x0001E88F
		public void ResetTransform()
		{
			GDIPlus.CheckStatus(GDIPlus.GdipResetLineTransform(base.NativeBrush));
		}

		/// <summary>Rotates the local geometric transform by the specified amount. This method prepends the rotation to the transform.</summary>
		/// <param name="angle">The angle of rotation. </param>
		// Token: 0x06000EA2 RID: 3746 RVA: 0x000206A1 File Offset: 0x0001E8A1
		public void RotateTransform(float angle)
		{
			this.RotateTransform(angle, MatrixOrder.Prepend);
		}

		/// <summary>Rotates the local geometric transform by the specified amount in the specified order.</summary>
		/// <param name="angle">The angle of rotation. </param>
		/// <param name="order">A <see cref="T:System.Drawing.Drawing2D.MatrixOrder" /> that specifies whether to append or prepend the rotation matrix. </param>
		// Token: 0x06000EA3 RID: 3747 RVA: 0x000206AB File Offset: 0x0001E8AB
		public void RotateTransform(float angle, MatrixOrder order)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipRotateLineTransform(base.NativeBrush, angle, order));
		}

		/// <summary>Scales the local geometric transform by the specified amounts. This method prepends the scaling matrix to the transform.</summary>
		/// <param name="sx">The amount by which to scale the transform in the x-axis direction. </param>
		/// <param name="sy">The amount by which to scale the transform in the y-axis direction. </param>
		// Token: 0x06000EA4 RID: 3748 RVA: 0x000206BF File Offset: 0x0001E8BF
		public void ScaleTransform(float sx, float sy)
		{
			this.ScaleTransform(sx, sy, MatrixOrder.Prepend);
		}

		/// <summary>Scales the local geometric transform by the specified amounts in the specified order.</summary>
		/// <param name="sx">The amount by which to scale the transform in the x-axis direction. </param>
		/// <param name="sy">The amount by which to scale the transform in the y-axis direction. </param>
		/// <param name="order">A <see cref="T:System.Drawing.Drawing2D.MatrixOrder" /> that specifies whether to append or prepend the scaling matrix. </param>
		// Token: 0x06000EA5 RID: 3749 RVA: 0x000206CA File Offset: 0x0001E8CA
		public void ScaleTransform(float sx, float sy, MatrixOrder order)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipScaleLineTransform(base.NativeBrush, sx, sy, order));
		}

		/// <summary>Creates a linear gradient with a center color and a linear falloff to a single color on both ends.</summary>
		/// <param name="focus">A value from 0 through 1 that specifies the center of the gradient (the point where the gradient is composed of only the ending color). </param>
		// Token: 0x06000EA6 RID: 3750 RVA: 0x000206DF File Offset: 0x0001E8DF
		public void SetBlendTriangularShape(float focus)
		{
			this.SetBlendTriangularShape(focus, 1f);
		}

		/// <summary>Creates a linear gradient with a center color and a linear falloff to a single color on both ends.</summary>
		/// <param name="focus">A value from 0 through 1 that specifies the center of the gradient (the point where the gradient is composed of only the ending color). </param>
		/// <param name="scale">A value from 0 through1 that specifies how fast the colors falloff from the starting color to <paramref name="focus" /> (ending color) </param>
		// Token: 0x06000EA7 RID: 3751 RVA: 0x000206ED File Offset: 0x0001E8ED
		public void SetBlendTriangularShape(float focus, float scale)
		{
			if (focus < 0f || focus > 1f || scale < 0f || scale > 1f)
			{
				throw new ArgumentException("Invalid parameter passed.");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipSetLineLinearBlend(base.NativeBrush, focus, scale));
		}

		/// <summary>Creates a gradient falloff based on a bell-shaped curve.</summary>
		/// <param name="focus">A value from 0 through 1 that specifies the center of the gradient (the point where the starting color and ending color are blended equally). </param>
		// Token: 0x06000EA8 RID: 3752 RVA: 0x0002072C File Offset: 0x0001E92C
		public void SetSigmaBellShape(float focus)
		{
			this.SetSigmaBellShape(focus, 1f);
		}

		/// <summary>Creates a gradient falloff based on a bell-shaped curve.</summary>
		/// <param name="focus">A value from 0 through 1 that specifies the center of the gradient (the point where the gradient is composed of only the ending color). </param>
		/// <param name="scale">A value from 0 through 1 that specifies how fast the colors falloff from the <paramref name="focus" />. </param>
		// Token: 0x06000EA9 RID: 3753 RVA: 0x0002073A File Offset: 0x0001E93A
		public void SetSigmaBellShape(float focus, float scale)
		{
			if (focus < 0f || focus > 1f || scale < 0f || scale > 1f)
			{
				throw new ArgumentException("Invalid parameter passed.");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipSetLineSigmaBlend(base.NativeBrush, focus, scale));
		}

		/// <summary>Translates the local geometric transform by the specified dimensions. This method prepends the translation to the transform.</summary>
		/// <param name="dx">The value of the translation in x. </param>
		/// <param name="dy">The value of the translation in y. </param>
		// Token: 0x06000EAA RID: 3754 RVA: 0x00020779 File Offset: 0x0001E979
		public void TranslateTransform(float dx, float dy)
		{
			this.TranslateTransform(dx, dy, MatrixOrder.Prepend);
		}

		/// <summary>Translates the local geometric transform by the specified dimensions in the specified order.</summary>
		/// <param name="dx">The value of the translation in x. </param>
		/// <param name="dy">The value of the translation in y. </param>
		/// <param name="order">The order (prepend or append) in which to apply the translation. </param>
		// Token: 0x06000EAB RID: 3755 RVA: 0x00020784 File Offset: 0x0001E984
		public void TranslateTransform(float dx, float dy, MatrixOrder order)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipTranslateLineTransform(base.NativeBrush, dx, dy, order));
		}

		/// <summary>Creates an exact copy of this <see cref="T:System.Drawing.Drawing2D.LinearGradientBrush" />.</summary>
		/// <returns>The <see cref="T:System.Drawing.Drawing2D.LinearGradientBrush" /> this method creates, cast as an object.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000EAC RID: 3756 RVA: 0x0002079C File Offset: 0x0001E99C
		public override object Clone()
		{
			IntPtr intPtr;
			GDIPlus.CheckStatus((Status)GDIPlus.GdipCloneBrush(new HandleRef(this, base.NativeBrush), out intPtr));
			return new LinearGradientBrush(intPtr);
		}

		// Token: 0x04000B58 RID: 2904
		private RectangleF rectangle;
	}
}
