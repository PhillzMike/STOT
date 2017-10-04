using Engine;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Front_End {
    public class LblBoldWords : Label {
        HashSet<String> bolded;
        Semanter sam;
        public LblBoldWords(String bold) {
            this.bolded = new HashSet<string>();
            this.sam = Querier.Invt.Samantha;
            bold = bold.ToLower();
            String[] bolded = bold.Split(Semanter.punctuations, StringSplitOptions.RemoveEmptyEntries);
            foreach(string boldStem in bolded)
                this.bolded.Add(sam.StemWord(boldStem));
        }
        protected override void OnPaint(PaintEventArgs e) {
            Point drawPoint = new Point(0, 0);
            String[] keep = new[] { "]", "[" };
            String[] relevantWords = Semanter.Splitwords(Text, keep);
            //Fonts
            Font normalFont = this.Font;
            Font boldFont = new Font(normalFont, FontStyle.Bold);
            if (this.Font.Underline)
                boldFont = new Font(normalFont, newStyle: FontStyle.Bold | FontStyle.Underline);
            foreach (String word in relevantWords) {
                Size boldSize = TextRenderer.MeasureText(word, boldFont);
                Size normalSize = TextRenderer.MeasureText(word, normalFont);
                if (drawPoint.X + boldSize.Width > Width) {
                    drawPoint = new Point(0, drawPoint.Y + boldSize.Height);
                }
                if (bolded.Contains(sam.StemWord(word.ToLower()))) {
                    Rectangle boldRect = new Rectangle(drawPoint, boldSize);
                    drawPoint = new Point(drawPoint.X + boldRect.Width, drawPoint.Y);
                    TextRenderer.DrawText(e.Graphics, " "+word+" ", boldFont, boldRect, ForeColor);
                } else {
                    Rectangle normalRect = new Rectangle(drawPoint, normalSize);
                    drawPoint = new Point(drawPoint.X + normalSize.Width, drawPoint.Y);
                    TextRenderer.DrawText(e.Graphics, " "+word+" ", normalFont, normalRect, ForeColor);
                }

            }
        }
    }
}
