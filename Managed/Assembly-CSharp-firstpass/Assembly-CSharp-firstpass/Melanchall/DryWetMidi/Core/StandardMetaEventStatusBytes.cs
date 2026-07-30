using System;
using System.Linq;
using System.Reflection;

namespace Melanchall.DryWetMidi.Core
{
	// Token: 0x0200013D RID: 317
	internal static class StandardMetaEventStatusBytes
	{
		// Token: 0x06000825 RID: 2085 RVA: 0x0001EB94 File Offset: 0x0001CD94
		public static byte[] GetStatusBytes()
		{
			byte[] array;
			if ((array = StandardMetaEventStatusBytes._statusBytes) == null)
			{
				array = (StandardMetaEventStatusBytes._statusBytes = (from f in typeof(EventStatusBytes.Meta).GetFields(BindingFlags.Static | BindingFlags.Public)
					select (byte)f.GetValue(null)).ToArray<byte>());
			}
			return array;
		}

		// Token: 0x0400089A RID: 2202
		private static byte[] _statusBytes;
	}
}
