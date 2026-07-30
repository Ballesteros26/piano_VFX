using System;

namespace Novell.Directory.Ldap.Extensions
{
	// Token: 0x0200009C RID: 156
	public class ReplicationConstants
	{
		// Token: 0x040002B1 RID: 689
		public const string CREATE_NAMING_CONTEXT_REQ = "2.16.840.1.113719.1.27.100.3";

		// Token: 0x040002B2 RID: 690
		public const string CREATE_NAMING_CONTEXT_RES = "2.16.840.1.113719.1.27.100.4";

		// Token: 0x040002B3 RID: 691
		public const string MERGE_NAMING_CONTEXT_REQ = "2.16.840.1.113719.1.27.100.5";

		// Token: 0x040002B4 RID: 692
		public const string MERGE_NAMING_CONTEXT_RES = "2.16.840.1.113719.1.27.100.6";

		// Token: 0x040002B5 RID: 693
		public const string ADD_REPLICA_REQ = "2.16.840.1.113719.1.27.100.7";

		// Token: 0x040002B6 RID: 694
		public const string ADD_REPLICA_RES = "2.16.840.1.113719.1.27.100.8";

		// Token: 0x040002B7 RID: 695
		public const string REFRESH_SERVER_REQ = "2.16.840.1.113719.1.27.100.9";

		// Token: 0x040002B8 RID: 696
		public const string REFRESH_SERVER_RES = "2.16.840.1.113719.1.27.100.10";

		// Token: 0x040002B9 RID: 697
		public const string DELETE_REPLICA_REQ = "2.16.840.1.113719.1.27.100.11";

		// Token: 0x040002BA RID: 698
		public const string DELETE_REPLICA_RES = "2.16.840.1.113719.1.27.100.12";

		// Token: 0x040002BB RID: 699
		public const string NAMING_CONTEXT_COUNT_REQ = "2.16.840.1.113719.1.27.100.13";

		// Token: 0x040002BC RID: 700
		public const string NAMING_CONTEXT_COUNT_RES = "2.16.840.1.113719.1.27.100.14";

		// Token: 0x040002BD RID: 701
		public const string CHANGE_REPLICA_TYPE_REQ = "2.16.840.1.113719.1.27.100.15";

		// Token: 0x040002BE RID: 702
		public const string CHANGE_REPLICA_TYPE_RES = "2.16.840.1.113719.1.27.100.16";

		// Token: 0x040002BF RID: 703
		public const string GET_REPLICA_INFO_REQ = "2.16.840.1.113719.1.27.100.17";

		// Token: 0x040002C0 RID: 704
		public const string GET_REPLICA_INFO_RES = "2.16.840.1.113719.1.27.100.18";

		// Token: 0x040002C1 RID: 705
		public const string LIST_REPLICAS_REQ = "2.16.840.1.113719.1.27.100.19";

		// Token: 0x040002C2 RID: 706
		public const string LIST_REPLICAS_RES = "2.16.840.1.113719.1.27.100.20";

		// Token: 0x040002C3 RID: 707
		public const string RECEIVE_ALL_UPDATES_REQ = "2.16.840.1.113719.1.27.100.21";

		// Token: 0x040002C4 RID: 708
		public const string RECEIVE_ALL_UPDATES_RES = "2.16.840.1.113719.1.27.100.22";

		// Token: 0x040002C5 RID: 709
		public const string SEND_ALL_UPDATES_REQ = "2.16.840.1.113719.1.27.100.23";

		// Token: 0x040002C6 RID: 710
		public const string SEND_ALL_UPDATES_RES = "2.16.840.1.113719.1.27.100.24";

		// Token: 0x040002C7 RID: 711
		public const string NAMING_CONTEXT_SYNC_REQ = "2.16.840.1.113719.1.27.100.25";

		// Token: 0x040002C8 RID: 712
		public const string NAMING_CONTEXT_SYNC_RES = "2.16.840.1.113719.1.27.100.26";

		// Token: 0x040002C9 RID: 713
		public const string SCHEMA_SYNC_REQ = "2.16.840.1.113719.1.27.100.27";

		// Token: 0x040002CA RID: 714
		public const string SCHEMA_SYNC_RES = "2.16.840.1.113719.1.27.100.28";

		// Token: 0x040002CB RID: 715
		public const string ABORT_NAMING_CONTEXT_OP_REQ = "2.16.840.1.113719.1.27.100.29";

		// Token: 0x040002CC RID: 716
		public const string ABORT_NAMING_CONTEXT_OP_RES = "2.16.840.1.113719.1.27.100.30";

		// Token: 0x040002CD RID: 717
		public const string GET_IDENTITY_NAME_REQ = "2.16.840.1.113719.1.27.100.31";

		// Token: 0x040002CE RID: 718
		public const string GET_IDENTITY_NAME_RES = "2.16.840.1.113719.1.27.100.32";

		// Token: 0x040002CF RID: 719
		public const string GET_EFFECTIVE_PRIVILEGES_REQ = "2.16.840.1.113719.1.27.100.33";

		// Token: 0x040002D0 RID: 720
		public const string GET_EFFECTIVE_PRIVILEGES_RES = "2.16.840.1.113719.1.27.100.34";

		// Token: 0x040002D1 RID: 721
		public const string SET_REPLICATION_FILTER_REQ = "2.16.840.1.113719.1.27.100.35";

		// Token: 0x040002D2 RID: 722
		public const string SET_REPLICATION_FILTER_RES = "2.16.840.1.113719.1.27.100.36";

		// Token: 0x040002D3 RID: 723
		public const string GET_REPLICATION_FILTER_REQ = "2.16.840.1.113719.1.27.100.37";

		// Token: 0x040002D4 RID: 724
		public const string GET_REPLICATION_FILTER_RES = "2.16.840.1.113719.1.27.100.38";

		// Token: 0x040002D5 RID: 725
		public const string CREATE_ORPHAN_NAMING_CONTEXT_REQ = "2.16.840.1.113719.1.27.100.39";

		// Token: 0x040002D6 RID: 726
		public const string CREATE_ORPHAN_NAMING_CONTEXT_RES = "2.16.840.1.113719.1.27.100.40";

		// Token: 0x040002D7 RID: 727
		public const string REMOVE_ORPHAN_NAMING_CONTEXT_REQ = "2.16.840.1.113719.1.27.100.41";

		// Token: 0x040002D8 RID: 728
		public const string REMOVE_ORPHAN_NAMING_CONTEXT_RES = "2.16.840.1.113719.1.27.100.42";

		// Token: 0x040002D9 RID: 729
		public const string TRIGGER_BKLINKER_REQ = "2.16.840.1.113719.1.27.100.43";

		// Token: 0x040002DA RID: 730
		public const string TRIGGER_BKLINKER_RES = "2.16.840.1.113719.1.27.100.44";

		// Token: 0x040002DB RID: 731
		public const string TRIGGER_JANITOR_REQ = "2.16.840.1.113719.1.27.100.47";

		// Token: 0x040002DC RID: 732
		public const string TRIGGER_JANITOR_RES = "2.16.840.1.113719.1.27.100.48";

		// Token: 0x040002DD RID: 733
		public const string TRIGGER_LIMBER_REQ = "2.16.840.1.113719.1.27.100.49";

		// Token: 0x040002DE RID: 734
		public const string TRIGGER_LIMBER_RES = "2.16.840.1.113719.1.27.100.50";

		// Token: 0x040002DF RID: 735
		public const string TRIGGER_SKULKER_REQ = "2.16.840.1.113719.1.27.100.51";

		// Token: 0x040002E0 RID: 736
		public const string TRIGGER_SKULKER_RES = "2.16.840.1.113719.1.27.100.52";

		// Token: 0x040002E1 RID: 737
		public const string TRIGGER_SCHEMA_SYNC_REQ = "2.16.840.1.113719.1.27.100.53";

		// Token: 0x040002E2 RID: 738
		public const string TRIGGER_SCHEMA_SYNC_RES = "2.16.840.1.113719.1.27.100.54";

		// Token: 0x040002E3 RID: 739
		public const string TRIGGER_PART_PURGE_REQ = "2.16.840.1.113719.1.27.100.55";

		// Token: 0x040002E4 RID: 740
		public const string TRIGGER_PART_PURGE_RES = "2.16.840.1.113719.1.27.100.56";

		// Token: 0x040002E5 RID: 741
		public const int Ldap_ENSURE_SERVERS_UP = 1;

		// Token: 0x040002E6 RID: 742
		public const int Ldap_RT_MASTER = 0;

		// Token: 0x040002E7 RID: 743
		public const int Ldap_RT_SECONDARY = 1;

		// Token: 0x040002E8 RID: 744
		public const int Ldap_RT_READONLY = 2;

		// Token: 0x040002E9 RID: 745
		public const int Ldap_RT_SUBREF = 3;

		// Token: 0x040002EA RID: 746
		public const int Ldap_RT_SPARSE_WRITE = 4;

		// Token: 0x040002EB RID: 747
		public const int Ldap_RT_SPARSE_READ = 5;

		// Token: 0x040002EC RID: 748
		public const int Ldap_RS_ON = 0;

		// Token: 0x040002ED RID: 749
		public const int Ldap_RS_NEW_REPLICA = 1;

		// Token: 0x040002EE RID: 750
		public const int Ldap_RS_DYING_REPLICA = 2;

		// Token: 0x040002EF RID: 751
		public const int Ldap_RS_LOCKED = 3;

		// Token: 0x040002F0 RID: 752
		public const int Ldap_RS_TRANSITION_ON = 6;

		// Token: 0x040002F1 RID: 753
		public const int Ldap_RS_DEAD_REPLICA = 7;

		// Token: 0x040002F2 RID: 754
		public const int Ldap_RS_BEGIN_ADD = 8;

		// Token: 0x040002F3 RID: 755
		public const int Ldap_RS_MASTER_START = 11;

		// Token: 0x040002F4 RID: 756
		public const int Ldap_RS_MASTER_DONE = 12;

		// Token: 0x040002F5 RID: 757
		public const int Ldap_RS_SS_0 = 48;

		// Token: 0x040002F6 RID: 758
		public const int Ldap_RS_SS_1 = 49;

		// Token: 0x040002F7 RID: 759
		public const int Ldap_RS_JS_0 = 64;

		// Token: 0x040002F8 RID: 760
		public const int Ldap_RS_JS_1 = 65;

		// Token: 0x040002F9 RID: 761
		public const int Ldap_RS_JS_2 = 66;

		// Token: 0x040002FA RID: 762
		public const int Ldap_DS_FLAG_BUSY = 1;

		// Token: 0x040002FB RID: 763
		public const int Ldap_DS_FLAG_BOUNDARY = 2;
	}
}
