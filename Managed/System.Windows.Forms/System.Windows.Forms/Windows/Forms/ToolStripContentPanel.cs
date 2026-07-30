using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Represents the center panel of a <see cref="T:System.Windows.Forms.ToolStripContainer" /> control.</summary>
	// Token: 0x02000344 RID: 836
	[ComVisible(true)]
	[InitializationEvent("Load")]
	[Docking(DockingBehavior.Never)]
	[DefaultEvent("Load")]
	[ToolboxItem(false)]
	[Designer("System.Windows.Forms.Design.ToolStripContentPanelDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[ClassInterface(1)]
	public class ToolStripContentPanel : Panel
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripContentPanel" /> class. </summary>
		// Token: 0x06003B3C RID: 15164 RVA: 0x000F1774 File Offset: 0x000EF974
		public ToolStripContentPanel()
		{
			this.RenderMode = ToolStripRenderMode.System;
		}

		// Token: 0x06003B3D RID: 15165 RVA: 0x000F1784 File Offset: 0x000EF984
		// Note: this type is marked as 'beforefieldinit'.
		static ToolStripContentPanel()
		{
			ToolStripContentPanel.LoadEvent = new object();
			ToolStripContentPanel.RendererChangedEvent = new object();
		}

		/// <summary>This event is not relevant to this class.</summary>
		// Token: 0x14000375 RID: 885
		// (add) Token: 0x06003B3E RID: 15166 RVA: 0x000F179C File Offset: 0x000EF99C
		// (remove) Token: 0x06003B3F RID: 15167 RVA: 0x000F17A8 File Offset: 0x000EF9A8
		[Browsable(false)]
		[EditorBrowsable(1)]
		[DesignerSerializationVisibility(0)]
		public new event EventHandler AutoSizeChanged
		{
			add
			{
				base.AutoSizeChanged += value;
			}
			remove
			{
				base.AutoSizeChanged -= value;
			}
		}

		/// <summary>This event is not relevant for this class.</summary>
		// Token: 0x14000376 RID: 886
		// (add) Token: 0x06003B40 RID: 15168 RVA: 0x000F17B4 File Offset: 0x000EF9B4
		// (remove) Token: 0x06003B41 RID: 15169 RVA: 0x000F17C0 File Offset: 0x000EF9C0
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new event EventHandler CausesValidationChanged
		{
			add
			{
				base.CausesValidationChanged += value;
			}
			remove
			{
				base.CausesValidationChanged -= value;
			}
		}

		/// <summary>This event is not relevant to this class.</summary>
		// Token: 0x14000377 RID: 887
		// (add) Token: 0x06003B42 RID: 15170 RVA: 0x000F17CC File Offset: 0x000EF9CC
		// (remove) Token: 0x06003B43 RID: 15171 RVA: 0x000F17D8 File Offset: 0x000EF9D8
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new event EventHandler DockChanged
		{
			add
			{
				base.DockChanged += value;
			}
			remove
			{
				base.DockChanged -= value;
			}
		}

		/// <summary>Occurs when the content panel loads.</summary>
		// Token: 0x14000378 RID: 888
		// (add) Token: 0x06003B44 RID: 15172 RVA: 0x000F17E4 File Offset: 0x000EF9E4
		// (remove) Token: 0x06003B45 RID: 15173 RVA: 0x000F17F8 File Offset: 0x000EF9F8
		public event EventHandler Load
		{
			add
			{
				base.Events.AddHandler(ToolStripContentPanel.LoadEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripContentPanel.LoadEvent, value);
			}
		}

		/// <summary>This event is not relevant to this class.</summary>
		// Token: 0x14000379 RID: 889
		// (add) Token: 0x06003B46 RID: 15174 RVA: 0x000F180C File Offset: 0x000EFA0C
		// (remove) Token: 0x06003B47 RID: 15175 RVA: 0x000F1818 File Offset: 0x000EFA18
		[DesignerSerializationVisibility(0)]
		[EditorBrowsable(1)]
		[Browsable(false)]
		public new event EventHandler LocationChanged
		{
			add
			{
				base.LocationChanged += value;
			}
			remove
			{
				base.LocationChanged -= value;
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ToolStripContentPanel.Renderer" /> property changes.</summary>
		// Token: 0x1400037A RID: 890
		// (add) Token: 0x06003B48 RID: 15176 RVA: 0x000F1824 File Offset: 0x000EFA24
		// (remove) Token: 0x06003B49 RID: 15177 RVA: 0x000F1838 File Offset: 0x000EFA38
		public event EventHandler RendererChanged
		{
			add
			{
				base.Events.AddHandler(ToolStripContentPanel.RendererChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripContentPanel.RendererChangedEvent, value);
			}
		}

		/// <summary>This event is not relevant for this class.</summary>
		// Token: 0x1400037B RID: 891
		// (add) Token: 0x06003B4A RID: 15178 RVA: 0x000F184C File Offset: 0x000EFA4C
		// (remove) Token: 0x06003B4B RID: 15179 RVA: 0x000F1858 File Offset: 0x000EFA58
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new event EventHandler TabIndexChanged
		{
			add
			{
				base.TabIndexChanged += value;
			}
			remove
			{
				base.TabIndexChanged -= value;
			}
		}

		/// <summary>This event is not relevant for this class.</summary>
		// Token: 0x1400037C RID: 892
		// (add) Token: 0x06003B4C RID: 15180 RVA: 0x000F1864 File Offset: 0x000EFA64
		// (remove) Token: 0x06003B4D RID: 15181 RVA: 0x000F1870 File Offset: 0x000EFA70
		[EditorBrowsable(1)]
		[Browsable(false)]
		public new event EventHandler TabStopChanged
		{
			add
			{
				base.TabStopChanged += value;
			}
			remove
			{
				base.TabStopChanged -= value;
			}
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.AnchorStyles" />.</returns>
		// Token: 0x17000F78 RID: 3960
		// (get) Token: 0x06003B4E RID: 15182 RVA: 0x000F187C File Offset: 0x000EFA7C
		// (set) Token: 0x06003B4F RID: 15183 RVA: 0x000F1884 File Offset: 0x000EFA84
		[EditorBrowsable(1)]
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public override AnchorStyles Anchor
		{
			get
			{
				return base.Anchor;
			}
			set
			{
				base.Anchor = value;
			}
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>true to enable automatic scrolling; otherwise, false.</returns>
		// Token: 0x17000F79 RID: 3961
		// (get) Token: 0x06003B50 RID: 15184 RVA: 0x000F1890 File Offset: 0x000EFA90
		// (set) Token: 0x06003B51 RID: 15185 RVA: 0x000F1898 File Offset: 0x000EFA98
		[Browsable(false)]
		[EditorBrowsable(1)]
		public override bool AutoScroll
		{
			get
			{
				return base.AutoScroll;
			}
			set
			{
				base.AutoScroll = value;
			}
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" />.</returns>
		// Token: 0x17000F7A RID: 3962
		// (get) Token: 0x06003B52 RID: 15186 RVA: 0x000F18A4 File Offset: 0x000EFAA4
		// (set) Token: 0x06003B53 RID: 15187 RVA: 0x000F18AC File Offset: 0x000EFAAC
		[EditorBrowsable(1)]
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public new Size AutoScrollMargin
		{
			get
			{
				return base.AutoScrollMargin;
			}
			set
			{
				base.AutoScrollMargin = value;
			}
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" />.</returns>
		// Token: 0x17000F7B RID: 3963
		// (get) Token: 0x06003B54 RID: 15188 RVA: 0x000F18B8 File Offset: 0x000EFAB8
		// (set) Token: 0x06003B55 RID: 15189 RVA: 0x000F18C0 File Offset: 0x000EFAC0
		[EditorBrowsable(1)]
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public new Size AutoScrollMinSize
		{
			get
			{
				return base.AutoScrollMinSize;
			}
			set
			{
				base.AutoScrollMinSize = value;
			}
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>true to enable automatic sizing; otherwise, false.</returns>
		// Token: 0x17000F7C RID: 3964
		// (get) Token: 0x06003B56 RID: 15190 RVA: 0x000F18CC File Offset: 0x000EFACC
		// (set) Token: 0x06003B57 RID: 15191 RVA: 0x000F18D4 File Offset: 0x000EFAD4
		[EditorBrowsable(1)]
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public override bool AutoSize
		{
			get
			{
				return base.AutoSize;
			}
			set
			{
				base.AutoSize = value;
			}
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.AutoSizeMode" />.</returns>
		// Token: 0x17000F7D RID: 3965
		// (get) Token: 0x06003B58 RID: 15192 RVA: 0x000F18E0 File Offset: 0x000EFAE0
		// (set) Token: 0x06003B59 RID: 15193 RVA: 0x000F18E8 File Offset: 0x000EFAE8
		[EditorBrowsable(1)]
		[Browsable(false)]
		[Localizable(false)]
		[DesignerSerializationVisibility(0)]
		public override AutoSizeMode AutoSizeMode
		{
			get
			{
				return base.AutoSizeMode;
			}
			set
			{
				base.AutoSizeMode = value;
			}
		}

		/// <summary>Overridden to ensure that the background color of the <see cref="T:System.Windows.Forms.ToolStripContainer" /> reflects the background color of the <see cref="T:System.Windows.Forms.ToolStripContentPanel" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> structure representing the background color of the <see cref="T:System.Windows.Forms.ToolStripContentPanel" />.</returns>
		// Token: 0x17000F7E RID: 3966
		// (get) Token: 0x06003B5A RID: 15194 RVA: 0x000F18F4 File Offset: 0x000EFAF4
		// (set) Token: 0x06003B5B RID: 15195 RVA: 0x000F18FC File Offset: 0x000EFAFC
		public override Color BackColor
		{
			get
			{
				return base.BackColor;
			}
			set
			{
				base.BackColor = value;
				if (base.Parent != null)
				{
					base.Parent.BackColor = value;
				}
			}
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>true if the control causes validation; otherwise, false.</returns>
		// Token: 0x17000F7F RID: 3967
		// (get) Token: 0x06003B5C RID: 15196 RVA: 0x000F1928 File Offset: 0x000EFB28
		// (set) Token: 0x06003B5D RID: 15197 RVA: 0x000F1930 File Offset: 0x000EFB30
		[Browsable(false)]
		[EditorBrowsable(1)]
		[DesignerSerializationVisibility(0)]
		public new bool CausesValidation
		{
			get
			{
				return base.CausesValidation;
			}
			set
			{
				base.CausesValidation = value;
			}
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.DockStyle" />.</returns>
		// Token: 0x17000F80 RID: 3968
		// (get) Token: 0x06003B5E RID: 15198 RVA: 0x000F193C File Offset: 0x000EFB3C
		// (set) Token: 0x06003B5F RID: 15199 RVA: 0x000F1944 File Offset: 0x000EFB44
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
		[EditorBrowsable(1)]
		public override DockStyle Dock
		{
			get
			{
				return base.Dock;
			}
			set
			{
				base.Dock = value;
			}
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>A <see cref="T:System.Drawing.Point" />.</returns>
		// Token: 0x17000F81 RID: 3969
		// (get) Token: 0x06003B60 RID: 15200 RVA: 0x000F1950 File Offset: 0x000EFB50
		// (set) Token: 0x06003B61 RID: 15201 RVA: 0x000F1958 File Offset: 0x000EFB58
		[EditorBrowsable(1)]
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public new Point Location
		{
			get
			{
				return base.Location;
			}
			set
			{
				base.Location = value;
			}
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" />.</returns>
		// Token: 0x17000F82 RID: 3970
		// (get) Token: 0x06003B62 RID: 15202 RVA: 0x000F1964 File Offset: 0x000EFB64
		// (set) Token: 0x06003B63 RID: 15203 RVA: 0x000F196C File Offset: 0x000EFB6C
		[EditorBrowsable(1)]
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public override Size MaximumSize
		{
			get
			{
				return base.MaximumSize;
			}
			set
			{
				base.MaximumSize = value;
			}
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" />.</returns>
		// Token: 0x17000F83 RID: 3971
		// (get) Token: 0x06003B64 RID: 15204 RVA: 0x000F1978 File Offset: 0x000EFB78
		// (set) Token: 0x06003B65 RID: 15205 RVA: 0x000F1980 File Offset: 0x000EFB80
		[EditorBrowsable(1)]
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public override Size MinimumSize
		{
			get
			{
				return base.MinimumSize;
			}
			set
			{
				base.MinimumSize = value;
			}
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>A <see cref="T:System.String" />.</returns>
		// Token: 0x17000F84 RID: 3972
		// (get) Token: 0x06003B66 RID: 15206 RVA: 0x000F198C File Offset: 0x000EFB8C
		// (set) Token: 0x06003B67 RID: 15207 RVA: 0x000F1994 File Offset: 0x000EFB94
		[EditorBrowsable(1)]
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public new string Name
		{
			get
			{
				return base.Name;
			}
			set
			{
				base.Name = value;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Windows.Forms.ToolStripRenderer" /> used to customize the appearance of a <see cref="T:System.Windows.Forms.ToolStripContentPanel" />.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ToolStripRenderer" /> that handles painting.</returns>
		// Token: 0x17000F85 RID: 3973
		// (get) Token: 0x06003B68 RID: 15208 RVA: 0x000F19A0 File Offset: 0x000EFBA0
		// (set) Token: 0x06003B69 RID: 15209 RVA: 0x000F19BC File Offset: 0x000EFBBC
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public ToolStripRenderer Renderer
		{
			get
			{
				if (this.render_mode == ToolStripRenderMode.ManagerRenderMode)
				{
					return ToolStripManager.Renderer;
				}
				return this.renderer;
			}
			set
			{
				if (this.renderer != value)
				{
					this.renderer = value;
					this.render_mode = ToolStripRenderMode.Custom;
					this.OnRendererChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Gets or sets the painting styles to be applied to the <see cref="T:System.Windows.Forms.ToolStripContentPanel" />.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ToolStripRenderMode" /> values. </returns>
		// Token: 0x17000F86 RID: 3974
		// (get) Token: 0x06003B6A RID: 15210 RVA: 0x000F19E4 File Offset: 0x000EFBE4
		// (set) Token: 0x06003B6B RID: 15211 RVA: 0x000F19EC File Offset: 0x000EFBEC
		public ToolStripRenderMode RenderMode
		{
			get
			{
				return this.render_mode;
			}
			set
			{
				if (!Enum.IsDefined(typeof(ToolStripRenderMode), value))
				{
					throw new InvalidEnumArgumentException(string.Format("Enum argument value '{0}' is not valid for ToolStripRenderMode", value));
				}
				if (value == ToolStripRenderMode.Custom && this.renderer == null)
				{
					throw new NotSupportedException("Must set Renderer property before setting RenderMode to Custom");
				}
				if (value == ToolStripRenderMode.Professional)
				{
					this.renderer = new ToolStripProfessionalRenderer();
				}
				else if (value == ToolStripRenderMode.System)
				{
					this.renderer = new ToolStripSystemRenderer();
				}
				this.render_mode = value;
			}
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>An <see cref="T:System.Int32" />.</returns>
		// Token: 0x17000F87 RID: 3975
		// (get) Token: 0x06003B6C RID: 15212 RVA: 0x000F1A78 File Offset: 0x000EFC78
		// (set) Token: 0x06003B6D RID: 15213 RVA: 0x000F1A80 File Offset: 0x000EFC80
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new int TabIndex
		{
			get
			{
				return base.TabIndex;
			}
			set
			{
				base.TabIndex = value;
			}
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>true if the <see cref="T:System.Windows.Forms.ToolStripContentPanel" /> can be tabbed to; otherwise, false.</returns>
		// Token: 0x17000F88 RID: 3976
		// (get) Token: 0x06003B6E RID: 15214 RVA: 0x000F1A8C File Offset: 0x000EFC8C
		// (set) Token: 0x06003B6F RID: 15215 RVA: 0x000F1A94 File Offset: 0x000EFC94
		[DesignerSerializationVisibility(0)]
		[EditorBrowsable(1)]
		[Browsable(false)]
		public new bool TabStop
		{
			get
			{
				return base.TabStop;
			}
			set
			{
				base.TabStop = value;
			}
		}

		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06003B70 RID: 15216 RVA: 0x000F1AA0 File Offset: 0x000EFCA0
		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Form.Load" /> event.</summary>
		/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06003B71 RID: 15217 RVA: 0x000F1AAC File Offset: 0x000EFCAC
		[EditorBrowsable(2)]
		protected virtual void OnLoad(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ToolStripContentPanel.LoadEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Renders the <see cref="T:System.Windows.Forms.ToolStripContentPanel" />.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
		// Token: 0x06003B72 RID: 15218 RVA: 0x000F1AE0 File Offset: 0x000EFCE0
		[EditorBrowsable(2)]
		protected override void OnPaintBackground(PaintEventArgs e)
		{
			base.OnPaintBackground(e);
			this.Renderer.DrawToolStripContentPanelBackground(new ToolStripContentPanelRenderEventArgs(e.Graphics, this));
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripContentPanel.RendererChanged" /> event. </summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06003B73 RID: 15219 RVA: 0x000F1B0C File Offset: 0x000EFD0C
		protected virtual void OnRendererChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ToolStripContentPanel.RendererChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		// Token: 0x04001A51 RID: 6737
		private ToolStripRenderMode render_mode;

		// Token: 0x04001A52 RID: 6738
		private ToolStripRenderer renderer;
	}
}
