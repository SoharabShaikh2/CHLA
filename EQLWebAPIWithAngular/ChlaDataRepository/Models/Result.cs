using System;
using System.Collections.Generic;
using System.Text;

namespace ChlaDataRepository
{
    public class Result : IResult
    {

        private long _id;
        private string _userid;
        private string _scenarioname;
        private DateTime _DateTimeSession;
        private string _ResultJSon;

        public long id { get => _id; set => _id = value; }
        public string userid { get => _userid; set => _userid = value; }
        public string scenarioname { get => _scenarioname; set => _scenarioname = value; }
        public DateTime DateTimeSession { get => _DateTimeSession; set => _DateTimeSession = value; }
        public string ResultJSon { get => _ResultJSon; set => _ResultJSon = value; }
    }
}
