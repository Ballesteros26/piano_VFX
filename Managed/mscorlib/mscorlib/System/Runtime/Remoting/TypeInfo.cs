using System;

namespace System.Runtime.Remoting
{
	// Token: 0x02000766 RID: 1894
	[Serializable]
	internal class TypeInfo : IRemotingTypeInfo
	{
		// Token: 0x06004E4B RID: 20043 RVA: 0x0011AE6C File Offset: 0x0011906C
		public TypeInfo(Type type)
		{
			if (type.IsInterface)
			{
				this.serverType = typeof(MarshalByRefObject).AssemblyQualifiedName;
				this.serverHierarchy = new string[0];
				this.interfacesImplemented = new string[] { type.AssemblyQualifiedName };
				return;
			}
			this.serverType = type.AssemblyQualifiedName;
			int num = 0;
			Type type2 = type.BaseType;
			while (type2 != typeof(MarshalByRefObject) && type2 != null)
			{
				type2 = type2.BaseType;
				num++;
			}
			this.serverHierarchy = new string[num];
			type2 = type.BaseType;
			for (int i = 0; i < num; i++)
			{
				this.serverHierarchy[i] = type2.AssemblyQualifiedName;
				type2 = type2.BaseType;
			}
			Type[] interfaces = type.GetInterfaces();
			this.interfacesImplemented = new string[interfaces.Length];
			for (int j = 0; j < interfaces.Length; j++)
			{
				this.interfacesImplemented[j] = interfaces[j].AssemblyQualifiedName;
			}
		}

		// Token: 0x17000D09 RID: 3337
		// (get) Token: 0x06004E4C RID: 20044 RVA: 0x0011AF68 File Offset: 0x00119168
		// (set) Token: 0x06004E4D RID: 20045 RVA: 0x0011AF70 File Offset: 0x00119170
		public string TypeName
		{
			get
			{
				return this.serverType;
			}
			set
			{
				this.serverType = value;
			}
		}

		// Token: 0x06004E4E RID: 20046 RVA: 0x0011AF7C File Offset: 0x0011917C
		public bool CanCastTo(Type fromType, object o)
		{
			if (fromType == typeof(object))
			{
				return true;
			}
			if (fromType == typeof(MarshalByRefObject))
			{
				return true;
			}
			string text = fromType.AssemblyQualifiedName;
			int num = text.IndexOf(',');
			if (num != -1)
			{
				num = text.IndexOf(',', num + 1);
			}
			if (num != -1)
			{
				text = text.Substring(0, num + 1);
			}
			else
			{
				text += ",";
			}
			if ((this.serverType + ",").StartsWith(text))
			{
				return true;
			}
			if (this.serverHierarchy != null)
			{
				string[] array = this.serverHierarchy;
				for (int i = 0; i < array.Length; i++)
				{
					if ((array[i] + ",").StartsWith(text))
					{
						return true;
					}
				}
			}
			if (this.interfacesImplemented != null)
			{
				string[] array = this.interfacesImplemented;
				for (int i = 0; i < array.Length; i++)
				{
					if ((array[i] + ",").StartsWith(text))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x040029E0 RID: 10720
		private string serverType;

		// Token: 0x040029E1 RID: 10721
		private string[] serverHierarchy;

		// Token: 0x040029E2 RID: 10722
		private string[] interfacesImplemented;
	}
}
