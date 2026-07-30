using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms.Layout;

namespace System.Windows.Forms
{
	/// <summary>Manages the overflow behavior of a <see cref="T:System.Windows.Forms.ToolStrip" />.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000369 RID: 873
	[ComVisible(true)]
	[ClassInterface(1)]
	public class ToolStripOverflow : ToolStripDropDown, IDisposable, IComponent
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripOverflow" /> class derived from a base <see cref="T:System.Windows.Forms.ToolStripItem" />.</summary>
		/// <param name="parentItem">The <see cref="T:System.Windows.Forms.ToolStripItem" /> from which to derive this <see cref="T:System.Windows.Forms.ToolStripOverflow" /> instance. </param>
		// Token: 0x06003EC1 RID: 16065 RVA: 0x000FA7F8 File Offset: 0x000F89F8
		public ToolStripOverflow(ToolStripItem parentItem)
		{
			base.OwnerItem = parentItem;
		}

		/// <summary>Gets all of the items on the <see cref="T:System.Windows.Forms.ToolStrip" />, whether they are currently being displayed or not.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ToolStripItemCollection" /> containing all of the items.</returns>
		// Token: 0x17001062 RID: 4194
		// (get) Token: 0x06003EC2 RID: 16066 RVA: 0x000FA808 File Offset: 0x000F8A08
		public override ToolStripItemCollection Items
		{
			get
			{
				return base.Items;
			}
		}

		/// <filterpriority>1</filterpriority>
		// Token: 0x17001063 RID: 4195
		// (get) Token: 0x06003EC3 RID: 16067 RVA: 0x000FA810 File Offset: 0x000F8A10
		public override LayoutEngine LayoutEngine
		{
			get
			{
				if (this.layout_engine == null)
				{
					this.layout_engine = new FlowLayout();
				}
				return base.LayoutEngine;
			}
		}

		/// <summary>Gets all of the items that are currently being displayed on the <see cref="T:System.Windows.Forms.ToolStrip" />.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ToolStripItemCollection" /> that includes all items on this <see cref="T:System.Windows.Forms.ToolStrip" />.</returns>
		// Token: 0x17001064 RID: 4196
		// (get) Token: 0x06003EC4 RID: 16068 RVA: 0x000FA830 File Offset: 0x000F8A30
		protected internal override ToolStripItemCollection DisplayedItems
		{
			get
			{
				return base.DisplayedItems;
			}
		}

		/// <summary>Retrieves the size of a rectangular area into which a control can be fitted.</summary>
		/// <returns>An ordered pair of type <see cref="T:System.Drawing.Size" /> representing the width and height of a rectangle.</returns>
		/// <param name="constrainingSize">The custom-sized area for a control.</param>
		// Token: 0x06003EC5 RID: 16069 RVA: 0x000FA838 File Offset: 0x000F8A38
		public override Size GetPreferredSize(Size constrainingSize)
		{
			return base.GetToolStripPreferredSize(constrainingSize);
		}

		/// <summary>Creates a new accessibility object for the control.</summary>
		/// <returns>A new <see cref="T:System.Windows.Forms.AccessibleObject" /> for the control.</returns>
		// Token: 0x06003EC6 RID: 16070 RVA: 0x000FA844 File Offset: 0x000F8A44
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			return new ToolStripOverflow.ToolStripOverflowAccessibleObject();
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Layout" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.LayoutEventArgs" /> that contains the event data.</param>
		// Token: 0x06003EC7 RID: 16071 RVA: 0x000FA84C File Offset: 0x000F8A4C
		[MonoInternalNote("This should stack in rows of ~3, but for now 1 column will work.")]
		protected override void OnLayout(LayoutEventArgs e)
		{
			this.SetDisplayedItems();
			int num = 0;
			foreach (object obj in this.DisplayedItems)
			{
				ToolStripItem toolStripItem = (ToolStripItem)obj;
				if (toolStripItem.Available)
				{
					if (toolStripItem.GetPreferredSize(Size.Empty).Width > num)
					{
						num = toolStripItem.GetPreferredSize(Size.Empty).Width;
					}
				}
			}
			int left = base.Padding.Left;
			num += base.Padding.Horizontal;
			int num2 = base.Padding.Top;
			foreach (object obj2 in this.DisplayedItems)
			{
				ToolStripItem toolStripItem2 = (ToolStripItem)obj2;
				if (toolStripItem2.Available)
				{
					num2 += toolStripItem2.Margin.Top;
					int num3;
					if (toolStripItem2 is ToolStripSeparator)
					{
						num3 = 7;
					}
					else
					{
						num3 = toolStripItem2.GetPreferredSize(Size.Empty).Height;
					}
					toolStripItem2.SetBounds(new Rectangle(left, num2, num, num3));
					num2 += toolStripItem2.Height + toolStripItem2.Margin.Bottom;
				}
			}
			base.Size = new Size(num + base.Padding.Horizontal, num2 + base.Padding.Bottom);
		}

		/// <summary>Resets the collection of displayed and overflow items after a layout is done.</summary>
		// Token: 0x06003EC8 RID: 16072 RVA: 0x000FAA44 File Offset: 0x000F8C44
		protected override void SetDisplayedItems()
		{
			this.displayed_items.Clear();
			if (base.OwnerItem != null && base.OwnerItem.Parent != null)
			{
				foreach (object obj in base.OwnerItem.Parent.Items)
				{
					ToolStripItem toolStripItem = (ToolStripItem)obj;
					if (toolStripItem.Placement == ToolStripItemPlacement.Overflow && toolStripItem.Available && !(toolStripItem is ToolStripSeparator))
					{
						this.displayed_items.AddNoOwnerOrLayout(toolStripItem);
					}
				}
			}
			base.PerformLayout();
		}

		// Token: 0x17001065 RID: 4197
		// (get) Token: 0x06003EC9 RID: 16073 RVA: 0x000FAB14 File Offset: 0x000F8D14
		internal ToolStrip ParentToolStrip
		{
			get
			{
				return base.OwnerItem.Parent;
			}
		}

		// Token: 0x04001B2E RID: 6958
		private LayoutEngine layout_engine;

		// Token: 0x0200036A RID: 874
		private class ToolStripOverflowAccessibleObject : AccessibleObject
		{
		}
	}
}
