using System;
using System.Runtime.Serialization;

namespace System.Reflection
{
	// Token: 0x0200032C RID: 812
	internal abstract class RuntimeConstructorInfo : ConstructorInfo, ISerializable
	{
		// Token: 0x1700056E RID: 1390
		// (get) Token: 0x060023C9 RID: 9161 RVA: 0x00082D7B File Offset: 0x00080F7B
		public override Module Module
		{
			get
			{
				return this.GetRuntimeModule();
			}
		}

		// Token: 0x060023CA RID: 9162 RVA: 0x00082D83 File Offset: 0x00080F83
		internal RuntimeModule GetRuntimeModule()
		{
			return RuntimeTypeHandle.GetModule((RuntimeType)this.DeclaringType);
		}

		// Token: 0x1700056F RID: 1391
		// (get) Token: 0x060023CB RID: 9163 RVA: 0x00015ED5 File Offset: 0x000140D5
		internal BindingFlags BindingFlags
		{
			get
			{
				return BindingFlags.Default;
			}
		}

		// Token: 0x17000570 RID: 1392
		// (get) Token: 0x060023CC RID: 9164 RVA: 0x000822C7 File Offset: 0x000804C7
		private RuntimeType ReflectedTypeInternal
		{
			get
			{
				return (RuntimeType)this.ReflectedType;
			}
		}

		// Token: 0x060023CD RID: 9165 RVA: 0x00082D95 File Offset: 0x00080F95
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			MemberInfoSerializationHolder.GetSerializationInfo(info, this.Name, this.ReflectedTypeInternal, this.ToString(), this.SerializationToString(), MemberTypes.Constructor, null);
		}

		// Token: 0x060023CE RID: 9166 RVA: 0x00082DC5 File Offset: 0x00080FC5
		internal string SerializationToString()
		{
			return this.FormatNameAndSig(true);
		}

		// Token: 0x060023CF RID: 9167 RVA: 0x00082DCE File Offset: 0x00080FCE
		internal void SerializationInvoke(object target, SerializationInfo info, StreamingContext context)
		{
			base.Invoke(target, new object[] { info, context });
		}
	}
}
