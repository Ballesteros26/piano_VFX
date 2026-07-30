using System;
using System.Reflection;

namespace UnityEngine.Events
{
	// Token: 0x0200024F RID: 591
	internal abstract class BaseInvokableCall
	{
		// Token: 0x06001930 RID: 6448 RVA: 0x000166AA File Offset: 0x000148AA
		protected BaseInvokableCall()
		{
		}

		// Token: 0x06001931 RID: 6449 RVA: 0x00028744 File Offset: 0x00026944
		protected BaseInvokableCall(object target, MethodInfo function)
		{
			bool isStatic = function.IsStatic;
			if (isStatic)
			{
				bool flag = target != null;
				if (flag)
				{
					throw new ArgumentException("target must be null");
				}
			}
			else
			{
				bool flag2 = target == null;
				if (flag2)
				{
					throw new ArgumentNullException("target");
				}
			}
			bool flag3 = function == null;
			if (flag3)
			{
				throw new ArgumentNullException("function");
			}
		}

		// Token: 0x06001932 RID: 6450
		public abstract void Invoke(object[] args);

		// Token: 0x06001933 RID: 6451 RVA: 0x000287A4 File Offset: 0x000269A4
		protected static void ThrowOnInvalidArg<T>(object arg)
		{
			bool flag = arg != null && !(arg is T);
			if (flag)
			{
				throw new ArgumentException(UnityString.Format("Passed argument 'args[0]' is of the wrong type. Type:{0} Expected:{1}", new object[]
				{
					arg.GetType(),
					typeof(T)
				}));
			}
		}

		// Token: 0x06001934 RID: 6452 RVA: 0x000287F4 File Offset: 0x000269F4
		protected static bool AllowInvoke(Delegate @delegate)
		{
			object target = @delegate.Target;
			bool flag = target == null;
			bool flag2;
			if (flag)
			{
				flag2 = true;
			}
			else
			{
				Object @object = target as Object;
				bool flag3 = @object != null;
				flag2 = !flag3 || @object != null;
			}
			return flag2;
		}

		// Token: 0x06001935 RID: 6453
		public abstract bool Find(object targetObj, MethodInfo method);
	}
}
