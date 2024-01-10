using System.ComponentModel;
using System.IO.Compression;
using System.Runtime.CompilerServices;


namespace SystemUniversity.Contracts.Models{
    public class Professor
    {
        public int Id;
        public string Name;
        public string LastName;
        public string NationalId;

        public Professor(string name, string lastName, string nationalId, int? id = null)
        {
            this.Name = name;
            this.LastName = lastName;
            this.NationalId = nationalId;
            this.Id = id ?? 0;
        }
    }
}