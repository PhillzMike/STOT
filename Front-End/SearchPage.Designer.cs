namespace Front_End
{
    partial class SearchPage
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
            this.TxtSearch1 = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.BtnClick = new MaterialSkin.Controls.MaterialRaisedButton();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // TxtSearch1
            // 
            this.TxtSearch1.Depth = 0;
            this.TxtSearch1.Hint = "";
            this.TxtSearch1.Location = new System.Drawing.Point(12, 24);
            this.TxtSearch1.MaxLength = 32767;
            this.TxtSearch1.MouseState = MaterialSkin.MouseState.HOVER;
            this.TxtSearch1.Name = "TxtSearch1";
            this.TxtSearch1.PasswordChar = '\0';
            this.TxtSearch1.SelectedText = "";
            this.TxtSearch1.SelectionLength = 0;
            this.TxtSearch1.SelectionStart = 0;
            this.TxtSearch1.Size = new System.Drawing.Size(503, 23);
            this.TxtSearch1.TabIndex = 0;
            this.TxtSearch1.TabStop = false;
            this.TxtSearch1.UseSystemPasswordChar = false;
            // 
            // BtnClick
            // 
            this.BtnClick.AutoSize = true;
            this.BtnClick.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BtnClick.Depth = 0;
            this.BtnClick.Icon = null;
            this.BtnClick.Location = new System.Drawing.Point(573, 11);
            this.BtnClick.MouseState = MaterialSkin.MouseState.HOVER;
            this.BtnClick.Name = "BtnClick";
            this.BtnClick.Primary = true;
            this.BtnClick.Size = new System.Drawing.Size(40, 36);
            this.BtnClick.TabIndex = 1;
            this.BtnClick.Text = "GO";
            this.BtnClick.UseVisualStyleBackColor = true;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(12, 53);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(503, 69);
            this.listBox1.TabIndex = 2;
            this.listBox1.Visible = false;
            // 
            // listView1
            // 
            this.listView1.Location = new System.Drawing.Point(12, 79);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(600, 531);
            this.listView1.TabIndex = 3;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // SearchPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(672, 552);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.BtnClick);
            this.Controls.Add(this.TxtSearch1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SearchPage";
            this.Text = "SearchPage";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MaterialSkin.Controls.MaterialSingleLineTextField TxtSearch1;
        private MaterialSkin.Controls.MaterialRaisedButton BtnClick;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.ListView listView1;
    }
}