using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms.Layout;

namespace System.Windows.Forms
{
	/// <summary>Collects the characteristics associated with flow layouts.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200018D RID: 397
	[DefaultProperty("FlowDirection")]
	public class FlowLayoutSettings : LayoutSettings
	{
		// Token: 0x06001962 RID: 6498 RVA: 0x00060C48 File Offset: 0x0005EE48
		internal FlowLayoutSettings()
			: this(null)
		{
		}

		// Token: 0x06001963 RID: 6499 RVA: 0x00060C54 File Offset: 0x0005EE54
		internal FlowLayoutSettings(Control owner)
		{
			this.flow_breaks = new Dictionary<object, bool>();
			this.wrap_contents = true;
			this.flow_direction = FlowDirection.LeftToRight;
			this.owner = owner;
		}

		/// <summary>Gets or sets a value indicating the flow direction of consecutive controls.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.FlowDirection" /> indicating the flow direction of consecutive controls in the container. The default is <see cref="F:System.Windows.Forms.FlowDirection.LeftToRight" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700060B RID: 1547
		// (get) Token: 0x06001964 RID: 6500 RVA: 0x00060C88 File Offset: 0x0005EE88
		// (set) Token: 0x06001965 RID: 6501 RVA: 0x00060C90 File Offset: 0x0005EE90
		[DefaultValue(FlowDirection.LeftToRight)]
		public FlowDirection FlowDirection
		{
			get
			{
				return this.flow_direction;
			}
			set
			{
				if (this.flow_direction != value)
				{
					this.flow_direction = value;
					if (this.owner != null)
					{
						this.owner.PerformLayout(this.owner, "FlowDirection");
					}
				}
			}
		}

		/// <summary>Gets the current flow layout engine.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Layout.LayoutEngine" /> currently being used. </returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700060C RID: 1548
		// (get) Token: 0x06001966 RID: 6502 RVA: 0x00060CD4 File Offset: 0x0005EED4
		public override LayoutEngine LayoutEngine
		{
			get
			{
				if (this.layout_engine == null)
				{
					this.layout_engine = new FlowLayout();
				}
				return this.layout_engine;
			}
		}

		/// <summary>Gets or sets a value indicating whether the contents should be wrapped or clipped when they exceed the original boundaries of their container.</summary>
		/// <returns>true if the contents should be wrapped; otherwise, false if the contents should be clipped. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700060D RID: 1549
		// (get) Token: 0x06001967 RID: 6503 RVA: 0x00060CF4 File Offset: 0x0005EEF4
		// (set) Token: 0x06001968 RID: 6504 RVA: 0x00060CFC File Offset: 0x0005EEFC
		[DefaultValue(true)]
		public bool WrapContents
		{
			get
			{
				return this.wrap_contents;
			}
			set
			{
				if (this.wrap_contents != value)
				{
					this.wrap_contents = value;
					if (this.owner != null)
					{
						this.owner.PerformLayout(this.owner, "WrapContents");
					}
				}
			}
		}

		/// <summary>Returns a value that represents the flow break setting of the control.</summary>
		/// <returns>true if the flow break is set; otherwise, false.</returns>
		/// <param name="child">The child control.</param>
		// Token: 0x06001969 RID: 6505 RVA: 0x00060D40 File Offset: 0x0005EF40
		public bool GetFlowBreak(object child)
		{
			bool flag;
			return this.flow_breaks.TryGetValue(child, ref flag) && flag;
		}

		/// <summary>Sets the value that represents the flow break setting of the control.</summary>
		/// <param name="child">The child control.</param>
		/// <param name="value">The flow break value to set.</param>
		// Token: 0x0600196A RID: 6506 RVA: 0x00060D64 File Offset: 0x0005EF64
		public void SetFlowBreak(object child, bool value)
		{
			this.flow_breaks[child] = value;
			if (this.owner != null)
			{
				this.owner.PerformLayout((Control)child, "FlowBreak");
			}
		}

		// Token: 0x04000E47 RID: 3655
		private FlowDirection flow_direction;

		// Token: 0x04000E48 RID: 3656
		private bool wrap_contents;

		// Token: 0x04000E49 RID: 3657
		private LayoutEngine layout_engine;

		// Token: 0x04000E4A RID: 3658
		private Dictionary<object, bool> flow_breaks;

		// Token: 0x04000E4B RID: 3659
		private Control owner;
	}
}
