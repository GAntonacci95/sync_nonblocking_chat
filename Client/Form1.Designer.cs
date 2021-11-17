namespace LatoClient
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
            this.btnConnect = new System.Windows.Forms.Button();
            this.txtIP = new System.Windows.Forms.TextBox();
            this.txtPorta = new System.Windows.Forms.TextBox();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtGen = new System.Windows.Forms.RichTextBox();
            this.btnInvia = new System.Windows.Forms.Button();
            this.txtMess = new System.Windows.Forms.TextBox();
            this.txtDest = new System.Windows.Forms.TextBox();
            this.txtMitt = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(249, 84);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 23);
            this.btnConnect.TabIndex = 0;
            this.btnConnect.Text = "Connetti";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // txtIP
            // 
            this.txtIP.Location = new System.Drawing.Point(6, 32);
            this.txtIP.Name = "txtIP";
            this.txtIP.Size = new System.Drawing.Size(100, 20);
            this.txtIP.TabIndex = 1;
            // 
            // txtPorta
            // 
            this.txtPorta.Location = new System.Drawing.Point(6, 86);
            this.txtPorta.Name = "txtPorta";
            this.txtPorta.Size = new System.Drawing.Size(100, 20);
            this.txtPorta.TabIndex = 2;
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Location = new System.Drawing.Point(341, 84);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(75, 23);
            this.btnDisconnect.TabIndex = 3;
            this.btnDisconnect.Text = "Disconnetti";
            this.btnDisconnect.UseVisualStyleBackColor = true;
            this.btnDisconnect.Click += new System.EventHandler(this.btnDisconnect_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtMitt);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.btnDisconnect);
            this.groupBox1.Controls.Add(this.txtIP);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtPorta);
            this.groupBox1.Controls.Add(this.btnConnect);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(427, 129);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Connessione";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Porta:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(20, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "IP:";
            // 
            // txtGen
            // 
            this.txtGen.Location = new System.Drawing.Point(12, 147);
            this.txtGen.Name = "txtGen";
            this.txtGen.Size = new System.Drawing.Size(427, 441);
            this.txtGen.TabIndex = 5;
            this.txtGen.Text = "";
            // 
            // btnInvia
            // 
            this.btnInvia.Location = new System.Drawing.Point(364, 602);
            this.btnInvia.Name = "btnInvia";
            this.btnInvia.Size = new System.Drawing.Size(75, 23);
            this.btnInvia.TabIndex = 7;
            this.btnInvia.Text = "Invia";
            this.btnInvia.UseVisualStyleBackColor = true;
            this.btnInvia.Click += new System.EventHandler(this.btnInvia_Click);
            // 
            // txtMess
            // 
            this.txtMess.Location = new System.Drawing.Point(12, 602);
            this.txtMess.Multiline = true;
            this.txtMess.Name = "txtMess";
            this.txtMess.Size = new System.Drawing.Size(346, 23);
            this.txtMess.TabIndex = 7;
            // 
            // txtDest
            // 
            this.txtDest.Location = new System.Drawing.Point(469, 44);
            this.txtDest.Name = "txtDest";
            this.txtDest.Size = new System.Drawing.Size(151, 20);
            this.txtDest.TabIndex = 7;
            // 
            // txtMitt
            // 
            this.txtMitt.Location = new System.Drawing.Point(130, 56);
            this.txtMitt.Name = "txtMitt";
            this.txtMitt.Size = new System.Drawing.Size(100, 20);
            this.txtMitt.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(127, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Nickname:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(466, 28);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(115, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Nickname destinatario:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(632, 637);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtDest);
            this.Controls.Add(this.txtMess);
            this.Controls.Add(this.btnInvia);
            this.Controls.Add(this.txtGen);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Client";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.TextBox txtIP;
        private System.Windows.Forms.TextBox txtPorta;
        private System.Windows.Forms.Button btnDisconnect;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox txtGen;
        private System.Windows.Forms.Button btnInvia;
        private System.Windows.Forms.TextBox txtMess;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtMitt;
        private System.Windows.Forms.TextBox txtDest;
        private System.Windows.Forms.Label label4;
    }
}

