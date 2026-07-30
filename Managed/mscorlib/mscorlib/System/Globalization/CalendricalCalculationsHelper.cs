using System;

namespace System.Globalization
{
	// Token: 0x020003F4 RID: 1012
	internal class CalendricalCalculationsHelper
	{
		// Token: 0x06002F99 RID: 12185 RVA: 0x000AA10B File Offset: 0x000A830B
		private static double RadiansFromDegrees(double degree)
		{
			return degree * 3.141592653589793 / 180.0;
		}

		// Token: 0x06002F9A RID: 12186 RVA: 0x000AA122 File Offset: 0x000A8322
		private static double SinOfDegree(double degree)
		{
			return Math.Sin(CalendricalCalculationsHelper.RadiansFromDegrees(degree));
		}

		// Token: 0x06002F9B RID: 12187 RVA: 0x000AA12F File Offset: 0x000A832F
		private static double CosOfDegree(double degree)
		{
			return Math.Cos(CalendricalCalculationsHelper.RadiansFromDegrees(degree));
		}

		// Token: 0x06002F9C RID: 12188 RVA: 0x000AA13C File Offset: 0x000A833C
		private static double TanOfDegree(double degree)
		{
			return Math.Tan(CalendricalCalculationsHelper.RadiansFromDegrees(degree));
		}

		// Token: 0x06002F9D RID: 12189 RVA: 0x000AA149 File Offset: 0x000A8349
		public static double Angle(int degrees, int minutes, double seconds)
		{
			return (seconds / 60.0 + (double)minutes) / 60.0 + (double)degrees;
		}

		// Token: 0x06002F9E RID: 12190 RVA: 0x000AA166 File Offset: 0x000A8366
		private static double Obliquity(double julianCenturies)
		{
			return CalendricalCalculationsHelper.PolynomialSum(CalendricalCalculationsHelper.Coefficients, julianCenturies);
		}

		// Token: 0x06002F9F RID: 12191 RVA: 0x000AA173 File Offset: 0x000A8373
		internal static long GetNumberOfDays(DateTime date)
		{
			return date.Ticks / 864000000000L;
		}

		// Token: 0x06002FA0 RID: 12192 RVA: 0x000AA188 File Offset: 0x000A8388
		private static int GetGregorianYear(double numberOfDays)
		{
			return new DateTime(Math.Min((long)(Math.Floor(numberOfDays) * 864000000000.0), DateTime.MaxValue.Ticks)).Year;
		}

		// Token: 0x06002FA1 RID: 12193 RVA: 0x000AA1C8 File Offset: 0x000A83C8
		private static double Reminder(double divisor, double dividend)
		{
			double num = Math.Floor(divisor / dividend);
			return divisor - dividend * num;
		}

		// Token: 0x06002FA2 RID: 12194 RVA: 0x000AA1E3 File Offset: 0x000A83E3
		private static double NormalizeLongitude(double longitude)
		{
			longitude = CalendricalCalculationsHelper.Reminder(longitude, 360.0);
			if (longitude < 0.0)
			{
				longitude += 360.0;
			}
			return longitude;
		}

		// Token: 0x06002FA3 RID: 12195 RVA: 0x000AA210 File Offset: 0x000A8410
		public static double AsDayFraction(double longitude)
		{
			return longitude / 360.0;
		}

		// Token: 0x06002FA4 RID: 12196 RVA: 0x000AA220 File Offset: 0x000A8420
		private static double PolynomialSum(double[] coefficients, double indeterminate)
		{
			double num = coefficients[0];
			double num2 = 1.0;
			for (int i = 1; i < coefficients.Length; i++)
			{
				num2 *= indeterminate;
				num += coefficients[i] * num2;
			}
			return num;
		}

		// Token: 0x06002FA5 RID: 12197 RVA: 0x000AA256 File Offset: 0x000A8456
		private static double CenturiesFrom1900(int gregorianYear)
		{
			return (double)(CalendricalCalculationsHelper.GetNumberOfDays(new DateTime(gregorianYear, 7, 1)) - CalendricalCalculationsHelper.StartOf1900Century) / 36525.0;
		}

		// Token: 0x06002FA6 RID: 12198 RVA: 0x000AA278 File Offset: 0x000A8478
		private static double DefaultEphemerisCorrection(int gregorianYear)
		{
			double num = (double)(CalendricalCalculationsHelper.GetNumberOfDays(new DateTime(gregorianYear, 1, 1)) - CalendricalCalculationsHelper.StartOf1810);
			return (Math.Pow(0.5 + num, 2.0) / 41048480.0 - 15.0) / 86400.0;
		}

		// Token: 0x06002FA7 RID: 12199 RVA: 0x000AA2D1 File Offset: 0x000A84D1
		private static double EphemerisCorrection1988to2019(int gregorianYear)
		{
			return (double)(gregorianYear - 1933) / 86400.0;
		}

		// Token: 0x06002FA8 RID: 12200 RVA: 0x000AA2E8 File Offset: 0x000A84E8
		private static double EphemerisCorrection1900to1987(int gregorianYear)
		{
			double num = CalendricalCalculationsHelper.CenturiesFrom1900(gregorianYear);
			return CalendricalCalculationsHelper.PolynomialSum(CalendricalCalculationsHelper.Coefficients1900to1987, num);
		}

		// Token: 0x06002FA9 RID: 12201 RVA: 0x000AA308 File Offset: 0x000A8508
		private static double EphemerisCorrection1800to1899(int gregorianYear)
		{
			double num = CalendricalCalculationsHelper.CenturiesFrom1900(gregorianYear);
			return CalendricalCalculationsHelper.PolynomialSum(CalendricalCalculationsHelper.Coefficients1800to1899, num);
		}

		// Token: 0x06002FAA RID: 12202 RVA: 0x000AA328 File Offset: 0x000A8528
		private static double EphemerisCorrection1700to1799(int gregorianYear)
		{
			double num = (double)(gregorianYear - 1700);
			return CalendricalCalculationsHelper.PolynomialSum(CalendricalCalculationsHelper.Coefficients1700to1799, num) / 86400.0;
		}

		// Token: 0x06002FAB RID: 12203 RVA: 0x000AA354 File Offset: 0x000A8554
		private static double EphemerisCorrection1620to1699(int gregorianYear)
		{
			double num = (double)(gregorianYear - 1600);
			return CalendricalCalculationsHelper.PolynomialSum(CalendricalCalculationsHelper.Coefficients1620to1699, num) / 86400.0;
		}

		// Token: 0x06002FAC RID: 12204 RVA: 0x000AA380 File Offset: 0x000A8580
		private static double EphemerisCorrection(double time)
		{
			int gregorianYear = CalendricalCalculationsHelper.GetGregorianYear(time);
			CalendricalCalculationsHelper.EphemerisCorrectionAlgorithmMap[] ephemerisCorrectionTable = CalendricalCalculationsHelper.EphemerisCorrectionTable;
			int i = 0;
			while (i < ephemerisCorrectionTable.Length)
			{
				CalendricalCalculationsHelper.EphemerisCorrectionAlgorithmMap ephemerisCorrectionAlgorithmMap = ephemerisCorrectionTable[i];
				if (ephemerisCorrectionAlgorithmMap._lowestYear <= gregorianYear)
				{
					switch (ephemerisCorrectionAlgorithmMap._algorithm)
					{
					case CalendricalCalculationsHelper.CorrectionAlgorithm.Default:
						return CalendricalCalculationsHelper.DefaultEphemerisCorrection(gregorianYear);
					case CalendricalCalculationsHelper.CorrectionAlgorithm.Year1988to2019:
						return CalendricalCalculationsHelper.EphemerisCorrection1988to2019(gregorianYear);
					case CalendricalCalculationsHelper.CorrectionAlgorithm.Year1900to1987:
						return CalendricalCalculationsHelper.EphemerisCorrection1900to1987(gregorianYear);
					case CalendricalCalculationsHelper.CorrectionAlgorithm.Year1800to1899:
						return CalendricalCalculationsHelper.EphemerisCorrection1800to1899(gregorianYear);
					case CalendricalCalculationsHelper.CorrectionAlgorithm.Year1700to1799:
						return CalendricalCalculationsHelper.EphemerisCorrection1700to1799(gregorianYear);
					case CalendricalCalculationsHelper.CorrectionAlgorithm.Year1620to1699:
						return CalendricalCalculationsHelper.EphemerisCorrection1620to1699(gregorianYear);
					default:
						goto IL_007F;
					}
				}
				else
				{
					i++;
				}
			}
			IL_007F:
			return CalendricalCalculationsHelper.DefaultEphemerisCorrection(gregorianYear);
		}

		// Token: 0x06002FAD RID: 12205 RVA: 0x000AA412 File Offset: 0x000A8612
		public static double JulianCenturies(double moment)
		{
			return (moment + CalendricalCalculationsHelper.EphemerisCorrection(moment) - 730120.5) / 36525.0;
		}

		// Token: 0x06002FAE RID: 12206 RVA: 0x000AA430 File Offset: 0x000A8630
		private static bool IsNegative(double value)
		{
			return Math.Sign(value) == -1;
		}

		// Token: 0x06002FAF RID: 12207 RVA: 0x000AA43B File Offset: 0x000A863B
		private static double CopySign(double value, double sign)
		{
			if (CalendricalCalculationsHelper.IsNegative(value) != CalendricalCalculationsHelper.IsNegative(sign))
			{
				return -value;
			}
			return value;
		}

		// Token: 0x06002FB0 RID: 12208 RVA: 0x000AA450 File Offset: 0x000A8650
		private static double EquationOfTime(double time)
		{
			double num = CalendricalCalculationsHelper.JulianCenturies(time);
			double num2 = CalendricalCalculationsHelper.PolynomialSum(CalendricalCalculationsHelper.LambdaCoefficients, num);
			double num3 = CalendricalCalculationsHelper.PolynomialSum(CalendricalCalculationsHelper.AnomalyCoefficients, num);
			double num4 = CalendricalCalculationsHelper.PolynomialSum(CalendricalCalculationsHelper.EccentricityCoefficients, num);
			double num5 = CalendricalCalculationsHelper.TanOfDegree(CalendricalCalculationsHelper.Obliquity(num) / 2.0);
			double num6 = num5 * num5;
			double num7 = num6 * CalendricalCalculationsHelper.SinOfDegree(2.0 * num2) - 2.0 * num4 * CalendricalCalculationsHelper.SinOfDegree(num3) + 4.0 * num4 * num6 * CalendricalCalculationsHelper.SinOfDegree(num3) * CalendricalCalculationsHelper.CosOfDegree(2.0 * num2) - 0.5 * Math.Pow(num6, 2.0) * CalendricalCalculationsHelper.SinOfDegree(4.0 * num2) - 1.25 * Math.Pow(num4, 2.0) * CalendricalCalculationsHelper.SinOfDegree(2.0 * num3);
			double num8 = 6.283185307179586;
			double num9 = num7 / num8;
			return CalendricalCalculationsHelper.CopySign(Math.Min(Math.Abs(num9), 0.5), num9);
		}

		// Token: 0x06002FB1 RID: 12209 RVA: 0x000AA574 File Offset: 0x000A8774
		private static double AsLocalTime(double apparentMidday, double longitude)
		{
			double num = apparentMidday - CalendricalCalculationsHelper.AsDayFraction(longitude);
			return apparentMidday - CalendricalCalculationsHelper.EquationOfTime(num);
		}

		// Token: 0x06002FB2 RID: 12210 RVA: 0x000AA592 File Offset: 0x000A8792
		public static double Midday(double date, double longitude)
		{
			return CalendricalCalculationsHelper.AsLocalTime(date + 0.5, longitude) - CalendricalCalculationsHelper.AsDayFraction(longitude);
		}

		// Token: 0x06002FB3 RID: 12211 RVA: 0x000AA5AC File Offset: 0x000A87AC
		private static double InitLongitude(double longitude)
		{
			return CalendricalCalculationsHelper.NormalizeLongitude(longitude + 180.0) - 180.0;
		}

		// Token: 0x06002FB4 RID: 12212 RVA: 0x000AA5C8 File Offset: 0x000A87C8
		public static double MiddayAtPersianObservationSite(double date)
		{
			return CalendricalCalculationsHelper.Midday(date, CalendricalCalculationsHelper.InitLongitude(52.5));
		}

		// Token: 0x06002FB5 RID: 12213 RVA: 0x000AA5DE File Offset: 0x000A87DE
		private static double PeriodicTerm(double julianCenturies, int x, double y, double z)
		{
			return (double)x * CalendricalCalculationsHelper.SinOfDegree(y + z * julianCenturies);
		}

		// Token: 0x06002FB6 RID: 12214 RVA: 0x000AA5F0 File Offset: 0x000A87F0
		private static double SumLongSequenceOfPeriodicTerms(double julianCenturies)
		{
			return 0.0 + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 403406, 270.54861, 0.9287892) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 195207, 340.19128, 35999.1376958) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 119433, 63.91854, 35999.4089666) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 112392, 331.2622, 35998.7287385) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 3891, 317.843, 71998.20261) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 2819, 86.631, 71998.4403) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 1721, 240.052, 36000.35726) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 660, 310.26, 71997.4812) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 350, 247.23, 32964.4678) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 334, 260.87, -19.441) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 314, 297.82, 445267.1117) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 268, 343.14, 45036.884) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 242, 166.79, 3.1008) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 234, 81.53, 22518.4434) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 158, 3.5, -19.9739) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 132, 132.75, 65928.9345) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 129, 182.95, 9038.0293) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 114, 162.03, 3034.7684) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 99, 29.8, 33718.148) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 93, 266.4, 3034.448) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 86, 249.2, -2280.773) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 78, 157.6, 29929.992) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 72, 257.8, 31556.493) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 68, 185.1, 149.588) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 64, 69.9, 9037.75) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 46, 8.0, 107997.405) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 38, 197.1, -4444.176) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 37, 250.4, 151.771) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 32, 65.3, 67555.316) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 29, 162.7, 31556.08) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 28, 341.5, -4561.54) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 27, 291.6, 107996.706) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 27, 98.5, 1221.655) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 25, 146.7, 62894.167) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 24, 110.0, 31437.369) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 21, 5.2, 14578.298) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 21, 342.6, -31931.757) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 20, 230.9, 34777.243) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 18, 256.1, 1221.999) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 17, 45.3, 62894.511) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 14, 242.9, -4442.039) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 13, 115.2, 107997.909) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 13, 151.8, 119.066) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 13, 285.3, 16859.071) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 12, 53.3, -4.578) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 10, 126.6, 26895.292) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 10, 205.7, -39.127) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 10, 85.9, 12297.536) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 10, 146.1, 90073.778);
		}

		// Token: 0x06002FB7 RID: 12215 RVA: 0x000AAB64 File Offset: 0x000A8D64
		private static double Aberration(double julianCenturies)
		{
			return 9.74E-05 * CalendricalCalculationsHelper.CosOfDegree(177.63 + 35999.01848 * julianCenturies) - 0.005575;
		}

		// Token: 0x06002FB8 RID: 12216 RVA: 0x000AAB94 File Offset: 0x000A8D94
		private static double Nutation(double julianCenturies)
		{
			double num = CalendricalCalculationsHelper.PolynomialSum(CalendricalCalculationsHelper.CoefficientsA, julianCenturies);
			double num2 = CalendricalCalculationsHelper.PolynomialSum(CalendricalCalculationsHelper.CoefficientsB, julianCenturies);
			return -0.004778 * CalendricalCalculationsHelper.SinOfDegree(num) - 0.0003667 * CalendricalCalculationsHelper.SinOfDegree(num2);
		}

		// Token: 0x06002FB9 RID: 12217 RVA: 0x000AABDC File Offset: 0x000A8DDC
		public static double Compute(double time)
		{
			double num = CalendricalCalculationsHelper.JulianCenturies(time);
			return CalendricalCalculationsHelper.InitLongitude(282.7771834 + 36000.76953744 * num + 5.729577951308232E-06 * CalendricalCalculationsHelper.SumLongSequenceOfPeriodicTerms(num) + CalendricalCalculationsHelper.Aberration(num) + CalendricalCalculationsHelper.Nutation(num));
		}

		// Token: 0x06002FBA RID: 12218 RVA: 0x000AAC29 File Offset: 0x000A8E29
		public static double AsSeason(double longitude)
		{
			if (longitude >= 0.0)
			{
				return longitude;
			}
			return longitude + 360.0;
		}

		// Token: 0x06002FBB RID: 12219 RVA: 0x000AAC44 File Offset: 0x000A8E44
		private static double EstimatePrior(double longitude, double time)
		{
			double num = time - 1.0145616361111112 * CalendricalCalculationsHelper.AsSeason(CalendricalCalculationsHelper.InitLongitude(CalendricalCalculationsHelper.Compute(time) - longitude));
			double num2 = CalendricalCalculationsHelper.InitLongitude(CalendricalCalculationsHelper.Compute(num) - longitude);
			return Math.Min(time, num - 1.0145616361111112 * num2);
		}

		// Token: 0x06002FBC RID: 12220 RVA: 0x000AAC94 File Offset: 0x000A8E94
		internal static long PersianNewYearOnOrBefore(long numberOfDays)
		{
			double num = (double)numberOfDays;
			long num2 = (long)Math.Floor(CalendricalCalculationsHelper.EstimatePrior(0.0, CalendricalCalculationsHelper.MiddayAtPersianObservationSite(num))) - 1L;
			long num3 = num2 + 3L;
			long num4;
			for (num4 = num2; num4 != num3; num4 += 1L)
			{
				double num5 = CalendricalCalculationsHelper.Compute(CalendricalCalculationsHelper.MiddayAtPersianObservationSite((double)num4));
				if (0.0 <= num5 && num5 <= 2.0)
				{
					break;
				}
			}
			return num4 - 1L;
		}

		// Token: 0x0400187B RID: 6267
		private const double FullCircleOfArc = 360.0;

		// Token: 0x0400187C RID: 6268
		private const int HalfCircleOfArc = 180;

		// Token: 0x0400187D RID: 6269
		private const double TwelveHours = 0.5;

		// Token: 0x0400187E RID: 6270
		private const double Noon2000Jan01 = 730120.5;

		// Token: 0x0400187F RID: 6271
		internal const double MeanTropicalYearInDays = 365.242189;

		// Token: 0x04001880 RID: 6272
		private const double MeanSpeedOfSun = 1.0145616361111112;

		// Token: 0x04001881 RID: 6273
		private const double LongitudeSpring = 0.0;

		// Token: 0x04001882 RID: 6274
		private const double TwoDegreesAfterSpring = 2.0;

		// Token: 0x04001883 RID: 6275
		private const int SecondsPerDay = 86400;

		// Token: 0x04001884 RID: 6276
		private const int DaysInUniformLengthCentury = 36525;

		// Token: 0x04001885 RID: 6277
		private const int SecondsPerMinute = 60;

		// Token: 0x04001886 RID: 6278
		private const int MinutesPerDegree = 60;

		// Token: 0x04001887 RID: 6279
		private static long StartOf1810 = CalendricalCalculationsHelper.GetNumberOfDays(new DateTime(1810, 1, 1));

		// Token: 0x04001888 RID: 6280
		private static long StartOf1900Century = CalendricalCalculationsHelper.GetNumberOfDays(new DateTime(1900, 1, 1));

		// Token: 0x04001889 RID: 6281
		private static double[] Coefficients1900to1987 = new double[] { -2E-05, 0.000297, 0.025184, -0.181133, 0.55304, -0.861938, 0.677066, -0.212591 };

		// Token: 0x0400188A RID: 6282
		private static double[] Coefficients1800to1899 = new double[]
		{
			-9E-06, 0.003844, 0.083563, 0.865736, 4.867575, 15.845535, 31.332267, 38.291999, 28.316289, 11.636204,
			2.043794
		};

		// Token: 0x0400188B RID: 6283
		private static double[] Coefficients1700to1799 = new double[] { 8.118780842, -0.005092142, 0.003336121, -2.66484E-05 };

		// Token: 0x0400188C RID: 6284
		private static double[] Coefficients1620to1699 = new double[] { 196.58333, -4.0675, 0.0219167 };

		// Token: 0x0400188D RID: 6285
		private static double[] LambdaCoefficients = new double[] { 280.46645, 36000.76983, 0.0003032 };

		// Token: 0x0400188E RID: 6286
		private static double[] AnomalyCoefficients = new double[] { 357.5291, 35999.0503, -0.0001559, -4.8E-07 };

		// Token: 0x0400188F RID: 6287
		private static double[] EccentricityCoefficients = new double[] { 0.016708617, -4.2037E-05, -1.236E-07 };

		// Token: 0x04001890 RID: 6288
		private static double[] Coefficients = new double[]
		{
			CalendricalCalculationsHelper.Angle(23, 26, 21.448),
			CalendricalCalculationsHelper.Angle(0, 0, -46.815),
			CalendricalCalculationsHelper.Angle(0, 0, -0.00059),
			CalendricalCalculationsHelper.Angle(0, 0, 0.001813)
		};

		// Token: 0x04001891 RID: 6289
		private static double[] CoefficientsA = new double[] { 124.9, -1934.134, 0.002063 };

		// Token: 0x04001892 RID: 6290
		private static double[] CoefficientsB = new double[] { 201.11, 72001.5377, 0.00057 };

		// Token: 0x04001893 RID: 6291
		private static CalendricalCalculationsHelper.EphemerisCorrectionAlgorithmMap[] EphemerisCorrectionTable = new CalendricalCalculationsHelper.EphemerisCorrectionAlgorithmMap[]
		{
			new CalendricalCalculationsHelper.EphemerisCorrectionAlgorithmMap(2020, CalendricalCalculationsHelper.CorrectionAlgorithm.Default),
			new CalendricalCalculationsHelper.EphemerisCorrectionAlgorithmMap(1988, CalendricalCalculationsHelper.CorrectionAlgorithm.Year1988to2019),
			new CalendricalCalculationsHelper.EphemerisCorrectionAlgorithmMap(1900, CalendricalCalculationsHelper.CorrectionAlgorithm.Year1900to1987),
			new CalendricalCalculationsHelper.EphemerisCorrectionAlgorithmMap(1800, CalendricalCalculationsHelper.CorrectionAlgorithm.Year1800to1899),
			new CalendricalCalculationsHelper.EphemerisCorrectionAlgorithmMap(1700, CalendricalCalculationsHelper.CorrectionAlgorithm.Year1700to1799),
			new CalendricalCalculationsHelper.EphemerisCorrectionAlgorithmMap(1620, CalendricalCalculationsHelper.CorrectionAlgorithm.Year1620to1699),
			new CalendricalCalculationsHelper.EphemerisCorrectionAlgorithmMap(int.MinValue, CalendricalCalculationsHelper.CorrectionAlgorithm.Default)
		};

		// Token: 0x020003F5 RID: 1013
		private enum CorrectionAlgorithm
		{
			// Token: 0x04001895 RID: 6293
			Default,
			// Token: 0x04001896 RID: 6294
			Year1988to2019,
			// Token: 0x04001897 RID: 6295
			Year1900to1987,
			// Token: 0x04001898 RID: 6296
			Year1800to1899,
			// Token: 0x04001899 RID: 6297
			Year1700to1799,
			// Token: 0x0400189A RID: 6298
			Year1620to1699
		}

		// Token: 0x020003F6 RID: 1014
		private struct EphemerisCorrectionAlgorithmMap
		{
			// Token: 0x06002FBF RID: 12223 RVA: 0x000AAEDE File Offset: 0x000A90DE
			public EphemerisCorrectionAlgorithmMap(int year, CalendricalCalculationsHelper.CorrectionAlgorithm algorithm)
			{
				this._lowestYear = year;
				this._algorithm = algorithm;
			}

			// Token: 0x0400189B RID: 6299
			internal int _lowestYear;

			// Token: 0x0400189C RID: 6300
			internal CalendricalCalculationsHelper.CorrectionAlgorithm _algorithm;
		}
	}
}
