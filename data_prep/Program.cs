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

            ///// SETTINGS /////
            int game_limit = 1;
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
                            int selected_neuron_index = iopt.SelectAppropriateOutputNeuronIndex(bp, move, bp.ToMove);
                            float[] outputs = iopt.PrepareOutputs(selected_neuron_index);

                            //Compress
                            int[] inputs_compressed = iopt.Compress(inputs);
                            int[] outputs_compressed = iopt.Compress(outputs);

                            //Write
                            tw.Add(inputs, outputs);
                            
                            bp.ExecuteMove(move);
                        }

                        Console.WriteLine(pgn.Moves.Length.ToString("#,##0") + " moves saved!");
                        on_game_number = on_game_number + 1;
                    }
                }
                catch
                {
                    Console.WriteLine("Critical failure on that PGN!");
                }
            }            

            

        }

        
    
    }
}