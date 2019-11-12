using System;
using System.Linq;
using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using System.Text;

namespace UAHBot
{

    class Program
    {
        static ITelegramBotClient botClient;
        static void Main(string[] args)
        {
            botClient = new TelegramBotClient("TOKEN");
            var me = botClient.GetMeAsync().Result;
            Console.WriteLine(
              $"Iniciando bot {me.Id} con nombre {me.FirstName}."
            );

            botClient.OnMessage += Bot_OnMessage;
            botClient.StartReceiving();
            Thread.Sleep(int.MaxValue);
        }
        static async void Bot_OnMessage(object sender, MessageEventArgs e)
        {
            string testFirsts = "\n-Espaghetti  \n-Judias blancas  \n-Verduras";
            string testSeconds = "\n-San jacobos \n-Lomos  \n-Filetes de merluza";

            if (e.Message.Text == null)
            {

            }
            //Help
            else if (e.Message.Text.Equals("/help"))
            {
                Help(e);
            }
            //Degree Schedule
            else if (e.Message.Text.StartsWith("/horario"))
            {
                DegreeSchedule(e);
            }
            //Menu
            else if (e.Message.Text.Equals("/menu"))
            {
                Menu(e, testFirsts, testSeconds);
            }
            //UAH Events
            else if (e.Message.Text.Equals("/eventos"))
            {
                Events(e);
            }
            //UAH Sports page
            else if (e.Message.Text.Equals("/deportes"))
            {
                Sports(e);
            }
            //Final exams calendar
            else if (e.Message.Text.StartsWith("/examenesfinales"))
            {
                FinalExams(e);
            }
            //Pasts years exams
            else if (e.Message.Text.StartsWith("/examenes"))
            {
                Exams(e);
            }
            //Subjects and teachers
            else if (e.Message.Text.StartsWith("/asignaturas"))
            {
                Subject(e);
            }
            //Professors list
            else if (e.Message.Text.Equals("/profesores"))
            {
                Professors(e);
            }
            //Professor data, office and email
            else if (e.Message.Text.StartsWith("/profesor"))
            {
                Professor(e);
            }

        }

        static string[] GetArgs(string message)
        {
            string[] args = { };
            if (message != null)
                args = message.Split(' ').Skip(1).ToArray();
            return args;
        }

        static async void Help(MessageEventArgs e)
        {
            string help = System.IO.File.ReadAllText(@"C:\Users\alex1\source\repos\ConsoleApp1\ConsoleApp1\Help.txt", Encoding.UTF8);
            Console.WriteLine($"Solicitud de eventos por parte de {e.Message.Chat.Id}.");
            Message message = await botClient.SendTextMessageAsync(
               chatId: e.Message.Chat,
               text: help,
               parseMode: ParseMode.Markdown,
               disableNotification: true
             );
        }
        static async void DegreeSchedule(MessageEventArgs e)
        {
            string[] args = GetArgs(e.Message.Text);
            string[] grados = { "GII", "GIC", "GISI" };
            Console.WriteLine(args.Length);
            if (args.Length == 2)
            {
                if (System.Convert.ToInt32(args[1]) > 2)
                {
                    Console.WriteLine($"Error with message arguments in schedule{e.Message.Chat.Id}.");
                    Message schedule = await botClient.SendTextMessageAsync(
                      chatId: e.Message.Chat,
                      text: $"Solo existe primer o segundo cuatrimestre",
                      parseMode: ParseMode.Markdown,
                      disableNotification: true
                    );
                }
                else
                {
                    if (!grados.Contains(args[0]))
                    {
                        Console.WriteLine($"Error with message arguments in schedule{e.Message.Chat.Id}.");
                        Message schedule = await botClient.SendTextMessageAsync(
                          chatId: e.Message.Chat,
                          text: $"No existe el grado seleccionado",
                          parseMode: ParseMode.Markdown,
                          disableNotification: true
                        );
                    }
                    else
                    {
                        Console.WriteLine($"Received a schedule message in chat {e.Message.Chat.Id}.");
                        Message schedule = await botClient.SendTextMessageAsync(
                          chatId: e.Message.Chat,
                          text: $"Horarios de {args[0]} del semestre {args[1]}",
                          parseMode: ParseMode.Markdown,
                          disableNotification: true,
                          replyMarkup: new InlineKeyboardMarkup(InlineKeyboardButton.WithUrl(
                            "Horarios",
                            $"http://escuelapolitecnica.uah.es/estudiantes/documentos/Semestre" + args[1] + args[0] + ".pdf"
                             ))
                        );
                    }
                }
            }
            else
            {
                Console.WriteLine($"Error with message arguments in schedule{e.Message.Chat.Id}.");
                Message schedule = await botClient.SendTextMessageAsync(
                  chatId: e.Message.Chat,
                  text: $"Debes de introducir solo dos etiquetas, la primera curso y la segunda semestre",
                  parseMode: ParseMode.Markdown,
                  disableNotification: true
                );
            }

        }

        static async void Subject(MessageEventArgs e)
        {
            string[] args = GetArgs(e.Message.Text);
            Console.WriteLine(args.Length);
            if (args.Length == 2)
            {
                string subjectsGII_1 = System.IO.File.ReadAllText(@"GII_1.txt", Encoding.UTF8);

                string subjectsGII_2 = System.IO.File.ReadAllText(@"GII_2.txt", Encoding.UTF8);

                string subjectsGII_3 = System.IO.File.ReadAllText(@"GII_3.txt", Encoding.UTF8);

                string subjectsGIC_1 = System.IO.File.ReadAllText(@"GIC_1.txt", Encoding.UTF8);

                string subjectsGIC_2 = System.IO.File.ReadAllText(@"GIC_2.txt", Encoding.UTF8);

                string subjectsGIC_3 = System.IO.File.ReadAllText(@"GIC_3.txt", Encoding.UTF8);

                string subjectsGIC_4 = System.IO.File.ReadAllText(@"GIC_4.txt", Encoding.UTF8);

                string subjectsGISI_1 = System.IO.File.ReadAllText(@"GISI_1.txt", Encoding.UTF8);

                string subjectsGISI_2 = System.IO.File.ReadAllText(@"GISI_2.txt", Encoding.UTF8);

                string subjectsGISI_3 = System.IO.File.ReadAllText(@"GISI_3.txt", Encoding.UTF8);

                string[] subjectsGII = { subjectsGII_1, subjectsGII_2, subjectsGII_3 };
                string[] subjectsGIC = { subjectsGIC_1, subjectsGIC_2, subjectsGIC_3, subjectsGIC_4 };
                string[] subjectsGISI = { subjectsGISI_1, subjectsGISI_2, subjectsGISI_3 };

                string subjects = "";
                int i = System.Convert.ToInt32(args[1]);
                if (args[0].Equals("GII"))
                {
                    subjects = subjectsGII[i - 1];
                }
                else if (args[0].Equals("GIC"))
                {
                    subjects = subjectsGIC[i - 1];
                }
                else if (args[0].Equals("GISI"))
                {
                    subjects = subjectsGISI[i - 1];
                }
                Console.WriteLine($"Received a subject message in chat {e.Message.Chat.Id}.");
                Message schedule = await botClient.SendTextMessageAsync(
                  chatId: e.Message.Chat,
                  text: $"Asignaturas del grado {args[0]} del año {args[1]}\n" + subjects,
                  parseMode: ParseMode.Markdown,
                  disableNotification: true
                );
            }
            else
            {
                Console.WriteLine($"Error with message arguments in schedule{e.Message.Chat.Id}.");
                Message schedule = await botClient.SendTextMessageAsync(
                  chatId: e.Message.Chat,
                  text: $"Debes de introducir solo dos etiquetas, la primera curso y la segunda semestre",
                  parseMode: ParseMode.Markdown,
                  disableNotification: true
                );
            }

        }

        //Final exams
        static async void FinalExams(MessageEventArgs e)
        {
            //Get the different arguments
            string[] args = GetArgs(e.Message.Text);
            //Check for arguments
            if (args.Length == 1)
            {
                //Store the degree
                string degree = args[0];
                //Check if degree is GII
                if (args[0].Equals("GII") || args[0].Equals("Ingenieria informatica") || args[0].Equals("Informatica"))
                {
                    degree = "GII";
                }
                //Check if degree is GIC
                else if (args[0].Equals("GIC") || args[0].Equals("Ingenieria de computadores") || args[0].Equals("Computadores"))
                {
                    degree = "GIC";
                }
                //Check if degree is GISI
                else if (args[0].Equals("GISI") || args[0].Equals("Ingenieria de sistemas") || args[0].Equals("Sistemas"))
                {
                    degree = "GISI";
                }
                //If the degree is not valid
                else
                {
                    Console.WriteLine($"Received an exam request in chat {e.Message.Chat.Id}.");
                    //Send message to user
                    Message errores = await botClient.SendTextMessageAsync(
                      chatId: e.Message.Chat,
                      text: $"No existe el grado solicitado",
                      parseMode: ParseMode.Markdown,
                      disableNotification: true
                    );
                    return;
                }
                //If the degree is valid, the url is created with the degree initials
                Console.WriteLine($"Received a final exam request in chat {e.Message.Chat.Id}.");
                Message error = await botClient.SendDocumentAsync(
                  chatId: e.Message.Chat,
                  document: "http://escuelapolitecnica.uah.es/estudiantes/documentos/Examenes" + degree + ".pdf",
                  parseMode: ParseMode.Markdown,
                  disableNotification: true
                  );
            }
            else
            {
                //If there is no degree specified, notify the user 
                Console.WriteLine($"Error with message arguments in schedule{e.Message.Chat.Id}.");
                Message schedule = await botClient.SendTextMessageAsync(
                  chatId: e.Message.Chat,
                  text: $"Debes de introducir solo una etiqueta, correspondiente al curso.",
                  parseMode: ParseMode.Markdown,
                  disableNotification: true
                );
            }
        }

        static async void Professor(MessageEventArgs e)
        {
            string[] professors = {"Javier Albert Segui Despacho N201, javier.albert@uah.es",
                "Tomasa Calvo Sanchez Despacho N337, tomasa.calvo@uah.es",
                "Ana Castillo Martinez Despacho N335, ana.castillo@uah.es",
                "Felipe Catedra Perez Despacho N342, felipe.catedra@uah.es",
                "Juan Jose Cuadrado Gallego Despacho O343, jjcg@uah.es",
                "Maria Angeles Fernandez de Sevilla Despacho N318, marian.fernandez@uah.es",
                "Antonio Garcia Cabot Despacho N324, a.garciac@uah.es",
                "Jose Maria Gomez Pulido Despacho N224, jose.gomez@uah.es",
                "Oscar Gutierrez Blanco Despacho N311, oscar.gutierrez@uah.es",
                "Jose Maria Gutierrez Martinez Despacho N316, josem.gutierrez@uah.es",
                "Jose Amelio Medina Merodio Despacho N346, josea.medina@uah.es",
                "Marçal Mora Cantallops Despacho O242, marcal.mora@uah.es",
                "Jose Enrique Morais San Miguel Despacho N343,je.morais@uah.es",
                "Antonio Moratilla Ocaña Despacho N334, antonio.moratilla@uah.es",
                "Enriqueta Muel Muel Despacho N333, enriqueta.muel@uah.es",
                "Ignacio Olmeda Martos Despacho N328, josei.olmeda@uah.es",
                "Carmen Pages Arevalo Despacho N236, carmina.pages@uah.es",
                "Rosalia Peña Ros Despacho N235, rpr@uah.es",
                "Francisco Saez de Adana Herrero Despacho N223, kiko.saez@uah.es",
                "Abdelhamid Tayebi Tayebi Despacho N225, hamid.tayebi@uah.es" };
            string[] args = GetArgs(e.Message.Text);
            Console.WriteLine(args.Length);
            if (args.Length > 0)
            {
                string busqueda = "";
                for (int i = 0; i < args.Length; i++)
                {
                    busqueda += args[i] + " ";
                }
                string professor = "";
                for (int j = 0; j < professors.Length; j++)
                {
                    if (professors[j].StartsWith(busqueda))
                    {
                        professor += "\n" + professors[j];
                    }
                }
                if (professor.Equals(""))
                {
                    professor = "No se han encontrado profesores";
                }
                Console.WriteLine($"Consulta del profesor {busqueda}.");
                Message message = await botClient.SendTextMessageAsync(
                   chatId: e.Message.Chat,
                   text: professor,
                   parseMode: ParseMode.Markdown,
                   disableNotification: true
                 );

            }
            else
            {
                Console.WriteLine($"Error with message arguments in schedule{e.Message.Chat.Id}.");
                Message schedule = await botClient.SendTextMessageAsync(
                  chatId: e.Message.Chat,
                  text: $"Debes de introducir solo una etiqueta, correspondiente al profesor.",
                  parseMode: ParseMode.Markdown,
                  disableNotification: true
                );
            }

        }

        static async void Professors(MessageEventArgs e)
        {
            string professors = "Javier Albert Segui, Despacho N201" +
                "\nTomasa Calvo Sanchez, Despacho N337" +
                "\nAna Castillo Martinez, Despacho N335" +
                "\nFelipe Catedra Perez, Despacho N342" +
                "\nJuan Jose Cuadrado Gallego, Despacho O343" +
                "\nMª Angeles Fernandez de Sevilla, Despacho N318" +
                "\nAntonio Garcia Cabot, Despacho N324" +
                "\nJose Maria Gomez Pulido, Despacho N224" +
                "\nOscar Gutierrez Blanco, Despacho N311" +
                "\nJose Maria Gutierrez Martinez, Despacho N316" +
                "\nJose Amelio Medina Merodio, Despacho N346" +
                "\nMarçal Mora Cantallops, Despacho O242" +
                "\nJose Enrique Morais San Miguel, Despacho N343" +
                "\nAntonio Moratilla Ocaña, Despacho N334" +
                "\nEnriqueta Muel Muel, Despacho N333" +
                "\nIgnacio Olmeda Martos, Despacho N328" +
                "\nCarmen Pages Arevalo, Despacho N236" +
                "\nRosalia Peña Ros, Despacho N235" +
                "\nFrancisco Saez de Adana Herrero, Despacho N223" +
                "\nAbdelhamid Tayebi Tayebi, Despacho N225";
            Console.WriteLine($"Consulta de los profesores de {e.Message.Chat.Id}.");
            Message message = await botClient.SendTextMessageAsync(
               chatId: e.Message.Chat,
               text: professors,
               parseMode: ParseMode.Markdown,
               disableNotification: true
             );
        }

        static async void Exams(MessageEventArgs e)
        {
            string[] args = GetArgs(e.Message.Text);
            Console.WriteLine(args.Length);
            if (args.Length == 1)
            {
                string url = "";
                if (args[0].Equals("GII") || args[0].Equals("Ingenieria informatica") || args[0].Equals("Informatica"))
                {
                    url = "http://dei.uah.es/index.php/examenes/ingenieria-informatica/";
                }
                else if (args[0].Equals("GIC") || args[0].Equals("Ingenieria de computadores") || args[0].Equals("Computadores"))
                {
                    url = "http://dei.uah.es/index.php/examenes/ingenieria-de-computadores/";
                }
                else if (args[0].Equals("GISI") || args[0].Equals("Ingenieria de sistemas") || args[0].Equals("Sistemas"))
                {
                    url = "http://dei.uah.es/index.php/examenes/sistemas-de-informacion/";
                }
                else
                {
                    Console.WriteLine($"Received an exam request in chat {e.Message.Chat.Id}.");
                    Message error = await botClient.SendTextMessageAsync(
                      chatId: e.Message.Chat,
                      text: $"No existe el grado solicitado",
                      parseMode: ParseMode.Markdown,
                      disableNotification: true
                    );
                }
                Console.WriteLine($"Received an exam request in chat {e.Message.Chat.Id}.");
                Message schedule = await botClient.SendTextMessageAsync(
                  chatId: e.Message.Chat,
                  text: $"Examenes de {args[0]}",
                  parseMode: ParseMode.Markdown,
                  disableNotification: true,
                  replyMarkup: new InlineKeyboardMarkup(InlineKeyboardButton.WithUrl(
                    "Examenes",
                    url
                     ))
                );
            }
            else
            {
                Console.WriteLine($"Error with message arguments in schedule{e.Message.Chat.Id}.");
                Message schedule = await botClient.SendTextMessageAsync(
                  chatId: e.Message.Chat,
                  text: $"Debes de introducir solo una etiqueta, correspondiente al curso.",
                  parseMode: ParseMode.Markdown,
                  disableNotification: true
                );
            }

        }

        static async void Menu(MessageEventArgs e, string firsts, string seconds)
        {
            Console.WriteLine($"Solicitud de menu por parte de {e.Message.Chat.Id}.");
            Message message = await botClient.SendTextMessageAsync(
               chatId: e.Message.Chat,
               text: $"Primeros: {firsts} \n Segundos: {seconds}",
               parseMode: ParseMode.Markdown,
               disableNotification: true
             );
        }
        static async void Events(MessageEventArgs e)
        {
            Console.WriteLine($"Solicitud de eventos por parte de {e.Message.Chat.Id}.");
            Message message = await botClient.SendTextMessageAsync(
               chatId: e.Message.Chat,
               text: $"Eventos de la UAH \n En el siguiente enlace podrá consultarlos.",
               parseMode: ParseMode.Markdown,
               disableNotification: true,
               replyMarkup: new InlineKeyboardMarkup(InlineKeyboardButton.WithUrl(
                    "Eventos",
                    "http://www3.uah.es/cultura/index.php/todos-los-eventos"
                     ))
             );
        }
        static async void Sports(MessageEventArgs e)
        {
            Console.WriteLine($"Solicitud de deportes por parte de {e.Message.Chat.Id}.");
            Message message = await botClient.SendTextMessageAsync(
               chatId: e.Message.Chat,
               text: $"Deportes de la UAH \n En el siguiente enlace podrá consultarlos.",
               parseMode: ParseMode.Markdown,
               disableNotification: true,
               replyMarkup: new InlineKeyboardMarkup(InlineKeyboardButton.WithUrl(
                    "Deportes",
                    "https://www.uah.es/es/vivir-la-uah/actividades/deportes/"
                     ))
             );
        }
    }

}
