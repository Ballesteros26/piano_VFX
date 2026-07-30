using System;
using System.Runtime.Serialization;
using System.Text;

namespace System.Reflection
{
	// Token: 0x02000336 RID: 822
	internal abstract class RuntimePropertyInfo : PropertyInfo, ISerializable
	{
		// Token: 0x17000586 RID: 1414
		// (get) Token: 0x06002425 RID: 9253 RVA: 0x00015ED5 File Offset: 0x000140D5
		internal BindingFlags BindingFlags
		{
			get
			{
				return BindingFlags.Default;
			}
		}

		// Token: 0x17000587 RID: 1415
		// (get) Token: 0x06002426 RID: 9254 RVA: 0x0008379E File Offset: 0x0008199E
		public override Module Module
		{
			get
			{
				return this.GetRuntimeModule();
			}
		}

		// Token: 0x06002427 RID: 9255 RVA: 0x000822BA File Offset: 0x000804BA
		internal RuntimeType GetDeclaringTypeInternal()
		{
			return (RuntimeType)this.DeclaringType;
		}

		// Token: 0x17000588 RID: 1416
		// (get) Token: 0x06002428 RID: 9256 RVA: 0x000822C7 File Offset: 0x000804C7
		private RuntimeType ReflectedTypeInternal
		{
			get
			{
				return (RuntimeType)this.ReflectedType;
			}
		}

		// Token: 0x06002429 RID: 9257 RVA: 0x000837A6 File Offset: 0x000819A6
		internal RuntimeModule GetRuntimeModule()
		{
			return this.GetDeclaringTypeInternal().GetRuntimeModule();
		}

		// Token: 0x0600242A RID: 9258 RVA: 0x000837B3 File Offset: 0x000819B3
		public override string ToString()
		{
			return this.FormatNameAndSig(false);
		}

		// Token: 0x0600242B RID: 9259 RVA: 0x000837BC File Offset: 0x000819BC
		private string FormatNameAndSig(bool serialization)
		{
			StringBuilder stringBuilder = new StringBuilder(this.PropertyType.FormatTypeName(serialization));
			stringBuilder.Append(" ");
			stringBuilder.Append(this.Name);
			ParameterInfo[] indexParameters = this.GetIndexParameters();
			if (indexParameters.Length != 0)
			{
				stringBuilder.Append(" [");
				ParameterInfo.FormatParameters(stringBuilder, indexParameters, (CallingConventions)0, serialization);
				stringBuilder.Append("]");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600242C RID: 9260 RVA: 0x00083826 File Offset: 0x00081A26
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			MemberInfoSerializationHolder.GetSerializationInfo(info, this.Name, this.ReflectedTypeInternal, this.ToString(), this.SerializationToString(), MemberTypes.Property, null);
		}

		// Token: 0x0600242D RID: 9261 RVA: 0x00083857 File Offset: 0x00081A57
		internal string SerializationToString()
		{
			return this.FormatNameAndSig(true);
		}
	}
}
