using System;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;

namespace System.Drawing
{
	/// <summary>Describes the interior of a graphics shape composed of rectangles and paths. This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000082 RID: 130
	public sealed class Region : MarshalByRefObject, IDisposable
	{
		/// <summary>Initializes a new <see cref="T:System.Drawing.Region" />.</summary>
		// Token: 0x0600068A RID: 1674 RVA: 0x00013151 File Offset: 0x00011351
		public Region()
		{
			GDIPlus.CheckStatus(GDIPlus.GdipCreateRegion(out this.nativeRegion));
		}

		// Token: 0x0600068B RID: 1675 RVA: 0x00013174 File Offset: 0x00011374
		internal Region(IntPtr native)
		{
			this.nativeRegion = native;
		}

		/// <summary>Initializes a new <see cref="T:System.Drawing.Region" /> with the specified <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</summary>
		/// <param name="path">A <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> that defines the new <see cref="T:System.Drawing.Region" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> isnull.</exception>
		// Token: 0x0600068C RID: 1676 RVA: 0x0001318E File Offset: 0x0001138E
		public Region(GraphicsPath path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipCreateRegionPath(path.nativePath, out this.nativeRegion));
		}

		/// <summary>Initializes a new <see cref="T:System.Drawing.Region" /> from the specified <see cref="T:System.Drawing.Rectangle" /> structure.</summary>
		/// <param name="rect">A <see cref="T:System.Drawing.Rectangle" /> structure that defines the interior of the new <see cref="T:System.Drawing.Region" />. </param>
		// Token: 0x0600068D RID: 1677 RVA: 0x000131C5 File Offset: 0x000113C5
		public Region(Rectangle rect)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipCreateRegionRectI(ref rect, out this.nativeRegion));
		}

		/// <summary>Initializes a new <see cref="T:System.Drawing.Region" /> from the specified <see cref="T:System.Drawing.RectangleF" /> structure.</summary>
		/// <param name="rect">A <see cref="T:System.Drawing.RectangleF" /> structure that defines the interior of the new <see cref="T:System.Drawing.Region" />. </param>
		// Token: 0x0600068E RID: 1678 RVA: 0x000131EA File Offset: 0x000113EA
		public Region(RectangleF rect)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipCreateRegionRect(ref rect, out this.nativeRegion));
		}

		/// <summary>Initializes a new <see cref="T:System.Drawing.Region" /> from the specified data.</summary>
		/// <param name="rgnData">A <see cref="T:System.Drawing.Drawing2D.RegionData" /> that defines the interior of the new <see cref="T:System.Drawing.Region" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="rgnData" /> isnull.</exception>
		// Token: 0x0600068F RID: 1679 RVA: 0x00013210 File Offset: 0x00011410
		public Region(RegionData rgnData)
		{
			if (rgnData == null)
			{
				throw new ArgumentNullException("rgnData");
			}
			if (rgnData.Data.Length == 0)
			{
				throw new ArgumentException("rgnData");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipCreateRegionRgnData(rgnData.Data, rgnData.Data.Length, out this.nativeRegion));
		}

		/// <summary>Updates this <see cref="T:System.Drawing.Region" /> to the union of itself and the specified <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</summary>
		/// <param name="path">The <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> to unite with this <see cref="T:System.Drawing.Region" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000690 RID: 1680 RVA: 0x0001326E File Offset: 0x0001146E
		public void Union(GraphicsPath path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipCombineRegionPath(this.nativeRegion, path.nativePath, CombineMode.Union));
		}

		/// <summary>Updates this <see cref="T:System.Drawing.Region" /> to the union of itself and the specified <see cref="T:System.Drawing.Rectangle" /> structure.</summary>
		/// <param name="rect">The <see cref="T:System.Drawing.Rectangle" /> structure to unite with this <see cref="T:System.Drawing.Region" />. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000691 RID: 1681 RVA: 0x00013295 File Offset: 0x00011495
		public void Union(Rectangle rect)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipCombineRegionRectI(this.nativeRegion, ref rect, CombineMode.Union));
		}

		/// <summary>Updates this <see cref="T:System.Drawing.Region" /> to the union of itself and the specified <see cref="T:System.Drawing.RectangleF" /> structure.</summary>
		/// <param name="rect">The <see cref="T:System.Drawing.RectangleF" /> structure to unite with this <see cref="T:System.Drawing.Region" />. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000692 RID: 1682 RVA: 0x000132AA File Offset: 0x000114AA
		public void Union(RectangleF rect)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipCombineRegionRect(this.nativeRegion, ref rect, CombineMode.Union));
		}

		/// <summary>Updates this <see cref="T:System.Drawing.Region" /> to the union of itself and the specified <see cref="T:System.Drawing.Region" />.</summary>
		/// <param name="region">The <see cref="T:System.Drawing.Region" /> to unite with this <see cref="T:System.Drawing.Region" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="region" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000693 RID: 1683 RVA: 0x000132BF File Offset: 0x000114BF
		public void Union(Region region)
		{
			if (region == null)
			{
				throw new ArgumentNullException("region");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipCombineRegionRegion(this.nativeRegion, region.NativeObject, CombineMode.Union));
		}

		/// <summary>Updates this <see cref="T:System.Drawing.Region" /> to the intersection of itself with the specified <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</summary>
		/// <param name="path">The <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> to intersect with this <see cref="T:System.Drawing.Region" />. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000694 RID: 1684 RVA: 0x000132E6 File Offset: 0x000114E6
		public void Intersect(GraphicsPath path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipCombineRegionPath(this.nativeRegion, path.nativePath, CombineMode.Intersect));
		}

		/// <summary>Updates this <see cref="T:System.Drawing.Region" /> to the intersection of itself with the specified <see cref="T:System.Drawing.Rectangle" /> structure.</summary>
		/// <param name="rect">The <see cref="T:System.Drawing.Rectangle" /> structure to intersect with this <see cref="T:System.Drawing.Region" />. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000695 RID: 1685 RVA: 0x0001330D File Offset: 0x0001150D
		public void Intersect(Rectangle rect)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipCombineRegionRectI(this.nativeRegion, ref rect, CombineMode.Intersect));
		}

		/// <summary>Updates this <see cref="T:System.Drawing.Region" /> to the intersection of itself with the specified <see cref="T:System.Drawing.RectangleF" /> structure.</summary>
		/// <param name="rect">The <see cref="T:System.Drawing.RectangleF" /> structure to intersect with this <see cref="T:System.Drawing.Region" />. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000696 RID: 1686 RVA: 0x00013322 File Offset: 0x00011522
		public void Intersect(RectangleF rect)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipCombineRegionRect(this.nativeRegion, ref rect, CombineMode.Intersect));
		}

		/// <summary>Updates this <see cref="T:System.Drawing.Region" /> to the intersection of itself with the specified <see cref="T:System.Drawing.Region" />.</summary>
		/// <param name="region">The <see cref="T:System.Drawing.Region" /> to intersect with this <see cref="T:System.Drawing.Region" />. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000697 RID: 1687 RVA: 0x00013337 File Offset: 0x00011537
		public void Intersect(Region region)
		{
			if (region == null)
			{
				throw new ArgumentNullException("region");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipCombineRegionRegion(this.nativeRegion, region.NativeObject, CombineMode.Intersect));
		}

		/// <summary>Updates this <see cref="T:System.Drawing.Region" /> to contain the portion of the specified <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> that does not intersect with this <see cref="T:System.Drawing.Region" />.</summary>
		/// <param name="path">The <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> to complement this <see cref="T:System.Drawing.Region" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> isnull.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000698 RID: 1688 RVA: 0x0001335E File Offset: 0x0001155E
		public void Complement(GraphicsPath path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipCombineRegionPath(this.nativeRegion, path.nativePath, CombineMode.Complement));
		}

		/// <summary>Updates this <see cref="T:System.Drawing.Region" /> to contain the portion of the specified <see cref="T:System.Drawing.Rectangle" /> structure that does not intersect with this <see cref="T:System.Drawing.Region" />.</summary>
		/// <param name="rect">The <see cref="T:System.Drawing.Rectangle" /> structure to complement this <see cref="T:System.Drawing.Region" />. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000699 RID: 1689 RVA: 0x00013385 File Offset: 0x00011585
		public void Complement(Rectangle rect)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipCombineRegionRectI(this.nativeRegion, ref rect, CombineMode.Complement));
		}

		/// <summary>Updates this <see cref="T:System.Drawing.Region" /> to contain the portion of the specified <see cref="T:System.Drawing.RectangleF" /> structure that does not intersect with this <see cref="T:System.Drawing.Region" />.</summary>
		/// <param name="rect">The <see cref="T:System.Drawing.RectangleF" /> structure to complement this <see cref="T:System.Drawing.Region" />. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600069A RID: 1690 RVA: 0x0001339A File Offset: 0x0001159A
		public void Complement(RectangleF rect)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipCombineRegionRect(this.nativeRegion, ref rect, CombineMode.Complement));
		}

		/// <summary>Updates this <see cref="T:System.Drawing.Region" /> to contain the portion of the specified <see cref="T:System.Drawing.Region" /> that does not intersect with this <see cref="T:System.Drawing.Region" />.</summary>
		/// <param name="region">The <see cref="T:System.Drawing.Region" /> object to complement this <see cref="T:System.Drawing.Region" /> object. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="region" /> isnull.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600069B RID: 1691 RVA: 0x000133AF File Offset: 0x000115AF
		public void Complement(Region region)
		{
			if (region == null)
			{
				throw new ArgumentNullException("region");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipCombineRegionRegion(this.nativeRegion, region.NativeObject, CombineMode.Complement));
		}

		/// <summary>Updates this <see cref="T:System.Drawing.Region" /> to contain only the portion of its interior that does not intersect with the specified <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</summary>
		/// <param name="path">The <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> to exclude from this <see cref="T:System.Drawing.Region" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600069C RID: 1692 RVA: 0x000133D6 File Offset: 0x000115D6
		public void Exclude(GraphicsPath path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipCombineRegionPath(this.nativeRegion, path.nativePath, CombineMode.Exclude));
		}

		/// <summary>Updates this <see cref="T:System.Drawing.Region" /> to contain only the portion of its interior that does not intersect with the specified <see cref="T:System.Drawing.Rectangle" /> structure.</summary>
		/// <param name="rect">The <see cref="T:System.Drawing.Rectangle" /> structure to exclude from this <see cref="T:System.Drawing.Region" />. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600069D RID: 1693 RVA: 0x000133FD File Offset: 0x000115FD
		public void Exclude(Rectangle rect)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipCombineRegionRectI(this.nativeRegion, ref rect, CombineMode.Exclude));
		}

		/// <summary>Updates this <see cref="T:System.Drawing.Region" /> to contain only the portion of its interior that does not intersect with the specified <see cref="T:System.Drawing.RectangleF" /> structure.</summary>
		/// <param name="rect">The <see cref="T:System.Drawing.RectangleF" /> structure to exclude from this <see cref="T:System.Drawing.Region" />. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600069E RID: 1694 RVA: 0x00013412 File Offset: 0x00011612
		public void Exclude(RectangleF rect)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipCombineRegionRect(this.nativeRegion, ref rect, CombineMode.Exclude));
		}

		/// <summary>Updates this <see cref="T:System.Drawing.Region" /> to contain only the portion of its interior that does not intersect with the specified <see cref="T:System.Drawing.Region" />.</summary>
		/// <param name="region">The <see cref="T:System.Drawing.Region" /> to exclude from this <see cref="T:System.Drawing.Region" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="region" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600069F RID: 1695 RVA: 0x00013427 File Offset: 0x00011627
		public void Exclude(Region region)
		{
			if (region == null)
			{
				throw new ArgumentNullException("region");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipCombineRegionRegion(this.nativeRegion, region.NativeObject, CombineMode.Exclude));
		}

		/// <summary>Updates this <see cref="T:System.Drawing.Region" /> to the union minus the intersection of itself with the specified <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</summary>
		/// <param name="path">The <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> to <see cref="Overload:System.Drawing.Region.Xor" /> with this <see cref="T:System.Drawing.Region" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060006A0 RID: 1696 RVA: 0x0001344E File Offset: 0x0001164E
		public void Xor(GraphicsPath path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipCombineRegionPath(this.nativeRegion, path.nativePath, CombineMode.Xor));
		}

		/// <summary>Updates this <see cref="T:System.Drawing.Region" /> to the union minus the intersection of itself with the specified <see cref="T:System.Drawing.Rectangle" /> structure.</summary>
		/// <param name="rect">The <see cref="T:System.Drawing.Rectangle" /> structure to <see cref="Overload:System.Drawing.Region.Xor" /> with this <see cref="T:System.Drawing.Region" />. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060006A1 RID: 1697 RVA: 0x00013475 File Offset: 0x00011675
		public void Xor(Rectangle rect)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipCombineRegionRectI(this.nativeRegion, ref rect, CombineMode.Xor));
		}

		/// <summary>Updates this <see cref="T:System.Drawing.Region" /> to the union minus the intersection of itself with the specified <see cref="T:System.Drawing.RectangleF" /> structure.</summary>
		/// <param name="rect">The <see cref="T:System.Drawing.RectangleF" /> structure to <see cref="M:System.Drawing.Region.Xor(System.Drawing.Drawing2D.GraphicsPath)" /> with this <see cref="T:System.Drawing.Region" />. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060006A2 RID: 1698 RVA: 0x0001348A File Offset: 0x0001168A
		public void Xor(RectangleF rect)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipCombineRegionRect(this.nativeRegion, ref rect, CombineMode.Xor));
		}

		/// <summary>Updates this <see cref="T:System.Drawing.Region" /> to the union minus the intersection of itself with the specified <see cref="T:System.Drawing.Region" />.</summary>
		/// <param name="region">The <see cref="T:System.Drawing.Region" /> to <see cref="Overload:System.Drawing.Region.Xor" /> with this <see cref="T:System.Drawing.Region" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="region" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060006A3 RID: 1699 RVA: 0x0001349F File Offset: 0x0001169F
		public void Xor(Region region)
		{
			if (region == null)
			{
				throw new ArgumentNullException("region");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipCombineRegionRegion(this.nativeRegion, region.NativeObject, CombineMode.Xor));
		}

		/// <summary>Gets a <see cref="T:System.Drawing.RectangleF" /> structure that represents a rectangle that bounds this <see cref="T:System.Drawing.Region" /> on the drawing surface of a <see cref="T:System.Drawing.Graphics" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.RectangleF" /> structure that represents the bounding rectangle for this <see cref="T:System.Drawing.Region" /> on the specified drawing surface.</returns>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> on which this <see cref="T:System.Drawing.Region" /> is drawn. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="g" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060006A4 RID: 1700 RVA: 0x000134C8 File Offset: 0x000116C8
		public RectangleF GetBounds(Graphics g)
		{
			if (g == null)
			{
				throw new ArgumentNullException("g");
			}
			RectangleF rectangleF = default(Rectangle);
			GDIPlus.CheckStatus(GDIPlus.GdipGetRegionBounds(this.nativeRegion, g.NativeObject, ref rectangleF));
			return rectangleF;
		}

		/// <summary>Offsets the coordinates of this <see cref="T:System.Drawing.Region" /> by the specified amount.</summary>
		/// <param name="dx">The amount to offset this <see cref="T:System.Drawing.Region" /> horizontally. </param>
		/// <param name="dy">The amount to offset this <see cref="T:System.Drawing.Region" /> vertically. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060006A5 RID: 1701 RVA: 0x0001350B File Offset: 0x0001170B
		public void Translate(int dx, int dy)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipTranslateRegionI(this.nativeRegion, dx, dy));
		}

		/// <summary>Offsets the coordinates of this <see cref="T:System.Drawing.Region" /> by the specified amount.</summary>
		/// <param name="dx">The amount to offset this <see cref="T:System.Drawing.Region" /> horizontally. </param>
		/// <param name="dy">The amount to offset this <see cref="T:System.Drawing.Region" /> vertically. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060006A6 RID: 1702 RVA: 0x0001351F File Offset: 0x0001171F
		public void Translate(float dx, float dy)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipTranslateRegion(this.nativeRegion, dx, dy));
		}

		/// <summary>Tests whether the specified point is contained within this <see cref="T:System.Drawing.Region" /> object when drawn using the specified <see cref="T:System.Drawing.Graphics" /> object.</summary>
		/// <returns>true when the specified point is contained within this <see cref="T:System.Drawing.Region" />; otherwise, false.</returns>
		/// <param name="x">The x-coordinate of the point to test. </param>
		/// <param name="y">The y-coordinate of the point to test. </param>
		/// <param name="g">A <see cref="T:System.Drawing.Graphics" /> that represents a graphics context. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060006A7 RID: 1703 RVA: 0x00013534 File Offset: 0x00011734
		public bool IsVisible(int x, int y, Graphics g)
		{
			IntPtr intPtr = ((g == null) ? IntPtr.Zero : g.NativeObject);
			bool flag;
			GDIPlus.CheckStatus(GDIPlus.GdipIsVisibleRegionPointI(this.nativeRegion, x, y, intPtr, out flag));
			return flag;
		}

		/// <summary>Tests whether any portion of the specified rectangle is contained within this <see cref="T:System.Drawing.Region" />.</summary>
		/// <returns>true when any portion of the specified rectangle is contained within this <see cref="T:System.Drawing.Region" />; otherwise, false.</returns>
		/// <param name="x">The x-coordinate of the upper-left corner of the rectangle to test. </param>
		/// <param name="y">The y-coordinate of the upper-left corner of the rectangle to test. </param>
		/// <param name="width">The width of the rectangle to test. </param>
		/// <param name="height">The height of the rectangle to test. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060006A8 RID: 1704 RVA: 0x00013568 File Offset: 0x00011768
		public bool IsVisible(int x, int y, int width, int height)
		{
			bool flag;
			GDIPlus.CheckStatus(GDIPlus.GdipIsVisibleRegionRectI(this.nativeRegion, x, y, width, height, IntPtr.Zero, out flag));
			return flag;
		}

		/// <summary>Tests whether any portion of the specified rectangle is contained within this <see cref="T:System.Drawing.Region" /> when drawn using the specified <see cref="T:System.Drawing.Graphics" />.</summary>
		/// <returns>true when any portion of the specified rectangle is contained within this <see cref="T:System.Drawing.Region" />; otherwise, false.</returns>
		/// <param name="x">The x-coordinate of the upper-left corner of the rectangle to test. </param>
		/// <param name="y">The y-coordinate of the upper-left corner of the rectangle to test. </param>
		/// <param name="width">The width of the rectangle to test. </param>
		/// <param name="height">The height of the rectangle to test. </param>
		/// <param name="g">A <see cref="T:System.Drawing.Graphics" /> that represents a graphics context. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060006A9 RID: 1705 RVA: 0x00013594 File Offset: 0x00011794
		public bool IsVisible(int x, int y, int width, int height, Graphics g)
		{
			IntPtr intPtr = ((g == null) ? IntPtr.Zero : g.NativeObject);
			bool flag;
			GDIPlus.CheckStatus(GDIPlus.GdipIsVisibleRegionRectI(this.nativeRegion, x, y, width, height, intPtr, out flag));
			return flag;
		}

		/// <summary>Tests whether the specified <see cref="T:System.Drawing.Point" /> structure is contained within this <see cref="T:System.Drawing.Region" />.</summary>
		/// <returns>true when <paramref name="point" /> is contained within this <see cref="T:System.Drawing.Region" />; otherwise, false.</returns>
		/// <param name="point">The <see cref="T:System.Drawing.Point" /> structure to test. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060006AA RID: 1706 RVA: 0x000135D0 File Offset: 0x000117D0
		public bool IsVisible(Point point)
		{
			bool flag;
			GDIPlus.CheckStatus(GDIPlus.GdipIsVisibleRegionPointI(this.nativeRegion, point.X, point.Y, IntPtr.Zero, out flag));
			return flag;
		}

		/// <summary>Tests whether the specified <see cref="T:System.Drawing.PointF" /> structure is contained within this <see cref="T:System.Drawing.Region" />.</summary>
		/// <returns>true when <paramref name="point" /> is contained within this <see cref="T:System.Drawing.Region" />; otherwise, false.</returns>
		/// <param name="point">The <see cref="T:System.Drawing.PointF" /> structure to test. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060006AB RID: 1707 RVA: 0x00013604 File Offset: 0x00011804
		public bool IsVisible(PointF point)
		{
			bool flag;
			GDIPlus.CheckStatus(GDIPlus.GdipIsVisibleRegionPoint(this.nativeRegion, point.X, point.Y, IntPtr.Zero, out flag));
			return flag;
		}

		/// <summary>Tests whether the specified <see cref="T:System.Drawing.Point" /> structure is contained within this <see cref="T:System.Drawing.Region" /> when drawn using the specified <see cref="T:System.Drawing.Graphics" />.</summary>
		/// <returns>true when <paramref name="point" /> is contained within this <see cref="T:System.Drawing.Region" />; otherwise, false.</returns>
		/// <param name="point">The <see cref="T:System.Drawing.Point" /> structure to test. </param>
		/// <param name="g">A <see cref="T:System.Drawing.Graphics" /> that represents a graphics context. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060006AC RID: 1708 RVA: 0x00013638 File Offset: 0x00011838
		public bool IsVisible(Point point, Graphics g)
		{
			IntPtr intPtr = ((g == null) ? IntPtr.Zero : g.NativeObject);
			bool flag;
			GDIPlus.CheckStatus(GDIPlus.GdipIsVisibleRegionPointI(this.nativeRegion, point.X, point.Y, intPtr, out flag));
			return flag;
		}

		/// <summary>Tests whether the specified <see cref="T:System.Drawing.PointF" /> structure is contained within this <see cref="T:System.Drawing.Region" /> when drawn using the specified <see cref="T:System.Drawing.Graphics" />.</summary>
		/// <returns>true when <paramref name="point" /> is contained within this <see cref="T:System.Drawing.Region" />; otherwise, false.</returns>
		/// <param name="point">The <see cref="T:System.Drawing.PointF" /> structure to test. </param>
		/// <param name="g">A <see cref="T:System.Drawing.Graphics" /> that represents a graphics context. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060006AD RID: 1709 RVA: 0x00013678 File Offset: 0x00011878
		public bool IsVisible(PointF point, Graphics g)
		{
			IntPtr intPtr = ((g == null) ? IntPtr.Zero : g.NativeObject);
			bool flag;
			GDIPlus.CheckStatus(GDIPlus.GdipIsVisibleRegionPoint(this.nativeRegion, point.X, point.Y, intPtr, out flag));
			return flag;
		}

		/// <summary>Tests whether any portion of the specified <see cref="T:System.Drawing.Rectangle" /> structure is contained within this <see cref="T:System.Drawing.Region" />.</summary>
		/// <returns>This method returns true when any portion of <paramref name="rect" /> is contained within this <see cref="T:System.Drawing.Region" />; otherwise, false.</returns>
		/// <param name="rect">The <see cref="T:System.Drawing.Rectangle" /> structure to test. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060006AE RID: 1710 RVA: 0x000136B8 File Offset: 0x000118B8
		public bool IsVisible(Rectangle rect)
		{
			bool flag;
			GDIPlus.CheckStatus(GDIPlus.GdipIsVisibleRegionRectI(this.nativeRegion, rect.X, rect.Y, rect.Width, rect.Height, IntPtr.Zero, out flag));
			return flag;
		}

		/// <summary>Tests whether any portion of the specified <see cref="T:System.Drawing.RectangleF" /> structure is contained within this <see cref="T:System.Drawing.Region" />.</summary>
		/// <returns>true when any portion of <paramref name="rect" /> is contained within this <see cref="T:System.Drawing.Region" />; otherwise, false.</returns>
		/// <param name="rect">The <see cref="T:System.Drawing.RectangleF" /> structure to test. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060006AF RID: 1711 RVA: 0x000136FC File Offset: 0x000118FC
		public bool IsVisible(RectangleF rect)
		{
			bool flag;
			GDIPlus.CheckStatus(GDIPlus.GdipIsVisibleRegionRect(this.nativeRegion, rect.X, rect.Y, rect.Width, rect.Height, IntPtr.Zero, out flag));
			return flag;
		}

		/// <summary>Tests whether any portion of the specified <see cref="T:System.Drawing.Rectangle" /> structure is contained within this <see cref="T:System.Drawing.Region" /> when drawn using the specified <see cref="T:System.Drawing.Graphics" />.</summary>
		/// <returns>true when any portion of the <paramref name="rect" /> is contained within this <see cref="T:System.Drawing.Region" />; otherwise, false.</returns>
		/// <param name="rect">The <see cref="T:System.Drawing.Rectangle" /> structure to test. </param>
		/// <param name="g">A <see cref="T:System.Drawing.Graphics" /> that represents a graphics context. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060006B0 RID: 1712 RVA: 0x00013740 File Offset: 0x00011940
		public bool IsVisible(Rectangle rect, Graphics g)
		{
			IntPtr intPtr = ((g == null) ? IntPtr.Zero : g.NativeObject);
			bool flag;
			GDIPlus.CheckStatus(GDIPlus.GdipIsVisibleRegionRectI(this.nativeRegion, rect.X, rect.Y, rect.Width, rect.Height, intPtr, out flag));
			return flag;
		}

		/// <summary>Tests whether any portion of the specified <see cref="T:System.Drawing.RectangleF" /> structure is contained within this <see cref="T:System.Drawing.Region" /> when drawn using the specified <see cref="T:System.Drawing.Graphics" />.</summary>
		/// <returns>true when <paramref name="rect" /> is contained within this <see cref="T:System.Drawing.Region" />; otherwise, false.</returns>
		/// <param name="rect">The <see cref="T:System.Drawing.RectangleF" /> structure to test. </param>
		/// <param name="g">A <see cref="T:System.Drawing.Graphics" /> that represents a graphics context. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060006B1 RID: 1713 RVA: 0x00013790 File Offset: 0x00011990
		public bool IsVisible(RectangleF rect, Graphics g)
		{
			IntPtr intPtr = ((g == null) ? IntPtr.Zero : g.NativeObject);
			bool flag;
			GDIPlus.CheckStatus(GDIPlus.GdipIsVisibleRegionRect(this.nativeRegion, rect.X, rect.Y, rect.Width, rect.Height, intPtr, out flag));
			return flag;
		}

		/// <summary>Tests whether the specified point is contained within this <see cref="T:System.Drawing.Region" />.</summary>
		/// <returns>true when the specified point is contained within this <see cref="T:System.Drawing.Region" />; otherwise, false.</returns>
		/// <param name="x">The x-coordinate of the point to test. </param>
		/// <param name="y">The y-coordinate of the point to test. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060006B2 RID: 1714 RVA: 0x000137E0 File Offset: 0x000119E0
		public bool IsVisible(float x, float y)
		{
			bool flag;
			GDIPlus.CheckStatus(GDIPlus.GdipIsVisibleRegionPoint(this.nativeRegion, x, y, IntPtr.Zero, out flag));
			return flag;
		}

		/// <summary>Tests whether the specified point is contained within this <see cref="T:System.Drawing.Region" /> when drawn using the specified <see cref="T:System.Drawing.Graphics" />.</summary>
		/// <returns>true when the specified point is contained within this <see cref="T:System.Drawing.Region" />; otherwise, false.</returns>
		/// <param name="x">The x-coordinate of the point to test. </param>
		/// <param name="y">The y-coordinate of the point to test. </param>
		/// <param name="g">A <see cref="T:System.Drawing.Graphics" /> that represents a graphics context. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060006B3 RID: 1715 RVA: 0x00013808 File Offset: 0x00011A08
		public bool IsVisible(float x, float y, Graphics g)
		{
			IntPtr intPtr = ((g == null) ? IntPtr.Zero : g.NativeObject);
			bool flag;
			GDIPlus.CheckStatus(GDIPlus.GdipIsVisibleRegionPoint(this.nativeRegion, x, y, intPtr, out flag));
			return flag;
		}

		/// <summary>Tests whether any portion of the specified rectangle is contained within this <see cref="T:System.Drawing.Region" />.</summary>
		/// <returns>true when any portion of the specified rectangle is contained within this <see cref="T:System.Drawing.Region" /> object; otherwise, false.</returns>
		/// <param name="x">The x-coordinate of the upper-left corner of the rectangle to test. </param>
		/// <param name="y">The y-coordinate of the upper-left corner of the rectangle to test. </param>
		/// <param name="width">The width of the rectangle to test. </param>
		/// <param name="height">The height of the rectangle to test. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060006B4 RID: 1716 RVA: 0x0001383C File Offset: 0x00011A3C
		public bool IsVisible(float x, float y, float width, float height)
		{
			bool flag;
			GDIPlus.CheckStatus(GDIPlus.GdipIsVisibleRegionRect(this.nativeRegion, x, y, width, height, IntPtr.Zero, out flag));
			return flag;
		}

		/// <summary>Tests whether any portion of the specified rectangle is contained within this <see cref="T:System.Drawing.Region" /> when drawn using the specified <see cref="T:System.Drawing.Graphics" />.</summary>
		/// <returns>true when any portion of the specified rectangle is contained within this <see cref="T:System.Drawing.Region" />; otherwise, false.</returns>
		/// <param name="x">The x-coordinate of the upper-left corner of the rectangle to test. </param>
		/// <param name="y">The y-coordinate of the upper-left corner of the rectangle to test. </param>
		/// <param name="width">The width of the rectangle to test. </param>
		/// <param name="height">The height of the rectangle to test. </param>
		/// <param name="g">A <see cref="T:System.Drawing.Graphics" /> that represents a graphics context. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060006B5 RID: 1717 RVA: 0x00013868 File Offset: 0x00011A68
		public bool IsVisible(float x, float y, float width, float height, Graphics g)
		{
			IntPtr intPtr = ((g == null) ? IntPtr.Zero : g.NativeObject);
			bool flag;
			GDIPlus.CheckStatus(GDIPlus.GdipIsVisibleRegionRect(this.nativeRegion, x, y, width, height, intPtr, out flag));
			return flag;
		}

		/// <summary>Tests whether this <see cref="T:System.Drawing.Region" /> has an empty interior on the specified drawing surface.</summary>
		/// <returns>true if the interior of this <see cref="T:System.Drawing.Region" /> is empty when the transformation associated with <paramref name="g" /> is applied; otherwise, false.</returns>
		/// <param name="g">A <see cref="T:System.Drawing.Graphics" /> that represents a drawing surface. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="g" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060006B6 RID: 1718 RVA: 0x000138A4 File Offset: 0x00011AA4
		public bool IsEmpty(Graphics g)
		{
			if (g == null)
			{
				throw new ArgumentNullException("g");
			}
			bool flag;
			GDIPlus.CheckStatus(GDIPlus.GdipIsEmptyRegion(this.nativeRegion, g.NativeObject, out flag));
			return flag;
		}

		/// <summary>Tests whether this <see cref="T:System.Drawing.Region" /> has an infinite interior on the specified drawing surface.</summary>
		/// <returns>true if the interior of this <see cref="T:System.Drawing.Region" /> is infinite when the transformation associated with <paramref name="g" /> is applied; otherwise, false.</returns>
		/// <param name="g">A <see cref="T:System.Drawing.Graphics" /> that represents a drawing surface. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="g" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060006B7 RID: 1719 RVA: 0x000138D8 File Offset: 0x00011AD8
		public bool IsInfinite(Graphics g)
		{
			if (g == null)
			{
				throw new ArgumentNullException("g");
			}
			bool flag;
			GDIPlus.CheckStatus(GDIPlus.GdipIsInfiniteRegion(this.nativeRegion, g.NativeObject, out flag));
			return flag;
		}

		/// <summary>Initializes this <see cref="T:System.Drawing.Region" /> to an empty interior.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060006B8 RID: 1720 RVA: 0x0001390C File Offset: 0x00011B0C
		public void MakeEmpty()
		{
			GDIPlus.CheckStatus(GDIPlus.GdipSetEmpty(this.nativeRegion));
		}

		/// <summary>Initializes this <see cref="T:System.Drawing.Region" /> object to an infinite interior.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060006B9 RID: 1721 RVA: 0x0001391E File Offset: 0x00011B1E
		public void MakeInfinite()
		{
			GDIPlus.CheckStatus(GDIPlus.GdipSetInfinite(this.nativeRegion));
		}

		/// <summary>Tests whether the specified <see cref="T:System.Drawing.Region" /> is identical to this <see cref="T:System.Drawing.Region" /> on the specified drawing surface.</summary>
		/// <returns>true if the interior of region is identical to the interior of this region when the transformation associated with the <paramref name="g" /> parameter is applied; otherwise, false.</returns>
		/// <param name="region">The <see cref="T:System.Drawing.Region" /> to test. </param>
		/// <param name="g">A <see cref="T:System.Drawing.Graphics" /> that represents a drawing surface. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="g" /> or <paramref name="region" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060006BA RID: 1722 RVA: 0x00013930 File Offset: 0x00011B30
		public bool Equals(Region region, Graphics g)
		{
			if (region == null)
			{
				throw new ArgumentNullException("region");
			}
			if (g == null)
			{
				throw new ArgumentNullException("g");
			}
			bool flag;
			GDIPlus.CheckStatus(GDIPlus.GdipIsEqualRegion(this.nativeRegion, region.NativeObject, g.NativeObject, out flag));
			return flag;
		}

		/// <summary>Initializes a new <see cref="T:System.Drawing.Region" /> from a handle to the specified existing GDI region.</summary>
		/// <returns>The new <see cref="T:System.Drawing.Region" />.</returns>
		/// <param name="hrgn">A handle to an existing <see cref="T:System.Drawing.Region" />. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060006BB RID: 1723 RVA: 0x00013978 File Offset: 0x00011B78
		public static Region FromHrgn(IntPtr hrgn)
		{
			if (hrgn == IntPtr.Zero)
			{
				throw new ArgumentException("hrgn");
			}
			IntPtr intPtr;
			GDIPlus.CheckStatus(GDIPlus.GdipCreateRegionHrgn(hrgn, out intPtr));
			return new Region(intPtr);
		}

		/// <summary>Returns a Windows handle to this <see cref="T:System.Drawing.Region" /> in the specified graphics context.</summary>
		/// <returns>A Windows handle to this <see cref="T:System.Drawing.Region" />.</returns>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> on which this <see cref="T:System.Drawing.Region" /> is drawn. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="g" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060006BC RID: 1724 RVA: 0x000139B0 File Offset: 0x00011BB0
		public IntPtr GetHrgn(Graphics g)
		{
			if (g == null)
			{
				return this.nativeRegion;
			}
			IntPtr zero = IntPtr.Zero;
			GDIPlus.CheckStatus(GDIPlus.GdipGetRegionHRgn(this.nativeRegion, g.NativeObject, ref zero));
			return zero;
		}

		/// <summary>Returns a <see cref="T:System.Drawing.Drawing2D.RegionData" /> that represents the information that describes this <see cref="T:System.Drawing.Region" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Drawing2D.RegionData" /> that represents the information that describes this <see cref="T:System.Drawing.Region" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060006BD RID: 1725 RVA: 0x000139E8 File Offset: 0x00011BE8
		public RegionData GetRegionData()
		{
			int num;
			GDIPlus.CheckStatus(GDIPlus.GdipGetRegionDataSize(this.nativeRegion, out num));
			byte[] array = new byte[num];
			int num2;
			GDIPlus.CheckStatus(GDIPlus.GdipGetRegionData(this.nativeRegion, array, num, out num2));
			return new RegionData(array);
		}

		/// <summary>Returns an array of <see cref="T:System.Drawing.RectangleF" /> structures that approximate this <see cref="T:System.Drawing.Region" /> after the specified matrix transformation is applied.</summary>
		/// <returns>An array of <see cref="T:System.Drawing.RectangleF" /> structures that approximate this <see cref="T:System.Drawing.Region" /> after the specified matrix transformation is applied.</returns>
		/// <param name="matrix">A <see cref="T:System.Drawing.Drawing2D.Matrix" /> that represents a geometric transformation to apply to the region. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="matrix" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060006BE RID: 1726 RVA: 0x00013A28 File Offset: 0x00011C28
		public RectangleF[] GetRegionScans(Matrix matrix)
		{
			if (matrix == null)
			{
				throw new ArgumentNullException("matrix");
			}
			int num;
			GDIPlus.CheckStatus(GDIPlus.GdipGetRegionScansCount(this.nativeRegion, out num, matrix.NativeObject));
			if (num == 0)
			{
				return new RectangleF[0];
			}
			RectangleF[] array = new RectangleF[num];
			IntPtr intPtr = Marshal.AllocHGlobal(Marshal.SizeOf<RectangleF>(array[0]) * num);
			try
			{
				GDIPlus.CheckStatus(GDIPlus.GdipGetRegionScans(this.nativeRegion, intPtr, out num, matrix.NativeObject));
			}
			finally
			{
				GDIPlus.FromUnManagedMemoryToRectangles(intPtr, array);
			}
			return array;
		}

		/// <summary>Transforms this <see cref="T:System.Drawing.Region" /> by the specified <see cref="T:System.Drawing.Drawing2D.Matrix" />.</summary>
		/// <param name="matrix">The <see cref="T:System.Drawing.Drawing2D.Matrix" /> by which to transform this <see cref="T:System.Drawing.Region" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="matrix" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060006BF RID: 1727 RVA: 0x00013AB4 File Offset: 0x00011CB4
		public void Transform(Matrix matrix)
		{
			if (matrix == null)
			{
				throw new ArgumentNullException("matrix");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipTransformRegion(this.nativeRegion, matrix.NativeObject));
		}

		/// <summary>Creates an exact copy of this <see cref="T:System.Drawing.Region" />.</summary>
		/// <returns>The <see cref="T:System.Drawing.Region" /> that this method creates.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060006C0 RID: 1728 RVA: 0x00013ADC File Offset: 0x00011CDC
		public Region Clone()
		{
			IntPtr intPtr;
			GDIPlus.CheckStatus(GDIPlus.GdipCloneRegion(this.nativeRegion, out intPtr));
			return new Region(intPtr);
		}

		/// <summary>Releases all resources used by this <see cref="T:System.Drawing.Region" />.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060006C1 RID: 1729 RVA: 0x00013B01 File Offset: 0x00011D01
		public void Dispose()
		{
			this.DisposeHandle();
			GC.SuppressFinalize(this);
		}

		// Token: 0x060006C2 RID: 1730 RVA: 0x00013B0F File Offset: 0x00011D0F
		private void DisposeHandle()
		{
			if (this.nativeRegion != IntPtr.Zero)
			{
				GDIPlus.GdipDeleteRegion(this.nativeRegion);
				this.nativeRegion = IntPtr.Zero;
			}
		}

		// Token: 0x060006C3 RID: 1731 RVA: 0x00013B3C File Offset: 0x00011D3C
		~Region()
		{
			this.DisposeHandle();
		}

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x060006C4 RID: 1732 RVA: 0x00013B68 File Offset: 0x00011D68
		// (set) Token: 0x060006C5 RID: 1733 RVA: 0x00013B70 File Offset: 0x00011D70
		internal IntPtr NativeObject
		{
			get
			{
				return this.nativeRegion;
			}
			set
			{
				this.nativeRegion = value;
			}
		}

		/// <summary>Releases the handle of the <see cref="T:System.Drawing.Region" />.</summary>
		/// <param name="regionHandle">The handle to the <see cref="T:System.Drawing.Region" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="regionHandle" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060006C6 RID: 1734 RVA: 0x00013B7C File Offset: 0x00011D7C
		public void ReleaseHrgn(IntPtr regionHandle)
		{
			if (regionHandle == IntPtr.Zero)
			{
				throw new ArgumentNullException("regionHandle");
			}
			Status status = Status.Ok;
			if (GDIPlus.RunningOnUnix())
			{
				status = GDIPlus.GdipDeleteRegion(regionHandle);
			}
			else if (!GDIPlus.DeleteObject(regionHandle))
			{
				status = Status.InvalidParameter;
			}
			GDIPlus.CheckStatus(status);
		}

		// Token: 0x0400054C RID: 1356
		private IntPtr nativeRegion = IntPtr.Zero;
	}
}
