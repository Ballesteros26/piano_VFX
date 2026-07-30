using System;
using System.Collections.Generic;

namespace System.ComponentModel.Design
{
	// Token: 0x02000136 RID: 310
	internal class ReferenceService : IReferenceService, IDisposable
	{
		// Token: 0x0600091A RID: 2330 RVA: 0x0000F8FC File Offset: 0x0000DAFC
		internal ReferenceService(IServiceProvider provider)
		{
			if (provider == null)
			{
				throw new ArgumentNullException("provider");
			}
			this._references = new List<IComponent>();
			IComponentChangeService componentChangeService = provider.GetService(typeof(IComponentChangeService)) as IComponentChangeService;
			if (componentChangeService != null)
			{
				componentChangeService.ComponentAdded += this.OnComponentAdded;
				componentChangeService.ComponentRemoved += this.OnComponentRemoved;
			}
		}

		// Token: 0x0600091B RID: 2331 RVA: 0x0000F965 File Offset: 0x0000DB65
		private void OnComponentAdded(object sender, ComponentEventArgs args)
		{
			this._references.Add(args.Component);
		}

		// Token: 0x0600091C RID: 2332 RVA: 0x0000F978 File Offset: 0x0000DB78
		private void OnComponentRemoved(object sender, ComponentEventArgs args)
		{
			this._references.Remove(args.Component);
		}

		// Token: 0x0600091D RID: 2333 RVA: 0x0000F98C File Offset: 0x0000DB8C
		public IComponent GetComponent(object reference)
		{
			return reference as IComponent;
		}

		// Token: 0x0600091E RID: 2334 RVA: 0x0000F994 File Offset: 0x0000DB94
		public string GetName(object reference)
		{
			IComponent component = reference as IComponent;
			if (component != null && component.Site != null)
			{
				return component.Site.Name;
			}
			return null;
		}

		// Token: 0x0600091F RID: 2335 RVA: 0x0000F9C0 File Offset: 0x0000DBC0
		public object GetReference(string name)
		{
			foreach (IComponent component in this._references)
			{
				if (component.Site != null && component.Site.Name == name)
				{
					return component;
				}
			}
			return null;
		}

		// Token: 0x06000920 RID: 2336 RVA: 0x0000FA30 File Offset: 0x0000DC30
		public object[] GetReferences()
		{
			IComponent[] array = new IComponent[this._references.Count];
			this._references.CopyTo(array);
			return array;
		}

		// Token: 0x06000921 RID: 2337 RVA: 0x0000FA5C File Offset: 0x0000DC5C
		public object[] GetReferences(Type baseType)
		{
			List<IComponent> list = new List<IComponent>();
			foreach (IComponent component in this._references)
			{
				if (baseType.IsAssignableFrom(component.GetType()))
				{
					list.Add(component);
				}
			}
			IComponent[] array = new IComponent[list.Count];
			list.CopyTo(array);
			return array;
		}

		// Token: 0x06000922 RID: 2338 RVA: 0x0000FAD8 File Offset: 0x0000DCD8
		public void Dispose()
		{
			this._references.Clear();
		}

		// Token: 0x0400020A RID: 522
		private List<IComponent> _references;
	}
}
