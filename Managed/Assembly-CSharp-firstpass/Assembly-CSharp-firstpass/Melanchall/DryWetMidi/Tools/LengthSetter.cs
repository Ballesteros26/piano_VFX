using System;
using System.Collections.Generic;
using Melanchall.DryWetMidi.Interaction;

namespace Melanchall.DryWetMidi.Tools
{
	// Token: 0x0200000F RID: 15
	internal static class LengthSetter
	{
		// Token: 0x060000B1 RID: 177 RVA: 0x0000460A File Offset: 0x0000280A
		public static void SetObjectLength<TObject>(TObject obj, long time) where TObject : ILengthedObject
		{
			LengthSetter.LengthSetters[obj.GetType()](obj, time);
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00004630 File Offset: 0x00002830
		// Note: this type is marked as 'beforefieldinit'.
		static LengthSetter()
		{
			Dictionary<Type, Action<ILengthedObject, long>> dictionary = new Dictionary<Type, Action<ILengthedObject, long>>();
			Type typeFromHandle = typeof(Note);
			dictionary[typeFromHandle] = delegate(ILengthedObject obj, long length)
			{
				((Note)obj).Length = length;
			};
			Type typeFromHandle2 = typeof(Chord);
			dictionary[typeFromHandle2] = delegate(ILengthedObject obj, long length)
			{
				((Chord)obj).Length = length;
			};
			LengthSetter.LengthSetters = dictionary;
		}

		// Token: 0x0400006C RID: 108
		private static readonly Dictionary<Type, Action<ILengthedObject, long>> LengthSetters;
	}
}
