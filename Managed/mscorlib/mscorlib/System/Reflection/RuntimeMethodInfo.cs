using System;
using System.Runtime.Serialization;
using System.Text;

namespace System.Reflection
{
	// Token: 0x0200032A RID: 810
	internal abstract class RuntimeMethodInfo : MethodInfo, ISerializable
	{
		// Token: 0x1700055C RID: 1372
		// (get) Token: 0x06002395 RID: 9109 RVA: 0x00015ED5 File Offset: 0x000140D5
		internal BindingFlags BindingFlags
		{
			get
			{
				return BindingFlags.Default;
			}
		}

		// Token: 0x1700055D RID: 1373
		// (get) Token: 0x06002396 RID: 9110 RVA: 0x0008287D File Offset: 0x00080A7D
		public override Module Module
		{
			get
			{
				return this.GetRuntimeModule();
			}
		}

		// Token: 0x1700055E RID: 1374
		// (get) Token: 0x06002397 RID: 9111 RVA: 0x000822C7 File Offset: 0x000804C7
		private RuntimeType ReflectedTypeInternal
		{
			get
			{
				return (RuntimeType)this.ReflectedType;
			}
		}

		// Token: 0x06002398 RID: 9112 RVA: 0x00082888 File Offset: 0x00080A88
		internal override string FormatNameAndSig(bool serialization)
		{
			StringBuilder stringBuilder = new StringBuilder(this.Name);
			TypeNameFormatFlags typeNameFormatFlags = (serialization ? TypeNameFormatFlags.FormatSerialization : TypeNameFormatFlags.FormatBasic);
			if (this.IsGenericMethod)
			{
				stringBuilder.Append(RuntimeMethodHandle.ConstructInstantiation(this, typeNameFormatFlags));
			}
			stringBuilder.Append("(");
			ParameterInfo.FormatParameters(stringBuilder, this.GetParametersNoCopy(), this.CallingConvention, serialization);
			stringBuilder.Append(")");
			return stringBuilder.ToString();
		}

		// Token: 0x06002399 RID: 9113 RVA: 0x000828F4 File Offset: 0x00080AF4
		public override Delegate CreateDelegate(Type delegateType)
		{
			return Delegate.CreateDelegate(delegateType, this);
		}

		// Token: 0x0600239A RID: 9114 RVA: 0x000828FD File Offset: 0x00080AFD
		public override Delegate CreateDelegate(Type delegateType, object target)
		{
			return Delegate.CreateDelegate(delegateType, target, this);
		}

		// Token: 0x0600239B RID: 9115 RVA: 0x00082907 File Offset: 0x00080B07
		public override string ToString()
		{
			return this.ReturnType.FormatTypeName() + " " + this.FormatNameAndSig(false);
		}

		// Token: 0x0600239C RID: 9116 RVA: 0x00082925 File Offset: 0x00080B25
		internal RuntimeModule GetRuntimeModule()
		{
			return ((RuntimeType)this.DeclaringType).GetRuntimeModule();
		}

		// Token: 0x0600239D RID: 9117 RVA: 0x00082938 File Offset: 0x00080B38
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			MemberInfoSerializationHolder.GetSerializationInfo(info, this.Name, this.ReflectedTypeInternal, this.ToString(), this.SerializationToString(), MemberTypes.Method, (this.IsGenericMethod & !this.IsGenericMethodDefinition) ? this.GetGenericArguments() : null);
		}

		// Token: 0x0600239E RID: 9118 RVA: 0x0008298D File Offset: 0x00080B8D
		internal string SerializationToString()
		{
			return this.ReturnType.FormatTypeName(true) + " " + this.FormatNameAndSig(true);
		}
	}
}
