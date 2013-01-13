﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Windows_qPaste
{
    public partial class ToastForm : Form
    {
        private const int SW_SHOWNA = 8;
        [DllImport("user32", CharSet = CharSet.Auto)]
        private extern static int ShowWindow(IntPtr hWnd, int nCmdShow);

        public ToastForm()
        {
            InitializeComponent();
            TopMost = false;
            //ShowDialog();
        }

        private static ToastForm instance;
        protected override bool ShowWithoutActivation
        {
            get { return true; }
        }

        public static void View()
        {
            if (instance == null)
            {
                new Thread(new ThreadStart(() => { instance = null; instance = new ToastForm(); instance.ShowDialog(); })).Start();
            }

            /*instance.Invoke(new Action(() => 
            {
                instance.Show();
            }));*/
        }

        public static void DontView()
        {
            if (instance != null)
            {
                try
                {
                    instance.Invoke(new Action(() =>
                    {
                        instance.Close();
                        instance.Dispose();
                    }));
                }
                catch (InvalidOperationException e)
                {
                    Debug.WriteLine("Exception: " + e.ToString());
                }
            }
        }

        private void ToastForm_Load(object sender, EventArgs e)
        {
            Width = 135;
            Height = 30;
            Location = new Point(Screen.PrimaryScreen.WorkingArea.Width - Width, Screen.PrimaryScreen.WorkingArea.Height - Height);
        }
    }
}
