using System;
using System.ComponentModel.Design;

namespace System.Web.UI.Design
{
	/// <summary>Represents a designer verb that creates a template editing frame, and that can be invoked only by a template editor.</summary>
	// Token: 0x020000A4 RID: 164
	[Obsolete("Template editing is supported in ControlDesigner.TemplateGroups with SetViewFlags(ViewFlags.TemplateEditing, true) in 2.0.")]
	public class TemplateEditingVerb : DesignerVerb, IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.TemplateEditingVerb" /> class with the specified verb text and index.</summary>
		/// <param name="text">The text to show for the verb on a menu.</param>
		/// <param name="index">An optional integer value that can be used by a designer, typically to indicate the index of the verb within a set of verbs.</param>
		// Token: 0x060004DB RID: 1243 RVA: 0x000093C4 File Offset: 0x000075C4
		[MonoTODO]
		public TemplateEditingVerb(string text, int index)
			: base(text, null)
		{
			throw new NotImplementedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.TemplateEditingVerb" /> class.</summary>
		/// <param name="text">The text to show for the verb on a menu. </param>
		/// <param name="index">An optional integer value that can be used by a designer, typically to indicate the index of the verb within a set of verbs. </param>
		/// <param name="designer">The <see cref="T:System.Web.UI.Design.TemplatedControlDesigner" /> that can use this verb. </param>
		// Token: 0x060004DC RID: 1244 RVA: 0x000093D3 File Offset: 0x000075D3
		public TemplateEditingVerb(string text, int index, TemplatedControlDesigner designer)
			: base(text, designer.TemplateEditingVerbHandler)
		{
			this._index = index;
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x000093EC File Offset: 0x000075EC
		~TemplateEditingVerb()
		{
			this.Dispose(false);
		}

		/// <summary>Releases all resources used by the <see cref="T:System.Web.UI.Design.TemplateEditingVerb" />.</summary>
		// Token: 0x060004DE RID: 1246 RVA: 0x0000941C File Offset: 0x0000761C
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Web.UI.Design.TemplateEditingVerb" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x060004DF RID: 1247 RVA: 0x0000942B File Offset: 0x0000762B
		[MonoTODO]
		protected virtual void Dispose(bool disposing)
		{
		}

		/// <summary>Gets the index or other user data for the verb.</summary>
		/// <returns>The index or user data for the verb.</returns>
		// Token: 0x17000131 RID: 305
		// (get) Token: 0x060004E0 RID: 1248 RVA: 0x0000942F File Offset: 0x0000762F
		public int Index
		{
			get
			{
				return this._index;
			}
		}

		// Token: 0x04000135 RID: 309
		private int _index;
	}
}
