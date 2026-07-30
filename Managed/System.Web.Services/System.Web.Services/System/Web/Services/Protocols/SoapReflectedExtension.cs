using System;

namespace System.Web.Services.Protocols
{
	// Token: 0x02000073 RID: 115
	internal class SoapReflectedExtension : IComparable
	{
		// Token: 0x060002F6 RID: 758 RVA: 0x0000D1B5 File Offset: 0x0000B3B5
		internal SoapReflectedExtension(Type type, SoapExtensionAttribute attribute)
			: this(type, attribute, attribute.Priority)
		{
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x0000D1C8 File Offset: 0x0000B3C8
		internal SoapReflectedExtension(Type type, SoapExtensionAttribute attribute, int priority)
		{
			if (priority < 0)
			{
				throw new ArgumentException(Res.GetString("WebConfigInvalidExtensionPriority", new object[] { priority }), "priority");
			}
			this.type = type;
			this.attribute = attribute;
			this.priority = priority;
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x0000D218 File Offset: 0x0000B418
		internal SoapExtension CreateInstance(object initializer)
		{
			SoapExtension soapExtension = (SoapExtension)Activator.CreateInstance(this.type);
			soapExtension.Initialize(initializer);
			return soapExtension;
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x0000D231 File Offset: 0x0000B431
		internal object GetInitializer(LogicalMethodInfo methodInfo)
		{
			return ((SoapExtension)Activator.CreateInstance(this.type)).GetInitializer(methodInfo, this.attribute);
		}

		// Token: 0x060002FA RID: 762 RVA: 0x0000D24F File Offset: 0x0000B44F
		internal object GetInitializer(Type serviceType)
		{
			return ((SoapExtension)Activator.CreateInstance(this.type)).GetInitializer(serviceType);
		}

		// Token: 0x060002FB RID: 763 RVA: 0x0000D268 File Offset: 0x0000B468
		internal static object[] GetInitializers(LogicalMethodInfo methodInfo, SoapReflectedExtension[] extensions)
		{
			object[] array = new object[extensions.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = extensions[i].GetInitializer(methodInfo);
			}
			return array;
		}

		// Token: 0x060002FC RID: 764 RVA: 0x0000D29C File Offset: 0x0000B49C
		internal static object[] GetInitializers(Type serviceType, SoapReflectedExtension[] extensions)
		{
			object[] array = new object[extensions.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = extensions[i].GetInitializer(serviceType);
			}
			return array;
		}

		// Token: 0x060002FD RID: 765 RVA: 0x0000D2CD File Offset: 0x0000B4CD
		public int CompareTo(object o)
		{
			return this.priority - ((SoapReflectedExtension)o).priority;
		}

		// Token: 0x040002AF RID: 687
		private Type type;

		// Token: 0x040002B0 RID: 688
		private SoapExtensionAttribute attribute;

		// Token: 0x040002B1 RID: 689
		private int priority;
	}
}
