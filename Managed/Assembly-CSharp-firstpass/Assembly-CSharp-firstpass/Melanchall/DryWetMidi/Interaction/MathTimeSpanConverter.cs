using System;
using System.Collections.Generic;

namespace Melanchall.DryWetMidi.Interaction
{
	// Token: 0x020000B3 RID: 179
	internal sealed class MathTimeSpanConverter : ITimeSpanConverter
	{
		// Token: 0x06000413 RID: 1043 RVA: 0x000140AC File Offset: 0x000122AC
		public ITimeSpan ConvertTo(long timeSpan, long time, TempoMap tempoMap)
		{
			throw new NotSupportedException("Conversion to the MathTimeSpan is not supported.");
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x000140B8 File Offset: 0x000122B8
		public long ConvertFrom(ITimeSpan timeSpan, long time, TempoMap tempoMap)
		{
			MathTimeSpan mathTimeSpan = (MathTimeSpan)timeSpan;
			Func<MathTimeSpan, long, TempoMap, long> func;
			if (MathTimeSpanConverter.Converters.TryGetValue(mathTimeSpan.Mode, out func))
			{
				return func(mathTimeSpan, time, tempoMap);
			}
			throw new ArgumentException(string.Format("{0} mode is not supported by the converter.", mathTimeSpan.Mode), "timeSpan");
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x0001410C File Offset: 0x0001230C
		private static long ConvertFromLengthLength(MathTimeSpan mathTimeSpan, long time, TempoMap tempoMap)
		{
			long num = LengthConverter.ConvertFrom(mathTimeSpan.TimeSpan1, time, tempoMap);
			long num2 = time + num;
			MathOperation operation = mathTimeSpan.Operation;
			if (operation == MathOperation.Add)
			{
				return num + LengthConverter.ConvertFrom(mathTimeSpan.TimeSpan2, num2, tempoMap);
			}
			if (operation != MathOperation.Subtract)
			{
				throw new ArgumentException(string.Format("{0} is not supported by the converter.", mathTimeSpan.Operation), "mathTimeSpan");
			}
			return num - LengthConverter.ConvertFrom(mathTimeSpan.TimeSpan2, num2, tempoMap.Flip(num2));
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x00014180 File Offset: 0x00012380
		private static long ConvertFromTimeLength(MathTimeSpan mathTimeSpan, long time, TempoMap tempoMap)
		{
			long num = TimeConverter.ConvertFrom(mathTimeSpan.TimeSpan1, tempoMap);
			MathOperation operation = mathTimeSpan.Operation;
			if (operation == MathOperation.Add)
			{
				return num + LengthConverter.ConvertFrom(mathTimeSpan.TimeSpan2, num, tempoMap);
			}
			if (operation != MathOperation.Subtract)
			{
				throw new ArgumentException(string.Format("{0} is not supported by the converter.", mathTimeSpan.Operation), "mathTimeSpan");
			}
			return num - LengthConverter.ConvertFrom(mathTimeSpan.TimeSpan2, num, tempoMap.Flip(num));
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x000141F0 File Offset: 0x000123F0
		private static long ConvertFromTimeTime(MathTimeSpan mathTimeSpan, long time, TempoMap tempoMap)
		{
			ITimeSpan timeSpan = mathTimeSpan.TimeSpan1;
			ITimeSpan timeSpan2 = mathTimeSpan.TimeSpan2;
			MathTimeSpan mathTimeSpan2 = mathTimeSpan.TimeSpan1 as MathTimeSpan;
			if (mathTimeSpan2 != null)
			{
				timeSpan = TimeSpanConverter.ConvertTo(mathTimeSpan2, mathTimeSpan2.TimeSpan1.GetType(), time, tempoMap);
			}
			MathOperation operation = mathTimeSpan.Operation;
			if (operation == MathOperation.Subtract)
			{
				ITimeSpan timeSpan3 = TimeConverter.ConvertTo(timeSpan2, timeSpan.GetType(), tempoMap);
				return TimeSpanConverter.ConvertFrom(timeSpan.Subtract(timeSpan3, TimeSpanMode.TimeTime), time, tempoMap);
			}
			throw new ArgumentException(string.Format("{0} is not supported by the converter.", mathTimeSpan.Operation), "mathTimeSpan");
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x00014280 File Offset: 0x00012480
		// Note: this type is marked as 'beforefieldinit'.
		static MathTimeSpanConverter()
		{
			Dictionary<TimeSpanMode, Func<MathTimeSpan, long, TempoMap, long>> dictionary = new Dictionary<TimeSpanMode, Func<MathTimeSpan, long, TempoMap, long>>();
			dictionary[TimeSpanMode.TimeTime] = new Func<MathTimeSpan, long, TempoMap, long>(MathTimeSpanConverter.ConvertFromTimeTime);
			dictionary[TimeSpanMode.TimeLength] = new Func<MathTimeSpan, long, TempoMap, long>(MathTimeSpanConverter.ConvertFromTimeLength);
			dictionary[TimeSpanMode.LengthLength] = new Func<MathTimeSpan, long, TempoMap, long>(MathTimeSpanConverter.ConvertFromLengthLength);
			MathTimeSpanConverter.Converters = dictionary;
		}

		// Token: 0x040006A4 RID: 1700
		private static readonly Dictionary<TimeSpanMode, Func<MathTimeSpan, long, TempoMap, long>> Converters;
	}
}
