/*
 * A tiny replacement app for Market watch [ Forex trading ]
 * 
 * Created by Ramtin Jokar [ Ramtinak@live.com ]
 * 
 * License: MIT
 * 
 */

using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MarketWatch
{
    public partial class Form1 : Form
    {
        #region Fields
        readonly Timer Timer = new Timer();
        readonly TimeZoneInfo Timezone = TimeZoneInfo.FindSystemTimeZoneById("Israel Standard Time");
        bool IsBlack = true;
        bool Clicked = false;
        #endregion Fields

        #region Constructor

        public Form1()
        {
            InitializeComponent();
            Load += FormLoad;
            FormClosing += FormFormClosing;
            Timer.Interval = 100;
            Timer.Tick += TimerTick;
            CheckForIllegalCrossThreadCalls = false;
            MouseDown += FormMouseDown;
            TimeLabel.MouseDown += FormMouseDown;
           
            StartPosition = FormStartPosition.Manual;

            Location = new Point((Screen.PrimaryScreen.WorkingArea.Width / 2) - (Width / 2),
                                   0);
        }

        #endregion Constructor

        #region Event handlers

        private void FormFormClosing(object sender, FormClosingEventArgs e)
        {
            Timer.Stop();
        }

        private void FormLoad(object sender, EventArgs e)
        {
            TopMost = true;
            TopLevel = true;
            Timer.Start();
        }

        private async void FormMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
                // why this?>
                // because Mouse click, double click won't work for me so
                // I created a double click option by myself
                if (Clicked)
                {
                    if (!IsBlack)
                    {
                        XCancelButton.BackColor = BackColor = Color.Black;
                        TimeLabel.ForeColor = Color.White;
                        XCancelButton.ForeColor = Color.Silver;
                    }
                    else
                    {
                        XCancelButton.BackColor = BackColor = Color.White;
                        TimeLabel.ForeColor = Color.Black;
                        XCancelButton.ForeColor = Color.Black;
                    }
                    IsBlack = !IsBlack;
                }
                Clicked = true;
                await Task.Delay(100);
                Clicked = false;
            }
        }

        private void XCancelButtonClick(object sender, EventArgs e)
        {
            try
            {
                Application.ExitThread();
                Application.Exit();
            }
            catch { }
        }
        private void TimerTick(object sender, EventArgs e)
        {
            try
            {
                var date = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, Timezone);
                TimeLabel.Text = date.ToString("HH:mm:ss");
            }
            catch { }
        }

        #endregion Event handlers

        #region DllImport

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        #endregion DllImport
    }
}
