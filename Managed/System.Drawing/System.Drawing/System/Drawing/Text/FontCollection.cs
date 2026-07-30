using System;
using System.Runtime.InteropServices;

namespace System.Drawing.Text
{
	/// <summary>Provides a base class for installed and private font collections. </summary>
	// Token: 0x020000AD RID: 173
	public abstract class FontCollection : IDisposable
	{
		// Token: 0x06000A3A RID: 2618 RVA: 0x00016293 File Offset: 0x00014493
		internal FontCollection()
		{
			this._nativeFontCollection = IntPtr.Zero;
		}

		/// <summary>Releases all resources used by this <see cref="T:System.Drawing.Text.FontCollection" />.</summary>
		// Token: 0x06000A3B RID: 2619 RVA: 0x000162A6 File Offset: 0x000144A6
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Drawing.Text.FontCollection" /> and optionally releases the managed resources. </summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x06000A3C RID: 2620 RVA: 0x00002CE2 File Offset: 0x00000EE2
		protected virtual void Dispose(bool disposing)
		{
		}

		/// <summary>Gets the array of <see cref="T:System.Drawing.FontFamily" /> objects associated with this <see cref="T:System.Drawing.Text.FontCollection" />. </summary>
		/// <returns>An array of <see cref="T:System.Drawing.FontFamily" /> objects.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170002CA RID: 714
		// (get) Token: 0x06000A3D RID: 2621 RVA: 0x000162B8 File Offset: 0x000144B8
		public FontFamily[] Families
		{
			get
			{
				int num = 0;
				SafeNativeMethods.Gdip.CheckStatus(GDIPlus.GdipGetFontCollectionFamilyCount(new HandleRef(this, this._nativeFontCollection), out num));
				IntPtr[] array = new IntPtr[num];
				int num2 = 0;
				SafeNativeMethods.Gdip.CheckStatus(GDIPlus.GdipGetFontCollectionFamilyList(new HandleRef(this, this._nativeFontCollection), num, array, out num2));
				FontFamily[] array2 = new FontFamily[num2];
				for (int i = 0; i < num2; i++)
				{
					IntPtr intPtr;
					GDIPlus.GdipCloneFontFamily(new HandleRef(null, array[i]), out intPtr);
					array2[i] = new FontFamily(intPtr);
				}
				return array2;
			}
		}

		// Token: 0x06000A3E RID: 2622 RVA: 0x00016338 File Offset: 0x00014538
		~FontCollection()
		{
			this.Dispose(false);
		}

		// Token: 0x04000632 RID: 1586
		internal IntPtr _nativeFontCollection;
	}
}
