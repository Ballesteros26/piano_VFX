using System;

namespace System.Web.UI.Design
{
	/// <summary>Defines an editable region of content within the design-time markup for the associated control.</summary>
	// Token: 0x020000A9 RID: 169
	public class TemplatedEditableDesignerRegion : EditableDesignerRegion
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.TemplatedEditableDesignerRegion" /> class using the provided template definition.</summary>
		/// <param name="templateDefinition">A <see cref="T:System.Web.UI.Design.TemplateDefinition" /> instance for the template to edit.</param>
		// Token: 0x06000521 RID: 1313 RVA: 0x00009491 File Offset: 0x00007691
		[MonoNotSupported("")]
		public TemplatedEditableDesignerRegion(TemplateDefinition templateDefinition)
			: base(null, null)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets or sets whether the template occurs only once per instance of the containing control, such as a header template, or can appear many times according to available data, such as an item template.</summary>
		/// <returns>true if the template appears only once; otherwise, false.</returns>
		// Token: 0x17000145 RID: 325
		// (get) Token: 0x06000522 RID: 1314 RVA: 0x0000234B File Offset: 0x0000054B
		// (set) Token: 0x06000523 RID: 1315 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		public virtual bool IsSingleInstanceTemplate
		{
			[MonoNotSupported("")]
			get
			{
				throw new NotImplementedException();
			}
			[MonoNotSupported("")]
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets a value that indicates whether the template can be bound to a data source.</summary>
		/// <returns>true if the template represented by the region can be bound to a data source; otherwise, false.</returns>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to set this property.</exception>
		// Token: 0x17000146 RID: 326
		// (get) Token: 0x06000524 RID: 1316 RVA: 0x0000234B File Offset: 0x0000054B
		// (set) Token: 0x06000525 RID: 1317 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		public override bool SupportsDataBinding
		{
			[MonoNotSupported("")]
			get
			{
				throw new NotImplementedException();
			}
			[MonoNotSupported("")]
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a <see cref="T:System.Web.UI.Design.TemplateDefinition" /> object describing the template that is referenced by the region.</summary>
		/// <returns>A <see cref="T:System.Web.UI.Design.TemplateDefinition" /> object.</returns>
		// Token: 0x17000147 RID: 327
		// (get) Token: 0x06000526 RID: 1318 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		public TemplateDefinition TemplateDefinition
		{
			[MonoNotSupported("")]
			get
			{
				throw new NotImplementedException();
			}
		}
	}
}
