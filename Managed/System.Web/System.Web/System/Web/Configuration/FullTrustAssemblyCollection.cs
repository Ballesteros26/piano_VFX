using System;
using System.Configuration;
using Unity;

namespace System.Web.Configuration
{
	/// <summary>Represents a collection of <see cref="T:System.Web.Configuration.FullTrustAssembly" /> objects. This class cannot be inherited. </summary>
	// Token: 0x020006AA RID: 1706
	[ConfigurationCollection(typeof(string))]
	public sealed class FullTrustAssemblyCollection : ConfigurationElementCollection
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.FullTrustAssemblyCollection" /> class.</summary>
		// Token: 0x06004815 RID: 18453 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public FullTrustAssemblyCollection()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the <see cref="T:System.Web.Configuration.FullTrustAssembly" /> object from the collection at the specified index.</summary>
		/// <returns>The element at the specified index.</returns>
		/// <param name="index">The collection element index.</param>
		// Token: 0x1700164B RID: 5707
		public FullTrustAssembly this[int index]
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

		/// <summary>Adds a <see cref="T:System.Web.Configuration.FullTrustAssembly" /> object to the collection.</summary>
		/// <param name="fullTrustAssembly">The object to add to the collection.</param>
		// Token: 0x06004818 RID: 18456 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public void Add(FullTrustAssembly fullTrustAssembly)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Removes all the <see cref="T:System.Web.Configuration.FullTrustAssembly" /> objects from the collection.</summary>
		// Token: 0x06004819 RID: 18457 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public void Clear()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x0600481A RID: 18458 RVA: 0x0000E80B File Offset: 0x0000CA0B
		protected override ConfigurationElement CreateNewElement()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		// Token: 0x0600481B RID: 18459 RVA: 0x0000E80B File Offset: 0x0000CA0B
		protected override object GetElementKey(ConfigurationElement element)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Removes the specified <see cref="T:System.Web.Configuration.FullTrustAssembly" /> object from the collection.</summary>
		/// <param name="key">The key of the element to remove from the collection.</param>
		// Token: 0x0600481C RID: 18460 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public void Remove(string key)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Removes the <see cref="T:System.Web.Configuration.FullTrustAssembly" /> object from the collection at the specified index.</summary>
		/// <param name="index">The index of the element to remove from the collection.</param>
		// Token: 0x0600481D RID: 18461 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public void RemoveAt(int index)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
