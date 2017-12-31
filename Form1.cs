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
using TMDbLib.Objects.Movies;
using TMDbLib.Objects.Search;

namespace TMDb___Moviedable
{
    public partial class Moviedable : Form
    {
        private static string apiKey = "1c4b19026060e9bc66fb5aabf4866e95";
        public static TMDbClient client = new TMDbClient(apiKey);
        private readonly Dictionary<string, string> genres = new Dictionary<string, string>();
        ProcessStartInfo facebook = new ProcessStartInfo("https://www.facebook.com/pitigoigabi");
        ProcessStartInfo twitter = new ProcessStartInfo("https://twitter.com/pitigoiionut");
        ProcessStartInfo github = new ProcessStartInfo("https://github.com/gabiionut/TMDb-Moviedable");

        public Moviedable()
        {
            InitializeComponent();
            InitDictionary();
            panel6.SendToBack();
            InTheaters();
            searchBox.Text = "Search Movie";
            searchBox.Enter += textBox1_Enter;
            searchBox.Leave += textBox1_Leave;
            panel6.Visible = false;
        }

        private void InitDictionary()
        {
            genres.Add("Family", "https://i.imgur.com/5pqj3qN.png");
            genres.Add("Music", "https://i.imgur.com/jfUlNX1.png");
            genres.Add("History", "https://i.imgur.com/VY7vbI9.png");
            genres.Add("Crime", "https://i.imgur.com/wvEN58f.png");
            genres.Add("Horror", "https://i.imgur.com/99zSmN8.png");
            genres.Add("Science Fiction", "https://i.imgur.com/E4kW25U.png");
            genres.Add("Thriller", "https://i.imgur.com/WJXb2oy.png");
            genres.Add("Western", "https://i.imgur.com/4kt4Dqz.png");
            genres.Add("Documentary", "https://i.imgur.com/uR2cKsq.png");
            genres.Add("War", "https://i.imgur.com/jJHu6Ft.png");
            genres.Add("Action", "https://i.imgur.com/uNhutiK.png");
            genres.Add("Animation", "https://i.imgur.com/6WLMxtl.png");
            genres.Add("Fantasy", "https://i.imgur.com/X6OfpIH.png");
            genres.Add("Adventure", "https://i.imgur.com/s4lI3Y2.png");
            genres.Add("Comedy", "https://i.imgur.com/icrDmFe.png");
            genres.Add("Drama", "https://i.imgur.com/tqjZ4PJ.png");
            genres.Add("Mystery", "https://i.imgur.com/icrDmFe.png");
            genres.Add("Romance", "https://i.imgur.com/eov3D20.png");
        }

        private void Search()
        {
            if (!String.IsNullOrWhiteSpace(searchBox.Text))
            {
                ClearPictureBox();

                var collection = client.SearchCollectionAsync(searchBox.Text).Result;

                var index = 0;
                foreach (var pictureBox in panel5.Controls.OfType<PictureBox>())
                {
                    if (index + 1 > collection.Results.Count)
                    {
                        break;
                    }

                    var movie = client.GetCollectionAsync(collection.Results[index].Id).Result;
                    pictureBox.Visible = true;
                    client.GetConfig();

                    pictureBox.Text = movie.Id.ToString();
                    pictureBox.ImageLocation = client.GetImageUrl("w150", movie.PosterPath, false).AbsoluteUri;
                    if (pictureBox.Image is System.Drawing.Bitmap)
                    {
                        index++;
                    }
                    index++;
                }
            }
        }

        private void InTheaters()
        {
            var disc = client.DiscoverMoviesAsync().WherePrimaryReleaseDateIsBefore(DateTime.Today)
                .WherePrimaryReleaseDateIsAfter(DateTime.Today.Subtract(new TimeSpan(7, 0, 0, 0)));

            var collection = disc.Query();
            client.GetConfig();

            var index = 0;
            foreach (var p in panel5.Controls.OfType<PictureBox>())
            {
                if (index + 1 > collection.Result.Results.Count)
                {
                    break;
                }

                p.Visible = true;
                client.GetConfig();
                p.Text = collection.Result.Results[index].Id.ToString();
                p.ImageLocation = client.GetImageUrl("w150", collection.Result.Results[index].PosterPath, false).AbsoluteUri;

                if (p.Image is System.Drawing.Bitmap)
                {
                    index++;
                }
                index++;
            }
        }

        private void Popular()
        {
            var disc = client.DiscoverMoviesAsync().OrderBy(DiscoverMovieSortBy.PopularityDesc);

            var collection1 = disc.Query();
            client.GetConfig();

            var index = 0;
            foreach (var p in panel5.Controls.OfType<PictureBox>())
            {
                if (index + 1 > collection1.Result.Results.Count)
                {
                    break;
                }

                p.Visible = true;
                client.GetConfig();

                p.ImageLocation = client.GetImageUrl("w150", collection1.Result.Results[index].PosterPath, false).AbsoluteUri;
                p.Text = collection1.Result.Results[index].Id.ToString();
                if (p.Image is System.Drawing.Bitmap)
                {
                    index++;
                }
                index++;
            }
        }

        private void GetTopRated()
        {
            var disc = client.DiscoverMoviesAsync().OrderBy(DiscoverMovieSortBy.VoteAverageDesc);

            var collection1 = disc.Query();
            client.GetConfig();

            var index = 0;
            foreach (var p in panel5.Controls.OfType<PictureBox>())
            {
                if (index + 1 > collection1.Result.Results.Count)
                {
                    break;
                }

                p.Visible = true;
                client.GetConfig();

                p.ImageLocation = client.GetImageUrl("w150", collection1.Result.Results[index].PosterPath, false).AbsoluteUri;
                p.Text = collection1.Result.Results[index].Id.ToString();
                if (p.Image is System.Drawing.Bitmap)
                {
                    index++;
                }
                index++;
            }
        }

        private void GetUpcoming()
        {
            var collection1 = client.GetMovieUpcomingListAsync().Result;
            client.GetConfig();

            var index = 0;
            foreach (var p in panel5.Controls.OfType<PictureBox>())
            {
                if (index + 1 > collection1.Results.Count)
                {
                    break;
                }

                p.Visible = true;
                client.GetConfig();

                p.ImageLocation = client.GetImageUrl("w150", collection1.Results[index].PosterPath, false).AbsoluteUri;
                p.Text = collection1.Results[index].Id.ToString();
                if (p.Image is System.Drawing.Bitmap)
                {
                    index++;
                }
                index++;
            }
        }

        private void Details(PictureBox poster)
        {
            var id = Int32.Parse(poster.Text);
            var movie = client.GetMovieAsync(id).Result;
            if (movie.Title != null)
            {
                movieTitle.Text = movie.Title;
                label1.Text = movie.Overview;
                pictureBox9.ImageLocation = client.GetImageUrl("w150", movie.PosterPath, false).AbsoluteUri;
                DisplayVotes(movie);
                panel6.Visible = true;
                panel6.BringToFront();
                DisplayGenres(movie);
            }
            else
            {
                MessageBox.Show("The details can not be displayed.");
            }

        }

        private void DisplayVotes(Movie movie)
        {
            var index = 1;
            const string starFilled = "https://i.imgur.com/2c4PrbW.png";
            const string starHalf = "https://i.imgur.com/tHlBqst.png";
            ClearVotes();
            label2.Text = "Note: " + movie.VoteAverage;
            foreach (var pictureBox in votesPanel.Controls.OfType<PictureBox>())
            {
                if (index <= movie.VoteAverage)
                {
                    pictureBox.ImageLocation = starFilled;
                }
                else if (movie.VoteAverage % 10 != 0 && index < movie.VoteAverage + 1)
                {
                    pictureBox.ImageLocation = starHalf;
                }
                else
                {
                    break;
                }
                pictureBox.Visible = true;
                index++;
            }
          

        }

        private void DisplayGenres(Movie movie)
        {
            foreach (var label in panel7.Controls.OfType<Label>())
            {
                if (label != label1 && label != movieTitle)
                    label.Visible = false;
            }
            foreach (var pictureBox in panel7.Controls.OfType<PictureBox>())
            {
                    pictureBox.Visible = false;
            }

            var movieGenres = movie.Genres;
            var indexGenres = 0;
            if (movie.Genres != null)
            {
                foreach (var label in panel7.Controls.OfType<Label>())
                {

                    if (indexGenres + 1 > movieGenres.Count)
                    {
                        break;
                    }
                    label.Visible = true;
                    if (label != label7)
                    {
                        label.Text = movieGenres[indexGenres].Name;
                        indexGenres++;
                    }
                }
                indexGenres = 0;
                foreach (var pictureBox in panel7.Controls.OfType<PictureBox>())
                {
                    if (indexGenres + 1 > movieGenres.Count)
                    {
                        break;
                    }
                    pictureBox.Visible = true;
                    pictureBox.ImageLocation = genres[movieGenres[indexGenres].Name];
                    indexGenres++;
                }
            }
        }

        private void home_Click(object sender, EventArgs e)
        {
            InTheaters();
            panel6.SendToBack();
            panel5.BringToFront();
            panel6.Visible = false;
            SidePanel.Height = home.Height;
            SidePanel.Top = home.Top;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Popular();
            panel6.SendToBack();
            panel6.Visible = false;
            panel5.BringToFront();
            SidePanel.Height = button1.Height;
            SidePanel.Top = button1.Top;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            GetTopRated();
            panel6.SendToBack();
            panel6.Visible = false;
            panel5.BringToFront();
            SidePanel.Height = topRated.Height;
            SidePanel.Top = topRated.Top;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            GetUpcoming();
            panel6.SendToBack();
            panel6.Visible = false;
            panel5.BringToFront();
            SidePanel.Height = upcoming.Height;
            SidePanel.Top = upcoming.Top;
        }

        private void ClearPictureBox()
        {
            foreach (var pictureBox in panel5.Controls.OfType<PictureBox>())
            {
                pictureBox.Visible = false;
            }
        }

        private void ClearVotes()
        {
            foreach (var pictureBox in votesPanel.Controls.OfType<PictureBox>())
            {
                pictureBox.Visible = false;
            }
        }

        private void pictureBox1_Click_4(object sender, EventArgs e)
        {
            Details((PictureBox)sender);
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

        private void Moviedable_Load(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

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
                panel6.SendToBack();
                panel6.Visible = false;
                panel5.BringToFront();
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

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void votesPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox13_Click(object sender, EventArgs e)
        {

        }
    }

}
