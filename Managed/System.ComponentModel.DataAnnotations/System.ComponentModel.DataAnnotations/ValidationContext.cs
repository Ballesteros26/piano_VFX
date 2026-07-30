using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Globalization;

namespace System.ComponentModel.DataAnnotations
{
	/// <summary>Describes the context in which a validation check is performed.</summary>
	// Token: 0x0200003E RID: 62
	public sealed class ValidationContext : IServiceProvider
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DataAnnotations.ValidationContext" /> class using the specified object instance</summary>
		/// <param name="instance">The object instance to validate. It cannot be null.</param>
		// Token: 0x06000162 RID: 354 RVA: 0x00005010 File Offset: 0x00003210
		public ValidationContext(object instance)
			: this(instance, null, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DataAnnotations.ValidationContext" /> class using the specified object and an optional property bag.</summary>
		/// <param name="instance">The object instance to validate.  It cannot be null</param>
		/// <param name="items">An optional set of key/value pairs to make available to consumers.</param>
		// Token: 0x06000163 RID: 355 RVA: 0x0000501B File Offset: 0x0000321B
		public ValidationContext(object instance, IDictionary<object, object> items)
			: this(instance, null, items)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DataAnnotations.ValidationContext" /> class using the service provider and dictionary of service consumers. </summary>
		/// <param name="instance">The object to validate. This parameter is required.</param>
		/// <param name="serviceProvider">The object that implements the <see cref="T:System.IServiceProvider" /> interface. This parameter is optional.</param>
		/// <param name="items">A dictionary of key/value pairs to make available to the service consumers. This parameter is optional.</param>
		// Token: 0x06000164 RID: 356 RVA: 0x00005028 File Offset: 0x00003228
		public ValidationContext(object instance, IServiceProvider serviceProvider, IDictionary<object, object> items)
		{
			if (instance == null)
			{
				throw new ArgumentNullException("instance");
			}
			if (serviceProvider != null)
			{
				this.InitializeServiceProvider((Type serviceType) => serviceProvider.GetService(serviceType));
			}
			IServiceContainer serviceContainer = serviceProvider as IServiceContainer;
			if (serviceContainer != null)
			{
				this._serviceContainer = new ValidationContext.ValidationContextServiceContainer(serviceContainer);
			}
			else
			{
				this._serviceContainer = new ValidationContext.ValidationContextServiceContainer();
			}
			if (items != null)
			{
				this._items = new Dictionary<object, object>(items);
			}
			else
			{
				this._items = new Dictionary<object, object>();
			}
			this._objectInstance = instance;
		}

		/// <summary>Gets the object to validate.</summary>
		/// <returns>The object to validate.</returns>
		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000165 RID: 357 RVA: 0x000050BB File Offset: 0x000032BB
		public object ObjectInstance
		{
			get
			{
				return this._objectInstance;
			}
		}

		/// <summary>Gets the type of the object to validate.</summary>
		/// <returns>The type of the object to validate.</returns>
		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000166 RID: 358 RVA: 0x000050C3 File Offset: 0x000032C3
		public Type ObjectType
		{
			get
			{
				return this.ObjectInstance.GetType();
			}
		}

		/// <summary>Gets or sets the name of the member to validate. </summary>
		/// <returns>The name of the member to validate. </returns>
		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000167 RID: 359 RVA: 0x000050D0 File Offset: 0x000032D0
		// (set) Token: 0x06000168 RID: 360 RVA: 0x00005133 File Offset: 0x00003333
		public string DisplayName
		{
			get
			{
				if (string.IsNullOrEmpty(this._displayName))
				{
					this._displayName = this.GetDisplayName();
					if (string.IsNullOrEmpty(this._displayName))
					{
						this._displayName = this.MemberName;
						if (string.IsNullOrEmpty(this._displayName))
						{
							this._displayName = this.ObjectType.Name;
						}
					}
				}
				return this._displayName;
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					throw new ArgumentNullException("value");
				}
				this._displayName = value;
			}
		}

		/// <summary>Gets or sets the name of the member to validate. </summary>
		/// <returns>The name of the member to validate. </returns>
		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000169 RID: 361 RVA: 0x0000514F File Offset: 0x0000334F
		// (set) Token: 0x0600016A RID: 362 RVA: 0x00005157 File Offset: 0x00003357
		public string MemberName
		{
			get
			{
				return this._memberName;
			}
			set
			{
				this._memberName = value;
			}
		}

		/// <summary>Gets the dictionary of key/value pairs that is associated with this context.</summary>
		/// <returns>The dictionary of the key/value pairs for this context.</returns>
		// Token: 0x1700005B RID: 91
		// (get) Token: 0x0600016B RID: 363 RVA: 0x00005160 File Offset: 0x00003360
		public IDictionary<object, object> Items
		{
			get
			{
				return this._items;
			}
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00005168 File Offset: 0x00003368
		private string GetDisplayName()
		{
			string text = null;
			ValidationAttributeStore instance = ValidationAttributeStore.Instance;
			DisplayAttribute displayAttribute = null;
			if (string.IsNullOrEmpty(this._memberName))
			{
				displayAttribute = instance.GetTypeDisplayAttribute(this);
			}
			else if (instance.IsPropertyContext(this))
			{
				displayAttribute = instance.GetPropertyDisplayAttribute(this);
			}
			if (displayAttribute != null)
			{
				text = displayAttribute.GetName();
			}
			return text ?? this.MemberName;
		}

		/// <summary>Initializes the <see cref="T:System.ComponentModel.DataAnnotations.ValidationContext" /> using a service provider that can return service instances by type when GetService is called.</summary>
		/// <param name="serviceProvider">The service provider.</param>
		// Token: 0x0600016D RID: 365 RVA: 0x000051BC File Offset: 0x000033BC
		public void InitializeServiceProvider(Func<Type, object> serviceProvider)
		{
			this._serviceProvider = serviceProvider;
		}

		/// <summary>Returns the service that provides custom validation.</summary>
		/// <returns>An instance of the service, or null if the service is not available.</returns>
		/// <param name="serviceType">The type of the service to use for validation.</param>
		// Token: 0x0600016E RID: 366 RVA: 0x000051C8 File Offset: 0x000033C8
		public object GetService(Type serviceType)
		{
			object obj = null;
			if (this._serviceContainer != null)
			{
				obj = this._serviceContainer.GetService(serviceType);
			}
			if (obj == null && this._serviceProvider != null)
			{
				obj = this._serviceProvider(serviceType);
			}
			return obj;
		}

		/// <summary>Gets the validation services container.</summary>
		/// <returns>The validation services container.</returns>
		// Token: 0x1700005C RID: 92
		// (get) Token: 0x0600016F RID: 367 RVA: 0x00005205 File Offset: 0x00003405
		public IServiceContainer ServiceContainer
		{
			get
			{
				if (this._serviceContainer == null)
				{
					this._serviceContainer = new ValidationContext.ValidationContextServiceContainer();
				}
				return this._serviceContainer;
			}
		}

		// Token: 0x040000BF RID: 191
		private Func<Type, object> _serviceProvider;

		// Token: 0x040000C0 RID: 192
		private object _objectInstance;

		// Token: 0x040000C1 RID: 193
		private string _memberName;

		// Token: 0x040000C2 RID: 194
		private string _displayName;

		// Token: 0x040000C3 RID: 195
		private Dictionary<object, object> _items;

		// Token: 0x040000C4 RID: 196
		private IServiceContainer _serviceContainer;

		// Token: 0x0200003F RID: 63
		private class ValidationContextServiceContainer : IServiceContainer, IServiceProvider
		{
			// Token: 0x06000170 RID: 368 RVA: 0x00005220 File Offset: 0x00003420
			internal ValidationContextServiceContainer()
			{
			}

			// Token: 0x06000171 RID: 369 RVA: 0x0000523E File Offset: 0x0000343E
			internal ValidationContextServiceContainer(IServiceContainer parentContainer)
			{
				this._parentContainer = parentContainer;
			}

			// Token: 0x06000172 RID: 370 RVA: 0x00005264 File Offset: 0x00003464
			public void AddService(Type serviceType, ServiceCreatorCallback callback, bool promote)
			{
				if (promote && this._parentContainer != null)
				{
					this._parentContainer.AddService(serviceType, callback, promote);
					return;
				}
				object @lock = this._lock;
				lock (@lock)
				{
					if (this._services.ContainsKey(serviceType))
					{
						throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, "A service of type '{0}' already exists in the container.", serviceType), "serviceType");
					}
					this._services.Add(serviceType, callback);
				}
			}

			// Token: 0x06000173 RID: 371 RVA: 0x000052F0 File Offset: 0x000034F0
			public void AddService(Type serviceType, ServiceCreatorCallback callback)
			{
				this.AddService(serviceType, callback, true);
			}

			// Token: 0x06000174 RID: 372 RVA: 0x000052FC File Offset: 0x000034FC
			public void AddService(Type serviceType, object serviceInstance, bool promote)
			{
				if (promote && this._parentContainer != null)
				{
					this._parentContainer.AddService(serviceType, serviceInstance, promote);
					return;
				}
				object @lock = this._lock;
				lock (@lock)
				{
					if (this._services.ContainsKey(serviceType))
					{
						throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, "A service of type '{0}' already exists in the container.", serviceType), "serviceType");
					}
					this._services.Add(serviceType, serviceInstance);
				}
			}

			// Token: 0x06000175 RID: 373 RVA: 0x00005388 File Offset: 0x00003588
			public void AddService(Type serviceType, object serviceInstance)
			{
				this.AddService(serviceType, serviceInstance, true);
			}

			// Token: 0x06000176 RID: 374 RVA: 0x00005394 File Offset: 0x00003594
			public void RemoveService(Type serviceType, bool promote)
			{
				object @lock = this._lock;
				lock (@lock)
				{
					if (this._services.ContainsKey(serviceType))
					{
						this._services.Remove(serviceType);
					}
				}
				if (promote && this._parentContainer != null)
				{
					this._parentContainer.RemoveService(serviceType);
				}
			}

			// Token: 0x06000177 RID: 375 RVA: 0x00005400 File Offset: 0x00003600
			public void RemoveService(Type serviceType)
			{
				this.RemoveService(serviceType, true);
			}

			// Token: 0x06000178 RID: 376 RVA: 0x0000540C File Offset: 0x0000360C
			public object GetService(Type serviceType)
			{
				if (serviceType == null)
				{
					throw new ArgumentNullException("serviceType");
				}
				object obj = null;
				this._services.TryGetValue(serviceType, out obj);
				if (obj == null && this._parentContainer != null)
				{
					obj = this._parentContainer.GetService(serviceType);
				}
				ServiceCreatorCallback serviceCreatorCallback = obj as ServiceCreatorCallback;
				if (serviceCreatorCallback != null)
				{
					obj = serviceCreatorCallback(this, serviceType);
				}
				return obj;
			}

			// Token: 0x040000C5 RID: 197
			private IServiceContainer _parentContainer;

			// Token: 0x040000C6 RID: 198
			private Dictionary<Type, object> _services = new Dictionary<Type, object>();

			// Token: 0x040000C7 RID: 199
			private readonly object _lock = new object();
		}
	}
}
