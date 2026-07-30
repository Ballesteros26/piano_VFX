using System;
using System.Collections;

namespace System.Web.Services.Discovery
{
	/// <summary>A collection of discovery references. This class cannot be inherited.</summary>
	// Token: 0x020000AC RID: 172
	public sealed class DiscoveryReferenceCollection : CollectionBase
	{
		/// <summary>Gets or sets the <see cref="T:System.Web.Services.Discovery.DiscoveryReference" /> at the specified index.</summary>
		/// <returns>The <see cref="T:System.Web.Services.Discovery.DiscoveryReference" /> at the specified index.</returns>
		/// <param name="i">The zero-based index of the <see cref="T:System.Web.Services.Discovery.DiscoveryReference" /> to get or set. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="i" /> is not a valid index in the <see cref="T:System.Web.Services.Discovery.DiscoveryReferenceCollection" />. </exception>
		// Token: 0x17000133 RID: 307
		public DiscoveryReference this[int i]
		{
			get
			{
				return (DiscoveryReference)base.List[i];
			}
			set
			{
				base.List[i] = value;
			}
		}

		/// <summary>Adds a <see cref="T:System.Web.Services.Discovery.DiscoveryReference" /> to the <see cref="T:System.Web.Services.Discovery.DiscoveryReferenceCollection" />.</summary>
		/// <returns>The position where the <see cref="T:System.Web.Services.Discovery.DiscoveryReference" /> was inserted in the <see cref="T:System.Web.Services.Discovery.DiscoveryReferenceCollection" />.</returns>
		/// <param name="value">The <see cref="T:System.Web.Services.Discovery.DiscoveryReference" /> to add to the <see cref="T:System.Web.Services.Discovery.DiscoveryReferenceCollection" />. </param>
		// Token: 0x06000485 RID: 1157 RVA: 0x0000CD4B File Offset: 0x0000AF4B
		public int Add(DiscoveryReference value)
		{
			return base.List.Add(value);
		}

		/// <summary>Determines whether the <see cref="T:System.Web.Services.Discovery.DiscoveryReferenceCollection" /> contains a specific <see cref="T:System.Web.Services.Discovery.DiscoveryReference" />.</summary>
		/// <returns>true if the <see cref="T:System.Web.Services.Discovery.DiscoveryReferenceCollection" /> contains the <see cref="T:System.Web.Services.Discovery.DiscoveryReference" />; otherwise, false.</returns>
		/// <param name="value">The <see cref="T:System.Web.Services.Discovery.DiscoveryReference" /> to locate within the <see cref="T:System.Web.Services.Discovery.DiscoveryReferenceCollection" />. </param>
		// Token: 0x06000486 RID: 1158 RVA: 0x0000CD76 File Offset: 0x0000AF76
		public bool Contains(DiscoveryReference value)
		{
			return base.List.Contains(value);
		}

		/// <summary>Removes a <see cref="T:System.Web.Services.Discovery.DiscoveryReference" /> from the <see cref="T:System.Web.Services.Discovery.DiscoveryReferenceCollection" />.</summary>
		/// <param name="value">The <see cref="T:System.Web.Services.Discovery.DiscoveryReference" /> to remove from the <see cref="T:System.Web.Services.Discovery.DiscoveryReferenceCollection" />. </param>
		// Token: 0x06000487 RID: 1159 RVA: 0x0000CD84 File Offset: 0x0000AF84
		public void Remove(DiscoveryReference value)
		{
			base.List.Remove(value);
		}
	}
}
