using System;
using System.Runtime.InteropServices;

namespace System.Drawing.Imaging
{
	/// <summary>The <see cref="T:System.Drawing.Imaging.ImageCodecInfo" /> class provides the necessary storage members and methods to retrieve all pertinent information about the installed image encoders and decoders (called codecs). Not inheritable. </summary>
	// Token: 0x02000105 RID: 261
	public sealed class ImageCodecInfo
	{
		// Token: 0x06000C84 RID: 3204 RVA: 0x00002050 File Offset: 0x00000250
		internal ImageCodecInfo()
		{
		}

		/// <summary>Gets or sets a <see cref="T:System.Guid" /> structure that contains a GUID that identifies a specific codec.</summary>
		/// <returns>A <see cref="T:System.Guid" /> structure that contains a GUID that identifies a specific codec.</returns>
		// Token: 0x1700035F RID: 863
		// (get) Token: 0x06000C85 RID: 3205 RVA: 0x0001C35C File Offset: 0x0001A55C
		// (set) Token: 0x06000C86 RID: 3206 RVA: 0x0001C364 File Offset: 0x0001A564
		public Guid Clsid
		{
			get
			{
				return this._clsid;
			}
			set
			{
				this._clsid = value;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Guid" /> structure that contains a GUID that identifies the codec's format.</summary>
		/// <returns>A <see cref="T:System.Guid" /> structure that contains a GUID that identifies the codec's format.</returns>
		// Token: 0x17000360 RID: 864
		// (get) Token: 0x06000C87 RID: 3207 RVA: 0x0001C36D File Offset: 0x0001A56D
		// (set) Token: 0x06000C88 RID: 3208 RVA: 0x0001C375 File Offset: 0x0001A575
		public Guid FormatID
		{
			get
			{
				return this._formatID;
			}
			set
			{
				this._formatID = value;
			}
		}

		/// <summary>Gets or sets a string that contains the name of the codec.</summary>
		/// <returns>A string that contains the name of the codec.</returns>
		// Token: 0x17000361 RID: 865
		// (get) Token: 0x06000C89 RID: 3209 RVA: 0x0001C37E File Offset: 0x0001A57E
		// (set) Token: 0x06000C8A RID: 3210 RVA: 0x0001C386 File Offset: 0x0001A586
		public string CodecName
		{
			get
			{
				return this._codecName;
			}
			set
			{
				this._codecName = value;
			}
		}

		/// <summary>Gets or sets string that contains the path name of the DLL that holds the codec. If the codec is not in a DLL, this pointer is null.</summary>
		/// <returns>A string that contains the path name of the DLL that holds the codec.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000362 RID: 866
		// (get) Token: 0x06000C8B RID: 3211 RVA: 0x0001C38F File Offset: 0x0001A58F
		// (set) Token: 0x06000C8C RID: 3212 RVA: 0x0001C397 File Offset: 0x0001A597
		public string DllName
		{
			get
			{
				return this._dllName;
			}
			set
			{
				this._dllName = value;
			}
		}

		/// <summary>Gets or sets a string that describes the codec's file format.</summary>
		/// <returns>A string that describes the codec's file format.</returns>
		// Token: 0x17000363 RID: 867
		// (get) Token: 0x06000C8D RID: 3213 RVA: 0x0001C3A0 File Offset: 0x0001A5A0
		// (set) Token: 0x06000C8E RID: 3214 RVA: 0x0001C3A8 File Offset: 0x0001A5A8
		public string FormatDescription
		{
			get
			{
				return this._formatDescription;
			}
			set
			{
				this._formatDescription = value;
			}
		}

		/// <summary>Gets or sets string that contains the file name extension(s) used in the codec. The extensions are separated by semicolons.</summary>
		/// <returns>A string that contains the file name extension(s) used in the codec.</returns>
		// Token: 0x17000364 RID: 868
		// (get) Token: 0x06000C8F RID: 3215 RVA: 0x0001C3B1 File Offset: 0x0001A5B1
		// (set) Token: 0x06000C90 RID: 3216 RVA: 0x0001C3B9 File Offset: 0x0001A5B9
		public string FilenameExtension
		{
			get
			{
				return this._filenameExtension;
			}
			set
			{
				this._filenameExtension = value;
			}
		}

		/// <summary>Gets or sets a string that contains the codec's Multipurpose Internet Mail Extensions (MIME) type.</summary>
		/// <returns>A string that contains the codec's Multipurpose Internet Mail Extensions (MIME) type.</returns>
		// Token: 0x17000365 RID: 869
		// (get) Token: 0x06000C91 RID: 3217 RVA: 0x0001C3C2 File Offset: 0x0001A5C2
		// (set) Token: 0x06000C92 RID: 3218 RVA: 0x0001C3CA File Offset: 0x0001A5CA
		public string MimeType
		{
			get
			{
				return this._mimeType;
			}
			set
			{
				this._mimeType = value;
			}
		}

		/// <summary>Gets or sets 32-bit value used to store additional information about the codec. This property returns a combination of flags from the <see cref="T:System.Drawing.Imaging.ImageCodecFlags" /> enumeration.</summary>
		/// <returns>A 32-bit value used to store additional information about the codec.</returns>
		// Token: 0x17000366 RID: 870
		// (get) Token: 0x06000C93 RID: 3219 RVA: 0x0001C3D3 File Offset: 0x0001A5D3
		// (set) Token: 0x06000C94 RID: 3220 RVA: 0x0001C3DB File Offset: 0x0001A5DB
		public ImageCodecFlags Flags
		{
			get
			{
				return this._flags;
			}
			set
			{
				this._flags = value;
			}
		}

		/// <summary>Gets or sets the version number of the codec.</summary>
		/// <returns>The version number of the codec.</returns>
		// Token: 0x17000367 RID: 871
		// (get) Token: 0x06000C95 RID: 3221 RVA: 0x0001C3E4 File Offset: 0x0001A5E4
		// (set) Token: 0x06000C96 RID: 3222 RVA: 0x0001C3EC File Offset: 0x0001A5EC
		public int Version
		{
			get
			{
				return this._version;
			}
			set
			{
				this._version = value;
			}
		}

		/// <summary>Gets or sets a two dimensional array of bytes that represents the signature of the codec.</summary>
		/// <returns>A two dimensional array of bytes that represents the signature of the codec.</returns>
		// Token: 0x17000368 RID: 872
		// (get) Token: 0x06000C97 RID: 3223 RVA: 0x0001C3F5 File Offset: 0x0001A5F5
		// (set) Token: 0x06000C98 RID: 3224 RVA: 0x0001C3FD File Offset: 0x0001A5FD
		[CLSCompliant(false)]
		public byte[][] SignaturePatterns
		{
			get
			{
				return this._signaturePatterns;
			}
			set
			{
				this._signaturePatterns = value;
			}
		}

		/// <summary>Gets or sets a two dimensional array of bytes that can be used as a filter.</summary>
		/// <returns>A two dimensional array of bytes that can be used as a filter.</returns>
		// Token: 0x17000369 RID: 873
		// (get) Token: 0x06000C99 RID: 3225 RVA: 0x0001C406 File Offset: 0x0001A606
		// (set) Token: 0x06000C9A RID: 3226 RVA: 0x0001C40E File Offset: 0x0001A60E
		[CLSCompliant(false)]
		public byte[][] SignatureMasks
		{
			get
			{
				return this._signatureMasks;
			}
			set
			{
				this._signatureMasks = value;
			}
		}

		/// <summary>Returns an array of <see cref="T:System.Drawing.Imaging.ImageCodecInfo" /> objects that contain information about the image decoders built into GDI+.</summary>
		/// <returns>An array of <see cref="T:System.Drawing.Imaging.ImageCodecInfo" /> objects. Each <see cref="T:System.Drawing.Imaging.ImageCodecInfo" /> object in the array contains information about one of the built-in image decoders.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000C9B RID: 3227 RVA: 0x0001C418 File Offset: 0x0001A618
		public static ImageCodecInfo[] GetImageDecoders()
		{
			int num2;
			int num3;
			int num = GDIPlus.GdipGetImageDecodersSize(out num2, out num3);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			IntPtr intPtr = Marshal.AllocHGlobal(num3);
			ImageCodecInfo[] array;
			try
			{
				num = GDIPlus.GdipGetImageDecoders(num2, num3, intPtr);
				if (num != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num);
				}
				array = ImageCodecInfo.ConvertFromMemory(intPtr, num2);
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
			}
			return array;
		}

		/// <summary>Returns an array of <see cref="T:System.Drawing.Imaging.ImageCodecInfo" /> objects that contain information about the image encoders built into GDI+.</summary>
		/// <returns>An array of <see cref="T:System.Drawing.Imaging.ImageCodecInfo" /> objects. Each <see cref="T:System.Drawing.Imaging.ImageCodecInfo" /> object in the array contains information about one of the built-in image encoders.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000C9C RID: 3228 RVA: 0x0001C47C File Offset: 0x0001A67C
		public static ImageCodecInfo[] GetImageEncoders()
		{
			int num2;
			int num3;
			int num = GDIPlus.GdipGetImageEncodersSize(out num2, out num3);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			IntPtr intPtr = Marshal.AllocHGlobal(num3);
			ImageCodecInfo[] array;
			try
			{
				num = GDIPlus.GdipGetImageEncoders(num2, num3, intPtr);
				if (num != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num);
				}
				array = ImageCodecInfo.ConvertFromMemory(intPtr, num2);
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
			}
			return array;
		}

		// Token: 0x06000C9D RID: 3229 RVA: 0x0001C4E0 File Offset: 0x0001A6E0
		private static ImageCodecInfo[] ConvertFromMemory(IntPtr memoryStart, int numCodecs)
		{
			ImageCodecInfo[] array = new ImageCodecInfo[numCodecs];
			for (int i = 0; i < numCodecs; i++)
			{
				IntPtr intPtr = (IntPtr)((long)memoryStart + (long)(Marshal.SizeOf(typeof(ImageCodecInfoPrivate)) * i));
				ImageCodecInfoPrivate imageCodecInfoPrivate = new ImageCodecInfoPrivate();
				Marshal.PtrToStructure<ImageCodecInfoPrivate>(intPtr, imageCodecInfoPrivate);
				array[i] = new ImageCodecInfo();
				array[i].Clsid = imageCodecInfoPrivate.Clsid;
				array[i].FormatID = imageCodecInfoPrivate.FormatID;
				array[i].CodecName = Marshal.PtrToStringUni(imageCodecInfoPrivate.CodecName);
				array[i].DllName = Marshal.PtrToStringUni(imageCodecInfoPrivate.DllName);
				array[i].FormatDescription = Marshal.PtrToStringUni(imageCodecInfoPrivate.FormatDescription);
				array[i].FilenameExtension = Marshal.PtrToStringUni(imageCodecInfoPrivate.FilenameExtension);
				array[i].MimeType = Marshal.PtrToStringUni(imageCodecInfoPrivate.MimeType);
				array[i].Flags = (ImageCodecFlags)imageCodecInfoPrivate.Flags;
				array[i].Version = imageCodecInfoPrivate.Version;
				array[i].SignaturePatterns = new byte[imageCodecInfoPrivate.SigCount][];
				array[i].SignatureMasks = new byte[imageCodecInfoPrivate.SigCount][];
				for (int j = 0; j < imageCodecInfoPrivate.SigCount; j++)
				{
					array[i].SignaturePatterns[j] = new byte[imageCodecInfoPrivate.SigSize];
					array[i].SignatureMasks[j] = new byte[imageCodecInfoPrivate.SigSize];
					Marshal.Copy((IntPtr)((long)imageCodecInfoPrivate.SigMask + (long)(j * imageCodecInfoPrivate.SigSize)), array[i].SignatureMasks[j], 0, imageCodecInfoPrivate.SigSize);
					Marshal.Copy((IntPtr)((long)imageCodecInfoPrivate.SigPattern + (long)(j * imageCodecInfoPrivate.SigSize)), array[i].SignaturePatterns[j], 0, imageCodecInfoPrivate.SigSize);
				}
			}
			return array;
		}

		// Token: 0x0400099A RID: 2458
		private Guid _clsid;

		// Token: 0x0400099B RID: 2459
		private Guid _formatID;

		// Token: 0x0400099C RID: 2460
		private string _codecName;

		// Token: 0x0400099D RID: 2461
		private string _dllName;

		// Token: 0x0400099E RID: 2462
		private string _formatDescription;

		// Token: 0x0400099F RID: 2463
		private string _filenameExtension;

		// Token: 0x040009A0 RID: 2464
		private string _mimeType;

		// Token: 0x040009A1 RID: 2465
		private ImageCodecFlags _flags;

		// Token: 0x040009A2 RID: 2466
		private int _version;

		// Token: 0x040009A3 RID: 2467
		private byte[][] _signaturePatterns;

		// Token: 0x040009A4 RID: 2468
		private byte[][] _signatureMasks;
	}
}
