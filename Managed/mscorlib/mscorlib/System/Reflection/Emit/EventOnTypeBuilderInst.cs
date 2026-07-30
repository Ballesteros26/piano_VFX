using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x02000357 RID: 855
	[StructLayout(LayoutKind.Sequential)]
	internal class EventOnTypeBuilderInst : EventInfo
	{
		// Token: 0x06002651 RID: 9809 RVA: 0x00088B24 File Offset: 0x00086D24
		internal EventOnTypeBuilderInst(TypeBuilderInstantiation instantiation, EventBuilder evt)
		{
			this.instantiation = instantiation;
			this.event_builder = evt;
		}

		// Token: 0x06002652 RID: 9810 RVA: 0x00088B3A File Offset: 0x00086D3A
		internal EventOnTypeBuilderInst(TypeBuilderInstantiation instantiation, EventInfo evt)
		{
			this.instantiation = instantiation;
			this.event_info = evt;
		}

		// Token: 0x170005FD RID: 1533
		// (get) Token: 0x06002653 RID: 9811 RVA: 0x00088B50 File Offset: 0x00086D50
		public override EventAttributes Attributes
		{
			get
			{
				if (this.event_builder == null)
				{
					return this.event_info.Attributes;
				}
				return this.event_builder.attrs;
			}
		}

		// Token: 0x06002654 RID: 9812 RVA: 0x00088B74 File Offset: 0x00086D74
		public override MethodInfo GetAddMethod(bool nonPublic)
		{
			MethodInfo methodInfo = ((this.event_builder != null) ? this.event_builder.add_method : this.event_info.GetAddMethod(nonPublic));
			if (methodInfo == null || (!nonPublic && !methodInfo.IsPublic))
			{
				return null;
			}
			return TypeBuilder.GetMethod(this.instantiation, methodInfo);
		}

		// Token: 0x06002655 RID: 9813 RVA: 0x00088BC8 File Offset: 0x00086DC8
		public override MethodInfo GetRaiseMethod(bool nonPublic)
		{
			MethodInfo methodInfo = ((this.event_builder != null) ? this.event_builder.raise_method : this.event_info.GetRaiseMethod(nonPublic));
			if (methodInfo == null || (!nonPublic && !methodInfo.IsPublic))
			{
				return null;
			}
			return TypeBuilder.GetMethod(this.instantiation, methodInfo);
		}

		// Token: 0x06002656 RID: 9814 RVA: 0x00088C1C File Offset: 0x00086E1C
		public override MethodInfo GetRemoveMethod(bool nonPublic)
		{
			MethodInfo methodInfo = ((this.event_builder != null) ? this.event_builder.remove_method : this.event_info.GetRemoveMethod(nonPublic));
			if (methodInfo == null || (!nonPublic && !methodInfo.IsPublic))
			{
				return null;
			}
			return TypeBuilder.GetMethod(this.instantiation, methodInfo);
		}

		// Token: 0x06002657 RID: 9815 RVA: 0x00088C70 File Offset: 0x00086E70
		public override MethodInfo[] GetOtherMethods(bool nonPublic)
		{
			MethodInfo[] array = ((this.event_builder != null) ? this.event_builder.other_methods : this.event_info.GetOtherMethods(nonPublic));
			if (array == null)
			{
				return new MethodInfo[0];
			}
			ArrayList arrayList = new ArrayList();
			foreach (MethodInfo methodInfo in array)
			{
				if (nonPublic || methodInfo.IsPublic)
				{
					arrayList.Add(TypeBuilder.GetMethod(this.instantiation, methodInfo));
				}
			}
			MethodInfo[] array3 = new MethodInfo[arrayList.Count];
			arrayList.CopyTo(array3, 0);
			return array3;
		}

		// Token: 0x170005FE RID: 1534
		// (get) Token: 0x06002658 RID: 9816 RVA: 0x00088CFE File Offset: 0x00086EFE
		public override Type DeclaringType
		{
			get
			{
				return this.instantiation;
			}
		}

		// Token: 0x170005FF RID: 1535
		// (get) Token: 0x06002659 RID: 9817 RVA: 0x00088D06 File Offset: 0x00086F06
		public override string Name
		{
			get
			{
				if (this.event_builder == null)
				{
					return this.event_info.Name;
				}
				return this.event_builder.name;
			}
		}

		// Token: 0x17000600 RID: 1536
		// (get) Token: 0x0600265A RID: 9818 RVA: 0x00088CFE File Offset: 0x00086EFE
		public override Type ReflectedType
		{
			get
			{
				return this.instantiation;
			}
		}

		// Token: 0x0600265B RID: 9819 RVA: 0x00014B5A File Offset: 0x00012D5A
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600265C RID: 9820 RVA: 0x00014B5A File Offset: 0x00012D5A
		public override object[] GetCustomAttributes(bool inherit)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600265D RID: 9821 RVA: 0x00014B5A File Offset: 0x00012D5A
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			throw new NotSupportedException();
		}

		// Token: 0x04001402 RID: 5122
		private TypeBuilderInstantiation instantiation;

		// Token: 0x04001403 RID: 5123
		private EventBuilder event_builder;

		// Token: 0x04001404 RID: 5124
		private EventInfo event_info;
	}
}
