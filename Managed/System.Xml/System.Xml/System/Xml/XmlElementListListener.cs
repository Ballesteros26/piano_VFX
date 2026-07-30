using System;

namespace System.Xml
{
	// Token: 0x02000225 RID: 549
	internal class XmlElementListListener
	{
		// Token: 0x060014BC RID: 5308 RVA: 0x00075E20 File Offset: 0x00074020
		internal XmlElementListListener(XmlDocument doc, XmlElementList elemList)
		{
			this.doc = doc;
			this.elemList = new WeakReference(elemList);
			this.nodeChangeHandler = new XmlNodeChangedEventHandler(this.OnListChanged);
			doc.NodeInserted += this.nodeChangeHandler;
			doc.NodeRemoved += this.nodeChangeHandler;
		}

		// Token: 0x060014BD RID: 5309 RVA: 0x00075E70 File Offset: 0x00074070
		private void OnListChanged(object sender, XmlNodeChangedEventArgs args)
		{
			lock (this)
			{
				if (this.elemList != null)
				{
					XmlElementList xmlElementList = (XmlElementList)this.elemList.Target;
					if (xmlElementList != null)
					{
						xmlElementList.ConcurrencyCheck(args);
					}
					else
					{
						this.doc.NodeInserted -= this.nodeChangeHandler;
						this.doc.NodeRemoved -= this.nodeChangeHandler;
						this.elemList = null;
					}
				}
			}
		}

		// Token: 0x060014BE RID: 5310 RVA: 0x00075EF4 File Offset: 0x000740F4
		internal void Unregister()
		{
			lock (this)
			{
				if (this.elemList != null)
				{
					this.doc.NodeInserted -= this.nodeChangeHandler;
					this.doc.NodeRemoved -= this.nodeChangeHandler;
					this.elemList = null;
				}
			}
		}

		// Token: 0x04000DD3 RID: 3539
		private WeakReference elemList;

		// Token: 0x04000DD4 RID: 3540
		private XmlDocument doc;

		// Token: 0x04000DD5 RID: 3541
		private XmlNodeChangedEventHandler nodeChangeHandler;
	}
}
