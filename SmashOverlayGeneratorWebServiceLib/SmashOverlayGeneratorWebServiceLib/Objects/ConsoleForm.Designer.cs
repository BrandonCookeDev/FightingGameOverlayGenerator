namespace SmashOverlayGeneratorWebServiceLib.Objects
{
    partial class ConsoleForm
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
            this.connList = new System.Windows.Forms.ListView();
            this.connLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // connList
            // 
            this.connList.Location = new System.Drawing.Point(13, 41);
            this.connList.Name = "connList";
            this.connList.Size = new System.Drawing.Size(205, 209);
            this.connList.TabIndex = 0;
            this.connList.UseCompatibleStateImageBehavior = false;
            this.connList.SelectedIndexChanged += new System.EventHandler(this.connList_SelectedIndexChanged);
            // 
            // connLabel
            // 
            this.connLabel.AutoSize = true;
            this.connLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.connLabel.Location = new System.Drawing.Point(13, 13);
            this.connLabel.Name = "connLabel";
            this.connLabel.Size = new System.Drawing.Size(129, 20);
            this.connLabel.TabIndex = 1;
            this.connLabel.Text = "ConnectionList";
            // 
            // ConsoleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(352, 262);
            this.Controls.Add(this.connLabel);
            this.Controls.Add(this.connList);
            this.Name = "ConsoleForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.ConsoleForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView connList;
        private System.Windows.Forms.Label connLabel;
    }
}