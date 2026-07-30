using System;
using System.Collections;
using System.ComponentModel.Design.Serialization;

namespace System.ComponentModel.Design
{
	// Token: 0x02000103 RID: 259
	internal class DesignModeSite : ISite, IServiceProvider, IDictionaryService, IServiceContainer
	{
		// Token: 0x06000764 RID: 1892 RVA: 0x0000C24B File Offset: 0x0000A44B
		public DesignModeSite(IComponent component, string name, IContainer container, IServiceProvider serviceProvider)
		{
			this._component = component;
			this._container = container;
			this._componentName = name;
			this._serviceProvider = serviceProvider;
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x06000765 RID: 1893 RVA: 0x0000C270 File Offset: 0x0000A470
		public IComponent Component
		{
			get
			{
				return this._component;
			}
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x06000766 RID: 1894 RVA: 0x0000C278 File Offset: 0x0000A478
		public IContainer Container
		{
			get
			{
				return this._container;
			}
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x06000767 RID: 1895 RVA: 0x000023D8 File Offset: 0x000005D8
		public bool DesignMode
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x06000768 RID: 1896 RVA: 0x0000C280 File Offset: 0x0000A480
		// (set) Token: 0x06000769 RID: 1897 RVA: 0x0000C288 File Offset: 0x0000A488
		public string Name
		{
			get
			{
				return this._componentName;
			}
			set
			{
				if (value != this._componentName && value != null && value.Trim().Length > 0)
				{
					INameCreationService nameCreationService = this.GetService(typeof(INameCreationService)) as INameCreationService;
					if (this._container.Components[value] == null && (nameCreationService == null || (nameCreationService != null && nameCreationService.IsValidName(value))))
					{
						string componentName = this._componentName;
						this._componentName = value;
						((DesignerHost)this.GetService(typeof(IDesignerHost))).OnComponentRename(this._component, componentName, this._componentName);
					}
				}
			}
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x0600076A RID: 1898 RVA: 0x0000C321 File Offset: 0x0000A521
		private ServiceContainer SiteSpecificServices
		{
			get
			{
				if (this._siteSpecificServices == null)
				{
					this._siteSpecificServices = new ServiceContainer(null);
				}
				return this._siteSpecificServices;
			}
		}

		// Token: 0x0600076B RID: 1899 RVA: 0x0000C33D File Offset: 0x0000A53D
		void IServiceContainer.AddService(Type serviceType, object serviceInstance)
		{
			this.SiteSpecificServices.AddService(serviceType, serviceInstance);
		}

		// Token: 0x0600076C RID: 1900 RVA: 0x0000C34C File Offset: 0x0000A54C
		void IServiceContainer.AddService(Type serviceType, object serviceInstance, bool promote)
		{
			this.SiteSpecificServices.AddService(serviceType, serviceInstance, promote);
		}

		// Token: 0x0600076D RID: 1901 RVA: 0x0000C35C File Offset: 0x0000A55C
		void IServiceContainer.AddService(Type serviceType, ServiceCreatorCallback callback)
		{
			this.SiteSpecificServices.AddService(serviceType, callback);
		}

		// Token: 0x0600076E RID: 1902 RVA: 0x0000C36B File Offset: 0x0000A56B
		void IServiceContainer.AddService(Type serviceType, ServiceCreatorCallback callback, bool promote)
		{
			this.SiteSpecificServices.AddService(serviceType, callback, promote);
		}

		// Token: 0x0600076F RID: 1903 RVA: 0x0000C37B File Offset: 0x0000A57B
		void IServiceContainer.RemoveService(Type serviceType)
		{
			this.SiteSpecificServices.RemoveService(serviceType);
		}

		// Token: 0x06000770 RID: 1904 RVA: 0x0000C389 File Offset: 0x0000A589
		void IServiceContainer.RemoveService(Type serviceType, bool promote)
		{
			this.SiteSpecificServices.RemoveService(serviceType, promote);
		}

		// Token: 0x06000771 RID: 1905 RVA: 0x0000C398 File Offset: 0x0000A598
		object IDictionaryService.GetKey(object value)
		{
			if (this._dictionary != null)
			{
				foreach (object obj in this._dictionary)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					if (value != null && value.Equals(dictionaryEntry.Value))
					{
						return dictionaryEntry.Key;
					}
				}
			}
			return null;
		}

		// Token: 0x06000772 RID: 1906 RVA: 0x0000C414 File Offset: 0x0000A614
		object IDictionaryService.GetValue(object key)
		{
			if (this._dictionary != null)
			{
				return this._dictionary[key];
			}
			return null;
		}

		// Token: 0x06000773 RID: 1907 RVA: 0x0000C42C File Offset: 0x0000A62C
		void IDictionaryService.SetValue(object key, object value)
		{
			if (this._dictionary == null)
			{
				this._dictionary = new Hashtable();
			}
			if (value == null)
			{
				this._dictionary.Remove(key);
			}
			this._dictionary[key] = value;
		}

		// Token: 0x06000774 RID: 1908 RVA: 0x0000C460 File Offset: 0x0000A660
		public virtual object GetService(Type service)
		{
			object obj = null;
			if (typeof(IDictionaryService) == service)
			{
				obj = this;
			}
			if (typeof(INestedContainer) == service)
			{
				if (this._nestedContainer == null)
				{
					this._nestedContainer = new DesignModeNestedContainer(this._component, null);
				}
				obj = this._nestedContainer;
			}
			if (obj == null && service != typeof(IServiceContainer) && this._siteSpecificServices != null)
			{
				obj = this._siteSpecificServices.GetService(service);
			}
			if (obj == null)
			{
				obj = this._serviceProvider.GetService(service);
			}
			return obj;
		}

		// Token: 0x0400018B RID: 395
		private IServiceProvider _serviceProvider;

		// Token: 0x0400018C RID: 396
		private IComponent _component;

		// Token: 0x0400018D RID: 397
		private IContainer _container;

		// Token: 0x0400018E RID: 398
		private string _componentName;

		// Token: 0x0400018F RID: 399
		private NestedContainer _nestedContainer;

		// Token: 0x04000190 RID: 400
		private ServiceContainer _siteSpecificServices;

		// Token: 0x04000191 RID: 401
		private Hashtable _dictionary;
	}
}
