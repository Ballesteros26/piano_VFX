using System;
using System.Collections;
using System.Diagnostics;
using System.Reflection;

namespace System.Runtime.InteropServices
{
	/// <summary>Enables customization of managed objects that extend from unmanaged objects during creation.</summary>
	// Token: 0x02000913 RID: 2323
	[ComVisible(true)]
	public sealed class ExtensibleClassFactory
	{
		// Token: 0x060055D6 RID: 21974 RVA: 0x00002111 File Offset: 0x00000311
		private ExtensibleClassFactory()
		{
		}

		// Token: 0x060055D7 RID: 21975 RVA: 0x0012934F File Offset: 0x0012754F
		internal static ObjectCreationDelegate GetObjectCreationCallback(Type t)
		{
			return ExtensibleClassFactory.hashtable[t] as ObjectCreationDelegate;
		}

		/// <summary>Registers a delegate that is called when an instance of a managed type, that extends from an unmanaged type, needs to allocate the aggregated unmanaged object.</summary>
		/// <param name="callback">A delegate that is called in place of CoCreateInstance. </param>
		// Token: 0x060055D8 RID: 21976 RVA: 0x00129364 File Offset: 0x00127564
		public static void RegisterObjectCreationCallback(ObjectCreationDelegate callback)
		{
			int i = 1;
			StackTrace stackTrace = new StackTrace(false);
			while (i < stackTrace.FrameCount)
			{
				MethodBase method = stackTrace.GetFrame(i).GetMethod();
				if (method.MemberType == MemberTypes.Constructor && method.IsStatic)
				{
					ExtensibleClassFactory.hashtable.Add(method.DeclaringType, callback);
					return;
				}
				i++;
			}
			throw new InvalidOperationException("RegisterObjectCreationCallback must be called from .cctor of class derived from ComImport type.");
		}

		// Token: 0x04002D9A RID: 11674
		private static readonly Hashtable hashtable = new Hashtable();
	}
}
