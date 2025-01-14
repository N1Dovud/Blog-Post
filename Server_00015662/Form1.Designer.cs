namespace CWProject2_00015662
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
            this.btnSend = new System.Windows.Forms.Button();
            this.lblLocalAddress = new System.Windows.Forms.Label();
            this.lblIp = new System.Windows.Forms.Label();
            this.lblEmpNum = new System.Windows.Forms.Label();
            this.DTDate = new System.Windows.Forms.DateTimePicker();
            this.lblDate = new System.Windows.Forms.Label();
            this.DTInTime = new System.Windows.Forms.DateTimePicker();
            this.DTOutTime = new System.Windows.Forms.DateTimePicker();
            this.lblInTime = new System.Windows.Forms.Label();
            this.lblOutTime = new System.Windows.Forms.Label();
            this.tbxDailyLog = new System.Windows.Forms.TextBox();
            this.lblDailyLog = new System.Windows.Forms.Label();
            this.tbxEmpNum = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(48, 435);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(245, 43);
            this.btnSend.TabIndex = 3;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // lblLocalAddress
            // 
            this.lblLocalAddress.AutoSize = true;
            this.lblLocalAddress.Location = new System.Drawing.Point(164, 9);
            this.lblLocalAddress.Name = "lblLocalAddress";
            this.lblLocalAddress.Size = new System.Drawing.Size(28, 16);
            this.lblLocalAddress.TabIndex = 5;
            this.lblLocalAddress.Text = "???";
            // 
            // lblIp
            // 
            this.lblIp.AutoSize = true;
            this.lblIp.Location = new System.Drawing.Point(12, 9);
            this.lblIp.Name = "lblIp";
            this.lblIp.Size = new System.Drawing.Size(41, 16);
            this.lblIp.TabIndex = 6;
            this.lblIp.Text = "From:";
            // 
            // lblEmpNum
            // 
            this.lblEmpNum.AutoSize = true;
            this.lblEmpNum.Location = new System.Drawing.Point(12, 49);
            this.lblEmpNum.Name = "lblEmpNum";
            this.lblEmpNum.Size = new System.Drawing.Size(123, 16);
            this.lblEmpNum.TabIndex = 7;
            this.lblEmpNum.Text = "Employee Number:";
            // 
            // DTDate
            // 
            this.DTDate.Location = new System.Drawing.Point(87, 95);
            this.DTDate.Name = "DTDate";
            this.DTDate.Size = new System.Drawing.Size(200, 22);
            this.DTDate.TabIndex = 9;
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Location = new System.Drawing.Point(14, 100);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(39, 16);
            this.lblDate.TabIndex = 10;
            this.lblDate.Text = "Date:";
            // 
            // DTInTime
            // 
            this.DTInTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.DTInTime.Location = new System.Drawing.Point(87, 151);
            this.DTInTime.Name = "DTInTime";
            this.DTInTime.Size = new System.Drawing.Size(200, 22);
            this.DTInTime.TabIndex = 11;
            // 
            // DTOutTime
            // 
            this.DTOutTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.DTOutTime.Location = new System.Drawing.Point(87, 203);
            this.DTOutTime.Name = "DTOutTime";
            this.DTOutTime.Size = new System.Drawing.Size(200, 22);
            this.DTOutTime.TabIndex = 12;
            // 
            // lblInTime
            // 
            this.lblInTime.AutoSize = true;
            this.lblInTime.Location = new System.Drawing.Point(14, 157);
            this.lblInTime.Name = "lblInTime";
            this.lblInTime.Size = new System.Drawing.Size(54, 16);
            this.lblInTime.TabIndex = 13;
            this.lblInTime.Text = "In Time:";
            // 
            // lblOutTime
            // 
            this.lblOutTime.AutoSize = true;
            this.lblOutTime.Location = new System.Drawing.Point(14, 208);
            this.lblOutTime.Name = "lblOutTime";
            this.lblOutTime.Size = new System.Drawing.Size(64, 16);
            this.lblOutTime.TabIndex = 14;
            this.lblOutTime.Text = "Out Time:";
            // 
            // tbxDailyLog
            // 
            this.tbxDailyLog.Location = new System.Drawing.Point(87, 250);
            this.tbxDailyLog.Multiline = true;
            this.tbxDailyLog.Name = "tbxDailyLog";
            this.tbxDailyLog.Size = new System.Drawing.Size(206, 139);
            this.tbxDailyLog.TabIndex = 15;
            // 
            // lblDailyLog
            // 
            this.lblDailyLog.AutoSize = true;
            this.lblDailyLog.Location = new System.Drawing.Point(14, 268);
            this.lblDailyLog.Name = "lblDailyLog";
            this.lblDailyLog.Size = new System.Drawing.Size(67, 16);
            this.lblDailyLog.TabIndex = 16;
            this.lblDailyLog.Text = "Daily Log:";
            // 
            // tbxEmpNum
            // 
            this.tbxEmpNum.Location = new System.Drawing.Point(141, 43);
            this.tbxEmpNum.Name = "tbxEmpNum";
            this.tbxEmpNum.Size = new System.Drawing.Size(146, 22);
            this.tbxEmpNum.TabIndex = 17;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 625);
            this.Controls.Add(this.tbxEmpNum);
            this.Controls.Add(this.lblDailyLog);
            this.Controls.Add(this.tbxDailyLog);
            this.Controls.Add(this.lblOutTime);
            this.Controls.Add(this.lblInTime);
            this.Controls.Add(this.DTOutTime);
            this.Controls.Add(this.DTInTime);
            this.Controls.Add(this.lblDate);
            this.Controls.Add(this.DTDate);
            this.Controls.Add(this.lblEmpNum);
            this.Controls.Add(this.lblIp);
            this.Controls.Add(this.lblLocalAddress);
            this.Controls.Add(this.btnSend);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Label lblLocalAddress;
        private System.Windows.Forms.Label lblIp;
        private System.Windows.Forms.Label lblEmpNum;
        private System.Windows.Forms.DateTimePicker DTDate;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.DateTimePicker DTInTime;
        private System.Windows.Forms.DateTimePicker DTOutTime;
        private System.Windows.Forms.Label lblInTime;
        private System.Windows.Forms.Label lblOutTime;
        private System.Windows.Forms.TextBox tbxDailyLog;
        private System.Windows.Forms.Label lblDailyLog;
        private System.Windows.Forms.TextBox tbxEmpNum;
    }
}

