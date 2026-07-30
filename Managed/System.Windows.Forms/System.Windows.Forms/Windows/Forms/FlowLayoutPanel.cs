using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms.Layout;

namespace System.Windows.Forms
{
	/// <summary>Represents a panel that dynamically lays out its contents horizontally or vertically.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200018C RID: 396
	[Designer("System.Windows.Forms.Design.FlowLayoutPanelDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[ClassInterface(1)]
	[ProvideProperty("FlowBreak", typeof(Control))]
	[ComVisible(true)]
	[DefaultProperty("FlowDirection")]
	[Docking(DockingBehavior.Ask)]
	public class FlowLayoutPanel : Panel, IExtenderProvider
	{
		/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.IExtenderProvider.CanExtend(System.Object)" />.</summary>
		/// <returns>true if this object can provide extender properties to the specified object; otherwise, false.</returns>
		/// <param name="obj">The <see cref="T:System.Object" /> to receive the extender properties.</param>
		// Token: 0x06001957 RID: 6487 RVA: 0x000608A4 File Offset: 0x0005EAA4
		bool IExtenderProvider.CanExtend(object obj)
		{
			return obj is Control && (obj as Control).Parent == this;
		}

		/// <summary>Gets or sets a value indicating the flow direction of the <see cref="T:System.Windows.Forms.FlowLayoutPanel" /> control.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.FlowDirection" /> values indicating the direction of consecutive placement of controls in the panel. The default is <see cref="F:System.Windows.Forms.FlowDirection.LeftToRight" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000607 RID: 1543
		// (get) Token: 0x06001958 RID: 6488 RVA: 0x000608C8 File Offset: 0x0005EAC8
		// (set) Token: 0x06001959 RID: 6489 RVA: 0x000608D8 File Offset: 0x0005EAD8
		[DefaultValue(FlowDirection.LeftToRight)]
		[Localizable(true)]
		public FlowDirection FlowDirection
		{
			get
			{
				return this.LayoutSettings.FlowDirection;
			}
			set
			{
				this.LayoutSettings.FlowDirection = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.FlowLayoutPanel" /> control should wrap its contents or let the contents be clipped.</summary>
		/// <returns>true if the contents should be wrapped; otherwise, false. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000608 RID: 1544
		// (get) Token: 0x0600195A RID: 6490 RVA: 0x000608E8 File Offset: 0x0005EAE8
		// (set) Token: 0x0600195B RID: 6491 RVA: 0x000608F8 File Offset: 0x0005EAF8
		[DefaultValue(true)]
		[Localizable(true)]
		public bool WrapContents
		{
			get
			{
				return this.LayoutSettings.WrapContents;
			}
			set
			{
				this.LayoutSettings.WrapContents = value;
			}
		}

		/// <summary>Gets a cached instance of the panel's layout engine.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Layout.LayoutEngine" /> for the panel's contents.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000609 RID: 1545
		// (get) Token: 0x0600195C RID: 6492 RVA: 0x00060908 File Offset: 0x0005EB08
		public override LayoutEngine LayoutEngine
		{
			get
			{
				return this.LayoutSettings.LayoutEngine;
			}
		}

		// Token: 0x1700060A RID: 1546
		// (get) Token: 0x0600195D RID: 6493 RVA: 0x00060918 File Offset: 0x0005EB18
		internal FlowLayoutSettings LayoutSettings
		{
			get
			{
				if (this.settings == null)
				{
					this.settings = new FlowLayoutSettings(this);
				}
				return this.settings;
			}
		}

		/// <summary>Returns a value that represents the flow-break setting of the <see cref="T:System.Windows.Forms.FlowLayoutPanel" /> control.</summary>
		/// <returns>true if the flow break is set; otherwise, false.</returns>
		/// <param name="control">The child control.</param>
		// Token: 0x0600195E RID: 6494 RVA: 0x00060938 File Offset: 0x0005EB38
		[DisplayName("FlowBreak")]
		[DefaultValue(false)]
		public bool GetFlowBreak(Control control)
		{
			return this.LayoutSettings.GetFlowBreak(control);
		}

		/// <summary>Sets the value that represents the flow-break setting of the <see cref="T:System.Windows.Forms.FlowLayoutPanel" /> control.</summary>
		/// <param name="control">The child control.</param>
		/// <param name="value">The flow-break value to set.</param>
		// Token: 0x0600195F RID: 6495 RVA: 0x00060948 File Offset: 0x0005EB48
		[DisplayName("FlowBreak")]
		public void SetFlowBreak(Control control, bool value)
		{
			this.LayoutSettings.SetFlowBreak(control, value);
		}

		// Token: 0x06001960 RID: 6496 RVA: 0x00060958 File Offset: 0x0005EB58
		internal override void CalculateCanvasSize(bool canOverride)
		{
			if (canOverride)
			{
				this.canvas_size = base.ClientSize;
			}
			else
			{
				base.CalculateCanvasSize(canOverride);
			}
		}

		// Token: 0x06001961 RID: 6497 RVA: 0x00060978 File Offset: 0x0005EB78
		internal override Size GetPreferredSizeCore(Size proposedSize)
		{
			int num = 0;
			int num2 = 0;
			bool flag = this.FlowDirection == FlowDirection.LeftToRight || this.FlowDirection == FlowDirection.RightToLeft;
			if (!this.WrapContents || (flag && proposedSize.Width == 0) || (!flag && proposedSize.Height == 0))
			{
				foreach (object obj in base.Controls)
				{
					Control control = (Control)obj;
					Size size;
					if (control.AutoSize)
					{
						size = control.PreferredSize;
					}
					else
					{
						size = control.Size;
					}
					Padding margin = control.Margin;
					if (flag)
					{
						num += size.Width + margin.Horizontal;
						num2 = Math.Max(num2, size.Height + margin.Vertical);
					}
					else
					{
						num2 += size.Height + margin.Vertical;
						num = Math.Max(num, size.Width + margin.Horizontal);
					}
				}
			}
			else
			{
				int num3 = 0;
				int num4 = 0;
				foreach (object obj2 in base.Controls)
				{
					Control control2 = (Control)obj2;
					Size size2;
					if (control2.AutoSize)
					{
						size2 = control2.PreferredSize;
					}
					else
					{
						size2 = control2.ExplicitBounds.Size;
					}
					Padding margin2 = control2.Margin;
					if (flag)
					{
						int num5 = size2.Width + margin2.Horizontal;
						if (num3 != 0 && num3 + num5 >= proposedSize.Width)
						{
							num = Math.Max(num, num3);
							num3 = 0;
							num2 += num4;
							num4 = 0;
						}
						num3 += num5;
						num4 = Math.Max(num4, size2.Height + margin2.Vertical);
					}
					else
					{
						int num5 = size2.Height + margin2.Vertical;
						if (num3 != 0 && num3 + num5 >= proposedSize.Height)
						{
							num2 = Math.Max(num2, num3);
							num3 = 0;
							num += num4;
							num4 = 0;
						}
						num3 += num5;
						num4 = Math.Max(num4, size2.Width + margin2.Horizontal);
					}
				}
				if (flag)
				{
					num = Math.Max(num, num3);
					num2 += num4;
				}
				else
				{
					num2 = Math.Max(num2, num3);
					num += num4;
				}
			}
			return new Size(num, num2);
		}

		// Token: 0x04000E46 RID: 3654
		private FlowLayoutSettings settings;
	}
}
