using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the Scroll event.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020002CC RID: 716
	[ComVisible(true)]
	public class ScrollEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ScrollEventArgs" /> class using the given values for the <see cref="P:System.Windows.Forms.ScrollEventArgs.Type" /> and <see cref="P:System.Windows.Forms.ScrollEventArgs.NewValue" /> properties.</summary>
		/// <param name="type">One of the <see cref="T:System.Windows.Forms.ScrollEventType" /> values. </param>
		/// <param name="newValue">The new value for the scroll bar. </param>
		// Token: 0x06002F8A RID: 12170 RVA: 0x000B7C10 File Offset: 0x000B5E10
		public ScrollEventArgs(ScrollEventType type, int newValue)
			: this(type, -1, newValue, ScrollOrientation.HorizontalScroll)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ScrollEventArgs" /> class using the given values for the <see cref="P:System.Windows.Forms.ScrollEventArgs.Type" />, <see cref="P:System.Windows.Forms.ScrollEventArgs.OldValue" />, and <see cref="P:System.Windows.Forms.ScrollEventArgs.NewValue" /> properties.</summary>
		/// <param name="type">One of the <see cref="T:System.Windows.Forms.ScrollEventType" /> values. </param>
		/// <param name="oldValue">The old value for the scroll bar. </param>
		/// <param name="newValue">The new value for the scroll bar. </param>
		// Token: 0x06002F8B RID: 12171 RVA: 0x000B7C1C File Offset: 0x000B5E1C
		public ScrollEventArgs(ScrollEventType type, int oldValue, int newValue)
			: this(type, oldValue, newValue, ScrollOrientation.HorizontalScroll)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ScrollEventArgs" /> class using the given values for the <see cref="P:System.Windows.Forms.ScrollEventArgs.Type" />, <see cref="P:System.Windows.Forms.ScrollEventArgs.NewValue" />, and <see cref="P:System.Windows.Forms.ScrollEventArgs.ScrollOrientation" /> properties.</summary>
		/// <param name="type">One of the <see cref="T:System.Windows.Forms.ScrollEventType" /> values. </param>
		/// <param name="newValue">The new value for the scroll bar. </param>
		/// <param name="scroll">One of the <see cref="T:System.Windows.Forms.ScrollOrientation" /> values. </param>
		// Token: 0x06002F8C RID: 12172 RVA: 0x000B7C28 File Offset: 0x000B5E28
		public ScrollEventArgs(ScrollEventType type, int newValue, ScrollOrientation scroll)
			: this(type, -1, newValue, scroll)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ScrollEventArgs" /> class using the given values for the <see cref="P:System.Windows.Forms.ScrollEventArgs.Type" />, <see cref="P:System.Windows.Forms.ScrollEventArgs.OldValue" />, <see cref="P:System.Windows.Forms.ScrollEventArgs.NewValue" />, and <see cref="P:System.Windows.Forms.ScrollEventArgs.ScrollOrientation" /> properties.</summary>
		/// <param name="type">One of the <see cref="T:System.Windows.Forms.ScrollEventType" /> values. </param>
		/// <param name="oldValue">The old value for the scroll bar. </param>
		/// <param name="newValue">The new value for the scroll bar. </param>
		/// <param name="scroll">One of the <see cref="T:System.Windows.Forms.ScrollOrientation" /> values. </param>
		// Token: 0x06002F8D RID: 12173 RVA: 0x000B7C34 File Offset: 0x000B5E34
		public ScrollEventArgs(ScrollEventType type, int oldValue, int newValue, ScrollOrientation scroll)
		{
			this.new_value = newValue;
			this.old_value = oldValue;
			this.scroll_orientation = scroll;
			this.type = type;
		}

		/// <summary>Gets or sets the new <see cref="P:System.Windows.Forms.ScrollBar.Value" /> of the scroll bar.</summary>
		/// <returns>The numeric value that the <see cref="P:System.Windows.Forms.ScrollBar.Value" /> property will be changed to.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000C0A RID: 3082
		// (get) Token: 0x06002F8E RID: 12174 RVA: 0x000B7C5C File Offset: 0x000B5E5C
		// (set) Token: 0x06002F8F RID: 12175 RVA: 0x000B7C64 File Offset: 0x000B5E64
		public int NewValue
		{
			get
			{
				return this.new_value;
			}
			set
			{
				this.new_value = value;
			}
		}

		/// <summary>Gets the old <see cref="P:System.Windows.Forms.ScrollBar.Value" /> of the scroll bar.</summary>
		/// <returns>The numeric value that the <see cref="P:System.Windows.Forms.ScrollBar.Value" /> property contained before it changed.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000C0B RID: 3083
		// (get) Token: 0x06002F90 RID: 12176 RVA: 0x000B7C70 File Offset: 0x000B5E70
		public int OldValue
		{
			get
			{
				return this.old_value;
			}
		}

		/// <summary>Gets the scroll bar orientation that raised the Scroll event.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ScrollOrientation" /> values.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000C0C RID: 3084
		// (get) Token: 0x06002F91 RID: 12177 RVA: 0x000B7C78 File Offset: 0x000B5E78
		public ScrollOrientation ScrollOrientation
		{
			get
			{
				return this.scroll_orientation;
			}
		}

		/// <summary>Gets the type of scroll event that occurred.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ScrollEventType" /> values.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000C0D RID: 3085
		// (get) Token: 0x06002F92 RID: 12178 RVA: 0x000B7C80 File Offset: 0x000B5E80
		public ScrollEventType Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x040016BB RID: 5819
		private ScrollEventType type;

		// Token: 0x040016BC RID: 5820
		private int new_value;

		// Token: 0x040016BD RID: 5821
		private int old_value;

		// Token: 0x040016BE RID: 5822
		private ScrollOrientation scroll_orientation;
	}
}
