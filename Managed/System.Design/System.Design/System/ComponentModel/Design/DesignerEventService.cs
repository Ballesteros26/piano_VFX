using System;
using System.Collections;

namespace System.ComponentModel.Design
{
	// Token: 0x0200011E RID: 286
	internal sealed class DesignerEventService : IDesignerEventService
	{
		// Token: 0x06000842 RID: 2114 RVA: 0x0000DA5D File Offset: 0x0000BC5D
		public DesignerEventService()
		{
			this._designerList = new ArrayList();
		}

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x06000843 RID: 2115 RVA: 0x0000DA70 File Offset: 0x0000BC70
		// (set) Token: 0x06000844 RID: 2116 RVA: 0x0000DA78 File Offset: 0x0000BC78
		public IDesignerHost ActiveDesigner
		{
			get
			{
				return this._activeDesigner;
			}
			internal set
			{
				IDesignerHost activeDesigner = this._activeDesigner;
				this._activeDesigner = value;
				if (this.ActiveDesignerChanged != null)
				{
					this.ActiveDesignerChanged(this, new ActiveDesignerEventArgs(activeDesigner, value));
				}
			}
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x06000845 RID: 2117 RVA: 0x0000DAAE File Offset: 0x0000BCAE
		public DesignerCollection Designers
		{
			get
			{
				return new DesignerCollection(this._designerList);
			}
		}

		// Token: 0x1400001D RID: 29
		// (add) Token: 0x06000846 RID: 2118 RVA: 0x0000DABC File Offset: 0x0000BCBC
		// (remove) Token: 0x06000847 RID: 2119 RVA: 0x0000DAF4 File Offset: 0x0000BCF4
		public event ActiveDesignerEventHandler ActiveDesignerChanged;

		// Token: 0x1400001E RID: 30
		// (add) Token: 0x06000848 RID: 2120 RVA: 0x0000DB2C File Offset: 0x0000BD2C
		// (remove) Token: 0x06000849 RID: 2121 RVA: 0x0000DB64 File Offset: 0x0000BD64
		public event DesignerEventHandler DesignerCreated;

		// Token: 0x1400001F RID: 31
		// (add) Token: 0x0600084A RID: 2122 RVA: 0x0000DB9C File Offset: 0x0000BD9C
		// (remove) Token: 0x0600084B RID: 2123 RVA: 0x0000DBD4 File Offset: 0x0000BDD4
		public event DesignerEventHandler DesignerDisposed;

		// Token: 0x14000020 RID: 32
		// (add) Token: 0x0600084C RID: 2124 RVA: 0x0000DC0C File Offset: 0x0000BE0C
		// (remove) Token: 0x0600084D RID: 2125 RVA: 0x0000DC44 File Offset: 0x0000BE44
		public event EventHandler SelectionChanged;

		// Token: 0x0600084E RID: 2126 RVA: 0x0000DC79 File Offset: 0x0000BE79
		public void RaiseDesignerCreated(IDesignerHost host)
		{
			if (this.DesignerCreated != null)
			{
				this.DesignerCreated(this, new DesignerEventArgs(host));
			}
		}

		// Token: 0x0600084F RID: 2127 RVA: 0x0000DC95 File Offset: 0x0000BE95
		public void RaiseDesignerDisposed(IDesignerHost host)
		{
			if (this.DesignerDisposed != null)
			{
				this.DesignerDisposed(this, new DesignerEventArgs(host));
			}
		}

		// Token: 0x06000850 RID: 2128 RVA: 0x0000DCB1 File Offset: 0x0000BEB1
		public void RaiseSelectionChanged()
		{
			if (this.SelectionChanged != null)
			{
				this.SelectionChanged(this, EventArgs.Empty);
			}
		}

		// Token: 0x040001CA RID: 458
		private ArrayList _designerList;

		// Token: 0x040001CB RID: 459
		private IDesignerHost _activeDesigner;
	}
}
