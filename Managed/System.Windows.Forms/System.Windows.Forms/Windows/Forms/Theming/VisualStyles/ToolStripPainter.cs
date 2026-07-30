using System;
using System.Drawing;
using System.Windows.Forms.Theming.Default;
using System.Windows.Forms.VisualStyles;

namespace System.Windows.Forms.Theming.VisualStyles
{
	// Token: 0x020004D0 RID: 1232
	internal class ToolStripPainter : ToolStripPainter
	{
		// Token: 0x06004CB4 RID: 19636 RVA: 0x00133628 File Offset: 0x00131828
		private static bool IsDisabled(ToolStripItem toolStripItem)
		{
			return !toolStripItem.Enabled;
		}

		// Token: 0x06004CB5 RID: 19637 RVA: 0x00133634 File Offset: 0x00131834
		private static bool IsPressed(ToolStripItem toolStripItem)
		{
			return toolStripItem.Pressed;
		}

		// Token: 0x06004CB6 RID: 19638 RVA: 0x0013363C File Offset: 0x0013183C
		private static bool IsChecked(ToolStripItem toolStripItem)
		{
			ToolStripButton toolStripButton = toolStripItem as ToolStripButton;
			return toolStripButton != null && toolStripButton.Checked;
		}

		// Token: 0x06004CB7 RID: 19639 RVA: 0x00133660 File Offset: 0x00131860
		private static bool IsHot(ToolStripItem toolStripItem)
		{
			return toolStripItem.Selected;
		}

		// Token: 0x06004CB8 RID: 19640 RVA: 0x00133668 File Offset: 0x00131868
		public override void OnRenderButtonBackground(ToolStripItemRenderEventArgs e)
		{
			if (!ThemeVisualStyles.RenderClientAreas)
			{
				base.OnRenderButtonBackground(e);
				return;
			}
			VisualStyleElement visualStyleElement;
			if (ToolStripPainter.IsDisabled(e.Item))
			{
				visualStyleElement = VisualStyleElement.ToolBar.Button.Disabled;
			}
			else if (ToolStripPainter.IsPressed(e.Item))
			{
				visualStyleElement = VisualStyleElement.ToolBar.Button.Pressed;
			}
			else if (ToolStripPainter.IsChecked(e.Item))
			{
				if (ToolStripPainter.IsHot(e.Item))
				{
					visualStyleElement = VisualStyleElement.ToolBar.Button.HotChecked;
				}
				else
				{
					visualStyleElement = VisualStyleElement.ToolBar.Button.Checked;
				}
			}
			else if (ToolStripPainter.IsHot(e.Item))
			{
				visualStyleElement = VisualStyleElement.ToolBar.Button.Hot;
			}
			else
			{
				visualStyleElement = VisualStyleElement.ToolBar.Button.Normal;
			}
			if (!VisualStyleRenderer.IsElementDefined(visualStyleElement))
			{
				base.OnRenderButtonBackground(e);
				return;
			}
			new VisualStyleRenderer(visualStyleElement).DrawBackground(e.Graphics, e.Item.Bounds);
		}

		// Token: 0x06004CB9 RID: 19641 RVA: 0x00133744 File Offset: 0x00131944
		public override void OnRenderDropDownButtonBackground(ToolStripItemRenderEventArgs e)
		{
			if (!ThemeVisualStyles.RenderClientAreas)
			{
				base.OnRenderDropDownButtonBackground(e);
				return;
			}
			VisualStyleElement visualStyleElement;
			if (ToolStripPainter.IsDisabled(e.Item))
			{
				visualStyleElement = VisualStyleElement.ToolBar.DropDownButton.Disabled;
			}
			else if (ToolStripPainter.IsPressed(e.Item))
			{
				visualStyleElement = VisualStyleElement.ToolBar.DropDownButton.Pressed;
			}
			else if (ToolStripPainter.IsChecked(e.Item))
			{
				if (ToolStripPainter.IsHot(e.Item))
				{
					visualStyleElement = VisualStyleElement.ToolBar.DropDownButton.HotChecked;
				}
				else
				{
					visualStyleElement = VisualStyleElement.ToolBar.DropDownButton.Checked;
				}
			}
			else if (ToolStripPainter.IsHot(e.Item))
			{
				visualStyleElement = VisualStyleElement.ToolBar.DropDownButton.Hot;
			}
			else
			{
				visualStyleElement = VisualStyleElement.ToolBar.DropDownButton.Normal;
			}
			if (!VisualStyleRenderer.IsElementDefined(visualStyleElement))
			{
				base.OnRenderDropDownButtonBackground(e);
				return;
			}
			new VisualStyleRenderer(visualStyleElement).DrawBackground(e.Graphics, e.Item.Bounds);
		}

		// Token: 0x06004CBA RID: 19642 RVA: 0x00133820 File Offset: 0x00131A20
		public override void OnRenderGrip(ToolStripGripRenderEventArgs e)
		{
			if (!ThemeVisualStyles.RenderClientAreas)
			{
				base.OnRenderGrip(e);
				return;
			}
			if (e.GripStyle == ToolStripGripStyle.Hidden)
			{
				return;
			}
			VisualStyleElement visualStyleElement = ((e.GripDisplayStyle != ToolStripGripDisplayStyle.Vertical) ? VisualStyleElement.Rebar.GripperVertical.Normal : VisualStyleElement.Rebar.Gripper.Normal);
			if (!VisualStyleRenderer.IsElementDefined(visualStyleElement))
			{
				base.OnRenderGrip(e);
				return;
			}
			new VisualStyleRenderer(visualStyleElement).DrawBackground(e.Graphics, (e.GripDisplayStyle != ToolStripGripDisplayStyle.Vertical) ? new Rectangle(0, 2, 20, 5) : new Rectangle(2, 0, 5, 20));
		}

		// Token: 0x06004CBB RID: 19643 RVA: 0x001338B0 File Offset: 0x00131AB0
		public override void OnRenderOverflowButtonBackground(ToolStripItemRenderEventArgs e)
		{
			if (!ThemeVisualStyles.RenderClientAreas)
			{
				base.OnRenderOverflowButtonBackground(e);
				return;
			}
			VisualStyleElement visualStyleElement = ((e.ToolStrip.Orientation != Orientation.Horizontal) ? VisualStyleElement.Rebar.ChevronVertical.Normal : VisualStyleElement.Rebar.Chevron.Normal);
			if (!VisualStyleRenderer.IsElementDefined(visualStyleElement))
			{
				base.OnRenderOverflowButtonBackground(e);
				return;
			}
			this.OnRenderButtonBackground(e);
			new VisualStyleRenderer(visualStyleElement).DrawBackground(e.Graphics, e.Item.Bounds);
		}

		// Token: 0x06004CBC RID: 19644 RVA: 0x00133928 File Offset: 0x00131B28
		public override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e)
		{
			if (!ThemeVisualStyles.RenderClientAreas)
			{
				base.OnRenderSeparator(e);
				return;
			}
			VisualStyleElement visualStyleElement = ((e.ToolStrip.Orientation != Orientation.Horizontal) ? VisualStyleElement.ToolBar.SeparatorVertical.Normal : VisualStyleElement.ToolBar.SeparatorHorizontal.Normal);
			if (!VisualStyleRenderer.IsElementDefined(visualStyleElement))
			{
				base.OnRenderSeparator(e);
				return;
			}
			new VisualStyleRenderer(visualStyleElement).DrawBackground(e.Graphics, e.Item.Bounds);
		}

		// Token: 0x06004CBD RID: 19645 RVA: 0x00133998 File Offset: 0x00131B98
		public override void OnRenderSplitButtonBackground(ToolStripItemRenderEventArgs e)
		{
			if (!ThemeVisualStyles.RenderClientAreas)
			{
				base.OnRenderSplitButtonBackground(e);
				return;
			}
			VisualStyleElement visualStyleElement;
			VisualStyleElement visualStyleElement2;
			if (ToolStripPainter.IsDisabled(e.Item))
			{
				visualStyleElement = VisualStyleElement.ToolBar.SplitButton.Disabled;
				visualStyleElement2 = VisualStyleElement.ToolBar.SplitButtonDropDown.Disabled;
			}
			else if (ToolStripPainter.IsPressed(e.Item))
			{
				visualStyleElement = VisualStyleElement.ToolBar.SplitButton.Pressed;
				visualStyleElement2 = VisualStyleElement.ToolBar.SplitButtonDropDown.Pressed;
			}
			else if (ToolStripPainter.IsChecked(e.Item))
			{
				if (ToolStripPainter.IsHot(e.Item))
				{
					visualStyleElement = VisualStyleElement.ToolBar.SplitButton.HotChecked;
					visualStyleElement2 = VisualStyleElement.ToolBar.SplitButtonDropDown.HotChecked;
				}
				else
				{
					visualStyleElement = VisualStyleElement.ToolBar.Button.Checked;
					visualStyleElement2 = VisualStyleElement.ToolBar.SplitButtonDropDown.Checked;
				}
			}
			else if (ToolStripPainter.IsHot(e.Item))
			{
				visualStyleElement = VisualStyleElement.ToolBar.SplitButton.Hot;
				visualStyleElement2 = VisualStyleElement.ToolBar.SplitButtonDropDown.Hot;
			}
			else
			{
				visualStyleElement = VisualStyleElement.ToolBar.SplitButton.Normal;
				visualStyleElement2 = VisualStyleElement.ToolBar.SplitButtonDropDown.Normal;
			}
			if (!VisualStyleRenderer.IsElementDefined(visualStyleElement) || !VisualStyleRenderer.IsElementDefined(visualStyleElement2))
			{
				base.OnRenderSplitButtonBackground(e);
				return;
			}
			ToolStripSplitButton toolStripSplitButton = (ToolStripSplitButton)e.Item;
			VisualStyleRenderer visualStyleRenderer = new VisualStyleRenderer(visualStyleElement);
			visualStyleRenderer.DrawBackground(e.Graphics, toolStripSplitButton.ButtonBounds);
			visualStyleRenderer.SetParameters(visualStyleElement2);
			visualStyleRenderer.DrawBackground(e.Graphics, toolStripSplitButton.DropDownButtonBounds);
		}

		// Token: 0x06004CBE RID: 19646 RVA: 0x00133AC4 File Offset: 0x00131CC4
		public override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
		{
			if (e.ToolStrip.BackgroundImage != null)
			{
				return;
			}
			if (!ThemeVisualStyles.RenderClientAreas)
			{
				base.OnRenderToolStripBackground(e);
				return;
			}
			VisualStyleElement visualStyleElement;
			if (e.ToolStrip is StatusStrip)
			{
				visualStyleElement = VisualStyleElement.Status.Bar.Normal;
			}
			else
			{
				visualStyleElement = VisualStyleElement.Rebar.Band.Normal;
			}
			if (!VisualStyleRenderer.IsElementDefined(visualStyleElement))
			{
				base.OnRenderToolStripBackground(e);
				return;
			}
			new VisualStyleRenderer(visualStyleElement).DrawBackground(e.Graphics, e.ToolStrip.Bounds, e.AffectedBounds);
		}
	}
}
