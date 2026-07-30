using System;
using System.Configuration;
using Unity;

namespace System.Web.Configuration
{
	/// <summary>Represents a collection of <see cref="T:System.Web.Configuration.PartialTrustVisibleAssembly" /> objects. This class cannot be inherited. </summary>
	// Token: 0x020006AD RID: 1709
	[ConfigurationCollection(typeof(string))]
	public sealed class PartialTrustVisibleAssemblyCollection : ConfigurationElementCollection
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.PartialTrustVisibleAssemblyCollection" /> class.</summary>
		// Token: 0x06004827 RID: 18471 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public PartialTrustVisibleAssemblyCollection()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the <see cref="T:System.Web.Configuration.PartialTrustVisibleAssembly" /> object at the specified index.</summary>
		/// <returns>The collection object at the specified index.</returns>
		/// <param name="index">The index of the element to get.</param>
		// Token: 0x17001650 RID: 5712
		public PartialTrustVisibleAssembly this[int index]
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

		/// <summary>Adds a <see cref="T:System.Web.Configuration.PartialTrustVisibleAssembly" /> object to the collection.</summary>
		/// <param name="partialTrustVisibleAssembly">The object to add to the collection.</param>
		// Token: 0x0600482A RID: 18474 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public void Add(PartialTrustVisibleAssembly partialTrustVisibleAssembly)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Removes all the <see cref="T:System.Web.Configuration.PartialTrustVisibleAssembly" /> objects from the collection.</summary>
		// Token: 0x0600482B RID: 18475 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public void Clear()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x0600482C RID: 18476 RVA: 0x0000E80B File Offset: 0x0000CA0B
		protected override ConfigurationElement CreateNewElement()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		// Token: 0x0600482D RID: 18477 RVA: 0x0000E80B File Offset: 0x0000CA0B
		protected override object GetElementKey(ConfigurationElement element)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Removes the specified <see cref="T:System.Web.Configuration.PartialTrustVisibleAssembly" /> object from the collection </summary>
		/// <param name="key">The key of the element to remove.</param>
		// Token: 0x0600482E RID: 18478 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public void Remove(string key)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Removes the <see cref="T:System.Web.Configuration.PartialTrustVisibleAssembly" /> object from the collection at the specified index.</summary>
		/// <param name="index">The index of the element to remove.</param>
		// Token: 0x0600482F RID: 18479 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public void RemoveAt(int index)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
