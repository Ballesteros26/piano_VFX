using System;
using System.Collections.Generic;
using Melanchall.DryWetMidi.Interaction;

namespace Melanchall.DryWetMidi.Tools
{
	// Token: 0x02000013 RID: 19
	internal static class TimeSetter
	{
		// Token: 0x060000B8 RID: 184 RVA: 0x000046DA File Offset: 0x000028DA
		public static void SetObjectTime<TObject>(TObject obj, long time) where TObject : ITimedObject
		{
			TimeSetter.TimeSetters[obj.GetType()](obj, time);
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00004700 File Offset: 0x00002900
		// Note: this type is marked as 'beforefieldinit'.
		static TimeSetter()
		{
			Dictionary<Type, Action<ITimedObject, long>> dictionary = new Dictionary<Type, Action<ITimedObject, long>>();
			Type typeFromHandle = typeof(TimedEvent);
			dictionary[typeFromHandle] = delegate(ITimedObject obj, long time)
			{
				((TimedEvent)obj).Time = time;
			};
			Type typeFromHandle2 = typeof(Note);
			dictionary[typeFromHandle2] = delegate(ITimedObject obj, long time)
			{
				((Note)obj).Time = time;
			};
			Type typeFromHandle3 = typeof(Chord);
			dictionary[typeFromHandle3] = delegate(ITimedObject obj, long time)
			{
				((Chord)obj).Time = time;
			};
			TimeSetter.TimeSetters = dictionary;
		}

		// Token: 0x04000077 RID: 119
		private static readonly Dictionary<Type, Action<ITimedObject, long>> TimeSetters;
	}
}
