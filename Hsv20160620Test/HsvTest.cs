using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using NUnit.Framework;

namespace Hsv20160620Test
{
    [TestFixture]
    public class HsvTest
    {
        [Test]
        public void FromColorTest0()
        {
            Hsv20160620.Hsv hsv = new Hsv20160620.Hsv();

            hsv.FromColor(Color.FromArgb(0, 0, 0));
            Assert.AreEqual(0.0f, hsv.H);
            Assert.AreEqual(0.0f, hsv.S);
            Assert.AreEqual(0.0f, hsv.V);
        }

        [Test]
        public void FromColorTest1()
        {
            Hsv20160620.Hsv hsv = new Hsv20160620.Hsv();

            hsv.FromColor(Color.FromArgb(255, 255, 255));
            Assert.AreEqual(0.0f, hsv.H);
            Assert.AreEqual(0.0f, hsv.S);
            Assert.AreEqual(1.0f, hsv.V);
        }

        [Test]
        public void FromColorTest2()
        {
            Hsv20160620.Hsv hsv = new Hsv20160620.Hsv();

            hsv.FromColor(Color.FromArgb(127, 63, 106));
            Assert.AreEqual(320.0f, Math.Round(hsv.H));  // h=319.6875f
            Assert.AreEqual(0.5f, Math.Round(hsv.S, 1));
            Assert.AreEqual(0.5f, Math.Round(hsv.V, 1));
        }

        [Test]
        public void ToColorTest0()
        {
            Hsv20160620.Hsv hsv = new Hsv20160620.Hsv();
            hsv.H = 20.0f;
            hsv.S = 50.0f;
            hsv.V = 0.0f;   // 明度=0なら黒
            Color c = hsv.ToColor();

            // http://www.peko-step.com/tool/hsvrgb.html
            Assert.AreEqual(0, c.R);
            Assert.AreEqual(0, c.G);
            Assert.AreEqual(0, c.B);
        }

        [Test]
        public void ToColorTest1()
        {
            Hsv20160620.Hsv hsv = new Hsv20160620.Hsv();
            hsv.H = 136.0f;
            hsv.S = 0.0f;   // 彩度=0なら灰色
            hsv.V = 0.5f;
            Color c = hsv.ToColor();

            // http://www.peko-step.com/tool/hsvrgb.html
            Assert.AreEqual(127, c.R);
            Assert.AreEqual(127, c.G);
            Assert.AreEqual(127, c.B);
        }

        [Test]
        public void ToColorTest2()
        {
            Hsv20160620.Hsv hsv = new Hsv20160620.Hsv();
            hsv.H = 320;
            hsv.S = 0.5f;
            hsv.V = 0.5f;
            Color c = hsv.ToColor();
            System.Diagnostics.Debug.WriteLine("H=" + hsv.H + " S=" + hsv.S + " V" + hsv.V);
            System.Diagnostics.Debug.WriteLine("R=" + c.R + " G=" + c.G + " B="+ c.B);

            // http://www.peko-step.com/tool/hsvrgb.html
            Assert.AreEqual(127, c.R);
            Assert.AreEqual(63, c.G);
            Assert.AreEqual(106, c.B);
            //hsv.FromColor(Color.FromArgb(128, 50, 80)); // 336.923065f
            //Assert.AreEqual(0.0f, hsv.H);
            //Assert.AreEqual(0.0f, hsv.S);
            //Assert.AreEqual(1.0f, hsv.V);
        }

    }
}
