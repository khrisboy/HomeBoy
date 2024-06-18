/*
NPlot - A charting library for .NET

MenuForm.cs
Copyright (C) 2003 
Matt Howlett

Redistribution and use of NPlot or parts there-of in source and
binary forms, with or without modification, are permitted provided
that the following conditions are met:

1. Re-distributions in source form must retain at the head of each
   source file the above copyright notice, this list of conditions
   and the following disclaimer.

2. Any product ("the product") that makes use NPlot or parts 
   there-of must either:
  
    (a) allow any user of the product to obtain a complete machine-
        readable copy of the corresponding source code for the 
        product and the version of NPlot used for a charge no more
        than your cost of physically performing source distribution,
	on a medium customarily used for software interchange, or:

    (b) reproduce the following text in the documentation, about 
        box or other materials intended to be read by human users
        of the product that is provided to every human user of the
        product: 
   
              "This product includes software developed as 
              part of the NPlot library project available 
              from: http://www.nplot.com/" 

        The words "This product" may optionally be replace with 
        the actual name of the product.

------------------------------------------------------------------------

THIS SOFTWARE IS PROVIDED BY THE AUTHOR ``AS IS'' AND ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES
OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED.
IN NO EVENT SHALL THE AUTHOR BE LIABLE FOR ANY DIRECT, INDIRECT,
INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT
NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE,
DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY
THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
(INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF
THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

*/

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Threading;

namespace NPlotDemo
{
	/// <summary>
	/// Summary description for MenuForm.
	/// </summary>
	public class MenuForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button multiPlotDemoButton;
		private System.Windows.Forms.Button plotSurface2DDemoButton;
		private System.Windows.Forms.Button quitButton;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public MenuForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();		
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.multiPlotDemoButton = new System.Windows.Forms.Button();
			this.plotSurface2DDemoButton = new System.Windows.Forms.Button();
			this.quitButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// multiPlotDemoButton
			// 
			this.multiPlotDemoButton.Location = new System.Drawing.Point(8, 40);
			this.multiPlotDemoButton.Name = "multiPlotDemoButton";
			this.multiPlotDemoButton.Size = new System.Drawing.Size(136, 23);
			this.multiPlotDemoButton.TabIndex = 2;
			this.multiPlotDemoButton.Text = "Multi Plot Demo";
			this.multiPlotDemoButton.Click += new System.EventHandler(this.runDemoButton_Click);
			// 
			// plotSurface2DDemoButton
			// 
			this.plotSurface2DDemoButton.Location = new System.Drawing.Point(8, 13);
			this.plotSurface2DDemoButton.Name = "plotSurface2DDemoButton";
			this.plotSurface2DDemoButton.Size = new System.Drawing.Size(136, 23);
			this.plotSurface2DDemoButton.TabIndex = 3;
			this.plotSurface2DDemoButton.Text = "PlotSurface2D Demo";
			this.plotSurface2DDemoButton.Click += new System.EventHandler(this.plotSurface2DDemoButton_Click);
			// 
			// quitButton
			// 
			this.quitButton.Location = new System.Drawing.Point(40, 80);
			this.quitButton.Name = "quitButton";
			this.quitButton.TabIndex = 8;
			this.quitButton.Text = "Quit";
			this.quitButton.Click += new System.EventHandler(this.quitButton_Click);
			// 
			// MenuForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(152, 117);
			this.Controls.Add(this.quitButton);
			this.Controls.Add(this.plotSurface2DDemoButton);
			this.Controls.Add(this.multiPlotDemoButton);
			this.Name = "MenuForm";
			this.Text = "NPlot Demo";
			this.ResumeLayout(false);

		}
		#endregion

        [STAThread]
		static void Main() 
		{
			Application.Run(new MenuForm());
		}

		private System.Windows.Forms.Form displayForm_ = null;
		private void WindowThread()
		{
			displayForm_.ShowDialog();
		}

		private void runDemoButton_Click( object sender, System.EventArgs e )
		{
           
			displayForm_	=  new FinancialDemo();
			System.Threading.Thread t	=  new Thread( new ThreadStart(WindowThread) );
         t.SetApartmentState( ApartmentState.STA );
         t.Start();
        }

        private void plotSurface2DDemoButton_Click(object sender, System.EventArgs e)
		{
			displayForm_ = new PlotSurface2DDemo();
			System.Threading.Thread t = new Thread( new ThreadStart(WindowThread) );
         t.SetApartmentState( ApartmentState.STA ); // necessary for copy to clipboard to work.
         t.Start();
		}


		private void quitButton_Click(object sender, System.EventArgs e)
		{
			Application.Exit();
		}



	}
}
