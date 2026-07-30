using System;
using Unity;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Represents a lightweight version of shared personalization information about a page. This class cannot be inherited.</summary>
	// Token: 0x020007BC RID: 1980
	[Serializable]
	public sealed class SharedPersonalizationStateInfo : PersonalizationStateInfo
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.SharedPersonalizationStateInfo" /> class. </summary>
		/// <param name="path">The page that the information applies to. <paramref name="path" /> is an application-relative (using tilde syntax) virtual path.</param>
		/// <param name="lastUpdatedDate">A <see cref="T:System.DateTime" /> indicating when the shared information for the page was last updated.</param>
		/// <param name="size">The size, in bytes, of the shared state information for the page.</param>
		/// <param name="sizeOfPersonalizations">The total size, in bytes, of all per-user personalization information that exists for the page.</param>
		/// <param name="countOfPersonalizations">The total number of users who have personalized the page.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" />, when trimmed, is an empty string ("").</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="size" />, <paramref name="sizeOfPersonalizations" /> or <paramref name="countOfPersonalizations" /> is negative.</exception>
		// Token: 0x06004FDB RID: 20443 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public SharedPersonalizationStateInfo(string path, DateTime lastUpdatedDate, int size, int sizeOfPersonalizations, int countOfPersonalizations)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the total number of users that have personalized a page.</summary>
		/// <returns>The total number of per-user personalization settings that have been applied to a page.</returns>
		// Token: 0x17001845 RID: 6213
		// (get) Token: 0x06004FDC RID: 20444 RVA: 0x000CB97C File Offset: 0x000C9B7C
		public int CountOfPersonalizations
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}

		/// <summary>Gets the total size of all per-user personalization information for a page.</summary>
		/// <returns>The total size, in bytes, of all personalization information for a page.</returns>
		// Token: 0x17001846 RID: 6214
		// (get) Token: 0x06004FDD RID: 20445 RVA: 0x000CB998 File Offset: 0x000C9B98
		public int SizeOfPersonalizations
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}
	}
}
