using System;
using Unity;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>When overridden in a derived class, implements a representation of information about a set of Web Parts data for a page.</summary>
	// Token: 0x020007B4 RID: 1972
	[Serializable]
	public abstract class PersonalizationStateInfo
	{
		// Token: 0x06004FAB RID: 20395 RVA: 0x0000B3E4 File Offset: 0x000095E4
		internal PersonalizationStateInfo()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the date and time that the personalization state was last updated.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> indicating when the personalization state was last updated.</returns>
		// Token: 0x1700183A RID: 6202
		// (get) Token: 0x06004FAC RID: 20396 RVA: 0x000CB8B8 File Offset: 0x000C9AB8
		public DateTime LastUpdatedDate
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(DateTime);
			}
		}

		/// <summary>Gets the path to the page associated with the personalization state information.</summary>
		/// <returns>The path of the page associated with the personalization state information.</returns>
		// Token: 0x1700183B RID: 6203
		// (get) Token: 0x06004FAD RID: 20397 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public string Path
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the size of the personalization state information stored in the underlying data store.</summary>
		/// <returns>The size, in bytes, of the personalization state information.</returns>
		// Token: 0x1700183C RID: 6204
		// (get) Token: 0x06004FAE RID: 20398 RVA: 0x000CB8D4 File Offset: 0x000C9AD4
		public int Size
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}
	}
}
