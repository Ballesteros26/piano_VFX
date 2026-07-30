using System;
using System.Windows.Forms;

namespace System.Drawing.Design
{
	/// <summary>Encapsulates a <see cref="T:System.Drawing.Design.ToolboxItemCreatorCallback" />. This class cannot be inherited.</summary>
	// Token: 0x0200001A RID: 26
	public sealed class ToolboxItemCreator
	{
		// Token: 0x06000060 RID: 96 RVA: 0x00002050 File Offset: 0x00000250
		internal ToolboxItemCreator()
		{
		}

		/// <summary>Creates a new <see cref="T:System.Drawing.Design.ToolboxItem" /> from a <see cref="T:System.Windows.Forms.IDataObject" />.</summary>
		/// <returns>A new instance of the <see cref="T:System.Drawing.Design.ToolboxItem" /> class.</returns>
		/// <param name="data">A data object that represents a <see cref="T:System.Drawing.Design.ToolboxItem" />.</param>
		// Token: 0x06000061 RID: 97 RVA: 0x0000359B File Offset: 0x0000179B
		[MonoTODO]
		public ToolboxItem Create(IDataObject data)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the Clipboard format that represents the data needed to deserialize a <see cref="T:System.Drawing.Design.ToolboxItem" />.</summary>
		/// <returns>A string representing the Clipboard format.</returns>
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000062 RID: 98 RVA: 0x0000359B File Offset: 0x0000179B
		[MonoTODO]
		public string Format
		{
			get
			{
				throw new NotImplementedException();
			}
		}
	}
}
