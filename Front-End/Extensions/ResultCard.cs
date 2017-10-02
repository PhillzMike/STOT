using Engine;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace Front_End {
    public class ResultCard : TableLayoutPanel {
        public ResultCard(Document x, String query) {
            LinkLabel title = new LinkLabel {
                AutoSize = true,
                Text = "[" + x.Type + "] " + x.Name,
                Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)))
            };
            title.Click += (o, i) =>
            {
                try {
                    Process.Start(x.Address);
                } catch {

                }
            };
            Label adress = new Label {
                AutoSize = true,
                Text = x.LastModified.ToString() + " " + x.Address,
                Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)))
            };
            LblBoldWords Relevance = new LblBoldWords(query) {
                AutoSize = true,
                Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))),
                Text = "Document Relevance At present it is not too much to say that it is of such weight it may have an influence upon European history. I promise,said Holmes. And I.You will excuse this mask,continued our strange visitor. The august person who employs me wishes his agent to be unknown to you,and I may confess at once that the title by which I have just called myself is not exactly my own.I was aware of it,said Holmes dryly.You are sure that she has not sent it yet?I love and am loved by a better man than he.The King may do what he will without hindrance from one whom he has cruelly wronged.I keep it only to safeguard myself, and to preserve a weapon which will always secure me from any steps which he might take in the future. I leave a photograph which he might care to possess; and I remain, dear Mr. Sherlock Holmes,Very truly yours,IRENE NORTON, nee ADLER.hat a woman--oh, what a woman! cried the King of Bohemia, when we had all three read this epistle. Did I not tell you how quick and resolute she was? Would she not have made an admirable queen? Is it not a pity that she was not on my level?From what I have seen of the lady, she seems, indeed, to be on a very different level to your Majesty, said Holmes coldly. I am sorry that I have not been able to bring your Majesty's business to a more successful conclusion.On the contrary, my dear sir, cried the King; nothing could be more successful. I know that her"
            };
            ColumnCount = 1;
            ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            Controls.Add(title, 0, 0);
            Controls.Add(adress, 0, 1);
            Controls.Add(Relevance, 0, 2);
            Name = x.Address;
            Padding = new System.Windows.Forms.Padding(2);
            RowCount = 3;
            RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 61.36364F));
            RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 38.63636F));
            RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 61F));
            Size = new System.Drawing.Size(1534, 107);
        }
    }
}
