using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace viridian {
	public partial class MainWindow : Window {
		public UtilCanvas c;
		public Document d;

		int top = 0;
		int left = 0;

		public MainWindow() {
			InitializeComponent();
			c = new UtilCanvas(DisplayCanvas);

			// generate example document
			string html = @"<html>
<head>
	<title>my webpage</title>
</head>
<body>
	<h1>welcome</h1>
	<p>welcome to my website</p>
	<div id=""content"" class=""container wrapper"">
		<h2>a title in a div</h2>
		<p>a paragraph in a div</p>
	</div>
</body>
</html>";

			d = new Document(html);
			/*
			d.head.AddChild(Document.NewNode("title", null, new List<Node>() { Document.NewTextNode("my webpage") }));
			d.body.AddChild(Document.NewNode("h1", null, new List<Node>() { Document.NewTextNode("welcome") }));
			d.body.AddChild(Document.NewNode("p", null, new List<Node>() { Document.NewTextNode("welcome to my website") }));
			d.body.AddChild(Document.NewNode("div", new List<Attr>() { new Attr("id", "content"), new Attr("class", "container wrapper") }, new List<Node>() { Document.NewNode("h2", null, new List<Node>() { Document.NewTextNode("a title in a div") }), Document.NewNode("p", null, new List<Node>() { Document.NewTextNode("a paragraph in a div") }) }));
			*/

			DrawElementTree(d.root);
		}

		public void DrawElementTree(ElementNode node) {
			foreach (Node child in node.Children) {
				if (child.GetType() == typeof(ElementNode)) {
					string attributes = "";

					foreach (Attr attribute in ((ElementNode)child).Attributes) {
						attributes += ' ' + attribute.key + '=' + '"' + attribute.value + '"';
					}

					c.DrawText("<" + ((ElementNode)child).TagName + attributes + ">", left * 12, top * 12, 12);
					top++;
					left++;
					DrawElementTree((ElementNode)child);
					left--;
					c.DrawText("</" + ((ElementNode)child).TagName + ">", left * 12, top * 12, 12);
					top++;
				} else if (child.GetType() == typeof(TextNode)) {
					left++;
					c.DrawText('"' + ((TextNode)child).Text + '"', left * 12, top * 12, 12);
					left--;
					top++;
				}
			}
		}
	}

	public class UtilCanvas {
		Canvas target;
	
		public UtilCanvas(Canvas canvas) {
			target = canvas;
		}

		public void DrawText(string text, double x = 0, double y = 0, double size = 16, Color color = default(Color)) {
			color = color == default(Color) ? Color.FromRgb(0, 0, 0) : color;

			TextBlock textBlock = new TextBlock();

			// set content
			textBlock.Text = text;
			// set appearance
			textBlock.FontSize = size;
			textBlock.Foreground = new SolidColorBrush(color);
			// set position
			Canvas.SetLeft(textBlock, x);
			Canvas.SetTop(textBlock, y);

			target.Children.Add(textBlock);
		}
	}
}
