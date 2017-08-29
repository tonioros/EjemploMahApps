using System.Windows;
using System.Windows.Controls;
using System;

namespace EjemploMahApp
{
    public partial class MainWindow 
    {
        private String Password = "";
        public String PassUS
        {
            get {
                OnChangeText();
                return Password;
            }
        }
        public static MainWindow Instancia { get; set; } 
        public MainWindow()
        {
            InitializeComponent();
            Instancia = this;
        }
        private void OnChangeText() {
            MainWindow.Instancia.Password = ContraUS.Password;
        }
        public void closeMe()
        {
            this.Close();
        }
    }
}
