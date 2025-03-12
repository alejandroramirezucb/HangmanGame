using System;
using System.Data;
using System.Security.Principal;

public class main
{
    static void Main(string[] args)
    {
        GameHangman game = new GameHangman(6);
        game.play();
    }
}

public class Dictionary
{
    private string[] dictionary = new string[] {"ingeniero", "software", "elefante", "computadora", "telefono", "television" , "internacional", "restaurantes", "inteligencia", "industrial", "adolescentes", "comerciante", "profesional", "saturno", "religion", "lampara", "camioneta", "explorador", "astronauta", "teclado", "republica", "corona", "mariposa", "labrador", "martillo", "cocina"};
    private string word;
    // Getters/Setters
    public string getWord()
    {
        return word;
    }
    public void setWord(string word)
    {
        this.word = word;
    }
    public string[] getDictionary()
    {
        return dictionary;
    }
}

public class GameHangman
{
    Dictionary dictionary = new Dictionary();
    Hangman tallyMark = new Hangman();
    string hiddenWord;
    string dashes;
    int attempts = 0;
    // Constructor
    public GameHangman(int attempts)
    {
        this.hiddenWord = randomWord(dictionary.getDictionary());
        setAttempts(attempts, false);
        for (int index = 0; index < getHiddenWord().Length; ++index)
        {
            dashes += "_";
        }
    }
    // Getters/Setters
    private string getHiddenWord()
    {
        return hiddenWord;
    }
    public string getDashes()
    {
        return dashes;
    }
    public void setDashes(int index)
    {
        char[] dashesArray = getDashes().ToCharArray();
        if (index >= 0 && index < getHiddenWord().Length)
        {
            dashesArray[index] = getHiddenWord()[index];
        }
        this.dashes = new string(dashesArray);
    }
    public int getAttempts()
    {
        return attempts;
    }
    public void setAttempts(int attempts, bool check = true)
    {
        if (attempts <= 0 && check == false)
        {
            Console.WriteLine("¡Error! El numero de intentos debe ser mayor a 0");
        }
        else
        {
            this.attempts = attempts;
        }
    }
    // Funciones/Procedimientos
    public string randomWord(string[] dictionary)
    {
        Random random = new Random();
        int index = random.Next(0, dictionary.Length);
        return dictionary[index];
    }
    public bool checkLetter(char letter)
    {
        for (int index = 0; index < getHiddenWord().Length; ++index)
        {
            if (getHiddenWord()[index] == letter)
            {
                return true;
            }
        }
        return false;
    }
    public void play()
    {
        while (getAttempts() > 0)
        {
            Console.WriteLine("------------------------EL AHORCADO------------------------");
            Console.WriteLine("Palabra Oculta: " + getDashes());
            Console.Write("Ingrese una letra: ");
            string input = Console.ReadLine().ToLower();
            if (input.Length == 0)
            {
                Console.WriteLine("¡Error! Debes ingresar una letra o una palabra");
                continue;
            }
            if (input.Length > 1)
            {
                if (input != getHiddenWord())
                {
                    setAttempts(getAttempts() - 1);
                    tallyMark.drawHangman(getAttempts());
                    if (getAttempts() == 1)
                    {
                        Console.WriteLine("¡Letra equivocada! Queda " + getAttempts() + " intento");
                    }
                    else
                    {
                        Console.WriteLine("¡Letra equivocada! Quedan " + getAttempts() + " intentos");
                    }
                    continue;
                }
                if (input == getHiddenWord())
                {
                    Console.WriteLine("¡Felicidades! Has adivinado la palabra");
                    return;
                }
            }
            if (input.Length == 1)
            {
                if (checkLetter(input[0]) == false)
                {
                    setAttempts(getAttempts() - 1);
                    tallyMark.drawHangman(getAttempts());
                    if (getAttempts() == 1)
                    {
                        Console.WriteLine("¡Letra equivocada! Queda " + getAttempts() + " intento");
                    }
                    else
                    {
                        Console.WriteLine("¡Letra equivocada! Quedan " + getAttempts() + " intentos");
                    }
                    continue;
                }

                if (checkLetter(input[0]) == true)
                {
                    for (int index = 0; index < getHiddenWord().Length; ++index)
                    {
                        if (getHiddenWord()[index] == input[0])
                        {
                            setDashes(index);
                        }
                    }
                }
            }            
            if (getDashes() == getHiddenWord())
            {
                Console.WriteLine("¡Felicidades! Has adivinado la palabra");
                Console.WriteLine("-----------------------------------------------------------");
                return;
            }
        }
        Console.WriteLine("¡Has Muerto! No has logrado adivinar la palabra: " + getHiddenWord());
        Console.WriteLine("-----------------------------------------------------------");
        return;
    }
    private class Hangman
    {
        private string[] horca = {
        "  +---+",
        "  |   |",
        "      |",
        "      |",
        "      |",
        "      |",
        "========="
        };
        private char head = 'O';
        private char torso = '|';
        private char left_arm = '/';
        private char right_arm = '\\';
        private char left_leg = '/';
        private char right_leg = '\\';
        // Procedimientos
        public void drawHangman(int strike)
        {
            switch (strike)
            {
                case 5:
                    horca[2] = "  " + head + "   |";
                    break;
                case 4:
                    horca[3] = " " + left_arm + "    |";
                    break;
                case 3:
                    horca[3] = " " + left_arm + torso + "   |";
                    break;
                case 2:
                    horca[3] = " " + left_arm + torso + right_arm + "  |";
                    break;
                case 1:
                    horca[4] = " " + left_leg + "    |";
                    break;
                case 0:
                    horca[4] = " " + left_leg + " " + right_leg + "  |";
                    break;
            }
            print();

        }
        public void print()
        {
            for (int index = 0; index<horca.Length; ++index)
            {
                Console.WriteLine(horca[index]);
            }
        }
    }
}
