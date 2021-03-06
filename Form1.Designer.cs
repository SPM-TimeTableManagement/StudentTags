﻿namespace StudentTagsSprint1
{
    partial class Form1
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
            this.panelMenu = new System.Windows.Forms.Panel();
            this.buttonNotAvailable = new System.Windows.Forms.Button();
            this.btnTag = new System.Windows.Forms.Button();
            this.panelSubStuMenu = new System.Windows.Forms.Panel();
            this.btnSubGroup = new System.Windows.Forms.Button();
            this.btnStuBatch = new System.Windows.Forms.Button();
            this.btnStuManage = new System.Windows.Forms.Button();
            this.btnProg = new System.Windows.Forms.Button();
            this.btnStu = new System.Windows.Forms.Button();
            this.panelForm1 = new System.Windows.Forms.Panel();
            this.buttonConsecutive = new System.Windows.Forms.Button();
            this.buttonOverlap = new System.Windows.Forms.Button();
            this.buttonParallel = new System.Windows.Forms.Button();
            this.panelMenu.SuspendLayout();
            this.panelSubStuMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMenu
            // 
            this.panelMenu.AutoScroll = true;
            this.panelMenu.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.panelMenu.Controls.Add(this.buttonParallel);
            this.panelMenu.Controls.Add(this.buttonOverlap);
            this.panelMenu.Controls.Add(this.buttonConsecutive);
            this.panelMenu.Controls.Add(this.buttonNotAvailable);
            this.panelMenu.Controls.Add(this.btnTag);
            this.panelMenu.Controls.Add(this.panelSubStuMenu);
            this.panelMenu.Controls.Add(this.btnStu);
            this.panelMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelMenu.Location = new System.Drawing.Point(0, 0);
            this.panelMenu.Name = "panelMenu";
            this.panelMenu.Size = new System.Drawing.Size(221, 689);
            this.panelMenu.TabIndex = 0;
            // 
            // buttonNotAvailable
            // 
            this.buttonNotAvailable.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonNotAvailable.Location = new System.Drawing.Point(0, 409);
            this.buttonNotAvailable.Name = "buttonNotAvailable";
            this.buttonNotAvailable.Size = new System.Drawing.Size(221, 70);
            this.buttonNotAvailable.TabIndex = 8;
            this.buttonNotAvailable.Text = "Manage Not Available";
            this.buttonNotAvailable.UseVisualStyleBackColor = true;
            this.buttonNotAvailable.Click += new System.EventHandler(this.buttonNotAvailable_Click);
            // 
            // btnTag
            // 
            this.btnTag.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnTag.Location = new System.Drawing.Point(0, 339);
            this.btnTag.Name = "btnTag";
            this.btnTag.Size = new System.Drawing.Size(221, 70);
            this.btnTag.TabIndex = 7;
            this.btnTag.Text = "Manage Tags";
            this.btnTag.UseVisualStyleBackColor = true;
            this.btnTag.Click += new System.EventHandler(this.btnTag_Click);
            // 
            // panelSubStuMenu
            // 
            this.panelSubStuMenu.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.panelSubStuMenu.Controls.Add(this.btnSubGroup);
            this.panelSubStuMenu.Controls.Add(this.btnStuBatch);
            this.panelSubStuMenu.Controls.Add(this.btnStuManage);
            this.panelSubStuMenu.Controls.Add(this.btnProg);
            this.panelSubStuMenu.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSubStuMenu.Location = new System.Drawing.Point(0, 65);
            this.panelSubStuMenu.Name = "panelSubStuMenu";
            this.panelSubStuMenu.Size = new System.Drawing.Size(221, 274);
            this.panelSubStuMenu.TabIndex = 1;
            // 
            // btnSubGroup
            // 
            this.btnSubGroup.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnSubGroup.Location = new System.Drawing.Point(0, 198);
            this.btnSubGroup.Name = "btnSubGroup";
            this.btnSubGroup.Size = new System.Drawing.Size(221, 68);
            this.btnSubGroup.TabIndex = 7;
            this.btnSubGroup.Text = "Edit Sub Groups";
            this.btnSubGroup.UseVisualStyleBackColor = true;
            this.btnSubGroup.Click += new System.EventHandler(this.btnSubGroup_Click);
            // 
            // btnStuBatch
            // 
            this.btnStuBatch.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnStuBatch.Location = new System.Drawing.Point(0, 130);
            this.btnStuBatch.Name = "btnStuBatch";
            this.btnStuBatch.Size = new System.Drawing.Size(221, 68);
            this.btnStuBatch.TabIndex = 6;
            this.btnStuBatch.Text = "Student List";
            this.btnStuBatch.UseVisualStyleBackColor = true;
            this.btnStuBatch.Click += new System.EventHandler(this.btnStuBatch_Click);
            // 
            // btnStuManage
            // 
            this.btnStuManage.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnStuManage.Location = new System.Drawing.Point(0, 65);
            this.btnStuManage.Name = "btnStuManage";
            this.btnStuManage.Size = new System.Drawing.Size(221, 65);
            this.btnStuManage.TabIndex = 5;
            this.btnStuManage.Text = "Manage Student Batch";
            this.btnStuManage.UseVisualStyleBackColor = true;
            this.btnStuManage.Click += new System.EventHandler(this.btnStuManage_Click);
            // 
            // btnProg
            // 
            this.btnProg.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnProg.Location = new System.Drawing.Point(0, 0);
            this.btnProg.Name = "btnProg";
            this.btnProg.Size = new System.Drawing.Size(221, 65);
            this.btnProg.TabIndex = 4;
            this.btnProg.Text = "Programme Manage";
            this.btnProg.UseVisualStyleBackColor = true;
            this.btnProg.Click += new System.EventHandler(this.btnProg_Click);
            // 
            // btnStu
            // 
            this.btnStu.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnStu.Location = new System.Drawing.Point(0, 0);
            this.btnStu.Name = "btnStu";
            this.btnStu.Size = new System.Drawing.Size(221, 65);
            this.btnStu.TabIndex = 3;
            this.btnStu.Text = "Student Batch and Programme";
            this.btnStu.UseVisualStyleBackColor = true;
            this.btnStu.Click += new System.EventHandler(this.btnStu_Click);
            // 
            // panelForm1
            // 
            this.panelForm1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelForm1.Location = new System.Drawing.Point(227, 12);
            this.panelForm1.Name = "panelForm1";
            this.panelForm1.Size = new System.Drawing.Size(1080, 665);
            this.panelForm1.TabIndex = 1;
            // 
            // buttonConsecutive
            // 
            this.buttonConsecutive.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonConsecutive.Location = new System.Drawing.Point(0, 479);
            this.buttonConsecutive.Name = "buttonConsecutive";
            this.buttonConsecutive.Size = new System.Drawing.Size(221, 70);
            this.buttonConsecutive.TabIndex = 9;
            this.buttonConsecutive.Text = "Manage Consecutive Sessions";
            this.buttonConsecutive.UseVisualStyleBackColor = true;
            this.buttonConsecutive.Click += new System.EventHandler(this.buttonConsecutive_Click);
            // 
            // buttonOverlap
            // 
            this.buttonOverlap.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonOverlap.Location = new System.Drawing.Point(0, 549);
            this.buttonOverlap.Name = "buttonOverlap";
            this.buttonOverlap.Size = new System.Drawing.Size(221, 70);
            this.buttonOverlap.TabIndex = 10;
            this.buttonOverlap.Text = "Manage Overlapping Sessions";
            this.buttonOverlap.UseVisualStyleBackColor = true;
            this.buttonOverlap.Click += new System.EventHandler(this.buttonOverlap_Click);
            // 
            // buttonParallel
            // 
            this.buttonParallel.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonParallel.Location = new System.Drawing.Point(0, 619);
            this.buttonParallel.Name = "buttonParallel";
            this.buttonParallel.Size = new System.Drawing.Size(221, 70);
            this.buttonParallel.TabIndex = 11;
            this.buttonParallel.Text = "Manage Parallel Sessions";
            this.buttonParallel.UseVisualStyleBackColor = true;
            this.buttonParallel.Click += new System.EventHandler(this.buttonParallel_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1309, 689);
            this.Controls.Add(this.panelForm1);
            this.Controls.Add(this.panelMenu);
            this.Name = "Form1";
            this.Text = "Form1";
            this.panelMenu.ResumeLayout(false);
            this.panelSubStuMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelMenu;
        private System.Windows.Forms.Button btnStu;
        private System.Windows.Forms.Panel panelSubStuMenu;
        private System.Windows.Forms.Button btnStuBatch;
        private System.Windows.Forms.Button btnStuManage;
        private System.Windows.Forms.Button btnProg;
        private System.Windows.Forms.Button btnTag;
        private System.Windows.Forms.Panel panelForm1;
        private System.Windows.Forms.Button btnSubGroup;
        private System.Windows.Forms.Button buttonNotAvailable;
        private System.Windows.Forms.Button buttonConsecutive;
        private System.Windows.Forms.Button buttonOverlap;
        private System.Windows.Forms.Button buttonParallel;
    }
}

