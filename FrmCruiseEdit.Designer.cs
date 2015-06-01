namespace AlarmMapClient
{
    partial class FrmCruiseEdit
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cruise_textBox = new System.Windows.Forms.TextBox();
            this.preset_comboBox = new System.Windows.Forms.ComboBox();
            this.addpreset_button = new System.Windows.Forms.Button();
            this.delpreset_button = new System.Windows.Forms.Button();
            this.removecruise_button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(64, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "巡航线路:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(76, 95);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "预置点:";
            // 
            // cruise_textBox
            // 
            this.cruise_textBox.Location = new System.Drawing.Point(129, 47);
            this.cruise_textBox.Name = "cruise_textBox";
            this.cruise_textBox.ReadOnly = true;
            this.cruise_textBox.Size = new System.Drawing.Size(100, 21);
            this.cruise_textBox.TabIndex = 2;
            // 
            // preset_comboBox
            // 
            this.preset_comboBox.FormattingEnabled = true;
            this.preset_comboBox.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24",
            "25",
            "26",
            "27",
            "28",
            "29",
            "30",
            "31",
            "32"});
            this.preset_comboBox.Location = new System.Drawing.Point(129, 95);
            this.preset_comboBox.Name = "preset_comboBox";
            this.preset_comboBox.Size = new System.Drawing.Size(100, 20);
            this.preset_comboBox.TabIndex = 3;
            // 
            // addpreset_button
            // 
            this.addpreset_button.Location = new System.Drawing.Point(12, 157);
            this.addpreset_button.Name = "addpreset_button";
            this.addpreset_button.Size = new System.Drawing.Size(75, 23);
            this.addpreset_button.TabIndex = 4;
            this.addpreset_button.Text = "增加预置点";
            this.addpreset_button.UseVisualStyleBackColor = true;
            this.addpreset_button.MouseDown += new System.Windows.Forms.MouseEventHandler(this.addpreset_button_MouseDown);
            this.addpreset_button.MouseUp += new System.Windows.Forms.MouseEventHandler(this.addpreset_button_MouseUp);
            // 
            // delpreset_button
            // 
            this.delpreset_button.Location = new System.Drawing.Point(129, 157);
            this.delpreset_button.Name = "delpreset_button";
            this.delpreset_button.Size = new System.Drawing.Size(75, 23);
            this.delpreset_button.TabIndex = 5;
            this.delpreset_button.Text = "删除预置点";
            this.delpreset_button.UseVisualStyleBackColor = true;
            this.delpreset_button.MouseDown += new System.Windows.Forms.MouseEventHandler(this.delpreset_button_MouseDown);
            this.delpreset_button.MouseUp += new System.Windows.Forms.MouseEventHandler(this.delpreset_button_MouseUp);
            // 
            // removecruise_button
            // 
            this.removecruise_button.Location = new System.Drawing.Point(245, 157);
            this.removecruise_button.Name = "removecruise_button";
            this.removecruise_button.Size = new System.Drawing.Size(75, 23);
            this.removecruise_button.TabIndex = 6;
            this.removecruise_button.Text = "清除巡航线路";
            this.removecruise_button.UseVisualStyleBackColor = true;
            this.removecruise_button.MouseDown += new System.Windows.Forms.MouseEventHandler(this.removecruise_button_MouseDown);
            this.removecruise_button.MouseUp += new System.Windows.Forms.MouseEventHandler(this.removecruise_button_MouseUp);
            // 
            // FrmCruiseEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(354, 212);
            this.Controls.Add(this.removecruise_button);
            this.Controls.Add(this.delpreset_button);
            this.Controls.Add(this.addpreset_button);
            this.Controls.Add(this.preset_comboBox);
            this.Controls.Add(this.cruise_textBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.Name = "FrmCruiseEdit";
            this.Text = "间点巡航";
            this.Load += new System.EventHandler(this.FrmCruiseEdit_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox cruise_textBox;
        private System.Windows.Forms.ComboBox preset_comboBox;
        private System.Windows.Forms.Button addpreset_button;
        private System.Windows.Forms.Button delpreset_button;
        private System.Windows.Forms.Button removecruise_button;
    }
}