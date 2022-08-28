
namespace ITCampFinalProject
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.FPSTimer = new System.Timers.Timer();
            this.InputTimer = new System.Windows.Forms.Timer(this.components);
            this.PhysicsTimer = new System.Windows.Forms.Timer(this.components);
            this.addNodeButton = new System.Windows.Forms.Button();
            this.addEdgeButton = new System.Windows.Forms.Button();
            this.HintLabel = new System.Windows.Forms.Label();
            this.SolveButton = new System.Windows.Forms.Button();
            this.deleteNodeButton = new System.Windows.Forms.Button();
            this.ResetButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize) (this.FPSTimer)).BeginInit();
            this.SuspendLayout();
            // 
            // FPSTimer
            // 
            this.FPSTimer.Interval = 16D;
            this.FPSTimer.SynchronizingObject = this;
            this.FPSTimer.Elapsed += new System.Timers.ElapsedEventHandler(this.FpsTick);
            // 
            // InputTimer
            // 
            this.InputTimer.Interval = 25;
            this.InputTimer.Tick += new System.EventHandler(this.InputTimerTick);
            // 
            // addNodeButton
            // 
            this.addNodeButton.Location = new System.Drawing.Point(12, 12);
            this.addNodeButton.Name = "addNodeButton";
            this.addNodeButton.Size = new System.Drawing.Size(92, 34);
            this.addNodeButton.TabIndex = 0;
            this.addNodeButton.TabStop = false;
            this.addNodeButton.Text = "Add Node";
            this.addNodeButton.UseVisualStyleBackColor = true;
            this.addNodeButton.Click += new System.EventHandler(this.AddNodeButton_Click);
            // 
            // addEdgeButton
            // 
            this.addEdgeButton.Location = new System.Drawing.Point(217, 12);
            this.addEdgeButton.Name = "addEdgeButton";
            this.addEdgeButton.Size = new System.Drawing.Size(87, 34);
            this.addEdgeButton.TabIndex = 0;
            this.addEdgeButton.TabStop = false;
            this.addEdgeButton.Text = "Add Edge";
            this.addEdgeButton.UseVisualStyleBackColor = true;
            this.addEdgeButton.Click += new System.EventHandler(this.AddEdgeButton_Click);
            // 
            // HintLabel
            // 
            this.HintLabel.BackColor = System.Drawing.SystemColors.Control;
            this.HintLabel.Location = new System.Drawing.Point(618, 12);
            this.HintLabel.Name = "HintLabel";
            this.HintLabel.Size = new System.Drawing.Size(376, 34);
            this.HintLabel.TabIndex = 1;
            // 
            // SolveButton
            // 
            this.SolveButton.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.SolveButton.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.SolveButton.Location = new System.Drawing.Point(310, 12);
            this.SolveButton.Name = "SolveButton";
            this.SolveButton.Size = new System.Drawing.Size(110, 33);
            this.SolveButton.TabIndex = 0;
            this.SolveButton.TabStop = false;
            this.SolveButton.Text = "Solve Graph!";
            this.SolveButton.UseVisualStyleBackColor = false;
            this.SolveButton.Click += new System.EventHandler(this.SolveButton_Click);
            // 
            // deleteNodeButton
            // 
            this.deleteNodeButton.Enabled = false;
            this.deleteNodeButton.Location = new System.Drawing.Point(110, 12);
            this.deleteNodeButton.Name = "deleteNodeButton";
            this.deleteNodeButton.Size = new System.Drawing.Size(101, 34);
            this.deleteNodeButton.TabIndex = 0;
            this.deleteNodeButton.TabStop = false;
            this.deleteNodeButton.Text = "Delete Node";
            this.deleteNodeButton.UseVisualStyleBackColor = true;
            this.deleteNodeButton.Click += new System.EventHandler(this.DeleteNodeButton_Click);
            // 
            // ResetButton
            // 
            this.ResetButton.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ResetButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ResetButton.Location = new System.Drawing.Point(426, 12);
            this.ResetButton.Name = "ResetButton";
            this.ResetButton.Size = new System.Drawing.Size(78, 33);
            this.ResetButton.TabIndex = 0;
            this.ResetButton.TabStop = false;
            this.ResetButton.Text = "Reset";
            this.ResetButton.UseVisualStyleBackColor = false;
            this.ResetButton.Click += new System.EventHandler(this.ResetButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1006, 556);
            this.Controls.Add(this.ResetButton);
            this.Controls.Add(this.SolveButton);
            this.Controls.Add(this.HintLabel);
            this.Controls.Add(this.addEdgeButton);
            this.Controls.Add(this.deleteNodeButton);
            this.Controls.Add(this.addNodeButton);
            this.Name = "MainForm";
            this.Text = "GraphRush";
            this.SizeChanged += new System.EventHandler(this.ResizeWindow);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnKeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.OnKeyUp);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseUp);
            ((System.ComponentModel.ISupportInitialize) (this.FPSTimer)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Button ResetButton;

        private System.Windows.Forms.Button SolveButton;

        private System.Windows.Forms.Label HintLabel;

        private System.Windows.Forms.Button addNodeButton;

        private System.Windows.Forms.Button deleteNodeButton;
        private System.Windows.Forms.Button addEdgeButton;

        private System.Windows.Forms.Timer PhysicsTimer;
        
        private System.Windows.Forms.Timer InputTimer;

        private System.Timers.Timer FPSTimer;

        #endregion
    }
}

