using System;
using System.Security;
using System.Threading;

namespace System.Runtime.Versioning
{
	// Token: 0x020006BE RID: 1726
	internal static class MultitargetingHelpers
	{
		// Token: 0x06004979 RID: 18809 RVA: 0x00107BC0 File Offset: 0x00105DC0
		internal static string GetAssemblyQualifiedName(Type type, Func<Type, string> converter)
		{
			string text = null;
			if (type != null)
			{
				if (converter != null)
				{
					try
					{
						text = converter(type);
					}
					catch (Exception ex)
					{
						if (MultitargetingHelpers.IsSecurityOrCriticalException(ex))
						{
							throw;
						}
					}
				}
				if (text == null)
				{
					text = MultitargetingHelpers.defaultConverter(type);
				}
			}
			return text;
		}

		// Token: 0x0600497A RID: 18810 RVA: 0x00107C10 File Offset: 0x00105E10
		private static bool IsCriticalException(Exception ex)
		{
			return ex is NullReferenceException || ex is StackOverflowException || ex is OutOfMemoryException || ex is ThreadAbortException || ex is IndexOutOfRangeException || ex is AccessViolationException;
		}

		// Token: 0x0600497B RID: 18811 RVA: 0x00107C45 File Offset: 0x00105E45
		private static bool IsSecurityOrCriticalException(Exception ex)
		{
			return ex is SecurityException || MultitargetingHelpers.IsCriticalException(ex);
		}

		// Token: 0x04002685 RID: 9861
		private static Func<Type, string> defaultConverter = (Type t) => t.AssemblyQualifiedName;
	}
}
