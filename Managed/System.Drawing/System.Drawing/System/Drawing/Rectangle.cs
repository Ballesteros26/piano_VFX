using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace System.Drawing
{
	/// <summary>Stores a set of four integers that represent the location and size of a rectangle</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200007F RID: 127
	[TypeConverter(typeof(RectangleConverter))]
	[ComVisible(true)]
	[Serializable]
	public struct Rectangle
	{
		/// <summary>Converts the specified <see cref="T:System.Drawing.RectangleF" /> structure to a <see cref="T:System.Drawing.Rectangle" /> structure by rounding the <see cref="T:System.Drawing.RectangleF" /> values to the next higher integer values.</summary>
		/// <returns>Returns a <see cref="T:System.Drawing.Rectangle" />.</returns>
		/// <param name="value">The <see cref="T:System.Drawing.RectangleF" /> structure to be converted. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000631 RID: 1585 RVA: 0x00012314 File Offset: 0x00010514
		public static Rectangle Ceiling(RectangleF value)
		{
			checked
			{
				int num = (int)Math.Ceiling((double)value.X);
				int num2 = (int)Math.Ceiling((double)value.Y);
				int num3 = (int)Math.Ceiling((double)value.Width);
				int num4 = (int)Math.Ceiling((double)value.Height);
				return new Rectangle(num, num2, num3, num4);
			}
		}

		/// <summary>Creates a <see cref="T:System.Drawing.Rectangle" /> structure with the specified edge locations.</summary>
		/// <returns>The new <see cref="T:System.Drawing.Rectangle" /> that this method creates.</returns>
		/// <param name="left">The x-coordinate of the upper-left corner of this <see cref="T:System.Drawing.Rectangle" /> structure. </param>
		/// <param name="top">The y-coordinate of the upper-left corner of this <see cref="T:System.Drawing.Rectangle" /> structure. </param>
		/// <param name="right">The x-coordinate of the lower-right corner of this <see cref="T:System.Drawing.Rectangle" /> structure. </param>
		/// <param name="bottom">The y-coordinate of the lower-right corner of this <see cref="T:System.Drawing.Rectangle" /> structure. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000632 RID: 1586 RVA: 0x00012364 File Offset: 0x00010564
		public static Rectangle FromLTRB(int left, int top, int right, int bottom)
		{
			return new Rectangle(left, top, right - left, bottom - top);
		}

		/// <summary>Creates and returns an enlarged copy of the specified <see cref="T:System.Drawing.Rectangle" /> structure. The copy is enlarged by the specified amount. The original <see cref="T:System.Drawing.Rectangle" /> structure remains unmodified.</summary>
		/// <returns>The enlarged <see cref="T:System.Drawing.Rectangle" />.</returns>
		/// <param name="rect">The <see cref="T:System.Drawing.Rectangle" /> with which to start. This rectangle is not modified. </param>
		/// <param name="x">The amount to inflate this <see cref="T:System.Drawing.Rectangle" /> horizontally. </param>
		/// <param name="y">The amount to inflate this <see cref="T:System.Drawing.Rectangle" /> vertically. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000633 RID: 1587 RVA: 0x00012374 File Offset: 0x00010574
		public static Rectangle Inflate(Rectangle rect, int x, int y)
		{
			Rectangle rectangle = new Rectangle(rect.Location, rect.Size);
			rectangle.Inflate(x, y);
			return rectangle;
		}

		/// <summary>Enlarges this <see cref="T:System.Drawing.Rectangle" /> by the specified amount.</summary>
		/// <param name="width">The amount to inflate this <see cref="T:System.Drawing.Rectangle" /> horizontally. </param>
		/// <param name="height">The amount to inflate this <see cref="T:System.Drawing.Rectangle" /> vertically. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000634 RID: 1588 RVA: 0x000123A0 File Offset: 0x000105A0
		public void Inflate(int width, int height)
		{
			this.Inflate(new Size(width, height));
		}

		/// <summary>Enlarges this <see cref="T:System.Drawing.Rectangle" /> by the specified amount.</summary>
		/// <param name="size">The amount to inflate this rectangle. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000635 RID: 1589 RVA: 0x000123B0 File Offset: 0x000105B0
		public void Inflate(Size size)
		{
			this.x -= size.Width;
			this.y -= size.Height;
			this.Width += size.Width * 2;
			this.Height += size.Height * 2;
		}

		/// <summary>Returns a third <see cref="T:System.Drawing.Rectangle" /> structure that represents the intersection of two other <see cref="T:System.Drawing.Rectangle" /> structures. If there is no intersection, an empty <see cref="T:System.Drawing.Rectangle" /> is returned.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the intersection of <paramref name="a" /> and <paramref name="b" />.</returns>
		/// <param name="a">A rectangle to intersect. </param>
		/// <param name="b">A rectangle to intersect. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000636 RID: 1590 RVA: 0x00012414 File Offset: 0x00010614
		public static Rectangle Intersect(Rectangle a, Rectangle b)
		{
			if (!a.IntersectsWithInclusive(b))
			{
				return Rectangle.Empty;
			}
			return Rectangle.FromLTRB(Math.Max(a.Left, b.Left), Math.Max(a.Top, b.Top), Math.Min(a.Right, b.Right), Math.Min(a.Bottom, b.Bottom));
		}

		/// <summary>Replaces this <see cref="T:System.Drawing.Rectangle" /> with the intersection of itself and the specified <see cref="T:System.Drawing.Rectangle" />.</summary>
		/// <param name="rect">The <see cref="T:System.Drawing.Rectangle" /> with which to intersect. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000637 RID: 1591 RVA: 0x00012482 File Offset: 0x00010682
		public void Intersect(Rectangle rect)
		{
			this = Rectangle.Intersect(this, rect);
		}

		/// <summary>Converts the specified <see cref="T:System.Drawing.RectangleF" /> to a <see cref="T:System.Drawing.Rectangle" /> by rounding the <see cref="T:System.Drawing.RectangleF" /> values to the nearest integer values.</summary>
		/// <returns>The rounded interger value of the <see cref="T:System.Drawing.Rectangle" />.</returns>
		/// <param name="value">The <see cref="T:System.Drawing.RectangleF" /> to be converted. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000638 RID: 1592 RVA: 0x00012498 File Offset: 0x00010698
		public static Rectangle Round(RectangleF value)
		{
			checked
			{
				int num = (int)Math.Round((double)value.X);
				int num2 = (int)Math.Round((double)value.Y);
				int num3 = (int)Math.Round((double)value.Width);
				int num4 = (int)Math.Round((double)value.Height);
				return new Rectangle(num, num2, num3, num4);
			}
		}

		/// <summary>Converts the specified <see cref="T:System.Drawing.RectangleF" /> to a <see cref="T:System.Drawing.Rectangle" /> by truncating the <see cref="T:System.Drawing.RectangleF" /> values.</summary>
		/// <returns>The truncated value of the  <see cref="T:System.Drawing.Rectangle" />.</returns>
		/// <param name="value">The <see cref="T:System.Drawing.RectangleF" /> to be converted. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000639 RID: 1593 RVA: 0x000124E8 File Offset: 0x000106E8
		public static Rectangle Truncate(RectangleF value)
		{
			checked
			{
				int num = (int)value.X;
				int num2 = (int)value.Y;
				int num3 = (int)value.Width;
				int num4 = (int)value.Height;
				return new Rectangle(num, num2, num3, num4);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Rectangle" /> structure that contains the union of two <see cref="T:System.Drawing.Rectangle" /> structures.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> structure that bounds the union of the two <see cref="T:System.Drawing.Rectangle" /> structures.</returns>
		/// <param name="a">A rectangle to union. </param>
		/// <param name="b">A rectangle to union. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600063A RID: 1594 RVA: 0x00012520 File Offset: 0x00010720
		public static Rectangle Union(Rectangle a, Rectangle b)
		{
			return Rectangle.FromLTRB(Math.Min(a.Left, b.Left), Math.Min(a.Top, b.Top), Math.Max(a.Right, b.Right), Math.Max(a.Bottom, b.Bottom));
		}

		/// <summary>Tests whether two <see cref="T:System.Drawing.Rectangle" /> structures have equal location and size.</summary>
		/// <returns>This operator returns true if the two <see cref="T:System.Drawing.Rectangle" /> structures have equal <see cref="P:System.Drawing.Rectangle.X" />, <see cref="P:System.Drawing.Rectangle.Y" />, <see cref="P:System.Drawing.Rectangle.Width" />, and <see cref="P:System.Drawing.Rectangle.Height" /> properties.</returns>
		/// <param name="left">The <see cref="T:System.Drawing.Rectangle" /> structure that is to the left of the equality operator. </param>
		/// <param name="right">The <see cref="T:System.Drawing.Rectangle" /> structure that is to the right of the equality operator. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x0600063B RID: 1595 RVA: 0x0001257E File Offset: 0x0001077E
		public static bool operator ==(Rectangle left, Rectangle right)
		{
			return left.Location == right.Location && left.Size == right.Size;
		}

		/// <summary>Tests whether two <see cref="T:System.Drawing.Rectangle" /> structures differ in location or size.</summary>
		/// <returns>This operator returns true if any of the <see cref="P:System.Drawing.Rectangle.X" />, <see cref="P:System.Drawing.Rectangle.Y" />, <see cref="P:System.Drawing.Rectangle.Width" /> or <see cref="P:System.Drawing.Rectangle.Height" /> properties of the two <see cref="T:System.Drawing.Rectangle" /> structures are unequal; otherwise false.</returns>
		/// <param name="left">The <see cref="T:System.Drawing.Rectangle" /> structure that is to the left of the inequality operator. </param>
		/// <param name="right">The <see cref="T:System.Drawing.Rectangle" /> structure that is to the right of the inequality operator. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x0600063C RID: 1596 RVA: 0x000125AA File Offset: 0x000107AA
		public static bool operator !=(Rectangle left, Rectangle right)
		{
			return left.Location != right.Location || left.Size != right.Size;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Rectangle" /> class with the specified location and size.</summary>
		/// <param name="location">A <see cref="T:System.Drawing.Point" /> that represents the upper-left corner of the rectangular region. </param>
		/// <param name="size">A <see cref="T:System.Drawing.Size" /> that represents the width and height of the rectangular region. </param>
		// Token: 0x0600063D RID: 1597 RVA: 0x000125D6 File Offset: 0x000107D6
		public Rectangle(Point location, Size size)
		{
			this.x = location.X;
			this.y = location.Y;
			this.width = size.Width;
			this.height = size.Height;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Rectangle" /> class with the specified location and size.</summary>
		/// <param name="x">The x-coordinate of the upper-left corner of the rectangle. </param>
		/// <param name="y">The y-coordinate of the upper-left corner of the rectangle. </param>
		/// <param name="width">The width of the rectangle. </param>
		/// <param name="height">The height of the rectangle. </param>
		// Token: 0x0600063E RID: 1598 RVA: 0x0001260C File Offset: 0x0001080C
		public Rectangle(int x, int y, int width, int height)
		{
			this.x = x;
			this.y = y;
			this.width = width;
			this.height = height;
		}

		/// <summary>Gets the y-coordinate that is the sum of the <see cref="P:System.Drawing.Rectangle.Y" /> and <see cref="P:System.Drawing.Rectangle.Height" /> property values of this <see cref="T:System.Drawing.Rectangle" /> structure.</summary>
		/// <returns>The y-coordinate that is the sum of <see cref="P:System.Drawing.Rectangle.Y" /> and <see cref="P:System.Drawing.Rectangle.Height" /> of this <see cref="T:System.Drawing.Rectangle" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000246 RID: 582
		// (get) Token: 0x0600063F RID: 1599 RVA: 0x0001262B File Offset: 0x0001082B
		[Browsable(false)]
		public int Bottom
		{
			get
			{
				return this.y + this.height;
			}
		}

		/// <summary>Gets or sets the height of this <see cref="T:System.Drawing.Rectangle" /> structure.</summary>
		/// <returns>The height of this <see cref="T:System.Drawing.Rectangle" /> structure. The default is 0.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000247 RID: 583
		// (get) Token: 0x06000640 RID: 1600 RVA: 0x0001263A File Offset: 0x0001083A
		// (set) Token: 0x06000641 RID: 1601 RVA: 0x00012642 File Offset: 0x00010842
		public int Height
		{
			get
			{
				return this.height;
			}
			set
			{
				this.height = value;
			}
		}

		/// <summary>Tests whether all numeric properties of this <see cref="T:System.Drawing.Rectangle" /> have values of zero.</summary>
		/// <returns>This property returns true if the <see cref="P:System.Drawing.Rectangle.Width" />, <see cref="P:System.Drawing.Rectangle.Height" />, <see cref="P:System.Drawing.Rectangle.X" />, and <see cref="P:System.Drawing.Rectangle.Y" /> properties of this <see cref="T:System.Drawing.Rectangle" /> all have values of zero; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000248 RID: 584
		// (get) Token: 0x06000642 RID: 1602 RVA: 0x0001264B File Offset: 0x0001084B
		[Browsable(false)]
		public bool IsEmpty
		{
			get
			{
				return this.x == 0 && this.y == 0 && this.width == 0 && this.height == 0;
			}
		}

		/// <summary>Gets the x-coordinate of the left edge of this <see cref="T:System.Drawing.Rectangle" /> structure.</summary>
		/// <returns>The x-coordinate of the left edge of this <see cref="T:System.Drawing.Rectangle" /> structure.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000249 RID: 585
		// (get) Token: 0x06000643 RID: 1603 RVA: 0x00012670 File Offset: 0x00010870
		[Browsable(false)]
		public int Left
		{
			get
			{
				return this.X;
			}
		}

		/// <summary>Gets or sets the coordinates of the upper-left corner of this <see cref="T:System.Drawing.Rectangle" /> structure.</summary>
		/// <returns>A <see cref="T:System.Drawing.Point" /> that represents the upper-left corner of this <see cref="T:System.Drawing.Rectangle" /> structure.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700024A RID: 586
		// (get) Token: 0x06000644 RID: 1604 RVA: 0x00012678 File Offset: 0x00010878
		// (set) Token: 0x06000645 RID: 1605 RVA: 0x0001268B File Offset: 0x0001088B
		[Browsable(false)]
		public Point Location
		{
			get
			{
				return new Point(this.x, this.y);
			}
			set
			{
				this.x = value.X;
				this.y = value.Y;
			}
		}

		/// <summary>Gets the x-coordinate that is the sum of <see cref="P:System.Drawing.Rectangle.X" /> and <see cref="P:System.Drawing.Rectangle.Width" /> property values of this <see cref="T:System.Drawing.Rectangle" /> structure.</summary>
		/// <returns>The x-coordinate that is the sum of <see cref="P:System.Drawing.Rectangle.X" /> and <see cref="P:System.Drawing.Rectangle.Width" /> of this <see cref="T:System.Drawing.Rectangle" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700024B RID: 587
		// (get) Token: 0x06000646 RID: 1606 RVA: 0x000126A7 File Offset: 0x000108A7
		[Browsable(false)]
		public int Right
		{
			get
			{
				return this.X + this.Width;
			}
		}

		/// <summary>Gets or sets the size of this <see cref="T:System.Drawing.Rectangle" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that represents the width and height of this <see cref="T:System.Drawing.Rectangle" /> structure.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700024C RID: 588
		// (get) Token: 0x06000647 RID: 1607 RVA: 0x000126B6 File Offset: 0x000108B6
		// (set) Token: 0x06000648 RID: 1608 RVA: 0x000126C9 File Offset: 0x000108C9
		[Browsable(false)]
		public Size Size
		{
			get
			{
				return new Size(this.Width, this.Height);
			}
			set
			{
				this.Width = value.Width;
				this.Height = value.Height;
			}
		}

		/// <summary>Gets the y-coordinate of the top edge of this <see cref="T:System.Drawing.Rectangle" /> structure.</summary>
		/// <returns>The y-coordinate of the top edge of this <see cref="T:System.Drawing.Rectangle" /> structure.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700024D RID: 589
		// (get) Token: 0x06000649 RID: 1609 RVA: 0x000126E5 File Offset: 0x000108E5
		[Browsable(false)]
		public int Top
		{
			get
			{
				return this.y;
			}
		}

		/// <summary>Gets or sets the width of this <see cref="T:System.Drawing.Rectangle" /> structure.</summary>
		/// <returns>The width of this <see cref="T:System.Drawing.Rectangle" /> structure. The default is 0.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700024E RID: 590
		// (get) Token: 0x0600064A RID: 1610 RVA: 0x000126ED File Offset: 0x000108ED
		// (set) Token: 0x0600064B RID: 1611 RVA: 0x000126F5 File Offset: 0x000108F5
		public int Width
		{
			get
			{
				return this.width;
			}
			set
			{
				this.width = value;
			}
		}

		/// <summary>Gets or sets the x-coordinate of the upper-left corner of this <see cref="T:System.Drawing.Rectangle" /> structure.</summary>
		/// <returns>The x-coordinate of the upper-left corner of this <see cref="T:System.Drawing.Rectangle" /> structure. The default is 0.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700024F RID: 591
		// (get) Token: 0x0600064C RID: 1612 RVA: 0x000126FE File Offset: 0x000108FE
		// (set) Token: 0x0600064D RID: 1613 RVA: 0x00012706 File Offset: 0x00010906
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

		/// <summary>Gets or sets the y-coordinate of the upper-left corner of this <see cref="T:System.Drawing.Rectangle" /> structure.</summary>
		/// <returns>The y-coordinate of the upper-left corner of this <see cref="T:System.Drawing.Rectangle" /> structure. The default is 0.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000250 RID: 592
		// (get) Token: 0x0600064E RID: 1614 RVA: 0x000126E5 File Offset: 0x000108E5
		// (set) Token: 0x0600064F RID: 1615 RVA: 0x0001270F File Offset: 0x0001090F
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

		/// <summary>Determines if the specified point is contained within this <see cref="T:System.Drawing.Rectangle" /> structure.</summary>
		/// <returns>This method returns true if the point defined by <paramref name="x" /> and <paramref name="y" /> is contained within this <see cref="T:System.Drawing.Rectangle" /> structure; otherwise false.</returns>
		/// <param name="x">The x-coordinate of the point to test. </param>
		/// <param name="y">The y-coordinate of the point to test. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000650 RID: 1616 RVA: 0x00012718 File Offset: 0x00010918
		public bool Contains(int x, int y)
		{
			return x >= this.Left && x < this.Right && y >= this.Top && y < this.Bottom;
		}

		/// <summary>Determines if the specified point is contained within this <see cref="T:System.Drawing.Rectangle" /> structure.</summary>
		/// <returns>This method returns true if the point represented by <paramref name="pt" /> is contained within this <see cref="T:System.Drawing.Rectangle" /> structure; otherwise false.</returns>
		/// <param name="pt">The <see cref="T:System.Drawing.Point" /> to test. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000651 RID: 1617 RVA: 0x00012740 File Offset: 0x00010940
		public bool Contains(Point pt)
		{
			return this.Contains(pt.X, pt.Y);
		}

		/// <summary>Determines if the rectangular region represented by <paramref name="rect" /> is entirely contained within this <see cref="T:System.Drawing.Rectangle" /> structure.</summary>
		/// <returns>This method returns true if the rectangular region represented by <paramref name="rect" /> is entirely contained within this <see cref="T:System.Drawing.Rectangle" /> structure; otherwise false.</returns>
		/// <param name="rect">The <see cref="T:System.Drawing.Rectangle" /> to test. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000652 RID: 1618 RVA: 0x00012756 File Offset: 0x00010956
		public bool Contains(Rectangle rect)
		{
			return rect == Rectangle.Intersect(this, rect);
		}

		/// <summary>Tests whether <paramref name="obj" /> is a <see cref="T:System.Drawing.Rectangle" /> structure with the same location and size of this <see cref="T:System.Drawing.Rectangle" /> structure.</summary>
		/// <returns>This method returns true if <paramref name="obj" /> is a <see cref="T:System.Drawing.Rectangle" /> structure and its <see cref="P:System.Drawing.Rectangle.X" />, <see cref="P:System.Drawing.Rectangle.Y" />, <see cref="P:System.Drawing.Rectangle.Width" />, and <see cref="P:System.Drawing.Rectangle.Height" /> properties are equal to the corresponding properties of this <see cref="T:System.Drawing.Rectangle" /> structure; otherwise, false.</returns>
		/// <param name="obj">The <see cref="T:System.Object" /> to test. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000653 RID: 1619 RVA: 0x0001276A File Offset: 0x0001096A
		public override bool Equals(object obj)
		{
			return obj is Rectangle && this == (Rectangle)obj;
		}

		/// <summary>Returns the hash code for this <see cref="T:System.Drawing.Rectangle" /> structure. For information about the use of hash codes, see <see cref="M:System.Object.GetHashCode" /> .</summary>
		/// <returns>An integer that represents the hash code for this rectangle.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000654 RID: 1620 RVA: 0x00012787 File Offset: 0x00010987
		public override int GetHashCode()
		{
			return (this.height + this.width) ^ (this.x + this.y);
		}

		/// <summary>Determines if this rectangle intersects with <paramref name="rect" />.</summary>
		/// <returns>This method returns true if there is any intersection, otherwise false.</returns>
		/// <param name="rect">The rectangle to test. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000655 RID: 1621 RVA: 0x000127A4 File Offset: 0x000109A4
		public bool IntersectsWith(Rectangle rect)
		{
			return this.Left < rect.Right && this.Right > rect.Left && this.Top < rect.Bottom && this.Bottom > rect.Top;
		}

		// Token: 0x06000656 RID: 1622 RVA: 0x000127E4 File Offset: 0x000109E4
		private bool IntersectsWithInclusive(Rectangle r)
		{
			return this.Left <= r.Right && this.Right >= r.Left && this.Top <= r.Bottom && this.Bottom >= r.Top;
		}

		/// <summary>Adjusts the location of this rectangle by the specified amount.</summary>
		/// <param name="x">The horizontal offset. </param>
		/// <param name="y">The vertical offset. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000657 RID: 1623 RVA: 0x00012832 File Offset: 0x00010A32
		public void Offset(int x, int y)
		{
			this.x += x;
			this.y += y;
		}

		/// <summary>Adjusts the location of this rectangle by the specified amount.</summary>
		/// <param name="pos">Amount to offset the location. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000658 RID: 1624 RVA: 0x00012850 File Offset: 0x00010A50
		public void Offset(Point pos)
		{
			this.x += pos.X;
			this.y += pos.Y;
		}

		/// <summary>Converts the attributes of this <see cref="T:System.Drawing.Rectangle" /> to a human-readable string.</summary>
		/// <returns>A string that contains the position, width, and height of this <see cref="T:System.Drawing.Rectangle" /> structure ¾ for example, {X=20, Y=20, Width=100, Height=50} </returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06000659 RID: 1625 RVA: 0x0001287C File Offset: 0x00010A7C
		public override string ToString()
		{
			return string.Format("{{X={0},Y={1},Width={2},Height={3}}}", new object[] { this.x, this.y, this.width, this.height });
		}

		// Token: 0x04000542 RID: 1346
		private int x;

		// Token: 0x04000543 RID: 1347
		private int y;

		// Token: 0x04000544 RID: 1348
		private int width;

		// Token: 0x04000545 RID: 1349
		private int height;

		/// <summary>Represents a <see cref="T:System.Drawing.Rectangle" /> structure with its properties left uninitialized.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x04000546 RID: 1350
		public static readonly Rectangle Empty;
	}
}
