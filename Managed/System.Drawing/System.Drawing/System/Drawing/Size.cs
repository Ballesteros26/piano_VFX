using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace System.Drawing
{
	/// <summary>Stores an ordered pair of integers, which specify a <see cref="P:System.Drawing.Size.Height" /> and <see cref="P:System.Drawing.Size.Width" />.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000084 RID: 132
	[ComVisible(true)]
	[TypeConverter(typeof(SizeConverter))]
	[Serializable]
	public struct Size
	{
		/// <summary>Converts the specified <see cref="T:System.Drawing.SizeF" /> structure to a <see cref="T:System.Drawing.Size" /> structure by rounding the values of the <see cref="T:System.Drawing.Size" /> structure to the next higher integer values.</summary>
		/// <returns>The <see cref="T:System.Drawing.Size" /> structure this method converts to.</returns>
		/// <param name="value">The <see cref="T:System.Drawing.SizeF" /> structure to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060006C9 RID: 1737 RVA: 0x00013BF4 File Offset: 0x00011DF4
		public static Size Ceiling(SizeF value)
		{
			checked
			{
				int num = (int)Math.Ceiling((double)value.Width);
				int num2 = (int)Math.Ceiling((double)value.Height);
				return new Size(num, num2);
			}
		}

		/// <summary>Converts the specified <see cref="T:System.Drawing.SizeF" /> structure to a <see cref="T:System.Drawing.Size" /> structure by rounding the values of the <see cref="T:System.Drawing.SizeF" /> structure to the nearest integer values.</summary>
		/// <returns>The <see cref="T:System.Drawing.Size" /> structure this method converts to.</returns>
		/// <param name="value">The <see cref="T:System.Drawing.SizeF" /> structure to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060006CA RID: 1738 RVA: 0x00013C24 File Offset: 0x00011E24
		public static Size Round(SizeF value)
		{
			checked
			{
				int num = (int)Math.Round((double)value.Width);
				int num2 = (int)Math.Round((double)value.Height);
				return new Size(num, num2);
			}
		}

		/// <summary>Converts the specified <see cref="T:System.Drawing.SizeF" /> structure to a <see cref="T:System.Drawing.Size" /> structure by truncating the values of the <see cref="T:System.Drawing.SizeF" /> structure to the next lower integer values.</summary>
		/// <returns>The <see cref="T:System.Drawing.Size" /> structure this method converts to.</returns>
		/// <param name="value">The <see cref="T:System.Drawing.SizeF" /> structure to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060006CB RID: 1739 RVA: 0x00013C54 File Offset: 0x00011E54
		public static Size Truncate(SizeF value)
		{
			checked
			{
				int num = (int)value.Width;
				int num2 = (int)value.Height;
				return new Size(num, num2);
			}
		}

		/// <summary>Adds the width and height of one <see cref="T:System.Drawing.Size" /> structure to the width and height of another <see cref="T:System.Drawing.Size" /> structure.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> structure that is the result of the addition operation.</returns>
		/// <param name="sz1">The first <see cref="T:System.Drawing.Size" /> to add. </param>
		/// <param name="sz2">The second <see cref="T:System.Drawing.Size" /> to add. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x060006CC RID: 1740 RVA: 0x00013C78 File Offset: 0x00011E78
		public static Size operator +(Size sz1, Size sz2)
		{
			return new Size(sz1.Width + sz2.Width, sz1.Height + sz2.Height);
		}

		/// <summary>Tests whether two <see cref="T:System.Drawing.Size" /> structures are equal.</summary>
		/// <returns>true if <paramref name="sz1" /> and <paramref name="sz2" /> have equal width and height; otherwise, false.</returns>
		/// <param name="sz1">The <see cref="T:System.Drawing.Size" /> structure on the left side of the equality operator. </param>
		/// <param name="sz2">The <see cref="T:System.Drawing.Size" /> structure on the right of the equality operator. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x060006CD RID: 1741 RVA: 0x00013C9D File Offset: 0x00011E9D
		public static bool operator ==(Size sz1, Size sz2)
		{
			return sz1.Width == sz2.Width && sz1.Height == sz2.Height;
		}

		/// <summary>Tests whether two <see cref="T:System.Drawing.Size" /> structures are different.</summary>
		/// <returns>true if <paramref name="sz1" /> and <paramref name="sz2" /> differ either in width or height; false if <paramref name="sz1" /> and <paramref name="sz2" /> are equal.</returns>
		/// <param name="sz1">The <see cref="T:System.Drawing.Size" /> structure on the left of the inequality operator. </param>
		/// <param name="sz2">The <see cref="T:System.Drawing.Size" /> structure on the right of the inequality operator. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x060006CE RID: 1742 RVA: 0x00013CC1 File Offset: 0x00011EC1
		public static bool operator !=(Size sz1, Size sz2)
		{
			return sz1.Width != sz2.Width || sz1.Height != sz2.Height;
		}

		/// <summary>Subtracts the width and height of one <see cref="T:System.Drawing.Size" /> structure from the width and height of another <see cref="T:System.Drawing.Size" /> structure.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> structure that is the result of the subtraction operation.</returns>
		/// <param name="sz1">The <see cref="T:System.Drawing.Size" /> structure on the left side of the subtraction operator. </param>
		/// <param name="sz2">The <see cref="T:System.Drawing.Size" /> structure on the right side of the subtraction operator. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x060006CF RID: 1743 RVA: 0x00013CE8 File Offset: 0x00011EE8
		public static Size operator -(Size sz1, Size sz2)
		{
			return new Size(sz1.Width - sz2.Width, sz1.Height - sz2.Height);
		}

		/// <summary>Converts the specified <see cref="T:System.Drawing.Size" /> structure to a <see cref="T:System.Drawing.Point" /> structure.</summary>
		/// <returns>The <see cref="T:System.Drawing.Point" /> structure to which this operator converts.</returns>
		/// <param name="size">The <see cref="T:System.Drawing.Size" /> structure to convert. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x060006D0 RID: 1744 RVA: 0x00013D0D File Offset: 0x00011F0D
		public static explicit operator Point(Size size)
		{
			return new Point(size.Width, size.Height);
		}

		/// <summary>Converts the specified <see cref="T:System.Drawing.Size" /> structure to a <see cref="T:System.Drawing.SizeF" /> structure.</summary>
		/// <returns>The <see cref="T:System.Drawing.SizeF" /> structure to which this operator converts.</returns>
		/// <param name="p">The <see cref="T:System.Drawing.Size" /> structure to convert. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x060006D1 RID: 1745 RVA: 0x00013D22 File Offset: 0x00011F22
		public static implicit operator SizeF(Size p)
		{
			return new SizeF((float)p.Width, (float)p.Height);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Size" /> structure from the specified <see cref="T:System.Drawing.Point" /> structure.</summary>
		/// <param name="pt">The <see cref="T:System.Drawing.Point" /> structure from which to initialize this <see cref="T:System.Drawing.Size" /> structure. </param>
		// Token: 0x060006D2 RID: 1746 RVA: 0x00013D39 File Offset: 0x00011F39
		public Size(Point pt)
		{
			this.width = pt.X;
			this.height = pt.Y;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Size" /> structure from the specified dimensions.</summary>
		/// <param name="width">The width component of the new <see cref="T:System.Drawing.Size" />. </param>
		/// <param name="height">The height component of the new <see cref="T:System.Drawing.Size" />. </param>
		// Token: 0x060006D3 RID: 1747 RVA: 0x00013D55 File Offset: 0x00011F55
		public Size(int width, int height)
		{
			this.width = width;
			this.height = height;
		}

		/// <summary>Tests whether this <see cref="T:System.Drawing.Size" /> structure has width and height of 0.</summary>
		/// <returns>This property returns true when this <see cref="T:System.Drawing.Size" /> structure has both a width and height of 0; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700025E RID: 606
		// (get) Token: 0x060006D4 RID: 1748 RVA: 0x00013D65 File Offset: 0x00011F65
		[Browsable(false)]
		public bool IsEmpty
		{
			get
			{
				return this.width == 0 && this.height == 0;
			}
		}

		/// <summary>Gets or sets the horizontal component of this <see cref="T:System.Drawing.Size" /> structure.</summary>
		/// <returns>The horizontal component of this <see cref="T:System.Drawing.Size" /> structure, typically measured in pixels.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700025F RID: 607
		// (get) Token: 0x060006D5 RID: 1749 RVA: 0x00013D7A File Offset: 0x00011F7A
		// (set) Token: 0x060006D6 RID: 1750 RVA: 0x00013D82 File Offset: 0x00011F82
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

		/// <summary>Gets or sets the vertical component of this <see cref="T:System.Drawing.Size" /> structure.</summary>
		/// <returns>The vertical component of this <see cref="T:System.Drawing.Size" /> structure, typically measured in pixels.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000260 RID: 608
		// (get) Token: 0x060006D7 RID: 1751 RVA: 0x00013D8B File Offset: 0x00011F8B
		// (set) Token: 0x060006D8 RID: 1752 RVA: 0x00013D93 File Offset: 0x00011F93
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

		/// <summary>Tests to see whether the specified object is a <see cref="T:System.Drawing.Size" /> structure with the same dimensions as this <see cref="T:System.Drawing.Size" /> structure.</summary>
		/// <returns>true if <paramref name="obj" /> is a <see cref="T:System.Drawing.Size" /> and has the same width and height as this <see cref="T:System.Drawing.Size" />; otherwise, false.</returns>
		/// <param name="obj">The <see cref="T:System.Object" /> to test. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060006D9 RID: 1753 RVA: 0x00013D9C File Offset: 0x00011F9C
		public override bool Equals(object obj)
		{
			return obj is Size && this == (Size)obj;
		}

		/// <summary>Returns a hash code for this <see cref="T:System.Drawing.Size" /> structure.</summary>
		/// <returns>An integer value that specifies a hash value for this <see cref="T:System.Drawing.Size" /> structure.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060006DA RID: 1754 RVA: 0x00013DB9 File Offset: 0x00011FB9
		public override int GetHashCode()
		{
			return this.width ^ this.height;
		}

		/// <summary>Creates a human-readable string that represents this <see cref="T:System.Drawing.Size" /> structure.</summary>
		/// <returns>A string that represents this <see cref="T:System.Drawing.Size" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060006DB RID: 1755 RVA: 0x00013DC8 File Offset: 0x00011FC8
		public override string ToString()
		{
			return string.Format("{{Width={0}, Height={1}}}", this.width, this.height);
		}

		/// <summary>Adds the width and height of one <see cref="T:System.Drawing.Size" /> structure to the width and height of another <see cref="T:System.Drawing.Size" /> structure.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> structure that is the result of the addition operation.</returns>
		/// <param name="sz1">The first <see cref="T:System.Drawing.Size" /> structure to add.</param>
		/// <param name="sz2">The second <see cref="T:System.Drawing.Size" /> structure to add.</param>
		// Token: 0x060006DC RID: 1756 RVA: 0x00013C78 File Offset: 0x00011E78
		public static Size Add(Size sz1, Size sz2)
		{
			return new Size(sz1.Width + sz2.Width, sz1.Height + sz2.Height);
		}

		/// <summary>Subtracts the width and height of one <see cref="T:System.Drawing.Size" /> structure from the width and height of another <see cref="T:System.Drawing.Size" /> structure.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> structure that is a result of the subtraction operation.</returns>
		/// <param name="sz1">The <see cref="T:System.Drawing.Size" /> structure on the left side of the subtraction operator. </param>
		/// <param name="sz2">The <see cref="T:System.Drawing.Size" /> structure on the right side of the subtraction operator. </param>
		// Token: 0x060006DD RID: 1757 RVA: 0x00013CE8 File Offset: 0x00011EE8
		public static Size Subtract(Size sz1, Size sz2)
		{
			return new Size(sz1.Width - sz2.Width, sz1.Height - sz2.Height);
		}

		// Token: 0x0400054E RID: 1358
		private int width;

		// Token: 0x0400054F RID: 1359
		private int height;

		/// <summary>Gets a <see cref="T:System.Drawing.Size" /> structure that has a <see cref="P:System.Drawing.Size.Height" /> and <see cref="P:System.Drawing.Size.Width" /> value of 0. </summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that has a <see cref="P:System.Drawing.Size.Height" /> and <see cref="P:System.Drawing.Size.Width" /> value of 0.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x04000550 RID: 1360
		public static readonly Size Empty;
	}
}
