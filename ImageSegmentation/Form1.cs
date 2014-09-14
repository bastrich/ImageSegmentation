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
    public partial class Form1 : Form
    {

        private string fileName; //имя открываемого файла изображения
        public Bitmap inputBitmap; //поле длч хранения и работы с изображением

        public Form1()
        {
            InitializeComponent();
        }

        private void btn_OpenImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog(); //создаём объект OpenDialog
            openFile.Filter = "bmp|*.bmp|jpeg|*.jpg"; //задаём маску открываемых файлов

            if (openFile.ShowDialog() == DialogResult.OK) //показываем OpenDialog и если файл был успешно выбран
            {
                fileName = openFile.FileName; //получаем адрес файла

                inputBitmap = new Bitmap(fileName); //получаем доступ к изображению в файле

                SourceImage.Image = inputBitmap; //показываем изображение на форме

                btn_LoG.Enabled = true; //активируем кнопки для запуска алгоритмов обработки
                btn_Roberts.Enabled = true;
            }
        }

        private void btn_LoG_Click(object sender, EventArgs e) //реакция на событие нажатие кнопки для запуска алгоритма LoG
        {
            LOG LoG = new LOG(); //создание нового окна типа LOG
            LoG.Show(this); //показываем окно на экране, передаём в качестве аргументв ссылку на текущую форму, чтобы иметь доступ из открываемой форме к текущей
            //окно специально не модальное, чтобы можно было открыть м обработать сразу несколько разных изрбражений и рассматривать сразу все
        }

        private void btn_Roberts_Click(object sender, EventArgs e) //всё аналогично рекции на событие нажатие кнопки для запуска алгоритма LoG
        {
            Roberts Rob = new Roberts();
            Rob.Show(this);
        }
    }
}
