using System;
using System.IO;

namespace Mono.WebBrowser.DOM
{
	// Token: 0x02000029 RID: 41
	public interface IElement : INode
	{
		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000EA RID: 234
		IElementCollection All { get; }

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000EB RID: 235
		IElementCollection Children { get; }

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000EC RID: 236
		int ClientWidth { get; }

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000ED RID: 237
		int ClientHeight { get; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000EE RID: 238
		int ScrollHeight { get; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000EF RID: 239
		int ScrollWidth { get; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000F0 RID: 240
		// (set) Token: 0x060000F1 RID: 241
		int ScrollLeft { get; set; }

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000F2 RID: 242
		// (set) Token: 0x060000F3 RID: 243
		int ScrollTop { get; set; }

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060000F4 RID: 244
		int OffsetHeight { get; }

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060000F5 RID: 245
		int OffsetWidth { get; }

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060000F6 RID: 246
		int OffsetLeft { get; }

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060000F7 RID: 247
		int OffsetTop { get; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060000F8 RID: 248
		IElement OffsetParent { get; }

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060000F9 RID: 249
		// (set) Token: 0x060000FA RID: 250
		string InnerText { get; set; }

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060000FB RID: 251
		// (set) Token: 0x060000FC RID: 252
		string InnerHTML { get; set; }

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060000FD RID: 253
		// (set) Token: 0x060000FE RID: 254
		string OuterText { get; set; }

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060000FF RID: 255
		// (set) Token: 0x06000100 RID: 256
		string OuterHTML { get; set; }

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000101 RID: 257
		// (set) Token: 0x06000102 RID: 258
		string Style { get; set; }

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000103 RID: 259
		// (set) Token: 0x06000104 RID: 260
		int TabIndex { get; set; }

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000105 RID: 261
		string TagName { get; }

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000106 RID: 262
		// (set) Token: 0x06000107 RID: 263
		bool Disabled { get; set; }

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000108 RID: 264
		Stream ContentStream { get; }

		// Token: 0x06000109 RID: 265
		IElement AppendChild(IElement child);

		// Token: 0x0600010A RID: 266
		void Blur();

		// Token: 0x0600010B RID: 267
		void Focus();

		// Token: 0x0600010C RID: 268
		bool HasAttribute(string name);

		// Token: 0x0600010D RID: 269
		string GetAttribute(string name);

		// Token: 0x0600010E RID: 270
		IElementCollection GetElementsByTagName(string id);

		// Token: 0x0600010F RID: 271
		int GetHashCode();

		// Token: 0x06000110 RID: 272
		void ScrollIntoView(bool alignWithTop);

		// Token: 0x06000111 RID: 273
		void SetAttribute(string name, string value);
	}
}
