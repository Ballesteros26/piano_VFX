using System;
using System.Drawing;
using System.Windows.Forms.Theming.Default;
using System.Windows.Forms.VisualStyles;

namespace System.Windows.Forms.Theming.VisualStyles
{
	// Token: 0x020004CF RID: 1231
	internal class TabControlPainter : TabControlPainter
	{
		// Token: 0x06004CAD RID: 19629 RVA: 0x00133058 File Offset: 0x00131258
		private static bool ShouldPaint(TabControl tabControl)
		{
			return ThemeVisualStyles.RenderClientAreas && tabControl.Alignment == TabAlignment.Top && tabControl.DrawMode == TabDrawMode.Normal;
		}

		// Token: 0x06004CAE RID: 19630 RVA: 0x0013307C File Offset: 0x0013127C
		protected override void DrawBackground(Graphics dc, Rectangle area, TabControl tab)
		{
			if (!TabControlPainter.ShouldPaint(tab))
			{
				base.DrawBackground(dc, area, tab);
				return;
			}
			VisualStyleElement normal = VisualStyleElement.Tab.Pane.Normal;
			if (!VisualStyleRenderer.IsElementDefined(normal))
			{
				base.DrawBackground(dc, area, tab);
				return;
			}
			Rectangle tabPanelRect = base.GetTabPanelRect(tab);
			if (tabPanelRect.IntersectsWith(area))
			{
				new VisualStyleRenderer(normal).DrawBackground(dc, tabPanelRect, area);
			}
		}

		// Token: 0x06004CAF RID: 19631 RVA: 0x001330DC File Offset: 0x001312DC
		protected override int DrawTab(Graphics dc, TabPage page, TabControl tab, Rectangle bounds, bool is_selected)
		{
			if (!TabControlPainter.ShouldPaint(tab))
			{
				return base.DrawTab(dc, page, tab, bounds, is_selected);
			}
			VisualStyleElement visualStyleElement = TabControlPainter.GetVisualStyleElement(tab, page, is_selected);
			if (!VisualStyleRenderer.IsElementDefined(visualStyleElement))
			{
				return base.DrawTab(dc, page, tab, bounds, is_selected);
			}
			new VisualStyleRenderer(visualStyleElement).DrawBackground(dc, bounds);
			bounds.Inflate(-(this.FocusRectSpacing.X + this.BorderThickness.X), -(this.FocusRectSpacing.Y + this.BorderThickness.Y));
			Rectangle rectangle = bounds;
			if (tab.ImageList != null && page.ImageIndex >= 0 && page.ImageIndex < tab.ImageList.Images.Count)
			{
				int num = bounds.Y + (bounds.Height - tab.ImageList.ImageSize.Height) / 2;
				tab.ImageList.Draw(dc, new Point(bounds.X, num), page.ImageIndex);
				int num2 = tab.ImageList.ImageSize.Width + 2;
				rectangle.X += num2;
				rectangle.Width -= num2;
			}
			if (page.Text != null)
			{
				dc.DrawString(page.Text, page.Font, SystemBrushes.ControlText, rectangle, this.DefaultFormatting);
			}
			if (tab.Focused && is_selected && tab.ShowFocusCues)
			{
				ControlPaint.DrawFocusRectangle(dc, bounds);
			}
			return 0;
		}

		// Token: 0x06004CB0 RID: 19632 RVA: 0x00133280 File Offset: 0x00131480
		private static VisualStyleElement GetVisualStyleElement(TabControl tabControl, TabPage tabPage, bool selected)
		{
			bool flag = tabPage.Row == tabControl.RowCount;
			int num = tabControl.TabPages.IndexOf(tabPage);
			bool flag2 = true;
			for (int i = tabControl.SliderPos; i < num; i++)
			{
				if (tabControl.TabPages[i].Row == tabPage.Row)
				{
					flag2 = false;
					break;
				}
			}
			bool flag3 = true;
			for (int i = num; i < tabControl.TabCount; i++)
			{
				if (tabControl.TabPages[i].Row == tabPage.Row)
				{
					flag3 = false;
					break;
				}
			}
			if (!tabPage.Enabled)
			{
				if (flag)
				{
					if (flag2)
					{
						if (flag3)
						{
							return VisualStyleElement.Tab.TopTabItem.Disabled;
						}
						return VisualStyleElement.Tab.TopTabItemLeftEdge.Disabled;
					}
					else
					{
						if (flag3)
						{
							return VisualStyleElement.Tab.TopTabItemRightEdge.Disabled;
						}
						return VisualStyleElement.Tab.TopTabItem.Disabled;
					}
				}
				else if (flag2)
				{
					if (flag3)
					{
						return VisualStyleElement.Tab.TabItem.Disabled;
					}
					return VisualStyleElement.Tab.TabItemLeftEdge.Disabled;
				}
				else
				{
					if (flag3)
					{
						return VisualStyleElement.Tab.TabItemRightEdge.Disabled;
					}
					return VisualStyleElement.Tab.TabItem.Disabled;
				}
			}
			else if (selected)
			{
				if (flag)
				{
					if (flag2)
					{
						if (flag3)
						{
							return VisualStyleElement.Tab.TopTabItem.Pressed;
						}
						return VisualStyleElement.Tab.TopTabItemLeftEdge.Pressed;
					}
					else
					{
						if (flag3)
						{
							return VisualStyleElement.Tab.TopTabItemRightEdge.Pressed;
						}
						return VisualStyleElement.Tab.TopTabItem.Pressed;
					}
				}
				else if (flag2)
				{
					if (flag3)
					{
						return VisualStyleElement.Tab.TabItem.Pressed;
					}
					return VisualStyleElement.Tab.TabItemLeftEdge.Pressed;
				}
				else
				{
					if (flag3)
					{
						return VisualStyleElement.Tab.TabItemRightEdge.Pressed;
					}
					return VisualStyleElement.Tab.TabItem.Pressed;
				}
			}
			else if (tabControl.EnteredTabPage == tabPage)
			{
				if (flag)
				{
					if (flag2)
					{
						if (flag3)
						{
							return VisualStyleElement.Tab.TopTabItem.Hot;
						}
						return VisualStyleElement.Tab.TopTabItemLeftEdge.Hot;
					}
					else
					{
						if (flag3)
						{
							return VisualStyleElement.Tab.TopTabItemRightEdge.Hot;
						}
						return VisualStyleElement.Tab.TopTabItem.Hot;
					}
				}
				else if (flag2)
				{
					if (flag3)
					{
						return VisualStyleElement.Tab.TabItem.Hot;
					}
					return VisualStyleElement.Tab.TabItemLeftEdge.Hot;
				}
				else
				{
					if (flag3)
					{
						return VisualStyleElement.Tab.TabItemRightEdge.Hot;
					}
					return VisualStyleElement.Tab.TabItem.Hot;
				}
			}
			else if (flag)
			{
				if (flag2)
				{
					if (flag3)
					{
						return VisualStyleElement.Tab.TopTabItemBothEdges.Normal;
					}
					return VisualStyleElement.Tab.TopTabItemLeftEdge.Normal;
				}
				else
				{
					if (flag3)
					{
						return VisualStyleElement.Tab.TopTabItemRightEdge.Normal;
					}
					return VisualStyleElement.Tab.TopTabItem.Normal;
				}
			}
			else if (flag2)
			{
				if (flag3)
				{
					return VisualStyleElement.Tab.TabItemBothEdges.Normal;
				}
				return VisualStyleElement.Tab.TabItemLeftEdge.Normal;
			}
			else
			{
				if (flag3)
				{
					return VisualStyleElement.Tab.TabItemRightEdge.Normal;
				}
				return VisualStyleElement.Tab.TabItem.Normal;
			}
		}

		// Token: 0x06004CB1 RID: 19633 RVA: 0x001334B8 File Offset: 0x001316B8
		public override bool HasHotElementStyles(TabControl tabControl)
		{
			return TabControlPainter.ShouldPaint(tabControl) || base.HasHotElementStyles(tabControl);
		}

		// Token: 0x06004CB2 RID: 19634 RVA: 0x001334D0 File Offset: 0x001316D0
		protected override void DrawScrollButton(Graphics dc, Rectangle bounds, Rectangle clippingArea, ScrollButton button, PushButtonState state)
		{
			if (!ThemeVisualStyles.RenderClientAreas)
			{
				base.DrawScrollButton(dc, bounds, clippingArea, button, state);
				return;
			}
			VisualStyleElement visualStyleElement;
			if (button == ScrollButton.Left)
			{
				if (state != PushButtonState.Hot)
				{
					if (state != PushButtonState.Pressed)
					{
						visualStyleElement = VisualStyleElement.Spin.DownHorizontal.Normal;
					}
					else
					{
						visualStyleElement = VisualStyleElement.Spin.DownHorizontal.Pressed;
					}
				}
				else
				{
					visualStyleElement = VisualStyleElement.Spin.DownHorizontal.Hot;
				}
			}
			else if (state != PushButtonState.Hot)
			{
				if (state != PushButtonState.Pressed)
				{
					visualStyleElement = VisualStyleElement.Spin.UpHorizontal.Normal;
				}
				else
				{
					visualStyleElement = VisualStyleElement.Spin.UpHorizontal.Pressed;
				}
			}
			else
			{
				visualStyleElement = VisualStyleElement.Spin.UpHorizontal.Hot;
			}
			if (!VisualStyleRenderer.IsElementDefined(visualStyleElement))
			{
				if (button == ScrollButton.Left)
				{
					if (state != PushButtonState.Hot)
					{
						if (state != PushButtonState.Pressed)
						{
							visualStyleElement = VisualStyleElement.ScrollBar.ArrowButton.LeftNormal;
						}
						else
						{
							visualStyleElement = VisualStyleElement.ScrollBar.ArrowButton.LeftPressed;
						}
					}
					else
					{
						visualStyleElement = VisualStyleElement.ScrollBar.ArrowButton.LeftHot;
					}
				}
				else if (state != PushButtonState.Hot)
				{
					if (state != PushButtonState.Pressed)
					{
						visualStyleElement = VisualStyleElement.ScrollBar.ArrowButton.RightNormal;
					}
					else
					{
						visualStyleElement = VisualStyleElement.ScrollBar.ArrowButton.RightPressed;
					}
				}
				else
				{
					visualStyleElement = VisualStyleElement.ScrollBar.ArrowButton.RightHot;
				}
				if (!VisualStyleRenderer.IsElementDefined(visualStyleElement))
				{
					base.DrawScrollButton(dc, bounds, clippingArea, button, state);
					return;
				}
			}
			new VisualStyleRenderer(visualStyleElement).DrawBackground(dc, bounds, clippingArea);
		}
	}
}
