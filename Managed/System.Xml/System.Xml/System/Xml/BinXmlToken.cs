using System;

namespace System.Xml
{
	// Token: 0x0200007D RID: 125
	internal enum BinXmlToken
	{
		// Token: 0x0400023F RID: 575
		Error,
		// Token: 0x04000240 RID: 576
		NotImpl = -2,
		// Token: 0x04000241 RID: 577
		EOF,
		// Token: 0x04000242 RID: 578
		XmlDecl = 254,
		// Token: 0x04000243 RID: 579
		Encoding = 253,
		// Token: 0x04000244 RID: 580
		DocType = 252,
		// Token: 0x04000245 RID: 581
		System = 251,
		// Token: 0x04000246 RID: 582
		Public = 250,
		// Token: 0x04000247 RID: 583
		Subset = 249,
		// Token: 0x04000248 RID: 584
		Element = 248,
		// Token: 0x04000249 RID: 585
		EndElem = 247,
		// Token: 0x0400024A RID: 586
		Attr = 246,
		// Token: 0x0400024B RID: 587
		EndAttrs = 245,
		// Token: 0x0400024C RID: 588
		PI = 244,
		// Token: 0x0400024D RID: 589
		Comment = 243,
		// Token: 0x0400024E RID: 590
		CData = 242,
		// Token: 0x0400024F RID: 591
		EndCData = 241,
		// Token: 0x04000250 RID: 592
		Name = 240,
		// Token: 0x04000251 RID: 593
		QName = 239,
		// Token: 0x04000252 RID: 594
		XmlText = 237,
		// Token: 0x04000253 RID: 595
		Nest = 236,
		// Token: 0x04000254 RID: 596
		EndNest = 235,
		// Token: 0x04000255 RID: 597
		Extn = 234,
		// Token: 0x04000256 RID: 598
		NmFlush = 233,
		// Token: 0x04000257 RID: 599
		SQL_BIT = 6,
		// Token: 0x04000258 RID: 600
		SQL_TINYINT,
		// Token: 0x04000259 RID: 601
		SQL_SMALLINT = 1,
		// Token: 0x0400025A RID: 602
		SQL_INT,
		// Token: 0x0400025B RID: 603
		SQL_BIGINT = 8,
		// Token: 0x0400025C RID: 604
		SQL_REAL = 3,
		// Token: 0x0400025D RID: 605
		SQL_FLOAT,
		// Token: 0x0400025E RID: 606
		SQL_MONEY,
		// Token: 0x0400025F RID: 607
		SQL_SMALLMONEY = 20,
		// Token: 0x04000260 RID: 608
		SQL_DATETIME = 18,
		// Token: 0x04000261 RID: 609
		SQL_SMALLDATETIME,
		// Token: 0x04000262 RID: 610
		SQL_DECIMAL = 10,
		// Token: 0x04000263 RID: 611
		SQL_NUMERIC,
		// Token: 0x04000264 RID: 612
		SQL_UUID = 9,
		// Token: 0x04000265 RID: 613
		SQL_VARBINARY = 15,
		// Token: 0x04000266 RID: 614
		SQL_BINARY = 12,
		// Token: 0x04000267 RID: 615
		SQL_IMAGE = 23,
		// Token: 0x04000268 RID: 616
		SQL_CHAR = 13,
		// Token: 0x04000269 RID: 617
		SQL_VARCHAR = 16,
		// Token: 0x0400026A RID: 618
		SQL_TEXT = 22,
		// Token: 0x0400026B RID: 619
		SQL_NVARCHAR = 17,
		// Token: 0x0400026C RID: 620
		SQL_NCHAR = 14,
		// Token: 0x0400026D RID: 621
		SQL_NTEXT = 24,
		// Token: 0x0400026E RID: 622
		SQL_UDT = 27,
		// Token: 0x0400026F RID: 623
		XSD_KATMAI_DATE = 127,
		// Token: 0x04000270 RID: 624
		XSD_KATMAI_DATETIME = 126,
		// Token: 0x04000271 RID: 625
		XSD_KATMAI_TIME = 125,
		// Token: 0x04000272 RID: 626
		XSD_KATMAI_DATEOFFSET = 124,
		// Token: 0x04000273 RID: 627
		XSD_KATMAI_DATETIMEOFFSET = 123,
		// Token: 0x04000274 RID: 628
		XSD_KATMAI_TIMEOFFSET = 122,
		// Token: 0x04000275 RID: 629
		XSD_BOOLEAN = 134,
		// Token: 0x04000276 RID: 630
		XSD_TIME = 129,
		// Token: 0x04000277 RID: 631
		XSD_DATETIME,
		// Token: 0x04000278 RID: 632
		XSD_DATE,
		// Token: 0x04000279 RID: 633
		XSD_BINHEX,
		// Token: 0x0400027A RID: 634
		XSD_BASE64,
		// Token: 0x0400027B RID: 635
		XSD_DECIMAL = 135,
		// Token: 0x0400027C RID: 636
		XSD_BYTE,
		// Token: 0x0400027D RID: 637
		XSD_UNSIGNEDSHORT,
		// Token: 0x0400027E RID: 638
		XSD_UNSIGNEDINT,
		// Token: 0x0400027F RID: 639
		XSD_UNSIGNEDLONG,
		// Token: 0x04000280 RID: 640
		XSD_QNAME
	}
}
