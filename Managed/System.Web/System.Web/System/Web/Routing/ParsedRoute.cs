using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace System.Web.Routing
{
	// Token: 0x020004E7 RID: 1255
	internal sealed class ParsedRoute
	{
		// Token: 0x0600386F RID: 14447 RVA: 0x0009791C File Offset: 0x00095B1C
		public ParsedRoute(IList<PathSegment> pathSegments)
		{
			this.PathSegments = pathSegments;
		}

		// Token: 0x1700119D RID: 4509
		// (get) Token: 0x06003870 RID: 14448 RVA: 0x0009792B File Offset: 0x00095B2B
		// (set) Token: 0x06003871 RID: 14449 RVA: 0x00097933 File Offset: 0x00095B33
		private IList<PathSegment> PathSegments { get; set; }

		// Token: 0x06003872 RID: 14450 RVA: 0x0009793C File Offset: 0x00095B3C
		public BoundUrl Bind(RouteValueDictionary currentValues, RouteValueDictionary values, RouteValueDictionary defaultValues, RouteValueDictionary constraints)
		{
			if (currentValues == null)
			{
				currentValues = new RouteValueDictionary();
			}
			if (values == null)
			{
				values = new RouteValueDictionary();
			}
			if (defaultValues == null)
			{
				defaultValues = new RouteValueDictionary();
			}
			RouteValueDictionary acceptedValues = new RouteValueDictionary();
			HashSet<string> unusedNewValues = new HashSet<string>(values.Keys, StringComparer.OrdinalIgnoreCase);
			ParsedRoute.ForEachParameter(this.PathSegments, delegate(ParameterSubsegment parameterSubsegment)
			{
				string parameterName = parameterSubsegment.ParameterName;
				object obj5;
				bool flag5 = values.TryGetValue(parameterName, out obj5);
				if (flag5)
				{
					unusedNewValues.Remove(parameterName);
				}
				object obj6;
				bool flag6 = currentValues.TryGetValue(parameterName, out obj6);
				if (flag5 && flag6 && !ParsedRoute.RoutePartsEqual(obj6, obj5))
				{
					return false;
				}
				if (flag5)
				{
					if (ParsedRoute.IsRoutePartNonEmpty(obj5))
					{
						acceptedValues.Add(parameterName, obj5);
					}
				}
				else if (flag6)
				{
					acceptedValues.Add(parameterName, obj6);
				}
				return true;
			});
			foreach (KeyValuePair<string, object> keyValuePair in values)
			{
				if (ParsedRoute.IsRoutePartNonEmpty(keyValuePair.Value) && !acceptedValues.ContainsKey(keyValuePair.Key))
				{
					acceptedValues.Add(keyValuePair.Key, keyValuePair.Value);
				}
			}
			foreach (KeyValuePair<string, object> keyValuePair2 in currentValues)
			{
				string key = keyValuePair2.Key;
				if (!acceptedValues.ContainsKey(key) && ParsedRoute.GetParameterSubsegment(this.PathSegments, key) == null)
				{
					acceptedValues.Add(key, keyValuePair2.Value);
				}
			}
			ParsedRoute.ForEachParameter(this.PathSegments, delegate(ParameterSubsegment parameterSubsegment)
			{
				object obj7;
				if (!acceptedValues.ContainsKey(parameterSubsegment.ParameterName) && !ParsedRoute.IsParameterRequired(parameterSubsegment, defaultValues, out obj7))
				{
					acceptedValues.Add(parameterSubsegment.ParameterName, obj7);
				}
				return true;
			});
			if (!ParsedRoute.ForEachParameter(this.PathSegments, delegate(ParameterSubsegment parameterSubsegment)
			{
				object obj8;
				return !ParsedRoute.IsParameterRequired(parameterSubsegment, defaultValues, out obj8) || acceptedValues.ContainsKey(parameterSubsegment.ParameterName);
			}))
			{
				return null;
			}
			RouteValueDictionary otherDefaultValues = new RouteValueDictionary(defaultValues);
			ParsedRoute.ForEachParameter(this.PathSegments, delegate(ParameterSubsegment parameterSubsegment)
			{
				otherDefaultValues.Remove(parameterSubsegment.ParameterName);
				return true;
			});
			foreach (KeyValuePair<string, object> keyValuePair3 in otherDefaultValues)
			{
				object obj;
				if (values.TryGetValue(keyValuePair3.Key, out obj))
				{
					unusedNewValues.Remove(keyValuePair3.Key);
					if (!ParsedRoute.RoutePartsEqual(obj, keyValuePair3.Value))
					{
						return null;
					}
				}
			}
			StringBuilder stringBuilder = new StringBuilder();
			StringBuilder stringBuilder2 = new StringBuilder();
			bool flag = false;
			bool flag2 = false;
			for (int i = 0; i < this.PathSegments.Count; i++)
			{
				PathSegment pathSegment = this.PathSegments[i];
				if (pathSegment is SeparatorPathSegment)
				{
					if (flag && stringBuilder2.Length > 0)
					{
						if (flag2)
						{
							return null;
						}
						stringBuilder.Append(stringBuilder2.ToString());
						stringBuilder2.Length = 0;
					}
					flag = false;
					if (stringBuilder2.Length > 0 && stringBuilder2[stringBuilder2.Length - 1] == '/')
					{
						if (flag2)
						{
							return null;
						}
						stringBuilder.Append(stringBuilder2.ToString(0, stringBuilder2.Length - 1));
						stringBuilder2.Length = 0;
						flag2 = true;
					}
					else
					{
						stringBuilder2.Append("/");
					}
				}
				else
				{
					ContentPathSegment contentPathSegment = pathSegment as ContentPathSegment;
					if (contentPathSegment != null)
					{
						bool flag3 = false;
						foreach (PathSubsegment pathSubsegment in contentPathSegment.Subsegments)
						{
							LiteralSubsegment literalSubsegment = pathSubsegment as LiteralSubsegment;
							if (literalSubsegment != null)
							{
								flag = true;
								stringBuilder2.Append(ParsedRoute.UrlEncode(literalSubsegment.Literal));
							}
							else
							{
								ParameterSubsegment parameterSubsegment2 = pathSubsegment as ParameterSubsegment;
								if (parameterSubsegment2 != null)
								{
									if (flag && stringBuilder2.Length > 0)
									{
										if (flag2)
										{
											return null;
										}
										stringBuilder.Append(stringBuilder2.ToString());
										stringBuilder2.Length = 0;
										flag3 = true;
									}
									flag = false;
									object obj2;
									if (acceptedValues.TryGetValue(parameterSubsegment2.ParameterName, out obj2))
									{
										unusedNewValues.Remove(parameterSubsegment2.ParameterName);
									}
									object obj3;
									defaultValues.TryGetValue(parameterSubsegment2.ParameterName, out obj3);
									if (ParsedRoute.RoutePartsEqual(obj2, obj3))
									{
										stringBuilder2.Append(ParsedRoute.UrlEncode(Convert.ToString(obj2, CultureInfo.InvariantCulture)));
									}
									else
									{
										if (flag2)
										{
											return null;
										}
										if (stringBuilder2.Length > 0)
										{
											stringBuilder.Append(stringBuilder2.ToString());
											stringBuilder2.Length = 0;
										}
										stringBuilder.Append(ParsedRoute.UrlEncode(Convert.ToString(obj2, CultureInfo.InvariantCulture)));
										flag3 = true;
									}
								}
							}
						}
						if (flag3 && stringBuilder2.Length > 0)
						{
							if (flag2)
							{
								return null;
							}
							stringBuilder.Append(stringBuilder2.ToString());
							stringBuilder2.Length = 0;
						}
					}
				}
			}
			if (flag && stringBuilder2.Length > 0)
			{
				if (flag2)
				{
					return null;
				}
				stringBuilder.Append(stringBuilder2.ToString());
			}
			if (constraints != null)
			{
				foreach (KeyValuePair<string, object> keyValuePair4 in constraints)
				{
					unusedNewValues.Remove(keyValuePair4.Key);
				}
			}
			if (unusedNewValues.Count > 0)
			{
				bool flag4 = true;
				foreach (string text in unusedNewValues)
				{
					object obj4;
					if (acceptedValues.TryGetValue(text, out obj4))
					{
						stringBuilder.Append(flag4 ? '?' : '&');
						flag4 = false;
						stringBuilder.Append(Uri.EscapeDataString(text));
						stringBuilder.Append('=');
						stringBuilder.Append(Uri.EscapeDataString(Convert.ToString(obj4, CultureInfo.InvariantCulture)));
					}
				}
			}
			return new BoundUrl
			{
				Url = stringBuilder.ToString(),
				Values = acceptedValues
			};
		}

		// Token: 0x06003873 RID: 14451 RVA: 0x00097F78 File Offset: 0x00096178
		private static string EscapeReservedCharacters(Match m)
		{
			return "%" + Convert.ToUInt16(m.Value[0]).ToString("x2", CultureInfo.InvariantCulture);
		}

		// Token: 0x06003874 RID: 14452 RVA: 0x00097FB4 File Offset: 0x000961B4
		private static bool ForEachParameter(IList<PathSegment> pathSegments, Func<ParameterSubsegment, bool> action)
		{
			for (int i = 0; i < pathSegments.Count; i++)
			{
				PathSegment pathSegment = pathSegments[i];
				if (!(pathSegment is SeparatorPathSegment))
				{
					ContentPathSegment contentPathSegment = pathSegment as ContentPathSegment;
					if (contentPathSegment != null)
					{
						foreach (PathSubsegment pathSubsegment in contentPathSegment.Subsegments)
						{
							if (!(pathSubsegment is LiteralSubsegment))
							{
								ParameterSubsegment parameterSubsegment = pathSubsegment as ParameterSubsegment;
								if (parameterSubsegment != null && !action(parameterSubsegment))
								{
									return false;
								}
							}
						}
					}
				}
			}
			return true;
		}

		// Token: 0x06003875 RID: 14453 RVA: 0x00098050 File Offset: 0x00096250
		private static ParameterSubsegment GetParameterSubsegment(IList<PathSegment> pathSegments, string parameterName)
		{
			ParameterSubsegment foundParameterSubsegment = null;
			ParsedRoute.ForEachParameter(pathSegments, delegate(ParameterSubsegment parameterSubsegment)
			{
				if (string.Equals(parameterName, parameterSubsegment.ParameterName, StringComparison.OrdinalIgnoreCase))
				{
					foundParameterSubsegment = parameterSubsegment;
					return false;
				}
				return true;
			});
			return foundParameterSubsegment;
		}

		// Token: 0x06003876 RID: 14454 RVA: 0x0009808A File Offset: 0x0009628A
		private static bool IsParameterRequired(ParameterSubsegment parameterSubsegment, RouteValueDictionary defaultValues, out object defaultValue)
		{
			if (parameterSubsegment.IsCatchAll)
			{
				defaultValue = null;
				return false;
			}
			return !defaultValues.TryGetValue(parameterSubsegment.ParameterName, out defaultValue);
		}

		// Token: 0x06003877 RID: 14455 RVA: 0x000980AC File Offset: 0x000962AC
		private static bool IsRoutePartNonEmpty(object routePart)
		{
			string text = routePart as string;
			if (text != null)
			{
				return text.Length > 0;
			}
			return routePart != null;
		}

		// Token: 0x06003878 RID: 14456 RVA: 0x000980D4 File Offset: 0x000962D4
		public RouteValueDictionary Match(string virtualPath, RouteValueDictionary defaultValues)
		{
			IList<string> list = RouteParser.SplitUrlToPathSegmentStrings(virtualPath);
			if (defaultValues == null)
			{
				defaultValues = new RouteValueDictionary();
			}
			RouteValueDictionary routeValueDictionary = new RouteValueDictionary();
			bool flag = false;
			bool flag2 = false;
			for (int i = 0; i < this.PathSegments.Count; i++)
			{
				PathSegment pathSegment = this.PathSegments[i];
				if (list.Count <= i)
				{
					flag = true;
				}
				string text = (flag ? null : list[i]);
				if (pathSegment is SeparatorPathSegment)
				{
					if (!flag && !string.Equals(text, "/", StringComparison.Ordinal))
					{
						return null;
					}
				}
				else
				{
					ContentPathSegment contentPathSegment = pathSegment as ContentPathSegment;
					if (contentPathSegment != null)
					{
						if (contentPathSegment.IsCatchAll)
						{
							this.MatchCatchAll(contentPathSegment, list.Skip(i), defaultValues, routeValueDictionary);
							flag2 = true;
						}
						else if (!this.MatchContentPathSegment(contentPathSegment, text, defaultValues, routeValueDictionary))
						{
							return null;
						}
					}
				}
			}
			if (!flag2 && this.PathSegments.Count < list.Count)
			{
				for (int j = this.PathSegments.Count; j < list.Count; j++)
				{
					if (!RouteParser.IsSeparator(list[j]))
					{
						return null;
					}
				}
			}
			if (defaultValues != null)
			{
				foreach (KeyValuePair<string, object> keyValuePair in defaultValues)
				{
					if (!routeValueDictionary.ContainsKey(keyValuePair.Key))
					{
						routeValueDictionary.Add(keyValuePair.Key, keyValuePair.Value);
					}
				}
			}
			return routeValueDictionary;
		}

		// Token: 0x06003879 RID: 14457 RVA: 0x0009824C File Offset: 0x0009644C
		private void MatchCatchAll(ContentPathSegment contentPathSegment, IEnumerable<string> remainingRequestSegments, RouteValueDictionary defaultValues, RouteValueDictionary matchedValues)
		{
			string text = string.Join(string.Empty, remainingRequestSegments.ToArray<string>());
			ParameterSubsegment parameterSubsegment = contentPathSegment.Subsegments[0] as ParameterSubsegment;
			object obj;
			if (text.Length > 0)
			{
				obj = text;
			}
			else
			{
				defaultValues.TryGetValue(parameterSubsegment.ParameterName, out obj);
			}
			matchedValues.Add(parameterSubsegment.ParameterName, obj);
		}

		// Token: 0x0600387A RID: 14458 RVA: 0x000982A8 File Offset: 0x000964A8
		private bool MatchContentPathSegment(ContentPathSegment routeSegment, string requestPathSegment, RouteValueDictionary defaultValues, RouteValueDictionary matchedValues)
		{
			if (!string.IsNullOrEmpty(requestPathSegment))
			{
				int num = requestPathSegment.Length;
				int i = routeSegment.Subsegments.Count - 1;
				ParameterSubsegment parameterSubsegment = null;
				LiteralSubsegment literalSubsegment = null;
				while (i >= 0)
				{
					int num2 = num;
					ParameterSubsegment parameterSubsegment2 = routeSegment.Subsegments[i] as ParameterSubsegment;
					if (parameterSubsegment2 != null)
					{
						parameterSubsegment = parameterSubsegment2;
					}
					else
					{
						LiteralSubsegment literalSubsegment2 = routeSegment.Subsegments[i] as LiteralSubsegment;
						if (literalSubsegment2 != null)
						{
							literalSubsegment = literalSubsegment2;
							int num3 = num - 1;
							if (parameterSubsegment != null)
							{
								num3--;
							}
							if (num3 < 0)
							{
								return false;
							}
							int num4 = requestPathSegment.LastIndexOf(literalSubsegment2.Literal, num3, StringComparison.OrdinalIgnoreCase);
							if (num4 == -1)
							{
								return false;
							}
							if (i == routeSegment.Subsegments.Count - 1 && num4 + literalSubsegment2.Literal.Length != requestPathSegment.Length)
							{
								return false;
							}
							num2 = num4;
						}
					}
					if (parameterSubsegment != null && ((literalSubsegment != null && parameterSubsegment2 == null) || i == 0))
					{
						int num5;
						int num6;
						if (literalSubsegment == null)
						{
							if (i == 0)
							{
								num5 = 0;
							}
							else
							{
								num5 = num2;
							}
							num6 = num;
						}
						else if (i == 0 && parameterSubsegment2 != null)
						{
							num5 = 0;
							num6 = num;
						}
						else
						{
							num5 = num2 + literalSubsegment.Literal.Length;
							num6 = num - num5;
						}
						string text = requestPathSegment.Substring(num5, num6);
						if (string.IsNullOrEmpty(text))
						{
							return false;
						}
						matchedValues.Add(parameterSubsegment.ParameterName, text);
						parameterSubsegment = null;
						literalSubsegment = null;
					}
					num = num2;
					i--;
				}
				return num == 0 || routeSegment.Subsegments[0] is ParameterSubsegment;
			}
			if (routeSegment.Subsegments.Count > 1)
			{
				return false;
			}
			ParameterSubsegment parameterSubsegment3 = routeSegment.Subsegments[0] as ParameterSubsegment;
			if (parameterSubsegment3 == null)
			{
				return false;
			}
			object obj;
			if (defaultValues.TryGetValue(parameterSubsegment3.ParameterName, out obj))
			{
				matchedValues.Add(parameterSubsegment3.ParameterName, obj);
				return true;
			}
			return false;
		}

		// Token: 0x0600387B RID: 14459 RVA: 0x00098454 File Offset: 0x00096654
		private static bool RoutePartsEqual(object a, object b)
		{
			string text = a as string;
			string text2 = b as string;
			if (text != null && text2 != null)
			{
				return string.Equals(text, text2, StringComparison.OrdinalIgnoreCase);
			}
			if (a != null && b != null)
			{
				return a.Equals(b);
			}
			return a == b;
		}

		// Token: 0x0600387C RID: 14460 RVA: 0x00098490 File Offset: 0x00096690
		private static string UrlEncode(string str)
		{
			return Regex.Replace(Uri.EscapeUriString(str), "([#;?:@&=+$,])", new MatchEvaluator(ParsedRoute.EscapeReservedCharacters));
		}
	}
}
