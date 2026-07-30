using System;
using System.Collections;

namespace System.Web.Services.Discovery
{
	/// <summary>Contains a collection of <see cref="T:System.Web.Services.Discovery.DiscoveryClientResult" /> objects. This class cannot be inherited.</summary>
	// Token: 0x020000A0 RID: 160
	public sealed class DiscoveryClientResultCollection : CollectionBase
	{
		/// <summary>Gets or sets the <see cref="T:System.Web.Services.Discovery.DiscoveryClientResult" /> at position <paramref name="i" /> of the <see cref="T:System.Web.Services.Discovery.DiscoveryClientResultCollection" />.</summary>
		/// <returns>The <see cref="T:System.Web.Services.Discovery.DiscoveryClientResult" /> at the specified index.</returns>
		/// <param name="i">The zero-based index of the <see cref="T:System.Web.Services.Discovery.DiscoveryClientResult" /> to get or set. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="i" /> is not a valid index in the <see cref="T:System.Web.Services.Discovery.DiscoveryClientResultCollection" />. </exception>
		// Token: 0x1700011F RID: 287
		public DiscoveryClientResult this[int i]
		{
			get
			{
				return (DiscoveryClientResult)base.List[i];
			}
			set
			{
				base.List[i] = value;
			}
		}

		/// <summary>Adds a <see cref="T:System.Web.Services.Discovery.DiscoveryClientResult" /> to the <see cref="T:System.Web.Services.Discovery.DiscoveryClientResultCollection" />.</summary>
		/// <returns>The position into which the <see cref="T:System.Web.Services.Discovery.DiscoveryClientResult" /> was inserted.</returns>
		/// <param name="value">The <see cref="T:System.Web.Services.Discovery.DiscoveryClientResult" /> to add to the <see cref="T:System.Web.Services.Discovery.DiscoveryClientResultCollection" />. </param>
		// Token: 0x06000422 RID: 1058 RVA: 0x0000CD4B File Offset: 0x0000AF4B
		public int Add(DiscoveryClientResult value)
		{
			return base.List.Add(value);
		}

		/// <summary>Determines whether the <see cref="T:System.Web.Services.Discovery.DiscoveryClientResultCollection" /> contains a specific <see cref="T:System.Web.Services.Discovery.DiscoveryClientResult" />.</summary>
		/// <returns>true if the <see cref="T:System.Web.Services.Discovery.DiscoveryClientResult" /> is found in the <see cref="T:System.Web.Services.Discovery.DiscoveryClientResultCollection" />; otherwise, false.</returns>
		/// <param name="value">The <see cref="T:System.Web.Services.Discovery.DiscoveryClientResult" /> to locate in the <see cref="T:System.Web.Services.Discovery.DiscoveryClientResultCollection" />. </param>
		// Token: 0x06000423 RID: 1059 RVA: 0x0000CD76 File Offset: 0x0000AF76
		public bool Contains(DiscoveryClientResult value)
		{
			return base.List.Contains(value);
		}

		/// <summary>Removes the first occurrence of a specific <see cref="T:System.Web.Services.Discovery.DiscoveryClientResult" /> from the <see cref="T:System.Web.Services.Discovery.DiscoveryClientResultCollection" />.</summary>
		/// <param name="value">The <see cref="T:System.Web.Services.Discovery.DiscoveryClientResult" /> to remove from the <see cref="T:System.Web.Services.Discovery.DiscoveryClientResultCollection" />. </param>
		// Token: 0x06000424 RID: 1060 RVA: 0x0000CD84 File Offset: 0x0000AF84
		public void Remove(DiscoveryClientResult value)
		{
			base.List.Remove(value);
		}
	}
}
