using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace UnityEngine.Serialization
{
	// Token: 0x02000266 RID: 614
	public class UnitySurrogateSelector : ISurrogateSelector
	{
		// Token: 0x060019D6 RID: 6614 RVA: 0x0002A34C File Offset: 0x0002854C
		public ISerializationSurrogate GetSurrogate(Type type, StreamingContext context, out ISurrogateSelector selector)
		{
			bool isGenericType = type.IsGenericType;
			if (isGenericType)
			{
				Type genericTypeDefinition = type.GetGenericTypeDefinition();
				bool flag = genericTypeDefinition == typeof(List);
				if (flag)
				{
					selector = this;
					return ListSerializationSurrogate.Default;
				}
				bool flag2 = genericTypeDefinition == typeof(Dictionary);
				if (flag2)
				{
					selector = this;
					Type type2 = typeof(DictionarySerializationSurrogate<, >).MakeGenericType(type.GetGenericArguments());
					return (ISerializationSurrogate)Activator.CreateInstance(type2);
				}
			}
			selector = null;
			return null;
		}

		// Token: 0x060019D7 RID: 6615 RVA: 0x000125E4 File Offset: 0x000107E4
		public void ChainSelector(ISurrogateSelector selector)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060019D8 RID: 6616 RVA: 0x000125E4 File Offset: 0x000107E4
		public ISurrogateSelector GetNextSelector()
		{
			throw new NotImplementedException();
		}
	}
}
