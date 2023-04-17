using System;
using System.Collections.Generic;
using System.IO;


namespace AnaliseImagens
{

    public class ColorPercentages
    {
        public double RedPercentage { get; set; }
        public double GreenPercentage { get; set; }
        public double BluePercentage { get; set; }
    }
    
    //Definição das diferentes excepções que esta classe pode lançar
    class NoCommandFound : Exception
    {
        public NoCommandFound() : base("Deve introduzir um comando."){}
    }


    class CommandNotValid : Exception
    {
        public CommandNotValid(string command) : base($"Comando {command} não é válido.") { }
    }

    class InvalidPath : Exception
    {   
        public InvalidPath(string path) : base($"Não foi encontrada nenhuma imagem em {path}.") { }
    }

    class OperationError: Exception
    {
        public OperationError(string operation) : base($"Não foi posível executar a operação {operation} com sucesso.") { }
    }

    public delegate void ResultsHandler(object sender, ResultsEventArgs e);
    class Model
    {
        //Atributos
        private List<string> listCmds;
        private Controller controller;
        private View view;
        public delegate void CommandValidator(string commandReceived);
        public delegate void CommandExecutor(string commandReceived);
        public delegate void ResultsHandler(object sender, ResultsEventArgs e);
        public event ResultsHandler OnResultsAvailable;

        private Dictionary<string, CommandValidator> commandValidators;
        private Dictionary<string, CommandExecutor> commandExecutors;

        //Construtor
        public Model(Controller _controller, View _view)
        {
            controller = _controller;
            view = _view;
            listCmds = new List<string> {"analisar..."};

            // Initialize the dictionaries and add the corresponding delegates for each command
            commandValidators = new Dictionary<string, CommandValidator>
            {
                { "analyze", ValidarComando }
            };

            commandExecutors = new Dictionary<string, CommandExecutor>
            {
                { "analyze", ExecutarComando }
            };
        }

        public List<string> ListarComandos() 
        {
            return listCmds;
        }

         protected virtual void RaiseResultsAvailable(ColorPercentages results)
        {
            OnResultsAvailable?.Invoke(this, new ResultsEventArgs(results));
        }

        public void ValidarComando(string commandReceived)
        {
            if (commandValidators.TryGetValue(commandReceived, out CommandValidator validator))
            {
                validator(commandReceived);
            }
            else
            {
                throw new CommandNotValid(commandReceived);
            }
        }

        public void ExecutarComando(string commandReceived)
        {
            if (commandExecutors.TryGetValue(commandReceived, out CommandExecutor executor))
            {
                executor(commandReceived);
                OnResultsAvailable?.Invoke(this,  new ResultsEventArgs(FornecerResultado()));
            }
            else
            {
                throw new CommandNotValid(commandReceived);
            }
        }


       public ColorPercentages FornecerResultado()
        {
            // Calculate color percentages and return the result as a ColorPercentages object
            ColorPercentages colorPercentages = new ColorPercentages
            {
                RedPercentage = 0.30,
                GreenPercentage = 0.3,
                BluePercentage = 0.4,
            };

            return colorPercentages;
        }
        
     }

}