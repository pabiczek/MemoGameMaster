using System;
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
using System.Windows.Shapes;
using System.Windows.Threading;
using ClassLibrary;

namespace memoryGame
{
    //Metoda rozszerzająca która poprawia optymalizacje interfejsu graficznego wykorzystując zdarzenia i delegaty
    public static class ExtensionMethods
    {
        private static Action EmptyDelegate = delegate () { };

        public static void Refresh(this UIElement uiElement)
        {
            uiElement.Dispatcher.Invoke(DispatcherPriority.Render, EmptyDelegate);
        }
    }

    //Algorytm pod nazwą Fisher mieszający karty 
    static class RandomExtensions
    {
        public static void Shuffle<T>(this Random randomNumber, T[] array)
        {
            int n = array.Length;
            while (n > 1)
            {
                int k = randomNumber.Next(n--);
                T temp = array[n];
                array[n] = array[k];
                array[k] = temp;
            }
        }
    }

    public partial class GamePage : Page
    {
        //zawiera informacje odnośnie klikniętych kart by następnie sprawdzić czy są takie same
        Button buttonPreviously = null;
        Button buttonNext = null;
        int countDown = 0;  //liczba widocznych kart

        //zawiera liste kolekcji przyciskow i obrazkow 
        List<Image> images = new List<Image>();
        List<Button> buttons = new List<Button>();

public GamePage()
        {    
            InitializeComponent();
            Stoper.TimerReset();
            Time.Content = "00:00";
            Initialize_list();
            RandomizeList();
        }

        private void Initialize_list()
        {
            images.Add(A1);
            images.Add(A2);
            images.Add(A3);
            images.Add(A4);
            images.Add(A5);
            images.Add(A6);
            images.Add(A7);
            images.Add(A8);
            images.Add(A9);
            images.Add(A10);
            images.Add(A11);
            images.Add(A12);

            buttons.Add(B1);
            buttons.Add(B2);
            buttons.Add(B3);
            buttons.Add(B4);
            buttons.Add(B5);
            buttons.Add(B6);
            buttons.Add(B7);
            buttons.Add(B8);
            buttons.Add(B9);
            buttons.Add(B10);
            buttons.Add(B11);
            buttons.Add(B12);
        }

        //algorytm losuje obrazki by znajdowaly sie w roznych miejsca
        private void RandomizeList()
        {
            new Random().Shuffle(Game.board);
            int randomNumber = 0;
            int i = 0;
            foreach (Image img in images)
            {
                randomNumber = Game.board[i];
                img.Source = new BitmapImage(new Uri("img2/P" + randomNumber + ".png", UriKind.Relative));
                i++;
            }
        }

        //zdarzenie wystepujace podczas klikniecia na karte
        private void ButtonClik(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;

            if (countDown == 0)
            {
                buttonPreviously = b;
                buttonPreviously.Visibility = Visibility.Hidden;
            }
            else if (countDown == 1)
            {
                buttonNext = b;
                buttonNext.Visibility = Visibility.Hidden;
                buttonNext.Refresh();

                System.Threading.Thread.Sleep(500);

                if (Check_image())
                {
                    buttonPreviously.IsEnabled = false;
                    buttonNext.IsEnabled = false;
                }
                else
                {
                    buttonPreviously.Visibility = Visibility.Visible;
                    buttonNext.Visibility = Visibility.Visible;
                }

                if (GameWin())
                {
                    System.Threading.Thread.Sleep(1000);
                }
                countDown = -1;
            }
            countDown++;
        }

        //sprawdza czy widoczne obrazki są takie same

        private bool Check_image()
        {
            string image1 = Game.ImageDictionary(buttonPreviously.Name);
            string image2 = Game.ImageDictionary(buttonNext.Name);
            string src1 = "";
            string src2 = "";

            foreach (Image img in images)
            {
                if (img.Name == image1 || img.Name == image2)
                {
                    if (src1 == "")
                        src1 = img.Source.ToString();
                    else
                        src2 = img.Source.ToString();
                }
            }

            if (src1 == src2)
                return true;

            return false;
        }

        //Sprawdza czy odkryliśmy wszystkie karty
        private bool GameWin()
        {
            foreach (Button button in buttons)
            {
                if (button.IsEnabled == true)
                    return false;
            }
            Time.Content = "YouWin!";
            return true;
        } 

        public void GameLoaed(object sender, RoutedEventArgs e)
        {
            //Podczas załadowania sekcji gry następuję inicjalizacja timeru
            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Interval = TimeSpan.FromSeconds(1);
            dispatcherTimer.Tick += DispatcherTimerTicker;
            dispatcherTimer.Start();
        }
        public void DispatcherTimerTicker(object sender, EventArgs e)
        {
            Stoper.Timer();
            if(Time.Content != "YouWin!")
            Time.Content = Stoper.zeroMinutes + Stoper.minutes.ToString() + ":" + Stoper.zeroSeconds + Stoper.seconds.ToString();

        }

        private void ExitGame(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void PlayAgain(object sender, MouseButtonEventArgs e)
        {
            InitalizePlayAgain();
        }

        private void InitalizePlayAgain()
        {
            InitializeComponent();
            images.Remove(A1);
            images.Remove(A2);
            images.Remove(A3);
            images.Remove(A4);
            images.Remove(A5);
            images.Remove(A6);
            images.Remove(A7);
            images.Remove(A8);
            images.Remove(A9);
            images.Remove(A10);
            images.Remove(A11);
            images.Remove(A12);
            buttons.Remove(B1);
            buttons.Remove(B2);
            buttons.Remove(B3);
            buttons.Remove(B4);
            buttons.Remove(B5);
            buttons.Remove(B6);
            buttons.Remove(B7);
            buttons.Remove(B8);
            buttons.Remove(B9);
            buttons.Remove(B10);
            buttons.Remove(B11);
            buttons.Remove(B12);
            Stoper.TimerReset();
            Time.Content = "00:00";
            Initialize_list();
            RandomizeList();
            Stoper.TimerReset();
            Time.Content = "00:00";
            foreach (Button button in buttons)
            {
                button.IsEnabled = true;
                button.Visibility = Visibility.Visible;

            }
        }
    }
}

