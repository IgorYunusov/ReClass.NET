﻿using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ReClassNET.UI
{
	public class BannerBox : Control, ISupportInitialize
	{
		public const int DefaultBannerHeight = 48;

		private bool inInitialize;

		private Image icon;
		private string title;
		private string text;

		private Image image;

		public Image Icon { get { return icon; } set { icon = value; UpdateBanner(); } }

		public string Title { get { return title; } set { title = value ?? string.Empty; UpdateBanner(); } }

		public override string Text { get { return text; } set { text = value ?? string.Empty; UpdateBanner(); } }

		public BannerBox()
		{
			title = string.Empty;
			text = string.Empty;
		}

		protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
		{
			int oldWidth = Width;

			base.SetBoundsCore(x, y, width, DefaultBannerHeight, specified);

			if (oldWidth != width && width > 0)
			{
				UpdateBanner();
			}
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			if (image != null)
			{
				e.Graphics.DrawImage(image, ClientRectangle);
			}
		}

		public void BeginInit()
		{
			inInitialize = true;
		}

		public void EndInit()
		{
			inInitialize = false;

			UpdateBanner();
		}

		private void UpdateBanner()
		{
			if (inInitialize)
			{
				return;
			}

			try
			{
				var oldImage = image;

				image = BannerFactory.CreateBanner(Width, DefaultBannerHeight, icon, title, text, true);

				oldImage?.Dispose();

				Invalidate();
			}
			catch
			{

			}
		}
	}
}
