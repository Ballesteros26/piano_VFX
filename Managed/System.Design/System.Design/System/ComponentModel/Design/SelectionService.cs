using System;
using System.Collections;
using System.Windows.Forms;

namespace System.ComponentModel.Design
{
	// Token: 0x02000137 RID: 311
	internal class SelectionService : ISelectionService
	{
		// Token: 0x06000923 RID: 2339 RVA: 0x0000FAE8 File Offset: 0x0000DCE8
		public SelectionService(IServiceProvider provider)
		{
			this._serviceProvider = provider;
			this._selection = new ArrayList();
			IComponentChangeService componentChangeService = provider.GetService(typeof(IComponentChangeService)) as IComponentChangeService;
			if (componentChangeService != null)
			{
				componentChangeService.ComponentRemoving += this.OnComponentRemoving;
			}
		}

		// Token: 0x06000924 RID: 2340 RVA: 0x0000FB38 File Offset: 0x0000DD38
		private void OnComponentRemoving(object sender, ComponentEventArgs args)
		{
			if (this.GetComponentSelected(args.Component))
			{
				this.SetSelectedComponents(new IComponent[] { args.Component }, SelectionTypes.Remove);
			}
		}

		// Token: 0x14000034 RID: 52
		// (add) Token: 0x06000925 RID: 2341 RVA: 0x0000FB64 File Offset: 0x0000DD64
		// (remove) Token: 0x06000926 RID: 2342 RVA: 0x0000FB9C File Offset: 0x0000DD9C
		public event EventHandler SelectionChanging;

		// Token: 0x14000035 RID: 53
		// (add) Token: 0x06000927 RID: 2343 RVA: 0x0000FBD4 File Offset: 0x0000DDD4
		// (remove) Token: 0x06000928 RID: 2344 RVA: 0x0000FC0C File Offset: 0x0000DE0C
		public event EventHandler SelectionChanged;

		// Token: 0x06000929 RID: 2345 RVA: 0x0000FC41 File Offset: 0x0000DE41
		public ICollection GetSelectedComponents()
		{
			if (this._selection != null)
			{
				return this._selection.ToArray();
			}
			return new object[0];
		}

		// Token: 0x0600092A RID: 2346 RVA: 0x0000FC5D File Offset: 0x0000DE5D
		protected virtual void OnSelectionChanging()
		{
			if (this.SelectionChanging != null)
			{
				this.SelectionChanging(this, EventArgs.Empty);
			}
		}

		// Token: 0x0600092B RID: 2347 RVA: 0x0000FC78 File Offset: 0x0000DE78
		protected virtual void OnSelectionChanged()
		{
			if (this.SelectionChanged != null)
			{
				this.SelectionChanged(this, EventArgs.Empty);
			}
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x0600092C RID: 2348 RVA: 0x0000FC93 File Offset: 0x0000DE93
		public object PrimarySelection
		{
			get
			{
				return this._primarySelection;
			}
		}

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x0600092D RID: 2349 RVA: 0x0000FC9B File Offset: 0x0000DE9B
		public int SelectionCount
		{
			get
			{
				if (this._selection != null)
				{
					return this._selection.Count;
				}
				return 0;
			}
		}

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x0600092E RID: 2350 RVA: 0x0000FCB4 File Offset: 0x0000DEB4
		private IComponent RootComponent
		{
			get
			{
				if (this._serviceProvider != null)
				{
					IDesignerHost designerHost = this._serviceProvider.GetService(typeof(IDesignerHost)) as IDesignerHost;
					if (designerHost != null)
					{
						return designerHost.RootComponent;
					}
				}
				return null;
			}
		}

		// Token: 0x0600092F RID: 2351 RVA: 0x0000FCEF File Offset: 0x0000DEEF
		public bool GetComponentSelected(object component)
		{
			return this._selection != null && this._selection.Contains(component);
		}

		// Token: 0x06000930 RID: 2352 RVA: 0x0000FD07 File Offset: 0x0000DF07
		public void SetSelectedComponents(ICollection components)
		{
			this.SetSelectedComponents(components, SelectionTypes.Auto);
		}

		// Token: 0x06000931 RID: 2353 RVA: 0x0000FD14 File Offset: 0x0000DF14
		public void SetSelectedComponents(ICollection components, SelectionTypes selectionType)
		{
			bool flag5;
			bool flag4;
			bool flag3;
			bool flag2;
			bool flag = (flag2 = (flag3 = (flag4 = (flag5 = false))));
			this.OnSelectionChanging();
			if (this._selection == null)
			{
				throw new InvalidOperationException("_selection == null");
			}
			if (components == null || components.Count == 0)
			{
				components = new ArrayList();
				((ArrayList)components).Add(this.RootComponent);
				selectionType = SelectionTypes.Replace;
			}
			if (!Enum.IsDefined(typeof(SelectionTypes), selectionType))
			{
				selectionType = SelectionTypes.Auto;
			}
			if ((selectionType & SelectionTypes.Auto) == SelectionTypes.Auto)
			{
				if ((Control.ModifierKeys & 131072) == 131072 || (Control.ModifierKeys & 65536) == 65536)
				{
					flag5 = true;
				}
				else if (components.Count == 1)
				{
					object obj = null;
					using (IEnumerator enumerator = components.GetEnumerator())
					{
						if (enumerator.MoveNext())
						{
							obj = enumerator.Current;
						}
					}
					if (this.GetComponentSelected(obj))
					{
						flag2 = true;
					}
					else
					{
						flag4 = true;
					}
				}
				else
				{
					flag4 = true;
				}
			}
			else
			{
				flag2 = (selectionType & SelectionTypes.Click) == SelectionTypes.Click;
				flag = (selectionType & SelectionTypes.Add) == SelectionTypes.Add;
				flag3 = (selectionType & SelectionTypes.Remove) == SelectionTypes.Remove;
				flag5 = (selectionType & SelectionTypes.Toggle) == SelectionTypes.Toggle;
				flag4 = (selectionType & SelectionTypes.Replace) == SelectionTypes.Replace;
			}
			if (flag4)
			{
				this._selection.Clear();
				flag = true;
			}
			if (flag)
			{
				foreach (object obj2 in components)
				{
					if (obj2 is IComponent && !this._selection.Contains(obj2))
					{
						this._selection.Add(obj2);
						this._primarySelection = (IComponent)obj2;
					}
				}
			}
			if (flag3)
			{
				bool flag6 = false;
				foreach (object obj3 in components)
				{
					if (obj3 is IComponent && this._selection.Contains(obj3))
					{
						this._selection.Remove(obj3);
					}
					if (obj3 == this.RootComponent)
					{
						flag6 = true;
					}
				}
				if (this._selection.Count == 0)
				{
					if (flag6)
					{
						this._primarySelection = null;
					}
					else
					{
						this._primarySelection = this.RootComponent;
						this._selection.Add(this.RootComponent);
					}
				}
			}
			if (flag5)
			{
				foreach (object obj4 in components)
				{
					if (obj4 is IComponent)
					{
						if (this._selection.Contains(obj4))
						{
							this._selection.Remove(obj4);
							if (obj4 == this._primarySelection)
							{
								this._primarySelection = this.RootComponent;
							}
						}
						else
						{
							this._selection.Add(obj4);
							this._primarySelection = (IComponent)obj4;
						}
					}
				}
			}
			if (flag2)
			{
				object obj5 = null;
				using (IEnumerator enumerator = components.GetEnumerator())
				{
					if (enumerator.MoveNext())
					{
						obj5 = enumerator.Current;
					}
				}
				if (!this.GetComponentSelected(obj5))
				{
					this._selection.Add(obj5);
				}
				this._primarySelection = (IComponent)obj5;
			}
			this.OnSelectionChanged();
		}

		// Token: 0x0400020B RID: 523
		private IServiceProvider _serviceProvider;

		// Token: 0x0400020C RID: 524
		private ArrayList _selection;

		// Token: 0x0400020D RID: 525
		private IComponent _primarySelection;
	}
}
