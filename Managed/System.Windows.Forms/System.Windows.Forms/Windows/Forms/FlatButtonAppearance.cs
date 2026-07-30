using System;
using System.ComponentModel;
using System.Drawing;

namespace System.Windows.Forms
{
	/// <summary>Provides properties that specify the appearance of <see cref="T:System.Windows.Forms.Button" /> controls whose <see cref="T:System.Windows.Forms.FlatStyle" /> is <see cref="F:System.Windows.Forms.FlatStyle.Flat" />.</summary>
	// Token: 0x02000188 RID: 392
	[TypeConverter(typeof(FlatButtonAppearanceConverter))]
	public class FlatButtonAppearance
	{
		// Token: 0x06001948 RID: 6472 RVA: 0x0006067C File Offset: 0x0005E87C
		internal FlatButtonAppearance(ButtonBase owner)
		{
			this.owner = owner;
		}

		/// <summary>Gets or sets the color of the border around the button.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> structure representing the color of the border around the button.</returns>
		// Token: 0x17000602 RID: 1538
		// (get) Token: 0x06001949 RID: 6473 RVA: 0x000606CC File Offset: 0x0005E8CC
		// (set) Token: 0x0600194A RID: 6474 RVA: 0x000606D4 File Offset: 0x0005E8D4
		[Browsable(true)]
		[NotifyParentProperty(true)]
		[DefaultValue(typeof(Color), "")]
		[EditorBrowsable(0)]
		public Color BorderColor
		{
			get
			{
				return this.borderColor;
			}
			set
			{
				if (this.borderColor == value)
				{
					return;
				}
				if (value == Color.Transparent)
				{
					throw new NotSupportedException("Cannot have a Transparent border.");
				}
				this.borderColor = value;
				if (this.owner != null)
				{
					this.owner.Invalidate();
				}
			}
		}

		/// <summary>Gets or sets a value that specifies the size, in pixels, of the border around the button.</summary>
		/// <returns>An <see cref="T:System.Int32" /> representing the size, in pixels, of the border around the button.</returns>
		// Token: 0x17000603 RID: 1539
		// (get) Token: 0x0600194B RID: 6475 RVA: 0x0006072C File Offset: 0x0005E92C
		// (set) Token: 0x0600194C RID: 6476 RVA: 0x00060734 File Offset: 0x0005E934
		[EditorBrowsable(0)]
		[DefaultValue(1)]
		[NotifyParentProperty(true)]
		[Browsable(true)]
		public int BorderSize
		{
			get
			{
				return this.borderSize;
			}
			set
			{
				if (this.borderSize == value)
				{
					return;
				}
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("value", string.Format("'{0}' is not a valid value for 'BorderSize'. 'BorderSize' must be greater or equal than {1}.", value, 0));
				}
				this.borderSize = value;
				if (this.owner != null)
				{
					this.owner.Invalidate();
				}
			}
		}

		/// <summary>Gets or sets the color of the client area of the button when the button is checked and the mouse pointer is outside the bounds of the control.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> structure representing the color of the client area of the button.</returns>
		// Token: 0x17000604 RID: 1540
		// (get) Token: 0x0600194D RID: 6477 RVA: 0x00060794 File Offset: 0x0005E994
		// (set) Token: 0x0600194E RID: 6478 RVA: 0x0006079C File Offset: 0x0005E99C
		[DefaultValue(typeof(Color), "")]
		[Browsable(true)]
		[NotifyParentProperty(true)]
		[EditorBrowsable(0)]
		public Color CheckedBackColor
		{
			get
			{
				return this.checkedBackColor;
			}
			set
			{
				if (this.checkedBackColor == value)
				{
					return;
				}
				this.checkedBackColor = value;
				if (this.owner != null)
				{
					this.owner.Invalidate();
				}
			}
		}

		/// <summary>Gets or sets the color of the client area of the button when the mouse is pressed within the bounds of the control.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> structure representing the color of the client area of the button.</returns>
		// Token: 0x17000605 RID: 1541
		// (get) Token: 0x0600194F RID: 6479 RVA: 0x000607D0 File Offset: 0x0005E9D0
		// (set) Token: 0x06001950 RID: 6480 RVA: 0x000607D8 File Offset: 0x0005E9D8
		[DefaultValue(typeof(Color), "")]
		[EditorBrowsable(0)]
		[NotifyParentProperty(true)]
		[Browsable(true)]
		public Color MouseDownBackColor
		{
			get
			{
				return this.mouseDownBackColor;
			}
			set
			{
				if (this.mouseDownBackColor == value)
				{
					return;
				}
				this.mouseDownBackColor = value;
				if (this.owner != null)
				{
					this.owner.Invalidate();
				}
			}
		}

		/// <summary>Gets or sets the color of the client area of the button when the mouse pointer is within the bounds of the control.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> structure representing the color of the client area of the button.</returns>
		// Token: 0x17000606 RID: 1542
		// (get) Token: 0x06001951 RID: 6481 RVA: 0x0006080C File Offset: 0x0005EA0C
		// (set) Token: 0x06001952 RID: 6482 RVA: 0x00060814 File Offset: 0x0005EA14
		[Browsable(true)]
		[EditorBrowsable(0)]
		[DefaultValue(typeof(Color), "")]
		[NotifyParentProperty(true)]
		public Color MouseOverBackColor
		{
			get
			{
				return this.mouseOverBackColor;
			}
			set
			{
				if (this.mouseOverBackColor == value)
				{
					return;
				}
				this.mouseOverBackColor = value;
				if (this.owner != null)
				{
					this.owner.Invalidate();
				}
			}
		}

		// Token: 0x04000E36 RID: 3638
		private Color borderColor = Color.Empty;

		// Token: 0x04000E37 RID: 3639
		private int borderSize = 1;

		// Token: 0x04000E38 RID: 3640
		private Color checkedBackColor = Color.Empty;

		// Token: 0x04000E39 RID: 3641
		private Color mouseDownBackColor = Color.Empty;

		// Token: 0x04000E3A RID: 3642
		private Color mouseOverBackColor = Color.Empty;

		// Token: 0x04000E3B RID: 3643
		private ButtonBase owner;
	}
}
