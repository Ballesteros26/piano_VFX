using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x02000325 RID: 805
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	internal sealed class MonoEvent : RuntimeEventInfo
	{
		// Token: 0x1700054C RID: 1356
		// (get) Token: 0x06002354 RID: 9044 RVA: 0x0008230D File Offset: 0x0008050D
		public override EventAttributes Attributes
		{
			get
			{
				return MonoEventInfo.GetEventInfo(this).attrs;
			}
		}

		// Token: 0x06002355 RID: 9045 RVA: 0x0008231C File Offset: 0x0008051C
		public override MethodInfo GetAddMethod(bool nonPublic)
		{
			MonoEventInfo eventInfo = MonoEventInfo.GetEventInfo(this);
			if (nonPublic || (eventInfo.add_method != null && eventInfo.add_method.IsPublic))
			{
				return eventInfo.add_method;
			}
			return null;
		}

		// Token: 0x06002356 RID: 9046 RVA: 0x00082358 File Offset: 0x00080558
		public override MethodInfo GetRaiseMethod(bool nonPublic)
		{
			MonoEventInfo eventInfo = MonoEventInfo.GetEventInfo(this);
			if (nonPublic || (eventInfo.raise_method != null && eventInfo.raise_method.IsPublic))
			{
				return eventInfo.raise_method;
			}
			return null;
		}

		// Token: 0x06002357 RID: 9047 RVA: 0x00082394 File Offset: 0x00080594
		public override MethodInfo GetRemoveMethod(bool nonPublic)
		{
			MonoEventInfo eventInfo = MonoEventInfo.GetEventInfo(this);
			if (nonPublic || (eventInfo.remove_method != null && eventInfo.remove_method.IsPublic))
			{
				return eventInfo.remove_method;
			}
			return null;
		}

		// Token: 0x06002358 RID: 9048 RVA: 0x000823D0 File Offset: 0x000805D0
		public override MethodInfo[] GetOtherMethods(bool nonPublic)
		{
			MonoEventInfo eventInfo = MonoEventInfo.GetEventInfo(this);
			if (nonPublic)
			{
				return eventInfo.other_methods;
			}
			int num = 0;
			MethodInfo[] array = eventInfo.other_methods;
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i].IsPublic)
				{
					num++;
				}
			}
			if (num == eventInfo.other_methods.Length)
			{
				return eventInfo.other_methods;
			}
			MethodInfo[] array2 = new MethodInfo[num];
			num = 0;
			foreach (MethodInfo methodInfo in eventInfo.other_methods)
			{
				if (methodInfo.IsPublic)
				{
					array2[num++] = methodInfo;
				}
			}
			return array2;
		}

		// Token: 0x1700054D RID: 1357
		// (get) Token: 0x06002359 RID: 9049 RVA: 0x00082465 File Offset: 0x00080665
		public override Type DeclaringType
		{
			get
			{
				return MonoEventInfo.GetEventInfo(this).declaring_type;
			}
		}

		// Token: 0x1700054E RID: 1358
		// (get) Token: 0x0600235A RID: 9050 RVA: 0x00082472 File Offset: 0x00080672
		public override Type ReflectedType
		{
			get
			{
				return MonoEventInfo.GetEventInfo(this).reflected_type;
			}
		}

		// Token: 0x1700054F RID: 1359
		// (get) Token: 0x0600235B RID: 9051 RVA: 0x0008247F File Offset: 0x0008067F
		public override string Name
		{
			get
			{
				return MonoEventInfo.GetEventInfo(this).name;
			}
		}

		// Token: 0x0600235C RID: 9052 RVA: 0x0008248C File Offset: 0x0008068C
		public override string ToString()
		{
			return this.EventHandlerType + " " + this.Name;
		}

		// Token: 0x0600235D RID: 9053 RVA: 0x000330F9 File Offset: 0x000312F9
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return MonoCustomAttrs.IsDefined(this, attributeType, inherit);
		}

		// Token: 0x0600235E RID: 9054 RVA: 0x0007F7D9 File Offset: 0x0007D9D9
		public override object[] GetCustomAttributes(bool inherit)
		{
			return MonoCustomAttrs.GetCustomAttributes(this, inherit);
		}

		// Token: 0x0600235F RID: 9055 RVA: 0x0007F7E2 File Offset: 0x0007D9E2
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return MonoCustomAttrs.GetCustomAttributes(this, attributeType, inherit);
		}

		// Token: 0x06002360 RID: 9056 RVA: 0x000824A4 File Offset: 0x000806A4
		public override IList<CustomAttributeData> GetCustomAttributesData()
		{
			return CustomAttributeData.GetCustomAttributes(this);
		}

		// Token: 0x04001340 RID: 4928
		private IntPtr klass;

		// Token: 0x04001341 RID: 4929
		private IntPtr handle;
	}
}
