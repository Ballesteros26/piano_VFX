using System;
using System.Data.Common;
using System.Threading;

namespace System.Data.SqlClient
{
	// Token: 0x02000230 RID: 560
	internal sealed class TdsParserStaticMethods
	{
		// Token: 0x0600194A RID: 6474 RVA: 0x00080DFC File Offset: 0x0007EFFC
		internal static byte[] ObfuscatePassword(string password)
		{
			byte[] array = new byte[password.Length << 1];
			for (int i = 0; i < password.Length; i++)
			{
				char c = password[i];
				byte b = (byte)(c & 'ÿ');
				byte b2 = (byte)((c >> 8) & 'ÿ');
				array[i << 1] = (byte)((((int)(b & 15) << 4) | (b >> 4)) ^ 165);
				array[(i << 1) + 1] = (byte)((((int)(b2 & 15) << 4) | (b2 >> 4)) ^ 165);
			}
			return array;
		}

		// Token: 0x0600194B RID: 6475 RVA: 0x00080E74 File Offset: 0x0007F074
		internal static int GetCurrentProcessIdForTdsLoginOnly()
		{
			if (TdsParserStaticMethods.s_currentProcessId == -1)
			{
				int num = new Random().Next();
				Interlocked.CompareExchange(ref TdsParserStaticMethods.s_currentProcessId, num, -1);
			}
			return TdsParserStaticMethods.s_currentProcessId;
		}

		// Token: 0x0600194C RID: 6476 RVA: 0x00080EA6 File Offset: 0x0007F0A6
		internal static int GetCurrentThreadIdForTdsLoginOnly()
		{
			return Environment.CurrentManagedThreadId;
		}

		// Token: 0x0600194D RID: 6477 RVA: 0x00080EB0 File Offset: 0x0007F0B0
		internal static byte[] GetNetworkPhysicalAddressForTdsLoginOnly()
		{
			if (TdsParserStaticMethods.s_nicAddress == null)
			{
				byte[] array = new byte[6];
				new Random().NextBytes(array);
				Interlocked.CompareExchange<byte[]>(ref TdsParserStaticMethods.s_nicAddress, array, null);
			}
			return TdsParserStaticMethods.s_nicAddress;
		}

		// Token: 0x0600194E RID: 6478 RVA: 0x00080EE8 File Offset: 0x0007F0E8
		internal static int GetTimeoutMilliseconds(long timeoutTime)
		{
			if (9223372036854775807L == timeoutTime)
			{
				return -1;
			}
			long num = ADP.TimerRemainingMilliseconds(timeoutTime);
			if (num < 0L)
			{
				return 0;
			}
			if (num > 2147483647L)
			{
				return int.MaxValue;
			}
			return (int)num;
		}

		// Token: 0x0600194F RID: 6479 RVA: 0x00080F24 File Offset: 0x0007F124
		internal static long GetTimeout(long timeoutMilliseconds)
		{
			long num;
			if (timeoutMilliseconds <= 0L)
			{
				num = long.MaxValue;
			}
			else
			{
				try
				{
					num = checked(ADP.TimerCurrent() + ADP.TimerFromMilliseconds(timeoutMilliseconds));
				}
				catch (OverflowException)
				{
					num = long.MaxValue;
				}
			}
			return num;
		}

		// Token: 0x06001950 RID: 6480 RVA: 0x00080F70 File Offset: 0x0007F170
		internal static bool TimeoutHasExpired(long timeoutTime)
		{
			bool flag = false;
			if (timeoutTime != 0L && 9223372036854775807L != timeoutTime)
			{
				flag = ADP.TimerHasExpired(timeoutTime);
			}
			return flag;
		}

		// Token: 0x06001951 RID: 6481 RVA: 0x00080F96 File Offset: 0x0007F196
		internal static int NullAwareStringLength(string str)
		{
			if (str == null)
			{
				return 0;
			}
			return str.Length;
		}

		// Token: 0x06001952 RID: 6482 RVA: 0x00080FA4 File Offset: 0x0007F1A4
		internal static int GetRemainingTimeout(int timeout, long start)
		{
			if (timeout <= 0)
			{
				return timeout;
			}
			long num = ADP.TimerRemainingSeconds(start + ADP.TimerFromSeconds(timeout));
			if (num <= 0L)
			{
				return 1;
			}
			return checked((int)num);
		}

		// Token: 0x04001227 RID: 4647
		private const int NoProcessId = -1;

		// Token: 0x04001228 RID: 4648
		private static int s_currentProcessId = -1;

		// Token: 0x04001229 RID: 4649
		private static byte[] s_nicAddress = null;
	}
}
