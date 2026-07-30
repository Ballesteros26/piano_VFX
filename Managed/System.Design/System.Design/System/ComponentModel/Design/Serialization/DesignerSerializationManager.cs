using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity;

namespace System.ComponentModel.Design.Serialization
{
	/// <summary>Provides an implementation of the <see cref="T:System.ComponentModel.Design.Serialization.IDesignerSerializationManager" /> interface.</summary>
	// Token: 0x02000152 RID: 338
	public class DesignerSerializationManager : IDesignerSerializationManager, IServiceProvider
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.Serialization.DesignerSerializationManager" /> class.</summary>
		// Token: 0x06000A3B RID: 2619 RVA: 0x00014BF7 File Offset: 0x00012DF7
		public DesignerSerializationManager()
			: this(null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.Serialization.DesignerSerializationManager" /> class with the given service provider.</summary>
		/// <param name="provider">An <see cref="T:System.IServiceProvider" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="provider" /> is null.</exception>
		// Token: 0x06000A3C RID: 2620 RVA: 0x00014C00 File Offset: 0x00012E00
		public DesignerSerializationManager(IServiceProvider provider)
		{
			this._serviceProvider = provider;
			this._preserveNames = true;
			this._validateRecycledTypes = true;
		}

		/// <summary>Gets or sets a flag indicating whether <see cref="M:System.ComponentModel.Design.Serialization.DesignerSerializationManager.CreateInstance(System.Type,System.Collections.ICollection,System.String,System.Boolean)" /> will always create a new instance of a type. </summary>
		/// <returns>true if <see cref="M:System.ComponentModel.Design.Serialization.DesignerSerializationManager.CreateInstance(System.Type,System.Collections.ICollection,System.String,System.Boolean)" /> will return the existing instance; false if <see cref="M:System.ComponentModel.Design.Serialization.DesignerSerializationManager.CreateInstance(System.Type,System.Collections.ICollection,System.String,System.Boolean)" /> will create a new instance of a type. The default is false.</returns>
		/// <exception cref="T:System.InvalidOperationException">The serialization manager has an active serialization session.</exception>
		// Token: 0x1700021A RID: 538
		// (get) Token: 0x06000A3D RID: 2621 RVA: 0x00014C1D File Offset: 0x00012E1D
		// (set) Token: 0x06000A3E RID: 2622 RVA: 0x00014C25 File Offset: 0x00012E25
		public bool RecycleInstances
		{
			get
			{
				return this._recycleInstances;
			}
			set
			{
				this.VerifyNotInSession();
				this._recycleInstances = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="M:System.ComponentModel.Design.Serialization.DesignerSerializationManager.CreateInstance(System.Type,System.Collections.ICollection,System.String,System.Boolean)" /> method should check for the presence of the given name in the container.</summary>
		/// <returns>true if <see cref="M:System.ComponentModel.Design.Serialization.DesignerSerializationManager.CreateInstance(System.Type,System.Collections.ICollection,System.String,System.Boolean)" /> will pass the given component name; false if <see cref="M:System.ComponentModel.Design.Serialization.DesignerSerializationManager.CreateInstance(System.Type,System.Collections.ICollection,System.String,System.Boolean)" /> will check for the presence of the given name in the container. The default is true.</returns>
		/// <exception cref="T:System.InvalidOperationException">This property was changed from within a serialization session.</exception>
		// Token: 0x1700021B RID: 539
		// (get) Token: 0x06000A3F RID: 2623 RVA: 0x00014C34 File Offset: 0x00012E34
		// (set) Token: 0x06000A40 RID: 2624 RVA: 0x00014C3C File Offset: 0x00012E3C
		public bool PreserveNames
		{
			get
			{
				return this._preserveNames;
			}
			set
			{
				this.VerifyNotInSession();
				this._preserveNames = value;
			}
		}

		/// <summary>Gets or sets a flag indicating whether the <see cref="M:System.ComponentModel.Design.Serialization.DesignerSerializationManager.CreateInstance(System.Type,System.Collections.ICollection,System.String,System.Boolean)" /> method will verify that matching names refer to the same type.</summary>
		/// <returns>true if <see cref="M:System.ComponentModel.Design.Serialization.DesignerSerializationManager.CreateInstance(System.Type,System.Collections.ICollection,System.String,System.Boolean)" /> verifies types; otherwise, false if it does not. The default is true.</returns>
		/// <exception cref="T:System.InvalidOperationException">The serialization manager has an active serialization session.</exception>
		// Token: 0x1700021C RID: 540
		// (get) Token: 0x06000A41 RID: 2625 RVA: 0x00014C4B File Offset: 0x00012E4B
		// (set) Token: 0x06000A42 RID: 2626 RVA: 0x00014C53 File Offset: 0x00012E53
		public bool ValidateRecycledTypes
		{
			get
			{
				return this._validateRecycledTypes;
			}
			set
			{
				this.VerifyNotInSession();
				this._validateRecycledTypes = value;
			}
		}

		/// <summary>Gets or sets to the container for this serialization manager.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.IContainer" /> to which the serialization manager will add components.</returns>
		/// <exception cref="T:System.InvalidOperationException">The serialization manager has an active serialization session.</exception>
		// Token: 0x1700021D RID: 541
		// (get) Token: 0x06000A43 RID: 2627 RVA: 0x00014C62 File Offset: 0x00012E62
		// (set) Token: 0x06000A44 RID: 2628 RVA: 0x00014C92 File Offset: 0x00012E92
		public IContainer Container
		{
			get
			{
				if (this._designerContainer == null)
				{
					this._designerContainer = (this.GetService(typeof(IDesignerHost)) as IDesignerHost).Container;
				}
				return this._designerContainer;
			}
			set
			{
				this.VerifyNotInSession();
				this._designerContainer = value;
			}
		}

		/// <summary>Gets the object that should be used to provide properties to the serialization manager's <see cref="P:System.ComponentModel.Design.Serialization.IDesignerSerializationManager.Properties" /> property.</summary>
		/// <returns>The object that should be used to provide properties to the serialization manager's <see cref="P:System.ComponentModel.Design.Serialization.IDesignerSerializationManager.Properties" /> property.</returns>
		// Token: 0x1700021E RID: 542
		// (get) Token: 0x06000A45 RID: 2629 RVA: 0x00014CA1 File Offset: 0x00012EA1
		// (set) Token: 0x06000A46 RID: 2630 RVA: 0x00014CA9 File Offset: 0x00012EA9
		public object PropertyProvider
		{
			get
			{
				return this._propertyProvider;
			}
			set
			{
				this._propertyProvider = value;
			}
		}

		/// <summary>Gets the list of errors that occurred during serialization or deserialization.</summary>
		/// <returns>The list of errors that occurred during serialization or deserialization.</returns>
		/// <exception cref="T:System.InvalidOperationException">This property was accessed outside of a serialization session.</exception>
		// Token: 0x1700021F RID: 543
		// (get) Token: 0x06000A47 RID: 2631 RVA: 0x00014CB2 File Offset: 0x00012EB2
		public IList Errors
		{
			get
			{
				return this._errors;
			}
		}

		/// <summary>Occurs when a session is disposed.</summary>
		// Token: 0x1400003A RID: 58
		// (add) Token: 0x06000A48 RID: 2632 RVA: 0x00014CBC File Offset: 0x00012EBC
		// (remove) Token: 0x06000A49 RID: 2633 RVA: 0x00014CF4 File Offset: 0x00012EF4
		public event EventHandler SessionDisposed;

		/// <summary>Occurs when a session is created. </summary>
		// Token: 0x1400003B RID: 59
		// (add) Token: 0x06000A4A RID: 2634 RVA: 0x00014D2C File Offset: 0x00012F2C
		// (remove) Token: 0x06000A4B RID: 2635 RVA: 0x00014D64 File Offset: 0x00012F64
		public event EventHandler SessionCreated;

		/// <summary>Raises the <see cref="E:System.ComponentModel.Design.Serialization.DesignerSerializationManager.SessionCreated" /> event. </summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000A4C RID: 2636 RVA: 0x00014D99 File Offset: 0x00012F99
		protected virtual void OnSessionCreated(EventArgs e)
		{
			if (this.SessionCreated != null)
			{
				this.SessionCreated(this, e);
			}
		}

		/// <summary>Creates an instance of a type.</summary>
		/// <returns>A new instance of the type specified by <paramref name="type" />.</returns>
		/// <param name="type">The type to create an instance of.</param>
		/// <param name="arguments">The parameters of the type’s constructor. This can be null or an empty collection to invoke the default constructor.</param>
		/// <param name="name">A name to give the object. If null, the object will not be given a name, unless the object is added to a container and the container gives the object a name.</param>
		/// <param name="addToContainer">true to add the object to the container if the object implements <see cref="T:System.ComponentModel.IComponent" />; otherwise, false.</param>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">
		///   <paramref name="type" /> does not have a constructor that takes parameters contained in <paramref name="arguments" />.</exception>
		// Token: 0x06000A4D RID: 2637 RVA: 0x00014DB0 File Offset: 0x00012FB0
		protected virtual object CreateInstance(Type type, ICollection arguments, string name, bool addToContainer)
		{
			this.VerifyInSession();
			object obj = null;
			if (name != null && this._recycleInstances)
			{
				this._instancesByNameCache.TryGetValue(name, out obj);
				if (obj != null && this._validateRecycledTypes && obj.GetType() != type)
				{
					obj = null;
				}
			}
			if (obj == null || !this._recycleInstances)
			{
				obj = this.CreateInstance(type, arguments);
			}
			if (addToContainer && obj != null && this.Container != null && typeof(IComponent).IsAssignableFrom(type))
			{
				if (this._preserveNames)
				{
					this.Container.Add((IComponent)obj, name);
				}
				else if (name != null && this.Container.Components[name] != null)
				{
					this.Container.Add((IComponent)obj);
				}
				else
				{
					this.Container.Add((IComponent)obj, name);
				}
				ISite site = ((IComponent)obj).Site;
				if (site != null)
				{
					name = site.Name;
				}
			}
			if (obj != null && name != null)
			{
				this._instancesByNameCache[name] = obj;
				this._instancesByValueCache[obj] = name;
			}
			return obj;
		}

		// Token: 0x06000A4E RID: 2638 RVA: 0x00014EC8 File Offset: 0x000130C8
		private object CreateInstance(Type type, ICollection argsCollection)
		{
			object obj = null;
			object[] array = null;
			Type[] array2 = new Type[0];
			if (argsCollection != null)
			{
				array = new object[argsCollection.Count];
				array2 = new Type[argsCollection.Count];
				argsCollection.CopyTo(array, 0);
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i] == null)
					{
						array2[i] = null;
					}
					else
					{
						array2[i] = array[i].GetType();
					}
				}
			}
			ConstructorInfo constructor = type.GetConstructor(array2);
			if (constructor != null)
			{
				obj = constructor.Invoke(array);
			}
			return obj;
		}

		/// <summary>Gets the serializer for the given object type.</summary>
		/// <returns>The serializer for <paramref name="objectType" />, or null, if not found.</returns>
		/// <param name="objectType">The type of object for which to retrieve the serializer.</param>
		/// <param name="serializerType">The type of serializer to retrieve.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="objectType" /> or <paramref name="serializerType" /> is null.</exception>
		// Token: 0x06000A4F RID: 2639 RVA: 0x00014F4C File Offset: 0x0001314C
		public object GetSerializer(Type objectType, Type serializerType)
		{
			this.VerifyInSession();
			if (serializerType == null)
			{
				throw new ArgumentNullException("serializerType");
			}
			object obj = null;
			if (objectType != null)
			{
				this._serializersCache.TryGetValue(objectType, out obj);
				if (obj != null && !serializerType.IsAssignableFrom(obj.GetType()))
				{
					obj = null;
				}
				DefaultSerializationProviderAttribute defaultSerializationProviderAttribute = TypeDescriptor.GetAttributes(objectType)[typeof(DefaultSerializationProviderAttribute)] as DefaultSerializationProviderAttribute;
				if (defaultSerializationProviderAttribute != null && this.GetType(defaultSerializationProviderAttribute.ProviderTypeName) == serializerType)
				{
					object obj2 = Activator.CreateInstance(this.GetType(defaultSerializationProviderAttribute.ProviderTypeName), BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.CreateInstance, null, null, null);
					((IDesignerSerializationManager)this).AddSerializationProvider((IDesignerSerializationProvider)obj2);
				}
			}
			if (obj == null && objectType != null)
			{
				DesignerSerializerAttribute designerSerializerAttribute = TypeDescriptor.GetAttributes(objectType)[typeof(DesignerSerializerAttribute)] as DesignerSerializerAttribute;
				if (designerSerializerAttribute != null && this.GetType(designerSerializerAttribute.SerializerBaseTypeName) == serializerType)
				{
					try
					{
						obj = Activator.CreateInstance(this.GetType(designerSerializerAttribute.SerializerTypeName), BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.CreateInstance, null, null, null);
					}
					catch
					{
					}
				}
				if (obj != null)
				{
					this._serializersCache[objectType] = obj;
				}
			}
			if (obj == null && this._serializationProviders != null)
			{
				foreach (IDesignerSerializationProvider designerSerializationProvider in this._serializationProviders)
				{
					obj = designerSerializationProvider.GetSerializer(this, null, objectType, serializerType);
					if (obj != null)
					{
						break;
					}
				}
			}
			return obj;
		}

		// Token: 0x06000A50 RID: 2640 RVA: 0x000150CC File Offset: 0x000132CC
		private void VerifyInSession()
		{
			if (this._session == null)
			{
				throw new InvalidOperationException("Not in session.");
			}
		}

		// Token: 0x06000A51 RID: 2641 RVA: 0x000150E1 File Offset: 0x000132E1
		private void VerifyNotInSession()
		{
			if (this._session != null)
			{
				throw new InvalidOperationException("In session.");
			}
		}

		/// <summary>Creates a new serialization session.</summary>
		/// <returns>An <see cref="T:System.IDisposable" /> that represents a new serialization session.</returns>
		/// <exception cref="T:System.InvalidOperationException">The serialization manager is already within a session. This version of <see cref="T:System.ComponentModel.Design.Serialization.DesignerSerializationManager" /> does not support simultaneous sessions.</exception>
		// Token: 0x06000A52 RID: 2642 RVA: 0x000150F8 File Offset: 0x000132F8
		public IDisposable CreateSession()
		{
			this.VerifyNotInSession();
			this._errors = new ArrayList();
			this._session = new DesignerSerializationManager.Session(this);
			this._serializersCache = new Dictionary<Type, object>();
			this._instancesByNameCache = new Dictionary<string, object>();
			this._instancesByValueCache = new Dictionary<object, string>();
			this._contextStack = new ContextStack();
			this.OnSessionCreated(EventArgs.Empty);
			return this._session;
		}

		/// <summary>Raises the <see cref="E:System.ComponentModel.Design.Serialization.DesignerSerializationManager.SessionDisposed" /> event. </summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06000A53 RID: 2643 RVA: 0x00015160 File Offset: 0x00013360
		protected virtual void OnSessionDisposed(EventArgs e)
		{
			this._errors.Clear();
			this._errors = null;
			this._serializersCache.Clear();
			this._serializersCache = null;
			this._instancesByNameCache.Clear();
			this._instancesByNameCache = null;
			this._instancesByValueCache.Clear();
			this._instancesByValueCache = null;
			this._session = null;
			this._contextStack = null;
			this._resolveNameHandler = null;
			this._serializationCompleteHandler = null;
			if (this.SessionDisposed != null)
			{
				this.SessionDisposed(this, e);
			}
			if (this._serializationCompleteHandler != null)
			{
				this._serializationCompleteHandler(this, EventArgs.Empty);
			}
		}

		/// <summary>Gets the requested type.</summary>
		/// <returns>The requested type, or null if the type cannot be resolved.</returns>
		/// <param name="typeName">The name of the type to retrieve.</param>
		// Token: 0x06000A54 RID: 2644 RVA: 0x00015200 File Offset: 0x00013400
		protected virtual Type GetType(string typeName)
		{
			if (typeName == null)
			{
				throw new ArgumentNullException("typeName");
			}
			this.VerifyInSession();
			Type type = null;
			ITypeResolutionService typeResolutionService = this.GetService(typeof(ITypeResolutionService)) as ITypeResolutionService;
			if (typeResolutionService != null)
			{
				type = typeResolutionService.GetType(typeName);
			}
			if (type == null)
			{
				type = Type.GetType(typeName);
			}
			return type;
		}

		/// <summary>Raises the <see cref="E:System.ComponentModel.Design.Serialization.IDesignerSerializationManager.ResolveName" /> event. </summary>
		/// <param name="e">A <see cref="T:System.ComponentModel.Design.Serialization.ResolveNameEventArgs" /> that contains the event data. </param>
		// Token: 0x06000A55 RID: 2645 RVA: 0x00015255 File Offset: 0x00013455
		protected virtual void OnResolveName(ResolveNameEventArgs e)
		{
			if (this._resolveNameHandler != null)
			{
				this._resolveNameHandler(this, e);
			}
		}

		/// <summary>Adds a custom serialization provider to the serialization manager.</summary>
		/// <param name="provider">The serialization provider to add.</param>
		// Token: 0x06000A56 RID: 2646 RVA: 0x0001526C File Offset: 0x0001346C
		void IDesignerSerializationManager.AddSerializationProvider(IDesignerSerializationProvider provider)
		{
			if (this._serializationProviders == null)
			{
				this._serializationProviders = new List<IDesignerSerializationProvider>();
			}
			if (!this._serializationProviders.Contains(provider))
			{
				this._serializationProviders.Add(provider);
			}
		}

		/// <summary>Removes a previously added serialization provider.</summary>
		/// <param name="provider">The <see cref="T:System.ComponentModel.Design.Serialization.IDesignerSerializationProvider" /> to remove.</param>
		// Token: 0x06000A57 RID: 2647 RVA: 0x0001529B File Offset: 0x0001349B
		void IDesignerSerializationManager.RemoveSerializationProvider(IDesignerSerializationProvider provider)
		{
			if (this._serializationProviders != null)
			{
				this._serializationProviders.Remove(provider);
			}
		}

		/// <summary>Implements the <see cref="M:System.ComponentModel.Design.Serialization.IDesignerSerializationManager.CreateInstance(System.Type,System.Collections.ICollection,System.String,System.Boolean)" /> method.</summary>
		/// <returns>The newly created object instance.</returns>
		/// <param name="type">The data type to create. </param>
		/// <param name="arguments">The arguments to pass to the constructor for this type. </param>
		/// <param name="name">The name of the object. This name can be used to access the object later through <see cref="M:System.ComponentModel.Design.Serialization.IDesignerSerializationManager.GetInstance(System.String)" />. If null is passed, the object is still created but cannot be accessed by name. </param>
		/// <param name="addToContainer">true to add this object to the design container. The object must implement <see cref="T:System.ComponentModel.IComponent" /> for this to have any effect. </param>
		// Token: 0x06000A58 RID: 2648 RVA: 0x000152B2 File Offset: 0x000134B2
		object IDesignerSerializationManager.CreateInstance(Type type, ICollection arguments, string name, bool addToContainer)
		{
			return this.CreateInstance(type, arguments, name, addToContainer);
		}

		/// <summary>Retrieves an instance of a created object of the specified name.</summary>
		/// <returns>An instance of the object with the given name, or null if no object by that name can be found.</returns>
		/// <param name="name">The name of the object to retrieve.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">This property was accessed outside of a serialization session.</exception>
		// Token: 0x06000A59 RID: 2649 RVA: 0x000152C0 File Offset: 0x000134C0
		object IDesignerSerializationManager.GetInstance(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			this.VerifyInSession();
			object obj = null;
			this._instancesByNameCache.TryGetValue(name, out obj);
			if (obj == null && this.Container != null)
			{
				obj = this.Container.Components[name];
			}
			if (obj == null)
			{
				obj = this.RequestInstance(name);
			}
			return obj;
		}

		// Token: 0x06000A5A RID: 2650 RVA: 0x0001531C File Offset: 0x0001351C
		private object RequestInstance(string name)
		{
			ResolveNameEventArgs resolveNameEventArgs = new ResolveNameEventArgs(name);
			this.OnResolveName(resolveNameEventArgs);
			return resolveNameEventArgs.Value;
		}

		/// <summary>Gets a type of the specified name.</summary>
		/// <returns>An instance of the type, or null if the type cannot be loaded.</returns>
		/// <param name="typeName">The fully qualified name of the type to load.</param>
		/// <exception cref="T:System.InvalidOperationException">This property was accessed outside of a serialization session.</exception>
		// Token: 0x06000A5B RID: 2651 RVA: 0x0001533D File Offset: 0x0001353D
		Type IDesignerSerializationManager.GetType(string name)
		{
			return this.GetType(name);
		}

		/// <summary>Gets a serializer of the requested type for the specified object type.</summary>
		/// <returns>An instance of the requested serializer, or null if no appropriate serializer can be located.</returns>
		/// <param name="objectType">The type of the object to get the serializer for.</param>
		/// <param name="serializerType">The type of the serializer to retrieve.</param>
		// Token: 0x06000A5C RID: 2652 RVA: 0x00015346 File Offset: 0x00013546
		object IDesignerSerializationManager.GetSerializer(Type type, Type serializerType)
		{
			return this.GetSerializer(type, serializerType);
		}

		/// <summary>Retrieves a name for the specified object.</summary>
		/// <returns>The name of the object, or null if the object is unnamed.</returns>
		/// <param name="value">The object for which to retrieve the name.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">This property was accessed outside of a serialization session.</exception>
		// Token: 0x06000A5D RID: 2653 RVA: 0x00015350 File Offset: 0x00013550
		string IDesignerSerializationManager.GetName(object instance)
		{
			if (instance == null)
			{
				throw new ArgumentNullException("instance");
			}
			this.VerifyInSession();
			string text = null;
			if (instance is IComponent)
			{
				ISite site = ((IComponent)instance).Site;
				if (site != null && site is INestedSite)
				{
					text = ((INestedSite)site).FullName;
				}
				else if (site != null)
				{
					text = site.Name;
				}
			}
			if (text == null)
			{
				this._instancesByValueCache.TryGetValue(instance, out text);
			}
			return text;
		}

		/// <summary>Sets the name for the specified object.</summary>
		/// <param name="instance">The object to set the name.</param>
		/// <param name="name">A <see cref="T:System.String" /> used as the name of the object.</param>
		/// <exception cref="T:System.ArgumentNullException">One or both of the parameters are null.</exception>
		/// <exception cref="T:System.ArgumentException">The object specified by instance already has a name, or <paramref name="name" /> is already used by another named object.</exception>
		/// <exception cref="T:System.InvalidOperationException">This property was accessed outside of a serialization session.</exception>
		// Token: 0x06000A5E RID: 2654 RVA: 0x000153C0 File Offset: 0x000135C0
		void IDesignerSerializationManager.SetName(object instance, string name)
		{
			if (instance == null)
			{
				throw new ArgumentNullException("instance");
			}
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (this._instancesByNameCache.ContainsKey(name))
			{
				throw new ArgumentException("The object specified by instance already has a name, or name is already used by another named object.");
			}
			this._instancesByNameCache.Add(name, instance);
			this._instancesByValueCache.Add(instance, name);
		}

		/// <summary>Used to report a recoverable error in serialization.</summary>
		/// <param name="errorInformation">An object containing the error information, usually of type <see cref="T:System.String" /> or <see cref="T:System.Exception" />.</param>
		/// <exception cref="T:System.InvalidOperationException">This property was accessed outside of a serialization session.</exception>
		// Token: 0x06000A5F RID: 2655 RVA: 0x0001541C File Offset: 0x0001361C
		void IDesignerSerializationManager.ReportError(object error)
		{
			this.VerifyInSession();
			this._errors.Add(error);
		}

		/// <summary>Gets the context stack for this serialization session. </summary>
		/// <returns>A <see cref="T:System.ComponentModel.Design.Serialization.ContextStack" /> that stores data.</returns>
		/// <exception cref="T:System.InvalidOperationException">This property was accessed outside of a serialization session.</exception>
		// Token: 0x17000220 RID: 544
		// (get) Token: 0x06000A60 RID: 2656 RVA: 0x00015431 File Offset: 0x00013631
		ContextStack IDesignerSerializationManager.Context
		{
			get
			{
				return this._contextStack;
			}
		}

		/// <summary>Implements the <see cref="P:System.ComponentModel.Design.Serialization.IDesignerSerializationManager.Properties" /> property. </summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> containing the properties to be serialized.</returns>
		// Token: 0x17000221 RID: 545
		// (get) Token: 0x06000A61 RID: 2657 RVA: 0x0001543C File Offset: 0x0001363C
		PropertyDescriptorCollection IDesignerSerializationManager.Properties
		{
			get
			{
				PropertyDescriptorCollection propertyDescriptorCollection = new PropertyDescriptorCollection(new PropertyDescriptor[0]);
				object propertyProvider = this.PropertyProvider;
				if (propertyProvider != null)
				{
					propertyDescriptorCollection = TypeDescriptor.GetProperties(propertyProvider);
				}
				return propertyDescriptorCollection;
			}
		}

		/// <summary>Occurs when serialization is complete.</summary>
		/// <exception cref="T:System.InvalidOperationException">The serialization manager does not have an active serialization session.</exception>
		// Token: 0x1400003C RID: 60
		// (add) Token: 0x06000A62 RID: 2658 RVA: 0x00015467 File Offset: 0x00013667
		// (remove) Token: 0x06000A63 RID: 2659 RVA: 0x00015486 File Offset: 0x00013686
		event EventHandler IDesignerSerializationManager.SerializationComplete
		{
			add
			{
				this.VerifyInSession();
				this._serializationCompleteHandler = (EventHandler)Delegate.Combine(this._serializationCompleteHandler, value);
			}
			remove
			{
				this._serializationCompleteHandler = (EventHandler)Delegate.Remove(this._serializationCompleteHandler, value);
			}
		}

		/// <summary>Occurs when <see cref="M:System.ComponentModel.Design.Serialization.DesignerSerializationManager.System#ComponentModel#Design#Serialization#IDesignerSerializationManager#GetName(System.Object)" /> cannot locate the specified name in the serialization manager's name table. </summary>
		/// <exception cref="T:System.InvalidOperationException">The serialization manager does not have an active serialization session.</exception>
		// Token: 0x1400003D RID: 61
		// (add) Token: 0x06000A64 RID: 2660 RVA: 0x0001549F File Offset: 0x0001369F
		// (remove) Token: 0x06000A65 RID: 2661 RVA: 0x000154BE File Offset: 0x000136BE
		event ResolveNameEventHandler IDesignerSerializationManager.ResolveName
		{
			add
			{
				this.VerifyInSession();
				this._resolveNameHandler = (ResolveNameEventHandler)Delegate.Combine(this._resolveNameHandler, value);
			}
			remove
			{
				this._resolveNameHandler = (ResolveNameEventHandler)Delegate.Remove(this._resolveNameHandler, value);
			}
		}

		/// <summary>For a description of this member, see the <see cref="M:System.IServiceProvider.GetService(System.Type)" /> method.</summary>
		/// <returns>A service object of type <paramref name="serviceType" />.-or-null if there is no service object of type <paramref name="serviceType" />.</returns>
		/// <param name="serviceType">An object that specifies the type of service object to get.</param>
		// Token: 0x06000A66 RID: 2662 RVA: 0x000154D7 File Offset: 0x000136D7
		object IServiceProvider.GetService(Type serviceType)
		{
			return this.GetService(serviceType);
		}

		/// <summary>Gets the requested service.</summary>
		/// <returns>The requested service, or null if the service cannot be resolved.</returns>
		/// <param name="serviceType">The type of service to retrieve.</param>
		// Token: 0x06000A67 RID: 2663 RVA: 0x000154E0 File Offset: 0x000136E0
		protected virtual object GetService(Type serviceType)
		{
			object obj = null;
			if (this._serviceProvider != null)
			{
				obj = this._serviceProvider.GetService(serviceType);
			}
			return obj;
		}

		/// <summary>Gets the type corresponding to the specified type name.</summary>
		/// <returns>The specified type.</returns>
		/// <param name="typeName">The name of the type to get.</param>
		// Token: 0x06000A68 RID: 2664 RVA: 0x0000970B File Offset: 0x0000790B
		public Type GetRuntimeType(string typeName)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		// Token: 0x04000255 RID: 597
		private IServiceProvider _serviceProvider;

		// Token: 0x04000256 RID: 598
		private bool _preserveNames;

		// Token: 0x04000257 RID: 599
		private bool _validateRecycledTypes;

		// Token: 0x04000258 RID: 600
		private bool _recycleInstances;

		// Token: 0x04000259 RID: 601
		private IContainer _designerContainer;

		// Token: 0x0400025A RID: 602
		private object _propertyProvider;

		// Token: 0x0400025B RID: 603
		private DesignerSerializationManager.Session _session;

		// Token: 0x0400025C RID: 604
		private ArrayList _errors;

		// Token: 0x0400025D RID: 605
		private List<IDesignerSerializationProvider> _serializationProviders;

		// Token: 0x0400025E RID: 606
		private Dictionary<Type, object> _serializersCache;

		// Token: 0x0400025F RID: 607
		private Dictionary<string, object> _instancesByNameCache;

		// Token: 0x04000260 RID: 608
		private Dictionary<object, string> _instancesByValueCache;

		// Token: 0x04000261 RID: 609
		private ContextStack _contextStack;

		// Token: 0x04000264 RID: 612
		private EventHandler _serializationCompleteHandler;

		// Token: 0x04000265 RID: 613
		private ResolveNameEventHandler _resolveNameHandler;

		// Token: 0x02000153 RID: 339
		private class Session : IDisposable
		{
			// Token: 0x06000A69 RID: 2665 RVA: 0x00015505 File Offset: 0x00013705
			public Session(DesignerSerializationManager manager)
			{
				this._manager = manager;
			}

			// Token: 0x06000A6A RID: 2666 RVA: 0x00015514 File Offset: 0x00013714
			public void Dispose()
			{
				this._manager.OnSessionDisposed(EventArgs.Empty);
			}

			// Token: 0x04000266 RID: 614
			private DesignerSerializationManager _manager;
		}
	}
}
