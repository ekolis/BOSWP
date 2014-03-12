namespace BOSWP
{
	partial class ShopForm
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
			this.lstComponents = new System.Windows.Forms.ListBox();
			this.btnBuy = new System.Windows.Forms.Button();
			this.btnOurShip = new System.Windows.Forms.Button();
			this.lblCost = new System.Windows.Forms.Label();
			this.lblSensor = new System.Windows.Forms.Label();
			this.lblScanner = new System.Windows.Forms.Label();
			this.label29 = new System.Windows.Forms.Label();
			this.label30 = new System.Windows.Forms.Label();
			this.lblEmissive = new System.Windows.Forms.Label();
			this.lblPD = new System.Windows.Forms.Label();
			this.lblEvasion = new System.Windows.Forms.Label();
			this.label24 = new System.Windows.Forms.Label();
			this.label25 = new System.Windows.Forms.Label();
			this.label26 = new System.Windows.Forms.Label();
			this.lblMissile = new System.Windows.Forms.Label();
			this.lblReload = new System.Windows.Forms.Label();
			this.lblRange = new System.Windows.Forms.Label();
			this.lblDamage = new System.Windows.Forms.Label();
			this.label17 = new System.Windows.Forms.Label();
			this.label15 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.lblDescription = new System.Windows.Forms.Label();
			this.lblThrust = new System.Windows.Forms.Label();
			this.lblCrew = new System.Windows.Forms.Label();
			this.lblMass = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.label14 = new System.Windows.Forms.Label();
			this.lblShields = new System.Windows.Forms.Label();
			this.label16 = new System.Windows.Forms.Label();
			this.lblHitpoints = new System.Windows.Forms.Label();
			this.label18 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// lstComponents
			// 
			this.lstComponents.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.lstComponents.FormattingEnabled = true;
			this.lstComponents.Location = new System.Drawing.Point(12, 12);
			this.lstComponents.Name = "lstComponents";
			this.lstComponents.Size = new System.Drawing.Size(206, 355);
			this.lstComponents.TabIndex = 28;
			this.lstComponents.SelectedIndexChanged += new System.EventHandler(this.lstComponents_SelectedIndexChanged);
			// 
			// btnBuy
			// 
			this.btnBuy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnBuy.Location = new System.Drawing.Point(12, 391);
			this.btnBuy.Name = "btnBuy";
			this.btnBuy.Size = new System.Drawing.Size(100, 23);
			this.btnBuy.TabIndex = 29;
			this.btnBuy.Text = "Buy";
			this.btnBuy.UseVisualStyleBackColor = true;
			this.btnBuy.Click += new System.EventHandler(this.btnBuy_Click);
			// 
			// btnOurShip
			// 
			this.btnOurShip.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnOurShip.Location = new System.Drawing.Point(118, 391);
			this.btnOurShip.Name = "btnOurShip";
			this.btnOurShip.Size = new System.Drawing.Size(100, 23);
			this.btnOurShip.TabIndex = 30;
			this.btnOurShip.Text = "Our Ship";
			this.btnOurShip.UseVisualStyleBackColor = true;
			this.btnOurShip.Click += new System.EventHandler(this.btnOurShip_Click);
			// 
			// lblCost
			// 
			this.lblCost.AutoSize = true;
			this.lblCost.Location = new System.Drawing.Point(225, 391);
			this.lblCost.Name = "lblCost";
			this.lblCost.Size = new System.Drawing.Size(187, 13);
			this.lblCost.TabIndex = 68;
			this.lblCost.Text = "Choose a component to view its price.";
			// 
			// lblSensor
			// 
			this.lblSensor.Location = new System.Drawing.Point(370, 172);
			this.lblSensor.Name = "lblSensor";
			this.lblSensor.Size = new System.Drawing.Size(61, 13);
			this.lblSensor.TabIndex = 97;
			this.lblSensor.Text = "?";
			this.lblSensor.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// lblScanner
			// 
			this.lblScanner.Location = new System.Drawing.Point(370, 159);
			this.lblScanner.Name = "lblScanner";
			this.lblScanner.Size = new System.Drawing.Size(61, 13);
			this.lblScanner.TabIndex = 96;
			this.lblScanner.Text = "?";
			this.lblScanner.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label29
			// 
			this.label29.AutoSize = true;
			this.label29.Location = new System.Drawing.Point(225, 172);
			this.label29.Name = "label29";
			this.label29.Size = new System.Drawing.Size(78, 13);
			this.label29.TabIndex = 95;
			this.label29.Text = "Sensor Range:";
			// 
			// label30
			// 
			this.label30.AutoSize = true;
			this.label30.Location = new System.Drawing.Point(225, 159);
			this.label30.Name = "label30";
			this.label30.Size = new System.Drawing.Size(85, 13);
			this.label30.TabIndex = 94;
			this.label30.Text = "Scanner Range:";
			// 
			// lblEmissive
			// 
			this.lblEmissive.Location = new System.Drawing.Point(369, 137);
			this.lblEmissive.Name = "lblEmissive";
			this.lblEmissive.Size = new System.Drawing.Size(61, 13);
			this.lblEmissive.TabIndex = 93;
			this.lblEmissive.Text = "?";
			this.lblEmissive.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// lblPD
			// 
			this.lblPD.Location = new System.Drawing.Point(369, 124);
			this.lblPD.Name = "lblPD";
			this.lblPD.Size = new System.Drawing.Size(61, 13);
			this.lblPD.TabIndex = 92;
			this.lblPD.Text = "?";
			this.lblPD.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// lblEvasion
			// 
			this.lblEvasion.Location = new System.Drawing.Point(369, 111);
			this.lblEvasion.Name = "lblEvasion";
			this.lblEvasion.Size = new System.Drawing.Size(61, 13);
			this.lblEvasion.TabIndex = 91;
			this.lblEvasion.Text = "?";
			this.lblEvasion.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label24
			// 
			this.label24.AutoSize = true;
			this.label24.Location = new System.Drawing.Point(225, 137);
			this.label24.Name = "label24";
			this.label24.Size = new System.Drawing.Size(81, 13);
			this.label24.TabIndex = 90;
			this.label24.Text = "Emissive Armor:";
			// 
			// label25
			// 
			this.label25.AutoSize = true;
			this.label25.Location = new System.Drawing.Point(225, 124);
			this.label25.Name = "label25";
			this.label25.Size = new System.Drawing.Size(77, 13);
			this.label25.TabIndex = 89;
			this.label25.Text = "Point Defense:";
			// 
			// label26
			// 
			this.label26.AutoSize = true;
			this.label26.Location = new System.Drawing.Point(225, 111);
			this.label26.Name = "label26";
			this.label26.Size = new System.Drawing.Size(48, 13);
			this.label26.TabIndex = 88;
			this.label26.Text = "Evasion:";
			// 
			// lblMissile
			// 
			this.lblMissile.Location = new System.Drawing.Point(369, 283);
			this.lblMissile.Name = "lblMissile";
			this.lblMissile.Size = new System.Drawing.Size(61, 13);
			this.lblMissile.TabIndex = 87;
			this.lblMissile.Text = "?";
			this.lblMissile.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// lblReload
			// 
			this.lblReload.Location = new System.Drawing.Point(369, 270);
			this.lblReload.Name = "lblReload";
			this.lblReload.Size = new System.Drawing.Size(61, 13);
			this.lblReload.TabIndex = 86;
			this.lblReload.Text = "?";
			this.lblReload.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// lblRange
			// 
			this.lblRange.Location = new System.Drawing.Point(369, 257);
			this.lblRange.Name = "lblRange";
			this.lblRange.Size = new System.Drawing.Size(61, 13);
			this.lblRange.TabIndex = 85;
			this.lblRange.Text = "?";
			this.lblRange.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// lblDamage
			// 
			this.lblDamage.Location = new System.Drawing.Point(369, 244);
			this.lblDamage.Name = "lblDamage";
			this.lblDamage.Size = new System.Drawing.Size(61, 13);
			this.lblDamage.TabIndex = 84;
			this.lblDamage.Text = "?";
			this.lblDamage.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label17
			// 
			this.label17.AutoSize = true;
			this.label17.Location = new System.Drawing.Point(225, 283);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(44, 13);
			this.label17.TabIndex = 83;
			this.label17.Text = "Missile?";
			// 
			// label15
			// 
			this.label15.AutoSize = true;
			this.label15.Location = new System.Drawing.Point(225, 270);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(70, 13);
			this.label15.TabIndex = 82;
			this.label15.Text = "Reload Rate:";
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Location = new System.Drawing.Point(225, 257);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(42, 13);
			this.label11.TabIndex = 81;
			this.label11.Text = "Range:";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(224, 244);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(50, 13);
			this.label7.TabIndex = 80;
			this.label7.Text = "Damage:";
			// 
			// lblDescription
			// 
			this.lblDescription.Location = new System.Drawing.Point(225, 12);
			this.lblDescription.Name = "lblDescription";
			this.lblDescription.Size = new System.Drawing.Size(205, 73);
			this.lblDescription.TabIndex = 79;
			this.lblDescription.Text = "Component Description";
			// 
			// lblThrust
			// 
			this.lblThrust.Location = new System.Drawing.Point(369, 222);
			this.lblThrust.Name = "lblThrust";
			this.lblThrust.Size = new System.Drawing.Size(61, 13);
			this.lblThrust.TabIndex = 78;
			this.lblThrust.Text = "?";
			this.lblThrust.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// lblCrew
			// 
			this.lblCrew.Location = new System.Drawing.Point(370, 209);
			this.lblCrew.Name = "lblCrew";
			this.lblCrew.Size = new System.Drawing.Size(61, 13);
			this.lblCrew.TabIndex = 77;
			this.lblCrew.Text = "?";
			this.lblCrew.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// lblMass
			// 
			this.lblMass.Location = new System.Drawing.Point(370, 196);
			this.lblMass.Name = "lblMass";
			this.lblMass.Size = new System.Drawing.Size(61, 13);
			this.lblMass.TabIndex = 76;
			this.lblMass.Text = "?";
			this.lblMass.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.Location = new System.Drawing.Point(225, 222);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(40, 13);
			this.label12.TabIndex = 75;
			this.label12.Text = "Thrust:";
			// 
			// label13
			// 
			this.label13.AutoSize = true;
			this.label13.Location = new System.Drawing.Point(225, 209);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(34, 13);
			this.label13.TabIndex = 74;
			this.label13.Text = "Crew:";
			// 
			// label14
			// 
			this.label14.AutoSize = true;
			this.label14.Location = new System.Drawing.Point(225, 196);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(35, 13);
			this.label14.TabIndex = 73;
			this.label14.Text = "Mass:";
			// 
			// lblShields
			// 
			this.lblShields.Location = new System.Drawing.Point(370, 98);
			this.lblShields.Name = "lblShields";
			this.lblShields.Size = new System.Drawing.Size(61, 13);
			this.lblShields.TabIndex = 72;
			this.lblShields.Text = "?";
			this.lblShields.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label16
			// 
			this.label16.AutoSize = true;
			this.label16.Location = new System.Drawing.Point(225, 98);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(44, 13);
			this.label16.TabIndex = 71;
			this.label16.Text = "Shields:";
			// 
			// lblHitpoints
			// 
			this.lblHitpoints.Location = new System.Drawing.Point(370, 85);
			this.lblHitpoints.Name = "lblHitpoints";
			this.lblHitpoints.Size = new System.Drawing.Size(61, 13);
			this.lblHitpoints.TabIndex = 70;
			this.lblHitpoints.Text = "?";
			this.lblHitpoints.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label18
			// 
			this.label18.AutoSize = true;
			this.label18.Location = new System.Drawing.Point(225, 85);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(51, 13);
			this.label18.TabIndex = 69;
			this.label18.Text = "Hitpoints:";
			// 
			// ShopForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(466, 426);
			this.Controls.Add(this.lblSensor);
			this.Controls.Add(this.lblScanner);
			this.Controls.Add(this.label29);
			this.Controls.Add(this.label30);
			this.Controls.Add(this.lblEmissive);
			this.Controls.Add(this.lblPD);
			this.Controls.Add(this.lblEvasion);
			this.Controls.Add(this.label24);
			this.Controls.Add(this.label25);
			this.Controls.Add(this.label26);
			this.Controls.Add(this.lblMissile);
			this.Controls.Add(this.lblReload);
			this.Controls.Add(this.lblRange);
			this.Controls.Add(this.lblDamage);
			this.Controls.Add(this.label17);
			this.Controls.Add(this.label15);
			this.Controls.Add(this.label11);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.lblDescription);
			this.Controls.Add(this.lblThrust);
			this.Controls.Add(this.lblCrew);
			this.Controls.Add(this.lblMass);
			this.Controls.Add(this.label12);
			this.Controls.Add(this.label13);
			this.Controls.Add(this.label14);
			this.Controls.Add(this.lblShields);
			this.Controls.Add(this.label16);
			this.Controls.Add(this.lblHitpoints);
			this.Controls.Add(this.label18);
			this.Controls.Add(this.lblCost);
			this.Controls.Add(this.btnOurShip);
			this.Controls.Add(this.btnBuy);
			this.Controls.Add(this.lstComponents);
			this.Name = "ShopForm";
			this.Text = "Shop";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ListBox lstComponents;
		private System.Windows.Forms.Button btnBuy;
		private System.Windows.Forms.Button btnOurShip;
		private System.Windows.Forms.Label lblCost;
		private System.Windows.Forms.Label lblSensor;
		private System.Windows.Forms.Label lblScanner;
		private System.Windows.Forms.Label label29;
		private System.Windows.Forms.Label label30;
		private System.Windows.Forms.Label lblEmissive;
		private System.Windows.Forms.Label lblPD;
		private System.Windows.Forms.Label lblEvasion;
		private System.Windows.Forms.Label label24;
		private System.Windows.Forms.Label label25;
		private System.Windows.Forms.Label label26;
		private System.Windows.Forms.Label lblMissile;
		private System.Windows.Forms.Label lblReload;
		private System.Windows.Forms.Label lblRange;
		private System.Windows.Forms.Label lblDamage;
		private System.Windows.Forms.Label label17;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label lblDescription;
		private System.Windows.Forms.Label lblThrust;
		private System.Windows.Forms.Label lblCrew;
		private System.Windows.Forms.Label lblMass;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.Label lblShields;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.Label lblHitpoints;
		private System.Windows.Forms.Label label18;
	}
}