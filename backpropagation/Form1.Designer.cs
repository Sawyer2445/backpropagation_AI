namespace backpropagation
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
            this.components = new System.ComponentModel.Container();
            this.button3 = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.cb_drink = new System.Windows.Forms.CheckBox();
            this.cb_rain = new System.Windows.Forms.CheckBox();
            this.cb_friend = new System.Windows.Forms.CheckBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(181, 158);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(235, 23);
            this.button3.TabIndex = 4;
            this.button3.Text = "Сканировать";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(181, 6);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(235, 146);
            this.richTextBox1.TabIndex = 6;
            this.richTextBox1.Text = "";
            this.richTextBox1.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            // 
            // cb_drink
            // 
            this.cb_drink.AutoSize = true;
            this.cb_drink.Location = new System.Drawing.Point(12, 68);
            this.cb_drink.Name = "cb_drink";
            this.cb_drink.Size = new System.Drawing.Size(99, 17);
            this.cb_drink.TabIndex = 10;
            this.cb_drink.Text = "Там наливают";
            this.cb_drink.UseVisualStyleBackColor = true;
            // 
            // cb_rain
            // 
            this.cb_rain.AutoSize = true;
            this.cb_rain.Location = new System.Drawing.Point(12, 91);
            this.cb_rain.Name = "cb_rain";
            this.cb_rain.Size = new System.Drawing.Size(110, 17);
            this.cb_rain.TabIndex = 11;
            this.cb_rain.Text = "На улице дождь ";
            this.cb_rain.UseVisualStyleBackColor = true;
            // 
            // cb_friend
            // 
            this.cb_friend.AutoSize = true;
            this.cb_friend.Location = new System.Drawing.Point(12, 114);
            this.cb_friend.Name = "cb_friend";
            this.cb_friend.Size = new System.Drawing.Size(72, 17);
            this.cb_friend.TabIndex = 12;
            this.cb_friend.Text = "Там друг";
            this.cb_friend.UseVisualStyleBackColor = true;
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(166, 39);
            this.label1.TabIndex = 13;
            this.label1.Text = "ИИ ответит на вопрос пойти ли\r\n в гости в зависимости от \r\nситуации ";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(429, 193);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cb_friend);
            this.Controls.Add(this.cb_rain);
            this.Controls.Add(this.cb_drink);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.button3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Backpropagation";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.CheckBox cb_drink;
        private System.Windows.Forms.CheckBox cb_rain;
        private System.Windows.Forms.CheckBox cb_friend;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label1;
    }
}

