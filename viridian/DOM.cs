using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace viridian {
	public class Document {
		public static TextNode NewText(string text) {
			return new TextNode(
				text
			);
		}

		public static ElementNode NewElement(string name, List<Attr> attributes = null, List<Node> children = null) {
			return new ElementNode(
				name,
				attributes ?? new List<Attr>(),
				children ?? new List<Node>()
			);
		}
	}

	// Nodes
	public class Node {
		// placeholder class to allow children lists to be of type textnode and elementnode
	}

	public class TextNode : Node {
		public string Text;

		public TextNode(string text) {
			Text = text;
		}
	}

	public class ElementNode : Node {
		public string TagName;
		public List<Attr> Attributes;
		public List<Node> Children;

		public ElementNode(string name, List<Attr> attributes, List<Node> children) {
			TagName = name;
			Attributes = attributes;
			Children = children;
		}
	}

	// Attributes
	public class Attr {
		public string key;
		public string value;

		public Attr(string k, string v) {
			key = k;
			value = v;
		}
	}
}
