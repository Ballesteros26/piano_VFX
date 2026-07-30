using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Imaging;
using System.Globalization;

namespace System.Windows.Forms
{
	/// <summary>Provides methods to manage a collection of <see cref="T:System.Drawing.Image" /> objects. This class cannot be inherited.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001D8 RID: 472
	[DesignerSerializer("System.Windows.Forms.Design.ImageListCodeDomSerializer, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.Serialization.CodeDomSerializer, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[ToolboxItemFilter("System.Windows.Forms")]
	[TypeConverter(typeof(ImageListConverter))]
	[Designer("System.Windows.Forms.Design.ImageListDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[DefaultProperty("Images")]
	public sealed class ImageList : Component
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ImageList" /> class with default values for <see cref="P:System.Windows.Forms.ImageList.ColorDepth" />, <see cref="P:System.Windows.Forms.ImageList.ImageSize" />, and <see cref="P:System.Windows.Forms.ImageList.TransparentColor" />.</summary>
		// Token: 0x06001E18 RID: 7704 RVA: 0x0007103C File Offset: 0x0006F23C
		public ImageList()
		{
			this.images = new ImageList.ImageCollection(this);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ImageList" /> class, associating it with a container.</summary>
		/// <param name="container">An object implementing <see cref="T:System.ComponentModel.IContainer" /> to associate with this instance of <see cref="T:System.Windows.Forms.ImageList" />. </param>
		// Token: 0x06001E19 RID: 7705 RVA: 0x00071050 File Offset: 0x0006F250
		public ImageList(IContainer container)
			: this()
		{
			container.Add(this);
		}

		// Token: 0x06001E1A RID: 7706 RVA: 0x00071060 File Offset: 0x0006F260
		// Note: this type is marked as 'beforefieldinit'.
		static ImageList()
		{
			ImageList.RecreateHandleEvent = new object();
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.ImageList.Handle" /> is recreated.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140001EA RID: 490
		// (add) Token: 0x06001E1B RID: 7707 RVA: 0x00071090 File Offset: 0x0006F290
		// (remove) Token: 0x06001E1C RID: 7708 RVA: 0x000710A4 File Offset: 0x0006F2A4
		[EditorBrowsable(2)]
		[Browsable(false)]
		public event EventHandler RecreateHandle
		{
			add
			{
				base.Events.AddHandler(ImageList.RecreateHandleEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ImageList.RecreateHandleEvent, value);
			}
		}

		// Token: 0x06001E1D RID: 7709 RVA: 0x000710B8 File Offset: 0x0006F2B8
		private void OnRecreateHandle()
		{
			EventHandler eventHandler = (EventHandler)base.Events[ImageList.RecreateHandleEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, EventArgs.Empty);
			}
		}

		// Token: 0x06001E1E RID: 7710 RVA: 0x000710F0 File Offset: 0x0006F2F0
		internal bool ShouldSerializeTransparentColor()
		{
			return this.TransparentColor != Color.LightGray;
		}

		// Token: 0x06001E1F RID: 7711 RVA: 0x00071104 File Offset: 0x0006F304
		internal bool ShouldSerializeColorDepth()
		{
			return this.images.Empty;
		}

		// Token: 0x06001E20 RID: 7712 RVA: 0x00071114 File Offset: 0x0006F314
		internal bool ShouldSerializeImageSize()
		{
			return this.images.Empty;
		}

		// Token: 0x06001E21 RID: 7713 RVA: 0x00071124 File Offset: 0x0006F324
		internal void ResetColorDepth()
		{
			this.ColorDepth = ColorDepth.Depth8Bit;
		}

		// Token: 0x06001E22 RID: 7714 RVA: 0x00071130 File Offset: 0x0006F330
		internal void ResetImageSize()
		{
			this.ImageSize = ImageList.DefaultImageSize;
		}

		// Token: 0x06001E23 RID: 7715 RVA: 0x00071140 File Offset: 0x0006F340
		internal void ResetTransparentColor()
		{
			this.TransparentColor = Color.LightGray;
		}

		/// <summary>Gets the color depth of the image list.</summary>
		/// <returns>The number of available colors for the image. In the .NET Framework version 1.0, the default is <see cref="F:System.Windows.Forms.ColorDepth.Depth4Bit" />. In the .NET Framework version 1.1 or later, the default is <see cref="F:System.Windows.Forms.ColorDepth.Depth8Bit" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The color depth is not a valid <see cref="T:System.Windows.Forms.ColorDepth" /> enumeration value. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000758 RID: 1880
		// (get) Token: 0x06001E24 RID: 7716 RVA: 0x00071150 File Offset: 0x0006F350
		// (set) Token: 0x06001E25 RID: 7717 RVA: 0x00071160 File Offset: 0x0006F360
		public ColorDepth ColorDepth
		{
			get
			{
				return this.images.ColorDepth;
			}
			set
			{
				this.images.ColorDepth = value;
			}
		}

		/// <summary>Gets the handle of the image list object.</summary>
		/// <returns>The handle for the image list. The default is null.</returns>
		/// <exception cref="T:System.InvalidOperationException">Creating the handle for the <see cref="T:System.Windows.Forms.ImageList" /> failed.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000759 RID: 1881
		// (get) Token: 0x06001E26 RID: 7718 RVA: 0x00071170 File Offset: 0x0006F370
		[EditorBrowsable(2)]
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public IntPtr Handle
		{
			get
			{
				return this.images.Handle;
			}
		}

		/// <summary>Gets a value indicating whether the underlying Win32 handle has been created.</summary>
		/// <returns>true if the <see cref="P:System.Windows.Forms.ImageList.Handle" /> has been created; otherwise, false. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700075A RID: 1882
		// (get) Token: 0x06001E27 RID: 7719 RVA: 0x00071180 File Offset: 0x0006F380
		[DesignerSerializationVisibility(0)]
		[EditorBrowsable(2)]
		[Browsable(false)]
		public bool HandleCreated
		{
			get
			{
				return this.images.HandleCreated;
			}
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.ImageList.ImageCollection" /> for this image list.</summary>
		/// <returns>The collection of images.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700075B RID: 1883
		// (get) Token: 0x06001E28 RID: 7720 RVA: 0x00071190 File Offset: 0x0006F390
		[DesignerSerializationVisibility(0)]
		[MergableProperty(false)]
		[DefaultValue(null)]
		public ImageList.ImageCollection Images
		{
			get
			{
				return this.images;
			}
		}

		/// <summary>Gets or sets the size of the images in the image list.</summary>
		/// <returns>The <see cref="T:System.Drawing.Size" /> that defines the height and width, in pixels, of the images in the list. The default size is 16 by 16. The maximum size is 256 by 256.</returns>
		/// <exception cref="T:System.ArgumentException">The value assigned is equal to <see cref="P:System.Drawing.Size.IsEmpty" />.-or- The value of the height or width is less than or equal to 0.-or- The value of the height or width is greater than 256. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The new size has a dimension less than 0 or greater than 256.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700075C RID: 1884
		// (get) Token: 0x06001E29 RID: 7721 RVA: 0x00071198 File Offset: 0x0006F398
		// (set) Token: 0x06001E2A RID: 7722 RVA: 0x000711A8 File Offset: 0x0006F3A8
		[Localizable(true)]
		public Size ImageSize
		{
			get
			{
				return this.images.ImageSize;
			}
			set
			{
				this.images.ImageSize = value;
			}
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.ImageListStreamer" /> associated with this image list.</summary>
		/// <returns>null if the image list is empty; otherwise, a <see cref="T:System.Windows.Forms.ImageListStreamer" /> for this <see cref="T:System.Windows.Forms.ImageList" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700075D RID: 1885
		// (get) Token: 0x06001E2B RID: 7723 RVA: 0x000711B8 File Offset: 0x0006F3B8
		// (set) Token: 0x06001E2C RID: 7724 RVA: 0x000711C8 File Offset: 0x0006F3C8
		[EditorBrowsable(2)]
		[Browsable(false)]
		[DefaultValue(null)]
		public ImageListStreamer ImageStream
		{
			get
			{
				return this.images.ImageStream;
			}
			set
			{
				this.images.ImageStream = value;
			}
		}

		/// <summary>Gets or sets an object that contains additional data about the <see cref="T:System.Windows.Forms.ImageList" />.</summary>
		/// <returns>An <see cref="T:System.Object" /> that contains additional data about the <see cref="T:System.Windows.Forms.ImageList" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700075E RID: 1886
		// (get) Token: 0x06001E2D RID: 7725 RVA: 0x000711D8 File Offset: 0x0006F3D8
		// (set) Token: 0x06001E2E RID: 7726 RVA: 0x000711E0 File Offset: 0x0006F3E0
		[Bindable(true)]
		[TypeConverter(typeof(StringConverter))]
		[Localizable(false)]
		[DefaultValue(null)]
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

		/// <summary>Gets or sets the color to treat as transparent.</summary>
		/// <returns>One of the <see cref="T:System.Drawing.Color" /> values. The default is Transparent.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700075F RID: 1887
		// (get) Token: 0x06001E2F RID: 7727 RVA: 0x000711EC File Offset: 0x0006F3EC
		// (set) Token: 0x06001E30 RID: 7728 RVA: 0x000711FC File Offset: 0x0006F3FC
		public Color TransparentColor
		{
			get
			{
				return this.images.TransparentColor;
			}
			set
			{
				this.images.TransparentColor = value;
			}
		}

		/// <summary>Draws the image indicated by the specified index on the specified <see cref="T:System.Drawing.Graphics" /> at the given location.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> to draw on. </param>
		/// <param name="pt">The location defined by a <see cref="T:System.Drawing.Point" /> at which to draw the image. </param>
		/// <param name="index">The index of the image in the <see cref="T:System.Windows.Forms.ImageList" /> to draw. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index is less than 0.-or- The index is greater than or equal to the count of images in the image list. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001E31 RID: 7729 RVA: 0x0007120C File Offset: 0x0006F40C
		public void Draw(Graphics g, Point pt, int index)
		{
			this.Draw(g, pt.X, pt.Y, index);
		}

		/// <summary>Draws the image indicated by the given index on the specified <see cref="T:System.Drawing.Graphics" /> at the specified location.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> to draw on. </param>
		/// <param name="x">The horizontal position at which to draw the image. </param>
		/// <param name="y">The vertical position at which to draw the image. </param>
		/// <param name="index">The index of the image in the <see cref="T:System.Windows.Forms.ImageList" /> to draw. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index is less than 0.-or- The index is greater than or equal to the count of images in the image list. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001E32 RID: 7730 RVA: 0x00071230 File Offset: 0x0006F430
		public void Draw(Graphics g, int x, int y, int index)
		{
			g.DrawImage(this.images.GetImage(index), x, y);
		}

		/// <summary>Draws the image indicated by the given index on the specified <see cref="T:System.Drawing.Graphics" /> using the specified location and size.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> to draw on. </param>
		/// <param name="x">The horizontal position at which to draw the image. </param>
		/// <param name="y">The vertical position at which to draw the image. </param>
		/// <param name="width">The width, in pixels, of the destination image. </param>
		/// <param name="height">The height, in pixels, of the destination image. </param>
		/// <param name="index">The index of the image in the <see cref="T:System.Windows.Forms.ImageList" /> to draw. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index is less than 0.-or- The index is greater than or equal to the count of images in the image list. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001E33 RID: 7731 RVA: 0x00071248 File Offset: 0x0006F448
		public void Draw(Graphics g, int x, int y, int width, int height, int index)
		{
			g.DrawImage(this.images.GetImage(index), x, y, width, height);
		}

		/// <summary>Returns a string that represents the current <see cref="T:System.Windows.Forms.ImageList" />.</summary>
		/// <returns>A string that represents the current <see cref="T:System.Windows.Forms.ImageList" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001E34 RID: 7732 RVA: 0x00071270 File Offset: 0x0006F470
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				base.ToString(),
				" Images.Count: ",
				this.images.Count.ToString(),
				", ImageSize: ",
				this.ImageSize.ToString()
			});
		}

		// Token: 0x06001E35 RID: 7733 RVA: 0x000712C8 File Offset: 0x0006F4C8
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.images.DestroyHandle();
			}
			base.Dispose(disposing);
		}

		// Token: 0x04000FCE RID: 4046
		private const ColorDepth DefaultColorDepth = ColorDepth.Depth8Bit;

		// Token: 0x04000FCF RID: 4047
		private static readonly Size DefaultImageSize = new Size(16, 16);

		// Token: 0x04000FD0 RID: 4048
		private static readonly Color DefaultTransparentColor = Color.Transparent;

		// Token: 0x04000FD1 RID: 4049
		private object tag;

		// Token: 0x04000FD2 RID: 4050
		private readonly ImageList.ImageCollection images;

		/// <summary>Encapsulates the collection of <see cref="T:System.Drawing.Image" /> objects in an <see cref="T:System.Windows.Forms.ImageList" />.</summary>
		// Token: 0x020001D9 RID: 473
		[Editor("System.Windows.Forms.Design.ImageCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		public sealed class ImageCollection : ICollection, IEnumerable, IList
		{
			// Token: 0x06001E36 RID: 7734 RVA: 0x000712E4 File Offset: 0x0006F4E4
			internal ImageCollection(ImageList owner)
			{
				this.owner = owner;
			}

			// Token: 0x140001EB RID: 491
			// (add) Token: 0x06001E37 RID: 7735 RVA: 0x00071338 File Offset: 0x0006F538
			// (remove) Token: 0x06001E38 RID: 7736 RVA: 0x00071354 File Offset: 0x0006F554
			internal event EventHandler Changed;

			/// <summary>Gets or sets an image in an existing <see cref="T:System.Windows.Forms.ImageList.ImageCollection" />.</summary>
			/// <returns>The image in the list specified by the index.</returns>
			/// <param name="index">The zero-based index of the image to get or set. </param>
			/// <exception cref="T:System.ArgumentOutOfRangeException">The index is less than 0 or greater than or equal to <see cref="P:System.Windows.Forms.ImageList.ImageCollection.Count" />.</exception>
			/// <exception cref="T:System.Exception">The attempt to replace the image failed.</exception>
			/// <exception cref="T:System.ArgumentNullException">The image to be assigned is null or not a bitmap.</exception>
			// Token: 0x17000760 RID: 1888
			// (get) Token: 0x06001E39 RID: 7737 RVA: 0x00071370 File Offset: 0x0006F570
			// (set) Token: 0x06001E3A RID: 7738 RVA: 0x0007137C File Offset: 0x0006F57C
			object IList.Item
			{
				get
				{
					return this[index];
				}
				set
				{
					if (!(value is Image))
					{
						throw new ArgumentException("value");
					}
					this[index] = (Image)value;
				}
			}

			/// <summary>Gets a value indicating whether the <see cref="T:System.Windows.Forms.ImageList.ImageCollection" /> has a fixed size.</summary>
			/// <returns>false in all cases.</returns>
			// Token: 0x17000761 RID: 1889
			// (get) Token: 0x06001E3B RID: 7739 RVA: 0x000713A4 File Offset: 0x0006F5A4
			bool IList.IsFixedSize
			{
				get
				{
					return false;
				}
			}

			/// <summary>Gets a value indicating whether access to the collection is synchronized (thread safe).</summary>
			/// <returns>false in all cases.</returns>
			// Token: 0x17000762 RID: 1890
			// (get) Token: 0x06001E3C RID: 7740 RVA: 0x000713A8 File Offset: 0x0006F5A8
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			/// <summary>Gets an object that can be used to synchronize access to the collection.</summary>
			/// <returns>The object used to synchronize the <see cref="T:System.Windows.Forms.ImageList.ImageCollection" />.</returns>
			// Token: 0x17000763 RID: 1891
			// (get) Token: 0x06001E3D RID: 7741 RVA: 0x000713AC File Offset: 0x0006F5AC
			object ICollection.SyncRoot
			{
				get
				{
					return this;
				}
			}

			/// <summary>Adds the specified image to the <see cref="T:System.Windows.Forms.ImageList" />.</summary>
			/// <returns>The index of the newly added image, or -1 if the image could not be added.</returns>
			/// <param name="value">The image to add to the list.</param>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="value" /> is null.</exception>
			/// <exception cref="T:System.ArgumentException">
			///   <paramref name="value" /> is not a <see cref="T:System.Drawing.Bitmap" />.</exception>
			// Token: 0x06001E3E RID: 7742 RVA: 0x000713B0 File Offset: 0x0006F5B0
			int IList.Add(object value)
			{
				if (!(value is Image))
				{
					throw new ArgumentException("value");
				}
				int num = this.Count;
				this.Add((Image)value);
				return num;
			}

			/// <summary>Implements the <see cref="M:System.Collections.IList.Contains(System.Object)" /> method. Throws a <see cref="T:System.NotSupportedException" /> in all cases.</summary>
			/// <param name="image">The image to locate in the <see cref="T:System.Windows.Forms.ImageList.ImageCollection" />.</param>
			/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
			// Token: 0x06001E3F RID: 7743 RVA: 0x000713E8 File Offset: 0x0006F5E8
			bool IList.Contains(object image)
			{
				return image is Image && this.Contains((Image)image);
			}

			/// <summary>Implements the <see cref="M:System.Collections.IList.IndexOf(System.Object)" /> method. Throws a <see cref="T:System.NotSupportedException" /> in all cases.</summary>
			/// <param name="image">The image to find in the list.</param>
			/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
			// Token: 0x06001E40 RID: 7744 RVA: 0x00071408 File Offset: 0x0006F608
			int IList.IndexOf(object image)
			{
				return (!(image is Image)) ? (-1) : this.IndexOf((Image)image);
			}

			/// <summary>Implements the <see cref="M:System.Collections.IList.Insert(System.Int32,System.Object)" /> method. Throws a <see cref="T:System.NotSupportedException" /> in all cases.</summary>
			/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
			// Token: 0x06001E41 RID: 7745 RVA: 0x00071428 File Offset: 0x0006F628
			void IList.Insert(int index, object value)
			{
				throw new NotSupportedException();
			}

			/// <summary>Implements the <see cref="M:System.Collections.IList.Remove(System.Object)" />. Throws a <see cref="T:System.NotSupportedException" /> in all cases.</summary>
			/// <param name="image"></param>
			/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
			// Token: 0x06001E42 RID: 7746 RVA: 0x00071430 File Offset: 0x0006F630
			void IList.Remove(object image)
			{
				if (image is Image)
				{
					this.Remove((Image)image);
				}
			}

			/// <summary>Copies the items in this collection to a compatible one-dimensional array, starting at the specified index of the target array.</summary>
			/// <param name="dest">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from the collection. The array must have zero-based indexing.  </param>
			/// <param name="index">The zero-based index in the <see cref="T:System.Array" /> at which copying begins.  </param>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="dest" /> is null.</exception>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			///   <paramref name="index" /> is less than 0.</exception>
			/// <exception cref="T:System.ArgumentException">
			///   <paramref name="dest" /> is multidimensional.-or-The number of elements in the <see cref="T:System.Windows.Forms.ComboBox.ObjectCollection" /> is greater than the available space from <paramref name="index" /> to the end of the destination array.</exception>
			/// <exception cref="T:System.InvalidCastException">The type of the <see cref="T:System.Windows.Forms.ComboBox.ObjectCollection" /> cannot be cast automatically to the type of the destination array.</exception>
			// Token: 0x06001E43 RID: 7747 RVA: 0x0007144C File Offset: 0x0006F64C
			void ICollection.CopyTo(Array dest, int index)
			{
				for (int i = 0; i < this.Count; i++)
				{
					dest.SetValue(this[i], index++);
				}
			}

			// Token: 0x17000764 RID: 1892
			// (get) Token: 0x06001E44 RID: 7748 RVA: 0x00071484 File Offset: 0x0006F684
			// (set) Token: 0x06001E45 RID: 7749 RVA: 0x0007148C File Offset: 0x0006F68C
			internal ColorDepth ColorDepth
			{
				get
				{
					return this.colorDepth;
				}
				set
				{
					if (!Enum.IsDefined(typeof(ColorDepth), value))
					{
						throw new InvalidEnumArgumentException("value", (int)value, typeof(ColorDepth));
					}
					if (this.colorDepth != value)
					{
						this.colorDepth = value;
						this.RecreateHandle();
					}
				}
			}

			// Token: 0x17000765 RID: 1893
			// (get) Token: 0x06001E46 RID: 7750 RVA: 0x000714E4 File Offset: 0x0006F6E4
			internal IntPtr Handle
			{
				get
				{
					this.CreateHandle();
					return (IntPtr)(-1);
				}
			}

			// Token: 0x17000766 RID: 1894
			// (get) Token: 0x06001E47 RID: 7751 RVA: 0x000714F4 File Offset: 0x0006F6F4
			internal bool HandleCreated
			{
				get
				{
					return this.handleCreated;
				}
			}

			// Token: 0x17000767 RID: 1895
			// (get) Token: 0x06001E48 RID: 7752 RVA: 0x000714FC File Offset: 0x0006F6FC
			// (set) Token: 0x06001E49 RID: 7753 RVA: 0x00071504 File Offset: 0x0006F704
			internal Size ImageSize
			{
				get
				{
					return this.imageSize;
				}
				set
				{
					if (value.Width < 1 || value.Width > 256 || value.Height < 1 || value.Height > 256)
					{
						throw new ArgumentException("ImageSize.Width and Height must be between 1 and 256", "value");
					}
					if (this.imageSize != value)
					{
						this.imageSize = value;
						this.RecreateHandle();
					}
				}
			}

			// Token: 0x17000768 RID: 1896
			// (get) Token: 0x06001E4A RID: 7754 RVA: 0x0007157C File Offset: 0x0006F77C
			// (set) Token: 0x06001E4B RID: 7755 RVA: 0x00071598 File Offset: 0x0006F798
			internal ImageListStreamer ImageStream
			{
				get
				{
					return (!this.Empty) ? new ImageListStreamer(this) : null;
				}
				set
				{
					Image[] images;
					if (value == null)
					{
						if (this.handleCreated)
						{
							this.DestroyHandle();
						}
						else
						{
							this.Clear();
						}
					}
					else if ((images = value.Images) != null)
					{
						this.list = new ArrayList(images.Length);
						this.count = 0;
						this.handleCreated = true;
						this.keys = new ArrayList(images.Length);
						for (int i = 0; i < images.Length; i++)
						{
							this.list.Add((Image)images[i].Clone());
							this.keys.Add(null);
						}
						if (Enum.IsDefined(typeof(ColorDepth), value.ColorDepth))
						{
							this.colorDepth = value.ColorDepth;
						}
						this.imageSize = value.ImageSize;
						this.owner.OnRecreateHandle();
					}
				}
			}

			// Token: 0x17000769 RID: 1897
			// (get) Token: 0x06001E4C RID: 7756 RVA: 0x00071680 File Offset: 0x0006F880
			// (set) Token: 0x06001E4D RID: 7757 RVA: 0x00071688 File Offset: 0x0006F888
			internal Color TransparentColor
			{
				get
				{
					return this.transparentColor;
				}
				set
				{
					this.transparentColor = value;
				}
			}

			/// <summary>Gets the number of images currently in the list.</summary>
			/// <returns>The number of images in the list. The default is 0.</returns>
			// Token: 0x1700076A RID: 1898
			// (get) Token: 0x06001E4E RID: 7758 RVA: 0x00071694 File Offset: 0x0006F894
			[Browsable(false)]
			public int Count
			{
				get
				{
					return (!this.handleCreated) ? this.count : this.list.Count;
				}
			}

			/// <summary>Gets a value indicating whether the <see cref="T:System.Windows.Forms.ImageList" /> has any images.</summary>
			/// <returns>true if there are no images in the list; otherwise, false. The default is false.</returns>
			// Token: 0x1700076B RID: 1899
			// (get) Token: 0x06001E4F RID: 7759 RVA: 0x000716B8 File Offset: 0x0006F8B8
			public bool Empty
			{
				get
				{
					return this.Count == 0;
				}
			}

			/// <summary>Gets a value indicating whether the list is read-only.</summary>
			/// <returns>Always false.</returns>
			// Token: 0x1700076C RID: 1900
			// (get) Token: 0x06001E50 RID: 7760 RVA: 0x000716C4 File Offset: 0x0006F8C4
			public bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			/// <summary>Gets or sets an <see cref="T:System.Drawing.Image" /> at the specified index within the collection.</summary>
			/// <returns>The image in the list specified by <paramref name="index" />. </returns>
			/// <param name="index">The index of the image to get or set. </param>
			/// <exception cref="T:System.ArgumentOutOfRangeException">The index is less than 0 or greater than or equal to <see cref="P:System.Windows.Forms.ImageList.ImageCollection.Count" />. </exception>
			/// <exception cref="T:System.ArgumentException">
			///   <paramref name="image" /> is not a <see cref="T:System.Drawing.Bitmap" />.</exception>
			/// <exception cref="T:System.ArgumentNullException">The image to be assigned is null or not a <see cref="T:System.Drawing.Bitmap" />. </exception>
			/// <exception cref="T:System.InvalidOperationException">The image cannot be added to the list.</exception>
			// Token: 0x1700076D RID: 1901
			[Browsable(false)]
			[DesignerSerializationVisibility(0)]
			public Image this[int index]
			{
				get
				{
					return (Image)this.GetImage(index).Clone();
				}
				set
				{
					if (index < 0 || index >= this.Count)
					{
						throw new ArgumentOutOfRangeException("index");
					}
					if (value == null)
					{
						throw new ArgumentNullException("value");
					}
					if (!(value is Bitmap))
					{
						throw new ArgumentException("Image must be a Bitmap.");
					}
					Image image = this.CreateImage(value, this.transparentColor);
					this.CreateHandle();
					this.list[index] = image;
				}
			}

			/// <summary>Gets an <see cref="T:System.Drawing.Image" /> with the specified key from the collection.</summary>
			/// <returns>The <see cref="T:System.Drawing.Image" /> with the specified key.</returns>
			/// <param name="key">The name of the image to retrieve from the collection.</param>
			// Token: 0x1700076E RID: 1902
			public Image this[string key]
			{
				get
				{
					int num;
					return ((num = this.IndexOfKey(key)) != -1) ? this[num] : null;
				}
			}

			/// <summary>Gets the collection of keys associated with the images in the <see cref="T:System.Windows.Forms.ImageList.ImageCollection" />.</summary>
			/// <returns>A <see cref="T:System.Collections.Specialized.StringCollection" /> containing the names of the images in the <see cref="T:System.Windows.Forms.ImageList.ImageCollection" />.</returns>
			// Token: 0x1700076F RID: 1903
			// (get) Token: 0x06001E54 RID: 7764 RVA: 0x0007177C File Offset: 0x0006F97C
			public StringCollection Keys
			{
				get
				{
					StringCollection stringCollection = new StringCollection();
					for (int i = 0; i < this.keys.Count; i++)
					{
						string text;
						stringCollection.Add(((text = (string)this.keys[i]) != null && text.Length != 0) ? text : string.Empty);
					}
					return stringCollection;
				}
			}

			// Token: 0x06001E55 RID: 7765 RVA: 0x000717E4 File Offset: 0x0006F9E4
			private static bool CompareKeys(string key1, string key2)
			{
				return key1 != null && key2 != null && key1.Length == key2.Length && string.Compare(key1, key2, true, CultureInfo.InvariantCulture) == 0;
			}

			// Token: 0x06001E56 RID: 7766 RVA: 0x00071820 File Offset: 0x0006FA20
			private int AddItem(string key, ImageList.ImageCollection.ImageListItem item)
			{
				int num;
				if (this.handleCreated)
				{
					num = this.AddItemInternal(item);
				}
				else
				{
					num = this.list.Add(item);
					this.count += item.ImageCount;
				}
				if ((item.Flags & ImageList.ImageCollection.ItemFlags.ImageStrip) == ImageList.ImageCollection.ItemFlags.None)
				{
					this.keys.Add(key);
				}
				else
				{
					for (int i = 0; i < item.ImageCount; i++)
					{
						this.keys.Add(null);
					}
				}
				return num;
			}

			// Token: 0x06001E57 RID: 7767 RVA: 0x000718AC File Offset: 0x0006FAAC
			private int AddItemInternal(ImageList.ImageCollection.ImageListItem item)
			{
				if (this.Changed != null)
				{
					this.Changed.Invoke(this, EventArgs.Empty);
				}
				if (item.Image is Icon)
				{
					int width;
					int height;
					Bitmap bitmap = new Bitmap(width = this.imageSize.Width, height = this.imageSize.Height, 2498570);
					Graphics graphics = Graphics.FromImage(bitmap);
					graphics.DrawIcon((Icon)item.Image, new Rectangle(0, 0, width, height));
					graphics.Dispose();
					this.ReduceColorDepth(bitmap);
					return this.list.Add(bitmap);
				}
				if ((item.Flags & ImageList.ImageCollection.ItemFlags.ImageStrip) == ImageList.ImageCollection.ItemFlags.None)
				{
					return this.list.Add(this.CreateImage((Image)item.Image, ((item.Flags & ImageList.ImageCollection.ItemFlags.UseTransparentColor) != ImageList.ImageCollection.ItemFlags.None) ? item.TransparentColor : this.transparentColor));
				}
				Image image;
				int width2;
				int width3;
				if ((width2 = (image = (Image)item.Image).Width) == 0 || width2 % (width3 = this.imageSize.Width) != 0)
				{
					throw new ArgumentException("Width of image strip must be a positive multiple of ImageSize.Width.", "value");
				}
				int height2;
				if (image.Height != (height2 = this.imageSize.Height))
				{
					throw new ArgumentException("Height of image strip must be equal to ImageSize.Height.", "value");
				}
				Rectangle rectangle;
				rectangle..ctor(0, 0, width3, height2);
				ImageAttributes imageAttributes;
				if (this.transparentColor.A == 0)
				{
					imageAttributes = null;
				}
				else
				{
					imageAttributes = new ImageAttributes();
					imageAttributes.SetColorKey(this.transparentColor, this.transparentColor);
				}
				int num = this.list.Count;
				for (int i = 0; i < width2; i += width3)
				{
					Bitmap bitmap2 = new Bitmap(width3, height2, 2498570);
					Graphics graphics2 = Graphics.FromImage(bitmap2);
					graphics2.DrawImage(image, rectangle, i, 0, width3, height2, 2, imageAttributes);
					graphics2.Dispose();
					this.ReduceColorDepth(bitmap2);
					this.list.Add(bitmap2);
				}
				if (imageAttributes != null)
				{
					imageAttributes.Dispose();
				}
				return num;
			}

			// Token: 0x06001E58 RID: 7768 RVA: 0x00071AB8 File Offset: 0x0006FCB8
			private void CreateHandle()
			{
				if (!this.handleCreated)
				{
					ArrayList arrayList = this.list;
					this.list = new ArrayList(this.count);
					this.count = 0;
					this.handleCreated = true;
					for (int i = 0; i < arrayList.Count; i++)
					{
						this.AddItemInternal((ImageList.ImageCollection.ImageListItem)arrayList[i]);
					}
				}
			}

			// Token: 0x06001E59 RID: 7769 RVA: 0x00071B20 File Offset: 0x0006FD20
			private Image CreateImage(Image value, Color transparentColor)
			{
				ImageAttributes imageAttributes;
				if (transparentColor.A == 0)
				{
					imageAttributes = null;
				}
				else
				{
					imageAttributes = new ImageAttributes();
					imageAttributes.SetColorKey(transparentColor, transparentColor);
				}
				int width;
				int height;
				Bitmap bitmap = new Bitmap(width = this.imageSize.Width, height = this.imageSize.Height, 2498570);
				Graphics graphics = Graphics.FromImage(bitmap);
				graphics.DrawImage(value, new Rectangle(0, 0, width, height), 0, 0, value.Width, value.Height, 2, imageAttributes);
				graphics.Dispose();
				if (imageAttributes != null)
				{
					imageAttributes.Dispose();
				}
				this.ReduceColorDepth(bitmap);
				return bitmap;
			}

			// Token: 0x06001E5A RID: 7770 RVA: 0x00071BBC File Offset: 0x0006FDBC
			private void RecreateHandle()
			{
				if (this.handleCreated)
				{
					this.DestroyHandle();
					this.handleCreated = true;
					this.owner.OnRecreateHandle();
				}
			}

			// Token: 0x06001E5B RID: 7771 RVA: 0x00071BE4 File Offset: 0x0006FDE4
			private unsafe void ReduceColorDepth(Bitmap bitmap)
			{
				if (this.colorDepth < ColorDepth.Depth32Bit)
				{
					BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), 3, 2498570);
					try
					{
						byte* ptr = (byte*)(void*)bitmapData.Scan0;
						int height = bitmapData.Height;
						int num = bitmapData.Width << 2;
						int stride = bitmapData.Stride;
						if (this.colorDepth < ColorDepth.Depth16Bit)
						{
							Color[] entries = ((this.colorDepth >= ColorDepth.Depth8Bit) ? ImageList.ImageCollection.IndexedColorDepths.Palette8Bit : ImageList.ImageCollection.IndexedColorDepths.Palette4Bit).Entries;
							for (int i = 0; i < height; i++)
							{
								byte* ptr2 = ptr + num;
								for (byte* ptr3 = ptr; ptr3 < ptr2; ptr3 += 4)
								{
									int num2;
									*(int*)ptr3 = ((((num2 = *(int*)ptr3) & -16777216) != 0) ? ImageList.ImageCollection.IndexedColorDepths.GetNearestColor(entries, num2 | -16777216) : 0);
								}
								ptr += stride;
							}
						}
						else if (this.colorDepth < ColorDepth.Depth24Bit)
						{
							for (int i = 0; i < height; i++)
							{
								byte* ptr2 = ptr + num;
								for (byte* ptr3 = ptr; ptr3 < ptr2; ptr3 += 4)
								{
									int num2;
									*(int*)ptr3 = ((((num2 = *(int*)ptr3) & -16777216) != 0) ? ((num2 & 16316664) | -16777216) : 0);
								}
								ptr += stride;
							}
						}
						else
						{
							for (int i = 0; i < height; i++)
							{
								byte* ptr2 = ptr + num;
								for (byte* ptr3 = ptr; ptr3 < ptr2; ptr3 += 4)
								{
									int num2;
									*(int*)ptr3 = ((((num2 = *(int*)ptr3) & -16777216) != 0) ? (num2 | -16777216) : 0);
								}
								ptr += stride;
							}
						}
					}
					finally
					{
						bitmap.UnlockBits(bitmapData);
					}
				}
			}

			// Token: 0x06001E5C RID: 7772 RVA: 0x00071DB4 File Offset: 0x0006FFB4
			internal void DestroyHandle()
			{
				if (this.handleCreated)
				{
					this.list = new ArrayList();
					this.count = 0;
					this.handleCreated = false;
					this.keys = new ArrayList();
				}
			}

			// Token: 0x06001E5D RID: 7773 RVA: 0x00071DE8 File Offset: 0x0006FFE8
			internal Image GetImage(int index)
			{
				if (index < 0 || index >= this.Count)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				this.CreateHandle();
				return (Image)this.list[index];
			}

			// Token: 0x06001E5E RID: 7774 RVA: 0x00071E20 File Offset: 0x00070020
			internal Image[] ToArray()
			{
				this.CreateHandle();
				Image[] array = new Image[this.list.Count];
				this.list.CopyTo(array);
				return array;
			}

			/// <summary>Adds the specified icon to the <see cref="T:System.Windows.Forms.ImageList" />.</summary>
			/// <param name="value">An <see cref="T:System.Drawing.Icon" /> to add to the list. </param>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="value" /> is null-or-value is not an <see cref="T:System.Drawing.Icon" />. </exception>
			// Token: 0x06001E5F RID: 7775 RVA: 0x00071E54 File Offset: 0x00070054
			public void Add(Icon value)
			{
				this.Add(null, value);
			}

			/// <summary>Adds the specified image to the <see cref="T:System.Windows.Forms.ImageList" />.</summary>
			/// <param name="value">A <see cref="T:System.Drawing.Bitmap" /> of the image to add to the list. </param>
			/// <exception cref="T:System.ArgumentNullException">The image being added is null. </exception>
			/// <exception cref="T:System.ArgumentException">The image being added is not a <see cref="T:System.Drawing.Bitmap" />. </exception>
			// Token: 0x06001E60 RID: 7776 RVA: 0x00071E60 File Offset: 0x00070060
			public void Add(Image value)
			{
				this.Add(null, value);
			}

			/// <summary>Adds the specified image to the <see cref="T:System.Windows.Forms.ImageList" />, using the specified color to generate the mask.</summary>
			/// <returns>The index of the newly added image, or -1 if the image cannot be added.</returns>
			/// <param name="value">A <see cref="T:System.Drawing.Bitmap" /> of the image to add to the list. </param>
			/// <param name="transparentColor">The <see cref="T:System.Drawing.Color" /> to mask this image. </param>
			/// <exception cref="T:System.ArgumentNullException">The image being added is null. </exception>
			/// <exception cref="T:System.ArgumentException">The image being added is not a <see cref="T:System.Drawing.Bitmap" />. </exception>
			// Token: 0x06001E61 RID: 7777 RVA: 0x00071E6C File Offset: 0x0007006C
			public int Add(Image value, Color transparentColor)
			{
				return this.AddItem(null, new ImageList.ImageCollection.ImageListItem(value, transparentColor));
			}

			/// <summary>Adds an icon with the specified key to the end of the collection. </summary>
			/// <param name="key">The name of the icon.</param>
			/// <param name="icon">The <see cref="T:System.Drawing.Icon" /> to add to the collection.</param>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="icon" /> is null. </exception>
			// Token: 0x06001E62 RID: 7778 RVA: 0x00071E7C File Offset: 0x0007007C
			public void Add(string key, Icon icon)
			{
				this.AddItem(key, new ImageList.ImageCollection.ImageListItem(icon));
			}

			/// <summary>Adds an image with the specified key to the end of the collection.</summary>
			/// <param name="key">The name of the image.</param>
			/// <param name="image">The <see cref="T:System.Drawing.Image" /> to add to the collection.</param>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="image" /> is null. </exception>
			// Token: 0x06001E63 RID: 7779 RVA: 0x00071E8C File Offset: 0x0007008C
			public void Add(string key, Image image)
			{
				this.AddItem(key, new ImageList.ImageCollection.ImageListItem(image));
			}

			/// <summary>Adds an array of images to the collection.</summary>
			/// <param name="images">The array of <see cref="T:System.Drawing.Image" /> objects to add to the collection.</param>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="images" /> is null.</exception>
			// Token: 0x06001E64 RID: 7780 RVA: 0x00071E9C File Offset: 0x0007009C
			public void AddRange(Image[] images)
			{
				if (images == null)
				{
					throw new ArgumentNullException("images");
				}
				for (int i = 0; i < images.Length; i++)
				{
					this.Add(images[i]);
				}
			}

			/// <summary>Adds an image strip for the specified image to the <see cref="T:System.Windows.Forms.ImageList" />.</summary>
			/// <returns>The index of the newly added image, or -1 if the image cannot be added.</returns>
			/// <param name="value">A <see cref="T:System.Drawing.Bitmap" /> with the images to add. </param>
			/// <exception cref="T:System.ArgumentException">The image being added is null.-or- The image being added is not a <see cref="T:System.Drawing.Bitmap" />. </exception>
			/// <exception cref="T:System.InvalidOperationException">The image cannot be added.-or- The width of image strip being added is 0, or the width is not equal to the existing image width.-or- The image strip height is not equal to existing image height. </exception>
			// Token: 0x06001E65 RID: 7781 RVA: 0x00071ED8 File Offset: 0x000700D8
			public int AddStrip(Image value)
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				int width;
				int width2;
				if ((width = value.Width) == 0 || width % (width2 = this.imageSize.Width) != 0)
				{
					throw new ArgumentException("Width of image strip must be a positive multiple of ImageSize.Width.", "value");
				}
				if (value.Height != this.imageSize.Height)
				{
					throw new ArgumentException("Height of image strip must be equal to ImageSize.Height.", "value");
				}
				return this.AddItem(null, new ImageList.ImageCollection.ImageListItem(value, width / width2));
			}

			/// <summary>Removes all the images and masks from the <see cref="T:System.Windows.Forms.ImageList" />.</summary>
			// Token: 0x06001E66 RID: 7782 RVA: 0x00071F60 File Offset: 0x00070160
			public void Clear()
			{
				this.list.Clear();
				if (this.handleCreated)
				{
					this.count = 0;
				}
				this.keys.Clear();
			}

			/// <summary>Not supported. The <see cref="M:System.Collections.IList.Contains(System.Object)" /> method indicates whether a specified object is contained in the list.</summary>
			/// <returns>true if the image is found in the list; otherwise, false.</returns>
			/// <param name="image">The <see cref="T:System.Drawing.Image" /> to find in the list. </param>
			/// <exception cref="T:System.NotSupportedException">This method is not supported. </exception>
			// Token: 0x06001E67 RID: 7783 RVA: 0x00071F98 File Offset: 0x00070198
			[EditorBrowsable(1)]
			public bool Contains(Image image)
			{
				throw new NotSupportedException();
			}

			/// <summary>Determines if the collection contains an image with the specified key.</summary>
			/// <returns>true to indicate an image with the specified key is contained in the collection; otherwise, false. </returns>
			/// <param name="key">The key of the image to search for.</param>
			// Token: 0x06001E68 RID: 7784 RVA: 0x00071FA0 File Offset: 0x000701A0
			public bool ContainsKey(string key)
			{
				return this.IndexOfKey(key) != -1;
			}

			/// <summary>Returns an enumerator that can be used to iterate through the item collection.</summary>
			/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that represents the item collection.</returns>
			// Token: 0x06001E69 RID: 7785 RVA: 0x00071FB0 File Offset: 0x000701B0
			public IEnumerator GetEnumerator()
			{
				Image[] array = new Image[this.Count];
				if (array.Length != 0)
				{
					this.CreateHandle();
					for (int i = 0; i < array.Length; i++)
					{
						array[i] = (Image)((Image)this.list[i]).Clone();
					}
				}
				return array.GetEnumerator();
			}

			/// <summary>Not supported. The <see cref="M:System.Collections.IList.IndexOf(System.Object)" /> method returns the index of a specified object in the list.</summary>
			/// <returns>The index of the image in the list.</returns>
			/// <param name="image">The <see cref="T:System.Drawing.Image" /> to find in the list. </param>
			/// <exception cref="T:System.NotSupportedException">This method is not supported. </exception>
			// Token: 0x06001E6A RID: 7786 RVA: 0x00072010 File Offset: 0x00070210
			[EditorBrowsable(1)]
			public int IndexOf(Image image)
			{
				throw new NotSupportedException();
			}

			/// <summary>Determines the index of the first occurrence of an image with the specified key in the collection.</summary>
			/// <returns>The zero-based index of the first occurrence of an image with the specified key in the collection, if found; otherwise, -1.</returns>
			/// <param name="key">The key of the image to retrieve the index for.</param>
			// Token: 0x06001E6B RID: 7787 RVA: 0x00072018 File Offset: 0x00070218
			public int IndexOfKey(string key)
			{
				if (key != null && key.Length != 0)
				{
					if (this.lastKeyIndex >= 0 && this.lastKeyIndex < this.Count && ImageList.ImageCollection.CompareKeys((string)this.keys[this.lastKeyIndex], key))
					{
						return this.lastKeyIndex;
					}
					for (int i = 0; i < this.Count; i++)
					{
						if (ImageList.ImageCollection.CompareKeys((string)this.keys[i], key))
						{
							return this.lastKeyIndex = i;
						}
					}
				}
				return this.lastKeyIndex = -1;
			}

			/// <summary>Not supported. The <see cref="M:System.Collections.IList.Remove(System.Object)" /> method removes a specified object from the list.</summary>
			/// <param name="image">The <see cref="T:System.Drawing.Image" /> to remove from the list. </param>
			/// <exception cref="T:System.NotSupportedException">This method is not supported. </exception>
			// Token: 0x06001E6C RID: 7788 RVA: 0x000720C4 File Offset: 0x000702C4
			[EditorBrowsable(1)]
			public void Remove(Image image)
			{
				throw new NotSupportedException();
			}

			/// <summary>Removes an image from the list.</summary>
			/// <param name="index">The index of the image to remove. </param>
			/// <exception cref="T:System.InvalidOperationException">The image cannot be removed. </exception>
			/// <exception cref="T:System.ArgumentOutOfRangeException">The index value was less than 0.-or- The index value is greater than or equal to the <see cref="P:System.Windows.Forms.ImageList.ImageCollection.Count" /> of images. </exception>
			// Token: 0x06001E6D RID: 7789 RVA: 0x000720CC File Offset: 0x000702CC
			public void RemoveAt(int index)
			{
				if (index < 0 || index >= this.Count)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				this.CreateHandle();
				this.list.RemoveAt(index);
				this.keys.RemoveAt(index);
				if (this.Changed != null)
				{
					this.Changed.Invoke(this, EventArgs.Empty);
				}
			}

			/// <summary>Removes the image with the specified key from the collection.</summary>
			/// <param name="key">The key of the image to remove from the collection.</param>
			// Token: 0x06001E6E RID: 7790 RVA: 0x00072134 File Offset: 0x00070334
			public void RemoveByKey(string key)
			{
				int num;
				if ((num = this.IndexOfKey(key)) != -1)
				{
					this.RemoveAt(num);
				}
			}

			/// <summary>Sets the key for an image in the collection.</summary>
			/// <param name="index">The zero-based index of an image in the collection.</param>
			/// <param name="name">The name of the image to be set as the image key.</param>
			/// <exception cref="T:System.IndexOutOfRangeException">The specified index is less than 0 or greater than or equal to <see cref="P:System.Windows.Forms.ImageList.ImageCollection.Count" />.</exception>
			// Token: 0x06001E6F RID: 7791 RVA: 0x00072158 File Offset: 0x00070358
			public void SetKeyName(int index, string name)
			{
				if (index < 0 || index >= this.Count)
				{
					throw new IndexOutOfRangeException();
				}
				this.keys[index] = name;
			}

			// Token: 0x04000FD4 RID: 4052
			private const int AlphaMask = -16777216;

			// Token: 0x04000FD5 RID: 4053
			private ColorDepth colorDepth = ColorDepth.Depth8Bit;

			// Token: 0x04000FD6 RID: 4054
			private Size imageSize = ImageList.DefaultImageSize;

			// Token: 0x04000FD7 RID: 4055
			private Color transparentColor = ImageList.DefaultTransparentColor;

			// Token: 0x04000FD8 RID: 4056
			private ArrayList list = new ArrayList();

			// Token: 0x04000FD9 RID: 4057
			private ArrayList keys = new ArrayList();

			// Token: 0x04000FDA RID: 4058
			private int count;

			// Token: 0x04000FDB RID: 4059
			private bool handleCreated;

			// Token: 0x04000FDC RID: 4060
			private int lastKeyIndex = -1;

			// Token: 0x04000FDD RID: 4061
			private readonly ImageList owner;

			// Token: 0x020001DA RID: 474
			private static class IndexedColorDepths
			{
				// Token: 0x06001E70 RID: 7792 RVA: 0x0007218C File Offset: 0x0007038C
				static IndexedColorDepths()
				{
					Bitmap bitmap = new Bitmap(1, 1, 197634);
					ImageList.ImageCollection.IndexedColorDepths.Palette4Bit = bitmap.Palette;
					bitmap.Dispose();
					bitmap = new Bitmap(1, 1, 198659);
					ImageList.ImageCollection.IndexedColorDepths.Palette8Bit = bitmap.Palette;
					bitmap.Dispose();
					ImageList.ImageCollection.IndexedColorDepths.squares = new int[511];
					for (int i = 0; i < 256; i++)
					{
						ImageList.ImageCollection.IndexedColorDepths.squares[255 + i] = (ImageList.ImageCollection.IndexedColorDepths.squares[255 - i] = i * i);
					}
				}

				// Token: 0x06001E71 RID: 7793 RVA: 0x0007221C File Offset: 0x0007041C
				internal static int GetNearestColor(Color[] palette, int color)
				{
					int num = palette.Length;
					for (int i = 0; i < num; i++)
					{
						if (palette[i].ToArgb() == color)
						{
							return color;
						}
					}
					int num2 = (int)(((uint)color >> 16) & 255U);
					int num3 = (int)(((uint)color >> 8) & 255U);
					int num4 = color & 255;
					int num5 = -16777216;
					int num6 = int.MaxValue;
					for (int i = 0; i < num; i++)
					{
						int num7;
						if ((num7 = ImageList.ImageCollection.IndexedColorDepths.squares[(int)(255 + palette[i].R) - num2] + ImageList.ImageCollection.IndexedColorDepths.squares[(int)(255 + palette[i].G) - num3] + ImageList.ImageCollection.IndexedColorDepths.squares[(int)(255 + palette[i].B) - num4]) < num6)
						{
							num5 = palette[i].ToArgb();
							num6 = num7;
						}
					}
					return num5;
				}

				// Token: 0x04000FDF RID: 4063
				internal static readonly ColorPalette Palette4Bit;

				// Token: 0x04000FE0 RID: 4064
				internal static readonly ColorPalette Palette8Bit;

				// Token: 0x04000FE1 RID: 4065
				private static readonly int[] squares;
			}

			// Token: 0x020001DB RID: 475
			[Flags]
			private enum ItemFlags
			{
				// Token: 0x04000FE3 RID: 4067
				None = 0,
				// Token: 0x04000FE4 RID: 4068
				UseTransparentColor = 1,
				// Token: 0x04000FE5 RID: 4069
				ImageStrip = 2
			}

			// Token: 0x020001DC RID: 476
			private sealed class ImageListItem
			{
				// Token: 0x06001E72 RID: 7794 RVA: 0x00072300 File Offset: 0x00070500
				internal ImageListItem(Icon value)
				{
					if (value == null)
					{
						throw new ArgumentNullException("value");
					}
					this.Image = (Icon)value.Clone();
				}

				// Token: 0x06001E73 RID: 7795 RVA: 0x00072334 File Offset: 0x00070534
				internal ImageListItem(Image value)
				{
					if (value == null)
					{
						throw new ArgumentNullException("value");
					}
					if (!(value is Bitmap))
					{
						throw new ArgumentException("Image must be a Bitmap.");
					}
					this.Image = value;
				}

				// Token: 0x06001E74 RID: 7796 RVA: 0x00072374 File Offset: 0x00070574
				internal ImageListItem(Image value, Color transparentColor)
					: this(value)
				{
					this.Flags = ImageList.ImageCollection.ItemFlags.UseTransparentColor;
					this.TransparentColor = transparentColor;
				}

				// Token: 0x06001E75 RID: 7797 RVA: 0x0007238C File Offset: 0x0007058C
				internal ImageListItem(Image value, int imageCount)
					: this(value)
				{
					this.Flags = ImageList.ImageCollection.ItemFlags.ImageStrip;
					this.ImageCount = imageCount;
				}

				// Token: 0x04000FE6 RID: 4070
				internal readonly object Image;

				// Token: 0x04000FE7 RID: 4071
				internal readonly ImageList.ImageCollection.ItemFlags Flags;

				// Token: 0x04000FE8 RID: 4072
				internal readonly Color TransparentColor;

				// Token: 0x04000FE9 RID: 4073
				internal readonly int ImageCount = 1;
			}
		}
	}
}
