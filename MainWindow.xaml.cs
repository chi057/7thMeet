﻿using System;
using System.Collections.Generic;
using System.IO;
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

namespace _7thMeet
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            RtbText.Document.Blocks.Clear();
            CmbFontFamily.ItemsSource = Fonts.SystemFontFamilies.OrderBy(f => f.Source);
            CmbFontSize.ItemsSource = new List<double>() { 8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 48, 72 };
        }

        private void BtnOpen_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.Filter = "RTF文件 (*.rtf)|*.rtf|All files (*.*)|*.*";
            if (dlg.ShowDialog() == true)
            {
                FileStream fileStream = new FileStream(dlg.FileName, FileMode.Open);
                TextRange range = new TextRange(RtbText.Document.ContentStart, RtbText.Document.ContentEnd);
                range.Load(fileStream, DataFormats.Rtf);
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.Filter = "RTF文件 (*.rtf)|*.rtf|All files (*.*)|*.*";
            if (dlg.ShowDialog() == true)
            {
                FileStream fileStream = new FileStream(dlg.FileName, FileMode.Create);
                TextRange range = new TextRange(RtbText.Document.ContentStart, RtbText.Document.ContentEnd);
                range.Save(fileStream, DataFormats.Rtf);
            }
        }

        private void CmbFontFamily_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CmbFontFamily.SelectedItem != null)
                RtbText.Selection.ApplyPropertyValue(Inline.FontFamilyProperty, CmbFontFamily.SelectedItem);
        }

        private void CmbFontSize_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CmbFontSize.SelectedItem != null)
                RtbText.Selection.ApplyPropertyValue(Inline.FontSizeProperty, CmbFontSize.SelectedItem);
        }

        private void BtnBold_Click(object sender, RoutedEventArgs e)
        {
            object temp = RtbText.Selection.GetPropertyValue(Inline.FontWeightProperty);

            if ((temp != DependencyProperty.UnsetValue) && (temp.Equals(FontWeights.Bold)))
                RtbText.Selection.ApplyPropertyValue(FontWeightProperty, FontWeights.Normal);
            else
                RtbText.Selection.ApplyPropertyValue(FontWeightProperty, FontWeights.Bold);
        }

        private void BtnItalic_Click(object sender, RoutedEventArgs e)
        {
            object temp = RtbText.Selection.GetPropertyValue(Inline.FontStyleProperty);

            if ((temp != DependencyProperty.UnsetValue) && (temp.Equals(FontStyles.Italic)))
                RtbText.Selection.ApplyPropertyValue(FontStyleProperty, FontStyles.Normal);
            else
                RtbText.Selection.ApplyPropertyValue(FontStyleProperty, FontStyles.Italic);
        }

        private void BtnUnderline_Click(object sender, RoutedEventArgs e)
        {
            object temp = RtbText.Selection.GetPropertyValue(Inline.TextDecorationsProperty);

            if ((temp != DependencyProperty.UnsetValue) && (temp.Equals(TextDecorations.Underline)))
                RtbText.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, null);
            else
                RtbText.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, TextDecorations.Underline);
        }

        SolidColorBrush DefaultColor = new SolidColorBrush(Color.FromArgb(100, 221, 221, 221));
        private void RtbText_SelectionChanged(object sender, RoutedEventArgs e)
        {
            object temp = RtbText.Selection.GetPropertyValue(Inline.FontWeightProperty);
            if ((temp != DependencyProperty.UnsetValue) && (temp.Equals(FontWeights.Bold)))
                BtnBold.Background = Brushes.Gray;
            else
                BtnBold.Background = DefaultColor;

            temp = RtbText.Selection.GetPropertyValue(Inline.FontStyleProperty);
            if ((temp != DependencyProperty.UnsetValue) && (temp.Equals(FontStyles.Italic)))
                BtnItalic.Background = Brushes.Gray;
            else
                BtnItalic.Background = DefaultColor;

            temp = RtbText.Selection.GetPropertyValue(Inline.TextDecorationsProperty);
            if ((temp != DependencyProperty.UnsetValue) && (temp.Equals(TextDecorations.Underline)))
                BtnUnderline.Background = Brushes.Gray;
            else
                BtnUnderline.Background = DefaultColor;

            temp = RtbText.Selection.GetPropertyValue(Inline.FontFamilyProperty);
            CmbFontFamily.SelectedItem = temp;
            temp = RtbText.Selection.GetPropertyValue(Inline.FontSizeProperty);
            CmbFontSize.SelectedItem = temp;
        }

        private void RtbText_LostFocus(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
        }
    }
}
