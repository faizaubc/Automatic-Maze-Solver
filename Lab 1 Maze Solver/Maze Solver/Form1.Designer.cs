namespace Maze_Solver
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
            this.buttonSolveColor = new System.Windows.Forms.Button();
            this.buttonLoadMaze = new System.Windows.Forms.Button();
            this.buttonDeadColor = new System.Windows.Forms.Button();
            this.buttonSolve = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDownSpeed = new System.Windows.Forms.NumericUpDown();
            this.listBoxData = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSpeed)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonSolveColor
            // 
            this.buttonSolveColor.BackColor = System.Drawing.Color.MediumOrchid;
            this.buttonSolveColor.Location = new System.Drawing.Point(35, 77);
            this.buttonSolveColor.Name = "buttonSolveColor";
            this.buttonSolveColor.Size = new System.Drawing.Size(113, 32);
            this.buttonSolveColor.TabIndex = 0;
            this.buttonSolveColor.Text = "Solve Color";
            this.buttonSolveColor.UseVisualStyleBackColor = false;
            this.buttonSolveColor.Click += new System.EventHandler(this.buttonSolveColor_Click);
            // 
            // buttonLoadMaze
            // 
            this.buttonLoadMaze.Location = new System.Drawing.Point(26, 26);
            this.buttonLoadMaze.Name = "buttonLoadMaze";
            this.buttonLoadMaze.Size = new System.Drawing.Size(348, 33);
            this.buttonLoadMaze.TabIndex = 1;
            this.buttonLoadMaze.Text = "Load Maze";
            this.buttonLoadMaze.UseVisualStyleBackColor = true;
            this.buttonLoadMaze.Click += new System.EventHandler(this.buttonLoadMaze_Click);
            // 
            // buttonDeadColor
            // 
            this.buttonDeadColor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.buttonDeadColor.Location = new System.Drawing.Point(35, 115);
            this.buttonDeadColor.Name = "buttonDeadColor";
            this.buttonDeadColor.Size = new System.Drawing.Size(113, 32);
            this.buttonDeadColor.TabIndex = 2;
            this.buttonDeadColor.Text = "Dead Color";
            this.buttonDeadColor.UseVisualStyleBackColor = false;
            this.buttonDeadColor.Click += new System.EventHandler(this.buttonDeadColor_Click);
            // 
            // buttonSolve
            // 
            this.buttonSolve.Location = new System.Drawing.Point(188, 77);
            this.buttonSolve.Name = "buttonSolve";
            this.buttonSolve.Size = new System.Drawing.Size(186, 98);
            this.buttonSolve.TabIndex = 3;
            this.buttonSolve.Text = "Solve";
            this.buttonSolve.UseVisualStyleBackColor = true;
            this.buttonSolve.Click += new System.EventHandler(this.buttonSolve_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 159);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 20);
            this.label1.TabIndex = 4;
            this.label1.Text = "Speed:";
            // 
            // numericUpDownSpeed
            // 
            this.numericUpDownSpeed.Location = new System.Drawing.Point(94, 153);
            this.numericUpDownSpeed.Name = "numericUpDownSpeed";
            this.numericUpDownSpeed.Size = new System.Drawing.Size(67, 26);
            this.numericUpDownSpeed.TabIndex = 5;
            // 
            // listBoxData
            // 
            this.listBoxData.FormattingEnabled = true;
            this.listBoxData.ItemHeight = 20;
            this.listBoxData.Location = new System.Drawing.Point(14, 245);
            this.listBoxData.Name = "listBoxData";
            this.listBoxData.Size = new System.Drawing.Size(394, 224);
            this.listBoxData.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 204);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 20);
            this.label2.TabIndex = 7;
            this.label2.Text = "label2";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(416, 471);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.listBoxData);
            this.Controls.Add(this.numericUpDownSpeed);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonSolve);
            this.Controls.Add(this.buttonDeadColor);
            this.Controls.Add(this.buttonLoadMaze);
            this.Controls.Add(this.buttonSolveColor);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSpeed)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonSolveColor;
        private System.Windows.Forms.Button buttonLoadMaze;
        private System.Windows.Forms.Button buttonDeadColor;
        private System.Windows.Forms.Button buttonSolve;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDownSpeed;
        private System.Windows.Forms.ListBox listBoxData;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ColorDialog colorDialog1;
    }
}

