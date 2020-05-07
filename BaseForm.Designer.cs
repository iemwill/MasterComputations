namespace MasterComputations
{
    partial class BaseForm
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.parseCSV = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.publicAPIderibit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // parseCSV
            // 
            this.parseCSV.Location = new System.Drawing.Point(12, 12);
            this.parseCSV.Name = "parseCSV";
            this.parseCSV.Size = new System.Drawing.Size(129, 33);
            this.parseCSV.TabIndex = 0;
            this.parseCSV.Text = "parse csv";
            this.parseCSV.UseVisualStyleBackColor = true;
            this.parseCSV.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(88, 41);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(8, 8);
            this.button2.TabIndex = 1;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // publicAPIderibit
            // 
            this.publicAPIderibit.Location = new System.Drawing.Point(12, 51);
            this.publicAPIderibit.Name = "publicAPIderibit";
            this.publicAPIderibit.Size = new System.Drawing.Size(129, 33);
            this.publicAPIderibit.TabIndex = 2;
            this.publicAPIderibit.Text = "use deribit public Data";
            this.publicAPIderibit.UseVisualStyleBackColor = true;
            this.publicAPIderibit.Click += new System.EventHandler(this.button3_Click);
            // 
            // BaseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(898, 370);
            this.Controls.Add(this.publicAPIderibit);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.parseCSV);
            this.Name = "BaseForm";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button parseCSV;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button publicAPIderibit;
    }
}

