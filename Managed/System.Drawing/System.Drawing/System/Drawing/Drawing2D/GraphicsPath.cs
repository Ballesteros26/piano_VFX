using System;
using System.ComponentModel;

namespace System.Drawing.Drawing2D
{
	/// <summary>Represents a series of connected lines and curves. This class cannot be inherited.</summary>
	// Token: 0x02000150 RID: 336
	public sealed class GraphicsPath : MarshalByRefObject, ICloneable, IDisposable
	{
		// Token: 0x06000E24 RID: 3620 RVA: 0x0001F0DA File Offset: 0x0001D2DA
		private GraphicsPath(IntPtr ptr)
		{
			this.nativePath = ptr;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> class with a <see cref="P:System.Drawing.Drawing2D.GraphicsPath.FillMode" /> value of <see cref="F:System.Drawing.Drawing2D.FillMode.Alternate" />.</summary>
		// Token: 0x06000E25 RID: 3621 RVA: 0x0001F0F4 File Offset: 0x0001D2F4
		public GraphicsPath()
		{
			GDIPlus.CheckStatus(GDIPlus.GdipCreatePath(FillMode.Alternate, out this.nativePath));
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> class with the specified <see cref="T:System.Drawing.Drawing2D.FillMode" /> enumeration.</summary>
		/// <param name="fillMode">The <see cref="T:System.Drawing.Drawing2D.FillMode" /> enumeration that determines how the interior of this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> is filled. </param>
		// Token: 0x06000E26 RID: 3622 RVA: 0x0001F118 File Offset: 0x0001D318
		public GraphicsPath(FillMode fillMode)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipCreatePath(fillMode, out this.nativePath));
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> class with the specified <see cref="T:System.Drawing.Drawing2D.PathPointType" /> and <see cref="T:System.Drawing.Point" /> arrays.</summary>
		/// <param name="pts">An array of <see cref="T:System.Drawing.Point" /> structures that defines the coordinates of the points that make up this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />. </param>
		/// <param name="types">An array of <see cref="T:System.Drawing.Drawing2D.PathPointType" /> enumeration elements that specifies the type of each corresponding point in the <paramref name="pts" /> array. </param>
		// Token: 0x06000E27 RID: 3623 RVA: 0x0001F13C File Offset: 0x0001D33C
		public GraphicsPath(Point[] pts, byte[] types)
			: this(pts, types, FillMode.Alternate)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> array with the specified <see cref="T:System.Drawing.Drawing2D.PathPointType" /> and <see cref="T:System.Drawing.PointF" /> arrays.</summary>
		/// <param name="pts">An array of <see cref="T:System.Drawing.PointF" /> structures that defines the coordinates of the points that make up this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />. </param>
		/// <param name="types">An array of <see cref="T:System.Drawing.Drawing2D.PathPointType" /> enumeration elements that specifies the type of each corresponding point in the <paramref name="pts" /> array. </param>
		// Token: 0x06000E28 RID: 3624 RVA: 0x0001F147 File Offset: 0x0001D347
		public GraphicsPath(PointF[] pts, byte[] types)
			: this(pts, types, FillMode.Alternate)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> class with the specified <see cref="T:System.Drawing.Drawing2D.PathPointType" /> and <see cref="T:System.Drawing.Point" /> arrays and with the specified <see cref="T:System.Drawing.Drawing2D.FillMode" /> enumeration element.</summary>
		/// <param name="pts">An array of <see cref="T:System.Drawing.Point" /> structures that defines the coordinates of the points that make up this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />. </param>
		/// <param name="types">An array of <see cref="T:System.Drawing.Drawing2D.PathPointType" /> enumeration elements that specifies the type of each corresponding point in the <paramref name="pts" /> array. </param>
		/// <param name="fillMode">A <see cref="T:System.Drawing.Drawing2D.FillMode" /> enumeration that specifies how the interiors of shapes in this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> are filled. </param>
		// Token: 0x06000E29 RID: 3625 RVA: 0x0001F154 File Offset: 0x0001D354
		public GraphicsPath(Point[] pts, byte[] types, FillMode fillMode)
		{
			if (pts == null)
			{
				throw new ArgumentNullException("pts");
			}
			if (pts.Length != types.Length)
			{
				throw new ArgumentException("Invalid parameter passed. Number of points and types must be same.");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipCreatePath2I(pts, types, pts.Length, fillMode, out this.nativePath));
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> array with the specified <see cref="T:System.Drawing.Drawing2D.PathPointType" /> and <see cref="T:System.Drawing.PointF" /> arrays and with the specified <see cref="T:System.Drawing.Drawing2D.FillMode" /> enumeration element.</summary>
		/// <param name="pts">An array of <see cref="T:System.Drawing.PointF" /> structures that defines the coordinates of the points that make up this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />. </param>
		/// <param name="types">An array of <see cref="T:System.Drawing.Drawing2D.PathPointType" /> enumeration elements that specifies the type of each corresponding point in the <paramref name="pts" /> array. </param>
		/// <param name="fillMode">A <see cref="T:System.Drawing.Drawing2D.FillMode" /> enumeration that specifies how the interiors of shapes in this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> are filled. </param>
		// Token: 0x06000E2A RID: 3626 RVA: 0x0001F1AC File Offset: 0x0001D3AC
		public GraphicsPath(PointF[] pts, byte[] types, FillMode fillMode)
		{
			if (pts == null)
			{
				throw new ArgumentNullException("pts");
			}
			if (pts.Length != types.Length)
			{
				throw new ArgumentException("Invalid parameter passed. Number of points and types must be same.");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipCreatePath2(pts, types, pts.Length, fillMode, out this.nativePath));
		}

		/// <summary>Creates an exact copy of this path.</summary>
		/// <returns>The <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> this method creates, cast as an object.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000E2B RID: 3627 RVA: 0x0001F204 File Offset: 0x0001D404
		public object Clone()
		{
			IntPtr intPtr;
			GDIPlus.CheckStatus(GDIPlus.GdipClonePath(this.nativePath, out intPtr));
			return new GraphicsPath(intPtr);
		}

		/// <summary>Releases all resources used by this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</summary>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000E2C RID: 3628 RVA: 0x0001F229 File Offset: 0x0001D429
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000E2D RID: 3629 RVA: 0x0001F238 File Offset: 0x0001D438
		~GraphicsPath()
		{
			this.Dispose(false);
		}

		// Token: 0x06000E2E RID: 3630 RVA: 0x0001F268 File Offset: 0x0001D468
		private void Dispose(bool disposing)
		{
			if (this.nativePath != IntPtr.Zero)
			{
				GDIPlus.CheckStatus(GDIPlus.GdipDeletePath(this.nativePath));
				this.nativePath = IntPtr.Zero;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Drawing.Drawing2D.FillMode" /> enumeration that determines how the interiors of shapes in this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> are filled.</summary>
		/// <returns>A <see cref="T:System.Drawing.Drawing2D.FillMode" /> enumeration that specifies how the interiors of shapes in this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> are filled.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170003CD RID: 973
		// (get) Token: 0x06000E2F RID: 3631 RVA: 0x0001F298 File Offset: 0x0001D498
		// (set) Token: 0x06000E30 RID: 3632 RVA: 0x0001F2B8 File Offset: 0x0001D4B8
		public FillMode FillMode
		{
			get
			{
				FillMode fillMode;
				GDIPlus.CheckStatus(GDIPlus.GdipGetPathFillMode(this.nativePath, out fillMode));
				return fillMode;
			}
			set
			{
				if (value < FillMode.Alternate || value > FillMode.Winding)
				{
					throw new InvalidEnumArgumentException("FillMode", (int)value, typeof(FillMode));
				}
				GDIPlus.CheckStatus(GDIPlus.GdipSetPathFillMode(this.nativePath, value));
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Drawing2D.PathData" /> that encapsulates arrays of points (<paramref name="points" />) and types (<paramref name="types" />) for this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Drawing2D.PathData" /> that encapsulates arrays for both the points and types for this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170003CE RID: 974
		// (get) Token: 0x06000E31 RID: 3633 RVA: 0x0001F2EC File Offset: 0x0001D4EC
		public PathData PathData
		{
			get
			{
				int num;
				GDIPlus.CheckStatus(GDIPlus.GdipGetPointCount(this.nativePath, out num));
				PointF[] array = new PointF[num];
				byte[] array2 = new byte[num];
				if (num > 0)
				{
					GDIPlus.CheckStatus(GDIPlus.GdipGetPathPoints(this.nativePath, array, num));
					GDIPlus.CheckStatus(GDIPlus.GdipGetPathTypes(this.nativePath, array2, num));
				}
				return new PathData
				{
					Points = array,
					Types = array2
				};
			}
		}

		/// <summary>Gets the points in the path.</summary>
		/// <returns>An array of <see cref="T:System.Drawing.PointF" /> objects that represent the path.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170003CF RID: 975
		// (get) Token: 0x06000E32 RID: 3634 RVA: 0x0001F354 File Offset: 0x0001D554
		public PointF[] PathPoints
		{
			get
			{
				int num;
				GDIPlus.CheckStatus(GDIPlus.GdipGetPointCount(this.nativePath, out num));
				if (num == 0)
				{
					throw new ArgumentException("PathPoints");
				}
				PointF[] array = new PointF[num];
				GDIPlus.CheckStatus(GDIPlus.GdipGetPathPoints(this.nativePath, array, num));
				return array;
			}
		}

		/// <summary>Gets the types of the corresponding points in the <see cref="P:System.Drawing.Drawing2D.GraphicsPath.PathPoints" /> array.</summary>
		/// <returns>An array of bytes that specifies the types of the corresponding points in the path.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x06000E33 RID: 3635 RVA: 0x0001F39C File Offset: 0x0001D59C
		public byte[] PathTypes
		{
			get
			{
				int num;
				GDIPlus.CheckStatus(GDIPlus.GdipGetPointCount(this.nativePath, out num));
				if (num == 0)
				{
					throw new ArgumentException("PathTypes");
				}
				byte[] array = new byte[num];
				GDIPlus.CheckStatus(GDIPlus.GdipGetPathTypes(this.nativePath, array, num));
				return array;
			}
		}

		/// <summary>Gets the number of elements in the <see cref="P:System.Drawing.Drawing2D.GraphicsPath.PathPoints" /> or the <see cref="P:System.Drawing.Drawing2D.GraphicsPath.PathTypes" /> array.</summary>
		/// <returns>An integer that specifies the number of elements in the <see cref="P:System.Drawing.Drawing2D.GraphicsPath.PathPoints" /> or the <see cref="P:System.Drawing.Drawing2D.GraphicsPath.PathTypes" /> array.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x06000E34 RID: 3636 RVA: 0x0001F3E4 File Offset: 0x0001D5E4
		public int PointCount
		{
			get
			{
				int num;
				GDIPlus.CheckStatus(GDIPlus.GdipGetPointCount(this.nativePath, out num));
				return num;
			}
		}

		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x06000E35 RID: 3637 RVA: 0x0001F404 File Offset: 0x0001D604
		// (set) Token: 0x06000E36 RID: 3638 RVA: 0x0001F40C File Offset: 0x0001D60C
		internal IntPtr NativeObject
		{
			get
			{
				return this.nativePath;
			}
			set
			{
				this.nativePath = value;
			}
		}

		/// <summary>Appends an elliptical arc to the current figure.</summary>
		/// <param name="rect">A <see cref="T:System.Drawing.Rectangle" /> that represents the rectangular bounds of the ellipse from which the arc is taken. </param>
		/// <param name="startAngle">The starting angle of the arc, measured in degrees clockwise from the x-axis. </param>
		/// <param name="sweepAngle">The angle between <paramref name="startAngle" /> and the end of the arc. </param>
		// Token: 0x06000E37 RID: 3639 RVA: 0x0001F415 File Offset: 0x0001D615
		public void AddArc(Rectangle rect, float startAngle, float sweepAngle)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipAddPathArcI(this.nativePath, rect.X, rect.Y, rect.Width, rect.Height, startAngle, sweepAngle));
		}

		/// <summary>Appends an elliptical arc to the current figure.</summary>
		/// <param name="rect">A <see cref="T:System.Drawing.RectangleF" /> that represents the rectangular bounds of the ellipse from which the arc is taken. </param>
		/// <param name="startAngle">The starting angle of the arc, measured in degrees clockwise from the x-axis. </param>
		/// <param name="sweepAngle">The angle between <paramref name="startAngle" /> and the end of the arc. </param>
		// Token: 0x06000E38 RID: 3640 RVA: 0x0001F445 File Offset: 0x0001D645
		public void AddArc(RectangleF rect, float startAngle, float sweepAngle)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipAddPathArc(this.nativePath, rect.X, rect.Y, rect.Width, rect.Height, startAngle, sweepAngle));
		}

		/// <summary>Appends an elliptical arc to the current figure.</summary>
		/// <param name="x">The x-coordinate of the upper-left corner of the rectangular region that defines the ellipse from which the arc is drawn. </param>
		/// <param name="y">The y-coordinate of the upper-left corner of the rectangular region that defines the ellipse from which the arc is drawn. </param>
		/// <param name="width">The width of the rectangular region that defines the ellipse from which the arc is drawn. </param>
		/// <param name="height">The height of the rectangular region that defines the ellipse from which the arc is drawn. </param>
		/// <param name="startAngle">The starting angle of the arc, measured in degrees clockwise from the x-axis. </param>
		/// <param name="sweepAngle">The angle between <paramref name="startAngle" /> and the end of the arc. </param>
		// Token: 0x06000E39 RID: 3641 RVA: 0x0001F475 File Offset: 0x0001D675
		public void AddArc(int x, int y, int width, int height, float startAngle, float sweepAngle)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipAddPathArcI(this.nativePath, x, y, width, height, startAngle, sweepAngle));
		}

		/// <summary>Appends an elliptical arc to the current figure.</summary>
		/// <param name="x">The x-coordinate of the upper-left corner of the rectangular region that defines the ellipse from which the arc is drawn. </param>
		/// <param name="y">The y-coordinate of the upper-left corner of the rectangular region that defines the ellipse from which the arc is drawn. </param>
		/// <param name="width">The width of the rectangular region that defines the ellipse from which the arc is drawn. </param>
		/// <param name="height">The height of the rectangular region that defines the ellipse from which the arc is drawn. </param>
		/// <param name="startAngle">The starting angle of the arc, measured in degrees clockwise from the x-axis. </param>
		/// <param name="sweepAngle">The angle between <paramref name="startAngle" /> and the end of the arc. </param>
		// Token: 0x06000E3A RID: 3642 RVA: 0x0001F490 File Offset: 0x0001D690
		public void AddArc(float x, float y, float width, float height, float startAngle, float sweepAngle)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipAddPathArc(this.nativePath, x, y, width, height, startAngle, sweepAngle));
		}

		/// <summary>Adds a cubic Bézier curve to the current figure.</summary>
		/// <param name="pt1">A <see cref="T:System.Drawing.Point" /> that represents the starting point of the curve. </param>
		/// <param name="pt2">A <see cref="T:System.Drawing.Point" /> that represents the first control point for the curve. </param>
		/// <param name="pt3">A <see cref="T:System.Drawing.Point" /> that represents the second control point for the curve. </param>
		/// <param name="pt4">A <see cref="T:System.Drawing.Point" /> that represents the endpoint of the curve. </param>
		// Token: 0x06000E3B RID: 3643 RVA: 0x0001F4AC File Offset: 0x0001D6AC
		public void AddBezier(Point pt1, Point pt2, Point pt3, Point pt4)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipAddPathBezierI(this.nativePath, pt1.X, pt1.Y, pt2.X, pt2.Y, pt3.X, pt3.Y, pt4.X, pt4.Y));
		}

		/// <summary>Adds a cubic Bézier curve to the current figure.</summary>
		/// <param name="pt1">A <see cref="T:System.Drawing.PointF" /> that represents the starting point of the curve. </param>
		/// <param name="pt2">A <see cref="T:System.Drawing.PointF" /> that represents the first control point for the curve. </param>
		/// <param name="pt3">A <see cref="T:System.Drawing.PointF" /> that represents the second control point for the curve. </param>
		/// <param name="pt4">A <see cref="T:System.Drawing.PointF" /> that represents the endpoint of the curve. </param>
		// Token: 0x06000E3C RID: 3644 RVA: 0x0001F504 File Offset: 0x0001D704
		public void AddBezier(PointF pt1, PointF pt2, PointF pt3, PointF pt4)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipAddPathBezier(this.nativePath, pt1.X, pt1.Y, pt2.X, pt2.Y, pt3.X, pt3.Y, pt4.X, pt4.Y));
		}

		/// <summary>Adds a cubic Bézier curve to the current figure.</summary>
		/// <param name="x1">The x-coordinate of the starting point of the curve. </param>
		/// <param name="y1">The y-coordinate of the starting point of the curve. </param>
		/// <param name="x2">The x-coordinate of the first control point for the curve. </param>
		/// <param name="y2">The y-coordinate of the first control point for the curve. </param>
		/// <param name="x3">The x-coordinate of the second control point for the curve. </param>
		/// <param name="y3">The y-coordinate of the second control point for the curve. </param>
		/// <param name="x4">The x-coordinate of the endpoint of the curve. </param>
		/// <param name="y4">The y-coordinate of the endpoint of the curve. </param>
		// Token: 0x06000E3D RID: 3645 RVA: 0x0001F55C File Offset: 0x0001D75C
		public void AddBezier(int x1, int y1, int x2, int y2, int x3, int y3, int x4, int y4)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipAddPathBezierI(this.nativePath, x1, y1, x2, y2, x3, y3, x4, y4));
		}

		/// <summary>Adds a cubic Bézier curve to the current figure.</summary>
		/// <param name="x1">The x-coordinate of the starting point of the curve. </param>
		/// <param name="y1">The y-coordinate of the starting point of the curve. </param>
		/// <param name="x2">The x-coordinate of the first control point for the curve. </param>
		/// <param name="y2">The y-coordinate of the first control point for the curve. </param>
		/// <param name="x3">The x-coordinate of the second control point for the curve. </param>
		/// <param name="y3">The y-coordinate of the second control point for the curve. </param>
		/// <param name="x4">The x-coordinate of the endpoint of the curve. </param>
		/// <param name="y4">The y-coordinate of the endpoint of the curve. </param>
		// Token: 0x06000E3E RID: 3646 RVA: 0x0001F588 File Offset: 0x0001D788
		public void AddBezier(float x1, float y1, float x2, float y2, float x3, float y3, float x4, float y4)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipAddPathBezier(this.nativePath, x1, y1, x2, y2, x3, y3, x4, y4));
		}

		/// <summary>Adds a sequence of connected cubic Bézier curves to the current figure.</summary>
		/// <param name="points">An array of <see cref="T:System.Drawing.Point" /> structures that represents the points that define the curves. </param>
		// Token: 0x06000E3F RID: 3647 RVA: 0x0001F5B2 File Offset: 0x0001D7B2
		public void AddBeziers(params Point[] points)
		{
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipAddPathBeziersI(this.nativePath, points, points.Length));
		}

		/// <summary>Adds a sequence of connected cubic Bézier curves to the current figure.</summary>
		/// <param name="points">An array of <see cref="T:System.Drawing.PointF" /> structures that represents the points that define the curves. </param>
		// Token: 0x06000E40 RID: 3648 RVA: 0x0001F5D6 File Offset: 0x0001D7D6
		public void AddBeziers(PointF[] points)
		{
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipAddPathBeziers(this.nativePath, points, points.Length));
		}

		/// <summary>Adds an ellipse to the current path.</summary>
		/// <param name="rect">A <see cref="T:System.Drawing.RectangleF" /> that represents the bounding rectangle that defines the ellipse. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000E41 RID: 3649 RVA: 0x0001F5FA File Offset: 0x0001D7FA
		public void AddEllipse(RectangleF rect)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipAddPathEllipse(this.nativePath, rect.X, rect.Y, rect.Width, rect.Height));
		}

		/// <summary>Adds an ellipse to the current path.</summary>
		/// <param name="x">The x-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse. </param>
		/// <param name="y">The y-coordinate of the upper left corner of the bounding rectangle that defines the ellipse. </param>
		/// <param name="width">The width of the bounding rectangle that defines the ellipse. </param>
		/// <param name="height">The height of the bounding rectangle that defines the ellipse. </param>
		// Token: 0x06000E42 RID: 3650 RVA: 0x0001F628 File Offset: 0x0001D828
		public void AddEllipse(float x, float y, float width, float height)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipAddPathEllipse(this.nativePath, x, y, width, height));
		}

		/// <summary>Adds an ellipse to the current path.</summary>
		/// <param name="rect">A <see cref="T:System.Drawing.Rectangle" /> that represents the bounding rectangle that defines the ellipse. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000E43 RID: 3651 RVA: 0x0001F63F File Offset: 0x0001D83F
		public void AddEllipse(Rectangle rect)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipAddPathEllipseI(this.nativePath, rect.X, rect.Y, rect.Width, rect.Height));
		}

		/// <summary>Adds an ellipse to the current path.</summary>
		/// <param name="x">The x-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse. </param>
		/// <param name="y">The y-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse. </param>
		/// <param name="width">The width of the bounding rectangle that defines the ellipse. </param>
		/// <param name="height">The height of the bounding rectangle that defines the ellipse. </param>
		// Token: 0x06000E44 RID: 3652 RVA: 0x0001F66D File Offset: 0x0001D86D
		public void AddEllipse(int x, int y, int width, int height)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipAddPathEllipseI(this.nativePath, x, y, width, height));
		}

		/// <summary>Appends a line segment to this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</summary>
		/// <param name="pt1">A <see cref="T:System.Drawing.Point" /> that represents the starting point of the line. </param>
		/// <param name="pt2">A <see cref="T:System.Drawing.Point" /> that represents the endpoint of the line. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000E45 RID: 3653 RVA: 0x0001F684 File Offset: 0x0001D884
		public void AddLine(Point pt1, Point pt2)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipAddPathLineI(this.nativePath, pt1.X, pt1.Y, pt2.X, pt2.Y));
		}

		/// <summary>Appends a line segment to this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</summary>
		/// <param name="pt1">A <see cref="T:System.Drawing.PointF" /> that represents the starting point of the line. </param>
		/// <param name="pt2">A <see cref="T:System.Drawing.PointF" /> that represents the endpoint of the line. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000E46 RID: 3654 RVA: 0x0001F6B2 File Offset: 0x0001D8B2
		public void AddLine(PointF pt1, PointF pt2)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipAddPathLine(this.nativePath, pt1.X, pt1.Y, pt2.X, pt2.Y));
		}

		/// <summary>Appends a line segment to the current figure.</summary>
		/// <param name="x1">The x-coordinate of the starting point of the line. </param>
		/// <param name="y1">The y-coordinate of the starting point of the line. </param>
		/// <param name="x2">The x-coordinate of the endpoint of the line. </param>
		/// <param name="y2">The y-coordinate of the endpoint of the line. </param>
		// Token: 0x06000E47 RID: 3655 RVA: 0x0001F6E0 File Offset: 0x0001D8E0
		public void AddLine(int x1, int y1, int x2, int y2)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipAddPathLineI(this.nativePath, x1, y1, x2, y2));
		}

		/// <summary>Appends a line segment to this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</summary>
		/// <param name="x1">The x-coordinate of the starting point of the line. </param>
		/// <param name="y1">The y-coordinate of the starting point of the line. </param>
		/// <param name="x2">The x-coordinate of the endpoint of the line. </param>
		/// <param name="y2">The y-coordinate of the endpoint of the line. </param>
		// Token: 0x06000E48 RID: 3656 RVA: 0x0001F6F7 File Offset: 0x0001D8F7
		public void AddLine(float x1, float y1, float x2, float y2)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipAddPathLine(this.nativePath, x1, y1, x2, y2));
		}

		/// <summary>Appends a series of connected line segments to the end of this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</summary>
		/// <param name="points">An array of <see cref="T:System.Drawing.Point" /> structures that represents the points that define the line segments to add. </param>
		// Token: 0x06000E49 RID: 3657 RVA: 0x0001F70E File Offset: 0x0001D90E
		public void AddLines(Point[] points)
		{
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			if (points.Length == 0)
			{
				throw new ArgumentException("points");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipAddPathLine2I(this.nativePath, points, points.Length));
		}

		/// <summary>Appends a series of connected line segments to the end of this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</summary>
		/// <param name="points">An array of <see cref="T:System.Drawing.PointF" /> structures that represents the points that define the line segments to add. </param>
		// Token: 0x06000E4A RID: 3658 RVA: 0x0001F741 File Offset: 0x0001D941
		public void AddLines(PointF[] points)
		{
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			if (points.Length == 0)
			{
				throw new ArgumentException("points");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipAddPathLine2(this.nativePath, points, points.Length));
		}

		/// <summary>Adds the outline of a pie shape to this path.</summary>
		/// <param name="rect">A <see cref="T:System.Drawing.Rectangle" /> that represents the bounding rectangle that defines the ellipse from which the pie is drawn. </param>
		/// <param name="startAngle">The starting angle for the pie section, measured in degrees clockwise from the x-axis. </param>
		/// <param name="sweepAngle">The angle between <paramref name="startAngle" /> and the end of the pie section, measured in degrees clockwise from <paramref name="startAngle" />. </param>
		// Token: 0x06000E4B RID: 3659 RVA: 0x0001F774 File Offset: 0x0001D974
		public void AddPie(Rectangle rect, float startAngle, float sweepAngle)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipAddPathPie(this.nativePath, (float)rect.X, (float)rect.Y, (float)rect.Width, (float)rect.Height, startAngle, sweepAngle));
		}

		/// <summary>Adds the outline of a pie shape to this path.</summary>
		/// <param name="x">The x-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse from which the pie is drawn. </param>
		/// <param name="y">The y-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse from which the pie is drawn. </param>
		/// <param name="width">The width of the bounding rectangle that defines the ellipse from which the pie is drawn. </param>
		/// <param name="height">The height of the bounding rectangle that defines the ellipse from which the pie is drawn. </param>
		/// <param name="startAngle">The starting angle for the pie section, measured in degrees clockwise from the x-axis. </param>
		/// <param name="sweepAngle">The angle between <paramref name="startAngle" /> and the end of the pie section, measured in degrees clockwise from <paramref name="startAngle" />. </param>
		// Token: 0x06000E4C RID: 3660 RVA: 0x0001F7A8 File Offset: 0x0001D9A8
		public void AddPie(int x, int y, int width, int height, float startAngle, float sweepAngle)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipAddPathPieI(this.nativePath, x, y, width, height, startAngle, sweepAngle));
		}

		/// <summary>Adds the outline of a pie shape to this path.</summary>
		/// <param name="x">The x-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse from which the pie is drawn. </param>
		/// <param name="y">The y-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse from which the pie is drawn. </param>
		/// <param name="width">The width of the bounding rectangle that defines the ellipse from which the pie is drawn. </param>
		/// <param name="height">The height of the bounding rectangle that defines the ellipse from which the pie is drawn. </param>
		/// <param name="startAngle">The starting angle for the pie section, measured in degrees clockwise from the x-axis. </param>
		/// <param name="sweepAngle">The angle between <paramref name="startAngle" /> and the end of the pie section, measured in degrees clockwise from <paramref name="startAngle" />. </param>
		// Token: 0x06000E4D RID: 3661 RVA: 0x0001F7C3 File Offset: 0x0001D9C3
		public void AddPie(float x, float y, float width, float height, float startAngle, float sweepAngle)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipAddPathPie(this.nativePath, x, y, width, height, startAngle, sweepAngle));
		}

		/// <summary>Adds a polygon to this path.</summary>
		/// <param name="points">An array of <see cref="T:System.Drawing.Point" /> structures that defines the polygon to add. </param>
		// Token: 0x06000E4E RID: 3662 RVA: 0x0001F7DE File Offset: 0x0001D9DE
		public void AddPolygon(Point[] points)
		{
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipAddPathPolygonI(this.nativePath, points, points.Length));
		}

		/// <summary>Adds a polygon to this path.</summary>
		/// <param name="points">An array of <see cref="T:System.Drawing.PointF" /> structures that defines the polygon to add. </param>
		// Token: 0x06000E4F RID: 3663 RVA: 0x0001F802 File Offset: 0x0001DA02
		public void AddPolygon(PointF[] points)
		{
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipAddPathPolygon(this.nativePath, points, points.Length));
		}

		/// <summary>Adds a rectangle to this path.</summary>
		/// <param name="rect">A <see cref="T:System.Drawing.Rectangle" /> that represents the rectangle to add. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000E50 RID: 3664 RVA: 0x0001F826 File Offset: 0x0001DA26
		public void AddRectangle(Rectangle rect)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipAddPathRectangleI(this.nativePath, rect.X, rect.Y, rect.Width, rect.Height));
		}

		/// <summary>Adds a rectangle to this path.</summary>
		/// <param name="rect">A <see cref="T:System.Drawing.RectangleF" /> that represents the rectangle to add. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000E51 RID: 3665 RVA: 0x0001F854 File Offset: 0x0001DA54
		public void AddRectangle(RectangleF rect)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipAddPathRectangle(this.nativePath, rect.X, rect.Y, rect.Width, rect.Height));
		}

		/// <summary>Adds a series of rectangles to this path.</summary>
		/// <param name="rects">An array of <see cref="T:System.Drawing.Rectangle" /> structures that represents the rectangles to add. </param>
		// Token: 0x06000E52 RID: 3666 RVA: 0x0001F882 File Offset: 0x0001DA82
		public void AddRectangles(Rectangle[] rects)
		{
			if (rects == null)
			{
				throw new ArgumentNullException("rects");
			}
			if (rects.Length == 0)
			{
				throw new ArgumentException("rects");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipAddPathRectanglesI(this.nativePath, rects, rects.Length));
		}

		/// <summary>Adds a series of rectangles to this path.</summary>
		/// <param name="rects">An array of <see cref="T:System.Drawing.RectangleF" /> structures that represents the rectangles to add. </param>
		// Token: 0x06000E53 RID: 3667 RVA: 0x0001F8B5 File Offset: 0x0001DAB5
		public void AddRectangles(RectangleF[] rects)
		{
			if (rects == null)
			{
				throw new ArgumentNullException("rects");
			}
			if (rects.Length == 0)
			{
				throw new ArgumentException("rects");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipAddPathRectangles(this.nativePath, rects, rects.Length));
		}

		/// <summary>Appends the specified <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> to this path.</summary>
		/// <param name="addingPath">The <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> to add. </param>
		/// <param name="connect">A Boolean value that specifies whether the first figure in the added path is part of the last figure in this path. A value of true specifies that (if possible) the first figure in the added path is part of the last figure in this path. A value of false specifies that the first figure in the added path is separate from the last figure in this path. </param>
		// Token: 0x06000E54 RID: 3668 RVA: 0x0001F8E8 File Offset: 0x0001DAE8
		public void AddPath(GraphicsPath addingPath, bool connect)
		{
			if (addingPath == null)
			{
				throw new ArgumentNullException("addingPath");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipAddPathPath(this.nativePath, addingPath.nativePath, connect));
		}

		/// <summary>Gets the last point in the <see cref="P:System.Drawing.Drawing2D.GraphicsPath.PathPoints" /> array of this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.PointF" /> that represents the last point in this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000E55 RID: 3669 RVA: 0x0001F910 File Offset: 0x0001DB10
		public PointF GetLastPoint()
		{
			PointF pointF;
			GDIPlus.CheckStatus(GDIPlus.GdipGetPathLastPoint(this.nativePath, out pointF));
			return pointF;
		}

		/// <summary>Adds a closed curve to this path. A cardinal spline curve is used because the curve travels through each of the points in the array.</summary>
		/// <param name="points">An array of <see cref="T:System.Drawing.Point" /> structures that represents the points that define the curve. </param>
		// Token: 0x06000E56 RID: 3670 RVA: 0x0001F930 File Offset: 0x0001DB30
		public void AddClosedCurve(Point[] points)
		{
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipAddPathClosedCurveI(this.nativePath, points, points.Length));
		}

		/// <summary>Adds a closed curve to this path. A cardinal spline curve is used because the curve travels through each of the points in the array.</summary>
		/// <param name="points">An array of <see cref="T:System.Drawing.PointF" /> structures that represents the points that define the curve. </param>
		// Token: 0x06000E57 RID: 3671 RVA: 0x0001F954 File Offset: 0x0001DB54
		public void AddClosedCurve(PointF[] points)
		{
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipAddPathClosedCurve(this.nativePath, points, points.Length));
		}

		/// <summary>Adds a closed curve to this path. A cardinal spline curve is used because the curve travels through each of the points in the array.</summary>
		/// <param name="points">An array of <see cref="T:System.Drawing.Point" /> structures that represents the points that define the curve. </param>
		/// <param name="tension">A value between from 0 through 1 that specifies the amount that the curve bends between points, with 0 being the smallest curve (sharpest corner) and 1 being the smoothest curve. </param>
		// Token: 0x06000E58 RID: 3672 RVA: 0x0001F978 File Offset: 0x0001DB78
		public void AddClosedCurve(Point[] points, float tension)
		{
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipAddPathClosedCurve2I(this.nativePath, points, points.Length, tension));
		}

		/// <summary>Adds a closed curve to this path. A cardinal spline curve is used because the curve travels through each of the points in the array.</summary>
		/// <param name="points">An array of <see cref="T:System.Drawing.PointF" /> structures that represents the points that define the curve. </param>
		/// <param name="tension">A value between from 0 through 1 that specifies the amount that the curve bends between points, with 0 being the smallest curve (sharpest corner) and 1 being the smoothest curve. </param>
		// Token: 0x06000E59 RID: 3673 RVA: 0x0001F99D File Offset: 0x0001DB9D
		public void AddClosedCurve(PointF[] points, float tension)
		{
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipAddPathClosedCurve2(this.nativePath, points, points.Length, tension));
		}

		/// <summary>Adds a spline curve to the current figure. A cardinal spline curve is used because the curve travels through each of the points in the array.</summary>
		/// <param name="points">An array of <see cref="T:System.Drawing.Point" /> structures that represents the points that define the curve. </param>
		// Token: 0x06000E5A RID: 3674 RVA: 0x0001F9C2 File Offset: 0x0001DBC2
		public void AddCurve(Point[] points)
		{
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipAddPathCurveI(this.nativePath, points, points.Length));
		}

		/// <summary>Adds a spline curve to the current figure. A cardinal spline curve is used because the curve travels through each of the points in the array.</summary>
		/// <param name="points">An array of <see cref="T:System.Drawing.PointF" /> structures that represents the points that define the curve. </param>
		// Token: 0x06000E5B RID: 3675 RVA: 0x0001F9E6 File Offset: 0x0001DBE6
		public void AddCurve(PointF[] points)
		{
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipAddPathCurve(this.nativePath, points, points.Length));
		}

		/// <summary>Adds a spline curve to the current figure.</summary>
		/// <param name="points">An array of <see cref="T:System.Drawing.Point" /> structures that represents the points that define the curve. </param>
		/// <param name="tension">A value that specifies the amount that the curve bends between control points. Values greater than 1 produce unpredictable results. </param>
		// Token: 0x06000E5C RID: 3676 RVA: 0x0001FA0A File Offset: 0x0001DC0A
		public void AddCurve(Point[] points, float tension)
		{
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipAddPathCurve2I(this.nativePath, points, points.Length, tension));
		}

		/// <summary>Adds a spline curve to the current figure.</summary>
		/// <param name="points">An array of <see cref="T:System.Drawing.PointF" /> structures that represents the points that define the curve. </param>
		/// <param name="tension">A value that specifies the amount that the curve bends between control points. Values greater than 1 produce unpredictable results. </param>
		// Token: 0x06000E5D RID: 3677 RVA: 0x0001FA2F File Offset: 0x0001DC2F
		public void AddCurve(PointF[] points, float tension)
		{
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipAddPathCurve2(this.nativePath, points, points.Length, tension));
		}

		/// <summary>Adds a spline curve to the current figure.</summary>
		/// <param name="points">An array of <see cref="T:System.Drawing.Point" /> structures that represents the points that define the curve. </param>
		/// <param name="offset">The index of the element in the <paramref name="points" /> array that is used as the first point in the curve. </param>
		/// <param name="numberOfSegments">A value that specifies the amount that the curve bends between control points. Values greater than 1 produce unpredictable results. </param>
		/// <param name="tension">A value that specifies the amount that the curve bends between control points. Values greater than 1 produce unpredictable results. </param>
		// Token: 0x06000E5E RID: 3678 RVA: 0x0001FA54 File Offset: 0x0001DC54
		public void AddCurve(Point[] points, int offset, int numberOfSegments, float tension)
		{
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipAddPathCurve3I(this.nativePath, points, points.Length, offset, numberOfSegments, tension));
		}

		/// <summary>Adds a spline curve to the current figure.</summary>
		/// <param name="points">An array of <see cref="T:System.Drawing.PointF" /> structures that represents the points that define the curve. </param>
		/// <param name="offset">The index of the element in the <paramref name="points" /> array that is used as the first point in the curve. </param>
		/// <param name="numberOfSegments">The number of segments used to draw the curve. A segment can be thought of as a line connecting two points. </param>
		/// <param name="tension">A value that specifies the amount that the curve bends between control points. Values greater than 1 produce unpredictable results. </param>
		// Token: 0x06000E5F RID: 3679 RVA: 0x0001FA7C File Offset: 0x0001DC7C
		public void AddCurve(PointF[] points, int offset, int numberOfSegments, float tension)
		{
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipAddPathCurve3(this.nativePath, points, points.Length, offset, numberOfSegments, tension));
		}

		/// <summary>Empties the <see cref="P:System.Drawing.Drawing2D.GraphicsPath.PathPoints" /> and <see cref="P:System.Drawing.Drawing2D.GraphicsPath.PathTypes" /> arrays and sets the <see cref="T:System.Drawing.Drawing2D.FillMode" /> to <see cref="F:System.Drawing.Drawing2D.FillMode.Alternate" />.</summary>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000E60 RID: 3680 RVA: 0x0001FAA4 File Offset: 0x0001DCA4
		public void Reset()
		{
			GDIPlus.CheckStatus(GDIPlus.GdipResetPath(this.nativePath));
		}

		/// <summary>Reverses the order of points in the <see cref="P:System.Drawing.Drawing2D.GraphicsPath.PathPoints" /> array of this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</summary>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000E61 RID: 3681 RVA: 0x0001FAB6 File Offset: 0x0001DCB6
		public void Reverse()
		{
			GDIPlus.CheckStatus(GDIPlus.GdipReversePath(this.nativePath));
		}

		/// <summary>Applies a transform matrix to this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</summary>
		/// <param name="matrix">A <see cref="T:System.Drawing.Drawing2D.Matrix" /> that represents the transformation to apply. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000E62 RID: 3682 RVA: 0x0001FAC8 File Offset: 0x0001DCC8
		public void Transform(Matrix matrix)
		{
			if (matrix == null)
			{
				throw new ArgumentNullException("matrix");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipTransformPath(this.nativePath, matrix.nativeMatrix));
		}

		/// <summary>Adds a text string to this path.</summary>
		/// <param name="s">The <see cref="T:System.String" /> to add. </param>
		/// <param name="family">A <see cref="T:System.Drawing.FontFamily" /> that represents the name of the font with which the test is drawn. </param>
		/// <param name="style">A <see cref="T:System.Drawing.FontStyle" /> enumeration that represents style information about the text (bold, italic, and so on). This must be cast as an integer (see the example code later in this section). </param>
		/// <param name="emSize">The height of the em square box that bounds the character. </param>
		/// <param name="origin">A <see cref="T:System.Drawing.Point" /> that represents the point where the text starts. </param>
		/// <param name="format">A <see cref="T:System.Drawing.StringFormat" /> that specifies text formatting information, such as line spacing and alignment. </param>
		// Token: 0x06000E63 RID: 3683 RVA: 0x0001FAF0 File Offset: 0x0001DCF0
		[MonoTODO("The StringFormat parameter is ignored when using libgdiplus.")]
		public void AddString(string s, FontFamily family, int style, float emSize, Point origin, StringFormat format)
		{
			this.AddString(s, family, style, emSize, new Rectangle
			{
				X = origin.X,
				Y = origin.Y
			}, format);
		}

		/// <summary>Adds a text string to this path.</summary>
		/// <param name="s">The <see cref="T:System.String" /> to add. </param>
		/// <param name="family">A <see cref="T:System.Drawing.FontFamily" /> that represents the name of the font with which the test is drawn. </param>
		/// <param name="style">A <see cref="T:System.Drawing.FontStyle" /> enumeration that represents style information about the text (bold, italic, and so on). This must be cast as an integer (see the example code later in this section). </param>
		/// <param name="emSize">The height of the em square box that bounds the character. </param>
		/// <param name="origin">A <see cref="T:System.Drawing.PointF" /> that represents the point where the text starts. </param>
		/// <param name="format">A <see cref="T:System.Drawing.StringFormat" /> that specifies text formatting information, such as line spacing and alignment. </param>
		// Token: 0x06000E64 RID: 3684 RVA: 0x0001FB30 File Offset: 0x0001DD30
		[MonoTODO("The StringFormat parameter is ignored when using libgdiplus.")]
		public void AddString(string s, FontFamily family, int style, float emSize, PointF origin, StringFormat format)
		{
			this.AddString(s, family, style, emSize, new RectangleF
			{
				X = origin.X,
				Y = origin.Y
			}, format);
		}

		/// <summary>Adds a text string to this path.</summary>
		/// <param name="s">The <see cref="T:System.String" /> to add. </param>
		/// <param name="family">A <see cref="T:System.Drawing.FontFamily" /> that represents the name of the font with which the test is drawn. </param>
		/// <param name="style">A <see cref="T:System.Drawing.FontStyle" /> enumeration that represents style information about the text (bold, italic, and so on). This must be cast as an integer (see the example code later in this section). </param>
		/// <param name="emSize">The height of the em square box that bounds the character. </param>
		/// <param name="layoutRect">A <see cref="T:System.Drawing.Rectangle" /> that represents the rectangle that bounds the text. </param>
		/// <param name="format">A <see cref="T:System.Drawing.StringFormat" /> that specifies text formatting information, such as line spacing and alignment. </param>
		// Token: 0x06000E65 RID: 3685 RVA: 0x0001FB70 File Offset: 0x0001DD70
		[MonoTODO("The layoutRect and StringFormat parameters are ignored when using libgdiplus.")]
		public void AddString(string s, FontFamily family, int style, float emSize, Rectangle layoutRect, StringFormat format)
		{
			if (family == null)
			{
				throw new ArgumentException("family");
			}
			IntPtr intPtr = ((format == null) ? IntPtr.Zero : format.NativeObject);
			GDIPlus.CheckStatus(GDIPlus.GdipAddPathStringI(this.nativePath, s, s.Length, family.NativeFamily, style, emSize, ref layoutRect, intPtr));
		}

		/// <summary>Adds a text string to this path.</summary>
		/// <param name="s">The <see cref="T:System.String" /> to add. </param>
		/// <param name="family">A <see cref="T:System.Drawing.FontFamily" /> that represents the name of the font with which the test is drawn. </param>
		/// <param name="style">A <see cref="T:System.Drawing.FontStyle" /> enumeration that represents style information about the text (bold, italic, and so on). This must be cast as an integer (see the example code later in this section). </param>
		/// <param name="emSize">The height of the em square box that bounds the character. </param>
		/// <param name="layoutRect">A <see cref="T:System.Drawing.RectangleF" /> that represents the rectangle that bounds the text. </param>
		/// <param name="format">A <see cref="T:System.Drawing.StringFormat" /> that specifies text formatting information, such as line spacing and alignment. </param>
		// Token: 0x06000E66 RID: 3686 RVA: 0x0001FBC4 File Offset: 0x0001DDC4
		[MonoTODO("The layoutRect and StringFormat parameters are ignored when using libgdiplus.")]
		public void AddString(string s, FontFamily family, int style, float emSize, RectangleF layoutRect, StringFormat format)
		{
			if (family == null)
			{
				throw new ArgumentException("family");
			}
			IntPtr intPtr = ((format == null) ? IntPtr.Zero : format.NativeObject);
			GDIPlus.CheckStatus(GDIPlus.GdipAddPathString(this.nativePath, s, s.Length, family.NativeFamily, style, emSize, ref layoutRect, intPtr));
		}

		/// <summary>Clears all markers from this path.</summary>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000E67 RID: 3687 RVA: 0x0001FC15 File Offset: 0x0001DE15
		public void ClearMarkers()
		{
			GDIPlus.CheckStatus(GDIPlus.GdipClearPathMarkers(this.nativePath));
		}

		/// <summary>Closes all open figures in this path and starts a new figure. It closes each open figure by connecting a line from its endpoint to its starting point.</summary>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000E68 RID: 3688 RVA: 0x0001FC27 File Offset: 0x0001DE27
		public void CloseAllFigures()
		{
			GDIPlus.CheckStatus(GDIPlus.GdipClosePathFigures(this.nativePath));
		}

		/// <summary>Closes the current figure and starts a new figure. If the current figure contains a sequence of connected lines and curves, the method closes the loop by connecting a line from the endpoint to the starting point.</summary>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000E69 RID: 3689 RVA: 0x0001FC39 File Offset: 0x0001DE39
		public void CloseFigure()
		{
			GDIPlus.CheckStatus(GDIPlus.GdipClosePathFigure(this.nativePath));
		}

		/// <summary>Converts each curve in this path into a sequence of connected line segments.</summary>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000E6A RID: 3690 RVA: 0x0001FC4B File Offset: 0x0001DE4B
		public void Flatten()
		{
			this.Flatten(null, 0.25f);
		}

		/// <summary>Applies the specified transform and then converts each curve in this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> into a sequence of connected line segments.</summary>
		/// <param name="matrix">A <see cref="T:System.Drawing.Drawing2D.Matrix" /> by which to transform this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> before flattening. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000E6B RID: 3691 RVA: 0x0001FC59 File Offset: 0x0001DE59
		public void Flatten(Matrix matrix)
		{
			this.Flatten(matrix, 0.25f);
		}

		/// <summary>Converts each curve in this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> into a sequence of connected line segments.</summary>
		/// <param name="matrix">A <see cref="T:System.Drawing.Drawing2D.Matrix" /> by which to transform this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> before flattening. </param>
		/// <param name="flatness">Specifies the maximum permitted error between the curve and its flattened approximation. A value of 0.25 is the default. Reducing the flatness value will increase the number of line segments in the approximation. </param>
		// Token: 0x06000E6C RID: 3692 RVA: 0x0001FC68 File Offset: 0x0001DE68
		public void Flatten(Matrix matrix, float flatness)
		{
			IntPtr intPtr = ((matrix == null) ? IntPtr.Zero : matrix.nativeMatrix);
			GDIPlus.CheckStatus(GDIPlus.GdipFlattenPath(this.nativePath, intPtr, flatness));
		}

		/// <summary>Returns a rectangle that bounds this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.RectangleF" /> that represents a rectangle that bounds this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000E6D RID: 3693 RVA: 0x0001FC98 File Offset: 0x0001DE98
		public RectangleF GetBounds()
		{
			return this.GetBounds(null, null);
		}

		/// <summary>Returns a rectangle that bounds this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> when this path is transformed by the specified <see cref="T:System.Drawing.Drawing2D.Matrix" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.RectangleF" /> that represents a rectangle that bounds this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</returns>
		/// <param name="matrix">The <see cref="T:System.Drawing.Drawing2D.Matrix" /> that specifies a transformation to be applied to this path before the bounding rectangle is calculated. This path is not permanently transformed; the transformation is used only during the process of calculating the bounding rectangle. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000E6E RID: 3694 RVA: 0x0001FCA2 File Offset: 0x0001DEA2
		public RectangleF GetBounds(Matrix matrix)
		{
			return this.GetBounds(matrix, null);
		}

		/// <summary>Returns a rectangle that bounds this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> when the current path is transformed by the specified <see cref="T:System.Drawing.Drawing2D.Matrix" /> and drawn with the specified <see cref="T:System.Drawing.Pen" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.RectangleF" /> that represents a rectangle that bounds this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</returns>
		/// <param name="matrix">The <see cref="T:System.Drawing.Drawing2D.Matrix" /> that specifies a transformation to be applied to this path before the bounding rectangle is calculated. This path is not permanently transformed; the transformation is used only during the process of calculating the bounding rectangle. </param>
		/// <param name="pen">The <see cref="T:System.Drawing.Pen" /> with which to draw the <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />. </param>
		// Token: 0x06000E6F RID: 3695 RVA: 0x0001FCAC File Offset: 0x0001DEAC
		public RectangleF GetBounds(Matrix matrix, Pen pen)
		{
			IntPtr intPtr = ((matrix == null) ? IntPtr.Zero : matrix.nativeMatrix);
			IntPtr intPtr2 = ((pen == null) ? IntPtr.Zero : pen.NativePen);
			RectangleF rectangleF;
			GDIPlus.CheckStatus(GDIPlus.GdipGetPathWorldBounds(this.nativePath, out rectangleF, intPtr, intPtr2));
			return rectangleF;
		}

		/// <summary>Indicates whether the specified point is contained within (under) the outline of this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> when drawn with the specified <see cref="T:System.Drawing.Pen" />.</summary>
		/// <returns>This method returns true if the specified point is contained within the outline of this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> when drawn with the specified <see cref="T:System.Drawing.Pen" />; otherwise, false.</returns>
		/// <param name="point">A <see cref="T:System.Drawing.Point" /> that specifies the location to test. </param>
		/// <param name="pen">The <see cref="T:System.Drawing.Pen" /> to test. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000E70 RID: 3696 RVA: 0x0001FCF0 File Offset: 0x0001DEF0
		public bool IsOutlineVisible(Point point, Pen pen)
		{
			return this.IsOutlineVisible(point.X, point.Y, pen, null);
		}

		/// <summary>Indicates whether the specified point is contained within (under) the outline of this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> when drawn with the specified <see cref="T:System.Drawing.Pen" />.</summary>
		/// <returns>This method returns true if the specified point is contained within the outline of this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> when drawn with the specified <see cref="T:System.Drawing.Pen" />; otherwise, false.</returns>
		/// <param name="point">A <see cref="T:System.Drawing.PointF" /> that specifies the location to test. </param>
		/// <param name="pen">The <see cref="T:System.Drawing.Pen" /> to test. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000E71 RID: 3697 RVA: 0x0001FD08 File Offset: 0x0001DF08
		public bool IsOutlineVisible(PointF point, Pen pen)
		{
			return this.IsOutlineVisible(point.X, point.Y, pen, null);
		}

		/// <summary>Indicates whether the specified point is contained within (under) the outline of this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> when drawn with the specified <see cref="T:System.Drawing.Pen" />.</summary>
		/// <returns>This method returns true if the specified point is contained within the outline of this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> when drawn with the specified <see cref="T:System.Drawing.Pen" />; otherwise, false.</returns>
		/// <param name="x">The x-coordinate of the point to test. </param>
		/// <param name="y">The y-coordinate of the point to test. </param>
		/// <param name="pen">The <see cref="T:System.Drawing.Pen" /> to test. </param>
		// Token: 0x06000E72 RID: 3698 RVA: 0x0001FD20 File Offset: 0x0001DF20
		public bool IsOutlineVisible(int x, int y, Pen pen)
		{
			return this.IsOutlineVisible(x, y, pen, null);
		}

		/// <summary>Indicates whether the specified point is contained within (under) the outline of this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> when drawn with the specified <see cref="T:System.Drawing.Pen" />.</summary>
		/// <returns>This method returns true if the specified point is contained within the outline of this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> when drawn with the specified <see cref="T:System.Drawing.Pen" />; otherwise, false.</returns>
		/// <param name="x">The x-coordinate of the point to test. </param>
		/// <param name="y">The y-coordinate of the point to test. </param>
		/// <param name="pen">The <see cref="T:System.Drawing.Pen" /> to test. </param>
		// Token: 0x06000E73 RID: 3699 RVA: 0x0001FD2C File Offset: 0x0001DF2C
		public bool IsOutlineVisible(float x, float y, Pen pen)
		{
			return this.IsOutlineVisible(x, y, pen, null);
		}

		/// <summary>Indicates whether the specified point is contained within (under) the outline of this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> when drawn with the specified <see cref="T:System.Drawing.Pen" /> and using the specified <see cref="T:System.Drawing.Graphics" />.</summary>
		/// <returns>This method returns true if the specified point is contained within the outline of this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> as drawn with the specified <see cref="T:System.Drawing.Pen" />; otherwise, false.</returns>
		/// <param name="pt">A <see cref="T:System.Drawing.Point" /> that specifies the location to test. </param>
		/// <param name="pen">The <see cref="T:System.Drawing.Pen" /> to test. </param>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> for which to test visibility. </param>
		// Token: 0x06000E74 RID: 3700 RVA: 0x0001FD38 File Offset: 0x0001DF38
		public bool IsOutlineVisible(Point pt, Pen pen, Graphics graphics)
		{
			return this.IsOutlineVisible(pt.X, pt.Y, pen, graphics);
		}

		/// <summary>Indicates whether the specified point is contained within (under) the outline of this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> when drawn with the specified <see cref="T:System.Drawing.Pen" /> and using the specified <see cref="T:System.Drawing.Graphics" />.</summary>
		/// <returns>This method returns true if the specified point is contained within (under) the outline of this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> as drawn with the specified <see cref="T:System.Drawing.Pen" />; otherwise, false.</returns>
		/// <param name="pt">A <see cref="T:System.Drawing.PointF" /> that specifies the location to test. </param>
		/// <param name="pen">The <see cref="T:System.Drawing.Pen" /> to test. </param>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> for which to test visibility. </param>
		// Token: 0x06000E75 RID: 3701 RVA: 0x0001FD50 File Offset: 0x0001DF50
		public bool IsOutlineVisible(PointF pt, Pen pen, Graphics graphics)
		{
			return this.IsOutlineVisible(pt.X, pt.Y, pen, graphics);
		}

		/// <summary>Indicates whether the specified point is contained within (under) the outline of this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> when drawn with the specified <see cref="T:System.Drawing.Pen" /> and using the specified <see cref="T:System.Drawing.Graphics" />.</summary>
		/// <returns>This method returns true if the specified point is contained within the outline of this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> as drawn with the specified <see cref="T:System.Drawing.Pen" />; otherwise, false.</returns>
		/// <param name="x">The x-coordinate of the point to test. </param>
		/// <param name="y">The y-coordinate of the point to test. </param>
		/// <param name="pen">The <see cref="T:System.Drawing.Pen" /> to test. </param>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> for which to test visibility. </param>
		// Token: 0x06000E76 RID: 3702 RVA: 0x0001FD68 File Offset: 0x0001DF68
		public bool IsOutlineVisible(int x, int y, Pen pen, Graphics graphics)
		{
			if (pen == null)
			{
				throw new ArgumentNullException("pen");
			}
			IntPtr intPtr = ((graphics == null) ? IntPtr.Zero : graphics.nativeObject);
			bool flag;
			GDIPlus.CheckStatus(GDIPlus.GdipIsOutlineVisiblePathPointI(this.nativePath, x, y, pen.NativePen, intPtr, out flag));
			return flag;
		}

		/// <summary>Indicates whether the specified point is contained within (under) the outline of this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> when drawn with the specified <see cref="T:System.Drawing.Pen" /> and using the specified <see cref="T:System.Drawing.Graphics" />.</summary>
		/// <returns>This method returns true if the specified point is contained within (under) the outline of this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> as drawn with the specified <see cref="T:System.Drawing.Pen" />; otherwise, false.</returns>
		/// <param name="x">The x-coordinate of the point to test. </param>
		/// <param name="y">The y-coordinate of the point to test. </param>
		/// <param name="pen">The <see cref="T:System.Drawing.Pen" /> to test. </param>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> for which to test visibility. </param>
		// Token: 0x06000E77 RID: 3703 RVA: 0x0001FDB4 File Offset: 0x0001DFB4
		public bool IsOutlineVisible(float x, float y, Pen pen, Graphics graphics)
		{
			if (pen == null)
			{
				throw new ArgumentNullException("pen");
			}
			IntPtr intPtr = ((graphics == null) ? IntPtr.Zero : graphics.nativeObject);
			bool flag;
			GDIPlus.CheckStatus(GDIPlus.GdipIsOutlineVisiblePathPoint(this.nativePath, x, y, pen.NativePen, intPtr, out flag));
			return flag;
		}

		/// <summary>Indicates whether the specified point is contained within this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</summary>
		/// <returns>This method returns true if the specified point is contained within this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />; otherwise, false.</returns>
		/// <param name="point">A <see cref="T:System.Drawing.Point" /> that represents the point to test. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000E78 RID: 3704 RVA: 0x0001FDFE File Offset: 0x0001DFFE
		public bool IsVisible(Point point)
		{
			return this.IsVisible(point.X, point.Y, null);
		}

		/// <summary>Indicates whether the specified point is contained within this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</summary>
		/// <returns>This method returns true if the specified point is contained within this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />; otherwise, false.</returns>
		/// <param name="point">A <see cref="T:System.Drawing.PointF" /> that represents the point to test. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000E79 RID: 3705 RVA: 0x0001FE15 File Offset: 0x0001E015
		public bool IsVisible(PointF point)
		{
			return this.IsVisible(point.X, point.Y, null);
		}

		/// <summary>Indicates whether the specified point is contained within this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</summary>
		/// <returns>This method returns true if the specified point is contained within this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />; otherwise, false.</returns>
		/// <param name="x">The x-coordinate of the point to test. </param>
		/// <param name="y">The y-coordinate of the point to test. </param>
		// Token: 0x06000E7A RID: 3706 RVA: 0x0001FE2C File Offset: 0x0001E02C
		public bool IsVisible(int x, int y)
		{
			return this.IsVisible(x, y, null);
		}

		/// <summary>Indicates whether the specified point is contained within this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</summary>
		/// <returns>This method returns true if the specified point is contained within this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />; otherwise, false.</returns>
		/// <param name="x">The x-coordinate of the point to test. </param>
		/// <param name="y">The y-coordinate of the point to test. </param>
		// Token: 0x06000E7B RID: 3707 RVA: 0x0001FE37 File Offset: 0x0001E037
		public bool IsVisible(float x, float y)
		{
			return this.IsVisible(x, y, null);
		}

		/// <summary>Indicates whether the specified point is contained within this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</summary>
		/// <returns>This method returns true if the specified point is contained within this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />; otherwise, false.</returns>
		/// <param name="pt">A <see cref="T:System.Drawing.Point" /> that represents the point to test. </param>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> for which to test visibility. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000E7C RID: 3708 RVA: 0x0001FE42 File Offset: 0x0001E042
		public bool IsVisible(Point pt, Graphics graphics)
		{
			return this.IsVisible(pt.X, pt.Y, graphics);
		}

		/// <summary>Indicates whether the specified point is contained within this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</summary>
		/// <returns>This method returns true if the specified point is contained within this; otherwise, false.</returns>
		/// <param name="pt">A <see cref="T:System.Drawing.PointF" /> that represents the point to test. </param>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> for which to test visibility. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000E7D RID: 3709 RVA: 0x0001FE59 File Offset: 0x0001E059
		public bool IsVisible(PointF pt, Graphics graphics)
		{
			return this.IsVisible(pt.X, pt.Y, graphics);
		}

		/// <summary>Indicates whether the specified point is contained within this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />, using the specified <see cref="T:System.Drawing.Graphics" />.</summary>
		/// <returns>This method returns true if the specified point is contained within this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />; otherwise, false.</returns>
		/// <param name="x">The x-coordinate of the point to test. </param>
		/// <param name="y">The y-coordinate of the point to test. </param>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> for which to test visibility. </param>
		// Token: 0x06000E7E RID: 3710 RVA: 0x0001FE70 File Offset: 0x0001E070
		public bool IsVisible(int x, int y, Graphics graphics)
		{
			IntPtr intPtr = ((graphics == null) ? IntPtr.Zero : graphics.nativeObject);
			bool flag;
			GDIPlus.CheckStatus(GDIPlus.GdipIsVisiblePathPointI(this.nativePath, x, y, intPtr, out flag));
			return flag;
		}

		/// <summary>Indicates whether the specified point is contained within this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> in the visible clip region of the specified <see cref="T:System.Drawing.Graphics" />.</summary>
		/// <returns>This method returns true if the specified point is contained within this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />; otherwise, false.</returns>
		/// <param name="x">The x-coordinate of the point to test. </param>
		/// <param name="y">The y-coordinate of the point to test. </param>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> for which to test visibility. </param>
		// Token: 0x06000E7F RID: 3711 RVA: 0x0001FEA4 File Offset: 0x0001E0A4
		public bool IsVisible(float x, float y, Graphics graphics)
		{
			IntPtr intPtr = ((graphics == null) ? IntPtr.Zero : graphics.nativeObject);
			bool flag;
			GDIPlus.CheckStatus(GDIPlus.GdipIsVisiblePathPoint(this.nativePath, x, y, intPtr, out flag));
			return flag;
		}

		/// <summary>Sets a marker on this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</summary>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000E80 RID: 3712 RVA: 0x0001FED8 File Offset: 0x0001E0D8
		public void SetMarkers()
		{
			GDIPlus.CheckStatus(GDIPlus.GdipSetPathMarker(this.nativePath));
		}

		/// <summary>Starts a new figure without closing the current figure. All subsequent points added to the path are added to this new figure.</summary>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000E81 RID: 3713 RVA: 0x0001FEEA File Offset: 0x0001E0EA
		public void StartFigure()
		{
			GDIPlus.CheckStatus(GDIPlus.GdipStartPathFigure(this.nativePath));
		}

		/// <summary>Applies a warp transform, defined by a rectangle and a parallelogram, to this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</summary>
		/// <param name="destPoints">An array of <see cref="T:System.Drawing.PointF" /> structures that define a parallelogram to which the rectangle defined by <paramref name="srcRect" /> is transformed. The array can contain either three or four elements. If the array contains three elements, the lower-right corner of the parallelogram is implied by the first three points. </param>
		/// <param name="srcRect">A <see cref="T:System.Drawing.RectangleF" /> that represents the rectangle that is transformed to the parallelogram defined by <paramref name="destPoints" />. </param>
		// Token: 0x06000E82 RID: 3714 RVA: 0x0001FEFC File Offset: 0x0001E0FC
		[MonoTODO("GdipWarpPath isn't implemented in libgdiplus")]
		public void Warp(PointF[] destPoints, RectangleF srcRect)
		{
			this.Warp(destPoints, srcRect, null, WarpMode.Perspective, 0.25f);
		}

		/// <summary>Applies a warp transform, defined by a rectangle and a parallelogram, to this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</summary>
		/// <param name="destPoints">An array of <see cref="T:System.Drawing.PointF" /> structures that define a parallelogram to which the rectangle defined by <paramref name="srcRect" /> is transformed. The array can contain either three or four elements. If the array contains three elements, the lower-right corner of the parallelogram is implied by the first three points. </param>
		/// <param name="srcRect">A <see cref="T:System.Drawing.RectangleF" /> that represents the rectangle that is transformed to the parallelogram defined by <paramref name="destPoints" />. </param>
		/// <param name="matrix">A <see cref="T:System.Drawing.Drawing2D.Matrix" /> that specifies a geometric transform to apply to the path. </param>
		// Token: 0x06000E83 RID: 3715 RVA: 0x0001FF0D File Offset: 0x0001E10D
		[MonoTODO("GdipWarpPath isn't implemented in libgdiplus")]
		public void Warp(PointF[] destPoints, RectangleF srcRect, Matrix matrix)
		{
			this.Warp(destPoints, srcRect, matrix, WarpMode.Perspective, 0.25f);
		}

		/// <summary>Applies a warp transform, defined by a rectangle and a parallelogram, to this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</summary>
		/// <param name="destPoints">An array of <see cref="T:System.Drawing.PointF" /> structures that defines a parallelogram to which the rectangle defined by <paramref name="srcRect" /> is transformed. The array can contain either three or four elements. If the array contains three elements, the lower-right corner of the parallelogram is implied by the first three points. </param>
		/// <param name="srcRect">A <see cref="T:System.Drawing.RectangleF" /> that represents the rectangle that is transformed to the parallelogram defined by <paramref name="destPoints" />. </param>
		/// <param name="matrix">A <see cref="T:System.Drawing.Drawing2D.Matrix" /> that specifies a geometric transform to apply to the path. </param>
		/// <param name="warpMode">A <see cref="T:System.Drawing.Drawing2D.WarpMode" /> enumeration that specifies whether this warp operation uses perspective or bilinear mode. </param>
		// Token: 0x06000E84 RID: 3716 RVA: 0x0001FF1E File Offset: 0x0001E11E
		[MonoTODO("GdipWarpPath isn't implemented in libgdiplus")]
		public void Warp(PointF[] destPoints, RectangleF srcRect, Matrix matrix, WarpMode warpMode)
		{
			this.Warp(destPoints, srcRect, matrix, warpMode, 0.25f);
		}

		/// <summary>Applies a warp transform, defined by a rectangle and a parallelogram, to this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</summary>
		/// <param name="destPoints">An array of <see cref="T:System.Drawing.PointF" /> structures that define a parallelogram to which the rectangle defined by <paramref name="srcRect" /> is transformed. The array can contain either three or four elements. If the array contains three elements, the lower-right corner of the parallelogram is implied by the first three points. </param>
		/// <param name="srcRect">A <see cref="T:System.Drawing.RectangleF" /> that represents the rectangle that is transformed to the parallelogram defined by <paramref name="destPoints" />. </param>
		/// <param name="matrix">A <see cref="T:System.Drawing.Drawing2D.Matrix" /> that specifies a geometric transform to apply to the path. </param>
		/// <param name="warpMode">A <see cref="T:System.Drawing.Drawing2D.WarpMode" /> enumeration that specifies whether this warp operation uses perspective or bilinear mode. </param>
		/// <param name="flatness">A value from 0 through 1 that specifies how flat the resulting path is. For more information, see the <see cref="M:System.Drawing.Drawing2D.GraphicsPath.Flatten" /> methods. </param>
		// Token: 0x06000E85 RID: 3717 RVA: 0x0001FF30 File Offset: 0x0001E130
		[MonoTODO("GdipWarpPath isn't implemented in libgdiplus")]
		public void Warp(PointF[] destPoints, RectangleF srcRect, Matrix matrix, WarpMode warpMode, float flatness)
		{
			if (destPoints == null)
			{
				throw new ArgumentNullException("destPoints");
			}
			IntPtr intPtr = ((matrix == null) ? IntPtr.Zero : matrix.nativeMatrix);
			GDIPlus.CheckStatus(GDIPlus.GdipWarpPath(this.nativePath, intPtr, destPoints, destPoints.Length, srcRect.X, srcRect.Y, srcRect.Width, srcRect.Height, warpMode, flatness));
		}

		/// <summary>Adds an additional outline to the path.</summary>
		/// <param name="pen">A <see cref="T:System.Drawing.Pen" /> that specifies the width between the original outline of the path and the new outline this method creates. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000E86 RID: 3718 RVA: 0x0001FF91 File Offset: 0x0001E191
		[MonoTODO("GdipWidenPath isn't implemented in libgdiplus")]
		public void Widen(Pen pen)
		{
			this.Widen(pen, null, 0.25f);
		}

		/// <summary>Adds an additional outline to the <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</summary>
		/// <param name="pen">A <see cref="T:System.Drawing.Pen" /> that specifies the width between the original outline of the path and the new outline this method creates. </param>
		/// <param name="matrix">A <see cref="T:System.Drawing.Drawing2D.Matrix" /> that specifies a transform to apply to the path before widening. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000E87 RID: 3719 RVA: 0x0001FFA0 File Offset: 0x0001E1A0
		[MonoTODO("GdipWidenPath isn't implemented in libgdiplus")]
		public void Widen(Pen pen, Matrix matrix)
		{
			this.Widen(pen, matrix, 0.25f);
		}

		/// <summary>Replaces this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> with curves that enclose the area that is filled when this path is drawn by the specified pen.</summary>
		/// <param name="pen">A <see cref="T:System.Drawing.Pen" /> that specifies the width between the original outline of the path and the new outline this method creates. </param>
		/// <param name="matrix">A <see cref="T:System.Drawing.Drawing2D.Matrix" /> that specifies a transform to apply to the path before widening. </param>
		/// <param name="flatness">A value that specifies the flatness for curves. </param>
		// Token: 0x06000E88 RID: 3720 RVA: 0x0001FFB0 File Offset: 0x0001E1B0
		[MonoTODO("GdipWidenPath isn't implemented in libgdiplus")]
		public void Widen(Pen pen, Matrix matrix, float flatness)
		{
			if (pen == null)
			{
				throw new ArgumentNullException("pen");
			}
			if (this.PointCount == 0)
			{
				return;
			}
			IntPtr intPtr = ((matrix == null) ? IntPtr.Zero : matrix.nativeMatrix);
			GDIPlus.CheckStatus(GDIPlus.GdipWidenPath(this.nativePath, pen.NativePen, intPtr, flatness));
		}

		// Token: 0x04000B56 RID: 2902
		private const float FlatnessDefault = 0.25f;

		// Token: 0x04000B57 RID: 2903
		internal IntPtr nativePath = IntPtr.Zero;
	}
}
