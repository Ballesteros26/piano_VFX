using System;

namespace Novell.Directory.Ldap.Extensions
{
	// Token: 0x02000094 RID: 148
	public class NamingContextConstants
	{
		// Token: 0x04000265 RID: 613
		public const string CREATE_NAMING_CONTEXT_REQ = "2.16.840.1.113719.1.27.100.3";

		// Token: 0x04000266 RID: 614
		public const string CREATE_NAMING_CONTEXT_RES = "2.16.840.1.113719.1.27.100.4";

		// Token: 0x04000267 RID: 615
		public const string MERGE_NAMING_CONTEXT_REQ = "2.16.840.1.113719.1.27.100.5";

		// Token: 0x04000268 RID: 616
		public const string MERGE_NAMING_CONTEXT_RES = "2.16.840.1.113719.1.27.100.6";

		// Token: 0x04000269 RID: 617
		public const string ADD_REPLICA_REQ = "2.16.840.1.113719.1.27.100.7";

		// Token: 0x0400026A RID: 618
		public const string ADD_REPLICA_RES = "2.16.840.1.113719.1.27.100.8";

		// Token: 0x0400026B RID: 619
		public const string REFRESH_SERVER_REQ = "2.16.840.1.113719.1.27.100.9";

		// Token: 0x0400026C RID: 620
		public const string REFRESH_SERVER_RES = "2.16.840.1.113719.1.27.100.10";

		// Token: 0x0400026D RID: 621
		public const string DELETE_REPLICA_REQ = "2.16.840.1.113719.1.27.100.11";

		// Token: 0x0400026E RID: 622
		public const string DELETE_REPLICA_RES = "2.16.840.1.113719.1.27.100.12";

		// Token: 0x0400026F RID: 623
		public const string NAMING_CONTEXT_COUNT_REQ = "2.16.840.1.113719.1.27.100.13";

		// Token: 0x04000270 RID: 624
		public const string NAMING_CONTEXT_COUNT_RES = "2.16.840.1.113719.1.27.100.14";

		// Token: 0x04000271 RID: 625
		public const string CHANGE_REPLICA_TYPE_REQ = "2.16.840.1.113719.1.27.100.15";

		// Token: 0x04000272 RID: 626
		public const string CHANGE_REPLICA_TYPE_RES = "2.16.840.1.113719.1.27.100.16";

		// Token: 0x04000273 RID: 627
		public const string GET_REPLICA_INFO_REQ = "2.16.840.1.113719.1.27.100.17";

		// Token: 0x04000274 RID: 628
		public const string GET_REPLICA_INFO_RES = "2.16.840.1.113719.1.27.100.18";

		// Token: 0x04000275 RID: 629
		public const string LIST_REPLICAS_REQ = "2.16.840.1.113719.1.27.100.19";

		// Token: 0x04000276 RID: 630
		public const string LIST_REPLICAS_RES = "2.16.840.1.113719.1.27.100.20";

		// Token: 0x04000277 RID: 631
		public const string RECEIVE_ALL_UPDATES_REQ = "2.16.840.1.113719.1.27.100.21";

		// Token: 0x04000278 RID: 632
		public const string RECEIVE_ALL_UPDATES_RES = "2.16.840.1.113719.1.27.100.22";

		// Token: 0x04000279 RID: 633
		public const string SEND_ALL_UPDATES_REQ = "2.16.840.1.113719.1.27.100.23";

		// Token: 0x0400027A RID: 634
		public const string SEND_ALL_UPDATES_RES = "2.16.840.1.113719.1.27.100.24";

		// Token: 0x0400027B RID: 635
		public const string NAMING_CONTEXT_SYNC_REQ = "2.16.840.1.113719.1.27.100.25";

		// Token: 0x0400027C RID: 636
		public const string NAMING_CONTEXT_SYNC_RES = "2.16.840.1.113719.1.27.100.26";

		// Token: 0x0400027D RID: 637
		public const string SCHEMA_SYNC_REQ = "2.16.840.1.113719.1.27.100.27";

		// Token: 0x0400027E RID: 638
		public const string SCHEMA_SYNC_RES = "2.16.840.1.113719.1.27.100.28";

		// Token: 0x0400027F RID: 639
		public const string ABORT_NAMING_CONTEXT_OP_REQ = "2.16.840.1.113719.1.27.100.29";

		// Token: 0x04000280 RID: 640
		public const string ABORT_NAMING_CONTEXT_OP_RES = "2.16.840.1.113719.1.27.100.30";

		// Token: 0x04000281 RID: 641
		public const string GET_IDENTITY_NAME_REQ = "2.16.840.1.113719.1.27.100.31";

		// Token: 0x04000282 RID: 642
		public const string GET_IDENTITY_NAME_RES = "2.16.840.1.113719.1.27.100.32";

		// Token: 0x04000283 RID: 643
		public const string GET_EFFECTIVE_PRIVILEGES_REQ = "2.16.840.1.113719.1.27.100.33";

		// Token: 0x04000284 RID: 644
		public const string GET_EFFECTIVE_PRIVILEGES_RES = "2.16.840.1.113719.1.27.100.34";

		// Token: 0x04000285 RID: 645
		public const string SET_REPLICATION_FILTER_REQ = "2.16.840.1.113719.1.27.100.35";

		// Token: 0x04000286 RID: 646
		public const string SET_REPLICATION_FILTER_RES = "2.16.840.1.113719.1.27.100.36";

		// Token: 0x04000287 RID: 647
		public const string GET_REPLICATION_FILTER_REQ = "2.16.840.1.113719.1.27.100.37";

		// Token: 0x04000288 RID: 648
		public const string GET_REPLICATION_FILTER_RES = "2.16.840.1.113719.1.27.100.38";

		// Token: 0x04000289 RID: 649
		public const string CREATE_ORPHAN_NAMING_CONTEXT_REQ = "2.16.840.1.113719.1.27.100.39";

		// Token: 0x0400028A RID: 650
		public const string CREATE_ORPHAN_NAMING_CONTEXT_RES = "2.16.840.1.113719.1.27.100.40";

		// Token: 0x0400028B RID: 651
		public const string REMOVE_ORPHAN_NAMING_CONTEXT_REQ = "2.16.840.1.113719.1.27.100.41";

		// Token: 0x0400028C RID: 652
		public const string REMOVE_ORPHAN_NAMING_CONTEXT_RES = "2.16.840.1.113719.1.27.100.42";

		// Token: 0x0400028D RID: 653
		public const string TRIGGER_BKLINKER_REQ = "2.16.840.1.113719.1.27.100.43";

		// Token: 0x0400028E RID: 654
		public const string TRIGGER_BKLINKER_RES = "2.16.840.1.113719.1.27.100.44";

		// Token: 0x0400028F RID: 655
		public const string TRIGGER_JANITOR_REQ = "2.16.840.1.113719.1.27.100.47";

		// Token: 0x04000290 RID: 656
		public const string TRIGGER_JANITOR_RES = "2.16.840.1.113719.1.27.100.48";

		// Token: 0x04000291 RID: 657
		public const string TRIGGER_LIMBER_REQ = "2.16.840.1.113719.1.27.100.49";

		// Token: 0x04000292 RID: 658
		public const string TRIGGER_LIMBER_RES = "2.16.840.1.113719.1.27.100.50";

		// Token: 0x04000293 RID: 659
		public const string TRIGGER_SKULKER_REQ = "2.16.840.1.113719.1.27.100.51";

		// Token: 0x04000294 RID: 660
		public const string TRIGGER_SKULKER_RES = "2.16.840.1.113719.1.27.100.52";

		// Token: 0x04000295 RID: 661
		public const string TRIGGER_SCHEMA_SYNC_REQ = "2.16.840.1.113719.1.27.100.53";

		// Token: 0x04000296 RID: 662
		public const string TRIGGER_SCHEMA_SYNC_RES = "2.16.840.1.113719.1.27.100.54";

		// Token: 0x04000297 RID: 663
		public const string TRIGGER_PART_PURGE_REQ = "2.16.840.1.113719.1.27.100.55";

		// Token: 0x04000298 RID: 664
		public const string TRIGGER_PART_PURGE_RES = "2.16.840.1.113719.1.27.100.56";

		// Token: 0x04000299 RID: 665
		public const int Ldap_ENSURE_SERVERS_UP = 1;

		// Token: 0x0400029A RID: 666
		public const int Ldap_RT_MASTER = 0;

		// Token: 0x0400029B RID: 667
		public const int Ldap_RT_SECONDARY = 1;

		// Token: 0x0400029C RID: 668
		public const int Ldap_RT_READONLY = 2;

		// Token: 0x0400029D RID: 669
		public const int Ldap_RT_SUBREF = 3;

		// Token: 0x0400029E RID: 670
		public const int Ldap_RT_SPARSE_WRITE = 4;

		// Token: 0x0400029F RID: 671
		public const int Ldap_RT_SPARSE_READ = 5;

		// Token: 0x040002A0 RID: 672
		public const int Ldap_RS_ON = 0;

		// Token: 0x040002A1 RID: 673
		public const int Ldap_RS_NEW_REPLICA = 1;

		// Token: 0x040002A2 RID: 674
		public const int Ldap_RS_DYING_REPLICA = 2;

		// Token: 0x040002A3 RID: 675
		public const int Ldap_RS_LOCKED = 3;

		// Token: 0x040002A4 RID: 676
		public const int Ldap_RS_TRANSITION_ON = 6;

		// Token: 0x040002A5 RID: 677
		public const int Ldap_RS_DEAD_REPLICA = 7;

		// Token: 0x040002A6 RID: 678
		public const int Ldap_RS_BEGIN_ADD = 8;

		// Token: 0x040002A7 RID: 679
		public const int Ldap_RS_MASTER_START = 11;

		// Token: 0x040002A8 RID: 680
		public const int Ldap_RS_MASTER_DONE = 12;

		// Token: 0x040002A9 RID: 681
		public const int Ldap_RS_SS_0 = 48;

		// Token: 0x040002AA RID: 682
		public const int Ldap_RS_SS_1 = 49;

		// Token: 0x040002AB RID: 683
		public const int Ldap_RS_JS_0 = 64;

		// Token: 0x040002AC RID: 684
		public const int Ldap_RS_JS_1 = 65;

		// Token: 0x040002AD RID: 685
		public const int Ldap_RS_JS_2 = 66;

		// Token: 0x040002AE RID: 686
		public const int Ldap_DS_FLAG_BUSY = 1;

		// Token: 0x040002AF RID: 687
		public const int Ldap_DS_FLAG_BOUNDARY = 2;
	}
}
