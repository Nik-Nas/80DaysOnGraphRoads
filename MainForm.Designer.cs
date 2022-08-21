
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
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "MainForm";
            this.Text = "GraphRush";
            this.SizeChanged += new System.EventHandler(this.ResizeWindow);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnKeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.OnKeyUp);
            ((System.ComponentModel.ISupportInitialize) (this.FPSTimer)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Timer PhysicsTimer;
        
        private System.Windows.Forms.Timer InputTimer;

        private System.Timers.Timer FPSTimer;

        #endregion
    }
}

