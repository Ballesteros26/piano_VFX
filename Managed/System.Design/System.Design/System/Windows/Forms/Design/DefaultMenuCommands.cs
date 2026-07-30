using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;

namespace System.Windows.Forms.Design
{
	// Token: 0x02000017 RID: 23
	internal sealed class DefaultMenuCommands
	{
		// Token: 0x060000EA RID: 234 RVA: 0x0000396E File Offset: 0x00001B6E
		public DefaultMenuCommands(IServiceProvider serviceProvider)
		{
			if (serviceProvider == null)
			{
				throw new ArgumentNullException("serviceProvider");
			}
			this._serviceProvider = serviceProvider;
		}

		// Token: 0x060000EB RID: 235 RVA: 0x0000398C File Offset: 0x00001B8C
		public void AddTo(IMenuCommandService commands)
		{
			commands.AddCommand(new MenuCommand(new EventHandler(this.Copy), StandardCommands.Copy));
			commands.AddCommand(new MenuCommand(new EventHandler(this.Cut), StandardCommands.Cut));
			commands.AddCommand(new MenuCommand(new EventHandler(this.Paste), StandardCommands.Paste));
			commands.AddCommand(new MenuCommand(new EventHandler(this.Delete), StandardCommands.Delete));
			commands.AddCommand(new MenuCommand(new EventHandler(this.SelectAll), StandardCommands.SelectAll));
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00003A28 File Offset: 0x00001C28
		private void Copy(object sender, EventArgs args)
		{
			IDesignerSerializationService designerSerializationService = this.GetService(typeof(IDesignerSerializationService)) as IDesignerSerializationService;
			IDesignerHost designerHost = this.GetService(typeof(IDesignerHost)) as IDesignerHost;
			ISelectionService selectionService = this.GetService(typeof(ISelectionService)) as ISelectionService;
			if (designerHost == null || designerSerializationService == null || selectionService == null)
			{
				return;
			}
			IEnumerable selectedComponents = selectionService.GetSelectedComponents();
			ArrayList arrayList = new ArrayList();
			foreach (object obj in selectedComponents)
			{
				if (obj != designerHost.RootComponent)
				{
					arrayList.Add(obj);
					ComponentDesigner componentDesigner = designerHost.GetDesigner((IComponent)obj) as ComponentDesigner;
					if (componentDesigner != null && componentDesigner.AssociatedComponents != null)
					{
						arrayList.AddRange(componentDesigner.AssociatedComponents);
					}
				}
			}
			object obj2 = designerSerializationService.Serialize(arrayList);
			this._clipboard = obj2;
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00003B24 File Offset: 0x00001D24
		private void Paste(object sender, EventArgs args)
		{
			IDesignerSerializationService designerSerializationService = this.GetService(typeof(IDesignerSerializationService)) as IDesignerSerializationService;
			ISelectionService selectionService = this.GetService(typeof(ISelectionService)) as ISelectionService;
			IDesignerHost designerHost = this.GetService(typeof(IDesignerHost)) as IDesignerHost;
			IComponentChangeService componentChangeService = this.GetService(typeof(IComponentChangeService)) as IComponentChangeService;
			if (designerHost == null || designerSerializationService == null)
			{
				return;
			}
			if (this._clipboard == null)
			{
				return;
			}
			DesignerTransaction designerTransaction = designerHost.CreateTransaction("Paste");
			foreach (object obj in designerSerializationService.Deserialize(this._clipboard))
			{
				Control control = obj as Control;
				if (control != null)
				{
					PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(control)["Parent"];
					if (control.Parent != null)
					{
						if (componentChangeService != null)
						{
							componentChangeService.OnComponentChanging(control, propertyDescriptor);
							componentChangeService.OnComponentChanged(control, propertyDescriptor, null, control.Parent);
						}
					}
					else
					{
						ParentControlDesigner parentControlDesigner = null;
						if (selectionService != null && selectionService.PrimarySelection != null)
						{
							parentControlDesigner = designerHost.GetDesigner((IComponent)selectionService.PrimarySelection) as ParentControlDesigner;
						}
						if (parentControlDesigner == null)
						{
							parentControlDesigner = designerHost.GetDesigner(designerHost.RootComponent) as DocumentDesigner;
						}
						if (parentControlDesigner != null && parentControlDesigner.CanParent(control))
						{
							propertyDescriptor.SetValue(control, parentControlDesigner.Control);
						}
					}
				}
			}
			this._clipboard = null;
			designerTransaction.Commit();
			((IDisposable)designerTransaction).Dispose();
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00003CB4 File Offset: 0x00001EB4
		private void Cut(object sender, EventArgs args)
		{
			IDesignerHost designerHost = this.GetService(typeof(IDesignerHost)) as IDesignerHost;
			if (designerHost == null)
			{
				return;
			}
			using (DesignerTransaction designerTransaction = designerHost.CreateTransaction("Cut"))
			{
				this.Copy(this, EventArgs.Empty);
				this.Delete(this, EventArgs.Empty);
				designerTransaction.Commit();
			}
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00003D24 File Offset: 0x00001F24
		private void Delete(object sender, EventArgs args)
		{
			IDesignerHost designerHost = this.GetService(typeof(IDesignerHost)) as IDesignerHost;
			ISelectionService selectionService = this.GetService(typeof(ISelectionService)) as ISelectionService;
			if (designerHost == null || selectionService == null)
			{
				return;
			}
			ICollection selectedComponents = selectionService.GetSelectedComponents();
			string text = "Delete " + ((selectedComponents.Count > 1) ? (selectedComponents.Count.ToString() + " controls") : ((IComponent)selectionService.PrimarySelection).Site.Name);
			DesignerTransaction designerTransaction = designerHost.CreateTransaction(text);
			foreach (object obj in selectedComponents)
			{
				if (obj != designerHost.RootComponent)
				{
					ComponentDesigner componentDesigner = designerHost.GetDesigner((IComponent)obj) as ComponentDesigner;
					if (componentDesigner != null && componentDesigner.AssociatedComponents != null)
					{
						foreach (object obj2 in componentDesigner.AssociatedComponents)
						{
							designerHost.DestroyComponent((IComponent)obj2);
						}
					}
					designerHost.DestroyComponent((IComponent)obj);
				}
			}
			selectionService.SetSelectedComponents(selectedComponents, SelectionTypes.Remove);
			designerTransaction.Commit();
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00003EA0 File Offset: 0x000020A0
		private void SelectAll(object sender, EventArgs args)
		{
			IDesignerHost designerHost = this.GetService(typeof(IDesignerHost)) as IDesignerHost;
			ISelectionService selectionService = this.GetService(typeof(ISelectionService)) as ISelectionService;
			if (designerHost != null && selectionService != null)
			{
				selectionService.SetSelectedComponents(designerHost.Container.Components, SelectionTypes.Replace);
			}
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00003EF1 File Offset: 0x000020F1
		private object GetService(Type serviceType)
		{
			if (this._serviceProvider != null)
			{
				return this._serviceProvider.GetService(serviceType);
			}
			return null;
		}

		// Token: 0x0400002A RID: 42
		private IServiceProvider _serviceProvider;

		// Token: 0x0400002B RID: 43
		private const string DT_DATA_FORMAT = "DT_DATA_FORMAT";

		// Token: 0x0400002C RID: 44
		private object _clipboard;
	}
}
