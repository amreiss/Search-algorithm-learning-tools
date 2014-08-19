namespace Pacman
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.btnMultiDotBFS = new System.Windows.Forms.Button();
            this.btnMultiDotDFS = new System.Windows.Forms.Button();
            this.btnMultiDotGreedy = new System.Windows.Forms.Button();
            this.btnMultiDotAStar = new System.Windows.Forms.Button();
            this.btnBugtrap = new System.Windows.Forms.Button();
            this.btnRandomizedBugtrap = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btn8Puzzle = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnMultiDotBFS
            // 
            this.btnMultiDotBFS.Location = new System.Drawing.Point(16, 17);
            this.btnMultiDotBFS.Name = "btnMultiDotBFS";
            this.btnMultiDotBFS.Size = new System.Drawing.Size(174, 32);
            this.btnMultiDotBFS.TabIndex = 7;
            this.btnMultiDotBFS.Text = "Multi Dot BFS";
            this.btnMultiDotBFS.UseVisualStyleBackColor = true;
            this.btnMultiDotBFS.Click += new System.EventHandler(this.btnMultiDotBFS_Click);
            // 
            // btnMultiDotDFS
            // 
            this.btnMultiDotDFS.Location = new System.Drawing.Point(16, 55);
            this.btnMultiDotDFS.Name = "btnMultiDotDFS";
            this.btnMultiDotDFS.Size = new System.Drawing.Size(174, 32);
            this.btnMultiDotDFS.TabIndex = 8;
            this.btnMultiDotDFS.Text = "Multi Dot DFS";
            this.btnMultiDotDFS.UseVisualStyleBackColor = true;
            this.btnMultiDotDFS.Click += new System.EventHandler(this.btnMultiDotDFS_Click);
            // 
            // btnMultiDotGreedy
            // 
            this.btnMultiDotGreedy.Location = new System.Drawing.Point(16, 94);
            this.btnMultiDotGreedy.Name = "btnMultiDotGreedy";
            this.btnMultiDotGreedy.Size = new System.Drawing.Size(174, 32);
            this.btnMultiDotGreedy.TabIndex = 9;
            this.btnMultiDotGreedy.Text = "Multi Dot Greedy";
            this.btnMultiDotGreedy.UseVisualStyleBackColor = true;
            this.btnMultiDotGreedy.Click += new System.EventHandler(this.btnMultiDotGreedy_Click);
            // 
            // btnMultiDotAStar
            // 
            this.btnMultiDotAStar.Location = new System.Drawing.Point(16, 132);
            this.btnMultiDotAStar.Name = "btnMultiDotAStar";
            this.btnMultiDotAStar.Size = new System.Drawing.Size(174, 32);
            this.btnMultiDotAStar.TabIndex = 10;
            this.btnMultiDotAStar.Text = "Multi Dot A* Search";
            this.btnMultiDotAStar.UseVisualStyleBackColor = true;
            this.btnMultiDotAStar.Click += new System.EventHandler(this.btnMultiDotAStar_Click);
            // 
            // btnBugtrap
            // 
            this.btnBugtrap.Location = new System.Drawing.Point(16, 171);
            this.btnBugtrap.Name = "btnBugtrap";
            this.btnBugtrap.Size = new System.Drawing.Size(174, 32);
            this.btnBugtrap.TabIndex = 11;
            this.btnBugtrap.Text = "BugTrap Search";
            this.btnBugtrap.UseVisualStyleBackColor = true;
            this.btnBugtrap.Click += new System.EventHandler(this.btnBugtrap_Click);
            // 
            // btnRandomizedBugtrap
            // 
            this.btnRandomizedBugtrap.Location = new System.Drawing.Point(16, 209);
            this.btnRandomizedBugtrap.Name = "btnRandomizedBugtrap";
            this.btnRandomizedBugtrap.Size = new System.Drawing.Size(174, 32);
            this.btnRandomizedBugtrap.TabIndex = 12;
            this.btnRandomizedBugtrap.Text = "Randomized BugTrap Search";
            this.btnRandomizedBugtrap.UseVisualStyleBackColor = true;
            this.btnRandomizedBugtrap.Click += new System.EventHandler(this.btnRandomizedBugtrap_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(16, 249);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(174, 35);
            this.button1.TabIndex = 13;
            this.button1.Text = "Bugtrap Part2";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btn8Puzzle
            // 
            this.btn8Puzzle.Location = new System.Drawing.Point(16, 294);
            this.btn8Puzzle.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btn8Puzzle.Name = "btn8Puzzle";
            this.btn8Puzzle.Size = new System.Drawing.Size(174, 35);
            this.btn8Puzzle.TabIndex = 14;
            this.btn8Puzzle.Text = "8 puzzle";
            this.btn8Puzzle.UseVisualStyleBackColor = true;
            this.btn8Puzzle.Click += new System.EventHandler(this.btn8Puzzle_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(218, 339);
            this.Controls.Add(this.btn8Puzzle);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnRandomizedBugtrap);
            this.Controls.Add(this.btnBugtrap);
            this.Controls.Add(this.btnMultiDotAStar);
            this.Controls.Add(this.btnMultiDotGreedy);
            this.Controls.Add(this.btnMultiDotDFS);
            this.Controls.Add(this.btnMultiDotBFS);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "MainForm";
            this.Text = "Pacman";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnMultiDotBFS;
        private System.Windows.Forms.Button btnMultiDotDFS;
        private System.Windows.Forms.Button btnMultiDotGreedy;
        private System.Windows.Forms.Button btnMultiDotAStar;
        private System.Windows.Forms.Button btnBugtrap;
        private System.Windows.Forms.Button btnRandomizedBugtrap;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btn8Puzzle;
    }
}

