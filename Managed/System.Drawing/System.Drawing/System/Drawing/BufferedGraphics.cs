using System;

namespace System.Drawing
{
	/// <summary>Provides a graphics buffer for double buffering.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200003F RID: 63
	public sealed class BufferedGraphics : IDisposable
	{
		// Token: 0x060001DF RID: 479 RVA: 0x00002050 File Offset: 0x00000250
		private BufferedGraphics()
		{
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x000057F8 File Offset: 0x000039F8
		internal BufferedGraphics(Graphics targetGraphics, Rectangle targetRectangle)
		{
			this.size = targetRectangle;
			this.target = targetGraphics;
			this.membmp = new Bitmap(this.size.Width, this.size.Height);
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x00005830 File Offset: 0x00003A30
		~BufferedGraphics()
		{
			this.Dispose(false);
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Graphics" /> object that outputs to the graphics buffer.</summary>
		/// <returns>A <see cref="T:System.Drawing.Graphics" /> object that outputs to the graphics buffer.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060001E2 RID: 482 RVA: 0x00005860 File Offset: 0x00003A60
		public Graphics Graphics
		{
			get
			{
				if (this.source == null)
				{
					this.source = Graphics.FromImage(this.membmp);
				}
				return this.source;
			}
		}

		/// <summary>Releases all resources used by the <see cref="T:System.Drawing.BufferedGraphics" /> object.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060001E3 RID: 483 RVA: 0x00005881 File Offset: 0x00003A81
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x00005890 File Offset: 0x00003A90
		private void Dispose(bool disposing)
		{
			if (!disposing)
			{
				return;
			}
			if (this.membmp != null)
			{
				this.membmp.Dispose();
				this.membmp = null;
			}
			if (this.source != null)
			{
				this.source.Dispose();
				this.source = null;
			}
			this.target = null;
		}

		/// <summary>Writes the contents of the graphics buffer to the default device.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060001E5 RID: 485 RVA: 0x000058DC File Offset: 0x00003ADC
		public void Render()
		{
			this.Render(this.target);
		}

		/// <summary>Writes the contents of the graphics buffer to the specified <see cref="T:System.Drawing.Graphics" /> object.</summary>
		/// <param name="target">A <see cref="T:System.Drawing.Graphics" /> object to which to write the contents of the graphics buffer. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060001E6 RID: 486 RVA: 0x000058EA File Offset: 0x00003AEA
		public void Render(Graphics target)
		{
			if (target == null)
			{
				return;
			}
			target.DrawImage(this.membmp, this.size);
		}

		/// <summary>Writes the contents of the graphics buffer to the device context associated with the specified <see cref="T:System.IntPtr" /> handle.</summary>
		/// <param name="targetDC">An <see cref="T:System.IntPtr" /> that points to the device context to which to write the contents of the graphics buffer. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060001E7 RID: 487 RVA: 0x00005902 File Offset: 0x00003B02
		[MonoTODO("The targetDC parameter has no equivalent in libgdiplus.")]
		public void Render(IntPtr targetDC)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04000349 RID: 841
		private Rectangle size;

		// Token: 0x0400034A RID: 842
		private Bitmap membmp;

		// Token: 0x0400034B RID: 843
		private Graphics target;

		// Token: 0x0400034C RID: 844
		private Graphics source;
	}
}
