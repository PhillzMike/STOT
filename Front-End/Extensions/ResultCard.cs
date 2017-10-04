using Engine;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Front_End {
    public class ResultCard : TableLayoutPanel {
        public ResultCard(Document x, String query) {
            LblBoldWords title = new LblBoldWords(query) {
                AutoSize = false,
                Width = 1271,
                Height = 23,
                Cursor = Cursors.Hand,
                ForeColor = Color.Blue,
                Text = "[ " +x.Type+" ] " + x.Name,
                Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)))
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
                AutoSize = false,
                Width = 1271,
                Height=62,
                Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))),
                Text = x.Relevance
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
