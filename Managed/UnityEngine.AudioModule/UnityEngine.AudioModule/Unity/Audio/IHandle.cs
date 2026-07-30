using System;
using UnityEngine.Bindings;

namespace Unity.Audio
{
	// Token: 0x02000032 RID: 50
	[VisibleToOtherModules]
	internal interface IHandle<HandleType> : IValidatable, IEquatable<HandleType> where HandleType : struct, IHandle<HandleType>
	{
	}
}
