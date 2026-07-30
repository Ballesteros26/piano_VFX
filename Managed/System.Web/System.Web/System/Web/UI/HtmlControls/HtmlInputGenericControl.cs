using System;
using System.Collections.Specialized;
using System.ComponentModel;
using Unity;

namespace System.Web.UI.HtmlControls
{
	/// <summary>Defines the methods, properties, and events for server-side access to the HTML5 input element. </summary>
	// Token: 0x02000793 RID: 1939
	[DefaultEvent("ServerChange")]
	[ValidationProperty("Value")]
	public class HtmlInputGenericControl : HtmlInputControl, IPostBackDataHandler
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlInputGenericControl" /> class.</summary>
		// Token: 0x06004E68 RID: 20072 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public HtmlInputGenericControl()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlInputGenericControl" /> class based on the specified type.</summary>
		/// <param name="type">The type of the control.</param>
		// Token: 0x06004E69 RID: 20073 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public HtmlInputGenericControl(string type)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Occurs when the content of the HTML5 input element changes between posts to the server.</summary>
		// Token: 0x1400012F RID: 303
		// (add) Token: 0x06004E6A RID: 20074 RVA: 0x0000B3E4 File Offset: 0x000095E4
		// (remove) Token: 0x06004E6B RID: 20075 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public event EventHandler ServerChange
		{
			add
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
			remove
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Processes the postback data for the HTML5 input element.</summary>
		/// <returns>true if the posted content differs from the content in a previous postback; otherwise, false.</returns>
		/// <param name="postDataKey">The index in the posted collection that references the content to load.</param>
		/// <param name="postCollection">The collection of all posted values.</param>
		// Token: 0x06004E6C RID: 20076 RVA: 0x000CB404 File Offset: 0x000C9604
		protected virtual bool LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.HtmlControls.HtmlInputGenericControl.ServerChange" /> event.</summary>
		/// <param name="e">The event data.</param>
		// Token: 0x06004E6D RID: 20077 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void OnServerChange(EventArgs e)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Invokes the <see cref="M:System.Web.UI.HtmlControls.HtmlInputGenericControl.OnServerChange(System.EventArgs)" /> method when the posted data for the <see cref="T:System.Web.UI.HtmlControls.HtmlInputGenericControl" /> element has changed.</summary>
		// Token: 0x06004E6E RID: 20078 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void RaisePostDataChangedEvent()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>When implemented in a derived class, processes postback data for an HTML5 input element.</summary>
		/// <returns>true if the state of the HTML5 input element changes because of the postback; otherwise, false.</returns>
		/// <param name="postDataKey">The index in the posted collection that references the content to load.</param>
		/// <param name="postCollection">The collection of all posted values.</param>
		// Token: 0x06004E6F RID: 20079 RVA: 0x000CB420 File Offset: 0x000C9620
		bool IPostBackDataHandler.LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		/// <summary>When implemented in a derived class, invokes the <see cref="M:System.Web.UI.HtmlControls.HtmlInputGenericControl.OnServerChange(System.EventArgs)" /> method when the posted data for the HTML5 input element has changed.</summary>
		// Token: 0x06004E70 RID: 20080 RVA: 0x0000B3E4 File Offset: 0x000095E4
		void IPostBackDataHandler.RaisePostDataChangedEvent()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
