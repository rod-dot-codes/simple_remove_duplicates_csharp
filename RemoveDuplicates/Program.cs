using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RemoveDuplicates
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Enter directory where to search:");
			var input = Console.ReadLine();
			string[] files = Directory.GetFiles(input, "*", SearchOption.AllDirectories);
			//We now use a lookup to build a List of the files.
			List<Dictionary<string,int>> values = new List<Dictionary<string, int>>();
			foreach (string file_id in files)
			{
				Console.WriteLine(file_id);
				bool contains = file_id.Contains("(");
				if (!contains)
				{
					var newValue = new Dictionary<string, int> { { file_id, 1}};

					values.Add(newValue);
				}
				else
				{
					Match match = Regex.Match(file_id, @"\((.*?)\)");
					Console.WriteLine(match.Groups[0].Value);
					int number = Int32.Parse(match.Groups[0].Value.Replace("(","").Replace(")",""));
					var filename = file_id.Replace(Regex.Match(file_id, @"( \(.*?\))").Groups[0].Value, "");
					
					
					var newValue = new Dictionary<string,int> { { filename, number}};
					values.Add(newValue);
				}
			}
				var results = values.SelectMany(d => d)
				  .GroupBy(d => d.Key)
				  .Select(g => new
			{
				GroupName = g.Key,
				MaxValue = g.Max(i => i.Value)
			});

			Directory.CreateDirectory(Path.Combine(input, "output"));

			foreach (var item in results)
			{
				Console.WriteLine(item);
				//We now copy the most relevent to a new folder.
				if (item.MaxValue == 1)
				{

					File.Copy(item.GroupName, Path.Combine(input, "output", Path.GetFileName(item.GroupName)));
				}
				else
				{
					var Replace = String.Format(" ({0}).", item.MaxValue);
					File.Copy(item.GroupName.Replace(".", Replace), Path.Combine(input, "output", Path.GetFileName(item.GroupName)));
				}
			}
			Console.WriteLine("Completed");
			Console.ReadLine();
		}
	}
}
