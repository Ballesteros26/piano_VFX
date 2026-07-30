using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace System.Web.Routing
{
	// Token: 0x020004F5 RID: 1269
	internal static class RouteParser
	{
		// Token: 0x060038D2 RID: 14546 RVA: 0x000991CC File Offset: 0x000973CC
		private static string GetLiteral(string segmentLiteral)
		{
			string text = segmentLiteral.Replace("{{", "").Replace("}}", "");
			if (text.Contains("{") || text.Contains("}"))
			{
				return null;
			}
			return segmentLiteral.Replace("{{", "{").Replace("}}", "}");
		}

		// Token: 0x060038D3 RID: 14547 RVA: 0x00099234 File Offset: 0x00097434
		private static int IndexOfFirstOpenParameter(string segment, int startIndex)
		{
			for (;;)
			{
				startIndex = segment.IndexOf('{', startIndex);
				if (startIndex == -1)
				{
					break;
				}
				if (startIndex + 1 == segment.Length || (startIndex + 1 < segment.Length && segment[startIndex + 1] != '{'))
				{
					return startIndex;
				}
				startIndex += 2;
			}
			return -1;
		}

		// Token: 0x060038D4 RID: 14548 RVA: 0x00099272 File Offset: 0x00097472
		internal static bool IsSeparator(string s)
		{
			return string.Equals(s, "/", StringComparison.Ordinal);
		}

		// Token: 0x060038D5 RID: 14549 RVA: 0x00099280 File Offset: 0x00097480
		private static bool IsValidParameterName(string parameterName)
		{
			if (parameterName.Length == 0)
			{
				return false;
			}
			foreach (char c in parameterName)
			{
				if (c == '/' || c == '{' || c == '}')
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060038D6 RID: 14550 RVA: 0x000992C2 File Offset: 0x000974C2
		internal static bool IsInvalidRouteUrl(string routeUrl)
		{
			return routeUrl.StartsWith("~", StringComparison.Ordinal) || routeUrl.StartsWith("/", StringComparison.Ordinal) || routeUrl.IndexOf('?') != -1;
		}

		// Token: 0x060038D7 RID: 14551 RVA: 0x000992F0 File Offset: 0x000974F0
		public static ParsedRoute Parse(string routeUrl)
		{
			if (routeUrl == null)
			{
				routeUrl = string.Empty;
			}
			if (RouteParser.IsInvalidRouteUrl(routeUrl))
			{
				throw new ArgumentException(global::SR.GetString("The route URL cannot start with a '/' or '~' character and it cannot contain a '?' character."), "routeUrl");
			}
			IList<string> list = RouteParser.SplitUrlToPathSegmentStrings(routeUrl);
			Exception ex = RouteParser.ValidateUrlParts(list);
			if (ex != null)
			{
				throw ex;
			}
			return new ParsedRoute(RouteParser.SplitUrlToPathSegments(list));
		}

		// Token: 0x060038D8 RID: 14552 RVA: 0x00099340 File Offset: 0x00097540
		private static IList<PathSubsegment> ParseUrlSegment(string segment, out Exception exception)
		{
			int i = 0;
			List<PathSubsegment> list = new List<PathSubsegment>();
			while (i < segment.Length)
			{
				int num = RouteParser.IndexOfFirstOpenParameter(segment, i);
				if (num == -1)
				{
					string literal = RouteParser.GetLiteral(segment.Substring(i));
					if (literal == null)
					{
						exception = new ArgumentException(string.Format(CultureInfo.CurrentUICulture, global::SR.GetString("There is an incomplete parameter in this path segment: '{0}'. Check that each '{{' character has a matching '}}' character."), segment), "routeUrl");
						return null;
					}
					if (literal.Length > 0)
					{
						list.Add(new LiteralSubsegment(literal));
						break;
					}
					break;
				}
				else
				{
					int num2 = segment.IndexOf('}', num + 1);
					if (num2 == -1)
					{
						exception = new ArgumentException(string.Format(CultureInfo.CurrentUICulture, global::SR.GetString("There is an incomplete parameter in this path segment: '{0}'. Check that each '{{' character has a matching '}}' character."), segment), "routeUrl");
						return null;
					}
					string literal2 = RouteParser.GetLiteral(segment.Substring(i, num - i));
					if (literal2 == null)
					{
						exception = new ArgumentException(string.Format(CultureInfo.CurrentUICulture, global::SR.GetString("There is an incomplete parameter in this path segment: '{0}'. Check that each '{{' character has a matching '}}' character."), segment), "routeUrl");
						return null;
					}
					if (literal2.Length > 0)
					{
						list.Add(new LiteralSubsegment(literal2));
					}
					string text = segment.Substring(num + 1, num2 - num - 1);
					list.Add(new ParameterSubsegment(text));
					i = num2 + 1;
				}
			}
			exception = null;
			return list;
		}

		// Token: 0x060038D9 RID: 14553 RVA: 0x00099470 File Offset: 0x00097670
		private static IList<PathSegment> SplitUrlToPathSegments(IList<string> urlParts)
		{
			List<PathSegment> list = new List<PathSegment>();
			foreach (string text in urlParts)
			{
				if (RouteParser.IsSeparator(text))
				{
					list.Add(new SeparatorPathSegment());
				}
				else
				{
					Exception ex;
					IList<PathSubsegment> list2 = RouteParser.ParseUrlSegment(text, out ex);
					list.Add(new ContentPathSegment(list2));
				}
			}
			return list;
		}

		// Token: 0x060038DA RID: 14554 RVA: 0x000994E4 File Offset: 0x000976E4
		internal static IList<string> SplitUrlToPathSegmentStrings(string url)
		{
			List<string> list = new List<string>();
			if (string.IsNullOrEmpty(url))
			{
				return list;
			}
			int i = 0;
			while (i < url.Length)
			{
				int num = url.IndexOf('/', i);
				if (num == -1)
				{
					string text = url.Substring(i);
					if (text.Length > 0)
					{
						list.Add(text);
						break;
					}
					break;
				}
				else
				{
					string text2 = url.Substring(i, num - i);
					if (text2.Length > 0)
					{
						list.Add(text2);
					}
					list.Add("/");
					i = num + 1;
				}
			}
			return list;
		}

		// Token: 0x060038DB RID: 14555 RVA: 0x00099564 File Offset: 0x00097764
		private static Exception ValidateUrlParts(IList<string> pathSegments)
		{
			HashSet<string> hashSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
			bool? flag = null;
			bool flag2 = false;
			foreach (string text in pathSegments)
			{
				if (flag2)
				{
					return new ArgumentException(string.Format(CultureInfo.CurrentCulture, global::SR.GetString("A catch-all parameter can only appear as the last segment of the route URL."), Array.Empty<object>()), "routeUrl");
				}
				bool flag3;
				if (flag == null)
				{
					flag = new bool?(RouteParser.IsSeparator(text));
					flag3 = flag.Value;
				}
				else
				{
					flag3 = RouteParser.IsSeparator(text);
					if (flag3 && flag.Value)
					{
						return new ArgumentException(global::SR.GetString("The route URL separator character '/' cannot appear consecutively. It must be separated by either a parameter or a literal value."), "routeUrl");
					}
					flag = new bool?(flag3);
				}
				if (!flag3)
				{
					Exception ex;
					IList<PathSubsegment> list = RouteParser.ParseUrlSegment(text, out ex);
					if (ex != null)
					{
						return ex;
					}
					ex = RouteParser.ValidateUrlSegment(list, hashSet, text);
					if (ex != null)
					{
						return ex;
					}
					flag2 = list.Any((PathSubsegment seg) => seg is ParameterSubsegment && ((ParameterSubsegment)seg).IsCatchAll);
				}
			}
			return null;
		}

		// Token: 0x060038DC RID: 14556 RVA: 0x000996A0 File Offset: 0x000978A0
		private static Exception ValidateUrlSegment(IList<PathSubsegment> pathSubsegments, HashSet<string> usedParameterNames, string pathSegment)
		{
			bool flag = false;
			Type type = null;
			foreach (PathSubsegment pathSubsegment in pathSubsegments)
			{
				if (type != null && type == pathSubsegment.GetType())
				{
					return new ArgumentException(string.Format(CultureInfo.CurrentCulture, global::SR.GetString("A path segment cannot contain two consecutive parameters. They must be separated by a '/' or by a literal string."), Array.Empty<object>()), "routeUrl");
				}
				type = pathSubsegment.GetType();
				if (!(pathSubsegment is LiteralSubsegment))
				{
					ParameterSubsegment parameterSubsegment = pathSubsegment as ParameterSubsegment;
					if (parameterSubsegment != null)
					{
						string parameterName = parameterSubsegment.ParameterName;
						if (parameterSubsegment.IsCatchAll)
						{
							flag = true;
						}
						if (!RouteParser.IsValidParameterName(parameterName))
						{
							return new ArgumentException(string.Format(CultureInfo.CurrentUICulture, global::SR.GetString("The route parameter name '{0}' is invalid. Route parameter names must be non-empty and cannot contain these characters: \"{{\", \"}}\", \"/\", \"?\""), parameterName), "routeUrl");
						}
						if (usedParameterNames.Contains(parameterName))
						{
							return new ArgumentException(string.Format(CultureInfo.CurrentUICulture, global::SR.GetString("The route parameter name '{0}' appears more than one time in the URL."), parameterName), "routeUrl");
						}
						usedParameterNames.Add(parameterName);
					}
				}
			}
			if (flag && pathSubsegments.Count != 1)
			{
				return new ArgumentException(string.Format(CultureInfo.CurrentCulture, global::SR.GetString("A path segment that contains more than one section, such as a literal section or a parameter, cannot contain a catch-all parameter."), Array.Empty<object>()), "routeUrl");
			}
			return null;
		}
	}
}
