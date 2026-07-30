using System;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Drawing
{
	/// <summary>Represents an ordered pair of integer x- and y-coordinates that defines a point in a two-dimensional plane.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200007C RID: 124
	[ComVisible(true)]
	[TypeConverter(typeof(PointConverter))]
	[Serializable]
	public struct Point
	{
		/// <summary>Converts the specified <see cref="T:System.Drawing.PointF" /> to a <see cref="T:System.Drawing.Point" /> by rounding the values of the <see cref="T:System.Drawing.PointF" /> to the next higher integer values.</summary>
		/// <returns>The <see cref="T:System.Drawing.Point" /> this method converts to.</returns>
		/// <param name="value">The <see cref="T:System.Drawing.PointF" /> to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060005FD RID: 1533 RVA: 0x00011D34 File Offset: 0x0000FF34
		public static Point Ceiling(PointF value)
		{
			checked
			{
				int num = (int)Math.Ceiling((double)value.X);
				int num2 = (int)Math.Ceiling((double)value.Y);
				return new Point(num, num2);
			}
		}

		/// <summary>Converts the specified <see cref="T:System.Drawing.PointF" /> to a <see cref="T:System.Drawing.Point" /> object by rounding the <see cref="T:System.Drawing.Point" /> values to the nearest integer.</summary>
		/// <returns>The <see cref="T:System.Drawing.Point" /> this method converts to.</returns>
		/// <param name="value">The <see cref="T:System.Drawing.PointF" /> to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060005FE RID: 1534 RVA: 0x00011D64 File Offset: 0x0000FF64
		public static Point Round(PointF value)
		{
			checked
			{
				int num = (int)Math.Round((double)value.X);
				int num2 = (int)Math.Round((double)value.Y);
				return new Point(num, num2);
			}
		}

		/// <summary>Converts the specified <see cref="T:System.Drawing.PointF" /> to a <see cref="T:System.Drawing.Point" /> by truncating the values of the <see cref="T:System.Drawing.Point" />.</summary>
		/// <returns>The <see cref="T:System.Drawing.Point" /> this method converts to.</returns>
		/// <param name="value">The <see cref="T:System.Drawing.PointF" /> to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060005FF RID: 1535 RVA: 0x00011D94 File Offset: 0x0000FF94
		public static Point Truncate(PointF value)
		{
			checked
			{
				int num = (int)value.X;
				int num2 = (int)value.Y;
				return new Point(num, num2);
			}
		}

		/// <summary>Translates a <see cref="T:System.Drawing.Point" /> by a given <see cref="T:System.Drawing.Size" />.</summary>
		/// <returns>The translated <see cref="T:System.Drawing.Point" />.</returns>
		/// <param name="pt">The <see cref="T:System.Drawing.Point" /> to translate. </param>
		/// <param name="sz">A <see cref="T:System.Drawing.Size" /> that specifies the pair of numbers to add to the coordinates of <paramref name="pt" />. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06000600 RID: 1536 RVA: 0x00011DB8 File Offset: 0x0000FFB8
		public static Point operator +(Point pt, Size sz)
		{
			return new Point(pt.X + sz.Width, pt.Y + sz.Height);
		}

		/// <summary>Compares two <see cref="T:System.Drawing.Point" /> objects. The result specifies whether the values of the <see cref="P:System.Drawing.Point.X" /> and <see cref="P:System.Drawing.Point.Y" /> properties of the two <see cref="T:System.Drawing.Point" /> objects are equal.</summary>
		/// <returns>true if the <see cref="P:System.Drawing.Point.X" /> and <see cref="P:System.Drawing.Point.Y" /> values of <paramref name="left" /> and <paramref name="right" /> are equal; otherwise, false.</returns>
		/// <param name="left">A <see cref="T:System.Drawing.Point" /> to compare. </param>
		/// <param name="right">A <see cref="T:System.Drawing.Point" /> to compare. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06000601 RID: 1537 RVA: 0x00011DDD File Offset: 0x0000FFDD
		public static bool operator ==(Point left, Point right)
		{
			return left.X == right.X && left.Y == right.Y;
		}

		/// <summary>Compares two <see cref="T:System.Drawing.Point" /> objects. The result specifies whether the values of the <see cref="P:System.Drawing.Point.X" /> or <see cref="P:System.Drawing.Point.Y" /> properties of the two <see cref="T:System.Drawing.Point" /> objects are unequal.</summary>
		/// <returns>true if the values of either the <see cref="P:System.Drawing.Point.X" /> properties or the <see cref="P:System.Drawing.Point.Y" /> properties of <paramref name="left" /> and <paramref name="right" /> differ; otherwise, false.</returns>
		/// <param name="left">A <see cref="T:System.Drawing.Point" /> to compare. </param>
		/// <param name="right">A <see cref="T:System.Drawing.Point" /> to compare. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06000602 RID: 1538 RVA: 0x00011E01 File Offset: 0x00010001
		public static bool operator !=(Point left, Point right)
		{
			return left.X != right.X || left.Y != right.Y;
		}

		/// <summary>Translates a <see cref="T:System.Drawing.Point" /> by the negative of a given <see cref="T:System.Drawing.Size" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Point" /> structure that is translated by the negative of a given <see cref="T:System.Drawing.Size" /> structure.</returns>
		/// <param name="pt">The <see cref="T:System.Drawing.Point" /> to translate. </param>
		/// <param name="sz">A <see cref="T:System.Drawing.Size" /> that specifies the pair of numbers to subtract from the coordinates of <paramref name="pt" />. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06000603 RID: 1539 RVA: 0x00011E28 File Offset: 0x00010028
		public static Point operator -(Point pt, Size sz)
		{
			return new Point(pt.X - sz.Width, pt.Y - sz.Height);
		}

		/// <summary>Converts the specified <see cref="T:System.Drawing.Point" /> structure to a <see cref="T:System.Drawing.Size" /> structure.</summary>
		/// <returns>The <see cref="T:System.Drawing.Size" /> that results from the conversion.</returns>
		/// <param name="p">The <see cref="T:System.Drawing.Point" /> to be converted.</param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06000604 RID: 1540 RVA: 0x00011E4D File Offset: 0x0001004D
		public static explicit operator Size(Point p)
		{
			return new Size(p.X, p.Y);
		}

		/// <summary>Converts the specified <see cref="T:System.Drawing.Point" /> structure to a <see cref="T:System.Drawing.PointF" /> structure.</summary>
		/// <returns>The <see cref="T:System.Drawing.PointF" /> that results from the conversion.</returns>
		/// <param name="p">The <see cref="T:System.Drawing.Point" /> to be converted.</param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06000605 RID: 1541 RVA: 0x00011E62 File Offset: 0x00010062
		public static implicit operator PointF(Point p)
		{
			return new PointF((float)p.X, (float)p.Y);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Point" /> class using coordinates specified by an integer value.</summary>
		/// <param name="dw">A 32-bit integer that specifies the coordinates for the new <see cref="T:System.Drawing.Point" />. </param>
		// Token: 0x06000606 RID: 1542 RVA: 0x00011E79 File Offset: 0x00010079
		public Point(int dw)
		{
			this.y = dw >> 16;
			this.x = (int)((short)(dw & 65535));
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Point" /> class from a <see cref="T:System.Drawing.Size" />.</summary>
		/// <param name="sz">A <see cref="T:System.Drawing.Size" /> that specifies the coordinates for the new <see cref="T:System.Drawing.Point" />. </param>
		// Token: 0x06000607 RID: 1543 RVA: 0x00011E93 File Offset: 0x00010093
		public Point(Size sz)
		{
			this.x = sz.Width;
			this.y = sz.Height;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Point" /> class with the specified coordinates.</summary>
		/// <param name="x">The horizontal position of the point. </param>
		/// <param name="y">The vertical position of the point. </param>
		// Token: 0x06000608 RID: 1544 RVA: 0x00011EAF File Offset: 0x000100AF
		public Point(int x, int y)
		{
			this.x = x;
			this.y = y;
		}

		/// <summary>Gets a value indicating whether this <see cref="T:System.Drawing.Point" /> is empty.</summary>
		/// <returns>true if both <see cref="P:System.Drawing.Point.X" /> and <see cref="P:System.Drawing.Point.Y" /> are 0; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000240 RID: 576
		// (get) Token: 0x06000609 RID: 1545 RVA: 0x00011EBF File Offset: 0x000100BF
		[Browsable(false)]
		public bool IsEmpty
		{
			get
			{
				return this.x == 0 && this.y == 0;
			}
		}

		/// <summary>Gets or sets the x-coordinate of this <see cref="T:System.Drawing.Point" />.</summary>
		/// <returns>The x-coordinate of this <see cref="T:System.Drawing.Point" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000241 RID: 577
		// (get) Token: 0x0600060A RID: 1546 RVA: 0x00011ED4 File Offset: 0x000100D4
		// (set) Token: 0x0600060B RID: 1547 RVA: 0x00011EDC File Offset: 0x000100DC
		public int X
		{
			get
			{
				return this.x;
			}
			set
			{
				this.x = value;
			}
		}

		/// <summary>Gets or sets the y-coordinate of this <see cref="T:System.Drawing.Point" />.</summary>
		/// <returns>The y-coordinate of this <see cref="T:System.Drawing.Point" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000242 RID: 578
		// (get) Token: 0x0600060C RID: 1548 RVA: 0x00011EE5 File Offset: 0x000100E5
		// (set) Token: 0x0600060D RID: 1549 RVA: 0x00011EED File Offset: 0x000100ED
		public int Y
		{
			get
			{
				return this.y;
			}
			set
			{
				this.y = value;
			}
		}

		/// <summary>Specifies whether this <see cref="T:System.Drawing.Point" /> contains the same coordinates as the specified <see cref="T:System.Object" />.</summary>
		/// <returns>true if <paramref name="obj" /> is a <see cref="T:System.Drawing.Point" /> and has the same coordinates as this <see cref="T:System.Drawing.Point" />.</returns>
		/// <param name="obj">The <see cref="T:System.Object" /> to test. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600060E RID: 1550 RVA: 0x00011EF6 File Offset: 0x000100F6
		public override bool Equals(object obj)
		{
			return obj is Point && this == (Point)obj;
		}

		/// <summary>Returns a hash code for this <see cref="T:System.Drawing.Point" />.</summary>
		/// <returns>An integer value that specifies a hash value for this <see cref="T:System.Drawing.Point" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600060F RID: 1551 RVA: 0x00011F13 File Offset: 0x00010113
		public override int GetHashCode()
		{
			return this.x ^ this.y;
		}

		/// <summary>Translates this <see cref="T:System.Drawing.Point" /> by the specified amount.</summary>
		/// <param name="dx">The amount to offset the x-coordinate. </param>
		/// <param name="dy">The amount to offset the y-coordinate. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000610 RID: 1552 RVA: 0x00011F22 File Offset: 0x00010122
		public void Offset(int dx, int dy)
		{
			this.x += dx;
			this.y += dy;
		}

		/// <summary>Converts this <see cref="T:System.Drawing.Point" /> to a human-readable string.</summary>
		/// <returns>A string that represents this <see cref="T:System.Drawing.Point" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06000611 RID: 1553 RVA: 0x00011F40 File Offset: 0x00010140
		public override string ToString()
		{
			return string.Format("{{X={0},Y={1}}}", this.x.ToString(CultureInfo.InvariantCulture), this.y.ToString(CultureInfo.InvariantCulture));
		}

		/// <summary>Adds the specified <see cref="T:System.Drawing.Size" /> to the specified <see cref="T:System.Drawing.Point" />.</summary>
		/// <returns>The <see cref="T:System.Drawing.Point" /> that is the result of the addition operation.</returns>
		/// <param name="pt">The <see cref="T:System.Drawing.Point" /> to add.</param>
		/// <param name="sz">The <see cref="T:System.Drawing.Size" /> to add</param>
		// Token: 0x06000612 RID: 1554 RVA: 0x00011DB8 File Offset: 0x0000FFB8
		public static Point Add(Point pt, Size sz)
		{
			return new Point(pt.X + sz.Width, pt.Y + sz.Height);
		}

		/// <summary>Translates this <see cref="T:System.Drawing.Point" /> by the specified <see cref="T:System.Drawing.Point" />.</summary>
		/// <param name="p">The <see cref="T:System.Drawing.Point" /> used offset this <see cref="T:System.Drawing.Point" />.</param>
		// Token: 0x06000613 RID: 1555 RVA: 0x00011F6C File Offset: 0x0001016C
		public void Offset(Point p)
		{
			this.Offset(p.X, p.Y);
		}

		/// <summary>Returns the result of subtracting specified <see cref="T:System.Drawing.Size" /> from the specified <see cref="T:System.Drawing.Point" />.</summary>
		/// <returns>The <see cref="T:System.Drawing.Point" /> that is the result of the subtraction operation.</returns>
		/// <param name="pt">The <see cref="T:System.Drawing.Point" /> to be subtracted from. </param>
		/// <param name="sz">The <see cref="T:System.Drawing.Size" /> to subtract from the <see cref="T:System.Drawing.Point" />.</param>
		// Token: 0x06000614 RID: 1556 RVA: 0x00011E28 File Offset: 0x00010028
		public static Point Subtract(Point pt, Size sz)
		{
			return new Point(pt.X - sz.Width, pt.Y - sz.Height);
		}

		// Token: 0x0400053C RID: 1340
		private int x;

		// Token: 0x0400053D RID: 1341
		private int y;

		/// <summary>Represents a <see cref="T:System.Drawing.Point" /> that has <see cref="P:System.Drawing.Point.X" /> and <see cref="P:System.Drawing.Point.Y" /> values set to zero. </summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0400053E RID: 1342
		public static readonly Point Empty;
	}
}
