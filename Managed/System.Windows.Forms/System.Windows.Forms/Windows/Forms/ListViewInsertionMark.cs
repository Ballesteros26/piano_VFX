using System;
using System.Drawing;

namespace System.Windows.Forms
{
	/// <summary>Used to indicate the expected drop location when an item is dragged to a new position in a <see cref="T:System.Windows.Forms.ListView" /> control. This functionality is available only on Windows XP and later.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200022F RID: 559
	public sealed class ListViewInsertionMark
	{
		// Token: 0x0600247E RID: 9342 RVA: 0x000899C0 File Offset: 0x00087BC0
		internal ListViewInsertionMark(ListView listview)
		{
			this.listview_owner = listview;
		}

		/// <summary>Gets or sets a value indicating whether the insertion mark appears to the right of the item with the index specified by the <see cref="P:System.Windows.Forms.ListViewInsertionMark.Index" /> property.</summary>
		/// <returns>true if the insertion mark appears to the right of the item with the index specified by the <see cref="P:System.Windows.Forms.ListViewInsertionMark.Index" /> property; otherwise, false. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170008F9 RID: 2297
		// (get) Token: 0x0600247F RID: 9343 RVA: 0x000899D0 File Offset: 0x00087BD0
		// (set) Token: 0x06002480 RID: 9344 RVA: 0x000899D8 File Offset: 0x00087BD8
		public bool AppearsAfterItem
		{
			get
			{
				return this.appears_after_item;
			}
			set
			{
				if (value == this.appears_after_item)
				{
					return;
				}
				this.appears_after_item = value;
				this.listview_owner.item_control.Invalidate(this.bounds);
				this.UpdateBounds();
				this.listview_owner.item_control.Invalidate(this.bounds);
			}
		}

		/// <summary>Gets the bounding rectangle of the insertion mark.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the position and size of the insertion mark.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170008FA RID: 2298
		// (get) Token: 0x06002481 RID: 9345 RVA: 0x00089A2C File Offset: 0x00087C2C
		public Rectangle Bounds
		{
			get
			{
				return this.bounds;
			}
		}

		/// <summary>Gets or sets the color of the insertion mark.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> value that represents the color of the insertion mark. The default value is the value of the <see cref="P:System.Windows.Forms.ListView.ForeColor" /> property.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170008FB RID: 2299
		// (get) Token: 0x06002482 RID: 9346 RVA: 0x00089A34 File Offset: 0x00087C34
		// (set) Token: 0x06002483 RID: 9347 RVA: 0x00089A74 File Offset: 0x00087C74
		public Color Color
		{
			get
			{
				Color? color = this.color;
				return (color != null) ? this.color.Value : this.listview_owner.ForeColor;
			}
			set
			{
				this.color = new Color?(value);
			}
		}

		/// <summary>Gets or sets the index of the item next to which the insertion mark appears.</summary>
		/// <returns>The index of the item next to which the insertion mark appears or -1 when the insertion mark is hidden.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170008FC RID: 2300
		// (get) Token: 0x06002484 RID: 9348 RVA: 0x00089A84 File Offset: 0x00087C84
		// (set) Token: 0x06002485 RID: 9349 RVA: 0x00089A8C File Offset: 0x00087C8C
		public int Index
		{
			get
			{
				return this.index;
			}
			set
			{
				if (value == this.index)
				{
					return;
				}
				this.index = value;
				this.listview_owner.item_control.Invalidate(this.bounds);
				this.UpdateBounds();
				this.listview_owner.item_control.Invalidate(this.bounds);
			}
		}

		// Token: 0x06002486 RID: 9350 RVA: 0x00089AE0 File Offset: 0x00087CE0
		private void UpdateBounds()
		{
			if (this.index < 0 || this.index >= this.listview_owner.Items.Count)
			{
				this.bounds = Rectangle.Empty;
				return;
			}
			Rectangle rectangle = this.listview_owner.Items[this.index].Bounds;
			int num = ((!this.appears_after_item) ? rectangle.Left : rectangle.Right) - 2;
			int num2 = rectangle.Height + ThemeEngine.Current.ListViewVerticalSpacing;
			this.bounds = new Rectangle(num, rectangle.Top, 7, num2);
		}

		/// <summary>Retrieves the index of the item closest to the specified point.</summary>
		/// <returns>The index of the item closest to the specified point or -1 if the closest item is the item currently being dragged.</returns>
		/// <param name="pt">A <see cref="T:System.Drawing.Point" /> representing the location from which to find the nearest item. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002487 RID: 9351 RVA: 0x00089B88 File Offset: 0x00087D88
		public int NearestIndex(Point pt)
		{
			double num = double.MaxValue;
			int num2 = -1;
			for (int i = 0; i < this.listview_owner.Items.Count; i++)
			{
				Point itemLocation = this.listview_owner.GetItemLocation(i);
				double num3 = Math.Pow((double)(itemLocation.X - pt.X), 2.0) + Math.Pow((double)(itemLocation.Y - pt.Y), 2.0);
				if (num3 < num)
				{
					num = num3;
					num2 = i;
				}
			}
			if (this.listview_owner.item_control.dragged_item_index == num2)
			{
				return -1;
			}
			return num2;
		}

		// Token: 0x170008FD RID: 2301
		// (get) Token: 0x06002488 RID: 9352 RVA: 0x00089C34 File Offset: 0x00087E34
		internal PointF[] TopTriangle
		{
			get
			{
				PointF pointF;
				pointF..ctor((float)this.bounds.X, (float)this.bounds.Y);
				PointF pointF2;
				pointF2..ctor((float)this.bounds.Right, (float)this.bounds.Y);
				PointF pointF3;
				pointF3..ctor((float)(this.bounds.X + (this.bounds.Right - this.bounds.X) / 2), (float)(this.bounds.Y + 5));
				return new PointF[] { pointF, pointF2, pointF3 };
			}
		}

		// Token: 0x170008FE RID: 2302
		// (get) Token: 0x06002489 RID: 9353 RVA: 0x00089CE8 File Offset: 0x00087EE8
		internal PointF[] BottomTriangle
		{
			get
			{
				PointF pointF;
				pointF..ctor((float)this.bounds.X, (float)this.bounds.Bottom);
				PointF pointF2;
				pointF2..ctor((float)this.bounds.Right, (float)this.bounds.Bottom);
				PointF pointF3;
				pointF3..ctor((float)(this.bounds.X + (this.bounds.Right - this.bounds.X) / 2), (float)(this.bounds.Bottom - 5));
				return new PointF[] { pointF, pointF2, pointF3 };
			}
		}

		// Token: 0x170008FF RID: 2303
		// (get) Token: 0x0600248A RID: 9354 RVA: 0x00089D9C File Offset: 0x00087F9C
		internal Rectangle Line
		{
			get
			{
				return new Rectangle(this.bounds.X + 2, this.bounds.Y + 2, 2, this.bounds.Height - 5);
			}
		}

		// Token: 0x040012A7 RID: 4775
		private ListView listview_owner;

		// Token: 0x040012A8 RID: 4776
		private bool appears_after_item;

		// Token: 0x040012A9 RID: 4777
		private Rectangle bounds;

		// Token: 0x040012AA RID: 4778
		private Color? color;

		// Token: 0x040012AB RID: 4779
		private int index;
	}
}
