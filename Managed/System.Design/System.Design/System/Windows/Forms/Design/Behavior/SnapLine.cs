using System;

namespace System.Windows.Forms.Design.Behavior
{
	/// <summary>Represents the horizontal and vertical line segments that are dynamically created in the user interface (UI) to assist in the design-time layout of controls in a container. This class cannot be inherited.</summary>
	// Token: 0x0200004D RID: 77
	public sealed class SnapLine
	{
		/// <summary>Returns a value indicating whether the specified <see cref="T:System.Windows.Forms.Design.Behavior.SnapLine" /> should snap to another <see cref="T:System.Windows.Forms.Design.Behavior.SnapLine" />.</summary>
		/// <returns>true if <paramref name="line1" /> should snap to <paramref name="line2" />; otherwise, false.</returns>
		/// <param name="line1">The specified <see cref="T:System.Windows.Forms.Design.Behavior.SnapLine" />.</param>
		/// <param name="line2">The <see cref="T:System.Windows.Forms.Design.Behavior.SnapLine" /> to which the specified <see cref="T:System.Windows.Forms.Design.Behavior.SnapLine" /> is expected to snap.</param>
		// Token: 0x06000290 RID: 656 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public static bool ShouldSnap(SnapLine line1, SnapLine line2)
		{
			throw new NotImplementedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Design.Behavior.SnapLine" /> class using the specified snapline type and offset.</summary>
		/// <param name="type">The <see cref="T:System.Windows.Forms.Design.Behavior.SnapLineType" /> to create. Describes the relative position and orientation of the snapline.</param>
		/// <param name="offset">The position of the snapline, in pixels, relative to the upper-left origin of the owning control.</param>
		// Token: 0x06000291 RID: 657 RVA: 0x00008CD8 File Offset: 0x00006ED8
		[MonoTODO]
		public SnapLine(SnapLineType type, int offset)
			: this(type, offset, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Design.Behavior.SnapLine" /> class using the specified snapline type, offset, and filter name. </summary>
		/// <param name="type">The <see cref="T:System.Windows.Forms.Design.Behavior.SnapLineType" /> to create. Describes the relative position and orientation of the snapline.</param>
		/// <param name="offset">The position of the snapline, in pixels, relative to the upper-left origin of the owning control.</param>
		/// <param name="filter">A <see cref="T:System.String" /> used to specify a programmer-defined category of snaplines.</param>
		// Token: 0x06000292 RID: 658 RVA: 0x00008CE3 File Offset: 0x00006EE3
		[MonoTODO]
		public SnapLine(SnapLineType type, int offset, string filter)
			: this(type, offset, filter, (SnapLinePriority)0)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Design.Behavior.SnapLine" /> class using the specified snapline type, offset, and priority. </summary>
		/// <param name="type">The <see cref="T:System.Windows.Forms.Design.Behavior.SnapLineType" /> to create. Describes the relative position and orientation of the snapline.</param>
		/// <param name="offset">The position of the snapline, in pixels, relative to the upper-left origin of the owning control.</param>
		/// <param name="priority">The <see cref="T:System.Windows.Forms.Design.Behavior.SnapLinePriority" /> of the snapline.</param>
		// Token: 0x06000293 RID: 659 RVA: 0x00008CEF File Offset: 0x00006EEF
		[MonoTODO]
		public SnapLine(SnapLineType type, int offset, SnapLinePriority priority)
			: this(type, offset, null, priority)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Design.Behavior.SnapLine" /> class using the specified snapline type, offset, filter name, and priority. </summary>
		/// <param name="type">The <see cref="T:System.Windows.Forms.Design.Behavior.SnapLineType" /> to create. Describes the relative position and orientation of the snapline.</param>
		/// <param name="offset">The position of the snapline, in pixels, relative to the upper-left origin of the owning control.</param>
		/// <param name="filter">A <see cref="T:System.String" /> used to specify a programmer-defined category of snaplines.</param>
		/// <param name="priority">The <see cref="T:System.Windows.Forms.Design.Behavior.SnapLinePriority" /> of the snapline.</param>
		// Token: 0x06000294 RID: 660 RVA: 0x00008CFB File Offset: 0x00006EFB
		[MonoTODO]
		public SnapLine(SnapLineType type, int offset, string filter, SnapLinePriority priority)
		{
			this.type = type;
			this.offset = offset;
			this.filter = filter;
			this.priority = priority;
		}

		/// <summary>Gets the programmer-defined filter category associated with this snapline.</summary>
		/// <returns>A <see cref="T:System.String" /> that defines the filter category. The default is null.</returns>
		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000295 RID: 661 RVA: 0x00008D20 File Offset: 0x00006F20
		public string Filter
		{
			get
			{
				return this.filter;
			}
		}

		/// <summary>Gets a value indicating whether the snapline has a horizontal orientation.</summary>
		/// <returns>true if the snapline is horizontal; otherwise, false.</returns>
		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000296 RID: 662 RVA: 0x00008D28 File Offset: 0x00006F28
		public bool IsHorizontal
		{
			get
			{
				switch (this.SnapLineType)
				{
				case SnapLineType.Top:
				case SnapLineType.Bottom:
				case SnapLineType.Horizontal:
				case SnapLineType.Baseline:
					return true;
				}
				return false;
			}
		}

		/// <summary>Gets a value indicating whether the snapline has a vertical orientation.</summary>
		/// <returns>true if the snapline is vertical; otherwise, false.</returns>
		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000297 RID: 663 RVA: 0x00008D64 File Offset: 0x00006F64
		public bool IsVertical
		{
			get
			{
				SnapLineType snapLineType = this.SnapLineType;
				return snapLineType - SnapLineType.Left <= 1 || snapLineType == SnapLineType.Vertical;
			}
		}

		/// <summary>Gets the number of pixels that the snapline is offset from the origin of the associated control.</summary>
		/// <returns>The offset, in pixels, of the snapline. </returns>
		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000298 RID: 664 RVA: 0x00008D85 File Offset: 0x00006F85
		public int Offset
		{
			get
			{
				return this.offset;
			}
		}

		/// <summary>Gets a value indicating the relative importance of the snapline.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Design.Behavior.SnapLinePriority" /> that represents the priority category of a snapline.</returns>
		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000299 RID: 665 RVA: 0x00008D8D File Offset: 0x00006F8D
		public SnapLinePriority Priority
		{
			get
			{
				return this.priority;
			}
		}

		/// <summary>Gets the type of a snapline, which indicates the general location and orientation.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Design.Behavior.SnapLineType" /> that represents the orientation and general location, relative to control edges, of a snapline.</returns>
		// Token: 0x17000084 RID: 132
		// (get) Token: 0x0600029A RID: 666 RVA: 0x00008D95 File Offset: 0x00006F95
		public SnapLineType SnapLineType
		{
			get
			{
				return this.type;
			}
		}

		/// <summary>Adjusts the <see cref="P:System.Windows.Forms.Design.Behavior.SnapLine.Offset" /> property of the snapline.</summary>
		/// <param name="adjustment">The number of pixels to change the snapline offset by.</param>
		// Token: 0x0600029B RID: 667 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public void AdjustOffset(int adjustment)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns a string representation of the current snapline.</summary>
		/// <returns>A <see cref="T:System.String" /> that represents the current <see cref="T:System.Windows.Forms.Design.Behavior.SnapLine" />.</returns>
		// Token: 0x0600029C RID: 668 RVA: 0x00005153 File Offset: 0x00003353
		[MonoTODO]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x04000105 RID: 261
		private SnapLineType type;

		// Token: 0x04000106 RID: 262
		private int offset;

		// Token: 0x04000107 RID: 263
		private string filter;

		// Token: 0x04000108 RID: 264
		private SnapLinePriority priority;
	}
}
