namespace ArabicVerbBuilder
{
    partial class MainWin
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
            this.OutputBox = new System.Windows.Forms.TextBox();
            this.BuildButton = new System.Windows.Forms.Button();
            this.RootIn = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.FormIn = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.MoodIn = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.PronounIn = new System.Windows.Forms.ComboBox();
            this.VowelIn = new System.Windows.Forms.ComboBox();
            this.passiveCheckBox = new System.Windows.Forms.CheckBox();
            this.pastVowelKasra_CheckBox = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.CaseIn = new System.Windows.Forms.ComboBox();
            this.caseLabel = new System.Windows.Forms.Label();
            this.knownCheckBox = new System.Windows.Forms.CheckBox();
            this.repotNutton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.FormIn)).BeginInit();
            this.SuspendLayout();
            // 
            // OutputBox
            // 
            this.OutputBox.Font = new System.Drawing.Font("Traditional Arabic", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.OutputBox.Location = new System.Drawing.Point(16, 374);
            this.OutputBox.Name = "OutputBox";
            this.OutputBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.OutputBox.Size = new System.Drawing.Size(250, 103);
            this.OutputBox.TabIndex = 0;
            this.OutputBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // BuildButton
            // 
            this.BuildButton.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.BuildButton.Location = new System.Drawing.Point(16, 314);
            this.BuildButton.Name = "BuildButton";
            this.BuildButton.Size = new System.Drawing.Size(250, 54);
            this.BuildButton.TabIndex = 1;
            this.BuildButton.Text = "Build!";
            this.BuildButton.UseVisualStyleBackColor = true;
            this.BuildButton.Click += new System.EventHandler(this.BuildButton_Click);
            // 
            // RootIn
            // 
            this.RootIn.Font = new System.Drawing.Font("Times New Roman", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.RootIn.Location = new System.Drawing.Point(166, 66);
            this.RootIn.Name = "RootIn";
            this.RootIn.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RootIn.Size = new System.Drawing.Size(96, 39);
            this.RootIn.TabIndex = 2;
            this.RootIn.Text = "كتب";
            this.RootIn.TextChanged += new System.EventHandler(this.RootIn_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label1.Location = new System.Drawing.Point(12, 75);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 23);
            this.label1.TabIndex = 3;
            this.label1.Text = "Root:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label2.Location = new System.Drawing.Point(12, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 23);
            this.label2.TabIndex = 5;
            this.label2.Text = "Form:";
            // 
            // FormIn
            // 
            this.FormIn.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.FormIn.Location = new System.Drawing.Point(106, 29);
            this.FormIn.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.FormIn.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.FormIn.Name = "FormIn";
            this.FormIn.Size = new System.Drawing.Size(55, 32);
            this.FormIn.TabIndex = 6;
            this.FormIn.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.FormIn.ValueChanged += new System.EventHandler(this.FormIn_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Cursor = System.Windows.Forms.Cursors.Default;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label3.Location = new System.Drawing.Point(12, 144);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 23);
            this.label3.TabIndex = 7;
            this.label3.Text = "Mood:";
            // 
            // MoodIn
            // 
            this.MoodIn.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.MoodIn.FormattingEnabled = true;
            this.MoodIn.Location = new System.Drawing.Point(106, 141);
            this.MoodIn.Name = "MoodIn";
            this.MoodIn.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.MoodIn.Size = new System.Drawing.Size(156, 31);
            this.MoodIn.TabIndex = 8;
            this.MoodIn.SelectedIndexChanged += new System.EventHandler(this.MoodIn_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Cursor = System.Windows.Forms.Cursors.Default;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label4.Location = new System.Drawing.Point(12, 210);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 23);
            this.label4.TabIndex = 9;
            this.label4.Text = "Pronoun:";
            // 
            // PronounIn
            // 
            this.PronounIn.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.PronounIn.FormattingEnabled = true;
            this.PronounIn.Location = new System.Drawing.Point(106, 207);
            this.PronounIn.Name = "PronounIn";
            this.PronounIn.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.PronounIn.Size = new System.Drawing.Size(156, 31);
            this.PronounIn.TabIndex = 10;
            this.PronounIn.SelectedIndexChanged += new System.EventHandler(this.PronounIn_SelectedIndexChanged);
            // 
            // VowelIn
            // 
            this.VowelIn.Cursor = System.Windows.Forms.Cursors.Default;
            this.VowelIn.Font = new System.Drawing.Font("Times New Roman", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.VowelIn.FormattingEnabled = true;
            this.VowelIn.Location = new System.Drawing.Point(106, 66);
            this.VowelIn.Name = "VowelIn";
            this.VowelIn.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.VowelIn.Size = new System.Drawing.Size(54, 39);
            this.VowelIn.TabIndex = 11;
            this.VowelIn.Text = "َ";
            // 
            // passiveCheckBox
            // 
            this.passiveCheckBox.AutoSize = true;
            this.passiveCheckBox.Cursor = System.Windows.Forms.Cursors.Default;
            this.passiveCheckBox.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.passiveCheckBox.Location = new System.Drawing.Point(106, 178);
            this.passiveCheckBox.Name = "passiveCheckBox";
            this.passiveCheckBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.passiveCheckBox.Size = new System.Drawing.Size(85, 25);
            this.passiveCheckBox.TabIndex = 12;
            this.passiveCheckBox.Text = "Passive";
            this.passiveCheckBox.UseVisualStyleBackColor = true;
            // 
            // pastVowelKasra_CheckBox
            // 
            this.pastVowelKasra_CheckBox.AutoSize = true;
            this.pastVowelKasra_CheckBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pastVowelKasra_CheckBox.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.pastVowelKasra_CheckBox.Location = new System.Drawing.Point(22, 112);
            this.pastVowelKasra_CheckBox.Name = "pastVowelKasra_CheckBox";
            this.pastVowelKasra_CheckBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.pastVowelKasra_CheckBox.Size = new System.Drawing.Size(232, 23);
            this.pastVowelKasra_CheckBox.TabIndex = 13;
            this.pastVowelKasra_CheckBox.Text = "حرف مصوّت ع الفعل في الماضي كسرة";
            this.pastVowelKasra_CheckBox.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label5.Location = new System.Drawing.Point(12, 485);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(201, 133);
            this.label5.TabIndex = 14;
            this.label5.Text = "© Copyright Shachar Har-Shuv\r\nArabic Verb Builder Beta 4.0 \r\nComplete Version\r\n\r\n" +
    "\r\n\r\n\r\n";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // CaseIn
            // 
            this.CaseIn.Font = new System.Drawing.Font("Times New Roman", 16F);
            this.CaseIn.FormattingEnabled = true;
            this.CaseIn.Location = new System.Drawing.Point(74, 246);
            this.CaseIn.Name = "CaseIn";
            this.CaseIn.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.CaseIn.Size = new System.Drawing.Size(188, 31);
            this.CaseIn.TabIndex = 16;
            // 
            // caseLabel
            // 
            this.caseLabel.AutoSize = true;
            this.caseLabel.Cursor = System.Windows.Forms.Cursors.Default;
            this.caseLabel.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.caseLabel.Location = new System.Drawing.Point(12, 249);
            this.caseLabel.Name = "caseLabel";
            this.caseLabel.Size = new System.Drawing.Size(56, 23);
            this.caseLabel.TabIndex = 15;
            this.caseLabel.Text = "Case:";
            this.caseLabel.Click += new System.EventHandler(this.label6_Click);
            // 
            // knownCheckBox
            // 
            this.knownCheckBox.AutoSize = true;
            this.knownCheckBox.Cursor = System.Windows.Forms.Cursors.Default;
            this.knownCheckBox.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.knownCheckBox.Location = new System.Drawing.Point(74, 283);
            this.knownCheckBox.Name = "knownCheckBox";
            this.knownCheckBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.knownCheckBox.Size = new System.Drawing.Size(138, 25);
            this.knownCheckBox.TabIndex = 17;
            this.knownCheckBox.Text = "Known (\"the\")";
            this.knownCheckBox.UseVisualStyleBackColor = true;
            // 
            // repotNutton
            // 
            this.repotNutton.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.repotNutton.Location = new System.Drawing.Point(16, 548);
            this.repotNutton.Name = "repotNutton";
            this.repotNutton.Size = new System.Drawing.Size(250, 23);
            this.repotNutton.TabIndex = 18;
            this.repotNutton.Text = "REPORT A MISTAKE";
            this.repotNutton.UseVisualStyleBackColor = true;
            this.repotNutton.Click += new System.EventHandler(this.reportButton_Click);
            // 
            // MainWin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(274, 582);
            this.Controls.Add(this.repotNutton);
            this.Controls.Add(this.knownCheckBox);
            this.Controls.Add(this.CaseIn);
            this.Controls.Add(this.caseLabel);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.pastVowelKasra_CheckBox);
            this.Controls.Add(this.passiveCheckBox);
            this.Controls.Add(this.VowelIn);
            this.Controls.Add(this.PronounIn);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.MoodIn);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.FormIn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.RootIn);
            this.Controls.Add(this.BuildButton);
            this.Controls.Add(this.OutputBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainWin";
            this.Text = "Arabic Verb Builder";
            this.Load += new System.EventHandler(this.MainWin_Load);
            ((System.ComponentModel.ISupportInitialize)(this.FormIn)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox OutputBox;
        private System.Windows.Forms.Button BuildButton;
        private System.Windows.Forms.TextBox RootIn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown FormIn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox MoodIn;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox PronounIn;
        private System.Windows.Forms.ComboBox VowelIn;
        private System.Windows.Forms.CheckBox passiveCheckBox;
        private System.Windows.Forms.CheckBox pastVowelKasra_CheckBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox CaseIn;
        private System.Windows.Forms.Label caseLabel;
        private System.Windows.Forms.CheckBox knownCheckBox;
        private System.Windows.Forms.Button repotNutton;



    }
}

