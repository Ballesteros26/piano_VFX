using System;
using System.Collections.Generic;

namespace System.Web.Hosting
{
	// Token: 0x0200054C RID: 1356
	internal sealed class BareApplicationHost : MarshalByRefObject
	{
		// Token: 0x06003AB2 RID: 15026 RVA: 0x0009E41C File Offset: 0x0009C61C
		public BareApplicationHost()
		{
			this.Init();
		}

		// Token: 0x06003AB3 RID: 15027 RVA: 0x0009E42C File Offset: 0x0009C62C
		private void Init()
		{
			this.hash = new Dictionary<Type, RegisteredItem>();
			HostingEnvironment.Host = this;
			AppDomain currentDomain = AppDomain.CurrentDomain;
			currentDomain.DomainUnload += this.OnDomainUnload;
			this.phys_path = (string)currentDomain.GetData(".appPath");
			this.vpath = (string)currentDomain.GetData(".appVPath");
		}

		// Token: 0x1700120F RID: 4623
		// (get) Token: 0x06003AB4 RID: 15028 RVA: 0x0009E48E File Offset: 0x0009C68E
		public string VirtualPath
		{
			get
			{
				return this.vpath;
			}
		}

		// Token: 0x17001210 RID: 4624
		// (get) Token: 0x06003AB5 RID: 15029 RVA: 0x0009E496 File Offset: 0x0009C696
		public string PhysicalPath
		{
			get
			{
				return this.phys_path;
			}
		}

		// Token: 0x17001211 RID: 4625
		// (get) Token: 0x06003AB6 RID: 15030 RVA: 0x0009E49E File Offset: 0x0009C69E
		public AppDomain Domain
		{
			get
			{
				return AppDomain.CurrentDomain;
			}
		}

		// Token: 0x06003AB7 RID: 15031 RVA: 0x0009E4A5 File Offset: 0x0009C6A5
		public void Shutdown()
		{
			HostingEnvironment.InitiateShutdown();
		}

		// Token: 0x06003AB8 RID: 15032 RVA: 0x0009E4AC File Offset: 0x0009C6AC
		public void StopObject(Type type)
		{
			if (!this.hash.ContainsKey(type))
			{
				return;
			}
			this.hash[type].Item.Stop(false);
		}

		// Token: 0x06003AB9 RID: 15033 RVA: 0x0009E4D4 File Offset: 0x0009C6D4
		public IRegisteredObject CreateInstance(Type type)
		{
			return (IRegisteredObject)Activator.CreateInstance(type, null);
		}

		// Token: 0x06003ABA RID: 15034 RVA: 0x0009E4E2 File Offset: 0x0009C6E2
		public void RegisterObject(IRegisteredObject obj, bool auto_clean)
		{
			this.hash[obj.GetType()] = new RegisteredItem(obj, auto_clean);
		}

		// Token: 0x06003ABB RID: 15035 RVA: 0x0009E4FC File Offset: 0x0009C6FC
		public bool UnregisterObject(IRegisteredObject obj)
		{
			return this.hash.Remove(obj.GetType());
		}

		// Token: 0x06003ABC RID: 15036 RVA: 0x0009E50F File Offset: 0x0009C70F
		public IRegisteredObject GetObject(Type type)
		{
			if (this.hash.ContainsKey(type))
			{
				return this.hash[type].Item;
			}
			return null;
		}

		// Token: 0x06003ABD RID: 15037 RVA: 0x0009E532 File Offset: 0x0009C732
		public string GetCodeGenDir()
		{
			return AppDomain.CurrentDomain.SetupInformation.DynamicBase;
		}

		// Token: 0x06003ABE RID: 15038 RVA: 0x0009E544 File Offset: 0x0009C744
		private void OnDomainUnload(object sender, EventArgs args)
		{
			this.Manager.RemoveHost(this.AppID);
			ICollection<RegisteredItem> values = this.hash.Values;
			RegisteredItem[] array = new RegisteredItem[this.hash.Count];
			values.CopyTo(array, 0);
			foreach (RegisteredItem registeredItem in array)
			{
				try
				{
					registeredItem.Item.Stop(true);
				}
				catch
				{
				}
			}
			this.hash.Clear();
		}

		// Token: 0x04001FDD RID: 8157
		private string vpath;

		// Token: 0x04001FDE RID: 8158
		private string phys_path;

		// Token: 0x04001FDF RID: 8159
		private Dictionary<Type, RegisteredItem> hash;

		// Token: 0x04001FE0 RID: 8160
		internal ApplicationManager Manager;

		// Token: 0x04001FE1 RID: 8161
		internal string AppID;
	}
}
