using System;
using System.Runtime.Serialization;

namespace System.Reflection
{
	// Token: 0x02000324 RID: 804
	internal abstract class RuntimeEventInfo : EventInfo, ISerializable
	{
		// Token: 0x17000549 RID: 1353
		// (get) Token: 0x0600234D RID: 9037 RVA: 0x00015ED5 File Offset: 0x000140D5
		internal BindingFlags BindingFlags
		{
			get
			{
				return BindingFlags.Default;
			}
		}

		// Token: 0x1700054A RID: 1354
		// (get) Token: 0x0600234E RID: 9038 RVA: 0x000822B2 File Offset: 0x000804B2
		public override Module Module
		{
			get
			{
				return this.GetRuntimeModule();
			}
		}

		// Token: 0x0600234F RID: 9039 RVA: 0x000822BA File Offset: 0x000804BA
		internal RuntimeType GetDeclaringTypeInternal()
		{
			return (RuntimeType)this.DeclaringType;
		}

		// Token: 0x1700054B RID: 1355
		// (get) Token: 0x06002350 RID: 9040 RVA: 0x000822C7 File Offset: 0x000804C7
		private RuntimeType ReflectedTypeInternal
		{
			get
			{
				return (RuntimeType)this.ReflectedType;
			}
		}

		// Token: 0x06002351 RID: 9041 RVA: 0x000822D4 File Offset: 0x000804D4
		internal RuntimeModule GetRuntimeModule()
		{
			return this.GetDeclaringTypeInternal().GetRuntimeModule();
		}

		// Token: 0x06002352 RID: 9042 RVA: 0x000822E1 File Offset: 0x000804E1
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			MemberInfoSerializationHolder.GetSerializationInfo(info, this.Name, this.ReflectedTypeInternal, null, MemberTypes.Event);
		}
	}
}
