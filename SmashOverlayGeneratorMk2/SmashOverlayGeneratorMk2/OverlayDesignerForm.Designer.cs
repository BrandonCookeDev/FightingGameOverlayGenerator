namespace SmashOverlayGeneratorMk2
{
    partial class OverlayDesignerForm
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
            this.overlayDesignerDataGrid = new System.Windows.Forms.DataGridView();
            this.Data = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.picturePreviewBox = new System.Windows.Forms.PictureBox();
            this.clearBtn = new System.Windows.Forms.Button();
            this.generateBtn = new System.Windows.Forms.Button();
            this.browsePicturesButton = new System.Windows.Forms.Button();
            this.browsePictureTextbox = new System.Windows.Forms.TextBox();
            this.designerLogLabel = new System.Windows.Forms.Label();
            this.cutBtn = new System.Windows.Forms.Button();
            this.squareSelectBtn = new System.Windows.Forms.CheckBox();
            this.freeSelectBtn = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.overlayDesignerDataGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picturePreviewBox)).BeginInit();
            this.SuspendLayout();
            // 
            // overlayDesignerDataGrid
            // 
            this.overlayDesignerDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.overlayDesignerDataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Data,
            this.Value});
            this.overlayDesignerDataGrid.Location = new System.Drawing.Point(869, 187);
            this.overlayDesignerDataGrid.Name = "overlayDesignerDataGrid";
            this.overlayDesignerDataGrid.Size = new System.Drawing.Size(240, 329);
            this.overlayDesignerDataGrid.TabIndex = 1;
            // 
            // Data
            // 
            this.Data.HeaderText = "Data";
            this.Data.Name = "Data";
            this.Data.ReadOnly = true;
            this.Data.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // Value
            // 
            this.Value.HeaderText = "Value";
            this.Value.Name = "Value";
            this.Value.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // picturePreviewBox
            // 
            this.picturePreviewBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picturePreviewBox.Location = new System.Drawing.Point(10, 10);
            this.picturePreviewBox.Name = "picturePreviewBox";
            this.picturePreviewBox.Size = new System.Drawing.Size(854, 548);
            this.picturePreviewBox.TabIndex = 0;
            this.picturePreviewBox.TabStop = false;
            this.picturePreviewBox.Click += new System.EventHandler(this.picturePreviewBox_Click);
            // 
            // clearBtn
            // 
            this.clearBtn.Location = new System.Drawing.Point(869, 522);
            this.clearBtn.Name = "clearBtn";
            this.clearBtn.Size = new System.Drawing.Size(124, 36);
            this.clearBtn.TabIndex = 2;
            this.clearBtn.Text = "Clear Everything";
            this.clearBtn.UseVisualStyleBackColor = true;
            this.clearBtn.Click += new System.EventHandler(this.clearBtn_Click);
            // 
            // generateBtn
            // 
            this.generateBtn.Location = new System.Drawing.Point(999, 522);
            this.generateBtn.Name = "generateBtn";
            this.generateBtn.Size = new System.Drawing.Size(110, 36);
            this.generateBtn.TabIndex = 3;
            this.generateBtn.Text = "Generate";
            this.generateBtn.UseVisualStyleBackColor = true;
            // 
            // browsePicturesButton
            // 
            this.browsePicturesButton.Location = new System.Drawing.Point(869, 38);
            this.browsePicturesButton.Name = "browsePicturesButton";
            this.browsePicturesButton.Size = new System.Drawing.Size(70, 23);
            this.browsePicturesButton.TabIndex = 4;
            this.browsePicturesButton.Text = "Browse";
            this.browsePicturesButton.UseVisualStyleBackColor = true;
            this.browsePicturesButton.Click += new System.EventHandler(this.browsePicturesButton_Click);
            // 
            // browsePictureTextbox
            // 
            this.browsePictureTextbox.Enabled = false;
            this.browsePictureTextbox.Location = new System.Drawing.Point(945, 41);
            this.browsePictureTextbox.Name = "browsePictureTextbox";
            this.browsePictureTextbox.Size = new System.Drawing.Size(161, 20);
            this.browsePictureTextbox.TabIndex = 5;
            // 
            // designerLogLabel
            // 
            this.designerLogLabel.AutoSize = true;
            this.designerLogLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.designerLogLabel.Location = new System.Drawing.Point(870, 10);
            this.designerLogLabel.Name = "designerLogLabel";
            this.designerLogLabel.Size = new System.Drawing.Size(20, 16);
            this.designerLogLabel.TabIndex = 8;
            this.designerLogLabel.Text = "...";
            // 
            // cutBtn
            // 
            this.cutBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cutBtn.Location = new System.Drawing.Point(869, 158);
            this.cutBtn.Name = "cutBtn";
            this.cutBtn.Size = new System.Drawing.Size(70, 23);
            this.cutBtn.TabIndex = 9;
            this.cutBtn.Text = "Cut";
            this.cutBtn.UseVisualStyleBackColor = true;
            // 
            // squareSelectBtn
            // 
            this.squareSelectBtn.Appearance = System.Windows.Forms.Appearance.Button;
            this.squareSelectBtn.AutoSize = true;
            this.squareSelectBtn.Location = new System.Drawing.Point(869, 68);
            this.squareSelectBtn.Name = "squareSelectBtn";
            this.squareSelectBtn.Size = new System.Drawing.Size(84, 23);
            this.squareSelectBtn.TabIndex = 10;
            this.squareSelectBtn.Text = "Square Select";
            this.squareSelectBtn.UseVisualStyleBackColor = true;
            this.squareSelectBtn.CheckedChanged += new System.EventHandler(this.squareSelectBtn_CheckedChanged);
            // 
            // freeSelectBtn
            // 
            this.freeSelectBtn.Appearance = System.Windows.Forms.Appearance.Button;
            this.freeSelectBtn.AutoSize = true;
            this.freeSelectBtn.Location = new System.Drawing.Point(869, 98);
            this.freeSelectBtn.Name = "freeSelectBtn";
            this.freeSelectBtn.Size = new System.Drawing.Size(71, 23);
            this.freeSelectBtn.TabIndex = 11;
            this.freeSelectBtn.Text = "Free Select";
            this.freeSelectBtn.UseVisualStyleBackColor = true;
            // 
            // OverlayDesignerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1121, 570);
            this.Controls.Add(this.freeSelectBtn);
            this.Controls.Add(this.squareSelectBtn);
            this.Controls.Add(this.cutBtn);
            this.Controls.Add(this.designerLogLabel);
            this.Controls.Add(this.browsePictureTextbox);
            this.Controls.Add(this.browsePicturesButton);
            this.Controls.Add(this.generateBtn);
            this.Controls.Add(this.clearBtn);
            this.Controls.Add(this.overlayDesignerDataGrid);
            this.Controls.Add(this.picturePreviewBox);
            this.Name = "OverlayDesignerForm";
            this.Text = "OverlayDesignerForm";
            ((System.ComponentModel.ISupportInitialize)(this.overlayDesignerDataGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picturePreviewBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picturePreviewBox;
        private System.Windows.Forms.DataGridView overlayDesignerDataGrid;
        private System.Windows.Forms.Button clearBtn;
        private System.Windows.Forms.Button generateBtn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Data;
        private System.Windows.Forms.DataGridViewTextBoxColumn Value;
        private System.Windows.Forms.Button browsePicturesButton;
        private System.Windows.Forms.TextBox browsePictureTextbox;
        private System.Windows.Forms.Label designerLogLabel;
        private System.Windows.Forms.Button cutBtn;
        private System.Windows.Forms.CheckBox squareSelectBtn;
        private System.Windows.Forms.CheckBox freeSelectBtn;
    }
}