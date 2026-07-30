using System;
using System.Configuration;
using Unity;

namespace System.Web.Configuration
{
	/// <summary>Represents a configuration element that contains a collection of protocols.</summary>
	// Token: 0x0200077C RID: 1916
	[ConfigurationCollection(typeof(ProtocolElement))]
	public sealed class ProtocolCollection : ConfigurationElementCollection
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.ProtocolCollection" /> class. </summary>
		// Token: 0x06004DF5 RID: 19957 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public ProtocolCollection()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets all the keys of the <see cref="T:System.Web.Configuration.ProtocolCollection" /> instance.</summary>
		/// <returns>The array that contains the collection keys.</returns>
		// Token: 0x170017BF RID: 6079
		// (get) Token: 0x06004DF6 RID: 19958 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public string[] AllKeys
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.Configuration.ProtocolElement" /> object at the specified index of the collection.</summary>
		/// <returns>A <see cref="T:System.Web.Configuration.ProtocolElement" /> object.</returns>
		/// <param name="index">The index of the <see cref="T:System.Web.Configuration.ProtocolElement" /> object in the <see cref="T:System.Web.Configuration.ProtocolCollection" /> instance.</param>
		// Token: 0x170017C0 RID: 6080
		public ProtocolElement this[int index]
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

		/// <summary>Adds a configuration element to the <see cref="T:System.Web.Configuration.ProtocolCollection" /> instance.</summary>
		/// <param name="protocolElement">The element to add.</param>
		// Token: 0x06004DF9 RID: 19961 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public void Add(ProtocolElement protocolElement)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Removes all configuration element objects from the collection.</summary>
		// Token: 0x06004DFA RID: 19962 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public void Clear()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x06004DFB RID: 19963 RVA: 0x0000E80B File Offset: 0x0000CA0B
		protected override ConfigurationElement CreateNewElement()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		// Token: 0x06004DFC RID: 19964 RVA: 0x0000E80B File Offset: 0x0000CA0B
		protected override object GetElementKey(ConfigurationElement element)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Removes the <see cref="T:System.Web.Configuration.ProtocolElement" /> object that has the specified key from the collection.</summary>
		/// <param name="name">The key of the <see cref="T:System.Web.Configuration.ProtocolElement" /> object to remove.</param>
		// Token: 0x06004DFD RID: 19965 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public void Remove(string name)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Removes the specified <see cref="T:System.Web.Configuration.ProtocolElement" /> object from the collection.</summary>
		/// <param name="protocolElement">The element to remove.</param>
		// Token: 0x06004DFE RID: 19966 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public void Remove(ProtocolElement protocolElement)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Removes the <see cref="T:System.Web.Configuration.ProtocolElement" /> object at the specified index location.</summary>
		/// <param name="index">The index location of the element to remove. </param>
		// Token: 0x06004DFF RID: 19967 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public void RemoveAt(int index)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
