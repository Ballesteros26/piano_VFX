using System;
using System.Configuration;
using System.Security.Permissions;

namespace System.Diagnostics
{
	// Token: 0x020001B1 RID: 433
	[ConfigurationCollection(typeof(ListenerElement))]
	internal class ListenerElementsCollection : ConfigurationElementCollection
	{
		// Token: 0x17000236 RID: 566
		public ListenerElement this[string name]
		{
			get
			{
				return (ListenerElement)base.BaseGet(name);
			}
		}

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x06000CC6 RID: 3270 RVA: 0x000027E2 File Offset: 0x000009E2
		public override ConfigurationElementCollectionType CollectionType
		{
			get
			{
				return ConfigurationElementCollectionType.AddRemoveClearMap;
			}
		}

		// Token: 0x06000CC7 RID: 3271 RVA: 0x0003E089 File Offset: 0x0003C289
		protected override ConfigurationElement CreateNewElement()
		{
			return new ListenerElement(true);
		}

		// Token: 0x06000CC8 RID: 3272 RVA: 0x0003E091 File Offset: 0x0003C291
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((ListenerElement)element).Name;
		}

		// Token: 0x06000CC9 RID: 3273 RVA: 0x0003E0A0 File Offset: 0x0003C2A0
		public TraceListenerCollection GetRuntimeObject()
		{
			TraceListenerCollection traceListenerCollection = new TraceListenerCollection();
			bool flag = false;
			foreach (object obj in this)
			{
				ListenerElement listenerElement = (ListenerElement)obj;
				if (!flag && !listenerElement._isAddedByDefault)
				{
					new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Demand();
					flag = true;
				}
				traceListenerCollection.Add(listenerElement.GetRuntimeObject());
			}
			return traceListenerCollection;
		}

		// Token: 0x06000CCA RID: 3274 RVA: 0x0003E120 File Offset: 0x0003C320
		protected override void InitializeDefault()
		{
			this.InitializeDefaultInternal();
		}

		// Token: 0x06000CCB RID: 3275 RVA: 0x0003E128 File Offset: 0x0003C328
		internal void InitializeDefaultInternal()
		{
			this.BaseAdd(new ListenerElement(false)
			{
				Name = "Default",
				TypeName = typeof(DefaultTraceListener).FullName,
				_isAddedByDefault = true
			});
		}

		// Token: 0x06000CCC RID: 3276 RVA: 0x0003E16C File Offset: 0x0003C36C
		protected override void BaseAdd(ConfigurationElement element)
		{
			ListenerElement listenerElement = element as ListenerElement;
			if (listenerElement.Name.Equals("Default") && listenerElement.TypeName.Equals(typeof(DefaultTraceListener).FullName))
			{
				base.BaseAdd(listenerElement, false);
				return;
			}
			base.BaseAdd(listenerElement, this.ThrowOnDuplicate);
		}
	}
}
