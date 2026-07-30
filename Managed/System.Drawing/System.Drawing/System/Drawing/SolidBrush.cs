using System;
using System.Runtime.InteropServices;

namespace System.Drawing
{
	/// <summary>Defines a brush of a single color. Brushes are used to fill graphics shapes, such as rectangles, ellipses, pies, polygons, and paths. This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000032 RID: 50
	public sealed class SolidBrush : Brush
	{
		/// <summary>Initializes a new <see cref="T:System.Drawing.SolidBrush" /> object of the specified color.</summary>
		/// <param name="color">A <see cref="T:System.Drawing.Color" /> structure that represents the color of this brush. </param>
		// Token: 0x060000C1 RID: 193 RVA: 0x000037B8 File Offset: 0x000019B8
		public SolidBrush(Color color)
		{
			this._color = color;
			IntPtr zero = IntPtr.Zero;
			SafeNativeMethods.Gdip.CheckStatus(GDIPlus.GdipCreateSolidFill(this._color.ToArgb(), out zero));
			base.SetNativeBrushInternal(zero);
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00003801 File Offset: 0x00001A01
		internal SolidBrush(Color color, bool immutable)
			: this(color)
		{
			this._immutable = immutable;
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00003811 File Offset: 0x00001A11
		internal SolidBrush(IntPtr nativeBrush)
		{
			base.SetNativeBrushInternal(nativeBrush);
		}

		/// <summary>Creates an exact copy of this <see cref="T:System.Drawing.SolidBrush" /> object.</summary>
		/// <returns>The <see cref="T:System.Drawing.SolidBrush" /> object that this method creates.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060000C4 RID: 196 RVA: 0x0000382C File Offset: 0x00001A2C
		public override object Clone()
		{
			IntPtr zero = IntPtr.Zero;
			SafeNativeMethods.Gdip.CheckStatus(GDIPlus.GdipCloneBrush(new HandleRef(this, base.NativeBrush), out zero));
			return new SolidBrush(zero);
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x0000385D File Offset: 0x00001A5D
		protected override void Dispose(bool disposing)
		{
			if (!disposing)
			{
				this._immutable = false;
			}
			else if (this._immutable)
			{
				throw new ArgumentException(SR.Format("Changes cannot be made to {0} because permissions are not valid.", new object[] { "Brush" }));
			}
			base.Dispose(disposing);
		}

		/// <summary>Gets or sets the color of this <see cref="T:System.Drawing.SolidBrush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> structure that represents the color of this brush.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Drawing.SolidBrush.Color" /> property is set on an immutable <see cref="T:System.Drawing.SolidBrush" />.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x00003898 File Offset: 0x00001A98
		// (set) Token: 0x060000C7 RID: 199 RVA: 0x000038E4 File Offset: 0x00001AE4
		public Color Color
		{
			get
			{
				if (this._color == Color.Empty)
				{
					int num;
					SafeNativeMethods.Gdip.CheckStatus(GDIPlus.GdipGetSolidFillColor(new HandleRef(this, base.NativeBrush), out num));
					this._color = Color.FromArgb(num);
				}
				return this._color;
			}
			set
			{
				if (this._immutable)
				{
					throw new ArgumentException(SR.Format("Changes cannot be made to {0} because permissions are not valid.", new object[] { "Brush" }));
				}
				if (this._color != value)
				{
					Color color = this._color;
					this.InternalSetColor(value);
				}
			}
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00003933 File Offset: 0x00001B33
		private void InternalSetColor(Color value)
		{
			SafeNativeMethods.Gdip.CheckStatus(GDIPlus.GdipSetSolidFillColor(new HandleRef(this, base.NativeBrush), value.ToArgb()));
			this._color = value;
		}

		// Token: 0x04000294 RID: 660
		private Color _color = Color.Empty;

		// Token: 0x04000295 RID: 661
		private bool _immutable;
	}
}
