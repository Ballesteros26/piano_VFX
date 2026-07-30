using System;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Unity;

namespace System.Drawing
{
	/// <summary>Encapsulates a GDI+ drawing surface. This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000065 RID: 101
	public sealed class Graphics : MarshalByRefObject, IDisposable, IDeviceContext
	{
		// Token: 0x0600039A RID: 922 RVA: 0x00009478 File Offset: 0x00007678
		internal Graphics(IntPtr nativeGraphics)
		{
			this.nativeObject = IntPtr.Zero;
			base..ctor();
			this.nativeObject = nativeGraphics;
		}

		// Token: 0x0600039B RID: 923 RVA: 0x00009494 File Offset: 0x00007694
		~Graphics()
		{
			this.Dispose();
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x0600039C RID: 924 RVA: 0x000094C0 File Offset: 0x000076C0
		internal static float systemDpiX
		{
			get
			{
				if (Graphics.defDpiX == 0f)
				{
					Graphics graphics = Graphics.FromImage(new Bitmap(1, 1));
					Graphics.defDpiX = graphics.DpiX;
					Graphics.defDpiY = graphics.DpiY;
				}
				return Graphics.defDpiX;
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x0600039D RID: 925 RVA: 0x000094F4 File Offset: 0x000076F4
		internal static float systemDpiY
		{
			get
			{
				if (Graphics.defDpiY == 0f)
				{
					Graphics graphics = Graphics.FromImage(new Bitmap(1, 1));
					Graphics.defDpiX = graphics.DpiX;
					Graphics.defDpiY = graphics.DpiY;
				}
				return Graphics.defDpiY;
			}
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x0600039E RID: 926 RVA: 0x00009528 File Offset: 0x00007728
		internal IntPtr NativeGraphics
		{
			get
			{
				return this.nativeObject;
			}
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x0600039F RID: 927 RVA: 0x00009528 File Offset: 0x00007728
		// (set) Token: 0x060003A0 RID: 928 RVA: 0x00009530 File Offset: 0x00007730
		internal IntPtr NativeObject
		{
			get
			{
				return this.nativeObject;
			}
			set
			{
				this.nativeObject = value;
			}
		}

		/// <summary>Adds a comment to the current <see cref="T:System.Drawing.Imaging.Metafile" />.</summary>
		/// <param name="data">Array of bytes that contains the comment. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060003A1 RID: 929 RVA: 0x00005902 File Offset: 0x00003B02
		[MonoTODO("Metafiles, both WMF and EMF formats, aren't supported.")]
		public void AddMetafileComment(byte[] data)
		{
			throw new NotImplementedException();
		}

		/// <summary>Saves a graphics container with the current state of this <see cref="T:System.Drawing.Graphics" /> and opens and uses a new graphics container.</summary>
		/// <returns>This method returns a <see cref="T:System.Drawing.Drawing2D.GraphicsContainer" /> that represents the state of this <see cref="T:System.Drawing.Graphics" /> at the time of the method call.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060003A2 RID: 930 RVA: 0x0000953C File Offset: 0x0000773C
		public GraphicsContainer BeginContainer()
		{
			uint num;
			GDIPlus.CheckStatus(GDIPlus.GdipBeginContainer2(this.nativeObject, out num));
			return new GraphicsContainer(num);
		}

		/// <summary>Saves a graphics container with the current state of this <see cref="T:System.Drawing.Graphics" /> and opens and uses a new graphics container with the specified scale transformation.</summary>
		/// <returns>This method returns a <see cref="T:System.Drawing.Drawing2D.GraphicsContainer" /> that represents the state of this <see cref="T:System.Drawing.Graphics" /> at the time of the method call.</returns>
		/// <param name="dstrect">
		///   <see cref="T:System.Drawing.Rectangle" /> structure that, together with the <paramref name="srcrect" /> parameter, specifies a scale transformation for the container. </param>
		/// <param name="srcrect">
		///   <see cref="T:System.Drawing.Rectangle" /> structure that, together with the <paramref name="dstrect" /> parameter, specifies a scale transformation for the container. </param>
		/// <param name="unit">Member of the <see cref="T:System.Drawing.GraphicsUnit" /> enumeration that specifies the unit of measure for the container. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060003A3 RID: 931 RVA: 0x00009564 File Offset: 0x00007764
		[MonoTODO("The rectangles and unit parameters aren't supported in libgdiplus")]
		public GraphicsContainer BeginContainer(Rectangle dstrect, Rectangle srcrect, GraphicsUnit unit)
		{
			uint num;
			GDIPlus.CheckStatus(GDIPlus.GdipBeginContainerI(this.nativeObject, ref dstrect, ref srcrect, unit, out num));
			return new GraphicsContainer(num);
		}

		/// <summary>Saves a graphics container with the current state of this <see cref="T:System.Drawing.Graphics" /> and opens and uses a new graphics container with the specified scale transformation.</summary>
		/// <returns>This method returns a <see cref="T:System.Drawing.Drawing2D.GraphicsContainer" /> that represents the state of this <see cref="T:System.Drawing.Graphics" /> at the time of the method call.</returns>
		/// <param name="dstrect">
		///   <see cref="T:System.Drawing.RectangleF" /> structure that, together with the <paramref name="srcrect" /> parameter, specifies a scale transformation for the new graphics container. </param>
		/// <param name="srcrect">
		///   <see cref="T:System.Drawing.RectangleF" /> structure that, together with the <paramref name="dstrect" /> parameter, specifies a scale transformation for the new graphics container. </param>
		/// <param name="unit">Member of the <see cref="T:System.Drawing.GraphicsUnit" /> enumeration that specifies the unit of measure for the container. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060003A4 RID: 932 RVA: 0x00009590 File Offset: 0x00007790
		[MonoTODO("The rectangles and unit parameters aren't supported in libgdiplus")]
		public GraphicsContainer BeginContainer(RectangleF dstrect, RectangleF srcrect, GraphicsUnit unit)
		{
			uint num;
			GDIPlus.CheckStatus(GDIPlus.GdipBeginContainer(this.nativeObject, ref dstrect, ref srcrect, unit, out num));
			return new GraphicsContainer(num);
		}

		/// <summary>Clears the entire drawing surface and fills it with the specified background color.</summary>
		/// <param name="color">
		///   <see cref="T:System.Drawing.Color" /> structure that represents the background color of the drawing surface. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060003A5 RID: 933 RVA: 0x000095BA File Offset: 0x000077BA
		public void Clear(Color color)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipGraphicsClear(this.nativeObject, color.ToArgb()));
		}

		/// <summary>Performs a bit-block transfer of color data, corresponding to a rectangle of pixels, from the screen to the drawing surface of the <see cref="T:System.Drawing.Graphics" />.</summary>
		/// <param name="upperLeftSource">The point at the upper-left corner of the source rectangle.</param>
		/// <param name="upperLeftDestination">The point at the upper-left corner of the destination rectangle.</param>
		/// <param name="blockRegionSize">The size of the area to be transferred.</param>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The operation failed.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Window="AllWindows" />
		/// </PermissionSet>
		// Token: 0x060003A6 RID: 934 RVA: 0x000095D3 File Offset: 0x000077D3
		[MonoLimitation("Works on Win32 and on X11 (but not on Cocoa and Quartz)")]
		public void CopyFromScreen(Point upperLeftSource, Point upperLeftDestination, Size blockRegionSize)
		{
			this.CopyFromScreen(upperLeftSource.X, upperLeftSource.Y, upperLeftDestination.X, upperLeftDestination.Y, blockRegionSize, CopyPixelOperation.SourceCopy);
		}

		/// <summary>Performs a bit-block transfer of color data, corresponding to a rectangle of pixels, from the screen to the drawing surface of the <see cref="T:System.Drawing.Graphics" />.</summary>
		/// <param name="upperLeftSource">The point at the upper-left corner of the source rectangle.</param>
		/// <param name="upperLeftDestination">The point at the upper-left corner of the destination rectangle.</param>
		/// <param name="blockRegionSize">The size of the area to be transferred.</param>
		/// <param name="copyPixelOperation">One of the <see cref="T:System.Drawing.CopyPixelOperation" /> values.</param>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="copyPixelOperation" /> is not a member of <see cref="T:System.Drawing.CopyPixelOperation" />.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The operation failed.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Window="AllWindows" />
		/// </PermissionSet>
		// Token: 0x060003A7 RID: 935 RVA: 0x000095FD File Offset: 0x000077FD
		[MonoLimitation("Works on Win32 and (for CopyPixelOperation.SourceCopy only) on X11 but not on Cocoa and Quartz")]
		public void CopyFromScreen(Point upperLeftSource, Point upperLeftDestination, Size blockRegionSize, CopyPixelOperation copyPixelOperation)
		{
			this.CopyFromScreen(upperLeftSource.X, upperLeftSource.Y, upperLeftDestination.X, upperLeftDestination.Y, blockRegionSize, copyPixelOperation);
		}

		/// <summary>Performs a bit-block transfer of the color data, corresponding to a rectangle of pixels, from the screen to the drawing surface of the <see cref="T:System.Drawing.Graphics" />.</summary>
		/// <param name="sourceX">The x-coordinate of the point at the upper-left corner of the source rectangle.</param>
		/// <param name="sourceY">The y-coordinate of the point at the upper-left corner of the source rectangle.</param>
		/// <param name="destinationX">The x-coordinate of the point at the upper-left corner of the destination rectangle.</param>
		/// <param name="destinationY">The y-coordinate of the point at the upper-left corner of the destination rectangle.</param>
		/// <param name="blockRegionSize">The size of the area to be transferred.</param>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The operation failed.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Window="AllWindows" />
		/// </PermissionSet>
		// Token: 0x060003A8 RID: 936 RVA: 0x00009624 File Offset: 0x00007824
		[MonoLimitation("Works on Win32 and on X11 (but not on Cocoa and Quartz)")]
		public void CopyFromScreen(int sourceX, int sourceY, int destinationX, int destinationY, Size blockRegionSize)
		{
			this.CopyFromScreen(sourceX, sourceY, destinationX, destinationY, blockRegionSize, CopyPixelOperation.SourceCopy);
		}

		/// <summary>Performs a bit-block transfer of the color data, corresponding to a rectangle of pixels, from the screen to the drawing surface of the <see cref="T:System.Drawing.Graphics" />.</summary>
		/// <param name="sourceX">The x-coordinate of the point at the upper-left corner of the source rectangle.</param>
		/// <param name="sourceY">The y-coordinate of the point at the upper-left corner of the source rectangle</param>
		/// <param name="destinationX">The x-coordinate of the point at the upper-left corner of the destination rectangle.</param>
		/// <param name="destinationY">The y-coordinate of the point at the upper-left corner of the destination rectangle.</param>
		/// <param name="blockRegionSize">The size of the area to be transferred.</param>
		/// <param name="copyPixelOperation">One of the <see cref="T:System.Drawing.CopyPixelOperation" /> values.</param>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="copyPixelOperation" /> is not a member of <see cref="T:System.Drawing.CopyPixelOperation" />.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The operation failed.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Window="AllWindows" />
		/// </PermissionSet>
		// Token: 0x060003A9 RID: 937 RVA: 0x00009638 File Offset: 0x00007838
		[MonoLimitation("Works on Win32 and (for CopyPixelOperation.SourceCopy only) on X11 but not on Cocoa and Quartz")]
		public void CopyFromScreen(int sourceX, int sourceY, int destinationX, int destinationY, Size blockRegionSize, CopyPixelOperation copyPixelOperation)
		{
			if (!Enum.IsDefined(typeof(CopyPixelOperation), copyPixelOperation))
			{
				throw new InvalidEnumArgumentException(Locale.GetText("Enum argument value '{0}' is not valid for CopyPixelOperation", new object[] { copyPixelOperation }));
			}
			if (GDIPlus.UseX11Drawable)
			{
				this.CopyFromScreenX11(sourceX, sourceY, destinationX, destinationY, blockRegionSize, copyPixelOperation);
				return;
			}
			if (GDIPlus.UseCarbonDrawable)
			{
				this.CopyFromScreenMac(sourceX, sourceY, destinationX, destinationY, blockRegionSize, copyPixelOperation);
				return;
			}
			if (GDIPlus.UseCocoaDrawable)
			{
				this.CopyFromScreenMac(sourceX, sourceY, destinationX, destinationY, blockRegionSize, copyPixelOperation);
				return;
			}
			this.CopyFromScreenWin32(sourceX, sourceY, destinationX, destinationY, blockRegionSize, copyPixelOperation);
		}

		// Token: 0x060003AA RID: 938 RVA: 0x000096D4 File Offset: 0x000078D4
		private void CopyFromScreenWin32(int sourceX, int sourceY, int destinationX, int destinationY, Size blockRegionSize, CopyPixelOperation copyPixelOperation)
		{
			IntPtr dc = GDIPlus.GetDC(GDIPlus.GetDesktopWindow());
			IntPtr hdc = this.GetHdc();
			GDIPlus.BitBlt(hdc, destinationX, destinationY, blockRegionSize.Width, blockRegionSize.Height, dc, sourceX, sourceY, (int)copyPixelOperation);
			GDIPlus.ReleaseDC(IntPtr.Zero, dc);
			this.ReleaseHdc(hdc);
		}

		// Token: 0x060003AB RID: 939 RVA: 0x00005902 File Offset: 0x00003B02
		private void CopyFromScreenMac(int sourceX, int sourceY, int destinationX, int destinationY, Size blockRegionSize, CopyPixelOperation copyPixelOperation)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060003AC RID: 940 RVA: 0x00009724 File Offset: 0x00007924
		private void CopyFromScreenX11(int sourceX, int sourceY, int destinationX, int destinationY, Size blockRegionSize, CopyPixelOperation copyPixelOperation)
		{
			int num = -1;
			int num2 = 0;
			if (copyPixelOperation != CopyPixelOperation.SourceCopy)
			{
				throw new NotImplementedException("Operation not implemented under X11");
			}
			if (GDIPlus.Display == IntPtr.Zero)
			{
				GDIPlus.Display = GDIPlus.XOpenDisplay(IntPtr.Zero);
			}
			IntPtr intPtr = GDIPlus.XRootWindow(GDIPlus.Display, 0);
			IntPtr intPtr2 = GDIPlus.XDefaultVisual(GDIPlus.Display, 0);
			XVisualInfo xvisualInfo = default(XVisualInfo);
			xvisualInfo.visualid = GDIPlus.XVisualIDFromVisual(intPtr2);
			IntPtr intPtr3 = GDIPlus.XGetVisualInfo(GDIPlus.Display, 1, ref xvisualInfo, ref num2);
			xvisualInfo = (XVisualInfo)Marshal.PtrToStructure(intPtr3, typeof(XVisualInfo));
			IntPtr intPtr4 = GDIPlus.XGetImage(GDIPlus.Display, intPtr, sourceX, sourceY, blockRegionSize.Width, blockRegionSize.Height, num, 2);
			if (intPtr4 == IntPtr.Zero)
			{
				throw new InvalidOperationException(string.Format("XGetImage returned NULL when asked to for a {0}x{1} region block", blockRegionSize.Width, blockRegionSize.Height));
			}
			Bitmap bitmap = new Bitmap(blockRegionSize.Width, blockRegionSize.Height);
			int num3 = (int)xvisualInfo.red_mask;
			int num4 = (int)xvisualInfo.blue_mask;
			int num5 = (int)xvisualInfo.green_mask;
			for (int i = 0; i < blockRegionSize.Height; i++)
			{
				for (int j = 0; j < blockRegionSize.Width; j++)
				{
					int num6 = GDIPlus.XGetPixel(intPtr4, j, i);
					uint depth = xvisualInfo.depth;
					int num7;
					int num8;
					int num9;
					if (depth != 16U)
					{
						if (depth != 24U && depth != 32U)
						{
							throw new NotImplementedException(Locale.GetText("{0}bbp depth not supported.", new object[] { xvisualInfo.depth }));
						}
						num7 = ((num6 & num3) >> 16) & 255;
						num8 = ((num6 & num5) >> 8) & 255;
						num9 = num6 & num4 & 255;
					}
					else
					{
						num7 = ((num6 & num3) >> 8) & 255;
						num8 = ((num6 & num5) >> 3) & 255;
						num9 = ((num6 & num4) << 3) & 255;
					}
					bitmap.SetPixel(j, i, Color.FromArgb(255, num7, num8, num9));
				}
			}
			this.DrawImage(bitmap, destinationX, destinationY);
			bitmap.Dispose();
			GDIPlus.XDestroyImage(intPtr4);
			GDIPlus.XFree(intPtr3);
		}

		/// <summary>Releases all resources used by this <see cref="T:System.Drawing.Graphics" />.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060003AD RID: 941 RVA: 0x00009970 File Offset: 0x00007B70
		public void Dispose()
		{
			if (!this.disposed)
			{
				if (GDIPlus.UseCarbonDrawable || GDIPlus.UseCocoaDrawable)
				{
					this.Flush();
					if (this.maccontext != null)
					{
						this.maccontext.Release();
					}
				}
				Status status = GDIPlus.GdipDeleteGraphics(this.nativeObject);
				this.nativeObject = IntPtr.Zero;
				GDIPlus.CheckStatus(status);
				this.disposed = true;
			}
			GC.SuppressFinalize(this);
		}

		/// <summary>Draws an arc representing a portion of an ellipse specified by a <see cref="T:System.Drawing.Rectangle" /> structure.</summary>
		/// <param name="pen">
		///   <see cref="T:System.Drawing.Pen" /> that determines the color, width, and style of the arc. </param>
		/// <param name="rect">
		///   <see cref="T:System.Drawing.RectangleF" /> structure that defines the boundaries of the ellipse. </param>
		/// <param name="startAngle">Angle in degrees measured clockwise from the x-axis to the starting point of the arc. </param>
		/// <param name="sweepAngle">Angle in degrees measured clockwise from the <paramref name="startAngle" /> parameter to ending point of the arc. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pen" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060003AE RID: 942 RVA: 0x000099D4 File Offset: 0x00007BD4
		public void DrawArc(Pen pen, Rectangle rect, float startAngle, float sweepAngle)
		{
			this.DrawArc(pen, (float)rect.X, (float)rect.Y, (float)rect.Width, (float)rect.Height, startAngle, sweepAngle);
		}

		/// <summary>Draws an arc representing a portion of an ellipse specified by a <see cref="T:System.Drawing.RectangleF" /> structure.</summary>
		/// <param name="pen">
		///   <see cref="T:System.Drawing.Pen" /> that determines the color, width, and style of the arc. </param>
		/// <param name="rect">
		///   <see cref="T:System.Drawing.RectangleF" /> structure that defines the boundaries of the ellipse. </param>
		/// <param name="startAngle">Angle in degrees measured clockwise from the x-axis to the starting point of the arc. </param>
		/// <param name="sweepAngle">Angle in degrees measured clockwise from the <paramref name="startAngle" /> parameter to ending point of the arc. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pen" /> is null</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060003AF RID: 943 RVA: 0x00009A00 File Offset: 0x00007C00
		public void DrawArc(Pen pen, RectangleF rect, float startAngle, float sweepAngle)
		{
			this.DrawArc(pen, rect.X, rect.Y, rect.Width, rect.Height, startAngle, sweepAngle);
		}

		/// <summary>Draws an arc representing a portion of an ellipse specified by a pair of coordinates, a width, and a height.</summary>
		/// <param name="pen">
		///   <see cref="T:System.Drawing.Pen" /> that determines the color, width, and style of the arc. </param>
		/// <param name="x">The x-coordinate of the upper-left corner of the rectangle that defines the ellipse. </param>
		/// <param name="y">The y-coordinate of the upper-left corner of the rectangle that defines the ellipse. </param>
		/// <param name="width">Width of the rectangle that defines the ellipse. </param>
		/// <param name="height">Height of the rectangle that defines the ellipse. </param>
		/// <param name="startAngle">Angle in degrees measured clockwise from the x-axis to the starting point of the arc. </param>
		/// <param name="sweepAngle">Angle in degrees measured clockwise from the <paramref name="startAngle" /> parameter to ending point of the arc. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pen" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060003B0 RID: 944 RVA: 0x00009A28 File Offset: 0x00007C28
		public void DrawArc(Pen pen, float x, float y, float width, float height, float startAngle, float sweepAngle)
		{
			if (pen == null)
			{
				throw new ArgumentNullException("pen");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipDrawArc(this.nativeObject, pen.NativePen, x, y, width, height, startAngle, sweepAngle));
		}

		/// <summary>Draws an arc representing a portion of an ellipse specified by a pair of coordinates, a width, and a height.</summary>
		/// <param name="pen">
		///   <see cref="T:System.Drawing.Pen" /> that determines the color, width, and style of the arc. </param>
		/// <param name="x">The x-coordinate of the upper-left corner of the rectangle that defines the ellipse. </param>
		/// <param name="y">The y-coordinate of the upper-left corner of the rectangle that defines the ellipse. </param>
		/// <param name="width">Width of the rectangle that defines the ellipse. </param>
		/// <param name="height">Height of the rectangle that defines the ellipse. </param>
		/// <param name="startAngle">Angle in degrees measured clockwise from the x-axis to the starting point of the arc. </param>
		/// <param name="sweepAngle">Angle in degrees measured clockwise from the <paramref name="startAngle" /> parameter to ending point of the arc. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pen" /> is null.-or-<paramref name="rects" /> is null.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="rects" /> is a zero-length array.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060003B1 RID: 945 RVA: 0x00009A58 File Offset: 0x00007C58
		public void DrawArc(Pen pen, int x, int y, int width, int height, int startAngle, int sweepAngle)
		{
			if (pen == null)
			{
				throw new ArgumentNullException("pen");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipDrawArcI(this.nativeObject, pen.NativePen, x, y, width, height, (float)startAngle, (float)sweepAngle));
		}

		/// <summary>Draws a Bézier spline defined by four <see cref="T:System.Drawing.PointF" /> structures.</summary>
		/// <param name="pen">
		///   <see cref="T:System.Drawing.Pen" /> that determines the color, width, and style of the curve. </param>
		/// <param name="pt1">
		///   <see cref="T:System.Drawing.PointF" /> structure that represents the starting point of the curve. </param>
		/// <param name="pt2">
		///   <see cref="T:System.Drawing.PointF" /> structure that represents the first control point for the curve. </param>
		/// <param name="pt3">
		///   <see cref="T:System.Drawing.PointF" /> structure that represents the second control point for the curve. </param>
		/// <param name="pt4">
		///   <see cref="T:System.Drawing.PointF" /> structure that represents the ending point of the curve. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pen" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060003B2 RID: 946 RVA: 0x00009A8C File Offset: 0x00007C8C
		public void DrawBezier(Pen pen, PointF pt1, PointF pt2, PointF pt3, PointF pt4)
		{
			if (pen == null)
			{
				throw new ArgumentNullException("pen");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipDrawBezier(this.nativeObject, pen.NativePen, pt1.X, pt1.Y, pt2.X, pt2.Y, pt3.X, pt3.Y, pt4.X, pt4.Y));
		}

		/// <summary>Draws a Bézier spline defined by four <see cref="T:System.Drawing.Point" /> structures.</summary>
		/// <param name="pen">
		///   <see cref="T:System.Drawing.Pen" /> structure that determines the color, width, and style of the curve. </param>
		/// <param name="pt1">
		///   <see cref="T:System.Drawing.Point" /> structure that represents the starting point of the curve. </param>
		/// <param name="pt2">
		///   <see cref="T:System.Drawing.Point" /> structure that represents the first control point for the curve. </param>
		/// <param name="pt3">
		///   <see cref="T:System.Drawing.Point" /> structure that represents the second control point for the curve. </param>
		/// <param name="pt4">
		///   <see cref="T:System.Drawing.Point" /> structure that represents the ending point of the curve. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pen" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060003B3 RID: 947 RVA: 0x00009AF8 File Offset: 0x00007CF8
		public void DrawBezier(Pen pen, Point pt1, Point pt2, Point pt3, Point pt4)
		{
			if (pen == null)
			{
				throw new ArgumentNullException("pen");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipDrawBezierI(this.nativeObject, pen.NativePen, pt1.X, pt1.Y, pt2.X, pt2.Y, pt3.X, pt3.Y, pt4.X, pt4.Y));
		}

		/// <summary>Draws a Bézier spline defined by four ordered pairs of coordinates that represent points.</summary>
		/// <param name="pen">
		///   <see cref="T:System.Drawing.Pen" /> that determines the color, width, and style of the curve. </param>
		/// <param name="x1">The x-coordinate of the starting point of the curve. </param>
		/// <param name="y1">The y-coordinate of the starting point of the curve. </param>
		/// <param name="x2">The x-coordinate of the first control point of the curve. </param>
		/// <param name="y2">The y-coordinate of the first control point of the curve. </param>
		/// <param name="x3">The x-coordinate of the second control point of the curve. </param>
		/// <param name="y3">The y-coordinate of the second control point of the curve. </param>
		/// <param name="x4">The x-coordinate of the ending point of the curve. </param>
		/// <param name="y4">The y-coordinate of the ending point of the curve. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pen" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060003B4 RID: 948 RVA: 0x00009B64 File Offset: 0x00007D64
		public void DrawBezier(Pen pen, float x1, float y1, float x2, float y2, float x3, float y3, float x4, float y4)
		{
			if (pen == null)
			{
				throw new ArgumentNullException("pen");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipDrawBezier(this.nativeObject, pen.NativePen, x1, y1, x2, y2, x3, y3, x4, y4));
		}

		/// <summary>Draws a series of Bézier splines from an array of <see cref="T:System.Drawing.Point" /> structures.</summary>
		/// <param name="pen">
		///   <see cref="T:System.Drawing.Pen" /> that determines the color, width, and style of the curve. </param>
		/// <param name="points">Array of <see cref="T:System.Drawing.Point" /> structures that represent the points that determine the curve. The number of points in the array should be a multiple of 3 plus 1, such as 4, 7, or 10.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pen" /> is null.-or-<paramref name="points" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060003B5 RID: 949 RVA: 0x00009BA4 File Offset: 0x00007DA4
		public void DrawBeziers(Pen pen, Point[] points)
		{
			if (pen == null)
			{
				throw new ArgumentNullException("pen");
			}
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			int num = points.Length;
			if (num < 4)
			{
				return;
			}
			for (int i = 0; i < num - 1; i += 3)
			{
				Point point = points[i];
				Point point2 = points[i + 1];
				Point point3 = points[i + 2];
				Point point4 = points[i + 3];
				GDIPlus.CheckStatus(GDIPlus.GdipDrawBezier(this.nativeObject, pen.NativePen, (float)point.X, (float)point.Y, (float)point2.X, (float)point2.Y, (float)point3.X, (float)point3.Y, (float)point4.X, (float)point4.Y));
			}
		}

		/// <summary>Draws a series of Bézier splines from an array of <see cref="T:System.Drawing.PointF" /> structures.</summary>
		/// <param name="pen">
		///   <see cref="T:System.Drawing.Pen" /> that determines the color, width, and style of the curve. </param>
		/// <param name="points">Array of <see cref="T:System.Drawing.PointF" /> structures that represent the points that determine the curve. The number of points in the array should be a multiple of 3 plus 1, such as 4, 7, or 10.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pen" /> is null.-or-<paramref name="points" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060003B6 RID: 950 RVA: 0x00009C68 File Offset: 0x00007E68
		public void DrawBeziers(Pen pen, PointF[] points)
		{
			if (pen == null)
			{
				throw new ArgumentNullException("pen");
			}
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			int num = points.Length;
			if (num < 4)
			{
				return;
			}
			for (int i = 0; i < num - 1; i += 3)
			{
				PointF pointF = points[i];
				PointF pointF2 = points[i + 1];
				PointF pointF3 = points[i + 2];
				PointF pointF4 = points[i + 3];
				GDIPlus.CheckStatus(GDIPlus.GdipDrawBezier(this.nativeObject, pen.NativePen, pointF.X, pointF.Y, pointF2.X, pointF2.Y, pointF3.X, pointF3.Y, pointF4.X, pointF4.Y));
			}
		}

		/// <summary>Draws a closed cardinal spline defined by an array of <see cref="T:System.Drawing.PointF" /> structures.</summary>
		/// <param name="pen">
		///   <see cref="T:System.Drawing.Pen" /> that determines the color, width, and height of the curve. </param>
		/// <param name="points">Array of <see cref="T:System.Drawing.PointF" /> structures that define the spline. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pen" /> is null.-or-<paramref name="points" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060003B7 RID: 951 RVA: 0x00009D1E File Offset: 0x00007F1E
		public void DrawClosedCurve(Pen pen, PointF[] points)
		{
			if (pen == null)
			{
				throw new ArgumentNullException("pen");
			}
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipDrawClosedCurve(this.nativeObject, pen.NativePen, points, points.Length));
		}

		/// <summary>Draws a closed cardinal spline defined by an array of <see cref="T:System.Drawing.Point" /> structures.</summary>
		/// <param name="pen">
		///   <see cref="T:System.Drawing.Pen" /> that determines the color, width, and height of the curve. </param>
		/// <param name="points">Array of <see cref="T:System.Drawing.Point" /> structures that define the spline. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pen" /> is null.-or-<paramref name="points" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060003B8 RID: 952 RVA: 0x00009D56 File Offset: 0x00007F56
		public void DrawClosedCurve(Pen pen, Point[] points)
		{
			if (pen == null)
			{
				throw new ArgumentNullException("pen");
			}
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipDrawClosedCurveI(this.nativeObject, pen.NativePen, points, points.Length));
		}

		/// <summary>Draws a closed cardinal spline defined by an array of <see cref="T:System.Drawing.Point" /> structures using a specified tension.</summary>
		/// <param name="pen">
		///   <see cref="T:System.Drawing.Pen" /> that determines the color, width, and height of the curve. </param>
		/// <param name="points">Array of <see cref="T:System.Drawing.Point" /> structures that define the spline. </param>
		/// <param name="tension">Value greater than or equal to 0.0F that specifies the tension of the curve. </param>
		/// <param name="fillmode">Member of the <see cref="T:System.Drawing.Drawing2D.FillMode" /> enumeration that determines how the curve is filled. This parameter is required but ignored. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pen" /> is null.-or-<paramref name="points" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060003B9 RID: 953 RVA: 0x00009D8E File Offset: 0x00007F8E
		public void DrawClosedCurve(Pen pen, Point[] points, float tension, FillMode fillmode)
		{
			if (pen == null)
			{
				throw new ArgumentNullException("pen");
			}
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipDrawClosedCurve2I(this.nativeObject, pen.NativePen, points, points.Length, tension));
		}

		/// <summary>Draws a closed cardinal spline defined by an array of <see cref="T:System.Drawing.PointF" /> structures using a specified tension.</summary>
		/// <param name="pen">
		///   <see cref="T:System.Drawing.Pen" /> that determines the color, width, and height of the curve. </param>
		/// <param name="points">Array of <see cref="T:System.Drawing.PointF" /> structures that define the spline. </param>
		/// <param name="tension">Value greater than or equal to 0.0F that specifies the tension of the curve. </param>
		/// <param name="fillmode">Member of the <see cref="T:System.Drawing.Drawing2D.FillMode" /> enumeration that determines how the curve is filled. This parameter is required but is ignored. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pen" /> is null.-or-<paramref name="points" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060003BA RID: 954 RVA: 0x00009DC7 File Offset: 0x00007FC7
		public void DrawClosedCurve(Pen pen, PointF[] points, float tension, FillMode fillmode)
		{
			if (pen == null)
			{
				throw new ArgumentNullException("pen");
			}
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipDrawClosedCurve2(this.nativeObject, pen.NativePen, points, points.Length, tension));
		}

		/// <summary>Draws a cardinal spline through a specified array of <see cref="T:System.Drawing.Point" /> structures.</summary>
		/// <param name="pen">
		///   <see cref="T:System.Drawing.Pen" /> that determines the color, width, and height of the curve. </param>
		/// <param name="points">Array of <see cref="T:System.Drawing.Point" /> structures that define the spline. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pen" /> is null.-or-<paramref name="points" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060003BB RID: 955 RVA: 0x00009E00 File Offset: 0x00008000
		public void DrawCurve(Pen pen, Point[] points)
		{
			if (pen == null)
			{
				throw new ArgumentNullException("pen");
			}
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipDrawCurveI(this.nativeObject, pen.NativePen, points, points.Length));
		}

		/// <summary>Draws a cardinal spline through a specified array of <see cref="T:System.Drawing.PointF" /> structures.</summary>
		/// <param name="pen">
		///   <see cref="T:System.Drawing.Pen" /> that determines the color, width, and style of the curve. </param>
		/// <param name="points">Array of <see cref="T:System.Drawing.PointF" /> structures that define the spline. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pen" /> is null.-or-<paramref name="points" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060003BC RID: 956 RVA: 0x00009E38 File Offset: 0x00008038
		public void DrawCurve(Pen pen, PointF[] points)
		{
			if (pen == null)
			{
				throw new ArgumentNullException("pen");
			}
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipDrawCurve(this.nativeObject, pen.NativePen, points, points.Length));
		}

		/// <summary>Draws a cardinal spline through a specified array of <see cref="T:System.Drawing.PointF" /> structures using a specified tension.</summary>
		/// <param name="pen">
		///   <see cref="T:System.Drawing.Pen" /> that determines the color, width, and style of the curve. </param>
		/// <param name="points">Array of <see cref="T:System.Drawing.PointF" /> structures that represent the points that define the curve. </param>
		/// <param name="tension">Value greater than or equal to 0.0F that specifies the tension of the curve. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pen" /> is null.-or-<paramref name="points" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060003BD RID: 957 RVA: 0x00009E70 File Offset: 0x00008070
		public void DrawCurve(Pen pen, PointF[] points, float tension)
		{
			if (pen == null)
			{
				throw new ArgumentNullException("pen");
			}
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipDrawCurve2(this.nativeObject, pen.NativePen, points, points.Length, tension));
		}

		/// <summary>Draws a cardinal spline through a specified array of <see cref="T:System.Drawing.Point" /> structures using a specified tension.</summary>
		/// <param name="pen">
		///   <see cref="T:System.Drawing.Pen" /> that determines the color, width, and style of the curve. </param>
		/// <param name="points">Array of <see cref="T:System.Drawing.Point" /> structures that define the spline. </param>
		/// <param name="tension">Value greater than or equal to 0.0F that specifies the tension of the curve. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pen" /> is null.-or-<paramref name="points" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060003BE RID: 958 RVA: 0x00009EA9 File Offset: 0x000080A9
		public void DrawCurve(Pen pen, Point[] points, float tension)
		{
			if (pen == null)
			{
				throw new ArgumentNullException("pen");
			}
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipDrawCurve2I(this.nativeObject, pen.NativePen, points, points.Length, tension));
		}

		/// <summary>Draws a cardinal spline through a specified array of <see cref="T:System.Drawing.PointF" /> structures. The drawing begins offset from the beginning of the array.</summary>
		/// <param name="pen">
		///   <see cref="T:System.Drawing.Pen" /> that determines the color, width, and style of the curve. </param>
		/// <param name="points">Array of <see cref="T:System.Drawing.PointF" /> structures that define the spline. </param>
		/// <param name="offset">Offset from the first element in the array of the <paramref name="points" /> parameter to the starting point in the curve. </param>
		/// <param name="numberOfSegments">Number of segments after the starting point to include in the curve. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pen" /> is null.-or-<paramref name="points" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060003BF RID: 959 RVA: 0x00009EE2 File Offset: 0x000080E2
		public void DrawCurve(Pen pen, PointF[] points, int offset, int numberOfSegments)
		{
			if (pen == null)
			{
				throw new ArgumentNullException("pen");
			}
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipDrawCurve3(this.nativeObject, pen.NativePen, points, points.Length, offset, numberOfSegments, 0.5f));
		}

		/// <summary>Draws a cardinal spline through a specified array of <see cref="T:System.Drawing.Point" /> structures using a specified tension.</summary>
		/// <param name="pen">
		///   <see cref="T:System.Drawing.Pen" /> that determines the color, width, and style of the curve. </param>
		/// <param name="points">Array of <see cref="T:System.Drawing.Point" /> structures that define the spline. </param>
		/// <param name="offset">Offset from the first element in the array of the <paramref name="points" /> parameter to the starting point in the curve. </param>
		/// <param name="numberOfSegments">Number of segments after the starting point to include in the curve. </param>
		/// <param name="tension">Value greater than or equal to 0.0F that specifies the tension of the curve. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pen" /> is null.-or-<paramref name="points" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060003C0 RID: 960 RVA: 0x00009F22 File Offset: 0x00008122
		public void DrawCurve(Pen pen, Point[] points, int offset, int numberOfSegments, float tension)
		{
			if (pen == null)
			{
				throw new ArgumentNullException("pen");
			}
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipDrawCurve3I(this.nativeObject, pen.NativePen, points, points.Length, offset, numberOfSegments, tension));
		}

		/// <summary>Draws a cardinal spline through a specified array of <see cref="T:System.Drawing.PointF" /> structures using a specified tension. The drawing begins offset from the beginning of the array.</summary>
		/// <param name="pen">
		///   <see cref="T:System.Drawing.Pen" /> that determines the color, width, and style of the curve. </param>
		/// <param name="points">Array of <see cref="T:System.Drawing.PointF" /> structures that define the spline. </param>
		/// <param name="offset">Offset from the first element in the array of the <paramref name="points" /> parameter to the starting point in the curve. </param>
		/// <param name="numberOfSegments">Number of segments after the starting point to include in the curve. </param>
		/// <param name="tension">Value greater than or equal to 0.0F that specifies the tension of the curve. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pen" /> is null.-or-<paramref name="points" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060003C1 RID: 961 RVA: 0x00009F5F File Offset: 0x0000815F
		public void DrawCurve(Pen pen, PointF[] points, int offset, int numberOfSegments, float tension)
		{
			if (pen == null)
			{
				throw new ArgumentNullException("pen");
			}
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipDrawCurve3(this.nativeObject, pen.NativePen, points, points.Length, offset, numberOfSegments, tension));
		}

		/// <summary>Draws an ellipse specified by a bounding <see cref="T:System.Drawing.Rectangle" /> structure.</summary>
		/// <param name="pen">
		///   <see cref="T:System.Drawing.Pen" /> that determines the color, width, and style of the ellipse. </param>
		/// <param name="rect">
		///   <see cref="T:System.Drawing.Rectangle" /> structure that defines the boundaries of the ellipse. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pen" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060003C2 RID: 962 RVA: 0x00009F9C File Offset: 0x0000819C
		public void DrawEllipse(Pen pen, Rectangle rect)
		{
			if (pen == null)
			{
				throw new ArgumentNullException("pen");
			}
			this.DrawEllipse(pen, rect.X, rect.Y, rect.Width, rect.Height);
		}

		/// <summary>Draws an ellipse defined by a bounding <see cref="T:System.Drawing.RectangleF" />.</summary>
		/// <param name="pen">
		///   <see cref="T:System.Drawing.Pen" /> that determines the color, width, and style of the ellipse. </param>
		/// <param name="rect">
		///   <see cref="T:System.Drawing.RectangleF" /> structure that defines the boundaries of the ellipse. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pen" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060003C3 RID: 963 RVA: 0x00009FCF File Offset: 0x000081CF
		public void DrawEllipse(Pen pen, RectangleF rect)
		{
			if (pen == null)
			{
				throw new ArgumentNullException("pen");
			}
			this.DrawEllipse(pen, rect.X, rect.Y, rect.Width, rect.Height);
		}

		/// <summary>Draws an ellipse defined by a bounding rectangle specified by coordinates for the upper-left corner of the rectangle, a height, and a width.</summary>
		/// <param name="pen">
		///   <see cref="T:System.Drawing.Pen" /> that determines the color, width, and style of the ellipse. </param>
		/// <param name="x">The x-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse. </param>
		/// <param name="y">The y-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse. </param>
		/// <param name="width">Width of the bounding rectangle that defines the ellipse. </param>
		/// <param name="height">Height of the bounding rectangle that defines the ellipse. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pen" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060003C4 RID: 964 RVA: 0x0000A002 File Offset: 0x00008202
		public void DrawEllipse(Pen pen, int x, int y, int width, int height)
		{
			if (pen == null)
			{
				throw new ArgumentNullException("pen");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipDrawEllipseI(this.nativeObject, pen.NativePen, x, y, width, height));
		}

		/// <summary>Draws an ellipse defined by a bounding rectangle specified by a pair of coordinates, a height, and a width.</summary>
		/// <param name="pen">
		///   <see cref="T:System.Drawing.Pen" /> that determines the color, width, and style of the ellipse. </param>
		/// <param name="x">The x-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse. </param>
		/// <param name="y">The y-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse. </param>
		/// <param name="width">Width of the bounding rectangle that defines the ellipse. </param>
		/// <param name="height">Height of the bounding rectangle that defines the ellipse. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pen" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060003C5 RID: 965 RVA: 0x0000A02E File Offset: 0x0000822E
		public void DrawEllipse(Pen pen, float x, float y, float width, float height)
		{
			if (pen == null)
			{
				throw new ArgumentNullException("pen");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipDrawEllipse(this.nativeObject, pen.NativePen, x, y, width, height));
		}

		/// <summary>Draws the image represented by the specified <see cref="T:System.Drawing.Icon" /> within the area specified by a <see cref="T:System.Drawing.Rectangle" /> structure.</summary>
		/// <param name="icon">
		///   <see cref="T:System.Drawing.Icon" /> to draw. </param>
		/// <param name="targetRect">
		///   <see cref="T:System.Drawing.Rectangle" /> structure that specifies the location and size of the resulting image on the display surface. The image contained in the <paramref name="icon" /> parameter is scaled to the dimensions of this rectangular area. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="icon" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060003C6 RID: 966 RVA: 0x0000A05A File Offset: 0x0000825A
		public void DrawIcon(Icon icon, Rectangle targetRect)
		{
			if (icon == null)
			{
				throw new ArgumentNullException("icon");
			}
			this.DrawImage(icon.GetInternalBitmap(), targetRect);
		}

		/// <summary>Draws the image represented by the specified <see cref="T:System.Drawing.Icon" /> at the specified coordinates.</summary>
		/// <param name="icon">
		///   <see cref="T:System.Drawing.Icon" /> to draw. </param>
		/// <param name="x">The x-coordinate of the upper-left corner of the drawn image. </param>
		/// <param name="y">The y-coordinate of the upper-left corner of the drawn image. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="icon" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060003C7 RID: 967 RVA: 0x0000A077 File Offset: 0x00008277
		public void DrawIcon(Icon icon, int x, int y)
		{
			if (icon == null)
			{
				throw new ArgumentNullException("icon");
			}
			this.DrawImage(icon.GetInternalBitmap(), x, y);
		}

		/// <summary>Draws the image represented by the specified <see cref="T:System.Drawing.Icon" /> without scaling the image.</summary>
		/// <param name="icon">
		///   <see cref="T:System.Drawing.Icon" /> to draw. </param>
		/// <param name="targetRect">
		///   <see cref="T:System.Drawing.Rectangle" /> structure that specifies the location and size of the resulting image. The image is not scaled to fit this rectangle, but retains its original size. If the image is larger than the rectangle, it is clipped to fit inside it. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="icon" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060003C8 RID: 968 RVA: 0x0000A095 File Offset: 0x00008295
		public void DrawIconUnstretched(Icon icon, Rectangle targetRect)
		{
			if (icon == null)
			{
				throw new ArgumentNullException("icon");
			}
			this.DrawImageUnscaled(icon.GetInternalBitmap(), targetRect);
		}

		/// <summary>Draws the specified <see cref="T:System.Drawing.Image" /> at the specified location and with the specified size.</summary>
		/// <param name="image">
		///   <see cref="T:System.Drawing.Image" /> to draw. </param>
		/// <param name="rect">
		///   <see cref="T:System.Drawing.RectangleF" /> structure that specifies the location and size of the drawn image. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="image" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060003C9 RID: 969 RVA: 0x0000A0B4 File Offset: 0x000082B4
		public void DrawImage(Image image, RectangleF rect)
		{
			if (image == null)
			{
				throw new ArgumentNullException("image");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipDrawImageRect(this.nativeObject, image.NativeObject, rect.X, rect.Y, rect.Width, rect.Height));
		}

		/// <summary>Draws the specified <see cref="T:System.Drawing.Image" />, using its original physical size, at the specified location.</summary>
		/// <param name="image">
		///   <see cref="T:System.Drawing.Image" /> to draw. </param>
		/// <param name="point">
		///   <see cref="T:System.Drawing.PointF" /> structure that represents the upper-left corner of the drawn image. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="image" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060003CA RID: 970 RVA: 0x0000A101 File Offset: 0x00008301
		public void DrawImage(Image image, PointF point)
		{
			if (image == null)
			{
				throw new ArgumentNullException("image");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipDrawImage(this.nativeObject, image.NativeObject, point.X, point.Y));
		}

		/// <summary>Draws the specified <see cref="T:System.Drawing.Image" /> at the specified location and with the specified shape and size.</summary>
		/// <param name="image">
		///   <see cref="T:System.Drawing.Image" /> to draw. </param>
		/// <param name="destPoints">Array of three <see cref="T:System.Drawing.Point" /> structures that define a parallelogram. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="image" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060003CB RID: 971 RVA: 0x0000A135 File Offset: 0x00008335
		public void DrawImage(Image image, Point[] destPoints)
		{
			if (image == null)
			{
				throw new ArgumentNullException("image");
			}
			if (destPoints == null)
			{
				throw new ArgumentNullException("destPoints");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipDrawImagePointsI(this.nativeObject, image.NativeObject, destPoints, destPoints.Length));
		}

		/// <summary>Draws the specified <see cref="T:System.Drawing.Image" />, using its original physical size, at the specified location.</summary>
		/// <param name="image">
		///   <see cref="T:System.Drawing.Image" /> to draw. </param>
		/// <param name="point">
		///   <see cref="T:System.Drawing.Point" /> structure that represents the location of the upper-left corner of the drawn image. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="image" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060003CC RID: 972 RVA: 0x0000A16D File Offset: 0x0000836D
		public void DrawImage(Image image, Point point)
		{
			if (image == null)
			{
				throw new ArgumentNullException("image");
			}
			this.DrawImage(image, point.X, point.Y);
		}

		/// <summary>Draws the specified <see cref="T:System.Drawing.Image" /> at the specified location and with the specified size.</summary>
		/// <param name="image">
		///   <see cref="T:System.Drawing.Image" /> to draw. </param>
		/// <param name="rect">
		///   <see cref="T:System.Drawing.Rectangle" /> structure that specifies the location and size of the drawn image. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="image" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060003CD RID: 973 RVA: 0x0000A192 File Offset: 0x00008392
		public void DrawImage(Image image, Rectangle rect)
		{
			if (image == null)
			{
				throw new ArgumentNullException("image");
			}
			this.DrawImage(image, rect.X, rect.Y, rect.Width, rect.Height);
		}

		/// <summary>Draws the specified <see cref="T:System.Drawing.Image" /> at the specified location and with the specified shape and size.</summary>
		/// <param name="image">
		///   <see cref="T:System.Drawing.Image" /> to draw. </param>
		/// <param name="destPoints">Array of three <see cref="T:System.Drawing.PointF" /> structures that define a parallelogram. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="image" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060003CE RID: 974 RVA: 0x0000A1C5 File Offset: 0x000083C5
		public void DrawImage(Image image, PointF[] destPoints)
		{
			if (image == null)
			{
				throw new ArgumentNullException("image");
			}
			if (destPoints == null)
			{
				throw new ArgumentNullException("destPoints");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipDrawImagePoints(this.nativeObject, image.NativeObject, destPoints, destPoints.Length));
		}

		/// <summary>Draws the specified image, using its original physical size, at the location specified by a coordinate pair.</summary>
		/// <param name="image">
		///   <see cref="T:System.Drawing.Image" /> to draw. </param>
		/// <param name="x">The x-coordinate of the upper-left corner of the drawn image. </param>
		/// <param name="y">The y-coordinate of the upper-left corner of the drawn image. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="image" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060003CF RID: 975 RVA: 0x0000A1FD File Offset: 0x000083FD
		public void DrawImage(Image image, int x, int y)
		{
			if (image == null)
			{
				throw new ArgumentNullException("image");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipDrawImageI(this.nativeObject, image.NativeObject, x, y));
		}

		/// <summary>Draws the specified <see cref="T:System.Drawing.Image" />, using its original physical size, at the specified location.</summary>
		/// <param name="image">
		///   <see cref="T:System.Drawing.Image" /> to draw. </param>
		/// <param name="x">The x-coordinate of the upper-left corner of the drawn image. </param>
		/// <param name="y">The y-coordinate of the upper-left corner of the drawn image. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="image" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060003D0 RID: 976 RVA: 0x0000A225 File Offset: 0x00008425
		public void DrawImage(Image image, float x, float y)
		{
			if (image == null)
			{
				throw new ArgumentNullException("image");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipDrawImage(this.nativeObject, image.NativeObject, x, y));
		}

		/// <summary>Draws the specified portion of the specified <see cref="T:System.Drawing.Image" /> at the specified location and with the specified size.</summary>
		/// <param name="image">
		///   <see cref="T:System.Drawing.Image" /> to draw. </param>
		/// <param name="destRect">
		///   <see cref="T:System.Drawing.Rectangle" /> structure that specifies the location and size of the drawn image. The image is scaled to fit the rectangle. </param>
		/// <param name="srcRect">
		///   <see cref="T:System.Drawing.Rectangle" /> structure that specifies the portion of the <paramref name="image" /> object to draw. </param>
		/// <param name="srcUnit">Member of the <see cref="T:System.Drawing.GraphicsUnit" /> enumeration that specifies the units of measure used by the <paramref name="srcRect" /> parameter. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="image" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060003D1 RID: 977 RVA: 0x0000A250 File Offset: 0x00008450
		public void DrawImage(Image image, Rectangle destRect, Rectangle srcRect, GraphicsUnit srcUnit)
		{
			if (image == null)
			{
				throw new ArgumentNullException("image");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipDrawImageRectRectI(this.nativeObject, image.NativeObject, destRect.X, destRect.Y, destRect.Width, destRect.Height, srcRect.X, srcRect.Y, srcRect.Width, srcRect.Height, srcUnit, IntPtr.Zero, null, IntPtr.Zero));
		}

		/// <summary>Draws the specified portion of the specified <see cref="T:System.Drawing.Image" /> at the specified location and with the specified size.</summary>
		/// <param name="image">
		///   <see cref="T:System.Drawing.Image" /> to draw. </param>
		/// <param name="destRect">
		///   <see cref="T:System.Drawing.RectangleF" /> structure that specifies the location and size of the drawn image. The image is scaled to fit the rectangle. </param>
		/// <param name="srcRect">
		///   <see cref="T:System.Drawing.RectangleF" /> structure that specifies the portion of the <paramref name="image" /> object to draw. </param>
		/// <param name="srcUnit">Member of the <see cref="T:System.Drawing.GraphicsUnit" /> enumeration that specifies the units of measure used by the <paramref name="srcRect" /> parameter. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="image" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060003D2 RID: 978 RVA: 0x0000A2C8 File Offset: 0x000084C8
		public void DrawImage(Image image, RectangleF destRect, RectangleF srcRect, GraphicsUnit srcUnit)
		{
			if (image == null)
			{
				throw new ArgumentNullException("image");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipDrawImageRectRect(this.nativeObject, image.NativeObject, destRect.X, destRect.Y, destRect.Width, destRect.Height, srcRect.X, srcRect.Y, srcRect.Width, srcRect.Height, srcUnit, IntPtr.Zero, null, IntPtr.Zero));
		}

		/// <summary>Draws the specified portion of the specified <see cref="T:System.Drawing.Image" /> at the specified location and with the specified size.</summary>
		/// <param name="image">
		///   <see cref="T:System.Drawing.Image" /> to draw. </param>
		/// <param name="destPoints">Array of three <see cref="T:System.Drawing.Point" /> structures that define a parallelogram. </param>
		/// <param name="srcRect">
		///   <see cref="T:System.Drawing.Rectangle" /> structure that specifies the portion of the <paramref name="image" /> object to draw. </param>
		/// <param name="srcUnit">Member of the <see cref="T:System.Drawing.GraphicsUnit" /> enumeration that specifies the units of measure used by the <paramref name="srcRect" /> parameter. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="image" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060003D3 RID: 979 RVA: 0x0000A340 File Offset: 0x00008540
		public void DrawImage(Image image, Point[] destPoints, Rectangle srcRect, GraphicsUnit srcUnit)
		{
			if (image == null)
			{
				throw new ArgumentNullException("image");
			}
			if (destPoints == null)
			{
				throw new ArgumentNullException("destPoints");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipDrawImagePointsRectI(this.nativeObject, image.NativeObject, destPoints, destPoints.Length, srcRect.X, srcRect.Y, srcRect.Width, srcRect.Height, srcUnit, IntPtr.Zero, null, IntPtr.Zero));
		}

		/// <summary>Draws the specified portion of the specified <see cref="T:System.Drawing.Image" /> at the specified location and with the specified size.</summary>
		/// <param name="image">
		///   <see cref="T:System.Drawing.Image" /> to draw. </param>
		/// <param name="destPoints">Array of three <see cref="T:System.Drawing.PointF" /> structures that define a parallelogram. </param>
		/// <param name="srcRect">
		///   <see cref="T:System.Drawing.RectangleF" /> structure that specifies the portion of the <paramref name="image" /> object to draw. </param>
		/// <param name="srcUnit">Member of the <see cref="T:System.Drawing.GraphicsUnit" /> enumeration that specifies the units of measure used by the <paramref name="srcRect" /> parameter. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="image" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060003D4 RID: 980 RVA: 0x0000A3AC File Offset: 0x000085AC
		public void DrawImage(Image image, PointF[] destPoints, RectangleF srcRect, GraphicsUnit srcUnit)
		{
			if (image == null)
			{
				throw new ArgumentNullException("image");
			}
			if (destPoints == null)
			{
				throw new ArgumentNullException("destPoints");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipDrawImagePointsRect(this.nativeObject, image.NativeObject, destPoints, destPoints.Length, srcRect.X, srcRect.Y, srcRect.Width, srcRect.Height, srcUnit, IntPtr.Zero, null, IntPtr.Zero));
		}

		/// <summary>Draws the specified portion of the specified <see cref="T:System.Drawing.Image" /> at the specified location.</summary>
		/// <param name="image">
		///   <see cref="T:System.Drawing.Image" /> to draw. </param>
		/// <param name="destPoints">Array of three <see cref="T:System.Drawing.Point" /> structures that define a parallelogram. </param>
		/// <param name="srcRect">
		///   <see cref="T:System.Drawing.Rectangle" /> structure that specifies the portion of the <paramref name="image" /> object to draw. </param>
		/// <param name="srcUnit">Member of the <see cref="T:System.Drawing.GraphicsUnit" /> enumeration that specifies the units of measure used by the <paramref name="srcRect" /> parameter. </param>
		/// <param name="imageAttr">
		///   <see cref="T:System.Drawing.Imaging.ImageAttributes" /> that specifies recoloring and gamma information for the <paramref name="image" /> object. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="image" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060003D5 RID: 981 RVA: 0x0000A418 File Offset: 0x00008618
		public void DrawImage(Image image, Point[] destPoints, Rectangle srcRect, GraphicsUnit srcUnit, ImageAttributes imageAttr)
		{
			if (image == null)
			{
				throw new ArgumentNullException("image");
			}
			if (destPoints == null)
			{
				throw new ArgumentNullException("destPoints");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipDrawImagePointsRectI(this.nativeObject, image.NativeObject, destPoints, destPoints.Length, srcRect.X, srcRect.Y, srcRect.Width, srcRect.Height, srcUnit, (imageAttr != null) ? imageAttr.nativeImageAttributes : IntPtr.Zero, null, IntPtr.Zero));
		}

		/// <summary>Draws the specified <see cref="T:System.Drawing.Image" /> at the specified location and with the specified size.</summary>
		/// <param name="image">
		///   <see cref="T:System.Drawing.Image" /> to draw. </param>
		/// <param name="x">The x-coordinate of the upper-left corner of the drawn image. </param>
		/// <param name="y">The y-coordinate of the upper-left corner of the drawn image. </param>
		/// <param name="width">Width of the drawn image. </param>
		/// <param name="height">Height of the drawn image. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="image" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060003D6 RID: 982 RVA: 0x0000A491 File Offset: 0x00008691
		public void DrawImage(Image image, float x, float y, float width, float height)
		{
			if (image == null)
			{
				throw new ArgumentNullException("image");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipDrawImageRect(this.nativeObject, image.NativeObject, x, y, width, height));
		}

		/// <summary>Draws the specified portion of the specified <see cref="T:System.Drawing.Image" /> at the specified location and with the specified size.</summary>
		/// <param name="image">
		///   <see cref="T:System.Drawing.Image" /> to draw. </param>
		/// <param name="destPoints">Array of three <see cref="T:System.Drawing.PointF" /> structures that define a parallelogram. </param>
		/// <param name="srcRect">
		///   <see cref="T:System.Drawing.RectangleF" /> structure that specifies the portion of the <paramref name="image" /> object to draw. </param>
		/// <param name="srcUnit">Member of the <see cref="T:System.Drawing.GraphicsUnit" /> enumeration that specifies the units of measure used by the <paramref name="srcRect" /> parameter. </param>
		/// <param name="imageAttr">
		///   <see cref="T:System.Drawing.Imaging.ImageAttributes" /> that specifies recoloring and gamma information for the <paramref name="image" /> object. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="image" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060003D7 RID: 983 RVA: 0x0000A4C0 File Offset: 0x000086C0
		public void DrawImage(Image image, PointF[] destPoints, RectangleF srcRect, GraphicsUnit srcUnit, ImageAttributes imageAttr)
		{
			if (image == null)
			{
				throw new ArgumentNullException("image");
			}
			if (destPoints == null)
			{
				throw new ArgumentNullException("destPoints");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipDrawImagePointsRect(this.nativeObject, image.NativeObject, destPoints, destPoints.Length, srcRect.X, srcRect.Y, srcRect.Width, srcRect.Height, srcUnit, (imageAttr != null) ? imageAttr.nativeImageAttributes : IntPtr.Zero, null, IntPtr.Zero));
		}

		/// <summary>Draws a portion of an image at a specified location.</summary>
		/// <param name="image">
		///   <see cref="T:System.Drawing.Image" /> to draw. </param>
		/// <param name="x">The x-coordinate of the upper-left corner of the drawn image. </param>
		/// <param name="y">The y-coordinate of the upper-left corner of the drawn image. </param>
		/// <param name="srcRect">
		///   <see cref="T:System.Drawing.Rectangle" /> structure that specifies the portion of the <paramref name="image" /> object to draw. </param>
		/// <param name="srcUnit">Member of the <see cref="T:System.Drawing.GraphicsUnit" /> enumeration that specifies the units of measure used by the <paramref name="srcRect" /> parameter. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="image" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060003D8 RID: 984 RVA: 0x0000A53C File Offset: 0x0000873C
		public void DrawImage(Image image, int x, int y, Rectangle srcRect, GraphicsUnit srcUnit)
		{
			if (image == null)
			{
				throw new ArgumentNullException("image");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipDrawImagePointRectI(this.nativeObject, image.NativeObject, x, y, srcRect.X, srcRect.Y, srcRect.Width, srcRect.Height, srcUnit));
		}

		/// <summary>Draws the specified <see cref="T:System.Drawing.Image" /> at the specified location and with the specified size.</summary>
		/// <param name="image">
		///   <see cref="T:System.Drawing.Image" /> to draw. </param>
		/// <param name="x">The x-coordinate of the upper-left corner of the drawn image. </param>
		/// <param name="y">The y-coordinate of the upper-left corner of the drawn image. </param>
		/// <param name="width">Width of the drawn image. </param>
		/// <param name="height">Height of the drawn image. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="image" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060003D9 RID: 985 RVA: 0x0000A58D File Offset: 0x0000878D
		public void DrawImage(Image image, int x, int y, int width, int height)
		{
			if (image == null)
			{
				throw new ArgumentNullException("image");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipDrawImageRectI(this.nativeObject, image.nativeObject, x, y, width, height));
		}

		/// <summary>Draws a portion of an image at a specified location.</summary>
		/// <param name="image">
		///   <see cref="T:System.Drawing.Image" /> to draw. </param>
		/// <param name="x">The x-coordinate of the upper-left corner of the drawn image. </param>
		/// <param name="y">The y-coordinate of the upper-left corner of the drawn image. </param>
		/// <param name="srcRect">
		///   <see cref="T:System.Drawing.RectangleF" /> structure that specifies the portion of the <see cref="T:System.Drawing.Image" /> to draw. </param>
		/// <param name="srcUnit">Member of the <see cref="T:System.Drawing.GraphicsUnit" /> enumeration that specifies the units of measure used by the <paramref name="srcRect" /> parameter. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="image" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060003DA RID: 986 RVA: 0x0000A5BC File Offset: 0x000087BC
		public void DrawImage(Image image, float x, float y, RectangleF srcRect, GraphicsUnit srcUnit)
		{
			if (image == null)
			{
				throw new ArgumentNullException("image");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipDrawImagePointRect(this.nativeObject, image.nativeObject, x, y, srcRect.X, srcRect.Y, srcRect.Width, srcRect.Height, srcUnit));
		}

		/// <summary>Draws the specified portion of the specified <see cref="T:System.Drawing.Image" /> at the specified location and with the specified size.</summary>
		/// <param name="image">
		///   <see cref="T:System.Drawing.Image" /> to draw. </param>
		/// <param name="destPoints">Array of three <see cref="T:System.Drawing.PointF" /> structures that define a parallelogram. </param>
		/// <param name="srcRect">
		///   <see cref="T:System.Drawing.RectangleF" /> structure that specifies the portion of the <paramref name="image" /> object to draw. </param>
		/// <param name="srcUnit">Member of the <see cref="T:System.Drawing.GraphicsUnit" /> enumeration that specifies the units of measure used by the <paramref name="srcRect" /> parameter. </param>
		/// <param name="imageAttr">
		///   <see cref="T:System.Drawing.Imaging.ImageAttributes" /> that specifies recoloring and gamma information for the <paramref name="image" /> object. </param>
		/// <param name="callback">
		///   <see cref="T:System.Drawing.Graphics.DrawImageAbort" /> delegate that specifies a method to call during the drawing of the image. This method is called frequently to check whether to stop execution of the <see cref="M:System.Drawing.Graphics.DrawImage(System.Drawing.Image,System.Drawing.PointF[],System.Drawing.RectangleF,System.Drawing.GraphicsUnit,System.Drawing.Imaging.ImageAttributes,System.Drawing.Graphics.DrawImageAbort)" />  method according to application-determined criteria. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="image" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060003DB RID: 987 RVA: 0x0000A610 File Offset: 0x00008810
		public void DrawImage(Image image, PointF[] destPoints, RectangleF srcRect, GraphicsUnit srcUnit, ImageAttributes imageAttr, Graphics.DrawImageAbort callback)
		{
			if (image == null)
			{
				throw new ArgumentNullException("image");
			}
			if (destPoints == null)
			{
				throw new ArgumentNullException("destPoints");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipDrawImagePointsRect(this.nativeObject, image.NativeObject, destPoints, destPoints.Length, srcRect.X, srcRect.Y, srcRect.Width, srcRect.Height, srcUnit, (imageAttr != null) ? imageAttr.nativeImageAttributes : IntPtr.Zero, callback, IntPtr.Zero));
		}

		/// <summary>Draws the specified portion of the specified <see cref="T:System.Drawing.Image" /> at the specified location and with the specified size.</summary>
		/// <param name="image">
		///   <see cref="T:System.Drawing.Image" /> to draw. </param>
		/// <param name="destPoints">Array of three <see cref="T:System.Drawing.PointF" /> structures that define a parallelogram. </param>
		/// <param name="srcRect">
		///   <see cref="T:System.Drawing.Rectangle" /> structure that specifies the portion of the <paramref name="image" /> object to draw. </param>
		/// <param name="srcUnit">Member of the <see cref="T:System.Drawing.GraphicsUnit" /> enumeration that specifies the units of measure used by the <paramref name="srcRect" /> parameter. </param>
		/// <param name="imageAttr">
		///   <see cref="T:System.Drawing.Imaging.ImageAttributes" /> that specifies recoloring and gamma information for the <paramref name="image" /> object. </param>
		/// <param name="callback">
		///   <see cref="T:System.Drawing.Graphics.DrawImageAbort" /> delegate that specifies a method to call during the drawing of the image. This method is called frequently to check whether to stop execution of the <see cref="M:System.Drawing.Graphics.DrawImage(System.Drawing.Image,System.Drawing.Point[],System.Drawing.Rectangle,System.Drawing.GraphicsUnit,System.Drawing.Imaging.ImageAttributes,System.Drawing.Graphics.DrawImageAbort)" /> method according to application-determined criteria. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="image" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060003DC RID: 988 RVA: 0x0000A68C File Offset: 0x0000888C
		public void DrawImage(Image image, Point[] destPoints, Rectangle srcRect, GraphicsUnit srcUnit, ImageAttributes imageAttr, Graphics.DrawImageAbort callback)
		{
			if (image == null)
			{
				throw new ArgumentNullException("image");
			}
			if (destPoints == null)
			{
				throw new ArgumentNullException("destPoints");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipDrawImagePointsRectI(this.nativeObject, image.NativeObject, destPoints, destPoints.Length, srcRect.X, srcRect.Y, srcRect.Width, srcRect.Height, srcUnit, (imageAttr != null) ? imageAttr.nativeImageAttributes : IntPtr.Zero, callback, IntPtr.Zero));
		}

		/// <summary>Draws the specified portion of the specified <see cref="T:System.Drawing.Image" /> at the specified location and with the specified size.</summary>
		/// <param name="image">
		///   <see cref="T:System.Drawing.Image" /> to draw. </param>
		/// <param name="destPoints">Array of three <see cref="T:System.Drawing.PointF" /> structures that define a parallelogram. </param>
		/// <param name="srcRect">
		///   <see cref="T:System.Drawing.Rectangle" /> structure that specifies the portion of the <paramref name="image" /> object to draw. </param>
		/// <param name="srcUnit">Member of the <see cref="T:System.Drawing.GraphicsUnit" /> enumeration that specifies the units of measure used by the <paramref name="srcRect" /> parameter. </param>
		/// <param name="imageAttr">
		///   <see cref="T:System.Drawing.Imaging.ImageAttributes" /> that specifies recoloring and gamma information for the <paramref name="image" /> object. </param>
		/// <param name="callback">
		///   <see cref="T:System.Drawing.Graphics.DrawImageAbort" /> delegate that specifies a method to call during the drawing of the image. This method is called frequently to check whether to stop execution of the <see cref="M:System.Drawing.Graphics.DrawImage(System.Drawing.Image,System.Drawing.Point[],System.Drawing.Rectangle,System.Drawing.GraphicsUnit,System.Drawing.Imaging.ImageAttributes,System.Drawing.Graphics.DrawImageAbort,System.Int32)" /> method according to application-determined criteria. </param>
		/// <param name="callbackData">Value specifying additional data for the <see cref="T:System.Drawing.Graphics.DrawImageAbort" /> delegate to use when checking whether to stop execution of the <see cref="M:System.Drawing.Graphics.DrawImage(System.Drawing.Image,System.Drawing.Point[],System.Drawing.Rectangle,System.Drawing.GraphicsUnit,System.Drawing.Imaging.ImageAttributes,System.Drawing.Graphics.DrawImageAbort,System.Int32)" /> method. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060003DD RID: 989 RVA: 0x0000A708 File Offset: 0x00008908
		public void DrawImage(Image image, Point[] destPoints, Rectangle srcRect, GraphicsUnit srcUnit, ImageAttributes imageAttr, Graphics.DrawImageAbort callback, int callbackData)
		{
			if (image == null)
			{
				throw new ArgumentNullException("image");
			}
			if (destPoints == null)
			{
				throw new ArgumentNullException("destPoints");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipDrawImagePointsRectI(this.nativeObject, image.NativeObject, destPoints, destPoints.Length, srcRect.X, srcRect.Y, srcRect.Width, srcRect.Height, srcUnit, (imageAttr != null) ? imageAttr.nativeImageAttributes : IntPtr.Zero, callback, (IntPtr)callbackData));
		}

		/// <summary>Draws the specified portion of the specified <see cref="T:System.Drawing.Image" /> at the specified location and with the specified size.</summary>
		/// <param name="image">
		///   <see cref="T:System.Drawing.Image" /> to draw. </param>
		/// <param name="destRect">
		///   <see cref="T:System.Drawing.Rectangle" /> structure that specifies the location and size of the drawn image. The image is scaled to fit the rectangle. </param>
		/// <param name="srcX">The x-coordinate of the upper-left corner of the portion of the source image to draw. </param>
		/// <param name="srcY">The y-coordinate of the upper-left corner of the portion of the source image to draw. </param>
		/// <param name="srcWidth">Width of the portion of the source image to draw. </param>
		/// <param name="srcHeight">Height of the portion of the source image to draw. </param>
		/// <param name="srcUnit">Member of the <see cref="T:System.Drawing.GraphicsUnit" /> enumeration that specifies the units of measure used to determine the source rectangle. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="image" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060003DE RID: 990 RVA: 0x0000A784 File Offset: 0x00008984
		public void DrawImage(Image image, Rectangle destRect, float srcX, float srcY, float srcWidth, float srcHeight, GraphicsUnit srcUnit)
		{
			if (image == null)
			{
				throw new ArgumentNullException("image");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipDrawImageRectRect(this.nativeObject, image.NativeObject, (float)destRect.X, (float)destRect.Y, (float)destRect.Width, (float)destRect.Height, srcX, srcY, srcWidth, srcHeight, srcUnit, IntPtr.Zero, null, IntPtr.Zero));
		}

		/// <summary>Draws the specified portion of the specified <see cref="T:System.Drawing.Image" /> at the specified location and with the specified size.</summary>
		/// <param name="image">
		///   <see cref="T:System.Drawing.Image" /> to draw. </param>
		/// <param name="destPoints">Array of three <see cref="T:System.Drawing.PointF" /> structures that define a parallelogram. </param>
		/// <param name="srcRect">
		///   <see cref="T:System.Drawing.RectangleF" /> structure that specifies the portion of the <paramref name="image" /> object to draw. </param>
		/// <param name="srcUnit">Member of the <see cref="T:System.Drawing.GraphicsUnit" /> enumeration that specifies the units of measure used by the <paramref name="srcRect" /> parameter. </param>
		/// <param name="imageAttr">
		///   <see cref="T:System.Drawing.Imaging.ImageAttributes" /> that specifies recoloring and gamma information for the <paramref name="image" /> object. </param>
		/// <param name="callback">
		///   <see cref="T:System.Drawing.Graphics.DrawImageAbort" /> delegate that specifies a method to call during the drawing of the image. This method is called frequently to check whether to stop execution of the <see cref="M:System.Drawing.Graphics.DrawImage(System.Drawing.Image,System.Drawing.PointF[],System.Drawing.RectangleF,System.Drawing.GraphicsUnit,System.Drawing.Imaging.ImageAttributes,System.Drawing.Graphics.DrawImageAbort,System.Int32)" /> method according to application-determined criteria. </param>
		/// <param name="callbackData">Value specifying additional data for the <see cref="T:System.Drawing.Graphics.DrawImageAbort" /> delegate to use when checking whether to stop execution of the <see cref="M:System.Drawing.Graphics.DrawImage(System.Drawing.Image,System.Drawing.PointF[],System.Drawing.RectangleF,System.Drawing.GraphicsUnit,System.Drawing.Imaging.ImageAttributes,System.Drawing.Graphics.DrawImageAbort,System.Int32)" /> method. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="image" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060003DF RID: 991 RVA: 0x0000A7EC File Offset: 0x000089EC
		public void DrawImage(Image image, PointF[] destPoints, RectangleF srcRect, GraphicsUnit srcUnit, ImageAttributes imageAttr, Graphics.DrawImageAbort callback, int callbackData)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipDrawImagePointsRect(this.nativeObject, image.NativeObject, destPoints, destPoints.Length, srcRect.X, srcRect.Y, srcRect.Width, srcRect.Height, srcUnit, (imageAttr != null) ? imageAttr.nativeImageAttributes : IntPtr.Zero, callback, (IntPtr)callbackData));
		}

		/// <summary>Draws the specified portion of the specified <see cref="T:System.Drawing.Image" /> at the specified location and with the specified size.</summary>
		/// <param name="image">
		///   <see cref="T:System.Drawing.Image" /> to draw. </param>
		/// <param name="destRect">
		///   <see cref="T:System.Drawing.Rectangle" /> structure that specifies the location and size of the drawn image. The image is scaled to fit the rectangle. </param>
		/// <param name="srcX">The x-coordinate of the upper-left corner of the portion of the source image to draw. </param>
		/// <param name="srcY">The y-coordinate of the upper-left corner of the portion of the source image to draw. </param>
		/// <param name="srcWidth">Width of the portion of the source image to draw. </param>
		/// <param name="srcHeight">Height of the portion of the source image to draw. </param>
		/// <param name="srcUnit">Member of the <see cref="T:System.Drawing.GraphicsUnit" /> enumeration that specifies the units of measure used to determine the source rectangle. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="image" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060003E0 RID: 992 RVA: 0x0000A84C File Offset: 0x00008A4C
		public void DrawImage(Image image, Rectangle destRect, int srcX, int srcY, int srcWidth, int srcHeight, GraphicsUnit srcUnit)
		{
			if (image == null)
			{
				throw new ArgumentNullException("image");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipDrawImageRectRectI(this.nativeObject, image.NativeObject, destRect.X, destRect.Y, destRect.Width, destRect.Height, srcX, srcY, srcWidth, srcHeight, srcUnit, IntPtr.Zero, null, IntPtr.Zero));
		}

		/// <summary>Draws the specified portion of the specified <see cref="T:System.Drawing.Image" /> at the specified location and with the specified size.</summary>
		/// <param name="image">
		///   <see cref="T:System.Drawing.Image" /> to draw. </param>
		/// <param name="destRect">
		///   <see cref="T:System.Drawing.Rectangle" /> structure that specifies the location and size of the drawn image. The image is scaled to fit the rectangle. </param>
		/// <param name="srcX">The x-coordinate of the upper-left corner of the portion of the source image to draw. </param>
		/// <param name="srcY">The y-coordinate of the upper-left corner of the portion of the source image to draw. </param>
		/// <param name="srcWidth">Width of the portion of the source image to draw. </param>
		/// <param name="srcHeight">Height of the portion of the source image to draw. </param>
		/// <param name="srcUnit">Member of the <see cref="T:System.Drawing.GraphicsUnit" /> enumeration that specifies the units of measure used to determine the source rectangle. </param>
		/// <param name="imageAttrs">
		///   <see cref="T:System.Drawing.Imaging.ImageAttributes" /> that specifies recoloring and gamma information for the <paramref name="image" /> object. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="image" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060003E1 RID: 993 RVA: 0x0000A8B0 File Offset: 0x00008AB0
		public void DrawImage(Image image, Rectangle destRect, float srcX, float srcY, float srcWidth, float srcHeight, GraphicsUnit srcUnit, ImageAttributes imageAttrs)
		{
			if (image == null)
			{
				throw new ArgumentNullException("image");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipDrawImageRectRect(this.nativeObject, image.NativeObject, (float)destRect.X, (float)destRect.Y, (float)destRect.Width, (float)destRect.Height, srcX, srcY, srcWidth, srcHeight, srcUnit, (imageAttrs != null) ? imageAttrs.nativeImageAttributes : IntPtr.Zero, null, IntPtr.Zero));
		}

		/// <summary>Draws the specified portion of the specified <see cref="T:System.Drawing.Image" /> at the specified location and with the specified size.</summary>
		/// <param name="image">
		///   <see cref="T:System.Drawing.Image" /> to draw. </param>
		/// <param name="destRect">
		///   <see cref="T:System.Drawing.Rectangle" /> structure that specifies the location and size of the drawn image. The image is scaled to fit the rectangle. </param>
		/// <param name="srcX">The x-coordinate of the upper-left corner of the portion of the source image to draw. </param>
		/// <param name="srcY">The y-coordinate of the upper-left corner of the portion of the source image to draw. </param>
		/// <param name="srcWidth">Width of the portion of the source image to draw. </param>
		/// <param name="srcHeight">Height of the portion of the source image to draw. </param>
		/// <param name="srcUnit">Member of the <see cref="T:System.Drawing.GraphicsUnit" /> enumeration that specifies the units of measure used to determine the source rectangle. </param>
		/// <param name="imageAttr">
		///   <see cref="T:System.Drawing.Imaging.ImageAttributes" /> that specifies recoloring and gamma information for the <paramref name="image" /> object. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="image" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060003E2 RID: 994 RVA: 0x0000A924 File Offset: 0x00008B24
		public void DrawImage(Image image, Rectangle destRect, int srcX, int srcY, int srcWidth, int srcHeight, GraphicsUnit srcUnit, ImageAttributes imageAttr)
		{
			if (image == null)
			{
				throw new ArgumentNullException("image");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipDrawImageRectRectI(this.nativeObject, image.NativeObject, destRect.X, destRect.Y, destRect.Width, destRect.Height, srcX, srcY, srcWidth, srcHeight, srcUnit, (imageAttr != null) ? imageAttr.nativeImageAttributes : IntPtr.Zero, null, IntPtr.Zero));
		}

		/// <summary>Draws the specified portion of the specified <see cref="T:System.Drawing.Image" /> at the specified location and with the specified size.</summary>
		/// <param name="image">
		///   <see cref="T:System.Drawing.Image" /> to draw. </param>
		/// <param name="destRect">
		///   <see cref="T:System.Drawing.Rectangle" /> structure that specifies the location and size of the drawn image. The image is scaled to fit the rectangle. </param>
		/// <param name="srcX">The x-coordinate of the upper-left corner of the portion of the source image to draw. </param>
		/// <param name="srcY">The y-coordinate of the upper-left corner of the portion of the source image to draw. </param>
		/// <param name="srcWidth">Width of the portion of the source image to draw. </param>
		/// <param name="srcHeight">Height of the portion of the source image to draw. </param>
		/// <param name="srcUnit">Member of the <see cref="T:System.Drawing.GraphicsUnit" /> enumeration that specifies the units of measure used to determine the source rectangle. </param>
		/// <param name="imageAttr">
		///   <see cref="T:System.Drawing.Imaging.ImageAttributes" /> that specifies recoloring and gamma information for <paramref name="image" />. </param>
		/// <param name="callback">
		///   <see cref="T:System.Drawing.Graphics.DrawImageAbort" /> delegate that specifies a method to call during the drawing of the image. This method is called frequently to check whether to stop execution of the <see cref="M:System.Drawing.Graphics.DrawImage(System.Drawing.Image,System.Drawing.Rectangle,System.Int32,System.Int32,System.Int32,System.Int32,System.Drawing.GraphicsUnit,System.Drawing.Imaging.ImageAttributes,System.Drawing.Graphics.DrawImageAbort)" /> method according to application-determined criteria. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="image" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060003E3 RID: 995 RVA: 0x0000A994 File Offset: 0x00008B94
		public void DrawImage(Image image, Rectangle destRect, int srcX, int srcY, int srcWidth, int srcHeight, GraphicsUnit srcUnit, ImageAttributes imageAttr, Graphics.DrawImageAbort callback)
		{
			if (image == null)
			{
				throw new ArgumentNullException("image");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipDrawImageRectRectI(this.nativeObject, image.NativeObject, destRect.X, destRect.Y, destRect.Width, destRect.Height, srcX, srcY, srcWidth, srcHeight, srcUnit, (imageAttr != null) ? imageAttr.nativeImageAttributes : IntPtr.Zero, callback, IntPtr.Zero));
		}

		/// <summary>Draws the specified portion of the specified <see cref="T:System.Drawing.Image" /> at the specified location and with the specified size.</summary>
		/// <param name="image">
		///   <see cref="T:System.Drawing.Image" /> to draw. </param>
		/// <param name="destRect">
		///   <see cref="T:System.Drawing.Rectangle" /> structure that specifies the location and size of the drawn image. The image is scaled to fit the rectangle. </param>
		/// <param name="srcX">The x-coordinate of the upper-left corner of the portion of the source image to draw. </param>
		/// <param name="srcY">The y-coordinate of the upper-left corner of the portion of the source image to draw. </param>
		/// <param name="srcWidth">Width of the portion of the source image to draw. </param>
		/// <param name="srcHeight">Height of the portion of the source image to draw. </param>
		/// <param name="srcUnit">Member of the <see cref="T:System.Drawing.GraphicsUnit" /> enumeration that specifies the units of measure used to determine the source rectangle. </param>
		/// <param name="imageAttrs">
		///   <see cref="T:System.Drawing.Imaging.ImageAttributes" /> that specifies recoloring and gamma information for the <paramref name="image" /> object. </param>
		/// <param name="callback">
		///   <see cref="T:System.Drawing.Graphics.DrawImageAbort" /> delegate that specifies a method to call during the drawing of the image. This method is called frequently to check whether to stop execution of the <see cref="M:System.Drawing.Graphics.DrawImage(System.Drawing.Image,System.Drawing.Rectangle,System.Single,System.Single,System.Single,System.Single,System.Drawing.GraphicsUnit,System.Drawing.Imaging.ImageAttributes,System.Drawing.Graphics.DrawImageAbort)" /> method according to application-determined criteria. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="image" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060003E4 RID: 996 RVA: 0x0000AA04 File Offset: 0x00008C04
		public void DrawImage(Image image, Rectangle destRect, float srcX, float srcY, float srcWidth, float srcHeight, GraphicsUnit srcUnit, ImageAttributes imageAttrs, Graphics.DrawImageAbort callback)
		{
			if (image == null)
			{
				throw new ArgumentNullException("image");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipDrawImageRectRect(this.nativeObject, image.NativeObject, (float)destRect.X, (float)destRect.Y, (float)destRect.Width, (float)destRect.Height, srcX, srcY, srcWidth, srcHeight, srcUnit, (imageAttrs != null) ? imageAttrs.nativeImageAttributes : IntPtr.Zero, callback, IntPtr.Zero));
		}

		/// <summary>Draws the specified portion of the specified <see cref="T:System.Drawing.Image" /> at the specified location and with the specified size.</summary>
		/// <param name="image">
		///   <see cref="T:System.Drawing.Image" /> to draw. </param>
		/// <param name="destRect">
		///   <see cref="T:System.Drawing.Rectangle" /> structure that specifies the location and size of the drawn image. The image is scaled to fit the rectangle. </param>
		/// <param name="srcX">The x-coordinate of the upper-left corner of the portion of the source image to draw. </param>
		/// <param name="srcY">The y-coordinate of the upper-left corner of the portion of the source image to draw. </param>
		/// <param name="srcWidth">Width of the portion of the source image to draw. </param>
		/// <param name="srcHeight">Height of the portion of the source image to draw. </param>
		/// <param name="srcUnit">Member of the <see cref="T:System.Drawing.GraphicsUnit" /> enumeration that specifies the units of measure used to determine the source rectangle. </param>
		/// <param name="imageAttrs">
		///   <see cref="T:System.Drawing.Imaging.ImageAttributes" /> that specifies recoloring and gamma information for the <paramref name="image" /> object. </param>
		/// <param name="callback">
		///   <see cref="T:System.Drawing.Graphics.DrawImageAbort" /> delegate that specifies a method to call during the drawing of the image. This method is called frequently to check whether to stop execution of the <see cref="M:System.Drawing.Graphics.DrawImage(System.Drawing.Image,System.Drawing.Rectangle,System.Single,System.Single,System.Single,System.Single,System.Drawing.GraphicsUnit,System.Drawing.Imaging.ImageAttributes,System.Drawing.Graphics.DrawImageAbort,System.IntPtr)" /> method according to application-determined criteria. </param>
		/// <param name="callbackData">Value specifying additional data for the <see cref="T:System.Drawing.Graphics.DrawImageAbort" /> delegate to use when checking whether to stop execution of the DrawImage method. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="image" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060003E5 RID: 997 RVA: 0x0000AA78 File Offset: 0x00008C78
		public void DrawImage(Image image, Rectangle destRect, float srcX, float srcY, float srcWidth, float srcHeight, GraphicsUnit srcUnit, ImageAttributes imageAttrs, Graphics.DrawImageAbort callback, IntPtr callbackData)
		{
			if (image == null)
			{
				throw new ArgumentNullException("image");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipDrawImageRectRect(this.nativeObject, image.NativeObject, (float)destRect.X, (float)destRect.Y, (float)destRect.Width, (float)destRect.Height, srcX, srcY, srcWidth, srcHeight, srcUnit, (imageAttrs != null) ? imageAttrs.nativeImageAttributes : IntPtr.Zero, callback, callbackData));
		}

		/// <summary>Draws the specified portion of the specified <see cref="T:System.Drawing.Image" /> at the specified location and with the specified size.</summary>
		/// <param name="image">
		///   <see cref="T:System.Drawing.Image" /> to draw. </param>
		/// <param name="destRect">
		///   <see cref="T:System.Drawing.Rectangle" /> structure that specifies the location and size of the drawn image. The image is scaled to fit the rectangle. </param>
		/// <param name="srcX">The x-coordinate of the upper-left corner of the portion of the source image to draw. </param>
		/// <param name="srcY">The y-coordinate of the upper-left corner of the portion of the source image to draw. </param>
		/// <param name="srcWidth">Width of the portion of the source image to draw. </param>
		/// <param name="srcHeight">Height of the portion of the source image to draw. </param>
		/// <param name="srcUnit">Member of the <see cref="T:System.Drawing.GraphicsUnit" /> enumeration that specifies the units of measure used to determine the source rectangle. </param>
		/// <param name="imageAttrs">
		///   <see cref="T:System.Drawing.Imaging.ImageAttributes" /> that specifies recoloring and gamma information for the <paramref name="image" /> object. </param>
		/// <param name="callback">
		///   <see cref="T:System.Drawing.Graphics.DrawImageAbort" /> delegate that specifies a method to call during the drawing of the image. This method is called frequently to check whether to stop execution of the <see cref="M:System.Drawing.Graphics.DrawImage(System.Drawing.Image,System.Drawing.Rectangle,System.Int32,System.Int32,System.Int32,System.Int32,System.Drawing.GraphicsUnit,System.Drawing.Imaging.ImageAttributes,System.Drawing.Graphics.DrawImageAbort,System.IntPtr)" /> method according to application-determined criteria. </param>
		/// <param name="callbackData">Value specifying additional data for the <see cref="T:System.Drawing.Graphics.DrawImageAbort" /> delegate to use when checking whether to stop execution of the DrawImage method. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="image" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060003E6 RID: 998 RVA: 0x0000AAE8 File Offset: 0x00008CE8
		public void DrawImage(Image image, Rectangle destRect, int srcX, int srcY, int srcWidth, int srcHeight, GraphicsUnit srcUnit, ImageAttributes imageAttrs, Graphics.DrawImageAbort callback, IntPtr callbackData)
		{
			if (image == null)
			{
				throw new ArgumentNullException("image");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipDrawImageRectRect(this.nativeObject, image.NativeObject, (float)destRect.X, (float)destRect.Y, (float)destRect.Width, (float)destRect.Height, (float)srcX, (float)srcY, (float)srcWidth, (float)srcHeight, srcUnit, (imageAttrs != null) ? imageAttrs.nativeImageAttributes : IntPtr.Zero, callback, callbackData));
		}

		/// <summary>Draws a specified image using its original physical size at a specified location.</summary>
		/// <param name="image">
		///   <see cref="T:System.Drawing.Image" /> to draw. </param>
		/// <param name="point">
		///   <see cref="T:System.Drawing.Point" /> structure that specifies the upper-left corner of the drawn image. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="image" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060003E7 RID: 999 RVA: 0x0000AB5C File Offset: 0x00008D5C
		public void DrawImageUnscaled(Image image, Point point)
		{
			this.DrawImageUnscaled(image, point.X, point.Y);
		}

		/// <summary>Draws a specified image using its original physical size at a specified location.</summary>
		/// <param name="image">
		///   <see cref="T:System.Drawing.Image" /> to draw. </param>
		/// <param name="rect">
		///   <see cref="T:System.Drawing.Rectangle" /> that specifies the upper-left corner of the drawn image. The X and Y properties of the rectangle specify the upper-left corner. The Width and Height properties are ignored. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="image" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060003E8 RID: 1000 RVA: 0x0000AB73 File Offset: 0x00008D73
		public void DrawImageUnscaled(Image image, Rectangle rect)
		{
			this.DrawImageUnscaled(image, rect.X, rect.Y, rect.Width, rect.Height);
		}

		/// <summary>Draws the specified image using its original physical size at the location specified by a coordinate pair.</summary>
		/// <param name="image">
		///   <see cref="T:System.Drawing.Image" /> to draw. </param>
		/// <param name="x">The x-coordinate of the upper-left corner of the drawn image. </param>
		/// <param name="y">The y-coordinate of the upper-left corner of the drawn image. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="image" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060003E9 RID: 1001 RVA: 0x0000AB98 File Offset: 0x00008D98
		public void DrawImageUnscaled(Image image, int x, int y)
		{
			if (image == null)
			{
				throw new ArgumentNullException("image");
			}
			this.DrawImage(image, x, y, image.Width, image.Height);
		}

		/// <summary>Draws a specified image using its original physical size at a specified location.</summary>
		/// <param name="image">
		///   <see cref="T:System.Drawing.Image" /> to draw. </param>
		/// <param name="x">The x-coordinate of the upper-left corner of the drawn image. </param>
		/// <param name="y">The y-coordinate of the upper-left corner of the drawn image. </param>
		/// <param name="width">Not used. </param>
		/// <param name="height">Not used. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="image" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060003EA RID: 1002 RVA: 0x0000ABC0 File Offset: 0x00008DC0
		public void DrawImageUnscaled(Image image, int x, int y, int width, int height)
		{
			if (image == null)
			{
				throw new ArgumentNullException("image");
			}
			if (width <= 0 || height <= 0)
			{
				return;
			}
			using (Image image2 = new Bitmap(width, height))
			{
				using (Graphics graphics = Graphics.FromImage(image2))
				{
					graphics.DrawImage(image, 0, 0, image.Width, image.Height);
					this.DrawImage(image2, x, y, width, height);
				}
			}
		}

		/// <summary>Draws the specified image without scaling and clips it, if necessary, to fit in the specified rectangle.</summary>
		/// <param name="image">The <see cref="T:System.Drawing.Image" /> to draw.</param>
		/// <param name="rect">The <see cref="T:System.Drawing.Rectangle" /> in which to draw the image.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="image" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060003EB RID: 1003 RVA: 0x0000AC4C File Offset: 0x00008E4C
		public void DrawImageUnscaledAndClipped(Image image, Rectangle rect)
		{
			if (image == null)
			{
				throw new ArgumentNullException("image");
			}
			int num = ((image.Width > rect.Width) ? rect.Width : image.Width);
			int num2 = ((image.Height > rect.Height) ? rect.Height : image.Height);
			this.DrawImageUnscaled(image, rect.X, rect.Y, num, num2);
		}

		/// <summary>Draws a line connecting two <see cref="T:System.Drawing.PointF" /> structures.</summary>
		/// <param name="pen">
		///   <see cref="T:System.Drawing.Pen" /> that determines the color, width, and style of the line. </param>
		/// <param name="pt1">
		///   <see cref="T:System.Drawing.PointF" /> structure that represents the first point to connect. </param>
		/// <param name="pt2">
		///   <see cref="T:System.Drawing.PointF" /> structure that represents the second point to connect. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pen" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060003EC RID: 1004 RVA: 0x0000ACBC File Offset: 0x00008EBC
		public void DrawLine(Pen pen, PointF pt1, PointF pt2)
		{
			if (pen == null)
			{
				throw new ArgumentNullException("pen");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipDrawLine(this.nativeObject, pen.NativePen, pt1.X, pt1.Y, pt2.X, pt2.Y));
		}

		/// <summary>Draws a line connecting two <see cref="T:System.Drawing.Point" /> structures.</summary>
		/// <param name="pen">
		///   <see cref="T:System.Drawing.Pen" /> that determines the color, width, and style of the line. </param>
		/// <param name="pt1">
		///   <see cref="T:System.Drawing.Point" /> structure that represents the first point to connect. </param>
		/// <param name="pt2">
		///   <see cref="T:System.Drawing.Point" /> structure that represents the second point to connect. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pen" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060003ED RID: 1005 RVA: 0x0000AD0C File Offset: 0x00008F0C
		public void DrawLine(Pen pen, Point pt1, Point pt2)
		{
			if (pen == null)
			{
				throw new ArgumentNullException("pen");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipDrawLineI(this.nativeObject, pen.NativePen, pt1.X, pt1.Y, pt2.X, pt2.Y));
		}

		/// <summary>Draws a line connecting the two points specified by the coordinate pairs.</summary>
		/// <param name="pen">
		///   <see cref="T:System.Drawing.Pen" /> that determines the color, width, and style of the line. </param>
		/// <param name="x1">The x-coordinate of the first point. </param>
		/// <param name="y1">The y-coordinate of the first point. </param>
		/// <param name="x2">The x-coordinate of the second point. </param>
		/// <param name="y2">The y-coordinate of the second point. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pen" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060003EE RID: 1006 RVA: 0x0000AD59 File Offset: 0x00008F59
		public void DrawLine(Pen pen, int x1, int y1, int x2, int y2)
		{
			if (pen == null)
			{
				throw new ArgumentNullException("pen");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipDrawLineI(this.nativeObject, pen.NativePen, x1, y1, x2, y2));
		}

		/// <summary>Draws a line connecting the two points specified by the coordinate pairs.</summary>
		/// <param name="pen">
		///   <see cref="T:System.Drawing.Pen" /> that determines the color, width, and style of the line. </param>
		/// <param name="x1">The x-coordinate of the first point. </param>
		/// <param name="y1">The y-coordinate of the first point. </param>
		/// <param name="x2">The x-coordinate of the second point. </param>
		/// <param name="y2">The y-coordinate of the second point. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pen" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060003EF RID: 1007 RVA: 0x0000AD88 File Offset: 0x00008F88
		public void DrawLine(Pen pen, float x1, float y1, float x2, float y2)
		{
			if (pen == null)
			{
				throw new ArgumentNullException("pen");
			}
			if (!float.IsNaN(x1) && !float.IsNaN(y1) && !float.IsNaN(x2) && !float.IsNaN(y2))
			{
				GDIPlus.CheckStatus(GDIPlus.GdipDrawLine(this.nativeObject, pen.NativePen, x1, y1, x2, y2));
			}
		}

		/// <summary>Draws a series of line segments that connect an array of <see cref="T:System.Drawing.PointF" /> structures.</summary>
		/// <param name="pen">
		///   <see cref="T:System.Drawing.Pen" /> that determines the color, width, and style of the line segments. </param>
		/// <param name="points">Array of <see cref="T:System.Drawing.PointF" /> structures that represent the points to connect. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pen" /> is null.-or-<paramref name="points" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060003F0 RID: 1008 RVA: 0x0000ADE1 File Offset: 0x00008FE1
		public void DrawLines(Pen pen, PointF[] points)
		{
			if (pen == null)
			{
				throw new ArgumentNullException("pen");
			}
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipDrawLines(this.nativeObject, pen.NativePen, points, points.Length));
		}

		/// <summary>Draws a series of line segments that connect an array of <see cref="T:System.Drawing.Point" /> structures.</summary>
		/// <param name="pen">
		///   <see cref="T:System.Drawing.Pen" /> that determines the color, width, and style of the line segments. </param>
		/// <param name="points">Array of <see cref="T:System.Drawing.Point" /> structures that represent the points to connect. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pen" /> is null.-or-<paramref name="points" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060003F1 RID: 1009 RVA: 0x0000AE19 File Offset: 0x00009019
		public void DrawLines(Pen pen, Point[] points)
		{
			if (pen == null)
			{
				throw new ArgumentNullException("pen");
			}
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipDrawLinesI(this.nativeObject, pen.NativePen, points, points.Length));
		}

		/// <summary>Draws a <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</summary>
		/// <param name="pen">
		///   <see cref="T:System.Drawing.Pen" /> that determines the color, width, and style of the path. </param>
		/// <param name="path">
		///   <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> to draw. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pen" /> is null.-or-<paramref name="path" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060003F2 RID: 1010 RVA: 0x0000AE51 File Offset: 0x00009051
		public void DrawPath(Pen pen, GraphicsPath path)
		{
			if (pen == null)
			{
				throw new ArgumentNullException("pen");
			}
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipDrawPath(this.nativeObject, pen.NativePen, path.nativePath));
		}

		/// <summary>Draws a pie shape defined by an ellipse specified by a <see cref="T:System.Drawing.Rectangle" /> structure and two radial lines.</summary>
		/// <param name="pen">
		///   <see cref="T:System.Drawing.Pen" /> that determines the color, width, and style of the pie shape. </param>
		/// <param name="rect">
		///   <see cref="T:System.Drawing.Rectangle" /> structure that represents the bounding rectangle that defines the ellipse from which the pie shape comes. </param>
		/// <param name="startAngle">Angle measured in degrees clockwise from the x-axis to the first side of the pie shape. </param>
		/// <param name="sweepAngle">Angle measured in degrees clockwise from the <paramref name="startAngle" /> parameter to the second side of the pie shape. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pen" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060003F3 RID: 1011 RVA: 0x0000AE8B File Offset: 0x0000908B
		public void DrawPie(Pen pen, Rectangle rect, float startAngle, float sweepAngle)
		{
			if (pen == null)
			{
				throw new ArgumentNullException("pen");
			}
			this.DrawPie(pen, (float)rect.X, (float)rect.Y, (float)rect.Width, (float)rect.Height, startAngle, sweepAngle);
		}

		/// <summary>Draws a pie shape defined by an ellipse specified by a <see cref="T:System.Drawing.RectangleF" /> structure and two radial lines.</summary>
		/// <param name="pen">
		///   <see cref="T:System.Drawing.Pen" /> that determines the color, width, and style of the pie shape. </param>
		/// <param name="rect">
		///   <see cref="T:System.Drawing.RectangleF" /> structure that represents the bounding rectangle that defines the ellipse from which the pie shape comes. </param>
		/// <param name="startAngle">Angle measured in degrees clockwise from the x-axis to the first side of the pie shape. </param>
		/// <param name="sweepAngle">Angle measured in degrees clockwise from the <paramref name="startAngle" /> parameter to the second side of the pie shape. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pen" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060003F4 RID: 1012 RVA: 0x0000AEC5 File Offset: 0x000090C5
		public void DrawPie(Pen pen, RectangleF rect, float startAngle, float sweepAngle)
		{
			if (pen == null)
			{
				throw new ArgumentNullException("pen");
			}
			this.DrawPie(pen, rect.X, rect.Y, rect.Width, rect.Height, startAngle, sweepAngle);
		}

		/// <summary>Draws a pie shape defined by an ellipse specified by a coordinate pair, a width, a height, and two radial lines.</summary>
		/// <param name="pen">
		///   <see cref="T:System.Drawing.Pen" /> that determines the color, width, and style of the pie shape. </param>
		/// <param name="x">The x-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse from which the pie shape comes. </param>
		/// <param name="y">The y-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse from which the pie shape comes. </param>
		/// <param name="width">Width of the bounding rectangle that defines the ellipse from which the pie shape comes. </param>
		/// <param name="height">Height of the bounding rectangle that defines the ellipse from which the pie shape comes. </param>
		/// <param name="startAngle">Angle measured in degrees clockwise from the x-axis to the first side of the pie shape. </param>
		/// <param name="sweepAngle">Angle measured in degrees clockwise from the <paramref name="startAngle" /> parameter to the second side of the pie shape. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pen" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060003F5 RID: 1013 RVA: 0x0000AEFB File Offset: 0x000090FB
		public void DrawPie(Pen pen, float x, float y, float width, float height, float startAngle, float sweepAngle)
		{
			if (pen == null)
			{
				throw new ArgumentNullException("pen");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipDrawPie(this.nativeObject, pen.NativePen, x, y, width, height, startAngle, sweepAngle));
		}

		/// <summary>Draws a pie shape defined by an ellipse specified by a coordinate pair, a width, a height, and two radial lines.</summary>
		/// <param name="pen">
		///   <see cref="T:System.Drawing.Pen" /> that determines the color, width, and style of the pie shape. </param>
		/// <param name="x">The x-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse from which the pie shape comes. </param>
		/// <param name="y">The y-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse from which the pie shape comes. </param>
		/// <param name="width">Width of the bounding rectangle that defines the ellipse from which the pie shape comes. </param>
		/// <param name="height">Height of the bounding rectangle that defines the ellipse from which the pie shape comes. </param>
		/// <param name="startAngle">Angle measured in degrees clockwise from the x-axis to the first side of the pie shape. </param>
		/// <param name="sweepAngle">Angle measured in degrees clockwise from the <paramref name="startAngle" /> parameter to the second side of the pie shape. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pen" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060003F6 RID: 1014 RVA: 0x0000AF2B File Offset: 0x0000912B
		public void DrawPie(Pen pen, int x, int y, int width, int height, int startAngle, int sweepAngle)
		{
			if (pen == null)
			{
				throw new ArgumentNullException("pen");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipDrawPieI(this.nativeObject, pen.NativePen, x, y, width, height, (float)startAngle, (float)sweepAngle));
		}

		/// <summary>Draws a polygon defined by an array of <see cref="T:System.Drawing.Point" /> structures.</summary>
		/// <param name="pen">
		///   <see cref="T:System.Drawing.Pen" /> that determines the color, width, and style of the polygon. </param>
		/// <param name="points">Array of <see cref="T:System.Drawing.Point" /> structures that represent the vertices of the polygon. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pen" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060003F7 RID: 1015 RVA: 0x0000AF5D File Offset: 0x0000915D
		public void DrawPolygon(Pen pen, Point[] points)
		{
			if (pen == null)
			{
				throw new ArgumentNullException("pen");
			}
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipDrawPolygonI(this.nativeObject, pen.NativePen, points, points.Length));
		}

		/// <summary>Draws a polygon defined by an array of <see cref="T:System.Drawing.PointF" /> structures.</summary>
		/// <param name="pen">
		///   <see cref="T:System.Drawing.Pen" /> that determines the color, width, and style of the polygon. </param>
		/// <param name="points">Array of <see cref="T:System.Drawing.PointF" /> structures that represent the vertices of the polygon. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pen" /> is null.-or-<paramref name="points" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060003F8 RID: 1016 RVA: 0x0000AF95 File Offset: 0x00009195
		public void DrawPolygon(Pen pen, PointF[] points)
		{
			if (pen == null)
			{
				throw new ArgumentNullException("pen");
			}
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipDrawPolygon(this.nativeObject, pen.NativePen, points, points.Length));
		}

		/// <summary>Draws a rectangle specified by a <see cref="T:System.Drawing.Rectangle" /> structure.</summary>
		/// <param name="pen">A <see cref="T:System.Drawing.Pen" /> that determines the color, width, and style of the rectangle. </param>
		/// <param name="rect">A <see cref="T:System.Drawing.Rectangle" /> structure that represents the rectangle to draw. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pen" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060003F9 RID: 1017 RVA: 0x0000AFCD File Offset: 0x000091CD
		public void DrawRectangle(Pen pen, Rectangle rect)
		{
			if (pen == null)
			{
				throw new ArgumentNullException("pen");
			}
			this.DrawRectangle(pen, rect.Left, rect.Top, rect.Width, rect.Height);
		}

		/// <summary>Draws a rectangle specified by a coordinate pair, a width, and a height.</summary>
		/// <param name="pen">A <see cref="T:System.Drawing.Pen" /> that determines the color, width, and style of the rectangle. </param>
		/// <param name="x">The x-coordinate of the upper-left corner of the rectangle to draw. </param>
		/// <param name="y">The y-coordinate of the upper-left corner of the rectangle to draw. </param>
		/// <param name="width">The width of the rectangle to draw. </param>
		/// <param name="height">The height of the rectangle to draw. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pen" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060003FA RID: 1018 RVA: 0x0000B000 File Offset: 0x00009200
		public void DrawRectangle(Pen pen, float x, float y, float width, float height)
		{
			if (pen == null)
			{
				throw new ArgumentNullException("pen");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipDrawRectangle(this.nativeObject, pen.NativePen, x, y, width, height));
		}

		/// <summary>Draws a rectangle specified by a coordinate pair, a width, and a height.</summary>
		/// <param name="pen">
		///   <see cref="T:System.Drawing.Pen" /> that determines the color, width, and style of the rectangle. </param>
		/// <param name="x">The x-coordinate of the upper-left corner of the rectangle to draw. </param>
		/// <param name="y">The y-coordinate of the upper-left corner of the rectangle to draw. </param>
		/// <param name="width">Width of the rectangle to draw. </param>
		/// <param name="height">Height of the rectangle to draw. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pen" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060003FB RID: 1019 RVA: 0x0000B02C File Offset: 0x0000922C
		public void DrawRectangle(Pen pen, int x, int y, int width, int height)
		{
			if (pen == null)
			{
				throw new ArgumentNullException("pen");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipDrawRectangleI(this.nativeObject, pen.NativePen, x, y, width, height));
		}

		/// <summary>Draws a series of rectangles specified by <see cref="T:System.Drawing.RectangleF" /> structures.</summary>
		/// <param name="pen">
		///   <see cref="T:System.Drawing.Pen" /> that determines the color, width, and style of the outlines of the rectangles. </param>
		/// <param name="rects">Array of <see cref="T:System.Drawing.RectangleF" /> structures that represent the rectangles to draw. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pen" /> is null.-or-<paramref name="rects" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="rects" /> is a zero-length array.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060003FC RID: 1020 RVA: 0x0000B058 File Offset: 0x00009258
		public void DrawRectangles(Pen pen, RectangleF[] rects)
		{
			if (pen == null)
			{
				throw new ArgumentNullException("image");
			}
			if (rects == null)
			{
				throw new ArgumentNullException("rects");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipDrawRectangles(this.nativeObject, pen.NativePen, rects, rects.Length));
		}

		/// <summary>Draws a series of rectangles specified by <see cref="T:System.Drawing.Rectangle" /> structures.</summary>
		/// <param name="pen">
		///   <see cref="T:System.Drawing.Pen" /> that determines the color, width, and style of the outlines of the rectangles. </param>
		/// <param name="rects">Array of <see cref="T:System.Drawing.Rectangle" /> structures that represent the rectangles to draw. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pen" /> is null.-or-<paramref name="rects" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="rects" /> is a zero-length array.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060003FD RID: 1021 RVA: 0x0000B090 File Offset: 0x00009290
		public void DrawRectangles(Pen pen, Rectangle[] rects)
		{
			if (pen == null)
			{
				throw new ArgumentNullException("image");
			}
			if (rects == null)
			{
				throw new ArgumentNullException("rects");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipDrawRectanglesI(this.nativeObject, pen.NativePen, rects, rects.Length));
		}

		/// <summary>Draws the specified text string in the specified rectangle with the specified <see cref="T:System.Drawing.Brush" /> and <see cref="T:System.Drawing.Font" /> objects.</summary>
		/// <param name="s">String to draw. </param>
		/// <param name="font">
		///   <see cref="T:System.Drawing.Font" /> that defines the text format of the string. </param>
		/// <param name="brush">
		///   <see cref="T:System.Drawing.Brush" /> that determines the color and texture of the drawn text. </param>
		/// <param name="layoutRectangle">
		///   <see cref="T:System.Drawing.RectangleF" /> structure that specifies the location of the drawn text. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="brush" /> is null.-or-<paramref name="s" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060003FE RID: 1022 RVA: 0x0000B0C8 File Offset: 0x000092C8
		public void DrawString(string s, Font font, Brush brush, RectangleF layoutRectangle)
		{
			this.DrawString(s, font, brush, layoutRectangle, null);
		}

		/// <summary>Draws the specified text string at the specified location with the specified <see cref="T:System.Drawing.Brush" /> and <see cref="T:System.Drawing.Font" /> objects.</summary>
		/// <param name="s">String to draw. </param>
		/// <param name="font">
		///   <see cref="T:System.Drawing.Font" /> that defines the text format of the string. </param>
		/// <param name="brush">
		///   <see cref="T:System.Drawing.Brush" /> that determines the color and texture of the drawn text. </param>
		/// <param name="point">
		///   <see cref="T:System.Drawing.PointF" /> structure that specifies the upper-left corner of the drawn text. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="brush" /> is null.-or-<paramref name="s" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060003FF RID: 1023 RVA: 0x0000B0D6 File Offset: 0x000092D6
		public void DrawString(string s, Font font, Brush brush, PointF point)
		{
			this.DrawString(s, font, brush, new RectangleF(point.X, point.Y, 0f, 0f), null);
		}

		/// <summary>Draws the specified text string at the specified location with the specified <see cref="T:System.Drawing.Brush" /> and <see cref="T:System.Drawing.Font" /> objects using the formatting attributes of the specified <see cref="T:System.Drawing.StringFormat" />.</summary>
		/// <param name="s">String to draw. </param>
		/// <param name="font">
		///   <see cref="T:System.Drawing.Font" /> that defines the text format of the string. </param>
		/// <param name="brush">
		///   <see cref="T:System.Drawing.Brush" /> that determines the color and texture of the drawn text. </param>
		/// <param name="point">
		///   <see cref="T:System.Drawing.PointF" /> structure that specifies the upper-left corner of the drawn text. </param>
		/// <param name="format">
		///   <see cref="T:System.Drawing.StringFormat" /> that specifies formatting attributes, such as line spacing and alignment, that are applied to the drawn text. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="brush" /> is null.-or-<paramref name="s" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000400 RID: 1024 RVA: 0x0000B0FF File Offset: 0x000092FF
		public void DrawString(string s, Font font, Brush brush, PointF point, StringFormat format)
		{
			this.DrawString(s, font, brush, new RectangleF(point.X, point.Y, 0f, 0f), format);
		}

		/// <summary>Draws the specified text string at the specified location with the specified <see cref="T:System.Drawing.Brush" /> and <see cref="T:System.Drawing.Font" /> objects.</summary>
		/// <param name="s">String to draw. </param>
		/// <param name="font">
		///   <see cref="T:System.Drawing.Font" /> that defines the text format of the string. </param>
		/// <param name="brush">
		///   <see cref="T:System.Drawing.Brush" /> that determines the color and texture of the drawn text. </param>
		/// <param name="x">The x-coordinate of the upper-left corner of the drawn text. </param>
		/// <param name="y">The y-coordinate of the upper-left corner of the drawn text. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="brush" /> is null.-or-<paramref name="s" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000401 RID: 1025 RVA: 0x0000B129 File Offset: 0x00009329
		public void DrawString(string s, Font font, Brush brush, float x, float y)
		{
			this.DrawString(s, font, brush, new RectangleF(x, y, 0f, 0f), null);
		}

		/// <summary>Draws the specified text string at the specified location with the specified <see cref="T:System.Drawing.Brush" /> and <see cref="T:System.Drawing.Font" /> objects using the formatting attributes of the specified <see cref="T:System.Drawing.StringFormat" />.</summary>
		/// <param name="s">String to draw. </param>
		/// <param name="font">
		///   <see cref="T:System.Drawing.Font" /> that defines the text format of the string. </param>
		/// <param name="brush">
		///   <see cref="T:System.Drawing.Brush" /> that determines the color and texture of the drawn text. </param>
		/// <param name="x">The x-coordinate of the upper-left corner of the drawn text. </param>
		/// <param name="y">The y-coordinate of the upper-left corner of the drawn text. </param>
		/// <param name="format">
		///   <see cref="T:System.Drawing.StringFormat" /> that specifies formatting attributes, such as line spacing and alignment, that are applied to the drawn text. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="brush" /> is null.-or-<paramref name="s" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000402 RID: 1026 RVA: 0x0000B148 File Offset: 0x00009348
		public void DrawString(string s, Font font, Brush brush, float x, float y, StringFormat format)
		{
			this.DrawString(s, font, brush, new RectangleF(x, y, 0f, 0f), format);
		}

		/// <summary>Draws the specified text string in the specified rectangle with the specified <see cref="T:System.Drawing.Brush" /> and <see cref="T:System.Drawing.Font" /> objects using the formatting attributes of the specified <see cref="T:System.Drawing.StringFormat" />.</summary>
		/// <param name="s">String to draw. </param>
		/// <param name="font">
		///   <see cref="T:System.Drawing.Font" /> that defines the text format of the string. </param>
		/// <param name="brush">
		///   <see cref="T:System.Drawing.Brush" /> that determines the color and texture of the drawn text. </param>
		/// <param name="layoutRectangle">
		///   <see cref="T:System.Drawing.RectangleF" /> structure that specifies the location of the drawn text. </param>
		/// <param name="format">
		///   <see cref="T:System.Drawing.StringFormat" /> that specifies formatting attributes, such as line spacing and alignment, that are applied to the drawn text. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="brush" /> is null.-or-<paramref name="s" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000403 RID: 1027 RVA: 0x0000B168 File Offset: 0x00009368
		public void DrawString(string s, Font font, Brush brush, RectangleF layoutRectangle, StringFormat format)
		{
			if (font == null)
			{
				throw new ArgumentNullException("font");
			}
			if (brush == null)
			{
				throw new ArgumentNullException("brush");
			}
			if (s == null || s.Length == 0)
			{
				return;
			}
			GDIPlus.CheckStatus(GDIPlus.GdipDrawString(this.nativeObject, s, s.Length, font.NativeObject, ref layoutRectangle, (format != null) ? format.NativeObject : IntPtr.Zero, brush.NativeBrush));
		}

		/// <summary>Closes the current graphics container and restores the state of this <see cref="T:System.Drawing.Graphics" /> to the state saved by a call to the <see cref="M:System.Drawing.Graphics.BeginContainer" /> method.</summary>
		/// <param name="container">
		///   <see cref="T:System.Drawing.Drawing2D.GraphicsContainer" /> that represents the container this method restores. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000404 RID: 1028 RVA: 0x0000B1D4 File Offset: 0x000093D4
		public void EndContainer(GraphicsContainer container)
		{
			if (container == null)
			{
				throw new ArgumentNullException("container");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipEndContainer(this.nativeObject, container.NativeObject));
		}

		/// <summary>Sends the records in the specified <see cref="T:System.Drawing.Imaging.Metafile" />, one at a time, to a callback method for display in a specified parallelogram.</summary>
		/// <param name="metafile">
		///   <see cref="T:System.Drawing.Imaging.Metafile" /> to enumerate. </param>
		/// <param name="destPoints">Array of three <see cref="T:System.Drawing.Point" /> structures that define a parallelogram that determines the size and location of the drawn metafile. </param>
		/// <param name="callback">
		///   <see cref="T:System.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the metafile records are sent. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000405 RID: 1029 RVA: 0x00005902 File Offset: 0x00003B02
		[MonoTODO("Metafiles enumeration, for both WMF and EMF formats, isn't supported.")]
		public void EnumerateMetafile(Metafile metafile, Point[] destPoints, Graphics.EnumerateMetafileProc callback)
		{
			throw new NotImplementedException();
		}

		/// <summary>Sends the records of the specified <see cref="T:System.Drawing.Imaging.Metafile" />, one at a time, to a callback method for display in a specified rectangle.</summary>
		/// <param name="metafile">
		///   <see cref="T:System.Drawing.Imaging.Metafile" /> to enumerate. </param>
		/// <param name="destRect">
		///   <see cref="T:System.Drawing.RectangleF" /> structure that specifies the location and size of the drawn metafile. </param>
		/// <param name="callback">
		///   <see cref="T:System.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the metafile records are sent. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000406 RID: 1030 RVA: 0x00005902 File Offset: 0x00003B02
		[MonoTODO("Metafiles enumeration, for both WMF and EMF formats, isn't supported.")]
		public void EnumerateMetafile(Metafile metafile, RectangleF destRect, Graphics.EnumerateMetafileProc callback)
		{
			throw new NotImplementedException();
		}

		/// <summary>Sends the records in the specified <see cref="T:System.Drawing.Imaging.Metafile" />, one at a time, to a callback method for display in a specified parallelogram.</summary>
		/// <param name="metafile">
		///   <see cref="T:System.Drawing.Imaging.Metafile" /> to enumerate. </param>
		/// <param name="destPoints">Array of three <see cref="T:System.Drawing.PointF" /> structures that define a parallelogram that determines the size and location of the drawn metafile. </param>
		/// <param name="callback">
		///   <see cref="T:System.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the metafile records are sent. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000407 RID: 1031 RVA: 0x00005902 File Offset: 0x00003B02
		[MonoTODO("Metafiles enumeration, for both WMF and EMF formats, isn't supported.")]
		public void EnumerateMetafile(Metafile metafile, PointF[] destPoints, Graphics.EnumerateMetafileProc callback)
		{
			throw new NotImplementedException();
		}

		/// <summary>Sends the records of the specified <see cref="T:System.Drawing.Imaging.Metafile" />, one at a time, to a callback method for display in a specified rectangle.</summary>
		/// <param name="metafile">
		///   <see cref="T:System.Drawing.Imaging.Metafile" /> to enumerate. </param>
		/// <param name="destRect">
		///   <see cref="T:System.Drawing.Rectangle" /> structure that specifies the location and size of the drawn metafile. </param>
		/// <param name="callback">
		///   <see cref="T:System.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the metafile records are sent. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000408 RID: 1032 RVA: 0x00005902 File Offset: 0x00003B02
		[MonoTODO("Metafiles enumeration, for both WMF and EMF formats, isn't supported.")]
		public void EnumerateMetafile(Metafile metafile, Rectangle destRect, Graphics.EnumerateMetafileProc callback)
		{
			throw new NotImplementedException();
		}

		/// <summary>Sends the records in the specified <see cref="T:System.Drawing.Imaging.Metafile" />, one at a time, to a callback method for display at a specified point.</summary>
		/// <param name="metafile">
		///   <see cref="T:System.Drawing.Imaging.Metafile" /> to enumerate. </param>
		/// <param name="destPoint">
		///   <see cref="T:System.Drawing.Point" /> structure that specifies the location of the upper-left corner of the drawn metafile. </param>
		/// <param name="callback">
		///   <see cref="T:System.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the metafile records are sent. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000409 RID: 1033 RVA: 0x00005902 File Offset: 0x00003B02
		[MonoTODO("Metafiles enumeration, for both WMF and EMF formats, isn't supported.")]
		public void EnumerateMetafile(Metafile metafile, Point destPoint, Graphics.EnumerateMetafileProc callback)
		{
			throw new NotImplementedException();
		}

		/// <summary>Sends the records in the specified <see cref="T:System.Drawing.Imaging.Metafile" />, one at a time, to a callback method for display at a specified point.</summary>
		/// <param name="metafile">
		///   <see cref="T:System.Drawing.Imaging.Metafile" /> to enumerate. </param>
		/// <param name="destPoint">
		///   <see cref="T:System.Drawing.PointF" /> structure that specifies the location of the upper-left corner of the drawn metafile. </param>
		/// <param name="callback">
		///   <see cref="T:System.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the metafile records are sent. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600040A RID: 1034 RVA: 0x00005902 File Offset: 0x00003B02
		[MonoTODO("Metafiles enumeration, for both WMF and EMF formats, isn't supported.")]
		public void EnumerateMetafile(Metafile metafile, PointF destPoint, Graphics.EnumerateMetafileProc callback)
		{
			throw new NotImplementedException();
		}

		/// <summary>Sends the records in the specified <see cref="T:System.Drawing.Imaging.Metafile" />, one at a time, to a callback method for display at a specified point.</summary>
		/// <param name="metafile">
		///   <see cref="T:System.Drawing.Imaging.Metafile" /> to enumerate. </param>
		/// <param name="destPoint">
		///   <see cref="T:System.Drawing.PointF" /> structure that specifies the location of the upper-left corner of the drawn metafile. </param>
		/// <param name="callback">
		///   <see cref="T:System.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the metafile records are sent. </param>
		/// <param name="callbackData">Internal pointer that is required, but ignored. You can pass <see cref="F:System.IntPtr.Zero" /> for this parameter. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600040B RID: 1035 RVA: 0x00005902 File Offset: 0x00003B02
		[MonoTODO("Metafiles enumeration, for both WMF and EMF formats, isn't supported.")]
		public void EnumerateMetafile(Metafile metafile, PointF destPoint, Graphics.EnumerateMetafileProc callback, IntPtr callbackData)
		{
			throw new NotImplementedException();
		}

		/// <summary>Sends the records of the specified <see cref="T:System.Drawing.Imaging.Metafile" />, one at a time, to a callback method for display in a specified rectangle.</summary>
		/// <param name="metafile">
		///   <see cref="T:System.Drawing.Imaging.Metafile" /> to enumerate. </param>
		/// <param name="destRect">
		///   <see cref="T:System.Drawing.Rectangle" /> structure that specifies the location and size of the drawn metafile. </param>
		/// <param name="callback">
		///   <see cref="T:System.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the metafile records are sent. </param>
		/// <param name="callbackData">Internal pointer that is required, but ignored. You can pass <see cref="F:System.IntPtr.Zero" /> for this parameter. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600040C RID: 1036 RVA: 0x00005902 File Offset: 0x00003B02
		[MonoTODO("Metafiles enumeration, for both WMF and EMF formats, isn't supported.")]
		public void EnumerateMetafile(Metafile metafile, Rectangle destRect, Graphics.EnumerateMetafileProc callback, IntPtr callbackData)
		{
			throw new NotImplementedException();
		}

		/// <summary>Sends the records in the specified <see cref="T:System.Drawing.Imaging.Metafile" />, one at a time, to a callback method for display in a specified parallelogram.</summary>
		/// <param name="metafile">
		///   <see cref="T:System.Drawing.Imaging.Metafile" /> to enumerate. </param>
		/// <param name="destPoints">Array of three <see cref="T:System.Drawing.PointF" /> structures that define a parallelogram that determines the size and location of the drawn metafile. </param>
		/// <param name="callback">
		///   <see cref="T:System.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the metafile records are sent. </param>
		/// <param name="callbackData">Internal pointer that is required, but ignored. You can pass <see cref="F:System.IntPtr.Zero" /> for this parameter. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600040D RID: 1037 RVA: 0x00005902 File Offset: 0x00003B02
		[MonoTODO("Metafiles enumeration, for both WMF and EMF formats, isn't supported.")]
		public void EnumerateMetafile(Metafile metafile, PointF[] destPoints, Graphics.EnumerateMetafileProc callback, IntPtr callbackData)
		{
			throw new NotImplementedException();
		}

		/// <summary>Sends the records in the specified <see cref="T:System.Drawing.Imaging.Metafile" />, one at a time, to a callback method for display at a specified point.</summary>
		/// <param name="metafile">
		///   <see cref="T:System.Drawing.Imaging.Metafile" /> to enumerate. </param>
		/// <param name="destPoint">
		///   <see cref="T:System.Drawing.Point" /> structure that specifies the location of the upper-left corner of the drawn metafile. </param>
		/// <param name="callback">
		///   <see cref="T:System.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the metafile records are sent. </param>
		/// <param name="callbackData">Internal pointer that is required, but ignored. You can pass <see cref="F:System.IntPtr.Zero" /> for this parameter. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600040E RID: 1038 RVA: 0x00005902 File Offset: 0x00003B02
		[MonoTODO("Metafiles enumeration, for both WMF and EMF formats, isn't supported.")]
		public void EnumerateMetafile(Metafile metafile, Point destPoint, Graphics.EnumerateMetafileProc callback, IntPtr callbackData)
		{
			throw new NotImplementedException();
		}

		/// <summary>Sends the records in the specified <see cref="T:System.Drawing.Imaging.Metafile" />, one at a time, to a callback method for display in a specified parallelogram.</summary>
		/// <param name="metafile">
		///   <see cref="T:System.Drawing.Imaging.Metafile" /> to enumerate. </param>
		/// <param name="destPoints">Array of three <see cref="T:System.Drawing.Point" /> structures that define a parallelogram that determines the size and location of the drawn metafile. </param>
		/// <param name="callback">
		///   <see cref="T:System.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the metafile records are sent. </param>
		/// <param name="callbackData">Internal pointer that is required, but ignored. You can pass <see cref="F:System.IntPtr.Zero" /> for this parameter. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600040F RID: 1039 RVA: 0x00005902 File Offset: 0x00003B02
		[MonoTODO("Metafiles enumeration, for both WMF and EMF formats, isn't supported.")]
		public void EnumerateMetafile(Metafile metafile, Point[] destPoints, Graphics.EnumerateMetafileProc callback, IntPtr callbackData)
		{
			throw new NotImplementedException();
		}

		/// <summary>Sends the records of the specified <see cref="T:System.Drawing.Imaging.Metafile" />, one at a time, to a callback method for display in a specified rectangle.</summary>
		/// <param name="metafile">
		///   <see cref="T:System.Drawing.Imaging.Metafile" /> to enumerate. </param>
		/// <param name="destRect">
		///   <see cref="T:System.Drawing.RectangleF" /> structure that specifies the location and size of the drawn metafile. </param>
		/// <param name="callback">
		///   <see cref="T:System.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the metafile records are sent. </param>
		/// <param name="callbackData">Internal pointer that is required, but ignored. You can pass <see cref="F:System.IntPtr.Zero" /> for this parameter. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000410 RID: 1040 RVA: 0x00005902 File Offset: 0x00003B02
		[MonoTODO("Metafiles enumeration, for both WMF and EMF formats, isn't supported.")]
		public void EnumerateMetafile(Metafile metafile, RectangleF destRect, Graphics.EnumerateMetafileProc callback, IntPtr callbackData)
		{
			throw new NotImplementedException();
		}

		/// <summary>Sends the records in a selected rectangle from a <see cref="T:System.Drawing.Imaging.Metafile" />, one at a time, to a callback method for display at a specified point.</summary>
		/// <param name="metafile">
		///   <see cref="T:System.Drawing.Imaging.Metafile" /> to enumerate. </param>
		/// <param name="destPoint">
		///   <see cref="T:System.Drawing.PointF" /> structure that specifies the location of the upper-left corner of the drawn metafile. </param>
		/// <param name="srcRect">
		///   <see cref="T:System.Drawing.RectangleF" /> structure that specifies the portion of the metafile, relative to its upper-left corner, to draw. </param>
		/// <param name="srcUnit">Member of the <see cref="T:System.Drawing.GraphicsUnit" /> enumeration that specifies the unit of measure used to determine the portion of the metafile that the rectangle specified by the <paramref name="srcRect" /> parameter contains. </param>
		/// <param name="callback">
		///   <see cref="T:System.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the metafile records are sent. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000411 RID: 1041 RVA: 0x00005902 File Offset: 0x00003B02
		[MonoTODO("Metafiles enumeration, for both WMF and EMF formats, isn't supported.")]
		public void EnumerateMetafile(Metafile metafile, PointF destPoint, RectangleF srcRect, GraphicsUnit srcUnit, Graphics.EnumerateMetafileProc callback)
		{
			throw new NotImplementedException();
		}

		/// <summary>Sends the records in a selected rectangle from a <see cref="T:System.Drawing.Imaging.Metafile" />, one at a time, to a callback method for display at a specified point.</summary>
		/// <param name="metafile">
		///   <see cref="T:System.Drawing.Imaging.Metafile" /> to enumerate. </param>
		/// <param name="destPoint">
		///   <see cref="T:System.Drawing.Point" /> structure that specifies the location of the upper-left corner of the drawn metafile. </param>
		/// <param name="srcRect">
		///   <see cref="T:System.Drawing.Rectangle" /> structure that specifies the portion of the metafile, relative to its upper-left corner, to draw. </param>
		/// <param name="srcUnit">Member of the <see cref="T:System.Drawing.GraphicsUnit" /> enumeration that specifies the unit of measure used to determine the portion of the metafile that the rectangle specified by the <paramref name="srcRect" /> parameter contains. </param>
		/// <param name="callback">
		///   <see cref="T:System.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the metafile records are sent. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000412 RID: 1042 RVA: 0x00005902 File Offset: 0x00003B02
		[MonoTODO("Metafiles enumeration, for both WMF and EMF formats, isn't supported.")]
		public void EnumerateMetafile(Metafile metafile, Point destPoint, Rectangle srcRect, GraphicsUnit srcUnit, Graphics.EnumerateMetafileProc callback)
		{
			throw new NotImplementedException();
		}

		/// <summary>Sends the records in a selected rectangle from a <see cref="T:System.Drawing.Imaging.Metafile" />, one at a time, to a callback method for display in a specified parallelogram.</summary>
		/// <param name="metafile">
		///   <see cref="T:System.Drawing.Imaging.Metafile" /> to enumerate. </param>
		/// <param name="destPoints">Array of three <see cref="T:System.Drawing.PointF" /> structures that define a parallelogram that determines the size and location of the drawn metafile. </param>
		/// <param name="srcRect">
		///   <see cref="T:System.Drawing.RectangleF" /> structures that specifies the portion of the metafile, relative to its upper-left corner, to draw. </param>
		/// <param name="srcUnit">Member of the <see cref="T:System.Drawing.GraphicsUnit" /> enumeration that specifies the unit of measure used to determine the portion of the metafile that the rectangle specified by the <paramref name="srcRect" /> parameter contains. </param>
		/// <param name="callback">
		///   <see cref="T:System.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the metafile records are sent. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000413 RID: 1043 RVA: 0x00005902 File Offset: 0x00003B02
		[MonoTODO("Metafiles enumeration, for both WMF and EMF formats, isn't supported.")]
		public void EnumerateMetafile(Metafile metafile, PointF[] destPoints, RectangleF srcRect, GraphicsUnit srcUnit, Graphics.EnumerateMetafileProc callback)
		{
			throw new NotImplementedException();
		}

		/// <summary>Sends the records in a selected rectangle from a <see cref="T:System.Drawing.Imaging.Metafile" />, one at a time, to a callback method for display in a specified parallelogram.</summary>
		/// <param name="metafile">
		///   <see cref="T:System.Drawing.Imaging.Metafile" /> to enumerate. </param>
		/// <param name="destPoints">Array of three <see cref="T:System.Drawing.Point" /> structures that define a parallelogram that determines the size and location of the drawn metafile. </param>
		/// <param name="srcRect">
		///   <see cref="T:System.Drawing.Rectangle" /> structure that specifies the portion of the metafile, relative to its upper-left corner, to draw. </param>
		/// <param name="srcUnit">Member of the <see cref="T:System.Drawing.GraphicsUnit" /> enumeration that specifies the unit of measure used to determine the portion of the metafile that the rectangle specified by the <paramref name="srcRect" /> parameter contains. </param>
		/// <param name="callback">
		///   <see cref="T:System.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the metafile records are sent. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000414 RID: 1044 RVA: 0x00005902 File Offset: 0x00003B02
		[MonoTODO("Metafiles enumeration, for both WMF and EMF formats, isn't supported.")]
		public void EnumerateMetafile(Metafile metafile, Point[] destPoints, Rectangle srcRect, GraphicsUnit srcUnit, Graphics.EnumerateMetafileProc callback)
		{
			throw new NotImplementedException();
		}

		/// <summary>Sends the records of a selected rectangle from a <see cref="T:System.Drawing.Imaging.Metafile" />, one at a time, to a callback method for display in a specified rectangle.</summary>
		/// <param name="metafile">
		///   <see cref="T:System.Drawing.Imaging.Metafile" /> to enumerate. </param>
		/// <param name="destRect">
		///   <see cref="T:System.Drawing.RectangleF" /> structure that specifies the location and size of the drawn metafile. </param>
		/// <param name="srcRect">
		///   <see cref="T:System.Drawing.RectangleF" /> structure that specifies the portion of the metafile, relative to its upper-left corner, to draw. </param>
		/// <param name="srcUnit">Member of the <see cref="T:System.Drawing.GraphicsUnit" /> enumeration that specifies the unit of measure used to determine the portion of the metafile that the rectangle specified by the <paramref name="srcRect" /> parameter contains. </param>
		/// <param name="callback">
		///   <see cref="T:System.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the metafile records are sent. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000415 RID: 1045 RVA: 0x00005902 File Offset: 0x00003B02
		[MonoTODO("Metafiles enumeration, for both WMF and EMF formats, isn't supported.")]
		public void EnumerateMetafile(Metafile metafile, RectangleF destRect, RectangleF srcRect, GraphicsUnit srcUnit, Graphics.EnumerateMetafileProc callback)
		{
			throw new NotImplementedException();
		}

		/// <summary>Sends the records of a selected rectangle from a <see cref="T:System.Drawing.Imaging.Metafile" />, one at a time, to a callback method for display in a specified rectangle.</summary>
		/// <param name="metafile">
		///   <see cref="T:System.Drawing.Imaging.Metafile" /> to enumerate. </param>
		/// <param name="destRect">
		///   <see cref="T:System.Drawing.Rectangle" /> structure that specifies the location and size of the drawn metafile. </param>
		/// <param name="srcRect">
		///   <see cref="T:System.Drawing.Rectangle" /> structure that specifies the portion of the metafile, relative to its upper-left corner, to draw. </param>
		/// <param name="srcUnit">Member of the <see cref="T:System.Drawing.GraphicsUnit" /> enumeration that specifies the unit of measure used to determine the portion of the metafile that the rectangle specified by the <paramref name="srcRect" /> parameter contains. </param>
		/// <param name="callback">
		///   <see cref="T:System.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the metafile records are sent. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000416 RID: 1046 RVA: 0x00005902 File Offset: 0x00003B02
		[MonoTODO("Metafiles enumeration, for both WMF and EMF formats, isn't supported.")]
		public void EnumerateMetafile(Metafile metafile, Rectangle destRect, Rectangle srcRect, GraphicsUnit srcUnit, Graphics.EnumerateMetafileProc callback)
		{
			throw new NotImplementedException();
		}

		/// <summary>Sends the records of the specified <see cref="T:System.Drawing.Imaging.Metafile" />, one at a time, to a callback method for display in a specified rectangle using specified image attributes.</summary>
		/// <param name="metafile">
		///   <see cref="T:System.Drawing.Imaging.Metafile" /> to enumerate. </param>
		/// <param name="destRect">
		///   <see cref="T:System.Drawing.RectangleF" /> structure that specifies the location and size of the drawn metafile. </param>
		/// <param name="callback">
		///   <see cref="T:System.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the metafile records are sent. </param>
		/// <param name="callbackData">Internal pointer that is required, but ignored. You can pass <see cref="F:System.IntPtr.Zero" /> for this parameter. </param>
		/// <param name="imageAttr">
		///   <see cref="T:System.Drawing.Imaging.ImageAttributes" /> that specifies image attribute information for the drawn image. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000417 RID: 1047 RVA: 0x00005902 File Offset: 0x00003B02
		[MonoTODO("Metafiles enumeration, for both WMF and EMF formats, isn't supported.")]
		public void EnumerateMetafile(Metafile metafile, RectangleF destRect, Graphics.EnumerateMetafileProc callback, IntPtr callbackData, ImageAttributes imageAttr)
		{
			throw new NotImplementedException();
		}

		/// <summary>Sends the records in the specified <see cref="T:System.Drawing.Imaging.Metafile" />, one at a time, to a callback method for display at a specified point using specified image attributes.</summary>
		/// <param name="metafile">
		///   <see cref="T:System.Drawing.Imaging.Metafile" /> to enumerate. </param>
		/// <param name="destPoint">
		///   <see cref="T:System.Drawing.Point" /> structure that specifies the location of the upper-left corner of the drawn metafile. </param>
		/// <param name="callback">
		///   <see cref="T:System.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the metafile records are sent. </param>
		/// <param name="callbackData">Internal pointer that is required, but ignored. You can pass <see cref="F:System.IntPtr.Zero" /> for this parameter. </param>
		/// <param name="imageAttr">
		///   <see cref="T:System.Drawing.Imaging.ImageAttributes" /> that specifies image attribute information for the drawn image. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000418 RID: 1048 RVA: 0x00005902 File Offset: 0x00003B02
		[MonoTODO("Metafiles enumeration, for both WMF and EMF formats, isn't supported.")]
		public void EnumerateMetafile(Metafile metafile, Point destPoint, Graphics.EnumerateMetafileProc callback, IntPtr callbackData, ImageAttributes imageAttr)
		{
			throw new NotImplementedException();
		}

		/// <summary>Sends the records in the specified <see cref="T:System.Drawing.Imaging.Metafile" />, one at a time, to a callback method for display at a specified point using specified image attributes.</summary>
		/// <param name="metafile">
		///   <see cref="T:System.Drawing.Imaging.Metafile" /> to enumerate. </param>
		/// <param name="destPoint">
		///   <see cref="T:System.Drawing.PointF" /> structure that specifies the location of the upper-left corner of the drawn metafile. </param>
		/// <param name="callback">
		///   <see cref="T:System.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the metafile records are sent. </param>
		/// <param name="callbackData">Internal pointer that is required, but ignored. You can pass <see cref="F:System.IntPtr.Zero" /> for this parameter. </param>
		/// <param name="imageAttr">
		///   <see cref="T:System.Drawing.Imaging.ImageAttributes" /> that specifies image attribute information for the drawn image. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000419 RID: 1049 RVA: 0x00005902 File Offset: 0x00003B02
		[MonoTODO("Metafiles enumeration, for both WMF and EMF formats, isn't supported.")]
		public void EnumerateMetafile(Metafile metafile, PointF destPoint, Graphics.EnumerateMetafileProc callback, IntPtr callbackData, ImageAttributes imageAttr)
		{
			throw new NotImplementedException();
		}

		/// <summary>Sends the records in the specified <see cref="T:System.Drawing.Imaging.Metafile" />, one at a time, to a callback method for display in a specified parallelogram using specified image attributes.</summary>
		/// <param name="metafile">
		///   <see cref="T:System.Drawing.Imaging.Metafile" /> to enumerate. </param>
		/// <param name="destPoints">Array of three <see cref="T:System.Drawing.Point" /> structures that define a parallelogram that determines the size and location of the drawn metafile. </param>
		/// <param name="callback">
		///   <see cref="T:System.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the metafile records are sent. </param>
		/// <param name="callbackData">Internal pointer that is required, but ignored. You can pass <see cref="F:System.IntPtr.Zero" /> for this parameter. </param>
		/// <param name="imageAttr">
		///   <see cref="T:System.Drawing.Imaging.ImageAttributes" /> that specifies image attribute information for the drawn image. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600041A RID: 1050 RVA: 0x00005902 File Offset: 0x00003B02
		[MonoTODO("Metafiles enumeration, for both WMF and EMF formats, isn't supported.")]
		public void EnumerateMetafile(Metafile metafile, Point[] destPoints, Graphics.EnumerateMetafileProc callback, IntPtr callbackData, ImageAttributes imageAttr)
		{
			throw new NotImplementedException();
		}

		/// <summary>Sends the records in the specified <see cref="T:System.Drawing.Imaging.Metafile" />, one at a time, to a callback method for display in a specified parallelogram using specified image attributes.</summary>
		/// <param name="metafile">
		///   <see cref="T:System.Drawing.Imaging.Metafile" /> to enumerate. </param>
		/// <param name="destPoints">Array of three <see cref="T:System.Drawing.PointF" /> structures that define a parallelogram that determines the size and location of the drawn metafile. </param>
		/// <param name="callback">
		///   <see cref="T:System.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the metafile records are sent. </param>
		/// <param name="callbackData">Internal pointer that is required, but ignored. You can pass <see cref="F:System.IntPtr.Zero" /> for this parameter. </param>
		/// <param name="imageAttr">
		///   <see cref="T:System.Drawing.Imaging.ImageAttributes" /> that specifies image attribute information for the drawn image. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600041B RID: 1051 RVA: 0x00005902 File Offset: 0x00003B02
		[MonoTODO("Metafiles enumeration, for both WMF and EMF formats, isn't supported.")]
		public void EnumerateMetafile(Metafile metafile, PointF[] destPoints, Graphics.EnumerateMetafileProc callback, IntPtr callbackData, ImageAttributes imageAttr)
		{
			throw new NotImplementedException();
		}

		/// <summary>Sends the records of the specified <see cref="T:System.Drawing.Imaging.Metafile" />, one at a time, to a callback method for display in a specified rectangle using specified image attributes.</summary>
		/// <param name="metafile">
		///   <see cref="T:System.Drawing.Imaging.Metafile" /> to enumerate. </param>
		/// <param name="destRect">
		///   <see cref="T:System.Drawing.Rectangle" /> structure that specifies the location and size of the drawn metafile. </param>
		/// <param name="callback">
		///   <see cref="T:System.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the metafile records are sent. </param>
		/// <param name="callbackData">Internal pointer that is required, but ignored. You can pass <see cref="F:System.IntPtr.Zero" /> for this parameter. </param>
		/// <param name="imageAttr">
		///   <see cref="T:System.Drawing.Imaging.ImageAttributes" /> that specifies image attribute information for the drawn image. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600041C RID: 1052 RVA: 0x00005902 File Offset: 0x00003B02
		[MonoTODO("Metafiles enumeration, for both WMF and EMF formats, isn't supported.")]
		public void EnumerateMetafile(Metafile metafile, Rectangle destRect, Graphics.EnumerateMetafileProc callback, IntPtr callbackData, ImageAttributes imageAttr)
		{
			throw new NotImplementedException();
		}

		/// <summary>Sends the records of a selected rectangle from a <see cref="T:System.Drawing.Imaging.Metafile" />, one at a time, to a callback method for display in a specified rectangle.</summary>
		/// <param name="metafile">
		///   <see cref="T:System.Drawing.Imaging.Metafile" /> to enumerate. </param>
		/// <param name="destRect">
		///   <see cref="T:System.Drawing.Rectangle" /> structure that specifies the location and size of the drawn metafile. </param>
		/// <param name="srcRect">
		///   <see cref="T:System.Drawing.Rectangle" /> structure that specifies the portion of the metafile, relative to its upper-left corner, to draw. </param>
		/// <param name="srcUnit">Member of the <see cref="T:System.Drawing.GraphicsUnit" /> enumeration that specifies the unit of measure used to determine the portion of the metafile that the rectangle specified by the <paramref name="srcRect" /> parameter contains. </param>
		/// <param name="callback">
		///   <see cref="T:System.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the metafile records are sent. </param>
		/// <param name="callbackData">Internal pointer that is required, but ignored. You can pass <see cref="F:System.IntPtr.Zero" /> for this parameter. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600041D RID: 1053 RVA: 0x00005902 File Offset: 0x00003B02
		[MonoTODO("Metafiles enumeration, for both WMF and EMF formats, isn't supported.")]
		public void EnumerateMetafile(Metafile metafile, Rectangle destRect, Rectangle srcRect, GraphicsUnit srcUnit, Graphics.EnumerateMetafileProc callback, IntPtr callbackData)
		{
			throw new NotImplementedException();
		}

		/// <summary>Sends the records in a selected rectangle from a <see cref="T:System.Drawing.Imaging.Metafile" />, one at a time, to a callback method for display in a specified parallelogram.</summary>
		/// <param name="metafile">
		///   <see cref="T:System.Drawing.Imaging.Metafile" /> to enumerate. </param>
		/// <param name="destPoints">Array of three <see cref="T:System.Drawing.PointF" /> structures that define a parallelogram that determines the size and location of the drawn metafile. </param>
		/// <param name="srcRect">
		///   <see cref="T:System.Drawing.RectangleF" /> structure that specifies the portion of the metafile, relative to its upper-left corner, to draw. </param>
		/// <param name="srcUnit">Member of the <see cref="T:System.Drawing.GraphicsUnit" /> enumeration that specifies the unit of measure used to determine the portion of the metafile that the rectangle specified by the <paramref name="srcRect" /> parameter contains. </param>
		/// <param name="callback">
		///   <see cref="T:System.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the metafile records are sent. </param>
		/// <param name="callbackData">Internal pointer that is required, but ignored. You can pass <see cref="F:System.IntPtr.Zero" /> for this parameter. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600041E RID: 1054 RVA: 0x00005902 File Offset: 0x00003B02
		[MonoTODO("Metafiles enumeration, for both WMF and EMF formats, isn't supported.")]
		public void EnumerateMetafile(Metafile metafile, PointF[] destPoints, RectangleF srcRect, GraphicsUnit srcUnit, Graphics.EnumerateMetafileProc callback, IntPtr callbackData)
		{
			throw new NotImplementedException();
		}

		/// <summary>Sends the records of a selected rectangle from a <see cref="T:System.Drawing.Imaging.Metafile" />, one at a time, to a callback method for display in a specified rectangle.</summary>
		/// <param name="metafile">
		///   <see cref="T:System.Drawing.Imaging.Metafile" /> to enumerate. </param>
		/// <param name="destRect">
		///   <see cref="T:System.Drawing.RectangleF" /> structure that specifies the location and size of the drawn metafile. </param>
		/// <param name="srcRect">
		///   <see cref="T:System.Drawing.RectangleF" /> structure that specifies the portion of the metafile, relative to its upper-left corner, to draw. </param>
		/// <param name="srcUnit">Member of the <see cref="T:System.Drawing.GraphicsUnit" /> enumeration that specifies the unit of measure used to determine the portion of the metafile that the rectangle specified by the <paramref name="srcRect" /> parameter contains. </param>
		/// <param name="callback">
		///   <see cref="T:System.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the metafile records are sent. </param>
		/// <param name="callbackData">Internal pointer that is required, but ignored. You can pass <see cref="F:System.IntPtr.Zero" /> for this parameter. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600041F RID: 1055 RVA: 0x00005902 File Offset: 0x00003B02
		[MonoTODO("Metafiles enumeration, for both WMF and EMF formats, isn't supported.")]
		public void EnumerateMetafile(Metafile metafile, RectangleF destRect, RectangleF srcRect, GraphicsUnit srcUnit, Graphics.EnumerateMetafileProc callback, IntPtr callbackData)
		{
			throw new NotImplementedException();
		}

		/// <summary>Sends the records in a selected rectangle from a <see cref="T:System.Drawing.Imaging.Metafile" />, one at a time, to a callback method for display at a specified point.</summary>
		/// <param name="metafile">
		///   <see cref="T:System.Drawing.Imaging.Metafile" /> to enumerate. </param>
		/// <param name="destPoint">
		///   <see cref="T:System.Drawing.PointF" /> structure that specifies the location of the upper-left corner of the drawn metafile. </param>
		/// <param name="srcRect">
		///   <see cref="T:System.Drawing.RectangleF" /> structure that specifies the portion of the metafile, relative to its upper-left corner, to draw. </param>
		/// <param name="srcUnit">Member of the <see cref="T:System.Drawing.GraphicsUnit" /> enumeration that specifies the unit of measure used to determine the portion of the metafile that the rectangle specified by the <paramref name="srcRect" /> parameter contains. </param>
		/// <param name="callback">
		///   <see cref="T:System.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the metafile records are sent. </param>
		/// <param name="callbackData">Internal pointer that is required, but ignored. You can pass <see cref="F:System.IntPtr.Zero" /> for this parameter. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000420 RID: 1056 RVA: 0x00005902 File Offset: 0x00003B02
		[MonoTODO("Metafiles enumeration, for both WMF and EMF formats, isn't supported.")]
		public void EnumerateMetafile(Metafile metafile, PointF destPoint, RectangleF srcRect, GraphicsUnit srcUnit, Graphics.EnumerateMetafileProc callback, IntPtr callbackData)
		{
			throw new NotImplementedException();
		}

		/// <summary>Sends the records in a selected rectangle from a <see cref="T:System.Drawing.Imaging.Metafile" />, one at a time, to a callback method for display at a specified point.</summary>
		/// <param name="metafile">
		///   <see cref="T:System.Drawing.Imaging.Metafile" /> to enumerate. </param>
		/// <param name="destPoint">
		///   <see cref="T:System.Drawing.Point" /> structure that specifies the location of the upper-left corner of the drawn metafile. </param>
		/// <param name="srcRect">
		///   <see cref="T:System.Drawing.Rectangle" /> structure that specifies the portion of the metafile, relative to its upper-left corner, to draw. </param>
		/// <param name="srcUnit">Member of the <see cref="T:System.Drawing.GraphicsUnit" /> enumeration that specifies the unit of measure used to determine the portion of the metafile that the rectangle specified by the <paramref name="srcRect" /> parameter contains. </param>
		/// <param name="callback">
		///   <see cref="T:System.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the metafile records are sent. </param>
		/// <param name="callbackData">Internal pointer that is required, but ignored. You can pass <see cref="F:System.IntPtr.Zero" /> for this parameter. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000421 RID: 1057 RVA: 0x00005902 File Offset: 0x00003B02
		[MonoTODO("Metafiles enumeration, for both WMF and EMF formats, isn't supported.")]
		public void EnumerateMetafile(Metafile metafile, Point destPoint, Rectangle srcRect, GraphicsUnit srcUnit, Graphics.EnumerateMetafileProc callback, IntPtr callbackData)
		{
			throw new NotImplementedException();
		}

		/// <summary>Sends the records in a selected rectangle from a <see cref="T:System.Drawing.Imaging.Metafile" />, one at a time, to a callback method for display in a specified parallelogram.</summary>
		/// <param name="metafile">
		///   <see cref="T:System.Drawing.Imaging.Metafile" /> to enumerate. </param>
		/// <param name="destPoints">Array of three <see cref="T:System.Drawing.Point" /> structures that define a parallelogram that determines the size and location of the drawn metafile. </param>
		/// <param name="srcRect">
		///   <see cref="T:System.Drawing.Rectangle" /> structure that specifies the portion of the metafile, relative to its upper-left corner, to draw. </param>
		/// <param name="srcUnit">Member of the <see cref="T:System.Drawing.GraphicsUnit" /> enumeration that specifies the unit of measure used to determine the portion of the metafile that the rectangle specified by the <paramref name="srcRect" /> parameter contains. </param>
		/// <param name="callback">
		///   <see cref="T:System.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the metafile records are sent. </param>
		/// <param name="callbackData">Internal pointer that is required, but ignored. You can pass <see cref="F:System.IntPtr.Zero" /> for this parameter. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000422 RID: 1058 RVA: 0x00005902 File Offset: 0x00003B02
		[MonoTODO("Metafiles enumeration, for both WMF and EMF formats, isn't supported.")]
		public void EnumerateMetafile(Metafile metafile, Point[] destPoints, Rectangle srcRect, GraphicsUnit srcUnit, Graphics.EnumerateMetafileProc callback, IntPtr callbackData)
		{
			throw new NotImplementedException();
		}

		/// <summary>Sends the records in a selected rectangle from a <see cref="T:System.Drawing.Imaging.Metafile" />, one at a time, to a callback method for display in a specified parallelogram using specified image attributes.</summary>
		/// <param name="metafile">
		///   <see cref="T:System.Drawing.Imaging.Metafile" /> to enumerate. </param>
		/// <param name="destPoints">Array of three <see cref="T:System.Drawing.Point" /> structures that define a parallelogram that determines the size and location of the drawn metafile. </param>
		/// <param name="srcRect">
		///   <see cref="T:System.Drawing.Rectangle" /> structure that specifies the portion of the metafile, relative to its upper-left corner, to draw. </param>
		/// <param name="unit">Member of the <see cref="T:System.Drawing.GraphicsUnit" /> enumeration that specifies the unit of measure used to determine the portion of the metafile that the rectangle specified by the <paramref name="srcRect" /> parameter contains. </param>
		/// <param name="callback">
		///   <see cref="T:System.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the metafile records are sent. </param>
		/// <param name="callbackData">Internal pointer that is required, but ignored. You can pass <see cref="F:System.IntPtr.Zero" /> for this parameter. </param>
		/// <param name="imageAttr">
		///   <see cref="T:System.Drawing.Imaging.ImageAttributes" /> that specifies image attribute information for the drawn image. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000423 RID: 1059 RVA: 0x00005902 File Offset: 0x00003B02
		[MonoTODO("Metafiles enumeration, for both WMF and EMF formats, isn't supported.")]
		public void EnumerateMetafile(Metafile metafile, Point[] destPoints, Rectangle srcRect, GraphicsUnit unit, Graphics.EnumerateMetafileProc callback, IntPtr callbackData, ImageAttributes imageAttr)
		{
			throw new NotImplementedException();
		}

		/// <summary>Sends the records of a selected rectangle from a <see cref="T:System.Drawing.Imaging.Metafile" />, one at a time, to a callback method for display in a specified rectangle using specified image attributes.</summary>
		/// <param name="metafile">
		///   <see cref="T:System.Drawing.Imaging.Metafile" /> to enumerate. </param>
		/// <param name="destRect">
		///   <see cref="T:System.Drawing.Rectangle" /> structure that specifies the location and size of the drawn metafile. </param>
		/// <param name="srcRect">
		///   <see cref="T:System.Drawing.Rectangle" /> structure that specifies the portion of the metafile, relative to its upper-left corner, to draw. </param>
		/// <param name="unit">Member of the <see cref="T:System.Drawing.GraphicsUnit" /> enumeration that specifies the unit of measure used to determine the portion of the metafile that the rectangle specified by the <paramref name="srcRect" /> parameter contains. </param>
		/// <param name="callback">
		///   <see cref="T:System.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the metafile records are sent. </param>
		/// <param name="callbackData">Internal pointer that is required, but ignored. You can pass <see cref="F:System.IntPtr.Zero" /> for this parameter. </param>
		/// <param name="imageAttr">
		///   <see cref="T:System.Drawing.Imaging.ImageAttributes" /> that specifies image attribute information for the drawn image. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000424 RID: 1060 RVA: 0x00005902 File Offset: 0x00003B02
		[MonoTODO("Metafiles enumeration, for both WMF and EMF formats, isn't supported.")]
		public void EnumerateMetafile(Metafile metafile, Rectangle destRect, Rectangle srcRect, GraphicsUnit unit, Graphics.EnumerateMetafileProc callback, IntPtr callbackData, ImageAttributes imageAttr)
		{
			throw new NotImplementedException();
		}

		/// <summary>Sends the records in a selected rectangle from a <see cref="T:System.Drawing.Imaging.Metafile" />, one at a time, to a callback method for display at a specified point using specified image attributes.</summary>
		/// <param name="metafile">
		///   <see cref="T:System.Drawing.Imaging.Metafile" /> to enumerate. </param>
		/// <param name="destPoint">
		///   <see cref="T:System.Drawing.Point" /> structure that specifies the location of the upper-left corner of the drawn metafile. </param>
		/// <param name="srcRect">
		///   <see cref="T:System.Drawing.Rectangle" /> structure that specifies the portion of the metafile, relative to its upper-left corner, to draw. </param>
		/// <param name="unit">Member of the <see cref="T:System.Drawing.GraphicsUnit" /> enumeration that specifies the unit of measure used to determine the portion of the metafile that the rectangle specified by the <paramref name="srcRect" /> parameter contains. </param>
		/// <param name="callback">
		///   <see cref="T:System.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the metafile records are sent. </param>
		/// <param name="callbackData">Internal pointer that is required, but ignored. You can pass <see cref="F:System.IntPtr.Zero" /> for this parameter. </param>
		/// <param name="imageAttr">
		///   <see cref="T:System.Drawing.Imaging.ImageAttributes" /> that specifies image attribute information for the drawn image. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000425 RID: 1061 RVA: 0x00005902 File Offset: 0x00003B02
		[MonoTODO("Metafiles enumeration, for both WMF and EMF formats, isn't supported.")]
		public void EnumerateMetafile(Metafile metafile, Point destPoint, Rectangle srcRect, GraphicsUnit unit, Graphics.EnumerateMetafileProc callback, IntPtr callbackData, ImageAttributes imageAttr)
		{
			throw new NotImplementedException();
		}

		/// <summary>Sends the records of a selected rectangle from a <see cref="T:System.Drawing.Imaging.Metafile" />, one at a time, to a callback method for display in a specified rectangle using specified image attributes.</summary>
		/// <param name="metafile">
		///   <see cref="T:System.Drawing.Imaging.Metafile" /> to enumerate. </param>
		/// <param name="destRect">
		///   <see cref="T:System.Drawing.RectangleF" /> structure that specifies the location and size of the drawn metafile. </param>
		/// <param name="srcRect">
		///   <see cref="T:System.Drawing.RectangleF" /> structure that specifies the portion of the metafile, relative to its upper-left corner, to draw. </param>
		/// <param name="unit">Member of the <see cref="T:System.Drawing.GraphicsUnit" /> enumeration that specifies the unit of measure used to determine the portion of the metafile that the rectangle specified by the <paramref name="srcRect" /> parameter contains. </param>
		/// <param name="callback">
		///   <see cref="T:System.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the metafile records are sent. </param>
		/// <param name="callbackData">Internal pointer that is required, but ignored. You can pass <see cref="F:System.IntPtr.Zero" /> for this parameter. </param>
		/// <param name="imageAttr">
		///   <see cref="T:System.Drawing.Imaging.ImageAttributes" /> that specifies image attribute information for the drawn image. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000426 RID: 1062 RVA: 0x00005902 File Offset: 0x00003B02
		[MonoTODO("Metafiles enumeration, for both WMF and EMF formats, isn't supported.")]
		public void EnumerateMetafile(Metafile metafile, RectangleF destRect, RectangleF srcRect, GraphicsUnit unit, Graphics.EnumerateMetafileProc callback, IntPtr callbackData, ImageAttributes imageAttr)
		{
			throw new NotImplementedException();
		}

		/// <summary>Sends the records in a selected rectangle from a <see cref="T:System.Drawing.Imaging.Metafile" />, one at a time, to a callback method for display in a specified parallelogram using specified image attributes.</summary>
		/// <param name="metafile">
		///   <see cref="T:System.Drawing.Imaging.Metafile" /> to enumerate. </param>
		/// <param name="destPoints">Array of three <see cref="T:System.Drawing.PointF" /> structures that define a parallelogram that determines the size and location of the drawn metafile. </param>
		/// <param name="srcRect">
		///   <see cref="T:System.Drawing.RectangleF" /> structure that specifies the portion of the metafile, relative to its upper-left corner, to draw. </param>
		/// <param name="unit">Member of the <see cref="T:System.Drawing.GraphicsUnit" /> enumeration that specifies the unit of measure used to determine the portion of the metafile that the rectangle specified by the <paramref name="srcRect" /> parameter contains. </param>
		/// <param name="callback">
		///   <see cref="T:System.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the metafile records are sent. </param>
		/// <param name="callbackData">Internal pointer that is required, but ignored. You can pass <see cref="F:System.IntPtr.Zero" /> for this parameter. </param>
		/// <param name="imageAttr">
		///   <see cref="T:System.Drawing.Imaging.ImageAttributes" /> that specifies image attribute information for the drawn image. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000427 RID: 1063 RVA: 0x00005902 File Offset: 0x00003B02
		[MonoTODO("Metafiles enumeration, for both WMF and EMF formats, isn't supported.")]
		public void EnumerateMetafile(Metafile metafile, PointF[] destPoints, RectangleF srcRect, GraphicsUnit unit, Graphics.EnumerateMetafileProc callback, IntPtr callbackData, ImageAttributes imageAttr)
		{
			throw new NotImplementedException();
		}

		/// <summary>Sends the records in a selected rectangle from a <see cref="T:System.Drawing.Imaging.Metafile" />, one at a time, to a callback method for display at a specified point using specified image attributes.</summary>
		/// <param name="metafile">
		///   <see cref="T:System.Drawing.Imaging.Metafile" /> to enumerate. </param>
		/// <param name="destPoint">
		///   <see cref="T:System.Drawing.PointF" /> structure that specifies the location of the upper-left corner of the drawn metafile. </param>
		/// <param name="srcRect">
		///   <see cref="T:System.Drawing.RectangleF" /> structure that specifies the portion of the metafile, relative to its upper-left corner, to draw. </param>
		/// <param name="unit">Member of the <see cref="T:System.Drawing.GraphicsUnit" /> enumeration that specifies the unit of measure used to determine the portion of the metafile that the rectangle specified by the <paramref name="srcRect" /> parameter contains. </param>
		/// <param name="callback">
		///   <see cref="T:System.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the metafile records are sent. </param>
		/// <param name="callbackData">Internal pointer that is required, but ignored. You can pass <see cref="F:System.IntPtr.Zero" /> for this parameter. </param>
		/// <param name="imageAttr">
		///   <see cref="T:System.Drawing.Imaging.ImageAttributes" /> that specifies image attribute information for the drawn image. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000428 RID: 1064 RVA: 0x00005902 File Offset: 0x00003B02
		[MonoTODO("Metafiles enumeration, for both WMF and EMF formats, isn't supported.")]
		public void EnumerateMetafile(Metafile metafile, PointF destPoint, RectangleF srcRect, GraphicsUnit unit, Graphics.EnumerateMetafileProc callback, IntPtr callbackData, ImageAttributes imageAttr)
		{
			throw new NotImplementedException();
		}

		/// <summary>Updates the clip region of this <see cref="T:System.Drawing.Graphics" /> to exclude the area specified by a <see cref="T:System.Drawing.Rectangle" /> structure.</summary>
		/// <param name="rect">
		///   <see cref="T:System.Drawing.Rectangle" /> structure that specifies the rectangle to exclude from the clip region. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000429 RID: 1065 RVA: 0x0000B1FA File Offset: 0x000093FA
		public void ExcludeClip(Rectangle rect)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipSetClipRectI(this.nativeObject, rect.X, rect.Y, rect.Width, rect.Height, CombineMode.Exclude));
		}

		/// <summary>Updates the clip region of this <see cref="T:System.Drawing.Graphics" /> to exclude the area specified by a <see cref="T:System.Drawing.Region" />.</summary>
		/// <param name="region">
		///   <see cref="T:System.Drawing.Region" /> that specifies the region to exclude from the clip region. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600042A RID: 1066 RVA: 0x0000B229 File Offset: 0x00009429
		public void ExcludeClip(Region region)
		{
			if (region == null)
			{
				throw new ArgumentNullException("region");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipSetClipRegion(this.nativeObject, region.NativeObject, CombineMode.Exclude));
		}

		/// <summary>Fills the interior of a closed cardinal spline curve defined by an array of <see cref="T:System.Drawing.PointF" /> structures.</summary>
		/// <param name="brush">
		///   <see cref="T:System.Drawing.Brush" /> that determines the characteristics of the fill. </param>
		/// <param name="points">Array of <see cref="T:System.Drawing.PointF" /> structures that define the spline. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="brush" /> is null.-or-<paramref name="points" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600042B RID: 1067 RVA: 0x0000B250 File Offset: 0x00009450
		public void FillClosedCurve(Brush brush, PointF[] points)
		{
			if (brush == null)
			{
				throw new ArgumentNullException("brush");
			}
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipFillClosedCurve(this.nativeObject, brush.NativeBrush, points, points.Length));
		}

		/// <summary>Fills the interior of a closed cardinal spline curve defined by an array of <see cref="T:System.Drawing.Point" /> structures.</summary>
		/// <param name="brush">
		///   <see cref="T:System.Drawing.Brush" /> that determines the characteristics of the fill. </param>
		/// <param name="points">Array of <see cref="T:System.Drawing.Point" /> structures that define the spline. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="brush" /> is null.-or-<paramref name="points" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600042C RID: 1068 RVA: 0x0000B288 File Offset: 0x00009488
		public void FillClosedCurve(Brush brush, Point[] points)
		{
			if (brush == null)
			{
				throw new ArgumentNullException("brush");
			}
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipFillClosedCurveI(this.nativeObject, brush.NativeBrush, points, points.Length));
		}

		/// <summary>Fills the interior of a closed cardinal spline curve defined by an array of <see cref="T:System.Drawing.PointF" /> structures using the specified fill mode.</summary>
		/// <param name="brush">
		///   <see cref="T:System.Drawing.Brush" /> that determines the characteristics of the fill. </param>
		/// <param name="points">Array of <see cref="T:System.Drawing.PointF" /> structures that define the spline. </param>
		/// <param name="fillmode">Member of the <see cref="T:System.Drawing.Drawing2D.FillMode" /> enumeration that determines how the curve is filled. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="brush" /> is null.-or-<paramref name="points" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600042D RID: 1069 RVA: 0x0000B2C0 File Offset: 0x000094C0
		public void FillClosedCurve(Brush brush, PointF[] points, FillMode fillmode)
		{
			if (brush == null)
			{
				throw new ArgumentNullException("brush");
			}
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			this.FillClosedCurve(brush, points, fillmode, 0.5f);
		}

		/// <summary>Fills the interior of a closed cardinal spline curve defined by an array of <see cref="T:System.Drawing.Point" /> structures using the specified fill mode.</summary>
		/// <param name="brush">
		///   <see cref="T:System.Drawing.Brush" /> that determines the characteristics of the fill. </param>
		/// <param name="points">Array of <see cref="T:System.Drawing.Point" /> structures that define the spline. </param>
		/// <param name="fillmode">Member of the <see cref="T:System.Drawing.Drawing2D.FillMode" /> enumeration that determines how the curve is filled. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="brush" /> is null.-or-<paramref name="points" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600042E RID: 1070 RVA: 0x0000B2EC File Offset: 0x000094EC
		public void FillClosedCurve(Brush brush, Point[] points, FillMode fillmode)
		{
			if (brush == null)
			{
				throw new ArgumentNullException("brush");
			}
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			this.FillClosedCurve(brush, points, fillmode, 0.5f);
		}

		/// <summary>Fills the interior of a closed cardinal spline curve defined by an array of <see cref="T:System.Drawing.PointF" /> structures using the specified fill mode and tension.</summary>
		/// <param name="brush">A <see cref="T:System.Drawing.Brush" /> that determines the characteristics of the fill. </param>
		/// <param name="points">Array of <see cref="T:System.Drawing.PointF" /> structures that define the spline. </param>
		/// <param name="fillmode">Member of the <see cref="T:System.Drawing.Drawing2D.FillMode" /> enumeration that determines how the curve is filled. </param>
		/// <param name="tension">Value greater than or equal to 0.0F that specifies the tension of the curve. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="brush" /> is null.-or-<paramref name="points" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600042F RID: 1071 RVA: 0x0000B318 File Offset: 0x00009518
		public void FillClosedCurve(Brush brush, PointF[] points, FillMode fillmode, float tension)
		{
			if (brush == null)
			{
				throw new ArgumentNullException("brush");
			}
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipFillClosedCurve2(this.nativeObject, brush.NativeBrush, points, points.Length, tension, fillmode));
		}

		/// <summary>Fills the interior of a closed cardinal spline curve defined by an array of <see cref="T:System.Drawing.Point" /> structures using the specified fill mode and tension.</summary>
		/// <param name="brush">
		///   <see cref="T:System.Drawing.Brush" /> that determines the characteristics of the fill. </param>
		/// <param name="points">Array of <see cref="T:System.Drawing.Point" /> structures that define the spline. </param>
		/// <param name="fillmode">Member of the <see cref="T:System.Drawing.Drawing2D.FillMode" /> enumeration that determines how the curve is filled. </param>
		/// <param name="tension">Value greater than or equal to 0.0F that specifies the tension of the curve. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="brush" /> is null.-or-<paramref name="points" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000430 RID: 1072 RVA: 0x0000B353 File Offset: 0x00009553
		public void FillClosedCurve(Brush brush, Point[] points, FillMode fillmode, float tension)
		{
			if (brush == null)
			{
				throw new ArgumentNullException("brush");
			}
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipFillClosedCurve2I(this.nativeObject, brush.NativeBrush, points, points.Length, tension, fillmode));
		}

		/// <summary>Fills the interior of an ellipse defined by a bounding rectangle specified by a <see cref="T:System.Drawing.Rectangle" /> structure.</summary>
		/// <param name="brush">
		///   <see cref="T:System.Drawing.Brush" /> that determines the characteristics of the fill. </param>
		/// <param name="rect">
		///   <see cref="T:System.Drawing.Rectangle" /> structure that represents the bounding rectangle that defines the ellipse. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="brush" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000431 RID: 1073 RVA: 0x0000B38E File Offset: 0x0000958E
		public void FillEllipse(Brush brush, Rectangle rect)
		{
			if (brush == null)
			{
				throw new ArgumentNullException("brush");
			}
			this.FillEllipse(brush, rect.X, rect.Y, rect.Width, rect.Height);
		}

		/// <summary>Fills the interior of an ellipse defined by a bounding rectangle specified by a <see cref="T:System.Drawing.RectangleF" /> structure.</summary>
		/// <param name="brush">
		///   <see cref="T:System.Drawing.Brush" /> that determines the characteristics of the fill. </param>
		/// <param name="rect">
		///   <see cref="T:System.Drawing.RectangleF" /> structure that represents the bounding rectangle that defines the ellipse. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="brush" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000432 RID: 1074 RVA: 0x0000B3C1 File Offset: 0x000095C1
		public void FillEllipse(Brush brush, RectangleF rect)
		{
			if (brush == null)
			{
				throw new ArgumentNullException("brush");
			}
			this.FillEllipse(brush, rect.X, rect.Y, rect.Width, rect.Height);
		}

		/// <summary>Fills the interior of an ellipse defined by a bounding rectangle specified by a pair of coordinates, a width, and a height.</summary>
		/// <param name="brush">
		///   <see cref="T:System.Drawing.Brush" /> that determines the characteristics of the fill. </param>
		/// <param name="x">The x-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse. </param>
		/// <param name="y">The y-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse. </param>
		/// <param name="width">Width of the bounding rectangle that defines the ellipse. </param>
		/// <param name="height">Height of the bounding rectangle that defines the ellipse. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="brush" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000433 RID: 1075 RVA: 0x0000B3F4 File Offset: 0x000095F4
		public void FillEllipse(Brush brush, float x, float y, float width, float height)
		{
			if (brush == null)
			{
				throw new ArgumentNullException("brush");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipFillEllipse(this.nativeObject, brush.NativeBrush, x, y, width, height));
		}

		/// <summary>Fills the interior of an ellipse defined by a bounding rectangle specified by a pair of coordinates, a width, and a height.</summary>
		/// <param name="brush">
		///   <see cref="T:System.Drawing.Brush" /> that determines the characteristics of the fill. </param>
		/// <param name="x">The x-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse. </param>
		/// <param name="y">The y-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse. </param>
		/// <param name="width">Width of the bounding rectangle that defines the ellipse. </param>
		/// <param name="height">Height of the bounding rectangle that defines the ellipse. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="brush" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000434 RID: 1076 RVA: 0x0000B420 File Offset: 0x00009620
		public void FillEllipse(Brush brush, int x, int y, int width, int height)
		{
			if (brush == null)
			{
				throw new ArgumentNullException("brush");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipFillEllipseI(this.nativeObject, brush.NativeBrush, x, y, width, height));
		}

		/// <summary>Fills the interior of a <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</summary>
		/// <param name="brush">
		///   <see cref="T:System.Drawing.Brush" /> that determines the characteristics of the fill. </param>
		/// <param name="path">
		///   <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> that represents the path to fill. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="brush" /> is null.-or-<paramref name="path" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000435 RID: 1077 RVA: 0x0000B44C File Offset: 0x0000964C
		public void FillPath(Brush brush, GraphicsPath path)
		{
			if (brush == null)
			{
				throw new ArgumentNullException("brush");
			}
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipFillPath(this.nativeObject, brush.NativeBrush, path.nativePath));
		}

		/// <summary>Fills the interior of a pie section defined by an ellipse specified by a <see cref="T:System.Drawing.RectangleF" /> structure and two radial lines.</summary>
		/// <param name="brush">
		///   <see cref="T:System.Drawing.Brush" /> that determines the characteristics of the fill. </param>
		/// <param name="rect">
		///   <see cref="T:System.Drawing.Rectangle" /> structure that represents the bounding rectangle that defines the ellipse from which the pie section comes. </param>
		/// <param name="startAngle">Angle in degrees measured clockwise from the x-axis to the first side of the pie section. </param>
		/// <param name="sweepAngle">Angle in degrees measured clockwise from the <paramref name="startAngle" /> parameter to the second side of the pie section. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="brush" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000436 RID: 1078 RVA: 0x0000B488 File Offset: 0x00009688
		public void FillPie(Brush brush, Rectangle rect, float startAngle, float sweepAngle)
		{
			if (brush == null)
			{
				throw new ArgumentNullException("brush");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipFillPie(this.nativeObject, brush.NativeBrush, (float)rect.X, (float)rect.Y, (float)rect.Width, (float)rect.Height, startAngle, sweepAngle));
		}

		/// <summary>Fills the interior of a pie section defined by an ellipse specified by a pair of coordinates, a width, a height, and two radial lines.</summary>
		/// <param name="brush">
		///   <see cref="T:System.Drawing.Brush" /> that determines the characteristics of the fill. </param>
		/// <param name="x">The x-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse from which the pie section comes. </param>
		/// <param name="y">The y-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse from which the pie section comes. </param>
		/// <param name="width">Width of the bounding rectangle that defines the ellipse from which the pie section comes. </param>
		/// <param name="height">Height of the bounding rectangle that defines the ellipse from which the pie section comes. </param>
		/// <param name="startAngle">Angle in degrees measured clockwise from the x-axis to the first side of the pie section. </param>
		/// <param name="sweepAngle">Angle in degrees measured clockwise from the <paramref name="startAngle" /> parameter to the second side of the pie section. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="brush" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000437 RID: 1079 RVA: 0x0000B4DC File Offset: 0x000096DC
		public void FillPie(Brush brush, int x, int y, int width, int height, int startAngle, int sweepAngle)
		{
			if (brush == null)
			{
				throw new ArgumentNullException("brush");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipFillPieI(this.nativeObject, brush.NativeBrush, x, y, width, height, (float)startAngle, (float)sweepAngle));
		}

		/// <summary>Fills the interior of a pie section defined by an ellipse specified by a pair of coordinates, a width, a height, and two radial lines.</summary>
		/// <param name="brush">
		///   <see cref="T:System.Drawing.Brush" /> that determines the characteristics of the fill. </param>
		/// <param name="x">The x-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse from which the pie section comes. </param>
		/// <param name="y">The y-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse from which the pie section comes. </param>
		/// <param name="width">Width of the bounding rectangle that defines the ellipse from which the pie section comes. </param>
		/// <param name="height">Height of the bounding rectangle that defines the ellipse from which the pie section comes. </param>
		/// <param name="startAngle">Angle in degrees measured clockwise from the x-axis to the first side of the pie section. </param>
		/// <param name="sweepAngle">Angle in degrees measured clockwise from the <paramref name="startAngle" /> parameter to the second side of the pie section. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="brush" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000438 RID: 1080 RVA: 0x0000B50E File Offset: 0x0000970E
		public void FillPie(Brush brush, float x, float y, float width, float height, float startAngle, float sweepAngle)
		{
			if (brush == null)
			{
				throw new ArgumentNullException("brush");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipFillPie(this.nativeObject, brush.NativeBrush, x, y, width, height, startAngle, sweepAngle));
		}

		/// <summary>Fills the interior of a polygon defined by an array of points specified by <see cref="T:System.Drawing.PointF" /> structures.</summary>
		/// <param name="brush">
		///   <see cref="T:System.Drawing.Brush" /> that determines the characteristics of the fill. </param>
		/// <param name="points">Array of <see cref="T:System.Drawing.PointF" /> structures that represent the vertices of the polygon to fill. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="brush" /> is null.-or-<paramref name="points" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000439 RID: 1081 RVA: 0x0000B53E File Offset: 0x0000973E
		public void FillPolygon(Brush brush, PointF[] points)
		{
			if (brush == null)
			{
				throw new ArgumentNullException("brush");
			}
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipFillPolygon2(this.nativeObject, brush.NativeBrush, points, points.Length));
		}

		/// <summary>Fills the interior of a polygon defined by an array of points specified by <see cref="T:System.Drawing.Point" /> structures.</summary>
		/// <param name="brush">
		///   <see cref="T:System.Drawing.Brush" /> that determines the characteristics of the fill. </param>
		/// <param name="points">Array of <see cref="T:System.Drawing.Point" /> structures that represent the vertices of the polygon to fill. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="brush" /> is null.-or-<paramref name="points" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600043A RID: 1082 RVA: 0x0000B576 File Offset: 0x00009776
		public void FillPolygon(Brush brush, Point[] points)
		{
			if (brush == null)
			{
				throw new ArgumentNullException("brush");
			}
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipFillPolygon2I(this.nativeObject, brush.NativeBrush, points, points.Length));
		}

		/// <summary>Fills the interior of a polygon defined by an array of points specified by <see cref="T:System.Drawing.Point" /> structures using the specified fill mode.</summary>
		/// <param name="brush">
		///   <see cref="T:System.Drawing.Brush" /> that determines the characteristics of the fill. </param>
		/// <param name="points">Array of <see cref="T:System.Drawing.Point" /> structures that represent the vertices of the polygon to fill. </param>
		/// <param name="fillMode">Member of the <see cref="T:System.Drawing.Drawing2D.FillMode" /> enumeration that determines the style of the fill. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="brush" /> is null.-or-<paramref name="points" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600043B RID: 1083 RVA: 0x0000B5AE File Offset: 0x000097AE
		public void FillPolygon(Brush brush, Point[] points, FillMode fillMode)
		{
			if (brush == null)
			{
				throw new ArgumentNullException("brush");
			}
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipFillPolygonI(this.nativeObject, brush.NativeBrush, points, points.Length, fillMode));
		}

		/// <summary>Fills the interior of a polygon defined by an array of points specified by <see cref="T:System.Drawing.PointF" /> structures using the specified fill mode.</summary>
		/// <param name="brush">
		///   <see cref="T:System.Drawing.Brush" /> that determines the characteristics of the fill. </param>
		/// <param name="points">Array of <see cref="T:System.Drawing.PointF" /> structures that represent the vertices of the polygon to fill. </param>
		/// <param name="fillMode">Member of the <see cref="T:System.Drawing.Drawing2D.FillMode" /> enumeration that determines the style of the fill. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="brush" /> is null.-or-<paramref name="points" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600043C RID: 1084 RVA: 0x0000B5E7 File Offset: 0x000097E7
		public void FillPolygon(Brush brush, PointF[] points, FillMode fillMode)
		{
			if (brush == null)
			{
				throw new ArgumentNullException("brush");
			}
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipFillPolygon(this.nativeObject, brush.NativeBrush, points, points.Length, fillMode));
		}

		/// <summary>Fills the interior of a rectangle specified by a <see cref="T:System.Drawing.RectangleF" /> structure.</summary>
		/// <param name="brush">
		///   <see cref="T:System.Drawing.Brush" /> that determines the characteristics of the fill. </param>
		/// <param name="rect">
		///   <see cref="T:System.Drawing.RectangleF" /> structure that represents the rectangle to fill. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="brush" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600043D RID: 1085 RVA: 0x0000B620 File Offset: 0x00009820
		public void FillRectangle(Brush brush, RectangleF rect)
		{
			if (brush == null)
			{
				throw new ArgumentNullException("brush");
			}
			this.FillRectangle(brush, rect.Left, rect.Top, rect.Width, rect.Height);
		}

		/// <summary>Fills the interior of a rectangle specified by a <see cref="T:System.Drawing.Rectangle" /> structure.</summary>
		/// <param name="brush">
		///   <see cref="T:System.Drawing.Brush" /> that determines the characteristics of the fill. </param>
		/// <param name="rect">
		///   <see cref="T:System.Drawing.Rectangle" /> structure that represents the rectangle to fill. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="brush" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600043E RID: 1086 RVA: 0x0000B653 File Offset: 0x00009853
		public void FillRectangle(Brush brush, Rectangle rect)
		{
			if (brush == null)
			{
				throw new ArgumentNullException("brush");
			}
			this.FillRectangle(brush, rect.Left, rect.Top, rect.Width, rect.Height);
		}

		/// <summary>Fills the interior of a rectangle specified by a pair of coordinates, a width, and a height.</summary>
		/// <param name="brush">
		///   <see cref="T:System.Drawing.Brush" /> that determines the characteristics of the fill. </param>
		/// <param name="x">The x-coordinate of the upper-left corner of the rectangle to fill. </param>
		/// <param name="y">The y-coordinate of the upper-left corner of the rectangle to fill. </param>
		/// <param name="width">Width of the rectangle to fill. </param>
		/// <param name="height">Height of the rectangle to fill. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="brush" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600043F RID: 1087 RVA: 0x0000B686 File Offset: 0x00009886
		public void FillRectangle(Brush brush, int x, int y, int width, int height)
		{
			if (brush == null)
			{
				throw new ArgumentNullException("brush");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipFillRectangleI(this.nativeObject, brush.NativeBrush, x, y, width, height));
		}

		/// <summary>Fills the interior of a rectangle specified by a pair of coordinates, a width, and a height.</summary>
		/// <param name="brush">
		///   <see cref="T:System.Drawing.Brush" /> that determines the characteristics of the fill. </param>
		/// <param name="x">The x-coordinate of the upper-left corner of the rectangle to fill. </param>
		/// <param name="y">The y-coordinate of the upper-left corner of the rectangle to fill. </param>
		/// <param name="width">Width of the rectangle to fill. </param>
		/// <param name="height">Height of the rectangle to fill. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="brush" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000440 RID: 1088 RVA: 0x0000B6B2 File Offset: 0x000098B2
		public void FillRectangle(Brush brush, float x, float y, float width, float height)
		{
			if (brush == null)
			{
				throw new ArgumentNullException("brush");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipFillRectangle(this.nativeObject, brush.NativeBrush, x, y, width, height));
		}

		/// <summary>Fills the interiors of a series of rectangles specified by <see cref="T:System.Drawing.Rectangle" /> structures.</summary>
		/// <param name="brush">
		///   <see cref="T:System.Drawing.Brush" /> that determines the characteristics of the fill. </param>
		/// <param name="rects">Array of <see cref="T:System.Drawing.Rectangle" /> structures that represent the rectangles to fill. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="brush" /> is null.-or-<paramref name="rects" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="rects" /> is a zero-length array.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000441 RID: 1089 RVA: 0x0000B6DE File Offset: 0x000098DE
		public void FillRectangles(Brush brush, Rectangle[] rects)
		{
			if (brush == null)
			{
				throw new ArgumentNullException("brush");
			}
			if (rects == null)
			{
				throw new ArgumentNullException("rects");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipFillRectanglesI(this.nativeObject, brush.NativeBrush, rects, rects.Length));
		}

		/// <summary>Fills the interiors of a series of rectangles specified by <see cref="T:System.Drawing.RectangleF" /> structures.</summary>
		/// <param name="brush">
		///   <see cref="T:System.Drawing.Brush" /> that determines the characteristics of the fill. </param>
		/// <param name="rects">Array of <see cref="T:System.Drawing.RectangleF" /> structures that represent the rectangles to fill. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="brush" /> is null.-or-<paramref name="rects" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="Rects" /> is a zero-length array.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000442 RID: 1090 RVA: 0x0000B716 File Offset: 0x00009916
		public void FillRectangles(Brush brush, RectangleF[] rects)
		{
			if (brush == null)
			{
				throw new ArgumentNullException("brush");
			}
			if (rects == null)
			{
				throw new ArgumentNullException("rects");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipFillRectangles(this.nativeObject, brush.NativeBrush, rects, rects.Length));
		}

		/// <summary>Fills the interior of a <see cref="T:System.Drawing.Region" />.</summary>
		/// <param name="brush">
		///   <see cref="T:System.Drawing.Brush" /> that determines the characteristics of the fill. </param>
		/// <param name="region">
		///   <see cref="T:System.Drawing.Region" /> that represents the area to fill. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="brush" /> is null.-or-<paramref name="region" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000443 RID: 1091 RVA: 0x0000B74E File Offset: 0x0000994E
		public void FillRegion(Brush brush, Region region)
		{
			if (brush == null)
			{
				throw new ArgumentNullException("brush");
			}
			if (region == null)
			{
				throw new ArgumentNullException("region");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipFillRegion(this.nativeObject, brush.NativeBrush, region.NativeObject));
		}

		/// <summary>Forces execution of all pending graphics operations and returns immediately without waiting for the operations to finish.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000444 RID: 1092 RVA: 0x0000B788 File Offset: 0x00009988
		public void Flush()
		{
			this.Flush(FlushIntention.Flush);
		}

		/// <summary>Forces execution of all pending graphics operations with the method waiting or not waiting, as specified, to return before the operations finish.</summary>
		/// <param name="intention">Member of the <see cref="T:System.Drawing.Drawing2D.FlushIntention" /> enumeration that specifies whether the method returns immediately or waits for any existing operations to finish. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000445 RID: 1093 RVA: 0x0000B791 File Offset: 0x00009991
		public void Flush(FlushIntention intention)
		{
			if (this.nativeObject == IntPtr.Zero)
			{
				return;
			}
			GDIPlus.CheckStatus(GDIPlus.GdipFlush(this.nativeObject, intention));
			if (this.maccontext != null)
			{
				this.maccontext.Synchronize();
			}
		}

		/// <summary>Creates a new <see cref="T:System.Drawing.Graphics" /> from the specified handle to a device context.</summary>
		/// <returns>This method returns a new <see cref="T:System.Drawing.Graphics" /> for the specified device context.</returns>
		/// <param name="hdc">Handle to a device context. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000446 RID: 1094 RVA: 0x0000B7CC File Offset: 0x000099CC
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static Graphics FromHdc(IntPtr hdc)
		{
			IntPtr intPtr;
			GDIPlus.CheckStatus(GDIPlus.GdipCreateFromHDC(hdc, out intPtr));
			return new Graphics(intPtr);
		}

		/// <summary>Creates a new <see cref="T:System.Drawing.Graphics" /> from the specified handle to a device context and handle to a device.</summary>
		/// <returns>This method returns a new <see cref="T:System.Drawing.Graphics" /> for the specified device context and device.</returns>
		/// <param name="hdc">Handle to a device context. </param>
		/// <param name="hdevice">Handle to a device. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000447 RID: 1095 RVA: 0x00005902 File Offset: 0x00003B02
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[MonoTODO]
		public static Graphics FromHdc(IntPtr hdc, IntPtr hdevice)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns a <see cref="T:System.Drawing.Graphics" /> for the specified device context.</summary>
		/// <returns>A <see cref="T:System.Drawing.Graphics" /> for the specified device context.</returns>
		/// <param name="hdc">Handle to a device context. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06000448 RID: 1096 RVA: 0x0000B7EC File Offset: 0x000099EC
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static Graphics FromHdcInternal(IntPtr hdc)
		{
			GDIPlus.Display = hdc;
			return null;
		}

		/// <summary>Creates a new <see cref="T:System.Drawing.Graphics" /> from the specified handle to a window.</summary>
		/// <returns>This method returns a new <see cref="T:System.Drawing.Graphics" /> for the specified window handle.</returns>
		/// <param name="hwnd">Handle to a window. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000449 RID: 1097 RVA: 0x0000B7F8 File Offset: 0x000099F8
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static Graphics FromHwnd(IntPtr hwnd)
		{
			IntPtr intPtr;
			if (GDIPlus.UseCocoaDrawable)
			{
				CocoaContext cgcontextForNSView = MacSupport.GetCGContextForNSView(hwnd);
				GDIPlus.GdipCreateFromContext_macosx(cgcontextForNSView.ctx, cgcontextForNSView.width, cgcontextForNSView.height, out intPtr);
				return new Graphics(intPtr)
				{
					maccontext = cgcontextForNSView
				};
			}
			if (GDIPlus.UseCarbonDrawable)
			{
				CarbonContext cgcontextForView = MacSupport.GetCGContextForView(hwnd);
				GDIPlus.GdipCreateFromContext_macosx(cgcontextForView.ctx, cgcontextForView.width, cgcontextForView.height, out intPtr);
				return new Graphics(intPtr)
				{
					maccontext = cgcontextForView
				};
			}
			if (GDIPlus.UseX11Drawable)
			{
				if (GDIPlus.Display == IntPtr.Zero)
				{
					GDIPlus.Display = GDIPlus.XOpenDisplay(IntPtr.Zero);
					if (GDIPlus.Display == IntPtr.Zero)
					{
						throw new NotSupportedException("Could not open display (X-Server required. Check your DISPLAY environment variable)");
					}
				}
				if (hwnd == IntPtr.Zero)
				{
					hwnd = GDIPlus.XRootWindow(GDIPlus.Display, GDIPlus.XDefaultScreen(GDIPlus.Display));
				}
				return Graphics.FromXDrawable(hwnd, GDIPlus.Display);
			}
			GDIPlus.CheckStatus(GDIPlus.GdipCreateFromHWND(hwnd, out intPtr));
			return new Graphics(intPtr);
		}

		/// <summary>Creates a new <see cref="T:System.Drawing.Graphics" /> for the specified windows handle.</summary>
		/// <returns>A <see cref="T:System.Drawing.Graphics" /> for the specified window handle.</returns>
		/// <param name="hwnd">Handle to a window. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x0600044A RID: 1098 RVA: 0x0000B900 File Offset: 0x00009B00
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static Graphics FromHwndInternal(IntPtr hwnd)
		{
			return Graphics.FromHwnd(hwnd);
		}

		/// <summary>Creates a new <see cref="T:System.Drawing.Graphics" /> from the specified <see cref="T:System.Drawing.Image" />.</summary>
		/// <returns>This method returns a new <see cref="T:System.Drawing.Graphics" /> for the specified <see cref="T:System.Drawing.Image" />.</returns>
		/// <param name="image">
		///   <see cref="T:System.Drawing.Image" /> from which to create the new <see cref="T:System.Drawing.Graphics" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="image" /> is null.</exception>
		/// <exception cref="T:System.Exception">
		///   <paramref name="image" /> has an indexed pixel format or its format is undefined.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600044B RID: 1099 RVA: 0x0000B908 File Offset: 0x00009B08
		public static Graphics FromImage(Image image)
		{
			if (image == null)
			{
				throw new ArgumentNullException("image");
			}
			if ((image.PixelFormat & PixelFormat.Indexed) != PixelFormat.Undefined)
			{
				throw new Exception(Locale.GetText("Cannot create Graphics from an indexed bitmap."));
			}
			IntPtr intPtr;
			GDIPlus.CheckStatus(GDIPlus.GdipGetImageGraphicsContext(image.nativeObject, out intPtr));
			Graphics graphics = new Graphics(intPtr);
			if (GDIPlus.RunningOnUnix())
			{
				Rectangle rectangle = new Rectangle(0, 0, image.Width, image.Height);
				GDIPlus.GdipSetVisibleClip_linux(graphics.NativeObject, ref rectangle);
			}
			return graphics;
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x0000B988 File Offset: 0x00009B88
		internal static Graphics FromXDrawable(IntPtr drawable, IntPtr display)
		{
			IntPtr intPtr;
			GDIPlus.CheckStatus(GDIPlus.GdipCreateFromXDrawable_linux(drawable, display, out intPtr));
			return new Graphics(intPtr);
		}

		/// <summary>Gets a handle to the current Windows halftone palette.</summary>
		/// <returns>Internal pointer that specifies the handle to the palette.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600044D RID: 1101 RVA: 0x00005902 File Offset: 0x00003B02
		[MonoTODO]
		public static IntPtr GetHalftonePalette()
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the handle to the device context associated with this <see cref="T:System.Drawing.Graphics" />.</summary>
		/// <returns>Handle to the device context associated with this <see cref="T:System.Drawing.Graphics" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600044E RID: 1102 RVA: 0x0000B9A9 File Offset: 0x00009BA9
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public IntPtr GetHdc()
		{
			GDIPlus.CheckStatus(GDIPlus.GdipGetDC(this.nativeObject, out this.deviceContextHdc));
			return this.deviceContextHdc;
		}

		/// <summary>Gets the nearest color to the specified <see cref="T:System.Drawing.Color" /> structure.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> structure that represents the nearest color to the one specified with the <paramref name="color" /> parameter.</returns>
		/// <param name="color">
		///   <see cref="T:System.Drawing.Color" /> structure for which to find a match. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600044F RID: 1103 RVA: 0x0000B9C8 File Offset: 0x00009BC8
		public Color GetNearestColor(Color color)
		{
			int num;
			GDIPlus.CheckStatus(GDIPlus.GdipGetNearestColor(this.nativeObject, out num));
			return Color.FromArgb(num);
		}

		/// <summary>Updates the clip region of this <see cref="T:System.Drawing.Graphics" /> to the intersection of the current clip region and the specified <see cref="T:System.Drawing.Region" />.</summary>
		/// <param name="region">
		///   <see cref="T:System.Drawing.Region" /> to intersect with the current region. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000450 RID: 1104 RVA: 0x0000B9ED File Offset: 0x00009BED
		public void IntersectClip(Region region)
		{
			if (region == null)
			{
				throw new ArgumentNullException("region");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipSetClipRegion(this.nativeObject, region.NativeObject, CombineMode.Intersect));
		}

		/// <summary>Updates the clip region of this <see cref="T:System.Drawing.Graphics" /> to the intersection of the current clip region and the specified <see cref="T:System.Drawing.RectangleF" /> structure.</summary>
		/// <param name="rect">
		///   <see cref="T:System.Drawing.RectangleF" /> structure to intersect with the current clip region. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000451 RID: 1105 RVA: 0x0000BA14 File Offset: 0x00009C14
		public void IntersectClip(RectangleF rect)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipSetClipRect(this.nativeObject, rect.X, rect.Y, rect.Width, rect.Height, CombineMode.Intersect));
		}

		/// <summary>Updates the clip region of this <see cref="T:System.Drawing.Graphics" /> to the intersection of the current clip region and the specified <see cref="T:System.Drawing.Rectangle" /> structure.</summary>
		/// <param name="rect">
		///   <see cref="T:System.Drawing.Rectangle" /> structure to intersect with the current clip region. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000452 RID: 1106 RVA: 0x0000BA43 File Offset: 0x00009C43
		public void IntersectClip(Rectangle rect)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipSetClipRectI(this.nativeObject, rect.X, rect.Y, rect.Width, rect.Height, CombineMode.Intersect));
		}

		/// <summary>Indicates whether the specified <see cref="T:System.Drawing.Point" /> structure is contained within the visible clip region of this <see cref="T:System.Drawing.Graphics" />.</summary>
		/// <returns>true if the point specified by the <paramref name="point" /> parameter is contained within the visible clip region of this <see cref="T:System.Drawing.Graphics" />; otherwise, false.</returns>
		/// <param name="point">
		///   <see cref="T:System.Drawing.Point" /> structure to test for visibility. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000453 RID: 1107 RVA: 0x0000BA74 File Offset: 0x00009C74
		public bool IsVisible(Point point)
		{
			bool flag = false;
			GDIPlus.CheckStatus(GDIPlus.GdipIsVisiblePointI(this.nativeObject, point.X, point.Y, out flag));
			return flag;
		}

		/// <summary>Indicates whether the rectangle specified by a <see cref="T:System.Drawing.RectangleF" /> structure is contained within the visible clip region of this <see cref="T:System.Drawing.Graphics" />.</summary>
		/// <returns>true if the rectangle specified by the <paramref name="rect" /> parameter is contained within the visible clip region of this <see cref="T:System.Drawing.Graphics" />; otherwise, false.</returns>
		/// <param name="rect">
		///   <see cref="T:System.Drawing.RectangleF" /> structure to test for visibility. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000454 RID: 1108 RVA: 0x0000BAA4 File Offset: 0x00009CA4
		public bool IsVisible(RectangleF rect)
		{
			bool flag = false;
			GDIPlus.CheckStatus(GDIPlus.GdipIsVisibleRect(this.nativeObject, rect.X, rect.Y, rect.Width, rect.Height, out flag));
			return flag;
		}

		/// <summary>Indicates whether the specified <see cref="T:System.Drawing.PointF" /> structure is contained within the visible clip region of this <see cref="T:System.Drawing.Graphics" />.</summary>
		/// <returns>true if the point specified by the <paramref name="point" /> parameter is contained within the visible clip region of this <see cref="T:System.Drawing.Graphics" />; otherwise, false.</returns>
		/// <param name="point">
		///   <see cref="T:System.Drawing.PointF" /> structure to test for visibility. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000455 RID: 1109 RVA: 0x0000BAE4 File Offset: 0x00009CE4
		public bool IsVisible(PointF point)
		{
			bool flag = false;
			GDIPlus.CheckStatus(GDIPlus.GdipIsVisiblePoint(this.nativeObject, point.X, point.Y, out flag));
			return flag;
		}

		/// <summary>Indicates whether the rectangle specified by a <see cref="T:System.Drawing.Rectangle" /> structure is contained within the visible clip region of this <see cref="T:System.Drawing.Graphics" />.</summary>
		/// <returns>true if the rectangle specified by the <paramref name="rect" /> parameter is contained within the visible clip region of this <see cref="T:System.Drawing.Graphics" />; otherwise, false.</returns>
		/// <param name="rect">
		///   <see cref="T:System.Drawing.Rectangle" /> structure to test for visibility. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000456 RID: 1110 RVA: 0x0000BB14 File Offset: 0x00009D14
		public bool IsVisible(Rectangle rect)
		{
			bool flag = false;
			GDIPlus.CheckStatus(GDIPlus.GdipIsVisibleRectI(this.nativeObject, rect.X, rect.Y, rect.Width, rect.Height, out flag));
			return flag;
		}

		/// <summary>Indicates whether the point specified by a pair of coordinates is contained within the visible clip region of this <see cref="T:System.Drawing.Graphics" />.</summary>
		/// <returns>true if the point defined by the <paramref name="x" /> and <paramref name="y" /> parameters is contained within the visible clip region of this <see cref="T:System.Drawing.Graphics" />; otherwise, false.</returns>
		/// <param name="x">The x-coordinate of the point to test for visibility. </param>
		/// <param name="y">The y-coordinate of the point to test for visibility. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000457 RID: 1111 RVA: 0x0000BB52 File Offset: 0x00009D52
		public bool IsVisible(float x, float y)
		{
			return this.IsVisible(new PointF(x, y));
		}

		/// <summary>Indicates whether the point specified by a pair of coordinates is contained within the visible clip region of this <see cref="T:System.Drawing.Graphics" />.</summary>
		/// <returns>true if the point defined by the <paramref name="x" /> and <paramref name="y" /> parameters is contained within the visible clip region of this <see cref="T:System.Drawing.Graphics" />; otherwise, false.</returns>
		/// <param name="x">The x-coordinate of the point to test for visibility. </param>
		/// <param name="y">The y-coordinate of the point to test for visibility. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000458 RID: 1112 RVA: 0x0000BB61 File Offset: 0x00009D61
		public bool IsVisible(int x, int y)
		{
			return this.IsVisible(new Point(x, y));
		}

		/// <summary>Indicates whether the rectangle specified by a pair of coordinates, a width, and a height is contained within the visible clip region of this <see cref="T:System.Drawing.Graphics" />.</summary>
		/// <returns>true if the rectangle defined by the <paramref name="x" />, <paramref name="y" />, <paramref name="width" />, and <paramref name="height" /> parameters is contained within the visible clip region of this <see cref="T:System.Drawing.Graphics" />; otherwise, false.</returns>
		/// <param name="x">The x-coordinate of the upper-left corner of the rectangle to test for visibility. </param>
		/// <param name="y">The y-coordinate of the upper-left corner of the rectangle to test for visibility. </param>
		/// <param name="width">Width of the rectangle to test for visibility. </param>
		/// <param name="height">Height of the rectangle to test for visibility. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000459 RID: 1113 RVA: 0x0000BB70 File Offset: 0x00009D70
		public bool IsVisible(float x, float y, float width, float height)
		{
			return this.IsVisible(new RectangleF(x, y, width, height));
		}

		/// <summary>Indicates whether the rectangle specified by a pair of coordinates, a width, and a height is contained within the visible clip region of this <see cref="T:System.Drawing.Graphics" />.</summary>
		/// <returns>true if the rectangle defined by the <paramref name="x" />, <paramref name="y" />, <paramref name="width" />, and <paramref name="height" /> parameters is contained within the visible clip region of this <see cref="T:System.Drawing.Graphics" />; otherwise, false.</returns>
		/// <param name="x">The x-coordinate of the upper-left corner of the rectangle to test for visibility. </param>
		/// <param name="y">The y-coordinate of the upper-left corner of the rectangle to test for visibility. </param>
		/// <param name="width">Width of the rectangle to test for visibility. </param>
		/// <param name="height">Height of the rectangle to test for visibility. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600045A RID: 1114 RVA: 0x0000BB82 File Offset: 0x00009D82
		public bool IsVisible(int x, int y, int width, int height)
		{
			return this.IsVisible(new Rectangle(x, y, width, height));
		}

		/// <summary>Gets an array of <see cref="T:System.Drawing.Region" /> objects, each of which bounds a range of character positions within the specified string.</summary>
		/// <returns>This method returns an array of <see cref="T:System.Drawing.Region" /> objects, each of which bounds a range of character positions within the specified string.</returns>
		/// <param name="text">String to measure. </param>
		/// <param name="font">
		///   <see cref="T:System.Drawing.Font" /> that defines the text format of the string. </param>
		/// <param name="layoutRect">
		///   <see cref="T:System.Drawing.RectangleF" /> structure that specifies the layout rectangle for the string. </param>
		/// <param name="stringFormat">
		///   <see cref="T:System.Drawing.StringFormat" /> that represents formatting information, such as line spacing, for the string. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600045B RID: 1115 RVA: 0x0000BB94 File Offset: 0x00009D94
		public Region[] MeasureCharacterRanges(string text, Font font, RectangleF layoutRect, StringFormat stringFormat)
		{
			if (text == null || text.Length == 0)
			{
				return new Region[0];
			}
			if (font == null)
			{
				throw new ArgumentNullException("font");
			}
			if (stringFormat == null)
			{
				throw new ArgumentException("stringFormat");
			}
			int measurableCharacterRangeCount = stringFormat.GetMeasurableCharacterRangeCount();
			if (measurableCharacterRangeCount == 0)
			{
				return new Region[0];
			}
			IntPtr[] array = new IntPtr[measurableCharacterRangeCount];
			Region[] array2 = new Region[measurableCharacterRangeCount];
			for (int i = 0; i < measurableCharacterRangeCount; i++)
			{
				array2[i] = new Region();
				array[i] = array2[i].NativeObject;
			}
			GDIPlus.CheckStatus(GDIPlus.GdipMeasureCharacterRanges(this.nativeObject, text, text.Length, font.NativeObject, ref layoutRect, stringFormat.NativeObject, measurableCharacterRangeCount, out array[0]));
			return array2;
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x0000BC40 File Offset: 0x00009E40
		private SizeF GdipMeasureString(IntPtr graphics, string text, Font font, ref RectangleF layoutRect, IntPtr stringFormat)
		{
			if (text == null || text.Length == 0)
			{
				return SizeF.Empty;
			}
			if (font == null)
			{
				throw new ArgumentNullException("font");
			}
			RectangleF rectangleF = default(RectangleF);
			GDIPlus.CheckStatus(GDIPlus.GdipMeasureString(this.nativeObject, text, text.Length, font.NativeObject, ref layoutRect, stringFormat, out rectangleF, null, null));
			return new SizeF(rectangleF.Width, rectangleF.Height);
		}

		/// <summary>Measures the specified string when drawn with the specified <see cref="T:System.Drawing.Font" />.</summary>
		/// <returns>This method returns a <see cref="T:System.Drawing.SizeF" /> structure that represents the size, in the units specified by the <see cref="P:System.Drawing.Graphics.PageUnit" /> property, of the string specified by the <paramref name="text" /> parameter as drawn with the <paramref name="font" /> parameter.</returns>
		/// <param name="text">String to measure. </param>
		/// <param name="font">
		///   <see cref="T:System.Drawing.Font" /> that defines the text format of the string. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="font" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600045D RID: 1117 RVA: 0x0000BCAE File Offset: 0x00009EAE
		public SizeF MeasureString(string text, Font font)
		{
			return this.MeasureString(text, font, SizeF.Empty);
		}

		/// <summary>Measures the specified string when drawn with the specified <see cref="T:System.Drawing.Font" /> within the specified layout area.</summary>
		/// <returns>This method returns a <see cref="T:System.Drawing.SizeF" /> structure that represents the size, in the units specified by the <see cref="P:System.Drawing.Graphics.PageUnit" /> property, of the string specified by the <paramref name="text" /> parameter as drawn with the <paramref name="font" /> parameter.</returns>
		/// <param name="text">String to measure. </param>
		/// <param name="font">
		///   <see cref="T:System.Drawing.Font" /> defines the text format of the string. </param>
		/// <param name="layoutArea">
		///   <see cref="T:System.Drawing.SizeF" /> structure that specifies the maximum layout area for the text. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="font" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600045E RID: 1118 RVA: 0x0000BCC0 File Offset: 0x00009EC0
		public SizeF MeasureString(string text, Font font, SizeF layoutArea)
		{
			RectangleF rectangleF = new RectangleF(0f, 0f, layoutArea.Width, layoutArea.Height);
			return this.GdipMeasureString(this.nativeObject, text, font, ref rectangleF, IntPtr.Zero);
		}

		/// <summary>Measures the specified string when drawn with the specified <see cref="T:System.Drawing.Font" />.</summary>
		/// <returns>This method returns a <see cref="T:System.Drawing.SizeF" /> structure that represents the size, in the units specified by the <see cref="P:System.Drawing.Graphics.PageUnit" /> property, of the string specified in the <paramref name="text" /> parameter as drawn with the <paramref name="font" /> parameter.</returns>
		/// <param name="text">String to measure. </param>
		/// <param name="font">
		///   <see cref="T:System.Drawing.Font" /> that defines the format of the string. </param>
		/// <param name="width">Maximum width of the string in pixels. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="font" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600045F RID: 1119 RVA: 0x0000BD04 File Offset: 0x00009F04
		public SizeF MeasureString(string text, Font font, int width)
		{
			RectangleF rectangleF = new RectangleF(0f, 0f, (float)width, 2.1474836E+09f);
			return this.GdipMeasureString(this.nativeObject, text, font, ref rectangleF, IntPtr.Zero);
		}

		/// <summary>Measures the specified string when drawn with the specified <see cref="T:System.Drawing.Font" /> and formatted with the specified <see cref="T:System.Drawing.StringFormat" />.</summary>
		/// <returns>This method returns a <see cref="T:System.Drawing.SizeF" /> structure that represents the size, in the units specified by the <see cref="P:System.Drawing.Graphics.PageUnit" /> property, of the string specified in the <paramref name="text" /> parameter as drawn with the <paramref name="font" /> parameter and the <paramref name="stringFormat" /> parameter.</returns>
		/// <param name="text">String to measure. </param>
		/// <param name="font">
		///   <see cref="T:System.Drawing.Font" /> defines the text format of the string. </param>
		/// <param name="layoutArea">
		///   <see cref="T:System.Drawing.SizeF" /> structure that specifies the maximum layout area for the text. </param>
		/// <param name="stringFormat">
		///   <see cref="T:System.Drawing.StringFormat" /> that represents formatting information, such as line spacing, for the string. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="font" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000460 RID: 1120 RVA: 0x0000BD40 File Offset: 0x00009F40
		public SizeF MeasureString(string text, Font font, SizeF layoutArea, StringFormat stringFormat)
		{
			RectangleF rectangleF = new RectangleF(0f, 0f, layoutArea.Width, layoutArea.Height);
			IntPtr intPtr = ((stringFormat == null) ? IntPtr.Zero : stringFormat.NativeObject);
			return this.GdipMeasureString(this.nativeObject, text, font, ref rectangleF, intPtr);
		}

		/// <summary>Measures the specified string when drawn with the specified <see cref="T:System.Drawing.Font" /> and formatted with the specified <see cref="T:System.Drawing.StringFormat" />.</summary>
		/// <returns>This method returns a <see cref="T:System.Drawing.SizeF" /> structure that represents the size, in the units specified by the <see cref="P:System.Drawing.Graphics.PageUnit" /> property, of the string specified in the <paramref name="text" /> parameter as drawn with the <paramref name="font" /> parameter and the <paramref name="stringFormat" /> parameter.</returns>
		/// <param name="text">String to measure. </param>
		/// <param name="font">
		///   <see cref="T:System.Drawing.Font" /> that defines the text format of the string. </param>
		/// <param name="width">Maximum width of the string. </param>
		/// <param name="format">
		///   <see cref="T:System.Drawing.StringFormat" /> that represents formatting information, such as line spacing, for the string. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="font" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000461 RID: 1121 RVA: 0x0000BD90 File Offset: 0x00009F90
		public SizeF MeasureString(string text, Font font, int width, StringFormat format)
		{
			RectangleF rectangleF = new RectangleF(0f, 0f, (float)width, 2.1474836E+09f);
			IntPtr intPtr = ((format == null) ? IntPtr.Zero : format.NativeObject);
			return this.GdipMeasureString(this.nativeObject, text, font, ref rectangleF, intPtr);
		}

		/// <summary>Measures the specified string when drawn with the specified <see cref="T:System.Drawing.Font" /> and formatted with the specified <see cref="T:System.Drawing.StringFormat" />.</summary>
		/// <returns>This method returns a <see cref="T:System.Drawing.SizeF" /> structure that represents the size, in the units specified by the <see cref="P:System.Drawing.Graphics.PageUnit" /> property, of the string specified by the <paramref name="text" /> parameter as drawn with the <paramref name="font" /> parameter and the <paramref name="stringFormat" /> parameter.</returns>
		/// <param name="text">String to measure. </param>
		/// <param name="font">
		///   <see cref="T:System.Drawing.Font" /> defines the text format of the string. </param>
		/// <param name="origin">
		///   <see cref="T:System.Drawing.PointF" /> structure that represents the upper-left corner of the string. </param>
		/// <param name="stringFormat">
		///   <see cref="T:System.Drawing.StringFormat" /> that represents formatting information, such as line spacing, for the string. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="font" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000462 RID: 1122 RVA: 0x0000BDDC File Offset: 0x00009FDC
		public SizeF MeasureString(string text, Font font, PointF origin, StringFormat stringFormat)
		{
			RectangleF rectangleF = new RectangleF(origin.X, origin.Y, 0f, 0f);
			IntPtr intPtr = ((stringFormat == null) ? IntPtr.Zero : stringFormat.NativeObject);
			return this.GdipMeasureString(this.nativeObject, text, font, ref rectangleF, intPtr);
		}

		/// <summary>Measures the specified string when drawn with the specified <see cref="T:System.Drawing.Font" /> and formatted with the specified <see cref="T:System.Drawing.StringFormat" />.</summary>
		/// <returns>This method returns a <see cref="T:System.Drawing.SizeF" /> structure that represents the size of the string, in the units specified by the <see cref="P:System.Drawing.Graphics.PageUnit" /> property, of the <paramref name="text" /> parameter as drawn with the <paramref name="font" /> parameter and the <paramref name="stringFormat" /> parameter.</returns>
		/// <param name="text">String to measure. </param>
		/// <param name="font">
		///   <see cref="T:System.Drawing.Font" /> that defines the text format of the string. </param>
		/// <param name="layoutArea">
		///   <see cref="T:System.Drawing.SizeF" /> structure that specifies the maximum layout area for the text. </param>
		/// <param name="stringFormat">
		///   <see cref="T:System.Drawing.StringFormat" /> that represents formatting information, such as line spacing, for the string. </param>
		/// <param name="charactersFitted">Number of characters in the string. </param>
		/// <param name="linesFilled">Number of text lines in the string. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="font" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000463 RID: 1123 RVA: 0x0000BE2C File Offset: 0x0000A02C
		public unsafe SizeF MeasureString(string text, Font font, SizeF layoutArea, StringFormat stringFormat, out int charactersFitted, out int linesFilled)
		{
			charactersFitted = 0;
			linesFilled = 0;
			if (text == null || text.Length == 0)
			{
				return SizeF.Empty;
			}
			if (font == null)
			{
				throw new ArgumentNullException("font");
			}
			RectangleF rectangleF = default(RectangleF);
			RectangleF rectangleF2 = new RectangleF(0f, 0f, layoutArea.Width, layoutArea.Height);
			IntPtr intPtr = ((stringFormat == null) ? IntPtr.Zero : stringFormat.NativeObject);
			fixed (int* ptr = &charactersFitted)
			{
				int* ptr2 = ptr;
				fixed (int* ptr3 = &linesFilled)
				{
					int* ptr4 = ptr3;
					GDIPlus.CheckStatus(GDIPlus.GdipMeasureString(this.nativeObject, text, text.Length, font.NativeObject, ref rectangleF2, intPtr, out rectangleF, ptr2, ptr4));
					ptr = null;
				}
				return new SizeF(rectangleF.Width, rectangleF.Height);
			}
		}

		/// <summary>Multiplies the world transformation of this <see cref="T:System.Drawing.Graphics" /> and specified the <see cref="T:System.Drawing.Drawing2D.Matrix" />.</summary>
		/// <param name="matrix">4x4 <see cref="T:System.Drawing.Drawing2D.Matrix" /> that multiplies the world transformation. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000464 RID: 1124 RVA: 0x0000BEEB File Offset: 0x0000A0EB
		public void MultiplyTransform(Matrix matrix)
		{
			this.MultiplyTransform(matrix, MatrixOrder.Prepend);
		}

		/// <summary>Multiplies the world transformation of this <see cref="T:System.Drawing.Graphics" /> and specified the <see cref="T:System.Drawing.Drawing2D.Matrix" /> in the specified order.</summary>
		/// <param name="matrix">4x4 <see cref="T:System.Drawing.Drawing2D.Matrix" /> that multiplies the world transformation. </param>
		/// <param name="order">Member of the <see cref="T:System.Drawing.Drawing2D.MatrixOrder" /> enumeration that determines the order of the multiplication. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000465 RID: 1125 RVA: 0x0000BEF5 File Offset: 0x0000A0F5
		public void MultiplyTransform(Matrix matrix, MatrixOrder order)
		{
			if (matrix == null)
			{
				throw new ArgumentNullException("matrix");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipMultiplyWorldTransform(this.nativeObject, matrix.nativeMatrix, order));
		}

		/// <summary>Releases a device context handle obtained by a previous call to the <see cref="M:System.Drawing.Graphics.GetHdc" /> method of this <see cref="T:System.Drawing.Graphics" />.</summary>
		/// <param name="hdc">Handle to a device context obtained by a previous call to the <see cref="M:System.Drawing.Graphics.GetHdc" /> method of this <see cref="T:System.Drawing.Graphics" />. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000466 RID: 1126 RVA: 0x0000BF1C File Offset: 0x0000A11C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public void ReleaseHdc(IntPtr hdc)
		{
			this.ReleaseHdcInternal(hdc);
		}

		/// <summary>Releases a device context handle obtained by a previous call to the <see cref="M:System.Drawing.Graphics.GetHdc" /> method of this <see cref="T:System.Drawing.Graphics" />.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000467 RID: 1127 RVA: 0x0000BF25 File Offset: 0x0000A125
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public void ReleaseHdc()
		{
			this.ReleaseHdcInternal(this.deviceContextHdc);
		}

		/// <summary>Releases a handle to a device context.</summary>
		/// <param name="hdc">Handle to a device context. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06000468 RID: 1128 RVA: 0x0000BF34 File Offset: 0x0000A134
		[MonoLimitation("Can only be used when hdc was provided by Graphics.GetHdc() method")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void ReleaseHdcInternal(IntPtr hdc)
		{
			Status status = Status.InvalidParameter;
			if (hdc == this.deviceContextHdc)
			{
				status = GDIPlus.GdipReleaseDC(this.nativeObject, this.deviceContextHdc);
				this.deviceContextHdc = IntPtr.Zero;
			}
			GDIPlus.CheckStatus(status);
		}

		/// <summary>Resets the clip region of this <see cref="T:System.Drawing.Graphics" /> to an infinite region.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000469 RID: 1129 RVA: 0x0000BF74 File Offset: 0x0000A174
		public void ResetClip()
		{
			GDIPlus.CheckStatus(GDIPlus.GdipResetClip(this.nativeObject));
		}

		/// <summary>Resets the world transformation matrix of this <see cref="T:System.Drawing.Graphics" /> to the identity matrix.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600046A RID: 1130 RVA: 0x0000BF86 File Offset: 0x0000A186
		public void ResetTransform()
		{
			GDIPlus.CheckStatus(GDIPlus.GdipResetWorldTransform(this.nativeObject));
		}

		/// <summary>Restores the state of this <see cref="T:System.Drawing.Graphics" /> to the state represented by a <see cref="T:System.Drawing.Drawing2D.GraphicsState" />.</summary>
		/// <param name="gstate">
		///   <see cref="T:System.Drawing.Drawing2D.GraphicsState" /> that represents the state to which to restore this <see cref="T:System.Drawing.Graphics" />. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600046B RID: 1131 RVA: 0x0000BF98 File Offset: 0x0000A198
		public void Restore(GraphicsState gstate)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipRestoreGraphics(this.nativeObject, (uint)gstate.nativeState));
		}

		/// <summary>Applies the specified rotation to the transformation matrix of this <see cref="T:System.Drawing.Graphics" />.</summary>
		/// <param name="angle">Angle of rotation in degrees. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600046C RID: 1132 RVA: 0x0000BFB0 File Offset: 0x0000A1B0
		public void RotateTransform(float angle)
		{
			this.RotateTransform(angle, MatrixOrder.Prepend);
		}

		/// <summary>Applies the specified rotation to the transformation matrix of this <see cref="T:System.Drawing.Graphics" /> in the specified order.</summary>
		/// <param name="angle">Angle of rotation in degrees. </param>
		/// <param name="order">Member of the <see cref="T:System.Drawing.Drawing2D.MatrixOrder" /> enumeration that specifies whether the rotation is appended or prepended to the matrix transformation. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600046D RID: 1133 RVA: 0x0000BFBA File Offset: 0x0000A1BA
		public void RotateTransform(float angle, MatrixOrder order)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipRotateWorldTransform(this.nativeObject, angle, order));
		}

		/// <summary>Saves the current state of this <see cref="T:System.Drawing.Graphics" /> and identifies the saved state with a <see cref="T:System.Drawing.Drawing2D.GraphicsState" />.</summary>
		/// <returns>This method returns a <see cref="T:System.Drawing.Drawing2D.GraphicsState" /> that represents the saved state of this <see cref="T:System.Drawing.Graphics" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600046E RID: 1134 RVA: 0x0000BFD0 File Offset: 0x0000A1D0
		public GraphicsState Save()
		{
			uint num;
			GDIPlus.CheckStatus(GDIPlus.GdipSaveGraphics(this.nativeObject, out num));
			return new GraphicsState((int)num);
		}

		/// <summary>Applies the specified scaling operation to the transformation matrix of this <see cref="T:System.Drawing.Graphics" /> by prepending it to the object's transformation matrix.</summary>
		/// <param name="sx">Scale factor in the x direction. </param>
		/// <param name="sy">Scale factor in the y direction. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600046F RID: 1135 RVA: 0x0000BFF5 File Offset: 0x0000A1F5
		public void ScaleTransform(float sx, float sy)
		{
			this.ScaleTransform(sx, sy, MatrixOrder.Prepend);
		}

		/// <summary>Applies the specified scaling operation to the transformation matrix of this <see cref="T:System.Drawing.Graphics" /> in the specified order.</summary>
		/// <param name="sx">Scale factor in the x direction. </param>
		/// <param name="sy">Scale factor in the y direction. </param>
		/// <param name="order">Member of the <see cref="T:System.Drawing.Drawing2D.MatrixOrder" /> enumeration that specifies whether the scaling operation is prepended or appended to the transformation matrix. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000470 RID: 1136 RVA: 0x0000C000 File Offset: 0x0000A200
		public void ScaleTransform(float sx, float sy, MatrixOrder order)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipScaleWorldTransform(this.nativeObject, sx, sy, order));
		}

		/// <summary>Sets the clipping region of this <see cref="T:System.Drawing.Graphics" /> to the rectangle specified by a <see cref="T:System.Drawing.RectangleF" /> structure.</summary>
		/// <param name="rect">
		///   <see cref="T:System.Drawing.RectangleF" /> structure that represents the new clip region. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000471 RID: 1137 RVA: 0x0000C015 File Offset: 0x0000A215
		public void SetClip(RectangleF rect)
		{
			this.SetClip(rect, CombineMode.Replace);
		}

		/// <summary>Sets the clipping region of this <see cref="T:System.Drawing.Graphics" /> to the specified <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</summary>
		/// <param name="path">
		///   <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> that represents the new clip region. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000472 RID: 1138 RVA: 0x0000C01F File Offset: 0x0000A21F
		public void SetClip(GraphicsPath path)
		{
			this.SetClip(path, CombineMode.Replace);
		}

		/// <summary>Sets the clipping region of this <see cref="T:System.Drawing.Graphics" /> to the rectangle specified by a <see cref="T:System.Drawing.Rectangle" /> structure.</summary>
		/// <param name="rect">
		///   <see cref="T:System.Drawing.Rectangle" /> structure that represents the new clip region. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000473 RID: 1139 RVA: 0x0000C029 File Offset: 0x0000A229
		public void SetClip(Rectangle rect)
		{
			this.SetClip(rect, CombineMode.Replace);
		}

		/// <summary>Sets the clipping region of this <see cref="T:System.Drawing.Graphics" /> to the Clip property of the specified <see cref="T:System.Drawing.Graphics" />.</summary>
		/// <param name="g">
		///   <see cref="T:System.Drawing.Graphics" /> from which to take the new clip region. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000474 RID: 1140 RVA: 0x0000C033 File Offset: 0x0000A233
		public void SetClip(Graphics g)
		{
			this.SetClip(g, CombineMode.Replace);
		}

		/// <summary>Sets the clipping region of this <see cref="T:System.Drawing.Graphics" /> to the result of the specified combining operation of the current clip region and the <see cref="P:System.Drawing.Graphics.Clip" /> property of the specified <see cref="T:System.Drawing.Graphics" />.</summary>
		/// <param name="g">
		///   <see cref="T:System.Drawing.Graphics" /> that specifies the clip region to combine. </param>
		/// <param name="combineMode">Member of the <see cref="T:System.Drawing.Drawing2D.CombineMode" /> enumeration that specifies the combining operation to use. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000475 RID: 1141 RVA: 0x0000C03D File Offset: 0x0000A23D
		public void SetClip(Graphics g, CombineMode combineMode)
		{
			if (g == null)
			{
				throw new ArgumentNullException("g");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipSetClipGraphics(this.nativeObject, g.NativeObject, combineMode));
		}

		/// <summary>Sets the clipping region of this <see cref="T:System.Drawing.Graphics" /> to the result of the specified operation combining the current clip region and the rectangle specified by a <see cref="T:System.Drawing.Rectangle" /> structure.</summary>
		/// <param name="rect">
		///   <see cref="T:System.Drawing.Rectangle" /> structure to combine. </param>
		/// <param name="combineMode">Member of the <see cref="T:System.Drawing.Drawing2D.CombineMode" /> enumeration that specifies the combining operation to use. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000476 RID: 1142 RVA: 0x0000C064 File Offset: 0x0000A264
		public void SetClip(Rectangle rect, CombineMode combineMode)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipSetClipRectI(this.nativeObject, rect.X, rect.Y, rect.Width, rect.Height, combineMode));
		}

		/// <summary>Sets the clipping region of this <see cref="T:System.Drawing.Graphics" /> to the result of the specified operation combining the current clip region and the rectangle specified by a <see cref="T:System.Drawing.RectangleF" /> structure.</summary>
		/// <param name="rect">
		///   <see cref="T:System.Drawing.RectangleF" /> structure to combine. </param>
		/// <param name="combineMode">Member of the <see cref="T:System.Drawing.Drawing2D.CombineMode" /> enumeration that specifies the combining operation to use. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000477 RID: 1143 RVA: 0x0000C093 File Offset: 0x0000A293
		public void SetClip(RectangleF rect, CombineMode combineMode)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipSetClipRect(this.nativeObject, rect.X, rect.Y, rect.Width, rect.Height, combineMode));
		}

		/// <summary>Sets the clipping region of this <see cref="T:System.Drawing.Graphics" /> to the result of the specified operation combining the current clip region and the specified <see cref="T:System.Drawing.Region" />.</summary>
		/// <param name="region">
		///   <see cref="T:System.Drawing.Region" /> to combine. </param>
		/// <param name="combineMode">Member from the <see cref="T:System.Drawing.Drawing2D.CombineMode" /> enumeration that specifies the combining operation to use. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000478 RID: 1144 RVA: 0x0000C0C2 File Offset: 0x0000A2C2
		public void SetClip(Region region, CombineMode combineMode)
		{
			if (region == null)
			{
				throw new ArgumentNullException("region");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipSetClipRegion(this.nativeObject, region.NativeObject, combineMode));
		}

		/// <summary>Sets the clipping region of this <see cref="T:System.Drawing.Graphics" /> to the result of the specified operation combining the current clip region and the specified <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</summary>
		/// <param name="path">
		///   <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> to combine. </param>
		/// <param name="combineMode">Member of the <see cref="T:System.Drawing.Drawing2D.CombineMode" /> enumeration that specifies the combining operation to use. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000479 RID: 1145 RVA: 0x0000C0E9 File Offset: 0x0000A2E9
		public void SetClip(GraphicsPath path, CombineMode combineMode)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipSetClipPath(this.nativeObject, path.nativePath, combineMode));
		}

		/// <summary>Transforms an array of points from one coordinate space to another using the current world and page transformations of this <see cref="T:System.Drawing.Graphics" />.</summary>
		/// <param name="destSpace">Member of the <see cref="T:System.Drawing.Drawing2D.CoordinateSpace" /> enumeration that specifies the destination coordinate space. </param>
		/// <param name="srcSpace">Member of the <see cref="T:System.Drawing.Drawing2D.CoordinateSpace" /> enumeration that specifies the source coordinate space. </param>
		/// <param name="pts">Array of <see cref="T:System.Drawing.PointF" /> structures that represent the points to transform. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600047A RID: 1146 RVA: 0x0000C110 File Offset: 0x0000A310
		public void TransformPoints(CoordinateSpace destSpace, CoordinateSpace srcSpace, PointF[] pts)
		{
			if (pts == null)
			{
				throw new ArgumentNullException("pts");
			}
			IntPtr intPtr = GDIPlus.FromPointToUnManagedMemory(pts);
			GDIPlus.CheckStatus(GDIPlus.GdipTransformPoints(this.nativeObject, destSpace, srcSpace, intPtr, pts.Length));
			GDIPlus.FromUnManagedMemoryToPoint(intPtr, pts);
		}

		/// <summary>Transforms an array of points from one coordinate space to another using the current world and page transformations of this <see cref="T:System.Drawing.Graphics" />.</summary>
		/// <param name="destSpace">Member of the <see cref="T:System.Drawing.Drawing2D.CoordinateSpace" /> enumeration that specifies the destination coordinate space. </param>
		/// <param name="srcSpace">Member of the <see cref="T:System.Drawing.Drawing2D.CoordinateSpace" /> enumeration that specifies the source coordinate space. </param>
		/// <param name="pts">Array of <see cref="T:System.Drawing.Point" /> structures that represents the points to transformation. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600047B RID: 1147 RVA: 0x0000C150 File Offset: 0x0000A350
		public void TransformPoints(CoordinateSpace destSpace, CoordinateSpace srcSpace, Point[] pts)
		{
			if (pts == null)
			{
				throw new ArgumentNullException("pts");
			}
			IntPtr intPtr = GDIPlus.FromPointToUnManagedMemoryI(pts);
			GDIPlus.CheckStatus(GDIPlus.GdipTransformPointsI(this.nativeObject, destSpace, srcSpace, intPtr, pts.Length));
			GDIPlus.FromUnManagedMemoryToPointI(intPtr, pts);
		}

		/// <summary>Translates the clipping region of this <see cref="T:System.Drawing.Graphics" /> by specified amounts in the horizontal and vertical directions.</summary>
		/// <param name="dx">The x-coordinate of the translation. </param>
		/// <param name="dy">The y-coordinate of the translation. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600047C RID: 1148 RVA: 0x0000C18F File Offset: 0x0000A38F
		public void TranslateClip(int dx, int dy)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipTranslateClipI(this.nativeObject, dx, dy));
		}

		/// <summary>Translates the clipping region of this <see cref="T:System.Drawing.Graphics" /> by specified amounts in the horizontal and vertical directions.</summary>
		/// <param name="dx">The x-coordinate of the translation. </param>
		/// <param name="dy">The y-coordinate of the translation. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600047D RID: 1149 RVA: 0x0000C1A3 File Offset: 0x0000A3A3
		public void TranslateClip(float dx, float dy)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipTranslateClip(this.nativeObject, dx, dy));
		}

		/// <summary>Changes the origin of the coordinate system by prepending the specified translation to the transformation matrix of this <see cref="T:System.Drawing.Graphics" />.</summary>
		/// <param name="dx">The x-coordinate of the translation. </param>
		/// <param name="dy">The y-coordinate of the translation. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600047E RID: 1150 RVA: 0x0000C1B7 File Offset: 0x0000A3B7
		public void TranslateTransform(float dx, float dy)
		{
			this.TranslateTransform(dx, dy, MatrixOrder.Prepend);
		}

		/// <summary>Changes the origin of the coordinate system by applying the specified translation to the transformation matrix of this <see cref="T:System.Drawing.Graphics" /> in the specified order.</summary>
		/// <param name="dx">The x-coordinate of the translation. </param>
		/// <param name="dy">The y-coordinate of the translation. </param>
		/// <param name="order">Member of the <see cref="T:System.Drawing.Drawing2D.MatrixOrder" /> enumeration that specifies whether the translation is prepended or appended to the transformation matrix. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600047F RID: 1151 RVA: 0x0000C1C2 File Offset: 0x0000A3C2
		public void TranslateTransform(float dx, float dy, MatrixOrder order)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipTranslateWorldTransform(this.nativeObject, dx, dy, order));
		}

		/// <summary>Gets or sets a <see cref="T:System.Drawing.Region" /> that limits the drawing region of this <see cref="T:System.Drawing.Graphics" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Region" /> that limits the portion of this <see cref="T:System.Drawing.Graphics" /> that is currently available for drawing.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x1700017A RID: 378
		// (get) Token: 0x06000480 RID: 1152 RVA: 0x0000C1D8 File Offset: 0x0000A3D8
		// (set) Token: 0x06000481 RID: 1153 RVA: 0x0000C202 File Offset: 0x0000A402
		public Region Clip
		{
			get
			{
				Region region = new Region();
				GDIPlus.CheckStatus(GDIPlus.GdipGetClip(this.nativeObject, region.NativeObject));
				return region;
			}
			set
			{
				this.SetClip(value, CombineMode.Replace);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.RectangleF" /> structure that bounds the clipping region of this <see cref="T:System.Drawing.Graphics" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.RectangleF" /> structure that represents a bounding rectangle for the clipping region of this <see cref="T:System.Drawing.Graphics" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x1700017B RID: 379
		// (get) Token: 0x06000482 RID: 1154 RVA: 0x0000C20C File Offset: 0x0000A40C
		public RectangleF ClipBounds
		{
			get
			{
				RectangleF rectangleF = default(RectangleF);
				GDIPlus.CheckStatus(GDIPlus.GdipGetClipBounds(this.nativeObject, out rectangleF));
				return rectangleF;
			}
		}

		/// <summary>Gets a value that specifies how composited images are drawn to this <see cref="T:System.Drawing.Graphics" />.</summary>
		/// <returns>This property specifies a member of the <see cref="T:System.Drawing.Drawing2D.CompositingMode" /> enumeration. The default is <see cref="F:System.Drawing.Drawing2D.CompositingMode.SourceOver" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x1700017C RID: 380
		// (get) Token: 0x06000483 RID: 1155 RVA: 0x0000C234 File Offset: 0x0000A434
		// (set) Token: 0x06000484 RID: 1156 RVA: 0x0000C254 File Offset: 0x0000A454
		public CompositingMode CompositingMode
		{
			get
			{
				CompositingMode compositingMode;
				GDIPlus.CheckStatus(GDIPlus.GdipGetCompositingMode(this.nativeObject, out compositingMode));
				return compositingMode;
			}
			set
			{
				GDIPlus.CheckStatus(GDIPlus.GdipSetCompositingMode(this.nativeObject, value));
			}
		}

		/// <summary>Gets or sets the rendering quality of composited images drawn to this <see cref="T:System.Drawing.Graphics" />.</summary>
		/// <returns>This property specifies a member of the <see cref="T:System.Drawing.Drawing2D.CompositingQuality" /> enumeration. The default is <see cref="F:System.Drawing.Drawing2D.CompositingQuality.Default" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x1700017D RID: 381
		// (get) Token: 0x06000485 RID: 1157 RVA: 0x0000C268 File Offset: 0x0000A468
		// (set) Token: 0x06000486 RID: 1158 RVA: 0x0000C288 File Offset: 0x0000A488
		public CompositingQuality CompositingQuality
		{
			get
			{
				CompositingQuality compositingQuality;
				GDIPlus.CheckStatus(GDIPlus.GdipGetCompositingQuality(this.nativeObject, out compositingQuality));
				return compositingQuality;
			}
			set
			{
				GDIPlus.CheckStatus(GDIPlus.GdipSetCompositingQuality(this.nativeObject, value));
			}
		}

		/// <summary>Gets the horizontal resolution of this <see cref="T:System.Drawing.Graphics" />.</summary>
		/// <returns>The value, in dots per inch, for the horizontal resolution supported by this <see cref="T:System.Drawing.Graphics" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x1700017E RID: 382
		// (get) Token: 0x06000487 RID: 1159 RVA: 0x0000C29C File Offset: 0x0000A49C
		public float DpiX
		{
			get
			{
				float num;
				GDIPlus.CheckStatus(GDIPlus.GdipGetDpiX(this.nativeObject, out num));
				return num;
			}
		}

		/// <summary>Gets the vertical resolution of this <see cref="T:System.Drawing.Graphics" />.</summary>
		/// <returns>The value, in dots per inch, for the vertical resolution supported by this <see cref="T:System.Drawing.Graphics" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x1700017F RID: 383
		// (get) Token: 0x06000488 RID: 1160 RVA: 0x0000C2BC File Offset: 0x0000A4BC
		public float DpiY
		{
			get
			{
				float num;
				GDIPlus.CheckStatus(GDIPlus.GdipGetDpiY(this.nativeObject, out num));
				return num;
			}
		}

		/// <summary>Gets or sets the interpolation mode associated with this <see cref="T:System.Drawing.Graphics" />.</summary>
		/// <returns>One of the <see cref="T:System.Drawing.Drawing2D.InterpolationMode" /> values.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000180 RID: 384
		// (get) Token: 0x06000489 RID: 1161 RVA: 0x0000C2DC File Offset: 0x0000A4DC
		// (set) Token: 0x0600048A RID: 1162 RVA: 0x0000C2FE File Offset: 0x0000A4FE
		public InterpolationMode InterpolationMode
		{
			get
			{
				InterpolationMode interpolationMode = InterpolationMode.Invalid;
				GDIPlus.CheckStatus(GDIPlus.GdipGetInterpolationMode(this.nativeObject, out interpolationMode));
				return interpolationMode;
			}
			set
			{
				GDIPlus.CheckStatus(GDIPlus.GdipSetInterpolationMode(this.nativeObject, value));
			}
		}

		/// <summary>Gets a value indicating whether the clipping region of this <see cref="T:System.Drawing.Graphics" /> is empty.</summary>
		/// <returns>true if the clipping region of this <see cref="T:System.Drawing.Graphics" /> is empty; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000181 RID: 385
		// (get) Token: 0x0600048B RID: 1163 RVA: 0x0000C314 File Offset: 0x0000A514
		public bool IsClipEmpty
		{
			get
			{
				bool flag = false;
				GDIPlus.CheckStatus(GDIPlus.GdipIsClipEmpty(this.nativeObject, out flag));
				return flag;
			}
		}

		/// <summary>Gets a value indicating whether the visible clipping region of this <see cref="T:System.Drawing.Graphics" /> is empty.</summary>
		/// <returns>true if the visible portion of the clipping region of this <see cref="T:System.Drawing.Graphics" /> is empty; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000182 RID: 386
		// (get) Token: 0x0600048C RID: 1164 RVA: 0x0000C338 File Offset: 0x0000A538
		public bool IsVisibleClipEmpty
		{
			get
			{
				bool flag = false;
				GDIPlus.CheckStatus(GDIPlus.GdipIsVisibleClipEmpty(this.nativeObject, out flag));
				return flag;
			}
		}

		/// <summary>Gets or sets the scaling between world units and page units for this <see cref="T:System.Drawing.Graphics" />.</summary>
		/// <returns>This property specifies a value for the scaling between world units and page units for this <see cref="T:System.Drawing.Graphics" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000183 RID: 387
		// (get) Token: 0x0600048D RID: 1165 RVA: 0x0000C35C File Offset: 0x0000A55C
		// (set) Token: 0x0600048E RID: 1166 RVA: 0x0000C37C File Offset: 0x0000A57C
		public float PageScale
		{
			get
			{
				float num;
				GDIPlus.CheckStatus(GDIPlus.GdipGetPageScale(this.nativeObject, out num));
				return num;
			}
			set
			{
				GDIPlus.CheckStatus(GDIPlus.GdipSetPageScale(this.nativeObject, value));
			}
		}

		/// <summary>Gets or sets the unit of measure used for page coordinates in this <see cref="T:System.Drawing.Graphics" />.</summary>
		/// <returns>One of the <see cref="T:System.Drawing.GraphicsUnit" /> values other than <see cref="F:System.Drawing.GraphicsUnit.World" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <see cref="P:System.Drawing.Graphics.PageUnit" /> is set to <see cref="F:System.Drawing.GraphicsUnit.World" />, which is not a physical unit. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000184 RID: 388
		// (get) Token: 0x0600048F RID: 1167 RVA: 0x0000C390 File Offset: 0x0000A590
		// (set) Token: 0x06000490 RID: 1168 RVA: 0x0000C3B0 File Offset: 0x0000A5B0
		public GraphicsUnit PageUnit
		{
			get
			{
				GraphicsUnit graphicsUnit;
				GDIPlus.CheckStatus(GDIPlus.GdipGetPageUnit(this.nativeObject, out graphicsUnit));
				return graphicsUnit;
			}
			set
			{
				GDIPlus.CheckStatus(GDIPlus.GdipSetPageUnit(this.nativeObject, value));
			}
		}

		/// <summary>Gets or set a value specifying how pixels are offset during rendering of this <see cref="T:System.Drawing.Graphics" />.</summary>
		/// <returns>This property specifies a member of the <see cref="T:System.Drawing.Drawing2D.PixelOffsetMode" /> enumeration </returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000185 RID: 389
		// (get) Token: 0x06000491 RID: 1169 RVA: 0x0000C3C4 File Offset: 0x0000A5C4
		// (set) Token: 0x06000492 RID: 1170 RVA: 0x0000C3E6 File Offset: 0x0000A5E6
		[MonoTODO("This property does not do anything when used with libgdiplus.")]
		public PixelOffsetMode PixelOffsetMode
		{
			get
			{
				PixelOffsetMode pixelOffsetMode = PixelOffsetMode.Invalid;
				GDIPlus.CheckStatus(GDIPlus.GdipGetPixelOffsetMode(this.nativeObject, out pixelOffsetMode));
				return pixelOffsetMode;
			}
			set
			{
				GDIPlus.CheckStatus(GDIPlus.GdipSetPixelOffsetMode(this.nativeObject, value));
			}
		}

		/// <summary>Gets or sets the rendering origin of this <see cref="T:System.Drawing.Graphics" /> for dithering and for hatch brushes.</summary>
		/// <returns>A <see cref="T:System.Drawing.Point" /> structure that represents the dither origin for 8-bits-per-pixel and 16-bits-per-pixel dithering and is also used to set the origin for hatch brushes.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000186 RID: 390
		// (get) Token: 0x06000493 RID: 1171 RVA: 0x0000C3FC File Offset: 0x0000A5FC
		// (set) Token: 0x06000494 RID: 1172 RVA: 0x0000C424 File Offset: 0x0000A624
		public Point RenderingOrigin
		{
			get
			{
				int num;
				int num2;
				GDIPlus.CheckStatus(GDIPlus.GdipGetRenderingOrigin(this.nativeObject, out num, out num2));
				return new Point(num, num2);
			}
			set
			{
				GDIPlus.CheckStatus(GDIPlus.GdipSetRenderingOrigin(this.nativeObject, value.X, value.Y));
			}
		}

		/// <summary>Gets or sets the rendering quality for this <see cref="T:System.Drawing.Graphics" />.</summary>
		/// <returns>One of the <see cref="T:System.Drawing.Drawing2D.SmoothingMode" /> values.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000187 RID: 391
		// (get) Token: 0x06000495 RID: 1173 RVA: 0x0000C444 File Offset: 0x0000A644
		// (set) Token: 0x06000496 RID: 1174 RVA: 0x0000C466 File Offset: 0x0000A666
		public SmoothingMode SmoothingMode
		{
			get
			{
				SmoothingMode smoothingMode = SmoothingMode.Invalid;
				GDIPlus.CheckStatus(GDIPlus.GdipGetSmoothingMode(this.nativeObject, out smoothingMode));
				return smoothingMode;
			}
			set
			{
				GDIPlus.CheckStatus(GDIPlus.GdipSetSmoothingMode(this.nativeObject, value));
			}
		}

		/// <summary>Gets or sets the gamma correction value for rendering text.</summary>
		/// <returns>The gamma correction value used for rendering antialiased and ClearType text.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000188 RID: 392
		// (get) Token: 0x06000497 RID: 1175 RVA: 0x0000C47C File Offset: 0x0000A67C
		// (set) Token: 0x06000498 RID: 1176 RVA: 0x0000C49C File Offset: 0x0000A69C
		[MonoTODO("This property does not do anything when used with libgdiplus.")]
		public int TextContrast
		{
			get
			{
				int num;
				GDIPlus.CheckStatus(GDIPlus.GdipGetTextContrast(this.nativeObject, out num));
				return num;
			}
			set
			{
				GDIPlus.CheckStatus(GDIPlus.GdipSetTextContrast(this.nativeObject, value));
			}
		}

		/// <summary>Gets or sets the rendering mode for text associated with this <see cref="T:System.Drawing.Graphics" />.</summary>
		/// <returns>One of the <see cref="T:System.Drawing.Text.TextRenderingHint" /> values.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000189 RID: 393
		// (get) Token: 0x06000499 RID: 1177 RVA: 0x0000C4B0 File Offset: 0x0000A6B0
		// (set) Token: 0x0600049A RID: 1178 RVA: 0x0000C4D0 File Offset: 0x0000A6D0
		public TextRenderingHint TextRenderingHint
		{
			get
			{
				TextRenderingHint textRenderingHint;
				GDIPlus.CheckStatus(GDIPlus.GdipGetTextRenderingHint(this.nativeObject, out textRenderingHint));
				return textRenderingHint;
			}
			set
			{
				GDIPlus.CheckStatus(GDIPlus.GdipSetTextRenderingHint(this.nativeObject, value));
			}
		}

		/// <summary>Gets or sets a copy of the geometric world transformation for this <see cref="T:System.Drawing.Graphics" />.</summary>
		/// <returns>A copy of the <see cref="T:System.Drawing.Drawing2D.Matrix" /> that represents the geometric world transformation for this <see cref="T:System.Drawing.Graphics" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x1700018A RID: 394
		// (get) Token: 0x0600049B RID: 1179 RVA: 0x0000C4E4 File Offset: 0x0000A6E4
		// (set) Token: 0x0600049C RID: 1180 RVA: 0x0000C50E File Offset: 0x0000A70E
		public Matrix Transform
		{
			get
			{
				Matrix matrix = new Matrix();
				GDIPlus.CheckStatus(GDIPlus.GdipGetWorldTransform(this.nativeObject, matrix.nativeMatrix));
				return matrix;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				GDIPlus.CheckStatus(GDIPlus.GdipSetWorldTransform(this.nativeObject, value.nativeMatrix));
			}
		}

		/// <summary>Gets the bounding rectangle of the visible clipping region of this <see cref="T:System.Drawing.Graphics" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.RectangleF" /> structure that represents a bounding rectangle for the visible clipping region of this <see cref="T:System.Drawing.Graphics" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Drawing.Printing.PrintingPermission, System.Drawing, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700018B RID: 395
		// (get) Token: 0x0600049D RID: 1181 RVA: 0x0000C534 File Offset: 0x0000A734
		public RectangleF VisibleClipBounds
		{
			get
			{
				RectangleF rectangleF;
				GDIPlus.CheckStatus(GDIPlus.GdipGetVisibleClipBounds(this.nativeObject, out rectangleF));
				return rectangleF;
			}
		}

		/// <summary>Gets the cumulative graphics context.</summary>
		/// <returns>An <see cref="T:System.Object" /> representing the cumulative graphics context.</returns>
		// Token: 0x0600049E RID: 1182 RVA: 0x00005902 File Offset: 0x00003B02
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MonoTODO]
		public object GetContextInfo()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x00003B8D File Offset: 0x00001D8D
		internal Graphics()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040003BC RID: 956
		internal IntPtr nativeObject;

		// Token: 0x040003BD RID: 957
		internal IMacContext maccontext;

		// Token: 0x040003BE RID: 958
		private bool disposed;

		// Token: 0x040003BF RID: 959
		private static float defDpiX;

		// Token: 0x040003C0 RID: 960
		private static float defDpiY;

		// Token: 0x040003C1 RID: 961
		private IntPtr deviceContextHdc;

		// Token: 0x040003C2 RID: 962
		private const string MetafileEnumeration = "Metafiles enumeration, for both WMF and EMF formats, isn't supported.";

		/// <summary>Provides a callback method for the <see cref="Overload:System.Drawing.Graphics.EnumerateMetafile" /> method.</summary>
		/// <returns>Return true if you want to continue enumerating records; otherwise, false.</returns>
		/// <param name="recordType">Member of the <see cref="T:System.Drawing.Imaging.EmfPlusRecordType" /> enumeration that specifies the type of metafile record. </param>
		/// <param name="flags">Set of flags that specify attributes of the record. </param>
		/// <param name="dataSize">Number of bytes in the record data. </param>
		/// <param name="data">Pointer to a buffer that contains the record data. </param>
		/// <param name="callbackData">Not used. </param>
		// Token: 0x02000066 RID: 102
		// (Invoke) Token: 0x060004A2 RID: 1186
		public delegate bool EnumerateMetafileProc(EmfPlusRecordType recordType, int flags, int dataSize, IntPtr data, PlayRecordCallback callbackData);

		/// <summary>Provides a callback method for deciding when the <see cref="Overload:System.Drawing.Graphics.DrawImage" /> method should prematurely cancel execution and stop drawing an image.</summary>
		/// <returns>This method returns true if it decides that the <see cref="Overload:System.Drawing.Graphics.DrawImage" /> method should prematurely stop execution. Otherwise it returns false to indicate that the <see cref="Overload:System.Drawing.Graphics.DrawImage" /> method should continue execution.</returns>
		/// <param name="callbackdata">Internal pointer that specifies data for the callback method. This parameter is not passed by all <see cref="Overload:System.Drawing.Graphics.DrawImage" /> overloads. You can test for its absence by checking for the value <see cref="F:System.IntPtr.Zero" />. </param>
		// Token: 0x02000067 RID: 103
		// (Invoke) Token: 0x060004A6 RID: 1190
		public delegate bool DrawImageAbort(IntPtr callbackdata);
	}
}
