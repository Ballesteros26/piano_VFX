using System;
using System.Security;

namespace System.Runtime.Serialization
{
	// Token: 0x020006CF RID: 1743
	internal sealed class SurrogateForCyclicalReference : ISerializationSurrogate
	{
		// Token: 0x060049E7 RID: 18919 RVA: 0x00108E29 File Offset: 0x00107029
		internal SurrogateForCyclicalReference(ISerializationSurrogate innerSurrogate)
		{
			if (innerSurrogate == null)
			{
				throw new ArgumentNullException("innerSurrogate");
			}
			this.innerSurrogate = innerSurrogate;
		}

		// Token: 0x060049E8 RID: 18920 RVA: 0x00108E46 File Offset: 0x00107046
		[SecurityCritical]
		public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
		{
			this.innerSurrogate.GetObjectData(obj, info, context);
		}

		// Token: 0x060049E9 RID: 18921 RVA: 0x00108E56 File Offset: 0x00107056
		[SecurityCritical]
		public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
		{
			return this.innerSurrogate.SetObjectData(obj, info, context, selector);
		}

		// Token: 0x040026B0 RID: 9904
		private ISerializationSurrogate innerSurrogate;
	}
}
