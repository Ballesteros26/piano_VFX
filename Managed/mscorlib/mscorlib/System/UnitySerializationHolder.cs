using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;
using System.Security;

namespace System
{
	// Token: 0x020001E9 RID: 489
	[Serializable]
	internal class UnitySerializationHolder : ISerializable, IObjectReference
	{
		// Token: 0x06001677 RID: 5751 RVA: 0x00059133 File Offset: 0x00057333
		internal static void GetUnitySerializationInfo(SerializationInfo info, Missing missing)
		{
			info.SetType(typeof(UnitySerializationHolder));
			info.AddValue("UnityType", 3);
		}

		// Token: 0x06001678 RID: 5752 RVA: 0x00059154 File Offset: 0x00057354
		internal static RuntimeType AddElementTypes(SerializationInfo info, RuntimeType type)
		{
			List<int> list = new List<int>();
			while (type.HasElementType)
			{
				if (type.IsSzArray)
				{
					list.Add(3);
				}
				else if (type.IsArray)
				{
					list.Add(type.GetArrayRank());
					list.Add(2);
				}
				else if (type.IsPointer)
				{
					list.Add(1);
				}
				else if (type.IsByRef)
				{
					list.Add(4);
				}
				type = (RuntimeType)type.GetElementType();
			}
			info.AddValue("ElementTypes", list.ToArray(), typeof(int[]));
			return type;
		}

		// Token: 0x06001679 RID: 5753 RVA: 0x000591E8 File Offset: 0x000573E8
		internal Type MakeElementTypes(Type type)
		{
			for (int i = this.m_elementTypes.Length - 1; i >= 0; i--)
			{
				if (this.m_elementTypes[i] == 3)
				{
					type = type.MakeArrayType();
				}
				else if (this.m_elementTypes[i] == 2)
				{
					type = type.MakeArrayType(this.m_elementTypes[--i]);
				}
				else if (this.m_elementTypes[i] == 1)
				{
					type = type.MakePointerType();
				}
				else if (this.m_elementTypes[i] == 4)
				{
					type = type.MakeByRefType();
				}
			}
			return type;
		}

		// Token: 0x0600167A RID: 5754 RVA: 0x0005926C File Offset: 0x0005746C
		internal static void GetUnitySerializationInfo(SerializationInfo info, RuntimeType type)
		{
			if (type.GetRootElementType().IsGenericParameter)
			{
				type = UnitySerializationHolder.AddElementTypes(info, type);
				info.SetType(typeof(UnitySerializationHolder));
				info.AddValue("UnityType", 7);
				info.AddValue("GenericParameterPosition", type.GenericParameterPosition);
				info.AddValue("DeclaringMethod", type.DeclaringMethod, typeof(MethodBase));
				info.AddValue("DeclaringType", type.DeclaringType, typeof(Type));
				return;
			}
			int num = 4;
			if (!type.IsGenericTypeDefinition && type.ContainsGenericParameters)
			{
				num = 8;
				type = UnitySerializationHolder.AddElementTypes(info, type);
				info.AddValue("GenericArguments", type.GetGenericArguments(), typeof(Type[]));
				type = (RuntimeType)type.GetGenericTypeDefinition();
			}
			UnitySerializationHolder.GetUnitySerializationInfo(info, num, type.FullName, type.GetRuntimeAssembly());
		}

		// Token: 0x0600167B RID: 5755 RVA: 0x0005934C File Offset: 0x0005754C
		internal static void GetUnitySerializationInfo(SerializationInfo info, int unityType, string data, RuntimeAssembly assembly)
		{
			info.SetType(typeof(UnitySerializationHolder));
			info.AddValue("Data", data, typeof(string));
			info.AddValue("UnityType", unityType);
			string text;
			if (assembly == null)
			{
				text = string.Empty;
			}
			else
			{
				text = assembly.FullName;
			}
			info.AddValue("AssemblyName", text);
		}

		// Token: 0x0600167C RID: 5756 RVA: 0x000593B0 File Offset: 0x000575B0
		internal UnitySerializationHolder(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			this.m_unityType = info.GetInt32("UnityType");
			if (this.m_unityType == 3)
			{
				return;
			}
			if (this.m_unityType == 7)
			{
				this.m_declaringMethod = info.GetValue("DeclaringMethod", typeof(MethodBase)) as MethodBase;
				this.m_declaringType = info.GetValue("DeclaringType", typeof(Type)) as Type;
				this.m_genericParameterPosition = info.GetInt32("GenericParameterPosition");
				this.m_elementTypes = info.GetValue("ElementTypes", typeof(int[])) as int[];
				return;
			}
			if (this.m_unityType == 8)
			{
				this.m_instantiation = info.GetValue("GenericArguments", typeof(Type[])) as Type[];
				this.m_elementTypes = info.GetValue("ElementTypes", typeof(int[])) as int[];
			}
			this.m_data = info.GetString("Data");
			this.m_assemblyName = info.GetString("AssemblyName");
		}

		// Token: 0x0600167D RID: 5757 RVA: 0x000594D2 File Offset: 0x000576D2
		private void ThrowInsufficientInformation(string field)
		{
			throw new SerializationException(Environment.GetResourceString("Insufficient state to deserialize the object. Missing field '{0}'. More information is needed.", new object[] { field }));
		}

		// Token: 0x0600167E RID: 5758 RVA: 0x000594ED File Offset: 0x000576ED
		[SecurityCritical]
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new NotSupportedException(Environment.GetResourceString("The UnitySerializationHolder object is designed to transmit information about other types and is not serializable itself."));
		}

		// Token: 0x0600167F RID: 5759 RVA: 0x00059500 File Offset: 0x00057700
		[SecurityCritical]
		public virtual object GetRealObject(StreamingContext context)
		{
			switch (this.m_unityType)
			{
			case 1:
				return Empty.Value;
			case 2:
				return DBNull.Value;
			case 3:
				return Missing.Value;
			case 4:
				if (this.m_data == null || this.m_data.Length == 0)
				{
					this.ThrowInsufficientInformation("Data");
				}
				if (this.m_assemblyName == null)
				{
					this.ThrowInsufficientInformation("AssemblyName");
				}
				if (this.m_assemblyName.Length == 0)
				{
					return Type.GetType(this.m_data, true, false);
				}
				return Assembly.Load(this.m_assemblyName).GetType(this.m_data, true, false);
			case 5:
			{
				if (this.m_data == null || this.m_data.Length == 0)
				{
					this.ThrowInsufficientInformation("Data");
				}
				if (this.m_assemblyName == null)
				{
					this.ThrowInsufficientInformation("AssemblyName");
				}
				Module module = Assembly.Load(this.m_assemblyName).GetModule(this.m_data);
				if (module == null)
				{
					throw new SerializationException(Environment.GetResourceString("The given module {0} cannot be found within the assembly {1}.", new object[] { this.m_data, this.m_assemblyName }));
				}
				return module;
			}
			case 6:
				if (this.m_data == null || this.m_data.Length == 0)
				{
					this.ThrowInsufficientInformation("Data");
				}
				if (this.m_assemblyName == null)
				{
					this.ThrowInsufficientInformation("AssemblyName");
				}
				return Assembly.Load(this.m_assemblyName);
			case 7:
				if (this.m_declaringMethod == null && this.m_declaringType == null)
				{
					this.ThrowInsufficientInformation("DeclaringMember");
				}
				if (this.m_declaringMethod != null)
				{
					return this.m_declaringMethod.GetGenericArguments()[this.m_genericParameterPosition];
				}
				return this.MakeElementTypes(this.m_declaringType.GetGenericArguments()[this.m_genericParameterPosition]);
			case 8:
			{
				this.m_unityType = 4;
				Type type = this.GetRealObject(context) as Type;
				this.m_unityType = 8;
				if (this.m_instantiation[0] == null)
				{
					return null;
				}
				return this.MakeElementTypes(type.MakeGenericType(this.m_instantiation));
			}
			default:
				throw new ArgumentException(Environment.GetResourceString("Invalid Unity type."));
			}
		}

		// Token: 0x04000BC4 RID: 3012
		internal const int EmptyUnity = 1;

		// Token: 0x04000BC5 RID: 3013
		internal const int NullUnity = 2;

		// Token: 0x04000BC6 RID: 3014
		internal const int MissingUnity = 3;

		// Token: 0x04000BC7 RID: 3015
		internal const int RuntimeTypeUnity = 4;

		// Token: 0x04000BC8 RID: 3016
		internal const int ModuleUnity = 5;

		// Token: 0x04000BC9 RID: 3017
		internal const int AssemblyUnity = 6;

		// Token: 0x04000BCA RID: 3018
		internal const int GenericParameterTypeUnity = 7;

		// Token: 0x04000BCB RID: 3019
		internal const int PartialInstantiationTypeUnity = 8;

		// Token: 0x04000BCC RID: 3020
		internal const int Pointer = 1;

		// Token: 0x04000BCD RID: 3021
		internal const int Array = 2;

		// Token: 0x04000BCE RID: 3022
		internal const int SzArray = 3;

		// Token: 0x04000BCF RID: 3023
		internal const int ByRef = 4;

		// Token: 0x04000BD0 RID: 3024
		private Type[] m_instantiation;

		// Token: 0x04000BD1 RID: 3025
		private int[] m_elementTypes;

		// Token: 0x04000BD2 RID: 3026
		private int m_genericParameterPosition;

		// Token: 0x04000BD3 RID: 3027
		private Type m_declaringType;

		// Token: 0x04000BD4 RID: 3028
		private MethodBase m_declaringMethod;

		// Token: 0x04000BD5 RID: 3029
		private string m_data;

		// Token: 0x04000BD6 RID: 3030
		private string m_assemblyName;

		// Token: 0x04000BD7 RID: 3031
		private int m_unityType;
	}
}
