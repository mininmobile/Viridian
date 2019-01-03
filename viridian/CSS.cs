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
			Rules.Add(NewBasicRule("h1",
				new List<Property>() {
					new Property("color", new CSSRgb(255, 0, 0)),
					new Property("font-size", new CSSPx(16))
				}
			));
		}

		public List<Property> GetStyleForNode(ElementNode node) {
			Property color = new Property("color", new CSSRgb(0, 0, 0));
			Property fontSize = new Property("font-size", new CSSPx(12));

			foreach (Rule rule in Rules) {
				bool applies = false;

				foreach (Selector selector in rule.Selectors) {
					if (selector.TagName == node.TagName) applies = true;
					// ignore ids for now
					// ignore classes for now
				}

				if (applies) {
					foreach (Property property in rule.Properties) {
						if (property.Name == "color") color.Value = property.Value;
						if (property.Name == "font-size") fontSize.Value = property.Value;
					}
				}
			}

			return new List<Property>() { color, fontSize };
		}

		public Property GetPropertyFromList(string name, List<Property> list) {
			Property property = null;

			foreach (Property p in list) {
				if (p.Name == name) {
					property = p;
					break;
				}
			}

			return property;
		}

		public static Rule NewBasicRule(string selector, List<Property> properties) {
			return new Rule(new List<Selector>() { new Selector(selector, null, null) }, properties);
		}
	}

	// declate css shit
	public class Rule {
		public List<Selector> Selectors;
		public List<Property> Properties;

		public Rule(List<Selector> selectors, List<Property> properties) {
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

	public class Property {
		public string Name;
		public CSSValue Value;

		public Property(string name, CSSValue value) {
			Name = name;
			Value = value;
		}
	}

	// values
	public class CSSValue {
		// now I can refer to all values as one thing
	}

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

	public class CSSPx : CSSValue {
		public int Pixels;

		public CSSPx(int pixels) {
			Pixels = pixels;
		}
	}
}
