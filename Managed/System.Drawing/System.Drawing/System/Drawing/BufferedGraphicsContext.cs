using System;

namespace System.Drawing
{
	/// <summary>Provides methods for creating graphics buffers that can be used for double buffering.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000040 RID: 64
	public sealed class BufferedGraphicsContext : IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.BufferedGraphicsContext" /> class.</summary>
		// Token: 0x060001E8 RID: 488 RVA: 0x00005909 File Offset: 0x00003B09
		public BufferedGraphicsContext()
		{
			this.max_buffer = Size.Empty;
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x0000591C File Offset: 0x00003B1C
		~BufferedGraphicsContext()
		{
		}

		/// <summary>Creates a graphics buffer of the specified size using the pixel format of the specified <see cref="T:System.Drawing.Graphics" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.BufferedGraphics" /> that can be used to draw to a buffer of the specified dimensions.</returns>
		/// <param name="targetGraphics">The <see cref="T:System.Drawing.Graphics" /> to match the pixel format for the new buffer to. </param>
		/// <param name="targetRectangle">A <see cref="T:System.Drawing.Rectangle" /> indicating the size of the buffer to create. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060001EA RID: 490 RVA: 0x00005944 File Offset: 0x00003B44
		public BufferedGraphics Allocate(Graphics targetGraphics, Rectangle targetRectangle)
		{
			return new BufferedGraphics(targetGraphics, targetRectangle);
		}

		/// <summary>Creates a graphics buffer of the specified size using the pixel format of the specified <see cref="T:System.Drawing.Graphics" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.BufferedGraphics" /> that can be used to draw to a buffer of the specified dimensions.</returns>
		/// <param name="targetDC">An <see cref="T:System.IntPtr" /> to a device context to match the pixel format of the new buffer to. </param>
		/// <param name="targetRectangle">A <see cref="T:System.Drawing.Rectangle" /> indicating the size of the buffer to create. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060001EB RID: 491 RVA: 0x00005902 File Offset: 0x00003B02
		[MonoTODO("The targetDC parameter has no equivalent in libgdiplus.")]
		public BufferedGraphics Allocate(IntPtr targetDC, Rectangle targetRectangle)
		{
			throw new NotImplementedException();
		}

		/// <summary>Releases all resources used by the <see cref="T:System.Drawing.BufferedGraphicsContext" />.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060001EC RID: 492 RVA: 0x0000594D File Offset: 0x00003B4D
		public void Dispose()
		{
			GC.SuppressFinalize(this);
		}

		/// <summary>Disposes of the current graphics buffer, if a buffer has been allocated and has not yet been disposed.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060001ED RID: 493 RVA: 0x00002CE2 File Offset: 0x00000EE2
		public void Invalidate()
		{
		}

		/// <summary>Gets or sets the maximum size of the buffer to use.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> indicating the maximum size of the buffer dimensions.</returns>
		/// <exception cref="T:System.ArgumentException">The height or width of the size is less than or equal to zero. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Window="AllWindows" />
		/// </PermissionSet>
		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060001EE RID: 494 RVA: 0x00005955 File Offset: 0x00003B55
		// (set) Token: 0x060001EF RID: 495 RVA: 0x0000595D File Offset: 0x00003B5D
		public Size MaximumBuffer
		{
			get
			{
				return this.max_buffer;
			}
			set
			{
				if (value.Width <= 0 || value.Height <= 0)
				{
					throw new ArgumentException("The height or width of the size is less than or equal to zero.");
				}
				this.max_buffer = value;
			}
		}

		// Token: 0x0400034D RID: 845
		private Size max_buffer;
	}
}
