using System;
using System.ComponentModel;
using System.Drawing;

namespace System.Windows.Forms
{
	/// <summary>Represents padding or margin information associated with a user interface (UI) element.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000281 RID: 641
	[TypeConverter(typeof(PaddingConverter))]
	[Serializable]
	public struct Padding
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Padding" /> class using the supplied padding size for all edges.</summary>
		/// <param name="all">The number of pixels to be used for padding for all edges.</param>
		// Token: 0x060029A0 RID: 10656 RVA: 0x000A083C File Offset: 0x0009EA3C
		public Padding(int all)
		{
			this._left = all;
			this._right = all;
			this._top = all;
			this._bottom = all;
			this._all = true;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Padding" /> class using a separate padding size for each edge.</summary>
		/// <param name="left">The padding size, in pixels, for the left edge.</param>
		/// <param name="top">The padding size, in pixels, for the top edge.</param>
		/// <param name="right">The padding size, in pixels, for the right edge.</param>
		/// <param name="bottom">The padding size, in pixels, for the bottom edge.</param>
		// Token: 0x060029A1 RID: 10657 RVA: 0x000A0864 File Offset: 0x0009EA64
		public Padding(int left, int top, int right, int bottom)
		{
			this._left = left;
			this._right = right;
			this._top = top;
			this._bottom = bottom;
			this._all = this._left == this._top && this._left == this._right && this._left == this._bottom;
		}

		/// <summary>Gets or sets the padding value for all the edges.</summary>
		/// <returns>The padding, in pixels, for all edges if the same; otherwise, -1.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000A2E RID: 2606
		// (get) Token: 0x060029A3 RID: 10659 RVA: 0x000A08D8 File Offset: 0x0009EAD8
		// (set) Token: 0x060029A4 RID: 10660 RVA: 0x000A08F0 File Offset: 0x0009EAF0
		[RefreshProperties(1)]
		public int All
		{
			get
			{
				if (!this._all)
				{
					return -1;
				}
				return this._top;
			}
			set
			{
				this._all = true;
				this._bottom = value;
				this._right = value;
				this._top = value;
				this._left = value;
			}
		}

		/// <summary>Gets or sets the padding value for the bottom edge.</summary>
		/// <returns>The padding, in pixels, for the bottom edge.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000A2F RID: 2607
		// (get) Token: 0x060029A5 RID: 10661 RVA: 0x000A0928 File Offset: 0x0009EB28
		// (set) Token: 0x060029A6 RID: 10662 RVA: 0x000A0930 File Offset: 0x0009EB30
		[RefreshProperties(1)]
		public int Bottom
		{
			get
			{
				return this._bottom;
			}
			set
			{
				this._bottom = value;
				this._all = false;
			}
		}

		/// <summary>Gets the combined padding for the right and left edges.</summary>
		/// <returns>Gets the sum, in pixels, of the <see cref="P:System.Windows.Forms.Padding.Left" /> and <see cref="P:System.Windows.Forms.Padding.Right" /> padding values.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000A30 RID: 2608
		// (get) Token: 0x060029A7 RID: 10663 RVA: 0x000A0940 File Offset: 0x0009EB40
		[Browsable(false)]
		public int Horizontal
		{
			get
			{
				return this._left + this._right;
			}
		}

		/// <summary>Gets or sets the padding value for the left edge.</summary>
		/// <returns>The padding, in pixels, for the left edge.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000A31 RID: 2609
		// (get) Token: 0x060029A8 RID: 10664 RVA: 0x000A0950 File Offset: 0x0009EB50
		// (set) Token: 0x060029A9 RID: 10665 RVA: 0x000A0958 File Offset: 0x0009EB58
		[RefreshProperties(1)]
		public int Left
		{
			get
			{
				return this._left;
			}
			set
			{
				this._left = value;
				this._all = false;
			}
		}

		/// <summary>Gets or sets the padding value for the right edge.</summary>
		/// <returns>The padding, in pixels, for the right edge.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000A32 RID: 2610
		// (get) Token: 0x060029AA RID: 10666 RVA: 0x000A0968 File Offset: 0x0009EB68
		// (set) Token: 0x060029AB RID: 10667 RVA: 0x000A0970 File Offset: 0x0009EB70
		[RefreshProperties(1)]
		public int Right
		{
			get
			{
				return this._right;
			}
			set
			{
				this._right = value;
				this._all = false;
			}
		}

		/// <summary>Gets the padding information in the form of a <see cref="T:System.Drawing.Size" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> containing the padding information.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000A33 RID: 2611
		// (get) Token: 0x060029AC RID: 10668 RVA: 0x000A0980 File Offset: 0x0009EB80
		[Browsable(false)]
		public Size Size
		{
			get
			{
				return new Size(this.Horizontal, this.Vertical);
			}
		}

		/// <summary>Gets or sets the padding value for the top edge.</summary>
		/// <returns>The padding, in pixels, for the top edge.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000A34 RID: 2612
		// (get) Token: 0x060029AD RID: 10669 RVA: 0x000A0994 File Offset: 0x0009EB94
		// (set) Token: 0x060029AE RID: 10670 RVA: 0x000A099C File Offset: 0x0009EB9C
		[RefreshProperties(1)]
		public int Top
		{
			get
			{
				return this._top;
			}
			set
			{
				this._top = value;
				this._all = false;
			}
		}

		/// <summary>Gets the combined padding for the top and bottom edges.</summary>
		/// <returns>Gets the sum, in pixels, of the <see cref="P:System.Windows.Forms.Padding.Top" /> and <see cref="P:System.Windows.Forms.Padding.Bottom" /> padding values.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000A35 RID: 2613
		// (get) Token: 0x060029AF RID: 10671 RVA: 0x000A09AC File Offset: 0x0009EBAC
		[Browsable(false)]
		public int Vertical
		{
			get
			{
				return this._top + this._bottom;
			}
		}

		/// <summary>Computes the sum of the two specified <see cref="T:System.Windows.Forms.Padding" /> values.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Padding" /> that contains the sum of the two specified <see cref="T:System.Windows.Forms.Padding" /> values.</returns>
		/// <param name="p1">A <see cref="T:System.Windows.Forms.Padding" />.</param>
		/// <param name="p2">A <see cref="T:System.Windows.Forms.Padding" />.</param>
		// Token: 0x060029B0 RID: 10672 RVA: 0x000A09BC File Offset: 0x0009EBBC
		public static Padding Add(Padding p1, Padding p2)
		{
			return p1 + p2;
		}

		/// <summary>Determines whether the value of the specified object is equivalent to the current <see cref="T:System.Windows.Forms.Padding" />.</summary>
		/// <returns>true if the <see cref="T:System.Windows.Forms.Padding" /> objects are equivalent; otherwise, false.</returns>
		/// <param name="other">The object to compare to the current <see cref="T:System.Windows.Forms.Padding" />.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060029B1 RID: 10673 RVA: 0x000A09C8 File Offset: 0x0009EBC8
		public override bool Equals(object other)
		{
			if (other is Padding)
			{
				Padding padding = (Padding)other;
				return this._left == padding.Left && this._top == padding.Top && this._right == padding.Right && this._bottom == padding.Bottom;
			}
			return false;
		}

		/// <summary>Generates a hash code for the current <see cref="T:System.Windows.Forms.Padding" />. </summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060029B2 RID: 10674 RVA: 0x000A0A34 File Offset: 0x0009EC34
		public override int GetHashCode()
		{
			return this._top ^ this._bottom ^ this._left ^ this._right;
		}

		/// <summary>Subtracts one specified <see cref="T:System.Windows.Forms.Padding" /> value from another.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Padding" /> that contains the result of the subtraction of one specified <see cref="T:System.Windows.Forms.Padding" /> value from another.</returns>
		/// <param name="p1">A <see cref="T:System.Windows.Forms.Padding" />.</param>
		/// <param name="p2">A <see cref="T:System.Windows.Forms.Padding" />.</param>
		// Token: 0x060029B3 RID: 10675 RVA: 0x000A0A54 File Offset: 0x0009EC54
		public static Padding Subtract(Padding p1, Padding p2)
		{
			return p1 - p2;
		}

		/// <summary>Returns a string that represents the current <see cref="T:System.Windows.Forms.Padding" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that represents the current <see cref="T:System.Windows.Forms.Padding" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060029B4 RID: 10676 RVA: 0x000A0A60 File Offset: 0x0009EC60
		public override string ToString()
		{
			return string.Concat(new object[] { "{Left=", this.Left, ",Top=", this.Top, ",Right=", this.Right, ",Bottom=", this.Bottom, "}" });
		}

		/// <summary>Performs vector addition on the two specified <see cref="T:System.Windows.Forms.Padding" /> objects, resulting in a new <see cref="T:System.Windows.Forms.Padding" />.</summary>
		/// <returns>A new <see cref="T:System.Windows.Forms.Padding" /> that results from adding <paramref name="p1" /> and <paramref name="p2" />.</returns>
		/// <param name="p1">The first <see cref="T:System.Windows.Forms.Padding" /> to add.</param>
		/// <param name="p2">The second <see cref="T:System.Windows.Forms.Padding" /> to add.</param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x060029B5 RID: 10677 RVA: 0x000A0ADC File Offset: 0x0009ECDC
		public static Padding operator +(Padding p1, Padding p2)
		{
			return new Padding(p1.Left + p2.Left, p1.Top + p2.Top, p1.Right + p2.Right, p1.Bottom + p2.Bottom);
		}

		/// <summary>Tests whether two specified <see cref="T:System.Windows.Forms.Padding" /> objects are equivalent.</summary>
		/// <returns>true if the two <see cref="T:System.Windows.Forms.Padding" /> objects are equal; otherwise, false.</returns>
		/// <param name="p1">A <see cref="T:System.Windows.Forms.Padding" /> to test.</param>
		/// <param name="p2">A <see cref="T:System.Windows.Forms.Padding" /> to test.</param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x060029B6 RID: 10678 RVA: 0x000A0B2C File Offset: 0x0009ED2C
		public static bool operator ==(Padding p1, Padding p2)
		{
			return p1.Equals(p2);
		}

		/// <summary>Tests whether two specified <see cref="T:System.Windows.Forms.Padding" /> objects are not equivalent.</summary>
		/// <returns>true if the two <see cref="T:System.Windows.Forms.Padding" /> objects are different; otherwise, false.</returns>
		/// <param name="p1">A <see cref="T:System.Windows.Forms.Padding" /> to test.</param>
		/// <param name="p2">A <see cref="T:System.Windows.Forms.Padding" /> to test.</param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x060029B7 RID: 10679 RVA: 0x000A0B3C File Offset: 0x0009ED3C
		public static bool operator !=(Padding p1, Padding p2)
		{
			return !p1.Equals(p2);
		}

		/// <summary>Performs vector subtraction on the two specified <see cref="T:System.Windows.Forms.Padding" /> objects, resulting in a new <see cref="T:System.Windows.Forms.Padding" />.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Padding" /> result of subtracting <paramref name="p2" /> from <paramref name="p1" />.</returns>
		/// <param name="p1">The <see cref="T:System.Windows.Forms.Padding" /> to subtract from (the minuend).</param>
		/// <param name="p2">The <see cref="T:System.Windows.Forms.Padding" /> to subtract from (the subtrahend).</param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x060029B8 RID: 10680 RVA: 0x000A0B50 File Offset: 0x0009ED50
		public static Padding operator -(Padding p1, Padding p2)
		{
			return new Padding(p1.Left - p2.Left, p1.Top - p2.Top, p1.Right - p2.Right, p1.Bottom - p2.Bottom);
		}

		// Token: 0x040014A8 RID: 5288
		private int _bottom;

		// Token: 0x040014A9 RID: 5289
		private int _left;

		// Token: 0x040014AA RID: 5290
		private int _right;

		// Token: 0x040014AB RID: 5291
		private int _top;

		// Token: 0x040014AC RID: 5292
		private bool _all;

		/// <summary>Provides a <see cref="T:System.Windows.Forms.Padding" /> object with no padding.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x040014AD RID: 5293
		public static readonly Padding Empty = new Padding(0);
	}
}
