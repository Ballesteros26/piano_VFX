using System;

namespace System.ComponentModel.Design
{
	// Token: 0x02000101 RID: 257
	internal class DesignModeNestedContainer : NestedContainer
	{
		// Token: 0x0600075C RID: 1884 RVA: 0x0000C08E File Offset: 0x0000A28E
		public DesignModeNestedContainer(IComponent owner, string containerName)
			: base(owner)
		{
			this._containerName = containerName;
		}

		// Token: 0x0600075D RID: 1885 RVA: 0x0000C0A0 File Offset: 0x0000A2A0
		public override void Add(IComponent component, string name)
		{
			if (base.Owner.Site != null)
			{
				DesignerHost designerHost = base.Owner.Site.GetService(typeof(IDesignerHost)) as DesignerHost;
				if (designerHost != null)
				{
					designerHost.AddPreProcess(component, name);
					base.Add(component, name);
					designerHost.AddPostProcess(component, name);
				}
			}
		}

		// Token: 0x0600075E RID: 1886 RVA: 0x0000C0F8 File Offset: 0x0000A2F8
		public override void Remove(IComponent component)
		{
			if (base.Owner.Site != null)
			{
				DesignerHost designerHost = base.Owner.Site.GetService(typeof(IDesignerHost)) as DesignerHost;
				if (designerHost != null)
				{
					designerHost.RemovePreProcess(component);
					base.Remove(component);
					designerHost.RemovePostProcess(component);
				}
			}
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x0600075F RID: 1887 RVA: 0x0000C14A File Offset: 0x0000A34A
		protected override string OwnerName
		{
			get
			{
				if (this._containerName != null)
				{
					return base.OwnerName + "." + this._containerName;
				}
				return base.OwnerName;
			}
		}

		// Token: 0x06000760 RID: 1888 RVA: 0x0000C171 File Offset: 0x0000A371
		protected override ISite CreateSite(IComponent component, string name)
		{
			if (component == null)
			{
				throw new ArgumentNullException("component");
			}
			if (base.Owner.Site == null)
			{
				throw new InvalidOperationException("Owner not sited.");
			}
			return new DesignModeNestedContainer.Site(component, name, this, base.Owner.Site);
		}

		// Token: 0x06000761 RID: 1889 RVA: 0x0000C1AC File Offset: 0x0000A3AC
		protected override object GetService(Type service)
		{
			if (service == typeof(INestedContainer))
			{
				return this;
			}
			object obj = null;
			if (base.Owner.Site != null)
			{
				obj = base.Owner.Site.GetService(service);
			}
			if (obj == null)
			{
				return base.GetService(service);
			}
			return null;
		}

		// Token: 0x0400018A RID: 394
		private string _containerName;

		// Token: 0x02000102 RID: 258
		private class Site : DesignModeSite, INestedSite, ISite, IServiceProvider
		{
			// Token: 0x06000762 RID: 1890 RVA: 0x0000C1FA File Offset: 0x0000A3FA
			public Site(IComponent component, string name, IContainer container, IServiceProvider serviceProvider)
				: base(component, name, container, serviceProvider)
			{
			}

			// Token: 0x170001B6 RID: 438
			// (get) Token: 0x06000763 RID: 1891 RVA: 0x0000C208 File Offset: 0x0000A408
			public string FullName
			{
				get
				{
					if (base.Name == null)
					{
						return null;
					}
					string ownerName = ((DesignModeNestedContainer)base.Container).OwnerName;
					if (ownerName == null)
					{
						return base.Name;
					}
					return ownerName + "." + base.Name;
				}
			}
		}
	}
}
