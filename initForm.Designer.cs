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
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.pingBookData = new System.Windows.Forms.Button();
            this.computations = new System.Windows.Forms.Button();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.dataVs = new System.Windows.Forms.Button();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.apiKEY = new System.Windows.Forms.Button();
            this.updateHistoricalData = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).BeginInit();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
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
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer3);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(510, 221);
            this.splitContainer1.SplitterDistance = 255;
            this.splitContainer1.TabIndex = 0;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.pingBookData);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.computations);
            this.splitContainer3.Size = new System.Drawing.Size(255, 221);
            this.splitContainer3.SplitterDistance = 109;
            this.splitContainer3.TabIndex = 0;
            // 
            // pingBookData
            // 
            this.pingBookData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pingBookData.Location = new System.Drawing.Point(0, 0);
            this.pingBookData.Name = "pingBookData";
            this.pingBookData.Size = new System.Drawing.Size(255, 109);
            this.pingBookData.TabIndex = 1;
            this.pingBookData.Text = "ping Order Book Data";
            this.pingBookData.UseVisualStyleBackColor = true;
            this.pingBookData.Click += new System.EventHandler(this.pingData_Click);
            // 
            // computations
            // 
            this.computations.Dock = System.Windows.Forms.DockStyle.Fill;
            this.computations.Location = new System.Drawing.Point(0, 0);
            this.computations.Name = "computations";
            this.computations.Size = new System.Drawing.Size(255, 108);
            this.computations.TabIndex = 2;
            this.computations.Text = "computations";
            this.computations.UseVisualStyleBackColor = true;
            this.computations.Click += new System.EventHandler(this.computations_Click);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.dataVs);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.splitContainer4);
            this.splitContainer2.Size = new System.Drawing.Size(251, 221);
            this.splitContainer2.SplitterDistance = 109;
            this.splitContainer2.TabIndex = 0;
            // 
            // dataVs
            // 
            this.dataVs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataVs.Location = new System.Drawing.Point(0, 0);
            this.dataVs.Name = "dataVs";
            this.dataVs.Size = new System.Drawing.Size(251, 109);
            this.dataVs.TabIndex = 0;
            this.dataVs.Text = "data Vs";
            this.dataVs.UseVisualStyleBackColor = true;
            this.dataVs.Click += new System.EventHandler(this.dataVs_Click);
            // 
            // splitContainer4
            // 
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.Location = new System.Drawing.Point(0, 0);
            this.splitContainer4.Name = "splitContainer4";
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.apiKEY);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.updateHistoricalData);
            this.splitContainer4.Size = new System.Drawing.Size(251, 108);
            this.splitContainer4.SplitterDistance = 120;
            this.splitContainer4.TabIndex = 4;
            // 
            // apiKEY
            // 
            this.apiKEY.Dock = System.Windows.Forms.DockStyle.Fill;
            this.apiKEY.Location = new System.Drawing.Point(0, 0);
            this.apiKEY.Name = "apiKEY";
            this.apiKEY.Size = new System.Drawing.Size(120, 108);
            this.apiKEY.TabIndex = 4;
            this.apiKEY.Text = "add API KEY for TRADING";
            this.apiKEY.UseVisualStyleBackColor = true;
            // 
            // updateHistoricalData
            // 
            this.updateHistoricalData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.updateHistoricalData.Location = new System.Drawing.Point(0, 0);
            this.updateHistoricalData.Name = "updateHistoricalData";
            this.updateHistoricalData.Size = new System.Drawing.Size(127, 108);
            this.updateHistoricalData.TabIndex = 0;
            this.updateHistoricalData.Text = "update historical data";
            this.updateHistoricalData.UseVisualStyleBackColor = true;
            this.updateHistoricalData.Click += new System.EventHandler(this.updateHistoricalData_Click);
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
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).EndInit();
            this.splitContainer4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.Button pingBookData;
        private System.Windows.Forms.Button computations;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Button dataVs;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private System.Windows.Forms.Button apiKEY;
        private System.Windows.Forms.Button updateHistoricalData;
    }
}