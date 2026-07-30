using System;
using System.Globalization;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Provides the base implementation for the <see cref="T:System.ComponentModel.INestedContainer" /> interface, which enables containers to have an owning component.</summary>
	// Token: 0x020002B7 RID: 695
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class NestedContainer : Container, INestedContainer, IContainer, IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.NestedContainer" /> class.</summary>
		/// <param name="owner">The <see cref="T:System.ComponentModel.IComponent" /> that owns this nested container.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="owner" /> is null.</exception>
		// Token: 0x060015D6 RID: 5590 RVA: 0x00056547 File Offset: 0x00054747
		public NestedContainer(IComponent owner)
		{
			if (owner == null)
			{
				throw new ArgumentNullException("owner");
			}
			this._owner = owner;
			this._owner.Disposed += this.OnOwnerDisposed;
		}

		/// <summary>Gets the owning component for this nested container.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.IComponent" /> that owns this nested container.</returns>
		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x060015D7 RID: 5591 RVA: 0x0005657B File Offset: 0x0005477B
		public IComponent Owner
		{
			get
			{
				return this._owner;
			}
		}

		/// <summary>Gets the name of the owning component.</summary>
		/// <returns>The name of the owning component.</returns>
		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x060015D8 RID: 5592 RVA: 0x00056584 File Offset: 0x00054784
		protected virtual string OwnerName
		{
			get
			{
				string text = null;
				if (this._owner != null && this._owner.Site != null)
				{
					INestedSite nestedSite = this._owner.Site as INestedSite;
					if (nestedSite != null)
					{
						text = nestedSite.FullName;
					}
					else
					{
						text = this._owner.Site.Name;
					}
				}
				return text;
			}
		}

		/// <summary>Creates a site for the component within the container.</summary>
		/// <returns>The newly created <see cref="T:System.ComponentModel.ISite" />.</returns>
		/// <param name="component">The <see cref="T:System.ComponentModel.IComponent" /> to create a site for.</param>
		/// <param name="name">The name to assign to <paramref name="component" />, or null to skip the name assignment.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="component" /> is null.</exception>
		// Token: 0x060015D9 RID: 5593 RVA: 0x000565D7 File Offset: 0x000547D7
		protected override ISite CreateSite(IComponent component, string name)
		{
			if (component == null)
			{
				throw new ArgumentNullException("component");
			}
			return new NestedContainer.Site(component, this, name);
		}

		/// <summary>Releases the resources used by the nested container.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
		// Token: 0x060015DA RID: 5594 RVA: 0x000565EF File Offset: 0x000547EF
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this._owner.Disposed -= this.OnOwnerDisposed;
			}
			base.Dispose(disposing);
		}

		/// <summary>Gets the service object of the specified type, if it is available.</summary>
		/// <returns>An <see cref="T:System.Object" /> that implements the requested service, or null if the service cannot be resolved.</returns>
		/// <param name="service">The <see cref="T:System.Type" /> of the service to retrieve.</param>
		// Token: 0x060015DB RID: 5595 RVA: 0x00056612 File Offset: 0x00054812
		protected override object GetService(Type service)
		{
			if (service == typeof(INestedContainer))
			{
				return this;
			}
			return base.GetService(service);
		}

		// Token: 0x060015DC RID: 5596 RVA: 0x0005662F File Offset: 0x0005482F
		private void OnOwnerDisposed(object sender, EventArgs e)
		{
			base.Dispose();
		}

		// Token: 0x04001379 RID: 4985
		private IComponent _owner;

		// Token: 0x020002B8 RID: 696
		private class Site : INestedSite, ISite, IServiceProvider
		{
			// Token: 0x060015DD RID: 5597 RVA: 0x00056637 File Offset: 0x00054837
			internal Site(IComponent component, NestedContainer container, string name)
			{
				this.component = component;
				this.container = container;
				this.name = name;
			}

			// Token: 0x17000496 RID: 1174
			// (get) Token: 0x060015DE RID: 5598 RVA: 0x00056654 File Offset: 0x00054854
			public IComponent Component
			{
				get
				{
					return this.component;
				}
			}

			// Token: 0x17000497 RID: 1175
			// (get) Token: 0x060015DF RID: 5599 RVA: 0x0005665C File Offset: 0x0005485C
			public IContainer Container
			{
				get
				{
					return this.container;
				}
			}

			// Token: 0x060015E0 RID: 5600 RVA: 0x00056664 File Offset: 0x00054864
			public object GetService(Type service)
			{
				if (!(service == typeof(ISite)))
				{
					return this.container.GetService(service);
				}
				return this;
			}

			// Token: 0x17000498 RID: 1176
			// (get) Token: 0x060015E1 RID: 5601 RVA: 0x00056688 File Offset: 0x00054888
			public bool DesignMode
			{
				get
				{
					IComponent owner = this.container.Owner;
					return owner != null && owner.Site != null && owner.Site.DesignMode;
				}
			}

			// Token: 0x17000499 RID: 1177
			// (get) Token: 0x060015E2 RID: 5602 RVA: 0x000566BC File Offset: 0x000548BC
			public string FullName
			{
				get
				{
					if (this.name != null)
					{
						string ownerName = this.container.OwnerName;
						string text = this.name;
						if (ownerName != null)
						{
							text = string.Format(CultureInfo.InvariantCulture, "{0}.{1}", ownerName, text);
						}
						return text;
					}
					return this.name;
				}
			}

			// Token: 0x1700049A RID: 1178
			// (get) Token: 0x060015E3 RID: 5603 RVA: 0x00056701 File Offset: 0x00054901
			// (set) Token: 0x060015E4 RID: 5604 RVA: 0x00056709 File Offset: 0x00054909
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

			// Token: 0x0400137A RID: 4986
			private IComponent component;

			// Token: 0x0400137B RID: 4987
			private NestedContainer container;

			// Token: 0x0400137C RID: 4988
			private string name;
		}
	}
}
