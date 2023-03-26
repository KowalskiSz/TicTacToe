using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TicTacToe
{

    public partial class MainWindow : Window
    {

        #region Private atributes

        /// <summary>
        /// Holds current contents (mark) of a cell in game
        /// </summary>
        private MarkType[] cellResult;

        private bool PlayerOneTurn; //true if player's 1 turn (X) else palyer's 2 (0)

        private bool isGameDone; //true if game is finished

        #endregion

        #region Constructor
        /// <summary>
        /// Defoult constructor 
        /// </summary>

        public MainWindow()
        {
            InitializeComponent();

            NewGame(); 
        }

        #endregion


        /// <summary>
        /// Starting a new game, reseting each cell to default, 
        /// </summary>
        private void NewGame()
        {
           
            /// <summary>
            /// New list of the defoult free state of each cell, for loop just to clarify the state (free)
            /// </summary>
            
            cellResult = new MarkType[9];

            for (int i = 0; i < cellResult.Length; i++)
            {
                cellResult[i] = MarkType.Empty;  
            }

            PlayerOneTurn = true; //Player one is a current playing


            /// <summary>
            /// Clearing cells, (setting Content to empty string), setting colours to default
            /// </summary>
            
            Container.Children.Cast<Button>().ToList().ForEach( b => 
                { 
                    b.Content = string.Empty;
                    b.Background = Brushes.White;
                    b.Foreground = Brushes.Black; 

                });
            

            isGameDone = false; //true if the game is over
        }

        /// <summary>
        /// Function that specifies the action when button clicked. Applied to all the buttons
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(isGameDone)
            {
                NewGame();

                return; 
            }

            //cast the sender object to button object
            var button = (Button)sender;

            var buttonColumn = Grid.GetColumn(button);
            var buttonRow = Grid.GetRow(button);

            var buttonIndex = buttonColumn + (buttonRow * 3); // getting each button index to state list

            if (cellResult[buttonIndex] != MarkType.Empty) // check if the button is already clicked
            {
                return; 
            }

           
            //inserts into state list cross if P1 turn, and circle if P2 turn 
            cellResult[buttonIndex] = PlayerOneTurn ? MarkType.Cross : MarkType.Circle; 

            if(PlayerOneTurn) //Change the content of a button 
            {
                button.Content = "X";
            }
            else
            {
                button.Content = "O"; 
            }

            if(!PlayerOneTurn)
            {
                button.Foreground = Brushes.Red; 
            }

            PlayerOneTurn ^= true;  //toggle player's turn 

            // function chcking if it is a winning set 

            isGameWon(); 


        }


        private void isGameWon()
        {

            #region Horizontal
            //1. horizontal combinations 

            if (cellResult[0] != MarkType.Empty && (cellResult[0] & cellResult[1] & cellResult[2]) == cellResult[0])
            {
                isGameDone = true;
                Button0_0.Background = Button0_1.Background = Button0_2.Background = Brushes.Blue; 
            }
            else if (cellResult[3] != MarkType.Empty && (cellResult[3] & cellResult[4] & cellResult[5]) == cellResult[3])
            {
                isGameDone = true;
                Button1_0.Background = Button1_1.Background = Button1_2.Background = Brushes.Blue; 
            }
            else if (cellResult[6]!=MarkType.Empty && (cellResult[6] & cellResult[7] & cellResult[8]) == cellResult[6])
            {
                isGameDone = true;
                Button2_0.Background = Button2_1.Background = Button2_2.Background = Brushes.Blue; 
            }

            #endregion


            #region Veltical 
            //2. vertical combinations 

            if (cellResult[0]!=MarkType.Empty && (cellResult[0] & cellResult[3] & cellResult[6]) == cellResult[0])
            {
                isGameDone = true;
                Button0_0.Background = Button1_0.Background = Button2_0.Background = Brushes.Blue; 
            }
            else if (cellResult[1]!=MarkType.Empty && (cellResult[1] & cellResult[4] & cellResult[7]) == cellResult[1])
            {
                isGameDone = true;
                Button0_1.Background = Button1_1.Background = Button2_1.Background = Brushes.Blue; 
            }
            else if (cellResult[2] != MarkType.Empty && (cellResult[2] & cellResult[5] & cellResult[8]) == cellResult[2])
            {
                isGameDone = true; 
                Button0_2.Background = Button1_2.Background = Button2_2.Background = Brushes.Blue;
            }

            #endregion

            #region

            //3. Cross combinations

            if (cellResult[0]!=MarkType.Empty && (cellResult[0] & cellResult[4] & cellResult[8]) == cellResult[0])
            {
                isGameDone = true;
                Button0_0.Background = Button1_1.Background = Button2_2.Background = Brushes.Blue; 
            }
            else if (cellResult[2] != MarkType.Empty && (cellResult[2] & cellResult[4] & cellResult[6]) == cellResult[2])
            {
                isGameDone = true;
                Button0_2.Background = Button1_1.Background = Button2_0.Background = Brushes.Blue; 
            }

            #endregion

            //In case if a draw (nobody wins)

            if (!cellResult.Any(c => c == MarkType.Empty))
            {
                Container.Children.Cast<Button>().ToList().ForEach(b =>
                {
                    isGameDone = true;
                    b.Background = Brushes.Orange;
                   
                });

            }


        }

    }


}
