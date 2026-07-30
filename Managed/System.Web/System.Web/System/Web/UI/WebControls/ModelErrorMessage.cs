using System;
using System.ComponentModel;
using Unity;

namespace System.Web.UI.WebControls
{
	/// <summary>Displays the first model error for a given key from the page's model state.</summary>
	// Token: 0x0200079A RID: 1946
	[DefaultProperty("Key")]
	[ToolboxData("<{0}:ModelErrorMessage runat=\"server\" Key=\"ModelStateKey\"></{0}:ModelErrorMessage>")]
	[PersistChildren(false)]
	[ParseChildren(true)]
	public class ModelErrorMessage : Label
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.ModelErrorMessage" /> class.</summary>
		// Token: 0x06004E8F RID: 20111 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public ModelErrorMessage()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets or sets the key for which the first error from the page's model state is to be displayed.</summary>
		/// <returns>The key.</returns>
		// Token: 0x170017DD RID: 6109
		// (get) Token: 0x06004E90 RID: 20112 RVA: 0x0000E80B File Offset: 0x0000CA0B
		// (set) Token: 0x06004E91 RID: 20113 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public string ModelStateKey
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets a value that specifies whether the focus should be set on the associated control when an error is displayed.</summary>
		/// <returns>true if the focus should be set; otherwise, false.</returns>
		// Token: 0x170017DE RID: 6110
		// (get) Token: 0x06004E92 RID: 20114 RVA: 0x000CB490 File Offset: 0x000C9690
		// (set) Token: 0x06004E93 RID: 20115 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public bool SetFocusOnError
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}
	}
}
