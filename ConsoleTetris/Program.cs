using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTetris
{
    /// <summary>
    /// Interaction logic for Program.
    /// </summary>
    class Program
    {
        #region Fields

        #region Map

        // The map's width.
        private const int mapWidth = 12;

        // The map's height.
        private const int mapHeight = 22;

        // The map.
        public static char[,] map = new char[mapHeight, mapWidth]
        {
            {'#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#'},
            {'#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#'},
            {'#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#'},
            {'#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#'},
            {'#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#'},
            {'#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#'},
            {'#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#'},
            {'#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#'},
            {'#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#'},
            {'#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#'},
            {'#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#'},
            {'#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#'},
            {'#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#'},
            {'#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#'},
            {'#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#'},
            {'#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#'},
            {'#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#'},
            {'#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#'},
            {'#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#'},
            {'#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#'},
            {'#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#'},
            {'#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#'}
        };

        // Draw the map in color.
        private static bool coloredMap = true;

        #endregion Map

        #region Highscore

        // The limit of the highscores.
        private const int scoreLimit = 20;

        // The highscores file path.
        private static XmlAccess xmlAccess = new XmlAccess(@"..\..\Resources\highscores.xml");

        // The list of highscores.
        private static List<Highscore> highscoresList = new List<Highscore>();

        #endregion Highscore

        #region Character

        // The shape.
        private static Shape shape = new Shape();

        #endregion Character

        #region CharacterFunction

        // The player's score.
        private static int playerScore = 0;

        // 
        private static int playerLine = 0;

        // 
        private static int playerLevel = 1;

        // The time delay to move the shape.
        private static int moveShapeCounter = 0;

        // The snake's movement.
        private static MoveDirections shapeDirection = MoveDirections.Stay;

        // The enum of move directions.
        private enum MoveDirections
        {
            Left,
            Right,
            Up,
            Down,
            Stay
        }

        #endregion CharacterFunction

        #region MapFunction

        // Indicates that the game is in session or not.
        private static bool gameFinished = false;

        // The game needs to preset.
        private static bool gamePreset = true;

        // The status of the game.
        private static GameStages gameStatus = GameStages.Menu;

        // Refresh the display.
        public static bool refreshDisplay = false;

        // The random.
        private static Random rnd = new Random();

        // The number of the options.
        private const int optionsNumber = 1;

        // The curzor for the options.
        private static int optionsPointer = 1;

        // Indicates that the user's score should be checked or not.
        private static bool checkHighscores = false;

        // The time limit of the snake move.
        private static int moveShapeTime = 10000000;

        // The enum of game stages.
        private enum GameStages
        {
            Menu,
            Highsores,
            Game,
            Options,
            Exit
        }

        #endregion MapFunction

        #endregion Fields

        #region Methods

        /// <summary>
        /// The main method of the Program.
        /// </summary>
        /// <param name="args">The input arguments.</param>
        static void Main(string[] args)
        {
            try
            {
                while (!gameFinished)
                {
                    #region Menu

                    // The menu stage.
                    if (gameStatus == GameStages.Menu)
                    {
                        #region RefreshDisplay

                        DisplayMenu();

                        #endregion RefreshDisplay

                        // Repeat the key read until a valid key is pushed.
                        while (gameStatus == GameStages.Menu)
                        {
                            #region HandleInput

                            // A key was pushed.
                            if (Console.KeyAvailable)
                            {
                                // Handle the key input.
                                switch (Console.ReadKey().Key)
                                {
                                    case ConsoleKey.D1:
                                    case ConsoleKey.NumPad1:
                                        gameStatus = GameStages.Game;
                                        break;
                                    case ConsoleKey.D2:
                                    case ConsoleKey.NumPad2:
                                        gameStatus = GameStages.Highsores;
                                        break;
                                    case ConsoleKey.D3:
                                    case ConsoleKey.NumPad3:
                                        gameStatus = GameStages.Options;
                                        break;
                                    case ConsoleKey.D4:
                                    case ConsoleKey.NumPad4:
                                        gameStatus = GameStages.Exit;
                                        break;
                                }

                                // Delete the read key input.
                                DeleteKeyInput();
                            }

                            #endregion HandleInput
                        }
                    }

                    #endregion Menu

                    #region Game

                    // The game stage.
                    else if (gameStatus == GameStages.Game)
                    {
                        #region PreSet

                        shape = GenerateShape();

                        // Pre-set values.
                        if (gamePreset)
                        {
                            for (int i = shape.Position.X; i < shape.Position.X + 5; i++)
                            {
                                for (int j = shape.Position.Y; j < shape.Position.Y + 5; j++)
                                {
                                    map[i, j] = shape.ShapeMap[i, j];
                                    RefreshChar(new Position(i, j), shape.ShapeMap[i, j]);
                                }
                            }

                            playerScore = 0;
                            gamePreset = false;
                            checkHighscores = false;
                        }

                        #endregion PreSet

                        #region RefreshDisplay

                        DisplayMap();

                        #endregion RefreshDisplay

                        // Repeat while the game is active.
                        while (gameStatus == GameStages.Game)
                        {
                            // 
                            if (moveShapeCounter == moveShapeTime)
                            {
                                #region HandleInput

                                // A key was pushed.
                                if (Console.KeyAvailable)
                                {
                                    // Handle the key input.
                                    switch (Console.ReadKey().Key)
                                    {
                                        case ConsoleKey.W:
                                        case ConsoleKey.UpArrow:
                                            
                                            break;
                                        case ConsoleKey.A:
                                        case ConsoleKey.LeftArrow:
                                            
                                            break;
                                        case ConsoleKey.D:
                                        case ConsoleKey.RightArrow:
                                            
                                            break;
                                        case ConsoleKey.S:
                                        case ConsoleKey.DownArrow:
                                            
                                            break;
                                        case ConsoleKey.Escape:
                                            gameStatus = GameStages.Menu;
                                            checkHighscores = false;
                                            gamePreset = true;
                                            break;
                                    }

                                    // Delete the read key input.
                                    DeleteKeyInput();
                                }

                                #endregion HandleInput

                                #region HandleChanges

                                // Only handle the changes in the game stage.
                                if (gameStatus == GameStages.Game)
                                {
                                    #region MoveShape

                                    // The shape moves.
                                    MoveShape();

                                    #endregion MoveShape

                                    // Search for full line
                                }

                                #endregion HandleChanges

                                moveShapeCounter = 0;
                            }
                            // 
                            else
                            {
                                moveShapeCounter++;
                            }

                            #region GameRefresh

                            // The display needs to be refreshed.
                            if (refreshDisplay)
                            {
                                #region Text

                                #region Score

                                // Display the player's score.
                                if (playerScore / 10000 > 0)
                                {
                                    Console.SetCursorPosition(8, 20);
                                }
                                else if (playerScore / 1000 > 0)
                                {
                                    Console.SetCursorPosition(8, 20);
                                    Console.Write("0");
                                    Console.SetCursorPosition(9, 20);
                                }
                                else if (playerScore / 100 > 0)
                                {
                                    Console.SetCursorPosition(8, 20);
                                    Console.Write("00");
                                    Console.SetCursorPosition(10, 20);
                                }
                                else if (playerScore / 10 > 0)
                                {
                                    Console.SetCursorPosition(8, 20);
                                    Console.Write("000");
                                    Console.SetCursorPosition(11, 20);
                                }
                                else
                                {
                                    Console.SetCursorPosition(8, 20);
                                    Console.Write("0000");
                                    Console.SetCursorPosition(12, 20);
                                }

                                Console.Write(playerScore);
                                Console.SetCursorPosition(0, 21);

                                #endregion Score

                                #endregion Text

                                refreshDisplay = false;
                            }

                            #endregion GameRefresh
                        }

                        // Check the player's score if it's in the highscores.
                        if (checkHighscores)
                        {
                            #region RefreshDisplay

                            StoreHighscore();

                            #endregion RefreshDisplay

                            gamePreset = true;
                        }
                    }

                    #endregion Game

                    #region Highscores

                    // The highscore stage.
                    else if (gameStatus == GameStages.Highsores)
                    {
                        #region RefreshDisplay

                        DisplayHighscores();

                        #endregion RefreshDisplay

                        // Repeat the key read until the escape is pushed.
                        while (gameStatus == GameStages.Highsores)
                        {
                            #region HandleInput

                            // A key was pushed.
                            if (Console.KeyAvailable)
                            {
                                // Handle the key input.
                                if (Console.ReadKey().Key == ConsoleKey.Escape)
                                {
                                    gameStatus = GameStages.Menu;
                                }

                                // Delete the read key input.
                                DeleteKeyInput();
                            }

                            #endregion HandleInput
                        }
                    }

                    #endregion Highscores

                    #region Options

                    // The options stage.
                    else if (gameStatus == GameStages.Options)
                    {
                        #region RefreshDisplay

                        DisplayOptions();
                        optionsPointer = 1;

                        #endregion RefreshDisplay

                        // Repeat the key read until the escape is pushed.
                        while (gameStatus == GameStages.Options)
                        {
                            #region HandleInput

                            // A key was pushed.
                            if (Console.KeyAvailable)
                            {
                                // Handle the key input.
                                switch (Console.ReadKey().Key)
                                {
                                    case ConsoleKey.Escape:
                                        gameStatus = GameStages.Menu;
                                        break;
                                    case ConsoleKey.LeftArrow:
                                        switch (optionsPointer)
                                        {
                                            case 1:
                                                if (coloredMap)
                                                {
                                                    coloredMap = false;
                                                    ChangeText(new Position(2, 7), "Off");
                                                }
                                                else
                                                {
                                                    coloredMap = true;
                                                    ChangeText(new Position(2, 7), "On ");
                                                }
                                                break;
                                        }
                                        break;
                                    case ConsoleKey.RightArrow:
                                        switch (optionsPointer)
                                        {
                                            case 1:
                                                if (coloredMap)
                                                {
                                                    coloredMap = false;
                                                    ChangeText(new Position(2, 7), "Off");
                                                }
                                                else
                                                {
                                                    coloredMap = true;
                                                    ChangeText(new Position(2, 7), "On ");
                                                }
                                                break;
                                        }
                                        break;
                                    case ConsoleKey.UpArrow:
                                        if (optionsPointer > 1)
                                        {
                                            optionsPointer--;
                                            ChangeOption(optionsPointer, optionsPointer + 1);
                                        }
                                        else
                                        {
                                            optionsPointer = optionsNumber;
                                            ChangeOption(optionsNumber, 1);
                                        }
                                        break;
                                    case ConsoleKey.DownArrow:
                                        if (optionsPointer < optionsNumber)
                                        {
                                            optionsPointer++;
                                            ChangeOption(optionsPointer, optionsPointer - 1);
                                        }
                                        else
                                        {
                                            optionsPointer = 1;
                                            ChangeOption(1, optionsNumber);
                                        }
                                        break;
                                }

                                // Delete the read key input.
                                DeleteKeyInput();
                            }

                            #endregion HandleInput
                        }
                    }

                    #endregion Options

                    #region Exit

                    // The exit stage.
                    else if (gameStatus == GameStages.Exit)
                    {
                        // End the program.
                        gameFinished = true;
                    }

                    #endregion Exit
                }
            }
            catch
            { }
        }

        #region Functions

        /// <summary>
        /// Genetares a shape.
        /// </summary>
        private static Shape GenerateShape()
        {
            try
            {
                return null;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Changes the selected option.
        /// </summary>
        /// <param name="currentpointer">The current selection.</param>
        /// <param name="previouspointer">The previous selection.</param>
        private static void ChangeOption(int currentpointer, int previouspointer)
        {
            try
            {
                if (currentpointer != previouspointer)
                {
                    // Make the current selection have the curzor.
                    switch (currentpointer)
                    {
                        case 1:
                            Console.SetCursorPosition(7, 2);
                            Console.BackgroundColor = ConsoleColor.Gray;
                            Console.ForegroundColor = ConsoleColor.Black;
                            if (coloredMap)
                            {
                                Console.Write("On ");
                            }
                            else
                            {
                                Console.Write("Off");
                            }
                            break;
                    }

                    // Make the previous selection the default color.
                    switch (previouspointer)
                    {
                        case 1:
                            Console.SetCursorPosition(7, 2);
                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.ForegroundColor = ConsoleColor.Gray;
                            if (coloredMap)
                            {
                                Console.Write("On ");
                            }
                            else
                            {
                                Console.Write("Off");
                            }
                            break;
                    }

                    Console.SetCursorPosition(1, 7);
                }
            }
            catch
            { }
        }

        /// <summary>
        /// Changes the text at the given position.
        /// </summary>
        /// <param name="position">The position to write.</param>
        /// <param name="text">The test to write.</param>
        private static void ChangeText(Position position, string text)
        {
            try
            {
                Console.SetCursorPosition(position.Y, position.X);
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write(text);
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.SetCursorPosition(1, 7);
            }
            catch
            { }
        }

        /// <summary>
        /// Validates the next step of the shape.
        /// </summary>
        /// <param name="newposition">The new position.</param>
        private static void ValidateNextStep(Position newposition)
        {
            try
            {
                switch (map[newposition.X, newposition.Y])
                {
                    case ' ':

                        break;
                    case 'O':

                        break;
                    case '.':

                        break;
                    case '*':

                        break;
                    case '#':

                        break;
                }
            }
            catch
            { }
        }

        /// <summary>
        /// Moves the shape.
        /// </summary>
        private static void MoveShape()
        {
            try
            {
                switch (shapeDirection)
                {
                    case MoveDirections.Left:

                        break;
                    case MoveDirections.Right:

                        break;
                    case MoveDirections.Up:

                        break;
                    case MoveDirections.Down:

                        break;
                }
            }
            catch
            { }
        }

        /// <summary>
        /// Displays the menu.
        /// </summary>
        private static void DisplayMenu()
        {
            try
            {
                // Clear the console.
                Console.Clear();

                // Display the menu.
                Console.WriteLine("Press the number of the option.");
                Console.WriteLine();
                Console.WriteLine("1. Start Game");
                Console.WriteLine();
                Console.WriteLine("2. Highscores");
                Console.WriteLine();
                Console.WriteLine("3. Options");
                Console.WriteLine();
                Console.WriteLine("4. Exit");
                Console.WriteLine();
            }
            catch
            { }
        }

        /// <summary>
        /// Overwrites the given position with the given character.
        /// </summary>
        /// <param name="position">The position to write to.</param>
        /// <param name="character">The character to write.</param>
        private static void RefreshChar(Position position, char character)
        {
            try
            {
                Console.SetCursorPosition(position.Y, position.X);

                // Display the ghost in color.
                if (coloredMap)
                {
                    // Handles the characters.
                    switch (character)
                    {
                        case '#':
                            Console.ForegroundColor = ConsoleColor.DarkCyan;
                            break;
                        case '.':
                            Console.ForegroundColor = ConsoleColor.White;
                            break;
                        case 'O':
                        case '@':
                            Console.ForegroundColor = ConsoleColor.Green;
                            break;
                        case '*':
                            Console.ForegroundColor = ConsoleColor.Red;
                            break;
                        case ' ':
                            break;
                    }

                    Console.Write(character);
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                // Display the ghost in black and white.
                else
                {
                    Console.Write(character);
                }

                Console.SetCursorPosition(0, 21);
            }
            catch
            { }
        }

        /// <summary>
        /// Deletes the read key input.
        /// </summary>
        private static void DeleteKeyInput()
        {
            try
            {
                Console.CursorLeft -= 1;
                Console.Write(" ");
                Console.CursorLeft -= 1;
            }
            catch
            { }
        }

        /// <summary>
        /// Displays the options.
        /// </summary>
        private static void DisplayOptions()
        {
            try
            {
                // Clear the console.
                Console.Clear();

                // Display the menu.
                Console.WriteLine("Options");
                Console.WriteLine();
                Console.Write("Color: ");
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.ForegroundColor = ConsoleColor.Black;
                if (coloredMap)
                {
                    Console.Write("On ");
                }
                else
                {
                    Console.Write("Off");
                }
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine();
                Console.WriteLine();

                // Display the return message.
                Console.WriteLine();
                Console.WriteLine("Press Up or Down to select an option and Left or Right to change it.");
                Console.WriteLine("Press Esc to return to menu.");
            }
            catch
            { }
        }

        /// <summary>
        /// Displays the highscores.
        /// </summary>
        private static void DisplayHighscores()
        {
            try
            {
                // Clear the console.
                Console.Clear();

                // Display the highscores.
                Console.WriteLine("Highscores.");
                Console.WriteLine();

                try
                {
                    // Get the highscores from the file.
                    highscoresList = xmlAccess.LoadScores();

                    // There is at leat one highsore in the list.
                    if (highscoresList != null && highscoresList.Count > 0)
                    {
                        // Sort the highscores in the list in a descending order.
                        highscoresList.Sort((x, y) => y.Score.CompareTo(x.Score));

                        // Check each highscore.
                        foreach (var oneItem in highscoresList)
                        {
                            // Show the highscore.
                            Console.WriteLine(oneItem.ToString());
                        }
                    }
                }
                catch
                { }

                // Display the return message.
                Console.WriteLine();
                Console.WriteLine("Press Esc to return to menu.");
            }
            catch
            { }
        }

        /// <summary>
        /// Displays the map.
        /// </summary>
        private static void DisplayMap()
        {
            try
            {
                // Clear the console.
                Console.Clear();

                // The rows of the map.
                for (int i = 0; i < mapHeight; i++)
                {
                    // The columns of the map.
                    for (int j = 0; j < mapWidth; j++)
                    {
                        // Show colored map.
                        if (coloredMap)
                        {
                            // Set the color based on the map item.
                            switch (map[i, j])
                            {
                                case '#':
                                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                                    break;
                                case '.':
                                    Console.ForegroundColor = ConsoleColor.White;
                                    break;
                                case 'O':
                                case '@':
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    break;
                                case '*':
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    break;
                                case ' ':
                                    break;
                            }

                            // Show the item and reset the color.
                            Console.Write(map[i, j]);
                            Console.ForegroundColor = ConsoleColor.Gray;
                        }
                        // Show black and white map.
                        else
                        {
                            Console.Write(map[i, j]);
                        }
                    }

                    // Brake the line.
                    Console.WriteLine();
                }

                // Show the information line.
                Console.WriteLine("Score: {0:00000}, Exit: Press Esc.", playerScore);
            }
            catch
            { }
        }

        /// <summary>
        /// Stores a new highscore.
        /// </summary>
        private static void StoreHighscore()
        {
            try
            {
                // Clear the console.
                Console.Clear();

                List<Highscore> scoreList = new List<Highscore>();

                try
                {
                    // Load the scores.
                    scoreList = xmlAccess.LoadScores();

                    // The scores are valid.
                    if (scoreList != null && scoreList.Count > 0)
                    {
                        // Sort the scores in descending order.
                        scoreList.Sort((x, y) => y.Score.CompareTo(x.Score));

                        bool found = false;
                        checkHighscores = false;
                        string name = "";

                        // Check each highscore.
                        for (int i = 0; i < scoreList.Count; i++)
                        {
                            // The player's score is bigger then any of the highscores.
                            if (scoreList[i].Score < playerScore)
                            {
                                // No mach found previously. 
                                if (!found)
                                {
                                    found = true;
                                    break;
                                }
                            }
                        }

                        // Try to submit it.
                        if (found)
                        {
                            // Ask if the player would like to submit the score.
                            Console.WriteLine("Your score made it to the highscores, would you like to submit it? Y or N.");
                            ConsoleKey pressed;

                            // Read the answer.
                            do
                            {
                                // Store the given answer.
                                pressed = (ConsoleKey)Console.ReadKey().Key;

                                // Delete the read key input.
                                DeleteKeyInput();
                            } while (pressed != ConsoleKey.Y && pressed != ConsoleKey.N);

                            // Submit.
                            if (pressed == ConsoleKey.Y)
                            {
                                // Get the player's name.
                                Console.WriteLine("Enter your name: ");
                                name = Console.ReadLine();

                                // The player entered a name.
                                if (!string.IsNullOrEmpty(name))
                                {
                                    // Add the highscore to the list.
                                    scoreList.Add(new Highscore(0, name, playerScore));
                                    scoreList.Sort((x, y) => y.Score.CompareTo(x.Score));

                                    // Remove the extra items.
                                    if (scoreList.Count >= scoreLimit)
                                    {
                                        int j = scoreList.Count - 1;

                                        while (j >= scoreLimit)
                                        {
                                            scoreList.RemoveAt(j);
                                            j--;
                                        }
                                    }

                                    // Save the highscores.
                                    xmlAccess.SaveScores(scoreList);
                                }
                            }
                        }
                    }
                }
                catch
                { }
            }
            catch
            { }
        }

        #endregion Functions

        #endregion Methods
    }
}
