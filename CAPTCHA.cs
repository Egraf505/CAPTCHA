using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CaptchaDemo
{
    internal class CAPTCHA
    {
        // Текст капчи
        private string _textCaptcha;

        // Так где будет наша капча и её же мы будем настраивать
        private readonly TextBlock _outTextCaptcha;
        // Откуда мы будем брать вводимую капчу юзером
        private readonly TextBox _inputTextCaptcha;
        // Кнопка что бы обновить капчу
        private readonly Button _refreshCaptcha;

        private readonly TextDecorationCollection _decorationCollection = new TextDecorationCollection()
        {
            TextDecorations.OverLine,
            TextDecorations.Underline,
            TextDecorations.Strikethrough,
            TextDecorations.Baseline
        };
        // Коллекция стиля для надписи
        private readonly FontStyle[] _textStyles = 
        {
            FontStyles.Normal,
            FontStyles.Italic,
            FontStyles.Oblique
        };

        // Коллекция жирности текста
        private readonly FontWeight[] _textWeights = 
        {
            FontWeights.Bold,
            FontWeights.Regular,
            FontWeights.ExtraLight
        };

        // Коллекция цветов текста
        private readonly Brush[] _brushes =
        {
            Brushes.AliceBlue,
            Brushes.Pink,
            Brushes.Red,
            Brushes.Green,
            Brushes.Blue
        };


        public CAPTCHA(TextBlock outTextCaptcha, TextBox inputTextCaptcha, Button refreshCaptcha)
        {
            _outTextCaptcha = outTextCaptcha;            
            _inputTextCaptcha = inputTextCaptcha;
            _refreshCaptcha = refreshCaptcha;

            GenerateCaptcha();

            _refreshCaptcha.Click += RefreshCaptcha_Click;    
        }
        ~CAPTCHA()
        {
            _refreshCaptcha.Click -= RefreshCaptcha_Click;
        }

        public bool IsCorrectCaptcha
        {
            get
            {
                return _inputTextCaptcha.Text == _textCaptcha;
            }
        }

        async public void GenerateCaptcha()
        {
            _textCaptcha = "";

            Random random = new Random();

            FontStyle fontStyle = _textStyles[random.Next(0, _textStyles.Length-1)];
            FontWeight fontWeight = _textWeights[random.Next(0,_textWeights.Length-1)];
            Brush brush = _brushes[random.Next(0, _brushes.Length-1)];
            TextDecorationCollection textDecoration = new TextDecorationCollection() { _decorationCollection[random.Next(0, _decorationCollection.Count - 1)] };

            string ALF = "1234567890QWERTYUIOPASDFGHJKLZXCVBNM";

            for (int i = 0; i < 6; i++)
            {
                await Task.Run(() => { _textCaptcha += ALF[random.Next(0, ALF.Length)]; });
            }

            _outTextCaptcha.FontStyle = fontStyle;
            _outTextCaptcha.FontWeight = fontWeight;
            _outTextCaptcha.Foreground = brush;
            _outTextCaptcha.TextDecorations = textDecoration;

            _outTextCaptcha.Text = _textCaptcha;
        }        
   
        private void RefreshCaptcha_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            GenerateCaptcha();
        }
    }
}
