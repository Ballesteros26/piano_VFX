using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace System.Drawing
{
	/// <summary>Defines objects used to fill the interiors of graphical shapes such as rectangles, ellipses, pies, polygons, and paths.</summary>
	/// <filterpriority>1</filterpriority>
	/// <completionlist cref="T:System.Drawing.Brushes" />
	// Token: 0x02000013 RID: 19
	public abstract class Brush : MarshalByRefObject, ICloneable, IDisposable
	{
		/// <summary>When overridden in a derived class, creates an exact copy of this <see cref="T:System.Drawing.Brush" />.</summary>
		/// <returns>The new <see cref="T:System.Drawing.Brush" /> that this method creates.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000029 RID: 41
		public abstract object Clone();

		/// <summary>In a derived class, sets a reference to a GDI+ brush object. </summary>
		/// <param name="brush">A pointer to the GDI+ brush object.</param>
		// Token: 0x0600002A RID: 42 RVA: 0x00002503 File Offset: 0x00000703
		protected internal void SetNativeBrush(IntPtr brush)
		{
			this.SetNativeBrushInternal(brush);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x0000250C File Offset: 0x0000070C
		internal void SetNativeBrushInternal(IntPtr brush)
		{
			this._nativeBrush = brush;
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600002C RID: 44 RVA: 0x00002515 File Offset: 0x00000715
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		internal IntPtr NativeBrush
		{
			get
			{
				return this._nativeBrush;
			}
		}

		/// <summary>Releases all resources used by this <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x0600002D RID: 45 RVA: 0x0000251D File Offset: 0x0000071D
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Drawing.Brush" /> and optionally releases the managed resources. </summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
		// Token: 0x0600002E RID: 46 RVA: 0x0000252C File Offset: 0x0000072C
		protected virtual void Dispose(bool disposing)
		{
			if (this._nativeBrush != IntPtr.Zero)
			{
				try
				{
					GDIPlus.GdipDeleteBrush(new HandleRef(this, this._nativeBrush));
				}
				catch (Exception ex) when (!ClientUtils.IsSecurityOrCriticalException(ex))
				{
				}
				finally
				{
					this._nativeBrush = IntPtr.Zero;
				}
			}
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000025A4 File Offset: 0x000007A4
		~Brush()
		{
			this.Dispose(false);
		}

		// Token: 0x04000095 RID: 149
		private IntPtr _nativeBrush;
	}
}
