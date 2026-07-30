using System;

namespace System.Drawing.Drawing2D
{
	/// <summary>Contains the graphical data that makes up a <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> object. This class cannot be inherited.</summary>
	// Token: 0x02000144 RID: 324
	public sealed class PathData
	{
		/// <summary>Gets or sets an array of <see cref="T:System.Drawing.PointF" /> structures that represents the points through which the path is constructed.</summary>
		/// <returns>An array of <see cref="T:System.Drawing.PointF" /> objects that represents the points through which the path is constructed.</returns>
		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x06000E14 RID: 3604 RVA: 0x0001EFDB File Offset: 0x0001D1DB
		// (set) Token: 0x06000E15 RID: 3605 RVA: 0x0001EFE3 File Offset: 0x0001D1E3
		public PointF[] Points { get; set; }

		/// <summary>Gets or sets the types of the corresponding points in the path.</summary>
		/// <returns>An array of bytes that specify the types of the corresponding points in the path.</returns>
		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x06000E16 RID: 3606 RVA: 0x0001EFEC File Offset: 0x0001D1EC
		// (set) Token: 0x06000E17 RID: 3607 RVA: 0x0001EFF4 File Offset: 0x0001D1F4
		public byte[] Types { get; set; }
	}
}
