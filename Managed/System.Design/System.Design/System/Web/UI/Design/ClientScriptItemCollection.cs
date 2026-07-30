using System;
using System.Collections;

namespace System.Web.UI.Design
{
	/// <summary>Represents a read-only collection of client script blocks that are contained within a Web Form or user control at design time. This class cannot be inherited.</summary>
	// Token: 0x02000053 RID: 83
	public sealed class ClientScriptItemCollection : ReadOnlyCollectionBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.ClientScriptItemCollection" /> class. </summary>
		/// <param name="clientScriptItems">An array of <see cref="T:System.Web.UI.Design.ClientScriptItem" /> elements used to initialize the collection.</param>
		// Token: 0x060002A8 RID: 680 RVA: 0x00008E02 File Offset: 0x00007002
		public ClientScriptItemCollection(ClientScriptItem[] clientScriptItems)
		{
			if (clientScriptItems == null)
			{
				throw new ArgumentNullException("clientScriptItems");
			}
			base.InnerList.AddRange(clientScriptItems);
		}
	}
}
