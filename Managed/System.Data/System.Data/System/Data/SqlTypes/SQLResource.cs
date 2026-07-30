using System;

namespace System.Data.SqlTypes
{
	// Token: 0x020002B9 RID: 697
	internal static class SQLResource
	{
		// Token: 0x1700056E RID: 1390
		// (get) Token: 0x06001DAE RID: 7598 RVA: 0x000922CF File Offset: 0x000904CF
		internal static string NullString
		{
			get
			{
				return "Null";
			}
		}

		// Token: 0x1700056F RID: 1391
		// (get) Token: 0x06001DAF RID: 7599 RVA: 0x000922D6 File Offset: 0x000904D6
		internal static string MessageString
		{
			get
			{
				return "Message";
			}
		}

		// Token: 0x17000570 RID: 1392
		// (get) Token: 0x06001DB0 RID: 7600 RVA: 0x000922DD File Offset: 0x000904DD
		internal static string ArithOverflowMessage
		{
			get
			{
				return "Arithmetic Overflow.";
			}
		}

		// Token: 0x17000571 RID: 1393
		// (get) Token: 0x06001DB1 RID: 7601 RVA: 0x000922E4 File Offset: 0x000904E4
		internal static string DivideByZeroMessage
		{
			get
			{
				return "Divide by zero error encountered.";
			}
		}

		// Token: 0x17000572 RID: 1394
		// (get) Token: 0x06001DB2 RID: 7602 RVA: 0x000922EB File Offset: 0x000904EB
		internal static string NullValueMessage
		{
			get
			{
				return "Data is Null. This method or property cannot be called on Null values.";
			}
		}

		// Token: 0x17000573 RID: 1395
		// (get) Token: 0x06001DB3 RID: 7603 RVA: 0x000922F2 File Offset: 0x000904F2
		internal static string TruncationMessage
		{
			get
			{
				return "Numeric arithmetic causes truncation.";
			}
		}

		// Token: 0x17000574 RID: 1396
		// (get) Token: 0x06001DB4 RID: 7604 RVA: 0x000922F9 File Offset: 0x000904F9
		internal static string DateTimeOverflowMessage
		{
			get
			{
				return "SqlDateTime overflow. Must be between 1/1/1753 12:00:00 AM and 12/31/9999 11:59:59 PM.";
			}
		}

		// Token: 0x17000575 RID: 1397
		// (get) Token: 0x06001DB5 RID: 7605 RVA: 0x00092300 File Offset: 0x00090500
		internal static string ConcatDiffCollationMessage
		{
			get
			{
				return "Two strings to be concatenated have different collation.";
			}
		}

		// Token: 0x17000576 RID: 1398
		// (get) Token: 0x06001DB6 RID: 7606 RVA: 0x00092307 File Offset: 0x00090507
		internal static string CompareDiffCollationMessage
		{
			get
			{
				return "Two strings to be compared have different collation.";
			}
		}

		// Token: 0x17000577 RID: 1399
		// (get) Token: 0x06001DB7 RID: 7607 RVA: 0x0009230E File Offset: 0x0009050E
		internal static string InvalidFlagMessage
		{
			get
			{
				return "Invalid flag value.";
			}
		}

		// Token: 0x17000578 RID: 1400
		// (get) Token: 0x06001DB8 RID: 7608 RVA: 0x00092315 File Offset: 0x00090515
		internal static string NumeToDecOverflowMessage
		{
			get
			{
				return "Conversion from SqlDecimal to Decimal overflows.";
			}
		}

		// Token: 0x17000579 RID: 1401
		// (get) Token: 0x06001DB9 RID: 7609 RVA: 0x0009231C File Offset: 0x0009051C
		internal static string ConversionOverflowMessage
		{
			get
			{
				return "Conversion overflows.";
			}
		}

		// Token: 0x1700057A RID: 1402
		// (get) Token: 0x06001DBA RID: 7610 RVA: 0x00092323 File Offset: 0x00090523
		internal static string InvalidDateTimeMessage
		{
			get
			{
				return "Invalid SqlDateTime.";
			}
		}

		// Token: 0x1700057B RID: 1403
		// (get) Token: 0x06001DBB RID: 7611 RVA: 0x0009232A File Offset: 0x0009052A
		internal static string TimeZoneSpecifiedMessage
		{
			get
			{
				return "A time zone was specified. SqlDateTime does not support time zones.";
			}
		}

		// Token: 0x1700057C RID: 1404
		// (get) Token: 0x06001DBC RID: 7612 RVA: 0x00092331 File Offset: 0x00090531
		internal static string InvalidArraySizeMessage
		{
			get
			{
				return "Invalid array size.";
			}
		}

		// Token: 0x1700057D RID: 1405
		// (get) Token: 0x06001DBD RID: 7613 RVA: 0x00092338 File Offset: 0x00090538
		internal static string InvalidPrecScaleMessage
		{
			get
			{
				return "Invalid numeric precision/scale.";
			}
		}

		// Token: 0x1700057E RID: 1406
		// (get) Token: 0x06001DBE RID: 7614 RVA: 0x0009233F File Offset: 0x0009053F
		internal static string FormatMessage
		{
			get
			{
				return "The input wasn't in a correct format.";
			}
		}

		// Token: 0x1700057F RID: 1407
		// (get) Token: 0x06001DBF RID: 7615 RVA: 0x00092346 File Offset: 0x00090546
		internal static string NotFilledMessage
		{
			get
			{
				return "SQL Type has not been loaded with data.";
			}
		}

		// Token: 0x17000580 RID: 1408
		// (get) Token: 0x06001DC0 RID: 7616 RVA: 0x0009234D File Offset: 0x0009054D
		internal static string AlreadyFilledMessage
		{
			get
			{
				return "SQL Type has already been loaded with data.";
			}
		}

		// Token: 0x17000581 RID: 1409
		// (get) Token: 0x06001DC1 RID: 7617 RVA: 0x00092354 File Offset: 0x00090554
		internal static string ClosedXmlReaderMessage
		{
			get
			{
				return "Invalid attempt to access a closed XmlReader.";
			}
		}

		// Token: 0x06001DC2 RID: 7618 RVA: 0x0009235B File Offset: 0x0009055B
		internal static string InvalidOpStreamClosed(string method)
		{
			return SR.Format("Invalid attempt to call {0} when the stream is closed.", method);
		}

		// Token: 0x06001DC3 RID: 7619 RVA: 0x00092368 File Offset: 0x00090568
		internal static string InvalidOpStreamNonWritable(string method)
		{
			return SR.Format("Invalid attempt to call {0} when the stream non-writable.", method);
		}

		// Token: 0x06001DC4 RID: 7620 RVA: 0x00092375 File Offset: 0x00090575
		internal static string InvalidOpStreamNonReadable(string method)
		{
			return SR.Format("Invalid attempt to call {0} when the stream non-readable.", method);
		}

		// Token: 0x06001DC5 RID: 7621 RVA: 0x00092382 File Offset: 0x00090582
		internal static string InvalidOpStreamNonSeekable(string method)
		{
			return SR.Format("Invalid attempt to call {0} when the stream is non-seekable.", method);
		}
	}
}
