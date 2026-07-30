using System;
using System.ComponentModel;
using Unity;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Provides a way for developers to declare static connections in a content page when a <see cref="T:System.Web.UI.WebControls.WebParts.WebPartManager" /> control has been declared in the content page's associated master page.</summary>
	// Token: 0x020007B8 RID: 1976
	[PersistChildren(false)]
	[ParseChildren(true)]
	[NonVisualControl]
	[Designer("System.Web.UI.Design.WebControls.WebParts.ProxyWebPartManagerDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[Bindable(false)]
	public class ProxyWebPartManager : Control
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.ProxyWebPartManager" /> class. </summary>
		// Token: 0x06004FBF RID: 20415 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public ProxyWebPartManager()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets a collection of static connections declared within the &lt;asp:proxywebpartmanager&gt; element on a content page.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.ProxyWebPartConnectionCollection" /> that contains all static <see cref="T:System.Web.UI.WebControls.WebParts.WebPartConnection" /> objects declared within an &lt;asp:proxywebpartmanager&gt; element. </returns>
		// Token: 0x1700183F RID: 6207
		// (get) Token: 0x06004FC0 RID: 20416 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public ProxyWebPartConnectionCollection StaticConnections
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}
	}
}
