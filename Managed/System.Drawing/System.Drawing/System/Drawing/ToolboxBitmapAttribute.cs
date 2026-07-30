using System;
using System.IO;
using System.Reflection;

namespace System.Drawing
{
	/// <summary>Allows you to specify an icon to represent a control in a container, such as the Microsoft Visual Studio Form Designer.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200008D RID: 141
	[AttributeUsage(AttributeTargets.Class)]
	public class ToolboxBitmapAttribute : Attribute
	{
		// Token: 0x06000780 RID: 1920 RVA: 0x00002064 File Offset: 0x00000264
		private ToolboxBitmapAttribute()
		{
		}

		/// <summary>Initializes a new <see cref="T:System.Drawing.ToolboxBitmapAttribute" /> object with an image from a specified file.</summary>
		/// <param name="imageFile">The name of a file that contains a 16 by 16 bitmap. </param>
		// Token: 0x06000781 RID: 1921 RVA: 0x00002064 File Offset: 0x00000264
		public ToolboxBitmapAttribute(string imageFile)
		{
		}

		/// <summary>Initializes a new <see cref="T:System.Drawing.ToolboxBitmapAttribute" /> object based on a 16 x 16 bitmap that is embedded as a resource in a specified assembly.</summary>
		/// <param name="t">A <see cref="T:System.Type" /> whose defining assembly is searched for the bitmap resource. </param>
		// Token: 0x06000782 RID: 1922 RVA: 0x00015018 File Offset: 0x00013218
		public ToolboxBitmapAttribute(Type t)
		{
			this.smallImage = ToolboxBitmapAttribute.GetImageFromResource(t, null, false);
		}

		/// <summary>Initializes a new <see cref="T:System.Drawing.ToolboxBitmapAttribute" /> object based on a 16 by 16 bitmap that is embedded as a resource in a specified assembly.</summary>
		/// <param name="t">A <see cref="T:System.Type" /> whose defining assembly is searched for the bitmap resource. </param>
		/// <param name="name">The name of the embedded bitmap resource. </param>
		// Token: 0x06000783 RID: 1923 RVA: 0x0001502E File Offset: 0x0001322E
		public ToolboxBitmapAttribute(Type t, string name)
		{
			this.smallImage = ToolboxBitmapAttribute.GetImageFromResource(t, name, false);
		}

		/// <summary>Indicates whether the specified object is a <see cref="T:System.Drawing.ToolboxBitmapAttribute" /> object and is identical to this <see cref="T:System.Drawing.ToolboxBitmapAttribute" /> object.</summary>
		/// <returns>This method returns true if <paramref name="value" /> is both a <see cref="T:System.Drawing.ToolboxBitmapAttribute" /> object and is identical to this <see cref="T:System.Drawing.ToolboxBitmapAttribute" /> object.</returns>
		/// <param name="value">The <see cref="T:System.Object" /> to test. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000784 RID: 1924 RVA: 0x00015044 File Offset: 0x00013244
		public override bool Equals(object value)
		{
			return value is ToolboxBitmapAttribute && (value == this || ((ToolboxBitmapAttribute)value).smallImage == this.smallImage);
		}

		/// <summary>Gets a hash code for this <see cref="T:System.Drawing.ToolboxBitmapAttribute" /> object.</summary>
		/// <returns>The hash code for this <see cref="T:System.Drawing.ToolboxBitmapAttribute" /> object.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000785 RID: 1925 RVA: 0x00015069 File Offset: 0x00013269
		public override int GetHashCode()
		{
			return this.smallImage.GetHashCode() ^ this.bigImage.GetHashCode();
		}

		/// <summary>Gets the small <see cref="T:System.Drawing.Image" /> associated with this <see cref="T:System.Drawing.ToolboxBitmapAttribute" /> object.</summary>
		/// <returns>The small <see cref="T:System.Drawing.Image" /> associated with this <see cref="T:System.Drawing.ToolboxBitmapAttribute" /> object.</returns>
		/// <param name="component">If this <see cref="T:System.Drawing.ToolboxBitmapAttribute" /> object does not already have a small image, this method searches for a bitmap resource in the assembly that defines the type of the object specified by the component parameter. For example, if you pass an object of type ControlA to the component parameter, then this method searches the assembly that defines ControlA. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000786 RID: 1926 RVA: 0x00015082 File Offset: 0x00013282
		public Image GetImage(object component)
		{
			return this.GetImage(component.GetType(), null, false);
		}

		/// <summary>Gets the small or large <see cref="T:System.Drawing.Image" /> associated with this <see cref="T:System.Drawing.ToolboxBitmapAttribute" /> object.</summary>
		/// <returns>An <see cref="T:System.Drawing.Image" /> object associated with this <see cref="T:System.Drawing.ToolboxBitmapAttribute" /> object.</returns>
		/// <param name="component">If this <see cref="T:System.Drawing.ToolboxBitmapAttribute" /> object does not already have a small image, this method searches for a bitmap resource in the assembly that defines the type of the object specified by the component parameter. For example, if you pass an object of type ControlA to the component parameter, then this method searches the assembly that defines ControlA. </param>
		/// <param name="large">Specifies whether this method returns a large image (true) or a small image (false). The small image is 16 by 16, and the large image is 32 by 32. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000787 RID: 1927 RVA: 0x00015092 File Offset: 0x00013292
		public Image GetImage(object component, bool large)
		{
			return this.GetImage(component.GetType(), null, large);
		}

		/// <summary>Gets the small <see cref="T:System.Drawing.Image" /> associated with this <see cref="T:System.Drawing.ToolboxBitmapAttribute" /> object.</summary>
		/// <returns>The small <see cref="T:System.Drawing.Image" /> associated with this <see cref="T:System.Drawing.ToolboxBitmapAttribute" /> object.</returns>
		/// <param name="type">If this <see cref="T:System.Drawing.ToolboxBitmapAttribute" /> object does not already have a small image, this method searches for a bitmap resource in the assembly that defines the type specified by the type parameter. For example, if you pass typeof(ControlA) to the type parameter, then this method searches the assembly that defines ControlA. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000788 RID: 1928 RVA: 0x000150A2 File Offset: 0x000132A2
		public Image GetImage(Type type)
		{
			return this.GetImage(type, null, false);
		}

		/// <summary>Gets the small or large <see cref="T:System.Drawing.Image" /> associated with this <see cref="T:System.Drawing.ToolboxBitmapAttribute" /> object.</summary>
		/// <returns>An <see cref="T:System.Drawing.Image" /> associated with this <see cref="T:System.Drawing.ToolboxBitmapAttribute" /> object.</returns>
		/// <param name="type">If this <see cref="T:System.Drawing.ToolboxBitmapAttribute" /> object does not already have a small image, this method searches for a bitmap resource in the assembly that defines the type specified by the component type. For example, if you pass typeof(ControlA) to the type parameter, then this method searches the assembly that defines ControlA. </param>
		/// <param name="large">Specifies whether this method returns a large image (true) or a small image (false). The small image is 16 by 16, and the large image is 32 by 32. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000789 RID: 1929 RVA: 0x000150AD File Offset: 0x000132AD
		public Image GetImage(Type type, bool large)
		{
			return this.GetImage(type, null, large);
		}

		/// <summary>Gets the small or large <see cref="T:System.Drawing.Image" /> associated with this <see cref="T:System.Drawing.ToolboxBitmapAttribute" /> object.</summary>
		/// <returns>An <see cref="T:System.Drawing.Image" /> associated with this <see cref="T:System.Drawing.ToolboxBitmapAttribute" /> object.</returns>
		/// <param name="type">If this <see cref="T:System.Drawing.ToolboxBitmapAttribute" /> object does not already have a small image, this method searches for an embedded bitmap resource in the assembly that defines the type specified by the component type. For example, if you pass typeof(ControlA) to the type parameter, then this method searches the assembly that defines ControlA. </param>
		/// <param name="imgName">The name of the embedded bitmap resource. </param>
		/// <param name="large">Specifies whether this method returns a large image (true) or a small image (false). The small image is 16 by 16, and the large image is 32 by 32. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600078A RID: 1930 RVA: 0x000150B8 File Offset: 0x000132B8
		public Image GetImage(Type type, string imgName, bool large)
		{
			if (this.smallImage == null)
			{
				this.smallImage = ToolboxBitmapAttribute.GetImageFromResource(type, imgName, false);
			}
			if (large)
			{
				if (this.bigImage == null)
				{
					this.bigImage = new Bitmap(this.smallImage, 32, 32);
				}
				return this.bigImage;
			}
			return this.smallImage;
		}

		/// <summary>Returns an <see cref="T:System.Drawing.Image" /> object based on a bitmap resource that is embedded in an assembly.</summary>
		/// <returns>An <see cref="T:System.Drawing.Image" /> object based on the retrieved bitmap.</returns>
		/// <param name="t">This method searches for an embedded bitmap resource in the assembly that defines the type specified by the t parameter. For example, if you pass typeof(ControlA) to the t parameter, then this method searches the assembly that defines ControlA. </param>
		/// <param name="imageName">The name of the embedded bitmap resource. </param>
		/// <param name="large">Specifies whether this method returns a large image (true)or a small image (false). The small image is 16 by 16, and the large image is 32 x 32. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600078B RID: 1931 RVA: 0x00015108 File Offset: 0x00013308
		public static Image GetImageFromResource(Type t, string imageName, bool large)
		{
			if (imageName == null)
			{
				imageName = t.Name + ".bmp";
			}
			Image image;
			try
			{
				Bitmap bitmap;
				using (Stream manifestResourceStream = t.GetTypeInfo().Assembly.GetManifestResourceStream(t.Namespace + "." + imageName))
				{
					if (manifestResourceStream == null)
					{
						return null;
					}
					bitmap = new Bitmap(manifestResourceStream, false);
				}
				if (large)
				{
					image = new Bitmap(bitmap, 32, 32);
				}
				else
				{
					image = bitmap;
				}
			}
			catch
			{
				image = null;
			}
			return image;
		}

		// Token: 0x0400057E RID: 1406
		private Image smallImage;

		// Token: 0x0400057F RID: 1407
		private Image bigImage;

		/// <summary>A <see cref="T:System.Drawing.ToolboxBitmapAttribute" /> object that has its small image and its large image set to null.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x04000580 RID: 1408
		public static readonly ToolboxBitmapAttribute Default = new ToolboxBitmapAttribute();
	}
}
