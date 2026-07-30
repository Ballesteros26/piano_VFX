using System;
using System.Reflection;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x020007FC RID: 2044
	[Serializable]
	internal class CADMethodRef
	{
		// Token: 0x060051F0 RID: 20976 RVA: 0x00121A50 File Offset: 0x0011FC50
		private Type[] GetTypes(string[] typeArray)
		{
			Type[] array = new Type[typeArray.Length];
			for (int i = 0; i < typeArray.Length; i++)
			{
				array[i] = Type.GetType(typeArray[i], true);
			}
			return array;
		}

		// Token: 0x060051F1 RID: 20977 RVA: 0x00121A84 File Offset: 0x0011FC84
		public MethodBase Resolve()
		{
			Type type = Type.GetType(this.typeName, true);
			Type[] types = this.GetTypes(this.param_names);
			MethodBase methodBase;
			if (this.ctor)
			{
				methodBase = type.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, types, null);
			}
			else
			{
				methodBase = type.GetMethod(this.methodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, types, null);
			}
			if (methodBase != null && this.generic_arg_names != null && !methodBase.IsGenericMethodDefinition)
			{
				methodBase = null;
			}
			if (methodBase != null && this.generic_arg_names != null)
			{
				methodBase = ((MethodInfo)methodBase).MakeGenericMethod(this.GetTypes(this.generic_arg_names));
			}
			if (methodBase == null && this.generic_arg_names != null)
			{
				foreach (MethodInfo methodInfo in type.GetMethods())
				{
					if (!(methodInfo.Name != this.methodName) && methodInfo.IsGenericMethodDefinition && methodInfo.GetGenericArguments().Length == this.generic_arg_names.Length)
					{
						methodBase = methodInfo.MakeGenericMethod(this.GetTypes(this.generic_arg_names));
						ParameterInfo[] parameters = methodBase.GetParameters();
						if (this.param_names.Length == parameters.Length)
						{
							for (int j = 0; j < parameters.Length; j++)
							{
								if (parameters[j].ParameterType.AssemblyQualifiedName != this.param_names[j])
								{
									methodBase = null;
									break;
								}
							}
							if (methodBase != null)
							{
								break;
							}
						}
					}
				}
			}
			if (methodBase == null)
			{
				throw new RemotingException(string.Format("Method '{0}' not found in type '{1}'", this.methodName, this.typeName));
			}
			return methodBase;
		}

		// Token: 0x060051F2 RID: 20978 RVA: 0x00121C18 File Offset: 0x0011FE18
		public CADMethodRef(IMethodMessage msg)
		{
			MethodBase methodBase = msg.MethodBase;
			this.typeName = methodBase.DeclaringType.AssemblyQualifiedName;
			this.ctor = methodBase.IsConstructor;
			this.methodName = methodBase.Name;
			ParameterInfo[] parameters = methodBase.GetParameters();
			this.param_names = new string[parameters.Length];
			for (int i = 0; i < parameters.Length; i++)
			{
				this.param_names[i] = parameters[i].ParameterType.AssemblyQualifiedName;
			}
			if (!this.ctor && methodBase.IsGenericMethod)
			{
				Type[] genericArguments = methodBase.GetGenericArguments();
				this.generic_arg_names = new string[genericArguments.Length];
				for (int j = 0; j < genericArguments.Length; j++)
				{
					this.generic_arg_names[j] = genericArguments[j].AssemblyQualifiedName;
				}
			}
		}

		// Token: 0x04002AEA RID: 10986
		private bool ctor;

		// Token: 0x04002AEB RID: 10987
		private string typeName;

		// Token: 0x04002AEC RID: 10988
		private string methodName;

		// Token: 0x04002AED RID: 10989
		private string[] param_names;

		// Token: 0x04002AEE RID: 10990
		private string[] generic_arg_names;
	}
}
