using System;

namespace Mono.WebBrowser.DOM
{
	// Token: 0x02000027 RID: 39
	public interface IDocument : INode
	{
		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000BA RID: 186
		IElement Active { get; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000BB RID: 187
		// (set) Token: 0x060000BC RID: 188
		string ActiveLinkColor { get; set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000BD RID: 189
		IElementCollection Anchors { get; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000BE RID: 190
		IElementCollection Applets { get; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000BF RID: 191
		// (set) Token: 0x060000C0 RID: 192
		string Background { get; set; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000C1 RID: 193
		// (set) Token: 0x060000C2 RID: 194
		string BackColor { get; set; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000C3 RID: 195
		IElement Body { get; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000C4 RID: 196
		// (set) Token: 0x060000C5 RID: 197
		string Charset { get; set; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000C6 RID: 198
		// (set) Token: 0x060000C7 RID: 199
		string Cookie { get; set; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000C8 RID: 200
		IElement DocumentElement { get; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000C9 RID: 201
		IDocumentType DocType { get; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000CA RID: 202
		string Domain { get; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000CB RID: 203
		// (set) Token: 0x060000CC RID: 204
		string ForeColor { get; set; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000CD RID: 205
		IElementCollection Forms { get; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000CE RID: 206
		IElementCollection Images { get; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000CF RID: 207
		IDOMImplementation Implementation { get; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000D0 RID: 208
		// (set) Token: 0x060000D1 RID: 209
		string LinkColor { get; set; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000D2 RID: 210
		IElementCollection Links { get; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000D3 RID: 211
		IStylesheetList Stylesheets { get; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000D4 RID: 212
		// (set) Token: 0x060000D5 RID: 213
		string Title { get; set; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000D6 RID: 214
		string Url { get; }

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000D7 RID: 215
		// (set) Token: 0x060000D8 RID: 216
		string VisitedLinkColor { get; set; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000D9 RID: 217
		IWindow Window { get; }

		// Token: 0x060000DA RID: 218
		IAttribute CreateAttribute(string name);

		// Token: 0x060000DB RID: 219
		IElement CreateElement(string tagName);

		// Token: 0x060000DC RID: 220
		IElement GetElementById(string id);

		// Token: 0x060000DD RID: 221
		IElement GetElement(int x, int y);

		// Token: 0x060000DE RID: 222
		IElementCollection GetElementsByTagName(string id);

		// Token: 0x060000DF RID: 223
		void Write(string text);

		// Token: 0x060000E0 RID: 224
		string InvokeScript(string script);

		// Token: 0x060000E1 RID: 225
		int GetHashCode();

		// Token: 0x14000016 RID: 22
		// (add) Token: 0x060000E2 RID: 226
		// (remove) Token: 0x060000E3 RID: 227
		event EventHandler LoadStopped;
	}
}
