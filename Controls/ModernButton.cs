using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;

namespace MusicPlayer.Controls
{
    public class ModernButton : Button
    {
        private Color _hoverColor = Color.FromArgb(41, 128, 185);
        private Color _pressedColor = Color.FromArgb(31, 97, 141);
        private Color _normalColor = Color.FromArgb(45, 45, 45);
        
        private int _borderRadius = 10;
        private bool _isHovered = false;
        private bool _isPressed = false;
        
        // Let the developer override colors easily in designer
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color HoverColor { get => _hoverColor; set { _hoverColor = value; Invalidate(); } }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color PressedColor { get => _pressedColor; set { _pressedColor = value; Invalidate(); } }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color NormalColor { get => _normalColor; set { _normalColor = value; Invalidate(); } }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int BorderRadius { get => _borderRadius; set { _borderRadius = value; Invalidate(); } }

        private static readonly KeyValuePair<string, string>[] EmojiToIcon = new KeyValuePair<string, string>[]
        {
            new KeyValuePair<string, string>("◀◀", "\uE892"), // Previous
            new KeyValuePair<string, string>("▶▶", "\uE893"), // Next
            new KeyValuePair<string, string>("▶", "\uE768"), // Play
            new KeyValuePair<string, string>("⏸", "\uE769"), // Pause
            new KeyValuePair<string, string>("🔀", "\uE8B1"), // Shuffle
            new KeyValuePair<string, string>("🔁", "\uE8EE"), // Repeat
            new KeyValuePair<string, string>("🔂", "\uE8ED"), // Repeat One
            new KeyValuePair<string, string>("⏹", "\uE71A"), // Stop
            new KeyValuePair<string, string>("⏩", "\uE8D0"), // FastForward
            new KeyValuePair<string, string>("＋", "\uE710"), // Add
            new KeyValuePair<string, string>("🗑", "\uE74D"), // Delete
            new KeyValuePair<string, string>("★", "\uE734"), // Favorite
            new KeyValuePair<string, string>("✕", "\uE894")  // Clear
        };

        public ModernButton()
        {
            this.FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.BorderSize = 0;
            this.BackColor = _normalColor;
            this.ForeColor = Color.White;
            this.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            this.Cursor = Cursors.Hand;
            this.DoubleBuffered = true;
        }

        protected override void OnMouseEnter(System.EventArgs e)
        {
            base.OnMouseEnter(e);
            _isHovered = true;
            Invalidate();
        }

        protected override void OnMouseLeave(System.EventArgs e)
        {
            base.OnMouseLeave(e);
            _isHovered = false;
            Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            base.OnMouseDown(mevent);
            _isPressed = true;
            Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            base.OnMouseUp(mevent);
            _isPressed = false;
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            var graphics = pevent.Graphics;
            graphics.SmoothingMode = SmoothingMode.AntiAlias;

            var rect = new Rectangle(0, 0, this.Width, this.Height);
            
            Color currentColor = _normalColor;
            if (_isPressed)
                currentColor = _pressedColor;
            else if (_isHovered)
                currentColor = _hoverColor;

            using (var path = GetFigurePath(rect, _borderRadius))
            {
                this.Region = new Region(path);
                using (var brush = new SolidBrush(currentColor))
                {
                    graphics.FillPath(brush, path);
                }
            }

            if (!string.IsNullOrEmpty(this.Text))
            {
                string rawText = this.Text;
                string iconPart = "";
                string textPart = rawText;

                // Strip shuffle/repeat status text for circular buttons to avoid overflow
                if (this.Name == "btnShuffle" || this.Name == "btnRepeat")
                {
                    if (rawText.StartsWith("🔀") || rawText.StartsWith("🔁") || rawText.StartsWith("🔂"))
                    {
                        var stringInfo = new System.Globalization.StringInfo(rawText);
                        textPart = stringInfo.SubstringByTextElements(0, 1);
                    }
                }

                // Check if text begins with a known emoji
                foreach (var kvp in EmojiToIcon)
                {
                    if (textPart.StartsWith(kvp.Key))
                    {
                        iconPart = kvp.Value;
                        textPart = textPart.Substring(kvp.Key.Length).Trim();
                        break;
                    }
                }

                // Draw Icon and Text
                using (var iconFont = new Font("Segoe MDL2 Assets", this.Font.Size + 2, FontStyle.Regular))
                {
                    if (!string.IsNullOrEmpty(iconPart) && string.IsNullOrEmpty(textPart))
                    {
                        // Icon only, centered
                        TextRenderer.DrawText(graphics, iconPart, iconFont, rect, this.ForeColor, 
                            TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.SingleLine);
                    }
                    else if (!string.IsNullOrEmpty(iconPart) && !string.IsNullOrEmpty(textPart))
                    {
                        // Icon + Text
                        // Measure icon
                        Size iconSize = TextRenderer.MeasureText(graphics, iconPart, iconFont);
                        Size textSize = TextRenderer.MeasureText(graphics, textPart, this.Font);

                        int totalWidth = iconSize.Width + 5 + textSize.Width;
                        int startX = (rect.Width - totalWidth) / 2;

                        Rectangle iconRect = new Rectangle(startX, 0, iconSize.Width, rect.Height);
                        Rectangle textRect = new Rectangle(startX + iconSize.Width + 5, 0, textSize.Width, rect.Height);

                        TextRenderer.DrawText(graphics, iconPart, iconFont, iconRect, this.ForeColor, 
                            TextFormatFlags.VerticalCenter | TextFormatFlags.Left | TextFormatFlags.SingleLine);
                        TextRenderer.DrawText(graphics, textPart, this.Font, textRect, this.ForeColor, 
                            TextFormatFlags.VerticalCenter | TextFormatFlags.Left | TextFormatFlags.SingleLine);
                    }
                    else
                    {
                        // Text only
                        TextRenderer.DrawText(graphics, textPart, this.Font, rect, this.ForeColor, 
                            TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.SingleLine);
                    }
                }
            }
        }

        private GraphicsPath GetFigurePath(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            float curveSize = radius * 2F;
            path.StartFigure();
            
            if (radius > 0)
            {
                path.AddArc(rect.X, rect.Y, curveSize, curveSize, 180, 90);
                path.AddArc(rect.Right - curveSize, rect.Y, curveSize, curveSize, 270, 90);
                path.AddArc(rect.Right - curveSize, rect.Bottom - curveSize, curveSize, curveSize, 0, 90);
                path.AddArc(rect.X, rect.Bottom - curveSize, curveSize, curveSize, 90, 90);
            }
            else
            {
                path.AddRectangle(rect);
            }
            
            path.CloseFigure();
            return path;
        }
    }
}
