using System;
using System.Runtime.InteropServices;

namespace System.Drawing.Imaging
{
	/// <summary>Specifies the attributes of a bitmap image. The <see cref="T:System.Drawing.Imaging.BitmapData" /> class is used by the <see cref="Overload:System.Drawing.Bitmap.LockBits" /> and <see cref="M:System.Drawing.Bitmap.UnlockBits(System.Drawing.Imaging.BitmapData)" /> methods of the <see cref="T:System.Drawing.Bitmap" /> class. Not inheritable. </summary>
	// Token: 0x02000113 RID: 275
	[StructLayout(LayoutKind.Sequential)]
	public sealed class BitmapData
	{
		/// <summary>Gets or sets the pixel height of the <see cref="T:System.Drawing.Bitmap" /> object. Also sometimes referred to as the number of scan lines.</summary>
		/// <returns>The pixel height of the <see cref="T:System.Drawing.Bitmap" /> object.</returns>
		// Token: 0x17000378 RID: 888
		// (get) Token: 0x06000CC8 RID: 3272 RVA: 0x0001C9EF File Offset: 0x0001ABEF
		// (set) Token: 0x06000CC9 RID: 3273 RVA: 0x0001C9F7 File Offset: 0x0001ABF7
		public int Height
		{
			get
			{
				return this.height;
			}
			set
			{
				this.height = value;
			}
		}

		/// <summary>Gets or sets the pixel width of the <see cref="T:System.Drawing.Bitmap" /> object. This can also be thought of as the number of pixels in one scan line.</summary>
		/// <returns>The pixel width of the <see cref="T:System.Drawing.Bitmap" /> object.</returns>
		// Token: 0x17000379 RID: 889
		// (get) Token: 0x06000CCA RID: 3274 RVA: 0x0001CA00 File Offset: 0x0001AC00
		// (set) Token: 0x06000CCB RID: 3275 RVA: 0x0001CA08 File Offset: 0x0001AC08
		public int Width
		{
			get
			{
				return this.width;
			}
			set
			{
				this.width = value;
			}
		}

		/// <summary>Gets or sets the format of the pixel information in the <see cref="T:System.Drawing.Bitmap" /> object that returned this <see cref="T:System.Drawing.Imaging.BitmapData" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Imaging.PixelFormat" /> that specifies the format of the pixel information in the associated <see cref="T:System.Drawing.Bitmap" /> object.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x1700037A RID: 890
		// (get) Token: 0x06000CCC RID: 3276 RVA: 0x0001CA11 File Offset: 0x0001AC11
		// (set) Token: 0x06000CCD RID: 3277 RVA: 0x0001CA19 File Offset: 0x0001AC19
		public PixelFormat PixelFormat
		{
			get
			{
				return this.pixel_format;
			}
			set
			{
				this.pixel_format = value;
			}
		}

		/// <summary>Reserved. Do not use.</summary>
		/// <returns>Reserved. Do not use.</returns>
		// Token: 0x1700037B RID: 891
		// (get) Token: 0x06000CCE RID: 3278 RVA: 0x0001CA22 File Offset: 0x0001AC22
		// (set) Token: 0x06000CCF RID: 3279 RVA: 0x0001CA2A File Offset: 0x0001AC2A
		public int Reserved
		{
			get
			{
				return this.reserved;
			}
			set
			{
				this.reserved = value;
			}
		}

		/// <summary>Gets or sets the address of the first pixel data in the bitmap. This can also be thought of as the first scan line in the bitmap.</summary>
		/// <returns>The address of the first pixel data in the bitmap.</returns>
		// Token: 0x1700037C RID: 892
		// (get) Token: 0x06000CD0 RID: 3280 RVA: 0x0001CA33 File Offset: 0x0001AC33
		// (set) Token: 0x06000CD1 RID: 3281 RVA: 0x0001CA3B File Offset: 0x0001AC3B
		public IntPtr Scan0
		{
			get
			{
				return this.scan0;
			}
			set
			{
				this.scan0 = value;
			}
		}

		/// <summary>Gets or sets the stride width (also called scan width) of the <see cref="T:System.Drawing.Bitmap" /> object.</summary>
		/// <returns>The stride width, in bytes, of the <see cref="T:System.Drawing.Bitmap" /> object.</returns>
		// Token: 0x1700037D RID: 893
		// (get) Token: 0x06000CD2 RID: 3282 RVA: 0x0001CA44 File Offset: 0x0001AC44
		// (set) Token: 0x06000CD3 RID: 3283 RVA: 0x0001CA4C File Offset: 0x0001AC4C
		public int Stride
		{
			get
			{
				return this.stride;
			}
			set
			{
				this.stride = value;
			}
		}

		// Token: 0x04000A2D RID: 2605
		private int width;

		// Token: 0x04000A2E RID: 2606
		private int height;

		// Token: 0x04000A2F RID: 2607
		private int stride;

		// Token: 0x04000A30 RID: 2608
		private PixelFormat pixel_format;

		// Token: 0x04000A31 RID: 2609
		private IntPtr scan0;

		// Token: 0x04000A32 RID: 2610
		private int reserved;

		// Token: 0x04000A33 RID: 2611
		private IntPtr palette;

		// Token: 0x04000A34 RID: 2612
		private int property_count;

		// Token: 0x04000A35 RID: 2613
		private IntPtr property;

		// Token: 0x04000A36 RID: 2614
		private float dpi_horz;

		// Token: 0x04000A37 RID: 2615
		private float dpi_vert;

		// Token: 0x04000A38 RID: 2616
		private int image_flags;

		// Token: 0x04000A39 RID: 2617
		private int left;

		// Token: 0x04000A3A RID: 2618
		private int top;

		// Token: 0x04000A3B RID: 2619
		private int x;

		// Token: 0x04000A3C RID: 2620
		private int y;

		// Token: 0x04000A3D RID: 2621
		private int transparent;
	}
}
