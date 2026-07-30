using System;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.Serialization;

namespace System.Drawing.Printing
{
	/// <summary>Specifies the dimensions of the margins of a printed page.</summary>
	// Token: 0x020000B5 RID: 181
	[TypeConverter(typeof(MarginsConverter))]
	[Serializable]
	public class Margins : ICloneable
	{
		// Token: 0x06000A48 RID: 2632 RVA: 0x00016504 File Offset: 0x00014704
		[OnDeserialized]
		private void OnDeserializedMethod(StreamingContext context)
		{
			if (this._doubleLeft == 0.0 && this._left != 0)
			{
				this._doubleLeft = (double)this._left;
			}
			if (this._doubleRight == 0.0 && this._right != 0)
			{
				this._doubleRight = (double)this._right;
			}
			if (this._doubleTop == 0.0 && this._top != 0)
			{
				this._doubleTop = (double)this._top;
			}
			if (this._doubleBottom == 0.0 && this._bottom != 0)
			{
				this._doubleBottom = (double)this._bottom;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Printing.Margins" /> class with 1-inch wide margins.</summary>
		// Token: 0x06000A49 RID: 2633 RVA: 0x000165A9 File Offset: 0x000147A9
		public Margins()
			: this(100, 100, 100, 100)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Printing.Margins" /> class with the specified left, right, top, and bottom margins.</summary>
		/// <param name="left">The left margin, in hundredths of an inch. </param>
		/// <param name="right">The right margin, in hundredths of an inch. </param>
		/// <param name="top">The top margin, in hundredths of an inch. </param>
		/// <param name="bottom">The bottom margin, in hundredths of an inch. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="left" /> parameter value is less than 0.-or- The <paramref name="right" /> parameter value is less than 0.-or- The <paramref name="top" /> parameter value is less than 0.-or- The <paramref name="bottom" /> parameter value is less than 0. </exception>
		// Token: 0x06000A4A RID: 2634 RVA: 0x000165BC File Offset: 0x000147BC
		public Margins(int left, int right, int top, int bottom)
		{
			this.CheckMargin(left, "left");
			this.CheckMargin(right, "right");
			this.CheckMargin(top, "top");
			this.CheckMargin(bottom, "bottom");
			this._left = left;
			this._right = right;
			this._top = top;
			this._bottom = bottom;
			this._doubleLeft = (double)left;
			this._doubleRight = (double)right;
			this._doubleTop = (double)top;
			this._doubleBottom = (double)bottom;
		}

		/// <summary>Gets or sets the left margin width, in hundredths of an inch.</summary>
		/// <returns>The left margin width, in hundredths of an inch.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Drawing.Printing.Margins.Left" /> property is set to a value that is less than 0. </exception>
		// Token: 0x170002CB RID: 715
		// (get) Token: 0x06000A4B RID: 2635 RVA: 0x0001663E File Offset: 0x0001483E
		// (set) Token: 0x06000A4C RID: 2636 RVA: 0x00016646 File Offset: 0x00014846
		public int Left
		{
			get
			{
				return this._left;
			}
			set
			{
				this.CheckMargin(value, "Left");
				this._left = value;
				this._doubleLeft = (double)value;
			}
		}

		/// <summary>Gets or sets the right margin width, in hundredths of an inch.</summary>
		/// <returns>The right margin width, in hundredths of an inch.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Drawing.Printing.Margins.Right" /> property is set to a value that is less than 0. </exception>
		// Token: 0x170002CC RID: 716
		// (get) Token: 0x06000A4D RID: 2637 RVA: 0x00016663 File Offset: 0x00014863
		// (set) Token: 0x06000A4E RID: 2638 RVA: 0x0001666B File Offset: 0x0001486B
		public int Right
		{
			get
			{
				return this._right;
			}
			set
			{
				this.CheckMargin(value, "Right");
				this._right = value;
				this._doubleRight = (double)value;
			}
		}

		/// <summary>Gets or sets the top margin width, in hundredths of an inch.</summary>
		/// <returns>The top margin width, in hundredths of an inch.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Drawing.Printing.Margins.Top" /> property is set to a value that is less than 0. </exception>
		// Token: 0x170002CD RID: 717
		// (get) Token: 0x06000A4F RID: 2639 RVA: 0x00016688 File Offset: 0x00014888
		// (set) Token: 0x06000A50 RID: 2640 RVA: 0x00016690 File Offset: 0x00014890
		public int Top
		{
			get
			{
				return this._top;
			}
			set
			{
				this.CheckMargin(value, "Top");
				this._top = value;
				this._doubleTop = (double)value;
			}
		}

		/// <summary>Gets or sets the bottom margin, in hundredths of an inch.</summary>
		/// <returns>The bottom margin, in hundredths of an inch.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Drawing.Printing.Margins.Bottom" /> property is set to a value that is less than 0. </exception>
		// Token: 0x170002CE RID: 718
		// (get) Token: 0x06000A51 RID: 2641 RVA: 0x000166AD File Offset: 0x000148AD
		// (set) Token: 0x06000A52 RID: 2642 RVA: 0x000166B5 File Offset: 0x000148B5
		public int Bottom
		{
			get
			{
				return this._bottom;
			}
			set
			{
				this.CheckMargin(value, "Bottom");
				this._bottom = value;
				this._doubleBottom = (double)value;
			}
		}

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x06000A53 RID: 2643 RVA: 0x000166D2 File Offset: 0x000148D2
		// (set) Token: 0x06000A54 RID: 2644 RVA: 0x000166DA File Offset: 0x000148DA
		internal double DoubleLeft
		{
			get
			{
				return this._doubleLeft;
			}
			set
			{
				this.Left = (int)Math.Round(value);
				this._doubleLeft = value;
			}
		}

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x06000A55 RID: 2645 RVA: 0x000166F0 File Offset: 0x000148F0
		// (set) Token: 0x06000A56 RID: 2646 RVA: 0x000166F8 File Offset: 0x000148F8
		internal double DoubleRight
		{
			get
			{
				return this._doubleRight;
			}
			set
			{
				this.Right = (int)Math.Round(value);
				this._doubleRight = value;
			}
		}

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x06000A57 RID: 2647 RVA: 0x0001670E File Offset: 0x0001490E
		// (set) Token: 0x06000A58 RID: 2648 RVA: 0x00016716 File Offset: 0x00014916
		internal double DoubleTop
		{
			get
			{
				return this._doubleTop;
			}
			set
			{
				this.Top = (int)Math.Round(value);
				this._doubleTop = value;
			}
		}

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x06000A59 RID: 2649 RVA: 0x0001672C File Offset: 0x0001492C
		// (set) Token: 0x06000A5A RID: 2650 RVA: 0x00016734 File Offset: 0x00014934
		internal double DoubleBottom
		{
			get
			{
				return this._doubleBottom;
			}
			set
			{
				this.Bottom = (int)Math.Round(value);
				this._doubleBottom = value;
			}
		}

		// Token: 0x06000A5B RID: 2651 RVA: 0x0001674A File Offset: 0x0001494A
		private void CheckMargin(int margin, string name)
		{
			if (margin < 0)
			{
				throw new ArgumentException(SR.Format("Value of '{1}' is not valid for '{0}'. '{0}' must be greater than or equal to {2}.", new object[] { name, margin, "0" }));
			}
		}

		/// <summary>Retrieves a duplicate of this object, member by member.</summary>
		/// <returns>A duplicate of this object.</returns>
		// Token: 0x06000A5C RID: 2652 RVA: 0x0001677B File Offset: 0x0001497B
		public object Clone()
		{
			return base.MemberwiseClone();
		}

		/// <summary>Compares this <see cref="T:System.Drawing.Printing.Margins" /> to the specified <see cref="T:System.Object" /> to determine whether they have the same dimensions.</summary>
		/// <returns>true if the specified object is a <see cref="T:System.Drawing.Printing.Margins" /> and has the same <see cref="P:System.Drawing.Printing.Margins.Top" />, <see cref="P:System.Drawing.Printing.Margins.Bottom" />, <see cref="P:System.Drawing.Printing.Margins.Right" /> and <see cref="P:System.Drawing.Printing.Margins.Left" /> values as this <see cref="T:System.Drawing.Printing.Margins" />; otherwise, false.</returns>
		/// <param name="obj">The object to which to compare this <see cref="T:System.Drawing.Printing.Margins" />. </param>
		// Token: 0x06000A5D RID: 2653 RVA: 0x00016784 File Offset: 0x00014984
		public override bool Equals(object obj)
		{
			Margins margins = obj as Margins;
			return margins == this || (!(margins == null) && (margins.Left == this.Left && margins.Right == this.Right && margins.Top == this.Top) && margins.Bottom == this.Bottom);
		}

		/// <summary>Calculates and retrieves a hash code based on the width of the left, right, top, and bottom margins.</summary>
		/// <returns>A hash code based on the left, right, top, and bottom margins.</returns>
		// Token: 0x06000A5E RID: 2654 RVA: 0x000167E8 File Offset: 0x000149E8
		public override int GetHashCode()
		{
			int left = this.Left;
			uint right = (uint)this.Right;
			uint top = (uint)this.Top;
			uint bottom = (uint)this.Bottom;
			return left ^ (int)((right << 13) | (right >> 19)) ^ (int)((top << 26) | (top >> 6)) ^ (int)((bottom << 7) | (bottom >> 25));
		}

		/// <summary>Compares two <see cref="T:System.Drawing.Printing.Margins" /> to determine if they have the same dimensions.</summary>
		/// <returns>true to indicate the <see cref="P:System.Drawing.Printing.Margins.Left" />, <see cref="P:System.Drawing.Printing.Margins.Right" />, <see cref="P:System.Drawing.Printing.Margins.Top" />, and <see cref="P:System.Drawing.Printing.Margins.Bottom" /> properties of both margins have the same value; otherwise, false.</returns>
		/// <param name="m1">The first <see cref="T:System.Drawing.Printing.Margins" /> to compare for equality.</param>
		/// <param name="m2">The second <see cref="T:System.Drawing.Printing.Margins" /> to compare for equality.</param>
		// Token: 0x06000A5F RID: 2655 RVA: 0x0001682C File Offset: 0x00014A2C
		public static bool operator ==(Margins m1, Margins m2)
		{
			return m1 == null == (m2 == null) && (m1 == null || (m1.Left == m2.Left && m1.Top == m2.Top && m1.Right == m2.Right && m1.Bottom == m2.Bottom));
		}

		/// <summary>Compares two <see cref="T:System.Drawing.Printing.Margins" /> to determine whether they are of unequal width.</summary>
		/// <returns>true to indicate if the <see cref="P:System.Drawing.Printing.Margins.Left" />, <see cref="P:System.Drawing.Printing.Margins.Right" />, <see cref="P:System.Drawing.Printing.Margins.Top" />, or <see cref="P:System.Drawing.Printing.Margins.Bottom" /> properties of both margins are not equal; otherwise, false.</returns>
		/// <param name="m1">The first <see cref="T:System.Drawing.Printing.Margins" /> to compare for inequality.</param>
		/// <param name="m2">The second <see cref="T:System.Drawing.Printing.Margins" /> to compare for inequality.</param>
		// Token: 0x06000A60 RID: 2656 RVA: 0x00016884 File Offset: 0x00014A84
		public static bool operator !=(Margins m1, Margins m2)
		{
			return !(m1 == m2);
		}

		/// <summary>Converts the <see cref="T:System.Drawing.Printing.Margins" /> to a string.</summary>
		/// <returns>A <see cref="T:System.String" /> representation of the <see cref="T:System.Drawing.Printing.Margins" />. </returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06000A61 RID: 2657 RVA: 0x00016890 File Offset: 0x00014A90
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"[Margins Left=",
				this.Left.ToString(CultureInfo.InvariantCulture),
				" Right=",
				this.Right.ToString(CultureInfo.InvariantCulture),
				" Top=",
				this.Top.ToString(CultureInfo.InvariantCulture),
				" Bottom=",
				this.Bottom.ToString(CultureInfo.InvariantCulture),
				"]"
			});
		}

		// Token: 0x04000648 RID: 1608
		private int _left;

		// Token: 0x04000649 RID: 1609
		private int _right;

		// Token: 0x0400064A RID: 1610
		private int _bottom;

		// Token: 0x0400064B RID: 1611
		private int _top;

		// Token: 0x0400064C RID: 1612
		[OptionalField]
		private double _doubleLeft;

		// Token: 0x0400064D RID: 1613
		[OptionalField]
		private double _doubleRight;

		// Token: 0x0400064E RID: 1614
		[OptionalField]
		private double _doubleTop;

		// Token: 0x0400064F RID: 1615
		[OptionalField]
		private double _doubleBottom;
	}
}
