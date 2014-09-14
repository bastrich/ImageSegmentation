using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ColorTransform
{
    public struct HSV
    {
        #region [Fields]

        private float h;
        private float s;
        private float v;

        #endregion

        #region [Properties]

        public float H
        {
            get
            {
                return h;
            }
            set
            {
                h = value;
            }
        }

        public float S
        {
            get
            {
                return s;
            }
            set
            {
                s = value;
            }
        }

        public float V
        {
            get
            {
                return v;
            }
            set
            {
                if (value > 100) v = 100;
                else v = value;
            }
        }

        #endregion

        public HSV (float hin, float sin, float vin)
        {
            h = hin;
            s = sin;
            v = vin;
        }

        public Color getRGB()
        {
            int Hi = Convert.ToInt32(Math.Floor(H/60));

            float Vmin = ((100 - S) * V) / 100;

            float a = (V - Vmin) * ((H % 60) / 60);

            float Vinc = Vmin + a;
            float Vdec = V - a;

            float R, G, B;

            switch (Hi)
            {
                case 0:
                    R = V;
                    G = Vinc;
                    B = Vmin;
                    break;
                case 1:
                    R = Vdec;
                    G = V;
                    B = Vmin;
                    break;
                case 2:
                    R = Vmin;
                    G = V;
                    B = Vinc;
                    break;
                case 3:
                    R = Vmin;
                    G = Vdec;
                    B = V;
                    break;
                case 4:
                    R = Vinc;
                    G = Vmin;
                    B = V;
                    break;
                case 5:
                    R = V;
                    G = Vmin;
                    B = Vdec;
                    break;
                default:
                    R = G = B = 0;
                    break;
            }

            return Color.FromArgb(Convert.ToInt32((255 * R)/100),Convert.ToInt32( (255 * G)/100),Convert.ToInt32( (255 * B))/100);
        }
        
        public static HSV FromRGB(int R,int G,int B)
        {
            float r = (float)R / 255;
            float g = (float)G / 255;
            float b = (float)B / 255;

            float MAX = r, MIN = r;

            float[] tmp = { r, g, b };

            foreach (float f in tmp)
            {
                if (f > MAX) MAX = f;
                if (f < MIN) MIN = f;
            }

            float H,S,V;

            if (MAX == MIN) H = 0;
            else if (MAX == r && g >= b) H = (60 * (g - b)) / (MAX - MIN);
            else if (MAX == r && g < b) H = (60 * (g - b)) / (MAX - MIN) + 360;
            else if (MAX == g) H = (60 * (b - r)) / (MAX - MIN) + 120;
            else if (MAX == b) H = (60 * (r - g)) / (MAX - MIN) + 240;
            else H = 0;

            if (MAX == 0) S = 0;
            else S = 1 - MIN / MAX;

            V = MAX;

            return new HSV(H, S * 100, V * 100);
        }
    }
}
