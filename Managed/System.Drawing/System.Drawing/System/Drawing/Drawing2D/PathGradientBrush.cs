using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace System.Drawing.Drawing2D
{
	/// <summary>Encapsulates a <see cref="T:System.Drawing.Brush" /> object that fills the interior of a <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> object with a gradient. This class cannot be inherited.</summary>
	// Token: 0x02000153 RID: 339
	[MonoTODO("libgdiplus/cairo doesn't support path gradients - unless it can be mapped to a radial gradient")]
	public sealed class PathGradientBrush : Brush
	{
		// Token: 0x06000ED1 RID: 3793 RVA: 0x00020CEC File Offset: 0x0001EEEC
		internal PathGradientBrush(IntPtr native)
		{
			base.SetNativeBrush(native);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Drawing2D.PathGradientBrush" /> class with the specified path.</summary>
		/// <param name="path">The <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> that defines the area filled by this <see cref="T:System.Drawing.Drawing2D.PathGradientBrush" />. </param>
		// Token: 0x06000ED2 RID: 3794 RVA: 0x00020CFC File Offset: 0x0001EEFC
		public PathGradientBrush(GraphicsPath path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			IntPtr intPtr;
			GDIPlus.CheckStatus(GDIPlus.GdipCreatePathGradientFromPath(path.nativePath, out intPtr));
			base.SetNativeBrush(intPtr);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Drawing2D.PathGradientBrush" /> class with the specified points.</summary>
		/// <param name="points">An array of <see cref="T:System.Drawing.Point" /> structures that represents the points that make up the vertices of the path. </param>
		// Token: 0x06000ED3 RID: 3795 RVA: 0x00020D36 File Offset: 0x0001EF36
		public PathGradientBrush(Point[] points)
			: this(points, WrapMode.Clamp)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Drawing2D.PathGradientBrush" /> class with the specified points.</summary>
		/// <param name="points">An array of <see cref="T:System.Drawing.PointF" /> structures that represents the points that make up the vertices of the path. </param>
		// Token: 0x06000ED4 RID: 3796 RVA: 0x00020D40 File Offset: 0x0001EF40
		public PathGradientBrush(PointF[] points)
			: this(points, WrapMode.Clamp)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Drawing2D.PathGradientBrush" /> class with the specified points and wrap mode.</summary>
		/// <param name="points">An array of <see cref="T:System.Drawing.Point" /> structures that represents the points that make up the vertices of the path. </param>
		/// <param name="wrapMode">A <see cref="T:System.Drawing.Drawing2D.WrapMode" /> that specifies how fills drawn with this <see cref="T:System.Drawing.Drawing2D.PathGradientBrush" /> are tiled. </param>
		// Token: 0x06000ED5 RID: 3797 RVA: 0x00020D4C File Offset: 0x0001EF4C
		public PathGradientBrush(Point[] points, WrapMode wrapMode)
		{
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			if (wrapMode < WrapMode.Tile || wrapMode > WrapMode.Clamp)
			{
				throw new InvalidEnumArgumentException("WrapMode");
			}
			IntPtr intPtr;
			GDIPlus.CheckStatus(GDIPlus.GdipCreatePathGradientI(points, points.Length, wrapMode, out intPtr));
			base.SetNativeBrush(intPtr);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Drawing2D.PathGradientBrush" /> class with the specified points and wrap mode.</summary>
		/// <param name="points">An array of <see cref="T:System.Drawing.PointF" /> structures that represents the points that make up the vertices of the path. </param>
		/// <param name="wrapMode">A <see cref="T:System.Drawing.Drawing2D.WrapMode" /> that specifies how fills drawn with this <see cref="T:System.Drawing.Drawing2D.PathGradientBrush" /> are tiled. </param>
		// Token: 0x06000ED6 RID: 3798 RVA: 0x00020D98 File Offset: 0x0001EF98
		public PathGradientBrush(PointF[] points, WrapMode wrapMode)
		{
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			if (wrapMode < WrapMode.Tile || wrapMode > WrapMode.Clamp)
			{
				throw new InvalidEnumArgumentException("WrapMode");
			}
			IntPtr intPtr;
			GDIPlus.CheckStatus(GDIPlus.GdipCreatePathGradient(points, points.Length, wrapMode, out intPtr));
			base.SetNativeBrush(intPtr);
		}

		/// <summary>Gets or sets a <see cref="T:System.Drawing.Drawing2D.Blend" /> that specifies positions and factors that define a custom falloff for the gradient.</summary>
		/// <returns>A <see cref="T:System.Drawing.Drawing2D.Blend" /> that represents a custom falloff for the gradient.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x06000ED7 RID: 3799 RVA: 0x00020DE4 File Offset: 0x0001EFE4
		// (set) Token: 0x06000ED8 RID: 3800 RVA: 0x00020E38 File Offset: 0x0001F038
		public Blend Blend
		{
			get
			{
				int num;
				GDIPlus.CheckStatus(GDIPlus.GdipGetPathGradientBlendCount(base.NativeBrush, out num));
				float[] array = new float[num];
				float[] array2 = new float[num];
				GDIPlus.CheckStatus(GDIPlus.GdipGetPathGradientBlend(base.NativeBrush, array, array2, num));
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
				GDIPlus.CheckStatus(GDIPlus.GdipSetPathGradientBlend(base.NativeBrush, factors, positions, num));
			}
		}

		/// <summary>Gets or sets the color at the center of the path gradient.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the color at the center of the path gradient.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x06000ED9 RID: 3801 RVA: 0x00020EBC File Offset: 0x0001F0BC
		// (set) Token: 0x06000EDA RID: 3802 RVA: 0x00020EE1 File Offset: 0x0001F0E1
		public Color CenterColor
		{
			get
			{
				int num;
				GDIPlus.CheckStatus(GDIPlus.GdipGetPathGradientCenterColor(base.NativeBrush, out num));
				return Color.FromArgb(num);
			}
			set
			{
				GDIPlus.CheckStatus(GDIPlus.GdipSetPathGradientCenterColor(base.NativeBrush, value.ToArgb()));
			}
		}

		/// <summary>Gets or sets the center point of the path gradient.</summary>
		/// <returns>A <see cref="T:System.Drawing.PointF" /> that represents the center point of the path gradient.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x06000EDB RID: 3803 RVA: 0x00020EFC File Offset: 0x0001F0FC
		// (set) Token: 0x06000EDC RID: 3804 RVA: 0x00020F1C File Offset: 0x0001F11C
		public PointF CenterPoint
		{
			get
			{
				PointF pointF;
				GDIPlus.CheckStatus(GDIPlus.GdipGetPathGradientCenterPoint(base.NativeBrush, out pointF));
				return pointF;
			}
			set
			{
				PointF pointF = value;
				GDIPlus.CheckStatus(GDIPlus.GdipSetPathGradientCenterPoint(base.NativeBrush, ref pointF));
			}
		}

		/// <summary>Gets or sets the focus point for the gradient falloff.</summary>
		/// <returns>A <see cref="T:System.Drawing.PointF" /> that represents the focus point for the gradient falloff.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x06000EDD RID: 3805 RVA: 0x00020F40 File Offset: 0x0001F140
		// (set) Token: 0x06000EDE RID: 3806 RVA: 0x00020F68 File Offset: 0x0001F168
		public PointF FocusScales
		{
			get
			{
				float num;
				float num2;
				GDIPlus.CheckStatus(GDIPlus.GdipGetPathGradientFocusScales(base.NativeBrush, out num, out num2));
				return new PointF(num, num2);
			}
			set
			{
				GDIPlus.CheckStatus(GDIPlus.GdipSetPathGradientFocusScales(base.NativeBrush, value.X, value.Y));
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Drawing.Drawing2D.ColorBlend" /> that defines a multicolor linear gradient.</summary>
		/// <returns>A <see cref="T:System.Drawing.Drawing2D.ColorBlend" /> that defines a multicolor linear gradient.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x06000EDF RID: 3807 RVA: 0x00020F88 File Offset: 0x0001F188
		// (set) Token: 0x06000EE0 RID: 3808 RVA: 0x00021014 File Offset: 0x0001F214
		public ColorBlend InterpolationColors
		{
			get
			{
				int num;
				GDIPlus.CheckStatus(GDIPlus.GdipGetPathGradientPresetBlendCount(base.NativeBrush, out num));
				if (num < 1)
				{
					num = 1;
				}
				int[] array = new int[num];
				float[] array2 = new float[num];
				if (num > 1)
				{
					GDIPlus.CheckStatus(GDIPlus.GdipGetPathGradientPresetBlend(base.NativeBrush, array, array2, num));
				}
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
				GDIPlus.CheckStatus(GDIPlus.GdipSetPathGradientPresetBlend(base.NativeBrush, array, positions, num));
			}
		}

		/// <summary>Gets a bounding rectangle for this <see cref="T:System.Drawing.Drawing2D.PathGradientBrush" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.RectangleF" /> that represents a rectangular region that bounds the path this <see cref="T:System.Drawing.Drawing2D.PathGradientBrush" /> fills.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x06000EE1 RID: 3809 RVA: 0x000210C4 File Offset: 0x0001F2C4
		public RectangleF Rectangle
		{
			get
			{
				RectangleF rectangleF;
				GDIPlus.CheckStatus(GDIPlus.GdipGetPathGradientRect(base.NativeBrush, out rectangleF));
				return rectangleF;
			}
		}

		/// <summary>Gets or sets an array of colors that correspond to the points in the path this <see cref="T:System.Drawing.Drawing2D.PathGradientBrush" /> fills.</summary>
		/// <returns>An array of <see cref="T:System.Drawing.Color" /> structures that represents the colors associated with each point in the path this <see cref="T:System.Drawing.Drawing2D.PathGradientBrush" /> fills.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x06000EE2 RID: 3810 RVA: 0x000210E4 File Offset: 0x0001F2E4
		// (set) Token: 0x06000EE3 RID: 3811 RVA: 0x00021140 File Offset: 0x0001F340
		public Color[] SurroundColors
		{
			get
			{
				int num;
				GDIPlus.CheckStatus(GDIPlus.GdipGetPathGradientSurroundColorCount(base.NativeBrush, out num));
				int[] array = new int[num];
				GDIPlus.CheckStatus(GDIPlus.GdipGetPathGradientSurroundColorsWithCount(base.NativeBrush, array, ref num));
				Color[] array2 = new Color[num];
				for (int i = 0; i < num; i++)
				{
					array2[i] = Color.FromArgb(array[i]);
				}
				return array2;
			}
			set
			{
				int num = value.Length;
				int[] array = new int[num];
				for (int i = 0; i < num; i++)
				{
					array[i] = value[i].ToArgb();
				}
				GDIPlus.CheckStatus(GDIPlus.GdipSetPathGradientSurroundColorsWithCount(base.NativeBrush, array, ref num));
			}
		}

		/// <summary>Gets or sets a copy of the <see cref="T:System.Drawing.Drawing2D.Matrix" /> that defines a local geometric transform for this <see cref="T:System.Drawing.Drawing2D.PathGradientBrush" />.</summary>
		/// <returns>A copy of the <see cref="T:System.Drawing.Drawing2D.Matrix" /> that defines a geometric transform that applies only to fills drawn with this <see cref="T:System.Drawing.Drawing2D.PathGradientBrush" />.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x06000EE4 RID: 3812 RVA: 0x00021188 File Offset: 0x0001F388
		// (set) Token: 0x06000EE5 RID: 3813 RVA: 0x000211B2 File Offset: 0x0001F3B2
		public Matrix Transform
		{
			get
			{
				Matrix matrix = new Matrix();
				GDIPlus.CheckStatus(GDIPlus.GdipGetPathGradientTransform(base.NativeBrush, matrix.nativeMatrix));
				return matrix;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("Transform");
				}
				GDIPlus.CheckStatus(GDIPlus.GdipSetPathGradientTransform(base.NativeBrush, value.nativeMatrix));
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Drawing.Drawing2D.WrapMode" /> that indicates the wrap mode for this <see cref="T:System.Drawing.Drawing2D.PathGradientBrush" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Drawing2D.WrapMode" /> that specifies how fills drawn with this <see cref="T:System.Drawing.Drawing2D.PathGradientBrush" /> are tiled.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x06000EE6 RID: 3814 RVA: 0x000211D8 File Offset: 0x0001F3D8
		// (set) Token: 0x06000EE7 RID: 3815 RVA: 0x000211F8 File Offset: 0x0001F3F8
		public WrapMode WrapMode
		{
			get
			{
				WrapMode wrapMode;
				GDIPlus.CheckStatus(GDIPlus.GdipGetPathGradientWrapMode(base.NativeBrush, out wrapMode));
				return wrapMode;
			}
			set
			{
				if (value < WrapMode.Tile || value > WrapMode.Clamp)
				{
					throw new InvalidEnumArgumentException("WrapMode");
				}
				GDIPlus.CheckStatus(GDIPlus.GdipSetPathGradientWrapMode(base.NativeBrush, value));
			}
		}

		/// <summary>Updates the brush's transformation matrix with the product of brush's transformation matrix multiplied by another matrix.</summary>
		/// <param name="matrix">The <see cref="T:System.Drawing.Drawing2D.Matrix" /> that will be multiplied by the brush's current transformation matrix. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000EE8 RID: 3816 RVA: 0x0002121E File Offset: 0x0001F41E
		public void MultiplyTransform(Matrix matrix)
		{
			this.MultiplyTransform(matrix, MatrixOrder.Prepend);
		}

		/// <summary>Updates the brush's transformation matrix with the product of the brush's transformation matrix multiplied by another matrix.</summary>
		/// <param name="matrix">The <see cref="T:System.Drawing.Drawing2D.Matrix" /> that will be multiplied by the brush's current transformation matrix. </param>
		/// <param name="order">A <see cref="T:System.Drawing.Drawing2D.MatrixOrder" /> that specifies in which order to multiply the two matrices. </param>
		// Token: 0x06000EE9 RID: 3817 RVA: 0x00021228 File Offset: 0x0001F428
		public void MultiplyTransform(Matrix matrix, MatrixOrder order)
		{
			if (matrix == null)
			{
				throw new ArgumentNullException("matrix");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipMultiplyPathGradientTransform(base.NativeBrush, matrix.nativeMatrix, order));
		}

		/// <summary>Resets the <see cref="P:System.Drawing.Drawing2D.PathGradientBrush.Transform" /> property to identity.</summary>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000EEA RID: 3818 RVA: 0x0002124F File Offset: 0x0001F44F
		public void ResetTransform()
		{
			GDIPlus.CheckStatus(GDIPlus.GdipResetPathGradientTransform(base.NativeBrush));
		}

		/// <summary>Rotates the local geometric transform by the specified amount. This method prepends the rotation to the transform.</summary>
		/// <param name="angle">The angle (extent) of rotation. </param>
		// Token: 0x06000EEB RID: 3819 RVA: 0x00021261 File Offset: 0x0001F461
		public void RotateTransform(float angle)
		{
			this.RotateTransform(angle, MatrixOrder.Prepend);
		}

		/// <summary>Rotates the local geometric transform by the specified amount in the specified order.</summary>
		/// <param name="angle">The angle (extent) of rotation. </param>
		/// <param name="order">A <see cref="T:System.Drawing.Drawing2D.MatrixOrder" /> that specifies whether to append or prepend the rotation matrix. </param>
		// Token: 0x06000EEC RID: 3820 RVA: 0x0002126B File Offset: 0x0001F46B
		public void RotateTransform(float angle, MatrixOrder order)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipRotatePathGradientTransform(base.NativeBrush, angle, order));
		}

		/// <summary>Scales the local geometric transform by the specified amounts. This method prepends the scaling matrix to the transform.</summary>
		/// <param name="sx">The transform scale factor in the x-axis direction. </param>
		/// <param name="sy">The transform scale factor in the y-axis direction. </param>
		// Token: 0x06000EED RID: 3821 RVA: 0x0002127F File Offset: 0x0001F47F
		public void ScaleTransform(float sx, float sy)
		{
			this.ScaleTransform(sx, sy, MatrixOrder.Prepend);
		}

		/// <summary>Scales the local geometric transform by the specified amounts in the specified order.</summary>
		/// <param name="sx">The transform scale factor in the x-axis direction. </param>
		/// <param name="sy">The transform scale factor in the y-axis direction. </param>
		/// <param name="order">A <see cref="T:System.Drawing.Drawing2D.MatrixOrder" /> that specifies whether to append or prepend the scaling matrix. </param>
		// Token: 0x06000EEE RID: 3822 RVA: 0x0002128A File Offset: 0x0001F48A
		public void ScaleTransform(float sx, float sy, MatrixOrder order)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipScalePathGradientTransform(base.NativeBrush, sx, sy, order));
		}

		/// <summary>Creates a gradient with a center color and a linear falloff to one surrounding color.</summary>
		/// <param name="focus">A value from 0 through 1 that specifies where, along any radial from the center of the path to the path's boundary, the center color will be at its highest intensity. A value of 1 (the default) places the highest intensity at the center of the path. </param>
		// Token: 0x06000EEF RID: 3823 RVA: 0x0002129F File Offset: 0x0001F49F
		public void SetBlendTriangularShape(float focus)
		{
			this.SetBlendTriangularShape(focus, 1f);
		}

		/// <summary>Creates a gradient with a center color and a linear falloff to each surrounding color.</summary>
		/// <param name="focus">A value from 0 through 1 that specifies where, along any radial from the center of the path to the path's boundary, the center color will be at its highest intensity. A value of 1 (the default) places the highest intensity at the center of the path. </param>
		/// <param name="scale">A value from 0 through 1 that specifies the maximum intensity of the center color that gets blended with the boundary color. A value of 1 causes the highest possible intensity of the center color, and it is the default value. </param>
		// Token: 0x06000EF0 RID: 3824 RVA: 0x000212AD File Offset: 0x0001F4AD
		public void SetBlendTriangularShape(float focus, float scale)
		{
			if (focus < 0f || focus > 1f || scale < 0f || scale > 1f)
			{
				throw new ArgumentException("Invalid parameter passed.");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipSetPathGradientLinearBlend(base.NativeBrush, focus, scale));
		}

		/// <summary>Creates a gradient brush that changes color starting from the center of the path outward to the path's boundary. The transition from one color to another is based on a bell-shaped curve.</summary>
		/// <param name="focus">A value from 0 through 1 that specifies where, along any radial from the center of the path to the path's boundary, the center color will be at its highest intensity. A value of 1 (the default) places the highest intensity at the center of the path. </param>
		// Token: 0x06000EF1 RID: 3825 RVA: 0x000212EC File Offset: 0x0001F4EC
		public void SetSigmaBellShape(float focus)
		{
			this.SetSigmaBellShape(focus, 1f);
		}

		/// <summary>Creates a gradient brush that changes color starting from the center of the path outward to the path's boundary. The transition from one color to another is based on a bell-shaped curve.</summary>
		/// <param name="focus">A value from 0 through 1 that specifies where, along any radial from the center of the path to the path's boundary, the center color will be at its highest intensity. A value of 1 (the default) places the highest intensity at the center of the path. </param>
		/// <param name="scale">A value from 0 through 1 that specifies the maximum intensity of the center color that gets blended with the boundary color. A value of 1 causes the highest possible intensity of the center color, and it is the default value. </param>
		// Token: 0x06000EF2 RID: 3826 RVA: 0x000212FA File Offset: 0x0001F4FA
		public void SetSigmaBellShape(float focus, float scale)
		{
			if (focus < 0f || focus > 1f || scale < 0f || scale > 1f)
			{
				throw new ArgumentException("Invalid parameter passed.");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipSetPathGradientSigmaBlend(base.NativeBrush, focus, scale));
		}

		/// <summary>Applies the specified translation to the local geometric transform. This method prepends the translation to the transform.</summary>
		/// <param name="dx">The value of the translation in x. </param>
		/// <param name="dy">The value of the translation in y. </param>
		// Token: 0x06000EF3 RID: 3827 RVA: 0x00021339 File Offset: 0x0001F539
		public void TranslateTransform(float dx, float dy)
		{
			this.TranslateTransform(dx, dy, MatrixOrder.Prepend);
		}

		/// <summary>Applies the specified translation to the local geometric transform in the specified order.</summary>
		/// <param name="dx">The value of the translation in x. </param>
		/// <param name="dy">The value of the translation in y. </param>
		/// <param name="order">The order (prepend or append) in which to apply the translation. </param>
		// Token: 0x06000EF4 RID: 3828 RVA: 0x00021344 File Offset: 0x0001F544
		public void TranslateTransform(float dx, float dy, MatrixOrder order)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipTranslatePathGradientTransform(base.NativeBrush, dx, dy, order));
		}

		/// <summary>Creates an exact copy of this <see cref="T:System.Drawing.Drawing2D.PathGradientBrush" />.</summary>
		/// <returns>The <see cref="T:System.Drawing.Drawing2D.PathGradientBrush" /> this method creates, cast as an object.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000EF5 RID: 3829 RVA: 0x0002135C File Offset: 0x0001F55C
		public override object Clone()
		{
			IntPtr intPtr;
			GDIPlus.CheckStatus((Status)GDIPlus.GdipCloneBrush(new HandleRef(this, base.NativeBrush), out intPtr));
			return new PathGradientBrush(intPtr);
		}
	}
}
