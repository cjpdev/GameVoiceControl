namespace GameVoiceControl
{
    partial class FormMain
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
            this.BtnConfigure = new System.Windows.Forms.Button();
            this.labelAction = new System.Windows.Forms.Label();
            this.checkBoxTopmost = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // BtnConfigure
            // 
            this.BtnConfigure.BackColor = System.Drawing.Color.Navy;
            this.BtnConfigure.Font = new System.Drawing.Font("Gill Sans Ultra Bold", 10.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnConfigure.ForeColor = System.Drawing.Color.White;
            this.BtnConfigure.Location = new System.Drawing.Point(14, 14);
            this.BtnConfigure.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.BtnConfigure.Name = "BtnConfigure";
            this.BtnConfigure.Size = new System.Drawing.Size(264, 85);
            this.BtnConfigure.TabIndex = 0;
            this.BtnConfigure.Text = "Configure";
            this.BtnConfigure.UseVisualStyleBackColor = false;
            this.BtnConfigure.Click += new System.EventHandler(this.btnConfigure_Click);
            // 
            // labelAction
            // 
            this.labelAction.BackColor = System.Drawing.Color.White;
            this.labelAction.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelAction.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labelAction.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAction.Location = new System.Drawing.Point(303, 20);
            this.labelAction.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.labelAction.Name = "labelAction";
            this.labelAction.Size = new System.Drawing.Size(214, 72);
            this.labelAction.TabIndex = 2;
            this.labelAction.Text = "n/a";
            this.labelAction.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // checkBoxTopmost
            // 
            this.checkBoxTopmost.AutoSize = true;
            this.checkBoxTopmost.Location = new System.Drawing.Point(565, 41);
            this.checkBoxTopmost.Name = "checkBoxTopmost";
            this.checkBoxTopmost.Size = new System.Drawing.Size(28, 27);
            this.checkBoxTopmost.TabIndex = 3;
            this.checkBoxTopmost.UseVisualStyleBackColor = true;
            this.checkBoxTopmost.CheckedChanged += new System.EventHandler(this.checkBoxTopmost_CheckedChanged);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 29F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(190)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(656, 116);
            this.Controls.Add(this.checkBoxTopmost);
            this.Controls.Add(this.labelAction);
            this.Controls.Add(this.BtnConfigure);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormMain";
            this.Opacity = 0.6D;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "GVC BY CHAY";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.VisibleChanged += new System.EventHandler(this.FormMain_VisibleChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BtnConfigure;
        private System.Windows.Forms.Label labelAction;
        private System.Windows.Forms.CheckBox checkBoxTopmost;
    }
}

