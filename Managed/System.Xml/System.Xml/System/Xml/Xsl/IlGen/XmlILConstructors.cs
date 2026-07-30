using System;
using System.Diagnostics;
using System.Reflection;
using System.Security;

namespace System.Xml.Xsl.IlGen
{
	// Token: 0x02000657 RID: 1623
	internal static class XmlILConstructors
	{
		// Token: 0x06004138 RID: 16696 RVA: 0x0015C22D File Offset: 0x0015A42D
		private static ConstructorInfo GetConstructor(Type className)
		{
			return className.GetConstructor(new Type[0]);
		}

		// Token: 0x06004139 RID: 16697 RVA: 0x0015C23B File Offset: 0x0015A43B
		private static ConstructorInfo GetConstructor(Type className, params Type[] args)
		{
			return className.GetConstructor(args);
		}

		// Token: 0x0400290E RID: 10510
		public static readonly ConstructorInfo DecFromParts = XmlILConstructors.GetConstructor(typeof(decimal), new Type[]
		{
			typeof(int),
			typeof(int),
			typeof(int),
			typeof(bool),
			typeof(byte)
		});

		// Token: 0x0400290F RID: 10511
		public static readonly ConstructorInfo DecFromInt32 = XmlILConstructors.GetConstructor(typeof(decimal), new Type[] { typeof(int) });

		// Token: 0x04002910 RID: 10512
		public static readonly ConstructorInfo DecFromInt64 = XmlILConstructors.GetConstructor(typeof(decimal), new Type[] { typeof(long) });

		// Token: 0x04002911 RID: 10513
		public static readonly ConstructorInfo Debuggable = XmlILConstructors.GetConstructor(typeof(DebuggableAttribute), new Type[] { typeof(DebuggableAttribute.DebuggingModes) });

		// Token: 0x04002912 RID: 10514
		public static readonly ConstructorInfo NonUserCode = XmlILConstructors.GetConstructor(typeof(DebuggerNonUserCodeAttribute));

		// Token: 0x04002913 RID: 10515
		public static readonly ConstructorInfo QName = XmlILConstructors.GetConstructor(typeof(XmlQualifiedName), new Type[]
		{
			typeof(string),
			typeof(string)
		});

		// Token: 0x04002914 RID: 10516
		public static readonly ConstructorInfo StepThrough = XmlILConstructors.GetConstructor(typeof(DebuggerStepThroughAttribute));

		// Token: 0x04002915 RID: 10517
		public static readonly ConstructorInfo Transparent = XmlILConstructors.GetConstructor(typeof(SecurityTransparentAttribute));
	}
}
