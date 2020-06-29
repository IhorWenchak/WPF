﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Task.Models;

namespace Task
{
	public class Packer
	{

		private double _containerHeight = 0;
		private double _containerWidth = 0;
		private List<Surface> _boxes;
		private List<Surface> _boxesMain;
		private Node rootNode;
		private int _counter = 0;
		string output = String.Empty;

		public Packer(List<Surface> boxes, double containerHeight, double containerWidth)
		{

			_containerHeight = containerHeight;
			_containerWidth = containerWidth;
			_boxes = boxes;
			_counter = _boxes.Count();
			_boxesMain = new List<Surface>();

			_boxes.ForEach(x => x.Volume = (x.Height * x.Width));
			_boxes = _boxes.OrderByDescending(x => x.Volume).ToList();

			rootNode = new Node { Height = _containerHeight, Width = _containerWidth };


			Pack();

			Filter();

			Display();
		}



		private void Display()
		{
			if (_boxesMain.Count > 0)
			{
				ResBoxes.Clear();
				foreach (var box in _boxesMain)
				{
					ResBoxes.Add(new Node { Height = box.Height, Width = box.Width, Pos_x = box.Position.Pos_x, Pos_y = box.Position.Pos_y });				
				}
			}
		}


		private void Pack()
		{
			foreach (var box in _boxes)
			{
				var node = FindNode(rootNode, box.Width, box.Height);
				if (node != null)
				{
					box.Position = SplitNode(node, box.Width, box.Height);

				}
			}
		}

		private Node FindNode(Node rootNode, double boxWidth, double boxheight)
		{
			if (rootNode.IsOccupied)
			{
				var nextNode = FindNode(rootNode.BottomNode, boxWidth, boxheight);

				if (nextNode == null)
				{
					nextNode = FindNode(rootNode.RightNode, boxWidth, boxheight);
				}

				return nextNode;
			}
			else if (boxWidth <= rootNode.Width && boxheight <= rootNode.Height)
			{
				return rootNode;
			}
			else
			{
				return null;
			}
		}

		private Node SplitNode(Node node, double boxWidth, double boxheight)
		{
			node.IsOccupied = true;
			node.BottomNode = new Node { Pos_y = node.Pos_y, Pos_x = node.Pos_x + boxWidth, Height = node.Height, Width = node.Width - boxWidth };
			node.RightNode = new Node { Pos_y = node.Pos_y + boxheight, Pos_x = node.Pos_x, Height = node.Height - boxheight, Width = boxWidth };
			return node;
		}

		private void Filter()
		{
			foreach (var box in _boxes)
			{
				if (box.Position != null)
				{
					_boxesMain.Add(box);
				}
			}

			if (_boxesMain.Count < _counter)
			{
				_boxesMain.Clear();
			}

		}
		public List<Node> ResBoxes { get; set; } = new List<Node>();
	}
}
