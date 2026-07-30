using System;
using System.ComponentModel;
using System.Drawing;

namespace System.Windows.Forms.Design
{
	/// <summary>Provides a base class for property tabs.</summary>
	// Token: 0x02000017 RID: 23
	public abstract class PropertyTab : IExtenderProvider
	{
		/// <summary>Allows a <see cref="T:System.Windows.Forms.Design.PropertyTab" /> to attempt to free resources and perform other cleanup operations before the <see cref="T:System.Windows.Forms.Design.PropertyTab" /> is reclaimed by garbage collection.</summary>
		// Token: 0x060000C0 RID: 192 RVA: 0x00004318 File Offset: 0x00002518
		~PropertyTab()
		{
			this.Dispose(false);
		}

		/// <summary>Gets the bitmap that is displayed for the <see cref="T:System.Windows.Forms.Design.PropertyTab" />.</summary>
		/// <returns>The <see cref="T:System.Drawing.Bitmap" /> to display for the <see cref="T:System.Windows.Forms.Design.PropertyTab" />.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x00004354 File Offset: 0x00002554
		public virtual Bitmap Bitmap
		{
			get
			{
				if (this.bitmap == null)
				{
					Type type = base.GetType();
					this.bitmap = new Bitmap(type, type.Name + ".bmp");
				}
				return this.bitmap;
			}
		}

		/// <summary>Gets or sets the array of components the property tab is associated with.</summary>
		/// <returns>The array of components the property tab is associated with.</returns>
		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x00004398 File Offset: 0x00002598
		// (set) Token: 0x060000C3 RID: 195 RVA: 0x000043A0 File Offset: 0x000025A0
		public virtual object[] Components
		{
			get
			{
				return this.components;
			}
			set
			{
				this.components = value;
			}
		}

		/// <summary>Gets the Help keyword that is to be associated with this tab.</summary>
		/// <returns>The Help keyword to be associated with this tab.</returns>
		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x000043AC File Offset: 0x000025AC
		public virtual string HelpKeyword
		{
			get
			{
				return this.TabName;
			}
		}

		/// <summary>Gets the name for the property tab.</summary>
		/// <returns>The name for the property tab.</returns>
		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000C5 RID: 197
		public abstract string TabName { get; }

		/// <summary>Gets a value indicating whether this <see cref="T:System.Windows.Forms.Design.PropertyTab" /> can display properties for the specified component.</summary>
		/// <returns>true if the object can be extended; otherwise, false.</returns>
		/// <param name="extendee">The object to test. </param>
		// Token: 0x060000C6 RID: 198 RVA: 0x000043B4 File Offset: 0x000025B4
		public virtual bool CanExtend(object extendee)
		{
			return true;
		}

		/// <summary>Releases all the resources used by the <see cref="T:System.Windows.Forms.Design.PropertyTab" />.</summary>
		// Token: 0x060000C7 RID: 199 RVA: 0x000043B8 File Offset: 0x000025B8
		public virtual void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.Design.PropertyTab" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x060000C8 RID: 200 RVA: 0x000043C8 File Offset: 0x000025C8
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && this.bitmap != null)
			{
				this.bitmap.Dispose();
				this.bitmap = null;
			}
		}

		/// <summary>Gets the default property of the specified component.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptor" /> that represents the default property.</returns>
		/// <param name="component">The component to retrieve the default property of. </param>
		// Token: 0x060000C9 RID: 201 RVA: 0x000043F0 File Offset: 0x000025F0
		public virtual PropertyDescriptor GetDefaultProperty(object component)
		{
			return TypeDescriptor.GetDefaultProperty(component);
		}

		/// <summary>Gets the properties of the specified component.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> that contains the properties of the component.</returns>
		/// <param name="component">The component to retrieve the properties of. </param>
		// Token: 0x060000CA RID: 202 RVA: 0x000043F8 File Offset: 0x000025F8
		public virtual PropertyDescriptorCollection GetProperties(object component)
		{
			return this.GetProperties(component, null);
		}

		/// <summary>Gets the properties of the specified component that match the specified attributes.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> that contains the properties.</returns>
		/// <param name="component">The component to retrieve properties from. </param>
		/// <param name="attributes">An array of type <see cref="T:System.Attribute" /> that indicates the attributes of the properties to retrieve. </param>
		// Token: 0x060000CB RID: 203
		public abstract PropertyDescriptorCollection GetProperties(object component, Attribute[] attributes);

		/// <summary>Gets the properties of the specified component that match the specified attributes and context.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> that contains the properties matching the specified context and attributes.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that indicates the context to retrieve properties from. </param>
		/// <param name="component">The component to retrieve properties from. </param>
		/// <param name="attributes">An array of type <see cref="T:System.Attribute" /> that indicates the attributes of the properties to retrieve. </param>
		// Token: 0x060000CC RID: 204 RVA: 0x00004404 File Offset: 0x00002604
		public virtual PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object component, Attribute[] attributes)
		{
			return this.GetProperties(component, attributes);
		}

		// Token: 0x0400004D RID: 77
		private Bitmap bitmap;

		// Token: 0x0400004E RID: 78
		private object[] components;
	}
}
