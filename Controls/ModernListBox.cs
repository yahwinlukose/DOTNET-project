using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.ComponentModel;

namespace MusicPlayer.Controls
{
    public class ModernListBox : ListBox
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color AccentColor { get; set; } = Color.FromArgb(52, 152, 219); // Default Blue
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color HoverColor { get; set; } = Color.FromArgb(45, 45, 45);
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color ItemBackColor { get; set; } = Color.FromArgb(30, 30, 30);
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color ItemForeColor { get; set; } = Color.White;
        
        private int _hoveredIndex = -1;

        public ModernListBox()
        {
            this.DrawMode = DrawMode.OwnerDrawFixed;
            this.ItemHeight = 45; // Taller items for modern look
            this.BorderStyle = BorderStyle.None;
            this.BackColor = ItemBackColor;
            this.ForeColor = ItemForeColor;
            this.DoubleBuffered = true;
            this.Font = new Font("Segoe UI", 11F, FontStyle.Regular);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            int index = this.IndexFromPoint(e.Location);
            if (index != _hoveredIndex)
            {
                int oldIndex = _hoveredIndex;
                _hoveredIndex = index;
                if (oldIndex >= 0 && oldIndex < this.Items.Count) this.Invalidate(this.GetItemRectangle(oldIndex));
                if (_hoveredIndex >= 0 && _hoveredIndex < this.Items.Count) this.Invalidate(this.GetItemRectangle(_hoveredIndex));
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            if (_hoveredIndex >= 0 && _hoveredIndex < this.Items.Count)
            {
                int oldIndex = _hoveredIndex;
                _hoveredIndex = -1;
                this.Invalidate(this.GetItemRectangle(oldIndex));
            }
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            if (e.Index < 0 || e.Index >= this.Items.Count) return;

            bool isSelected = (e.State & DrawItemState.Selected) == DrawItemState.Selected;
            bool isHovered = e.Index == _hoveredIndex;

            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // Background
            var bounds = e.Bounds;
            
            // Draw item background
            using (var bgBrush = new SolidBrush(ItemBackColor))
            {
                g.FillRectangle(bgBrush, bounds);
            }

            // Draw selection/hover state (rounded rect)
            var rect = new Rectangle(bounds.X + 5, bounds.Y + 2, bounds.Width - 10, bounds.Height - 4);
            
            if (isSelected)
            {
                using (var path = GetRoundedRect(rect, 8))
                using (var brush = new SolidBrush(AccentColor))
                {
                    g.FillPath(brush, path);
                }
            }
            else if (isHovered)
            {
                using (var path = GetRoundedRect(rect, 8))
                using (var brush = new SolidBrush(HoverColor))
                {
                    g.FillPath(brush, path);
                }
            }

            // Draw Text
            var item = this.Items[e.Index];
            string text = item.ToString() ?? "";

            string iconPart = "\uE8D6"; // Segoe MDL2 Music Note

            using (var textBrush = new SolidBrush(isSelected ? Color.White : ItemForeColor))
            using (var iconFont = new Font("Segoe MDL2 Assets", this.Font.Size))
            {
                var textFormat = new StringFormat
                {
                    LineAlignment = StringAlignment.Center,
                    Trimming = StringTrimming.EllipsisCharacter,
                    FormatFlags = StringFormatFlags.NoWrap
                };
                
                // Draw Icon
                var iconRect = new Rectangle(rect.X + 10, rect.Y, 20, rect.Height);
                g.DrawString(iconPart, iconFont, textBrush, iconRect, textFormat);

                // Draw Text
                var textRect = new Rectangle(rect.X + 35, rect.Y, rect.Width - 45, rect.Height);
                g.DrawString(text, this.Font, textBrush, textRect, textFormat);
            }
        }

        private GraphicsPath GetRoundedRect(Rectangle bounds, int radius)
        {
            int diameter = radius * 2;
            Size size = new Size(diameter, diameter);
            Rectangle arc = new Rectangle(bounds.Location, size);
            GraphicsPath path = new GraphicsPath();

            if (radius == 0)
            {
                path.AddRectangle(bounds);
                return path;
            }

            // top left arc
            path.AddArc(arc, 180, 90);
            // top right arc
            arc.X = bounds.Right - diameter;
            path.AddArc(arc, 270, 90);
            // bottom right arc
            arc.Y = bounds.Bottom - diameter;
            path.AddArc(arc, 0, 90);
            // bottom left arc
            arc.X = bounds.Left;
            path.AddArc(arc, 90, 90);
            path.CloseFigure();
            return path;
        }
    }
}
