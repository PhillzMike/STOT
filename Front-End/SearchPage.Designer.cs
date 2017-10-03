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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SearchPage));
            this.TxtSearch1 = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.BtnClick = new MaterialSkin.Controls.MaterialRaisedButton();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SearchTime = new System.Windows.Forms.Label();
            this.ResultsWindow = new System.Windows.Forms.FlowLayoutPanel();
            this.Pages = new System.Windows.Forms.FlowLayoutPanel();
            this.KeyPressTimer = new System.Windows.Forms.Timer(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            this.ResultsWindow.SuspendLayout();
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
            this.TxtSearch1.Click += new System.EventHandler(this.TxtSearch1_Click);
            this.TxtSearch1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtSearch1_KeyPress);
            this.TxtSearch1.TextChanged += new System.EventHandler(this.TxtSearch1_TextChanged);
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
            this.BtnClick.Size = new System.Drawing.Size(73, 36);
            this.BtnClick.TabIndex = 1;
            this.BtnClick.Text = "Search";
            this.BtnClick.UseVisualStyleBackColor = true;
            this.BtnClick.Click += new System.EventHandler(this.BtnClick_Click);
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
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.linkLabel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 2);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(23, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 61.36364F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 38.63636F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 61F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1283, 104);
            this.tableLayoutPanel1.TabIndex = 6;
            this.tableLayoutPanel1.Visible = false;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel1.Location = new System.Drawing.Point(5, 2);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(153, 23);
            this.linkLabel1.TabIndex = 0;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Document Name";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(5, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Document address";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(5, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1271, 62);
            this.label2.TabIndex = 2;
            this.label2.Text = resources.GetString("label2.Text");
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 125);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(105, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Results Obtained in: ";
            // 
            // SearchTime
            // 
            this.SearchTime.AutoSize = true;
            this.SearchTime.Location = new System.Drawing.Point(124, 125);
            this.SearchTime.Name = "SearchTime";
            this.SearchTime.Size = new System.Drawing.Size(0, 13);
            this.SearchTime.TabIndex = 8;
            // 
            // ResultsWindow
            // 
            this.ResultsWindow.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ResultsWindow.BackColor = System.Drawing.SystemColors.Control;
            this.ResultsWindow.Controls.Add(this.tableLayoutPanel1);
            this.ResultsWindow.Location = new System.Drawing.Point(12, 141);
            this.ResultsWindow.Name = "ResultsWindow";
            this.ResultsWindow.Padding = new System.Windows.Forms.Padding(20, 0, 20, 0);
            this.ResultsWindow.Size = new System.Drawing.Size(1329, 684);
            this.ResultsWindow.TabIndex = 9;
            // 
            // Pages
            // 
            this.Pages.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Pages.BackColor = System.Drawing.Color.Transparent;
            this.Pages.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.Pages.Location = new System.Drawing.Point(503, 731);
            this.Pages.Name = "Pages";
            this.Pages.Size = new System.Drawing.Size(340, 37);
            this.Pages.TabIndex = 10;
            // 
            // KeyPressTimer
            // 
            this.KeyPressTimer.Interval = 500;
            this.KeyPressTimer.Tick += new System.EventHandler(this.KeyPressTimer_Tick);
            // 
            // SearchPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1378, 780);
            this.Controls.Add(this.Pages);
            this.Controls.Add(this.ResultsWindow);
            this.Controls.Add(this.SearchTime);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.BtnClick);
            this.Controls.Add(this.TxtSearch1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SearchPage";
            this.Text = "SearchPage";
            this.Load += new System.EventHandler(this.SearchPage_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResultsWindow.ResumeLayout(false);
            this.ResultsWindow.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MaterialSkin.Controls.MaterialSingleLineTextField TxtSearch1;
        private MaterialSkin.Controls.MaterialRaisedButton BtnClick;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label SearchTime;
        private System.Windows.Forms.FlowLayoutPanel ResultsWindow;
        private System.Windows.Forms.FlowLayoutPanel Pages;
        private System.Windows.Forms.Timer KeyPressTimer;
    }
}