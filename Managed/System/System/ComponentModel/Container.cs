using System;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Encapsulates zero or more components.</summary>
	// Token: 0x02000249 RID: 585
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class Container : IContainer, IDisposable
	{
		// Token: 0x060012DF RID: 4831 RVA: 0x0004ED50 File Offset: 0x0004CF50
		~Container()
		{
			this.Dispose(false);
		}

		/// <summary>Adds the specified <see cref="T:System.ComponentModel.Component" /> to the <see cref="T:System.ComponentModel.Container" />. The component is unnamed.</summary>
		/// <param name="component">The component to add. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="component" /> is null.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060012E0 RID: 4832 RVA: 0x0004ED80 File Offset: 0x0004CF80
		public virtual void Add(IComponent component)
		{
			this.Add(component, null);
		}

		/// <summary>Adds the specified <see cref="T:System.ComponentModel.Component" /> to the <see cref="T:System.ComponentModel.Container" /> and assigns it a name.</summary>
		/// <param name="component">The component to add. </param>
		/// <param name="name">The unique, case-insensitive name to assign to the component.-or- null, which leaves the component unnamed. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="component" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is not unique.</exception>
		// Token: 0x060012E1 RID: 4833 RVA: 0x0004ED8C File Offset: 0x0004CF8C
		public virtual void Add(IComponent component, string name)
		{
			object obj = this.syncObj;
			lock (obj)
			{
				if (component != null)
				{
					ISite site = component.Site;
					if (site == null || site.Container != this)
					{
						if (this.sites == null)
						{
							this.sites = new ISite[4];
						}
						else
						{
							this.ValidateName(component, name);
							if (this.sites.Length == this.siteCount)
							{
								ISite[] array = new ISite[this.siteCount * 2];
								Array.Copy(this.sites, 0, array, 0, this.siteCount);
								this.sites = array;
							}
						}
						if (site != null)
						{
							site.Container.Remove(component);
						}
						ISite site2 = this.CreateSite(component, name);
						ISite[] array2 = this.sites;
						int num = this.siteCount;
						this.siteCount = num + 1;
						array2[num] = site2;
						component.Site = site2;
						this.components = null;
					}
				}
			}
		}

		/// <summary>Creates a site <see cref="T:System.ComponentModel.ISite" /> for the given <see cref="T:System.ComponentModel.IComponent" /> and assigns the given name to the site.</summary>
		/// <returns>The newly created site.</returns>
		/// <param name="component">The <see cref="T:System.ComponentModel.IComponent" /> to create a site for. </param>
		/// <param name="name">The name to assign to <paramref name="component" />, or null to skip the name assignment. </param>
		// Token: 0x060012E2 RID: 4834 RVA: 0x0004EE84 File Offset: 0x0004D084
		protected virtual ISite CreateSite(IComponent component, string name)
		{
			return new Container.Site(component, this, name);
		}

		/// <summary>Releases all resources used by the <see cref="T:System.ComponentModel.Container" />.</summary>
		// Token: 0x060012E3 RID: 4835 RVA: 0x0004EE8E File Offset: 0x0004D08E
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.ComponentModel.Container" />, and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x060012E4 RID: 4836 RVA: 0x0004EEA0 File Offset: 0x0004D0A0
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				object obj = this.syncObj;
				lock (obj)
				{
					while (this.siteCount > 0)
					{
						ISite[] array = this.sites;
						int num = this.siteCount - 1;
						this.siteCount = num;
						object obj2 = array[num];
						((ISite)obj2).Component.Site = null;
						((ISite)obj2).Component.Dispose();
					}
					this.sites = null;
					this.components = null;
				}
			}
		}

		/// <summary>Gets the service object of the specified type, if it is available.</summary>
		/// <returns>An <see cref="T:System.Object" /> implementing the requested service, or null if the service cannot be resolved.</returns>
		/// <param name="service">The <see cref="T:System.Type" /> of the service to retrieve. </param>
		// Token: 0x060012E5 RID: 4837 RVA: 0x0004EF24 File Offset: 0x0004D124
		protected virtual object GetService(Type service)
		{
			if (!(service == typeof(IContainer)))
			{
				return null;
			}
			return this;
		}

		/// <summary>Gets all the components in the <see cref="T:System.ComponentModel.Container" />.</summary>
		/// <returns>A collection that contains the components in the <see cref="T:System.ComponentModel.Container" />.</returns>
		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x060012E6 RID: 4838 RVA: 0x0004EF3C File Offset: 0x0004D13C
		public virtual ComponentCollection Components
		{
			get
			{
				object obj = this.syncObj;
				ComponentCollection componentCollection2;
				lock (obj)
				{
					if (this.components == null)
					{
						IComponent[] array = new IComponent[this.siteCount];
						for (int i = 0; i < this.siteCount; i++)
						{
							array[i] = this.sites[i].Component;
						}
						this.components = new ComponentCollection(array);
						if (this.filter == null && this.checkedFilter)
						{
							this.checkedFilter = false;
						}
					}
					if (!this.checkedFilter)
					{
						this.filter = this.GetService(typeof(ContainerFilterService)) as ContainerFilterService;
						this.checkedFilter = true;
					}
					if (this.filter != null)
					{
						ComponentCollection componentCollection = this.filter.FilterComponents(this.components);
						if (componentCollection != null)
						{
							this.components = componentCollection;
						}
					}
					componentCollection2 = this.components;
				}
				return componentCollection2;
			}
		}

		/// <summary>Removes a component from the <see cref="T:System.ComponentModel.Container" />.</summary>
		/// <param name="component">The component to remove. </param>
		// Token: 0x060012E7 RID: 4839 RVA: 0x0004F02C File Offset: 0x0004D22C
		public virtual void Remove(IComponent component)
		{
			this.Remove(component, false);
		}

		// Token: 0x060012E8 RID: 4840 RVA: 0x0004F038 File Offset: 0x0004D238
		private void Remove(IComponent component, bool preserveSite)
		{
			object obj = this.syncObj;
			lock (obj)
			{
				if (component != null)
				{
					ISite site = component.Site;
					if (site != null && site.Container == this)
					{
						if (!preserveSite)
						{
							component.Site = null;
						}
						for (int i = 0; i < this.siteCount; i++)
						{
							if (this.sites[i] == site)
							{
								this.siteCount--;
								Array.Copy(this.sites, i + 1, this.sites, i, this.siteCount - i);
								this.sites[this.siteCount] = null;
								this.components = null;
								break;
							}
						}
					}
				}
			}
		}

		/// <summary>Removes a component from the <see cref="T:System.ComponentModel.Container" /> without setting <see cref="P:System.ComponentModel.IComponent.Site" /> to null.</summary>
		/// <param name="component">The component to remove.</param>
		// Token: 0x060012E9 RID: 4841 RVA: 0x0004F0F8 File Offset: 0x0004D2F8
		protected void RemoveWithoutUnsiting(IComponent component)
		{
			this.Remove(component, true);
		}

		/// <summary>Determines whether the component name is unique for this container.</summary>
		/// <param name="component">The named component.</param>
		/// <param name="name">The component name to validate.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="component" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is not unique.</exception>
		// Token: 0x060012EA RID: 4842 RVA: 0x0004F104 File Offset: 0x0004D304
		protected virtual void ValidateName(IComponent component, string name)
		{
			if (component == null)
			{
				throw new ArgumentNullException("component");
			}
			if (name != null)
			{
				for (int i = 0; i < Math.Min(this.siteCount, this.sites.Length); i++)
				{
					ISite site = this.sites[i];
					if (site != null && site.Name != null && string.Equals(site.Name, name, StringComparison.OrdinalIgnoreCase) && site.Component != component && ((InheritanceAttribute)TypeDescriptor.GetAttributes(site.Component)[typeof(InheritanceAttribute)]).InheritanceLevel != InheritanceLevel.InheritedReadOnly)
					{
						throw new ArgumentException(global::SR.GetString("Duplicate component name '{0}'.  Component names must be unique and case-insensitive.", new object[] { name }));
					}
				}
			}
		}

		// Token: 0x04001288 RID: 4744
		private ISite[] sites;

		// Token: 0x04001289 RID: 4745
		private int siteCount;

		// Token: 0x0400128A RID: 4746
		private ComponentCollection components;

		// Token: 0x0400128B RID: 4747
		private ContainerFilterService filter;

		// Token: 0x0400128C RID: 4748
		private bool checkedFilter;

		// Token: 0x0400128D RID: 4749
		private object syncObj = new object();

		// Token: 0x0200024A RID: 586
		private class Site : ISite, IServiceProvider
		{
			// Token: 0x060012EC RID: 4844 RVA: 0x0004F1C6 File Offset: 0x0004D3C6
			internal Site(IComponent component, Container container, string name)
			{
				this.component = component;
				this.container = container;
				this.name = name;
			}

			// Token: 0x170003EB RID: 1003
			// (get) Token: 0x060012ED RID: 4845 RVA: 0x0004F1E3 File Offset: 0x0004D3E3
			public IComponent Component
			{
				get
				{
					return this.component;
				}
			}

			// Token: 0x170003EC RID: 1004
			// (get) Token: 0x060012EE RID: 4846 RVA: 0x0004F1EB File Offset: 0x0004D3EB
			public IContainer Container
			{
				get
				{
					return this.container;
				}
			}

			// Token: 0x060012EF RID: 4847 RVA: 0x0004F1F3 File Offset: 0x0004D3F3
			public object GetService(Type service)
			{
				if (!(service == typeof(ISite)))
				{
					return this.container.GetService(service);
				}
				return this;
			}

			// Token: 0x170003ED RID: 1005
			// (get) Token: 0x060012F0 RID: 4848 RVA: 0x00004240 File Offset: 0x00002440
			public bool DesignMode
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170003EE RID: 1006
			// (get) Token: 0x060012F1 RID: 4849 RVA: 0x0004F215 File Offset: 0x0004D415
			// (set) Token: 0x060012F2 RID: 4850 RVA: 0x0004F21D File Offset: 0x0004D41D
			public string Name
			{
				get
				{
					return this.name;
				}
				set
				{
					if (value == null || this.name == null || !value.Equals(this.name))
					{
						this.container.ValidateName(this.component, value);
						this.name = value;
					}
				}
			}

			// Token: 0x0400128E RID: 4750
			private IComponent component;

			// Token: 0x0400128F RID: 4751
			private Container container;

			// Token: 0x04001290 RID: 4752
			private string name;
		}
	}
}
