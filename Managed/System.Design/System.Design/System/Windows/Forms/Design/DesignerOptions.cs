using System;
using System.ComponentModel;
using System.Drawing;

namespace System.Windows.Forms.Design
{
	/// <summary>Provides access to get and set option values for a designer.</summary>
	// Token: 0x02000018 RID: 24
	public class DesignerOptions
	{
		/// <summary>Gets or sets a value that enables or disables in-place editing for <see cref="T:System.Windows.Forms.ToolStrip" /> controls.</summary>
		/// <returns>true if in-place editing for <see cref="T:System.Windows.Forms.ToolStrip" /> controls is enabled; otherwise, false. The default is true.</returns>
		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000F3 RID: 243 RVA: 0x0000234B File Offset: 0x0000054B
		// (set) Token: 0x060000F4 RID: 244 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		[Browsable(false)]
		public virtual bool EnableInSituEditing
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Drawing.Size" /> representing the dimensions of a grid unit. </summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> representing the dimensions of a grid unit.</returns>
		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000F5 RID: 245 RVA: 0x0000234B File Offset: 0x0000054B
		// (set) Token: 0x060000F6 RID: 246 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public virtual Size GridSize
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets a value that specifies whether a designer shows a component's smart tag panel automatically on creation. </summary>
		/// <returns>true to allow a component's smart tag panel to open automatically upon creation; otherwise, false. The default is true.</returns>
		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000F7 RID: 247 RVA: 0x0000234B File Offset: 0x0000054B
		// (set) Token: 0x060000F8 RID: 248 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public virtual bool ObjectBoundSmartTagAutoShow
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets a value that enables or disables the grid in the designer. </summary>
		/// <returns>true if the grid is enabled; otherwise, false. The default is true.</returns>
		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x0000234B File Offset: 0x0000054B
		// (set) Token: 0x060000FA RID: 250 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public virtual bool ShowGrid
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets a value that enables or disables whether controls are automatically placed at grid coordinates. </summary>
		/// <returns>true if snapping is enabled; otherwise, false.</returns>
		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000FB RID: 251 RVA: 0x0000234B File Offset: 0x0000054B
		// (set) Token: 0x060000FC RID: 252 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public virtual bool SnapToGrid
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets a value that enables or disables the component cache. </summary>
		/// <returns>true if the component cache is enabled; otherwise, false. The default is true.</returns>
		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000FD RID: 253 RVA: 0x0000234B File Offset: 0x0000054B
		// (set) Token: 0x060000FE RID: 254 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public virtual bool UseOptimizedCodeGeneration
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets a value that enables or disables smart tags in the designer.</summary>
		/// <returns>true if smart tags in the designer are enabled; otherwise, false.</returns>
		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000FF RID: 255 RVA: 0x0000234B File Offset: 0x0000054B
		// (set) Token: 0x06000100 RID: 256 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public virtual bool UseSmartTags
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets a value that enables or disables snaplines in the designer.</summary>
		/// <returns>true if snaplines in the designer are enabled; otherwise, false.</returns>
		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000101 RID: 257 RVA: 0x0000234B File Offset: 0x0000054B
		// (set) Token: 0x06000102 RID: 258 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public virtual bool UseSnapLines
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}
	}
}
