namespace project_vniia
{
    partial class Form_calibr
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
            this.butSaveFile = new System.Windows.Forms.Button();
            this.fldContent = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.butSelectFile = new System.Windows.Forms.Button();
            this.fldFilePath = new System.Windows.Forms.TextBox();
            this.butOpenFile = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // butSaveFile
            // 
            this.butSaveFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.butSaveFile.Location = new System.Drawing.Point(461, 410);
            this.butSaveFile.Name = "butSaveFile";
            this.butSaveFile.Size = new System.Drawing.Size(138, 28);
            this.butSaveFile.TabIndex = 0;
            this.butSaveFile.Text = "Записать в файл";
            this.butSaveFile.UseVisualStyleBackColor = true;
            this.butSaveFile.Click += new System.EventHandler(this.button_calibr_Click);
            // 
            // fldContent
            // 
            this.fldContent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fldContent.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fldContent.Location = new System.Drawing.Point(12, 49);
            this.fldContent.Multiline = true;
            this.fldContent.Name = "fldContent";
            this.fldContent.Size = new System.Drawing.Size(587, 341);
            this.fldContent.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 19);
            this.label1.TabIndex = 2;
            this.label1.Text = "Введите :";
            // 
            // butSelectFile
            // 
            this.butSelectFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.butSelectFile.Location = new System.Drawing.Point(12, 414);
            this.butSelectFile.Name = "butSelectFile";
            this.butSelectFile.Size = new System.Drawing.Size(80, 23);
            this.butSelectFile.TabIndex = 3;
            this.butSelectFile.Text = "open";
            this.butSelectFile.UseVisualStyleBackColor = true;
            this.butSelectFile.Click += new System.EventHandler(this.butSelectFile_Click_1);
            // 
            // fldFilePath
            // 
            this.fldFilePath.Location = new System.Drawing.Point(99, 13);
            this.fldFilePath.Name = "fldFilePath";
            this.fldFilePath.Size = new System.Drawing.Size(386, 20);
            this.fldFilePath.TabIndex = 4;
            // 
            // butOpenFile
            // 
            this.butOpenFile.Location = new System.Drawing.Point(509, 9);
            this.butOpenFile.Name = "butOpenFile";
            this.butOpenFile.Size = new System.Drawing.Size(75, 23);
            this.butOpenFile.TabIndex = 5;
            this.butOpenFile.Text = "button1";
            this.butOpenFile.UseVisualStyleBackColor = true;
            this.butOpenFile.Click += new System.EventHandler(this.butOpenFile_Click_1);
            // 
            // Form_calibr
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(611, 450);
            this.Controls.Add(this.butOpenFile);
            this.Controls.Add(this.fldFilePath);
            this.Controls.Add(this.butSelectFile);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.fldContent);
            this.Controls.Add(this.butSaveFile);
            this.Name = "Form_calibr";
            this.Text = "Form_calibr";
            this.Load += new System.EventHandler(this.Form_calibr_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button butSaveFile;
        private System.Windows.Forms.TextBox fldContent;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button butSelectFile;
        private System.Windows.Forms.TextBox fldFilePath;
        private System.Windows.Forms.Button butOpenFile;
    }
}