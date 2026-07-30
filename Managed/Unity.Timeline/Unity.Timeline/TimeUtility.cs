using System;
using System.Text.RegularExpressions;

namespace UnityEngine.Timeline
{
	// Token: 0x0200004D RID: 77
	internal static class TimeUtility
	{
		// Token: 0x060002CF RID: 719 RVA: 0x00009DC6 File Offset: 0x00007FC6
		private static void ValidateFrameRate(double frameRate)
		{
			if (frameRate <= TimeUtility.kTimeEpsilon)
			{
				throw new ArgumentException("frame rate cannot be 0 or negative");
			}
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x00009DDC File Offset: 0x00007FDC
		public static int ToFrames(double time, double frameRate)
		{
			TimeUtility.ValidateFrameRate(frameRate);
			time = Math.Min(Math.Max(time, -TimeUtility.k_MaxTimelineDurationInSeconds), TimeUtility.k_MaxTimelineDurationInSeconds);
			double num = TimeUtility.GetEpsilon(time, frameRate) / 2.0;
			if (time < 0.0)
			{
				return (int)Math.Ceiling(time * frameRate - num);
			}
			return (int)Math.Floor(time * frameRate + num);
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x00009E3C File Offset: 0x0000803C
		public static double ToExactFrames(double time, double frameRate)
		{
			TimeUtility.ValidateFrameRate(frameRate);
			return time * frameRate;
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x00009E47 File Offset: 0x00008047
		public static double FromFrames(int frames, double frameRate)
		{
			TimeUtility.ValidateFrameRate(frameRate);
			return (double)frames / frameRate;
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x00009E53 File Offset: 0x00008053
		public static double FromFrames(double frames, double frameRate)
		{
			TimeUtility.ValidateFrameRate(frameRate);
			return frames / frameRate;
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x00009E5E File Offset: 0x0000805E
		public static bool OnFrameBoundary(double time, double frameRate)
		{
			return TimeUtility.OnFrameBoundary(time, frameRate, TimeUtility.GetEpsilon(time, frameRate));
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x00009E6E File Offset: 0x0000806E
		public static double GetEpsilon(double time, double frameRate)
		{
			return Math.Max(Math.Abs(time), 1.0) * frameRate * TimeUtility.kTimeEpsilon;
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x00009E8C File Offset: 0x0000808C
		public static bool OnFrameBoundary(double time, double frameRate, double epsilon)
		{
			TimeUtility.ValidateFrameRate(frameRate);
			double num = TimeUtility.ToExactFrames(time, frameRate);
			double num2 = Math.Round(num);
			return Math.Abs(num - num2) < epsilon;
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x00009EB8 File Offset: 0x000080B8
		public static double RoundToFrame(double time, double frameRate)
		{
			TimeUtility.ValidateFrameRate(frameRate);
			double num = (double)((int)Math.Floor(time * frameRate)) / frameRate;
			double num2 = (double)((int)Math.Ceiling(time * frameRate)) / frameRate;
			if (Math.Abs(time - num) >= Math.Abs(time - num2))
			{
				return num2;
			}
			return num;
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x00009EFC File Offset: 0x000080FC
		public static string TimeAsFrames(double timeValue, double frameRate, string format = "F2")
		{
			if (TimeUtility.OnFrameBoundary(timeValue, frameRate))
			{
				return TimeUtility.ToFrames(timeValue, frameRate).ToString();
			}
			return TimeUtility.ToExactFrames(timeValue, frameRate).ToString(format);
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x00009F34 File Offset: 0x00008134
		public static string TimeAsTimeCode(double timeValue, double frameRate, string format = "F2")
		{
			TimeUtility.ValidateFrameRate(frameRate);
			int num = (int)Math.Abs(timeValue);
			int num2 = num / 3600;
			int num3 = num % 3600 / 60;
			int num4 = num % 60;
			string text = ((timeValue < 0.0) ? "-" : string.Empty);
			string text2;
			if (num2 > 0)
			{
				text2 = string.Concat(new object[]
				{
					num2,
					":",
					num3.ToString("D2"),
					":",
					num4.ToString("D2")
				});
			}
			else if (num3 > 0)
			{
				text2 = num3 + ":" + num4.ToString("D2");
			}
			else
			{
				text2 = num4.ToString();
			}
			int num5 = (int)Math.Floor(Math.Log10(frameRate) + 1.0);
			string text3 = (TimeUtility.ToFrames(timeValue, frameRate) - TimeUtility.ToFrames((double)num, frameRate)).ToString().PadLeft(num5, '0');
			if (!TimeUtility.OnFrameBoundary(timeValue, frameRate))
			{
				string text4 = TimeUtility.ToExactFrames(timeValue, frameRate).ToString(format);
				int num6 = text4.IndexOf('.');
				if (num6 >= 0)
				{
					text3 = text3 + " [" + text4.Substring(num6) + "]";
				}
			}
			return text + text2 + ":" + text3;
		}

		// Token: 0x060002DA RID: 730 RVA: 0x0000A08C File Offset: 0x0000828C
		public static double ParseTimeCode(string timeCode, double frameRate, double defaultValue)
		{
			timeCode = TimeUtility.RemoveChar(timeCode, (char c) => char.IsWhiteSpace(c));
			string[] array = timeCode.Split(new char[] { ':' });
			if (array.Length == 0 || array.Length > 4)
			{
				return defaultValue;
			}
			int num = 0;
			int num2 = 0;
			double num3 = 0.0;
			double num4 = 0.0;
			try
			{
				string text = array[array.Length - 1];
				if (Regex.Match(text, "^\\d+\\.\\d+$").Success)
				{
					num3 = double.Parse(text);
					if (array.Length > 3)
					{
						return defaultValue;
					}
					if (array.Length > 1)
					{
						num2 = int.Parse(array[array.Length - 2]);
					}
					if (array.Length > 2)
					{
						num = int.Parse(array[array.Length - 3]);
					}
				}
				else
				{
					if (Regex.Match(text, "^\\d+\\[\\.\\d+\\]$").Success)
					{
						num4 = double.Parse(TimeUtility.RemoveChar(text, (char c) => c == '[' || c == ']'));
					}
					else
					{
						if (!Regex.Match(text, "^\\d*$").Success)
						{
							return defaultValue;
						}
						num4 = (double)int.Parse(text);
					}
					if (array.Length > 1)
					{
						num3 = (double)int.Parse(array[array.Length - 2]);
					}
					if (array.Length > 2)
					{
						num2 = int.Parse(array[array.Length - 3]);
					}
					if (array.Length > 3)
					{
						num = int.Parse(array[array.Length - 4]);
					}
				}
			}
			catch (FormatException)
			{
				return defaultValue;
			}
			return num4 / frameRate + num3 + (double)(num2 * 60) + (double)(num * 3600);
		}

		// Token: 0x060002DB RID: 731 RVA: 0x0000A234 File Offset: 0x00008434
		public static double GetAnimationClipLength(AnimationClip clip)
		{
			if (clip == null || clip.empty)
			{
				return 0.0;
			}
			double num = (double)clip.length;
			if (clip.frameRate > 0f)
			{
				num = (double)Mathf.Round(clip.length * clip.frameRate) / (double)clip.frameRate;
			}
			return num;
		}

		// Token: 0x060002DC RID: 732 RVA: 0x0000A290 File Offset: 0x00008490
		private static string RemoveChar(string str, Func<char, bool> charToRemoveFunc)
		{
			int length = str.Length;
			char[] array = str.ToCharArray();
			int num = 0;
			for (int i = 0; i < length; i++)
			{
				if (!charToRemoveFunc(array[i]))
				{
					array[num++] = array[i];
				}
			}
			return new string(array, 0, num);
		}

		// Token: 0x040000FA RID: 250
		public static readonly double kTimeEpsilon = 1E-14;

		// Token: 0x040000FB RID: 251
		public static readonly double kFrameRateEpsilon = 1E-06;

		// Token: 0x040000FC RID: 252
		public static readonly double k_MaxTimelineDurationInSeconds = 9000000.0;
	}
}
