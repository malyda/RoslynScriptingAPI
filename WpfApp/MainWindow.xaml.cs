using RoslynScriptingAPI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
/// <summary>
/// If FileNotFoundException is thrown in .Net Framework project. Add to .csproj (WpfApp.csproj) like in this project:
/// <PropertyGroup> <RestoreProjectStyle>PackageReference</RestoreProjectStyle> </PropertyGroup>
/// FileNotFoundException for Microsoft.CodeAnalysis.CSharp.Scripting (nuget package added to .Net Standard project)
/// </summary>
namespace WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            IntBox.Text = "1 + 2";
            GlobalsBox.Text = "SomeVariable = \"Changed inside script\";";

            string code = @"int Add(int x, int y) {
                            return x+y;
                            }
                            Add(1, 4)";
            CodeBox.Text = code; 
        }

        private async void IntButton_Click(object sender, RoutedEventArgs e)
        {
            ExecuteSample executeSample = new ExecuteSample();
            int sum = await executeSample.ReturnIntAsync(IntBox.Text);

            IntResultLabel.Content = sum;
        }

        private async void GlobalsButton_Click(object sender, RoutedEventArgs e)
        {
            ExecuteSample executeSample = new ExecuteSample();

            string initValue = "default Value";

            string newValue = await executeSample.UseGlobalDataInScriptAsync(initValue);

            GlobalsResultLabel.Content = $"Inserted script changed value to: {newValue}";

        }

        private async void CodeButton_Click(object sender, RoutedEventArgs e)
        {
            ExecuteSample executeSample = new ExecuteSample();
            CodeResultLabel.Content = await executeSample.EvaluateScriptAsync(CodeBox.Text);

        }
    }
}
