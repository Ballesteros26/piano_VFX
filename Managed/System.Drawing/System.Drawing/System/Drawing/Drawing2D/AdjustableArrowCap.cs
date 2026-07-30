using System;
using System.Runtime.InteropServices;

namespace System.Drawing.Drawing2D
{
	/// <summary>Represents an adjustable arrow-shaped line cap. This class cannot be inherited.</summary>
	// Token: 0x0200012E RID: 302
	public sealed class AdjustableArrowCap : CustomLineCap
	{
		// Token: 0x06000DCF RID: 3535 RVA: 0x0001E4DD File Offset: 0x0001C6DD
		internal AdjustableArrowCap(IntPtr nativeCap)
			: base(nativeCap)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Drawing2D.AdjustableArrowCap" /> class with the specified width and height. The arrow end caps created with this constructor are always filled.</summary>
		/// <param name="width">The width of the arrow. </param>
		/// <param name="height">The height of the arrow. </param>
		// Token: 0x06000DD0 RID: 3536 RVA: 0x0001E4E6 File Offset: 0x0001C6E6
		public AdjustableArrowCap(float width, float height)
			: this(width, height, true)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Drawing2D.AdjustableArrowCap" /> class with the specified width, height, and fill property. Whether an arrow end cap is filled depends on the argument passed to the <paramref name="isFilled" /> parameter.</summary>
		/// <param name="width">The width of the arrow. </param>
		/// <param name="height">The height of the arrow. </param>
		/// <param name="isFilled">true to fill the arrow cap; otherwise, false. </param>
		// Token: 0x06000DD1 RID: 3537 RVA: 0x0001E4F4 File Offset: 0x0001C6F4
		public AdjustableArrowCap(float width, float height, bool isFilled)
		{
			IntPtr intPtr;
			SafeNativeMethods.Gdip.CheckStatus(GDIPlus.GdipCreateAdjustableArrowCap(height, width, isFilled, out intPtr));
			base.SetNativeLineCap(intPtr);
		}

		/// <summary>Gets or sets the height of the arrow cap.</summary>
		/// <returns>The height of the arrow cap.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x06000DD2 RID: 3538 RVA: 0x0001E520 File Offset: 0x0001C720
		// (set) Token: 0x06000DD3 RID: 3539 RVA: 0x0001E54B File Offset: 0x0001C74B
		public float Height
		{
			get
			{
				float num;
				SafeNativeMethods.Gdip.CheckStatus(GDIPlus.GdipGetAdjustableArrowCapHeight(new HandleRef(this, this.nativeCap), out num));
				return num;
			}
			set
			{
				SafeNativeMethods.Gdip.CheckStatus(GDIPlus.GdipSetAdjustableArrowCapHeight(new HandleRef(this, this.nativeCap), value));
			}
		}

		/// <summary>Gets or sets the width of the arrow cap.</summary>
		/// <returns>The width, in units, of the arrow cap.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x06000DD4 RID: 3540 RVA: 0x0001E56C File Offset: 0x0001C76C
		// (set) Token: 0x06000DD5 RID: 3541 RVA: 0x0001E597 File Offset: 0x0001C797
		public float Width
		{
			get
			{
				float num;
				SafeNativeMethods.Gdip.CheckStatus(GDIPlus.GdipGetAdjustableArrowCapWidth(new HandleRef(this, this.nativeCap), out num));
				return num;
			}
			set
			{
				SafeNativeMethods.Gdip.CheckStatus(GDIPlus.GdipSetAdjustableArrowCapWidth(new HandleRef(this, this.nativeCap), value));
			}
		}

		/// <summary>Gets or sets the number of units between the outline of the arrow cap and the fill.</summary>
		/// <returns>The number of units between the outline of the arrow cap and the fill of the arrow cap.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x06000DD6 RID: 3542 RVA: 0x0001E5B8 File Offset: 0x0001C7B8
		// (set) Token: 0x06000DD7 RID: 3543 RVA: 0x0001E5E3 File Offset: 0x0001C7E3
		public float MiddleInset
		{
			get
			{
				float num;
				SafeNativeMethods.Gdip.CheckStatus(GDIPlus.GdipGetAdjustableArrowCapMiddleInset(new HandleRef(this, this.nativeCap), out num));
				return num;
			}
			set
			{
				SafeNativeMethods.Gdip.CheckStatus(GDIPlus.GdipSetAdjustableArrowCapMiddleInset(new HandleRef(this, this.nativeCap), value));
			}
		}

		/// <summary>Gets or sets whether the arrow cap is filled.</summary>
		/// <returns>This property is true if the arrow cap is filled; otherwise, false.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170003BA RID: 954
		// (get) Token: 0x06000DD8 RID: 3544 RVA: 0x0001E604 File Offset: 0x0001C804
		// (set) Token: 0x06000DD9 RID: 3545 RVA: 0x0001E62F File Offset: 0x0001C82F
		public bool Filled
		{
			get
			{
				bool flag;
				SafeNativeMethods.Gdip.CheckStatus(GDIPlus.GdipGetAdjustableArrowCapFillState(new HandleRef(this, this.nativeCap), out flag));
				return flag;
			}
			set
			{
				SafeNativeMethods.Gdip.CheckStatus(GDIPlus.GdipSetAdjustableArrowCapFillState(new HandleRef(this, this.nativeCap), value));
			}
		}
	}
}
