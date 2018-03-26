﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace my_primitive_paint
{
    public class Ellipse : MainFigure
    {
        private float fatness;
        public Ellipse(float fatness, int x, int y, int width, int height) : base(x, y, width, height)
        {
            this.fatness = fatness;
        }
        public override void Draw(Graphics graphics)
        {
            Pen pen = new Pen(Color.Yellow, fatness);
            graphics.DrawEllipse(pen, x, y, width, height);
        }
    }
}
