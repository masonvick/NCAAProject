namespace NCAAProject
{
    partial class CoachForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.dropDownList1 = new System.Windows.Forms.ComboBox();
            this.dropDownList2 = new System.Windows.Forms.ComboBox();
            this.compareButton = new System.Windows.Forms.Button();
            this.coachRichTextBox2 = new System.Windows.Forms.RichTextBox();
            this.coachRichTextBox1 = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // dropDownList1
            // 
            this.dropDownList1.Location = new System.Drawing.Point(10, 10);
            this.dropDownList1.Name = "dropDownList1";
            this.dropDownList1.Size = new System.Drawing.Size(150, 23);
            this.dropDownList1.TabIndex = 0;
            // 
            // dropDownList2
            // 
            this.dropDownList2.Location = new System.Drawing.Point(258, 10);
            this.dropDownList2.Name = "dropDownList2";
            this.dropDownList2.Size = new System.Drawing.Size(150, 23);
            this.dropDownList2.TabIndex = 1;
            // 
            // compareButton
            // 
            this.compareButton.Location = new System.Drawing.Point(166, 13);
            this.compareButton.Name = "compareButton";
            this.compareButton.Size = new System.Drawing.Size(75, 20);
            this.compareButton.TabIndex = 2;
            this.compareButton.Text = "Compare";
            this.compareButton.Click += new System.EventHandler(this.compareButton_Click);
            // 
            // coachRichTextBox2
            // 
            this.coachRichTextBox2.Location = new System.Drawing.Point(216, 65);
            this.coachRichTextBox2.Name = "coachRichTextBox2";
            this.coachRichTextBox2.Size = new System.Drawing.Size(192, 223);
            this.coachRichTextBox2.TabIndex = 3;
            this.coachRichTextBox2.Text = "";
            // 
            // coachRichTextBox1
            // 
            this.coachRichTextBox1.Location = new System.Drawing.Point(10, 65);
            this.coachRichTextBox1.Name = "coachRichTextBox1";
            this.coachRichTextBox1.Size = new System.Drawing.Size(192, 223);
            this.coachRichTextBox1.TabIndex = 4;
            this.coachRichTextBox1.Text = "";
            // 
            // CoachForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(420, 300);
            this.Controls.Add(this.coachRichTextBox1);
            this.Controls.Add(this.coachRichTextBox2);
            this.Controls.Add(this.dropDownList1);
            this.Controls.Add(this.dropDownList2);
            this.Controls.Add(this.compareButton);
            this.Name = "CoachForm";
            this.Text = "Coach Comparison Tool";
            this.ResumeLayout(false);

        }


        private System.Windows.Forms.ComboBox dropDownList1;
        private System.Windows.Forms.ComboBox dropDownList2;
        private System.Windows.Forms.Button compareButton;

        #endregion

        private RichTextBox coachRichTextBox2;
        private RichTextBox coachRichTextBox1;
    }
}