using System;
using System.Drawing;

namespace System.Web.UI.Design
{
	/// <summary>Defines a region of content within the design-time markup for the associated control.</summary>
	// Token: 0x02000075 RID: 117
	public class DesignerRegion : DesignerObject
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.DesignerRegion" /> class with the specified name for a control designer.</summary>
		/// <param name="designer">The control designer that contains this designer region.</param>
		/// <param name="name">The name of this designer region.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="designer" /> is null.-or-<paramref name="designer" /> is an empty string ("").-or-<paramref name="name" /> is null.-or-<paramref name="name" /> is an empty string ("").</exception>
		// Token: 0x060003AE RID: 942 RVA: 0x0000902A File Offset: 0x0000722A
		[MonoNotSupported("")]
		public DesignerRegion(ControlDesigner designer, string name)
			: this(designer, name, false)
		{
			throw new NotImplementedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.DesignerRegion" /> class with the specified name for a control designer, optionally setting the instance as a selectable region in the designer.</summary>
		/// <param name="designer">The control designer that contains this designer region.</param>
		/// <param name="name">The name of this designer region.</param>
		/// <param name="selectable">true to select the region; otherwise, false.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="designer" /> is null.-or-<paramref name="designer" /> is an empty string ("").-or-<paramref name="name" /> is null.-or-<paramref name="name" /> is an empty string ("").</exception>
		// Token: 0x060003AF RID: 943 RVA: 0x0000903A File Offset: 0x0000723A
		[MonoNotSupported("")]
		public DesignerRegion(ControlDesigner designer, string name, bool selectable)
			: base(designer, name)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the size of the designer region on the design surface.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> representing the designer region size on the design surface.</returns>
		// Token: 0x060003B0 RID: 944 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		public Rectangle GetBounds()
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets or sets the description for a designer region.</summary>
		/// <returns>A text description of the designer region. The default is an empty string ("").</returns>
		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060003B1 RID: 945 RVA: 0x0000234B File Offset: 0x0000054B
		// (set) Token: 0x060003B2 RID: 946 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		public virtual string Description
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

		/// <summary>Gets or sets the friendly display name for a designer region.</summary>
		/// <returns>A text display name for the designer region. The default is an empty string ("").</returns>
		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060003B3 RID: 947 RVA: 0x0000234B File Offset: 0x0000054B
		// (set) Token: 0x060003B4 RID: 948 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		public virtual string DisplayName
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

		/// <summary>Gets or sets a value indicating whether the region size is to be explicitly set on the designer region by the design host.</summary>
		/// <returns>true, if the design host should set the size on the designer region; otherwise, false. The default is false.</returns>
		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060003B5 RID: 949 RVA: 0x0000234B File Offset: 0x0000054B
		// (set) Token: 0x060003B6 RID: 950 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		public bool EnsureSize
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

		/// <summary>Gets or sets a value indicating whether to highlight the designer region on the design surface.</summary>
		/// <returns>true, if the visual designer should highlight the designer region on the design surface; otherwise, false. The default is false.</returns>
		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060003B7 RID: 951 RVA: 0x0000234B File Offset: 0x0000054B
		// (set) Token: 0x060003B8 RID: 952 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		public virtual bool Highlight
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

		/// <summary>Gets or sets a value indicating whether the designer region can be selected by the user on the design surface.</summary>
		/// <returns>true, if the designer region can be selected by the user on the design surface; otherwise, false. The default is false.</returns>
		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060003B9 RID: 953 RVA: 0x0000234B File Offset: 0x0000054B
		// (set) Token: 0x060003BA RID: 954 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		public virtual bool Selectable
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

		/// <summary>Gets or sets a value indicating whether the designer region is currently selected on the design surface.</summary>
		/// <returns>true, if the designer region is currently selected on the design surface; otherwise, false. The default is false.</returns>
		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060003BB RID: 955 RVA: 0x0000234B File Offset: 0x0000054B
		// (set) Token: 0x060003BC RID: 956 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		public virtual bool Selected
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

		/// <summary>Gets or sets optional user data to associate with the designer region.</summary>
		/// <returns>An object that contains user data stored for the designer region. The default is null.</returns>
		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060003BD RID: 957 RVA: 0x0000234B File Offset: 0x0000054B
		// (set) Token: 0x060003BE RID: 958 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		public object UserData
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

		/// <summary>Defines the HTML attribute name for a designer region.</summary>
		// Token: 0x0400012B RID: 299
		public static readonly string DesignerRegionAttributeName;
	}
}
