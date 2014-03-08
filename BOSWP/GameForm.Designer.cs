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
			// 
			// galaxyMap
			// 
			this.galaxyMap.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.galaxyMap.BackColor = System.Drawing.Color.Black;
			this.galaxyMap.Grid = null;
			this.galaxyMap.Location = new System.Drawing.Point(509, 13);
			this.galaxyMap.Name = "galaxyMap";
			this.galaxyMap.NullColor = System.Drawing.Color.Empty;
			this.galaxyMap.NullGlyph = '\0';
			this.galaxyMap.Size = new System.Drawing.Size(212, 212);
			this.galaxyMap.TabIndex = 2;
			this.galaxyMap.Text = "charGridView1";
			// 
			// systemMap
			// 
			this.systemMap.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.systemMap.BackColor = System.Drawing.Color.Black;
			this.systemMap.Grid = null;
			this.systemMap.Location = new System.Drawing.Point(12, 123);
			this.systemMap.Name = "systemMap";
			this.systemMap.NullColor = System.Drawing.Color.Silver;
			this.systemMap.NullGlyph = '.';
			this.systemMap.Size = new System.Drawing.Size(491, 491);
			this.systemMap.TabIndex = 0;
			this.systemMap.Text = "charGridView1";
			// 
			// GameForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(728, 626);
			this.Controls.Add(this.galaxyMap);
			this.Controls.Add(this.lstMessages);
			this.Controls.Add(this.systemMap);
			this.KeyPreview = true;
			this.Name = "GameForm";
			this.Text = "Beware of Strange Warp Points";
			this.Load += new System.EventHandler(this.GameForm_Load);
			this.SizeChanged += new System.EventHandler(this.GameForm_SizeChanged);
			this.ResumeLayout(false);

		}

		#endregion

		private CharGridView systemMap;
		private System.Windows.Forms.ListBox lstMessages;
		private CharGridView galaxyMap;
	}
}

