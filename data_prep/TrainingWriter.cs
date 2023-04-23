using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ChessAI
{
    public class TrainingWriter
    {
        private string file_path; //jsonl file path

        public TrainingWriter(string jsonl_file_path)
        {
            file_path = jsonl_file_path;
        }

        public void Add(int[] inputs, int output_neuron_index)
        {
            JObject jo = new JObject();
            jo.Add("inputs", JArray.Parse((JsonConvert.SerializeObject(inputs))));
            jo.Add("output", output_neuron_index);
            StreamWriter sw = System.IO.File.AppendText(file_path);
            sw.WriteLine(jo.ToString(Formatting.None));
            sw.Close();
        }
    }
}