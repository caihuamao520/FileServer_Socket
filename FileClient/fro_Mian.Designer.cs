namespace FileClient
{
    partial class fro_Mian
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnEditState = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtPoint = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtIP = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtSendContent = new System.Windows.Forms.TextBox();
            this.btnSendMsg = new System.Windows.Forms.Button();
            this.ckClerContent = new System.Windows.Forms.CheckBox();
            this.linkUpNetFile = new System.Windows.Forms.LinkLabel();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnEditState
            // 
            this.btnEditState.Location = new System.Drawing.Point(187, 90);
            this.btnEditState.Name = "btnEditState";
            this.btnEditState.Size = new System.Drawing.Size(133, 33);
            this.btnEditState.TabIndex = 6;
            this.btnEditState.Text = "连接到服务端";
            this.btnEditState.UseVisualStyleBackColor = true;
            this.btnEditState.Click += new System.EventHandler(this.btnEditState_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.txtPoint);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtIP);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(308, 57);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "网络配置";
            // 
            // txtPoint
            // 
            this.txtPoint.Location = new System.Drawing.Point(209, 20);
            this.txtPoint.MaxLength = 5;
            this.txtPoint.Name = "txtPoint";
            this.txtPoint.Size = new System.Drawing.Size(43, 21);
            this.txtPoint.TabIndex = 3;
            this.txtPoint.Text = "8899";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(195, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(11, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = ":";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "IP地址：";
            // 
            // txtIP
            // 
            this.txtIP.Location = new System.Drawing.Point(77, 20);
            this.txtIP.Name = "txtIP";
            this.txtIP.Size = new System.Drawing.Size(114, 21);
            this.txtIP.TabIndex = 0;
            this.txtIP.Text = "127.0.0.1";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.ckClerContent);
            this.groupBox2.Controls.Add(this.btnSendMsg);
            this.groupBox2.Controls.Add(this.txtSendContent);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(18, 139);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(302, 174);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "发送消息";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "发送的内容：";
            // 
            // txtSendContent
            // 
            this.txtSendContent.Location = new System.Drawing.Point(20, 43);
            this.txtSendContent.Multiline = true;
            this.txtSendContent.Name = "txtSendContent";
            this.txtSendContent.Size = new System.Drawing.Size(258, 76);
            this.txtSendContent.TabIndex = 1;
            // 
            // btnSendMsg
            // 
            this.btnSendMsg.Location = new System.Drawing.Point(171, 135);
            this.btnSendMsg.Name = "btnSendMsg";
            this.btnSendMsg.Size = new System.Drawing.Size(107, 33);
            this.btnSendMsg.TabIndex = 2;
            this.btnSendMsg.Text = "发送(Enter)";
            this.btnSendMsg.UseVisualStyleBackColor = true;
            this.btnSendMsg.Click += new System.EventHandler(this.btnSendMsg_Click);
            // 
            // ckClerContent
            // 
            this.ckClerContent.AutoSize = true;
            this.ckClerContent.Checked = true;
            this.ckClerContent.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckClerContent.Location = new System.Drawing.Point(14, 144);
            this.ckClerContent.Name = "ckClerContent";
            this.ckClerContent.Size = new System.Drawing.Size(132, 16);
            this.ckClerContent.TabIndex = 3;
            this.ckClerContent.Text = "发送成功后清除消息";
            this.ckClerContent.UseVisualStyleBackColor = true;
            // 
            // linkUpNetFile
            // 
            this.linkUpNetFile.AutoSize = true;
            this.linkUpNetFile.Location = new System.Drawing.Point(18, 111);
            this.linkUpNetFile.Name = "linkUpNetFile";
            this.linkUpNetFile.Size = new System.Drawing.Size(77, 12);
            this.linkUpNetFile.TabIndex = 8;
            this.linkUpNetFile.TabStop = true;
            this.linkUpNetFile.Text = "上传一个文件";
            this.linkUpNetFile.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkUpNetFile_LinkClicked);
            // 
            // fro_Mian
            // 
            this.AcceptButton = this.btnSendMsg;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(332, 325);
            this.Controls.Add(this.linkUpNetFile);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnEditState);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "fro_Mian";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "客户端";
            this.Load += new System.EventHandler(this.fro_Mian_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnEditState;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtPoint;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtIP;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnSendMsg;
        private System.Windows.Forms.TextBox txtSendContent;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox ckClerContent;
        private System.Windows.Forms.LinkLabel linkUpNetFile;
    }
}

