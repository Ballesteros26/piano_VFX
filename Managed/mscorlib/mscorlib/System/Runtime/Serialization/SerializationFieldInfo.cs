using System;
using System.Globalization;
using System.Reflection;
using System.Runtime.Remoting.Metadata;
using System.Security;
using System.Threading;

namespace System.Runtime.Serialization
{
	// Token: 0x020006EE RID: 1774
	internal sealed class SerializationFieldInfo : FieldInfo
	{
		// Token: 0x17000C86 RID: 3206
		// (get) Token: 0x06004AA9 RID: 19113 RVA: 0x0010B546 File Offset: 0x00109746
		public override Module Module
		{
			get
			{
				return this.m_field.Module;
			}
		}

		// Token: 0x17000C87 RID: 3207
		// (get) Token: 0x06004AAA RID: 19114 RVA: 0x0010B553 File Offset: 0x00109753
		public override int MetadataToken
		{
			get
			{
				return this.m_field.MetadataToken;
			}
		}

		// Token: 0x06004AAB RID: 19115 RVA: 0x0010B560 File Offset: 0x00109760
		internal SerializationFieldInfo(RuntimeFieldInfo field, string namePrefix)
		{
			this.m_field = field;
			this.m_serializationName = namePrefix + "+" + this.m_field.Name;
		}

		// Token: 0x17000C88 RID: 3208
		// (get) Token: 0x06004AAC RID: 19116 RVA: 0x0010B58B File Offset: 0x0010978B
		public override string Name
		{
			get
			{
				return this.m_serializationName;
			}
		}

		// Token: 0x17000C89 RID: 3209
		// (get) Token: 0x06004AAD RID: 19117 RVA: 0x0010B593 File Offset: 0x00109793
		public override Type DeclaringType
		{
			get
			{
				return this.m_field.DeclaringType;
			}
		}

		// Token: 0x17000C8A RID: 3210
		// (get) Token: 0x06004AAE RID: 19118 RVA: 0x0010B5A0 File Offset: 0x001097A0
		public override Type ReflectedType
		{
			get
			{
				return this.m_field.ReflectedType;
			}
		}

		// Token: 0x06004AAF RID: 19119 RVA: 0x0010B5AD File Offset: 0x001097AD
		public override object[] GetCustomAttributes(bool inherit)
		{
			return this.m_field.GetCustomAttributes(inherit);
		}

		// Token: 0x06004AB0 RID: 19120 RVA: 0x0010B5BB File Offset: 0x001097BB
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return this.m_field.GetCustomAttributes(attributeType, inherit);
		}

		// Token: 0x06004AB1 RID: 19121 RVA: 0x0010B5CA File Offset: 0x001097CA
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return this.m_field.IsDefined(attributeType, inherit);
		}

		// Token: 0x17000C8B RID: 3211
		// (get) Token: 0x06004AB2 RID: 19122 RVA: 0x0010B5D9 File Offset: 0x001097D9
		public override Type FieldType
		{
			get
			{
				return this.m_field.FieldType;
			}
		}

		// Token: 0x06004AB3 RID: 19123 RVA: 0x0010B5E6 File Offset: 0x001097E6
		public override object GetValue(object obj)
		{
			return this.m_field.GetValue(obj);
		}

		// Token: 0x06004AB4 RID: 19124 RVA: 0x0010B5F4 File Offset: 0x001097F4
		[SecurityCritical]
		internal object InternalGetValue(object obj)
		{
			RtFieldInfo rtFieldInfo = this.m_field as RtFieldInfo;
			if (rtFieldInfo != null)
			{
				rtFieldInfo.CheckConsistency(obj);
				return rtFieldInfo.UnsafeGetValue(obj);
			}
			return this.m_field.GetValue(obj);
		}

		// Token: 0x06004AB5 RID: 19125 RVA: 0x0010B631 File Offset: 0x00109831
		public override void SetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, CultureInfo culture)
		{
			this.m_field.SetValue(obj, value, invokeAttr, binder, culture);
		}

		// Token: 0x06004AB6 RID: 19126 RVA: 0x0010B648 File Offset: 0x00109848
		[SecurityCritical]
		internal void InternalSetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, CultureInfo culture)
		{
			RtFieldInfo rtFieldInfo = this.m_field as RtFieldInfo;
			if (rtFieldInfo != null)
			{
				rtFieldInfo.CheckConsistency(obj);
				rtFieldInfo.UnsafeSetValue(obj, value, invokeAttr, binder, culture);
				return;
			}
			this.m_field.SetValue(obj, value, invokeAttr, binder, culture);
		}

		// Token: 0x17000C8C RID: 3212
		// (get) Token: 0x06004AB7 RID: 19127 RVA: 0x0010B691 File Offset: 0x00109891
		internal RuntimeFieldInfo FieldInfo
		{
			get
			{
				return this.m_field;
			}
		}

		// Token: 0x17000C8D RID: 3213
		// (get) Token: 0x06004AB8 RID: 19128 RVA: 0x0010B699 File Offset: 0x00109899
		public override RuntimeFieldHandle FieldHandle
		{
			get
			{
				return this.m_field.FieldHandle;
			}
		}

		// Token: 0x17000C8E RID: 3214
		// (get) Token: 0x06004AB9 RID: 19129 RVA: 0x0010B6A6 File Offset: 0x001098A6
		public override FieldAttributes Attributes
		{
			get
			{
				return this.m_field.Attributes;
			}
		}

		// Token: 0x17000C8F RID: 3215
		// (get) Token: 0x06004ABA RID: 19130 RVA: 0x0010B6B4 File Offset: 0x001098B4
		internal RemotingFieldCachedData RemotingCache
		{
			get
			{
				RemotingFieldCachedData remotingFieldCachedData = this.m_cachedData;
				if (remotingFieldCachedData == null)
				{
					remotingFieldCachedData = new RemotingFieldCachedData(this);
					RemotingFieldCachedData remotingFieldCachedData2 = Interlocked.CompareExchange<RemotingFieldCachedData>(ref this.m_cachedData, remotingFieldCachedData, null);
					if (remotingFieldCachedData2 != null)
					{
						remotingFieldCachedData = remotingFieldCachedData2;
					}
				}
				return remotingFieldCachedData;
			}
		}

		// Token: 0x04002704 RID: 9988
		internal const string FakeNameSeparatorString = "+";

		// Token: 0x04002705 RID: 9989
		private RuntimeFieldInfo m_field;

		// Token: 0x04002706 RID: 9990
		private string m_serializationName;

		// Token: 0x04002707 RID: 9991
		private RemotingFieldCachedData m_cachedData;
	}
}
