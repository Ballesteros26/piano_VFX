using System;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Drawing
{
	/// <summary>Represents an ordered pair of floating-point x- and y-coordinates that defines a point in a two-dimensional plane.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200007E RID: 126
	[ComVisible(true)]
	[Serializable]
	public struct PointF
	{
		/// <summary>Translates a <see cref="T:System.Drawing.PointF" /> by a given <see cref="T:System.Drawing.Size" />.</summary>
		/// <returns>Returns the translated <see cref="T:System.Drawing.PointF" />.</returns>
		/// <param name="pt">The <see cref="T:System.Drawing.PointF" /> to translate. </param>
		/// <param name="sz">A <see cref="T:System.Drawing.Size" /> that specifies the pair of numbers to add to the coordinates of <paramref name="pt" />. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x0600061E RID: 1566 RVA: 0x0001217B File Offset: 0x0001037B
		public static PointF operator +(PointF pt, Size sz)
		{
			return new PointF(pt.X + (float)sz.Width, pt.Y + (float)sz.Height);
		}

		/// <summary>Translates the <see cref="T:System.Drawing.PointF" /> by the specified <see cref="T:System.Drawing.SizeF" />.</summary>
		/// <returns>The translated <see cref="T:System.Drawing.PointF" />.</returns>
		/// <param name="pt">The <see cref="T:System.Drawing.PointF" /> to translate.</param>
		/// <param name="sz">The <see cref="T:System.Drawing.SizeF" /> that specifies the numbers to add to the x- and y-coordinates of the <see cref="T:System.Drawing.PointF" />.</param>
		// Token: 0x0600061F RID: 1567 RVA: 0x000121A2 File Offset: 0x000103A2
		public static PointF operator +(PointF pt, SizeF sz)
		{
			return new PointF(pt.X + sz.Width, pt.Y + sz.Height);
		}

		/// <summary>Compares two <see cref="T:System.Drawing.PointF" /> structures. The result specifies whether the values of the <see cref="P:System.Drawing.PointF.X" /> and <see cref="P:System.Drawing.PointF.Y" /> properties of the two <see cref="T:System.Drawing.PointF" /> structures are equal.</summary>
		/// <returns>true if the <see cref="P:System.Drawing.PointF.X" /> and <see cref="P:System.Drawing.PointF.Y" /> values of the left and right <see cref="T:System.Drawing.PointF" /> structures are equal; otherwise, false.</returns>
		/// <param name="left">A <see cref="T:System.Drawing.PointF" /> to compare. </param>
		/// <param name="right">A <see cref="T:System.Drawing.PointF" /> to compare. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06000620 RID: 1568 RVA: 0x000121C7 File Offset: 0x000103C7
		public static bool operator ==(PointF left, PointF right)
		{
			return left.X == right.X && left.Y == right.Y;
		}

		/// <summary>Determines whether the coordinates of the specified points are not equal.</summary>
		/// <returns>true to indicate the <see cref="P:System.Drawing.PointF.X" /> and <see cref="P:System.Drawing.PointF.Y" /> values of <paramref name="left" /> and <paramref name="right" /> are not equal; otherwise, false. </returns>
		/// <param name="left">A <see cref="T:System.Drawing.PointF" /> to compare.</param>
		/// <param name="right">A <see cref="T:System.Drawing.PointF" /> to compare.</param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06000621 RID: 1569 RVA: 0x000121EB File Offset: 0x000103EB
		public static bool operator !=(PointF left, PointF right)
		{
			return left.X != right.X || left.Y != right.Y;
		}

		/// <summary>Translates a <see cref="T:System.Drawing.PointF" /> by the negative of a given <see cref="T:System.Drawing.Size" />.</summary>
		/// <returns>The translated <see cref="T:System.Drawing.PointF" />.</returns>
		/// <param name="pt">The <see cref="T:System.Drawing.PointF" /> to translate.</param>
		/// <param name="sz">The <see cref="T:System.Drawing.Size" /> that specifies the numbers to subtract from the coordinates of <paramref name="pt" />.</param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06000622 RID: 1570 RVA: 0x00012212 File Offset: 0x00010412
		public static PointF operator -(PointF pt, Size sz)
		{
			return new PointF(pt.X - (float)sz.Width, pt.Y - (float)sz.Height);
		}

		/// <summary>Translates a <see cref="T:System.Drawing.PointF" /> by the negative of a specified <see cref="T:System.Drawing.SizeF" />. </summary>
		/// <returns>The translated <see cref="T:System.Drawing.PointF" />.</returns>
		/// <param name="pt">The <see cref="T:System.Drawing.PointF" /> to translate.</param>
		/// <param name="sz">The <see cref="T:System.Drawing.SizeF" /> that specifies the numbers to subtract from the coordinates of <paramref name="pt" />.</param>
		// Token: 0x06000623 RID: 1571 RVA: 0x00012239 File Offset: 0x00010439
		public static PointF operator -(PointF pt, SizeF sz)
		{
			return new PointF(pt.X - sz.Width, pt.Y - sz.Height);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.PointF" /> class with the specified coordinates.</summary>
		/// <param name="x">The horizontal position of the point. </param>
		/// <param name="y">The vertical position of the point. </param>
		// Token: 0x06000624 RID: 1572 RVA: 0x0001225E File Offset: 0x0001045E
		public PointF(float x, float y)
		{
			this.x = x;
			this.y = y;
		}

		/// <summary>Gets a value indicating whether this <see cref="T:System.Drawing.PointF" /> is empty.</summary>
		/// <returns>true if both <see cref="P:System.Drawing.PointF.X" /> and <see cref="P:System.Drawing.PointF.Y" /> are 0; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000243 RID: 579
		// (get) Token: 0x06000625 RID: 1573 RVA: 0x0001226E File Offset: 0x0001046E
		[Browsable(false)]
		public bool IsEmpty
		{
			get
			{
				return (double)this.x == 0.0 && (double)this.y == 0.0;
			}
		}

		/// <summary>Gets or sets the x-coordinate of this <see cref="T:System.Drawing.PointF" />.</summary>
		/// <returns>The x-coordinate of this <see cref="T:System.Drawing.PointF" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000244 RID: 580
		// (get) Token: 0x06000626 RID: 1574 RVA: 0x00012296 File Offset: 0x00010496
		// (set) Token: 0x06000627 RID: 1575 RVA: 0x0001229E File Offset: 0x0001049E
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

		/// <summary>Gets or sets the y-coordinate of this <see cref="T:System.Drawing.PointF" />.</summary>
		/// <returns>The y-coordinate of this <see cref="T:System.Drawing.PointF" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000245 RID: 581
		// (get) Token: 0x06000628 RID: 1576 RVA: 0x000122A7 File Offset: 0x000104A7
		// (set) Token: 0x06000629 RID: 1577 RVA: 0x000122AF File Offset: 0x000104AF
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

		/// <summary>Specifies whether this <see cref="T:System.Drawing.PointF" /> contains the same coordinates as the specified <see cref="T:System.Object" />.</summary>
		/// <returns>This method returns true if <paramref name="obj" /> is a <see cref="T:System.Drawing.PointF" /> and has the same coordinates as this <see cref="T:System.Drawing.Point" />.</returns>
		/// <param name="obj">The <see cref="T:System.Object" /> to test. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600062A RID: 1578 RVA: 0x000122B8 File Offset: 0x000104B8
		public override bool Equals(object obj)
		{
			return obj is PointF && this == (PointF)obj;
		}

		/// <summary>Returns a hash code for this <see cref="T:System.Drawing.PointF" /> structure.</summary>
		/// <returns>An integer value that specifies a hash value for this <see cref="T:System.Drawing.PointF" /> structure.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600062B RID: 1579 RVA: 0x000122D5 File Offset: 0x000104D5
		public override int GetHashCode()
		{
			return (int)this.x ^ (int)this.y;
		}

		/// <summary>Converts this <see cref="T:System.Drawing.PointF" /> to a human readable string.</summary>
		/// <returns>A string that represents this <see cref="T:System.Drawing.PointF" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600062C RID: 1580 RVA: 0x000122E6 File Offset: 0x000104E6
		public override string ToString()
		{
			return string.Format("{{X={0}, Y={1}}}", this.x.ToString(CultureInfo.CurrentCulture), this.y.ToString(CultureInfo.CurrentCulture));
		}

		/// <summary>Translates a given <see cref="T:System.Drawing.PointF" /> by the specified <see cref="T:System.Drawing.Size" />.</summary>
		/// <returns>The translated <see cref="T:System.Drawing.PointF" />.</returns>
		/// <param name="pt">The <see cref="T:System.Drawing.PointF" /> to translate.</param>
		/// <param name="sz">The <see cref="T:System.Drawing.Size" /> that specifies the numbers to add to the coordinates of <paramref name="pt" />.</param>
		// Token: 0x0600062D RID: 1581 RVA: 0x0001217B File Offset: 0x0001037B
		public static PointF Add(PointF pt, Size sz)
		{
			return new PointF(pt.X + (float)sz.Width, pt.Y + (float)sz.Height);
		}

		/// <summary>Translates a given <see cref="T:System.Drawing.PointF" /> by a specified <see cref="T:System.Drawing.SizeF" />.</summary>
		/// <returns>The translated <see cref="T:System.Drawing.PointF" />.</returns>
		/// <param name="pt">The <see cref="T:System.Drawing.PointF" /> to translate.</param>
		/// <param name="sz">The <see cref="T:System.Drawing.SizeF" /> that specifies the numbers to add to the coordinates of <paramref name="pt" />.</param>
		// Token: 0x0600062E RID: 1582 RVA: 0x000121A2 File Offset: 0x000103A2
		public static PointF Add(PointF pt, SizeF sz)
		{
			return new PointF(pt.X + sz.Width, pt.Y + sz.Height);
		}

		/// <summary>Translates a <see cref="T:System.Drawing.PointF" /> by the negative of a specified size.</summary>
		/// <returns>The translated <see cref="T:System.Drawing.PointF" />.</returns>
		/// <param name="pt">The <see cref="T:System.Drawing.PointF" /> to translate.</param>
		/// <param name="sz">The <see cref="T:System.Drawing.Size" /> that specifies the numbers to subtract from the coordinates of <paramref name="pt" />.</param>
		// Token: 0x0600062F RID: 1583 RVA: 0x00012212 File Offset: 0x00010412
		public static PointF Subtract(PointF pt, Size sz)
		{
			return new PointF(pt.X - (float)sz.Width, pt.Y - (float)sz.Height);
		}

		/// <summary>Translates a <see cref="T:System.Drawing.PointF" /> by the negative of a specified size.</summary>
		/// <returns>The translated <see cref="T:System.Drawing.PointF" />.</returns>
		/// <param name="pt">The <see cref="T:System.Drawing.PointF" /> to translate.</param>
		/// <param name="sz">The <see cref="T:System.Drawing.SizeF" /> that specifies the numbers to subtract from the coordinates of <paramref name="pt" />.</param>
		// Token: 0x06000630 RID: 1584 RVA: 0x00012239 File Offset: 0x00010439
		public static PointF Subtract(PointF pt, SizeF sz)
		{
			return new PointF(pt.X - sz.Width, pt.Y - sz.Height);
		}

		// Token: 0x0400053F RID: 1343
		private float x;

		// Token: 0x04000540 RID: 1344
		private float y;

		/// <summary>Represents a new instance of the <see cref="T:System.Drawing.PointF" /> class with member data left uninitialized.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x04000541 RID: 1345
		public static readonly PointF Empty;
	}
}
