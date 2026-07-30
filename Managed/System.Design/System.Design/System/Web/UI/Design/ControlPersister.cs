using System;
using System.ComponentModel.Design;
using System.IO;

namespace System.Web.UI.Design
{
	/// <summary>Provides methods for persisting Web server controls at design-time.</summary>
	// Token: 0x0200005E RID: 94
	public sealed class ControlPersister
	{
		// Token: 0x0600030B RID: 779 RVA: 0x00002352 File Offset: 0x00000552
		private ControlPersister()
		{
		}

		/// <summary>Gets a string of data that represents the persisted form of the specified control.</summary>
		/// <returns>A string that represents the persisted form of the control.</returns>
		/// <param name="control">The <see cref="T:System.Web.UI.Control" /> to persist. </param>
		// Token: 0x0600030C RID: 780 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public static string PersistControl(Control control)
		{
			throw new NotImplementedException();
		}

		/// <summary>Outputs a string of data that represents the persisted form of the specified control to the specified <see cref="T:System.IO.TextWriter" />.</summary>
		/// <param name="sw">The <see cref="T:System.IO.TextWriter" /> to output the persisted control data to. </param>
		/// <param name="control">The <see cref="T:System.Web.UI.Control" /> to persist. </param>
		// Token: 0x0600030D RID: 781 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public static void PersistControl(TextWriter sw, Control control)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets a string of data that represents the persisted form of the specified control, using the specified designer host.</summary>
		/// <returns>A string that represents the persisted form of the control.</returns>
		/// <param name="control">The <see cref="T:System.Web.UI.Control" /> to persist. </param>
		/// <param name="host">An <see cref="T:System.ComponentModel.Design.IDesignerHost" /> that is the designer host for the control. </param>
		// Token: 0x0600030E RID: 782 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public static string PersistControl(Control control, IDesignerHost host)
		{
			throw new NotImplementedException();
		}

		/// <summary>Outputs a string of data that represents the persisted form of the specified control to the specified <see cref="T:System.IO.TextWriter" />, using the specified designer host.</summary>
		/// <param name="sw">The <see cref="T:System.IO.TextWriter" /> to output the persisted control data to. </param>
		/// <param name="control">The <see cref="T:System.Web.UI.Control" /> to persist. </param>
		/// <param name="host">An <see cref="T:System.ComponentModel.Design.IDesignerHost" /> that is the designer host for the control. </param>
		// Token: 0x0600030F RID: 783 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public static void PersistControl(TextWriter sw, Control control, IDesignerHost host)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets a string of data that can persist the inner properties of the specified control.</summary>
		/// <returns>A string that contains the information to persist about the inner properties of the control.</returns>
		/// <param name="component">The component to persist the inner properties of. </param>
		/// <param name="host">An <see cref="T:System.ComponentModel.Design.IDesignerHost" /> that is the designer host for the control. </param>
		// Token: 0x06000310 RID: 784 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public static string PersistInnerProperties(object component, IDesignerHost host)
		{
			throw new NotImplementedException();
		}

		/// <summary>Outputs a string of data that can persist the inner properties of the specified control to the specified <see cref="T:System.IO.TextWriter" />, using the specified designer host.</summary>
		/// <param name="sw">The <see cref="T:System.IO.TextWriter" /> to use. </param>
		/// <param name="component">The component to persist. </param>
		/// <param name="host">An <see cref="T:System.ComponentModel.Design.IDesignerHost" /> that is the designer host for the control. </param>
		// Token: 0x06000311 RID: 785 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public static void PersistInnerProperties(TextWriter sw, object component, IDesignerHost host)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets a string of data that represents the persisted form of the specified template.</summary>
		/// <returns>A string that represents the persisted form of the template.</returns>
		/// <param name="template">The template to persist.</param>
		/// <param name="host">An <see cref="T:System.ComponentModel.Design.IDesignerHost" /> that is the designer host for the control.</param>
		// Token: 0x06000312 RID: 786 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		public static string PersistTemplate(ITemplate template, IDesignerHost host)
		{
			throw new NotImplementedException();
		}

		/// <summary>Writes a string of data that represents the persisted form of the specified template to the specified <see cref="T:System.IO.TextWriter" /> object.</summary>
		/// <param name="writer">The <see cref="T:System.IO.TextWriter" /> to write the persisted template data to.</param>
		/// <param name="template">The template to persist.</param>
		/// <param name="host">An <see cref="T:System.ComponentModel.Design.IDesignerHost" /> that is the designer host for the control.</param>
		// Token: 0x06000313 RID: 787 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		public static void PersistTemplate(TextWriter writer, ITemplate template, IDesignerHost host)
		{
			throw new NotImplementedException();
		}
	}
}
