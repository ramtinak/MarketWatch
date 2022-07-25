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
using System.IO;
using System.Diagnostics;

namespace MarketWatch
{
    public partial class Form1 : Form
    {
        #region Fields
        private readonly Timer Timer = new Timer();
        private readonly TimeZoneInfo Timezone = TimeZoneInfo.FindSystemTimeZoneById("FLE Standard Time");
        private bool Clicked = false;
        //private readonly Color White = Color.White;
        private readonly Color White = Color.FromArgb(255, 240, 240, 240);
        private const int Black = -16777216;
        private bool IsBlack = true;
        #endregion Fields

        #region Constructor

        public Form1()
        {
            InitializeComponent();
            TimeZoneInfo Timezone = TimeZoneInfo.FindSystemTimeZoneById("FLE Standard Time");
            Load += FormLoad;
            FormClosing += FormFormClosing;
            Timer.Interval = 1;
            Timer.Tick += TimerTick;
            CheckForIllegalCrossThreadCalls = false;
            MouseDown += FormMouseDown;
            TimeLabel.MouseDown += FormMouseDown;
            var rcr = CreateRoundRectRgn(0, 0, Width, Height, 0, 0);
            if (rcr != null)
                Region = Region.FromHrgn(rcr);

            StartPosition = FormStartPosition.Manual;

            Location = new Point((Screen.PrimaryScreen.WorkingArea.Width / 2) -
                (Width / 2), 0);
        }

        #endregion Constructor

        #region Event handlers

        private void FormFormClosing(object sender, FormClosingEventArgs e)
        {
            Timer.Stop();
            SaveState();
        }

        private void FormLoad(object sender, EventArgs e)
        {
            TopMost = true;
            TopLevel = true;
            Timer.Start(); 
            LoadState();
            SyncTime();
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
                    ChangeTheme();
                }
                Clicked = true;
                await Task.Delay(150);
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
            catch (Exception) { }
        }
        private void TimerTick(object sender, EventArgs e)
        {
            try
            {
                var date = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, Timezone);
                TimeLabel.Text = date.ToString("HH:mm:ss");
            }
            catch (Exception) { }
        }
        private void ChangeTheme()
        {
            if (!IsBlack)
            {
                XCancelButton.BackColor = BackColor = Color.Black;
                TimeLabel.ForeColor = White;
                XCancelButton.ForeColor = Color.Silver;
                IsBlack = true;
            }
            else
            {
                XCancelButton.BackColor = BackColor = White;
                TimeLabel.ForeColor = Color.Black;
                XCancelButton.ForeColor = Color.Black;
                IsBlack = false;
            }
        }
        #endregion Event handlers

        #region Theme Save
        static readonly string State = Path.Combine(Path.GetTempPath(), "MarketWatchStatX.dat");

        private void LoadState()
        {
            if (File.Exists(State))
            {
                try
                {
                    var splitter = File.ReadAllText(State).Split(',');
                    if (splitter.Length < 2) return;
                    IsBlack = int.Parse(splitter[0]) == Black;
                    XCancelButton.BackColor = BackColor = Color.FromArgb(int.Parse(splitter[0]));
                    TimeLabel.ForeColor = Color.FromArgb(int.Parse(splitter[1]));
                    XCancelButton.ForeColor = Color.FromArgb(int.Parse(splitter[2]));
                }
                catch (Exception){ }
            }
        }
        private void SaveState()
        {
            try
            {
                var state = $"{BackColor.ToArgb()},{TimeLabel.ForeColor.ToArgb()},{XCancelButton.ForeColor.ToArgb()}";
                File.WriteAllText(State, state);
            }
            catch (Exception) { }
        }
        #endregion Theme Save

        #region Run Module
        async void SyncTime()
        {
            RunCommand("net stop w32time");
            await Task.Delay(300);
            RunCommand("w32tm /unregister");
            await Task.Delay(300);
            RunCommand("w32tm /register");
            await Task.Delay(300);
            RunCommand("net start w32time", 1500);
            await Task.Delay(1000);
            RunCommand("w32tm /resync", 1000);
            //RunCommand("");
            //            startInfo.Arguments = @"/c net stop w32time

            //w32tm /unregister

            //w32tm /register

            //net start w32time

            //w32tm /resync
            //";
        }
        async void RunCommand(string cmd, int delay = 300)
        {
            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                WindowStyle = ProcessWindowStyle.Hidden,
                FileName = "cmd.exe",
                Arguments = @"/c " + cmd
            };
            process.StartInfo = startInfo;
            process.Start();
            try
            {
                await Task.Delay(delay);
                //process.WaitForExit();
                process.Close(); 
            }
            catch (Exception) { }
        }
        #endregion Run Module

        #region DllImport

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,     // x-coordinate of upper-left corner
            int nTopRect,      // y-coordinate of upper-left corner
            int nRightRect,    // x-coordinate of lower-right corner
            int nBottomRect,   // y-coordinate of lower-right corner
            int nWidthEllipse, // width of ellipse
            int nHeightEllipse // height of ellipse
        );

        #endregion DllImport
    }
}
