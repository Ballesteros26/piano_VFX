using System;
using System.ComponentModel;

namespace System.Windows.Forms
{
	/// <summary>Encapsulates properties related to scrolling. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020002CF RID: 719
	public abstract class ScrollProperties
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ScrollProperties" /> class. </summary>
		/// <param name="container">The <see cref="T:System.Windows.Forms.ScrollableControl" /> whose scrolling properties this object describes.</param>
		// Token: 0x06002F93 RID: 12179 RVA: 0x000B7C88 File Offset: 0x000B5E88
		protected ScrollProperties(ScrollableControl container)
		{
			this.parentControl = container;
		}

		/// <summary>Gets or sets whether the scroll bar can be used on the container.</summary>
		/// <returns>true if the scroll bar can be used; otherwise, false. </returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000C0E RID: 3086
		// (get) Token: 0x06002F94 RID: 12180 RVA: 0x000B7C98 File Offset: 0x000B5E98
		// (set) Token: 0x06002F95 RID: 12181 RVA: 0x000B7CA8 File Offset: 0x000B5EA8
		[DefaultValue(true)]
		public bool Enabled
		{
			get
			{
				return this.scroll_bar.Enabled;
			}
			set
			{
				this.scroll_bar.Enabled = value;
			}
		}

		/// <summary>Gets or sets the distance to move a scroll bar in response to a large scroll command. </summary>
		/// <returns>An <see cref="T:System.Int32" /> describing how far, in pixels, to move the scroll bar in response to a large change.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <see cref="P:System.Windows.Forms.ScrollProperties.LargeChange" /> cannot be less than zero. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000C0F RID: 3087
		// (get) Token: 0x06002F96 RID: 12182 RVA: 0x000B7CB8 File Offset: 0x000B5EB8
		// (set) Token: 0x06002F97 RID: 12183 RVA: 0x000B7CC8 File Offset: 0x000B5EC8
		[RefreshProperties(2)]
		[DefaultValue(10)]
		public int LargeChange
		{
			get
			{
				return this.scroll_bar.LargeChange;
			}
			set
			{
				this.scroll_bar.LargeChange = value;
			}
		}

		/// <summary>Gets or sets the upper limit of the scrollable range. </summary>
		/// <returns>An <see cref="T:System.Int32" /> representing the maximum range of the scroll bar.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000C10 RID: 3088
		// (get) Token: 0x06002F98 RID: 12184 RVA: 0x000B7CD8 File Offset: 0x000B5ED8
		// (set) Token: 0x06002F99 RID: 12185 RVA: 0x000B7CE8 File Offset: 0x000B5EE8
		[RefreshProperties(2)]
		[DefaultValue(100)]
		public int Maximum
		{
			get
			{
				return this.scroll_bar.Maximum;
			}
			set
			{
				this.scroll_bar.Maximum = value;
			}
		}

		/// <summary>Gets or sets the lower limit of the scrollable range. </summary>
		/// <returns>An <see cref="T:System.Int32" /> representing the lower range of the scroll bar.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <see cref="P:System.Windows.Forms.ScrollProperties.Minimum" /> cannot be less than zero. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000C11 RID: 3089
		// (get) Token: 0x06002F9A RID: 12186 RVA: 0x000B7CF8 File Offset: 0x000B5EF8
		// (set) Token: 0x06002F9B RID: 12187 RVA: 0x000B7D08 File Offset: 0x000B5F08
		[DefaultValue(0)]
		[RefreshProperties(2)]
		public int Minimum
		{
			get
			{
				return this.scroll_bar.Minimum;
			}
			set
			{
				this.scroll_bar.Minimum = value;
			}
		}

		/// <summary>Gets or sets the distance to move a scroll bar in response to a small scroll command. </summary>
		/// <returns>An <see cref="T:System.Int32" /> representing how far, in pixels, to move the scroll bar.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000C12 RID: 3090
		// (get) Token: 0x06002F9C RID: 12188 RVA: 0x000B7D18 File Offset: 0x000B5F18
		// (set) Token: 0x06002F9D RID: 12189 RVA: 0x000B7D28 File Offset: 0x000B5F28
		[DefaultValue(1)]
		public int SmallChange
		{
			get
			{
				return this.scroll_bar.SmallChange;
			}
			set
			{
				this.scroll_bar.SmallChange = value;
			}
		}

		/// <summary>Gets or sets a numeric value that represents the current position of the scroll bar box.</summary>
		/// <returns>An <see cref="T:System.Int32" /> representing the position of the scroll bar box, in pixels. </returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000C13 RID: 3091
		// (get) Token: 0x06002F9E RID: 12190 RVA: 0x000B7D38 File Offset: 0x000B5F38
		// (set) Token: 0x06002F9F RID: 12191 RVA: 0x000B7D48 File Offset: 0x000B5F48
		[Bindable(true)]
		[DefaultValue(0)]
		public int Value
		{
			get
			{
				return this.scroll_bar.Value;
			}
			set
			{
				this.scroll_bar.Value = value;
			}
		}

		/// <summary>Gets or sets whether the scroll bar can be seen by the user.</summary>
		/// <returns>true if it can be seen; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000C14 RID: 3092
		// (get) Token: 0x06002FA0 RID: 12192 RVA: 0x000B7D58 File Offset: 0x000B5F58
		// (set) Token: 0x06002FA1 RID: 12193 RVA: 0x000B7D68 File Offset: 0x000B5F68
		[DefaultValue(false)]
		public bool Visible
		{
			get
			{
				return this.scroll_bar.Visible;
			}
			set
			{
				this.scroll_bar.Visible = value;
			}
		}

		/// <summary>Gets the control to which this scroll information applies.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ScrollableControl" />.</returns>
		// Token: 0x17000C15 RID: 3093
		// (get) Token: 0x06002FA2 RID: 12194 RVA: 0x000B7D78 File Offset: 0x000B5F78
		protected ScrollableControl ParentControl
		{
			get
			{
				return this.parentControl;
			}
		}

		// Token: 0x040016CC RID: 5836
		private ScrollableControl parentControl;

		// Token: 0x040016CD RID: 5837
		internal ScrollBar scroll_bar;
	}
}
