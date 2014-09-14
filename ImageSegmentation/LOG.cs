using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;



namespace ImageSegmentation
{
    public partial class LOG : Form
    {
        private int freq;
        private double[,] mask;
        private double norm;
        private Boolean inProc;

        private Boolean stdMask = false;

        //double debug1, debug2;

        public LOG()
        {
            InitializeComponent();
        }

        private void LOG_Load(object sender, EventArgs e)
        {
            button1.Enabled = false;
            DstImage.Image = ((Form1)(this.Owner)).inputBitmap;
        }

        private Bitmap LoG(Bitmap SrcImage, PictureBox picture = null)
        {

            Bitmap bitmap = new Bitmap(SrcImage); //создаём новое пустое изображение
            
          
            

            for (int j = 0; j < SrcImage.Height; j++) //проходим по всем пикселям исходного изображения
            {
                for (int i = 0; i < SrcImage.Width; i++)
                {
                    double c = 0d;//переменная для получения нового значения яркости

                    //применяем маску в окрестности текущего пикселя исходного изображения
                    //то есть за новое значение яркости в данной точке берём сумму произаедений коэффициентво маски на яркости соотвествующих пикселей\
                    //причём на текуший обрабатываемый пиксель устанавливается центр маски
            
                    for (int k = getLow(mask.GetLength(0),j); k <= getHigh(mask.GetLength(0),j,SrcImage.Height-1); k++)
                    {
                        for (int l = getLow(mask.GetLength(1), i); l <= getHigh(mask.GetLength(1), i, SrcImage.Width - 1); l++)
                        {
                            c += mask[mask.GetLength(0)/2 + k, mask.GetLength(1)/2 + l] * SrcImage.GetPixel(i + l, j + k).GetBrightness();
                        }
                    }

                    //сам процесс нормировки
                    if (stdMask)
                    {
                        c = c / (1 + Math.Abs(c));//затемняем места с маленькой яркостью для более чёткой картины
                        c = c / norm; //нормируем
                    }

    
                    if (c < 0) c = (1 + c); //в результате применения такой маски могут получиться отрицательные значения яркости
                    //то есть применение данной маски предполагает,  что можно будет задать яркость для нового изображения как для негатива
                    //поэтому отрицательные значения яркости нужно переводить в нормальные путём их вычитания из максимальной яркости

                    bitmap.SetPixel(i, j, Color.FromArgb((int)(255 * (c)), (int)(255 * (c)), (int)(255 * (c)))); //установка нового значения пикселя
                    if (picture != null && (i*j) % freq == 0)
                    {
                        picture.Image = bitmap;
                        picture.Refresh();
                        Application.DoEvents();
                    }
                }
            }
             
          
            return bitmap; //возвращаем обработанное изображение
        }

        private double[,] getLoGMask(double sigma, int n)
        {
            double[,] mask = new double[n, n];

            for (int i = -mask.GetLength(0)/2; i <= mask.GetLength(0)/2; i++)
            {
                for (int j = -mask.GetLength(1)/2; j <= mask.GetLength(1) / 2; j++)
                {
                    mask[i + mask.GetLength(0) / 2, j + mask.GetLength(0) / 2] = -1 * (1 / (Math.PI * Math.Pow(sigma, 4))) * (1 - ((i * i + j * j) / (2 * sigma * sigma))) * Math.Exp(-((i * i + j * j) / (2 * sigma * sigma)));
                }
            }

            for (int i = 0; i < mask.GetLength(0); i++)
            {
                for (int j = 0; j < mask.GetLength(1); j++)
                {
                    mask[i, j] = (mask[i, j]);
                }
            }

   

            return mask;
        }

        private int getLow(int dim, int cur)
        {
            if (cur >= dim / 2) return -dim / 2;
            else return -cur;
        }

        private int getHigh(int dim, int cur, int size)
        {
            if ((size-cur) >= dim / 2) return dim / 2;
            else return size-cur;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            inProc = true;
            if ((freq = int.Parse(textBox3.Text)) == 0) MessageBox.Show("Частота обновления не может быть равной 0");
            else
            {
                button1.Enabled = false;
                LoG(((Form1)(this.Owner)).inputBitmap,DstImage).Save("LoG" + System.DateTime.Now.ToString().Replace(':', '-') + ".bmp"); //вызываем функцию обработки изображения от исходного изображения на главной форме и сохраняем обработанное изображение в файл
                button1.Enabled = true;
            }
            inProc = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if ((int.Parse(textBox2.Text) == 0) || (int.Parse(textBox2.Text) % 2 == 0)) System.Windows.Forms.MessageBox.Show("Размерность маски должна быть нечётная и не равна 0");
            else if (Double.Parse(textBox1.Text) == 0) MessageBox.Show("Среднеквадратическое отклонение должно быть не равно 0");
            else
            {
                mask = getLoGMask(Double.Parse(textBox1.Text), int.Parse(textBox2.Text));
                maskv.ColumnCount = maskv.RowCount = mask.GetLength(0);
                for (int i = 0; i < mask.GetLength(0); i++)
                {
                    maskv.Columns[i].Width = 30;
                    for (int j = 0; j <mask.GetLength(1); j++)
                    {                      
                        maskv.Rows[i].Cells[j].Value = mask[i, j];
                    }
                }
            }
            stdMask = false;
            if (!inProc) button1.Enabled = true;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBox1.Checked)
            {
                textBox1.Enabled = false;
                textBox2.Enabled = false;
                button2.Enabled = false;
                label1.Enabled = false;
                label2.Enabled = false;
                stdMask = true;
                mask = new double[9, 9]{
                                {0,0,3,2,2,2,3,0,0},
                                {0,2,3,5,5,5,3,2,0},
                                {3,3,5,3,0,3,5,3,3},
                                {2,5,3,-12,-23,-12,3,5,2},
                                {2,5,0,-23,-40,-23,0,5,2},
                                {2,5,3,-12,-23,-12,3,5,2},
                                {3,3,5,3,0,3,5,3,3},
                                {0,2,3,5,5,5,3,2,0},
                                {0,0,3,2,2,2,3,0,0}
                                }; //матрица для обхода и наложения на исходное изображение

                double normpos = 0, normneg = 0; //для расчёта нормировочного коэффициента

                //так как значения яркости колеблются от 0 до 1, то для нормировки надо сложить отдельно все отрицательные и положительны элементы матрицы
                //друг с другом, а потом в качестве нормировочного коэффициента взять модуль наибольшей по модулю суммы
                for (int i = 0; i < (int)Math.Sqrt(mask.Length); i++) //считаем нормировочный коэффициент для данной маски
                {
                    for (int j = 0; j < (int)Math.Sqrt(mask.Length); j++)
                    {
                        if (mask[i, j] > 0) normpos += mask[i, j];
                        else if (mask[i, j] < 0) normneg += mask[i, j];
                    }
                }
                norm = normpos > Math.Abs(normneg) ? normpos : Math.Abs(normneg); //получаем окончательное значения нормировочного коэффициента
                
                maskv.ColumnCount = maskv.RowCount = mask.GetLength(0);
                for (int i = 0; i < mask.GetLength(0); i++)
                {
                    maskv.Columns[i].Width = 30;
                    for (int j = 0; j < mask.GetLength(1); j++)
                    {
                        maskv.Rows[i].Cells[j].Value = mask[i, j];
                    }
                }
                if (!inProc) button1.Enabled = true;
            }
            else
            {
                textBox1.Enabled = true;
                textBox2.Enabled = true;
                button2.Enabled = true;
                label1.Enabled = true;
                label2.Enabled = true;
            }
        }


    }
}
