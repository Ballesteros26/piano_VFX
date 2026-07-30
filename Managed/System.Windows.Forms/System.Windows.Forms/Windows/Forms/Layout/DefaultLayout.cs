using System;
using System.Drawing;

namespace System.Windows.Forms.Layout
{
	// Token: 0x0200049D RID: 1181
	internal class DefaultLayout : LayoutEngine
	{
		// Token: 0x06004B77 RID: 19319 RVA: 0x001286E8 File Offset: 0x001268E8
		private void LayoutDockedChildren(Control parent, Control[] controls)
		{
			Rectangle displayRectangle = parent.DisplayRectangle;
			MdiClient mdiClient = null;
			for (int i = controls.Length - 1; i >= 0; i--)
			{
				Control control = controls[i];
				Size size = control.Size;
				if (control.AutoSize)
				{
					size = this.GetPreferredControlSize(control);
				}
				if (control.VisibleInternal && control.ControlLayoutType != Control.LayoutType.Anchor)
				{
					if (control is MdiClient)
					{
						mdiClient = (MdiClient)control;
					}
					else
					{
						switch (control.Dock)
						{
						case DockStyle.Top:
							control.SetBoundsInternal(displayRectangle.Left, displayRectangle.Y, displayRectangle.Width, size.Height, BoundsSpecified.None);
							displayRectangle.Y += control.Height;
							displayRectangle.Height -= control.Height;
							break;
						case DockStyle.Bottom:
							control.SetBoundsInternal(displayRectangle.Left, displayRectangle.Bottom - size.Height, displayRectangle.Width, size.Height, BoundsSpecified.None);
							displayRectangle.Height -= control.Height;
							break;
						case DockStyle.Left:
							control.SetBoundsInternal(displayRectangle.Left, displayRectangle.Y, size.Width, displayRectangle.Height, BoundsSpecified.None);
							displayRectangle.X += control.Width;
							displayRectangle.Width -= control.Width;
							break;
						case DockStyle.Right:
							control.SetBoundsInternal(displayRectangle.Right - size.Width, displayRectangle.Y, size.Width, displayRectangle.Height, BoundsSpecified.None);
							displayRectangle.Width -= control.Width;
							break;
						case DockStyle.Fill:
							control.SetBoundsInternal(displayRectangle.Left, displayRectangle.Top, displayRectangle.Width, displayRectangle.Height, BoundsSpecified.None);
							break;
						}
					}
				}
			}
			if (mdiClient != null)
			{
				mdiClient.SetBoundsInternal(displayRectangle.Left, displayRectangle.Top, displayRectangle.Width, displayRectangle.Height, BoundsSpecified.None);
			}
		}

		// Token: 0x06004B78 RID: 19320 RVA: 0x00128910 File Offset: 0x00126B10
		private void LayoutAnchoredChildren(Control parent, Control[] controls)
		{
			Rectangle clientRectangle = parent.ClientRectangle;
			foreach (Control control in controls)
			{
				if (control.VisibleInternal && control.ControlLayoutType != Control.LayoutType.Dock)
				{
					AnchorStyles anchor = control.Anchor;
					int num = control.Left;
					int num2 = control.Top;
					int num3 = control.Width;
					int num4 = control.Height;
					if ((anchor & AnchorStyles.Right) != AnchorStyles.None)
					{
						if ((anchor & AnchorStyles.Left) != AnchorStyles.None)
						{
							num3 = clientRectangle.Width - control.dist_right - num;
						}
						else
						{
							num = clientRectangle.Width - control.dist_right - num3;
						}
					}
					else if ((anchor & AnchorStyles.Left) == AnchorStyles.None)
					{
						num += (clientRectangle.Width - (num + num3 + control.dist_right)) / 2;
						control.dist_right = clientRectangle.Width - (num + num3);
					}
					if ((anchor & AnchorStyles.Bottom) != AnchorStyles.None)
					{
						if ((anchor & AnchorStyles.Top) != AnchorStyles.None)
						{
							num4 = clientRectangle.Height - control.dist_bottom - num2;
						}
						else
						{
							num2 = clientRectangle.Height - control.dist_bottom - num4;
						}
					}
					else if ((anchor & AnchorStyles.Top) == AnchorStyles.None)
					{
						num2 += (clientRectangle.Height - (num2 + num4 + control.dist_bottom)) / 2;
						control.dist_bottom = clientRectangle.Height - (num2 + num4);
					}
					if (num3 < 0)
					{
						num3 = 0;
					}
					if (num4 < 0)
					{
						num4 = 0;
					}
					control.SetBoundsInternal(num, num2, num3, num4, BoundsSpecified.None);
				}
			}
		}

		// Token: 0x06004B79 RID: 19321 RVA: 0x00128A98 File Offset: 0x00126C98
		private void LayoutAutoSizedChildren(Control parent, Control[] controls)
		{
			foreach (Control control in controls)
			{
				if (control.VisibleInternal && control.ControlLayoutType != Control.LayoutType.Dock && control.AutoSize)
				{
					AnchorStyles anchor = control.Anchor;
					int left = control.Left;
					int top = control.Top;
					Size preferredControlSize = this.GetPreferredControlSize(control);
					if ((anchor & AnchorStyles.Left) != AnchorStyles.None || (anchor & AnchorStyles.Right) == AnchorStyles.None)
					{
						control.dist_right += control.Width - preferredControlSize.Width;
					}
					if ((anchor & AnchorStyles.Top) != AnchorStyles.None || (anchor & AnchorStyles.Bottom) == AnchorStyles.None)
					{
						control.dist_bottom += control.Height - preferredControlSize.Height;
					}
					control.SetBoundsInternal(left, top, preferredControlSize.Width, preferredControlSize.Height, BoundsSpecified.None);
				}
			}
		}

		// Token: 0x06004B7A RID: 19322 RVA: 0x00128B74 File Offset: 0x00126D74
		private void LayoutAutoSizeContainer(Control container)
		{
			if (!container.VisibleInternal || container.ControlLayoutType == Control.LayoutType.Dock || !container.AutoSize)
			{
				return;
			}
			int left = container.Left;
			int top = container.Top;
			Size preferredSize = container.PreferredSize;
			int num;
			int num2;
			if (container.GetAutoSizeMode() == AutoSizeMode.GrowAndShrink)
			{
				num = preferredSize.Width;
				num2 = preferredSize.Height;
			}
			else
			{
				num = container.ExplicitBounds.Width;
				num2 = container.ExplicitBounds.Height;
				if (preferredSize.Width > num)
				{
					num = preferredSize.Width;
				}
				if (preferredSize.Height > num2)
				{
					num2 = preferredSize.Height;
				}
			}
			if (num < container.MinimumSize.Width)
			{
				num = container.MinimumSize.Width;
			}
			if (num2 < container.MinimumSize.Height)
			{
				num2 = container.MinimumSize.Height;
			}
			if (container.MaximumSize.Width != 0 && num > container.MaximumSize.Width)
			{
				num = container.MaximumSize.Width;
			}
			if (container.MaximumSize.Height != 0 && num2 > container.MaximumSize.Height)
			{
				num2 = container.MaximumSize.Height;
			}
			container.SetBoundsInternal(left, top, num, num2, BoundsSpecified.None);
		}

		// Token: 0x06004B7B RID: 19323 RVA: 0x00128CEC File Offset: 0x00126EEC
		public override bool Layout(object container, LayoutEventArgs args)
		{
			Control control = container as Control;
			Control[] allControls = control.Controls.GetAllControls();
			this.LayoutDockedChildren(control, allControls);
			this.LayoutAnchoredChildren(control, allControls);
			this.LayoutAutoSizedChildren(control, allControls);
			if (control is Form)
			{
				this.LayoutAutoSizeContainer(control);
			}
			return false;
		}

		// Token: 0x06004B7C RID: 19324 RVA: 0x00128D38 File Offset: 0x00126F38
		private Size GetPreferredControlSize(Control child)
		{
			Size preferredSize = child.PreferredSize;
			int num;
			int num2;
			if (child.GetAutoSizeMode() == AutoSizeMode.GrowAndShrink || (child.Dock != DockStyle.None && !(child is Button)))
			{
				num = preferredSize.Width;
				num2 = preferredSize.Height;
			}
			else
			{
				num = child.ExplicitBounds.Width;
				num2 = child.ExplicitBounds.Height;
				if (preferredSize.Width > num)
				{
					num = preferredSize.Width;
				}
				if (preferredSize.Height > num2)
				{
					num2 = preferredSize.Height;
				}
			}
			if (num < child.MinimumSize.Width)
			{
				num = child.MinimumSize.Width;
			}
			if (num2 < child.MinimumSize.Height)
			{
				num2 = child.MinimumSize.Height;
			}
			if (child.MaximumSize.Width != 0 && num > child.MaximumSize.Width)
			{
				num = child.MaximumSize.Width;
			}
			if (child.MaximumSize.Height != 0 && num2 > child.MaximumSize.Height)
			{
				num2 = child.MaximumSize.Height;
			}
			return new Size(num, num2);
		}
	}
}
