using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Mono.Data.Sqlite
{
	// Token: 0x02000035 RID: 53
	[SuppressUnmanagedCodeSecurity]
	internal static class UnsafeNativeMethods
	{
		// Token: 0x06000295 RID: 661 RVA: 0x0000E470 File Offset: 0x0000C670
		static UnsafeNativeMethods()
		{
			int num = UnsafeNativeMethods.sqlite3_libversion_number();
			int num2 = num % 1000;
			int num3 = num / 1000 % 1000;
			Version version = new Version(num / 1000000, num3, num2);
			UnsafeNativeMethods.use_sqlite3_open_v2 = version >= new Version(3, 5, 0);
			UnsafeNativeMethods.use_sqlite3_close_v2 = version >= new Version(3, 7, 14);
			UnsafeNativeMethods.use_sqlite3_create_function_v2 = version >= new Version(3, 7, 3);
		}

		// Token: 0x06000296 RID: 662
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int sqlite3_close(IntPtr db);

		// Token: 0x06000297 RID: 663
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int sqlite3_close_v2(IntPtr db);

		// Token: 0x06000298 RID: 664
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int sqlite3_create_function(IntPtr db, byte[] strName, int nArgs, int nType, IntPtr pvUser, SQLiteCallback func, SQLiteCallback fstep, SQLiteFinalCallback ffinal);

		// Token: 0x06000299 RID: 665
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int sqlite3_create_function_v2(IntPtr db, byte[] strName, int nArgs, int nType, IntPtr pvUser, SQLiteCallback func, SQLiteCallback fstep, SQLiteFinalCallback ffinal, SQLiteFinalCallback fdestroy);

		// Token: 0x0600029A RID: 666
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int sqlite3_finalize(IntPtr stmt);

		// Token: 0x0600029B RID: 667
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int sqlite3_open_v2(byte[] utf8Filename, out IntPtr db, int flags, IntPtr vfs);

		// Token: 0x0600029C RID: 668
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int sqlite3_open(byte[] utf8Filename, out IntPtr db);

		// Token: 0x0600029D RID: 669
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		internal static extern int sqlite3_open16(string fileName, out IntPtr db);

		// Token: 0x0600029E RID: 670
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int sqlite3_reset(IntPtr stmt);

		// Token: 0x0600029F RID: 671
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr sqlite3_bind_parameter_name(IntPtr stmt, int index);

		// Token: 0x060002A0 RID: 672
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr sqlite3_column_database_name(IntPtr stmt, int index);

		// Token: 0x060002A1 RID: 673
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr sqlite3_column_database_name16(IntPtr stmt, int index);

		// Token: 0x060002A2 RID: 674
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr sqlite3_column_decltype(IntPtr stmt, int index);

		// Token: 0x060002A3 RID: 675
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr sqlite3_column_decltype16(IntPtr stmt, int index);

		// Token: 0x060002A4 RID: 676
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr sqlite3_column_name(IntPtr stmt, int index);

		// Token: 0x060002A5 RID: 677
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr sqlite3_column_name16(IntPtr stmt, int index);

		// Token: 0x060002A6 RID: 678
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr sqlite3_column_origin_name(IntPtr stmt, int index);

		// Token: 0x060002A7 RID: 679
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr sqlite3_column_origin_name16(IntPtr stmt, int index);

		// Token: 0x060002A8 RID: 680
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr sqlite3_column_table_name(IntPtr stmt, int index);

		// Token: 0x060002A9 RID: 681
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr sqlite3_column_table_name16(IntPtr stmt, int index);

		// Token: 0x060002AA RID: 682
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr sqlite3_column_text(IntPtr stmt, int index);

		// Token: 0x060002AB RID: 683
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr sqlite3_column_text16(IntPtr stmt, int index);

		// Token: 0x060002AC RID: 684
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr sqlite3_errmsg(IntPtr db);

		// Token: 0x060002AD RID: 685
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int sqlite3_prepare(IntPtr db, IntPtr pSql, int nBytes, out IntPtr stmt, out IntPtr ptrRemain);

		// Token: 0x060002AE RID: 686
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int sqlite3_table_column_metadata(IntPtr db, byte[] dbName, byte[] tblName, byte[] colName, out IntPtr ptrDataType, out IntPtr ptrCollSeq, out int notNull, out int primaryKey, out int autoInc);

		// Token: 0x060002AF RID: 687
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr sqlite3_value_text(IntPtr p);

		// Token: 0x060002B0 RID: 688
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr sqlite3_value_text16(IntPtr p);

		// Token: 0x060002B1 RID: 689
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr sqlite3_libversion();

		// Token: 0x060002B2 RID: 690
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern void sqlite3_interrupt(IntPtr db);

		// Token: 0x060002B3 RID: 691
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int sqlite3_changes(IntPtr db);

		// Token: 0x060002B4 RID: 692
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int sqlite3_busy_timeout(IntPtr db, int ms);

		// Token: 0x060002B5 RID: 693
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int sqlite3_bind_blob(IntPtr stmt, int index, byte[] value, int nSize, IntPtr nTransient);

		// Token: 0x060002B6 RID: 694
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int sqlite3_bind_double(IntPtr stmt, int index, double value);

		// Token: 0x060002B7 RID: 695
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int sqlite3_bind_int(IntPtr stmt, int index, int value);

		// Token: 0x060002B8 RID: 696
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int sqlite3_bind_int64(IntPtr stmt, int index, long value);

		// Token: 0x060002B9 RID: 697
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int sqlite3_bind_null(IntPtr stmt, int index);

		// Token: 0x060002BA RID: 698
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int sqlite3_bind_text(IntPtr stmt, int index, byte[] value, int nlen, IntPtr pvReserved);

		// Token: 0x060002BB RID: 699
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int sqlite3_bind_parameter_count(IntPtr stmt);

		// Token: 0x060002BC RID: 700
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int sqlite3_bind_parameter_index(IntPtr stmt, byte[] strName);

		// Token: 0x060002BD RID: 701
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int sqlite3_column_count(IntPtr stmt);

		// Token: 0x060002BE RID: 702
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int sqlite3_step(IntPtr stmt);

		// Token: 0x060002BF RID: 703
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern double sqlite3_column_double(IntPtr stmt, int index);

		// Token: 0x060002C0 RID: 704
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int sqlite3_column_int(IntPtr stmt, int index);

		// Token: 0x060002C1 RID: 705
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern long sqlite3_column_int64(IntPtr stmt, int index);

		// Token: 0x060002C2 RID: 706
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr sqlite3_column_blob(IntPtr stmt, int index);

		// Token: 0x060002C3 RID: 707
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int sqlite3_column_bytes(IntPtr stmt, int index);

		// Token: 0x060002C4 RID: 708
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern TypeAffinity sqlite3_column_type(IntPtr stmt, int index);

		// Token: 0x060002C5 RID: 709
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int sqlite3_create_collation(IntPtr db, byte[] strName, int nType, IntPtr pvUser, SQLiteCollation func);

		// Token: 0x060002C6 RID: 710
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int sqlite3_aggregate_count(IntPtr context);

		// Token: 0x060002C7 RID: 711
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr sqlite3_value_blob(IntPtr p);

		// Token: 0x060002C8 RID: 712
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int sqlite3_value_bytes(IntPtr p);

		// Token: 0x060002C9 RID: 713
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern double sqlite3_value_double(IntPtr p);

		// Token: 0x060002CA RID: 714
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int sqlite3_value_int(IntPtr p);

		// Token: 0x060002CB RID: 715
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern long sqlite3_value_int64(IntPtr p);

		// Token: 0x060002CC RID: 716
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern TypeAffinity sqlite3_value_type(IntPtr p);

		// Token: 0x060002CD RID: 717
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern void sqlite3_result_blob(IntPtr context, byte[] value, int nSize, IntPtr pvReserved);

		// Token: 0x060002CE RID: 718
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern void sqlite3_result_double(IntPtr context, double value);

		// Token: 0x060002CF RID: 719
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern void sqlite3_result_error(IntPtr context, byte[] strErr, int nLen);

		// Token: 0x060002D0 RID: 720
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern void sqlite3_result_int(IntPtr context, int value);

		// Token: 0x060002D1 RID: 721
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern void sqlite3_result_int64(IntPtr context, long value);

		// Token: 0x060002D2 RID: 722
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern void sqlite3_result_null(IntPtr context);

		// Token: 0x060002D3 RID: 723
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern void sqlite3_result_text(IntPtr context, byte[] value, int nLen, IntPtr pvReserved);

		// Token: 0x060002D4 RID: 724
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr sqlite3_aggregate_context(IntPtr context, int nBytes);

		// Token: 0x060002D5 RID: 725
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		internal static extern int sqlite3_bind_text16(IntPtr stmt, int index, string value, int nlen, IntPtr pvReserved);

		// Token: 0x060002D6 RID: 726
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		internal static extern void sqlite3_result_error16(IntPtr context, string strName, int nLen);

		// Token: 0x060002D7 RID: 727
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		internal static extern void sqlite3_result_text16(IntPtr context, string strName, int nLen, IntPtr pvReserved);

		// Token: 0x060002D8 RID: 728
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int sqlite3_key(IntPtr db, byte[] key, int keylen);

		// Token: 0x060002D9 RID: 729
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int sqlite3_rekey(IntPtr db, byte[] key, int keylen);

		// Token: 0x060002DA RID: 730
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr sqlite3_update_hook(IntPtr db, SQLiteUpdateCallback func, IntPtr pvUser);

		// Token: 0x060002DB RID: 731
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr sqlite3_commit_hook(IntPtr db, SQLiteCommitCallback func, IntPtr pvUser);

		// Token: 0x060002DC RID: 732
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr sqlite3_rollback_hook(IntPtr db, SQLiteRollbackCallback func, IntPtr pvUser);

		// Token: 0x060002DD RID: 733
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr sqlite3_db_handle(IntPtr stmt);

		// Token: 0x060002DE RID: 734
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr sqlite3_next_stmt(IntPtr db, IntPtr stmt);

		// Token: 0x060002DF RID: 735
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int sqlite3_exec(IntPtr db, byte[] strSql, IntPtr pvCallback, IntPtr pvParam, out IntPtr errMsg);

		// Token: 0x060002E0 RID: 736
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int sqlite3_config(SQLiteConfig config);

		// Token: 0x060002E1 RID: 737
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr sqlite3_user_data(IntPtr context);

		// Token: 0x060002E2 RID: 738
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int sqlite3_free(IntPtr ptr);

		// Token: 0x060002E3 RID: 739
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int sqlite3_libversion_number();

		// Token: 0x04000108 RID: 264
		internal static readonly bool use_sqlite3_close_v2;

		// Token: 0x04000109 RID: 265
		internal static readonly bool use_sqlite3_open_v2;

		// Token: 0x0400010A RID: 266
		internal static readonly bool use_sqlite3_create_function_v2;

		// Token: 0x0400010B RID: 267
		private const string SQLITE_DLL = "sqlite3";
	}
}
