using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Hsv20160620
{
    public class Hsv
    {
        public SaturationModelType SaturationModel = SaturationModelType.Cylinder;
        public enum SaturationModelType
        {
            Cone,       // 円錐モデル
            Cylinder,   // 円柱モデル
        }

        public float MinH { get { return 0.0f; } }
        public float MinS { get { return 0.0f; } }
        public float MinV { get { return 0.0f; } }

        public float MaxH { get { return 360.0f; } }
        public float MaxS { get { return 100.0f; } }
        public float MaxV { get { return 100.0f; } }

        private float _h;
        private float _s;
        private float _v;

        public float H { get { return _h; } set { if (MinH <= value  && value <= MaxH) { _h = value; } } }
        public float S { get { return _s; } set { if (MinS <= value && value <= MaxS) { _s = value; } } }
        public float V { get { return _v; } set { if (MinV <= value && value <= MaxV) { _v = value; } } }

        public System.Drawing.Color ToColor()
		{
            // ※HSV/RGBを0.0～1.0の浮動小数点数で表現した円柱モデルの場合
            float r = _v;
            float g = _v;
            float b = _v;
            if (_s > 0.0f) {
                //_h *= 6.0f;
                float h2 = _h / 60.0f;
                int i = (int)h2;
                float f = h2 - (float)i;
                switch (i) {
                    default:
                    case 0:
                        g *= 1 - _s * (1 - f);
                        b *= 1 - _s;
                        break;
                    case 1:
                        r *= 1 - _s * f;
                        b *= 1 - _s;
                        break;
                    case 2:
                        r *= 1 - _s;
                        b *= 1 - _s * (1 - f);
                        break;
                    case 3:
                        r *= 1 - _s;
                        g *= 1 - _s * f;
                        break;
                    case 4:
                        r *= 1 - _s * (1 - f);
                        g *= 1 - _s;
                        break;
                    case 5:
                        g *= 1 - _s;
                        b *= 1 - _s * f;
                        break;
                }
                if (SaturationModelType.Cone == SaturationModel)
                {
                    r /= _v;
                    g /= _v;
                    b /= _v;
                }
            }

            return System.Drawing.Color.FromArgb((int)(r * 255), (int)(g * 255), (int)(b * 255));
		}

        public void FromColor(System.Drawing.Color color)
        {
            float r = (float)color.R / 255.0f;
            float g = (float)color.G / 255.0f;
            float b = (float)color.B / 255.0f;

            float min = Math.Min(Math.Min(r, g), b);
            float max = Math.Max(Math.Max(r, g), b);

            _v = max;

            if (0 == max) { _s = 0; }
            else
            {
                if (SaturationModelType.Cone == SaturationModel) { _s = max - min; }
                else { _s = (max - min) / max; }
            }

            if (min == max) { H = 0; }
            else
            {
                if (min == b)
                {
                    _h = 60 * ((g - r) / (max - min)) + 60;
                }
                else if (min == r)
                {
                    _h = 60 * ((b - g) / (max - min)) + 180;
                }
                else // if (min == g)
                {
                    _h = 60 * ((r - b) / (max - min)) + 300;
                }

                if (_h < MinH || MaxH < _h) { _h %= MaxH; }
            }
        }
    }
}
