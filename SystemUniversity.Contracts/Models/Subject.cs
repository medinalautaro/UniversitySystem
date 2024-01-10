using System.ComponentModel;
using System.IO.Compression;
using System.Runtime.CompilerServices;


namespace SystemUniversity.Contracts.Models{
    public class Subject
    {
        public int Id;
        public string Name;

        public Subject(string name, int? id = null)
        {
            this.Name = name;
            this.Id = id ?? 0;
        }
    }
}