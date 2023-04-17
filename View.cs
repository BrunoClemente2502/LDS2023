

namespace AnaliseImagens
{
    class View
    {
        //Atributos
        private Controller controller;
        private Model model;

        public Model Model
        {
            get { return model; }
            set
            {
                model = value;
                model.OnResultsAvailable += ApresentarResultados;
            }
        }
        //Construtor
        public View(Controller _controller) 
        {
            controller = _controller;
        }

        public void ApresentarInstrucoes(List <string> availableCommands) 
        {
            Console.WriteLine("Available commands:");
            foreach (string command in availableCommands)
            {
                Console.WriteLine($"- {command}");
            }
        }

        public void HandleException(Exception excp) {
            if (excp is NoCommandFound)
            {
                Console.WriteLine(excp.Message);
                Environment.Exit(ExitCodes.ERROR_NO_COMMAND);
            }
            else if (excp is CommandNotValid)
            {
                Console.WriteLine(excp.Message);
                Environment.Exit(ExitCodes.ERROR_INVALID_COMMAND);
            }
            else if (excp is InvalidPath)
            {
                Console.WriteLine(excp.Message);
                Environment.Exit(ExitCodes.ERROR_INVALID_IMAGE);

            }
            else if (excp is OperationError)
            {
                Console.WriteLine(excp.Message);
                Environment.Exit(ExitCodes.ERROR_OPERATION_NOT_SUCCESSFUL);
            }   
        }

    
        public void ApresentarResultados(object sender, ResultsEventArgs e)
        {
             ColorPercentages results = e.Results;

            Console.WriteLine("Results:");
            Console.WriteLine($"Red: {results.RedPercentage}%");
            Console.WriteLine($"Green: {results.GreenPercentage}%");
            Console.WriteLine($"Blue: {results.BluePercentage}%");

            Environment.Exit(ExitCodes.SUCCESS);
        }

        public static class ExitCodes
        {
            public const int ERROR_NO_COMMAND = 1;
            public const int ERROR_INVALID_COMMAND = 2;
            public const int ERROR_INVALID_IMAGE = 3;
            public const int ERROR_OPERATION_NOT_SUCCESSFUL = 4;
            public const int SUCCESS = 0;
        }
    }
}