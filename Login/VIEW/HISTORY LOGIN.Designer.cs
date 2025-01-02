namespace Login.VIEW
{
    partial class HISTORY_LOGIN
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
            this.txtNama = new System.Windows.Forms.TextBox();
            this.lvwUser = new System.Windows.Forms.ListView();
            this.btnHapusHistory = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnUpdateUser = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtNama
            // 
            this.txtNama.Location = new System.Drawing.Point(308, 195);
            this.txtNama.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtNama.Name = "txtNama";
            this.txtNama.Size = new System.Drawing.Size(985, 26);
            this.txtNama.TabIndex = 30;
            // 
            // lvwUser
            // 
            this.lvwUser.HideSelection = false;
            this.lvwUser.Location = new System.Drawing.Point(82, 257);
            this.lvwUser.Name = "lvwUser";
            this.lvwUser.Size = new System.Drawing.Size(1376, 535);
            this.lvwUser.TabIndex = 26;
            this.lvwUser.UseCompatibleStateImageBehavior = false;
            this.lvwUser.SelectedIndexChanged += new System.EventHandler(this.lvwUser_SelectedIndexChanged);
            // 
            // btnHapusHistory
            // 
            this.btnHapusHistory.BackColor = System.Drawing.Color.Transparent;
            this.btnHapusHistory.BackgroundImage = global::Login.Properties.Resources.Artboard_18;
            this.btnHapusHistory.Font = new System.Drawing.Font("Segoe UI Black", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHapusHistory.Location = new System.Drawing.Point(82, 825);
            this.btnHapusHistory.Name = "btnHapusHistory";
            this.btnHapusHistory.Size = new System.Drawing.Size(158, 41);
            this.btnHapusHistory.TabIndex = 28;
            this.btnHapusHistory.Text = "HAPUS USER";
            this.btnHapusHistory.UseVisualStyleBackColor = false;
            this.btnHapusHistory.Click += new System.EventHandler(this.btnHapusHistory_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.Transparent;
            this.btnSearch.BackgroundImage = global::Login.Properties.Resources.Artboard_18;
            this.btnSearch.Font = new System.Drawing.Font("Segoe UI Black", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.Location = new System.Drawing.Point(1304, 183);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(158, 41);
            this.btnSearch.TabIndex = 27;
            this.btnSearch.Text = "SEARCH USER";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnUpdateUser
            // 
            this.btnUpdateUser.BackColor = System.Drawing.Color.Transparent;
            this.btnUpdateUser.BackgroundImage = global::Login.Properties.Resources.Artboard_18;
            this.btnUpdateUser.Font = new System.Drawing.Font("Segoe UI Black", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdateUser.Location = new System.Drawing.Point(113, 187);
            this.btnUpdateUser.Name = "btnUpdateUser";
            this.btnUpdateUser.Size = new System.Drawing.Size(158, 41);
            this.btnUpdateUser.TabIndex = 31;
            this.btnUpdateUser.Text = "UPDATE USER";
            this.btnUpdateUser.UseVisualStyleBackColor = false;
            this.btnUpdateUser.Click += new System.EventHandler(this.btnUpdateUser_Click);
            // 
            // HISTORY_LOGIN
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Login.Properties.Resources.p2;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1517, 968);
            this.Controls.Add(this.btnUpdateUser);
            this.Controls.Add(this.txtNama);
            this.Controls.Add(this.btnHapusHistory);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.lvwUser);
            this.DoubleBuffered = true;
            this.Name = "HISTORY_LOGIN";
            this.Text = "HISTORY_LOGIN";
            this.Load += new System.EventHandler(this.HISTORY_LOGIN_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtNama;
        private System.Windows.Forms.Button btnHapusHistory;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.ListView lvwUser;
        private System.Windows.Forms.Button btnUpdateUser;
    }
}