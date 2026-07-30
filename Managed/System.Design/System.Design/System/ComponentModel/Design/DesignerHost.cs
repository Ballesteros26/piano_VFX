using System;
using System.Collections;
using System.ComponentModel.Design.Serialization;
using System.Reflection;
using System.Windows.Forms.Design;

namespace System.ComponentModel.Design
{
	// Token: 0x0200011F RID: 287
	internal sealed class DesignerHost : Container, IDesignerLoaderHost, IDesignerHost, IServiceContainer, IServiceProvider, IComponentChangeService
	{
		// Token: 0x06000851 RID: 2129 RVA: 0x0000DCCC File Offset: 0x0000BECC
		public DesignerHost(IServiceProvider serviceProvider)
		{
			if (serviceProvider == null)
			{
				throw new ArgumentNullException("serviceProvider");
			}
			this._serviceProvider = serviceProvider;
			this._serviceContainer = serviceProvider.GetService(typeof(IServiceContainer)) as IServiceContainer;
			this._designers = new Hashtable();
			this._transactions = new Stack();
			this._loading = true;
		}

		// Token: 0x06000852 RID: 2130 RVA: 0x0000DD2C File Offset: 0x0000BF2C
		public override void Add(IComponent component, string name)
		{
			this.AddPreProcess(component, name);
			base.Add(component, name);
			this.AddPostProcess(component, name);
		}

		// Token: 0x06000853 RID: 2131 RVA: 0x0000DD46 File Offset: 0x0000BF46
		internal void AddPreProcess(IComponent component, string name)
		{
			if (this.ComponentAdding != null)
			{
				this.ComponentAdding(this, new ComponentEventArgs(component));
			}
		}

		// Token: 0x06000854 RID: 2132 RVA: 0x0000DD64 File Offset: 0x0000BF64
		internal void AddPostProcess(IComponent component, string name)
		{
			IDesigner designer;
			if (this._rootComponent == null)
			{
				this._rootComponent = component;
				designer = this.CreateDesigner(component, true);
			}
			else
			{
				designer = this.CreateDesigner(component, false);
			}
			if (designer != null)
			{
				this._designers[component] = designer;
				designer.Initialize(component);
			}
			else
			{
				IUIService iuiservice = this.GetService(typeof(IUIService)) as IUIService;
				if (iuiservice != null)
				{
					iuiservice.ShowError("Unable to load a designer for component type '" + component.GetType().Name + "'");
				}
				this.DestroyComponent(component);
			}
			if (component == this._rootComponent)
			{
				this.Activate();
			}
			if (component is IExtenderProvider)
			{
				IExtenderProviderService extenderProviderService = this.GetService(typeof(IExtenderProviderService)) as IExtenderProviderService;
				if (extenderProviderService != null)
				{
					extenderProviderService.AddExtenderProvider((IExtenderProvider)component);
				}
			}
			if (this.ComponentAdded != null)
			{
				this.ComponentAdded(this, new ComponentEventArgs(component));
			}
		}

		// Token: 0x06000855 RID: 2133 RVA: 0x0000DE43 File Offset: 0x0000C043
		public override void Remove(IComponent component)
		{
			DesignerTransaction designerTransaction = this.CreateTransaction("Remove " + component.Site.Name);
			this.RemovePreProcess(component);
			base.Remove(component);
			this.RemovePostProcess(component);
			designerTransaction.Commit();
		}

		// Token: 0x06000856 RID: 2134 RVA: 0x0000DE7C File Offset: 0x0000C07C
		internal void RemovePreProcess(IComponent component)
		{
			if (!this._unloading && this.ComponentRemoving != null)
			{
				this.ComponentRemoving(this, new ComponentEventArgs(component));
			}
			IDesigner designer = this._designers[component] as IDesigner;
			if (designer != null)
			{
				designer.Dispose();
			}
			this._designers.Remove(component);
			if (component == this._rootComponent)
			{
				this._rootComponent = null;
			}
			if (component is IExtenderProvider)
			{
				IExtenderProviderService extenderProviderService = this.GetService(typeof(IExtenderProviderService)) as IExtenderProviderService;
				if (extenderProviderService != null)
				{
					extenderProviderService.RemoveExtenderProvider((IExtenderProvider)component);
				}
			}
		}

		// Token: 0x06000857 RID: 2135 RVA: 0x0000DF0F File Offset: 0x0000C10F
		internal void RemovePostProcess(IComponent component)
		{
			if (!this._unloading && this.ComponentRemoved != null)
			{
				this.ComponentRemoved(this, new ComponentEventArgs(component));
			}
		}

		// Token: 0x06000858 RID: 2136 RVA: 0x0000DF34 File Offset: 0x0000C134
		protected override ISite CreateSite(IComponent component, string name)
		{
			if (name == null)
			{
				INameCreationService nameCreationService = this.GetService(typeof(INameCreationService)) as INameCreationService;
				if (nameCreationService != null)
				{
					name = nameCreationService.CreateName(this, component.GetType());
				}
			}
			return new DesignModeSite(component, name, this, this);
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x06000859 RID: 2137 RVA: 0x0000DF75 File Offset: 0x0000C175
		public IContainer Container
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x0600085A RID: 2138 RVA: 0x0000DF78 File Offset: 0x0000C178
		public bool InTransaction
		{
			get
			{
				return this._transactions != null && this._transactions.Count > 0;
			}
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x0600085B RID: 2139 RVA: 0x0000DF93 File Offset: 0x0000C193
		public bool Loading
		{
			get
			{
				return this._loading;
			}
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x0600085C RID: 2140 RVA: 0x0000DF9B File Offset: 0x0000C19B
		public IComponent RootComponent
		{
			get
			{
				return this._rootComponent;
			}
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x0600085D RID: 2141 RVA: 0x0000DFA3 File Offset: 0x0000C1A3
		public string RootComponentClassName
		{
			get
			{
				if (this._rootComponent != null)
				{
					return this._rootComponent.GetType().AssemblyQualifiedName;
				}
				return null;
			}
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x0600085E RID: 2142 RVA: 0x0000DFBF File Offset: 0x0000C1BF
		public string TransactionDescription
		{
			get
			{
				if (this._transactions != null && this._transactions.Count > 0)
				{
					return ((DesignerHost.DesignerHostTransaction)this._transactions.Peek()).Description;
				}
				return null;
			}
		}

		// Token: 0x0600085F RID: 2143 RVA: 0x0000DFF0 File Offset: 0x0000C1F0
		public void Activate()
		{
			ISelectionService selectionService = this.GetService(typeof(ISelectionService)) as ISelectionService;
			if (selectionService != null)
			{
				selectionService.SetSelectedComponents(new IComponent[] { this._rootComponent });
			}
			if (this.Activated != null)
			{
				this.Activated(this, EventArgs.Empty);
			}
		}

		// Token: 0x06000860 RID: 2144 RVA: 0x0000E044 File Offset: 0x0000C244
		public IComponent CreateComponent(Type componentClass)
		{
			return this.CreateComponent(componentClass, null);
		}

		// Token: 0x06000861 RID: 2145 RVA: 0x0000E050 File Offset: 0x0000C250
		public IComponent CreateComponent(Type componentClass, string name)
		{
			if (componentClass == null)
			{
				throw new ArgumentNullException("componentClass");
			}
			if (!typeof(IComponent).IsAssignableFrom(componentClass))
			{
				throw new ArgumentException("componentClass");
			}
			IComponent component = this.CreateInstance(componentClass) as IComponent;
			this.Add(component, name);
			return component;
		}

		// Token: 0x06000862 RID: 2146 RVA: 0x0000E0A4 File Offset: 0x0000C2A4
		internal object CreateInstance(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			return Activator.CreateInstance(type, BindingFlags.Instance | BindingFlags.Public | BindingFlags.CreateInstance, null, null, null);
		}

		// Token: 0x06000863 RID: 2147 RVA: 0x0000E0C8 File Offset: 0x0000C2C8
		internal IDesigner CreateDesigner(IComponent component, bool rootDesigner)
		{
			if (component == null)
			{
				throw new ArgumentNullException("component");
			}
			if (rootDesigner)
			{
				return this.CreateDesigner(component, typeof(IRootDesigner));
			}
			return this.CreateDesigner(component, typeof(IDesigner));
		}

		// Token: 0x06000864 RID: 2148 RVA: 0x0000E100 File Offset: 0x0000C300
		private IDesigner CreateDesigner(IComponent component, Type designerBaseType)
		{
			IDesigner designer = null;
			foreach (object obj in TypeDescriptor.GetAttributes(component))
			{
				DesignerAttribute designerAttribute = ((Attribute)obj) as DesignerAttribute;
				if (designerAttribute != null && (designerBaseType.FullName == designerAttribute.DesignerBaseTypeName || designerBaseType.AssemblyQualifiedName == designerAttribute.DesignerBaseTypeName))
				{
					Type type = Type.GetType(designerAttribute.DesignerTypeName);
					if (type == null && designerBaseType == typeof(IRootDesigner))
					{
						type = typeof(DocumentDesigner);
					}
					if (type != null)
					{
						designer = (IDesigner)Activator.CreateInstance(type);
						break;
					}
					break;
				}
			}
			if (designer == null)
			{
				Type type2 = component.GetType().BaseType;
				do
				{
					foreach (object obj2 in TypeDescriptor.GetAttributes(type2))
					{
						DesignerAttribute designerAttribute2 = ((Attribute)obj2) as DesignerAttribute;
						if (designerAttribute2 != null && (designerBaseType.FullName == designerAttribute2.DesignerBaseTypeName || designerBaseType.AssemblyQualifiedName == designerAttribute2.DesignerBaseTypeName))
						{
							Type type3 = Type.GetType(designerAttribute2.DesignerTypeName);
							if (type3 != null)
							{
								designer = (IDesigner)Activator.CreateInstance(type3);
								break;
							}
							break;
						}
					}
					type2 = type2.BaseType;
				}
				while (designer == null && type2 != null);
			}
			return designer;
		}

		// Token: 0x06000865 RID: 2149 RVA: 0x0000E2A0 File Offset: 0x0000C4A0
		public void DestroyComponent(IComponent component)
		{
			if (component.Site != null && component.Site.Container == this)
			{
				this.Remove(component);
				component.Dispose();
			}
		}

		// Token: 0x06000866 RID: 2150 RVA: 0x0000E2C5 File Offset: 0x0000C4C5
		public IDesigner GetDesigner(IComponent component)
		{
			if (component == null)
			{
				throw new ArgumentNullException("component");
			}
			return this._designers[component] as IDesigner;
		}

		// Token: 0x06000867 RID: 2151 RVA: 0x0000E2E6 File Offset: 0x0000C4E6
		public DesignerTransaction CreateTransaction()
		{
			return this.CreateTransaction(null);
		}

		// Token: 0x06000868 RID: 2152 RVA: 0x0000E2F0 File Offset: 0x0000C4F0
		public DesignerTransaction CreateTransaction(string description)
		{
			if (this.TransactionOpening != null)
			{
				this.TransactionOpening(this, EventArgs.Empty);
			}
			DesignerHost.DesignerHostTransaction designerHostTransaction = new DesignerHost.DesignerHostTransaction(this, description);
			this._transactions.Push(designerHostTransaction);
			if (this.TransactionOpened != null)
			{
				this.TransactionOpened(this, EventArgs.Empty);
			}
			return designerHostTransaction;
		}

		// Token: 0x06000869 RID: 2153 RVA: 0x0000E344 File Offset: 0x0000C544
		public Type GetType(string typeName)
		{
			ITypeResolutionService typeResolutionService = this.GetService(typeof(ITypeResolutionService)) as ITypeResolutionService;
			Type type;
			if (typeResolutionService != null)
			{
				type = typeResolutionService.GetType(typeName);
			}
			else
			{
				type = Type.GetType(typeName);
			}
			return type;
		}

		// Token: 0x0600086A RID: 2154 RVA: 0x0000E37C File Offset: 0x0000C57C
		protected override void Dispose(bool disposing)
		{
			this.Unload();
			base.Dispose(disposing);
		}

		// Token: 0x14000021 RID: 33
		// (add) Token: 0x0600086B RID: 2155 RVA: 0x0000E38C File Offset: 0x0000C58C
		// (remove) Token: 0x0600086C RID: 2156 RVA: 0x0000E3C4 File Offset: 0x0000C5C4
		public event EventHandler Activated;

		// Token: 0x14000022 RID: 34
		// (add) Token: 0x0600086D RID: 2157 RVA: 0x0000E3FC File Offset: 0x0000C5FC
		// (remove) Token: 0x0600086E RID: 2158 RVA: 0x0000E434 File Offset: 0x0000C634
		public event EventHandler Deactivated;

		// Token: 0x14000023 RID: 35
		// (add) Token: 0x0600086F RID: 2159 RVA: 0x0000E46C File Offset: 0x0000C66C
		// (remove) Token: 0x06000870 RID: 2160 RVA: 0x0000E4A4 File Offset: 0x0000C6A4
		public event EventHandler LoadComplete;

		// Token: 0x14000024 RID: 36
		// (add) Token: 0x06000871 RID: 2161 RVA: 0x0000E4DC File Offset: 0x0000C6DC
		// (remove) Token: 0x06000872 RID: 2162 RVA: 0x0000E514 File Offset: 0x0000C714
		public event DesignerTransactionCloseEventHandler TransactionClosed;

		// Token: 0x14000025 RID: 37
		// (add) Token: 0x06000873 RID: 2163 RVA: 0x0000E54C File Offset: 0x0000C74C
		// (remove) Token: 0x06000874 RID: 2164 RVA: 0x0000E584 File Offset: 0x0000C784
		public event DesignerTransactionCloseEventHandler TransactionClosing;

		// Token: 0x14000026 RID: 38
		// (add) Token: 0x06000875 RID: 2165 RVA: 0x0000E5BC File Offset: 0x0000C7BC
		// (remove) Token: 0x06000876 RID: 2166 RVA: 0x0000E5F4 File Offset: 0x0000C7F4
		public event EventHandler TransactionOpened;

		// Token: 0x14000027 RID: 39
		// (add) Token: 0x06000877 RID: 2167 RVA: 0x0000E62C File Offset: 0x0000C82C
		// (remove) Token: 0x06000878 RID: 2168 RVA: 0x0000E664 File Offset: 0x0000C864
		public event EventHandler TransactionOpening;

		// Token: 0x06000879 RID: 2169 RVA: 0x0000E69C File Offset: 0x0000C89C
		private void OnTransactionClosing(DesignerHost.DesignerHostTransaction raiser, DesignerHost.TransactionAction action)
		{
			bool flag = false;
			bool flag2 = false;
			if (this._transactions.Peek() != raiser)
			{
				throw new InvalidOperationException("Current transaction differs from the one a commit was requested for.");
			}
			if (this._transactions.Count == 1)
			{
				flag2 = true;
			}
			if (action == DesignerHost.TransactionAction.Commit)
			{
				flag = true;
			}
			if (this.TransactionClosing != null)
			{
				this.TransactionClosing(this, new DesignerTransactionCloseEventArgs(flag, flag2));
			}
		}

		// Token: 0x0600087A RID: 2170 RVA: 0x0000E6F8 File Offset: 0x0000C8F8
		private void OnTransactionClosed(DesignerHost.DesignerHostTransaction raiser, DesignerHost.TransactionAction action)
		{
			bool flag = false;
			bool flag2 = false;
			if (this._transactions.Peek() != raiser)
			{
				throw new InvalidOperationException("Current transaction differs from the one a commit was requested for.");
			}
			if (this._transactions.Count == 1)
			{
				flag2 = true;
			}
			if (action == DesignerHost.TransactionAction.Commit)
			{
				flag = true;
			}
			this._transactions.Pop();
			if (this.TransactionClosed != null)
			{
				this.TransactionClosed(this, new DesignerTransactionCloseEventArgs(flag, flag2));
			}
		}

		// Token: 0x14000028 RID: 40
		// (add) Token: 0x0600087B RID: 2171 RVA: 0x0000E760 File Offset: 0x0000C960
		// (remove) Token: 0x0600087C RID: 2172 RVA: 0x0000E798 File Offset: 0x0000C998
		internal event LoadedEventHandler DesignerLoaderHostLoaded;

		// Token: 0x14000029 RID: 41
		// (add) Token: 0x0600087D RID: 2173 RVA: 0x0000E7D0 File Offset: 0x0000C9D0
		// (remove) Token: 0x0600087E RID: 2174 RVA: 0x0000E808 File Offset: 0x0000CA08
		internal event EventHandler DesignerLoaderHostLoading;

		// Token: 0x1400002A RID: 42
		// (add) Token: 0x0600087F RID: 2175 RVA: 0x0000E840 File Offset: 0x0000CA40
		// (remove) Token: 0x06000880 RID: 2176 RVA: 0x0000E878 File Offset: 0x0000CA78
		internal event EventHandler DesignerLoaderHostUnloading;

		// Token: 0x1400002B RID: 43
		// (add) Token: 0x06000881 RID: 2177 RVA: 0x0000E8B0 File Offset: 0x0000CAB0
		// (remove) Token: 0x06000882 RID: 2178 RVA: 0x0000E8E8 File Offset: 0x0000CAE8
		internal event EventHandler DesignerLoaderHostUnloaded;

		// Token: 0x06000883 RID: 2179 RVA: 0x0000E91D File Offset: 0x0000CB1D
		public void EndLoad(string rootClassName, bool successful, ICollection errorCollection)
		{
			if (this.DesignerLoaderHostLoaded != null)
			{
				this.DesignerLoaderHostLoaded(this, new LoadedEventArgs(successful, errorCollection));
			}
			if (this.LoadComplete != null)
			{
				this.LoadComplete(this, EventArgs.Empty);
			}
			this._loading = false;
		}

		// Token: 0x06000884 RID: 2180 RVA: 0x0000E95A File Offset: 0x0000CB5A
		public void Reload()
		{
			this._loading = true;
			this.Unload();
			if (this.DesignerLoaderHostLoading != null)
			{
				this.DesignerLoaderHostLoading(this, EventArgs.Empty);
			}
		}

		// Token: 0x06000885 RID: 2181 RVA: 0x0000E984 File Offset: 0x0000CB84
		private void Unload()
		{
			this._unloading = true;
			if (this.DesignerLoaderHostUnloading != null)
			{
				this.DesignerLoaderHostUnloading(this, EventArgs.Empty);
			}
			IComponent[] array = new IComponent[this.Components.Count];
			this.Components.CopyTo(array, 0);
			foreach (IComponent component in array)
			{
				this.Remove(component);
			}
			this._transactions.Clear();
			if (this.DesignerLoaderHostUnloaded != null)
			{
				this.DesignerLoaderHostUnloaded(this, EventArgs.Empty);
			}
			this._unloading = false;
		}

		// Token: 0x1400002C RID: 44
		// (add) Token: 0x06000886 RID: 2182 RVA: 0x0000EA18 File Offset: 0x0000CC18
		// (remove) Token: 0x06000887 RID: 2183 RVA: 0x0000EA50 File Offset: 0x0000CC50
		public event ComponentEventHandler ComponentAdded;

		// Token: 0x1400002D RID: 45
		// (add) Token: 0x06000888 RID: 2184 RVA: 0x0000EA88 File Offset: 0x0000CC88
		// (remove) Token: 0x06000889 RID: 2185 RVA: 0x0000EAC0 File Offset: 0x0000CCC0
		public event ComponentEventHandler ComponentAdding;

		// Token: 0x1400002E RID: 46
		// (add) Token: 0x0600088A RID: 2186 RVA: 0x0000EAF8 File Offset: 0x0000CCF8
		// (remove) Token: 0x0600088B RID: 2187 RVA: 0x0000EB30 File Offset: 0x0000CD30
		public event ComponentChangedEventHandler ComponentChanged;

		// Token: 0x1400002F RID: 47
		// (add) Token: 0x0600088C RID: 2188 RVA: 0x0000EB68 File Offset: 0x0000CD68
		// (remove) Token: 0x0600088D RID: 2189 RVA: 0x0000EBA0 File Offset: 0x0000CDA0
		public event ComponentChangingEventHandler ComponentChanging;

		// Token: 0x14000030 RID: 48
		// (add) Token: 0x0600088E RID: 2190 RVA: 0x0000EBD8 File Offset: 0x0000CDD8
		// (remove) Token: 0x0600088F RID: 2191 RVA: 0x0000EC10 File Offset: 0x0000CE10
		public event ComponentEventHandler ComponentRemoved;

		// Token: 0x14000031 RID: 49
		// (add) Token: 0x06000890 RID: 2192 RVA: 0x0000EC48 File Offset: 0x0000CE48
		// (remove) Token: 0x06000891 RID: 2193 RVA: 0x0000EC80 File Offset: 0x0000CE80
		public event ComponentEventHandler ComponentRemoving;

		// Token: 0x14000032 RID: 50
		// (add) Token: 0x06000892 RID: 2194 RVA: 0x0000ECB8 File Offset: 0x0000CEB8
		// (remove) Token: 0x06000893 RID: 2195 RVA: 0x0000ECF0 File Offset: 0x0000CEF0
		public event ComponentRenameEventHandler ComponentRename;

		// Token: 0x06000894 RID: 2196 RVA: 0x0000ED25 File Offset: 0x0000CF25
		public void OnComponentChanged(object component, MemberDescriptor member, object oldValue, object newValue)
		{
			if (this.ComponentChanged != null)
			{
				this.ComponentChanged(this, new ComponentChangedEventArgs(component, member, oldValue, newValue));
			}
		}

		// Token: 0x06000895 RID: 2197 RVA: 0x0000ED45 File Offset: 0x0000CF45
		public void OnComponentChanging(object component, MemberDescriptor member)
		{
			if (this.ComponentChanging != null)
			{
				this.ComponentChanging(this, new ComponentChangingEventArgs(component, member));
			}
		}

		// Token: 0x06000896 RID: 2198 RVA: 0x0000ED62 File Offset: 0x0000CF62
		internal void OnComponentRename(object component, string oldName, string newName)
		{
			if (this.ComponentRename != null)
			{
				this.ComponentRename(this, new ComponentRenameEventArgs(component, oldName, newName));
			}
		}

		// Token: 0x06000897 RID: 2199 RVA: 0x0000ED80 File Offset: 0x0000CF80
		public void AddService(Type serviceType, object serviceInstance)
		{
			this._serviceContainer.AddService(serviceType, serviceInstance);
		}

		// Token: 0x06000898 RID: 2200 RVA: 0x0000ED8F File Offset: 0x0000CF8F
		public void AddService(Type serviceType, object serviceInstance, bool promote)
		{
			this._serviceContainer.AddService(serviceType, serviceInstance, promote);
		}

		// Token: 0x06000899 RID: 2201 RVA: 0x0000ED9F File Offset: 0x0000CF9F
		public void AddService(Type serviceType, ServiceCreatorCallback callback)
		{
			this._serviceContainer.AddService(serviceType, callback);
		}

		// Token: 0x0600089A RID: 2202 RVA: 0x0000EDAE File Offset: 0x0000CFAE
		public void AddService(Type serviceType, ServiceCreatorCallback callback, bool promote)
		{
			this._serviceContainer.AddService(serviceType, callback, promote);
		}

		// Token: 0x0600089B RID: 2203 RVA: 0x0000EDBE File Offset: 0x0000CFBE
		public void RemoveService(Type serviceType)
		{
			this._serviceContainer.RemoveService(serviceType);
		}

		// Token: 0x0600089C RID: 2204 RVA: 0x0000EDCC File Offset: 0x0000CFCC
		public void RemoveService(Type serviceType, bool promote)
		{
			this._serviceContainer.RemoveService(serviceType, promote);
		}

		// Token: 0x0600089D RID: 2205 RVA: 0x0000EDDB File Offset: 0x0000CFDB
		public new object GetService(Type serviceType)
		{
			if (this._serviceProvider != null)
			{
				return this._serviceProvider.GetService(serviceType);
			}
			return null;
		}

		// Token: 0x040001D0 RID: 464
		private IServiceProvider _serviceProvider;

		// Token: 0x040001D1 RID: 465
		private Hashtable _designers;

		// Token: 0x040001D2 RID: 466
		private Stack _transactions;

		// Token: 0x040001D3 RID: 467
		private IServiceContainer _serviceContainer;

		// Token: 0x040001D4 RID: 468
		private bool _loading;

		// Token: 0x040001D5 RID: 469
		private bool _unloading;

		// Token: 0x040001D6 RID: 470
		private IComponent _rootComponent;

		// Token: 0x02000120 RID: 288
		private enum TransactionAction
		{
			// Token: 0x040001EA RID: 490
			Commit,
			// Token: 0x040001EB RID: 491
			Cancel
		}

		// Token: 0x02000121 RID: 289
		private sealed class DesignerHostTransaction : DesignerTransaction
		{
			// Token: 0x0600089E RID: 2206 RVA: 0x0000EDF3 File Offset: 0x0000CFF3
			public DesignerHostTransaction(DesignerHost host, string description)
				: base(description)
			{
				this._designerHost = host;
			}

			// Token: 0x0600089F RID: 2207 RVA: 0x0000EE03 File Offset: 0x0000D003
			protected override void OnCancel()
			{
				this._designerHost.OnTransactionClosing(this, DesignerHost.TransactionAction.Cancel);
				this._designerHost.OnTransactionClosed(this, DesignerHost.TransactionAction.Cancel);
			}

			// Token: 0x060008A0 RID: 2208 RVA: 0x0000EE1F File Offset: 0x0000D01F
			protected override void OnCommit()
			{
				this._designerHost.OnTransactionClosing(this, DesignerHost.TransactionAction.Commit);
				this._designerHost.OnTransactionClosed(this, DesignerHost.TransactionAction.Commit);
			}

			// Token: 0x040001EC RID: 492
			private DesignerHost _designerHost;
		}
	}
}
