namespace HDCensorship.UI
{
    partial class TimeLine
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnRuler = new System.Windows.Forms.Panel();
            this.pnClip = new System.Windows.Forms.Panel();
            this.hScroll = new System.Windows.Forms.HScrollBar();
            this.pnRecord = new System.Windows.Forms.Panel();
            this.pnPlay = new System.Windows.Forms.Panel();
            this.pnPreview = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // pnRuler
            // 
            this.pnRuler.BackColor = System.Drawing.Color.Gainsboro;
            this.pnRuler.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnRuler.Location = new System.Drawing.Point(0, 0);
            this.pnRuler.Name = "pnRuler";
            this.pnRuler.Size = new System.Drawing.Size(809, 32);
            this.pnRuler.TabIndex = 0;
            this.pnRuler.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnRuler_MouseMove);
            this.pnRuler.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pnRuler_MouseUp);
            // 
            // pnClip
            // 
            this.pnClip.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)));
            this.pnClip.BackColor = System.Drawing.Color.Silver;
            this.pnClip.Location = new System.Drawing.Point(0, 32);
            this.pnClip.Name = "pnClip";
            this.pnClip.Size = new System.Drawing.Size(21, 43);
            this.pnClip.TabIndex = 1;
            // 
            // hScroll
            // 
            this.hScroll.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.hScroll.LargeChange = 1;
            this.hScroll.Location = new System.Drawing.Point(0, 75);
            this.hScroll.Maximum = 1000;
            this.hScroll.Name = "hScroll";
            this.hScroll.Size = new System.Drawing.Size(809, 17);
            this.hScroll.TabIndex = 2;
            this.hScroll.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScroll_Scroll);
            // 
            // pnRecord
            // 
            this.pnRecord.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)));
            this.pnRecord.BackColor = System.Drawing.Color.Red;
            this.pnRecord.Location = new System.Drawing.Point(294, 22);
            this.pnRecord.Name = "pnRecord";
            this.pnRecord.Size = new System.Drawing.Size(3, 53);
            this.pnRecord.TabIndex = 3;
            // 
            // pnPlay
            // 
            this.pnPlay.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)));
            this.pnPlay.BackColor = System.Drawing.Color.Teal;
            this.pnPlay.Location = new System.Drawing.Point(216, 22);
            this.pnPlay.Name = "pnPlay";
            this.pnPlay.Size = new System.Drawing.Size(3, 53);
            this.pnPlay.TabIndex = 4;
            // 
            // pnPreview
            // 
            this.pnPreview.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)));
            this.pnPreview.BackColor = System.Drawing.Color.Navy;
            this.pnPreview.Location = new System.Drawing.Point(240, 22);
            this.pnPreview.Name = "pnPreview";
            this.pnPreview.Size = new System.Drawing.Size(3, 53);
            this.pnPreview.TabIndex = 5;
            // 
            // TimeLine
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.pnPreview);
            this.Controls.Add(this.pnPlay);
            this.Controls.Add(this.pnRecord);
            this.Controls.Add(this.hScroll);
            this.Controls.Add(this.pnClip);
            this.Controls.Add(this.pnRuler);
            this.MinimumSize = new System.Drawing.Size(200, 50);
            this.Name = "TimeLine";
            this.Size = new System.Drawing.Size(809, 92);
            this.SizeChanged += new System.EventHandler(this.TimeLine2_SizeChanged);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnRuler;
        private System.Windows.Forms.Panel pnClip;
        private System.Windows.Forms.HScrollBar hScroll;
        private System.Windows.Forms.Panel pnRecord;
        private System.Windows.Forms.Panel pnPlay;
        private System.Windows.Forms.Panel pnPreview;


    }
}
