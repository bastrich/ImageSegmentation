using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using ColorTransform;

namespace ImageSegmentation
{
    public partial class Roberts : Form
    {

        private Bitmap outBitmap; //поле для хранения обработанного изображения

        public Roberts()
        {
            InitializeComponent();
        }

        private void Roberts_Load(object sender, EventArgs e)
        {
            outBitmap = roberts(((Form1)(this.Owner)).inputBitmap); //вызываем функцию обработки изображения от исходного изображения на главной форме
            DstImage.Image = outBitmap; //выводим обработанное изображение на экран
            outBitmap.Save("Roberts" + System.DateTime.Now.ToString() .Replace(':','-')+".bmp"); //сохраняем обработанное изображение в файл
        }

        private Bitmap roberts(Bitmap SrcImage)
        {
            Bitmap bitmap = new Bitmap(SrcImage.Width, SrcImage.Height);//создаём новое пустое изображение

            for (int i = 0; i < SrcImage.Width-1; i++) // делаем обход всех(кроме крайних правых и нижних) пикселец на изображении
            {
                for (int j = 0; j < SrcImage.Height-1; j++)
                {
                    /*float c,c1,c2;

                    HSV hsv1 = HSV.FromRGB(SrcImage.GetPixel(i, j).R, SrcImage.GetPixel(i, j).G, SrcImage.GetPixel(i, j).B);
                    HSV hsv2 = HSV.FromRGB(SrcImage.GetPixel(i + 1, j).R, SrcImage.GetPixel(i + 1, j).G, SrcImage.GetPixel(i + 1, j).B);
                    HSV hsv3 = HSV.FromRGB(SrcImage.GetPixel(i, j + 1).R, SrcImage.GetPixel(i, j + 1).G, SrcImage.GetPixel(i, j + 1).B);
                    HSV hsv4 = HSV.FromRGB(SrcImage.GetPixel(i + 1, j + 1).R, SrcImage.GetPixel(i + 1, j + 1).G, SrcImage.GetPixel(i + 1, j + 1).B);

                    c1 = Math.Abs(hsv1.V - hsv4.V);
                    c2 = Math.Abs(hsv2.V - hsv3.V);
                    c = (float) Math.Sqrt(Math.Pow(c1,2) + Math.Pow(c2,2));

                    hsv1.V = c; 

                    bitmap.SetPixel(i,j,hsv1.getRGB());
                     */
                    

                    //перекрёстный оператор Робертса представляет собой две матрицы 2х2
                    // 1  0
                    // 0 -1
                    //   и
                    // 0  1
                    //-1  0

                    //соответственно применяем к теущему обрабатываемому пикселю первую маску
                    //причём первый элемент маски устанавливается на текущий пиксель
                    float c1 = Math.Abs(SrcImage.GetPixel(i, j).GetBrightness() - SrcImage.GetPixel(i + 1, j + 1).GetBrightness());
                    //применяем вторую маску
                    float c2 = Math.Abs(SrcImage.GetPixel(i + 1, j).GetBrightness() - SrcImage.GetPixel(i, j + 1).GetBrightness());
                    //высчитываем результирующее значение яроксти
                    float c = (float)Math.Sqrt(Math.Pow(c1, 2)/2 + Math.Pow(c2, 2)/2);
                    //устанавливаем его
                    bitmap.SetPixel(i, j, Color.FromArgb((int)(255*(1-c)),(int)(255*(1-c)),(int)(255*(1-c))));//Устанавливается яркость 1-с, а не с для того, чтобы были видны тёмные границы на светлом фоне, а не наоборот
                    
                }
            }

            return bitmap; //возвращаем обработанное изображение
        }
    }
}
