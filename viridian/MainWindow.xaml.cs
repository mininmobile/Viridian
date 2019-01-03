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
		public CSS css;

		int top = 0;
		int scroll = 0;

		public MainWindow() {
			InitializeComponent();
			c = new UtilCanvas(DisplayCanvas);
			d = new Document();
			css = new CSS();

			// generate example document
			{
				{ // head
					ElementNode title = Document.NewNode("title");
					title.AddChild(Document.NewTextNode("my webpage"));
					d.head.AddChild(title);
				}

				{ //body
					ElementNode title1 = Document.NewNode("h1");
					title1.AddChild(Document.NewTextNode("welcome"));
					d.body.AddChild(title1);

					ElementNode paragraph1 = Document.NewNode("p");
					paragraph1.AddChild(Document.NewTextNode("welcome"));
					d.body.AddChild(paragraph1);
				}
			}

			/*
			d.body.AddChild(Document.NewNode("h1", new List<Attr>() { new Attr("id", "title") }, new List<Node>() { Document.NewTextNode("welcome") }));
			d.body.AddChild(Document.NewNode("p", new List<Attr>() { new Attr("class", "text") }, new List<Node>() { Document.NewTextNode("welcome to my website") }));
			d.body.AddChild(Document.NewNode("div", new List<Attr>() { new Attr("id", "content"), new Attr("class", "container wrapper") }, new List<Node>() { Document.NewNode("h2", null, new List<Node>() { Document.NewTextNode("a title in a div") }), Document.NewNode("p", new List<Attr>() { new Attr("class", "text") }, new List<Node>() { Document.NewTextNode("a paragraph in a div") }) }));*/

			DrawElementTree(d.root);
		}

		public void DrawElementTree(ElementNode node) {
			foreach (Node child in node.Children) {
				if (child.GetType() == typeof(ElementNode)) {
					DrawElementTree((ElementNode)child);
				} else if (child.GetType() == typeof(TextNode)) {
					List<Property> properties = css.GetStyleForNode(child.Parent);

					c.DrawText(
						((TextNode)child).Text,
						0,
						top,
						((CSSPx)css.GetPropertyFromList("font-size", properties).Value).Pixels,
						((CSSRgb)css.GetPropertyFromList("color", properties).Value).ToColor()
					);

					top += ((CSSPx)css.GetPropertyFromList("font-size", properties).Value).Pixels;
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
