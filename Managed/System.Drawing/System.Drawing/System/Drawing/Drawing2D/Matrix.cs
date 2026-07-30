using System;
using System.Runtime.InteropServices;

namespace System.Drawing.Drawing2D
{
	/// <summary>Encapsulates a 3-by-3 affine matrix that represents a geometric transform. This class cannot be inherited.</summary>
	// Token: 0x02000152 RID: 338
	public sealed class Matrix : MarshalByRefObject, IDisposable
	{
		// Token: 0x06000EAD RID: 3757 RVA: 0x000207C7 File Offset: 0x0001E9C7
		internal Matrix(IntPtr ptr)
		{
			this.nativeMatrix = ptr;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Drawing2D.Matrix" /> class as the identity matrix.</summary>
		// Token: 0x06000EAE RID: 3758 RVA: 0x000207D6 File Offset: 0x0001E9D6
		public Matrix()
		{
			GDIPlus.CheckStatus(GDIPlus.GdipCreateMatrix(out this.nativeMatrix));
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Drawing2D.Matrix" /> class to the geometric transform defined by the specified rectangle and array of points.</summary>
		/// <param name="rect">A <see cref="T:System.Drawing.Rectangle" /> structure that represents the rectangle to be transformed. </param>
		/// <param name="plgpts">An array of three <see cref="T:System.Drawing.Point" /> structures that represents the points of a parallelogram to which the upper-left, upper-right, and lower-left corners of the rectangle is to be transformed. The lower-right corner of the parallelogram is implied by the first three corners. </param>
		// Token: 0x06000EAF RID: 3759 RVA: 0x000207EE File Offset: 0x0001E9EE
		public Matrix(Rectangle rect, Point[] plgpts)
		{
			if (plgpts == null)
			{
				throw new ArgumentNullException("plgpts");
			}
			if (plgpts.Length != 3)
			{
				throw new ArgumentException("plgpts");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipCreateMatrix3I(ref rect, plgpts, out this.nativeMatrix));
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Drawing2D.Matrix" /> class to the geometric transform defined by the specified rectangle and array of points.</summary>
		/// <param name="rect">A <see cref="T:System.Drawing.RectangleF" /> structure that represents the rectangle to be transformed. </param>
		/// <param name="plgpts">An array of three <see cref="T:System.Drawing.PointF" /> structures that represents the points of a parallelogram to which the upper-left, upper-right, and lower-left corners of the rectangle is to be transformed. The lower-right corner of the parallelogram is implied by the first three corners. </param>
		// Token: 0x06000EB0 RID: 3760 RVA: 0x00020828 File Offset: 0x0001EA28
		public Matrix(RectangleF rect, PointF[] plgpts)
		{
			if (plgpts == null)
			{
				throw new ArgumentNullException("plgpts");
			}
			if (plgpts.Length != 3)
			{
				throw new ArgumentException("plgpts");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipCreateMatrix3(ref rect, plgpts, out this.nativeMatrix));
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Drawing2D.Matrix" /> class with the specified elements.</summary>
		/// <param name="m11">The value in the first row and first column of the new <see cref="T:System.Drawing.Drawing2D.Matrix" />. </param>
		/// <param name="m12">The value in the first row and second column of the new <see cref="T:System.Drawing.Drawing2D.Matrix" />. </param>
		/// <param name="m21">The value in the second row and first column of the new <see cref="T:System.Drawing.Drawing2D.Matrix" />. </param>
		/// <param name="m22">The value in the second row and second column of the new <see cref="T:System.Drawing.Drawing2D.Matrix" />. </param>
		/// <param name="dx">The value in the third row and first column of the new <see cref="T:System.Drawing.Drawing2D.Matrix" />. </param>
		/// <param name="dy">The value in the third row and second column of the new <see cref="T:System.Drawing.Drawing2D.Matrix" />. </param>
		// Token: 0x06000EB1 RID: 3761 RVA: 0x00020862 File Offset: 0x0001EA62
		public Matrix(float m11, float m12, float m21, float m22, float dx, float dy)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipCreateMatrix2(m11, m12, m21, m22, dx, dy, out this.nativeMatrix));
		}

		/// <summary>Gets an array of floating-point values that represents the elements of this <see cref="T:System.Drawing.Drawing2D.Matrix" />.</summary>
		/// <returns>An array of floating-point values that represents the elements of this <see cref="T:System.Drawing.Drawing2D.Matrix" />.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170003DA RID: 986
		// (get) Token: 0x06000EB2 RID: 3762 RVA: 0x00020884 File Offset: 0x0001EA84
		public float[] Elements
		{
			get
			{
				float[] array = new float[6];
				IntPtr intPtr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(float)) * 6);
				try
				{
					GDIPlus.CheckStatus(GDIPlus.GdipGetMatrixElements(this.nativeMatrix, intPtr));
					Marshal.Copy(intPtr, array, 0, 6);
				}
				finally
				{
					Marshal.FreeHGlobal(intPtr);
				}
				return array;
			}
		}

		/// <summary>Gets a value indicating whether this <see cref="T:System.Drawing.Drawing2D.Matrix" /> is the identity matrix.</summary>
		/// <returns>This property is true if this <see cref="T:System.Drawing.Drawing2D.Matrix" /> is identity; otherwise, false.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170003DB RID: 987
		// (get) Token: 0x06000EB3 RID: 3763 RVA: 0x000208E4 File Offset: 0x0001EAE4
		public bool IsIdentity
		{
			get
			{
				bool flag;
				GDIPlus.CheckStatus(GDIPlus.GdipIsMatrixIdentity(this.nativeMatrix, out flag));
				return flag;
			}
		}

		/// <summary>Gets a value indicating whether this <see cref="T:System.Drawing.Drawing2D.Matrix" /> is invertible.</summary>
		/// <returns>This property is true if this <see cref="T:System.Drawing.Drawing2D.Matrix" /> is invertible; otherwise, false.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170003DC RID: 988
		// (get) Token: 0x06000EB4 RID: 3764 RVA: 0x00020904 File Offset: 0x0001EB04
		public bool IsInvertible
		{
			get
			{
				bool flag;
				GDIPlus.CheckStatus(GDIPlus.GdipIsMatrixInvertible(this.nativeMatrix, out flag));
				return flag;
			}
		}

		/// <summary>Gets the x translation value (the dx value, or the element in the third row and first column) of this <see cref="T:System.Drawing.Drawing2D.Matrix" />.</summary>
		/// <returns>The x translation value of this <see cref="T:System.Drawing.Drawing2D.Matrix" />.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170003DD RID: 989
		// (get) Token: 0x06000EB5 RID: 3765 RVA: 0x00020924 File Offset: 0x0001EB24
		public float OffsetX
		{
			get
			{
				return this.Elements[4];
			}
		}

		/// <summary>Gets the y translation value (the dy value, or the element in the third row and second column) of this <see cref="T:System.Drawing.Drawing2D.Matrix" />.</summary>
		/// <returns>The y translation value of this <see cref="T:System.Drawing.Drawing2D.Matrix" />.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170003DE RID: 990
		// (get) Token: 0x06000EB6 RID: 3766 RVA: 0x0002092E File Offset: 0x0001EB2E
		public float OffsetY
		{
			get
			{
				return this.Elements[5];
			}
		}

		/// <summary>Creates an exact copy of this <see cref="T:System.Drawing.Drawing2D.Matrix" />.</summary>
		/// <returns>The <see cref="T:System.Drawing.Drawing2D.Matrix" /> that this method creates.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000EB7 RID: 3767 RVA: 0x00020938 File Offset: 0x0001EB38
		public Matrix Clone()
		{
			IntPtr intPtr;
			GDIPlus.CheckStatus(GDIPlus.GdipCloneMatrix(this.nativeMatrix, out intPtr));
			return new Matrix(intPtr);
		}

		/// <summary>Releases all resources used by this <see cref="T:System.Drawing.Drawing2D.Matrix" />.</summary>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06000EB8 RID: 3768 RVA: 0x0002095D File Offset: 0x0001EB5D
		public void Dispose()
		{
			if (this.nativeMatrix != IntPtr.Zero)
			{
				GDIPlus.CheckStatus(GDIPlus.GdipDeleteMatrix(this.nativeMatrix));
				this.nativeMatrix = IntPtr.Zero;
			}
			GC.SuppressFinalize(this);
		}

		/// <summary>Tests whether the specified object is a <see cref="T:System.Drawing.Drawing2D.Matrix" /> and is identical to this <see cref="T:System.Drawing.Drawing2D.Matrix" />.</summary>
		/// <returns>This method returns true if <paramref name="obj" /> is the specified <see cref="T:System.Drawing.Drawing2D.Matrix" /> identical to this <see cref="T:System.Drawing.Drawing2D.Matrix" />; otherwise, false.</returns>
		/// <param name="obj">The object to test. </param>
		// Token: 0x06000EB9 RID: 3769 RVA: 0x00020994 File Offset: 0x0001EB94
		public override bool Equals(object obj)
		{
			Matrix matrix = obj as Matrix;
			if (matrix != null)
			{
				bool flag;
				GDIPlus.CheckStatus(GDIPlus.GdipIsMatrixEqual(this.nativeMatrix, matrix.nativeMatrix, out flag));
				return flag;
			}
			return false;
		}

		// Token: 0x06000EBA RID: 3770 RVA: 0x000209C8 File Offset: 0x0001EBC8
		~Matrix()
		{
			this.Dispose();
		}

		/// <summary>Returns a hash code.</summary>
		/// <returns>The hash code for this <see cref="T:System.Drawing.Drawing2D.Matrix" />.</returns>
		// Token: 0x06000EBB RID: 3771 RVA: 0x0000277B File Offset: 0x0000097B
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Inverts this <see cref="T:System.Drawing.Drawing2D.Matrix" />, if it is invertible.</summary>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000EBC RID: 3772 RVA: 0x000209F4 File Offset: 0x0001EBF4
		public void Invert()
		{
			GDIPlus.CheckStatus(GDIPlus.GdipInvertMatrix(this.nativeMatrix));
		}

		/// <summary>Multiplies this <see cref="T:System.Drawing.Drawing2D.Matrix" /> by the matrix specified in the <paramref name="matrix" /> parameter, by prepending the specified <see cref="T:System.Drawing.Drawing2D.Matrix" />.</summary>
		/// <param name="matrix">The <see cref="T:System.Drawing.Drawing2D.Matrix" /> by which this <see cref="T:System.Drawing.Drawing2D.Matrix" /> is to be multiplied. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000EBD RID: 3773 RVA: 0x00020A06 File Offset: 0x0001EC06
		public void Multiply(Matrix matrix)
		{
			this.Multiply(matrix, MatrixOrder.Prepend);
		}

		/// <summary>Multiplies this <see cref="T:System.Drawing.Drawing2D.Matrix" /> by the matrix specified in the <paramref name="matrix" /> parameter, and in the order specified in the <paramref name="order" /> parameter.</summary>
		/// <param name="matrix">The <see cref="T:System.Drawing.Drawing2D.Matrix" /> by which this <see cref="T:System.Drawing.Drawing2D.Matrix" /> is to be multiplied. </param>
		/// <param name="order">The <see cref="T:System.Drawing.Drawing2D.MatrixOrder" /> that represents the order of the multiplication. </param>
		// Token: 0x06000EBE RID: 3774 RVA: 0x00020A10 File Offset: 0x0001EC10
		public void Multiply(Matrix matrix, MatrixOrder order)
		{
			if (matrix == null)
			{
				throw new ArgumentNullException("matrix");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipMultiplyMatrix(this.nativeMatrix, matrix.nativeMatrix, order));
		}

		/// <summary>Resets this <see cref="T:System.Drawing.Drawing2D.Matrix" /> to have the elements of the identity matrix.</summary>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000EBF RID: 3775 RVA: 0x00020A37 File Offset: 0x0001EC37
		public void Reset()
		{
			GDIPlus.CheckStatus(GDIPlus.GdipSetMatrixElements(this.nativeMatrix, 1f, 0f, 0f, 1f, 0f, 0f));
		}

		/// <summary>Prepend to this <see cref="T:System.Drawing.Drawing2D.Matrix" /> a clockwise rotation, around the origin and by the specified angle.</summary>
		/// <param name="angle">The angle of the rotation, in degrees. </param>
		// Token: 0x06000EC0 RID: 3776 RVA: 0x00020A67 File Offset: 0x0001EC67
		public void Rotate(float angle)
		{
			this.Rotate(angle, MatrixOrder.Prepend);
		}

		/// <summary>Applies a clockwise rotation of an amount specified in the <paramref name="angle" /> parameter, around the origin (zero x and y coordinates) for this <see cref="T:System.Drawing.Drawing2D.Matrix" />.</summary>
		/// <param name="angle">The angle (extent) of the rotation, in degrees. </param>
		/// <param name="order">A <see cref="T:System.Drawing.Drawing2D.MatrixOrder" /> that specifies the order (append or prepend) in which the rotation is applied to this <see cref="T:System.Drawing.Drawing2D.Matrix" />. </param>
		// Token: 0x06000EC1 RID: 3777 RVA: 0x00020A71 File Offset: 0x0001EC71
		public void Rotate(float angle, MatrixOrder order)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipRotateMatrix(this.nativeMatrix, angle, order));
		}

		/// <summary>Applies a clockwise rotation to this <see cref="T:System.Drawing.Drawing2D.Matrix" /> around the point specified in the <paramref name="point" /> parameter, and by prepending the rotation.</summary>
		/// <param name="angle">The angle (extent) of the rotation, in degrees. </param>
		/// <param name="point">A <see cref="T:System.Drawing.PointF" /> that represents the center of the rotation. </param>
		// Token: 0x06000EC2 RID: 3778 RVA: 0x00020A85 File Offset: 0x0001EC85
		public void RotateAt(float angle, PointF point)
		{
			this.RotateAt(angle, point, MatrixOrder.Prepend);
		}

		/// <summary>Applies a clockwise rotation about the specified point to this <see cref="T:System.Drawing.Drawing2D.Matrix" /> in the specified order.</summary>
		/// <param name="angle">The angle of the rotation, in degrees. </param>
		/// <param name="point">A <see cref="T:System.Drawing.PointF" /> that represents the center of the rotation. </param>
		/// <param name="order">A <see cref="T:System.Drawing.Drawing2D.MatrixOrder" /> that specifies the order (append or prepend) in which the rotation is applied. </param>
		// Token: 0x06000EC3 RID: 3779 RVA: 0x00020A90 File Offset: 0x0001EC90
		public void RotateAt(float angle, PointF point, MatrixOrder order)
		{
			if (order < MatrixOrder.Prepend || order > MatrixOrder.Append)
			{
				throw new ArgumentException("order");
			}
			angle *= 0.017453292f;
			float num = (float)Math.Cos((double)angle);
			float num2 = (float)Math.Sin((double)angle);
			float num3 = -point.X * num + point.Y * num2 + point.X;
			float num4 = -point.X * num2 - point.Y * num + point.Y;
			float[] elements = this.Elements;
			Status status;
			if (order == MatrixOrder.Prepend)
			{
				status = GDIPlus.GdipSetMatrixElements(this.nativeMatrix, num * elements[0] + num2 * elements[2], num * elements[1] + num2 * elements[3], -num2 * elements[0] + num * elements[2], -num2 * elements[1] + num * elements[3], num3 * elements[0] + num4 * elements[2] + elements[4], num3 * elements[1] + num4 * elements[3] + elements[5]);
			}
			else
			{
				status = GDIPlus.GdipSetMatrixElements(this.nativeMatrix, elements[0] * num + elements[1] * -num2, elements[0] * num2 + elements[1] * num, elements[2] * num + elements[3] * -num2, elements[2] * num2 + elements[3] * num, elements[4] * num + elements[5] * -num2 + num3, elements[4] * num2 + elements[5] * num + num4);
			}
			GDIPlus.CheckStatus(status);
		}

		/// <summary>Applies the specified scale vector to this <see cref="T:System.Drawing.Drawing2D.Matrix" /> by prepending the scale vector.</summary>
		/// <param name="scaleX">The value by which to scale this <see cref="T:System.Drawing.Drawing2D.Matrix" /> in the x-axis direction. </param>
		/// <param name="scaleY">The value by which to scale this <see cref="T:System.Drawing.Drawing2D.Matrix" /> in the y-axis direction. </param>
		// Token: 0x06000EC4 RID: 3780 RVA: 0x00020BE2 File Offset: 0x0001EDE2
		public void Scale(float scaleX, float scaleY)
		{
			this.Scale(scaleX, scaleY, MatrixOrder.Prepend);
		}

		/// <summary>Applies the specified scale vector (<paramref name="scaleX" /> and <paramref name="scaleY" />) to this <see cref="T:System.Drawing.Drawing2D.Matrix" /> using the specified order.</summary>
		/// <param name="scaleX">The value by which to scale this <see cref="T:System.Drawing.Drawing2D.Matrix" /> in the x-axis direction. </param>
		/// <param name="scaleY">The value by which to scale this <see cref="T:System.Drawing.Drawing2D.Matrix" /> in the y-axis direction. </param>
		/// <param name="order">A <see cref="T:System.Drawing.Drawing2D.MatrixOrder" /> that specifies the order (append or prepend) in which the scale vector is applied to this <see cref="T:System.Drawing.Drawing2D.Matrix" />. </param>
		// Token: 0x06000EC5 RID: 3781 RVA: 0x00020BED File Offset: 0x0001EDED
		public void Scale(float scaleX, float scaleY, MatrixOrder order)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipScaleMatrix(this.nativeMatrix, scaleX, scaleY, order));
		}

		/// <summary>Applies the specified shear vector to this <see cref="T:System.Drawing.Drawing2D.Matrix" /> by prepending the shear transformation.</summary>
		/// <param name="shearX">The horizontal shear factor. </param>
		/// <param name="shearY">The vertical shear factor. </param>
		// Token: 0x06000EC6 RID: 3782 RVA: 0x00020C02 File Offset: 0x0001EE02
		public void Shear(float shearX, float shearY)
		{
			this.Shear(shearX, shearY, MatrixOrder.Prepend);
		}

		/// <summary>Applies the specified shear vector to this <see cref="T:System.Drawing.Drawing2D.Matrix" /> in the specified order.</summary>
		/// <param name="shearX">The horizontal shear factor. </param>
		/// <param name="shearY">The vertical shear factor. </param>
		/// <param name="order">A <see cref="T:System.Drawing.Drawing2D.MatrixOrder" /> that specifies the order (append or prepend) in which the shear is applied. </param>
		// Token: 0x06000EC7 RID: 3783 RVA: 0x00020C0D File Offset: 0x0001EE0D
		public void Shear(float shearX, float shearY, MatrixOrder order)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipShearMatrix(this.nativeMatrix, shearX, shearY, order));
		}

		/// <summary>Applies the geometric transform represented by this <see cref="T:System.Drawing.Drawing2D.Matrix" /> to a specified array of points.</summary>
		/// <param name="pts">An array of <see cref="T:System.Drawing.Point" /> structures that represents the points to transform. </param>
		// Token: 0x06000EC8 RID: 3784 RVA: 0x00020C22 File Offset: 0x0001EE22
		public void TransformPoints(Point[] pts)
		{
			if (pts == null)
			{
				throw new ArgumentNullException("pts");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipTransformMatrixPointsI(this.nativeMatrix, pts, pts.Length));
		}

		/// <summary>Applies the geometric transform represented by this <see cref="T:System.Drawing.Drawing2D.Matrix" /> to a specified array of points.</summary>
		/// <param name="pts">An array of <see cref="T:System.Drawing.PointF" /> structures that represents the points to transform. </param>
		// Token: 0x06000EC9 RID: 3785 RVA: 0x00020C46 File Offset: 0x0001EE46
		public void TransformPoints(PointF[] pts)
		{
			if (pts == null)
			{
				throw new ArgumentNullException("pts");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipTransformMatrixPoints(this.nativeMatrix, pts, pts.Length));
		}

		/// <summary>Applies only the scale and rotate components of this <see cref="T:System.Drawing.Drawing2D.Matrix" /> to the specified array of points.</summary>
		/// <param name="pts">An array of <see cref="T:System.Drawing.Point" /> structures that represents the points to transform. </param>
		// Token: 0x06000ECA RID: 3786 RVA: 0x00020C6A File Offset: 0x0001EE6A
		public void TransformVectors(Point[] pts)
		{
			if (pts == null)
			{
				throw new ArgumentNullException("pts");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipVectorTransformMatrixPointsI(this.nativeMatrix, pts, pts.Length));
		}

		/// <summary>Multiplies each vector in an array by the matrix. The translation elements of this matrix (third row) are ignored.</summary>
		/// <param name="pts">An array of <see cref="T:System.Drawing.Point" /> structures that represents the points to transform. </param>
		// Token: 0x06000ECB RID: 3787 RVA: 0x00020C8E File Offset: 0x0001EE8E
		public void TransformVectors(PointF[] pts)
		{
			if (pts == null)
			{
				throw new ArgumentNullException("pts");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipVectorTransformMatrixPoints(this.nativeMatrix, pts, pts.Length));
		}

		/// <summary>Applies the specified translation vector (<paramref name="offsetX" /> and <paramref name="offsetY" />) to this <see cref="T:System.Drawing.Drawing2D.Matrix" /> by prepending the translation vector.</summary>
		/// <param name="offsetX">The x value by which to translate this <see cref="T:System.Drawing.Drawing2D.Matrix" />. </param>
		/// <param name="offsetY">The y value by which to translate this <see cref="T:System.Drawing.Drawing2D.Matrix" />. </param>
		// Token: 0x06000ECC RID: 3788 RVA: 0x00020CB2 File Offset: 0x0001EEB2
		public void Translate(float offsetX, float offsetY)
		{
			this.Translate(offsetX, offsetY, MatrixOrder.Prepend);
		}

		/// <summary>Applies the specified translation vector to this <see cref="T:System.Drawing.Drawing2D.Matrix" /> in the specified order.</summary>
		/// <param name="offsetX">The x value by which to translate this <see cref="T:System.Drawing.Drawing2D.Matrix" />. </param>
		/// <param name="offsetY">The y value by which to translate this <see cref="T:System.Drawing.Drawing2D.Matrix" />. </param>
		/// <param name="order">A <see cref="T:System.Drawing.Drawing2D.MatrixOrder" /> that specifies the order (append or prepend) in which the translation is applied to this <see cref="T:System.Drawing.Drawing2D.Matrix" />. </param>
		// Token: 0x06000ECD RID: 3789 RVA: 0x00020CBD File Offset: 0x0001EEBD
		public void Translate(float offsetX, float offsetY, MatrixOrder order)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipTranslateMatrix(this.nativeMatrix, offsetX, offsetY, order));
		}

		/// <summary>Multiplies each vector in an array by the matrix. The translation elements of this matrix (third row) are ignored.</summary>
		/// <param name="pts">An array of <see cref="T:System.Drawing.Point" /> structures that represents the points to transform.</param>
		// Token: 0x06000ECE RID: 3790 RVA: 0x00020CD2 File Offset: 0x0001EED2
		public void VectorTransformPoints(Point[] pts)
		{
			this.TransformVectors(pts);
		}

		// Token: 0x170003DF RID: 991
		// (get) Token: 0x06000ECF RID: 3791 RVA: 0x00020CDB File Offset: 0x0001EEDB
		// (set) Token: 0x06000ED0 RID: 3792 RVA: 0x00020CE3 File Offset: 0x0001EEE3
		internal IntPtr NativeObject
		{
			get
			{
				return this.nativeMatrix;
			}
			set
			{
				this.nativeMatrix = value;
			}
		}

		// Token: 0x04000B59 RID: 2905
		internal IntPtr nativeMatrix;
	}
}
