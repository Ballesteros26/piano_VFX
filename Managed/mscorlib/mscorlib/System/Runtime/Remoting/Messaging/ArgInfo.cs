using System;
using System.Reflection;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x020007F8 RID: 2040
	internal class ArgInfo
	{
		// Token: 0x060051CF RID: 20943 RVA: 0x001216B4 File Offset: 0x0011F8B4
		public ArgInfo(MethodBase method, ArgInfoType type)
		{
			this._method = method;
			ParameterInfo[] parameters = this._method.GetParameters();
			this._paramMap = new int[parameters.Length];
			this._inoutArgCount = 0;
			if (type == ArgInfoType.In)
			{
				for (int i = 0; i < parameters.Length; i++)
				{
					if (!parameters[i].ParameterType.IsByRef)
					{
						int[] paramMap = this._paramMap;
						int num = this._inoutArgCount;
						this._inoutArgCount = num + 1;
						paramMap[num] = i;
					}
				}
				return;
			}
			for (int j = 0; j < parameters.Length; j++)
			{
				if (parameters[j].ParameterType.IsByRef || parameters[j].IsOut)
				{
					int[] paramMap2 = this._paramMap;
					int num = this._inoutArgCount;
					this._inoutArgCount = num + 1;
					paramMap2[num] = j;
				}
			}
		}

		// Token: 0x060051D0 RID: 20944 RVA: 0x00121769 File Offset: 0x0011F969
		public int GetInOutArgIndex(int inoutArgNum)
		{
			return this._paramMap[inoutArgNum];
		}

		// Token: 0x060051D1 RID: 20945 RVA: 0x00121773 File Offset: 0x0011F973
		public virtual string GetInOutArgName(int index)
		{
			return this._method.GetParameters()[this._paramMap[index]].Name;
		}

		// Token: 0x060051D2 RID: 20946 RVA: 0x0012178E File Offset: 0x0011F98E
		public int GetInOutArgCount()
		{
			return this._inoutArgCount;
		}

		// Token: 0x060051D3 RID: 20947 RVA: 0x00121798 File Offset: 0x0011F998
		public object[] GetInOutArgs(object[] args)
		{
			object[] array = new object[this._inoutArgCount];
			for (int i = 0; i < this._inoutArgCount; i++)
			{
				array[i] = args[this._paramMap[i]];
			}
			return array;
		}

		// Token: 0x04002AD2 RID: 10962
		private int[] _paramMap;

		// Token: 0x04002AD3 RID: 10963
		private int _inoutArgCount;

		// Token: 0x04002AD4 RID: 10964
		private MethodBase _method;
	}
}
