namespace main
{
    partial class Levels
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
            this.level1Button = new System.Windows.Forms.Button();
            this.level2Button = new System.Windows.Forms.Button();
            this.level3Button = new System.Windows.Forms.Button();
            this.level4Button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // level1Button
            // 
            this.level1Button.Location = new System.Drawing.Point(12, 13);
            this.level1Button.Name = "level1Button";
            this.level1Button.Size = new System.Drawing.Size(102, 23);
            this.level1Button.TabIndex = 0;
            this.level1Button.Text = "Łatwy";
            this.level1Button.UseVisualStyleBackColor = true;
            this.level1Button.Click += new System.EventHandler(this.level1Button_Click);
            // 
            // level2Button
            // 
            this.level2Button.Location = new System.Drawing.Point(12, 42);
            this.level2Button.Name = "level2Button";
            this.level2Button.Size = new System.Drawing.Size(102, 23);
            this.level2Button.TabIndex = 1;
            this.level2Button.Text = "Średni";
            this.level2Button.UseVisualStyleBackColor = true;
            this.level2Button.Click += new System.EventHandler(this.level2Button_Click);
            // 
            // level3Button
            // 
            this.level3Button.Location = new System.Drawing.Point(12, 71);
            this.level3Button.Name = "level3Button";
            this.level3Button.Size = new System.Drawing.Size(102, 23);
            this.level3Button.TabIndex = 2;
            this.level3Button.Text = "Trudny";
            this.level3Button.UseVisualStyleBackColor = true;
            this.level3Button.Click += new System.EventHandler(this.level3Button_Click);
            // 
            // level4Button
            // 
            this.level4Button.Location = new System.Drawing.Point(12, 100);
            this.level4Button.Name = "level4Button";
            this.level4Button.Size = new System.Drawing.Size(102, 23);
            this.level4Button.TabIndex = 3;
            this.level4Button.Text = "Piekielnie Trudny";
            this.level4Button.UseVisualStyleBackColor = true;
            this.level4Button.Click += new System.EventHandler(this.level4Button_Click);
            // 
            // Levels
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.ClientSize = new System.Drawing.Size(125, 130);
            this.Controls.Add(this.level4Button);
            this.Controls.Add(this.level3Button);
            this.Controls.Add(this.level2Button);
            this.Controls.Add(this.level1Button);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Levels";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Wybór poziomu";
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Button level1Button;
        public System.Windows.Forms.Button level2Button;
        public System.Windows.Forms.Button level3Button;
        public System.Windows.Forms.Button level4Button;

    }
}