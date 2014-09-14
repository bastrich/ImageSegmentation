namespace ImageSegmentation
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SourceImage = new System.Windows.Forms.PictureBox();
            this.btn_OpenImage = new System.Windows.Forms.Button();
            this.btn_LoG = new System.Windows.Forms.Button();
            this.btn_Roberts = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.SourceImage)).BeginInit();
            this.SuspendLayout();
            // 
            // SourceImage
            // 
            this.SourceImage.Location = new System.Drawing.Point(27, 27);
            this.SourceImage.Name = "SourceImage";
            this.SourceImage.Size = new System.Drawing.Size(737, 296);
            this.SourceImage.TabIndex = 0;
            this.SourceImage.TabStop = false;
            // 
            // btn_OpenImage
            // 
            this.btn_OpenImage.Location = new System.Drawing.Point(70, 338);
            this.btn_OpenImage.Name = "btn_OpenImage";
            this.btn_OpenImage.Size = new System.Drawing.Size(91, 48);
            this.btn_OpenImage.TabIndex = 1;
            this.btn_OpenImage.Text = "Открыть изображение";
            this.btn_OpenImage.UseVisualStyleBackColor = true;
            this.btn_OpenImage.Click += new System.EventHandler(this.btn_OpenImage_Click);
            // 
            // btn_LoG
            // 
            this.btn_LoG.Enabled = false;
            this.btn_LoG.Location = new System.Drawing.Point(222, 348);
            this.btn_LoG.Name = "btn_LoG";
            this.btn_LoG.Size = new System.Drawing.Size(75, 23);
            this.btn_LoG.TabIndex = 2;
            this.btn_LoG.Text = "LoG";
            this.btn_LoG.UseVisualStyleBackColor = true;
            this.btn_LoG.Click += new System.EventHandler(this.btn_LoG_Click);
            // 
            // btn_Roberts
            // 
            this.btn_Roberts.Enabled = false;
            this.btn_Roberts.Location = new System.Drawing.Point(375, 348);
            this.btn_Roberts.Name = "btn_Roberts";
            this.btn_Roberts.Size = new System.Drawing.Size(75, 23);
            this.btn_Roberts.TabIndex = 3;
            this.btn_Roberts.Text = "Робертс";
            this.btn_Roberts.UseVisualStyleBackColor = true;
            this.btn_Roberts.Click += new System.EventHandler(this.btn_Roberts_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(825, 398);
            this.Controls.Add(this.btn_Roberts);
            this.Controls.Add(this.btn_LoG);
            this.Controls.Add(this.btn_OpenImage);
            this.Controls.Add(this.SourceImage);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.SourceImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox SourceImage;
        private System.Windows.Forms.Button btn_OpenImage;
        private System.Windows.Forms.Button btn_LoG;
        private System.Windows.Forms.Button btn_Roberts;
    }
}

