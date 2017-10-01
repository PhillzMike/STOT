namespace Front_End {
    partial class Splashscreen {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.lblDetails = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Skip = new System.Windows.Forms.ToolTip(this.components);
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblDetails
            // 
            this.lblDetails.AutoSize = true;
            this.lblDetails.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDetails.Location = new System.Drawing.Point(41, 275);
            this.lblDetails.Name = "lblDetails";
            this.lblDetails.Size = new System.Drawing.Size(129, 23);
            this.lblDetails.TabIndex = 1;
            this.lblDetails.Text = "Starting App";
            this.lblDetails.SizeChanged += new System.EventHandler(this.LblDetails_SizeChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label1.Font = new System.Drawing.Font("Vivaldi", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(517, 289);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(136, 22);
            this.label1.TabIndex = 2;
            this.label1.Text = "Click to Continue";
            this.Skip.SetToolTip(this.label1, "Some Files will be unavailable, but will show up as time goes on");
            this.label1.Visible = false;
            this.label1.Click += new System.EventHandler(this.Label1_Click);
            // 
            // Skip
            // 
            this.Skip.ToolTipTitle = "Skip waiting for database to update.";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::Front_End.Properties.Resources.loading_dots1;
            this.pictureBox2.Location = new System.Drawing.Point(164, 283);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(36, 22);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 3;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Front_End.Properties.Resources.Logo;
            this.pictureBox1.Location = new System.Drawing.Point(80, 10);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(492, 288);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.DoubleClick += new System.EventHandler(this.Splashscreen_DoubleClick);
            // 
            // Splashscreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(650, 310);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblDetails);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Splashscreen";
            this.Text = "Splashscreen";
            this.Shown += new System.EventHandler(this.Splashscreen_Shown);
            this.DoubleClick += new System.EventHandler(this.Splashscreen_DoubleClick);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblDetails;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolTip Skip;
        private System.Windows.Forms.PictureBox pictureBox2;
    }
}