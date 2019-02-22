namespace SolentimTest
{
    partial class Form1
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
            this.m_panel = new System.Windows.Forms.Panel();
            this.m_placeButton = new System.Windows.Forms.Button();
            this.m_posX = new System.Windows.Forms.NumericUpDown();
            this.m_posY = new System.Windows.Forms.NumericUpDown();
            this.m_label = new System.Windows.Forms.Label();
            this.m_DFSButton = new System.Windows.Forms.Button();
            this.m_BFSButton = new System.Windows.Forms.Button();
            this.m_AStarButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.m_posX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_posY)).BeginInit();
            this.SuspendLayout();
            // 
            // m_panel
            // 
            this.m_panel.BackColor = System.Drawing.SystemColors.ControlDark;
            this.m_panel.Location = new System.Drawing.Point(161, 0);
            this.m_panel.Name = "m_panel";
            this.m_panel.Size = new System.Drawing.Size(628, 473);
            this.m_panel.TabIndex = 0;
            this.m_panel.Paint += new System.Windows.Forms.PaintEventHandler(this.m_panel_Paint);
            // 
            // m_placeButton
            // 
            this.m_placeButton.Location = new System.Drawing.Point(12, 136);
            this.m_placeButton.Name = "m_placeButton";
            this.m_placeButton.Size = new System.Drawing.Size(143, 56);
            this.m_placeButton.TabIndex = 1;
            this.m_placeButton.Text = "Place";
            this.m_placeButton.UseVisualStyleBackColor = true;
            this.m_placeButton.Click += new System.EventHandler(this.m_placeButton_Click);
            // 
            // m_posX
            // 
            this.m_posX.Location = new System.Drawing.Point(13, 86);
            this.m_posX.Maximum = new decimal(new int[] {
            63,
            0,
            0,
            0});
            this.m_posX.Name = "m_posX";
            this.m_posX.Size = new System.Drawing.Size(56, 22);
            this.m_posX.TabIndex = 2;
            // 
            // m_posY
            // 
            this.m_posY.Location = new System.Drawing.Point(75, 86);
            this.m_posY.Maximum = new decimal(new int[] {
            63,
            0,
            0,
            0});
            this.m_posY.Name = "m_posY";
            this.m_posY.Size = new System.Drawing.Size(56, 22);
            this.m_posY.TabIndex = 3;
            // 
            // m_label
            // 
            this.m_label.AutoSize = true;
            this.m_label.Location = new System.Drawing.Point(13, 35);
            this.m_label.Name = "m_label";
            this.m_label.Size = new System.Drawing.Size(118, 17);
            this.m_label.TabIndex = 4;
            this.m_label.Text = "Obstacle Position";
            // 
            // m_DFSButton
            // 
            this.m_DFSButton.Location = new System.Drawing.Point(16, 334);
            this.m_DFSButton.Name = "m_DFSButton";
            this.m_DFSButton.Size = new System.Drawing.Size(115, 53);
            this.m_DFSButton.TabIndex = 5;
            this.m_DFSButton.Text = "Compute DFS";
            this.m_DFSButton.UseVisualStyleBackColor = true;
            this.m_DFSButton.Click += new System.EventHandler(this.m_DFSButton_Click);
            // 
            // m_BFSButton
            // 
            this.m_BFSButton.Location = new System.Drawing.Point(16, 393);
            this.m_BFSButton.Name = "m_BFSButton";
            this.m_BFSButton.Size = new System.Drawing.Size(115, 53);
            this.m_BFSButton.TabIndex = 6;
            this.m_BFSButton.Text = "Compute BFS (Overflow)";
            this.m_BFSButton.UseVisualStyleBackColor = true;
            this.m_BFSButton.Click += new System.EventHandler(this.m_BFSButton_Click);
            // 
            // m_AStarButton
            // 
            this.m_AStarButton.Location = new System.Drawing.Point(16, 266);
            this.m_AStarButton.Name = "m_AStarButton";
            this.m_AStarButton.Size = new System.Drawing.Size(115, 53);
            this.m_AStarButton.TabIndex = 7;
            this.m_AStarButton.Text = "Compute AStar";
            this.m_AStarButton.UseVisualStyleBackColor = true;
            this.m_AStarButton.Click += new System.EventHandler(this.m_AStarButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(789, 473);
            this.Controls.Add(this.m_AStarButton);
            this.Controls.Add(this.m_BFSButton);
            this.Controls.Add(this.m_DFSButton);
            this.Controls.Add(this.m_label);
            this.Controls.Add(this.m_posY);
            this.Controls.Add(this.m_posX);
            this.Controls.Add(this.m_placeButton);
            this.Controls.Add(this.m_panel);
            this.KeyPreview = true;
            this.Name = "Form1";
            this.Text = "Path Finding Demo";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Form1_KeyPress);
            ((System.ComponentModel.ISupportInitialize)(this.m_posX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_posY)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel m_panel;
        private System.Windows.Forms.Button m_placeButton;
        private System.Windows.Forms.NumericUpDown m_posX;
        private System.Windows.Forms.NumericUpDown m_posY;
        private System.Windows.Forms.Label m_label;
        private System.Windows.Forms.Button m_DFSButton;
        private System.Windows.Forms.Button m_BFSButton;
        private System.Windows.Forms.Button m_AStarButton;
    }
}

