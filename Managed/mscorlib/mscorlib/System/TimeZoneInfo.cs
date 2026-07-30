using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using Microsoft.Win32;
using Unity;

namespace System
{
	/// <summary>Represents any time zone in the world.</summary>
	// Token: 0x020001D0 RID: 464
	[TypeForwardedFrom("System.Core, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=b77a5c561934e089")]
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	[Serializable]
	public sealed class TimeZoneInfo : IEquatable<TimeZoneInfo>, ISerializable, IDeserializationCallback
	{
		// Token: 0x06001475 RID: 5237 RVA: 0x000523FE File Offset: 0x000505FE
		internal static bool UtcOffsetOutOfRange(TimeSpan offset)
		{
			return offset.TotalHours < -14.0 || offset.TotalHours > 14.0;
		}

		// Token: 0x06001476 RID: 5238 RVA: 0x00052428 File Offset: 0x00050628
		private static void ValidateTimeZoneInfo(string id, TimeSpan baseUtcOffset, TimeZoneInfo.AdjustmentRule[] adjustmentRules, out bool adjustmentRulesSupportDst)
		{
			if (id == null)
			{
				throw new ArgumentNullException("id");
			}
			if (id.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("The specified ID parameter '{0}' is not supported.", new object[] { id }), "id");
			}
			if (TimeZoneInfo.UtcOffsetOutOfRange(baseUtcOffset))
			{
				throw new ArgumentOutOfRangeException("baseUtcOffset", Environment.GetResourceString("The TimeSpan parameter must be within plus or minus 14.0 hours."));
			}
			if (baseUtcOffset.Ticks % 600000000L != 0L)
			{
				throw new ArgumentException(Environment.GetResourceString("The TimeSpan parameter cannot be specified more precisely than whole minutes."), "baseUtcOffset");
			}
			adjustmentRulesSupportDst = false;
			if (adjustmentRules != null && adjustmentRules.Length != 0)
			{
				adjustmentRulesSupportDst = true;
				TimeZoneInfo.AdjustmentRule adjustmentRule = null;
				for (int i = 0; i < adjustmentRules.Length; i++)
				{
					TimeZoneInfo.AdjustmentRule adjustmentRule2 = adjustmentRule;
					adjustmentRule = adjustmentRules[i];
					if (adjustmentRule == null)
					{
						throw new InvalidTimeZoneException(Environment.GetResourceString("The AdjustmentRule array cannot contain null elements."));
					}
					if (TimeZoneInfo.UtcOffsetOutOfRange(baseUtcOffset + adjustmentRule.DaylightDelta))
					{
						throw new InvalidTimeZoneException(Environment.GetResourceString("The sum of the BaseUtcOffset and DaylightDelta properties must within plus or minus 14.0 hours."));
					}
					if (adjustmentRule2 != null && adjustmentRule.DateStart <= adjustmentRule2.DateEnd)
					{
						throw new InvalidTimeZoneException(Environment.GetResourceString("The elements of the AdjustmentRule array must be in chronological order and must not overlap."));
					}
				}
			}
		}

		/// <summary>Deserializes a string to re-create an original serialized <see cref="T:System.TimeZoneInfo" /> object.</summary>
		/// <returns>The original serialized object.</returns>
		/// <param name="source">The string representation of the serialized <see cref="T:System.TimeZoneInfo" /> object.   </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="source" /> parameter is <see cref="F:System.String.Empty" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="source" /> parameter is a null string.</exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">The source parameter cannot be deserialized back into a <see cref="T:System.TimeZoneInfo" /> object.</exception>
		// Token: 0x06001477 RID: 5239 RVA: 0x00052530 File Offset: 0x00050730
		public static TimeZoneInfo FromSerializedString(string source)
		{
			StringBuilder stringBuilder = new StringBuilder(source);
			string text = TimeZoneInfo.DeserializeString(ref stringBuilder);
			int num = TimeZoneInfo.DeserializeInt(ref stringBuilder);
			string text2 = TimeZoneInfo.DeserializeString(ref stringBuilder);
			string text3 = TimeZoneInfo.DeserializeString(ref stringBuilder);
			string text4 = TimeZoneInfo.DeserializeString(ref stringBuilder);
			List<TimeZoneInfo.AdjustmentRule> list = null;
			while (stringBuilder[0] != ';')
			{
				if (list == null)
				{
					list = new List<TimeZoneInfo.AdjustmentRule>();
				}
				list.Add(TimeZoneInfo.DeserializeAdjustmentRule(ref stringBuilder));
			}
			TimeSpan timeSpan = TimeSpan.FromMinutes((double)num);
			return TimeZoneInfo.CreateCustomTimeZone(text, timeSpan, text2, text3, text4, (list != null) ? list.ToArray() : null);
		}

		/// <summary>Converts the current <see cref="T:System.TimeZoneInfo" /> object to a serialized string.</summary>
		/// <returns>A string that represents the current <see cref="T:System.TimeZoneInfo" /> object.</returns>
		// Token: 0x06001478 RID: 5240 RVA: 0x000525BC File Offset: 0x000507BC
		public string ToSerializedString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			string text = (string.IsNullOrEmpty(this.DaylightName) ? this.StandardName : this.DaylightName);
			stringBuilder.AppendFormat("{0};{1};{2};{3};{4};", new object[]
			{
				TimeZoneInfo.EscapeForSerialization(this.Id),
				(int)this.BaseUtcOffset.TotalMinutes,
				TimeZoneInfo.EscapeForSerialization(this.DisplayName),
				TimeZoneInfo.EscapeForSerialization(this.StandardName),
				TimeZoneInfo.EscapeForSerialization(text)
			});
			if (this.SupportsDaylightSavingTime)
			{
				foreach (TimeZoneInfo.AdjustmentRule adjustmentRule in this.GetAdjustmentRules())
				{
					string text2 = adjustmentRule.DateStart.ToString("MM:dd:yyyy", CultureInfo.InvariantCulture);
					string text3 = adjustmentRule.DateEnd.ToString("MM:dd:yyyy", CultureInfo.InvariantCulture);
					int num = (int)adjustmentRule.DaylightDelta.TotalMinutes;
					string text4 = TimeZoneInfo.SerializeTransitionTime(adjustmentRule.DaylightTransitionStart);
					string text5 = TimeZoneInfo.SerializeTransitionTime(adjustmentRule.DaylightTransitionEnd);
					stringBuilder.AppendFormat("[{0};{1};{2};{3};{4};]", new object[] { text2, text3, num, text4, text5 });
				}
			}
			stringBuilder.Append(";");
			return stringBuilder.ToString();
		}

		// Token: 0x06001479 RID: 5241 RVA: 0x00052718 File Offset: 0x00050918
		private static TimeZoneInfo.AdjustmentRule DeserializeAdjustmentRule(ref StringBuilder input)
		{
			if (input[0] != '[')
			{
				throw new SerializationException();
			}
			input.Remove(0, 1);
			DateTime dateTime = TimeZoneInfo.DeserializeDate(ref input);
			DateTime dateTime2 = TimeZoneInfo.DeserializeDate(ref input);
			double num = (double)TimeZoneInfo.DeserializeInt(ref input);
			TimeZoneInfo.TransitionTime transitionTime = TimeZoneInfo.DeserializeTransitionTime(ref input);
			TimeZoneInfo.TransitionTime transitionTime2 = TimeZoneInfo.DeserializeTransitionTime(ref input);
			input.Remove(0, 1);
			TimeSpan timeSpan = TimeSpan.FromMinutes(num);
			return TimeZoneInfo.AdjustmentRule.CreateAdjustmentRule(dateTime, dateTime2, timeSpan, transitionTime, transitionTime2);
		}

		// Token: 0x0600147A RID: 5242 RVA: 0x0005277C File Offset: 0x0005097C
		private static TimeZoneInfo.TransitionTime DeserializeTransitionTime(ref StringBuilder input)
		{
			if (input[0] != '[' || (input[1] != '0' && input[1] != '1') || input[2] != ';')
			{
				throw new SerializationException();
			}
			int num = (int)input[1];
			input.Remove(0, 3);
			DateTime dateTime = TimeZoneInfo.DeserializeTime(ref input);
			int num2 = TimeZoneInfo.DeserializeInt(ref input);
			if (num == 48)
			{
				int num3 = TimeZoneInfo.DeserializeInt(ref input);
				int num4 = TimeZoneInfo.DeserializeInt(ref input);
				input.Remove(0, 2);
				return TimeZoneInfo.TransitionTime.CreateFloatingDateRule(dateTime, num2, num3, (DayOfWeek)num4);
			}
			int num5 = TimeZoneInfo.DeserializeInt(ref input);
			input.Remove(0, 2);
			return TimeZoneInfo.TransitionTime.CreateFixedDateRule(dateTime, num2, num5);
		}

		// Token: 0x0600147B RID: 5243 RVA: 0x00052820 File Offset: 0x00050A20
		private static string DeserializeString(ref StringBuilder input)
		{
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = false;
			int i;
			for (i = 0; i < input.Length; i++)
			{
				char c = input[i];
				if (flag)
				{
					flag = false;
					stringBuilder.Append(c);
				}
				else if (c == '\\')
				{
					flag = true;
				}
				else
				{
					if (c == ';')
					{
						break;
					}
					stringBuilder.Append(c);
				}
			}
			input.Remove(0, i + 1);
			return stringBuilder.ToString();
		}

		// Token: 0x0600147C RID: 5244 RVA: 0x00052888 File Offset: 0x00050A88
		private static int DeserializeInt(ref StringBuilder input)
		{
			int num = 0;
			while (num++ < input.Length && input[num] != ';')
			{
			}
			int num2;
			if (!int.TryParse(input.ToString(0, num), NumberStyles.Integer, CultureInfo.InvariantCulture, out num2))
			{
				throw new SerializationException();
			}
			input.Remove(0, num + 1);
			return num2;
		}

		// Token: 0x0600147D RID: 5245 RVA: 0x000528E0 File Offset: 0x00050AE0
		private static DateTime DeserializeDate(ref StringBuilder input)
		{
			char[] array = new char[11];
			input.CopyTo(0, array, 0, array.Length);
			DateTime dateTime;
			if (!DateTime.TryParseExact(new string(array), "MM:dd:yyyy;", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime))
			{
				throw new SerializationException();
			}
			input.Remove(0, array.Length);
			return dateTime;
		}

		// Token: 0x0600147E RID: 5246 RVA: 0x00052930 File Offset: 0x00050B30
		private static DateTime DeserializeTime(ref StringBuilder input)
		{
			if (input[8] == ';')
			{
				char[] array = new char[9];
				input.CopyTo(0, array, 0, array.Length);
				DateTime dateTime;
				if (!DateTime.TryParseExact(new string(array), "HH:mm:ss;", CultureInfo.InvariantCulture, DateTimeStyles.NoCurrentDateDefault, out dateTime))
				{
					throw new SerializationException();
				}
				input.Remove(0, array.Length);
				return dateTime;
			}
			else
			{
				if (input[12] != ';')
				{
					throw new SerializationException();
				}
				char[] array2 = new char[13];
				input.CopyTo(0, array2, 0, array2.Length);
				DateTime dateTime2;
				if (!DateTime.TryParseExact(new string(array2), "HH:mm:ss.fff;", CultureInfo.InvariantCulture, DateTimeStyles.NoCurrentDateDefault, out dateTime2))
				{
					throw new SerializationException();
				}
				input.Remove(0, array2.Length);
				return dateTime2;
			}
		}

		// Token: 0x0600147F RID: 5247 RVA: 0x000529E1 File Offset: 0x00050BE1
		private static string EscapeForSerialization(string unescaped)
		{
			return unescaped.Replace("\\", "\\\\").Replace(";", "\\;");
		}

		// Token: 0x06001480 RID: 5248 RVA: 0x00052A04 File Offset: 0x00050C04
		private static string SerializeTransitionTime(TimeZoneInfo.TransitionTime transition)
		{
			string text;
			if (transition.TimeOfDay.Millisecond > 0)
			{
				text = transition.TimeOfDay.ToString("HH:mm:ss.fff");
			}
			else
			{
				text = transition.TimeOfDay.ToString("HH:mm:ss");
			}
			if (transition.IsFixedDateRule)
			{
				return string.Format("[1;{0};{1};{2};]", text, transition.Month, transition.Day);
			}
			return string.Format("[0;{0};{1};{2};{3};]", new object[]
			{
				text,
				transition.Month,
				transition.Week,
				(int)transition.DayOfWeek
			});
		}

		// Token: 0x06001481 RID: 5249 RVA: 0x00052AC0 File Offset: 0x00050CC0
		private static List<TimeZoneInfo.AdjustmentRule> CreateAdjustmentRule(int year, out long[] data, out string[] names, string standardNameCurrentYear, string daylightNameCurrentYear)
		{
			List<TimeZoneInfo.AdjustmentRule> list = new List<TimeZoneInfo.AdjustmentRule>();
			bool flag;
			if (!CurrentSystemTimeZone.GetTimeZoneData(year, out data, out names, out flag))
			{
				return list;
			}
			DateTime dateTime = new DateTime(data[0]);
			DateTime dateTime2 = new DateTime(data[1]);
			TimeSpan timeSpan = new TimeSpan(data[3]);
			if (standardNameCurrentYear != names[0])
			{
				return list;
			}
			if (daylightNameCurrentYear != names[1])
			{
				return list;
			}
			if (dateTime.Equals(dateTime2))
			{
				return list;
			}
			DateTime dateTime3 = new DateTime(year, 1, 1, 0, 0, 0, 0);
			DateTime dateTime4 = new DateTime(year, 12, DateTime.DaysInMonth(year, 12));
			DateTime dateTime5 = new DateTime(year, 12, DateTime.DaysInMonth(year, 12), 23, 59, 59, 999);
			if (!flag)
			{
				TimeZoneInfo.TransitionTime transitionTime = TimeZoneInfo.TransitionTime.CreateFixedDateRule(new DateTime(1, 1, 1).Add(dateTime.TimeOfDay), dateTime.Month, dateTime.Day);
				TimeZoneInfo.TransitionTime transitionTime2 = TimeZoneInfo.TransitionTime.CreateFixedDateRule(new DateTime(1, 1, 1).Add(dateTime2.TimeOfDay), dateTime2.Month, dateTime2.Day);
				TimeZoneInfo.AdjustmentRule adjustmentRule = TimeZoneInfo.AdjustmentRule.CreateAdjustmentRule(dateTime3, dateTime4, timeSpan, transitionTime, transitionTime2);
				list.Add(adjustmentRule);
			}
			else
			{
				TimeZoneInfo.TransitionTime transitionTime3 = TimeZoneInfo.TransitionTime.CreateFixedDateRule(new DateTime(1, 1, 1), 1, 1);
				TimeZoneInfo.TransitionTime transitionTime4 = TimeZoneInfo.TransitionTime.CreateFixedDateRule(new DateTime(1, 1, 1).Add(dateTime.TimeOfDay), dateTime.Month, dateTime.Day);
				TimeZoneInfo.AdjustmentRule adjustmentRule2 = TimeZoneInfo.AdjustmentRule.CreateAdjustmentRule(new DateTime(year, 1, 1), new DateTime(dateTime.Year, dateTime.Month, dateTime.Day), timeSpan, transitionTime3, transitionTime4);
				list.Add(adjustmentRule2);
				TimeZoneInfo.TransitionTime transitionTime5 = TimeZoneInfo.TransitionTime.CreateFixedDateRule(new DateTime(1, 1, 1).Add(dateTime2.TimeOfDay), dateTime2.Month, dateTime2.Day);
				TimeZoneInfo.TransitionTime transitionTime6 = TimeZoneInfo.TransitionTime.CreateFixedDateRule(new DateTime(1, 1, 1).Add(dateTime5.TimeOfDay), dateTime5.Month, dateTime5.Day);
				TimeZoneInfo.AdjustmentRule adjustmentRule3 = TimeZoneInfo.AdjustmentRule.CreateAdjustmentRule(new DateTime(dateTime.Year, dateTime.Month, dateTime.Day).AddDays(1.0), dateTime4, timeSpan, transitionTime5, transitionTime6);
				list.Add(adjustmentRule3);
			}
			return list;
		}

		// Token: 0x06001482 RID: 5250 RVA: 0x00052CF8 File Offset: 0x00050EF8
		private static TimeZoneInfo CreateLocalUnity()
		{
			int year = DateTime.UtcNow.Year;
			long[] array;
			string[] array2;
			bool flag;
			if (!CurrentSystemTimeZone.GetTimeZoneData(year, out array, out array2, out flag))
			{
				throw new NotSupportedException("Can't get timezone name.");
			}
			TimeSpan timeSpan = TimeSpan.FromTicks(array[2]);
			string text = "(GMT" + ((timeSpan >= TimeSpan.Zero) ? '+' : '-').ToString() + timeSpan.ToString("hh\\:mm") + ") Local Time";
			string text2 = array2[0];
			string text3 = array2[1];
			List<TimeZoneInfo.AdjustmentRule> list = new List<TimeZoneInfo.AdjustmentRule>();
			bool flag2 = array[3] == 0L;
			if (!flag2)
			{
				int num = 1971;
				int num2 = 2037;
				for (int i = year; i <= num2; i++)
				{
					List<TimeZoneInfo.AdjustmentRule> list2 = TimeZoneInfo.CreateAdjustmentRule(i, out array, out array2, text2, text3);
					if (list2.Count <= 0)
					{
						break;
					}
					list.AddRange(list2);
				}
				for (int j = year - 1; j >= num; j--)
				{
					List<TimeZoneInfo.AdjustmentRule> list3 = TimeZoneInfo.CreateAdjustmentRule(j, out array, out array2, text2, text3);
					if (list3.Count <= 0)
					{
						break;
					}
					list.AddRange(list3);
				}
				list.Sort((TimeZoneInfo.AdjustmentRule rule1, TimeZoneInfo.AdjustmentRule rule2) => rule1.DateStart.CompareTo(rule2.DateStart));
			}
			return TimeZoneInfo.CreateCustomTimeZone("Local", timeSpan, text, text2, text3, list.ToArray(), flag2);
		}

		// Token: 0x06001483 RID: 5251
		[DllImport("api-ms-win-core-timezone-l1-1-0.dll")]
		internal static extern uint EnumDynamicTimeZoneInformation(uint dwIndex, out TimeZoneInfo.DYNAMIC_TIME_ZONE_INFORMATION lpTimeZoneInformation);

		// Token: 0x06001484 RID: 5252
		[DllImport("api-ms-win-core-timezone-l1-1-0.dll")]
		internal static extern uint GetDynamicTimeZoneInformation(out TimeZoneInfo.DYNAMIC_TIME_ZONE_INFORMATION pTimeZoneInformation);

		// Token: 0x06001485 RID: 5253
		[DllImport("kernel32.dll", EntryPoint = "GetDynamicTimeZoneInformation")]
		internal static extern uint GetDynamicTimeZoneInformationWin32(out TimeZoneInfo.DYNAMIC_TIME_ZONE_INFORMATION pTimeZoneInformation);

		// Token: 0x06001486 RID: 5254
		[DllImport("api-ms-win-core-timezone-l1-1-0.dll")]
		internal static extern uint GetDynamicTimeZoneInformationEffectiveYears(ref TimeZoneInfo.DYNAMIC_TIME_ZONE_INFORMATION lpTimeZoneInformation, out uint FirstYear, out uint LastYear);

		// Token: 0x06001487 RID: 5255
		[DllImport("api-ms-win-core-timezone-l1-1-0.dll")]
		internal static extern bool GetTimeZoneInformationForYear(ushort wYear, ref TimeZoneInfo.DYNAMIC_TIME_ZONE_INFORMATION pdtzi, out TimeZoneInfo.TIME_ZONE_INFORMATION ptzi);

		// Token: 0x06001488 RID: 5256 RVA: 0x00052E50 File Offset: 0x00051050
		internal static TimeZoneInfo.AdjustmentRule CreateAdjustmentRuleFromTimeZoneInformation(ref TimeZoneInfo.DYNAMIC_TIME_ZONE_INFORMATION timeZoneInformation, DateTime startDate, DateTime endDate, int defaultBaseUtcOffset)
		{
			if (timeZoneInformation.TZI.StandardDate.wMonth <= 0)
			{
				if (timeZoneInformation.TZI.Bias == defaultBaseUtcOffset)
				{
					return null;
				}
				return TimeZoneInfo.AdjustmentRule.CreateAdjustmentRule(startDate, endDate, TimeSpan.Zero, TimeZoneInfo.TransitionTime.CreateFixedDateRule(DateTime.MinValue, 1, 1), TimeZoneInfo.TransitionTime.CreateFixedDateRule(DateTime.MinValue.AddMilliseconds(1.0), 1, 1), new TimeSpan(0, defaultBaseUtcOffset - timeZoneInformation.TZI.Bias, 0));
			}
			else
			{
				TimeZoneInfo.TransitionTime transitionTime;
				if (!TimeZoneInfo.TransitionTimeFromTimeZoneInformation(timeZoneInformation, out transitionTime, true))
				{
					return null;
				}
				TimeZoneInfo.TransitionTime transitionTime2;
				if (!TimeZoneInfo.TransitionTimeFromTimeZoneInformation(timeZoneInformation, out transitionTime2, false))
				{
					return null;
				}
				if (transitionTime.Equals(transitionTime2))
				{
					return null;
				}
				return TimeZoneInfo.AdjustmentRule.CreateAdjustmentRule(startDate, endDate, new TimeSpan(0, -timeZoneInformation.TZI.DaylightBias, 0), transitionTime, transitionTime2, new TimeSpan(0, defaultBaseUtcOffset - timeZoneInformation.TZI.Bias, 0));
			}
		}

		// Token: 0x06001489 RID: 5257 RVA: 0x00052F2C File Offset: 0x0005112C
		private static bool TransitionTimeFromTimeZoneInformation(TimeZoneInfo.DYNAMIC_TIME_ZONE_INFORMATION timeZoneInformation, out TimeZoneInfo.TransitionTime transitionTime, bool readStartDate)
		{
			if (timeZoneInformation.TZI.StandardDate.wMonth <= 0)
			{
				transitionTime = default(TimeZoneInfo.TransitionTime);
				return false;
			}
			if (readStartDate)
			{
				if (timeZoneInformation.TZI.DaylightDate.wYear == 0)
				{
					transitionTime = TimeZoneInfo.TransitionTime.CreateFloatingDateRule(new DateTime(1, 1, 1, (int)timeZoneInformation.TZI.DaylightDate.wHour, (int)timeZoneInformation.TZI.DaylightDate.wMinute, (int)timeZoneInformation.TZI.DaylightDate.wSecond, (int)timeZoneInformation.TZI.DaylightDate.wMilliseconds), (int)timeZoneInformation.TZI.DaylightDate.wMonth, (int)timeZoneInformation.TZI.DaylightDate.wDay, (DayOfWeek)timeZoneInformation.TZI.DaylightDate.wDayOfWeek);
				}
				else
				{
					transitionTime = TimeZoneInfo.TransitionTime.CreateFixedDateRule(new DateTime(1, 1, 1, (int)timeZoneInformation.TZI.DaylightDate.wHour, (int)timeZoneInformation.TZI.DaylightDate.wMinute, (int)timeZoneInformation.TZI.DaylightDate.wSecond, (int)timeZoneInformation.TZI.DaylightDate.wMilliseconds), (int)timeZoneInformation.TZI.DaylightDate.wMonth, (int)timeZoneInformation.TZI.DaylightDate.wDay);
				}
			}
			else if (timeZoneInformation.TZI.StandardDate.wYear == 0)
			{
				transitionTime = TimeZoneInfo.TransitionTime.CreateFloatingDateRule(new DateTime(1, 1, 1, (int)timeZoneInformation.TZI.StandardDate.wHour, (int)timeZoneInformation.TZI.StandardDate.wMinute, (int)timeZoneInformation.TZI.StandardDate.wSecond, (int)timeZoneInformation.TZI.StandardDate.wMilliseconds), (int)timeZoneInformation.TZI.StandardDate.wMonth, (int)timeZoneInformation.TZI.StandardDate.wDay, (DayOfWeek)timeZoneInformation.TZI.StandardDate.wDayOfWeek);
			}
			else
			{
				transitionTime = TimeZoneInfo.TransitionTime.CreateFixedDateRule(new DateTime(1, 1, 1, (int)timeZoneInformation.TZI.StandardDate.wHour, (int)timeZoneInformation.TZI.StandardDate.wMinute, (int)timeZoneInformation.TZI.StandardDate.wSecond, (int)timeZoneInformation.TZI.StandardDate.wMilliseconds), (int)timeZoneInformation.TZI.StandardDate.wMonth, (int)timeZoneInformation.TZI.StandardDate.wDay);
			}
			return true;
		}

		// Token: 0x0600148A RID: 5258 RVA: 0x00053180 File Offset: 0x00051380
		internal static TimeZoneInfo TryCreateTimeZone(TimeZoneInfo.DYNAMIC_TIME_ZONE_INFORMATION timeZoneInformation)
		{
			uint num = 0U;
			uint num2 = 0U;
			TimeZoneInfo.AdjustmentRule[] array = null;
			int bias = timeZoneInformation.TZI.Bias;
			if (string.IsNullOrEmpty(timeZoneInformation.TimeZoneKeyName))
			{
				return null;
			}
			try
			{
				if (TimeZoneInfo.GetDynamicTimeZoneInformationEffectiveYears(ref timeZoneInformation, out num, out num2) != 0U)
				{
					num2 = (num = 0U);
				}
			}
			catch
			{
				num2 = (num = 0U);
			}
			if (num == num2)
			{
				TimeZoneInfo.AdjustmentRule adjustmentRule = TimeZoneInfo.CreateAdjustmentRuleFromTimeZoneInformation(ref timeZoneInformation, DateTime.MinValue.Date, DateTime.MaxValue.Date, bias);
				if (adjustmentRule != null)
				{
					array = new TimeZoneInfo.AdjustmentRule[] { adjustmentRule };
				}
			}
			else
			{
				TimeZoneInfo.DYNAMIC_TIME_ZONE_INFORMATION dynamic_TIME_ZONE_INFORMATION = default(TimeZoneInfo.DYNAMIC_TIME_ZONE_INFORMATION);
				List<TimeZoneInfo.AdjustmentRule> list = new List<TimeZoneInfo.AdjustmentRule>();
				if (!TimeZoneInfo.GetTimeZoneInformationForYear((ushort)num, ref timeZoneInformation, out dynamic_TIME_ZONE_INFORMATION.TZI))
				{
					return null;
				}
				TimeZoneInfo.AdjustmentRule adjustmentRule = TimeZoneInfo.CreateAdjustmentRuleFromTimeZoneInformation(ref dynamic_TIME_ZONE_INFORMATION, DateTime.MinValue.Date, new DateTime((int)num, 12, 31), bias);
				if (adjustmentRule != null)
				{
					list.Add(adjustmentRule);
				}
				for (uint num3 = num + 1U; num3 < num2; num3 += 1U)
				{
					if (!TimeZoneInfo.GetTimeZoneInformationForYear((ushort)num3, ref timeZoneInformation, out dynamic_TIME_ZONE_INFORMATION.TZI))
					{
						return null;
					}
					adjustmentRule = TimeZoneInfo.CreateAdjustmentRuleFromTimeZoneInformation(ref dynamic_TIME_ZONE_INFORMATION, new DateTime((int)num3, 1, 1), new DateTime((int)num3, 12, 31), bias);
					if (adjustmentRule != null)
					{
						list.Add(adjustmentRule);
					}
				}
				if (!TimeZoneInfo.GetTimeZoneInformationForYear((ushort)num2, ref timeZoneInformation, out dynamic_TIME_ZONE_INFORMATION.TZI))
				{
					return null;
				}
				adjustmentRule = TimeZoneInfo.CreateAdjustmentRuleFromTimeZoneInformation(ref dynamic_TIME_ZONE_INFORMATION, new DateTime((int)num2, 1, 1), DateTime.MaxValue.Date, bias);
				if (adjustmentRule != null)
				{
					list.Add(adjustmentRule);
				}
				if (list.Count > 0)
				{
					array = list.ToArray();
				}
			}
			return new TimeZoneInfo(timeZoneInformation.TimeZoneKeyName, new TimeSpan(0, -timeZoneInformation.TZI.Bias, 0), timeZoneInformation.TZI.StandardName, timeZoneInformation.TZI.StandardName, timeZoneInformation.TZI.DaylightName, array, false);
		}

		// Token: 0x0600148B RID: 5259 RVA: 0x0005334C File Offset: 0x0005154C
		internal static TimeZoneInfo GetLocalTimeZoneInfoWinRTFallback()
		{
			TimeZoneInfo timeZoneInfo;
			try
			{
				TimeZoneInfo.DYNAMIC_TIME_ZONE_INFORMATION dynamic_TIME_ZONE_INFORMATION;
				if (TimeZoneInfo.GetDynamicTimeZoneInformation(out dynamic_TIME_ZONE_INFORMATION) == 4294967295U)
				{
					timeZoneInfo = TimeZoneInfo.Utc;
				}
				else
				{
					TimeZoneInfo timeZoneInfo2 = TimeZoneInfo.TryCreateTimeZone(dynamic_TIME_ZONE_INFORMATION);
					timeZoneInfo = ((timeZoneInfo2 != null) ? timeZoneInfo2 : TimeZoneInfo.Utc);
				}
			}
			catch
			{
				timeZoneInfo = TimeZoneInfo.Utc;
			}
			return timeZoneInfo;
		}

		// Token: 0x0600148C RID: 5260 RVA: 0x0005339C File Offset: 0x0005159C
		internal static string GetLocalTimeZoneKeyNameWin32Fallback()
		{
			string text;
			try
			{
				TimeZoneInfo.DYNAMIC_TIME_ZONE_INFORMATION dynamic_TIME_ZONE_INFORMATION;
				if (TimeZoneInfo.GetDynamicTimeZoneInformationWin32(out dynamic_TIME_ZONE_INFORMATION) == 4294967295U)
				{
					text = null;
				}
				else if (!string.IsNullOrEmpty(dynamic_TIME_ZONE_INFORMATION.TimeZoneKeyName))
				{
					text = dynamic_TIME_ZONE_INFORMATION.TimeZoneKeyName;
				}
				else if (!string.IsNullOrEmpty(dynamic_TIME_ZONE_INFORMATION.TZI.StandardName))
				{
					text = dynamic_TIME_ZONE_INFORMATION.TZI.StandardName;
				}
				else
				{
					text = null;
				}
			}
			catch
			{
				text = null;
			}
			return text;
		}

		// Token: 0x0600148D RID: 5261 RVA: 0x00053408 File Offset: 0x00051608
		internal static TimeZoneInfo FindSystemTimeZoneByIdWinRTFallback(string id)
		{
			foreach (TimeZoneInfo timeZoneInfo in TimeZoneInfo.GetSystemTimeZones())
			{
				if (string.Compare(id, timeZoneInfo.Id, StringComparison.Ordinal) == 0)
				{
					return timeZoneInfo;
				}
			}
			throw new TimeZoneNotFoundException();
		}

		// Token: 0x0600148E RID: 5262 RVA: 0x00053468 File Offset: 0x00051668
		internal static List<TimeZoneInfo> GetSystemTimeZonesWinRTFallback()
		{
			List<TimeZoneInfo> list = new List<TimeZoneInfo>();
			try
			{
				uint num = 0U;
				TimeZoneInfo.DYNAMIC_TIME_ZONE_INFORMATION dynamic_TIME_ZONE_INFORMATION;
				while (TimeZoneInfo.EnumDynamicTimeZoneInformation(num++, out dynamic_TIME_ZONE_INFORMATION) != 259U)
				{
					TimeZoneInfo timeZoneInfo = TimeZoneInfo.TryCreateTimeZone(dynamic_TIME_ZONE_INFORMATION);
					if (timeZoneInfo != null)
					{
						list.Add(timeZoneInfo);
					}
				}
			}
			catch
			{
			}
			if (list.Count == 0)
			{
				list.Add(TimeZoneInfo.Local);
			}
			return list;
		}

		/// <summary>Gets the time difference between the current time zone's standard time and Coordinated Universal Time (UTC).</summary>
		/// <returns>An object that indicates the time difference between the current time zone's standard time and Coordinated Universal Time (UTC).</returns>
		// Token: 0x1700024F RID: 591
		// (get) Token: 0x0600148F RID: 5263 RVA: 0x000534CC File Offset: 0x000516CC
		public TimeSpan BaseUtcOffset
		{
			get
			{
				return this.baseUtcOffset;
			}
		}

		/// <summary>Gets the display name for the current time zone's daylight saving time.</summary>
		/// <returns>The display name for the time zone's daylight saving time.</returns>
		// Token: 0x17000250 RID: 592
		// (get) Token: 0x06001490 RID: 5264 RVA: 0x000534D4 File Offset: 0x000516D4
		public string DaylightName
		{
			get
			{
				if (!this.supportsDaylightSavingTime)
				{
					return string.Empty;
				}
				return this.daylightDisplayName;
			}
		}

		/// <summary>Gets the general display name that represents the time zone.</summary>
		/// <returns>The time zone's general display name.</returns>
		// Token: 0x17000251 RID: 593
		// (get) Token: 0x06001491 RID: 5265 RVA: 0x000534EA File Offset: 0x000516EA
		public string DisplayName
		{
			get
			{
				return this.displayName;
			}
		}

		/// <summary>Gets the time zone identifier.</summary>
		/// <returns>The time zone identifier.</returns>
		// Token: 0x17000252 RID: 594
		// (get) Token: 0x06001492 RID: 5266 RVA: 0x000534F2 File Offset: 0x000516F2
		public string Id
		{
			get
			{
				return this.id;
			}
		}

		/// <summary>Gets a <see cref="T:System.TimeZoneInfo" /> object that represents the local time zone.</summary>
		/// <returns>An object that represents the local time zone.</returns>
		// Token: 0x17000253 RID: 595
		// (get) Token: 0x06001493 RID: 5267 RVA: 0x000534FC File Offset: 0x000516FC
		public static TimeZoneInfo Local
		{
			get
			{
				TimeZoneInfo timeZoneInfo = TimeZoneInfo.local;
				if (timeZoneInfo == null)
				{
					timeZoneInfo = TimeZoneInfo.CreateLocal();
					if (timeZoneInfo == null)
					{
						throw new TimeZoneNotFoundException();
					}
					if (Interlocked.CompareExchange<TimeZoneInfo>(ref TimeZoneInfo.local, timeZoneInfo, null) != null)
					{
						timeZoneInfo = TimeZoneInfo.local;
					}
				}
				return timeZoneInfo;
			}
		}

		// Token: 0x06001494 RID: 5268
		[DllImport("libc")]
		private static extern int readlink(string path, byte[] buffer, int buflen);

		// Token: 0x06001495 RID: 5269 RVA: 0x00053538 File Offset: 0x00051738
		private static string readlink(string path)
		{
			if (TimeZoneInfo.readlinkNotFound)
			{
				return null;
			}
			byte[] array = new byte[512];
			int num;
			try
			{
				num = TimeZoneInfo.readlink(path, array, array.Length);
			}
			catch (DllNotFoundException)
			{
				TimeZoneInfo.readlinkNotFound = true;
				return null;
			}
			catch (EntryPointNotFoundException)
			{
				TimeZoneInfo.readlinkNotFound = true;
				return null;
			}
			if (num == -1)
			{
				return null;
			}
			char[] array2 = new char[512];
			int chars = Encoding.Default.GetChars(array, 0, num, array2, 0);
			return new string(array2, 0, chars);
		}

		// Token: 0x06001496 RID: 5270 RVA: 0x000535C8 File Offset: 0x000517C8
		private static bool TryGetNameFromPath(string path, out string name)
		{
			name = null;
			if (!File.Exists(path))
			{
				return false;
			}
			string text = TimeZoneInfo.readlink(path);
			if (text != null)
			{
				if (Path.IsPathRooted(text))
				{
					path = text;
				}
				else
				{
					path = Path.Combine(Path.GetDirectoryName(path), text);
				}
			}
			path = Path.GetFullPath(path);
			if (string.IsNullOrEmpty(TimeZoneInfo.TimeZoneDirectory))
			{
				return false;
			}
			string text2 = TimeZoneInfo.TimeZoneDirectory;
			if (text2[text2.Length - 1] != Path.DirectorySeparatorChar)
			{
				text2 += Path.DirectorySeparatorChar.ToString();
			}
			if (!path.StartsWith(text2, StringComparison.InvariantCulture))
			{
				return false;
			}
			name = path.Substring(text2.Length);
			if (name == "localtime")
			{
				name = "Local";
			}
			return true;
		}

		// Token: 0x06001497 RID: 5271 RVA: 0x0005367C File Offset: 0x0005187C
		private static TimeZoneInfo CreateLocal()
		{
			if (TimeZoneInfo.IsWindows && TimeZoneInfo.LocalZoneKey != null)
			{
				string text = (string)TimeZoneInfo.LocalZoneKey.GetValue("TimeZoneKeyName");
				if (text == null)
				{
					text = (string)TimeZoneInfo.LocalZoneKey.GetValue("StandardName");
				}
				text = TimeZoneInfo.TrimSpecial(text);
				if (string.IsNullOrEmpty(text))
				{
					text = TimeZoneInfo.GetLocalTimeZoneKeyNameWin32Fallback();
				}
				if (text == null)
				{
					goto IL_0078;
				}
				try
				{
					return TimeZoneInfo.FindSystemTimeZoneById(text);
				}
				catch (TimeZoneNotFoundException)
				{
					return TimeZoneInfo.GetLocalTimeZoneInfoWinRTFallback();
				}
			}
			if (TimeZoneInfo.IsWindows)
			{
				return TimeZoneInfo.GetLocalTimeZoneInfoWinRTFallback();
			}
			IL_0078:
			TimeZoneInfo timeZoneInfo = null;
			try
			{
				timeZoneInfo = TimeZoneInfo.CreateLocalUnity();
			}
			catch
			{
				timeZoneInfo = null;
			}
			if (timeZoneInfo == null)
			{
				timeZoneInfo = TimeZoneInfo.Utc;
			}
			string environmentVariable = Environment.GetEnvironmentVariable("TZ");
			if (environmentVariable != null)
			{
				if (environmentVariable == string.Empty)
				{
					return timeZoneInfo;
				}
				try
				{
					return TimeZoneInfo.FindSystemTimeZoneByFileName(environmentVariable, Path.Combine(TimeZoneInfo.TimeZoneDirectory, environmentVariable));
				}
				catch
				{
					return timeZoneInfo;
				}
			}
			foreach (string text2 in new string[]
			{
				"/etc/localtime",
				Path.Combine(TimeZoneInfo.TimeZoneDirectory, "localtime")
			})
			{
				try
				{
					string text3 = null;
					if (!TimeZoneInfo.TryGetNameFromPath(text2, out text3))
					{
						text3 = "Local";
					}
					if (File.Exists(text2))
					{
						return TimeZoneInfo.FindSystemTimeZoneByFileName(text3, text2);
					}
				}
				catch (TimeZoneNotFoundException)
				{
				}
			}
			return timeZoneInfo;
		}

		// Token: 0x06001498 RID: 5272 RVA: 0x000537F4 File Offset: 0x000519F4
		private static TimeZoneInfo FindSystemTimeZoneByIdCore(string id)
		{
			string text = Path.Combine(TimeZoneInfo.TimeZoneDirectory, id);
			return TimeZoneInfo.FindSystemTimeZoneByFileName(id, text);
		}

		// Token: 0x06001499 RID: 5273 RVA: 0x00053814 File Offset: 0x00051A14
		private static void GetSystemTimeZonesCore(List<TimeZoneInfo> systemTimeZones)
		{
			if (TimeZoneInfo.TimeZoneKey != null)
			{
				string[] array = TimeZoneInfo.TimeZoneKey.GetSubKeyNames();
				int i = 0;
				while (i < array.Length)
				{
					string text = array[i];
					using (RegistryKey registryKey = TimeZoneInfo.TimeZoneKey.OpenSubKey(text))
					{
						if (registryKey == null || registryKey.GetValue("TZI") == null)
						{
							goto IL_0050;
						}
					}
					goto IL_0044;
					IL_0050:
					i++;
					continue;
					IL_0044:
					systemTimeZones.Add(TimeZoneInfo.FindSystemTimeZoneById(text));
					goto IL_0050;
				}
				return;
			}
			if (TimeZoneInfo.IsWindows)
			{
				systemTimeZones.AddRange(TimeZoneInfo.GetSystemTimeZonesWinRTFallback());
				return;
			}
			foreach (string text2 in new string[]
			{
				"Africa", "America", "Antarctica", "Arctic", "Asia", "Atlantic", "Australia", "Brazil", "Canada", "Chile",
				"Europe", "Indian", "Mexico", "Mideast", "Pacific", "US"
			})
			{
				try
				{
					foreach (string text3 in Directory.GetFiles(Path.Combine(TimeZoneInfo.TimeZoneDirectory, text2)))
					{
						try
						{
							string text4 = string.Format("{0}/{1}", text2, Path.GetFileName(text3));
							systemTimeZones.Add(TimeZoneInfo.FindSystemTimeZoneById(text4));
						}
						catch (ArgumentNullException)
						{
						}
						catch (TimeZoneNotFoundException)
						{
						}
						catch (InvalidTimeZoneException)
						{
						}
						catch (Exception)
						{
							throw;
						}
					}
				}
				catch
				{
				}
			}
		}

		/// <summary>Gets the display name for the time zone's standard time.</summary>
		/// <returns>The display name of the time zone's standard time.</returns>
		// Token: 0x17000254 RID: 596
		// (get) Token: 0x0600149A RID: 5274 RVA: 0x000539E0 File Offset: 0x00051BE0
		public string StandardName
		{
			get
			{
				return this.standardDisplayName;
			}
		}

		/// <summary>Gets a value indicating whether the time zone has any daylight saving time rules.</summary>
		/// <returns>true if the time zone supports daylight saving time; otherwise, false.</returns>
		// Token: 0x17000255 RID: 597
		// (get) Token: 0x0600149B RID: 5275 RVA: 0x000539E8 File Offset: 0x00051BE8
		public bool SupportsDaylightSavingTime
		{
			get
			{
				return this.supportsDaylightSavingTime;
			}
		}

		/// <summary>Gets a <see cref="T:System.TimeZoneInfo" /> object that represents the Coordinated Universal Time (UTC) zone.</summary>
		/// <returns>An object that represents the Coordinated Universal Time (UTC) zone.</returns>
		// Token: 0x17000256 RID: 598
		// (get) Token: 0x0600149C RID: 5276 RVA: 0x000539F0 File Offset: 0x00051BF0
		public static TimeZoneInfo Utc
		{
			get
			{
				if (TimeZoneInfo.utc == null)
				{
					TimeZoneInfo.utc = TimeZoneInfo.CreateCustomTimeZone("UTC", new TimeSpan(0L), "UTC", "UTC");
				}
				return TimeZoneInfo.utc;
			}
		}

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x0600149D RID: 5277 RVA: 0x00053A1E File Offset: 0x00051C1E
		// (set) Token: 0x0600149E RID: 5278 RVA: 0x00053A36 File Offset: 0x00051C36
		private static string TimeZoneDirectory
		{
			get
			{
				if (TimeZoneInfo.timeZoneDirectory == null)
				{
					TimeZoneInfo.timeZoneDirectory = "/usr/share/zoneinfo";
				}
				return TimeZoneInfo.timeZoneDirectory;
			}
			set
			{
				TimeZoneInfo.ClearCachedData();
				TimeZoneInfo.timeZoneDirectory = value;
			}
		}

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x0600149F RID: 5279 RVA: 0x00053A44 File Offset: 0x00051C44
		private static bool IsWindows
		{
			get
			{
				int platform = (int)Environment.OSVersion.Platform;
				return platform != 4 && platform != 6 && platform != 128;
			}
		}

		// Token: 0x060014A0 RID: 5280 RVA: 0x00053A74 File Offset: 0x00051C74
		private static string TrimSpecial(string str)
		{
			if (str == null)
			{
				return str;
			}
			int num = 0;
			while (num < str.Length && !char.IsLetterOrDigit(str[num]))
			{
				num++;
			}
			int num2 = str.Length - 1;
			while (num2 > num && !char.IsLetterOrDigit(str[num2]) && str[num2] != ')')
			{
				num2--;
			}
			return str.Substring(num, num2 - num + 1);
		}

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x060014A1 RID: 5281 RVA: 0x00053AE0 File Offset: 0x00051CE0
		private static RegistryKey TimeZoneKey
		{
			get
			{
				if (TimeZoneInfo.timeZoneKey != null)
				{
					return TimeZoneInfo.timeZoneKey;
				}
				if (!TimeZoneInfo.IsWindows)
				{
					return null;
				}
				RegistryKey registryKey;
				try
				{
					registryKey = (TimeZoneInfo.timeZoneKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Time Zones", false));
				}
				catch
				{
					registryKey = null;
				}
				return registryKey;
			}
		}

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x060014A2 RID: 5282 RVA: 0x00053B34 File Offset: 0x00051D34
		private static RegistryKey LocalZoneKey
		{
			get
			{
				if (TimeZoneInfo.localZoneKey != null)
				{
					return TimeZoneInfo.localZoneKey;
				}
				if (!TimeZoneInfo.IsWindows)
				{
					return null;
				}
				RegistryKey registryKey;
				try
				{
					registryKey = (TimeZoneInfo.localZoneKey = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Control\\TimeZoneInformation", false));
				}
				catch
				{
					registryKey = null;
				}
				return registryKey;
			}
		}

		// Token: 0x060014A3 RID: 5283 RVA: 0x00053B88 File Offset: 0x00051D88
		private static bool TryAddTicks(DateTime date, long ticks, out DateTime result, DateTimeKind kind = DateTimeKind.Unspecified)
		{
			long num = date.Ticks + ticks;
			if (num < DateTime.MinValue.Ticks)
			{
				result = DateTime.SpecifyKind(DateTime.MinValue, kind);
				return false;
			}
			if (num > DateTime.MaxValue.Ticks)
			{
				result = DateTime.SpecifyKind(DateTime.MaxValue, kind);
				return false;
			}
			result = new DateTime(num, kind);
			return true;
		}

		/// <summary>Clears cached time zone data.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060014A4 RID: 5284 RVA: 0x00053BF3 File Offset: 0x00051DF3
		public static void ClearCachedData()
		{
			TimeZoneInfo.local = null;
			TimeZoneInfo.utc = null;
			TimeZoneInfo.systemTimeZones = null;
		}

		/// <summary>Converts a time to the time in a particular time zone.</summary>
		/// <returns>The date and time in the destination time zone.</returns>
		/// <param name="dateTime">The date and time to convert.   </param>
		/// <param name="destinationTimeZone">The time zone to convert <paramref name="dateTime" /> to.</param>
		/// <exception cref="T:System.ArgumentException">The value of the <paramref name="dateTime" /> parameter represents an invalid time.</exception>
		/// <exception cref="T:System.ArgumentNullException">The value of the <paramref name="destinationTimeZone" /> parameter is null.</exception>
		// Token: 0x060014A5 RID: 5285 RVA: 0x00053C07 File Offset: 0x00051E07
		public static DateTime ConvertTime(DateTime dateTime, TimeZoneInfo destinationTimeZone)
		{
			return TimeZoneInfo.ConvertTime(dateTime, (dateTime.Kind == DateTimeKind.Utc) ? TimeZoneInfo.Utc : TimeZoneInfo.Local, destinationTimeZone);
		}

		/// <summary>Converts a time from one time zone to another.</summary>
		/// <returns>The date and time in the destination time zone that corresponds to the <paramref name="dateTime" /> parameter in the source time zone.</returns>
		/// <param name="dateTime">The date and time to convert.</param>
		/// <param name="sourceTimeZone">The time zone of <paramref name="dateTime" />.</param>
		/// <param name="destinationTimeZone">The time zone to convert <paramref name="dateTime" /> to.</param>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.DateTime.Kind" /> property of the <paramref name="dateTime" /> parameter is <see cref="F:System.DateTimeKind.Local" />, but the <paramref name="sourceTimeZone" /> parameter does not equal <see cref="F:System.DateTimeKind.Local" />. For more information, see the Remarks section. -or-The <see cref="P:System.DateTime.Kind" /> property of the <paramref name="dateTime" /> parameter is <see cref="F:System.DateTimeKind.Utc" />, but the <paramref name="sourceTimeZone" /> parameter does not equal <see cref="P:System.TimeZoneInfo.Utc" />.-or-The <paramref name="dateTime" /> parameter is an invalid time (that is, it represents a time that does not exist because of a time zone's adjustment rules).</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="sourceTimeZone" /> parameter is null.-or-The <paramref name="destinationTimeZone" /> parameter is null.</exception>
		// Token: 0x060014A6 RID: 5286 RVA: 0x00053C28 File Offset: 0x00051E28
		public static DateTime ConvertTime(DateTime dateTime, TimeZoneInfo sourceTimeZone, TimeZoneInfo destinationTimeZone)
		{
			if (sourceTimeZone == null)
			{
				throw new ArgumentNullException("sourceTimeZone");
			}
			if (destinationTimeZone == null)
			{
				throw new ArgumentNullException("destinationTimeZone");
			}
			if (dateTime.Kind == DateTimeKind.Local && sourceTimeZone != TimeZoneInfo.Local)
			{
				throw new ArgumentException("Kind property of dateTime is Local but the sourceTimeZone does not equal TimeZoneInfo.Local");
			}
			if (dateTime.Kind == DateTimeKind.Utc && sourceTimeZone != TimeZoneInfo.Utc)
			{
				throw new ArgumentException("Kind property of dateTime is Utc but the sourceTimeZone does not equal TimeZoneInfo.Utc");
			}
			if (sourceTimeZone.IsInvalidTime(dateTime))
			{
				throw new ArgumentException("dateTime parameter is an invalid time");
			}
			if (dateTime.Kind == DateTimeKind.Local && sourceTimeZone == TimeZoneInfo.Local && destinationTimeZone == TimeZoneInfo.Local)
			{
				return dateTime;
			}
			DateTime dateTime2 = TimeZoneInfo.ConvertTimeToUtc(dateTime, sourceTimeZone);
			if (destinationTimeZone != TimeZoneInfo.Utc)
			{
				dateTime2 = TimeZoneInfo.ConvertTimeFromUtc(dateTime2, destinationTimeZone);
				if (dateTime.Kind == DateTimeKind.Unspecified)
				{
					return DateTime.SpecifyKind(dateTime2, DateTimeKind.Unspecified);
				}
			}
			return dateTime2;
		}

		/// <summary>Converts a time to the time in a particular time zone.</summary>
		/// <returns>The date and time in the destination time zone.</returns>
		/// <param name="dateTimeOffset">The date and time to convert.   </param>
		/// <param name="destinationTimeZone">The time zone to convert <paramref name="dateTime" /> to.</param>
		/// <exception cref="T:System.ArgumentNullException">The value of the <paramref name="destinationTimeZone" /> parameter is null.</exception>
		// Token: 0x060014A7 RID: 5287 RVA: 0x00053CE8 File Offset: 0x00051EE8
		public static DateTimeOffset ConvertTime(DateTimeOffset dateTimeOffset, TimeZoneInfo destinationTimeZone)
		{
			if (destinationTimeZone == null)
			{
				throw new ArgumentNullException("destinationTimeZone");
			}
			DateTime utcDateTime = dateTimeOffset.UtcDateTime;
			bool flag;
			TimeSpan utcOffset = destinationTimeZone.GetUtcOffset(utcDateTime, out flag);
			return new DateTimeOffset(DateTime.SpecifyKind(utcDateTime, DateTimeKind.Unspecified) + utcOffset, utcOffset);
		}

		/// <summary>Converts a time to the time in another time zone based on the time zone's identifier.</summary>
		/// <returns>The date and time in the destination time zone.</returns>
		/// <param name="dateTime">The date and time to convert.</param>
		/// <param name="destinationTimeZoneId">The identifier of the destination time zone.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="destinationTimeZoneId" /> is null.</exception>
		/// <exception cref="T:System.InvalidTimeZoneException">The time zone identifier was found, but the registry data is corrupted.</exception>
		/// <exception cref="T:System.Security.SecurityException">The process does not have the permissions required to read from the registry key that contains the time zone information.</exception>
		/// <exception cref="T:System.TimeZoneNotFoundException">The <paramref name="destinationTimeZoneId" /> identifier was not found on the local system.</exception>
		// Token: 0x060014A8 RID: 5288 RVA: 0x00053D28 File Offset: 0x00051F28
		public static DateTime ConvertTimeBySystemTimeZoneId(DateTime dateTime, string destinationTimeZoneId)
		{
			return TimeZoneInfo.ConvertTime(dateTime, TimeZoneInfo.FindSystemTimeZoneById(destinationTimeZoneId));
		}

		/// <summary>Converts a time from one time zone to another based on time zone identifiers.</summary>
		/// <returns>The date and time in the destination time zone that corresponds to the <paramref name="dateTime" /> parameter in the source time zone.</returns>
		/// <param name="dateTime">The date and time to convert.</param>
		/// <param name="sourceTimeZoneId">The identifier of the source time zone. </param>
		/// <param name="destinationTimeZoneId">The identifier of the destination time zone.</param>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.DateTime.Kind" /> property of the <paramref name="dateTime" /> parameter does not correspond to the source time zone.-or-<paramref name="dateTime" /> is an invalid time in the source time zone.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="sourceTimeZoneId" /> is null.-or-<paramref name="destinationTimeZoneId" /> is null.</exception>
		/// <exception cref="T:System.InvalidTimeZoneException">The time zone identifiers were found, but the registry data is corrupted.</exception>
		/// <exception cref="T:System.Security.SecurityException">The process does not have the permissions required to read from the registry key that contains the time zone information.</exception>
		/// <exception cref="T:System.TimeZoneNotFoundException">The <paramref name="sourceTimeZoneId" /> identifier was not found on the local system.-or-The <paramref name="destinationTimeZoneId" /> identifier was not found on the local system.</exception>
		/// <exception cref="T:System.Security.SecurityException">The user does not have the permissions required to read from the registry keys that hold time zone data.</exception>
		// Token: 0x060014A9 RID: 5289 RVA: 0x00053D38 File Offset: 0x00051F38
		public static DateTime ConvertTimeBySystemTimeZoneId(DateTime dateTime, string sourceTimeZoneId, string destinationTimeZoneId)
		{
			TimeZoneInfo timeZoneInfo;
			if (dateTime.Kind == DateTimeKind.Utc && sourceTimeZoneId == TimeZoneInfo.Utc.Id)
			{
				timeZoneInfo = TimeZoneInfo.Utc;
			}
			else
			{
				timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(sourceTimeZoneId);
			}
			return TimeZoneInfo.ConvertTime(dateTime, timeZoneInfo, TimeZoneInfo.FindSystemTimeZoneById(destinationTimeZoneId));
		}

		/// <summary>Converts a time to the time in another time zone based on the time zone's identifier.</summary>
		/// <returns>The date and time in the destination time zone.</returns>
		/// <param name="dateTimeOffset">The date and time to convert.</param>
		/// <param name="destinationTimeZoneId">The identifier of the destination time zone.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="destinationTimeZoneId" /> is null.</exception>
		/// <exception cref="T:System.InvalidTimeZoneException">The time zone identifier was found but the registry data is corrupted.</exception>
		/// <exception cref="T:System.Security.SecurityException">The process does not have the permissions required to read from the registry key that contains the time zone information.</exception>
		/// <exception cref="T:System.TimeZoneNotFoundException">The <paramref name="destinationTimeZoneId" /> identifier was not found on the local system.</exception>
		// Token: 0x060014AA RID: 5290 RVA: 0x00053D7D File Offset: 0x00051F7D
		public static DateTimeOffset ConvertTimeBySystemTimeZoneId(DateTimeOffset dateTimeOffset, string destinationTimeZoneId)
		{
			return TimeZoneInfo.ConvertTime(dateTimeOffset, TimeZoneInfo.FindSystemTimeZoneById(destinationTimeZoneId));
		}

		// Token: 0x060014AB RID: 5291 RVA: 0x00053D8C File Offset: 0x00051F8C
		private DateTime ConvertTimeFromUtc(DateTime dateTime)
		{
			if (dateTime.Kind == DateTimeKind.Local)
			{
				throw new ArgumentException("Kind property of dateTime is Local");
			}
			if (this == TimeZoneInfo.Utc)
			{
				return DateTime.SpecifyKind(dateTime, DateTimeKind.Utc);
			}
			TimeSpan utcOffset = this.GetUtcOffset(dateTime);
			DateTimeKind dateTimeKind = ((this == TimeZoneInfo.Local) ? DateTimeKind.Local : DateTimeKind.Unspecified);
			DateTime dateTime2;
			if (!TimeZoneInfo.TryAddTicks(dateTime, utcOffset.Ticks, out dateTime2, dateTimeKind))
			{
				return DateTime.SpecifyKind(DateTime.MaxValue, dateTimeKind);
			}
			return dateTime2;
		}

		/// <summary>Converts a Coordinated Universal Time (UTC) to the time in a specified time zone.</summary>
		/// <returns>The date and time in the destination time zone. Its <see cref="P:System.DateTime.Kind" /> property is <see cref="F:System.DateTimeKind.Utc" /> if <paramref name="destinationTimeZone" /> is <see cref="P:System.TimeZoneInfo.Utc" />; otherwise, its <see cref="P:System.DateTime.Kind" /> property is <see cref="F:System.DateTimeKind.Unspecified" />.</returns>
		/// <param name="dateTime">The Coordinated Universal Time (UTC).</param>
		/// <param name="destinationTimeZone">The time zone to convert <paramref name="dateTime" /> to.</param>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.DateTime.Kind" /> property of <paramref name="dateTime" /> is <see cref="F:System.DateTimeKind.Local" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="destinationTimeZone" /> is null.</exception>
		// Token: 0x060014AC RID: 5292 RVA: 0x00053DF2 File Offset: 0x00051FF2
		public static DateTime ConvertTimeFromUtc(DateTime dateTime, TimeZoneInfo destinationTimeZone)
		{
			if (destinationTimeZone == null)
			{
				throw new ArgumentNullException("destinationTimeZone");
			}
			return destinationTimeZone.ConvertTimeFromUtc(dateTime);
		}

		/// <summary>Converts the current date and time to Coordinated Universal Time (UTC).</summary>
		/// <returns>The Coordinated Universal Time (UTC) that corresponds to the <paramref name="dateTime" /> parameter. The <see cref="T:System.DateTime" /> value's <see cref="P:System.DateTime.Kind" /> property is always set to <see cref="F:System.DateTimeKind.Utc" />.</returns>
		/// <param name="dateTime">The date and time to convert.</param>
		/// <exception cref="T:System.ArgumentException">TimeZoneInfo.Local.IsInvalidDateTime(<paramref name="dateTime" />) returns true.</exception>
		// Token: 0x060014AD RID: 5293 RVA: 0x00053E09 File Offset: 0x00052009
		public static DateTime ConvertTimeToUtc(DateTime dateTime)
		{
			if (dateTime.Kind == DateTimeKind.Utc)
			{
				return dateTime;
			}
			return TimeZoneInfo.ConvertTimeToUtc(dateTime, TimeZoneInfo.Local);
		}

		// Token: 0x060014AE RID: 5294 RVA: 0x00053E22 File Offset: 0x00052022
		internal static DateTime ConvertTimeToUtc(DateTime dateTime, TimeZoneInfoOptions flags)
		{
			return TimeZoneInfo.ConvertTimeToUtc(dateTime, TimeZoneInfo.Local, flags);
		}

		/// <summary>Converts the time in a specified time zone to Coordinated Universal Time (UTC).</summary>
		/// <returns>The Coordinated Universal Time (UTC) that corresponds to the <paramref name="dateTime" /> parameter. The <see cref="T:System.DateTime" /> object's <see cref="P:System.DateTime.Kind" /> property is always set to <see cref="F:System.DateTimeKind.Utc" />.</returns>
		/// <param name="dateTime">The date and time to convert.</param>
		/// <param name="sourceTimeZone">The time zone of <paramref name="dateTime" />.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="dateTime" />.Kind is <see cref="F:System.DateTimeKind.Utc" /> and <paramref name="sourceTimeZone" /> does not equal <see cref="P:System.TimeZoneInfo.Utc" />.-or-<paramref name="dateTime" />.Kind is <see cref="F:System.DateTimeKind.Local" /> and <paramref name="sourceTimeZone" /> does not equal <see cref="P:System.TimeZoneInfo.Local" />.-or-<paramref name="sourceTimeZone" />.IsInvalidDateTime(<paramref name="dateTime" />) returns true.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="sourceTimeZone" /> is null.</exception>
		// Token: 0x060014AF RID: 5295 RVA: 0x00053E30 File Offset: 0x00052030
		public static DateTime ConvertTimeToUtc(DateTime dateTime, TimeZoneInfo sourceTimeZone)
		{
			return TimeZoneInfo.ConvertTimeToUtc(dateTime, sourceTimeZone, TimeZoneInfoOptions.None);
		}

		// Token: 0x060014B0 RID: 5296 RVA: 0x00053E3C File Offset: 0x0005203C
		private static DateTime ConvertTimeToUtc(DateTime dateTime, TimeZoneInfo sourceTimeZone, TimeZoneInfoOptions flags)
		{
			if ((flags & TimeZoneInfoOptions.NoThrowOnInvalidTime) == (TimeZoneInfoOptions)0)
			{
				if (sourceTimeZone == null)
				{
					throw new ArgumentNullException("sourceTimeZone");
				}
				if (dateTime.Kind == DateTimeKind.Utc && sourceTimeZone != TimeZoneInfo.Utc)
				{
					throw new ArgumentException("Kind property of dateTime is Utc but the sourceTimeZone does not equal TimeZoneInfo.Utc");
				}
				if (dateTime.Kind == DateTimeKind.Local && sourceTimeZone != TimeZoneInfo.Local)
				{
					throw new ArgumentException("Kind property of dateTime is Local but the sourceTimeZone does not equal TimeZoneInfo.Local");
				}
				if (sourceTimeZone.IsInvalidTime(dateTime))
				{
					throw new ArgumentException("dateTime parameter is an invalid time");
				}
			}
			if (dateTime.Kind == DateTimeKind.Utc)
			{
				return dateTime;
			}
			bool flag;
			TimeSpan utcOffset = sourceTimeZone.GetUtcOffset(dateTime, out flag);
			DateTime dateTime2;
			TimeZoneInfo.TryAddTicks(dateTime, -utcOffset.Ticks, out dateTime2, DateTimeKind.Utc);
			return dateTime2;
		}

		// Token: 0x060014B1 RID: 5297 RVA: 0x00053ED4 File Offset: 0x000520D4
		internal static TimeSpan GetDateTimeNowUtcOffsetFromUtc(DateTime time, out bool isAmbiguousLocalDst)
		{
			bool flag;
			return TimeZoneInfo.GetUtcOffsetFromUtc(time, TimeZoneInfo.Local, out flag, out isAmbiguousLocalDst);
		}

		/// <summary>Creates a custom time zone with a specified identifier, an offset from Coordinated Universal Time (UTC), a display name, and a standard time display name.</summary>
		/// <returns>The new time zone.</returns>
		/// <param name="id">The time zone's identifier.</param>
		/// <param name="baseUtcOffset">An object that represents the time difference between this time zone and Coordinated Universal Time (UTC).</param>
		/// <param name="displayName">The display name of the new time zone.   </param>
		/// <param name="standardDisplayName">The name of the new time zone's standard time.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="id" /> parameter is null.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="id" /> parameter is an empty string ("").-or-The <paramref name="baseUtcOffset" /> parameter does not represent a whole number of minutes.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="baseUtcOffset" /> parameter is greater than 14 hours or less than -14 hours.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060014B2 RID: 5298 RVA: 0x00053EEF File Offset: 0x000520EF
		public static TimeZoneInfo CreateCustomTimeZone(string id, TimeSpan baseUtcOffset, string displayName, string standardDisplayName)
		{
			return TimeZoneInfo.CreateCustomTimeZone(id, baseUtcOffset, displayName, standardDisplayName, null, null, true);
		}

		/// <summary>Creates a custom time zone with a specified identifier, an offset from Coordinated Universal Time (UTC), a display name, a standard time name, a daylight saving time name, and daylight saving time rules.</summary>
		/// <returns>A <see cref="T:System.TimeZoneInfo" /> object that represents the new time zone.</returns>
		/// <param name="id">The time zone's identifier.</param>
		/// <param name="baseUtcOffset">An object that represents the time difference between this time zone and Coordinated Universal Time (UTC).</param>
		/// <param name="displayName">The display name of the new time zone.   </param>
		/// <param name="standardDisplayName">The new time zone's standard time name.</param>
		/// <param name="daylightDisplayName">The daylight saving time name of the new time zone.   </param>
		/// <param name="adjustmentRules">An array that augments the base UTC offset for a particular period. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="id" /> parameter is null.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="id" /> parameter is an empty string ("").-or-The <paramref name="baseUtcOffset" /> parameter does not represent a whole number of minutes.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="baseUtcOffset" /> parameter is greater than 14 hours or less than -14 hours.</exception>
		/// <exception cref="T:System.InvalidTimeZoneException">The adjustment rules specified in the <paramref name="adjustmentRules" /> parameter overlap.-or-The adjustment rules specified in the <paramref name="adjustmentRules" /> parameter are not in chronological order.-or-One or more elements in <paramref name="adjustmentRules" /> are null.-or-A date can have multiple adjustment rules applied to it.-or-The sum of the <paramref name="baseUtcOffset" /> parameter and the <see cref="P:System.TimeZoneInfo.AdjustmentRule.DaylightDelta" /> value of one or more objects in the <paramref name="adjustmentRules" /> array is greater than 14 hours or less than -14 hours.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060014B3 RID: 5299 RVA: 0x00053EFD File Offset: 0x000520FD
		public static TimeZoneInfo CreateCustomTimeZone(string id, TimeSpan baseUtcOffset, string displayName, string standardDisplayName, string daylightDisplayName, TimeZoneInfo.AdjustmentRule[] adjustmentRules)
		{
			return TimeZoneInfo.CreateCustomTimeZone(id, baseUtcOffset, displayName, standardDisplayName, daylightDisplayName, adjustmentRules, false);
		}

		/// <summary>Creates a custom time zone with a specified identifier, an offset from Coordinated Universal Time (UTC), a display name, a standard time name, a daylight saving time name, daylight saving time rules, and a value that indicates whether the returned object reflects daylight saving time information.</summary>
		/// <returns>The new time zone. If the <paramref name="disableDaylightSavingTime" /> parameter is true, the returned object has no daylight saving time data.</returns>
		/// <param name="id">The time zone's identifier.</param>
		/// <param name="baseUtcOffset">A <see cref="T:System.TimeSpan" /> object that represents the time difference between this time zone and Coordinated Universal Time (UTC).</param>
		/// <param name="displayName">The display name of the new time zone.   </param>
		/// <param name="standardDisplayName">The standard time name of the new time zone.</param>
		/// <param name="daylightDisplayName">The daylight saving time name of the new time zone.   </param>
		/// <param name="adjustmentRules">An array of <see cref="T:System.TimeZoneInfo.AdjustmentRule" /> objects that augment the base UTC offset for a particular period.</param>
		/// <param name="disableDaylightSavingTime">true to discard any daylight saving time-related information present in <paramref name="adjustmentRules" /> with the new object; otherwise, false.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="id" /> parameter is null.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="id" /> parameter is an empty string ("").-or-The <paramref name="baseUtcOffset" /> parameter does not represent a whole number of minutes.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="baseUtcOffset" /> parameter is greater than 14 hours or less than -14 hours.</exception>
		/// <exception cref="T:System.InvalidTimeZoneException">The adjustment rules specified in the <paramref name="adjustmentRules" /> parameter overlap.-or-The adjustment rules specified in the <paramref name="adjustmentRules" /> parameter are not in chronological order.-or-One or more elements in <paramref name="adjustmentRules" /> are null.-or-A date can have multiple adjustment rules applied to it.-or-The sum of the <paramref name="baseUtcOffset" /> parameter and the <see cref="P:System.TimeZoneInfo.AdjustmentRule.DaylightDelta" /> value of one or more objects in the <paramref name="adjustmentRules" /> array is greater than 14 hours or less than -14 hours.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060014B4 RID: 5300 RVA: 0x00053F0D File Offset: 0x0005210D
		public static TimeZoneInfo CreateCustomTimeZone(string id, TimeSpan baseUtcOffset, string displayName, string standardDisplayName, string daylightDisplayName, TimeZoneInfo.AdjustmentRule[] adjustmentRules, bool disableDaylightSavingTime)
		{
			return new TimeZoneInfo(id, baseUtcOffset, displayName, standardDisplayName, daylightDisplayName, adjustmentRules, disableDaylightSavingTime);
		}

		/// <summary>Determines whether the current <see cref="T:System.TimeZoneInfo" /> object and another object are equal.</summary>
		/// <returns>true if <paramref name="obj" /> is a <see cref="T:System.TimeZoneInfo" /> object that is equal to the current instance; otherwise, false.</returns>
		/// <param name="obj">A second object to compare with the current object.  </param>
		// Token: 0x060014B5 RID: 5301 RVA: 0x00053F1E File Offset: 0x0005211E
		public override bool Equals(object obj)
		{
			return this.Equals(obj as TimeZoneInfo);
		}

		/// <summary>Determines whether the current <see cref="T:System.TimeZoneInfo" /> object and another <see cref="T:System.TimeZoneInfo" /> object are equal.</summary>
		/// <returns>true if the two <see cref="T:System.TimeZoneInfo" /> objects are equal; otherwise, false.</returns>
		/// <param name="other">A second object to compare with the current object.  </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060014B6 RID: 5302 RVA: 0x00053F2C File Offset: 0x0005212C
		public bool Equals(TimeZoneInfo other)
		{
			return other != null && other.Id == this.Id && this.HasSameRules(other);
		}

		/// <summary>Retrieves a <see cref="T:System.TimeZoneInfo" /> object from the registry based on its identifier.</summary>
		/// <returns>An object whose identifier is the value of the <paramref name="id" /> parameter.</returns>
		/// <param name="id">The time zone identifier, which corresponds to the <see cref="P:System.TimeZoneInfo.Id" /> property.      </param>
		/// <exception cref="T:System.OutOfMemoryException">The system does not have enough memory to hold information about the time zone.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="id" /> parameter is null.</exception>
		/// <exception cref="T:System.TimeZoneNotFoundException">The time zone identifier specified by <paramref name="id" /> was not found. This means that a registry key whose name matches <paramref name="id" /> does not exist, or that the key exists but does not contain any time zone data.</exception>
		/// <exception cref="T:System.Security.SecurityException">The process does not have the permissions required to read from the registry key that contains the time zone information.</exception>
		/// <exception cref="T:System.InvalidTimeZoneException">The time zone identifier was found, but the registry data is corrupted.</exception>
		// Token: 0x060014B7 RID: 5303 RVA: 0x00053F50 File Offset: 0x00052150
		public static TimeZoneInfo FindSystemTimeZoneById(string id)
		{
			if (id == null)
			{
				throw new ArgumentNullException("id");
			}
			if (TimeZoneInfo.TimeZoneKey != null)
			{
				if (id == "Coordinated Universal Time")
				{
					id = "UTC";
				}
				RegistryKey registryKey = TimeZoneInfo.TimeZoneKey.OpenSubKey(id, false);
				if (registryKey == null)
				{
					throw new TimeZoneNotFoundException();
				}
				return TimeZoneInfo.FromRegistryKey(id, registryKey);
			}
			else
			{
				if (TimeZoneInfo.IsWindows)
				{
					return TimeZoneInfo.FindSystemTimeZoneByIdWinRTFallback(id);
				}
				if (id == "Local")
				{
					return TimeZoneInfo.Local;
				}
				return TimeZoneInfo.FindSystemTimeZoneByIdCore(id);
			}
		}

		// Token: 0x060014B8 RID: 5304 RVA: 0x00053FCC File Offset: 0x000521CC
		private static TimeZoneInfo FindSystemTimeZoneByFileName(string id, string filepath)
		{
			FileStream fileStream = null;
			try
			{
				fileStream = File.OpenRead(filepath);
			}
			catch (Exception ex)
			{
				throw new TimeZoneNotFoundException("Couldn't read time zone file " + filepath, ex);
			}
			TimeZoneInfo timeZoneInfo;
			try
			{
				timeZoneInfo = TimeZoneInfo.BuildFromStream(id, fileStream);
			}
			finally
			{
				if (fileStream != null)
				{
					fileStream.Dispose();
				}
			}
			return timeZoneInfo;
		}

		// Token: 0x060014B9 RID: 5305 RVA: 0x0005402C File Offset: 0x0005222C
		private static TimeZoneInfo FromRegistryKey(string id, RegistryKey key)
		{
			byte[] array = (byte[])key.GetValue("TZI");
			if (array == null)
			{
				throw new InvalidTimeZoneException();
			}
			int num = BitConverter.ToInt32(array, 0);
			TimeSpan timeSpan = new TimeSpan(0, -num, 0);
			string text = (string)key.GetValue("Display");
			string text2 = (string)key.GetValue("Std");
			string text3 = (string)key.GetValue("Dlt");
			List<TimeZoneInfo.AdjustmentRule> list = new List<TimeZoneInfo.AdjustmentRule>();
			RegistryKey registryKey = key.OpenSubKey("Dynamic DST", false);
			if (registryKey != null)
			{
				int num2 = (int)registryKey.GetValue("FirstEntry");
				int num3 = (int)registryKey.GetValue("LastEntry");
				for (int i = num2; i <= num3; i++)
				{
					byte[] array2 = (byte[])registryKey.GetValue(i.ToString());
					if (array2 != null)
					{
						int num4 = ((i == num2) ? 1 : i);
						int num5 = ((i == num3) ? 9999 : i);
						TimeZoneInfo.ParseRegTzi(list, num4, num5, array2);
					}
				}
			}
			else
			{
				TimeZoneInfo.ParseRegTzi(list, 1, 9999, array);
			}
			return TimeZoneInfo.CreateCustomTimeZone(id, timeSpan, text, text2, text3, TimeZoneInfo.ValidateRules(list));
		}

		// Token: 0x060014BA RID: 5306 RVA: 0x00054154 File Offset: 0x00052354
		private static void ParseRegTzi(List<TimeZoneInfo.AdjustmentRule> adjustmentRules, int start_year, int end_year, byte[] buffer)
		{
			int num = BitConverter.ToInt32(buffer, 8);
			int num2 = (int)BitConverter.ToInt16(buffer, 12);
			int num3 = (int)BitConverter.ToInt16(buffer, 14);
			int num4 = (int)BitConverter.ToInt16(buffer, 16);
			int num5 = (int)BitConverter.ToInt16(buffer, 18);
			int num6 = (int)BitConverter.ToInt16(buffer, 20);
			int num7 = (int)BitConverter.ToInt16(buffer, 22);
			int num8 = (int)BitConverter.ToInt16(buffer, 24);
			int num9 = (int)BitConverter.ToInt16(buffer, 26);
			int num10 = (int)BitConverter.ToInt16(buffer, 28);
			int num11 = (int)BitConverter.ToInt16(buffer, 30);
			int num12 = (int)BitConverter.ToInt16(buffer, 32);
			int num13 = (int)BitConverter.ToInt16(buffer, 34);
			int num14 = (int)BitConverter.ToInt16(buffer, 36);
			int num15 = (int)BitConverter.ToInt16(buffer, 38);
			int num16 = (int)BitConverter.ToInt16(buffer, 40);
			int num17 = (int)BitConverter.ToInt16(buffer, 42);
			if (num3 == 0 || num11 == 0)
			{
				return;
			}
			DateTime dateTime = new DateTime(1, 1, 1, num14, num15, num16, num17);
			DateTime dateTime2 = new DateTime(start_year, 1, 1);
			TimeZoneInfo.TransitionTime transitionTime;
			if (num10 == 0)
			{
				transitionTime = TimeZoneInfo.TransitionTime.CreateFloatingDateRule(dateTime, num11, num13, (DayOfWeek)num12);
			}
			else
			{
				transitionTime = TimeZoneInfo.TransitionTime.CreateFixedDateRule(dateTime, num11, num13);
			}
			DateTime dateTime3 = new DateTime(1, 1, 1, num6, num7, num8, num9);
			DateTime dateTime4 = new DateTime(end_year, 12, 31);
			TimeZoneInfo.TransitionTime transitionTime2;
			if (num2 == 0)
			{
				transitionTime2 = TimeZoneInfo.TransitionTime.CreateFloatingDateRule(dateTime3, num3, num5, (DayOfWeek)num4);
			}
			else
			{
				transitionTime2 = TimeZoneInfo.TransitionTime.CreateFixedDateRule(dateTime3, num3, num5);
			}
			TimeSpan timeSpan = new TimeSpan(0, -num, 0);
			adjustmentRules.Add(TimeZoneInfo.AdjustmentRule.CreateAdjustmentRule(dateTime2, dateTime4, timeSpan, transitionTime, transitionTime2));
		}

		/// <summary>Retrieves an array of <see cref="T:System.TimeZoneInfo.AdjustmentRule" /> objects that apply to the current <see cref="T:System.TimeZoneInfo" /> object.</summary>
		/// <returns>An array of objects for this time zone.</returns>
		/// <exception cref="T:System.OutOfMemoryException">The system does not have enough memory to make an in-memory copy of the adjustment rules.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060014BB RID: 5307 RVA: 0x000542A8 File Offset: 0x000524A8
		public TimeZoneInfo.AdjustmentRule[] GetAdjustmentRules()
		{
			if (!this.supportsDaylightSavingTime || this.adjustmentRules == null)
			{
				return new TimeZoneInfo.AdjustmentRule[0];
			}
			return (TimeZoneInfo.AdjustmentRule[])this.adjustmentRules.Clone();
		}

		/// <summary>Returns information about the possible dates and times that an ambiguous date and time can be mapped to.</summary>
		/// <returns>An array of objects that represents possible Coordinated Universal Time (UTC) offsets that a particular date and time can be mapped to.</returns>
		/// <param name="dateTime">A date and time.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="dateTime" /> is not an ambiguous time.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060014BC RID: 5308 RVA: 0x000542D4 File Offset: 0x000524D4
		public TimeSpan[] GetAmbiguousTimeOffsets(DateTime dateTime)
		{
			if (!this.IsAmbiguousTime(dateTime))
			{
				throw new ArgumentException("dateTime is not an ambiguous time");
			}
			TimeZoneInfo.AdjustmentRule applicableRule = this.GetApplicableRule(dateTime);
			if (applicableRule != null)
			{
				return new TimeSpan[]
				{
					this.baseUtcOffset,
					this.baseUtcOffset + applicableRule.DaylightDelta
				};
			}
			return new TimeSpan[] { this.baseUtcOffset, this.baseUtcOffset };
		}

		/// <summary>Returns information about the possible dates and times that an ambiguous date and time can be mapped to.</summary>
		/// <returns>An array of objects that represents possible Coordinated Universal Time (UTC) offsets that a particular date and time can be mapped to.</returns>
		/// <param name="dateTimeOffset">A date and time.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="dateTime" /> is not an ambiguous time.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060014BD RID: 5309 RVA: 0x0005434C File Offset: 0x0005254C
		public TimeSpan[] GetAmbiguousTimeOffsets(DateTimeOffset dateTimeOffset)
		{
			if (!this.IsAmbiguousTime(dateTimeOffset))
			{
				throw new ArgumentException("dateTimeOffset is not an ambiguous time");
			}
			throw new NotImplementedException();
		}

		/// <summary>Serves as a hash function for hashing algorithms and data structures such as hash tables.</summary>
		/// <returns>A 32-bit signed integer that serves as the hash code for this <see cref="T:System.TimeZoneInfo" /> object.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060014BE RID: 5310 RVA: 0x00054368 File Offset: 0x00052568
		public override int GetHashCode()
		{
			int num = this.Id.GetHashCode();
			foreach (TimeZoneInfo.AdjustmentRule adjustmentRule in this.GetAdjustmentRules())
			{
				num ^= adjustmentRule.GetHashCode();
			}
			return num;
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the data needed to serialize the current <see cref="T:System.TimeZoneInfo" /> object.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object to populate with data.</param>
		/// <param name="context">The destination for this serialization (see <see cref="T:System.Runtime.Serialization.StreamingContext" />).</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="info" /> parameter is null.</exception>
		// Token: 0x060014BF RID: 5311 RVA: 0x000543A4 File Offset: 0x000525A4
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("Id", this.id);
			info.AddValue("DisplayName", this.displayName);
			info.AddValue("StandardName", this.standardDisplayName);
			info.AddValue("DaylightName", this.daylightDisplayName);
			info.AddValue("BaseUtcOffset", this.baseUtcOffset);
			info.AddValue("AdjustmentRules", this.adjustmentRules);
			info.AddValue("SupportsDaylightSavingTime", this.SupportsDaylightSavingTime);
		}

		/// <summary>Returns a sorted collection of all the time zones about which information is available on the local system.</summary>
		/// <returns>A read-only collection of <see cref="T:System.TimeZoneInfo" /> objects.</returns>
		/// <exception cref="T:System.OutOfMemoryException">There is insufficient memory to store all time zone information.</exception>
		/// <exception cref="T:System.Security.SecurityException">The user does not have permission to read from the registry keys that contain time zone information.</exception>
		// Token: 0x060014C0 RID: 5312 RVA: 0x0005443C File Offset: 0x0005263C
		public static ReadOnlyCollection<TimeZoneInfo> GetSystemTimeZones()
		{
			if (TimeZoneInfo.systemTimeZones == null)
			{
				List<TimeZoneInfo> list = new List<TimeZoneInfo>();
				TimeZoneInfo.GetSystemTimeZonesCore(list);
				Interlocked.CompareExchange<ReadOnlyCollection<TimeZoneInfo>>(ref TimeZoneInfo.systemTimeZones, new ReadOnlyCollection<TimeZoneInfo>(list), null);
			}
			return TimeZoneInfo.systemTimeZones;
		}

		/// <summary>Calculates the offset or difference between the time in this time zone and Coordinated Universal Time (UTC) for a particular date and time.</summary>
		/// <returns>An object that indicates the time difference between the two time zones.</returns>
		/// <param name="dateTime">The date and time to determine the offset for.   </param>
		// Token: 0x060014C1 RID: 5313 RVA: 0x00054474 File Offset: 0x00052674
		public TimeSpan GetUtcOffset(DateTime dateTime)
		{
			bool flag;
			return this.GetUtcOffset(dateTime, out flag);
		}

		/// <summary>Calculates the offset or difference between the time in this time zone and Coordinated Universal Time (UTC) for a particular date and time.</summary>
		/// <returns>An object that indicates the time difference between Coordinated Universal Time (UTC) and the current time zone.</returns>
		/// <param name="dateTimeOffset">The date and time to determine the offset for.</param>
		// Token: 0x060014C2 RID: 5314 RVA: 0x0005448C File Offset: 0x0005268C
		public TimeSpan GetUtcOffset(DateTimeOffset dateTimeOffset)
		{
			bool flag;
			return this.GetUtcOffset(dateTimeOffset.UtcDateTime, out flag);
		}

		// Token: 0x060014C3 RID: 5315 RVA: 0x000544A8 File Offset: 0x000526A8
		private TimeSpan GetUtcOffset(DateTime dateTime, out bool isDST)
		{
			isDST = false;
			TimeZoneInfo timeZoneInfo = this;
			if (dateTime.Kind == DateTimeKind.Utc)
			{
				timeZoneInfo = TimeZoneInfo.Utc;
			}
			if (dateTime.Kind == DateTimeKind.Local)
			{
				timeZoneInfo = TimeZoneInfo.Local;
			}
			bool flag;
			TimeSpan utcOffsetHelper = TimeZoneInfo.GetUtcOffsetHelper(dateTime, timeZoneInfo, out flag);
			if (timeZoneInfo == this)
			{
				isDST = flag;
				return utcOffsetHelper;
			}
			DateTime dateTime2;
			if (!TimeZoneInfo.TryAddTicks(dateTime, -utcOffsetHelper.Ticks, out dateTime2, DateTimeKind.Utc))
			{
				return this.BaseUtcOffset;
			}
			return TimeZoneInfo.GetUtcOffsetHelper(dateTime2, this, out isDST);
		}

		// Token: 0x060014C4 RID: 5316 RVA: 0x00054510 File Offset: 0x00052710
		private static TimeSpan GetUtcOffsetHelper(DateTime dateTime, TimeZoneInfo tz, out bool isDST)
		{
			if (dateTime.Kind == DateTimeKind.Local && tz != TimeZoneInfo.Local)
			{
				throw new Exception();
			}
			isDST = false;
			if (tz == TimeZoneInfo.Utc)
			{
				return TimeSpan.Zero;
			}
			TimeSpan timeSpan;
			if (tz.TryGetTransitionOffset(dateTime, out timeSpan, out isDST))
			{
				return timeSpan;
			}
			if (dateTime.Kind == DateTimeKind.Utc)
			{
				TimeZoneInfo.AdjustmentRule applicableRule = tz.GetApplicableRule(dateTime);
				if (applicableRule != null && tz.IsInDST(applicableRule, dateTime))
				{
					isDST = true;
					return tz.BaseUtcOffset + applicableRule.DaylightDelta;
				}
				return tz.BaseUtcOffset;
			}
			else
			{
				DateTime dateTime2;
				if (!TimeZoneInfo.TryAddTicks(dateTime, -tz.BaseUtcOffset.Ticks, out dateTime2, DateTimeKind.Utc))
				{
					return tz.BaseUtcOffset;
				}
				TimeZoneInfo.AdjustmentRule applicableRule2 = tz.GetApplicableRule(dateTime2);
				DateTime minValue = DateTime.MinValue;
				if (applicableRule2 != null && !TimeZoneInfo.TryAddTicks(dateTime2, -applicableRule2.DaylightDelta.Ticks, out minValue, DateTimeKind.Utc))
				{
					return tz.BaseUtcOffset;
				}
				if (applicableRule2 == null || !tz.IsInDST(applicableRule2, dateTime))
				{
					return tz.BaseUtcOffset;
				}
				isDST = true;
				if (tz.IsInDST(applicableRule2, minValue))
				{
					return tz.BaseUtcOffset + applicableRule2.DaylightDelta;
				}
				return tz.BaseUtcOffset;
			}
		}

		/// <summary>Indicates whether the current object and another <see cref="T:System.TimeZoneInfo" /> object have the same adjustment rules.</summary>
		/// <returns>true if the two time zones have identical adjustment rules and an identical base offset; otherwise, false.</returns>
		/// <param name="other">A second object to compare with the current <see cref="T:System.TimeZoneInfo" /> object.   </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="other" /> parameter is null.</exception>
		// Token: 0x060014C5 RID: 5317 RVA: 0x00054624 File Offset: 0x00052824
		public bool HasSameRules(TimeZoneInfo other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			if (this.adjustmentRules == null != (other.adjustmentRules == null))
			{
				return false;
			}
			if (this.adjustmentRules == null)
			{
				return true;
			}
			if (this.BaseUtcOffset != other.BaseUtcOffset)
			{
				return false;
			}
			if (this.adjustmentRules.Length != other.adjustmentRules.Length)
			{
				return false;
			}
			for (int i = 0; i < this.adjustmentRules.Length; i++)
			{
				if (!this.adjustmentRules[i].Equals(other.adjustmentRules[i]))
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Determines whether a particular date and time in a particular time zone is ambiguous and can be mapped to two or more Coordinated Universal Time (UTC) times.</summary>
		/// <returns>true if the <paramref name="dateTime" /> parameter is ambiguous; otherwise, false.</returns>
		/// <param name="dateTime">A date and time value.   </param>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.DateTime.Kind" /> property of the <paramref name="dateTime" /> value is <see cref="F:System.DateTimeKind.Local" /> and <paramref name="dateTime" /> is an invalid time.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060014C6 RID: 5318 RVA: 0x000546B8 File Offset: 0x000528B8
		public bool IsAmbiguousTime(DateTime dateTime)
		{
			if (dateTime.Kind == DateTimeKind.Local && this.IsInvalidTime(dateTime))
			{
				throw new ArgumentException("Kind is Local and time is Invalid");
			}
			if (this == TimeZoneInfo.Utc)
			{
				return false;
			}
			if (dateTime.Kind == DateTimeKind.Utc)
			{
				dateTime = this.ConvertTimeFromUtc(dateTime);
			}
			if (dateTime.Kind == DateTimeKind.Local && this != TimeZoneInfo.Local)
			{
				dateTime = TimeZoneInfo.ConvertTime(dateTime, TimeZoneInfo.Local, this);
			}
			TimeZoneInfo.AdjustmentRule applicableRule = this.GetApplicableRule(dateTime);
			if (applicableRule != null)
			{
				DateTime dateTime2 = TimeZoneInfo.TransitionPoint(applicableRule.DaylightTransitionEnd, dateTime.Year);
				if (dateTime > dateTime2 - applicableRule.DaylightDelta && dateTime <= dateTime2)
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Determines whether a particular date and time in a particular time zone is ambiguous and can be mapped to two or more Coordinated Universal Time (UTC) times.</summary>
		/// <returns>true if the <paramref name="dateTimeOffset" /> parameter is ambiguous in the current time zone; otherwise, false.</returns>
		/// <param name="dateTimeOffset">A date and time.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060014C7 RID: 5319 RVA: 0x0002126B File Offset: 0x0001F46B
		public bool IsAmbiguousTime(DateTimeOffset dateTimeOffset)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060014C8 RID: 5320 RVA: 0x0005475E File Offset: 0x0005295E
		private bool IsInDST(TimeZoneInfo.AdjustmentRule rule, DateTime dateTime)
		{
			return this.IsInDSTForYear(rule, dateTime, dateTime.Year) || (dateTime.Year > 1 && this.IsInDSTForYear(rule, dateTime, dateTime.Year - 1));
		}

		// Token: 0x060014C9 RID: 5321 RVA: 0x00054790 File Offset: 0x00052990
		private bool IsInDSTForYear(TimeZoneInfo.AdjustmentRule rule, DateTime dateTime, int year)
		{
			DateTime dateTime2 = TimeZoneInfo.TransitionPoint(rule.DaylightTransitionStart, year);
			DateTime dateTime3 = TimeZoneInfo.TransitionPoint(rule.DaylightTransitionEnd, year + ((rule.DaylightTransitionStart.Month < rule.DaylightTransitionEnd.Month) ? 0 : 1));
			if (dateTime.Kind == DateTimeKind.Utc)
			{
				dateTime2 -= this.BaseUtcOffset;
				dateTime3 -= this.BaseUtcOffset + rule.DaylightDelta;
			}
			return dateTime >= dateTime2 && dateTime < dateTime3;
		}

		/// <summary>Indicates whether a specified date and time falls in the range of daylight saving time for the time zone of the current <see cref="T:System.TimeZoneInfo" /> object.</summary>
		/// <returns>true if the <paramref name="dateTime" /> parameter is a daylight saving time; otherwise, false.</returns>
		/// <param name="dateTime">A date and time value.   </param>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.DateTime.Kind" /> property of the <paramref name="dateTime" /> value is <see cref="F:System.DateTimeKind.Local" /> and <paramref name="dateTime" /> is an invalid time.</exception>
		// Token: 0x060014CA RID: 5322 RVA: 0x0005481C File Offset: 0x00052A1C
		public bool IsDaylightSavingTime(DateTime dateTime)
		{
			if (dateTime.Kind == DateTimeKind.Local && this.IsInvalidTime(dateTime))
			{
				throw new ArgumentException("dateTime is invalid and Kind is Local");
			}
			if (this == TimeZoneInfo.Utc)
			{
				return false;
			}
			if (!this.SupportsDaylightSavingTime)
			{
				return false;
			}
			bool flag;
			this.GetUtcOffset(dateTime, out flag);
			return flag;
		}

		// Token: 0x060014CB RID: 5323 RVA: 0x00054866 File Offset: 0x00052A66
		internal bool IsDaylightSavingTime(DateTime dateTime, TimeZoneInfoOptions flags)
		{
			return this.IsDaylightSavingTime(dateTime);
		}

		/// <summary>Indicates whether a specified date and time falls in the range of daylight saving time for the time zone of the current <see cref="T:System.TimeZoneInfo" /> object.</summary>
		/// <returns>true if the <paramref name="dateTimeOffset" /> parameter is a daylight saving time; otherwise, false.</returns>
		/// <param name="dateTimeOffset">A date and time value.</param>
		// Token: 0x060014CC RID: 5324 RVA: 0x0005486F File Offset: 0x00052A6F
		public bool IsDaylightSavingTime(DateTimeOffset dateTimeOffset)
		{
			return this.IsDaylightSavingTime(dateTimeOffset.DateTime);
		}

		// Token: 0x060014CD RID: 5325 RVA: 0x00054880 File Offset: 0x00052A80
		internal DaylightTime GetDaylightChanges(int year)
		{
			DateTime dateTime = DateTime.MinValue;
			DateTime dateTime2 = DateTime.MinValue;
			TimeSpan timeSpan = default(TimeSpan);
			if (this.transitions != null)
			{
				dateTime2 = DateTime.MaxValue;
				for (int i = this.transitions.Count - 1; i >= 0; i--)
				{
					KeyValuePair<DateTime, TimeType> keyValuePair = this.transitions[i];
					DateTime key = keyValuePair.Key;
					TimeType value = keyValuePair.Value;
					if (key.Year <= year)
					{
						if (key.Year < year)
						{
							break;
						}
						if (value.IsDst)
						{
							timeSpan = new TimeSpan(0, 0, value.Offset) - this.BaseUtcOffset;
							dateTime = key;
						}
						else
						{
							dateTime2 = key;
						}
					}
				}
				if (!TimeZoneInfo.TryAddTicks(dateTime, this.BaseUtcOffset.Ticks, out dateTime, DateTimeKind.Unspecified))
				{
					dateTime = DateTime.MinValue;
				}
				if (!TimeZoneInfo.TryAddTicks(dateTime2, this.BaseUtcOffset.Ticks + timeSpan.Ticks, out dateTime2, DateTimeKind.Unspecified))
				{
					dateTime2 = DateTime.MinValue;
				}
			}
			else
			{
				TimeZoneInfo.AdjustmentRule adjustmentRule = null;
				TimeZoneInfo.AdjustmentRule adjustmentRule2 = null;
				foreach (TimeZoneInfo.AdjustmentRule adjustmentRule3 in this.GetAdjustmentRules())
				{
					if (adjustmentRule3.DateStart.Year <= year && adjustmentRule3.DateEnd.Year >= year)
					{
						if (adjustmentRule3.DateStart.Year <= year && (adjustmentRule == null || adjustmentRule3.DateStart.Year > adjustmentRule.DateStart.Year))
						{
							adjustmentRule = adjustmentRule3;
						}
						if (adjustmentRule3.DateEnd.Year >= year && (adjustmentRule2 == null || adjustmentRule3.DateEnd.Year < adjustmentRule2.DateEnd.Year))
						{
							adjustmentRule2 = adjustmentRule3;
						}
					}
				}
				if (adjustmentRule == null || adjustmentRule2 == null)
				{
					return new DaylightTime(default(DateTime), default(DateTime), default(TimeSpan));
				}
				dateTime = TimeZoneInfo.TransitionPoint(adjustmentRule.DaylightTransitionStart, year);
				dateTime2 = TimeZoneInfo.TransitionPoint(adjustmentRule2.DaylightTransitionEnd, year);
				timeSpan = adjustmentRule.DaylightDelta;
			}
			if (dateTime == DateTime.MinValue || dateTime2 == DateTime.MinValue)
			{
				return new DaylightTime(default(DateTime), default(DateTime), default(TimeSpan));
			}
			return new DaylightTime(dateTime, dateTime2, timeSpan);
		}

		/// <summary>Indicates whether a particular date and time is invalid.</summary>
		/// <returns>true if <paramref name="dateTime" /> is invalid; otherwise, false.</returns>
		/// <param name="dateTime">A date and time value.   </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060014CE RID: 5326 RVA: 0x00054AE0 File Offset: 0x00052CE0
		public bool IsInvalidTime(DateTime dateTime)
		{
			if (dateTime.Kind == DateTimeKind.Utc)
			{
				return false;
			}
			if (dateTime.Kind == DateTimeKind.Local && this != TimeZoneInfo.Local)
			{
				return false;
			}
			TimeZoneInfo.AdjustmentRule applicableRule = this.GetApplicableRule(dateTime);
			if (applicableRule != null)
			{
				DateTime dateTime2 = TimeZoneInfo.TransitionPoint(applicableRule.DaylightTransitionStart, dateTime.Year);
				if (dateTime >= dateTime2 && dateTime < dateTime2 + applicableRule.DaylightDelta)
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Runs when the deserialization of an object has been completed.</summary>
		/// <param name="sender">The object that initiated the callback. The functionality for this parameter is not currently implemented.</param>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">The <see cref="T:System.TimeZoneInfo" /> object contains invalid or corrupted data.</exception>
		// Token: 0x060014CF RID: 5327 RVA: 0x00054B4C File Offset: 0x00052D4C
		void IDeserializationCallback.OnDeserialization(object sender)
		{
			try
			{
				TimeZoneInfo.Validate(this.id, this.baseUtcOffset, this.adjustmentRules);
			}
			catch (ArgumentException ex)
			{
				throw new SerializationException("invalid serialization data", ex);
			}
		}

		// Token: 0x060014D0 RID: 5328 RVA: 0x00054B90 File Offset: 0x00052D90
		private static void Validate(string id, TimeSpan baseUtcOffset, TimeZoneInfo.AdjustmentRule[] adjustmentRules)
		{
			if (id == null)
			{
				throw new ArgumentNullException("id");
			}
			if (id == string.Empty)
			{
				throw new ArgumentException("id parameter is an empty string");
			}
			if (baseUtcOffset.Ticks % 600000000L != 0L)
			{
				throw new ArgumentException("baseUtcOffset parameter does not represent a whole number of minutes");
			}
			if (baseUtcOffset > new TimeSpan(14, 0, 0) || baseUtcOffset < new TimeSpan(-14, 0, 0))
			{
				throw new ArgumentOutOfRangeException("baseUtcOffset parameter is greater than 14 hours or less than -14 hours");
			}
			if (adjustmentRules != null && adjustmentRules.Length != 0)
			{
				TimeZoneInfo.AdjustmentRule adjustmentRule = null;
				foreach (TimeZoneInfo.AdjustmentRule adjustmentRule2 in adjustmentRules)
				{
					if (adjustmentRule2 == null)
					{
						throw new InvalidTimeZoneException("one or more elements in adjustmentRules are null");
					}
					if (baseUtcOffset + adjustmentRule2.DaylightDelta < new TimeSpan(-14, 0, 0) || baseUtcOffset + adjustmentRule2.DaylightDelta > new TimeSpan(14, 0, 0))
					{
						throw new InvalidTimeZoneException("Sum of baseUtcOffset and DaylightDelta of one or more object in adjustmentRules array is greater than 14 or less than -14 hours;");
					}
					if (adjustmentRule != null && adjustmentRule.DateStart > adjustmentRule2.DateStart)
					{
						throw new InvalidTimeZoneException("adjustment rules specified in adjustmentRules parameter are not in chronological order");
					}
					if (adjustmentRule != null && adjustmentRule.DateEnd > adjustmentRule2.DateStart)
					{
						throw new InvalidTimeZoneException("some adjustment rules in the adjustmentRules parameter overlap");
					}
					if (adjustmentRule != null && adjustmentRule.DateEnd == adjustmentRule2.DateStart)
					{
						throw new InvalidTimeZoneException("a date can have multiple adjustment rules applied to it");
					}
					adjustmentRule = adjustmentRule2;
				}
			}
		}

		/// <summary>Returns the current <see cref="T:System.TimeZoneInfo" /> object's display name.</summary>
		/// <returns>The value of the <see cref="P:System.TimeZoneInfo.DisplayName" /> property of the current <see cref="T:System.TimeZoneInfo" /> object.</returns>
		// Token: 0x060014D1 RID: 5329 RVA: 0x00054CEA File Offset: 0x00052EEA
		public override string ToString()
		{
			return this.DisplayName;
		}

		// Token: 0x060014D2 RID: 5330 RVA: 0x00054CF4 File Offset: 0x00052EF4
		private TimeZoneInfo(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			this.id = (string)info.GetValue("Id", typeof(string));
			this.displayName = (string)info.GetValue("DisplayName", typeof(string));
			this.standardDisplayName = (string)info.GetValue("StandardName", typeof(string));
			this.daylightDisplayName = (string)info.GetValue("DaylightName", typeof(string));
			this.baseUtcOffset = (TimeSpan)info.GetValue("BaseUtcOffset", typeof(TimeSpan));
			this.adjustmentRules = (TimeZoneInfo.AdjustmentRule[])info.GetValue("AdjustmentRules", typeof(TimeZoneInfo.AdjustmentRule[]));
			this.supportsDaylightSavingTime = (bool)info.GetValue("SupportsDaylightSavingTime", typeof(bool));
		}

		// Token: 0x060014D3 RID: 5331 RVA: 0x00054DF8 File Offset: 0x00052FF8
		private TimeZoneInfo(string id, TimeSpan baseUtcOffset, string displayName, string standardDisplayName, string daylightDisplayName, TimeZoneInfo.AdjustmentRule[] adjustmentRules, bool disableDaylightSavingTime)
		{
			if (id == null)
			{
				throw new ArgumentNullException("id");
			}
			if (id == string.Empty)
			{
				throw new ArgumentException("id parameter is an empty string");
			}
			if (baseUtcOffset.Ticks % 600000000L != 0L)
			{
				throw new ArgumentException("baseUtcOffset parameter does not represent a whole number of minutes");
			}
			if (baseUtcOffset > new TimeSpan(14, 0, 0) || baseUtcOffset < new TimeSpan(-14, 0, 0))
			{
				throw new ArgumentOutOfRangeException("baseUtcOffset parameter is greater than 14 hours or less than -14 hours");
			}
			bool flag = !disableDaylightSavingTime;
			if (adjustmentRules != null && adjustmentRules.Length != 0)
			{
				TimeZoneInfo.AdjustmentRule adjustmentRule = null;
				foreach (TimeZoneInfo.AdjustmentRule adjustmentRule2 in adjustmentRules)
				{
					if (adjustmentRule2 == null)
					{
						throw new InvalidTimeZoneException("one or more elements in adjustmentRules are null");
					}
					if (baseUtcOffset + adjustmentRule2.DaylightDelta < new TimeSpan(-14, 0, 0) || baseUtcOffset + adjustmentRule2.DaylightDelta > new TimeSpan(14, 0, 0))
					{
						throw new InvalidTimeZoneException("Sum of baseUtcOffset and DaylightDelta of one or more object in adjustmentRules array is greater than 14 or less than -14 hours;");
					}
					if (adjustmentRule != null && adjustmentRule.DateStart > adjustmentRule2.DateStart)
					{
						throw new InvalidTimeZoneException("adjustment rules specified in adjustmentRules parameter are not in chronological order");
					}
					if (adjustmentRule != null && adjustmentRule.DateEnd > adjustmentRule2.DateStart)
					{
						throw new InvalidTimeZoneException("some adjustment rules in the adjustmentRules parameter overlap");
					}
					if (adjustmentRule != null && adjustmentRule.DateEnd == adjustmentRule2.DateStart)
					{
						throw new InvalidTimeZoneException("a date can have multiple adjustment rules applied to it");
					}
					adjustmentRule = adjustmentRule2;
				}
			}
			else
			{
				flag = false;
			}
			this.id = id;
			this.baseUtcOffset = baseUtcOffset;
			this.displayName = displayName ?? id;
			this.standardDisplayName = standardDisplayName ?? id;
			this.daylightDisplayName = daylightDisplayName;
			this.supportsDaylightSavingTime = flag;
			this.adjustmentRules = adjustmentRules;
		}

		// Token: 0x060014D4 RID: 5332 RVA: 0x00054FAC File Offset: 0x000531AC
		private TimeZoneInfo.AdjustmentRule GetApplicableRule(DateTime dateTime)
		{
			DateTime dateTime2 = dateTime;
			if (dateTime.Kind == DateTimeKind.Local && this != TimeZoneInfo.Local)
			{
				if (!TimeZoneInfo.TryAddTicks(dateTime2.ToUniversalTime(), this.BaseUtcOffset.Ticks, out dateTime2, DateTimeKind.Unspecified))
				{
					return null;
				}
			}
			else if (dateTime.Kind == DateTimeKind.Utc && this != TimeZoneInfo.Utc && !TimeZoneInfo.TryAddTicks(dateTime2, this.BaseUtcOffset.Ticks, out dateTime2, DateTimeKind.Unspecified))
			{
				return null;
			}
			dateTime2 = dateTime2.Date;
			if (this.adjustmentRules != null)
			{
				foreach (TimeZoneInfo.AdjustmentRule adjustmentRule in this.adjustmentRules)
				{
					if (adjustmentRule.DateStart > dateTime2)
					{
						return null;
					}
					if (!(adjustmentRule.DateEnd < dateTime2))
					{
						return adjustmentRule;
					}
				}
			}
			return null;
		}

		// Token: 0x060014D5 RID: 5333 RVA: 0x0005506C File Offset: 0x0005326C
		private bool TryGetTransitionOffset(DateTime dateTime, out TimeSpan offset, out bool isDst)
		{
			offset = this.BaseUtcOffset;
			isDst = false;
			if (this.transitions == null)
			{
				return false;
			}
			DateTime dateTime2 = dateTime;
			if (dateTime.Kind == DateTimeKind.Local && this != TimeZoneInfo.Local && !TimeZoneInfo.TryAddTicks(dateTime2.ToUniversalTime(), this.BaseUtcOffset.Ticks, out dateTime2, DateTimeKind.Utc))
			{
				return false;
			}
			if (dateTime.Kind != DateTimeKind.Utc && !TimeZoneInfo.TryAddTicks(dateTime2, -this.BaseUtcOffset.Ticks, out dateTime2, DateTimeKind.Utc))
			{
				return false;
			}
			TimeZoneInfo.AdjustmentRule applicableRule = this.GetApplicableRule(dateTime2);
			if (applicableRule != null)
			{
				DateTime dateTime3 = TimeZoneInfo.TransitionPoint(applicableRule.DaylightTransitionStart, dateTime2.Year);
				DateTime dateTime4 = TimeZoneInfo.TransitionPoint(applicableRule.DaylightTransitionEnd, dateTime2.Year);
				if (dateTime2 >= dateTime3 && dateTime2 <= dateTime4)
				{
					offset = this.baseUtcOffset + applicableRule.DaylightDelta;
					isDst = true;
					return true;
				}
			}
			return false;
		}

		// Token: 0x060014D6 RID: 5334 RVA: 0x00055150 File Offset: 0x00053350
		private static DateTime TransitionPoint(TimeZoneInfo.TransitionTime transition, int year)
		{
			if (transition.IsFixedDateRule)
			{
				return new DateTime(year, transition.Month, transition.Day) + transition.TimeOfDay.TimeOfDay;
			}
			DayOfWeek dayOfWeek = new DateTime(year, transition.Month, 1).DayOfWeek;
			int num = 1 + (transition.Week - 1) * 7 + (transition.DayOfWeek - dayOfWeek + 7) % 7;
			if (num > DateTime.DaysInMonth(year, transition.Month))
			{
				num -= 7;
			}
			if (num < 1)
			{
				num += 7;
			}
			return new DateTime(year, transition.Month, num) + transition.TimeOfDay.TimeOfDay;
		}

		// Token: 0x060014D7 RID: 5335 RVA: 0x00055200 File Offset: 0x00053400
		private static TimeZoneInfo.AdjustmentRule[] ValidateRules(List<TimeZoneInfo.AdjustmentRule> adjustmentRules)
		{
			if (adjustmentRules == null || adjustmentRules.Count == 0)
			{
				return null;
			}
			TimeZoneInfo.AdjustmentRule adjustmentRule = null;
			foreach (TimeZoneInfo.AdjustmentRule adjustmentRule2 in adjustmentRules.ToArray())
			{
				if (adjustmentRule != null && adjustmentRule.DateEnd > adjustmentRule2.DateStart)
				{
					adjustmentRules.Remove(adjustmentRule2);
				}
				adjustmentRule = adjustmentRule2;
			}
			return adjustmentRules.ToArray();
		}

		// Token: 0x060014D8 RID: 5336 RVA: 0x0005525C File Offset: 0x0005345C
		private static TimeZoneInfo BuildFromStream(string id, Stream stream)
		{
			byte[] array = new byte[16384];
			int num = stream.Read(array, 0, 16384);
			if (!TimeZoneInfo.ValidTZFile(array, num))
			{
				throw new InvalidTimeZoneException("TZ file too big for the buffer");
			}
			TimeZoneInfo timeZoneInfo;
			try
			{
				timeZoneInfo = TimeZoneInfo.ParseTZBuffer(id, array, num);
			}
			catch (InvalidTimeZoneException)
			{
				throw;
			}
			catch (Exception ex)
			{
				throw new InvalidTimeZoneException("Time zone information file contains invalid data", ex);
			}
			return timeZoneInfo;
		}

		// Token: 0x060014D9 RID: 5337 RVA: 0x000552D0 File Offset: 0x000534D0
		private static bool ValidTZFile(byte[] buffer, int length)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < 4; i++)
			{
				stringBuilder.Append((char)buffer[i]);
			}
			return !(stringBuilder.ToString() != "TZif") && length < 16384;
		}

		// Token: 0x060014DA RID: 5338 RVA: 0x00055318 File Offset: 0x00053518
		private static int SwapInt32(int i)
		{
			return ((i >> 24) & 255) | ((i >> 8) & 65280) | ((i << 8) & 16711680) | ((i & 255) << 24);
		}

		// Token: 0x060014DB RID: 5339 RVA: 0x00055344 File Offset: 0x00053544
		private static int ReadBigEndianInt32(byte[] buffer, int start)
		{
			int num = BitConverter.ToInt32(buffer, start);
			if (!BitConverter.IsLittleEndian)
			{
				return num;
			}
			return TimeZoneInfo.SwapInt32(num);
		}

		// Token: 0x060014DC RID: 5340 RVA: 0x00055368 File Offset: 0x00053568
		private static TimeZoneInfo ParseTZBuffer(string id, byte[] buffer, int length)
		{
			int num = TimeZoneInfo.ReadBigEndianInt32(buffer, 20);
			int num2 = TimeZoneInfo.ReadBigEndianInt32(buffer, 24);
			int num3 = TimeZoneInfo.ReadBigEndianInt32(buffer, 28);
			int num4 = TimeZoneInfo.ReadBigEndianInt32(buffer, 32);
			int num5 = TimeZoneInfo.ReadBigEndianInt32(buffer, 36);
			int num6 = TimeZoneInfo.ReadBigEndianInt32(buffer, 40);
			if (length < 44 + num4 * 5 + num5 * 6 + num6 + num3 * 8 + num2 + num)
			{
				throw new InvalidTimeZoneException();
			}
			Dictionary<int, string> dictionary = TimeZoneInfo.ParseAbbreviations(buffer, 44 + 4 * num4 + num4 + 6 * num5, num6);
			Dictionary<int, TimeType> dictionary2 = TimeZoneInfo.ParseTimesTypes(buffer, 44 + 4 * num4 + num4, num5, dictionary);
			List<KeyValuePair<DateTime, TimeType>> list = TimeZoneInfo.ParseTransitions(buffer, 44, num4, dictionary2);
			if (dictionary2.Count == 0)
			{
				throw new InvalidTimeZoneException();
			}
			if (dictionary2.Count == 1 && dictionary2[0].IsDst)
			{
				throw new InvalidTimeZoneException();
			}
			TimeSpan timeSpan = new TimeSpan(0L);
			TimeSpan timeSpan2 = new TimeSpan(0L);
			string text = null;
			string text2 = null;
			bool flag = false;
			DateTime dateTime = DateTime.MinValue;
			List<TimeZoneInfo.AdjustmentRule> list2 = new List<TimeZoneInfo.AdjustmentRule>();
			bool flag2 = false;
			for (int i = 0; i < list.Count; i++)
			{
				KeyValuePair<DateTime, TimeType> keyValuePair = list[i];
				DateTime key = keyValuePair.Key;
				TimeType value = keyValuePair.Value;
				if (!value.IsDst)
				{
					if (text != value.Name)
					{
						text = value.Name;
					}
					if (timeSpan.TotalSeconds != (double)value.Offset)
					{
						timeSpan = new TimeSpan(0, 0, value.Offset);
						if (list2.Count > 0)
						{
							flag2 = true;
						}
						list2 = new List<TimeZoneInfo.AdjustmentRule>();
						flag = false;
					}
					if (flag)
					{
						dateTime += timeSpan;
						DateTime dateTime2 = key + timeSpan + timeSpan2;
						if (dateTime2.Date == new DateTime(dateTime2.Year, 1, 1) && dateTime2.Year > dateTime.Year)
						{
							dateTime2 -= new TimeSpan(24, 0, 0);
						}
						if (dateTime.AddYears(1) < dateTime2)
						{
							flag2 = true;
						}
						DateTime dateTime3;
						if (dateTime.Month < 7)
						{
							dateTime3 = new DateTime(dateTime.Year, 1, 1);
						}
						else
						{
							dateTime3 = new DateTime(dateTime.Year, 7, 1);
						}
						DateTime dateTime4;
						if (dateTime2.Month >= 7)
						{
							dateTime4 = new DateTime(dateTime2.Year, 12, 31);
						}
						else
						{
							dateTime4 = new DateTime(dateTime2.Year, 6, 30);
						}
						TimeZoneInfo.TransitionTime transitionTime = TimeZoneInfo.TransitionTime.CreateFixedDateRule(new DateTime(1, 1, 1) + dateTime.TimeOfDay, dateTime.Month, dateTime.Day);
						TimeZoneInfo.TransitionTime transitionTime2 = TimeZoneInfo.TransitionTime.CreateFixedDateRule(new DateTime(1, 1, 1) + dateTime2.TimeOfDay, dateTime2.Month, dateTime2.Day);
						if (transitionTime != transitionTime2)
						{
							list2.Add(TimeZoneInfo.AdjustmentRule.CreateAdjustmentRule(dateTime3, dateTime4, timeSpan2, transitionTime, transitionTime2));
						}
					}
					flag = false;
				}
				else
				{
					if (text2 != value.Name)
					{
						text2 = value.Name;
					}
					if (timeSpan2.TotalSeconds != (double)value.Offset - timeSpan.TotalSeconds)
					{
						timeSpan2 = new TimeSpan(0, 0, value.Offset) - timeSpan;
						if (timeSpan2.Ticks % 600000000L != 0L)
						{
							timeSpan2 = TimeSpan.FromMinutes((double)((long)(timeSpan2.TotalMinutes + 0.5)));
						}
					}
					dateTime = key;
					flag = true;
				}
			}
			TimeZoneInfo timeZoneInfo;
			if (list2.Count == 0 && !flag2)
			{
				if (text == null)
				{
					TimeType timeType = dictionary2[0];
					text = timeType.Name;
					timeSpan = new TimeSpan(0, 0, timeType.Offset);
				}
				timeZoneInfo = TimeZoneInfo.CreateCustomTimeZone(id, timeSpan, id, text);
			}
			else
			{
				timeZoneInfo = TimeZoneInfo.CreateCustomTimeZone(id, timeSpan, id, text, text2, TimeZoneInfo.ValidateRules(list2));
			}
			if (flag2 && list.Count > 0)
			{
				timeZoneInfo.transitions = list;
			}
			timeZoneInfo.supportsDaylightSavingTime = list2.Count > 0;
			return timeZoneInfo;
		}

		// Token: 0x060014DD RID: 5341 RVA: 0x0005573C File Offset: 0x0005393C
		private static Dictionary<int, string> ParseAbbreviations(byte[] buffer, int index, int count)
		{
			Dictionary<int, string> dictionary = new Dictionary<int, string>();
			int num = 0;
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < count; i++)
			{
				char c = (char)buffer[index + i];
				if (c != '\0')
				{
					stringBuilder.Append(c);
				}
				else
				{
					dictionary.Add(num, stringBuilder.ToString());
					for (int j = 1; j <= stringBuilder.Length; j++)
					{
						dictionary.Add(num + j, stringBuilder.ToString(j, stringBuilder.Length - j));
					}
					num = i + 1;
					stringBuilder = new StringBuilder();
				}
			}
			return dictionary;
		}

		// Token: 0x060014DE RID: 5342 RVA: 0x000557C4 File Offset: 0x000539C4
		private static Dictionary<int, TimeType> ParseTimesTypes(byte[] buffer, int index, int count, Dictionary<int, string> abbreviations)
		{
			Dictionary<int, TimeType> dictionary = new Dictionary<int, TimeType>(count);
			for (int i = 0; i < count; i++)
			{
				int num = TimeZoneInfo.ReadBigEndianInt32(buffer, index + 6 * i);
				num = num / 60 * 60;
				byte b = buffer[index + 6 * i + 4];
				byte b2 = buffer[index + 6 * i + 5];
				dictionary.Add(i, new TimeType(num, b > 0, abbreviations[(int)b2]));
			}
			return dictionary;
		}

		// Token: 0x060014DF RID: 5343 RVA: 0x00055828 File Offset: 0x00053A28
		private static List<KeyValuePair<DateTime, TimeType>> ParseTransitions(byte[] buffer, int index, int count, Dictionary<int, TimeType> time_types)
		{
			List<KeyValuePair<DateTime, TimeType>> list = new List<KeyValuePair<DateTime, TimeType>>(count);
			for (int i = 0; i < count; i++)
			{
				DateTime dateTime = TimeZoneInfo.DateTimeFromUnixTime((long)TimeZoneInfo.ReadBigEndianInt32(buffer, index + 4 * i));
				byte b = buffer[index + 4 * count + i];
				list.Add(new KeyValuePair<DateTime, TimeType>(dateTime, time_types[(int)b]));
			}
			return list;
		}

		// Token: 0x060014E0 RID: 5344 RVA: 0x00055878 File Offset: 0x00053A78
		private static DateTime DateTimeFromUnixTime(long unix_time)
		{
			DateTime dateTime = new DateTime(1970, 1, 1);
			return dateTime.AddSeconds((double)unix_time);
		}

		// Token: 0x060014E1 RID: 5345 RVA: 0x0005589C File Offset: 0x00053A9C
		internal static TimeSpan GetLocalUtcOffset(DateTime dateTime, TimeZoneInfoOptions flags)
		{
			bool flag;
			return TimeZoneInfo.Local.GetUtcOffset(dateTime, out flag);
		}

		// Token: 0x060014E2 RID: 5346 RVA: 0x000558B8 File Offset: 0x00053AB8
		internal TimeSpan GetUtcOffset(DateTime dateTime, TimeZoneInfoOptions flags)
		{
			bool flag;
			return this.GetUtcOffset(dateTime, out flag);
		}

		// Token: 0x060014E3 RID: 5347 RVA: 0x000558CE File Offset: 0x00053ACE
		internal static TimeSpan GetUtcOffsetFromUtc(DateTime time, TimeZoneInfo zone, out bool isDaylightSavings, out bool isAmbiguousLocalDst)
		{
			isDaylightSavings = false;
			isAmbiguousLocalDst = false;
			TimeSpan timeSpan = zone.BaseUtcOffset;
			if (zone.IsAmbiguousTime(time))
			{
				isAmbiguousLocalDst = true;
			}
			return zone.GetUtcOffset(time, out isDaylightSavings);
		}

		// Token: 0x060014E4 RID: 5348 RVA: 0x0001FB35 File Offset: 0x0001DD35
		internal TimeZoneInfo()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04000B56 RID: 2902
		internal const uint TIME_ZONE_ID_INVALID = 4294967295U;

		// Token: 0x04000B57 RID: 2903
		internal const uint ERROR_NO_MORE_ITEMS = 259U;

		// Token: 0x04000B58 RID: 2904
		private TimeSpan baseUtcOffset;

		// Token: 0x04000B59 RID: 2905
		private string daylightDisplayName;

		// Token: 0x04000B5A RID: 2906
		private string displayName;

		// Token: 0x04000B5B RID: 2907
		private string id;

		// Token: 0x04000B5C RID: 2908
		private static TimeZoneInfo local;

		// Token: 0x04000B5D RID: 2909
		private List<KeyValuePair<DateTime, TimeType>> transitions;

		// Token: 0x04000B5E RID: 2910
		private static bool readlinkNotFound;

		// Token: 0x04000B5F RID: 2911
		private string standardDisplayName;

		// Token: 0x04000B60 RID: 2912
		private bool supportsDaylightSavingTime;

		// Token: 0x04000B61 RID: 2913
		private static TimeZoneInfo utc;

		// Token: 0x04000B62 RID: 2914
		private static string timeZoneDirectory;

		// Token: 0x04000B63 RID: 2915
		private TimeZoneInfo.AdjustmentRule[] adjustmentRules;

		// Token: 0x04000B64 RID: 2916
		private static RegistryKey timeZoneKey;

		// Token: 0x04000B65 RID: 2917
		private static RegistryKey localZoneKey;

		// Token: 0x04000B66 RID: 2918
		private static ReadOnlyCollection<TimeZoneInfo> systemTimeZones;

		// Token: 0x04000B67 RID: 2919
		private const int BUFFER_SIZE = 16384;

		/// <summary>Provides information about a time zone adjustment, such as the transition to and from daylight saving time.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x020001D1 RID: 465
		[TypeForwardedFrom("System.Core, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=b77a5c561934e089")]
		[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
		[Serializable]
		public sealed class AdjustmentRule : IEquatable<TimeZoneInfo.AdjustmentRule>, ISerializable, IDeserializationCallback
		{
			/// <summary>Gets the date when the adjustment rule takes effect.</summary>
			/// <returns>A <see cref="T:System.DateTime" /> value that indicates when the adjustment rule takes effect.</returns>
			// Token: 0x1700025B RID: 603
			// (get) Token: 0x060014E5 RID: 5349 RVA: 0x000558F1 File Offset: 0x00053AF1
			public DateTime DateStart
			{
				get
				{
					return this.m_dateStart;
				}
			}

			/// <summary>Gets the date when the adjustment rule ceases to be in effect.</summary>
			/// <returns>A <see cref="T:System.DateTime" /> value that indicates the end date of the adjustment rule.</returns>
			// Token: 0x1700025C RID: 604
			// (get) Token: 0x060014E6 RID: 5350 RVA: 0x000558F9 File Offset: 0x00053AF9
			public DateTime DateEnd
			{
				get
				{
					return this.m_dateEnd;
				}
			}

			/// <summary>Gets the amount of time that is required to form the time zone's daylight saving time. This amount of time is added to the time zone's offset from Coordinated Universal Time (UTC).</summary>
			/// <returns>A <see cref="T:System.TimeSpan" /> object that indicates the amount of time to add to the standard time changes as a result of the adjustment rule.</returns>
			// Token: 0x1700025D RID: 605
			// (get) Token: 0x060014E7 RID: 5351 RVA: 0x00055901 File Offset: 0x00053B01
			public TimeSpan DaylightDelta
			{
				get
				{
					return this.m_daylightDelta;
				}
			}

			/// <summary>Gets information about the annual transition from standard time to daylight saving time.</summary>
			/// <returns>A <see cref="T:System.TimeZoneInfo.TransitionTime" /> object that defines the annual transition from a time zone's standard time to daylight saving time.</returns>
			// Token: 0x1700025E RID: 606
			// (get) Token: 0x060014E8 RID: 5352 RVA: 0x00055909 File Offset: 0x00053B09
			public TimeZoneInfo.TransitionTime DaylightTransitionStart
			{
				get
				{
					return this.m_daylightTransitionStart;
				}
			}

			/// <summary>Gets information about the annual transition from daylight saving time back to standard time.</summary>
			/// <returns>A <see cref="T:System.TimeZoneInfo.TransitionTime" /> object that defines the annual transition from daylight saving time back to the time zone's standard time.</returns>
			// Token: 0x1700025F RID: 607
			// (get) Token: 0x060014E9 RID: 5353 RVA: 0x00055911 File Offset: 0x00053B11
			public TimeZoneInfo.TransitionTime DaylightTransitionEnd
			{
				get
				{
					return this.m_daylightTransitionEnd;
				}
			}

			// Token: 0x17000260 RID: 608
			// (get) Token: 0x060014EA RID: 5354 RVA: 0x00055919 File Offset: 0x00053B19
			internal TimeSpan BaseUtcOffsetDelta
			{
				get
				{
					return this.m_baseUtcOffsetDelta;
				}
			}

			// Token: 0x17000261 RID: 609
			// (get) Token: 0x060014EB RID: 5355 RVA: 0x00055924 File Offset: 0x00053B24
			internal bool HasDaylightSaving
			{
				get
				{
					return this.DaylightDelta != TimeSpan.Zero || this.DaylightTransitionStart.TimeOfDay != DateTime.MinValue || this.DaylightTransitionEnd.TimeOfDay != DateTime.MinValue.AddMilliseconds(1.0);
				}
			}

			/// <summary>Determines whether the current <see cref="T:System.TimeZoneInfo.AdjustmentRule" /> object is equal to a second <see cref="T:System.TimeZoneInfo.AdjustmentRule" /> object.</summary>
			/// <returns>true if both <see cref="T:System.TimeZoneInfo.AdjustmentRule" /> objects have equal values; otherwise, false.</returns>
			/// <param name="other">The object to compare with the current object.</param>
			/// <filterpriority>2</filterpriority>
			// Token: 0x060014EC RID: 5356 RVA: 0x00055988 File Offset: 0x00053B88
			public bool Equals(TimeZoneInfo.AdjustmentRule other)
			{
				return other != null && this.m_dateStart == other.m_dateStart && this.m_dateEnd == other.m_dateEnd && this.m_daylightDelta == other.m_daylightDelta && this.m_baseUtcOffsetDelta == other.m_baseUtcOffsetDelta && this.m_daylightTransitionEnd.Equals(other.m_daylightTransitionEnd) && this.m_daylightTransitionStart.Equals(other.m_daylightTransitionStart);
			}

			/// <summary>Serves as a hash function for hashing algorithms and data structures such as hash tables.</summary>
			/// <returns>A 32-bit signed integer that serves as the hash code for the current <see cref="T:System.TimeZoneInfo.AdjustmentRule" /> object.</returns>
			/// <filterpriority>2</filterpriority>
			// Token: 0x060014ED RID: 5357 RVA: 0x00055A0D File Offset: 0x00053C0D
			public override int GetHashCode()
			{
				return this.m_dateStart.GetHashCode();
			}

			// Token: 0x060014EE RID: 5358 RVA: 0x00002111 File Offset: 0x00000311
			private AdjustmentRule()
			{
			}

			/// <summary>Creates a new adjustment rule for a particular time zone.</summary>
			/// <returns>An object that represents the new adjustment rule.</returns>
			/// <param name="dateStart">The effective date of the adjustment rule. If the value of the <paramref name="dateStart" /> parameter is DateTime.MinValue.Date, this is the first adjustment rule in effect for a time zone.   </param>
			/// <param name="dateEnd">The last date that the adjustment rule is in force. If the value of the <paramref name="dateEnd" /> parameter is DateTime.MaxValue.Date, the adjustment rule has no end date.</param>
			/// <param name="daylightDelta">The time change that results from the adjustment. This value is added to the time zone's <see cref="P:System.TimeZoneInfo.BaseUtcOffset" /> property to obtain the correct daylight offset from Coordinated Universal Time (UTC). This value can range from -14 to 14. </param>
			/// <param name="daylightTransitionStart">An object that defines the start of daylight saving time.</param>
			/// <param name="daylightTransitionEnd">An object that defines the end of daylight saving time.   </param>
			/// <exception cref="T:System.ArgumentException">The <see cref="P:System.DateTime.Kind" /> property of the <paramref name="dateStart" /> or <paramref name="dateEnd" /> parameter does not equal <see cref="F:System.DateTimeKind.Unspecified" />.-or-The <paramref name="daylightTransitionStart" /> parameter is equal to the <paramref name="daylightTransitionEnd" /> parameter.-or-The <paramref name="dateStart" /> or <paramref name="dateEnd" /> parameter includes a time of day value.</exception>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			///   <paramref name="dateEnd" /> is earlier than <paramref name="dateStart" />.-or-<paramref name="daylightDelta" /> is less than -14 or greater than 14.-or-The <see cref="P:System.TimeSpan.Milliseconds" /> property of the <paramref name="daylightDelta" /> parameter is not equal to 0.-or-The <see cref="P:System.TimeSpan.Ticks" /> property of the <paramref name="daylightDelta" /> parameter does not equal a whole number of seconds.</exception>
			// Token: 0x060014EF RID: 5359 RVA: 0x00055A1C File Offset: 0x00053C1C
			public static TimeZoneInfo.AdjustmentRule CreateAdjustmentRule(DateTime dateStart, DateTime dateEnd, TimeSpan daylightDelta, TimeZoneInfo.TransitionTime daylightTransitionStart, TimeZoneInfo.TransitionTime daylightTransitionEnd)
			{
				TimeZoneInfo.AdjustmentRule.ValidateAdjustmentRule(dateStart, dateEnd, daylightDelta, daylightTransitionStart, daylightTransitionEnd);
				return new TimeZoneInfo.AdjustmentRule
				{
					m_dateStart = dateStart,
					m_dateEnd = dateEnd,
					m_daylightDelta = daylightDelta,
					m_daylightTransitionStart = daylightTransitionStart,
					m_daylightTransitionEnd = daylightTransitionEnd,
					m_baseUtcOffsetDelta = TimeSpan.Zero
				};
			}

			// Token: 0x060014F0 RID: 5360 RVA: 0x00055A68 File Offset: 0x00053C68
			internal static TimeZoneInfo.AdjustmentRule CreateAdjustmentRule(DateTime dateStart, DateTime dateEnd, TimeSpan daylightDelta, TimeZoneInfo.TransitionTime daylightTransitionStart, TimeZoneInfo.TransitionTime daylightTransitionEnd, TimeSpan baseUtcOffsetDelta)
			{
				TimeZoneInfo.AdjustmentRule adjustmentRule = TimeZoneInfo.AdjustmentRule.CreateAdjustmentRule(dateStart, dateEnd, daylightDelta, daylightTransitionStart, daylightTransitionEnd);
				adjustmentRule.m_baseUtcOffsetDelta = baseUtcOffsetDelta;
				return adjustmentRule;
			}

			// Token: 0x060014F1 RID: 5361 RVA: 0x00055A80 File Offset: 0x00053C80
			internal bool IsStartDateMarkerForBeginningOfYear()
			{
				return this.DaylightTransitionStart.Month == 1 && this.DaylightTransitionStart.Day == 1 && this.DaylightTransitionStart.TimeOfDay.Hour == 0 && this.DaylightTransitionStart.TimeOfDay.Minute == 0 && this.DaylightTransitionStart.TimeOfDay.Second == 0 && this.m_dateStart.Year == this.m_dateEnd.Year;
			}

			// Token: 0x060014F2 RID: 5362 RVA: 0x00055B14 File Offset: 0x00053D14
			internal bool IsEndDateMarkerForEndOfYear()
			{
				return this.DaylightTransitionEnd.Month == 1 && this.DaylightTransitionEnd.Day == 1 && this.DaylightTransitionEnd.TimeOfDay.Hour == 0 && this.DaylightTransitionEnd.TimeOfDay.Minute == 0 && this.DaylightTransitionEnd.TimeOfDay.Second == 0 && this.m_dateStart.Year == this.m_dateEnd.Year;
			}

			// Token: 0x060014F3 RID: 5363 RVA: 0x00055BA8 File Offset: 0x00053DA8
			private static void ValidateAdjustmentRule(DateTime dateStart, DateTime dateEnd, TimeSpan daylightDelta, TimeZoneInfo.TransitionTime daylightTransitionStart, TimeZoneInfo.TransitionTime daylightTransitionEnd)
			{
				if (dateStart.Kind != DateTimeKind.Unspecified)
				{
					throw new ArgumentException(Environment.GetResourceString("The supplied DateTime must have the Kind property set to DateTimeKind.Unspecified."), "dateStart");
				}
				if (dateEnd.Kind != DateTimeKind.Unspecified)
				{
					throw new ArgumentException(Environment.GetResourceString("The supplied DateTime must have the Kind property set to DateTimeKind.Unspecified."), "dateEnd");
				}
				if (daylightTransitionStart.Equals(daylightTransitionEnd))
				{
					throw new ArgumentException(Environment.GetResourceString("The DaylightTransitionStart property must not equal the DaylightTransitionEnd property."), "daylightTransitionEnd");
				}
				if (dateStart > dateEnd)
				{
					throw new ArgumentException(Environment.GetResourceString("The DateStart property must come before the DateEnd property."), "dateStart");
				}
				if (TimeZoneInfo.UtcOffsetOutOfRange(daylightDelta))
				{
					throw new ArgumentOutOfRangeException("daylightDelta", daylightDelta, Environment.GetResourceString("The TimeSpan parameter must be within plus or minus 14.0 hours."));
				}
				if (daylightDelta.Ticks % 600000000L != 0L)
				{
					throw new ArgumentException(Environment.GetResourceString("The TimeSpan parameter cannot be specified more precisely than whole minutes."), "daylightDelta");
				}
				if (dateStart.TimeOfDay != TimeSpan.Zero)
				{
					throw new ArgumentException(Environment.GetResourceString("The supplied DateTime includes a TimeOfDay setting.   This is not supported."), "dateStart");
				}
				if (dateEnd.TimeOfDay != TimeSpan.Zero)
				{
					throw new ArgumentException(Environment.GetResourceString("The supplied DateTime includes a TimeOfDay setting.   This is not supported."), "dateEnd");
				}
			}

			/// <summary>Runs when the deserialization of a <see cref="T:System.TimeZoneInfo.AdjustmentRule" /> object is completed.</summary>
			/// <param name="sender">The object that initiated the callback. The functionality for this parameter is not currently implemented.   </param>
			// Token: 0x060014F4 RID: 5364 RVA: 0x00055CC8 File Offset: 0x00053EC8
			void IDeserializationCallback.OnDeserialization(object sender)
			{
				try
				{
					TimeZoneInfo.AdjustmentRule.ValidateAdjustmentRule(this.m_dateStart, this.m_dateEnd, this.m_daylightDelta, this.m_daylightTransitionStart, this.m_daylightTransitionEnd);
				}
				catch (ArgumentException ex)
				{
					throw new SerializationException(Environment.GetResourceString("An error occurred while deserializing the object.  The serialized data is corrupt."), ex);
				}
			}

			/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the data that is required to serialize this object.</summary>
			/// <param name="info">The object to populate with data.</param>
			/// <param name="context">The destination for this serialization (see <see cref="T:System.Runtime.Serialization.StreamingContext" />).</param>
			// Token: 0x060014F5 RID: 5365 RVA: 0x00055D1C File Offset: 0x00053F1C
			[SecurityCritical]
			void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
			{
				if (info == null)
				{
					throw new ArgumentNullException("info");
				}
				info.AddValue("DateStart", this.m_dateStart);
				info.AddValue("DateEnd", this.m_dateEnd);
				info.AddValue("DaylightDelta", this.m_daylightDelta);
				info.AddValue("DaylightTransitionStart", this.m_daylightTransitionStart);
				info.AddValue("DaylightTransitionEnd", this.m_daylightTransitionEnd);
				info.AddValue("BaseUtcOffsetDelta", this.m_baseUtcOffsetDelta);
			}

			// Token: 0x060014F6 RID: 5366 RVA: 0x00055DB4 File Offset: 0x00053FB4
			private AdjustmentRule(SerializationInfo info, StreamingContext context)
			{
				if (info == null)
				{
					throw new ArgumentNullException("info");
				}
				this.m_dateStart = (DateTime)info.GetValue("DateStart", typeof(DateTime));
				this.m_dateEnd = (DateTime)info.GetValue("DateEnd", typeof(DateTime));
				this.m_daylightDelta = (TimeSpan)info.GetValue("DaylightDelta", typeof(TimeSpan));
				this.m_daylightTransitionStart = (TimeZoneInfo.TransitionTime)info.GetValue("DaylightTransitionStart", typeof(TimeZoneInfo.TransitionTime));
				this.m_daylightTransitionEnd = (TimeZoneInfo.TransitionTime)info.GetValue("DaylightTransitionEnd", typeof(TimeZoneInfo.TransitionTime));
				object valueNoThrow = info.GetValueNoThrow("BaseUtcOffsetDelta", typeof(TimeSpan));
				if (valueNoThrow != null)
				{
					this.m_baseUtcOffsetDelta = (TimeSpan)valueNoThrow;
				}
			}

			// Token: 0x04000B68 RID: 2920
			private DateTime m_dateStart;

			// Token: 0x04000B69 RID: 2921
			private DateTime m_dateEnd;

			// Token: 0x04000B6A RID: 2922
			private TimeSpan m_daylightDelta;

			// Token: 0x04000B6B RID: 2923
			private TimeZoneInfo.TransitionTime m_daylightTransitionStart;

			// Token: 0x04000B6C RID: 2924
			private TimeZoneInfo.TransitionTime m_daylightTransitionEnd;

			// Token: 0x04000B6D RID: 2925
			private TimeSpan m_baseUtcOffsetDelta;
		}

		/// <summary>Provides information about a specific time change, such as the change from daylight saving time to standard time or vice versa, in a particular time zone.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x020001D2 RID: 466
		[TypeForwardedFrom("System.Core, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=b77a5c561934e089")]
		[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
		[Serializable]
		public struct TransitionTime : IEquatable<TimeZoneInfo.TransitionTime>, ISerializable, IDeserializationCallback
		{
			/// <summary>Gets the hour, minute, and second at which the time change occurs.</summary>
			/// <returns>The time of day at which the time change occurs.</returns>
			// Token: 0x17000262 RID: 610
			// (get) Token: 0x060014F7 RID: 5367 RVA: 0x00055E9A File Offset: 0x0005409A
			public DateTime TimeOfDay
			{
				get
				{
					return this.m_timeOfDay;
				}
			}

			/// <summary>Gets the month in which the time change occurs.</summary>
			/// <returns>The month in which the time change occurs.</returns>
			// Token: 0x17000263 RID: 611
			// (get) Token: 0x060014F8 RID: 5368 RVA: 0x00055EA2 File Offset: 0x000540A2
			public int Month
			{
				get
				{
					return (int)this.m_month;
				}
			}

			/// <summary>Gets the week of the month in which a time change occurs.</summary>
			/// <returns>The week of the month in which the time change occurs.</returns>
			// Token: 0x17000264 RID: 612
			// (get) Token: 0x060014F9 RID: 5369 RVA: 0x00055EAA File Offset: 0x000540AA
			public int Week
			{
				get
				{
					return (int)this.m_week;
				}
			}

			/// <summary>Gets the day on which the time change occurs.</summary>
			/// <returns>The day on which the time change occurs.</returns>
			// Token: 0x17000265 RID: 613
			// (get) Token: 0x060014FA RID: 5370 RVA: 0x00055EB2 File Offset: 0x000540B2
			public int Day
			{
				get
				{
					return (int)this.m_day;
				}
			}

			/// <summary>Gets the day of the week on which the time change occurs.</summary>
			/// <returns>The day of the week on which the time change occurs.</returns>
			// Token: 0x17000266 RID: 614
			// (get) Token: 0x060014FB RID: 5371 RVA: 0x00055EBA File Offset: 0x000540BA
			public DayOfWeek DayOfWeek
			{
				get
				{
					return this.m_dayOfWeek;
				}
			}

			/// <summary>Gets a value indicating whether the time change occurs at a fixed date and time (such as November 1) or a floating date and time (such as the last Sunday of October).</summary>
			/// <returns>true if the time change rule is fixed-date; false if the time change rule is floating-date.</returns>
			// Token: 0x17000267 RID: 615
			// (get) Token: 0x060014FC RID: 5372 RVA: 0x00055EC2 File Offset: 0x000540C2
			public bool IsFixedDateRule
			{
				get
				{
					return this.m_isFixedDateRule;
				}
			}

			/// <summary>Determines whether an object has identical values to the current <see cref="T:System.TimeZoneInfo.TransitionTime" /> object.</summary>
			/// <returns>true if the two objects are equal; otherwise, false.</returns>
			/// <param name="obj">An object to compare with the current <see cref="T:System.TimeZoneInfo.TransitionTime" /> object.   </param>
			/// <filterpriority>2</filterpriority>
			// Token: 0x060014FD RID: 5373 RVA: 0x00055ECA File Offset: 0x000540CA
			public override bool Equals(object obj)
			{
				return obj is TimeZoneInfo.TransitionTime && this.Equals((TimeZoneInfo.TransitionTime)obj);
			}

			/// <summary>Determines whether two specified <see cref="T:System.TimeZoneInfo.TransitionTime" /> objects are equal.</summary>
			/// <returns>true if <paramref name="t1" /> and <paramref name="t2" /> have identical values; otherwise, false. </returns>
			/// <param name="t1">The first object to compare.</param>
			/// <param name="t2">The second object to compare.</param>
			// Token: 0x060014FE RID: 5374 RVA: 0x00055EE2 File Offset: 0x000540E2
			public static bool operator ==(TimeZoneInfo.TransitionTime t1, TimeZoneInfo.TransitionTime t2)
			{
				return t1.Equals(t2);
			}

			/// <summary>Determines whether two specified <see cref="T:System.TimeZoneInfo.TransitionTime" /> objects are not equal.</summary>
			/// <returns>true if <paramref name="t1" /> and <paramref name="t2" /> have any different member values; otherwise, false.</returns>
			/// <param name="t1">The first object to compare.</param>
			/// <param name="t2">The second object to compare.</param>
			// Token: 0x060014FF RID: 5375 RVA: 0x00055EEC File Offset: 0x000540EC
			public static bool operator !=(TimeZoneInfo.TransitionTime t1, TimeZoneInfo.TransitionTime t2)
			{
				return !t1.Equals(t2);
			}

			/// <summary>Determines whether the current <see cref="T:System.TimeZoneInfo.TransitionTime" /> object has identical values to a second <see cref="T:System.TimeZoneInfo.TransitionTime" /> object.</summary>
			/// <returns>true if the two objects have identical property values; otherwise, false.</returns>
			/// <param name="other">An object to compare to the current instance. </param>
			/// <filterpriority>2</filterpriority>
			// Token: 0x06001500 RID: 5376 RVA: 0x00055EFC File Offset: 0x000540FC
			public bool Equals(TimeZoneInfo.TransitionTime other)
			{
				bool flag = this.m_isFixedDateRule == other.m_isFixedDateRule && this.m_timeOfDay == other.m_timeOfDay && this.m_month == other.m_month;
				if (flag)
				{
					if (other.m_isFixedDateRule)
					{
						flag = this.m_day == other.m_day;
					}
					else
					{
						flag = this.m_week == other.m_week && this.m_dayOfWeek == other.m_dayOfWeek;
					}
				}
				return flag;
			}

			/// <summary>Serves as a hash function for hashing algorithms and data structures such as hash tables.</summary>
			/// <returns>A 32-bit signed integer that serves as the hash code for this <see cref="T:System.TimeZoneInfo.TransitionTime" /> object.</returns>
			/// <filterpriority>2</filterpriority>
			// Token: 0x06001501 RID: 5377 RVA: 0x00055F79 File Offset: 0x00054179
			public override int GetHashCode()
			{
				return (int)this.m_month ^ ((int)this.m_week << 8);
			}

			/// <summary>Defines a time change that uses a fixed-date rule.</summary>
			/// <returns>Data about the time change.</returns>
			/// <param name="timeOfDay">The time at which the time change occurs.</param>
			/// <param name="month">The month in which the time change occurs.</param>
			/// <param name="day">The day of the month on which the time change occurs.</param>
			/// <exception cref="T:System.ArgumentException">The <paramref name="timeOfDay" /> parameter has a non-default date component.-or-The <paramref name="timeOfDay" /> parameter's <see cref="P:System.DateTime.Kind" /> property is not <see cref="F:System.DateTimeKind.Unspecified" />.-or-The <paramref name="timeOfDay" /> parameter does not represent a whole number of milliseconds.</exception>
			/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="month" /> parameter is less than 1 or greater than 12.-or-The <paramref name="day" /> parameter is less than 1 or greater than 31.</exception>
			// Token: 0x06001502 RID: 5378 RVA: 0x00055F8A File Offset: 0x0005418A
			public static TimeZoneInfo.TransitionTime CreateFixedDateRule(DateTime timeOfDay, int month, int day)
			{
				return TimeZoneInfo.TransitionTime.CreateTransitionTime(timeOfDay, month, 1, day, DayOfWeek.Sunday, true);
			}

			/// <summary>Defines a time change that uses a floating-date rule.</summary>
			/// <returns>Data about the time change.</returns>
			/// <param name="timeOfDay">The time at which the time change occurs.</param>
			/// <param name="month">The month in which the time change occurs.</param>
			/// <param name="week">The week of the month in which the time change occurs.</param>
			/// <param name="dayOfWeek">The day of the week on which the time change occurs.</param>
			/// <exception cref="T:System.ArgumentException">The <paramref name="timeOfDay" /> parameter has a non-default date component.-or-The <paramref name="timeOfDay" /> parameter does not represent a whole number of milliseconds.-or-The <paramref name="timeOfDay" /> parameter's <see cref="P:System.DateTime.Kind" /> property is not <see cref="F:System.DateTimeKind.Unspecified" />.</exception>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			///   <paramref name="month" /> is less than 1 or greater than 12.-or-<paramref name="week" /> is less than 1 or greater than 5.-or-The <paramref name="dayOfWeek" /> parameter is not a member of the <see cref="T:System.DayOfWeek" /> enumeration.</exception>
			// Token: 0x06001503 RID: 5379 RVA: 0x00055F97 File Offset: 0x00054197
			public static TimeZoneInfo.TransitionTime CreateFloatingDateRule(DateTime timeOfDay, int month, int week, DayOfWeek dayOfWeek)
			{
				return TimeZoneInfo.TransitionTime.CreateTransitionTime(timeOfDay, month, week, 1, dayOfWeek, false);
			}

			// Token: 0x06001504 RID: 5380 RVA: 0x00055FA4 File Offset: 0x000541A4
			private static TimeZoneInfo.TransitionTime CreateTransitionTime(DateTime timeOfDay, int month, int week, int day, DayOfWeek dayOfWeek, bool isFixedDateRule)
			{
				TimeZoneInfo.TransitionTime.ValidateTransitionTime(timeOfDay, month, week, day, dayOfWeek);
				return new TimeZoneInfo.TransitionTime
				{
					m_isFixedDateRule = isFixedDateRule,
					m_timeOfDay = timeOfDay,
					m_dayOfWeek = dayOfWeek,
					m_day = (byte)day,
					m_week = (byte)week,
					m_month = (byte)month
				};
			}

			// Token: 0x06001505 RID: 5381 RVA: 0x00055FFC File Offset: 0x000541FC
			private static void ValidateTransitionTime(DateTime timeOfDay, int month, int week, int day, DayOfWeek dayOfWeek)
			{
				if (timeOfDay.Kind != DateTimeKind.Unspecified)
				{
					throw new ArgumentException(Environment.GetResourceString("The supplied DateTime must have the Kind property set to DateTimeKind.Unspecified."), "timeOfDay");
				}
				if (month < 1 || month > 12)
				{
					throw new ArgumentOutOfRangeException("month", Environment.GetResourceString("The Month parameter must be in the range 1 through 12."));
				}
				if (day < 1 || day > 31)
				{
					throw new ArgumentOutOfRangeException("day", Environment.GetResourceString("The Day parameter must be in the range 1 through 31."));
				}
				if (week < 1 || week > 5)
				{
					throw new ArgumentOutOfRangeException("week", Environment.GetResourceString("The Week parameter must be in the range 1 through 5."));
				}
				if (dayOfWeek < DayOfWeek.Sunday || dayOfWeek > DayOfWeek.Saturday)
				{
					throw new ArgumentOutOfRangeException("dayOfWeek", Environment.GetResourceString("The DayOfWeek enumeration must be in the range 0 through 6."));
				}
				if (timeOfDay.Year != 1 || timeOfDay.Month != 1 || timeOfDay.Day != 1 || timeOfDay.Ticks % 10000L != 0L)
				{
					throw new ArgumentException(Environment.GetResourceString("The supplied DateTime must have the Year, Month, and Day properties set to 1.  The time cannot be specified more precisely than whole milliseconds."), "timeOfDay");
				}
			}

			/// <summary>Runs when the deserialization of an object has been completed.</summary>
			/// <param name="sender">The object that initiated the callback. The functionality for this parameter is not currently implemented.   </param>
			// Token: 0x06001506 RID: 5382 RVA: 0x000560E4 File Offset: 0x000542E4
			void IDeserializationCallback.OnDeserialization(object sender)
			{
				try
				{
					TimeZoneInfo.TransitionTime.ValidateTransitionTime(this.m_timeOfDay, (int)this.m_month, (int)this.m_week, (int)this.m_day, this.m_dayOfWeek);
				}
				catch (ArgumentException ex)
				{
					throw new SerializationException(Environment.GetResourceString("An error occurred while deserializing the object.  The serialized data is corrupt."), ex);
				}
			}

			/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the data that is required to serialize this object.</summary>
			/// <param name="info">The object to populate with data.</param>
			/// <param name="context">The destination for this serialization (see <see cref="T:System.Runtime.Serialization.StreamingContext" />).</param>
			// Token: 0x06001507 RID: 5383 RVA: 0x00056138 File Offset: 0x00054338
			[SecurityCritical]
			void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
			{
				if (info == null)
				{
					throw new ArgumentNullException("info");
				}
				info.AddValue("TimeOfDay", this.m_timeOfDay);
				info.AddValue("Month", this.m_month);
				info.AddValue("Week", this.m_week);
				info.AddValue("Day", this.m_day);
				info.AddValue("DayOfWeek", this.m_dayOfWeek);
				info.AddValue("IsFixedDateRule", this.m_isFixedDateRule);
			}

			// Token: 0x06001508 RID: 5384 RVA: 0x000561C0 File Offset: 0x000543C0
			private TransitionTime(SerializationInfo info, StreamingContext context)
			{
				if (info == null)
				{
					throw new ArgumentNullException("info");
				}
				this.m_timeOfDay = (DateTime)info.GetValue("TimeOfDay", typeof(DateTime));
				this.m_month = (byte)info.GetValue("Month", typeof(byte));
				this.m_week = (byte)info.GetValue("Week", typeof(byte));
				this.m_day = (byte)info.GetValue("Day", typeof(byte));
				this.m_dayOfWeek = (DayOfWeek)info.GetValue("DayOfWeek", typeof(DayOfWeek));
				this.m_isFixedDateRule = (bool)info.GetValue("IsFixedDateRule", typeof(bool));
			}

			// Token: 0x04000B6E RID: 2926
			private DateTime m_timeOfDay;

			// Token: 0x04000B6F RID: 2927
			private byte m_month;

			// Token: 0x04000B70 RID: 2928
			private byte m_week;

			// Token: 0x04000B71 RID: 2929
			private byte m_day;

			// Token: 0x04000B72 RID: 2930
			private DayOfWeek m_dayOfWeek;

			// Token: 0x04000B73 RID: 2931
			private bool m_isFixedDateRule;
		}

		// Token: 0x020001D3 RID: 467
		private sealed class StringSerializer
		{
			// Token: 0x06001509 RID: 5385 RVA: 0x0005629C File Offset: 0x0005449C
			public static string GetSerializedString(TimeZoneInfo zone)
			{
				StringBuilder stringBuilder = StringBuilderCache.Acquire(16);
				stringBuilder.Append(TimeZoneInfo.StringSerializer.SerializeSubstitute(zone.Id));
				stringBuilder.Append(';');
				stringBuilder.Append(TimeZoneInfo.StringSerializer.SerializeSubstitute(zone.BaseUtcOffset.TotalMinutes.ToString(CultureInfo.InvariantCulture)));
				stringBuilder.Append(';');
				stringBuilder.Append(TimeZoneInfo.StringSerializer.SerializeSubstitute(zone.DisplayName));
				stringBuilder.Append(';');
				stringBuilder.Append(TimeZoneInfo.StringSerializer.SerializeSubstitute(zone.StandardName));
				stringBuilder.Append(';');
				stringBuilder.Append(TimeZoneInfo.StringSerializer.SerializeSubstitute(zone.DaylightName));
				stringBuilder.Append(';');
				TimeZoneInfo.AdjustmentRule[] adjustmentRules = zone.GetAdjustmentRules();
				if (adjustmentRules != null && adjustmentRules.Length != 0)
				{
					foreach (TimeZoneInfo.AdjustmentRule adjustmentRule in adjustmentRules)
					{
						stringBuilder.Append('[');
						stringBuilder.Append(TimeZoneInfo.StringSerializer.SerializeSubstitute(adjustmentRule.DateStart.ToString("MM:dd:yyyy", DateTimeFormatInfo.InvariantInfo)));
						stringBuilder.Append(';');
						stringBuilder.Append(TimeZoneInfo.StringSerializer.SerializeSubstitute(adjustmentRule.DateEnd.ToString("MM:dd:yyyy", DateTimeFormatInfo.InvariantInfo)));
						stringBuilder.Append(';');
						stringBuilder.Append(TimeZoneInfo.StringSerializer.SerializeSubstitute(adjustmentRule.DaylightDelta.TotalMinutes.ToString(CultureInfo.InvariantCulture)));
						stringBuilder.Append(';');
						TimeZoneInfo.StringSerializer.SerializeTransitionTime(adjustmentRule.DaylightTransitionStart, stringBuilder);
						stringBuilder.Append(';');
						TimeZoneInfo.StringSerializer.SerializeTransitionTime(adjustmentRule.DaylightTransitionEnd, stringBuilder);
						stringBuilder.Append(';');
						if (adjustmentRule.BaseUtcOffsetDelta != TimeSpan.Zero)
						{
							stringBuilder.Append(TimeZoneInfo.StringSerializer.SerializeSubstitute(adjustmentRule.BaseUtcOffsetDelta.TotalMinutes.ToString(CultureInfo.InvariantCulture)));
							stringBuilder.Append(';');
						}
						stringBuilder.Append(']');
					}
				}
				stringBuilder.Append(';');
				return StringBuilderCache.GetStringAndRelease(stringBuilder);
			}

			// Token: 0x0600150A RID: 5386 RVA: 0x000564A0 File Offset: 0x000546A0
			public static TimeZoneInfo GetDeserializedTimeZoneInfo(string source)
			{
				TimeZoneInfo.StringSerializer stringSerializer = new TimeZoneInfo.StringSerializer(source);
				string nextStringValue = stringSerializer.GetNextStringValue(false);
				TimeSpan nextTimeSpanValue = stringSerializer.GetNextTimeSpanValue(false);
				string nextStringValue2 = stringSerializer.GetNextStringValue(false);
				string nextStringValue3 = stringSerializer.GetNextStringValue(false);
				string nextStringValue4 = stringSerializer.GetNextStringValue(false);
				TimeZoneInfo.AdjustmentRule[] nextAdjustmentRuleArrayValue = stringSerializer.GetNextAdjustmentRuleArrayValue(false);
				TimeZoneInfo timeZoneInfo;
				try
				{
					timeZoneInfo = TimeZoneInfo.CreateCustomTimeZone(nextStringValue, nextTimeSpanValue, nextStringValue2, nextStringValue3, nextStringValue4, nextAdjustmentRuleArrayValue);
				}
				catch (ArgumentException ex)
				{
					throw new SerializationException(Environment.GetResourceString("An error occurred while deserializing the object.  The serialized data is corrupt."), ex);
				}
				catch (InvalidTimeZoneException ex2)
				{
					throw new SerializationException(Environment.GetResourceString("An error occurred while deserializing the object.  The serialized data is corrupt."), ex2);
				}
				return timeZoneInfo;
			}

			// Token: 0x0600150B RID: 5387 RVA: 0x0005653C File Offset: 0x0005473C
			private StringSerializer(string str)
			{
				this.m_serializedText = str;
				this.m_state = TimeZoneInfo.StringSerializer.State.StartOfToken;
			}

			// Token: 0x0600150C RID: 5388 RVA: 0x00056554 File Offset: 0x00054754
			private static string SerializeSubstitute(string text)
			{
				text = text.Replace("\\", "\\\\");
				text = text.Replace("[", "\\[");
				text = text.Replace("]", "\\]");
				return text.Replace(";", "\\;");
			}

			// Token: 0x0600150D RID: 5389 RVA: 0x000565A8 File Offset: 0x000547A8
			private static void SerializeTransitionTime(TimeZoneInfo.TransitionTime time, StringBuilder serializedText)
			{
				serializedText.Append('[');
				serializedText.Append((time.IsFixedDateRule ? 1 : 0).ToString(CultureInfo.InvariantCulture));
				serializedText.Append(';');
				if (time.IsFixedDateRule)
				{
					serializedText.Append(TimeZoneInfo.StringSerializer.SerializeSubstitute(time.TimeOfDay.ToString("HH:mm:ss.FFF", DateTimeFormatInfo.InvariantInfo)));
					serializedText.Append(';');
					serializedText.Append(TimeZoneInfo.StringSerializer.SerializeSubstitute(time.Month.ToString(CultureInfo.InvariantCulture)));
					serializedText.Append(';');
					serializedText.Append(TimeZoneInfo.StringSerializer.SerializeSubstitute(time.Day.ToString(CultureInfo.InvariantCulture)));
					serializedText.Append(';');
				}
				else
				{
					serializedText.Append(TimeZoneInfo.StringSerializer.SerializeSubstitute(time.TimeOfDay.ToString("HH:mm:ss.FFF", DateTimeFormatInfo.InvariantInfo)));
					serializedText.Append(';');
					serializedText.Append(TimeZoneInfo.StringSerializer.SerializeSubstitute(time.Month.ToString(CultureInfo.InvariantCulture)));
					serializedText.Append(';');
					serializedText.Append(TimeZoneInfo.StringSerializer.SerializeSubstitute(time.Week.ToString(CultureInfo.InvariantCulture)));
					serializedText.Append(';');
					serializedText.Append(TimeZoneInfo.StringSerializer.SerializeSubstitute(((int)time.DayOfWeek).ToString(CultureInfo.InvariantCulture)));
					serializedText.Append(';');
				}
				serializedText.Append(']');
			}

			// Token: 0x0600150E RID: 5390 RVA: 0x0005672B File Offset: 0x0005492B
			private static void VerifyIsEscapableCharacter(char c)
			{
				if (c != '\\' && c != ';' && c != '[' && c != ']')
				{
					throw new SerializationException(Environment.GetResourceString("The serialized data contained an invalid escape sequence '\\\\{0}'.", new object[] { c }));
				}
			}

			// Token: 0x0600150F RID: 5391 RVA: 0x00056760 File Offset: 0x00054960
			private void SkipVersionNextDataFields(int depth)
			{
				if (this.m_currentTokenStartIndex < 0 || this.m_currentTokenStartIndex >= this.m_serializedText.Length)
				{
					throw new SerializationException(Environment.GetResourceString("An error occurred while deserializing the object.  The serialized data is corrupt."));
				}
				TimeZoneInfo.StringSerializer.State state = TimeZoneInfo.StringSerializer.State.NotEscaped;
				for (int i = this.m_currentTokenStartIndex; i < this.m_serializedText.Length; i++)
				{
					if (state == TimeZoneInfo.StringSerializer.State.Escaped)
					{
						TimeZoneInfo.StringSerializer.VerifyIsEscapableCharacter(this.m_serializedText[i]);
						state = TimeZoneInfo.StringSerializer.State.NotEscaped;
					}
					else if (state == TimeZoneInfo.StringSerializer.State.NotEscaped)
					{
						char c = this.m_serializedText[i];
						if (c == '\0')
						{
							throw new SerializationException(Environment.GetResourceString("An error occurred while deserializing the object.  The serialized data is corrupt."));
						}
						switch (c)
						{
						case '[':
							depth++;
							break;
						case '\\':
							state = TimeZoneInfo.StringSerializer.State.Escaped;
							break;
						case ']':
							depth--;
							if (depth == 0)
							{
								this.m_currentTokenStartIndex = i + 1;
								if (this.m_currentTokenStartIndex >= this.m_serializedText.Length)
								{
									this.m_state = TimeZoneInfo.StringSerializer.State.EndOfLine;
									return;
								}
								this.m_state = TimeZoneInfo.StringSerializer.State.StartOfToken;
								return;
							}
							break;
						}
					}
				}
				throw new SerializationException(Environment.GetResourceString("An error occurred while deserializing the object.  The serialized data is corrupt."));
			}

			// Token: 0x06001510 RID: 5392 RVA: 0x00056860 File Offset: 0x00054A60
			private string GetNextStringValue(bool canEndWithoutSeparator)
			{
				if (this.m_state == TimeZoneInfo.StringSerializer.State.EndOfLine)
				{
					if (canEndWithoutSeparator)
					{
						return null;
					}
					throw new SerializationException(Environment.GetResourceString("An error occurred while deserializing the object.  The serialized data is corrupt."));
				}
				else
				{
					if (this.m_currentTokenStartIndex < 0 || this.m_currentTokenStartIndex >= this.m_serializedText.Length)
					{
						throw new SerializationException(Environment.GetResourceString("An error occurred while deserializing the object.  The serialized data is corrupt."));
					}
					TimeZoneInfo.StringSerializer.State state = TimeZoneInfo.StringSerializer.State.NotEscaped;
					StringBuilder stringBuilder = StringBuilderCache.Acquire(64);
					for (int i = this.m_currentTokenStartIndex; i < this.m_serializedText.Length; i++)
					{
						if (state == TimeZoneInfo.StringSerializer.State.Escaped)
						{
							TimeZoneInfo.StringSerializer.VerifyIsEscapableCharacter(this.m_serializedText[i]);
							stringBuilder.Append(this.m_serializedText[i]);
							state = TimeZoneInfo.StringSerializer.State.NotEscaped;
						}
						else if (state == TimeZoneInfo.StringSerializer.State.NotEscaped)
						{
							char c = this.m_serializedText[i];
							if (c == '\0')
							{
								throw new SerializationException(Environment.GetResourceString("An error occurred while deserializing the object.  The serialized data is corrupt."));
							}
							if (c == ';')
							{
								this.m_currentTokenStartIndex = i + 1;
								if (this.m_currentTokenStartIndex >= this.m_serializedText.Length)
								{
									this.m_state = TimeZoneInfo.StringSerializer.State.EndOfLine;
								}
								else
								{
									this.m_state = TimeZoneInfo.StringSerializer.State.StartOfToken;
								}
								return StringBuilderCache.GetStringAndRelease(stringBuilder);
							}
							switch (c)
							{
							case '[':
								throw new SerializationException(Environment.GetResourceString("An error occurred while deserializing the object.  The serialized data is corrupt."));
							case '\\':
								state = TimeZoneInfo.StringSerializer.State.Escaped;
								break;
							case ']':
								if (canEndWithoutSeparator)
								{
									this.m_currentTokenStartIndex = i;
									this.m_state = TimeZoneInfo.StringSerializer.State.StartOfToken;
									return stringBuilder.ToString();
								}
								throw new SerializationException(Environment.GetResourceString("An error occurred while deserializing the object.  The serialized data is corrupt."));
							default:
								stringBuilder.Append(this.m_serializedText[i]);
								break;
							}
						}
					}
					if (state == TimeZoneInfo.StringSerializer.State.Escaped)
					{
						throw new SerializationException(Environment.GetResourceString("The serialized data contained an invalid escape sequence '\\\\{0}'.", new object[] { string.Empty }));
					}
					if (!canEndWithoutSeparator)
					{
						throw new SerializationException(Environment.GetResourceString("An error occurred while deserializing the object.  The serialized data is corrupt."));
					}
					this.m_currentTokenStartIndex = this.m_serializedText.Length;
					this.m_state = TimeZoneInfo.StringSerializer.State.EndOfLine;
					return StringBuilderCache.GetStringAndRelease(stringBuilder);
				}
			}

			// Token: 0x06001511 RID: 5393 RVA: 0x00056A30 File Offset: 0x00054C30
			private DateTime GetNextDateTimeValue(bool canEndWithoutSeparator, string format)
			{
				DateTime dateTime;
				if (!DateTime.TryParseExact(this.GetNextStringValue(canEndWithoutSeparator), format, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None, out dateTime))
				{
					throw new SerializationException(Environment.GetResourceString("An error occurred while deserializing the object.  The serialized data is corrupt."));
				}
				return dateTime;
			}

			// Token: 0x06001512 RID: 5394 RVA: 0x00056A68 File Offset: 0x00054C68
			private TimeSpan GetNextTimeSpanValue(bool canEndWithoutSeparator)
			{
				int nextInt32Value = this.GetNextInt32Value(canEndWithoutSeparator);
				TimeSpan timeSpan;
				try
				{
					timeSpan = new TimeSpan(0, nextInt32Value, 0);
				}
				catch (ArgumentOutOfRangeException ex)
				{
					throw new SerializationException(Environment.GetResourceString("An error occurred while deserializing the object.  The serialized data is corrupt."), ex);
				}
				return timeSpan;
			}

			// Token: 0x06001513 RID: 5395 RVA: 0x00056AAC File Offset: 0x00054CAC
			private int GetNextInt32Value(bool canEndWithoutSeparator)
			{
				int num;
				if (!int.TryParse(this.GetNextStringValue(canEndWithoutSeparator), NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out num))
				{
					throw new SerializationException(Environment.GetResourceString("An error occurred while deserializing the object.  The serialized data is corrupt."));
				}
				return num;
			}

			// Token: 0x06001514 RID: 5396 RVA: 0x00056AE0 File Offset: 0x00054CE0
			private TimeZoneInfo.AdjustmentRule[] GetNextAdjustmentRuleArrayValue(bool canEndWithoutSeparator)
			{
				List<TimeZoneInfo.AdjustmentRule> list = new List<TimeZoneInfo.AdjustmentRule>(1);
				int num = 0;
				for (TimeZoneInfo.AdjustmentRule adjustmentRule = this.GetNextAdjustmentRuleValue(true); adjustmentRule != null; adjustmentRule = this.GetNextAdjustmentRuleValue(true))
				{
					list.Add(adjustmentRule);
					num++;
				}
				if (!canEndWithoutSeparator)
				{
					if (this.m_state == TimeZoneInfo.StringSerializer.State.EndOfLine)
					{
						throw new SerializationException(Environment.GetResourceString("An error occurred while deserializing the object.  The serialized data is corrupt."));
					}
					if (this.m_currentTokenStartIndex < 0 || this.m_currentTokenStartIndex >= this.m_serializedText.Length)
					{
						throw new SerializationException(Environment.GetResourceString("An error occurred while deserializing the object.  The serialized data is corrupt."));
					}
				}
				if (num == 0)
				{
					return null;
				}
				return list.ToArray();
			}

			// Token: 0x06001515 RID: 5397 RVA: 0x00056B6C File Offset: 0x00054D6C
			private TimeZoneInfo.AdjustmentRule GetNextAdjustmentRuleValue(bool canEndWithoutSeparator)
			{
				if (this.m_state == TimeZoneInfo.StringSerializer.State.EndOfLine)
				{
					if (canEndWithoutSeparator)
					{
						return null;
					}
					throw new SerializationException(Environment.GetResourceString("An error occurred while deserializing the object.  The serialized data is corrupt."));
				}
				else
				{
					if (this.m_currentTokenStartIndex < 0 || this.m_currentTokenStartIndex >= this.m_serializedText.Length)
					{
						throw new SerializationException(Environment.GetResourceString("An error occurred while deserializing the object.  The serialized data is corrupt."));
					}
					if (this.m_serializedText[this.m_currentTokenStartIndex] == ';')
					{
						return null;
					}
					if (this.m_serializedText[this.m_currentTokenStartIndex] != '[')
					{
						throw new SerializationException(Environment.GetResourceString("An error occurred while deserializing the object.  The serialized data is corrupt."));
					}
					this.m_currentTokenStartIndex++;
					DateTime nextDateTimeValue = this.GetNextDateTimeValue(false, "MM:dd:yyyy");
					DateTime nextDateTimeValue2 = this.GetNextDateTimeValue(false, "MM:dd:yyyy");
					TimeSpan nextTimeSpanValue = this.GetNextTimeSpanValue(false);
					TimeZoneInfo.TransitionTime nextTransitionTimeValue = this.GetNextTransitionTimeValue(false);
					TimeZoneInfo.TransitionTime nextTransitionTimeValue2 = this.GetNextTransitionTimeValue(false);
					TimeSpan timeSpan = TimeSpan.Zero;
					if (this.m_state == TimeZoneInfo.StringSerializer.State.EndOfLine || this.m_currentTokenStartIndex >= this.m_serializedText.Length)
					{
						throw new SerializationException(Environment.GetResourceString("An error occurred while deserializing the object.  The serialized data is corrupt."));
					}
					if ((this.m_serializedText[this.m_currentTokenStartIndex] >= '0' && this.m_serializedText[this.m_currentTokenStartIndex] <= '9') || this.m_serializedText[this.m_currentTokenStartIndex] == '-' || this.m_serializedText[this.m_currentTokenStartIndex] == '+')
					{
						timeSpan = this.GetNextTimeSpanValue(false);
					}
					if (this.m_state == TimeZoneInfo.StringSerializer.State.EndOfLine || this.m_currentTokenStartIndex >= this.m_serializedText.Length)
					{
						throw new SerializationException(Environment.GetResourceString("An error occurred while deserializing the object.  The serialized data is corrupt."));
					}
					if (this.m_serializedText[this.m_currentTokenStartIndex] != ']')
					{
						this.SkipVersionNextDataFields(1);
					}
					else
					{
						this.m_currentTokenStartIndex++;
					}
					TimeZoneInfo.AdjustmentRule adjustmentRule;
					try
					{
						adjustmentRule = TimeZoneInfo.AdjustmentRule.CreateAdjustmentRule(nextDateTimeValue, nextDateTimeValue2, nextTimeSpanValue, nextTransitionTimeValue, nextTransitionTimeValue2, timeSpan);
					}
					catch (ArgumentException ex)
					{
						throw new SerializationException(Environment.GetResourceString("An error occurred while deserializing the object.  The serialized data is corrupt."), ex);
					}
					if (this.m_currentTokenStartIndex >= this.m_serializedText.Length)
					{
						this.m_state = TimeZoneInfo.StringSerializer.State.EndOfLine;
					}
					else
					{
						this.m_state = TimeZoneInfo.StringSerializer.State.StartOfToken;
					}
					return adjustmentRule;
				}
			}

			// Token: 0x06001516 RID: 5398 RVA: 0x00056D84 File Offset: 0x00054F84
			private TimeZoneInfo.TransitionTime GetNextTransitionTimeValue(bool canEndWithoutSeparator)
			{
				if (this.m_state == TimeZoneInfo.StringSerializer.State.EndOfLine || (this.m_currentTokenStartIndex < this.m_serializedText.Length && this.m_serializedText[this.m_currentTokenStartIndex] == ']'))
				{
					throw new SerializationException(Environment.GetResourceString("An error occurred while deserializing the object.  The serialized data is corrupt."));
				}
				if (this.m_currentTokenStartIndex < 0 || this.m_currentTokenStartIndex >= this.m_serializedText.Length)
				{
					throw new SerializationException(Environment.GetResourceString("An error occurred while deserializing the object.  The serialized data is corrupt."));
				}
				if (this.m_serializedText[this.m_currentTokenStartIndex] != '[')
				{
					throw new SerializationException(Environment.GetResourceString("An error occurred while deserializing the object.  The serialized data is corrupt."));
				}
				this.m_currentTokenStartIndex++;
				int nextInt32Value = this.GetNextInt32Value(false);
				if (nextInt32Value != 0 && nextInt32Value != 1)
				{
					throw new SerializationException(Environment.GetResourceString("An error occurred while deserializing the object.  The serialized data is corrupt."));
				}
				DateTime nextDateTimeValue = this.GetNextDateTimeValue(false, "HH:mm:ss.FFF");
				nextDateTimeValue = new DateTime(1, 1, 1, nextDateTimeValue.Hour, nextDateTimeValue.Minute, nextDateTimeValue.Second, nextDateTimeValue.Millisecond);
				int nextInt32Value2 = this.GetNextInt32Value(false);
				TimeZoneInfo.TransitionTime transitionTime;
				if (nextInt32Value == 1)
				{
					int nextInt32Value3 = this.GetNextInt32Value(false);
					try
					{
						transitionTime = TimeZoneInfo.TransitionTime.CreateFixedDateRule(nextDateTimeValue, nextInt32Value2, nextInt32Value3);
						goto IL_015B;
					}
					catch (ArgumentException ex)
					{
						throw new SerializationException(Environment.GetResourceString("An error occurred while deserializing the object.  The serialized data is corrupt."), ex);
					}
				}
				int nextInt32Value4 = this.GetNextInt32Value(false);
				int nextInt32Value5 = this.GetNextInt32Value(false);
				try
				{
					transitionTime = TimeZoneInfo.TransitionTime.CreateFloatingDateRule(nextDateTimeValue, nextInt32Value2, nextInt32Value4, (DayOfWeek)nextInt32Value5);
				}
				catch (ArgumentException ex2)
				{
					throw new SerializationException(Environment.GetResourceString("An error occurred while deserializing the object.  The serialized data is corrupt."), ex2);
				}
				IL_015B:
				if (this.m_state == TimeZoneInfo.StringSerializer.State.EndOfLine || this.m_currentTokenStartIndex >= this.m_serializedText.Length)
				{
					throw new SerializationException(Environment.GetResourceString("An error occurred while deserializing the object.  The serialized data is corrupt."));
				}
				if (this.m_serializedText[this.m_currentTokenStartIndex] != ']')
				{
					this.SkipVersionNextDataFields(1);
				}
				else
				{
					this.m_currentTokenStartIndex++;
				}
				bool flag = false;
				if (this.m_currentTokenStartIndex < this.m_serializedText.Length && this.m_serializedText[this.m_currentTokenStartIndex] == ';')
				{
					this.m_currentTokenStartIndex++;
					flag = true;
				}
				if (!flag && !canEndWithoutSeparator)
				{
					throw new SerializationException(Environment.GetResourceString("An error occurred while deserializing the object.  The serialized data is corrupt."));
				}
				if (this.m_currentTokenStartIndex >= this.m_serializedText.Length)
				{
					this.m_state = TimeZoneInfo.StringSerializer.State.EndOfLine;
				}
				else
				{
					this.m_state = TimeZoneInfo.StringSerializer.State.StartOfToken;
				}
				return transitionTime;
			}

			// Token: 0x04000B74 RID: 2932
			private string m_serializedText;

			// Token: 0x04000B75 RID: 2933
			private int m_currentTokenStartIndex;

			// Token: 0x04000B76 RID: 2934
			private TimeZoneInfo.StringSerializer.State m_state;

			// Token: 0x04000B77 RID: 2935
			private const int initialCapacityForString = 64;

			// Token: 0x04000B78 RID: 2936
			private const char esc = '\\';

			// Token: 0x04000B79 RID: 2937
			private const char sep = ';';

			// Token: 0x04000B7A RID: 2938
			private const char lhs = '[';

			// Token: 0x04000B7B RID: 2939
			private const char rhs = ']';

			// Token: 0x04000B7C RID: 2940
			private const string escString = "\\";

			// Token: 0x04000B7D RID: 2941
			private const string sepString = ";";

			// Token: 0x04000B7E RID: 2942
			private const string lhsString = "[";

			// Token: 0x04000B7F RID: 2943
			private const string rhsString = "]";

			// Token: 0x04000B80 RID: 2944
			private const string escapedEsc = "\\\\";

			// Token: 0x04000B81 RID: 2945
			private const string escapedSep = "\\;";

			// Token: 0x04000B82 RID: 2946
			private const string escapedLhs = "\\[";

			// Token: 0x04000B83 RID: 2947
			private const string escapedRhs = "\\]";

			// Token: 0x04000B84 RID: 2948
			private const string dateTimeFormat = "MM:dd:yyyy";

			// Token: 0x04000B85 RID: 2949
			private const string timeOfDayFormat = "HH:mm:ss.FFF";

			// Token: 0x020001D4 RID: 468
			private enum State
			{
				// Token: 0x04000B87 RID: 2951
				Escaped,
				// Token: 0x04000B88 RID: 2952
				NotEscaped,
				// Token: 0x04000B89 RID: 2953
				StartOfToken,
				// Token: 0x04000B8A RID: 2954
				EndOfLine
			}
		}

		// Token: 0x020001D5 RID: 469
		private class TimeZoneInfoComparer : IComparer<TimeZoneInfo>
		{
			// Token: 0x06001517 RID: 5399 RVA: 0x00056FD8 File Offset: 0x000551D8
			int IComparer<TimeZoneInfo>.Compare(TimeZoneInfo x, TimeZoneInfo y)
			{
				int num = x.BaseUtcOffset.CompareTo(y.BaseUtcOffset);
				if (num != 0)
				{
					return num;
				}
				return string.Compare(x.DisplayName, y.DisplayName, StringComparison.Ordinal);
			}
		}

		// Token: 0x020001D6 RID: 470
		private enum TimeZoneData
		{
			// Token: 0x04000B8C RID: 2956
			DaylightSavingFirstTransitionIdx,
			// Token: 0x04000B8D RID: 2957
			DaylightSavingSecondTransitionIdx,
			// Token: 0x04000B8E RID: 2958
			UtcOffsetIdx,
			// Token: 0x04000B8F RID: 2959
			AdditionalDaylightOffsetIdx
		}

		// Token: 0x020001D7 RID: 471
		private enum TimeZoneNames
		{
			// Token: 0x04000B91 RID: 2961
			StandardNameIdx,
			// Token: 0x04000B92 RID: 2962
			DaylightNameIdx
		}

		// Token: 0x020001D8 RID: 472
		internal struct SYSTEMTIME
		{
			// Token: 0x04000B93 RID: 2963
			internal ushort wYear;

			// Token: 0x04000B94 RID: 2964
			internal ushort wMonth;

			// Token: 0x04000B95 RID: 2965
			internal ushort wDayOfWeek;

			// Token: 0x04000B96 RID: 2966
			internal ushort wDay;

			// Token: 0x04000B97 RID: 2967
			internal ushort wHour;

			// Token: 0x04000B98 RID: 2968
			internal ushort wMinute;

			// Token: 0x04000B99 RID: 2969
			internal ushort wSecond;

			// Token: 0x04000B9A RID: 2970
			internal ushort wMilliseconds;
		}

		// Token: 0x020001D9 RID: 473
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct TIME_ZONE_INFORMATION
		{
			// Token: 0x04000B9B RID: 2971
			internal int Bias;

			// Token: 0x04000B9C RID: 2972
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
			internal string StandardName;

			// Token: 0x04000B9D RID: 2973
			internal TimeZoneInfo.SYSTEMTIME StandardDate;

			// Token: 0x04000B9E RID: 2974
			internal int StandardBias;

			// Token: 0x04000B9F RID: 2975
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
			internal string DaylightName;

			// Token: 0x04000BA0 RID: 2976
			internal TimeZoneInfo.SYSTEMTIME DaylightDate;

			// Token: 0x04000BA1 RID: 2977
			internal int DaylightBias;
		}

		// Token: 0x020001DA RID: 474
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct DYNAMIC_TIME_ZONE_INFORMATION
		{
			// Token: 0x04000BA2 RID: 2978
			internal TimeZoneInfo.TIME_ZONE_INFORMATION TZI;

			// Token: 0x04000BA3 RID: 2979
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
			internal string TimeZoneKeyName;

			// Token: 0x04000BA4 RID: 2980
			internal byte DynamicDaylightTimeDisabled;
		}
	}
}
