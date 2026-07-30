using System;

namespace System.Web.UI.Design
{
	/// <summary>Provides the abstract base class for creating formats that can be applied to a custom Web server control at design time.</summary>
	// Token: 0x0200006F RID: 111
	public abstract class DesignerAutoFormat
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.DesignerAutoFormat" /> class.</summary>
		/// <param name="name">A string that identifies a specific <see cref="T:System.Web.UI.Design.DesignerAutoFormat" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null.</exception>
		// Token: 0x06000375 RID: 885 RVA: 0x00002352 File Offset: 0x00000552
		protected DesignerAutoFormat(string name)
		{
		}

		/// <summary>Gets the name of a <see cref="T:System.Web.UI.Design.DesignerAutoFormat" /> object.</summary>
		/// <returns>The <see cref="T:System.Web.UI.Design.DesignerAutoFormat" /> name.</returns>
		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000376 RID: 886 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public string Name
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a <see cref="T:System.Web.UI.Design.DesignerAutoFormatStyle" /> object that is used by the <see cref="T:System.Web.UI.Design.DesignerAutoFormat" /> object to render a design-time preview of the associated control.</summary>
		/// <returns>An  object that is used by the <see cref="T:System.Web.UI.Design.DesignerAutoFormat" /> object to render a design-time preview of the associated control.</returns>
		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000377 RID: 887 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public DesignerAutoFormatStyle Style
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Applies the associated formatting to the specified control.</summary>
		/// <param name="control">A Web server control to apply the formatting to.</param>
		// Token: 0x06000378 RID: 888
		public abstract void Apply(Control control);

		/// <summary>Returns a copy of the associated control in order to provide a preview before applying the format to the control.</summary>
		/// <returns>The <see cref="M:System.Web.UI.Design.DesignerAutoFormat.GetPreviewControl(System.Web.UI.Control)" /> method returns a copy of the associated Web server control.</returns>
		/// <param name="runtimeControl">A run-time version of the Web server control.</param>
		// Token: 0x06000379 RID: 889 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public virtual Control GetPreviewControl(Control runtimeControl)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns a string that represents the current <see cref="T:System.Web.UI.Design.DesignerAutoFormat" /> object.</summary>
		/// <returns>The <see cref="P:System.Web.UI.Design.DesignerAutoFormat.Name" /> property of the current <see cref="T:System.Web.UI.Design.DesignerAutoFormat" />.</returns>
		// Token: 0x0600037A RID: 890 RVA: 0x00005153 File Offset: 0x00003353
		[MonoTODO]
		public override string ToString()
		{
			return base.ToString();
		}
	}
}
