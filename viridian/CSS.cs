using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace viridian {
	public class CSS {
		public List<Rule> Rules;

		public CSS() {
			Rules = new List<Rule>();

			// create base rules
			Rules.Add(NewBasicRule("title", new Dictionary<string, CSSValue>() {
				{ "display", new CSSKeyword(Keyword.none) },
			}));

			Rules.Add(NewBasicRule("h1", new Dictionary<string, CSSValue>() {
				{ "color", new CSSRgb(0, 0, 0) },
				{ "font-size", new CSSEm(2) }
			}));
		}

		public Dictionary<string, CSSValue> GetStyleForNode(ElementNode node) {
			Dictionary<string, CSSValue> properties = new Dictionary<string, CSSValue> {
				{ "color", new CSSRgb(0, 0, 0) },
				{ "display", new CSSKeyword(Keyword.block) },
				{ "font-size", new CSSEm(1) }
			};

			foreach (Rule rule in Rules) {
				bool applies = false;

				foreach (Selector selector in rule.Selectors) {
					if (selector.TagName == node.TagName) applies = true;
					// ignore ids for now
					// ignore classes for now
				}

				if (applies) {
					foreach (KeyValuePair<string, CSSValue> property in rule.Properties) {
						if (property.Key == "color") properties["color"] = property.Value;
						if (property.Key == "display") properties["display"] = property.Value;
						if (property.Key == "font-size") properties["font-size"] = property.Value;
					}
				}
			}

			return properties;
		}

		public static Rule NewBasicRule(string selector, Dictionary<string, CSSValue> properties) {
			return new Rule(new List<Selector>() { new Selector(selector, null, null) }, properties);
		}
	}

	// declare css shit
	public class Rule {
		public List<Selector> Selectors;
		public Dictionary<string, CSSValue> Properties;

		public Rule(List<Selector> selectors, Dictionary<string, CSSValue> properties) {
			Selectors = selectors;
			Properties = properties;
		}
	}

	public class Selector {
		public string TagName;
		public string Id;
		public string Class;

		public Selector(string name, string id, string htmlClass) {
			TagName = name;
			Id = id;
			Class = htmlClass;
		}
	}

	// values
	public class CSSValue {
		// now I can refer to all values as one thing
	}

	public enum Keyword {
		none,
		block,
		inline
	}

	public class CSSKeyword : CSSValue {
		public Keyword Keyword;

		public CSSKeyword(Keyword k) {
			Keyword = k;
		}
	}

	// CSS VALUES: COLOR
	// rgb only is good enough for now
	public class CSSRgb : CSSValue {
		public int R;
		public int G;
		public int B;

		public CSSRgb(int r, int g, int b) {
			R = r;
			B = b;
			G = g;
		}

		public Color ToColor() {
			return Color.FromRgb((byte)R, (byte)G, (byte)B);
		}
	}


	// CSS VALUES: LENGTH
	public class CSSLength : CSSValue {
		public int Pixels;
		// allow all size/length values to work together
	}

	public class CSSPx : CSSLength {
		public CSSPx(int pixels) {
			Pixels = pixels;
		}
	}

	public class CSSEm : CSSLength {
		public int Em;

		public CSSEm(int em) {
			Em = em;

			// 16 should change due to em calculations blah blah I don't have them yet
			Pixels = Em * 16;
		}
	}
}
