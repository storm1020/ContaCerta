namespace PayGo_ContaCerta.Forms
{
    partial class frm_interacaoAPI
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
            this.btn_Post = new System.Windows.Forms.Button();
            this.txt_PostResponse = new System.Windows.Forms.TextBox();
            this.txt_TempoKey = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btn_Post
            // 
            this.btn_Post.Location = new System.Drawing.Point(232, 58);
            this.btn_Post.Name = "btn_Post";
            this.btn_Post.Size = new System.Drawing.Size(75, 52);
            this.btn_Post.TabIndex = 0;
            this.btn_Post.Text = "button1";
            this.btn_Post.UseVisualStyleBackColor = true;
            this.btn_Post.Click += new System.EventHandler(this.btn_Post_Click);
            // 
            // txt_PostResponse
            // 
            this.txt_PostResponse.Location = new System.Drawing.Point(364, 58);
            this.txt_PostResponse.Multiline = true;
            this.txt_PostResponse.Name = "txt_PostResponse";
            this.txt_PostResponse.Size = new System.Drawing.Size(333, 103);
            this.txt_PostResponse.TabIndex = 1;
            // 
            // txt_TempoKey
            // 
            this.txt_TempoKey.Location = new System.Drawing.Point(190, 300);
            this.txt_TempoKey.Name = "txt_TempoKey";
            this.txt_TempoKey.Size = new System.Drawing.Size(100, 20);
            this.txt_TempoKey.TabIndex = 2;
            // 
            // frm_interacaoAPI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.txt_TempoKey);
            this.Controls.Add(this.txt_PostResponse);
            this.Controls.Add(this.btn_Post);
            this.Name = "frm_interacaoAPI";
            this.Text = "InteracaoAPI";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_Post;
        private System.Windows.Forms.TextBox txt_PostResponse;
        private System.Windows.Forms.TextBox txt_TempoKey;
    }
}