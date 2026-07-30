using System;
using System.Configuration;
using Unity;

namespace System.Security.Authentication.ExtendedProtection.Configuration
{
	/// <summary>The <see cref="T:System.Security.Authentication.ExtendedProtection.ServiceNameCollection" /> class is a collection of service principal names that represent a configuration element for an <see cref="T:System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy" />.</summary>
	// Token: 0x0200038C RID: 908
	[ConfigurationCollection(typeof(ServiceNameElement))]
	public sealed class ServiceNameElementCollection : ConfigurationElementCollection
	{
		/// <summary>The <see cref="P:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElementCollection.Item(System.String)" /> property gets or sets the <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElement" /> instance at the specified index location.</summary>
		/// <returns>The <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElement" /> instance requested. If the requested instance is not found, then null is returned.</returns>
		/// <param name="index">The index of the <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElement" /> instance in this <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElementCollection" />.</param>
		// Token: 0x17000591 RID: 1425
		public ServiceNameElement this[int index]
		{
			get
			{
				return (ServiceNameElement)base.BaseGet(index);
			}
		}

		/// <summary>The <see cref="P:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElementCollection.Item(System.String)" /> property gets or sets the <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElement" /> instance based on a string that represents the <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElement" /> instance.</summary>
		/// <returns>The <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElement" /> instance requested. If the requested instance is not found, then null is returned.</returns>
		/// <param name="name">A <see cref="T:System.String" /> that represents the <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElement" /> instance in this <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElementCollection" />.</param>
		// Token: 0x17000592 RID: 1426
		public ServiceNameElement this[string name]
		{
			get
			{
				return (ServiceNameElement)base.BaseGet(name);
			}
		}

		/// <summary>The <see cref="M:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElementCollection.Add(System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElement)" /> method adds a <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElement" /> instance to this <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElementCollection" />. </summary>
		/// <param name="element">The <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElement" /> instance to add to this <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElementCollection" />.</param>
		// Token: 0x06001B81 RID: 7041 RVA: 0x00004239 File Offset: 0x00002439
		public void Add(ServiceNameElement element)
		{
			throw new NotImplementedException();
		}

		/// <summary>The <see cref="M:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElementCollection.Clear" /> method removes all configuration element objects from this <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElementCollection" />.</summary>
		// Token: 0x06001B82 RID: 7042 RVA: 0x00004239 File Offset: 0x00002439
		public void Clear()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001B83 RID: 7043 RVA: 0x0006D76A File Offset: 0x0006B96A
		protected override ConfigurationElement CreateNewElement()
		{
			return new ServiceNameElement();
		}

		// Token: 0x06001B84 RID: 7044 RVA: 0x0006D771 File Offset: 0x0006B971
		protected override object GetElementKey(ConfigurationElement element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			return ((ServiceNameElement)element).Name;
		}

		/// <summary>The <see cref="M:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElementCollection.IndexOf(System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElement)" /> method retrieves the index of the specified configuration element in this <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElementCollection" />.</summary>
		/// <returns>The index of the specified <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElement" /> in this <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElementCollection" />.</returns>
		/// <param name="element">The <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElement" /> instance to retrieve the index of in this <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElementCollection" />.</param>
		// Token: 0x06001B85 RID: 7045 RVA: 0x00004239 File Offset: 0x00002439
		public int IndexOf(ServiceNameElement element)
		{
			throw new NotImplementedException();
		}

		/// <summary>The <see cref="M:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElementCollection.Remove(System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElement)" /> method removes a <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElement" /> instance from this <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElementCollection" /> based on the <see cref="T:System.String" /> specified.</summary>
		/// <param name="name">A <see cref="T:System.String" /> that represents the <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElement" /> instance to remove from this <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElementCollection" /></param>
		// Token: 0x06001B86 RID: 7046 RVA: 0x00004239 File Offset: 0x00002439
		public void Remove(string name)
		{
			throw new NotImplementedException();
		}

		/// <summary>The <see cref="M:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElementCollection.Remove(System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElement)" /> method removes a <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElement" /> instance from this <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElementCollection" />.</summary>
		/// <param name="element">The <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElement" /> instance to remove from this <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElementCollection" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="element " />is null. </exception>
		// Token: 0x06001B87 RID: 7047 RVA: 0x00004239 File Offset: 0x00002439
		public void Remove(ServiceNameElement element)
		{
			throw new NotImplementedException();
		}

		/// <summary>The <see cref="M:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElementCollection.Remove(System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElement)" /> method removes a <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElement" /> instance from this <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElementCollection" /> based on the index specified.</summary>
		/// <param name="index">The index of the <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElement" /> instance to remove from this <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElementCollection" />.</param>
		// Token: 0x06001B88 RID: 7048 RVA: 0x00004239 File Offset: 0x00002439
		public void RemoveAt(int index)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001B8A RID: 7050 RVA: 0x0000F0CE File Offset: 0x0000D2CE
		public void set_Item(int index, ServiceNameElement value)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>The <see cref="P:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElementCollection.Item(System.String)" /> property gets or sets the <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElement" /> instance based on a string that represents the <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElement" /> instance.</summary>
		/// <returns>The <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElement" /> instance requested. If the requested instance is not found, then null is returned.</returns>
		/// <param name="name">A <see cref="T:System.String" /> that represents the <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElement" /> instance in this <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElementCollection" />.</param>
		// Token: 0x17000593 RID: 1427
		public string this[string name]
		{
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}
	}
}
