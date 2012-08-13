namespace BaoHien.UI
{
    partial class Login
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnLogin = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.lblErrorMessage = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.validator1 = new Itboy.Components.Validator(this.components);
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 14);
            this.label1.TabIndex = 5;
            this.label1.Text = "Tên đăng nhập:";
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(117, 25);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(234, 20);
            this.txtUsername.TabIndex = 0;
            this.validator1.SetType(this.txtUsername, Itboy.Components.ValidationType.Required);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(56, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 14);
            this.label2.TabIndex = 4;
            this.label2.Text = "Mật khẩu:";
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(275, 118);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(75, 25);
            this.btnLogin.TabIndex = 2;
            this.btnLogin.Text = "Đăng Nhập";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(175, 118);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 25);
            this.btnExit.TabIndex = 3;
            this.btnExit.Text = "Tắt";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // lblErrorMessage
            // 
            this.lblErrorMessage.AutoSize = true;
            this.lblErrorMessage.ForeColor = System.Drawing.Color.Red;
            this.lblErrorMessage.Location = new System.Drawing.Point(27, 98);
            this.lblErrorMessage.Name = "lblErrorMessage";
            this.lblErrorMessage.Size = new System.Drawing.Size(0, 14);
            this.lblErrorMessage.TabIndex = 6;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(118, 62);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(232, 20);
            this.txtPassword.TabIndex = 1;
            this.validator1.SetType(this.txtPassword, Itboy.Components.ValidationType.Required);
            // 
            // validator1
            // 
            this.validator1.Form = this;
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(399, 150);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.lblErrorMessage);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.label1);
            this.Name = "Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Đăng nhập để sử dụng hệ thống";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label lblErrorMessage;
        private System.Windows.Forms.TextBox txtPassword;
        private Itboy.Components.Validator validator1;
    }
}