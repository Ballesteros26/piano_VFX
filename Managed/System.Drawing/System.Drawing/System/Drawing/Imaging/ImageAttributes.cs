using System;
using System.Drawing.Drawing2D;
using System.IO;
using System.Runtime.InteropServices;

namespace System.Drawing.Imaging
{
	/// <summary>Contains information about how bitmap and metafile colors are manipulated during rendering. </summary>
	// Token: 0x02000103 RID: 259
	[StructLayout(LayoutKind.Sequential)]
	public sealed class ImageAttributes : ICloneable, IDisposable
	{
		// Token: 0x06000C53 RID: 3155 RVA: 0x0001BCB7 File Offset: 0x00019EB7
		internal void SetNativeImageAttributes(IntPtr handle)
		{
			if (handle == IntPtr.Zero)
			{
				throw new ArgumentNullException("handle");
			}
			this.nativeImageAttributes = handle;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.ImageAttributes" /> class.</summary>
		// Token: 0x06000C54 RID: 3156 RVA: 0x0001BCD8 File Offset: 0x00019ED8
		public ImageAttributes()
		{
			IntPtr zero = IntPtr.Zero;
			int num = GDIPlus.GdipCreateImageAttributes(out zero);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			this.SetNativeImageAttributes(zero);
		}

		// Token: 0x06000C55 RID: 3157 RVA: 0x0001BD0A File Offset: 0x00019F0A
		internal ImageAttributes(IntPtr newNativeImageAttributes)
		{
			this.SetNativeImageAttributes(newNativeImageAttributes);
		}

		/// <summary>Releases all resources used by this <see cref="T:System.Drawing.Imaging.ImageAttributes" /> object.</summary>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000C56 RID: 3158 RVA: 0x0001BD19 File Offset: 0x00019F19
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000C57 RID: 3159 RVA: 0x0001BD28 File Offset: 0x00019F28
		private void Dispose(bool disposing)
		{
			if (this.nativeImageAttributes != IntPtr.Zero)
			{
				try
				{
					GDIPlus.GdipDisposeImageAttributes(new HandleRef(this, this.nativeImageAttributes));
				}
				catch (Exception ex)
				{
					if (ClientUtils.IsSecurityOrCriticalException(ex))
					{
						throw;
					}
				}
				finally
				{
					this.nativeImageAttributes = IntPtr.Zero;
				}
			}
		}

		// Token: 0x06000C58 RID: 3160 RVA: 0x0001BD90 File Offset: 0x00019F90
		~ImageAttributes()
		{
			this.Dispose(false);
		}

		/// <summary>Creates an exact copy of this <see cref="T:System.Drawing.Imaging.ImageAttributes" /> object.</summary>
		/// <returns>The <see cref="T:System.Drawing.Imaging.ImageAttributes" /> object this class creates, cast as an object.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000C59 RID: 3161 RVA: 0x0001BDC0 File Offset: 0x00019FC0
		public object Clone()
		{
			IntPtr zero = IntPtr.Zero;
			int num = GDIPlus.GdipCloneImageAttributes(new HandleRef(this, this.nativeImageAttributes), out zero);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			return new ImageAttributes(zero);
		}

		/// <summary>Sets the color-adjustment matrix for the default category.</summary>
		/// <param name="newColorMatrix">The color-adjustment matrix. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000C5A RID: 3162 RVA: 0x0001BDF7 File Offset: 0x00019FF7
		public void SetColorMatrix(ColorMatrix newColorMatrix)
		{
			this.SetColorMatrix(newColorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Default);
		}

		/// <summary>Sets the color-adjustment matrix for the default category.</summary>
		/// <param name="newColorMatrix">The color-adjustment matrix. </param>
		/// <param name="flags">An element of <see cref="T:System.Drawing.Imaging.ColorMatrixFlag" /> that specifies the type of image and color that will be affected by the color-adjustment matrix. </param>
		// Token: 0x06000C5B RID: 3163 RVA: 0x0001BE02 File Offset: 0x0001A002
		public void SetColorMatrix(ColorMatrix newColorMatrix, ColorMatrixFlag flags)
		{
			this.SetColorMatrix(newColorMatrix, flags, ColorAdjustType.Default);
		}

		/// <summary>Sets the color-adjustment matrix for a specified category.</summary>
		/// <param name="newColorMatrix">The color-adjustment matrix. </param>
		/// <param name="mode">An element of <see cref="T:System.Drawing.Imaging.ColorMatrixFlag" /> that specifies the type of image and color that will be affected by the color-adjustment matrix. </param>
		/// <param name="type">An element of <see cref="T:System.Drawing.Imaging.ColorAdjustType" /> that specifies the category for which the color-adjustment matrix is set. </param>
		// Token: 0x06000C5C RID: 3164 RVA: 0x0001BE10 File Offset: 0x0001A010
		public void SetColorMatrix(ColorMatrix newColorMatrix, ColorMatrixFlag mode, ColorAdjustType type)
		{
			int num = GDIPlus.GdipSetImageAttributesColorMatrix(new HandleRef(this, this.nativeImageAttributes), type, true, newColorMatrix, null, mode);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		/// <summary>Clears the color-adjustment matrix for the default category.</summary>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000C5D RID: 3165 RVA: 0x0001BE3E File Offset: 0x0001A03E
		public void ClearColorMatrix()
		{
			this.ClearColorMatrix(ColorAdjustType.Default);
		}

		/// <summary>Clears the color-adjustment matrix for a specified category.</summary>
		/// <param name="type">An element of <see cref="T:System.Drawing.Imaging.ColorAdjustType" /> that specifies the category for which the color-adjustment matrix is cleared. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000C5E RID: 3166 RVA: 0x0001BE48 File Offset: 0x0001A048
		public void ClearColorMatrix(ColorAdjustType type)
		{
			int num = GDIPlus.GdipSetImageAttributesColorMatrix(new HandleRef(this, this.nativeImageAttributes), type, false, null, null, ColorMatrixFlag.Default);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		/// <summary>Sets the color-adjustment matrix and the grayscale-adjustment matrix for the default category.</summary>
		/// <param name="newColorMatrix">The color-adjustment matrix. </param>
		/// <param name="grayMatrix">The grayscale-adjustment matrix. </param>
		// Token: 0x06000C5F RID: 3167 RVA: 0x0001BE76 File Offset: 0x0001A076
		public void SetColorMatrices(ColorMatrix newColorMatrix, ColorMatrix grayMatrix)
		{
			this.SetColorMatrices(newColorMatrix, grayMatrix, ColorMatrixFlag.Default, ColorAdjustType.Default);
		}

		/// <summary>Sets the color-adjustment matrix and the grayscale-adjustment matrix for the default category.</summary>
		/// <param name="newColorMatrix">The color-adjustment matrix. </param>
		/// <param name="grayMatrix">The grayscale-adjustment matrix. </param>
		/// <param name="flags">An element of <see cref="T:System.Drawing.Imaging.ColorMatrixFlag" /> that specifies the type of image and color that will be affected by the color-adjustment and grayscale-adjustment matrices. </param>
		// Token: 0x06000C60 RID: 3168 RVA: 0x0001BE82 File Offset: 0x0001A082
		public void SetColorMatrices(ColorMatrix newColorMatrix, ColorMatrix grayMatrix, ColorMatrixFlag flags)
		{
			this.SetColorMatrices(newColorMatrix, grayMatrix, flags, ColorAdjustType.Default);
		}

		/// <summary>Sets the color-adjustment matrix and the grayscale-adjustment matrix for a specified category.</summary>
		/// <param name="newColorMatrix">The color-adjustment matrix. </param>
		/// <param name="grayMatrix">The grayscale-adjustment matrix. </param>
		/// <param name="mode">An element of <see cref="T:System.Drawing.Imaging.ColorMatrixFlag" /> that specifies the type of image and color that will be affected by the color-adjustment and grayscale-adjustment matrices. </param>
		/// <param name="type">An element of <see cref="T:System.Drawing.Imaging.ColorAdjustType" /> that specifies the category for which the color-adjustment and grayscale-adjustment matrices are set. </param>
		// Token: 0x06000C61 RID: 3169 RVA: 0x0001BE90 File Offset: 0x0001A090
		public void SetColorMatrices(ColorMatrix newColorMatrix, ColorMatrix grayMatrix, ColorMatrixFlag mode, ColorAdjustType type)
		{
			int num = GDIPlus.GdipSetImageAttributesColorMatrix(new HandleRef(this, this.nativeImageAttributes), type, true, newColorMatrix, grayMatrix, mode);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		/// <summary>Sets the threshold (transparency range) for the default category.</summary>
		/// <param name="threshold">A real number that specifies the threshold value. </param>
		// Token: 0x06000C62 RID: 3170 RVA: 0x0001BEBF File Offset: 0x0001A0BF
		public void SetThreshold(float threshold)
		{
			this.SetThreshold(threshold, ColorAdjustType.Default);
		}

		/// <summary>Sets the threshold (transparency range) for a specified category.</summary>
		/// <param name="threshold">A threshold value from 0.0 to 1.0 that is used as a breakpoint to sort colors that will be mapped to either a maximum or a minimum value. </param>
		/// <param name="type">An element of <see cref="T:System.Drawing.Imaging.ColorAdjustType" /> that specifies the category for which the color threshold is set. </param>
		// Token: 0x06000C63 RID: 3171 RVA: 0x0001BECC File Offset: 0x0001A0CC
		public void SetThreshold(float threshold, ColorAdjustType type)
		{
			int num = GDIPlus.GdipSetImageAttributesThreshold(new HandleRef(this, this.nativeImageAttributes), type, true, threshold);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		/// <summary>Clears the threshold value for the default category.</summary>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000C64 RID: 3172 RVA: 0x0001BEF8 File Offset: 0x0001A0F8
		public void ClearThreshold()
		{
			this.ClearThreshold(ColorAdjustType.Default);
		}

		/// <summary>Clears the threshold value for a specified category.</summary>
		/// <param name="type">An element of <see cref="T:System.Drawing.Imaging.ColorAdjustType" /> that specifies the category for which the threshold is cleared. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000C65 RID: 3173 RVA: 0x0001BF04 File Offset: 0x0001A104
		public void ClearThreshold(ColorAdjustType type)
		{
			int num = GDIPlus.GdipSetImageAttributesThreshold(new HandleRef(this, this.nativeImageAttributes), type, false, 0f);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		/// <summary>Sets the gamma value for the default category.</summary>
		/// <param name="gamma">The gamma correction value. </param>
		// Token: 0x06000C66 RID: 3174 RVA: 0x0001BF34 File Offset: 0x0001A134
		public void SetGamma(float gamma)
		{
			this.SetGamma(gamma, ColorAdjustType.Default);
		}

		/// <summary>Sets the gamma value for a specified category.</summary>
		/// <param name="gamma">The gamma correction value. </param>
		/// <param name="type">An element of the <see cref="T:System.Drawing.Imaging.ColorAdjustType" /> enumeration that specifies the category for which the gamma value is set. </param>
		// Token: 0x06000C67 RID: 3175 RVA: 0x0001BF40 File Offset: 0x0001A140
		public void SetGamma(float gamma, ColorAdjustType type)
		{
			int num = GDIPlus.GdipSetImageAttributesGamma(new HandleRef(this, this.nativeImageAttributes), type, true, gamma);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		/// <summary>Disables gamma correction for the default category.</summary>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000C68 RID: 3176 RVA: 0x0001BF6C File Offset: 0x0001A16C
		public void ClearGamma()
		{
			this.ClearGamma(ColorAdjustType.Default);
		}

		/// <summary>Disables gamma correction for a specified category.</summary>
		/// <param name="type">An element of <see cref="T:System.Drawing.Imaging.ColorAdjustType" /> that specifies the category for which gamma correction is disabled. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000C69 RID: 3177 RVA: 0x0001BF78 File Offset: 0x0001A178
		public void ClearGamma(ColorAdjustType type)
		{
			int num = GDIPlus.GdipSetImageAttributesGamma(new HandleRef(this, this.nativeImageAttributes), type, false, 0f);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		/// <summary>Turns off color adjustment for the default category. You can call the <see cref="Overload:System.Drawing.Imaging.ImageAttributes.ClearNoOp" /> method to reinstate the color-adjustment settings that were in place before the call to the <see cref="Overload:System.Drawing.Imaging.ImageAttributes.SetNoOp" /> method.</summary>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000C6A RID: 3178 RVA: 0x0001BFA8 File Offset: 0x0001A1A8
		public void SetNoOp()
		{
			this.SetNoOp(ColorAdjustType.Default);
		}

		/// <summary>Turns off color adjustment for a specified category. You can call the <see cref="Overload:System.Drawing.Imaging.ImageAttributes.ClearNoOp" /> method to reinstate the color-adjustment settings that were in place before the call to the <see cref="Overload:System.Drawing.Imaging.ImageAttributes.SetNoOp" /> method.</summary>
		/// <param name="type">An element of <see cref="T:System.Drawing.Imaging.ColorAdjustType" /> that specifies the category for which color correction is turned off. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000C6B RID: 3179 RVA: 0x0001BFB4 File Offset: 0x0001A1B4
		public void SetNoOp(ColorAdjustType type)
		{
			int num = GDIPlus.GdipSetImageAttributesNoOp(new HandleRef(this, this.nativeImageAttributes), type, true);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		/// <summary>Clears the NoOp setting for the default category.</summary>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000C6C RID: 3180 RVA: 0x0001BFDF File Offset: 0x0001A1DF
		public void ClearNoOp()
		{
			this.ClearNoOp(ColorAdjustType.Default);
		}

		/// <summary>Clears the NoOp setting for a specified category.</summary>
		/// <param name="type">An element of <see cref="T:System.Drawing.Imaging.ColorAdjustType" /> that specifies the category for which the NoOp setting is cleared. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000C6D RID: 3181 RVA: 0x0001BFE8 File Offset: 0x0001A1E8
		public void ClearNoOp(ColorAdjustType type)
		{
			int num = GDIPlus.GdipSetImageAttributesNoOp(new HandleRef(this, this.nativeImageAttributes), type, false);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		/// <summary>Sets the color key for the default category.</summary>
		/// <param name="colorLow">The low color-key value. </param>
		/// <param name="colorHigh">The high color-key value. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000C6E RID: 3182 RVA: 0x0001C013 File Offset: 0x0001A213
		public void SetColorKey(Color colorLow, Color colorHigh)
		{
			this.SetColorKey(colorLow, colorHigh, ColorAdjustType.Default);
		}

		/// <summary>Sets the color key (transparency range) for a specified category.</summary>
		/// <param name="colorLow">The low color-key value. </param>
		/// <param name="colorHigh">The high color-key value. </param>
		/// <param name="type">An element of <see cref="T:System.Drawing.Imaging.ColorAdjustType" /> that specifies the category for which the color key is set. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000C6F RID: 3183 RVA: 0x0001C020 File Offset: 0x0001A220
		public void SetColorKey(Color colorLow, Color colorHigh, ColorAdjustType type)
		{
			int num = colorLow.ToArgb();
			int num2 = colorHigh.ToArgb();
			int num3 = GDIPlus.GdipSetImageAttributesColorKeys(new HandleRef(this, this.nativeImageAttributes), type, true, num, num2);
			if (num3 != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num3);
			}
		}

		/// <summary>Clears the color key (transparency range) for the default category.</summary>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000C70 RID: 3184 RVA: 0x0001C05D File Offset: 0x0001A25D
		public void ClearColorKey()
		{
			this.ClearColorKey(ColorAdjustType.Default);
		}

		/// <summary>Clears the color key (transparency range) for a specified category.</summary>
		/// <param name="type">An element of <see cref="T:System.Drawing.Imaging.ColorAdjustType" /> that specifies the category for which the color key is cleared. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000C71 RID: 3185 RVA: 0x0001C068 File Offset: 0x0001A268
		public void ClearColorKey(ColorAdjustType type)
		{
			int num = 0;
			int num2 = GDIPlus.GdipSetImageAttributesColorKeys(new HandleRef(this, this.nativeImageAttributes), type, false, num, num);
			if (num2 != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num2);
			}
		}

		/// <summary>Sets the CMYK (cyan-magenta-yellow-black) output channel for the default category.</summary>
		/// <param name="flags">An element of <see cref="T:System.Drawing.Imaging.ColorChannelFlag" /> that specifies the output channel. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000C72 RID: 3186 RVA: 0x0001C097 File Offset: 0x0001A297
		public void SetOutputChannel(ColorChannelFlag flags)
		{
			this.SetOutputChannel(flags, ColorAdjustType.Default);
		}

		/// <summary>Sets the CMYK (cyan-magenta-yellow-black) output channel for a specified category.</summary>
		/// <param name="flags">An element of <see cref="T:System.Drawing.Imaging.ColorChannelFlag" /> that specifies the output channel. </param>
		/// <param name="type">An element of <see cref="T:System.Drawing.Imaging.ColorAdjustType" /> that specifies the category for which the output channel is set. </param>
		// Token: 0x06000C73 RID: 3187 RVA: 0x0001C0A4 File Offset: 0x0001A2A4
		public void SetOutputChannel(ColorChannelFlag flags, ColorAdjustType type)
		{
			int num = GDIPlus.GdipSetImageAttributesOutputChannel(new HandleRef(this, this.nativeImageAttributes), type, true, flags);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		/// <summary>Clears the CMYK (cyan-magenta-yellow-black) output channel setting for the default category.</summary>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000C74 RID: 3188 RVA: 0x0001C0D0 File Offset: 0x0001A2D0
		public void ClearOutputChannel()
		{
			this.ClearOutputChannel(ColorAdjustType.Default);
		}

		/// <summary>Clears the (cyan-magenta-yellow-black) output channel setting for a specified category.</summary>
		/// <param name="type">An element of <see cref="T:System.Drawing.Imaging.ColorAdjustType" /> that specifies the category for which the output channel setting is cleared. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000C75 RID: 3189 RVA: 0x0001C0DC File Offset: 0x0001A2DC
		public void ClearOutputChannel(ColorAdjustType type)
		{
			int num = GDIPlus.GdipSetImageAttributesOutputChannel(new HandleRef(this, this.nativeImageAttributes), type, false, ColorChannelFlag.ColorChannelLast);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		/// <summary>Sets the output channel color-profile file for the default category.</summary>
		/// <param name="colorProfileFilename">The path name of a color-profile file. If the color-profile file is in the %SystemRoot%\System32\Spool\Drivers\Color directory, this parameter can be the file name. Otherwise, this parameter must be the fully qualified path name. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000C76 RID: 3190 RVA: 0x0001C108 File Offset: 0x0001A308
		public void SetOutputChannelColorProfile(string colorProfileFilename)
		{
			this.SetOutputChannelColorProfile(colorProfileFilename, ColorAdjustType.Default);
		}

		/// <summary>Sets the output channel color-profile file for a specified category.</summary>
		/// <param name="colorProfileFilename">The path name of a color-profile file. If the color-profile file is in the %SystemRoot%\System32\Spool\Drivers\Color directory, this parameter can be the file name. Otherwise, this parameter must be the fully qualified path name. </param>
		/// <param name="type">An element of <see cref="T:System.Drawing.Imaging.ColorAdjustType" /> that specifies the category for which the output channel color-profile file is set. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000C77 RID: 3191 RVA: 0x0001C114 File Offset: 0x0001A314
		public void SetOutputChannelColorProfile(string colorProfileFilename, ColorAdjustType type)
		{
			Path.GetFullPath(colorProfileFilename);
			int num = GDIPlus.GdipSetImageAttributesOutputChannelColorProfile(new HandleRef(this, this.nativeImageAttributes), type, true, colorProfileFilename);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		/// <summary>Clears the output channel color profile setting for the default category.</summary>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000C78 RID: 3192 RVA: 0x0001C0D0 File Offset: 0x0001A2D0
		public void ClearOutputChannelColorProfile()
		{
			this.ClearOutputChannel(ColorAdjustType.Default);
		}

		/// <summary>Clears the output channel color profile setting for a specified category.</summary>
		/// <param name="type">An element of <see cref="T:System.Drawing.Imaging.ColorAdjustType" /> that specifies the category for which the output channel profile setting is cleared. </param>
		// Token: 0x06000C79 RID: 3193 RVA: 0x0001C148 File Offset: 0x0001A348
		public void ClearOutputChannelColorProfile(ColorAdjustType type)
		{
			int num = GDIPlus.GdipSetImageAttributesOutputChannel(new HandleRef(this, this.nativeImageAttributes), type, false, ColorChannelFlag.ColorChannelLast);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		/// <summary>Sets the color-remap table for the default category.</summary>
		/// <param name="map">An array of color pairs of type <see cref="T:System.Drawing.Imaging.ColorMap" />. Each color pair contains an existing color (the first value) and the color that it will be mapped to (the second value). </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000C7A RID: 3194 RVA: 0x0001C174 File Offset: 0x0001A374
		public void SetRemapTable(ColorMap[] map)
		{
			this.SetRemapTable(map, ColorAdjustType.Default);
		}

		/// <summary>Sets the color-remap table for a specified category.</summary>
		/// <param name="map">An array of color pairs of type <see cref="T:System.Drawing.Imaging.ColorMap" />. Each color pair contains an existing color (the first value) and the color that it will be mapped to (the second value). </param>
		/// <param name="type">An element of <see cref="T:System.Drawing.Imaging.ColorAdjustType" /> that specifies the category for which the color-remap table is set. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000C7B RID: 3195 RVA: 0x0001C180 File Offset: 0x0001A380
		public void SetRemapTable(ColorMap[] map, ColorAdjustType type)
		{
			int num = map.Length;
			int num2 = 4;
			IntPtr intPtr = Marshal.AllocHGlobal(checked(num * num2 * 2));
			try
			{
				for (int i = 0; i < num; i++)
				{
					Marshal.StructureToPtr<int>(map[i].OldColor.ToArgb(), (IntPtr)((long)intPtr + (long)(i * num2 * 2)), false);
					Marshal.StructureToPtr<int>(map[i].NewColor.ToArgb(), (IntPtr)((long)intPtr + (long)(i * num2 * 2) + (long)num2), false);
				}
				int num3 = GDIPlus.GdipSetImageAttributesRemapTable(new HandleRef(this, this.nativeImageAttributes), type, true, num, new HandleRef(null, intPtr));
				if (num3 != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num3);
				}
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
			}
		}

		/// <summary>Clears the color-remap table for the default category.</summary>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000C7C RID: 3196 RVA: 0x0001C244 File Offset: 0x0001A444
		public void ClearRemapTable()
		{
			this.ClearRemapTable(ColorAdjustType.Default);
		}

		/// <summary>Clears the color-remap table for a specified category.</summary>
		/// <param name="type">An element of <see cref="T:System.Drawing.Imaging.ColorAdjustType" /> that specifies the category for which the remap table is cleared. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000C7D RID: 3197 RVA: 0x0001C250 File Offset: 0x0001A450
		public void ClearRemapTable(ColorAdjustType type)
		{
			int num = GDIPlus.GdipSetImageAttributesRemapTable(new HandleRef(this, this.nativeImageAttributes), type, false, 0, NativeMethods.NullHandleRef);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		/// <summary>Sets the color-remap table for the brush category.</summary>
		/// <param name="map">An array of <see cref="T:System.Drawing.Imaging.ColorMap" /> objects. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000C7E RID: 3198 RVA: 0x0001C281 File Offset: 0x0001A481
		public void SetBrushRemapTable(ColorMap[] map)
		{
			this.SetRemapTable(map, ColorAdjustType.Brush);
		}

		/// <summary>Clears the brush color-remap table of this <see cref="T:System.Drawing.Imaging.ImageAttributes" /> object.</summary>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000C7F RID: 3199 RVA: 0x0001C28B File Offset: 0x0001A48B
		public void ClearBrushRemapTable()
		{
			this.ClearRemapTable(ColorAdjustType.Brush);
		}

		/// <summary>Sets the wrap mode that is used to decide how to tile a texture across a shape, or at shape boundaries. A texture is tiled across a shape to fill it in when the texture is smaller than the shape it is filling.</summary>
		/// <param name="mode">An element of <see cref="T:System.Drawing.Drawing2D.WrapMode" /> that specifies how repeated copies of an image are used to tile an area. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000C80 RID: 3200 RVA: 0x0001C294 File Offset: 0x0001A494
		public void SetWrapMode(WrapMode mode)
		{
			this.SetWrapMode(mode, default(Color), false);
		}

		/// <summary>Sets the wrap mode and color used to decide how to tile a texture across a shape, or at shape boundaries. A texture is tiled across a shape to fill it in when the texture is smaller than the shape it is filling.</summary>
		/// <param name="mode">An element of <see cref="T:System.Drawing.Drawing2D.WrapMode" /> that specifies how repeated copies of an image are used to tile an area. </param>
		/// <param name="color">An <see cref="T:System.Drawing.Imaging.ImageAttributes" /> object that specifies the color of pixels outside of a rendered image. This color is visible if the mode parameter is set to <see cref="F:System.Drawing.Drawing2D.WrapMode.Clamp" /> and the source rectangle passed to <see cref="Overload:System.Drawing.Graphics.DrawImage" /> is larger than the image itself. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000C81 RID: 3201 RVA: 0x0001C2B2 File Offset: 0x0001A4B2
		public void SetWrapMode(WrapMode mode, Color color)
		{
			this.SetWrapMode(mode, color, false);
		}

		/// <summary>Sets the wrap mode and color used to decide how to tile a texture across a shape, or at shape boundaries. A texture is tiled across a shape to fill it in when the texture is smaller than the shape it is filling.</summary>
		/// <param name="mode">An element of <see cref="T:System.Drawing.Drawing2D.WrapMode" /> that specifies how repeated copies of an image are used to tile an area. </param>
		/// <param name="color">A color object that specifies the color of pixels outside of a rendered image. This color is visible if the mode parameter is set to <see cref="F:System.Drawing.Drawing2D.WrapMode.Clamp" /> and the source rectangle passed to <see cref="Overload:System.Drawing.Graphics.DrawImage" /> is larger than the image itself. </param>
		/// <param name="clamp">This parameter has no effect. Set it to false. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000C82 RID: 3202 RVA: 0x0001C2C0 File Offset: 0x0001A4C0
		public void SetWrapMode(WrapMode mode, Color color, bool clamp)
		{
			int num = GDIPlus.GdipSetImageAttributesWrapMode(new HandleRef(this, this.nativeImageAttributes), (int)mode, color.ToArgb(), clamp);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		/// <summary>Adjusts the colors in a palette according to the adjustment settings of a specified category.</summary>
		/// <param name="palette">A <see cref="T:System.Drawing.Imaging.ColorPalette" /> that on input contains the palette to be adjusted, and on output contains the adjusted palette. </param>
		/// <param name="type">An element of <see cref="T:System.Drawing.Imaging.ColorAdjustType" /> that specifies the category whose adjustment settings will be applied to the palette. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000C83 RID: 3203 RVA: 0x0001C2F4 File Offset: 0x0001A4F4
		public void GetAdjustedPalette(ColorPalette palette, ColorAdjustType type)
		{
			IntPtr intPtr = palette.ConvertToMemory();
			try
			{
				int num = GDIPlus.GdipGetImageAttributesAdjustedPalette(new HandleRef(this, this.nativeImageAttributes), new HandleRef(null, intPtr), type);
				if (num != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num);
				}
				palette.ConvertFromMemory(intPtr);
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr);
				}
			}
		}

		// Token: 0x0400098F RID: 2447
		internal IntPtr nativeImageAttributes;
	}
}
