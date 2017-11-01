using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WCF.Communicator.Contracts.Models
{
    [DataContract]
    public class Message
    {
        [DataMember]
        public string Text { get; set; }
        [DataMember]
        public DateTime Date { get; set; }
    }
}
