using System;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Drawing
{
	/// <summary>Stores an ordered pair of floating-point numbers, typically the width and height of a rectangle.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000086 RID: 134
	[ComVisible(true)]
	[TypeConverter(typeof(SizeFConverter))]
	[Serializable]
	public struct SizeF
	{
		/// <summary>Adds the width and height of one <see cref="T:System.Drawing.SizeF" /> structure to the width and height of another <see cref="T:System.Drawing.SizeF" /> structure.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> structure that is the result of the addition operation.</returns>
		/// <param name="sz1">The first <see cref="T:System.Drawing.SizeF" /> structure to add. </param>
		/// <param name="sz2">The second <see cref="T:System.Drawing.SizeF" /> structure to add. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x060006E7 RID: 1767 RVA: 0x00013FE3 File Offset: 0x000121E3
		public static SizeF operator +(SizeF sz1, SizeF sz2)
		{
			return new SizeF(sz1.Width + sz2.Width, sz1.Height + sz2.Height);
		}

		/// <summary>Tests whether two <see cref="T:System.Drawing.SizeF" /> structures are equal.</summary>
		/// <returns>This operator returns true if <paramref name="sz1" /> and <paramref name="sz2" /> have equal width and height; otherwise, false.</returns>
		/// <param name="sz1">The <see cref="T:System.Drawing.SizeF" /> structure on the left side of the equality operator. </param>
		/// <param name="sz2">The <see cref="T:System.Drawing.SizeF" /> structure on the right of the equality operator. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x060006E8 RID: 1768 RVA: 0x00014008 File Offset: 0x00012208
		public static bool operator ==(SizeF sz1, SizeF sz2)
		{
			return sz1.Width == sz2.Width && sz1.Height == sz2.Height;
		}

		/// <summary>Tests whether two <see cref="T:System.Drawing.SizeF" /> structures are different.</summary>
		/// <returns>This operator returns true if <paramref name="sz1" /> and <paramref name="sz2" /> differ either in width or height; false if <paramref name="sz1" /> and <paramref name="sz2" /> are equal.</returns>
		/// <param name="sz1">The <see cref="T:System.Drawing.SizeF" /> structure on the left of the inequality operator. </param>
		/// <param name="sz2">The <see cref="T:System.Drawing.SizeF" /> structure on the right of the inequality operator. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x060006E9 RID: 1769 RVA: 0x0001402C File Offset: 0x0001222C
		public static bool operator !=(SizeF sz1, SizeF sz2)
		{
			return sz1.Width != sz2.Width || sz1.Height != sz2.Height;
		}

		/// <summary>Subtracts the width and height of one <see cref="T:System.Drawing.SizeF" /> structure from the width and height of another <see cref="T:System.Drawing.SizeF" /> structure.</summary>
		/// <returns>A <see cref="T:System.Drawing.SizeF" /> that is the result of the subtraction operation.</returns>
		/// <param name="sz1">The <see cref="T:System.Drawing.SizeF" /> structure on the left side of the subtraction operator. </param>
		/// <param name="sz2">The <see cref="T:System.Drawing.SizeF" /> structure on the right side of the subtraction operator. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x060006EA RID: 1770 RVA: 0x00014053 File Offset: 0x00012253
		public static SizeF operator -(SizeF sz1, SizeF sz2)
		{
			return new SizeF(sz1.Width - sz2.Width, sz1.Height - sz2.Height);
		}

		/// <summary>Converts the specified <see cref="T:System.Drawing.SizeF" /> structure to a <see cref="T:System.Drawing.PointF" /> structure.</summary>
		/// <returns>The <see cref="T:System.Drawing.PointF" /> structure to which this operator converts.</returns>
		/// <param name="size">The <see cref="T:System.Drawing.SizeF" /> structure to be converted</param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x060006EB RID: 1771 RVA: 0x00014078 File Offset: 0x00012278
		public static explicit operator PointF(SizeF size)
		{
			return new PointF(size.Width, size.Height);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.SizeF" /> structure from the specified <see cref="T:System.Drawing.PointF" /> structure.</summary>
		/// <param name="pt">The <see cref="T:System.Drawing.PointF" /> structure from which to initialize this <see cref="T:System.Drawing.SizeF" /> structure. </param>
		// Token: 0x060006EC RID: 1772 RVA: 0x0001408D File Offset: 0x0001228D
		public SizeF(PointF pt)
		{
			this.width = pt.X;
			this.height = pt.Y;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.SizeF" /> structure from the specified existing <see cref="T:System.Drawing.SizeF" /> structure.</summary>
		/// <param name="size">The <see cref="T:System.Drawing.SizeF" /> structure from which to create the new <see cref="T:System.Drawing.SizeF" /> structure. </param>
		// Token: 0x060006ED RID: 1773 RVA: 0x000140A9 File Offset: 0x000122A9
		public SizeF(SizeF size)
		{
			this.width = size.Width;
			this.height = size.Height;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.SizeF" /> structure from the specified dimensions.</summary>
		/// <param name="width">The width component of the new <see cref="T:System.Drawing.SizeF" /> structure. </param>
		/// <param name="height">The height component of the new <see cref="T:System.Drawing.SizeF" /> structure. </param>
		// Token: 0x060006EE RID: 1774 RVA: 0x000140C5 File Offset: 0x000122C5
		public SizeF(float width, float height)
		{
			this.width = width;
			this.height = height;
		}

		/// <summary>Gets a value that indicates whether this <see cref="T:System.Drawing.SizeF" /> structure has zero width and height.</summary>
		/// <returns>This property returns true when this <see cref="T:System.Drawing.SizeF" /> structure has both a width and height of zero; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000261 RID: 609
		// (get) Token: 0x060006EF RID: 1775 RVA: 0x000140D5 File Offset: 0x000122D5
		[Browsable(false)]
		public bool IsEmpty
		{
			get
			{
				return (double)this.width == 0.0 && (double)this.height == 0.0;
			}
		}

		/// <summary>Gets or sets the horizontal component of this <see cref="T:System.Drawing.SizeF" /> structure.</summary>
		/// <returns>The horizontal component of this <see cref="T:System.Drawing.SizeF" /> structure, typically measured in pixels.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000262 RID: 610
		// (get) Token: 0x060006F0 RID: 1776 RVA: 0x000140FD File Offset: 0x000122FD
		// (set) Token: 0x060006F1 RID: 1777 RVA: 0x00014105 File Offset: 0x00012305
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

		/// <summary>Gets or sets the vertical component of this <see cref="T:System.Drawing.SizeF" /> structure.</summary>
		/// <returns>The vertical component of this <see cref="T:System.Drawing.SizeF" /> structure, typically measured in pixels.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000263 RID: 611
		// (get) Token: 0x060006F2 RID: 1778 RVA: 0x0001410E File Offset: 0x0001230E
		// (set) Token: 0x060006F3 RID: 1779 RVA: 0x00014116 File Offset: 0x00012316
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

		/// <summary>Tests to see whether the specified object is a <see cref="T:System.Drawing.SizeF" /> structure with the same dimensions as this <see cref="T:System.Drawing.SizeF" /> structure.</summary>
		/// <returns>This method returns true if <paramref name="obj" /> is a <see cref="T:System.Drawing.SizeF" /> and has the same width and height as this <see cref="T:System.Drawing.SizeF" />; otherwise, false.</returns>
		/// <param name="obj">The <see cref="T:System.Object" /> to test. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060006F4 RID: 1780 RVA: 0x0001411F File Offset: 0x0001231F
		public override bool Equals(object obj)
		{
			return obj is SizeF && this == (SizeF)obj;
		}

		/// <summary>Returns a hash code for this <see cref="T:System.Drawing.Size" /> structure.</summary>
		/// <returns>An integer value that specifies a hash value for this <see cref="T:System.Drawing.Size" /> structure.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060006F5 RID: 1781 RVA: 0x0001413C File Offset: 0x0001233C
		public override int GetHashCode()
		{
			return (int)this.width ^ (int)this.height;
		}

		/// <summary>Converts a <see cref="T:System.Drawing.SizeF" /> structure to a <see cref="T:System.Drawing.PointF" /> structure.</summary>
		/// <returns>Returns a <see cref="T:System.Drawing.PointF" /> structure.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060006F6 RID: 1782 RVA: 0x0001414D File Offset: 0x0001234D
		public PointF ToPointF()
		{
			return new PointF(this.width, this.height);
		}

		/// <summary>Converts a <see cref="T:System.Drawing.SizeF" /> structure to a <see cref="T:System.Drawing.Size" /> structure.</summary>
		/// <returns>Returns a <see cref="T:System.Drawing.Size" /> structure.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060006F7 RID: 1783 RVA: 0x00014160 File Offset: 0x00012360
		public Size ToSize()
		{
			checked
			{
				int num = (int)this.width;
				int num2 = (int)this.height;
				return new Size(num, num2);
			}
		}

		/// <summary>Creates a human-readable string that represents this <see cref="T:System.Drawing.SizeF" /> structure.</summary>
		/// <returns>A string that represents this <see cref="T:System.Drawing.SizeF" /> structure.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060006F8 RID: 1784 RVA: 0x00014182 File Offset: 0x00012382
		public override string ToString()
		{
			return string.Format("{{Width={0}, Height={1}}}", this.width.ToString(CultureInfo.CurrentCulture), this.height.ToString(CultureInfo.CurrentCulture));
		}

		/// <summary>Adds the width and height of one <see cref="T:System.Drawing.SizeF" /> structure to the width and height of another <see cref="T:System.Drawing.SizeF" /> structure.</summary>
		/// <returns>A <see cref="T:System.Drawing.SizeF" /> structure that is the result of the addition operation.</returns>
		/// <param name="sz1">The first <see cref="T:System.Drawing.SizeF" /> structure to add.</param>
		/// <param name="sz2">The second <see cref="T:System.Drawing.SizeF" /> structure to add.</param>
		// Token: 0x060006F9 RID: 1785 RVA: 0x00013FE3 File Offset: 0x000121E3
		public static SizeF Add(SizeF sz1, SizeF sz2)
		{
			return new SizeF(sz1.Width + sz2.Width, sz1.Height + sz2.Height);
		}

		/// <summary>Subtracts the width and height of one <see cref="T:System.Drawing.SizeF" /> structure from the width and height of another <see cref="T:System.Drawing.SizeF" /> structure.</summary>
		/// <returns>A <see cref="T:System.Drawing.SizeF" /> structure that is a result of the subtraction operation.</returns>
		/// <param name="sz1">The <see cref="T:System.Drawing.SizeF" /> structure on the left side of the subtraction operator. </param>
		/// <param name="sz2">The <see cref="T:System.Drawing.SizeF" /> structure on the right side of the subtraction operator. </param>
		// Token: 0x060006FA RID: 1786 RVA: 0x00014053 File Offset: 0x00012253
		public static SizeF Subtract(SizeF sz1, SizeF sz2)
		{
			return new SizeF(sz1.Width - sz2.Width, sz1.Height - sz2.Height);
		}

		// Token: 0x04000551 RID: 1361
		private float width;

		// Token: 0x04000552 RID: 1362
		private float height;

		/// <summary>Gets a <see cref="T:System.Drawing.SizeF" /> structure that has a <see cref="P:System.Drawing.SizeF.Height" /> and <see cref="P:System.Drawing.SizeF.Width" /> value of 0. </summary>
		/// <returns>A <see cref="T:System.Drawing.SizeF" /> structure that has a <see cref="P:System.Drawing.SizeF.Height" /> and <see cref="P:System.Drawing.SizeF.Width" /> value of 0.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x04000553 RID: 1363
		public static readonly SizeF Empty;
	}
}
