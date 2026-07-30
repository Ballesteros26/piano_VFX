using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Runtime.Serialization;

namespace System.Windows.Forms
{
	/// <summary>Represents an item in a <see cref="T:System.Windows.Forms.ListView" /> control.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000230 RID: 560
	[DefaultProperty("Text")]
	[ToolboxItem(false)]
	[TypeConverter(typeof(ListViewItemConverter))]
	[DesignTimeVisible(false)]
	[Serializable]
	public class ListViewItem : ISerializable, ICloneable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListViewItem" /> class with default values.</summary>
		// Token: 0x0600248B RID: 9355 RVA: 0x00089DD8 File Offset: 0x00087FD8
		public ListViewItem()
			: this(string.Empty)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListViewItem" /> class with the specified item text.</summary>
		/// <param name="text">The text to display for the item. This should not exceed 259 characters.</param>
		// Token: 0x0600248C RID: 9356 RVA: 0x00089DE8 File Offset: 0x00087FE8
		public ListViewItem(string text)
			: this(text, -1)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListViewItem" /> class with an array of strings representing subitems.</summary>
		/// <param name="items">An array of strings that represent the subitems of the new item. </param>
		// Token: 0x0600248D RID: 9357 RVA: 0x00089DF4 File Offset: 0x00087FF4
		public ListViewItem(string[] items)
			: this(items, -1)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListViewItem" /> class with the image index position of the item's icon and an array of <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> objects.</summary>
		/// <param name="subItems">An array of type <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> that represents the subitems of the item. </param>
		/// <param name="imageIndex">The zero-based index of the image within the <see cref="T:System.Windows.Forms.ImageList" /> associated with the <see cref="T:System.Windows.Forms.ListView" /> that contains the item. </param>
		// Token: 0x0600248E RID: 9358 RVA: 0x00089E00 File Offset: 0x00088000
		public ListViewItem(ListViewItem.ListViewSubItem[] subItems, int imageIndex)
		{
			this.image_index = -1;
			this.state_image_index = -1;
			this.use_item_style = true;
			this.display_index = -1;
			this.name = string.Empty;
			this.image_key = string.Empty;
			this.tooltip_text = string.Empty;
			this.position = new Point(-1, -1);
			this.bounds = Rectangle.Empty;
			base..ctor();
			this.sub_items = new ListViewItem.ListViewSubItemCollection(this, null);
			for (int i = 0; i < subItems.Length; i++)
			{
				this.sub_items.Add(subItems[i]);
			}
			this.image_index = imageIndex;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListViewItem" /> class with the specified item text and the image index position of the item's icon.</summary>
		/// <param name="text">The text to display for the item. This should not exceed 259 characters.</param>
		/// <param name="imageIndex">The zero-based index of the image within the <see cref="T:System.Windows.Forms.ImageList" /> associated with the <see cref="T:System.Windows.Forms.ListView" /> that contains the item. </param>
		// Token: 0x0600248F RID: 9359 RVA: 0x00089EA0 File Offset: 0x000880A0
		public ListViewItem(string text, int imageIndex)
		{
			this.image_index = -1;
			this.state_image_index = -1;
			this.use_item_style = true;
			this.display_index = -1;
			this.name = string.Empty;
			this.image_key = string.Empty;
			this.tooltip_text = string.Empty;
			this.position = new Point(-1, -1);
			this.bounds = Rectangle.Empty;
			base..ctor();
			this.image_index = imageIndex;
			this.sub_items = new ListViewItem.ListViewSubItemCollection(this, text);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListViewItem" /> class with the image index position of the item's icon and an array of strings representing subitems.</summary>
		/// <param name="items">An array of strings that represent the subitems of the new item. </param>
		/// <param name="imageIndex">The zero-based index of the image within the <see cref="T:System.Windows.Forms.ImageList" /> associated with the <see cref="T:System.Windows.Forms.ListView" /> that contains the item. </param>
		// Token: 0x06002490 RID: 9360 RVA: 0x00089F1C File Offset: 0x0008811C
		public ListViewItem(string[] items, int imageIndex)
		{
			this.image_index = -1;
			this.state_image_index = -1;
			this.use_item_style = true;
			this.display_index = -1;
			this.name = string.Empty;
			this.image_key = string.Empty;
			this.tooltip_text = string.Empty;
			this.position = new Point(-1, -1);
			this.bounds = Rectangle.Empty;
			base..ctor();
			this.sub_items = new ListViewItem.ListViewSubItemCollection(this, null);
			if (items != null)
			{
				for (int i = 0; i < items.Length; i++)
				{
					this.sub_items.Add(new ListViewItem.ListViewSubItem(this, items[i]));
				}
			}
			this.image_index = imageIndex;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListViewItem" /> class with the image index position of the item's icon; the foreground color, background color, and font of the item; and an array of strings representing subitems.</summary>
		/// <param name="items">An array of strings that represent the subitems of the new item. </param>
		/// <param name="imageIndex">The zero-based index of the image within the <see cref="T:System.Windows.Forms.ImageList" /> associated with the <see cref="T:System.Windows.Forms.ListView" /> that contains the item. </param>
		/// <param name="foreColor">A <see cref="T:System.Drawing.Color" /> that represents the foreground color of the item. </param>
		/// <param name="backColor">A <see cref="T:System.Drawing.Color" /> that represents the background color of the item. </param>
		/// <param name="font">A <see cref="T:System.Drawing.Font" /> that represents the font to display the item's text in. </param>
		// Token: 0x06002491 RID: 9361 RVA: 0x00089FC8 File Offset: 0x000881C8
		public ListViewItem(string[] items, int imageIndex, Color foreColor, Color backColor, Font font)
			: this(items, imageIndex)
		{
			this.ForeColor = foreColor;
			this.BackColor = backColor;
			this.font = font;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListViewItem" /> class with the specified item and subitem text and image.</summary>
		/// <param name="items">An array containing the text of the subitems of the <see cref="T:System.Windows.Forms.ListViewItem" />.</param>
		/// <param name="imageKey">The name of the image within the <see cref="P:System.Windows.Forms.ListViewItem.ImageList" /> of the owning <see cref="T:System.Windows.Forms.ListView" /> to display in the <see cref="T:System.Windows.Forms.ListViewItem" />.</param>
		// Token: 0x06002492 RID: 9362 RVA: 0x00089FEC File Offset: 0x000881EC
		public ListViewItem(string[] items, string imageKey)
			: this(items)
		{
			this.ImageKey = imageKey;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListViewItem" /> class with the specified text and image.</summary>
		/// <param name="text">The text to display for the item. This should not exceed 259 characters.</param>
		/// <param name="imageKey">The name of the image within the <see cref="P:System.Windows.Forms.ListViewItem.ImageList" /> of the owning <see cref="T:System.Windows.Forms.ListView" /> to display in the <see cref="T:System.Windows.Forms.ListViewItem" />.</param>
		// Token: 0x06002493 RID: 9363 RVA: 0x00089FFC File Offset: 0x000881FC
		public ListViewItem(string text, string imageKey)
			: this(text)
		{
			this.ImageKey = imageKey;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListViewItem" /> class with the specified subitems and image.</summary>
		/// <param name="subItems">An array of <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> objects.</param>
		/// <param name="imageKey">The name of the image within the <see cref="P:System.Windows.Forms.ListViewItem.ImageList" /> of the owning <see cref="T:System.Windows.Forms.ListView" /> to display in the <see cref="T:System.Windows.Forms.ListViewItem" />.</param>
		// Token: 0x06002494 RID: 9364 RVA: 0x0008A00C File Offset: 0x0008820C
		public ListViewItem(ListViewItem.ListViewSubItem[] subItems, string imageKey)
		{
			this.image_index = -1;
			this.state_image_index = -1;
			this.use_item_style = true;
			this.display_index = -1;
			this.name = string.Empty;
			this.image_key = string.Empty;
			this.tooltip_text = string.Empty;
			this.position = new Point(-1, -1);
			this.bounds = Rectangle.Empty;
			base..ctor();
			this.sub_items = new ListViewItem.ListViewSubItemCollection(this, null);
			for (int i = 0; i < subItems.Length; i++)
			{
				this.sub_items.Add(subItems[i]);
			}
			this.ImageKey = imageKey;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListViewItem" /> class with the subitems containing the specified text, image, colors, and font.</summary>
		/// <param name="items">An array of strings that represent the text of the subitems for the <see cref="T:System.Windows.Forms.ListViewItem" />.</param>
		/// <param name="imageKey">The name of the image within the <see cref="P:System.Windows.Forms.ListViewItem.ImageList" /> of the owning <see cref="T:System.Windows.Forms.ListView" /> to display in the item.</param>
		/// <param name="foreColor">A <see cref="T:System.Drawing.Color" /> that represents the foreground color of the item.</param>
		/// <param name="backColor">A <see cref="T:System.Drawing.Color" /> that represents the background color of the item.</param>
		/// <param name="font">A <see cref="T:System.Drawing.Font" /> to apply to the item text.</param>
		// Token: 0x06002495 RID: 9365 RVA: 0x0008A0AC File Offset: 0x000882AC
		public ListViewItem(string[] items, string imageKey, Color foreColor, Color backColor, Font font)
			: this(items, imageKey)
		{
			this.ForeColor = foreColor;
			this.BackColor = backColor;
			this.font = font;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListViewItem" /> class and assigns it to the specified group.</summary>
		/// <param name="group">The <see cref="T:System.Windows.Forms.ListViewGroup" /> to assign the item to. </param>
		// Token: 0x06002496 RID: 9366 RVA: 0x0008A0D0 File Offset: 0x000882D0
		public ListViewItem(ListViewGroup group)
			: this()
		{
			this.Group = group;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListViewItem" /> class with the specified item text and assigns it to the specified group.</summary>
		/// <param name="text">The text to display for the item. This should not exceed 259 characters.</param>
		/// <param name="group">The <see cref="T:System.Windows.Forms.ListViewGroup" /> to assign the item to. </param>
		// Token: 0x06002497 RID: 9367 RVA: 0x0008A0E0 File Offset: 0x000882E0
		public ListViewItem(string text, ListViewGroup group)
			: this(text)
		{
			this.Group = group;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListViewItem" /> class with an array of strings representing subitems, and assigns the item to the specified group.</summary>
		/// <param name="items">An array of strings that represent the subitems of the new item. </param>
		/// <param name="group">The <see cref="T:System.Windows.Forms.ListViewGroup" /> to assign the item to. </param>
		// Token: 0x06002498 RID: 9368 RVA: 0x0008A0F0 File Offset: 0x000882F0
		public ListViewItem(string[] items, ListViewGroup group)
			: this(items)
		{
			this.Group = group;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListViewItem" /> class with the image index position of the item's icon and an array of <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> objects, and assigns the item to the specified group.</summary>
		/// <param name="subItems">An array of type <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> that represents the subitems of the item. </param>
		/// <param name="imageIndex">The zero-based index of the image within the <see cref="T:System.Windows.Forms.ImageList" /> associated with the <see cref="T:System.Windows.Forms.ListView" /> that contains the item. </param>
		/// <param name="group">The <see cref="T:System.Windows.Forms.ListViewGroup" /> to assign the item to. </param>
		// Token: 0x06002499 RID: 9369 RVA: 0x0008A100 File Offset: 0x00088300
		public ListViewItem(ListViewItem.ListViewSubItem[] subItems, int imageIndex, ListViewGroup group)
			: this(subItems, imageIndex)
		{
			this.Group = group;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListViewItem" /> class with the specified subitems, image, and group.</summary>
		/// <param name="subItems">An array of <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> objects that represent the subitems of the <see cref="T:System.Windows.Forms.ListViewItem" />.</param>
		/// <param name="imageKey">The name of the image within the <see cref="P:System.Windows.Forms.ListViewItem.ImageList" /> of the owning <see cref="T:System.Windows.Forms.ListView" /> to display in the item.</param>
		/// <param name="group">The <see cref="T:System.Windows.Forms.ListViewGroup" /> to assign the item to.</param>
		// Token: 0x0600249A RID: 9370 RVA: 0x0008A114 File Offset: 0x00088314
		public ListViewItem(ListViewItem.ListViewSubItem[] subItems, string imageKey, ListViewGroup group)
			: this(subItems, imageKey)
		{
			this.Group = group;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListViewItem" /> class with the specified item text and the image index position of the item's icon, and assigns the item to the specified group.</summary>
		/// <param name="text">The text to display for the item. This should not exceed 259 characters.</param>
		/// <param name="imageIndex">The zero-based index of the image within the <see cref="T:System.Windows.Forms.ImageList" /> associated with the <see cref="T:System.Windows.Forms.ListView" /> that contains the item. </param>
		/// <param name="group">The <see cref="T:System.Windows.Forms.ListViewGroup" /> to assign the item to. </param>
		// Token: 0x0600249B RID: 9371 RVA: 0x0008A128 File Offset: 0x00088328
		public ListViewItem(string text, int imageIndex, ListViewGroup group)
			: this(text, imageIndex)
		{
			this.Group = group;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListViewItem" /> class with the specified text, image, and group.</summary>
		/// <param name="text">The text to display for the item. This should not exceed 259 characters.</param>
		/// <param name="imageKey">The name of the image within the <see cref="P:System.Windows.Forms.ListViewItem.ImageList" /> of the owning <see cref="T:System.Windows.Forms.ListView" /> to display in the item.</param>
		/// <param name="group">The <see cref="T:System.Windows.Forms.ListViewGroup" /> to assign the item to.</param>
		// Token: 0x0600249C RID: 9372 RVA: 0x0008A13C File Offset: 0x0008833C
		public ListViewItem(string text, string imageKey, ListViewGroup group)
			: this(text, imageKey)
		{
			this.Group = group;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListViewItem" /> class with the image index position of the item's icon and an array of strings representing subitems, and assigns the item to the specified group.</summary>
		/// <param name="items">An array of strings that represent the subitems of the new item. </param>
		/// <param name="imageIndex">The zero-based index of the image within the <see cref="T:System.Windows.Forms.ImageList" /> associated with the <see cref="T:System.Windows.Forms.ListView" /> that contains the item. </param>
		/// <param name="group">The <see cref="T:System.Windows.Forms.ListViewGroup" /> to assign the item to. </param>
		// Token: 0x0600249D RID: 9373 RVA: 0x0008A150 File Offset: 0x00088350
		public ListViewItem(string[] items, int imageIndex, ListViewGroup group)
			: this(items, imageIndex)
		{
			this.Group = group;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListViewItem" /> class with subitems containing the specified text, image, and group.</summary>
		/// <param name="items">An array of strings that represents the text for subitems of the <see cref="T:System.Windows.Forms.ListViewItem" />.</param>
		/// <param name="imageKey">The name of the image within the <see cref="P:System.Windows.Forms.ListViewItem.ImageList" /> of the owning <see cref="T:System.Windows.Forms.ListView" /> to display in the item.</param>
		/// <param name="group">The <see cref="T:System.Windows.Forms.ListViewGroup" /> to assign the item to.</param>
		// Token: 0x0600249E RID: 9374 RVA: 0x0008A164 File Offset: 0x00088364
		public ListViewItem(string[] items, string imageKey, ListViewGroup group)
			: this(items, imageKey)
		{
			this.Group = group;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListViewItem" /> class with the image index position of the item's icon; the foreground color, background color, and font of the item; and an array of strings representing subitems. Assigns the item to the specified group.</summary>
		/// <param name="items">An array of strings that represent the subitems of the new item. </param>
		/// <param name="imageIndex">The zero-based index of the image within the <see cref="T:System.Windows.Forms.ImageList" /> associated with the <see cref="T:System.Windows.Forms.ListView" /> that contains the item. </param>
		/// <param name="foreColor">A <see cref="T:System.Drawing.Color" /> that represents the foreground color of the item. </param>
		/// <param name="backColor">A <see cref="T:System.Drawing.Color" /> that represents the background color of the item. </param>
		/// <param name="font">A <see cref="T:System.Drawing.Font" /> that represents the font to display the item's text in. </param>
		/// <param name="group">The <see cref="T:System.Windows.Forms.ListViewGroup" /> to assign the item to. </param>
		// Token: 0x0600249F RID: 9375 RVA: 0x0008A178 File Offset: 0x00088378
		public ListViewItem(string[] items, int imageIndex, Color foreColor, Color backColor, Font font, ListViewGroup group)
			: this(items, imageIndex, foreColor, backColor, font)
		{
			this.Group = group;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListViewItem" /> class with the subitems containing the specified text, image, colors, font, and group.</summary>
		/// <param name="items">An array of strings that represents the text of the subitems for the <see cref="T:System.Windows.Forms.ListViewItem" />.</param>
		/// <param name="imageKey">The name of the image within the <see cref="P:System.Windows.Forms.ListViewItem.ImageList" /> of the owning <see cref="T:System.Windows.Forms.ListView" /> to display in the item.</param>
		/// <param name="foreColor">A <see cref="T:System.Drawing.Color" /> that represents the foreground color of the item.</param>
		/// <param name="backColor">A <see cref="T:System.Drawing.Color" /> that represents the background color of the item.</param>
		/// <param name="font">A <see cref="T:System.Drawing.Font" /> to apply to the item text.</param>
		/// <param name="group">The <see cref="T:System.Windows.Forms.ListViewGroup" /> to assign the item to.</param>
		// Token: 0x060024A0 RID: 9376 RVA: 0x0008A190 File Offset: 0x00088390
		public ListViewItem(string[] items, string imageKey, Color foreColor, Color backColor, Font font, ListViewGroup group)
			: this(items, imageKey, foreColor, backColor, font)
		{
			this.Group = group;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListViewItem" /> class with the specified serialization information and streaming context.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> containing information about the <see cref="T:System.Windows.Forms.ListViewItem" /> to be initialized.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that indicates the source destination and context information of a serialized stream.</param>
		// Token: 0x060024A1 RID: 9377 RVA: 0x0008A1A8 File Offset: 0x000883A8
		protected ListViewItem(SerializationInfo info, StreamingContext context)
		{
			this.image_index = -1;
			this.state_image_index = -1;
			this.use_item_style = true;
			this.display_index = -1;
			this.name = string.Empty;
			this.image_key = string.Empty;
			this.tooltip_text = string.Empty;
			this.position = new Point(-1, -1);
			this.bounds = Rectangle.Empty;
			base..ctor();
			this.Deserialize(info, context);
		}

		// Token: 0x14000233 RID: 563
		// (add) Token: 0x060024A2 RID: 9378 RVA: 0x0008A218 File Offset: 0x00088418
		// (remove) Token: 0x060024A3 RID: 9379 RVA: 0x0008A234 File Offset: 0x00088434
		internal event EventHandler UIATextChanged;

		// Token: 0x14000234 RID: 564
		// (add) Token: 0x060024A4 RID: 9380 RVA: 0x0008A250 File Offset: 0x00088450
		// (remove) Token: 0x060024A5 RID: 9381 RVA: 0x0008A26C File Offset: 0x0008846C
		internal event LabelEditEventHandler UIASubItemTextChanged;

		/// <summary>Serializes the item.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the data needed to serialize the item.  </param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that represents the source and destination of the stream being serialized.</param>
		// Token: 0x060024A6 RID: 9382 RVA: 0x0008A288 File Offset: 0x00088488
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			this.Serialize(info, context);
		}

		// Token: 0x060024A7 RID: 9383 RVA: 0x0008A294 File Offset: 0x00088494
		internal void OnUIATextChanged()
		{
			if (this.UIATextChanged != null)
			{
				this.UIATextChanged.Invoke(this, EventArgs.Empty);
			}
		}

		// Token: 0x060024A8 RID: 9384 RVA: 0x0008A2B4 File Offset: 0x000884B4
		internal void OnUIASubItemTextChanged(LabelEditEventArgs args)
		{
			if (args.Item == 0)
			{
				this.OnUIATextChanged();
			}
			if (this.UIASubItemTextChanged != null)
			{
				this.UIASubItemTextChanged(this, args);
			}
		}

		/// <summary>Gets or sets the background color of the item's text.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the background color of the item's text.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000900 RID: 2304
		// (get) Token: 0x060024A9 RID: 9385 RVA: 0x0008A2E0 File Offset: 0x000884E0
		// (set) Token: 0x060024AA RID: 9386 RVA: 0x0008A334 File Offset: 0x00088534
		[DesignerSerializationVisibility(0)]
		public Color BackColor
		{
			get
			{
				if (this.sub_items.Count > 0)
				{
					return this.sub_items[0].BackColor;
				}
				if (this.owner != null)
				{
					return this.owner.BackColor;
				}
				return ThemeEngine.Current.ColorWindow;
			}
			set
			{
				this.SubItems[0].BackColor = value;
			}
		}

		/// <summary>Gets the bounding rectangle of the item, including subitems.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the bounding rectangle of the item.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000901 RID: 2305
		// (get) Token: 0x060024AB RID: 9387 RVA: 0x0008A348 File Offset: 0x00088548
		[Browsable(false)]
		public Rectangle Bounds
		{
			get
			{
				return this.GetBounds(ItemBoundsPortion.Entire);
			}
		}

		/// <summary>Gets or sets a value indicating whether the item is checked.</summary>
		/// <returns>true if the item is checked; otherwise, false. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000902 RID: 2306
		// (get) Token: 0x060024AC RID: 9388 RVA: 0x0008A354 File Offset: 0x00088554
		// (set) Token: 0x060024AD RID: 9389 RVA: 0x0008A35C File Offset: 0x0008855C
		[RefreshProperties(2)]
		[DefaultValue(false)]
		public bool Checked
		{
			get
			{
				return this.is_checked;
			}
			set
			{
				if (this.is_checked == value)
				{
					return;
				}
				if (this.owner != null)
				{
					CheckState checkState = ((!this.is_checked) ? CheckState.Unchecked : CheckState.Checked);
					CheckState checkState2 = ((!value) ? CheckState.Unchecked : CheckState.Checked);
					ItemCheckEventArgs itemCheckEventArgs = new ItemCheckEventArgs(this.Index, checkState2, checkState);
					this.owner.OnItemCheck(itemCheckEventArgs);
					if (checkState2 != checkState)
					{
						this.owner.CheckedItems.Reset();
						this.is_checked = checkState2 == CheckState.Checked;
						this.Invalidate();
						ItemCheckedEventArgs itemCheckedEventArgs = new ItemCheckedEventArgs(this);
						this.owner.OnItemChecked(itemCheckedEventArgs);
					}
				}
				else
				{
					this.is_checked = value;
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the item has focus within the <see cref="T:System.Windows.Forms.ListView" /> control.</summary>
		/// <returns>true if the item has focus; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000903 RID: 2307
		// (get) Token: 0x060024AE RID: 9390 RVA: 0x0008A404 File Offset: 0x00088604
		// (set) Token: 0x060024AF RID: 9391 RVA: 0x0008A450 File Offset: 0x00088650
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public bool Focused
		{
			get
			{
				if (this.owner == null)
				{
					return false;
				}
				if (this.owner.VirtualMode)
				{
					return this.Index == this.owner.focused_item_index;
				}
				return this.owner.FocusedItem == this;
			}
			set
			{
				if (this.owner == null)
				{
					return;
				}
				if (this.Focused == value)
				{
					return;
				}
				ListViewItem focusedItem = this.owner.FocusedItem;
				if (focusedItem != null)
				{
					focusedItem.UpdateFocusedState();
				}
				this.owner.focused_item_index = ((!value) ? (-1) : this.Index);
				if (value)
				{
					this.owner.OnUIAFocusedItemChanged();
				}
				this.UpdateFocusedState();
			}
		}

		/// <summary>Gets or sets the font of the text displayed by the item.</summary>
		/// <returns>The <see cref="T:System.Drawing.Font" /> to apply to the text displayed by the control. The default is the value of the <see cref="P:System.Windows.Forms.Control.DefaultFont" /> property if the <see cref="T:System.Windows.Forms.ListViewItem" /> is not associated with a <see cref="T:System.Windows.Forms.ListView" /> control; otherwise, the font specified in the <see cref="P:System.Windows.Forms.Control.Font" /> property for the <see cref="T:System.Windows.Forms.ListView" /> control is used.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000904 RID: 2308
		// (get) Token: 0x060024B0 RID: 9392 RVA: 0x0008A4C4 File Offset: 0x000886C4
		// (set) Token: 0x060024B1 RID: 9393 RVA: 0x0008A4FC File Offset: 0x000886FC
		[DesignerSerializationVisibility(0)]
		[Localizable(true)]
		public Font Font
		{
			get
			{
				if (this.font != null)
				{
					return this.font;
				}
				if (this.owner != null)
				{
					return this.owner.Font;
				}
				return ThemeEngine.Current.DefaultFont;
			}
			set
			{
				if (this.font == value)
				{
					return;
				}
				this.font = value;
				this.hot_font = null;
				if (this.owner != null)
				{
					this.Layout();
				}
				this.Invalidate();
			}
		}

		/// <summary>Gets or sets the foreground color of the item's text.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the foreground color of the item's text.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000905 RID: 2309
		// (get) Token: 0x060024B2 RID: 9394 RVA: 0x0008A53C File Offset: 0x0008873C
		// (set) Token: 0x060024B3 RID: 9395 RVA: 0x0008A590 File Offset: 0x00088790
		[DesignerSerializationVisibility(0)]
		public Color ForeColor
		{
			get
			{
				if (this.sub_items.Count > 0)
				{
					return this.sub_items[0].ForeColor;
				}
				if (this.owner != null)
				{
					return this.owner.ForeColor;
				}
				return ThemeEngine.Current.ColorWindowText;
			}
			set
			{
				this.SubItems[0].ForeColor = value;
			}
		}

		/// <summary>Gets or sets the index of the image that is displayed for the item.</summary>
		/// <returns>The zero-based index of the image in the <see cref="T:System.Windows.Forms.ImageList" /> that is displayed for the item. The default is -1.</returns>
		/// <exception cref="T:System.ArgumentException">The value specified is less than -1. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000906 RID: 2310
		// (get) Token: 0x060024B4 RID: 9396 RVA: 0x0008A5A4 File Offset: 0x000887A4
		// (set) Token: 0x060024B5 RID: 9397 RVA: 0x0008A5AC File Offset: 0x000887AC
		[Localizable(true)]
		[RefreshProperties(2)]
		[Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[DefaultValue(-1)]
		[DesignerSerializationVisibility(0)]
		[TypeConverter(typeof(NoneExcludedImageIndexConverter))]
		public int ImageIndex
		{
			get
			{
				return this.image_index;
			}
			set
			{
				if (value < -1)
				{
					throw new ArgumentException("Invalid ImageIndex. It must be greater than or equal to -1.");
				}
				this.image_index = value;
				this.image_key = string.Empty;
				if (this.owner != null)
				{
					this.Layout();
				}
				this.Invalidate();
			}
		}

		/// <summary>Gets or sets the key for the image that is displayed for the item.</summary>
		/// <returns>The key for the image that is displayed for the <see cref="T:System.Windows.Forms.ListViewItem" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000907 RID: 2311
		// (get) Token: 0x060024B6 RID: 9398 RVA: 0x0008A5EC File Offset: 0x000887EC
		// (set) Token: 0x060024B7 RID: 9399 RVA: 0x0008A5F4 File Offset: 0x000887F4
		[DesignerSerializationVisibility(0)]
		[DefaultValue("")]
		[Localizable(true)]
		[Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[RefreshProperties(2)]
		[TypeConverter(typeof(ImageKeyConverter))]
		public string ImageKey
		{
			get
			{
				return this.image_key;
			}
			set
			{
				this.image_key = ((value != null) ? value : string.Empty);
				this.image_index = -1;
				if (this.owner != null)
				{
					this.Layout();
				}
				this.Invalidate();
			}
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.ImageList" /> that contains the image displayed with the item.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ImageList" /> used by the <see cref="T:System.Windows.Forms.ListView" /> control that contains the image displayed with the item.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000908 RID: 2312
		// (get) Token: 0x060024B8 RID: 9400 RVA: 0x0008A62C File Offset: 0x0008882C
		[Browsable(false)]
		public ImageList ImageList
		{
			get
			{
				if (this.owner == null)
				{
					return null;
				}
				if (this.owner.View == View.LargeIcon)
				{
					return this.owner.large_image_list;
				}
				return this.owner.small_image_list;
			}
		}

		/// <summary>Gets or sets the number of small image widths by which to indent the <see cref="T:System.Windows.Forms.ListViewItem" />.</summary>
		/// <returns>The number of small image widths by which to indent the <see cref="T:System.Windows.Forms.ListViewItem" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">When setting <see cref="P:System.Windows.Forms.ListViewItem.IndentCount" />, the number specified is less than 0.</exception>
		// Token: 0x17000909 RID: 2313
		// (get) Token: 0x060024B9 RID: 9401 RVA: 0x0008A670 File Offset: 0x00088870
		// (set) Token: 0x060024BA RID: 9402 RVA: 0x0008A678 File Offset: 0x00088878
		[DefaultValue(0)]
		public int IndentCount
		{
			get
			{
				return this.indent_count;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				if (value == this.indent_count)
				{
					return;
				}
				this.indent_count = value;
				this.Invalidate();
			}
		}

		/// <summary>Gets the zero-based index of the item within the <see cref="T:System.Windows.Forms.ListView" /> control.</summary>
		/// <returns>The zero-based index of the item within the <see cref="T:System.Windows.Forms.ListView.ListViewItemCollection" /> of the <see cref="T:System.Windows.Forms.ListView" /> control, or -1 if the item is not associated with a <see cref="T:System.Windows.Forms.ListView" /> control.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700090A RID: 2314
		// (get) Token: 0x060024BB RID: 9403 RVA: 0x0008A6B4 File Offset: 0x000888B4
		[Browsable(false)]
		public int Index
		{
			get
			{
				if (this.owner == null)
				{
					return -1;
				}
				if (this.owner.VirtualMode)
				{
					return this.display_index;
				}
				if (this.display_index == -1)
				{
					return this.owner.Items.IndexOf(this);
				}
				return this.owner.GetItemIndex(this.display_index);
			}
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.ListView" /> control that contains the item.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ListView" /> that contains the <see cref="T:System.Windows.Forms.ListViewItem" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700090B RID: 2315
		// (get) Token: 0x060024BC RID: 9404 RVA: 0x0008A714 File Offset: 0x00088914
		[Browsable(false)]
		public ListView ListView
		{
			get
			{
				return this.owner;
			}
		}

		/// <summary>Gets or sets the name associated with this <see cref="T:System.Windows.Forms.ListViewItem" />. </summary>
		/// <returns>The name of the <see cref="T:System.Windows.Forms.ListViewItem" />. The default is an empty string ("").</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700090C RID: 2316
		// (get) Token: 0x060024BD RID: 9405 RVA: 0x0008A71C File Offset: 0x0008891C
		// (set) Token: 0x060024BE RID: 9406 RVA: 0x0008A724 File Offset: 0x00088924
		[Localizable(true)]
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = ((value != null) ? value : string.Empty);
			}
		}

		/// <summary>Gets or sets the position of the upper-left corner of the <see cref="T:System.Windows.Forms.ListViewItem" />.</summary>
		/// <returns>The <see cref="T:System.Drawing.Point" /> at the upper-left corner of the <see cref="T:System.Windows.Forms.ListViewItem" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Windows.Forms.ListViewItem.Position" /> is set when the containing <see cref="T:System.Windows.Forms.ListView" /> is in virtual mode.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700090D RID: 2317
		// (get) Token: 0x060024BF RID: 9407 RVA: 0x0008A740 File Offset: 0x00088940
		// (set) Token: 0x060024C0 RID: 9408 RVA: 0x0008A7A4 File Offset: 0x000889A4
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public Point Position
		{
			get
			{
				if (this.owner != null && this.owner.VirtualMode)
				{
					return this.owner.GetItemLocation(this.display_index);
				}
				if (this.owner != null && !this.owner.IsHandleCreated)
				{
					return new Point(-1, -1);
				}
				return this.position;
			}
			set
			{
				if (this.owner == null || this.owner.View == View.Details || this.owner.View == View.List)
				{
					return;
				}
				if (this.owner.VirtualMode)
				{
					throw new InvalidOperationException();
				}
				this.owner.ChangeItemLocation(this.display_index, value);
			}
		}

		/// <summary>Gets or sets a value indicating whether the item is selected.</summary>
		/// <returns>true if the item is selected; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700090E RID: 2318
		// (get) Token: 0x060024C1 RID: 9409 RVA: 0x0008A808 File Offset: 0x00088A08
		// (set) Token: 0x060024C2 RID: 9410 RVA: 0x0008A850 File Offset: 0x00088A50
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
		public bool Selected
		{
			get
			{
				if (this.owner != null && this.owner.VirtualMode)
				{
					return this.owner.SelectedIndices.Contains(this.Index);
				}
				return this.selected;
			}
			set
			{
				if (this.selected == value && this.owner != null && !this.owner.VirtualMode)
				{
					return;
				}
				if (this.owner != null)
				{
					if (value && !this.owner.MultiSelect)
					{
						this.owner.SelectedIndices.Clear();
					}
					if (this.owner.VirtualMode)
					{
						if (value)
						{
							this.owner.SelectedIndices.InsertIndex(this.Index);
						}
						else
						{
							this.owner.SelectedIndices.RemoveIndex(this.Index);
						}
					}
					else
					{
						this.selected = value;
						this.owner.SelectedIndices.Reset();
					}
					this.owner.OnItemSelectionChanged(new ListViewItemSelectionChangedEventArgs(this, this.Index, value));
					this.owner.OnSelectedIndexChanged();
				}
				else
				{
					this.selected = value;
				}
				this.Invalidate();
			}
		}

		/// <summary>Gets or sets the index of the state image (an image such as a selected or cleared check box that indicates the state of the item) that is displayed for the item.</summary>
		/// <returns>The zero-based index of the state image in the <see cref="T:System.Windows.Forms.ImageList" /> that is displayed for the item.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value specified for this property is less than -1.-or- The value specified for this property is greater than 14. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700090F RID: 2319
		// (get) Token: 0x060024C3 RID: 9411 RVA: 0x0008A950 File Offset: 0x00088B50
		// (set) Token: 0x060024C4 RID: 9412 RVA: 0x0008A958 File Offset: 0x00088B58
		[Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[Localizable(true)]
		[RefreshProperties(2)]
		[RelatedImageList("ListView.StateImageList")]
		[TypeConverter(typeof(NoneExcludedImageIndexConverter))]
		[DefaultValue(-1)]
		public int StateImageIndex
		{
			get
			{
				return this.state_image_index;
			}
			set
			{
				if (value < -1 || value > 14)
				{
					throw new ArgumentOutOfRangeException("Invalid StateImageIndex. It must be in the range of [-1, 14].");
				}
				this.state_image_index = value;
			}
		}

		/// <summary>Gets a collection containing all subitems of the item.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItemCollection" /> that contains the subitems.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000910 RID: 2320
		// (get) Token: 0x060024C5 RID: 9413 RVA: 0x0008A97C File Offset: 0x00088B7C
		[Editor("System.Windows.Forms.Design.ListViewSubItemCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[DesignerSerializationVisibility(0)]
		public ListViewItem.ListViewSubItemCollection SubItems
		{
			get
			{
				if (this.sub_items.Count == 0)
				{
					this.sub_items.Add(string.Empty);
				}
				return this.sub_items;
			}
		}

		/// <summary>Gets or sets an object that contains data to associate with the item.</summary>
		/// <returns>An object that contains information that is associated with the item.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000911 RID: 2321
		// (get) Token: 0x060024C6 RID: 9414 RVA: 0x0008A9A8 File Offset: 0x00088BA8
		// (set) Token: 0x060024C7 RID: 9415 RVA: 0x0008A9B0 File Offset: 0x00088BB0
		[Localizable(false)]
		[DefaultValue(null)]
		[Bindable(true)]
		[TypeConverter(typeof(StringConverter))]
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

		/// <summary>Gets or sets the text of the item.</summary>
		/// <returns>The text to display for the item. This should not exceed 259 characters.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000912 RID: 2322
		// (get) Token: 0x060024C8 RID: 9416 RVA: 0x0008A9BC File Offset: 0x00088BBC
		// (set) Token: 0x060024C9 RID: 9417 RVA: 0x0008A9F4 File Offset: 0x00088BF4
		[DesignerSerializationVisibility(0)]
		[Localizable(true)]
		public string Text
		{
			get
			{
				if (this.sub_items.Count > 0)
				{
					return this.sub_items[0].Text;
				}
				return string.Empty;
			}
			set
			{
				if (this.SubItems[0].Text == value)
				{
					return;
				}
				this.sub_items[0].Text = value;
				if (this.owner != null)
				{
					this.Layout();
				}
				this.Invalidate();
				this.OnUIATextChanged();
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="P:System.Windows.Forms.ListViewItem.Font" />, <see cref="P:System.Windows.Forms.ListViewItem.ForeColor" />, and <see cref="P:System.Windows.Forms.ListViewItem.BackColor" /> properties for the item are used for all its subitems.</summary>
		/// <returns>true if all subitems use the font, foreground color, and background color settings of the item; otherwise, false. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000913 RID: 2323
		// (get) Token: 0x060024CA RID: 9418 RVA: 0x0008AA50 File Offset: 0x00088C50
		// (set) Token: 0x060024CB RID: 9419 RVA: 0x0008AA58 File Offset: 0x00088C58
		[DefaultValue(true)]
		public bool UseItemStyleForSubItems
		{
			get
			{
				return this.use_item_style;
			}
			set
			{
				this.use_item_style = value;
			}
		}

		/// <summary>Gets or sets the group to which the item is assigned.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ListViewGroup" /> to which the item is assigned.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000914 RID: 2324
		// (get) Token: 0x060024CC RID: 9420 RVA: 0x0008AA64 File Offset: 0x00088C64
		// (set) Token: 0x060024CD RID: 9421 RVA: 0x0008AA6C File Offset: 0x00088C6C
		[DefaultValue(null)]
		[Localizable(true)]
		public ListViewGroup Group
		{
			get
			{
				return this.group;
			}
			set
			{
				if (this.group != value)
				{
					if (value == null)
					{
						this.group.Items.Remove(this);
					}
					else
					{
						value.Items.Add(this);
					}
					this.group = value;
				}
			}
		}

		/// <summary>Gets or sets the text shown when the mouse pointer rests on the <see cref="T:System.Windows.Forms.ListViewItem" />.</summary>
		/// <returns>The text shown when the mouse pointer rests on the <see cref="T:System.Windows.Forms.ListViewItem" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000915 RID: 2325
		// (get) Token: 0x060024CE RID: 9422 RVA: 0x0008AAB8 File Offset: 0x00088CB8
		// (set) Token: 0x060024CF RID: 9423 RVA: 0x0008AAC0 File Offset: 0x00088CC0
		[DefaultValue("")]
		public string ToolTipText
		{
			get
			{
				return this.tooltip_text;
			}
			set
			{
				if (value == null)
				{
					value = string.Empty;
				}
				this.tooltip_text = value;
			}
		}

		/// <summary>Places the item text into edit mode.</summary>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Windows.Forms.ListView.LabelEdit" /> property of the associated <see cref="T:System.Windows.Forms.ListView" /> is not set to true. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060024D0 RID: 9424 RVA: 0x0008AAD8 File Offset: 0x00088CD8
		public void BeginEdit()
		{
			if (this.owner != null && this.owner.LabelEdit)
			{
				this.owner.item_control.BeginEdit(this);
			}
		}

		/// <summary>Creates an identical copy of the item.</summary>
		/// <returns>An object that represents an item that has the same text, image, and subitems associated with it as the cloned item.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060024D1 RID: 9425 RVA: 0x0008AB14 File Offset: 0x00088D14
		public virtual object Clone()
		{
			ListViewItem listViewItem = new ListViewItem();
			listViewItem.image_index = this.image_index;
			listViewItem.is_checked = this.is_checked;
			listViewItem.selected = this.selected;
			listViewItem.font = this.font;
			listViewItem.state_image_index = this.state_image_index;
			listViewItem.sub_items = new ListViewItem.ListViewSubItemCollection(this, null);
			foreach (object obj in this.sub_items)
			{
				ListViewItem.ListViewSubItem listViewSubItem = (ListViewItem.ListViewSubItem)obj;
				listViewItem.sub_items.Add(listViewSubItem.Text, listViewSubItem.ForeColor, listViewSubItem.BackColor, listViewSubItem.Font);
			}
			listViewItem.tag = this.tag;
			listViewItem.use_item_style = this.use_item_style;
			listViewItem.owner = null;
			listViewItem.name = this.name;
			listViewItem.tooltip_text = this.tooltip_text;
			return listViewItem;
		}

		/// <summary>Ensures that the item is visible within the control, scrolling the contents of the control, if necessary.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060024D2 RID: 9426 RVA: 0x0008AC28 File Offset: 0x00088E28
		public virtual void EnsureVisible()
		{
			if (this.owner != null)
			{
				this.owner.EnsureVisible(this.owner.Items.IndexOf(this));
			}
		}

		/// <summary>Finds the next item from the <see cref="T:System.Windows.Forms.ListViewItem" />, searching in the specified direction.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ListViewItem" /> that is closest to the given coordinates, searching in the specified direction.</returns>
		/// <param name="searchDirection">One of the <see cref="T:System.Windows.Forms.SearchDirectionHint" /> values.</param>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Windows.Forms.ListView.View" /> property of the containing <see cref="T:System.Windows.Forms.ListView" /> is set to a value other than <see cref="F:System.Windows.Forms.View.SmallIcon" /> or <see cref="F:System.Windows.Forms.View.LargeIcon" />. </exception>
		// Token: 0x060024D3 RID: 9427 RVA: 0x0008AC54 File Offset: 0x00088E54
		public ListViewItem FindNearestItem(SearchDirectionHint searchDirection)
		{
			if (this.owner == null)
			{
				return null;
			}
			Point itemLocation = this.owner.GetItemLocation(this.display_index);
			return this.owner.FindNearestItem(searchDirection, itemLocation);
		}

		/// <summary>Retrieves the specified portion of the bounding rectangle for the item.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the bounding rectangle for the specified portion of the item.</returns>
		/// <param name="portion">One of the <see cref="T:System.Windows.Forms.ItemBoundsPortion" /> values that represents a portion of the item for which to retrieve the bounding rectangle. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060024D4 RID: 9428 RVA: 0x0008AC90 File Offset: 0x00088E90
		public Rectangle GetBounds(ItemBoundsPortion portion)
		{
			if (this.owner == null)
			{
				return Rectangle.Empty;
			}
			if (this.owner.VirtualMode && this.bounds == Rectangle.Empty)
			{
				this.Layout();
			}
			Rectangle rectangle;
			switch (portion)
			{
			case ItemBoundsPortion.Entire:
				rectangle = this.bounds;
				break;
			case ItemBoundsPortion.Icon:
				rectangle = this.icon_rect;
				break;
			case ItemBoundsPortion.Label:
				rectangle = this.label_rect;
				break;
			case ItemBoundsPortion.ItemOnly:
				rectangle = this.item_rect;
				break;
			default:
				throw new ArgumentException("Invalid value for portion.");
			}
			Point itemLocation = this.owner.GetItemLocation(this.DisplayIndex);
			rectangle.X += itemLocation.X;
			rectangle.Y += itemLocation.Y;
			return rectangle;
		}

		/// <summary>Returns the subitem of the <see cref="T:System.Windows.Forms.ListViewItem" /> at the specified coordinates.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> at the specified x- and y-coordinates.</returns>
		/// <param name="x">The x-coordinate. </param>
		/// <param name="y">The y-coordinate.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060024D5 RID: 9429 RVA: 0x0008AD70 File Offset: 0x00088F70
		public ListViewItem.ListViewSubItem GetSubItemAt(int x, int y)
		{
			if (this.owner != null && this.owner.View != View.Details)
			{
				return null;
			}
			foreach (object obj in this.sub_items)
			{
				ListViewItem.ListViewSubItem listViewSubItem = (ListViewItem.ListViewSubItem)obj;
				if (listViewSubItem.Bounds.Contains(x, y))
				{
					return listViewSubItem;
				}
			}
			return null;
		}

		/// <summary>Removes the item from its associated <see cref="T:System.Windows.Forms.ListView" /> control.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060024D6 RID: 9430 RVA: 0x0008AE18 File Offset: 0x00089018
		public virtual void Remove()
		{
			if (this.owner == null)
			{
				return;
			}
			this.owner.item_control.CancelEdit(this);
			this.owner.Items.Remove(this);
			this.owner = null;
		}

		/// <returns>A string that represents the current object.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060024D7 RID: 9431 RVA: 0x0008AE50 File Offset: 0x00089050
		public override string ToString()
		{
			return string.Format("ListViewItem: {0}", this.Text);
		}

		/// <summary>Deserializes the item.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the data needed to deserialize the item. </param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that represents the source and destination of the stream being deserialized. </param>
		// Token: 0x060024D8 RID: 9432 RVA: 0x0008AE64 File Offset: 0x00089064
		protected virtual void Deserialize(SerializationInfo info, StreamingContext context)
		{
			this.sub_items = new ListViewItem.ListViewSubItemCollection(this, null);
			int num = 0;
			foreach (SerializationEntry serializationEntry in info)
			{
				string text = serializationEntry.Name;
				if (text != null)
				{
					if (ListViewItem.<>f__switch$map4 == null)
					{
						Dictionary<string, int> dictionary = new Dictionary<string, int>(9);
						dictionary.Add("Text", 0);
						dictionary.Add("Font", 1);
						dictionary.Add("Checked", 2);
						dictionary.Add("ImageIndex", 3);
						dictionary.Add("StateImageIndex", 4);
						dictionary.Add("UseItemStyleForSubItems", 5);
						dictionary.Add("SubItemCount", 6);
						dictionary.Add("Group", 7);
						dictionary.Add("ImageKey", 8);
						ListViewItem.<>f__switch$map4 = dictionary;
					}
					int num2;
					if (ListViewItem.<>f__switch$map4.TryGetValue(text, ref num2))
					{
						switch (num2)
						{
						case 0:
							this.sub_items.Add((string)serializationEntry.Value);
							break;
						case 1:
							this.font = (Font)serializationEntry.Value;
							break;
						case 2:
							this.is_checked = (bool)serializationEntry.Value;
							break;
						case 3:
							this.image_index = (int)serializationEntry.Value;
							break;
						case 4:
							this.state_image_index = (int)serializationEntry.Value;
							break;
						case 5:
							this.use_item_style = (bool)serializationEntry.Value;
							break;
						case 6:
							num = (int)serializationEntry.Value;
							break;
						case 7:
							this.group = (ListViewGroup)serializationEntry.Value;
							break;
						case 8:
							if (this.image_index == -1)
							{
								this.image_key = (string)serializationEntry.Value;
							}
							break;
						}
					}
				}
			}
			Type typeFromHandle = typeof(ListViewItem.ListViewSubItem);
			if (num > 0)
			{
				this.sub_items.Clear();
				this.Text = info.GetString("Text");
				for (int i = 0; i < num - 1; i++)
				{
					this.sub_items.Add((ListViewItem.ListViewSubItem)info.GetValue("SubItem" + (i + 1), typeFromHandle));
				}
			}
			this.ForeColor = (Color)info.GetValue("ForeColor", typeof(Color));
			this.BackColor = (Color)info.GetValue("BackColor", typeof(Color));
		}

		/// <summary>Serializes the item.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the data needed to serialize the item. </param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that represents the source and destination of the stream being serialized. </param>
		// Token: 0x060024D9 RID: 9433 RVA: 0x0008B110 File Offset: 0x00089310
		protected virtual void Serialize(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("Text", this.Text);
			info.AddValue("Font", this.Font);
			info.AddValue("ImageIndex", this.image_index);
			info.AddValue("Checked", this.is_checked);
			info.AddValue("StateImageIndex", this.state_image_index);
			info.AddValue("UseItemStyleForSubItems", this.use_item_style);
			info.AddValue("BackColor", this.BackColor);
			info.AddValue("ForeColor", this.ForeColor);
			info.AddValue("ImageKey", this.image_key);
			info.AddValue("Group", this.group);
			if (this.sub_items.Count > 1)
			{
				info.AddValue("SubItemCount", this.sub_items.Count);
				for (int i = 1; i < this.sub_items.Count; i++)
				{
					info.AddValue("SubItem" + i, this.sub_items[i]);
				}
			}
		}

		// Token: 0x17000916 RID: 2326
		// (get) Token: 0x060024DA RID: 9434 RVA: 0x0008B238 File Offset: 0x00089438
		internal Rectangle CheckRectReal
		{
			get
			{
				Rectangle rectangle = this.checkbox_rect;
				Point itemLocation = this.owner.GetItemLocation(this.DisplayIndex);
				rectangle.X += itemLocation.X;
				rectangle.Y += itemLocation.Y;
				return rectangle;
			}
		}

		// Token: 0x17000917 RID: 2327
		// (get) Token: 0x060024DB RID: 9435 RVA: 0x0008B28C File Offset: 0x0008948C
		internal Rectangle TextBounds
		{
			get
			{
				if (this.owner.VirtualMode && this.bounds == new Rectangle(-1, -1, -1, -1))
				{
					this.Layout();
				}
				Rectangle rectangle = this.text_bounds;
				Point itemLocation = this.owner.GetItemLocation(this.DisplayIndex);
				rectangle.X += itemLocation.X;
				rectangle.Y += itemLocation.Y;
				return rectangle;
			}
		}

		// Token: 0x17000918 RID: 2328
		// (get) Token: 0x060024DC RID: 9436 RVA: 0x0008B30C File Offset: 0x0008950C
		// (set) Token: 0x060024DD RID: 9437 RVA: 0x0008B340 File Offset: 0x00089540
		internal int DisplayIndex
		{
			get
			{
				if (this.display_index == -1)
				{
					return this.owner.Items.IndexOf(this);
				}
				return this.display_index;
			}
			set
			{
				this.display_index = value;
			}
		}

		// Token: 0x17000919 RID: 2329
		// (get) Token: 0x060024DE RID: 9438 RVA: 0x0008B34C File Offset: 0x0008954C
		internal bool Hot
		{
			get
			{
				return this.Index == this.owner.HotItemIndex;
			}
		}

		// Token: 0x1700091A RID: 2330
		// (get) Token: 0x060024DF RID: 9439 RVA: 0x0008B364 File Offset: 0x00089564
		internal Font HotFont
		{
			get
			{
				if (this.hot_font == null)
				{
					this.hot_font = new Font(this.Font, this.Font.Style | 4);
				}
				return this.hot_font;
			}
		}

		// Token: 0x1700091B RID: 2331
		// (set) Token: 0x060024E0 RID: 9440 RVA: 0x0008B3A0 File Offset: 0x000895A0
		internal ListView Owner
		{
			set
			{
				if (this.owner == value)
				{
					return;
				}
				this.owner = value;
			}
		}

		// Token: 0x060024E1 RID: 9441 RVA: 0x0008B3B8 File Offset: 0x000895B8
		internal void SetGroup(ListViewGroup group)
		{
			this.group = group;
		}

		// Token: 0x060024E2 RID: 9442 RVA: 0x0008B3C4 File Offset: 0x000895C4
		internal void SetPosition(Point position)
		{
			this.position = position;
		}

		// Token: 0x060024E3 RID: 9443 RVA: 0x0008B3D0 File Offset: 0x000895D0
		private void UpdateFocusedState()
		{
			if (this.owner != null)
			{
				this.Invalidate();
				this.Layout();
				this.Invalidate();
			}
		}

		// Token: 0x060024E4 RID: 9444 RVA: 0x0008B3F0 File Offset: 0x000895F0
		internal void Invalidate()
		{
			if (this.owner == null || this.owner.item_control == null || this.owner.updating)
			{
				return;
			}
			Rectangle rectangle = this.Bounds;
			rectangle.Inflate(1, 1);
			this.owner.item_control.Invalidate(rectangle);
		}

		// Token: 0x060024E5 RID: 9445 RVA: 0x0008B44C File Offset: 0x0008964C
		internal void Layout()
		{
			if (this.owner == null)
			{
				return;
			}
			Size text_size = this.owner.text_size;
			this.checkbox_rect = Rectangle.Empty;
			if (this.owner.CheckBoxes)
			{
				this.checkbox_rect.Size = this.owner.CheckBoxSize;
			}
			switch (this.owner.View)
			{
			case View.LargeIcon:
				break;
			case View.Details:
			{
				int num = 0;
				if (this.owner.SmallImageList != null)
				{
					num = this.indent_count * this.owner.SmallImageList.ImageSize.Width;
				}
				if (this.owner.Columns.Count > 0)
				{
					this.checkbox_rect.X = this.owner.Columns[0].Rect.X + num;
				}
				this.icon_rect = (this.label_rect = Rectangle.Empty);
				this.icon_rect.X = this.checkbox_rect.Right + 2;
				int num2 = this.owner.ItemSize.Height;
				if (this.owner.SmallImageList != null)
				{
					this.icon_rect.Width = this.owner.SmallImageList.ImageSize.Width;
				}
				int num3 = num2;
				this.icon_rect.Height = num3;
				this.label_rect.Height = num3;
				this.checkbox_rect.Y = num2 - this.checkbox_rect.Height;
				this.label_rect.X = ((this.icon_rect.Width <= 0) ? this.icon_rect.Right : (this.icon_rect.Right + 1));
				if (this.owner.Columns.Count > 0)
				{
					this.label_rect.Width = this.owner.Columns[0].Wd - this.label_rect.X + this.checkbox_rect.X;
				}
				else
				{
					this.label_rect.Width = text_size.Width;
				}
				SizeF sizeF = TextRenderer.MeasureString(this.Text, this.Font);
				this.text_bounds = this.label_rect;
				this.text_bounds.Width = (int)sizeF.Width;
				Rectangle rectangle = (this.item_rect = Rectangle.Union(Rectangle.Union(this.checkbox_rect, this.icon_rect), this.label_rect));
				this.bounds.Size = rectangle.Size;
				this.item_rect.Width = 0;
				this.bounds.Width = 0;
				for (int i = 0; i < this.owner.Columns.Count; i++)
				{
					this.item_rect.Width = this.item_rect.Width + this.owner.Columns[i].Wd;
					this.bounds.Width = this.bounds.Width + this.owner.Columns[i].Wd;
				}
				int num4 = Math.Min(this.owner.Columns.Count, this.sub_items.Count);
				for (int j = 0; j < num4; j++)
				{
					Rectangle rect = this.owner.Columns[j].Rect;
					this.sub_items[j].SetBounds(rect.X, 0, rect.Width, num2);
				}
				return;
			}
			case View.SmallIcon:
			case View.List:
			{
				this.label_rect = (this.icon_rect = Rectangle.Empty);
				this.icon_rect.X = this.checkbox_rect.Width + 1;
				int num2 = Math.Max(this.owner.CheckBoxSize.Height, text_size.Height);
				if (this.owner.SmallImageList != null)
				{
					num2 = Math.Max(num2, this.owner.SmallImageList.ImageSize.Height);
					this.icon_rect.Width = this.owner.SmallImageList.ImageSize.Width;
					this.icon_rect.Height = this.owner.SmallImageList.ImageSize.Height;
				}
				this.checkbox_rect.Y = num2 - this.checkbox_rect.Height;
				this.label_rect.X = this.icon_rect.Right + 1;
				this.label_rect.Width = text_size.Width;
				int num3 = num2;
				this.icon_rect.Height = num3;
				this.label_rect.Height = num3;
				this.item_rect = Rectangle.Union(this.icon_rect, this.label_rect);
				this.bounds.Size = Rectangle.Union(this.item_rect, this.checkbox_rect).Size;
				return;
			}
			case View.Tile:
				if (Application.VisualStylesEnabled)
				{
					this.label_rect = (this.icon_rect = Rectangle.Empty);
					if (this.owner.LargeImageList != null)
					{
						this.icon_rect.Width = this.owner.LargeImageList.ImageSize.Width;
						this.icon_rect.Height = this.owner.LargeImageList.ImageSize.Height;
					}
					int num5 = 2;
					SizeF sizeF2 = TextRenderer.MeasureString(this.Text, this.Font);
					int num6 = (int)Math.Ceiling((double)sizeF2.Height);
					int num7 = (int)Math.Ceiling((double)sizeF2.Width);
					this.sub_items[0].bounds.Height = num6;
					int num8 = num6;
					int num9 = num7;
					int num10 = Math.Min(this.owner.Columns.Count, this.sub_items.Count);
					for (int k = 1; k < num10; k++)
					{
						ListViewItem.ListViewSubItem listViewSubItem = this.sub_items[k];
						if (listViewSubItem.Text != null && listViewSubItem.Text.Length != 0)
						{
							sizeF2 = TextRenderer.MeasureString(listViewSubItem.Text, listViewSubItem.Font);
							int num11 = (int)Math.Ceiling((double)sizeF2.Width);
							if (num11 > num9)
							{
								num9 = num11;
							}
							int num12 = (int)Math.Ceiling((double)sizeF2.Height);
							num8 += num12 + num5;
							listViewSubItem.bounds.Height = num12;
						}
					}
					num9 = Math.Min(num9, this.owner.TileSize.Width - (this.icon_rect.Width + 4));
					this.label_rect.X = this.icon_rect.Right + 4;
					this.label_rect.Y = this.owner.TileSize.Height / 2 - num8 / 2;
					this.label_rect.Width = num9;
					this.label_rect.Height = num8;
					this.sub_items[0].SetBounds(this.label_rect.X, this.label_rect.Y, num9, this.sub_items[0].bounds.Height);
					int num13 = this.sub_items[0].bounds.Bottom + num5;
					for (int l = 1; l < num10; l++)
					{
						ListViewItem.ListViewSubItem listViewSubItem2 = this.sub_items[l];
						if (listViewSubItem2.Text != null && listViewSubItem2.Text.Length != 0)
						{
							listViewSubItem2.SetBounds(this.label_rect.X, num13, num9, listViewSubItem2.bounds.Height);
							num13 += listViewSubItem2.Bounds.Height + num5;
						}
					}
					this.item_rect = Rectangle.Union(this.icon_rect, this.label_rect);
					this.bounds.Size = this.item_rect.Size;
					return;
				}
				break;
			default:
				return;
			}
			this.label_rect = (this.icon_rect = Rectangle.Empty);
			SizeF sizeF3 = TextRenderer.MeasureString(this.Text, this.Font);
			if ((int)sizeF3.Width > text_size.Width)
			{
				if (this.Focused && this.owner.InternalContainsFocus)
				{
					int width = text_size.Width;
					StringFormat stringFormat = new StringFormat();
					stringFormat.Alignment = 1;
					text_size.Height = (int)TextRenderer.MeasureString(this.Text, this.Font, width, stringFormat).Height;
				}
				else
				{
					text_size.Height = 2 * (int)sizeF3.Height;
				}
			}
			if (this.owner.LargeImageList != null)
			{
				this.icon_rect.Width = this.owner.LargeImageList.ImageSize.Width;
				this.icon_rect.Height = this.owner.LargeImageList.ImageSize.Height;
			}
			if (this.checkbox_rect.Height > this.icon_rect.Height)
			{
				this.icon_rect.Y = this.checkbox_rect.Height - this.icon_rect.Height;
			}
			else
			{
				this.checkbox_rect.Y = this.icon_rect.Height - this.checkbox_rect.Height;
			}
			if (text_size.Width <= this.icon_rect.Width)
			{
				this.icon_rect.X = this.checkbox_rect.Width + 1;
				this.label_rect.X = this.icon_rect.X + (this.icon_rect.Width - text_size.Width) / 2;
				this.label_rect.Y = this.icon_rect.Bottom + 2;
				this.label_rect.Size = text_size;
			}
			else
			{
				int num14 = text_size.Width / 2;
				this.icon_rect.X = this.checkbox_rect.Width + 1 + num14 - this.icon_rect.Width / 2;
				this.label_rect.X = this.checkbox_rect.Width + 1;
				this.label_rect.Y = this.icon_rect.Bottom + 2;
				this.label_rect.Size = text_size;
			}
			this.item_rect = Rectangle.Union(this.icon_rect, this.label_rect);
			this.bounds.Size = Rectangle.Union(this.item_rect, this.checkbox_rect).Size;
		}

		// Token: 0x040012AC RID: 4780
		private int image_index;

		// Token: 0x040012AD RID: 4781
		private bool is_checked;

		// Token: 0x040012AE RID: 4782
		private int state_image_index;

		// Token: 0x040012AF RID: 4783
		private ListViewItem.ListViewSubItemCollection sub_items;

		// Token: 0x040012B0 RID: 4784
		private object tag;

		// Token: 0x040012B1 RID: 4785
		private bool use_item_style;

		// Token: 0x040012B2 RID: 4786
		private int display_index;

		// Token: 0x040012B3 RID: 4787
		private ListViewGroup group;

		// Token: 0x040012B4 RID: 4788
		private string name;

		// Token: 0x040012B5 RID: 4789
		private string image_key;

		// Token: 0x040012B6 RID: 4790
		private string tooltip_text;

		// Token: 0x040012B7 RID: 4791
		private int indent_count;

		// Token: 0x040012B8 RID: 4792
		private Point position;

		// Token: 0x040012B9 RID: 4793
		private Rectangle bounds;

		// Token: 0x040012BA RID: 4794
		private Rectangle checkbox_rect;

		// Token: 0x040012BB RID: 4795
		private Rectangle icon_rect;

		// Token: 0x040012BC RID: 4796
		private Rectangle item_rect;

		// Token: 0x040012BD RID: 4797
		private Rectangle label_rect;

		// Token: 0x040012BE RID: 4798
		private ListView owner;

		// Token: 0x040012BF RID: 4799
		private Font font;

		// Token: 0x040012C0 RID: 4800
		private Font hot_font;

		// Token: 0x040012C1 RID: 4801
		private bool selected;

		// Token: 0x040012C2 RID: 4802
		internal int row;

		// Token: 0x040012C3 RID: 4803
		internal int col;

		// Token: 0x040012C4 RID: 4804
		private Rectangle text_bounds;

		/// <summary>Represents a subitem of a <see cref="T:System.Windows.Forms.ListViewItem" />.</summary>
		// Token: 0x02000231 RID: 561
		[ToolboxItem(false)]
		[DefaultProperty("Text")]
		[DesignTimeVisible(false)]
		[TypeConverter(typeof(ListViewSubItemConverter))]
		[Serializable]
		public class ListViewSubItem
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> class with default values.</summary>
			// Token: 0x060024E6 RID: 9446 RVA: 0x0008BF2C File Offset: 0x0008A12C
			public ListViewSubItem()
				: this(null, string.Empty, Color.Empty, Color.Empty, null)
			{
			}

			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> class with the specified owner and text.</summary>
			/// <param name="owner">A <see cref="T:System.Windows.Forms.ListViewItem" /> that represents the item that owns the subitem. </param>
			/// <param name="text">The text to display for the subitem. </param>
			// Token: 0x060024E7 RID: 9447 RVA: 0x0008BF48 File Offset: 0x0008A148
			public ListViewSubItem(ListViewItem owner, string text)
				: this(owner, text, Color.Empty, Color.Empty, null)
			{
			}

			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> class with the specified owner, text, foreground color, background color, and font values.</summary>
			/// <param name="owner">A <see cref="T:System.Windows.Forms.ListViewItem" /> that represents the item that owns the subitem. </param>
			/// <param name="text">The text to display for the subitem. </param>
			/// <param name="foreColor">A <see cref="T:System.Drawing.Color" /> that represents the foreground color of the subitem. </param>
			/// <param name="backColor">A <see cref="T:System.Drawing.Color" /> that represents the background color of the subitem. </param>
			/// <param name="font">A <see cref="T:System.Drawing.Font" /> that represents the font to display the subitem's text in. </param>
			// Token: 0x060024E8 RID: 9448 RVA: 0x0008BF60 File Offset: 0x0008A160
			public ListViewSubItem(ListViewItem owner, string text, Color foreColor, Color backColor, Font font)
			{
				this.owner = owner;
				this.Text = text;
				this.style = new ListViewItem.ListViewSubItem.SubItemStyle(foreColor, backColor, font);
			}

			// Token: 0x14000235 RID: 565
			// (add) Token: 0x060024E9 RID: 9449 RVA: 0x0008BF9C File Offset: 0x0008A19C
			// (remove) Token: 0x060024EA RID: 9450 RVA: 0x0008BFB8 File Offset: 0x0008A1B8
			[field: NonSerialized]
			internal event EventHandler UIATextChanged;

			// Token: 0x060024EB RID: 9451 RVA: 0x0008BFD4 File Offset: 0x0008A1D4
			private void OnUIATextChanged()
			{
				if (this.UIATextChanged != null)
				{
					this.UIATextChanged.Invoke(this, EventArgs.Empty);
				}
			}

			/// <summary>Gets or sets the background color of the subitem's text.</summary>
			/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the background color of the subitem's text.</returns>
			// Token: 0x1700091C RID: 2332
			// (get) Token: 0x060024EC RID: 9452 RVA: 0x0008BFF4 File Offset: 0x0008A1F4
			// (set) Token: 0x060024ED RID: 9453 RVA: 0x0008C060 File Offset: 0x0008A260
			public Color BackColor
			{
				get
				{
					if (this.style.backColor != Color.Empty)
					{
						return this.style.backColor;
					}
					if (this.owner != null && this.owner.ListView != null)
					{
						return this.owner.ListView.BackColor;
					}
					return ThemeEngine.Current.ColorWindow;
				}
				set
				{
					this.style.backColor = value;
					this.Invalidate();
				}
			}

			/// <summary>Gets the bounding rectangle of the <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" />.</summary>
			/// <returns>The bounding <see cref="T:System.Drawing.Rectangle" /> of the <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" />.</returns>
			// Token: 0x1700091D RID: 2333
			// (get) Token: 0x060024EE RID: 9454 RVA: 0x0008C074 File Offset: 0x0008A274
			[Browsable(false)]
			public Rectangle Bounds
			{
				get
				{
					Rectangle rectangle = this.bounds;
					if (this.owner != null)
					{
						rectangle.X += this.owner.Bounds.X;
						rectangle.Y += this.owner.Bounds.Y;
					}
					return rectangle;
				}
			}

			/// <summary>Gets or sets the font of the text displayed by the subitem.</summary>
			/// <returns>The <see cref="T:System.Drawing.Font" /> to apply to the text displayed by the control.</returns>
			// Token: 0x1700091E RID: 2334
			// (get) Token: 0x060024EF RID: 9455 RVA: 0x0008C0D8 File Offset: 0x0008A2D8
			// (set) Token: 0x060024F0 RID: 9456 RVA: 0x0008C118 File Offset: 0x0008A318
			[Localizable(true)]
			public Font Font
			{
				get
				{
					if (this.style.font != null)
					{
						return this.style.font;
					}
					if (this.owner != null)
					{
						return this.owner.Font;
					}
					return ThemeEngine.Current.DefaultFont;
				}
				set
				{
					if (this.style.font == value)
					{
						return;
					}
					this.style.font = value;
					this.Invalidate();
				}
			}

			/// <summary>Gets or sets the foreground color of the subitem's text.</summary>
			/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the foreground color of the subitem's text.</returns>
			// Token: 0x1700091F RID: 2335
			// (get) Token: 0x060024F1 RID: 9457 RVA: 0x0008C14C File Offset: 0x0008A34C
			// (set) Token: 0x060024F2 RID: 9458 RVA: 0x0008C1B8 File Offset: 0x0008A3B8
			public Color ForeColor
			{
				get
				{
					if (this.style.foreColor != Color.Empty)
					{
						return this.style.foreColor;
					}
					if (this.owner != null && this.owner.ListView != null)
					{
						return this.owner.ListView.ForeColor;
					}
					return ThemeEngine.Current.ColorWindowText;
				}
				set
				{
					this.style.foreColor = value;
					this.Invalidate();
				}
			}

			/// <summary>Gets or sets the name of the <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" />.</summary>
			/// <returns>The name of the <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" />, or an empty string ("") if a name has not been set.</returns>
			// Token: 0x17000920 RID: 2336
			// (get) Token: 0x060024F3 RID: 9459 RVA: 0x0008C1CC File Offset: 0x0008A3CC
			// (set) Token: 0x060024F4 RID: 9460 RVA: 0x0008C1E8 File Offset: 0x0008A3E8
			[Localizable(true)]
			public string Name
			{
				get
				{
					if (this.name == null)
					{
						return string.Empty;
					}
					return this.name;
				}
				set
				{
					this.name = value;
				}
			}

			/// <summary>Gets or sets an object that contains data about the <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" />. </summary>
			/// <returns>An <see cref="T:System.Object" /> that contains data about the <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" />. The default is null.</returns>
			// Token: 0x17000921 RID: 2337
			// (get) Token: 0x060024F5 RID: 9461 RVA: 0x0008C1F4 File Offset: 0x0008A3F4
			// (set) Token: 0x060024F6 RID: 9462 RVA: 0x0008C1FC File Offset: 0x0008A3FC
			[DefaultValue(null)]
			[Localizable(false)]
			[Bindable(true)]
			[TypeConverter(typeof(StringConverter))]
			public object Tag
			{
				get
				{
					return this.userData;
				}
				set
				{
					this.userData = value;
				}
			}

			/// <summary>Gets or sets the text of the subitem.</summary>
			/// <returns>The text to display for the subitem.</returns>
			// Token: 0x17000922 RID: 2338
			// (get) Token: 0x060024F7 RID: 9463 RVA: 0x0008C208 File Offset: 0x0008A408
			// (set) Token: 0x060024F8 RID: 9464 RVA: 0x0008C210 File Offset: 0x0008A410
			[Localizable(true)]
			public string Text
			{
				get
				{
					return this.text;
				}
				set
				{
					if (this.text == value)
					{
						return;
					}
					if (value == null)
					{
						this.text = string.Empty;
					}
					else
					{
						this.text = value;
					}
					this.Invalidate();
					this.OnUIATextChanged();
				}
			}

			/// <summary>Resets the styles applied to the subitem to the default font and colors.</summary>
			// Token: 0x060024F9 RID: 9465 RVA: 0x0008C250 File Offset: 0x0008A450
			public void ResetStyle()
			{
				this.style.Reset();
				this.Invalidate();
			}

			/// <returns>A string that represents the current object.</returns>
			// Token: 0x060024FA RID: 9466 RVA: 0x0008C264 File Offset: 0x0008A464
			public override string ToString()
			{
				return string.Format("ListViewSubItem {{0}}", this.text);
			}

			// Token: 0x060024FB RID: 9467 RVA: 0x0008C278 File Offset: 0x0008A478
			private void Invalidate()
			{
				if (this.owner == null || this.owner.owner == null)
				{
					return;
				}
				this.owner.Invalidate();
			}

			// Token: 0x060024FC RID: 9468 RVA: 0x0008C2A4 File Offset: 0x0008A4A4
			[OnDeserialized]
			private void OnDeserialized(StreamingContext context)
			{
				this.name = null;
				this.userData = null;
			}

			// Token: 0x17000923 RID: 2339
			// (get) Token: 0x060024FD RID: 9469 RVA: 0x0008C2B4 File Offset: 0x0008A4B4
			internal int Height
			{
				get
				{
					return this.bounds.Height;
				}
			}

			// Token: 0x060024FE RID: 9470 RVA: 0x0008C2C4 File Offset: 0x0008A4C4
			internal void SetBounds(int x, int y, int width, int height)
			{
				this.bounds = new Rectangle(x, y, width, height);
			}

			// Token: 0x040012C8 RID: 4808
			[NonSerialized]
			internal ListViewItem owner;

			// Token: 0x040012C9 RID: 4809
			private string text = string.Empty;

			// Token: 0x040012CA RID: 4810
			private string name;

			// Token: 0x040012CB RID: 4811
			private object userData;

			// Token: 0x040012CC RID: 4812
			private ListViewItem.ListViewSubItem.SubItemStyle style;

			// Token: 0x040012CD RID: 4813
			[NonSerialized]
			internal Rectangle bounds;

			// Token: 0x02000232 RID: 562
			[Serializable]
			private class SubItemStyle
			{
				// Token: 0x060024FF RID: 9471 RVA: 0x0008C2D8 File Offset: 0x0008A4D8
				public SubItemStyle()
				{
				}

				// Token: 0x06002500 RID: 9472 RVA: 0x0008C2E0 File Offset: 0x0008A4E0
				public SubItemStyle(Color foreColor, Color backColor, Font font)
				{
					this.foreColor = foreColor;
					this.backColor = backColor;
					this.font = font;
				}

				// Token: 0x06002501 RID: 9473 RVA: 0x0008C300 File Offset: 0x0008A500
				public void Reset()
				{
					this.foreColor = Color.Empty;
					this.backColor = Color.Empty;
					this.font = null;
				}

				// Token: 0x040012CF RID: 4815
				public Color backColor;

				// Token: 0x040012D0 RID: 4816
				public Color foreColor;

				// Token: 0x040012D1 RID: 4817
				public Font font;
			}
		}

		/// <summary>Represents a collection of <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> objects stored in a <see cref="T:System.Windows.Forms.ListViewItem" />.</summary>
		// Token: 0x02000233 RID: 563
		public class ListViewSubItemCollection : ICollection, IEnumerable, IList
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItemCollection" /> class.</summary>
			/// <param name="owner">The <see cref="T:System.Windows.Forms.ListViewItem" /> that owns the collection. </param>
			// Token: 0x06002502 RID: 9474 RVA: 0x0008C320 File Offset: 0x0008A520
			public ListViewSubItemCollection(ListViewItem owner)
				: this(owner, owner.Text)
			{
			}

			// Token: 0x06002503 RID: 9475 RVA: 0x0008C330 File Offset: 0x0008A530
			internal ListViewSubItemCollection(ListViewItem owner, string text)
			{
				this.owner = owner;
				this.list = new ArrayList();
				if (text != null)
				{
					this.Add(text);
				}
			}

			/// <summary>Gets a value indicating whether access to the collection is synchronized (thread safe).</summary>
			/// <returns>true in all cases.</returns>
			// Token: 0x17000924 RID: 2340
			// (get) Token: 0x06002504 RID: 9476 RVA: 0x0008C364 File Offset: 0x0008A564
			bool ICollection.IsSynchronized
			{
				get
				{
					return this.list.IsSynchronized;
				}
			}

			/// <summary>Gets an object that can be used to synchronize access to the collection.</summary>
			/// <returns>The object used to synchronize the collection.</returns>
			// Token: 0x17000925 RID: 2341
			// (get) Token: 0x06002505 RID: 9477 RVA: 0x0008C374 File Offset: 0x0008A574
			object ICollection.SyncRoot
			{
				get
				{
					return this.list.SyncRoot;
				}
			}

			/// <summary>Gets a value indicating whether the collection has a fixed size.</summary>
			/// <returns>false in all cases.</returns>
			// Token: 0x17000926 RID: 2342
			// (get) Token: 0x06002506 RID: 9478 RVA: 0x0008C384 File Offset: 0x0008A584
			bool IList.IsFixedSize
			{
				get
				{
					return this.list.IsFixedSize;
				}
			}

			/// <summary>Gets or sets the <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> at the specified index within the collection.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> that represents the item located at the specified index within the collection.</returns>
			/// <param name="index">The zero-based index of the element to get or set.</param>
			/// <exception cref="T:System.ArgumentOutOfRangeException">The index parameter is less than 0 or greater than or equal to the value of the Count property of the <see cref="T:System.Windows.Forms.ListView.ColumnHeaderCollection" />.</exception>
			/// <exception cref="T:System.ArgumentException">The object is not a <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" />.</exception>
			// Token: 0x17000927 RID: 2343
			// (get) Token: 0x06002507 RID: 9479 RVA: 0x0008C394 File Offset: 0x0008A594
			// (set) Token: 0x06002508 RID: 9480 RVA: 0x0008C3A0 File Offset: 0x0008A5A0
			object IList.Item
			{
				get
				{
					return this[index];
				}
				set
				{
					if (!(value is ListViewItem.ListViewSubItem))
					{
						throw new ArgumentException("Not of type ListViewSubItem", "value");
					}
					this[index] = (ListViewItem.ListViewSubItem)value;
				}
			}

			/// <summary>Copies the item and collection of subitems into an array.</summary>
			/// <param name="dest">An array of <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" />.</param>
			/// <param name="index">The zero-based index in array at which copying begins.</param>
			/// <exception cref="T:System.ArrayTypeMismatchException">The array type is not compatible with <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" />.</exception>
			// Token: 0x06002509 RID: 9481 RVA: 0x0008C3D8 File Offset: 0x0008A5D8
			void ICollection.CopyTo(Array dest, int index)
			{
				this.list.CopyTo(dest, index);
			}

			/// <summary>Adds an existing <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> to the collection.</summary>
			/// <returns>The zero-based index that indicates the location of the object that was added to the collection.</returns>
			/// <param name="item">The <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> to add to the collection.</param>
			/// <exception cref="T:System.ArgumentException">
			///   <paramref name="item" /> is not a <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" />.</exception>
			// Token: 0x0600250A RID: 9482 RVA: 0x0008C3E8 File Offset: 0x0008A5E8
			int IList.Add(object item)
			{
				if (!(item is ListViewItem.ListViewSubItem))
				{
					throw new ArgumentException("Not of type ListViewSubItem", "item");
				}
				ListViewItem.ListViewSubItem listViewSubItem = (ListViewItem.ListViewSubItem)item;
				listViewSubItem.owner = this.owner;
				listViewSubItem.UIATextChanged += new EventHandler(this.OnUIASubItemTextChanged);
				return this.list.Add(listViewSubItem);
			}

			/// <summary>Determines whether the specified subitem is located in the collection.</summary>
			/// <returns>true if the subitem is contained in the collection; otherwise, false.</returns>
			/// <param name="subItem">An object that represents the subitem to locate in the collection.</param>
			// Token: 0x0600250B RID: 9483 RVA: 0x0008C444 File Offset: 0x0008A644
			bool IList.Contains(object subItem)
			{
				if (!(subItem is ListViewItem.ListViewSubItem))
				{
					throw new ArgumentException("Not of type ListViewSubItem", "subItem");
				}
				return this.Contains((ListViewItem.ListViewSubItem)subItem);
			}

			/// <summary>Returns the index within the collection of the specified subitem.</summary>
			/// <returns>The zero-based index of the subitem if it is in the collection; otherwise, -1.</returns>
			/// <param name="subItem">An object that represents the subitem to locate in the collection.</param>
			// Token: 0x0600250C RID: 9484 RVA: 0x0008C470 File Offset: 0x0008A670
			int IList.IndexOf(object subItem)
			{
				if (!(subItem is ListViewItem.ListViewSubItem))
				{
					throw new ArgumentException("Not of type ListViewSubItem", "subItem");
				}
				return this.IndexOf((ListViewItem.ListViewSubItem)subItem);
			}

			/// <summary>Inserts a subitem into the collection at the specified index.</summary>
			/// <param name="index">The zero-based index location where the item is inserted.</param>
			/// <param name="item">An object that represents the subitem to insert into the collection.</param>
			/// <exception cref="T:System.ArgumentException">
			///   <paramref name="item" /> is not a <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" />.</exception>
			/// <exception cref="T:System.ArgumentOutOfRangeException">The index parameter is less than 0 or greater than or equal to the value of the Count property of the <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItemCollection" />.</exception>
			// Token: 0x0600250D RID: 9485 RVA: 0x0008C49C File Offset: 0x0008A69C
			void IList.Insert(int index, object item)
			{
				if (!(item is ListViewItem.ListViewSubItem))
				{
					throw new ArgumentException("Not of type ListViewSubItem", "item");
				}
				this.Insert(index, (ListViewItem.ListViewSubItem)item);
			}

			/// <summary>Removes a specified item from the collection.</summary>
			/// <param name="item">The item to remove from the collection.</param>
			// Token: 0x0600250E RID: 9486 RVA: 0x0008C4D4 File Offset: 0x0008A6D4
			void IList.Remove(object item)
			{
				if (!(item is ListViewItem.ListViewSubItem))
				{
					throw new ArgumentException("Not of type ListViewSubItem", "item");
				}
				this.Remove((ListViewItem.ListViewSubItem)item);
			}

			/// <summary>Gets the number of subitems in the collection.</summary>
			/// <returns>The number of subitems in the collection.</returns>
			// Token: 0x17000928 RID: 2344
			// (get) Token: 0x0600250F RID: 9487 RVA: 0x0008C500 File Offset: 0x0008A700
			[Browsable(false)]
			public int Count
			{
				get
				{
					return this.list.Count;
				}
			}

			/// <summary>Gets a value indicating whether the collection is read-only.</summary>
			/// <returns>true if the collection is read-only; otherwise, false.</returns>
			// Token: 0x17000929 RID: 2345
			// (get) Token: 0x06002510 RID: 9488 RVA: 0x0008C510 File Offset: 0x0008A710
			public bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			/// <summary>Gets or sets the subitem at the specified index within the collection.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> representing the subitem located at the specified index within the collection.</returns>
			/// <param name="index">The index of the item in the collection to retrieve. </param>
			/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameter is less than zero or greater than or equal to the value of the <see cref="P:System.Windows.Forms.ListViewItem.ListViewSubItemCollection.Count" /> property of the <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItemCollection" />. </exception>
			// Token: 0x1700092A RID: 2346
			public ListViewItem.ListViewSubItem this[int index]
			{
				get
				{
					return (ListViewItem.ListViewSubItem)this.list[index];
				}
				set
				{
					value.owner = this.owner;
					this.list[index] = value;
					this.owner.Layout();
					this.owner.Invalidate();
				}
			}

			/// <summary>Gets an item with the specified key from the collection.</summary>
			/// <returns>The <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> with the specified key.</returns>
			/// <param name="key">The name of the <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> to retrieve.</param>
			// Token: 0x1700092B RID: 2347
			public virtual ListViewItem.ListViewSubItem this[string key]
			{
				get
				{
					int num = this.IndexOfKey(key);
					if (num == -1)
					{
						return null;
					}
					return (ListViewItem.ListViewSubItem)this.list[num];
				}
			}

			/// <summary>Adds an existing <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> to the collection.</summary>
			/// <returns>The <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> that was added to the collection.</returns>
			/// <param name="item">The <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> to add to the collection. </param>
			// Token: 0x06002514 RID: 9492 RVA: 0x0008C58C File Offset: 0x0008A78C
			public ListViewItem.ListViewSubItem Add(ListViewItem.ListViewSubItem item)
			{
				this.AddSubItem(item);
				this.owner.Layout();
				this.owner.Invalidate();
				return item;
			}

			/// <summary>Adds a subitem to the collection with specified text.</summary>
			/// <returns>The <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> that was added to the collection.</returns>
			/// <param name="text">The text to display for the subitem. </param>
			// Token: 0x06002515 RID: 9493 RVA: 0x0008C5AC File Offset: 0x0008A7AC
			public ListViewItem.ListViewSubItem Add(string text)
			{
				ListViewItem.ListViewSubItem listViewSubItem = new ListViewItem.ListViewSubItem(this.owner, text);
				return this.Add(listViewSubItem);
			}

			/// <summary>Adds a subitem to the collection with specified text, foreground color, background color, and font settings.</summary>
			/// <returns>The <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> that was added to the collection.</returns>
			/// <param name="text">The text to display for the subitem. </param>
			/// <param name="foreColor">A <see cref="T:System.Drawing.Color" /> that represents the foreground color of the subitem. </param>
			/// <param name="backColor">A <see cref="T:System.Drawing.Color" /> that represents the background color of the subitem. </param>
			/// <param name="font">A <see cref="T:System.Drawing.Font" /> that represents the typeface to display the subitem's text in. </param>
			// Token: 0x06002516 RID: 9494 RVA: 0x0008C5D0 File Offset: 0x0008A7D0
			public ListViewItem.ListViewSubItem Add(string text, Color foreColor, Color backColor, Font font)
			{
				ListViewItem.ListViewSubItem listViewSubItem = new ListViewItem.ListViewSubItem(this.owner, text, foreColor, backColor, font);
				return this.Add(listViewSubItem);
			}

			/// <summary>Adds an array of <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> objects to the collection.</summary>
			/// <param name="items">An array of <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> objects to add to the collection. </param>
			// Token: 0x06002517 RID: 9495 RVA: 0x0008C5F8 File Offset: 0x0008A7F8
			public void AddRange(ListViewItem.ListViewSubItem[] items)
			{
				if (items == null)
				{
					throw new ArgumentNullException("items");
				}
				foreach (ListViewItem.ListViewSubItem listViewSubItem in items)
				{
					if (listViewSubItem != null)
					{
						this.AddSubItem(listViewSubItem);
					}
				}
				this.owner.Layout();
				this.owner.Invalidate();
			}

			/// <summary>Creates new subitems based on an array and adds them to the collection.</summary>
			/// <param name="items">An array of strings representing the text of each subitem to add to the collection. </param>
			// Token: 0x06002518 RID: 9496 RVA: 0x0008C658 File Offset: 0x0008A858
			public void AddRange(string[] items)
			{
				if (items == null)
				{
					throw new ArgumentNullException("items");
				}
				foreach (string text in items)
				{
					if (text != null)
					{
						this.AddSubItem(new ListViewItem.ListViewSubItem(this.owner, text));
					}
				}
				this.owner.Layout();
				this.owner.Invalidate();
			}

			/// <summary>Creates new subitems based on an array and adds them to the collection with specified foreground color, background color, and font.</summary>
			/// <param name="items">An array of strings representing the text of each subitem to add to the collection. </param>
			/// <param name="foreColor">A <see cref="T:System.Drawing.Color" /> that represents the foreground color of the subitem. </param>
			/// <param name="backColor">A <see cref="T:System.Drawing.Color" /> that represents the background color of the subitem. </param>
			/// <param name="font">A <see cref="T:System.Drawing.Font" /> that represents the typeface to display the subitem's text in. </param>
			// Token: 0x06002519 RID: 9497 RVA: 0x0008C6C4 File Offset: 0x0008A8C4
			public void AddRange(string[] items, Color foreColor, Color backColor, Font font)
			{
				if (items == null)
				{
					throw new ArgumentNullException("items");
				}
				foreach (string text in items)
				{
					if (text != null)
					{
						this.AddSubItem(new ListViewItem.ListViewSubItem(this.owner, text, foreColor, backColor, font));
					}
				}
				this.owner.Layout();
				this.owner.Invalidate();
			}

			// Token: 0x0600251A RID: 9498 RVA: 0x0008C734 File Offset: 0x0008A934
			private void AddSubItem(ListViewItem.ListViewSubItem subItem)
			{
				subItem.owner = this.owner;
				this.list.Add(subItem);
				subItem.UIATextChanged += new EventHandler(this.OnUIASubItemTextChanged);
			}

			/// <summary>Removes all subitems and the parent <see cref="T:System.Windows.Forms.ListViewItem" /> from the collection.</summary>
			// Token: 0x0600251B RID: 9499 RVA: 0x0008C764 File Offset: 0x0008A964
			public void Clear()
			{
				this.list.Clear();
			}

			/// <summary>Determines whether the specified subitem is located in the collection.</summary>
			/// <returns>true if the subitem is contained in the collection; otherwise, false.</returns>
			/// <param name="subItem">A <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> representing the subitem to locate in the collection. </param>
			// Token: 0x0600251C RID: 9500 RVA: 0x0008C774 File Offset: 0x0008A974
			public bool Contains(ListViewItem.ListViewSubItem subItem)
			{
				return this.list.Contains(subItem);
			}

			/// <summary>Determines if the collection contains an item with the specified key.</summary>
			/// <returns>true to indicate the collection contains an item with the specified key; otherwise, false. </returns>
			/// <param name="key">The name of the <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> to look for.</param>
			// Token: 0x0600251D RID: 9501 RVA: 0x0008C784 File Offset: 0x0008A984
			public virtual bool ContainsKey(string key)
			{
				return this.IndexOfKey(key) != -1;
			}

			/// <summary>Returns an enumerator to use to iterate through the subitem collection.</summary>
			/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that represents the subitem collection.</returns>
			// Token: 0x0600251E RID: 9502 RVA: 0x0008C794 File Offset: 0x0008A994
			public IEnumerator GetEnumerator()
			{
				return this.list.GetEnumerator();
			}

			/// <summary>Returns the index within the collection of the specified subitem.</summary>
			/// <returns>The zero-based index of the subitem's location in the collection. If the subitem is not located in the collection, the return value is negative one (-1).</returns>
			/// <param name="subItem">A <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> representing the subitem to locate in the collection. </param>
			// Token: 0x0600251F RID: 9503 RVA: 0x0008C7A4 File Offset: 0x0008A9A4
			public int IndexOf(ListViewItem.ListViewSubItem subItem)
			{
				return this.list.IndexOf(subItem);
			}

			/// <summary>Returns the index of the first occurrence of an item with the specified key within the collection.</summary>
			/// <returns>The zero-based index of the first occurrence of an item with the specified key.</returns>
			/// <param name="key">The name of the item to retrieve the index for.</param>
			// Token: 0x06002520 RID: 9504 RVA: 0x0008C7B4 File Offset: 0x0008A9B4
			public virtual int IndexOfKey(string key)
			{
				if (key == null || key.Length == 0)
				{
					return -1;
				}
				for (int i = 0; i < this.list.Count; i++)
				{
					ListViewItem.ListViewSubItem listViewSubItem = (ListViewItem.ListViewSubItem)this.list[i];
					if (string.Compare(listViewSubItem.Name, key, true) == 0)
					{
						return i;
					}
				}
				return -1;
			}

			/// <summary>Inserts a subitem into the collection at the specified index.</summary>
			/// <param name="index">The zero-based index location where the item is inserted. </param>
			/// <param name="item">A <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> representing the subitem to insert into the collection. </param>
			/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameter is less than zero or greater than or equal to the value of the <see cref="P:System.Windows.Forms.ListViewItem.ListViewSubItemCollection.Count" /> property of the <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItemCollection" />. </exception>
			// Token: 0x06002521 RID: 9505 RVA: 0x0008C818 File Offset: 0x0008AA18
			public void Insert(int index, ListViewItem.ListViewSubItem item)
			{
				item.owner = this.owner;
				this.list.Insert(index, item);
				this.owner.Layout();
				this.owner.Invalidate();
				item.UIATextChanged += new EventHandler(this.OnUIASubItemTextChanged);
			}

			/// <summary>Removes a specified item from the collection.</summary>
			/// <param name="item">The item to remove from the collection.</param>
			// Token: 0x06002522 RID: 9506 RVA: 0x0008C868 File Offset: 0x0008AA68
			public void Remove(ListViewItem.ListViewSubItem item)
			{
				this.list.Remove(item);
				this.owner.Layout();
				this.owner.Invalidate();
				item.UIATextChanged -= new EventHandler(this.OnUIASubItemTextChanged);
			}

			/// <summary>Removes an item with the specified key from the collection.</summary>
			/// <param name="key">The name of the item to remove from the collection.</param>
			// Token: 0x06002523 RID: 9507 RVA: 0x0008C8AC File Offset: 0x0008AAAC
			public virtual void RemoveByKey(string key)
			{
				int num = this.IndexOfKey(key);
				if (num != -1)
				{
					this.RemoveAt(num);
				}
			}

			/// <summary>Removes the subitem at the specified index within the collection.</summary>
			/// <param name="index">The zero-based index of the subitem to remove. </param>
			/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameter is less than zero or greater than or equal to the value of the <see cref="P:System.Windows.Forms.ListViewItem.ListViewSubItemCollection.Count" /> property of the <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItemCollection" />. </exception>
			// Token: 0x06002524 RID: 9508 RVA: 0x0008C8D0 File Offset: 0x0008AAD0
			public void RemoveAt(int index)
			{
				if (index >= 0 && index < this.list.Count)
				{
					((ListViewItem.ListViewSubItem)this.list[index]).UIATextChanged -= new EventHandler(this.OnUIASubItemTextChanged);
				}
				this.list.RemoveAt(index);
			}

			// Token: 0x06002525 RID: 9509 RVA: 0x0008C924 File Offset: 0x0008AB24
			private void OnUIASubItemTextChanged(object sender, EventArgs args)
			{
				this.owner.OnUIASubItemTextChanged(new LabelEditEventArgs(this.list.IndexOf(sender)));
			}

			// Token: 0x040012D2 RID: 4818
			private ArrayList list;

			// Token: 0x040012D3 RID: 4819
			internal ListViewItem owner;
		}
	}
}
