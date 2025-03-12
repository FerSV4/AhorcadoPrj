
public class Palabra
{
    public string Valor { get; set; }
    public bool[] Mostrarletra { get; set; }
    public Palabra(string valor)
    {
        Valor = valor;
        Mostrarletra = new bool[valor.Length];
        MostrarAleatorio();
    }
    private void MostrarAleatorio()
    {
        Random random = new Random();
        for (int i = 0; i < Valor.Length; i++)
        {
            if (random.NextDouble() < 0.5)
            {
                Mostrarletra[i] = true;
            }
        }
    }
    public bool RevelarLetra(char letra)
    {
        for (int i = 0; i < Valor.Length; i++)
        {
            if (Valor[i] == letra && !Mostrarletra[i])
            {
                Mostrarletra[i] = true;
                return true;
            }
        }
        return false;
    }
    public string Mostrar()
    {
        string resultado = "";
        for (int i = 0; i < Valor.Length; i++)
        {
            if (Mostrarletra[i])
            {
                resultado += Valor[i];
            }
            else
            {
                resultado += "_";
            }
        }
        return resultado;
    }
}
public class Jugador
{
    public int Intentoincorrecto { get; set; }

    public bool probarletra(char letra, Palabra palabra)
    {
        if (palabra.RevelarLetra(letra))
        {
            return true;
        }
        else
        {
            Intentoincorrecto++;
            return false;
        }
    }
}
public class Ahorcado
{
    private List<string> Palabras { get; set; }
    private Palabra PalabraActual { get; set; }
    private Jugador Jugador { get; set; }

    public Ahorcado(List<string> palabras)
    {
        Palabras = palabras;
        Jugador = new Jugador();
        PalabraAleatoria();
    }

    private void PalabraAleatoria()
    {
        Random random = new Random();
        PalabraActual = new Palabra(Palabras[random.Next(Palabras.Count)]);
    }
    public void IniciarJuego()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Palabra: " + PalabraActual.Mostrar());
            Console.WriteLine("Incorrectas: " + Jugador.Intentoincorrecto);
            Console.Write("Ingresar letra: ");
            char letra = Console.ReadKey().KeyChar;
            Console.WriteLine();
            if (!Jugador.probarletra(letra, PalabraActual))
            {
                Console.WriteLine("Incorrecta");
                Console.ReadKey();
            }
            if (Jugador.Intentoincorrecto >= 5)
            {
                Console.WriteLine("Game Over, la palabra era: " + PalabraActual.Valor);
                break;
            }
            if (PalabraActual.Mostrar().IndexOf('_') == -1)
            {
                Console.WriteLine("WIN! La palabra era: " + PalabraActual.Valor);
                break;
            }
        }
    }
}
//---------------------------------------------------------------
class Program
{
    static void Main()
    {
        List<string> palabras = new List<string>
        {
            "universidad",
            "catolica",
            "boliviana",
            "san",
            "pablo"
        };

        Ahorcado juego = new Ahorcado(palabras);
        juego.IniciarJuego();
    }
}