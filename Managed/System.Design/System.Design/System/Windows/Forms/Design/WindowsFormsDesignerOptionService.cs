using System;
using System.ComponentModel.Design;

namespace System.Windows.Forms.Design
{
	/// <summary>Provides access to get and set option values for a Windows Forms designer.</summary>
	// Token: 0x0200003F RID: 63
	public class WindowsFormsDesignerOptionService : DesignerOptionService
	{
		/// <summary>Gets the <see cref="T:System.Windows.Forms.Design.DesignerOptions" /> exposed by the <see cref="T:System.Windows.Forms.Design.WindowsFormsDesignerOptionService" />.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Design.DesignerOptions" /> exposed by the <see cref="T:System.Windows.Forms.Design.WindowsFormsDesignerOptionService" />.</returns>
		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000218 RID: 536 RVA: 0x0000234B File Offset: 0x0000054B
		public virtual DesignerOptions CompatibilityOptions
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Populates a <see cref="T:System.ComponentModel.Design.DesignerOptionService.DesignerOptionCollection" />.</summary>
		/// <param name="options">The collection to populate.</param>
		// Token: 0x06000219 RID: 537 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		protected override void PopulateOptionCollection(DesignerOptionService.DesignerOptionCollection options)
		{
			throw new NotImplementedException();
		}
	}
}
