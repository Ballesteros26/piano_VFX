using System;
using System.Configuration;
using Unity;

namespace System.Web.Configuration
{
	/// <summary>Represents a collection of <see cref="T:System.Web.Configuration.IgnoreDeviceFilterElement" /> objects. This class cannot be inherited. </summary>
	// Token: 0x020006A7 RID: 1703
	[ConfigurationCollection(typeof(IgnoreDeviceFilterElement), AddItemName = "filter", CollectionType = ConfigurationElementCollectionType.BasicMap)]
	public sealed class IgnoreDeviceFilterElementCollection : ConfigurationElementCollection
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.IgnoreDeviceFilterElementCollection" /> class.</summary>
		// Token: 0x06004807 RID: 18439 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public IgnoreDeviceFilterElementCollection()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the <see cref="T:System.Web.Configuration.IgnoreDeviceFilterElement" /> object from the collection at the specified index.</summary>
		/// <returns>The specified filter object.</returns>
		/// <param name="index">The index of the filter object to get.</param>
		// Token: 0x17001648 RID: 5704
		public IgnoreDeviceFilterElement this[int index]
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

		/// <summary>Adds a <see cref="T:System.Web.Configuration.IgnoreDeviceFilterElement" /> object to the collection.</summary>
		/// <param name="deviceFilter">The object to add to the collection.</param>
		// Token: 0x0600480A RID: 18442 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public void Add(IgnoreDeviceFilterElement deviceFilter)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Removes all the <see cref="T:System.Web.Configuration.IgnoreDeviceFilterElement" /> objects from the collection.</summary>
		// Token: 0x0600480B RID: 18443 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public void Clear()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x0600480C RID: 18444 RVA: 0x0000E80B File Offset: 0x0000CA0B
		protected override ConfigurationElement CreateNewElement()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		// Token: 0x0600480D RID: 18445 RVA: 0x0000E80B File Offset: 0x0000CA0B
		protected override object GetElementKey(ConfigurationElement element)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Removes the specified <see cref="T:System.Web.Configuration.IgnoreDeviceFilterElement" /> object from the collection, by using its name.</summary>
		/// <param name="name">The name of the object to remove.</param>
		// Token: 0x0600480E RID: 18446 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public void Remove(string name)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Removes the specified <see cref="T:System.Web.Configuration.IgnoreDeviceFilterElement" /> object from the collection.</summary>
		/// <param name="deviceFilter">The object to remove. </param>
		// Token: 0x0600480F RID: 18447 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public void Remove(IgnoreDeviceFilterElement deviceFilter)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Removes the <see cref="T:System.Web.Configuration.IgnoreDeviceFilterElement" /> object from the collection at the specified index.</summary>
		/// <param name="index">The index of the object to remove.</param>
		// Token: 0x06004810 RID: 18448 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public void RemoveAt(int index)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
