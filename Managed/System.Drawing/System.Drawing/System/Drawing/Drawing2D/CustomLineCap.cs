using System;
using System.Runtime.InteropServices;

namespace System.Drawing.Drawing2D
{
	/// <summary>Encapsulates a custom user-defined line cap.</summary>
	// Token: 0x02000136 RID: 310
	public class CustomLineCap : MarshalByRefObject, ICloneable, IDisposable
	{
		// Token: 0x06000DE6 RID: 3558 RVA: 0x0001E711 File Offset: 0x0001C911
		internal static CustomLineCap CreateCustomLineCapObject(IntPtr cap)
		{
			return new CustomLineCap(cap);
		}

		// Token: 0x06000DE7 RID: 3559 RVA: 0x000025D4 File Offset: 0x000007D4
		internal CustomLineCap()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Drawing2D.CustomLineCap" /> class with the specified outline and fill.</summary>
		/// <param name="fillPath">A <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> object that defines the fill for the custom cap. </param>
		/// <param name="strokePath">A <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> object that defines the outline of the custom cap. </param>
		// Token: 0x06000DE8 RID: 3560 RVA: 0x0001E719 File Offset: 0x0001C919
		public CustomLineCap(GraphicsPath fillPath, GraphicsPath strokePath)
			: this(fillPath, strokePath, LineCap.Flat)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Drawing2D.CustomLineCap" /> class from the specified existing <see cref="T:System.Drawing.Drawing2D.LineCap" /> enumeration with the specified outline and fill.</summary>
		/// <param name="fillPath">A <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> object that defines the fill for the custom cap. </param>
		/// <param name="strokePath">A <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> object that defines the outline of the custom cap. </param>
		/// <param name="baseCap">The line cap from which to create the custom cap. </param>
		// Token: 0x06000DE9 RID: 3561 RVA: 0x0001E724 File Offset: 0x0001C924
		public CustomLineCap(GraphicsPath fillPath, GraphicsPath strokePath, LineCap baseCap)
			: this(fillPath, strokePath, baseCap, 0f)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Drawing2D.CustomLineCap" /> class from the specified existing <see cref="T:System.Drawing.Drawing2D.LineCap" /> enumeration with the specified outline, fill, and inset.</summary>
		/// <param name="fillPath">A <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> object that defines the fill for the custom cap. </param>
		/// <param name="strokePath">A <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> object that defines the outline of the custom cap. </param>
		/// <param name="baseCap">The line cap from which to create the custom cap. </param>
		/// <param name="baseInset">The distance between the cap and the line. </param>
		// Token: 0x06000DEA RID: 3562 RVA: 0x0001E734 File Offset: 0x0001C934
		public CustomLineCap(GraphicsPath fillPath, GraphicsPath strokePath, LineCap baseCap, float baseInset)
		{
			IntPtr intPtr;
			int num = GDIPlus.GdipCreateCustomLineCap(new HandleRef(fillPath, (fillPath == null) ? IntPtr.Zero : fillPath.nativePath), new HandleRef(strokePath, (strokePath == null) ? IntPtr.Zero : strokePath.nativePath), baseCap, baseInset, out intPtr);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			this.SetNativeLineCap(intPtr);
		}

		// Token: 0x06000DEB RID: 3563 RVA: 0x0001E78F File Offset: 0x0001C98F
		internal CustomLineCap(IntPtr nativeLineCap)
		{
			this.SetNativeLineCap(nativeLineCap);
		}

		// Token: 0x06000DEC RID: 3564 RVA: 0x0001E79E File Offset: 0x0001C99E
		internal void SetNativeLineCap(IntPtr handle)
		{
			if (handle == IntPtr.Zero)
			{
				throw new ArgumentNullException("handle");
			}
			this.nativeCap = new SafeCustomLineCapHandle(handle);
		}

		/// <summary>Releases all resources used by this <see cref="T:System.Drawing.Drawing2D.CustomLineCap" /> object.</summary>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000DED RID: 3565 RVA: 0x0001E7C4 File Offset: 0x0001C9C4
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Drawing.Drawing2D.CustomLineCap" /> and optionally releases the managed resources. </summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x06000DEE RID: 3566 RVA: 0x0001E7D3 File Offset: 0x0001C9D3
		protected virtual void Dispose(bool disposing)
		{
			if (this._disposed)
			{
				return;
			}
			if (disposing && this.nativeCap != null)
			{
				this.nativeCap.Dispose();
			}
			this._disposed = true;
		}

		// Token: 0x06000DEF RID: 3567 RVA: 0x0001E7FC File Offset: 0x0001C9FC
		~CustomLineCap()
		{
			this.Dispose(false);
		}

		/// <summary>Creates an exact copy of this <see cref="T:System.Drawing.Drawing2D.CustomLineCap" />.</summary>
		/// <returns>The <see cref="T:System.Drawing.Drawing2D.CustomLineCap" /> this method creates, cast as an object.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000DF0 RID: 3568 RVA: 0x0001E82C File Offset: 0x0001CA2C
		public virtual object Clone()
		{
			IntPtr intPtr;
			int num = GDIPlus.GdipCloneCustomLineCap(new HandleRef(this, this.nativeCap), out intPtr);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			return CustomLineCap.CreateCustomLineCapObject(intPtr);
		}

		/// <summary>Sets the caps used to start and end lines that make up this custom cap.</summary>
		/// <param name="startCap">The <see cref="T:System.Drawing.Drawing2D.LineCap" /> enumeration used at the beginning of a line within this cap. </param>
		/// <param name="endCap">The <see cref="T:System.Drawing.Drawing2D.LineCap" /> enumeration used at the end of a line within this cap. </param>
		// Token: 0x06000DF1 RID: 3569 RVA: 0x0001E864 File Offset: 0x0001CA64
		public void SetStrokeCaps(LineCap startCap, LineCap endCap)
		{
			int num = GDIPlus.GdipSetCustomLineCapStrokeCaps(new HandleRef(this, this.nativeCap), startCap, endCap);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		/// <summary>Gets the caps used to start and end lines that make up this custom cap.</summary>
		/// <param name="startCap">The <see cref="T:System.Drawing.Drawing2D.LineCap" /> enumeration used at the beginning of a line within this cap. </param>
		/// <param name="endCap">The <see cref="T:System.Drawing.Drawing2D.LineCap" /> enumeration used at the end of a line within this cap. </param>
		// Token: 0x06000DF2 RID: 3570 RVA: 0x0001E894 File Offset: 0x0001CA94
		public void GetStrokeCaps(out LineCap startCap, out LineCap endCap)
		{
			int num = GDIPlus.GdipGetCustomLineCapStrokeCaps(new HandleRef(this, this.nativeCap), out startCap, out endCap);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Drawing.Drawing2D.LineJoin" /> enumeration that determines how lines that compose this <see cref="T:System.Drawing.Drawing2D.CustomLineCap" /> object are joined.</summary>
		/// <returns>The <see cref="T:System.Drawing.Drawing2D.LineJoin" /> enumeration this <see cref="T:System.Drawing.Drawing2D.CustomLineCap" /> object uses to join lines.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170003BF RID: 959
		// (get) Token: 0x06000DF3 RID: 3571 RVA: 0x0001E8C4 File Offset: 0x0001CAC4
		// (set) Token: 0x06000DF4 RID: 3572 RVA: 0x0001E8F8 File Offset: 0x0001CAF8
		public LineJoin StrokeJoin
		{
			get
			{
				LineJoin lineJoin;
				int num = GDIPlus.GdipGetCustomLineCapStrokeJoin(new HandleRef(this, this.nativeCap), out lineJoin);
				if (num != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num);
				}
				return lineJoin;
			}
			set
			{
				int num = GDIPlus.GdipSetCustomLineCapStrokeJoin(new HandleRef(this, this.nativeCap), value);
				if (num != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num);
				}
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Drawing.Drawing2D.LineCap" /> enumeration on which this <see cref="T:System.Drawing.Drawing2D.CustomLineCap" /> is based.</summary>
		/// <returns>The <see cref="T:System.Drawing.Drawing2D.LineCap" /> enumeration on which this <see cref="T:System.Drawing.Drawing2D.CustomLineCap" /> is based.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x06000DF5 RID: 3573 RVA: 0x0001E928 File Offset: 0x0001CB28
		// (set) Token: 0x06000DF6 RID: 3574 RVA: 0x0001E95C File Offset: 0x0001CB5C
		public LineCap BaseCap
		{
			get
			{
				LineCap lineCap;
				int num = GDIPlus.GdipGetCustomLineCapBaseCap(new HandleRef(this, this.nativeCap), out lineCap);
				if (num != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num);
				}
				return lineCap;
			}
			set
			{
				int num = GDIPlus.GdipSetCustomLineCapBaseCap(new HandleRef(this, this.nativeCap), value);
				if (num != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num);
				}
			}
		}

		/// <summary>Gets or sets the distance between the cap and the line.</summary>
		/// <returns>The distance between the beginning of the cap and the end of the line.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x06000DF7 RID: 3575 RVA: 0x0001E98C File Offset: 0x0001CB8C
		// (set) Token: 0x06000DF8 RID: 3576 RVA: 0x0001E9C0 File Offset: 0x0001CBC0
		public float BaseInset
		{
			get
			{
				float num2;
				int num = GDIPlus.GdipGetCustomLineCapBaseInset(new HandleRef(this, this.nativeCap), out num2);
				if (num != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num);
				}
				return num2;
			}
			set
			{
				int num = GDIPlus.GdipSetCustomLineCapBaseInset(new HandleRef(this, this.nativeCap), value);
				if (num != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num);
				}
			}
		}

		/// <summary>Gets or sets the amount by which to scale this <see cref="T:System.Drawing.Drawing2D.CustomLineCap" /> Class object with respect to the width of the <see cref="T:System.Drawing.Pen" /> object.</summary>
		/// <returns>The amount by which to scale the cap.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x06000DF9 RID: 3577 RVA: 0x0001E9F0 File Offset: 0x0001CBF0
		// (set) Token: 0x06000DFA RID: 3578 RVA: 0x0001EA24 File Offset: 0x0001CC24
		public float WidthScale
		{
			get
			{
				float num2;
				int num = GDIPlus.GdipGetCustomLineCapWidthScale(new HandleRef(this, this.nativeCap), out num2);
				if (num != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num);
				}
				return num2;
			}
			set
			{
				int num = GDIPlus.GdipSetCustomLineCapWidthScale(new HandleRef(this, this.nativeCap), value);
				if (num != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num);
				}
			}
		}

		// Token: 0x04000AB1 RID: 2737
		internal SafeCustomLineCapHandle nativeCap;

		// Token: 0x04000AB2 RID: 2738
		private bool _disposed;
	}
}
