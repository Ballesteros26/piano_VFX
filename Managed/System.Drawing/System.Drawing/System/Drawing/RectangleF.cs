using System;
using System.ComponentModel;

namespace System.Drawing
{
	/// <summary>Stores a set of four floating-point numbers that represent the location and size of a rectangle. For more advanced region functions, use a <see cref="T:System.Drawing.Region" /> object.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000081 RID: 129
	[Serializable]
	public struct RectangleF
	{
		/// <summary>Creates a <see cref="T:System.Drawing.RectangleF" /> structure with upper-left corner and lower-right corner at the specified locations.</summary>
		/// <returns>The new <see cref="T:System.Drawing.RectangleF" /> that this method creates.</returns>
		/// <param name="left">The x-coordinate of the upper-left corner of the rectangular region. </param>
		/// <param name="top">The y-coordinate of the upper-left corner of the rectangular region. </param>
		/// <param name="right">The x-coordinate of the lower-right corner of the rectangular region. </param>
		/// <param name="bottom">The y-coordinate of the lower-right corner of the rectangular region. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000663 RID: 1635 RVA: 0x00012BB1 File Offset: 0x00010DB1
		public static RectangleF FromLTRB(float left, float top, float right, float bottom)
		{
			return new RectangleF(left, top, right - left, bottom - top);
		}

		/// <summary>Creates and returns an enlarged copy of the specified <see cref="T:System.Drawing.RectangleF" /> structure. The copy is enlarged by the specified amount and the original rectangle remains unmodified.</summary>
		/// <returns>The enlarged <see cref="T:System.Drawing.RectangleF" />.</returns>
		/// <param name="rect">The <see cref="T:System.Drawing.RectangleF" /> to be copied. This rectangle is not modified. </param>
		/// <param name="x">The amount to enlarge the copy of the rectangle horizontally. </param>
		/// <param name="y">The amount to enlarge the copy of the rectangle vertically. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000664 RID: 1636 RVA: 0x00012BC0 File Offset: 0x00010DC0
		public static RectangleF Inflate(RectangleF rect, float x, float y)
		{
			RectangleF rectangleF = new RectangleF(rect.X, rect.Y, rect.Width, rect.Height);
			rectangleF.Inflate(x, y);
			return rectangleF;
		}

		/// <summary>Enlarges this <see cref="T:System.Drawing.RectangleF" /> structure by the specified amount.</summary>
		/// <param name="x">The amount to inflate this <see cref="T:System.Drawing.RectangleF" /> structure horizontally. </param>
		/// <param name="y">The amount to inflate this <see cref="T:System.Drawing.RectangleF" /> structure vertically. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000665 RID: 1637 RVA: 0x00012BFA File Offset: 0x00010DFA
		public void Inflate(float x, float y)
		{
			this.Inflate(new SizeF(x, y));
		}

		/// <summary>Enlarges this <see cref="T:System.Drawing.RectangleF" /> by the specified amount.</summary>
		/// <param name="size">The amount to inflate this rectangle. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000666 RID: 1638 RVA: 0x00012C0C File Offset: 0x00010E0C
		public void Inflate(SizeF size)
		{
			this.x -= size.Width;
			this.y -= size.Height;
			this.width += size.Width * 2f;
			this.height += size.Height * 2f;
		}

		/// <summary>Returns a <see cref="T:System.Drawing.RectangleF" /> structure that represents the intersection of two rectangles. If there is no intersection, and empty <see cref="T:System.Drawing.RectangleF" /> is returned.</summary>
		/// <returns>A third <see cref="T:System.Drawing.RectangleF" /> structure the size of which represents the overlapped area of the two specified rectangles.</returns>
		/// <param name="a">A rectangle to intersect. </param>
		/// <param name="b">A rectangle to intersect. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000667 RID: 1639 RVA: 0x00012C78 File Offset: 0x00010E78
		public static RectangleF Intersect(RectangleF a, RectangleF b)
		{
			if (!a.IntersectsWithInclusive(b))
			{
				return RectangleF.Empty;
			}
			return RectangleF.FromLTRB(Math.Max(a.Left, b.Left), Math.Max(a.Top, b.Top), Math.Min(a.Right, b.Right), Math.Min(a.Bottom, b.Bottom));
		}

		/// <summary>Replaces this <see cref="T:System.Drawing.RectangleF" /> structure with the intersection of itself and the specified <see cref="T:System.Drawing.RectangleF" /> structure.</summary>
		/// <param name="rect">The rectangle to intersect. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000668 RID: 1640 RVA: 0x00012CE6 File Offset: 0x00010EE6
		public void Intersect(RectangleF rect)
		{
			this = RectangleF.Intersect(this, rect);
		}

		/// <summary>Creates the smallest possible third rectangle that can contain both of two rectangles that form a union.</summary>
		/// <returns>A third <see cref="T:System.Drawing.RectangleF" /> structure that contains both of the two rectangles that form the union.</returns>
		/// <param name="a">A rectangle to union. </param>
		/// <param name="b">A rectangle to union. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000669 RID: 1641 RVA: 0x00012CFC File Offset: 0x00010EFC
		public static RectangleF Union(RectangleF a, RectangleF b)
		{
			return RectangleF.FromLTRB(Math.Min(a.Left, b.Left), Math.Min(a.Top, b.Top), Math.Max(a.Right, b.Right), Math.Max(a.Bottom, b.Bottom));
		}

		/// <summary>Tests whether two <see cref="T:System.Drawing.RectangleF" /> structures have equal location and size.</summary>
		/// <returns>This operator returns true if the two specified <see cref="T:System.Drawing.RectangleF" /> structures have equal <see cref="P:System.Drawing.RectangleF.X" />, <see cref="P:System.Drawing.RectangleF.Y" />, <see cref="P:System.Drawing.RectangleF.Width" />, and <see cref="P:System.Drawing.RectangleF.Height" /> properties.</returns>
		/// <param name="left">The <see cref="T:System.Drawing.RectangleF" /> structure that is to the left of the equality operator. </param>
		/// <param name="right">The <see cref="T:System.Drawing.RectangleF" /> structure that is to the right of the equality operator. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x0600066A RID: 1642 RVA: 0x00012D5C File Offset: 0x00010F5C
		public static bool operator ==(RectangleF left, RectangleF right)
		{
			return left.X == right.X && left.Y == right.Y && left.Width == right.Width && left.Height == right.Height;
		}

		/// <summary>Tests whether two <see cref="T:System.Drawing.RectangleF" /> structures differ in location or size.</summary>
		/// <returns>This operator returns true if any of the <see cref="P:System.Drawing.RectangleF.X" /> , <see cref="P:System.Drawing.RectangleF.Y" />, <see cref="P:System.Drawing.RectangleF.Width" />, or <see cref="P:System.Drawing.RectangleF.Height" /> properties of the two <see cref="T:System.Drawing.Rectangle" /> structures are unequal; otherwise false.</returns>
		/// <param name="left">The <see cref="T:System.Drawing.RectangleF" /> structure that is to the left of the inequality operator. </param>
		/// <param name="right">The <see cref="T:System.Drawing.RectangleF" /> structure that is to the right of the inequality operator. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x0600066B RID: 1643 RVA: 0x00012DAC File Offset: 0x00010FAC
		public static bool operator !=(RectangleF left, RectangleF right)
		{
			return left.X != right.X || left.Y != right.Y || left.Width != right.Width || left.Height != right.Height;
		}

		/// <summary>Converts the specified <see cref="T:System.Drawing.Rectangle" /> structure to a <see cref="T:System.Drawing.RectangleF" /> structure.</summary>
		/// <returns>The <see cref="T:System.Drawing.RectangleF" /> structure that is converted from the specified <see cref="T:System.Drawing.Rectangle" /> structure.</returns>
		/// <param name="r">The <see cref="T:System.Drawing.Rectangle" /> structure to convert. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x0600066C RID: 1644 RVA: 0x00012DFE File Offset: 0x00010FFE
		public static implicit operator RectangleF(Rectangle r)
		{
			return new RectangleF((float)r.X, (float)r.Y, (float)r.Width, (float)r.Height);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.RectangleF" /> class with the specified location and size.</summary>
		/// <param name="location">A <see cref="T:System.Drawing.PointF" /> that represents the upper-left corner of the rectangular region. </param>
		/// <param name="size">A <see cref="T:System.Drawing.SizeF" /> that represents the width and height of the rectangular region. </param>
		// Token: 0x0600066D RID: 1645 RVA: 0x00012E25 File Offset: 0x00011025
		public RectangleF(PointF location, SizeF size)
		{
			this.x = location.X;
			this.y = location.Y;
			this.width = size.Width;
			this.height = size.Height;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.RectangleF" /> class with the specified location and size.</summary>
		/// <param name="x">The x-coordinate of the upper-left corner of the rectangle. </param>
		/// <param name="y">The y-coordinate of the upper-left corner of the rectangle. </param>
		/// <param name="width">The width of the rectangle. </param>
		/// <param name="height">The height of the rectangle. </param>
		// Token: 0x0600066E RID: 1646 RVA: 0x00012E5B File Offset: 0x0001105B
		public RectangleF(float x, float y, float width, float height)
		{
			this.x = x;
			this.y = y;
			this.width = width;
			this.height = height;
		}

		/// <summary>Gets the y-coordinate that is the sum of <see cref="P:System.Drawing.RectangleF.Y" /> and <see cref="P:System.Drawing.RectangleF.Height" /> of this <see cref="T:System.Drawing.RectangleF" /> structure.</summary>
		/// <returns>The y-coordinate that is the sum of <see cref="P:System.Drawing.RectangleF.Y" /> and <see cref="P:System.Drawing.RectangleF.Height" /> of this <see cref="T:System.Drawing.RectangleF" /> structure.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000251 RID: 593
		// (get) Token: 0x0600066F RID: 1647 RVA: 0x00012E7A File Offset: 0x0001107A
		[Browsable(false)]
		public float Bottom
		{
			get
			{
				return this.Y + this.Height;
			}
		}

		/// <summary>Gets or sets the height of this <see cref="T:System.Drawing.RectangleF" /> structure.</summary>
		/// <returns>The height of this <see cref="T:System.Drawing.RectangleF" /> structure. The default is 0.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000252 RID: 594
		// (get) Token: 0x06000670 RID: 1648 RVA: 0x00012E89 File Offset: 0x00011089
		// (set) Token: 0x06000671 RID: 1649 RVA: 0x00012E91 File Offset: 0x00011091
		public float Height
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

		/// <summary>Tests whether the <see cref="P:System.Drawing.RectangleF.Width" /> or <see cref="P:System.Drawing.RectangleF.Height" /> property of this <see cref="T:System.Drawing.RectangleF" /> has a value of zero.</summary>
		/// <returns>This property returns true if the <see cref="P:System.Drawing.RectangleF.Width" /> or <see cref="P:System.Drawing.RectangleF.Height" /> property of this <see cref="T:System.Drawing.RectangleF" /> has a value of zero; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000253 RID: 595
		// (get) Token: 0x06000672 RID: 1650 RVA: 0x00012E9A File Offset: 0x0001109A
		[Browsable(false)]
		public bool IsEmpty
		{
			get
			{
				return this.width <= 0f || this.height <= 0f;
			}
		}

		/// <summary>Gets the x-coordinate of the left edge of this <see cref="T:System.Drawing.RectangleF" /> structure.</summary>
		/// <returns>The x-coordinate of the left edge of this <see cref="T:System.Drawing.RectangleF" /> structure. </returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000254 RID: 596
		// (get) Token: 0x06000673 RID: 1651 RVA: 0x00012EBB File Offset: 0x000110BB
		[Browsable(false)]
		public float Left
		{
			get
			{
				return this.X;
			}
		}

		/// <summary>Gets or sets the coordinates of the upper-left corner of this <see cref="T:System.Drawing.RectangleF" /> structure.</summary>
		/// <returns>A <see cref="T:System.Drawing.PointF" /> that represents the upper-left corner of this <see cref="T:System.Drawing.RectangleF" /> structure.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000255 RID: 597
		// (get) Token: 0x06000674 RID: 1652 RVA: 0x00012EC3 File Offset: 0x000110C3
		// (set) Token: 0x06000675 RID: 1653 RVA: 0x00012ED6 File Offset: 0x000110D6
		[Browsable(false)]
		public PointF Location
		{
			get
			{
				return new PointF(this.x, this.y);
			}
			set
			{
				this.x = value.X;
				this.y = value.Y;
			}
		}

		/// <summary>Gets the x-coordinate that is the sum of <see cref="P:System.Drawing.RectangleF.X" /> and <see cref="P:System.Drawing.RectangleF.Width" /> of this <see cref="T:System.Drawing.RectangleF" /> structure.</summary>
		/// <returns>The x-coordinate that is the sum of <see cref="P:System.Drawing.RectangleF.X" /> and <see cref="P:System.Drawing.RectangleF.Width" /> of this <see cref="T:System.Drawing.RectangleF" /> structure. </returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000256 RID: 598
		// (get) Token: 0x06000676 RID: 1654 RVA: 0x00012EF2 File Offset: 0x000110F2
		[Browsable(false)]
		public float Right
		{
			get
			{
				return this.X + this.Width;
			}
		}

		/// <summary>Gets or sets the size of this <see cref="T:System.Drawing.RectangleF" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.SizeF" /> that represents the width and height of this <see cref="T:System.Drawing.RectangleF" /> structure.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000257 RID: 599
		// (get) Token: 0x06000677 RID: 1655 RVA: 0x00012F01 File Offset: 0x00011101
		// (set) Token: 0x06000678 RID: 1656 RVA: 0x00012F14 File Offset: 0x00011114
		[Browsable(false)]
		public SizeF Size
		{
			get
			{
				return new SizeF(this.width, this.height);
			}
			set
			{
				this.width = value.Width;
				this.height = value.Height;
			}
		}

		/// <summary>Gets the y-coordinate of the top edge of this <see cref="T:System.Drawing.RectangleF" /> structure.</summary>
		/// <returns>The y-coordinate of the top edge of this <see cref="T:System.Drawing.RectangleF" /> structure.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000258 RID: 600
		// (get) Token: 0x06000679 RID: 1657 RVA: 0x00012F30 File Offset: 0x00011130
		[Browsable(false)]
		public float Top
		{
			get
			{
				return this.Y;
			}
		}

		/// <summary>Gets or sets the width of this <see cref="T:System.Drawing.RectangleF" /> structure.</summary>
		/// <returns>The width of this <see cref="T:System.Drawing.RectangleF" /> structure. The default is 0.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000259 RID: 601
		// (get) Token: 0x0600067A RID: 1658 RVA: 0x00012F38 File Offset: 0x00011138
		// (set) Token: 0x0600067B RID: 1659 RVA: 0x00012F40 File Offset: 0x00011140
		public float Width
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

		/// <summary>Gets or sets the x-coordinate of the upper-left corner of this <see cref="T:System.Drawing.RectangleF" /> structure.</summary>
		/// <returns>The x-coordinate of the upper-left corner of this <see cref="T:System.Drawing.RectangleF" /> structure. The default is 0.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700025A RID: 602
		// (get) Token: 0x0600067C RID: 1660 RVA: 0x00012F49 File Offset: 0x00011149
		// (set) Token: 0x0600067D RID: 1661 RVA: 0x00012F51 File Offset: 0x00011151
		public float X
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

		/// <summary>Gets or sets the y-coordinate of the upper-left corner of this <see cref="T:System.Drawing.RectangleF" /> structure.</summary>
		/// <returns>The y-coordinate of the upper-left corner of this <see cref="T:System.Drawing.RectangleF" /> structure. The default is 0.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700025B RID: 603
		// (get) Token: 0x0600067E RID: 1662 RVA: 0x00012F5A File Offset: 0x0001115A
		// (set) Token: 0x0600067F RID: 1663 RVA: 0x00012F62 File Offset: 0x00011162
		public float Y
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

		/// <summary>Determines if the specified point is contained within this <see cref="T:System.Drawing.RectangleF" /> structure.</summary>
		/// <returns>This method returns true if the point defined by <paramref name="x" /> and <paramref name="y" /> is contained within this <see cref="T:System.Drawing.RectangleF" /> structure; otherwise false.</returns>
		/// <param name="x">The x-coordinate of the point to test. </param>
		/// <param name="y">The y-coordinate of the point to test. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000680 RID: 1664 RVA: 0x00012F6B File Offset: 0x0001116B
		public bool Contains(float x, float y)
		{
			return x >= this.Left && x < this.Right && y >= this.Top && y < this.Bottom;
		}

		/// <summary>Determines if the specified point is contained within this <see cref="T:System.Drawing.RectangleF" /> structure.</summary>
		/// <returns>This method returns true if the point represented by the <paramref name="pt" /> parameter is contained within this <see cref="T:System.Drawing.RectangleF" /> structure; otherwise false.</returns>
		/// <param name="pt">The <see cref="T:System.Drawing.PointF" /> to test. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000681 RID: 1665 RVA: 0x00012F93 File Offset: 0x00011193
		public bool Contains(PointF pt)
		{
			return this.Contains(pt.X, pt.Y);
		}

		/// <summary>Determines if the rectangular region represented by <paramref name="rect" /> is entirely contained within this <see cref="T:System.Drawing.RectangleF" /> structure.</summary>
		/// <returns>This method returns true if the rectangular region represented by <paramref name="rect" /> is entirely contained within the rectangular region represented by this <see cref="T:System.Drawing.RectangleF" />; otherwise false.</returns>
		/// <param name="rect">The <see cref="T:System.Drawing.RectangleF" /> to test. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000682 RID: 1666 RVA: 0x00012FAC File Offset: 0x000111AC
		public bool Contains(RectangleF rect)
		{
			return this.X <= rect.X && this.Right >= rect.Right && this.Y <= rect.Y && this.Bottom >= rect.Bottom;
		}

		/// <summary>Tests whether <paramref name="obj" /> is a <see cref="T:System.Drawing.RectangleF" /> with the same location and size of this <see cref="T:System.Drawing.RectangleF" />.</summary>
		/// <returns>This method returns true if <paramref name="obj" /> is a <see cref="T:System.Drawing.RectangleF" /> and its X, Y, Width, and Height properties are equal to the corresponding properties of this <see cref="T:System.Drawing.RectangleF" />; otherwise, false.</returns>
		/// <param name="obj">The <see cref="T:System.Object" /> to test. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000683 RID: 1667 RVA: 0x00012FFA File Offset: 0x000111FA
		public override bool Equals(object obj)
		{
			return obj is RectangleF && this == (RectangleF)obj;
		}

		/// <summary>Gets the hash code for this <see cref="T:System.Drawing.RectangleF" /> structure. For information about the use of hash codes, see Object.GetHashCode.</summary>
		/// <returns>The hash code for this <see cref="T:System.Drawing.RectangleF" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000684 RID: 1668 RVA: 0x00013017 File Offset: 0x00011217
		public override int GetHashCode()
		{
			return (int)(this.x + this.y + this.width + this.height);
		}

		/// <summary>Determines if this rectangle intersects with <paramref name="rect" />.</summary>
		/// <returns>This method returns true if there is any intersection.</returns>
		/// <param name="rect">The rectangle to test. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000685 RID: 1669 RVA: 0x00013035 File Offset: 0x00011235
		public bool IntersectsWith(RectangleF rect)
		{
			return this.Left < rect.Right && this.Right > rect.Left && this.Top < rect.Bottom && this.Bottom > rect.Top;
		}

		// Token: 0x06000686 RID: 1670 RVA: 0x00013078 File Offset: 0x00011278
		private bool IntersectsWithInclusive(RectangleF r)
		{
			return this.Left <= r.Right && this.Right >= r.Left && this.Top <= r.Bottom && this.Bottom >= r.Top;
		}

		/// <summary>Adjusts the location of this rectangle by the specified amount.</summary>
		/// <param name="x">The amount to offset the location horizontally. </param>
		/// <param name="y">The amount to offset the location vertically. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000687 RID: 1671 RVA: 0x000130C6 File Offset: 0x000112C6
		public void Offset(float x, float y)
		{
			this.X += x;
			this.Y += y;
		}

		/// <summary>Adjusts the location of this rectangle by the specified amount.</summary>
		/// <param name="pos">The amount to offset the location. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000688 RID: 1672 RVA: 0x000130E4 File Offset: 0x000112E4
		public void Offset(PointF pos)
		{
			this.Offset(pos.X, pos.Y);
		}

		/// <summary>Converts the Location and <see cref="T:System.Drawing.Size" /> of this <see cref="T:System.Drawing.RectangleF" /> to a human-readable string.</summary>
		/// <returns>A string that contains the position, width, and height of this <see cref="T:System.Drawing.RectangleF" /> structure. For example, "{X=20, Y=20, Width=100, Height=50}".</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06000689 RID: 1673 RVA: 0x000130FC File Offset: 0x000112FC
		public override string ToString()
		{
			return string.Format("{{X={0},Y={1},Width={2},Height={3}}}", new object[] { this.x, this.y, this.width, this.height });
		}

		// Token: 0x04000547 RID: 1351
		private float x;

		// Token: 0x04000548 RID: 1352
		private float y;

		// Token: 0x04000549 RID: 1353
		private float width;

		// Token: 0x0400054A RID: 1354
		private float height;

		/// <summary>Represents an instance of the <see cref="T:System.Drawing.RectangleF" /> class with its members uninitialized.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0400054B RID: 1355
		public static readonly RectangleF Empty;
	}
}
