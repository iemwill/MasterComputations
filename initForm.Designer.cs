namespace MasterComputations
{
    partial class initForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(initForm));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.pingOB = new System.Windows.Forms.Button();
            this.dataV = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.pingOB);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dataV);
            this.splitContainer1.Size = new System.Drawing.Size(510, 221);
            this.splitContainer1.SplitterDistance = 255;
            this.splitContainer1.TabIndex = 0;
            // 
            // pingOB
            // 
            this.pingOB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pingOB.Location = new System.Drawing.Point(0, 0);
            this.pingOB.Name = "pingOB";
            this.pingOB.Size = new System.Drawing.Size(255, 221);
            this.pingOB.TabIndex = 0;
            this.pingOB.Text = "ping Order Book";
            this.pingOB.UseVisualStyleBackColor = true;
            this.pingOB.Click += new System.EventHandler(this.pingOB_Click);
            // 
            // dataV
            // 
            this.dataV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataV.Location = new System.Drawing.Point(0, 0);
            this.dataV.Name = "dataV";
            this.dataV.Size = new System.Drawing.Size(251, 221);
            this.dataV.TabIndex = 0;
            this.dataV.Text = "data Visualization";
            this.dataV.UseVisualStyleBackColor = true;
            // 
            // initForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(510, 221);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "initForm";
            this.Text = "Choose your settings";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button pingOB;
        private System.Windows.Forms.Button dataV;
    }
}