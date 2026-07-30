using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using UnityEngine.Assertions.Comparers;

namespace UnityEngine.Assertions
{
	// Token: 0x020003E2 RID: 994
	[DebuggerStepThrough]
	public static class Assert
	{
		// Token: 0x06002258 RID: 8792 RVA: 0x00039AC8 File Offset: 0x00037CC8
		private static void Fail(string message, string userMessage)
		{
			bool flag = !Assert.raiseExceptions;
			if (flag)
			{
				bool flag2 = message == null;
				if (flag2)
				{
					message = "Assertion has failed\n";
				}
				bool flag3 = userMessage != null;
				if (flag3)
				{
					message = userMessage + "\n" + message;
				}
				Debug.LogAssertion(message);
				return;
			}
			throw new AssertionException(message, userMessage);
		}

		// Token: 0x06002259 RID: 8793 RVA: 0x00039B19 File Offset: 0x00037D19
		[Obsolete("Assert.Equals should not be used for Assertions", true)]
		[EditorBrowsable(1)]
		public static bool Equals(object obj1, object obj2)
		{
			throw new InvalidOperationException("Assert.Equals should not be used for Assertions");
		}

		// Token: 0x0600225A RID: 8794 RVA: 0x00039B26 File Offset: 0x00037D26
		[EditorBrowsable(1)]
		[Obsolete("Assert.ReferenceEquals should not be used for Assertions", true)]
		public static bool ReferenceEquals(object obj1, object obj2)
		{
			throw new InvalidOperationException("Assert.ReferenceEquals should not be used for Assertions");
		}

		// Token: 0x0600225B RID: 8795 RVA: 0x00039B34 File Offset: 0x00037D34
		[Conditional("UNITY_ASSERTIONS")]
		public static void IsTrue(bool condition)
		{
			bool flag = !condition;
			if (flag)
			{
				Assert.IsTrue(condition, null);
			}
		}

		// Token: 0x0600225C RID: 8796 RVA: 0x00039B54 File Offset: 0x00037D54
		[Conditional("UNITY_ASSERTIONS")]
		public static void IsTrue(bool condition, string message)
		{
			bool flag = !condition;
			if (flag)
			{
				Assert.Fail(AssertionMessageUtil.BooleanFailureMessage(true), message);
			}
		}

		// Token: 0x0600225D RID: 8797 RVA: 0x00039B78 File Offset: 0x00037D78
		[Conditional("UNITY_ASSERTIONS")]
		public static void IsFalse(bool condition)
		{
			if (condition)
			{
				Assert.IsFalse(condition, null);
			}
		}

		// Token: 0x0600225E RID: 8798 RVA: 0x00039B94 File Offset: 0x00037D94
		[Conditional("UNITY_ASSERTIONS")]
		public static void IsFalse(bool condition, string message)
		{
			if (condition)
			{
				Assert.Fail(AssertionMessageUtil.BooleanFailureMessage(false), message);
			}
		}

		// Token: 0x0600225F RID: 8799 RVA: 0x00039BB4 File Offset: 0x00037DB4
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreApproximatelyEqual(float expected, float actual)
		{
			Assert.AreEqual<float>(expected, actual, null, FloatComparer.s_ComparerWithDefaultTolerance);
		}

		// Token: 0x06002260 RID: 8800 RVA: 0x00039BC5 File Offset: 0x00037DC5
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreApproximatelyEqual(float expected, float actual, string message)
		{
			Assert.AreEqual<float>(expected, actual, message, FloatComparer.s_ComparerWithDefaultTolerance);
		}

		// Token: 0x06002261 RID: 8801 RVA: 0x00039BD6 File Offset: 0x00037DD6
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreApproximatelyEqual(float expected, float actual, float tolerance)
		{
			Assert.AreApproximatelyEqual(expected, actual, tolerance, null);
		}

		// Token: 0x06002262 RID: 8802 RVA: 0x00039BE3 File Offset: 0x00037DE3
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreApproximatelyEqual(float expected, float actual, float tolerance, string message)
		{
			Assert.AreEqual<float>(expected, actual, message, new FloatComparer(tolerance));
		}

		// Token: 0x06002263 RID: 8803 RVA: 0x00039BF5 File Offset: 0x00037DF5
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreNotApproximatelyEqual(float expected, float actual)
		{
			Assert.AreNotEqual<float>(expected, actual, null, FloatComparer.s_ComparerWithDefaultTolerance);
		}

		// Token: 0x06002264 RID: 8804 RVA: 0x00039C06 File Offset: 0x00037E06
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreNotApproximatelyEqual(float expected, float actual, string message)
		{
			Assert.AreNotEqual<float>(expected, actual, message, FloatComparer.s_ComparerWithDefaultTolerance);
		}

		// Token: 0x06002265 RID: 8805 RVA: 0x00039C17 File Offset: 0x00037E17
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreNotApproximatelyEqual(float expected, float actual, float tolerance)
		{
			Assert.AreNotApproximatelyEqual(expected, actual, tolerance, null);
		}

		// Token: 0x06002266 RID: 8806 RVA: 0x00039C24 File Offset: 0x00037E24
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreNotApproximatelyEqual(float expected, float actual, float tolerance, string message)
		{
			Assert.AreNotEqual<float>(expected, actual, message, new FloatComparer(tolerance));
		}

		// Token: 0x06002267 RID: 8807 RVA: 0x00039C36 File Offset: 0x00037E36
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreEqual<T>(T expected, T actual)
		{
			Assert.AreEqual<T>(expected, actual, null);
		}

		// Token: 0x06002268 RID: 8808 RVA: 0x00039C42 File Offset: 0x00037E42
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreEqual<T>(T expected, T actual, string message)
		{
			Assert.AreEqual<T>(expected, actual, message, EqualityComparer<T>.Default);
		}

		// Token: 0x06002269 RID: 8809 RVA: 0x00039C54 File Offset: 0x00037E54
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreEqual<T>(T expected, T actual, string message, IEqualityComparer<T> comparer)
		{
			bool flag = typeof(Object).IsAssignableFrom(typeof(T));
			if (flag)
			{
				Assert.AreEqual(expected as Object, actual as Object, message);
			}
			else
			{
				bool flag2 = !comparer.Equals(actual, expected);
				if (flag2)
				{
					Assert.Fail(AssertionMessageUtil.GetEqualityMessage(actual, expected, true), message);
				}
			}
		}

		// Token: 0x0600226A RID: 8810 RVA: 0x00039CC8 File Offset: 0x00037EC8
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreEqual(Object expected, Object actual, string message)
		{
			bool flag = actual != expected;
			if (flag)
			{
				Assert.Fail(AssertionMessageUtil.GetEqualityMessage(actual, expected, true), message);
			}
		}

		// Token: 0x0600226B RID: 8811 RVA: 0x00039CF0 File Offset: 0x00037EF0
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreNotEqual<T>(T expected, T actual)
		{
			Assert.AreNotEqual<T>(expected, actual, null);
		}

		// Token: 0x0600226C RID: 8812 RVA: 0x00039CFC File Offset: 0x00037EFC
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreNotEqual<T>(T expected, T actual, string message)
		{
			Assert.AreNotEqual<T>(expected, actual, message, EqualityComparer<T>.Default);
		}

		// Token: 0x0600226D RID: 8813 RVA: 0x00039D10 File Offset: 0x00037F10
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreNotEqual<T>(T expected, T actual, string message, IEqualityComparer<T> comparer)
		{
			bool flag = typeof(Object).IsAssignableFrom(typeof(T));
			if (flag)
			{
				Assert.AreNotEqual(expected as Object, actual as Object, message);
			}
			else
			{
				bool flag2 = comparer.Equals(actual, expected);
				if (flag2)
				{
					Assert.Fail(AssertionMessageUtil.GetEqualityMessage(actual, expected, false), message);
				}
			}
		}

		// Token: 0x0600226E RID: 8814 RVA: 0x00039D80 File Offset: 0x00037F80
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreNotEqual(Object expected, Object actual, string message)
		{
			bool flag = actual == expected;
			if (flag)
			{
				Assert.Fail(AssertionMessageUtil.GetEqualityMessage(actual, expected, false), message);
			}
		}

		// Token: 0x0600226F RID: 8815 RVA: 0x00039DA8 File Offset: 0x00037FA8
		[Conditional("UNITY_ASSERTIONS")]
		public static void IsNull<T>(T value) where T : class
		{
			Assert.IsNull<T>(value, null);
		}

		// Token: 0x06002270 RID: 8816 RVA: 0x00039DB4 File Offset: 0x00037FB4
		[Conditional("UNITY_ASSERTIONS")]
		public static void IsNull<T>(T value, string message) where T : class
		{
			bool flag = typeof(Object).IsAssignableFrom(typeof(T));
			if (flag)
			{
				Assert.IsNull(value as Object, message);
			}
			else
			{
				bool flag2 = value != null;
				if (flag2)
				{
					Assert.Fail(AssertionMessageUtil.NullFailureMessage(value, true), message);
				}
			}
		}

		// Token: 0x06002271 RID: 8817 RVA: 0x00039E18 File Offset: 0x00038018
		[Conditional("UNITY_ASSERTIONS")]
		public static void IsNull(Object value, string message)
		{
			bool flag = value != null;
			if (flag)
			{
				Assert.Fail(AssertionMessageUtil.NullFailureMessage(value, true), message);
			}
		}

		// Token: 0x06002272 RID: 8818 RVA: 0x00039E3F File Offset: 0x0003803F
		[Conditional("UNITY_ASSERTIONS")]
		public static void IsNotNull<T>(T value) where T : class
		{
			Assert.IsNotNull<T>(value, null);
		}

		// Token: 0x06002273 RID: 8819 RVA: 0x00039E4C File Offset: 0x0003804C
		[Conditional("UNITY_ASSERTIONS")]
		public static void IsNotNull<T>(T value, string message) where T : class
		{
			bool flag = typeof(Object).IsAssignableFrom(typeof(T));
			if (flag)
			{
				Assert.IsNotNull(value as Object, message);
			}
			else
			{
				bool flag2 = value == null;
				if (flag2)
				{
					Assert.Fail(AssertionMessageUtil.NullFailureMessage(value, false), message);
				}
			}
		}

		// Token: 0x06002274 RID: 8820 RVA: 0x00039EB0 File Offset: 0x000380B0
		[Conditional("UNITY_ASSERTIONS")]
		public static void IsNotNull(Object value, string message)
		{
			bool flag = value == null;
			if (flag)
			{
				Assert.Fail(AssertionMessageUtil.NullFailureMessage(value, false), message);
			}
		}

		// Token: 0x06002275 RID: 8821 RVA: 0x00039ED8 File Offset: 0x000380D8
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreEqual(sbyte expected, sbyte actual)
		{
			bool flag = expected != actual;
			if (flag)
			{
				Assert.AreEqual<sbyte>(expected, actual, null);
			}
		}

		// Token: 0x06002276 RID: 8822 RVA: 0x00039EFC File Offset: 0x000380FC
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreEqual(sbyte expected, sbyte actual, string message)
		{
			bool flag = expected != actual;
			if (flag)
			{
				Assert.AreEqual<sbyte>(expected, actual, message);
			}
		}

		// Token: 0x06002277 RID: 8823 RVA: 0x00039F20 File Offset: 0x00038120
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreNotEqual(sbyte expected, sbyte actual)
		{
			bool flag = expected == actual;
			if (flag)
			{
				Assert.AreNotEqual<sbyte>(expected, actual, null);
			}
		}

		// Token: 0x06002278 RID: 8824 RVA: 0x00039F40 File Offset: 0x00038140
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreNotEqual(sbyte expected, sbyte actual, string message)
		{
			bool flag = expected == actual;
			if (flag)
			{
				Assert.AreNotEqual<sbyte>(expected, actual, message);
			}
		}

		// Token: 0x06002279 RID: 8825 RVA: 0x00039F60 File Offset: 0x00038160
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreEqual(byte expected, byte actual)
		{
			bool flag = expected != actual;
			if (flag)
			{
				Assert.AreEqual<byte>(expected, actual, null);
			}
		}

		// Token: 0x0600227A RID: 8826 RVA: 0x00039F84 File Offset: 0x00038184
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreEqual(byte expected, byte actual, string message)
		{
			bool flag = expected != actual;
			if (flag)
			{
				Assert.AreEqual<byte>(expected, actual, message);
			}
		}

		// Token: 0x0600227B RID: 8827 RVA: 0x00039FA8 File Offset: 0x000381A8
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreNotEqual(byte expected, byte actual)
		{
			bool flag = expected == actual;
			if (flag)
			{
				Assert.AreNotEqual<byte>(expected, actual, null);
			}
		}

		// Token: 0x0600227C RID: 8828 RVA: 0x00039FC8 File Offset: 0x000381C8
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreNotEqual(byte expected, byte actual, string message)
		{
			bool flag = expected == actual;
			if (flag)
			{
				Assert.AreNotEqual<byte>(expected, actual, message);
			}
		}

		// Token: 0x0600227D RID: 8829 RVA: 0x00039FE8 File Offset: 0x000381E8
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreEqual(char expected, char actual)
		{
			bool flag = expected != actual;
			if (flag)
			{
				Assert.AreEqual<char>(expected, actual, null);
			}
		}

		// Token: 0x0600227E RID: 8830 RVA: 0x0003A00C File Offset: 0x0003820C
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreEqual(char expected, char actual, string message)
		{
			bool flag = expected != actual;
			if (flag)
			{
				Assert.AreEqual<char>(expected, actual, message);
			}
		}

		// Token: 0x0600227F RID: 8831 RVA: 0x0003A030 File Offset: 0x00038230
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreNotEqual(char expected, char actual)
		{
			bool flag = expected == actual;
			if (flag)
			{
				Assert.AreNotEqual<char>(expected, actual, null);
			}
		}

		// Token: 0x06002280 RID: 8832 RVA: 0x0003A050 File Offset: 0x00038250
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreNotEqual(char expected, char actual, string message)
		{
			bool flag = expected == actual;
			if (flag)
			{
				Assert.AreNotEqual<char>(expected, actual, message);
			}
		}

		// Token: 0x06002281 RID: 8833 RVA: 0x0003A070 File Offset: 0x00038270
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreEqual(short expected, short actual)
		{
			bool flag = expected != actual;
			if (flag)
			{
				Assert.AreEqual<short>(expected, actual, null);
			}
		}

		// Token: 0x06002282 RID: 8834 RVA: 0x0003A094 File Offset: 0x00038294
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreEqual(short expected, short actual, string message)
		{
			bool flag = expected != actual;
			if (flag)
			{
				Assert.AreEqual<short>(expected, actual, message);
			}
		}

		// Token: 0x06002283 RID: 8835 RVA: 0x0003A0B8 File Offset: 0x000382B8
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreNotEqual(short expected, short actual)
		{
			bool flag = expected == actual;
			if (flag)
			{
				Assert.AreNotEqual<short>(expected, actual, null);
			}
		}

		// Token: 0x06002284 RID: 8836 RVA: 0x0003A0D8 File Offset: 0x000382D8
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreNotEqual(short expected, short actual, string message)
		{
			bool flag = expected == actual;
			if (flag)
			{
				Assert.AreNotEqual<short>(expected, actual, message);
			}
		}

		// Token: 0x06002285 RID: 8837 RVA: 0x0003A0F8 File Offset: 0x000382F8
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreEqual(ushort expected, ushort actual)
		{
			bool flag = expected != actual;
			if (flag)
			{
				Assert.AreEqual<ushort>(expected, actual, null);
			}
		}

		// Token: 0x06002286 RID: 8838 RVA: 0x0003A11C File Offset: 0x0003831C
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreEqual(ushort expected, ushort actual, string message)
		{
			bool flag = expected != actual;
			if (flag)
			{
				Assert.AreEqual<ushort>(expected, actual, message);
			}
		}

		// Token: 0x06002287 RID: 8839 RVA: 0x0003A140 File Offset: 0x00038340
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreNotEqual(ushort expected, ushort actual)
		{
			bool flag = expected == actual;
			if (flag)
			{
				Assert.AreNotEqual<ushort>(expected, actual, null);
			}
		}

		// Token: 0x06002288 RID: 8840 RVA: 0x0003A160 File Offset: 0x00038360
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreNotEqual(ushort expected, ushort actual, string message)
		{
			bool flag = expected == actual;
			if (flag)
			{
				Assert.AreNotEqual<ushort>(expected, actual, message);
			}
		}

		// Token: 0x06002289 RID: 8841 RVA: 0x0003A180 File Offset: 0x00038380
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreEqual(int expected, int actual)
		{
			bool flag = expected != actual;
			if (flag)
			{
				Assert.AreEqual<int>(expected, actual, null);
			}
		}

		// Token: 0x0600228A RID: 8842 RVA: 0x0003A1A4 File Offset: 0x000383A4
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreEqual(int expected, int actual, string message)
		{
			bool flag = expected != actual;
			if (flag)
			{
				Assert.AreEqual<int>(expected, actual, message);
			}
		}

		// Token: 0x0600228B RID: 8843 RVA: 0x0003A1C8 File Offset: 0x000383C8
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreNotEqual(int expected, int actual)
		{
			bool flag = expected == actual;
			if (flag)
			{
				Assert.AreNotEqual<int>(expected, actual, null);
			}
		}

		// Token: 0x0600228C RID: 8844 RVA: 0x0003A1E8 File Offset: 0x000383E8
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreNotEqual(int expected, int actual, string message)
		{
			bool flag = expected == actual;
			if (flag)
			{
				Assert.AreNotEqual<int>(expected, actual, message);
			}
		}

		// Token: 0x0600228D RID: 8845 RVA: 0x0003A208 File Offset: 0x00038408
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreEqual(uint expected, uint actual)
		{
			bool flag = expected != actual;
			if (flag)
			{
				Assert.AreEqual<uint>(expected, actual, null);
			}
		}

		// Token: 0x0600228E RID: 8846 RVA: 0x0003A22C File Offset: 0x0003842C
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreEqual(uint expected, uint actual, string message)
		{
			bool flag = expected != actual;
			if (flag)
			{
				Assert.AreEqual<uint>(expected, actual, message);
			}
		}

		// Token: 0x0600228F RID: 8847 RVA: 0x0003A250 File Offset: 0x00038450
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreNotEqual(uint expected, uint actual)
		{
			bool flag = expected == actual;
			if (flag)
			{
				Assert.AreNotEqual<uint>(expected, actual, null);
			}
		}

		// Token: 0x06002290 RID: 8848 RVA: 0x0003A270 File Offset: 0x00038470
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreNotEqual(uint expected, uint actual, string message)
		{
			bool flag = expected == actual;
			if (flag)
			{
				Assert.AreNotEqual<uint>(expected, actual, message);
			}
		}

		// Token: 0x06002291 RID: 8849 RVA: 0x0003A290 File Offset: 0x00038490
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreEqual(long expected, long actual)
		{
			bool flag = expected != actual;
			if (flag)
			{
				Assert.AreEqual<long>(expected, actual, null);
			}
		}

		// Token: 0x06002292 RID: 8850 RVA: 0x0003A2B4 File Offset: 0x000384B4
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreEqual(long expected, long actual, string message)
		{
			bool flag = expected != actual;
			if (flag)
			{
				Assert.AreEqual<long>(expected, actual, message);
			}
		}

		// Token: 0x06002293 RID: 8851 RVA: 0x0003A2D8 File Offset: 0x000384D8
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreNotEqual(long expected, long actual)
		{
			bool flag = expected == actual;
			if (flag)
			{
				Assert.AreNotEqual<long>(expected, actual, null);
			}
		}

		// Token: 0x06002294 RID: 8852 RVA: 0x0003A2F8 File Offset: 0x000384F8
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreNotEqual(long expected, long actual, string message)
		{
			bool flag = expected == actual;
			if (flag)
			{
				Assert.AreNotEqual<long>(expected, actual, message);
			}
		}

		// Token: 0x06002295 RID: 8853 RVA: 0x0003A318 File Offset: 0x00038518
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreEqual(ulong expected, ulong actual)
		{
			bool flag = expected != actual;
			if (flag)
			{
				Assert.AreEqual<ulong>(expected, actual, null);
			}
		}

		// Token: 0x06002296 RID: 8854 RVA: 0x0003A33C File Offset: 0x0003853C
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreEqual(ulong expected, ulong actual, string message)
		{
			bool flag = expected != actual;
			if (flag)
			{
				Assert.AreEqual<ulong>(expected, actual, message);
			}
		}

		// Token: 0x06002297 RID: 8855 RVA: 0x0003A360 File Offset: 0x00038560
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreNotEqual(ulong expected, ulong actual)
		{
			bool flag = expected == actual;
			if (flag)
			{
				Assert.AreNotEqual<ulong>(expected, actual, null);
			}
		}

		// Token: 0x06002298 RID: 8856 RVA: 0x0003A380 File Offset: 0x00038580
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreNotEqual(ulong expected, ulong actual, string message)
		{
			bool flag = expected == actual;
			if (flag)
			{
				Assert.AreNotEqual<ulong>(expected, actual, message);
			}
		}

		// Token: 0x04000CFE RID: 3326
		internal const string UNITY_ASSERTIONS = "UNITY_ASSERTIONS";

		// Token: 0x04000CFF RID: 3327
		[Obsolete("Future versions of Unity are expected to always throw exceptions and not have this field.")]
		public static bool raiseExceptions = true;
	}
}
