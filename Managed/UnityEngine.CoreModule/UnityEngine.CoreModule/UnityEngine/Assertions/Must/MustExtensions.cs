using System;
using System.Diagnostics;

namespace UnityEngine.Assertions.Must
{
	// Token: 0x020003E5 RID: 997
	[Obsolete("Must extensions are deprecated. Use UnityEngine.Assertions.Assert instead")]
	[DebuggerStepThrough]
	public static class MustExtensions
	{
		// Token: 0x060022A2 RID: 8866 RVA: 0x0003A55C File Offset: 0x0003875C
		[Conditional("UNITY_ASSERTIONS")]
		[Obsolete("Must extensions are deprecated. Use UnityEngine.Assertions.Assert instead")]
		public static void MustBeTrue(this bool value)
		{
			Assert.IsTrue(value);
		}

		// Token: 0x060022A3 RID: 8867 RVA: 0x0003A566 File Offset: 0x00038766
		[Obsolete("Must extensions are deprecated. Use UnityEngine.Assertions.Assert instead")]
		[Conditional("UNITY_ASSERTIONS")]
		public static void MustBeTrue(this bool value, string message)
		{
			Assert.IsTrue(value, message);
		}

		// Token: 0x060022A4 RID: 8868 RVA: 0x0003A571 File Offset: 0x00038771
		[Conditional("UNITY_ASSERTIONS")]
		[Obsolete("Must extensions are deprecated. Use UnityEngine.Assertions.Assert instead")]
		public static void MustBeFalse(this bool value)
		{
			Assert.IsFalse(value);
		}

		// Token: 0x060022A5 RID: 8869 RVA: 0x0003A57B File Offset: 0x0003877B
		[Conditional("UNITY_ASSERTIONS")]
		[Obsolete("Must extensions are deprecated. Use UnityEngine.Assertions.Assert instead")]
		public static void MustBeFalse(this bool value, string message)
		{
			Assert.IsFalse(value, message);
		}

		// Token: 0x060022A6 RID: 8870 RVA: 0x0003A586 File Offset: 0x00038786
		[Obsolete("Must extensions are deprecated. Use UnityEngine.Assertions.Assert instead")]
		[Conditional("UNITY_ASSERTIONS")]
		public static void MustBeApproximatelyEqual(this float actual, float expected)
		{
			Assert.AreApproximatelyEqual(actual, expected);
		}

		// Token: 0x060022A7 RID: 8871 RVA: 0x0003A591 File Offset: 0x00038791
		[Obsolete("Must extensions are deprecated. Use UnityEngine.Assertions.Assert instead")]
		[Conditional("UNITY_ASSERTIONS")]
		public static void MustBeApproximatelyEqual(this float actual, float expected, string message)
		{
			Assert.AreApproximatelyEqual(actual, expected, message);
		}

		// Token: 0x060022A8 RID: 8872 RVA: 0x0003A59D File Offset: 0x0003879D
		[Obsolete("Must extensions are deprecated. Use UnityEngine.Assertions.Assert instead")]
		[Conditional("UNITY_ASSERTIONS")]
		public static void MustBeApproximatelyEqual(this float actual, float expected, float tolerance)
		{
			Assert.AreApproximatelyEqual(actual, expected, tolerance);
		}

		// Token: 0x060022A9 RID: 8873 RVA: 0x0003A5A9 File Offset: 0x000387A9
		[Obsolete("Must extensions are deprecated. Use UnityEngine.Assertions.Assert instead")]
		[Conditional("UNITY_ASSERTIONS")]
		public static void MustBeApproximatelyEqual(this float actual, float expected, float tolerance, string message)
		{
			Assert.AreApproximatelyEqual(expected, actual, tolerance, message);
		}

		// Token: 0x060022AA RID: 8874 RVA: 0x0003A5B6 File Offset: 0x000387B6
		[Obsolete("Must extensions are deprecated. Use UnityEngine.Assertions.Assert instead")]
		[Conditional("UNITY_ASSERTIONS")]
		public static void MustNotBeApproximatelyEqual(this float actual, float expected)
		{
			Assert.AreNotApproximatelyEqual(expected, actual);
		}

		// Token: 0x060022AB RID: 8875 RVA: 0x0003A5C1 File Offset: 0x000387C1
		[Obsolete("Must extensions are deprecated. Use UnityEngine.Assertions.Assert instead")]
		[Conditional("UNITY_ASSERTIONS")]
		public static void MustNotBeApproximatelyEqual(this float actual, float expected, string message)
		{
			Assert.AreNotApproximatelyEqual(expected, actual, message);
		}

		// Token: 0x060022AC RID: 8876 RVA: 0x0003A5CD File Offset: 0x000387CD
		[Conditional("UNITY_ASSERTIONS")]
		[Obsolete("Must extensions are deprecated. Use UnityEngine.Assertions.Assert instead")]
		public static void MustNotBeApproximatelyEqual(this float actual, float expected, float tolerance)
		{
			Assert.AreNotApproximatelyEqual(expected, actual, tolerance);
		}

		// Token: 0x060022AD RID: 8877 RVA: 0x0003A5D9 File Offset: 0x000387D9
		[Obsolete("Must extensions are deprecated. Use UnityEngine.Assertions.Assert instead")]
		[Conditional("UNITY_ASSERTIONS")]
		public static void MustNotBeApproximatelyEqual(this float actual, float expected, float tolerance, string message)
		{
			Assert.AreNotApproximatelyEqual(expected, actual, tolerance, message);
		}

		// Token: 0x060022AE RID: 8878 RVA: 0x0003A5E6 File Offset: 0x000387E6
		[Conditional("UNITY_ASSERTIONS")]
		[Obsolete("Must extensions are deprecated. Use UnityEngine.Assertions.Assert instead")]
		public static void MustBeEqual<T>(this T actual, T expected)
		{
			Assert.AreEqual<T>(actual, expected);
		}

		// Token: 0x060022AF RID: 8879 RVA: 0x0003A5F1 File Offset: 0x000387F1
		[Obsolete("Must extensions are deprecated. Use UnityEngine.Assertions.Assert instead")]
		[Conditional("UNITY_ASSERTIONS")]
		public static void MustBeEqual<T>(this T actual, T expected, string message)
		{
			Assert.AreEqual<T>(expected, actual, message);
		}

		// Token: 0x060022B0 RID: 8880 RVA: 0x0003A5FD File Offset: 0x000387FD
		[Conditional("UNITY_ASSERTIONS")]
		[Obsolete("Must extensions are deprecated. Use UnityEngine.Assertions.Assert instead")]
		public static void MustNotBeEqual<T>(this T actual, T expected)
		{
			Assert.AreNotEqual<T>(actual, expected);
		}

		// Token: 0x060022B1 RID: 8881 RVA: 0x0003A608 File Offset: 0x00038808
		[Conditional("UNITY_ASSERTIONS")]
		[Obsolete("Must extensions are deprecated. Use UnityEngine.Assertions.Assert instead")]
		public static void MustNotBeEqual<T>(this T actual, T expected, string message)
		{
			Assert.AreNotEqual<T>(expected, actual, message);
		}

		// Token: 0x060022B2 RID: 8882 RVA: 0x0003A614 File Offset: 0x00038814
		[Conditional("UNITY_ASSERTIONS")]
		[Obsolete("Must extensions are deprecated. Use UnityEngine.Assertions.Assert instead")]
		public static void MustBeNull<T>(this T expected) where T : class
		{
			Assert.IsNull<T>(expected);
		}

		// Token: 0x060022B3 RID: 8883 RVA: 0x0003A61E File Offset: 0x0003881E
		[Conditional("UNITY_ASSERTIONS")]
		[Obsolete("Must extensions are deprecated. Use UnityEngine.Assertions.Assert instead")]
		public static void MustBeNull<T>(this T expected, string message) where T : class
		{
			Assert.IsNull<T>(expected, message);
		}

		// Token: 0x060022B4 RID: 8884 RVA: 0x0003A629 File Offset: 0x00038829
		[Conditional("UNITY_ASSERTIONS")]
		[Obsolete("Must extensions are deprecated. Use UnityEngine.Assertions.Assert instead")]
		public static void MustNotBeNull<T>(this T expected) where T : class
		{
			Assert.IsNotNull<T>(expected);
		}

		// Token: 0x060022B5 RID: 8885 RVA: 0x0003A633 File Offset: 0x00038833
		[Obsolete("Must extensions are deprecated. Use UnityEngine.Assertions.Assert instead")]
		[Conditional("UNITY_ASSERTIONS")]
		public static void MustNotBeNull<T>(this T expected, string message) where T : class
		{
			Assert.IsNotNull<T>(expected, message);
		}
	}
}
