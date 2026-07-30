using System;
using System.Collections;

namespace System.ComponentModel.Design
{
	/// <summary>Represents a base class for design-time tools, not derived from <see cref="T:System.ComponentModel.Design.ComponentDesigner" />, that provide smart tag or designer verb capabilities.</summary>
	// Token: 0x0200011D RID: 285
	public class DesignerCommandSet
	{
		/// <summary>Gets the collection of all the smart tags associated with the designed component.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.Design.DesignerActionListCollection" /> that contains the smart tags for the associated designed component.</returns>
		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x0600083F RID: 2111 RVA: 0x0000DA4D File Offset: 0x0000BC4D
		public DesignerActionListCollection ActionLists
		{
			get
			{
				return this.action_lists;
			}
		}

		/// <summary>Returns a collection of command objects.</summary>
		/// <returns>A collection that contains the specified type—either <see cref="T:System.ComponentModel.Design.DesignerActionList" /> or <see cref="T:System.ComponentModel.Design.DesignerVerb" />—of command objects. The base implementation always returns null.</returns>
		/// <param name="name">The type of collection to return, indicating either a <see cref="T:System.ComponentModel.Design.DesignerActionListCollection" /> or a <see cref="T:System.ComponentModel.Design.DesignerVerbCollection" />.</param>
		// Token: 0x06000840 RID: 2112 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public virtual ICollection GetCommands(string name)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the collection of all the designer verbs associated with the designed component.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.Design.DesignerVerbCollection" /> that contains the designer verbs for the associated designed component.</returns>
		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x06000841 RID: 2113 RVA: 0x0000DA55 File Offset: 0x0000BC55
		public DesignerVerbCollection Verbs
		{
			get
			{
				return this.verbs;
			}
		}

		// Token: 0x040001C8 RID: 456
		private DesignerActionListCollection action_lists = new DesignerActionListCollection();

		// Token: 0x040001C9 RID: 457
		private DesignerVerbCollection verbs = new DesignerVerbCollection();
	}
}
