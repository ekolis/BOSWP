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
			this.components = new System.ComponentModel.Container();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			this.lstMessages = new System.Windows.Forms.ListBox();
			this.label1 = new System.Windows.Forms.Label();
			this.lblHitpoints = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.lblShields = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.lblMass = new System.Windows.Forms.Label();
			this.lblCrew = new System.Windows.Forms.Label();
			this.lblThrust = new System.Windows.Forms.Label();
			this.lblSpeed = new System.Windows.Forms.Label();
			this.gridWeapons = new System.Windows.Forms.DataGridView();
			this.waitDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.damageDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.rangeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.isMissileDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.reloadRateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.weaponInfoBindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.galaxyMap = new BOSWP.CharGridView();
			this.systemMap = new BOSWP.CharGridView();
			this.btnComponents = new System.Windows.Forms.Button();
			this.label7 = new System.Windows.Forms.Label();
			this.lblSavings = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.gridWeapons)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.weaponInfoBindingSource)).BeginInit();
			this.SuspendLayout();
			// 
			// lstMessages
			// 
			this.lstMessages.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lstMessages.FormattingEnabled = true;
			this.lstMessages.Location = new System.Drawing.Point(12, 13);
			this.lstMessages.Name = "lstMessages";
			this.lstMessages.Size = new System.Drawing.Size(545, 95);
			this.lstMessages.TabIndex = 1;
			this.lstMessages.TabStop = false;
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(560, 232);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(51, 13);
			this.label1.TabIndex = 3;
			this.label1.Text = "Hitpoints:";
			// 
			// lblHitpoints
			// 
			this.lblHitpoints.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lblHitpoints.Location = new System.Drawing.Point(705, 232);
			this.lblHitpoints.Name = "lblHitpoints";
			this.lblHitpoints.Size = new System.Drawing.Size(61, 13);
			this.lblHitpoints.TabIndex = 4;
			this.lblHitpoints.Text = "0";
			this.lblHitpoints.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label2
			// 
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(560, 245);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(44, 13);
			this.label2.TabIndex = 5;
			this.label2.Text = "Shields:";
			// 
			// lblShields
			// 
			this.lblShields.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lblShields.Location = new System.Drawing.Point(705, 245);
			this.lblShields.Name = "lblShields";
			this.lblShields.Size = new System.Drawing.Size(61, 13);
			this.lblShields.TabIndex = 6;
			this.lblShields.Text = "0";
			this.lblShields.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label3
			// 
			this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(560, 271);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(35, 13);
			this.label3.TabIndex = 7;
			this.label3.Text = "Mass:";
			// 
			// label4
			// 
			this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(560, 284);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(34, 13);
			this.label4.TabIndex = 8;
			this.label4.Text = "Crew:";
			// 
			// label5
			// 
			this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(561, 297);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(40, 13);
			this.label5.TabIndex = 9;
			this.label5.Text = "Thrust:";
			// 
			// label6
			// 
			this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(561, 310);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(41, 13);
			this.label6.TabIndex = 10;
			this.label6.Text = "Speed:";
			// 
			// lblMass
			// 
			this.lblMass.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lblMass.Location = new System.Drawing.Point(705, 271);
			this.lblMass.Name = "lblMass";
			this.lblMass.Size = new System.Drawing.Size(61, 13);
			this.lblMass.TabIndex = 11;
			this.lblMass.Text = "0";
			this.lblMass.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// lblCrew
			// 
			this.lblCrew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lblCrew.Location = new System.Drawing.Point(705, 284);
			this.lblCrew.Name = "lblCrew";
			this.lblCrew.Size = new System.Drawing.Size(61, 13);
			this.lblCrew.TabIndex = 12;
			this.lblCrew.Text = "0";
			this.lblCrew.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// lblThrust
			// 
			this.lblThrust.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lblThrust.Location = new System.Drawing.Point(705, 297);
			this.lblThrust.Name = "lblThrust";
			this.lblThrust.Size = new System.Drawing.Size(61, 13);
			this.lblThrust.TabIndex = 13;
			this.lblThrust.Text = "0";
			this.lblThrust.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// lblSpeed
			// 
			this.lblSpeed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lblSpeed.Location = new System.Drawing.Point(705, 310);
			this.lblSpeed.Name = "lblSpeed";
			this.lblSpeed.Size = new System.Drawing.Size(61, 13);
			this.lblSpeed.TabIndex = 14;
			this.lblSpeed.Text = "0";
			this.lblSpeed.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// gridWeapons
			// 
			this.gridWeapons.AllowUserToAddRows = false;
			this.gridWeapons.AllowUserToDeleteRows = false;
			this.gridWeapons.AllowUserToResizeColumns = false;
			this.gridWeapons.AllowUserToResizeRows = false;
			this.gridWeapons.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridWeapons.AutoGenerateColumns = false;
			this.gridWeapons.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.gridWeapons.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.waitDataGridViewTextBoxColumn,
            this.damageDataGridViewTextBoxColumn,
            this.rangeDataGridViewTextBoxColumn,
            this.isMissileDataGridViewCheckBoxColumn,
            this.reloadRateDataGridViewTextBoxColumn});
			this.gridWeapons.DataSource = this.weaponInfoBindingSource;
			this.gridWeapons.Location = new System.Drawing.Point(509, 381);
			this.gridWeapons.Name = "gridWeapons";
			this.gridWeapons.ReadOnly = true;
			this.gridWeapons.RowHeadersVisible = false;
			this.gridWeapons.Size = new System.Drawing.Size(262, 233);
			this.gridWeapons.TabIndex = 15;
			// 
			// waitDataGridViewTextBoxColumn
			// 
			this.waitDataGridViewTextBoxColumn.DataPropertyName = "Wait";
			dataGridViewCellStyle1.Format = "N2";
			dataGridViewCellStyle1.NullValue = null;
			this.waitDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle1;
			this.waitDataGridViewTextBoxColumn.HeaderText = "Wait";
			this.waitDataGridViewTextBoxColumn.Name = "waitDataGridViewTextBoxColumn";
			this.waitDataGridViewTextBoxColumn.ReadOnly = true;
			this.waitDataGridViewTextBoxColumn.ToolTipText = "Delay until the weapon can be fired.";
			this.waitDataGridViewTextBoxColumn.Width = 50;
			// 
			// damageDataGridViewTextBoxColumn
			// 
			this.damageDataGridViewTextBoxColumn.DataPropertyName = "Damage";
			this.damageDataGridViewTextBoxColumn.HeaderText = "Damage";
			this.damageDataGridViewTextBoxColumn.Name = "damageDataGridViewTextBoxColumn";
			this.damageDataGridViewTextBoxColumn.ReadOnly = true;
			this.damageDataGridViewTextBoxColumn.ToolTipText = "Damage inflicted by the weapon.";
			this.damageDataGridViewTextBoxColumn.Width = 50;
			// 
			// rangeDataGridViewTextBoxColumn
			// 
			this.rangeDataGridViewTextBoxColumn.DataPropertyName = "Range";
			this.rangeDataGridViewTextBoxColumn.HeaderText = "Range";
			this.rangeDataGridViewTextBoxColumn.Name = "rangeDataGridViewTextBoxColumn";
			this.rangeDataGridViewTextBoxColumn.ReadOnly = true;
			this.rangeDataGridViewTextBoxColumn.ToolTipText = "Range of the weapon.";
			this.rangeDataGridViewTextBoxColumn.Width = 50;
			// 
			// isMissileDataGridViewCheckBoxColumn
			// 
			this.isMissileDataGridViewCheckBoxColumn.DataPropertyName = "IsMissile";
			this.isMissileDataGridViewCheckBoxColumn.HeaderText = "Missile?";
			this.isMissileDataGridViewCheckBoxColumn.Name = "isMissileDataGridViewCheckBoxColumn";
			this.isMissileDataGridViewCheckBoxColumn.ReadOnly = true;
			this.isMissileDataGridViewCheckBoxColumn.ToolTipText = "Is this a missile weapon?";
			this.isMissileDataGridViewCheckBoxColumn.Width = 50;
			// 
			// reloadRateDataGridViewTextBoxColumn
			// 
			this.reloadRateDataGridViewTextBoxColumn.DataPropertyName = "ReloadRate";
			this.reloadRateDataGridViewTextBoxColumn.HeaderText = "Reload";
			this.reloadRateDataGridViewTextBoxColumn.Name = "reloadRateDataGridViewTextBoxColumn";
			this.reloadRateDataGridViewTextBoxColumn.ReadOnly = true;
			this.reloadRateDataGridViewTextBoxColumn.ToolTipText = "The weapon\'s reload time.";
			this.reloadRateDataGridViewTextBoxColumn.Width = 50;
			// 
			// weaponInfoBindingSource
			// 
			this.weaponInfoBindingSource.DataSource = typeof(BOSWP.WeaponInfo);
			// 
			// galaxyMap
			// 
			this.galaxyMap.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.galaxyMap.BackColor = System.Drawing.Color.Black;
			this.galaxyMap.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.galaxyMap.Grid = null;
			this.galaxyMap.Location = new System.Drawing.Point(563, 13);
			this.galaxyMap.Name = "galaxyMap";
			this.galaxyMap.NullColor = System.Drawing.Color.Empty;
			this.galaxyMap.NullGlyph = '\0';
			this.galaxyMap.Size = new System.Drawing.Size(208, 212);
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
			this.systemMap.MouseDown += new System.Windows.Forms.MouseEventHandler(this.systemMap_MouseDown);
			// 
			// btnComponents
			// 
			this.btnComponents.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnComponents.Location = new System.Drawing.Point(696, 352);
			this.btnComponents.Name = "btnComponents";
			this.btnComponents.Size = new System.Drawing.Size(75, 23);
			this.btnComponents.TabIndex = 16;
			this.btnComponents.Text = "Components";
			this.btnComponents.UseVisualStyleBackColor = true;
			this.btnComponents.Click += new System.EventHandler(this.btnComponents_Click);
			// 
			// label7
			// 
			this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(560, 336);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(48, 13);
			this.label7.TabIndex = 17;
			this.label7.Text = "Savings:";
			// 
			// lblSavings
			// 
			this.lblSavings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lblSavings.Location = new System.Drawing.Point(705, 336);
			this.lblSavings.Name = "lblSavings";
			this.lblSavings.Size = new System.Drawing.Size(61, 13);
			this.lblSavings.TabIndex = 18;
			this.lblSavings.Text = "$0";
			this.lblSavings.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// GameForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(778, 626);
			this.Controls.Add(this.lblSavings);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.btnComponents);
			this.Controls.Add(this.gridWeapons);
			this.Controls.Add(this.lblSpeed);
			this.Controls.Add(this.lblThrust);
			this.Controls.Add(this.lblCrew);
			this.Controls.Add(this.lblMass);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.lblShields);
			this.Controls.Add(this.label2);
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
			((System.ComponentModel.ISupportInitialize)(this.gridWeapons)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.weaponInfoBindingSource)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private CharGridView systemMap;
		private CharGridView galaxyMap;
		private System.Windows.Forms.ListBox lstMessages;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label lblHitpoints;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label lblShields;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label lblMass;
		private System.Windows.Forms.Label lblCrew;
		private System.Windows.Forms.Label lblThrust;
		private System.Windows.Forms.Label lblSpeed;
		private System.Windows.Forms.DataGridView gridWeapons;
		private System.Windows.Forms.BindingSource weaponInfoBindingSource;
		private System.Windows.Forms.DataGridViewTextBoxColumn waitDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn damageDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn rangeDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewCheckBoxColumn isMissileDataGridViewCheckBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn reloadRateDataGridViewTextBoxColumn;
		private System.Windows.Forms.Button btnComponents;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label lblSavings;
	}
}

