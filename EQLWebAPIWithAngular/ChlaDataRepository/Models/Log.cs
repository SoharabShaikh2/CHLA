using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ChlaDataRepository
{
    public class Log : ILog
    {
        private long _id;
        private string _log;
        private string _mlog;
        private string _Session_ID;
        private string _clientname;


        [Key]
        public long id { get => _id; set => _id = value; }
        public string log { get => _log; set => _log = value; }
        public string mlog { get => _mlog; set => _mlog = value; }
        public string Session_ID { get => _Session_ID; set => _Session_ID = value; }
        public string clientname { get => _clientname; set => _clientname = value; }
    }
}
