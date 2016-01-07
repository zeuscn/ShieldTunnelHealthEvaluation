namespace TestDB
{
    partial class Form1
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnTestConnection = new System.Windows.Forms.Button();
            this.btnTestCriteriaRead = new System.Windows.Forms.Button();
            this.btnCriterias = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnTestConnection
            // 
            this.btnTestConnection.Location = new System.Drawing.Point(12, 12);
            this.btnTestConnection.Name = "btnTestConnection";
            this.btnTestConnection.Size = new System.Drawing.Size(75, 23);
            this.btnTestConnection.TabIndex = 0;
            this.btnTestConnection.Text = "测试连接";
            this.btnTestConnection.UseVisualStyleBackColor = true;
            this.btnTestConnection.Click += new System.EventHandler(this.btnTestConnection_Click);
            // 
            // btnTestCriteriaRead
            // 
            this.btnTestCriteriaRead.Location = new System.Drawing.Point(115, 11);
            this.btnTestCriteriaRead.Name = "btnTestCriteriaRead";
            this.btnTestCriteriaRead.Size = new System.Drawing.Size(75, 23);
            this.btnTestCriteriaRead.TabIndex = 1;
            this.btnTestCriteriaRead.Text = "测试ReadCriteria";
            this.btnTestCriteriaRead.UseVisualStyleBackColor = true;
            this.btnTestCriteriaRead.Click += new System.EventHandler(this.btnTestCriteriaRead_Click);
            // 
            // btnCriterias
            // 
            this.btnCriterias.Location = new System.Drawing.Point(216, 11);
            this.btnCriterias.Name = "btnCriterias";
            this.btnCriterias.Size = new System.Drawing.Size(75, 23);
            this.btnCriterias.TabIndex = 2;
            this.btnCriterias.Text = "测试分级标准";
            this.btnCriterias.UseVisualStyleBackColor = true;
            this.btnCriterias.Click += new System.EventHandler(this.btnCriterias_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(380, 254);
            this.Controls.Add(this.btnCriterias);
            this.Controls.Add(this.btnTestCriteriaRead);
            this.Controls.Add(this.btnTestConnection);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnTestConnection;
        private System.Windows.Forms.Button btnTestCriteriaRead;
        private System.Windows.Forms.Button btnCriterias;
    }
}

