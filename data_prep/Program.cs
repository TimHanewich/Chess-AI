using System;
using TimHanewich.Chess;
using TimHanewich.Chess.PGN;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ChessAI
{
    public class Program
    {

        public static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                if (args[0] == "encode" || args[0] == "decode")
                {
                    encodedecode(args);
                }
            }
        }

        //i.e. "encode rnbqkbnr/1pppppp1/8/6Pp/p7/1P6/P1PPPP1P/RNBQKBNR w KQkq h6 0 4" will produce an input array of floats
        //i.e. "decode 645" will return the move
        public static void encodedecode(string[] args)
        {
            if (args[0] == "encode")
            {
                InputOutputPrepTools iopt = new InputOutputPrepTools(System.IO.File.ReadAllText(@"C:\Users\timh\Downloads\tah\chess-ai\data_prep\InputOutputPrep\standard_moves.json"));
                string fen = args[1];
                fen = fen.Replace("\"", "");
                BoardPosition bp = new BoardPosition(fen);
                float[] inputs = iopt.PrepareInputs(bp);
                Console.WriteLine(JsonConvert.SerializeObject(inputs, Formatting.None));
            }
            else if (args[0] == "decode")
            {
                Console.WriteLine("Not supported yet!");
            }
        }

        public static void PrepareTrainingData()
        {
            ///// SETTINGS /////
            int game_limit = 50000;
            int elo_floor = 1200;
            ///////////////////

            
            //Set up the tool that will prepare the input + outputs and the tool that will write to the file
            InputOutputPrepTools iopt = new InputOutputPrepTools(System.IO.File.ReadAllText(@"C:\Users\timh\Downloads\tah\chess-ai\data_prep\InputOutputPrep\standard_moves.json"));
            TrainingWriter tw = new TrainingWriter(@"C:\Users\timh\Downloads\tah\chess-ai\training.jsonl");

            //Open massive stream and prepare to split
            Stream s = System.IO.File.OpenRead(@"C:\Users\timh\Downloads\tah\chess-ai\lichess_db_standard_rated_2023-03.pgn");
            MassivePgnFileSplitter splitter = new MassivePgnFileSplitter(s);
            
            int on_game_number = 1;
            while (on_game_number <= game_limit)
            {
                try
                {
                    
                    string pgn_ = splitter.NextGame();
                    PgnFile pgn = PgnFile.ParsePgn(pgn_);

                    if (pgn.WhiteElo >= elo_floor && pgn.BlackElo >= elo_floor)
                    {
                        Console.Write("On game # " + on_game_number.ToString("#,##0") + "... ");
                        BoardPosition bp = new BoardPosition("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1"); //Start from new position
                        foreach (string m in pgn.Moves)
                        {   
                            Move move = new Move(m, bp);

                            //Prepare inputs
                            float[] inputs = iopt.PrepareInputs(bp);
                            
                            //Prepare outputs
                            int selected_output_neuron_index = iopt.SelectAppropriateOutputNeuronIndex(bp, move, bp.ToMove);

                            //Compress
                            int[] inputs_compressed = iopt.Compress(inputs);

                            //Write
                            tw.Add(inputs_compressed, selected_output_neuron_index);
                            
                            bp.ExecuteMove(move);
                        }

                        Console.WriteLine(pgn.Moves.Length.ToString("#,##0") + " moves saved!");
                        on_game_number = on_game_number + 1;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine();
                    Console.WriteLine("Critical failure on that PGN! Message: " + ex.Message);
                    Console.WriteLine();
                    Console.Write("Press enter to continue... ");
                }
            }           
        }
        
    
    }
}