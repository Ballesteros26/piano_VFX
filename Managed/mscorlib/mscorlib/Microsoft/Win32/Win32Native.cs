using System;
using System.IO;
using System.Runtime.InteropServices;

namespace Microsoft.Win32
{
	// Token: 0x020000BC RID: 188
	internal static class Win32Native
	{
		// Token: 0x06000635 RID: 1589 RVA: 0x00021E0E File Offset: 0x0002000E
		public static string GetMessage(int hr)
		{
			return "Error " + hr;
		}

		// Token: 0x06000636 RID: 1590 RVA: 0x00021E20 File Offset: 0x00020020
		public static int MakeHRFromErrorCode(int errorCode)
		{
			return -2147024896 | errorCode;
		}

		// Token: 0x06000637 RID: 1591
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern uint GetCurrentProcessId();

		// Token: 0x0400065A RID: 1626
		internal const string ADVAPI32 = "advapi32.dll";

		// Token: 0x0400065B RID: 1627
		internal const int ERROR_SUCCESS = 0;

		// Token: 0x0400065C RID: 1628
		internal const int ERROR_INVALID_FUNCTION = 1;

		// Token: 0x0400065D RID: 1629
		internal const int ERROR_FILE_NOT_FOUND = 2;

		// Token: 0x0400065E RID: 1630
		internal const int ERROR_PATH_NOT_FOUND = 3;

		// Token: 0x0400065F RID: 1631
		internal const int ERROR_ACCESS_DENIED = 5;

		// Token: 0x04000660 RID: 1632
		internal const int ERROR_INVALID_HANDLE = 6;

		// Token: 0x04000661 RID: 1633
		internal const int ERROR_NOT_ENOUGH_MEMORY = 8;

		// Token: 0x04000662 RID: 1634
		internal const int ERROR_INVALID_DATA = 13;

		// Token: 0x04000663 RID: 1635
		internal const int ERROR_INVALID_DRIVE = 15;

		// Token: 0x04000664 RID: 1636
		internal const int ERROR_NO_MORE_FILES = 18;

		// Token: 0x04000665 RID: 1637
		internal const int ERROR_NOT_READY = 21;

		// Token: 0x04000666 RID: 1638
		internal const int ERROR_BAD_LENGTH = 24;

		// Token: 0x04000667 RID: 1639
		internal const int ERROR_SHARING_VIOLATION = 32;

		// Token: 0x04000668 RID: 1640
		internal const int ERROR_NOT_SUPPORTED = 50;

		// Token: 0x04000669 RID: 1641
		internal const int ERROR_FILE_EXISTS = 80;

		// Token: 0x0400066A RID: 1642
		internal const int ERROR_INVALID_PARAMETER = 87;

		// Token: 0x0400066B RID: 1643
		internal const int ERROR_BROKEN_PIPE = 109;

		// Token: 0x0400066C RID: 1644
		internal const int ERROR_CALL_NOT_IMPLEMENTED = 120;

		// Token: 0x0400066D RID: 1645
		internal const int ERROR_INSUFFICIENT_BUFFER = 122;

		// Token: 0x0400066E RID: 1646
		internal const int ERROR_INVALID_NAME = 123;

		// Token: 0x0400066F RID: 1647
		internal const int ERROR_BAD_PATHNAME = 161;

		// Token: 0x04000670 RID: 1648
		internal const int ERROR_ALREADY_EXISTS = 183;

		// Token: 0x04000671 RID: 1649
		internal const int ERROR_ENVVAR_NOT_FOUND = 203;

		// Token: 0x04000672 RID: 1650
		internal const int ERROR_FILENAME_EXCED_RANGE = 206;

		// Token: 0x04000673 RID: 1651
		internal const int ERROR_NO_DATA = 232;

		// Token: 0x04000674 RID: 1652
		internal const int ERROR_PIPE_NOT_CONNECTED = 233;

		// Token: 0x04000675 RID: 1653
		internal const int ERROR_MORE_DATA = 234;

		// Token: 0x04000676 RID: 1654
		internal const int ERROR_DIRECTORY = 267;

		// Token: 0x04000677 RID: 1655
		internal const int ERROR_OPERATION_ABORTED = 995;

		// Token: 0x04000678 RID: 1656
		internal const int ERROR_NOT_FOUND = 1168;

		// Token: 0x04000679 RID: 1657
		internal const int ERROR_NO_TOKEN = 1008;

		// Token: 0x0400067A RID: 1658
		internal const int ERROR_DLL_INIT_FAILED = 1114;

		// Token: 0x0400067B RID: 1659
		internal const int ERROR_NON_ACCOUNT_SID = 1257;

		// Token: 0x0400067C RID: 1660
		internal const int ERROR_NOT_ALL_ASSIGNED = 1300;

		// Token: 0x0400067D RID: 1661
		internal const int ERROR_UNKNOWN_REVISION = 1305;

		// Token: 0x0400067E RID: 1662
		internal const int ERROR_INVALID_OWNER = 1307;

		// Token: 0x0400067F RID: 1663
		internal const int ERROR_INVALID_PRIMARY_GROUP = 1308;

		// Token: 0x04000680 RID: 1664
		internal const int ERROR_NO_SUCH_PRIVILEGE = 1313;

		// Token: 0x04000681 RID: 1665
		internal const int ERROR_PRIVILEGE_NOT_HELD = 1314;

		// Token: 0x04000682 RID: 1666
		internal const int ERROR_NONE_MAPPED = 1332;

		// Token: 0x04000683 RID: 1667
		internal const int ERROR_INVALID_ACL = 1336;

		// Token: 0x04000684 RID: 1668
		internal const int ERROR_INVALID_SID = 1337;

		// Token: 0x04000685 RID: 1669
		internal const int ERROR_INVALID_SECURITY_DESCR = 1338;

		// Token: 0x04000686 RID: 1670
		internal const int ERROR_BAD_IMPERSONATION_LEVEL = 1346;

		// Token: 0x04000687 RID: 1671
		internal const int ERROR_CANT_OPEN_ANONYMOUS = 1347;

		// Token: 0x04000688 RID: 1672
		internal const int ERROR_NO_SECURITY_ON_OBJECT = 1350;

		// Token: 0x04000689 RID: 1673
		internal const int ERROR_TRUSTED_RELATIONSHIP_FAILURE = 1789;

		// Token: 0x0400068A RID: 1674
		internal const FileAttributes FILE_ATTRIBUTE_DIRECTORY = FileAttributes.Directory;

		// Token: 0x020000BD RID: 189
		public class SECURITY_ATTRIBUTES
		{
		}

		// Token: 0x020000BE RID: 190
		internal class WIN32_FIND_DATA
		{
			// Token: 0x0400068B RID: 1675
			internal int dwFileAttributes;

			// Token: 0x0400068C RID: 1676
			internal string cFileName;
		}
	}
}
