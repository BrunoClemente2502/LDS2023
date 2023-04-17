

using System.Data;

namespace AnaliseImagens
{
    class Controller
    {
        //Atributos da classe 
        private View view;
        private Model model; 
        private string? command;

        //Construtor sem parâmetros
        public Controller() {
            view = new View(this); // Pass the Controller reference to the View
            model = new Model(this, view); // Pass both the Controller and View references to the Model
            view.Model = model; // Set the Model reference in the View
            command = null;

            List<string> availableCmds = model.ListarComandos();
            view.ApresentarInstrucoes(availableCmds);

            Console.WriteLine("Introduza um comando:");
            command = Console.ReadLine();

            try
            {
               model.ValidarComando(command);
               model.ExecutarComando(command);
                
            } catch (Exception excp)
            {
              view.HandleException(excp);
            }

            
        }


     
    }
}
