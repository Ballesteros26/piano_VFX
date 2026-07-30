using System;
using System.Collections;

namespace System.Web.Services.Discovery
{
	/// <summary>Represents a collection of <see cref="T:System.Web.Services.Discovery.DiscoveryReference" /> objects. This class cannot be inherited.</summary>
	// Token: 0x020000A2 RID: 162
	public sealed class DiscoveryClientReferenceCollection : DictionaryBase
	{
		/// <summary>Gets or sets a <see cref="T:System.Web.Services.Discovery.DiscoveryReference" /> object from the <see cref="T:System.Web.Services.Discovery.DiscoveryClientReferenceCollection" /> with the specified URL.</summary>
		/// <returns>An DiscoveryReference representing a reference to a discovery document.</returns>
		/// <param name="url">The URL for the <see cref="T:System.Web.Services.Discovery.DiscoveryReference" /> to get or set from the <see cref="T:System.Web.Services.Discovery.DiscoveryClientReferenceCollection" />. </param>
		// Token: 0x17000123 RID: 291
		public DiscoveryReference this[string url]
		{
			get
			{
				return (DiscoveryReference)base.Dictionary[url];
			}
			set
			{
				base.Dictionary[url] = value;
			}
		}

		/// <summary>Gets an <see cref="T:System.Collections.ICollection" /> object with all the keys in the <see cref="T:System.Web.Services.Discovery.DiscoveryClientReferenceCollection" />.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> containing the keys of the <see cref="T:System.Web.Services.Discovery.DiscoveryClientReferenceCollection" />.</returns>
		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000430 RID: 1072 RVA: 0x00012AC3 File Offset: 0x00010CC3
		public ICollection Keys
		{
			get
			{
				return base.Dictionary.Keys;
			}
		}

		/// <summary>Gets an <see cref="T:System.Collections.ICollection" /> object with all the values in the <see cref="T:System.Web.Services.Discovery.DiscoveryClientReferenceCollection" />.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> containing the values in the <see cref="T:System.Web.Services.Discovery.DiscoveryClientReferenceCollection" />.</returns>
		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000431 RID: 1073 RVA: 0x00012AD0 File Offset: 0x00010CD0
		public ICollection Values
		{
			get
			{
				return base.Dictionary.Values;
			}
		}

		/// <summary>Adds a <see cref="T:System.Web.Services.Discovery.DiscoveryReference" /> to the <see cref="T:System.Web.Services.Discovery.DiscoveryClientReferenceCollection" />.</summary>
		/// <param name="value">The <see cref="T:System.Web.Services.Discovery.DiscoveryReference" /> to add to the <see cref="T:System.Web.Services.Discovery.DiscoveryClientReferenceCollection" />. </param>
		// Token: 0x06000432 RID: 1074 RVA: 0x00013A82 File Offset: 0x00011C82
		public void Add(DiscoveryReference value)
		{
			this.Add(value.Url, value);
		}

		/// <summary>Adds a <see cref="T:System.Web.Services.Discovery.DiscoveryReference" /> with the specified URL and value to the <see cref="T:System.Web.Services.Discovery.DiscoveryClientReferenceCollection" />.</summary>
		/// <param name="url">The URL for the reference to add to the <see cref="T:System.Web.Services.Discovery.DiscoveryClientReferenceCollection" />. </param>
		/// <param name="value">The <see cref="T:System.Web.Services.Discovery.DiscoveryReference" /> to add to the <see cref="T:System.Web.Services.Discovery.DiscoveryClientReferenceCollection" />. </param>
		// Token: 0x06000433 RID: 1075 RVA: 0x00012ADD File Offset: 0x00010CDD
		public void Add(string url, DiscoveryReference value)
		{
			base.Dictionary.Add(url, value);
		}

		/// <summary>Determines if the <see cref="T:System.Web.Services.Discovery.DiscoveryClientReferenceCollection" /> contains a <see cref="T:System.Web.Services.Discovery.DiscoveryReference" /> with the specified URL.</summary>
		/// <returns>true if the <see cref="T:System.Web.Services.Discovery.DiscoveryClientReferenceCollection" /> contains a <see cref="T:System.Web.Services.Discovery.DiscoveryReference" /> with the specified URL; otherwise, false.</returns>
		/// <param name="url">The URL for the <see cref="T:System.Web.Services.Discovery.DiscoveryReference" /> to locate within the <see cref="T:System.Web.Services.Discovery.DiscoveryClientReferenceCollection" />. </param>
		// Token: 0x06000434 RID: 1076 RVA: 0x00012AEC File Offset: 0x00010CEC
		public bool Contains(string url)
		{
			return base.Dictionary.Contains(url);
		}

		/// <summary>Removes a <see cref="T:System.Web.Services.Discovery.DiscoveryReference" /> with the specified URL from the <see cref="T:System.Web.Services.Discovery.DiscoveryClientReferenceCollection" />.</summary>
		/// <param name="url">A string that represents the URL for the object to remove from the <see cref="T:System.Web.Services.Discovery.DiscoveryClientReferenceCollection" />. </param>
		// Token: 0x06000435 RID: 1077 RVA: 0x00012AFA File Offset: 0x00010CFA
		public void Remove(string url)
		{
			base.Dictionary.Remove(url);
		}
	}
}
