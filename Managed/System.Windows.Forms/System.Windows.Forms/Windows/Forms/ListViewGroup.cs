using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.Serialization;

namespace System.Windows.Forms
{
	/// <summary>Represents a group of items displayed within a <see cref="T:System.Windows.Forms.ListView" /> control.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200022A RID: 554
	[DefaultProperty("Header")]
	[DesignTimeVisible(false)]
	[ToolboxItem(false)]
	[TypeConverter(typeof(ListViewGroupConverter))]
	[Serializable]
	public sealed class ListViewGroup : ISerializable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListViewGroup" /> class using the default header text of "ListViewGroup" and the default left header alignment.</summary>
		// Token: 0x06002437 RID: 9271 RVA: 0x00088E48 File Offset: 0x00087048
		public ListViewGroup()
			: this("ListViewGroup", HorizontalAlignment.Left)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListViewGroup" /> class using the specified value to initialize the <see cref="P:System.Windows.Forms.ListViewGroup.Header" /> property and using the default left header alignment.</summary>
		/// <param name="header">The text to display for the group header. </param>
		// Token: 0x06002438 RID: 9272 RVA: 0x00088E58 File Offset: 0x00087058
		public ListViewGroup(string header)
			: this(header, HorizontalAlignment.Left)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListViewGroup" /> class using the specified values to initialize the <see cref="P:System.Windows.Forms.ListViewGroup.Name" /> and <see cref="P:System.Windows.Forms.ListViewGroup.Header" /> properties. </summary>
		/// <param name="key">The initial value of the <see cref="P:System.Windows.Forms.ListViewGroup.Name" /> property.</param>
		/// <param name="headerText">The initial value of the <see cref="P:System.Windows.Forms.ListViewGroup.Header" /> property.</param>
		// Token: 0x06002439 RID: 9273 RVA: 0x00088E64 File Offset: 0x00087064
		public ListViewGroup(string key, string headerText)
			: this(headerText, HorizontalAlignment.Left)
		{
			this.name = key;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListViewGroup" /> class using the specified header text and the specified header alignment.</summary>
		/// <param name="header">The text to display for the group header. </param>
		/// <param name="headerAlignment">One of the <see cref="T:System.Windows.Forms.HorizontalAlignment" /> values that specifies the alignment of the header text. </param>
		// Token: 0x0600243A RID: 9274 RVA: 0x00088E78 File Offset: 0x00087078
		public ListViewGroup(string header, HorizontalAlignment headerAlignment)
		{
			this.header = string.Empty;
			this.header_bounds = Rectangle.Empty;
			base..ctor();
			this.header = header;
			this.header_alignment = headerAlignment;
			this.items = new ListView.ListViewItemCollection(this.list_view_owner, this);
		}

		// Token: 0x0600243B RID: 9275 RVA: 0x00088EC4 File Offset: 0x000870C4
		private ListViewGroup(SerializationInfo info, StreamingContext context)
		{
			this.header = string.Empty;
			this.header_bounds = Rectangle.Empty;
			base..ctor();
			this.header = info.GetString("Header");
			this.name = info.GetString("Name");
			this.header_alignment = (HorizontalAlignment)info.GetInt32("HeaderAlignment");
			this.tag = info.GetValue("Tag", typeof(object));
			int @int = info.GetInt32("ListViewItemCount");
			if (@int > 0)
			{
				if (this.items == null)
				{
					this.items = new ListView.ListViewItemCollection(this.list_view_owner);
				}
				for (int i = 0; i < @int; i++)
				{
					this.items.Add((ListViewItem)info.GetValue(string.Format("ListViewItem_{0}", i), typeof(ListViewItem)));
				}
			}
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data needed to serialize the target object.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data.</param>
		/// <param name="context">The destination (see <see cref="T:System.Runtime.Serialization.StreamingContext" />) for this serialization.</param>
		// Token: 0x0600243C RID: 9276 RVA: 0x00088FB0 File Offset: 0x000871B0
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("Header", this.header);
			info.AddValue("Name", this.name);
			info.AddValue("HeaderAlignment", this.header_alignment);
			info.AddValue("Tag", this.tag);
			info.AddValue("ListViewItemCount", this.items.Count);
			int num = 0;
			foreach (object obj in this.items)
			{
				ListViewItem listViewItem = (ListViewItem)obj;
				info.AddValue(string.Format("ListViewItem_{0}", num), listViewItem);
				num++;
			}
		}

		/// <summary>Gets or sets the header text for the group.</summary>
		/// <returns>The text to display for the group header. The default is "ListViewGroup".</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170008E1 RID: 2273
		// (get) Token: 0x0600243D RID: 9277 RVA: 0x00089094 File Offset: 0x00087294
		// (set) Token: 0x0600243E RID: 9278 RVA: 0x0008909C File Offset: 0x0008729C
		public string Header
		{
			get
			{
				return this.header;
			}
			set
			{
				if (!this.header.Equals(value))
				{
					this.header = value;
					if (this.list_view_owner != null)
					{
						this.list_view_owner.Redraw(true);
					}
				}
			}
		}

		/// <summary>Gets or sets the alignment of the group header text.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.HorizontalAlignment" /> values that specifies the alignment of the header text. The default is <see cref="F:System.Windows.Forms.HorizontalAlignment.Left" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value when setting this property is not a valid <see cref="T:System.Windows.Forms.HorizontalAlignment" /> value.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170008E2 RID: 2274
		// (get) Token: 0x0600243F RID: 9279 RVA: 0x000890D0 File Offset: 0x000872D0
		// (set) Token: 0x06002440 RID: 9280 RVA: 0x000890D8 File Offset: 0x000872D8
		[DefaultValue(HorizontalAlignment.Left)]
		public HorizontalAlignment HeaderAlignment
		{
			get
			{
				return this.header_alignment;
			}
			set
			{
				if (!this.header_alignment.Equals(value))
				{
					if (value != HorizontalAlignment.Left && value != HorizontalAlignment.Right && value != HorizontalAlignment.Center)
					{
						throw new InvalidEnumArgumentException("HeaderAlignment", (int)value, typeof(HorizontalAlignment));
					}
					this.header_alignment = value;
					if (this.list_view_owner != null)
					{
						this.list_view_owner.Redraw(true);
					}
				}
			}
		}

		/// <summary>Gets a collection containing all items associated with this group.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ListView.ListViewItemCollection" /> that contains all the items in the group. If there are no items in the group, an empty <see cref="T:System.Windows.Forms.ListView.ListViewItemCollection" /> object is returned.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170008E3 RID: 2275
		// (get) Token: 0x06002441 RID: 9281 RVA: 0x00089148 File Offset: 0x00087348
		[Browsable(false)]
		public ListView.ListViewItemCollection Items
		{
			get
			{
				return this.items;
			}
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.ListView" /> control that contains this group. </summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ListView" /> control that contains this group.</returns>
		// Token: 0x170008E4 RID: 2276
		// (get) Token: 0x06002442 RID: 9282 RVA: 0x00089150 File Offset: 0x00087350
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public ListView ListView
		{
			get
			{
				return this.list_view_owner;
			}
		}

		// Token: 0x170008E5 RID: 2277
		// (get) Token: 0x06002443 RID: 9283 RVA: 0x00089158 File Offset: 0x00087358
		// (set) Token: 0x06002444 RID: 9284 RVA: 0x00089160 File Offset: 0x00087360
		internal ListView ListViewOwner
		{
			get
			{
				return this.list_view_owner;
			}
			set
			{
				this.list_view_owner = value;
				if (!this.is_default_group)
				{
					this.items.Owner = value;
				}
			}
		}

		// Token: 0x170008E6 RID: 2278
		// (get) Token: 0x06002445 RID: 9285 RVA: 0x00089180 File Offset: 0x00087380
		// (set) Token: 0x06002446 RID: 9286 RVA: 0x000891C8 File Offset: 0x000873C8
		internal Rectangle HeaderBounds
		{
			get
			{
				Rectangle rectangle = this.header_bounds;
				rectangle.X -= this.list_view_owner.h_marker;
				rectangle.Y -= this.list_view_owner.v_marker;
				return rectangle;
			}
			set
			{
				if (this.list_view_owner != null)
				{
					this.list_view_owner.item_control.Invalidate(this.HeaderBounds);
				}
				this.header_bounds = value;
				if (this.list_view_owner != null)
				{
					this.list_view_owner.item_control.Invalidate(this.HeaderBounds);
				}
			}
		}

		// Token: 0x170008E7 RID: 2279
		// (get) Token: 0x06002447 RID: 9287 RVA: 0x00089220 File Offset: 0x00087420
		// (set) Token: 0x06002448 RID: 9288 RVA: 0x00089228 File Offset: 0x00087428
		internal bool IsDefault
		{
			get
			{
				return this.is_default_group;
			}
			set
			{
				this.is_default_group = value;
			}
		}

		// Token: 0x170008E8 RID: 2280
		// (get) Token: 0x06002449 RID: 9289 RVA: 0x00089234 File Offset: 0x00087434
		// (set) Token: 0x0600244A RID: 9290 RVA: 0x00089258 File Offset: 0x00087458
		internal int ItemCount
		{
			get
			{
				return (!this.is_default_group) ? this.items.Count : this.item_count;
			}
			set
			{
				if (!this.is_default_group)
				{
					throw new InvalidOperationException("ItemCount cannot be set for non-default groups.");
				}
				this.item_count = value;
			}
		}

		// Token: 0x0600244B RID: 9291 RVA: 0x00089278 File Offset: 0x00087478
		internal int GetActualItemCount()
		{
			if (this.is_default_group)
			{
				return this.item_count;
			}
			int num = 0;
			for (int i = 0; i < this.items.Count; i++)
			{
				if (this.items[i].ListView != null)
				{
					num++;
				}
			}
			return num;
		}

		/// <summary>Gets or sets the name of the group.</summary>
		/// <returns>The name of the group.</returns>
		// Token: 0x170008E9 RID: 2281
		// (get) Token: 0x0600244C RID: 9292 RVA: 0x000892D0 File Offset: 0x000874D0
		// (set) Token: 0x0600244D RID: 9293 RVA: 0x000892D8 File Offset: 0x000874D8
		[DefaultValue("")]
		[Browsable(true)]
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		/// <summary>Gets or sets the object that contains data about the group.</summary>
		/// <returns>An <see cref="T:System.Object" /> for storing the additional data. </returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170008EA RID: 2282
		// (get) Token: 0x0600244E RID: 9294 RVA: 0x000892E4 File Offset: 0x000874E4
		// (set) Token: 0x0600244F RID: 9295 RVA: 0x000892EC File Offset: 0x000874EC
		[TypeConverter(typeof(StringConverter))]
		[DefaultValue(null)]
		[Localizable(false)]
		[Bindable(true)]
		public object Tag
		{
			get
			{
				return this.tag;
			}
			set
			{
				this.tag = value;
			}
		}

		/// <returns>A string that represents the current object.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002450 RID: 9296 RVA: 0x000892F8 File Offset: 0x000874F8
		public override string ToString()
		{
			return this.header;
		}

		// Token: 0x0400128A RID: 4746
		internal string header;

		// Token: 0x0400128B RID: 4747
		private string name;

		// Token: 0x0400128C RID: 4748
		private HorizontalAlignment header_alignment;

		// Token: 0x0400128D RID: 4749
		private ListView list_view_owner;

		// Token: 0x0400128E RID: 4750
		private ListView.ListViewItemCollection items;

		// Token: 0x0400128F RID: 4751
		private object tag;

		// Token: 0x04001290 RID: 4752
		private Rectangle header_bounds;

		// Token: 0x04001291 RID: 4753
		internal int starting_row;

		// Token: 0x04001292 RID: 4754
		internal int starting_item;

		// Token: 0x04001293 RID: 4755
		internal int rows;

		// Token: 0x04001294 RID: 4756
		internal int current_item;

		// Token: 0x04001295 RID: 4757
		internal Point items_area_location;

		// Token: 0x04001296 RID: 4758
		private bool is_default_group;

		// Token: 0x04001297 RID: 4759
		private int item_count;
	}
}
