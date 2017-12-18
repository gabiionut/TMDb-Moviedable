using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TMDbLib.Client;
using TMDbLib.Objects.Discover;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Search;

namespace TMDb___Moviedable
{
    public partial class Moviedable : Form
    {
        private static string apiKey = "1c4b19026060e9bc66fb5aabf4866e95";
        public static TMDbClient client = new TMDbClient(apiKey);
        ProcessStartInfo facebook = new ProcessStartInfo("https://www.facebook.com/pitigoigabi");
        ProcessStartInfo twitter = new ProcessStartInfo("https://twitter.com/pitigoiionut");
        ProcessStartInfo email = new ProcessStartInfo("pitigoiionutgabriel@gmail.com");
        ProcessStartInfo github = new ProcessStartInfo("https://github.com/gabiionut");





        public Moviedable()
        {
            InitializeComponent();
            panel6.SendToBack();
            Start();
            searchBox.Text = "Search Movie";
            searchBox.Enter += textBox1_Enter;
            searchBox.Leave += textBox1_Leave;
        }

        private void Moviedable_Load(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }


        private void button3_Click(object sender, EventArgs e)
        {
            panel6.SendToBack();
            panel6.Visible = false;
            panel5.BringToFront();
            getUpcoming();
            SidePanel.Height = upcoming.Height;
            SidePanel.Top = upcoming.Top;
        }

        private void button6_Click(object sender, EventArgs e)
        {

            Process.Start(facebook);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Process.Start(twitter);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Process.Start(github);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            panel4.SendToBack();
            if (searchBox.Text == "Search Movie")
            {
                searchBox.Text = "";
                searchBox.ForeColor = Color.Black;

            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (searchBox.Text == "")
            {
                searchBox.Text = "Search Movie";
                searchBox.ForeColor = Color.Gray;
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Search();
            }
        }

        private void Search()
        {
            if (!String.IsNullOrWhiteSpace(searchBox.Text))
            {

                panel6.SendToBack();
                panel6.Visible = false;
                panel5.BringToFront();

                ClearPictureBox();

                SearchContainer<SearchCollection> colection = client.SearchCollectionAsync(searchBox.Text).Result;


                var m = 0;
                foreach (var pictureBox in panel5.Controls.OfType<PictureBox>())
                {
                    if (m + 1 > colection.Results.Count)
                    {
                        break;
                    }

                    var movie = client.GetCollectionAsync(colection.Results[m].Id).Result;
                    pictureBox.Visible = true;
                    client.GetConfig();

                    pictureBox.ImageLocation = client.GetImageUrl("w150", movie.PosterPath, false).AbsoluteUri;
                    pictureBox.Text = movie.Id.ToString();
                    if (pictureBox.Image is System.Drawing.Bitmap)
                    {
                        m++;
                    }
                    m++;
                }
            }
        }

        private void Start()
        {
            InTheaters();
        }

        private void ClearPictureBox()
        {
            foreach (var pictureBox in panel5.Controls.OfType<PictureBox>())
            {
                pictureBox.Visible = false;
            }
        }

        private void InTheaters()
        {
            var disc = client.DiscoverMoviesAsync().WherePrimaryReleaseDateIsBefore(DateTime.Today)
                .WherePrimaryReleaseDateIsAfter(DateTime.Today.Subtract(new TimeSpan(7, 0, 0, 0)));

            var colection = disc.Query();
            client.GetConfig();

            var m = 0;
            foreach (var p in panel5.Controls.OfType<PictureBox>())
            {
                if (m + 1 > colection.Result.Results.Count)
                {
                    break;
                }

                p.Visible = true;
                client.GetConfig();
                p.Text = colection.Result.Results[m].Id.ToString();
                p.ImageLocation = client.GetImageUrl("w150", colection.Result.Results[m].PosterPath, false).AbsoluteUri;
                
                if (p.Image is System.Drawing.Bitmap)
                {
                    m++;
                }
                m++;
            }
        }

        private void Popular()
        {
            var disc = client.DiscoverMoviesAsync().OrderBy(DiscoverMovieSortBy.PopularityDesc);

            var colection1 = disc.Query();
            client.GetConfig();

            var m = 0;
            foreach (var p in panel5.Controls.OfType<PictureBox>())
            {
                if (m + 1 > colection1.Result.Results.Count)
                {
                    break;
                }

                p.Visible = true;
                client.GetConfig();

                p.ImageLocation = client.GetImageUrl("w150", colection1.Result.Results[m].PosterPath, false).AbsoluteUri;
                p.Text = colection1.Result.Results[m].Id.ToString();
                if (p.Image is System.Drawing.Bitmap)
                {
                    m++;
                }
                m++;
            }
        }

        private void home_Click(object sender, EventArgs e)
        {
            panel6.SendToBack();
            panel5.BringToFront();
            panel6.Visible = false;
            InTheaters();
            SidePanel.Height = home.Height;
            SidePanel.Top = home.Top;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel6.SendToBack();
            panel6.Visible = false;
            panel5.BringToFront();
            Popular();
            SidePanel.Height = button1.Height;
            SidePanel.Top = button1.Top;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel6.SendToBack();
            panel6.Visible = false;
            panel5.BringToFront();
            getTopRated();
            SidePanel.Height = topRated.Height;
            SidePanel.Top = topRated.Top;
        }

        private void getTopRated()
        {
            var disc = client.DiscoverMoviesAsync().OrderBy(DiscoverMovieSortBy.VoteAverageDesc);

            var colection1 = disc.Query();
            client.GetConfig();

            var m = 0;
            foreach (var p in panel5.Controls.OfType<PictureBox>())
            {
                if (m + 1 > colection1.Result.Results.Count)
                {
                    break;
                }

                p.Visible = true;
                client.GetConfig();

                p.ImageLocation = client.GetImageUrl("w150", colection1.Result.Results[m].PosterPath, false).AbsoluteUri;
                p.Text = colection1.Result.Results[m].Id.ToString();
                if (p.Image is System.Drawing.Bitmap)
                {
                    m++;
                }
                m++;
            }
        }

        private void getUpcoming()
        {
            var colection1 = client.GetMovieUpcomingListAsync().Result;
            client.GetConfig();

            var m = 0;
            foreach (var p in panel5.Controls.OfType<PictureBox>())
            {
                if (m + 1 > colection1.Results.Count)
                {
                    break;
                }

                p.Visible = true;
                client.GetConfig();

                p.ImageLocation = client.GetImageUrl("w150", colection1.Results[m].PosterPath, false).AbsoluteUri;
                p.Text = colection1.Results[m].Id.ToString();
                if (p.Image is System.Drawing.Bitmap)
                {
                    m++;
                }
                m++;
            }
        }

        // Move window
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == WM_NCHITTEST)
                m.Result = (IntPtr)(HT_CAPTION);
        }

        private const int WM_NCHITTEST = 0x84;
        private const int HT_CLIENT = 0x1;
        private const int HT_CAPTION = 0x2;

        private void pictureBox1_Click_4(object sender, EventArgs e)
        {
            Details((PictureBox) sender);
        }

        private void Details(PictureBox poster)
        {

            var movie = Moviedable.client.GetMovieAsync(Int32.Parse(poster.Text));
            movieTitle.Text = movie.Result.Title;
            label1.Text = movie.Result.Overview;
            pictureBox9.ImageLocation = Moviedable.client.GetImageUrl("w150", movie.Result.PosterPath, false).AbsoluteUri;
            label2.Text = "Note: " + movie.Result.VoteAverage.ToString();
            panel6.Visible = true;
            panel6.BringToFront();

        }

        private void pictureBox2_Click_1(object sender, EventArgs e)
        {
            Details((PictureBox)sender);

        }

        private void pictureBox3_Click_1(object sender, EventArgs e)
        {
            Details((PictureBox)sender);

        }

        private void pictureBox4_Click_1(object sender, EventArgs e)
        {
            Details((PictureBox)sender);

        }

        private void pictureBox5_Click_1(object sender, EventArgs e)
        {
            Details((PictureBox)sender);

        }

        private void pictureBox6_Click_1(object sender, EventArgs e)
        {
            Details((PictureBox)sender);

        }

        private void pictureBox7_Click_1(object sender, EventArgs e)
        {
            Details((PictureBox)sender);

        }

        private void pictureBox8_Click_1(object sender, EventArgs e)
        {
            Details((PictureBox)sender);

        }
    }

}
