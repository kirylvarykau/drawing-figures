﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.IO;

namespace my_primitive_paint
{
    public partial class mainForm : Form
    {
        public Bitmap bmap;
        public Graphics graphics;
        public mainForm()
        {
            InitializeComponent();
            bmap = new Bitmap(pictrueDrawing.Height, pictrueDrawing.Width);
            graphics = Graphics.FromImage(bmap);
        }

        
        private void drawButton_Click(object sender, EventArgs e)
        {
            
            Point point1 = new Point(450, 50);
            Point point2 = new Point(370, 150);
            Point point3 = new Point(540, 100);
            Point point4 = new Point(360, 100);
            Point point5 = new Point(530, 150);
            Point[] points =
             {
                 point1,
                 point2,
                 point3,
                 point4,
                 point5
             
             };

            List<MainFigure> Figures = new List<MainFigure>();
            Figures.Add(new Square(4, Color.Aqua, new Point(30, 30), new Point(130,130)));
            Figures.Add(new Rectangle(3, Color.Black, new Point(150, 30), new Point(250, 90)));
            Figures.Add(new Ellipse(2, Color.Aquamarine, new Point(200, 110), new Point(350, 200)));
            Figures.Add(new Circle(4, Color.Aqua, new Point(30, 200), new Point(130, 130))); 
            Figures.Add(new Polygon(5, Color.Chocolate, points));

            ListOfFigures listOfFigures = new ListOfFigures(Figures);
            listOfFigures.Draw(graphics);

            pictrueDrawing.Image = bmap;

        }

        private MainFigure figure;
        private Fabric maker;
        private List<MainFigure> drawnFigures = new List<MainFigure>();

        private bool IsInt(string x1, string y1, string x2, string y2)
        {
            int res = 0;
            if (Int32.TryParse(x1, out res) && Int32.TryParse(y1, out res) && Int32.TryParse(x2, out res) && Int32.TryParse(y2, out res))
            {
                return true;
            }
            
            return false;
        }

        private const int fatness = 4;
        private Color color = Color.Aquamarine;

        private void draw_Click(object sender, EventArgs e)
        {

            if (IsInt(tb_x1.Text, tb_y1.Text, tb_x2.Text, tb_y2.Text) && (rb_circle.Checked == true || rb_ellipse.Checked == true ||
                rb_reactangle.Checked == true || rb_square.Checked == true) && ((Convert.ToInt32(tb_x1.Text, 10) < pictrueDrawing.Width) &&
                (Convert.ToInt32(tb_y1.Text, 10) < pictrueDrawing.Height) && (Convert.ToInt32(tb_x2.Text, 10) < pictrueDrawing.Width) &&
                (Convert.ToInt32(tb_y2.Text, 10) < pictrueDrawing.Height)))
            {
                figure = maker.FactoryMethod(fatness, color,
                                new Point(Convert.ToInt32(tb_x1.Text, 10), Convert.ToInt32(tb_y1.Text)),
                                new Point(Convert.ToInt32(tb_x2.Text, 10), Convert.ToInt32(tb_y2.Text)));
                figure.Draw(graphics);
                drawnFigures.Add(figure);

                pictrueDrawing.Image = bmap;
            } else
            {
                tb_x1.Text = tb_x2.Text = tb_y1.Text = tb_y2.Text = "";
                MessageBox.Show("Invalid coordinate entered or no figure selected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
            

            
        }

        private void rb_square_CheckedChanged(object sender, EventArgs e)
        {
            maker = new SquareFabric();
        }

        private void rb_reactangle_CheckedChanged(object sender, EventArgs e)
        {
            maker = new RectangleFabric();
        }

        private void rb_ellipse_CheckedChanged(object sender, EventArgs e)
        {
            maker = new EllipseFabric();
        }

        private void rb_circle_CheckedChanged(object sender, EventArgs e)
        {
            maker = new CircleFabric();
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            graphics.Clear(Color.White);
            pictrueDrawing.Image = bmap;
        }


        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.InitialDirectory = ".";
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.FileName = "figures";
            saveFileDialog.DefaultExt = ".json";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                StreamWriter stream = new StreamWriter(saveFileDialog.OpenFile());

                var json = JsonConvert.SerializeObject(drawnFigures);

                stream.Write(json);
                stream.Close();

            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.InitialDirectory = "/files";
            openFileDialog.RestoreDirectory = true;
            openFileDialog.FileName = "figures";
            openFileDialog.DefaultExt = ".json";

             if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                StreamReader stream = new StreamReader(openFileDialog.OpenFile());

                string json = stream.ReadToEnd();

               // List<MainFigure> mainFigures = JsonConvert.DeserializeObject<List<MainFigure>>(json);

                stream.Close();

                //ListOfFigures listOfFigures = new ListOfFigures(mainFigures);
               // listOfFigures.Draw(graphics);


            }
                   

        }
    }

       
}
