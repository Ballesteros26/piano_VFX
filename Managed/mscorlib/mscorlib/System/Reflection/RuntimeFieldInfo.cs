using System;
using System.Runtime.Serialization;

namespace System.Reflection
{
	// Token: 0x02000326 RID: 806
	internal abstract class RuntimeFieldInfo : FieldInfo, ISerializable
	{
		// Token: 0x17000550 RID: 1360
		// (get) Token: 0x06002362 RID: 9058 RVA: 0x00015ED5 File Offset: 0x000140D5
		internal BindingFlags BindingFlags
		{
			get
			{
				return BindingFlags.Default;
			}
		}

		// Token: 0x17000551 RID: 1361
		// (get) Token: 0x06002363 RID: 9059 RVA: 0x000824B4 File Offset: 0x000806B4
		public override Module Module
		{
			get
			{
				return this.GetRuntimeModule();
			}
		}

		// Token: 0x06002364 RID: 9060 RVA: 0x000822BA File Offset: 0x000804BA
		internal RuntimeType GetDeclaringTypeInternal()
		{
			return (RuntimeType)this.DeclaringType;
		}

		// Token: 0x17000552 RID: 1362
		// (get) Token: 0x06002365 RID: 9061 RVA: 0x000822C7 File Offset: 0x000804C7
		private RuntimeType ReflectedTypeInternal
		{
			get
			{
				return (RuntimeType)this.ReflectedType;
			}
		}

		// Token: 0x06002366 RID: 9062 RVA: 0x000824BC File Offset: 0x000806BC
		internal RuntimeModule GetRuntimeModule()
		{
			return this.GetDeclaringTypeInternal().GetRuntimeModule();
		}

		// Token: 0x06002367 RID: 9063 RVA: 0x000824C9 File Offset: 0x000806C9
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			MemberInfoSerializationHolder.GetSerializationInfo(info, this.Name, this.ReflectedTypeInternal, this.ToString(), MemberTypes.Field);
		}
	}
}
