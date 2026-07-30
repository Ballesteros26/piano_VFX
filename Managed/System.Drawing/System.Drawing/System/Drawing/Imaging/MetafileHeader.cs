using System;
using System.Runtime.InteropServices;
using Unity;

namespace System.Drawing.Imaging
{
	/// <summary>Contains attributes of an associated <see cref="T:System.Drawing.Imaging.Metafile" />. Not inheritable.</summary>
	// Token: 0x0200011A RID: 282
	[MonoTODO("Metafiles, both WMF and EMF formats, aren't supported.")]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class MetafileHeader
	{
		// Token: 0x06000D26 RID: 3366 RVA: 0x0001D777 File Offset: 0x0001B977
		internal MetafileHeader(IntPtr henhmetafile)
		{
			Marshal.PtrToStructure<MetafileHeader>(henhmetafile, this);
		}

		/// <summary>Returns a value that indicates whether the associated <see cref="T:System.Drawing.Imaging.Metafile" /> is device dependent.</summary>
		/// <returns>true if the associated <see cref="T:System.Drawing.Imaging.Metafile" /> is device dependent; otherwise, false.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000D27 RID: 3367 RVA: 0x0000915C File Offset: 0x0000735C
		[MonoTODO("always returns false")]
		public bool IsDisplay()
		{
			return false;
		}

		/// <summary>Returns a value that indicates whether the associated <see cref="T:System.Drawing.Imaging.Metafile" /> is in the Windows enhanced metafile format.</summary>
		/// <returns>true if the associated <see cref="T:System.Drawing.Imaging.Metafile" /> is in the Windows enhanced metafile format; otherwise, false.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000D28 RID: 3368 RVA: 0x0001D786 File Offset: 0x0001B986
		public bool IsEmf()
		{
			return this.Type == MetafileType.Emf;
		}

		/// <summary>Returns a value that indicates whether the associated <see cref="T:System.Drawing.Imaging.Metafile" /> is in the Windows enhanced metafile format or the Windows enhanced metafile plus format.</summary>
		/// <returns>true if the associated <see cref="T:System.Drawing.Imaging.Metafile" /> is in the Windows enhanced metafile format or the Windows enhanced metafile plus format; otherwise, false.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000D29 RID: 3369 RVA: 0x0001D791 File Offset: 0x0001B991
		public bool IsEmfOrEmfPlus()
		{
			return this.Type >= MetafileType.Emf;
		}

		/// <summary>Returns a value that indicates whether the associated <see cref="T:System.Drawing.Imaging.Metafile" /> is in the Windows enhanced metafile plus format.</summary>
		/// <returns>true if the associated <see cref="T:System.Drawing.Imaging.Metafile" /> is in the Windows enhanced metafile plus format; otherwise, false.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000D2A RID: 3370 RVA: 0x0001D79F File Offset: 0x0001B99F
		public bool IsEmfPlus()
		{
			return this.Type >= MetafileType.EmfPlusOnly;
		}

		/// <summary>Returns a value that indicates whether the associated <see cref="T:System.Drawing.Imaging.Metafile" /> is in the Dual enhanced metafile format. This format supports both the enhanced and the enhanced plus format.</summary>
		/// <returns>true if the associated <see cref="T:System.Drawing.Imaging.Metafile" /> is in the Dual enhanced metafile format; otherwise, false.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000D2B RID: 3371 RVA: 0x0001D7AD File Offset: 0x0001B9AD
		public bool IsEmfPlusDual()
		{
			return this.Type == MetafileType.EmfPlusDual;
		}

		/// <summary>Returns a value that indicates whether the associated <see cref="T:System.Drawing.Imaging.Metafile" /> supports only the Windows enhanced metafile plus format.</summary>
		/// <returns>true if the associated <see cref="T:System.Drawing.Imaging.Metafile" /> supports only the Windows enhanced metafile plus format; otherwise, false.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000D2C RID: 3372 RVA: 0x0001D7B8 File Offset: 0x0001B9B8
		public bool IsEmfPlusOnly()
		{
			return this.Type == MetafileType.EmfPlusOnly;
		}

		/// <summary>Returns a value that indicates whether the associated <see cref="T:System.Drawing.Imaging.Metafile" /> is in the Windows metafile format.</summary>
		/// <returns>true if the associated <see cref="T:System.Drawing.Imaging.Metafile" /> is in the Windows metafile format; otherwise, false.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000D2D RID: 3373 RVA: 0x0001D7C3 File Offset: 0x0001B9C3
		public bool IsWmf()
		{
			return this.Type <= MetafileType.WmfPlaceable;
		}

		/// <summary>Returns a value that indicates whether the associated <see cref="T:System.Drawing.Imaging.Metafile" /> is in the Windows placeable metafile format.</summary>
		/// <returns>true if the associated <see cref="T:System.Drawing.Imaging.Metafile" /> is in the Windows placeable metafile format; otherwise, false.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000D2E RID: 3374 RVA: 0x0001D7D1 File Offset: 0x0001B9D1
		public bool IsWmfPlaceable()
		{
			return this.Type == MetafileType.WmfPlaceable;
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Rectangle" /> that bounds the associated <see cref="T:System.Drawing.Imaging.Metafile" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that bounds the associated <see cref="T:System.Drawing.Imaging.Metafile" />.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000390 RID: 912
		// (get) Token: 0x06000D2F RID: 3375 RVA: 0x0001D7DC File Offset: 0x0001B9DC
		public Rectangle Bounds
		{
			get
			{
				return new Rectangle(this.header.x, this.header.y, this.header.width, this.header.height);
			}
		}

		/// <summary>Gets the horizontal resolution, in dots per inch, of the associated <see cref="T:System.Drawing.Imaging.Metafile" />.</summary>
		/// <returns>The horizontal resolution, in dots per inch, of the associated <see cref="T:System.Drawing.Imaging.Metafile" />.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000391 RID: 913
		// (get) Token: 0x06000D30 RID: 3376 RVA: 0x0001D80F File Offset: 0x0001BA0F
		public float DpiX
		{
			get
			{
				return this.header.dpi_x;
			}
		}

		/// <summary>Gets the vertical resolution, in dots per inch, of the associated <see cref="T:System.Drawing.Imaging.Metafile" />.</summary>
		/// <returns>The vertical resolution, in dots per inch, of the associated <see cref="T:System.Drawing.Imaging.Metafile" />.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000392 RID: 914
		// (get) Token: 0x06000D31 RID: 3377 RVA: 0x0001D81C File Offset: 0x0001BA1C
		public float DpiY
		{
			get
			{
				return this.header.dpi_y;
			}
		}

		/// <summary>Gets the size, in bytes, of the enhanced metafile plus header file.</summary>
		/// <returns>The size, in bytes, of the enhanced metafile plus header file.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000393 RID: 915
		// (get) Token: 0x06000D32 RID: 3378 RVA: 0x0001D829 File Offset: 0x0001BA29
		public int EmfPlusHeaderSize
		{
			get
			{
				return this.header.emfplus_header_size;
			}
		}

		/// <summary>Gets the logical horizontal resolution, in dots per inch, of the associated <see cref="T:System.Drawing.Imaging.Metafile" />.</summary>
		/// <returns>The logical horizontal resolution, in dots per inch, of the associated <see cref="T:System.Drawing.Imaging.Metafile" />.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000394 RID: 916
		// (get) Token: 0x06000D33 RID: 3379 RVA: 0x0001D836 File Offset: 0x0001BA36
		public int LogicalDpiX
		{
			get
			{
				return this.header.logical_dpi_x;
			}
		}

		/// <summary>Gets the logical vertical resolution, in dots per inch, of the associated <see cref="T:System.Drawing.Imaging.Metafile" />.</summary>
		/// <returns>The logical vertical resolution, in dots per inch, of the associated <see cref="T:System.Drawing.Imaging.Metafile" />.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000395 RID: 917
		// (get) Token: 0x06000D34 RID: 3380 RVA: 0x0001D843 File Offset: 0x0001BA43
		public int LogicalDpiY
		{
			get
			{
				return this.header.logical_dpi_y;
			}
		}

		/// <summary>Gets the size, in bytes, of the associated <see cref="T:System.Drawing.Imaging.Metafile" />.</summary>
		/// <returns>The size, in bytes, of the associated <see cref="T:System.Drawing.Imaging.Metafile" />.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000396 RID: 918
		// (get) Token: 0x06000D35 RID: 3381 RVA: 0x0001D850 File Offset: 0x0001BA50
		public int MetafileSize
		{
			get
			{
				return this.header.size;
			}
		}

		/// <summary>Gets the type of the associated <see cref="T:System.Drawing.Imaging.Metafile" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Imaging.MetafileType" /> enumeration that represents the type of the associated <see cref="T:System.Drawing.Imaging.Metafile" />.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000397 RID: 919
		// (get) Token: 0x06000D36 RID: 3382 RVA: 0x0001D85D File Offset: 0x0001BA5D
		public MetafileType Type
		{
			get
			{
				return this.header.type;
			}
		}

		/// <summary>Gets the version number of the associated <see cref="T:System.Drawing.Imaging.Metafile" />.</summary>
		/// <returns>The version number of the associated <see cref="T:System.Drawing.Imaging.Metafile" />.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000398 RID: 920
		// (get) Token: 0x06000D37 RID: 3383 RVA: 0x0001D86A File Offset: 0x0001BA6A
		public int Version
		{
			get
			{
				return this.header.version;
			}
		}

		/// <summary>Gets the Windows metafile (WMF) header file for the associated <see cref="T:System.Drawing.Imaging.Metafile" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Imaging.MetaHeader" /> that contains the WMF header file for the associated <see cref="T:System.Drawing.Imaging.Metafile" />.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000399 RID: 921
		// (get) Token: 0x06000D38 RID: 3384 RVA: 0x0001D877 File Offset: 0x0001BA77
		public MetaHeader WmfHeader
		{
			get
			{
				if (this.IsWmf())
				{
					return new MetaHeader(this.header.wmf_header);
				}
				throw new ArgumentException("WmfHeader only available on WMF files.");
			}
		}

		// Token: 0x06000D39 RID: 3385 RVA: 0x00003B8D File Offset: 0x00001D8D
		internal MetafileHeader()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04000A7C RID: 2684
		private MonoMetafileHeader header;
	}
}
