using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Expander_Test
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        ICollectionView m_View_Bible;
        public static MainWindow_ViewModel m_ViewModel;

        public MainWindow()
        {
            InitializeComponent();

            m_ViewModel = new MainWindow_ViewModel();
            DataContext = m_ViewModel;

            Info_Bible_Init();
        }

        #region Info_Bible_Init

        public void Info_Bible_Init()
        {
            int i = 1;

            m_ViewModel.m_WholeBible = new List<Info_Bible>
            {
                #region all content

                new Info_Bible {Book = "Jakobus", Acron="Jak",              Bund = "NT", Typ = "Jakobusbrief",          CaptureMax = 05,  Nr=i++, arrVerses = new ArrayList {27,26,18,17,20}},         
                new Info_Bible {Book = "1.Pet",                             Bund = "NT", Typ = "Petrusbrief",           CaptureMax = 05,  Nr=i++, arrVerses = new ArrayList {25,25,22,19,14}},         
                new Info_Bible {Book = "2.Pet",                             Bund = "NT", Typ = "Petrusbrief",           CaptureMax = 03,  Nr=i++, arrVerses = new ArrayList {21,22,18}},         
                new Info_Bible {Book = "1.John", Acron="1.Joh",             Bund = "NT", Typ = "Johannesbrief",         CaptureMax = 05,  Nr=i++, arrVerses = new ArrayList {10,29,24,21,21}},        
                new Info_Bible {Book = "2.John", Acron="1.Joh",             Bund = "NT", Typ = "Johannesbrief",         CaptureMax = 01,  Nr=i++, arrVerses = new ArrayList {13}},
                new Info_Bible {Book = "3.John", Acron="1.Joh",             Bund = "NT", Typ = "Johannesbrief",         CaptureMax = 01,  Nr=i++, arrVerses = new ArrayList {14}},
                new Info_Bible {Book = "Judas",  Acron="Jud",               Bund = "NT", Typ = "Judasbrief",            CaptureMax = 01,  Nr=i++, arrVerses = new ArrayList {25}},

                #endregion
            };

            m_View_Bible          = CollectionViewSource.GetDefaultView(m_ViewModel.m_WholeBible);
            m_ListBooks.ItemsSource       = m_View_Bible;

            Filter_View_Bund(m_View_Bible, "Bund", "NT");

        }

        #endregion

        private void Filter_View_Bund(ICollectionView aView, string strGroup, string strTyp)
        {
            aView.GroupDescriptions.Clear();

            if (!String.IsNullOrEmpty(strGroup))
                aView.GroupDescriptions.Add(new PropertyGroupDescription(strGroup)); // das macht das Grouping + <ListView.GroupStyle> + der Binding Name muss auf "Name" stehen !!!

            if (String.IsNullOrEmpty(strTyp))
                aView.Filter = null;
            else
            {
                aView.Filter = o =>
                {
                    Info_Bible item = (Info_Bible)o;

                    return (item.Bund == strTyp);
                };
            }
        }

        private void m_lv_Books_LayoutUpdated(object sender, EventArgs e)
        {

        }

        private void ListView_Books_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
