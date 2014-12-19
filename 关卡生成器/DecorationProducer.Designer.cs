namespace 关卡生成器
{
    partial class DecorationProducer
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
            this.radius = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.centerX = new System.Windows.Forms.TextBox();
            this.centerY = new System.Windows.Forms.TextBox();
            this.centerZ = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.assetName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.number = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.number)).BeginInit();
            this.SuspendLayout();
            // 
            // radius
            // 
            this.radius.Location = new System.Drawing.Point(54, 12);
            this.radius.Name = "radius";
            this.radius.Size = new System.Drawing.Size(48, 21);
            this.radius.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "半径";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "中心";
            // 
            // centerX
            // 
            this.centerX.Location = new System.Drawing.Point(54, 40);
            this.centerX.Name = "centerX";
            this.centerX.Size = new System.Drawing.Size(48, 21);
            this.centerX.TabIndex = 3;
            // 
            // centerY
            // 
            this.centerY.Location = new System.Drawing.Point(108, 40);
            this.centerY.Name = "centerY";
            this.centerY.Size = new System.Drawing.Size(48, 21);
            this.centerY.TabIndex = 4;
            // 
            // centerZ
            // 
            this.centerZ.Location = new System.Drawing.Point(162, 40);
            this.centerZ.Name = "centerZ";
            this.centerZ.Size = new System.Drawing.Size(48, 21);
            this.centerZ.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "装饰物AssetName";
            // 
            // assetName
            // 
            this.assetName.Location = new System.Drawing.Point(108, 67);
            this.assetName.Name = "assetName";
            this.assetName.Size = new System.Drawing.Size(129, 21);
            this.assetName.TabIndex = 7;
            this.assetName.Text = "DecorationTypes\\Rock1";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(108, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 8;
            this.label4.Text = "数量";
            // 
            // number
            // 
            this.number.Location = new System.Drawing.Point(144, 12);
            this.number.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.number.Name = "number";
            this.number.Size = new System.Drawing.Size(66, 21);
            this.number.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 86);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(0, 12);
            this.label5.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(98, 94);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(299, 12);
            this.label6.TabIndex = 12;
            this.label6.Text = "加到关卡环境文档的<Decorations></Decorations>间！";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(322, 65);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 10;
            this.button1.Text = "输出";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // DecorationProducer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(410, 128);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.number);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.assetName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.centerZ);
            this.Controls.Add(this.centerY);
            this.Controls.Add(this.centerX);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.radius);
            this.Name = "DecorationProducer";
            this.Text = "装饰物生成器";
            ((System.ComponentModel.ISupportInitialize)(this.number)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox radius;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox centerX;
        private System.Windows.Forms.TextBox centerY;
        private System.Windows.Forms.TextBox centerZ;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox assetName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown number;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button1;
    }
}

