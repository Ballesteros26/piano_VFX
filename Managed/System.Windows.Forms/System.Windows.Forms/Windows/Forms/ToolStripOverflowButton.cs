using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms.Design;

namespace System.Windows.Forms
{
	/// <summary>Hosts a <see cref="T:System.Windows.Forms.ToolStripDropDown" /> that displays items that overflow the <see cref="T:System.Windows.Forms.ToolStrip" />.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200036B RID: 875
	[ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.None)]
	public class ToolStripOverflowButton : ToolStripDropDownButton
	{
		// Token: 0x06003ECB RID: 16075 RVA: 0x000FAB2C File Offset: 0x000F8D2C
		internal ToolStripOverflowButton(ToolStrip ts)
		{
			base.InternalOwner = ts;
			base.Parent = ts;
			base.Visible = false;
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Windows.Forms.ToolStripOverflowButton" /> has items that overflow the <see cref="T:System.Windows.Forms.ToolStrip" />.</summary>
		/// <returns>true if the <see cref="T:System.Windows.Forms.ToolStripOverflowButton" /> has overflow items; otherwise, false. </returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17001066 RID: 4198
		// (get) Token: 0x06003ECC RID: 16076 RVA: 0x000FAB54 File Offset: 0x000F8D54
		public override bool HasDropDownItems
		{
			get
			{
				return this.drop_down != null && base.DropDown.DisplayedItems.Count > 0;
			}
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>true to enable automatic mirroring; otherwise, false.</returns>
		// Token: 0x17001067 RID: 4199
		// (get) Token: 0x06003ECD RID: 16077 RVA: 0x000FAB84 File Offset: 0x000F8D84
		// (set) Token: 0x06003ECE RID: 16078 RVA: 0x000FAB8C File Offset: 0x000F8D8C
		[Browsable(false)]
		[EditorBrowsable(1)]
		[DesignerSerializationVisibility(0)]
		public new bool RightToLeftAutoMirrorImage
		{
			get
			{
				return base.RightToLeftAutoMirrorImage;
			}
			set
			{
				base.RightToLeftAutoMirrorImage = value;
			}
		}

		/// <summary>Gets the space, in pixels, that is specified by default between controls.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Padding" /> value representing the space between controls.</returns>
		// Token: 0x17001068 RID: 4200
		// (get) Token: 0x06003ECF RID: 16079 RVA: 0x000FAB98 File Offset: 0x000F8D98
		protected internal override Padding DefaultMargin
		{
			get
			{
				return new Padding(0, 1, 0, 2);
			}
		}

		/// <summary>Retrieves the size of a rectangular area into which a control can fit.</summary>
		/// <returns>An ordered pair of type <see cref="T:System.Drawing.Size" /> representing the width and height of a rectangle.</returns>
		/// <param name="constrainingSize">The custom-sized area for a control. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003ED0 RID: 16080 RVA: 0x000FABA4 File Offset: 0x000F8DA4
		public override Size GetPreferredSize(Size constrainingSize)
		{
			return new Size(16, base.Parent.Height);
		}

		/// <summary>Creates a new accessibility object for the control.</summary>
		/// <returns>A new <see cref="T:System.Windows.Forms.AccessibleObject" /> for the control.</returns>
		// Token: 0x06003ED1 RID: 16081 RVA: 0x000FABB8 File Offset: 0x000F8DB8
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			return new ToolStripOverflowButton.ToolStripOverflowButtonAccessibleObject();
		}

		/// <summary>Creates an empty <see cref="T:System.Windows.Forms.ToolStripDropDown" /> that can be dropped down and to which events can be attached.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ToolStripDropDown" /> control.</returns>
		// Token: 0x06003ED2 RID: 16082 RVA: 0x000FABC0 File Offset: 0x000F8DC0
		protected override ToolStripDropDown CreateDefaultDropDown()
		{
			return new ToolStripOverflow(this)
			{
				DefaultDropDownDirection = ToolStripDropDownDirection.BelowLeft,
				OwnerItem = this
			};
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06003ED3 RID: 16083 RVA: 0x000FABE4 File Offset: 0x000F8DE4
		protected override void OnPaint(PaintEventArgs e)
		{
			if (base.Owner != null)
			{
				base.Owner.Renderer.DrawOverflowButtonBackground(new ToolStripItemRenderEventArgs(e.Graphics, this));
			}
		}

		/// <summary>Sets the size and location of the <see cref="T:System.Windows.Forms.ToolStripOverflowButton" />.</summary>
		/// <param name="bounds">A <see cref="T:System.Drawing.Rectangle" /> representing the size and location of the <see cref="T:System.Windows.Forms.ToolStripOverflowButton" />.</param>
		// Token: 0x06003ED4 RID: 16084 RVA: 0x000FAC18 File Offset: 0x000F8E18
		protected internal override void SetBounds(Rectangle bounds)
		{
			base.SetBounds(bounds);
		}

		// Token: 0x0200036C RID: 876
		private class ToolStripOverflowButtonAccessibleObject : AccessibleObject
		{
		}
	}
}
