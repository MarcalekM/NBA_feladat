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

            foreach(var csapat in csapatok) Csapatok.Items.Add(csapat.Nev);
        }

        private void Csapatok_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Jatekosok.Items.Clear();
            var jatekosok = csapatok.Where(cs => cs.Nev.Equals(Csapatok.SelectedValue)).First().Jatekosok;
            foreach (var jatekos in jatekosok) Jatekosok.Items.Add(jatekos);
        }
    }
}