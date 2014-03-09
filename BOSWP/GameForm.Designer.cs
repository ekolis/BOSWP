namespace BOSWP
{
	partial class GameForm
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
			this.lstMessages = new System.Windows.Forms.ListBox();
			this.galaxyMap = new BOSWP.CharGridView();
			this.systemMap = new BOSWP.CharGridView();
			this.label1 = new System.Windows.Forms.Label();
			this.lblHitpoints = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// lstMessages
			// 
			this.lstMessages.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lstMessages.FormattingEnabled = true;
			this.lstMessages.Location = new System.Drawing.Point(12, 13);
			this.lstMessages.Name = "lstMessages";
			this.lstMessages.Size = new System.Drawing.Size(491, 95);
			this.lstMessages.TabIndex = 1;
			this.lstMessages.TabStop = false;
			// 
			// galaxyMap
			// 
			this.galaxyMap.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.galaxyMap.BackColor = System.Drawing.Color.Black;
			this.galaxyMap.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.galaxyMap.Grid = null;
			this.galaxyMap.Location = new System.Drawing.Point(509, 13);
			this.galaxyMap.Name = "galaxyMap";
			this.galaxyMap.NullColor = System.Drawing.Color.Empty;
			this.galaxyMap.NullGlyph = '\0';
			this.galaxyMap.Size = new System.Drawing.Size(212, 212);
			this.galaxyMap.TabIndex = 2;
			this.galaxyMap.TabStop = false;
			this.galaxyMap.Text = "charGridView1";
			// 
			// systemMap
			// 
			this.systemMap.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.systemMap.BackColor = System.Drawing.Color.Black;
			this.systemMap.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.systemMap.Grid = null;
			this.systemMap.Location = new System.Drawing.Point(12, 123);
			this.systemMap.Name = "systemMap";
			this.systemMap.NullColor = System.Drawing.Color.Silver;
			this.systemMap.NullGlyph = '.';
			this.systemMap.Size = new System.Drawing.Size(491, 491);
			this.systemMap.TabIndex = 0;
			this.systemMap.Text = "charGridView1";
			this.systemMap.KeyDown += new System.Windows.Forms.KeyEventHandler(this.systemMap_KeyDown);
			this.systemMap.Leave += new System.EventHandler(this.systemMap_Leave);
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(510, 232);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(51, 13);
			this.label1.TabIndex = 3;
			this.label1.Text = "Hitpoints:";
			// 
			// lblHitpoints
			// 
			this.lblHitpoints.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lblHitpoints.AutoSize = true;
			this.lblHitpoints.Location = new System.Drawing.Point(703, 232);
			this.lblHitpoints.Name = "lblHitpoints";
			this.lblHitpoints.Size = new System.Drawing.Size(13, 13);
			this.lblHitpoints.TabIndex = 4;
			this.lblHitpoints.Text = "0";
			this.lblHitpoints.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// GameForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(728, 626);
			this.Controls.Add(this.lblHitpoints);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.galaxyMap);
			this.Controls.Add(this.lstMessages);
			this.Controls.Add(this.systemMap);
			this.KeyPreview = true;
			this.Name = "GameForm";
			this.Text = "Beware of Strange Warp Points";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.GameForm_FormClosed);
			this.Load += new System.EventHandler(this.GameForm_Load);
			this.SizeChanged += new System.EventHandler(this.GameForm_SizeChanged);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GameForm_KeyDown);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private CharGridView systemMap;
		private CharGridView galaxyMap;
		private System.Windows.Forms.ListBox lstMessages;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label lblHitpoints;
	}
}

