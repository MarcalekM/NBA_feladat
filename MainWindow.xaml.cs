using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

namespace NBA_feladat
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Csapat> csapatok = new();
        public MainWindow()
        {
            InitializeComponent();

            List<Jatekos> jatekosok = new();
            using StreamReader sr = new(
                path: @"../../../src/jatekosok.txt",
                encoding: Encoding.UTF8);
            while (!sr.EndOfStream) jatekosok.Add(new(sr.ReadLine()));

            List<Edzo> edzok = new();
            using StreamReader sr2 = new(
                path: @"../../../src/edzok.txt",
                encoding: Encoding.UTF8);
            while (!sr2.EndOfStream) edzok.Add(new(sr2.ReadLine()));

            using StreamReader sr3 = new(
                path: @"../../../src/csapatok.txt",
                encoding: Encoding.UTF8);
            while (!sr3.EndOfStream) csapatok.Add(new(sr3.ReadLine(), jatekosok, edzok));

            foreach(var csapat in csapatok.OrderBy(cs => cs.Nev)) Csapatok.Items.Add(csapat.Nev);
            Csapatok.SelectedItem = Csapatok.Items[0];
        }

        private void Csapatok_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            JatekosAdatokFeltoltes();

            EdzoAdatokFeltoltes();

            if (csapatok.Where(cs => cs.Nev.Equals(Csapatok.SelectedValue)).First().HazaiSzin.Contains('+')) TobbSzinuHatter();
            else ChangeBackgroundColor(csapatok.Where(cs => cs.Nev.Equals(Csapatok.SelectedValue)).First().HazaiSzin);

            KepCsere();
            
        }

        private void JatekosAdatokFeltoltes()
        {
            Jatekosok.Items.Clear();
            var jatekosok = csapatok.Where(cs => cs.Nev.Equals(Csapatok.SelectedValue)).Single().Jatekosok;
            foreach (var jatekos in jatekosok) Jatekosok.Items.Add(jatekos);
        }
        private void EdzoAdatokFeltoltes()
        {
            var edzok = csapatok.Where(cs => cs.Nev.Equals(Csapatok.SelectedValue)).First().Edzok;
            Edzok.Items.Clear();
            foreach (var edzo in edzok) Edzok.Items.Add(edzo);
        }
        private void TobbSzinuHatter()
        {
            if (csapatok.Where(cs => cs.Nev.Equals(Csapatok.SelectedValue)).First().HazaiSzin.Split('+').Count() < 3)
                ChangeBackgroundToTwoColors(csapatok.Where(cs => cs.Nev.Equals(Csapatok.SelectedValue)).First().HazaiSzin.Split('+')[0],
                    csapatok.Where(cs => cs.Nev.Equals(Csapatok.SelectedValue)).First().HazaiSzin.Split('+')[1]);

            else ChangeBackgroundToThreeColors(csapatok.Where(cs => cs.Nev.Equals(Csapatok.SelectedValue)).First().HazaiSzin.Split('+')[0],
                        csapatok.Where(cs => cs.Nev.Equals(Csapatok.SelectedValue)).First().HazaiSzin.Split('+')[1],
                        csapatok.Where(cs => cs.Nev.Equals(Csapatok.SelectedValue)).First().HazaiSzin.Split('+')[2]);
        }
        private void ChangeBackgroundColor(string colorString)
        {
            try
            {
                // Parse the color string into a Color
                Color color = (Color)ColorConverter.ConvertFromString(colorString);

                // Create a SolidColorBrush from the Color
                SolidColorBrush brush = new SolidColorBrush(color);

                // Set the Background property of the Window
                this.Background = brush;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Invalid color string: {colorString}\nError: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void ChangeBackgroundToTwoColors(string color1, string color2)
        {
            try
            {
                // Parse the two color strings into Colors
                Color firstColor = (Color)ColorConverter.ConvertFromString(color1);
                Color secondColor = (Color)ColorConverter.ConvertFromString(color2);

                // Create a LinearGradientBrush
                LinearGradientBrush gradientBrush = new LinearGradientBrush();
                gradientBrush.GradientStops.Add(new GradientStop(firstColor, 0.0)); // Start color
                gradientBrush.GradientStops.Add(new GradientStop(secondColor, 1.0)); // End color

                // Set the Background property of the Window
                this.Background = gradientBrush;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Invalid color strings: {color1}, {color2}\nError: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void ChangeBackgroundToThreeColors(string color1, string color2, string color3)
        {
            try
            {
                // Convert color strings to Color objects
                Color firstColor = (Color)ColorConverter.ConvertFromString(color1);
                Color secondColor = (Color)ColorConverter.ConvertFromString(color2);
                Color thirdColor = (Color)ColorConverter.ConvertFromString(color3);

                // Create a LinearGradientBrush
                LinearGradientBrush gradientBrush = new LinearGradientBrush();
                gradientBrush.StartPoint = new Point(0, 0); // Top-left
                gradientBrush.EndPoint = new Point(1, 1);   // Bottom-right

                // Add three GradientStops
                gradientBrush.GradientStops.Add(new GradientStop(firstColor, 0.0)); // Start color
                gradientBrush.GradientStops.Add(new GradientStop(secondColor, 0.5)); // Middle color
                gradientBrush.GradientStops.Add(new GradientStop(thirdColor, 1.0)); // End color

                // Set the background of the Window
                this.Background = gradientBrush;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error setting gradient: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void KepCsere()
        {
            var nev = csapatok.Where(cs => cs.Nev.Equals(Csapatok.SelectedValue)).Single().Nev.ToString() + ".jpg";
            Logo.Source = new BitmapImage(new Uri($@"C:\Users\Ny19MarcalekM\Source\Repos\MarcalekM\NBA_feladat\Images\{nev}"));
        }
    }
}