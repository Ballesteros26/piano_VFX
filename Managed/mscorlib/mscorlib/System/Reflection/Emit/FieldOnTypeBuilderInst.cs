using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x0200035A RID: 858
	[StructLayout(LayoutKind.Sequential)]
	internal class FieldOnTypeBuilderInst : FieldInfo
	{
		// Token: 0x06002684 RID: 9860 RVA: 0x00089100 File Offset: 0x00087300
		public FieldOnTypeBuilderInst(TypeBuilderInstantiation instantiation, FieldInfo fb)
		{
			this.instantiation = instantiation;
			this.fb = fb;
		}

		// Token: 0x17000609 RID: 1545
		// (get) Token: 0x06002685 RID: 9861 RVA: 0x00089116 File Offset: 0x00087316
		public override Type DeclaringType
		{
			get
			{
				return this.instantiation;
			}
		}

		// Token: 0x1700060A RID: 1546
		// (get) Token: 0x06002686 RID: 9862 RVA: 0x0008911E File Offset: 0x0008731E
		public override string Name
		{
			get
			{
				return this.fb.Name;
			}
		}

		// Token: 0x1700060B RID: 1547
		// (get) Token: 0x06002687 RID: 9863 RVA: 0x00089116 File Offset: 0x00087316
		public override Type ReflectedType
		{
			get
			{
				return this.instantiation;
			}
		}

		// Token: 0x06002688 RID: 9864 RVA: 0x00014B5A File Offset: 0x00012D5A
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002689 RID: 9865 RVA: 0x00014B5A File Offset: 0x00012D5A
		public override object[] GetCustomAttributes(bool inherit)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600268A RID: 9866 RVA: 0x00014B5A File Offset: 0x00012D5A
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600268B RID: 9867 RVA: 0x0008912B File Offset: 0x0008732B
		public override string ToString()
		{
			return this.fb.FieldType.ToString() + " " + this.Name;
		}

		// Token: 0x1700060C RID: 1548
		// (get) Token: 0x0600268C RID: 9868 RVA: 0x0008914D File Offset: 0x0008734D
		public override FieldAttributes Attributes
		{
			get
			{
				return this.fb.Attributes;
			}
		}

		// Token: 0x1700060D RID: 1549
		// (get) Token: 0x0600268D RID: 9869 RVA: 0x00014B5A File Offset: 0x00012D5A
		public override RuntimeFieldHandle FieldHandle
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700060E RID: 1550
		// (get) Token: 0x0600268E RID: 9870 RVA: 0x0007EA26 File Offset: 0x0007CC26
		public override int MetadataToken
		{
			get
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x1700060F RID: 1551
		// (get) Token: 0x0600268F RID: 9871 RVA: 0x00014B5A File Offset: 0x00012D5A
		public override Type FieldType
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06002690 RID: 9872 RVA: 0x00014B5A File Offset: 0x00012D5A
		public override object GetValue(object obj)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002691 RID: 9873 RVA: 0x00014B5A File Offset: 0x00012D5A
		public override void SetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, CultureInfo culture)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002692 RID: 9874 RVA: 0x0008915A File Offset: 0x0008735A
		internal FieldInfo RuntimeResolve()
		{
			return this.instantiation.RuntimeResolve().GetField(this.fb);
		}

		// Token: 0x04001414 RID: 5140
		internal TypeBuilderInstantiation instantiation;

		// Token: 0x04001415 RID: 5141
		internal FieldInfo fb;
	}
}
