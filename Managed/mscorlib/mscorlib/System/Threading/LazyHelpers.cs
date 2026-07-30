using System;

namespace System.Threading
{
	// Token: 0x02000459 RID: 1113
	internal static class LazyHelpers<T>
	{
		// Token: 0x0600352C RID: 13612 RVA: 0x000C47D4 File Offset: 0x000C29D4
		private static T ActivatorFactorySelector()
		{
			T t;
			try
			{
				t = (T)((object)Activator.CreateInstance(typeof(T)));
			}
			catch (MissingMethodException)
			{
				throw new MissingMemberException(Environment.GetResourceString("The lazily-initialized type does not have a public, parameterless constructor."));
			}
			return t;
		}

		// Token: 0x04001C54 RID: 7252
		internal static Func<T> s_activatorFactorySelector = new Func<T>(LazyHelpers<T>.ActivatorFactorySelector);
	}
}
