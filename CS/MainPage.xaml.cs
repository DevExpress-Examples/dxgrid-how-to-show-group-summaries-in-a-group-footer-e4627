using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Data;
using System.Collections.ObjectModel;

namespace Q484501GroupFooterWorkaround {
    public partial class MainPage : UserControl {
        public MainPage() {
            InitializeComponent();

            List<TestData> list = new List<TestData>();
            for(int i = 0; i < 200; i++)
                list.Add(new TestData() { Text = "Text0 Row" + i,
                                          Number0 = i,
                                          Number1 = 100 - i,
                                          Number2 = i % 10,
                                          Group = i % 10
                });
            DataContext = list;

            Loaded += new RoutedEventHandler(MainPage_Loaded);
        }

        void MainPage_Loaded(object sender, RoutedEventArgs e) {
            grid.ExpandGroupRow(-1);
        }
    }

    public class TestData {
        public string Text { get; set; }
        public int Number0 { get; set; }
        public int Number1 { get; set; }
        public int Number2 { get; set; }
        public int Group { get; set; }
    }
}
